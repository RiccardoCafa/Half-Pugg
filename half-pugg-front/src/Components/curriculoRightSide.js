import React, { Component } from 'react';
import { Header, Statistic, Segment } from 'semantic-ui-react';

export default class curriculoRightSide extends Component {


    render(){
        return (
            <div style={{display: 'flex', flexDirection: 'column', alignItems: 'center', alignContent: 'center'}}>
                <Header content={`Participação de ${this.props.nickname} em Half-Pugg`}></Header>
                <Statistic.Group horizontal >
                    <Statistic value={this.props.ConnectionsLength} label='conexões'></Statistic>
                    <Statistic value={this.props.stars} label='média da nota'></Statistic>
                </Statistic.Group>
            </div>
        )
    }
}