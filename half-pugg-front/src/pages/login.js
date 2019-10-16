import React, { useState } from 'react';

import './login.css';
import { Button, Input } from 'semantic-ui-react';
import api from '../services/api';

export default function({history}) {

    const [ email, setEmail ] = useState(''); 
    const [ senha, setSenha ] = useState('');
    const [ cor, setCor ] = useState('white');

    async function handleSubmit(e) {
        e.preventDefault();

        const response = await api.post('api/Login', {
            "Email": email,
            "HashPassword": senha
        }).catch(function(error){
            console.log(error);
            switch(error.response.status){
                case 404:
                    setCor('red');
                break;
                case 200:
                    history.push('/match');
                break;
                default:
                    console.log('algo deu errado');
                break;
            }
        });
    }

    function handleCadastro(e){
        e.preventDefault();

        console.log('fui clicado');
        history.push('/register');
    }

    function handleBranchConnect(e){
        e.preventDefault();

        console.log('Branch Connect');
    }

    return (
        <div className = "login-container">
            <form >
                <h1>Half Pugg</h1>
                <div>
                    <h4>E-MAIL</h4>
                    <input style={{color: {cor}}}
                        placeholder= "Seu email"
                        value = {email}
                        onChange = { e => setEmail(e.target.value)} 
                        maxLength = {25}
                    />
                </div>
                <div>
                    <h4>SENHA</h4>
                    <input 
                        style = {{color: {cor}}}
                        placeholder= "Suas palavras secretas ( ͡~ ͜ʖ ͡°)"
                        value = {senha}
                        onChange = { e => setSenha(e.target.value)}
                        type = {"password"}
                    />
                </div>
                <Button.Group id="botoes">
                    <Button color='green' onClick={e => handleSubmit(e)} >
                        Login
                    </Button>
                    <Button.Or />
                    <Button color='youtube' onClick={e => handleBranchConnect(e)} >
                        Branch Connect!
                    </Button>
                </Button.Group>
            </form>
            <form className="cadastro" >
                <span>
                    <label className="cadastro-label" onClick={handleCadastro}>Cadastra-se agora e vire um profissional!</label>
                </span>
            </form>
        </div>
    );
}