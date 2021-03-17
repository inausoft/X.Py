import '../App.css';
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import React, { Component } from 'react';
import TextField from '@material-ui/core/TextField';
import Grid from '@material-ui/core/Grid';
import axios from 'axios';

class RegistrationView extends Component {

    constructor(props) {
        super(props);
        this.state = {
            name : null
        };
    }

    updateName = (value) => {
        this.setState({ name : value} );
    }

    render(){
        return (
            <div>
                <div>
                    <input class="App-input App-input-general" autocomplete="false" 
                           onChange={event => this.updateName(event.target.value)} type="text" name="name"/>
                </div>
                <div>
                    <div class="App-button App-input-general" onClick={() => this.props.callBack(this.state.name)}> Zarejestruj się! </div>
                </div>
            </div>
      );
    }
}

export default RegistrationView;
