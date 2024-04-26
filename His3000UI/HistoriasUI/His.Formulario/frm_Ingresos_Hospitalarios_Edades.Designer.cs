namespace His.Formulario
{
    partial class frm_Ingresos_Hospitalarios_Edades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Ingresos_Hospitalarios_Edades));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ultraGroupBoxPaciente = new Infragistics.Win.Misc.UltraGroupBox();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTipoAtencion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnbuscar = new System.Windows.Forms.ToolStripButton();
            this.btnexportar = new System.Windows.Forms.ToolStripButton();
            this.btnimprimir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnsalir = new System.Windows.Forms.ToolStripButton();
            this.TablaRango = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.redad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.muj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPaciente)).BeginInit();
            this.ultraGroupBoxPaciente.SuspendLayout();
            this.tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaRango)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBoxPaciente
            // 
            this.ultraGroupBoxPaciente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBoxPaciente.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Near;
            this.ultraGroupBoxPaciente.Controls.Add(this.button1);
            this.ultraGroupBoxPaciente.Controls.Add(this.dtpHasta);
            this.ultraGroupBoxPaciente.Controls.Add(this.dtpDesde);
            this.ultraGroupBoxPaciente.Controls.Add(this.label6);
            this.ultraGroupBoxPaciente.Controls.Add(this.label5);
            this.ultraGroupBoxPaciente.Controls.Add(this.cbTipoAtencion);
            this.ultraGroupBoxPaciente.Controls.Add(this.label4);
            this.ultraGroupBoxPaciente.ForeColor = System.Drawing.Color.DimGray;
            this.ultraGroupBoxPaciente.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBoxPaciente.Location = new System.Drawing.Point(0, 48);
            this.ultraGroupBoxPaciente.Name = "ultraGroupBoxPaciente";
            this.ultraGroupBoxPaciente.Size = new System.Drawing.Size(929, 47);
            this.ultraGroupBoxPaciente.TabIndex = 41;
            this.ultraGroupBoxPaciente.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(706, 7);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(98, 20);
            this.dtpHasta.TabIndex = 274;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(474, 7);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(98, 20);
            this.dtpDesde.TabIndex = 273;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(619, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 16);
            this.label6.TabIndex = 272;
            this.label6.Text = "Fecha Final:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(382, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 16);
            this.label5.TabIndex = 271;
            this.label5.Text = "Fecha Inicial:";
            // 
            // cbTipoAtencion
            // 
            this.cbTipoAtencion.FormattingEnabled = true;
            this.cbTipoAtencion.Location = new System.Drawing.Point(131, 10);
            this.cbTipoAtencion.Name = "cbTipoAtencion";
            this.cbTipoAtencion.Size = new System.Drawing.Size(213, 21);
            this.cbTipoAtencion.TabIndex = 270;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 16);
            this.label4.TabIndex = 269;
            this.label4.Text = "Tipo de Atención:";
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnbuscar,
            this.btnexportar,
            this.btnimprimir,
            this.toolStripSeparator3,
            this.btnsalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Size = new System.Drawing.Size(929, 45);
            this.tools.TabIndex = 42;
            this.tools.Text = "toolStrip1";
            // 
            // btnbuscar
            // 
            this.btnbuscar.AutoSize = false;
            this.btnbuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnbuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnbuscar.Image")));
            this.btnbuscar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnbuscar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(42, 42);
            this.btnbuscar.Text = "buscar";
            this.btnbuscar.ToolTipText = "Actualizar";
            this.btnbuscar.Visible = false;
            // 
            // btnexportar
            // 
            this.btnexportar.AutoSize = false;
            this.btnexportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnexportar.Image = ((System.Drawing.Image)(resources.GetObject("btnexportar.Image")));
            this.btnexportar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnexportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnexportar.Name = "btnexportar";
            this.btnexportar.Size = new System.Drawing.Size(42, 42);
            this.btnexportar.Text = "Exportar";
            this.btnexportar.ToolTipText = "Exportar a Excel";
            this.btnexportar.Visible = false;
            // 
            // btnimprimir
            // 
            this.btnimprimir.AutoSize = false;
            this.btnimprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnimprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnimprimir.Image")));
            this.btnimprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnimprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnimprimir.Name = "btnimprimir";
            this.btnimprimir.Size = new System.Drawing.Size(42, 42);
            this.btnimprimir.Text = "Imprimir";
            this.btnimprimir.ToolTipText = "Imprimir Reporte";
            this.btnimprimir.Click += new System.EventHandler(this.btnimprimir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 45);
            // 
            // btnsalir
            // 
            this.btnsalir.AutoSize = false;
            this.btnsalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnsalir.Image = ((System.Drawing.Image)(resources.GetObject("btnsalir.Image")));
            this.btnsalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnsalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(42, 42);
            this.btnsalir.Text = "Salir";
            this.btnsalir.ToolTipText = "Salir";
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // TablaRango
            // 
            this.TablaRango.AllowUserToAddRows = false;
            this.TablaRango.AllowUserToDeleteRows = false;
            this.TablaRango.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaRango.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablaRango.BackgroundColor = System.Drawing.Color.White;
            this.TablaRango.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaRango.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TablaRango.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.TablaRango.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaRango.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.num,
            this.redad,
            this.hom,
            this.muj,
            this.tot});
            this.TablaRango.EnableHeadersVisualStyles = false;
            this.TablaRango.Location = new System.Drawing.Point(0, 101);
            this.TablaRango.Name = "TablaRango";
            this.TablaRango.ReadOnly = true;
            this.TablaRango.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TablaRango.RowHeadersVisible = false;
            this.TablaRango.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaRango.Size = new System.Drawing.Size(487, 241);
            this.TablaRango.TabIndex = 43;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(833, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 44;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // num
            // 
            this.num.HeaderText = "NRO";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            this.num.Width = 58;
            // 
            // redad
            // 
            this.redad.HeaderText = "RANGO EDADES";
            this.redad.Name = "redad";
            this.redad.ReadOnly = true;
            this.redad.Width = 118;
            // 
            // hom
            // 
            this.hom.HeaderText = "HOMBRES";
            this.hom.Name = "hom";
            this.hom.ReadOnly = true;
            this.hom.Width = 92;
            // 
            // muj
            // 
            this.muj.HeaderText = "MUJERES";
            this.muj.Name = "muj";
            this.muj.ReadOnly = true;
            this.muj.Width = 89;
            // 
            // tot
            // 
            this.tot.HeaderText = "TOTAL";
            this.tot.Name = "tot";
            this.tot.ReadOnly = true;
            this.tot.Width = 71;
            // 
            // frm_Ingresos_Hospitalarios_Edades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 467);
            this.Controls.Add(this.TablaRango);
            this.Controls.Add(this.tools);
            this.Controls.Add(this.ultraGroupBoxPaciente);
            this.Name = "frm_Ingresos_Hospitalarios_Edades";
            this.Text = "Ingreso Hospitalario por Edades";
            this.Load += new System.EventHandler(this.frm_Ingresos_Hospitalarios_Edades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBoxPaciente)).EndInit();
            this.ultraGroupBoxPaciente.ResumeLayout(false);
            this.ultraGroupBoxPaciente.PerformLayout();
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaRango)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBoxPaciente;
        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton btnbuscar;
        private System.Windows.Forms.ToolStripButton btnexportar;
        private System.Windows.Forms.ToolStripButton btnimprimir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnsalir;
        private System.Windows.Forms.ComboBox cbTipoAtencion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView TablaRango;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn redad;
        private System.Windows.Forms.DataGridViewTextBoxColumn hom;
        private System.Windows.Forms.DataGridViewTextBoxColumn muj;
        private System.Windows.Forms.DataGridViewTextBoxColumn tot;
    }
}