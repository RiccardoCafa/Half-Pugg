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

export default function () {
    return (
        <BrowserRouter>
            <Route path="/" exact component={Login}/>
            <Route path="/register" component={register}/>
            <Route path="/bio" component={register2}/>
            <Route path="/match" exact component={match}/>
            <Route path="/curriculo" component={curriculoT}/>
            <Route path="/registergame" component={registergame}/>
            <Route path="/MyConnections" component={MyConnections}/>
           <Route path="/Interesse" component={interesses}/>
        </BrowserRouter>
    );
}
