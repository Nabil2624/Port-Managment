using AutoMapper;
using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.DAL.Models;
using PortManagementSystem.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Managers
{
    public class ShipServices : IShipServices
    {
        IMapper _mapper;
        IShipRepository _repo;

        public ShipServices(IShipRepository ShipRepository)  // <---
        {
            _repo = ShipRepository;

            _mapper = new Mapper(new MapperConfiguration(cfg   //Mapping ShipToAddDTO with Ship
            =>
            { cfg.CreateMap<ShipToAddDTO, Ship>(); }));
        }


        //Body Methods
        public bool AddingShips(Ship ship)
        {
            _repo.AddShip(ship);
            return SavingChanges();
        }


        public bool RemovingShip(int ShipId)
        {
            var ship = _repo.GetShip(ShipId);
            if (ship != null)
            {
                _repo.RemoveShip(ship);
                return true;
            }
            return false;
        }


        public Ship RetriveShip(int shipId)
        {
            var ship = _repo.GetShip(shipId);
            if (ship != null)
                return ship;
            return null;
        }

        public bool SavingChanges()
        {
            return _repo.SaveChanges();
        }






        //Helping Methods
        public Ship MappingShips(ShipToAddDTO ships)
        {
            var shipdb = _mapper.Map<Ship>(ships);
            shipdb.status = "Arriving";
            shipdb.userId = 2;          //Should fetch the value from the Authentication Token
            //shipdb.terminalId = null;         //Should be nallable      ******Terminal is assinged after the ship has arrived*******
            return shipdb;
        }

        public void UpdatingShipProperties(Ship shipdb, ShipToEditDTO ship)
        {
            shipdb.cargoType = ship.cargoType;
            shipdb.EATDate = ship.EATDate;
            shipdb.EDTDate = ship.EDTDate;
            shipdb.destination = ship.destination;
            shipdb.length = ship.length;
            shipdb.width = ship.width;
        }
    }
}

