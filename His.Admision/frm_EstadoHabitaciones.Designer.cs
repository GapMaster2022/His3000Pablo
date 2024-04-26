namespace His.Admision
{
    partial class frm_EstadoHabitaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_EstadoHabitaciones));
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_infestado = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.grid_estados = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_infespecialidad = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_infmedico = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_infatencion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_infpaciente = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_infhistoriaclinica = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_inffecfacturacion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_inffecdisponible = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_inffecalta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_inffecingreso = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_infhabitacion = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_estados)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnActualizar,
            this.btnGuardar,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(633, 36);
            this.menu.TabIndex = 63;
            this.menu.Text = "menu";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = global::His.Admision.Properties.Resources.HIS_EDITAR;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(78, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::His.Admision.Properties.Resources.HIS_GUARDAR;
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(69, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::His.Admision.Properties.Resources.HIS_CANCELAR;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 33);
            this.btnCancelar.Text = "Cancelar";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = global::His.Admision.Properties.Resources.HIS_SALIR;
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(49, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Silver;
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txt_infestado);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.grid_estados);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txt_infespecialidad);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_infmedico);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_infatencion);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_infpaciente);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_infhistoriaclinica);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_inffecfacturacion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_inffecdisponible);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_inffecalta);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_inffecingreso);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_infhabitacion);
            this.groupBox1.Location = new System.Drawing.Point(27, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 469);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de Habitación";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(235, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Estado Actual:";
            // 
            // txt_infestado
            // 
            this.txt_infestado.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_infestado.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infestado.Location = new System.Drawing.Point(238, 89);
            this.txt_infestado.Multiline = true;
            this.txt_infestado.Name = "txt_infestado";
            this.txt_infestado.ReadOnly = true;
            this.txt_infestado.Size = new System.Drawing.Size(297, 20);
            this.txt_infestado.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(225, 385);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Observación:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(228, 401);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(326, 20);
            this.textBox1.TabIndex = 0;
            // 
            // grid_estados
            // 
            this.grid_estados.AllowUserToAddRows = false;
            this.grid_estados.BackgroundColor = System.Drawing.Color.Silver;
            this.grid_estados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid_estados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_estados.Location = new System.Drawing.Point(228, 128);
            this.grid_estados.Name = "grid_estados";
            this.grid_estados.RowHeadersVisible = false;
            this.grid_estados.Size = new System.Drawing.Size(326, 254);
            this.grid_estados.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 385);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Especialidad:";
            // 
            // txt_infespecialidad
            // 
            this.txt_infespecialidad.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_infespecialidad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infespecialidad.Location = new System.Drawing.Point(18, 401);
            this.txt_infespecialidad.Multiline = true;
            this.txt_infespecialidad.Name = "txt_infespecialidad";
            this.txt_infespecialidad.ReadOnly = true;
            this.txt_infespecialidad.Size = new System.Drawing.Size(153, 20);
            this.txt_infespecialidad.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 346);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Médico Tratante:";
            // 
            // txt_infmedico
            // 
            this.txt_infmedico.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_infmedico.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infmedico.Location = new System.Drawing.Point(18, 362);
            this.txt_infmedico.Multiline = true;
            this.txt_infmedico.Name = "txt_infmedico";
            this.txt_infmedico.ReadOnly = true;
            this.txt_infmedico.Size = new System.Drawing.Size(153, 20);
            this.txt_infmedico.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 307);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "No. Atención:";
            // 
            // txt_infatencion
            // 
            this.txt_infatencion.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_infatencion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infatencion.Location = new System.Drawing.Point(18, 323);
            this.txt_infatencion.Multiline = true;
            this.txt_infatencion.Name = "txt_infatencion";
            this.txt_infatencion.ReadOnly = true;
            this.txt_infatencion.Size = new System.Drawing.Size(153, 20);
            this.txt_infatencion.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 268);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Nombre Paciente: ";
            // 
            // txt_infpaciente
            // 
            this.txt_infpaciente.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_infpaciente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infpaciente.Location = new System.Drawing.Point(18, 284);
            this.txt_infpaciente.Multiline = true;
            this.txt_infpaciente.Name = "txt_infpaciente";
            this.txt_infpaciente.ReadOnly = true;
            this.txt_infpaciente.Size = new System.Drawing.Size(153, 20);
            this.txt_infpaciente.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "No. Historia Clinica:";
            // 
            // txt_infhistoriaclinica
            // 
            this.txt_infhistoriaclinica.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_infhistoriaclinica.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infhistoriaclinica.Location = new System.Drawing.Point(18, 245);
            this.txt_infhistoriaclinica.Multiline = true;
            this.txt_infhistoriaclinica.Name = "txt_infhistoriaclinica";
            this.txt_infhistoriaclinica.ReadOnly = true;
            this.txt_infhistoriaclinica.Size = new System.Drawing.Size(153, 20);
            this.txt_infhistoriaclinica.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Fecha Facturación:";
            // 
            // txt_inffecfacturacion
            // 
            this.txt_inffecfacturacion.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_inffecfacturacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_inffecfacturacion.Location = new System.Drawing.Point(18, 206);
            this.txt_inffecfacturacion.Multiline = true;
            this.txt_inffecfacturacion.Name = "txt_inffecfacturacion";
            this.txt_inffecfacturacion.ReadOnly = true;
            this.txt_inffecfacturacion.Size = new System.Drawing.Size(153, 20);
            this.txt_inffecfacturacion.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Fecha Disponibilidad:";
            // 
            // txt_inffecdisponible
            // 
            this.txt_inffecdisponible.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_inffecdisponible.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_inffecdisponible.Location = new System.Drawing.Point(18, 167);
            this.txt_inffecdisponible.Multiline = true;
            this.txt_inffecdisponible.Name = "txt_inffecdisponible";
            this.txt_inffecdisponible.ReadOnly = true;
            this.txt_inffecdisponible.Size = new System.Drawing.Size(153, 20);
            this.txt_inffecdisponible.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Fecha Alta: ";
            // 
            // txt_inffecalta
            // 
            this.txt_inffecalta.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_inffecalta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_inffecalta.Location = new System.Drawing.Point(18, 128);
            this.txt_inffecalta.Multiline = true;
            this.txt_inffecalta.Name = "txt_inffecalta";
            this.txt_inffecalta.ReadOnly = true;
            this.txt_inffecalta.Size = new System.Drawing.Size(153, 20);
            this.txt_inffecalta.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha Ingreso:";
            // 
            // txt_inffecingreso
            // 
            this.txt_inffecingreso.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_inffecingreso.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_inffecingreso.Location = new System.Drawing.Point(18, 89);
            this.txt_inffecingreso.Multiline = true;
            this.txt_inffecingreso.Name = "txt_inffecingreso";
            this.txt_inffecingreso.ReadOnly = true;
            this.txt_inffecingreso.Size = new System.Drawing.Size(153, 20);
            this.txt_inffecingreso.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "No Habitación";
            // 
            // txt_infhabitacion
            // 
            this.txt_infhabitacion.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_infhabitacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infhabitacion.Location = new System.Drawing.Point(18, 50);
            this.txt_infhabitacion.Multiline = true;
            this.txt_infhabitacion.Name = "txt_infhabitacion";
            this.txt_infhabitacion.ReadOnly = true;
            this.txt_infhabitacion.Size = new System.Drawing.Size(153, 20);
            this.txt_infhabitacion.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "blanco.JPG");
            this.imageList1.Images.SetKeyName(1, "red_button.ico");
            this.imageList1.Images.SetKeyName(2, "orange_button.ico");
            this.imageList1.Images.SetKeyName(3, "turquoise_button.ico");
            this.imageList1.Images.SetKeyName(4, "violet_button.ico");
            this.imageList1.Images.SetKeyName(5, "blue_button.ico");
            this.imageList1.Images.SetKeyName(6, "green_button.ico");
            // 
            // frm_EstadoHabitaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 537);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menu);
            this.Name = "frm_EstadoHabitaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estado de Habitaciones";
            this.Load += new System.EventHandler(this.frm_EstadoHabitaciones_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_estados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_infespecialidad;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_infmedico;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_infatencion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_infpaciente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_infhistoriaclinica;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_inffecfacturacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_inffecdisponible;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_inffecalta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_inffecingreso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_infhabitacion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView grid_estados;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_infestado;
        private System.Windows.Forms.ImageList imageList1;
    }
}