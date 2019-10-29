import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import api from '../services/api';

class Auth extends Component {

    state = {
        user: undefined,
        toLogin: false,
        toMatch: false,
    }
    
    componentDidMount() {
        const jwt = localStorage.getItem("jwt");
        if(!jwt){
            this.setState({toLogin: true});
            
            api.get('api/ValidateToken', { headers: { "token-jwt": {jwt} }})
               .then(res => res.setState({ toMatch: true }))
               .catch(() => {
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