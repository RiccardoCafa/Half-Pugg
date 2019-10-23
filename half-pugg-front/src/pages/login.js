import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

import './login.css';
import { Button, Popup } from 'semantic-ui-react';
import api from '../services/api';

export default class Login extends Component {

    state = {
        email: '',
        senha: '',
        showPopUp: false,
        goToMatch: false,
        goToRegister: false,
    }

    async componentDidMount() {
        const response = await api.get('api/Login');

        if(response != null) {
            this.setState({goToMatch: true});
        }
    }

    async handleSubmit(e) {
        e.preventDefault();

        const response = await api.post('api/Login', {
            "Email": this.state.email,
            "HashPassword": this.state.senha
        }).catch(function(error){
            console.log(error);
            switch(error.response.status){
                case 404:
                    this.setState({showPopUp: true});
                    console.log("404");
                break;
                default:
                    console.log('algo deu errado');
                break;
            }
        });
        if(response != null) {
            console.log('deu bom hein');
            console.log(response);
            console.log(response.data);
            this.setState({goToMatch: true});
        }
    }

    handleCadastro(e){
        e.preventDefault();

        console.log('fui clicado');
        this.setState({goToRegister: true});
        //history.push('/register');
    }

    handleBranchConnect(e){
        e.preventDefault();

        console.log('Branch Connect');
    }

    render() {
        if(this.state.goToMatch) {
            return <Redirect to='/match'></Redirect>
        }
        if(this.state.goToRegister) {
            return <Redirect to='/register'></Redirect>
        }
        return (
            <div className = "login-container">
                <form >
                    <h1>Half Pugg</h1>
                    <div>
                        {this.state.showPopUp ? 
                            <Popup 
                                content='Email ou Senha errada!'
                                pinned 
                                on='click'
                                open={this.state.showPopUp}
                                trigger={<h4 className="emailLabel">E-MAIL</h4>}
                            />
                        : <h4 className="emailLabel">E-MAIL</h4> }
                        <input
                            placeholder= "Seu email"
                            value = {this.state.email}
                            onChange = { e => this.setState({email: e.target.value})} 
                            maxLength = {25}
                        />
                    </div>
                    <div>
                        <h4>SENHA</h4>
                        <input 
                            placeholder= "Suas palavras secretas ( ͡~ ͜ʖ ͡°)"
                            value = {this.state.senha}
                            onChange = { e => this.setState({senha: e.target.value})}
                            type = {"password"}
                        />
                    </div>
                    <Button.Group id="botoes">
                        <Button color='green' onClick={e => this.handleSubmit(e)} >
                            Login
                        </Button>
                        <Button.Or />
                        <Button color='youtube' onClick={e => this.handleBranchConnect(e)} >
                            Branch Connect!
                        </Button>
                    </Button.Group>
                </form>
                <form className="cadastro" >
                    <span>
                        <label className="cadastro-label" onClick={this.handleCadastro}>Cadastra-se agora e vire um profissional!</label>
                    </span>
                </form>
            </div>
        );
    }
    
}