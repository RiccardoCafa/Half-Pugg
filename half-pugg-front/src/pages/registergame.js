import React, { useState, Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Comment, Form, TextArea, Image } from 'semantic-ui-react';

import './registergame.css';

export default class Register2 extends Component {

    state = {
        slogan: '',
        descricao: '',
        MyImage: '',
        Gamer: {},
        toLogin: false,
    }

//    handleSubmit(e) {
       
//    }

    render() {
        if(this.state.toLogin) {
            return <Redirect to ='/'></Redirect>
        }
        return (
            <div className = "register-container">
                <form> 
                    <h1 id='title'>Half Pugg</h1>
                    <div>
                        <h2>Choose a game</h2>
                        <div class="ui segment dimmable">
                                <h3 class="ui header">Overwatch</h3>
                                <div class="ui small ui small images images">
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image> 
                                </div>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png" class="ui medium image"></Image>
                        </div>
                        <div id = "botoes">
                            <Button.Group id="botoes">
                                <Button color='green'>
                                    Adicionar
                                </Button>
                            </Button.Group>
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
                        <div id = "botoes">
                            <Button.Group id="botoes">
                                <Button color='green'>
                                    Adicionar
                                </Button>
                            </Button.Group>
                        </div>
                        <div class="ui segment dimmable">
                                <h3 class="ui header">Couter-Strike</h3>
                                <div class="ui small ui small images images">
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image> 
                                </div>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png" class="ui medium image"></Image>
                        </div>
                        <div id = "botoes">
                            <Button.Group id="botoes">
                                <Button color='green'>
                                    Adicionar
                                </Button>
                            </Button.Group>
                        </div>
                    </div>
                </form>
            </div>
        );
    }
}