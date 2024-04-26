namespace TarifariosUI
{
    partial class frmAdministrarAseguradoras
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.dtpFinConvenio = new System.Windows.Forms.DateTimePicker();
            this.dtpInicioConvenio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFinConvenio = new System.Windows.Forms.Label();
            this.lblFechaInicioConvenio = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblRUC = new System.Windows.Forms.Label();
            this.txtRUC = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkConConvenio = new System.Windows.Forms.CheckBox();
            this.grpFechasConvenio = new System.Windows.Forms.GroupBox();
            this.cboTipoEmpresa = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.MaskedTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listaGridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camposPanel)).BeginInit();
            this.camposPanel.SuspendLayout();
            this.grpFechasConvenio.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(12, 221);
            this.panel1.Size = new System.Drawing.Size(481, 135);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // listaGridview
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.listaGridview.DisplayLayout.Appearance = appearance1;
            this.listaGridview.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.listaGridview.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.listaGridview.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.listaGridview.DisplayLayout.GroupByBox.Hidden = true;
            this.listaGridview.DisplayLayout.MaxColScrollRegions = 1;
            this.listaGridview.DisplayLayout.MaxRowScrollRegions = 1;
            appearance2.BackColor = System.Drawing.SystemColors.Window;
            appearance2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listaGridview.DisplayLayout.Override.ActiveCellAppearance = appearance2;
            appearance3.BackColor = System.Drawing.SystemColors.Highlight;
            appearance3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.listaGridview.DisplayLayout.Override.ActiveRowAppearance = appearance3;
            this.listaGridview.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.listaGridview.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.listaGridview.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.listaGridview.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.listaGridview.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            this.listaGridview.DisplayLayout.Override.CardAreaAppearance = appearance4;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.listaGridview.DisplayLayout.Override.CellAppearance = appearance5;
            this.listaGridview.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.listaGridview.DisplayLayout.Override.CellPadding = 0;
            this.listaGridview.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            appearance6.BackColor = System.Drawing.SystemColors.Control;
            appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance6.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance6.BorderColor = System.Drawing.SystemColors.Window;
            this.listaGridview.DisplayLayout.Override.GroupByRowAppearance = appearance6;
            appearance7.TextHAlignAsString = "Left";
            this.listaGridview.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.listaGridview.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.listaGridview.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance8.BackColor = System.Drawing.SystemColors.Window;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            this.listaGridview.DisplayLayout.Override.RowAppearance = appearance8;
            this.listaGridview.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.listaGridview.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.listaGridview.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            appearance9.BackColor = System.Drawing.SystemColors.ControlLight;
            this.listaGridview.DisplayLayout.Override.TemplateAddRowAppearance = appearance9;
            this.listaGridview.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.listaGridview.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.listaGridview.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.listaGridview.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.listaGridview.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.listaGridview.Size = new System.Drawing.Size(427, 129);
            this.listaGridview.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.listaGridview_InitializeLayout);
            this.listaGridview.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.listaGridview_DoubleClickCell);
            // 
            // camposPanel
            // 
            this.camposPanel.BackColorInternal = System.Drawing.Color.Gainsboro;
            this.camposPanel.Controls.Add(this.txtTelefono);
            this.camposPanel.Controls.Add(this.txtCiudad);
            this.camposPanel.Controls.Add(this.lblTipo);
            this.camposPanel.Controls.Add(this.cboTipoEmpresa);
            this.camposPanel.Controls.Add(this.grpFechasConvenio);
            this.camposPanel.Controls.Add(this.chkConConvenio);
            this.camposPanel.Controls.Add(this.label2);
            this.camposPanel.Controls.Add(this.label1);
            this.camposPanel.Controls.Add(this.txtCodigo);
            this.camposPanel.Controls.Add(this.lblTelefono);
            this.camposPanel.Controls.Add(this.lblRUC);
            this.camposPanel.Controls.Add(this.txtRUC);
            this.camposPanel.Controls.Add(this.lblDireccion);
            this.camposPanel.Controls.Add(this.txtDireccion);
            this.camposPanel.Controls.Add(this.lblNombre);
            this.camposPanel.Controls.Add(this.txtNombre);
            this.camposPanel.Location = new System.Drawing.Point(12, 16);
            this.camposPanel.Size = new System.Drawing.Size(573, 199);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(15, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Codigo:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.Gainsboro;
            this.txtCodigo.Location = new System.Drawing.Point(94, 44);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(99, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // dtpFinConvenio
            // 
            this.dtpFinConvenio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFinConvenio.Location = new System.Drawing.Point(261, 12);
            this.dtpFinConvenio.Name = "dtpFinConvenio";
            this.dtpFinConvenio.Size = new System.Drawing.Size(86, 20);
            this.dtpFinConvenio.TabIndex = 1;
            this.dtpFinConvenio.Value = new System.DateTime(2010, 6, 15, 0, 0, 0, 0);
            this.dtpFinConvenio.ValueChanged += new System.EventHandler(this.dtpFinConvenio_ValueChanged);
            // 
            // dtpInicioConvenio
            // 
            this.dtpInicioConvenio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInicioConvenio.Location = new System.Drawing.Point(84, 12);
            this.dtpInicioConvenio.Name = "dtpInicioConvenio";
            this.dtpInicioConvenio.Size = new System.Drawing.Size(87, 20);
            this.dtpInicioConvenio.TabIndex = 0;
            this.dtpInicioConvenio.ValueChanged += new System.EventHandler(this.dtpInicioConvenio_ValueChanged);
            this.dtpInicioConvenio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtpInicioConvenio_KeyPress);
            // 
            // lblFechaFinConvenio
            // 
            this.lblFechaFinConvenio.AutoSize = true;
            this.lblFechaFinConvenio.Location = new System.Drawing.Point(186, 16);
            this.lblFechaFinConvenio.Name = "lblFechaFinConvenio";
            this.lblFechaFinConvenio.Size = new System.Drawing.Size(69, 13);
            this.lblFechaFinConvenio.TabIndex = 25;
            this.lblFechaFinConvenio.Text = "Fin Convenio";
            // 
            // lblFechaInicioConvenio
            // 
            this.lblFechaInicioConvenio.AutoSize = true;
            this.lblFechaInicioConvenio.Location = new System.Drawing.Point(6, 16);
            this.lblFechaInicioConvenio.Name = "lblFechaInicioConvenio";
            this.lblFechaInicioConvenio.Size = new System.Drawing.Size(72, 13);
            this.lblFechaInicioConvenio.TabIndex = 24;
            this.lblFechaInicioConvenio.Text = "Ini.  Convenio";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.BackColor = System.Drawing.Color.Transparent;
            this.lblTelefono.Location = new System.Drawing.Point(15, 129);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(52, 13);
            this.lblTelefono.TabIndex = 23;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // lblRUC
            // 
            this.lblRUC.AutoSize = true;
            this.lblRUC.BackColor = System.Drawing.Color.Transparent;
            this.lblRUC.Location = new System.Drawing.Point(337, 99);
            this.lblRUC.Name = "lblRUC";
            this.lblRUC.Size = new System.Drawing.Size(30, 13);
            this.lblRUC.TabIndex = 21;
            this.lblRUC.Text = "RUC";
            // 
            // txtRUC
            // 
            this.txtRUC.Location = new System.Drawing.Point(379, 96);
            this.txtRUC.MaxLength = 13;
            this.txtRUC.Name = "txtRUC";
            this.txtRUC.ReadOnly = true;
            this.txtRUC.Size = new System.Drawing.Size(186, 20);
            this.txtRUC.TabIndex = 5;
            this.txtRUC.TextChanged += new System.EventHandler(this.txtRUC_TextChanged);
            this.txtRUC.Leave += new System.EventHandler(this.txtRUC_Leave);
            this.txtRUC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRUC_KeyPress);
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.BackColor = System.Drawing.Color.Transparent;
            this.lblDireccion.Location = new System.Drawing.Point(15, 73);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(55, 13);
            this.lblDireccion.TabIndex = 19;
            this.lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(94, 70);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(471, 20);
            this.txtDireccion.TabIndex = 3;
            this.txtDireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccion_KeyPress);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Location = new System.Drawing.Point(218, 47);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 17;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Location = new System.Drawing.Point(271, 44);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(294, 20);
            this.txtNombre.TabIndex = 2;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(15, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Ciudad:";
            // 
            // chkConConvenio
            // 
            this.chkConConvenio.AutoSize = true;
            this.chkConConvenio.BackColor = System.Drawing.Color.Transparent;
            this.chkConConvenio.Checked = true;
            this.chkConConvenio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConConvenio.Location = new System.Drawing.Point(94, 164);
            this.chkConConvenio.Name = "chkConConvenio";
            this.chkConConvenio.Size = new System.Drawing.Size(71, 17);
            this.chkConConvenio.TabIndex = 7;
            this.chkConConvenio.Text = "Convenio";
            this.chkConConvenio.UseVisualStyleBackColor = false;
            this.chkConConvenio.CheckedChanged += new System.EventHandler(this.chkConConvenio_CheckedChanged);
            // 
            // grpFechasConvenio
            // 
            this.grpFechasConvenio.BackColor = System.Drawing.Color.Transparent;
            this.grpFechasConvenio.Controls.Add(this.dtpFinConvenio);
            this.grpFechasConvenio.Controls.Add(this.dtpInicioConvenio);
            this.grpFechasConvenio.Controls.Add(this.lblFechaFinConvenio);
            this.grpFechasConvenio.Controls.Add(this.lblFechaInicioConvenio);
            this.grpFechasConvenio.Location = new System.Drawing.Point(171, 152);
            this.grpFechasConvenio.Name = "grpFechasConvenio";
            this.grpFechasConvenio.Size = new System.Drawing.Size(361, 38);
            this.grpFechasConvenio.TabIndex = 34;
            this.grpFechasConvenio.TabStop = false;
            // 
            // cboTipoEmpresa
            // 
            this.cboTipoEmpresa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTipoEmpresa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTipoEmpresa.BackColor = System.Drawing.Color.LightGray;
            this.cboTipoEmpresa.FormattingEnabled = true;
            this.cboTipoEmpresa.Location = new System.Drawing.Point(94, 17);
            this.cboTipoEmpresa.Name = "cboTipoEmpresa";
            this.cboTipoEmpresa.Size = new System.Drawing.Size(248, 21);
            this.cboTipoEmpresa.TabIndex = 0;
            this.cboTipoEmpresa.SelectedIndexChanged += new System.EventHandler(this.cboTipoEmpresa_SelectedIndexChanged);
            this.cboTipoEmpresa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboTipoEmpresa_KeyPress);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.BackColor = System.Drawing.Color.Transparent;
            this.lblTipo.Location = new System.Drawing.Point(15, 20);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(75, 13);
            this.lblTipo.TabIndex = 36;
            this.lblTipo.Text = "Tipo Empresa:";
            // 
            // txtCiudad
            // 
            this.txtCiudad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCiudad.Location = new System.Drawing.Point(94, 97);
            this.txtCiudad.MaxLength = 13;
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.ReadOnly = true;
            this.txtCiudad.Size = new System.Drawing.Size(186, 20);
            this.txtCiudad.TabIndex = 4;
            this.txtCiudad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCiudad_KeyDown);
            this.txtCiudad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCiudad_KeyPress);
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTelefono.Location = new System.Drawing.Point(94, 126);
            this.txtTelefono.Mask = "00-000-0000";
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(99, 20);
            this.txtTelefono.TabIndex = 6;
            this.txtTelefono.Leave += new System.EventHandler(this.txtTelefono_Leave);
            this.txtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefono_KeyPress);
            // 
            // frmAdministrarAseguradoras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(597, 419);
            this.Name = "frmAdministrarAseguradoras";
            this.Text = "Administración de Empresas y Aseguradoras";
            this.Load += new System.EventHandler(this.frmAdministrarAseguradoras2_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listaGridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camposPanel)).EndInit();
            this.camposPanel.ResumeLayout(false);
            this.camposPanel.PerformLayout();
            this.grpFechasConvenio.ResumeLayout(false);
            this.grpFechasConvenio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.DateTimePicker dtpFinConvenio;
        private System.Windows.Forms.DateTimePicker dtpInicioConvenio;
        private System.Windows.Forms.Label lblFechaFinConvenio;
        private System.Windows.Forms.Label lblFechaInicioConvenio;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblRUC;
        private System.Windows.Forms.TextBox txtRUC;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkConConvenio;
        private System.Windows.Forms.GroupBox grpFechasConvenio;
        private System.Windows.Forms.ComboBox cboTipoEmpresa;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.MaskedTextBox txtTelefono;
    }
}
