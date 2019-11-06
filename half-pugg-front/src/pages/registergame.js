import React, { useState } from 'react';
import { Button, Comment, Form, TextArea } from 'semantic-ui-react'

import './registergame.css';
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
                     <h4>Choose a game</h4>
                     

                 </div>
                 <div id = {"botoes"}>
                    <Button.Group id={"botoes"}>
                        <Button color='green' onClick={e => handleSubmit(e)} >
                            Próximo
                        </Button>
                    </Button.Group>
                </div>
            </form>
        </div>
    );
}