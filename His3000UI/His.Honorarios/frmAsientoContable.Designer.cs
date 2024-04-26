namespace His.Honorarios
{
    partial class frmAsientoContable
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblAsiento = new System.Windows.Forms.Label();
            this.lblFechaAsiento = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFechaRet = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRetencion = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ASIENTO CONTABLE:";
            // 
            // lblAsiento
            // 
            this.lblAsiento.AutoSize = true;
            this.lblAsiento.Location = new System.Drawing.Point(135, 21);
            this.lblAsiento.Name = "lblAsiento";
            this.lblAsiento.Size = new System.Drawing.Size(117, 13);
            this.lblAsiento.TabIndex = 1;
            this.lblAsiento.Text = "ASIENTO CONTABLE:";
            // 
            // lblFechaAsiento
            // 
            this.lblFechaAsiento.AutoSize = true;
            this.lblFechaAsiento.Location = new System.Drawing.Point(329, 21);
            this.lblFechaAsiento.Name = "lblFechaAsiento";
            this.lblFechaAsiento.Size = new System.Drawing.Size(42, 13);
            this.lblFechaAsiento.TabIndex = 3;
            this.lblFechaAsiento.Text = "FECHA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "FECHA:";
            // 
            // lblFechaRet
            // 
            this.lblFechaRet.AutoSize = true;
            this.lblFechaRet.Location = new System.Drawing.Point(329, 49);
            this.lblFechaRet.Name = "lblFechaRet";
            this.lblFechaRet.Size = new System.Drawing.Size(42, 13);
            this.lblFechaRet.TabIndex = 9;
            this.lblFechaRet.Text = "FECHA";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(278, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "FECHA:";
            // 
            // lblRetencion
            // 
            this.lblRetencion.AutoSize = true;
            this.lblRetencion.Location = new System.Drawing.Point(91, 49);
            this.lblRetencion.Name = "lblRetencion";
            this.lblRetencion.Size = new System.Drawing.Size(70, 13);
            this.lblRetencion.TabIndex = 7;
            this.lblRetencion.Text = "RETENCION";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "RETENCIÓN:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(262, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 80);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(470, 3);
            this.panel2.TabIndex = 11;
            // 
            // frmAsientoContable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 83);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblFechaRet);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblRetencion);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblFechaAsiento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblAsiento);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAsientoContable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asiento Contable";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAsiento;
        private System.Windows.Forms.Label lblFechaAsiento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFechaRet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRetencion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}