namespace TarifariosUI
{
    partial class frmBusquedaCIE10
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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusquedaCIE10));
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
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinScrollBar.ScrollBarLook scrollBarLook1 = new Infragistics.Win.UltraWinScrollBar.ScrollBarLook();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAutoBusqueda = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbPorDescripcion = new System.Windows.Forms.RadioButton();
            this.rdbPorCodigo = new System.Windows.Forms.RadioButton();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.ulgdbListadoCIE = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.timerBusqueda = new System.Windows.Forms.Timer(this.components);
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.frmBusquedaCIE10_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ulgdbListadoCIE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.frmBusquedaCIE10_Fill_Panel.ClientArea.SuspendLayout();
            this.frmBusquedaCIE10_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkAutoBusqueda);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txtBuscar);
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 78);
            this.panel1.TabIndex = 0;
            // 
            // chkAutoBusqueda
            // 
            this.chkAutoBusqueda.AutoSize = true;
            this.chkAutoBusqueda.Checked = true;
            this.chkAutoBusqueda.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoBusqueda.Location = new System.Drawing.Point(256, 50);
            this.chkAutoBusqueda.Name = "chkAutoBusqueda";
            this.chkAutoBusqueda.Size = new System.Drawing.Size(166, 17);
            this.chkAutoBusqueda.TabIndex = 2;
            this.chkAutoBusqueda.Text = "Activar Búsqueda Automática";
            this.chkAutoBusqueda.UseVisualStyleBackColor = true;
            this.chkAutoBusqueda.CheckedChanged += new System.EventHandler(this.chkAutoBusqueda_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rdbPorDescripcion);
            this.groupBox1.Controls.Add(this.rdbPorCodigo);
            this.groupBox1.Location = new System.Drawing.Point(13, 38);
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
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(13, 12);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(468, 20);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.Leave += new System.EventHandler(this.txtBuscar_Leave);
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnBuscar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnBuscar.Location = new System.Drawing.Point(487, 10);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(82, 23);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // ulgdbListadoCIE
            // 
            this.ulgdbListadoCIE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance13.BackColor = System.Drawing.Color.White;
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            this.ulgdbListadoCIE.DisplayLayout.AddNewBox.Appearance = appearance13;
            this.ulgdbListadoCIE.DisplayLayout.AddNewBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            appearance14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance14.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance14.ImageBackground")));
            appearance14.ImageBackgroundAlpha = Infragistics.Win.Alpha.UseAlphaLevel;
            appearance14.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(6, 3, 6, 3);
            appearance14.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.ulgdbListadoCIE.DisplayLayout.AddNewBox.ButtonAppearance = appearance14;
            this.ulgdbListadoCIE.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            this.ulgdbListadoCIE.DisplayLayout.AddNewBox.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            appearance15.BackColor = System.Drawing.Color.White;
            this.ulgdbListadoCIE.DisplayLayout.Appearance = appearance15;
            appearance16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance16.FontData.Name = "Trebuchet MS";
            appearance16.FontData.SizeInPoints = 9F;
            appearance16.ForeColor = System.Drawing.Color.White;
            appearance16.TextHAlignAsString = "Right";
            this.ulgdbListadoCIE.DisplayLayout.CaptionAppearance = appearance16;
            this.ulgdbListadoCIE.DisplayLayout.FixedHeaderOffImage = ((System.Drawing.Image)(resources.GetObject("ulgdbListadoCIE.DisplayLayout.FixedHeaderOffImage")));
            this.ulgdbListadoCIE.DisplayLayout.FixedHeaderOnImage = ((System.Drawing.Image)(resources.GetObject("ulgdbListadoCIE.DisplayLayout.FixedHeaderOnImage")));
            this.ulgdbListadoCIE.DisplayLayout.FixedRowOffImage = ((System.Drawing.Image)(resources.GetObject("ulgdbListadoCIE.DisplayLayout.FixedRowOffImage")));
            this.ulgdbListadoCIE.DisplayLayout.FixedRowOnImage = ((System.Drawing.Image)(resources.GetObject("ulgdbListadoCIE.DisplayLayout.FixedRowOnImage")));
            appearance17.FontData.BoldAsString = "True";
            appearance17.FontData.Name = "Trebuchet MS";
            appearance17.FontData.SizeInPoints = 10F;
            appearance17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(127)))), ((int)(((byte)(177)))));
            appearance17.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance17.ImageBackground")));
            appearance17.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(0, 2, 0, 3);
            appearance17.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.Appearance = appearance17;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.ButtonBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ulgdbListadoCIE.DisplayLayout.GroupByBox.Hidden = true;
            this.ulgdbListadoCIE.DisplayLayout.MaxColScrollRegions = 1;
            this.ulgdbListadoCIE.DisplayLayout.MaxRowScrollRegions = 1;
            this.ulgdbListadoCIE.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
            this.ulgdbListadoCIE.DisplayLayout.Override.BorderStyleHeader = Infragistics.Win.UIElementBorderStyle.None;
            this.ulgdbListadoCIE.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
            this.ulgdbListadoCIE.DisplayLayout.Override.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
            appearance18.BackColor = System.Drawing.Color.Transparent;
            this.ulgdbListadoCIE.DisplayLayout.Override.CardAreaAppearance = appearance18;
            appearance19.BorderColor = System.Drawing.Color.Transparent;
            appearance19.FontData.Name = "Verdana";
            this.ulgdbListadoCIE.DisplayLayout.Override.CellAppearance = appearance19;
            appearance20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance20.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance20.ImageBackground")));
            appearance20.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(6, 3, 6, 3);
            appearance20.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.ulgdbListadoCIE.DisplayLayout.Override.CellButtonAppearance = appearance20;
            this.ulgdbListadoCIE.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            appearance21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.ulgdbListadoCIE.DisplayLayout.Override.FilterCellAppearance = appearance21;
            appearance22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance22.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance22.ImageBackground")));
            appearance22.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(6, 3, 6, 3);
            this.ulgdbListadoCIE.DisplayLayout.Override.FilterClearButtonAppearance = appearance22;
            appearance23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            appearance23.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
            this.ulgdbListadoCIE.DisplayLayout.Override.FilterRowPromptAppearance = appearance23;
            appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance24.FontData.BoldAsString = "True";
            appearance24.FontData.Name = "Trebuchet MS";
            appearance24.FontData.SizeInPoints = 10F;
            appearance24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            appearance24.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance24.ImageBackground")));
            appearance24.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Tiled;
            appearance24.TextHAlignAsString = "Left";
            appearance24.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ulgdbListadoCIE.DisplayLayout.Override.HeaderAppearance = appearance24;
            this.ulgdbListadoCIE.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ulgdbListadoCIE.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.XPThemed;
            appearance25.BorderColor = System.Drawing.Color.Transparent;
            this.ulgdbListadoCIE.DisplayLayout.Override.RowAppearance = appearance25;
            appearance26.BackColor = System.Drawing.Color.White;
            this.ulgdbListadoCIE.DisplayLayout.Override.RowSelectorAppearance = appearance26;
            appearance27.BorderColor = System.Drawing.Color.Transparent;
            appearance27.ForeColor = System.Drawing.Color.Black;
            this.ulgdbListadoCIE.DisplayLayout.Override.SelectedCellAppearance = appearance27;
            appearance28.BorderColor = System.Drawing.Color.Transparent;
            appearance28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(27)))), ((int)(((byte)(85)))));
            appearance28.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance28.ImageBackground")));
            appearance28.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(1, 1, 1, 4);
            appearance28.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.ulgdbListadoCIE.DisplayLayout.Override.SelectedRowAppearance = appearance28;
            appearance29.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(2, 4, 2, 4);
            appearance29.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            scrollBarLook1.Appearance = appearance29;
            appearance30.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance30.ImageBackground")));
            appearance30.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(3, 2, 3, 2);
            scrollBarLook1.AppearanceHorizontal = appearance30;
            appearance31.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance31.ImageBackground")));
            appearance31.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(2, 3, 2, 3);
            appearance31.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            scrollBarLook1.AppearanceVertical = appearance31;
            appearance32.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance32.ImageBackground")));
            appearance32.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(0, 2, 0, 1);
            scrollBarLook1.TrackAppearanceHorizontal = appearance32;
            appearance33.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance33.ImageBackground")));
            appearance33.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(2, 0, 1, 0);
            appearance33.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            scrollBarLook1.TrackAppearanceVertical = appearance33;
            this.ulgdbListadoCIE.DisplayLayout.ScrollBarLook = scrollBarLook1;
            this.ulgdbListadoCIE.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ulgdbListadoCIE.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ulgdbListadoCIE.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ulgdbListadoCIE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ulgdbListadoCIE.Location = new System.Drawing.Point(8, 84);
            this.ulgdbListadoCIE.Name = "ulgdbListadoCIE";
            this.ulgdbListadoCIE.Size = new System.Drawing.Size(552, 379);
            this.ulgdbListadoCIE.TabIndex = 0;
            this.ulgdbListadoCIE.Text = "Grid Caption Area";
            this.ulgdbListadoCIE.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.ulgdbListadoCIE.DoubleClick += new System.EventHandler(this.ulgdbListadoCIE_DoubleClick);
            this.ulgdbListadoCIE.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ulgdbListadoCIE_InitializeLayout);
            this.ulgdbListadoCIE.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ulgdbListadoCIE_KeyUp);
            // 
            // timerBusqueda
            // 
            this.timerBusqueda.Interval = 1000;
            this.timerBusqueda.Tick += new System.EventHandler(this.timerBusqueda_Tick);
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // frmBusquedaCIE10_Fill_Panel
            // 
            // 
            // frmBusquedaCIE10_Fill_Panel.ClientArea
            // 
            this.frmBusquedaCIE10_Fill_Panel.ClientArea.Controls.Add(this.ulgdbListadoCIE);
            this.frmBusquedaCIE10_Fill_Panel.ClientArea.Controls.Add(this.panel1);
            this.frmBusquedaCIE10_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frmBusquedaCIE10_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmBusquedaCIE10_Fill_Panel.Location = new System.Drawing.Point(4, 24);
            this.frmBusquedaCIE10_Fill_Panel.Name = "frmBusquedaCIE10_Fill_Panel";
            this.frmBusquedaCIE10_Fill_Panel.Size = new System.Drawing.Size(572, 474);
            this.frmBusquedaCIE10_Fill_Panel.TabIndex = 0;
            // 
            // _frmBusquedaCIE10_UltraFormManager_Dock_Area_Left
            // 
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 24);
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left.Name = "_frmBusquedaCIE10_UltraFormManager_Dock_Area_Left";
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 474);
            // 
            // _frmBusquedaCIE10_UltraFormManager_Dock_Area_Right
            // 
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(576, 24);
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right.Name = "_frmBusquedaCIE10_UltraFormManager_Dock_Area_Right";
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 474);
            // 
            // _frmBusquedaCIE10_UltraFormManager_Dock_Area_Top
            // 
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top.Name = "_frmBusquedaCIE10_UltraFormManager_Dock_Area_Top";
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(580, 24);
            // 
            // _frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom
            // 
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 498);
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom.Name = "_frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom";
            this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(580, 4);
            // 
            // frmBusquedaCIE10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(580, 502);
            this.Controls.Add(this.frmBusquedaCIE10_Fill_Panel);
            this.Controls.Add(this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom);
            this.MaximizeBox = false;
            this.Name = "frmBusquedaCIE10";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CIE 10";
            this.Load += new System.EventHandler(this.frmBusquedaCIE10_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ulgdbListadoCIE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.frmBusquedaCIE10_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frmBusquedaCIE10_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ulgdbListadoCIE;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbPorDescripcion;
        private System.Windows.Forms.RadioButton rdbPorCodigo;
        private System.Windows.Forms.Timer timerBusqueda;
        private System.Windows.Forms.CheckBox chkAutoBusqueda;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.Misc.UltraPanel frmBusquedaCIE10_Fill_Panel;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmBusquedaCIE10_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmBusquedaCIE10_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmBusquedaCIE10_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmBusquedaCIE10_UltraFormManager_Dock_Area_Bottom;
    }
}