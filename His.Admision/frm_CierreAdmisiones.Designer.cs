namespace His.Admision
{
    partial class frm_CierreAdmisiones
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
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
            this.tools = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBuscar = new System.Windows.Forms.ToolStripButton();
            this.btnimprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cmb_tipoatencion = new System.Windows.Forms.ComboBox();
            this.chbTipoIngreso = new System.Windows.Forms.CheckBox();
            this.chkTratamiento = new System.Windows.Forms.CheckBox();
            this.cboTipoIngreso = new System.Windows.Forms.ComboBox();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cmbUsuario = new System.Windows.Forms.ComboBox();
            this.chkUsuario = new System.Windows.Forms.CheckBox();
            this.grpFechas = new Infragistics.Win.Misc.UltraGroupBox();
            this.chkFacturacion = new System.Windows.Forms.CheckBox();
            this.chkAlta = new System.Windows.Forms.CheckBox();
            this.chkIngreso = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFiltroDesde = new System.Windows.Forms.DateTimePicker();
            this.ultraGridPacientes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.tools.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).BeginInit();
            this.grpFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPacientes)).BeginInit();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonActualizar,
            this.toolStripButtonBuscar,
            this.btnimprimir,
            this.toolStripSeparator3,
            this.toolStripButtonSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(875, 45);
            this.tools.TabIndex = 11;
            this.tools.Text = "toolStrip1";
            // 
            // toolStripButtonActualizar
            // 
            this.toolStripButtonActualizar.AutoSize = false;
            this.toolStripButtonActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonActualizar.Image = global::His.Admision.Properties.Resources.HIS_REFRESH;
            this.toolStripButtonActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonActualizar.Name = "toolStripButtonActualizar";
            this.toolStripButtonActualizar.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonActualizar.Text = "toolStripButton1";
            this.toolStripButtonActualizar.ToolTipText = "Actualizar";
            this.toolStripButtonActualizar.Click += new System.EventHandler(this.toolStripButtonActualizar_Click_1);
            // 
            // toolStripButtonBuscar
            // 
            this.toolStripButtonBuscar.AutoSize = false;
            this.toolStripButtonBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBuscar.Image = global::His.Admision.Properties.Resources.HIS_EXPORT_TO_EXCEL;
            this.toolStripButtonBuscar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBuscar.Name = "toolStripButtonBuscar";
            this.toolStripButtonBuscar.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonBuscar.Text = "Exportar";
            this.toolStripButtonBuscar.ToolTipText = "Exportar a excel";
            this.toolStripButtonBuscar.Click += new System.EventHandler(this.toolStripButtonBuscar_Click);
            // 
            // btnimprimir
            // 
            this.btnimprimir.AutoSize = false;
            this.btnimprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnimprimir.Image = global::His.Admision.Properties.Resources.HIS_IMPRIMIR;
            this.btnimprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnimprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnimprimir.Name = "btnimprimir";
            this.btnimprimir.Size = new System.Drawing.Size(42, 42);
            this.btnimprimir.Text = "Imprimir";
            this.btnimprimir.ToolTipText = "Imprimir";
            this.btnimprimir.Click += new System.EventHandler(this.btnimprimir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButtonSalir
            // 
            this.toolStripButtonSalir.AutoSize = false;
            this.toolStripButtonSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSalir.Image = global::His.Admision.Properties.Resources.HIS_SALIR;
            this.toolStripButtonSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSalir.Name = "toolStripButtonSalir";
            this.toolStripButtonSalir.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonSalir.Text = "toolStripButton1";
            this.toolStripButtonSalir.ToolTipText = "Salir";
            this.toolStripButtonSalir.Click += new System.EventHandler(this.toolStripButtonSalir_Click_1);
            // 
            // ultraPanel1
            // 
            this.ultraPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance14.BackColor = System.Drawing.Color.Silver;
            appearance14.BackColor2 = System.Drawing.Color.DimGray;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop20;
            appearance14.BackHatchStyle = Infragistics.Win.BackHatchStyle.DiagonalCross;
            this.ultraPanel1.Appearance = appearance14;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox2);
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.ultraPanel1.ClientArea.Controls.Add(this.grpFechas);
            this.ultraPanel1.Location = new System.Drawing.Point(2, 52);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(873, 99);
            this.ultraPanel1.TabIndex = 12;
            // 
            // ultraGroupBox2
            // 
            appearance15.BackColor = System.Drawing.Color.Transparent;
            this.ultraGroupBox2.Appearance = appearance15;
            this.ultraGroupBox2.Controls.Add(this.cmb_tipoatencion);
            this.ultraGroupBox2.Controls.Add(this.chbTipoIngreso);
            this.ultraGroupBox2.Controls.Add(this.chkTratamiento);
            this.ultraGroupBox2.Controls.Add(this.cboTipoIngreso);
            this.ultraGroupBox2.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox2.Location = new System.Drawing.Point(565, 3);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(299, 93);
            this.ultraGroupBox2.TabIndex = 5;
            this.ultraGroupBox2.Text = "Atenciones";
            // 
            // cmb_tipoatencion
            // 
            this.cmb_tipoatencion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_tipoatencion.Enabled = false;
            this.cmb_tipoatencion.FormattingEnabled = true;
            this.cmb_tipoatencion.Location = new System.Drawing.Point(120, 26);
            this.cmb_tipoatencion.Name = "cmb_tipoatencion";
            this.cmb_tipoatencion.Size = new System.Drawing.Size(163, 21);
            this.cmb_tipoatencion.TabIndex = 5;
            // 
            // chbTipoIngreso
            // 
            this.chbTipoIngreso.AutoSize = true;
            this.chbTipoIngreso.Location = new System.Drawing.Point(10, 61);
            this.chbTipoIngreso.Name = "chbTipoIngreso";
            this.chbTipoIngreso.Size = new System.Drawing.Size(85, 17);
            this.chbTipoIngreso.TabIndex = 4;
            this.chbTipoIngreso.Text = "Tipo Ingreso";
            this.chbTipoIngreso.UseVisualStyleBackColor = true;
            this.chbTipoIngreso.CheckedChanged += new System.EventHandler(this.chbTipoIngreso_CheckedChanged);
            this.chbTipoIngreso.ClientSizeChanged += new System.EventHandler(this.chbTipoIngreso_CheckedChanged_1);
            // 
            // chkTratamiento
            // 
            this.chkTratamiento.AutoSize = true;
            this.chkTratamiento.Location = new System.Drawing.Point(10, 29);
            this.chkTratamiento.Name = "chkTratamiento";
            this.chkTratamiento.Size = new System.Drawing.Size(106, 17);
            this.chkTratamiento.TabIndex = 4;
            this.chkTratamiento.Text = "Tipo Tratamiento";
            this.chkTratamiento.UseVisualStyleBackColor = true;
            this.chkTratamiento.CheckedChanged += new System.EventHandler(this.chkTratamiento_CheckedChanged);
            // 
            // cboTipoIngreso
            // 
            this.cboTipoIngreso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoIngreso.Enabled = false;
            this.cboTipoIngreso.FormattingEnabled = true;
            this.cboTipoIngreso.Location = new System.Drawing.Point(120, 61);
            this.cboTipoIngreso.Name = "cboTipoIngreso";
            this.cboTipoIngreso.Size = new System.Drawing.Size(163, 21);
            this.cboTipoIngreso.TabIndex = 2;
            // 
            // ultraGroupBox1
            // 
            appearance17.BackColor = System.Drawing.Color.Transparent;
            this.ultraGroupBox1.Appearance = appearance17;
            this.ultraGroupBox1.Controls.Add(this.cmbUsuario);
            this.ultraGroupBox1.Controls.Add(this.chkUsuario);
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox1.Location = new System.Drawing.Point(336, 3);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(220, 93);
            this.ultraGroupBox1.TabIndex = 4;
            this.ultraGroupBox1.Text = "Usuario";
            // 
            // cmbUsuario
            // 
            this.cmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsuario.FormattingEnabled = true;
            this.cmbUsuario.Location = new System.Drawing.Point(31, 35);
            this.cmbUsuario.Name = "cmbUsuario";
            this.cmbUsuario.Size = new System.Drawing.Size(178, 21);
            this.cmbUsuario.TabIndex = 274;
            // 
            // chkUsuario
            // 
            this.chkUsuario.AutoSize = true;
            this.chkUsuario.Checked = true;
            this.chkUsuario.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUsuario.Location = new System.Drawing.Point(10, 38);
            this.chkUsuario.Name = "chkUsuario";
            this.chkUsuario.Size = new System.Drawing.Size(15, 14);
            this.chkUsuario.TabIndex = 272;
            this.chkUsuario.UseVisualStyleBackColor = true;
            this.chkUsuario.CheckedChanged += new System.EventHandler(this.chkUsuario_CheckedChanged);
            // 
            // grpFechas
            // 
            appearance16.BackColor = System.Drawing.Color.Transparent;
            this.grpFechas.Appearance = appearance16;
            this.grpFechas.Controls.Add(this.chkFacturacion);
            this.grpFechas.Controls.Add(this.chkAlta);
            this.grpFechas.Controls.Add(this.chkIngreso);
            this.grpFechas.Controls.Add(this.label1);
            this.grpFechas.Controls.Add(this.lblHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroDesde);
            this.grpFechas.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.grpFechas.Location = new System.Drawing.Point(3, 3);
            this.grpFechas.Name = "grpFechas";
            this.grpFechas.Size = new System.Drawing.Size(327, 93);
            this.grpFechas.TabIndex = 3;
            this.grpFechas.Text = "Filtro por Fechas:";
            // 
            // chkFacturacion
            // 
            this.chkFacturacion.AutoSize = true;
            this.chkFacturacion.Checked = true;
            this.chkFacturacion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFacturacion.Location = new System.Drawing.Point(11, 65);
            this.chkFacturacion.Name = "chkFacturacion";
            this.chkFacturacion.Size = new System.Drawing.Size(82, 17);
            this.chkFacturacion.TabIndex = 10;
            this.chkFacturacion.Text = "Facturacion";
            this.chkFacturacion.UseVisualStyleBackColor = true;
            // 
            // chkAlta
            // 
            this.chkAlta.AutoSize = true;
            this.chkAlta.Checked = true;
            this.chkAlta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlta.Location = new System.Drawing.Point(11, 45);
            this.chkAlta.Name = "chkAlta";
            this.chkAlta.Size = new System.Drawing.Size(89, 17);
            this.chkAlta.TabIndex = 9;
            this.chkAlta.Text = "Alta Paciente";
            this.chkAlta.UseVisualStyleBackColor = true;
            // 
            // chkIngreso
            // 
            this.chkIngreso.AutoSize = true;
            this.chkIngreso.Checked = true;
            this.chkIngreso.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIngreso.Location = new System.Drawing.Point(11, 26);
            this.chkIngreso.Name = "chkIngreso";
            this.chkIngreso.Size = new System.Drawing.Size(106, 17);
            this.chkIngreso.TabIndex = 8;
            this.chkIngreso.Text = "Ingreso Paciente";
            this.chkIngreso.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(165, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Desde:";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(165, 63);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 6;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(212, 59);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroHasta.TabIndex = 4;
            // 
            // dtpFiltroDesde
            // 
            this.dtpFiltroDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroDesde.Location = new System.Drawing.Point(212, 30);
            this.dtpFiltroDesde.Name = "dtpFiltroDesde";
            this.dtpFiltroDesde.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroDesde.TabIndex = 3;
            this.dtpFiltroDesde.Value = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            // 
            // ultraGridPacientes
            // 
            this.ultraGridPacientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.SystemColors.Window;
            appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridPacientes.DisplayLayout.Appearance = appearance2;
            this.ultraGridPacientes.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridPacientes.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance3.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridPacientes.DisplayLayout.GroupByBox.Appearance = appearance3;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridPacientes.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
            this.ultraGridPacientes.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance5.BackColor2 = System.Drawing.SystemColors.Control;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridPacientes.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
            this.ultraGridPacientes.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridPacientes.DisplayLayout.MaxRowScrollRegions = 1;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridPacientes.DisplayLayout.Override.ActiveCellAppearance = appearance6;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridPacientes.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.ultraGridPacientes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGridPacientes.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridPacientes.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridPacientes.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance8.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridPacientes.DisplayLayout.Override.CardAreaAppearance = appearance8;
            appearance9.BorderColor = System.Drawing.Color.Silver;
            appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridPacientes.DisplayLayout.Override.CellAppearance = appearance9;
            this.ultraGridPacientes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridPacientes.DisplayLayout.Override.CellPadding = 0;
            appearance10.BackColor = System.Drawing.SystemColors.Control;
            appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance10.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridPacientes.DisplayLayout.Override.GroupByRowAppearance = appearance10;
            appearance11.TextHAlignAsString = "Left";
            this.ultraGridPacientes.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.ultraGridPacientes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridPacientes.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridPacientes.DisplayLayout.Override.RowAppearance = appearance12;
            this.ultraGridPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridPacientes.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
            this.ultraGridPacientes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridPacientes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridPacientes.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGridPacientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGridPacientes.Location = new System.Drawing.Point(10, 159);
            this.ultraGridPacientes.Name = "ultraGridPacientes";
            this.ultraGridPacientes.Size = new System.Drawing.Size(864, 232);
            this.ultraGridPacientes.TabIndex = 13;
            this.ultraGridPacientes.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridPacientes_InitializeLayout);
            this.ultraGridPacientes.BeforeEnterEditMode += new System.ComponentModel.CancelEventHandler(this.ultraGridPacientes_BeforeEnterEditMode);
            this.ultraGridPacientes.BeforeExitEditMode += new Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventHandler(this.ultraGridPacientes_BeforeExitEditMode);
            // 
            // frm_CierreAdmisiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 399);
            this.Controls.Add(this.ultraGridPacientes);
            this.Controls.Add(this.ultraPanel1);
            this.Controls.Add(this.tools);
            this.Name = "frm_CierreAdmisiones";
            this.Text = "Cierre de Admisiones";
            this.Load += new System.EventHandler(this.frm_ExploradorIngresos_Load);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPacientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualizar;
        private System.Windows.Forms.ToolStripButton toolStripButtonBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonSalir;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.ComboBox cmb_tipoatencion;
        private System.Windows.Forms.CheckBox chbTipoIngreso;
        private System.Windows.Forms.CheckBox chkTratamiento;
        private System.Windows.Forms.ComboBox cboTipoIngreso;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraGroupBox grpFechas;
        private System.Windows.Forms.CheckBox chkFacturacion;
        private System.Windows.Forms.CheckBox chkAlta;
        private System.Windows.Forms.CheckBox chkIngreso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroDesde;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridPacientes;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.ComboBox cmbUsuario;
        private System.Windows.Forms.CheckBox chkUsuario;
        private System.Windows.Forms.ToolStripButton btnimprimir;
    }
}