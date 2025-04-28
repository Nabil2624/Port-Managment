using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Managers
{
    public interface ITempShipManager
    {
        public void AddTempShip();
        public IEnumerable<TempShipReadDto> GetAllTempShips();

    }
}
