import React, {Component} from 'react';
import {Input,Divider, Button, Loader, Table, Dropdown, Card, Grid, Image, Segment } from 'semantic-ui-react';
import OpenCurriculum from './openCurriculum';

import api from '../services/api';

import gostosao from '../images/chris.jpg';

export default class MatchList extends Component {

    state = {
        OWFilter: false,
        OWF: {
            "role": -2,
            "level": [-2, -2],
            "rating": [-2, -2],
            "damage": [-2,-2],
            "healing": [-2,-2],
            "elimination": [-2,-2],
            "competitve": false,
        },
        owfType: [
            {
                key: 1,
                text: 'Maior que',
                value: 1
            },
            {
                key: 2,
                text: 'Menor que',
                value: 2
            },
            {
                key: 3,
                text: 'Entre dois valores',
                value: 3,
            },
            {
                key: 4,
                text: 'Igual que',
                value: 4,
            }
        ], 
        loadingFilter: false,
        tiposProcura: [
            {
                key: 1,
                text: 'Pessoas Aleatórias',
                value: 'Pessoas Aleatórias'
            },
            {
                key: 2,
                text: 'Jogo',
                value: 'Jogo',
            },
            {
                key: 3,
                text: 'Conhecidos',
                value: 'Conhecidos',
            },
            {
                key: 4,
                text: 'Interesse',
                value: 'Interesse'
            },
            {
                key: 5,
                text: 'Por Nickname',
                value: 'Por Nickname'
            }
        ],
        tipoSelecionado: Number,
        gameSelected: -1,
        gamesToSelect: [
            {
                key: 1,
                text: 'Overwatch',
                value: 'Overwatch'
            }
        ],
        searchDelegate: Function,
        typeSearch: 2,
        NicknameToFind: '',
        GamerMatch: []
    }

    componentDidMount = async() => {
        await this.getPlayersToRec();
    }

    defaultFunction = () => {
        console.log('selecione uma opção do filtro')
    }

    getPlayersToRec = async() => {
        this.setState({loadingFilter: true});
        const jwt = localStorage.getItem("jwt");
        const MatchData = await api.get('api/GamersMatch?RecType=' + this.state.typeSearch, { headers: { "token-jwt": jwt }});
        if(MatchData.data != null){
            this.setState({GamerMatch: MatchData.data, loadingFilter: false});
        }
    }

    // Faz uma requisição de match para outro gamer
    connectMatch = (matcher) => {
        console.log(matcher);
        console.log(this.props.GamerLogado);
        const response = api.post('api/RequestedMatches', {
            "IdPlayer1": this.props.GamerLogado.ID,
            "IdPlayer2": matcher.playerFound.ID,
            "Status": "A",
            "IdFilters": 1
        })
        .catch(error => 
            console.log(error)
        );
        
        if(response !== null) {
            var array = [...this.state.GamerMatch];
            var index = array.indexOf(matcher);
            if(index !== -1) {
                array.splice(index, 1);
                this.setState({GamerMatch: array});
            }
        }
    }

    // Remove um gamer da lista de sugestões de match
    desconnectMatch = (matcher) => {
        console.log(matcher);
        var array = [...this.state.GamerMatch];
        console.log(this.state.GamerMatch);
        var index = array.indexOf(matcher);
        if(index !== -1) {
            console.log('removendo');
            array.splice(index, 1);
            this.setState({GamerMatch: array});
        }
    }
    
    // Seta o filtro para a busca
    openGamersByFilter = () => {
        console.log(this.state.OWF);
        this.setState({loadingFilter: true});
        api.post('api/Overwatch/PostFilterPlayerRec?PlayerID=' + this.props.GamerLogado.ID, {
            "role": this.state.OWF.role,
            "level": [this.state.OWF.level[0], this.state.OWF.level[1]],
            "rating": [this.state.OWF.rating[0], this.state.OWF.rating[1]],
            "damage":[this.state.OWF.damage[0], this.state.OWF.damage[1]],
            "elimination": [this.state.OWF.elimination[0], this.state.OWF.elimination[1]],
            "competitve": false

        })
        .then( res => this.setState({GamerMatch: res.data, loadingFilter: false})).catch(err => console.log(err.message));
    }

