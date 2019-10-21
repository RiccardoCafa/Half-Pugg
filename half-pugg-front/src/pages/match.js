import React, {Component} from 'react'

import './match.css'
import api from '../services/api'
import Headera from '../Components/header';
import { Card, Image, Button, Menu, Icon, Label } from 'semantic-ui-react';

import gostosao from '../images/chris-hemsworth.jpg'

export default class Match extends Component {

    state = {
        Nickname: '',
    }

    async componentDidMount() {
        const myData = await api.get('api/Login');
        if(myData.data != null){
            this.setState ({Nickname: myData.data.Nickname})
            console.log(this.state.Nickname);
        }
    }

    render() {
      
    return (
        <div>
            <div>
                <Headera dataFP = {this.state.Nickname}/>
            </div>  
            <div className='submenu'>
                <Menu compact>
                    <Menu.Item>
                        <Icon name='users'/> New Connections
                        <Label color='teal' floating>0</Label>
                    </Menu.Item>
                </Menu>
            </div>
            <div className='connections'>
                <Card.Group>
                    <Card>
                        <Card.Content>
                            <Image
                                floated='right'
                                size='mini'
                                src={gostosao}
                            />
                            <Card.Header>xXThorXx</Card.Header>
                            <Card.Meta>Sugestão de xXNoobMaster69Xx</Card.Meta>
                            <Card.Description>Principais Jogos: LOL, Overwatch e WoW. Recomendação de 80%</Card.Description>
                        </Card.Content>
                        <Card.Content extra>
                            <div className='ui two buttons'>
                                <Button basic color='green'>
                                    Connect!
                                </Button>
                                <Button basic color='red'>
                                    Decline!
                                </Button>
                            </div>
                        </Card.Content>
                    </Card>

                    <Card>
                        <Card.Content>
                            <Image
                                floated='right'
                                size='mini'
                                src={gostosao}
                            />
                            <Card.Header>xXThorXx</Card.Header>
                            <Card.Meta>Sugestão de xXNoobMaster69Xx</Card.Meta>
                            <Card.Description>Principais Jogos: LOL, Overwatch e WoW. Recomendação de 80%</Card.Description>
                        </Card.Content>
                        <Card.Content extra>
                            <div className='ui two buttons'>
                                <Button basic color='green'>
                                    Connect!
                                </Button>
                                <Button basic color='red'>
                                    Decline!
                                </Button>
                            </div>
                        </Card.Content>
                    </Card>
                </Card.Group>
            </div>
        </div>  
    )
    }
}