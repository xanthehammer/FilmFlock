import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Start from './components/Start';
import CreateRoom from './components/CreateRoom';
import WaitingRoom from './components/WaitingRoom';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path='/start' element={<Start/>} />
        <Route path='/createRoom' element={<CreateRoom/>} />
        <Route path='/waitingRoom' element={<WaitingRoom/>} />
      </Routes>
    </Router>
  );
};

const root = ReactDOM.createRoot(document.getElementById("root"));

root.render(
    <React.StrictMode>
      <App />
    </React.StrictMode>,
    document.getElementById('root')
  );

export default App;