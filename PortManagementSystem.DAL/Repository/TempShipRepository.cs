using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public class TempShipRepository : ITempShipRepository
    {
        private ProgramContext _context;

        public TempShipRepository(ProgramContext context) {
            _context = context;
        }

        public void AddTempShip(TempShip shipToAdd)
        {
            _context.TempShips.Add(shipToAdd);
            _context.SaveChanges();
        }

        public void DeleteTempShips(TempShip tempShip)
        {
            _context.TempShips.Remove(tempShip);
            _context.SaveChanges();
        }

        public IQueryable<TempShip> GetAllTempShips()
        {
            var found = _context.TempShips;
            return found;
        }

        public TempShip GetTempById(int id)
        {
            var found = _context.TempShips.Find(id);
            return found;
        }

        public IQueryable<TempShip> GetWaitingStateTemps()
        {
            var found = _context.TempShips.Where(a => a.status == "Arriving");
            return found;
        }

        public void UpdateTempShip(TempShip tempShipToUpdate)
        {
            _context.TempShips.Update(tempShipToUpdate);
            _context.SaveChanges();
        }
    }
}
