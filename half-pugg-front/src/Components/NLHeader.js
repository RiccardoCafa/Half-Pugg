import React, {Component} from 'react';
import {withRouter} from 'react-router-dom';

import './header.css';
import { Menu, Button } from 'semantic-ui-react';

class NLHeader extends Component {

    voltaProLogin = () => {
        this.props.history.push('/')
    }

    vaiProRegistro = () => {
        this.props.history.push('/register');
    }

    render() {
        return(
            <div id='myHeader'>
                <Menu secondary id='botoes-header'>
                    <Menu.Menu position='left'>
                        <Menu.Item
                        icon='user circle'
                        >Olá, indefinido! Que tal fazer login ou cadastrar-se?
                        </Menu.Item>
                        <Menu.Item>
                            <Button color='green' size='mini' onClick={this.voltaProLogin}>Login</Button>
                            <Button color='blue' size='mini' onClick={this.vaiProRegistro}>Cadastro</Button>
                        </Menu.Item>
                    </Menu.Menu>
                </Menu>
            </div>
        );
    }
}

export default withRouter(NLHeader);