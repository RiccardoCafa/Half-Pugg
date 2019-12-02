import React, {Component} from 'react';

import {List, Checkbox, Image, Segment, Card, Grid, Header, Label, Icon, Statistic, Divider, Table} from 'semantic-ui-react';

import api from '../services/api';
import dotaImage from '../images/dota-2.jpg';

class OWCard extends Component {
    state = {
        Gamer: {},
        OWGamer: {},
        compCareerCollapse: false,
        quickCareerCollapse: false,
    }
    
    constructor(gamer){
        super();
        this.state.Gamer = gamer; 
    }

    componentDidMount = async () => {
        console.log(this.state.Gamer.ID);
        await api.get('api/Dota/GetPlayers?PlayerID=' + this.state.Gamer.ID).then(res =>
            this.setState({OWGamer: res.data})
        ).catch(err => console.log('jogador não possui conta dota 2 cadastrada!'));
        
    }
    
    componentWillUnmount = () => {}

    handleQuickCareerCollapse = (ligado) => this.setState({quickCareerCollapse: ligado});
    handleCareerCollapse = (ligado) => this.setState({compCareerCollapse: ligado});

    render() {
        const { compCareerCollapse } = this.state;
        const { quickCareerCollapse } = this.state;

        let owLevel = 1;
        if(this.state.OWGamer.profile !== undefined){
            owLevel = this.state.OWGamer.profile.prestige * 100 + this.state.OWGamer.profile.level;
        }

        return (
            <div>
                {this.state.OWGamer.profile !== undefined ?
                <Segment style={{width: '100%', display: 'flex', alignItems: 'center', flexDirection: 'column'}}>
                    <Card fluid style={{width: '40%'}}>
                        <Image src={dotaImage} fluid style={{height:'150px'}} />
                        <Card.Content>
                            <Card.Header as='h3' content='Dota 2'></Card.Header>
                            <Card.Meta><Image avatar src={this.state.OWGamer.profile.avatar}></Image> {this.state.OWGamer.profile.personaname} </Card.Meta>
                            <Card.Description>
                                <List>
                                    <List.Item>
                                        <List.Content><Label icon='trophy' basic content={`Competitive Rank ${this.state.OWGamer.competitive_rank}`}></Label></List.Content>
                                    </List.Item>
                                    <List.Item>
                                        <List.Content>
                                            <Label
                                            icon='trophy' basic 
                                            content={`Solo Competitive Rank ${this.state.OWGamer.solo_competitive_rank === 0 ? 'não possui' : this.state.OWGamer.solo_competitive_rank}`} >
                                            </Label>
                                        </List.Content>
                                    </List.Item>
                                    <List.Item>
                                        <List.Content>
                                            <Label
                                            icon='trophy' basic 
                                            content={`Rank Tier ${this.state.OWGamer.rank_tier === 0 ? 'não possui' : this.state.OWGamer.rank_tier}`} >
                                            </Label>
                                        </List.Content>
                                    </List.Item>
                                    <List.Item>
                                        <List.Content>
                                            <Label
                                            icon='trophy' basic 
                                            content={`MMR ${this.state.OWGamer.mmr_estimate.estimate === 0 ? 'não possui' : this.state.OWGamer.mmr_estimate.estimate}`} >
                                            </Label>
                                        </List.Content>
                                    </List.Item>
                                </List>
                            </Card.Description>
                        </Card.Content>
                        <Card.Content extra>
                            {this.state.OWGamer.stats !== undefined ?
                                <div>
                                <a onClick={() => this.handleQuickCareerCollapse(!quickCareerCollapse)}>
                                    <Icon name='bolt'></Icon>
                                    Gameplay Stats
                                </a>
                                {quickCareerCollapse === true ?
                                <div>
                                    <Table textAlign='center'> 
                                        <Table.Header>
                                            <Table.Row>
                                                <Table.HeaderCell>Atributo</Table.HeaderCell>
                                                <Table.HeaderCell>Valor</Table.HeaderCell>
                                            </Table.Row>
                                        </Table.Header>

                                        <Table.Body>
                                            <Table.Row>
                                                <Table.Cell><Icon name='crosshairs' />Kills</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.stats.kills}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='x' />Deaths</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.stats.deaths}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='compress' />Assists</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.stats.assists}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='clock' />XP/min</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.stats.xp_per_min}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='bitcoin' />Gold/min</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.stats.gold_per_min}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='schlix' />Hero Damage</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.stats.gold_per_min}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='chess rock' />Tower Damage</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.stats.tower_damage}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='plus' />Hero Healing</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.stats.hero_healing}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='deaf' />Last Hits</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.stats.last_hits}</Table.Cell>
                                            </Table.Row>
                                        </Table.Body>
                                    </Table>
                                </div> : <div/>}
                                </div>
                            : <div />}
                        </Card.Content>
                    </Card>
                </Segment>
                : <div></div>}
            </div>
        )
    }
}

export default OWCard;