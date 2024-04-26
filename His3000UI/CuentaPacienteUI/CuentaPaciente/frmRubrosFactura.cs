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
using Infragistics.Win.UltraWinGrid;

namespace CuentaPaciente
{
    public partial class frmRubrosFactura : Form
    {
        public int codigoRubro;
        public PRODUCTO producto;
        //List<RUBROS> listaRubros = new List<RUBROS>();
        public RUBROS rubros = new RUBROS();
        FACTURA_FORMA_PAGO facturaFormaPago = new FACTURA_FORMA_PAGO();
        FACTURA nuevaFactura = new FACTURA();
        public List<RUBROS> listaRubros = new List<RUBROS>();

        private List<PRODUCTO> listaProductos = new List<PRODUCTO>();
        Double totalFactura;
        
        public frmRubrosFactura()
        {
            InitializeComponent();
            cargarFormasPagos();
        }
        public frmRubrosFactura(Double totalFactura, FACTURA nuevaFactura, List<RUBROS> listaRubros)
        {
            InitializeComponent();
            this.totalFactura = totalFactura;
            this.nuevaFactura = nuevaFactura;
            this.listaRubros = listaRubros;
            cargarFormasPagos();            
        }

        private void cargarFormasPagos()
        {
            try
            {

                listaProductos = NegProducto.RecuperarProducto(23);
                gridRubros.DataSource = listaProductos;
                //if (listaRubros.Count <= 0)
                //{
                //    //DataRow drFormasPagos;
                //    gridRubros.DataSource = dtRubros;
                //    dtRubros.Columns.Add("RUBRO", Type.GetType("System.String"));
                //    dtRubros.Columns.Add("DESCRIPCION", Type.GetType("System.String"));
                //    dtRubros.Columns.Add("CODIGO", Type.GetType("System.String"));
                //    dtRubros.Columns.Add("MONTO", Type.GetType("System.String"));
                //    //dtRubros.Columns.Add("VENCIMIENTO", Type.GetType("System.String"));
                //    //dtRubros.Columns.Add("FECHA BANCO", Type.GetType("System.String"));
                //    //dtRubros.Columns.Add("N.CUENTA/TARJETA", Type.GetType("System.String"));
                //    //dtRubros.Columns.Add("N. CHEQUE/LOTE /RET", Type.GetType("System.String"));
                //    //dtRubros.Columns.Add("NOMBRE DUEÑO", Type.GetType("System.String"));
                //    //dtRubros.Columns.Add("AUTORIZACION", Type.GetType("System.String"));
                //}
                //else 
                //{
                //    gridRubros.DataSource = listaRubros;
                //}
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void cargarTotales(int fila)
        //{
        //    if (gridRubros.Rows.Count == 1)
        //        gridRubros.Rows[fila].Cells[2].Value = String.Format("{0:0.00}", totalFactura);
        //    else
        //    {
        //        Double total = Convert.ToDouble(totalFactura);
        //        double valor = 0.00;
        //        double iva = 0.00;
        //        //int posicion = 0;
        //        for (int i = 0; i < gridRubros.Rows.Count; i++)
        //        {
        //            string nombreConv = Convert.ToString(gridRubros.Rows[i].Cells[0].Value);
        //            if (Convert.ToString(gridRubros.Rows[i].Cells[2].Value) != "")
        //            {
        //                valor = valor + Convert.ToDouble(gridRubros.Rows[i].Cells[2].Value);
        //                iva = iva + Convert.ToDouble(gridRubros.Rows[i].Cells[3].Value);
        //                //posicion = i;
        //            }
        //        }
        //        gridRubros.Rows[fila].Cells[2].Value = String.Format("{0:0.00}", total - valor);

        //    }
        //    if (gridRubros.Rows.Count > 0)
        //    {
        //        Double subTotal = 0.00;
        //        double iva = 0.00;
        //        for (int i = 0; i < gridRubros.Rows.Count; i++)
        //        {
        //            string nombreConv = Convert.ToString(gridRubros.Rows[i].Cells[0].Value);
        //            if (Convert.ToString(gridRubros.Rows[i].Cells[2].Value) != "")
        //            {
        //                if (Convert.ToString(gridRubros.Rows[i].Cells[2].Value) != "")
        //                {
        //                    subTotal = subTotal + Convert.ToDouble(Convert.ToDouble(gridRubros.Rows[i].Cells[2].Value));
        //                }
        //                //iva = iva + Convert.ToDouble(pedido.IVA);
        //            }
        //        }
        //        txt_SubtotalPago.Text = String.Format("{0:0.00}", subTotal);
        //        txt_SIvaPago.Text = String.Format("{0:0.00}", "0.00");
        //        txt_CIvaPago.Text = String.Format("{0:0.00}", "0.00");
        //        txt_IVAPago.Text = String.Format("{0:0.00}", "0.00");
        //        txt_TotalCSPago.Text = String.Format("{0:0.00}", "0.00");
        //        txt_DescuentoPago.Text = String.Format("{0:0.00}", "0.00");
        //        txt_TotalP.Text = String.Format("{0:0.00}", subTotal);
        //    }
        //}

        private void agregaRubrosFactura(FACTURA nuevaFactura)
        {
            try
            {
                //RUBROS rubro = new RUBROS();
                //for (int i = 0; i < gridRubros.Rows.Count; i++)
                //{
                //    if (Convert.ToString(gridRubros.Rows[i].Cells[0].Value) != "" && Convert.ToString(gridRubros.Rows[i].Cells[3].Value) != "")
                //    {
                //        rubro.RUB_CODIGO = Convert.ToInt16(gridRubros.Rows[i].Cells[0].Value);
                //        rubro.RUB_NOMBRE = Convert.ToString(gridRubros.Rows[i].Cells[1].Value);
                //        rubro.RUB_TIPO = Convert.ToString(gridRubros.Rows[i].Cells[2].Value);
                //        rubro.RUB_ORDEN = Convert.ToInt32(gridRubros.Rows[i].Cells[3].Value);                        
                //        listaRubros.Add(rubro);
                //    }
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos Detalle Factura: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos Detalle Factura: \n" + e.InnerException);
            }
        }


        private void enviarCodigo(int codigo)
        {
            try
            {
                var query = (from a in listaProductos
                             where a.PRO_CODIGO == codigo
                             select a).FirstOrDefault();
                producto = query;
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            //agregaRubrosFactura(nuevaFactura);
            //this.Dispose();
        }

        private void gridRubros_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    //e.Handled = true;
                    //int index = gridRubros.CurrentRow.Index;
                    //if (index < gridRubros.Rows.Count - 1)
                    //{
                    //    gridRubros.Rows.RemoveAt(index);
                    //}
                }
                else
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        //int columna = gridRubros.SelectedCells[0].ColumnIndex;
                        ////string nombreCat = gridFormasPago.Rows[fila].Cells[0].Value.ToString();                        
                        //int fila = gridRubros.CurrentRow.Index;
                        //if (columna == 0)
                        //{
                        //    frmAyudaOtrosRubros form = new frmAyudaOtrosRubros();
                        //    form.ShowDialog();
                        //    if (form.rubro != null)
                        //    {
                        //        gridRubros.Rows[fila].Cells[0].Value = form.rubro.RUB_CODIGO;
                        //        gridRubros.Rows[fila].Cells[1].Value = form.rubro.RUB_NOMBRE;
                        //        gridRubros.Rows[fila].Cells[2].Value = form.rubro.RUB_TIPO;
                        //        //gridRubros.Rows[fila].Cells[3].Value = "50.00";                                
                        //        //cargarTotales(fila);
                        //    }
                        //}
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmRubrosFactura_Load(object sender, EventArgs e)
        {

        }

        private void ultraGridRubros_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            
            
        }

        private void gridRubros_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = gridRubros.DisplayLayout.Bands[0];
            //Caracteristicas de Filtro en la grilla
            gridRubros.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            gridRubros.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            gridRubros.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            gridRubros.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            gridRubros.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            bandUno.Columns["PRO_CODIGO"].Hidden = false;
            bandUno.Columns["PRODUCTO_ESTRUCTURA"].Hidden = true;
            bandUno.Columns["PRO_TIPO"].Hidden = true;
            bandUno.Columns["PRO_DESCRIPCION"].Hidden = true;
            bandUno.Columns["PRO_NOMBRE_GENERICO"].Hidden = false;
            bandUno.Columns["PRO_NOMBRE_COMERCIAL"].Hidden = true;
            bandUno.Columns["PRO_CONC"].Hidden = true;
            bandUno.Columns["PRO_FF"].Hidden = true;
            bandUno.Columns["PRO_PRESENTACION"].Hidden = true;
            bandUno.Columns["PRO_VIA_ADMINISTRACION"].Hidden = true;
            bandUno.Columns["PRO_REFERENCIA"].Hidden = true;
            bandUno.Columns["PRO_CODIGO_BARRAS"].Hidden = true;
            bandUno.Columns["PRO_ESTADO"].Hidden = true;
            bandUno.Columns["PRO_CANTIDAD"].Hidden = true;
            bandUno.Columns["PRO_ACCION_TERAPEUTICA"].Hidden = true;
            bandUno.Columns["PRO_OBSERVACION"].Hidden = true;
            bandUno.Columns["PRO_PRECIO"].Hidden = false;
            bandUno.Columns["PRO_IVA"].Hidden = false;
        }

        private void gridRubros_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void gridRubros_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void gridRubros_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            //int fila = Convert.ToInt32(gridRubros.ActiveRow.Index);
            int codigo = Convert.ToInt32(gridRubros.Rows[gridRubros.ActiveRow.Index].Cells[0].Value);
            enviarCodigo(codigo);
        }
    }
}
