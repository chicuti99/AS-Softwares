using OrderSheetPrinter.Model.ReportsDataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class PaymentReport
    {
        public int id { get; set; }
        public String name { get; set; }
        public bool available { get; set; }
        public List<Payment> payments { get; set; }
        public double total_payments_price { get; set; }

        public FormaPagamento ToFormaPagamento()
        {
            var forma = new FormaPagamento();

            forma.nome = name;
            forma.valor = total_payments_price;

            return forma;
        }
    }
}
