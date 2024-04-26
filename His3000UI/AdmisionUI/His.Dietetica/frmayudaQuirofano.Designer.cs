namespace His.Dietetica
{
    partial class frmayudaQuirofano
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
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.TablaProductos = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.chkAutoBusqueda = new System.Windows.Forms.CheckBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbPorDescripcion = new System.Windows.Forms.RadioButton();
            this.rdbPorCodigo = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.timerBusqueda = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TablaProductos)).BeginInit();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TablaProductos
            // 
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.TablaProductos.DisplayLayout.Appearance = appearance4;
            this.TablaProductos.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            this.TablaProductos.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.TablaProductos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance1.BorderColor = System.Drawing.SystemColors.Window;
            this.TablaProductos.DisplayLayout.GroupByBox.Appearance = appearance1;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TablaProductos.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.TablaProductos.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TablaProductos.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.TablaProductos.DisplayLayout.MaxColScrollRegions = 1;
            this.TablaProductos.DisplayLayout.MaxRowScrollRegions = 1;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TablaProductos.DisplayLayout.Override.ActiveCellAppearance = appearance12;
            appearance7.BackColor = System.Drawing.SystemColors.Highlight;
            appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.TablaProductos.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.TablaProductos.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.TablaProductos.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.TablaProductos.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance5.BorderColor = System.Drawing.Color.Silver;
            appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.TablaProductos.DisplayLayout.Override.CellAppearance = appearance5;
            this.TablaProductos.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.TablaProductos.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.TablaProductos.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance11.TextHAlignAsString = "Left";
            this.TablaProductos.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.TablaProductos.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.TablaProductos.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.TablaProductos.DisplayLayout.Override.RowAppearance = appearance10;
            this.TablaProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TablaProductos.DisplayLayout.Override.TemplateAddRowAppearance = appearance8;
            this.TablaProductos.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.TablaProductos.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.TablaProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TablaProductos.Location = new System.Drawing.Point(0, 82);
            this.TablaProductos.Name = "TablaProductos";
            this.TablaProductos.Size = new System.Drawing.Size(589, 357);
            this.TablaProductos.TabIndex = 5;
            this.TablaProductos.Text = "ultraGrid1";
            this.TablaProductos.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.TablaProductos_InitializeLayout);
            this.TablaProductos.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.TablaProductos_DoubleClickRow);
            this.TablaProductos.DoubleClick += new System.EventHandler(this.TablaProductos_DoubleClick);
            this.TablaProductos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TablaProductos_KeyUp);
            // 
            // ultraPanel1
            // 
            appearance14.BackColor = System.Drawing.Color.Silver;
            appearance14.BackColor2 = System.Drawing.Color.DimGray;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop20;
            this.ultraPanel1.Appearance = appearance14;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.chkAutoBusqueda);
            this.ultraPanel1.ClientArea.Controls.Add(this.txtBuscar);
            this.ultraPanel1.ClientArea.Controls.Add(this.groupBox1);
            this.ultraPanel1.ClientArea.Controls.Add(this.btnBuscar);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraPanel1.Location = new System.Drawing.Point(0, 0);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(589, 82);
            this.ultraPanel1.TabIndex = 7;
            // 
            // chkAutoBusqueda
            // 
            this.chkAutoBusqueda.AutoSize = true;
            this.chkAutoBusqueda.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoBusqueda.Checked = true;
            this.chkAutoBusqueda.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoBusqueda.Location = new System.Drawing.Point(258, 44);
            this.chkAutoBusqueda.Name = "chkAutoBusqueda";
            this.chkAutoBusqueda.Size = new System.Drawing.Size(166, 17);
            this.chkAutoBusqueda.TabIndex = 2;
            this.chkAutoBusqueda.Text = "Activar Búsqueda Automática";
            this.chkAutoBusqueda.UseVisualStyleBackColor = false;
            this.chkAutoBusqueda.CheckedChanged += new System.EventHandler(this.chkAutoBusqueda_CheckedChanged);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(15, 6);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(468, 20);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rdbPorDescripcion);
            this.groupBox1.Controls.Add(this.rdbPorCodigo);
            this.groupBox1.Location = new System.Drawing.Point(15, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 35);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones de Busqueda";
            // 
            // rdbPorDescripcion
            // 
            this.rdbPorDescripcion.AutoSize = true;
            this.rdbPorDescripcion.Checked = true;
            this.rdbPorDescripcion.Location = new System.Drawing.Point(123, 14);
            this.rdbPorDescripcion.Name = "rdbPorDescripcion";
            this.rdbPorDescripcion.Size = new System.Drawing.Size(81, 17);
            this.rdbPorDescripcion.TabIndex = 1;
            this.rdbPorDescripcion.TabStop = true;
            this.rdbPorDescripcion.Text = "Descripción";
            this.rdbPorDescripcion.UseVisualStyleBackColor = true;
            // 
            // rdbPorCodigo
            // 
            this.rdbPorCodigo.AutoSize = true;
            this.rdbPorCodigo.Location = new System.Drawing.Point(36, 14);
            this.rdbPorCodigo.Name = "rdbPorCodigo";
            this.rdbPorCodigo.Size = new System.Drawing.Size(58, 17);
            this.rdbPorCodigo.TabIndex = 0;
            this.rdbPorCodigo.Text = "Codigo";
            this.rdbPorCodigo.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Sienna;
            this.btnBuscar.Location = new System.Drawing.Point(489, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(82, 23);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // timerBusqueda
            // 
            this.timerBusqueda.Tick += new System.EventHandler(this.timerBusqueda_Tick);
            // 
            // frmayudaQuirofano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 439);
            this.Controls.Add(this.TablaProductos);
            this.Controls.Add(this.ultraPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmayudaQuirofano";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda [Quirófano]";
            this.Load += new System.EventHandler(this.frmayudaQuirofano_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TablaProductos)).EndInit();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ClientArea.PerformLayout();
            this.ultraPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid TablaProductos;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private System.Windows.Forms.CheckBox chkAutoBusqueda;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbPorDescripcion;
        private System.Windows.Forms.RadioButton rdbPorCodigo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Timer timerBusqueda;
    }
}