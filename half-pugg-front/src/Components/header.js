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
                "ID": 0,
                "Nickname": this.state.Nickname,
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
                    <Menu.Item 
                        name='Home'
                        active={activeItem === 'Home'}
                        onClick={this.handleItemClick}
                        />
                    <Menu.Item
                        name='Match'
                        active={activeItem === 'Match'}
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
                        <Menu.Item >
                            <Label as='a' color='black'>
                                <Image avatar spaced='right'></Image>
                                {this.props.dataFP}
                            </Label>
                        </Menu.Item>
                        <Menu.Item>
                            <Button basic color='red' size='mini' onClick={this.handleLogoff}>Sair</Button>
                        </Menu.Item>
                    </Menu.Menu>
                </Menu>
            </div>
            
        );
    }
}