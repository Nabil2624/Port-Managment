using PortManagementSystem.BLL.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Managers
{
    public interface ITempWaitingShipsManager
    {
        public void AddTempWaitingShip();
        public IEnumerable<TempWaitingShipsReadDto> GetAllTempWaitingShips();
    }
}
