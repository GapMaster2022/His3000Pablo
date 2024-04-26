using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FeserWard.Controls;
using His.Entidades;
using His.Negocio;
using System.Data;
using His.Entidades.Clases;
using System.Threading;
using System.Net;

namespace His.HabitacionesUI
{
    /// <summary>
    /// Lógica de interacción para frmDevolucionPedido.xaml
    /// </summary>
    public partial class frmDevolucionPedido : Window
    {
        public string descripcion;
        private Int32 codigoArea;
        private string vistaModo;

        private string _Medico = "";
        private string _Paciente = "";
        private string _Historia = "";
        private string _Atencion = "";
        private Int64 _AtencionCodigo = 0;
        private string _Pedido = "";

        public List<DtoPedidoDevolucionDetalle> PedidosDetalle = new List<DtoPedidoDevolucionDetalle>();
        public DtoPedidoDevolucionDetalle PedidosDetalleItem = null;

        public Int32 DevolucionNumero = 0;

        public frmDevolucionPedido(Int32 parCodigoArea, string Paciente, string Medico, string Historia, string Atencion, string Pedido, Int64 AtencionCodigo)
        {
            _Atencion = Atencion;
            _Historia = Historia;
            _Medico = Medico;
            _Paciente = Paciente;
            _Pedido = Pedido;
            _AtencionCodigo = AtencionCodigo;

            codigoArea = parCodigoArea;
            InitializeComponent();
        }

