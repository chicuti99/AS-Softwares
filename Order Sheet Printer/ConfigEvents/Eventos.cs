using Newtonsoft.Json;
using OrderSheetPrinter.Controller;
using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Model;
using OrderSheetPrinter.Model.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketBE;
using WebSocketBE.Entities;

namespace OrderSheetPrinter.ConfigEvents
{
    public class Eventos
    {
        #region EVENTOS

        public static void OnMessageReceived(ThreadControl thread)
        {
            try
            {
                OrderPrinterWebSocket listPrint = Utils.DeserializeJson<OrderPrinterWebSocket>(thread.Json.ToString());

                if (listPrint?.data?.Count > 0)
                {
                    var sistema = Sistema.GetLoggedSystem();

                    var printers = sistema.service.GetAvaliablePrinters();

                    foreach (var obj in listPrint.data)                      
                            FluxoImpressao.GetInstance().PrintOrder(obj.ToObjetoImpressao(), printers.FirstOrDefault(x => x.id == obj.printer_id), sistema);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            thread.IsAlive = false;
        }
        #endregion

        public static void SetEventos(WebSocketServidor webSocket)
        {
            webSocket.OnMessageReceivedEvent -= OnMessageReceived;
            webSocket.OnMessageReceivedEvent += OnMessageReceived;
        }
    }



}
