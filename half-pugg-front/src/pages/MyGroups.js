import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';

import './MyGroups.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox, Statistic, Table, Loader, Dropdown } from 'semantic-ui-react';

export default class MyGroups extends Component {

    state = {
        Nickname: '',
        GamerGroup: [],
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        Group: [],
        loaded: false,
    }

    async componentDidMount() {
        // Pega o usuário a partir do token
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
        
        
    }
    
    render() {
        
        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera gamer = {this.state.GamerLogado}/>
                </div>  
                <Segment>
                    {this.state.Group.length === 0 ?
                    <div>
                        <Statistic.Group>
                            <Statistic
                            value = "Ops! Parece que você não participa de nenhum grupo..."
                            label = "Crie um grupo e e chame seus amigos"
                            text
                            id="sem-conexao-texto"></Statistic>
                        </Statistic.Group>
                        <Button id="sem-conexao-button" label="Quero me conectar!" basic icon='users' onClick={this.goToBio}></Button>
                    </div>:<div/>}
                </Segment>
            </div>    
        );
    }
}