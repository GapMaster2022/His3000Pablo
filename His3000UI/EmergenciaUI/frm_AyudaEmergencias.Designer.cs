namespace His.Emergencia
{
    partial class frm_AyudaEmergencias
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.ultraPanel2 = new Infragistics.Win.Misc.UltraPanel();
            this.dataGridViewEmergencias = new System.Windows.Forms.DataGridView();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txt_Paciente = new System.Windows.Forms.Label();
            this.btnActualizar = new Infragistics.Win.Misc.UltraButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_Filtrar = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.ultraPanel2.ClientArea.SuspendLayout();
            this.ultraPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmergencias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraPanel1
            // 
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraPanel2);
            this.ultraPanel1.Location = new System.Drawing.Point(-1, 0);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(507, 423);
            this.ultraPanel1.TabIndex = 0;
            // 
            // ultraPanel2
            // 
            appearance1.BackColor = System.Drawing.Color.WhiteSmoke;
            appearance1.BackColor2 = System.Drawing.Color.LightSteelBlue;
            this.ultraPanel2.Appearance = appearance1;
            // 
            // ultraPanel2.ClientArea
            // 
            this.ultraPanel2.ClientArea.Controls.Add(this.dataGridViewEmergencias);
            this.ultraPanel2.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.ultraPanel2.Location = new System.Drawing.Point(3, 3);
            this.ultraPanel2.Name = "ultraPanel2";
            this.ultraPanel2.Size = new System.Drawing.Size(500, 413);
            this.ultraPanel2.TabIndex = 0;
            // 
            // dataGridViewEmergencias
            // 
            this.dataGridViewEmergencias.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewEmergencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmergencias.Location = new System.Drawing.Point(11, 123);
            this.dataGridViewEmergencias.Name = "dataGridViewEmergencias";
            this.dataGridViewEmergencias.ReadOnly = true;
            this.dataGridViewEmergencias.Size = new System.Drawing.Size(482, 279);
            this.dataGridViewEmergencias.TabIndex = 3;
            this.dataGridViewEmergencias.DoubleClick += new System.EventHandler(this.dataGridViewEmergencias_DoubleClick);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.txt_Paciente);
            this.ultraGroupBox1.Controls.Add(this.btnActualizar);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.cbx_Filtrar);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Location = new System.Drawing.Point(41, 14);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(421, 88);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "Datos de Filtro";
            // 
            // txt_Paciente
            // 
            this.txt_Paciente.AutoSize = true;
            this.txt_Paciente.Location = new System.Drawing.Point(116, 21);
            this.txt_Paciente.Name = "txt_Paciente";
            this.txt_Paciente.Size = new System.Drawing.Size(49, 13);
            this.txt_Paciente.TabIndex = 4;
            this.txt_Paciente.Text = "Nombres";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(327, 39);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(72, 30);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Buscar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Paciente:";
            // 
            // cbx_Filtrar
            // 
            this.cbx_Filtrar.FormattingEnabled = true;
            this.cbx_Filtrar.Items.AddRange(new object[] {
            "10",
            "100"});
            this.cbx_Filtrar.Location = new System.Drawing.Point(116, 44);
            this.cbx_Filtrar.Name = "cbx_Filtrar";
            this.cbx_Filtrar.Size = new System.Drawing.Size(121, 21);
            this.cbx_Filtrar.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtrar :";
            // 
            // frm_AyudaEmergencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 423);
            this.Controls.Add(this.ultraPanel1);
            this.Name = "frm_AyudaEmergencias";
            this.Text = "Emergencias";
            this.Load += new System.EventHandler(this.frm_AyudaEmergencias_Load);
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            this.ultraPanel2.ClientArea.ResumeLayout(false);
            this.ultraPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmergencias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraPanel ultraPanel2;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbx_Filtrar;
        private Infragistics.Win.Misc.UltraButton btnActualizar;
        private System.Windows.Forms.DataGridView dataGridViewEmergencias;
        private System.Windows.Forms.Label txt_Paciente;
    }
}