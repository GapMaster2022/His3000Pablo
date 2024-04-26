namespace His.Dietetica
{
    partial class frmQuirofanoExplorador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuirofanoExplorador));
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
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
            this.toolStripButtonActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBuscar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
            this.grpDatos = new Infragistics.Win.Misc.UltraGroupBox();
            this.cmbProcedimiento = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UltraGridPacientes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.toolStripButtonTablet = new System.Windows.Forms.ToolStripButton();
            this.tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatos)).BeginInit();
            this.grpDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridPacientes)).BeginInit();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonActualizar,
            this.toolStripButtonBuscar,
            this.toolStripSeparator1,
            this.toolStripButtonTablet,
            this.toolStripButtonSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(992, 45);
            this.tools.TabIndex = 78;
            this.tools.Text = "toolStrip1";
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
            this.toolStripButtonActualizar.Text = "toolStripButton1";
            this.toolStripButtonActualizar.ToolTipText = "Restaurar";
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
            // grpDatos
            // 
            this.grpDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance13.BackColor = System.Drawing.Color.Gainsboro;
            appearance13.BackColor2 = System.Drawing.Color.Snow;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop37;
            this.grpDatos.Appearance = appearance13;
            appearance14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            appearance14.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance14.BorderColor = System.Drawing.Color.White;
            this.grpDatos.ContentAreaAppearance = appearance14;
            this.grpDatos.Controls.Add(this.cmbProcedimiento);
            this.grpDatos.Controls.Add(this.label3);
            this.grpDatos.ForeColor = System.Drawing.Color.Black;
            appearance15.BackColor = System.Drawing.Color.Transparent;
            appearance15.BackColor2 = System.Drawing.Color.Transparent;
            appearance15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grpDatos.HeaderAppearance = appearance15;
            this.grpDatos.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.grpDatos.Location = new System.Drawing.Point(1, 48);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(979, 53);
            this.grpDatos.TabIndex = 79;
            this.grpDatos.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // cmbProcedimiento
            // 
            this.cmbProcedimiento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProcedimiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcedimiento.FormattingEnabled = true;
            this.cmbProcedimiento.Location = new System.Drawing.Point(98, 16);
            this.cmbProcedimiento.Name = "cmbProcedimiento";
            this.cmbProcedimiento.Size = new System.Drawing.Size(617, 21);
            this.cmbProcedimiento.TabIndex = 75;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(19, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Procedimiento:";
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
            this.UltraGridPacientes.Location = new System.Drawing.Point(1, 107);
            this.UltraGridPacientes.Name = "UltraGridPacientes";
            this.UltraGridPacientes.Size = new System.Drawing.Size(968, 215);
            this.UltraGridPacientes.TabIndex = 80;
            this.UltraGridPacientes.Text = "ultraGrid1";
            this.UltraGridPacientes.Click += new System.EventHandler(this.UltraGridPacientes_Click);
            this.UltraGridPacientes.DoubleClick += new System.EventHandler(this.UltraGridPacientes_DoubleClick);
            this.UltraGridPacientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UltraGridPacientes_KeyDown);
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
            // frmQuirofanoExplorador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 334);
            this.Controls.Add(this.UltraGridPacientes);
            this.Controls.Add(this.grpDatos);
            this.Controls.Add(this.tools);
            this.Name = "frmQuirofanoExplorador";
            this.Text = "Explorador Pacientes - Quirofano";
            this.Load += new System.EventHandler(this.frmQuirofanoExplorador_Load);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatos)).EndInit();
            this.grpDatos.ResumeLayout(false);
            this.grpDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridPacientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualizar;
        private System.Windows.Forms.ToolStripButton toolStripButtonBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSalir;
        private Infragistics.Win.Misc.UltraGroupBox grpDatos;
        private System.Windows.Forms.ComboBox cmbProcedimiento;
        private System.Windows.Forms.Label label3;
        private Infragistics.Win.UltraWinGrid.UltraGrid UltraGridPacientes;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.ToolStripButton toolStripButtonTablet;
    }
}