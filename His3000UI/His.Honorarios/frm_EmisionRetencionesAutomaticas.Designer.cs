namespace His.Honorarios
{
    partial class frm_EmisionRetencionesAutomaticas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_EmisionRetencionesAutomaticas));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_Maratodas = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_ingreso = new System.Windows.Forms.MaskedTextBox();
            this.txt_codigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridnotacd = new System.Windows.Forms.DataGridView();
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridnotacd)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(29, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "No Ingreso:";
            // 
            // chk_Maratodas
            // 
            this.chk_Maratodas.AutoSize = true;
            this.chk_Maratodas.BackColor = System.Drawing.Color.Transparent;
            this.chk_Maratodas.Location = new System.Drawing.Point(718, 24);
            this.chk_Maratodas.Name = "chk_Maratodas";
            this.chk_Maratodas.Size = new System.Drawing.Size(88, 17);
            this.chk_Maratodas.TabIndex = 1;
            this.chk_Maratodas.Text = "Marcar todas";
            this.chk_Maratodas.UseVisualStyleBackColor = false;
            this.chk_Maratodas.CheckedChanged += new System.EventHandler(this.chk_Maratodas_CheckedChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(464, 24);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(102, 17);
            this.label25.TabIndex = 59;
            this.label25.Text = "Fecha ingreso:";
            // 
            // txt_ingreso
            // 
            this.txt_ingreso.Location = new System.Drawing.Point(572, 23);
            this.txt_ingreso.Mask = "00/00/0000";
            this.txt_ingreso.Name = "txt_ingreso";
            this.txt_ingreso.ReadOnly = true;
            this.txt_ingreso.Size = new System.Drawing.Size(92, 20);
            this.txt_ingreso.TabIndex = 58;
            this.txt_ingreso.ValidatingType = typeof(System.DateTime);
            // 
            // txt_codigo
            // 
            this.txt_codigo.Location = new System.Drawing.Point(301, 23);
            this.txt_codigo.MaxLength = 7;
            this.txt_codigo.Name = "txt_codigo";
            this.txt_codigo.ReadOnly = true;
            this.txt_codigo.Size = new System.Drawing.Size(124, 20);
            this.txt_codigo.TabIndex = 0;
            this.txt_codigo.Leave += new System.EventHandler(this.txt_codigo_Leave);
            this.txt_codigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_codigo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(227, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
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
            this.btnImprimir,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(894, 36);
            this.menu.TabIndex = 74;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(58, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Visible = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(70, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
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
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(47, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.gridnotacd);
            this.panel2.Location = new System.Drawing.Point(22, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 375);
            this.panel2.TabIndex = 75;
            // 
            // gridnotacd
            // 
            this.gridnotacd.BackgroundColor = System.Drawing.Color.LightGray;
            this.gridnotacd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridnotacd.Location = new System.Drawing.Point(3, 3);
            this.gridnotacd.Name = "gridnotacd";
            this.gridnotacd.Size = new System.Drawing.Size(844, 369);
            this.gridnotacd.TabIndex = 2;
            this.gridnotacd.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridnotacd_ColumnHeaderMouseClick);
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
            // controlErrores
            // 
            this.controlErrores.ContainerControl = this;
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.Controls.Add(this.label2);
            this.grpDatosPrincipales.Controls.Add(this.label3);
            this.grpDatosPrincipales.Controls.Add(this.label25);
            this.grpDatosPrincipales.Controls.Add(this.txt_codigo);
            this.grpDatosPrincipales.Controls.Add(this.label1);
            this.grpDatosPrincipales.Controls.Add(this.chk_Maratodas);
            this.grpDatosPrincipales.Controls.Add(this.txt_ingreso);
            this.grpDatosPrincipales.Location = new System.Drawing.Point(22, 14);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(850, 63);
            this.grpDatosPrincipales.TabIndex = 77;
            this.grpDatosPrincipales.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // frm_EmisionRetencionesAutomaticas_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.Appearance = appearance1;
            // 
            // frm_EmisionRetencionesAutomaticas_Fill_Panel.ClientArea
            // 
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.ClientArea.Controls.Add(this.panel2);
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.Name = "frm_EmisionRetencionesAutomaticas_Fill_Panel";
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.Size = new System.Drawing.Size(894, 497);
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.TabIndex = 75;
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.PaintClient += new System.Windows.Forms.PaintEventHandler(this.frm_EmisionRetencionesAutomaticas_Fill_Panel_PaintClient);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(114, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 65;
            this.label2.Text = ".";
            // 
            // frm_EmisionRetencionesAutomaticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(894, 533);
            this.Controls.Add(this.frm_EmisionRetencionesAutomaticas_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "frm_EmisionRetencionesAutomaticas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emision de Retenciones Automaticas";
            this.Load += new System.EventHandler(this.frm_EmisionRetencionesAutomaticas_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridnotacd)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_EmisionRetencionesAutomaticas_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chk_Maratodas;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.MaskedTextBox txt_ingreso;
        private System.Windows.Forms.TextBox txt_codigo;
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
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem mnuBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider controlErrores;
        private System.Windows.Forms.Label label3;
        private Infragistics.Win.Misc.UltraPanel frm_EmisionRetencionesAutomaticas_Fill_Panel;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
        private System.Windows.Forms.Label label2;
    }
}