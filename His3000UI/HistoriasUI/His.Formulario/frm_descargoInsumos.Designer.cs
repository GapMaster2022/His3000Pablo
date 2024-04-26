namespace His.Formulario
{
    partial class frm_descargoInsumos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_descargoInsumos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCodMedicamento = new System.Windows.Forms.Label();
            this.lblMedicamento = new System.Windows.Forms.TextBox();
            this.btnCargar = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblCodPaciente = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.btnMedicamento = new System.Windows.Forms.Button();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.P_MENU = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.P_CENTRAL = new System.Windows.Forms.Panel();
            this.dtgCabeceraKardex = new System.Windows.Forms.DataGridView();
            this.P_FOOTER = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.horaAdministracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.P_MENU.SuspendLayout();
            this.P_CENTRAL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCabeceraKardex)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCodMedicamento);
            this.groupBox1.Controls.Add(this.lblMedicamento);
            this.groupBox1.Controls.Add(this.btnCargar);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.lblCodPaciente);
            this.groupBox1.Controls.Add(this.lblHora);
            this.groupBox1.Controls.Add(this.btnMedicamento);
            this.groupBox1.Controls.Add(this.lblPaciente);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(933, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CABECERA KARDEX";
            // 
            // lblCodMedicamento
            // 
            this.lblCodMedicamento.AutoSize = true;
            this.lblCodMedicamento.Location = new System.Drawing.Point(413, 35);
            this.lblCodMedicamento.Name = "lblCodMedicamento";
            this.lblCodMedicamento.Size = new System.Drawing.Size(46, 13);
            this.lblCodMedicamento.TabIndex = 19;
            this.lblCodMedicamento.Text = "codMed";
            this.lblCodMedicamento.Visible = false;
            // 
            // lblMedicamento
            // 
            this.lblMedicamento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lblMedicamento.Location = new System.Drawing.Point(44, 32);
            this.lblMedicamento.Name = "lblMedicamento";
            this.lblMedicamento.ReadOnly = true;
            this.lblMedicamento.Size = new System.Drawing.Size(345, 20);
            this.lblMedicamento.TabIndex = 18;
            // 
            // btnCargar
            // 
            this.btnCargar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargar.Location = new System.Drawing.Point(721, 35);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(181, 23);
            this.btnCargar.TabIndex = 17;
            this.btnCargar.Text = "CARGA INSUMOS";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(513, 36);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(118, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Trae Insumo Propio";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblCodPaciente
            // 
            this.lblCodPaciente.AutoSize = true;
            this.lblCodPaciente.Location = new System.Drawing.Point(413, 16);
            this.lblCodPaciente.Name = "lblCodPaciente";
            this.lblCodPaciente.Size = new System.Drawing.Size(25, 13);
            this.lblCodPaciente.TabIndex = 10;
            this.lblCodPaciente.Text = "cod";
            this.lblCodPaciente.Visible = false;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.Location = new System.Drawing.Point(873, 7);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(51, 25);
            this.lblHora.TabIndex = 9;
            this.lblHora.Text = "hora";
            this.lblHora.Visible = false;
            // 
            // btnMedicamento
            // 
            this.btnMedicamento.Location = new System.Drawing.Point(6, 32);
            this.btnMedicamento.Name = "btnMedicamento";
            this.btnMedicamento.Size = new System.Drawing.Size(29, 23);
            this.btnMedicamento.TabIndex = 3;
            this.btnMedicamento.Text = "F1";
            this.btnMedicamento.UseVisualStyleBackColor = true;
            this.btnMedicamento.Click += new System.EventHandler(this.btnMedicamento_Click);
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Location = new System.Drawing.Point(64, 16);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(48, 13);
            this.lblPaciente.TabIndex = 1;
            this.lblPaciente.Text = "paciente";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paciente:";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(703, 3);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(60, 25);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "fecha";
            // 
            // P_MENU
            // 
            this.P_MENU.Controls.Add(this.lblUsuario);
            this.P_MENU.Controls.Add(this.btnCancelar);
            this.P_MENU.Controls.Add(this.btnImprimir);
            this.P_MENU.Controls.Add(this.lblFecha);
            this.P_MENU.Dock = System.Windows.Forms.DockStyle.Top;
            this.P_MENU.Location = new System.Drawing.Point(0, 0);
            this.P_MENU.Name = "P_MENU";
            this.P_MENU.Size = new System.Drawing.Size(942, 49);
            this.P_MENU.TabIndex = 1;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(705, 27);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(41, 16);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "fecha";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(146, 6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(42, 37);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.Location = new System.Drawing.Point(6, 6);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(42, 37);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // P_CENTRAL
            // 
            this.P_CENTRAL.Controls.Add(this.dtgCabeceraKardex);
            this.P_CENTRAL.Controls.Add(this.groupBox1);
            this.P_CENTRAL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.P_CENTRAL.Location = new System.Drawing.Point(0, 49);
            this.P_CENTRAL.Name = "P_CENTRAL";
            this.P_CENTRAL.Size = new System.Drawing.Size(942, 700);
            this.P_CENTRAL.TabIndex = 2;
            // 
            // dtgCabeceraKardex
            // 
            this.dtgCabeceraKardex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgCabeceraKardex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_producto,
            this.detalle,
            this.horaAdministracion});
            this.dtgCabeceraKardex.Location = new System.Drawing.Point(34, 131);
            this.dtgCabeceraKardex.Name = "dtgCabeceraKardex";
            this.dtgCabeceraKardex.ReadOnly = true;
            this.dtgCabeceraKardex.Size = new System.Drawing.Size(874, 509);
            this.dtgCabeceraKardex.TabIndex = 1;
            // 
            // P_FOOTER
            // 
            this.P_FOOTER.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.P_FOOTER.Location = new System.Drawing.Point(0, 721);
            this.P_FOOTER.Name = "P_FOOTER";
            this.P_FOOTER.Size = new System.Drawing.Size(942, 28);
            this.P_FOOTER.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "PRESENTACIÓN";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "HORA";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "INI";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "FUNCIONARIO";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "ID";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "PRESENTACIÓN";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 400;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "VÍA";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "DOSIS UNITARIA";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "FRECUENCIA";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Frecuencia";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "FRECUENCIA";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "HORA";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // id_producto
            // 
            this.id_producto.HeaderText = "ID";
            this.id_producto.Name = "id_producto";
            this.id_producto.ReadOnly = true;
            // 
            // detalle
            // 
            this.detalle.HeaderText = "PRESENTACIÓN";
            this.detalle.Name = "detalle";
            this.detalle.ReadOnly = true;
            this.detalle.Width = 600;
            // 
            // horaAdministracion
            // 
            this.horaAdministracion.HeaderText = "HORA";
            this.horaAdministracion.Name = "horaAdministracion";
            this.horaAdministracion.ReadOnly = true;
            // 
            // frm_descargoInsumos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 749);
            this.Controls.Add(this.P_FOOTER);
            this.Controls.Add(this.P_CENTRAL);
            this.Controls.Add(this.P_MENU);
            this.Name = "frm_descargoInsumos";
            this.Text = "frm_admisnitracionMedicamentos";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.P_MENU.ResumeLayout(false);
            this.P_MENU.PerformLayout();
            this.P_CENTRAL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCabeceraKardex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblCodPaciente;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Button btnMedicamento;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel P_MENU;
        private System.Windows.Forms.Panel P_CENTRAL;
        private System.Windows.Forms.Panel P_FOOTER;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DataGridView dtgCabeceraKardex;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.TextBox lblMedicamento;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblCodMedicamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn detalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn horaAdministracion;
    }
}