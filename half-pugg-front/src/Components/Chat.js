import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';
import { Header } from 'semantic-ui-react';
import $ from 'jquery';
import 'signalr';   
import ow from '../images/overwatch.jpg'

import api from '../services/api'
import Auth from '../Components/auth';
import {HubConnection} from '@aspnet/signalr'
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox, Statistic, Table, Loader, Dropdown , List} from 'semantic-ui-react';


import gostosao from '../images/chris.jpg';
import { request } from 'http';

export default class Chat extends Component {

    state = {
        Nickname: '',
        GamerLogado: {},
        message: '',
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
        var connection = $.hubConnection('http://localhost:44338/');
        var proxy = connection.createHubProxy('[chatHub]');
        this.state.hubConnection = new HubConnection('http://localhost:44338/signalr');

        this.state.hubConnection.connectToAPI(this.state.GamerLogado.ID);
        
        

    }

    setNickname(myData) {
        this.setState({Nickname: myData.Nickname})
    }

    // Faz uma requisição de match para outro gamer
    
    render() {
               
        return (
            <List relaxed animated divided verticalAlign='middle' style={{'marginLeft': '5%'}}>
                {this.state.message.map((message) => 
                    <List.Item >
                        <List.Content>
                            <List.Header>{message.Nickname}</List.Header>
                            <List.Description>{message.Content }</List.Description>
                        </List.Content>
                    </List.Item>
                )}
            </List>
                
                
            
        )
    }
}
