namespace His.Honorarios
{
    partial class Reporte_FactVendedores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reporte_FactVendedores));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.frm_RepMedicos_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbReferido = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodVendedor = new System.Windows.Forms.TextBox();
            this.btnAddVendedor = new System.Windows.Forms.Button();
            this.txtVendedor = new System.Windows.Forms.TextBox();
            this.menu.SuspendLayout();
            this.frm_RepMedicos_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_RepMedicos_Fill_Panel.SuspendLayout();
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
            this.menu.Size = new System.Drawing.Size(502, 36);
            this.menu.TabIndex = 64;
            this.menu.Text = "menu";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(73, 33);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 33);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(49, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            // 
            // frm_RepMedicos_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_RepMedicos_Fill_Panel.Appearance = appearance1;
            // 
            // frm_RepMedicos_Fill_Panel.ClientArea
            // 
            this.frm_RepMedicos_Fill_Panel.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.frm_RepMedicos_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_RepMedicos_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_RepMedicos_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frm_RepMedicos_Fill_Panel.Name = "frm_RepMedicos_Fill_Panel";
            this.frm_RepMedicos_Fill_Panel.Size = new System.Drawing.Size(502, 228);
            this.frm_RepMedicos_Fill_Panel.TabIndex = 65;
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ultraGroupBox1.Controls.Add(this.txtCodVendedor);
            this.ultraGroupBox1.Controls.Add(this.btnAddVendedor);
            this.ultraGroupBox1.Controls.Add(this.txtVendedor);
            this.ultraGroupBox1.Controls.Add(this.label5);
            this.ultraGroupBox1.Controls.Add(this.label4);
            this.ultraGroupBox1.Controls.Add(this.label3);
            this.ultraGroupBox1.Controls.Add(this.dtpHasta);
            this.ultraGroupBox1.Controls.Add(this.dtpDesde);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.cmbReferido);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Location = new System.Drawing.Point(10, 24);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(483, 166);
            this.ultraGroupBox1.TabIndex = 65;
            this.ultraGroupBox1.Text = "Filtros";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(30, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Referido: ";
            // 
            // cmbReferido
            // 
            this.cmbReferido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReferido.FormattingEnabled = true;
            this.cmbReferido.Items.AddRange(new object[] {
            "Todos",
            "Institucional",
            "Privado"});
            this.cmbReferido.Location = new System.Drawing.Point(89, 28);
            this.cmbReferido.Name = "cmbReferido";
            this.cmbReferido.Size = new System.Drawing.Size(143, 21);
            this.cmbReferido.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(30, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha de facturacion:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Checked = false;
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(185, 72);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.ShowCheckBox = true;
            this.dtpDesde.Size = new System.Drawing.Size(126, 20);
            this.dtpDesde.TabIndex = 81;
            this.dtpDesde.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Checked = false;
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(185, 94);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.ShowCheckBox = true;
            this.dtpHasta.Size = new System.Drawing.Size(126, 20);
            this.dtpHasta.TabIndex = 82;
            this.dtpHasta.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(147, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 83;
            this.label3.Text = "Desde";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(147, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 84;
            this.label4.Text = "Hasta";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(30, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 85;
            this.label5.Text = "Vendedor:";
            // 
            // txtCodVendedor
            // 
            this.txtCodVendedor.Location = new System.Drawing.Point(391, 125);
            this.txtCodVendedor.Name = "txtCodVendedor";
            this.txtCodVendedor.ReadOnly = true;
            this.txtCodVendedor.Size = new System.Drawing.Size(37, 20);
            this.txtCodVendedor.TabIndex = 120;
            this.txtCodVendedor.Visible = false;
            // 
            // btnAddVendedor
            // 
            this.btnAddVendedor.Location = new System.Drawing.Point(86, 124);
            this.btnAddVendedor.Name = "btnAddVendedor";
            this.btnAddVendedor.Size = new System.Drawing.Size(30, 23);
            this.btnAddVendedor.TabIndex = 119;
            this.btnAddVendedor.Text = "F1";
            this.btnAddVendedor.UseVisualStyleBackColor = true;
            this.btnAddVendedor.Click += new System.EventHandler(this.btnAddVendedor_Click);
            // 
            // txtVendedor
            // 
            this.txtVendedor.Location = new System.Drawing.Point(116, 125);
            this.txtVendedor.Name = "txtVendedor";
            this.txtVendedor.ReadOnly = true;
            this.txtVendedor.Size = new System.Drawing.Size(312, 20);
            this.txtVendedor.TabIndex = 118;
            this.txtVendedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVendedor_KeyDown);
            // 
            // Reporte_FactVendedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 264);
            this.Controls.Add(this.frm_RepMedicos_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "Reporte_FactVendedores";
            this.Text = "Reporte_FactVendedores";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.frm_RepMedicos_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_RepMedicos_Fill_Panel.ResumeLayout(false);
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
        private Infragistics.Win.Misc.UltraPanel frm_RepMedicos_Fill_Panel;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbReferido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.TextBox txtCodVendedor;
        private System.Windows.Forms.Button btnAddVendedor;
        private System.Windows.Forms.TextBox txtVendedor;
    }
}