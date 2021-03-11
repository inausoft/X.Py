import '../App.css';
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import React, { Component } from 'react';
import TextField from '@material-ui/core/TextField';
import Grid from '@material-ui/core/Grid';
import axios from 'axios';

const DescriptionStyle= {
    fontSize: "16px",
}

const TextStyle = {
    fontSize: "28px",
    marginBottom: '20px',
    marginTop: '20px'
}

class TokenView extends Component {

    constructor(props) {
        super(props);
    }

    goTo = (link) => {
        window.location.href = '/' + link;
    }

    render(){
        return (
            <div>
                <div style={DescriptionStyle}> To twój token. Możesz zawsze wrócić do gry wpisując go w adresie przeglądarki. </div>
                <div style={TextStyle}> {this.props.token} </div>
                <div class="App-button App-input-general" onClick={() => this.goTo(this.props.token)}> Graj </div>
          </div>
      );
    }
}

export default TokenView;
