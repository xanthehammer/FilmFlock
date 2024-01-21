import React, {useState} from 'react';
import ReactDOM from 'react-dom/client';
import '../index.css';
import App from '../App';
import reportWebVitals from '../reportWebVitals';
import Header from './Header';
import { Link } from 'react-router-dom';

function CreateRoomOptions(){
    return(
        <>
        <label>
            <input name="username" placeholder="Name" />
        </label>

        <br/>

        <label>
            <input name="maxFilms" placeholder="Max # of Films"/>
        </label>

        <br/>
        
        {/* TODO: Change this to waiting room */}
        <Link to="/JoinRoom">
            <button>Let's Go!</button>
        </Link>
        
        </>
    )
}


function CreateRoom() {
    return (
      <div>
        <Header/>
        <CreateRoomOptions/>
      </div>
    )
  }

export default CreateRoom;