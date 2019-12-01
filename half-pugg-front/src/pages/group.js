import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';
import { Header } from 'semantic-ui-react';

import ow from '../images/overwatch.jpg'

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox, Statistic, Table, Loader, Dropdown , List} from 'semantic-ui-react';


import gostosao from '../images/chris.jpg';
import { request } from 'http';
import Chat from '../Components/Chat';

export default class Group extends Component {

    state = {
        Nickname: '',
        GamerMatch: [],
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        Group : {
            "ID": 2,
        },
        Integrants : [], 
        MenssageList : [    ],
        Friends :[]
    }

    async componentDidMount() {
        
        // Pega o usuário a partir do token
        const jwt = localStorage.getItem("jwt");
        let stop = false;
        let myData;
        if(jwt){
            await api.get('api/Login', { headers: { "token-jwt": jwt }}).then(res => 
                myData = res.data
            ).catch(error => stop = true)
        } else {
            stop = true;
        }

        if(stop) {
            this.setState({toLogin: true});
            return;
        }
        //console.log(this.props)
        this.setState({GamerLogado: myData})
        this.setNickname(myData);
        
        // Pega os dados de match do jogador
        if(myData !== undefined && myData.data !== null) {
            const GroupData = await api.get('api/Groups/'+ this.props.match.params.id);
            //console.log(GroupData.data)
            if(GroupData.data != null){
                this.setState({Group: GroupData.data});
            }    
            const compo = await api.get('api/GroupIntegrants?IdGroup='+ this.state.Group.ID );
            if(compo.data !== null) {
               this.setState({Integrants: compo.data});               
            }          
            console.log(compo)
            const MatchData = await api.get('api/Matches/' + myData.ID);
            //{ headers: { "token-jwt": jwt }}
            if(MatchData.data != null){
                this.setState({Friends: MatchData.data});
            }         
            
        }                
    }

    setNickname(myData) {
        this.setState({Nickname: myData.Nickname})
    }

    // Faz uma requisição de match para outro gamer
    
    render() {
        if(this.state.toLogin === true) {
            return <Redirect to="/"></Redirect>
        }
        if(this.state.goToMatch) {
            return <Redirect to='/match'></Redirect>
        }
        
        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera gamer = {this.state.GamerLogado }/>
                </div>  
                <Segment style={{'marginLeft': '1%', 'marginRight': '0%', 'marginBottom': '1%'}}><Header as='h2' icon='users' content={this.state.Group.Name} textAlign='center' /></Segment>
                
                <Grid columns={3} divided style={{'marginTop': '1%', 'marginLeft': '1%'}}   >
                
                    <Grid.Row >
                        <Segment >
                            <Grid.Column width={4} style={{'marginLeft': '1%', 'marginRight': '1%', 'marginBottom': '1%'}} >
                                <Segment textAlign='center'><Header as='h2' textAlign='center' icon='users' content='Integrants'/></Segment>
                                    <List relaxed animated divided verticalAlign='middle' style={{'marginLeft': '5%'}}>
                                            {this.state.Integrants.map((playerFound) => 
                                                <List.Item size='tiny' key={playerFound.ID} >
                                                    <Image avatar
                                                        floated='left'
                                                        src={(playerFound.ImagePath === "" || playerFound.ImagePath === null) 
                                                        ? gostosao : playerFound.ImagePath}
                                                        />
                                                    <List.Content>
                                                        <List.Header>{playerFound.Nickname}</List.Header>
                                                        <List.Description>{playerFound.Description }</List.Description>
                                                    </List.Content>
                                                </List.Item>
                                            )}
                                    </List>
                            </Grid.Column>
                        </Segment>
                        <Segment style={{'marginLeft': '1%'}} >
                                <Grid.Column width={100} style={{'marginLeft': '1%', 'marginRight': '1%', 'marginBottom': '1%'}}>
                                    <Segment textAlign='center'><Header as='h2' textAlign='center' icon='users' content='Menssagem'/></Segment>
                                    <Chat></Chat>
                                </Grid.Column>
                            </Segment>
                            <Segment style={{'marginLeft': '1%', 'marginRight': '0%', 'marginBottom': '1%'}}>
                                <Grid.Column width={4} style={{'marginLeft': '1%', 'marginRight': '1%', 'marginBottom': '3%'}}>
                                    <Segment textAlign='center'><Header as='h2' textAlign='center' icon='users' content='Invite' /></Segment>
                                    <List >
                                        
                                            {this.state.Friends.map((matcher) => 
                                                <Card key={matcher.matchPlayer.ID}>
                                                    
                                                    <Card.Content extra>
                                                        <div className='ui two buttons'>
                                                            <Button 
                                                                id='btn-acpden' 
                                                                basic color='green' 
                                                                content='Invite to Group!'
                                                                />
                                                        </div>
                                                    </Card.Content>
                                                    <Card.Content extra>
                                                        <OpenCurriculum {...matcher.matchPlayer}></OpenCurriculum>
                                                        
                                                    </Card.Content>
                                                </Card>
                                            )}
                                        
                                    </List>
                                </Grid.Column>
                            </Segment>
                    </Grid.Row>
                   
                </Grid>
                
            
                
            </div>
                
                
            
        )
    }
}
