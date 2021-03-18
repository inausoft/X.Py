import './App.css';
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import React, { Component } from 'react';
import axios from 'axios';
import AppBar from './views/appBar';
import RegistrationView from './views/registrationView';
import TokenView from './views/tokenView';
import * as consts from './config';

class App extends Component {

  constructor(props) {
    super(props);
    this.state = {
        id : null,
        players : null,
      };
    }

  componentDidMount() {
    
    const connection = new HubConnectionBuilder()
    .withUrl(consts.X_PY_API_ADDRESS + "/quiz")
    .configureLogging(LogLevel.Information)
    .build();

    connection.on('PlayersCountChnaged', data => {
      this.setState({ players: data });
    });

    connection.start()
    .catch(err => console.log('Error while establishing connection :(' + err));
  }

  register = (value) => {
    axios.post(consts.X_PY_API_ADDRESS + "/api/game/",
      { name: value }
    ).then(res => {
          this.setState({ id: res.data.token });
      });
  }

  renderActionPanel = () => {
    if(this.state.id == null){
      return <RegistrationView callBack={this.register}/>
    }
    else{
      return <TokenView token={this.state.id}/>
    }
  }

  render(){

      return (
        <div className="App">
          <AppBar playersCount={this.state.players}/>
          <img src="../logo256.png" alt="logo" />
        
          { this.renderActionPanel() }

        </div>
      );
  }
}



export default App;
