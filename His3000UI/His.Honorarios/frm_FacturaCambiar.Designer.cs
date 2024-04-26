namespace His.Honorarios
{
    partial class frm_FacturaCambiar
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
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraGroupBoxPaciente = new Infragistics.Win.Misc.UltraGroupBox();
            this.lblAnterior = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtcredito = new System.Windows.Forms.TextBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lbldoc = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtFacturaNueva = new System.Windows.Forms.TextBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.dtpFactura = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpAlta = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpIngreso = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHab = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAtencion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPaciente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ayudaPacientes = new Infragistics.Win.Misc.UltraButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.FacturasAnuladas = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPaciente)).BeginInit();
            this.ultraGroupBoxPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FacturasAnuladas)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnActualizar,
            this.btnGuardar,
            this.btnCancelar,
            this.toolStripSeparator1,
            this.btnImprimir,
            this.btnSalir});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(969, 37);
            this.menu.TabIndex = 1;
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
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Image = global::His.Honorarios.Properties.Resources.HIS_GUARDAR;
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(36, 34);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Image = global::His.Honorarios.Properties.Resources.Button_Delete_01_25095;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(36, 34);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.Black;
            this.btnImprimir.Image = global::His.Honorarios.Properties.Resources.HIS_IMPRIMIR;
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(36, 34);
            this.btnImprimir.Visible = false;
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Black;
            this.btnSalir.Image = global::His.Honorarios.Properties.Resources.HIS_SALIR;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(36, 34);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // ultraGroupBoxPaciente
            // 
            this.ultraGroupBoxPaciente.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center;
            this.ultraGroupBoxPaciente.Controls.Add(this.lblAnterior);
            this.ultraGroupBoxPaciente.Controls.Add(this.label12);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtcredito);
            this.ultraGroupBoxPaciente.Controls.Add(this.cmbEstado);
            this.ultraGroupBoxPaciente.Controls.Add(this.label15);
            this.ultraGroupBoxPaciente.Controls.Add(this.lbldoc);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtObservacion);
            this.ultraGroupBoxPaciente.Controls.Add(this.label13);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtFacturaNueva);
            this.ultraGroupBoxPaciente.Controls.Add(this.lblDocumento);
            this.ultraGroupBoxPaciente.Controls.Add(this.dtpFactura);
            this.ultraGroupBoxPaciente.Controls.Add(this.label11);
            this.ultraGroupBoxPaciente.Controls.Add(this.dtpAlta);
            this.ultraGroupBoxPaciente.Controls.Add(this.label10);
            this.ultraGroupBoxPaciente.Controls.Add(this.dtpIngreso);
            this.ultraGroupBoxPaciente.Controls.Add(this.label9);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtFactura);
            this.ultraGroupBoxPaciente.Controls.Add(this.label8);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtCedula);
            this.ultraGroupBoxPaciente.Controls.Add(this.label7);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtHab);
            this.ultraGroupBoxPaciente.Controls.Add(this.label6);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtTelefono);
            this.ultraGroupBoxPaciente.Controls.Add(this.label5);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtAtencion);
            this.ultraGroupBoxPaciente.Controls.Add(this.label4);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtHc);
            this.ultraGroupBoxPaciente.Controls.Add(this.label3);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtDireccion);
            this.ultraGroupBoxPaciente.Controls.Add(this.label2);
            this.ultraGroupBoxPaciente.Controls.Add(this.txtPaciente);
            this.ultraGroupBoxPaciente.Controls.Add(this.label1);
            this.ultraGroupBoxPaciente.Controls.Add(this.ayudaPacientes);
            this.ultraGroupBoxPaciente.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBoxPaciente.ForeColor = System.Drawing.Color.DimGray;
            this.ultraGroupBoxPaciente.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBoxPaciente.Location = new System.Drawing.Point(0, 37);
            this.ultraGroupBoxPaciente.Name = "ultraGroupBoxPaciente";
            this.ultraGroupBoxPaciente.Size = new System.Drawing.Size(969, 207);
            this.ultraGroupBoxPaciente.TabIndex = 3;
            this.ultraGroupBoxPaciente.Text = "Datos del Paciente";
            this.ultraGroupBoxPaciente.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
            // 
            // lblAnterior
            // 
            this.lblAnterior.AutoSize = true;
            this.lblAnterior.BackColor = System.Drawing.Color.Transparent;
            this.lblAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnterior.ForeColor = System.Drawing.Color.Black;
            this.lblAnterior.Location = new System.Drawing.Point(12, 123);
            this.lblAnterior.Name = "lblAnterior";
            this.lblAnterior.Size = new System.Drawing.Size(106, 13);
            this.lblAnterior.TabIndex = 300;
            this.lblAnterior.Text = "FAC. ANTERIOR:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(12, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 13);
            this.label12.TabIndex = 299;
            this.label12.Text = "N. CREDITO(*):";
            // 
            // txtcredito
            // 
            this.txtcredito.Location = new System.Drawing.Point(118, 183);
            this.txtcredito.MaxLength = 15;
            this.txtcredito.Name = "txtcredito";
            this.txtcredito.Size = new System.Drawing.Size(127, 20);
            this.txtcredito.TabIndex = 298;
            this.txtcredito.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ANULADA",
            "NOTA DE CREDITO"});
            this.cmbEstado.Location = new System.Drawing.Point(347, 120);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(132, 21);
            this.cmbEstado.TabIndex = 297;
            this.cmbEstado.SelectedIndexChanged += new System.EventHandler(this.cmbEstado_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(267, 123);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(74, 13);
            this.label15.TabIndex = 296;
            this.label15.Text = "ESTADO(*):";
            // 
            // lbldoc
            // 
            this.lbldoc.AutoSize = true;
            this.lbldoc.BackColor = System.Drawing.Color.Transparent;
            this.lbldoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldoc.ForeColor = System.Drawing.Color.Black;
            this.lbldoc.Location = new System.Drawing.Point(23, 165);
            this.lbldoc.Name = "lbldoc";
            this.lbldoc.Size = new System.Drawing.Size(70, 12);
            this.lbldoc.TabIndex = 295;
            this.lbldoc.Text = "(Nueva factura)";
            // 
            // txtObservacion
            // 
            this.txtObservacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacion.Location = new System.Drawing.Point(385, 149);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(317, 54);
            this.txtObservacion.TabIndex = 294;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(267, 152);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 13);
            this.label13.TabIndex = 293;
            this.label13.Text = "OBSERVACIÓN(*):";
            // 
            // txtFacturaNueva
            // 
            this.txtFacturaNueva.Location = new System.Drawing.Point(118, 149);
            this.txtFacturaNueva.MaxLength = 15;
            this.txtFacturaNueva.Name = "txtFacturaNueva";
            this.txtFacturaNueva.Size = new System.Drawing.Size(127, 20);
            this.txtFacturaNueva.TabIndex = 292;
            this.txtFacturaNueva.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFacturaNueva_KeyDown);
            this.txtFacturaNueva.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFacturaNueva_KeyPress);
            this.txtFacturaNueva.Leave += new System.EventHandler(this.txtFacturaNueva_Leave);
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.BackColor = System.Drawing.Color.Transparent;
            this.lblDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumento.ForeColor = System.Drawing.Color.Black;
            this.lblDocumento.Location = new System.Drawing.Point(12, 152);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(99, 13);
            this.lblDocumento.TabIndex = 291;
            this.lblDocumento.Text = "N° FACTURA(*):";
            // 
            // dtpFactura
            // 
            this.dtpFactura.CustomFormat = "dd/MM/yyyy HH:MM:ss";
            this.dtpFactura.Enabled = false;
            this.dtpFactura.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFactura.Location = new System.Drawing.Point(812, 179);
            this.dtpFactura.Name = "dtpFactura";
            this.dtpFactura.Size = new System.Drawing.Size(145, 20);
            this.dtpFactura.TabIndex = 290;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(735, 182);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 289;
            this.label11.Text = "F. FACTURA:";
            // 
            // dtpAlta
            // 
            this.dtpAlta.CustomFormat = "dd/MM/yyyy HH:MM:ss";
            this.dtpAlta.Enabled = false;
            this.dtpAlta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAlta.Location = new System.Drawing.Point(812, 149);
            this.dtpAlta.Name = "dtpAlta";
            this.dtpAlta.Size = new System.Drawing.Size(145, 20);
            this.dtpAlta.TabIndex = 288;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(735, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 287;
            this.label10.Text = "F. ALTA:";
            // 
            // dtpIngreso
            // 
            this.dtpIngreso.CustomFormat = "dd/MM/yyyy HH:MM:ss";
            this.dtpIngreso.Enabled = false;
            this.dtpIngreso.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIngreso.Location = new System.Drawing.Point(812, 120);
            this.dtpIngreso.Name = "dtpIngreso";
            this.dtpIngreso.Size = new System.Drawing.Size(145, 20);
            this.dtpIngreso.TabIndex = 286;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(735, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 285;
            this.label9.Text = "F. INGRESO:";
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(816, 28);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.ReadOnly = true;
            this.txtFactura.Size = new System.Drawing.Size(141, 20);
            this.txtFactura.TabIndex = 284;
            this.txtFactura.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(735, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 283;
            this.label8.Text = "N° FACTURA:";
            this.label8.Visible = false;
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(584, 28);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.ReadOnly = true;
            this.txtCedula.Size = new System.Drawing.Size(118, 20);
            this.txtCedula.TabIndex = 282;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(522, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 281;
            this.label7.Text = "CÉDULA: ";
            // 
            // txtHab
            // 
            this.txtHab.Location = new System.Drawing.Point(816, 88);
            this.txtHab.Name = "txtHab";
            this.txtHab.ReadOnly = true;
            this.txtHab.Size = new System.Drawing.Size(74, 20);
            this.txtHab.TabIndex = 280;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(735, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 279;
            this.label6.Text = "HABITACIÓN:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(595, 88);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.ReadOnly = true;
            this.txtTelefono.Size = new System.Drawing.Size(107, 20);
            this.txtTelefono.TabIndex = 278;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(522, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 277;
            this.label5.Text = "TELEFONO:";
            // 
            // txtAtencion
            // 
            this.txtAtencion.Location = new System.Drawing.Point(821, 58);
            this.txtAtencion.Name = "txtAtencion";
            this.txtAtencion.ReadOnly = true;
            this.txtAtencion.Size = new System.Drawing.Size(69, 20);
            this.txtAtencion.TabIndex = 276;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(735, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 275;
            this.label4.Text = "N° ATENCIÓN:";
            // 
            // txtHc
            // 
            this.txtHc.Location = new System.Drawing.Point(633, 58);
            this.txtHc.Name = "txtHc";
            this.txtHc.ReadOnly = true;
            this.txtHc.Size = new System.Drawing.Size(69, 20);
            this.txtHc.TabIndex = 274;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(522, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 273;
            this.label3.Text = "HISTORIA CLINICA:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(87, 58);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(392, 50);
            this.txtDireccion.TabIndex = 272;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 271;
            this.label2.Text = "DIRECCIÓN:";
            // 
            // txtPaciente
            // 
            this.txtPaciente.Location = new System.Drawing.Point(117, 28);
            this.txtPaciente.Name = "txtPaciente";
            this.txtPaciente.ReadOnly = true;
            this.txtPaciente.Size = new System.Drawing.Size(362, 20);
            this.txtPaciente.TabIndex = 270;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 269;
            this.label1.Text = "PACIENTE:";
            // 
            // ayudaPacientes
            // 
            appearance50.ForeColor = System.Drawing.Color.Navy;
            appearance50.TextHAlignAsString = "Center";
            appearance50.TextVAlignAsString = "Middle";
            this.ayudaPacientes.Appearance = appearance50;
            this.ayudaPacientes.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            this.ayudaPacientes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaPacientes.Location = new System.Drawing.Point(81, 28);
            this.ayudaPacientes.Name = "ayudaPacientes";
            this.ayudaPacientes.Size = new System.Drawing.Size(30, 21);
            this.ayudaPacientes.TabIndex = 268;
            this.ayudaPacientes.TabStop = false;
            this.ayudaPacientes.Text = "F1";
            this.ayudaPacientes.Click += new System.EventHandler(this.ayudaPacientes_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FacturasAnuladas
            // 
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.FacturasAnuladas.DisplayLayout.Appearance = appearance17;
            this.FacturasAnuladas.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.FacturasAnuladas.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance18.BorderColor = System.Drawing.SystemColors.Window;
            this.FacturasAnuladas.DisplayLayout.GroupByBox.Appearance = appearance18;
            appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
            this.FacturasAnuladas.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
            this.FacturasAnuladas.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance20.BackColor2 = System.Drawing.SystemColors.Control;
            appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
            this.FacturasAnuladas.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
            this.FacturasAnuladas.DisplayLayout.MaxColScrollRegions = 1;
            this.FacturasAnuladas.DisplayLayout.MaxRowScrollRegions = 1;
            appearance21.BackColor = System.Drawing.SystemColors.Window;
            appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FacturasAnuladas.DisplayLayout.Override.ActiveCellAppearance = appearance21;
            appearance22.BackColor = System.Drawing.SystemColors.Highlight;
            appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.FacturasAnuladas.DisplayLayout.Override.ActiveRowAppearance = appearance22;
            this.FacturasAnuladas.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.FacturasAnuladas.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            this.FacturasAnuladas.DisplayLayout.Override.CardAreaAppearance = appearance23;
            appearance24.BorderColor = System.Drawing.Color.Silver;
            appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.FacturasAnuladas.DisplayLayout.Override.CellAppearance = appearance24;
            this.FacturasAnuladas.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.FacturasAnuladas.DisplayLayout.Override.CellPadding = 0;
            appearance25.BackColor = System.Drawing.SystemColors.Control;
            appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance25.BorderColor = System.Drawing.SystemColors.Window;
            this.FacturasAnuladas.DisplayLayout.Override.GroupByRowAppearance = appearance25;
            appearance26.TextHAlignAsString = "Left";
            this.FacturasAnuladas.DisplayLayout.Override.HeaderAppearance = appearance26;
            this.FacturasAnuladas.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.FacturasAnuladas.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance27.BackColor = System.Drawing.SystemColors.Window;
            appearance27.BorderColor = System.Drawing.Color.Silver;
            this.FacturasAnuladas.DisplayLayout.Override.RowAppearance = appearance27;
            this.FacturasAnuladas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
            this.FacturasAnuladas.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
            this.FacturasAnuladas.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.FacturasAnuladas.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.FacturasAnuladas.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.FacturasAnuladas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FacturasAnuladas.Location = new System.Drawing.Point(0, 244);
            this.FacturasAnuladas.Name = "FacturasAnuladas";
            this.FacturasAnuladas.Size = new System.Drawing.Size(969, 171);
            this.FacturasAnuladas.TabIndex = 256;
            this.FacturasAnuladas.Text = "ultraGrid1";
            this.FacturasAnuladas.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.FacturasAnuladas_InitializeLayout);
            // 
            // frm_FacturaCambiar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 415);
            this.Controls.Add(this.FacturasAnuladas);
            this.Controls.Add(this.ultraGroupBoxPaciente);
            this.Controls.Add(this.menu);
            this.Name = "frm_FacturaCambiar";
            this.Text = "Facturas Anuladas";
            this.Load += new System.EventHandler(this.frm_FacturaCambiar_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPaciente)).EndInit();
            this.ultraGroupBoxPaciente.ResumeLayout(false);
            this.ultraGroupBoxPaciente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FacturasAnuladas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        protected System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBoxPaciente;
        private Infragistics.Win.Misc.UltraButton ayudaPacientes;
        private System.Windows.Forms.TextBox txtPaciente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHab;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAtencion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFactura;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpAlta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpIngreso;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbldoc;
        private System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtFacturaNueva;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Infragistics.Win.UltraWinGrid.UltraGrid FacturasAnuladas;
        private System.Windows.Forms.TextBox txtcredito;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblAnterior;
    }
}