namespace TarifariosUI
{
    partial class frm_PreciosConvenios
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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinScrollBar.ScrollBarLook scrollBarLook1 = new Infragistics.Win.UltraWinScrollBar.ScrollBarLook();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtFechaFin = new System.Windows.Forms.TextBox();
            this.txtFechaInicio = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.cboTipoEmpresa = new System.Windows.Forms.ComboBox();
            this.lblTipoEmpresa = new System.Windows.Forms.Label();
            this.Cmb_Convenios = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.gridPconvenios = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPconvenios)).BeginInit();
            this.menu.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaFin.Location = new System.Drawing.Point(288, 113);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(54, 13);
            this.lblFechaFin.TabIndex = 17;
            this.lblFechaFin.Text = "Fecha Fin";
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaInicio.Location = new System.Drawing.Point(68, 113);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(65, 13);
            this.lblFechaInicio.TabIndex = 16;
            this.lblFechaInicio.Text = "Fecha Inicio";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.BackColor = System.Drawing.Color.Transparent;
            this.lblDescripcion.Location = new System.Drawing.Point(70, 87);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblDescripcion.TabIndex = 15;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // txtFechaFin
            // 
            this.txtFechaFin.Location = new System.Drawing.Point(411, 110);
            this.txtFechaFin.Name = "txtFechaFin";
            this.txtFechaFin.ReadOnly = true;
            this.txtFechaFin.Size = new System.Drawing.Size(125, 20);
            this.txtFechaFin.TabIndex = 14;
            // 
            // txtFechaInicio
            // 
            this.txtFechaInicio.Location = new System.Drawing.Point(139, 110);
            this.txtFechaInicio.Name = "txtFechaInicio";
            this.txtFechaInicio.ReadOnly = true;
            this.txtFechaInicio.Size = new System.Drawing.Size(126, 20);
            this.txtFechaInicio.TabIndex = 13;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(139, 84);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(397, 20);
            this.txtDescripcion.TabIndex = 12;
            // 
            // cboTipoEmpresa
            // 
            this.cboTipoEmpresa.FormattingEnabled = true;
            this.cboTipoEmpresa.Location = new System.Drawing.Point(139, 30);
            this.cboTipoEmpresa.Name = "cboTipoEmpresa";
            this.cboTipoEmpresa.Size = new System.Drawing.Size(214, 21);
            this.cboTipoEmpresa.TabIndex = 11;
            this.cboTipoEmpresa.SelectedIndexChanged += new System.EventHandler(this.cboTipoEmpresa_SelectedIndexChanged);
            // 
            // lblTipoEmpresa
            // 
            this.lblTipoEmpresa.AutoSize = true;
            this.lblTipoEmpresa.BackColor = System.Drawing.Color.Transparent;
            this.lblTipoEmpresa.Location = new System.Drawing.Point(61, 33);
            this.lblTipoEmpresa.Name = "lblTipoEmpresa";
            this.lblTipoEmpresa.Size = new System.Drawing.Size(72, 13);
            this.lblTipoEmpresa.TabIndex = 10;
            this.lblTipoEmpresa.Text = "Tipo Empresa";
            // 
            // Cmb_Convenios
            // 
            this.Cmb_Convenios.FormattingEnabled = true;
            this.Cmb_Convenios.Location = new System.Drawing.Point(139, 57);
            this.Cmb_Convenios.Name = "Cmb_Convenios";
            this.Cmb_Convenios.Size = new System.Drawing.Size(397, 21);
            this.Cmb_Convenios.TabIndex = 9;
            this.Cmb_Convenios.SelectedIndexChanged += new System.EventHandler(this.Cmb_Convenios_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(76, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Convenios";
            // 
            // controlErrores
            // 
            this.controlErrores.ContainerControl = this;
            // 
            // gridPconvenios
            // 
            this.gridPconvenios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            this.gridPconvenios.DisplayLayout.AddNewBox.Appearance = appearance1;
            this.gridPconvenios.DisplayLayout.AddNewBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            appearance2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance2.ImageBackgroundAlpha = Infragistics.Win.Alpha.UseAlphaLevel;
            appearance2.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(6, 3, 6, 3);
            appearance2.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.gridPconvenios.DisplayLayout.AddNewBox.ButtonAppearance = appearance2;
            this.gridPconvenios.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            this.gridPconvenios.DisplayLayout.AddNewBox.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            appearance3.BackColor = System.Drawing.Color.White;
            this.gridPconvenios.DisplayLayout.Appearance = appearance3;
            appearance4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance4.FontData.Name = "Trebuchet MS";
            appearance4.FontData.SizeInPoints = 9F;
            appearance4.ForeColor = System.Drawing.Color.White;
            appearance4.TextHAlignAsString = "Right";
            this.gridPconvenios.DisplayLayout.CaptionAppearance = appearance4;
            appearance5.FontData.BoldAsString = "True";
            appearance5.FontData.Name = "Trebuchet MS";
            appearance5.FontData.SizeInPoints = 10F;
            appearance5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(127)))), ((int)(((byte)(177)))));
            appearance5.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(0, 2, 0, 3);
            appearance5.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.gridPconvenios.DisplayLayout.GroupByBox.Appearance = appearance5;
            this.gridPconvenios.DisplayLayout.GroupByBox.ButtonBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.gridPconvenios.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
            this.gridPconvenios.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.None;
            this.gridPconvenios.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
            this.gridPconvenios.DisplayLayout.Override.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            appearance6.BackColor = System.Drawing.Color.Transparent;
            this.gridPconvenios.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance7.BorderColor = System.Drawing.Color.Transparent;
            appearance7.FontData.Name = "Verdana";
            this.gridPconvenios.DisplayLayout.Override.CellAppearance = appearance7;
            appearance8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance8.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(6, 3, 6, 3);
            appearance8.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.gridPconvenios.DisplayLayout.Override.CellButtonAppearance = appearance8;
            appearance9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.gridPconvenios.DisplayLayout.Override.FilterCellAppearance = appearance9;
            appearance10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance10.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(6, 3, 6, 3);
            this.gridPconvenios.DisplayLayout.Override.FilterClearButtonAppearance = appearance10;
            appearance11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            appearance11.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.gridPconvenios.DisplayLayout.Override.FilterRowPromptAppearance = appearance11;
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance12.FontData.BoldAsString = "True";
            appearance12.FontData.Name = "Trebuchet MS";
            appearance12.FontData.SizeInPoints = 10F;
            appearance12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            appearance12.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Tiled;
            appearance12.TextHAlignAsString = "Left";
            appearance12.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.gridPconvenios.DisplayLayout.Override.HeaderAppearance = appearance12;
            this.gridPconvenios.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.XPThemed;
            appearance13.BorderColor = System.Drawing.Color.Transparent;
            this.gridPconvenios.DisplayLayout.Override.RowAppearance = appearance13;
            appearance14.BackColor = System.Drawing.Color.White;
            this.gridPconvenios.DisplayLayout.Override.RowSelectorAppearance = appearance14;
            appearance15.BorderColor = System.Drawing.Color.Transparent;
            appearance15.ForeColor = System.Drawing.Color.Black;
            this.gridPconvenios.DisplayLayout.Override.SelectedCellAppearance = appearance15;
            appearance16.BorderColor = System.Drawing.Color.Transparent;
            appearance16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance16.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(1, 1, 1, 4);
            appearance16.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.gridPconvenios.DisplayLayout.Override.SelectedRowAppearance = appearance16;
            appearance17.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(2, 4, 2, 4);
            appearance17.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            scrollBarLook1.Appearance = appearance17;
            appearance18.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(3, 2, 3, 2);
            scrollBarLook1.AppearanceHorizontal = appearance18;
            appearance19.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(2, 3, 2, 3);
            appearance19.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            scrollBarLook1.AppearanceVertical = appearance19;
            appearance20.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(0, 2, 0, 1);
            scrollBarLook1.TrackAppearanceHorizontal = appearance20;
            appearance21.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(2, 0, 1, 0);
            appearance21.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            scrollBarLook1.TrackAppearanceVertical = appearance21;
            this.gridPconvenios.DisplayLayout.ScrollBarLook = scrollBarLook1;
            this.gridPconvenios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.gridPconvenios.Location = new System.Drawing.Point(13, 174);
            this.gridPconvenios.Name = "gridPconvenios";
            this.gridPconvenios.Size = new System.Drawing.Size(634, 140);
            this.gridPconvenios.TabIndex = 15;
            this.gridPconvenios.Text = "Grid Caption Area";
            this.gridPconvenios.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.gridPconvenios.InitializeGroupByRow += new Infragistics.Win.UltraWinGrid.InitializeGroupByRowEventHandler(this.gridPconvenios_InitializeGroupByRow);
            this.gridPconvenios.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridPconvenios_InitializeLayout);
            this.gridPconvenios.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.gridPconvenios_CellChange);
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
            this.btnCerrar,
            this.btnImprimir});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(659, 36);
            this.menu.TabIndex = 41;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(54, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(47, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(50, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(53, 33);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // btnCerrar
            // 
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(31, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(49, 33);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // ultraPanel1
            // 
            this.ultraPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance22.BackColor = System.Drawing.Color.LightGray;
            appearance22.BackColor2 = System.Drawing.Color.GhostWhite;
            appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop37;
            this.ultraPanel1.Appearance = appearance22;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.ultraPanel1.ClientArea.Controls.Add(this.gridPconvenios);
            this.ultraPanel1.Location = new System.Drawing.Point(0, 0);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(659, 326);
            this.ultraPanel1.TabIndex = 42;
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.Controls.Add(this.lblFechaFin);
            this.grpDatosPrincipales.Controls.Add(this.txtDescripcion);
            this.grpDatosPrincipales.Controls.Add(this.lblFechaInicio);
            this.grpDatosPrincipales.Controls.Add(this.label5);
            this.grpDatosPrincipales.Controls.Add(this.lblDescripcion);
            this.grpDatosPrincipales.Controls.Add(this.Cmb_Convenios);
            this.grpDatosPrincipales.Controls.Add(this.txtFechaFin);
            this.grpDatosPrincipales.Controls.Add(this.lblTipoEmpresa);
            this.grpDatosPrincipales.Controls.Add(this.txtFechaInicio);
            this.grpDatosPrincipales.Controls.Add(this.cboTipoEmpresa);
            this.grpDatosPrincipales.Location = new System.Drawing.Point(13, 12);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(633, 156);
            this.grpDatosPrincipales.TabIndex = 16;
            this.grpDatosPrincipales.Text = "Datos";
            this.grpDatosPrincipales.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // frm_PreciosConvenios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 326);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.ultraPanel1);
            this.Name = "frm_PreciosConvenios";
            this.Text = "Precios por Convenios";
            this.Load += new System.EventHandler(this.frm_PreciosConvenios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPconvenios)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider controlErrores;
        private System.Windows.Forms.ComboBox Cmb_Convenios;
        private System.Windows.Forms.Label label5;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtFechaFin;
        private System.Windows.Forms.TextBox txtFechaInicio;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.ComboBox cboTipoEmpresa;
        private System.Windows.Forms.Label lblTipoEmpresa;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridPconvenios;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
    }
}