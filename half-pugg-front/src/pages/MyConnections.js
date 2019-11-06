import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import { Card, Image, Button, Segment, Statistic } from 'semantic-ui-react';

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
        toMatch: false,
    }

    componentDidMount = async () => {

        const jwt = localStorage.getItem("jwt");
        let stop = false;
        //console.log(jwt);
        let myData;
        if(jwt){
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
        this.setNickname(myData);
        
        if(myData !== undefined && myData.data !== null) {
            const MatchData = await api.get('api/Matches/' + myData.ID);
            //{ headers: { "token-jwt": jwt }}
            if(MatchData.data != null){
                this.setState({Matches: MatchData.data});
            }
        }
    }

    componentWillMount() {

    }

    setNickname(myData) {
        
        this.setState({Nickname: myData.Nickname})
    }

    goToBio = () => {
        this.setState({toMatch: true});
    }

    render() {
        if(this.state.toMatch === true) {
            return <Redirect to='/match'></Redirect>
        }
        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera HeaderGamer = {this.state.GamerLogado}/>
                </div>  
                <Segment>
                    {this.state.Matches.length === 0 ?
                    <div>
                        <Statistic.Group>
                            <Statistic
                            value = "Ops! Parece que você não possui conexões..."
                            label = "Experimente conectar-se com outros gamers!"
                            text
                            id="sem-conexao-texto"></Statistic>
                        </Statistic.Group>
                        <Button id="sem-conexao-button" label="Quero me conectar!" basic icon='users' onClick={this.goToBio}></Button>
                    </div>:<div/>}
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