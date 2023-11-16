using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class OrderBasket
    {
        public int id { get; set; }
        public String order_status { get; set; }
        public String basket_id { get; set; }
        public Double total_price { get; set; }
        public Double total_service_price { get; set; }
        public DateTime start_time { get; set; }
        public DateTime close_time { get; set; }
        public DateTime canceled_at { get; set; }
        public int bill_id { get; set; }
        public List<Order> orders { get; set; }
    }
}
