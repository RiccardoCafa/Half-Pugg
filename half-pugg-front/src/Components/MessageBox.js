import React, {Component} from 'react';

import {Modal, Button, Header} from 'semantic-ui-react';

export default class MessageBox extends Component {
    
    render() {
        return (
        <Modal open={this.props.open} onClose={this.props.close}>
            <Header size='medium' icon='spy' content={this.props.title}></Header>
            <Modal.Content>
                <Modal.Description>{this.props.Message}</Modal.Description>
            </Modal.Content>
            <Modal.Actions>
                <Button basic color='green' icon='check mark' content='Ah Ok' onClick={this.props.close}></Button>
            </Modal.Actions>
        </Modal>)
    }
}