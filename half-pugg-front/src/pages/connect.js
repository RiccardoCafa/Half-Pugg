import React, { Component, createRef } from 'react';

import './connect.css';
import {Header, Icon, Image, Menu, Segment, Sidebar} from 'semantic-ui-react'

export default function({history}) {
    
    return (
            <div className = "connect-container">
                <div className="connect-title">
                    <h1>Half Pugg</h1>
                </div>
                <div className="connect-title">
                    <h2>Conectar</h2>
                </div>  
                <div class="ui divider"></div>
                    <div class="ui doubling stackable three cards">
                        <div class="ui card">
                            <img src="/images/avatar/large/helen.jpg" class="ui image" />
                            <div class="content">
                            <div class="header">Helen</div>
                            <div class="meta">Joined in 2013</div>
                            <div class="description">Primary Contact</div>
                            </div>
                            <div class="extra content">
                            <button class="ui primary button">connect</button>
                            <button class="ui button">Forget it</button>
                            </div>
                        </div>
                        <div class="ui card">
                            <img src="/images/avatar/large/matthew.png" class="ui image" />
                            <div class="content">
                            <div class="header">Matthew</div>
                            <div class="meta">Joined in 2013</div>
                            <div class="description">Primary Contact</div>
                            </div>
                            <div class="extra content">
                            <button class="ui primary button">connect</button>
                            <button class="ui button">Forget it</button>
                            </div>
                        </div>
                        <div class="ui card">
                            <img src="/images/avatar/large/molly.png" class="ui image" />
                            <div class="content">
                            <div class="header">Molly</div>
                            <div class="meta">Joined in 2013</div>
                            <div class="description">Primary Contact</div>
                            </div>
                            <div class="extra content">
                            <button class="ui primary button">connect</button>
                            <button class="ui button">Forget it</button>
                            </div>
                        </div>
                </div>       
            </div>
        );
}