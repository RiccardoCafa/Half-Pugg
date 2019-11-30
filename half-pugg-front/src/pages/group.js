import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';
import { Header } from 'semantic-ui-react';

import ow from '../images/overwatch.jpg'

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox, Statistic, Table, Loader, Dropdown } from 'semantic-ui-react';


import gostosao from '../images/chris.jpg';
import { request } from 'http';

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

    connectGroup = (matcher) => {
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

    render() {
        if(this.state.toLogin === true) {
            return <Redirect to="/"></Redirect>
        }
        if(this.state.goToMatch) {
            return <Redirect to='/match'></Redirect>
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
                    <Segment>
                        <Header as='h2'>
                        <Icon name='user' />
                        <Header.Content>Players</Header.Content>
                        </Header>
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
                                            <OpenCurriculum {...matcher.playerFound}></OpenCurriculum>
                                        </Card.Content>
                                    </Card>
                                )}
                            </Card.Group>
                            :
                            <Card.Group>
                                {this.state.RequestedMatches.length === 0 ? 
                                <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
                                    <Statistic.Group>
                                        <Statistic
                                        value = "Oh :( você não possui convites de conexão..."
                                        label = "Experimente conectar-se com mais gamers para que seja encontrado!"
                                        text size='mini'
                                        id="sem-conexao-texto"></Statistic>
                                    </Statistic.Group>
                                </div>
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
                                Filtro <br/><br/>
                                <Grid>
                                    <Grid.Column width={15}>
                                        <Dropdown 
                                            icon='filter' 
                                            placeholder='Tipo de Procura'
                                            floating labeled
                                            options={this.state.tiposProcura}
                                            onChange={this.applyFiltroSearch}
                                        ></Dropdown>
                                    </Grid.Column>
                                    <Grid.Column width={8}>
                                        <Button>Pesquisar!</Button>
                                    </Grid.Column>
                                </Grid>
                                <br></br>
                                <Checkbox label='Filtrar por Overwatch' onChange={this.openOWFiltro}/>
                                {this.state.OWFilter === true ?
                                <div>
                                    <Table celled>
                                        <Table.Header>
                                            <Table.Row>
                                            <Table.HeaderCell>Filtro</Table.HeaderCell>
                                            <Table.HeaderCell>Valores</Table.HeaderCell>
                                            </Table.Row>
                                        </Table.Header>
                                        <Table.Body>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Papel
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input  placeholder='role' 
                                                        value={this.state.OWF.role >= 0 ? this.state.OWF.role.toString(2) : ''} 
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
                                                    <Dropdown options={this.state.owfType} placeholder='Tipo de filtro' 
                                                    onChange={this.setFilterType} />
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.level[0] >= 0 ? this.state.OWF.level[0].toString(2) : ''} 
                                                        onChange={e => this.setLevel(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.level[1] >= 0 ? this.state.OWF.level[1].toString(2) : ''} 
                                                        onChange={e => this.setLevel(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>

                                            <Table.Row>
                                                <Table.Cell>
                                                    Rating
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.rating[0] >= 0 ? this.state.OWF.rating[0].toString(2) : ''} 
                                                        onChange={e => this.setRating(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.rating[1] >= 0 ? this.state.OWF.rating[1].toString(2):''} 
                                                        onChange={e => this.setRating(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>

                                            <Table.Row>
                                                <Table.Cell>
                                                    Damage
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.damage[0] >= 0 ? this.state.OWF.damage[0].toString(2):''} 
                                                        onChange={e => this.setDamage(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.damage[1] >= 0 ? this.state.OWF.damage[1].toString(2):''} 
                                                        onChange={e => this.setDamage(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            
                                            <Table.Row>
                                                <Table.Cell>
                                                    Healing
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.healing[0] >= 0 ? this.state.OWF.healing[0].toString(2):''} 
                                                        onChange={e => this.setHealing(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.healing[1] >= 0 ? this.state.OWF.healing[1].toString(2):''} 
                                                        onChange={e => this.setHealing(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            
                                            <Table.Row>
                                                <Table.Cell>
                                                    Elimination
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.elimination[0]  >= 0 ? this.state.OWF.elimination[0].toString(2):''} 
                                                        onChange={e => this.setElimination(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.elimination[1] >= 0 ? this.state.OWF.elimination[1].toString(2):''} 
                                                        onChange={e => this.setElimination(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                        </Table.Body>
                                    </Table>
                                    {this.state.loadingFilter ?
                                    <Loader active inline></Loader>
                                    : <Button onClick={this.openGamersByFilter}>Filtrar</Button>
                                    }
                                </div>
                                :
                                <div/>}
                            </Grid.Column>
                            
                            :<div/>}
                        </Grid>
                        
                    </Segment>
                    <Segment>
                        <Header as='h2'>
                        <Icon name='users' />
                        <Header.Content>Groups</Header.Content>
                        </Header>
                         <Grid columns={2} celled='internally' stackable>
                            
                            {this.state.NumberOfGroups != null ?
                            <Card.Group>
                                {this.state.Groups.map((group) => 
                                    <Card key={group.ID} >
                                        <Card.Content>
                                            <Image
                                                floated='right'
                                                size='mini'
                                                src={ow}
                                                />
                                            <Card.Header>{group.name}</Card.Header>
                                            
                                            
                                        </Card.Content>
                                        <Card.Content extra>
                                            <div className='ui two buttons'>
                                                <Button 
                                                    id='btn-acpden' 
                                                    basic color='green' 
                                                    onClick={() => this.connectMatch(null)}
                                                    content='Connect!'
                                                    />
                                                <Button 
                                                    id='btn-acpden' 
                                                    basic color='red' 
                                                    onClick={() => this.desconnectMatch(null)}
                                                    content='Not Interested!'
                                                    />
                                            </div>
                                        </Card.Content>
                                        
                                    </Card>
                                )}
                            </Card.Group>
                            :
                            <Card.Group>
                                {this.state.RequestedGroup.length === 0 ? 
                                    <Statistic.Group>
                                        <Statistic
                                        value = "Oh :( você não possui convites para grupos..."
                                        text size='mini'
                                        id="sem-conexao-texto"></Statistic>
                                    </Statistic.Group>
                                :
                                <div>
                                {this.state.RequestedGroup.map((requests) => 
                                    <Card key = {requests.ID} >
                                        <Card.Content>
                                            <Image
                                                floated='right' 
                                                size='mini'
                                                circular
                                                src={(requests.ImagePath === "" || requests.ImagePath === null) ? gostosao : requests.ImagePath}
                                                />
                                            
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
                                        
                                    </Card>
                                )}
                                </div>
                                }
                                </Card.Group>
                            }

                            
                            
                            
                        </Grid>           
                    </Segment>
                </div>
            </div>  
        )
    }
}
