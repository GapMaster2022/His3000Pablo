namespace His.Pedidos
{
    partial class frmSeleccionArea
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
            this.lsbAreasAsignadas = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lsbAreasAsignadas
            // 
            this.lsbAreasAsignadas.FormattingEnabled = true;
            this.lsbAreasAsignadas.Location = new System.Drawing.Point(4, 5);
            this.lsbAreasAsignadas.Name = "lsbAreasAsignadas";
            this.lsbAreasAsignadas.Size = new System.Drawing.Size(285, 251);
            this.lsbAreasAsignadas.TabIndex = 0;
            this.lsbAreasAsignadas.DoubleClick += new System.EventHandler(this.lsbAreasAsignadas_DoubleClick);
            this.lsbAreasAsignadas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lsbAreasAsignadas_KeyPress);
            // 
            // frmSeleccionArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 261);
            this.Controls.Add(this.lsbAreasAsignadas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSeleccionArea";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Areas Asignadas";
            this.Load += new System.EventHandler(this.frmSeleccionArea_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lsbAreasAsignadas;
    }
}