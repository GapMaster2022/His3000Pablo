
namespace His.Admision
{
    partial class frmExploradorPreAdmision
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
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
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
            this.tools = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkHC = new System.Windows.Forms.CheckBox();
            this.ayudaPacientes = new Infragistics.Win.Misc.UltraButton();
            this.txt_historiaclinica = new System.Windows.Forms.TextBox();
            this.grpFechas = new Infragistics.Win.Misc.UltraGroupBox();
            this.chkFacturacion = new System.Windows.Forms.CheckBox();
            this.chkAlta = new System.Windows.Forms.CheckBox();
            this.chkIngreso = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFiltroDesde = new System.Windows.Forms.DateTimePicker();
            this.grid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.tools.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).BeginInit();
            this.grpFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonActualizar,
            this.toolStripButtonBuscar,
            this.toolStripSeparator1,
            this.toolStripButtonSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tools.Size = new System.Drawing.Size(1448, 69);
            this.tools.TabIndex = 12;
            this.tools.Text = "toolStrip1";
            // 
            // toolStripButtonActualizar
            // 
            this.toolStripButtonActualizar.AutoSize = false;
            this.toolStripButtonActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonActualizar.Image = global::His.Admision.Properties.Resources.HIS_REFRESH;
            this.toolStripButtonActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonActualizar.Name = "toolStripButtonActualizar";
            this.toolStripButtonActualizar.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonActualizar.Text = "toolStripButton1";
            this.toolStripButtonActualizar.ToolTipText = "Actualizar";
            this.toolStripButtonActualizar.Click += new System.EventHandler(this.toolStripButtonActualizar_Click);
            // 
            // toolStripButtonBuscar
            // 
            this.toolStripButtonBuscar.AutoSize = false;
            this.toolStripButtonBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBuscar.Image = global::His.Admision.Properties.Resources.HIS_EXPORT_TO_EXCEL;
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
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 69);
            // 
            // toolStripButtonSalir
            // 
            this.toolStripButtonSalir.AutoSize = false;
            this.toolStripButtonSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSalir.Image = global::His.Admision.Properties.Resources.HIS_SALIR;
            this.toolStripButtonSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSalir.Name = "toolStripButtonSalir";
            this.toolStripButtonSalir.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonSalir.Text = "toolStripButton1";
            this.toolStripButtonSalir.ToolTipText = "Salir";
            this.toolStripButtonSalir.Click += new System.EventHandler(this.toolStripButtonSalir_Click);
            // 
            // ultraPanel1
            // 
            appearance14.BackColor = System.Drawing.Color.Silver;
            appearance14.BackColor2 = System.Drawing.Color.DimGray;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop20;
            appearance14.BackHatchStyle = Infragistics.Win.BackHatchStyle.DiagonalCross;
            this.ultraPanel1.Appearance = appearance14;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.ultraPanel1.ClientArea.Controls.Add(this.grpFechas);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraPanel1.Location = new System.Drawing.Point(0, 69);
            this.ultraPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(1448, 157);
            this.ultraPanel1.TabIndex = 13;
            // 
            // ultraGroupBox1
            // 
            appearance17.BackColor = System.Drawing.Color.Transparent;
            this.ultraGroupBox1.Appearance = appearance17;
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.chkHC);
            this.ultraGroupBox1.Controls.Add(this.ayudaPacientes);
            this.ultraGroupBox1.Controls.Add(this.txt_historiaclinica);
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox1.Location = new System.Drawing.Point(442, 5);
            this.ultraGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(330, 143);
            this.ultraGroupBox1.TabIndex = 1;
            this.ultraGroupBox1.Text = "Paciente";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(302, 38);
            this.label2.TabIndex = 273;
            this.label2.Text = "[Escriba el numero de historia clinica o presione F1 para buscar por nombre.]";
            // 
            // chkHC
            // 
            this.chkHC.AutoSize = true;
            this.chkHC.Location = new System.Drawing.Point(18, 57);
            this.chkHC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkHC.Name = "chkHC";
            this.chkHC.Size = new System.Drawing.Size(130, 24);
            this.chkHC.TabIndex = 272;
            this.chkHC.Text = "Nro. Historia :";
            this.chkHC.UseVisualStyleBackColor = true;
            this.chkHC.CheckedChanged += new System.EventHandler(this.chkHC_CheckedChanged);
            // 
            // ayudaPacientes
            // 
            appearance50.ForeColor = System.Drawing.Color.Navy;
            appearance50.TextHAlignAsString = "Center";
            appearance50.TextVAlignAsString = "Middle";
            this.ayudaPacientes.Appearance = appearance50;
            this.ayudaPacientes.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            this.ayudaPacientes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaPacientes.Location = new System.Drawing.Point(266, 49);
            this.ayudaPacientes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ayudaPacientes.Name = "ayudaPacientes";
            this.ayudaPacientes.Size = new System.Drawing.Size(45, 32);
            this.ayudaPacientes.TabIndex = 271;
            this.ayudaPacientes.TabStop = false;
            this.ayudaPacientes.Text = "F1";
            this.ayudaPacientes.Visible = false;
            this.ayudaPacientes.Click += new System.EventHandler(this.ayudaPacientes_Click);
            // 
            // txt_historiaclinica
            // 
            this.txt_historiaclinica.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_historiaclinica.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_historiaclinica.Enabled = false;
            this.txt_historiaclinica.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_historiaclinica.Location = new System.Drawing.Point(156, 48);
            this.txt_historiaclinica.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_historiaclinica.MaxLength = 6;
            this.txt_historiaclinica.Name = "txt_historiaclinica";
            this.txt_historiaclinica.Size = new System.Drawing.Size(98, 33);
            this.txt_historiaclinica.TabIndex = 269;
            this.txt_historiaclinica.Tag = "false";
            this.txt_historiaclinica.Text = "0";
            this.txt_historiaclinica.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_historiaclinica_KeyPress);
            this.txt_historiaclinica.Leave += new System.EventHandler(this.txt_historiaclinica_Leave);
            // 
            // grpFechas
            // 
            appearance16.BackColor = System.Drawing.Color.Transparent;
            this.grpFechas.Appearance = appearance16;
            this.grpFechas.Controls.Add(this.chkFacturacion);
            this.grpFechas.Controls.Add(this.chkAlta);
            this.grpFechas.Controls.Add(this.chkIngreso);
            this.grpFechas.Controls.Add(this.label1);
            this.grpFechas.Controls.Add(this.lblHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroDesde);
            this.grpFechas.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.grpFechas.Location = new System.Drawing.Point(9, 5);
            this.grpFechas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpFechas.Name = "grpFechas";
            this.grpFechas.Size = new System.Drawing.Size(424, 143);
            this.grpFechas.TabIndex = 0;
            this.grpFechas.Text = "Filtro por Fechas:";
            // 
            // chkFacturacion
            // 
            this.chkFacturacion.AutoSize = true;
            this.chkFacturacion.Location = new System.Drawing.Point(16, 100);
            this.chkFacturacion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkFacturacion.Name = "chkFacturacion";
            this.chkFacturacion.Size = new System.Drawing.Size(119, 24);
            this.chkFacturacion.TabIndex = 10;
            this.chkFacturacion.Text = "Facturacion";
            this.chkFacturacion.UseVisualStyleBackColor = true;
            this.chkFacturacion.Visible = false;
            // 
            // chkAlta
            // 
            this.chkAlta.AutoSize = true;
            this.chkAlta.Location = new System.Drawing.Point(16, 69);
            this.chkAlta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkAlta.Name = "chkAlta";
            this.chkAlta.Size = new System.Drawing.Size(129, 24);
            this.chkAlta.TabIndex = 9;
            this.chkAlta.Text = "Alta Paciente";
            this.chkAlta.UseVisualStyleBackColor = true;
            this.chkAlta.Visible = false;
            // 
            // chkIngreso
            // 
            this.chkIngreso.AutoSize = true;
            this.chkIngreso.Checked = true;
            this.chkIngreso.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIngreso.Location = new System.Drawing.Point(16, 40);
            this.chkIngreso.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkIngreso.Name = "chkIngreso";
            this.chkIngreso.Size = new System.Drawing.Size(138, 24);
            this.chkIngreso.TabIndex = 8;
            this.chkIngreso.Text = "Fecha Ingreso";
            this.chkIngreso.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Desde:";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(204, 97);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(56, 20);
            this.lblHasta.TabIndex = 6;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(274, 91);
            this.dtpFiltroHasta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(128, 26);
            this.dtpFiltroHasta.TabIndex = 4;
            // 
            // dtpFiltroDesde
            // 
            this.dtpFiltroDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroDesde.Location = new System.Drawing.Point(274, 46);
            this.dtpFiltroDesde.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpFiltroDesde.Name = "dtpFiltroDesde";
            this.dtpFiltroDesde.Size = new System.Drawing.Size(128, 26);
            this.dtpFiltroDesde.TabIndex = 3;
            this.dtpFiltroDesde.Value = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            // 
            // grid
            // 
            appearance2.BackColor = System.Drawing.SystemColors.Window;
            appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grid.DisplayLayout.Appearance = appearance2;
            this.grid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance3.BorderColor = System.Drawing.SystemColors.Window;
            this.grid.DisplayLayout.GroupByBox.Appearance = appearance3;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
            this.grid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance5.BackColor2 = System.Drawing.SystemColors.Control;
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
            this.grid.DisplayLayout.MaxColScrollRegions = 1;
            this.grid.DisplayLayout.MaxRowScrollRegions = 1;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grid.DisplayLayout.Override.ActiveCellAppearance = appearance6;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grid.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.grid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance8.BackColor = System.Drawing.SystemColors.Window;
            this.grid.DisplayLayout.Override.CardAreaAppearance = appearance8;
            appearance9.BorderColor = System.Drawing.Color.Silver;
            appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grid.DisplayLayout.Override.CellAppearance = appearance9;
            this.grid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grid.DisplayLayout.Override.CellPadding = 0;
            appearance10.BackColor = System.Drawing.SystemColors.Control;
            appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance10.BorderColor = System.Drawing.SystemColors.Window;
            this.grid.DisplayLayout.Override.GroupByRowAppearance = appearance10;
            appearance11.TextHAlignAsString = "Left";
            this.grid.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.grid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.BorderColor = System.Drawing.Color.Silver;
            this.grid.DisplayLayout.Override.RowAppearance = appearance12;
            this.grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grid.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
            this.grid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid.Location = new System.Drawing.Point(0, 226);
            this.grid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1448, 299);
            this.grid.TabIndex = 14;
            this.grid.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.grid_InitializeLayout);
            // 
            // frmExploradorPreAdmision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1448, 525);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ultraPanel1);
            this.Controls.Add(this.tools);
            this.Name = "frmExploradorPreAdmision";
            this.Text = "Explorador PreAdmision";
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).EndInit();
            this.grpFechas.ResumeLayout(false);
            this.grpFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualizar;
        private System.Windows.Forms.ToolStripButton toolStripButtonBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSalir;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkHC;
        private Infragistics.Win.Misc.UltraButton ayudaPacientes;
        private System.Windows.Forms.TextBox txt_historiaclinica;
        private Infragistics.Win.Misc.UltraGroupBox grpFechas;
        private System.Windows.Forms.CheckBox chkFacturacion;
        private System.Windows.Forms.CheckBox chkAlta;
        private System.Windows.Forms.CheckBox chkIngreso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroDesde;
        private Infragistics.Win.UltraWinGrid.UltraGrid grid;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
    }
}