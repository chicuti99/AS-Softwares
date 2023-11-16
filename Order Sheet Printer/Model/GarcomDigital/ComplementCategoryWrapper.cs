using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class ComplementCategoryWrapper
    {
        public String id { get; set; }
        public ComplementCategory complement_category { get; set; }
        public List<OrderComplement> order_complements { get; set; }

        public CategoriaComplemento ToCategoriaComplemento()
        {
            var categoria = new CategoriaComplemento();

            categoria.id = id;
            categoria.nome = complement_category.name;
            categoria.complementos = order_complements.ConvertAll<Complemento>(x => x.ToComplemento());

            return categoria;
        }
    }
}
