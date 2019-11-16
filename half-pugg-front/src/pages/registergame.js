import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Input, Image, Card, Container } from 'semantic-ui-react';

import api from '../services/api'

import './registergame.css';
import OWCard from '../Components/OWCard';

import overwatchImage from '../images/overwatch.jpg';
import lolImage from '../images/lol.jpg';
import csImage from '../images/cs.jpg';

export default class registergame extends Component {

    state = {
        slogan: '',
        descricao: '',
        MyImage: '',
        toLogin: false,
        overwatchIDAPI: '',
        lolIDAPI: '',
        csIDAPI: '',
        renderize: true,
        GamerLogado: {},
        OverwatchInfo: {},
        loaded: false,
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

        this.setState({GamerLogado: myData});

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

        this.setState({loaded: true});
    }

    handleOWAPIInput = (e) => {
        this.setState({overwatchIDAPI: e.target.value});
    }

    handleLOLAPIInput = (e) => {
        this.setState({lolIDAPI: e.target.value});
    }

    handleCSAPIInput = (e) => {
        this.setState({csIDAPI: e.target.value});
    }

    handleAdicionarButton = (e, idGame) => {
        e.preventDefault();
        // 1 do Overwatch
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

    goBack = () => {
        this.props.history.push('/curriculo');
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
                        {this.state.loaded === true ?
                        <div>
                            <h2>Your games</h2>
                            {this.state.OverwatchInfo.profile !== undefined ?
                                <OWCard {...this.state.GamerLogado}> </OWCard>
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
                            <Card.Group>
                                <Card >
                                    <Image size='medium' src={overwatchImage} wrapped ui='false'/>
                                    <Card.Content>
                                        <Card.Header>Overwatch</Card.Header>
                                        <Card.Meta>
                                            FPS
                                        </Card.Meta>
                                        <Card.Description>Insira seu id da blizzard</Card.Description>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <Input value={this.state.overwatchIDAPI} onChange={e => this.handleOWAPIInput(e)} placeholder='Game ID'></Input>
                                        <Button.Group >
                                            <Button color='green' onClick={e => this.handleAdicionarButton(e, 1)}>
                                                Add
                                            </Button>
                                        </Button.Group>
                                    </Card.Content>
                                </Card>

                                <Card >
                                    <Image size='medium' src={lolImage} wrapped ui='false'/>
                                    <Card.Content>
                                        <Card.Header>League of Legends</Card.Header>
                                        <Card.Meta>
                                            MOBA
                                        </Card.Meta>
                                        <Card.Description>Insira seu id do riot</Card.Description>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <Input value={this.state.lolIDAPI} onChange={e => this.handleLOLAPIInput(e)} placeholder='Game ID'></Input>
                                        <Button.Group >
                                            <Button color='green' onClick={e => this.handleAdicionarButton(e, 2)}>
                                                Add
                                            </Button>
                                        </Button.Group>
                                    </Card.Content>
                                </Card>

                                <Card >
                                    <Image size='medium' src={csImage} wrapped ui='false'/>
                                    <Card.Content>
                                        <Card.Header>Counter Strike</Card.Header>
                                        <Card.Meta>
                                            FPS
                                        </Card.Meta>
                                        <Card.Description>Insira seu id da steam</Card.Description>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <Input value={this.state.csIDAPI} onChange={e => this.handleCSAPIInput(e)} placeholder='Game ID'></Input>
                                        <Button.Group >
                                            <Button color='green' onClick={e => this.handleAdicionarButton(e, 2)}>
                                                Add
                                            </Button>
                                        </Button.Group>
                                    </Card.Content>
                                </Card>
                            </Card.Group>
                            <br></br>
                            <Button.Group >
                                <Button color='blue' id="botoes" onClick={this.goBack}>
                                    Go back
                                </Button>
                            </Button.Group>
                        </div>
                        : null }
                    </div>
                </form>
            </div>
        );
    }
}