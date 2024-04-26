namespace TarifariosUI.Reportes
{
    partial class frmReportesTarifario
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.vTarifarioFacturavTarifarioFacturaDetalleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vTarifarioFacturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.hIS3000BDDataSet1 = new TarifariosUI.HIS3000BDDataSet();
            this.vTarifarioFacturaBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.hIS3000BDDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reporteHonFactura = new Microsoft.Reporting.WinForms.ReportViewer();
            this.vTarifario_FacturaTableAdapter = new TarifariosUI.HIS3000BDDataSetTableAdapters.vTarifario_FacturaTableAdapter();
            this.vTarifario_FacturaDetalleTableAdapter = new TarifariosUI.HIS3000BDDataSetTableAdapters.vTarifario_FacturaDetalleTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.vTarifarioFacturavTarifarioFacturaDetalleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vTarifarioFacturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hIS3000BDDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vTarifarioFacturaBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hIS3000BDDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // vTarifarioFacturavTarifarioFacturaDetalleBindingSource
            // 
            this.vTarifarioFacturavTarifarioFacturaDetalleBindingSource.DataMember = "vTarifario_Factura_vTarifario_FacturaDetalle";
            this.vTarifarioFacturavTarifarioFacturaDetalleBindingSource.DataSource = this.vTarifarioFacturaBindingSource;
            // 
            // vTarifarioFacturaBindingSource
            // 
            this.vTarifarioFacturaBindingSource.DataMember = "vTarifario_Factura";
            this.vTarifarioFacturaBindingSource.DataSource = this.hIS3000BDDataSet1;
            // 
            // hIS3000BDDataSet1
            // 
            this.hIS3000BDDataSet1.DataSetName = "HIS3000BDDataSet";
            this.hIS3000BDDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vTarifarioFacturaBindingSource1
            // 
            this.vTarifarioFacturaBindingSource1.DataMember = "vTarifario_Factura";
            this.vTarifarioFacturaBindingSource1.DataSource = this.hIS3000BDDataSet1;
            // 
            // reporteHonFactura
            // 
            this.reporteHonFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "HIS3000BDDataSet_vTarifario_FacturaDetalle";
            reportDataSource1.Value = this.hIS3000BDDataSetBindingSource;
            reportDataSource2.Name = "HIS3000BDDataSet_vTarifario_Factura";
            reportDataSource2.Value = this.hIS3000BDDataSetBindingSource;
            this.reporteHonFactura.LocalReport.DataSources.Add(reportDataSource1);
            this.reporteHonFactura.LocalReport.DataSources.Add(reportDataSource2);
            this.reporteHonFactura.LocalReport.ReportEmbeddedResource = "TarifariosUI.TarifarioFactura.rdlc";
            this.reporteHonFactura.Location = new System.Drawing.Point(0, 0);
            this.reporteHonFactura.Name = "reporteHonFactura";
            this.reporteHonFactura.Size = new System.Drawing.Size(779, 373);
            this.reporteHonFactura.TabIndex = 0;
            // 
            // vTarifario_FacturaTableAdapter
            // 
            this.vTarifario_FacturaTableAdapter.ClearBeforeFill = true;
            // 
            // vTarifario_FacturaDetalleTableAdapter
            // 
            this.vTarifario_FacturaDetalleTableAdapter.ClearBeforeFill = true;
            // 
            // frmReportesTarifario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 373);
            this.Controls.Add(this.reporteHonFactura);
            this.Name = "frmReportesTarifario";
            this.Load += new System.EventHandler(this.frmReportesTarifario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vTarifarioFacturavTarifarioFacturaDetalleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vTarifarioFacturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hIS3000BDDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vTarifarioFacturaBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hIS3000BDDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reporteHonFactura;
        private System.Windows.Forms.BindingSource hIS3000BDDataSetBindingSource;
        private HIS3000BDDataSet hIS3000BDDataSet;
        private HIS3000BDDataSet hIS3000BDDataSet1;
        private System.Windows.Forms.BindingSource vTarifarioFacturaBindingSource;
        private TarifariosUI.HIS3000BDDataSetTableAdapters.vTarifario_FacturaTableAdapter vTarifario_FacturaTableAdapter;
        private System.Windows.Forms.BindingSource vTarifarioFacturavTarifarioFacturaDetalleBindingSource;
        private TarifariosUI.HIS3000BDDataSetTableAdapters.vTarifario_FacturaDetalleTableAdapter vTarifario_FacturaDetalleTableAdapter;
        private System.Windows.Forms.BindingSource vTarifarioFacturaBindingSource1;
    }
}