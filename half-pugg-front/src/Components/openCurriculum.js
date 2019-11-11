import React, {Component} from 'react';
import { Modal, Header, Button, Image } from 'semantic-ui-react';
import gostosao from '../images/chris.jpg'
import OWCard from './OWCard';

export default class OpenCurriculum extends Component {
    state ={
        OWGamer: {},
    }
    constructor(owgamer){
        super();
        this.state.OWGamer = owgamer;
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
                        {'Gamerlum de ' + this.props.matcher.Nickname}
                    </Modal.Header>
                    <Modal.Content image>
                        <Image wrapped size ='small' 
                        src={(this.props.matcher.ImagePath === "" || this.props.matcher.ImagePath === null) 
                            ? gostosao : this.props.matcher.ImagePath}
                        circular/>
                        <Modal.Description>
                            <b>{this.props.matcher.Slogan}</b><br/><br/>
                            <b>História épica></b><br/>{this.props.matcher.Bio === null ? 'Gamer normal' : this.props.matcher.Bio}
                        </Modal.Description>
                        {this.state.OWGamer !== undefined ?
                        <OWCard {...this.state.OWGamer}></OWCard>
                        : <div/>}
                    </Modal.Content>
                </Modal>
            </div>
        );
    }
}