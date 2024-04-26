namespace His.Honorarios
{
    partial class frm_ComisionAprtes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ComisionAprtes));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.cmb_tipoformapago = new System.Windows.Forms.ComboBox();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.txt_formapago = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_aporte = new System.Windows.Forms.TextBox();
            this.txt_comision = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grid_formapago = new System.Windows.Forms.DataGridView();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.frm_ComisionAprtes_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.grpDatosSecundarios = new Infragistics.Win.Misc.UltraGroupBox();
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Pago = new System.Windows.Forms.ComboBox();
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_formapago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.frm_ComisionAprtes_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_ComisionAprtes_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosSecundarios)).BeginInit();
            this.grpDatosSecundarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_tipoformapago
            // 
            this.cmb_tipoformapago.FormattingEnabled = true;
            this.cmb_tipoformapago.Location = new System.Drawing.Point(31, 28);
            this.cmb_tipoformapago.Name = "cmb_tipoformapago";
            this.cmb_tipoformapago.Size = new System.Drawing.Size(418, 21);
            this.cmb_tipoformapago.TabIndex = 0;
            this.cmb_tipoformapago.SelectedIndexChanged += new System.EventHandler(this.cmb_tipoformapago_SelectedIndexChanged);
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnGuardar,
            this.btnModificar,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(4, 28);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(696, 36);
            this.menu.TabIndex = 64;
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
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(69, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Image = ((System.Drawing.Image)(resources.GetObject("btnModificar.Image")));
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(78, 33);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
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
            this.btnCerrar.Image = global::His.Honorarios.Properties.Resources.HIS_SALIR;
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(49, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // txt_formapago
            // 
            this.txt_formapago.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_formapago.Location = new System.Drawing.Point(165, 19);
            this.txt_formapago.Name = "txt_formapago";
            this.txt_formapago.ReadOnly = true;
            this.txt_formapago.Size = new System.Drawing.Size(286, 20);
            this.txt_formapago.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(162, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Aporte de Llamadas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(162, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Comisión de la Clinica";
            // 
            // txt_aporte
            // 
            this.txt_aporte.Location = new System.Drawing.Point(313, 78);
            this.txt_aporte.Name = "txt_aporte";
            this.txt_aporte.Size = new System.Drawing.Size(138, 20);
            this.txt_aporte.TabIndex = 2;
            this.txt_aporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_aporte_KeyPress);
            this.txt_aporte.Leave += new System.EventHandler(this.txt_aporte_Leave);
            // 
            // txt_comision
            // 
            this.txt_comision.Location = new System.Drawing.Point(313, 49);
            this.txt_comision.Name = "txt_comision";
            this.txt_comision.Size = new System.Drawing.Size(138, 20);
            this.txt_comision.TabIndex = 1;
            this.txt_comision.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_comision_KeyPress);
            this.txt_comision.Leave += new System.EventHandler(this.txt_comision_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.grid_formapago);
            this.panel1.Location = new System.Drawing.Point(24, 257);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(652, 274);
            this.panel1.TabIndex = 66;
            // 
            // grid_formapago
            // 
            this.grid_formapago.AllowUserToAddRows = false;
            this.grid_formapago.AllowUserToDeleteRows = false;
            this.grid_formapago.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid_formapago.BackgroundColor = System.Drawing.Color.White;
            this.grid_formapago.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid_formapago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_formapago.Location = new System.Drawing.Point(3, 3);
            this.grid_formapago.Name = "grid_formapago";
            this.grid_formapago.ReadOnly = true;
            this.grid_formapago.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid_formapago.Size = new System.Drawing.Size(646, 268);
            this.grid_formapago.TabIndex = 0;
            this.grid_formapago.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_formapago_ColumnHeaderMouseClick);
            this.grid_formapago.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_formapago_RowHeaderMouseDoubleClick);
            this.grid_formapago.DoubleClick += new System.EventHandler(this.grid_formapago_DoubleClick);
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
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // frm_ComisionAprtes_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_ComisionAprtes_Fill_Panel.Appearance = appearance1;
            // 
            // frm_ComisionAprtes_Fill_Panel.ClientArea
            // 
            this.frm_ComisionAprtes_Fill_Panel.ClientArea.Controls.Add(this.grpDatosSecundarios);
            this.frm_ComisionAprtes_Fill_Panel.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.frm_ComisionAprtes_Fill_Panel.ClientArea.Controls.Add(this.panel1);
            this.frm_ComisionAprtes_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_ComisionAprtes_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_ComisionAprtes_Fill_Panel.Location = new System.Drawing.Point(4, 64);
            this.frm_ComisionAprtes_Fill_Panel.Name = "frm_ComisionAprtes_Fill_Panel";
            this.frm_ComisionAprtes_Fill_Panel.Size = new System.Drawing.Size(696, 541);
            this.frm_ComisionAprtes_Fill_Panel.TabIndex = 65;
            // 
            // grpDatosSecundarios
            // 
            this.grpDatosSecundarios.Controls.Add(this.txt_formapago);
            this.grpDatosSecundarios.Controls.Add(this.label3);
            this.grpDatosSecundarios.Controls.Add(this.txt_comision);
            this.grpDatosSecundarios.Controls.Add(this.label2);
            this.grpDatosSecundarios.Controls.Add(this.txt_aporte);
            this.grpDatosSecundarios.Location = new System.Drawing.Point(24, 116);
            this.grpDatosSecundarios.Name = "grpDatosSecundarios";
            this.grpDatosSecundarios.Size = new System.Drawing.Size(651, 131);
            this.grpDatosSecundarios.TabIndex = 68;
            this.grpDatosSecundarios.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            this.grpDatosSecundarios.Click += new System.EventHandler(this.grpDatosSecundarios_Click);
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.Controls.Add(this.label1);
            this.grpDatosPrincipales.Controls.Add(this.cmb_Pago);
            this.grpDatosPrincipales.Controls.Add(this.cmb_tipoformapago);
            this.grpDatosPrincipales.Location = new System.Drawing.Point(26, 12);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(482, 98);
            this.grpDatosPrincipales.TabIndex = 67;
            this.grpDatosPrincipales.Text = "Tipo de Formas de pago";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pago:";
            // 
            // cmb_Pago
            // 
            this.cmb_Pago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Pago.FormattingEnabled = true;
            this.cmb_Pago.Location = new System.Drawing.Point(69, 60);
            this.cmb_Pago.Name = "cmb_Pago";
            this.cmb_Pago.Size = new System.Drawing.Size(252, 21);
            this.cmb_Pago.TabIndex = 1;
            this.cmb_Pago.SelectedIndexChanged += new System.EventHandler(this.cmb_Pago_SelectedIndexChanged);
            // 
            // _frm_ComisionAprtes_UltraFormManager_Dock_Area_Left
            // 
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 28);
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left.Name = "_frm_ComisionAprtes_UltraFormManager_Dock_Area_Left";
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 577);
            // 
            // _frm_ComisionAprtes_UltraFormManager_Dock_Area_Right
            // 
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(700, 28);
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right.Name = "_frm_ComisionAprtes_UltraFormManager_Dock_Area_Right";
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 577);
            // 
            // _frm_ComisionAprtes_UltraFormManager_Dock_Area_Top
            // 
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top.Name = "_frm_ComisionAprtes_UltraFormManager_Dock_Area_Top";
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(704, 28);
            // 
            // _frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom
            // 
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 605);
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom.Name = "_frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom";
            this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(704, 4);
            // 
            // frm_ComisionAprtes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 609);
            this.Controls.Add(this.frm_ComisionAprtes_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Controls.Add(this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom);
            this.Name = "frm_ComisionAprtes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comisiones de la Clinica y Aporte de LLamadas";
            this.Load += new System.EventHandler(this.frm_ComisionAprtes_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_formapago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.frm_ComisionAprtes_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_ComisionAprtes_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosSecundarios)).EndInit();
            this.grpDatosSecundarios.ResumeLayout(false);
            this.grpDatosSecundarios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_tipoformapago;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView grid_formapago;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_aporte;
        private System.Windows.Forms.TextBox txt_comision;
        private System.Windows.Forms.TextBox txt_formapago;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider controlErrores;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem mnuBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        private Infragistics.Win.Misc.UltraPanel frm_ComisionAprtes_Fill_Panel;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_ComisionAprtes_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_ComisionAprtes_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_ComisionAprtes_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_ComisionAprtes_UltraFormManager_Dock_Area_Bottom;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosSecundarios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Pago;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnModificar;
    }
}