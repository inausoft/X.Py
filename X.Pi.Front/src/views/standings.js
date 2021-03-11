import '../App.css';
import React, { Component } from 'react';

const TextStyle = {
    fontSize: "26px",
    maxWidth: "500px",
    display: "inline-block",
    marginBottom: '20px',
    marginTop: '20px'
}

const PlayerStyle = {
    width: '200px',
    height: '40px',
    paddingTop: '10px',
    paddingLeft: '20px',
    margin: '10px',
    fontSize: '20px',
    textAlign: 'left',
    borderRadius: '25px',
    backgroundColor: '#ddd',
    float: "left",
    color: '#242673',
}

const testStyle = {
    display: "inline-block"    
}

const testStyle2 = {
    width: '40px',
    height: '40px',
    float: "left",
    borderRadius: '25px',
    backgroundColor: '#ddd',
    paddingTop: '10px',
    margin: '10px',
    color: "#242673",
    fontSize: '20px',
    paddingLeft: "5px",
    paddingRight: "5px"
}

const ScoreStyle = {
    width: '40px',
    height: '40px',
    float: "left",
    borderRadius: '25px',
    backgroundColor: '#ddd',
    padding: '5px',
    margin: '10px',
    color: "#242673",
}

class Standings extends Component {

    constructor(props) {
        super(props);
    }

    renderPlayer = (player, index) => {
        let style = JSON.parse(JSON.stringify(ScoreStyle));
        if(index === 0){
            style.backgroundColor = "#f8ca09"
        }
        else{
            style.backgroundColor = "#dd7346"
        }

        return (
            <div style={testStyle}>
                <div style={style}>
                    {this.renderIcon(index)}
                </div>
                <div style={testStyle2}>
                    {player.score}
                </div>
                <div style={PlayerStyle}>
                        {player.nickname}
                </div>
            </div>
    )}

    renderIcon = (index) => {
        if(index === 0){
            return <img src="../trophy_icon30.png" alt="logo" />
        }
        else{
            return <img src="../medal_icon30.png" alt="logo" />
        }
    }


    render(){
        return (
            <div>
                <div><div style={TextStyle}> Wyniki!! </div></div>
                {this.props.players.map( (player, i) =>
                        this.renderPlayer(player, i)
                    )}
            </div>
    )}
}



export default Standings;
