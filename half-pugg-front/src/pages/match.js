import React, {Component} from 'react'

import './match.css'
import api from '../services/api'
import Headera from '../Components/header';
import { Card, Image, Button, Menu, Icon, Label } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg'

export default class Match extends Component {

    state = {
        Nickname: '',
        GamerMatch: [],
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        RequestedMatches: [],
        NumberOfRequests: 0,
        NewConnections: false,
    }

    async componentDidMount() {
        const myData = await api.get('api/Login');
        if(myData.data != null){
            this.setState({GamerLogado: myData.data});
            this.setState ({Nickname: myData.data.Nickname});
            console.log(this.state.Nickname);
        }

        const MatchData = await api.get('api/GamersMatch');
        if(MatchData.data != null){
            console.log(MatchData.data);
            this.setState({GamerMatch: MatchData.data});
        }

        const requestedMatch = await api.get('api/RequestedMatchesLoggedGamer');
        if(requestedMatch.data !== null) {
            this.setState({RequestedMatches: requestedMatch.data});
            console.log(requestedMatch.data);
            this.setState({NumberOfRequests: requestedMatch.data.length});
        }
    }

    connectMatch(matcher) {

        console.log(matcher);
        const response = api.post('api/RequestedMatches', {
            "IdPlayer": this.state.GamerLogado.ID,
            "IdPlayer2": matcher.ID,
            "Status": "A",
        })
        .catch(function(error){
            console.log(error);
        });
        
        if(response !== null) {
            var array = [...this.state.GamerMatch];
            var index = array.indexOf(matcher);
            if(index !== -1) {
                array.splice(index, 1);
                this.setState({GamerMatch: array});
            }
        }
    }

    async desconnectMatch() {
        
    }

    openRequests() {
        console.log(this.state.RequestedMatches.data);
        this.setState({NewConnections: true})
    }

    openConnections() {
        this.setState({NewConnections: false});
    }

    render() {
      
        return (
            <div>
                <div>
                    <Headera dataFP = {this.state.Nickname}/>
                </div>  
                <div className='submenu'>
                    <Menu compact>
                        <Menu.Item onClick={e => this.openConnections()}>
                            <Icon name='users'/> My Connections
                        </Menu.Item>
                        <Menu.Item onClick={e => this.openRequests()}>
                            <Icon name='mail'/> Pending Requests
                            <Label color='teal' floating>{this.state.NumberOfRequests}</Label>
                        </Menu.Item>
                    </Menu>
                </div>
                <div className='connections'>
                    {this.state.NewConnections === false ?
                    <Card.Group>
                        {this.state.GamerMatch.map((matcher) => 
                            <Card key={matcher.ID} >
                                <Card.Content>
                                    <Image
                                        floated='right'
                                        size='mini'
                                        src={matcher.ImagePath === "" ? gostosao : matcher.ImagePath}
                                        />
                                    <Card.Header>{matcher.Nickname}</Card.Header>
                                    <Card.Meta>Sugestão de xXNoobMaster69Xx</Card.Meta>
                                    <Card.Description>Principais Jogos: LOL, Overwatch e WoW. Recomendação de 80%</Card.Description>
                                </Card.Content>
                                <Card.Content extra>
                                    <div className='ui two buttons'>
                                        <Button id='btn-acpden' basic color='green' onClick={() => this.connectMatch(matcher)}>
                                            Connect!
                                        </Button>
                                        <Button id='btn-acpden' basic color='red' onClick={this.desconnectMatch}>
                                            Not Interested!
                                        </Button>
                                    </div>
                                </Card.Content>
                                <Card.Content extra>
                                    <Button fluid basic color='blue'>
                                        Open Curriculum
                                    </Button>
                                </Card.Content>
                            </Card>
                        )};
                    </Card.Group>
                    :
                    <Card.Group>
                        {this.state.RequestedMatches.map((requests) => 
                            <Card key = {requests.ID} >
                                <Card.Content>
                                    <Image
                                        floated='right'
                                        size='mini'
                                        src={gostosao}
                                        />
                                    <Card.Header>{requests.Nickname}</Card.Header>
                                    <Card.Meta>Sugestão de xXNoobMaster69Xx</Card.Meta>
                                    <Card.Description>Principais Jogos: LOL, Overwatch e WoW. Recomendação de 80%</Card.Description>
                                </Card.Content>
                                <Card.Content extra>
                                    <div className='ui two buttons'>
                                        <Button id='btn-acpden' basic color='green'>
                                            Accept!
                                        </Button>
                                        <Button id='btn-acpden' basic color='red'>
                                            Deny!
                                        </Button>
                                    </div>
                                </Card.Content>
                                <Card.Content extra>
                                    <Button fluid basic color='blue'>
                                        Open Curriculum
                                    </Button>
                                </Card.Content>
                            </Card>
                        )}
                    </Card.Group>
                    }
                </div>
            </div>  
        )
    }
}