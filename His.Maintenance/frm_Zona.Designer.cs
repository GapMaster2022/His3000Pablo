namespace His.Maintenance
{
    partial class frm_Zona
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
            this.btn_salir = new System.Windows.Forms.Button();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevaZonaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContxtLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grid = new System.Windows.Forms.DataGridView();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.frm_Zona_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ultraSplitter1 = new Infragistics.Win.Misc.UltraSplitter();
            this.mnuContext.SuspendLayout();
            this.mnuContxtLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.frm_Zona_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_Zona_Fill_Panel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_salir
            // 
            this.btn_salir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_salir.Location = new System.Drawing.Point(314, 8);
            this.btn_salir.Name = "btn_salir";
            this.btn_salir.Size = new System.Drawing.Size(40, 25);
            this.btn_salir.TabIndex = 17;
            this.btn_salir.Text = "Salir";
            this.btn_salir.UseVisualStyleBackColor = true;
            this.btn_salir.Click += new System.EventHandler(this.btn_salir_Click);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaZonaToolStripMenuItem,
            this.toolStripSeparator1,
            this.cancelarToolStripMenuItem});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(144, 54);
            // 
            // nuevaZonaToolStripMenuItem
            // 
            this.nuevaZonaToolStripMenuItem.Name = "nuevaZonaToolStripMenuItem";
            this.nuevaZonaToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.nuevaZonaToolStripMenuItem.Text = "Nueva Zona";
            this.nuevaZonaToolStripMenuItem.Click += new System.EventHandler(this.nuevaZonaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // cancelarToolStripMenuItem
            // 
            this.cancelarToolStripMenuItem.Name = "cancelarToolStripMenuItem";
            this.cancelarToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.cancelarToolStripMenuItem.Text = "Cancelar";
            // 
            // mnuContxtLocal
            // 
            this.mnuContxtLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoLocalToolStripMenuItem,
            this.toolStripSeparator3,
            this.editaToolStripMenuItem,
            this.rToolStripMenuItem,
            this.toolStripSeparator2,
            this.cancelarToolStripMenuItem1});
            this.mnuContxtLocal.Name = "mnuContxtLocal";
            this.mnuContxtLocal.Size = new System.Drawing.Size(149, 104);
            // 
            // nuevoLocalToolStripMenuItem
            // 
            this.nuevoLocalToolStripMenuItem.Name = "nuevoLocalToolStripMenuItem";
            this.nuevoLocalToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.nuevoLocalToolStripMenuItem.Text = "Nuevo Local";
            this.nuevoLocalToolStripMenuItem.Click += new System.EventHandler(this.nuevoLocalToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // editaToolStripMenuItem
            // 
            this.editaToolStripMenuItem.Name = "editaToolStripMenuItem";
            this.editaToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.editaToolStripMenuItem.Text = "Edita Zona";
            this.editaToolStripMenuItem.Click += new System.EventHandler(this.editaToolStripMenuItem_Click);
            // 
            // rToolStripMenuItem
            // 
            this.rToolStripMenuItem.Name = "rToolStripMenuItem";
            this.rToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.rToolStripMenuItem.Text = "Eliminar Zona";
            this.rToolStripMenuItem.Visible = false;
            this.rToolStripMenuItem.Click += new System.EventHandler(this.rToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // cancelarToolStripMenuItem1
            // 
            this.cancelarToolStripMenuItem1.Name = "cancelarToolStripMenuItem1";
            this.cancelarToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.cancelarToolStripMenuItem1.Text = "Cancelar";
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.GridColor = System.Drawing.Color.LightBlue;
            this.grid.Location = new System.Drawing.Point(3, 3);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(220, 212);
            this.grid.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(109, 212);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // frm_Zona_Fill_Panel
            // 
            this.frm_Zona_Fill_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.LightGray;
            appearance1.BackColor2 = System.Drawing.Color.GhostWhite;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop37;
            this.frm_Zona_Fill_Panel.Appearance = appearance1;
            // 
            // frm_Zona_Fill_Panel.ClientArea
            // 
            this.frm_Zona_Fill_Panel.ClientArea.Controls.Add(this.splitContainer1);
            this.frm_Zona_Fill_Panel.ClientArea.Controls.Add(this.btn_salir);
            this.frm_Zona_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_Zona_Fill_Panel.Location = new System.Drawing.Point(0, 0);
            this.frm_Zona_Fill_Panel.Name = "frm_Zona_Fill_Panel";
            this.frm_Zona_Fill_Panel.Size = new System.Drawing.Size(369, 266);
            this.frm_Zona_Fill_Panel.TabIndex = 2;
            this.frm_Zona_Fill_Panel.PaintClient += new System.Windows.Forms.PaintEventHandler(this.frm_Zona_Fill_Panel_PaintClient);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ultraSplitter1);
            this.splitContainer1.Panel2.Controls.Add(this.grid);
            this.splitContainer1.Size = new System.Drawing.Size(345, 218);
            this.splitContainer1.SplitterDistance = 115;
            this.splitContainer1.TabIndex = 18;
            // 
            // ultraSplitter1
            // 
            this.ultraSplitter1.Location = new System.Drawing.Point(0, 0);
            this.ultraSplitter1.Name = "ultraSplitter1";
            this.ultraSplitter1.RestoreExtent = 0;
            this.ultraSplitter1.Size = new System.Drawing.Size(14, 218);
            this.ultraSplitter1.TabIndex = 5;
            // 
            // frm_Zona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(369, 266);
            this.Controls.Add(this.frm_Zona_Fill_Panel);
            this.Name = "frm_Zona";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zonas y Locales";
            this.Load += new System.EventHandler(this.frm_Zona_Load);
            this.mnuContext.ResumeLayout(false);
            this.mnuContxtLocal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.frm_Zona_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_Zona_Fill_Panel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_salir;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem nuevaZonaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mnuContxtLocal;
        private System.Windows.Forms.ToolStripMenuItem nuevoLocalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.TreeView treeView1;
        private Infragistics.Win.Misc.UltraPanel frm_Zona_Fill_Panel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Infragistics.Win.Misc.UltraSplitter ultraSplitter1;
    }
}