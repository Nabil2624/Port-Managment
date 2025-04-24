using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Repository
{
    public interface ITerminalRepository
    {
        public void AddTerminal(Terminal terminal);
        public void UpdateTerminal(Terminal terminal);
        public void DeleteTerminal(Terminal terminal);
        public IQueryable<Terminal> GetAll();
        public Terminal GetById(int id);
    }
}
