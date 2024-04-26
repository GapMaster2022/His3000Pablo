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

namespace CuentaPaciente
{
    public partial class frmDevolucionPedidos : Form
    {

        private Int64 _AtencionCodigo = 0;
        private string _Pedido = "";

        public List<DtoPedidoDevolucionDetalle> PedidosDetalle = new List<DtoPedidoDevolucionDetalle>();
        public DtoPedidoDevolucionDetalle PedidosDetalleItem = null;

        public frmDevolucionPedidos(Int32 parCodigoArea, string Paciente, string Medico, string Historia, string Atencion, string Pedido, Int64 AtencionCodigo)
        {

            _Pedido = Pedido;
            _AtencionCodigo = AtencionCodigo;

            InitializeComponent();
        }

        private void frmDevolucionPedidos_Load(object sender, EventArgs e)
        {
            try
            {
                //cargo la lista de cantidad a paginar
                DataTable dtProductos = new DataTable();
                List<DataRow> List = new List<DataRow>();

                ProductosSolicitados.AutoGenerateColumns = false;
                dtProductos = NegProducto.RecuperarPedido(Convert.ToInt32(_Pedido));

                ProductosSolicitados.DataSource = dtProductos;

                this.PDD_CODIGO.DataPropertyName = "PDD_CODIGO";
                this.PED_CODIGO.DataPropertyName = "PED_CODIGO";
                this.PRO_CODIGO.DataPropertyName = "PRO_CODIGO";
                this.PRO_DESCRIPCION.DataPropertyName = "PRO_DESCRIPCION";
                this.PDD_CANTIDAD.DataPropertyName = "PDD_CANTIDAD";
                this.CantidadDevuelta.DataPropertyName = "CantidadDevuelta";
                this.PDD_VALOR.DataPropertyName = "PDD_VALOR";
                this.PDD_IVA.DataPropertyName = "PDD_IVA";
                this.PDD_TOTAL.DataPropertyName = "PDD_TOTAL";
                this.PED_CODIGO.DataPropertyName = "PED_CODIGO";
                this.PDD_ESTADO.DataPropertyName = "PDD_ESTADO";
                this.PDD_COSTO.DataPropertyName = "PDD_COSTO";
                this.PDD_FACTURA.DataPropertyName = "PDD_FACTURA";
                this.PDD_ESTADO_FACTURA.DataPropertyName = "PDD_ESTADO_FACTURA";
                this.PDD_FECHA_FACTURA.DataPropertyName = "PDD_FECHA_FACTURA";
                this.PDD_RESULTADO.DataPropertyName = "PDD_RESULTADO";
                this.PRO_CODIGO_BARRAS.DataPropertyName = "PRO_CODIGO_BARRAS";

                ProductosSolicitados.Refresh();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                AgregarProducto();
                this.txtCantidad.Text = "1";
                ProductosSolicitados.Focus();
            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AgregarProducto()
        {

            //addProducto();
            //txtCantidad.Text = "1";
            DataGridViewRow Item = null;
            PRODUCTO Prod = new PRODUCTO();

            Item = (DataGridViewRow)ProductosSolicitados.CurrentRow; // Selecciono la fila que esta selecionada en el grid de articulos / Giovanny Tapia / 04/10/2012


            var Verifica = (from qs in PedidosDetalle.AsEnumerable() // Verifico si el item seleccionado ya fue añadido anteriormente / Giovanny Tapia / 04/10/2012
                            where (qs.PRO_CODIGO.ToString() == Item.Cells["PRO_CODIGO"].Value.ToString())
                            select qs).ToList();

            if (Verifica.Count > 0) // Verifico si el LINQ devolvio datos / Giovanny Tapia / 04/10/2012
            {
                MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000");
                return;
            }

            if ((Convert.ToDecimal(Item.Cells["PDD_CANTIDAD"].Value.ToString()) - Convert.ToDecimal(Item.Cells["CantidadDevuelta"].Value.ToString())) - (Convert.ToDecimal(this.txtCantidad.Text)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
            {
                PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                PedidosDetalleItem.DevCodigo = 1;
                PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(Item.Cells["PRO_CODIGO"].Value);
                PedidosDetalleItem.PRO_DESCRIPCION = Item.Cells["PRO_DESCRIPCION"].Value.ToString();
                PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(txtCantidad.Text);
                PedidosDetalleItem.DevDetValor = Convert.ToDecimal(Item.Cells["PDD_VALOR"].Value.ToString());
                //cambio hr 23052016
                //PedidosDetalleItem.DevDetIva = ((((Convert.ToDecimal(Item.Cells["PDD_VALOR"].Value.ToString()) * Convert.ToInt32(this.txtCantidad.Text))) * 12) / 100);
                PedidosDetalleItem.DevDetIva = ((((Convert.ToDecimal(Item.Cells["PDD_VALOR"].Value.ToString()) * Convert.ToInt32(this.txtCantidad.Text))) * 14) / 100);
                PedidosDetalleItem.DevDetIvaTotal = Convert.ToDecimal(Item.Cells["PDD_VALOR"].Value.ToString()) * Convert.ToInt32(this.txtCantidad.Text);
                PedidosDetalleItem.PDD_CODIGO = Convert.ToInt64(Item.Cells["PDD_CODIGO"].Value.ToString());

                PedidosDetalle.Add(PedidosDetalleItem);
                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.DataSource = PedidosDetalle.ToList();

                this.DevCodigo.DataPropertyName = "DevCodigo";
                this.PRO_CODIGO_DEV.DataPropertyName = "PRO_CODIGO";
                this.PRO_DESCRIPCION_DEV.DataPropertyName = "PRO_DESCRIPCION";
                this.DevDetCantidad.DataPropertyName = "DevDetCantidad";
                this.DevDetValor.DataPropertyName = "DevDetValor";
                this.DevDetIva.DataPropertyName = "DevDetIva";
                this.DevDetIvaTotal.DataPropertyName = "DevDetIvaTotal";
                this.PDD_CODIGO_DEV.DataPropertyName = "PDD_CODIGO";
            }
            else
            {
                MessageBox.Show("La cantidad devuelta no puede ser mayor a la cantidad ingresada en el pedido..", "His3000");
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Int32 DevolucionNumero = 0;
            if ((MessageBox.Show("Estas seguro que desea guardar la devolucion? ", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    foreach (var item in PedidosDetalle)// validaciones no genere devoluciones en negativo // MarioValencia // 10-10-2023
                    {
                        decimal cantidadReal = NegPedidos.validaCantidad(_AtencionCodigo, Convert.ToString(item.PRO_CODIGO), Convert.ToInt64(_Pedido));
                        if (cantidadReal < 1)
                        {
                            MessageBox.Show("El Siguiente Producto: " + item.PRO_DESCRIPCION.ToString() + ", No Cuenta Con Stock Suficiente\r\nCantidad a devolver: " + item.DevDetCantidad + ".\r\nDebe Ser Removido Para Continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    DtoPedidoDevolucion Devolucion = new DtoPedidoDevolucion();

                    Devolucion.DevCodigo = 1;
                    Devolucion.Ped_Codigo = Convert.ToInt64(_Pedido);
                    Devolucion.DevFecha = Convert.ToDateTime(DateTime.Now.ToString());
                    Devolucion.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    Devolucion.DevObservacion = "";
                    Devolucion.IP_MAQUINA = Sesion.ip;

                    Devolucion.DetalleDevolucion = PedidosDetalle;
                    DevolucionNumero = NegPedidos.CrearDevolucionPedido(Devolucion, _AtencionCodigo, 0);

                    if (DevolucionNumero != 0)
                    {
                        MessageBox.Show("La devolucion No." + " " + DevolucionNumero.ToString() + " a sido ingresada correctamente.", "His3000");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("La devolución no se a guardado. Consulte con el administrador del sistema.", "His3000");
                    }
                }
                else
                    MessageBox.Show("No tiene productos para realizar devolución.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ProductosSolicitados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.txtCantidad.SelectAll();
                this.txtCantidad.Focus();
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAgregar.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Esta seguro que desea cancelar la devolución.?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.No))
            {
                return;
            }

            this.Close();

        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {

            NegUtilitarios.OnlyNumberDecimal(e, false);
        }
    }
}
