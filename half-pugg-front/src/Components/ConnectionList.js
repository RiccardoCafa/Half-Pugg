import React, { Component } from 'react';
import { Segment, Card, Image, Button, Statistic } from 'semantic-ui-react';
import OpenCurriculum from './openCurriculum';
import gostosao from '../images/chris.jpg';
import api from '../services/api';

export default class ConnectionList extends Component {

    state = {
        RequestedMatches: [],
    }

    componentDidMount = () => {
        console.log(this.props.requestedMatch)
        this.setState({RequestedMatches: this.props.requestedMatch})
    }

    // Atualiza uma requisição de match, podendo ser aceita ou não
    FazMatch = async (deuMatch, gamerMatch) => {
        this.setState({isMatching: true});
        try {
            await api.put('api/RequestedMatches/1', {
                "ID": 1,
                "IdPlayer1": gamerMatch.ID,
                "IdPlayer2": this.props.GamerLogado.ID,
                "Status": deuMatch ? "M" : "F",
                "IdFilters": 1,
            });
    
            var array = [...this.state.RequestedMatches];
            var index = array.indexOf(gamerMatch);
            if(index !== -1) {
                array.splice(index, 1);
                this.setState({
                    RequestedMatches: array,
                    isMatching: true
                });
                this.props.updateRequestes(this.state.RequestedMatches);
            }
        } catch(error) {
            console.log(error);
        }
    }

    render() {
        return (
            <div>
                <Segment>
                        {this.state.RequestedMatches.length === 0 ?
                            <div style={{display: 'flex', alignItems: 'center', flexDirection: 'column', 'marginBottom': '2%'}}>
                                <Statistic.Group>
                                    <Statistic
                                    value = "Oh :( você não possui convites de conexão..."
                                    label = "Experimente conectar-se com mais gamers para que seja encontrado!"
                                    text size='mini'
                                    id="sem-conexao-texto"></Statistic>
                                </Statistic.Group>
                            </div>
                        :
                        <Card.Group style={{'marginLeft': '2%', 'marginTop': '1%', 'marginRight': '2%', 'marginBottom': '2%', 'alignItems': 'horizontal'}}>
                            {this.state.RequestedMatches.map((requests) => 
                                <Card key = {requests.ID} >
                                    <Card.Content>
                                        <Image
                                            floated='right'
                                            size='mini'
                                            circular
                                            src={(requests.ImagePath === "" || requests.ImagePath === null) ? gostosao : requests.ImagePath}
                                            />
                                        <Card.Header>{requests.Nickname}</Card.Header>
                                        <Card.Meta>Sugestão de xXNoobMaster69Xx</Card.Meta>
                                        <Card.Description>Principais Jogos: LOL, Overwatch e WoW. Recomendação de 80%</Card.Description>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <div className='ui two buttons'>
                                            <Button id='btn-acpden' basic color='green' onClick={() => this.FazMatch(true, requests)}>
                                                Accept!
                                            </Button>
                                            <Button id='btn-acpden' basic color='red' onClick={() => this.FazMatch(false, requests)}>
                                                Deny!
                                            </Button>
                                        </div>
                                    </Card.Content> 
                                    <Card.Content extra>
                                        <OpenCurriculum {...requests}></OpenCurriculum>
                                    </Card.Content>
                                </Card>
                            )}
                        </Card.Group>
                        }
                </Segment>
            </div>
        ); 
    }

}