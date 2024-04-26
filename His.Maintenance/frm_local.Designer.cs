namespace His.Maintenance
{
    partial class frm_local
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_local));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.txt_codzona = new System.Windows.Forms.TextBox();
            this.cmb_usuarios = new System.Windows.Forms.ComboBox();
            this.cmb_tiponegocio = new System.Windows.Forms.ComboBox();
            this.chk_matriz = new System.Windows.Forms.CheckBox();
            this.chk_principal = new System.Windows.Forms.CheckBox();
            this.txt_nempleados = new System.Windows.Forms.TextBox();
            this.txt_pordistribucion = new System.Windows.Forms.TextBox();
            this.txt_area = new System.Windows.Forms.TextBox();
            this.txt_fax = new System.Windows.Forms.TextBox();
            this.txt_direccion = new System.Windows.Forms.TextBox();
            this.txt_nomlocal = new System.Windows.Forms.TextBox();
            this.txt_codlocal = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.gridlocales = new System.Windows.Forms.DataGridView();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            this.txt_telefono3 = new System.Windows.Forms.MaskedTextBox();
            this.txt_telefono2 = new System.Windows.Forms.MaskedTextBox();
            this.txt_telefono1 = new System.Windows.Forms.MaskedTextBox();
            this.txt_ruc = new System.Windows.Forms.MaskedTextBox();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridlocales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_codzona
            // 
            this.txt_codzona.Location = new System.Drawing.Point(124, 83);
            this.txt_codzona.Name = "txt_codzona";
            this.txt_codzona.ReadOnly = true;
            this.txt_codzona.Size = new System.Drawing.Size(210, 20);
            this.txt_codzona.TabIndex = 41;
            this.txt_codzona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codzona_KeyPress);
            // 
            // cmb_usuarios
            // 
            this.cmb_usuarios.FormattingEnabled = true;
            this.cmb_usuarios.Location = new System.Drawing.Point(565, 82);
            this.cmb_usuarios.Name = "cmb_usuarios";
            this.cmb_usuarios.Size = new System.Drawing.Size(216, 21);
            this.cmb_usuarios.TabIndex = 11;
            this.cmb_usuarios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_usuarios_KeyPress);
            // 
            // cmb_tiponegocio
            // 
            this.cmb_tiponegocio.FormattingEnabled = true;
            this.cmb_tiponegocio.Location = new System.Drawing.Point(124, 107);
            this.cmb_tiponegocio.Name = "cmb_tiponegocio";
            this.cmb_tiponegocio.Size = new System.Drawing.Size(210, 21);
            this.cmb_tiponegocio.TabIndex = 1;
            this.cmb_tiponegocio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_tiponegocio_KeyPress);
            // 
            // chk_matriz
            // 
            this.chk_matriz.AutoSize = true;
            this.chk_matriz.BackColor = System.Drawing.Color.Transparent;
            this.chk_matriz.Location = new System.Drawing.Point(565, 186);
            this.chk_matriz.Name = "chk_matriz";
            this.chk_matriz.Size = new System.Drawing.Size(54, 17);
            this.chk_matriz.TabIndex = 14;
            this.chk_matriz.Text = "Matriz";
            this.chk_matriz.UseVisualStyleBackColor = false;
            this.chk_matriz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chk_matriz_KeyPress);
            // 
            // chk_principal
            // 
            this.chk_principal.AutoSize = true;
            this.chk_principal.BackColor = System.Drawing.Color.Transparent;
            this.chk_principal.Location = new System.Drawing.Point(565, 160);
            this.chk_principal.Name = "chk_principal";
            this.chk_principal.Size = new System.Drawing.Size(66, 17);
            this.chk_principal.TabIndex = 13;
            this.chk_principal.Text = "Principal";
            this.chk_principal.UseVisualStyleBackColor = false;
            this.chk_principal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chk_principal_KeyPress);
            // 
            // txt_nempleados
            // 
            this.txt_nempleados.Location = new System.Drawing.Point(565, 107);
            this.txt_nempleados.Name = "txt_nempleados";
            this.txt_nempleados.Size = new System.Drawing.Size(100, 20);
            this.txt_nempleados.TabIndex = 12;
            this.txt_nempleados.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_nempleados_KeyPress);
            // 
            // txt_pordistribucion
            // 
            this.txt_pordistribucion.Location = new System.Drawing.Point(565, 57);
            this.txt_pordistribucion.Name = "txt_pordistribucion";
            this.txt_pordistribucion.Size = new System.Drawing.Size(100, 20);
            this.txt_pordistribucion.TabIndex = 10;
            this.txt_pordistribucion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_pordistribucion_KeyPress);
            // 
            // txt_area
            // 
            this.txt_area.Location = new System.Drawing.Point(565, 132);
            this.txt_area.Name = "txt_area";
            this.txt_area.Size = new System.Drawing.Size(100, 20);
            this.txt_area.TabIndex = 5;
            this.txt_area.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_area_KeyPress);
            // 
            // txt_fax
            // 
            this.txt_fax.Location = new System.Drawing.Point(565, 32);
            this.txt_fax.MaxLength = 16;
            this.txt_fax.Name = "txt_fax";
            this.txt_fax.Size = new System.Drawing.Size(100, 20);
            this.txt_fax.TabIndex = 9;
            this.txt_fax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_fax_KeyPress);
            // 
            // txt_direccion
            // 
            this.txt_direccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_direccion.Location = new System.Drawing.Point(124, 157);
            this.txt_direccion.MaxLength = 128;
            this.txt_direccion.Name = "txt_direccion";
            this.txt_direccion.Size = new System.Drawing.Size(317, 20);
            this.txt_direccion.TabIndex = 4;
            this.txt_direccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_direccion_KeyPress);
            // 
            // txt_nomlocal
            // 
            this.txt_nomlocal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nomlocal.Location = new System.Drawing.Point(124, 57);
            this.txt_nomlocal.MaxLength = 128;
            this.txt_nomlocal.Name = "txt_nomlocal";
            this.txt_nomlocal.Size = new System.Drawing.Size(317, 20);
            this.txt_nomlocal.TabIndex = 0;
            this.txt_nomlocal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_nomlocal_KeyPress);
            // 
            // txt_codlocal
            // 
            this.txt_codlocal.Location = new System.Drawing.Point(124, 32);
            this.txt_codlocal.Name = "txt_codlocal";
            this.txt_codlocal.ReadOnly = true;
            this.txt_codlocal.Size = new System.Drawing.Size(94, 20);
            this.txt_codlocal.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(471, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "% Distribución";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(474, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "No. Empleados:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(474, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Fax:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(474, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Administrador";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(474, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Area:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(23, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "RUC:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(23, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Teléfonos:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(23, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Dirección:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(23, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Tipo de Negocio:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(23, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Codigo de Zona:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(23, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Código:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(23, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nombre:";
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnActualizar,
            this.btnEliminar,
            this.btnGuardar,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(830, 36);
            this.menu.TabIndex = 58;
            this.menu.Text = "menu";
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(62, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(78, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(70, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(69, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 33);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(49, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // gridlocales
            // 
            this.gridlocales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridlocales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridlocales.Location = new System.Drawing.Point(3, 232);
            this.gridlocales.Name = "gridlocales";
            this.gridlocales.RowHeadersWidth = 20;
            this.gridlocales.Size = new System.Drawing.Size(823, 85);
            this.gridlocales.TabIndex = 0;
            this.gridlocales.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridlocales_ColumnHeaderMouseClick);
            this.gridlocales.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridlocales_RowHeaderMouseDoubleClick);
            // 
            // controlErrores
            // 
            this.controlErrores.ContainerControl = this;
            // 
            // mnuContexsecundario
            // 
            this.mnuContexsecundario.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBuscar,
            this.toolStripSeparator1,
            this.cancelarToolStripMenuItem});
            this.mnuContexsecundario.Name = "mnuContext";
            this.mnuContexsecundario.Size = new System.Drawing.Size(121, 54);
            // 
            // mnuBuscar
            // 
            this.mnuBuscar.Name = "mnuBuscar";
            this.mnuBuscar.Size = new System.Drawing.Size(120, 22);
            this.mnuBuscar.Text = "Buscar";
            this.mnuBuscar.Click += new System.EventHandler(this.mnuBuscar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(117, 6);
            // 
            // cancelarToolStripMenuItem
            // 
            this.cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            this.cancelarToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.cancelarToolStripMenuItem.Text = "Cancelar";
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosPrincipales.Controls.Add(this.txt_telefono3);
            this.grpDatosPrincipales.Controls.Add(this.txt_telefono2);
            this.grpDatosPrincipales.Controls.Add(this.txt_telefono1);
            this.grpDatosPrincipales.Controls.Add(this.txt_ruc);
            this.grpDatosPrincipales.Controls.Add(this.txt_codzona);
            this.grpDatosPrincipales.Controls.Add(this.txt_direccion);
            this.grpDatosPrincipales.Controls.Add(this.label2);
            this.grpDatosPrincipales.Controls.Add(this.cmb_usuarios);
            this.grpDatosPrincipales.Controls.Add(this.label1);
            this.grpDatosPrincipales.Controls.Add(this.cmb_tiponegocio);
            this.grpDatosPrincipales.Controls.Add(this.label3);
            this.grpDatosPrincipales.Controls.Add(this.chk_matriz);
            this.grpDatosPrincipales.Controls.Add(this.label4);
            this.grpDatosPrincipales.Controls.Add(this.chk_principal);
            this.grpDatosPrincipales.Controls.Add(this.label5);
            this.grpDatosPrincipales.Controls.Add(this.txt_nempleados);
            this.grpDatosPrincipales.Controls.Add(this.label7);
            this.grpDatosPrincipales.Controls.Add(this.txt_pordistribucion);
            this.grpDatosPrincipales.Controls.Add(this.label8);
            this.grpDatosPrincipales.Controls.Add(this.txt_area);
            this.grpDatosPrincipales.Controls.Add(this.label9);
            this.grpDatosPrincipales.Controls.Add(this.txt_fax);
            this.grpDatosPrincipales.Controls.Add(this.label10);
            this.grpDatosPrincipales.Controls.Add(this.label11);
            this.grpDatosPrincipales.Controls.Add(this.label12);
            this.grpDatosPrincipales.Controls.Add(this.label13);
            this.grpDatosPrincipales.Controls.Add(this.txt_codlocal);
            this.grpDatosPrincipales.Controls.Add(this.txt_nomlocal);
            this.grpDatosPrincipales.Location = new System.Drawing.Point(3, 3);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(823, 223);
            this.grpDatosPrincipales.TabIndex = 59;
            this.grpDatosPrincipales.Text = "Datos Generales";
            this.grpDatosPrincipales.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
            // 
            // txt_telefono3
            // 
            this.txt_telefono3.Location = new System.Drawing.Point(343, 183);
            this.txt_telefono3.Mask = "00-000-0000";
            this.txt_telefono3.Name = "txt_telefono3";
            this.txt_telefono3.Size = new System.Drawing.Size(98, 20);
            this.txt_telefono3.TabIndex = 45;
            this.txt_telefono3.Leave += new System.EventHandler(this.txt_telefono3_Leave);
            // 
            // txt_telefono2
            // 
            this.txt_telefono2.Location = new System.Drawing.Point(238, 183);
            this.txt_telefono2.Mask = "00-000-0000";
            this.txt_telefono2.Name = "txt_telefono2";
            this.txt_telefono2.Size = new System.Drawing.Size(100, 20);
            this.txt_telefono2.TabIndex = 44;
            this.txt_telefono2.Leave += new System.EventHandler(this.txt_telefono2_Leave);
            // 
            // txt_telefono1
            // 
            this.txt_telefono1.Location = new System.Drawing.Point(124, 183);
            this.txt_telefono1.Mask = "00-000-0000";
            this.txt_telefono1.Name = "txt_telefono1";
            this.txt_telefono1.Size = new System.Drawing.Size(108, 20);
            this.txt_telefono1.TabIndex = 43;
            this.txt_telefono1.Leave += new System.EventHandler(this.txt_telefono1_Leave);
            // 
            // txt_ruc
            // 
            this.txt_ruc.Location = new System.Drawing.Point(124, 134);
            this.txt_ruc.Mask = "9999999999999";
            this.txt_ruc.Name = "txt_ruc";
            this.txt_ruc.Size = new System.Drawing.Size(137, 20);
            this.txt_ruc.TabIndex = 42;
            this.txt_ruc.Leave += new System.EventHandler(this.txt_ruc_Leave);
            // 
            // ultraPanel1
            // 
            this.ultraPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightGray;
            appearance1.BackColor2 = System.Drawing.Color.GhostWhite;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop37;
            this.ultraPanel1.Appearance = appearance1;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.gridlocales);
            this.ultraPanel1.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.ultraPanel1.Location = new System.Drawing.Point(0, 37);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(829, 320);
            this.ultraPanel1.TabIndex = 60;
            // 
            // frm_local
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(830, 358);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.ultraPanel1);
            this.Name = "frm_local";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Locales";
            this.Load += new System.EventHandler(this.frm_local_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridlocales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_codzona;
        private System.Windows.Forms.ComboBox cmb_usuarios;
        private System.Windows.Forms.ComboBox cmb_tiponegocio;
        private System.Windows.Forms.CheckBox chk_matriz;
        private System.Windows.Forms.CheckBox chk_principal;
        private System.Windows.Forms.TextBox txt_nempleados;
        private System.Windows.Forms.TextBox txt_pordistribucion;
        private System.Windows.Forms.TextBox txt_area;
        private System.Windows.Forms.TextBox txt_fax;
        private System.Windows.Forms.TextBox txt_direccion;
        private System.Windows.Forms.TextBox txt_nomlocal;
        private System.Windows.Forms.TextBox txt_codlocal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.DataGridView gridlocales;
        private System.Windows.Forms.ErrorProvider controlErrores;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem mnuBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private System.Windows.Forms.MaskedTextBox txt_ruc;
        private System.Windows.Forms.MaskedTextBox txt_telefono1;
        private System.Windows.Forms.MaskedTextBox txt_telefono3;
        private System.Windows.Forms.MaskedTextBox txt_telefono2;
    }
}