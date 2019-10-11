import React, { useState } from 'react';
import { Link } from 'react-router-dom';

import './register.css';
import pugg from '../images/Logo_Pugg.png';
import api from '../services/api';

export default function({history}) {

    const [ username, setUsername ] = useState(''); 
    const [ senha, setSenha ] = useState('');
    const [ confirmSenha, setConfirmSenha ] = useState('');
    const [ dataNascimento, setDataNascimento] = useState('');
    const [ email, setEmail ] = useState('');
    const [ firstName, setFirstName ] = useState('');
    const [ lastName, setLastName ] = useState('');

    async function handleSubmit(e) {
        e.preventDefault();

        const response = await api.post('/', {
            "asd": username,
            
        });
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
                        <h4>Primeiro Nome</h4>
                        <input 
                            placeholder = ""
                            value = {firstName}
                            onChange = { e => setFirstName(e.target.value)} 
                            maxLength = {30}
                        />
                    </li>
                    <li>
                        <h4>Último Nome</h4>
                        <input 
                            placeholder = ""
                            value = {lastName}
                            maxLength = {30}
                            onChange = {e => setLastName(e.target.value)}
                        />
                    </li>
                    <li>
                        <h4>Seu nome heróico (ง ͠° ͟ل͜ ͡°)ง</h4>
                        <input 
                            placeholder = ""
                            value = {username}
                            onChange = { e => setUsername(e.target.value)} 
                            maxLength = {25}
                        />
                    </li>
                    <li>
                        <h4>Seu email fabuloso ( ✧≖ ͜ʖ≖)</h4>
                        <input 
                            placeholder = ""
                            value = {email}
                            onChange = { e => setEmail(e.target.value)}
                            type= {"email"}
                        />
                    </li>
                    <li>
                        <h4>Declare palavras secretas ( ͡~ ͜ʖ ͡°)</h4>
                        <input 
                            placeholder = ""
                            value = {senha}
                            onChange = { e => setSenha(e.target.value)}
                            type = {"password"}
                            maxLength = {20}
                        />
                    </li>
                    <li >  
                        <h4>Confirme as palavras secretas ಠ_ಠ</h4>
                        <input 
                            placeholder = ""
                            value = {confirmSenha}
                            onChange = { e => setConfirmSenha(e.target.value)}
                            type = {"password"}
                        />
                    </li>
                    <li>
                        <h4>Dia de spawn no mundo</h4>
                        <input 
                            placeholder = ""
                            value = {dataNascimento}
                            onChange = { e => setDataNascimento(e.target.value)}
                            type = {"date"}
                        />
                    </li>
                    <li>
                        <button type="submit" >
                            Cadastrar-se
                        </button>
                    </li>
                </ul>
            </form>
            </div>
            
        </div>
    );
}