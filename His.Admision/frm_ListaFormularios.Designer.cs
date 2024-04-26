namespace His.Admision
{
    partial class frm_ListaFormularios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grid = new System.Windows.Forms.DataGridView();
            this.btnCrear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkNinguno = new System.Windows.Forms.RadioButton();
            this.chk_select = new System.Windows.Forms.RadioButton();
            this.estado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.imprimir = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.estado,
            this.imprimir});
            this.grid.Location = new System.Drawing.Point(23, 28);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(475, 391);
            this.grid.TabIndex = 1;
            this.grid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grid_CellPainting);
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellContentClick);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(437, 434);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(79, 26);
            this.btnCrear.TabIndex = 2;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkNinguno);
            this.groupBox1.Controls.Add(this.chk_select);
            this.groupBox1.Location = new System.Drawing.Point(23, 428);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 32);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // chkNinguno
            // 
            this.chkNinguno.AutoSize = true;
            this.chkNinguno.Location = new System.Drawing.Point(129, 9);
            this.chkNinguno.Name = "chkNinguno";
            this.chkNinguno.Size = new System.Drawing.Size(65, 17);
            this.chkNinguno.TabIndex = 7;
            this.chkNinguno.TabStop = true;
            this.chkNinguno.Text = "Ninguno";
            this.chkNinguno.UseVisualStyleBackColor = true;
            this.chkNinguno.CheckedChanged += new System.EventHandler(this.chkNinguno_CheckedChanged);
            // 
            // chk_select
            // 
            this.chk_select.AutoSize = true;
            this.chk_select.Location = new System.Drawing.Point(6, 9);
            this.chk_select.Name = "chk_select";
            this.chk_select.Size = new System.Drawing.Size(105, 17);
            this.chk_select.TabIndex = 7;
            this.chk_select.TabStop = true;
            this.chk_select.Text = "Seleccionar todo";
            this.chk_select.UseVisualStyleBackColor = true;
            this.chk_select.CheckedChanged += new System.EventHandler(this.chk_select_CheckedChanged);
            // 
            // estado
            // 
            this.estado.HeaderText = "";
            this.estado.IndeterminateValue = "False";
            this.estado.Name = "estado";
            this.estado.Width = 30;
            // 
            // imprimir
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.imprimir.DefaultCellStyle = dataGridViewCellStyle1;
            this.imprimir.HeaderText = "";
            this.imprimir.Name = "imprimir";
            this.imprimir.Visible = false;
            this.imprimir.Width = 20;
            // 
            // frm_ListaFormularios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 473);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.grid);
            this.MaximumSize = new System.Drawing.Size(550, 500);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "frm_ListaFormularios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_ListaFormularios";
            this.Load += new System.EventHandler(this.frm_ListaFormularios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton chk_select;
        private System.Windows.Forms.RadioButton chkNinguno;
        private System.Windows.Forms.DataGridViewCheckBoxColumn estado;
        private System.Windows.Forms.DataGridViewButtonColumn imprimir;
    }
}