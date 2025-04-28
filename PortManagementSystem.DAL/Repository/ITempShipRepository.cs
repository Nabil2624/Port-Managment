using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public interface ITempShipRepository
    {
        public void AddTempShip(TempShip tempShip);
        public void UpdateTempShip(TempShip tempShipToUpdate);
        public IQueryable<TempShip> GetAllTempShips();
        public TempShip GetTempById (int id);
        public IQueryable<TempShip> GetWaitingStateTemps();
        //public void DeleteTempShips(TempShip tempShip);
    }
}
