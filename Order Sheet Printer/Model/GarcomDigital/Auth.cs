using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class Auth
    {
        public User user { get; set; }
        public String token { get; set; }

        public Usuario ToUsuario()
        {
            var usuario = new Usuario();
            usuario.usuario = user.email;
            usuario.admin = user.admin;
            usuario.token = token;
            usuario.nomeRestaurante = user.fantasy_name;
            return usuario;
        }
    }
}
