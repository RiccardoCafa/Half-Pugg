import React, {Component} from 'react';

import Headera from '../Components/headera';
import getPlayer from '../Components/getPlayer';

import CanvasJSReact from '../services/canvasjs.react'
import { Segment, Grid, Header, Image, List, Loader } from 'semantic-ui-react';
import gostosao from '../images/chris.jpg';
import api from '../services/api';
import GraphPlayer from '../Components/graphPlayer';
const CanvasJSChart = CanvasJSReact.CanvasJSChart;


export default class Analytics extends Component {

    state = {
        Gamer: {},
        matchesData: [],
        topTenPlayers: [],
        graph: {
            nodes: [],
            edges: [],
        },
        loading: true,
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

        // pegar os dados e pah
        let playergraph = {
            nodes: [],
            edges: []
        }
        const responseGraph = await api.get('api/Analytics/GetPlayersMatch');
        if(responseGraph)
        {
            let count = 0;
            let graphapi = responseGraph.data;
            (graphapi.playerPair).forEach(vertice => {
                //format { data: { id: 'a' } }
                playergraph.nodes.push({ data: { id: vertice.Nickname}});
            });
            graphapi.edgesPair.forEach(edge => {
                //format { data: { id: 'ad', source: 'a', target: 'd' } }
                playergraph.edges.push({ data: { id: count, source: edge.PlayerDe, target: edge.PlayerPara}});
                count++;
            });
            console.log(playergraph);
        }

        this.setState({
            graph: playergraph,
            loading: false,
        });
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
        if(this.state.loading) {
            return <Loader active></Loader>
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
                                        <List.Item size='tiny' key={playerFound.player.ID} >
                                            <Image avatar
                                                floated='left'
                                                src={(playerFound.player.ImagePath === "" || playerFound.player.ImagePath === null) 
                                                ? gostosao : playerFound.player.ImagePath}
                                                />
                                            <List.Content>
                                                <List.Header>{playerFound.player.Nickname}</List.Header>
                                                <List.Description>{'nota: '+playerFound.weight} <a onClick={() => this.handleClickCurriculo(playerFound.player.Nickname)}><b>acesse o currículo.</b></a></List.Description>
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
                        <Grid.Row >
                            <Grid.Column width={7} style={{'marginLeft': '1%', 'marginRight': '1%', 'marginTop': '1%', 'marginBottom': '3%'}}>
                                <Segment textAlign='center'><Header as='h2' textAlign='center' icon='connectdevelop' content='Rede de conexões'></Header></Segment>
                                <GraphPlayer graph={this.state.graph}></GraphPlayer>
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