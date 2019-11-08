import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Input, Image } from 'semantic-ui-react';

import api from '../services/api'

import './registergame.css';
import OWCard from '../Components/OWCard';

export default class registergame extends Component {

    state = {
        slogan: '',
        descricao: '',
        MyImage: '',
        toLogin: false,
        overwatchIDAPI: '',
        renderize: true,
        GamerLogado: {},
        OverwatchInfo: {},
    }
    
    componentDidMount = async () => {
        
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
        });

        const resposta = await api.get('api/GetGamesInPlayer?PlayerID=' + myData.ID).catch(err => console.log(err))
        resposta.data.map(async (playergame) => {
            let jogo = playergame;
            if(jogo.IDGame === 1){
                // Overwatch
                const ow = await api.get('api/GetPlayersOwerwatch?PlayerID='+jogo.IDGamer + '&Region=0').catch(err => console.log(err));
                console.log(ow.data);
                this.setState({OverwatchInfo: ow.data});
            }
        })
    }

    handleAPIInput = (e) => {
        this.setState({overwatchIDAPI: e.target.value});
    }

    handleAdicionarButton = (e) => {
        e.preventDefault();
        let idGame = 1; // do Overwatch
        api.post('api/PlayerGames', {
            "ID": 1,
            "Description": "Jogando",
            "IDGame": idGame,
            "IDGamer": this.state.GamerLogado.ID,
            "IdAPI": this.state.overwatchIDAPI,
            "Weight": 0,
        }
        ).then(res => {
            console.log('foi');
        }).catch(err => {
           console.log('id api inválida'); 
        });
    }

    render() {
        if(this.state.toLogin) {
            return <Redirect to ='/'></Redirect>
        }
        return (
            <div className = "register-container">
                <form> 
                    <h1 id='title'>Half Pugg</h1>
                    <div>
                        <h2>Your games</h2>
                        {this.state.OverwatchInfo.profile !== undefined ?
                            <OWCard {...this.state.OverwatchInfo}> </OWCard>
                            :
                            <div>
                                <div className="ui segment dimmable">
                                        <h3 className="ui header">Overwatch</h3>
                                        <div className="ui small ui small images images">
                                                <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                                <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                                <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image> 
                                        </div>
                                                <Image src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png" className="ui medium image"></Image>
                                </div>
                            </div>
                        }
                        <h2>Choose a new game</h2>
                        <div>
                            <div className="ui segment dimmable">
                                    <h3 className="ui header">League of Legends</h3>
                                    <div className="ui small ui small images images">
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image> 
                                    </div>
                                            <Image src={lol} circular size= 'small'></Image>
                            </div>
                            <div id="gameapi">
                            <Input value={this.state.overwatchIDAPI} onChange={e => this.handleAPIInput(e)} placeholder='Game API'></Input>
                            <Button.Group id="botoes">
                                <Button color='green' onClick={e => this.handleAdicionarButton(e)}>
                                    Add
                                </Button>
                            </Button.Group>
                            </div>
                        </div>
                        <div>
                            <div className="ui segment dimmable">
                                    <h3 className="ui header">Counter Strike</h3>
                                    <div className="ui small ui small images images">
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                            <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image> 
                                    </div>
                                            <Image src={cs} circular size= 'small'></Image>
                            </div>
                            <div id="gameapi">
                            <Input value={this.state.overwatchIDAPI} onChange={e => this.handleAPIInput(e)} placeholder='Game API'></Input>
                            <Button.Group id="botoes">
                                <Button color='green' onClick={e => this.handleAdicionarButton(e)}>
                                    Add
                                </Button>
                            </Button.Group>
                            </div>
                        </div>
                        <hr id='divider'></hr>
                        <div id="goback">
                            <Button.Group id="botoes">
                                <Button color='blue' onClick={e => this.handleAdicionarButton(e)}>
                                    Go back
                                </Button>
                            </Button.Group>
                        </div>
                        <hr id='divider'></hr>
                    </div>
                </form>
            </div>
        );
    }
}