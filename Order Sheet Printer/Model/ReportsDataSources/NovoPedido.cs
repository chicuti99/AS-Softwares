using System;
using System.Collections.Generic;

namespace OrderSheetPrinter.Model
{
    public class NovoPedido
    {
        public int id { get; set; }
        public String numero { get; set; }
        public String mesa { get; set; }
        public String hora { get; set; }
        public String nomeCliente { get; set; } //input_client
        public String telefoneCliente { get; set; }
        public String situacao { get; set; }
        public double total { get; set; }
        public double totalServicos { get; set; }
        public List<ItemPedido> itens { get; set; }
        public bool isEntrega { get; set; }
        public string DescricaoEntrega { get; set; }
        public string NomeRestaurante { get; set; } //restaurant_name
        public string TipoPedido { get; set; } //order_type
        public string MesaOrComanda { get; set; } // table_type
        public string GetNomeClienteFormatado
        {
            get
            {
                if (!String.IsNullOrEmpty(nomeCliente))
                {
                    return $"Cliente: " + nomeCliente;
                }
                return "";
            }
        }
        public string NomeGarcom { get; set; } //input_waiter
        public string GetNomeGarcomFormatado
        {
            get
            {
                if (!String.IsNullOrEmpty(NomeGarcom))
                {

                    return $"Garçom: " + NomeGarcom;
                }
                return "";
            }
        }
        public string Order_Type { get; set; }//

    }
}
