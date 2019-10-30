import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

import { Menu, Input, Button, Segment } from 'semantic-ui-react';

import './header.css';

export default class header extends Component {

    state = {
        Name: '',
        LastName: '',
        Nickname: '',
        Email: '',
        activeItem: '',
        toHome: false,
        toMyConnections: false,
        toBio: false,
        hideCom: false,
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

    goToMyConnections = () => {
        this.setState({toMyConnections: true});
    }

    render() {
        if(this.state.toMyConnections === true) {
            return <Redirect to="/MyConnections"></Redirect>
        }
        const { activeItem } = this.state;

        if(this.state.toHome === true) {
            return <Redirect to='/'></Redirect>
        } 
        if(this.state.toBio === true) {
            return <Redirect to="/bio"></Redirect>
        }

        return (
            <div>
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
                            onClick={this.goToMyConnections}
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
                {this.state.hideCom === false ?
                    <Segment id="incomplete-cadastro">
                        <p>Parece que tem informações faltando no seu perfil, atualize para que outros jogadores consigam saber mais de você!</p>
                        <Button primary onClick={() => this.goToBio}>Atualizar!</Button>
                        <Button id='botao-complete-cadastro' labelPosition='right' floated='right' onClick={() => this.setState({hideCom: true})}>X</Button>
                    </Segment>
                    : <div/>}
            </div>
        );
    }
}