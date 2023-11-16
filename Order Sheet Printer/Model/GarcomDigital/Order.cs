using OrderSheetPrinter.Model.ReportsDataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class Order
    {
        public int id { get; set; }
        public String details { get; set; }
        public int amount { get; set; }
        public Double price { get; set; }
        public Double total_price { get; set; }
        public Double total_service_price { get; set; }
        public Product product { get; set; }
        public List<ComplementCategoryWrapper> complement_categories { get; set; }
        public decimal weight { get; set; }

        public bool use_weight { get; set; }

        public ItemPedido ToItemPedido()
        {
            var item = new ItemPedido();

            item.id = id;
            item.quantidade = use_weight ? weight + " Kg" : amount.ToString();
            item.descricao = product.name;
            item.total = total_price;

            item.observacao = !String.IsNullOrEmpty(details) ? new ObservacaoItemPedido(details) : null;

            item.categoriasComplemento = complement_categories != null ? complement_categories.ConvertAll(x => x.ToCategoriaComplemento()) : new List<CategoriaComplemento>();
            return item;
        }
    }
}
