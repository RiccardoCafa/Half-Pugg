import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';

import './match.css'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import OpenCurriculum from '../Components/openCurriculum';
import Classification from '../Components/classification';
import { Card, Image, Button, Segment, Statistic, Loader } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg';
import UserContentCard from '../Components/UserContentCard';

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
        loaded: false,
    }

    componentDidMount = async () => {

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
        this.setState({loaded: true});
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
        if(!this.state.loaded) {
            return <Loader active></Loader>
        }

        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera gamer = {this.state.GamerLogado}/>
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
                            <Card key={matcher.matchPlayer.ID}>
                                <UserContentCard gamer={this.state.GamerLogado} gamerMatch={matcher}></UserContentCard>
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
                                    <Classification gamer={this.state.GamerLogado} gamerclassf={matcher.matchPlayer}></Classification>
                                </Card.Content>
                            </Card>
                        )}
                        </Card.Group>
                    </Segment>
            </div>  
        )
    }
}