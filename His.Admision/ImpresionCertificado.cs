using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace His.Admision
{
    public partial class ImpresionCertificado : Form
    {
        public List<CertificadoMedico> certificadoMedico = new List<CertificadoMedico>();
        public ImpresionCertificado()
        {
            InitializeComponent();
        }

        private void ImpresionCertificado_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", certificadoMedico));
            this.reportViewer1.RefreshReport();
        }
    }
}
