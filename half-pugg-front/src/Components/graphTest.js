import React, { Component } from 'react';
import { ReactCytoscape, cytoscape } from 'react-cytoscape';
import './graphStyle.css'

class graphTest extends Component {
    
    state = {
        graph:{ 
            nodes: [],
            edges: [],
        }
    }

    componentDidMount = () => {
        this.setState({graph: this.props.graph});
    }

	getElements() {
		return {
			nodes: [
				{ data: { id: 'a' } },
				{ data: { id: 'b' } },
				{ data: { id: 'c' } },
				{ data: { id: 'd' } },
				{ data: { id: 'e' } },
				{ data: { id: 'f' } }
			],
			edges: [
				{ data: { id: 'ad', source: 'a', target: 'd' } },
				{ data: { id: 'eb', source: 'e', target: 'b' } }
			]
		};
	}

	render() {

		return (
            <ReactCytoscape containerID="cy"
                elements={this.state.graph}
                cyRef={(cy) => { this.cyRef(cy) }}
                cytoscapeOptions={{ wheelSensitivity: 0.1 }}
                layout={{ name: 'dagre' }} />
		);
	}

	cyRef(cy) {
		this.cy = cy;
		cy.on('tap', 'node', function (evt) {
            var node = evt.target;
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

export default graphTest;