using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Model.ReportsDataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model
{
    public class PrintItem
    {
        private const String PRINT_TYPE_ORDER = "order";    // print obj with bill and orders
        private const String PRINT_TYPE_INDIVIDUAL_BILL = "individual_bill"; // print obj with order_baskets
        private const String PRINT_TYPE_TABLE_SESSION = "table_session"; // print obj with bills
        private const String PRINT_TYPE_ORDERS_REPORT = "orders-report";

        public int id { get; set; }
        public String print_status { get; set; }
        public int printer_id { get; set; }
        public DateTime printed_at { get; set; }
        public String print_type { get; set; }
        public DateTime filter_start_date { get; set; }
        public DateTime filter_end_date { get; set; }
        public string table_type { get; set; }
        public bool is_delivery { get; set; }
        public string restaurant_name { get; set; }
        public string order_type { get; set; }
        public string input_waiter { get; set; }
        public string input_client { get; set; }
        public string with_withdrawal { get; set; }
        public PrintObject print_object { get; set; }



        #region ToObjetoImpressaoOld
        /*
        public ObjetoImpressao ToObjetoImpressaoOld()
        {
            var obj = new ObjetoImpressao();
            obj.id = id;

            switch (print_type)
            {
                case PRINT_TYPE_ORDER:                    
                    
                    return ToObjectPrint();

                case PRINT_TYPE_INDIVIDUAL_BILL:
                    obj.tipo = TipoImpressaoEnum.COMANDA_INDIVIDUAL;
                    obj.comanda = print_object.ToComandaIndividual();
                    return obj;

                case PRINT_TYPE_TABLE_SESSION:
                    obj.tipo = TipoImpressaoEnum.MESA;
                    obj.mesa = print_object.ToMesa();
                    return obj;

                case PRINT_TYPE_ORDERS_REPORT:
                    obj.tipo = TipoImpressaoEnum.RELATORIO_PRODUTOS;
                    obj.relatorio = print_object.ToRelatorioProdutos(filter_start_date, filter_end_date);
                    return obj;

                default:
                    return null;
            }
        }*/
        #endregion
        public ObjetoImpressao ToObjetoImpressao()
        {
            return ToObjectPrint();            
        }

        private ObjetoImpressao ToObjectPrint()
        {
            var obj = new ObjetoImpressao();
            obj.id = id;
            
            obj.tipo = TipoImpressaoEnum.PEDIDO;
            obj.pedido = print_object.ToPedido();
            obj.pedido.isEntrega = is_delivery;
            obj.pedido.DescricaoEntrega = is_delivery ? with_withdrawal : "";
            obj.pedido.mesa = table_type;
            obj.pedido.NomeRestaurante = restaurant_name;
            obj.pedido.MesaOrComanda = table_type;
            obj.pedido.Order_Type = order_type;
            obj.pedido.nomeCliente = input_client;
            obj.pedido.NomeGarcom = input_waiter;

            return obj;
        }


    }
}
