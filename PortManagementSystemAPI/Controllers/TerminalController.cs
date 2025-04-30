using Microsoft.AspNetCore.Mvc;
using PortManagementSystem.BLL.Managers;
using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;

namespace PortManagementSystem.API.Controllers
{
    [ApiController]
    [Route("API/[Controller]")]
    public class TerminalController : Controller
    {
        ITerminalServices _terminal;
        public TerminalController(ITerminalServices terminalServices)
        {
            _terminal = terminalServices;
        }



        //This method should be called before applying the terminal Management Logic
        [HttpPost("PopulatingTerminal")]
        public void AddPortTerminals()
        {
            _terminal.PopulatingTerminal();
        }

        [HttpPost("TerminalUpdate")]
        public IActionResult UpdateTerminal(DateOnly today)
        {
            try
            {
                _terminal.CheckArrivedShips(today);
                _terminal.CheckleavingShips(today);
                _terminal.CheckWaitingTable(today);
                return Ok("Terminal(s) Updated");
            }

            catch (Exception ex) {
                return Ok("Terminal(s) Updated");
            }

            
        }

        [HttpGet("ViewAllTerminals")]
        public IActionResult GetAll()
        {
            var found = _terminal.GetAll();
            return Ok(found);

        }
    }
}
