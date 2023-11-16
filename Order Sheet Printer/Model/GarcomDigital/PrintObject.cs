using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Model.GarcomDigital;
using OrderSheetPrinter.Model.ReportsDataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model
{
    public class PrintObject
    {
        public int id { get; set; }
        public String order_status { get; set; }
        public String status { get; set; }
        public String basket_id { get; set; }
        public Double total_price { get; set; }
        public Double total_service_price { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public DateTime close_time { get; set; }
        public DateTime canceled_at { get; set; }
        public Table table { get; set; }
        public Session session { get; set; }
        public Bill bill { get; set; }

        public Buyer buyer { get; set; }
        public Waiter waiter { get; set; }
        public List<Order> orders { get; set; }
        public List<Bill> bills { get; set; }
        public double total_paid { get; set; }

        public List<OrderBasket> order_baskets { get; set; }
        public OrdersReport orders_report { get; set; }
        public CashierReport cashier_report { get; set; }
        public BuyersReport buyers_report { get; set; }
        public void SetBuyer(Buyer buyer)
        {
            this.buyer = buyer;
        }
        public Mesa ToMesa()
        {
            var mesa = new Mesa();

            mesa.mesa = table?.table_number.ToString();
            mesa.comanda = id.ToString();
            mesa.dataFechamento = Utils.GetBrasiliaTimezoneDateTime(end_time).ToString("dd/MM/yy HH:mm");

            mesa.pedidosFechados = new List<NovoPedido>();
            mesa.pedidosPendentes = new List<NovoPedido>();

            var pedidos = bills.ConvertAll<NovoPedido>(x => x.ToNovoPedido());

            foreach (var pedido in pedidos)
            {
                if (String.IsNullOrEmpty(pedido.nomeCliente))
                    pedido.nomeCliente = pedido.telefoneCliente;

                if (pedido.situacao == "PAGO")
                    mesa.pedidosFechados.Add(pedido);
                else
                    mesa.pedidosPendentes.Add(pedido);
            }

            mesa.totalMesa = total_service_price;
            mesa.totalPago = total_paid;
            mesa.totalRestante = mesa.totalMesa - mesa.totalPago;

            return mesa;
        }

        public NovoPedido ToPedido()
        {
            var pedido = bill.ToNovoPedido();
            
            pedido.numero = basket_id;
           // pedido.mesa = table.table_number != -1 ? "Mesa " + table?.table_number.ToString() : "Balcão";

            if (orders != null)
            {
                pedido.itens = orders.ConvertAll(x => x.ToItemPedido());
            }

            pedido.hora = Utils.GetBrasiliaTimezoneDateTime(close_time).ToString("HH:mm");
            pedido.situacao = !String.IsNullOrEmpty(order_status) && order_status == "finished" ? "PAGO" : "";

            return pedido;
        }

        public ComandaIndividual ToComandaIndividual()
        {
            var comanda = new ComandaIndividual();

            comanda.id = id.ToString();
            comanda.mesa = session?.table?.table_number.ToString();
            comanda.dataFechamento = Utils.GetBrasiliaTimezoneDateTime(close_time).ToString("dd/MM/yy - HH:mm");
            comanda.nomeCliente = Utils.CodePhoneDigits(buyer?.phone) ?? waiter?.name;

            comanda.itens = new List<ItemPedido>();
            order_baskets.ForEach(x => comanda.itens.AddRange(x.orders.ConvertAll<ItemPedido>(o => o.ToItemPedido())));
            
            comanda.total = total_service_price;
            comanda.totalServicos = total_service_price - total_price;

            return comanda;
        }

        public RelatorioProdutos ToRelatorioProdutos(DateTime dataInicial, DateTime dataFinal)
        {
            var relatorio = new RelatorioProdutos();

            relatorio.dataInicial = dataInicial.ToString("dd/MM/yy HH:mm");
            relatorio.dataFinal = dataFinal.ToString("dd/MM/yy HH:mm");
            relatorio.valorTotalPedidos = orders_report.total_restaurant_price;
            relatorio.valorTotalServicos = orders_report.total_restaurant_service_price - orders_report.total_restaurant_price;
            relatorio.qtdProdutos = orders_report.total_restaurant_amount;

            relatorio.valorTotalPedidosCancelados = orders_report.total_restaurant_canceled_amount;
            relatorio.valorTotalServicosCancelados = orders_report.total_restaurant_canceled_service_price - orders_report.total_restaurant_canceled_amount;
            relatorio.qtdTotalPedidosCancelados = orders_report.total_restaurant_canceled_amount;

            relatorio.pedidos = orders_report.categoriesReport.ConvertAll<CategoriaRelatorioProdutos>(x => x.ToCategoriaRelatorio());
            relatorio.pedidos.ForEach(x => x.percentualVendas = Math.Round(relatorio.valorTotalPedidos > 0 ? x.valorTotalVendas * 100 / relatorio.valorTotalPedidos : 0, 2) + "%");
            relatorio.pedidos = relatorio.pedidos.FindAll(x => x.valorTotalVendas > 0);

            relatorio.pedidosCancelados = orders_report.canceledCategoriesReport.ConvertAll<CategoriaRelatorioProdutos>(x => x.ToCategoriaRelatorio());
            relatorio.pedidosCancelados.ForEach(x => x.percentualVendas = Math.Round(relatorio.valorTotalPedidosCancelados > 0 ? x.valorTotalVendas * 100 / relatorio.valorTotalPedidosCancelados : 0, 2) + "%");

            relatorio.formasPagamento = cashier_report.paymentsReport.ConvertAll<FormaPagamento>(x => x.ToFormaPagamento());
            relatorio.totalCaixa = cashier_report.total_payment_methods_price;

            relatorio.qtdClientes = buyers_report?.total_buyers ?? 0;

            return relatorio;
        }
    }
}
