import React from 'react'
import { BrowserRouter, Route } from 'react-router-dom'

import Login from './pages/login';
import register from './pages/register';
import register2 from './pages/register2';
import match from './pages/match';
import curriculoT from './pages/curriculoT';
import registergame from './pages/registergame';
import MyConnections from './pages/MyConnections';
import interesses from './pages/interesses';
import editcurriculo from './pages/editcurriculo';
import MyGroups from './pages/MyGroups';
import Analytics from './pages/Analytics';
import CurriculoSpecific from './pages/curriculoSpecific';
import Group from './pages/group';

export default function () {
    return (
        <BrowserRouter>
            <Route path="/" exact component={Login}/>
            <Route path="/register" component={register}/>
            <Route path="/bio" component={register2}/>
            <Route path="/match" exact component={match}/>
            <Route exact path="/curriculo" exact component={curriculoT}/>
            <Route path="/registergame" component={registergame}/>
            <Route path="/MyConnections" component={MyConnections}/>
            <Route path="/Interesse" component={interesses}/>
            <Route path="/MyGroups" component={MyGroups}/>
            <Route path="/curriculo/editar" exact component={editcurriculo}/>
            <Route path="/Analytics" component={Analytics}/>
            <Route exact path="/curriculo/:player" render={(props) => <CurriculoSpecific {...props}/>}/>
            <Route exact path="/group/:id" render={(props) => <Group {...props}/>}/>
        </BrowserRouter>
    );
}
