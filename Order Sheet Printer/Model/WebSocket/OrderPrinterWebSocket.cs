using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.WebSocket
{
    public class OrderPrinterWebSocket
    {
        public string type { get; set; }
        public List<PrintItem> data { get; set; }

    }
}
