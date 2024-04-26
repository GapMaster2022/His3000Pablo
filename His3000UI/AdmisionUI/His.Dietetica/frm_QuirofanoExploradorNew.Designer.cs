namespace His.Dietetica
{
    partial class frm_QuirofanoExploradorNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_QuirofanoExploradorNew));
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
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonTablet = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
            this.UltraGridPacientes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoProcedimientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verProcedimientosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregaProductoUltimoProcedimientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarProcedimientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridPacientes)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripButtonActualizar,
            this.toolStripButtonBuscar,
            this.toolStripSeparator1,
            this.toolStripButtonTablet,
            this.toolStripButtonSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tools.Size = new System.Drawing.Size(913, 45);
            this.tools.TabIndex = 79;
            this.tools.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.AutoSize = false;
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = global::His.Dietetica.Properties.Resources.HIS_REFRESH;
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 42);
            this.btnNuevo.Text = "Actualizar";
            this.btnNuevo.ToolTipText = "Actualizar";
            this.btnNuevo.Click += new System.EventHandler(this.toolStripActualizar_Click);
            // 
            // toolStripButtonActualizar
            // 
            this.toolStripButtonActualizar.AutoSize = false;
            this.toolStripButtonActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonActualizar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonActualizar.Image")));
            this.toolStripButtonActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonActualizar.Name = "toolStripButtonActualizar";
            this.toolStripButtonActualizar.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonActualizar.Text = "Agregar Procedimiento";
            this.toolStripButtonActualizar.ToolTipText = "Agregar Procedimiento";
            this.toolStripButtonActualizar.Visible = false;
            this.toolStripButtonActualizar.Click += new System.EventHandler(this.toolStripButtonActualizar_Click);
            // 
            // toolStripButtonBuscar
            // 
            this.toolStripButtonBuscar.AutoSize = false;
            this.toolStripButtonBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBuscar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBuscar.Image")));
            this.toolStripButtonBuscar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonBuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBuscar.Name = "toolStripButtonBuscar";
            this.toolStripButtonBuscar.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonBuscar.Text = "Exportar";
            this.toolStripButtonBuscar.ToolTipText = "Exportar a excel";
            this.toolStripButtonBuscar.Click += new System.EventHandler(this.toolStripButtonBuscar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButtonTablet
            // 
            this.toolStripButtonTablet.AutoSize = false;
            this.toolStripButtonTablet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTablet.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTablet.Image")));
            this.toolStripButtonTablet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonTablet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTablet.Name = "toolStripButtonTablet";
            this.toolStripButtonTablet.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonTablet.Text = "Modo Tablet";
            this.toolStripButtonTablet.ToolTipText = "Modo Tablet";
            this.toolStripButtonTablet.Click += new System.EventHandler(this.toolStripButtonTablet_Click);
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
            // UltraGridPacientes
            // 
            this.UltraGridPacientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.UltraGridPacientes.DisplayLayout.Appearance = appearance1;
            this.UltraGridPacientes.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.UltraGridPacientes.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.UltraGridPacientes.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.MediumPurple;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.UltraGridPacientes.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UltraGridPacientes.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.UltraGridPacientes.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UltraGridPacientes.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.UltraGridPacientes.DisplayLayout.MaxColScrollRegions = 1;
            this.UltraGridPacientes.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UltraGridPacientes.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.UltraGridPacientes.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.UltraGridPacientes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.UltraGridPacientes.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.UltraGridPacientes.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.UltraGridPacientes.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.UltraGridPacientes.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.UltraGridPacientes.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.UltraGridPacientes.DisplayLayout.Override.CellAppearance = appearance8;
            this.UltraGridPacientes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.UltraGridPacientes.DisplayLayout.Override.CellPadding = 0;
            this.UltraGridPacientes.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.EntireColumn;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.UltraGridPacientes.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.UltraGridPacientes.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.UltraGridPacientes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.UltraGridPacientes.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.UltraGridPacientes.DisplayLayout.Override.RowAppearance = appearance11;
            this.UltraGridPacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.UltraGridPacientes.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.UltraGridPacientes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.UltraGridPacientes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.UltraGridPacientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UltraGridPacientes.Location = new System.Drawing.Point(1, 49);
            this.UltraGridPacientes.Name = "UltraGridPacientes";
            this.UltraGridPacientes.Size = new System.Drawing.Size(1053, 229);
            this.UltraGridPacientes.TabIndex = 81;
            this.UltraGridPacientes.Text = "ultraGrid1";
            this.UltraGridPacientes.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.UltraGridPacientes_InitializeRow);
            this.UltraGridPacientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UltraGridPacientes_KeyDown);
            this.UltraGridPacientes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UltraGridPacientes_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 276);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 51);
            this.panel1.TabIndex = 82;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(946, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 26);
            this.label6.TabIndex = 9;
            this.label6.Text = "Procedimientos\r\nCerrados";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(137)))));
            this.panel6.Location = new System.Drawing.Point(896, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(44, 45);
            this.panel6.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(711, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 26);
            this.label5.TabIndex = 7;
            this.label5.Text = "Varios Procedimientos\r\n1 o varios Cerrados";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(201)))));
            this.panel5.Location = new System.Drawing.Point(661, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(44, 45);
            this.panel5.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(465, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 26);
            this.label4.TabIndex = 5;
            this.label4.Text = "Varios Procedimientos\r\n0 Cerrados";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(245)))), ((int)(((byte)(164)))));
            this.panel4.Location = new System.Drawing.Point(415, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(44, 45);
            this.panel4.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "1 Procedimiento\r\n0 Cerrados";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(210)))), ((int)(((byte)(72)))));
            this.panel3.Location = new System.Drawing.Point(208, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(44, 45);
            this.panel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "0 Procedimientos\r\n0 Cerrados";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(44, 45);
            this.panel2.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoProcedimientoToolStripMenuItem,
            this.verProcedimientosToolStripMenuItem,
            this.modificarProcedimientoToolStripMenuItem,
            this.agregaProductoUltimoProcedimientoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(285, 114);
            // 
            // nuevoProcedimientoToolStripMenuItem
            // 
            this.nuevoProcedimientoToolStripMenuItem.Name = "nuevoProcedimientoToolStripMenuItem";
            this.nuevoProcedimientoToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.nuevoProcedimientoToolStripMenuItem.Text = "Nuevo Procedimiento";
            this.nuevoProcedimientoToolStripMenuItem.Click += new System.EventHandler(this.nuevoProcedimientoToolStripMenuItem_Click);
            // 
            // verProcedimientosToolStripMenuItem
            // 
            this.verProcedimientosToolStripMenuItem.Name = "verProcedimientosToolStripMenuItem";
            this.verProcedimientosToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.verProcedimientosToolStripMenuItem.Text = "Ver Procedimientos (DETALLE)";
            this.verProcedimientosToolStripMenuItem.Click += new System.EventHandler(this.verProcedimientosToolStripMenuItem_Click);
            // 
            // agregaProductoUltimoProcedimientoToolStripMenuItem
            // 
            this.agregaProductoUltimoProcedimientoToolStripMenuItem.Name = "agregaProductoUltimoProcedimientoToolStripMenuItem";
            this.agregaProductoUltimoProcedimientoToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.agregaProductoUltimoProcedimientoToolStripMenuItem.Text = "Agrega Producto Ultimo Procedimiento";
            this.agregaProductoUltimoProcedimientoToolStripMenuItem.Click += new System.EventHandler(this.agregaProductoUltimoProcedimientoToolStripMenuItem_Click);
            // 
            // modificarProcedimientoToolStripMenuItem
            // 
            this.modificarProcedimientoToolStripMenuItem.Name = "modificarProcedimientoToolStripMenuItem";
            this.modificarProcedimientoToolStripMenuItem.Size = new System.Drawing.Size(284, 22);
            this.modificarProcedimientoToolStripMenuItem.Text = "Modificar Procedimiento";
            this.modificarProcedimientoToolStripMenuItem.Click += new System.EventHandler(this.modificarProcedimientoToolStripMenuItem_Click);
            // 
            // frm_QuirofanoExploradorNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 327);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.UltraGridPacientes);
            this.Controls.Add(this.tools);
            this.Name = "frm_QuirofanoExploradorNew";
            this.Text = "Pacientes - Procedimientos";
            this.Load += new System.EventHandler(this.frm_QuirofanoExploradorNew_Load);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridPacientes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualizar;
        private System.Windows.Forms.ToolStripButton toolStripButtonBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonTablet;
        private System.Windows.Forms.ToolStripButton toolStripButtonSalir;
        private Infragistics.Win.UltraWinGrid.UltraGrid UltraGridPacientes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nuevoProcedimientoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verProcedimientosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregaProductoUltimoProcedimientoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarProcedimientoToolStripMenuItem;
    }
}