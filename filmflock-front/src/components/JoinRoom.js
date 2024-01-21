import React, {useState} from 'react';
import ReactDOM from 'react-dom/client';
import '../index.css';
import App from '../App';
import reportWebVitals from '../reportWebVitals';
import Header from './Header';
import { Link } from 'react-router-dom';

function JoinRoomOptions(){
    return(
        <label>
            <input name="username" />
      </label>
    )
}


function JoinRoom() {
    return (
      <div>
        <Header/>
        <JoinRoomOptions/>
      </div>
    )
  }

export default JoinRoom;