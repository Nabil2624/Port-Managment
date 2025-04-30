using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.BLL.Managers;
using PortManagementSystem.DAL.Models;

namespace PortManagementSystem.API.Controllers
{
    [ApiController]
    [Route("API/[Controller]")]
    [Authorize]
    public class ShipController : Controller
    {
        IShipServices _services;
        IMapper _mapper;

        public ShipController(IConfiguration config, IShipServices shipServices)
        {
            _services = shipServices;
            _mapper = new Mapper(new MapperConfiguration(cfg   //Mapping ShipToAddDTO with Ship
                    =>
                { cfg.CreateMap<ShipToAddDTO, Ship>(); }));
        }


        [HttpPost("AddShip")]
        public IActionResult AddShip(ShipToAddDTO ships)
        {
            bool check = _services.AddingShips(MappingShips(ships));
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

        private Ship MappingShips(ShipToAddDTO ships)
        {
            var shipdb = _mapper.Map<Ship>(ships);
            shipdb.status = "Arriving";
            shipdb.userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);          //Should fetch the value from the Authentication Token
            //shipdb.terminalId = null;         //Should be nallable      ******Terminal is assinged after the ship has arrived*******
            return shipdb;
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
           var found = _services.GetShipList();
           return Ok(found);
        }
    }
}
