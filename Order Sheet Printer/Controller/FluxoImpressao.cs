using OrderSheetPrinter.Domain.Entities;
using OrderSheetPrinter.Model;
using OrderSheetPrinter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WebSocketBE;

namespace OrderSheetPrinter.Controller
{
    public class FluxoImpressao
    {
        private static FluxoImpressao instance;
        private bool running = false;
        private List<Sistema> sistemas = new List<Sistema>();

        private FluxoImpressao() { }

        public static FluxoImpressao GetInstance()
        {
            if (instance == null)
            {
                instance = new FluxoImpressao();
                instance.Init();
            }
            return instance;
        }

        private void Init()
        {
            foreach (SistemaEnum item in Enum.GetValues(typeof(SistemaEnum)))
            {
                Sistema sistema = (Sistema.Create(item));
                sistemas.Add(sistema);
            }
        }

        public async void Execute()
        {
            if (running)
                return;

            running = true;

            await Task.Run(() =>
            {
                foreach (var sistema in sistemas)
                {
                    try
                    {
                        if (sistema.defaultUser == null)
                        {
                            var storedUser = CredentialService.Get(sistema.type);
                            if (storedUser != null)
                            {
                                sistema.defaultUser = sistema.service.Login(storedUser.usuario, storedUser.senha);
                                sistema.defaultUser.senha = storedUser.senha;
                            }
                        }

                        if (sistema.defaultUser != null)
                        {
                            sistema.service.SetToken(sistema.defaultUser.token);

                            RunPrintRoutine(sistema);

                        }
                    }
                    catch (Exception e)
                    {
                        Log($"Execute() - {sistema.nome}: {e}");
                    }
                }
            });

            running = false;
        }

        private void RunPrintRoutine(Sistema sistema)
        {
            try
            {
                IOrderService service = sistema.service;

                var printers = service.GetAvaliablePrinters();

                var installedPrinters = new List<String>();

                var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
                foreach (var printer in printerQuery.Get())
                {
                    String name = Convert.ToString(printer.GetPropertyValue("Name"));

                    installedPrinters.Add(name);
                }

                foreach (var printer in printers)
                {
                    // só executar a rotina de impressão se a impressora estiver instalada no pc atual
                    if (installedPrinters.Contains(printer.host))
                        PrintOrdersForPrinter(printer, sistema);
                }
            }
            catch (Exception e)
            {
                Log($"ERRO - RunPrintRoutine(): {sistema.nome} -> {e}");
            }
        }

        private void PrintOrdersForPrinter(Impressora printer, Sistema sistema)
        {
            try
            {
                IOrderService service = sistema.service;

                var printObjects = service.GetOrdersForPrinter(printer.id);

                foreach (var obj in printObjects)
                {
                    PrintOrder(obj, printer, sistema);
                }
            }
            catch (Exception e)
            {
                Log($"ERRO - PrintOrdersForPrinter(): impressora: {printer.nome}({printer.id}), {sistema.nome} -> {e}");
            }
        }
        public void PrintOrder(ObjetoImpressao objetoImpressao, Impressora impressora, Sistema sistema)
        {
            try
            {
                if (objetoImpressao == null)
                    return;

                var service = PrinterService.GetInstance();
                var printHost = impressora.host;

                service.Print(objetoImpressao.pedido, printHost);


                sistema.service.RemoveFromQueue(objetoImpressao.id);
            }
            catch (Exception e)
            {
                Log($"ERRO - PrintOrder(): tipo de impressão: {objetoImpressao.tipo.ToString()}, impressora: {impressora.nome}({impressora.id}), {sistema.nome} -> {e}");
            }
        }

        #region PRINTER OLD
        /*
        public void PrintOrderOLD(ObjetoImpressao objetoImpressao, Impressora impressora, Sistema sistema)
        {
            try
            {
                if (objetoImpressao == null)
                    return;

                var service = PrinterService.GetInstance();
                var printHost = impressora.host;

                switch (objetoImpressao.tipo)
                {
                    case TipoImpressaoEnum.COMANDA_INDIVIDUAL:
                        objetoImpressao.comanda.nomeRestaurante = sistema.defaultUser.nomeRestaurante;
                        service.Print(objetoImpressao.comanda, printHost);
                        break;

                    case TipoImpressaoEnum.MESA:
                        objetoImpressao.mesa.nomeRestaurante = sistema.defaultUser.nomeRestaurante;
                        service.Print(objetoImpressao.mesa, printHost);
                        break;

                    case TipoImpressaoEnum.PEDIDO:
                        service.Print(objetoImpressao.pedido, printHost);
                        break;

                    case TipoImpressaoEnum.RELATORIO_PRODUTOS:
                        objetoImpressao.relatorio.nomeRestaurante = sistema.defaultUser.nomeRestaurante;
                        service.Print(objetoImpressao.relatorio, printHost);
                        break;

                    default:
                        throw new Exception("Tipo de impressao desconhecido");
                }

                sistema.service.RemoveFromQueue(objetoImpressao.id);
            }
            catch (Exception e)
            {
                Log($"ERRO - PrintOrder(): tipo de impressão: {objetoImpressao.tipo.ToString()}, impressora: {impressora.nome}({impressora.id}), {sistema.nome} -> {e}");
            }
        }
        */

        #endregion

        private void Log(string msg)
        {
            LogService.GetIntance().Log(msg);
        }

        public List<Sistema> GetSistemas()
        {
            return sistemas;
        }
    }
}
