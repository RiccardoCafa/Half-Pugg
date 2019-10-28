import React, { useState } from 'react';
import { Button, Image, TextArea, Form, Segment } from 'semantic-ui-react';
import gostosao from '../images/chris.jpg';

import './register2.css';
export default  function({history}) {

    const [ descricao, setDescricao ] = useState(''); 

    function handleSubmit(e) {
        e.preventDefault();

        console.log('descricao: ' + descricao);
    }

    return (
        <div className = "login-container">
            <h1 id='title'>Half Pugg</h1>
            <div id="biography">
            <Segment>
                <Image circular src={gostosao} size="small" centered></Image>
                <h4>DESCRIÇÃO</h4>
                <Form>
                    <TextArea placeholder="Tell us more" value={descricao} rows="3" 
                            onChange={e => setDescricao(e.target.value)}></TextArea>
                </Form>
                <h4>IMAGEM</h4>
                <Button fluid icon='upload'></Button>
                <Button.Group id={"botoes"}>
                    <Button color='green' onClick={e => handleSubmit(e)}>
                        Confirm
                    </Button>
                </Button.Group>
            </Segment>
            </div>
        </div>
    );
}