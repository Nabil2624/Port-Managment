using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortManagementSystem.BLL.Managers;

namespace PortManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempTerminalController : ControllerBase
    {
        private ITempShipManager _tempShipManager;
        private ITempTerminalManager _tempTerminalManager;
        private ITempWaitingShipsManager _tempWaitingShipManager;

        public TempTerminalController(ITempShipManager tempShipManager , ITempTerminalManager tempTerminalManager 
            , ITempWaitingShipsManager tempWaitingShipsManager) {
            _tempShipManager = tempShipManager;
            _tempTerminalManager = tempTerminalManager;
            _tempWaitingShipManager = tempWaitingShipsManager;
        }
        
        
        [HttpPut("SevenDayForecast")]
        public IActionResult SevenDayForecast(DateOnly today)
        {
            _tempTerminalManager.AddTempTerminal();
            _tempShipManager.AddTempShip();
            _tempTerminalManager.AddTempTerminal();

          var log = _tempTerminalManager.ForecastNextSevenDays(today);

            return Ok(log);

        }
    }
}
