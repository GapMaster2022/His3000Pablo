namespace CuentaPaciente
{
    partial class frmActualizaFechas
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
            this.cmb_Rubros = new System.Windows.Forms.ComboBox();
            this.cmb_Areas = new System.Windows.Forms.ComboBox();
            this.lblRubros = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.dtpFechaPedido = new System.Windows.Forms.DateTimePicker();
            this.btnAnadir = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmb_Rubros
            // 
            this.cmb_Rubros.FormattingEnabled = true;
            this.cmb_Rubros.Location = new System.Drawing.Point(92, 39);
            this.cmb_Rubros.Name = "cmb_Rubros";
            this.cmb_Rubros.Size = new System.Drawing.Size(199, 21);
            this.cmb_Rubros.TabIndex = 106;
            // 
            // cmb_Areas
            // 
            this.cmb_Areas.FormattingEnabled = true;
            this.cmb_Areas.Location = new System.Drawing.Point(92, 7);
            this.cmb_Areas.Name = "cmb_Areas";
            this.cmb_Areas.Size = new System.Drawing.Size(199, 21);
            this.cmb_Areas.TabIndex = 104;
            this.cmb_Areas.SelectedIndexChanged += new System.EventHandler(this.cmb_Areas_SelectedIndexChanged);
            // 
            // lblRubros
            // 
            this.lblRubros.AutoSize = true;
            this.lblRubros.BackColor = System.Drawing.Color.Transparent;
            this.lblRubros.Location = new System.Drawing.Point(48, 15);
            this.lblRubros.Name = "lblRubros";
            this.lblRubros.Size = new System.Drawing.Size(35, 13);
            this.lblRubros.TabIndex = 103;
            this.lblRubros.Text = "Área :";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Location = new System.Drawing.Point(29, 42);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(54, 13);
            this.label34.TabIndex = 105;
            this.label34.Text = "SubArea :";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Location = new System.Drawing.Point(7, 68);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(79, 13);
            this.label39.TabIndex = 119;
            this.label39.Text = "Fecha Pedido :";
            // 
            // dtpFechaPedido
            // 
            this.dtpFechaPedido.CustomFormat = "MM-dd-yyyy HH:mm";
            this.dtpFechaPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaPedido.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPedido.Location = new System.Drawing.Point(92, 66);
            this.dtpFechaPedido.MaxDate = new System.DateTime(9000, 12, 31, 0, 0, 0, 0);
            this.dtpFechaPedido.Name = "dtpFechaPedido";
            this.dtpFechaPedido.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaPedido.TabIndex = 118;
            this.dtpFechaPedido.Value = new System.DateTime(2012, 2, 28, 0, 0, 0, 0);
            // 
            // btnAnadir
            // 
            this.btnAnadir.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAnadir.Location = new System.Drawing.Point(161, 107);
            this.btnAnadir.Name = "btnAnadir";
            this.btnAnadir.Size = new System.Drawing.Size(62, 23);
            this.btnAnadir.TabIndex = 120;
            this.btnAnadir.Text = "Aceptar";
            this.btnAnadir.UseVisualStyleBackColor = false;
            this.btnAnadir.Click += new System.EventHandler(this.btnAnadir_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.Location = new System.Drawing.Point(229, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 23);
            this.button1.TabIndex = 121;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmActualizaFechas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 135);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAnadir);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.dtpFechaPedido);
            this.Controls.Add(this.cmb_Rubros);
            this.Controls.Add(this.cmb_Areas);
            this.Controls.Add(this.lblRubros);
            this.Controls.Add(this.label34);
            this.Name = "frmActualizaFechas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualizacion Fechas";
            this.Load += new System.EventHandler(this.frmActualizaFechas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_Rubros;
        private System.Windows.Forms.ComboBox cmb_Areas;
        private System.Windows.Forms.Label lblRubros;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.DateTimePicker dtpFechaPedido;
        private System.Windows.Forms.Button btnAnadir;
        private System.Windows.Forms.Button button1;
    }
}