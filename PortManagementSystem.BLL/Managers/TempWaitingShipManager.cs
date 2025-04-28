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
    public class TempWaitingShipManager : ITempWaitingShipsManager
    {
        private ITempShipWaitingRepository _tempWaitngShipRepository;
        private ITempShipRepository _tempShipRepository;

        public TempWaitingShipManager(ITempShipWaitingRepository tempShipWaitingRepository , ITempShipRepository tempShipRepository) {
            _tempWaitngShipRepository = tempShipWaitingRepository;
            _tempShipRepository = tempShipRepository;
        }

        public void AddTempWaitingShip()
        {
            var existingShipsWaiting = _tempShipRepository.GetWaitingStateTemps().ToList();

            foreach (var ship in existingShipsWaiting)
            {
                _tempWaitngShipRepository.AddWaitingTempShip(new TempShipsWaiting
                {
                    shipId = ship.id,
                    status = ship.status,
                    cargoType = ship.cargoType,
                    Duration = ship.EDTDate.DayNumber - ship.EATDate.DayNumber,
                    EATDate = ship.EATDate,
                    EDTDate = ship.EDTDate,
                    name = ship.name,
                    
                });
            }

        }
        public IEnumerable<TempWaitingShipsReadDto> GetAllTempWaitingShips()
        {
            var found = _tempWaitngShipRepository.GetAllShipsWaiting().Select(a => new TempWaitingShipsReadDto
            {
                shipId = a.shipId,
                status = a.status,
                cargoType = a.cargoType,
                Duration = a.Duration,
                EATDate = a.EATDate,
                EDTDate = a.EDTDate,
                name = a.name,
                
            });

            return found;
        }
    }
}

