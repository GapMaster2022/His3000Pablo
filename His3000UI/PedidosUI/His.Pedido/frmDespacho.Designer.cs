
namespace His.Pedido
{
    partial class frmDespacho
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
            this.tileNavPane1 = new DevExpress.XtraBars.Navigation.TileNavPane();
            this.navButton2 = new DevExpress.XtraBars.Navigation.NavButton();
            this.btnActualizar = new DevExpress.XtraBars.Navigation.NavButton();
            this.btnExportar = new DevExpress.XtraBars.Navigation.NavButton();
            this.btnImprimir = new DevExpress.XtraBars.Navigation.NavButton();
            this.btnSalir = new DevExpress.XtraBars.Navigation.NavButton();
            this.dtpDesde = new DevExpress.XtraEditors.DateEdit();
            this.lblFecLiq = new System.Windows.Forms.Label();
            this.dtphasta = new DevExpress.XtraEditors.DateEdit();
            this.ultraGridDespachos = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraTabbedMdiManager1 = new Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(this.components);
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            ((System.ComponentModel.ISupportInitialize)(this.tileNavPane1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtphasta.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtphasta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDespachos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.ultraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tileNavPane1
            // 
            this.tileNavPane1.Buttons.Add(this.navButton2);
            this.tileNavPane1.Buttons.Add(this.btnActualizar);
            this.tileNavPane1.Buttons.Add(this.btnExportar);
            this.tileNavPane1.Buttons.Add(this.btnImprimir);
            this.tileNavPane1.Buttons.Add(this.btnSalir);
            // 
            // tileNavCategory1
            // 
            this.tileNavPane1.DefaultCategory.Name = "tileNavCategory1";
            // 
            // 
            // 
            this.tileNavPane1.DefaultCategory.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileNavPane1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileNavPane1.Location = new System.Drawing.Point(0, 0);
            this.tileNavPane1.Name = "tileNavPane1";
            this.tileNavPane1.Size = new System.Drawing.Size(800, 40);
            this.tileNavPane1.TabIndex = 3;
            this.tileNavPane1.Text = "tileNavPane1";
            // 
            // navButton2
            // 
            this.navButton2.Caption = "Main Menu";
            this.navButton2.IsMain = true;
            this.navButton2.Name = "navButton2";
            this.navButton2.Visible = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.btnActualizar.Caption = "ACTUALIZAR";
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.btnActualizar_ElementClick);
            // 
            // btnExportar
            // 
            this.btnExportar.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.btnExportar.Caption = "EXPORTAR";
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.btnImprimir.Caption = "IMPRIMIR";
            this.btnImprimir.Name = "btnImprimir";
            // 
            // btnSalir
            // 
            this.btnSalir.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.btnSalir.Caption = "SALIR";
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.navSalir_ElementClick);
            // 
            // dtpDesde
            // 
            this.dtpDesde.EditValue = null;
            this.dtpDesde.Location = new System.Drawing.Point(66, 11);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDesde.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDesde.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.ClassicNew;
            this.dtpDesde.Properties.MaxValue = new System.DateTime(2199, 12, 31, 23, 59, 0, 0);
            this.dtpDesde.Properties.MinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDesde.Properties.NullDate = new System.DateTime(2021, 1, 8, 10, 23, 48, 0);
            this.dtpDesde.Properties.NullDateCalendarValue = new System.DateTime(2021, 1, 28, 15, 12, 43, 0);
            this.dtpDesde.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtpDesde.Size = new System.Drawing.Size(196, 20);
            this.dtpDesde.TabIndex = 71;
            // 
            // lblFecLiq
            // 
            this.lblFecLiq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(92)))), ((int)(((byte)(146)))));
            this.lblFecLiq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecLiq.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblFecLiq.Location = new System.Drawing.Point(12, 9);
            this.lblFecLiq.Name = "lblFecLiq";
            this.lblFecLiq.Size = new System.Drawing.Size(48, 20);
            this.lblFecLiq.TabIndex = 70;
            this.lblFecLiq.Text = "Fecha:";
            this.lblFecLiq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtphasta
            // 
            this.dtphasta.EditValue = null;
            this.dtphasta.Location = new System.Drawing.Point(278, 11);
            this.dtphasta.Name = "dtphasta";
            this.dtphasta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtphasta.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtphasta.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.ClassicNew;
            this.dtphasta.Properties.MaxValue = new System.DateTime(2199, 12, 31, 23, 59, 0, 0);
            this.dtphasta.Properties.MinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtphasta.Properties.NullDate = new System.DateTime(2021, 1, 8, 10, 23, 48, 0);
            this.dtphasta.Properties.NullDateCalendarValue = new System.DateTime(2021, 1, 28, 15, 12, 43, 0);
            this.dtphasta.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtphasta.Size = new System.Drawing.Size(196, 20);
            this.dtphasta.TabIndex = 72;
            // 
            // ultraGridDespachos
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridDespachos.DisplayLayout.Appearance = appearance1;
            this.ultraGridDespachos.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridDespachos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridDespachos.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridDespachos.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ultraGridDespachos.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridDespachos.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ultraGridDespachos.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridDespachos.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridDespachos.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridDespachos.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.ultraGridDespachos.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridDespachos.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridDespachos.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridDespachos.DisplayLayout.Override.CellAppearance = appearance8;
            this.ultraGridDespachos.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridDespachos.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridDespachos.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.ultraGridDespachos.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ultraGridDespachos.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridDespachos.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridDespachos.DisplayLayout.Override.RowAppearance = appearance11;
            this.ultraGridDespachos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridDespachos.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.ultraGridDespachos.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridDespachos.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridDespachos.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGridDespachos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridDespachos.Location = new System.Drawing.Point(0, 40);
            this.ultraGridDespachos.Name = "ultraGridDespachos";
            this.ultraGridDespachos.Size = new System.Drawing.Size(800, 410);
            this.ultraGridDespachos.TabIndex = 73;
            this.ultraGridDespachos.Text = "ultraGrid1";
            // 
            // ultraTabbedMdiManager1
            // 
            this.ultraTabbedMdiManager1.MdiParent = this;
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Location = new System.Drawing.Point(469, 234);
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Size = new System.Drawing.Size(200, 100);
            this.ultraTabControl1.TabIndex = 74;
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(1, 20);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(196, 77);
            // 
            // frmDespacho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ultraTabControl1);
            this.Controls.Add(this.ultraGridDespachos);
            this.Controls.Add(this.dtphasta);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.lblFecLiq);
            this.Controls.Add(this.tileNavPane1);
            this.Name = "frmDespacho";
            this.Text = "Control de Despacho";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.tileNavPane1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDesde.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtphasta.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtphasta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridDespachos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.ultraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TileNavPane tileNavPane1;
        private DevExpress.XtraBars.Navigation.NavButton navButton2;
        private DevExpress.XtraBars.Navigation.NavButton btnActualizar;
        private DevExpress.XtraBars.Navigation.NavButton btnExportar;
        private DevExpress.XtraBars.Navigation.NavButton btnImprimir;
        private DevExpress.XtraBars.Navigation.NavButton btnSalir;
        private DevExpress.XtraEditors.DateEdit dtpDesde;
        private System.Windows.Forms.Label lblFecLiq;
        private DevExpress.XtraEditors.DateEdit dtphasta;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridDespachos;
        private Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager ultraTabbedMdiManager1;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
    }
}