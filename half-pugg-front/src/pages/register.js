import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import { Button, Checkbox, Form, Segment, Grid, Input, Header, Modal } from 'semantic-ui-react';

import './register.css';
import api from '../services/api';

export default class register extends Component {

    state = {
        Name: '',
        LastName: '',
        Nickname: '',
        HashPassword: '',
        Email: '',
        Birthday: '',
        confirmSenha: '',
        Sex: '',
        ShowLoginMessage: false,
        toLogin: false,
        toRegister2: false,
    }
    
    temMaisTreze = (dt) => {
        let dataNasc = new Date(dt);
        let nowadays = new Date();
        let anhos = nowadays.getFullYear() - dataNasc.getFullYear();
        if(anhos < 13) {
            return false;
        } else if(anhos === 13) {
            if(dataNasc.getMonth() <= nowadays.getMonth()) {
                if(dataNasc.getMonth() === nowadays.getMonth()){
                    if(dataNasc.getDate() <= nowadays.getDate()) {
                        if(dataNasc.getDate() === nowadays.getDate()){
                            console.log('aniversario do cara ou');
                            alert('Parabeeens!');
                            return true;
                        }
                    } else {return false;}
                }
                return true;
            }
        }
        return true;
    }

    handleSubmit = async e => {
        e.preventDefault();
        const dts = this.state.Birthday.split("-");
        const dt = dts[1] + "/" + dts[2] + "/" + dts[0];
        const tem13 = this.temMaisTreze(dt);
        if(!tem13) {
            alert('Voce precisa ter mais de 13 anos meu caro');
            return;
        }
        const regex = /\W/;
        if(regex.test(this.state.Nickname) || this.state.Nickname === ''){
            alert('Seu nickname está inválido! Apenas letras, números e underline (_)');
            return;
        }
        if(!this.state.Email.includes('@')) {
            alert('email inválido');
            return;
        }
        const response = await api.post('api/CadastroPlayer', {
            "Name": this.state.Name,
            "LastName": this.state.LastName,
            "Nickname": this.state.Nickname,
            "HashPassword": this.state.HashPassword,
            "Email": this.state.Email,
            "Birthday": dt,
            "Type": "F",
            "Sex": this.state.Sex,
            "ID_Branch": -1,
        }).catch(function (error) {
            console.log(error.response);
            console.log("Error: " + error.message);
            alert('algo deu errado');
        });
        if(response){
            this.setState({ShowLoginMessage: true});
        }
    }

    handleCheckBox(e, value) {
        this.setState({Sex: value});
    }

    handleDesistoBtn = () => {
        this.props.history.push('/');
    }

    handleNameBox = (e) => {
        this.setState({Name: e.target.value})
    }

