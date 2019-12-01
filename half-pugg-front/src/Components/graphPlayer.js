import React, { Component } from 'react';
import { ReactCytoscape, cytoscape } from 'react-cytoscape';
import './graphStyle.css'
import api from '../services/api';
import {withRouter} from 'react-router-dom';
import { Modal, Button } from 'semantic-ui-react';

class GraphPlayer extends Component {
    
    state = {
        graph:{ 
            nodes: [],
            edges: [],
        },
        playerNicknameTo: '',
        openCurriculoCheck: false,
        modal: false,
    }

    componentDidMount = () => {
        console.log(this.props.graph)
        this.setState({graph: this.props.graph});
    }

    closeCurriculoModal = () => {
        this.setState({
            openCurriculoCheck: false,
            modal: false,
            playerNicknameTo: '',
        })
    }

    openGamerCurriculo = () => {
        this.props.history.push(`/curriculo/${this.state.playerNicknameTo}`);
    }

	render() {

		return (
            <div>
                <ReactCytoscape containerID="cy"
                    elements={this.state.graph}
                    cyRef={(cy) => { this.cyRef(cy) }}
                    cytoscapeOptions={{ wheelSensitivity: 0.1 }}
                    layout={{ name: 'dagre' }} />
                    {this.state.openCurriculoCheck ?
                    <Modal size='mini' open={this.state.modal} onClose={this.closeCurriculoModal} centered basic>
                        <Modal.Header>Deseja abrir currículo de <b>{this.state.playerNicknameTo}</b>?</Modal.Header>
                        <Modal.Actions>
                            <Button content='Sim' positive icon='checkmark' onClick={this.openGamerCurriculo}></Button>
                            <Button content='Não' negative icon='x' onClick={this.closeCurriculoModal}></Button>
                        </Modal.Actions>
                    </Modal>
                    :null}
            </div>
		);
	}

	cyRef = async (cy) => {
		this.cy = cy;
		await cy.on('tap', 'node', async (evt) => {
            var node = evt.target;
            var gamerTo = await api.get(`api/GetGamerByNickname?nickname=${node.id()}`);
            if(gamerTo){
                this.setState({
                    playerNicknameTo: gamerTo.data.Nickname,
                    openCurriculoCheck: true,
                    modal: true,
                })
            }
            console.log(node.id())
        });
        cy.on('tap', 'edge', function(edgevent){
            var edge = edgevent.target;
            console.log(edge.id());
        })
	}

	handleEval() {
		const cy = this.cy;
        const str = this.text.value;
		eval(str);
	}
}

export default withRouter(GraphPlayer);