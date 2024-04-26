using His.DatosReportes;
using His.Formulario;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Honorarios
{
    public partial class Reporte_FactVendedores : Form
    {
        public Reporte_FactVendedores()
        {
            InitializeComponent();
            cmbReferido.SelectedIndex = 0;
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnAddVendedor_Click(object sender, EventArgs e)
        {
            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("VENDEDORES");
            ayuda.ShowDialog();


            if (ayuda.codigo != string.Empty)
            {
                txtVendedor.Text = ayuda.producto;
                txtCodVendedor.Text = ayuda.codigo;
            }
        }

        private void txtVendedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnAddVendedor.Visible)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtVendedor.Text = string.Empty;
                    txtCodVendedor.Text = string.Empty;
                    e.Handled = true;
                }
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string condicion = "where ";
            #region armo filtros sql
            if (cmbReferido.Text.Trim() == "Todos")
                {
                    condicion += "a.ATE_REFERIDO like '%' ";
                }
                else if (cmbReferido.Text.Trim() == "Privado")
                {
                    condicion += "a.ATE_REFERIDO like '0' ";
                }
                else if (cmbReferido.Text.Trim() == "Institucional")
                {
                    condicion += "a.ATE_REFERIDO like '1' ";
                }
                if (txtCodVendedor.Text.Trim()!=string.Empty)
                    condicion += " AND dbo.vendedores.codigo=" + txtCodVendedor.Text.Trim() + " ";
                 if (!dtpDesde.Checked && dtpHasta.Checked)
                {
                    condicion += " and 	Sic3000.dbo.Nota.fecha<= '" + dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' ";
                }
                else if (dtpDesde.Checked && !dtpHasta.Checked)
                {
                    condicion += " and 	Sic3000.dbo.Nota.fecha>= '" + dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' ";
                }
                else if (dtpDesde.Checked && dtpHasta.Checked)
                {
                    condicion += " and 	Sic3000.dbo.Nota.fecha between '" + dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' and '" + dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' ";
                }
            #endregion
            //DataTable reporte = NegVendedores.getReporteVendedor(condicion);
            DataTable reporte = NegVendedores.getReporteVendedor2(condicion);
            ReportesHistoriaClinica x = new ReportesHistoriaClinica();
            x.deleteVendedoresReporte();
            ReportesHistoriaClinica y = new ReportesHistoriaClinica();
            //y.saveVendedoresReporte(reporte);
            y.saveVendedoresReporte2(reporte);

            His.Formulario.frmReportes ventana = new His.Formulario.frmReportes(1, "Vendedores");
            ventana.Show();

        }
    }
}
