using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public interface IShipmentRepository
    {
        public void AddShipment (Shipment shipment);
        public void UpdateShipment (Shipment shipment);
        public void DeleteShipment (Shipment shipment);
        public IQueryable<Shipment> GetAll ();
        public Shipment GetById (int id);
    }
}
