import React, { Component } from 'react';

const TimerStyle = {
    width: '90px',
    height: '50px',
    paddingTop: '5px',
    paddingBottom: '5px',
    paddingLeft: '10px',
    paddingRight: '10px',
    margin: '5px',
    fontSize: '36px',
    textAlign: 'center',
    borderRadius: '20px',
    backgroundColor: '#f30541',
    display: 'inline-block'
}

class Timer extends Component {

   constructor(props) {
      super(props);
   }

   render(){
      if(this.props.time != null){
         return <div style={TimerStyle}> {this.props.time} </div>
      }
      else{
         return <div style={TimerStyle}> --:-- </div>
      }
   }
}

export default Timer;
