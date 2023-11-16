using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Win32;
using OrderSheetPrinter.Auxiliar;
using OrderSheetPrinter.ConfigEvents;
using OrderSheetPrinter.Controller;
using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Domain.Entities;
using OrderSheetPrinter.Factory;
using OrderSheetPrinter.Model;
using OrderSheetPrinter.Model.GarcomDigital;
using OrderSheetPrinter.Model.ReportsDataSources;
using OrderSheetPrinter.Services;
using OrderSheetPrinter.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketBE;
using WebSocketBE.Entities;
using WebSocketBE.Enums;
using WebSocketBE.Interfaces;

namespace OrderSheetPrinter
{
    public partial class Form1 : MaterialForm
    {
        private bool forceClosing = false;
        private List<Impressora> printers;
        private Impressora selectedPrinter;
        private AppVersion newVersion;
        public static WebSocketServidor webSocketServidor;
        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(848, 380);
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(Color.FromArgb(250, 5, 97), Color.FromArgb(250, 5, 97), Color.FromArgb(250, 5, 97), Color.FromArgb(250, 5, 97), TextShade.WHITE);
            if (System.IO.File.Exists("Interval.txt"))
            {
                timerPrinterService.Interval = Convert.ToInt32(System.IO.File.ReadAllText("Interval.txt"));
                nudIntervalo.Value = timerPrinterService.Interval;
            }

            webSocketServidor = ConfiguracaoWebSocket.CarregarConfiguracoes();
        }

        private void SetStartup()
        {
            try
            {
                string appName = System.AppDomain.CurrentDomain.FriendlyName;
                string cmd = $"\"{Application.ExecutablePath.ToString()}\" -startup";

                RegistryKey rk = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                rk.SetValue(appName, cmd);
            }
            catch
            {

            }
        }

        private void DeleteUpdateInstaller()
        {
            try
            {
                var installerPath = $"{System.IO.Path.GetTempPath()}/OSP";
                if (Directory.Exists(installerPath))
                    Directory.Delete(installerPath, true);
            }
            catch
            {

            }
        }

