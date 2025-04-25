using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public class ShipRepository : IShipRepository
    {
        private ProgramContext _context;

        public ShipRepository(ProgramContext context)
        {
            _context = context;
        }


        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void AddShip<T>(T shipToAdd)
        {
            if (shipToAdd != null)
                _context.Add(shipToAdd);
        }

        public Ship GetShip(int id)
        {
            var ship = _context.Ships.Where(i => i.id == id).FirstOrDefault();
            if (ship != null)
                return ship;
            return null;
        }

        public void RemoveShip<T>(T ship)
        {
            _context.Remove(ship);
        }



        /*        public void AddShip(Ship ship)
                {
                    _context.Ships.Add(ship);
                    _context.SaveChanges();
                }

                public void DeleteShip(Ship ship)
                {
                    _context.Ships.Remove(ship);
                    _context.SaveChanges();
                }

                public IQueryable<Ship> GetAll()
                {
                    var found = _context.Ships;
                    return found;
                }

                public Ship GetById(int id)
                {
                    var found = _context.Ships.Find(id);
                    return found;
                }

                public void UpdateShip(Ship ship)
                {
                    _context.Ships.Update(ship);
                    _context.SaveChanges();
                }*/
    }
}
