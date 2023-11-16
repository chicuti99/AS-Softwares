using MaterialSkin;
using MaterialSkin.Controls;
using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Domain.Entities;
using OrderSheetPrinter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSheetPrinter.View
{
    public partial class FrmCadastroImpressora : MaterialForm
    {
        private Impressora impressora;
        public FrmCadastroImpressora(Impressora impressora)
        {
            this.impressora = impressora;

            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
        }

        public void OnRadioChecked(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            bool isChecked = rb.Checked;

            Color backColor = isChecked ? Color.Green : Color.White;
            Color fontColor = isChecked ? Color.White : Color.Green;

            rb.BackColor = backColor;
            rb.ForeColor = fontColor;
        }

        private void GetPrinters()
        {
            var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
            foreach (var printer in printerQuery.Get())
            {
                String name = Convert.ToString(printer.GetPropertyValue("Name"));
                var status = printer.GetPropertyValue("Status");
                var isDefault = printer.GetPropertyValue("Default");
                var isNetworkPrinter = printer.GetPropertyValue("Network");

                AddPrinter(name);
            }
        }

        private void AddPrinter(String name)
        {
            materialComboBox1.Items.Add(name);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmCadastroImpressora_Load(object sender, EventArgs e)
        {
            GetPrinters();

            if (impressora != null)
            {
                txtNome.Text = impressora.nome;
                materialComboBox1.SelectedItem = impressora.host;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            String nome = txtNome.Text;
            var item = materialComboBox1.SelectedItem;

            if (String.IsNullOrEmpty(nome))
            {
                Utils.ShowErrorDialog("Preenha o nome");
                return;
            }

            if (item == null)
            {
                Utils.ShowErrorDialog("Selecione uma impressora para vincular");
                return;
            }

            if (impressora == null)
                impressora = new Impressora();

            impressora.nome = txtNome.Text;
            impressora.host = Convert.ToString(item);

            var service = Sistema.GetLoggedSystem().service;

            try
            {
                if (impressora.id > 0)
                    service.UpdatePrinter(impressora);
                else
                    service.SavePrinter(impressora);

                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                Utils.ShowErrorDialog(ex.Message);
            }
        }
    }
}
