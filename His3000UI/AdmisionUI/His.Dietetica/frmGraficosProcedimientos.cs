using His.Negocio;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class frmGraficosProcedimientos : Form
    {
        public frmGraficosProcedimientos()
        {
            InitializeComponent();
        }
        ArrayList codigo = new ArrayList();
        ArrayList detalle = new ArrayList();
        private void frmGraficosProcedimientos_Load(object sender, EventArgs e)
        {
            DateTime oPrimerDiaDelMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtpDesde.Value = oPrimerDiaDelMes;
            dtpHasta.Value = oUltimoDiaDelMes;
            actualizarGrafrico();
        }
        private void actualizarGrafrico()
        {
            DataTable graficoXmes = new DataTable();
            graficoXmes = NegQuirofano.procedimietosXmes(dtpDesde.Value, dtpHasta.Value);
            //for (int i = 0; i < graficoXmes.Rows.Count; i++)
            //{
            //    codigo.Add(graficoXmes.Rows[i][0].ToString());
            //    detalle.Add(graficoXmes.Rows[i][1].ToString());
            //}
            ultraChart1.DataSource = graficoXmes;

            
        }
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            actualizarGrafrico();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
