import React, { useState } from 'react';
import { Button, Comment, Form, TextArea } from 'semantic-ui-react'

import './register2.css';
export default  function({history}) {

    const [ descricao, setDescricao ] = useState(''); 

    function handleSubmit(e) {
        e.preventDefault();

        console.log('descricao: ' + descricao);
    }

    return (
        <div className = "login-container">
            <form> 
                <h1>Half Pugg</h1>
                <div>
                     <h4>DESCRIÇÃO</h4>
                     <form class="ui form"><textarea placeholder="Tell us more" rows="3"></textarea></form>
                    <h4>IMAGEM</h4>
                    <img src="/images/wireframe/square-image.png" class="ui medium circular image" />
                 </div>
                 <div id = {"botoes"}>
                    <Button.Group id={"botoes"}>
                        <Button color='green' onClick={e => handleSubmit(e)} >
                            Login
                        </Button>
                    </Button.Group>
                </div>
            </form>
        </div>
    );
}