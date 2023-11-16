using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class CashierReport
    {
        public List<PaymentReport> paymentsReport { get; set; }
        public double total_payment_methods_price { get; set; }
    }
}
