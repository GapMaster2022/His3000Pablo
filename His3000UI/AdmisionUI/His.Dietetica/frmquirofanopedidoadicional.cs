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
using His.Formulario;
using His.Entidades;
using His.Entidades.Clases;
using His.Parametros;

namespace His.Dietetica
{
    public partial class frmquirofanopedidoadicional : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        NegHabitaciones pedido = new NegHabitaciones();
        internal static string ate_codigo; //Codigo de atencion del paciente
        internal static string pac_codigo; //Codigo del paciente
        internal static string cie_codigo; //Codigo del procedimiento
        internal static string id_usuario; //codigo de usuario
        internal static string hab_codigo;//codigo de habitacion

        ATENCIONES ultimaAtencion = new ATENCIONES();
        PACIENTES datosPacientes = new PACIENTES();

        public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()); //por defecto la bodega de quirofano
        public frmquirofanopedidoadicional()
        {
            InitializeComponent();
        }

        public frmquirofanopedidoadicional(int bodega)
        {
            InitializeComponent();
            this.bodega = bodega;
        }
        private void frmquirofanopedidoadicional_Load(object sender, EventArgs e)
        {
            toolStripImprimir.Enabled = false;
            btnDevolucion.Enabled = false;
            CargarTickets();
            CargarProductos();
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            datosPacientes = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
        }
        public void CargarTickets()
        {
            try
            {
                TablaTickets.DataSource = Quirofano.VerTicketsPaciente(ultimaAtencion.ATE_CODIGO.ToString(), bodega);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarProductosSic()
        {
            UltraGridProductos.DataSource = NegQuirofano.ProductosSic("", bodega);
        }
        public void CargarProductos()
        {
            CargarProductosSic();
            Redimencionar();
        }

        public void GuardarCambios()
        {
            string codpro;
            string cantidad;
            if(TablaPedidos.Rows.Count > 0)
            {
                try
                {
                    DataTable TablaProcedimientos = Quirofano.PacienteProcedimiento(cie_codigo, ultimaAtencion.ATE_CODIGO.ToString(), bodega);

                    foreach (DataGridViewRow item in TablaPedidos.Rows)
                    {
                        bool actualizar = false;
                        foreach (DataRow item1 in TablaProcedimientos.Rows)
                        {
                            if (item.Cells["codpro"].Value.ToString() == item1["Codigo"].ToString())
                            { 
                                actualizar = true;
                                break;
                            }
                        }
                        
                        if (!actualizar)
                            Quirofano.AgregarProcedimientoPaciente(item.Cells["orden"].Value.ToString(), cie_codigo, item.Cells["codpro"].Value.ToString(), item.Cells["cant"].Value.ToString(), datosPacientes.PAC_CODIGO.ToString(), ultimaAtencion.ATE_CODIGO.ToString(), null, Sesion.nomUsuario, 1);
                        else
                            Quirofano.ActualizarPedidoAdicional(ate_codigo, cie_codigo, item.Cells["codpro"].Value.ToString(), item.Cells["cant"].Value.ToString());

                    }
                    MessageBox.Show("Pedido guardado correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("¡Algo Ocurrio al Guardar Pedido Adicional! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Tabla de \"Productos Solicitados\" está vacia \n Agregue Productos para Continuar...",
                    "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Desea guardar los cambios?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            {
                GuardarCambios();
            }
            else
            {
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(ultimaAtencion.ESC_CODIGO != 1)
            {
                MessageBox.Show("Paciente ha sido dado de alta.\r\nComuniquese con sistemas.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(TablaPedidos.Rows.Count > 0)
            {
                if(MessageBox.Show("¿Está seguro de generar pedido?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    GuardarCambios();
                    GuardarPedido();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Tabla de \"Productos Solicitados\" esta vacio", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public PEDIDOS_DETALLE PedidosDetalleItem = null;
        PRODUCTO Prod = new PRODUCTO();
        public List<PEDIDOS_DETALLE> PedidosDetalle = new List<PEDIDOS_DETALLE>();
        public void GuardarPedido()
        {
            DateTime fecha = DateTime.Now;
            try
            {
                PedidosDetalle = new List<PEDIDOS_DETALLE>();
                //Quirofano.AgregarPedidoPaciente(Convert.ToString(fecha), id_usuario, ate_codigo, hab_codigo); //Primero almacena el pedido
                //numpedido = Quirofano.RecuperarPedidoNum(ate_codigo); //Recupera el ultimo numero de pedido de acuerdo con la atencion
                foreach (DataGridViewRow item in TablaPedidos.Rows)
                {
                    PedidosDetalleItem = new PEDIDOS_DETALLE();
                    DataTable TablaP = NegProducto.RecuperarProductoSic(item.Cells[0].Value.ToString());
                    //Cambios Edgar 20210601

                    string xIVA = "1." + TablaP.Rows[0]["iva"].ToString(); //SE REGRESA EL VALOR UNITARIO DEL PRECIO DE VENTA
                    double valorsinIVA = Math.Round(Convert.ToDouble(TablaP.Rows[0]["preven"].ToString()) / Convert.ToDouble(xIVA), 2);


                    PedidosDetalleItem.PDD_CODIGO = 1;
                    PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                    PedidosDetalleItem.PDD_CANTIDAD = Convert.ToDecimal(item.Cells[2].Value.ToString());
                    PedidosDetalleItem.PRO_DESCRIPCION = item.Cells[1].Value.ToString();
                    PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(valorsinIVA);
                    PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(item.Cells[2].Value.ToString()) * Convert.ToDecimal(valorsinIVA))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 4);
                    PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(item.Cells[2].Value.ToString()) * Convert.ToDecimal(valorsinIVA)), 2) + Math.Round(((((Convert.ToDecimal(item.Cells[2].Value.ToString()) * Convert.ToDecimal(valorsinIVA))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                    PedidosDetalleItem.PDD_ESTADO = true;
                    PedidosDetalleItem.PDD_COSTO = 0;
                    PedidosDetalleItem.PDD_FACTURA = null;
                    PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                    PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                    PedidosDetalleItem.PDD_RESULTADO = null;
                    PedidosDetalleItem.PRO_CODIGO_BARRAS = item.Cells[0].Value.ToString();

                    PedidosDetalle.Add(PedidosDetalleItem);
                }

                GuardarPedidoQuirofano();

                //Aqui Funcion de impresion de pedido
                GenerarTicket();
            }
            catch (Exception)
            {
                MessageBox.Show("¡Algo ocurrio al guardar pedido!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public string numpedido;
        public bool GuardarPedidoQuirofano()
        {
            try
            {
                Int32 Pedido = 0;
                datosPacientes = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
                var archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
                PEDIDOS_AREAS p_a = NegPedidos.recuperarAreaPorID(100);
                ATENCIONES ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
                int codigoArea = 100;
                if (PedidosDetalle.Count() > 0)
                {
                    string descripcion = "PEDIDO GENERADO DESDE ";
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
                        PED_TRANSACCION = 1
                    };

                    pedido.PEDIDOS_AREAS.PEA_CODIGO = 100;
                    Pedido = pedido.PED_CODIGO;
                    numpedido = Pedido.ToString();
                    //NegPedidos.crearPedido(pedido);


                    if (!NegPedidos.nuevoPedidoProcedimiento(pedido, PedidosDetalle, bodega, Convert.ToInt32(cie_codigo), descripcion))
                    {
                        MessageBox.Show("Pedido no generado. Intente nuevamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    //CREO EL DESPACHO AUTOMATICO PARA QUIROFANO
                    List<DtoDespachos> despachos = new List<DtoDespachos>();
                    DtoDespachos xdespacho = new DtoDespachos();
                    if (bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
                        xdespacho.observacion = "DESPACHADO DESDE QUIROFANO";
                    else
                        xdespacho.observacion = "DESPACHADO DESDE GASTROENTEROLOGIA";
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
        public void GenerarTicket()
        {
            try
            {
                
                //numped = Quirofano.RecuperarPedidoNum(ate_codigo); //Recupera el ultimo numero de pedido de acuerdo con la atencion
                DataTable TablaInfo = new DataTable();
                TablaInfo = Quirofano.RecuperarInfoPaciente(ultimaAtencion.ATE_CODIGO.ToString());
                DataTable Tabla = new DataTable(); //Almacena los productos del pedido generado recientemente
                Tabla = Quirofano.ProductosPaciente(ultimaAtencion.ATE_CODIGO.ToString(), numpedido);

                DatosImpresion DI = new DatosImpresion();
                DataRow drImpresion;
                foreach (DataRow item in Tabla.Rows)
                {
                    drImpresion = DI.Tables["Pedido"].NewRow();
                    if(bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumberDecimal(e, false);
        }
        int contador = 0;
        public void AñadeProducto()
        {

            UltraGridRow Fila = UltraGridProductos.ActiveRow;
            string codpro = "";
            string descripcion;
            string valor;
            if (UltraGridProductos.Selected.Rows.Count == 1)
            {
                codpro = Fila.Cells["CODIGO"].Value.ToString();

                DataTable TablaP = NegProducto.RecuperarProductoSic(codpro);
                if (txtcantidad.Text.Trim() != "")
                {
                    bool existe = false;
                    if (Convert.ToDouble(Fila.Cells["STOCK"].Value.ToString()) != 0 && Convert.ToDouble(Fila.Cells["STOCK"].Value.ToString()) >= Convert.ToDouble(txtcantidad.Text))
                    {
                        DataTable TablaProcedimientos = Quirofano.PacienteProcedimiento(cie_codigo, ultimaAtencion.ATE_CODIGO.ToString(), bodega);

                        foreach (DataGridViewRow item in TablaPedidos.Rows)
                        {

                            if (item.Cells["codpro"].Value.ToString() == codpro)
                            {
                                MessageBox.Show("El producto ya está agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                existe = true;
                                break;
                            }
                        }
                        if (!existe)
                        {
                            int orden = 0;
                            if (contador == 0)
                            {
                                orden = NegQuirofano.ultimoOrden(Convert.ToInt32(cie_codigo), Convert.ToInt64(ultimaAtencion.ATE_CODIGO.ToString()));
                                contador = orden + 1;
                            }
                            codpro = Fila.Cells[0].Value.ToString();
                            descripcion = Fila.Cells[1].Value.ToString();
                            valor = TablaP.Rows[0]["preven"].ToString();
                            TablaPedidos.Rows.Add(codpro, descripcion, txtcantidad.Text, valor, contador.ToString());
                            txtcantidad.Text = "";
                            //txtOrden.Text = "";
                            errorProvider1.Clear();
                            contador++;
                        }
                    }
                    else
                    {
                        MessageBox.Show("¡No hay suficiente Stock!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtcantidad.Text = "1";
                    }
                }
                else
                    errorProvider1.SetError(txtcantidad, "Debe agregar la cantidad, no puede ser 0");
            }
            else
            {
                MessageBox.Show("¡No se ha Elegido Producto ha Agregar!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UltraGridRow Fila = UltraGridProductos.ActiveRow;
            string isDecimal = txtcantidad.Text.Trim();
            bool valido = false;
            for (int i = 0; i < isDecimal.Length; i++)
            {
                if (isDecimal.Substring(i, 1) == ".")
                    valido = true;
            }
            if (valido)
            {
                if (!NegQuirofano.validaDecimales(Fila.Cells["CODIGO"].Value.ToString()))
                {
                    MessageBox.Show("El producto no permite decimales", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtcantidad.Text = "1";
                    return;
                }
            }
            AñadeProducto();
        }
        public void Redimencionar()
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
                UltraGridProductos.DisplayLayout.Bands[0].Columns[1].Width = 300;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[2].Width = 100;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[3].Width = 60;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridProductos.DisplayLayout.Bands[0].Columns[4].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[5].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[8].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[10].Hidden = true;
            }
            catch (Exception)
            {

            }
        }

        private void UltraGridProductos_KeyDown(object sender, KeyEventArgs e)
        {
            UltraGridRow Fila = UltraGridProductos.ActiveRow;
            if(e.KeyCode == Keys.Enter)
            {
                if (UltraGridProductos.Selected.Rows.Count > 0)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        txtcantidad.Text = "1";
                        txtcantidad.Focus();
                    }
                }
            }
            
        }

        private void txtcantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (txtcantidad.Text.Trim() != "")
                {
                    if (Convert.ToDouble(txtcantidad.Text.Trim()) > 0)
                    {
                        UltraGridRow Fila = UltraGridProductos.ActiveRow;
                        string isDecimal = txtcantidad.Text.Trim();
                        bool valido = false;
                        for (int i = 0; i < isDecimal.Length; i++)
                        {
                            if (isDecimal.Substring(i, 1) == ".")
                                valido = true;
                        }
                        if (valido)
                        {
                            if (!NegQuirofano.validaDecimales(Fila.Cells["CODIGO"].Value.ToString()))
                            {
                                MessageBox.Show("El producto no permite decimales", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtcantidad.Text = "1";
                                return;
                            }
                        }
                        AñadeProducto();
                    }
                    else
                        txtcantidad.Text = "1";
                }
                else
                    txtcantidad.Text = "1";
            }
            

                //UltraGridRow Fila = UltraGridProductos.ActiveRow;
                //string codpro = "";
                //string descripcion;
                //string valor;
                //if (UltraGridProductos.Selected.Rows.Count > 0 && txtcantidad.Text != "")
                //{
                //    bool existe = false;
                //    codpro = Fila.Cells[0].Value.ToString();
                //    if (Convert.ToInt32(Fila.Cells["STOCK"].Value.ToString()) > 0)
                //    {
                //        foreach (DataGridViewRow item in TablaPedidos.Rows)
                //        {
                //            if (item.Cells["codpro"].Value.ToString() == codpro)
                //            {
                //                MessageBox.Show("Producto ya ha sido agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //                existe = true;
                //                break;
                //            }
                //        }
                //        if (!existe)
                //        {
                //            codpro = Fila.Cells[0].Value.ToString();
                //            descripcion = Fila.Cells[1].Value.ToString();
                //            valor = Fila.Cells[10].Value.ToString();
                //            TablaPedidos.Rows.Add(codpro, descripcion, txtcantidad.Text, valor);
                //            txtcantidad.Text = "";
                //            UltraGridProductos.Focus();
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("¡No hay suficiente Stock!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("¡No se ha Elegido Producto ha Agregar!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            //}
        }

        private void toolStripImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string numpedido;
                DataTable TablaPedidoProducto = new DataTable();
                DatosImpresion DI = new DatosImpresion();
                DataRow drImpresion;

                if (TablaTickets.SelectedRows.Count > 0)
                {
                    //Mostrara el ticket a farmacia
                    numpedido = TablaTickets.CurrentRow.Cells[2].Value.ToString();
                    TablaPedidoProducto = Quirofano.ProductosPaciente(ultimaAtencion.ATE_CODIGO.ToString(), numpedido);
                    foreach (DataRow item in TablaPedidoProducto.Rows)
                    {
                        drImpresion = DI.Tables["Pedido"].NewRow();
                        if(bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
                            drImpresion["Departamento"] = "REIMPRESION - QUIROFANO";
                        else
                            drImpresion["Departamento"] = "REIMP - GASTROENTEROLOGIA";
                        drImpresion["Atencion"] = TablaTickets.CurrentRow.Cells[0].Value.ToString();
                        drImpresion["Hc"] = TablaTickets.CurrentRow.Cells[1].Value.ToString();
                        drImpresion["Pedido"] = TablaTickets.CurrentRow.Cells[2].Value.ToString();
                        drImpresion["Paciente"] = TablaTickets.CurrentRow.Cells[3].Value.ToString();
                        drImpresion["Fecha"] = TablaTickets.CurrentRow.Cells[4].Value.ToString();
                        drImpresion["Medico"] = TablaTickets.CurrentRow.Cells[5].Value.ToString();
                        drImpresion["Usuario"] = TablaTickets.CurrentRow.Cells[6].Value.ToString();
                        drImpresion["Habitacion"] = item[3].ToString();
                        drImpresion["Cantidad"] = item[0].ToString();
                        drImpresion["Codigo"] = item[1].ToString();
                        drImpresion["Descripcion"] = item[2].ToString();
                        DI.Tables["Pedido"].Rows.Add(drImpresion);
                    }
                    frmReportes Reporte = new frmReportes(1, "PedidoQuirofano", DI);
                    Reporte.Show();
                    MessageBox.Show("Reimpresion de Pedido: " + numpedido, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("¡Debe Elegir un Pedido!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void tools_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                toolStripImprimir.Enabled = false;
                btnDevolucion.Enabled = false;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                toolStripImprimir.Enabled = true;
                btnDevolucion.Enabled = true;
                CargarTickets();
            }
        }

        private void txtOrden_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void TablaPedidos_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            if(TablaTickets.SelectedRows.Count == 1)
            {
                frm_QuirofanoDevolucion x = new frm_QuirofanoDevolucion(bodega);
                x.ped_codigo = TablaTickets.CurrentRow.Cells[2].Value.ToString();
                x.ate_codigo = ultimaAtencion.ATE_CODIGO.ToString();
                x.medico = TablaTickets.CurrentRow.Cells[5].Value.ToString();
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("Debe elegir solo un pedido para realizar la devolucion.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
