namespace His.Emergencia
{
    partial class frm_AyudaMedico
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.hIS3000BDDataSet = new His.Emergencia.HIS3000BDDataSet();
            this.mEDICOSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mEDICOSTableAdapter = new His.Emergencia.HIS3000BDDataSetTableAdapters.MEDICOSTableAdapter();
            this.mEDCODIGODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mEDNOMBRE1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mEDNOMBRE2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mEDAPELLIDOPATERNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mEDAPELLIDOMATERNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hIS3000BDDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mEDICOSBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoGenerateColumns = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mEDCODIGODataGridViewTextBoxColumn,
            this.mEDNOMBRE1DataGridViewTextBoxColumn,
            this.mEDNOMBRE2DataGridViewTextBoxColumn,
            this.mEDAPELLIDOPATERNODataGridViewTextBoxColumn,
            this.mEDAPELLIDOMATERNODataGridViewTextBoxColumn});
            this.grid.DataSource = this.mEDICOSBindingSource;
            this.grid.Location = new System.Drawing.Point(-2, -1);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(541, 235);
            this.grid.TabIndex = 2;
            this.grid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_RowHeaderMouseDoubleClick);
            // 
            // hIS3000BDDataSet
            // 
            this.hIS3000BDDataSet.DataSetName = "HIS3000BDDataSet";
            this.hIS3000BDDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mEDICOSBindingSource
            // 
            this.mEDICOSBindingSource.DataMember = "MEDICOS";
            this.mEDICOSBindingSource.DataSource = this.hIS3000BDDataSet;
            // 
            // mEDICOSTableAdapter
            // 
            this.mEDICOSTableAdapter.ClearBeforeFill = true;
            // 
            // mEDCODIGODataGridViewTextBoxColumn
            // 
            this.mEDCODIGODataGridViewTextBoxColumn.DataPropertyName = "MED_CODIGO";
            this.mEDCODIGODataGridViewTextBoxColumn.HeaderText = "CODIGO";
            this.mEDCODIGODataGridViewTextBoxColumn.Name = "mEDCODIGODataGridViewTextBoxColumn";
            this.mEDCODIGODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mEDNOMBRE1DataGridViewTextBoxColumn
            // 
            this.mEDNOMBRE1DataGridViewTextBoxColumn.DataPropertyName = "MED_NOMBRE1";
            this.mEDNOMBRE1DataGridViewTextBoxColumn.HeaderText = "NOMBRE";
            this.mEDNOMBRE1DataGridViewTextBoxColumn.Name = "mEDNOMBRE1DataGridViewTextBoxColumn";
            this.mEDNOMBRE1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mEDNOMBRE2DataGridViewTextBoxColumn
            // 
            this.mEDNOMBRE2DataGridViewTextBoxColumn.DataPropertyName = "MED_NOMBRE2";
            this.mEDNOMBRE2DataGridViewTextBoxColumn.HeaderText = "NOMBRE";
            this.mEDNOMBRE2DataGridViewTextBoxColumn.Name = "mEDNOMBRE2DataGridViewTextBoxColumn";
            this.mEDNOMBRE2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mEDAPELLIDOPATERNODataGridViewTextBoxColumn
            // 
            this.mEDAPELLIDOPATERNODataGridViewTextBoxColumn.DataPropertyName = "MED_APELLIDO_PATERNO";
            this.mEDAPELLIDOPATERNODataGridViewTextBoxColumn.HeaderText = "APELLIDO_PATERNO";
            this.mEDAPELLIDOPATERNODataGridViewTextBoxColumn.Name = "mEDAPELLIDOPATERNODataGridViewTextBoxColumn";
            this.mEDAPELLIDOPATERNODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mEDAPELLIDOMATERNODataGridViewTextBoxColumn
            // 
            this.mEDAPELLIDOMATERNODataGridViewTextBoxColumn.DataPropertyName = "MED_APELLIDO_MATERNO";
            this.mEDAPELLIDOMATERNODataGridViewTextBoxColumn.HeaderText = "APELLIDO_MATERNO";
            this.mEDAPELLIDOMATERNODataGridViewTextBoxColumn.Name = "mEDAPELLIDOMATERNODataGridViewTextBoxColumn";
            this.mEDAPELLIDOMATERNODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // frm_AyudaMedico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 231);
            this.Controls.Add(this.grid);
            this.Name = "frm_AyudaMedico";
            this.Text = "frm_AyudaMedico";
            this.Load += new System.EventHandler(this.frm_AyudaMedico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hIS3000BDDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mEDICOSBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private HIS3000BDDataSet hIS3000BDDataSet;
        private System.Windows.Forms.BindingSource mEDICOSBindingSource;
        private His.Emergencia.HIS3000BDDataSetTableAdapters.MEDICOSTableAdapter mEDICOSTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn mEDCODIGODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mEDNOMBRE1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mEDNOMBRE2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mEDAPELLIDOPATERNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mEDAPELLIDOMATERNODataGridViewTextBoxColumn;
    }
}