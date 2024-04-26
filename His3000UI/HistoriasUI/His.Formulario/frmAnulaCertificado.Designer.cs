namespace His.Formulario
{
    partial class frmAnulaCertificado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnulaCertificado));
            this.txtmedico = new System.Windows.Forms.TextBox();
            this.btnBuscaMedico = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancela = new System.Windows.Forms.Button();
            this.btnGuarda = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtmedico
            // 
            this.txtmedico.Location = new System.Drawing.Point(26, 44);
            this.txtmedico.Name = "txtmedico";
            this.txtmedico.ReadOnly = true;
            this.txtmedico.Size = new System.Drawing.Size(306, 20);
            this.txtmedico.TabIndex = 0;
            // 
            // btnBuscaMedico
            // 
            this.btnBuscaMedico.Location = new System.Drawing.Point(26, 22);
            this.btnBuscaMedico.Name = "btnBuscaMedico";
            this.btnBuscaMedico.Size = new System.Drawing.Size(36, 23);
            this.btnBuscaMedico.TabIndex = 1;
            this.btnBuscaMedico.Text = "F1";
            this.btnBuscaMedico.UseVisualStyleBackColor = true;
            this.btnBuscaMedico.Visible = false;
            this.btnBuscaMedico.Click += new System.EventHandler(this.btnBuscaMedico_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "MÉDICO SOLICITANTE";
            // 
            // txtMotivo
            // 
            this.txtMotivo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMotivo.Location = new System.Drawing.Point(26, 132);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(306, 69);
            this.txtMotivo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "MOTIVO";
            // 
            // btnCancela
            // 
            this.btnCancela.Image = ((System.Drawing.Image)(resources.GetObject("btnCancela.Image")));
            this.btnCancela.Location = new System.Drawing.Point(283, 222);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(49, 51);
            this.btnCancela.TabIndex = 5;
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            // 
            // btnGuarda
            // 
            this.btnGuarda.Image = ((System.Drawing.Image)(resources.GetObject("btnGuarda.Image")));
            this.btnGuarda.Location = new System.Drawing.Point(228, 222);
            this.btnGuarda.Name = "btnGuarda";
            this.btnGuarda.Size = new System.Drawing.Size(49, 51);
            this.btnGuarda.TabIndex = 6;
            this.btnGuarda.UseVisualStyleBackColor = true;
            this.btnGuarda.Click += new System.EventHandler(this.btnGuarda_Click);
            // 
            // frmAnulaCertificado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 285);
            this.Controls.Add(this.btnGuarda);
            this.Controls.Add(this.btnCancela);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBuscaMedico);
            this.Controls.Add(this.txtmedico);
            this.Name = "frmAnulaCertificado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inhabilita ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtmedico;
        private System.Windows.Forms.Button btnBuscaMedico;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Button btnGuarda;
    }
}