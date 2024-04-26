namespace His.Admision
{
    partial class frm_Habitaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Habitaciones));
            this.grid_habitaciones = new System.Windows.Forms.DataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mnuContextHab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnAsignaHabitaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMuevePaciente = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuColocaPaciente = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCambioEstado = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnCancelar = new System.Windows.Forms.ToolStripMenuItem();
            this.chk_encargado = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_infencargado = new System.Windows.Forms.CheckBox();
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
            this.grid_informativo = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grid_habitaciones)).BeginInit();
            this.mnuContextHab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_informativo)).BeginInit();
            this.SuspendLayout();
            // 
            // grid_habitaciones
            // 
            this.grid_habitaciones.AllowUserToAddRows = false;
            this.grid_habitaciones.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.grid_habitaciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid_habitaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_habitaciones.ColumnHeadersVisible = false;
            this.grid_habitaciones.Location = new System.Drawing.Point(34, 70);
            this.grid_habitaciones.Name = "grid_habitaciones";
            this.grid_habitaciones.RowHeadersVisible = false;
            this.grid_habitaciones.Size = new System.Drawing.Size(993, 283);
            this.grid_habitaciones.TabIndex = 0;
            this.grid_habitaciones.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_habitaciones_CellMouseClick);
            this.grid_habitaciones.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_habitaciones_CellContentDoubleClick);
            this.grid_habitaciones.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_habitaciones_CellEnter);
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
            // mnuContextHab
            // 
            this.mnuContextHab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnAsignaHabitaciones,
            this.mnuMuevePaciente,
            this.mnuColocaPaciente,
            this.mnuCambioEstado,
            this.toolStripSeparator1,
            this.mnCancelar});
            this.mnuContextHab.Name = "mnuContext";
            this.mnuContextHab.Size = new System.Drawing.Size(219, 120);
            // 
            // mnAsignaHabitaciones
            // 
            this.mnAsignaHabitaciones.Name = "mnAsignaHabitaciones";
            this.mnAsignaHabitaciones.Size = new System.Drawing.Size(218, 22);
            this.mnAsignaHabitaciones.Text = "Asignacion  de Habitaciones";
            this.mnAsignaHabitaciones.Click += new System.EventHandler(this.mnAsignaHabitaciones_Click);
            // 
            // mnuMuevePaciente
            // 
            this.mnuMuevePaciente.Name = "mnuMuevePaciente";
            this.mnuMuevePaciente.Size = new System.Drawing.Size(218, 22);
            this.mnuMuevePaciente.Text = "Mover Paciente";
            this.mnuMuevePaciente.Click += new System.EventHandler(this.mnuMuevePaciente_Click);
            // 
            // mnuColocaPaciente
            // 
            this.mnuColocaPaciente.Name = "mnuColocaPaciente";
            this.mnuColocaPaciente.Size = new System.Drawing.Size(218, 22);
            this.mnuColocaPaciente.Text = "Colocar Paciente";
            this.mnuColocaPaciente.Click += new System.EventHandler(this.mnuColocaPaciente_Click);
            // 
            // mnuCambioEstado
            // 
            this.mnuCambioEstado.Name = "mnuCambioEstado";
            this.mnuCambioEstado.Size = new System.Drawing.Size(218, 22);
            this.mnuCambioEstado.Text = "Cambio de Estado";
            this.mnuCambioEstado.Click += new System.EventHandler(this.mnuCambioEstado_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
            // 
            // mnCancelar
            // 
            this.mnCancelar.Name = "mnCancelar";
            this.mnCancelar.Size = new System.Drawing.Size(218, 22);
            this.mnCancelar.Text = "Cancelar";
            // 
            // chk_encargado
            // 
            this.chk_encargado.AutoSize = true;
            this.chk_encargado.Location = new System.Drawing.Point(446, 37);
            this.chk_encargado.Name = "chk_encargado";
            this.chk_encargado.Size = new System.Drawing.Size(249, 17);
            this.chk_encargado.TabIndex = 1;
            this.chk_encargado.Text = "Paciente Encargado en la Habitación a asignar";
            this.chk_encargado.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_infencargado);
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
            this.groupBox1.Location = new System.Drawing.Point(1068, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 480);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de Habitación";
            // 
            // chk_infencargado
            // 
            this.chk_infencargado.AutoSize = true;
            this.chk_infencargado.Location = new System.Drawing.Point(23, 441);
            this.chk_infencargado.Name = "chk_infencargado";
            this.chk_infencargado.Size = new System.Drawing.Size(123, 17);
            this.chk_infencargado.TabIndex = 20;
            this.chk_infencargado.Text = "Paciente Encargado";
            this.chk_infencargado.UseVisualStyleBackColor = true;
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
            this.txt_infespecialidad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infespecialidad.Location = new System.Drawing.Point(18, 401);
            this.txt_infespecialidad.Multiline = true;
            this.txt_infespecialidad.Name = "txt_infespecialidad";
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
            this.txt_infmedico.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infmedico.Location = new System.Drawing.Point(18, 362);
            this.txt_infmedico.Multiline = true;
            this.txt_infmedico.Name = "txt_infmedico";
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
            this.txt_infatencion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infatencion.Location = new System.Drawing.Point(18, 323);
            this.txt_infatencion.Multiline = true;
            this.txt_infatencion.Name = "txt_infatencion";
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
            this.txt_infpaciente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infpaciente.Location = new System.Drawing.Point(18, 284);
            this.txt_infpaciente.Multiline = true;
            this.txt_infpaciente.Name = "txt_infpaciente";
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
            this.txt_infhistoriaclinica.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infhistoriaclinica.Location = new System.Drawing.Point(18, 245);
            this.txt_infhistoriaclinica.Multiline = true;
            this.txt_infhistoriaclinica.Name = "txt_infhistoriaclinica";
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
            this.txt_inffecfacturacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_inffecfacturacion.Location = new System.Drawing.Point(18, 206);
            this.txt_inffecfacturacion.Multiline = true;
            this.txt_inffecfacturacion.Name = "txt_inffecfacturacion";
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
            this.txt_inffecdisponible.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_inffecdisponible.Location = new System.Drawing.Point(18, 167);
            this.txt_inffecdisponible.Multiline = true;
            this.txt_inffecdisponible.Name = "txt_inffecdisponible";
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
            this.txt_inffecalta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_inffecalta.Location = new System.Drawing.Point(18, 128);
            this.txt_inffecalta.Multiline = true;
            this.txt_inffecalta.Name = "txt_inffecalta";
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
            this.txt_inffecingreso.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_inffecingreso.Location = new System.Drawing.Point(18, 89);
            this.txt_inffecingreso.Multiline = true;
            this.txt_inffecingreso.Name = "txt_inffecingreso";
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
            this.txt_infhabitacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_infhabitacion.Location = new System.Drawing.Point(18, 50);
            this.txt_infhabitacion.Multiline = true;
            this.txt_infhabitacion.Name = "txt_infhabitacion";
            this.txt_infhabitacion.Size = new System.Drawing.Size(153, 20);
            this.txt_infhabitacion.TabIndex = 0;
            // 
            // grid_informativo
            // 
            this.grid_informativo.AllowUserToAddRows = false;
            this.grid_informativo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.grid_informativo.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.grid_informativo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid_informativo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_informativo.Location = new System.Drawing.Point(785, 352);
            this.grid_informativo.Name = "grid_informativo";
            this.grid_informativo.RowHeadersVisible = false;
            this.grid_informativo.Size = new System.Drawing.Size(242, 166);
            this.grid_informativo.TabIndex = 3;
            // 
            // frm_Habitaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 563);
            this.Controls.Add(this.chk_encargado);
            this.Controls.Add(this.grid_habitaciones);
            this.Controls.Add(this.grid_informativo);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_Habitaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Habitaciones";
            this.Load += new System.EventHandler(this.frm_Habitaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid_habitaciones)).EndInit();
            this.mnuContextHab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_informativo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid_habitaciones;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip mnuContextHab;
        private System.Windows.Forms.ToolStripMenuItem mnAsignaHabitaciones;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnCancelar;
        private System.Windows.Forms.ToolStripMenuItem mnuMuevePaciente;
        private System.Windows.Forms.ToolStripMenuItem mnuColocaPaciente;
        private System.Windows.Forms.ToolStripMenuItem mnuCambioEstado;
        private System.Windows.Forms.CheckBox chk_encargado;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_infhabitacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_inffecingreso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_inffecalta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_inffecdisponible;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_inffecfacturacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_infhistoriaclinica;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_infatencion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_infpaciente;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_infmedico;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_infespecialidad;
        private System.Windows.Forms.DataGridView grid_informativo;
        private System.Windows.Forms.CheckBox chk_infencargado;
    }
}