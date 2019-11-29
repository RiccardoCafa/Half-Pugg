import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import { Button, Checkbox, Form, Segment, Grid, Input, Header, TextArea } from 'semantic-ui-react';

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
        toLogin: false,
        toRegister2: false,
    }
    
    handleSubmit = async e => {
        e.preventDefault();
        console.log("cadastro");
        const dts = this.state.Birthday.split("-");
        const dt = dts[1] + "/" + dts[2] + "/" + dts[0];

        await api.post('api/Gamers', {
            "Name": this.state.Name,
            "LastName": this.state.LastName,
            "Nickname": this.state.Nickname,
            "HashPassword": this.state.HashPassword,
            "Email": this.state.Email,
            "Birthday": dt,
            "Type": "F",
            "Sex": this.state.Sex,
            "ID_Branch": -1,
        }).then(
            this.setState({toLogin: true})
        ).catch(function (error) {
            console.log(error.response);
            console.log("Error: " + error.message);
        });
        console.log(dt);
    }

    handleCheckBox(e, value) {
        this.setState({Sex: value});
    }

    handleDesistoBtn = () => {
        this.props.history.push('/');
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
            <Segment style={{'marginLeft': '17%', 'marginRight': '17%'}}>
                <Header icon='edit' content='Faça seu cadastro!'></Header>
                <Grid centered columns={8}>
                    <Grid.Row>
                        <Grid.Column width={5}>
                            <h4>Primeiro Nome</h4>
                            <Input fluid
                                placeholder = ""
                                value = {this.state.Name}
                                onChange = { e => this.setState({Name: e.target.value})} 
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