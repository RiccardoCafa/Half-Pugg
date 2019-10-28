import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import api from '../services/api';

class Auth extends Component {

    state = {
        user: undefined,
        toLogin: false,

    }
    
    componentDidMount() {
        const jwt = localStorage.getItem("jwt");
        if(!jwt){
            this.setState({toLogin: true});
            
            api.get('api/Login', { headers: { "token-jwt": {jwt} }}).then(res => res.setState(
            {
                user: res.data
            }
            )).catch(err => {
                localStorage.removeItem("jwt");
            this.setState({toLogin: true});
            })
            console.log(this.state.user);
        }
    }

    render() {
        if(this.state.toLogin === true){
            return (
                <Redirect to='/'></Redirect>
            )
        }
        return (
            <div> 

            </div>
        )
    }
}

export default Auth;