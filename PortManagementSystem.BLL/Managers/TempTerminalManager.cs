using Microsoft.EntityFrameworkCore;
using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.DAL.Models;
using PortManagementSystem.DAL.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Managers
{
    public class TempTerminalManager : ITempTerminalManager
    {
        private ITerminalTempRepository _tempTerminalRepository;
        private ITerminalRepository _terminalRepository;
        private ITempShipWaitingRepository _tempWaitingShipRepository;
        private ITempShipRepository _tempShipRepository;

        public TempTerminalManager(ITerminalTempRepository tempTerminalRepository, ITerminalRepository terminalRepository, ITempShipWaitingRepository tempShipWaitingRepository, ITempShipRepository tempShipRepository)
        {
            _tempTerminalRepository = tempTerminalRepository;
            _terminalRepository = terminalRepository;
            _tempWaitingShipRepository = tempShipWaitingRepository;
            _tempShipRepository = tempShipRepository;
        }
        public void AddTempTerminal()
        {
            var existingTerminals = _terminalRepository.GetAll().ToList();
            var exsistingTempTerminals = _tempTerminalRepository.GetAllTempTerminals().ToList();

            if (exsistingTempTerminals.Count < 7)
            {
                foreach (var terminal in existingTerminals)
                {
                    _tempTerminalRepository.AddTempTerminal(new TempTerminal
                    {
                        classification = terminal.classification,
                        status = terminal.status,

                    });
                }
            }
        }

        public IEnumerable<TempTerminalReadDto> GetAllTempTerminals()
        {
            var found = _tempTerminalRepository.GetAllTempTerminals().Select(a => new TempTerminalReadDto
            {
                id = a.id,
                status = a.status,
                classification = a.classification,
            });

            return found;
        }

        public List<DailyChangesDto> ForecastNextSevenDays(DateOnly today)
        {
            List<DailyChangesDto> snapshots = new List<DailyChangesDto>();

            for (int day = 0; day < 7; day++)
            {
                var currentDate = today.AddDays(day);

                // Handle Arriving Ships
                var arrivingShips = _tempShipRepository.GetAllTempShips()
                    .Where(d => d.EATDate == currentDate && d.status == "Arriving")
                    .ToList();

                foreach (var arrival in arrivingShips)
                {
                    var ship = new TempShipsWaiting
                    {
                        shipId = arrival.id,
                        name = arrival.name,
                        EATDate = arrival.EATDate,
                        EDTDate = arrival.EDTDate,
                        cargoType = arrival.cargoType,
                        status = "Anchor out",
                        Duration = arrival.EDTDate.DayNumber - arrival.EATDate.DayNumber
                    };

                    _tempWaitingShipRepository.AddWaitingTempShip(ship);
                    arrival.status = "Anchor out";
                    _tempShipRepository.UpdateTempShip(arrival);
                }

                // Handle Departed Ships
                var departuredShips = _tempShipRepository.GetAllTempShips()
                    .Where(d => d.EDTDate == currentDate && d.status == "At Port")
                    .ToList();

                foreach (var departure in departuredShips)
                {
                    if (departure.tempTerminalId.HasValue)
                    {
                        var terminal = _tempTerminalRepository.GetTempTerminalById(departure.tempTerminalId.Value);
                        terminal.status = "Available";
                        _tempTerminalRepository.UpdateTempTerminal(terminal);
                    }

                    departure.tempTerminalId = null;
                    departure.status = "Departed";

                    _tempShipRepository.UpdateTempShip(departure);
                }

                // Refresh state
                var anchorOutShips = _tempWaitingShipRepository.GetAllTempWaitingShips().ToList();
                var availableTerminals = _tempTerminalRepository.GetAllTempTerminals()
                    .Where(t => t.status == "Available")
                    .ToList();

                // Assign waiting ships to available terminals
                foreach (var terminal in availableTerminals.ToList())
                {
                    var matchedShip = anchorOutShips.FirstOrDefault(s => s.cargoType == terminal.classification);

                    if (matchedShip != null)
                    {
                        var tempShip = _tempShipRepository.GetTempById(matchedShip.shipId);
                        tempShip.status = "At Port";
                        tempShip.EDTDate = currentDate.AddDays(matchedShip.Duration);
                        tempShip.tempTerminalId = terminal.id;
                        tempShip.cargoType = matchedShip.cargoType;

                        _tempShipRepository.UpdateTempShip(tempShip);

                        terminal.status = "Busy";
                        _tempTerminalRepository.UpdateTempTerminal(terminal);

                        var waitingModel = _tempWaitingShipRepository.GetTempWaitingShipsWaitingById(matchedShip.id);
                        _tempWaitingShipRepository.DeleteTempWaitingShips(waitingModel);

                        anchorOutShips.Remove(matchedShip);
                    }
                }

                // Snapshot of daily changes
                var snapshot = new DailyChangesDto
                {
                    Date = currentDate.ToString("dddd", CultureInfo.InvariantCulture),
                    Ships = _tempShipRepository.GetAllTempShips()
                        .Select(s => new ShipForecastReadDto
                        {
                            name = s.name,
                            cargoType = s.cargoType,
                            status = s.status,
                        }).ToList(),
                };

                snapshots.Add(snapshot);
            }

            return snapshots;
        }
    }
}
