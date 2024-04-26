namespace CuentaPaciente
{
    partial class frmAnticipos
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
            this.dgvAnticiposCliente = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnticiposCliente)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAnticiposCliente
            // 
            this.dgvAnticiposCliente.AllowUserToAddRows = false;
            this.dgvAnticiposCliente.AllowUserToDeleteRows = false;
            this.dgvAnticiposCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnticiposCliente.Location = new System.Drawing.Point(12, 12);
            this.dgvAnticiposCliente.Name = "dgvAnticiposCliente";
            this.dgvAnticiposCliente.ReadOnly = true;
            this.dgvAnticiposCliente.Size = new System.Drawing.Size(485, 336);
            this.dgvAnticiposCliente.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(427, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cerrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmAnticipos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 372);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvAnticiposCliente);
            this.Name = "frmAnticipos";
            this.Text = "Anticipo Clientes";
            this.Load += new System.EventHandler(this.frmAnticipos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnticiposCliente)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAnticiposCliente;
        private System.Windows.Forms.Button button1;
    }
}