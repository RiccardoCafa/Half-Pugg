import React, {Component} from 'react';

import {List, Checkbox} from 'semantic-ui-react';

class OWCard extends Component {
    state = {
        OWGamer: {},
        compCareerCollapse: false,
        quickCareerCollapse: false,
    }
    constructor(owgamer){
        super();
        this.state.OWGamer = owgamer;
        console.log(owgamer);
    }

    handleQuickCareerCollapse = (ligado) => this.setState({quickCareerCollapse: ligado});
    handleCareerCollapse = (ligado) => this.setState({compCareerCollapse: ligado});

    render() {
        const { compCareerCollapse } = this.state;
        const { quickCareerCollapse } = this.state;

        let owLevel = 1;
        if(this.state.OWGamer.profile !== undefined){
            owLevel = this.state.OWGamer.profile.endorsement * 100 + this.state.OWGamer.profile.level;
        }

        return (
            <div>
                {this.state.OWGamer.profile !== undefined ?
                <div className="ui segment dimmable">
                    <h3 className="ui header">Overwatch</h3>
                    <div>
                        <div className="header">Nome: {this.state.OWGamer.profile.name}</div>
                        <div className="header">level: {owLevel}</div>
                        <div className="header">prestige: {this.state.OWGamer.profile.prestige}</div>
                        <div className="header">rating: {this.state.OWGamer.profile.rating}</div>
                        <div className="header">tank_rating: {this.state.OWGamer.profile.tank_rating}</div>
                        <div className="header">damage_rating: {this.state.OWGamer.profile.damage_rating}</div>
                        <div className="header">support_rating: {this.state.OWGamer.profile.support_rating}</div>
                    </div>
                    {this.state.OWGamer.quickCareer !== undefined ?
                        <div><Checkbox
                            label='quick career'
                            onChange={() => this.handleQuickCareerCollapse(!quickCareerCollapse)}
                        />
                        {quickCareerCollapse === true ?
                        <List>
                            <List.Item>
                                <List.Content>All Damage done = {this.state.OWGamer.quickCareer.allDamageDone}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Barrier Damage Done = {this.state.OWGamer.quickCareer.barrierDamageDone}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Deaths = {this.state.OWGamer.quickCareer.deaths}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Eliminations = {this.state.OWGamer.quickCareer.eliminations}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Final Blows {this.state.OWGamer.quickCareer.finalBlows}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Healing Done {this.state.OWGamer.quickCareer.healingDone}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Hero Damage Done {this.state.OWGamer.quickCareer.heroDamageDone}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Objective Kills {this.state.OWGamer.quickCareer.objectiveKills}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Objective Time {this.state.OWGamer.quickCareer.objectiveTime}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Solo Kills {this.state.OWGamer.quickCareer.soloKills}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Time Spent On Fire {this.state.OWGamer.quickCareer.timeSpentOnFire}</List.Content>
                            </List.Item>
                        </List> : <div/>}</div>
                    : <div />}
                    {this.state.OWGamer.compCareer !== undefined ?
                        <div><Checkbox
                            label='career comp'
                            onChange={() => this.handleCareerCollapse(!compCareerCollapse)}
                        />
                        {compCareerCollapse === true ?
                        <List>
                            <List.Item>
                                <List.Content>All Damage done = {this.state.OWGamer.compCareer.allDamageDone}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Barrier Damage Done = {this.state.OWGamer.compCareer.barrierDamageDone}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Deaths = {this.state.OWGamer.compCareer.deaths}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Eliminations = {this.state.OWGamer.compCareer.eliminations}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Final Blows {this.state.OWGamer.compCareer.finalBlows}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Healing Done {this.state.OWGamer.compCareer.healingDone}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Hero Damage Done {this.state.OWGamer.compCareer.heroDamageDone}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Objective Kills {this.state.OWGamer.compCareer.objectiveKills}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Objective Time {this.state.OWGamer.compCareer.objectiveTime}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Solo Kills {this.state.OWGamer.compCareer.soloKills}</List.Content>
                            </List.Item>
                            <List.Item>
                                <List.Content>Time Spent On Fire {this.state.OWGamer.compCareer.timeSpentOnFire}</List.Content>
                            </List.Item>
                        </List> : <div/>}</div>
                    : <div />}
                </div>
                : <div></div>}
            </div>
        )
    }
}

export default OWCard;