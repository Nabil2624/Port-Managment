using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public interface ITerminalTempRepository
    {
        public void AddTempTerminal(TempTerminal tempTerminal);
        public void UpdateTempTerminal(TempTerminal tempTerminal);
        public IQueryable<TempTerminal> GetAllTempTerminals();
        public void DeleteTempTerminal(TempTerminal tempTerminal);
        public TempTerminal GetTempTerminalById(int? id);
    }
}
