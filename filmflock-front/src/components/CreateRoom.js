import React, {useState} from 'react';
import ReactDOM from 'react-dom/client';
import '../index.css';
import App from '../App';
import reportWebVitals from '../reportWebVitals';
import Header from './Header';
import axios from 'axios';
import { Link, redirect, Navigate } from 'react-router-dom';

function CreateRoomOptions(){

  const [moveToWaiting, setMoveToWaiting] = useState(false);
  
  function handleClick() {

    axios.post('http://localhost:5149/api/CreateRoom', {"FilmSelectionMethod": 0, "PerUserFilmLimit": 3}, { headers: {'Content-Type': 'application/json'}, withCredentials: true});

    setMoveToWaiting(true);
  }

  return (
    <div>
      <input name="userName" />
      <input name="maxMovies" />
      <button onClick={handleClick}>Let's Go!</button>
      {moveToWaiting && (<Navigate to="/waitingRoom" replace={true} />)}
    </div>
  );
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