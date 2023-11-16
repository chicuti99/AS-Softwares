using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Model.GarcomDigital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model
{
    public class Bill
    {
        public int id { get; set; }
        public Double total_price { get; set; }
        public Double total_service_price { get; set; }
        public int payment_method { get; set; }
        public bool service_tax { get; set; }
        public DateTime start_time { get; set; }
        public DateTime close_time { get; set; }

        public String status { get; set; }
        public Waiter waiter { get; set; }
        public Buyer buyer { get; set; }
        public List<OrderBasket> order_baskets { get; set; }

        public NovoPedido ToNovoPedido()
        {
            var pedido = new NovoPedido();
            pedido.id = id;
            pedido.total = total_service_price;
            pedido.totalServicos = total_service_price - total_price;
            pedido.situacao = !String.IsNullOrEmpty(status) && status == "finished" ? "PAGO" : "";
            pedido.nomeCliente = buyer != null ? buyer.name : waiter?.name;
            pedido.telefoneCliente = Utils.CodePhoneDigits(buyer?.phone) ?? "";

            if (pedido.telefoneCliente == null || pedido.telefoneCliente == "") pedido.telefoneCliente = pedido.nomeCliente;
            pedido.total = total_service_price;

            if (order_baskets != null)
            {
                pedido.itens = new List<ItemPedido>();
                pedido.situacao = status == "finished" ? "PAGO" : "ABERTO";
                order_baskets.ForEach(x => pedido.itens.AddRange(x.orders.ConvertAll<ItemPedido>(o => o.ToItemPedido())));
            }

            return pedido;
        }
    }
}
