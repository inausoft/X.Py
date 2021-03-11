import '../App.css';
import React, { Component } from 'react';
import Grid from '@material-ui/core/Grid';

const AppBarStyle = {
    padding: '20px',
}

const playersCountStyle = {
    fontSize: "22px"
}

class AppBar extends Component {

    constructor(props) {
        super(props);
    }

    render(){
        return (
            <Grid container spacing={0} style={AppBarStyle}>
                <Grid item xs={3}>
                    <img src="../user20.png" alt="logo" />
                    <span style={playersCountStyle}> {this.props.playersCount} </span>
                </Grid>
          <Grid item xs={6}/>
          <Grid item xs={3}>
          </Grid>
        </Grid>
      );
    }
}

export default AppBar;
