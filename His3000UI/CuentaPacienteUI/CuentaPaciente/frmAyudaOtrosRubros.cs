using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using frm_Ayudas = His.Admision.frm_Ayudas;
using Infragistics.Win.UltraWinGrid;

namespace CuentaPaciente
{
    public partial class frmAyudaProductos : Form
    {


        #region variables

        //Declaración de variables

        List<RUBROS> listaRubros = new List<RUBROS>();
        public RUBROS rubro = new RUBROS();
        private PEDIDOS_AREAS areas;
        private List<PRODUCTO> listaProductosDisponibles;
        public List<PRODUCTO> listaProductosSolicitados;
        public string descripcion;
        private Int16 codigoArea;
        private string vistaModo;
        public int codTransaccion;
        DataTable Productos = new DataTable();
        public PEDIDOS_DETALLE PedidosDetalleItem = null;
        public List<PEDIDOS_DETALLE> PedidosDetalle = new List<PEDIDOS_DETALLE>();
        public string NumVale;
        bool StockNegativo = false;
        int Resultado = 0;
        bool listartodos = false;
        RUBROS _Rubro = new RUBROS();
        public Int64 _codigoatencion;
        Int32 _CodigoEmpresa = 0;
        Int32 _CodigoConvenio = 0;

        public DtoDetalleHonorariosMedicos DetalleHonorariosItem = null;
        public List<DtoDetalleHonorariosMedicos> DetalleHonorarios = new List<DtoDetalleHonorariosMedicos>();

        #endregion


        public frmAyudaProductos()
        {
            InitializeComponent();
            cargarProductos();
        }
        public int numcaja = 0;
        public bool mushuñan = false;
        public frmAyudaProductos(PEDIDOS_AREAS areas, RUBROS Rubro, Boolean todos/*, Int32 CodigoEmpresa*/, Int32 CodigoConvenio)
        {
            listartodos = todos;
            InitializeComponent();
            this.areas = areas;
            txt_Cantidad.Text = "1";
            txtVale.Text = "SIN FACTURA";
            _Rubro = Rubro;
            _CodigoConvenio = CodigoConvenio
            ;

            cargarProductos();
        }

        public frmAyudaProductos(PEDIDOS_AREAS areas, RUBROS Rubro/*, Int32 CodigoEmpresa, Int32 CodigoConvenio*/)
        {
            InitializeComponent();
            this.areas = areas;
            txt_Cantidad.Text = "1";
            txtVale.Text = "SIN FACTURA";
            _Rubro = Rubro;
            /*_CodigoEmpresa = CodigoEmpresa;
            _CodigoConvenio = CodigoConvenio*/
            ;

            cargarProductos();
        }
        #region Métodos

        private void cargarProductos()
        {
            //valido el perfil del usuario mushuñan
            List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(His.Entidades.Clases.Sesion.codUsuario);

            foreach (var item in perfilUsuario)
            {
                List<ACCESO_OPCIONES> accop = NegUtilitarios.ListaAccesoOpcionesPorPerfil(item.ID_PERFIL, 7);
                foreach (var items in accop)
                {
                    if (items.ID_ACCESO == 71110)// se cambia del perfil  29 a opcion 71110// Mario Valencia 14/11/2023 // cambio en seguridades.
                    {
                        mushuñan = true;
                    }
                }
                //if (item.ID_PERFIL == 29)
                //{
                //    if (item.DESCRIPCION.Contains("SUCURSALES")) //se debe tomar en cuenta que si es 29 en otra empresa no actuara de la forma como en la pasteur.
                //        mushuñan = true;
                //}
            }


            ////no eliminar ya qeu toma
            //if (His.Parametros.FacturaPAR.BodegaPorDefecto == 1) // Se coemta por cambio a trabajar por IP-Bodega // Mario 15/02/2023
            //{
            //    if (NegAccesoOpciones.ParametroBodega())
            //    {
            //        His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
            //        if (mushuñan)
            //        {
            //            Int16 AreaUsuario = 1;
            //            DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario));
            //            bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
            //            if (parse)
            //            {
            //                switch (AreaUsuario)
            //                {
            //                    case 2:
            //                        His.Parametros.FacturaPAR.BodegaPorDefecto = 61;
            //                        break;
            //                    case 3:
            //                        His.Parametros.FacturaPAR.BodegaPorDefecto = 62;
            //                        break;
            //                    default:
            //                        break;
            //                }
            //            }
            //        }                        
            //    }
            //}
            //else
            //{
            //    if (NegAccesoOpciones.ParametroBodega())
            //    {
            //        His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
            //        if (mushuñan)
            //            His.Parametros.FacturaPAR.BodegaPorDefecto = 61;
            //    }
            //}

            listaProductosSolicitados = new List<PRODUCTO>();
            descripcion = null;
            try
            {
                //cargo la lista de cantidad a paginar
                List<int> listaCantidad = new List<int>() { 10, 20, 50, 100, 500, 1000 };
                cmb_Mostrar.DataSource = listaCantidad;
                cmb_Mostrar.SelectedIndex = 1;
                List<DataRow> List = new List<DataRow>();

                if (areas.PEA_CODIGO != 1)
                {
                    PARAMETROS_DETALLE parametros = new PARAMETROS_DETALLE();
                    parametros = NegParametros.RecuperaPorCodigo(45);

                    if (codigoArea > 0)
                    {
                        //listaProductosDisponibles = NegProducto.RecuperarProductosLista(0, 20, null, areas.PEA_CODIGO);

                        if (listartodos)
                        {

                            Productos = NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/ , _Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega , _CodigoEmpresa, _CodigoConvenio); // Se coemta por cambio a trabajar por IP-Bodega // Mario 15/02/2023
                        }
                        else
                        {
                            Productos = NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/ , _Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                        }
                    }
                    else
                    {
                        //listaProductosDisponibles = NegProducto.RecuperarProductosLista(0, 20, null);
                        if ( listartodos && His.Entidades.Clases.Sesion.codDepartamento == 7 && parametros.PAD_ACTIVO == true)
                        {
                            Productos = NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                        }
                        else if (listartodos && His.Entidades.Clases.Sesion.codDepartamento != 7 )
                        {
                            Productos = NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                        }
                        
                        else if (listartodos && His.Entidades.Clases.Sesion.codDepartamento == 7 ) //Cambios Edgar Ramos
                        {
                            //aqui se filtraran si es cajero se mostraran unicamente todos los servicios
                            Productos = NegProducto.RecuperarProductosListaServicios(1, "", _Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                        }
                        else
                        {
                            Productos = NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                        }
                    }
                }
                else
                {

                    if (codigoArea > 0)
                    {
                        //listaProductosDisponibles = NegProducto.RecuperarProductosLista(0, 20, null, areas.PEA_CODIGO);
                        Productos = NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/ , _Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                    }
                    else
                    {
                        //listaProductosDisponibles = NegProducto.RecuperarProductosLista(0, 20, null);
                        Productos = NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                    }

                }
                //foreach (DataRow dr in Productos.Rows)
                //{
                //    List.Add(dr);
                //}

                //gridProductos.DataSource = listaProductosDisponibles;


                gridProductos.DataSource = Productos;
                gridProductos.Columns["DIVISION"].Width = 200;
                gridProductos.Columns["CODIGO"].Width = 65;
                gridProductos.Columns["PRODUCTO"].Width = 250;
                gridProductos.Columns["STOCK"].Width = 90;
                gridProductos.Columns["IVA"].Width = 50;
                gridProductos.Columns["VALOR"].Width = 80;
                gridProductos.Columns["Cantidad"].Width = 0;
                DataGridViewColumn Column = gridProductos.Columns[7];
                Column.Visible = false;



                //cargo los valores por defecto de la grilla
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message);
            }

        }

