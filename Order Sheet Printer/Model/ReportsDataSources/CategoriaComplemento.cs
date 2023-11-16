using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model
{
    public class CategoriaComplemento
    {
        public String id { get; set; }
        public String nome { get; set; }
        public List<Complemento> complementos { get; set; }
    }
}
