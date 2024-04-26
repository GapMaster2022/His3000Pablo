namespace His.Honorarios
{
    partial class frm_ParametrosHonorarios
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerHasta = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDesde = new System.Windows.Forms.DateTimePicker();
            this.frm_ParametrosHonorarios_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.panel1.SuspendLayout();
            this.frm_ParametrosHonorarios_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_ParametrosHonorarios_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(66, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(177, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 26);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePickerHasta);
            this.panel1.Controls.Add(this.dateTimePickerDesde);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 93);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Hasta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Desde:";
            // 
            // dateTimePickerHasta
            // 
            this.dateTimePickerHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerHasta.Location = new System.Drawing.Point(235, 18);
            this.dateTimePickerHasta.Name = "dateTimePickerHasta";
            this.dateTimePickerHasta.Size = new System.Drawing.Size(89, 20);
            this.dateTimePickerHasta.TabIndex = 3;
            this.dateTimePickerHasta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePickerHasta_KeyPress);
            // 
            // dateTimePickerDesde
            // 
            this.dateTimePickerDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDesde.Location = new System.Drawing.Point(74, 19);
            this.dateTimePickerDesde.Name = "dateTimePickerDesde";
            this.dateTimePickerDesde.Size = new System.Drawing.Size(89, 20);
            this.dateTimePickerDesde.TabIndex = 2;
            this.dateTimePickerDesde.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dateTimePickerDesde_KeyPress);
            // 
            // frm_ParametrosHonorarios_Fill_Panel
            // 
            // 
            // frm_ParametrosHonorarios_Fill_Panel.ClientArea
            // 
            this.frm_ParametrosHonorarios_Fill_Panel.ClientArea.Controls.Add(this.panel1);
            this.frm_ParametrosHonorarios_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_ParametrosHonorarios_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_ParametrosHonorarios_Fill_Panel.Location = new System.Drawing.Point(0, 0);
            this.frm_ParametrosHonorarios_Fill_Panel.Name = "frm_ParametrosHonorarios_Fill_Panel";
            this.frm_ParametrosHonorarios_Fill_Panel.Size = new System.Drawing.Size(351, 93);
            this.frm_ParametrosHonorarios_Fill_Panel.TabIndex = 0;
            // 
            // frm_ParametrosHonorarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(351, 93);
            this.Controls.Add(this.frm_ParametrosHonorarios_Fill_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ParametrosHonorarios";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informe de Honorarios";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.frm_ParametrosHonorarios_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_ParametrosHonorarios_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerHasta;
        private System.Windows.Forms.DateTimePicker dateTimePickerDesde;
        private Infragistics.Win.Misc.UltraPanel frm_ParametrosHonorarios_Fill_Panel;

    }
}