    render(){
        if(this.state.toLogin === true) {
            return <Redirect to='/'></Redirect>
        }
        return (
            <div>    
            <div className= "register-title">
                <Link to = "/">
                    <Header textAlign='center' as='h1' style={{'marginTop': '2%', 'MarginBottom': '2%'}}>Half Pugg</Header>
                </Link>
            </div>
            <div>
            <Modal open={this.state.ShowLoginMessage} size='small'>
                <Header icon='checkmark' content='A operação foi um sucesso!'></Header>
                <Modal.Content>Clique no botão para ser redirecionado à página de Login.</Modal.Content>
                <Modal.Actions>
                    <Button color='green' content='Ok' onClick={this.handleDesistoBtn}></Button>
                </Modal.Actions>
            </Modal>
            <Segment style={{'marginLeft': '17%', 'marginRight': '17%'}}>
                <Header icon='edit' content='Faça seu cadastro!'></Header>
                <Grid centered columns={8}>
                    <Grid.Row>
                        <Grid.Column width={5}>
                            <h4>Primeiro Nome</h4>
                            <Input fluid
                                placeholder = ""
                                value = {this.state.Name}
                                onChange = { e => this.handleNameBox(e)} 
                                maxLength = {30}
                                />
                        </Grid.Column>
                        <Grid.Column width={5}>
                            <h4>Último Nome</h4>
                            <Input fluid
                                placeholder = ""
                                value = {this.state.LastName}
                                maxLength = {30}
                                onChange = {e => this.setState({LastName: e.target.value})}
                                />
                        </Grid.Column>
                        <Grid.Column width={5}>
                            <h4>Seu nome heróico (ง ͠° ͟ل͜ ͡°)ง</h4>
                            <Input fluid
                                placeholder = ""
                                value = {this.state.Nickname}
                                onChange = { e => this.setState({Nickname: e.target.value})} 
                                maxLength = {25}
                            />
                        </Grid.Column>
                    </Grid.Row>
                    <Grid.Row>
                        <Grid.Column width={5}>
                            <h4>Seu email fabuloso ( ✧≖ ͜ʖ≖)</h4>
                            <Input fluid
                                placeholder = ""
                                value = {this.state.Email}
                                onChange = { e => this.setState({Email: e.target.value})}
                                type= {"email"}
                            />
                        </Grid.Column>
                        <Grid.Column width={5}>
                            <h4>Declare palavras secretas ( ͡~ ͜ʖ ͡°)</h4>
                            <Input fluid
                                placeholder = ""
                                value = {this.state.HashPassword}
                                onChange = { e => this.setState({HashPassword: e.target.value})}
                                type = {"password"}
                                maxLength = {20}
                            />
                        </Grid.Column>
                        <Grid.Column width={5}>  
                            <h4>Confirme as palavras secretas ಠ_ಠ</h4>
                            <Input fluid
                                placeholder = ""
                                value = {this.state.confirmSenha}
                                onChange = { e => this.setState({confirmSenha: e.target.value})}
                                type = {"password"}
                            />
                        </Grid.Column>
                    </Grid.Row>
                    <Grid.Row>
                        <Grid.Column width={5}>
                            <h4>Dia de spawn no mundo</h4>
                            <Input fluid
                                placeholder = ""
                                value = {this.state.Birthday}
                                onChange = { e => this.setState({Birthday: e.target.value})}
                                type = {"date"}
                            />
                        </Grid.Column>
                        <Grid.Column width={5} >
                            <Form size='mini' style={{'marginLeft': '30%'}}>
                                <Form.Field>
                                    <Checkbox
                                        radio
                                        label='Homi'
                                        name='radioGroup'
                                        value='M'
                                        checked = {this.state.Sex === 'M'}
                                        onChange={e => this.handleCheckBox(e, 'M')}
                                    />
                                </Form.Field>
                                <Form.Field>
                                    <Checkbox
                                        radio
                                        label='Mulé'
                                        name='radioGroup'
                                        value='F'
                                        checked={this.state.Sex === 'F'}
                                        onChange={e => this.handleCheckBox(e, 'F')}
                                    />
                                </Form.Field>
                                <Form.Field>
                                    <Checkbox
                                        radio
                                        label='Outros'
                                        name='radioGroup'
                                        value='F'
                                        checked={this.state.Sex === 'O'}
                                        onChange={e => this.handleCheckBox(e, 'O')}
                                    />
                                </Form.Field>
                            </Form>
                        </Grid.Column>
                        <Grid.Column width={5}>
                        </Grid.Column>
                    </Grid.Row>
                    <Grid.Row>
                        <Button.Group fluid style={{'marginLeft': '5%', 'marginRight': '5%'}}>
                            <Button secondary size='mini' color='red' onClick={this.handleDesistoBtn}>
                                Desistir
                            </Button>
                            <Button.Or text='Ou'/>
                            <Button primary size='mini' color='green' onClick={this.handleSubmit} >
                                Cadastrar-se
                            </Button>
                        </Button.Group>
                    </Grid.Row>
                </Grid>
            </Segment>
            </div>
            
        </div>
        );
    }
}