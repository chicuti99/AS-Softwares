using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.ReportsDataSources
{
    public class RelatorioProdutos
    {
        public String nomeRestaurante { get; set; }
        public String dataInicial { get; set; }
        public String dataFinal { get; set; }
        public List<CategoriaRelatorioProdutos> pedidos { get; set; }
        public Double valorTotalPedidos { get; set; }
        public Double valorTotalServicos { get; set; }
        public Double qtdTotalPedidos { get; set; }
        public List<CategoriaRelatorioProdutos> pedidosCancelados { get; set; }
        public Double valorTotalPedidosCancelados { get; set; }
        public Double valorTotalServicosCancelados { get; set; }
        public int qtdTotalPedidosCancelados { get; set; }
        public Double totalCaixa { get; set; }
        public int qtdProdutos { get; set; }
        public int qtdClientes { get; set; }
        public List<FormaPagamento> formasPagamento { get; set; }
    }
}
