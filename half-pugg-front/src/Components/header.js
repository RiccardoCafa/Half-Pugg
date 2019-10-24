import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

import { Menu, Image, Label, Input, Button } from 'semantic-ui-react';
import api from '../services/api';

import './header.css';

export default class header extends Component {

    state = {
        Name: '',
        LastName: '',
        Nickname: '',
        Email: '',
        activeItem: '',
        toHome: false,
    }
    //4b0082
    handleItemClick = (e, { name }) => this.setState( {activeItem: name } );

    async handleLogoff() {
        try {
            const response = api.delete('api/logoff', {
                "ID": 5,
                "Name": "t",
                "LastName": "e",
                "Nickname": "teste",
                "HashPassword": "12345",
                "Bio": "Biografia",
                "Email": "r@gmail.com",
                "Birthday": "1999-01-01T00:00:00",
            });
            if(response != null){
                this.setState({toHome: true});
            }
        } catch(error) {
            console.log(error);
        }
    }

    render() {
        const { activeItem } = this.state;

        if(this.state.toHome === true) {
            return <Redirect to='/'></Redirect>
        } 

        return (
            <div id='myHeader'>
                <Menu secondary id='botoes-header'>
                    <Menu.Item id='user-name-item' >
                        Olá, {this.props.dataFP}
                    </Menu.Item>
                    <Menu.Item 
                        name='Home'
                        active={activeItem === 'Home'}
                        onClick={this.handleItemClick}
                        />
                    <Menu.Item
                        name='My Connections'
                        active={activeItem === "Connect"}
                        onClick={this.handleItemClick}
                        />
                    <Menu.Menu position='right'>
                        <Menu.Item >
                            <Input icon='search' placeholder='Search in Half-Pugg'></Input>
                        </Menu.Item>
                        <Menu.Item>
                            <Button color='red' size='mini' onClick={this.handleLogoff}>Sair</Button>
                        </Menu.Item>
                    </Menu.Menu>
                </Menu>
            </div>
            
        );
    }
}