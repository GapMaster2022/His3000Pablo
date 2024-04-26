using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Parametros;
using His.Formulario;

namespace His.Dietetica
{
    public partial class frm_QuirofanoAgregarVarios : Form
    {
        public string ate_codigo = "";
        public string pac_codigo = "";
        public string pci_codigo = "";

        private string numpedido;
        public bool adicional = false;
        public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()); //por defecto la bodega de quirofano
        NegQuirofano Quirofano = new NegQuirofano();

        ATENCIONES ultimaAtencion = new ATENCIONES();
        PACIENTES datosPacientes = new PACIENTES();
        public frm_QuirofanoAgregarVarios()
        {
            InitializeComponent();
        }
        public frm_QuirofanoAgregarVarios(int bodega)
        {
            InitializeComponent();
            this.bodega = bodega;
        }
        public frm_QuirofanoAgregarVarios(int bodega, string _ate_codigo, string _pac_codigo)
        {
            InitializeComponent();
            this.bodega = bodega;
            this.ate_codigo = _ate_codigo;
            this.pac_codigo = _pac_codigo;
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            datosPacientes = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
            pci_codigo = Convert.ToString(NegQuirofano.IdRegistroQuirofano(Convert.ToInt64(ate_codigo)));
            adicional = true;
        }
        private void frm_QuirofanoAgregarVarios_Load(object sender, EventArgs e)
        {
            cmb_Mostrar.SelectedIndex = 0;
            CargarProductosSic();
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            datosPacientes = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
            txt_filtro.Focus();
        }

