using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketBE.Interfaces
{
    public interface IUsuario
    {
         String usuario { get; set; }
         String senha { get; set; }
         bool admin { get; set; }
         String token { get; set; }
         String nomeRestaurante { get; set; }
         DateTime ultimoLogin { get; set; }
    }
}
