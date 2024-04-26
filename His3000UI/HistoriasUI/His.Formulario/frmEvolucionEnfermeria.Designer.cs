namespace His.Formulario
{
    partial class frmEvolucionEnfermeria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEvolucionEnfermeria));
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gridNotasEvolucion = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.grpDatos = new System.Windows.Forms.GroupBox();
            this.txtEVD = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.txtNota = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpInformacion = new System.Windows.Forms.GroupBox();
            this.txtIngreso = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtHabitacion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSexo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMedico = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEdad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAteCodigo = new System.Windows.Forms.TextBox();
            this.txtHC = new System.Windows.Forms.TextBox();
            this.txtPaciente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tools.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNotasEvolucion)).BeginInit();
            this.grpDatos.SuspendLayout();
            this.grpInformacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnModificar,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tools.Size = new System.Drawing.Size(900, 45);
            this.tools.TabIndex = 38;
            this.tools.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.AutoSize = false;
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 42);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.ToolTipText = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.AutoSize = false;
            this.btnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(42, 42);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.ToolTipText = "Modificar";
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.AutoSize = false;
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = global::His.Formulario.Properties.Resources.HIS_IMPRIMIR;
            this.btnImprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(42, 42);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.ToolTipText = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // btnSalir
            // 
            this.btnSalir.AutoSize = false;
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = global::His.Formulario.Properties.Resources.HIS_SALIR;
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(42, 42);
            this.btnSalir.Text = "Salir";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gridNotasEvolucion);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 304);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(900, 146);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Registro";
            // 
            // gridNotasEvolucion
            // 
            appearance21.BackColor = System.Drawing.Color.LightGray;
            appearance21.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.gridNotasEvolucion.DisplayLayout.Appearance = appearance21;
            this.gridNotasEvolucion.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            this.gridNotasEvolucion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance22.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance22.BorderColor = System.Drawing.SystemColors.Window;
            this.gridNotasEvolucion.DisplayLayout.GroupByBox.Appearance = appearance22;
            appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridNotasEvolucion.DisplayLayout.GroupByBox.BandLabelAppearance = appearance23;
            this.gridNotasEvolucion.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance24.BackColor2 = System.Drawing.SystemColors.Control;
            appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridNotasEvolucion.DisplayLayout.GroupByBox.PromptAppearance = appearance24;
            this.gridNotasEvolucion.DisplayLayout.MaxColScrollRegions = 1;
            this.gridNotasEvolucion.DisplayLayout.MaxRowScrollRegions = 1;
            appearance25.BackColor = System.Drawing.SystemColors.Window;
            appearance25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridNotasEvolucion.DisplayLayout.Override.ActiveCellAppearance = appearance25;
            appearance26.BackColor = System.Drawing.SystemColors.Highlight;
            appearance26.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridNotasEvolucion.DisplayLayout.Override.ActiveRowAppearance = appearance26;
            this.gridNotasEvolucion.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.gridNotasEvolucion.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.gridNotasEvolucion.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.gridNotasEvolucion.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.gridNotasEvolucion.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance27.BackColor = System.Drawing.SystemColors.Window;
            this.gridNotasEvolucion.DisplayLayout.Override.CardAreaAppearance = appearance27;
            appearance28.BorderColor = System.Drawing.Color.Silver;
            appearance28.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.gridNotasEvolucion.DisplayLayout.Override.CellAppearance = appearance28;
            this.gridNotasEvolucion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.gridNotasEvolucion.DisplayLayout.Override.CellPadding = 0;
            appearance29.BackColor = System.Drawing.SystemColors.Control;
            appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance29.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance29.BorderColor = System.Drawing.SystemColors.Window;
            this.gridNotasEvolucion.DisplayLayout.Override.GroupByRowAppearance = appearance29;
            appearance30.TextHAlignAsString = "Left";
            this.gridNotasEvolucion.DisplayLayout.Override.HeaderAppearance = appearance30;
            this.gridNotasEvolucion.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.gridNotasEvolucion.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            appearance31.BorderColor = System.Drawing.Color.Silver;
            this.gridNotasEvolucion.DisplayLayout.Override.RowAppearance = appearance31;
            this.gridNotasEvolucion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.gridNotasEvolucion.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            appearance32.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gridNotasEvolucion.DisplayLayout.Override.TemplateAddRowAppearance = appearance32;
            this.gridNotasEvolucion.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.gridNotasEvolucion.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.gridNotasEvolucion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridNotasEvolucion.Location = new System.Drawing.Point(3, 16);
            this.gridNotasEvolucion.Name = "gridNotasEvolucion";
            this.gridNotasEvolucion.Size = new System.Drawing.Size(894, 127);
            this.gridNotasEvolucion.TabIndex = 5;
            this.gridNotasEvolucion.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridNotasEvolucion_InitializeLayout);
            // 
            // grpDatos
            // 
            this.grpDatos.Controls.Add(this.txtEVD);
            this.grpDatos.Controls.Add(this.btnGuardar);
            this.grpDatos.Controls.Add(this.btnCancelar);
            this.grpDatos.Controls.Add(this.dtpFecha);
            this.grpDatos.Controls.Add(this.txtNota);
            this.grpDatos.Controls.Add(this.label3);
            this.grpDatos.Controls.Add(this.label2);
            this.grpDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDatos.Location = new System.Drawing.Point(0, 132);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(900, 172);
            this.grpDatos.TabIndex = 43;
            this.grpDatos.TabStop = false;
            this.grpDatos.Visible = false;
            // 
            // txtEVD
            // 
            this.txtEVD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEVD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.txtEVD.Location = new System.Drawing.Point(228, 18);
            this.txtEVD.Margin = new System.Windows.Forms.Padding(2);
            this.txtEVD.Name = "txtEVD";
            this.txtEVD.ReadOnly = true;
            this.txtEVD.Size = new System.Drawing.Size(79, 13);
            this.txtEVD.TabIndex = 6;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::His.Formulario.Properties.Resources.HIS_GUARDAR;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(482, 11);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 24);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::His.Formulario.Properties.Resources.HIS_CANCELAR;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(576, 11);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(93, 24);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(67, 16);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(154, 20);
            this.dtpFecha.TabIndex = 3;
            this.dtpFecha.Value = new System.DateTime(2021, 2, 25, 18, 41, 0, 0);
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // txtNota
            // 
            this.txtNota.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNota.Location = new System.Drawing.Point(65, 39);
            this.txtNota.Margin = new System.Windows.Forms.Padding(2);
            this.txtNota.MaxLength = 1000;
            this.txtNota.Multiline = true;
            this.txtNota.Name = "txtNota";
            this.txtNota.Size = new System.Drawing.Size(827, 127);
            this.txtNota.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nota :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fecha :";
            // 
            // grpInformacion
            // 
            this.grpInformacion.Controls.Add(this.txtIngreso);
            this.grpInformacion.Controls.Add(this.label11);
            this.grpInformacion.Controls.Add(this.txtHabitacion);
            this.grpInformacion.Controls.Add(this.label10);
            this.grpInformacion.Controls.Add(this.txtSexo);
            this.grpInformacion.Controls.Add(this.label9);
            this.grpInformacion.Controls.Add(this.txtDNI);
            this.grpInformacion.Controls.Add(this.label8);
            this.grpInformacion.Controls.Add(this.txtMedico);
            this.grpInformacion.Controls.Add(this.label7);
            this.grpInformacion.Controls.Add(this.txtEdad);
            this.grpInformacion.Controls.Add(this.label6);
            this.grpInformacion.Controls.Add(this.txtAteCodigo);
            this.grpInformacion.Controls.Add(this.txtHC);
            this.grpInformacion.Controls.Add(this.txtPaciente);
            this.grpInformacion.Controls.Add(this.label5);
            this.grpInformacion.Controls.Add(this.label4);
            this.grpInformacion.Controls.Add(this.label1);
            this.grpInformacion.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInformacion.Location = new System.Drawing.Point(0, 45);
            this.grpInformacion.Name = "grpInformacion";
            this.grpInformacion.Size = new System.Drawing.Size(900, 87);
            this.grpInformacion.TabIndex = 42;
            this.grpInformacion.TabStop = false;
            this.grpInformacion.Text = "Informacion de la atención";
            // 
            // txtIngreso
            // 
            this.txtIngreso.Location = new System.Drawing.Point(543, 60);
            this.txtIngreso.Margin = new System.Windows.Forms.Padding(2);
            this.txtIngreso.Name = "txtIngreso";
            this.txtIngreso.ReadOnly = true;
            this.txtIngreso.Size = new System.Drawing.Size(102, 20);
            this.txtIngreso.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(466, 63);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Fecha Ingreso:";
            // 
            // txtHabitacion
            // 
            this.txtHabitacion.Location = new System.Drawing.Point(543, 36);
            this.txtHabitacion.Margin = new System.Windows.Forms.Padding(2);
            this.txtHabitacion.Name = "txtHabitacion";
            this.txtHabitacion.ReadOnly = true;
            this.txtHabitacion.Size = new System.Drawing.Size(102, 20);
            this.txtHabitacion.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(478, 39);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Habitación:";
            // 
            // txtSexo
            // 
            this.txtSexo.Location = new System.Drawing.Point(691, 13);
            this.txtSexo.Margin = new System.Windows.Forms.Padding(2);
            this.txtSexo.Name = "txtSexo";
            this.txtSexo.ReadOnly = true;
            this.txtSexo.Size = new System.Drawing.Size(31, 20);
            this.txtSexo.TabIndex = 17;
            this.txtSexo.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(652, 15);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Sexo:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(543, 13);
            this.txtDNI.Margin = new System.Windows.Forms.Padding(2);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.ReadOnly = true;
            this.txtDNI.Size = new System.Drawing.Size(102, 20);
            this.txtDNI.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(466, 17);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Identificación:";
            // 
            // txtMedico
            // 
            this.txtMedico.Location = new System.Drawing.Point(133, 60);
            this.txtMedico.Margin = new System.Windows.Forms.Padding(2);
            this.txtMedico.Name = "txtMedico";
            this.txtMedico.ReadOnly = true;
            this.txtMedico.Size = new System.Drawing.Size(325, 20);
            this.txtMedico.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 63);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Médico Tratante:";
            // 
            // txtEdad
            // 
            this.txtEdad.Location = new System.Drawing.Point(401, 36);
            this.txtEdad.Margin = new System.Windows.Forms.Padding(2);
            this.txtEdad.Name = "txtEdad";
            this.txtEdad.ReadOnly = true;
            this.txtEdad.Size = new System.Drawing.Size(57, 20);
            this.txtEdad.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 38);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Edad:";
            // 
            // txtAteCodigo
            // 
            this.txtAteCodigo.Location = new System.Drawing.Point(282, 36);
            this.txtAteCodigo.Margin = new System.Windows.Forms.Padding(2);
            this.txtAteCodigo.Name = "txtAteCodigo";
            this.txtAteCodigo.ReadOnly = true;
            this.txtAteCodigo.Size = new System.Drawing.Size(73, 20);
            this.txtAteCodigo.TabIndex = 9;
            // 
            // txtHC
            // 
            this.txtHC.Location = new System.Drawing.Point(133, 36);
            this.txtHC.Margin = new System.Windows.Forms.Padding(2);
            this.txtHC.Name = "txtHC";
            this.txtHC.ReadOnly = true;
            this.txtHC.Size = new System.Drawing.Size(65, 20);
            this.txtHC.TabIndex = 8;
            // 
            // txtPaciente
            // 
            this.txtPaciente.Location = new System.Drawing.Point(133, 13);
            this.txtPaciente.Margin = new System.Windows.Forms.Padding(2);
            this.txtPaciente.Name = "txtPaciente";
            this.txtPaciente.ReadOnly = true;
            this.txtPaciente.Size = new System.Drawing.Size(325, 20);
            this.txtPaciente.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(203, 39);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Atencion Nro.:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Historia Clínica. :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre de paciente :";
            // 
            // frmEvolucionEnfermeria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.grpInformacion);
            this.Controls.Add(this.tools);
            this.Name = "frmEvolucionEnfermeria";
            this.ShowIcon = false;
            this.Text = "Evolución de enfermería";
            this.Load += new System.EventHandler(this.frmEvolucionEnfermeria_Load);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridNotasEvolucion)).EndInit();
            this.grpDatos.ResumeLayout(false);
            this.grpDatos.PerformLayout();
            this.grpInformacion.ResumeLayout(false);
            this.grpInformacion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.GroupBox groupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridNotasEvolucion;
        private System.Windows.Forms.GroupBox grpDatos;
        private System.Windows.Forms.TextBox txtEVD;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.TextBox txtNota;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpInformacion;
        private System.Windows.Forms.TextBox txtAteCodigo;
        private System.Windows.Forms.TextBox txtHC;
        private System.Windows.Forms.TextBox txtPaciente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSexo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMedico;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHabitacion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIngreso;
        private System.Windows.Forms.Label label11;
    }
}