namespace His.Honorarios
{
    partial class frm_EmisionRetenciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_EmisionRetenciones));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.txt_factura = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_ruc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_porcentaje = new System.Windows.Forms.TextBox();
            this.txt_valorretenido = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_base = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_ingreso = new System.Windows.Forms.MaskedTextBox();
            this.txt_sujetoderetencion = new System.Windows.Forms.TextBox();
            this.txt_codigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridnotacd = new System.Windows.Forms.DataGridView();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frm_EmisionRetenciones_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            this.menu.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridnotacd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            this.frm_EmisionRetenciones_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_EmisionRetenciones_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_factura
            // 
            this.txt_factura.Location = new System.Drawing.Point(183, 115);
            this.txt_factura.Mask = "000-000-0000000";
            this.txt_factura.Name = "txt_factura";
            this.txt_factura.Size = new System.Drawing.Size(110, 21);
            this.txt_factura.TabIndex = 73;
            this.txt_factura.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_factura_KeyDown);
            this.txt_factura.Leave += new System.EventHandler(this.txt_factura_Leave);
            this.txt_factura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_factura_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(10, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 16);
            this.label7.TabIndex = 72;
            this.label7.Text = "Porcentaje:";
            // 
            // txt_ruc
            // 
            this.txt_ruc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_ruc.Location = new System.Drawing.Point(183, 88);
            this.txt_ruc.MaxLength = 40;
            this.txt_ruc.Name = "txt_ruc";
            this.txt_ruc.ReadOnly = true;
            this.txt_ruc.Size = new System.Drawing.Size(110, 21);
            this.txt_ruc.TabIndex = 68;
            this.txt_ruc.Leave += new System.EventHandler(this.txt_ruc_Leave);
            this.txt_ruc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ruc_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(12, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 67;
            this.label6.Text = "RUC:";
            // 
            // txt_porcentaje
            // 
            this.txt_porcentaje.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_porcentaje.Location = new System.Drawing.Point(183, 170);
            this.txt_porcentaje.MaxLength = 40;
            this.txt_porcentaje.Name = "txt_porcentaje";
            this.txt_porcentaje.ReadOnly = true;
            this.txt_porcentaje.Size = new System.Drawing.Size(110, 21);
            this.txt_porcentaje.TabIndex = 66;
            // 
            // txt_valorretenido
            // 
            this.txt_valorretenido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_valorretenido.Location = new System.Drawing.Point(182, 200);
            this.txt_valorretenido.MaxLength = 40;
            this.txt_valorretenido.Name = "txt_valorretenido";
            this.txt_valorretenido.Size = new System.Drawing.Size(111, 21);
            this.txt_valorretenido.TabIndex = 65;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(12, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 16);
            this.label5.TabIndex = 64;
            this.label5.Text = "Valor Retenido:";
            // 
            // txt_base
            // 
            this.txt_base.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_base.Location = new System.Drawing.Point(182, 142);
            this.txt_base.MaxLength = 40;
            this.txt_base.Name = "txt_base";
            this.txt_base.ReadOnly = true;
            this.txt_base.Size = new System.Drawing.Size(111, 21);
            this.txt_base.TabIndex = 63;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 62;
            this.label4.Text = "Base:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(12, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 60;
            this.label3.Text = "Factura:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(334, 27);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(102, 17);
            this.label25.TabIndex = 59;
            this.label25.Text = "Fecha ingreso:";
            // 
            // txt_ingreso
            // 
            this.txt_ingreso.Location = new System.Drawing.Point(442, 27);
            this.txt_ingreso.Mask = "00/00/0000";
            this.txt_ingreso.Name = "txt_ingreso";
            this.txt_ingreso.ReadOnly = true;
            this.txt_ingreso.Size = new System.Drawing.Size(92, 21);
            this.txt_ingreso.TabIndex = 58;
            this.txt_ingreso.ValidatingType = typeof(System.DateTime);
            // 
            // txt_sujetoderetencion
            // 
            this.txt_sujetoderetencion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_sujetoderetencion.Location = new System.Drawing.Point(182, 59);
            this.txt_sujetoderetencion.MaxLength = 40;
            this.txt_sujetoderetencion.Name = "txt_sujetoderetencion";
            this.txt_sujetoderetencion.ReadOnly = true;
            this.txt_sujetoderetencion.Size = new System.Drawing.Size(228, 21);
            this.txt_sujetoderetencion.TabIndex = 14;
            this.txt_sujetoderetencion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_sujetoderetencion_KeyDown);
            this.txt_sujetoderetencion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_sujetoderetencion_KeyPress);
            // 
            // txt_codigo
            // 
            this.txt_codigo.Location = new System.Drawing.Point(182, 27);
            this.txt_codigo.Name = "txt_codigo";
            this.txt_codigo.ReadOnly = true;
            this.txt_codigo.Size = new System.Drawing.Size(111, 21);
            this.txt_codigo.TabIndex = 13;
            this.txt_codigo.Leave += new System.EventHandler(this.txt_codigo_Leave);
            this.txt_codigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codigo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Sujeto de Retencion:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Retención:";
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnActualizar,
            this.btnGuardar,
            this.btnEliminar,
            this.btnImprimir,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(613, 36);
            this.menu.TabIndex = 71;
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
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(66, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(58, 33);
            this.btnEliminar.Text = "Anular";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(65, 33);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
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
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.gridnotacd);
            this.panel2.Location = new System.Drawing.Point(27, 281);
            this.panel2.MaximumSize = new System.Drawing.Size(800, 350);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(559, 176);
            this.panel2.TabIndex = 72;
            // 
            // gridnotacd
            // 
            this.gridnotacd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridnotacd.BackgroundColor = System.Drawing.Color.LightGray;
            this.gridnotacd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridnotacd.Location = new System.Drawing.Point(3, 3);
            this.gridnotacd.Name = "gridnotacd";
            this.gridnotacd.ReadOnly = true;
            this.gridnotacd.Size = new System.Drawing.Size(553, 170);
            this.gridnotacd.TabIndex = 0;
            this.gridnotacd.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridnotacd_ColumnHeaderMouseClick);
            this.gridnotacd.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridnotacd_RowHeaderMouseDoubleClick);
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
            // frm_EmisionRetenciones_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_EmisionRetenciones_Fill_Panel.Appearance = appearance1;
            // 
            // frm_EmisionRetenciones_Fill_Panel.ClientArea
            // 
            this.frm_EmisionRetenciones_Fill_Panel.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.frm_EmisionRetenciones_Fill_Panel.ClientArea.Controls.Add(this.panel2);
            this.frm_EmisionRetenciones_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_EmisionRetenciones_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_EmisionRetenciones_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frm_EmisionRetenciones_Fill_Panel.Name = "frm_EmisionRetenciones_Fill_Panel";
            this.frm_EmisionRetenciones_Fill_Panel.Size = new System.Drawing.Size(613, 476);
            this.frm_EmisionRetenciones_Fill_Panel.TabIndex = 72;
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.BackColorInternal = System.Drawing.Color.WhiteSmoke;
            this.grpDatosPrincipales.Controls.Add(this.txt_codigo);
            this.grpDatosPrincipales.Controls.Add(this.label1);
            this.grpDatosPrincipales.Controls.Add(this.txt_factura);
            this.grpDatosPrincipales.Controls.Add(this.label2);
            this.grpDatosPrincipales.Controls.Add(this.label7);
            this.grpDatosPrincipales.Controls.Add(this.txt_sujetoderetencion);
            this.grpDatosPrincipales.Controls.Add(this.txt_ruc);
            this.grpDatosPrincipales.Controls.Add(this.txt_ingreso);
            this.grpDatosPrincipales.Controls.Add(this.label6);
            this.grpDatosPrincipales.Controls.Add(this.label25);
            this.grpDatosPrincipales.Controls.Add(this.txt_porcentaje);
            this.grpDatosPrincipales.Controls.Add(this.label3);
            this.grpDatosPrincipales.Controls.Add(this.txt_valorretenido);
            this.grpDatosPrincipales.Controls.Add(this.label4);
            this.grpDatosPrincipales.Controls.Add(this.label5);
            this.grpDatosPrincipales.Controls.Add(this.txt_base);
            this.grpDatosPrincipales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosPrincipales.Location = new System.Drawing.Point(30, 18);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(551, 248);
            this.grpDatosPrincipales.TabIndex = 74;
            this.grpDatosPrincipales.Text = "Datos de la Retención";
            this.grpDatosPrincipales.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // frm_EmisionRetenciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(613, 512);
            this.Controls.Add(this.frm_EmisionRetenciones_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "frm_EmisionRetenciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retenciones";
            this.Load += new System.EventHandler(this.frm_EmisionRetenciones_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridnotacd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            this.frm_EmisionRetenciones_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_EmisionRetenciones_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ruc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_porcentaje;
        private System.Windows.Forms.TextBox txt_valorretenido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_base;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.MaskedTextBox txt_ingreso;
        private System.Windows.Forms.TextBox txt_sujetoderetencion;
        private System.Windows.Forms.TextBox txt_codigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnImprimir;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView gridnotacd;
        private System.Windows.Forms.ErrorProvider controlErrores;
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem mnuBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.MaskedTextBox txt_factura;
        private Infragistics.Win.Misc.UltraPanel frm_EmisionRetenciones_Fill_Panel;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
    }
}