using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public interface ITempShipWaitingRepository
    {
        public void AddWaitingTempShip(TempShipsWaiting shipToAdd);
        public void UpdateTempWaitingShip(TempShipsWaiting tempShipToUpdate);
        public IQueryable<TempShipsWaiting> GetAllTempWaitingShips();
        public void DeleteTempWaitingShips(TempShipsWaiting tempShip);
        public IQueryable<ShipsWaiting> GetAllShipsWaiting();
        public TempShipsWaiting GetTempWaitingShipsWaitingById(int id);
    }
}
