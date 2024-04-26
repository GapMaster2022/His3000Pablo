namespace TarifariosUI
{
    partial class frmPorcentajes
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
            this.dtgPorcentaje = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPorcentaje)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgPorcentaje
            // 
            this.dtgPorcentaje.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgPorcentaje.Location = new System.Drawing.Point(1, 3);
            this.dtgPorcentaje.Name = "dtgPorcentaje";
            this.dtgPorcentaje.Size = new System.Drawing.Size(523, 133);
            this.dtgPorcentaje.TabIndex = 0;
            this.dtgPorcentaje.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgPorcentaje_RowHeaderMouseDoubleClick);
            // 
            // frmPorcentajes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 133);
            this.Controls.Add(this.dtgPorcentaje);
            this.Name = "frmPorcentajes";
            this.Text = "Porcentajes";
            ((System.ComponentModel.ISupportInitialize)(this.dtgPorcentaje)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgPorcentaje;
    }
}