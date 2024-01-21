import React, {useState} from 'react';
import Header from './Header';
import { Link } from 'react-router-dom';

function StartOptions() {
  
    return (
      <div id='startMenu'>
 
        <Link to="/CreateRoom">
            <button>Create Room</button>
        </Link>



        <Link to="/JoinRoom">
            <button>Join Room</button>
        </Link>
     
      </div>
    )
  }
  
  function Start(){
  
    return (
  
        <div>
          <Header />
          <StartOptions/>
        </div>
    )
  
  }

  export default Start;