        private void addProducto()
        {
            NegFactura fact = new NegFactura();
            if (gridProductos.CurrentRow != null)
            {
                Decimal cantidad = Convert.ToDecimal(txt_Cantidad.Text);
                if (cantidad > 0)
                {
                    //PRODUCTO producto = (PRODUCTO)gridProductos.ActiveRow.ListObject;
                    //producto = (PRODUCTO)gridProductos.ActiveRow.ListObject;
                    //producto.PRO_CANTIDAD = cantidad;
                    //listaProductosDisponibles.Remove(producto);
                    //gridProductos.DataSource = listaProductosDisponibles.ToList();
                    //listaProductosSolicitados.Add(producto);
                    //gridProductosSeleccionados.DataSource = listaProductosSolicitados.ToList();

                    Decimal Valor = 0;
                    DataGridViewRow Item = null;
                    PRODUCTO Prod = new PRODUCTO();

                    Item = gridProductos.CurrentRow; // Selecciono la fila que esta selecionada en el grid de articulos / Giovanny Tapia / 04/10/2012
                    Prod = NegProducto.RecuperarProductoID(Convert.ToInt32(Item.Cells["CODIGO"].Value));// Creo un objeto con el item seleccionado / Giovanny Tapia / 04/10/2012
                    Valor = Convert.ToDecimal(this.txt_Cantidad.Text) - Math.Round((Convert.ToDecimal(this.txt_Cantidad.Text)), 0);
                    if (Math.Abs(Valor) > 0 && Item.Cells["Cantidad"].Value.ToString() == "False")
                    {
                        System.Windows.MessageBox.Show("Este producto no permite cantidades decimales.");
                        return;
                    }

                    var Verifica = (from qs in PedidosDetalle.AsEnumerable() // Verifico si el item seleccionado ya fue añadido anteriormente / Giovanny Tapia / 04/10/2012
                                    where (qs.PRODUCTOReference.EntityKey == Prod.EntityKey)
                                    select qs).ToList();

                    if (Verifica.Count > 0) // Verifico si el LINQ devolvio datos / Giovanny Tapia / 04/10/2012
                    {
                        System.Windows.MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000");
                        return;
                    }

                    DataTable cantidadReal = new DataTable();
                    cantidadReal = NegHabitaciones.VerificaCantidadStock(Convert.ToInt64(Item.Cells["CODIGO"].Value), His.Entidades.Clases.Sesion.bodega);
                    if (StockNegativo == false)
                    {//Convert.ToDecimal(Item.Cells["STOCK"].Value)
                        if (Convert.ToDecimal(cantidadReal.Rows[0][0].ToString()) - (Convert.ToDecimal(this.txt_Cantidad.Text)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                        {
                            PedidosDetalleItem = new PEDIDOS_DETALLE();

                            PedidosDetalleItem.PDD_CODIGO = 1;
                            PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                            PedidosDetalleItem.PDD_CANTIDAD = Convert.ToDecimal(this.txt_Cantidad.Text);
                            PedidosDetalleItem.PRO_DESCRIPCION = Item.Cells["PRODUCTO"].Value.ToString();

                            //Cambios Edgar 20210507 Valida que paciente tenga seguro y valida el rubro de dieta para valor 0
                            int rubroDieta = NegPedidos.ValidarRubro(Item.Cells["CODIGO"].Value.ToString());
                            if (NegPedidos.ParametroDieta() && rubroDieta == 4)
                            {
                                int te_codigo = NegPedidos.ValidarAseguradoraPaciente(_codigoatencion);
                                if (te_codigo == 1)
                                {
                                    PedidosDetalleItem.PDD_VALOR = 0;
                                    PedidosDetalleItem.PDD_TOTAL = 0;
                                    PedidosDetalleItem.PDD_IVA = 0;
                                }

                                else
                                {
                                    PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(this.txtValorUnitario.Text);
                                    if (Convert.ToDecimal(Item.Cells["IVA"].Value) > 0)
                                    {
                                        PedidosDetalleItem.PDD_IVA = Math.Round(((((/*Convert.ToDecimal(Item.Cells["VALOR"].Value)*/Convert.ToDecimal(this.txtValorUnitario.Text) * Convert.ToDecimal(this.txt_Cantidad.Text))) * Convert.ToDecimal(Item.Cells["IVA"].Value)) / 100), 4);
                                    }
                                    else
                                    {
                                        PedidosDetalleItem.PDD_IVA = 0;
                                    }

                                    PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(this.txtValorUnitario.Text) * Convert.ToDecimal(this.txt_Cantidad.Text)), 2) + PedidosDetalleItem.PDD_IVA;
                                }

                            }
                            else
                            {
                                PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(this.txtValorUnitario.Text);
                                if (Convert.ToDecimal(Item.Cells["IVA"].Value) > 0)
                                {
                                    PedidosDetalleItem.PDD_IVA = Math.Round(((((/*Convert.ToDecimal(Item.Cells["VALOR"].Value)*/Convert.ToDecimal(this.txtValorUnitario.Text) * Convert.ToDecimal(this.txt_Cantidad.Text))) * Convert.ToDecimal(Item.Cells["IVA"].Value)) / 100), 4);
                                }
                                else
                                {
                                    PedidosDetalleItem.PDD_IVA = 0;
                                }

                                PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(this.txtValorUnitario.Text) * Convert.ToDecimal(this.txt_Cantidad.Text)), 2) + PedidosDetalleItem.PDD_IVA;
                            }


                            //PedidosDetalleItem.PDD_VALOR = /*Convert.ToDecimal(Item.Cells["VALOR"].Value)*/ Convert.ToDecimal(this.txtValorUnitario.Text);
                            //PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(this.txtValorUnitario.Text) * Convert.ToDecimal(this.txt_Cantidad.Text)), 2) + PedidosDetalleItem.PDD_IVA;
                            PedidosDetalleItem.PDD_ESTADO = true;
                            PedidosDetalleItem.PDD_COSTO = 0;
                            PedidosDetalleItem.PDD_FACTURA = null;
                            PedidosDetalleItem.PDD_ESTADO_FACTURA = 0;
                            PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                            PedidosDetalleItem.PDD_RESULTADO = null;
                            PedidosDetalleItem.PRO_CODIGO_BARRAS = Item.Cells["CODIGO"].Value.ToString();
                            PedidosDetalleItem.PRO_BODEGA_SIC = His.Entidades.Clases.Sesion.bodega;

                            PedidosDetalle.Insert(0, PedidosDetalleItem);

                            //gridProductosSeleccionados.DataSource = PedidosDetalle;

                            gridProductosSeleccionados.DataSource = PedidosDetalle.Select(
                            p => new
                            {
                                CODIGO = p.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value,
                                DESCRIPCION = p.PRO_DESCRIPCION,
                                CANTIDAD = p.PDD_CANTIDAD,
                                VALOR = p.PDD_VALOR,
                                IVA = p.PDD_IVA,
                                TOTAL = p.PDD_TOTAL
                            }
                            ).Distinct().ToList();

                            Redimensionar();
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Por favor seleccione la cantidad del producto que necesita");
                        }
                    }
                    else
                    {

                        PedidosDetalleItem = new PEDIDOS_DETALLE();

                        PedidosDetalleItem.PDD_CODIGO = 1;
                        PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                        PedidosDetalleItem.PDD_CANTIDAD = Convert.ToDecimal(this.txt_Cantidad.Text);
                        PedidosDetalleItem.PRO_DESCRIPCION = Item.Cells["PRODUCTO"].Value.ToString();
                        PedidosDetalleItem.PDD_VALOR = /*Convert.ToDecimal(Item.Cells["VALOR"].Value*/ Convert.ToDecimal(this.txtValorUnitario.Text);

                        if (Convert.ToDecimal(Item.Cells["IVA"].Value) > 0)
                        {
                            PedidosDetalleItem.PDD_IVA = Math.Round(((((/*Convert.ToDecimal(Item.Cells["VALOR"].Value)*/Convert.ToDecimal(this.txtValorUnitario.Text) * Convert.ToDecimal(this.txt_Cantidad.Text))) * Convert.ToDecimal(Item.Cells["IVA"].Value)) / 100), 2);
                        }
                        else
                        {
                            PedidosDetalleItem.PDD_IVA = 0;
                        }

                        PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(this.txtValorUnitario.Text) * Convert.ToDecimal(this.txt_Cantidad.Text)), 2) + PedidosDetalleItem.PDD_IVA;
                        PedidosDetalleItem.PDD_ESTADO = true;
                        PedidosDetalleItem.PDD_COSTO = 0;
                        PedidosDetalleItem.PDD_FACTURA = null;
                        PedidosDetalleItem.PDD_ESTADO_FACTURA = 0;
                        PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                        PedidosDetalleItem.PDD_RESULTADO = null;
                        PedidosDetalleItem.PRO_CODIGO_BARRAS = Item.Cells["CODIGO"].Value.ToString();

