using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.ReportsDataSources
{
    public class Mesa
    {
        public String mesa { get; set; }
        public String comanda { get; set; }
        public String dataFechamento { get; set; }
        public String nomeRestaurante { get; set; }
        public List<NovoPedido> pedidosPendentes { get; set; }
        public List<NovoPedido> pedidosFechados { get; set; }
        public double totalMesa { get; set; }
        public double totalPago { get; set; }
        public double totalRestante { get; set; }
    }
}
