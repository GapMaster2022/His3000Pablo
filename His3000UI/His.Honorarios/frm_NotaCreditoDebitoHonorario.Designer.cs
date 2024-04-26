namespace His.Honorarios
{
    partial class frm_NotaCreditoDebitoHonorario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_NotaCreditoDebitoHonorario));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.cmb_tipoformapago = new System.Windows.Forms.ComboBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_formapago = new System.Windows.Forms.ComboBox();
            this.txt_factura = new System.Windows.Forms.MaskedTextBox();
            this.txt_cuentacontable = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_ruc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_iva = new System.Windows.Forms.TextBox();
            this.txt_observacion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_monto = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_ingreso = new System.Windows.Forms.MaskedTextBox();
            this.chk_iva = new System.Windows.Forms.CheckBox();
            this.txt_medico = new System.Windows.Forms.TextBox();
            this.txt_codigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridnotacd = new System.Windows.Forms.DataGridView();
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.chk_interno = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new Infragistics.Win.Misc.UltraButton();
            this.pictureBox1 = new Infragistics.Win.Misc.UltraButton();
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.menu.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridnotacd)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnActualizar,
            this.btnGuardar,
            this.btnEliminar,
            this.btnImprimir,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(610, 36);
            this.menu.TabIndex = 64;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(58, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(70, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(66, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(58, 33);
            this.btnEliminar.Text = "Anular";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(65, 33);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(69, 33);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(47, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // cmb_tipoformapago
            // 
            this.cmb_tipoformapago.FormattingEnabled = true;
            this.cmb_tipoformapago.Location = new System.Drawing.Point(126, 214);
            this.cmb_tipoformapago.Name = "cmb_tipoformapago";
            this.cmb_tipoformapago.Size = new System.Drawing.Size(152, 21);
            this.cmb_tipoformapago.TabIndex = 75;
            this.cmb_tipoformapago.SelectedIndexChanged += new System.EventHandler(this.cmb_tipoformapago_SelectedIndexChanged);
            this.cmb_tipoformapago.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_tipoformapago_KeyPress);
            // 
            // txt_nombre
            // 
            this.txt_nombre.BackColor = System.Drawing.Color.LightGray;
            this.txt_nombre.Location = new System.Drawing.Point(217, 46);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.ReadOnly = true;
            this.txt_nombre.Size = new System.Drawing.Size(317, 20);
            this.txt_nombre.TabIndex = 74;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(27, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 73;
            this.label7.Text = "Forma de Pago:";
            // 
            // cmb_formapago
            // 
            this.cmb_formapago.FormattingEnabled = true;
            this.cmb_formapago.Location = new System.Drawing.Point(306, 214);
            this.cmb_formapago.Name = "cmb_formapago";
            this.cmb_formapago.Size = new System.Drawing.Size(228, 21);
            this.cmb_formapago.TabIndex = 72;
            // 
            // txt_factura
            // 
            this.txt_factura.Location = new System.Drawing.Point(126, 100);
            this.txt_factura.Mask = "000-000-0000000";
            this.txt_factura.Name = "txt_factura";
            this.txt_factura.Size = new System.Drawing.Size(109, 20);
            this.txt_factura.TabIndex = 71;
            this.txt_factura.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_factura_KeyDown);
            this.txt_factura.Leave += new System.EventHandler(this.txt_factura_Leave);
            this.txt_factura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_factura_KeyPress);
            // 
            // txt_cuentacontable
            // 
            this.txt_cuentacontable.Location = new System.Drawing.Point(126, 183);
            this.txt_cuentacontable.Mask = "000000-000";
            this.txt_cuentacontable.Name = "txt_cuentacontable";
            this.txt_cuentacontable.Size = new System.Drawing.Size(109, 20);
            this.txt_cuentacontable.TabIndex = 69;
            this.txt_cuentacontable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cuentacontable_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(27, 186);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 70;
            this.label15.Text = "Cuenta Contable:";
            // 
            // txt_ruc
            // 
            this.txt_ruc.BackColor = System.Drawing.Color.LightGray;
            this.txt_ruc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_ruc.Enabled = false;
            this.txt_ruc.Location = new System.Drawing.Point(126, 73);
            this.txt_ruc.MaxLength = 13;
            this.txt_ruc.Name = "txt_ruc";
            this.txt_ruc.ReadOnly = true;
            this.txt_ruc.Size = new System.Drawing.Size(109, 20);
            this.txt_ruc.TabIndex = 68;
            this.txt_ruc.Leave += new System.EventHandler(this.txt_ruc_Leave);
            this.txt_ruc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ruc_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(27, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 67;
            this.label6.Text = "RUC:";
            // 
            // txt_iva
            // 
            this.txt_iva.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_iva.Location = new System.Drawing.Point(455, 119);
            this.txt_iva.MaxLength = 40;
            this.txt_iva.Name = "txt_iva";
            this.txt_iva.Size = new System.Drawing.Size(79, 20);
            this.txt_iva.TabIndex = 66;
            // 
            // txt_observacion
            // 
            this.txt_observacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_observacion.Location = new System.Drawing.Point(126, 154);
            this.txt_observacion.MaxLength = 40;
            this.txt_observacion.Name = "txt_observacion";
            this.txt_observacion.Size = new System.Drawing.Size(408, 20);
            this.txt_observacion.TabIndex = 65;
            this.txt_observacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_observacion_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(27, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Observación:";
            // 
            // txt_monto
            // 
            this.txt_monto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_monto.Location = new System.Drawing.Point(126, 127);
            this.txt_monto.MaxLength = 40;
            this.txt_monto.Name = "txt_monto";
            this.txt_monto.Size = new System.Drawing.Size(109, 20);
            this.txt_monto.TabIndex = 63;
            this.txt_monto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_monto_KeyDown);
            this.txt_monto.Leave += new System.EventHandler(this.txt_monto_Leave);
            this.txt_monto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_monto_KeyPress);
            this.txt_monto.Enter += new System.EventHandler(this.txt_monto_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(27, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Monto:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(27, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Factura:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(334, 19);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(102, 17);
            this.label25.TabIndex = 59;
            this.label25.Text = "Fecha ingreso:";
            // 
            // txt_ingreso
            // 
            this.txt_ingreso.BackColor = System.Drawing.Color.LightGray;
            this.txt_ingreso.Location = new System.Drawing.Point(442, 19);
            this.txt_ingreso.Mask = "00/00/0000";
            this.txt_ingreso.Name = "txt_ingreso";
            this.txt_ingreso.ReadOnly = true;
            this.txt_ingreso.Size = new System.Drawing.Size(92, 20);
            this.txt_ingreso.TabIndex = 58;
            this.txt_ingreso.ValidatingType = typeof(System.DateTime);
            // 
            // chk_iva
            // 
            this.chk_iva.AutoSize = true;
            this.chk_iva.BackColor = System.Drawing.Color.Transparent;
            this.chk_iva.Location = new System.Drawing.Point(408, 121);
            this.chk_iva.Name = "chk_iva";
            this.chk_iva.Size = new System.Drawing.Size(41, 17);
            this.chk_iva.TabIndex = 17;
            this.chk_iva.Text = "Iva";
            this.chk_iva.UseVisualStyleBackColor = false;
            this.chk_iva.CheckedChanged += new System.EventHandler(this.chk_iva_CheckedChanged);
            // 
            // txt_medico
            // 
            this.txt_medico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_medico.Location = new System.Drawing.Point(126, 46);
            this.txt_medico.MaxLength = 40;
            this.txt_medico.Name = "txt_medico";
            this.txt_medico.Size = new System.Drawing.Size(85, 20);
            this.txt_medico.TabIndex = 14;
            this.txt_medico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_medico_KeyDown);
            this.txt_medico.Leave += new System.EventHandler(this.txt_medico_Leave);
            this.txt_medico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_medico_KeyPress);
            // 
            // txt_codigo
            // 
            this.txt_codigo.BackColor = System.Drawing.Color.LightGray;
            this.txt_codigo.Location = new System.Drawing.Point(126, 19);
            this.txt_codigo.Name = "txt_codigo";
            this.txt_codigo.ReadOnly = true;
            this.txt_codigo.Size = new System.Drawing.Size(109, 20);
            this.txt_codigo.TabIndex = 13;
            this.txt_codigo.Leave += new System.EventHandler(this.txt_codigo_Leave);
            this.txt_codigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codigo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(27, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nombre:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(27, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Código:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.gridnotacd);
            this.panel2.Location = new System.Drawing.Point(21, 307);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(562, 186);
            this.panel2.TabIndex = 69;
            // 
            // gridnotacd
            // 
            this.gridnotacd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridnotacd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridnotacd.Location = new System.Drawing.Point(3, 3);
            this.gridnotacd.Name = "gridnotacd";
            this.gridnotacd.ReadOnly = true;
            this.gridnotacd.Size = new System.Drawing.Size(556, 180);
            this.gridnotacd.TabIndex = 0;
            this.gridnotacd.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridnotacd_ColumnHeaderMouseClick);
            this.gridnotacd.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridnotacd_RowHeaderMouseDoubleClick);
            // 
            // mnuContexsecundario
            // 
            this.mnuContexsecundario.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBuscar,
            this.toolStripSeparator1,
            this.cancelarToolStripMenuItem});
            this.mnuContexsecundario.Name = "mnuContext";
            this.mnuContexsecundario.Size = new System.Drawing.Size(128, 54);
            // 
            // mnuBuscar
            // 
            this.mnuBuscar.Name = "mnuBuscar";
            this.mnuBuscar.Size = new System.Drawing.Size(127, 22);
            this.mnuBuscar.Text = "Buscar";
            this.mnuBuscar.Click += new System.EventHandler(this.mnuBuscar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // cancelarToolStripMenuItem
            // 
            this.cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            this.cancelarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.cancelarToolStripMenuItem.Text = "Cancelar";
            // 
            // controlErrores
            // 
            this.controlErrores.ContainerControl = this;
            // 
            // chk_interno
            // 
            this.chk_interno.AutoSize = true;
            this.chk_interno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_interno.Location = new System.Drawing.Point(429, 281);
            this.chk_interno.Name = "chk_interno";
            this.chk_interno.Size = new System.Drawing.Size(139, 20);
            this.chk_interno.TabIndex = 71;
            this.chk_interno.Text = "Documento Interno";
            this.chk_interno.UseVisualStyleBackColor = true;
            this.chk_interno.CheckedChanged += new System.EventHandler(this.chk_interno_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.pictureBox2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pictureBox2.Location = new System.Drawing.Point(98, 102);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 18);
            this.pictureBox2.TabIndex = 242;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Text = "F1";
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.pictureBox1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pictureBox1.Location = new System.Drawing.Point(98, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 18);
            this.pictureBox1.TabIndex = 241;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Text = "F1";
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.Controls.Add(this.pictureBox2);
            this.grpDatosPrincipales.Controls.Add(this.txt_codigo);
            this.grpDatosPrincipales.Controls.Add(this.pictureBox1);
            this.grpDatosPrincipales.Controls.Add(this.txt_ruc);
            this.grpDatosPrincipales.Controls.Add(this.txt_nombre);
            this.grpDatosPrincipales.Controls.Add(this.label1);
            this.grpDatosPrincipales.Controls.Add(this.label4);
            this.grpDatosPrincipales.Controls.Add(this.txt_factura);
            this.grpDatosPrincipales.Controls.Add(this.txt_monto);
            this.grpDatosPrincipales.Controls.Add(this.txt_ingreso);
            this.grpDatosPrincipales.Controls.Add(this.label7);
            this.grpDatosPrincipales.Controls.Add(this.label25);
            this.grpDatosPrincipales.Controls.Add(this.label2);
            this.grpDatosPrincipales.Controls.Add(this.txt_observacion);
            this.grpDatosPrincipales.Controls.Add(this.label15);
            this.grpDatosPrincipales.Controls.Add(this.chk_iva);
            this.grpDatosPrincipales.Controls.Add(this.label6);
            this.grpDatosPrincipales.Controls.Add(this.txt_cuentacontable);
            this.grpDatosPrincipales.Controls.Add(this.txt_medico);
            this.grpDatosPrincipales.Controls.Add(this.txt_iva);
            this.grpDatosPrincipales.Controls.Add(this.cmb_tipoformapago);
            this.grpDatosPrincipales.Controls.Add(this.cmb_formapago);
            this.grpDatosPrincipales.Controls.Add(this.label3);
            this.grpDatosPrincipales.Controls.Add(this.label5);
            this.grpDatosPrincipales.Location = new System.Drawing.Point(21, 24);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(562, 251);
            this.grpDatosPrincipales.TabIndex = 73;
            this.grpDatosPrincipales.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // frm_NotaCreditoDebitoHonorario_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.Appearance = appearance1;
            // 
            // frm_NotaCreditoDebitoHonorario_Fill_Panel.ClientArea
            // 
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.ClientArea.Controls.Add(this.chk_interno);
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.ClientArea.Controls.Add(this.panel2);
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.Name = "frm_NotaCreditoDebitoHonorario_Fill_Panel";
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.Size = new System.Drawing.Size(610, 544);
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.TabIndex = 65;
            // 
            // frm_NotaCreditoDebitoHonorario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(610, 580);
            this.Controls.Add(this.frm_NotaCreditoDebitoHonorario_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "frm_NotaCreditoDebitoHonorario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nota Credito Debito Honorario";
            this.Load += new System.EventHandler(this.frm_NotaCreditoDebitoHonorario_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridnotacd)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.ClientArea.PerformLayout();
            this.frm_NotaCreditoDebitoHonorario_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.CheckBox chk_iva;
        private System.Windows.Forms.TextBox txt_medico;
        private System.Windows.Forms.TextBox txt_codigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridnotacd;
        private System.Windows.Forms.TextBox txt_observacion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_monto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.MaskedTextBox txt_ingreso;
        protected System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem mnuBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider controlErrores;
        private System.Windows.Forms.TextBox txt_iva;
        private System.Windows.Forms.TextBox txt_ruc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox txt_cuentacontable;
        private System.Windows.Forms.Label label15;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.MaskedTextBox txt_factura;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmb_formapago;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.CheckBox chk_interno;
        private System.Windows.Forms.ComboBox cmb_tipoformapago;
        private Infragistics.Win.Misc.UltraButton pictureBox2;
        private Infragistics.Win.Misc.UltraButton pictureBox1;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
        private Infragistics.Win.Misc.UltraPanel frm_NotaCreditoDebitoHonorario_Fill_Panel;
        //private AxACTIVESKINLib.AxSkin axSkin1;
    }
}