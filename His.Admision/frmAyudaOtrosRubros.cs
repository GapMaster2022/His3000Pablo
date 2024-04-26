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
using His.Entidades.Clases;

namespace His.Admision
{
    public partial class frmAyudaProductos : Form
    {


        #region variables

        //Declaración de variables
        int auxFirst = 0;
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
        bool mushuñan = false;
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
        public frmAyudaProductos(PEDIDOS_AREAS areas, RUBROS Rubro, Boolean todos/*, Int32 CodigoEmpresa, Int32 CodigoConvenio*/)
        {
            //valido el perfil del usuario mushuñan
            List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(Sesion.codUsuario);

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
            listartodos = todos;
            InitializeComponent();
            this.areas = areas;
            txt_Cantidad.Text = "1";
            txtVale.Text = "SIN FACTURA";
            _Rubro = Rubro;
            /*_CodigoEmpresa = CodigoEmpresa;
            _CodigoConvenio = CodigoConvenio*/
            ;
            if (Sesion.codDepartamento == 32 || Sesion.codDepartamento == 13 || Sesion.codDepartamento == 1)
            {
                txtValorUnitario.Visible = true;
            }
            cargarProductos();

        }

        public frmAyudaProductos(PEDIDOS_AREAS areas, RUBROS Rubro/*, Int32 CodigoEmpresa, Int32 CodigoConvenio*/)
        {
            InitializeComponent();
            this.areas = areas;
            txt_Cantidad.Text = "1";
            txtVale.Text = "SIN FACTURA";
            _Rubro = Rubro;

            cargarProductos();
        }
        #region Métodos

