
namespace CuentaPaciente
{
    partial class frmExploradorAuditoria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExploradorAuditoria));
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
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
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraPanel2 = new Infragistics.Win.Misc.UltraPanel();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ckbDepartamento = new System.Windows.Forms.CheckBox();
            this.cmbDepartamento = new System.Windows.Forms.ComboBox();
            this.cmbPiso = new System.Windows.Forms.ComboBox();
            this.chkHab = new System.Windows.Forms.CheckBox();
            this.chkPiso = new System.Windows.Forms.CheckBox();
            this.cmbHabitacion = new System.Windows.Forms.ComboBox();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkHC = new System.Windows.Forms.CheckBox();
            this.ayudaPacientes = new Infragistics.Win.Misc.UltraButton();
            this.txt_historiaclinica = new System.Windows.Forms.TextBox();
            this.grpFechas = new Infragistics.Win.Misc.UltraGroupBox();
            this.chkAlta = new System.Windows.Forms.CheckBox();
            this.chkIngreso = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtphasta = new System.Windows.Forms.DateTimePicker();
            this.dtpdesde = new System.Windows.Forms.DateTimePicker();
            this.ultraGridAuditoria = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.nuevoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tools.SuspendLayout();
            this.ultraPanel2.ClientArea.SuspendLayout();
            this.ultraPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).BeginInit();
            this.grpFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridAuditoria)).BeginInit();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnActualizar,
            this.btnExportar,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.toolStripButtonSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(896, 45);
            this.tools.TabIndex = 12;
            this.tools.Text = "toolStrip1";
            // 
            // btnActualizar
            // 
            this.btnActualizar.AutoSize = false;
            this.btnActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(42, 42);
            this.btnActualizar.Text = "Buscar";
            this.btnActualizar.ToolTipText = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.AutoSize = false;
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(42, 42);
            this.btnExportar.Text = "Exportar";
            this.btnExportar.ToolTipText = "Exportar a excel";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.AutoSize = false;
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(42, 42);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.ToolTipText = "Exportar a excel";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButtonSalir
            // 
            this.toolStripButtonSalir.AutoSize = false;
            this.toolStripButtonSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSalir.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSalir.Image")));
            this.toolStripButtonSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSalir.Name = "toolStripButtonSalir";
            this.toolStripButtonSalir.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonSalir.Text = "toolStripButton1";
            this.toolStripButtonSalir.ToolTipText = "Salir";
            this.toolStripButtonSalir.Click += new System.EventHandler(this.toolStripButtonSalir_Click);
            // 
            // ultraPanel2
            // 
            appearance14.BackColor = System.Drawing.Color.Silver;
            appearance14.BackColor2 = System.Drawing.Color.CornflowerBlue;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop20;
            appearance14.BackHatchStyle = Infragistics.Win.BackHatchStyle.DiagonalCross;
            this.ultraPanel2.Appearance = appearance14;
            // 
            // ultraPanel2.ClientArea
            // 
            this.ultraPanel2.ClientArea.Controls.Add(this.ultraGroupBox2);
            this.ultraPanel2.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.ultraPanel2.ClientArea.Controls.Add(this.grpFechas);
            this.ultraPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraPanel2.Location = new System.Drawing.Point(0, 45);
            this.ultraPanel2.Name = "ultraPanel2";
            this.ultraPanel2.Size = new System.Drawing.Size(896, 102);
            this.ultraPanel2.TabIndex = 13;
            // 
            // ultraGroupBox2
            // 
            appearance15.BackColor = System.Drawing.Color.Transparent;
            this.ultraGroupBox2.Appearance = appearance15;
            this.ultraGroupBox2.Controls.Add(this.ckbDepartamento);
            this.ultraGroupBox2.Controls.Add(this.cmbDepartamento);
            this.ultraGroupBox2.Controls.Add(this.cmbPiso);
            this.ultraGroupBox2.Controls.Add(this.chkHab);
            this.ultraGroupBox2.Controls.Add(this.chkPiso);
            this.ultraGroupBox2.Controls.Add(this.cmbHabitacion);
            this.ultraGroupBox2.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox2.Location = new System.Drawing.Point(526, 3);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(365, 93);
            this.ultraGroupBox2.TabIndex = 2;
            this.ultraGroupBox2.Text = "Grupos";
            // 
            // ckbDepartamento
            // 
            this.ckbDepartamento.AutoSize = true;
            this.ckbDepartamento.Location = new System.Drawing.Point(10, 73);
            this.ckbDepartamento.Name = "ckbDepartamento";
            this.ckbDepartamento.Size = new System.Drawing.Size(93, 17);
            this.ckbDepartamento.TabIndex = 9;
            this.ckbDepartamento.Text = "Departamento";
            this.ckbDepartamento.UseVisualStyleBackColor = true;
            this.ckbDepartamento.Visible = false;
            // 
            // cmbDepartamento
            // 
            this.cmbDepartamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartamento.Enabled = false;
            this.cmbDepartamento.FormattingEnabled = true;
            this.cmbDepartamento.Items.AddRange(new object[] {
            "ANAMNESIS",
            "EMERGENCIA",
            "EPICRISIS",
            "EVOLUCION",
            "IMAGENOLOGIA",
            "INTERCONSULTA",
            "LABORATORIO",
            "PROTOCOLO"});
            this.cmbDepartamento.Location = new System.Drawing.Point(106, 68);
            this.cmbDepartamento.Name = "cmbDepartamento";
            this.cmbDepartamento.Size = new System.Drawing.Size(248, 21);
            this.cmbDepartamento.TabIndex = 8;
            this.cmbDepartamento.Visible = false;
            // 
            // cmbPiso
            // 
            this.cmbPiso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPiso.Enabled = false;
            this.cmbPiso.FormattingEnabled = true;
            this.cmbPiso.Location = new System.Drawing.Point(92, 26);
            this.cmbPiso.Name = "cmbPiso";
            this.cmbPiso.Size = new System.Drawing.Size(262, 21);
            this.cmbPiso.TabIndex = 5;
            // 
            // chkHab
            // 
            this.chkHab.AutoSize = true;
            this.chkHab.Location = new System.Drawing.Point(10, 50);
            this.chkHab.Name = "chkHab";
            this.chkHab.Size = new System.Drawing.Size(77, 17);
            this.chkHab.TabIndex = 4;
            this.chkHab.Text = "Habitación";
            this.chkHab.UseVisualStyleBackColor = true;
            // 
            // chkPiso
            // 
            this.chkPiso.AutoSize = true;
            this.chkPiso.Location = new System.Drawing.Point(10, 29);
            this.chkPiso.Name = "chkPiso";
            this.chkPiso.Size = new System.Drawing.Size(46, 17);
            this.chkPiso.TabIndex = 4;
            this.chkPiso.Text = "Piso";
            this.chkPiso.UseVisualStyleBackColor = true;
            // 
            // cmbHabitacion
            // 
            this.cmbHabitacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHabitacion.Enabled = false;
            this.cmbHabitacion.FormattingEnabled = true;
            this.cmbHabitacion.Location = new System.Drawing.Point(92, 47);
            this.cmbHabitacion.Name = "cmbHabitacion";
            this.cmbHabitacion.Size = new System.Drawing.Size(262, 21);
            this.cmbHabitacion.TabIndex = 2;
            // 
            // ultraGroupBox1
            // 
            appearance18.BackColor = System.Drawing.Color.Transparent;
            this.ultraGroupBox1.Appearance = appearance18;
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.chkHC);
            this.ultraGroupBox1.Controls.Add(this.ayudaPacientes);
            this.ultraGroupBox1.Controls.Add(this.txt_historiaclinica);
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox1.Location = new System.Drawing.Point(297, 3);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(220, 93);
            this.ultraGroupBox1.TabIndex = 1;
            this.ultraGroupBox1.Text = "Paciente";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 25);
            this.label2.TabIndex = 273;
            this.label2.Text = "[Escriba el numero de historia clinica o presione F1 para buscar por nombre.]";
            // 
            // chkHC
            // 
            this.chkHC.AutoSize = true;
            this.chkHC.Location = new System.Drawing.Point(12, 37);
            this.chkHC.Name = "chkHC";
            this.chkHC.Size = new System.Drawing.Size(90, 17);
            this.chkHC.TabIndex = 272;
            this.chkHC.Text = "Nro. Historia :";
            this.chkHC.UseVisualStyleBackColor = true;
            this.chkHC.CheckedChanged += new System.EventHandler(this.chkHC_CheckedChanged);
            // 
            // ayudaPacientes
            // 
            appearance19.ForeColor = System.Drawing.Color.Navy;
            appearance19.TextHAlignAsString = "Center";
            appearance19.TextVAlignAsString = "Middle";
            this.ayudaPacientes.Appearance = appearance19;
            this.ayudaPacientes.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            this.ayudaPacientes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaPacientes.Location = new System.Drawing.Point(177, 32);
            this.ayudaPacientes.Name = "ayudaPacientes";
            this.ayudaPacientes.Size = new System.Drawing.Size(30, 21);
            this.ayudaPacientes.TabIndex = 271;
            this.ayudaPacientes.TabStop = false;
            this.ayudaPacientes.Text = "F1";
            this.ayudaPacientes.Visible = false;
            this.ayudaPacientes.Click += new System.EventHandler(this.ayudaPacientes_Click);
            // 
            // txt_historiaclinica
            // 
            this.txt_historiaclinica.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_historiaclinica.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_historiaclinica.Enabled = false;
            this.txt_historiaclinica.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_historiaclinica.Location = new System.Drawing.Point(104, 31);
            this.txt_historiaclinica.MaxLength = 6;
            this.txt_historiaclinica.Name = "txt_historiaclinica";
            this.txt_historiaclinica.Size = new System.Drawing.Size(67, 24);
            this.txt_historiaclinica.TabIndex = 269;
            this.txt_historiaclinica.Tag = "false";
            this.txt_historiaclinica.Text = "0";
            this.txt_historiaclinica.Enter += new System.EventHandler(this.txt_historiaclinica_Enter);
            this.txt_historiaclinica.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_historiaclinica_KeyPress);
            this.txt_historiaclinica.Leave += new System.EventHandler(this.txt_historiaclinica_Leave);
            // 
            // grpFechas
            // 
            appearance16.BackColor = System.Drawing.Color.Transparent;
            this.grpFechas.Appearance = appearance16;
            this.grpFechas.Controls.Add(this.chkAlta);
            this.grpFechas.Controls.Add(this.chkIngreso);
            this.grpFechas.Controls.Add(this.label3);
            this.grpFechas.Controls.Add(this.lblHasta);
            this.grpFechas.Controls.Add(this.dtphasta);
            this.grpFechas.Controls.Add(this.dtpdesde);
            this.grpFechas.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.grpFechas.Location = new System.Drawing.Point(6, 3);
            this.grpFechas.Name = "grpFechas";
            this.grpFechas.Size = new System.Drawing.Size(283, 93);
            this.grpFechas.TabIndex = 0;
            this.grpFechas.Text = "Filtro por Fechas:";
            // 
            // chkAlta
            // 
            this.chkAlta.AutoSize = true;
            this.chkAlta.Location = new System.Drawing.Point(11, 51);
            this.chkAlta.Name = "chkAlta";
            this.chkAlta.Size = new System.Drawing.Size(77, 17);
            this.chkAlta.TabIndex = 10;
            this.chkAlta.Text = "Fecha Alta";
            this.chkAlta.UseVisualStyleBackColor = true;
            // 
            // chkIngreso
            // 
            this.chkIngreso.AutoSize = true;
            this.chkIngreso.Checked = true;
            this.chkIngreso.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIngreso.Location = new System.Drawing.Point(11, 26);
            this.chkIngreso.Name = "chkIngreso";
            this.chkIngreso.Size = new System.Drawing.Size(94, 17);
            this.chkIngreso.TabIndex = 8;
            this.chkIngreso.Text = "Fecha Ingreso";
            this.chkIngreso.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Desde:";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(136, 63);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 6;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtphasta
            // 
            this.dtphasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtphasta.Location = new System.Drawing.Point(183, 59);
            this.dtphasta.Name = "dtphasta";
            this.dtphasta.Size = new System.Drawing.Size(87, 20);
            this.dtphasta.TabIndex = 4;
            // 
            // dtpdesde
            // 
            this.dtpdesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdesde.Location = new System.Drawing.Point(183, 30);
            this.dtpdesde.Name = "dtpdesde";
            this.dtpdesde.Size = new System.Drawing.Size(87, 20);
            this.dtpdesde.TabIndex = 3;
            this.dtpdesde.Value = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            // 
            // ultraGridAuditoria
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridAuditoria.DisplayLayout.Appearance = appearance1;
            this.ultraGridAuditoria.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridAuditoria.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridAuditoria.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridAuditoria.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ultraGridAuditoria.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridAuditoria.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ultraGridAuditoria.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridAuditoria.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridAuditoria.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridAuditoria.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.ultraGridAuditoria.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGridAuditoria.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridAuditoria.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridAuditoria.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridAuditoria.DisplayLayout.Override.CellAppearance = appearance8;
            this.ultraGridAuditoria.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridAuditoria.DisplayLayout.Override.CellPadding = 0;
            this.ultraGridAuditoria.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.EntireColumn;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridAuditoria.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.ultraGridAuditoria.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ultraGridAuditoria.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridAuditoria.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridAuditoria.DisplayLayout.Override.RowAppearance = appearance11;
            this.ultraGridAuditoria.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridAuditoria.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.ultraGridAuditoria.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridAuditoria.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridAuditoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridAuditoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraGridAuditoria.Location = new System.Drawing.Point(0, 147);
            this.ultraGridAuditoria.Name = "ultraGridAuditoria";
            this.ultraGridAuditoria.Size = new System.Drawing.Size(896, 303);
            this.ultraGridAuditoria.TabIndex = 78;
            this.ultraGridAuditoria.Text = "ultraGrid1";
            this.ultraGridAuditoria.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridAuditoria_InitializeLayout);
            // 
            // nuevoToolStripButton
            // 
            this.nuevoToolStripButton.Name = "nuevoToolStripButton";
            this.nuevoToolStripButton.Size = new System.Drawing.Size(23, 23);
            // 
            // frmExploradorAuditoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 450);
            this.Controls.Add(this.ultraGridAuditoria);
            this.Controls.Add(this.ultraPanel2);
            this.Controls.Add(this.tools);
            this.Name = "frmExploradorAuditoria";
            this.Text = "frmExploradorAuditoria";
            this.Load += new System.EventHandler(this.frmExploradorAuditoria_Load);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.ultraPanel2.ClientArea.ResumeLayout(false);
            this.ultraPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).EndInit();
            this.grpFechas.ResumeLayout(false);
            this.grpFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridAuditoria)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSalir;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private Infragistics.Win.Misc.UltraPanel ultraPanel2;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.CheckBox ckbDepartamento;
        private System.Windows.Forms.ComboBox cmbDepartamento;
        private System.Windows.Forms.ComboBox cmbPiso;
        private System.Windows.Forms.CheckBox chkHab;
        private System.Windows.Forms.CheckBox chkPiso;
        private System.Windows.Forms.ComboBox cmbHabitacion;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkHC;
        private Infragistics.Win.Misc.UltraButton ayudaPacientes;
        private System.Windows.Forms.TextBox txt_historiaclinica;
        private Infragistics.Win.Misc.UltraGroupBox grpFechas;
        private System.Windows.Forms.CheckBox chkAlta;
        private System.Windows.Forms.CheckBox chkIngreso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtphasta;
        private System.Windows.Forms.DateTimePicker dtpdesde;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridAuditoria;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.ToolStripButton nuevoToolStripButton;
    }
}