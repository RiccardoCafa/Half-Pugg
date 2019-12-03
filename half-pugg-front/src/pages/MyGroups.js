import React, {Component} from 'react'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import { Card, Image, Button, Icon, Segment,  Statistic, Header, Menu, Modal, Loader, } from 'semantic-ui-react';
import CriarGrupo from '../Components/CriarGrupo';
import GoupList from '../Components/GroupUI';

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
        openCreation: false,
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
        this.setState({loaded: true});
    }

    setOpenCreation = () => this.setState({openCreation: true});
    setCloseCreation = () => this.setState({openCreation: false});

    render() {
        if(!this.state.loaded){
            return <Loader active/>
        }

        return (
            <div>
                <Auth></Auth>
                <div>
                    <Headera gamer = {this.state.GamerLogado}/>
                </div>
                <div className='submenu'>
                    <Menu compact>
                        <Menu.Item onClick={this.setOpenCreation}>
                            <Icon name='edit'/> Criar um grupo
                        </Menu.Item>
                        <Menu.Item >
                            <Icon name='search'/> Procurar um grupo
                        </Menu.Item>
                    </Menu>
                </div>
                <CriarGrupo gamer={this.state.GamerLogado} open={this.state.openCreation} close={this.setCloseCreation}></CriarGrupo>
                <Segment>
                    <Segment><Header content='Seus Grupos' as='h3' icon='group'></Header></Segment>
                    {this.state.Group.length === 0 ?
                    <div style={{display: 'flex', flexDirection: 'line', alignItems: 'left'}}>
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
                    <div>
                       
                        <GoupList groups = {this.state.Group} history = {this.props.history}/>

                      
                    </div>
                   }
                </Segment>
            </div>    
        );
    }
}