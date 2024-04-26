namespace CuentaPaciente
{
    partial class frmAyudaPacientesFacturacion
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.grid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnTermina = new Infragistics.Win.Misc.UltraButton();
            this.btnAgrupar = new Infragistics.Win.Misc.UltraButton();
            this.cmbTipoFactura = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ultraTextEditor1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnActualizar = new Infragistics.Win.Misc.UltraButton();
            this.txt_busqCi = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txt_busqNom = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txt_busqHist = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.cb_numFilas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqCi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqNom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqHist)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraPanel1
            // 
            appearance1.BackColor = System.Drawing.Color.AliceBlue;
            appearance1.BackColor2 = System.Drawing.Color.GhostWhite;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop37;
            this.ultraPanel1.Appearance = appearance1;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.grid);
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.ultraPanel1.Location = new System.Drawing.Point(2, 0);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(965, 348);
            this.ultraPanel1.TabIndex = 1;
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColor = System.Drawing.SystemColors.Window;
            appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grid.DisplayLayout.Appearance = appearance3;
            this.grid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.SystemColors.Window;
            this.grid.DisplayLayout.GroupByBox.Appearance = appearance4;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
            this.grid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance6.BackColor2 = System.Drawing.SystemColors.Control;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
            this.grid.DisplayLayout.MaxColScrollRegions = 1;
            this.grid.DisplayLayout.MaxRowScrollRegions = 1;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grid.DisplayLayout.Override.ActiveCellAppearance = appearance7;
            appearance8.BackColor = System.Drawing.SystemColors.Highlight;
            appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grid.DisplayLayout.Override.ActiveRowAppearance = appearance8;
            this.grid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            this.grid.DisplayLayout.Override.CardAreaAppearance = appearance9;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grid.DisplayLayout.Override.CellAppearance = appearance10;
            this.grid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grid.DisplayLayout.Override.CellPadding = 0;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance11.BorderColor = System.Drawing.SystemColors.Window;
            this.grid.DisplayLayout.Override.GroupByRowAppearance = appearance11;
            appearance12.TextHAlignAsString = "Left";
            this.grid.DisplayLayout.Override.HeaderAppearance = appearance12;
            this.grid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.Color.Silver;
            this.grid.DisplayLayout.Override.RowAppearance = appearance13;
            this.grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grid.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
            this.grid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.grid.Location = new System.Drawing.Point(12, 91);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(941, 244);
            this.grid.TabIndex = 1;
            this.grid.Text = "ultraGrid1";
            this.grid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grid_InitializeLayout);
            this.grid.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.grid_InitializeRow);
            this.grid.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.ultraGrid1_DoubleClickRow);
            this.grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ultraGrid1_KeyDown);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.btnTermina);
            this.ultraGroupBox1.Controls.Add(this.btnAgrupar);
            this.ultraGroupBox1.Controls.Add(this.cmbTipoFactura);
            this.ultraGroupBox1.Controls.Add(this.label5);
            this.ultraGroupBox1.Controls.Add(this.ultraTextEditor1);
            this.ultraGroupBox1.Controls.Add(this.btnActualizar);
            this.ultraGroupBox1.Controls.Add(this.txt_busqCi);
            this.ultraGroupBox1.Controls.Add(this.txt_busqNom);
            this.ultraGroupBox1.Controls.Add(this.txt_busqHist);
            this.ultraGroupBox1.Controls.Add(this.cb_numFilas);
            this.ultraGroupBox1.Controls.Add(this.label4);
            this.ultraGroupBox1.Controls.Add(this.label3);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Location = new System.Drawing.Point(12, 13);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(941, 72);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "Filtros";
            // 
            // btnTermina
            // 
            appearance16.BackColor = System.Drawing.Color.LightGray;
            this.btnTermina.Appearance = appearance16;
            this.btnTermina.Location = new System.Drawing.Point(828, -5);
            this.btnTermina.Name = "btnTermina";
            this.btnTermina.Size = new System.Drawing.Size(107, 27);
            this.btnTermina.TabIndex = 15;
            this.btnTermina.Text = "FIN PROCESO";
            this.btnTermina.Visible = false;
            this.btnTermina.Click += new System.EventHandler(this.btnTermina_Click);
            // 
            // btnAgrupar
            // 
            appearance2.BackColor = System.Drawing.Color.LightGray;
            this.btnAgrupar.Appearance = appearance2;
            this.btnAgrupar.Location = new System.Drawing.Point(715, -5);
            this.btnAgrupar.Name = "btnAgrupar";
            this.btnAgrupar.Size = new System.Drawing.Size(107, 27);
            this.btnAgrupar.TabIndex = 14;
            this.btnAgrupar.Text = "AGRUPAR";
            this.btnAgrupar.Visible = false;
            this.btnAgrupar.Click += new System.EventHandler(this.btnAgrupar_Click);
            // 
            // cmbTipoFactura
            // 
            this.cmbTipoFactura.FormattingEnabled = true;
            this.cmbTipoFactura.Items.AddRange(new object[] {
            "POR FACTURAR",
            "FACTURADA",
            "PRE-FACTURADA"});
            this.cmbTipoFactura.Location = new System.Drawing.Point(701, 39);
            this.cmbTipoFactura.Name = "cmbTipoFactura";
            this.cmbTipoFactura.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoFactura.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(557, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "N° de Factura:";
            // 
            // ultraTextEditor1
            // 
            this.ultraTextEditor1.Location = new System.Drawing.Point(557, 39);
            this.ultraTextEditor1.MaxLength = 12;
            this.ultraTextEditor1.Name = "ultraTextEditor1";
            this.ultraTextEditor1.Size = new System.Drawing.Size(138, 21);
            this.ultraTextEditor1.TabIndex = 11;
            this.ultraTextEditor1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ultraTextEditor1_KeyDown);
            this.ultraTextEditor1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ultraTextEditor1_KeyPress);
            this.ultraTextEditor1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ultraTextEditor1_KeyUp);
            // 
            // btnActualizar
            // 
            appearance15.BackColor = System.Drawing.Color.LightGray;
            this.btnActualizar.Appearance = appearance15;
            this.btnActualizar.Location = new System.Drawing.Point(828, 36);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(107, 27);
            this.btnActualizar.TabIndex = 2;
            this.btnActualizar.Text = "BUSCAR";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // txt_busqCi
            // 
            this.txt_busqCi.Location = new System.Drawing.Point(451, 39);
            this.txt_busqCi.Name = "txt_busqCi";
            this.txt_busqCi.Size = new System.Drawing.Size(100, 21);
            this.txt_busqCi.TabIndex = 9;
            this.txt_busqCi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_busqCi_KeyPress);
            // 
            // txt_busqNom
            // 
            this.txt_busqNom.Location = new System.Drawing.Point(192, 39);
            this.txt_busqNom.Name = "txt_busqNom";
            this.txt_busqNom.Size = new System.Drawing.Size(256, 21);
            this.txt_busqNom.TabIndex = 8;
            this.txt_busqNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_busqNom_KeyPress);
            // 
            // txt_busqHist
            // 
            this.txt_busqHist.Location = new System.Drawing.Point(89, 39);
            this.txt_busqHist.Name = "txt_busqHist";
            this.txt_busqHist.Size = new System.Drawing.Size(100, 21);
            this.txt_busqHist.TabIndex = 7;
            this.txt_busqHist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_busqHist_KeyPress);
            // 
            // cb_numFilas
            // 
            this.cb_numFilas.FormattingEnabled = true;
            this.cb_numFilas.Items.AddRange(new object[] {
            "10",
            "100",
            "1000"});
            this.cb_numFilas.Location = new System.Drawing.Point(5, 39);
            this.cb_numFilas.Name = "cb_numFilas";
            this.cb_numFilas.Size = new System.Drawing.Size(83, 21);
            this.cb_numFilas.TabIndex = 4;
            this.cb_numFilas.SelectedValueChanged += new System.EventHandler(this.cb_numFilas_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(448, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nro. Identificación :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombres :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Historia Clínica :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro. Filas :";
            // 
            // frmAyudaPacientesFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 349);
            this.Controls.Add(this.ultraPanel1);
            this.Name = "frmAyudaPacientesFacturacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pacientes Facturacion";
            this.Load += new System.EventHandler(this.frmAyudaPacientesFacturacion_Load);
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqCi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqNom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqHist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraButton btnActualizar;
        private Infragistics.Win.UltraWinGrid.UltraGrid grid;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txt_busqCi;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txt_busqNom;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txt_busqHist;
        private System.Windows.Forms.ComboBox cb_numFilas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor ultraTextEditor1;
        private System.Windows.Forms.ComboBox cmbTipoFactura;
        private Infragistics.Win.Misc.UltraButton btnAgrupar;
        private Infragistics.Win.Misc.UltraButton btnTermina;
    }
}