namespace His.Honorarios
{
    partial class frmCorreosEntrantes
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.wbTextEmail = new System.Windows.Forms.WebBrowser();
            this.wbHTMLEmail = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridEmails = new System.Windows.Forms.DataGridView();
            this.colReceivedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReceivedFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessageId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.accountsTree = new System.Windows.Forms.TreeView();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.getSelectedMailAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSelectedUser = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cuentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEmailAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmails)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.wbTextEmail);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(301, 282);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Formato Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // wbTextEmail
            // 
            this.wbTextEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbTextEmail.Location = new System.Drawing.Point(3, 3);
            this.wbTextEmail.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbTextEmail.Name = "wbTextEmail";
            this.wbTextEmail.Size = new System.Drawing.Size(295, 276);
            this.wbTextEmail.TabIndex = 1;
            // 
            // wbHTMLEmail
            // 
            this.wbHTMLEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbHTMLEmail.Location = new System.Drawing.Point(3, 3);
            this.wbHTMLEmail.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbHTMLEmail.Name = "wbHTMLEmail";
            this.wbHTMLEmail.Size = new System.Drawing.Size(408, 362);
            this.wbHTMLEmail.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(422, 394);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.wbHTMLEmail);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(414, 368);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Formato HTML";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridEmails
            // 
            this.gridEmails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEmails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colReceivedDate,
            this.colSubject,
            this.colReceivedFrom,
            this.colMessageId});
            this.gridEmails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEmails.Location = new System.Drawing.Point(0, 0);
            this.gridEmails.Name = "gridEmails";
            this.gridEmails.Size = new System.Drawing.Size(211, 394);
            this.gridEmails.TabIndex = 3;
            this.gridEmails.SelectionChanged += new System.EventHandler(this.gridEmails_SelectionChanged);
            // 
            // colReceivedDate
            // 
            this.colReceivedDate.HeaderText = "Fecha de Recepción";
            this.colReceivedDate.Name = "colReceivedDate";
            this.colReceivedDate.ReadOnly = true;
            // 
            // colSubject
            // 
            this.colSubject.HeaderText = "Asunto";
            this.colSubject.Name = "colSubject";
            this.colSubject.ReadOnly = true;
            // 
            // colReceivedFrom
            // 
            this.colReceivedFrom.HeaderText = "De";
            this.colReceivedFrom.Name = "colReceivedFrom";
            this.colReceivedFrom.ReadOnly = true;
            // 
            // colMessageId
            // 
            this.colMessageId.HeaderText = "Id";
            this.colMessageId.Name = "colMessageId";
            this.colMessageId.ReadOnly = true;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(193, 59);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.gridEmails);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.tabControl1);
            this.mainSplitContainer.Size = new System.Drawing.Size(637, 394);
            this.mainSplitContainer.SplitterDistance = 211;
            this.mainSplitContainer.TabIndex = 8;
            // 
            // accountsTree
            // 
            this.accountsTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.accountsTree.Location = new System.Drawing.Point(0, 59);
            this.accountsTree.Name = "accountsTree";
            this.accountsTree.Size = new System.Drawing.Size(193, 394);
            this.accountsTree.TabIndex = 7;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.toolStripSeparator1,
            this.getSelectedMailAccountToolStripMenuItem,
            this.getToolStripMenuItem,
            this.toolStripSeparator2});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.fileToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo Correo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(264, 6);
            // 
            // getSelectedMailAccountToolStripMenuItem
            // 
            this.getSelectedMailAccountToolStripMenuItem.Name = "getSelectedMailAccountToolStripMenuItem";
            this.getSelectedMailAccountToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.getSelectedMailAccountToolStripMenuItem.Text = "Recuperarde una cuenta determinada";
            this.getSelectedMailAccountToolStripMenuItem.Click += new System.EventHandler(this.getSelectedMailAccountToolStripMenuItem_Click);
            // 
            // getToolStripMenuItem
            // 
            this.getToolStripMenuItem.Name = "getToolStripMenuItem";
            this.getToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.getToolStripMenuItem.Text = "Recuperar todos";
            this.getToolStripMenuItem.Click += new System.EventHandler(this.gridEmails_SelectionChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(264, 6);
            // 
            // lblSelectedUser
            // 
            this.lblSelectedUser.AutoSize = true;
            this.lblSelectedUser.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedUser.ForeColor = System.Drawing.Color.White;
            this.lblSelectedUser.Location = new System.Drawing.Point(102, 10);
            this.lblSelectedUser.Name = "lblSelectedUser";
            this.lblSelectedUser.Size = new System.Drawing.Size(15, 13);
            this.lblSelectedUser.TabIndex = 1;
            this.lblSelectedUser.Text = "..";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.lblSelectedUser);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(830, 35);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.cuentasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(830, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cuentasToolStripMenuItem
            // 
            this.cuentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEmailAccountToolStripMenuItem});
            this.cuentasToolStripMenuItem.Name = "cuentasToolStripMenuItem";
            this.cuentasToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.cuentasToolStripMenuItem.Text = "Cuentas";
            // 
            // addEmailAccountToolStripMenuItem
            // 
            this.addEmailAccountToolStripMenuItem.Name = "addEmailAccountToolStripMenuItem";
            this.addEmailAccountToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.addEmailAccountToolStripMenuItem.Text = "Añadir Cuenta";
            this.addEmailAccountToolStripMenuItem.Click += new System.EventHandler(this.addEmailAccountToolStripMenuItem_Click);
            // 
            // frmCorreosEntrantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 453);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.accountsTree);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frmCorreosEntrantes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCorreosEntrantes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCorreosEntrantes_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridEmails)).EndInit();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            this.mainSplitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.WebBrowser wbTextEmail;
        private System.Windows.Forms.WebBrowser wbHTMLEmail;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gridEmails;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TreeView accountsTree;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getSelectedMailAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getToolStripMenuItem;
        private System.Windows.Forms.Label lblSelectedUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReceivedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReceivedFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessageId;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEmailAccountToolStripMenuItem;
    }
}