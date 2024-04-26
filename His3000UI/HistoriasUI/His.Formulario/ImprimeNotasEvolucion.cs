using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace His.Formulario
{     
    public partial class ImprimeNotasEvolucion : Form
    {
        public List<NotaEvoluciones> notas = new List<NotaEvoluciones>();
        public ImprimeNotasEvolucion()
        {
            InitializeComponent();
        }

        private void ImprimeNotasEvolucion_Load(object sender, EventArgs e)
        {
            NotasIndividualesMedicos cr1 = new NotasIndividualesMedicos();
            //CrystalReportViewer vista = new CrystalReportViewer();
            crystalReportViewer1.ReportSource = cr1;
            //vista.ReportSource = cr1;
            //vista.PrintReport();
            //reportViewer1.LocalReport.DataSources.Clear();
            //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1",notas));
            //this.reportViewer1.RefreshReport();
        }
    }
}
