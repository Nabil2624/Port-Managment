using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.DAL.Models
{
    public class TempShipsWaiting
    {
        public int id { get; set; }
        public int shipId { get; set; }
        public TempShip? ship {  get; set; } 
        public string name { get; set; }
        public string cargoType { get; set; }
        public DateOnly EATDate { get; set; }
        public DateOnly EDTDate { get; set; }
        public string status { get; set; }
        public int Duration { get; set; }  //Use .AddDays(Duration) to edit the EDTDate

    }
}
