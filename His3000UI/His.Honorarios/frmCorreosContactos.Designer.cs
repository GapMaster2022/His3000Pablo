namespace His.Honorarios
{
    partial class frmCorreosContactos
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
            this.menuPanel = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cboCategoria = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.listaContactosPanel = new System.Windows.Forms.Panel();
            this.dbdgListaContactos = new System.Windows.Forms.DataGridView();
            this.btnTo = new System.Windows.Forms.Button();
            this.btnCC = new System.Windows.Forms.Button();
            this.btnCCO = new System.Windows.Forms.Button();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.txtCC = new System.Windows.Forms.TextBox();
            this.txtCCO = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.infToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.frmCorreosContactos_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.menuPanel.SuspendLayout();
            this.listaContactosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbdgListaContactos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.frmCorreosContactos_Fill_Panel.ClientArea.SuspendLayout();
            this.frmCorreosContactos_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuPanel.Controls.Add(this.btnBuscar);
            this.menuPanel.Controls.Add(this.txtBuscar);
            this.menuPanel.Controls.Add(this.lblBuscar);
            this.menuPanel.Controls.Add(this.lblCategoria);
            this.menuPanel.Controls.Add(this.cboCategoria);
            this.menuPanel.Controls.Add(this.lblTipo);
            this.menuPanel.Controls.Add(this.cboTipo);
            this.menuPanel.Location = new System.Drawing.Point(7, 4);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(634, 64);
            this.menuPanel.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBuscar.Location = new System.Drawing.Point(231, 34);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(24, 20);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = "->";
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.infToolTip.SetToolTip(this.btnBuscar, "Filtra el contenido de la lista de acuerdo al texto ingresado");
            this.btnBuscar.UseVisualStyleBackColor = false;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(114, 35);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(111, 20);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.Text = "Ingrese el texto a buscar";
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(65, 38);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(43, 13);
            this.lblBuscar.TabIndex = 4;
            this.lblBuscar.Text = "Buscar:";
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(279, 13);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(100, 13);
            this.lblCategoria.TabIndex = 3;
            this.lblCategoria.Text = "Lista de Categorias:";
            // 
            // cboCategoria
            // 
            this.cboCategoria.FormattingEnabled = true;
            this.cboCategoria.Location = new System.Drawing.Point(385, 10);
            this.cboCategoria.Name = "cboCategoria";
            this.cboCategoria.Size = new System.Drawing.Size(240, 21);
            this.cboCategoria.TabIndex = 1;
            this.infToolTip.SetToolTip(this.cboCategoria, "Clasificación de los Contactos");
            this.cboCategoria.SelectedIndexChanged += new System.EventHandler(this.cboCategoria_SelectedIndexChanged);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(11, 11);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(97, 13);
            this.lblTipo.TabIndex = 1;
            this.lblTipo.Text = "Tipo de Contactos:";
            // 
            // cboTipo
            // 
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.Location = new System.Drawing.Point(114, 8);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(141, 21);
            this.cboTipo.TabIndex = 0;
            this.infToolTip.SetToolTip(this.cboTipo, "Tipos de listas existentes en el sistema");
            this.cboTipo.SelectedIndexChanged += new System.EventHandler(this.cboTipo_SelectedIndexChanged);
            this.cboTipo.DisplayMemberChanged += new System.EventHandler(this.cboTipo_DisplayMemberChanged);
            // 
            // listaContactosPanel
            // 
            this.listaContactosPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listaContactosPanel.Controls.Add(this.dbdgListaContactos);
            this.listaContactosPanel.Location = new System.Drawing.Point(7, 74);
            this.listaContactosPanel.Name = "listaContactosPanel";
            this.listaContactosPanel.Size = new System.Drawing.Size(634, 183);
            this.listaContactosPanel.TabIndex = 1;
            // 
            // dbdgListaContactos
            // 
            this.dbdgListaContactos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbdgListaContactos.Location = new System.Drawing.Point(5, 20);
            this.dbdgListaContactos.Name = "dbdgListaContactos";
            this.dbdgListaContactos.RowHeadersVisible = false;
            this.dbdgListaContactos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dbdgListaContactos.Size = new System.Drawing.Size(626, 160);
            this.dbdgListaContactos.TabIndex = 0;
            this.dbdgListaContactos.DoubleClick += new System.EventHandler(this.dbdgListaContactos_DoubleClick);
            this.dbdgListaContactos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbdgListaContactos_CellContentClick);
            // 
            // btnTo
            // 
            this.btnTo.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnTo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTo.Location = new System.Drawing.Point(14, 263);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(65, 21);
            this.btnTo.TabIndex = 0;
            this.btnTo.Text = "Para: ";
            this.btnTo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.infToolTip.SetToolTip(this.btnTo, "Añade ");
            this.btnTo.UseVisualStyleBackColor = false;
            this.btnTo.Click += new System.EventHandler(this.btnTo_Click);
            // 
            // btnCC
            // 
            this.btnCC.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCC.Location = new System.Drawing.Point(14, 290);
            this.btnCC.Name = "btnCC";
            this.btnCC.Size = new System.Drawing.Size(65, 21);
            this.btnCC.TabIndex = 2;
            this.btnCC.Text = "CC:";
            this.btnCC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCC.UseVisualStyleBackColor = false;
            this.btnCC.Click += new System.EventHandler(this.btnCC_Click);
            // 
            // btnCCO
            // 
            this.btnCCO.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCCO.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCCO.Location = new System.Drawing.Point(14, 317);
            this.btnCCO.Name = "btnCCO";
            this.btnCCO.Size = new System.Drawing.Size(65, 21);
            this.btnCCO.TabIndex = 4;
            this.btnCCO.Text = "CCO:";
            this.btnCCO.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCCO.UseVisualStyleBackColor = false;
            this.btnCCO.Click += new System.EventHandler(this.btnCCO_Click);
            // 
            // txtTo
            // 
            this.txtTo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(85, 263);
            this.txtTo.Name = "txtTo";
            this.txtTo.ReadOnly = true;
            this.txtTo.Size = new System.Drawing.Size(486, 21);
            this.txtTo.TabIndex = 1;
            // 
            // txtCC
            // 
            this.txtCC.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCC.Location = new System.Drawing.Point(85, 290);
            this.txtCC.Name = "txtCC";
            this.txtCC.ReadOnly = true;
            this.txtCC.Size = new System.Drawing.Size(486, 21);
            this.txtCC.TabIndex = 3;
            // 
            // txtCCO
            // 
            this.txtCCO.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCCO.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCO.Location = new System.Drawing.Point(85, 317);
            this.txtCCO.Name = "txtCCO";
            this.txtCCO.ReadOnly = true;
            this.txtCCO.Size = new System.Drawing.Size(486, 21);
            this.txtCCO.TabIndex = 5;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAceptar.Location = new System.Drawing.Point(469, 344);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(83, 24);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancelar.Location = new System.Drawing.Point(558, 344);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(83, 24);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // frmCorreosContactos_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frmCorreosContactos_Fill_Panel.Appearance = appearance1;
            // 
            // frmCorreosContactos_Fill_Panel.ClientArea
            // 
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.btnCancelar);
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.btnAceptar);
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.txtCCO);
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.txtCC);
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.txtTo);
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.btnCCO);
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.btnCC);
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.btnTo);
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.listaContactosPanel);
            this.frmCorreosContactos_Fill_Panel.ClientArea.Controls.Add(this.menuPanel);
            this.frmCorreosContactos_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frmCorreosContactos_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmCorreosContactos_Fill_Panel.Location = new System.Drawing.Point(4, 31);
            this.frmCorreosContactos_Fill_Panel.Name = "frmCorreosContactos_Fill_Panel";
            this.frmCorreosContactos_Fill_Panel.Size = new System.Drawing.Size(649, 367);
            this.frmCorreosContactos_Fill_Panel.TabIndex = 0;
            // 
            // _frmCorreosContactos_UltraFormManager_Dock_Area_Left
            // 
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 31);
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left.Name = "_frmCorreosContactos_UltraFormManager_Dock_Area_Left";
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 367);
            // 
            // _frmCorreosContactos_UltraFormManager_Dock_Area_Right
            // 
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(653, 31);
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right.Name = "_frmCorreosContactos_UltraFormManager_Dock_Area_Right";
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 367);
            // 
            // _frmCorreosContactos_UltraFormManager_Dock_Area_Top
            // 
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Top.Name = "_frmCorreosContactos_UltraFormManager_Dock_Area_Top";
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(657, 31);
            // 
            // _frmCorreosContactos_UltraFormManager_Dock_Area_Bottom
            // 
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 398);
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom.Name = "_frmCorreosContactos_UltraFormManager_Dock_Area_Bottom";
            this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(657, 4);
            // 
            // frmCorreosContactos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(657, 402);
            this.Controls.Add(this.frmCorreosContactos_Fill_Panel);
            this.Controls.Add(this._frmCorreosContactos_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frmCorreosContactos_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frmCorreosContactos_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frmCorreosContactos_UltraFormManager_Dock_Area_Bottom);
            this.MaximizeBox = false;
            this.Name = "frmCorreosContactos";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccionar Contactos";
            this.Load += new System.EventHandler(this.frmCorreosContactos_Load);
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            this.listaContactosPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbdgListaContactos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.frmCorreosContactos_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frmCorreosContactos_Fill_Panel.ClientArea.PerformLayout();
            this.frmCorreosContactos_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Panel listaContactosPanel;
        private System.Windows.Forms.Button btnTo;
        private System.Windows.Forms.Button btnCC;
        private System.Windows.Forms.Button btnCCO;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.TextBox txtCC;
        private System.Windows.Forms.TextBox txtCCO;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cboCategoria;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.ToolTip infToolTip;
        private System.Windows.Forms.DataGridView dbdgListaContactos;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.Misc.UltraPanel frmCorreosContactos_Fill_Panel;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmCorreosContactos_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmCorreosContactos_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmCorreosContactos_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frmCorreosContactos_UltraFormManager_Dock_Area_Bottom;
    }
}