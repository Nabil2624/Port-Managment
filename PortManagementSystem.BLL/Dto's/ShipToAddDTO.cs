using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Dto_s
{
    public class ShipToAddDTO
    {
        public string name { get; set; }
        public string cargoType { get; set; }
        public DateTime EAT { get; set; }
        public DateTime EDT { get; set; }
        public string destination { get; set; }
        public double length { get; set; }
        public double width { get; set; }

    }
}
