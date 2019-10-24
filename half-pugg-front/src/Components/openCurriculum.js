import React, {Component} from 'react';
import { Modal, Header, Button, Image } from 'semantic-ui-react';
import gostosao from '../images/chris.jpg'

export default class OpenCurriculum extends Component {
    render() {
        return (
            <div>
                <Modal
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
                            <Header>{this.props.matcher.Bio === null ? 'Gamer normal' : this.props.matcher.Bio}</Header>
                        </Modal.Description>
                    </Modal.Content>
                </Modal>
            </div>
        );
    }
}