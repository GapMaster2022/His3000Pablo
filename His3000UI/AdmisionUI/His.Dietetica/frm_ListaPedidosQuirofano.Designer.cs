namespace His.Dietetica
{
    partial class frm_ListaPedidosQuirofano
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
            this.ListaPedidos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ListaPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // ListaPedidos
            // 
            this.ListaPedidos.AllowUserToAddRows = false;
            this.ListaPedidos.AllowUserToDeleteRows = false;
            this.ListaPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ListaPedidos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ListaPedidos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.ListaPedidos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListaPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaPedidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListaPedidos.Location = new System.Drawing.Point(0, 0);
            this.ListaPedidos.Name = "ListaPedidos";
            this.ListaPedidos.ReadOnly = true;
            this.ListaPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListaPedidos.Size = new System.Drawing.Size(593, 225);
            this.ListaPedidos.TabIndex = 0;
            this.ListaPedidos.DoubleClick += new System.EventHandler(this.ListaPedidos_DoubleClick);
            // 
            // frm_ListaPedidosQuirofano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 225);
            this.Controls.Add(this.ListaPedidos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_ListaPedidosQuirofano";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Pedidos";
            this.Load += new System.EventHandler(this.frm_ListaPedidosQuirofano_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListaPedidos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ListaPedidos;
    }
}