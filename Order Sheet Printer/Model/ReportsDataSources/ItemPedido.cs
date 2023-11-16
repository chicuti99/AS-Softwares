using OrderSheetPrinter.Model.ReportsDataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model
{
    public class ItemPedido
    {
        public int id { get; set; }
        public String quantidade { get; set; }
        public String descricao { get; set; }
        public double total { get; set; }
        public List<CategoriaComplemento> categoriasComplemento { get; set; }
        public ObservacaoItemPedido observacao { get; set; }
    }
}
