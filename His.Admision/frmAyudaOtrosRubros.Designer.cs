namespace His.Admision
{
    partial class frmAyudaProductos
    {//// <summary>
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
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
            this.lblRubros = new System.Windows.Forms.Label();
            this.btn_Buscar = new System.Windows.Forms.Button();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.gbxDetalleHonorarios = new System.Windows.Forms.GroupBox();
            this.dgvDetalleMedicos = new System.Windows.Forms.DataGridView();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardarMedicos = new System.Windows.Forms.Button();
            this.txtValorUnitario = new System.Windows.Forms.TextBox();
            this.txt_Cantidad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVale = new System.Windows.Forms.TextBox();
            this.cmb_Mostrar = new System.Windows.Forms.ComboBox();
            this.btn_Anadir = new System.Windows.Forms.Button();
            this.txt_Nombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridProductos = new System.Windows.Forms.DataGridView();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.btn_Aceptar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Observaciones = new System.Windows.Forms.TextBox();
            this.gridProductosSeleccionados = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.gbxDetalleHonorarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleMedicos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductosSeleccionados)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRubros
            // 
            this.lblRubros.AutoSize = true;
            this.lblRubros.BackColor = System.Drawing.Color.Transparent;
            this.lblRubros.Location = new System.Drawing.Point(537, 330);
            this.lblRubros.Name = "lblRubros";
            this.lblRubros.Size = new System.Drawing.Size(55, 13);
            this.lblRubros.TabIndex = 82;
            this.lblRubros.Text = "Cantidad :";
            // 
            // btn_Buscar
            // 
            this.btn_Buscar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Buscar.Location = new System.Drawing.Point(490, 24);
            this.btn_Buscar.Name = "btn_Buscar";
            this.btn_Buscar.Size = new System.Drawing.Size(55, 23);
            this.btn_Buscar.TabIndex = 84;
            this.btn_Buscar.Text = "Buscar";
            this.btn_Buscar.UseVisualStyleBackColor = false;
            this.btn_Buscar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox1.Controls.Add(this.gbxDetalleHonorarios);
            this.ultraGroupBox1.Controls.Add(this.txtValorUnitario);
            this.ultraGroupBox1.Controls.Add(this.txt_Cantidad);
            this.ultraGroupBox1.Controls.Add(this.label5);
            this.ultraGroupBox1.Controls.Add(this.label4);
            this.ultraGroupBox1.Controls.Add(this.label3);
            this.ultraGroupBox1.Controls.Add(this.txtVale);
            this.ultraGroupBox1.Controls.Add(this.cmb_Mostrar);
            this.ultraGroupBox1.Controls.Add(this.btn_Anadir);
            this.ultraGroupBox1.Controls.Add(this.txt_Nombre);
            this.ultraGroupBox1.Controls.Add(this.lblRubros);
            this.ultraGroupBox1.Controls.Add(this.btn_Buscar);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Controls.Add(this.gridProductos);
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 1);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(839, 374);
            this.ultraGroupBox1.TabIndex = 88;
            this.ultraGroupBox1.Text = "Productos Disponibles";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // gbxDetalleHonorarios
            // 
            this.gbxDetalleHonorarios.Controls.Add(this.dgvDetalleMedicos);
            this.gbxDetalleHonorarios.Controls.Add(this.btnSalir);
            this.gbxDetalleHonorarios.Controls.Add(this.btnEliminar);
            this.gbxDetalleHonorarios.Controls.Add(this.btnGuardarMedicos);
            this.gbxDetalleHonorarios.Location = new System.Drawing.Point(81, 124);
            this.gbxDetalleHonorarios.Name = "gbxDetalleHonorarios";
            this.gbxDetalleHonorarios.Size = new System.Drawing.Size(684, 180);
            this.gbxDetalleHonorarios.TabIndex = 105;
            this.gbxDetalleHonorarios.TabStop = false;
            this.gbxDetalleHonorarios.Text = "Detalle Medicos";
            this.gbxDetalleHonorarios.Visible = false;
            // 
            // dgvDetalleMedicos
            // 
            this.dgvDetalleMedicos.AllowUserToAddRows = false;
            this.dgvDetalleMedicos.AllowUserToDeleteRows = false;
            this.dgvDetalleMedicos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleMedicos.Location = new System.Drawing.Point(21, 19);
            this.dgvDetalleMedicos.Name = "dgvDetalleMedicos";
            this.dgvDetalleMedicos.Size = new System.Drawing.Size(654, 126);
            this.dgvDetalleMedicos.TabIndex = 100;
            this.dgvDetalleMedicos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDetalleMedicos_KeyDown);
            this.dgvDetalleMedicos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvDetalleMedicos_KeyPress);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(600, 151);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(517, 151);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardarMedicos
            // 
            this.btnGuardarMedicos.Location = new System.Drawing.Point(436, 151);
            this.btnGuardarMedicos.Name = "btnGuardarMedicos";
            this.btnGuardarMedicos.Size = new System.Drawing.Size(75, 23);
            this.btnGuardarMedicos.TabIndex = 1;
            this.btnGuardarMedicos.Text = "Guardar";
            this.btnGuardarMedicos.UseVisualStyleBackColor = true;
            this.btnGuardarMedicos.Click += new System.EventHandler(this.btnGuardarMedicos_Click);
            // 
            // txtValorUnitario
            // 
            this.txtValorUnitario.Location = new System.Drawing.Point(695, 327);
            this.txtValorUnitario.Name = "txtValorUnitario";
            this.txtValorUnitario.Size = new System.Drawing.Size(70, 20);
            this.txtValorUnitario.TabIndex = 5000;
            this.txtValorUnitario.Visible = false;
            this.txtValorUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorUnitario_KeyPress);
            // 
            // txt_Cantidad
            // 
            this.txt_Cantidad.Location = new System.Drawing.Point(598, 326);
            this.txt_Cantidad.Name = "txt_Cantidad";
            this.txt_Cantidad.Size = new System.Drawing.Size(49, 20);
            this.txt_Cantidad.TabIndex = 103;
            this.txt_Cantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Cantidad_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(653, 331);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 101;
            this.label5.Text = "Valor :";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(139, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 100;
            this.label4.Text = "Filtro :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(308, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 96;
            this.label3.Text = "Nro. Factura :";
            // 
            // txtVale
            // 
            this.txtVale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVale.Location = new System.Drawing.Point(384, 324);
            this.txtVale.Multiline = true;
            this.txtVale.Name = "txtVale";
            this.txtVale.Size = new System.Drawing.Size(147, 21);
            this.txtVale.TabIndex = 95;
            this.txtVale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVale_KeyPress);
            // 
            // cmb_Mostrar
            // 
            this.cmb_Mostrar.FormattingEnabled = true;
            this.cmb_Mostrar.Location = new System.Drawing.Point(66, 25);
            this.cmb_Mostrar.Name = "cmb_Mostrar";
            this.cmb_Mostrar.Size = new System.Drawing.Size(48, 21);
            this.cmb_Mostrar.TabIndex = 93;
            this.cmb_Mostrar.Visible = false;
            // 
            // btn_Anadir
            // 
            this.btn_Anadir.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Anadir.Location = new System.Drawing.Point(774, 326);
            this.btn_Anadir.Name = "btn_Anadir";
            this.btn_Anadir.Size = new System.Drawing.Size(55, 21);
            this.btn_Anadir.TabIndex = 104;
            this.btn_Anadir.Text = "Añadir";
            this.btn_Anadir.UseVisualStyleBackColor = false;
            this.btn_Anadir.Click += new System.EventHandler(this.btn_Anadir_Click);
            // 
            // txt_Nombre
            // 
            this.txt_Nombre.Location = new System.Drawing.Point(180, 26);
            this.txt_Nombre.Name = "txt_Nombre";
            this.txt_Nombre.Size = new System.Drawing.Size(304, 20);
            this.txt_Nombre.TabIndex = 91;
            this.txt_Nombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Nombre_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 86;
            this.label1.Text = "Mostrar :";
            this.label1.Visible = false;
            // 
            // gridProductos
            // 
            this.gridProductos.AllowUserToAddRows = false;
            this.gridProductos.AllowUserToDeleteRows = false;
            this.gridProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProductos.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.gridProductos.Location = new System.Drawing.Point(12, 64);
            this.gridProductos.Name = "gridProductos";
            this.gridProductos.ReadOnly = true;
            this.gridProductos.Size = new System.Drawing.Size(814, 251);
            this.gridProductos.TabIndex = 106;
            this.gridProductos.TabStop = false;
            this.gridProductos.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridProductos_CellEnter);
            this.gridProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridProductos_KeyDown);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.btn_Cancelar);
            this.ultraGroupBox2.Controls.Add(this.btn_Aceptar);
            this.ultraGroupBox2.Controls.Add(this.label2);
            this.ultraGroupBox2.Controls.Add(this.txt_Observaciones);
            this.ultraGroupBox2.Controls.Add(this.gridProductosSeleccionados);
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 367);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(839, 328);
            this.ultraGroupBox2.TabIndex = 89;
            this.ultraGroupBox2.Text = "Productos Solicitados";
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Cancelar.Location = new System.Drawing.Point(766, 296);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(61, 23);
            this.btn_Cancelar.TabIndex = 94;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = false;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // btn_Aceptar
            // 
            this.btn_Aceptar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Aceptar.Location = new System.Drawing.Point(695, 296);
            this.btn_Aceptar.Name = "btn_Aceptar";
            this.btn_Aceptar.Size = new System.Drawing.Size(61, 23);
            this.btn_Aceptar.TabIndex = 93;
            this.btn_Aceptar.Text = "Aceptar";
            this.btn_Aceptar.UseVisualStyleBackColor = false;
            this.btn_Aceptar.Click += new System.EventHandler(this.btn_Aceptar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 95;
            this.label2.Text = "Observaciones :";
            // 
            // txt_Observaciones
            // 
            this.txt_Observaciones.Location = new System.Drawing.Point(114, 251);
            this.txt_Observaciones.Multiline = true;
            this.txt_Observaciones.Name = "txt_Observaciones";
            this.txt_Observaciones.Size = new System.Drawing.Size(713, 39);
            this.txt_Observaciones.TabIndex = 90;
            // 
            // gridProductosSeleccionados
            // 
            this.gridProductosSeleccionados.AllowDrop = true;
            this.gridProductosSeleccionados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridProductosSeleccionados.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            ultraGridBand1.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.gridProductosSeleccionados.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.gridProductosSeleccionados.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.gridProductosSeleccionados.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.gridProductosSeleccionados.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridProductosSeleccionados.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.gridProductosSeleccionados.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.gridProductosSeleccionados.DisplayLayout.GroupByBox.Hidden = true;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridProductosSeleccionados.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.gridProductosSeleccionados.DisplayLayout.MaxColScrollRegions = 1;
            this.gridProductosSeleccionados.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridProductosSeleccionados.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridProductosSeleccionados.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.gridProductosSeleccionados.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.gridProductosSeleccionados.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.gridProductosSeleccionados.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.gridProductosSeleccionados.DisplayLayout.Override.CellAppearance = appearance8;
            this.gridProductosSeleccionados.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.gridProductosSeleccionados.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.gridProductosSeleccionados.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.gridProductosSeleccionados.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.gridProductosSeleccionados.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.gridProductosSeleccionados.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.gridProductosSeleccionados.DisplayLayout.Override.RowAppearance = appearance11;
            this.gridProductosSeleccionados.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gridProductosSeleccionados.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.gridProductosSeleccionados.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.False;
            this.gridProductosSeleccionados.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Inset;
            this.gridProductosSeleccionados.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.gridProductosSeleccionados.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.gridProductosSeleccionados.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.gridProductosSeleccionados.ExitEditModeOnLeave = false;
            this.gridProductosSeleccionados.Location = new System.Drawing.Point(6, 19);
            this.gridProductosSeleccionados.Name = "gridProductosSeleccionados";
            this.gridProductosSeleccionados.Size = new System.Drawing.Size(821, 227);
            this.gridProductosSeleccionados.TabIndex = 89;
            this.gridProductosSeleccionados.Text = "ultraGridDetallefactura";
            this.gridProductosSeleccionados.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridProductosSeleccionados_InitializeLayout_1);
            this.gridProductosSeleccionados.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.gridProductosSeleccionados_BeforeRowsDeleted);
            this.gridProductosSeleccionados.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridProductosSeleccionados_KeyUp);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Código";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Descripción";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Vencimiento";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Fecha Banco";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "N.Cuenta/Tarjeta";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "N. Cheque/Lote/Ret";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Nombre Dueño";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Autorización";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // frmAyudaProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(841, 690);
            this.ControlBox = false;
            this.Controls.Add(this.ultraGroupBox2);
            this.Controls.Add(this.ultraGroupBox1);
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "frmAyudaProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda Productos";
            this.TransparencyKey = System.Drawing.Color.Silver;
            this.Activated += new System.EventHandler(this.frmAyudaProductos_Activated);
            this.Load += new System.EventHandler(this.frmAyudaProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            this.gbxDetalleHonorarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleMedicos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductosSeleccionados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Label lblRubros;
        private System.Windows.Forms.Button btn_Buscar;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.TextBox txt_Nombre;
        private System.Windows.Forms.Button btn_Anadir;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridProductosSeleccionados;
        private System.Windows.Forms.Button btn_Aceptar;
        private System.Windows.Forms.Button btn_Cancelar;
        private System.Windows.Forms.TextBox txt_Observaciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Cantidad;
        private System.Windows.Forms.TextBox txtValorUnitario;
        private System.Windows.Forms.GroupBox gbxDetalleHonorarios;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardarMedicos;
        private System.Windows.Forms.DataGridView dgvDetalleMedicos;
        private System.Windows.Forms.DataGridView gridProductos;
        private System.Windows.Forms.ComboBox cmb_Mostrar;
        private System.Windows.Forms.Label label1;
    }
}