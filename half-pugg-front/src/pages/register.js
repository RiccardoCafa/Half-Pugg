import React, { useState } from 'react';
import { Link } from 'react-router-dom';

import './register.css';
import pugg from '../images/Logo_Pugg.png';

export default function({history}) {

    const [ username, setUsername ] = useState(''); 
    const [ senha, setSenha ] = useState('');
    const [ confirmSenha, setConfirmSenha ] = useState('');
    const [ dataNascimento, setDataNascimento] = useState('');
    const [ email, setEmail ] = useState('');
    const [ firstName, setFirstName ] = useState('');
    const [ lastName, setLastName ] = useState('');

    function handleSubmit(e) {
        e.preventDefault();

        console.log('usuario: ' + username);
    }

    return (
        <div className = "register-container">    
            <div className= "register-title">
                <Link to = "/">
                    <img src={pugg} width="100" height="100" alt="pugg logo"/>
                </Link>
                <h1>Half Pugg</h1>
            </div>
            <div className = "register-inputs">
            <form onSubmit={handleSubmit}>
                <ul>
                    <li>
                        <input 
                            placeholder = "Seu nome heróico (ง ͠° ͟ل͜ ͡°)ง"
                            value = {username}
                            onChange = { e => setUsername(e.target.value)} 
                            maxLength = {25}
                        />
                    </li>
                    <li>
                        <input 
                            placeholder = "Seu email fabuloso ( ✧≖ ͜ʖ≖)"
                            value = {email}
                            onChange = { e => setEmail(e.target.value)}
                            type= {"email"}
                        />
                    </li>
                    <li>
                        <input 
                            placeholder = "Declare palavras secretas ( ͡~ ͜ʖ ͡°)"
                            value = {senha}
                            onChange = { e => setSenha(e.target.value)}
                            type = {"password"}
                            maxLength = {20}
                        />
                    </li>
                    <li >  
                        <input 
                            placeholder = "Confirme as palavras secretas ಠ_ಠ"
                            value = {confirmSenha}
                            onChange = { e => setConfirmSenha(e.target.value)}
                            type = {"password"}
                        />
                    </li>
                    <li>
                        <input 
                            placeholder = "Primeiro Nome"
                            value = {firstName}
                            onChange = { e => setFirstName(e.target.value)} 
                            maxLength = {30}
                        />
                    </li>
                    <li>
                        <input 
                            placeholder = "Último Nome"
                            value = {lastName}
                            pattern="[A-Za-z]"
                            maxLength = {30}
                            onChange = {e => setLastName(e.target.value)}
                        />
                    </li>
                    <li>
                        <input 
                            placeholder = "Dia de spawn no mundo"
                            value = {dataNascimento}
                            onChange = { e => setDataNascimento(e.target.value)}
                            type = {"date"}
                        />
                    </li>
                </ul>
            </form>
            </div>
            <button type="submit" >
                    Sign-up
                </button>
        </div>
    );
}