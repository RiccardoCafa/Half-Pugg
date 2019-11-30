import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox, Statistic, Table, Loader, Dropdown, Divider } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg';
import MatchList from '../Components/MatchList';
import getPlayer from '../Components/getPlayer';
import ConnectionList from '../Components/ConnectionList';

export default class Match extends Component {

    state = {
        Nickname: '',
        GamerMatch: [],
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        RequestedMatches: [],
        RequestedGroups: [],
        Groups :[] ,
        NumberOfRequests: 0,
        NewConnections: false,
        toLogin: false,
        cadastroIncompleto: false,
        isMatching: false,
        Games: [],
        loaded: false,
    }

    async componentDidMount() {
        let gamer = getPlayer();
        if(!gamer){
            this.setState({toLogin: true});
            return;
        }
        this.setState({
            GamerLogado: gamer,
            Nickname: gamer.Nickname,
            loaded: true,
        })
    }

    // Abre as requisições de match
    openRequests = () => {
        this.setState({NewConnections: true})
    }

    // Abre a tela de novas conexões que podem ser feitas
    openConnections = () => {
        this.setState({NewConnections: false});
    }

    render() {
        if(this.state.toLogin === true) {
            return <Redirect to="/"></Redirect>
        }
        if(!this.state.loaded) {
            return <Loader active></Loader>
        }
        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera gamer = {this.state.GamerLogado }/>
                </div>  
                <div className='submenu'>
                    <Menu compact>
                        <Menu.Item onClick={this.openConnections}>
                            <Icon name='users'/> New Connections
                        </Menu.Item>
                        <Menu.Item onClick={this.openRequests}>
                            <Icon name='mail'/> Pending Requests
                            <Label color='teal' floating>{this.state.NumberOfRequests}</Label>
                        </Menu.Item>
                    </Menu>
                </div>
                <div className='connections'>
                    {!this.state.NewConnections ?
                        <MatchList></MatchList>
                    :
                        <ConnectionList></ConnectionList>
                    }
                </div>
            </div>  
        )
    }
}