import React, {Component} from 'react';
import {withRouter} from 'react-router-dom';
import { Modal, Icon, Image } from 'semantic-ui-react';
import gostosao from '../images/chris.jpg'
import OWCard from './OWCard';
import DOTACard from './DOTACard';
import GameView from '../Components/gameView'
class OpenCurriculum extends Component {
    state ={
        Gamer: {},
    }
    constructor(gamer){
        super();
        this.state.Gamer = gamer;
    }

    componentWillMount = () => {}

    openGamerCurriculumPage = () =>{
        this.props.history.push(`/curriculo/${this.state.Gamer.Nickname}`)
    }

    render() {
        return (
            <div>
                <Modal
                    size='small'
                    trigger={
                        <Icon  style={{cursor:'pointer'}} name = 'address card outline' />
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
                        <GameView ShowOw = {true} ShowDota={true} gamer={this.state.Gamer}/>
                        {/* <OWCard {...this.state.Gamer}></OWCard>
                        <DOTACard {...this.state.Gamer}></DOTACard> */}
                    </Modal.Content>
                    <Modal.Actions>
                    <Icon style={{cursor:'pointer'}} name = 'address card outline' onClick={this.openGamerCurriculumPage}/>
                    </Modal.Actions>
                </Modal>
            </div>
        );
    }
}

export default withRouter(OpenCurriculum);