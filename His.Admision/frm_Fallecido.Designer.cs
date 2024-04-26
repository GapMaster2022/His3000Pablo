namespace His.Admision
{
    partial class frm_Fallecido
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
            Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab17 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab18 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.ultraTabPageControl14 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.txtfechafallecio = new System.Windows.Forms.DateTimePicker();
            this.txtdiagnostico = new System.Windows.Forms.TextBox();
            this.txtmotivo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtfolio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ckbPasaporte = new System.Windows.Forms.CheckBox();
            this.ckbCedula = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtidentificacion = new System.Windows.Forms.TextBox();
            this.dtpEntrega = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtcelular = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txttelefono1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtnombreapellido = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabulador2 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnmodificar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ultraTabPageControl14.SuspendLayout();
            this.ultraTabPageControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabulador2)).BeginInit();
            this.tabulador2.SuspendLayout();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraTabPageControl14
            // 
            this.ultraTabPageControl14.Controls.Add(this.dtpHora);
            this.ultraTabPageControl14.Controls.Add(this.txtfechafallecio);
            this.ultraTabPageControl14.Controls.Add(this.txtdiagnostico);
            this.ultraTabPageControl14.Controls.Add(this.txtmotivo);
            this.ultraTabPageControl14.Controls.Add(this.label4);
            this.ultraTabPageControl14.Controls.Add(this.label3);
            this.ultraTabPageControl14.Controls.Add(this.label2);
            this.ultraTabPageControl14.Controls.Add(this.txtfolio);
            this.ultraTabPageControl14.Controls.Add(this.label1);
            this.ultraTabPageControl14.Location = new System.Drawing.Point(1, 22);
            this.ultraTabPageControl14.Name = "ultraTabPageControl14";
            this.ultraTabPageControl14.Size = new System.Drawing.Size(737, 161);
            // 
            // dtpHora
            // 
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHora.Location = new System.Drawing.Point(641, 7);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.Size = new System.Drawing.Size(70, 20);
            this.dtpHora.TabIndex = 9;
            // 
            // txtfechafallecio
            // 
            this.txtfechafallecio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtfechafallecio.Location = new System.Drawing.Point(544, 7);
            this.txtfechafallecio.Name = "txtfechafallecio";
            this.txtfechafallecio.Size = new System.Drawing.Size(91, 20);
            this.txtfechafallecio.TabIndex = 8;
            // 
            // txtdiagnostico
            // 
            this.txtdiagnostico.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtdiagnostico.Location = new System.Drawing.Point(93, 88);
            this.txtdiagnostico.Name = "txtdiagnostico";
            this.txtdiagnostico.Size = new System.Drawing.Size(607, 20);
            this.txtdiagnostico.TabIndex = 7;
            this.txtdiagnostico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtdiagnostico_KeyDown);
            // 
            // txtmotivo
            // 
            this.txtmotivo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtmotivo.Location = new System.Drawing.Point(61, 45);
            this.txtmotivo.Name = "txtmotivo";
            this.txtmotivo.Size = new System.Drawing.Size(639, 20);
            this.txtmotivo.TabIndex = 6;
            this.txtmotivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtmotivo_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "DIAGNOSTICO:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "MOTIVO:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "FECHA Y HORA DE DEFUNCIÓN:";
            // 
            // txtfolio
            // 
            this.txtfolio.Location = new System.Drawing.Point(139, 7);
            this.txtfolio.Name = "txtfolio";
            this.txtfolio.Size = new System.Drawing.Size(190, 20);
            this.txtfolio.TabIndex = 1;
            this.txtfolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtfolio_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "No. FORMULARIO IEDG:";
            // 
            // ultraTabPageControl5
            // 
            this.ultraTabPageControl5.Controls.Add(this.ckbPasaporte);
            this.ultraTabPageControl5.Controls.Add(this.ckbCedula);
            this.ultraTabPageControl5.Controls.Add(this.label5);
            this.ultraTabPageControl5.Controls.Add(this.txtidentificacion);
            this.ultraTabPageControl5.Controls.Add(this.dtpEntrega);
            this.ultraTabPageControl5.Controls.Add(this.label10);
            this.ultraTabPageControl5.Controls.Add(this.txtemail);
            this.ultraTabPageControl5.Controls.Add(this.label9);
            this.ultraTabPageControl5.Controls.Add(this.txtcelular);
            this.ultraTabPageControl5.Controls.Add(this.label8);
            this.ultraTabPageControl5.Controls.Add(this.txttelefono1);
            this.ultraTabPageControl5.Controls.Add(this.label7);
            this.ultraTabPageControl5.Controls.Add(this.txtnombreapellido);
            this.ultraTabPageControl5.Controls.Add(this.label6);
            this.ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl5.Name = "ultraTabPageControl5";
            this.ultraTabPageControl5.Size = new System.Drawing.Size(737, 161);
            // 
            // ckbPasaporte
            // 
            this.ckbPasaporte.AutoSize = true;
            this.ckbPasaporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbPasaporte.Location = new System.Drawing.Point(116, 16);
            this.ckbPasaporte.Name = "ckbPasaporte";
            this.ckbPasaporte.Size = new System.Drawing.Size(83, 17);
            this.ckbPasaporte.TabIndex = 15;
            this.ckbPasaporte.Text = "Pasaporte";
            this.ckbPasaporte.UseVisualStyleBackColor = true;
            this.ckbPasaporte.CheckedChanged += new System.EventHandler(this.ckbPasaporte_CheckedChanged);
            // 
            // ckbCedula
            // 
            this.ckbCedula.AutoSize = true;
            this.ckbCedula.Checked = true;
            this.ckbCedula.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbCedula.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbCedula.Location = new System.Drawing.Point(14, 16);
            this.ckbCedula.Name = "ckbCedula";
            this.ckbCedula.Size = new System.Drawing.Size(65, 17);
            this.ckbCedula.TabIndex = 14;
            this.ckbCedula.Text = "Cédula";
            this.ckbCedula.UseVisualStyleBackColor = true;
            this.ckbCedula.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(281, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "IDENTIFICACIÓN:";
            // 
            // txtidentificacion
            // 
            this.txtidentificacion.Location = new System.Drawing.Point(382, 14);
            this.txtidentificacion.MaxLength = 10;
            this.txtidentificacion.Name = "txtidentificacion";
            this.txtidentificacion.Size = new System.Drawing.Size(126, 20);
            this.txtidentificacion.TabIndex = 12;
            this.txtidentificacion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtidentificacion_KeyDown);
            this.txtidentificacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtidentificacion_KeyPress);
            // 
            // dtpEntrega
            // 
            this.dtpEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEntrega.Location = new System.Drawing.Point(507, 119);
            this.dtpEntrega.Name = "dtpEntrega";
            this.dtpEntrega.Size = new System.Drawing.Size(100, 20);
            this.dtpEntrega.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(281, 125);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(220, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "FECHA EN QUE RECIBE EL DOCUMENTO:";
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(329, 85);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(351, 20);
            this.txtemail.TabIndex = 9;
            this.txtemail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtemail_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(281, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "EMAIL:";
            // 
            // txtcelular
            // 
            this.txtcelular.Location = new System.Drawing.Point(84, 122);
            this.txtcelular.MaxLength = 10;
            this.txtcelular.Name = "txtcelular";
            this.txtcelular.Size = new System.Drawing.Size(117, 20);
            this.txtcelular.TabIndex = 7;
            this.txtcelular.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcelular_KeyDown);
            this.txtcelular.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcelular_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "CÉLULAR:";
            // 
            // txttelefono1
            // 
            this.txttelefono1.Location = new System.Drawing.Point(84, 85);
            this.txttelefono1.MaxLength = 9;
            this.txttelefono1.Name = "txttelefono1";
            this.txttelefono1.Size = new System.Drawing.Size(117, 20);
            this.txttelefono1.TabIndex = 5;
            this.txttelefono1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttelefono1_KeyDown);
            this.txttelefono1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttelefono1_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "TELÉFONO:";
            // 
            // txtnombreapellido
            // 
            this.txtnombreapellido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtnombreapellido.Location = new System.Drawing.Point(139, 49);
            this.txtnombreapellido.Name = "txtnombreapellido";
            this.txtnombreapellido.Size = new System.Drawing.Size(541, 20);
            this.txtnombreapellido.TabIndex = 3;
            this.txtnombreapellido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtnombreapellido_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "NOMBRE Y APELLIDO:";
            // 
            // tabulador2
            // 
            appearance78.BorderColor = System.Drawing.Color.DarkGray;
            appearance78.ForeColor = System.Drawing.Color.Black;
            this.tabulador2.Appearance = appearance78;
            this.tabulador2.Controls.Add(this.ultraTabSharedControlsPage2);
            this.tabulador2.Controls.Add(this.ultraTabPageControl5);
            this.tabulador2.Controls.Add(this.ultraTabPageControl14);
            this.tabulador2.Controls.Add(this.ultraTabSharedControlsPage1);
            this.tabulador2.Location = new System.Drawing.Point(0, 60);
            this.tabulador2.Name = "tabulador2";
            appearance79.BorderColor = System.Drawing.Color.Black;
            appearance79.FontData.BoldAsString = "True";
            appearance79.ForeColor = System.Drawing.Color.Navy;
            this.tabulador2.SelectedTabAppearance = appearance79;
            this.tabulador2.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.tabulador2.Size = new System.Drawing.Size(739, 184);
            this.tabulador2.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Office2007Ribbon;
            this.tabulador2.TabIndex = 13;
            this.tabulador2.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowSizeToFit;
            ultraTab17.Key = "defuncion";
            ultraTab17.TabPage = this.ultraTabPageControl14;
            ultraTab17.Text = "Registro de información de Defunción";
            ultraTab18.Key = "acompanante";
            ultraTab18.TabPage = this.ultraTabPageControl5;
            ultraTab18.Text = "Datos de Familiar que realiza el trámite";
            this.tabulador2.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab17,
            ultraTab18});
            this.tabulador2.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard;
            // 
            // ultraTabSharedControlsPage2
            // 
            this.ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
            this.ultraTabSharedControlsPage2.Size = new System.Drawing.Size(834, 79);
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(737, 161);
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnmodificar,
            this.btnGuardar,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(739, 37);
            this.menu.TabIndex = 14;
            this.menu.Text = "menu";
            // 
            // btnmodificar
            // 
            this.btnmodificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmodificar.ForeColor = System.Drawing.Color.Black;
            this.btnmodificar.Image = global::His.Admision.Properties.Resources.HIS_EDITAR;
            this.btnmodificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnmodificar.Name = "btnmodificar";
            this.btnmodificar.Size = new System.Drawing.Size(86, 34);
            this.btnmodificar.Text = "Modificar";
            this.btnmodificar.Click += new System.EventHandler(this.btnmodificar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.Image = global::His.Admision.Properties.Resources.HIS_GUARDAR;
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(81, 34);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Image = global::His.Admision.Properties.Resources.HIS_CANCELAR;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 34);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.Black;
            this.btnCerrar.Image = global::His.Admision.Properties.Resources.HIS_SALIR;
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(63, 34);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frm_Fallecido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 246);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.tabulador2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_Fallecido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Defunción";
            this.Load += new System.EventHandler(this.frm_Fallecido_Load);
            this.ultraTabPageControl14.ResumeLayout(false);
            this.ultraTabPageControl14.PerformLayout();
            this.ultraTabPageControl5.ResumeLayout(false);
            this.ultraTabPageControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabulador2)).EndInit();
            this.tabulador2.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinTabControl.UltraTabControl tabulador2;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage2;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl5;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl14;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnmodificar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtcelular;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txttelefono1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtnombreapellido;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtdiagnostico;
        private System.Windows.Forms.TextBox txtmotivo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtfolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEntrega;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox ckbPasaporte;
        private System.Windows.Forms.CheckBox ckbCedula;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtidentificacion;
        private System.Windows.Forms.DateTimePicker txtfechafallecio;
        private System.Windows.Forms.DateTimePicker dtpHora;
    }
}