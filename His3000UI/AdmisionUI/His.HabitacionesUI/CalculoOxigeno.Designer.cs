namespace His.HabitacionesUI
{
    partial class CalculoOxigeno
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
            this.cmbHora = new System.Windows.Forms.ComboBox();
            this.cmbLitos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGenraTotal = new System.Windows.Forms.Button();
            this.btnCargaOxigeno = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbHora
            // 
            this.cmbHora.FormattingEnabled = true;
            this.cmbHora.Location = new System.Drawing.Point(123, 115);
            this.cmbHora.Name = "cmbHora";
            this.cmbHora.Size = new System.Drawing.Size(68, 21);
            this.cmbHora.TabIndex = 0;
            // 
            // cmbLitos
            // 
            this.cmbLitos.FormattingEnabled = true;
            this.cmbLitos.Location = new System.Drawing.Point(306, 115);
            this.cmbLitos.Name = "cmbLitos";
            this.cmbLitos.Size = new System.Drawing.Size(66, 21);
            this.cmbLitos.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Calculo de Oxígeno y Aíre Comprimido";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Horas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Flujo x min";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(72, 180);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 93);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TOTAL LITROS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // btnGenraTotal
            // 
            this.btnGenraTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenraTotal.Location = new System.Drawing.Point(297, 142);
            this.btnGenraTotal.Name = "btnGenraTotal";
            this.btnGenraTotal.Size = new System.Drawing.Size(75, 32);
            this.btnGenraTotal.TabIndex = 6;
            this.btnGenraTotal.Text = "GENERA";
            this.btnGenraTotal.UseVisualStyleBackColor = true;
            this.btnGenraTotal.Click += new System.EventHandler(this.btnGenraTotal_Click);
            // 
            // btnCargaOxigeno
            // 
            this.btnCargaOxigeno.Location = new System.Drawing.Point(72, 277);
            this.btnCargaOxigeno.Name = "btnCargaOxigeno";
            this.btnCargaOxigeno.Size = new System.Drawing.Size(75, 23);
            this.btnCargaOxigeno.TabIndex = 7;
            this.btnCargaOxigeno.Text = "CARGAR";
            this.btnCargaOxigeno.UseVisualStyleBackColor = true;
            this.btnCargaOxigeno.Click += new System.EventHandler(this.btnCargaOxigeno_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(297, 277);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "CANCELAR";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // CalculoOxigeno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 312);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnCargaOxigeno);
            this.Controls.Add(this.btnGenraTotal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLitos);
            this.Controls.Add(this.cmbHora);
            this.Name = "CalculoOxigeno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculo de Oxígeno";
            this.Load += new System.EventHandler(this.CalculoOxigeno_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbHora;
        private System.Windows.Forms.ComboBox cmbLitos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGenraTotal;
        private System.Windows.Forms.Button btnCargaOxigeno;
        private System.Windows.Forms.Button button3;
    }
}