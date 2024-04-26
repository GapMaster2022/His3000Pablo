namespace His.Honorarios
{
    partial class frm_BalanceApagar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_BalanceApagar));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_medicos = new System.Windows.Forms.ComboBox();
            this.txt_fecfin = new System.Windows.Forms.MaskedTextBox();
            this.txt_fecini = new System.Windows.Forms.MaskedTextBox();
            this.frm_BalanceApagar_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.menu.SuspendLayout();
            this.frm_BalanceApagar_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_BalanceApagar_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImprimir,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(511, 36);
            this.menu.TabIndex = 68;
            this.menu.Text = "menu";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(65, 33);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(69, 33);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(47, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(31, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Médico:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(242, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Fecha Final:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(31, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Fecha Inicio:";
            // 
            // cmb_medicos
            // 
            this.cmb_medicos.FormattingEnabled = true;
            this.cmb_medicos.Location = new System.Drawing.Point(114, 64);
            this.cmb_medicos.Name = "cmb_medicos";
            this.cmb_medicos.Size = new System.Drawing.Size(308, 21);
            this.cmb_medicos.TabIndex = 11;
            // 
            // txt_fecfin
            // 
            this.txt_fecfin.Location = new System.Drawing.Point(312, 27);
            this.txt_fecfin.Mask = "00/00/0000";
            this.txt_fecfin.Name = "txt_fecfin";
            this.txt_fecfin.Size = new System.Drawing.Size(110, 20);
            this.txt_fecfin.TabIndex = 7;
            this.txt_fecfin.ValidatingType = typeof(System.DateTime);
            this.txt_fecfin.Leave += new System.EventHandler(this.txt_fecfin_Leave);
            this.txt_fecfin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_fecfin_KeyPress);
            // 
            // txt_fecini
            // 
            this.txt_fecini.Location = new System.Drawing.Point(114, 27);
            this.txt_fecini.Mask = "00/00/0000";
            this.txt_fecini.Name = "txt_fecini";
            this.txt_fecini.Size = new System.Drawing.Size(110, 20);
            this.txt_fecini.TabIndex = 6;
            this.txt_fecini.ValidatingType = typeof(System.DateTime);
            this.txt_fecini.Leave += new System.EventHandler(this.txt_fecini_Leave);
            this.txt_fecini.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_fecini_KeyPress);
            // 
            // frm_BalanceApagar_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop20;
            this.frm_BalanceApagar_Fill_Panel.Appearance = appearance1;
            // 
            // frm_BalanceApagar_Fill_Panel.ClientArea
            // 
            this.frm_BalanceApagar_Fill_Panel.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.frm_BalanceApagar_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_BalanceApagar_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_BalanceApagar_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frm_BalanceApagar_Fill_Panel.Name = "frm_BalanceApagar_Fill_Panel";
            this.frm_BalanceApagar_Fill_Panel.Size = new System.Drawing.Size(511, 139);
            this.frm_BalanceApagar_Fill_Panel.TabIndex = 69;
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.label3);
            this.ultraGroupBox1.Controls.Add(this.cmb_medicos);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.txt_fecini);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Controls.Add(this.txt_fecfin);
            this.ultraGroupBox1.Location = new System.Drawing.Point(27, 15);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(460, 106);
            this.ultraGroupBox1.TabIndex = 68;
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // frm_BalanceApagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 175);
            this.Controls.Add(this.frm_BalanceApagar_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "frm_BalanceApagar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Balance de Honorarios pendientes de  Pago";
            this.Load += new System.EventHandler(this.frm_BalanceApagar_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.frm_BalanceApagar_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_BalanceApagar_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnImprimir;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_medicos;
        private System.Windows.Forms.MaskedTextBox txt_fecfin;
        private System.Windows.Forms.MaskedTextBox txt_fecini;
        private Infragistics.Win.Misc.UltraPanel frm_BalanceApagar_Fill_Panel;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
    }
}