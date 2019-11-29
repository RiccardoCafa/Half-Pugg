import React, {Component} from 'react';

import NotFound from '../Components/404';
import { Loader, Segment, Grid, Image, Statistic, Button, Icon, Container, Rating, Menu } from 'semantic-ui-react';
import api from '../services/api';
import OWCard from '../Components/OWCard';
import gostosao from '../images/chris.jpg'
import getPlayer from '../Components/getPlayer';
import NLHeader from '../Components/NLHeader';
import Headera from '../Components/headera';

export default class curriculoSpecific extends Component {

    state = {
        GamerCurriculo: {},
        OverwatchInfo: {},
        GamerLogado: {},
        ConnectionsLenght: 0,
        stars: 0,
        loadedOW: false,
        loading: false,
        notfound: false,
        notLogged: false,
    }

    componentDidMount = async () => {
        let g = await getPlayer();
        if(g) {
            this.setState({GamerLogado: g});
            console.log(g);
        } else {
            this.setState({notLogged: true});
        }

        this.setState({loading: true});
        const response = await api.get(`api/GetGamerByNickname?nickname=${this.props.match.params.player}`).catch(() =>
        this.setState({notfound: true, loading: false}));
        if(response){
            let myData = response.data;
            this.setState({GamerCurriculo: myData});
            let CurriculoData = await api.get('api/Curriculo?GamerID=' + myData.ID);
            if(CurriculoData !== null) {
                if(CurriculoData.data.OverwatchInfo !== undefined) {
                    this.setState({
                        OverwatchInfo: CurriculoData.data.OverwatchInfo
                        , loadedOW: true
                    });
                }
                this.setState({ConnectionsLength: CurriculoData.data.ConnectionsLenght, stars: CurriculoData.data.Stars});
            }
        }
        this.setState({loading: false});
    }

    render() {
        console.log(this.props.match.params.player);
        if(this.state.notfound === true) {
            return (
                <NotFound></NotFound>
            );
        }
        else if(this.state.loading === true) {
            return (
                <div>
                    <Loader active></Loader>
                </div>
            )
        }
        return(
            <div>
                {this.state.notLogged ?
                <NLHeader></NLHeader> : <Headera gamer = {this.state.GamerLogado}/>
                }
                <Segment>
                    <Grid columns={1} relaxed='very' centered stackable>
                        <Grid.Column width={10}>
                            <div className="main-content">
                            <div className="ui container">
                                    <h2 className="ui icon center aligned header">
                                        <Image circular aria-hidden="true" 
                                            src={this.state.GamerCurriculo.ImagePath === null ? 
                                                    gostosao : this.state.GamerCurriculo.ImagePath}></Image>
                                        <div className="content">{this.state.GamerCurriculo.Nickname}</div>
                                        <div id='realname' className="content">{this.state.GamerCurriculo.Name} {this.state.GamerCurriculo.LastName}</div>
                                    </h2>
                                    <div className='space'>
                                    <Rating rating={this.state.stars} maxRating={5} disabled></Rating>
                                    </div>
                                    <div className='space'> 
                                    <div className="ui message">
                                        <div className="header">Meu grito de guerra</div>
                                        <ul className="list">
                                            <Container textAlign='center'><i>{this.state.GamerCurriculo.Slogan}</i></Container>
                                            <br/>
                                        </ul>
                                        <div className="header">História que cantam</div>
                                        <ul className="list">
                                            <Container textAlign="center"><i>{this.state.GamerCurriculo.Bio}</i></Container>
                                            <br/>
                                        </ul>
                                    </div>
                                    </div>
                            </div>
                            <div>
                                {this.state.OverwatchInfo !== null && this.state.OverwatchInfo.profile !== undefined ?
                                <OWCard {...this.state.GamerCurriculo}></OWCard>
                                : <Loader/>}
                                <div className="ui segment dimmable">
                                    <h3 className="ui header">League of legends</h3>
                                    <div className="ui small ui small images images">
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image> 
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                        <Image src="https://react.semantic-ui.com/images/wireframe/image.png" className="ui image"> </Image>
                                    </div>
                                    <Image
                                        src="https://react.semantic-ui.com/images/wireframe/media-paragraph.png"
                                        className="ui medium image"
                                    />
                                </div>
                                </div>
                            </div>
                        </Grid.Column>
                        <Grid.Column width={1} id='coluna-3'>
                            <Statistic value={this.state.ConnectionsLength} label='conexoes'></Statistic>
                        </Grid.Column>
                    </Grid>
                </Segment>
            </div>
        );
    }
}