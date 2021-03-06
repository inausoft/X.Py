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
      axios.get(consts.X_PY_API_ADDRESS + '/api/game/' + this.props.match.params.id)
         .then(res => {
            this.setState({ 
               player: res.data.players.find(it => it.id === this.props.match.params.id),
               players : res.data.players,
               playersCount: res.data.players.length,
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
                    axios.get(consts.X_PY_API_ADDRESS + "/api/game/" + this.props.match.params.id)
                    .then(res => {
                        this.setState({ 
                           players : res.data.players,
                           player: res.data.players.find(it => it.id === this.props.match.params.id),
                            playersCount: res.data.players.length,
                            activeQuestion: res.data.activeQuestion,
                        });
                        if(this.state.gameState == 1){
                            this.setState({ 
                            LastAnswerRecord: null
                            });
                        }
                    });
                }
            }
        });

        connection.start();
    }

    sendAnswerRequest = (answerId) => {
        axios.post(consts.X_PY_API_ADDRESS + "/api/game/answer",
        { 
            playerToken : this.props.match.params.id,
            answerId : answerId
        })
        .then(res => {
           if(this.state.LastAnswerRecord === null){
            this.setState({ 
               LastAnswerRecord : answerId //res.data
            });
           }
             
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
               <QuestionView question={this.state.activeQuestion} callback={this.sendAnswerRequest} answerId={this.state.LastAnswerRecord}/>
            </div>
         )
      }
      else if(this.state.gameState === 4){
         return <div style={TextStyle} > Czekamy na quiz </div>
      }
      else if(this.state.gameState === 3){
         return(
                <Standings players={this.state.players.sort((a,b) => b.score - a.score)}/>
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
