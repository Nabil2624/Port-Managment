using Microsoft.AspNetCore.Mvc;
using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;

namespace PortManagementSystem.API.Controllers
{
    [ApiController]
    [Route("API/[Controller]")]
    public class TerminalController : Controller
    {
        ProgramContext _context;
        public TerminalController(IConfiguration config)
        {
            _context = new ProgramContext(config);
        }



        //This method should be called before applying the terminal Management Logic
        [HttpPost("PopulatingTerminal")]
        public void AddPortTerminals()
        {
            var check = _context.Terminals.Any();
            if (!check)
            {

                for (int i = 0; i < 7; i++)
                {
                    if (i < 5)
                        _context.Terminals.Add(new() { classification = "Safe", status = "Available" });
                    else
                        _context.Terminals.Add(new() { classification = "Dangerous", status = "Available" });
                }
                if (_context.SaveChanges() == 0)
                    throw new Exception("Could not add your terminals");
            }

        }



        [HttpPost("TerminalUpdate")]
        public IActionResult UpdateTerminal(DateOnly today)
        {
            //Checks Arrived Ships in the Ships Table & Adds them to the waiting  table
            var Arrivingships = _context.Ships.Where(d => d.EATDate == today && d.status == "Arriving").ToList();
            if (Arrivingships.Count > 0)
            {
                for (int i = 0; i < Arrivingships.Count; i++)
                {
                    var waitingShips = Arrivingships[i];
                    var ship = new ShipsWaiting
                    {
                        shipId = waitingShips.id,
                        name = waitingShips.name,
                        EATDate = waitingShips.EATDate,
                        EDTDate = waitingShips.EDTDate,
                        cargoType = waitingShips.cargoType,
                        status = "Anchor out",
                        Duration = waitingShips.EDTDate.DayNumber - waitingShips.EATDate.DayNumber
                    };
                    _context.shipsWaitingTable.Add(ship);
                    waitingShips.status = "Anchor out";
                    _context.ChangeTracker.Entries();
                    _context.SaveChanges();
                }
            }

            //Checks leaving Ships at Ships table and frees the terminal from them 
            var departuredShips = _context.Ships.Where(d => d.EDTDate == today && d.status == "At Port").ToList();
            if (departuredShips.Count > 0)
            {
                for (int i = 0; i < departuredShips.Count; i++)
                {
                    int? terminal_id = departuredShips[i].terminalId;
                    var terminalsOccupied = _context.Terminals.Where(t => t.id == terminal_id).FirstOrDefault();
                    terminalsOccupied.status = "Available";
                    departuredShips[i].terminalId = null;
                    departuredShips[i].status = "Depatured";
                    _context.ChangeTracker.Entries();
                    _context.SaveChanges();
                }
            }      


            //pt.3 here!!!
            var availableterminals = _context.Terminals.Where(s => s.status == "Available").ToList();
            var anchorOutShips = _context.shipsWaitingTable.ToList();
            if(anchorOutShips.Count > 0 && availableterminals.Count > 0)
            {
                for(int i = 0; i < availableterminals.Count; i++)
                {
                    for(int j = 0; j < anchorOutShips.Count; j++)
                    {
                        if (availableterminals[i].classification == anchorOutShips[j].cargoType && availableterminals[i].status == "Available")
                        {
                            var ship = _context.Ships.Where(x => x.id == anchorOutShips[j].shipId).FirstOrDefault();
                            ship.status = "At Port";
                            ship.EDTDate = today.AddDays(anchorOutShips[j].Duration);
                            ship.terminalId = availableterminals[i].id;
                            availableterminals[i].status = "Busy";
                            _context.shipsWaitingTable.Remove(anchorOutShips[j]);
                            _context.ChangeTracker.Entries();
                            _context.SaveChanges();
                        }
                    }
                }
            }
            return Ok();
        }
    }
}
