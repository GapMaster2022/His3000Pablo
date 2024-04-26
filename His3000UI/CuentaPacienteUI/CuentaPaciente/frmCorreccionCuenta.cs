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

using System.Text.RegularExpressions;


namespace CuentaPaciente
{
    public partial class frmCorreccionCuenta : Form
    {
        DataTable dtValoresCuentas = new DataTable();
        DataTable dtValoresCuentasEliminados = new DataTable();
        List<DataRow> ElementosEliminados = new List<DataRow>();
        List<DtoItemEliminadoCuentas> ListaItemsEliminados = new List<DtoItemEliminadoCuentas>();

        Int64 _CodigoAtencion = 0;
        Int32 FilaActual = 0;
        String NumeroPedido = "";

        Int64 Area = 0;
        Int64 SubArea = 0;

        public frmCorreccionCuenta(Int64 CodigoAtencion)
        {
            _CodigoAtencion = CodigoAtencion;

            InitializeComponent();
        }

        private void frmCorreccionCuenta_Load(object sender, EventArgs e)
        {
            //Cambios Edgar 20210201
            if (NegRubros.ParametroServicios() && Sesion.codDepartamento == 7) //si es cajero solo se presentan los servicios
            {
                cmb_Areas.DataSource = NegPedidos.RecuperarListaServicios().OrderBy(a => a.PEA_NOMBRE).ToList();
                cmb_Areas.ValueMember = "PEA_CODIGO";
                cmb_Areas.DisplayMember = "PEA_NOMBRE";
                cmb_Areas.SelectedIndex = 0;
            }
            else
            {
                cmb_Areas.DataSource = NegPedidos.recuperarListaAreasTodas().OrderBy(a => a.PEA_NOMBRE).ToList();
                cmb_Areas.ValueMember = "PEA_CODIGO";
                cmb_Areas.DisplayMember = "PEA_NOMBRE";
                cmb_Areas.SelectedIndex = 0;
            }
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

                    cmb_Rubros.SelectedIndex = 0;
                    //cargarProductos();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }


        }

        private void cmb_Rubros_SelectedIndexChanged(object sender, EventArgs e)
        {

            GuardaDatos();

            PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
            RUBROS Rubro1 = new RUBROS();
            Rubro1 = (RUBROS)cmb_Rubros.SelectedItem;

            ar = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;

            if (Rubro1 != null)
            {
                var Rubro = (Int16)Rubro1.RUB_CODIGO;

                dtValoresCuentas = NegFactura.DatosFacturaCambio(_CodigoAtencion, ar.PEA_CODIGO, Rubro);
                this.dgvDatosCuenta.DataSource = dtValoresCuentas;

                dgvDatosCuenta.Columns[0].ReadOnly = true;
                dgvDatosCuenta.Columns[1].ReadOnly = true;
                dgvDatosCuenta.Columns[2].ReadOnly = true;
                dgvDatosCuenta.Columns[3].ReadOnly = true;
                dgvDatosCuenta.Columns[6].ReadOnly = true;
                dgvDatosCuenta.Columns[7].ReadOnly = true;
                dgvDatosCuenta.Columns[8].ReadOnly = true;
                //dgvDatosCuenta.Columns[7].Visible=false;

                var sum = dtValoresCuentas.AsEnumerable().Sum(x => x.Field<decimal>("TOTAL"));

                this.lblTotal.Text = sum.ToString();

                Area = ar.PEA_CODIGO;
                SubArea = Rubro;

                if (ar.PEA_CODIGO == 1 && (Rubro == 1 || Rubro == 27))
                {

                    btnDevolucion.Enabled = true;
                    btnEliminar.Enabled = true;
                    this.dgvDatosCuenta.ReadOnly = true;
                    dgvDatosCuenta.AllowUserToDeleteRows = false;

                }
                else
                {

                    btnDevolucion.Enabled = false;
                    btnEliminar.Enabled = false;
                    this.dgvDatosCuenta.ReadOnly = false;
                    dgvDatosCuenta.AllowUserToDeleteRows = true;

                }

            }
        }

