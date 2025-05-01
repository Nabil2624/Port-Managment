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

        public bool SaveChanges();
        public void AddShip<T>(T shipToAdd);
        public Ship GetShip(int id);
        public void RemoveShip<T>(T ship);
        public IQueryable<Ship> GetAll();
        public IQueryable<Ship> ViewFullShipDetails();

        /*public void AddShip(Ship ship);
        public void UpdateShip(Ship ship);
        public void DeleteShip(Ship ship);
        public Ship GetById(int id);*/
    }
}