        private void cargarProductos()
        {
            //if (His.Parametros.FacturaPAR.BodegaPorDefecto == 1)
            //{
            //    if (NegAccesoOpciones.ParametroBodega())
            //    {
            //        His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
            //        if (mushuñan)
            //            Parametros.FacturaPAR.BodegaPorDefecto = 61;
            //    }
            //}
            //else
            //{
            //    if (NegAccesoOpciones.ParametroBodega())
            //    {
            //        His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
            //        if (mushuñan)
            //            Parametros.FacturaPAR.BodegaPorDefecto = 61;
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

                if (listartodos)
                {
                    Productos = NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, 0, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                }
                else if (_Rubro == null)
                {
                    Productos = NegProducto.RecuperarProductosListaSPPedidosAreas(1, ""/*txtBuscar.Value.ToString()*/, 0, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio, areas.PEA_CODIGO);
                }
                else
                {
                    Productos = NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro.RUB_CODIGO, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                }

                gridProductos.DataSource = Productos;
                gridProductos.Columns["DIVISION"].Width = 200;
                gridProductos.Columns["CODIGO"].Width = 65;
                gridProductos.Columns["PRODUCTO"].Width = 250;
                gridProductos.Columns["STOCK"].Width = 90;
                gridProductos.Columns["IVA"].Width = 50;
                gridProductos.Columns["VALOR"].Width = 80;
                gridProductos.Columns["Cantidad"].Width = 0;

                //cargo los valores por defecto de la grilla
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message);

            }

        }


        private void addProducto()
        {
            if (gridProductos.CurrentRow != null)
            {
                Decimal cantidad = Convert.ToDecimal(txt_Cantidad.Text);
                if (cantidad > 0)
                {

                    Decimal Valor = 0;
                    DataGridViewRow Item = null;
                    PRODUCTO Prod = new PRODUCTO();

                    Item = gridProductos.CurrentRow; // Selecciono la fila que esta selecionada en el grid de articulos / Giovanny Tapia / 04/10/2012
                    Prod = NegProducto.RecuperarProductoID(Convert.ToInt32(Item.Cells["CODIGO"].Value));// Creo un objeto con el item seleccionado / Giovanny Tapia / 04/10/2012

                    Valor = Convert.ToDecimal(this.txt_Cantidad.Text) - Math.Round((Convert.ToDecimal(this.txt_Cantidad.Text)), 0);

                    if (Math.Abs(Valor) > 0 && Item.Cells["Cantidad"].Value.ToString() == "False")
                    {

                        System.Windows.Forms.MessageBox.Show("Este producto no permite cantidades decimales.");
                        return;
                    }

                    var Verifica = (from qs in PedidosDetalle.AsEnumerable() // Verifico si el item seleccionado ya fue añadido anteriormente / Giovanny Tapia / 04/10/2012
                                    where (qs.PRODUCTOReference.EntityKey == Prod.EntityKey)
                                    select qs).ToList();

                    if (Verifica.Count > 0) // Verifico si el LINQ devolvio datos / Giovanny Tapia / 04/10/2012
                    {
                        System.Windows.Forms.MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000");
                        return;
                    }

                    if (StockNegativo == false)
                    {
                        if (Convert.ToDecimal(Item.Cells["STOCK"].Value) - (Convert.ToDecimal(this.txt_Cantidad.Text)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                        {
                            PedidosDetalleItem = new PEDIDOS_DETALLE();

                            PedidosDetalleItem.PDD_CODIGO = 1;
                            PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                            PedidosDetalleItem.PDD_CANTIDAD = Convert.ToDecimal(this.txt_Cantidad.Text);
                            PedidosDetalleItem.PRO_DESCRIPCION = Item.Cells["PRODUCTO"].Value.ToString();
                            PedidosDetalleItem.PDD_VALOR = /*Convert.ToDecimal(Item.Cells["VALOR"].Value)*/ Convert.ToDecimal(this.txtValorUnitario.Text);

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
                            PedidosDetalleItem.PRO_BODEGA_SIC = Sesion.bodega;

                            PedidosDetalle.Add(PedidosDetalleItem);

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

                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Por favor seleccione la cantidad del producto que necesita");
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

                        PedidosDetalle.Add(PedidosDetalleItem);

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

                    }
                }

                else
                {
                    //MessageBox.Show("Por favor seleccione un medicamento de la lista de medicamentos disponibles", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        #endregion


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int cantidad = Convert.ToInt32(cmb_Mostrar.SelectedValue);
                string textoBusqueda = txt_Nombre.Text.Trim() == "" ? null : txt_Nombre.Text.Trim();
                //listaProductosDisponibles = NegProducto.RecuperarProductosLista(1, cantidad, textoBusqueda, codigoArea);




                if (listartodos)
                {
                    Productos = NegProducto.RecuperarProductosListaSPall(1, txt_Nombre.Text.ToString(), 0, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                }
                else if (_Rubro == null)
                {
                    Productos = NegProducto.RecuperarProductosListaSPPedidosAreas(1, txt_Nombre.Text.ToString(), 0, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio, areas.PEA_CODIGO);
                }
                else
                {
                    Productos = NegProducto.RecuperarProductosListaSP(1, txt_Nombre.Text.ToString(), _Rubro.RUB_CODIGO, Sesion.bodega, _CodigoEmpresa, _CodigoConvenio);
                }


                //if (areas.PEA_CODIGO != 1)
                //{

                //    if (listartodos)
                //    {
                //        Productos = NegProducto.RecuperarProductosListaSPall(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                //    }
                //    else
                //    {
                //        Productos = NegProducto.RecuperarProductosListaSP(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                //    }


                //}
                //else
                //{

                //    Productos = NegProducto.RecuperarProductosListaSP_Farmacia(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);

                //}

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


        private void btn_Anadir_Click(object sender, EventArgs e)
        {
            try
            {
                addProducto();
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
            if (e.KeyChar == 13)
            {
                btn_Anadir.Focus();
            }
        }

        private void gridProductosSeleccionados_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
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

            if (e.KeyCode == Keys.F11)
            {
                gbxDetalleHonorarios.Visible = true;
                DetalleHonorariosItem = new DtoDetalleHonorariosMedicos();

                DetalleHonorariosItem.PED_CODIGO = 0;
                DetalleHonorariosItem.ID_LINEA = this.gridProductosSeleccionados.ActiveRow.Index;
                DetalleHonorariosItem.CODPRO = this.gridProductosSeleccionados.ActiveRow.Cells[0].Value.ToString();
                DetalleHonorariosItem.MED_CODIGO = 0;
                DetalleHonorariosItem.FECHA = DateTime.Now;
                DetalleHonorariosItem.CODIGO2 = "";
                DetalleHonorariosItem.VALOR = Convert.ToDecimal(this.gridProductosSeleccionados.ActiveRow.Cells[5].Value);

                DetalleHonorarios.Add(DetalleHonorariosItem);

                dgvDetalleMedicos.DataSource = null;
                dgvDetalleMedicos.DataSource = DetalleHonorarios;

                dgvDetalleMedicos.Columns[0].ReadOnly = true;
                dgvDetalleMedicos.Columns[1].ReadOnly = true;
                dgvDetalleMedicos.Columns[2].ReadOnly = true;
                dgvDetalleMedicos.Columns[4].ReadOnly = true;
                dgvDetalleMedicos.Columns[6].ReadOnly = true;
                dgvDetalleMedicos.Focus();
            }
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {

            descripcion = txt_Observaciones.Text;
            DataTable cantidadReal = new DataTable();
            foreach (var item in gridProductosSeleccionados.Rows)
            {
                cantidadReal = NegHabitaciones.VerificaCantidadStock(Convert.ToInt64(item.Cells["CODIGO"].Value), Sesion.bodega);
                if (Convert.ToDecimal(cantidadReal.Rows[0][0].ToString()) < Convert.ToDecimal(item.Cells["CANTIDAD"].Value))
                {
                    System.Windows.Forms.MessageBox.Show("El Siguiente Producto: " + item.Cells["CODIGO"].Value.ToString() + ", No Cuenta Con Stock Suficiente\r\nStock Solicitado: " + item.Cells["CANTIDAD"].Value + "\r\nStock Físico: " + cantidadReal.Rows[0][0].ToString() + ".\r\nDebe Ser Removido Para Continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            if ((System.Windows.Forms.MessageBox.Show("Esta seguro que desea guardar el pedido.?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.No))
            {
                //frmEmergenciaProcedimientos.observacion = txt_Observaciones.Text; //aqui no habia nada, envio datos de observacion para el ticket
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


        private void gridProductosSeleccionados_InitializeLayout_1(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["CODIGO"].Hidden = false;
            e.Layout.Bands[0].Columns["DESCRIPCION"].Hidden = false;
            e.Layout.Bands[0].Columns["CANTIDAD"].Hidden = false;
            e.Layout.Bands[0].Columns["VALOR"].Hidden = false;
            e.Layout.Bands[0].Columns["IVA"].Hidden = false;
            e.Layout.Bands[0].Columns["TOTAL"].Hidden = false;

            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 300;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["CANTIDAD"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["VALOR"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["IVA"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["TOTAL"].Width = 100;

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
            if (auxFirst == 0)
            {
                txt_Nombre.Focus();
                auxFirst++;
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


        private void txtValorUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_Anadir.Focus();
            }
        }


        private void gridProductosSeleccionados_BeforeRowsDeleted(object sender, Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs e)
        {
            Int32 item = 0;
            item = gridProductosSeleccionados.ActiveRow.Index;

            PedidosDetalle.RemoveAt(item);

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.gbxDetalleHonorarios.Visible = false;
        }

        private void dgvDetalleMedicos_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgvDetalleMedicos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (dgvDetalleMedicos.CurrentCell.ColumnIndex == 3)
                {
                    //His.Admision.
                    List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

                    His.Admision.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
                    //ayuda.campoPadre = txtCodMedico;
                    ayuda.ShowDialog();

                    if (ayuda.campoPadre.Text != string.Empty)
                        //CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                        dgvDetalleMedicos.CurrentCell.Value = ayuda.campoPadre.Text.ToString();
                }
            }
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

                    if (areas.PEA_CODIGO != 1)
                    {

                        if (listartodos)
                        {
                            Productos = NegProducto.RecuperarProductosListaSPall(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                        }
                        else
                        {
                            Productos = NegProducto.RecuperarProductosListaSP(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                        }


                    }
                    else
                    {

                        Productos = NegProducto.RecuperarProductosListaSP_Farmacia(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);

                    }

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

        private void btnGuardarMedicos_Click(object sender, EventArgs e)
        {
            if ((System.Windows.Forms.MessageBox.Show("Desea Guardar el Honorario Con el Codigo de DR. Ingresado", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                this.gbxDetalleHonorarios.Visible = false;
            else
                btnEliminar.PerformClick();
            btnSalir.PerformClick();
        }

        private void frmAyudaProductos_Activated(object sender, EventArgs e)
        {
            txt_Nombre.Focus();
        }
    }
}

