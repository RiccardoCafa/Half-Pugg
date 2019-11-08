import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox, Statistic, Table } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg';

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
        Games: [],
        OWFilter: false,
        OWF: {
            "role": Number,
            "level": [],
            "rating": [],
            "damage": [],
            "healing": [],
            "elimination": [],
            "competitve": false,
        },
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
        
        // Pega os dados de match do jogador
        if(myData !== undefined && myData.data !== null) {
            const MatchData = await api.get('api/GamersMatch', { headers: { "token-jwt": jwt }});
            if(MatchData.data != null){
                this.setState({GamerMatch: MatchData.data});
            }
    
            const requestedMatch = await api.get('api/RequestedMatchesLoggedGamer',
                { headers: { "token-jwt": jwt }});
            if(requestedMatch.data !== null) {
                this.setState({RequestedMatches: requestedMatch.data});
                this.setState({NumberOfRequests: requestedMatch.data.length});
            }
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

    // Remove um gamer da lista de sugestões de match
    desconnectMatch = (matcher) => {
        console.log(matcher);
        var array = [...this.state.GamerMatch];
        console.log(this.state.GamerMatch);
        var index = array.indexOf(matcher);
        if(index !== -1) {
            console.log('removendo');
            array.splice(index, 1);
            this.setState({GamerMatch: array});
        }
    }

    // Seta o filtro para a busca
    openGamersByFilter = () => {
        console.log(this.state.OWF);
        api.get('api/GetOwMatchFilter?PlayerID=' + this.state.GamerLogado.ID, {
            "role": [this.state.OWF.role],
            "level": [this.state.OWF.level[0], this.state.OWF.level[1]],
            "rating": [this.state.OWF.rating[0], this.state.OWF.rating[1]],
            "damage":[this.state.OWF.damage[0], this.state.OWF.damage[1]],
            "elimination": [this.state.OWF.elimination[0], this.state.OWF.elimination[1]],
            "competitve": false
        })
        .then( res => this.setState({GamerMatch: res.data})).catch(err => console.log(err.message));
    }
    
    // Abre as requisições de match
    openRequests = () => {
        this.setState({NewConnections: true})
    }

    // Abre a tela de novas conexões que podem ser feitas
    openConnections = () => {
        this.setState({NewConnections: false});
    }

    // Atualiza uma requisição de match, podendo ser aceita ou não
    FazMatch = async (deuMatch, gamerMatch) => {
        this.setState({isMatching: true});
        try {
            await api.put('api/RequestedMatches/1', {
                "ID": 1,
                "IdPlayer1": gamerMatch.ID,
                "IdPlayer2": this.state.GamerLogado.ID,
                "Status": "M",
                "IdFilters": 1,
            });
    
            // await api.post('api/Matches', {
            //     "IdPlayer1": gamerMatch.ID,
            //     "IdPlayer2": this.state.GamerLogado.ID,
            //     "Status": deuMatch,
            //     "Weight": 0,
            // });
    
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

    // Filtros do Overwatch
    openOWFiltro = () => this.setState({OWFilter: true});
    
    setRole = (role) => {
        this.setState( prevOWF => ({
            OWF: {
                ...prevOWF.OWF,
                "role": role,
            }
        }))
    }

    setLevel = (level, ind) => {
        let owf = {...this.state.OWF}
        owf.level[ind] = level;
        this.setState({
            OWF: owf,
        })
    }

    setDamage= (val, ind) =>{
        let owf = {...this.state.OWF};
        owf.damage[ind] = val;
        this.setState({
            OWF: owf,
        })
    }

    setHealing = (val, ind) => {
        let owf = {...this.state.OWF};
        owf.healing[ind] = val;
        this.setState({
            OWF: owf,
        })
    }

    setElimination= (val, ind) => {
        let owf = {...this.state.OWF};
        owf.elimination[ind] = val;
        this.setState({
            OWF: owf,
        });
    }

    setRating = (val, ind) => {
        let owf = {...this.state.OWF};
        owf.rating[ind] = val;
        this.setState({
            OWF: owf,
        })
    }


    render() {
        if(this.state.toLogin === true) {
            return <Redirect to="/"></Redirect>
        }
        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera HeaderGamer = { this.state.GamerLogado }/>
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
                    <Segment>
                        <Grid columns={2} celled='internally' stackable>
                            <Grid.Column width={12}>
                            {this.state.NewConnections === false ?
                            <Card.Group>
                                {this.state.GamerMatch.map((matcher) => 
                                    <Card key={matcher.playerFound.ID} >
                                        <Card.Content>
                                            <Image
                                                floated='right'
                                                size='mini'
                                                src={(matcher.playerFound.ImagePath === "" || matcher.playerFound.ImagePath === null) 
                                                    ? gostosao : matcher.playerFound.ImagePath}
                                                />
                                            <Card.Header>{matcher.playerFound.Nickname}</Card.Header>
                                            <Card.Meta>Sugestão de {matcher.PlayerRecName}</Card.Meta>
                                            <Card.Description><b>Moto de vida</b> <br></br>
                                                                {matcher.playerFound.Slogan === null ?
                                                                "Esse cara não possui..." : 
                                                                matcher.playerFound.Slogan}</Card.Description>
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
                                                    onClick={() => this.desconnectMatch(matcher)}
                                                    content='Not Interested!'
                                                    />
                                            </div>
                                        </Card.Content>
                                        <Card.Content extra>
                                            <OpenCurriculum matcher={matcher.playerFound}></OpenCurriculum>
                                        </Card.Content>
                                    </Card>
                                )}
                            </Card.Group>
                            :
                            <Card.Group>
                                {this.state.RequestedMatches.length === 0 ? 
                                    <Statistic.Group>
                                        <Statistic
                                        value = "Oh :( você não possui convites de conexão..."
                                        label = "Experimente conectar-se com mais gamers para que seja encontrado!"
                                        text size='mini'
                                        id="sem-conexao-texto"></Statistic>
                                    </Statistic.Group>
                                :
                                <div>
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
                                </div>
                                }
                                </Card.Group>
                            }
                            </Grid.Column>
                            {this.state.NewConnections === false ?
                            <Grid.Column width={3}>
                                Filtro
                                <Checkbox label='Filtrar por Overwatch' onChange={this.openOWFiltro}/>
                                {this.state.OWFilter === true ?
                                <div>
                                    <Table celled>
                                        <Table.Header>
                                            <Table.Row>
                                            <Table.HeaderCell>Filtro</Table.HeaderCell>
                                            <Table.HeaderCell>Min/Max</Table.HeaderCell>
                                            </Table.Row>
                                        </Table.Header>
                                        <Table.Body>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Papel
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input  placeholder='role' 
                                                        value={this.state.OWF.role === undefined ? this.state.OWF.role.toString(2) : undefined} 
                                                        onChange={e => this.setRole(e.target.value)}
                                                        size='mini'>
                                                        </Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Level
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='min level' value={this.state.OWF.level[0]} 
                                                        onChange={e => this.setLevel(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='max level' value={this.state.OWF.level[1]} 
                                                        onChange={e => this.setLevel(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>

                                            <Table.Row>
                                                <Table.Cell>
                                                    Rating
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='min rating' value={this.state.OWF.rating[0]} 
                                                        onChange={e => this.setRating(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='max rating' value={this.state.OWF.rating[1]} 
                                                        onChange={e => this.setRating(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>

                                            <Table.Row>
                                                <Table.Cell>
                                                    Damage
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='min damage' value={this.state.OWF.damage[0]} 
                                                        onChange={e => this.setDamage(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='max damage' value={this.state.OWF.damage[1]} 
                                                        onChange={e => this.setDamage(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            
                                            <Table.Row>
                                                <Table.Cell>
                                                    Healing
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='min healing' value={this.state.OWF.healing[0]} 
                                                        onChange={e => this.setHealing(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='max healing' value={this.state.OWF.healing[1]} 
                                                        onChange={e => this.setHealing(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            
                                            <Table.Row>
                                                <Table.Cell>
                                                    Elimination
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='min elimination' value={this.state.OWF.elimination[0]} 
                                                        onChange={e => this.setElimination(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='max elimination' value={this.state.OWF.elimination[1]} 
                                                        onChange={e => this.setElimination(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                        </Table.Body>
                                    </Table>
                                    <Button onClick={this.openGamersByFilter}>Filtrar</Button>
                                </div>
                                :
                                <div/>}
                            </Grid.Column>
                            :<div/>}
                        </Grid>
                    </Segment>
                    
                </div>
            </div>  
        )
    }
}