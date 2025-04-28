using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public class TempTerminalRepository : ITerminalTempRepository
    {
        private ProgramContext _context;

        public TempTerminalRepository(ProgramContext context) {
            _context = context;
        }
        public void AddTempTerminal(TempTerminal tempTerminal)
        {
            _context.TempTerminals.Add(tempTerminal);
            _context.SaveChanges();
        }

        public void DeleteTempTerminal(TempTerminal tempTerminal)
        {
            _context.TempTerminals.Remove(tempTerminal);
            _context.SaveChanges();
        }

        public IQueryable<TempTerminal> GetAllTempTerminals()
        {
           var found = _context.TempTerminals;
            return found;
        }

        public TempTerminal GetTempTerminalById(int? id)
        {
            var found = _context.TempTerminals.Where(a => a.id == id).FirstOrDefault();
            return found;
        }

        public void UpdateTempTerminal(TempTerminal tempTerminal)
        {
            _context.TempTerminals.Update(tempTerminal);
            _context.SaveChanges();
        }
    }
}
