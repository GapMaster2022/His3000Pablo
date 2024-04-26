namespace CuentaPaciente
{
    partial class frmRecuperaClienteSIC
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
            this.btnBurcar = new System.Windows.Forms.Button();
            this.txt_Cliente = new System.Windows.Forms.TextBox();
            this.dgvClienteSic = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClienteSic)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBurcar
            // 
            this.btnBurcar.Location = new System.Drawing.Point(370, 43);
            this.btnBurcar.Name = "btnBurcar";
            this.btnBurcar.Size = new System.Drawing.Size(75, 23);
            this.btnBurcar.TabIndex = 0;
            this.btnBurcar.Text = "BUSCAR";
            this.btnBurcar.UseVisualStyleBackColor = true;
            this.btnBurcar.Click += new System.EventHandler(this.btnBurcar_Click);
            // 
            // txt_Cliente
            // 
            this.txt_Cliente.Location = new System.Drawing.Point(66, 43);
            this.txt_Cliente.Name = "txt_Cliente";
            this.txt_Cliente.Size = new System.Drawing.Size(298, 20);
            this.txt_Cliente.TabIndex = 1;
            // 
            // dgvClienteSic
            // 
            this.dgvClienteSic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClienteSic.Location = new System.Drawing.Point(12, 69);
            this.dgvClienteSic.Name = "dgvClienteSic";
            this.dgvClienteSic.Size = new System.Drawing.Size(471, 180);
            this.dgvClienteSic.TabIndex = 2;
            this.dgvClienteSic.DoubleClick += new System.EventHandler(this.dgvClienteSic_DoubleClick);
            this.dgvClienteSic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvClienteSic_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "NOMBRE / RAZÓN SOCIAL";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "IDENTIFICADOR";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "RAZÓN SOCIAL";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // frmRecuperaClienteSIC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvClienteSic);
            this.Controls.Add(this.txt_Cliente);
            this.Controls.Add(this.btnBurcar);
            this.Name = "frmRecuperaClienteSIC";
            this.Text = "frmRecuperaClienteSIC";
            ((System.ComponentModel.ISupportInitialize)(this.dgvClienteSic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBurcar;
        private System.Windows.Forms.TextBox txt_Cliente;
        private System.Windows.Forms.DataGridView dgvClienteSic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}