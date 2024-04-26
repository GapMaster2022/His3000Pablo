namespace GeneralApp
{
    partial class FrmAdministracion
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
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listaGridview = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.camposPanel = new Infragistics.Win.Misc.UltraGroupBox();
            this.frmAdministracion_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listaGridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camposPanel)).BeginInit();
            this.frmAdministracion_Fill_Panel.ClientArea.SuspendLayout();
            this.frmAdministracion_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnActualizar,
            this.btnEliminar,
            this.btnGuardar,
            this.btnCancelar,
            this.btnSalir});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(740, 36);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevoClick);
            // 
            // btnActualizar
            // 
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(54, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Click += new System.EventHandler(this.BtnActualizarClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(47, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.BtnEliminarClick);
            // 
            // btnGuardar
            // 
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(50, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardarClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(42, 33);
            this.btnCancelar.Text = "Cerrar";
            this.btnCancelar.Click += new System.EventHandler(this.BtnCerrarClick);
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(23, 33);
            this.btnSalir.Text = "toolStripButton1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.listaGridview);
            this.panel1.Location = new System.Drawing.Point(15, 176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 159);
            this.panel1.TabIndex = 2;
            // 
            // listaGridview
            // 
            this.listaGridview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance25.BackColor = System.Drawing.SystemColors.Window;
            appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.listaGridview.DisplayLayout.Appearance = appearance25;
            this.listaGridview.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.listaGridview.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance26.BorderColor = System.Drawing.SystemColors.Window;
            this.listaGridview.DisplayLayout.GroupByBox.Appearance = appearance26;
            appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
            this.listaGridview.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
            this.listaGridview.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.listaGridview.DisplayLayout.GroupByBox.Hidden = true;
            appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance28.BackColor2 = System.Drawing.SystemColors.Control;
            appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
            this.listaGridview.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
            this.listaGridview.DisplayLayout.MaxColScrollRegions = 1;
            this.listaGridview.DisplayLayout.MaxRowScrollRegions = 1;
            appearance29.BackColor = System.Drawing.SystemColors.Window;
            appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listaGridview.DisplayLayout.Override.ActiveCellAppearance = appearance29;
            appearance30.BackColor = System.Drawing.SystemColors.Highlight;
            appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.listaGridview.DisplayLayout.Override.ActiveRowAppearance = appearance30;
            this.listaGridview.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.listaGridview.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.listaGridview.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.listaGridview.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.listaGridview.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            this.listaGridview.DisplayLayout.Override.CardAreaAppearance = appearance31;
            appearance32.BorderColor = System.Drawing.Color.Silver;
            appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.listaGridview.DisplayLayout.Override.CellAppearance = appearance32;
            this.listaGridview.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.listaGridview.DisplayLayout.Override.CellPadding = 0;
            this.listaGridview.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            appearance33.BackColor = System.Drawing.SystemColors.Control;
            appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance33.BorderColor = System.Drawing.SystemColors.Window;
            this.listaGridview.DisplayLayout.Override.GroupByRowAppearance = appearance33;
            appearance34.TextHAlignAsString = "Left";
            this.listaGridview.DisplayLayout.Override.HeaderAppearance = appearance34;
            this.listaGridview.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.listaGridview.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance35.BackColor = System.Drawing.SystemColors.Window;
            appearance35.BorderColor = System.Drawing.Color.Silver;
            this.listaGridview.DisplayLayout.Override.RowAppearance = appearance35;
            this.listaGridview.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.listaGridview.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.listaGridview.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
            this.listaGridview.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
            this.listaGridview.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.listaGridview.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.listaGridview.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.listaGridview.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.listaGridview.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.listaGridview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaGridview.Location = new System.Drawing.Point(3, 3);
            this.listaGridview.Name = "listaGridview";
            this.listaGridview.Size = new System.Drawing.Size(694, 153);
            this.listaGridview.TabIndex = 0;
            this.listaGridview.Text = "ultraGrid1";
            // 
            // camposPanel
            // 
            this.camposPanel.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.camposPanel.Location = new System.Drawing.Point(15, 23);
            this.camposPanel.Name = "camposPanel";
            this.camposPanel.Size = new System.Drawing.Size(700, 137);
            this.camposPanel.TabIndex = 3;
            this.camposPanel.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // frmAdministracion_Fill_Panel
            // 
            // 
            // frmAdministracion_Fill_Panel.ClientArea
            // 
            this.frmAdministracion_Fill_Panel.ClientArea.Controls.Add(this.camposPanel);
            this.frmAdministracion_Fill_Panel.ClientArea.Controls.Add(this.panel1);
            this.frmAdministracion_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frmAdministracion_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmAdministracion_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frmAdministracion_Fill_Panel.Name = "frmAdministracion_Fill_Panel";
            this.frmAdministracion_Fill_Panel.Size = new System.Drawing.Size(740, 390);
            this.frmAdministracion_Fill_Panel.TabIndex = 1;
            // 
            // frmAdministracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 426);
            this.Controls.Add(this.frmAdministracion_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "FrmAdministracion";
            this.Load += new System.EventHandler(this.FrmAdministracionLoad);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listaGridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camposPanel)).EndInit();
            this.frmAdministracion_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frmAdministracion_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        protected Infragistics.Win.UltraWinGrid.UltraGrid listaGridview;
        protected Infragistics.Win.Misc.UltraGroupBox camposPanel;
        private Infragistics.Win.Misc.UltraPanel frmAdministracion_Fill_Panel;
    }
}