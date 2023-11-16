using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class Product
    {
        public int id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public Double price { get; set; }
        public Double price_promotion { get; set; }

    }
}
