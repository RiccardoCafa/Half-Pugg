import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

import './login.css';
import {  Segment, Button, Popup, Form, Grid, Divider, Loader } from 'semantic-ui-react';
import api from '../services/api';

export default class Login extends Component {

    state = {
        email: '',
        senha: '',
        showPopUp: false,
        goToMatch: false,
        goToRegister: false,
        loading: false,
        closePop: false,
    }

    async componentDidMount() {
        try {
            const response = await api.get('api/Login');
    
            if(response != null) {
                this.setState({goToMatch: true});
            }

        } catch(error) 
        {
            console.log('nao tem conta logada');
        };
    }

    async handleSubmit(e) {
        e.preventDefault();
        this.setState({loading: true}); 

        await api.post('api/Login', { 
            "Email": this.state.email,
            "HashPassword": this.state.senha
        }).then(res =>{
            localStorage.setItem("jwt", res.data);
            this.setState({goToMatch: true});
        }).catch(error => {
            console.log(error);
            this.setState({loading: false});
            /*switch(error.response.status) {
                case 404:
                    this.setState({showPopUp: true});
                    console.log("404");
                break;
                default:
                    console.log('algo deu errado');
                break;
            }*/
        });
    }

    handleCadastro(e){
        console.log('fui clicado');
        this.setState({goToRegister: true});
    }

    handleBranchConnect(e){
        e.preventDefault();

        console.log('Branch Connect');
    }

    closePopUp = () => {
        this.setState({showPopUp: false});
    }

    render() {
        if(this.state.goToMatch) {
            return <Redirect to='/match'></Redirect>
        }
        if(this.state.goToRegister) {
            return <Redirect to='/register'></Redirect>
        }
        return (
            <div>
                <div className = "login-container-2">
                    {this.state.loading !== true ?
                    <Segment>
                    <Grid columns={2} relaxed='very' stackable>
                        <Grid.Column>
                            <Form >
                                <h1 id="title">Half Pugg</h1>
                                {this.state.showPopUp ? 
                                    <Popup onClose={this.closePopUp} closeOnTriggerClick
                                        content='Email ou Senha errada!'
                                        pinned 
                                        on='click'
                                        open={this.state.showPopUp}
                                        trigger={<h4 className="emailLabel">E-MAIL</h4>}
                                        />
                                        : <h4 className="emailLabel">E-MAIL</h4> }
                                <Form.Input
                                    id="input-login"
                                    placeholder= "Seu email"
                                    value = {this.state.email}
                                    onChange = { e => this.setState({email: e.target.value})} 
                                    maxLength = {25}
                                    />
                                
                                <h4>SENHA</h4>
                                <Form.Input 
                                    id="input-login"
                                    placeholder= "Suas palavras secretas ( ͡~ ͜ʖ ͡°)"
                                    value = {this.state.senha}
                                    onChange = { e => this.setState({senha: e.target.value})}
                                    type = {"password"}
                                    />
                                <Button.Group>
                                    <Button
                                        color='green' 
                                        content='Login' 
                                        onClick={e => this.handleSubmit(e)} primary>
                                    </Button>
                                    <Button.Or />
                                    <Button 
                                        color='red' 
                                        content='Branch Connect!' 
                                        onClick={e => this.handleBranchConnect(e)} secondary>
                                    </Button>
                                </Button.Group>
                            </Form>
                        </Grid.Column>
                        <Grid.Column verticalAlign='middle'>
                                <Button content='Cadastrar-se' onClick={e => this.handleCadastro()} icon='signup' size='big'></Button>
                        </Grid.Column>
                    </Grid>
                    <Divider vertical>Or</Divider>
                    </Segment>
                    :
                    <Loader active></Loader>}
                </div>
                <div>

                </div>
            </div>

        );
    }
    
}