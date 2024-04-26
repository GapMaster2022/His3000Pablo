namespace His.Dietetica
{
    partial class frm_QuirofanoNuevo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_QuirofanoNuevo));
            this.btnproducto = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnanestesia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnproducto
            // 
            this.btnproducto.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnproducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnproducto.Image = ((System.Drawing.Image)(resources.GetObject("btnproducto.Image")));
            this.btnproducto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnproducto.Location = new System.Drawing.Point(32, 84);
            this.btnproducto.Name = "btnproducto";
            this.btnproducto.Size = new System.Drawing.Size(115, 69);
            this.btnproducto.TabIndex = 0;
            this.btnproducto.Text = "Nuevo Producto";
            this.btnproducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnproducto.UseVisualStyleBackColor = false;
            this.btnproducto.Click += new System.EventHandler(this.btnproducto_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione una opción";
            // 
            // btnanestesia
            // 
            this.btnanestesia.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnanestesia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnanestesia.Image = ((System.Drawing.Image)(resources.GetObject("btnanestesia.Image")));
            this.btnanestesia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnanestesia.Location = new System.Drawing.Point(176, 84);
            this.btnanestesia.Name = "btnanestesia";
            this.btnanestesia.Size = new System.Drawing.Size(120, 69);
            this.btnanestesia.TabIndex = 2;
            this.btnanestesia.Text = "Nueva Anestesia";
            this.btnanestesia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnanestesia.UseVisualStyleBackColor = false;
            this.btnanestesia.Click += new System.EventHandler(this.btnanestesia_Click);
            // 
            // frm_QuirofanoNuevo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 185);
            this.Controls.Add(this.btnanestesia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnproducto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_QuirofanoNuevo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar - Nuevo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnproducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnanestesia;
    }
}