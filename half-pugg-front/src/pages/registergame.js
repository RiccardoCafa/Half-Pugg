import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Input, Image, Card, Loader, Modal } from 'semantic-ui-react';

import api from '../services/api'

import './registergame.css';
import OWCard from '../Components/OWCard';
import DOTACard from '../Components/DOTACard';

import overwatchImage from '../images/overwatch.jpg';
import Dota2 from '../images/dota-2.jpg';
import lolImage from '../images/lol.jpg';
import csImage from '../images/cs.jpg';

export default class registergame extends Component {

    state = {
        slogan: '',
        descricao: '',
        MyImage: '',
        toLogin: false,
        overwatchIDAPI: '',
        dotaAPI: '',
        lolIDAPI: '',
        csIDAPI: '',
        renderize: true,
        GamerLogado: {},
        OverwatchInfo: {},
        DotaInfo: {},
        loaded: false,
        openMessageBox: false,
        textMessageBox: '',


        GamesCadastrados:[{}],
        GamesUsuario:[{}],
        GamesSemConta:[{}],
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

        await api.get('api/Games').then(res=>{
            this.setState({GamesCadastrados: res.data});
        }).catch(erro=>{
            console.log(erro);
            this.setState({toLogin: true});
            return;
        }).then(
            ()=>{
                 api.get('api/Player/GetGames?PlayerID='+this.state.GamerLogado.ID).then(res=>{
                    this.setState({GamesUsuario:res.data});
        
                    this.state.GamesCadastrados.map((game)=>{
                        if(this.state.GamesUsuario.includes(game) == false){
                            this.state.GamesSemConta.push(game);
                        }
                    })

                    console.log('Cadastrados: '+this.state.GamesCadastrados);
                    console.log('Usuario: '+ this.state.GamesUsuario);
                    console.log('Sem conta: '+this.state.GamesSemConta);
                }).catch(erro=>{
                    console.log(erro);
                    this.setState({toLogin: true});
                    return;
                });
            }
        );

       

        await api.get('api/GetGamesInPlayer?PlayerID=' + myData.ID).catch(err => console.log(err)).then(
            resposta => {
                resposta.data.map(async (playergame) => {
                    let jogo = playergame;
                    if(jogo.IDGame === 1){
                        // Overwatch
                        const ow = await api.get('api/Overwatch/GetPlayers?PlayerID='+jogo.IDGamer + '&Region=0').catch(err => console.log(err));
                        this.setState({OverwatchInfo: ow.data});
                    }
                    if(jogo.IDGame === 2){
                        // Dota
                        const dota = await api.get('api/Dota/GetPlayers?PlayerID='+jogo.IDGamer).catch(err => console.log(err));
                        this.setState({DotaInfo: dota.data});
                    }
                })
            }
        )
        this.setState({loaded: true})
    }

    componentWillUnmount = () => {}

    handleDotaAPIInput = (e) => {
        this.setState({dotaAPI: e.target.value});
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
        let apid;
        switch(idGame){
            case 1:
                 apid = this.state.overwatchIDAPI;
                 api.post('api/Overwatch/PostPlayerInOw?Region=0', {
                    'ID': 0,
                    'Description': 'Jogando',
                    'IDGame': idGame,
                    'IDGamer': this.state.GamerLogado.ID,
                    'IdAPI': apid,
                    'Weight': 0
                }).then(res => 
                    this.setState({
                        openMessageBox: true,
                        textMessageBox: 'Conta adicionada com sucesso!'
                    })
                ).catch(err =>
                    this.setState({
                        openMessageBox: true,
                        textMessageBox: 'Conta não encontrada ou não está pública!'
                    })
                );
                 break;
            case 2:
                 apid = this.state.dotaAPI;
                 api.post('api/Dota/PostPlayerInDota', {
                    'ID': 0,
                    'Description': 'Jogando',
                    'IDGame': idGame,
                    'IDGamer': this.state.GamerLogado.ID,
                    'IdAPI': apid,
                    'Weight': 0
                }).then(res => 
                    this.setState({
                        openMessageBox: true,
                        textMessageBox: 'Conta adicionada com sucesso!'
                    })
                ).catch(err =>
                    this.setState({
                        openMessageBox: true,
                        textMessageBox: 'Conta não encontrada ou não está pública!'
                    })
                );
                 break;
            default: return;
        }

       
    }

    goBack = () => {
        this.props.history.push('/curriculo');
    }

    closeBox = () => this.setState({openMessageBox: false});

    render() {
        if(!this.state.loaded) {
            return <Loader active/>
        }
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
                            <Modal open={this.state.openMessageBox} onClose={this.closeBox} size='large'>
                                <Modal.Header>
                                    Adição de Jogo ao seu perfil
                                </Modal.Header>
                                <Modal.Content>
                                    {this.state.textMessageBox}
                                </Modal.Content>
                                <Modal.Actions>
                                    <Button
                                    positive icon='checkmark' labelPosition='right' content='Ok' onClick={this.closeBox}
                                    />
                                </Modal.Actions>
                            </Modal>
                            {this.state.OverwatchInfo.profile !== undefined ?
                                <OWCard {...this.state.GamerLogado}> </OWCard>
                                : null }
                                {this.state.DotaInfo.stats !== undefined ?
                                <DOTACard {...this.state.GamerLogado}></DOTACard>:null}
                            <h2>Choose a new game</h2>
                            <Card.Group>
                                {this.state.OverwatchInfo.profile === undefined ?
                                <Card >
                                    <Image src={overwatchImage} fluid style={{height:'150px'}} />
                                    <Card.Content>
                                        <Card.Header>Overwatch</Card.Header>
                                        <Card.Meta>
                                            FPS
                                        </Card.Meta>
                                        <Card.Description>Insira seu id da blizzard, nesse formato -> <br/> Exemplo-1234
                                        </Card.Description>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <Input value={this.state.overwatchIDAPI} 
                                            onChange={e => this.handleOWAPIInput(e)} placeholder='Battle.net ID'></Input>
                                        <Button.Group >
                                            <Button color='green' onClick={e => this.handleAdicionarButton(e, 1)}>
                                                Add
                                            </Button>
                                        </Button.Group>
                                    </Card.Content>
                                </Card> : null }

                                {this.state.DotaInfo.stats === undefined ?
                                <Card >
                                    <Image src={Dota2} fluid style={{height:'150px'}} />
                                    <Card.Content>
                                        <Card.Header>Dota 2</Card.Header>
                                        <Card.Meta>
                                            MOBA
                                        </Card.Meta>
                                        <Card.Description>Insira seu id da steam, nesse formato
                                        </Card.Description>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <Input value={this.state.dotaAPI} 
                                            onChange={e => this.handleDotaAPIInput(e)} placeholder='Steam ID'></Input>
                                        <Button.Group >
                                            <Button color='green' onClick={e => this.handleAdicionarButton(e, 2)}>
                                                Add
                                            </Button>
                                        </Button.Group>
                                    </Card.Content>
                                </Card> : null }
                            </Card.Group>
                            <br></br>
                            <Button.Group >
                                <Button color='blue' id="botoes" onClick={this.goBack}>
                                    Go back
                                </Button>
                            </Button.Group>
                        </div>
                        : <Loader active></Loader> }
                    </div>
                </form>
            </div>
        );
    }
}