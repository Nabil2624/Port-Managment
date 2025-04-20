import React, { useState } from "react";
import "./AddShipForm.css";

const AddShipForm = ({ onAddShip }) => {
  const [shipData, setShipData] = useState({
    name: "",
    length: "",
    width: "",
    eat: "",
    edt: "",
    cargoType: "Bulky",
    operation: "Loading",
    destination: "",
  });

  const handleSubmit = (e) => {
    e.preventDefault();
    const newShip = {
      id: Date.now(),
      ...shipData,
      state: "Arrival",
    };
    onAddShip(newShip);
    setShipData({
      name: "",
      length: "",
      width: "",
      eat: "",
      edt: "",
      cargoType: "Bulky",
      operation: "Loading",
      destination: "",
    });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setShipData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  return (
    <form className="add-ship-form" onSubmit={handleSubmit}>
      <h3>Add New Ship</h3>
      <div className="form-grid">
        <div className="form-group">
          <label htmlFor="name">Ship Name:</label>
          <input
            type="text"
            id="name"
            name="name"
            value={shipData.name}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="length">Length (m):</label>
          <input
            type="number"
            id="length"
            name="length"
            value={shipData.length}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="width">Width (m):</label>
          <input
            type="number"
            id="width"
            name="width"
            value={shipData.width}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="eat">Expected Arrival Time:</label>
          <input
            type="datetime-local"
            id="eat"
            name="eat"
            value={shipData.eat}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="edt">Expected Departure Time:</label>
          <input
            type="datetime-local"
            id="edt"
            name="edt"
            value={shipData.edt}
            onChange={handleChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="cargoType">Cargo Type:</label>
          <select
            id="cargoType"
            name="cargoType"
            value={shipData.cargoType}
            onChange={handleChange}
            required
          >
            <option value="Bulky">Bulky</option>
            <option value="Dangerous">Dangerous</option>
          </select>
        </div>

        <div className="form-group">
          <label htmlFor="operation">Operation:</label>
          <select
            id="operation"
            name="operation"
            value={shipData.operation}
            onChange={handleChange}
            required
          >
            <option value="Loading">Loading</option>
            <option value="Unloading">Unloading</option>
          </select>
        </div>

        <div className="form-group">
          <label htmlFor="destination">Destination:</label>
          <input
            type="text"
            id="destination"
            name="destination"
            value={shipData.destination}
            onChange={handleChange}
            required
          />
        </div>
      </div>

      <button type="submit" className="submit-button">
        Add Ship
      </button>
    </form>
  );
};

export default AddShipForm;
