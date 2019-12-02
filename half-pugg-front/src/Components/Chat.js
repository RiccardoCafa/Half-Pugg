import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';
import { Message, Button, Input, List } from 'semantic-ui-react';
import ow from '../images/overwatch.jpg'

import api from '../services/api'
import Auth from '../Components/auth';
import {HttpTransportType,HubConnectionBuilder, LogLevel,} from '@aspnet/signalr'


import { request } from 'http';

export default class Chat extends Component {

    state = {
        messages: [],
        hubConnection: null
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
            });
            this.state.hubConnection.on('leavedGroup',(userID) =>{
                //funcao chamada qnd alguém sai do grupo
            });
            this.state.hubConnection.on('joinedGroup',(userID) =>{
                //funcao chamada qnd alguém entra no grupo
            });

        }).catch(err => console.log(err + "m"));

        const message = await api.get('api/GroupMenssages?IdGroup='+this.props.Group.ID);
        
        if (message!== null){
            
            this.setState({messages : message.data});
            
        }
        
       
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

    
    // Faz uma requisição de match para outro gamer
    
    render() {
               
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
                <Input icon='message' iconPosition='left' />
                <Button attached='left' >Send</Button>
            </div>
            </div>
            
        );
    }
}
