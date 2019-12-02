import React, {Component} from 'react'
import { Message, Button, Input, List,Segment } from 'semantic-ui-react';
import api from '../services/api'
import {HttpTransportType,HubConnectionBuilder, LogLevel,} from '@aspnet/signalr'

export default class Chat extends Component {

    state = {
        Nickname: '',
        GamerLogado: {},
        message: '',
        messages: [],
        hubConnection: null,
        inpt_message: '',
        pog_ricc: false,
        groupID: 0,
        lastMessRend: 0
    }

    async componentDidMount() {
        
        
        const jwt = localStorage.getItem("jwt");
        let stop = false;
        let gamer;
        if(jwt){
            await api.get('api/Login', { headers: { "token-jwt": jwt }}).then(res => 
                gamer = res.data
            ).catch(error => stop = true)
        } else {
            stop = true;
        }

        if(stop) {
            this.setState({toLogin: true});
            return;
        }
        
        this.setState({GamerLogado: gamer})
        this.setState({groupID:this.props.Group.ID})
        this.setNickname(gamer);
        this.startConnection(gamer.ID)       
        this.setState({pog_ricc:true})
    }

    startConnection = (gamerID)=>{
        const hub = new HubConnectionBuilder().withUrl('https://localhost:44392/chat',{
            skipNegotiation: true,
            transport: HttpTransportType.WebSockets
          }).configureLogging(LogLevel.Information).build();
    
          this.setState({hubConnection:hub})

         hub.start().then(() =>{
            console.log("conected!");

           hub.on('receiveMessage', this.receiveMessageClient);
           hub.on('leavedGroup',this.leaveGroupClient);
           hub.on('joinedGroup',this.joinGroupClient);
            
            api.get(`api/PlayerGroups/GetGroups?playerID=${gamerID}`).then(res=>{
                res.data.map((id)=>{
                    this.joinGroup(id,gamerID)
                    console.log('logged: '+id)
                })
            })

          
        }).catch(err => console.log(err));
    }

    loadChat = ()=>{
        api.get()
    }

    addMessageToChat = (send,message,senderId)=>{
        var oldMessages = [...this.state.messages]
        oldMessages.push({sender: send,content: message,id : senderId})
        this.setState({messages:oldMessages})
        console.log(this.state.messages)
    }

    joinGroupClient = (userID) =>{  
        console.log('Entrou do grupo: '+userID)
    }

    leaveGroupClient = (userID) =>{
    
        console.log('Saiu do grupo: '+userID)
    }

    receiveMessageClient = (message, userID,userName) =>{
        console.log(userName+': '+message);
        this.addMessageToChat(userName,message,userID)
    }

    
    
    connectPlayer(playerID){
        this.state.hubConnection.invoke('connect',playerID);
    }

    joinGroup(groupID,playerID){
        this.state.hubConnection.invoke('joinGroup',groupID.toString(),playerID);
    }

    leaveGroup(groupID,playerID){
        this.state.hubConnection.invoke('leaveGroup',groupID.toString(),playerID);
    }

    sendMessage(message,groupID,playerID){
        
        this.state.hubConnection.invoke('sendMessage',message,groupID.toString(),playerID,this.state.Nickname);
        var pg;

        api.get(`api/PlayerGroups?playerID=${playerID}&groupID=${groupID}`).then(res=>{
            pg = res.data
            api.post(`api/MessageGroups`,{
                'Content' : message,
                'Status' : 1,
                'Send_Time' : new Date().toISOString(),
                'ID_Relation' : pg.ID,
                'PlayerGroup' : pg,
            }).then(()=>{
                console.log('saved')
            }).catch(()=>{
                console.log('failed to save')
            })
        }).catch(erro=>{
            console.log('player or group not finded')
        })

        
    }



    setNicknamePlayer = async(gamer) => {
        const p = await api.get('api/Player/', gamer.ID);
        if (p!= null && p.data != null){
            return p.Nickname;
        }
        else return "Anonimo";
    }

    setNickname(myData) {
        this.setState({Nickname: myData.Nickname})
    }

    clicouEnv =()=>{
        if(this.state.inpt_message){
            this.sendMessage(this.state.inpt_message,this.state.groupID,this.state.GamerLogado.ID)
            this.setState({inpt_message:''})
        }
        
    }

    hanldeInput = (e) => {
        this.setState({inpt_message: e.target.value});
    }
    
    calcMessSize = (mess)=>{
        return mess.length<40? (80-mess.length):15;
    }

    render() {
        
        return (
            <div>
                <Segment textAlign='center' style={{overflow: 'auto', maxHeight: 400 }}>
                <List >
                    {this.state.messages.map((message) => 
                        <List.Item style={message.id === this.state.GamerLogado.ID ?{'marginLeft': `${this.calcMessSize(message.content)}%`}:{'marginRight':`${this.calcMessSize(message.content)}%`}}>      
                         
                            <List.Header as='a'>{message.sender}</List.Header>
                                <Message  size='mini' color={message.id === this.state.GamerLogado.ID ?'green': 'blue'}>
                                    <p>{message.content }</p> 
                                </Message>                 
                                        
                        </List.Item>
                    )} 
                </List>
            </Segment> 
            <div >
                <Input  icon='paper plane outline' iconPosition='left' onChange={this.hanldeInput} value={this.state.inpt_message}/>
                <Button attached='right' onClick={this.clicouEnv }>Send</Button>
            </div>
            </div>
            
        );
    }
}
