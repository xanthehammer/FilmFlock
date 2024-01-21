import React, {useState} from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import logo from './assets/film.png';

/* const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
); */

function Head() {
  return (
    <div>
      <img src={logo} alt="Film Flock Logo" width={250}/>
    </div>
  )
}

function CreateRoomOptions(){
  return (
    <>
      <ul>
        <li>create room</li>
        <li>create room</li>
      </ul>
    </>
  )
}

function JoinRoomOptions(){

  <>
    <ul>
      <li>join room</li>
      <li>create room</li>
    </ul>
  </>

}

function StartOptions() {

  const [showNewComponent, setShowNewComponent] = useState(false);

  function createRoom(){
    setShowNewComponent(true);
  }

  function joinRoom(){
    root.render(<JoinRoomOptions/>)
  }

  return (
    <div id='startMenu'>
      <button onClick={createRoom}>Create Room</button>
      <button onClick={joinRoom}>Join Room</button>
      {showNewComponent && <CreateRoomOptions />}
    </div>
  )
}

function StartScreen(){

  return (

      <div>
        <Head />
        <StartOptions/>
      </div>
  )

}

const root = ReactDOM.createRoot(document.getElementById("root"));

root.render(<StartScreen />)

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
//reportWebVitals();
