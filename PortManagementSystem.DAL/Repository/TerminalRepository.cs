using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Migrations;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public class TerminalRepository : ITerminalRepository
    {
        private ProgramContext _context;

        public TerminalRepository(ProgramContext context) {
            _context = context;
        }

        public bool SaveChanges()
        {
            _context.ChangeTracker.Entries();
            return _context.SaveChanges() > 0;
        }

        public bool CheckPopulation()
        {
            return _context.Terminals.Any();
        }

        public void AddingTerminals(Terminal terminal)
        {
            _context.Terminals.Add(terminal);
        }


        public List<Ship> GettingArrivingShip(DateOnly today)
        {
            var Arrivingships = _context.Ships.Where(d => d.EATDate == today && d.status == "Arriving").ToList();
            return Arrivingships;
        } 

        public void AddingWaitingShips<T>(T waitingShip)
        {
            _context.Add(waitingShip);
        }
        //Finished Pt.1

        //Start Pt.2
        public List<Ship> GettingAtPortShip(DateOnly today)
        {
            var departuredShips = _context.Ships.Where(d => d.EDTDate == today && d.status == "At Port").ToList();
            return departuredShips;

        }
        public Terminal GetTerminalById(int? terminal_id)
        {
            var terminalsOccupied = _context.Terminals.Where(t => t.id == terminal_id).FirstOrDefault();
            return terminalsOccupied;
        }

        //Finished Pt.2 

        //Start Pt.3


        public List<Terminal> GetAvailableTerminals()
        {
            var availableterminals = _context.Terminals.Where(s => s.status == "Available").ToList();
            return availableterminals;
        }

        public List<ShipsWaiting> GetWaitingShips()
        {
            var anchorOutShips = _context.shipsWaitingTable.ToList();
            return anchorOutShips;
        }

        public Ship GetShipFromWaitingTable(ShipsWaiting shipsWaiting)
        {
            var ship = _context.Ships.Where(x => x.id == shipsWaiting.shipId).FirstOrDefault();
            return ship;
        }


        public void RemoveWaitingShip(ShipsWaiting ship)
        {
            _context.shipsWaitingTable.Remove(ship);
        }

        public IQueryable<Terminal> GetAll()
        {
            var found = _context.Terminals;
            return found;
        }
    }
}
