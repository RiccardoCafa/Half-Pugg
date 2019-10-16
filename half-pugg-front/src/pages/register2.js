import React, { useState } from 'react';
import { Link } from 'react-router-dom';

import './register2.css';
import pugg from '../images/Logo_Pugg.png'; 

export default function({history}) {

    const [ descricao, setDescricao ] = useState(''); 

    function handleSubmit(e) {
        e.preventDefault();

        console.log('descricao: ' + descricao);
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
                            placeholder = "Insira uma descrição:"
                            value = {descricao}
                            onChange = { e => setDescricao(e.target.value)} 
                            maxLength = {200}
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