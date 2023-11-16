using OrderSheetPrinter.Model.ReportsDataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model
{
    public class ObjetoImpressao
    {
        public int id { get; set; }
        public TipoImpressaoEnum tipo { get; set; }
        public NovoPedido pedido { get; set; }
        public Mesa mesa { get; set; }
        public ComandaIndividual comanda { get; set; }
        public RelatorioProdutos relatorio { get; set; }
    }
}