    //busca players por nickname e nome completo parecidos
    searchForPlayer = async (nickname) => {
        await api.get('api/Gamers?nickname='+nickname).then(res =>{
            this.setState({GamerMatch: res.data})
        }).catch(err => console.log(err.message));
    }

    // Filtros do Overwatch
    openOWFiltro = () => this.setState({OWFilter: !this.state.OWFilter});

    applyFiltroSearch = (eve, {value}) => {
        console.log(value);
    }

    setFilterType = (e, {value}) => {
        console.log(value);
    }

    changeFilter = (e, {value}) => {
        this.removeSelection();
        let key = this.state.tiposProcura.filter(function(item){
            return item.value == value
        });
        this.setState({tipoSelecionado: key[0].key});
        this.handleSelection(key[0].key)
    }

    removeSelection = () => {
        switch(this.state.tipoSelecionado){
            case 2:
                // jogo
                this.setState({gameSelected: -1});
            break;
        }
        this.setState({searchDelegate: this.defaultFunction});
    }

    handleSelection = (key) => {
        switch(key){
            case 1:
                // pessoas aleatorias
                this.setState({searchDelegate: this.getPlayersToRec, typeSearch: 2});
            break;
            case 2:
                // jogo
                this.setState({gameSelected: 0, searchDelegate: this.openGamersByFilter});
            break;
            case 3:
                // conhecidos
                this.setState({searchDelegate: this.getPlayersToRec, typeSearch: 1});
            break;
            case 4: 
                // interesses
            break;
            case 5:
                this.setState({searchDelegate: this.getPlayerByNickname});
            break;
        }
    }



    getPlayerByNickname = async () => {
        if(this.state.NicknameToFind === '') {
            return; // todo tratar
        }
        this.setState({loadingFilter: true});
        const response = await api.get('api/Gamers?nickname=' + this.state.NicknameToFind);
        if(response){
            this.setState({GamerMatch: response.data, loadingFilter: false});
        }
    }

    setNicknameToFind = (e) => this.setState({NicknameToFind: e.target.value});

    setGameFilter = () => {
        this.setState({gameSelected: 1});
    }
      
    //#region filtro ow
    setRole = (role) => {
        if(role === '') {
            role = -2;
        }
        this.setState( prevOWF => ({
            OWF: {
                ...prevOWF.OWF,
                "role": role,
            }
        }))
    }

