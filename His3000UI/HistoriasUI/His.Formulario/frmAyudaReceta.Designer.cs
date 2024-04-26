
namespace His.Formulario
{
    partial class frmAyudaReceta
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
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.chkAutoBusqueda = new System.Windows.Forms.CheckBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbPorDescripcion = new System.Windows.Forms.RadioButton();
            this.rdbPorCodigo = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.UltraGridDatos = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridDatos)).BeginInit();
            this.SuspendLayout();
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
            this.ultraPanel1.Size = new System.Drawing.Size(641, 82);
            this.ultraPanel1.TabIndex = 7;
            // 
            // chkAutoBusqueda
            // 
            this.chkAutoBusqueda.AutoSize = true;
            this.chkAutoBusqueda.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoBusqueda.Checked = true;
            this.chkAutoBusqueda.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoBusqueda.Location = new System.Drawing.Point(489, 33);
            this.chkAutoBusqueda.Name = "chkAutoBusqueda";
            this.chkAutoBusqueda.Size = new System.Drawing.Size(105, 17);
            this.chkAutoBusqueda.TabIndex = 2;
            this.chkAutoBusqueda.Text = "Cuadros Basicos";
            this.chkAutoBusqueda.UseVisualStyleBackColor = false;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(15, 6);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(468, 20);
            this.txtBuscar.TabIndex = 0;
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
            this.groupBox1.Visible = false;
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
            // UltraGridDatos
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.UltraGridDatos.DisplayLayout.Appearance = appearance1;
            this.UltraGridDatos.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.UltraGridDatos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.UltraGridDatos.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UltraGridDatos.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.UltraGridDatos.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UltraGridDatos.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.UltraGridDatos.DisplayLayout.MaxColScrollRegions = 1;
            this.UltraGridDatos.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UltraGridDatos.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.UltraGridDatos.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.UltraGridDatos.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.UltraGridDatos.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.UltraGridDatos.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.UltraGridDatos.DisplayLayout.Override.CellAppearance = appearance8;
            this.UltraGridDatos.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.UltraGridDatos.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.UltraGridDatos.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.UltraGridDatos.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.UltraGridDatos.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.UltraGridDatos.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.UltraGridDatos.DisplayLayout.Override.RowAppearance = appearance11;
            this.UltraGridDatos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.UltraGridDatos.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.UltraGridDatos.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.UltraGridDatos.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.UltraGridDatos.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.UltraGridDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UltraGridDatos.Location = new System.Drawing.Point(0, 82);
            this.UltraGridDatos.Name = "UltraGridDatos";
            this.UltraGridDatos.Size = new System.Drawing.Size(641, 368);
            this.UltraGridDatos.TabIndex = 8;
            this.UltraGridDatos.Text = "ultraGrid1";
            this.UltraGridDatos.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.UltraGridDatos_InitializeLayout);
            this.UltraGridDatos.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.UltraGridDatos_DoubleClickRow);
            // 
            // frmAyudaReceta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 450);
            this.Controls.Add(this.UltraGridDatos);
            this.Controls.Add(this.ultraPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAyudaReceta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda - Receta";
            this.Load += new System.EventHandler(this.frmAyudaReceta_Load);
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ClientArea.PerformLayout();
            this.ultraPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private System.Windows.Forms.CheckBox chkAutoBusqueda;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbPorDescripcion;
        private System.Windows.Forms.RadioButton rdbPorCodigo;
        private System.Windows.Forms.Button btnBuscar;
        private Infragistics.Win.UltraWinGrid.UltraGrid UltraGridDatos;
    }
}