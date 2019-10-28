import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/header';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Menu, Icon, Label } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg'

export default class Match extends Component {

    state = {
        Nickname: '',
        GamerMatch: [],
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        RequestedMatches: [],
        NumberOfRequests: 0,
        NewConnections: false,
        toLogin: false,
    }

    async componentDidMount() {

        const jwt = localStorage.getItem("jwt");
        //console.log(jwt);
        let myData;
        if(jwt){
            console.log(jwt);
            await api.get('api/Login', { headers: { "token-jwt": jwt }}).then(res => 
                myData = res.data
                //console.log(res.data)
            ).catch(error => this.setState({toLogin: true}))
        }else {
            this.setState({toLogin: true});
        }
        this.setState(
        {
            GamerLogado: myData
        })
        console.log(myData);

        /*const jwt = localStorage.getItem("jwt");
        if(!jwt) {
            this.setState({toLogin: true});
            return;
        }
        const myData = await api.get('api/Login', {
            headers: { "token-jwt": {jwt} }
        }).then(res =>{
            this.setState({GamerLogado: myData.data});
            this.setState ({Nickname: myData.data.Nickname});
            console.log(this.state.Nickname);
        }).catch(error => {
            this.setState({toLogin: true})
        });*/
        if(myData !== undefined && myData.data !== null) {
            const MatchData = await api.get('api/GamersMatch');
            if(MatchData.data != null){
                console.log(MatchData.data);
                this.setState({GamerMatch: MatchData.data});
            }
    
            const requestedMatch = await api.get('api/RequestedMatchesLoggedGamer');
            if(requestedMatch.data !== null) {
                this.setState({RequestedMatches: requestedMatch.data});
                console.log(requestedMatch.data);
                this.setState({NumberOfRequests: requestedMatch.data.length});
            }
        }
    }

    connectMatch(matcher) {

        console.log(matcher);
        const response = api.post('api/RequestedMatches', {
            "IdPlayer": this.state.GamerLogado.ID,
            "IdPlayer2": matcher.ID,
            "Status": "A",
        })
        .catch(function(error){
            console.log(error);
        });
        
        if(response !== null) {
            var array = [...this.state.GamerMatch];
            var index = array.indexOf(matcher);
            if(index !== -1) {
                array.splice(index, 1);
                this.setState({GamerMatch: array});
            }
        }
    }

    openRequests() {
        console.log(this.state.RequestedMatches.data);
        this.setState({NewConnections: true})
    }

    openConnections() {
        this.setState({NewConnections: false});
    }

    async FazMatch(deuMatch, gamerMatch) {
        try {
            const reqResponse = await api.put('api/RequestedMatches/1', {
                "IdPlayer": gamerMatch.ID,
                "IdPlayer2": this.state.GamerLogado.ID,
                "Status": "F"
            });
    
            const response = await api.post('api/Matches', {
                "IdPlayer1": gamerMatch.ID,
                "IdPlayer2": this.state.GamerLogado.ID,
                "Status": deuMatch,
                "Weight": 0,
            });
    
            var array = [...this.state.RequestedMatches];
            var index = array.indexOf(gamerMatch);
            if(index !== -1) {
                array.splice(index, 1);
                this.setState({RequestedMatches: array});
                this.setState({NumberOfRequests: this.state.RequestedMatches.length})
            }
        } catch(error) {
            console.log(error);
        }
    }

    render() {
        if(this.state.toLogin === true) {
            return <Redirect to="/"></Redirect>
        }
        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera dataFP = {this.state.Nickname}/>
                </div>  
                <div className='submenu'>
                    <Menu compact>
                        <Menu.Item onClick={e => this.openConnections()}>
                            <Icon name='users'/> New Connections
                        </Menu.Item>
                        <Menu.Item onClick={e => this.openRequests()}>
                            <Icon name='mail'/> Pending Requests
                            <Label color='teal' floating>{this.state.NumberOfRequests}</Label>
                        </Menu.Item>
                    </Menu>
                </div>
                <div className='connections'>
                    {this.state.NewConnections === false ?
                    <Card.Group>
                        {this.state.GamerMatch.map((matcher) => 
                            <Card key={matcher.ID} >
                                <Card.Content>
                                    <Image
                                        floated='right'
                                        size='mini'
                                        src={(matcher.ImagePath === "" || matcher.ImagePath === null) ? gostosao : matcher.ImagePath}
                                        />
                                    <Card.Header>{matcher.Nickname}</Card.Header>
                                    <Card.Meta>Sugestão de xXNoobMaster69Xx</Card.Meta>
                                    <Card.Description>Principais Jogos: LOL, Overwatch e WoW. Recomendação de 80%</Card.Description>
                                </Card.Content>
                                <Card.Content extra>
                                    <div className='ui two buttons'>
                                        <Button 
                                            id='btn-acpden' 
                                            basic color='green' 
                                            onClick={() => this.connectMatch(matcher)}
                                            content='Connect!'
                                            />
                                        <Button 
                                            id='btn-acpden' 
                                            basic color='red' 
                                            onClick={this.desconnectMatch}
                                            content='Not Interested!'
                                            />
                                    </div>
                                </Card.Content>
                                <Card.Content extra>
                                    <OpenCurriculum matcher={matcher}></OpenCurriculum>
                                </Card.Content>
                            </Card>
                        )}
                    </Card.Group>
                    :
                    <Card.Group>
                        {this.state.RequestedMatches.map((requests) => 
                            <Card key = {requests.ID} >
                                <Card.Content>
                                    <Image
                                        floated='right'
                                        size='mini'
                                        circular
                                        src={(requests.ImagePath === "" || requests.ImagePath === null) ? gostosao : requests.ImagePath}
                                        />
                                    <Card.Header>{requests.Nickname}</Card.Header>
                                    <Card.Meta>Sugestão de xXNoobMaster69Xx</Card.Meta>
                                    <Card.Description>Principais Jogos: LOL, Overwatch e WoW. Recomendação de 80%</Card.Description>
                                </Card.Content>
                                <Card.Content extra>
                                    <div className='ui two buttons'>
                                        <Button id='btn-acpden' basic color='green' onClick={() => this.FazMatch(true, requests)}>
                                            Accept!
                                        </Button>
                                        <Button id='btn-acpden' basic color='red' onClick={() => this.FazMatch(false, requests)}>
                                            Deny!
                                        </Button>
                                    </div>
                                </Card.Content>
                                <Card.Content extra>
                                    <OpenCurriculum matcher={requests}></OpenCurriculum>
                                </Card.Content>
                            </Card>
                        )}
                    </Card.Group>
                    }
                </div>
            </div>  
        )
    }
}