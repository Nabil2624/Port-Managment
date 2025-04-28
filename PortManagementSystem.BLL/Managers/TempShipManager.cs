using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.DAL.Models;
using PortManagementSystem.DAL.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Managers
{
    public class TempShipManager : ITempShipManager
    {
        private IShipRepository _shipRepository;
        private ITempShipRepository _tempShipRepository;

        public TempShipManager(IShipRepository shipRepository , ITempShipRepository tempShipRepository) {
            _shipRepository = shipRepository;
            _tempShipRepository = tempShipRepository;
        }
        
        public void AddTempShip()
        {
            var existingShips = _shipRepository.GetAll().ToList();
            var exsistingTempShips = _tempShipRepository.GetAllTempShips().ToList();

            foreach (var ship in existingShips)
            {

                if (exsistingTempShips.Count != 0)
                {
                    foreach (var tempShip in exsistingTempShips)
                    {
                        if (ship.destination != tempShip.destination && ship.status !=
                            tempShip.status && ship.width != tempShip.width && ship.cargoType != tempShip.cargoType &&
                            ship.EATDate != tempShip.EATDate && ship.EDTDate != tempShip.EDTDate && ship.length != tempShip.length &&
                            ship.name != tempShip.name && ship.terminalId != tempShip.tempTerminalId && ship.userId != tempShip.tempUserId)
                        {
                            tempShip.name = ship.name;
                            tempShip.destination = ship.destination;
                            tempShip.status = ship.status;
                            tempShip.width = ship.width;
                            tempShip.length = ship.length;
                            tempShip.EATDate = ship.EATDate;
                            tempShip.EDTDate = ship.EDTDate;
                            tempShip.tempTerminalId = ship.terminalId;
                            tempShip.tempUserId = ship.userId;
                            tempShip.cargoType = ship.cargoType;
                            
                            _tempShipRepository.AddTempShip(tempShip);
                        }
                        
                    }

                }
                else
                {
                    _tempShipRepository.AddTempShip(new TempShip
                    {
                        status = ship.status,
                        width = ship.width,
                        cargoType = ship.cargoType,
                        destination = ship.destination,
                        EATDate = ship.EATDate,
                        EDTDate = ship.EDTDate,
                        length = ship.length,
                        tempTerminalId = ship.terminalId,
                        tempUserId = ship.userId,
                        name = ship.name,
                    });
                }
            }
                
                          
        }


        public IEnumerable<TempShipReadDto> GetAllTempShips()
        {
            var found = _tempShipRepository.GetAllTempShips().Select(a => new TempShipReadDto
            {
                cargoType = a.cargoType,
                EATDate = a.EATDate,
                destination = a.destination,
                EDTDate= a.EDTDate,
                length = a.length,
                name = a.name,
                width = a.width,
                status = a.status,
                tempTerminalId=a.tempTerminalId,
                userId = a.tempUserId,
                id = a.id,
            }).ToList();

            return found;
        }

    }
}
