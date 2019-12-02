import React, {Component} from 'react';

import {List, Checkbox, Image, Segment, Card, Grid, Header, Label, Icon, Statistic, Divider, Table} from 'semantic-ui-react';

import api from '../services/api';
import overwatchImage from '../images/overwatch.jpg';

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
        await api.get('api/Overwatch/GetPlayers?PlayerID=' + this.state.Gamer.ID).then(res =>
            this.setState({OWGamer: res.data})
        ).catch(err => console.log('jogador não possui conta overwatch cadastrada!'));
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
                        <Image src={overwatchImage} fluid style={{height:'150px'}} />
                        <Card.Content>
                            <Card.Header as='h3' content='Overwatch'></Card.Header>
                            <Card.Meta>{this.state.OWGamer.profile.name}</Card.Meta>
                            <Card.Description>
                                <List>
                                    <List.Item>
                                        <List.Content><Label icon='user circle' basic content={`Level ${owLevel}`}></Label></List.Content>
                                    </List.Item>
                                    <b>Competitve Season 2019</b>
                                    <List.Item>
                                        <List.Content>
                                            <Label
                                            icon='trophy' basic 
                                            content={`Rating ${this.state.OWGamer.profile.rating === 0 ? 'não possui' : this.state.OWGamer.profile.rating}`} >
                                            </Label>
                                        </List.Content>
                                    </List.Item>
                                    <List.Item>
                                        <List.Content>
                                            <Label
                                            icon='shield' basic
                                            content={`Tank Rating ${this.state.OWGamer.profile.tank_rating === -1 ? 'não possui' : this.state.OWGamer.profile.tank_rating}`} >
                                            </Label>
                                        </List.Content>
                                    </List.Item>
                                    <List.Item>
                                        <List.Content>
                                            <Label
                                            icon='crosshairs' basic
                                            content={`Damage Rating ${this.state.OWGamer.profile.damage_rating === -1 ? 'não possui' : this.state.OWGamer.profile.damage_rating}`} >
                                            </Label>
                                        </List.Content>
                                    </List.Item>
                                    <List.Item>
                                        <List.Content>
                                            <Label
                                            icon='plus' basic
                                            content={`Support Rating ${this.state.OWGamer.profile.support_rating === -1 ? 'não possui' : this.state.OWGamer.profile.support_rating}`} >
                                            </Label>
                                        </List.Content>
                                    </List.Item>
                                </List>
                            </Card.Description>
                        </Card.Content>
                        <Card.Content extra>
                            {this.state.OWGamer.quickCareer !== undefined ?
                                <div>
                                <a onClick={() => this.handleQuickCareerCollapse(!quickCareerCollapse)}>
                                    <Icon name='bolt'></Icon>
                                    Quick Play
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
                                                <Table.Cell><Icon name='schlix' /> All Damage done</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.allDamageDone}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='shield'/>Barrier Damage Done</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.barrierDamageDone}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='x'/>Deaths</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.deaths}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='crosshairs'/>Eliminations</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.eliminations}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='certificate'/>Final Blows</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.finalBlows}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='heart'/>Healing Done</Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.healingDone}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='universal access'/>Hero Damage Done </Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.heroDamageDone}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='fire'/>Objective Kills </Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.objectiveKills}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='time'/>Objective Time </Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.objectiveTime}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='chess king'/> Solo Kills </Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.soloKills}</Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell><Icon name='fire'/> Time Spent On Fire </Table.Cell>
                                                <Table.Cell>{this.state.OWGamer.quickCareer.timeSpentOnFire}</Table.Cell>
                                            </Table.Row>
                                        </Table.Body>
                                    </Table>
                                    
                                    </div> : <div/>}</div>
                            : <div />}
                        </Card.Content>
                        <Card.Content extra>
                            {this.state.OWGamer.compCareer !== undefined ?
                                    <div>
                                    <a onClick={() => this.handleCareerCollapse(!compCareerCollapse)}>
                                        <Icon name='chess rook'></Icon>
                                        Competitve
                                    </a>
                                    {compCareerCollapse === true ?
                                    <Table textAlign='center'> 
                                        <Table.Header>
                                            <Table.Row>
                                                <Table.HeaderCell>Atributo</Table.HeaderCell>
                                                <Table.HeaderCell>Valor</Table.HeaderCell>
                                            </Table.Row>
                                        </Table.Header>

                                        <Table.Body>
                                            <Table.Row>
                                                    <Table.Cell><Icon name='schlix' /> All Damage done</Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.allDamageDone}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='shield'/>Barrier Damage Done</Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.barrierDamageDone}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='x'/>Deaths</Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.deaths}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='crosshairs'/>Eliminations</Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.eliminations}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='certificate'/>Final Blows</Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.finalBlows}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='heart'/>Healing Done</Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.healingDone}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='universal access'/>Hero Damage Done </Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.heroDamageDone}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='fire'/>Objective Kills </Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.objectiveKills}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='time'/>Objective Time </Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.objectiveTime}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='chess king'/> Solo Kills </Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.soloKills}</Table.Cell>
                                                </Table.Row>
                                                <Table.Row>
                                                    <Table.Cell><Icon name='fire'/> Time Spent On Fire </Table.Cell>
                                                    <Table.Cell>{this.state.OWGamer.compCareer.timeSpentOnFire}</Table.Cell>
                                                </Table.Row>
                                        </Table.Body>
                                    </Table> : <div/>}</div>
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