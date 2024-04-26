using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using His.Entidades;
using System.Net;

namespace His.Admision
{
    public partial class frm_Devolucion : Form
    {
        NegDietetica dieta = new NegDietetica();
        internal static Int64 codigopedido; //recibe el codigo de pedido desde el formulario de servicios externos.
        internal static Int64 ate_codigo;
        public frm_Devolucion()
        {
            InitializeComponent();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frm_Devolucion_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }
        public void CargarProductos()
        {
            try
            {
                TablaDevolucion.Rows.Clear();
                DataTable Tabla = new DataTable();
                Tabla = dieta.ProductosDevolucion(codigopedido);
                foreach (DataRow item in Tabla.Rows)
                {
                    TablaDevolucion.Rows.Add(item[0].ToString(), item[1].ToString(), item[2].ToString(), item[3].ToString(), item[4].ToString(),
                        item[5].ToString(), item[6].ToString(), item[7].ToString(), item[8].ToString(), false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public Int64 ped_codigo = 0;
        public List<DtoPedidoDevolucionDetalle> PedidosDetalle = new List<DtoPedidoDevolucionDetalle>();
        public Int32 DevolucionNumero = 0;
        public DtoPedidoDevolucionDetalle PedidosDetalleItem = null;
        public Int64 cue_Codigo = 0;

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            PedidosDetalle = new List<DtoPedidoDevolucionDetalle>();
            bool devolvio = false; //valida si encontro un producto para hacer la devolucion.
            bool fuebien = false;
            if(txtobservacion.Text.Trim() == "")
            {
                errorProvider1.SetError(txtobservacion, "Debe añadir una razon de la devolución.");
            }
            else
            {
                foreach (DataGridViewRow item in TablaDevolucion.Rows)
                {
                    if (Convert.ToBoolean(item.Cells["dev"].Value) == true)
                    {
                        if (!NegParametros.ParametroDevolucionBienes())
                        {
                            try
                            {
                                PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                                PedidosDetalleItem.DevCodigo = 1;
                                PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(item.Cells[1].Value.ToString());
                                PedidosDetalleItem.PRO_DESCRIPCION = item.Cells[2].Value.ToString();
                                PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(item.Cells[3].Value.ToString());
                                PedidosDetalleItem.DevDetValor = Convert.ToDecimal(item.Cells[4].Value.ToString());
                                PedidosDetalleItem.DevDetIva = 0;
                                PedidosDetalleItem.DevDetIvaTotal = 0;
                                Int64 pdd = Convert.ToInt64(item.Cells[7].Value.ToString());
                                PedidosDetalleItem.PDD_CODIGO = pdd;

                                PedidosDetalle.Add(PedidosDetalleItem);
                                devolvio = true;
                                ped_codigo = Convert.ToInt64(item.Cells[0].Value.ToString());

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            if (His.Entidades.Clases.Sesion.codDepartamento == 1 || His.Entidades.Clases.Sesion.codDepartamento == 14)
                            {
                                try
                                {
                                    PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                                    PedidosDetalleItem.DevCodigo = 1;
                                    PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(item.Cells[1].Value.ToString());
                                    PedidosDetalleItem.PRO_DESCRIPCION = item.Cells[2].Value.ToString();
                                    PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(item.Cells[3].Value.ToString());
                                    PedidosDetalleItem.DevDetValor = Convert.ToDecimal(item.Cells[4].Value.ToString());
                                    PedidosDetalleItem.DevDetIva = 0;
                                    PedidosDetalleItem.DevDetIvaTotal = 0;
                                    Int64 pdd = Convert.ToInt64(item.Cells[7].Value.ToString());
                                    PedidosDetalleItem.PDD_CODIGO = pdd;
                                    cue_Codigo = Convert.ToInt64(item.Cells[8].Value.ToString());
                                    PedidosDetalle.Add(PedidosDetalleItem);
                                    devolvio = true;
                                    ped_codigo = Convert.ToInt64(item.Cells[0].Value.ToString());

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            else
                            {
                                DataTable ProductoB = new DataTable();
                                ProductoB = NegProducto.RecuperarProductoSic(item.Cells[1].Value.ToString());

                                if (ProductoB.Rows[0]["clasprod"].ToString().Trim() == "B")
                                {
                                    MessageBox.Show("No tiene permisos para hacer devolución de bienes.\r\nConsulte con el Administrador.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    fuebien = true;
                                }
                                else
                                {
                                    var Verifica = (from qs in PedidosDetalle.AsEnumerable() // Verifico si el item seleccionado ya fue añadido anteriormente / Giovanny Tapia / 04/10/2012
                                                    where (qs.PRO_CODIGO.ToString() == item.Cells[1].Value.ToString())
                                                    select qs).ToList();

                                    //if (Verifica.Count > 0) // Verifico si el LINQ devolvio datos / Giovanny Tapia / 04/10/2012
                                    //{
                                    //    MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000");
                                    //    //xamDataPresenterProductosSolicitados.Focus();
                                    //    return;
                                    //}

                                    //if ((Convert.ToDecimal(Item["PDD_CANTIDAD"].ToString()) - Convert.ToDecimal(Item["CantidadDevuelta"].ToString())) - (Convert.ToDecimal(this.txtCantidad.Value)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                                    //{

                                    //}
                                    PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                                    PedidosDetalleItem.DevCodigo = 1;
                                    PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(item.Cells[1].Value.ToString());
                                    PedidosDetalleItem.PRO_DESCRIPCION = item.Cells[2].Value.ToString();
                                    PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(item.Cells[3].Value.ToString());
                                    PedidosDetalleItem.DevDetValor = Convert.ToDecimal(item.Cells[4].Value.ToString());
                                    PedidosDetalleItem.DevDetIva = 0;
                                    PedidosDetalleItem.DevDetIvaTotal = 0;
                                    Int64 pdd = Convert.ToInt64(item.Cells[7].Value.ToString());
                                    PedidosDetalleItem.PDD_CODIGO = pdd;

                                    PedidosDetalle.Add(PedidosDetalleItem);
                                    devolvio = true;
                                    ped_codigo = Convert.ToInt64(item.Cells[0].Value.ToString());

                                }
                            }
                            //else
                            //    MessageBox.Show("No tiene permisos para hacer devolución de bienes.\r\nConsulte con el Administrador.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                }
                if (devolvio == true)
                {
                    foreach (var item in PedidosDetalle)// validaciones no genere devoluciones en negativo // MarioValencia // 10-10-2023
                    {
                        decimal cantidadReal = NegPedidos.validaCantidad(Convert.ToInt64(ate_codigo), Convert.ToString(item.PRO_CODIGO), Convert.ToInt64(ped_codigo));
                        if (cantidadReal < 1)
                        {
                            MessageBox.Show("El Siguiente Producto: " + item.PRO_DESCRIPCION.ToString() + ", No Cuenta Con Stock Suficiente\r\nCantidad a devolver: " + item.DevDetCantidad + ".\r\nDebe Ser Removido Para Continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    DtoPedidoDevolucion Devolucion = new DtoPedidoDevolucion();

                    Devolucion.DevCodigo = 1;
                    Devolucion.Ped_Codigo = ped_codigo;
                    Devolucion.DevFecha = Convert.ToDateTime(DateTime.Now.ToString());
                    Devolucion.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                    Devolucion.DevObservacion = "SERVICIOS EXTERNOS - " + txtobservacion.Text;//AQUI SE VA A LA TABLA DE PEDIDO_DEVOLUCION LA RAZON DE DEVOLUCION AQUI ANTES ESTABA  = ""
                    Devolucion.IP_MAQUINA = Entidades.Clases.Sesion.ip;

                    Devolucion.DetalleDevolucion = PedidosDetalle;
                    DevolucionNumero = NegPedidos.CrearDevolucionPedido(Devolucion, ate_codigo ,cue_Codigo);

                    if (DevolucionNumero != 0)
                    {
                        MessageBox.Show("La devolucion No." + " " + DevolucionNumero.ToString() + " a sido ingresada correctamente.", "His3000");
                        this.Close();
                    }
                }
                else if (devolvio == false && fuebien == false)
                {
                    MessageBox.Show("Debe marcar el/los producto(s) para hacer la devolución.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            
        }
        double cantAnterior = 0;
        int rowAnterior = 0;
        private void TablaDevolucion_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if(e.ColumnIndex == 3)
            //{
            //    if(cantAnterior > Convert.ToDouble(TablaDevolucion.Rows[rowAnterior].Cells["cant"].Value.ToString()))
            //    {
            //        MessageBox.Show("La cantidad ha devolver no debe superar a la cantidad pedida.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        TablaDevolucion.Rows[e.RowIndex].Cells["val"].Value = cantAnterior;
            //    }
            //}
        }

        private void TablaDevolucion_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                cantAnterior = Convert.ToDouble(TablaDevolucion.Rows[e.RowIndex].Cells["cant"].Value.ToString());
                rowAnterior = e.RowIndex;
            }
        }

        private void TablaDevolucion_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (Convert.ToDouble(TablaDevolucion.Rows[rowAnterior].Cells["cant"].Value.ToString()) > cantAnterior )
                {
                    MessageBox.Show("La cantidad ha devolver no debe superar a la cantidad pedida.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TablaDevolucion.Rows[e.RowIndex].Cells["cant"].Value = cantAnterior;
                }
                else if(Convert.ToDouble(TablaDevolucion.Rows[rowAnterior].Cells["cant"].Value.ToString()) == 0)
                {
                    MessageBox.Show("Cantidad ha devolver no pueder ser 0", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TablaDevolucion.Rows[e.RowIndex].Cells["cant"].Value = cantAnterior;
                }
            }
        }
    }
}
