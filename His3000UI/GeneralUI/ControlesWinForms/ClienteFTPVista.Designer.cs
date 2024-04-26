namespace GeneralApp.ControlesWinForms
{
    partial class ClienteFTPVista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClienteFTPVista));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSubir = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.dgvArchivos = new System.Windows.Forms.DataGridView();
            this.btnMostrarPdf = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchivos)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSubir,
            this.btnEliminar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(700, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSubir
            // 
            this.btnSubir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSubir.Image = ((System.Drawing.Image)(resources.GetObject("btnSubir.Image")));
            this.btnSubir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(23, 22);
            this.btnSubir.Text = "toolStripButton1";
            this.btnSubir.ToolTipText = "Añadir";
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(23, 22);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dgvArchivos
            // 
            this.dgvArchivos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvArchivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArchivos.Location = new System.Drawing.Point(12, 41);
            this.dgvArchivos.Name = "dgvArchivos";
            this.dgvArchivos.Size = new System.Drawing.Size(676, 222);
            this.dgvArchivos.TabIndex = 2;
            this.dgvArchivos.DoubleClick += new System.EventHandler(this.dgvArchivos_DoubleClick);
            this.dgvArchivos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvArchivos_KeyDown);
            // 
            // btnMostrarPdf
            // 
            this.btnMostrarPdf.Location = new System.Drawing.Point(609, 269);
            this.btnMostrarPdf.Name = "btnMostrarPdf";
            this.btnMostrarPdf.Size = new System.Drawing.Size(79, 26);
            this.btnMostrarPdf.TabIndex = 3;
            this.btnMostrarPdf.Text = "Mostrar PDF";
            this.btnMostrarPdf.UseVisualStyleBackColor = true;
            this.btnMostrarPdf.Click += new System.EventHandler(this.btnMostrarPdf_Click);
            // 
            // ClienteFTPVista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 301);
            this.Controls.Add(this.btnMostrarPdf);
            this.Controls.Add(this.dgvArchivos);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ClienteFTPVista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Explorador de archivos adjuntos";
            this.Load += new System.EventHandler(this.ClienteFTPVista_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchivos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSubir;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.DataGridView dgvArchivos;
        private System.Windows.Forms.Button btnMostrarPdf;
    }
}