import React, {Component} from 'react';
import { Card, Image, Rating, Loader } from 'semantic-ui-react';

import gostosao from '../images/chris.jpg';
import api from '../services/api';

export default class UserContentCard extends Component {
    
    state = {
        loadedCard: false,
        jug: false,
        stars: 0,
        loadAvaliation: false,
    }

    loadCard = () => this.setState({loadedCard: true});

    componentDidMount = async () => {

        const classif = 
            await api.get(`api/classificationPlayers/Match?idJudge=${this.props.matchPlayer.ID}&idJudger=${this.props.gamer.ID}`)
            .catch(err => {console.log('.')});
        if(classif !== undefined){
            const cls = classif.data;
            this.setState({
                jug: true, 
                stars: cls.Points
            });
        }
        this.setState({
            loadAvaliation: this.props.isAvaliable
        })

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
                    avatar
                    src={(this.props.matchPlayer.ImagePath === "" || this.props.matchPlayer.ImagePath === null) 
                        ? gostosao : this.props.matchPlayer.ImagePath}
                    />
                <Card.Header>{this.props.matchPlayer.Nickname}</Card.Header>
                <Card.Description><b>Moto de vida</b> <br></br>
                                    {this.props.matchPlayer.Slogan === null ?
                                    "Esse cara não possui..." : 
                                    this.props.matchPlayer.Slogan}</Card.Description>
                {!this.state.loadAvaliation ?
                <div>
                    <Card.Description>
                        Sua avaliação ->
                        <Rating rating={this.state.stars} maxRating={5} disabled></Rating>
                    </Card.Description>
                </div>
                : null}
            </Card.Content>
        );
    }
}