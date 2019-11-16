import React, {Component} from 'react';
import { Card, Image, Rating, Loader } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg';
import api from '../services/api';
import { promised } from 'q';

export default class UserContentCard extends Component {
    
    state = {
        loadedCard: false,
        jug: false,
        stars: 0
    }

    loadCard = () => this.setState({loadedCard: true});

    componentDidMount = async () => {

        const classif = 
            await api.get(`api/classificationPlayers/Match?idJudge=${this.props.gamerMatch.matchPlayer.ID}&idJudger=${this.props.gamer.ID}`)
            .catch(err => {console.log('.')});
        if(classif !== undefined){
            const cls = classif.data;
            this.setState({
                jug: true, 
                stars: cls.Points
            });
        }

        this.loadCard();
    }

    render() {
        if(!this.state.loadedCard) {
            return <Loader active></Loader>
        }

        return (
            <Card.Content>
                <Image
                    floated='right'
                    size='mini'
                    src={(this.props.gamerMatch.matchPlayer.ImagePath === "" || this.props.gamerMatch.matchPlayer.ImagePath === null) 
                        ? gostosao : this.props.gamerMatch.matchPlayer.ImagePath}
                    />
                <Card.Header>{this.props.gamerMatch.matchPlayer.Nickname}</Card.Header>
                <Card.Description><b>Moto de vida</b> <br></br>
                                    {this.props.gamerMatch.matchPlayer.Slogan === null ?
                                    "Esse cara não possui..." : 
                                    this.props.gamerMatch.matchPlayer.Slogan}</Card.Description>
                <Card.Description>
                    Sua avaliação ->
                    <Rating rating={this.state.stars} maxRating={5} disabled></Rating>
                </Card.Description>
            </Card.Content>
        );
    }
}