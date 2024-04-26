namespace His.Honorarios
{
    partial class frmListas
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
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.mnuContexsecundario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.frmListas_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this._frmListas_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmListas_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmListas_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmListas_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.mnuContexsecundario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.frmListas_Fill_Panel.ClientArea.SuspendLayout();
            this.frmListas_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewList.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewList.MultiSelect = false;
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            this.dataGridViewList.Size = new System.Drawing.Size(848, 306);
            this.dataGridViewList.TabIndex = 0;
            this.dataGridViewList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewList_ColumnHeaderMouseClick);
            this.dataGridViewList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewList_KeyDown);
            this.dataGridViewList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewList_CellContentClick);
            // 
            // mnuContexsecundario
            // 
            this.mnuContexsecundario.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBuscar,
            this.toolStripSeparator1,
            this.cancelarToolStripMenuItem});
            this.mnuContexsecundario.Name = "mnuContext";
            this.mnuContexsecundario.Size = new System.Drawing.Size(128, 54);
            // 
            // mnuBuscar
            // 
            this.mnuBuscar.Name = "mnuBuscar";
            this.mnuBuscar.Size = new System.Drawing.Size(127, 22);
            this.mnuBuscar.Text = "Buscar";
            this.mnuBuscar.Click += new System.EventHandler(this.mnuBuscar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // cancelarToolStripMenuItem
            // 
            this.cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            this.cancelarToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.cancelarToolStripMenuItem.Text = "Cancelar";
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // frmListas_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frmListas_Fill_Panel.Appearance = appearance1;
            // 
            // frmListas_Fill_Panel.ClientArea
            // 
            this.frmListas_Fill_Panel.ClientArea.Controls.Add(this.dataGridViewList);
            this.frmListas_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frmListas_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmListas_Fill_Panel.Location = new System.Drawing.Point(4, 24);
            this.frmListas_Fill_Panel.Name = "frmListas_Fill_Panel";
            this.frmListas_Fill_Panel.Size = new System.Drawing.Size(848, 306);
            this.frmListas_Fill_Panel.TabIndex = 1;
            // 
            // _frmListas_UltraFormManager_Dock_Area_Left
            // 
            this._frmListas_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmListas_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmListas_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frmListas_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmListas_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frmListas_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frmListas_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 24);
            this._frmListas_UltraFormManager_Dock_Area_Left.Name = "_frmListas_UltraFormManager_Dock_Area_Left";
            this._frmListas_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 306);
            // 
            // _frmListas_UltraFormManager_Dock_Area_Right
            // 
            this._frmListas_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmListas_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmListas_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frmListas_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmListas_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frmListas_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frmListas_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(852, 24);
            this._frmListas_UltraFormManager_Dock_Area_Right.Name = "_frmListas_UltraFormManager_Dock_Area_Right";
            this._frmListas_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 306);
            // 
            // _frmListas_UltraFormManager_Dock_Area_Top
            // 
            this._frmListas_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmListas_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmListas_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frmListas_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmListas_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._frmListas_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frmListas_UltraFormManager_Dock_Area_Top.Name = "_frmListas_UltraFormManager_Dock_Area_Top";
            this._frmListas_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(856, 24);
            // 
            // _frmListas_UltraFormManager_Dock_Area_Bottom
            // 
            this._frmListas_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmListas_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 330);
            this._frmListas_UltraFormManager_Dock_Area_Bottom.Name = "_frmListas_UltraFormManager_Dock_Area_Bottom";
            this._frmListas_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(856, 4);
            // 
            // frmListas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(856, 334);
            this.Controls.Add(this.frmListas_Fill_Panel);
            this.Controls.Add(this._frmListas_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frmListas_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frmListas_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frmListas_UltraFormManager_Dock_Area_Bottom);
            this.Name = "frmListas";
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.frmListas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.mnuContexsecundario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.frmListas_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frmListas_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.ContextMenuStrip mnuContexsecundario;
        private System.Windows.Forms.ToolStripMenuItem mnuBuscar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.Misc.UltraPanel frmListas_Fill_Panel;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmListas_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmListas_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmListas_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmListas_UltraFormManager_Dock_Area_Bottom;
    }
}