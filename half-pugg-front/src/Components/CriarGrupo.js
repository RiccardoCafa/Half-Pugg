import React, {Component} from 'react';
import {Modal, Header, Grid, Input, Card, Image, Button, List, Label, Icon} from 'semantic-ui-react';
import api from '../services/api';
import MessageBox from '../Components/MessageBox';

import defaultImage from '../images/default.png';

export default class CriarGrupo extends Component {
    state = {
        loadingCreation: false,
        hasMessage: false,
        groupTitle: '',
        groupImage: defaultImage,
        MyMessage: '',
        TitleMessage: '',
        gamerCreating: {},
        imageToUpload: {},
        Capacidade: 0,
    }

    componentDidMount = () => {
        this.setState({
            gamerCreating: this.props.gamer
        })
    }

    createPage = async () => {
        this.setState({loadingCreation: true});
        let imagePath = '';
        if(this.state.imageToUpload !== null){
            let config = { headers: {'Content-Type': 'multipart/form-data'}};
            let formData = new FormData();
            formData.append('image', this.state.imageToUpload);
            const response = await api.post('api/ImageUpload', formData, config);
            if(response){
                imagePath = response.data;
            }
        }

        try {
            const resp = await api.post('api/Groups', {
                "ID": 0,
                "Name": this.state.groupTitle,
                "Capacity": this.state.Capacidade,
                "IdGame": 1,
                "IdAdmin": this.state.gamerCreating.ID,
                "SourceImg": imagePath,
            });
            if(resp) {
                this.setState({
                    hasMessage: true,
                    TitleMessage: 'Deu tudo certo!',
                    MyMessage: `O grupo ${this.state.groupTitle} foi criado com sucesso, já pode chamar seus amigos!`
                });
            }
        } catch (error) {
            if(error.response) {
                this.setState({
                    hasMessage: true,
                    TitleMessage: 'Algo deu errado!',
                    MyMessage: error.response.data.Message,
                });
            }
            console.log(error);
        }
        this.setState({loadingCreation: false});
    }

    uploadImage = async (e) => {
        this.setState({imageToUpload: e.target.files[0], groupImage: URL.createObjectURL(e.target.files[0])});
    }

    closeMessage = () => this.setState({hasMessage: false});

    setCapacidade = (e) => this.setState({Capacidade: e.target.value});
    setTitle = (e) => this.setState({groupTitle: e.target.value});
    closeModal = () => this.setState({open: false});

    render() {
        return(
            <div>
            <Modal open={this.props.open} size='fullscreen'>
                <Header icon='edit' content='Criar um grupo'></Header>
                <Modal.Content >
                    <Modal.Description as='h2'>
                        Essa página serve para você montar um grupo!
                    </Modal.Description>
                </Modal.Content>
                <Modal.Content>
                    <Grid>
                        <Grid.Row>
                            <Grid.Column width={8} style={{marginRight: '3%'}}>
                                <List style={{marginLeft: '15%'}}>
                                    <List.Content style={{marginBottom: '3%'}}>
                                        <Label basic style={{marginBottom: '1%'}}>Foto para o seu grupo!</Label><br/>
                                        <Input type='file' label='Foto' labelPosition='left' loading={this.state.loadingCreation} onChange={e => this.uploadImage(e)} disabled={this.state.loadingCreation}></Input>
                                    </List.Content>
                                    <List.Content style={{marginBottom: '3%'}}>
                                        <Label basic style={{marginBottom: '1%'}}>Nome para o seu grupo!</Label> <br/>
                                        <Input label='Nome' maxLength={60} labelPosition='left' loading={this.state.loadingCreation} value={this.state.groupTitle} onChange={e => this.setTitle(e)} disabled={this.state.loadingCreation}></Input>
                                    </List.Content>
                                    <List.Content style={{marginBottom: '3%'}}>
                                        <Label basic style={{marginBottom: '1%'}}>Capacidade do grupo!</Label><br/>
                                        <Input label='Capacidade' type='number' maxLength={2} labelPosition='left' loading={this.state.loadingCreation} onChange={e => this.setCapacidade(e)} disabled={this.state.loadingCreation}></Input>
                                    </List.Content>
                                </List>
                            </Grid.Column>
                            <Grid.Column width={7}>
                                <Header as='h2'><b>Preview</b></Header>
                                <Card>
                                    <Image src={this.state.groupImage} size='medium'></Image>
                                    <Card.Content>
                                        <Card.Header>{this.state.groupTitle}</Card.Header>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <a>
                                            <Icon name='group'></Icon>
                                            Capacidade de pessoas {this.state.Capacidade}
                                        </a>
                                    </Card.Content>
                                </Card>
                            </Grid.Column>
                        </Grid.Row>
                    </Grid>
                </Modal.Content>
                <Modal.Content style={{marginLeft: '7%'}}>
                    <Button.Group>
                        <Button fluid basic positive primary content='Criar!' onClick={this.createPage}></Button>
                        <Button fluid basic negative secondary content='Fechar' onClick={this.props.close}/>
                    </Button.Group>
                </Modal.Content>
            </Modal>
            <MessageBox open={this.state.hasMessage} title={this.state.TitleMessage} Message={this.state.MyMessage} close={this.closeMessage}></MessageBox>
            </div>
        )
    }
}