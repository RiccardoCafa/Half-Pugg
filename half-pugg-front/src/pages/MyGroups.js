import React, {Component} from 'react'
import api from '../services/api'
import Auth from '../Components/auth';
import Headera from '../Components/headera';
import { Card, Image, Button, Icon, Segment,  Statistic, Header, Menu, Modal, Loader, Label, } from 'semantic-ui-react';
import CriarGrupo from '../Components/CriarGrupo';
import getPlayer from '../Components/getPlayer';
import MessageBox from '../Components/MessageBox';

export default class MyConnections extends Component {

    state = {
        Nickname: '',
        GamerMatch: [],
        Gamer: {
            "ID": 0,
        },
        GamerLogado: {},
        Group: [],
        RequestedGroups: [],
        MessageBody: '',
        MessageTitle: '',
        hasMessage: false,
        loaded: false,
        openCreation: false,
        openSolicitacoes: false,
    }

    async componentDidMount() {
        // Pega o usuário a partir do token
        let myData = await getPlayer();

        if(!myData) {
            this.setState({toLogin: true});
            return;
        }

        this.setState({GamerLogado: myData})
        //this.setNickname(myData);
        
        const responseG = await api.get('api/Gamers/GetGroups?id='+myData.ID).catch(error => console.log(error));
        if(responseG){
            this.setState({Group: responseG.data});
        }

        const response = await api.get(`api/RequestedGroupsTo?idPlayer=${myData.ID}`).catch(err => console.log(err));
        if(response) {
            console.log(response.data);
            this.setState({
                RequestedGroups: response.data,
            })
        }


        this.setState({loaded: true});
    }

    acceptGroupRequest = async (group, aceitou) => {
        let status = aceitou ? "T" : "D";
        try {
            const response = await api.put(`api/RequestedGroups/${group.ID}`, {
                "ID": group.ID,
                "IdPlayer": this.state.GamerLogado.ID,
                "IdGroup": group.Group.ID,
                "Status": status,
            });
            if(response) {
                if(status === "T") {
                    this.setState({
                        hasMessage: true,
                        MessageBody: 'Você já pode acessar o grupo ' + group.Group.Name,
                        MessageTitle: 'Tudo certo!'
                    });
                } else {
                    this.setState({
                        hasMessage: true,
                        MessageBody: 'O grupo ' + group.Group.Name + ' foi recusado!',
                        MessageTitle: 'Tudo certo!'
                    });
                }
                let prev = [...this.state.RequestedGroups];
                let index = prev.indexOf(group);
                prev.splice(index, 1);
                this.setState({RequestedGroups: prev});
            }
        } catch(error){
            if(error.response) {
                let message = error.response.data.Message;
                this.setState({
                    hasMessage: true,
                    MessageBody: message,
                    MessageTitle: 'Ops, algo deu errado'
                });
                let prev = [...this.state.RequestedGroups];
                let index = prev.indexOf(group);
                prev.splice(index, 1);
                this.setState({RequestedGroups: prev});
            }
        }
    }

    closeHasMessage = () => this.setState({hasMessage: false});
    openSolicitacoes = () => this.setState({openSolicitacoes: true});
    closeSolicitacoes = () => this.setState({openSolicitacoes: false});
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
                        <Menu.Item onClick={this.openSolicitacoes}>
                            <Icon name='sign-in'/> Verificar Solicitações
                            <Label floating color='teal' >{this.state.RequestedGroups.length}</Label>
                        </Menu.Item>
                    </Menu>
                </div>
                <MessageBox close={this.closeHasMessage} Message={this.state.MessageBody} title={this.state.MessageTitle} open={this.state.hasMessage}></MessageBox>
                <CriarGrupo gamer={this.state.GamerLogado} open={this.state.openCreation} close={this.setCloseCreation}></CriarGrupo>
                <Segment style={{marginLeft: '2%', marginRight: '2%'}}> 
                    <Segment><Header content='Seus Grupos' as='h3' icon='group'></Header></Segment>
                    {this.state.Group.length === 0 ?
                    <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center', marginBottom: '5%'}}>
                        <Statistic.Group>
                            <Statistic
                            value = "Ops! Parece que você não participa de nenhum grupo..."
                            label = "Crie um grupo e e chame seus amigos"
                            text
                            id="sem-conexao-texto"></Statistic>
                        </Statistic.Group>
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
                                        <Icon name='user' />
                                        {group.PlayerCount +'/'+ group.Capacity}
                                        <Icon name='game' />
                                        {group.Game}
                                    </Card.Content>
                                </Card>
                    )}
                    </div>
                   }
                </Segment>
                <Modal open={this.state.openSolicitacoes} onClose={this.closeSolicitacoes}>
                   <Modal.Header>Suas solicitações</Modal.Header>
                   <Modal.Content>
                        {this.state.RequestedGroups.map((group) =>
                        <Card key={group.ID}>
                            <Image src={group.Group.SouceImg} size='medium'></Image>
                            <Card.Content>
                                <Card.Header>{group.Group.Name}</Card.Header>
                            </Card.Content>
                            <Card.Content extra>
                                <Icon name='group'></Icon>
                                Capacidade de pessoas {group.Group.Capacity}
                            </Card.Content>
                            <Card.Content extra>
                                <Button.Group fluid>
                                    <Button basic color='green' content='Aceitar' onClick={() => this.acceptGroupRequest(group, true)}></Button>
                                    <Button.Or text='Ou'/>
                                    <Button content='Recusar' basic color='red' onClick={() => this.acceptGroupRequest(group, false)}></Button>
                                </Button.Group>
                            </Card.Content>
                        </Card>
                        )}
                   </Modal.Content>
                </Modal>
            </div>    
        );
    }
}