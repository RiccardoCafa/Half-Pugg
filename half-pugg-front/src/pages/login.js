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

    function handleBranchConnect(e){
        e.preventDefault();

        console.log('Branch Connect');
    }
//<img src={pugg} width="100" height="100" alt="pugg logo"/>
    return (
        <div className = "login-container">
            <form >
                
                <h1>Half Pugg</h1>
                <div>
                    <h4>E-MAIL</h4>
                    <input 
                        placeholder aria-label= "Seu nome"
                        value = {username}
                        onChange = { e => setUsername(e.target.value)} 
                        maxLength = {25}
                    />
                </div>
                <div>
                    <h4>SENHA</h4>
                    <input 
                        placeholder aria-label = "Suas palavras secretas ( ͡~ ͜ʖ ͡°)"
                        value = {senha}
                        onChange = { e => setSenha(e.target.value)}
                        type = {"password"}
                    />
                </div>
                <form onSubmit={handleSubmit}>
                    <button type="submit" >
                        Login
                    </button>
                </form>
                <form className="branch-button" >
                    <button type="submit" >
                        Branch Connect!
                    </button>
                </form>
            </form>
            <form className="cadastro" onSubmit={handleBranchConnect} >
                <span>
                    <label className="cadastro-label" onClick={handleCadastro}>Cadastra-se agora e vire um profissional!</label>
                </span>
            </form>
        </div>
    );
}