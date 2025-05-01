using PortManagementSystem.BLL.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Managers
{
    public interface ITerminalServices
    {
        public void PopulatingTerminal();
        public void CheckArrivedShips(DateOnly today);
        public void CheckleavingShips(DateOnly today);
        public void CheckWaitingTable(DateOnly today);
        public IEnumerable<TempTerminalReadDto> GetAll();
        public int GetAvillableTerminalsNumber();
    }
}
