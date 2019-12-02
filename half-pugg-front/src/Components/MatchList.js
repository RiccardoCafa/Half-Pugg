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
        DOTA: {
            "kills": [-2, -2],
            "deaths": [-2, -2],
            "assists": [-2, -2],
            "xp_per_min": [-2, -2],
            "gold_per_min": [-2, -2],
            "hero_damage": [-2, -2],
            "tower_damage": [-2, -2],
            "hero_healing": [-2, -2],
            "last_hits": [-2, -2],
            "mmr_estimate": [-2, -2],
            "competitive_rank": [-2, -2],
            "rank_tier": [-2, -2]
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
            },
            {
                key: 2,
                text: 'Dota 2',
                value: 'Dota 2'
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
    openGamersByFilter = async () => {
        console.log(this.state.OWF);
        this.setState({loadingFilter: true});
        if(this.state.gameSelected === 1){
            await api.post('api/Overwatch/PostFilterPlayerRec?PlayerID=' + this.props.GamerLogado.ID, 
            {
                "role": this.state.OWF.role,
                "level": [this.state.OWF.level[0], this.state.OWF.level[1]],
                "rating": [this.state.OWF.rating[0], this.state.OWF.rating[1]],
                "damage":[this.state.OWF.damage[0], this.state.OWF.damage[1]],
                "elimination": [this.state.OWF.elimination[0], this.state.OWF.elimination[1]],
                "competitve": false
            })
            .then( res => this.setState({GamerMatch: res.data})).catch(err => alert('voce não possui esse jogo cadastrado'));
        } else if(this.state.gameSelected === 2){
            await api.get('api/Dota/GetFilterPlayer?PlayerID='+ this.props.GamerLogado.ID, {
                "kills": [this.state.DOTA.kills[0], this.state.DOTA.kills[1]],
                "deaths": [this.state.DOTA.deaths[0], this.state.DOTA.deaths[1]],
                "assists": [this.state.DOTA.assists[0], this.state.DOTA.assists[1]],
                "xp_per_min": [this.state.DOTA.xp_per_min[0], this.state.DOTA.xp_per_min[1]],
                "gold_per_min": [this.state.DOTA.gold_per_min[0], this.state.DOTA.gold_per_min[1]],
                "hero_damage": [this.state.DOTA.hero_damage[0], this.state.DOTA.hero_damage[1]],
                "tower_damage": [this.state.DOTA.tower_damage[0], this.state.DOTA.tower_damage[1]],
                "hero_healing": [this.state.DOTA.hero_healing[0], this.state.DOTA.hero_healing[1]],
                "last_hits": [this.state.DOTA.last_hits[0], this.state.DOTA.last_hits[1]],
                "mmr_estimate": [this.state.DOTA.mmr_estimate[0], this.state.DOTA.mmr_estimate[1]],
                "competitive_rank": [this.state.DOTA.competitive_rank[0], this.state.DOTA.competitive_rank[1]],
                "rank_tier": [this.state.DOTA.rank_tier[0], this.state.DOTA.rank_tier[1]]
            }).then(res => this.setState({GamerMatch: res.data})).catch(err => alert('voce não possui esse jogo cadastrado'));
        }

        this.setState({loadingFilter: false});
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
            return item.value === value
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
            default:
                this.setState({gameSelected: -1});
            return;
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
            case 5:
                this.setState({searchDelegate: this.getPlayerByNickname});
            break;
            default:
                this.setState({searchDelegate: this.defaultFunction});
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

    setGameFilter = (e, {value}) => {
        let key = this.state.gamesToSelect.filter(function(item){
            return item.value === value
        });
        this.setState({gameSelected: key[0].key});
    }
      
    //#region filtro dota
    setDeaths = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.deaths[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }
    setAssists = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.assists[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }

    setKills = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.kills[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }

    setXpPerMin = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.xp_per_min[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }

    setGoldPerMin = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.gold_per_min[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }
    setHeroDamage = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.hero_damage[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }
    setTowerDamage = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.tower_damage[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }
    setHeroHealing = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.hero_healing[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }
    setLastHits = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.last_hits[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }
    setMMR = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.mmr_estimate[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }
    setCompetitiveRank = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.competitive_rank[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }
    setRankTier = (level, ind) => {
        if(level === ''){ level = -2; }
        let owf = {...this.state.DOTA}
        owf.rank_tier[ind] = level;
        this.setState({
            DOTA: owf,
        })
    }
    //#endregion

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
                                {this.state.gameSelected === 2 ?
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
                                                    Kills
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.kills[0] >= 0 ? this.state.DOTA.kills[0].toString(2) : ''} 
                                                        onChange={e => this.setKills(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.kills[1] >= 0 ? this.state.DOTA.kills[1].toString(2) : ''} 
                                                        onChange={e => this.setKills(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Deaths
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.deaths[0] >= 0 ? this.state.DOTA.deaths[0].toString(2) : ''} 
                                                        onChange={e => this.setDeaths(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.deaths[1] >= 0 ? this.state.DOTA.deaths[1].toString(2) : ''} 
                                                        onChange={e => this.setDeaths(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Assists
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.assists[0] >= 0 ? this.state.DOTA.assists[0].toString(2) : ''} 
                                                        onChange={e => this.setAssists(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.assists[1] >= 0 ? this.state.DOTA.assists[1].toString(2) : ''} 
                                                        onChange={e => this.setAssists(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    XP/min
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.xp_per_min[0] >= 0 ? this.state.DOTA.xp_per_min[0].toString(2) : ''} 
                                                        onChange={e => this.setXpPerMin(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.xp_per_min[1] >= 0 ? this.state.DOTA.xp_per_min[1].toString(2) : ''} 
                                                        onChange={e => this.setXpPerMin(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Gold/Min
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.gold_per_min[0] >= 0 ? this.state.DOTA.gold_per_min[0].toString(2) : ''} 
                                                        onChange={e => this.setGoldPerMin(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.gold_per_min[1] >= 0 ? this.state.DOTA.gold_per_min[1].toString(2) : ''} 
                                                        onChange={e => this.setGoldPerMin(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Hero Damage
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.hero_damage[0] >= 0 ? this.state.DOTA.hero_damage[0].toString(2) : ''} 
                                                        onChange={e => this.setHeroDamage(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.hero_damage[1] >= 0 ? this.state.DOTA.hero_damage[1].toString(2) : ''} 
                                                        onChange={e => this.setHeroDamage(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Tower Damage
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.tower_damage[0] >= 0 ? this.state.DOTA.tower_damage[0].toString(2) : ''} 
                                                        onChange={e => this.setTowerDamage(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.tower_damage[1] >= 0 ? this.state.DOTA.tower_damage[1].toString(2) : ''} 
                                                        onChange={e => this.setTowerDamage(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Hero Healing
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.hero_healing[0] >= 0 ? this.state.DOTA.hero_healing[0].toString(2) : ''} 
                                                        onChange={e => this.setHeroHealing(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.hero_healing[1] >= 0 ? this.state.DOTA.hero_healing[1].toString(2) : ''} 
                                                        onChange={e => this.setHeroHealing(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Last Hits
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.last_hits[0] >= 0 ? this.state.DOTA.last_hits[0].toString(2) : ''} 
                                                        onChange={e => this.setLastHits(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.last_hits[1] >= 0 ? this.state.DOTA.last_hits[1].toString(2) : ''} 
                                                        onChange={e => this.setLastHits(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    MMR
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.mmr_estimate[0] >= 0 ? this.state.DOTA.mmr_estimate[0].toString(2) : ''} 
                                                        onChange={e => this.setMMR(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.mmr_estimate[1] >= 0 ? this.state.DOTA.mmr_estimate[1].toString(2) : ''} 
                                                        onChange={e => this.setMMR(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Competitive Rank
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.competitive_rank[0] >= 0 ? this.state.DOTA.competitive_rank[0].toString(2) : ''} 
                                                        onChange={e => this.setCompetitiveRank(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.competitive_rank[1] >= 0 ? this.state.DOTA.competitive_rank[1].toString(2) : ''} 
                                                        onChange={e => this.setCompetitiveRank(e.target.value, 1)} size='mini' ></Input>
                                                </Table.Cell>
                                            </Table.Row>
                                            <Table.Row>
                                                <Table.Cell>
                                                    Rank Tier
                                                </Table.Cell>
                                                <Table.Cell>
                                                    <Input placeholder='menor que' 
                                                        value={this.state.DOTA.rank_tier[0] >= 0 ? this.state.DOTA.rank_tier[0].toString(2) : ''} 
                                                        onChange={e => this.setRankTier(e.target.value, 0)} size='mini'></Input>
                                                    <Input placeholder='maior que' 
                                                        value={this.state.DOTA.rank_tier[1] >= 0 ? this.state.DOTA.rank_tier[1].toString(2) : ''} 
                                                        onChange={e => this.setRankTier(e.target.value, 1)} size='mini' ></Input>
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
                        <div/>
                    </Grid>
                </Segment>
            </div>
        );
    }
}