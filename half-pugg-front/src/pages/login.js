import React, { useState } from 'react';

import './login.css';
import pugg from '../images/Logo_Pugg.png';

export default function({history}) {

    const [ username, setUsername ] = useState(''); 
    const [ senha, setSenha ] = useState('');

    function handleSubmit(e) {
        e.preventDefault();

        console.log('usuario: ' + username);
    }

    function handleCadastro(e){
        e.preventDefault();

        console.log('fui clicado');
        history.push('/register');
    }

    return (
        <div className = "login-container">
            <form onSubmit={handleSubmit}>
                <img src={pugg} width="100" height="100" alt="pugg logo"/>
                <h1>Half Pugg</h1>
                <input 
                    placeholder = "Seu nome heróico (ง ͠° ͟ل͜ ͡°)ง"
                    value = {username}
                    onChange = { e => setUsername(e.target.value)} 
                    maxLength = {25}
                />
                <input 
                    placeholder = "Suas palavras secretas ( ͡~ ͜ʖ ͡°)"
                    value = {senha}
                    onChange = { e => setSenha(e.target.value)}
                    type = {"password"}
                />
                <button type="submit" >
                    Login
                </button>
            </form>
            <form className="cadastro" >
                <span>
                    <label className="cadastro-label" onClick={handleCadastro}>Cadastra-se agora e vire um profissional!</label>
                </span>
            </form>
        </div>
    );
}