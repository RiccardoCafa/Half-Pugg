import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import getPlayer from '../Components/getPlayer';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import Classification from '../Components/classification';
import { Card, Button, Segment, Statistic, Loader, Header, Icon, Divider } from 'semantic-ui-react';
import ConnectionCardList from '../Components/ConnectionUI'
import UserContentCard from '../Components/UserContentCard';
import GroupsInvite from '../Components/GroupsInvite';

export default class MyConnections extends Component {

    state = {
        Nickname: '',
        GamerMatch: [],
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        Matches: [],
        DeniedMatches: [],
        toMatch: false,
        loaded: false,
    }

    componentDidMount = async () => {

        let gamer = await getPlayer();

        if(!gamer) {
            this.setState({toLogin: true});
            return;
        }

        this.setState(
        {
            GamerLogado: gamer,
            Nickname: gamer.Nickname

        })
        
        if(gamer !== undefined) {
            const MatchData = await api.get('api/Matches/' + gamer.ID);
            //{ headers: { "token-jwt": jwt }}
            if(MatchData.data){
                this.setState({Matches: MatchData.data});
            }

            const RejectedData = await api.get('api/Matches/Rejected?playerID='+gamer.ID);
            if(RejectedData) {
                this.setState({DeniedMatches: RejectedData.data});
            }
        }
        this.setState({loaded: true});
    }

    atualizaMatch = async (match) => {
        const response = await api.put(`api/Matches/${match.ID}`, {
            "ID": match.ID,
            "IdPlayer1": match.IdPlayer1,
            "IdPlayer2": match.IdPlayer2,
            "Status": true,
            "Weight": match.Weight,
            "CreateAt": match.CreateAt,
            "AlteredAt": match.AlteredAt
        });
        if(response.status === 200) {
            alert('atualizado com sucesso');
            window.location.reload();
        
        }
    }

    componentWillMount() {

    }

    setNickname(myData) {
        
        this.setState({Nickname: myData.Nickname})
    }

    goToBio = () => {
        this.setState({toMatch: true});
    }

    render() {
        if(this.state.toLogin) {
            return <Redirect to='/'></Redirect>
        }
        if(this.state.toMatch === true) {
            return <Redirect to='/match'></Redirect>
        }
        if(!this.state.loaded) {
            return <Loader active></Loader>
        }

        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera gamer = {this.state.GamerLogado}/>
                </div>  
                <div >
                <Segment style={{'marginLeft': '2%', 'marginRight': '2%', 'marginTop': '2%'}}> 
                    <Header size='small' as='h2' style={{'marginLeft': '3%'}}>
                        <Icon name='users'></Icon>
                        <Header.Content>
                            Suas conexões!
                            <Header.Subheader>Você pode avaliar os usuários que você está conectado!</Header.Subheader>
                        </Header.Content>
                    </Header>
                    <Divider></Divider>
                    {this.state.Matches.length === 0 ?
                    <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
                        <Statistic.Group>
                            <Statistic
                            value = "Ops! Parece que você não possui conexões..."
                            label = "Experimente conectar-se com outros gamers!"
                            text
                            id="sem-conexao-texto"></Statistic>
                        </Statistic.Group>
                        <Button id="sem-conexao-button" label="Quero me conectar!" basic icon='users' onClick={this.goToBio}></Button>
                    </div>:<div/>}

                    <ConnectionCardList gamer={this.state.GamerLogado} Matches ={this.state.Matches}/> 
                    {/* { <Card.Group>
                        {this.state.Matches.map((matcher) => 
                            <Card key={matcher.matchPlayer.ID}>
                                <UserContentCard gamer={this.state.GamerLogado} matchPlayer={matcher.matchPlayer} isAvaliable={false}></UserContentCard>
                                <Card.Content extra>
                                    <GroupsInvite gamer={this.state.GamerLogado} playerToInvite={matcher.matchPlayer}></GroupsInvite>
                                    <OpenCurriculum {...matcher.matchPlayer}></OpenCurriculum>
                                    <Classification gamer={this.state.GamerLogado} gamerclassf={matcher.matchPlayer} classificacao={matcher.Classificacao}></Classification>
                                </Card.Content>
                            </Card>
                        )}
                    </Card.Group> } */}
                    </Segment>
                </div>
                <div >
                <Segment style={{'marginLeft': '2%', 'marginRight': '2%', 'marginTop': '2%'}}> 
                    <Header size='small' as='h2' style={{'marginLeft': '3%'}}>
                        <Icon name='users'></Icon>
                        <Header.Content>
                            Conexões que você humildemente recusou!
                            <Header.Subheader>Você pode dar outra chance para esses coitados <s>ou não</s>!</Header.Subheader>
                        </Header.Content>
                    </Header>
                    <Divider></Divider>
                    {this.state.DeniedMatches.length === 0 ?
                    <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center', marginBottom: '5%'}}>
                        <Statistic.Group>
                            <Statistic
                            value = "Você é um cara bem social!"
                            label = "Você não rejeitou ninguém, uau!"
                            text
                            id="sem-conexao-texto"></Statistic>
                        </Statistic.Group>
                    </div>:<div/>}
                    <Card.Group>
                        {this.state.DeniedMatches.map((matcher) => 
                            <Card key={matcher.match.ID}>
                                <UserContentCard gamer={this.state.GamerLogado} matchPlayer={matcher.rejected} isAvaliable={true}></UserContentCard>
                                <Card.Content extra>
                                    <Button basic fluid onClick={() => this.atualizaMatch(matcher.match)}>Aceitar Humildemente</Button>
                                </Card.Content>
                                <Card.Content extra>
                                    <OpenCurriculum {...matcher.rejected}></OpenCurriculum>
                                </Card.Content>
                            </Card>
                        )}
                    </Card.Group>
                    </Segment>
                </div>
            </div>  
        )
    }
}