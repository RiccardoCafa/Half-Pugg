import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/header';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg'
import { stringify } from 'querystring';

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
        cadastroIncompleto: false,
        isMatching: false,
        toBio: false,
        hideCom: false,
        Games: [],
        OWFilter: false,
        OWF: {
            "role": 0,
            "level": [0],
            "rating": [0],
            "damage": [0],
            "healing": [0],
            "elimination": [0],
            "competitve": false,
        },
    }

    async componentDidMount() {

        const jwt = localStorage.getItem("jwt");
        let stop = false;
        //console.log(jwt);
        let myData;
        if(jwt){
            console.log(jwt);
            await api.get('api/Login', { headers: { "token-jwt": jwt }}).then(res => 
                myData = res.data
                //console.log(res.data)
            ).catch(error => stop = true)
        } else {
            stop = true;
        }

        if(stop) {
            this.setState({toLogin: true});
            return;
        }

        this.setState(
        {
            GamerLogado: myData
        })
        console.log(myData);
        this.setNickname(myData);
        
        if(myData !== undefined && myData.data !== null) {
            const MatchData = await api.get('api/GamersMatch',
                { headers: { "token-jwt": jwt }});
            if(MatchData.data != null){
                console.log(MatchData.data);
                this.setState({GamerMatch: MatchData.data});
            }
    
            const requestedMatch = await api.get('api/RequestedMatchesLoggedGamer',
                { headers: { "token-jwt": jwt }});
            if(requestedMatch.data !== null) {
                this.setState({RequestedMatches: requestedMatch.data});
                console.log(requestedMatch.data);
                this.setState({NumberOfRequests: requestedMatch.data.length});
            }
        }
    }

    setNickname(myData) {
        
        this.setState({Nickname: myData.Nickname})
    }

    connectMatch = (matcher) => {
        console.log(this.state);
        const response = api.post('api/RequestedMatches', {
            "IdPlayer": this.state.GamerLogado.ID,
            "IdPlayer2": matcher.ID,
            "Status": "A",
            "IdFilters": 1
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

    openGamersByFilter = () => {
        console.log(this.state.OWF);
        api.get('api/GetOwMatchFilter?PlayerID=2' ,this.state.OWF)
        .then( res => this.setState({GamerMatch: res.data})).catch(err => console.log(err.message));
    }

    openRequests = () => {
        console.log(this.state.RequestedMatches.data);
        this.setState({NewConnections: true})
    }

    openConnections = () => {
        console.log("sla");
        this.setState({NewConnections: false});
    }

    FazMatch = async (deuMatch, gamerMatch) => {
        this.setState({isMatching: true});
        try {
            await api.put('api/RequestedMatches/1', {
                "IdPlayer": gamerMatch.ID,
                "IdPlayer2": this.state.GamerLogado.ID,
                "Status": "F",
                "IdFilters": 1,
            });
    
            await api.post('api/Matches', {
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
                this.setState({NumberOfRequests: this.state.RequestedMatches.length});
                this.setState({isMatching: true});
            }
        } catch(error) {
            console.log(error);
        }
    }

    goToBio() {
        this.setState({toBio: true})
    }

    openOWFiltro = () => {
        this.setState({OWFilter: true});
    }

    setRole = (role) => {
        var owf = {...this.state.OWF}
        owf.Role = role;
        this.setState({OWF: owf});
    }

    setLevel = (level) => {
        var owf = {...this.state.OWF}
        owf.level[0] = parseInt(level);
        this.setState({OWF: owf})
    }

    setDamage= (val) =>{
        var owf = {...this.state.OWF}
        owf.damage[0] = val
        this.setState({OWF: owf})
    }

    setHealing = (val) => {
        var owf = {...this.state.OWF}
        owf.healing[0] = val
        this.setState({OWF: owf})
    }

    setElimination= (val) => {
        var owf = {...this.state.OWF}
        owf.elimination[0] = val
        this.setState({OWF: owf})
    }


    render() {
        if(this.state.toLogin === true) {
            return <Redirect to="/"></Redirect>
        }
        if(this.state.toBio === true) {
            return <Redirect to="/bio"></Redirect>
        }
        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera dataFP = {this.state.Nickname}/>
                </div>  
                <div className='submenu'>
                    <Menu compact>
                        <Menu.Item onClick={this.openConnections}>
                            <Icon name='users'/> New Connections
                        </Menu.Item>
                        <Menu.Item onClick={() => this.openRequests()}>
                            <Icon name='mail'/> Pending Requests
                            <Label color='teal' floating>{this.state.NumberOfRequests}</Label>
                        </Menu.Item>
                    </Menu>
                </div>
                {this.state.hideCom === false ?
                <Segment id="incomplete-cadastro">
                    <p>Parece que tem informações faltando no seu perfil, atualize para que outros jogadores consigam saber mais de você!</p>
                    <Button primary onClick={() => this.goToBio}>Atualizar!</Button>
                    <Button id='botao-complete-cadastro' labelPosition='right' floated='right' onClick={() => this.setState({hideCom: true})}>X</Button>
                </Segment>
                : <div/>}
                <div className='connections'>
                    <Segment>
                        <Grid columns={2} celled='internally' stackable>
                            <Grid.Column width={12}>
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
                            </Grid.Column>
                            <Grid.Column width={3}>
                                Filtro
                                <Checkbox label='Filtrar por Overwatch' onChange={this.openOWFiltro}/>
                                {this.state.OWFilter === true ?
                                <div>
                                    <Input placeholder='role' value={this.state.OWF.role} 
                                            onChange={e => this.setRole(e.target.value)}></Input>
                                    <Input placeholder='level' value={this.state.OWF.level[0]} 
                                            onChange={e => this.setLevel(e.target.value)}></Input>
                                    <Input placeholder='rating' value={this.state.OWF.rating[0]} 
                                            onChange={e => this.setRating(e.target.value)}></Input>
                                    <Input placeholder='damage' value={this.state.OWF.damage[0]} 
                                            onChange={e => this.setDamage(e.target.value)}></Input>
                                    <Input placeholder='healing' value={this.state.OWF.healing[0]} 
                                            onChange={e => this.setHealing(e.target.value)}></Input>
                                    <Input placeholder='elimination' value={this.state.OWF.elimination[0]} 
                                            onChange={e => this.setElimination(e.target.value)}></Input>
                                    <Button onClick={this.openGamersByFilter}>Filtrar</Button>
                                </div>
                                :
                                <div/>}
                            </Grid.Column>
                        </Grid>
                    </Segment>
                    
                </div>
            </div>  
        )
    }
}