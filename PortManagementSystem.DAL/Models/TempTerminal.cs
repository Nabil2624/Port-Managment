using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Models
{
    public class TempTerminal
    {
        public int id { get; set; }
        public string classification { get; set; }
        public string status { get; set; }
        public ICollection<TempShip>? tempShips { get; set; }
    }
}
