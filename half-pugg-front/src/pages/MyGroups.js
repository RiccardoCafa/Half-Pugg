import React, {Component} from 'react'
import {Redirect} from 'react-router-dom';
import { withRouter } from 'react-router-dom';
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import { Card, Image, Button, Menu, Icon, Label, Segment, Grid, Input, Checkbox, Statistic, Table, Loader, Dropdown } from 'semantic-ui-react';

export default class MyConnections extends Component {

    state = {
        Nickname: '',
        GamerMatch: [],
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        Group: [],
        loaded: false,
    }

    async componentDidMount() {
        // Pega o usuário a partir do token
        const jwt = localStorage.getItem("jwt");
        let stop = false;
        let myData;
        let groups;
        if(jwt){
            await api.get('api/Login', { headers: { "token-jwt": jwt }}).then(res => 
                myData = res.data
            ).catch(error => stop = true)
        } else {
            stop = true;
        }

        if(stop) {
            this.setState({toLogin: true});
            return;
        }

        this.setState({GamerLogado: myData})
        //this.setNickname(myData);
        
        await api.get('api/Gamers/GetGroups?id='+myData.ID).then(res=> groups = res.data).catch(error => stop = true)
        this.setState({Group: groups})
    }

  
    render() {
        
        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera gamer = {this.state.GamerLogado}/>
                </div>  
                <Segment>
                    {this.state.Group.length === 0 ?
                    <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
                        <Statistic.Group>
                            <Statistic
                            value = "Ops! Parece que você não participa de nenhum grupo..."
                            label = "Crie um grupo e e chame seus amigos"
                            text
                            id="sem-conexao-texto"></Statistic>
                        </Statistic.Group>
                        <Button id="sem-conexao-button" label="Quero me conectar!" basic icon='users' onClick={this.goToBio}></Button>
                    </div>
                    :
                    <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center'}}>
                       
                        {this.state.Group.map((group) => 
                            <Card key={group.ID} onClick={()=> this.props.history.push('/group/'+group.ID)}>
                                    <Image src= {group.ImagePath} wrapped ui={false} />
                                    <Card.Content>
                                    <Card.Header>{group.Name}</Card.Header>
                                    <Card.Description>
                                       {group.Desc}
                                    </Card.Description>
                                    <Card.Meta>
                                        <span className='date'>{'Owner: '+group.Admin}</span>
                                    </Card.Meta>
                                    </Card.Content>
                                    <Card.Content extra >
                                        <a>
                                        <Icon name='user' />
                                        {group.PlayerCount +'/'+ group.Capacity}
                                        <Icon name='game' />
                                        {group.Game}
                                        </a>
                                   
                                    </Card.Content>
                                </Card>
                    )}
                    </div>
                   }
                </Segment>
            </div>    
        );
    }
}