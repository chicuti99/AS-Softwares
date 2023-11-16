using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketBE.Entities
{
    public class UrlConnections
    {
        public static string ConnectionServidorPrinter { get; } = "wss://backend.takeat.app/printers";
        public static string ConnectionServidorPrinterTeste { get; } = "wss://testbackend.takeat.app/printers";
    }
}
