import React, { useState } from "react";
import PortManagement from "./components/PortManagement";
import AddShipForm from "./components/AddShipForm";
import "./App.css";

function App() {
  const [ships, setShips] = useState([]);

  const handleAddShip = (shipData) => {
    setShips([...ships, shipData]);
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Port Management System</h1>
      </header>
      <main>
        <AddShipForm onAddShip={handleAddShip} />
        <PortManagement ships={ships} />
      </main>
    </div>
  );
}

export default App;
