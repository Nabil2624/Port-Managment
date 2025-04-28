using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Managers
{
    public interface ITempTerminalManager
    {
        public void AddTempTerminal();
        public IEnumerable<TempTerminalReadDto> GetAllTempTerminals();
       public List<DailyChangesDto> UpdateTempTerminal(List<TempShipReadDto> arrivingShips, List<TempShipReadDto> departuredShips,
            List<TempTerminalReadDto> availableTerminals , DateOnly today);
    }
}
