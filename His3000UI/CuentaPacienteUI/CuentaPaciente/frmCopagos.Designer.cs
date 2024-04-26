
namespace CuentaPaciente
{
    partial class frmCopagos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCopagos));
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.optInd = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.maskVal = new System.Windows.Forms.MaskedTextBox();
            this.optVal = new System.Windows.Forms.RadioButton();
            this.optPor = new System.Windows.Forms.RadioButton();
            this.btnDescuento = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.maskPor = new System.Windows.Forms.MaskedTextBox();
            this.gridN = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtStotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSiva = new System.Windows.Forms.TextBox();
            this.txtCtotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCiva = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.toolStripButton2,
            this.toolStripButton1});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1692, 36);
            this.menu.TabIndex = 107;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(86, 33);
            this.btnNuevo.Text = "Guardar";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(90, 33);
            this.toolStripButton2.Text = "Cancelar";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 33);
            this.toolStripButton1.Text = "Exportar Cuenta";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // optInd
            // 
            this.optInd.AutoSize = true;
            this.optInd.Checked = true;
            this.optInd.Location = new System.Drawing.Point(595, 14);
            this.optInd.Margin = new System.Windows.Forms.Padding(4);
            this.optInd.Name = "optInd";
            this.optInd.Size = new System.Drawing.Size(17, 16);
            this.optInd.TabIndex = 132;
            this.optInd.TabStop = true;
            this.optInd.UseVisualStyleBackColor = true;
            this.optInd.CheckedChanged += new System.EventHandler(this.optInd_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(463, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 17);
            this.label6.TabIndex = 131;
            this.label6.Text = "Copago Individual:";
            // 
            // maskVal
            // 
            this.maskVal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskVal.Location = new System.Drawing.Point(921, 13);
            this.maskVal.Margin = new System.Windows.Forms.Padding(4);
            this.maskVal.Mask = "$ 999999.99";
            this.maskVal.Name = "maskVal";
            this.maskVal.Size = new System.Drawing.Size(72, 16);
            this.maskVal.TabIndex = 130;
            // 
            // optVal
            // 
            this.optVal.AutoSize = true;
            this.optVal.Location = new System.Drawing.Point(884, 14);
            this.optVal.Margin = new System.Windows.Forms.Padding(4);
            this.optVal.Name = "optVal";
            this.optVal.Size = new System.Drawing.Size(17, 16);
            this.optVal.TabIndex = 129;
            this.optVal.UseVisualStyleBackColor = true;
            this.optVal.CheckedChanged += new System.EventHandler(this.optVal_CheckedChanged);
            // 
            // optPor
            // 
            this.optPor.AutoSize = true;
            this.optPor.Location = new System.Drawing.Point(771, 14);
            this.optPor.Margin = new System.Windows.Forms.Padding(4);
            this.optPor.Name = "optPor";
            this.optPor.Size = new System.Drawing.Size(17, 16);
            this.optPor.TabIndex = 128;
            this.optPor.UseVisualStyleBackColor = true;
            this.optPor.CheckedChanged += new System.EventHandler(this.optPor_CheckedChanged);
            // 
            // btnDescuento
            // 
            this.btnDescuento.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDescuento.Location = new System.Drawing.Point(1032, 7);
            this.btnDescuento.Margin = new System.Windows.Forms.Padding(4);
            this.btnDescuento.Name = "btnDescuento";
            this.btnDescuento.Size = new System.Drawing.Size(81, 27);
            this.btnDescuento.TabIndex = 127;
            this.btnDescuento.Text = "Aplicar";
            this.btnDescuento.UseVisualStyleBackColor = true;
            this.btnDescuento.Click += new System.EventHandler(this.btnDescuento_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(639, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 126;
            this.label2.Text = "Copago General:";
            // 
            // maskPor
            // 
            this.maskPor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskPor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskPor.Location = new System.Drawing.Point(807, 13);
            this.maskPor.Margin = new System.Windows.Forms.Padding(4);
            this.maskPor.Mask = "000.00%";
            this.maskPor.Name = "maskPor";
            this.maskPor.Size = new System.Drawing.Size(60, 16);
            this.maskPor.TabIndex = 125;
            // 
            // gridN
            // 
            this.gridN.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.gridN.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridN.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.gridN.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridN.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.gridN.DisplayLayout.MaxColScrollRegions = 1;
            this.gridN.DisplayLayout.MaxRowScrollRegions = 1;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridN.DisplayLayout.Override.ActiveCellAppearance = appearance4;
            appearance5.BackColor = System.Drawing.SystemColors.Highlight;
            appearance5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridN.DisplayLayout.Override.ActiveRowAppearance = appearance5;
            this.gridN.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.gridN.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.gridN.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance7.BorderColor = System.Drawing.Color.Silver;
            appearance7.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.gridN.DisplayLayout.Override.CellAppearance = appearance7;
            this.gridN.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.gridN.DisplayLayout.Override.CellPadding = 0;
            appearance8.BackColor = System.Drawing.SystemColors.Control;
            appearance8.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance8.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance8.BorderColor = System.Drawing.SystemColors.Window;
            this.gridN.DisplayLayout.Override.GroupByRowAppearance = appearance8;
            appearance9.TextHAlignAsString = "Left";
            this.gridN.DisplayLayout.Override.HeaderAppearance = appearance9;
            this.gridN.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.gridN.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.gridN.DisplayLayout.Override.RowAppearance = appearance10;
            this.gridN.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance11.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gridN.DisplayLayout.Override.TemplateAddRowAppearance = appearance11;
            this.gridN.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.gridN.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.gridN.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.gridN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridN.Location = new System.Drawing.Point(0, 36);
            this.gridN.Margin = new System.Windows.Forms.Padding(4);
            this.gridN.Name = "gridN";
            this.gridN.Size = new System.Drawing.Size(1692, 488);
            this.gridN.TabIndex = 133;
            this.gridN.Text = "ultraGrid";
            this.gridN.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridN_InitializeLayout);
            this.gridN.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.gridN_CellChange);
            this.gridN.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridN_KeyUp);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.txtStotal);
            this.ultraGroupBox2.Controls.Add(this.label4);
            this.ultraGroupBox2.Controls.Add(this.txtSiva);
            this.ultraGroupBox2.Controls.Add(this.txtCtotal);
            this.ultraGroupBox2.Controls.Add(this.label3);
            this.ultraGroupBox2.Controls.Add(this.txtCiva);
            this.ultraGroupBox2.Controls.Add(this.label1);
            this.ultraGroupBox2.Controls.Add(this.txtCantidad);
            this.ultraGroupBox2.Controls.Add(this.txtTotal);
            this.ultraGroupBox2.Controls.Add(this.label7);
            this.ultraGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 524);
            this.ultraGroupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(1692, 43);
            this.ultraGroupBox2.TabIndex = 134;
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // txtStotal
            // 
            this.txtStotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStotal.Location = new System.Drawing.Point(1525, 11);
            this.txtStotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtStotal.Multiline = true;
            this.txtStotal.Name = "txtStotal";
            this.txtStotal.ReadOnly = true;
            this.txtStotal.Size = new System.Drawing.Size(139, 24);
            this.txtStotal.TabIndex = 102;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1305, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 101;
            this.label4.Text = "Seguro:";
            // 
            // txtSiva
            // 
            this.txtSiva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSiva.Location = new System.Drawing.Point(1379, 12);
            this.txtSiva.Margin = new System.Windows.Forms.Padding(4);
            this.txtSiva.Multiline = true;
            this.txtSiva.Name = "txtSiva";
            this.txtSiva.ReadOnly = true;
            this.txtSiva.Size = new System.Drawing.Size(139, 24);
            this.txtSiva.TabIndex = 100;
            // 
            // txtCtotal
            // 
            this.txtCtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCtotal.Location = new System.Drawing.Point(1155, 12);
            this.txtCtotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtCtotal.Multiline = true;
            this.txtCtotal.Name = "txtCtotal";
            this.txtCtotal.ReadOnly = true;
            this.txtCtotal.Size = new System.Drawing.Size(139, 24);
            this.txtCtotal.TabIndex = 99;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(925, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 98;
            this.label3.Text = "Copago:";
            // 
            // txtCiva
            // 
            this.txtCiva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCiva.Location = new System.Drawing.Point(1011, 12);
            this.txtCiva.Margin = new System.Windows.Forms.Padding(4);
            this.txtCiva.Multiline = true;
            this.txtCiva.Name = "txtCiva";
            this.txtCiva.ReadOnly = true;
            this.txtCiva.Size = new System.Drawing.Size(139, 24);
            this.txtCiva.TabIndex = 97;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(717, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 96;
            this.label1.Text = "Total:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCantidad.Location = new System.Drawing.Point(547, 12);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.txtCantidad.Multiline = true;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(148, 24);
            this.txtCantidad.TabIndex = 95;
            this.txtCantidad.Visible = false;
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.Location = new System.Drawing.Point(778, 9);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Multiline = true;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(139, 24);
            this.txtTotal.TabIndex = 94;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(462, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 66;
            this.label7.Text = "Cantidad:";
            this.label7.Visible = false;
            // 
            // frmCopagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1692, 567);
            this.Controls.Add(this.gridN);
            this.Controls.Add(this.optInd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.maskVal);
            this.Controls.Add(this.optVal);
            this.Controls.Add(this.optPor);
            this.Controls.Add(this.btnDescuento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maskPor);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.ultraGroupBox2);
            this.Name = "frmCopagos";
            this.Text = "Generar Copago";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.RadioButton optInd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox maskVal;
        private System.Windows.Forms.RadioButton optVal;
        private System.Windows.Forms.RadioButton optPor;
        private System.Windows.Forms.Button btnDescuento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskPor;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridN;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCiva;
        private System.Windows.Forms.TextBox txtCtotal;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.TextBox txtStotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSiva;
    }
}