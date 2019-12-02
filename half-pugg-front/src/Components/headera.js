import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

import { Menu, Button, Segment } from 'semantic-ui-react';

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

    loadPage = (route) => {
        this.props.history.push(route);
    }

    render = () => {
        const { activeItem } = this.state;
        
        return (
            <div>
                <div id='myHeader'>
                    <Menu secondary id='botoes-header'>
                        <Menu.Item 
                            onClick={() => this.loadPage('/curriculo')}>
                            Olá, {this.props.gamer.Nickname}
                        </Menu.Item>
                        <Menu.Item 
                            name='Home'
                            icon='home'
                            active={activeItem === 'Home'}
                            onClick={() => this.loadPage('/match')}
                            />
                        <Menu.Item
                            name='My Connections'
                            icon='connectdevelop'
                            active={activeItem === "Connect"}
                            onClick={() => this.loadPage('/MyConnections')}
                            />
                        <Menu.Item
                            name='My Groups'
                            icon='group'
                            active={activeItem === "Connect"}
                            onClick={() => this.loadPage('/MyGroups')}
                            />
                        <Menu.Item
                            name='Analytics'
                            active={activeItem === "Connect"}
                            icon='chart line'
                            onClick={() => this.loadPage('/Analytics')}
                            />
                        <Menu.Menu position='right'>
                            <Menu.Item>
                                <Button color='red' size='mini' onClick={this.handleLogoff}>Sair</Button>
                            </Menu.Item>
                        </Menu.Menu>
                    </Menu>
                </div>
                {(this.props.gamer.Bio === null || this.props.gamer.Slogan === null) ?
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