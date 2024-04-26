namespace His.Garantia
{
    partial class frmAyuda
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
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
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
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            this.btnActualizar = new Infragistics.Win.Misc.UltraButton();
            this.TablaPacientes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txt_busqCi = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txt_busqNom = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txt_busqHist = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.cb_numFilas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TablaGarantia = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.TablaPacientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqCi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqNom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqHist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaGarantia)).BeginInit();
            this.SuspendLayout();
            // 
            // btnActualizar
            // 
            appearance14.BackColor = System.Drawing.Color.LightGray;
            this.btnActualizar.Appearance = appearance14;
            this.btnActualizar.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Flat;
            this.btnActualizar.Location = new System.Drawing.Point(555, 47);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(34, 33);
            this.btnActualizar.TabIndex = 5;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // TablaPacientes
            // 
            this.TablaPacientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.TablaPacientes.DisplayLayout.Appearance = appearance1;
            this.TablaPacientes.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.TablaPacientes.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.TablaPacientes.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TablaPacientes.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.TablaPacientes.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TablaPacientes.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.TablaPacientes.DisplayLayout.MaxColScrollRegions = 1;
            this.TablaPacientes.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TablaPacientes.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.TablaPacientes.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.TablaPacientes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.TablaPacientes.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.TablaPacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.TablaPacientes.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.TablaPacientes.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.TablaPacientes.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.TablaPacientes.DisplayLayout.Override.CellAppearance = appearance8;
            this.TablaPacientes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.TablaPacientes.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.TablaPacientes.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.TablaPacientes.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.TablaPacientes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.TablaPacientes.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.TablaPacientes.DisplayLayout.Override.RowAppearance = appearance11;
            this.TablaPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.TablaPacientes.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TablaPacientes.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.TablaPacientes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.TablaPacientes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.TablaPacientes.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.TablaPacientes.Location = new System.Drawing.Point(-2, 92);
            this.TablaPacientes.Name = "TablaPacientes";
            this.TablaPacientes.Size = new System.Drawing.Size(597, 244);
            this.TablaPacientes.TabIndex = 4;
            this.TablaPacientes.Text = "ultraGrid1";
            this.TablaPacientes.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.TablaPacientes_InitializeLayout);
            this.TablaPacientes.DoubleClick += new System.EventHandler(this.TablaPacientes_DoubleClick);
            this.TablaPacientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TablaPacientes_KeyDown);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.txt_busqCi);
            this.ultraGroupBox1.Controls.Add(this.txt_busqNom);
            this.ultraGroupBox1.Controls.Add(this.txt_busqHist);
            this.ultraGroupBox1.Controls.Add(this.cb_numFilas);
            this.ultraGroupBox1.Controls.Add(this.label4);
            this.ultraGroupBox1.Controls.Add(this.label3);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Location = new System.Drawing.Point(-2, 14);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(557, 72);
            this.ultraGroupBox1.TabIndex = 3;
            this.ultraGroupBox1.Text = "Filtros";
            // 
            // txt_busqCi
            // 
            this.txt_busqCi.Location = new System.Drawing.Point(451, 39);
            this.txt_busqCi.Name = "txt_busqCi";
            this.txt_busqCi.Size = new System.Drawing.Size(100, 21);
            this.txt_busqCi.TabIndex = 9;
            this.txt_busqCi.TextChanged += new System.EventHandler(this.txt_busqCi_TextChanged);
            this.txt_busqCi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_busqCi_KeyPress);
            // 
            // txt_busqNom
            // 
            this.txt_busqNom.Location = new System.Drawing.Point(192, 39);
            this.txt_busqNom.Name = "txt_busqNom";
            this.txt_busqNom.Size = new System.Drawing.Size(256, 21);
            this.txt_busqNom.TabIndex = 8;
            this.txt_busqNom.TextChanged += new System.EventHandler(this.txt_busqNom_TextChanged);
            this.txt_busqNom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_busqNom_KeyPress);
            // 
            // txt_busqHist
            // 
            this.txt_busqHist.Location = new System.Drawing.Point(89, 39);
            this.txt_busqHist.Name = "txt_busqHist";
            this.txt_busqHist.Size = new System.Drawing.Size(100, 21);
            this.txt_busqHist.TabIndex = 7;
            this.txt_busqHist.TextChanged += new System.EventHandler(this.txt_busqHist_TextChanged);
            this.txt_busqHist.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_busqHist_KeyPress);
            // 
            // cb_numFilas
            // 
            this.cb_numFilas.FormattingEnabled = true;
            this.cb_numFilas.Location = new System.Drawing.Point(5, 39);
            this.cb_numFilas.Name = "cb_numFilas";
            this.cb_numFilas.Size = new System.Drawing.Size(83, 21);
            this.cb_numFilas.TabIndex = 4;
            this.cb_numFilas.SelectedValueChanged += new System.EventHandler(this.cb_numFilas_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(448, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nro. Identificación :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombres :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Historia Clínica :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nro. Filas :";
            // 
            // TablaGarantia
            // 
            this.TablaGarantia.AllowUserToAddRows = false;
            this.TablaGarantia.AllowUserToDeleteRows = false;
            this.TablaGarantia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablaGarantia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaGarantia.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablaGarantia.BackgroundColor = System.Drawing.Color.White;
            this.TablaGarantia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaGarantia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaGarantia.Location = new System.Drawing.Point(-2, 92);
            this.TablaGarantia.Name = "TablaGarantia";
            this.TablaGarantia.ReadOnly = true;
            this.TablaGarantia.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TablaGarantia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaGarantia.Size = new System.Drawing.Size(597, 257);
            this.TablaGarantia.TabIndex = 6;
            this.TablaGarantia.Visible = false;
            this.TablaGarantia.DoubleClick += new System.EventHandler(this.TablaGarantia_DoubleClick);
            // 
            // frmAyuda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 350);
            this.Controls.Add(this.TablaGarantia);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.TablaPacientes);
            this.Controls.Add(this.ultraGroupBox1);
            this.Name = "frmAyuda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.frmAyuda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TablaPacientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqCi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqNom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_busqHist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaGarantia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraButton btnActualizar;
        private Infragistics.Win.UltraWinGrid.UltraGrid TablaPacientes;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txt_busqCi;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txt_busqNom;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txt_busqHist;
        private System.Windows.Forms.ComboBox cb_numFilas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView TablaGarantia;
    }
}