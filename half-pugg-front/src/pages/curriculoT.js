import React, { Component } from 'react';
import {Redirect} from 'react-router-dom';

import './curriculo.css';
import { Image, Segment, Grid, Checkbox, List} from 'semantic-ui-react'
import Header from '../Components/headera';
import api from '../services/api';
import gostosao from '../images/chris.jpg';

export default class Curriculo extends Component {

    state = {
        Nickname: '',
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        OverwatchInfo: {},
        toLogin: false,
        compCareerCollapse: false,
        quickCareerCollapse: false,
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
        
        this.setState({Nickname: myData.Nickname})
        
        let OWData = await api.get('api/GetPlayersOwerwatch?PlayerID=' + myData.ID + '&Region=0');
        if(OWData !== null) {
            this.setState({OverwatchInfo: OWData.data});
        }
        console.log(OWData);
    }

    OpenConnections(){
        this.setState({NewConnections: false});
    }
    
    handleQuickCareerCollapse = (ligado) => this.setState({quickCareerCollapse: ligado});
    handleCareerCollapse = (ligado) => this.setState({compCareerCollapse: ligado});

    render(){
        if(this.state.toLogin === true){
            return <Redirect to="/"></Redirect>
        }
        const { compCareerCollapse } = this.state;
        const { quickCareerCollapse } = this.state;

        let owLevel = 1;
        if(this.state.OverwatchInfo.profile !== undefined){
            owLevel = this.state.OverwatchInfo.profile.endorsement * 100 + this.state.OverwatchInfo.profile.level;
        }

        return (
            <div>
                <div>
                    <Header HeaderGamer = {this.state.GamerLogado}/>
                </div>
                <div className="menu-container">
                    <Segment>
                        <Grid columns={2} relaxed='very' stackable>
                            <Grid.Column width={2}>
                                <div className="left-content">
                                    <div className="ui vertical labeled icon menu">
                                        <div className="item">
                                        <i aria-hidden="true" className="gamepad icon"></i>
                                        Conectar
                                        </div>
                                        <div className="item">
                                        <i aria-hidden="true" className="video camera icon"></i>
                                        Criar salas
                                        </div>
                                        <div className="item">
                                        <i aria-hidden="true" className="video play icon"></i>
                                        Meus grupos
                                        </div>
                                        <div className="item">
                                        <i aria-hidden="true" className="gamepad icon"></i>
                                        Meus jogos
                                        </div>
                                        <div className="item">
                                        <i aria-hidden="true" className="video camera icon"></i>
                                        Perfil
                                        </div>
                                        <div className="item">
                                        <i aria-hidden="true" className="video play icon"></i>
                                        Ranking
                                        </div>
                                    </div>
                                </div>
                            </Grid.Column>
                            <Grid.Column width={10}>
                                <div className="main-content">
                                <div className="ui container">
                                        <h2 className="ui icon center aligned header">
                                            <Image circular aria-hidden="true" 
                                                src={this.state.GamerLogado.ImagePath === null ? 
                                                     gostosao : this.state.GamerLogado.ImagePath}></Image>
                                            <div className="content">{this.state.GamerLogado.Nickname}</div>
                                            <div className="content">{this.state.GamerLogado.Name} {this.state.GamerLogado.LastName}</div>
                                        </h2>
                                        <div className='space'>
                                        <div className="ui star rating" role="radiogroup">
                                            <i
                                                aria-checked="false"
                                                aria-posinset="1"
                                                aria-setsize="4"
                                                className="active icon"
                                                role="radio"
                                            ></i>
                                            <i
                                                aria-checked="false"
                                                aria-posinset="2"
                                                aria-setsize="4"
                                                className="active icon"
                                                role="radio"
                                            ></i>
                                            <i
                                                aria-checked="true"
                                                aria-posinset="3"
                                                aria-setsize="4"
                                                className="active icon"
                                                role="radio"
                                            ></i>
                                            <i
                                                aria-checked="false"
                                                aria-posinset="4"
                                                aria-setsize="4"
                                                className="icon"
                                                role="radio"
                                            ></i>
                                        </div>
                                        </div>
                                        <div className='space'> 
                                        <div className="ui message">
                                            <div className="header">Meu grito de guerra</div>
                                            <ul className="list">
                                                <li className="content">{this.state.GamerLogado.Slogan}</li>
                                            </ul>
                                            <div className="header">História que cantam</div>
                                            <ul className="list">
                                                <li className="content">{this.state.GamerLogado.Bio}</li>
                                            </ul>
                                        </div>
                                        </div>
                                </div>
                                <div>
                                    {this.state.OverwatchInfo.profile !== undefined ?
                                    <div className="ui segment dimmable">
                                        <h3 className="ui header">Overwatch</h3>
                                        <div>
                                            <div className="header">Nome: {this.state.OverwatchInfo.profile.name}</div>
                                            <div className="header">level: {owLevel}</div>
                                            <div className="header">prestige: {this.state.OverwatchInfo.profile.prestige}</div>
                                            <div className="header">rating: {this.state.OverwatchInfo.profile.rating}</div>
                                            <div className="header">tank_rating: {this.state.OverwatchInfo.profile.tank_rating}</div>
                                            <div className="header">damage_rating: {this.state.OverwatchInfo.profile.damage_rating}</div>
                                            <div className="header">support_rating: {this.state.OverwatchInfo.profile.support_rating}</div>
                                        </div>
                                        {this.state.OverwatchInfo.quickCareer !== undefined ?
                                            <div><Checkbox
                                                label='quick career'
                                                onChange={() => this.handleQuickCareerCollapse(!quickCareerCollapse)}
                                            />
                                            {quickCareerCollapse === true ?
                                            <List>
                                                <List.Item>
                                                    <List.Content>All Damage done = {this.state.OverwatchInfo.quickCareer.allDamageDone}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Barrier Damage Done = {this.state.OverwatchInfo.quickCareer.barrierDamageDone}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Deaths = {this.state.OverwatchInfo.quickCareer.deaths}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Eliminations = {this.state.OverwatchInfo.quickCareer.eliminations}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Final Blows {this.state.OverwatchInfo.quickCareer.finalBlows}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Healing Done {this.state.OverwatchInfo.quickCareer.healingDone}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Hero Damage Done {this.state.OverwatchInfo.quickCareer.heroDamageDone}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Objective Kills {this.state.OverwatchInfo.quickCareer.objectiveKills}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Objective Time {this.state.OverwatchInfo.quickCareer.objectiveTime}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Solo Kills {this.state.OverwatchInfo.quickCareer.soloKills}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Time Spent On Fire {this.state.OverwatchInfo.quickCareer.timeSpentOnFire}</List.Content>
                                                </List.Item>
                                            </List> : <div/>}</div>
                                        : <div />}
                                        {this.state.OverwatchInfo.compCareer !== undefined ?
                                            <div><Checkbox
                                                label='career comp'
                                                onChange={() => this.handleCareerCollapse(!compCareerCollapse)}
                                            />
                                            {compCareerCollapse === true ?
                                            <List>
                                                <List.Item>
                                                    <List.Content>All Damage done = {this.state.OverwatchInfo.compCareer.allDamageDone}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Barrier Damage Done = {this.state.OverwatchInfo.compCareer.barrierDamageDone}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Deaths = {this.state.OverwatchInfo.compCareer.deaths}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Eliminations = {this.state.OverwatchInfo.compCareer.eliminations}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Final Blows {this.state.OverwatchInfo.compCareer.finalBlows}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Healing Done {this.state.OverwatchInfo.compCareer.healingDone}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Hero Damage Done {this.state.OverwatchInfo.compCareer.heroDamageDone}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Objective Kills {this.state.OverwatchInfo.compCareer.objectiveKills}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Objective Time {this.state.OverwatchInfo.compCareer.objectiveTime}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Solo Kills {this.state.OverwatchInfo.compCareer.soloKills}</List.Content>
                                                </List.Item>
                                                <List.Item>
                                                    <List.Content>Time Spent On Fire {this.state.OverwatchInfo.compCareer.timeSpentOnFire}</List.Content>
                                                </List.Item>
                                            </List> : <div/>}</div>
                                        : <div />}
                                    </div>
                                    : <div/>}
                                    <div className="ui segment dimmable">
                                        <h3 className="ui header">League of legends</h3>
                                        <div className="ui small ui small images images">
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image> 
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                        </div>
                                        <Image
                                            src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png"
                                            className="ui medium image"
                                        />
                                    </div>
                                    </div>
                                </div>
                            </Grid.Column>
                        </Grid>
                        
                    </Segment>
                </div>
            </div>
        )
    }
}