        public void CargarProductosSic()
        {
            UltraGridProductos.DataSource = NegQuirofano.ProductosSic(txt_filtro.Text.Trim(), bodega);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void UltraGridProductos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = UltraGridProductos.DisplayLayout.Bands[0];

                UltraGridProductos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;


                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridProductos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridProductos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridProductos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                UltraGridProductos.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                UltraGridProductos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridProductos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridProductos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridProductos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridProductos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridProductos.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridProductos.DisplayLayout.Bands[0].Columns[0].Width = 60;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[1].Width = 400;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[2].Width = 100;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[3].Width = 80;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridProductos.DisplayLayout.Bands[0].Columns[4].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[5].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[8].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[10].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UltraGridProductos_KeyDown(object sender, KeyEventArgs e)
        {
            UltraGridRow Fila = UltraGridProductos.ActiveRow;
            if (e.KeyCode == Keys.Enter)
            {
                if (UltraGridProductos.Selected.Rows.Count == 1)
                {
                    txtCantidad.Text = "1";
                    txtCantidad.Focus();
                }
                else
                {
                    MessageBox.Show("Debe elegir un producto para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        int contador = 0;
        public void AñadeProducto()
        {

            UltraGridRow Fila = UltraGridProductos.ActiveRow;
            string codpro = "";
            string descripcion;
            REPOSICION_QUIROFANO rp = null;
            if (UltraGridProductos.Selected.Rows.Count == 1)
            {
                codpro = Fila.Cells["CODIGO"].Value.ToString();
                if(NegQuirofano.recuperaEstadoUltimoProcedimiento(ultimaAtencion.ATE_CODIGO))
                   rp = NegQuirofano.exieteRegistro(ultimaAtencion.ATE_CODIGO, Convert.ToInt64(pci_codigo), codpro);

                if (rp == null)
                {
                    if (txtCantidad.Text.Trim() != "")
                    {
                        bool existe = false;


                        if (Convert.ToInt32(Fila.Cells["STOCK"].Value.ToString()) != 0 && Convert.ToInt32(Fila.Cells["STOCK"].Value.ToString()) >= Convert.ToInt32(txtCantidad.Text))
                        {
                            DataTable TablaProcedimientos = Quirofano.PacienteProcedimiento(pci_codigo, ultimaAtencion.ATE_CODIGO.ToString(), bodega);

                            if (TablaPedidos.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow item in TablaPedidos.Rows)
                                {

                                    if (item.Cells["codpro"].Value.ToString() == codpro)
                                    {
                                        MessageBox.Show("El producto ya está agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        existe = true;
                                        break;
                                    }
                                    else
                                    {
                                        foreach (DataRow item1 in TablaProcedimientos.Rows)
                                        {
                                            if (item1["Codigo"].ToString() == item.Cells["codpro"].Value.ToString())
                                            {
                                                MessageBox.Show("El producto ya está agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                existe = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (DataRow item in TablaProcedimientos.Rows)
                                {
                                    if (item["Codigo"].ToString() == codpro)
                                    {
                                        MessageBox.Show("El producto ya está agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        existe = true;
                                        break;
                                    }
                                }
                            }
                            if (!existe)
                            {
                                //int orden = 0;
                                //if (contador == 0)
                                //{
                                //    orden = NegQuirofano.ultimoOrden(Convert.ToInt32(pci_codigo), ultimaAtencion.ATE_CODIGO);
                                //    contador = orden + 1;
                                //}
                                codpro = Fila.Cells[0].Value.ToString();
                                descripcion = Fila.Cells[1].Value.ToString();
                                //valor = Fila.Cells[10].Value.ToString();
                                TablaPedidos.Rows.Add(codpro, descripcion, txtCantidad.Text, "0");
                                txtCantidad.Text = "";
                                txtOrden.Text = "";
                                errorProvider1.Clear();
                                contador++;
                                txt_filtro.Text = "";
                                txt_filtro.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("¡No hay suficiente Stock!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtCantidad.Text = "1";
                        }
                        //if (txtOrden.Text != "")
                        //{

                        //}
                        //else
                        //    errorProvider1.SetError(txtCantidad, "Debe agregar el orden, no puede ser 0");
                    }
                    else
                        errorProvider1.SetError(txtCantidad, "Debe agregar la cantidad, no puede ser 0");
                }
                else
                    MessageBox.Show("¡El producto ya ha sigo agregado!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("¡No se ha Elegido Producto ha Agregar!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AñadeProducto();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CargarProductosSic();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TablaPedidos.Rows.Count > 0)
                {
                    DataTable cantidadReal = new DataTable();
                    foreach (DataGridViewRow item in TablaPedidos.Rows)
                    {
                        cantidadReal = NegHabitaciones.VerificaCantidadStock(Convert.ToInt64(item.Cells["codpro"].Value), Sesion.bodega);
                        if (Convert.ToDecimal(cantidadReal.Rows[0][0].ToString()) < Convert.ToDecimal(item.Cells["cant"].Value))
                        {
                            System.Windows.Forms.MessageBox.Show("El Siguiente Producto: " + item.Cells["codpro"].Value.ToString() + ", No Cuenta Con Stock Suficiente\r\nStock Solicitado: " + item.Cells["cant"].Value + "\r\nStock Físico: " + cantidadReal.Rows[0][0].ToString() + ".\r\nDebe Ser Removido Para Continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    OrdenarProductos();
                    List<REPOSICION_QUIROFANO> obj = new List<REPOSICION_QUIROFANO>();
                    foreach (DataGridViewRow item in TablaPedidos.Rows)
                    {

                        var repo = new REPOSICION_QUIROFANO();

                        repo.RQ_CANTIDAD = Convert.ToInt16(item.Cells["cant"].Value.ToString());
                        repo.CODPRO = item.Cells["codpro"].Value.ToString();
                        repo.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                        repo.PCI_CODIGO = Convert.ToInt64(pci_codigo);
                        repo.RQ_FECHAPEDIDO = DateTime.Now;
                        repo.RQ_FECHACREACION = DateTime.Now;
                        repo.RQ_FECHAREPOSICION = DateTime.Now;
                        repo.ID_USUARIO = Sesion.codUsuario;
                        repo.RQ_CANTIDADADICIONAL = 0;
                        repo.RQ_CANTIDADDEVOLUCION = 0;

                        obj.Add(repo);
                        //Quirofano.AgregarProcedimientoPaciente(item.Cells["orden"].Value.ToString(), pci_codigo, item.Cells["codpro"].Value.ToString(), item.Cells["cant"].Value.ToString(), datosPacientes.PAC_CODIGO.ToString(), ultimaAtencion.ATE_CODIGO.ToString(), null, Sesion.nomUsuario, 0);
                    }
                    if (NegQuirofano.GuarddaReposicion(obj))
                    {

                        if (adicional)
                        {
                            GeneraPedido();
                        }
                        MessageBox.Show("Información Guardada con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                    MessageBox.Show("Producto(s) agregados correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No tiene producto para guardar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al guardar los productos" + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        List<PEDIDOS_DETALLE> PedidosDetalle = new List<PEDIDOS_DETALLE>();
        private void GeneraPedido()
        {
            Int64 cie_codigo = NegQuirofano.IdRegistroQuirofano(Convert.ToInt64(ate_codigo));
            ATENCIONES nuevaConsulta = new ATENCIONES();
            nuevaConsulta = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            //CargarProcedimientosPaciente();
            if (nuevaConsulta.ESC_CODIGO != 1)
            {
                MessageBox.Show("Paciente ha sido dado de alta.\r\nComuniquese con caja.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                DataTable TablaPro = new DataTable();
                bool error = false;
                if (error == false)
                {
                    try
                    {

                        for (int i = 0; i < TablaPedidos.Rows.Count; i++)
                        {
                            double costo, total;

                            string codpro = TablaPedidos.Rows[i].Cells[0].Value.ToString();// item[0].ToString();
                            string cant_usada = TablaPedidos.Rows[i].Cells[2].Value.ToString();// item[4].ToString();
                            Quirofano.PedidoAdicional(ate_codigo, pac_codigo, Convert.ToString(cie_codigo), cant_usada, codpro); //Aqui graba cuando ya se decida hacer el pedido adicional

                            //PEDIDO ADICION -- CUENTA PACIENTE
                            if (Convert.ToDouble(TablaPedidos.Rows[i].Cells[2].Value.ToString()) > 0) //if (Convert.ToDouble(item[4].ToString()) > 0)
                            {
                                PEDIDOS_DETALLE PedidosDetalleItem = new PEDIDOS_DETALLE();
                                codpro = TablaPedidos.Rows[i].Cells[0].Value.ToString();// item[0].ToString();
                                DataTable TablaP = NegProducto.RecuperarProductoSic(codpro);
                                PRODUCTO Prod = NegProducto.RecuperarProductoID(Convert.ToInt32(codpro));
                                string pro_descripcion = TablaPedidos.Rows[i].Cells[1].Value.ToString();// item[1].ToString();
                                string cantidad = TablaPedidos.Rows[i].Cells[2].Value.ToString();// item[4].ToString();
                                costo = Convert.ToDouble(TablaP.Rows[0][13].ToString());
                                total = Math.Round((Convert.ToDouble(cantidad) * costo), 2);



                                //Cambios Edgar 20210601
                                string xIVA = "1." + TablaP.Rows[0]["iva"].ToString(); //SE REGRESA EL VALOR UNITARIO DEL PRECIO DE VENTA
                                double valorsinIVA = Convert.ToDouble(TablaP.Rows[0][7].ToString());

                                PedidosDetalleItem.PDD_CODIGO = 1;
                                PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                                PedidosDetalleItem.PDD_CANTIDAD = Math.Round(Convert.ToDecimal(cantidad), 2);//Math.Round(Convert.ToDecimal(item[4].ToString()), 2);
                                PedidosDetalleItem.PRO_DESCRIPCION = pro_descripcion;// item[1].ToString();
                                PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(valorsinIVA);
                                PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(cantidad))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 4);//Math.Round(((((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(item[4].ToString()))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 4);
                                PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(cantidad)), 2) + Math.Round(((((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(cantidad))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                                PedidosDetalleItem.PDD_ESTADO = true;
                                PedidosDetalleItem.PDD_COSTO = 0;
                                PedidosDetalleItem.PDD_FACTURA = null;
                                PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                                PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                                PedidosDetalleItem.PDD_RESULTADO = null;
                                PedidosDetalleItem.PRO_CODIGO_BARRAS = TablaPedidos.Rows[i].Cells[0].Value.ToString();//item[0].ToString();
                                PedidosDetalleItem.PRO_BODEGA_SIC = bodega;

                                PedidosDetalle.Add(PedidosDetalleItem);
                            }

                        }
                        if (GuardarPedidoQuirofano(cie_codigo))
                        {
                            GenerarTicket();
                            //MessageBox.Show("El despacho se realizo con éxito.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close();
                        }
                        else
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo Ocurrio al Cerrar el Procedimiento.\r\nMás detalle: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        public void GenerarTicket()
        {
            DatosImpresion DI = new DatosImpresion();
            DataRow drImpresion;
            DataTable TablaInfo = new DataTable();
            DataTable Tabla = new DataTable(); //Almacena los productos del pedido generado recientemente
            Tabla = Quirofano.ProductosPaciente(ate_codigo, numpedido);
            TablaInfo = Quirofano.RecuperarInfoPaciente(ate_codigo);

            foreach (DataRow item in Tabla.Rows)
            {
                drImpresion = DI.Tables["Pedido"].NewRow();
                if (bodega == NegParametros.ParametroBodegaQuirofano())
                    drImpresion["Departamento"] = "QUIROFANO";
                else
                    drImpresion["Departamento"] = "GASTROENTEROLOGIA";
                drImpresion["Atencion"] = TablaInfo.Rows[0][0].ToString();
                drImpresion["Hc"] = TablaInfo.Rows[0][1].ToString();
                drImpresion["Pedido"] = numpedido;
                drImpresion["Paciente"] = TablaInfo.Rows[0][2].ToString();
                drImpresion["Fecha"] = TablaInfo.Rows[0][3].ToString();
                drImpresion["Medico"] = TablaInfo.Rows[0][4].ToString();
                drImpresion["Usuario"] = TablaInfo.Rows[0][5].ToString();
                drImpresion["Habitacion"] = TablaInfo.Rows[0][6].ToString();
                drImpresion["Cantidad"] = item[0].ToString();
                drImpresion["Codigo"] = item[1].ToString();
                drImpresion["Descripcion"] = item[2].ToString();
                DI.Tables["Pedido"].Rows.Add(drImpresion);
            }

            frmReportes Reporte = new frmReportes(1, "PedidoQuirofano", DI);
            Reporte.Show();
            MessageBox.Show("Pedido Nro: " + numpedido + " Generado Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool GuardarPedidoQuirofano(Int64 cie_codigo)
        {
            try
            {
                Int32 Pedido = 0;
                PACIENTES paciente = new PACIENTES();
                paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
                var archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
                PEDIDOS_AREAS p_a = NegPedidos.recuperarAreaPorID(100);
                ATENCIONES ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
                //QUIROFANO_PROCE_PRODU reg = NegQuirofano.habitacionProcedimiento(ate_codigo, Convert.ToInt64(cie_codigo));
                int codigoArea = 100;
                if (PedidosDetalle.Count() > 0)
                {
                    string descripcion = "ADICIONAL-PEDIDO GENERADO DESDE ";
                    if (bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
                        descripcion = descripcion + "QUIROFANO.";
                    else
                        descripcion = descripcion + "GASTROENTEROLOGIA.";
                    var pedido = new PEDIDOS
                    {
                        PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1,
                        PED_FECHA = DateTime.Now /*PARA GUARDAR EL PEDIDO SE NECESITA FECHA Y HORA/ GIOVANNY TAPIA / 12/04/2013*/,
                        PED_DESCRIPCION = descripcion,
                        PED_ESTADO = FarmaciaPAR.PedidoPendiente,
                        ID_USUARIO = Sesion.codUsuario,
                        ATE_CODIGO = ultimaAtencion.ATE_CODIGO,
                        PEE_CODIGO = codigoEstacion,
                        TIP_PEDIDO = FarmaciaPAR.PedidoMedicamentos,
                        PED_PRIORIDAD = 1,
                        MED_CODIGO = 0,
                        PEDIDOS_AREASReference = { EntityKey = p_a.EntityKey },
                        HAB_CODIGO = ultimaAtencion.HABITACIONES.hab_Codigo,
                        PED_TRANSACCION = 1,
                        IP_MAQUINA = Sesion.ip
                    };

                    Pedido = pedido.PED_CODIGO;
                    numpedido = Pedido.ToString();
                    //NegPedidos.crearPedido(pedido);

                    Int32 xcodDiv = 0;
                    Int16 XRubro = 0;
                    DataTable auxDT = new DataTable();

                    List<PEDIDOS_DETALLE> pdetalle = new List<PEDIDOS_DETALLE>();
                    List<CUENTAS_PACIENTES> cuentaPaciente = new List<CUENTAS_PACIENTES>();
                    List<REPOSICION_QUIROFANO> reposicion = new List<REPOSICION_QUIROFANO>();
                    DateTime Hoy = DateTime.Now;
                    if (!NegPedidos.nuevoPedidoProcedimiento(pedido, PedidosDetalle, bodega, Convert.ToInt32(cie_codigo), descripcion))
                    {

                        MessageBox.Show("Pedido no generado. Intente nuevamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    //CREO EL DESPACHO AUTOMATICO PARA QUIROFANO
                    List<DtoDespachos> despachos = new List<DtoDespachos>();
                    DtoDespachos xdespacho = new DtoDespachos();
                    if (bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
                        xdespacho.observacion = "DESPACHADO DESDE QUIROFANO-producto adicional";
                    else
                        xdespacho.observacion = "DESPACHADO DESDE GASTROENTEROLOGIA-producto adicional";
                    xdespacho.ped_codigo = Convert.ToInt64(numpedido);

                    despachos.Add(xdespacho);

                    if (!NegPedidos.InsertarDespachos(despachos, 0, DateTime.Now))
                    {
                        MessageBox.Show("No se pudo realizar despacho automatico." +
                            "\r\nIntentelo manual desde el modulo de pedidos - Control despachos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtCantidad.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtCantidad.Text.Trim()) > 0)
                    {
                        AñadeProducto();
                    }
                    else
                        txtCantidad.Text = "1";
                }
                else
                    txtCantidad.Text = "1";
            }
        }

        private void txtOrden_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtOrden.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtOrden.Text.Trim()) > 0)
                    {
                        AñadeProducto();
                    }
                    else
                        txtOrden.Text = "1";
                }
                else
                    txtOrden.Text = "1";
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void OrdenarProductos()
        {
            int temp = 0;
            temp = NegQuirofano.ultimoOrden(Convert.ToInt32(pci_codigo), ultimaAtencion.ATE_CODIGO) + 1;

            int val = 0;
            foreach (DataGridViewRow item in TablaPedidos.Rows)
            {
                TablaPedidos.Rows[val].Cells["Orden"].Value = temp;
                temp++;
                val++;
            }
            contador = temp;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OrdenarProductos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CargarProductosSic();
            UltraGridProductos.Focus();
        }

        private void txt_filtro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarProductosSic();
                UltraGridProductos.Focus();
            }
            else if (e.KeyCode == Keys.Tab)
            {
                button3.Focus();
            }
        }

        private void toolStripImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}
