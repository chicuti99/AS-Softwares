using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSheetPrinter.Services
{
    public class LogService
    {
        private static LogService instance;
        private List<Action<String>> observers = new List<Action<string>>();
        private LogService() { }

        public static LogService GetIntance()
        {
            if (instance == null)
                instance = new LogService();
            return instance;
        }

        public void RemoveObserver(Action<String> observer)
        {
            observers.Remove(observer);
        }

        public void AddObserver(Action<String> observer)
        {
            observers.Add(observer);
        }
        public void Log(String msg)
        {
            msg = $"{Environment.NewLine}{DateTime.Now} - {msg}";

            try
            {
                String path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{System.AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "")}/logs";

                System.IO.Directory.CreateDirectory(path);

                path += $"/log-{DateTime.Now.ToString("yyyy-MM-dd")}.txt";


                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(msg);
                    }
                }

                // This text is always added, making the file longer over time
                // if it is not deleted.
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {
                msg += $"{Environment.NewLine}{ex.Message}";
            }

            foreach (var action in observers)
            {
                try
                {
                    action.Invoke(msg);
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
