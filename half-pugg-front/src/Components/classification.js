import React, { Component } from 'react';
import { Modal, Image, Button, Rating, Segment, Dropdown, Loader } from 'semantic-ui-react';
import gostosao from '../images/chris.jpg'
import api from '../services/api';

export default class classification extends Component {

    state = {
        Judged: false,
        jug: false,
        stars: 0,
        classificacoes: [
            {
                key: 1,
                text: 'Amigável',
                value: 'Amigável',
                icon: 'handshake outline',
            },
            {
                key: 2,
                text: 'Comunicativo',
                value: 'Comunicativo',
                icon: 'users',
            },
            {
                key: 3,
                text: 'Estratégico',
                value: 'Estratégico',
                icon: 'chess rock',
            }
        ],
        classificacao: '',
        loadingClassifc: false,
        loadedClassif: true,
        avaliacao: '',
        avalKey: 1,
        open: false,
        ImOpen: false,
        IDClassif: 0,
    }

    open = () => this.setState({open: true}); 
    close = () => this.setState({open: false});

    componentDidMount = async () => {
        if(this.props.classificacao !== null){
            this.setState({
                IDClassif: this.props.classificacao.ID,
                stars: this.props.classificacao.Points,
                classificacao: this.state.classificacoes.find(cl => cl.key === this.props.classificacao.IdClassification).value,
                jug: true,
            });
        }
    }

    onJudgment = (e, {rating}) => {
        this.setState({stars: rating});
    }

    setClassific = (e, {value}) => {
        const _key = this.state.classificacoes.find(x => x.value === value).key;
        this.setState({classificacao: value, avalKey: _key});
    }

    updateClassifc = async () => {
        await api.put(`api/ClassificationPlayers/${this.state.IDClassif}`, {
            'ID': this.state.IDClassif,
            'IdPlayer': this.props.gamer.ID,
            'IdJudgePlayer': this.props.gamerclassf.ID,
            'Points': this.state.stars,
            'IdClassification': this.state.avalKey,
        }).then(this.setState({
            avaliacao: 'Sua avaliação foi atualizada',
            loadingClassifc: true,
        })).catch(err =>{
            this.setState({avaliacao: 'Algo de inesperado ocorreu, tente recarregar a página, se isso ocorrer novamente contato o suporte!'})
        })
    }

    sendClassifc = async () => {
        this.setState({loadingClassifc: false});
        if(this.state.classificacao === '') {
            this.setState({
                loadingClassifc: false,
                avaliacao: 'Você precisa escolher um comportamento!'
            });
            return;
        }
        await api.post('api/ClassificationPlayers', {
            'IdPlayer': this.props.gamer.ID,
            'IdJudgePlayer': this.props.gamerclassf.ID,
            'Points': this.state.stars,
            'IdClassification': this.state.avalKey,
        })
        .then(this.setState({
            avaliacao: 'Sua avaliação foi bem sucedida!',
            loadingClassifc: true,
            ImOpen: false,
        }))
        .catch(err => this.setState({avaliacao: 'Algo de inesperado ocorreu, tente recarragar a página, se isso ocorrer novamente contate o suporte!'}));
    }

    fechaIsso = () => this.setState({ImOpen: false});

    render() {
        const {open} = this.state;
        const {avaliacao} = this.state;

        return (
            <div>
                {this.state.loadedClassif === true ?
                    <div>
                    <Modal open={this.state.ImOpen} onClose={this.fechaIsso} trigger={<Button onClick={() => this.setState({ImOpen: true})} fluid basic color='yellow'>Avaliar jogador</Button>}>
                        <Modal.Header>Avaliando {this.props.gamerclassf.Name}</Modal.Header>
                        <Modal.Content image>
                            <Image size='small' circular src={(this.props.gamerclassf.ImagePath === "" || this.props.gamerclassf.ImagePath === null) 
                                ? gostosao : this.props.gamerclassf.ImagePath}></Image>
                            <Modal.Description>
                                <Segment>Qual foi sua experiência com {this.props.gamerclassf.Name}? Considere comportamento, respeito e etc...</Segment>
                                <Rating size='massive' clearable icon='star' maxRating={5} rating={this.state.stars} 
                                        onRate={this.onJudgment}></Rating>
                                <Segment>Entre essas qual a melhor qualidade desse jogador em sua opinião?</Segment>
                                <Dropdown placeholder='Comportamento' value={this.state.classificacao} options={this.state.classificacoes} onChange={this.setClassific}></Dropdown>
                                <br/><br/>
                                <Modal.Actions>
                                    <Modal
                                        open={open}
                                        onOpen={this.open}
                                        onClose={this.close}
                                        size='small'
                                        trigger = {
                                            !this.state.jug ?
                                            <Button basic color='green' onClick={this.sendClassifc}>Enviar Avaliação!</Button>
                                            : <Button basic color='blue' onClick={this.updateClassifc}>Atualizar Classificação</Button>
                                        }>
                                        <Modal.Header>Avaliação...</Modal.Header>
                                        <Modal.Content>{avaliacao}</Modal.Content>
                                        <Modal.Actions>
                                            <Button icon='check' content='Certo' onClick={this.close}></Button>
                                        </Modal.Actions>
                                    </Modal>
                                </Modal.Actions>
                            </Modal.Description>
                        </Modal.Content>
                    </Modal>
                    </div>
                : <Loader active></Loader>}
            </div>
        );
    }
}