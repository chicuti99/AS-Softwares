using OrderSheetPrinter.Domain.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Services
{
    public class LocalStorageService
    {
        private static LocalStorageService instance;
        private readonly String passPhrase = "OSP-2020-11-23";

        private String BASE_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{System.AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "")}/localData";

        private LocalStorageService() { }

        public static LocalStorageService GetInstance()
        {
            if (instance == null)
                instance = new LocalStorageService();
            return instance;
        }

        public void Save<T>(T value, bool encrypt = false)
        {
            Save<T>(value, value.GetType().Name, encrypt);
        }

        public void Save<T>(T value, String id, bool encrypt = false)
        {
            String json = Utils.SerializeJson<T>(value);
            
            if (encrypt)
            {
                json = CryptoService.Encrypt(json, passPhrase);
                id += "-enc";
            }



            SaveToFile(json, id);
        }

        public T Get<T>(bool decrypt = false)
        {
            return Get<T>(typeof(T).Name);
        }

        public T Get<T>(String id, bool decrypt = false)
        {
            try
            {
                if (decrypt)
                    id += "-enc";

                String path = $"{BASE_PATH}/{id}.json";

                var json = File.ReadAllText(path);

                if (decrypt)
                    json = CryptoService.Decrypt(json, passPhrase);

                return Utils.DeserializeJson<T>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return default;
            }
        }

        private void SaveToFile(String json, String id)
        {
            try
            {
                String path = $"{BASE_PATH}/{id}.json";

                System.IO.Directory.CreateDirectory(BASE_PATH);

                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
