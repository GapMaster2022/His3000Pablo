namespace His.Dietetica
{
    partial class frmQuirofanoPedidoImprimir
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
            this.ImprimirPedido = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DatosQuirofanoPedidoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DatosQuirofanoPedidoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ImprimirPedido
            // 
            this.ImprimirPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DatosQuirofanoPedidoBindingSource;
            this.ImprimirPedido.LocalReport.DataSources.Add(reportDataSource1);
            this.ImprimirPedido.LocalReport.ReportEmbeddedResource = "His.Dietetica.ImprimirPedido.rdlc";
            this.ImprimirPedido.Location = new System.Drawing.Point(0, 0);
            this.ImprimirPedido.Name = "ImprimirPedido";
            this.ImprimirPedido.Size = new System.Drawing.Size(731, 365);
            this.ImprimirPedido.TabIndex = 0;
            // 
            // DatosQuirofanoPedidoBindingSource
            // 
            this.DatosQuirofanoPedidoBindingSource.DataSource = typeof(His.Dietetica.DatosQuirofanoPedido);
            // 
            // frmQuirofanoPedidoImprimir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 365);
            this.Controls.Add(this.ImprimirPedido);
            this.Name = "frmQuirofanoPedidoImprimir";
            this.Text = "Imprimir Pedido";
            this.Load += new System.EventHandler(this.frmQuirofanoPedidoImprimir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DatosQuirofanoPedidoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer ImprimirPedido;
        private System.Windows.Forms.BindingSource DatosQuirofanoPedidoBindingSource;
    }
}