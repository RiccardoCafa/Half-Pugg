import React, { Component } from 'react';
import { Segment, Modal, Button, Header, Card, Icon, Image } from 'semantic-ui-react';
import {withRouter} from 'react-router-dom';
import api from '../services/api';
import MessageBox from './MessageBox';

class GroupsInvite extends Component {

    state = {
        MyGroups: [],
        PlayerToInvite: {},
        Gamer: {},
        ErrorMessage: '',
        titleMessage: '',
        hasError: false,
        open: false,
        loaded: false,
    }

    componentDidMount = async () => {
        try {
            const response = await api.get(`api/PlayerGroups/GetGroups/complete?playerID=${this.props.gamer.ID}`);
            if(response) {
                console.log(response.data);
                this.setState({
                    MyGroups: response.data,
                    loaded: true,
                })
            }
        } catch(error) {
            this.setState({
                loaded: true,
            })
        }
        this.setState({
            Gamer: this.props.gamer,
            PlayerToInvite: this.props.playerToInvite,
        });
    }

    addToGroup = async (group) => {
        try{
            const response = await api.post('api/RequestedGroups', {
                "ID": 0,
                "IdPlayer": this.state.PlayerToInvite.ID,
                "IdGroup": group.ID,
                "Status": "A",
                "IdFilters": 1
            });
            if(response) {
                this.setState({
                    titleMessage: 'Solicitação enviada com suecesso!',
                    ErrorMessage: 'Agora é só esperar ele aceitar...',
                    hasError: true,
                });
            }
        } catch(error) {
            if (error.response) {
                console.log(error.response.data.Message);
                this.setState({
                    titleMessage: 'Ops, algo deu errado',
                    ErrorMessage: error.response.data.Message,
                    hasError: true,
                });
            }
        }
        
    }

    toGroupsPage =()=>this.props.history.push('/MyGroups');
    setOpen =()=>this.setState({open:true});
    setClose =()=>this.setState({open: false});
    closeErrorModal =()=>this.setState({hasError: false});

    render() {
        if(!this.state.loaded){
            return <></>
        }
        return <div>
            {this.state.MyGroups.length === 0 ?
                <Modal trigger={<Icon style={{cursor:'pointer'}} name = 'comments outline'/>} open={this.state.open} onOpen={this.setOpen} onClose={this.setClose}>
                        <Header size='medium' icon='users' style={{'marginLeft': '3%'}} content='Ops! Você não possui grupos'></Header>
                        <Modal.Content>
                            <Modal.Description>
                            Você pode criar ou fazer parte de um grupo!
                            </Modal.Description>
                        </Modal.Content>
                        <Modal.Actions>
                            <Button basic positive onClick={this.toGroupsPage}>Acessar meus grupos</Button>
                        </Modal.Actions>
                </Modal>
                :   <Modal trigger={<Icon style={{cursor:'pointer'}} name = 'comments outline'/>} open={this.state.open} onOpen={this.setOpen} onClose={this.setClose}>
                        <Header size='medium' icon='users' content='Convide para os grupos que você deseja'></Header>
                        {this.state.MyGroups.map((group) => 
                            <Modal.Content key={group.ID}>
                                <Card>
                                    <Image src={group.SouceImg} fluid wrapped></Image>
                                    <Card.Content>
                                        <Card.Header>{group.Name}</Card.Header>
                                        <Card.Meta>número máximo de jogadores é {group.Capacity}</Card.Meta>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <a onClick={() => this.addToGroup(group)}>
                                            <Icon name='sign-in alternate'/>
                                            Convidar {this.state.PlayerToInvite.Nickname}
                                        </a>
                                    </Card.Content>
                                </Card>
                            </Modal.Content>
                        )}
                        <MessageBox open={this.state.hasError} title={this.state.titleMessage} Message={this.state.ErrorMessage} close={this.closeErrorModal}></MessageBox>
                    </Modal>}
            
        </div>
    }
}

export default withRouter(GroupsInvite);