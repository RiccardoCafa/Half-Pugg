import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';
import { Header } from 'semantic-ui-react';

import ow from '../images/overwatch.jpg'

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox, Statistic, Table, Loader, Dropdown , List} from 'semantic-ui-react';


import gostosao from '../images/chris.jpg';
import { request } from 'http';

export default class Chat extends Component {

    state = {
        Nickname: '',
        GamerMatch: [],
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
        //console.log(this.props)
        this.setState({GamerLogado: myData})
        this.setNickname(myData);

        const hubConnection = new HubConnection('http://localhost:/chat');

        this.setState({ hubConnection, Nickname });
        this.setState({ hubConnection, Nickname }, () => {
            this.state.hubConnection
              .start()
              .then(() => console.log('Connection started!'))
              .catch(err => console.log('Error while establishing connection :('));
      
            this.state.hubConnection.on('sendToAll', (Nickname, receivedMessage) => {
              const text = `${Nickname}: ${receivedMessage}`;
              const messages = this.state.messages.concat([text]);
              this.setState({ messages });
            });
        });        

    }

    setNickname(myData) {
        this.setState({Nickname: myData.Nickname})
    }

    // Faz uma requisição de match para outro gamer
    
    render() {
               
        return (
            <div></div>
                
                
            
        )
    }
}
