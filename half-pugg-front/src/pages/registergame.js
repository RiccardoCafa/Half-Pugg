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
                     <h4>Choose a game</h4>
                     <div class="ui segment dimmable">
                            <h3 class="ui header">Overwatch</h3>
                            <div class="ui small ui small images images">
                                    <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                    <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                    <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image> 
                            </div>
                                    <Image src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png" class="ui medium image"></Image>
                            </div>
                    <div class="ui segment dimmable">
                            <h3 class="ui header">League of legends</h3>
                            <div class="ui small ui small images images">
                                    <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image> 
                                    <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                                    <Image src="https://react.semantic-ui.com/images/wireframe/image.png" class="ui image"> </Image>
                            </div>
                                    <Image
                                        src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png"
                                        class="ui medium image"
                                    > </Image>
                            </div>

                 </div>
                 <div id = "botoes">
                    <Button.Group id="botoes">
                        <Button color='green' onClick={e => handleSubmit(e)} >
                            Próximo
                        </Button>
                    </Button.Group>
                </div>
            </form>
        </div>
    );
}