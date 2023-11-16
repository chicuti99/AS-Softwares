using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class Buyer
    {
        public int id { get; set; }
        public String name { get; set; }
        public String phone { get; set; }
        public String email { get; set; }
        public int buy_count { get; set; }
    }
}
