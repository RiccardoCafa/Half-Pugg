import React from 'react'
import { BrowserRouter, Route } from 'react-router-dom'

import Login from './pages/login'
import register from './pages/register'
import register2 from './pages/register2'

export default function () {
    return (
        <BrowserRouter>
            <Route path="/" exact component={Login}/>
            <Route path="/register" component={register}/>
            <Route path="/bio" component={register2}/>
        </BrowserRouter>
    );
}
