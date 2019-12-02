import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Image,  Segment, Icon, Header, Dimmer } from 'semantic-ui-react';

import './interesses.css';

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
    }
//    handleSubmit(e) {
       
//    }

handleShowRPG = (e) => {
    e.preventDefault();
    this.setState({ RPGlike: true })
}
handleHideRPG = (e) => {
    e.preventDefault();
    this.setState({ RPGlike: false })
}

handleShowMOBA = (y) => {
    y.preventDefault();
    this.setState({ MOBAlike: true })
}
handleHideMOBA = (y) => {
    y.preventDefault();
    this.setState({ MOBAlike: false })
}

handleShowFPS = (x) => {
    x.preventDefault();
    this.setState({ FPSlike: true })
}
handleHideFPS = (x) => {
    x.preventDefault();
    this.setState({ FPSlike: false })
}

 
render() {
    if(this.state.toLogin) {
        return <Redirect to ='/'></Redirect>
    }
    const { active } = this.state
    return (
        <div className = "interesses-container">
            <form> 
                <h1 id='title'>Half Pugg</h1>
                <div className ='caixa'>
                    <Dimmer.Dimmable as={Segment} dimmed={active} size='medium'>
                    <Header as='h3'>RPG</Header>
                    <Image size='medium' src='https://react.semantic-ui.com/images/wireframe/media-paragraph.png' />

                    <Dimmer active={active} onClickOutside={this.handleHide}>
                        <Header as='h2' icon inverted>
                        <Icon name='heart' />
                        Like this!
                        </Header>
                    </Dimmer>
                    </Dimmer.Dimmable>

                    <Button.Group>
                    <Button icon='plus' onClick={e => this.handleShowRPG(e)} />
                    <Button icon='minus' onClick={e => this.handleHideRPG(e)} />
                    </Button.Group>
                </div>

                <div className ='caixa'>
                    <Dimmer.Dimmable as={Segment} dimmed={active} size='medium'>
                    <Header as='h3'>MOBA</Header>
                    <Image size='medium' src='https://react.semantic-ui.com/images/wireframe/media-paragraph.png' />

                    <Dimmer active={active} onClickOutside={this.handleHide}>
                        <Header as='h2' icon inverted>
                        <Icon name='heart' />
                        Like this!
                        </Header>
                    </Dimmer>
                    </Dimmer.Dimmable>

                    <Button.Group>
                    <Button icon='plus' onClick={y => this.handleShowMOBA(y)} />
                    <Button icon='minus' onClick={y => this.handleHideMOBA(y)} />
                    </Button.Group>
                </div>

                <div className ='caixa'>
                    <Dimmer.Dimmable as={Segment} dimmed={active} size='medium'>
                    <Header as='h3'>FPS</Header>
                    <Image size='medium' src='https://react.semantic-ui.com/images/wireframe/media-paragraph.png' />

                    <Dimmer active={active} onClickOutside={this.handleHide}>
                        <Header as='h2' icon inverted>
                        <Icon name='heart' />
                        Like this!
                        </Header>
                    </Dimmer>
                    </Dimmer.Dimmable>

                    <Button.Group>
                    <Button icon='plus' onClick={x => this.handleShowFPS(x)} />
                    <Button icon='minus' onClick={x => this.handleHideFPS(x)} />
                    </Button.Group>
                </div>

                <hr id='divider'></hr>
                <div id="goback">
                    <Button.Group id="botoes">
                        <Button color='blue' onClick={a => this.handleAdicionarButton(a)}>
                            Go back
                        </Button>
                    </Button.Group>
                </div>
                <hr id='divider'></hr>
            </form>
        </div>
    );
}
}