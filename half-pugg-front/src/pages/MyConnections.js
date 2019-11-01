import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Segment } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg';

export default class MyConnections extends Component {

    state = {
        Nickname: '',
        GamerMatch: [],
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        Matches: [],
    }

    async componentDidMount() {

        const jwt = localStorage.getItem("jwt");
        let stop = false;
        //console.log(jwt);
        let myData;
        if(jwt){
            console.log(jwt);
            await api.get('api/Login', { headers: { "token-jwt": jwt }}).then(res => 
                myData = res.data
                //console.log(res.data)
            ).catch(error => stop = true)
        } else {
            stop = true;
        }

        if(stop) {
            this.setState({toLogin: true});
            return;
        }

        this.setState(
        {
            GamerLogado: myData
        })
        console.log(myData);
        this.setNickname(myData);
        
        if(myData !== undefined && myData.data !== null) {
            console.log(myData.ID);
            const MatchData = await api.get('api/Matches/' + myData.ID);
            //{ headers: { "token-jwt": jwt }}
            if(MatchData.data != null){
                console.log(MatchData.data);
                this.setState({Matches: MatchData.data});
            }
        }
    }

    setNickname(myData) {
        
        this.setState({Nickname: myData.Nickname})
    }

    render() {
        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera HeaderGamer = {this.state.GamerLogado}/>
                </div>  
                <Segment>
                    <Card.Group>
                        {this.state.Matches.map((matcher) => 
                            <Card key={matcher.ID} >
                                <Card.Content>
                                    <Image
                                        floated='right'
                                        size='mini'
                                        src={(matcher.ImagePath === "" || matcher.ImagePath === null) ? gostosao : matcher.ImagePath}
                                        />
                                    <Card.Header>{matcher.Nickname}</Card.Header>
                                    <Card.Meta>Sugestão de xXNoobMaster69Xx</Card.Meta>
                                    <Card.Description>Principais Jogos: LOL, Overwatch e WoW. Recomendação de 80%</Card.Description>
                                </Card.Content>
                                <Card.Content extra>
                                    <div className='ui two buttons'>
                                        <Button 
                                            id='btn-acpden' 
                                            basic color='green' 
                                            onClick={() => this.connectMatch(matcher)}
                                            content='Invite to Group!'
                                            />
                                    </div>
                                </Card.Content>
                                <Card.Content extra>
                                    <OpenCurriculum matcher={matcher}></OpenCurriculum>
                                </Card.Content>
                            </Card>
                        )}
                        </Card.Group>
                    </Segment>
            </div>  
        )
    }
}