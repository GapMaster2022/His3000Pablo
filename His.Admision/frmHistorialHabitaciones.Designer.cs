namespace His.Admision
{
    partial class frmHistorialHabitaciones
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
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ugrdHistorial = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.grpFechas = new Infragistics.Win.Misc.UltraGroupBox();
            this.chk_Fecha = new System.Windows.Forms.CheckBox();
            this.chk_HC = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHistoria = new System.Windows.Forms.TextBox();
            this.ayudaPacientes = new Infragistics.Win.Misc.UltraButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripbtnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugrdHistorial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).BeginInit();
            this.grpFechas.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.ultraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.ugrdHistorial);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(1, 22);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(694, 383);
            // 
            // ugrdHistorial
            // 
            this.ugrdHistorial.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ugrdHistorial.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.ugrdHistorial.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ugrdHistorial.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.ugrdHistorial.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ugrdHistorial.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.ugrdHistorial.DisplayLayout.MaxColScrollRegions = 1;
            this.ugrdHistorial.DisplayLayout.MaxRowScrollRegions = 1;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ugrdHistorial.DisplayLayout.Override.ActiveCellAppearance = appearance4;
            appearance5.BackColor = System.Drawing.SystemColors.Highlight;
            appearance5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ugrdHistorial.DisplayLayout.Override.ActiveRowAppearance = appearance5;
            this.ugrdHistorial.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ugrdHistorial.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.ugrdHistorial.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance7.BorderColor = System.Drawing.Color.Silver;
            appearance7.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ugrdHistorial.DisplayLayout.Override.CellAppearance = appearance7;
            this.ugrdHistorial.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ugrdHistorial.DisplayLayout.Override.CellPadding = 0;
            appearance8.BackColor = System.Drawing.SystemColors.Control;
            appearance8.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance8.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance8.BorderColor = System.Drawing.SystemColors.Window;
            this.ugrdHistorial.DisplayLayout.Override.GroupByRowAppearance = appearance8;
            appearance9.TextHAlignAsString = "Left";
            this.ugrdHistorial.DisplayLayout.Override.HeaderAppearance = appearance9;
            this.ugrdHistorial.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ugrdHistorial.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.ugrdHistorial.DisplayLayout.Override.RowAppearance = appearance10;
            this.ugrdHistorial.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance11.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ugrdHistorial.DisplayLayout.Override.TemplateAddRowAppearance = appearance11;
            this.ugrdHistorial.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ugrdHistorial.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ugrdHistorial.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.ugrdHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ugrdHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ugrdHistorial.Location = new System.Drawing.Point(0, 0);
            this.ugrdHistorial.Name = "ugrdHistorial";
            this.ugrdHistorial.Size = new System.Drawing.Size(694, 383);
            this.ugrdHistorial.TabIndex = 262;
            this.ugrdHistorial.Text = "ultraGrid";
            this.ugrdHistorial.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ugrdHistorial_InitializeLayout);
            this.ugrdHistorial.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.ugrdHistorial_InitializeRow);
            // 
            // grpFechas
            // 
            appearance12.BackColor = System.Drawing.Color.Transparent;
            this.grpFechas.Appearance = appearance12;
            this.grpFechas.Controls.Add(this.chk_Fecha);
            this.grpFechas.Controls.Add(this.chk_HC);
            this.grpFechas.Controls.Add(this.label2);
            this.grpFechas.Controls.Add(this.txtHistoria);
            this.grpFechas.Controls.Add(this.ayudaPacientes);
            this.grpFechas.Controls.Add(this.dateTimePicker1);
            this.grpFechas.Controls.Add(this.toolStrip1);
            this.grpFechas.Controls.Add(this.label1);
            this.grpFechas.Controls.Add(this.lblHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroHasta);
            this.grpFechas.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFechas.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.grpFechas.Location = new System.Drawing.Point(0, 0);
            this.grpFechas.Name = "grpFechas";
            this.grpFechas.Size = new System.Drawing.Size(696, 128);
            this.grpFechas.TabIndex = 33;
            this.grpFechas.Text = "Filtro de busquedas:";
            this.grpFechas.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // chk_Fecha
            // 
            this.chk_Fecha.AutoSize = true;
            this.chk_Fecha.Checked = true;
            this.chk_Fecha.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Fecha.Location = new System.Drawing.Point(327, 52);
            this.chk_Fecha.Name = "chk_Fecha";
            this.chk_Fecha.Size = new System.Drawing.Size(15, 14);
            this.chk_Fecha.TabIndex = 245;
            this.chk_Fecha.UseVisualStyleBackColor = true;
            this.chk_Fecha.CheckedChanged += new System.EventHandler(this.chk_Fecha_CheckedChanged);
            // 
            // chk_HC
            // 
            this.chk_HC.AutoSize = true;
            this.chk_HC.Location = new System.Drawing.Point(35, 52);
            this.chk_HC.Name = "chk_HC";
            this.chk_HC.Size = new System.Drawing.Size(15, 14);
            this.chk_HC.TabIndex = 244;
            this.chk_HC.UseVisualStyleBackColor = true;
            this.chk_HC.CheckedChanged += new System.EventHandler(this.chk_HC_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 243;
            this.label2.Text = "HISTORIA CLINICA";
            // 
            // txtHistoria
            // 
            this.txtHistoria.Enabled = false;
            this.txtHistoria.Location = new System.Drawing.Point(96, 59);
            this.txtHistoria.Name = "txtHistoria";
            this.txtHistoria.Size = new System.Drawing.Size(81, 20);
            this.txtHistoria.TabIndex = 242;
            this.txtHistoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHistoria_KeyPress);
            // 
            // ayudaPacientes
            // 
            appearance14.BackColor2 = System.Drawing.Color.DarkGray;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ayudaPacientes.Appearance = appearance14;
            this.ayudaPacientes.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.ayudaPacientes.Enabled = false;
            this.ayudaPacientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayudaPacientes.Location = new System.Drawing.Point(56, 59);
            this.ayudaPacientes.Name = "ayudaPacientes";
            this.ayudaPacientes.Size = new System.Drawing.Size(26, 19);
            this.ayudaPacientes.TabIndex = 241;
            this.ayudaPacientes.TabStop = false;
            this.ayudaPacientes.Text = "F1";
            this.ayudaPacientes.Click += new System.EventHandler(this.ayudaPacientes_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(408, 33);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(87, 20);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripbtnNuevo,
            this.btnExportar});
            this.toolStrip1.Location = new System.Drawing.Point(3, 100);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(690, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripbtnNuevo
            // 
            this.toolStripbtnNuevo.Image = global::His.Admision.Properties.Resources.HIS_REFRESH;
            this.toolStripbtnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnNuevo.Name = "toolStripbtnNuevo";
            this.toolStripbtnNuevo.Size = new System.Drawing.Size(79, 22);
            this.toolStripbtnNuevo.Text = "Actualizar";
            this.toolStripbtnNuevo.Click += new System.EventHandler(this.toolStripbtnNuevo_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Image = global::His.Admision.Properties.Resources.HIS_EXPORT_TO_EXCEL;
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(71, 22);
            this.btnExportar.Text = "Exportar";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(361, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Desde:";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(364, 66);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 6;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(408, 62);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(87, 20);
            this.dtpFiltroHasta.TabIndex = 4;
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(694, 383);
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
            this.ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraTabControl1.Location = new System.Drawing.Point(0, 128);
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Size = new System.Drawing.Size(696, 406);
            this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Office2007Ribbon;
            this.ultraTabControl1.TabIndex = 264;
            ultraTab1.Key = "estado";
            ultraTab1.TabPage = this.ultraTabPageControl1;
            ultraTab1.Text = "Estado Habitaciones";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1});
            // 
            // frmHistorialHabitaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 534);
            this.Controls.Add(this.ultraTabControl1);
            this.Controls.Add(this.grpFechas);
            this.Name = "frmHistorialHabitaciones";
            this.Text = "Historial";
            this.Load += new System.EventHandler(this.frmHistorialHabitaciones_Load);
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugrdHistorial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).EndInit();
            this.grpFechas.ResumeLayout(false);
            this.grpFechas.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.ultraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox grpFechas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugrdHistorial;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripbtnNuevo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.TextBox txtHistoria;
        private Infragistics.Win.Misc.UltraButton ayudaPacientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_Fecha;
        private System.Windows.Forms.CheckBox chk_HC;
    }
}