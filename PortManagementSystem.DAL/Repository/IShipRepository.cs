using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public interface IShipRepository
    {
        public void AddShip(Ship ship);
        public void UpdateShip(Ship ship);
        public void DeleteShip(Ship ship);
        public IQueryable<Ship> GetAll();
        public Ship GetById(int id);
    }
}
