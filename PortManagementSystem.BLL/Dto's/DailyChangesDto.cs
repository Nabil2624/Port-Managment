using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Dto_s
{
    public class DailyChangesDto
    {
        public DateOnly Date { get; set; }
        public List<TempShipReadDto> Ships { get; set; } = new List<TempShipReadDto>();
        public List<TempTerminalReadDto> Terminals { get; set; } = new List<TempTerminalReadDto>();
        public List<TempWaitingShipsReadDto> WaitingShips { get; set; } = new List<TempWaitingShipsReadDto>();
    }
}
