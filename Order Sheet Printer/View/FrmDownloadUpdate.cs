using MaterialSkin.Controls;
using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSheetPrinter.View
{
    public partial class FrmDownloadUpdate : MaterialForm
    {
        private String url;
        public FrmDownloadUpdate(String url)
        {
            this.url = url;
            InitializeComponent();
        }

        async private void FrmDownloadUpdate_Shown(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    String path = $"{System.IO.Path.GetTempPath()}/OSP";

                    System.IO.Directory.CreateDirectory(path);

                    String name = url.Substring(url.LastIndexOf("/") + 1);

                    if (!name.EndsWith(".msi"))
                        name = "installer.msi";

                    byte[] file = AppService.GetInstance().DownloadUpdate(url);

                    File.WriteAllBytes($"{path}/{name}", file);

                    Process.Start($"{path}/{name}");
                }
                catch
                {
                    Utils.ShowErrorDialog("Não foi possível obter a atualização. Verifique sua conexão com a Internet e tente novamente");
                }
            });

            this.DialogResult = DialogResult.OK;
        }
    }
}
