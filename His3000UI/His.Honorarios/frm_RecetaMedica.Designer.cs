namespace His.Honorarios
{
    partial class frm_RecetaMedica
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_RecetaMedica));
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cie10texto = new System.Windows.Forms.TextBox();
            this.btnañadir = new Infragistics.Win.Misc.UltraButton();
            this.TablaDiagnostico = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.TablaMedicamentos = new System.Windows.Forms.DataGridView();
            this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
            this.txtindicaciones = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ultraGroupBoxPaciente = new Infragistics.Win.Misc.UltraGroupBox();
            this.ayudaPacientes = new Infragistics.Win.Misc.UltraButton();
            this.txt_apellido2 = new System.Windows.Forms.TextBox();
            this.txt_historiaclinica = new System.Windows.Forms.TextBox();
            this.txt_apellido1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_nombre2 = new System.Windows.Forms.TextBox();
            this.txt_nombre1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaDiagnostico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaMedicamentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPaciente)).BeginInit();
            this.ultraGroupBoxPaciente.SuspendLayout();
            this.tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Near;
            this.ultraGroupBox1.Controls.Add(this.cie10texto);
            this.ultraGroupBox1.Controls.Add(this.btnañadir);
            this.ultraGroupBox1.Controls.Add(this.TablaDiagnostico);
            this.ultraGroupBox1.Controls.Add(this.label6);
            this.ultraGroupBox1.Controls.Add(this.TablaMedicamentos);
            this.ultraGroupBox1.Controls.Add(this.ultraButton1);
            this.ultraGroupBox1.Controls.Add(this.txtindicaciones);
            this.ultraGroupBox1.Controls.Add(this.label4);
            this.ultraGroupBox1.Controls.Add(this.label5);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.ForeColor = System.Drawing.Color.DimGray;
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 102);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(910, 466);
            this.ultraGroupBox1.TabIndex = 45;
            this.ultraGroupBox1.Text = "Indicaciones Paciente";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // cie10texto
            // 
            this.cie10texto.BackColor = System.Drawing.SystemColors.Window;
            this.cie10texto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cie10texto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cie10texto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cie10texto.Location = new System.Drawing.Point(10, 92);
            this.cie10texto.MaxLength = 20;
            this.cie10texto.Multiline = true;
            this.cie10texto.Name = "cie10texto";
            this.cie10texto.ReadOnly = true;
            this.cie10texto.Size = new System.Drawing.Size(86, 20);
            this.cie10texto.TabIndex = 280;
            this.cie10texto.Visible = false;
            // 
            // btnañadir
            // 
            appearance1.ForeColor = System.Drawing.Color.Navy;
            appearance1.TextHAlignAsString = "Center";
            appearance1.TextVAlignAsString = "Middle";
            this.btnañadir.Appearance = appearance1;
            this.btnañadir.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            this.btnañadir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnañadir.Location = new System.Drawing.Point(810, 91);
            this.btnañadir.Name = "btnañadir";
            this.btnañadir.Size = new System.Drawing.Size(78, 21);
            this.btnañadir.TabIndex = 279;
            this.btnañadir.TabStop = false;
            this.btnañadir.Text = "AGREGAR";
            this.btnañadir.Click += new System.EventHandler(this.btnañadir_Click);
            // 
            // TablaDiagnostico
            // 
            this.TablaDiagnostico.AllowUserToAddRows = false;
            this.TablaDiagnostico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaDiagnostico.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablaDiagnostico.BackgroundColor = System.Drawing.Color.White;
            this.TablaDiagnostico.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaDiagnostico.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TablaDiagnostico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaDiagnostico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.Descripcion});
            this.TablaDiagnostico.EnableHeadersVisualStyles = false;
            this.TablaDiagnostico.Location = new System.Drawing.Point(104, 32);
            this.TablaDiagnostico.Name = "TablaDiagnostico";
            this.TablaDiagnostico.ReadOnly = true;
            this.TablaDiagnostico.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.TablaDiagnostico.Size = new System.Drawing.Size(679, 126);
            this.TablaDiagnostico.TabIndex = 272;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 64;
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 87;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 16);
            this.label6.TabIndex = 271;
            this.label6.Text = "Diagnóstico:";
            // 
            // TablaMedicamentos
            // 
            this.TablaMedicamentos.AllowUserToAddRows = false;
            this.TablaMedicamentos.AllowUserToDeleteRows = false;
            this.TablaMedicamentos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaMedicamentos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablaMedicamentos.BackgroundColor = System.Drawing.Color.White;
            this.TablaMedicamentos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaMedicamentos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TablaMedicamentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaMedicamentos.EnableHeadersVisualStyles = false;
            this.TablaMedicamentos.Location = new System.Drawing.Point(104, 176);
            this.TablaMedicamentos.Name = "TablaMedicamentos";
            this.TablaMedicamentos.ReadOnly = true;
            this.TablaMedicamentos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.TablaMedicamentos.Size = new System.Drawing.Size(679, 163);
            this.TablaMedicamentos.TabIndex = 269;
            // 
            // ultraButton1
            // 
            appearance2.ForeColor = System.Drawing.Color.Navy;
            appearance2.TextHAlignAsString = "Center";
            appearance2.TextVAlignAsString = "Middle";
            this.ultraButton1.Appearance = appearance2;
            this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            this.ultraButton1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraButton1.Location = new System.Drawing.Point(810, 244);
            this.ultraButton1.Name = "ultraButton1";
            this.ultraButton1.Size = new System.Drawing.Size(78, 21);
            this.ultraButton1.TabIndex = 268;
            this.ultraButton1.TabStop = false;
            this.ultraButton1.Text = "AGREGAR";
            this.ultraButton1.Click += new System.EventHandler(this.ultraButton1_Click);
            // 
            // txtindicaciones
            // 
            this.txtindicaciones.BackColor = System.Drawing.SystemColors.Window;
            this.txtindicaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtindicaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtindicaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtindicaciones.Location = new System.Drawing.Point(106, 356);
            this.txtindicaciones.MaxLength = 20000;
            this.txtindicaciones.Multiline = true;
            this.txtindicaciones.Name = "txtindicaciones";
            this.txtindicaciones.Size = new System.Drawing.Size(677, 89);
            this.txtindicaciones.TabIndex = 1;
            this.txtindicaciones.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 65;
            this.label4.Text = "Indicaciones:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 358);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Indicaciones:";
            this.label5.Visible = false;
            // 
            // ultraGroupBoxPaciente
            // 
            this.ultraGroupBoxPaciente.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Near;
            this.ultraGroupBoxPaciente.Controls.Add(this.ayudaPacientes);
            this.ultraGroupBoxPaciente.Controls.Add(this.txt_apellido2);
            this.ultraGroupBoxPaciente.Controls.Add(this.txt_historiaclinica);
            this.ultraGroupBoxPaciente.Controls.Add(this.txt_apellido1);
            this.ultraGroupBoxPaciente.Controls.Add(this.label1);
            this.ultraGroupBoxPaciente.Controls.Add(this.label2);
            this.ultraGroupBoxPaciente.Controls.Add(this.txt_nombre2);
            this.ultraGroupBoxPaciente.Controls.Add(this.txt_nombre1);
            this.ultraGroupBoxPaciente.Controls.Add(this.label3);
            this.ultraGroupBoxPaciente.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBoxPaciente.ForeColor = System.Drawing.Color.DimGray;
            this.ultraGroupBoxPaciente.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBoxPaciente.Location = new System.Drawing.Point(0, 45);
            this.ultraGroupBoxPaciente.Name = "ultraGroupBoxPaciente";
            this.ultraGroupBoxPaciente.Size = new System.Drawing.Size(910, 57);
            this.ultraGroupBoxPaciente.TabIndex = 44;
            this.ultraGroupBoxPaciente.Text = "Datos de Paciente";
            this.ultraGroupBoxPaciente.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // ayudaPacientes
            // 
            appearance5.ForeColor = System.Drawing.Color.Navy;
            appearance5.TextHAlignAsString = "Center";
            appearance5.TextVAlignAsString = "Middle";
            this.ayudaPacientes.Appearance = appearance5;
            this.ayudaPacientes.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            this.ayudaPacientes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaPacientes.Location = new System.Drawing.Point(86, 25);
            this.ayudaPacientes.Name = "ayudaPacientes";
            this.ayudaPacientes.Size = new System.Drawing.Size(30, 21);
            this.ayudaPacientes.TabIndex = 268;
            this.ayudaPacientes.TabStop = false;
            this.ayudaPacientes.Text = "F1";
            this.ayudaPacientes.Click += new System.EventHandler(this.ayudaPacientes_Click);
            // 
            // txt_apellido2
            // 
            this.txt_apellido2.BackColor = System.Drawing.SystemColors.Window;
            this.txt_apellido2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_apellido2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_apellido2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_apellido2.Location = new System.Drawing.Point(439, 24);
            this.txt_apellido2.MaxLength = 20;
            this.txt_apellido2.Name = "txt_apellido2";
            this.txt_apellido2.ReadOnly = true;
            this.txt_apellido2.Size = new System.Drawing.Size(123, 22);
            this.txt_apellido2.TabIndex = 2;
            // 
            // txt_historiaclinica
            // 
            this.txt_historiaclinica.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_historiaclinica.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_historiaclinica.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_historiaclinica.Location = new System.Drawing.Point(119, 24);
            this.txt_historiaclinica.MaxLength = 8;
            this.txt_historiaclinica.Name = "txt_historiaclinica";
            this.txt_historiaclinica.ReadOnly = true;
            this.txt_historiaclinica.Size = new System.Drawing.Size(85, 24);
            this.txt_historiaclinica.TabIndex = 0;
            this.txt_historiaclinica.Tag = "false";
            this.txt_historiaclinica.TextChanged += new System.EventHandler(this.txt_historiaclinica_TextChanged);
            // 
            // txt_apellido1
            // 
            this.txt_apellido1.BackColor = System.Drawing.SystemColors.Window;
            this.txt_apellido1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_apellido1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_apellido1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_apellido1.Location = new System.Drawing.Point(320, 24);
            this.txt_apellido1.MaxLength = 20;
            this.txt_apellido1.Name = "txt_apellido1";
            this.txt_apellido1.ReadOnly = true;
            this.txt_apellido1.Size = new System.Drawing.Size(119, 22);
            this.txt_apellido1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 65;
            this.label1.Text = "N° Historia:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(244, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Apellidos*:";
            // 
            // txt_nombre2
            // 
            this.txt_nombre2.BackColor = System.Drawing.SystemColors.Window;
            this.txt_nombre2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_nombre2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nombre2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nombre2.Location = new System.Drawing.Point(760, 24);
            this.txt_nombre2.MaxLength = 20;
            this.txt_nombre2.Name = "txt_nombre2";
            this.txt_nombre2.ReadOnly = true;
            this.txt_nombre2.Size = new System.Drawing.Size(128, 22);
            this.txt_nombre2.TabIndex = 4;
            // 
            // txt_nombre1
            // 
            this.txt_nombre1.BackColor = System.Drawing.SystemColors.Window;
            this.txt_nombre1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_nombre1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nombre1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_nombre1.Location = new System.Drawing.Point(636, 24);
            this.txt_nombre1.MaxLength = 20;
            this.txt_nombre1.Name = "txt_nombre1";
            this.txt_nombre1.ReadOnly = true;
            this.txt_nombre1.Size = new System.Drawing.Size(125, 22);
            this.txt_nombre1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(562, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nombres*:";
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImprimir,
            this.toolStripSeparator2,
            this.btnCancelar,
            this.btnSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(910, 45);
            this.tools.TabIndex = 43;
            this.tools.Text = "toolStrip1";
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
            this.btnImprimir.ToolTipText = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // btnSalir
            // 
            this.btnSalir.AutoSize = false;
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(42, 42);
            this.btnSalir.Text = "toolStripButton1";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Codigo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 339;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Descripción";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 678;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Medicamentos";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 863;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Medicamentos";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Cantidad";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 73;
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = false;
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = global::His.Honorarios.Properties.Resources.Button_Delete_01_25095;
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(42, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frm_RecetaMedica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 568);
            this.Controls.Add(this.ultraGroupBox1);
            this.Controls.Add(this.ultraGroupBoxPaciente);
            this.Controls.Add(this.tools);
            this.Name = "frm_RecetaMedica";
            this.Text = "frm_RecetaMedica";
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaDiagnostico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaMedicamentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPaciente)).EndInit();
            this.ultraGroupBoxPaciente.ResumeLayout(false);
            this.ultraGroupBoxPaciente.PerformLayout();
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.TextBox cie10texto;
        private Infragistics.Win.Misc.UltraButton btnañadir;
        private System.Windows.Forms.DataGridView TablaDiagnostico;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView TablaMedicamentos;
        private Infragistics.Win.Misc.UltraButton ultraButton1;
        private System.Windows.Forms.TextBox txtindicaciones;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBoxPaciente;
        private Infragistics.Win.Misc.UltraButton ayudaPacientes;
        private System.Windows.Forms.TextBox txt_apellido2;
        private System.Windows.Forms.TextBox txt_historiaclinica;
        private System.Windows.Forms.TextBox txt_apellido1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_nombre2;
        private System.Windows.Forms.TextBox txt_nombre1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.ToolStripButton btnCancelar;
    }
}