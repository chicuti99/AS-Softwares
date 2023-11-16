using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Domain.Entities;
using OrderSheetPrinter.Model;
using OrderSheetPrinter.Model.GarcomDigital;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Services
{
    public class GarcomDigitalService : IOrderService
    {
        private readonly String BASE_URL = Debugger.IsAttached ?  "https://testbackend.takeat.app" : "https://backend.takeat.app";

        private string _token;
        private String TOKEN
        {
            get
            {
                if (_token == null)
                {
                    _token = Sistema.GetLoggedSystem()?.defaultUser?.token;
                }
                return _token;
            }
        }

        public GarcomDigitalService() { }

        public Usuario Login(String usuario, String senha)
        {
            using (var client = new HttpClient())
            {
                InitClient(client, true);

                var login = new Login(usuario, senha);

                using (var response = client.PostAsync("/public/sessions/restaurants", Utils.GetStringContent<Login>(login)).Result)
                {
                    using (var content = response.Content)
                    {

                        if (response.IsSuccessStatusCode)
                        {
                            return Utils.ParseHttpContent<Auth>(content).ToUsuario();
                        }
                        else
                        {
                            throw new Exception(Utils.ParseHttpContent<Error>(content).GetMessage());
                        }
                    }
                }
            }
        }

        public void SetToken(String token)
        {
            _token = token;
        }

        private void InitClient(HttpClient client, bool ignoreToken = false)
        {
            client.BaseAddress = new Uri(BASE_URL);

            if (!ignoreToken)
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TOKEN}");
        }

        public List<ObjetoImpressao> GetOrdersForPrinter(int idImpressora)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                using (var response = client.GetAsync($"/restaurants/printers/printer-queue/{idImpressora}?print_status=pending").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        using (var content = response.Content)
                        {
                            var printItens = Utils.ParseHttpContent<List<PrintItem>>(content);
                            return printItens.ConvertAll<ObjetoImpressao>(x => x.ToObjetoImpressao());
                        }
                    }
                    else
                    {
                        throw new Exception(Utils.GetHttpError(response));
                    }
                }
            }
        }

        public void RemoveFromQueue(int printId)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                using (var response = client.PutAsync($"/restaurants/printers/printer-queue/{printId}/done", null).Result)
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception(Utils.GetHttpError(response));
                }
            }
        }

        public void SavePrinter(Impressora printer)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                using (var response = client.PostAsync("/restaurants/printers", Utils.GetStringContent<Printer>(printer.ToGarcomDigitalPrinter())).Result)
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception(Utils.GetHttpError(response));
                }
            }
        }

        public void UpdatePrinter(Impressora printer)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                using (var response = client.PutAsync($"/restaurants/printers/{printer.id}", Utils.GetStringContent<Printer>(printer.ToGarcomDigitalPrinter())).Result)
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception(Utils.GetHttpError(response));
                }
            }
        }

        public void DeletePrinter(Impressora printer)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                using (var response = client.DeleteAsync($"/restaurants/printers/{printer.id}").Result)
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception(Utils.GetHttpError(response));
                }
            }
        }

        public List<Impressora> GetAvaliablePrinters()
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                using (var response = client.GetAsync("/restaurants/printers").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var content = response.Content)
                        {
                            var printers = Utils.ParseHttpContent<List<Printer>>(content);

                            return printers.ConvertAll<Impressora>(x => x.ToImpressora());
                        }
                    }
                    else
                    {
                        throw new Exception(Utils.GetHttpError(response));
                    }
                }
            }
        }
    }
}
