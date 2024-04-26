namespace His.Honorarios
{
    partial class frm_ConsultaHonorariosMedico
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
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.ayudaMedicos = new Infragistics.Win.Misc.UltraButton();
            this.txtCodMedico = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbFormaPago = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbTipoPago = new System.Windows.Forms.ComboBox();
            this.dtpFactMedHasta = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFactMedDesde = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNomMedico = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dbugrFacturasIngresadas = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.frm_ConsultaHonorariosMedico_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.groupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dbugrFacturasIngresadas)).BeginInit();
            this.frm_ConsultaHonorariosMedico_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_ConsultaHonorariosMedico_Fill_Panel.SuspendLayout();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ayudaMedicos
            // 
            this.ayudaMedicos.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.ayudaMedicos.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaMedicos.Location = new System.Drawing.Point(171, 13);
            this.ayudaMedicos.Name = "ayudaMedicos";
            this.ayudaMedicos.Size = new System.Drawing.Size(25, 18);
            this.ayudaMedicos.TabIndex = 241;
            this.ayudaMedicos.TabStop = false;
            this.ayudaMedicos.Text = "F1";
            this.ayudaMedicos.Click += new System.EventHandler(this.ayudaMedicos_Click);
            // 
            // txtCodMedico
            // 
            this.txtCodMedico.Location = new System.Drawing.Point(202, 13);
            this.txtCodMedico.Mask = "99999999";
            this.txtCodMedico.Name = "txtCodMedico";
            this.txtCodMedico.Size = new System.Drawing.Size(59, 20);
            this.txtCodMedico.TabIndex = 145;
            this.txtCodMedico.ValidatingType = typeof(int);
            this.txtCodMedico.TextChanged += new System.EventHandler(this.maskedTextBox1_TextChanged);
            this.txtCodMedico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodMedico_KeyDown);
            // 
            // label8
            // 
            this.label8.AccessibleName = "11";
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(408, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Difiere de: ";
            // 
            // cbFormaPago
            // 
            this.cbFormaPago.AccessibleName = "11";
            this.cbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormaPago.FormattingEnabled = true;
            this.cbFormaPago.Location = new System.Drawing.Point(481, 36);
            this.cbFormaPago.Name = "cbFormaPago";
            this.cbFormaPago.Size = new System.Drawing.Size(178, 21);
            this.cbFormaPago.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AccessibleName = "11";
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(96, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Forma Pago:";
            // 
            // cbTipoPago
            // 
            this.cbTipoPago.AccessibleName = "11";
            this.cbTipoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoPago.FormattingEnabled = true;
            this.cbTipoPago.Location = new System.Drawing.Point(202, 36);
            this.cbTipoPago.Name = "cbTipoPago";
            this.cbTipoPago.Size = new System.Drawing.Size(186, 21);
            this.cbTipoPago.TabIndex = 15;
            this.cbTipoPago.SelectedValueChanged += new System.EventHandler(this.cbTipoPago_SelectedValueChanged);
            // 
            // dtpFactMedHasta
            // 
            this.dtpFactMedHasta.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFactMedHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFactMedHasta.Location = new System.Drawing.Point(391, 15);
            this.dtpFactMedHasta.Name = "dtpFactMedHasta";
            this.dtpFactMedHasta.Size = new System.Drawing.Size(110, 22);
            this.dtpFactMedHasta.TabIndex = 10;
            this.dtpFactMedHasta.ValueChanged += new System.EventHandler(this.dtpFactMedHasta_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(65, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Desde:";
            // 
            // dtpFactMedDesde
            // 
            this.dtpFactMedDesde.CalendarFont = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFactMedDesde.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFactMedDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFactMedDesde.Location = new System.Drawing.Point(112, 15);
            this.dtpFactMedDesde.MinDate = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.dtpFactMedDesde.Name = "dtpFactMedDesde";
            this.dtpFactMedDesde.Size = new System.Drawing.Size(107, 22);
            this.dtpFactMedDesde.TabIndex = 9;
            this.dtpFactMedDesde.ValueChanged += new System.EventHandler(this.dtpFactMedDesde_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(347, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Hasta:";
            // 
            // txtNomMedico
            // 
            this.txtNomMedico.AccessibleName = "11";
            this.txtNomMedico.Location = new System.Drawing.Point(260, 13);
            this.txtNomMedico.Name = "txtNomMedico";
            this.txtNomMedico.Size = new System.Drawing.Size(399, 20);
            this.txtNomMedico.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AccessibleName = "11";
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(96, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Médico:";
            // 
            // dbugrFacturasIngresadas
            // 
            this.dbugrFacturasIngresadas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dbugrFacturasIngresadas.DisplayLayout.Appearance = appearance4;
            this.dbugrFacturasIngresadas.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dbugrFacturasIngresadas.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.dbugrFacturasIngresadas.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dbugrFacturasIngresadas.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.dbugrFacturasIngresadas.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dbugrFacturasIngresadas.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.dbugrFacturasIngresadas.DisplayLayout.MaxColScrollRegions = 1;
            this.dbugrFacturasIngresadas.DisplayLayout.MaxRowScrollRegions = 1;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.CellAppearance = appearance5;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance11.TextHAlignAsString = "Left";
            this.dbugrFacturasIngresadas.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.RowAppearance = appearance10;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dbugrFacturasIngresadas.DisplayLayout.Override.TemplateAddRowAppearance = appearance8;
            this.dbugrFacturasIngresadas.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dbugrFacturasIngresadas.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dbugrFacturasIngresadas.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.dbugrFacturasIngresadas.Location = new System.Drawing.Point(12, 155);
            this.dbugrFacturasIngresadas.Name = "dbugrFacturasIngresadas";
            this.dbugrFacturasIngresadas.Size = new System.Drawing.Size(794, 120);
            this.dbugrFacturasIngresadas.TabIndex = 1;
            this.dbugrFacturasIngresadas.Text = "ultraGrid1";
            this.dbugrFacturasIngresadas.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grid_InitializeLayout);
            this.dbugrFacturasIngresadas.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.dbugrFacturasIngresadas_DoubleClickRow);
            this.dbugrFacturasIngresadas.Click += new System.EventHandler(this.dbugrFacturasIngresadas_Click);
            // 
            // frm_ConsultaHonorariosMedico_Fill_Panel
            // 
            appearance14.BackColor = System.Drawing.Color.GhostWhite;
            appearance14.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_ConsultaHonorariosMedico_Fill_Panel.Appearance = appearance14;
            // 
            // frm_ConsultaHonorariosMedico_Fill_Panel.ClientArea
            // 
            this.frm_ConsultaHonorariosMedico_Fill_Panel.ClientArea.Controls.Add(this.menu);
            this.frm_ConsultaHonorariosMedico_Fill_Panel.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.frm_ConsultaHonorariosMedico_Fill_Panel.ClientArea.Controls.Add(this.dbugrFacturasIngresadas);
            this.frm_ConsultaHonorariosMedico_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_ConsultaHonorariosMedico_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_ConsultaHonorariosMedico_Fill_Panel.Location = new System.Drawing.Point(0, 0);
            this.frm_ConsultaHonorariosMedico_Fill_Panel.Name = "frm_ConsultaHonorariosMedico_Fill_Panel";
            this.frm_ConsultaHonorariosMedico_Fill_Panel.Size = new System.Drawing.Size(818, 278);
            this.frm_ConsultaHonorariosMedico_Fill_Panel.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnActualizar,
            this.toolStripButton1,
            this.btnCancelar,
            this.btnSalir});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(818, 37);
            this.menu.TabIndex = 3;
            this.menu.Text = "menu";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.Color.Black;
            this.btnActualizar.Image = global::His.Honorarios.Properties.Resources.HIS_REFRESH;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(36, 34);
            this.btnActualizar.ToolTipText = "Buscar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton1.Image = global::His.Honorarios.Properties.Resources.HIS_EXPORT_TO_EXCEL;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 34);
            this.toolStripButton1.ToolTipText = "Exportar";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Image = global::His.Honorarios.Properties.Resources.Button_Delete_01_25095;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(36, 34);
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Black;
            this.btnSalir.Image = global::His.Honorarios.Properties.Resources.HIS_SALIR;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(36, 34);
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox1.Controls.Add(this.btnExcel);
            this.ultraGroupBox1.Controls.Add(this.groupBox1);
            this.ultraGroupBox1.Controls.Add(this.ayudaMedicos);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Controls.Add(this.txtCodMedico);
            this.ultraGroupBox1.Controls.Add(this.txtNomMedico);
            this.ultraGroupBox1.Controls.Add(this.label8);
            this.ultraGroupBox1.Controls.Add(this.cbFormaPago);
            this.ultraGroupBox1.Controls.Add(this.label7);
            this.ultraGroupBox1.Controls.Add(this.cbTipoPago);
            this.ultraGroupBox1.Location = new System.Drawing.Point(12, 35);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(793, 119);
            this.ultraGroupBox1.TabIndex = 2;
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.LightGray;
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = global::His.Honorarios.Properties.Resources.file_type_excel_icon_130611;
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(681, 36);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(99, 42);
            this.btnExcel.TabIndex = 3;
            this.btnExcel.Text = "Exportar Excel";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Visible = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // groupBox1
            // 
            appearance16.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Appearance = appearance16;
            this.groupBox1.Controls.Add(this.dtpFactMedHasta);
            this.groupBox1.Controls.Add(this.dtpFactMedDesde);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(90, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 50);
            this.groupBox1.TabIndex = 243;
            this.groupBox1.Text = "Fecha de Factura";
            // 
            // frm_ConsultaHonorariosMedico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(818, 278);
            this.Controls.Add(this.frm_ConsultaHonorariosMedico_Fill_Panel);
            this.Name = "frm_ConsultaHonorariosMedico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Honorarios por Médico";
            this.Load += new System.EventHandler(this.frm_ConsultaHonorariosMedico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbugrFacturasIngresadas)).EndInit();
            this.frm_ConsultaHonorariosMedico_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_ConsultaHonorariosMedico_Fill_Panel.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid dbugrFacturasIngresadas;
        private System.Windows.Forms.TextBox txtNomMedico;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFactMedHasta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFactMedDesde;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbFormaPago;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbTipoPago;
        private System.Windows.Forms.MaskedTextBox txtCodMedico;
        private Infragistics.Win.Misc.UltraButton ayudaMedicos;
        private Infragistics.Win.Misc.UltraPanel frm_ConsultaHonorariosMedico_Fill_Panel;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraGroupBox groupBox1;
        private System.Windows.Forms.Button btnExcel;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton toolStripButton1;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnSalir;
    }
}