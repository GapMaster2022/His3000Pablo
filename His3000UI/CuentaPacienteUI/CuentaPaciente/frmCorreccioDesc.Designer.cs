namespace CuentaPaciente
{
    partial class frmCorreccioDesc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorreccioDesc));
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTTvalor = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.TextBox();
            this.txtTTporcentage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTTcantidad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.maskVal = new System.Windows.Forms.MaskedTextBox();
            this.optVal = new System.Windows.Forms.RadioButton();
            this.optPor = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.maskPor = new System.Windows.Forms.MaskedTextBox();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.txtDescSinIVA = new System.Windows.Forms.TextBox();
            this.txtDescConIVA = new System.Windows.Forms.TextBox();
            this.dgvDatosCuenta = new System.Windows.Forms.DataGridView();
            this.optInd = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosCuenta)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.optInd);
            this.ultraGroupBox2.Controls.Add(this.label6);
            this.ultraGroupBox2.Controls.Add(this.button1);
            this.ultraGroupBox2.Controls.Add(this.txtTTvalor);
            this.ultraGroupBox2.Controls.Add(this.lblTotal);
            this.ultraGroupBox2.Controls.Add(this.txtTTporcentage);
            this.ultraGroupBox2.Controls.Add(this.label5);
            this.ultraGroupBox2.Controls.Add(this.label1);
            this.ultraGroupBox2.Controls.Add(this.txtTTcantidad);
            this.ultraGroupBox2.Controls.Add(this.label4);
            this.ultraGroupBox2.Controls.Add(this.label3);
            this.ultraGroupBox2.Controls.Add(this.maskVal);
            this.ultraGroupBox2.Controls.Add(this.optVal);
            this.ultraGroupBox2.Controls.Add(this.optPor);
            this.ultraGroupBox2.Controls.Add(this.button2);
            this.ultraGroupBox2.Controls.Add(this.label2);
            this.ultraGroupBox2.Controls.Add(this.maskPor);
            this.ultraGroupBox2.Controls.Add(this.menu);
            this.ultraGroupBox2.Controls.Add(this.txtDescSinIVA);
            this.ultraGroupBox2.Controls.Add(this.txtDescConIVA);
            this.ultraGroupBox2.Controls.Add(this.dgvDatosCuenta);
            this.ultraGroupBox2.Location = new System.Drawing.Point(2, 1);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(734, 451);
            this.ultraGroupBox2.TabIndex = 105;
            this.ultraGroupBox2.Text = "Descuentos";
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            this.ultraGroupBox2.Click += new System.EventHandler(this.ultraGroupBox2_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(234, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 122;
            this.button1.Text = "ACEPTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTTvalor
            // 
            this.txtTTvalor.Location = new System.Drawing.Point(438, 428);
            this.txtTTvalor.Name = "txtTTvalor";
            this.txtTTvalor.ReadOnly = true;
            this.txtTTvalor.Size = new System.Drawing.Size(76, 20);
            this.txtTTvalor.TabIndex = 121;
            // 
            // lblTotal
            // 
            this.lblTotal.Location = new System.Drawing.Point(649, 428);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.ReadOnly = true;
            this.lblTotal.Size = new System.Drawing.Size(79, 20);
            this.lblTotal.TabIndex = 120;
            // 
            // txtTTporcentage
            // 
            this.txtTTporcentage.Location = new System.Drawing.Point(591, 427);
            this.txtTTporcentage.Name = "txtTTporcentage";
            this.txtTTporcentage.ReadOnly = true;
            this.txtTTporcentage.Size = new System.Drawing.Size(52, 20);
            this.txtTTporcentage.TabIndex = 119;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(264, 431);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 118;
            this.label5.Text = "Cantidad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(520, 430);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 117;
            this.label1.Text = "Descuento: ";
            // 
            // txtTTcantidad
            // 
            this.txtTTcantidad.Location = new System.Drawing.Point(319, 427);
            this.txtTTcantidad.Name = "txtTTcantidad";
            this.txtTTcantidad.ReadOnly = true;
            this.txtTTcantidad.Size = new System.Drawing.Size(76, 20);
            this.txtTTcantidad.TabIndex = 116;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 431);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 115;
            this.label4.Text = "Valor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(77, 431);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 114;
            this.label3.Text = "TOTALES:";
            // 
            // maskVal
            // 
            this.maskVal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskVal.Location = new System.Drawing.Point(603, 24);
            this.maskVal.Mask = "$ 999999.99";
            this.maskVal.Name = "maskVal";
            this.maskVal.Size = new System.Drawing.Size(54, 13);
            this.maskVal.TabIndex = 113;
            // 
            // optVal
            // 
            this.optVal.AutoSize = true;
            this.optVal.Location = new System.Drawing.Point(583, 22);
            this.optVal.Name = "optVal";
            this.optVal.Size = new System.Drawing.Size(14, 13);
            this.optVal.TabIndex = 112;
            this.optVal.UseVisualStyleBackColor = true;
            // 
            // optPor
            // 
            this.optPor.AutoSize = true;
            this.optPor.Location = new System.Drawing.Point(508, 25);
            this.optPor.Name = "optPor";
            this.optPor.Size = new System.Drawing.Size(14, 13);
            this.optPor.TabIndex = 111;
            this.optPor.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(663, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 22);
            this.button2.TabIndex = 110;
            this.button2.Text = "Aplicar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(396, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 109;
            this.label2.Text = "Descuento general:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // maskPor
            // 
            this.maskPor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskPor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskPor.Location = new System.Drawing.Point(528, 22);
            this.maskPor.Mask = "000.00%";
            this.maskPor.Name = "maskPor";
            this.maskPor.Size = new System.Drawing.Size(45, 13);
            this.maskPor.TabIndex = 108;
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Dock = System.Windows.Forms.DockStyle.None;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripButton2});
            this.menu.Location = new System.Drawing.Point(6, 16);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(728, 29);
            this.menu.TabIndex = 106;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Enabled = false;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(69, 26);
            this.btnNuevo.Text = "Guardar";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(73, 26);
            this.toolStripButton2.Text = "Cancelar";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // txtDescSinIVA
            // 
            this.txtDescSinIVA.Location = new System.Drawing.Point(13, 237);
            this.txtDescSinIVA.Name = "txtDescSinIVA";
            this.txtDescSinIVA.Size = new System.Drawing.Size(146, 20);
            this.txtDescSinIVA.TabIndex = 106;
            this.txtDescSinIVA.Visible = false;
            // 
            // txtDescConIVA
            // 
            this.txtDescConIVA.Location = new System.Drawing.Point(13, 211);
            this.txtDescConIVA.Name = "txtDescConIVA";
            this.txtDescConIVA.Size = new System.Drawing.Size(142, 20);
            this.txtDescConIVA.TabIndex = 105;
            this.txtDescConIVA.Visible = false;
            // 
            // dgvDatosCuenta
            // 
            this.dgvDatosCuenta.AllowUserToAddRows = false;
            this.dgvDatosCuenta.AllowUserToDeleteRows = false;
            this.dgvDatosCuenta.AllowUserToResizeRows = false;
            this.dgvDatosCuenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDatosCuenta.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvDatosCuenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatosCuenta.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvDatosCuenta.Location = new System.Drawing.Point(6, 48);
            this.dgvDatosCuenta.Name = "dgvDatosCuenta";
            this.dgvDatosCuenta.Size = new System.Drawing.Size(725, 373);
            this.dgvDatosCuenta.TabIndex = 104;
            this.dgvDatosCuenta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCuenta_CellLeave);
            this.dgvDatosCuenta.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCuenta_CellContentClick);
            this.dgvDatosCuenta.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCuenta_RowEnter);
            this.dgvDatosCuenta.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCuenta_CellLeave);
            this.dgvDatosCuenta.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCuenta_CellValueChanged);
            this.dgvDatosCuenta.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDatosCuenta_EditingControlShowing);
            this.dgvDatosCuenta.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCuenta_RowEnter);
            this.dgvDatosCuenta.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvDatosCuenta_UserDeletedRow);
            this.dgvDatosCuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDatosCuenta_KeyDown);
            // 
            // optInd
            // 
            this.optInd.AutoSize = true;
            this.optInd.Checked = true;
            this.optInd.Location = new System.Drawing.Point(376, 25);
            this.optInd.Name = "optInd";
            this.optInd.Size = new System.Drawing.Size(14, 13);
            this.optInd.TabIndex = 124;
            this.optInd.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(315, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 123;
            this.label6.Text = "Individual:";
            // 
            // frmCorreccioDesc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 454);
            this.Controls.Add(this.ultraGroupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCorreccioDesc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCorreccioDesc";
            this.Load += new System.EventHandler(this.frmCorreccioDesc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosCuenta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.DataGridView dgvDatosCuenta;
        private System.Windows.Forms.TextBox txtDescSinIVA;
        private System.Windows.Forms.TextBox txtDescConIVA;
        protected System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskPor;
        private System.Windows.Forms.MaskedTextBox maskVal;
        private System.Windows.Forms.RadioButton optVal;
        private System.Windows.Forms.RadioButton optPor;
        private System.Windows.Forms.TextBox lblTotal;
        private System.Windows.Forms.TextBox txtTTporcentage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTTcantidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTTvalor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton optInd;
        private System.Windows.Forms.Label label6;
    }
}