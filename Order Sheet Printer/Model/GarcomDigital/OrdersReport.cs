using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class OrdersReport
    {
        public List<CategoryReport> categoriesReport { get; set; }
        public Double total_restaurant_price { get; set; }
        public Double total_restaurant_service_price { get; set; }
        public int total_restaurant_amount { get; set; }
        public List<CategoryReport> canceledCategoriesReport { get; set; }
        public Double total_restaurant_canceled_price { get; set; }
        public Double total_restaurant_canceled_service_price { get; set; }
        public int total_restaurant_canceled_amount { get; set; }
    }
}
