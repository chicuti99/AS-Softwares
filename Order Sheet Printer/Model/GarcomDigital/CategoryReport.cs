using OrderSheetPrinter.Model.ReportsDataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class CategoryReport
    {
        public int id { get; set; }
        public String name { get; set; }
        public int preparation_time { get; set; }
        public List<ProductOrderReport> products { get; set; }
        public Double total_sold_orders_price { get; set; }
        public Double total_sold_orders_service_price { get; set; }
        public int total_sold_orders_amount { get; set; }
        public Double total_canceled_orders_price { get; set; }
        public Double total_canceled_orders_service_price { get; set; }
        public int total_canceled_orders_amount { get; set; }
        public Double total_orders_price { get; set; }
        public Double total_orders_service_price { get; set; }
        public int total_orders_amount { get; set; }

        public CategoriaRelatorioProdutos ToCategoriaRelatorio()
        {
            var categoria = new CategoriaRelatorioProdutos();

            categoria.id = id;
            categoria.name = name;
            categoria.itens = products.ConvertAll<ItemPedido>(x => x.ToItemPedido());
            categoria.valorTotalVendas = total_canceled_orders_price > 0 ? total_canceled_orders_price : total_sold_orders_price;
            categoria.valorTotalServicoVendas = total_canceled_orders_service_price > 0 ? total_canceled_orders_service_price : total_sold_orders_service_price;
            categoria.qdtTotalVendas = total_canceled_orders_amount > 0 ? total_canceled_orders_amount : total_sold_orders_amount;
            categoria.valorTotalPedidos = total_orders_price;
            categoria.valorTotalServicos = total_orders_service_price;
            categoria.qtdTotalPedidos = total_orders_amount;


            return categoria;
        }
    }
}
