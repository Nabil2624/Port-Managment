using PortManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Dto_s
{
    public class TempShipReadDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cargoType { get; set; }
        public DateOnly EATDate { get; set; }
        public DateOnly EDTDate { get; set; }
        public string destination { get; set; }
        public double length { get; set; }
        public double width { get; set; }
        public string status { get; set; }
        public int? tempTerminalId { get; set; }  //Nullable
        public int userId { get; set; }

    }
}
