namespace His.Formulario
{
    partial class frm_AyudaKardex
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
            this.dtgAyudaKardex = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAyudaKardex)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgAyudaKardex
            // 
            this.dtgAyudaKardex.AllowUserToAddRows = false;
            this.dtgAyudaKardex.AllowUserToDeleteRows = false;
            this.dtgAyudaKardex.AllowUserToOrderColumns = true;
            this.dtgAyudaKardex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgAyudaKardex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAyudaKardex.Location = new System.Drawing.Point(4, 30);
            this.dtgAyudaKardex.Name = "dtgAyudaKardex";
            this.dtgAyudaKardex.Size = new System.Drawing.Size(561, 319);
            this.dtgAyudaKardex.TabIndex = 0;
            this.dtgAyudaKardex.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgAyudaKardex_CellDoubleClick);
            this.dtgAyudaKardex.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgAyudaKardex_CellEnter);
            this.dtgAyudaKardex.CurrentCellChanged += new System.EventHandler(this.dtgAyudaKardex_CurrentCellChanged);
            this.dtgAyudaKardex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgAyudaKardex_KeyDown);
            this.dtgAyudaKardex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dtgAyudaKardex_KeyPress);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "DETALLE";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 400;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox1.Location = new System.Drawing.Point(55, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(510, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Buscar";
            // 
            // frm_AyudaKardex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dtgAyudaKardex);
            this.Name = "frm_AyudaKardex";
            this.ShowIcon = false;
            this.Text = "Medicamentos";
            this.Load += new System.EventHandler(this.frm_AyudaKardex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAyudaKardex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgAyudaKardex;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}