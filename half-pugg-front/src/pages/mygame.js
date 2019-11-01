import React, { useState } from 'react';
import { Button, Comment, Form, TextArea,Image } from 'semantic-ui-react'

import './mygames.css';
export default  function({history}) {

    const [ descricao, setDescricao ] = useState(''); 

    function handleSubmit(e) {
        e.preventDefault();

        console.log('descricao: ' + descricao);
    }

    return (
        <div className = "register-container">
            <form> 
                <h1>Half Pugg</h1>
                <div>
                     <h1>Your games</h1>
                     
                 </div>
            </form>
        </div>
    );
}