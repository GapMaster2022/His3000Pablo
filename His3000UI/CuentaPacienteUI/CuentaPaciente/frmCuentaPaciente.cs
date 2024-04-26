using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Entidades.Productos;
using His.Negocio;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;


namespace CuentaPaciente
{
    public partial class frmCuentaPaciente : Form
    {
        public List<PRODUCTO> listaProductosSolicitados;
        public bool pacienteNuevo = false;
        PACIENTES pacienteActual = new PACIENTES();
        ATENCIONES ultimaAtencion = null;
        List<DtoDetalleCuentaPaciente> listaCuenta = new List<DtoDetalleCuentaPaciente>();
        DataTable dtDetalleFactura = new DataTable();
        private byte codigoEstacion;
        public PRODUCTO producto;

        private int codigoPaciente;
        
        public frmCuentaPaciente()
        {
            InitializeComponent();
            //cargarDatos();
            
        }

        public frmCuentaPaciente(int codigoPaciente)
        {
            InitializeComponent();
            this.codigoPaciente = codigoPaciente;
            cargarDatos(codigoPaciente);
        }

        private void cargarDatos(int codigoPaciente)
        {
            cmb_Areas.DataSource = NegPedidos.recuperarListaAreas().OrderBy(a => a.PEA_NOMBRE).ToList();
            cmb_Areas.ValueMember = "PEA_CODIGO";
            cmb_Areas.DisplayMember = "PEA_NOMBRE";
            cmb_Areas.SelectedIndex = 0;
            ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
            byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));

