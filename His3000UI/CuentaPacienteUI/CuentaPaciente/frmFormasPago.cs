using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;

namespace CuentaPaciente
{
    public partial class frmFormasPago : Form
    {
        List<FORMA_PAGO> listaFormaPago = new List<FORMA_PAGO>();
        public FORMA_PAGO formaPago = new FORMA_PAGO();
        FACTURA_FORMA_PAGO facturaFormaPago = new FACTURA_FORMA_PAGO();
        FACTURA nuevaFactura = new FACTURA();
        public List<FACTURA_FORMA_PAGO> listaFacturaPagos = new List<FACTURA_FORMA_PAGO>();
        DataTable dtFormasPagos = new DataTable();
        Double totalFactura;
        public frmFormasPago()
        {
            InitializeComponent();
            cargarFormasPagos();
        }
        public frmFormasPago(Double totalFactura, FACTURA nuevaFactura, List<FACTURA_FORMA_PAGO> listaFacturaPagos)
        {
            InitializeComponent();
            this.totalFactura = totalFactura;
            this.nuevaFactura = nuevaFactura;
            this.listaFacturaPagos = listaFacturaPagos;
            cargarFormasPagos();            
        }

        private void cargarFormasPagos()
        {
            try
            {
                if (listaFacturaPagos.Count <= 0)
                {
                    //DataRow drFormasPagos;
                    gridFormasPago.DataSource = dtFormasPagos;
                    dtFormasPagos.Columns.Add("CODIGO", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("DESCRIPCION", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("MONTO", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("VENCIMIENTO", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("FECHA BANCO", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("N.CUENTA/TARJETA", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("N. CHEQUE/LOTE /RET", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("NOMBRE DUEÑO", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("AUTORIZACION", Type.GetType("System.String"));
                }
                else 
                {
                    gridFormasPago.DataSource = listaFacturaPagos;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarTotales(int fila)
        {
            if (gridFormasPago.Rows.Count == 1)
                gridFormasPago.Rows[fila].Cells[2].Value = String.Format("{0:0.00}", totalFactura);
            else
            {
                Double total = Convert.ToDouble(totalFactura);
                double valor = 0.00;
                //int posicion = 0;
                for (int i = 0; i < gridFormasPago.Rows.Count; i++)
                {
                    string nombreConv = Convert.ToString(gridFormasPago.Rows[i].Cells[0].Value);
                    if (Convert.ToString(gridFormasPago.Rows[i].Cells[2].Value) != "")
                    {
                        valor = valor + Convert.ToDouble(gridFormasPago.Rows[i].Cells[2].Value);
                        //posicion = i;
                    }
                }
                gridFormasPago.Rows[fila].Cells[2].Value = String.Format("{0:0.00}", total - valor);

            }
            if (gridFormasPago.Rows.Count > 0)
            {

                Double subTotal = 0.00;
                //double iva = 0.00;
                for (int i = 0; i < gridFormasPago.Rows.Count; i++)
                {
                    string nombreConv = Convert.ToString(gridFormasPago.Rows[i].Cells[0].Value);
                    if (Convert.ToString(gridFormasPago.Rows[i].Cells[2].Value) != "")
                    {
                        if (Convert.ToString(gridFormasPago.Rows[i].Cells[2].Value) != "")
                        {
                            subTotal = subTotal + Convert.ToDouble(Convert.ToDouble(gridFormasPago.Rows[i].Cells[2].Value));
                        }
                        //iva = iva + Convert.ToDouble(pedido.IVA);
                    }
                }

                txt_SubtotalPago.Text = String.Format("{0:0.00}", subTotal);
                txt_SIvaPago.Text = String.Format("{0:0.00}", "0.00");
                txt_CIvaPago.Text = String.Format("{0:0.00}", "0.00");
                txt_IVAPago.Text = String.Format("{0:0.00}", "0.00");
                txt_TotalCSPago.Text = String.Format("{0:0.00}", "0.00");
                txt_DescuentoPago.Text = String.Format("{0:0.00}", "0.00");
                txt_TotalP.Text = String.Format("{0:0.00}", subTotal);
            }
        }

        private void agregarDatosPagoFactura(FACTURA nuevaFactura)
        {
            try
            {
                FORMA_PAGO formaPago = new FORMA_PAGO();
                for (int i = 0; i < gridFormasPago.Rows.Count; i++)
                {
                    if (Convert.ToString(gridFormasPago.Rows[i].Cells[0].Value) != "" && Convert.ToString(gridFormasPago.Rows[i].Cells[2].Value) != "") 
                    {
                        formaPago = NegFormaPago.RecuperaFormaPagoID(Convert.ToInt16(gridFormasPago.Rows[i].Cells[0].Value));
                        facturaFormaPago = new FACTURA_FORMA_PAGO();
                        facturaFormaPago.COD_FAC_PAGO = NegFactura.recuperaMaximoFacturaPago() + 1;
                        facturaFormaPago.FACTURAReference.EntityKey = nuevaFactura.EntityKey;
                        facturaFormaPago.FORMA_PAGOReference.EntityKey = formaPago.EntityKey;
                        facturaFormaPago.FORM_PAGO = " ";
                        facturaFormaPago.FORM_DESCRIPCION = Convert.ToString(gridFormasPago.Rows[i].Cells[1].Value);
                        facturaFormaPago.FORM_MONTO = Convert.ToDecimal(gridFormasPago.Rows[i].Cells[2].Value);
                        facturaFormaPago.FORM_PORCENTAJE = 0;
                        facturaFormaPago.FORM_CONTADO = 1;
                        facturaFormaPago.FORM_CREDITO = 1;
                        facturaFormaPago.FORM_VENCIMIENTO = Convert.ToDateTime(gridFormasPago.Rows[i].Cells[3].Value);
                        facturaFormaPago.FORM_BANCO = Convert.ToString(gridFormasPago.Rows[i].Cells[4].Value);
                        facturaFormaPago.FORM_TARJETA_CRE = Convert.ToString(gridFormasPago.Rows[i].Cells[5].Value);
                        facturaFormaPago.FORM_CHEQUE = Convert.ToString(gridFormasPago.Rows[i].Cells[6].Value);
                        facturaFormaPago.FORM_NOM_DUE = Convert.ToString(gridFormasPago.Rows[i].Cells[7].Value);
                        facturaFormaPago.FORM_AUTORIZACION = Convert.ToString(gridFormasPago.Rows[i].Cells[8].Value);
                        facturaFormaPago.FORM_ESTADO = 1;
                        listaFacturaPagos.Add(facturaFormaPago);
                    }                                     
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos Detalle Factura: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos Detalle Factura: \n" + e.InnerException);
            }
        }


        private void gridFormasPago_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    e.Handled = true;
                    int index = gridFormasPago.CurrentRow.Index;
                    if (index < gridFormasPago.Rows.Count - 1)
                    {
                        gridFormasPago.Rows.RemoveAt(index);
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        int columna = gridFormasPago.SelectedCells[0].ColumnIndex;
                        //string nombreCat = gridFormasPago.Rows[fila].Cells[0].Value.ToString();                        
                        int fila = gridFormasPago.CurrentRow.Index;
                        if (columna == 0)
                        {
                            frmAyudaFormasPagos form = new frmAyudaFormasPagos();
                            form.ShowDialog();
                            if (form.codigoFormaPago != null)
                            {
                                gridFormasPago.Rows[fila].Cells[0].Value = form.codigoFormaPago;
                                gridFormasPago.Rows[fila].Cells[1].Value = form.nombreFormaPago;
                                gridFormasPago.Rows[fila].Cells[3].Value = DateTime.Today.AddDays(1);
                                //gridFormasPago.Rows[fila].Cells[2].Value = "00.00";                                
                                cargarTotales(fila);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            agregarDatosPagoFactura(nuevaFactura);
            this.Dispose();
        }
    }
}
