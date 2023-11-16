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

namespace OrderSheetPrinter.View
{
    public partial class ConfigWebSocket : MaterialForm
    {
        private bool RegDirtyPreferencias = false;
        private bool RegDirtyWebSocket = false;
        public ConfigWebSocket()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btResetarConfiguracoes_Click(object sender, EventArgs e)
        {
            AtualizarCampos(ConfiguracaoWebSocket.GetConfiguracoesPadrao());
            MessageBox.Show("Configurações resetadas com sucesso!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AtualizarCampos(WebSocketServidor config)
        {
            txtUrl.Text = config.GetUrl();
            cmbValidarToken.Text = config.GetUsarToken() ? "Sim" : "Não";

            if (config.IsHabilitado())
                rdHabilitado.Checked = true;
            else
                rdDesabilitado.Checked = true;
        }


        private void SalvarConfiguracoesWebSocket()
        {
            string url = txtUrl.Text;
            bool habilitado = rdHabilitado.Checked;
            bool useToken = cmbValidarToken.Text.Equals("Sim") ? true : false;

            WebSocketServidor config = new WebSocketServidor(url, habilitado, useToken);
            ConfiguracaoWebSocket.SalvarConfiguracoes(config);
        }

        private void ConfigWebSocket_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (RegDirtyWebSocket)
                SalvarConfiguracoesWebSocket();

            if (RegDirtyPreferencias)            
                SalvarPreferencias();
            
            this.DialogResult = DialogResult.OK;
        }

        private void ConfigWebSocket_Load(object sender, EventArgs e)
        {

            AtualizarCampos(ConfiguracaoWebSocket.CarregarConfiguracoes());
            CarregarCampos();
            tabControl1.TabPages.Remove(tabWebSocket);
        }


        void CarregarCamposPreferencias()
        {
            if (ConfigManager.Preferencias != null && ConfigManager.Preferencias.Variaveis[NomesPreferencias.DIMENSAO_IMPRESSORA] != null)
                cmbDimensao.SelectedItem = (AuxEnum.DimensoImpressora)ConfigManager.Preferencias?.GetPreferenciaValue(NomesPreferencias.DIMENSAO_IMPRESSORA, "0").StringToShort(0);
        }
        private void rdHabilitado_CheckedChanged(object sender, EventArgs e)
        {
            RegDirtyWebSocket = true;
            if (rdHabilitado.Checked == false)
                pnlCampos.Enabled = false;
            else
                pnlCampos.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            CarregarCamposPreferencias();
        }

        public void CarregarCampos()
        {
            AuxEnum.PopulaCombo(cmbDimensao, typeof(AuxEnum.DimensoImpressora));
        }

        public void SalvarPreferencias()
        {

            Preferencias pref = ConfigManager.Preferencias ?? new Preferencias();
            if (pref?.Variaveis != null)
            {
                if (pref.Variaveis.Keys.Contains(NomesPreferencias.DIMENSAO_IMPRESSORA))
                    pref.Update(NomesPreferencias.DIMENSAO_IMPRESSORA, ((short)(AuxEnum.DimensoImpressora)cmbDimensao.SelectedItem).ToString());
                else
                    pref.Insert(NomesPreferencias.DIMENSAO_IMPRESSORA, 'T', ((short)(AuxEnum.DimensoImpressora)cmbDimensao.SelectedItem).ToString());
            }
        }


        private void cmbDimensao_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegDirtyPreferencias = true;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            RegDirtyWebSocket = true;
        }

        private void cmbValidarToken_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegDirtyWebSocket = true;
        }
    }
}
