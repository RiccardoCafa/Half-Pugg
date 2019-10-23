import React, { Component } from 'react';

import { Menu, Image, Label, Input, Button } from 'semantic-ui-react';

import './header.css';

export default class header extends Component {

    state = {
        Name: '',
        LastName: '',
        Nickname: '',
        Email: '',
        activeItem: '',
    }
    //4b0082
    handleItemClick = (e, { name }) => this.setState( {activeItem: name } );

    render() {
        const { activeItem } = this.state;

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
                            <Button basic color='red' size='mini'>Sair</Button>
                        </Menu.Item>
                    </Menu.Menu>
                </Menu>
            </div>
            
        );
    }
}