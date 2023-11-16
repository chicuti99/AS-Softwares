using MaterialSkin;
using MaterialSkin.Controls;
using OrderSheetPrinter.Domain.Core;
using OrderSheetPrinter.Model;
using OrderSheetPrinter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSheetPrinter.View
{
    public partial class FrmLoginV2 : MaterialForm
    {
        public FrmLoginV2()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(Color.FromArgb(250, 5, 97), Color.FromArgb(250, 5, 97), Color.FromArgb(250, 5, 97), Color.FromArgb(250, 5, 97), TextShade.WHITE);


            var sistemas = new List<Sistema>();
            foreach (SistemaEnum sistema in Enum.GetValues(typeof(SistemaEnum)))
                sistemas.Add(Sistema.Create(sistema));

            cbSistema.DataSource = sistemas;
            cbSistema.ValueMember = "type";
            cbSistema.DisplayMember = "nome";
        }

        private void Logar()
        {
            SetFieldsEnabled(false);

            Sistema sistema = (Sistema)cbSistema.SelectedItem;
            String email = txtEmail.Text;
            String senha = txtSenha.Text;

            try
            {
                var usuario = sistema.service.Login(email, senha);

                usuario.senha = senha;

                sistema.defaultUser = usuario;
                sistema.service.SetToken(usuario.token);

                // set the session active session
                Sistema.SetLoggedSystem(sistema);

                CredentialService.Save(sistema.type, usuario);
                LocalStorageService.GetInstance().Save<String>(sistema.type.ToString(), "LAST_LOGIN");

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception e)
            {
                Utils.ShowErrorDialog(e.Message);
                SetFieldsEnabled(true);
            }
        }

        private void SetFieldsEnabled(bool enable)
        {
            cbSistema.Enabled = enable;
            txtEmail.Enabled = enable;
            txtSenha.Enabled = enable;
            btnLogin.Enabled = enable;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Logar();
        }
    }
}
