import React, {Component} from 'react';

import {List, Image, Segment, Card, Label, Icon, Table} from 'semantic-ui-react';

import api from '../services/api';
import dotaImage from '../images/dota-2.jpg';

import OWCard from '../Components/OWCard';
import DOTACard from '../Components/DOTACard';

class GameView extends Component{
  
    render() {
        return(
            <div style={{display: 'flex', flexDirection: 'row', alignItems: 'left'}}>
                
                
                        {this.props.ShowOw === true ?<OWCard {...this.props.gamer}/> : null }
                    
                        {this.props.ShowDota === true ? <DOTACard {...this.props.gamer}/>:null}
              
            
            </div>
           
        )
    
    }
}

export default GameView;