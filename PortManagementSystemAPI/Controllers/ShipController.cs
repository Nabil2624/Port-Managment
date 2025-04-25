using Microsoft.AspNetCore.Mvc;
using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.BLL.Managers;
using PortManagementSystem.DAL.Models;

namespace PortManagementSystem.API.Controllers
{
    [ApiController]
    [Route("API/[Controller]")]
    public class ShipController : Controller
    {
        IShipServices _services;

        public ShipController(IConfiguration config, IShipServices shipServices)
        {
            _services = shipServices;
        }


        [HttpPost("AddShip")]
        public IActionResult AddShip(ShipToAddDTO ships)
        {
            bool check = _services.AddingShips(_services.MappingShips(ships));
            if (check)
                return Ok();
            throw new Exception("Could not add Ship");
        }



        [HttpDelete("RemoveShip/{shipId}")]
        public IActionResult RemoveShip(int shipId)
        {
            bool res = _services.RemovingShip(shipId);
            if (res)
            {
                if (_services.SavingChanges())
                    return Ok();
                throw new Exception("Failed remove Ship");
            }
            throw new Exception("Could not find Ship");
        }


        [HttpGet("GetShip/{shipId}")]
        public Ship GetShip(int shipId)
        {
            var ship = _services.RetriveShip(shipId);
            if (ship != null)
                return ship;
            throw new Exception("Ship Id is incorrect!!");
        }


        //This Endpoint not required, But Added if needed
        [HttpPut("EditShip")]
        public IActionResult EditShip(ShipToEditDTO ship)
        {
            var shipdb = _services.RetriveShip(ship.id);

            if (shipdb != null)
            {
                _services.UpdatingShipProperties(shipdb, ship);
                if (_services.SavingChanges())
                    return Ok();
                throw new Exception("Failed to Update");
            }
            throw new Exception("Ship Id is incorrect!!");
        }
    }
}
