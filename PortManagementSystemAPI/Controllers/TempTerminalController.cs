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
        
        
        [HttpPut("UpdateTerminal")]
        public IActionResult UpdateTerminal (DateOnly today)
        {
            _tempTerminalManager.AddTempTerminal();
            _tempShipManager.AddTempShip();
           // _tempWaitingShipManager.AddTempWaitingShip();

            var ships = _tempShipManager.GetAllTempShips();
            var terminals = _tempTerminalManager.GetAllTempTerminals().ToList();
            var anchorOutShips = _tempWaitingShipManager.GetAllTempWaitingShips().ToList();

            var Arrivingships = ships.Where(d => d.EATDate == today && d.status == "Arriving").ToList();
            var departuredShips =ships.Where(d => d.EDTDate == today && d.status == "At Port").ToList();
            var availableTerminals = terminals.Where(a => a.status == "Available").ToList();

           var log = _tempTerminalManager.UpdateTempTerminal(Arrivingships, departuredShips, availableTerminals , today);

            return Ok(log);

        }
    }
}
