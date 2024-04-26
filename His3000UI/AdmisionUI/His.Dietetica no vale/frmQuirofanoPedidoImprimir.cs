using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class frmQuirofanoPedidoImprimir : Form
    {
        public List<DatosQuirofanoPedido> ped = new List<DatosQuirofanoPedido>();
        public frmQuirofanoPedidoImprimir()
        {
            InitializeComponent();
        }

        private void frmQuirofanoPedidoImprimir_Load(object sender, EventArgs e)
        {
            ImprimirPedido.LocalReport.DataSources.Clear();
            ImprimirPedido.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ped));
            this.ImprimirPedido.RefreshReport();
        }
    }
}
