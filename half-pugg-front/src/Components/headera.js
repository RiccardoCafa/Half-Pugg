import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

import { Menu, Input, Button, Segment } from 'semantic-ui-react';

import './header.css';

class Header extends Component {

    state = {
        Name: '',
        LastName: '',
        Nickname: '',
        Email: '',
        activeItem: '',
        Gamer: {},
        hideCom: false,
    }
    //4b0082
    handleItemClick = (e, { name }) => this.setState( {activeItem: name } );

    handleLogoff = () => {
        try {
            localStorage.removeItem("jwt");
            localStorage.clear();
            this.props.history.push('/');
        } catch(error) {
            console.log(error);
        }
    }

    hideWindow = () => this.setState({hideCom: true});

    checkForBio = (gamer) => {
        if(gamer.Bio !== null && gamer.Slogan !== null) {
            if(!this.state.hideCom) {
                this.setState({hideCom: true})
            }
        }
    }

    componentDidMount = () => {
        this.checkForBio(this.props.gamer);
    }

    loadPage = (route) => {
        this.props.history.push(route);
    }

    render = () => {
        const { activeItem } = this.state;
        
        return (
            <div>
                <div id='myHeader'>
                    <Menu secondary id='botoes-header'>
                        <Menu.Item id='user-name-item' onClick={() => this.loadPage('/curriculo')}>
                            Olá, {this.props.gamer.Nickname}
                        </Menu.Item>
                        <Menu.Item 
                            name='Home'
                            active={activeItem === 'Home'}
                            onClick={() => this.loadPage('/match')}
                            />
                        <Menu.Item
                            name='My Connections'
                            active={activeItem === "Connect"}
                            onClick={() => this.loadPage('/MyConnections')}
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
                        <Button primary onClick={() => this.loadPage('/bio')}>Atualizar!</Button>
                        <Button id='botao-complete-cadastro' labelPosition='right' floated='right' onClick={this.hideWindow}>X</Button>
                    </Segment>
                    : <div/>}
            </div>
        );
    }
}

export default withRouter(Header);