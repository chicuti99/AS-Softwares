using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Auxiliar
{
    public static class ConfigManager
    {
        private static Preferencias _preferencias;
        public static Preferencias Preferencias
        {
            get
            {
                if (_preferencias == null)
                    _preferencias = Preferencias.CarregarConfiguracoes();
                return _preferencias;
            }
            set { _preferencias = value; }
        }


    }
}
