using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.ReportsDataSources
{
    public class ObservacaoItemPedido
    {
        public String obs { get; set; }
        
        public ObservacaoItemPedido(String obs)
        {
            this.obs = $"Obs: {obs}";
        }
    }
}
