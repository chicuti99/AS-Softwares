using OrderSheetPrinter.Logs;
using SysGestao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketBE;
using WebSocketBE.Entities;
using WebSocketBE.Enums;

namespace OrderSheetPrinter
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        public static bool LaunchedViaStartup { get; set; }
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Program.LaunchedViaStartup = args != null && args.Any(arg => arg.Contains("startup"));

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += Application_ThreadException;
                Application.Run(new Form1());

                mutex.ReleaseMutex();
            }
        }


        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            frmErro.Erro_Inesperado(e.Exception, ExceptionLog.Insert(e.Exception, "Program.cs"));
        }
    }
}
