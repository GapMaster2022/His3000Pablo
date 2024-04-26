namespace His.Admision
{
    partial class frmExploradorControlAyuda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExploradorControlAyuda));
            this.TablaControl = new System.Windows.Forms.DataGridView();
            this.cod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.des = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.est = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblpaciente = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblatencion = new System.Windows.Forms.Label();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btneditar = new System.Windows.Forms.ToolStripButton();
            this.btnAbrir = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TablaControl)).BeginInit();
            this.tools.SuspendLayout();
            this.SuspendLayout();
            // 
            // TablaControl
            // 
            this.TablaControl.AllowUserToAddRows = false;
            this.TablaControl.AllowUserToDeleteRows = false;
            this.TablaControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablaControl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaControl.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablaControl.BackgroundColor = System.Drawing.Color.White;
            this.TablaControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaControl.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TablaControl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.TablaControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaControl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cod,
            this.des,
            this.est,
            this.fecha,
            this.user});
            this.TablaControl.Location = new System.Drawing.Point(1, 142);
            this.TablaControl.Name = "TablaControl";
            this.TablaControl.Size = new System.Drawing.Size(915, 345);
            this.TablaControl.TabIndex = 0;
            // 
            // cod
            // 
            this.cod.HeaderText = "Codigo";
            this.cod.Name = "cod";
            this.cod.ReadOnly = true;
            this.cod.Width = 71;
            // 
            // des
            // 
            this.des.HeaderText = "Descripción";
            this.des.Name = "des";
            this.des.ReadOnly = true;
            this.des.Width = 99;
            // 
            // est
            // 
            this.est.HeaderText = "Estado";
            this.est.Name = "est";
            this.est.Width = 52;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fecha.Width = 48;
            // 
            // user
            // 
            this.user.HeaderText = "Usuario";
            this.user.Name = "user";
            this.user.ReadOnly = true;
            this.user.Width = 75;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Paciente :";
            // 
            // lblpaciente
            // 
            this.lblpaciente.AutoSize = true;
            this.lblpaciente.Location = new System.Drawing.Point(95, 70);
            this.lblpaciente.Name = "lblpaciente";
            this.lblpaciente.Size = new System.Drawing.Size(49, 13);
            this.lblpaciente.TabIndex = 2;
            this.lblpaciente.Text = "Paciente";
            this.lblpaciente.Click += new System.EventHandler(this.lblpaciente_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Atención :";
            // 
            // lblatencion
            // 
            this.lblatencion.AutoSize = true;
            this.lblatencion.Location = new System.Drawing.Point(94, 105);
            this.lblatencion.Name = "lblatencion";
            this.lblatencion.Size = new System.Drawing.Size(48, 13);
            this.lblatencion.TabIndex = 4;
            this.lblatencion.Text = "atencion";
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonActualizar,
            this.btnGuardar,
            this.btneditar,
            this.btnAbrir,
            this.btnCerrar,
            this.btnCancelar,
            this.btnExcel,
            this.toolStripSeparator1,
            this.toolStripButtonSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(918, 45);
            this.tools.TabIndex = 8;
            this.tools.Text = "toolStrip1";
            // 
            // toolStripButtonActualizar
            // 
            this.toolStripButtonActualizar.AutoSize = false;
            this.toolStripButtonActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonActualizar.Image = global::His.Admision.Properties.Resources.HIS_REFRESH;
            this.toolStripButtonActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonActualizar.Name = "toolStripButtonActualizar";
            this.toolStripButtonActualizar.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonActualizar.Text = "toolStripButton1";
            this.toolStripButtonActualizar.ToolTipText = "Actualizar";
            this.toolStripButtonActualizar.Click += new System.EventHandler(this.toolStripButtonActualizar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.AutoSize = false;
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = global::His.Admision.Properties.Resources.HIS_GUARDAR;
            this.btnGuardar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(42, 42);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.ToolTipText = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btneditar
            // 
            this.btneditar.AutoSize = false;
            this.btneditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btneditar.Image = global::His.Admision.Properties.Resources.HIS_EDITAR;
            this.btneditar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btneditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btneditar.Name = "btneditar";
            this.btneditar.Size = new System.Drawing.Size(42, 42);
            this.btneditar.Text = "Editar";
            this.btneditar.ToolTipText = "Editar";
            this.btneditar.Click += new System.EventHandler(this.btneditar_Click);
            // 
            // btnAbrir
            // 
            this.btnAbrir.AutoSize = false;
            this.btnAbrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbrir.Image = ((System.Drawing.Image)(resources.GetObject("btnAbrir.Image")));
            this.btnAbrir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAbrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(42, 42);
            this.btnAbrir.Text = "Abrir Documentos";
            this.btnAbrir.ToolTipText = "Abrir Documentos";
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.AutoSize = false;
            this.btnCerrar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(42, 42);
            this.btnCerrar.Text = "Cerrar Documentos";
            this.btnCerrar.ToolTipText = "Cerrar Documentos";
            this.btnCerrar.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = false;
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = global::His.Admision.Properties.Resources.HIS_CANCELAR;
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(42, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.AutoSize = false;
            this.btnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExcel.Image = global::His.Admision.Properties.Resources.HIS_EXPORT_TO_EXCEL;
            this.btnExcel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(42, 42);
            this.btnExcel.Text = "Exportar";
            this.btnExcel.ToolTipText = "Exportar a Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripButtonSalir
            // 
            this.toolStripButtonSalir.AutoSize = false;
            this.toolStripButtonSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSalir.Image = global::His.Admision.Properties.Resources.HIS_SALIR;
            this.toolStripButtonSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSalir.Name = "toolStripButtonSalir";
            this.toolStripButtonSalir.Size = new System.Drawing.Size(42, 42);
            this.toolStripButtonSalir.Text = "toolStripButton1";
            this.toolStripButtonSalir.ToolTipText = "Salir";
            this.toolStripButtonSalir.Click += new System.EventHandler(this.toolStripButtonSalir_Click);
            // 
            // frmExploradorControlAyuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 490);
            this.Controls.Add(this.tools);
            this.Controls.Add(this.lblatencion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblpaciente);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TablaControl);
            this.Name = "frmExploradorControlAyuda";
            this.Text = "Documentos CheckList";
            this.Load += new System.EventHandler(this.frmExploradorControlAyuda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TablaControl)).EndInit();
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView TablaControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblpaciente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblatencion;
        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualizar;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSalir;
        private System.Windows.Forms.ToolStripButton btnExcel;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.ToolStripButton btneditar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn cod;
        private System.Windows.Forms.DataGridViewTextBoxColumn des;
        private System.Windows.Forms.DataGridViewCheckBoxColumn est;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn user;
        private System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.ToolStripButton btnAbrir;
    }
}