                        PedidosDetalle.Insert(0, PedidosDetalleItem);

                        //gridProductosSeleccionados.DataSource = PedidosDetalle;

                        gridProductosSeleccionados.DataSource = PedidosDetalle.Select(
                        p => new
                        {
                            CODIGO = p.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value,
                            DESCRIPCION = p.PRO_DESCRIPCION,
                            CANTIDAD = p.PDD_CANTIDAD,
                            VALOR = p.PDD_VALOR,
                            IVA = p.PDD_IVA,
                            TOTAL = p.PDD_TOTAL
                        }
                        ).Distinct().ToList();
                        Redimensionar();
                    }
                }

                else
                {
                    //MessageBox.Show("Por favor seleccione un medicamento de la lista de medicamentos disponibles", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        #endregion

        public void Redimensionar()
        {
            UltraGridBand bandUno = gridProductosSeleccionados.DisplayLayout.Bands[0];

            gridProductosSeleccionados.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            gridProductosSeleccionados.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            gridProductosSeleccionados.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            gridProductosSeleccionados.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            gridProductosSeleccionados.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            gridProductosSeleccionados.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            gridProductosSeleccionados.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            //gridPagos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            //gridPagos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            //gridPagos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            //gridPagos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            //gridPagos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            gridProductosSeleccionados.DisplayLayout.UseFixedHeaders = true;


            //Dimension las celdas
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns[0].Width = 70;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns[1].Width = 400;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns[2].Width = 60;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns[3].Width = 60;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns[4].Width = 60;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns[5].Width = 60;
        }
        private void dataGridViewFormasPago_DoubleClick(object sender, System.EventArgs e)
        {
            //int posicion = dataGridViewRubros1.CurrentRow.Index;
            //rubro = listaRubros.ElementAt(posicion);
            //if (rubro != null)
            //{
            //    this.Close();
            //}

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int cantidad = Convert.ToInt32(cmb_Mostrar.SelectedValue);
                string textoBusqueda = txt_Nombre.Text.Trim() == "" ? null : txt_Nombre.Text.Trim();
                //listaProductosDisponibles = NegProducto.RecuperarProductosLista(1, cantidad, textoBusqueda, codigoArea);

                if (areas.PEA_CODIGO != 1)
                {

                    if (listartodos && His.Entidades.Clases.Sesion.codDepartamento != 7)
                    {
                        Productos = NegProducto.RecuperarProductosListaSPall(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                    }
                    else if (listartodos && His.Entidades.Clases.Sesion.codDepartamento == 7) //Cambios Edgar 20210203
                    {
                        //aqui se filtraran si es cajero se mostraran unicamente todos los servicios
                        Productos = NegProducto.RecuperarProductosListaServicios(1, txt_Nombre.Text.ToString(), _Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                    }
                    else
                    {
                        Productos = NegProducto.RecuperarProductosListaSP(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                    }


                }
                else
                {

                    Productos = NegProducto.RecuperarProductosListaSP_Farmacia(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);

                }

                //gridProductos.DataSource = listaProductosDisponibles;


                gridProductos.DataSource = Productos;

                if (Productos.Rows.Count > 0)
                {
                    gridProductos.Focus();
                }
                else
                {
                    txt_Nombre.Focus();
                }
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message);
            }
        }

        private void dataGridViewRubros_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Enter)
                //{
                //    int index = dataGridViewRubros.CurrentRow.Index;
                //    if (Convert.ToInt32(dataGridViewRubros.Rows[dataGridViewRubros.CurrentRow.Index].Cells[0].Value) != null)
                //    {
                //        RUBROS rubro = new RUBROS();
                //        var query = (from a in listaRubros
                //                     where a.RUB_CODIGO == Convert.ToInt32(dataGridViewRubros.Rows[dataGridViewRubros.CurrentRow.Index].Cells[0].Value)
                //                     select a).FirstOrDefault();
                //        rubro = query;
                //        dataGridViewRubros.Rows[Convert.ToInt32(dataGridViewRubros.Rows[dataGridViewRubros.CurrentRow.Index].Cells[0].Value)].Cells[0].Value = rubro.RUB_CODIGO;
                //        dataGridViewRubros.Rows[Convert.ToInt32(dataGridViewRubros.Rows[dataGridViewRubros.CurrentRow.Index].Cells[0].Value)].Cells[1].Value = rubro.RUB_NOMBRE;
                //        dataGridViewRubros.Rows[Convert.ToInt32(dataGridViewRubros.Rows[dataGridViewRubros.CurrentRow.Index].Cells[0].Value)].Cells[2].Value = 00.00;
                //        dataGridViewRubros.Rows[Convert.ToInt32(dataGridViewRubros.Rows[dataGridViewRubros.CurrentRow.Index].Cells[0].Value)].Cells[3].Value = 00.00;
                //        dataGridViewRubros.Rows[Convert.ToInt32(dataGridViewRubros.Rows[dataGridViewRubros.CurrentRow.Index].Cells[0].Value)].Cells[4].Value = 00.00;

                //    }
                //}
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error");
            }
        }


        private void dgw_Rubros_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgw_Rubros_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //dgw_Rubros.CurrentCell = dgw_Rubros[2, dgw_Rubros.CurrentRow.Index];

                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgw_Rubros_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    //dgw_Rubros.CurrentCell = dgw_Rubros[2, dgw_Rubros.CurrentRow.Index];

                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgw_Rubros_Enter(object sender, EventArgs e)
        {

        }

        private void dgw_Rubros_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                ////dgw_Rubros.CurrentCell = dgw_Rubros[0, dgw_Rubros.CurrentRow.Index];
                //int index = dgw_Rubros.CurrentRow.Index-1;

                //if (Convert.ToInt32(dgw_Rubros.Rows[index].Cells[0].Value) != null)
                //{
                //    RUBROS rubro = new RUBROS();
                //    var query = (from a in listaRubros
                //                 where a.RUB_CODIGO == Convert.ToInt32(dgw_Rubros.Rows[index].Cells[0].Value)
                //                 select a).FirstOrDefault();
                //    rubro = query;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[index].Cells[0].Value)].Cells[0].Value = rubro.RUB_CODIGO;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[index].Cells[0].Value)].Cells[1].Value = rubro.RUB_NOMBRE;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[index].Cells[0].Value)].Cells[2].Value = 00.00;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[index].Cells[0].Value)].Cells[3].Value = 00.00;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[index].Cells[0].Value)].Cells[4].Value = 00.00;
                //    }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgw_Rubros_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //int i = dgw_Rubros.CurrentRow.Index;
                //if (dgw_Rubros.Rows[e.ColumnIndex].Cells[0].Value != null)
                //{
                //    RUBROS rubro = new RUBROS();
                //    var query = (from a in listaRubros
                //                 where a.RUB_CODIGO == Convert.ToInt32(dgw_Rubros.Rows[e.ColumnIndex].Cells[0].Value)
                //                 select a).FirstOrDefault();
                //    rubro = query;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[e.ColumnIndex].Cells[0].Value)].Cells[0].Value = rubro.RUB_CODIGO;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[e.ColumnIndex].Cells[0].Value)].Cells[1].Value = rubro.RUB_NOMBRE;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[e.ColumnIndex].Cells[0].Value)].Cells[2].Value = 00.00;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[e.ColumnIndex].Cells[0].Value)].Cells[3].Value = 00.00;
                //    dgw_Rubros.Rows[Convert.ToInt32(dgw_Rubros.Rows[e.ColumnIndex].Cells[0].Value)].Cells[4].Value = 00.00;
                //}
                //else
                //{
                //    MessageBox.Show("Error");

                //}
            }
        }

        private void btn_Anadir_Click(object sender, EventArgs e)
        {
            NegFactura fact = new NegFactura();
            try
            {
                if (NegCuentasPacientes.LlamarParametroInventariable())
                {
                    DataGridViewRow Item = gridProductos.CurrentRow;
                    string claspro = fact.Recuperarclaspro(Item.Cells["CODIGO"].Value.ToString());
                    if (claspro != "B" && His.Entidades.Clases.Sesion.codDepartamento != 7)
                    {
                        addProducto();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("No tiene permiso. Comuniquese con el administrador", "HIS3000", MessageBoxButton.OK);
                    }
                }
                else
                {
                    addProducto();
                }
                txt_Cantidad.Text = "1";

                /*DEVUELVO EL FOCO A LA CAJA DE TEXTO DE BUSQUEDA / GIOBANNY TAPIA / 06/03/2013*/

                txt_Nombre.SelectAll();
                txt_Nombre.Focus();

            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txt_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumberDecimal(e, false);
            //if (Char.IsNumber(e.KeyChar))
            //{
            //    //e.Handled = false;
            //}
            //if (Char.IsLetter(e.KeyChar))
            //{
            //    //txt_Cantidad.Text = string.Empty;
            //    //e.Handled = true;
            //}
            if (e.KeyChar == 13)
            {
                txtValorUnitario.Focus();
            }
        }



        private void gridProductosSeleccionados_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //if (gridProductosSeleccionados.ActiveRow != null)
                //{
                //    PEDIDOS_DETALLE producto = (PEDIDOS_DETALLE)gridProductosSeleccionados.ActiveRow.ListObject;
                //    PedidosDetalle.Remove(producto);
                //    gridProductosSeleccionados.DataSource = PedidosDetalle.ToList();
                //}
                //else
                //{
                //    MessageBox.Show("Por favor seleccione un medicamento de la lista de medicamentos solicitados");
                //}

                Int32 item = 0;
                item = gridProductosSeleccionados.ActiveRow.Index;

                PedidosDetalle.RemoveAt(item);

                gridProductosSeleccionados.DataSource = PedidosDetalle.Select(
                    p => new
                    {
                        CODIGO = p.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value,
                        DESCRIPCION = p.PRO_DESCRIPCION,
                        CANTIDAD = p.PDD_CANTIDAD,
                        VALOR = p.PDD_VALOR,
                        IVA = p.PDD_IVA,
                        TOTAL = p.PDD_TOTAL
                    }
                ).Distinct().ToList();
            }

            //if (e.KeyCode == Keys.F11)
            //{
            //    gbxDetalleHonorarios.Visible = true;
            //    DetalleHonorariosItem = new DtoDetalleHonorariosMedicos();

            //    DetalleHonorariosItem.PED_CODIGO = 0;
            //    DetalleHonorariosItem.ID_LINEA = this.gridProductosSeleccionados.ActiveRow.Index;
            //    DetalleHonorariosItem.CODPRO = this.gridProductosSeleccionados.ActiveRow.Cells[0].Value.ToString();
            //    DetalleHonorariosItem.MED_CODIGO = 0;
            //    DetalleHonorariosItem.FECHA = DateTime.Now;
            //    DetalleHonorariosItem.CODIGO2 = "";
            //    DetalleHonorariosItem.VALOR = Convert.ToDecimal(this.gridProductosSeleccionados.ActiveRow.Cells[5].Value);

            //    DetalleHonorarios.Add(DetalleHonorariosItem);

            //    //dgvDetalleMedicos.DataSource = DetalleHonorarios.Select(
            //    //p => new
            //    //{
            //    //    CODIGO = this.gridProductosSeleccionados.ActiveRow.Index                 
            //    //}
            //    //).Distinct().ToList();

            //    dgvDetalleMedicos.DataSource = null;
            //    dgvDetalleMedicos.DataSource = DetalleHonorarios;

            //    dgvDetalleMedicos.Columns[0].ReadOnly = true;
            //    dgvDetalleMedicos.Columns[1].ReadOnly = true;
            //    dgvDetalleMedicos.Columns[2].ReadOnly = true;
            //    dgvDetalleMedicos.Columns[4].ReadOnly = true;
            //    dgvDetalleMedicos.Columns[6].ReadOnly = true;
            //    dgvDetalleMedicos.Focus();


            //}
        }

        private void gridProductos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            addProducto();
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            descripcion = txt_Observaciones.Text;
            DetalleHonorarios = new List<DtoDetalleHonorariosMedicos>();
            foreach (var item in gridProductosSeleccionados.Rows)
            {
                if (item.Cells["DESCRIPCION"].Value.ToString().Contains("HONORARIOS"))//Cambios Edgar 20210208 para pruebas 
                {
                    if (dgvDetalleMedicos.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(lblhonorario.Text) != Convert.ToDecimal(item.Cells[5].Value.ToString()))
                        {
                            System.Windows.Forms.MessageBox.Show("Valor de Honorarios no ha sido cubierto.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        Int64 numdoc = 0;
                        for (int i = 0; i < dgvDetalleMedicos.Rows.Count - 1; i++)
                        {
                            if (dgvDetalleMedicos.Rows[i].Cells["valor"].Value.ToString() == "0")
                            {
                                System.Windows.Forms.MessageBox.Show("No se le ha asignado valor al honorario del\r\nDR(A). " + dgvDetalleMedicos.Rows[i].Cells["medico"].Value.ToString(), "HIS3000",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                DetalleHonorarios = null;
                                return;
                            }
                            
                            else
                            {
                                if (dgvDetalleMedicos.Rows[i].Cells["numdoc"].Value == null)
                                {
                                    if (numdoc == 0)
                                        numdoc = NegProducto.NumeroDocumento();
                                    else
                                        numdoc += 1;
                                    dgvDetalleMedicos.Rows[i].Cells["numdoc"].Value = numdoc;
                                }

                                DetalleHonorariosItem = new DtoDetalleHonorariosMedicos();

                                DetalleHonorariosItem.CODIGO2 = dgvDetalleMedicos.Rows[i].Cells["codpro"].Value.ToString();
                                DetalleHonorariosItem.CODPRO = dgvDetalleMedicos.Rows[i].Cells["codpro"].Value.ToString();
                                DetalleHonorariosItem.MED_CODIGO = Convert.ToInt32(dgvDetalleMedicos.Rows[i].Cells["codmed"].Value.ToString());
                                DetalleHonorariosItem.MED_NOMBRE = dgvDetalleMedicos.Rows[i].Cells["medico"].Value.ToString();
                                DetalleHonorariosItem.MED_ESPECIALIDAD = dgvDetalleMedicos.Rows[i].Cells["medesp"].Value.ToString();
                                DetalleHonorariosItem.FECHA = DateTime.Now;
                                DetalleHonorariosItem.numdoc = dgvDetalleMedicos.Rows[i].Cells["numdoc"].Value.ToString();
                                DetalleHonorariosItem.VALOR = Convert.ToDecimal(dgvDetalleMedicos.Rows[i].Cells["valor"].Value.ToString());

                                DetalleHonorarios.Add(DetalleHonorariosItem);
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Por favor asigne uno o varios medicos \r\npara cubrir el monto de honorarios.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            if (areas.PEA_CODIGO == 16)
            {
                if (DetalleHonorarios != null)
                {
                    if (DetalleHonorarios.Count == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Por favor asigne un medicos a los honorarios ingresados,\r\npresione doble clic sobre HONORARIOS MEDICOS.");
                        return;
                    }
                }
            }
            if ((System.Windows.Forms.MessageBox.Show("Esta seguro que desea guardar el pedido.?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.No))
            {
                return;
            }
            NumVale = txtVale.Text;
            this.Dispose();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            if ((System.Windows.Forms.MessageBox.Show("Esta seguro que desea cancelar el pedido.?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.No))
            {
                return;
            }
            listaProductosSolicitados = null;
            PedidosDetalle = null;
            this.Dispose();
        }

        private void txtVale_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gridProductosSeleccionados_InitializeLayout_1(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = gridProductosSeleccionados.DisplayLayout.Bands[0];

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;

            e.Layout.Bands[0].Columns["CODIGO"].Hidden = false;
            e.Layout.Bands[0].Columns["DESCRIPCION"].Hidden = false;
            e.Layout.Bands[0].Columns["CANTIDAD"].Hidden = false;
            e.Layout.Bands[0].Columns["VALOR"].Hidden = false;
            e.Layout.Bands[0].Columns["IVA"].Hidden = false;
            e.Layout.Bands[0].Columns["TOTAL"].Hidden = false;
            //e.Layout.Bands[0].Columns["clasprod"].Hidden = true;

            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 300;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["CANTIDAD"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["VALOR"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["IVA"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["TOTAL"].Width = 100;


            // e.Layout.Bands[0].Columns["HABITACION"].Hidden = true;
        }

        private void frmAyudaProductos_Load(object sender, EventArgs e)
        {

            Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "SALDOS NEGATIVOS PEDIDOS");
            if (Resultado == 0)
            {
                StockNegativo = false;
            }
            else
            {
                StockNegativo = true;
            }
        }

        private void gridProductos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow Fila = null;
            Fila = gridProductos.CurrentRow;
            txtValorUnitario.Text = Fila.Cells["Valor"].Value.ToString();
        }

        private void txt_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_Buscar.Focus();
            }
        }

        private void gridProductos_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    e.Handled = true;
            //    this.txt_Cantidad.SelectAll();
            //    this.txt_Cantidad.Focus();

            //}
        }

        private void txtValorUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_Anadir.Focus();
            }
        }

        private void txtValorUnitario_KeyPress_1(object sender, KeyPressEventArgs e)
        {

        }

        private void gridProductosSeleccionados_BeforeRowsDeleted(object sender, Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs e)
        {
            Int32 item = 0;
            item = gridProductosSeleccionados.ActiveRow.Index;

            PedidosDetalle.RemoveAt(item);

            //gridProductosSeleccionados.DataSource = PedidosDetalle.Select(
            //    p => new
            //    {
            //        CODIGO = p.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value,
            //        DESCRIPCION = p.PRO_DESCRIPCION,
            //        CANTIDAD = p.PDD_CANTIDAD,
            //        VALOR = p.PDD_VALOR,
            //        IVA = p.PDD_IVA,
            //        TOTAL = p.PDD_TOTAL
            //    }
            //).Distinct().ToList();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.gbxDetalleHonorarios.Visible = false;
        }

        private void dgvDetalleMedicos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Convert.ToDecimal(lblhonorario.Text) == Convert.ToDecimal(this.gridProductosSeleccionados.ActiveRow.Cells[5].Value))
                {
                    System.Windows.Forms.MessageBox.Show("Monto de Honorario cubierto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

                    His.Admision.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
                    ayuda.ShowDialog();

                    if (ayuda.campoPadre.Text != string.Empty)
                    {
                        for (int i = 0; i < dgvDetalleMedicos.Rows.Count - 1; i++)
                        {
                            if (dgvDetalleMedicos.Rows[i].Cells["codmed"].Value.ToString() == ayuda.campoPadre.Text.ToString())
                            {
                                System.Windows.Forms.MessageBox.Show("El médico ya ha sido asigando, intente agregar otro.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        double totalvalor = 0;
                        dgvDetalleMedicos.CurrentRow.Cells["codpro"].Value = this.gridProductosSeleccionados.ActiveRow.Cells[0].Value.ToString();
                        dgvDetalleMedicos.CurrentRow.Cells["codmed"].Value = ayuda.campoPadre.Text.ToString();
                        dgvDetalleMedicos.CurrentRow.Cells["medico"].Value = ayuda.nombremedico;
                        dgvDetalleMedicos.CurrentRow.Cells["medesp"].Value = ayuda.especialida;
                        dgvDetalleMedicos.CurrentRow.Cells["valor"].Value = totalvalor;
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                decimal total = 0;
                for (int i = 0; i < dgvDetalleMedicos.Rows.Count - 1; i++)
                {
                    total += Convert.ToDecimal(dgvDetalleMedicos.Rows[i].Cells["valor"].Value);
                }
                lblhonorario.Text = total.ToString();
            }
        }

        private void CargarMedico(int codMedico)
        {
            //medico = NegMedicos.MedicoID(codMedico);
            //if (medico != null)
            //    txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            //else
            //    txtNombreMedico.Text = string.Empty;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Int32 Indice = 0;

            Indice = dgvDetalleMedicos.CurrentRow.Index;
            DetalleHonorarios.RemoveAt(Indice);
            dgvDetalleMedicos.DataSource = null;
            dgvDetalleMedicos.DataSource = DetalleHonorarios;
        }

        private void gridProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.txt_Cantidad.SelectAll();
                this.txt_Cantidad.Focus();
            }
        }

        private void btn_Buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int cantidad = Convert.ToInt32(cmb_Mostrar.SelectedValue);
                    string textoBusqueda = txt_Nombre.Text.Trim() == "" ? null : txt_Nombre.Text.Trim();
                    //listaProductosDisponibles = NegProducto.RecuperarProductosLista(1, cantidad, textoBusqueda, codigoArea);

                    if (areas.PEA_CODIGO != 1)
                    {

                        if (listartodos)
                        {
                            Productos = NegProducto.RecuperarProductosListaSPall(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                        }
                        else
                        {
                            Productos = NegProducto.RecuperarProductosListaSP(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                        }


                    }
                    else
                    {

                        Productos = NegProducto.RecuperarProductosListaSP_Farmacia(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Entidades.Clases.Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);

                    }

                    //gridProductos.DataSource = listaProductosDisponibles;
                    gridProductos.DataSource = Productos;

                    if (Productos.Rows.Count > 0)
                    {
                        gridProductos.Focus();
                    }
                    else
                    {
                        txt_Nombre.Focus();
                    }
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message);
                }
            }
        }
        public List<DtoDetalleHonorariosMedicos> HonorariosMedicos = null;
        private void btnGuardarMedicos_Click(object sender, EventArgs e)
        {
            HonorariosMedicos = new List<DtoDetalleHonorariosMedicos>();
            DtoDetalleHonorariosMedicos honorario = new DtoDetalleHonorariosMedicos();
            if ((System.Windows.Forms.MessageBox.Show("¿Esta seguro de guardar los honorarios medicos?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                this.gbxDetalleHonorarios.Visible = false;
                foreach (DataGridViewRow item in dgvDetalleMedicos.Rows)
                {
                    honorario.PED_CODIGO = Convert.ToInt64(item.Cells[0].Value.ToString());
                    honorario.MED_CODIGO = Convert.ToInt32(item.Cells[1].Value.ToString());
                    honorario.MED_NOMBRE = item.Cells[2].Value.ToString();
                    honorario.MED_ESPECIALIDAD = item.Cells[3].ToString();
                }
            }
            else
                btnEliminar.PerformClick();
            btnSalir.PerformClick();
        }
        public decimal honorario = 0;
        private void gridProductosSeleccionados_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (gridProductosSeleccionados.Selected.Rows.Count > 0 && gridProductosSeleccionados.ActiveRow.Cells["DESCRIPCION"].Value.ToString().Contains("HONORARIOS"))
            {
                gbxDetalleHonorarios.Visible = true;
                honorario = Convert.ToDecimal(this.gridProductosSeleccionados.ActiveRow.Cells[5].Value);
            }
        }

        private void dgvDetalleMedicos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal suma = 0;
            if (dgvDetalleMedicos.Rows.Count > 1)
            {
                if (dgvDetalleMedicos.CurrentCell.ColumnIndex == 4)
                {
                    if (dgvDetalleMedicos.CurrentCell.Value == null)
                    {
                        dgvDetalleMedicos.CurrentCell.Value = 0;
                    }
                    decimal valor = Convert.ToDecimal(dgvDetalleMedicos.CurrentCell.Value);
                    for (int i = 0; i < dgvDetalleMedicos.Rows.Count - 1; i++)
                    {
                        suma += Convert.ToDecimal(dgvDetalleMedicos.Rows[i].Cells["valor"].Value);
                        lblhonorario.Text = suma.ToString();
                    }
                    if (suma > honorario)
                    {
                        lblhonorario.Text = (Convert.ToDecimal(lblhonorario.Text) - valor).ToString();
                        valor = 0;
                        dgvDetalleMedicos.CurrentCell.Value = valor;
                        System.Windows.Forms.MessageBox.Show("El Valor no debe ser mayor a " + honorario, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (dgvDetalleMedicos.CurrentCell.ColumnIndex == 3)
                {
                    for (int i = 0; i < dgvDetalleMedicos.Rows.Count - 1; i++)
                    {
                        suma += Convert.ToDecimal(dgvDetalleMedicos.Rows[i].Cells["valor"].Value);
                        lblhonorario.Text = suma.ToString();

                    }
                }
                //if (dgvDetalleMedicos.CurrentRow.Cells["numdoc"].ColumnIndex == 5)
                //{
                //    validarNumeroFacturaMedico();
                //}
            }
        }

        public void validarNumeroFacturaMedico()
        {
            His.Honorarios.Datos.Atencion obj_atencion = new His.Honorarios.Datos.Atencion();
            if (dgvDetalleMedicos.CurrentRow.Cells["numdoc"].Value != null)
            {
                if(dgvDetalleMedicos.CurrentRow.Cells["numdoc"].Value.ToString().Length == 15)
                {
                    string factura = dgvDetalleMedicos.CurrentRow.Cells["numdoc"].Value.ToString();
                    factura = factura.Replace(" ", "");
                    if (factura.Length == 15)
                    {
                        int codMedico = Convert.ToInt16(dgvDetalleMedicos.CurrentRow.Cells["codmed"].Value.ToString());
                        bool facturaRepetida = false;
                        facturaRepetida = NegHonorariosMedicos.DatosRecuperaFacturasMedicos(codMedico, factura);
                        if (!facturaRepetida)
                        {
                            string serie = factura.Substring(0, 6);
                            Int64 numFact = Convert.ToInt64(factura.Substring(6, 9));
                            DataTable validaFactura = new DataTable();
                            validaFactura = obj_atencion.ValidaFactura(Convert.ToInt64(dgvDetalleMedicos.CurrentRow.Cells["codmed"].Value.ToString()));
                            if (validaFactura.Rows.Count > 0)
                            {
                                if (DateTime.Now.Date <= Convert.ToDateTime(validaFactura.Rows[0][5].ToString()).Date)
                                    if (serie == validaFactura.Rows[0][6].ToString())
                                        if (numFact >= Convert.ToInt64(validaFactura.Rows[0][7].ToString()) && numFact <= Convert.ToInt64(validaFactura.Rows[0][8].ToString()))
                                        {
                                            return;
                                        }
                                        else
                                            System.Windows.Forms.MessageBox.Show("Secuencial de factura ingresada no coincide con lo registrado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    else
                                        System.Windows.Forms.MessageBox.Show("Serie de factura ingresada no coincide con lo registrado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    System.Windows.Forms.MessageBox.Show("Libretin de facturas registradas se encuentra caducado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dgvDetalleMedicos.CurrentRow.Cells["numdoc"].Value = null;
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Médico no cuenta con facturas registradas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dgvDetalleMedicos.CurrentRow.Cells["numdoc"].Value = null;
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Factura del médico ya fue utilizada para liquidar otro Honorario", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvDetalleMedicos.CurrentRow.Cells["numdoc"].Value = null;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Formato de factura incorrecto, Intente nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgvDetalleMedicos.CurrentRow.Cells["numdoc"].Value = null;
                    }
                }
            }
        }
        private void dgvDetalleMedicos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(dgvDetalleMedicos_KeyPress);
            if (dgvDetalleMedicos.CurrentCell.ColumnIndex == 4) //Columnas deseadas
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dgvDetalleMedicos_KeyPress);
                }
            }
            else if(dgvDetalleMedicos.CurrentCell.ColumnIndex == 5)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dgvDetalleMedicos_KeyPress);
                }
            }
        }

        private void dgvDetalleMedicos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(dgvDetalleMedicos.CurrentCell.ColumnIndex == 4)
                OnlyNumber(e, false);

            else if(dgvDetalleMedicos.CurrentCell.ColumnIndex == 5)
                OnlyNumberFactura(e, false);

        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789." + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private static void OnlyNumberFactura(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvDetalleMedicos.Rows.Count > 1 && Convert.ToDecimal(lblhonorario.Text) == honorario)
            {
                if (checkBox1.Checked == true)
                {
                    Int64 numdoc = 0;
                    for (int i = 0; i < dgvDetalleMedicos.Rows.Count - 1; i++)
                    {
                        if (numdoc == 0)
                            numdoc = NegProducto.NumeroDocumento();
                        else
                            numdoc += 1;
                        dgvDetalleMedicos.Rows[i].Cells["numdoc"].Value = numdoc;
                    }
                }
                else
                {
                    for (int i = 0; i < dgvDetalleMedicos.Rows.Count - 1; i++)
                    {
                        dgvDetalleMedicos.Rows[i].Cells["numdoc"].Value = null;
                    }
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Antes de generar Nº de factura debe completar el valor del honorario.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                checkBox1.Checked = false;
            }
        }
    }
}

