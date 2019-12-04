import React, { Component } from 'react';
import { Icon, Card, Image, Button, Statistic,Rating } from 'semantic-ui-react';

import GroupsInvite from '../Components/GroupsInvite';
import OpenCurriculum from '../Components/openCurriculum';
import Classification from '../Components/classification';
import api from '../services/api';

class ConnectionCardList extends Component{
    render(){
        return(
            <div style={{display: 'flex', flexDirection: 'row', flexWrap : 'wrap', alignItems: 'left',marginLeft : '2%',marginRight: '2%'}}>
               
                {this.props.Matches.map((matcher) =>
                     <div style={{marginRight: '0.5%',marginLeft:'0.5%', marginTop: '0.5%', marginBottom: '0.5%'}}>
                        <ConnectionCard
                             gamer={this.props.gamer}
                             classificacao = {matcher.Classificacao} 
                             stars = {matcher.Weight}
                             id ={matcher.matchPlayer.ID} 
                             image={matcher.matchPlayer.ImagePath} 
                             nick={matcher.matchPlayer.Nickname} 
                             match={matcher.matchPlayer} 
                             description = {matcher.matchPlayer.Slogan}
                        />
                    </div>
                )}
           
        </div>
        )      
    }
}

class ConnectionCard extends Component{

    render(){
        return(
            <div>
                <Card key={this.props.id} fluid style={{width: '225px',height: '320px'}}>

                      <Image  circular style={{height:'190px'}} src={this.props.image}/>
                      <Card.Content style={{height: '75px'}}>
                         <Card.Header>{this.props.nick}</Card.Header>
                         <Rating rating={this.props.stars} maxRating={5} disabled></Rating>
                         <Card.Description>{this.props.description}</Card.Description>
                     </Card.Content>
                     <Card.Content extra>
                     <div style={{display: 'flex', flexDirection: 'row', justifyContent: 'space-around'}}>
                         <GroupsInvite gamer={this.props.gamer} playerToInvite={this.props.match}></GroupsInvite>
                         <OpenCurriculum {...this.props.match}></OpenCurriculum>
                         <Classification classificacao={this.props.classificacao} gamer={this.props.gamer} gamerclassf={this.props.match}></Classification>
                     </div>   
                     </Card.Content>
                </Card>
            </div>
        )
    }
    
}

export default ConnectionCardList;