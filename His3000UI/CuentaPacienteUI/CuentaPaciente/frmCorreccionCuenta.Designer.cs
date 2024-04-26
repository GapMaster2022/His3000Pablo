namespace CuentaPaciente
{
    partial class frmCorreccionCuenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorreccionCuenta));
            this.label34 = new System.Windows.Forms.Label();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnDevolucion = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.cmb_Rubros = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRubros = new System.Windows.Forms.Label();
            this.cmb_Areas = new System.Windows.Forms.ComboBox();
            this.dgvDatosCuenta = new System.Windows.Forms.DataGridView();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosCuenta)).BeginInit();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Location = new System.Drawing.Point(246, 38);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(54, 13);
            this.label34.TabIndex = 95;
            this.label34.Text = "SubArea :";
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.label2);
            this.ultraGroupBox2.Controls.Add(this.btnDevolucion);
            this.ultraGroupBox2.Controls.Add(this.lblTotal);
            this.ultraGroupBox2.Controls.Add(this.cmb_Rubros);
            this.ultraGroupBox2.Controls.Add(this.label1);
            this.ultraGroupBox2.Controls.Add(this.label34);
            this.ultraGroupBox2.Controls.Add(this.lblRubros);
            this.ultraGroupBox2.Controls.Add(this.cmb_Areas);
            this.ultraGroupBox2.Location = new System.Drawing.Point(2, 13);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(1020, 581);
            this.ultraGroupBox2.TabIndex = 101;
            this.ultraGroupBox2.Text = "Productos Solicitados";
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // btnDevolucion
            // 
            this.btnDevolucion.Enabled = false;
            this.btnDevolucion.Location = new System.Drawing.Point(11, 533);
            this.btnDevolucion.Name = "btnDevolucion";
            this.btnDevolucion.Size = new System.Drawing.Size(107, 23);
            this.btnDevolucion.TabIndex = 100;
            this.btnDevolucion.Text = "Devolucion";
            this.btnDevolucion.UseVisualStyleBackColor = true;
            this.btnDevolucion.Visible = false;
            this.btnDevolucion.Click += new System.EventHandler(this.btnDevolucion_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(860, 530);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(60, 24);
            this.lblTotal.TabIndex = 99;
            this.lblTotal.Text = "label2";
            // 
            // cmb_Rubros
            // 
            this.cmb_Rubros.FormattingEnabled = true;
            this.cmb_Rubros.Location = new System.Drawing.Point(300, 31);
            this.cmb_Rubros.Name = "cmb_Rubros";
            this.cmb_Rubros.Size = new System.Drawing.Size(192, 21);
            this.cmb_Rubros.TabIndex = 96;
            this.cmb_Rubros.SelectedIndexChanged += new System.EventHandler(this.cmb_Rubros_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(750, 530);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 98;
            this.label1.Text = "TOTAL :";
            // 
            // lblRubros
            // 
            this.lblRubros.AutoSize = true;
            this.lblRubros.BackColor = System.Drawing.Color.Transparent;
            this.lblRubros.Location = new System.Drawing.Point(8, 38);
            this.lblRubros.Name = "lblRubros";
            this.lblRubros.Size = new System.Drawing.Size(35, 13);
            this.lblRubros.TabIndex = 93;
            this.lblRubros.Text = "Área :";
            // 
            // cmb_Areas
            // 
            this.cmb_Areas.FormattingEnabled = true;
            this.cmb_Areas.Location = new System.Drawing.Point(44, 31);
            this.cmb_Areas.Name = "cmb_Areas";
            this.cmb_Areas.Size = new System.Drawing.Size(196, 21);
            this.cmb_Areas.TabIndex = 94;
            this.cmb_Areas.SelectedIndexChanged += new System.EventHandler(this.cmb_Areas_SelectedIndexChanged);
            // 
            // dgvDatosCuenta
            // 
            this.dgvDatosCuenta.AllowUserToAddRows = false;
            this.dgvDatosCuenta.AllowUserToDeleteRows = false;
            this.dgvDatosCuenta.AllowUserToResizeRows = false;
            this.dgvDatosCuenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDatosCuenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatosCuenta.Location = new System.Drawing.Point(13, 70);
            this.dgvDatosCuenta.Name = "dgvDatosCuenta";
            this.dgvDatosCuenta.Size = new System.Drawing.Size(1001, 462);
            this.dgvDatosCuenta.TabIndex = 97;
            this.dgvDatosCuenta.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCuenta_CellEnter);
            this.dgvDatosCuenta.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCuenta_CellValueChanged);
            this.dgvDatosCuenta.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCuenta_RowEnter);
            this.dgvDatosCuenta.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvDatosCuenta_UserDeletedRow);
            this.dgvDatosCuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDatosCuenta_KeyDown);
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnEliminar,
            this.toolStripButton2});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1025, 36);
            this.menu.TabIndex = 103;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(69, 33);
            this.btnNuevo.Text = "Guardar";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(87, 33);
            this.btnEliminar.Text = "Devolución";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(49, 33);
            this.toolStripButton2.Text = "Salir";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(71, 535);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(623, 23);
            this.label2.TabIndex = 101;
            this.label2.Text = "Cambios en  medicamentos e insumos realizar proceso de devolución";
            // 
            // frmCorreccionCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1025, 595);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.dgvDatosCuenta);
            this.Controls.Add(this.ultraGroupBox2);
            this.Name = "frmCorreccionCuenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Correccion Cuentas";
            this.Load += new System.EventHandler(this.frmCorreccionCuenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosCuenta)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label34;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.Button btnDevolucion;
        private System.Windows.Forms.ComboBox cmb_Rubros;
        private System.Windows.Forms.Label lblRubros;
        private System.Windows.Forms.ComboBox cmb_Areas;
        private System.Windows.Forms.DataGridView dgvDatosCuenta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotal;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label label2;
    }
}