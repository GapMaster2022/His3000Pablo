namespace His.Dietetica
{
    partial class frm_SolicitarProductos
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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            this.panelPaciente = new System.Windows.Forms.GroupBox();
            this.UltraGridProductos = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Solicitados = new System.Windows.Forms.DataGridView();
            this.codprod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.despro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.btncancelar = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridProductos)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Solicitados)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPaciente
            // 
            this.panelPaciente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(253)))));
            this.panelPaciente.Controls.Add(this.UltraGridProductos);
            this.panelPaciente.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelPaciente.Location = new System.Drawing.Point(0, 0);
            this.panelPaciente.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelPaciente.Name = "panelPaciente";
            this.panelPaciente.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelPaciente.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelPaciente.Size = new System.Drawing.Size(1242, 320);
            this.panelPaciente.TabIndex = 115;
            this.panelPaciente.TabStop = false;
            this.panelPaciente.Text = "Productos";
            // 
            // UltraGridProductos
            // 
            this.UltraGridProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.UltraGridProductos.DisplayLayout.Appearance = appearance13;
            this.UltraGridProductos.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.UltraGridProductos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.UltraGridProductos.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.MediumPurple;
            appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.UltraGridProductos.DisplayLayout.GroupByBox.Appearance = appearance14;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UltraGridProductos.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
            this.UltraGridProductos.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UltraGridProductos.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
            this.UltraGridProductos.DisplayLayout.MaxColScrollRegions = 1;
            this.UltraGridProductos.DisplayLayout.MaxRowScrollRegions = 1;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UltraGridProductos.DisplayLayout.Override.ActiveCellAppearance = appearance17;
            appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.UltraGridProductos.DisplayLayout.Override.ActiveRowAppearance = appearance18;
            this.UltraGridProductos.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.UltraGridProductos.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.UltraGridProductos.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.UltraGridProductos.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.UltraGridProductos.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.UltraGridProductos.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.UltraGridProductos.DisplayLayout.Override.CellAppearance = appearance20;
            this.UltraGridProductos.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.UltraGridProductos.DisplayLayout.Override.CellPadding = 0;
            this.UltraGridProductos.DisplayLayout.Override.ColumnSizingArea = Infragistics.Win.UltraWinGrid.ColumnSizingArea.EntireColumn;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.UltraGridProductos.DisplayLayout.Override.GroupByRowAppearance = appearance21;
            appearance22.TextHAlignAsString = "Left";
            this.UltraGridProductos.DisplayLayout.Override.HeaderAppearance = appearance22;
            this.UltraGridProductos.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.UltraGridProductos.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            this.UltraGridProductos.DisplayLayout.Override.RowAppearance = appearance23;
            this.UltraGridProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.UltraGridProductos.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
            this.UltraGridProductos.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.UltraGridProductos.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.UltraGridProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UltraGridProductos.Location = new System.Drawing.Point(9, 29);
            this.UltraGridProductos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UltraGridProductos.Name = "UltraGridProductos";
            this.UltraGridProductos.Size = new System.Drawing.Size(1224, 282);
            this.UltraGridProductos.TabIndex = 82;
            this.UltraGridProductos.Text = "ultraGrid1";
            this.UltraGridProductos.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.UltraGridProductos_InitializeLayout);
            this.UltraGridProductos.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.UltraGridProductos_DoubleClickRow);
            this.UltraGridProductos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UltraGridProductos_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(253)))));
            this.groupBox1.Controls.Add(this.Solicitados);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 320);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox1.Size = new System.Drawing.Size(1242, 320);
            this.groupBox1.TabIndex = 116;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Productos Solicitados";
            // 
            // Solicitados
            // 
            this.Solicitados.AllowUserToAddRows = false;
            this.Solicitados.BackgroundColor = System.Drawing.Color.White;
            this.Solicitados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Solicitados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Solicitados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Solicitados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codprod,
            this.despro});
            this.Solicitados.Location = new System.Drawing.Point(9, 29);
            this.Solicitados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Solicitados.Name = "Solicitados";
            this.Solicitados.ReadOnly = true;
            this.Solicitados.RowHeadersWidth = 62;
            this.Solicitados.Size = new System.Drawing.Size(1224, 282);
            this.Solicitados.TabIndex = 0;
            // 
            // codprod
            // 
            this.codprod.HeaderText = "Codigo";
            this.codprod.MinimumWidth = 8;
            this.codprod.Name = "codprod";
            this.codprod.ReadOnly = true;
            this.codprod.Width = 150;
            // 
            // despro
            // 
            this.despro.HeaderText = "Descripcion";
            this.despro.MinimumWidth = 8;
            this.despro.Name = "despro";
            this.despro.ReadOnly = true;
            this.despro.Width = 700;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(939, 649);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 117;
            this.button1.Text = "ACEPTAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btncancelar
            // 
            this.btncancelar.Location = new System.Drawing.Point(1088, 649);
            this.btncancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(112, 35);
            this.btncancelar.TabIndex = 118;
            this.btncancelar.Text = "CANCELAR";
            this.btncancelar.UseVisualStyleBackColor = true;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Codigo";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Descripcion";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 700;
            // 
            // frm_SolicitarProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 692);
            this.Controls.Add(this.btncancelar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelPaciente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frm_SolicitarProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Productos";
            this.Load += new System.EventHandler(this.frm_SolicitarProductos_Load);
            this.panelPaciente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridProductos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Solicitados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox panelPaciente;
        private Infragistics.Win.UltraWinGrid.UltraGrid UltraGridProductos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView Solicitados;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btncancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn codprod;
        private System.Windows.Forms.DataGridViewTextBoxColumn despro;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}