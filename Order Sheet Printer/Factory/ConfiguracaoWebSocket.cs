using OrderSheetPrinter.Domain.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketBE;
using WebSocketBE.Entities;

namespace OrderSheetPrinter.Factory
{
    public class ConfiguracaoWebSocket
    {
        public static WebSocketServidor CarregarConfiguracoes()
        {
            WebSocketServidor result = null;
            try
            {
                string filename = GetDirectory() + "ConfigWS.txt";
                string json = File.ReadAllText(filename);

                if (json != string.Empty)
                    result = Utils.DeserializeJson<WebSocketServidor>(json);
                else
                {
                    result = GetConfiguracoesPadrao();
                    SalvarConfiguracoes(result);
                }
            }
            catch (Exception ex)
            {
                result = GetConfiguracoesPadrao();
                SalvarConfiguracoes(result);
            }
            return result;
        }


        public static WebSocketServidor GetConfiguracoesPadrao()
        {

#if (DEBUG)
        {
            return new WebSocketServidor(UrlConnections.ConnectionServidorPrinterTeste, true, true);        
        }
#else
            return new WebSocketServidor(UrlConnections.ConnectionServidorPrinter, false, true);
#endif
        }

        public static void SalvarConfiguracoes(WebSocketServidor config)
        {
            string json = Utils.SerializeJson(config);
            File.WriteAllText(GetFullPathFilename(), json);
        }

        private static string GetDirectory() => AppDomain.CurrentDomain.BaseDirectory;


        public static string GetFilename() => "ConfigWS.txt";


        private static string GetFullPathFilename() => GetDirectory() + GetFilename();

    }
}
