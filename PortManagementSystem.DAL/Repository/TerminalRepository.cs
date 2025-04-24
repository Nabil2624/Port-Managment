using PortManagementSystem.DAL.Database;
using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public class TerminalRepository : ITerminalRepository
    {
        private ProgramContext _context;

        public TerminalRepository(ProgramContext context) {
            _context = context;
        }
        
        public void AddTerminal(Terminal terminal)
        {
            _context.Terminals.Add(terminal);
            _context.SaveChanges();
        }

        public void DeleteTerminal(Terminal terminal)
        {
           _context.Terminals.Remove(terminal);
            _context.SaveChanges();
        }

        public IQueryable<Terminal> GetAll()
        {
            var found = _context.Terminals;
            return found;
        }

        public Terminal GetById(int id)
        {
            var found = _context.Terminals.Find(id);
            return found;
        }

        public void UpdateTerminal(Terminal terminal)
        {
            _context.Terminals.Update(terminal);
            _context.SaveChanges();
        }
    }
}
