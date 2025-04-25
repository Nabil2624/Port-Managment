using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Managers
{
    public interface IShipServices
    {
        public Ship MappingShips(ShipToAddDTO ships);
        public void UpdatingShipProperties(Ship shipdb, ShipToEditDTO ship);
        public bool AddingShips(Ship ship);
        public bool RemovingShip(int ShipId);
        public Ship RetriveShip(int shipId);
        public bool SavingChanges();

    }
}
