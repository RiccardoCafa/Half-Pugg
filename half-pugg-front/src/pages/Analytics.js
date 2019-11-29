import React, {Component} from 'react';

import Headera from '../Components/headera';
import getPlayer from '../Components/getPlayer';

import CanvasJSReact from '../services/canvasjs.react'
import { Segment, Grid, Header, Card, Button, Image, List } from 'semantic-ui-react';
import OpenCurriculum from '../Components/openCurriculum';
import gostosao from '../images/chris.jpg';
import api from '../services/api';
const CanvasJS = CanvasJSReact.CanvasJS;
const CanvasJSChart = CanvasJSReact.CanvasJSChart;


export default class Analytics extends Component {

    state = {
        Gamer: {},
        matchesData: [],
        topTenPlayers: [],
    }

    componentDidMount = async () =>{
        let GamerData = await getPlayer();
        if(!GamerData) {
            this.props.history.push('/');
        } else {
            this.setState({Gamer: GamerData});
        }

        const response = await api.get('api/GetAnalytics?userId=' + GamerData.ID);

        if(response){
            console.log(response.data);
            let analytic = response.data;
            let data = [];
            let count = 0;
            analytic.MatchesDate.map((match) => {
                count++;
                data.push({x: new Date(match.dataMatch), y: count, label: match.PlayerName})
            });
            this.setState({matchesData: data, topTenPlayers: analytic.TopTenPlayers});
        }
    }

    handleClickCurriculo = (playername) => {
        this.props.history.push({
            pathname: `/curriculo/${playername}`,
        });
    }

    render() {
        const options = {
            axisX:{
                title: "Tempo"
            },
            axisY:{
                title: "Matches"
            },
            data: [{				
                type: "area",
                dataPoints: this.state.matchesData
             }]
         }
        return (
            <div>
                <Headera gamer = {this.state.Gamer}></Headera>
                <Segment style={{'marginLeft': '2%', 'marginRight': '2%'}}>
                    <Header as='h2' icon='chart line' content='Player Analytics' dividing/>
                    <Grid columns={2} divided>
                        <Grid.Row style={{'marginTop': '1%'}}>
                            <Grid.Column width={7} style={{'marginLeft': '1%', 'marginRight': '1%', 'marginBottom': '3%'}}>
                                <Segment textAlign='center'><Header as='h2' textAlign='center' icon='users' content='Conexões vs Tempo'/></Segment>
                                <CanvasJSChart options={options}/>
                            </Grid.Column>
                            <Grid.Column width={7} style={{'marginLeft': '2%', 'marginRight': '1%', 'marginBottom': '3%'}}>
                                <Segment textAlign='center'><Header as='h2' textAlign='center' icon='users' content='Top 10 Players nas suas conexões'/></Segment>
                                <List relaxed animated divided verticalAlign='middle' style={{'marginLeft': '5%'}}>
                                    {this.state.topTenPlayers.map((playerFound) => 
                                        <List.Item size='tiny' key={playerFound.ID} >
                                            <Image avatar
                                                floated='left'
                                                src={(playerFound.ImagePath === "" || playerFound.ImagePath === null) 
                                                ? gostosao : playerFound.ImagePath}
                                                />
                                            <List.Content>
                                                <List.Header>{playerFound.Nickname}</List.Header>
                                                <List.Description>Top 10 Players <a onClick={() => this.handleClickCurriculo(playerFound.Nickname)}><b>acesse o currículo.</b></a></List.Description>
                                            </List.Content>
                                        </List.Item>
                                    )}
                                </List>
                            </Grid.Column>
                        </Grid.Row>
                    </Grid>
                </Segment>
                <Segment style={{'marginLeft': '2%', 'marginRight': '2%'}}>
                    <Header as='h2' icon='chart line' content='Network Analytics' dividing/>
                    <Grid columns={2} divided>
                        <Grid.Row style={{'marginTop': '2%'}}>
                            <Grid.Column width={7} style={{'marginLeft': '1%', 'marginRight': '1%', 'marginTop': '1%', 'marginBottom': '3%'}}>
                                
                            </Grid.Column>
                            <Grid.Column width={7} style={{'marginLeft': '2%', 'marginRight': '1%', 'marginTop': '1%', 'marginBottom': '3%'}}>
                                
                            </Grid.Column>
                        </Grid.Row>
                    </Grid>
                </Segment>
            </div>

        )
    }

}