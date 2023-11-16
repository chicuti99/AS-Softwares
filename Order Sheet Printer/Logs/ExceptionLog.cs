using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OrderSheetPrinter.Logs
{
    public class ExceptionLog
    {
        public static string Insert(Exception ex, string pCommand)
        {
            string filePath = "";
            try
            {
                XDocument xml = new XDocument(new XDeclaration("1.0", "utf-8", null));
                XElement cabecalho = new XElement("LOG");
                XElement erro = new XElement("ERRORS");
                erro.Add(new XElement("Message", ex.Message));
                erro.Add(new XElement("StackTrace", ex.StackTrace));
                erro.Add(new XElement("Detalhes", (ex.InnerException != null ? ex.InnerException.Message : "") + (pCommand != null && pCommand.Length > 0 ? " - " + pCommand : "")));
                erro.Add(new XElement("Data", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));

                cabecalho.Add(erro);
                xml.Add(cabecalho);

                string caminho = GetPathFullFilanme();

                xml.Save(caminho);
            }
            catch (Exception x)
            {
                MessageBox.Show("Falha       ao registrar no Log! Verifique suas permissões de acesso." + "\n\r\n\r\n\rDetalhamento: " + x.Message, "Erro !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
            return filePath;
        }
        public static string GetPathFullFilanme()
        {
            var x = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + $"\\LogsErro");
           
            string path = AppDomain.CurrentDomain.BaseDirectory + $"\\LogsErro\\{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss").Replace("/", "").Replace(":", "") + ".xml"}";

            return path;
        }
    }
}
