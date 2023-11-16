using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class User
    {
        public int id { get; set; }
        public String name { get; set; }
        public String fantasy_name { get; set; }
        public String email { get; set; }
        public bool admin { get; set; }
        public bool opened { get; set; }
        public String phone { get; set; }
        public bool has_service_tax { get; set; }

    }
}
