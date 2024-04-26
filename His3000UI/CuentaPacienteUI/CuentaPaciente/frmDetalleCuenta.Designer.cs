namespace CuentaPaciente
{
    partial class frmDetalleCuenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetalleCuenta));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_Imprimir = new System.Windows.Forms.ToolStripButton();
            this.btn_Excel = new System.Windows.Forms.ToolStripButton();
            this.btn_Salir = new System.Windows.Forms.ToolStripButton();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.lblFechaAlta = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumeroAtencion = new System.Windows.Forms.Label();
            this.lbl_MedicoP = new System.Windows.Forms.Label();
            this.lbl_Medico = new System.Windows.Forms.Label();
            this.lbl_EdadAños = new System.Windows.Forms.Label();
            this.lbl_Edad = new System.Windows.Forms.Label();
            this.lbl_NroSeguro = new System.Windows.Forms.Label();
            this.lbl_Seguro = new System.Windows.Forms.Label();
            this.lbl_Fecha = new System.Windows.Forms.Label();
            this.lbl_FechaIngreso = new System.Windows.Forms.Label();
            this.lbl_NroAtencion = new System.Windows.Forms.Label();
            this.lbl_Paciente = new System.Windows.Forms.Label();
            this.lbl_HistoriaC = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lbl_Atencion = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.ulgdbListadoCIE = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.ultraTabbedMdiManager1 = new Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grvItemsEliminados = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgrItemsModificados = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgrItemsNuevos = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ulgdbListadoCIE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiManager1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvItemsEliminados)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrItemsModificados)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrItemsNuevos)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Imprimir,
            this.btn_Excel,
            this.btn_Salir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1017, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_Imprimir
            // 
            this.btn_Imprimir.Image = ((System.Drawing.Image)(resources.GetObject("btn_Imprimir.Image")));
            this.btn_Imprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Imprimir.Name = "btn_Imprimir";
            this.btn_Imprimir.Size = new System.Drawing.Size(89, 36);
            this.btn_Imprimir.Text = "Imprimir";
            this.btn_Imprimir.Click += new System.EventHandler(this.btn_Imprimir_Click);
            // 
            // btn_Excel
            // 
            this.btn_Excel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Excel.Image")));
            this.btn_Excel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Excel.Name = "btn_Excel";
            this.btn_Excel.Size = new System.Drawing.Size(70, 36);
            this.btn_Excel.Text = "Excel";
            this.btn_Excel.Click += new System.EventHandler(this.btn_Excel_Click);
            // 
            // btn_Salir
            // 
            this.btn_Salir.Image = ((System.Drawing.Image)(resources.GetObject("btn_Salir.Image")));
            this.btn_Salir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(65, 36);
            this.btn_Salir.Text = "Salir";
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.lblFechaAlta);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Controls.Add(this.lblNumeroAtencion);
            this.ultraGroupBox1.Controls.Add(this.lbl_MedicoP);
            this.ultraGroupBox1.Controls.Add(this.lbl_Medico);
            this.ultraGroupBox1.Controls.Add(this.lbl_EdadAños);
            this.ultraGroupBox1.Controls.Add(this.lbl_Edad);
            this.ultraGroupBox1.Controls.Add(this.lbl_NroSeguro);
            this.ultraGroupBox1.Controls.Add(this.lbl_Seguro);
            this.ultraGroupBox1.Controls.Add(this.lbl_Fecha);
            this.ultraGroupBox1.Controls.Add(this.lbl_FechaIngreso);
            this.ultraGroupBox1.Controls.Add(this.lbl_NroAtencion);
            this.ultraGroupBox1.Controls.Add(this.lbl_Paciente);
            this.ultraGroupBox1.Controls.Add(this.lbl_HistoriaC);
            this.ultraGroupBox1.Controls.Add(this.label24);
            this.ultraGroupBox1.Controls.Add(this.lbl_Atencion);
            this.ultraGroupBox1.Controls.Add(this.label23);
            this.ultraGroupBox1.Location = new System.Drawing.Point(-1, 42);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(1018, 76);
            this.ultraGroupBox1.TabIndex = 4;
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // lblFechaAlta
            // 
            this.lblFechaAlta.AutoSize = true;
            this.lblFechaAlta.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaAlta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaAlta.Location = new System.Drawing.Point(109, 55);
            this.lblFechaAlta.Name = "lblFechaAlta";
            this.lblFechaAlta.Size = new System.Drawing.Size(94, 15);
            this.lblFechaAlta.TabIndex = 87;
            this.lblFechaAlta.Text = "Fecha de Alta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 86;
            this.label1.Text = "Fecha Alta :";
            // 
            // lblNumeroAtencion
            // 
            this.lblNumeroAtencion.AutoSize = true;
            this.lblNumeroAtencion.BackColor = System.Drawing.Color.Transparent;
            this.lblNumeroAtencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroAtencion.Location = new System.Drawing.Point(573, 7);
            this.lblNumeroAtencion.Name = "lblNumeroAtencion";
            this.lblNumeroAtencion.Size = new System.Drawing.Size(62, 15);
            this.lblNumeroAtencion.TabIndex = 85;
            this.lblNumeroAtencion.Text = "Atención";
            // 
            // lbl_MedicoP
            // 
            this.lbl_MedicoP.AutoSize = true;
            this.lbl_MedicoP.BackColor = System.Drawing.Color.Transparent;
            this.lbl_MedicoP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MedicoP.Location = new System.Drawing.Point(570, 39);
            this.lbl_MedicoP.Name = "lbl_MedicoP";
            this.lbl_MedicoP.Size = new System.Drawing.Size(63, 15);
            this.lbl_MedicoP.TabIndex = 84;
            this.lbl_MedicoP.Text = "MédicoP";
            // 
            // lbl_Medico
            // 
            this.lbl_Medico.AutoSize = true;
            this.lbl_Medico.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Medico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Medico.Location = new System.Drawing.Point(505, 39);
            this.lbl_Medico.Name = "lbl_Medico";
            this.lbl_Medico.Size = new System.Drawing.Size(59, 16);
            this.lbl_Medico.TabIndex = 83;
            this.lbl_Medico.Text = "Médico :";
            // 
            // lbl_EdadAños
            // 
            this.lbl_EdadAños.AutoSize = true;
            this.lbl_EdadAños.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EdadAños.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EdadAños.Location = new System.Drawing.Point(776, 7);
            this.lbl_EdadAños.Name = "lbl_EdadAños";
            this.lbl_EdadAños.Size = new System.Drawing.Size(110, 16);
            this.lbl_EdadAños.TabIndex = 82;
            this.lbl_EdadAños.Text = "Edad Paciente";
            // 
            // lbl_Edad
            // 
            this.lbl_Edad.AutoSize = true;
            this.lbl_Edad.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Edad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Edad.Location = new System.Drawing.Point(730, 7);
            this.lbl_Edad.Name = "lbl_Edad";
            this.lbl_Edad.Size = new System.Drawing.Size(47, 16);
            this.lbl_Edad.TabIndex = 81;
            this.lbl_Edad.Text = "Edad :";
            // 
            // lbl_NroSeguro
            // 
            this.lbl_NroSeguro.AutoSize = true;
            this.lbl_NroSeguro.BackColor = System.Drawing.Color.Transparent;
            this.lbl_NroSeguro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NroSeguro.Location = new System.Drawing.Point(571, 21);
            this.lbl_NroSeguro.Name = "lbl_NroSeguro";
            this.lbl_NroSeguro.Size = new System.Drawing.Size(85, 15);
            this.lbl_NroSeguro.TabIndex = 80;
            this.lbl_NroSeguro.Text = "Tipo Seguro";
            // 
            // lbl_Seguro
            // 
            this.lbl_Seguro.AutoSize = true;
            this.lbl_Seguro.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Seguro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Seguro.Location = new System.Drawing.Point(506, 21);
            this.lbl_Seguro.Name = "lbl_Seguro";
            this.lbl_Seguro.Size = new System.Drawing.Size(58, 16);
            this.lbl_Seguro.TabIndex = 79;
            this.lbl_Seguro.Text = "Seguro :";
            // 
            // lbl_Fecha
            // 
            this.lbl_Fecha.AutoSize = true;
            this.lbl_Fecha.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Fecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Fecha.Location = new System.Drawing.Point(109, 39);
            this.lbl_Fecha.Name = "lbl_Fecha";
            this.lbl_Fecha.Size = new System.Drawing.Size(125, 15);
            this.lbl_Fecha.TabIndex = 78;
            this.lbl_Fecha.Text = "Fecha de Atención";
            // 
            // lbl_FechaIngreso
            // 
            this.lbl_FechaIngreso.AutoSize = true;
            this.lbl_FechaIngreso.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FechaIngreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FechaIngreso.Location = new System.Drawing.Point(6, 39);
            this.lbl_FechaIngreso.Name = "lbl_FechaIngreso";
            this.lbl_FechaIngreso.Size = new System.Drawing.Size(100, 16);
            this.lbl_FechaIngreso.TabIndex = 77;
            this.lbl_FechaIngreso.Text = "Fecha Ingreso :";
            // 
            // lbl_NroAtencion
            // 
            this.lbl_NroAtencion.AutoSize = true;
            this.lbl_NroAtencion.BackColor = System.Drawing.Color.Transparent;
            this.lbl_NroAtencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NroAtencion.Location = new System.Drawing.Point(572, 6);
            this.lbl_NroAtencion.Name = "lbl_NroAtencion";
            this.lbl_NroAtencion.Size = new System.Drawing.Size(62, 15);
            this.lbl_NroAtencion.TabIndex = 76;
            this.lbl_NroAtencion.Text = "Atención";
            // 
            // lbl_Paciente
            // 
            this.lbl_Paciente.AutoSize = true;
            this.lbl_Paciente.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Paciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Paciente.Location = new System.Drawing.Point(109, 23);
            this.lbl_Paciente.Name = "lbl_Paciente";
            this.lbl_Paciente.Size = new System.Drawing.Size(63, 15);
            this.lbl_Paciente.TabIndex = 75;
            this.lbl_Paciente.Text = "Paciente";
            // 
            // lbl_HistoriaC
            // 
            this.lbl_HistoriaC.AutoSize = true;
            this.lbl_HistoriaC.BackColor = System.Drawing.Color.Transparent;
            this.lbl_HistoriaC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_HistoriaC.Location = new System.Drawing.Point(109, 7);
            this.lbl_HistoriaC.Name = "lbl_HistoriaC";
            this.lbl_HistoriaC.Size = new System.Drawing.Size(105, 15);
            this.lbl_HistoriaC.TabIndex = 74;
            this.lbl_HistoriaC.Text = "Historia Clínica";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(39, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 16);
            this.label24.TabIndex = 70;
            this.label24.Text = "Paciente :";
            // 
            // lbl_Atencion
            // 
            this.lbl_Atencion.AutoSize = true;
            this.lbl_Atencion.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Atencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Atencion.Location = new System.Drawing.Point(471, 7);
            this.lbl_Atencion.Name = "lbl_Atencion";
            this.lbl_Atencion.Size = new System.Drawing.Size(94, 16);
            this.lbl_Atencion.TabIndex = 72;
            this.lbl_Atencion.Text = "Nro. Atención :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(29, 7);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(77, 16);
            this.label23.TabIndex = 66;
            this.label23.Text = "N° Historia :";
            // 
            // ulgdbListadoCIE
            // 
            this.ulgdbListadoCIE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ulgdbListadoCIE.DisplayLayout.Appearance = appearance1;
            this.ulgdbListadoCIE.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ulgdbListadoCIE.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ulgdbListadoCIE.DisplayLayout.MaxColScrollRegions = 1;
            this.ulgdbListadoCIE.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ulgdbListadoCIE.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ulgdbListadoCIE.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.ulgdbListadoCIE.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ulgdbListadoCIE.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ulgdbListadoCIE.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ulgdbListadoCIE.DisplayLayout.Override.CellAppearance = appearance8;
            this.ulgdbListadoCIE.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ulgdbListadoCIE.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ulgdbListadoCIE.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.ulgdbListadoCIE.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ulgdbListadoCIE.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ulgdbListadoCIE.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ulgdbListadoCIE.DisplayLayout.Override.RowAppearance = appearance11;
            this.ulgdbListadoCIE.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ulgdbListadoCIE.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.ulgdbListadoCIE.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ulgdbListadoCIE.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ulgdbListadoCIE.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ulgdbListadoCIE.Location = new System.Drawing.Point(0, 119);
            this.ulgdbListadoCIE.Name = "ulgdbListadoCIE";
            this.ulgdbListadoCIE.Size = new System.Drawing.Size(1017, 374);
            this.ulgdbListadoCIE.TabIndex = 5;
            this.ulgdbListadoCIE.Text = "Detalle Cuentas";
            this.ulgdbListadoCIE.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ulgdbListadoCIE_InitializeLayout);
            this.ulgdbListadoCIE.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ulgdbListadoCIE_InitializeRow);
            this.ulgdbListadoCIE.AfterRowExpanded += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.ulgdbListadoCIE_AfterRowExpanded);
            this.ulgdbListadoCIE.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ulgdbListadoCIE_ClickCell);
            // 
            // ultraTabbedMdiManager1
            // 
            this.ultraTabbedMdiManager1.MdiParent = this;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 499);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1017, 177);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grvItemsEliminados);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1009, 151);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Items Eliminados";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grvItemsEliminados
            // 
            this.grvItemsEliminados.AllowUserToAddRows = false;
            this.grvItemsEliminados.AllowUserToDeleteRows = false;
            this.grvItemsEliminados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvItemsEliminados.Location = new System.Drawing.Point(6, 4);
            this.grvItemsEliminados.Name = "grvItemsEliminados";
            this.grvItemsEliminados.Size = new System.Drawing.Size(995, 142);
            this.grvItemsEliminados.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgrItemsModificados);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1009, 151);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Items Modificados";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgrItemsModificados
            // 
            this.dgrItemsModificados.AllowUserToAddRows = false;
            this.dgrItemsModificados.AllowUserToDeleteRows = false;
            this.dgrItemsModificados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrItemsModificados.Location = new System.Drawing.Point(6, 4);
            this.dgrItemsModificados.Name = "dgrItemsModificados";
            this.dgrItemsModificados.Size = new System.Drawing.Size(893, 142);
            this.dgrItemsModificados.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgrItemsNuevos);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1009, 151);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Items Nuevos";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgrItemsNuevos
            // 
            this.dgrItemsNuevos.AllowUserToAddRows = false;
            this.dgrItemsNuevos.AllowUserToDeleteRows = false;
            this.dgrItemsNuevos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrItemsNuevos.Location = new System.Drawing.Point(6, 4);
            this.dgrItemsNuevos.Name = "dgrItemsNuevos";
            this.dgrItemsNuevos.Size = new System.Drawing.Size(885, 142);
            this.dgrItemsNuevos.TabIndex = 3;
            // 
            // frmDetalleCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 678);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ulgdbListadoCIE);
            this.Controls.Add(this.ultraGroupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmDetalleCuenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle Cuenta";
            this.Load += new System.EventHandler(this.frmDetalleCuenta_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ulgdbListadoCIE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiManager1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvItemsEliminados)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrItemsModificados)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrItemsNuevos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lbl_Atencion;
        private System.Windows.Forms.Label label23;
        private Infragistics.Win.UltraWinGrid.UltraGrid ulgdbListadoCIE;
        private System.Windows.Forms.Label lbl_HistoriaC;
        private System.Windows.Forms.Label lbl_Paciente;
        private System.Windows.Forms.Label lbl_NroSeguro;
        private System.Windows.Forms.Label lbl_Seguro;
        private System.Windows.Forms.Label lbl_Fecha;
        private System.Windows.Forms.Label lbl_FechaIngreso;
        private System.Windows.Forms.Label lbl_NroAtencion;
        private System.Windows.Forms.Label lbl_Edad;
        private System.Windows.Forms.Label lbl_MedicoP;
        private System.Windows.Forms.Label lbl_Medico;
        private System.Windows.Forms.Label lbl_EdadAños;
        private System.Windows.Forms.ToolStripButton btn_Salir;
        private System.Windows.Forms.ToolStripButton btn_Imprimir;
        private System.Windows.Forms.ToolStripButton btn_Excel;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.Label lblNumeroAtencion;
        private Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager ultraTabbedMdiManager1;
        private System.Windows.Forms.Label lblFechaAlta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView grvItemsEliminados;
        private System.Windows.Forms.DataGridView dgrItemsModificados;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgrItemsNuevos;
    }
}