namespace His.Honorarios
{
    partial class frm_Especialidades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Especialidades));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridespercialidades = new System.Windows.Forms.DataGridView();
            this.txt_cuentacontable = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chk_eliminado = new System.Windows.Forms.CheckBox();
            this.cmb_esppadre = new System.Windows.Forms.ComboBox();
            this.txt_descripcion = new System.Windows.Forms.TextBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_codigo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.controlErrores = new System.Windows.Forms.ErrorProvider();
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip();
            this.mnuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frm_Especialidades_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            this.gridEspecialidades = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.menu.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridespercialidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            this.frm_Especialidades_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_Especialidades_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEspecialidades)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnActualizar,
            this.btnGuardar,
            this.btnEliminar,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(809, 36);
            this.menu.TabIndex = 63;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(62, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(78, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(69, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(70, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 33);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(49, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.gridespercialidades);
            this.panel2.Location = new System.Drawing.Point(26, 216);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(499, 176);
            this.panel2.TabIndex = 65;
            // 
            // gridespercialidades
            // 
            this.gridespercialidades.AllowUserToAddRows = false;
            this.gridespercialidades.AllowUserToDeleteRows = false;
            this.gridespercialidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridespercialidades.Location = new System.Drawing.Point(3, 3);
            this.gridespercialidades.Name = "gridespercialidades";
            this.gridespercialidades.ReadOnly = true;
            this.gridespercialidades.Size = new System.Drawing.Size(493, 170);
            this.gridespercialidades.TabIndex = 0;
            this.gridespercialidades.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridespercialidades_CellContentClick);
            this.gridespercialidades.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridespercialidades_ColumnHeaderMouseClick);
            this.gridespercialidades.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridespercialidades_RowHeaderMouseDoubleClick);
            // 
            // txt_cuentacontable
            // 
            this.txt_cuentacontable.Location = new System.Drawing.Point(184, 146);
            this.txt_cuentacontable.Mask = "000000-000";
            this.txt_cuentacontable.Name = "txt_cuentacontable";
            this.txt_cuentacontable.Size = new System.Drawing.Size(100, 20);
            this.txt_cuentacontable.TabIndex = 3;
            this.txt_cuentacontable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cuentacontable_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(51, 150);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 53;
            this.label15.Text = "Cuenta Contable:";
            // 
            // chk_eliminado
            // 
            this.chk_eliminado.AutoSize = true;
            this.chk_eliminado.BackColor = System.Drawing.Color.Transparent;
            this.chk_eliminado.Location = new System.Drawing.Point(184, 172);
            this.chk_eliminado.Name = "chk_eliminado";
            this.chk_eliminado.Size = new System.Drawing.Size(56, 17);
            this.chk_eliminado.TabIndex = 4;
            this.chk_eliminado.Text = "Activo";
            this.chk_eliminado.UseVisualStyleBackColor = false;
            // 
            // cmb_esppadre
            // 
            this.cmb_esppadre.FormattingEnabled = true;
            this.cmb_esppadre.Location = new System.Drawing.Point(184, 115);
            this.cmb_esppadre.Name = "cmb_esppadre";
            this.cmb_esppadre.Size = new System.Drawing.Size(228, 21);
            this.cmb_esppadre.TabIndex = 2;
            this.cmb_esppadre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_esppadre_KeyPress);
            // 
            // txt_descripcion
            // 
            this.txt_descripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_descripcion.Location = new System.Drawing.Point(184, 82);
            this.txt_descripcion.MaxLength = 255;
            this.txt_descripcion.Name = "txt_descripcion";
            this.txt_descripcion.Size = new System.Drawing.Size(228, 20);
            this.txt_descripcion.TabIndex = 1;
            this.txt_descripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_descripcion_KeyPress);
            // 
            // txt_nombre
            // 
            this.txt_nombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nombre.Location = new System.Drawing.Point(184, 50);
            this.txt_nombre.MaxLength = 70;
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(228, 20);
            this.txt_nombre.TabIndex = 0;
            this.txt_nombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_nombre_KeyPress);
            // 
            // txt_codigo
            // 
            this.txt_codigo.Location = new System.Drawing.Point(184, 24);
            this.txt_codigo.Name = "txt_codigo";
            this.txt_codigo.ReadOnly = true;
            this.txt_codigo.Size = new System.Drawing.Size(100, 20);
            this.txt_codigo.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(51, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Especialidad Superior";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(51, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Descripción";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(51, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nombre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(51, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Código";
            // 
            // controlErrores
            // 
            this.controlErrores.ContainerControl = this;
            // 
            // mnuContexsecundario
            // 
            this.mnuContexsecundario.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBuscar,
            this.toolStripSeparator1,
            this.cancelarToolStripMenuItem});
            this.mnuContexsecundario.Name = "mnuContext";
            this.mnuContexsecundario.Size = new System.Drawing.Size(121, 54);
            // 
            // mnuBuscar
            // 
            this.mnuBuscar.Name = "mnuBuscar";
            this.mnuBuscar.Size = new System.Drawing.Size(120, 22);
            this.mnuBuscar.Text = "Buscar";
            this.mnuBuscar.Click += new System.EventHandler(this.mnuBuscar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(117, 6);
            // 
            // cancelarToolStripMenuItem
            // 
            this.cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            this.cancelarToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.cancelarToolStripMenuItem.Text = "Cancelar";
            // 
            // frm_Especialidades_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_Especialidades_Fill_Panel.Appearance = appearance1;
            // 
            // frm_Especialidades_Fill_Panel.ClientArea
            // 
            this.frm_Especialidades_Fill_Panel.ClientArea.Controls.Add(this.gridEspecialidades);
            this.frm_Especialidades_Fill_Panel.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.frm_Especialidades_Fill_Panel.ClientArea.Controls.Add(this.panel2);
            this.frm_Especialidades_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_Especialidades_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_Especialidades_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frm_Especialidades_Fill_Panel.Name = "frm_Especialidades_Fill_Panel";
            this.frm_Especialidades_Fill_Panel.Size = new System.Drawing.Size(809, 443);
            this.frm_Especialidades_Fill_Panel.TabIndex = 64;
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.Controls.Add(this.txt_cuentacontable);
            this.grpDatosPrincipales.Controls.Add(this.txt_nombre);
            this.grpDatosPrincipales.Controls.Add(this.label15);
            this.grpDatosPrincipales.Controls.Add(this.label1);
            this.grpDatosPrincipales.Controls.Add(this.chk_eliminado);
            this.grpDatosPrincipales.Controls.Add(this.label2);
            this.grpDatosPrincipales.Controls.Add(this.cmb_esppadre);
            this.grpDatosPrincipales.Controls.Add(this.label3);
            this.grpDatosPrincipales.Controls.Add(this.txt_descripcion);
            this.grpDatosPrincipales.Controls.Add(this.label4);
            this.grpDatosPrincipales.Controls.Add(this.txt_codigo);
            this.grpDatosPrincipales.Location = new System.Drawing.Point(26, 11);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(499, 199);
            this.grpDatosPrincipales.TabIndex = 67;
            this.grpDatosPrincipales.Text = "Dato de la Especialidad";
            this.grpDatosPrincipales.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // gridEspecialidades
            // 
            this.gridEspecialidades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance53.BackColor = System.Drawing.SystemColors.Window;
            appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridEspecialidades.DisplayLayout.Appearance = appearance53;
            this.gridEspecialidades.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.gridEspecialidades.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance54.BorderColor = System.Drawing.SystemColors.Window;
            this.gridEspecialidades.DisplayLayout.GroupByBox.Appearance = appearance54;
            appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridEspecialidades.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
            this.gridEspecialidades.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance56.BackColor2 = System.Drawing.SystemColors.Control;
            appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridEspecialidades.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
            this.gridEspecialidades.DisplayLayout.MaxColScrollRegions = 1;
            this.gridEspecialidades.DisplayLayout.MaxRowScrollRegions = 1;
            appearance57.BackColor = System.Drawing.SystemColors.Window;
            appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridEspecialidades.DisplayLayout.Override.ActiveCellAppearance = appearance57;
            appearance58.BackColor = System.Drawing.SystemColors.Highlight;
            appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridEspecialidades.DisplayLayout.Override.ActiveRowAppearance = appearance58;
            this.gridEspecialidades.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.gridEspecialidades.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance59.BackColor = System.Drawing.SystemColors.Window;
            this.gridEspecialidades.DisplayLayout.Override.CardAreaAppearance = appearance59;
            appearance60.BorderColor = System.Drawing.Color.Silver;
            appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.gridEspecialidades.DisplayLayout.Override.CellAppearance = appearance60;
            this.gridEspecialidades.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.gridEspecialidades.DisplayLayout.Override.CellPadding = 0;
            appearance61.BackColor = System.Drawing.SystemColors.Control;
            appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance61.BorderColor = System.Drawing.SystemColors.Window;
            this.gridEspecialidades.DisplayLayout.Override.GroupByRowAppearance = appearance61;
            appearance62.TextHAlignAsString = "Left";
            this.gridEspecialidades.DisplayLayout.Override.HeaderAppearance = appearance62;
            this.gridEspecialidades.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.gridEspecialidades.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance63.BackColor = System.Drawing.SystemColors.Window;
            appearance63.BorderColor = System.Drawing.Color.Silver;
            this.gridEspecialidades.DisplayLayout.Override.RowAppearance = appearance63;
            this.gridEspecialidades.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gridEspecialidades.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
            this.gridEspecialidades.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.gridEspecialidades.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.gridEspecialidades.Location = new System.Drawing.Point(533, 219);
            this.gridEspecialidades.Name = "gridEspecialidades";
            this.gridEspecialidades.Size = new System.Drawing.Size(276, 131);
            this.gridEspecialidades.TabIndex = 68;
            this.gridEspecialidades.TabStop = false;
            this.gridEspecialidades.Text = "ultraGrid1";
            // 
            // frm_Especialidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 479);
            this.Controls.Add(this.frm_Especialidades_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "frm_Especialidades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Especialidades";
            this.Load += new System.EventHandler(this.frm_Especialidades_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridespercialidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            this.frm_Especialidades_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_Especialidades_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEspecialidades)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridespercialidades;
        private System.Windows.Forms.CheckBox chk_eliminado;
        private System.Windows.Forms.ComboBox cmb_esppadre;
        private System.Windows.Forms.TextBox txt_descripcion;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_codigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider controlErrores;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem mnuBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private System.Windows.Forms.MaskedTextBox txt_cuentacontable;
        private System.Windows.Forms.Label label15;
        private Infragistics.Win.Misc.UltraPanel frm_Especialidades_Fill_Panel;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridEspecialidades;
    }
}