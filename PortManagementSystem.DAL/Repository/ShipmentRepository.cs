using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private ProgramContext _context;

        public ShipmentRepository(ProgramContext context) {
            _context = context;
        }
        
        public void AddShipment(Shipment shipment)
        {
            _context.Shipments.Add(shipment);
            _context.SaveChanges();
        }

        public void DeleteShipment(Shipment shipment)
        {
            _context.Shipments.Remove(shipment);
            _context.SaveChanges();
        }

        public IQueryable<Shipment> GetAll()
        {
            var found = _context.Shipments;
            return found;
        }

        public Shipment GetById(int id)
        {
            var found = _context.Shipments.Find(id);
            return found;
        }

        public void UpdateShipment(Shipment shipment)
        {
            _context.Shipments.Update(shipment);
            _context.SaveChanges();
        }
    }
}
