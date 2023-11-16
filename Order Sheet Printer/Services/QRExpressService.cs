using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Domain.Entities;
using OrderSheetPrinter.Model;
using OrderSheetPrinter.Model.GarcomDigital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Services
{
    public class QRExpressService : IOrderService
    {
        private readonly String BASE_URL = "";
        private String TOKEN;

        public QRExpressService() {}

        public Usuario Login(String usuario, String senha)
        {
            using (var client = new HttpClient())
            {
                InitClient(client, false);

                var login = new Login(usuario, senha);

                using (var response = client.PostAsync("/public/sessions/restaurants", Utils.GetStringContent<Login>(login)).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var content = response.Content)
                        {
                            return Utils.ParseHttpContent<Auth>(content).ToUsuario();
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new Exception("Usuário ou senha inválidos");
                    }
                    else
                    {
                        throw new Exception(Utils.GetHttpError(response));
                    }
                }
            }
        }

        public void SetToken(String token)
        {
            this.TOKEN = token;
        }


        private void InitClient(HttpClient client, bool ignoreToken = false)
        {
            client.BaseAddress = new Uri(BASE_URL);

            if (!ignoreToken)
                client.DefaultRequestHeaders.Add("Authorization", TOKEN);
        }
        public List<ObjetoImpressao> GetOrdersForPrinter(int idImpressora)
        {
            using (var client = new HttpClient())
            {
                InitClient(client);

                using (var response = client.GetAsync("/impressoras").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var content = response.Content)
                        {
                            return Utils.ParseHttpContent<List<ObjetoImpressao>>(content);
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

                using (var response = client.DeleteAsync($"/impressoras/{printId}").Result)
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

                using (var response = client.PostAsync("/impressora", Utils.GetStringContent<Impressora>(printer)).Result)
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

                using (var response = client.PostAsync($"/restaurants/printers/{printer.id}", Utils.GetStringContent<Printer>(printer.ToGarcomDigitalPrinter())).Result)
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

                using (var response = client.GetAsync("/impressoras").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var content = response.Content)
                        {
                            return Utils.ParseHttpContent<List<Impressora>>(content);
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
