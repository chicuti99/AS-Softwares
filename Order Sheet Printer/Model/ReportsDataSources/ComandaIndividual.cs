using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.ReportsDataSources
{
    public class ComandaIndividual
    {
        public String id { get; set; }
        public String mesa { get; set; }
        public String dataFechamento { get; set; }
        public String nomeRestaurante { get; set; }
        public String nomeCliente { get; set; }
        public List<ItemPedido> itens { get; set; }
        public Double total { get; set; }
        public Double totalServicos { get; set; }
    }
}
