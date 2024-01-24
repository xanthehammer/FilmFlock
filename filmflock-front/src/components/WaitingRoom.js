import React, {useState, useEffect} from 'react';
import ReactDOM from 'react-dom/client';
import '../index.css';
import App from '../App';
import reportWebVitals from '../reportWebVitals';
import Header from './Header';
import { Link } from 'react-router-dom';
import axios from 'axios';


function UserListComponent (){
  const [joinedUsers, setJoinedUsers] = useState([]);

  useEffect(() => {
    // Function to fetch the list of joined users
    const fetchJoinedUsers = async () => {
      try {
        const response = await axios.get('http://localhost:5149/api/GetUsers?roomId=FUGG');
        // Update the state with the new list of joined users
        setJoinedUsers(response.data);
      } catch (error) {
        console.error('Error fetching joined users:', error);
      }
    };

    // Fetch joined users every 2 seconds
    const intervalId = setInterval(fetchJoinedUsers, 2000);

    // Clean-up the interval when the component is unmounted
    return () => clearInterval(intervalId);
  }, []); // Empty dependency array to run the effect only once on mount


  return (
    <div>
      <h2>Joined Users:</h2>
      <ul>
        {joinedUsers.map((user, index) => (
          <li key={index}>{user}</li>
        ))}
      </ul>
    </div>
  );

}

function WaitingRoom() {
    return (
      <div>
        <Header/>
        you're stuck now bngulus schmungulus
        <UserListComponent/>
        //** TODO: Change this */
        <Link to="/InputMovies">
            <button>Let's Go!</button>
        </Link>
        <></>
      </div>
    )
}

export default WaitingRoom;