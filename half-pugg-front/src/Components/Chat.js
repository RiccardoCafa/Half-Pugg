import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';
import { Message, Button, Input, List,Loader } from 'semantic-ui-react';
import ow from '../images/overwatch.jpg'
import api from '../services/api'
import Auth from '../Components/auth';
import {HttpTransportType,HubConnectionBuilder, LogLevel,} from '@aspnet/signalr'


import { request } from 'http';

export default class Chat extends Component {

    state = {
        Nickname: '',
        GamerLogado: {},
        message: '',
        messages: [],
        hubConnection: null,
        inpt_message: '',
        pog_ricc: false

       
    }

    async componentDidMount() {
        
        
        const jwt = localStorage.getItem("jwt");
        let stop = false;
        let myData;
        if(jwt){
            await api.get('api/Login', { headers: { "token-jwt": jwt }}).then(res => 
                myData = res.data
            ).catch(error => stop = true)
        } else {
            stop = true;
        }

        if(stop) {
            this.setState({toLogin: true});
            return;
        }
        
        this.setState({GamerLogado: myData})
        this.setNickname(myData);

        this.state.hubConnection = new HubConnectionBuilder().withUrl('https://localhost:44392/chat',{
            skipNegotiation: true,
            transport: HttpTransportType.WebSockets
          }).configureLogging(LogLevel.Information).build();
    
          this.state.hubConnection.start().then(() =>{
            console.log("conected!");

            this.state.hubConnection.on('receiveMessage',(message, userID) =>{
                //funcao chamada qnd uma mensagem é recebida
                console.log(message);
            });
            this.state.hubConnection.on('leavedGroup',(userID) =>{
                //funcao chamada qnd alguém sai do grupo
                console.log('Saiu do grupo: '+userID)
            });
            this.state.hubConnection.on('joinedGroup',(userID) =>{
                //funcao chamada qnd alguém entra no grupo
                console.log('Entrou do grupo: '+userID)
            });
            this.state.hubConnection.on('error',(erro) =>{
                //funcao chamada qnd alguém entra no grupo
                console.log(erro)
            });
           
            this.joinGroup(this.props.Group,myData.ID)
          
        }).catch(err => console.log(err));
        this.setState({pog_ricc:true})
    }


    connectPlayer(playerID){
        this.state.hubConnection.invoke('connect',playerID);
    }

    joinGroup(groupID,playerID){
        this.state.hubConnection.invoke('joinGroup',groupID,playerID);
    }

    leaveGroup(groupID,playerID){
        this.state.hubConnection.invoke('leaveGroup',groupID,playerID);
    }

    sendMessage(message,groupID,playerID){
        this.state.hubConnection.invoke('sendMessage',message,groupID,playerID);
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
        if(this.state.pog_ricc) this.sendMessage(this.state.inpt_message,this.props.Group,this.state.GamerLogado.ID)
    }

    hanldeInput = (e) => {
        this.setState({inpt_message: e.target.value});
    }
    // Faz uma requisição de match para outro gamer
    
    render() {
        if(!this.state.pog_ricc) { return <Loader active /> }
        return (
            <div>
            <List relaxed animated divided verticalAlign='middle' style={{'marginLeft': '5%'}} withd = {1000}>
                {this.state.messages.map((message) => 
                    <List.Item >      
                        <List.Header>{this.setNicknamePlayer(message.IdPlayer)}</List.Header>
                        <Message floating size='tiny' color='green'>{message.Content }</Message>                                     
                        
                    </List.Item>
                )}
                
            </List>
            <div>
                <Input icon='message' iconPosition='left' onChange={this.hanldeInput} value={this.state.inpt_message}/>
                <Button attached='left' onClick={this.clicouEnv() }>Send</Button>
            </div>
            </div>
            
        );
    }
}
