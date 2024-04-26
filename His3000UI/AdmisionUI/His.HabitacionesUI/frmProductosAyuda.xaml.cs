using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FeserWard.Controls;
using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using System.Data;
using System.Globalization;

namespace His.HabitacionesUI
{
    /// <summary>
    /// Lógica de interacción para frmProductosAyuda.xaml
    /// </summary>
    public partial class frmProductosAyuda : Window
    {
        public IIntelliboxResultsProvider SqlServerCeProvider
        {
            get;
            private set;
        }

        #region variables

        //Declaración de variables
        PEDIDOS_DETALLE VerificaItems = new PEDIDOS_DETALLE();
        private List<PRODUCTO> listaProductosDisponibles;
        public List<PRODUCTO> listaProductosSolicitados;

        public List<PEDIDOS_DETALLE> PedidosDetalle = new List<PEDIDOS_DETALLE>();
        public PEDIDOS_DETALLE PedidosDetalleItem = null;

        public string descripcion;
        private Int32 codigoArea;
        private string vistaModo;

        private string _Medico = "";
        private string _Paciente = "";
        private string _Historia = "";
        private string _Atencion = "";
        private string _Edad = "";
        private string _Aseguradora = "";
        private Int32 _Rubro = 0;
        public Boolean CierraBoton = false;
        Boolean _todos = false;
        bool StockNegativo = false;
        int Resultado = 0;

        Int32 _CodigoEmpresa = 0;
        Int32 _CodigoConvenio = 0;
        public int bodega = 1;

        public bool _validador = false;
        public Int64 _usuarioActual = 0;
        #endregion

        #region constructor

        public frmProductosAyuda()
        {
            InitializeComponent();
            codigoArea = 0;
            inicializarValores();
        }
        public frmProductosAyuda(Int32 parCodigoArea, string Paciente, string Medico, string Historia, string Atencion, String Edad, String Aseguradora, Int32 Rubro, Int32 CodigoEmpresa, Int32 CodigoConvenio, Boolean Todos)
        {
            //if (His.Parametros.FacturaPAR.BodegaPorDefecto == 1) //
            //{
            //    if (NegAccesoOpciones.ParametroBodega())
            //    {
            //        His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
            //    }
            //}
            _Atencion = Atencion;
            _Historia = Historia;
            _Medico = Medico;
            _Paciente = Paciente;
            _Edad = Edad;
            _Aseguradora = Aseguradora;
            _Rubro = Rubro;
            _todos = Todos;
            _CodigoEmpresa = CodigoEmpresa;
            _CodigoConvenio = CodigoConvenio;

            codigoArea = parCodigoArea;


            InitializeComponent();
            inicializarValores();
            var SqlServerCeProvider = new LinqToEntitiesResultsProvider();
        }

        #endregion

        #region metodos generales

