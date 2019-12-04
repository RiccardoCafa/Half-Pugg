import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Image,  Segment, Icon, Header, Dimmer,Grid,  List } from 'semantic-ui-react';
import Headera from '../Components/headera';
import './interesses.css';
import Auth from '../Components/auth';  
import getPlayer from '../Components/getPlayer';    
import api from '../services/api';
import gostosao from '../images/chris.jpg';

export default class interesses extends Component {

    state = {
        slogan: '',
        descricao: '',
        MyImage: '',
        Gamer: {},
        toLogin: false,
        RPGlike: false,
        MOBAlike: false,
        FPSlike: false,
        Nickname: '',
        GamerLogado: {},
        Hashtags :[],
        ID : 0, 
        operation : false
        
    }


async componentDidMount() {
    let gamer = await getPlayer();
    if(!gamer){
        this.setState({toLogin: true});
        return;
    }
    
    this.setState({
        GamerLogado: gamer,
        Nickname: gamer.Nickname,
        loaded: true,
    });
    console.log(this.state.GamerLogado);

    const hash = await api.get('api/Hashtags')
    if (hash !== null){
        this.setState({Hashtags : hash.data});
        
    }
}
    setId = (tag)=>{
        this.setState({ID: tag.ID_Matter })
    }
    setOpAdd = ()=>{
        this.setState({operation: true})
    }

    setOpMinus = ()=>{
        this.setState({operation: false})
    }
    
    handleInt = async()=> {
        console.log(hs);
        const hs = await api.get('api/HashPlayer?IdHash='+ this.state.ID+'&IdPlayer='+this.state.GamerLogado.ID
            ).catch(error => {
                console.log(error);
                
            });
        console.log(hs)
        //console.log(this.state.GamerLogado.ID)
        if (hs == null){
            await api.post('api/HashPlayer', { 
                "IdHash": this.state.ID,
                "idPlayer": this.state.GamerLogado.ID,
                "Weight": 1
            }).catch(error => {
                console.log(error);
                
            });
        }else{
            var x = 1 - hs.Weight;
            if (hs){
                x = 1 + hs.Weight
            }
            await api.put('api/HashPlayer/'+hs.ID, { 
                "IdHash": this.state.ID,
                "idPlayer": this.state.GamerLogado.ID,
                "Weight": 1 + hs.Weight
            }).catch(error => {
                console.log(error);
                
            });
        }
    }

 
render() {
    if(this.state.toLogin) {
        return <Redirect to ='/'></Redirect>
    }
    const { active } = this.state
    return (
        
         <div>       
            <Auth></Auth>
            <div>
                <Headera gamer = {this.state.GamerLogado }/>
            </div>
                
            <Segment style={{'marginLeft': '1%', 'marginRight': '0%', 'marginBottom': '1%'}}>
                
                <form> 
                    
                    <div >
                    <Grid columns={3} divided style={{'marginTop': '1%', 'marginLeft': '1%'}}> 
                            <Grid.Row>
                            <Grid.Column>
                            {this.state.Hashtags.map((tag) => 
                                    
                                    <Dimmer.Dimmable as={Segment} dimmed={active} size='tiny'>
                                    <Header as='h3'>{tag.Hashtag}</Header>
                                    <Image size='tiny' src={(tag.PathImg === "" || tag.PathImg === null) 
                                    ? gostosao : tag.PathImg} />
                                    <Button.Group>
                                    <Button icon='plus'  onClick = {this.setId.bind(tag)} onClick = {this.setOpAdd} onClick={this.handleInt}  />
                                    <Button icon='minus' onClick = {this.setId.bind(tag)} onClick = {this.setOpMinus} onClick={this.handleInt}/>
                                    </Button.Group>
                                    </Dimmer.Dimmable>

                        )}
                            
                            </Grid.Column>
                            </Grid.Row>   
                        </Grid>
                        
                    </div>
                    <hr id='divider'></hr>
                </form>
            </Segment>
        </div> 
    );
}
}