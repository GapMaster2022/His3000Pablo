using His.Entidades;
using His.Entidades.Clases;
using His.Formulario;
using His.Negocio;
using His.Parametros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class frm_QuirofanoNuevo : Form
    {
        public frm_QuirofanoNuevo()
        {
            InitializeComponent();
        }
        public string cie_codigo, ate_codigo, pac_codigo;
        public PEDIDOS_DETALLE PedidosDetalleItem = null;
        PRODUCTO Prod = new PRODUCTO();
        public List<PEDIDOS_DETALLE> PedidosDetalle = new List<PEDIDOS_DETALLE>();
        NegQuirofano Quirofano = new NegQuirofano();
        public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()); //por defecto la bodega de quirofano
        private void btnproducto_Click(object sender, EventArgs e)
        {
            frmayudaQuirofano.productosPedidos = true;
            frmayudaQuirofano x = new frmayudaQuirofano();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        public frm_QuirofanoNuevo(int bodega)
        {
            InitializeComponent();
            this.bodega = bodega;
        }

        public static string codigo_producto, orden, cant;

        private void btnanestesia_Click(object sender, EventArgs e)
        {
            frmayudaQuirofano x = new frmayudaQuirofano();
            x.anestesia = true;
            x.Show();
            x.FormClosed += X_FormClosed1;
        }

        private void X_FormClosed1(object sender, FormClosedEventArgs e)
        {
            DataTable Anestesia = NegQuirofano.AnestesiaSolicitada(Convert.ToInt32(codigo_producto));
            PedidosDetalle = new List<PEDIDOS_DETALLE>();
            foreach (DataRow item in Anestesia.Rows)
            {
                DataTable TablaP = NegProducto.RecuperarProductoSic(item["CODPRO"].ToString());

                PedidosDetalleItem = new PEDIDOS_DETALLE();
                PedidosDetalleItem.PDD_CODIGO = 1;
                PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                PedidosDetalleItem.PDD_CANTIDAD = Convert.ToInt32(item["QPP_CANTIDAD"].ToString());
                PedidosDetalleItem.PRO_DESCRIPCION = TablaP.Rows[0]["despro"].ToString();
                PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(TablaP.Rows[0]["precos"].ToString());
                PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(TablaP.Rows[0]["precos"].ToString()) * Convert.ToDecimal(item["QPP_CANTIDAD"].ToString()))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(TablaP.Rows[0]["precos"].ToString()) * Convert.ToDecimal(item["QPP_CANTIDAD"].ToString())), 2) + Math.Round(((((Convert.ToDecimal(TablaP.Rows[0]["precos"].ToString()) * Convert.ToDecimal(cant))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                PedidosDetalleItem.PDD_ESTADO = true;
                PedidosDetalleItem.PDD_COSTO = 0;
                PedidosDetalleItem.PDD_FACTURA = null;
                PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                PedidosDetalleItem.PDD_RESULTADO = null;
                PedidosDetalleItem.PRO_CODIGO_BARRAS = item["CODPRO"].ToString();

                PedidosDetalle.Add(PedidosDetalleItem);
                Quirofano.AgregarProcedimientoPaciente(item["QPP_ORDEN"].ToString(), cie_codigo, item["CODPRO"].ToString(), item["QPP_CANTIDAD"].ToString(), pac_codigo, ate_codigo, null, Sesion.nomUsuario, 0);
              
            }
            MessageBox.Show("La anestesia ha sido agregada correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //SE COMENTA PARA NO HACER EL PEDIDO DIRECTAMENTE
            //GuardarPedidoQuirofano();
            //GenerarTicket();
            this.Close();
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (codigo_producto != null && orden != null && cant != null)
            {
                try
                {
                    PedidosDetalle = new List<PEDIDOS_DETALLE>();
                    DataTable TablaProcedimientos = Quirofano.PacienteProcedimiento(cie_codigo, ate_codigo, bodega);
                    bool existe = false;
                    
                    foreach (DataRow item in TablaProcedimientos.Rows)
                    {
                        if (item["Codigo"].ToString() == codigo_producto)
                        {
                            MessageBox.Show("El producto ya está agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        //Cambios 20210607
                        DateTime fecha = DateTime.Now;
                        DataTable TablaP = NegProducto.RecuperarProductoSic(codigo_producto);

                        //Quirofano.AgregarProcedimientoPaciente(orden, cie_codigo, codpro, cantidad, pac_codigo, ate_codigo, "0", usuario);
                        //Quirofano.PedidoDetalle(codpro, pro_descripcion, cantidad, valor.ToString(), total.ToString(), numpedido);

                        //Cambios Edgar 20210601
                        PedidosDetalleItem = new PEDIDOS_DETALLE();
                        PedidosDetalleItem.PDD_CODIGO = 1;
                        PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                        PedidosDetalleItem.PDD_CANTIDAD = Convert.ToInt32(cant);
                        PedidosDetalleItem.PRO_DESCRIPCION = TablaP.Rows[0]["despro"].ToString();
                        PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(TablaP.Rows[0]["precos"].ToString());
                        PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(TablaP.Rows[0]["precos"].ToString()) * Convert.ToDecimal(cant))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                        PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(TablaP.Rows[0]["precos"].ToString()) * Convert.ToDecimal(cant)), 2) + Math.Round(((((Convert.ToDecimal(TablaP.Rows[0]["precos"].ToString()) * Convert.ToDecimal(cant))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                        PedidosDetalleItem.PDD_ESTADO = true;
                        PedidosDetalleItem.PDD_COSTO = 0;
                        PedidosDetalleItem.PDD_FACTURA = null;
                        PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                        PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                        PedidosDetalleItem.PDD_RESULTADO = null;
                        PedidosDetalleItem.PRO_CODIGO_BARRAS = TablaP.Rows[0]["Codigo"].ToString();

                        PedidosDetalle.Add(PedidosDetalleItem);
                        Quirofano.AgregarProcedimientoPaciente(orden, cie_codigo, codigo_producto, cant, pac_codigo, ate_codigo, null, Sesion.nomUsuario, 0);
                        MessageBox.Show("El Producto ha sido Agregado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //SE COMENTA PARA QUE EL PEDIDO NO SE AGREGE
                        //GuardarPedidoQuirofano();
                        //GenerarTicket();
                        
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void GuardarPedidoQuirofano()
        {
            try
            {
                Int32 Pedido = 0;
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
                        HAB_CODIGO = ultimaAtencion.HABITACIONES.hab_Codigo
                    };

                    Pedido = pedido.PED_CODIGO;
                    numpedido = Pedido.ToString();
                    NegPedidos.crearPedido(pedido);

                    Int32 xcodDiv = 0;
                    Int16 XRubro = 0;
                    DataTable auxDT = new DataTable();
                    foreach (var solicitud in PedidosDetalle)
                    {
                        Int32 codpro = Convert.ToInt32(solicitud.PRO_CODIGO_BARRAS.ToString());
                        if (codigoArea != 1)
                        {
                            solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
                            solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                            auxDT = NegFactura.recuperaCodRubro(codpro);
                            foreach (DataRow row in auxDT.Rows)
                            {
                                XRubro = Convert.ToInt16(row[0].ToString());
                                xcodDiv = Convert.ToInt32(row[1].ToString());
                            }
                            NegPedidos.crearDetallePedidoQuirofano(solicitud, pedido, XRubro, xcodDiv, descripcion);
                            //Quirofano.ProductoBodega(codpro.ToString(), solicitud.PDD_CANTIDAD.ToString(), bodega);
                            DataTable TablaP = NegProducto.RecuperarProductoSic(solicitud.PRO_CODIGO_BARRAS);
                            //ACTUALIZO dentro del Kardex
                            NegQuirofano.ActualizarKardexSic(pedido.PED_CODIGO.ToString(), bodega);
                        }
                        else
                        {
                            string CodigoProducto = "";
                            decimal ValorIva = 0;
                            solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
                            solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                            CodigoProducto = Convert.ToString(solicitud.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value).Substring(0, 1);
                            ValorIva = Convert.ToDecimal(solicitud.PDD_IVA);
                            auxDT = NegFactura.recuperaCodRubro(codpro);
                            foreach (DataRow row in auxDT.Rows)
                            {
                                XRubro = Convert.ToInt16(row[0].ToString());
                                xcodDiv = Convert.ToInt32(row[1].ToString());
                            }
                            NegPedidos.crearDetallePedidoQuirofano(solicitud, pedido, XRubro, xcodDiv, descripcion);
                            //Resta de Bodega 
                            //Quirofano.ProductoBodega(codpro.ToString(), solicitud.PDD_CANTIDAD.ToString(), bodega);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string numpedido;
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
                if (bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
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
    }
}
