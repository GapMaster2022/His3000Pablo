namespace TarifariosUI
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
            this.dataGridViewLista = new System.Windows.Forms.DataGridView();
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.frmListas_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this._frmListas_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmListas_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmListas_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmListas_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.frmListas_Fill_Panel.ClientArea.SuspendLayout();
            this.frmListas_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewLista
            // 
            this.dataGridViewLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLista.Location = new System.Drawing.Point(4, 4);
            this.dataGridViewLista.Name = "dataGridViewLista";
            this.dataGridViewLista.Size = new System.Drawing.Size(460, 288);
            this.dataGridViewLista.TabIndex = 0;
            this.dataGridViewLista.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewLista_ColumnHeaderMouseClick);
            this.dataGridViewLista.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLista_CellContentDoubleClick);
            this.dataGridViewLista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewLista_KeyDown);
            this.dataGridViewLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridViewLista_KeyPress);
            this.dataGridViewLista.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLista_CellContentClick);
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // frmListas_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.WhiteSmoke;
            appearance1.BackColor2 = System.Drawing.Color.LightGray;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.frmListas_Fill_Panel.Appearance = appearance1;
            // 
            // frmListas_Fill_Panel.ClientArea
            // 
            this.frmListas_Fill_Panel.ClientArea.Controls.Add(this.dataGridViewLista);
            this.frmListas_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frmListas_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmListas_Fill_Panel.Location = new System.Drawing.Point(4, 31);
            this.frmListas_Fill_Panel.Name = "frmListas_Fill_Panel";
            this.frmListas_Fill_Panel.Size = new System.Drawing.Size(468, 290);
            this.frmListas_Fill_Panel.TabIndex = 0;
            // 
            // _frmListas_UltraFormManager_Dock_Area_Left
            // 
            this._frmListas_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmListas_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmListas_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frmListas_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmListas_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frmListas_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frmListas_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 31);
            this._frmListas_UltraFormManager_Dock_Area_Left.Name = "_frmListas_UltraFormManager_Dock_Area_Left";
            this._frmListas_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 290);
            // 
            // _frmListas_UltraFormManager_Dock_Area_Right
            // 
            this._frmListas_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmListas_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmListas_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frmListas_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmListas_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frmListas_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frmListas_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(472, 31);
            this._frmListas_UltraFormManager_Dock_Area_Right.Name = "_frmListas_UltraFormManager_Dock_Area_Right";
            this._frmListas_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 290);
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
            this._frmListas_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(476, 31);
            // 
            // _frmListas_UltraFormManager_Dock_Area_Bottom
            // 
            this._frmListas_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmListas_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frmListas_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 321);
            this._frmListas_UltraFormManager_Dock_Area_Bottom.Name = "_frmListas_UltraFormManager_Dock_Area_Bottom";
            this._frmListas_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(476, 4);
            // 
            // frmListas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 325);
            this.Controls.Add(this.frmListas_Fill_Panel);
            this.Controls.Add(this._frmListas_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frmListas_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frmListas_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frmListas_UltraFormManager_Dock_Area_Bottom);
            this.MaximizeBox = false;
            this.Name = "frmListas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.frmListas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.frmListas_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frmListas_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewLista;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.Misc.UltraPanel frmListas_Fill_Panel;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmListas_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmListas_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmListas_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmListas_UltraFormManager_Dock_Area_Bottom;
    }
}