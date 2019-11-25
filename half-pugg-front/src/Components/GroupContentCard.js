import React, {Component} from 'react';
import { Card, Image,  Loader } from 'semantic-ui-react';

import ow from '../images/overwatch.jpg'
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

        this.loadCard();
    }

    render() {
        if(!this.state.loadedCard) {
            return <Loader active></Loader>
        }

        return (
            <Card>
                <Card.Content>
                    <Image
                    floated="right"
                    size="small"
                    src= {ow}
                    />
                    <Card.Header></Card.Header>
                    <Card.Meta>Game : Overwatch</Card.Meta>
                </Card.Content>
                <Card.Content extra>
                    <div className="ui two buttons">
                    <Button basic color="green">
                        Ingress
                    </Button>
                    <Button basic color="red">
                        Not Interested
                    </Button>
                    </div>
                </Card.Content>
            </Card>
        );
    }
}