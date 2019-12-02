import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';
import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import { Menu, Icon, Label, Loader } from 'semantic-ui-react';

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
        RequestedGroup: [],
        NumberOfRequests: 0,
        NumberOfGroups: 0,
        NewConnections: false,
        toLogin: false,
        cadastroIncompleto: false,
        isMatching: false,
        Games: [],
        loaded: false,
    }

    async componentDidMount() {
        let gamer = await getPlayer();
        if(!gamer){
            this.setState({toLogin: true});
            return;
        }
        this.getRequestedMatches(localStorage.getItem('jwt'));
        this.setState({
            GamerLogado: gamer,
            Nickname: gamer.Nickname,
            loaded: true,
        })
    }

    getRequestedMatches = async(jwt) => {
        const requestedMatch = await api.get('api/RequestedMatchesLoggedGamer',
        { headers: { "token-jwt": jwt }});
        if(requestedMatch.data !== null) {
            this.setState({
                RequestedMatches: requestedMatch.data,
                NumberOfRequests: requestedMatch.data.length,
            });
        }
    }

    updateRequestes = (requests) => {
        this.setState({NumberOfRequests: requests.length, RequestedMatches: requests});
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
                        <MatchList GamerLogado={this.state.GamerLogado}></MatchList>
                    :
                        <ConnectionList GamerLogado={this.state.GamerLogado} requestedMatch={this.state.RequestedMatches} updateRequestes={this.updateRequestes}></ConnectionList>
                    }
                </div>
            </div>  
        )
    }
}