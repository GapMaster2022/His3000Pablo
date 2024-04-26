namespace His.Garantia
{
    partial class frmReporteGarantia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReporteGarantia));
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnactualizar = new System.Windows.Forms.ToolStripButton();
            this.btnexportar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.Nhc = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtf1 = new Infragistics.Win.Misc.UltraButton();
            this.txthc = new System.Windows.Forms.TextBox();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.chbTipoIngreso = new System.Windows.Forms.CheckBox();
            this.chkTratamiento = new System.Windows.Forms.CheckBox();
            this.cboEstadoGarantia = new System.Windows.Forms.ComboBox();
            this.combotipo = new System.Windows.Forms.ComboBox();
            this.grpFechas = new Infragistics.Win.Misc.UltraGroupBox();
            this.Fecha = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFiltroDesde = new System.Windows.Forms.DateTimePicker();
            this.TablaGarantia = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.TablaReporte = new System.Windows.Forms.DataGridView();
            this.toolStrip2.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).BeginInit();
            this.grpFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaGarantia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnactualizar,
            this.btnexportar,
            this.btnSalir});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(980, 39);
            this.toolStrip2.TabIndex = 9;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnactualizar
            // 
            this.btnactualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnactualizar.Image")));
            this.btnactualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnactualizar.Name = "btnactualizar";
            this.btnactualizar.Size = new System.Drawing.Size(36, 36);
            this.btnactualizar.Click += new System.EventHandler(this.btnmodificar_Click);
            // 
            // btnexportar
            // 
            this.btnexportar.Image = ((System.Drawing.Image)(resources.GetObject("btnexportar.Image")));
            this.btnexportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnexportar.Name = "btnexportar";
            this.btnexportar.Size = new System.Drawing.Size(36, 36);
            this.btnexportar.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(36, 36);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // ultraPanel1
            // 
            this.ultraPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance26.BackColor = System.Drawing.Color.Silver;
            appearance26.BackColor2 = System.Drawing.Color.DimGray;
            appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop20;
            appearance26.BackHatchStyle = Infragistics.Win.BackHatchStyle.DiagonalCross;
            this.ultraPanel1.Appearance = appearance26;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox2);
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.ultraPanel1.ClientArea.Controls.Add(this.grpFechas);
            this.ultraPanel1.Location = new System.Drawing.Point(0, 42);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(980, 99);
            this.ultraPanel1.TabIndex = 12;
            // 
            // ultraGroupBox2
            // 
            appearance28.BackColor = System.Drawing.Color.Transparent;
            this.ultraGroupBox2.Appearance = appearance28;
            this.ultraGroupBox2.Controls.Add(this.Nhc);
            this.ultraGroupBox2.Controls.Add(this.label3);
            this.ultraGroupBox2.Controls.Add(this.txtf1);
            this.ultraGroupBox2.Controls.Add(this.txthc);
            this.ultraGroupBox2.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox2.Location = new System.Drawing.Point(293, 6);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(243, 93);
            this.ultraGroupBox2.TabIndex = 3;
            this.ultraGroupBox2.Text = "Paciente:";
            // 
            // Nhc
            // 
            this.Nhc.AutoSize = true;
            this.Nhc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nhc.Location = new System.Drawing.Point(26, 46);
            this.Nhc.Name = "Nhc";
            this.Nhc.Size = new System.Drawing.Size(101, 17);
            this.Nhc.TabIndex = 275;
            this.Nhc.Text = "Nro. Historia:";
            this.Nhc.UseVisualStyleBackColor = true;
            this.Nhc.CheckedChanged += new System.EventHandler(this.Nhc_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 25);
            this.label3.TabIndex = 274;
            this.label3.Text = "[Escriba el numero de historia clinica o presione F1 para buscar por nombre.]";
            // 
            // txtf1
            // 
            appearance50.ForeColor = System.Drawing.Color.Navy;
            appearance50.TextHAlignAsString = "Center";
            appearance50.TextVAlignAsString = "Middle";
            this.txtf1.Appearance = appearance50;
            this.txtf1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            this.txtf1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtf1.Location = new System.Drawing.Point(189, 42);
            this.txtf1.Name = "txtf1";
            this.txtf1.Size = new System.Drawing.Size(30, 21);
            this.txtf1.TabIndex = 272;
            this.txtf1.TabStop = false;
            this.txtf1.Text = "F1";
            this.txtf1.Click += new System.EventHandler(this.ultraButton1_Click);
            // 
            // txthc
            // 
            this.txthc.Location = new System.Drawing.Point(133, 43);
            this.txthc.Name = "txthc";
            this.txthc.Size = new System.Drawing.Size(55, 20);
            this.txthc.TabIndex = 8;
            this.txthc.TextChanged += new System.EventHandler(this.txthc_TextChanged);
            // 
            // ultraGroupBox1
            // 
            appearance27.BackColor = System.Drawing.Color.Transparent;
            this.ultraGroupBox1.Appearance = appearance27;
            this.ultraGroupBox1.Controls.Add(this.chbTipoIngreso);
            this.ultraGroupBox1.Controls.Add(this.chkTratamiento);
            this.ultraGroupBox1.Controls.Add(this.cboEstadoGarantia);
            this.ultraGroupBox1.Controls.Add(this.combotipo);
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox1.Location = new System.Drawing.Point(542, 6);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(310, 93);
            this.ultraGroupBox1.TabIndex = 1;
            this.ultraGroupBox1.Text = "Atenciones";
            // 
            // chbTipoIngreso
            // 
            this.chbTipoIngreso.AutoSize = true;
            this.chbTipoIngreso.Location = new System.Drawing.Point(22, 63);
            this.chbTipoIngreso.Name = "chbTipoIngreso";
            this.chbTipoIngreso.Size = new System.Drawing.Size(113, 17);
            this.chbTipoIngreso.TabIndex = 5;
            this.chbTipoIngreso.Text = "Tipo de Garantía :";
            this.chbTipoIngreso.UseVisualStyleBackColor = true;
            this.chbTipoIngreso.CheckedChanged += new System.EventHandler(this.chbTipoIngreso_CheckedChanged);
            // 
            // chkTratamiento
            // 
            this.chkTratamiento.AutoSize = true;
            this.chkTratamiento.Location = new System.Drawing.Point(22, 32);
            this.chkTratamiento.Name = "chkTratamiento";
            this.chkTratamiento.Size = new System.Drawing.Size(65, 17);
            this.chkTratamiento.TabIndex = 6;
            this.chkTratamiento.Text = "Estado :";
            this.chkTratamiento.UseVisualStyleBackColor = true;
            this.chkTratamiento.CheckedChanged += new System.EventHandler(this.chkTratamiento_CheckedChanged);
            // 
            // cboEstadoGarantia
            // 
            this.cboEstadoGarantia.FormattingEnabled = true;
            this.cboEstadoGarantia.Items.AddRange(new object[] {
            "Todas",
            "Caducadas",
            "Canceladas",
            "Vigentes"});
            this.cboEstadoGarantia.Location = new System.Drawing.Point(136, 30);
            this.cboEstadoGarantia.Name = "cboEstadoGarantia";
            this.cboEstadoGarantia.Size = new System.Drawing.Size(168, 21);
            this.cboEstadoGarantia.TabIndex = 0;
            // 
            // combotipo
            // 
            this.combotipo.FormattingEnabled = true;
            this.combotipo.Location = new System.Drawing.Point(136, 58);
            this.combotipo.Name = "combotipo";
            this.combotipo.Size = new System.Drawing.Size(168, 21);
            this.combotipo.TabIndex = 2;
            // 
            // grpFechas
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            this.grpFechas.Appearance = appearance1;
            this.grpFechas.Controls.Add(this.Fecha);
            this.grpFechas.Controls.Add(this.label1);
            this.grpFechas.Controls.Add(this.label4);
            this.grpFechas.Controls.Add(this.dtpFiltroHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroDesde);
            this.grpFechas.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.grpFechas.Location = new System.Drawing.Point(3, 6);
            this.grpFechas.Name = "grpFechas";
            this.grpFechas.Size = new System.Drawing.Size(284, 93);
            this.grpFechas.TabIndex = 0;
            this.grpFechas.Text = "Filtro por Fechas:";
            // 
            // Fecha
            // 
            this.Fecha.AutoSize = true;
            this.Fecha.Checked = true;
            this.Fecha.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Fecha.Location = new System.Drawing.Point(20, 46);
            this.Fecha.Name = "Fecha";
            this.Fecha.Size = new System.Drawing.Size(65, 17);
            this.Fecha.TabIndex = 7;
            this.Fecha.Text = "Estado :";
            this.Fecha.UseVisualStyleBackColor = true;
            this.Fecha.CheckedChanged += new System.EventHandler(this.Fecha_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Desde:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Hasta:";
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(165, 53);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroHasta.TabIndex = 4;
            // 
            // dtpFiltroDesde
            // 
            this.dtpFiltroDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroDesde.Location = new System.Drawing.Point(165, 31);
            this.dtpFiltroDesde.Name = "dtpFiltroDesde";
            this.dtpFiltroDesde.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroDesde.TabIndex = 3;
            this.dtpFiltroDesde.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // TablaGarantia
            // 
            this.TablaGarantia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.TablaGarantia.DisplayLayout.Appearance = appearance13;
            this.TablaGarantia.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.TablaGarantia.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.TablaGarantia.DisplayLayout.GroupByBox.Appearance = appearance14;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TablaGarantia.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
            this.TablaGarantia.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TablaGarantia.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
            this.TablaGarantia.DisplayLayout.MaxColScrollRegions = 1;
            this.TablaGarantia.DisplayLayout.MaxRowScrollRegions = 1;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TablaGarantia.DisplayLayout.Override.ActiveCellAppearance = appearance17;
            appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.TablaGarantia.DisplayLayout.Override.ActiveRowAppearance = appearance18;
            this.TablaGarantia.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.TablaGarantia.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.TablaGarantia.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.TablaGarantia.DisplayLayout.Override.CellAppearance = appearance20;
            this.TablaGarantia.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.TablaGarantia.DisplayLayout.Override.CellPadding = 0;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.TablaGarantia.DisplayLayout.Override.GroupByRowAppearance = appearance21;
            appearance22.TextHAlignAsString = "Left";
            this.TablaGarantia.DisplayLayout.Override.HeaderAppearance = appearance22;
            this.TablaGarantia.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.TablaGarantia.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            this.TablaGarantia.DisplayLayout.Override.RowAppearance = appearance23;
            this.TablaGarantia.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TablaGarantia.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
            this.TablaGarantia.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.TablaGarantia.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.TablaGarantia.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.TablaGarantia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TablaGarantia.Location = new System.Drawing.Point(3, 147);
            this.TablaGarantia.Name = "TablaGarantia";
            this.TablaGarantia.Size = new System.Drawing.Size(973, 245);
            this.TablaGarantia.TabIndex = 19;
            this.TablaGarantia.Visible = false;
            // 
            // TablaReporte
            // 
            this.TablaReporte.AllowUserToAddRows = false;
            this.TablaReporte.AllowUserToDeleteRows = false;
            this.TablaReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablaReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaReporte.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablaReporte.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.TablaReporte.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaReporte.Location = new System.Drawing.Point(3, 147);
            this.TablaReporte.Name = "TablaReporte";
            this.TablaReporte.ReadOnly = true;
            this.TablaReporte.Size = new System.Drawing.Size(973, 242);
            this.TablaReporte.TabIndex = 20;
            // 
            // frmReporteGarantia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 394);
            this.Controls.Add(this.TablaReporte);
            this.Controls.Add(this.TablaGarantia);
            this.Controls.Add(this.ultraPanel1);
            this.Controls.Add(this.toolStrip2);
            this.Name = "frmReporteGarantia";
            this.Text = "REPORTE DE GARANTÍA";
            this.Load += new System.EventHandler(this.frmReporteGarantia_Load);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).EndInit();
            this.grpFechas.ResumeLayout(false);
            this.grpFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaGarantia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnactualizar;
        private System.Windows.Forms.ToolStripButton btnexportar;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.ComboBox cboEstadoGarantia;
        private System.Windows.Forms.ComboBox combotipo;
        private Infragistics.Win.Misc.UltraGroupBox grpFechas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroDesde;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.Misc.UltraButton txtf1;
        private System.Windows.Forms.TextBox txthc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbTipoIngreso;
        private System.Windows.Forms.CheckBox chkTratamiento;
        private System.Windows.Forms.CheckBox Fecha;
        private Infragistics.Win.UltraWinGrid.UltraGrid TablaGarantia;
        private System.Windows.Forms.CheckBox Nhc;
        private System.Windows.Forms.DataGridView TablaReporte;
    }
}