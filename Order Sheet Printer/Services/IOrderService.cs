using OrderSheetPrinter.Domain.Entities;
using OrderSheetPrinter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Services
{
    public interface IOrderService
    {
        Usuario Login(String usuario, String senha);
        void SetToken(String token);
        void SavePrinter(Impressora printer);
        void UpdatePrinter(Impressora printer);
        void DeletePrinter(Impressora printer);
        List<Impressora> GetAvaliablePrinters();
        List<ObjetoImpressao> GetOrdersForPrinter(int idImpressora);
        void RemoveFromQueue(int printId);
    }
}
