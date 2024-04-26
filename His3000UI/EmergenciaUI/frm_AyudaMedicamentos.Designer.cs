namespace His.Emergencia
{
    partial class frm_AyudaMedicamentos
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
            this.pRODCODIGODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRODNOMBREDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pRODUCTOSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.hIS3000BDDataSetProductos = new His.Emergencia.HIS3000BDDataSetProductos();
            this.pRODUCTOSTableAdapter = new His.Emergencia.HIS3000BDDataSetProductosTableAdapters.PRODUCTOSTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRODUCTOSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hIS3000BDDataSetProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoGenerateColumns = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pRODCODIGODataGridViewTextBoxColumn,
            this.pRODNOMBREDataGridViewTextBoxColumn});
            this.grid.DataSource = this.pRODUCTOSBindingSource;
            this.grid.Location = new System.Drawing.Point(-1, 0);
            this.grid.Name = "grid";
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(246, 192);
            this.grid.TabIndex = 1;
            this.grid.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_RowHeaderMouseDoubleClick);
            // 
            // pRODCODIGODataGridViewTextBoxColumn
            // 
            this.pRODCODIGODataGridViewTextBoxColumn.DataPropertyName = "PROD_CODIGO";
            this.pRODCODIGODataGridViewTextBoxColumn.HeaderText = "CODIGO";
            this.pRODCODIGODataGridViewTextBoxColumn.Name = "pRODCODIGODataGridViewTextBoxColumn";
            // 
            // pRODNOMBREDataGridViewTextBoxColumn
            // 
            this.pRODNOMBREDataGridViewTextBoxColumn.DataPropertyName = "PROD_NOMBRE";
            this.pRODNOMBREDataGridViewTextBoxColumn.HeaderText = "NOMBRE";
            this.pRODNOMBREDataGridViewTextBoxColumn.Name = "pRODNOMBREDataGridViewTextBoxColumn";
            // 
            // pRODUCTOSBindingSource
            // 
            this.pRODUCTOSBindingSource.DataMember = "PRODUCTOS";
            this.pRODUCTOSBindingSource.DataSource = this.hIS3000BDDataSetProductos;
            // 
            // hIS3000BDDataSetProductos
            // 
            this.hIS3000BDDataSetProductos.DataSetName = "HIS3000BDDataSetProductos";
            this.hIS3000BDDataSetProductos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pRODUCTOSTableAdapter
            // 
            this.pRODUCTOSTableAdapter.ClearBeforeFill = true;
            // 
            // frm_AyudaMedicamentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 188);
            this.Controls.Add(this.grid);
            this.Name = "frm_AyudaMedicamentos";
            this.Text = "frm_AyudaMedicamentos";
            this.Load += new System.EventHandler(this.frm_AyudaMedicamentos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pRODUCTOSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hIS3000BDDataSetProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private HIS3000BDDataSetProductos hIS3000BDDataSetProductos;
        private System.Windows.Forms.BindingSource pRODUCTOSBindingSource;
        private His.Emergencia.HIS3000BDDataSetProductosTableAdapters.PRODUCTOSTableAdapter pRODUCTOSTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRODCODIGODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pRODNOMBREDataGridViewTextBoxColumn;
    }
}