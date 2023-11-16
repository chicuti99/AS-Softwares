using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Model.GarcomDigital
{
    public class Error
    {
        public String error { get; set; }
        public List<ErrorMessage> messages { get; set; }
        public String GetMessage()
        {
            if (messages != null && messages.Count > 0)
            {
                return messages[0].message;
            }
            else
                return error;
        }
    }
}
