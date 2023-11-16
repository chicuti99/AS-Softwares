using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class ProductOrderReport
    {
        public String name { get; set; }
        public int total_sold_product_amount { get; set; }
        public Double total_sold_product_price { get; set; }

        public ItemPedido ToItemPedido()
        {
            var item = new ItemPedido();

            item.quantidade = total_sold_product_amount.ToString();
            item.descricao = name;
            item.total = total_sold_product_price;

            return item;
        }
    }
}