        private void inicializarValores()
        {
            listaProductosSolicitados = new List<PRODUCTO>();
            descripcion = null;
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                DataTable verificafactura = new DataTable();
                verificafactura = NegFactura.VerificaFactura(Convert.ToInt64(_Atencion.Trim()));
                string factura = verificafactura.Rows[0]["ate_factura_paciente"].ToString();
                string esc_codigo = verificafactura.Rows[0]["esc_codigo"].ToString();
                if (esc_codigo == "1")
                {
                    //cargo la lista de cantidad a paginar  
                    DataTable dtProductos = new DataTable();
                    DataTable dtPermisosUsuario = new DataTable();
                    List<DataRow> List = new List<DataRow>();
                    Resultado = 0;

                    Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "SALDOS NEGATIVOS PEDIDOS");
                    if (Resultado == 0)
                    {
                        StockNegativo = false;
                    }
                    else
                    {
                        StockNegativo = true;
                    }

                    List<int> listaCantidad = new List<int>() { 10, 20, 50, 100, 500, 1000 };
                    xamComboEditorCantidad.ItemsProvider.ItemsSource = listaCantidad;
                    xamComboEditorCantidad.SelectedIndex = 1;

                    if (codigoArea != 1)
                    {
                        //Valida el tipo de combenio para los rubros que no son medicamento ni insumos
                        if (_CodigoConvenio == 0)
                        {
                            if ((bool)xamCheckEditor1.IsChecked)
                            {
                                if (codigoArea > 0)
                                    if (_todos)  ///alexalex
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(2, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                    }
                                    else
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(2, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                    }
                                else
                               if (_todos)  ///alexalex
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(2, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                }
                                else
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(2, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                }
                            }
                            if (codigoArea > 0)
                            {
                                //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);
                                if (_todos)  ///alexalex
                                {
                                    //dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                }
                                else
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                }
                            }
                            else
                            {
                                if (_todos)  ///alexalex
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                }
                                else
                                {

                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                }
                            }
                        }
                        else
                        {
                            //dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, 10, _CodigoEmpresa, _CodigoConvenio);
                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                            if (dtProductos.Rows.Count == 0)
                            {
                                if ((bool)xamCheckEditor1.IsChecked)
                                {
                                    if (codigoArea > 0)
                                        if (_todos)  ///alexalex
                                        {
                                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(2, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                        }
                                        else
                                        {
                                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(2, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                        }
                                    else
                                   if (_todos)  ///alexalex
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(2, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                    }
                                    else
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(2, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                    }
                                }
                                if (codigoArea > 0)
                                    if (_todos)  ///alexalex
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                    }
                                    else
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                    }
                                else
                                    if (_todos)  ///alexalex
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                }
                                else
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                                }
                            }
                        }
                    }
                    else
                    {
                        //if(_CodigoConvenio==0)
                        if (codigoArea > 0)
                            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);
                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                        else
                            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                        //else
                        //    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);                
                    }

                    foreach (DataRow dr in dtProductos.Rows)
                    {
                        List.Add(dr);
                    }

                    //xamDataPresenterProductos.DataSource = listaProductosDisponibles;
                    //xamDataPresenterProductos.DataSource = dtProductos;
                    xamDataPresenterProductos.DataSource = List;
                    txtBuscar.Focus();
                    /*Verificacion de los permisos del usuario segun el perfil*/

                    Resultado = 0;
                    Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "MUESTRA PRECIOS PEDIDOS");

                    if (Resultado == 0)
                    {
                        xamDataPresenterProductos.FieldLayouts[0].Fields[4].Visibility = Visibility.Collapsed;
                        xamDataPresenterProductos.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
                        //xamDataPresenterProductos.FieldLayouts[0].Fields[6].Visibility = Visibility.Collapsed;
                    }
                    xamDataPresenterProductos.FieldLayouts[0].Fields[7].Visibility = Visibility.Collapsed;
                    this.lblAtencion.Content = _Atencion;
                    this.lblHistoria.Content = _Historia;
                    this.lblMedico.Content = _Medico;
                    this.lblPaciente.Content = _Paciente;
                    this.lblAseguradora.Content = _Aseguradora;
                    this.lblEdad.Content = _Edad;

                    //cargo los valores por defecto de la grilla
                }
                else
                {
                    MessageBox.Show("Paciente Fue Dado de Alta No Se Puede Hacer Pedidos, Consulte Con Caja", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Stop);
                    this.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            //try
            //{
            //    //cargo la lista de cantidad a paginar
            //    DataTable dtProductos = new DataTable();
            //    DataTable dtPermisosUsuario = new DataTable();
            //    List<DataRow> List = new List<DataRow>();
            //    Resultado = 0;

            //    Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "SALDOS NEGATIVOS PEDIDOS");
            //    if (Resultado == 0)
            //    {
            //        StockNegativo = false;
            //    }
            //    else
            //    {
            //        StockNegativo = true;
            //    }

            //    List<int> listaCantidad = new List<int>() { 10, 20, 50, 100, 500, 1000 };
            //    xamComboEditorCantidad.ItemsProvider.ItemsSource = listaCantidad;
            //    xamComboEditorCantidad.SelectedIndex = 1;

            //    if (codigoArea != 1)
            //    {
            //        //Valida el tipo de combenio para los rubros que no son medicamento ni insumos
            //        if (_CodigoConvenio == 0)
            //        {
            //            if (codigoArea > 0)
            //                //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);
            //                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
            //            else
            //                //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
            //                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
            //        }
            //        else
            //        {
            //            dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
            //            if (dtProductos.Rows.Count == 0)
            //            {
            //                if (codigoArea > 0)
            //                    //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);
            //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
            //                else
            //                    //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
            //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
            //            }
            //        }

            //    }
            //    else 
            //    {
            //        //if(_CodigoConvenio==0)
            //            if (codigoArea > 0)
            //            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);
            //                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
            //            else
            //            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
            //                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
            //        //else
            //        //    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);                
            //    }

            //    foreach (DataRow dr in dtProductos.Rows)
            //    {
            //        List.Add(dr);
            //    }

            //    //xamDataPresenterProductos.DataSource = listaProductosDisponibles;
            //    //xamDataPresenterProductos.DataSource = dtProductos;
            //    xamDataPresenterProductos.DataSource = List;
            //    txtBuscar.Focus();
            //    /*Verificacion de los permisos del usuario segun el perfil*/

            //    Resultado = 0;
            //    Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "MUESTRA PRECIOS PEDIDOS");

            //    if (Resultado == 0)
            //    {
            //        xamDataPresenterProductos.FieldLayouts[0].Fields[4].Visibility = Visibility.Collapsed;
            //        xamDataPresenterProductos.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
            //        //xamDataPresenterProductos.FieldLayouts[0].Fields[6].Visibility = Visibility.Collapsed;
            //    }

            //    this.lblAtencion.Content = _Atencion;
            //    this.lblHistoria.Content=_Historia ;
            //    this.lblMedico.Content = _Medico;
            //    this.lblPaciente.Content = _Paciente;
            //    this.lblAseguradora.Content = _Aseguradora;
            //    this.lblEdad.Content = _Edad;

            //    //cargo los valores por defecto de la grilla
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message);
            //}

        }
        #region comentada
        //public void CargarGrid() //Se comenta por que no tiene ninguna referencia //Mario 15/02/2023
        //{
        //    DataTable dtProductos = new DataTable();
        //    DataTable dtPermisosUsuario = new DataTable();
        //    List<DataRow> List = new List<DataRow>();
        //    Resultado = 0;

        //    Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "SALDOS NEGATIVOS PEDIDOS");
        //    if (Resultado == 0)
        //    {
        //        StockNegativo = false;
        //    }
        //    else
        //    {
        //        StockNegativo = true;
        //    }

        //    List<int> listaCantidad = new List<int>() { 10, 20, 50, 100, 500, 1000 };
        //    xamComboEditorCantidad.ItemsProvider.ItemsSource = listaCantidad;
        //    xamComboEditorCantidad.SelectedIndex = 1;

        //    if (codigoArea != 1)
        //    {
        //        //Valida el tipo de combenio para los rubros que no son medicamento ni insumos
        //        if (_CodigoConvenio == 0)
        //        {
        //            if (codigoArea > 0)
        //            {
        //                //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);
        //                if (_todos)  ///alexalex
        //                {
        //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //                }
        //                else
        //                {
        //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //                }
        //            }
        //            else
        //            {
        //                if (_todos)  ///alexalex
        //                {
        //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //                }
        //                else
        //                {
        //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //            if (dtProductos.Rows.Count == 0)
        //            {
        //                if (codigoArea > 0)
        //                    if (_todos)  ///alexalex
        //                    {
        //                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //                    }
        //                    else
        //                    {
        //                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //                    }
        //                else
        //                    if (_todos)  ///alexalex
        //                {
        //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //                }
        //                else
        //                {
        //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //if(_CodigoConvenio==0)
        //        if (codigoArea > 0)
        //            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);
        //            dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //        else
        //            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
        //            dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
        //        //else
        //        //    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);                
        //    }

        //    foreach (DataRow dr in dtProductos.Rows)
        //    {
        //        List.Add(dr);
        //    }

        //    //xamDataPresenterProductos.DataSource = listaProductosDisponibles;
        //    //xamDataPresenterProductos.DataSource = dtProductos;
        //    xamDataPresenterProductos.DataSource = List;
        //    txtBuscar.Focus();
        //}
        #endregion
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dtProductos = new DataTable();
                List<DataRow> List = new List<DataRow>();

                List<int> listaCantidad = new List<int>() { 10, 20, 50, 100, 500, 1000 };
                
                
                
                if (txtBuscar.Text != "")
                {
                    //cargo la lista de cantidad a paginar


                    xamComboEditorCantidad.ItemsProvider.ItemsSource = listaCantidad;
                    xamComboEditorCantidad.SelectedIndex = 1;

                    if (codigoArea != 1)
                    {
                        if (_CodigoConvenio == 0)
                        {
                            if (codigoArea > 0)
                            {
                                //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);

                                if (xamCheckEditor1.IsChecked == true)
                                {
                                    if (_todos)  ///alexalex
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(2, txtBuscar.Text.ToString(), _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio); // Busca por el generico / Giovanny Tapia / 18/10/2012

                                    }
                                    else
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(2, txtBuscar.Text.ToString(), _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio); // Busca por el generico / Giovanny Tapia / 18/10/2012
                                    }
                                }

                                else
                                {
                                    if (_todos)  ///alexalex
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, txtBuscar.Text.ToString(), _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                                    }
                                    else
                                    {
                                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, txtBuscar.Text.ToString(), _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                                    }
                                }
                            }
                        }
                        else
                        {
                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, txtBuscar.Text, _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                            if (dtProductos.Rows.Count == 0)
                            {

                                if (_todos)  ///alexalex
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, txtBuscar.Text.ToString(), _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                                }
                                else
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, txtBuscar.Text.ToString(), _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                                }

                            }
                        }
                    }
                    else
                    {
                        if (codigoArea > 0)
                        {

                            if (xamCheckEditor1.IsChecked == true)
                            {
                                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(2, txtBuscar.Text.ToString(), _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                            }

                            else
                            {
                                //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
                                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, txtBuscar.Text.ToString(), _Rubro, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                            }

                        }
                    }

                    foreach (DataRow dr in dtProductos.Rows)
                    {
                        List.Add(dr);
                    }

                    //xamDataPresenterProductos.DataSource = listaProductosDisponibles;
                    //xamDataPresenterProductos.DataSource = dtProductos;
                    xamDataPresenterProductos.DataSource = List;

                    if (List.Count > 0)
                    {
                        xamDataPresenterProductos.Focus();


                    }

                    //cargo los valores por defecto de la grilla
                }
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            //try
            //{
            //    //cargo la lista de cantidad a paginar
            //    DataTable dtProductos = new DataTable();

            //    List<DataRow> List = new List<DataRow>();

            //    List<int> listaCantidad = new List<int>() { 10, 20, 50, 100, 500, 1000 };
            //    xamComboEditorCantidad.ItemsProvider.ItemsSource = listaCantidad;
            //    xamComboEditorCantidad.SelectedIndex = 1;

            //    if (codigoArea != 1)
            //    {
            //        if (_CodigoConvenio == 0)
            //        {
            //            if (codigoArea > 0)
            //            {
            //                //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);

            //                if (xamCheckEditor1.IsChecked == true)
            //                {
            //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(2, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por el generico / Giovanny Tapia / 18/10/2012
            //                }

            //                else
            //                {
            //                    //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
            //                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
            //                }
            //            }
            //        }
            //        else
            //        {
            //            dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, txtBuscar.Value.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
            //            if (dtProductos.Rows.Count == 0)
            //            {
            //                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (codigoArea > 0)
            //        { 

            //            if (xamCheckEditor1.IsChecked == true)
            //            {
            //                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(2, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
            //            }

            //            else
            //            {
            //                //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
            //                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, txtBuscar.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
            //            }

            //        }
            //    }

            //    foreach (DataRow dr in dtProductos.Rows)
            //    {
            //        List.Add(dr);
            //    }

            //    //xamDataPresenterProductos.DataSource = listaProductosDisponibles;
            //    //xamDataPresenterProductos.DataSource = dtProductos;
            //    xamDataPresenterProductos.DataSource = List;

            //    if (List.Count > 0)
            //    {
            //        xamDataPresenterProductos.Focus();


            //    }

            //    //cargo los valores por defecto de la grilla

            //}

            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message);
            //}

        }


        private void xamTextEditorCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

                if (char.IsNumber((char)e.Key) || e.Key.ToString() == cc.NumberFormat.NumberDecimalSeparator)
                    e.Handled = true;
                else
                    e.Handled = false;

                //if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                //    e.Handled = false;
                //else if (e.Key == Key.Enter)
                //{
                //    addProducto();
                //    txtCantidad.Text = "1";
                //}
                //else
                //e.Handled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void xamDataPresenterProductos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                //addProducto();
                //AgregarProducto();
                txtCantidad.Focus();
            txtCantidad.SelectAll();

            /*AGREGO EL VALOR DEL ITEM A LA CAJA DE TEXTO ESTO ES TEMPORAL*/
            DataRow Item = null;
            if (xamDataPresenterProductos.ActiveDataItem != null)
            {
                Item = (DataRow)xamDataPresenterProductos.ActiveDataItem;
                this.txtValorItem.Text = Convert.ToDecimal(Item["VALOR"].ToString()).ToString();
            }



        }
        private void addProducto()
        {
            if (xamDataPresenterProductos.ActiveDataItem != null)
            {
                Int16 cantidad = Convert.ToInt16(txtCantidad.Text);
                if (cantidad > 0)
                {
                    PRODUCTO producto = (PRODUCTO)xamDataPresenterProductos.ActiveDataItem;
                    producto.PRO_CANTIDAD = cantidad;
                    listaProductosDisponibles.Remove(producto);
                    //xamDataPresenterProductos.DataSource = listaProductosDisponibles.ToList(); //Cambios Edgar Comento esto para que no se pierda la busqueda anterior
                    listaProductosSolicitados.Add(producto);
                    xamDataPresenterProductosSolicitados.DataSource = listaProductosSolicitados.ToList();
                    txtBuscar.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor seleccione la cantidad del producto que necesita", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    //txtCantidad.Focus();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un medicamento de la lista de medicamentos disponibles", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    if ((DataRow)xamDataPresenterProductos.ActiveDataItem == null)
                    {
                        MessageBox.Show("Debe seleccionar un producto para continuar...", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                        xamDataPresenterProductos.Focus();
                    }
                    else if (Convert.ToDecimal(txtCantidad.Text) > 0)
                    {
                        AgregarProducto();
                        txtCantidad.Text = "1";

                        txtBuscar.Focus();
                        txtBuscar.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Solo permite cantidades mayores a 0", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtCantidad.Text = "";
                        txtCantidad.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Valor inválido.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtCantidad.Text = "";
                    txtCantidad.Focus();
                    return;
                }
                //catch(NullReferenceException ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}



            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SumaMismoItem()
        {
            try
            {
                DataRow Item = null;
                Item = (DataRow)xamDataPresenterProductos.ActiveDataItem; // Selecciono la fila que esta selecionada en el grid de articulos / Giovanny Tapia / 04/10/2012            
                VerificaItems.PDD_CANTIDAD = VerificaItems.PDD_CANTIDAD + Convert.ToDecimal(this.txtCantidad.Text);
                VerificaItems.PDD_IVA = Math.Round(((((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100), 2);
                VerificaItems.PDD_TOTAL = (VerificaItems.PDD_CANTIDAD * VerificaItems.PDD_VALOR) + VerificaItems.PDD_IVA;

                foreach (var item in PedidosDetalle)
                {

                    if (item.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value.ToString() == VerificaItems.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value.ToString())
                    {

                        PedidosDetalle.Remove(item);
                        break;

                    }

                }

                PedidosDetalle.Add(VerificaItems);
                xamDataPresenterProductosSolicitados.DataSource = PedidosDetalle.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AgregarProducto()
        {

            //addProducto();
            //txtCantidad.Text = "1";          
            Decimal Valor = 0;
            bool entero = true;

            PRODUCTO Prod = new PRODUCTO();
            //DataTable Prod1 = new DataTable();
            DataRow Item = null;
            Item = (DataRow)xamDataPresenterProductos.ActiveDataItem; // Selecciono la fila que esta selecionada en el grid de articulos / Giovanny Tapia / 04/10/2012
                                                                      //if (Item["DIVISION"].ToString()=="IESS")
                                                                      //  Prod1 = Negocio.NegProducto.RecuperarProductoID1(Item["CODIGO"].ToString());// Creo un objeto con el item seleccionado / Giovanny Tapia / 04/10/2012
                                                                      //else            
            Prod = Negocio.NegProducto.RecuperarProductoID(Convert.ToInt32(Item["CODIGO"].ToString()));// Creo un objeto con el item seleccionado / Giovanny Tapia / 04/10/2012

            VerificaItems = (from qs in PedidosDetalle.AsEnumerable() // Verifico si el item seleccionado ya fue añadido anteriormente / Giovanny Tapia / 04/10/2012
                             where (qs.PRODUCTOReference.EntityKey == Prod.EntityKey)
                             select qs).FirstOrDefault();

            /*Verifica si el producto admite o no cantidades decimales /  Giovanny Tapia / 06/02/2013 */
            Valor = 0;
            Valor = Convert.ToDecimal(this.txtCantidad.Text) - Math.Round((Convert.ToDecimal(this.txtCantidad.Text)), 0);
            string prueba = Item["Cantidad"].ToString();

            char[] test = txtCantidad.Text.ToCharArray();

            for (int i = 0; i < test.Length; i++)
            {
                if (test[i] == '.')
                {
                    entero = false;
                }
                //else if (test[i] == ',')
                //{
                //    entero = false;
                //}
            }

            //if (Valor > 0 && Item["Cantidad"].ToString() == "False")
            //{
            //    MessageBox.Show("Este producto no permite cantidades decimales.");
            //    return;
            //}
            if (Item["Cantidad"].ToString() == "False" && entero == false)
            {
                MessageBox.Show("Este producto no permite cantidades decimales.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            DataTable cantidadReal = new DataTable();
            cantidadReal = NegHabitaciones.VerificaCantidadStock(Convert.ToInt64(Item["CODIGO"].ToString()), Sesion.bodega);
            /********************************************************************************************/

            if (VerificaItems != null) // Verifico si el LINQ devolvio datos / Giovanny Tapia / 04/10/2012
            {


                Int64 w = Convert.ToInt64(cantidadReal.Rows[0][0].ToString());//Convert.ToInt16(VerificaItems.PDD_CANTIDAD.ToString());
                decimal z = Convert.ToDecimal(Item["STOCK"].ToString()) - Convert.ToDecimal(this.txtCantidad.Value);
                if ((VerificaItems.PDD_CANTIDAD + Convert.ToDecimal(this.txtCantidad.Text)) <= w) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                {
                    System.Windows.Forms.DialogResult ok;
                    //SumaMismoItem();
                    ok = System.Windows.Forms.MessageBox.Show("El item seleccionado ya esta ingresado. \r\n¿Desea Sumarlo?.", "HIS3000", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //if (MessageBox.Show("DESEA GUARDAR LA ADMISIÓN", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    if (ok == System.Windows.Forms.DialogResult.Yes)
                    {
                        SumaMismoItem();
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("No hay stock suficiente..", "His3000");
                    return;
                }
            }

            if (StockNegativo == false)
            {
                if (Convert.ToDecimal(cantidadReal.Rows[0][0].ToString()) - (Convert.ToDecimal(this.txtCantidad.Value)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                {
                    PedidosDetalleItem = new PEDIDOS_DETALLE();
                    PedidosDetalleItem.PDD_CODIGO = 1;
                    PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                    PedidosDetalleItem.PDD_CANTIDAD = Convert.ToDecimal(this.txtCantidad.Value);
                    PedidosDetalleItem.PRO_DESCRIPCION = Item["PRODUCTO"].ToString();
                    int rubroDieta = NegPedidos.ValidarRubro(Item["CODIGO"].ToString());
                    if (NegPedidos.ParametroDieta() && rubroDieta == 4)
                    {
                        int te_codigo = NegPedidos.ValidarAseguradoraPaciente(Convert.ToInt64(_Atencion.Trim()));
                        if (te_codigo == 1)
                        {
                            PedidosDetalleItem.PDD_VALOR = 0;
                            PedidosDetalleItem.PDD_TOTAL = 0;
                        }

                        else
                        {
                            PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(this.txtValorItem.Text);
                            PedidosDetalleItem.PDD_TOTAL = (Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value)) + ((((Convert.ToDecimal(Item["VALOR"].ToString()) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100);
                        }

                    }
                    else
                    {
                        PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(this.txtValorItem.Text);
                        PedidosDetalleItem.PDD_TOTAL = (Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value)) + ((((Convert.ToDecimal(Item["VALOR"].ToString()) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100);
                    }

                    PedidosDetalleItem.PDD_IVA = ((((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100);
                    PedidosDetalleItem.PDD_ESTADO = true;
                    PedidosDetalleItem.PDD_COSTO = 0;
                    PedidosDetalleItem.PDD_FACTURA = null;
                    PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                    PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                    PedidosDetalleItem.PDD_RESULTADO = null;
                    PedidosDetalleItem.PRO_CODIGO_BARRAS = Item["CODIGO"].ToString();
                    PedidosDetalleItem.PRO_BODEGA_SIC = Sesion.bodega;

                    //PedidosDetalle.Add(PedidosDetalleItem); //Cambio Edgar 20210422 se comenta esto para que el producto que se agregue siempre vaya al principio y no al final.

                    PedidosDetalle.Insert(0, PedidosDetalleItem);

                    xamDataPresenterProductosSolicitados.DataSource = PedidosDetalle.ToList();

                    if (Resultado == 0) // verifico si los precios se muestran o no por perfil / Giovanny Tapia / 15/05/2013
                    {
                        xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[3].Visibility = Visibility.Collapsed;
                        xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[4].Visibility = Visibility.Collapsed;
                        xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
                    }

                    //txtCantidad.Focus();

                }
                else
                {
                    MessageBox.Show("No hay stock suficiente..", "His3000");
                }

                //if (Resultado == 0)
                //{
                //    if (Convert.ToDecimal(Item["STOCK"].ToString()) - (Convert.ToDecimal(this.txtCantidad.Value)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                //    {

                //        PedidosDetalleItem = new PEDIDOS_DETALLE();
                //        PedidosDetalleItem.PDD_CODIGO = 1;
                //        //if (Item["DIVISION"].ToString() == "IESS")
                //        //PedidosDetalleItem.PRODUCTOReference.EntityKey = Item["PRODUCTO"].ToString() ;
                //        //else
                //        PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                //        PedidosDetalleItem.PDD_CANTIDAD = Convert.ToDecimal(this.txtCantidad.Value);
                //        PedidosDetalleItem.PRO_DESCRIPCION = Item["PRODUCTO"].ToString();
                //        PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(this.txtValorItem.Text);
                //        PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100), 2);
                //        PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value)), 2) + Math.Round(((((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100), 2);
                //        PedidosDetalleItem.PDD_ESTADO = true;
                //        PedidosDetalleItem.PDD_COSTO = 0;
                //        PedidosDetalleItem.PDD_FACTURA = null;
                //        PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                //        PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                //        PedidosDetalleItem.PDD_RESULTADO = null;
                //        PedidosDetalleItem.PRO_CODIGO_BARRAS = Item["CODIGO"].ToString();

                //        PedidosDetalle.Add(PedidosDetalleItem);

                //        xamDataPresenterProductosSolicitados.DataSource = PedidosDetalle.ToList();

                //        if (Resultado == 0) // verifico si los precios se muestran o no por perfil / Giovanny Tapia / 15/05/2013
                //        {
                //            xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[3].Visibility = Visibility.Collapsed;
                //            xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[4].Visibility = Visibility.Collapsed;
                //            xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
                //        }

                //        //txtCantidad.Focus();

                //    }
                //    else
                //    {
                //        MessageBox.Show("No hay stock suficiente.. Maximo a pedir: " + Item["STOCK"].ToString(), "His3000");
                //    }
            }
            else
            {
                PedidosDetalleItem = new PEDIDOS_DETALLE();
                PedidosDetalleItem.PDD_CODIGO = 1;
                PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                PedidosDetalleItem.PDD_CANTIDAD = Convert.ToDecimal(this.txtCantidad.Value);
                PedidosDetalleItem.PRO_DESCRIPCION = Item["PRODUCTO"].ToString();
                PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(this.txtValorItem.Text);
                PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100), 2);
                PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value)), 2) + Math.Round(((((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100), 2);
                PedidosDetalleItem.PDD_ESTADO = true;
                PedidosDetalleItem.PDD_COSTO = 0;
                PedidosDetalleItem.PDD_FACTURA = null;
                PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                PedidosDetalleItem.PDD_RESULTADO = null;
                PedidosDetalleItem.PRO_CODIGO_BARRAS = Item["CODIGO"].ToString();

                PedidosDetalle.Add(PedidosDetalleItem);

                xamDataPresenterProductosSolicitados.DataSource = PedidosDetalle.ToList();

                if (Resultado == 0) // verifico si los precios se muestran o no por perfil / Giovanny Tapia / 15/05/2013
                {
                    xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[3].Visibility = Visibility.Collapsed;
                    xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[4].Visibility = Visibility.Collapsed;
                    xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
                }
            }


        }

        //private void AgregarProducto()
        //{

        //    //addProducto();
        //    //txtCantidad.Text = "1";          
        //    Decimal Valor = 0;

        //    DataRow Item = null;
        //    PRODUCTO Prod = new PRODUCTO();

        //    Item = (DataRow)xamDataPresenterProductos.ActiveDataItem; // Selecciono la fila que esta selecionada en el grid de articulos / Giovanny Tapia / 04/10/2012
        //    if(_CodigoConvenio==0)
        //        Prod = Negocio.NegProducto.RecuperarProductoID(Convert.ToInt32(Item["CODIGO"].ToString()));// Creo un objeto con el item seleccionado / Giovanny Tapia / 04/10/2012
        //    else
        //        Prod = Negocio.NegProducto.RecuperarProductoID(Convert.ToInt32(Item["CODIGO"].ToString()));

        //    var Verifica = (from qs in PedidosDetalle.AsEnumerable() // Verifico si el item seleccionado ya fue añadido anteriormente / Giovanny Tapia / 04/10/2012
        //                    where (qs.PRODUCTOReference.EntityKey == Prod.EntityKey)
        //                    select qs).ToList();

        //    /*Verifica si el producto admite o no cantidades decimales /  Giovanny Tapia / 06/02/2013 */

        //    Valor = Convert.ToDecimal(this.txtCantidad.Text) - Math.Round((Convert.ToDecimal(this.txtCantidad.Text)), 0);

        //    if (Valor > 0 && Item["Cantidad"].ToString() == "False")
        //    {
        //        MessageBox.Show("Este producto no permite cantidades decimales.");
        //        return;
        //    }

        //    /********************************************************************************************/

        //    if (Verifica.Count > 0) // Verifico si el LINQ devolvio datos / Giovanny Tapia / 04/10/2012
        //    {
        //        MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000");
        //        return;
        //    }

        //    if (StockNegativo == false)
        //    {
        //        if (Convert.ToDecimal(Item["STOCK"].ToString()) - (Convert.ToDecimal(this.txtCantidad.Value)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
        //        {
        //            PedidosDetalleItem = new PEDIDOS_DETALLE();
        //            PedidosDetalleItem.PDD_CODIGO = 1;
        //            PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
        //            PedidosDetalleItem.PDD_CANTIDAD = Convert.ToDecimal(this.txtCantidad.Value);
        //            PedidosDetalleItem.PRO_DESCRIPCION = Item["PRODUCTO"].ToString();
        //            PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(this.txtValorItem.Text);
        //            PedidosDetalleItem.PDD_IVA = ((((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100);
        //            PedidosDetalleItem.PDD_TOTAL = (Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value)) + ((((Convert.ToDecimal(Item["VALOR"].ToString()) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100);
        //            PedidosDetalleItem.PDD_ESTADO = true;
        //            PedidosDetalleItem.PDD_COSTO = 0;
        //            PedidosDetalleItem.PDD_FACTURA = null;
        //            PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
        //            PedidosDetalleItem.PDD_FECHA_FACTURA = null;
        //            PedidosDetalleItem.PDD_RESULTADO = null;
        //            PedidosDetalleItem.PRO_CODIGO_BARRAS = Item["CODIGO"].ToString();

        //            PedidosDetalle.Add(PedidosDetalleItem);

        //            xamDataPresenterProductosSolicitados.DataSource = PedidosDetalle.ToList();

        //            if (Resultado == 0) // verifico si los precios se muestran o no por perfil / Giovanny Tapia / 15/05/2013
        //            {
        //                xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[3].Visibility = Visibility.Collapsed;
        //                xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[4].Visibility = Visibility.Collapsed;
        //                xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
        //            }

        //            //txtCantidad.Focus();

        //        }
        //        else
        //        {
        //            MessageBox.Show("No hay stock suficiente..", "His3000");
        //        }
        //    }
        //    else
        //    {
        //        PedidosDetalleItem = new PEDIDOS_DETALLE();
        //        PedidosDetalleItem.PDD_CODIGO = 1;
        //        PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
        //        PedidosDetalleItem.PDD_CANTIDAD = Convert.ToDecimal(this.txtCantidad.Value);
        //        PedidosDetalleItem.PRO_DESCRIPCION = Item["PRODUCTO"].ToString();
        //        PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(this.txtValorItem.Text);
        //        PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100), 2);
        //        PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value)), 2) + Math.Round(((((Convert.ToDecimal(this.txtValorItem.Text) * Convert.ToDecimal(this.txtCantidad.Value))) * (Convert.ToDecimal(Item["IVA"].ToString()))) / 100), 2);
        //        PedidosDetalleItem.PDD_ESTADO = true;
        //        PedidosDetalleItem.PDD_COSTO = 0;
        //        PedidosDetalleItem.PDD_FACTURA = null;
        //        PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
        //        PedidosDetalleItem.PDD_FECHA_FACTURA = null;
        //        PedidosDetalleItem.PDD_RESULTADO = null;
        //        PedidosDetalleItem.PRO_CODIGO_BARRAS = Item["CODIGO"].ToString();

        //        PedidosDetalle.Add(PedidosDetalleItem);

        //        xamDataPresenterProductosSolicitados.DataSource = PedidosDetalle.ToList();

        //        if (Resultado == 0) // verifico si los precios se muestran o no por perfil / Giovanny Tapia / 15/05/2013
        //        {
        //            xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[3].Visibility = Visibility.Collapsed;
        //            xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[4].Visibility = Visibility.Collapsed;
        //            xamDataPresenterProductosSolicitados.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
        //        }

        //        //txtCantidad.Focus();
        //    }

        //}

        private void xamDataPresenterProductosSolicitados_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete))
            {
                //if (xamDataPresenterProductosSolicitados.ActiveDataItem != null)
                //{
                //PRODUCTO producto = (PRODUCTO )xamDataPresenterProductosSolicitados.ActiveDataItem ;
                //listaProductosSolicitados.Remove(producto);
                //xamDataPresenterProductosSolicitados.DataSource = listaProductosSolicitados.ToList(); 

                //}
                //else
                //{
                //    MessageBox.Show("Por favor seleccione un medicamento de la lista de medicamentos solicitados", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //}

                DataRow Item = null;

                if (xamDataPresenterProductosSolicitados.ActiveDataItem != null)
                {
                    PEDIDOS_DETALLE Detalle = (PEDIDOS_DETALLE)xamDataPresenterProductosSolicitados.ActiveDataItem;
                    PedidosDetalle.Remove(Detalle);
                    xamDataPresenterProductosSolicitados.DataSource = null;
                    xamDataPresenterProductosSolicitados.DataSource = PedidosDetalle.ToList();

                }
                else
                {
                    MessageBox.Show("Por favor seleccione un medicamento de la lista de medicamentos solicitados", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void xamDataPresenterProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //addProducto();
            //AgregarProducto();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //listaProductosSolicitados = null;
            if (PedidosDetalle.Count != 0)
            {
                if ((MessageBox.Show("Esta seguro que desea cancelar el pedido", "HIS3000", MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes))
                {
                    PedidosDetalle = null;
                    CierraBoton = true;
                    this.Close();
                }
            }
            else
            {
                CierraBoton = true;
                this.Close();
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            DataTable verificafactura = new DataTable();
            verificafactura = NegFactura.VerificaFactura(Convert.ToInt64(_Atencion));
            string factura = verificafactura.Rows[0]["ate_factura_paciente"].ToString();
            string esc_codigo = verificafactura.Rows[0]["esc_codigo"].ToString();
            if (esc_codigo == "1")
            {
                DataTable validador = new DataTable();
                validador = NegAtenciones.RecuperaPermisos();
                var usuario = new frm_ClavePedido();

                if (validador.Rows[0][0].ToString() == "True")
                {
                    usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    usuario.ShowDialog();
                    _validador = usuario.validador;
                    _usuarioActual = usuario.usuarioActual;
                }
                else
                {
                    _usuarioActual = Entidades.Clases.Sesion.codUsuario;
                    _validador = true; //Solo para la alianza
                }


                foreach (var item in PedidosDetalle)
                {
                    int i = 0;
                    DataTable cantidadReal = new DataTable();
                    cantidadReal = NegHabitaciones.VerificaCantidadStock(Convert.ToInt64(item.PRO_CODIGO_BARRAS.ToString()), Sesion.bodega);
                    if (Convert.ToDecimal(cantidadReal.Rows[i][0].ToString()) < Convert.ToDecimal(item.PDD_CANTIDAD.Value.ToString()))
                    {
                        MessageBox.Show("El Siguiente Producto: " + item.PRO_DESCRIPCION.ToString() + ", No Cuenta Con Stock Suficiente\r\nStock Solicitado: " + item.PDD_CANTIDAD.Value.ToString() + "\r\nStock Físico: " + cantidadReal.Rows[i][0].ToString() + ".\r\nDebe Ser Removido Para Continuar", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                        //////Cambios Edgar 20210323
                        ///CargarGrid();
                        return;
                        ////--------
                    }
                    i++;
                }

                ////Cambios Edgar 20210329 // se cambia para evitar ver el mensaje de confirmacion de pedido
                descripcion = txtDescripcion.Text.Trim();
                CierraBoton = true;
                //Mario por no valida stock
                //this.Close();

                //frm_ClavePedido x = new frm_ClavePedido();
                //x.ShowDialog();
                ////if(x.aceptado == true)
                ////{
                ////    descripcion = txtDescripcion.Text.Trim();
                ////    CierraBoton = true;
                ////    this.Close();
                ////}

                //if ((MessageBox.Show("Realmente desea guardar el pedido???", "HIS3000", MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes))
                //{
                //    descripcion = txtDescripcion.Text.Trim();
                //    CierraBoton = true;
                this.Close();
                //}
            }
            else
                MessageBox.Show("Paciente Con Cuenta Cerrada, Consulte Con Caja", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Stop);

        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                btnBuscar_Click(null, null);
                //btnBuscar.Focus();
            }
        }

        private void xamDataPresenterProductos_Initialized(object sender, EventArgs e)
        {
            try
            {
                if (Parametros.GeneralPAR.CodigoAreaFarmacia != codigoArea)
                {
                    //productos disponibles
                    //xamDataPresenterProductos.FieldLayouts[0].Fields["PRO_NOMBRE_GENERICO"].Visibility = Visibility.Collapsed;
                    //xamDataPresenterProductos.FieldLayouts[0].Fields["PRO_CONC"].Visibility = Visibility.Collapsed;
                    //xamDataPresenterProductos.FieldLayouts[0].Fields["PRO_FF"].Visibility = Visibility.Collapsed;
                    //xamDataPresenterProductos.FieldLayouts[0].Fields["PRO_PRESENTACION"].Visibility = Visibility.Collapsed;
                    //xamDataPresenterProductos.FieldLayouts[0].Fields["PRO_VIA_ADMINISTRACION"].Visibility = Visibility.Collapsed;
                    //xamDataPresenterProductos.FieldLayouts[0].Fields["PRO_REFERENCIA"].Visibility = Visibility.Collapsed;
                    //xamDataPresenterProductos.FieldLayouts[0].Fields["PRO_ACCION_TERAPEUTICA"].Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void xamDataPresenterProductosSolicitados_Initialized(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void xamCheckEditor1_ContextMenuClosing(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((DataRow)xamDataPresenterProductos.ActiveDataItem == null)
                {
                    MessageBox.Show("Debe seleccionar un producto para continuar...", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                    xamDataPresenterProductos.Focus();
                }
                else if (Convert.ToDecimal(txtCantidad.Text) > 0)
                {
                    AgregarProducto();
                    txtCantidad.Text = "1";

                    txtBuscar.Focus();
                    txtBuscar.Text = "";
                }
                else
                {
                    MessageBox.Show("Solo permite cantidades mayores a 0", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtCantidad.Text = "";
                    txtCantidad.Focus();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))

                btnAdd.Focus();

        }

        private void btnBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //cargo la lista de cantidad a paginar
                DataTable dtProductos = new DataTable();

                List<DataRow> List = new List<DataRow>();

                List<int> listaCantidad = new List<int>() { 10, 20, 50, 100, 500, 1000 };
                xamComboEditorCantidad.ItemsProvider.ItemsSource = listaCantidad;
                xamComboEditorCantidad.SelectedIndex = 1;

                if (codigoArea > 0)
                    //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);

                    if (xamCheckEditor1.IsChecked == true)
                    {
                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(2, txtBuscar.Text.ToString(), 3, codigoArea, _CodigoEmpresa, _CodigoConvenio); // Busca por el generico / Giovanny Tapia / 18/10/2012
                    }

                    else
                    {
                        //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, txtBuscar.Text.ToString(), 3, codigoArea, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                    }
                foreach (DataRow dr in dtProductos.Rows)
                {
                    List.Add(dr);
                }

                //xamDataPresenterProductos.DataSource = listaProductosDisponibles;
                //xamDataPresenterProductos.DataSource = dtProductos;
                xamDataPresenterProductos.DataSource = List;

                if (List.Count > 0)
                {
                    xamDataPresenterProductos.Focus();


                }

                //cargo los valores por defecto de la grilla

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }


        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
                btnAceptar.Focus();
        }

        private void btnAdd_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key.Equals(Key.Enter))
                {
                    if ((DataRow)xamDataPresenterProductos.ActiveDataItem == null)
                    {
                        MessageBox.Show("Debe seleccionar un producto para continuar...", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                        xamDataPresenterProductos.Focus();
                    }
                    else if (Convert.ToDecimal(txtCantidad.Text) > 0)
                    {
                        AgregarProducto();
                        txtCantidad.Text = "1";

                        txtBuscar.Focus();
                        txtBuscar.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Solo permite cantidades mayores a 0", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtCantidad.Text = "";
                        txtCantidad.Focus();
                    }
                }
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void xamDataPresenterProductos_CellChanged(object sender, Infragistics.Windows.DataPresenter.Events.CellChangedEventArgs e)
        {

        }

        private void xamDataPresenterProductos_CellActivated(object sender, Infragistics.Windows.DataPresenter.Events.CellActivatedEventArgs e)
        {
            DataRow Item = null;

            if (xamDataPresenterProductos.ActiveDataItem != null)
            {
                Item = (DataRow)xamDataPresenterProductos.ActiveDataItem;
                this.txtValorItem.Text = Convert.ToDecimal(Item["VALOR"].ToString()).ToString();
            }
        }

        private void txtValorItem_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key.Equals(Key.Enter))

                btnAdd.Focus();

        }

        private void txtValorItem_GotFocus(object sender, RoutedEventArgs e)
        {
            txtValorItem.SelectAll();
        }

        private void txtBuscar_ContextMenuClosing(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {

        }

        private void Window_ContextMenuClosing(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CierraBoton != true)
            {
                e.Cancel = true;
            }
        }

        private void btnOxigeno_Click(object sender, RoutedEventArgs e)
        {
            //DataRow Item = null;
            //Item = (DataRow)xamDataPresenterProductos.ActiveDataItem; // Selecciono la fila que esta selecionada en el grid de articulos / Giovanny Tapia / 04/10/2012

            //Int32 Prod = Convert.ToInt32(Item["CODIGO"].ToString());
            if (_Rubro == 17 && codigoArea == 13)
            {
                CalculoOxigeno calculo = new CalculoOxigeno();
                calculo.ShowDialog();
                txtCantidad.Text = calculo.totalCalculo;
            }

        }
    }
}
