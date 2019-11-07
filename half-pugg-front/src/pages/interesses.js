import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Image, TextArea, Form, Segment } from 'semantic-ui-react';

import './interesses.css';

export default class interesses extends Component {

    state = {
        slogan: '',
        descricao: '',
        MyImage: '',
        Gamer: {},
        toLogin: false,
    }
//    handleSubmit(e) {
       
//    }

render() {
    if(this.state.toLogin) {
        return <Redirect to ='/'></Redirect>
    }
    return (
       <div/>
    );
}
}