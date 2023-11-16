using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class Payment
    {
        public int id { get; set; }
        public double payment_value { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int payment_method_id { get; set; }
        public int restaurant_id { get; set; }
        public int table_session_id { get; set; }
    }
}
