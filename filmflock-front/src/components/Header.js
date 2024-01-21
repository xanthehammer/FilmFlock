import React, {useState} from 'react';
import ReactDOM from 'react-dom/client';
import '../index.css';
import App from '../App';
import reportWebVitals from '../reportWebVitals';
import logo from '../assets/film.png';
import { Link } from 'react-router-dom';

function Header() {
    return (
      <div>
        <img src={logo} alt="Film Flock Logo" width={250}/>
      </div>
    )
  }

export default Header;