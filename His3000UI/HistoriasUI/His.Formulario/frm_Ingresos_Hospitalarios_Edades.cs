using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.Formulario
{
    public partial class frm_Ingresos_Hospitalarios_Edades : Form
    {
        NegReporteEdades ReporteEdades = new NegReporteEdades();
        NegCertificadoMedico C = new NegCertificadoMedico();
        public frm_Ingresos_Hospitalarios_Edades()
        {
            InitializeComponent();
        }

        private void frm_Ingresos_Hospitalarios_Edades_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            dtpDesde.Value = new DateTime(date.Year, date.Month, 1);
            CargarAtenciones();
        }
        public void CargarAtenciones()
        {
            cbTipoAtencion.DataSource = ReporteEdades.CargarAtenciones();
            cbTipoAtencion.DisplayMember = "TIP_DESCRIPCION";
            cbTipoAtencion.ValueMember = "TIP_CODIGO";
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            if(dtpDesde.Value < dtpHasta.Value) //Controla que la fecha inicial no sea mayor a la fecha final
            {
                try
                {
                    DataTable TablaDatos = ReporteEdades.EdadesxAtencion(Convert.ToString(cbTipoAtencion.SelectedValue), dtpDesde.Value, dtpHasta.Value);
                    DatosReporteEdades DRE = new DatosReporteEdades();
                    DataRow drReporte;

                    string prueba = cbTipoAtencion.GetItemText(cbTipoAtencion.SelectedItem);

                    //variables que hacen el calculo por columna en total y totales
                    int h = 0, m = 0, t = 0, ht = 0, mt = 0, tt = 0;
                    foreach (DataRow item in TablaDatos.Rows)
                    {
                        drReporte = DRE.Tables["DATOS_REPORTE"].NewRow();
                        drReporte["Tipo_Atencion"] = prueba.ToString();
                        drReporte["Numero"] = item["NRO"];
                        drReporte["Descripcion"] = item["RANGO"];
                        drReporte["Hombres"] = item["HOMBRES"];
                        h = Convert.ToInt32(item["HOMBRES"]);
                        drReporte["Mujeres"] = item["MUJERES"];
                        m = Convert.ToInt32(item["MUJERES"]);
                        t = h + m;
                        drReporte["Total"] = t;
                        ht += h;    mt += m;    tt += t;
                        drReporte["Total_Hombres"] = ht;
                        drReporte["Total_Mujeres"] = mt;
                        drReporte["Total_Total"] = tt;
                        drReporte["Logo"] = C.path();
                        DRE.Tables["DATOS_REPORTE"].Rows.Add(drReporte);
                    }
                    frmReportes myreport = new frmReportes(1, "ReporteEdades", DRE);
                    myreport.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("La Fecha Inicial no puede ser mayor a la Fecha Final", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }
        public void CargarGrid()
        {
            TablaRango.Rows.Clear();
            DataTable Tabla = new DataTable(); //Contiene los valores buscados sobre el rango de edades
            Tabla = ReporteEdades.EdadesxAtencion(Convert.ToString(cbTipoAtencion.SelectedValue), dtpDesde.Value, dtpHasta.Value);

            int h = 0, m = 0, t = 0, ht = 0, mt = 0, tt = 0;

            //Cargamos los datos fila por fila dentro del grid
            foreach (DataRow item in Tabla.Rows)
            {
                h = Convert.ToInt32(item["HOMBRES"]);
                m = Convert.ToInt32(item["MUJERES"]);
                t = h + m;
                TablaRango.Rows.Add(item[0], item[1], item[2], item[3], t);
                ht += h; mt += m; tt += t;
            }
            TablaRango.Rows.Add("", "TOTALES", ht, mt, tt);
        }
    }
}