            if (codigoPaciente != null)
            {
                pacienteActual = NegPacientes.RecuperarPacienteID(codigoPaciente);
                ultimaAtencion = NegAtenciones.RecuperarUltimaAtencion(pacienteActual.PAC_CODIGO);
                listaCuenta = NegDetalleCuenta.recuperarCuentaPaciente(ultimaAtencion.ATE_CODIGO);
                cargarDetalleFactura(listaCuenta);
                btnSolicitar.Enabled = true;
            }
        }

        private void btnSolicitar_Click(object sender, EventArgs e)
        {
            RUBROS Rubro = new RUBROS();
            frmAyudaProductos form = new frmAyudaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem), Rubro);
            form.ShowDialog();
            listaProductosSolicitados = form.listaProductosSolicitados;
            if (listaProductosSolicitados != null)
            {
                Double totales = 0.00;
                Double valores = 0.00;
                Double ivaTotal = 0.00;
                PEDIDOS pedido = new PEDIDOS();
                pedido.PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1;
                pedido.PED_FECHA = DateTime.Now.Date;
                pedido.PED_DESCRIPCION = form.descripcion;
                pedido.PED_ESTADO = 2;
                pedido.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                pedido.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                pedido.PEE_CODIGO = codigoEstacion;
                pedido.TIP_PEDIDO = His.Parametros.FarmaciaPAR.PedidoMedicamentos;
                pedido.PED_PRIORIDAD = 1;
                pedido.PEDIDOS_AREASReference.EntityKey = ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).EntityKey;
                if (form.codTransaccion != 0)
                    pedido.PED_TRANSACCION = form.codTransaccion;
                else
                    pedido.PED_TRANSACCION = pedido.PED_CODIGO;
                //NegPedidos.crearPedido(pedido);
                List<PEDIDOS_DETALLE> listapedidos = new List<PEDIDOS_DETALLE>();
                foreach (var solicitud in listaProductosSolicitados)
                {
                    producto = solicitud;
                    PEDIDOS_DETALLE pedidoDetalle = new PEDIDOS_DETALLE();
                    //registro el detalle del pedido
                    pedidoDetalle.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                    pedidoDetalle.PDD_CANTIDAD = solicitud.PRO_CANTIDAD;
                    pedidoDetalle.PDD_ESTADO = true;
                    pedidoDetalle.PDD_IVA = solicitud.PRO_IVA;
                    pedidoDetalle.PDD_VALOR = solicitud.PRO_PRECIO;
                    pedidoDetalle.PDD_TOTAL = (solicitud.PRO_PRECIO * ((solicitud.PRO_IVA / 100) + 1) * solicitud.PRO_CANTIDAD);
                    pedidoDetalle.PRO_DESCRIPCION = solicitud.PRO_DESCRIPCION;
                    pedidoDetalle.PRODUCTOReference.EntityKey = solicitud.EntityKey;
                    pedidoDetalle.PEDIDOSReference.EntityKey = pedido.EntityKey;
                    totales = totales + Convert.ToDouble(pedidoDetalle.PDD_TOTAL);
                    valores = valores + Convert.ToDouble(pedidoDetalle.PDD_VALOR);
                    ivaTotal = ivaTotal + Convert.ToDouble(pedidoDetalle.PDD_IVA);
                    listapedidos.Add(pedidoDetalle);
                }
                    NegPedidos.crearDetallePedido(listapedidos, pedido,"");
            }
            listaCuenta = NegDetalleCuenta.recuperarCuentaPaciente(ultimaAtencion.ATE_CODIGO);
            cargarDetalleFactura(listaCuenta);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (pacienteNuevo == false)
                {
                    if (codigoPaciente != null)
                    {
                        pacienteActual = NegPacientes.RecuperarPacienteID(codigoPaciente);
                        ultimaAtencion = NegAtenciones.RecuperarUltimaAtencion(pacienteActual.PAC_CODIGO);
                        listaCuenta = NegDetalleCuenta.recuperarCuentaPaciente(ultimaAtencion.ATE_CODIGO);
                        cargarDetalleFactura(listaCuenta);
                        btnSolicitar.Enabled = true;
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cargarDetalleFactura(List<DtoDetalleCuentaPaciente> listaCuenta)
        {
            if (listaCuenta != null)
            {
                dtDetalleFactura = new DataTable();
                gridDetalleFactura.DataSource = dtDetalleFactura;
                dtDetalleFactura.Columns.Add("INDICE", Type.GetType("System.String"));
                dtDetalleFactura.Columns.Add("RUBRO", Type.GetType("System.String"));
                dtDetalleFactura.Columns.Add("TOTAL", Type.GetType("System.String"));
                List<RUBROS> listaRubros = new List<RUBROS>();
                listaRubros = NegRubros.recuperarRubros();
                RUBROS rubros = new RUBROS();
                int index = 0;
                int fila = gridDetalleFactura.Rows.Count;
                DataRow drDetalle;
                for (int i = 0; i < listaRubros.Count; i++)
                {
                    index = index + 1;
                    rubros = listaRubros.ElementAt(i);
                    DtoDetalleCuentaPaciente cuenta = new DtoDetalleCuentaPaciente();
                    double total = 0;
                    bool accion = false;
                    int j = 0;
                    for (j = 0; j < listaCuenta.Count; j++)
                    {
                        cuenta = listaCuenta.ElementAt(j);

                        if (rubros.RUB_CODIGO == cuenta.RUBRO)
                        {
                            total = total + Convert.ToDouble(cuenta.VALOR);
                            accion = true;
                        }
                        if (j == listaCuenta.Count - 1 && accion == true)
                        {
                            drDetalle = dtDetalleFactura.NewRow();
                            drDetalle["INDICE"] = index;
                            drDetalle["RUBRO"] = rubros.RUB_NOMBRE;
                            drDetalle["TOTAL"] = total;
                            dtDetalleFactura.Rows.Add(drDetalle);
                            accion = false;
                        }
                    }
                }
            }
            UltraGridBand bandUno = gridDetalleFactura.DisplayLayout.Bands[0];
            gridDetalleFactura.DisplayLayout.ViewStyle = ViewStyle.SingleBand;

            gridDetalleFactura.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            gridDetalleFactura.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            gridDetalleFactura.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            gridDetalleFactura.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            gridDetalleFactura.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            gridDetalleFactura.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            gridDetalleFactura.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            gridDetalleFactura.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            gridDetalleFactura.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            gridDetalleFactura.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            gridDetalleFactura.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            gridDetalleFactura.DisplayLayout.UseFixedHeaders = true;

            bandUno.Columns["INDICE"].Header.Caption = "INDICE";
            bandUno.Columns["RUBRO"].Header.Caption = "RUBRO";
            bandUno.Columns["TOTAL"].Header.Caption = "TOTAL";
            bandUno.Columns["INDICE"].Width = 50;
            bandUno.Columns["RUBRO"].Width = 570;
            bandUno.Columns["TOTAL"].Width = 80;
            bandUno.Columns["INDICE"].Hidden = false;
            bandUno.Columns["RUBRO"].Hidden = false;
            bandUno.Columns["TOTAL"].Hidden = false;
        }    
    }
}
