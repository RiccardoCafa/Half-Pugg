import React from 'react'
import { BrowserRouter, Route } from 'react-router-dom'

import Login from './pages/login'
import register from './pages/register'

export default function () {
    return (
        <BrowserRouter>
            <Route path="/" exact component={Login}/>
            <Route path="/register" component={register}/>
        </BrowserRouter>
    );
}
