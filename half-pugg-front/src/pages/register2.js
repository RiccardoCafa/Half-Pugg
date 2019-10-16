import React, { useState } from 'react';
import { Button, Comment, Form, Image } from 'semantic-ui-react'

import './register2.css';
export default function({history}) {

    const [ descricao, setDescricao ] = useState(''); 

    function handleSubmit(e) {
        e.preventDefault();

        console.log('descricao: ' + descricao);
    }

    return (
        <div className = "register-container">    
            <div className= "register-title">
                <h1>Half Pugg</h1>
            </div>
            <div className = "register-inputs">
            <form>
                <ul>
                    <div>
                        <h4>DESCRIÇÃO</h4>
                        <Form descricao id={"descricao"}>
                            <Form.TextArea />
                        </Form>
                        <h4>IMAGEM</h4>

                    </div>
                </ul>
            </form>
            </div>
            <button type="submit" onClick={e => handleSubmit(e)} >
                    Sign-up
                </button>
        </div>
    );
}