using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Reportes;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using Infragistics;
using His.General;
using System.Reflection;
using His.Parametros;
using His.DatosReportes;
using System.Runtime.InteropServices;

namespace His.Admision
{
    public partial class frmReportaGarantias : Form
    {
        public frmReportaGarantias()
        {
            InitializeComponent();
        }

        private void btnImprime_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                DataTable ds1 = new DataTable();
                dtsAdmision ds2 = new dtsAdmision();
                DataRow dr2;
                ds1 = NegAtencionDetalleGarantias.SPRecuperaGarantiaFecha(txtFechaInicio.Text,txtFechaFin.Text);
                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    DataTable ds3 = new DataTable();

                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["GarantiaBloque"].NewRow();

                        dr2["Path"] = NegUtilitarios.RutaLogo("General");
                        dr2["NombrePaciente"] = dr1["NOMBRE"];
                        dr2["FechaGarantia"] = dr1["FECHA"];
                        dr2["TipoGarantia"] = dr1["TG_NOMBRE"];
                        dr2["ValorGarantia"] = dr1["ADG_VALOR"];
                        dr2["ObservacionGarantia"] = dr1["ADG_DESCRIPCION"];
                        dr2["Responsable"] = dr1["CAJERO"];
                        ds2.Tables["GarantiaBloque"].Rows.Add(dr2);
                    }
                    MessageBox.Show("SE IMPRIMIRA INFORME DE GARANTIAS.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    GarantiasBloque crgarantia = new GarantiasBloque();
                    crgarantia.SetDataSource(ds2);
                    CrystalDecisions.Windows.Forms.CrystalReportViewer vista = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    vista.ReportSource = crgarantia;
                    vista.PrintReport();
                }
                else
                    MessageBox.Show("SISTEMA NO CUENTA CON GARANTIAS PARA EL PERIODO ESCOGIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