        private void frmDevolucionPedido1_Loaded(object sender, RoutedEventArgs e)
        {
            // Cargo Datos del pedido / Giovanny Tapia / 17/10/2012
            this.lblPedido.Content = this.lblPedido.Content + "  " + _Pedido;
            this.lblAtencion.Content = _Atencion;
            this.lblHistoria.Content = _Historia;
            this.lblMedico.Content = _Medico;
            this.lblPaciente.Content = _Paciente;


            // Cargo Detalle del Pedido / Giovanny Tapia 17/10/2012
            //xamDataPresenterProductosSolicitados.DataSource = NegPedidos.RecuperaDetallePedidos(Convert.ToInt32(_Pedido));

            try
            {
                //cargo la lista de cantidad a paginar
                DataTable dtProductos = new DataTable();
                List<DataRow> List = new List<DataRow>();

                dtProductos = Negocio.NegProducto.RecuperarPedido(Convert.ToInt32(_Pedido));
                if (dtProductos.Rows.Count == 0)
                {
                    MessageBox.Show("No puede hacer devolucion de una bodega diferente a la bodega asignada a esta maquina.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                foreach (DataRow dr in dtProductos.Rows)
                {
                    List.Add(dr);
                }

                xamDataPresenterProductosSolicitados.DataSource = List;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtCantidad.Text) < 0)
                {
                    MessageBox.Show("No se puede ingresar cantidades negativas", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                AgregarProducto();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AgregarProducto()
        {
            try
            {
                DataRow Item = null;
                PRODUCTO Prod = new PRODUCTO();

                Item = (DataRow)xamDataPresenterProductosSolicitados.ActiveDataItem; // Selecciono la fila que esta selecionada en el grid de articulos / Giovanny Tapia / 04/10/2012
                if (Item == null)
                {
                    MessageBox.Show("Debe seleccionar un producto para hacer la devolución", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                if (!NegParametros.ParametroDevolucionBienes())
                {
                    var Verifica = (from qs in PedidosDetalle.AsEnumerable() // Verifico si el item seleccionado ya fue añadido anteriormente / Giovanny Tapia / 04/10/2012
                                    where (qs.PRO_CODIGO.ToString() == Item["PRO_CODIGO"].ToString())
                                    select qs).ToList();

                    if (Verifica.Count > 0) // Verifico si el LINQ devolvio datos / Giovanny Tapia / 04/10/2012
                    {
                        MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000");
                        xamDataPresenterProductosSolicitados.Focus();
                        return;
                    }

                    if ((Convert.ToDecimal(Item["PDD_CANTIDAD"].ToString()) - Convert.ToDecimal(Item["CantidadDevuelta"].ToString())) - (Convert.ToDecimal(this.txtCantidad.Value)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                    {
                        PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                        PedidosDetalleItem.DevCodigo = 1;
                        PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(Item["PRO_CODIGO"]);
                        PedidosDetalleItem.PRO_DESCRIPCION = Item["PRO_DESCRIPCION"].ToString();
                        PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(txtCantidad.Value);
                        PedidosDetalleItem.DevDetValor = Convert.ToDecimal(Item["PDD_VALOR"].ToString());
                        PedidosDetalleItem.DevDetIva = ((((Convert.ToDecimal(Item["PDD_VALOR"].ToString()) * Convert.ToInt32(this.txtCantidad.Value))) * Convert.ToDecimal(Item["PDD_IVA"].ToString())) / 100);
                        PedidosDetalleItem.DevDetIvaTotal = Convert.ToDecimal(Item["PDD_VALOR"].ToString()) * Convert.ToInt32(this.txtCantidad.Value);
                        PedidosDetalleItem.PDD_CODIGO = Convert.ToInt64(Item["PDD_CODIGO"].ToString());

                        PedidosDetalle.Add(PedidosDetalleItem);

                        xamDataPresenter1.DataSource = PedidosDetalle.ToList();
                    }
                    else
                    {
                        MessageBox.Show("La cantidad devuelta no puede ser mayor a la cantidad ingresada en el pedido..", "His3000");
                    }
                }
                else
                {
                    if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 14) //Pueden hacer devoluciones para todo
                    {
                        var Verifica = (from qs in PedidosDetalle.AsEnumerable() // Verifico si el item seleccionado ya fue añadido anteriormente / Giovanny Tapia / 04/10/2012
                                        where (qs.PRO_CODIGO.ToString() == Item["PRO_CODIGO"].ToString())
                                        select qs).ToList();

                        if (Verifica.Count > 0) // Verifico si el LINQ devolvio datos / Giovanny Tapia / 04/10/2012
                        {
                            MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000");
                            xamDataPresenterProductosSolicitados.Focus();
                            return;
                        }

                        if ((Convert.ToDecimal(Item["PDD_CANTIDAD"].ToString()) - Convert.ToDecimal(Item["CantidadDevuelta"].ToString())) - (Convert.ToDecimal(this.txtCantidad.Value)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                        {
                            PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                            PedidosDetalleItem.DevCodigo = 1;
                            PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(Item["PRO_CODIGO"]);
                            PedidosDetalleItem.PRO_DESCRIPCION = Item["PRO_DESCRIPCION"].ToString();
                            PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(txtCantidad.Value);
                            PedidosDetalleItem.DevDetValor = Convert.ToDecimal(Item["PDD_VALOR"].ToString());
                            PedidosDetalleItem.DevDetIva = ((((Convert.ToDecimal(Item["PDD_VALOR"].ToString()) * Convert.ToDecimal(Convert.ToDouble(this.txtCantidad.Value)))) * Convert.ToDecimal(Item["PDD_IVA"].ToString())) / 100);
                            PedidosDetalleItem.DevDetIvaTotal = Convert.ToDecimal(Item["PDD_VALOR"].ToString()) * Convert.ToDecimal(Convert.ToDouble(this.txtCantidad.Value));
                            PedidosDetalleItem.PDD_CODIGO = Convert.ToInt64(Item["PDD_CODIGO"].ToString());

                            PedidosDetalle.Add(PedidosDetalleItem);

                            xamDataPresenter1.DataSource = PedidosDetalle.ToList();
                        }
                        else
                        {
                            MessageBox.Show("La cantidad devuelta no puede ser mayor a la cantidad ingresada en el pedido..", "His3000");
                        }
                    }
                    else
                    {
                        DataTable ProductoB = new DataTable();
                        ProductoB = NegProducto.RecuperarProductoSic(Item["PRO_CODIGO"].ToString());

                        if (ProductoB.Rows[0]["clasprod"].ToString().Trim() == "B")
                        {
                            MessageBox.Show("No tiene permisos para hacer devolución de bienes.\r\nConsulte con el Administrador.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            var Verifica = (from qs in PedidosDetalle.AsEnumerable() // Verifico si el item seleccionado ya fue añadido anteriormente / Giovanny Tapia / 04/10/2012
                                            where (qs.PRO_CODIGO.ToString() == Item["PRO_CODIGO"].ToString())
                                            select qs).ToList();

                            if (Verifica.Count > 0) // Verifico si el LINQ devolvio datos / Giovanny Tapia / 04/10/2012
                            {
                                MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000");
                                xamDataPresenterProductosSolicitados.Focus();
                                return;
                            }

                            if ((Convert.ToDecimal(Item["PDD_CANTIDAD"].ToString()) - Convert.ToDecimal(Item["CantidadDevuelta"].ToString())) - (Convert.ToDecimal(this.txtCantidad.Value)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                            {
                                PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                                PedidosDetalleItem.DevCodigo = 1;
                                PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(Item["PRO_CODIGO"]);
                                PedidosDetalleItem.PRO_DESCRIPCION = Item["PRO_DESCRIPCION"].ToString();
                                PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(txtCantidad.Value);
                                PedidosDetalleItem.DevDetValor = Convert.ToDecimal(Item["PDD_VALOR"].ToString());
                                PedidosDetalleItem.DevDetIva = ((((Convert.ToDecimal(Item["PDD_VALOR"].ToString()) * Convert.ToDecimal(Convert.ToDouble(this.txtCantidad.Value)))) * Convert.ToDecimal(Item["PDD_IVA"].ToString())) / 100);
                                PedidosDetalleItem.DevDetIvaTotal = Convert.ToDecimal(Item["PDD_VALOR"].ToString()) * Convert.ToDecimal(Convert.ToDouble(this.txtCantidad.Value));
                                PedidosDetalleItem.PDD_CODIGO = Convert.ToInt64(Item["PDD_CODIGO"].ToString());

                                PedidosDetalle.Add(PedidosDetalleItem);

                                xamDataPresenter1.DataSource = PedidosDetalle.ToList();
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {


            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Estas seguro que desea cancelar la devolución del pedido.?", "HIS3000", MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes))
            {
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (PedidosDetalle.Count > 0)
            {
                if (txtobservacion.Text != "")
                {
                    if ((MessageBox.Show("Estas seguro que desea guardar la devolucion? ", "HIS3000", MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes))
                    {
                        foreach (var item in PedidosDetalle)// validaciones no genere devoluciones en negativo // MarioValencia // 10-10-2023
                        {
                            decimal cantidadReal = NegPedidos.validaCantidad(_AtencionCodigo, Convert.ToString(item.PRO_CODIGO), Convert.ToInt64(_Pedido));
                            if (cantidadReal < 1)
                            {
                                MessageBox.Show("El Siguiente Producto: " + item.PRO_DESCRIPCION.ToString() + ", No Cuenta Con Stock Suficiente\r\nCantidad a devolver: " + item.DevDetCantidad + ".\r\nDebe Ser Removido Para Continuar", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                        }
                        DtoPedidoDevolucion Devolucion = new DtoPedidoDevolucion();

                        Devolucion.DevCodigo = 1;
                        Devolucion.Ped_Codigo = Convert.ToInt64(_Pedido);
                        Devolucion.DevFecha = Convert.ToDateTime(DateTime.Now.ToString());
                        Devolucion.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                        Devolucion.DevObservacion = txtobservacion.Text.ToUpper(); //AQUI SE VA A LA TABLA DE PEDIDO_DEVOLUCION LA RAZON DE DEVOLUCION AQUI ANTES ESTABA  = ""
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
                }
                else
                {
                    MessageBox.Show("Debe Indicar las Razones de la Devolución en \"Observación\"", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
                MessageBox.Show("No tiene productos para realizar devolución.", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void xamDataPresenter1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete))
            {
                DataRow Item = null;

                if (xamDataPresenter1.ActiveDataItem != null)
                {
                    DtoPedidoDevolucionDetalle Detalle = (DtoPedidoDevolucionDetalle)xamDataPresenter1.ActiveDataItem;
                    PedidosDetalle.Remove(Detalle);
                    xamDataPresenter1.DataSource = PedidosDetalle.ToList();
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un medicamento de la lista de medicamentos solicitados", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void xamDataPresenterProductosSolicitados_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                txtCantidad.Focus();
            txtCantidad.SelectAll();
        }

        private void xamDataPresenterProductosSolicitados_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                txtCantidad.Focus();
            }
        }

        private void txtCantidad_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                btnAdd.Focus();
        }

        private void btnAdd_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key.Equals(Key.Enter))
            //{
            //    try
            //    {
            //        AgregarProducto();
            //    }
            //    catch (Exception err)
            //    {
            //        MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
