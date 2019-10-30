import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

import { Menu, Input, Button } from 'semantic-ui-react';

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

    handleLogoff() {
        try {
            localStorage.removeItem("jwt");
            localStorage.clear();
            this.setState({toHome: true});
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