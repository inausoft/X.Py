import '../App.css';
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import React, { Component } from 'react';
import axios from 'axios';
import AppBar from './appBar';
import PlayerView from './playerView';
import QuestionView from './questionView';
import Grid from '@material-ui/core/Grid';
import Standings from './standings';
import * as consts from '../config';
import Timer from '../Components/timer'

const TextStyle = {
    fontSize: "34px"
}

const AppBarStyle = {
    paddingLeft: '20px',
    paddingRight: '20px'
}

class GameView extends Component {

   constructor(props) {
      super(props);
      this.state = {
         playersCount : 0,
         timeLeft : null,
         gameState : 0,
         player: null,
         activeQuestion: null,
         LastAnswerRecord : null,
         standings: null
      };
   }

   updateGameState = () => {
      axios.get(consts.X_PY_API_ADDRESS + '/api/quiz/' + this.props.match.params.id)
         .then(res => {
            this.setState({ 
               player: res.data.player,
               activeQuestion: res.data.activeQuestion,
               gameState : res.data.state 
            });
      });
   }
    
    componentDidMount() {
        this.updateGameState();

        const connection = new HubConnectionBuilder()
        .withUrl(consts.X_PY_API_ADDRESS + "/quiz")
        .configureLogging(LogLevel.Information)
        .build();

        connection.on('PlayersCountChnaged', data => {
            this.setState({ playersCount: data });
        });

        connection.on('UpdateQuizState', data => {
            let gameState = this.state.gameState;
            this.setState(
            { 
                timeLeft: data.timeLeft,
                gameState: data.state
            });
        
            if(gameState != this.state.gameState){

                if(this.state.gameState != 3){
                    axios.get(consts.X_PY_API_ADDRESS + "/api/quiz/" + this.props.match.params.id)
                    .then(res => {
                        this.setState({ 
                            player: res.data.player,
                            activeQuestion: res.data.activeQuestion,
                        });
                        if(this.state.gameState == 1){
                            this.setState({ 
                            LastAnswerRecord: null
                            });
                        }
                    });
                }
                else if(this.state.gameState == 3){
                    axios.get(consts.X_PY_API_ADDRESS + "/api/players/")
                    .then(res => {
                        this.setState({ 
                            standings: res.data,
                        });
                    });
                }
            }
        });

        connection.start();
    }

    sendAnswerRequest = (answerId) => {
        axios.post(consts.X_PY_API_ADDRESS + "/api/players/answer",
        { 
            playerToken : this.props.match.params.id,
            answerId : answerId
        })
        .then(res => {
            this.setState({ 
                LastAnswerRecord : res.data
            });
        })
    }

   renderQuiz = () => {
      if(this.state.gameState === 0){
         return(
            <div>
               <Timer time={this.state.timeLeft}/>
               <div style={TextStyle} >Zaraz zaczynamy! </div>
            </div>
         );    
      }
      else if((this.state.gameState === 1 || this.state.gameState === 2) && this.state.activeQuestion != null){
         return (
            <div>
               <Timer time={this.state.timeLeft}/>
               <QuestionView question={this.state.activeQuestion} callback={this.sendAnswerRequest} selectedAnswer={this.state.LastAnswerRecord}/>
            </div>
         )
      }
      else if(this.state.gameState === 4){
         return <div style={TextStyle} > Czekamy na quiz </div>
      }
      else if(this.state.gameState === 3 && this.state.standings != null){
         return(
                <Standings players={this.state.standings}/>
              );
      }
      else{
         return <div/>
      }
    }


  render(){

      return (
        <div className="App">
            <AppBar playersCount={this.state.playersCount}/>
            <Grid container spacing={0} style={AppBarStyle}>
                <Grid item xs={3}>
                    <PlayerView player={this.state.player} />
                </Grid>
                <Grid item xs={6}>
                    <div>
                        <img src="../logo256.png" alt="logo" />
                    </div>
                    
                    {this.renderQuiz()}
                </Grid>
                <Grid item xs={3}/>
            </Grid>
        </div>
      );
  }
}



export default GameView;
