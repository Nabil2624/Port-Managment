using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Models
{
    public class Shipment
    {
        public int id { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public int shipId { get; set; }
        public Ship? ship { get; set; }
    }
}
