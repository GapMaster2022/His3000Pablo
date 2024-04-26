namespace TarifariosUI
{
    partial class FrmCategoriasConvenios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridConvenios = new System.Windows.Forms.DataGridView();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MmanuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtp_Ffin = new System.Windows.Forms.DateTimePicker();
            this.txt_codigo = new System.Windows.Forms.TextBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_descripcion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CmbAseguradoras = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtp_Finicio = new System.Windows.Forms.DateTimePicker();
            this.grpDatosPrincipales = new Infragistics.Win.Misc.UltraGroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nTxtPorcentaje = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.cboTipoPrecio = new System.Windows.Forms.ComboBox();
            this.frm_CategoriasConvenios_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConvenios)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).BeginInit();
            this.grpDatosPrincipales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nTxtPorcentaje)).BeginInit();
            this.frm_CategoriasConvenios_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_CategoriasConvenios_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.gridConvenios);
            this.panel1.Location = new System.Drawing.Point(12, 251);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 141);
            this.panel1.TabIndex = 1;
            // 
            // gridConvenios
            // 
            this.gridConvenios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridConvenios.BackgroundColor = System.Drawing.Color.GhostWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridConvenios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridConvenios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridConvenios.Location = new System.Drawing.Point(3, 3);
            this.gridConvenios.Name = "gridConvenios";
            this.gridConvenios.RowHeadersWidth = 20;
            this.gridConvenios.Size = new System.Drawing.Size(640, 135);
            this.gridConvenios.TabIndex = 0;
            this.gridConvenios.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridConvenios_ColumnHeaderMouseClick);
            this.gridConvenios.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridConvenios_RowHeaderMouseDoubleClick);
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
            this.menu.Size = new System.Drawing.Size(673, 36);
            this.menu.TabIndex = 40;
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
            this.btnActualizar.Visible = false;
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
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(31, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // controlErrores
            // 
            this.controlErrores.ContainerControl = this;
            // 
            // mnuContexsecundario
            // 
            this.mnuContexsecundario.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MmanuBuscar,
            this.cancelarToolStripMenuItem});
            this.mnuContexsecundario.Name = "contextMenuStrip1";
            this.mnuContexsecundario.Size = new System.Drawing.Size(128, 48);
            // 
            // MmanuBuscar
            // 
            this.MmanuBuscar.Name = "MmanuBuscar";
            this.MmanuBuscar.Size = new System.Drawing.Size(127, 22);
            this.MmanuBuscar.Text = "Buscar";
            this.MmanuBuscar.Click += new System.EventHandler(this.MmanuBuscar_Click);
            // 
            // cancelarToolStripMenuItem
            // 
            this.cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            this.cancelarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.cancelarToolStripMenuItem.Text = "Cancelar";
            // 
            // dtp_Ffin
            // 
            this.dtp_Ffin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Ffin.Location = new System.Drawing.Point(529, 92);
            this.dtp_Ffin.Name = "dtp_Ffin";
            this.dtp_Ffin.Size = new System.Drawing.Size(91, 20);
            this.dtp_Ffin.TabIndex = 3;
            // 
            // txt_codigo
            // 
            this.txt_codigo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_codigo.Location = new System.Drawing.Point(146, 13);
            this.txt_codigo.Name = "txt_codigo";
            this.txt_codigo.ReadOnly = true;
            this.txt_codigo.Size = new System.Drawing.Size(91, 20);
            this.txt_codigo.TabIndex = 0;
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(146, 66);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(474, 20);
            this.txt_nombre.TabIndex = 1;
            this.txt_nombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_nombre_KeyPress);
            // 
            // txt_descripcion
            // 
            this.txt_descripcion.Location = new System.Drawing.Point(146, 145);
            this.txt_descripcion.Multiline = true;
            this.txt_descripcion.Name = "txt_descripcion";
            this.txt_descripcion.Size = new System.Drawing.Size(474, 52);
            this.txt_descripcion.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(96, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Codigo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(44, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nombre Convenio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(73, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Descripcion";
            // 
            // CmbAseguradoras
            // 
            this.CmbAseguradoras.FormattingEnabled = true;
            this.CmbAseguradoras.Location = new System.Drawing.Point(146, 39);
            this.CmbAseguradoras.Name = "CmbAseguradoras";
            this.CmbAseguradoras.Size = new System.Drawing.Size(197, 21);
            this.CmbAseguradoras.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(15, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Aseguradoras Empresas";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(71, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Fecha Inicio";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(469, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Fecha Fin";
            // 
            // dtp_Finicio
            // 
            this.dtp_Finicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_Finicio.Location = new System.Drawing.Point(146, 92);
            this.dtp_Finicio.Name = "dtp_Finicio";
            this.dtp_Finicio.Size = new System.Drawing.Size(91, 20);
            this.dtp_Finicio.TabIndex = 2;
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.Controls.Add(this.label8);
            this.grpDatosPrincipales.Controls.Add(this.label5);
            this.grpDatosPrincipales.Controls.Add(this.nTxtPorcentaje);
            this.grpDatosPrincipales.Controls.Add(this.cboTipoPrecio);
            this.grpDatosPrincipales.Controls.Add(this.dtp_Finicio);
            this.grpDatosPrincipales.Controls.Add(this.txt_descripcion);
            this.grpDatosPrincipales.Controls.Add(this.label7);
            this.grpDatosPrincipales.Controls.Add(this.dtp_Ffin);
            this.grpDatosPrincipales.Controls.Add(this.label6);
            this.grpDatosPrincipales.Controls.Add(this.txt_codigo);
            this.grpDatosPrincipales.Controls.Add(this.txt_nombre);
            this.grpDatosPrincipales.Controls.Add(this.label4);
            this.grpDatosPrincipales.Controls.Add(this.label1);
            this.grpDatosPrincipales.Controls.Add(this.CmbAseguradoras);
            this.grpDatosPrincipales.Controls.Add(this.label2);
            this.grpDatosPrincipales.Controls.Add(this.label3);
            this.grpDatosPrincipales.Location = new System.Drawing.Point(12, 14);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(643, 219);
            this.grpDatosPrincipales.TabIndex = 41;
            this.grpDatosPrincipales.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(56, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Precio a aplicar";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(397, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Porcentaje de descuento";
            // 
            // nTxtPorcentaje
            // 
            this.nTxtPorcentaje.Location = new System.Drawing.Point(529, 117);
            this.nTxtPorcentaje.MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.Raw;
            this.nTxtPorcentaje.MaskInput = "-nnn";
            this.nTxtPorcentaje.MaxValue = 100;
            this.nTxtPorcentaje.MinValue = -100;
            this.nTxtPorcentaje.Name = "nTxtPorcentaje";
            this.nTxtPorcentaje.Size = new System.Drawing.Size(91, 21);
            this.nTxtPorcentaje.TabIndex = 5;
            // 
            // cboTipoPrecio
            // 
            this.cboTipoPrecio.FormattingEnabled = true;
            this.cboTipoPrecio.Location = new System.Drawing.Point(146, 118);
            this.cboTipoPrecio.Name = "cboTipoPrecio";
            this.cboTipoPrecio.Size = new System.Drawing.Size(89, 21);
            this.cboTipoPrecio.TabIndex = 4;
            // 
            // frm_CategoriasConvenios_Fill_Panel
            // 
            this.frm_CategoriasConvenios_Fill_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightGray;
            appearance1.BackColor2 = System.Drawing.Color.GhostWhite;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop37;
            this.frm_CategoriasConvenios_Fill_Panel.Appearance = appearance1;
            // 
            // frm_CategoriasConvenios_Fill_Panel.ClientArea
            // 
            this.frm_CategoriasConvenios_Fill_Panel.ClientArea.Controls.Add(this.grpDatosPrincipales);
            this.frm_CategoriasConvenios_Fill_Panel.ClientArea.Controls.Add(this.panel1);
            this.frm_CategoriasConvenios_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_CategoriasConvenios_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frm_CategoriasConvenios_Fill_Panel.Name = "frm_CategoriasConvenios_Fill_Panel";
            this.frm_CategoriasConvenios_Fill_Panel.Size = new System.Drawing.Size(673, 439);
            this.frm_CategoriasConvenios_Fill_Panel.TabIndex = 41;
            // 
            // FrmCategoriasConvenios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 440);
            this.Controls.Add(this.frm_CategoriasConvenios_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "FrmCategoriasConvenios";
            this.Text = "Convenios y Categorias";
            this.Load += new System.EventHandler(this.FrmCategoriasConveniosLoad);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridConvenios)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatosPrincipales)).EndInit();
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nTxtPorcentaje)).EndInit();
            this.frm_CategoriasConvenios_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_CategoriasConvenios_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridConvenios;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.ErrorProvider controlErrores;
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem MmanuBuscar;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtp_Finicio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CmbAseguradoras;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_descripcion;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_codigo;
        private System.Windows.Forms.DateTimePicker dtp_Ffin;
        private Infragistics.Win.Misc.UltraGroupBox grpDatosPrincipales;
        private Infragistics.Win.Misc.UltraPanel frm_CategoriasConvenios_Fill_Panel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor nTxtPorcentaje;
        private System.Windows.Forms.ComboBox cboTipoPrecio;
    }
}