import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Image, TextArea, Form, Segment, Modal, Input } from 'semantic-ui-react';
import gostosao from '../images/chris.jpg';
import api from '../services/api';

import './register2.css';

export default class Register2 extends Component {

    state = {
        slogan: '',
        descricao: '',
        MyImage: '',
        Gamer: {},
        toLogin: false,
        showMessage: false,
        imageToUpload: {},
    }

    componentDidMount() {
        const jwt = localStorage.getItem("jwt");
        if(jwt){
            api.get('api/Login', { headers: { "token-jwt": jwt } })
                .then(res => 
                    this.loadGamer(res.data)
                ).catch(err => {
                    this.setState({toLogin: true});
                });
        }
    }

    loadGamer(GamerData) {
        this.setState({Gamer: GamerData});
        if(GamerData.Bio !== null) {
            this.setState({descricao: GamerData.Bio});
        }
        if(GamerData.Slogan !== null) {
            this.setState({slogan: GamerData.Slogan})
        }
        if(GamerData.ImagePath !== null) {
            this.setState({MyImage: GamerData.ImagePath});
        }
    }

    handleSubmit = async (e) => {
        e.preventDefault();

        console.log(this.state.Gamer);
        var newGamer = {...this.state.Gamer};
        if(this.state.imageToUpload !== ''){
            let config = { headers: {'Content-Type': 'multipart/form-data'}};
            let formData = new FormData();
            formData.append('image', this.state.imageToUpload);
            const response = await api.post('api/ImageUpload', formData, config);
            console.log(response.data);
            if(response){
                newGamer.ImagePath = response.data;
            }
        }

        newGamer.Bio = this.state.descricao;
        newGamer.Slogan = this.state.slogan;

        api.put('api/Gamers/' + this.state.Gamer.ID, newGamer);

        this.setState({showMessage: true});
    }

    confirmBox = () => {
        this.props.history.push('/curriculo');
    }

    uploadImage = async (e) => {
        this.setState({imageToUpload: e.target.files[0], MyImage: URL.createObjectURL(e.target.files[0])});
        console.log(e.target.files[0]);
    }

    render() {
        if(this.state.toLogin) {
            return <Redirect to ='/'></Redirect>
        }
        return (
            <div className = "login-container">
                <h1>Half Pugg</h1>
                <div id="biography">
                    <Modal open={this.state.showMessage}>
                        <Modal.Header>
                            Tudo certo!
                        </Modal.Header>
                        <Modal.Content>
                        Agora vamos te redirecionar para o seu currículo
                        </Modal.Content>
                        <Modal.Actions>
                            <Button positive labelPosition='left' icon='checkmark' content='Ok' onClick={this.confirmBox}></Button>
                        </Modal.Actions>
                    </Modal>
                <Segment>
                    <Image circular src={this.state.MyImage === null ? gostosao : this.state.MyImage} size="tiny" centered></Image>
                    <h4>SEU GRITO DE GUERRA (50)</h4>
                    <Form>
                        <TextArea placeholder="Qual seu recado?" rows="3" 
                                  onChange={e => this.setState({slogan: e.target.value})}
                                  value={this.state.slogan}>
                        </TextArea>
                    </Form>
                    <h4>SUA HISTÓRIA PARA SER CANTADA! (250)</h4>
                    <Form>
                        <TextArea placeholder="Quais foram as histórias mais sangrentas??" rows="3" 
                                  onChange={e => this.setState({descricao: e.target.value})}
                                  value={this.state.descricao}>
                        </TextArea>
                    </Form>
                    <h4>CARREGUE UMA FOTO DE PERFIL</h4>
                    <Input fluid icon='upload' type='file' accept='.png, .jpg, .jpeg' onChange={this.uploadImage}></Input>
                    <br/>
                    <Button.Group fluid>
                        <Button color='green' onClick={e => this.handleSubmit(e)}>
                            Confirmar
                        </Button>
                        <Button color='red' onClick={this.confirmBox}>
                            Descartar
                        </Button>
                    </Button.Group>
                </Segment>
                </div>
            </div>
        );
    }
}