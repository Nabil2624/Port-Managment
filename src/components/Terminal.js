import React from 'react';
import './Terminal.css';

const Terminal = ({ id, type, status, currentShip }) => {
  return (
    <div className={`terminal ${type.toLowerCase()} ${status.toLowerCase()}`}>
      <h3>Terminal {id}</h3>
      <div className="terminal-info">
        <p>Type: {type}</p>
        <p>Status: {status}</p>
        {currentShip && (
          <div className="current-ship">
            <p>Current Ship: {currentShip.name}</p>
            <p>Arrival Time: {currentShip.eat}</p>
            <p>Departure Time: {currentShip.edt}</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default Terminal; 