    setLevel = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.OWF}
        owf.level[ind] = level;
        this.setState({
            OWF: owf,
        })
    }

    setDamage = (val, ind) =>{
        if(val === '') { val = -2; }
        let owf = {...this.state.OWF};
        owf.damage[ind] = val;
        this.setState({
            OWF: owf,
        })
    }

    setHealing = (val, ind) => {
        if(val === '') { val = -2; }
        let owf = {...this.state.OWF};
        owf.healing[ind] = val;
        this.setState({
            OWF: owf,
        })
    }

    setElimination= (val, ind) => {
        if(val === ''){ val = -2; }
        let owf = {...this.state.OWF};
        owf.elimination[ind] = val;
        this.setState({
            OWF: owf,
        });
    }

    setRating = (val, ind) => {
        if(val === ''){ val = -2; }
        let owf = {...this.state.OWF};
        owf.rating[ind] = val;
        this.setState({
            OWF: owf,
        })
    }
    //#endregion

    render() {
        return (
            <div>
                <Segment>
                    <Grid columns={2} celled='internally' stackable>
                        <Grid.Column width={12}>
                        <Card.Group>
                            {this.state.GamerMatch.map((matcher) => 
                                <Card key={matcher.playerFound.ID} >
                                    <Card.Content>
                                        <Image
                                            floated='right'
                                            avatar
                                            src={(matcher.playerFound.ImagePath === "" || matcher.playerFound.ImagePath === null) 
                                                ? gostosao : matcher.playerFound.ImagePath}
                                            />
                                        <Card.Header>{matcher.playerFound.Nickname}</Card.Header>
                                        <Card.Meta>Sugestão de {matcher.PlayerRecName}</Card.Meta>
                                        <Card.Description><b>Moto de vida</b> <br></br>
                                                            {matcher.playerFound.Slogan === null ?
                                                            "Esse cara não possui..." : 
                                                            matcher.playerFound.Slogan}</Card.Description>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <div className='ui two buttons'>
                                            <Button 
                                                id='btn-acpden' 
                                                basic color='green' 
                                                onClick={() => this.connectMatch(matcher)}
                                                content='Connect!'
                                                />
                                            <Button 
                                                id='btn-acpden' 
                                                basic color='red' 
                                                onClick={() => this.desconnectMatch(matcher)}
                                                content='Not Interested!'
                                                />
                                        </div>
                                    </Card.Content>
                                    <Card.Content extra>
                                        <OpenCurriculum {...matcher.playerFound}></OpenCurriculum>
                                    </Card.Content>
                                </Card>
                            )}
                        </Card.Group>
                        </Grid.Column>
                        <Grid.Column width={3}>
                            Filtro <br/><br/>
                            <Grid>
                                <Grid.Column width={15}>
                                    <Dropdown 
                                        icon='filter' 
                                        placeholder='Tipo de Procura'
                                        floating labeled
                                        options={this.state.tiposProcura}
                                        onChange={this.changeFilter}
                                    ></Dropdown>
                                </Grid.Column>
                            </Grid>
                            <br></br>
                            {this.state.gameSelected === -1 ? null :
                            <div>
                            <Dropdown options={this.state.gamesToSelect} placeholder='Jogo para pesquisa' floating labeled onChange={this.setGameFilter}></Dropdown>
                            {this.state.gameSelected === 1 ?
                                <div>
                                    <Table celled>
                                        <Table.Header>
                                            <Table.Row>
                                            <Table.HeaderCell>Filtro</Table.HeaderCell>
                                            <Table.HeaderCell>Valores</Table.HeaderCell>
                                            </Table.Row>
                                        </Table.Header>
                                        <Table.Body>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Papel
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input  placeholder='role' 
                                                        value={this.state.OWF.role >= 0 ? this.state.OWF.role.toString(2) : ''} 
                                                        onChange={e => this.setRole(e.target.value)}
                                                        size='mini'>
                                                        </Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Level
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Dropdown options={this.state.owfType} placeholder='Tipo de filtro' 
                                                    onChange={this.setFilterType} />
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.level[0] >= 0 ? this.state.OWF.level[0].toString(2) : ''} 
                                                        onChange={e => this.setLevel(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.level[1] >= 0 ? this.state.OWF.level[1].toString(2) : ''} 
                                                        onChange={e => this.setLevel(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>

                                            <Table.Row>
                                                <Table.Cell>
                                                    Rating
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.rating[0] >= 0 ? this.state.OWF.rating[0].toString(2) : ''} 
                                                        onChange={e => this.setRating(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.rating[1] >= 0 ? this.state.OWF.rating[1].toString(2):''} 
                                                        onChange={e => this.setRating(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>

                                            <Table.Row>
                                                <Table.Cell>
                                                    Damage
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.damage[0] >= 0 ? this.state.OWF.damage[0].toString(2):''} 
                                                        onChange={e => this.setDamage(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.damage[1] >= 0 ? this.state.OWF.damage[1].toString(2):''} 
                                                        onChange={e => this.setDamage(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            
                                            <Table.Row>
                                                <Table.Cell>
                                                    Healing
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.healing[0] >= 0 ? this.state.OWF.healing[0].toString(2):''} 
                                                        onChange={e => this.setHealing(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.healing[1] >= 0 ? this.state.OWF.healing[1].toString(2):''} 
                                                        onChange={e => this.setHealing(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            
                                            <Table.Row>
                                                <Table.Cell>
                                                    Elimination
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.OWF.elimination[0]  >= 0 ? this.state.OWF.elimination[0].toString(2):''} 
                                                        onChange={e => this.setElimination(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.OWF.elimination[1] >= 0 ? this.state.OWF.elimination[1].toString(2):''} 
                                                        onChange={e => this.setElimination(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                        </Table.Body>
                                    </Table>
                                </div> : null}
                            </div>
                            }
                            {this.state.tipoSelecionado === 5 ?
                            <Input placeholder='Nickname' onChange={this.setNicknameToFind} label='Nickname'></Input>    
                            :null}
                        <Divider/>
                        {this.state.loadingFilter ?
                        <Loader active inline></Loader>
                        : <Button onClick={this.state.searchDelegate}>Pesquisar!</Button>
                        }
                        </Grid.Column>
                        :<div/>
                    </Grid>
                </Segment>
            </div>
        );
    }
}