import React, { useState } from 'react';

import './login.css';
import { Button, Popup } from 'semantic-ui-react';
import api from '../services/api';

export default function({history}) {

    const [ email, setEmail ] = useState(''); 
    const [ senha, setSenha ] = useState('');
    const [ showPopUp, setShowPopUp ] = useState(Boolean);

    async function handleSubmit(e) {
        e.preventDefault();

        const response = await api.post('api/Login', {
            "Email": email,
            "HashPassword": senha
        }).catch(function(error){
            switch(error.response.status){
                case 404:
                    setShowPopUp(true);
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
            history.push('/match');
        }
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
                    {{showPopUp} ? 
                        <Popup 
                            content='Email ou Senha errada!'
                            pinned 
                            on='click'
                            open={showPopUp}
                            trigger={<h4 className="emailLabel">E-MAIL</h4>}
                        />
                    : <h4 className="emailLabel">E-MAIL</h4> }
                    <input
                        placeholder= "Seu email"
                        value = {email}
                        onChange = { e => setEmail(e.target.value)} 
                        maxLength = {25}
                    />
                </div>
                <div>
                    <h4>SENHA</h4>
                    <input 
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