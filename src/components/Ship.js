import React from "react";
import "./Ship.css";

const Ship = ({
  name,
  length,
  width,
  eat,
  edt,
  cargoType,
  operation,
  destination,
  state = "Arrival",
}) => {
  return (
    <div
      className={`ship ${state?.toLowerCase().replace(" ", "-") || "arrival"}`}
    >
      <h3>{name}</h3>
      <div className="ship-details">
        <div className="ship-dimensions">
          <p>Length: {length}m</p>
          <p>Width: {width}m</p>
        </div>
        <div className="ship-schedule">
          <p>EAT: {eat}</p>
          <p>EDT: {edt}</p>
        </div>
        <div className="ship-cargo">
          <p>Cargo Type: {cargoType}</p>
          <p>Operation: {operation}</p>
          <p>Destination: {destination}</p>
        </div>
        <div className="ship-state">
          <p>State: {state}</p>
        </div>
      </div>
    </div>
  );
};

export default Ship;
