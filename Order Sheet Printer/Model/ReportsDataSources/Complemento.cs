using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model
{
    public class Complemento
    {
        public String id { get; set; }
        public int quantidade { get; set; }
        public String nome { get; set; }
        public double total { get; set; }

        public string quantidadeFormatada => $"   {quantidade}";
    }
}
