import '../App.css';
import React, { Component } from 'react';

const AnswerStyle = {
    width: '300px',
    height: '40px',
    paddingTop: '10px',
    paddingLeft: '20px',
    margin: '10px',
    fontSize: '20px',
    textAlign: 'left',
    borderRadius: '25px',
    backgroundColor: '#ddd',
    display: 'inline-block',
    color: '#242673',
    cursor: 'pointer',
}

class Answer extends Component {

    constructor(props) {
        super(props);
    }

    renderAnswer = (answer) => {
        return (
            <div>
                
            </div>
    )}

    render(){
        return (
            <div style={AnswerStyle} onClick={() => this.props.callback(answer.id)}>{answer.name}</div>
    )}
}



export default Answer;
