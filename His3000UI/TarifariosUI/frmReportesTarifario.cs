using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TarifariosUI.Reportes
{
    public partial class frmReportesTarifario : Form
    {
        public frmReportesTarifario()
        {
            InitializeComponent();
        }

        private void frmReportesTarifario_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hIS3000BDDataSet1.vTarifario_FacturaDetalle' table. You can move, or remove it, as needed.
            this.vTarifario_FacturaDetalleTableAdapter.Fill(this.hIS3000BDDataSet1.vTarifario_FacturaDetalle);
            // TODO: This line of code loads data into the 'hIS3000BDDataSet1.vTarifario_Factura' table. You can move, or remove it, as needed.
            this.vTarifario_FacturaTableAdapter.Fill(this.hIS3000BDDataSet1.vTarifario_Factura);

            this.reporteHonFactura.RefreshReport();
        }
    }
}
