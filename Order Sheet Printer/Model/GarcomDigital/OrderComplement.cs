using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class OrderComplement
    {
        public int id { get; set; }
        public int amount { get; set; }
        public Complement complement { get; set; }

        public Complemento ToComplemento()
        {
            var complemento = new Complemento();
            complemento.id = id.ToString();
            complemento.quantidade = amount;
            complemento.nome = complement.name;
            complemento.total = complement.price * amount;
            return complemento;
        }
    }
}
