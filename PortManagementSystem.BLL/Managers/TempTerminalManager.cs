using Microsoft.EntityFrameworkCore;
using PortManagementSystem.BLL.Dto_s;
using PortManagementSystem.DAL.Models;
using PortManagementSystem.DAL.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public List<DailyChangesDto> UpdateTempTerminal(List<TempShipReadDto> arrivingShips, List<TempShipReadDto> departuredShips,
    List<TempTerminalReadDto> availableTerminals, DateOnly today)
        {
            List<DailyChangesDto> snapshots = new List<DailyChangesDto>();

            for (int day = 0; day < 7; day++)
            {
                var currentDate = today.AddDays(day);

                // Handle Arriving Ships
                var todayArrivals = arrivingShips.Where(a => a.EATDate == currentDate && a.status == "Arriving").ToList();
                foreach (var arrival in todayArrivals)
                {
                    var waitingShip = new TempShipsWaiting
                    {
                        shipId = arrival.id,
                        name = arrival.name,
                        EATDate = arrival.EATDate,
                        EDTDate = arrival.EDTDate,
                        cargoType = arrival.cargoType,
                        status = "Anchor out",
                        Duration = arrival.EDTDate.DayNumber - arrival.EATDate.DayNumber
                    };
                    _tempWaitingShipRepository.AddWaitingTempShip(waitingShip);
                    arrival.status = "Anchor out";
                }

                // Handle Departing Ships
                var todayDepartures = departuredShips.Where(d => d.EDTDate == currentDate && d.status == "At Port").ToList();
                foreach (var departure in todayDepartures)
                {
                    if (departure.tempTerminalId.HasValue)
                    {
                        var terminal = _tempTerminalRepository.GetTempTerminalById(departure.tempTerminalId.Value);
                        terminal.status = "Available";
                        _tempTerminalRepository.UpdateTempTerminal(terminal);
                    }

                    departure.tempTerminalId = null;
                    departure.status = "Departed";

                    _tempShipRepository.UpdateTempShip(new TempShip
                    {
                        status = departure.status,
                        tempTerminalId = departure.tempTerminalId,
                        cargoType = departure.cargoType,
                        destination = departure.destination,
                        EATDate = departure.EATDate,
                        EDTDate = departure.EDTDate,
                        length = departure.length,
                        name = departure.name,
                        width = departure.width,
                        tempUserId = departure.userId
                    });
                }

                // Refresh available terminals and anchor out ships
                availableTerminals = _tempTerminalRepository.GetAllTempTerminals()
                    .Where(t => t.status == "Available")
                    .Select(t => new TempTerminalReadDto
                    {
                        id = t.id,
                        classification = t.classification,
                        status = t.status
                    }).ToList();

                var anchorOutShips = _tempWaitingShipRepository.GetAllTempWaitingShips().ToList();

                // Handle Assigning Waiting Ships to Available Terminals
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

                        var terminalEntity = _tempTerminalRepository.GetTempTerminalById(terminal.id);
                        terminalEntity.status = "Busy";
                        _tempTerminalRepository.UpdateTempTerminal(terminalEntity);

                        var waitingShipModel = _tempWaitingShipRepository.GetTempWaitingShipsWaitingById(matchedShip.id);
                        _tempWaitingShipRepository.DeleteTempWaitingShips(waitingShipModel);

                        anchorOutShips.Remove(matchedShip);
                        availableTerminals.Remove(terminal);
                    }
                }

                // ---- Take snapshot after today's changes ----

                var snapshot = new DailyChangesDto
                {
                    Date = currentDate,
                    Ships = _tempShipRepository.GetAllTempShips()
                                .Select(s => new TempShipReadDto
                                {
                                    id = s.id,
                                    name = s.name,
                                    cargoType = s.cargoType,
                                    EATDate = s.EATDate,
                                    EDTDate = s.EDTDate,
                                    status = s.status,
                                    tempTerminalId = s.tempTerminalId,
                                    userId = s.tempUserId
                                }).ToList(),

                    Terminals = _tempTerminalRepository.GetAllTempTerminals()
                                .Select(t => new TempTerminalReadDto
                                {
                                    id = t.id,
                                    classification = t.classification,
                                    status = t.status
                                }).ToList(),

                    WaitingShips = _tempWaitingShipRepository.GetAllTempWaitingShips()
                                .Select(w => new TempWaitingShipsReadDto
                                {
                                    id = w.id,
                                    shipId = w.shipId,
                                    name = w.name,
                                    cargoType = w.cargoType,
                                    EATDate = w.EATDate,
                                    EDTDate = w.EDTDate,
                                    status = w.status,
                                    Duration = w.Duration
                                }).ToList()
                };

                snapshots.Add(snapshot);
            }

            return snapshots;
        }
    }
    }
