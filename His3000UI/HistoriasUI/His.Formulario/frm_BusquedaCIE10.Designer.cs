namespace His.Formulario
{
    partial class frm_BusquedaCIE10
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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.chkAutoBusqueda = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbPorDescripcion = new System.Windows.Forms.RadioButton();
            this.rdbPorCodigo = new System.Windows.Forms.RadioButton();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.ulgdbListadoCIE = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.timerBusqueda = new System.Windows.Forms.Timer(this.components);
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.frm_BusquedaCIE10_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ulgdbListadoCIE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.frm_BusquedaCIE10_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_BusquedaCIE10_Fill_Panel.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkAutoBusqueda
            // 
            this.chkAutoBusqueda.AutoSize = true;
            this.chkAutoBusqueda.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoBusqueda.Checked = true;
            this.chkAutoBusqueda.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoBusqueda.Location = new System.Drawing.Point(344, 54);
            this.chkAutoBusqueda.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAutoBusqueda.Name = "chkAutoBusqueda";
            this.chkAutoBusqueda.Size = new System.Drawing.Size(215, 21);
            this.chkAutoBusqueda.TabIndex = 2;
            this.chkAutoBusqueda.Text = "Activar Búsqueda Automática";
            this.chkAutoBusqueda.UseVisualStyleBackColor = false;
            this.chkAutoBusqueda.CheckedChanged += new System.EventHandler(this.chkAutoBusqueda_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rdbPorDescripcion);
            this.groupBox1.Controls.Add(this.rdbPorCodigo);
            this.groupBox1.Location = new System.Drawing.Point(20, 39);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(296, 43);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones de Busqueda";
            // 
            // rdbPorDescripcion
            // 
            this.rdbPorDescripcion.AutoSize = true;
            this.rdbPorDescripcion.Checked = true;
            this.rdbPorDescripcion.Location = new System.Drawing.Point(164, 17);
            this.rdbPorDescripcion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbPorDescripcion.Name = "rdbPorDescripcion";
            this.rdbPorDescripcion.Size = new System.Drawing.Size(103, 21);
            this.rdbPorDescripcion.TabIndex = 1;
            this.rdbPorDescripcion.TabStop = true;
            this.rdbPorDescripcion.Text = "Descripción";
            this.rdbPorDescripcion.UseVisualStyleBackColor = true;
            // 
            // rdbPorCodigo
            // 
            this.rdbPorCodigo.AutoSize = true;
            this.rdbPorCodigo.Location = new System.Drawing.Point(48, 17);
            this.rdbPorCodigo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbPorCodigo.Name = "rdbPorCodigo";
            this.rdbPorCodigo.Size = new System.Drawing.Size(73, 21);
            this.rdbPorCodigo.TabIndex = 0;
            this.rdbPorCodigo.Text = "Codigo";
            this.rdbPorCodigo.UseVisualStyleBackColor = true;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(20, 7);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(797, 22);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Sienna;
            this.btnBuscar.Location = new System.Drawing.Point(824, 7);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(109, 28);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // ulgdbListadoCIE
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ulgdbListadoCIE.DisplayLayout.Appearance = appearance1;
            this.ulgdbListadoCIE.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ulgdbListadoCIE.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ulgdbListadoCIE.DisplayLayout.MaxColScrollRegions = 1;
            this.ulgdbListadoCIE.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ulgdbListadoCIE.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ulgdbListadoCIE.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.ulgdbListadoCIE.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ulgdbListadoCIE.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ulgdbListadoCIE.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ulgdbListadoCIE.DisplayLayout.Override.CellAppearance = appearance8;
            this.ulgdbListadoCIE.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ulgdbListadoCIE.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ulgdbListadoCIE.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.ulgdbListadoCIE.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ulgdbListadoCIE.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ulgdbListadoCIE.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ulgdbListadoCIE.DisplayLayout.Override.RowAppearance = appearance11;
            this.ulgdbListadoCIE.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ulgdbListadoCIE.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.ulgdbListadoCIE.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ulgdbListadoCIE.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ulgdbListadoCIE.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ulgdbListadoCIE.Location = new System.Drawing.Point(7, 124);
            this.ulgdbListadoCIE.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ulgdbListadoCIE.Name = "ulgdbListadoCIE";
            this.ulgdbListadoCIE.Size = new System.Drawing.Size(944, 412);
            this.ulgdbListadoCIE.TabIndex = 2;
            this.ulgdbListadoCIE.Text = "ultraGrid1";
            this.ulgdbListadoCIE.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ulgdbListadoCIE_InitializeLayout);
            this.ulgdbListadoCIE.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.ulgdbListadoCIE_DoubleClickCell);
            this.ulgdbListadoCIE.DoubleClick += new System.EventHandler(this.ulgdbListadoCIE_DoubleClick);
            this.ulgdbListadoCIE.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ulgdbListadoCIE_KeyUp);
            this.ulgdbListadoCIE.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ulgdbListadoCIE_MouseDoubleClick);
            // 
            // timerBusqueda
            // 
            this.timerBusqueda.Tick += new System.EventHandler(this.timerBusqueda_Tick);
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // frm_BusquedaCIE10_Fill_Panel
            // 
            appearance13.BackColor = System.Drawing.Color.GhostWhite;
            appearance13.BackColor2 = System.Drawing.Color.LightGray;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.frm_BusquedaCIE10_Fill_Panel.Appearance = appearance13;
            // 
            // frm_BusquedaCIE10_Fill_Panel.ClientArea
            // 
            this.frm_BusquedaCIE10_Fill_Panel.ClientArea.Controls.Add(this.ulgdbListadoCIE);
            this.frm_BusquedaCIE10_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_BusquedaCIE10_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_BusquedaCIE10_Fill_Panel.Location = new System.Drawing.Point(4, 34);
            this.frm_BusquedaCIE10_Fill_Panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.frm_BusquedaCIE10_Fill_Panel.Name = "frm_BusquedaCIE10_Fill_Panel";
            this.frm_BusquedaCIE10_Fill_Panel.Size = new System.Drawing.Size(959, 561);
            this.frm_BusquedaCIE10_Fill_Panel.TabIndex = 0;
            // 
            // _frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left
            // 
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 34);
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.Name = "_frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left";
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 561);
            // 
            // _frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right
            // 
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(963, 34);
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.Name = "_frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right";
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 561);
            // 
            // _frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top
            // 
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top.Name = "_frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top";
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(967, 34);
            // 
            // _frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom
            // 
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 595);
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.Name = "_frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom";
            this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(967, 4);
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
            this.ultraPanel1.Location = new System.Drawing.Point(4, 38);
            this.ultraPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(952, 101);
            this.ultraPanel1.TabIndex = 5;
            // 
            // frm_BusquedaCIE10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(967, 599);
            this.Controls.Add(this.ultraPanel1);
            this.Controls.Add(this.frm_BusquedaCIE10_Fill_Panel);
            this.Controls.Add(this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frm_BusquedaCIE10";
            this.Text = "Busqueda CIE10";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ulgdbListadoCIE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.frm_BusquedaCIE10_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_BusquedaCIE10_Fill_Panel.ResumeLayout(false);
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ClientArea.PerformLayout();
            this.ultraPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAutoBusqueda;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbPorDescripcion;
        private System.Windows.Forms.RadioButton rdbPorCodigo;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private Infragistics.Win.UltraWinGrid.UltraGrid ulgdbListadoCIE;
        private System.Windows.Forms.Timer timerBusqueda;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.Misc.UltraPanel frm_BusquedaCIE10_Fill_Panel;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_BusquedaCIE10_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_BusquedaCIE10_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_BusquedaCIE10_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_BusquedaCIE10_UltraFormManager_Dock_Area_Bottom;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
    }
}