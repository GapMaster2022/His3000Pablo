﻿namespace His.Maintenance
{
    partial class frm_OpcionesAcceso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_OpcionesAcceso));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.cmb_modulo = new System.Windows.Forms.ComboBox();
            this.txt_descripcion = new System.Windows.Forms.TextBox();
            this.txt_codigomodulo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.gridOpacceso = new System.Windows.Forms.DataGridView();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOpacceso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_modulo
            // 
            this.cmb_modulo.FormattingEnabled = true;
            this.cmb_modulo.Location = new System.Drawing.Point(150, 36);
            this.cmb_modulo.Name = "cmb_modulo";
            this.cmb_modulo.Size = new System.Drawing.Size(210, 21);
            this.cmb_modulo.TabIndex = 1;
            this.cmb_modulo.SelectedIndexChanged += new System.EventHandler(this.cmb_modulo_SelectedIndexChanged);
            this.cmb_modulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_modulo_KeyPress);
            // 
            // txt_descripcion
            // 
            this.txt_descripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_descripcion.Location = new System.Drawing.Point(150, 89);
            this.txt_descripcion.MaxLength = 128;
            this.txt_descripcion.Name = "txt_descripcion";
            this.txt_descripcion.Size = new System.Drawing.Size(210, 20);
            this.txt_descripcion.TabIndex = 3;
            this.txt_descripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_descripcion_KeyPress);
            // 
            // txt_codigomodulo
            // 
            this.txt_codigomodulo.Location = new System.Drawing.Point(150, 63);
            this.txt_codigomodulo.Name = "txt_codigomodulo";
            this.txt_codigomodulo.Size = new System.Drawing.Size(210, 20);
            this.txt_codigomodulo.TabIndex = 2;
            this.txt_codigomodulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codigomodulo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(32, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Módulo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(32, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descripción";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(32, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Código de Acceso";
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
            this.menu.Size = new System.Drawing.Size(449, 36);
            this.menu.TabIndex = 57;
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
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(70, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
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
            // gridOpacceso
            // 
            this.gridOpacceso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridOpacceso.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.gridOpacceso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOpacceso.Location = new System.Drawing.Point(24, 145);
            this.gridOpacceso.Name = "gridOpacceso";
            this.gridOpacceso.Size = new System.Drawing.Size(404, 63);
            this.gridOpacceso.TabIndex = 0;
            this.gridOpacceso.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridOpacceso_ColumnHeaderMouseClick);
            this.gridOpacceso.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridOpacceso_RowHeaderMouseDoubleClick);
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
            // ultraPanel1
            // 
            this.ultraPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightGray;
            appearance1.BackColor2 = System.Drawing.Color.GhostWhite;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop37;
            this.ultraPanel1.Appearance = appearance1;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.gridOpacceso);
            this.ultraPanel1.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.ultraPanel1.Location = new System.Drawing.Point(0, 39);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(448, 219);
            this.ultraPanel1.TabIndex = 58;
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosPrincipales.Controls.Add(this.cmb_modulo);
            this.grpDatosPrincipales.Controls.Add(this.txt_codigomodulo);
            this.grpDatosPrincipales.Controls.Add(this.label1);
            this.grpDatosPrincipales.Controls.Add(this.txt_descripcion);
            this.grpDatosPrincipales.Controls.Add(this.label2);
            this.grpDatosPrincipales.Controls.Add(this.label3);
            this.grpDatosPrincipales.Location = new System.Drawing.Point(24, 13);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(404, 126);
            this.grpDatosPrincipales.TabIndex = 0;
            this.grpDatosPrincipales.Text = "Datos Generales";
            this.grpDatosPrincipales.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
            // 
            // frm_OpcionesAcceso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 259);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.ultraPanel1);
            this.Name = "frm_OpcionesAcceso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones de Accesos";
            this.Load += new System.EventHandler(this.frm_OpcionesAcceso_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOpacceso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_modulo;
        private System.Windows.Forms.TextBox txt_descripcion;
        private System.Windows.Forms.TextBox txt_codigomodulo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.DataGridView gridOpacceso;
        private System.Windows.Forms.ErrorProvider controlErrores;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem mnuBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
    }
}