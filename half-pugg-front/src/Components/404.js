import React, {Component} from 'react';
import { Header, Icon } from 'semantic-ui-react';

export default class NotFound extends Component{

    render() {
        return(
            <div>
                <Header icon as='h1' textAlign='center' style={{'marginTop': '10%', 'marginBottom': '10%'}}>
                    <Icon name='eye slash'></Icon>
                    Ops não encontramos o que estava procurando
                    <Header.Subheader>
                        Talvez você tenha se perdido no caminho...
                    </Header.Subheader>
                </Header>
            </div>
        );
    }
}