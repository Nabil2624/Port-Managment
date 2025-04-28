using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public interface ITerminalRepository
    {

        public bool SaveChanges();
        public bool CheckPopulation();
        public void AddingTerminals(Terminal terminal);
        public List<Ship> GettingArrivingShip(DateOnly today);
        public void AddingWaitingShips<T>(T waitingShip);
        public List<Ship> GettingAtPortShip(DateOnly today);
        public Terminal GetTerminalById(int? terminal_id);
        public List<Terminal> GetAvailableTerminals();
        public List<ShipsWaiting> GetWaitingShips();
        public Ship GetShipFromWaitingTable(ShipsWaiting shipsWaiting);
        public void RemoveWaitingShip(ShipsWaiting ship);
    }
}
