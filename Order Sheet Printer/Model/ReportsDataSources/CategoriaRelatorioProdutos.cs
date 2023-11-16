using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.ReportsDataSources
{
    public class CategoriaRelatorioProdutos
    {
        public int id { get; set; }
        public String name { get; set; }
        public List<ItemPedido> itens { get; set; }
        public Double valorTotalVendas { get; set; }
        public Double valorTotalServicoVendas { get; set; }
        public int qdtTotalVendas { get; set; }
        public Double valorTotalPedidos { get; set; }
        public Double valorTotalServicos { get; set; }
        public Double totalPedidos { get; set; }
        public String percentualVendas { get; set; }
        public int qtdTotalPedidos { get; set; }
    }
}
