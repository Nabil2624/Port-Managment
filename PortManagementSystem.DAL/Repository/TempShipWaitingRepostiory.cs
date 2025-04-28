using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public class TempShipWaitingRepostiory : ITempShipWaitingRepository
    {
        private ProgramContext _context;

        public TempShipWaitingRepostiory(ProgramContext context) {
            _context = context;
        }
        
        public void AddWaitingTempShip(TempShipsWaiting shipToAdd)
        {
            _context.TempShipsWaiting.Add(shipToAdd);
            _context.SaveChanges();
        }

        public void DeleteTempWaitingShips(TempShipsWaiting tempShip)
        {
            _context.TempShipsWaiting.Remove(tempShip);
            _context.SaveChanges();
        }

        public IQueryable<ShipsWaiting> GetAllShipsWaiting()
        {
            var found = _context.shipsWaitingTable;
            return found;
        }

        public IQueryable<TempShipsWaiting> GetAllTempWaitingShips()
        {
            var found = _context.TempShipsWaiting;
            return found;
        }

        public TempShipsWaiting GetTempWaitingShipsWaitingById(int id)
        {
            var found = _context.TempShipsWaiting.Find(id);
            return found;
        }

        public void UpdateTempWaitingShip(TempShipsWaiting tempShipToUpdate)
        {
            _context.TempShipsWaiting.Update(tempShipToUpdate);
            _context.SaveChanges();
        }
    }
}
