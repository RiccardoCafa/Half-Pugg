import React, { Component } from 'react';
import api from '../services/api';

import { Menu, Image, Label, Input } from 'semantic-ui-react';

import './header.css';

export default class header extends Component {

    state = {
        Name: '',
        LastName: '',
        Nickname: '',
        Email: '',
        activeItem: '',
    }

    handleItemClick = (e, { name }) => this.setState( {activeItem: name } );

    // async componentDidMount() {
    //     const logged = await api.get('/api/Login');
    //     if(logged == null) {
    //         this.history.push('/');
    //     }else {
    //         console.log(logged.data);
    //     }
    // }

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
                    </Menu.Menu>
                </Menu>
            </div>
            
        );
    }
}