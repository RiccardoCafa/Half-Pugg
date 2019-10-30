import React, { Component, createRef } from 'react';

import './curriculo.css';
import {Icon, Image, Menu, Segment, Sidebar, Redirect} from 'semantic-ui-react'
import Header from '../Components/header';

export default class Curriculo extends Component {

    state = {
        Nickname: ' ',
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        toLogin: false,
    }

    async componentDidMount(){
    }

    OpenConnections(){
        this.setState({NewConnections: false});
    }

    
    render(){
            if(this.state.toLogin == true){
            //    return <Redirect to="/"></Redirect>
            }
            return (
                <Segment>
                    <Header />
                    <div className="menu-container">
                        <div className="left-content">
                            <div class="ui vertical labeled icon menu">
                                <a class="item">
                                <i aria-hidden="true" class="gamepad icon"></i>
                                Conectar
                                </a>
                                <a class="item">
                                <i aria-hidden="true" class="video camera icon"></i>
                                Criar salas
                                </a>
                                <a class="item">
                                <i aria-hidden="true" class="video play icon"></i>
                                Meus grupos
                                </a>
                                <a class="item">
                                <i aria-hidden="true" class="gamepad icon"></i>
                                Meus jogos
                                </a>
                                <a class="item">
                                <i aria-hidden="true" class="video camera icon"></i>
                                Perfil
                                </a>
                                <a class="item">
                                <i aria-hidden="true" class="video play icon"></i>
                                Ranking
                                </a>
                            </div>
                        </div>
                        <div className="main-content">
                            <div class="ui container">
                                    <h2 class="ui icon center aligned header">
                                        <i aria-hidden="true" class="users circular icon"></i>
                                        <div class="content">Fulaninho</div>
                                    </h2>
                                    <div class='space'>
                                    <div class="ui star rating" role="radiogroup" tabindex="-1">
                                        <i
                                            tabindex="0"
                                            aria-checked="false"
                                            aria-posinset="1"
                                            aria-setsize="4"
                                            class="active icon"
                                            role="radio"
                                        ></i>
                                        <i
                                            tabindex="0"
                                            aria-checked="false"
                                            aria-posinset="2"
                                            aria-setsize="4"
                                            class="active icon"
                                            role="radio"
                                        ></i>
                                        <i
                                            tabindex="0"
                                            aria-checked="true"
                                            aria-posinset="3"
                                            aria-setsize="4"
                                            class="active icon"
                                            role="radio"
                                        ></i>
                                        <i
                                            tabindex="0"
                                            aria-checked="false"
                                            aria-posinset="4"
                                            aria-setsize="4"
                                            class="icon"
                                            role="radio"
                                        ></i>
                                    </div>
                                    </div>
                                    <div class='space'> 
                                    <div class="ui message">
                                        <div class="header">Características</div>
                                        <ul class="list">
                                            <li class="content">Like to talk</li>
                                            <li class="content">Easy-going</li>
                                            <li class="content">Living the best life</li>
                                        </ul>
                                    </div>
                                    </div>
                            </div>
                            <div>
                                <div class="ui segment dimmable">
                                    <h3 class="ui header">Overwatch</h3>
                                    <div class="ui small ui small images images">
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image> 
                                    </div>
                                    <Image src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png" class="ui medium image"></Image>
                                </div>
                                <div class="ui segment dimmable">
                                    <h3 class="ui header">League of Legends</h3>
                                    <div class="ui small ui small images images">
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image> 
                                    </div>
                                    <Image src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png" class="ui medium image"></Image>
                                </div>
                            </div>
                        </div>
                    </div>
                </Segment>
            );
        }
}