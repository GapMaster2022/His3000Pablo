namespace CuentaPaciente
{
    partial class frmExportarExel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportarExel));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblTotalCuenta = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lLblNinguno = new System.Windows.Forms.LinkLabel();
            this.lLblTodos = new System.Windows.Forms.LinkLabel();
            this.uGridCuentas = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripbtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uGridCuentas)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblTotalCuenta);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lLblNinguno);
            this.splitContainer1.Panel1.Controls.Add(this.lLblTodos);
            this.splitContainer1.Panel1.Controls.Add(this.uGridCuentas);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(837, 622);
            this.splitContainer1.SplitterDistance = 569;
            this.splitContainer1.TabIndex = 33;
            // 
            // lblTotalCuenta
            // 
            this.lblTotalCuenta.AutoSize = true;
            this.lblTotalCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCuenta.Location = new System.Drawing.Point(118, 25);
            this.lblTotalCuenta.Name = "lblTotalCuenta";
            this.lblTotalCuenta.Size = new System.Drawing.Size(41, 13);
            this.lblTotalCuenta.TabIndex = 29;
            this.lblTotalCuenta.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "TOTAL CUENTA :";
            // 
            // lLblNinguno
            // 
            this.lLblNinguno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lLblNinguno.AutoSize = true;
            this.lLblNinguno.BackColor = System.Drawing.Color.Transparent;
            this.lLblNinguno.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.lLblNinguno.Location = new System.Drawing.Point(710, 4);
            this.lLblNinguno.Name = "lLblNinguno";
            this.lLblNinguno.Size = new System.Drawing.Size(47, 13);
            this.lLblNinguno.TabIndex = 27;
            this.lLblNinguno.TabStop = true;
            this.lLblNinguno.Text = "Ninguno";
            // 
            // lLblTodos
            // 
            this.lLblTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lLblTodos.AutoSize = true;
            this.lLblTodos.BackColor = System.Drawing.Color.Transparent;
            this.lLblTodos.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.lLblTodos.Location = new System.Drawing.Point(656, 4);
            this.lLblTodos.Name = "lLblTodos";
            this.lLblTodos.Size = new System.Drawing.Size(37, 13);
            this.lLblTodos.TabIndex = 26;
            this.lLblTodos.TabStop = true;
            this.lLblTodos.Text = "Todos";
            // 
            // uGridCuentas
            // 
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
            this.uGridCuentas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uGridCuentas.Location = new System.Drawing.Point(0, 41);
            this.uGridCuentas.Name = "uGridCuentas";
            this.uGridCuentas.Size = new System.Drawing.Size(837, 581);
            this.uGridCuentas.TabIndex = 0;
            this.uGridCuentas.Text = "Pedidos";
            this.uGridCuentas.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.uGridCuentas_InitializeRow);
            this.uGridCuentas.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.uGridCuentas_InitializeLayout);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripbtnNuevo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(837, 25);
            this.toolStrip1.TabIndex = 34;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripbtnNuevo
            // 
            this.toolStripbtnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnNuevo.Image")));
            this.toolStripbtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnNuevo.Name = "toolStripbtnNuevo";
            this.toolStripbtnNuevo.Size = new System.Drawing.Size(69, 22);
            this.toolStripbtnNuevo.Text = "Exportar";
            this.toolStripbtnNuevo.Click += new System.EventHandler(this.toolStripbtnNuevo_Click);
            // 
            // frmExportarExel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 622);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmExportarExel";
            this.Text = " ";
            this.Load += new System.EventHandler(this.frmExportarExel_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uGridCuentas)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.LinkLabel lLblNinguno;
        private Infragistics.Win.UltraWinGrid.UltraGrid uGridCuentas;
        private System.Windows.Forms.LinkLabel lLblTodos;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripbtnNuevo;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.Label lblTotalCuenta;
        private System.Windows.Forms.Label label1;
    }
}