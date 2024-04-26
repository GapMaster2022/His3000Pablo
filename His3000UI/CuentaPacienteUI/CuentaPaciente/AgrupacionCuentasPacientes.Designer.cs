namespace CuentaPaciente
{
    partial class AgrupacionCuentasPacientes
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            this.gridAgrupa = new System.Windows.Forms.DataGridView();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnBuscar = new Infragistics.Win.Misc.UltraButton();
            this.txtIdentificacion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtNombre = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtHistoria = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTerminar = new Infragistics.Win.Misc.UltraButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gridAñadidos = new System.Windows.Forms.DataGridView();
            this.ate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_ingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancelar = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridAgrupa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdentificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHistoria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAñadidos)).BeginInit();
            this.SuspendLayout();
            // 
            // gridAgrupa
            // 
            this.gridAgrupa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAgrupa.Location = new System.Drawing.Point(12, 100);
            this.gridAgrupa.Name = "gridAgrupa";
            this.gridAgrupa.Size = new System.Drawing.Size(706, 263);
            this.gridAgrupa.TabIndex = 0;
            this.gridAgrupa.DoubleClick += new System.EventHandler(this.gridAgrupa_DoubleClick);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.btnBuscar);
            this.ultraGroupBox1.Controls.Add(this.txtIdentificacion);
            this.ultraGroupBox1.Controls.Add(this.txtNombre);
            this.ultraGroupBox1.Controls.Add(this.txtHistoria);
            this.ultraGroupBox1.Controls.Add(this.label4);
            this.ultraGroupBox1.Controls.Add(this.label3);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Location = new System.Drawing.Point(12, 4);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(706, 90);
            this.ultraGroupBox1.TabIndex = 2;
            this.ultraGroupBox1.Text = "Filtros";
            // 
            // btnBuscar
            // 
            appearance1.BackColor = System.Drawing.Color.LightGray;
            this.btnBuscar.Appearance = appearance1;
            this.btnBuscar.Location = new System.Drawing.Point(557, 43);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(90, 33);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.Location = new System.Drawing.Point(445, 49);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.Size = new System.Drawing.Size(100, 21);
            this.txtIdentificacion.TabIndex = 9;
            this.txtIdentificacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdentificacion_KeyPress);
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Location = new System.Drawing.Point(186, 49);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(256, 21);
            this.txtNombre.TabIndex = 8;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            // 
            // txtHistoria
            // 
            this.txtHistoria.Location = new System.Drawing.Point(83, 49);
            this.txtHistoria.Name = "txtHistoria";
            this.txtHistoria.Size = new System.Drawing.Size(100, 21);
            this.txtHistoria.TabIndex = 7;
            this.txtHistoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHistoria_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(442, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nro. Identificación :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombres :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Historia Clínica :";
            // 
            // btnTerminar
            // 
            appearance2.BackColor = System.Drawing.Color.LightGray;
            this.btnTerminar.Appearance = appearance2;
            this.btnTerminar.Location = new System.Drawing.Point(554, 545);
            this.btnTerminar.Name = "btnTerminar";
            this.btnTerminar.Size = new System.Drawing.Size(164, 36);
            this.btnTerminar.TabIndex = 14;
            this.btnTerminar.Text = "TERMINAR AGRUPACIÓN";
            this.btnTerminar.Click += new System.EventHandler(this.btnTerminar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "PACIENTES AÑADIDOS";
            // 
            // gridAñadidos
            // 
            this.gridAñadidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAñadidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ate,
            this.hc,
            this.nombre,
            this.identificacion,
            this.fecha_ingreso});
            this.gridAñadidos.Location = new System.Drawing.Point(12, 390);
            this.gridAñadidos.Name = "gridAñadidos";
            this.gridAñadidos.Size = new System.Drawing.Size(706, 150);
            this.gridAñadidos.TabIndex = 5;
            // 
            // ate
            // 
            this.ate.HeaderText = "ATE";
            this.ate.Name = "ate";
            this.ate.Width = 50;
            // 
            // hc
            // 
            this.hc.HeaderText = "H CLINICA";
            this.hc.Name = "hc";
            this.hc.Width = 65;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "NOMBRE";
            this.nombre.Name = "nombre";
            this.nombre.Width = 300;
            // 
            // identificacion
            // 
            this.identificacion.HeaderText = "IDENTIFICACION";
            this.identificacion.Name = "identificacion";
            // 
            // fecha_ingreso
            // 
            this.fecha_ingreso.HeaderText = "FECHA INGRESO";
            this.fecha_ingreso.Name = "fecha_ingreso";
            this.fecha_ingreso.Width = 120;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ATE";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "H CLINICA";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 65;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "NOMBRE";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "IDENTIFICACION";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "FECHA INGRESO";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // btnCancelar
            // 
            appearance15.BackColor = System.Drawing.Color.LightGray;
            this.btnCancelar.Appearance = appearance15;
            this.btnCancelar.Location = new System.Drawing.Point(12, 545);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(164, 36);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.Text = "CANCELAR AGRUPACIÓN";
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // AgrupacionCuentasPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 593);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnTerminar);
            this.Controls.Add(this.gridAñadidos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ultraGroupBox1);
            this.Controls.Add(this.gridAgrupa);
            this.Name = "AgrupacionCuentasPacientes";
            this.Text = "AGRUPANCIÓN DE CUENTAS";
            ((System.ComponentModel.ISupportInitialize)(this.gridAgrupa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdentificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHistoria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAñadidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridAgrupa;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.Misc.UltraButton btnTerminar;
        private Infragistics.Win.Misc.UltraButton btnBuscar;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtIdentificacion;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNombre;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtHistoria;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridAñadidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ate;
        private System.Windows.Forms.DataGridViewTextBoxColumn hc;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_ingreso;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private Infragistics.Win.Misc.UltraButton btnCancelar;
    }
}