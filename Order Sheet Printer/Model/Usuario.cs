using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketBE.Interfaces;

namespace OrderSheetPrinter.Model
{
    public class Usuario : IUsuario
    {
        public string usuario { get; set; }
        public string senha { get; set; }
        public bool admin { get; set; }
        public string token { get; set; }
        public string nomeRestaurante { get; set; }
        public DateTime ultimoLogin { get; set; }
    }
}
