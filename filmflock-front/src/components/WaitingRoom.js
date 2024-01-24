import React, {useState} from 'react';
import ReactDOM from 'react-dom/client';
import '../index.css';
import App from '../App';
import reportWebVitals from '../reportWebVitals';
import Header from './Header';
import { Link } from 'react-router-dom';

function WaitingRoomOptions(){
    return(
        <label>
            <input name="username" />
      </label>
    )
}


function WaitingRoom() {
    return (
      <div>
        <Header/>
        you're stuck now bngulus schmungulus
        <WaitingRoomOptions/>
      </div>
    )
  }

export default WaitingRoom;