        private void CheckLoginActive()
        {
            try
            {
                String lastLogin = LocalStorageService.GetInstance().Get<String>("LAST_LOGIN");

                if (!String.IsNullOrEmpty(lastLogin))
                {
                    SistemaEnum sistemaEnum = SistemaEnum.QR_EXPRESS.ToString() == lastLogin ? SistemaEnum.QR_EXPRESS : SistemaEnum.GARCOM_DIGITAL;
                    Sistema sistema = Sistema.Create(sistemaEnum);
                    sistema.defaultUser = CredentialService.Get(sistema.type);
                    Sistema.SetLoggedSystem(sistema);
                }
            }
            catch
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // se o evento for disparado por interação do usuário, minizar para o System Tray
            if (e.CloseReason == CloseReason.UserClosing && !forceClosing)
            {
                if (MessageBox.Show("Deseja encerrar a aplicação ?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Process.GetCurrentProcess().Kill();
                }
                else
                    e.Cancel = true;
                #region OLD
                /*else
                {
                    e.Cancel = true;
                    this.WindowState = FormWindowState.Minimized;
                    Hide();
                }*/
                #endregion
            }
        }


        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (Sistema.GetLoggedSystem() != null || GetLogin())
            {
                Program.LaunchedViaStartup = false;

                LoadPrinters();

                btnCheckUpdate.Visible = newVersion == null;
                btnUpdate.Visible = newVersion != null;

                Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void timerPrinterService_Tick(object sender, EventArgs e)
        {
            FluxoImpressao.GetInstance().Execute();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if (!DEBUG)
            SetStartup();
#endif
            DeleteUpdateInstaller();
            CheckLoginActive();

        }

        private bool GetLogin()
        {
            var frmLogin = new FrmLoginV2();
            return frmLogin.ShowDialog() == DialogResult.OK;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja encerrar a aplicação ?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LocalStorageService.GetInstance().Save<String>(null, "LAST_LOGIN");
                Process.GetCurrentProcess().Kill();
            }

        }

        private void btnAddPrinter_Click(object sender, EventArgs e)
        {
            OpenAddPrinterDialog();
        }

        private void OpenAddPrinterDialog(Impressora impressora = null)
        {
            var frmPrinter = new FrmCadastroImpressora(impressora);
            if (frmPrinter.ShowDialog() == DialogResult.OK)
                LoadPrinters();
        }

        private void LoadPrinters()
        {
            try
            {
                var loggedSystem = Sistema.GetLoggedSystem();

                if (loggedSystem.defaultUser.token == null)
                {
                    var user = loggedSystem.service.Login(loggedSystem.defaultUser.usuario, loggedSystem.defaultUser.senha);
                    user.senha = loggedSystem.defaultUser.senha;
                    loggedSystem.service.SetToken(user.token);
                    loggedSystem.defaultUser = user;
                }

                printers = Sistema.GetLoggedSystem().service.GetAvaliablePrinters();
            }
            catch (Exception e)
            {
                printers = new List<Impressora>();
            }

            materialListView1.Items.Clear();

            if (printers != null && printers.Count > 0)
                printers.Sort((a, b) => a.nome.CompareTo(b.nome));

            foreach (var impressora in printers)
            {
                materialListView1.Items.Add(GetViewItemForPrinter(impressora));
            }
        }

        private ListViewItem GetViewItemForPrinter(Impressora impressora)
        {
            var item = new ListViewItem();
            item.Text = impressora.id.ToString();

            item.SubItems.Add(impressora.nome);
            item.SubItems.Add(impressora.host);

            return item;
        }

        private void materialListView1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < materialListView1.Items.Count; i++)
            {
                var rectangle = materialListView1.GetItemRect(i);
                if (rectangle.Contains(e.Location))
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (materialListView1.FocusedItem.Bounds.Contains(e.Location))
                        {
                            selectedPrinter = printers.FirstOrDefault(x => x.id == Convert.ToInt32(materialListView1.FocusedItem.Text));
                            materialContextMenuStrip1.Show(Cursor.Position);
                            return;
                        }
                    }
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Hide();
            if (Sistema.GetLoggedSystem() != null || GetLogin())
            {
                LoadPrinters();

                if (!Program.LaunchedViaStartup)
                {
                    Show();
                }

                btnCheckUpdate.Visible = newVersion == null;
                btnUpdate.Visible = newVersion != null;
            }
            CarregarPreferenciasServidor();
        }

        private void CarregarPreferenciasServidor()
        {
            EnableOrDisablePrinterDefault(false);
        }

        private void DownloadUpdate()
        {
            try
            {
                var frmUpdate = new FrmDownloadUpdate(newVersion.download_link);
                frmUpdate.ShowDialog();
            }
            catch
            {
                Utils.ShowErrorDialog("Não foi possível obter a atualização");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DownloadUpdate();
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDialogRemoverImpressora();
        }

        private void OpenDialogRemoverImpressora()
        {
            if (Utils.ShowDialog("Remover impressora?", "Excluir a impressora selecionada?", MessageBoxIcon.Question, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    Sistema.GetLoggedSystem().service.DeletePrinter(selectedPrinter);
                    LoadPrinters();
                }
                catch (Exception e)
                {
                    Utils.ShowErrorDialog(e.Message);
                }
            }
        }

        private void materialListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < materialListView1.Items.Count; i++)
            {
                var rectangle = materialListView1.GetItemRect(i);
                if (rectangle.Contains(e.Location))
                {
                    OpenAddPrinterDialog(printers[i]);
                    return;
                }
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void nudIntervalo_ValueChanged(object sender, EventArgs e)
        {
            var text = nudIntervalo.Value.ToString();
            File.WriteAllText("Interval.txt", text);
            timerPrinterService.Interval = Convert.ToInt32(nudIntervalo.Value * 1000);
        }

        private async void timerUpdateCheck_Tick(object sender, EventArgs e)
        {
           // await Task.Run(async () =>
           // {
                await CheckUpdate();
           // });
        }

        private async void CheckUpdateManually()
        {
            btnCheckUpdate.Enabled = false;
            await CheckUpdate();
            btnCheckUpdate.Enabled = true;

            btnUpdate.Visible = newVersion != null;
            btnCheckUpdate.Visible = newVersion == null;

            //Utils.ShowDialog("Buscar atualização", newVersion != null ? "Uma nova versão está disponível. Clique em \"Baixar atualização\" para fazer o download" : "Nenhuma atualização disponível", MessageBoxIcon.Information, MessageBoxButtons.OK);
            if (newVersion == null)
                Utils.ShowDialog("Buscar atualização", "Nenhuma atualização disponível", MessageBoxIcon.Information, MessageBoxButtons.OK);
        }

        private async Task CheckUpdate()
        {
            try
            {
                String currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var availableVersion = AppService.GetInstance().GetCurrentVersion();

                // se a versão atual for menor que a versão disponível
                if (currentVersion.CompareTo(availableVersion.printer_version) == -1)
                {
                    newVersion = availableVersion;
                    btnUpdate.Visible = true;
                    btnCheckUpdate.Visible = false;

                    ShowUpdateNotification();
                }
            }
            catch
            {

            }
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            //f.Location;
            CheckUpdateManually();
        }

        private void ShowUpdateNotification()
        {
            try
            {
                notifyIcon1.BalloonTipClicked += (sender, args) =>
                {
                    if (this.WindowState == FormWindowState.Minimized)
                        this.WindowState = FormWindowState.Normal;
                };

                notifyIcon1.ShowBalloonTip(5000, "Atualização disponível", "Uma nova versão está disponível para download", ToolTipIcon.Info);
            }
            catch
            {

            }
        }
        #region PRINTERS CHUMBADO
        //private void PrintPedido()
        //{
        //    String json = "{\"id\":519,\"print_status\":\"done\",\"printed_at\":\"2021-01-15T13:30:22.815Z\",\"print_type\":\"order\",\"filter_start_date\":null,\"filter_end_date\":null,\"print_object\":{\"id\":21243,\"order_status\":\"accepted\",\"basket_id\":\"046-9153\",\"total_price\":\"120.00\",\"total_service_price\":\"120.00\",\"start_time\":\"2021-01-15T13:29:29.358Z\",\"close_time\":null,\"canceled_at\":null,\"bill\":{\"id\":11549,\"total_price\":\"120.00\",\"total_service_price\":\"120.00\",\"payment_method\":1,\"service_tax\":false,\"start_time\":\"2021-01-15T13:29:29.344Z\",\"close_time\":null,\"waiter\":null,\"buyer\":{\"id\":46,\"name\":null,\"phone\":\"(00) 00000-0001\",\"email\":null,\"buy_count\":2,\"createdAt\":\"2020-07-03T15:54:28.788Z\",\"updatedAt\":\"2021-01-15T13:29:29.353Z\"}},\"orders\":[{\"id\":31949,\"details\":\"teste1\",\"amount\":1,\"price\":\"60.00\",\"total_price\":\"60.00\",\"total_service_price\":\"60.00\",\"product\":{\"id\":11,\"name\":\"Les Trois Musketeers\",\"description\":\"03 estrelas de nossa culin\u00E1ria em um mesmo prato por tempo limitado. Bife de ancho bovino babybeef, acompanhado de camar\u00F5es m\u00E9dios e aspargos\",\"price\":\"79.90\",\"price_promotion\":\"60.00\",\"category\":{\"id\":2,\"name\":\"Prato Principal\",\"preparation_time\":35,\"custom_order\":100,\"available\":true,\"deleted_at\":null,\"createdAt\":\"2020-06-24T15:04:34.519Z\",\"updatedAt\":\"2020-07-07T00:15:27.062Z\",\"restaurant_id\":2}},\"complement_categories\":[{\"id\":18362,\"created_at\":\"2021-01-15T13:29:29.373Z\",\"complement_category\":{\"id\":5,\"name\":\"Ponto da Carne\",\"available\":true,\"question\":\"Ponto da Carne\"},\"order_complements\":[{\"id\":19471,\"amount\":1,\"complement\":{\"id\":10,\"name\":\"Bem Passada\",\"price\":\"0.00\"}}]}]},{\"id\":31950,\"details\":\"teste2\",\"amount\":1,\"price\":\"60.00\",\"total_price\":\"60.00\",\"total_service_price\":\"60.00\",\"product\":{\"id\":11,\"name\":\"Les Trois Musketeers\",\"description\":\"03 estrelas de nossa culin\u00E1ria em um mesmo prato por tempo limitado. Bife de ancho bovino babybeef, acompanhado de camar\u00F5es m\u00E9dios e aspargos\",\"price\":\"79.90\",\"price_promotion\":\"60.00\",\"category\":{\"id\":2,\"name\":\"Prato Principal\",\"preparation_time\":35,\"custom_order\":100,\"available\":true,\"deleted_at\":null,\"createdAt\":\"2020-06-24T15:04:34.519Z\",\"updatedAt\":\"2020-07-07T00:15:27.062Z\",\"restaurant_id\":2}},\"complement_categories\":[{\"id\":18363,\"created_at\":\"2021-01-15T13:29:29.400Z\",\"complement_category\":{\"id\":5,\"name\":\"Ponto da Carne\",\"available\":true,\"question\":\"Ponto da Carne\"},\"order_complements\":[{\"id\":19472,\"amount\":1,\"complement\":{\"id\":12,\"name\":\"Ao Ponto\",\"price\":\"0.00\"}}]}]}],\"table\":{\"id\":4,\"status\":\"ongoing\",\"table_number\":3,\"deleted_at\":null,\"createdAt\":\"2020-06-30T21:01:05.102Z\",\"updatedAt\":\"2021-01-15T13:29:29.296Z\",\"restaurant_id\":2,\"table_code_id\":328}}}";

        //    var item = Utils.DeserializeJson<PrintItem>(json);

        //    var obj = item.ToObjetoImpressao();

        //    PrinterService.GetInstance().Print(obj.pedido, "Microsoft Print to PDF");
        //}

        //private void PrintComanda()
        //{
        //    String json = "{\"id\":298,\"print_status\":\"pending\",\"printed_at\":null,\"print_type\":\"individual_bill\",\"filter_start_date\":null,\"filter_end_date\":null,\"print_object\":{\"id\":484,\"total_price\":\"187.80\",\"total_service_price\":\"206.58\",\"payment_method\":1,\"service_tax\":true,\"start_time\":\"2021-01-07T12:44:23.639Z\",\"close_time\":\"2021-01-07T12:55:21.809Z\",\"session\":{\"id\":306,\"key\":\"8830225d-5a88-4a63-8d5f-8d5e63f2eafd\",\"total_price\":\"202.80\",\"total_service_price\":\"223.08\",\"start_time\":\"2021-01-07T12:44:23.592Z\",\"end_time\":\"2021-01-07T12:55:21.809Z\",\"status\":\"finished\",\"createdAt\":\"2021-01-07T12:44:23.594Z\",\"updatedAt\":\"2021-01-07T12:55:21.810Z\",\"restaurant_id\":2,\"table_id\":27,\"table\":{\"id\":27,\"status\":\"ongoing\",\"table_number\":132,\"deleted_at\":null,\"createdAt\":\"2020-07-21T19:46:28.002Z\",\"updatedAt\":\"2021-01-07T13:17:24.253Z\",\"restaurant_id\":2,\"table_code_id\":30}},\"buyer\":{\"phone\":\"(21) 11111-1111\"},\"waiter\":null,\"order_baskets\":[{\"id\":761,\"order_status\":\"finished\",\"basket_id\":\"025-809\",\"total_price\":\"107.90\",\"total_service_price\":\"118.69\",\"start_time\":\"2021-01-07T12:46:39.390Z\",\"close_time\":\"2021-01-07T12:55:21.809Z\",\"canceled_at\":null,\"createdAt\":\"2021-01-07T12:46:39.390Z\",\"updatedAt\":\"2021-01-07T12:55:21.842Z\",\"bill_id\":484,\"orders\":[{\"id\":869,\"details\":\"\",\"amount\":1,\"price\":\"89.90\",\"total_price\":\"107.90\",\"total_service_price\":\"118.69\",\"product\":{\"id\":6,\"name\":\"Filet Mignon R\u00FAstico\",\"description\":\"Corte nobre de Filet Mignon, preparado ao molho madeira com alecrim e mix de pimentas ex\u00F3ticas. Acompanha batatas r\u00FAsticas salteadas no azeite\",\"price\":\"89.90\",\"price_promotion\":null},\"complement_categories\":[{\"id\":467,\"complement_category\":{\"id\":39,\"name\":\"Complemento de Carne\",\"available\":true,\"question\":\"Qual complemento?\"},\"order_complements\":[{\"id\":497,\"amount\":2,\"complement\":{\"id\":55,\"name\":\"Cheddar\",\"price\":\"3.00\"}},{\"id\":498,\"amount\":1,\"complement\":{\"id\":54,\"name\":\"Queijo\",\"price\":\"2.00\"}}]},{\"id\":468,\"complement_category\":{\"id\":40,\"name\":\"Bebidas\",\"available\":true,\"question\":\"Bebidas?\"},\"order_complements\":[{\"id\":499,\"amount\":2,\"complement\":{\"id\":56,\"name\":\"Coca Cola\",\"price\":\"5.00\"}}]}]}]},{\"id\":759,\"order_status\":\"finished\",\"basket_id\":\"025-571\",\"total_price\":\"79.90\",\"total_service_price\":\"87.89\",\"start_time\":\"2021-01-07T12:44:23.654Z\",\"close_time\":\"2021-01-07T12:55:21.809Z\",\"canceled_at\":null,\"createdAt\":\"2021-01-07T12:44:23.655Z\",\"updatedAt\":\"2021-01-07T12:55:21.841Z\",\"bill_id\":484,\"orders\":[{\"id\":867,\"details\":\"\",\"amount\":1,\"price\":\"79.90\",\"total_price\":\"79.90\",\"total_service_price\":\"87.89\",\"product\":{\"id\":5,\"name\":\"testeproduto\",\"description\":\"03 estrelas de nossa culin\u00E1ria em um mesmo prato por tempo limitado. Bife de ancho bovino babybeef, acompanhado de camar\u00F5es m\u00E9dios e aspargos\",\"price\":\"79.90\",\"price_promotion\":null},\"complement_categories\":[]}]}]}}";

        //    var item = Utils.DeserializeJson<PrintItem>(json);

        //    var obj = item.ToObjetoImpressao();

        //    PrinterService.GetInstance().Print(obj.comanda, "Microsoft Print to PDF");
        //}

        //private void PrintMesa()
        //{
        //    String json = "{\"id\":311,\"print_status\":\"pending\",\"printed_at\":null,\"print_type\":\"table_session\",\"filter_start_date\":null,\"filter_end_date\":null,\"print_object\":{\"id\":307,\"total_price\":\"285.80\",\"total_service_price\":\"314.38\",\"start_time\":\"2021-01-07T13:06:58.499Z\",\"end_time\":\"2021-01-07T13:11:02.601Z\",\"status\":\"finished\",\"table\":{\"id\":27,\"status\":\"ongoing\",\"table_number\":132,\"deleted_at\":null,\"createdAt\":\"2020-07-21T19:46:28.002Z\",\"updatedAt\":\"2021-01-07T13:17:24.253Z\",\"restaurant_id\":2,\"table_code_id\":30},\"bills\":[{\"id\":486,\"total_price\":\"128.90\",\"total_service_price\":\"141.79\",\"status\":\"finished\",\"service_tax\":true,\"start_time\":\"2021-01-07T13:06:58.561Z\",\"close_time\":\"2021-01-07T13:11:02.601Z\",\"buyer\":{\"phone\":\"(22) 11111-1110\"},\"waiter\":null,\"order_baskets\":[{\"id\":762,\"order_status\":\"finished\",\"basket_id\":\"291-6794\",\"total_price\":\"128.90\",\"total_service_price\":\"141.79\",\"start_time\":\"2021-01-07T13:06:58.577Z\",\"close_time\":\"2021-01-07T13:11:02.601Z\",\"canceled_at\":null,\"createdAt\":\"2021-01-07T13:06:58.577Z\",\"updatedAt\":\"2021-01-07T13:11:02.632Z\",\"bill_id\":486,\"orders\":[{\"id\":871,\"details\":\"\",\"amount\":1,\"price\":\"89.90\",\"total_price\":\"99.90\",\"total_service_price\":\"109.89\",\"product\":{\"id\":6,\"name\":\"Filet Mignon R\u00FAstico\",\"description\":\"Corte nobre de Filet Mignon, preparado ao molho madeira com alecrim e mix de pimentas ex\u00F3ticas. Acompanha batatas r\u00FAsticas salteadas no azeite\",\"price\":\"89.90\",\"price_promotion\":null},\"complement_categories\":[{\"id\":469,\"complement_category\":{\"id\":39,\"name\":\"Complemento de Carne\",\"available\":true,\"question\":\"Qual complemento?\"},\"order_complements\":[{\"id\":500,\"amount\":1,\"complement\":{\"id\":55,\"name\":\"Cheddar\",\"price\":\"3.00\"}},{\"id\":501,\"amount\":1,\"complement\":{\"id\":54,\"name\":\"Queijo\",\"price\":\"2.00\"}}]},{\"id\":470,\"complement_category\":{\"id\":40,\"name\":\"Bebidas\",\"available\":true,\"question\":\"Bebidas?\"},\"order_complements\":[{\"id\":502,\"amount\":1,\"complement\":{\"id\":56,\"name\":\"Coca Cola\",\"price\":\"5.00\"}}]}]},{\"id\":870,\"details\":\"\",\"amount\":1,\"price\":\"9.00\",\"total_price\":\"9.00\",\"total_service_price\":\"9.90\",\"product\":{\"id\":2,\"name\":\"Fritas r\u00FAsticas\",\"description\":\"Batata frita r\u00FAstica\",\"price\":\"10.00\",\"price_promotion\":\"9.00\"},\"complement_categories\":[]},{\"id\":872,\"details\":\"\",\"amount\":1,\"price\":\"20.00\",\"total_price\":\"20.00\",\"total_service_price\":\"22.00\",\"product\":{\"id\":7,\"name\":\"Petit gateau\",\"description\":\"Petit gateau com sorvete de creme\",\"price\":\"25.00\",\"price_promotion\":\"20.00\"},\"complement_categories\":[]}]}]},{\"id\":487,\"total_price\":\"156.90\",\"total_service_price\":\"172.59\",\"status\":\"finished\",\"service_tax\":true,\"start_time\":\"2021-01-07T13:08:58.837Z\",\"close_time\":\"2021-01-07T13:11:02.601Z\",\"buyer\":{\"phone\":\"(22) 22222-2220\"},\"waiter\":null,\"order_baskets\":[{\"id\":763,\"order_status\":\"finished\",\"basket_id\":\"292-220\",\"total_price\":\"156.90\",\"total_service_price\":\"172.59\",\"start_time\":\"2021-01-07T13:08:58.850Z\",\"close_time\":\"2021-01-07T13:11:02.601Z\",\"canceled_at\":null,\"createdAt\":\"2021-01-07T13:08:58.851Z\",\"updatedAt\":\"2021-01-07T13:11:02.643Z\",\"bill_id\":487,\"orders\":[{\"id\":874,\"details\":\"\",\"amount\":1,\"price\":\"89.90\",\"total_price\":\"108.90\",\"total_service_price\":\"119.79\",\"product\":{\"id\":6,\"name\":\"Filet Mignon R\u00FAstico\",\"description\":\"Corte nobre de Filet Mignon, preparado ao molho madeira com alecrim e mix de pimentas ex\u00F3ticas. Acompanha batatas r\u00FAsticas salteadas no azeite\",\"price\":\"89.90\",\"price_promotion\":null},\"complement_categories\":[{\"id\":471,\"complement_category\":{\"id\":39,\"name\":\"Complemento de Carne\",\"available\":true,\"question\":\"Qual complemento?\"},\"order_complements\":[{\"id\":503,\"amount\":3,\"complement\":{\"id\":55,\"name\":\"Cheddar\",\"price\":\"3.00\"}}]},{\"id\":472,\"complement_category\":{\"id\":40,\"name\":\"Bebidas\",\"available\":true,\"question\":\"Bebidas?\"},\"order_complements\":[{\"id\":504,\"amount\":2,\"complement\":{\"id\":56,\"name\":\"Coca Cola\",\"price\":\"5.00\"}}]}]},{\"id\":873,\"details\":\"sal\",\"amount\":2,\"price\":\"9.00\",\"total_price\":\"18.00\",\"total_service_price\":\"19.80\",\"product\":{\"id\":2,\"name\":\"Fritas r\u00FAsticas\",\"description\":\"Batata frita r\u00FAstica\",\"price\":\"10.00\",\"price_promotion\":\"9.00\"},\"complement_categories\":[]},{\"id\":875,\"details\":\"chocolate\",\"amount\":2,\"price\":\"15.00\",\"total_price\":\"30.00\",\"total_service_price\":\"33.00\",\"product\":{\"id\":8,\"name\":\"Sorvete\",\"description\":\"Sorvete de v\u00E1rios sabores\",\"price\":\"15.00\",\"price_promotion\":null},\"complement_categories\":[]}]}]}],\"payments\":[],\"total_paid\":0}}";

        //    var item = Utils.DeserializeJson<PrintItem>(json);

        //    var obj = item.ToObjetoImpressao();

        //    PrinterService.GetInstance().Print(obj.mesa, "Microsoft Print to PDF");
        //}

        //private void PrintRelatorio()
        //{
        //    String json = "{\"id\":140,\"print_status\":\"done\",\"printed_at\":\"2020-12-22T22:20:43.260Z\",\"print_type\":\"orders-report\",\"filter_start_date\":\"2020-12-22T00:00:00.000Z\",\"filter_end_date\":\"2020-12-22T23:59:59.000Z\",\"print_object\":{\"orders_report\":{\"categoriesReport\":[{\"id\":4,\"name\":\"Sobremesas\",\"preparation_time\":10,\"products\":[{\"id\":7,\"name\":\"Petit gateau\",\"description\":\"Petit gateau com sorvete de creme\",\"price\":\"25.00\",\"price_promotion\":\"20.00\",\"sold_off\":false,\"request_counter\":0,\"promotion\":null,\"available\":true,\"custom_order\":100,\"deleted_at\":null,\"createdAt\":\"2020-07-10T16:39:26.516Z\",\"updatedAt\":\"2020-07-10T16:39:26.516Z\",\"product_category_id\":4,\"restaurant_id\":2,\"image_id\":null,\"thumbnail_id\":null,\"orders\":[{\"id\":825,\"details\":\"\",\"amount\":1,\"price\":\"20.00\",\"complements_price\":\"0.00\",\"total_price\":\"20.00\",\"total_service_price\":\"22.00\",\"canceled_at\":null,\"waiter_checked\":false,\"createdAt\":\"2020-12-22T14:05:54.426Z\",\"updatedAt\":\"2020-12-22T14:05:54.426Z\",\"product_id\":7,\"order_basket_id\":720}],\"total_sold_product_price\":\"20.00\",\"total_sold_product__service_price\":\"22.00\",\"total_sold_product_amount\":1}],\"total_sold_orders_price\":\"20.00\",\"total_sold_orders_service_price\":\"22.00\",\"total_sold_orders_amount\":1},{\"id\":23,\"name\":\"Bebidas\",\"preparation_time\":5,\"products\":[],\"total_sold_orders_price\":\"0.00\",\"total_sold_orders_service_price\":\"0.00\",\"total_sold_orders_amount\":0,\"total_orders_price\":0,\"total_orders_service_price\":0,\"total_orders_amount\":0},{\"id\":2,\"name\":\"Prato Principal\",\"preparation_time\":35,\"products\":[{\"id\":4,\"name\":\"Hamburger\",\"description\":\"P\u00E3o, carne, alface e tomate, queijo\",\"price\":\"20.00\",\"price_promotion\":null,\"sold_off\":false,\"request_counter\":0,\"promotion\":\"Sugest\u00E3o da casa\",\"available\":true,\"custom_order\":100,\"deleted_at\":null,\"createdAt\":\"2020-07-10T16:39:26.516Z\",\"updatedAt\":\"2020-07-10T16:39:26.516Z\",\"product_category_id\":2,\"restaurant_id\":2,\"image_id\":null,\"thumbnail_id\":null,\"orders\":[{\"id\":826,\"details\":\"\",\"amount\":1,\"price\":\"20.00\",\"complements_price\":\"2.00\",\"total_price\":\"22.00\",\"total_service_price\":\"24.20\",\"canceled_at\":null,\"waiter_checked\":false,\"createdAt\":\"2020-12-22T14:25:21.047Z\",\"updatedAt\":\"2020-12-22T14:25:21.047Z\",\"product_id\":4,\"order_basket_id\":721},{\"id\":823,\"details\":\"\",\"amount\":1,\"price\":\"20.00\",\"complements_price\":\"4.00\",\"total_price\":\"24.00\",\"total_service_price\":\"26.40\",\"canceled_at\":null,\"waiter_checked\":false,\"createdAt\":\"2020-12-22T14:05:54.354Z\",\"updatedAt\":\"2020-12-22T14:05:54.354Z\",\"product_id\":4,\"order_basket_id\":720}],\"total_sold_product_price\":\"46.00\",\"total_sold_product__service_price\":\"50.60\",\"total_sold_product_amount\":2},{\"id\":6,\"name\":\"Filet Mignon R\u00FAstico\",\"description\":\"Corte nobre de Filet Mignon, preparado ao molho madeira com alecrim e mix de pimentas ex\u00F3ticas. Acompanha batatas r\u00FAsticas salteadas no azeite\",\"price\":\"89.90\",\"price_promotion\":null,\"sold_off\":false,\"request_counter\":0,\"promotion\":\"\",\"available\":true,\"custom_order\":100,\"deleted_at\":null,\"createdAt\":\"2020-07-10T16:39:26.516Z\",\"updatedAt\":\"2020-07-29T12:07:07.083Z\",\"product_category_id\":2,\"restaurant_id\":2,\"image_id\":31,\"thumbnail_id\":null,\"orders\":[{\"id\":824,\"details\":\"\",\"amount\":1,\"price\":\"89.90\",\"complements_price\":\"0.00\",\"total_price\":\"89.90\",\"total_service_price\":\"98.89\",\"canceled_at\":null,\"waiter_checked\":false,\"createdAt\":\"2020-12-22T14:05:54.421Z\",\"updatedAt\":\"2020-12-22T14:05:54.421Z\",\"product_id\":6,\"order_basket_id\":720}],\"total_sold_product_price\":\"89.90\",\"total_sold_product__service_price\":\"98.89\",\"total_sold_product_amount\":1},{\"id\":5,\"name\":\"testeproduto\",\"description\":\"03 estrelas de nossa culin\u00E1ria em um mesmo prato por tempo limitado. Bife de ancho bovino babybeef, acompanhado de camar\u00F5es m\u00E9dios e aspargos\",\"price\":\"79.90\",\"price_promotion\":null,\"sold_off\":false,\"request_counter\":0,\"promotion\":\"\",\"available\":true,\"custom_order\":100,\"deleted_at\":null,\"createdAt\":\"2020-07-10T16:39:26.516Z\",\"updatedAt\":\"2020-09-22T20:30:25.135Z\",\"product_category_id\":2,\"restaurant_id\":2,\"image_id\":30,\"thumbnail_id\":null,\"orders\":[{\"id\":822,\"details\":\"\",\"amount\":1,\"price\":\"79.90\",\"complements_price\":\"0.00\",\"total_price\":\"79.90\",\"total_service_price\":\"87.89\",\"canceled_at\":null,\"waiter_checked\":false,\"createdAt\":\"2020-12-22T13:50:53.856Z\",\"updatedAt\":\"2020-12-22T13:50:53.856Z\",\"product_id\":5,\"order_basket_id\":719}],\"total_sold_product_price\":\"79.90\",\"total_sold_product__service_price\":\"87.89\",\"total_sold_product_amount\":1}],\"total_sold_orders_price\":\"215.80\",\"total_sold_orders_service_price\":\"237.38\",\"total_sold_orders_amount\":4},{\"id\":24,\"name\":\"Promo\u00E7\u00F5est\",\"preparation_time\":8,\"products\":[{\"id\":2,\"name\":\"Fritas r\u00FAsticas\",\"description\":\"Batata frita r\u00FAstica\",\"price\":\"10.00\",\"price_promotion\":\"9.00\",\"sold_off\":false,\"request_counter\":0,\"promotion\":\"\",\"available\":true,\"custom_order\":100,\"deleted_at\":null,\"createdAt\":\"2020-07-10T16:39:26.516Z\",\"updatedAt\":\"2020-08-12T18:25:54.241Z\",\"product_category_id\":24,\"restaurant_id\":2,\"image_id\":null,\"thumbnail_id\":null,\"orders\":[{\"id\":827,\"details\":\"\",\"amount\":1,\"price\":\"9.00\",\"complements_price\":\"0.00\",\"total_price\":\"9.00\",\"total_service_price\":\"9.90\",\"canceled_at\":null,\"waiter_checked\":false,\"createdAt\":\"2020-12-22T21:50:50.583Z\",\"updatedAt\":\"2020-12-22T21:50:50.583Z\",\"product_id\":2,\"order_basket_id\":722}],\"total_sold_product_price\":\"9.00\",\"total_sold_product__service_price\":\"9.90\",\"total_sold_product_amount\":1}],\"total_sold_orders_price\":\"9.00\",\"total_sold_orders_service_price\":\"9.90\",\"total_sold_orders_amount\":1},{\"id\":1,\"name\":\"Entradas\",\"preparation_time\":20,\"products\":[],\"total_sold_orders_price\":\"0.00\",\"total_sold_orders_service_price\":\"0.00\",\"total_sold_orders_amount\":0,\"total_orders_price\":0,\"total_orders_service_price\":0,\"total_orders_amount\":0},{\"id\":30,\"name\":\"teste3\",\"preparation_time\":45,\"products\":[],\"total_sold_orders_price\":\"0.00\",\"total_sold_orders_service_price\":\"0.00\",\"total_sold_orders_amount\":0,\"total_orders_price\":0,\"total_orders_service_price\":0,\"total_orders_amount\":0}],\"total_restaurant_price\":\"244.80\",\"total_restaurant_service_price\":\"269.28\",\"total_restaurant_amount\":6,\"canceledCategoriesReport\":[{\"id\":4,\"name\":\"Sobremesas\",\"preparation_time\":10,\"products\":[],\"total_canceled_orders_price\":\"0.00\",\"total_canceled_orders_service_price\":\"0.00\",\"total_canceled_orders_amount\":0,\"total_orders_price\":0,\"total_orders_service_price\":0,\"total_orders_amount\":0},{\"id\":23,\"name\":\"Bebidas\",\"preparation_time\":5,\"products\":[],\"total_canceled_orders_price\":\"0.00\",\"total_canceled_orders_service_price\":\"0.00\",\"total_canceled_orders_amount\":0,\"total_orders_price\":0,\"total_orders_service_price\":0,\"total_orders_amount\":0},{\"id\":2,\"name\":\"Prato Principal\",\"preparation_time\":35,\"products\":[],\"total_canceled_orders_price\":\"0.00\",\"total_canceled_orders_service_price\":\"0.00\",\"total_canceled_orders_amount\":0,\"total_orders_price\":0,\"total_orders_service_price\":0,\"total_orders_amount\":0},{\"id\":24,\"name\":\"Promo\u00E7\u00F5est\",\"preparation_time\":8,\"products\":[],\"total_canceled_orders_price\":\"0.00\",\"total_canceled_orders_service_price\":\"0.00\",\"total_canceled_orders_amount\":0,\"total_orders_price\":0,\"total_orders_service_price\":0,\"total_orders_amount\":0},{\"id\":1,\"name\":\"Entradas\",\"preparation_time\":20,\"products\":[],\"total_canceled_orders_price\":\"0.00\",\"total_canceled_orders_service_price\":\"0.00\",\"total_canceled_orders_amount\":0,\"total_orders_price\":0,\"total_orders_service_price\":0,\"total_orders_amount\":0},{\"id\":30,\"name\":\"teste3\",\"preparation_time\":45,\"products\":[],\"total_canceled_orders_price\":\"0.00\",\"total_canceled_orders_service_price\":\"0.00\",\"total_canceled_orders_amount\":0,\"total_orders_price\":0,\"total_orders_service_price\":0,\"total_orders_amount\":0}],\"total_restaurant_canceled_price\":\"0.00\",\"total_restaurant_canceled_service_price\":\"0.00\",\"total_restaurant_canceled_amount\":0},\"cashier_report\":{\"paymentsReport\":[{\"id\":10,\"name\":\"Dinheiro\",\"available\":true,\"payments\":[{\"id\":129,\"payment_value\":\"10.00\",\"createdAt\":\"2020-12-22T14:14:56.723Z\",\"updatedAt\":\"2020-12-22T14:14:56.723Z\",\"table_session_id\":281,\"payment_method_id\":10,\"restaurant_id\":2},{\"id\":132,\"payment_value\":\"13.00\",\"createdAt\":\"2020-12-22T14:15:39.037Z\",\"updatedAt\":\"2020-12-22T14:15:39.037Z\",\"table_session_id\":280,\"payment_method_id\":10,\"restaurant_id\":2}],\"total_payments_price\":\"23.00\"},{\"id\":11,\"name\":\"Picpay\",\"available\":true,\"payments\":[{\"id\":130,\"payment_value\":\"150.00\",\"createdAt\":\"2020-12-22T14:15:04.173Z\",\"updatedAt\":\"2020-12-22T14:15:04.173Z\",\"table_session_id\":279,\"payment_method_id\":11,\"restaurant_id\":2}],\"total_payments_price\":\"150.00\"},{\"id\":20,\"name\":\"Dinheiro\",\"available\":true,\"payments\":[{\"id\":131,\"payment_value\":\"240.00\",\"createdAt\":\"2020-12-22T14:15:16.322Z\",\"updatedAt\":\"2020-12-22T14:15:16.322Z\",\"table_session_id\":277,\"payment_method_id\":20,\"restaurant_id\":2}],\"total_payments_price\":\"240.00\"}],\"total_payment_methods_price\":413},\"buyers_report\":{\"buyers\":[{\"id\":261,\"name\":null,\"phone\":\"(26) 55555-5577\",\"email\":null,\"buy_count\":2,\"createdAt\":\"2020-12-22T13:49:51.389Z\",\"updatedAt\":\"2020-12-22T13:50:53.838Z\"},{\"id\":262,\"name\":null,\"phone\":\"(25) 66655-5557\",\"email\":null,\"buy_count\":2,\"createdAt\":\"2020-12-22T14:05:53.237Z\",\"updatedAt\":\"2020-12-22T14:05:54.344Z\"},{\"id\":263,\"name\":null,\"phone\":\"(26) 55874-2211\",\"email\":null,\"buy_count\":2,\"createdAt\":\"2020-12-22T14:25:20.000Z\",\"updatedAt\":\"2020-12-22T14:25:21.033Z\"},{\"id\":264,\"name\":null,\"phone\":\"(26) 55524-7522\",\"email\":null,\"buy_count\":2,\"createdAt\":\"2020-12-22T21:50:49.559Z\",\"updatedAt\":\"2020-12-22T21:50:50.570Z\"}],\"total_buyers\":4}}}";

        //    var item = Utils.DeserializeJson<PrintItem>(json);

        //    var obj = item.ToObjetoImpressao();

        //    PrinterService.GetInstance().Print(obj.relatorio, "Microsoft Print to PDF");
        //}
        #endregion
        private void EnableOrDisablePrinterDefault(bool update)
        {
            webSocketServidor = update ? ConfiguracaoWebSocket.CarregarConfiguracoes() : webSocketServidor;

            if (webSocketServidor.IsHabilitado())
            {
                timerPrinterService.Stop();
                timerPrinterService.Enabled = false;
                lblIntervalo.Visible = false;
                nudIntervalo.Visible = false;

                InitWebSocket(webSocketServidor);
                RunThreadValidaConexaoWebSocket();
            }
            else
            {
                webSocketServidor = null;
                lblIntervalo.Visible = true;
                nudIntervalo.Visible = true;
                timerPrinterService.Enabled = true;
                timerPrinterService.Start();
            }
        }


        private static void InitWebSocket(WebSocketServidor webSocket)
        {
            ServidorGateway.WebSocketServidor = webSocket;
            Eventos.SetEventos(ServidorGateway.WebSocketServidor);

            var loggedSystem = Sistema.GetLoggedSystem();
            if (loggedSystem.defaultUser == null || loggedSystem.defaultUser.token == null)
            {
                var _user = Sistema.GetLoggedSystem().service.Login(loggedSystem.defaultUser.usuario, loggedSystem.defaultUser.senha);
                loggedSystem.service.SetToken(_user.token);
                loggedSystem.defaultUser = _user;
            }

            ServidorGateway.InitWebSocketServer(TipoConexao.EndPoint, Sistema.GetLoggedSystem()?.defaultUser);
        }

        private void btConfiguracoes_Click(object sender, EventArgs e)
        {
            using (var form = new ConfigWebSocket())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    EnableOrDisablePrinterDefault(true);
                }
            }
        }

        public void RunThreadValidaConexaoWebSocket()
        {
            new Thread(x =>
            {
                while (webSocketServidor?.IsHabilitado() ?? false)
                {
                    if (!ServidorGateway.VerificaAtividadeConexaoWebSocket())
                    {
                        ReiniciarWebSocket(true);
                    }
                    Thread.Sleep(5000);
                }
            }).Start();
        }

        private void ReiniciarWebSocket(bool update)
        {
            webSocketServidor = update ? ConfiguracaoWebSocket.CarregarConfiguracoes() : webSocketServidor;
            InitWebSocket(webSocketServidor);
        }
    }
}
