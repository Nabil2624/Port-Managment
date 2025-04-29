using Microsoft.EntityFrameworkCore;
using PortManagementSystem.DAL.Models;
using PortManagementSystem.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Managers
{
    public class TerminalServices : ITerminalServices
    {
        ITerminalRepository _repo;
        public TerminalServices(ITerminalRepository terminalRepository)
        {
            _repo = terminalRepository;
        }


        //Add Port Terminals
        public void PopulatingTerminal()
        {
            if (!_repo.CheckPopulation())
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i < 5)
                        _repo.AddingTerminals(new() { classification = "Safe", status = "Available" });
                    else
                        _repo.AddingTerminals(new() { classification = "Dangerous", status = "Available" });
                }
                if (!_repo.SaveChanges())
                    throw new Exception("Could not Add Terminals");
            }
        }



        //Daily Terminal Update
        //pt.1 Check Arriving Ships and add them to Waiting Table
        public void CheckArrivedShips(DateOnly today)
        {
            var Arrivingships = _repo.GettingArrivingShip(today);
            if (Arrivingships.Count > 0)
            {
                for (int i = 0; i < Arrivingships.Count; i++)
                {
                    var waitingShips = Arrivingships[i];
                    var ship = new ShipsWaiting
                    {
                        shipId = waitingShips.id,
                        name = waitingShips.name,
                        EATDate = waitingShips.EATDate,
                        EDTDate = waitingShips.EDTDate,
                        cargoType = waitingShips.cargoType,
                        status = "Anchor out",
                        Duration = waitingShips.EDTDate.DayNumber - waitingShips.EATDate.DayNumber
                    };
                    _repo.AddingWaitingShips(ship);
                    waitingShips.status = "Anchor out";
                    if (!_repo.SaveChanges())
                        throw new Exception("Could not Add Terminals");
                }
            }
        }
        //Check Departured Ships in the Ships Tables and Free their space in the Terminal Table
        public void CheckleavingShips(DateOnly today)
        {
            var departuredShips = _repo.GettingAtPortShip(today);
            if (departuredShips.Count > 0)
            {
                for (int i = 0; i < departuredShips.Count; i++)
                {
                    int? terminal_id = departuredShips[i].terminalId;
                    var terminalsOccupied = _repo.GetTerminalById(terminal_id);
                    terminalsOccupied.status = "Available";
                    departuredShips[i].terminalId = null;
                    departuredShips[i].status = "Depatured";
                    if (!_repo.SaveChanges())
                        throw new Exception("Could not Add Terminals");
                }
            }

        }


        //Checking Terminals and Waiting Table
        public void CheckWaitingTable(DateOnly today)
        {
            var availableterminals = _repo.GetAvailableTerminals();
            var anchorOutShips = _repo.GetWaitingShips();
            //Repo
            if (anchorOutShips.Count > 0 && availableterminals.Count > 0)
            {
                for (int i = 0; i < availableterminals.Count; i++)
                {
                    for (int j = 0; j < anchorOutShips.Count; j++)
                    {
                        if (availableterminals[i].classification == anchorOutShips[j].cargoType && availableterminals[i].status == "Available")
                        {
                            var ship = _repo.GetShipFromWaitingTable(anchorOutShips[j]);
                            ship.status = "At Port";
                            ship.EDTDate = today.AddDays(anchorOutShips[j].Duration);
                            ship.terminalId = availableterminals[i].id;
                            availableterminals[i].status = "Busy";
                            _repo.RemoveWaitingShip(anchorOutShips[j]);
                            if (!_repo.SaveChanges())
                                throw new Exception("Could not Add Terminals");
                        }
                     
                    }   
                }
            }
        }

    }
}
