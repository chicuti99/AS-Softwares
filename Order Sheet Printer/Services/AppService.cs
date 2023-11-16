using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Model;
using OrderSheetPrinter.Model.DateTimeAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Services
{
    public class AppService
    {
        private static AppService instance;

        public static AppService GetInstance()
        {
            if (instance == null)
                instance = new AppService();
            return instance;
        }
        private AppService() { }
        public byte[] DownloadUpdate(String url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                using (var response = client.GetAsync("").Result)
                {
                    using (var content = response.Content)
                    {
                        return content.ReadAsByteArrayAsync().Result;
                    }
                }
            }
        }

        public AppVersion GetCurrentVersion()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://backend.takeat.app/public/printer-version");
                using (var response = client.GetAsync("").Result)
                {
                    using (var content = response.Content)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return Utils.ParseHttpContent<AppVersion>(content);
                        }
                        else
                        {
                            throw new Exception($"{response.StatusCode} - {response.ReasonPhrase}");
                        }
                    }
                }
            }
        }

        public DateTime GetCurrentBrasiliaDateTime()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://worldtimeapi.org/api/timezone/America/Sao_Paulo");
                using (var response = client.GetAsync("").Result)
                {
                    using (var content = response.Content)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var worldResponse = Utils.ParseHttpContent<WorldTimeResponse>(content);
                            return worldResponse.datetime;
                        }
                        else
                        {
                            return DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}
