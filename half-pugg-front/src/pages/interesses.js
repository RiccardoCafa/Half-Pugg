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

    const hs = await api.get('api/HashPlayer?IdHash='+ 3+'&IdPlayer='+this.state.GamerLogado.ID
            ).catch(error => {
                console.log(error);
                
            });
            if (hs == undefined){
                await api.post('api/PlayerHashtags', { 
                    "IdHash": 5,
                    "idPlayer": this.state.GamerLogado.ID,
                    "Weight": 1
                }).catch(error => {
                    console.log(error);
                    
                });
            }

            
        console.log(hs);    
}
        
    handleInt = async(operation, id)=> {
        console.log(hs);
        const hs = await api.get('api/HashPlayer?IdHash='+ id+'&IdPlayer='+this.state.GamerLogado.ID
            ).catch(error => {
                console.log(error);
                
            });
        console.log(hs)
        //console.log(this.state.GamerLogado.ID)
        if (hs == undefined){
            await api.post('api/PlayerHashtags', { 
                "IdHash": id,
                "idPlayer": 1,
                "Weight": 1
            }).catch(error => {
                console.log(error);
                
            });
        }else{
            var x = 1 - hs.Weight;
            if (operation){
                x = 1 + hs.Weight
            }
            await api.put('api/PlayerHashtags/'+hs.ID, { 
                "IdHash" : id,
                "idPlayer" : this.state.GamerLogado.ID,
                "Weight" : 1 + hs.Weight
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
                                        <Button icon='plus'  onClick={this.handleInt.bind(true, tag.ID_Matter)}  />
                                        <Button icon='minus' onClick={this.handleInt.bind(false, tag.ID_Matter)}/>
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