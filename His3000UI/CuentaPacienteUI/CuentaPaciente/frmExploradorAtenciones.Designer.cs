namespace CuentaPaciente
{
    partial class frmExploradorAtenciones
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.Form1_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.grpFechas = new Infragistics.Win.Misc.UltraGroupBox();
            this.chkSinFiltroFechas = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFiltroDesde = new System.Windows.Forms.DateTimePicker();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cmbConvenioPago = new System.Windows.Forms.ComboBox();
            this.lblFiltroTipoFormaPago = new System.Windows.Forms.Label();
            this.cb_seguros = new System.Windows.Forms.ComboBox();
            this.bindingNavigatorAtenciones = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGenerar = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripDropDownButton();
            this.atenciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.honorariosDiariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lLblNinguno = new System.Windows.Forms.LinkLabel();
            this.lLblTodos = new System.Windows.Forms.LinkLabel();
            this.uGridCuentas = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.uToolTip = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.Form1_Fill_Panel.ClientArea.SuspendLayout();
            this.Form1_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).BeginInit();
            this.grpFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorAtenciones)).BeginInit();
            this.bindingNavigatorAtenciones.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uGridCuentas)).BeginInit();
            this.SuspendLayout();
            // 
            // Form1_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.LightGray;
            appearance1.BackColor2 = System.Drawing.Color.GhostWhite;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Circular;
            this.Form1_Fill_Panel.Appearance = appearance1;
            // 
            // Form1_Fill_Panel.ClientArea
            // 
            this.Form1_Fill_Panel.ClientArea.Controls.Add(this.grpFechas);
            this.Form1_Fill_Panel.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.Form1_Fill_Panel.ClientArea.Controls.Add(this.bindingNavigatorAtenciones);
            this.Form1_Fill_Panel.ClientArea.Controls.Add(this.splitContainer1);
            this.Form1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.Form1_Fill_Panel.Location = new System.Drawing.Point(0, 0);
            this.Form1_Fill_Panel.Name = "Form1_Fill_Panel";
            this.Form1_Fill_Panel.Size = new System.Drawing.Size(968, 551);
            this.Form1_Fill_Panel.TabIndex = 3;
            // 
            // grpFechas
            // 
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.grpFechas.Appearance = appearance2;
            this.grpFechas.Controls.Add(this.chkSinFiltroFechas);
            this.grpFechas.Controls.Add(this.label1);
            this.grpFechas.Controls.Add(this.lblHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroDesde);
            this.grpFechas.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.grpFechas.Location = new System.Drawing.Point(225, 39);
            this.grpFechas.Name = "grpFechas";
            this.grpFechas.Size = new System.Drawing.Size(179, 98);
            this.grpFechas.TabIndex = 31;
            this.grpFechas.Text = "Filtro por Fechas:";
            this.grpFechas.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // chkSinFiltroFechas
            // 
            this.chkSinFiltroFechas.AutoSize = true;
            this.chkSinFiltroFechas.Checked = true;
            this.chkSinFiltroFechas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSinFiltroFechas.Location = new System.Drawing.Point(64, 24);
            this.chkSinFiltroFechas.Name = "chkSinFiltroFechas";
            this.chkSinFiltroFechas.Size = new System.Drawing.Size(66, 17);
            this.chkSinFiltroFechas.TabIndex = 7;
            this.chkSinFiltroFechas.Text = "Sin Filtro";
            this.chkSinFiltroFechas.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Desde:";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(20, 72);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 6;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(64, 68);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroHasta.TabIndex = 4;
            // 
            // dtpFiltroDesde
            // 
            this.dtpFiltroDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroDesde.Location = new System.Drawing.Point(64, 42);
            this.dtpFiltroDesde.Name = "dtpFiltroDesde";
            this.dtpFiltroDesde.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroDesde.TabIndex = 3;
            this.dtpFiltroDesde.Value = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.cmbConvenioPago);
            this.ultraGroupBox1.Controls.Add(this.lblFiltroTipoFormaPago);
            this.ultraGroupBox1.Controls.Add(this.cb_seguros);
            this.ultraGroupBox1.Location = new System.Drawing.Point(218, 33);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(532, 110);
            this.ultraGroupBox1.TabIndex = 32;
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // cmbConvenioPago
            // 
            this.cmbConvenioPago.Enabled = false;
            this.cmbConvenioPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbConvenioPago.FormattingEnabled = true;
            this.cmbConvenioPago.Location = new System.Drawing.Point(197, 42);
            this.cmbConvenioPago.Name = "cmbConvenioPago";
            this.cmbConvenioPago.Size = new System.Drawing.Size(164, 21);
            this.cmbConvenioPago.TabIndex = 242;
            this.cmbConvenioPago.SelectedValueChanged += new System.EventHandler(this.cmbConvenioPago_SelectedValueChanged);
            // 
            // lblFiltroTipoFormaPago
            // 
            this.lblFiltroTipoFormaPago.AutoSize = true;
            this.lblFiltroTipoFormaPago.BackColor = System.Drawing.Color.Transparent;
            this.lblFiltroTipoFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltroTipoFormaPago.ForeColor = System.Drawing.Color.Black;
            this.lblFiltroTipoFormaPago.Location = new System.Drawing.Point(194, 26);
            this.lblFiltroTipoFormaPago.Name = "lblFiltroTipoFormaPago";
            this.lblFiltroTipoFormaPago.Size = new System.Drawing.Size(95, 13);
            this.lblFiltroTipoFormaPago.TabIndex = 243;
            this.lblFiltroTipoFormaPago.Text = "Convenio de Pago";
            // 
            // cb_seguros
            // 
            this.cb_seguros.Enabled = false;
            this.cb_seguros.FormattingEnabled = true;
            this.cb_seguros.Location = new System.Drawing.Point(197, 69);
            this.cb_seguros.Name = "cb_seguros";
            this.cb_seguros.Size = new System.Drawing.Size(317, 21);
            this.cb_seguros.TabIndex = 241;
            // 
            // bindingNavigatorAtenciones
            // 
            this.bindingNavigatorAtenciones.AddNewItem = null;
            this.bindingNavigatorAtenciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindingNavigatorAtenciones.AutoSize = false;
            this.bindingNavigatorAtenciones.CountItem = null;
            this.bindingNavigatorAtenciones.DeleteItem = null;
            this.bindingNavigatorAtenciones.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigatorAtenciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.btnActualizar,
            this.btnGenerar,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.btnNuevo,
            this.btnEliminar,
            this.btnImprimir,
            this.btnCancelar,
            this.toolStripSeparator2,
            this.btnSalir,
            this.toolStripSeparator1});
            this.bindingNavigatorAtenciones.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.bindingNavigatorAtenciones.Location = new System.Drawing.Point(1, 0);
            this.bindingNavigatorAtenciones.MoveFirstItem = null;
            this.bindingNavigatorAtenciones.MoveLastItem = null;
            this.bindingNavigatorAtenciones.MoveNextItem = null;
            this.bindingNavigatorAtenciones.MovePreviousItem = null;
            this.bindingNavigatorAtenciones.Name = "bindingNavigatorAtenciones";
            this.bindingNavigatorAtenciones.Padding = new System.Windows.Forms.Padding(0);
            this.bindingNavigatorAtenciones.PositionItem = null;
            this.bindingNavigatorAtenciones.Size = new System.Drawing.Size(967, 30);
            this.bindingNavigatorAtenciones.TabIndex = 30;
            this.bindingNavigatorAtenciones.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMoveFirstItem.Text = "Mover primero";
            this.bindingNavigatorMoveFirstItem.Visible = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(63, 27);
            this.btnActualizar.Text = "&Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnGenerar
            // 
            this.btnGenerar.Enabled = false;
            this.btnGenerar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(52, 27);
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMovePreviousItem.Text = "Mover anterior";
            this.bindingNavigatorMovePreviousItem.Visible = false;
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 30);
            this.bindingNavigatorSeparator.Visible = false;
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Posición";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Posición actual";
            this.bindingNavigatorPositionItem.Visible = false;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(37, 27);
            this.bindingNavigatorCountItem.Text = "de {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Número total de elementos";
            this.bindingNavigatorCountItem.Visible = false;
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMoveNextItem.Text = "Mover siguiente";
            this.bindingNavigatorMoveNextItem.Visible = false;
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMoveLastItem.Text = "Mover último";
            this.bindingNavigatorMoveLastItem.Visible = false;
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // btnNuevo
            // 
            this.btnNuevo.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(46, 27);
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.Visible = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(54, 27);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.atenciónToolStripMenuItem,
            this.honorariosDiariosToolStripMenuItem});
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(66, 27);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // atenciónToolStripMenuItem
            // 
            this.atenciónToolStripMenuItem.Name = "atenciónToolStripMenuItem";
            this.atenciónToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.atenciónToolStripMenuItem.Text = "Atención";
            // 
            // honorariosDiariosToolStripMenuItem
            // 
            this.honorariosDiariosToolStripMenuItem.Name = "honorariosDiariosToolStripMenuItem";
            this.honorariosDiariosToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.honorariosDiariosToolStripMenuItem.Text = "Honorarios";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Enabled = false;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(57, 27);
            this.btnCancelar.Text = "Cancelar";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // btnSalir
            // 
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(33, 27);
            this.btnSalir.Text = "Salir";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 149);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lLblNinguno);
            this.splitContainer1.Panel1.Controls.Add(this.lLblTodos);
            this.splitContainer1.Panel1.Controls.Add(this.uGridCuentas);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(965, 402);
            this.splitContainer1.SplitterDistance = 569;
            this.splitContainer1.TabIndex = 14;
            // 
            // lLblNinguno
            // 
            this.lLblNinguno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lLblNinguno.AutoSize = true;
            this.lLblNinguno.BackColor = System.Drawing.Color.Transparent;
            this.lLblNinguno.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.lLblNinguno.Location = new System.Drawing.Point(910, 4);
            this.lLblNinguno.Name = "lLblNinguno";
            this.lLblNinguno.Size = new System.Drawing.Size(47, 13);
            this.lLblNinguno.TabIndex = 27;
            this.lLblNinguno.TabStop = true;
            this.lLblNinguno.Text = "Ninguno";
            this.lLblNinguno.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLblNinguno_LinkClicked);
            // 
            // lLblTodos
            // 
            this.lLblTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lLblTodos.AutoSize = true;
            this.lLblTodos.BackColor = System.Drawing.Color.Transparent;
            this.lLblTodos.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.lLblTodos.Location = new System.Drawing.Point(856, 4);
            this.lLblTodos.Name = "lLblTodos";
            this.lLblTodos.Size = new System.Drawing.Size(37, 13);
            this.lLblTodos.TabIndex = 26;
            this.lLblTodos.TabStop = true;
            this.lLblTodos.Text = "Todos";
            this.lLblTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLblTodos_LinkClicked);
            // 
            // uGridCuentas
            // 
            this.uGridCuentas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColor = System.Drawing.SystemColors.Window;
            appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.uGridCuentas.DisplayLayout.Appearance = appearance3;
            this.uGridCuentas.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.uGridCuentas.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance4.BorderColor = System.Drawing.SystemColors.Window;
            this.uGridCuentas.DisplayLayout.GroupByBox.Appearance = appearance4;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.uGridCuentas.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
            this.uGridCuentas.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance6.BackColor2 = System.Drawing.SystemColors.Control;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.uGridCuentas.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
            this.uGridCuentas.DisplayLayout.MaxColScrollRegions = 1;
            this.uGridCuentas.DisplayLayout.MaxRowScrollRegions = 1;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uGridCuentas.DisplayLayout.Override.ActiveCellAppearance = appearance7;
            appearance8.BackColor = System.Drawing.SystemColors.Highlight;
            appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.uGridCuentas.DisplayLayout.Override.ActiveRowAppearance = appearance8;
            this.uGridCuentas.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uGridCuentas.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uGridCuentas.DisplayLayout.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            this.uGridCuentas.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.uGridCuentas.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uGridCuentas.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.uGridCuentas.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            this.uGridCuentas.DisplayLayout.Override.CardAreaAppearance = appearance9;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.uGridCuentas.DisplayLayout.Override.CellAppearance = appearance10;
            this.uGridCuentas.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.uGridCuentas.DisplayLayout.Override.CellPadding = 0;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance11.BorderColor = System.Drawing.SystemColors.Window;
            this.uGridCuentas.DisplayLayout.Override.GroupByRowAppearance = appearance11;
            appearance12.TextHAlignAsString = "Left";
            this.uGridCuentas.DisplayLayout.Override.HeaderAppearance = appearance12;
            this.uGridCuentas.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.uGridCuentas.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.Color.Silver;
            this.uGridCuentas.DisplayLayout.Override.RowAppearance = appearance13;
            this.uGridCuentas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
            this.uGridCuentas.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
            this.uGridCuentas.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uGridCuentas.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uGridCuentas.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.uGridCuentas.Location = new System.Drawing.Point(2, 4);
            this.uGridCuentas.Name = "uGridCuentas";
            this.uGridCuentas.Size = new System.Drawing.Size(960, 395);
            this.uGridCuentas.TabIndex = 0;
            this.uGridCuentas.Text = "Pedidos";
            this.uGridCuentas.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uGridPedidos_InitializeLayout);
            this.uGridCuentas.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.uGridPedidos_InitializeRow);
            this.uGridCuentas.Click += new System.EventHandler(this.uGridPedidos_Click);
            // 
            // uToolTip
            // 
            this.uToolTip.ContainingControl = this;
            this.uToolTip.DisplayStyle = Infragistics.Win.ToolTipDisplayStyle.Office2007;
            this.uToolTip.ToolTipTitle = "Información";
            // 
            // frmExploradorAtenciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 551);
            this.Controls.Add(this.Form1_Fill_Panel);
            this.Name = "frmExploradorAtenciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Explorardor de Pedidos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmExplorardorPedidos_Load);
            this.Form1_Fill_Panel.ClientArea.ResumeLayout(false);
            this.Form1_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).EndInit();
            this.grpFechas.ResumeLayout(false);
            this.grpFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorAtenciones)).EndInit();
            this.bindingNavigatorAtenciones.ResumeLayout(false);
            this.bindingNavigatorAtenciones.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uGridCuentas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel Form1_Fill_Panel;
        private Infragistics.Win.UltraWinGrid.UltraGrid uGridCuentas;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Infragistics.Win.UltraWinToolTip.UltraToolTipManager uToolTip;
        private System.Windows.Forms.LinkLabel lLblNinguno;
        private System.Windows.Forms.LinkLabel lLblTodos;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.BindingNavigator bindingNavigatorAtenciones;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripButton btnGenerar;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripDropDownButton btnImprimir;
        private System.Windows.Forms.ToolStripMenuItem atenciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem honorariosDiariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Infragistics.Win.Misc.UltraGroupBox grpFechas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroDesde;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.ComboBox cmbConvenioPago;
        private System.Windows.Forms.Label lblFiltroTipoFormaPago;
        private System.Windows.Forms.ComboBox cb_seguros;
        private System.Windows.Forms.CheckBox chkSinFiltroFechas;
    }
}