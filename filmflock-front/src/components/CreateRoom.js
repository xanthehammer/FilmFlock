import React, {useState} from 'react';
import ReactDOM from 'react-dom/client';
import '../index.css';
import App from '../App';
import reportWebVitals from '../reportWebVitals';
import Header from './Header';
import axios from 'axios';
import { Link, Navigate } from 'react-router-dom';

function CreateRoomOptions(){

  const [moveToWaiting, setMoveToWaiting] = useState(false);
  
  async function handleClick() {

    const roomId = await axios.post('http://localhost:5149/api/CreateRoom', {"FilmSelectionMethod": 0, "PerUserFilmLimit": 3}, { headers: {'Content-Type': 'application/json'}, withCredentials: true});

    setMoveToWaiting(true);
  }

  return (
    <div>
      <input name="userName" />
      <input name="maxMovies" />
      <button onClick={handleClick}>Let's Go!</button>
      {moveToWaiting && (<Navigate to="/waitingRoom/${roomId}" replace={true} />)}
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