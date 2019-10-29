import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Image, TextArea, Form, Segment } from 'semantic-ui-react';
import gostosao from '../images/chris.jpg';
import api from '../services/api';

import './register2.css';

export default class Register2 extends Component {

    state = {
        descricao: '',
        Gamer: {},
        toLogin: false,
    }

    componentDidMount() {
        const jwt = localStorage.getItem("jwt");
        if(jwt){
            api.get('api/Login', { headers: { "token-jwt": jwt } })
                .then(res =>
                this.setState({Gamer: res})).catch(err => {
                    this.setState({toLogin: true});
                });
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();
        

    }

    render() {
        if(this.state.toLogin) {
            return <Redirect to ='/'></Redirect>
        }
        return (
            <div className = "login-container">
                <h1 id='title'>Half Pugg</h1>
                <div id="biography">
                <Segment>
                    <Image circular src={gostosao} size="small" centered></Image>
                    <h4>DESCRIÇÃO</h4>
                    <Form>
                        <TextArea placeholder="Tell us more" value={this.state.descricao} rows="3" 
                                  onChange={e => this.setState({descricao: e.target.value})}>
                        </TextArea>
                    </Form>
                    <h4>IMAGEM</h4>
                    <Button fluid icon='upload'></Button>
                    <Button.Group id={"botoes"}>
                        <Button color='green' onClick={e => this.handleSubmit(e)}>
                            Confirm
                        </Button>
                    </Button.Group>
                </Segment>
                </div>
            </div>
        );
    }
}