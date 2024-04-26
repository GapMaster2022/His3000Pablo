namespace CuentaPaciente
{
    partial class frmFormasPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormasPago));
            this.gridFormasPago = new System.Windows.Forms.DataGridView();
            this.txt_TotalPago = new System.Windows.Forms.Label();
            this.ultraTabControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.txt_SubtotalPago = new System.Windows.Forms.Label();
            this.txt_DescuentoPago = new System.Windows.Forms.Label();
            this.txt_CIvaPago = new System.Windows.Forms.Label();
            this.txt_SIvaPago = new System.Windows.Forms.Label();
            this.txt_TotalCSPago = new System.Windows.Forms.Label();
            this.txt_IVAPago = new System.Windows.Forms.Label();
            this.txt_TotalP = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.btn_Guardar = new System.Windows.Forms.ToolStripButton();
            this.btn_Salir = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridFormasPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl2)).BeginInit();
            this.toolStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridFormasPago
            // 
            this.gridFormasPago.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gridFormasPago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFormasPago.Location = new System.Drawing.Point(12, 59);
            this.gridFormasPago.Name = "gridFormasPago";
            this.gridFormasPago.Size = new System.Drawing.Size(785, 174);
            this.gridFormasPago.TabIndex = 0;
            this.gridFormasPago.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridFormasPago_KeyDown);
            // 
            // txt_TotalPago
            // 
            this.txt_TotalPago.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txt_TotalPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TotalPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TotalPago.Location = new System.Drawing.Point(780, 354);
            this.txt_TotalPago.MaximumSize = new System.Drawing.Size(230, 50);
            this.txt_TotalPago.MinimumSize = new System.Drawing.Size(10, 10);
            this.txt_TotalPago.Name = "txt_TotalPago";
            this.txt_TotalPago.Padding = new System.Windows.Forms.Padding(2);
            this.txt_TotalPago.Size = new System.Drawing.Size(100, 32);
            this.txt_TotalPago.TabIndex = 94;
            this.txt_TotalPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ultraTabControl2
            // 
            this.ultraTabControl2.Location = new System.Drawing.Point(0, 0);
            this.ultraTabControl2.Name = "ultraTabControl2";
            this.ultraTabControl2.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl2.Size = new System.Drawing.Size(200, 100);
            this.ultraTabControl2.TabIndex = 0;
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(2, 21);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(196, 77);
            // 
            // txt_SubtotalPago
            // 
            this.txt_SubtotalPago.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_SubtotalPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_SubtotalPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SubtotalPago.Location = new System.Drawing.Point(696, 236);
            this.txt_SubtotalPago.MaximumSize = new System.Drawing.Size(230, 50);
            this.txt_SubtotalPago.MinimumSize = new System.Drawing.Size(10, 10);
            this.txt_SubtotalPago.Name = "txt_SubtotalPago";
            this.txt_SubtotalPago.Padding = new System.Windows.Forms.Padding(2);
            this.txt_SubtotalPago.Size = new System.Drawing.Size(100, 23);
            this.txt_SubtotalPago.TabIndex = 89;
            this.txt_SubtotalPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_DescuentoPago
            // 
            this.txt_DescuentoPago.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_DescuentoPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_DescuentoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DescuentoPago.Location = new System.Drawing.Point(696, 259);
            this.txt_DescuentoPago.MaximumSize = new System.Drawing.Size(230, 50);
            this.txt_DescuentoPago.MinimumSize = new System.Drawing.Size(10, 10);
            this.txt_DescuentoPago.Name = "txt_DescuentoPago";
            this.txt_DescuentoPago.Padding = new System.Windows.Forms.Padding(2);
            this.txt_DescuentoPago.Size = new System.Drawing.Size(100, 23);
            this.txt_DescuentoPago.TabIndex = 90;
            this.txt_DescuentoPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_CIvaPago
            // 
            this.txt_CIvaPago.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_CIvaPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_CIvaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CIvaPago.Location = new System.Drawing.Point(696, 282);
            this.txt_CIvaPago.MaximumSize = new System.Drawing.Size(230, 50);
            this.txt_CIvaPago.MinimumSize = new System.Drawing.Size(10, 10);
            this.txt_CIvaPago.Name = "txt_CIvaPago";
            this.txt_CIvaPago.Padding = new System.Windows.Forms.Padding(2);
            this.txt_CIvaPago.Size = new System.Drawing.Size(100, 23);
            this.txt_CIvaPago.TabIndex = 91;
            this.txt_CIvaPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_SIvaPago
            // 
            this.txt_SIvaPago.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_SIvaPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_SIvaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SIvaPago.Location = new System.Drawing.Point(696, 305);
            this.txt_SIvaPago.MaximumSize = new System.Drawing.Size(230, 50);
            this.txt_SIvaPago.MinimumSize = new System.Drawing.Size(10, 10);
            this.txt_SIvaPago.Name = "txt_SIvaPago";
            this.txt_SIvaPago.Padding = new System.Windows.Forms.Padding(2);
            this.txt_SIvaPago.Size = new System.Drawing.Size(100, 23);
            this.txt_SIvaPago.TabIndex = 92;
            this.txt_SIvaPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_TotalCSPago
            // 
            this.txt_TotalCSPago.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_TotalCSPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TotalCSPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TotalCSPago.Location = new System.Drawing.Point(696, 328);
            this.txt_TotalCSPago.MaximumSize = new System.Drawing.Size(230, 50);
            this.txt_TotalCSPago.MinimumSize = new System.Drawing.Size(10, 10);
            this.txt_TotalCSPago.Name = "txt_TotalCSPago";
            this.txt_TotalCSPago.Padding = new System.Windows.Forms.Padding(2);
            this.txt_TotalCSPago.Size = new System.Drawing.Size(100, 23);
            this.txt_TotalCSPago.TabIndex = 93;
            this.txt_TotalCSPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_IVAPago
            // 
            this.txt_IVAPago.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_IVAPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_IVAPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IVAPago.Location = new System.Drawing.Point(696, 351);
            this.txt_IVAPago.MaximumSize = new System.Drawing.Size(230, 50);
            this.txt_IVAPago.MinimumSize = new System.Drawing.Size(10, 10);
            this.txt_IVAPago.Name = "txt_IVAPago";
            this.txt_IVAPago.Padding = new System.Windows.Forms.Padding(2);
            this.txt_IVAPago.Size = new System.Drawing.Size(100, 23);
            this.txt_IVAPago.TabIndex = 94;
            this.txt_IVAPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_TotalP
            // 
            this.txt_TotalP.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txt_TotalP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TotalP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TotalP.Location = new System.Drawing.Point(696, 374);
            this.txt_TotalP.MaximumSize = new System.Drawing.Size(230, 50);
            this.txt_TotalP.MinimumSize = new System.Drawing.Size(10, 10);
            this.txt_TotalP.Name = "txt_TotalP";
            this.txt_TotalP.Padding = new System.Windows.Forms.Padding(2);
            this.txt_TotalP.Size = new System.Drawing.Size(100, 32);
            this.txt_TotalP.TabIndex = 95;
            this.txt_TotalP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Location = new System.Drawing.Point(633, 288);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 13);
            this.label22.TabIndex = 102;
            this.label22.Text = "CON I.V.A.";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Location = new System.Drawing.Point(618, 265);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(74, 13);
            this.label26.TabIndex = 101;
            this.label26.Text = "DESCUENTO";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(632, 380);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(64, 20);
            this.label27.TabIndex = 100;
            this.label27.Text = "TOTAL";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Location = new System.Drawing.Point(660, 357);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(33, 13);
            this.label28.TabIndex = 99;
            this.label28.Text = "I.V.A.";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Location = new System.Drawing.Point(613, 334);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(77, 13);
            this.label29.TabIndex = 98;
            this.label29.Text = "TOTAL CI + SI";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Location = new System.Drawing.Point(639, 311);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(54, 13);
            this.label32.TabIndex = 97;
            this.label32.Text = "SIN I.V.A.";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Location = new System.Drawing.Point(623, 242);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(67, 13);
            this.label33.TabIndex = 96;
            this.label33.Text = "SUB-TOTAL";
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Guardar,
            this.btn_Salir});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(805, 25);
            this.toolStripMenu.TabIndex = 103;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // btn_Guardar
            // 
            this.btn_Guardar.Image = ((System.Drawing.Image)(resources.GetObject("btn_Guardar.Image")));
            this.btn_Guardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Guardar.Name = "btn_Guardar";
            this.btn_Guardar.Size = new System.Drawing.Size(66, 22);
            this.btn_Guardar.Text = "Guardar";
            this.btn_Guardar.Click += new System.EventHandler(this.btn_Guardar_Click);
            // 
            // btn_Salir
            // 
            this.btn_Salir.Image = ((System.Drawing.Image)(resources.GetObject("btn_Salir.Image")));
            this.btn_Salir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(47, 22);
            this.btn_Salir.Text = "Salir";
            // 
            // frmFormasPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 411);
            this.Controls.Add(this.toolStripMenu);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.txt_TotalP);
            this.Controls.Add(this.txt_IVAPago);
            this.Controls.Add(this.txt_TotalCSPago);
            this.Controls.Add(this.txt_SIvaPago);
            this.Controls.Add(this.txt_CIvaPago);
            this.Controls.Add(this.txt_DescuentoPago);
            this.Controls.Add(this.txt_SubtotalPago);
            this.Controls.Add(this.gridFormasPago);
            this.Name = "frmFormasPago";
            this.Text = "Formas de Pago";
            ((System.ComponentModel.ISupportInitialize)(this.gridFormasPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl2)).EndInit();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridFormasPago;
        private System.Windows.Forms.Label txt_TotalPago;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl2;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private System.Windows.Forms.Label txt_SubtotalPago;
        private System.Windows.Forms.Label txt_DescuentoPago;
        private System.Windows.Forms.Label txt_CIvaPago;
        private System.Windows.Forms.Label txt_SIvaPago;
        private System.Windows.Forms.Label txt_TotalCSPago;
        private System.Windows.Forms.Label txt_IVAPago;
        private System.Windows.Forms.Label txt_TotalP;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton btn_Guardar;
        private System.Windows.Forms.ToolStripButton btn_Salir;
    }
}