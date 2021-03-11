import React, { Component } from 'react';

const AppBarStyle = {
    padding: '20px',
}

class PlayerView extends Component {

    constructor(props) {
        super(props);
    }

    render(){
        if(this.props.player != null){
            return (
                <div>
                    <span> {this.props.player.nickname} </span>
                    <span> {this.props.player.score} </span>
                </div>
            );
        }
        else{
            return <div/>
        }
    }
}

export default PlayerView;
