import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import { Button, Checkbox } from 'semantic-ui-react';

import './register.css';
import pugg from '../images/Logo_Pugg.png';
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

        const respo = await api.post('api/Gamers', {
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
        });
        if(respo != null){
            this.setState({toRegister2: true});
        }
        //useHistory().push('/');
        console.log(dt);
    }

    handleCheckBox(e, value) {
        this.setState({Sex: value});
    }

    render(){
        if(this.state.toRegister2 === true) {
            return <Redirect to='/bio'></Redirect>
        }
        return (
            <div className = "register-container-3">    
            <div className= "register-title">
                <Link to = "/">
                    <h1>Half Pugg</h1>
                </Link>
            </div>
            <div className = "register-inputs">
            <form onSubmit={this.handleSubmit}>
                <ul>
                    <li>
                        <h4>Primeiro Nome</h4>
                        <input 
                            placeholder = ""
                            value = {this.state.Name}
                            onChange = { e => this.setState({Name: e.target.value})} 
                            maxLength = {30}
                        />
                    </li>
                    <li>
                        <h4>Último Nome</h4>
                        <input 
                            placeholder = ""
                            value = {this.state.LastName}
                            maxLength = {30}
                            onChange = {e => this.setState({LastName: e.target.value})}
                        />
                    </li>
                    <li>
                        <h4>Seu nome heróico (ง ͠° ͟ل͜ ͡°)ง</h4>
                        <input 
                            placeholder = ""
                            value = {this.state.Nickname}
                            onChange = { e => this.setState({Nickname: e.target.value})} 
                            maxLength = {25}
                        />
                    </li>
                    <li>
                        <h4>Seu email fabuloso ( ✧≖ ͜ʖ≖)</h4>
                        <input 
                            placeholder = ""
                            value = {this.state.Email}
                            onChange = { e => this.setState({Email: e.target.value})}
                            type= {"email"}
                        />
                    </li>
                    <li>
                        <h4>Declare palavras secretas ( ͡~ ͜ʖ ͡°)</h4>
                        <input 
                            placeholder = ""
                            value = {this.state.HashPassword}
                            onChange = { e => this.setState({HashPassword: e.target.value})}
                            type = {"password"}
                            maxLength = {20}
                        />
                    </li>
                    <li >  
                        <h4>Confirme as palavras secretas ಠ_ಠ</h4>
                        <input 
                            placeholder = ""
                            value = {this.state.confirmSenha}
                            onChange = { e => this.setState({confirmSenha: e.target.value})}
                            type = {"password"}
                        />
                    </li>
                    <li>
                        <h4>Dia de spawn no mundo</h4>
                        <input 
                            placeholder = ""
                            value = {this.state.Birthday}
                            onChange = { e => this.setState({Birthday: e.target.value})}
                            type = {"date"}
                        />
                    </li>
                    <li>
                        <Checkbox
                            radio
                            label='Homi'
                            name='checkboxRadioGroup'
                            value='M'
                            checked = {this.state.Sex === 'M'}
                            onChange={e => this.handleCheckBox(e, 'M')}
                        />
                        <Checkbox
                            radio
                            label='Mulé'
                            name='checkboxRadioGroup'
                            value='F'
                            checked={this.state.Sex === 'F'}
                            onChange={e => this.handleCheckBox(e, 'F')}
                        />
                    </li>
                </ul>
                <Button id='botao-registro' size='mini' color='green' type="submit" >
                    Cadastrar-se
                </Button>
            </form>
            </div>
            
        </div>
        );
    }
}