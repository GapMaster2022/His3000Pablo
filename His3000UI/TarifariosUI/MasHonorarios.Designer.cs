namespace TarifariosUI
{
    partial class MasHonorarios
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
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtmedico = new System.Windows.Forms.TextBox();
            this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
            this.lbl_medPrincipal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtTotalMed1 = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenera = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraGroupBox2
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.ultraGroupBox2.Appearance = appearance4;
            this.ultraGroupBox2.Controls.Add(this.txtmedico);
            this.ultraGroupBox2.Controls.Add(this.ultraButton2);
            this.ultraGroupBox2.Controls.Add(this.lbl_medPrincipal);
            this.ultraGroupBox2.Location = new System.Drawing.Point(12, 12);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(514, 96);
            this.ultraGroupBox2.TabIndex = 29;
            this.ultraGroupBox2.Text = "Medico";
            // 
            // txtmedico
            // 
            this.txtmedico.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtmedico.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmedico.Location = new System.Drawing.Point(46, 22);
            this.txtmedico.Name = "txtmedico";
            this.txtmedico.ReadOnly = true;
            this.txtmedico.Size = new System.Drawing.Size(73, 22);
            this.txtmedico.TabIndex = 25;
            // 
            // ultraButton2
            // 
            appearance1.ForeColor = System.Drawing.Color.Navy;
            this.ultraButton2.Appearance = appearance1;
            this.ultraButton2.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            this.ultraButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraButton2.Location = new System.Drawing.Point(9, 19);
            this.ultraButton2.Name = "ultraButton2";
            this.ultraButton2.Size = new System.Drawing.Size(31, 27);
            this.ultraButton2.TabIndex = 24;
            this.ultraButton2.Text = "F1";
            this.ultraButton2.Click += new System.EventHandler(this.ultraButton2_Click);
            // 
            // lbl_medPrincipal
            // 
            this.lbl_medPrincipal.AutoSize = true;
            this.lbl_medPrincipal.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_medPrincipal.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_medPrincipal.Location = new System.Drawing.Point(17, 64);
            this.lbl_medPrincipal.Name = "lbl_medPrincipal";
            this.lbl_medPrincipal.Size = new System.Drawing.Size(23, 18);
            this.lbl_medPrincipal.TabIndex = 6;
            this.lbl_medPrincipal.Text = "...";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(403, 187);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(69, 22);
            this.txtTotal.TabIndex = 26;
            // 
            // txtTotalMed1
            // 
            this.txtTotalMed1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTotalMed1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalMed1.Location = new System.Drawing.Point(179, 140);
            this.txtTotalMed1.Name = "txtTotalMed1";
            this.txtTotalMed1.ReadOnly = true;
            this.txtTotalMed1.Size = new System.Drawing.Size(69, 22);
            this.txtTotalMed1.TabIndex = 30;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(417, 237);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(109, 40);
            this.btnGuardar.TabIndex = 31;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Total Médico principal:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "% de cobro Médico Actual:";
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPorcentaje.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentaje.Location = new System.Drawing.Point(403, 140);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Size = new System.Drawing.Size(69, 22);
            this.txtPorcentaje.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Total a generar Honorario:";
            // 
            // btnGenera
            // 
            this.btnGenera.Location = new System.Drawing.Point(100, 177);
            this.btnGenera.Name = "btnGenera";
            this.btnGenera.Size = new System.Drawing.Size(109, 40);
            this.btnGenera.TabIndex = 36;
            this.btnGenera.Text = "GENERAR";
            this.btnGenera.UseVisualStyleBackColor = true;
            this.btnGenera.Click += new System.EventHandler(this.btnGenera_Click);
            // 
            // MasHonorarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 289);
            this.Controls.Add(this.btnGenera);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPorcentaje);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtTotalMed1);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.ultraGroupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MasHonorarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Honorarios";
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.TextBox txtmedico;
        private Infragistics.Win.Misc.UltraButton ultraButton2;
        private System.Windows.Forms.Label lbl_medPrincipal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtTotalMed1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGenera;
    }
}