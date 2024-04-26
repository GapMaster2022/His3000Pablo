namespace His.ConsultaExterna
{
    partial class frm_ConsultaAgenda
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
            this.P_Superior = new System.Windows.Forms.Panel();
            this.P_Izquierda = new System.Windows.Forms.Panel();
            this.P_Derecha = new System.Windows.Forms.Panel();
            this.P_Central = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // P_Superior
            // 
            this.P_Superior.Dock = System.Windows.Forms.DockStyle.Top;
            this.P_Superior.Location = new System.Drawing.Point(0, 0);
            this.P_Superior.Name = "P_Superior";
            this.P_Superior.Size = new System.Drawing.Size(771, 100);
            this.P_Superior.TabIndex = 0;
            // 
            // P_Izquierda
            // 
            this.P_Izquierda.Dock = System.Windows.Forms.DockStyle.Left;
            this.P_Izquierda.Location = new System.Drawing.Point(0, 100);
            this.P_Izquierda.Name = "P_Izquierda";
            this.P_Izquierda.Size = new System.Drawing.Size(128, 337);
            this.P_Izquierda.TabIndex = 1;
            // 
            // P_Derecha
            // 
            this.P_Derecha.Dock = System.Windows.Forms.DockStyle.Right;
            this.P_Derecha.Location = new System.Drawing.Point(643, 100);
            this.P_Derecha.Name = "P_Derecha";
            this.P_Derecha.Size = new System.Drawing.Size(128, 337);
            this.P_Derecha.TabIndex = 2;
            // 
            // P_Central
            // 
            this.P_Central.Dock = System.Windows.Forms.DockStyle.Top;
            this.P_Central.Location = new System.Drawing.Point(128, 100);
            this.P_Central.Name = "P_Central";
            this.P_Central.Size = new System.Drawing.Size(515, 214);
            this.P_Central.TabIndex = 3;
            // 
            // frm_ConsultaAgenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 437);
            this.Controls.Add(this.P_Central);
            this.Controls.Add(this.P_Derecha);
            this.Controls.Add(this.P_Izquierda);
            this.Controls.Add(this.P_Superior);
            this.Name = "frm_ConsultaAgenda";
            this.Text = "frm_ConsultaAgenda";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel P_Superior;
        private System.Windows.Forms.Panel P_Izquierda;
        private System.Windows.Forms.Panel P_Derecha;
        private System.Windows.Forms.Panel P_Central;
    }
}