import React, { Component } from 'react';
import {Redirect} from 'react-router-dom';

import './curriculo.css';
import { Image, Segment, Grid, Loader, Statistic, Icon, Menu, Container, Rating, Header } from 'semantic-ui-react'
import Headera from '../Components/headera';
import api from '../services/api';
import gostosao from '../images/chris.jpg';
import OWCard from '../Components/OWCard';
import getPlayer from '../Components/getPlayer';

export default class Curriculo extends Component {

    state = {
        Nickname: '',
        Gamer: {},
        OverwatchInfo: {},
        toLogin: false,
        ConnectionsLength: 0,
        loaded: false,
        stars: 0,
    }

    async componentDidMount() {
        
        let Player = await getPlayer();

        if(!Player) {
            this.setState({toLogin: true});
            return;
        }
        this.setState(
        {
            Gamer: Player,
            Nickname: Player.Nickname
        });
        
        let CurriculoData = await api.get('api/Curriculo?GamerID=' + Player.ID);
        if(CurriculoData !== null) {
            this.setState({ConnectionsLength: CurriculoData.data.ConnectionsLenght, stars: CurriculoData.data.Stars});
        }

        this.setState({loaded: true});
    }

    OpenConnections(){
        this.setState({NewConnections: false});
    }
    
    render(){
        if(this.state.toLogin === true){
            return <Redirect to="/"></Redirect>
        }
        if(!this.state.loaded) {
            return <Loader active/>
        }
        const { Gamer } = this.state;
        return (
            <div>
                <div>
                    <Headera gamer = {Gamer}/>
                </div>
                <div>
                    <Segment style={{'marginLeft': '2%', 'marginRight': '2%', 'marginTop': '2%'}}>
                        <Grid columns={2} style={{'marginBottom': '5%'}}>
                            <Grid.Column width={2}>
                                <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '3%'}}>
                                    <Menu vertical icon="labeled">
                                        <Menu.Item style={{cursor:'pointer'}} onClick={() => {this.props.history.push('/match')}}>
                                            <Icon aria-hidden="true" name="plug" >
                                            </Icon> Conectar
                                        </Menu.Item>
                                        <Menu.Item style={{cursor:'pointer'}}>
                                            <Icon name="space shuttle"/> Criar Salas
                                        </Menu.Item>
                                        <Menu.Item style={{cursor:'pointer'}}>
                                            <Icon name="users"/> Meus Grupos
                                        </Menu.Item>
                                        <Menu.Item style={{cursor:'pointer'}} onClick={() => {this.props.history.push('/registergame')}}>
                                            <Icon name="gamepad"/> Meus jogos
                                        </Menu.Item>
                                        <Menu.Item style={{cursor:'pointer'}} onClick={() => {this.props.history.push('/bio')}}>
                                            <Icon name="edit"/> Editar Currículo
                                        </Menu.Item>
                                    </Menu>
                                </div>
                            </Grid.Column>
                            <Grid.Column width={10}>
                                <div className="main-content">
                                <div className="ui container">
                                        <h2 className="ui icon center aligned header">
                                            <Image circular aria-hidden="true" 
                                                src={this.state.Gamer.ImagePath === null ? 
                                                     gostosao : this.state.Gamer.ImagePath}></Image>
                                            <div className="content">{this.state.Gamer.Nickname}</div>
                                            <div id='realname' className="content">{this.state.Gamer.Name} {this.state.Gamer.LastName}</div>
                                        </h2>
                                        <div className='space'>
                                        <Rating rating={this.state.stars} maxRating={5} disabled></Rating>
                                        </div>
                                        <div className='space'> 
                                        <div className="ui message">
                                            <div className="header">Meu grito de guerra</div>
                                            <ul className="list">
                                                <Container textAlign='center'><i>{this.state.Gamer.Slogan}</i></Container>
                                                <br/>
                                            </ul>
                                            <div className="header">História que cantam</div>
                                            <ul className="list">
                                                <Container textAlign="center"><i>{this.state.Gamer.Bio}</i></Container>
                                                <br/>
                                            </ul>
                                        </div>
                                        </div>
                                </div>
                                    <OWCard {...this.state.Gamer}></OWCard>
                                </div>
                            </Grid.Column>
                            <Grid.Column width={4} id='coluna-3' style={{display: 'flex', flexDirection: 'column', alignItems: 'center', alignContent: 'center'}}>
                                <Header content='Sua participação em Half-pugg'></Header>
                                <Statistic.Group horizontal>
                                    <Statistic value={this.state.ConnectionsLength} label='conexões'></Statistic>
                                    <Statistic value={this.state.stars} label='média da nota'></Statistic>
                                </Statistic.Group>
                            </Grid.Column>
                        </Grid>
                    </Segment>
                </div>
            </div>
        )
    }
}