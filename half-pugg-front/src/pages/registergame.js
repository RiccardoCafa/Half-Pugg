import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Input, Image, Card, Loader, Modal } from 'semantic-ui-react';

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
                })
                this.setState({loaded: true})
            }
        )
    }

    componentWillUnmount = () => {}

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
            case 1: apid = this.state.overwatchIDAPI; break;
            case 2: apid = this.state.lolIDAPI; break;
            default: return;
        }

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
    }

    goBack = () => {
        this.props.history.push('/curriculo');
    }

    closeBox = () => this.setState({openMessageBox: false});

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
                            <Modal open={this.state.openMessageBox} onClose={this.closeBox} size='mini'>
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


                            <h2>Choose a new game</h2>
                            <Card.Group>
                            {this.state.GamesSemConta.map((game) =>
                         
                                <Card key={game.Game_ID}>
                                    <Image src={game.EndPoint} fluid style={{height:'150px'}} />
                                    <Card.Content>
                                        <Card.Header>{game.Name}</Card.Header>
                                        <Card.Meta>
                                            FPS
                                        </Card.Meta>
                                        <Card.Description>
                                            {game.Description}
                                        </Card.Description>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <Input value={this.state.overwatchIDAPI} onChange={e => this.handleOWAPIInput(e)} placeholder='Game ID'/>
                                        <Button.Group >
                                            <Button color='green' onClick={e => this.handleAdicionarButton(e, 1)}>
                                                Add
                                            </Button>
                                        </Button.Group>
                                    </Card.Content>
                                </Card> 
                            )}
                               
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