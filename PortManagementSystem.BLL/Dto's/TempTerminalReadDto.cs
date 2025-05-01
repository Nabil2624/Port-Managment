using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortManagementSystem.BLL.Dto_s
{
    public class TempTerminalReadDto
    {
        public int id {  get; set; }
        public string classification { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public DateOnly EATDate { get; set; }
        public DateOnly EDTDate { get; set; }
        public int? terminalId { get; set; }

    }
}
