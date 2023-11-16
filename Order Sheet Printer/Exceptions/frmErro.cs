using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysGestao
{
    public partial class frmErro : Form
    {
        public Exception Exception { get; set; }
        public string Complemento { get; set; }
        public frmErro()
        {
            InitializeComponent();
        }
        public frmErro(Exception ex, string complemento)
        {
            InitializeComponent();
            Exception = ex;
            lblErro.Text = ex.Message;
            Complemento = complemento;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string erroText = "";

                erroText = lblErro.Text;
                Clipboard.SetText(erroText);
                MessageBox.Show("Texto copiado para sua área de transferência", "Copiado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void Erro_Inesperado(Exception ex, string complemento)
        {
            frmErro erro = new frmErro(ex, complemento);
            erro.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirPastaLog();
        }
        private void AbrirPastaLog()
        {
            Process.Start("Explorer", Complemento);
        }
    }
}
