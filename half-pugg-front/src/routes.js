import React from 'react'
import { BrowserRouter, Route } from 'react-router-dom'

import Login from './pages/login'
import register from './pages/register'
import register2 from './pages/register2'
import curriculoT from './pages/curriculoT'
import connect from './pages/connect'
import registergame from '.pages/registergame'

export default function () {
    return (
        <BrowserRouter>
            <Route path="/" exact component={Login}/>
            <Route path="/register" component={register}/>
            <Route path="/bio" component={register2}/>
            <Route path="/curriculo" component={curriculoT}/>
            <Route path="/connect" component={connect}/>
            <Route path="/registergame" component={registergame}/>
        </BrowserRouter>
    );
}
