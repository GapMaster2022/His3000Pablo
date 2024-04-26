
namespace CuentaPaciente
{
    partial class EstadoFechas
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
            this.f_Ingreso = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.f_Alta = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.chb_Valores = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // f_Ingreso
            // 
            this.f_Ingreso.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.f_Ingreso.Location = new System.Drawing.Point(44, 90);
            this.f_Ingreso.Name = "f_Ingreso";
            this.f_Ingreso.Size = new System.Drawing.Size(117, 20);
            this.f_Ingreso.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "FECHA INGRESO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "FECHA ACTUAL";
            // 
            // f_Alta
            // 
            this.f_Alta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.f_Alta.Location = new System.Drawing.Point(199, 90);
            this.f_Alta.Name = "f_Alta";
            this.f_Alta.Size = new System.Drawing.Size(117, 20);
            this.f_Alta.TabIndex = 2;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(234, 126);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(82, 32);
            this.btnGenerar.TabIndex = 4;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "PACIENTE:";
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Location = new System.Drawing.Point(81, 31);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(35, 13);
            this.lblPaciente.TabIndex = 6;
            this.lblPaciente.Text = "label4";
            // 
            // chb_Valores
            // 
            this.chb_Valores.AutoSize = true;
            this.chb_Valores.Location = new System.Drawing.Point(44, 135);
            this.chb_Valores.Name = "chb_Valores";
            this.chb_Valores.Size = new System.Drawing.Size(151, 17);
            this.chb_Valores.TabIndex = 7;
            this.chb_Valores.Text = "Incluir valores automáticos";
            this.chb_Valores.UseVisualStyleBackColor = true;
            // 
            // EstadoFechas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 169);
            this.Controls.Add(this.chb_Valores);
            this.Controls.Add(this.lblPaciente);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.f_Alta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.f_Ingreso);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EstadoFechas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estado x Fechas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker f_Ingreso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker f_Alta;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.CheckBox chb_Valores;
    }
}