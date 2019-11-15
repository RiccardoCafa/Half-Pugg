import React, {Component} from 'react';
import { Modal, Button, Image } from 'semantic-ui-react';
import gostosao from '../images/chris.jpg'
import OWCard from './OWCard';

export default class OpenCurriculum extends Component {
    state ={
        Gamer: {},
    }
    constructor(gamer){
        super();
        this.state.Gamer = gamer;
    }

    render() {
        return (
            <div>
                <Modal
                size='small'
                    trigger={
                        <Button fluid basic color='blue'>
                            Open Curriculum
                        </Button>
                    }>
                    <Modal.Header>
                        {'Gamerlum de ' + this.state.Gamer.Nickname}
                    </Modal.Header>
                    <Modal.Content image scrolling>
                        <Image wrapped size ='small' 
                        src={(this.state.Gamer.ImagePath === "" || this.state.Gamer.ImagePath === null) 
                            ? gostosao : this.state.Gamer.ImagePath}
                        circular/>
                        <Modal.Description>
                            <b>{this.state.Gamer.Slogan}</b><br/><br/>
                            <b>História épica></b><br/>{this.state.Gamer.Bio === null ? 'Gamer normal' : this.state.Gamer.Bio}
                        </Modal.Description>
                    </Modal.Content>
                    <Modal.Content>
                        <OWCard {...this.state.Gamer}></OWCard>
                    </Modal.Content>
                </Modal>
            </div>
        );
    }
}