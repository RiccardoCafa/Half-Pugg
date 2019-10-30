import React, { Component } from 'react';
import {Redirect} from 'react-router-dom';

import './curriculo.css';
import { Image, Segment, Grid} from 'semantic-ui-react'
import Header from '../Components/header';
import api from '../services/api';

export default class Curriculo extends Component {

    state = {
        Nickname: '',
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        toLogin: false,
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
        this.setState({Nickname: myData.Nickname})
        
    }

    OpenConnections(){
        this.setState({NewConnections: false});
    }
    
    render(){
        if(this.state.toLogin === true){
            return <Redirect to="/"></Redirect>
        }
        return (
            <div>
                <div>
                    <Header dataFP = {this.state.Nickname}/>
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
                                            <i aria-hidden="true" className="users circular icon"></i>
                                            <div className="content">Fulaninho</div>
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
                                            <div className="header">Características</div>
                                            <ul className="list">
                                                <li className="content">Like to talk</li>
                                                <li className="content">Easy-going</li>
                                                <li className="content">Living the best life</li>
                                            </ul>
                                        </div>
                                        </div>
                                </div>
                                <div>
                                    <div className="ui segment dimmable">
                                        <h3 className="ui header">Overwatch</h3>
                                        <div className="ui small ui small images images">
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image> 
                                        </div>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png" className="ui medium image"></Image>
                                    </div>
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