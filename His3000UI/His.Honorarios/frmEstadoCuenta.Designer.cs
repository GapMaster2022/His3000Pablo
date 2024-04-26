namespace His.Honorarios
{
    partial class frmEstadoCuenta
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
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEstadoCuenta));
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ugrdCuenta = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.ayudaMedicos = new Infragistics.Win.Misc.UltraButton();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFiltroDesde = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodMedico = new System.Windows.Forms.TextBox();
            this.txtNombreMedico = new System.Windows.Forms.TextBox();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonActualizar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugrdCuenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.ultraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.ugrdCuenta);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(1, 22);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(700, 263);
            // 
            // ugrdCuenta
            // 
            this.ugrdCuenta.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ugrdCuenta.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ugrdCuenta.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ugrdCuenta.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ugrdCuenta.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ugrdCuenta.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ugrdCuenta.DisplayLayout.MaxColScrollRegions = 1;
            this.ugrdCuenta.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ugrdCuenta.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ugrdCuenta.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.ugrdCuenta.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ugrdCuenta.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ugrdCuenta.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ugrdCuenta.DisplayLayout.Override.CellAppearance = appearance8;
            this.ugrdCuenta.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ugrdCuenta.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ugrdCuenta.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.ugrdCuenta.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ugrdCuenta.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ugrdCuenta.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ugrdCuenta.DisplayLayout.Override.RowAppearance = appearance11;
            this.ugrdCuenta.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ugrdCuenta.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.ugrdCuenta.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ugrdCuenta.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ugrdCuenta.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.ugrdCuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ugrdCuenta.Location = new System.Drawing.Point(0, 0);
            this.ugrdCuenta.Name = "ugrdCuenta";
            this.ugrdCuenta.Size = new System.Drawing.Size(700, 263);
            this.ugrdCuenta.TabIndex = 262;
            this.ugrdCuenta.Text = "ultraGrid";
            this.ugrdCuenta.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ugrdCuenta_InitializeRow);
            this.ugrdCuenta.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ugrdCuenta_InitializeLayout);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.ultraGroupBox2);
            this.ultraGroupBox1.Controls.Add(this.toolStripMenu);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(702, 170);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.label28);
            this.ultraGroupBox2.Controls.Add(this.ayudaMedicos);
            this.ultraGroupBox2.Controls.Add(this.dtpFiltroHasta);
            this.ultraGroupBox2.Controls.Add(this.lblHasta);
            this.ultraGroupBox2.Controls.Add(this.dtpFiltroDesde);
            this.ultraGroupBox2.Controls.Add(this.label1);
            this.ultraGroupBox2.Controls.Add(this.txtCodMedico);
            this.ultraGroupBox2.Controls.Add(this.txtNombreMedico);
            this.ultraGroupBox2.Location = new System.Drawing.Point(67, 41);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(588, 110);
            this.ultraGroupBox2.TabIndex = 253;
            this.ultraGroupBox2.Text = "Filtros";
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.XP;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(6, 26);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(44, 13);
            this.label28.TabIndex = 246;
            this.label28.Text = "Nombre";
            // 
            // ayudaMedicos
            // 
            appearance16.BackColor2 = System.Drawing.Color.DarkGray;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ayudaMedicos.Appearance = appearance16;
            this.ayudaMedicos.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.ayudaMedicos.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaMedicos.Location = new System.Drawing.Point(56, 26);
            this.ayudaMedicos.Name = "ayudaMedicos";
            this.ayudaMedicos.Size = new System.Drawing.Size(25, 18);
            this.ayudaMedicos.TabIndex = 247;
            this.ayudaMedicos.TabStop = false;
            this.ayudaMedicos.Text = "F1";
            this.ayudaMedicos.Click += new System.EventHandler(this.ayudaMedicos_Click);
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(56, 75);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroHasta.TabIndex = 249;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.BackColor = System.Drawing.Color.Transparent;
            this.lblHasta.Location = new System.Drawing.Point(6, 75);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 251;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpFiltroDesde
            // 
            this.dtpFiltroDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroDesde.Location = new System.Drawing.Point(56, 52);
            this.dtpFiltroDesde.Name = "dtpFiltroDesde";
            this.dtpFiltroDesde.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroDesde.TabIndex = 248;
            this.dtpFiltroDesde.Value = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(6, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 250;
            this.label1.Text = "Desde:";
            // 
            // txtCodMedico
            // 
            this.txtCodMedico.BackColor = System.Drawing.Color.White;
            this.txtCodMedico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodMedico.Location = new System.Drawing.Point(87, 24);
            this.txtCodMedico.Multiline = true;
            this.txtCodMedico.Name = "txtCodMedico";
            this.txtCodMedico.Size = new System.Drawing.Size(61, 22);
            this.txtCodMedico.TabIndex = 244;
            this.txtCodMedico.TextChanged += new System.EventHandler(this.txtCodMedico_TextChanged);
            // 
            // txtNombreMedico
            // 
            this.txtNombreMedico.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNombreMedico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreMedico.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreMedico.Location = new System.Drawing.Point(163, 23);
            this.txtNombreMedico.Name = "txtNombreMedico";
            this.txtNombreMedico.ReadOnly = true;
            this.txtNombreMedico.Size = new System.Drawing.Size(315, 22);
            this.txtNombreMedico.TabIndex = 245;
            this.txtNombreMedico.TabStop = false;
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonActualizar,
            this.toolStripButtonExportar,
            this.toolStripButtonSalir});
            this.toolStripMenu.Location = new System.Drawing.Point(3, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(696, 37);
            this.toolStripMenu.TabIndex = 252;
            this.toolStripMenu.Text = "Menu Principal";
            // 
            // toolStripButtonActualizar
            // 
            this.toolStripButtonActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonActualizar.Enabled = false;
            this.toolStripButtonActualizar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonActualizar.Image")));
            this.toolStripButtonActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonActualizar.Name = "toolStripButtonActualizar";
            this.toolStripButtonActualizar.Size = new System.Drawing.Size(28, 34);
            this.toolStripButtonActualizar.Text = "Actualizar";
            this.toolStripButtonActualizar.Click += new System.EventHandler(this.toolStripButtonActualizar_Click);
            // 
            // toolStripButtonExportar
            // 
            this.toolStripButtonExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExportar.Enabled = false;
            this.toolStripButtonExportar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportar.Image")));
            this.toolStripButtonExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportar.Name = "toolStripButtonExportar";
            this.toolStripButtonExportar.Size = new System.Drawing.Size(28, 34);
            this.toolStripButtonExportar.Click += new System.EventHandler(this.toolStripButtonExportar_Click);
            // 
            // toolStripButtonSalir
            // 
            this.toolStripButtonSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSalir.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSalir.Image")));
            this.toolStripButtonSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSalir.Name = "toolStripButtonSalir";
            this.toolStripButtonSalir.Padding = new System.Windows.Forms.Padding(3);
            this.toolStripButtonSalir.Size = new System.Drawing.Size(34, 34);
            this.toolStripButtonSalir.Text = "Salir";
            this.toolStripButtonSalir.ToolTipText = "Salir del Explorador";
            this.toolStripButtonSalir.Click += new System.EventHandler(this.toolStripButtonSalir_Click);
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
            this.ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraTabControl1.Enabled = false;
            this.ultraTabControl1.Location = new System.Drawing.Point(0, 170);
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Size = new System.Drawing.Size(702, 286);
            this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Office2007Ribbon;
            this.ultraTabControl1.TabIndex = 264;
            ultraTab1.TabPage = this.ultraTabPageControl1;
            ultraTab1.Text = "Estado Cuentas";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(700, 263);
            // 
            // frmEstadoCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 456);
            this.Controls.Add(this.ultraTabControl1);
            this.Controls.Add(this.ultraGroupBox1);
            this.Name = "frmEstadoCuenta";
            this.Text = "Estado  de Cuenta";
            this.Load += new System.EventHandler(this.frmEstadoCuenta_Load);
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugrdCuenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.ultraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraButton ayudaMedicos;
        private System.Windows.Forms.TextBox txtCodMedico;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtNombreMedico;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugrdCuenta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroDesde;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualizar;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportar;
        private System.Windows.Forms.ToolStripButton toolStripButtonSalir;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
    }
}