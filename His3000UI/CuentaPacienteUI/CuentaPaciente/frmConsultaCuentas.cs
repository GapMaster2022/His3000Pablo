using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using Infragistics.Win.UltraWinGrid;
using His.Maintenance;
using His.Parametros;
using His.DatosReportes;
using His.Entidades.Reportes;
using His.Formulario;
using Core.Utilitarios;
//using GeneralApp;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using frmImpresionPedidos = His.HabitacionesUI.frmImpresionPedidos;
namespace CuentaPaciente
{
    public partial class frmConsultaCuentas : Form
    {
        public frmConsultaCuentas()
        {
            InitializeComponent();
        }

        private void cmb_Areas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PEDIDOS_AREAS pArea = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
            try
            {
                cmb_Rubros.DataSource = null;
                if (cmb_Areas.SelectedItem != null)
                {

                    PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
                    List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
                    if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
                        cmb_Rubros.DataSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
                    else
                        cmb_Rubros.DataSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();

                    cmb_Rubros.DisplayMember = "RUB_NOMBRE".Trim();
                    cmb_Rubros.ValueMember = "RUB_CODIGO";
                    //cargarProductos();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }


        }

        private void frmConsultaCuentas_Load(object sender, EventArgs e)
        {
            cmb_Areas.DataSource = NegPedidos.recuperarListaAreasTodas().OrderBy(a => a.PEA_NOMBRE).ToList();
            cmb_Areas.ValueMember = "PEA_CODIGO";
            cmb_Areas.DisplayMember = "PEA_NOMBRE";
            cmb_Areas.SelectedIndex = 0;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DataTable DatosCuentas = new DataTable();
            Int32 Area = 0;
            int todosRubros = 0;
            if (chbTodosRubros.Checked == true)
                todosRubros = 1;

            PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
            Area = Convert.ToInt32(areaP.PEA_CODIGO);

            if (cmb_Rubros.Text != "")
            {
                DatosCuentas = NegCuentasPacientes.GeneraValoresCuentas(this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString(), Area, Convert.ToInt32(this.cmb_Rubros.SelectedValue), todosRubros);
                if (DatosCuentas.Rows.Count == 0)
                    MessageBox.Show("Rubro No Existente Para El Periodo Seleccionado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                DatosCuentas = NegCuentasPacientes.GeneraValoresCuentas(this.dateTimePicker1.Value.ToShortDateString(), this.dateTimePicker2.Value.ToShortDateString(), Area, 0, todosRubros);
            }

            if (DatosCuentas == null || DatosCuentas.Rows.Count == 0)
            {
                return;
            }

            FrmImpresionDatosCuentas Forma = new FrmImpresionDatosCuentas(DatosCuentas, "ValoresCuentas");
            Forma.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void chbTodosRubros_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTodosRubros.Checked == true)
                cmb_Rubros.Enabled = false;
            else
                cmb_Rubros.Enabled = true;
        }
    }
}