        private void dgvDatosCuenta_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal Valor = 0;
            decimal Iva = 0;

            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                decimal ivap = Convert.ToDecimal(NegParametros.ParametroIva());
                decimal pagaIva = NegParametros.ProductoPagaIVA(Convert.ToInt64(dgvDatosCuenta.Rows[e.RowIndex].Cells["codigo"].Value));
                Valor = Convert.ToDecimal(dgvDatosCuenta.Rows[e.RowIndex].Cells["cantidad"].Value) * Convert.ToDecimal(dgvDatosCuenta.Rows[e.RowIndex].Cells["valor"].Value);
                if (pagaIva != 0)
                {
                    Iva = (Valor * ivap)/100;
                    dgvDatosCuenta.Rows[e.RowIndex].Cells["IVA"].Value = Iva;
                    dgvDatosCuenta.Rows[e.RowIndex].Cells["TOTAL"].Value = Convert.ToDecimal(Valor + Iva);
                }
                else
                {
                    dgvDatosCuenta.Rows[e.RowIndex].Cells["TOTAL"].Value = Valor;
                }

                dgvDatosCuenta.Rows[e.RowIndex].Cells["ESTADO"].Value = "U";
                //var sum = dtValoresCuentas.AsEnumerable().Sum(x => x.Field<decimal>("TOTAL"));
                decimal sum = 0;
                for (int i = 0; i < dtValoresCuentas.Rows.Count; i++)
                {
                    sum += Convert.ToDecimal(dgvDatosCuenta.Rows[i].Cells["TOTAL"].Value);
                }
                decimal suma = Convert.ToDecimal(sum);
                string total;
                total = String.Format("{0:0.00}", SignificantTruncate(Convert.ToDouble(suma), 2));
                this.lblTotal.Text = total;
            }
        }
        public static double SignificantTruncate(double num, int significantDigits)
        {
            double y = Math.Pow(10, significantDigits);
            return Math.Round(num * y) / y;
        }
        private void dgvDatosCuenta_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {


        }

        private void GuardaDatos()
        {
            if (dtValoresCuentas != null)
            {

                int linqCount2 = dtValoresCuentas.AsEnumerable().Where(x => x.Field<string>("ESTADO") == "U").Count();

                if (linqCount2 > 0 || ListaItemsEliminados.Count > 0)
                {
                    
                    DialogResult resultado;
                    resultado = MessageBox.Show("Desea guardar los cambios realizados en este grupo ?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        try
                        {
                            if (linqCount2 > 0)
                            {
                                NegFactura.GuardaCambiosFactura(_CodigoAtencion, dtValoresCuentas, Sesion.codDepartamento);
                                dtValoresCuentas = null;
                            }

                            if (ListaItemsEliminados.Count > 0)
                            {
                                NegFactura.EliminaValoresCuentas(_CodigoAtencion, ListaItemsEliminados);
                                ListaItemsEliminados = null;
                                ListaItemsEliminados = new List<DtoItemEliminadoCuentas>();
                            }

                            MessageBox.Show("Los cambios se han guardado correctamente");
                        }
                        catch (Exception a)
                        {
                            MessageBox.Show(a.Message);
                        }
                    }
                    else
                    {
                        ListaItemsEliminados = null;
                        ListaItemsEliminados = new List<DtoItemEliminadoCuentas>();
                        return;

                    }
                }
                else
                {
                    return;
                }


                if (ElementosEliminados != null)
                {
                    ElementosEliminados = null;
                }

                ElementosEliminados = new List<DataRow>();
            }

        }

        private void dgvDatosCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            //DtoItemEliminadoCuentas Item = new DtoItemEliminadoCuentas();

            //if (e.KeyCode == Keys.Delete)
            //{
            //    if (Area == 1 && (SubArea == 1 || SubArea == 27))
            //    {
            //        return;
            //    }
            //    DataRow Fila;
            //    Int32 FilaActual = 0;
            //    FilaActual = dgvDatosCuenta.CurrentRow.Index;
            //    Fila = dtValoresCuentas.Rows[FilaActual];
            //    Item.CodigoAtencion = _CodigoAtencion;
            //    Item.CodigoPedido = Convert.ToInt32(Fila["PEDIDO"].ToString());
            //    Item.Producto = Fila["CODIGO"].ToString();
            //    ListaItemsEliminados.Add(Item);
            //    dgvDatosCuenta.Rows.RemoveAt(FilaActual);
            //    dtValoresCuentas.AcceptChanges();
            //}

        }

        private void dgvDatosCuenta_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatosCuenta.CurrentRow != null)
            {
                FilaActual = dgvDatosCuenta.CurrentRow.Index;
                NumeroPedido = dgvDatosCuenta.CurrentRow.Cells["PEDIDO"].Value.ToString();
            }
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            frmDevolucionPedidos Form = new frmDevolucionPedidos(0, "", "", "", "", NumeroPedido, _CodigoAtencion);
            Form.ShowDialog();


            PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
            RUBROS Rubro1 = new RUBROS();
            Rubro1 = (RUBROS)cmb_Rubros.SelectedItem;

            ar = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;

            if (Rubro1 != null)
            {
                var Rubro = (Int16)Rubro1.RUB_CODIGO;

                dtValoresCuentas = NegFactura.DatosFacturaCambio(_CodigoAtencion, ar.PEA_CODIGO, Rubro);
                this.dgvDatosCuenta.DataSource = dtValoresCuentas;

                dgvDatosCuenta.Columns[0].ReadOnly = true;
                dgvDatosCuenta.Columns[1].ReadOnly = true;
                dgvDatosCuenta.Columns[2].ReadOnly = true;
                dgvDatosCuenta.Columns[5].ReadOnly = true;
                dgvDatosCuenta.Columns[6].ReadOnly = true;
                //dgvDatosCuenta.Columns[7].Visible=false;

                var sum = dtValoresCuentas.AsEnumerable().Sum(x => x.Field<decimal>("TOTAL"));
                this.lblTotal.Text = sum.ToString();

                Area = ar.PEA_CODIGO;
                SubArea = Rubro;

                if (ar.PEA_CODIGO == 1 && (Rubro == 1 || Rubro == 27))
                {

                    btnDevolucion.Enabled = true;

                }
                else
                {

                    btnDevolucion.Enabled = false;

                }

            }


        }

        private void dgvDatosCuenta_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            NumeroPedido = dgvDatosCuenta.CurrentRow.Cells["PEDIDO"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            frmDevolucionPedidos Form = new frmDevolucionPedidos(0, "", "", "", "", NumeroPedido, _CodigoAtencion);
            Form.ShowDialog();


            PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
            RUBROS Rubro1 = new RUBROS();
            Rubro1 = (RUBROS)cmb_Rubros.SelectedItem;

            ar = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;

            if (Rubro1 != null)
            {
                var Rubro = (Int16)Rubro1.RUB_CODIGO;

                dtValoresCuentas = NegFactura.DatosFacturaCambio(_CodigoAtencion, ar.PEA_CODIGO, Rubro);
                this.dgvDatosCuenta.DataSource = dtValoresCuentas;

                dgvDatosCuenta.Columns[0].ReadOnly = true;
                dgvDatosCuenta.Columns[1].ReadOnly = true;
                dgvDatosCuenta.Columns[2].ReadOnly = true;
                dgvDatosCuenta.Columns[5].ReadOnly = true;
                dgvDatosCuenta.Columns[6].ReadOnly = true;
                //dgvDatosCuenta.Columns[7].Visible=false;

                var sum = dtValoresCuentas.AsEnumerable().Sum(x => x.Field<decimal>("TOTAL"));
                this.lblTotal.Text = sum.ToString();

                Area = ar.PEA_CODIGO;
                SubArea = Rubro;

                if (ar.PEA_CODIGO == 1 && (Rubro == 1 || Rubro == 27))
                {

                    btnDevolucion.Enabled = true;
                    btnEliminar.Enabled = true;

                }
                else
                {

                    btnDevolucion.Enabled = false;
                    btnEliminar.Enabled = false;

                }

            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            GuardaDatos();
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
