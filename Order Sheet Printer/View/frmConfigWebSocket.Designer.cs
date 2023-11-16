namespace OrderSheetPrinter.View
{
    partial class ConfigWebSocket
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigWebSocket));
            this.btnSair = new MaterialSkin.Controls.MaterialButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdDesabilitado = new System.Windows.Forms.RadioButton();
            this.rdHabilitado = new System.Windows.Forms.RadioButton();
            this.pnlCampos = new System.Windows.Forms.Panel();
            this.cmbValidarToken = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btResetarConfiguracoes = new MaterialSkin.Controls.MaterialButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabWebSocket = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbDimensao = new System.Windows.Forms.ComboBox();
            this.lblDimensaoPrint = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.materialButton1 = new MaterialSkin.Controls.MaterialButton();
            this.groupBox1.SuspendLayout();
            this.pnlCampos.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabWebSocket.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSair.AutoSize = false;
            this.btnSair.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSair.Depth = 0;
            this.btnSair.DrawShadows = true;
            this.btnSair.HighEmphasis = true;
            this.btnSair.Icon = null;
            this.btnSair.Location = new System.Drawing.Point(746, 236);
            this.btnSair.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnSair.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(53, 36);
            this.btnSair.TabIndex = 4;
            this.btnSair.Text = "Sair";
            this.btnSair.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSair.UseAccentColor = false;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdDesabilitado);
            this.groupBox1.Controls.Add(this.rdHabilitado);
            this.groupBox1.Location = new System.Drawing.Point(10, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conexão WebSocket";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(217, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(484, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "*Ao habilitar a conexão, as chamadas automáticas por tempo serão desativadas.";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // rdDesabilitado
            // 
            this.rdDesabilitado.AutoSize = true;
            this.rdDesabilitado.Location = new System.Drawing.Point(104, 21);
            this.rdDesabilitado.Name = "rdDesabilitado";
            this.rdDesabilitado.Size = new System.Drawing.Size(105, 20);
            this.rdDesabilitado.TabIndex = 1;
            this.rdDesabilitado.Text = "Desabilitado";
            this.rdDesabilitado.UseVisualStyleBackColor = true;
            // 
            // rdHabilitado
            // 
            this.rdHabilitado.AutoSize = true;
            this.rdHabilitado.Checked = true;
            this.rdHabilitado.Location = new System.Drawing.Point(6, 21);
            this.rdHabilitado.Name = "rdHabilitado";
            this.rdHabilitado.Size = new System.Drawing.Size(90, 20);
            this.rdHabilitado.TabIndex = 0;
            this.rdHabilitado.TabStop = true;
            this.rdHabilitado.Text = "Habilitado";
            this.rdHabilitado.UseVisualStyleBackColor = true;
            this.rdHabilitado.CheckedChanged += new System.EventHandler(this.rdHabilitado_CheckedChanged);
            // 
            // pnlCampos
            // 
            this.pnlCampos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCampos.BackColor = System.Drawing.Color.Transparent;
            this.pnlCampos.Controls.Add(this.cmbValidarToken);
            this.pnlCampos.Controls.Add(this.label3);
            this.pnlCampos.Controls.Add(this.txtUrl);
            this.pnlCampos.Controls.Add(this.label2);
            this.pnlCampos.Location = new System.Drawing.Point(10, 73);
            this.pnlCampos.Name = "pnlCampos";
            this.pnlCampos.Size = new System.Drawing.Size(787, 145);
            this.pnlCampos.TabIndex = 15;
            // 
            // cmbValidarToken
            // 
            this.cmbValidarToken.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValidarToken.FormattingEnabled = true;
            this.cmbValidarToken.Items.AddRange(new object[] {
            "Sim",
            "Não"});
            this.cmbValidarToken.Location = new System.Drawing.Point(110, 73);
            this.cmbValidarToken.Name = "cmbValidarToken";
            this.cmbValidarToken.Size = new System.Drawing.Size(121, 24);
            this.cmbValidarToken.TabIndex = 2;
            this.cmbValidarToken.SelectedIndexChanged += new System.EventHandler(this.cmbValidarToken_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Validar Token:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(110, 45);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(667, 22);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.TextChanged += new System.EventHandler(this.txtUrl_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "URL:";
            // 
            // btResetarConfiguracoes
            // 
            this.btResetarConfiguracoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btResetarConfiguracoes.AutoSize = false;
            this.btResetarConfiguracoes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btResetarConfiguracoes.Depth = 0;
            this.btResetarConfiguracoes.DrawShadows = true;
            this.btResetarConfiguracoes.HighEmphasis = true;
            this.btResetarConfiguracoes.Icon = null;
            this.btResetarConfiguracoes.Location = new System.Drawing.Point(530, 236);
            this.btResetarConfiguracoes.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btResetarConfiguracoes.MouseState = MaterialSkin.MouseState.HOVER;
            this.btResetarConfiguracoes.Name = "btResetarConfiguracoes";
            this.btResetarConfiguracoes.Size = new System.Drawing.Size(206, 36);
            this.btResetarConfiguracoes.TabIndex = 3;
            this.btResetarConfiguracoes.Text = "Resetar";
            this.btResetarConfiguracoes.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btResetarConfiguracoes.UseAccentColor = false;
            this.btResetarConfiguracoes.UseVisualStyleBackColor = true;
            this.btResetarConfiguracoes.Click += new System.EventHandler(this.btResetarConfiguracoes_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabWebSocket);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(811, 308);
            this.tabControl1.TabIndex = 16;
            // 
            // tabWebSocket
            // 
            this.tabWebSocket.Controls.Add(this.label4);
            this.tabWebSocket.Controls.Add(this.groupBox1);
            this.tabWebSocket.Controls.Add(this.btResetarConfiguracoes);
            this.tabWebSocket.Controls.Add(this.btnSair);
            this.tabWebSocket.Controls.Add(this.pnlCampos);
            this.tabWebSocket.Location = new System.Drawing.Point(4, 25);
            this.tabWebSocket.Name = "tabWebSocket";
            this.tabWebSocket.Padding = new System.Windows.Forms.Padding(3);
            this.tabWebSocket.Size = new System.Drawing.Size(803, 279);
            this.tabWebSocket.TabIndex = 0;
            this.tabWebSocket.Text = "WebSocket";
            this.tabWebSocket.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(803, 279);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preferências";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(7, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(325, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "*O salvamento desta página é feito automaticamente.";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.materialButton1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 267);
            this.panel1.TabIndex = 0;
            // 
            // cmbDimensao
            // 
            this.cmbDimensao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDimensao.FormattingEnabled = true;
            this.cmbDimensao.Location = new System.Drawing.Point(193, 30);
            this.cmbDimensao.Name = "cmbDimensao";
            this.cmbDimensao.Size = new System.Drawing.Size(121, 24);
            this.cmbDimensao.TabIndex = 0;
            this.cmbDimensao.SelectedIndexChanged += new System.EventHandler(this.cmbDimensao_SelectedIndexChanged);
            // 
            // lblDimensaoPrint
            // 
            this.lblDimensaoPrint.AutoSize = true;
            this.lblDimensaoPrint.Location = new System.Drawing.Point(118, 33);
            this.lblDimensaoPrint.Name = "lblDimensaoPrint";
            this.lblDimensaoPrint.Size = new System.Drawing.Size(69, 16);
            this.lblDimensaoPrint.TabIndex = 1;
            this.lblDimensaoPrint.Text = "Dimensão";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(3, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(325, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "*O salvamento desta página é feito automaticamente.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblDimensaoPrint);
            this.groupBox2.Controls.Add(this.cmbDimensao);
            this.groupBox2.Location = new System.Drawing.Point(162, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(455, 74);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Impressão";
            // 
            // materialButton1
            // 
            this.materialButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.materialButton1.AutoSize = false;
            this.materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton1.Depth = 0;
            this.materialButton1.DrawShadows = true;
            this.materialButton1.HighEmphasis = true;
            this.materialButton1.Icon = null;
            this.materialButton1.Location = new System.Drawing.Point(733, 224);
            this.materialButton1.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton1.Name = "materialButton1";
            this.materialButton1.Size = new System.Drawing.Size(53, 36);
            this.materialButton1.TabIndex = 19;
            this.materialButton1.Text = "Sair";
            this.materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton1.UseAccentColor = false;
            this.materialButton1.UseVisualStyleBackColor = true;
            this.materialButton1.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // ConfigWebSocket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(819, 316);
            this.Controls.Add(this.tabControl1);
            this.DrawerShowIconsWhenHidden = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "ConfigWebSocket";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Sizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigWebSocket_FormClosing);
            this.Load += new System.EventHandler(this.ConfigWebSocket_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlCampos.ResumeLayout(false);
            this.pnlCampos.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabWebSocket.ResumeLayout(false);
            this.tabWebSocket.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialButton btnSair;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdDesabilitado;
        private System.Windows.Forms.RadioButton rdHabilitado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlCampos;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbValidarToken;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialButton btResetarConfiguracoes;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabWebSocket;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDimensaoPrint;
        private System.Windows.Forms.ComboBox cmbDimensao;
        private System.Windows.Forms.GroupBox groupBox2;
        private MaterialSkin.Controls.MaterialButton materialButton1;
    }
}

