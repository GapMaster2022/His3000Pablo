namespace TarifariosUI
{
    partial class frmBusquedaTarifario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusquedaTarifario));
            this.panelMarcoo = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnExportarExcel = new System.Windows.Forms.ToolStripButton();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.lblBusqueda = new System.Windows.Forms.Label();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvwHonorarios = new System.Windows.Forms.ListView();
            this.lblTarifas = new System.Windows.Forms.Label();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.especialidadesPanel = new System.Windows.Forms.Panel();
            this.tvEspecialidades = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.opcionesBusqueda = new System.Windows.Forms.GroupBox();
            this.optDescripcion = new System.Windows.Forms.RadioButton();
            this.optCodigo = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.filtroPanel = new System.Windows.Forms.Panel();
            this.lblTarifario = new System.Windows.Forms.Label();
            this.tarifarioList = new System.Windows.Forms.ComboBox();
            this.txtPaciente = new System.Windows.Forms.TextBox();
            this.aseguradoraList = new System.Windows.Forms.ComboBox();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.lblAseguradora = new System.Windows.Forms.Label();
            this.panelEstructura = new System.Windows.Forms.Panel();
            this.tarifariosDetalleGrid = new System.Windows.Forms.DataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.gbTipo = new System.Windows.Forms.GroupBox();
            this.optAnestesia = new System.Windows.Forms.RadioButton();
            this.optUvr = new System.Windows.Forms.RadioButton();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.lblValorGenerar = new System.Windows.Forms.Label();
            this.btnAniadir = new System.Windows.Forms.Button();
            this.panelMarcoo.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.especialidadesPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.opcionesBusqueda.SuspendLayout();
            this.filtroPanel.SuspendLayout();
            this.panelEstructura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tarifariosDetalleGrid)).BeginInit();
            this.panel4.SuspendLayout();
            this.gbTipo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMarcoo
            // 
            this.panelMarcoo.BackColor = System.Drawing.Color.GhostWhite;
            this.panelMarcoo.Controls.Add(this.btnSalir);
            this.panelMarcoo.Controls.Add(this.toolStrip1);
            this.panelMarcoo.Controls.Add(this.lblFiltro);
            this.panelMarcoo.Controls.Add(this.lblBusqueda);
            this.panelMarcoo.Controls.Add(this.lblDetalle);
            this.panelMarcoo.Controls.Add(this.panel1);
            this.panelMarcoo.Controls.Add(this.lblTarifas);
            this.panelMarcoo.Controls.Add(this.lblEspecialidad);
            this.panelMarcoo.Controls.Add(this.especialidadesPanel);
            this.panelMarcoo.Controls.Add(this.panel3);
            this.panelMarcoo.Controls.Add(this.filtroPanel);
            this.panelMarcoo.Controls.Add(this.panelEstructura);
            this.panelMarcoo.Location = new System.Drawing.Point(1, 1);
            this.panelMarcoo.Name = "panelMarcoo";
            this.panelMarcoo.Size = new System.Drawing.Size(873, 634);
            this.panelMarcoo.TabIndex = 0;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnSalir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalir.Location = new System.Drawing.Point(766, 601);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(91, 22);
            this.btnSalir.TabIndex = 18;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnImprimir,
            this.btnExportarExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(873, 25);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(23, 22);
            this.btnImprimir.Text = "Imprimir Honorarios";
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportarExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportarExcel.Image")));
            this.btnExportarExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(23, 22);
            this.btnExportarExcel.Text = "Exportar a Excel";
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(32)))), ((int)(((byte)(111)))));
            this.lblFiltro.Location = new System.Drawing.Point(21, 125);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(32, 12);
            this.lblFiltro.TabIndex = 15;
            this.lblFiltro.Text = "Filtro";
            // 
            // lblBusqueda
            // 
            this.lblBusqueda.AutoSize = true;
            this.lblBusqueda.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.lblBusqueda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(49)))));
            this.lblBusqueda.Location = new System.Drawing.Point(13, 30);
            this.lblBusqueda.Name = "lblBusqueda";
            this.lblBusqueda.Size = new System.Drawing.Size(53, 12);
            this.lblBusqueda.TabIndex = 14;
            this.lblBusqueda.Text = "Busqueda";
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDetalle.Location = new System.Drawing.Point(333, 435);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(154, 16);
            this.lblDetalle.TabIndex = 12;
            this.lblDetalle.Text = "HONORARIOS MEDICOS";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lvwHonorarios);
            this.panel1.Location = new System.Drawing.Point(11, 454);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 141);
            this.panel1.TabIndex = 11;
            // 
            // lvwHonorarios
            // 
            this.lvwHonorarios.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwHonorarios.Location = new System.Drawing.Point(3, 3);
            this.lvwHonorarios.Name = "lvwHonorarios";
            this.lvwHonorarios.Size = new System.Drawing.Size(835, 133);
            this.lvwHonorarios.TabIndex = 0;
            this.lvwHonorarios.UseCompatibleStateImageBehavior = false;
            // 
            // lblTarifas
            // 
            this.lblTarifas.AutoSize = true;
            this.lblTarifas.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.lblTarifas.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTarifas.Location = new System.Drawing.Point(249, 244);
            this.lblTarifas.Name = "lblTarifas";
            this.lblTarifas.Size = new System.Drawing.Size(89, 12);
            this.lblTarifas.TabIndex = 10;
            this.lblTarifas.Text = "Detalle Tarifario";
            // 
            // lblEspecialidad
            // 
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.lblEspecialidad.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblEspecialidad.Location = new System.Drawing.Point(21, 244);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(66, 12);
            this.lblEspecialidad.TabIndex = 9;
            this.lblEspecialidad.Text = "Especialidad";
            // 
            // especialidadesPanel
            // 
            this.especialidadesPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.especialidadesPanel.Controls.Add(this.tvEspecialidades);
            this.especialidadesPanel.Location = new System.Drawing.Point(11, 259);
            this.especialidadesPanel.Name = "especialidadesPanel";
            this.especialidadesPanel.Size = new System.Drawing.Size(216, 173);
            this.especialidadesPanel.TabIndex = 8;
            // 
            // tvEspecialidades
            // 
            this.tvEspecialidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tvEspecialidades.Location = new System.Drawing.Point(4, 3);
            this.tvEspecialidades.Name = "tvEspecialidades";
            this.tvEspecialidades.Size = new System.Drawing.Size(209, 167);
            this.tvEspecialidades.TabIndex = 0;
            this.tvEspecialidades.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvEspecialidades_AfterSelect);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.opcionesBusqueda);
            this.panel3.Controls.Add(this.btnBuscar);
            this.panel3.Controls.Add(this.txtBuscar);
            this.panel3.Location = new System.Drawing.Point(11, 45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(263, 74);
            this.panel3.TabIndex = 7;
            // 
            // opcionesBusqueda
            // 
            this.opcionesBusqueda.Controls.Add(this.optDescripcion);
            this.opcionesBusqueda.Controls.Add(this.optCodigo);
            this.opcionesBusqueda.Location = new System.Drawing.Point(11, 37);
            this.opcionesBusqueda.Margin = new System.Windows.Forms.Padding(2);
            this.opcionesBusqueda.Name = "opcionesBusqueda";
            this.opcionesBusqueda.Padding = new System.Windows.Forms.Padding(2);
            this.opcionesBusqueda.Size = new System.Drawing.Size(239, 29);
            this.opcionesBusqueda.TabIndex = 1;
            this.opcionesBusqueda.TabStop = false;
            // 
            // optDescripcion
            // 
            this.optDescripcion.AutoSize = true;
            this.optDescripcion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDescripcion.Location = new System.Drawing.Point(122, 9);
            this.optDescripcion.Name = "optDescripcion";
            this.optDescripcion.Size = new System.Drawing.Size(98, 17);
            this.optDescripcion.TabIndex = 2;
            this.optDescripcion.Text = "Por Descripción";
            this.optDescripcion.UseVisualStyleBackColor = true;
            // 
            // optCodigo
            // 
            this.optCodigo.AutoSize = true;
            this.optCodigo.Checked = true;
            this.optCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCodigo.Location = new System.Drawing.Point(10, 9);
            this.optCodigo.Name = "optCodigo";
            this.optCodigo.Size = new System.Drawing.Size(96, 17);
            this.optCodigo.TabIndex = 1;
            this.optCodigo.TabStop = true;
            this.optCodigo.Text = "Por Referencia";
            this.optCodigo.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnBuscar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBuscar.Location = new System.Drawing.Point(171, 10);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(79, 25);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBuscar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(11, 10);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(151, 22);
            this.txtBuscar.TabIndex = 0;
            // 
            // filtroPanel
            // 
            this.filtroPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.filtroPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filtroPanel.Controls.Add(this.lblTarifario);
            this.filtroPanel.Controls.Add(this.tarifarioList);
            this.filtroPanel.Controls.Add(this.txtPaciente);
            this.filtroPanel.Controls.Add(this.aseguradoraList);
            this.filtroPanel.Controls.Add(this.lblPaciente);
            this.filtroPanel.Controls.Add(this.lblAseguradora);
            this.filtroPanel.Location = new System.Drawing.Point(11, 140);
            this.filtroPanel.Name = "filtroPanel";
            this.filtroPanel.Size = new System.Drawing.Size(476, 94);
            this.filtroPanel.TabIndex = 6;
            // 
            // lblTarifario
            // 
            this.lblTarifario.AutoSize = true;
            this.lblTarifario.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarifario.Location = new System.Drawing.Point(18, 9);
            this.lblTarifario.Name = "lblTarifario";
            this.lblTarifario.Size = new System.Drawing.Size(111, 13);
            this.lblTarifario.TabIndex = 9;
            this.lblTarifario.Text = "Seleccione el Tarifario";
            // 
            // tarifarioList
            // 
            this.tarifarioList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tarifarioList.FormattingEnabled = true;
            this.tarifarioList.Location = new System.Drawing.Point(157, 6);
            this.tarifarioList.Name = "tarifarioList";
            this.tarifarioList.Size = new System.Drawing.Size(306, 21);
            this.tarifarioList.TabIndex = 8;
            this.tarifarioList.SelectedIndexChanged += new System.EventHandler(this.tarifarioList_SelectedIndexChanged);
            // 
            // txtPaciente
            // 
            this.txtPaciente.AutoCompleteCustomSource.AddRange(new string[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "6",
            "8",
            "9"});
            this.txtPaciente.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaciente.Location = new System.Drawing.Point(157, 62);
            this.txtPaciente.Name = "txtPaciente";
            this.txtPaciente.Size = new System.Drawing.Size(58, 21);
            this.txtPaciente.TabIndex = 6;
            // 
            // aseguradoraList
            // 
            this.aseguradoraList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aseguradoraList.FormattingEnabled = true;
            this.aseguradoraList.Location = new System.Drawing.Point(157, 35);
            this.aseguradoraList.Name = "aseguradoraList";
            this.aseguradoraList.Size = new System.Drawing.Size(306, 21);
            this.aseguradoraList.TabIndex = 5;
            this.aseguradoraList.SelectedIndexChanged += new System.EventHandler(this.aseguradoraList_SelectedIndexChanged);
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaciente.Location = new System.Drawing.Point(18, 65);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(74, 13);
            this.lblPaciente.TabIndex = 3;
            this.lblPaciente.Text = "Nivel Paciente";
            // 
            // lblAseguradora
            // 
            this.lblAseguradora.AutoSize = true;
            this.lblAseguradora.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAseguradora.Location = new System.Drawing.Point(18, 38);
            this.lblAseguradora.Name = "lblAseguradora";
            this.lblAseguradora.Size = new System.Drawing.Size(133, 13);
            this.lblAseguradora.TabIndex = 1;
            this.lblAseguradora.Text = "Seleccione la Aseguradora";
            // 
            // panelEstructura
            // 
            this.panelEstructura.BackColor = System.Drawing.Color.Gainsboro;
            this.panelEstructura.Controls.Add(this.panel4);
            this.panelEstructura.Controls.Add(this.tarifariosDetalleGrid);
            this.panelEstructura.Location = new System.Drawing.Point(233, 259);
            this.panelEstructura.Name = "panelEstructura";
            this.panelEstructura.Size = new System.Drawing.Size(624, 173);
            this.panelEstructura.TabIndex = 2;
            // 
            // tarifariosDetalleGrid
            // 
            this.tarifariosDetalleGrid.AllowUserToAddRows = false;
            this.tarifariosDetalleGrid.AllowUserToDeleteRows = false;
            this.tarifariosDetalleGrid.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.tarifariosDetalleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tarifariosDetalleGrid.Location = new System.Drawing.Point(3, 3);
            this.tarifariosDetalleGrid.Name = "tarifariosDetalleGrid";
            this.tarifariosDetalleGrid.Size = new System.Drawing.Size(618, 121);
            this.tarifariosDetalleGrid.TabIndex = 0;
            this.tarifariosDetalleGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tarifariosDetalleGrid_CellDoubleClick);
            this.tarifariosDetalleGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tarifariosDetalleGrid_CellContentClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Document-01.ico");
            this.imageList1.Images.SetKeyName(1, "Document Blank-01.ico");
            this.imageList1.Images.SetKeyName(2, "Document Microsoft Word-01.ico");
            this.imageList1.Images.SetKeyName(3, "Excel.ico");
            this.imageList1.Images.SetKeyName(4, "Binoculars-01.ico");
            this.imageList1.Images.SetKeyName(5, "Refresh.ico");
            this.imageList1.Images.SetKeyName(6, "Document Preview-01.ico");
            this.imageList1.Images.SetKeyName(7, "Edit Document-01.ico");
            this.imageList1.Images.SetKeyName(8, "File New-01.ico");
            this.imageList1.Images.SetKeyName(9, "File Open-01.ico");
            this.imageList1.Images.SetKeyName(10, "Save.ico");
            this.imageList1.Images.SetKeyName(11, "Text Edit.ico");
            this.imageList1.Images.SetKeyName(12, "Window Refresh-01.ico");
            this.imageList1.Images.SetKeyName(13, "printer.ico");
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gainsboro;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblCantidad);
            this.panel4.Controls.Add(this.gbTipo);
            this.panel4.Controls.Add(this.txtCantidad);
            this.panel4.Controls.Add(this.lblValorGenerar);
            this.panel4.Controls.Add(this.btnAniadir);
            this.panel4.Location = new System.Drawing.Point(3, 130);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(442, 37);
            this.panel4.TabIndex = 25;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(243, 13);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(49, 13);
            this.lblCantidad.TabIndex = 7;
            this.lblCantidad.Text = "Cantidad";
            // 
            // gbTipo
            // 
            this.gbTipo.Controls.Add(this.optAnestesia);
            this.gbTipo.Controls.Add(this.optUvr);
            this.gbTipo.Location = new System.Drawing.Point(99, 2);
            this.gbTipo.Name = "gbTipo";
            this.gbTipo.Size = new System.Drawing.Size(138, 28);
            this.gbTipo.TabIndex = 6;
            this.gbTipo.TabStop = false;
            // 
            // optAnestesia
            // 
            this.optAnestesia.AutoSize = true;
            this.optAnestesia.Location = new System.Drawing.Point(61, 8);
            this.optAnestesia.Name = "optAnestesia";
            this.optAnestesia.Size = new System.Drawing.Size(71, 17);
            this.optAnestesia.TabIndex = 2;
            this.optAnestesia.TabStop = true;
            this.optAnestesia.Text = "Anestesia";
            this.optAnestesia.UseVisualStyleBackColor = true;
            // 
            // optUvr
            // 
            this.optUvr.AutoSize = true;
            this.optUvr.Checked = true;
            this.optUvr.Location = new System.Drawing.Point(6, 8);
            this.optUvr.Name = "optUvr";
            this.optUvr.Size = new System.Drawing.Size(48, 17);
            this.optUvr.TabIndex = 0;
            this.optUvr.TabStop = true;
            this.optUvr.Text = "UVR";
            this.optUvr.UseVisualStyleBackColor = true;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(301, 10);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(41, 20);
            this.txtCantidad.TabIndex = 5;
            this.txtCantidad.Text = "1";
            // 
            // lblValorGenerar
            // 
            this.lblValorGenerar.AutoSize = true;
            this.lblValorGenerar.Location = new System.Drawing.Point(9, 13);
            this.lblValorGenerar.Name = "lblValorGenerar";
            this.lblValorGenerar.Size = new System.Drawing.Size(79, 13);
            this.lblValorGenerar.TabIndex = 3;
            this.lblValorGenerar.Text = "Valor a generar";
            // 
            // btnAniadir
            // 
            this.btnAniadir.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnAniadir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAniadir.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAniadir.Image = ((System.Drawing.Image)(resources.GetObject("btnAniadir.Image")));
            this.btnAniadir.Location = new System.Drawing.Point(356, 7);
            this.btnAniadir.Name = "btnAniadir";
            this.btnAniadir.Size = new System.Drawing.Size(70, 24);
            this.btnAniadir.TabIndex = 2;
            this.btnAniadir.Text = "Añadir";
            this.btnAniadir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAniadir.UseVisualStyleBackColor = false;
            this.btnAniadir.Click += new System.EventHandler(this.btnAniadir_Click);
            // 
            // frmBusquedaTarifario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(870, 636);
            this.Controls.Add(this.panelMarcoo);
            this.MaximizeBox = false;
            this.Name = "frmBusquedaTarifario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBusquedaTarifario";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBusquedaTarifario_Load);
            this.panelMarcoo.ResumeLayout(false);
            this.panelMarcoo.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.especialidadesPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.opcionesBusqueda.ResumeLayout(false);
            this.opcionesBusqueda.PerformLayout();
            this.filtroPanel.ResumeLayout(false);
            this.filtroPanel.PerformLayout();
            this.panelEstructura.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tarifariosDetalleGrid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.gbTipo.ResumeLayout(false);
            this.gbTipo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMarcoo;
        private System.Windows.Forms.Label lblAseguradora;
        private System.Windows.Forms.Panel panelEstructura;
        private System.Windows.Forms.Panel filtroPanel;
        private System.Windows.Forms.ComboBox aseguradoraList;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.DataGridView tarifariosDetalleGrid;
        private System.Windows.Forms.Panel especialidadesPanel;
        private System.Windows.Forms.TreeView tvEspecialidades;
        private System.Windows.Forms.TextBox txtPaciente;
        private System.Windows.Forms.Label lblEspecialidad;
        private System.Windows.Forms.ComboBox tarifarioList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTarifas;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.ListView lvwHonorarios;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox opcionesBusqueda;
        private System.Windows.Forms.RadioButton optDescripcion;
        private System.Windows.Forms.RadioButton optCodigo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.Label lblBusqueda;
        private System.Windows.Forms.Label lblTarifario;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.ToolStripButton btnExportarExcel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.GroupBox gbTipo;
        private System.Windows.Forms.RadioButton optAnestesia;
        private System.Windows.Forms.RadioButton optUvr;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lblValorGenerar;
        private System.Windows.Forms.Button btnAniadir;
    }
}