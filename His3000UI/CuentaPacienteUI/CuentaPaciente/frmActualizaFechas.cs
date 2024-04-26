using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Honorarios.Datos;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Formulario;
using His.Negocio;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using His.Admision;
using His.General;
using System.Data.OleDb;

namespace CuentaPaciente
{
    public partial class frmActualizaFechas : Form
    {
        private List<PEDIDOS_AREAS> listaAreas = new List<PEDIDOS_AREAS>();
        int Atencion = 0;
        DateTime _FechaIngreso;
        DateTime _FechaAlta;

        public frmActualizaFechas(int CodigoAtencion,DateTime FechaIngreso, DateTime FechaAlta)
        {
            _FechaIngreso = FechaIngreso;
            _FechaAlta = FechaAlta;
            Atencion = CodigoAtencion;
            InitializeComponent();
        }

        private void frmActualizaFechas_Load(object sender, EventArgs e)
        {
            listaAreas = NegPedidos.recuperarListaAreas().OrderBy(a => a.PEA_NOMBRE).ToList();
            cmb_Areas.DataSource = listaAreas;
            cmb_Areas.ValueMember = "PEA_CODIGO";
            cmb_Areas.DisplayMember = "PEA_NOMBRE";
            cmb_Areas.SelectedIndex = 0;

            dtpFechaPedido.Value = Convert.ToDateTime(_FechaIngreso.ToShortDateString());

        }

        private void cmb_Areas_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            if (dtpFechaPedido.Value >= Convert.ToDateTime(_FechaIngreso.ToShortDateString()) && dtpFechaPedido.Value <= Convert.ToDateTime(_FechaAlta.ToShortDateString()))
            {

                int PedidoArea = 0;
                int Rubro = 0;

                PedidoArea = Convert.ToInt32(((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO);
                Rubro = Convert.ToInt32(((RUBROS)cmb_Rubros.SelectedItem).RUB_CODIGO);

                DialogResult resultado;
                resultado = MessageBox.Show("Realmente desea realizar esta modificacion de fechas ?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                try
                {
                    if (resultado == DialogResult.Yes)
                    {
                        NegCuentasPacientes.ActualizaFechasCuenta(Atencion, PedidoArea, Rubro, dtpFechaPedido.Value,His.Entidades.Clases.Sesion.codUsuario);
                        MessageBox.Show("Datos Guardados Correctamente.!");
                        this.Dispose();
                    }
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }
            }
            else 
            {
                MessageBox.Show("La fecha ingresada debe estar entre la fecha de ingreso y alta.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
