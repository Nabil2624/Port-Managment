using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Models
{
    public class Ship
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cargoType { get; set; }
        public DateTime EAT { get; set; }
        public DateTime EDT { get; set; }
        public string destination { get; set; }
        public double length { get; set; }
        public double width { get; set; }
        public string status { get; set; }
        public int terminalId { get; set; }
        public Terminal? terminal { get; set; }
        public int userId { get; set; }
        public User? user { get; set; }
    }
}
