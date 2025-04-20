import React, { useState } from "react";
import Terminal from "./Terminal";
import Ship from "./Ship";
import "./PortManagement.css";

const PortManagement = ({ ships }) => {
  const [terminals, setTerminals] = useState([
    { id: 1, type: "Dangerous", status: "Available", currentShip: null },
    { id: 2, type: "Dangerous", status: "Available", currentShip: null },
    { id: 3, type: "Bulky", status: "Available", currentShip: null },
    { id: 4, type: "Bulky", status: "Available", currentShip: null },
    { id: 5, type: "Bulky", status: "Available", currentShip: null },
    { id: 6, type: "Bulky", status: "Available", currentShip: null },
    { id: 7, type: "Bulky", status: "Available", currentShip: null },
  ]);

  const assignTerminal = (ship) => {
    const availableTerminal = terminals.find(
      (terminal) =>
        terminal.status === "Available" &&
        ((ship.cargoType === "Dangerous" && terminal.type === "Dangerous") ||
          (ship.cargoType !== "Dangerous" && terminal.type === "Bulky"))
    );

    if (availableTerminal) {
      setTerminals(
        terminals.map((terminal) =>
          terminal.id === availableTerminal.id
            ? { ...terminal, status: "Occupied", currentShip: ship }
            : terminal
        )
      );
    }
  };

  React.useEffect(() => {
    ships.forEach((ship) => {
      if (ship.state === "Arrival") {
        assignTerminal(ship);
      }
    });
  }, [ships]);

  return (
    <div className="port-management">
      <div className="terminals-section">
        <h2>Terminals</h2>
        <div className="terminals-grid">
          {terminals.map((terminal) => (
            <Terminal key={terminal.id} {...terminal} />
          ))}
        </div>
      </div>

      <div className="ships-section">
        <h2>Ships</h2>
        <div className="ships-grid">
          {ships.map((ship) => (
            <Ship key={ship.id} {...ship} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default PortManagement;
