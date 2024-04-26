namespace His.Honorarios
{
    partial class frm_TipoDocumento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_TipoDocumento));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.chk_estado = new System.Windows.Forms.CheckBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_codigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridtipodocmento = new System.Windows.Forms.DataGridView();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frm_TipoDocumento_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            this.menu.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridtipodocmento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            this.frm_TipoDocumento_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_TipoDocumento_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
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
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(535, 36);
            this.menu.TabIndex = 64;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(58, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(70, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(63, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(66, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(69, 33);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(47, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // chk_estado
            // 
            this.chk_estado.AutoSize = true;
            this.chk_estado.BackColor = System.Drawing.Color.Transparent;
            this.chk_estado.Location = new System.Drawing.Point(137, 78);
            this.chk_estado.Name = "chk_estado";
            this.chk_estado.Size = new System.Drawing.Size(56, 17);
            this.chk_estado.TabIndex = 17;
            this.chk_estado.Text = "Activo";
            this.chk_estado.UseVisualStyleBackColor = false;
            // 
            // txt_nombre
            // 
            this.txt_nombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nombre.Location = new System.Drawing.Point(137, 52);
            this.txt_nombre.MaxLength = 40;
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(228, 20);
            this.txt_nombre.TabIndex = 14;
            this.txt_nombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_nombre_KeyPress);
            // 
            // txt_codigo
            // 
            this.txt_codigo.Location = new System.Drawing.Point(137, 26);
            this.txt_codigo.Name = "txt_codigo";
            this.txt_codigo.ReadOnly = true;
            this.txt_codigo.Size = new System.Drawing.Size(100, 20);
            this.txt_codigo.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(55, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nombre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(55, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Código";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.gridtipodocmento);
            this.panel2.Location = new System.Drawing.Point(27, 167);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(476, 156);
            this.panel2.TabIndex = 67;
            // 
            // gridtipodocmento
            // 
            this.gridtipodocmento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridtipodocmento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridtipodocmento.Location = new System.Drawing.Point(3, 3);
            this.gridtipodocmento.Name = "gridtipodocmento";
            this.gridtipodocmento.ReadOnly = true;
            this.gridtipodocmento.Size = new System.Drawing.Size(470, 150);
            this.gridtipodocmento.TabIndex = 0;
            this.gridtipodocmento.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridtipodocmento_ColumnHeaderMouseClick);
            this.gridtipodocmento.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridtipodocmento_RowHeaderMouseDoubleClick);
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
            this.mnuContexsecundario.Size = new System.Drawing.Size(128, 54);
            // 
            // mnuBuscar
            // 
            this.mnuBuscar.Name = "mnuBuscar";
            this.mnuBuscar.Size = new System.Drawing.Size(127, 22);
            this.mnuBuscar.Text = "Buscar";
            this.mnuBuscar.Click += new System.EventHandler(this.mnuBuscar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // cancelarToolStripMenuItem
            // 
            this.cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            this.cancelarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.cancelarToolStripMenuItem.Text = "Cancelar";
            // 
            // frm_TipoDocumento_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_TipoDocumento_Fill_Panel.Appearance = appearance1;
            // 
            // frm_TipoDocumento_Fill_Panel.ClientArea
            // 
            this.frm_TipoDocumento_Fill_Panel.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.frm_TipoDocumento_Fill_Panel.ClientArea.Controls.Add(this.panel2);
            this.frm_TipoDocumento_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_TipoDocumento_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_TipoDocumento_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frm_TipoDocumento_Fill_Panel.Name = "frm_TipoDocumento_Fill_Panel";
            this.frm_TipoDocumento_Fill_Panel.Size = new System.Drawing.Size(535, 374);
            this.frm_TipoDocumento_Fill_Panel.TabIndex = 65;
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.Controls.Add(this.chk_estado);
            this.grpDatosPrincipales.Controls.Add(this.txt_codigo);
            this.grpDatosPrincipales.Controls.Add(this.txt_nombre);
            this.grpDatosPrincipales.Controls.Add(this.label1);
            this.grpDatosPrincipales.Controls.Add(this.label2);
            this.grpDatosPrincipales.Location = new System.Drawing.Point(27, 31);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(476, 116);
            this.grpDatosPrincipales.TabIndex = 69;
            this.grpDatosPrincipales.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // frm_TipoDocumento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(535, 410);
            this.Controls.Add(this.frm_TipoDocumento_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "frm_TipoDocumento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tipo de Documento";
            this.Load += new System.EventHandler(this.frm_TipoDocumento_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridtipodocmento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            this.frm_TipoDocumento_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_TipoDocumento_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.CheckBox chk_estado;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_codigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridtipodocmento;
        private System.Windows.Forms.ErrorProvider controlErrores;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem mnuBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private Infragistics.Win.Misc.UltraPanel frm_TipoDocumento_Fill_Panel;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
    }
}