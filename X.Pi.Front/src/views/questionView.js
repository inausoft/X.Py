import '../App.css';
import React, { Component } from 'react';

const QuestionStyle = {
   fontSize: "26px",
   maxWidth: "500px",
   display: "inline-block",
   marginBottom: '20px',
   marginTop: '20px'
}

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

const AnswerTextStyle = {
   width: "270px",
   display: "inline-block"
}

const AnswerNumberStyle = {
   display: "inline-block"
}

class QuestionView extends Component {

   constructor(props) {
      super(props);
   }

   renderAnswer = (answer) => {
      let style = JSON.parse(JSON.stringify(AnswerStyle));
        
      if(this.props.selectedAnswer != null){
         if(this.props.selectedAnswer.answerId == answer.id){
            style.color = "#ddd";
            style.backgroundColor = "#8b56f4";
         }
      }

        if(answer.isCorrect){
            style.backgroundColor = "#188000";
            style.color = "#ddd";
        }

        return (
            <div>
                <div style={style} onClick={() => this.props.callback(answer.id)}>
                    <div style={AnswerTextStyle}>
                        {answer.text}
                    </div>
                    {this.renderAnswerCount(answer)}
                </div>
            </div>
    )}

    renderAnswerCount = (answer) => {
        if(answer.responsesCount > 0){
            return <div style={AnswerNumberStyle}>
            {answer.responsesCount}
            </div>
        }
    }

    render(){
        return (
            <div>
                <div style={QuestionStyle}> {this.props.question.text} </div>
                {this.props.question.answers.map((answer =>
                        this.renderAnswer(answer)
                    ))}
            </div>
    )}
}



export default QuestionView;
