import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';
import { Header } from 'semantic-ui-react';

import ow from '../images/overwatch.jpg'

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox, Statistic, Table, Loader, Dropdown, Divider } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg';
import { request } from 'http';
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
        OWFilter: false,
        OWF: {
            "role": -2,
            "level": [-2, -2],
            "rating": [-2, -2],
            "damage": [-2,-2],
            "healing": [-2,-2],
            "elimination": [-2,-2],
            "competitve": false,
        },
        owfType: [
            {
                key: 1,
                text: 'Maior que',
                value: 1
            },
            {
                key: 2,
                text: 'Menor que',
                value: 2
            },
            {
                key: 3,
                text: 'Entre dois valores',
                value: 3,
            },
            {
                key: 4,
                text: 'Igual que',
                value: 4,
            }
        ], 
        loadingFilter: false,
        tiposProcura: [
            {
                key: 1,
                text: 'Pessoas Aleatórias',
                value: 'Pessoas Aleatórias'
            },
            {
                key: 2,
                text: 'Jogo',
                value: 'Jogo',
            },
            {
                key: 3,
                text: 'Conhecidos',
                value: 'Conhecidos',
            },
            {
                key: 4,
                text: 'Interesse',
                value: 'Interesse'
            }
        ],
        tipoSelecionado: Number,
        gameSelected: -1,
        gamesToSelect: [
            {
                key: 1,
                text: 'Overwatch',
                value: 'Overwatch'
            }
        ],
        searchDelegate: Function,
        typeSearch: 2,
        NicknameToFind: '',
        
        loaded: false,
    }

    async componentDidMount() {
        let gamer = getPlayer();
        if(!gamer){
            this.setState({toLogin: true});
            return;
        }

        this.setState({GamerLogado: myData})
        this.setNickname(myData);
        
        // Pega os dados de match do jogador
        if(myData !== undefined && myData.data !== null) {
            const MatchData = await api.get('api/GamersMatch', { headers: { "token-jwt": jwt }});
            if(MatchData.data != null){
                this.setState({GamerMatch: MatchData.data});
            }
    
            const requestedGroup = await api.get('api/RequestedGroup',
               { headers: { "token-jwt": jwt }});
            if(requestedGroup.data !== null) {
               this.setState({RequestedGroups: requestedGroup.data});
               this.setState({NumberOfRequests: requestedGroup.data.length});
            }
            else{
                const requestedGroup = await api.get('api/Groups');
                if(requestedGroup.data !== null) {
                    this.setState({RequestedGroups: requestedGroup.data});
                    this.setState({NumberOfRequests: requestedGroup.data.length});
                }
            }

            
            this.getPlayersToRec();
            this.getRequestedMatches(jwt);
            this.getRequestedGroup();
        }

                
    }

    setNickname(myData) {
        this.setState({Nickname: myData.Nickname})
    }

    // Faz uma requisição de match para outro gamer
    connectMatch = (matcher) => {
        console.log(matcher);
        const response = api.post('api/RequestedMatches', {
            "IdPlayer1": this.state.GamerLogado.ID,
            "IdPlayer2": matcher.playerFound.ID,
            "Status": "A",
            "IdFilters": 1
        })
        .catch(error => 
            console.log(error)
        );
        
        if(response !== null) {
            var array = [...this.state.GamerMatch];
            var index = array.indexOf(matcher);
            if(index !== -1) {
                array.splice(index, 1);
                this.setState({GamerMatch: array});
            }
        }
    }

    getRequestedGroup = async() =>{
        const requestedGroup = await api.get('api/Groups');
        //if(requestedGroup !==undefined && requestedGroup !== null) {
        //console.log(requestedGroup.data);
        this.setState({RequestedGroups: requestedGroup.data});
        this.setState({NumberOfRequests: requestedGroup.data.length});
        console.log('entrou');
        //}
        console.log('entrou');
        // else{
        //     const requestedGroup = await api.get('api/Groups');
        //     if(requestedGroup.data !== null) {
        //         this.setState({RequestedGroups: requestedGroup.data});
        //         this.setState({NumberOfRequests: requestedGroup.data.length});
        //     }
        // }
    }

    
    connectGroup = (matcher) => {
        console.log(matcher);
        const response = api.post('api/RequestedMatches', {
            "IdPlayer1": this.state.GamerLogado.ID,
            "IdPlayer2": matcher.playerFound.ID,
            "Status": "A",
            "IdFilters": 1 } )
        this.setState({
            GamerLogado: gamer,
            Nickname: gamer.Nickname,
            loaded: true
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