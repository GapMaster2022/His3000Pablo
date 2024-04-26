namespace His.Dietetica
{
    partial class frmquirofanopedidoadicional
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmquirofanopedidoadicional));
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.toolStripImprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
            this.lblejemplo = new System.Windows.Forms.Label();
            this.lblejemplo2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSolicitar = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TablaPedidos = new System.Windows.Forms.DataGridView();
            this.codpro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelPaciente = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtcantidad = new System.Windows.Forms.TextBox();
            this.UltraGridProductos = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.tpTickets = new System.Windows.Forms.TabPage();
            this.TablaTickets = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tools.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSolicitar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaPedidos)).BeginInit();
            this.panelPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridProductos)).BeginInit();
            this.tpTickets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaTickets)).BeginInit();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGuardar,
            this.toolStripImprimir,
            this.toolStripSeparator1,
            this.toolStripButtonSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(827, 45);
            this.tools.TabIndex = 80;
            this.tools.Text = "toolStrip1";
            this.tools.TabIndexChanged += new System.EventHandler(this.tools_TabIndexChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.AutoSize = false;
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(42, 42);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTipText = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // toolStripImprimir
            // 
            this.toolStripImprimir.AutoSize = false;
            this.toolStripImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripImprimir.Image = ((System.Drawing.Image)(resources.GetObject("toolStripImprimir.Image")));
            this.toolStripImprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripImprimir.Name = "toolStripImprimir";
            this.toolStripImprimir.Size = new System.Drawing.Size(42, 42);
            this.toolStripImprimir.Text = "Imprimir";
            this.toolStripImprimir.ToolTipText = "Imprimir";
            this.toolStripImprimir.Click += new System.EventHandler(this.toolStripImprimir_Click);
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
            // lblejemplo
            // 
            this.lblejemplo.AutoSize = true;
            this.lblejemplo.Location = new System.Drawing.Point(201, 32);
            this.lblejemplo.Name = "lblejemplo";
            this.lblejemplo.Size = new System.Drawing.Size(35, 13);
            this.lblejemplo.TabIndex = 81;
            this.lblejemplo.Text = "label1";
            this.lblejemplo.Visible = false;
            // 
            // lblejemplo2
            // 
            this.lblejemplo2.AutoSize = true;
            this.lblejemplo2.Location = new System.Drawing.Point(554, 174);
            this.lblejemplo2.Name = "lblejemplo2";
            this.lblejemplo2.Size = new System.Drawing.Size(61, 13);
            this.lblejemplo2.TabIndex = 82;
            this.lblejemplo2.Text = "Cantidad:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSolicitar);
            this.tabControl1.Controls.Add(this.tpTickets);
            this.tabControl1.Location = new System.Drawing.Point(0, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(827, 411);
            this.tabControl1.TabIndex = 83;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpSolicitar
            // 
            this.tpSolicitar.Controls.Add(this.groupBox1);
            this.tpSolicitar.Controls.Add(this.panelPaciente);
            this.tpSolicitar.Location = new System.Drawing.Point(4, 22);
            this.tpSolicitar.Name = "tpSolicitar";
            this.tpSolicitar.Padding = new System.Windows.Forms.Padding(3);
            this.tpSolicitar.Size = new System.Drawing.Size(819, 385);
            this.tpSolicitar.TabIndex = 0;
            this.tpSolicitar.Text = "SOLICITAR";
            this.tpSolicitar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(253)))));
            this.groupBox1.Controls.Add(this.TablaPedidos);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 212);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(805, 177);
            this.groupBox1.TabIndex = 115;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Productos Solicitados";
            // 
            // TablaPedidos
            // 
            this.TablaPedidos.AllowUserToAddRows = false;
            this.TablaPedidos.AllowUserToDeleteRows = false;
            this.TablaPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaPedidos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablaPedidos.BackgroundColor = System.Drawing.Color.White;
            this.TablaPedidos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaPedidos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TablaPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaPedidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codpro,
            this.prodesc,
            this.cant,
            this.pre});
            this.TablaPedidos.EnableHeadersVisualStyles = false;
            this.TablaPedidos.Location = new System.Drawing.Point(6, 19);
            this.TablaPedidos.Name = "TablaPedidos";
            this.TablaPedidos.ReadOnly = true;
            this.TablaPedidos.RowHeadersVisible = false;
            this.TablaPedidos.Size = new System.Drawing.Size(793, 137);
            this.TablaPedidos.TabIndex = 2;
            // 
            // codpro
            // 
            this.codpro.HeaderText = "Código";
            this.codpro.Name = "codpro";
            this.codpro.ReadOnly = true;
            this.codpro.Width = 70;
            // 
            // prodesc
            // 
            this.prodesc.HeaderText = "Producto";
            this.prodesc.Name = "prodesc";
            this.prodesc.ReadOnly = true;
            this.prodesc.Width = 82;
            // 
            // cant
            // 
            this.cant.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cant.FillWeight = 50F;
            this.cant.HeaderText = "Cantidad";
            this.cant.Name = "cant";
            this.cant.ReadOnly = true;
            this.cant.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cant.Width = 81;
            // 
            // pre
            // 
            this.pre.HeaderText = "precio";
            this.pre.Name = "pre";
            this.pre.ReadOnly = true;
            this.pre.Visible = false;
            this.pre.Width = 66;
            // 
            // panelPaciente
            // 
            this.panelPaciente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPaciente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(253)))));
            this.panelPaciente.Controls.Add(this.button1);
            this.panelPaciente.Controls.Add(this.txtcantidad);
            this.panelPaciente.Controls.Add(this.UltraGridProductos);
            this.panelPaciente.Controls.Add(this.lblejemplo2);
            this.panelPaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelPaciente.Location = new System.Drawing.Point(8, 6);
            this.panelPaciente.Name = "panelPaciente";
            this.panelPaciente.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelPaciente.Size = new System.Drawing.Size(805, 200);
            this.panelPaciente.TabIndex = 114;
            this.panelPaciente.TabStop = false;
            this.panelPaciente.Text = "Productos Disponibles";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(706, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 84;
            this.button1.Text = "Añadir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtcantidad
            // 
            this.txtcantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcantidad.Location = new System.Drawing.Point(621, 171);
            this.txtcantidad.Name = "txtcantidad";
            this.txtcantidad.Size = new System.Drawing.Size(79, 20);
            this.txtcantidad.TabIndex = 83;
            this.txtcantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcantidad_KeyDown);
            this.txtcantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // UltraGridProductos
            // 
            this.UltraGridProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.UltraGridProductos.DisplayLayout.Appearance = appearance1;
            this.UltraGridProductos.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.UltraGridProductos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.UltraGridProductos.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.MediumPurple;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.UltraGridProductos.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UltraGridProductos.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.UltraGridProductos.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UltraGridProductos.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.UltraGridProductos.DisplayLayout.MaxColScrollRegions = 1;
            this.UltraGridProductos.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UltraGridProductos.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.UltraGridProductos.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.UltraGridProductos.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.UltraGridProductos.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.UltraGridProductos.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.UltraGridProductos.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.UltraGridProductos.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.UltraGridProductos.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.UltraGridProductos.DisplayLayout.Override.CellAppearance = appearance8;
            this.UltraGridProductos.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.UltraGridProductos.DisplayLayout.Override.CellPadding = 0;
            this.UltraGridProductos.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.EntireColumn;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.UltraGridProductos.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.UltraGridProductos.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.UltraGridProductos.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.UltraGridProductos.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.UltraGridProductos.DisplayLayout.Override.RowAppearance = appearance11;
            this.UltraGridProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.UltraGridProductos.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.UltraGridProductos.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.UltraGridProductos.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.UltraGridProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UltraGridProductos.Location = new System.Drawing.Point(6, 19);
            this.UltraGridProductos.Name = "UltraGridProductos";
            this.UltraGridProductos.Size = new System.Drawing.Size(793, 151);
            this.UltraGridProductos.TabIndex = 82;
            this.UltraGridProductos.Text = "ultraGrid1";
            this.UltraGridProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UltraGridProductos_KeyDown);
            // 
            // tpTickets
            // 
            this.tpTickets.Controls.Add(this.TablaTickets);
            this.tpTickets.Location = new System.Drawing.Point(4, 22);
            this.tpTickets.Name = "tpTickets";
            this.tpTickets.Padding = new System.Windows.Forms.Padding(3);
            this.tpTickets.Size = new System.Drawing.Size(819, 385);
            this.tpTickets.TabIndex = 1;
            this.tpTickets.Text = "TICKETS";
            this.tpTickets.UseVisualStyleBackColor = true;
            // 
            // TablaTickets
            // 
            this.TablaTickets.AllowUserToAddRows = false;
            this.TablaTickets.AllowUserToDeleteRows = false;
            this.TablaTickets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaTickets.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablaTickets.BackgroundColor = System.Drawing.Color.White;
            this.TablaTickets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaTickets.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TablaTickets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.TablaTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaTickets.EnableHeadersVisualStyles = false;
            this.TablaTickets.Location = new System.Drawing.Point(6, 6);
            this.TablaTickets.Name = "TablaTickets";
            this.TablaTickets.ReadOnly = true;
            this.TablaTickets.RowHeadersVisible = false;
            this.TablaTickets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaTickets.Size = new System.Drawing.Size(805, 373);
            this.TablaTickets.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Código";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Producto";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 82;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Stock";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 64;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.FillWeight = 50F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Cantidad";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // frmquirofanopedidoadicional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 462);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblejemplo);
            this.Controls.Add(this.tools);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmquirofanopedidoadicional";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pedido Adicional";
            this.Load += new System.EventHandler(this.frmquirofanopedidoadicional_Load);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpSolicitar.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TablaPedidos)).EndInit();
            this.panelPaciente.ResumeLayout(false);
            this.panelPaciente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridProductos)).EndInit();
            this.tpTickets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TablaTickets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSalir;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Label lblejemplo;
        private System.Windows.Forms.Label lblejemplo2;
        private System.Windows.Forms.ToolStripButton toolStripImprimir;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSolicitar;
        private System.Windows.Forms.TabPage tpTickets;
        private System.Windows.Forms.GroupBox panelPaciente;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtcantidad;
        private Infragistics.Win.UltraWinGrid.UltraGrid UltraGridProductos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView TablaPedidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn codpro;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cant;
        private System.Windows.Forms.DataGridView TablaTickets;
        private System.Windows.Forms.DataGridViewTextBoxColumn pre;
    }
}