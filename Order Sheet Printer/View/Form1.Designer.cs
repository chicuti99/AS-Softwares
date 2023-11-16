namespace OrderSheetPrinter
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerPrinterService = new System.Windows.Forms.Timer(this.components);
            this.btnAddPrinter = new MaterialSkin.Controls.MaterialButton();
            this.btnSair = new MaterialSkin.Controls.MaterialButton();
            this.materialListView1 = new MaterialSkin.Controls.MaterialListView();
            this.thCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.thNome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.thHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.materialContextMenuStrip1 = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.excluirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.btnUpdate = new MaterialSkin.Controls.MaterialButton();
            this.lblIntervalo = new MaterialSkin.Controls.MaterialLabel();
            this.nudIntervalo = new System.Windows.Forms.NumericUpDown();
            this.timerUpdateCheck = new System.Windows.Forms.Timer(this.components);
            this.btnCheckUpdate = new MaterialSkin.Controls.MaterialButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btConfiguracoes = new MaterialSkin.Controls.MaterialButton();
            this.materialContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipTitle = "Order Sheet Printer";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Order Sheet Printer";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timerPrinterService
            // 
            this.timerPrinterService.Enabled = true;
            this.timerPrinterService.Interval = 60000;
            this.timerPrinterService.Tick += new System.EventHandler(this.timerPrinterService_Tick);
            // 
            // btnAddPrinter
            // 
            this.btnAddPrinter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddPrinter.AutoSize = false;
            this.btnAddPrinter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddPrinter.Depth = 0;
            this.btnAddPrinter.DrawShadows = true;
            this.btnAddPrinter.HighEmphasis = true;
            this.btnAddPrinter.Icon = null;
            this.btnAddPrinter.Location = new System.Drawing.Point(382, 318);
            this.btnAddPrinter.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnAddPrinter.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAddPrinter.Name = "btnAddPrinter";
            this.btnAddPrinter.Size = new System.Drawing.Size(221, 36);
            this.btnAddPrinter.TabIndex = 3;
            this.btnAddPrinter.Text = "Adicionar impressora";
            this.btnAddPrinter.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnAddPrinter.UseAccentColor = false;
            this.btnAddPrinter.UseCompatibleTextRendering = true;
            this.btnAddPrinter.UseVisualStyleBackColor = true;
            this.btnAddPrinter.Click += new System.EventHandler(this.btnAddPrinter_Click);
            // 
            // btnSair
            // 
            this.btnSair.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSair.AutoSize = false;
            this.btnSair.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSair.Depth = 0;
            this.btnSair.DrawShadows = true;
            this.btnSair.HighEmphasis = true;
            this.btnSair.Icon = null;
            this.btnSair.Location = new System.Drawing.Point(786, 318);
            this.btnSair.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnSair.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(53, 36);
            this.btnSair.TabIndex = 5;
            this.btnSair.Text = "Sair";
            this.btnSair.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSair.UseAccentColor = false;
            this.btnSair.UseCompatibleTextRendering = true;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // materialListView1
            // 
            this.materialListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialListView1.AutoSizeTable = false;
            this.materialListView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.thCodigo,
            this.thNome,
            this.thHost});
            this.materialListView1.Depth = 0;
            this.materialListView1.FullRowSelect = true;
            this.materialListView1.HideSelection = false;
            this.materialListView1.Location = new System.Drawing.Point(15, 119);
            this.materialListView1.Margin = new System.Windows.Forms.Padding(4);
            this.materialListView1.MinimumSize = new System.Drawing.Size(267, 123);
            this.materialListView1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialListView1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialListView1.MultiSelect = false;
            this.materialListView1.Name = "materialListView1";
            this.materialListView1.OwnerDraw = true;
            this.materialListView1.Size = new System.Drawing.Size(824, 185);
            this.materialListView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.materialListView1.TabIndex = 0;
            this.materialListView1.UseCompatibleStateImageBehavior = false;
            this.materialListView1.View = System.Windows.Forms.View.Details;
            this.materialListView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.materialListView1_MouseClick);
            this.materialListView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.materialListView1_MouseDoubleClick);
            // 
            // thCodigo
            // 
            this.thCodigo.Text = "Id";
            // 
            // thNome
            // 
            this.thNome.Text = "Nome";
            this.thNome.Width = 200;
            // 
            // thHost
            // 
            this.thHost.Text = "Host";
            this.thHost.Width = 330;
            // 
            // materialContextMenuStrip1
            // 
            this.materialContextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.materialContextMenuStrip1.Depth = 0;
            this.materialContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.materialContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excluirToolStripMenuItem});
            this.materialContextMenuStrip1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialContextMenuStrip1.Name = "materialContextMenuStrip1";
            this.materialContextMenuStrip1.Size = new System.Drawing.Size(122, 28);
            // 
            // excluirToolStripMenuItem
            // 
            this.excluirToolStripMenuItem.Name = "excluirToolStripMenuItem";
            this.excluirToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            this.excluirToolStripMenuItem.Text = "Excluir";
            this.excluirToolStripMenuItem.Click += new System.EventHandler(this.excluirToolStripMenuItem_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(16, 92);
            this.materialLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(180, 19);
            this.materialLabel1.TabIndex = 11;
            this.materialLabel1.Text = "Impressoras cadastradas";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnUpdate.AutoSize = false;
            this.btnUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUpdate.Depth = 0;
            this.btnUpdate.DrawShadows = true;
            this.btnUpdate.HighEmphasis = true;
            this.btnUpdate.Icon = null;
            this.btnUpdate.Location = new System.Drawing.Point(185, 318);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnUpdate.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(174, 36);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "Baixar atualização";
            this.btnUpdate.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnUpdate.UseAccentColor = false;
            this.btnUpdate.UseCompatibleTextRendering = true;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblIntervalo
            // 
            this.lblIntervalo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblIntervalo.Depth = 0;
            this.lblIntervalo.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblIntervalo.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            this.lblIntervalo.Location = new System.Drawing.Point(16, 308);
            this.lblIntervalo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIntervalo.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblIntervalo.Name = "lblIntervalo";
            this.lblIntervalo.Size = new System.Drawing.Size(130, 14);
            this.lblIntervalo.TabIndex = 14;
            this.lblIntervalo.Text = "Intervalo de busca (seg)";
            // 
            // nudIntervalo
            // 
            this.nudIntervalo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudIntervalo.Location = new System.Drawing.Point(15, 332);
            this.nudIntervalo.Margin = new System.Windows.Forms.Padding(4);
            this.nudIntervalo.Name = "nudIntervalo";
            this.nudIntervalo.Size = new System.Drawing.Size(131, 22);
            this.nudIntervalo.TabIndex = 1;
            this.nudIntervalo.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudIntervalo.ValueChanged += new System.EventHandler(this.nudIntervalo_ValueChanged);
            // 
            // timerUpdateCheck
            // 
            this.timerUpdateCheck.Enabled = true;
            this.timerUpdateCheck.Interval = 86400000;
            this.timerUpdateCheck.Tick += new System.EventHandler(this.timerUpdateCheck_Tick);
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCheckUpdate.AutoSize = false;
            this.btnCheckUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCheckUpdate.Depth = 0;
            this.btnCheckUpdate.DrawShadows = true;
            this.btnCheckUpdate.HighEmphasis = true;
            this.btnCheckUpdate.Icon = null;
            this.btnCheckUpdate.Location = new System.Drawing.Point(155, 318);
            this.btnCheckUpdate.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btnCheckUpdate.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(217, 36);
            this.btnCheckUpdate.TabIndex = 2;
            this.btnCheckUpdate.Text = "Buscar atualização";
            this.btnCheckUpdate.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnCheckUpdate.UseAccentColor = false;
            this.btnCheckUpdate.UseCompatibleTextRendering = true;
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::OrderSheetPrinter.Properties.Resources.logo_takeat;
            this.pictureBox1.Location = new System.Drawing.Point(357, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(168, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // btConfiguracoes
            // 
            this.btConfiguracoes.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btConfiguracoes.AutoSize = false;
            this.btConfiguracoes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btConfiguracoes.Depth = 0;
            this.btConfiguracoes.DrawShadows = true;
            this.btConfiguracoes.HighEmphasis = true;
            this.btConfiguracoes.Icon = null;
            this.btConfiguracoes.Location = new System.Drawing.Point(613, 318);
            this.btConfiguracoes.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.btConfiguracoes.MouseState = MaterialSkin.MouseState.HOVER;
            this.btConfiguracoes.Name = "btConfiguracoes";
            this.btConfiguracoes.Size = new System.Drawing.Size(163, 36);
            this.btConfiguracoes.TabIndex = 4;
            this.btConfiguracoes.Text = "Configurações";
            this.btConfiguracoes.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btConfiguracoes.UseAccentColor = false;
            this.btConfiguracoes.UseCompatibleTextRendering = true;
            this.btConfiguracoes.UseVisualStyleBackColor = true;
            this.btConfiguracoes.Click += new System.EventHandler(this.btConfiguracoes_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(848, 380);
            this.Controls.Add(this.btConfiguracoes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCheckUpdate);
            this.Controls.Add(this.nudIntervalo);
            this.Controls.Add(this.lblIntervalo);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.materialListView1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAddPrinter);
            this.DrawerShowIconsWhenHidden = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.materialContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timerPrinterService;
        private MaterialSkin.Controls.MaterialButton btnAddPrinter;
        private MaterialSkin.Controls.MaterialButton btnSair;
        private MaterialSkin.Controls.MaterialListView materialListView1;
        private System.Windows.Forms.ColumnHeader thNome;
        private System.Windows.Forms.ColumnHeader thHost;
        private MaterialSkin.Controls.MaterialContextMenuStrip materialContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem excluirToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader thCodigo;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialButton btnUpdate;
        private MaterialSkin.Controls.MaterialLabel lblIntervalo;
        private System.Windows.Forms.NumericUpDown nudIntervalo;
        private System.Windows.Forms.Timer timerUpdateCheck;
        private MaterialSkin.Controls.MaterialButton btnCheckUpdate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MaterialSkin.Controls.MaterialButton btConfiguracoes;
    }
}

