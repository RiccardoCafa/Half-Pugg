import React, { Component } from 'react';
import {Redirect} from 'react-router-dom';

import './curriculo.css';
import { Image, Segment, Grid, Loader, Statistic} from 'semantic-ui-react'
import Header from '../Components/headera';
import api from '../services/api';
import gostosao from '../images/chris.jpg';
import OWCard from '../Components/OWCard';

export default class Curriculo extends Component {

    state = {
        Nickname: '',
        Gamer: {},
        OverwatchInfo: {},
        toLogin: false,
        loadedOW: false,
        ConnectionsLength: 0,
        loaded: false,
    }

    async componentDidMount() {
        
        const jwt = localStorage.getItem("jwt");
        let stop = false;
        //console.log(jwt);
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
        this.setState(
        {
            Gamer: myData
        })
        this.setState({Nickname: myData.Nickname})
        
        let CurriculoData = await api.get('api/Curriculo?GamerID=' + myData.ID);
        if(CurriculoData !== null) {
            if(CurriculoData.data.OverwatchInfo !== undefined) {
                this.setState({
                    OverwatchInfo: CurriculoData.data.OverwatchInfo
                    , loadedOW: true
                });
            }
            this.setState({ConnectionsLength: CurriculoData.data.ConnectionsLenght});
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
        const { Gamer } = this.state;
        return (
            <div>
                <div>
                    {this.state.loaded ?
                    <Header gamer = {Gamer}/>
                    : null}
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
                                                src={this.state.Gamer.ImagePath === null ? 
                                                     gostosao : this.state.Gamer.ImagePath}></Image>
                                            <div className="content">{this.state.Gamer.Nickname}</div>
                                            <div className="content">{this.state.Gamer.Name} {this.state.Gamer.LastName}</div>
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
                                                <li className="content">{this.state.Gamer.Slogan}</li>
                                            </ul>
                                            <div className="header">História que cantam</div>
                                            <ul className="list">
                                                <li className="content">{this.state.Gamer.Bio}</li>
                                            </ul>
                                        </div>
                                        </div>
                                </div>
                                <div>
                                    {this.state.OverwatchInfo !== null && this.state.OverwatchInfo.profile !== undefined ?
                                    <OWCard {...this.state.Gamer}></OWCard>
                                    : <Loader/>}
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
                            <Grid.Column width={1} id='coluna-3'>
                                <Statistic value={this.state.ConnectionsLength} label='conexoes'></Statistic>
                            </Grid.Column>
                        </Grid>
                        
                    </Segment>
                </div>
            </div>
        )
    }
}