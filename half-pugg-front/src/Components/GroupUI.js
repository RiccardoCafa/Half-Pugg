import React, {Component} from 'react';

import {List, Image, Segment, Card, Label, Icon, Table} from 'semantic-ui-react';


class GoupList extends Component{

    render() {
        return(
            <div style={{display: 'flex', flexDirection: 'row', alignItems: 'left',marginLeft : '2%',marginRight: '2%'}}>
               
                    {this.props.groups.map((g)=>
                        <div style={{marginRight: '0.5%',marginLeft:'0.5%'}}>
                            <CardGroup group={g} history = { this.props.history} />
                        </div>
                    )}
                    
              
            </div>
           
        )
    
  }
}
class CardGroup extends Component{

    render(){
        return(
            <div>
                <Card fluid style={{width: '250px'}} key={this.props.group.ID} onClick={()=> this.props.history.push('/group/'+this.props.group.ID)}>
                    <Image src= {this.props.group.ImagePath} wrapped ui={false} />
                    
                    <Card.Content>
                        <Card.Header>{this.props.group.Name}</Card.Header>
                            <Card.Description>
                                {this.props.group.Desc}
                            </Card.Description>
                            <Card.Meta>
                                <span className='date'>{'Owner: '+this.props.group.Admin}</span>
                            </Card.Meta>
                    </Card.Content>

                    <Card.Content extra >
                        <Icon name='user' />
                            {this.props.group.PlayerCount +'/'+ this.props.group.Capacity}
                        <Icon name='game' />
                            {this.props.group.Game}
                    </Card.Content>
                </Card>
            </div>
        )
    }

}
export default GoupList;
