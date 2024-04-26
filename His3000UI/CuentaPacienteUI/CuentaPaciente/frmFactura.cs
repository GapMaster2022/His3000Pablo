using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using Infragistics.Win.UltraWinGrid;
using His.Parametros;
using His.Entidades.Reportes;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using frmImpresionPedidos = His.HabitacionesUI.frmImpresionPedidos;
using System.Globalization;
using System.Text.RegularExpressions;
using His.Formulario;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Mail;

namespace CuentaPaciente
{
    public partial class frmFactura : Form
    {
        #region Variables        
        string ate_codigo;
        string path;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public Int16 codigoCaja;
        PRODUCTO producto;
        FACTURA nuevaFactura = new FACTURA();
        FACTURA_DETALLE facturaDetalle = new FACTURA_DETALLE();
        FACTURA_FORMA_PAGO facturaFormaPago = new FACTURA_FORMA_PAGO();
        List<FACTURA_FORMA_PAGO> listaFacturaPagos = new List<FACTURA_FORMA_PAGO>();
        List<RUBROS> listaRubros = new List<RUBROS>();
        Boolean accionPaciente = false;
        Boolean accionFactura = false;
        Boolean accionPagos = false;
        bool pacienteNuevo = false;
        int codigoFactura;
        int codigoAtencion;
        string verificavalor = "";
        PACIENTES pacienteFactura = new PACIENTES();
        PACIENTES_DATOS_ADICIONALES datosPacienteActual = null;
        ASEGURADORAS_EMPRESAS empresaPaciente = null;
        List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = null;
        ATENCIONES ultimaAtencion = null;
        List<FORMA_PAGO> listaFormaPago = new List<FORMA_PAGO>();
        FORMA_PAGO formaPago = new FORMA_PAGO();
        DataTable dtFormasPagos = new DataTable();
        DataTable dtDetalleFactura = new DataTable();
        List<DtoDetalleCuentaPaciente> listaCuenta = new List<DtoDetalleCuentaPaciente>();
        CAJAS caja = new CAJAS();
        NUMERO_CONTROL_CAJAS ncajas = new NUMERO_CONTROL_CAJAS();
        MEDICOS medico = new MEDICOS();
        string DirectorioBaseSic3000 = "";
        string NumeroFactura = "";
        string NumeroCaja = "";
        List<PEDIDOS_DETALLE> listaProductosSolicitados;
        private byte codigoEstacion;
        DtoFacturaSic3000 FacturaSic3000 = new DtoFacturaSic3000();
        DtoFacturaDetalleSic3000 DetalleFactura = new DtoFacturaDetalleSic3000();
        List<DtoFacturaDetalleSic3000> ListaDetalleFactura = new List<DtoFacturaDetalleSic3000>();
        int CodigoConvenio = 0;
        int CodigoTipoEmpresa = 0;
        int CodigoAseguradora = 0;
        double precosmed = 0;
        double precosins = 0;
        Int64 NumeroPrefactura = 0;
        int facturaPrefactura = 0;
        TextBox VectorCodigos = new TextBox();
        string VectorCodigoOK = "0";
        public bool facturado = true;
        Int16 id = 0;
        #endregion

        #region Constructor
        public frmFactura(Int16 codigoCaja)
        {
            InitializeComponent();
            this.codigoCaja = codigoCaja;
            cargarDatos();
            txtNumeroAtencion.ReadOnly = true;
            txt_Historia_Pc.ReadOnly = true;
        }

        public frmFactura(string INIPath)
        {
            path = INIPath;
        }

        #endregion

        #region Métodos

        private void cargarDatos()
        {
            btnNuevo.Image = Archivo.imgBtnAdd2;
            btnDetalleCuenta.Image = Archivo.imgBtnDocuments_open;
            btnGuardar.Image = Archivo.imgBtnFloppy;
            btnCuenta.Image = Archivo.imgBtnRestart;
            btnCancelar.Image = Archivo.imgBtnStop;
            btnSalir.Image = Archivo.imgBtnSalir32;
            btnImprimir.Image = Archivo.imgBtnImprimir32;
            listaFormaPago = NegFormaPago.listaFormasPago();
            habiltarBotones(true, false, false, false, false, false, true, false);
            ultraGroupBox1.Enabled = true;
            ultraTabControl1.Enabled = true;
            ultraTabControl2.Enabled = true;
            //CAMBIOS EDGAR 20210201
            if (NegRubros.ParametroServicios() && Sesion.codDepartamento == 7)//si es cajero solo se mostraran servicos hospitalarios y otros servicios
            {
                cmb_Areas.DataSource = NegPedidos.RecuperarListaServicios().OrderBy(a => a.PEA_NOMBRE).ToList();
                cmb_Areas.ValueMember = "PEA_CODIGO";
                cmb_Areas.DisplayMember = "PEA_NOMBRE";
                cmb_Areas.Text = "TODAS LAS AREAS";
                cmbEstaciones.DataSource = NegPedidos.recuperarListaEstaciones().OrderBy(a => a.PEE_NOMBRE).ToList();
                cmbEstaciones.ValueMember = "PEE_CODIGO";
                cmbEstaciones.DisplayMember = "PEE_NOMBRE";
                cmbEstaciones.SelectedIndex = 0;
            }
            else
            {
                cmb_Areas.DataSource = NegPedidos.recuperarListaAreasTodas().OrderBy(a => a.PEA_NOMBRE).ToList();
                cmb_Areas.ValueMember = "PEA_CODIGO";
                cmb_Areas.DisplayMember = "PEA_NOMBRE";
                cmb_Areas.Text = "TODAS LAS AREAS";
                cmbEstaciones.DataSource = NegPedidos.recuperarListaEstaciones().OrderBy(a => a.PEE_NOMBRE).ToList();
                cmbEstaciones.ValueMember = "PEE_CODIGO";
                cmbEstaciones.DisplayMember = "PEE_NOMBRE";
                cmbEstaciones.SelectedIndex = 0;
            }
            //cmb_Areas.DataSource = NegPedidos.recuperarListaAreasTodas().OrderBy(a => a.PEA_NOMBRE).ToList();
            //cmb_Areas.ValueMember = "PEA_CODIGO";
            //cmb_Areas.DisplayMember = "PEA_NOMBRE";
            //cmb_Areas.SelectedIndex = 0;
            //cmbEstaciones.DataSource = NegPedidos.recuperarListaEstaciones().OrderBy(a => a.PEE_NOMBRE).ToList();
            //cmbEstaciones.ValueMember = "PEE_CODIGO";
            //cmbEstaciones.DisplayMember = "PEE_NOMBRE";
            //cmbEstaciones.SelectedIndex = 0;
            txt_Caja.Text = caja.CAJ_NOMBRE;
            btnSolicitar.Enabled = false;
            ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
            codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
        }
        private void limpiarCampos()
        {
            txt_ApellidoH1.Text = string.Empty;
            txt_Direccion_P.Text = string.Empty;
            txt_Telef_P.Text = string.Empty;
            txt_Habitacion_P.Text = string.Empty;
            txt_Dias_P.Text = string.Empty;
            txt_Ruc.Text = string.Empty;
            txt_Direc_Cliente.Text = string.Empty;
            txt_Cliente.Text = string.Empty;
            txt_telef_Cliente.Text = string.Empty;
            txt_Referido.Text = string.Empty;
            txtEmailFactura.Text = "";
            habiltarBotones(true, false, false, false, false, true, true, false);
            dtDetalleFactura = new DataTable();
            dtFormasPagos = new DataTable();
            gridDetalleFactura.DataSource = dtDetalleFactura;
            //gridFormasPago.DataSource = dtFormasPagos;
            DataTable dtDividefactura = new DataTable();
            dgvDivideFactura.DataSource = dtDividefactura;
            DataTable dtDescuentoFactura = new DataTable();
            dgvDescuento.DataSource = dtDescuentoFactura;
            DataTable dtValores = new DataTable();
            gridRecuperaFP.DataSource = dtValores;
            //gridFormasPago.Rows.Clear();
            txt_SubTotal.Text = "0.00";
            txt_TotalCSIva.Text = "0.00";
            txt_Descuento.Text = "0.00";
            txt_SinIVA.Text = "0.00";
            txt_ConIVA.Text = "0.00";
            txt_IVA.Text = "0.00";
            txt_Total.Text = "0.00";
            btnBuscaClienteSic.Visible = false;
            cbx_FacturaNombre.Text = "";
            VectorCodigoOK = "0";
            dgvDescuento.Enabled = true;
            ultraTabControl1.Enabled = true;
            if (facturaPrefactura == 2)
            {
                label49.Visible = true;
                label50.Visible = true;
            }
            else
            {
                label49.Visible = false;
                label50.Visible = false;
            }
            DesHabilitarBontones2();
            panel2.Visible = false;
        }
        private void habiltarBotones(Boolean nuevo, Boolean guardar, Boolean modificar, Boolean buscar, Boolean imprimir, Boolean cancelar, Boolean salir, Boolean importar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnCuenta.Enabled = modificar;
            btnDetalleCuenta.Enabled = buscar;
            btnImprimir.Enabled = imprimir;
            btnCancelar.Enabled = cancelar;
            btnSalir.Enabled = salir;
            btnImportar.Enabled = importar;
        }
        private void habiltarCampos()
        {
            txt_Ruc.Enabled = false;
            txt_Direc_Cliente.Enabled = false;
            txt_Cliente.Enabled = false;
            txt_telef_Cliente.Enabled = false;
        }
        private void cargarDatosPaciente(PACIENTES pacienteActual)
        {
            ultimaAtencion = NegAtenciones.AtencionID(Convert.ToInt32(txt_Atencion.Text.Trim()));
            DataTable Atenciones = NegAtenciones.atencionesID(Convert.ToInt32(txt_Atencion.Text.Trim()));
            DataTable DatosConvenioAtencion = new DataTable();
            limpiarCampos();
            string NombreFactura = ultimaAtencion.ATE_FACTURA_NOMBRE;
            if (NombreFactura != "PACIENTE")
            {
                txt_ApellidoH1.Text = pacienteActual.PAC_APELLIDO_PATERNO + ' ' + pacienteActual.PAC_APELLIDO_MATERNO + ' ' + pacienteActual.PAC_NOMBRE1 + ' ' + pacienteActual.PAC_NOMBRE2;
                FacturaNombre(NombreFactura);
            }
            else
            {
                if (pacienteActual.PAC_IDENTIFICACION != null)
                    txt_RucPaciente.Text = pacienteActual.PAC_IDENTIFICACION;
                txt_ApellidoH1.Text = pacienteActual.PAC_APELLIDO_PATERNO + ' ' + pacienteActual.PAC_APELLIDO_MATERNO + ' ' + pacienteActual.PAC_NOMBRE1 + ' ' + pacienteActual.PAC_NOMBRE2;
                txt_Telef_P.Text = pacienteActual.PAC_REFERENTE_TELEFONO;
            }
            txt_tipoIngreso.Text = Atenciones.Rows[0][0].ToString();
            cbx_FacturaNombre.SelectedText = NombreFactura;
            codigoAtencion = ultimaAtencion.ATE_CODIGO;
            HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == ultimaAtencion.HABITACIONESReference.EntityKey);
            if (hab != null)
            {
                Sesion.codHabitacion = hab.hab_Codigo;
                txt_Habitacion_P.Text = hab.hab_Numero;
            }
            int codMedicos = Convert.ToInt32(ultimaAtencion.MEDICOSReference.EntityKey.EntityKeyValues[0].Value);
            medico = NegMedicos.recuperarMedico(codMedicos);
            DatosConvenioAtencion = NegAtenciones.CodigoConvenio(ultimaAtencion.ATE_CODIGO);
            CodigoConvenio = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["TE_CODIGO"].ToString());
            CodigoTipoEmpresa = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["CAT_CODIGO"].ToString()); /*CONVENIO*/
            CodigoAseguradora = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["ASE_CODIGO"].ToString()); /* EMPRESA */
            this.lblCategoria.Text = DatosConvenioAtencion.Rows[0]["TE_DESCRIPCION"].ToString();
            this.lblConvenio.Text = DatosConvenioAtencion.Rows[0]["CAT_NOMBRE"].ToString();
            //CALCULO SI EL PACIENTE ESTA HOSPITALIZADO O NO PARA EL DIA DE HOSPITALIZACION PABLO ROCHA 30/05/2014
            DateTime diashospitalizadoalta;
            DateTime diashospitalizadoinicio;
            TimeSpan diastotales;
            int diaHospiAlta = (DateTime.Now.Day);
            int mesHospiAlta = (DateTime.Now.Month);
            int anioHospiAlta = (DateTime.Now.Year);
            int diaHospiIngreso = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_INGRESO).Day);
            int mesHospiIngreso = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_INGRESO).Month);
            int anioHospiIngreso = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_INGRESO).Year);
            if (ultimaAtencion.ESC_CODIGO == 1)
            {
                diashospitalizadoalta = Convert.ToDateTime(diaHospiAlta + "/" + mesHospiAlta + "/" + anioHospiAlta);
                diashospitalizadoinicio = Convert.ToDateTime(diaHospiIngreso + "/" + mesHospiIngreso + "/" + anioHospiIngreso);
                diastotales = diashospitalizadoalta.Subtract(diashospitalizadoinicio);
                txt_Dias_P.Text = Convert.ToString(diastotales.Days);
            }
            else
            {
                diaHospiAlta = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_ALTA).Day);
                mesHospiAlta = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_ALTA).Month);
                anioHospiAlta = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_ALTA).Year);
                diashospitalizadoalta = Convert.ToDateTime(diaHospiAlta + "/" + mesHospiAlta + "/" + anioHospiAlta);
                diashospitalizadoinicio = Convert.ToDateTime(diaHospiIngreso + "/" + mesHospiIngreso + "/" + anioHospiIngreso);
                diastotales = diashospitalizadoalta.Subtract(diashospitalizadoinicio);
                txt_Dias_P.Text = Convert.ToString(diastotales.Days);
            }
            DataTable PacienteReferido = NegDetalleCuenta.ReferidoPaciente(ultimaAtencion.ATE_CODIGO);
            if (txt_Dias_P.Text == "0")
                txt_Dias_P.Text = "1";
            if (PacienteReferido.Rows.Count > 0)
                txt_Referido.Text = PacienteReferido.Rows[0][0].ToString();
            datosPacienteActual = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);
            txt_Direccion_P.Text = datosPacienteActual.DAP_DIRECCION_DOMICILIO;
            cargarDatosFacturaA(pacienteActual);
            FacturaNombre(NombreFactura);
            cargarDatosPagos(ultimaAtencion);
        }
        private void cargarDatosPagos(ATENCIONES ultimaAtencion)
        {
            try
            {
                detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(ultimaAtencion.ATE_CODIGO);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarDatosFacturaA(PACIENTES pacienteActual)
        {
            if (ultimaAtencion.ATE_FACTURA_NOMBRE == "PACIENTE")
            {
                txt_Ruc.Text = pacienteActual.PAC_IDENTIFICACION;
                txt_Direc_Cliente.Text = datosPacienteActual.DAP_DIRECCION_DOMICILIO;
                txt_Cliente.Text = pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO + " " + pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2;
                txt_telef_Cliente.Text = pacienteActual.PAC_REFERENTE_TELEFONO;
                txtEmailFactura.Text = pacienteActual.PAC_EMAIL;
            }
            else
            {
                if (ultimaAtencion.ATE_FACTURA_NOMBRE == "ACOMPAÑANTE" || ultimaAtencion.ATE_FACTURA_NOMBRE == "OTROS")
                {
                    txt_Ruc.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
                    txt_Direc_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION;
                    txt_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                    txt_telef_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO;
                }
                else
                {
                    if (ultimaAtencion.ATE_FACTURA_NOMBRE == "GARANTE" || ultimaAtencion.ATE_FACTURA_NOMBRE == "CONVENIO DE PAGO")
                    {
                        DataTable convenio = new DataTable();
                        convenio = NegDetalleCuenta.ConvenioPago(lblConvenio.Text);

                        DataTable OtroCliente = new DataTable();
                        OtroCliente = NegDetalleCuenta.RecuperaOtroCliente(convenio.Rows[0][0].ToString());
                        foreach (DataRow Item in OtroCliente.AsEnumerable())
                        {
                            txt_Cliente.Text = Item["nomcli"].ToString();
                            if (Item["telcli"].ToString().Any(x => !char.IsNumber(x)))
                            {
                                txt_telef_Cliente.Text = Regex.Replace(Item["telcli"].ToString(), "[^1-9]", "", RegexOptions.None);
                            }
                            else
                                txt_telef_Cliente.Text = Item["telcli"].ToString();
                            txt_Direc_Cliente.Text = Item["dircli"].ToString();
                            txtEmailFactura.Text = Item["email"].ToString();
                        }
                        txt_Ruc.Text = convenio.Rows[0][0].ToString();
                        if (txt_Ruc.Text.Length == 10)
                        {
                            chk_Cedula.Checked = true;
                            chk_Ruc.Checked = false;
                            chk_pasaporte.Checked = false;
                            chbConsumidorFinal.Checked = false;
                        }
                        else if (txt_Ruc.Text.Length == 13)
                        {

                            chk_Cedula.Checked = false;
                            chk_Ruc.Checked = true;
                            chk_pasaporte.Checked = false;
                            chbConsumidorFinal.Checked = false;
                        }
                        else
                        {
                            chk_Cedula.Checked = false;
                            chk_Ruc.Checked = false;
                            chk_pasaporte.Checked = true;
                            chbConsumidorFinal.Checked = false;
                        }
                        //txt_Ruc.Text = ultimaAtencion.ATE_GARANTE_CEDULA;
                        //txt_Direc_Cliente.Text = ultimaAtencion.ATE_GARANTE_DIRECCION;
                        //txt_Cliente.Text = ultimaAtencion.ATE_GARANTE_NOMBRE;
                        //txt_telef_Cliente.Text = ultimaAtencion.ATE_GARANTE_TELEFONO;
                    }
                    else
                    {
                        if (ultimaAtencion.ATE_FACTURA_NOMBRE == "EMPRESA")
                        {
                            int codEmp = (Int16)datosPacienteActual.EMP_CODIGO;
                            empresaPaciente = aseguradoras.FirstOrDefault(e => e.ASE_CODIGO == codEmp);
                            cargarEmpresa(empresaPaciente.ASE_RUC);
                        }
                    }
                }
            }
        }
        private void cargarEmpresa(string numRuc)
        {
            empresaPaciente = NegAseguradoras.RecuperarEmpresa(numRuc);
            if (empresaPaciente != null)
            {
                txt_Ruc.Text = empresaPaciente.ASE_RUC;
                txt_Cliente.Text = empresaPaciente.ASE_NOMBRE;
                txt_Direc_Cliente.Text = empresaPaciente.ASE_CIUDAD;
                txt_telef_Cliente.Text = empresaPaciente.ASE_TELEFONO;
            }
        }

        NegQuirofano Quirofano = new NegQuirofano();
        private void cargarDetalleFactura()
        {
            double subTotal = 0;
            double ConIva = 0;
            double SinIva = 0;
            double ivatotal = 0;
            double totalIVA = 0;
            double descuentoFinal = 0;
            //CARGA CUENTA DE PACIENTE
            TABLA17sri parametroIva = NegParametros.tablaIva();
            ATENCIONES atencion = NegAtenciones.RecuperarAtencionID(ultimaAtencion.ATE_CODIGO);
            if (atencion.ESC_CODIGO == 6)
            {
                DataTable df = NegFactura.FacturaDetalleAgrupado(atencion.ATE_FACTURA_PACIENTE);
                if (df.Rows[0]["porcentajeIva"].ToString() == "12")
                {
                    parametroIva.PORCENTAJE = (decimal?)0.12;
                }
            }
            DataTable lista = new DataTable();
            if (VectorCodigoOK == "0")
            {
                NegFactura.EliminaAuxAgrupa();
                NegFactura.GuardaAuxAgrupa(ultimaAtencion.ATE_CODIGO);
                if (ultimaAtencion.ESC_CODIGO == 6)
                    lista = NegDetalleCuenta.RecuperaCuentaPacinteSPFacturado(ultimaAtencion.ATE_CODIGO);
                else
                    lista = NegDetalleCuenta.RecuperaCuentaPacinteSP(ultimaAtencion.ATE_CODIGO);
                NegFactura.EliminaAuxAgrupa();
            }
            else
            {
                lista = NegDetalleCuenta.RecuperaCuentaPacinteSP(ultimaAtencion.ATE_CODIGO);
                DataTable recuperaAuxAgrupa = new DataTable();
                recuperaAuxAgrupa = NegFactura.RecuperaAuxAgrupa();
                string recuperaAuxAgrupatexto = "CUENTAS AGRUPADAS CON H.Clinica: ";
                for (int i = 0; i < recuperaAuxAgrupa.Rows.Count; i++)
                {
                    string aux = recuperaAuxAgrupa.Rows[i][1].ToString().TrimEnd();
                    recuperaAuxAgrupatexto += aux + ", ";
                }
                recuperaAuxAgrupatexto = recuperaAuxAgrupatexto.TrimEnd();
                recuperaAuxAgrupatexto = recuperaAuxAgrupatexto.TrimEnd(',');
                txtObserva.Visible = true;
                txtObserva.Text = recuperaAuxAgrupatexto;
            }
            if (lista != null)
            {
                dtDetalleFactura = new DataTable();
                gridDetalleFactura.DataSource = dtDetalleFactura;
                dtDetalleFactura.Columns.Add("INDICE", Type.GetType("System.String"));
                dtDetalleFactura.Columns.Add("RUBRO", Type.GetType("System.String"));
                dtDetalleFactura.Columns.Add("SUBTOTAL", Type.GetType("System.String"));
                dtDetalleFactura.Columns.Add("DESCUENTO", Type.GetType("System.String"));
                dtDetalleFactura.Columns.Add("TOTAL SIN IVA", Type.GetType("System.String"));
                dtDetalleFactura.Columns.Add("TOTAL CON IVA", Type.GetType("System.String"));
                dtDetalleFactura.Columns.Add("IVA", Type.GetType("System.String"));
                dtDetalleFactura.Columns.Add("TOTAL", Type.GetType("System.String"));
                List<RUBROS> listaRubros = new List<RUBROS>();
                listaRubros = NegRubros.recuperarRubros();
                RUBROS rubros = new RUBROS();
                int fila = gridDetalleFactura.Rows.Count;
                DataRow drDetalle;
                string copago;
                //CORRECCION DE PROCEDIMIENTO POR EL COPAGO PABLO ROCHA 17-07-2019
                copago = NegFactura.ValidaCopago(Convert.ToString(ultimaAtencion.ATE_CODIGO));
                //for (int p = 0; p < lista.Rows.Count; p++)
                //{
                //    //Validar los precios de convenios Pablo Rocha 14_08-2018                    
                //    if (Convert.ToInt16(ultimaAtencion.ESC_CODIGO) != 6 && copago !="S")
                //        NegFactura.ValidaSeguroConvenio(Convert.ToString(ultimaAtencion.ATE_CODIGO), lista.Rows[p]["PRO_CODIGO_BARRAS"].ToString());
                //}
                for (int i = 0; i < listaRubros.Count; i++)
                {
                    rubros = listaRubros.ElementAt(i);

                    //double valorIva = NegParametros.ParametroIva() / 100;
                    double total = 0;
                    double iva = 0;
                    double totalSinIva = 0;
                    double descuentoTotal = 0;
                    bool accion = false;
                    for (int j = 0; j < lista.Rows.Count; j++)
                    {
                        if (Convert.ToInt16(rubros.RUB_CODIGO) == Convert.ToInt16(lista.Rows[j]["RUB_CODIGO"].ToString()))
                        {
                            descuentoTotal += Convert.ToDouble(lista.Rows[j]["DESCUENTO"].ToString());
                            total += Convert.ToDouble(lista.Rows[j]["CUE_VALOR_UNITARIO"].ToString()) * Convert.ToDouble(lista.Rows[j]["CUE_CANTIDAD"].ToString());
                            if (Convert.ToBoolean(lista.Rows[j]["paga_iva"].ToString()))
                            {
                                //iva += ((Convert.ToDouble(lista.Rows[j]["CUE_VALOR_UNITARIO"].ToString()) * Convert.ToDouble(lista.Rows[j]["CUE_CANTIDAD"].ToString())) - Convert.ToDouble(lista.Rows[j]["DESCUENTO"].ToString())) * valorIva;
                                totalIVA += (Convert.ToDouble(lista.Rows[j]["CUE_VALOR_UNITARIO"].ToString()) * Convert.ToDouble(lista.Rows[j]["CUE_CANTIDAD"].ToString())) - Convert.ToDouble(lista.Rows[j]["DESCUENTO"].ToString());
                                iva = totalIVA * (double)parametroIva.PORCENTAJE;
                            }
                            else
                            {
                                totalSinIva += (Convert.ToDouble(lista.Rows[j]["CUE_VALOR_UNITARIO"].ToString()) * Convert.ToDouble(lista.Rows[j]["CUE_CANTIDAD"].ToString())) - Convert.ToDouble(lista.Rows[j]["DESCUENTO"].ToString());
                            }
                            accion = true;
                        }
                        if (j == lista.Rows.Count - 1 && accion == true)
                        {
                            drDetalle = dtDetalleFactura.NewRow();
                            drDetalle["INDICE"] = rubros.RUB_CODIGO;
                            drDetalle["RUBRO"] = rubros.RUB_NOMBRE;
                            drDetalle["SUBTOTAL"] = String.Format("{0:0.0000}", Math.Abs(total - totalIVA) + totalIVA);
                            drDetalle["DESCUENTO"] = String.Format("{0:0.0000}", descuentoTotal);
                            drDetalle["TOTAL SIN IVA"] = String.Format("{0:0.0000}", totalSinIva);
                            drDetalle["TOTAL CON IVA"] = String.Format("{0:0.0000}", totalIVA);
                            drDetalle["IVA"] = String.Format("{0:0.0000}", iva);
                            drDetalle["TOTAL"] = String.Format("{0:0.0000}", Math.Abs(total - totalIVA) + totalIVA + iva - descuentoTotal);
                            dtDetalleFactura.Rows.Add(drDetalle);
                            accion = false;
                        }
                    }
                    subTotal += total;
                    ConIva += totalIVA;
                    ivatotal = iva;
                    descuentoFinal += descuentoTotal;
                    totalIVA = 0;
                }
                SinIva = subTotal - ConIva;
                if (lblConvenio.Text == "CATEGORIA 0 " || lblConvenio.Text == "PARTICULARES")
                    chbCopago.Visible = false;
                else
                    chbCopago.Visible = true;
                if (chbCopago.Checked == true)
                {
                    drDetalle = dtDetalleFactura.NewRow();
                    drDetalle["INDICE"] = "46";
                    drDetalle["RUBRO"] = "COPAGO";
                    drDetalle["SUBTOTAL"] = String.Format("{0:0.0000}", 0.00);
                    drDetalle["DESCUENTO"] = String.Format("{0:0.0000}", 0.00);
                    drDetalle["TOTAL CON IVA"] = String.Format("{0:0.0000}", 0.00);
                    drDetalle["TOTAL SIN IVA"] = String.Format("{0:0.0000}", 0.00);
                    drDetalle["IVA"] = String.Format("{0:0.0000}", 0.00);
                    drDetalle["TOTAL"] = String.Format("{0:0.0000}", 0.00);
                    dtDetalleFactura.Rows.Add(drDetalle);
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
            bandUno.Columns["SUBTOTAL"].Header.Caption = "SUBTOTAL";
            bandUno.Columns["DESCUENTO"].Header.Caption = "DESCUENTO";
            bandUno.Columns["TOTAL SIN IVA"].Header.Caption = "TOTAL SIN IVA";
            bandUno.Columns["TOTAL CON IVA"].Header.Caption = "TOTAL CON IVA";
            bandUno.Columns["IVA"].Header.Caption = "IVA";
            bandUno.Columns["TOTAL"].Header.Caption = "TOTAL";
            bandUno.Columns["INDICE"].Width = 80;
            bandUno.Columns["RUBRO"].Width = 600;
            bandUno.Columns["SUBTOTAL"].Width = 100;
            bandUno.Columns["DESCUENTO"].Width = 100;
            bandUno.Columns["TOTAL SIN IVA"].Width = 120;
            bandUno.Columns["TOTAL CON IVA"].Width = 120;
            bandUno.Columns["IVA"].Width = 80;
            bandUno.Columns["TOTAL"].Width = 100;
            bandUno.Columns["INDICE"].Hidden = false;
            bandUno.Columns["RUBRO"].Hidden = false;
            bandUno.Columns["SUBTOTAL"].Hidden = false;
            bandUno.Columns["DESCUENTO"].Hidden = false;
            bandUno.Columns["TOTAL SIN IVA"].Hidden = false;
            bandUno.Columns["TOTAL CON IVA"].Hidden = false;
            bandUno.Columns["IVA"].Hidden = false;
            bandUno.Columns["SUBTOTAL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["DESCUENTO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["TOTAL SIN IVA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["TOTAL CON IVA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["IVA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["TOTAL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            //Calculo los valores para factura       
            subTotal = 0;
            SinIva = 0;
            ConIva = 0;
            ivatotal = 0;
            descuentoFinal = 0;
            for (int i = 0; i < gridDetalleFactura.Rows.Count; i++)
            {
                subTotal += Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["SUBTOTAL"].Value.ToString());
                SinIva += Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString());
                ConIva += Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString());
                ivatotal += Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["IVA"].Value.ToString());
                descuentoFinal += Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value.ToString());
            }
            txt_SubTotal.Text = String.Format("{0:0.00}", SignificantTruncate(subTotal, 2));
            txt_Descuento.Text = String.Format("{0:0.00}", SignificantTruncate(descuentoFinal, 2));
            txt_SinIVA.Text = String.Format("{0:0.00}", SignificantTruncate(SinIva, 2));
            txt_ConIVA.Text = String.Format("{0:0.00}", SignificantTruncate(ConIva, 2));
            txt_IVA.Text = String.Format("{0:0.00}", Math.Round(ivatotal, 2));
            double ivaAux = Convert.ToDouble(txt_IVA.Text);
            double conIva = Convert.ToDouble(txt_ConIVA.Text);
            double sinIva = Convert.ToDouble(txt_SinIVA.Text);
            txt_Total.Text = String.Format("{0:0.00}", SignificantTruncate(conIva + sinIva + ivaAux, 2));
            lblConIva.Text = String.Format("{0:0.00}", Convert.ToString(Convert.ToDouble(txt_ConIVA.Text) + Convert.ToDouble(txt_IVA.Text)));
            //DETERMINA SI LA CUENTA ESTA FACTURADA O SE VA FACTURAR
            if (Convert.ToInt16(ultimaAtencion.ESC_CODIGO) == 6)
            {
                if (gridFormasPago.Rows.Count >= 2)
                {
                    gridFormasPago.Rows.Clear();

                }
                DataTable consultafacturasic = new DataTable();
                consultafacturasic = NegFormaPago.RecuperaDescuento(ultimaAtencion.ATE_FACTURA_PACIENTE, CodigoAseguradora);
                habiltarBotones(false, false, false, true, true, true, true, false);
                string facNum = Convert.ToString(consultafacturasic.Rows[0][1]);
                txt_Factura_Cod1.Text = "";
                txt_Factura_Cod3.Text = "";
                txt_Factura_Cod1.Text = facNum.Substring(0, 3);
                txt_Factura_Cod3.Text = facNum.Substring(3, 9);
                dtpFechaFacturacion.Text = Convert.ToString(consultafacturasic.Rows[0][7]);
                txt_Ruc.Text = Convert.ToString(consultafacturasic.Rows[0][3]);
                txt_Cliente.Text = Convert.ToString(consultafacturasic.Rows[0][4]);
                txt_telef_Cliente.Text = Convert.ToString(consultafacturasic.Rows[0][5]);
                txt_Direc_Cliente.Text = Convert.ToString(consultafacturasic.Rows[0][6]);
                lblObserva.Visible = true;
                txtObserva.Visible = true;
                txtObserva.Text = "";
                txtObserva.Text = Convert.ToString(consultafacturasic.Rows[0][2]);
                txt_Descuento.Text = String.Format("{0:0.00}", consultafacturasic.Rows[0][0]);
                //CARGO LOS DATOS Y BLOQUEO CONTROLES PABO ROCHA 31/05/2018
                DataTable clienteFacturado = new DataTable();
                clienteFacturado = NegFormaPago.ClienteFacturado(ultimaAtencion.ATE_CODIGO);
                chk_Ruc.Checked = false;
                chk_Cedula.Checked = false;
                chk_pasaporte.Checked = false;
                chbConsumidorFinal.Checked = false;
                if (clienteFacturado.Rows[0][0].ToString() == "04")
                    chk_Ruc.Checked = true;
                else if (clienteFacturado.Rows[0][0].ToString() == "05")
                    chk_Cedula.Checked = true;
                else if (clienteFacturado.Rows[0][0].ToString() == "06")
                    chk_pasaporte.Checked = true;
                else
                    chbConsumidorFinal.Checked = true;
                DataTable descuentosClienteFacturado = new DataTable();
                descuentosClienteFacturado = NegFormaPago.DescuentoClienteFacturado(ultimaAtencion.ATE_CODIGO);
                if (descuentosClienteFacturado.Rows.Count > 0)
                {
                    ultraTabControl2.Tabs["descuento"].Visible = true;
                    foreach (DataRow Fila in descuentosClienteFacturado.Rows)
                    {
                        ultraGrid1.Visible = true;
                        ultraGrid1.DataSource = descuentosClienteFacturado;
                        ultraGrid1.DataBind();
                    }
                }
                ultraGrid1.Enabled = true;
                txt_Ruc.Text = clienteFacturado.Rows[0][1].ToString();
                txt_Cliente.Text = clienteFacturado.Rows[0][2].ToString();
                dtpFechaFacturacion.Text = clienteFacturado.Rows[0][3].ToString();
                txt_telef_Cliente.Text = clienteFacturado.Rows[0][4].ToString();
                txtEmailFactura.Text = clienteFacturado.Rows[0][5].ToString();
                txt_Direc_Cliente.Text = clienteFacturado.Rows[0][6].ToString();
                ultraTabControl1.Tabs["datosFactura"].Enabled = true;
                ultraTabControl1.Tabs["datosPaciente"].Enabled = true;
                ultraTabControl2.Tabs["detalle"].Enabled = true;
                dgvDescuento.Enabled = false;
                chbDescuento.Enabled = false;
                gridRecuperaFP.Enabled = false;
                txtObserva.Enabled = false;
                facturaElectronicaToolStripMenuItem.Enabled = true;
            }
            else
            {
                // Lleno el grid con las formas de pago
                if (gridFormasPago.Rows.Count >= 2)
                {
                    gridFormasPago.Rows.Clear();
                    DataGridViewRow dt = new DataGridViewRow();
                    dt.CreateCells(gridFormasPago);
                    dt.Cells[0].Value = "6" /*formaPago.FOR_CODIGO*/;
                    dt.Cells[1].Value = "EFECTIVO"/*formaPago.FOR_DESCRIPCION*/;
                    dt.Cells[2].Value = this.txt_Total.Text;
                    dt.Cells[3].Value = DateTime.Today.AddDays(1);
                    dt.Cells[5].Value = "0";
                    dt.Cells[9].Value = txt_Cliente.Text;
                    gridFormasPago.Rows.Add(dt);
                }
                else
                {
                    DataGridViewRow dt = new DataGridViewRow();
                    dt.CreateCells(gridFormasPago);
                    dt.Cells[0].Value = "6" /*formaPago.FOR_CODIGO*/;
                    dt.Cells[1].Value = "EFECTIVO"/*formaPago.FOR_DESCRIPCION*/;
                    dt.Cells[2].Value = this.txt_Total.Text;
                    dt.Cells[3].Value = DateTime.Today.AddDays(1);
                    dt.Cells[5].Value = "0";
                    dt.Cells[9].Value = txt_Cliente.Text;
                    gridFormasPago.Rows.Add(dt);

                    gridFormasPago.Rows.Clear();
                    dt = new DataGridViewRow();
                    dt.CreateCells(gridFormasPago);
                    dt.Cells[0].Value = "6" /*formaPago.FOR_CODIGO*/;
                    dt.Cells[1].Value = "EFECTIVO"/*formaPago.FOR_DESCRIPCION*/;
                    dt.Cells[2].Value = this.txt_Total.Text;
                    dt.Cells[3].Value = DateTime.Today.AddDays(1);
                    dt.Cells[5].Value = "0";
                    dt.Cells[9].Value = txt_Cliente.Text;
                    gridFormasPago.Rows.Add(dt);


                    //
                }
            }
            DataTable anticipos = new DataTable();
            NegAtenciones atenciones = new NegAtenciones(); //Edgar 2020-12-03
            string estado = atenciones.EstadoCuenta(Convert.ToString(codigoAtencion));
            anticipos = NegFactura.AnticiposCliente(txt_Atencion.Text);
            if (anticipos.Rows.Count > 0 && estado != "6")
            {
                double totalanticipos = 0;
                for (int i = 0; i < anticipos.Rows.Count; i++)
                {
                    totalanticipos += Convert.ToDouble(anticipos.Rows[i][1].ToString());
                }
                panel2.Visible = true;
                txtAnticipos.Text = totalanticipos.ToString();
                txtSaldo.Text = Convert.ToString((Convert.ToDouble(txt_Total.Text) - totalanticipos));
            }
        }
        private void cargarTotales(int fila)
        {
            try
            {
                if (gridFormasPago.Rows.Count == 2 && gridFormasPago.CurrentRow.Index == 0 && gridFormasPago.Rows[fila].Cells[0].Value.ToString() != "15")
                    gridFormasPago.Rows[fila].Cells[2].Value = String.Format("{0:0.00}", txt_Total.Text.Trim());
                else
                {
                    //gridFormasPago.Refresh();
                    Double total = Convert.ToDouble(txt_Total.Text.Trim());
                    double valor = 0.00;
                    int cunt = 0;
                    int validaindicador = 0;
                    if (gridFormasPago.Rows[fila].Cells[0].Value.ToString() == "15")
                    {
                        frmAnticiposSic form1 = new frmAnticiposSic(txt_Atencion.Text);
                        if (Convert.ToString(gridFormasPago.Rows[fila].Cells[0].Value) == "15")
                        {
                            if (form1.validador == 0)
                            {
                                form1.ShowDialog();
                                bool validaAnticipos = true;
                                for (int i = 0; i < gridFormasPago.Rows.Count; i++)
                                {
                                    if (gridFormasPago.Rows[i].Cells[7].Value != null)
                                        if (gridFormasPago.Rows[i].Cells[7].Value.ToString() == form1.cod_anticipo)
                                        {
                                            validaAnticipos = false;
                                            break;
                                        }
                                }
                                if (form1.cod_anticipo != null && validaAnticipos)
                                {
                                    gridFormasPago.Rows[fila].Cells[2].Value = form1.valor_anticipo;
                                    gridFormasPago.Rows[fila].Cells[10].Value = form1.cod_anticipo;
                                    gridFormasPago.Rows[fila].Cells[7].Value = form1.cod_anticipo;
                                    gridFormasPago.Rows[fila].Cells[9].Value = "N-D";
                                    gridFormasPago.Rows[fila].Cells[5].Value = 0;
                                    gridFormasPago.Rows[fila].Cells[4].Value = form1.nombre_paciente;
                                    gridFormasPago.Rows[fila].Cells[6].Value = form1.cod_anticipo;
                                    gridFormasPago.Rows[fila].Cells[8].Value = form1.cod_anticipo;
                                    gridFormasPago.Rows[fila].Cells[1].Value = form1.nombre_paciente;
                                    chbConsumidorFinal.Enabled = false;
                                    //this.gridFormasPago.Rows.RemoveAt(fila + 1);
                                }
                                else
                                {
                                    MessageBox.Show("Anticipo ya fue seleccionado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    chbConsumidorFinal.Enabled = true;
                                    this.gridFormasPago.Rows.RemoveAt(fila);
                                }
                            }
                            else
                            {

                                gridFormasPago.Rows[fila].Cells[1].Value = "EFECTIVO";
                                gridFormasPago.Rows[fila].Cells[0].Value = 6;

                            }
                        }
                        for (int i = 1; i < gridFormasPago.Rows.Count - 1; i++)
                        {
                            if (Convert.ToString(gridFormasPago.Rows[i - 1].Cells[0].Value.ToString()) == "15")
                                validaindicador = validaindicador + 1;
                        }
                        int aux = 0;
                        for (int i = 0; i < gridFormasPago.Rows.Count - 1; i++)
                        {
                            if (gridFormasPago.Rows[i].Cells[0].Value.ToString() == "")
                            {
                                this.gridFormasPago.Rows.RemoveAt(i);
                            }

                        }
                        if (validaindicador != 1 && validaindicador > 1)
                        {
                            for (int i = 0; i < gridFormasPago.Rows.Count - 1; i++)
                            {
                                if (Convert.ToString(gridFormasPago.Rows[i].Cells[10].Value.ToString()) != "")
                                    if (Convert.ToString(gridFormasPago.Rows[i].Cells[10].Value.ToString()) == Convert.ToString(form1.cod_anticipo))
                                    {
                                        aux = aux + 1;
                                        if (aux > 1)
                                        {
                                            this.gridFormasPago.Rows.RemoveAt(fila);
                                            MessageBox.Show("ANTICIPO YA SELECCIONADO", "HIS3000");
                                            return;
                                        }
                                    }
                            }
                        }
                    }
                    int vacios = 0;
                    for (int i = 0; i < gridFormasPago.Rows.Count; i++)
                    {
                        string nombreConv = Convert.ToString(gridFormasPago.Rows[i].Cells[0].Value);
                        if (nombreConv != "")
                        {
                            vacios++;
                            if (Convert.ToString(gridFormasPago.Rows[i].Cells[2].Value) != "")
                                valor = valor + Convert.ToDouble(String.Format("{0:0.00}", gridFormasPago.Rows[i].Cells[2].Value));

                        }
                        else
                        {
                            if (gridFormasPago.Rows.Count - vacios > 1)
                                this.gridFormasPago.Rows.RemoveAt(cunt - 1);
                        }
                        cunt = i;
                    }
                    if (gridFormasPago.CurrentCell.ColumnIndex == 0)
                    {
                        if (total - valor < 0.00)
                        {
                            if (gridFormasPago.Rows.Count == 3)
                            {
                                MessageBox.Show("TOTAL DE PAGO COMPLETO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //this.gridFormasPago.Rows.RemoveAt(cunt - 1);
                            }
                            else
                            {
                                //this.gridFormasPago.Rows.RemoveAt(cunt - 1);
                                //gridFormasPago.Rows[fila].Cells[2].Value = "0.00";
                                MessageBox.Show("FORMAS DE PAGO COMPLETAS, NO SE PUEDE EXCEDER AL TOTAL", "HIS3000");
                            }
                        }
                        else
                        {
                            if (Convert.ToString(gridFormasPago.Rows[fila].Cells[0].Value) != "15" && Convert.ToString(gridFormasPago.Rows[fila].Cells[0].Value) != "")
                            {
                                total -= valor;
                                gridFormasPago.Rows[fila].Cells[2].Value = String.Format("{0:0.00}", total);
                                gridFormasPago.Rows[fila].Cells[10].Value = "0";
                            }
                        }
                        label39.Text = (total).ToString();
                    }
                    else if (gridFormasPago.CurrentCell.ColumnIndex == 5)
                    {
                        if (total - valor <= 0.00)
                        {
                            MessageBox.Show("FORMAS DE PAGO COMPLETAS, NO SE PUEDE EXEDER AL TOTAL", "HIS3000");
                        }
                    }
                }
            }
            catch (Exception err)
            {
                this.gridFormasPago.Rows.RemoveAt(fila + 1);//MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pagosFactura()
        {
            try
            {
                FORMA_PAGO formaPago = new FORMA_PAGO();
                for (int i = 0; i < gridFormasPago.Rows.Count; i++)
                {
                    if (Convert.ToString(gridFormasPago.Rows[i].Cells[0].Value) != "" && Convert.ToString(gridFormasPago.Rows[i].Cells[2].Value) != "")
                    {
                        formaPago = NegFormaPago.RecuperaFormaPagoID(Convert.ToInt16(gridFormasPago.Rows[i].Cells[0].Value));
                        facturaFormaPago = new FACTURA_FORMA_PAGO();
                        facturaFormaPago.COD_FAC_PAGO = NegFactura.recuperaMaximoFacturaPago() + 1;
                        facturaFormaPago.FACTURAReference.EntityKey = nuevaFactura.EntityKey;
                        facturaFormaPago.FORMA_PAGOReference.EntityKey = formaPago.EntityKey;
                        facturaFormaPago.FORM_PAGO = " ";
                        facturaFormaPago.FORM_DESCRIPCION = Convert.ToString(gridFormasPago.Rows[i].Cells[1].Value);
                        facturaFormaPago.FORM_MONTO = Convert.ToDecimal(gridFormasPago.Rows[i].Cells[2].Value);
                        facturaFormaPago.FORM_PORCENTAJE = 0;
                        facturaFormaPago.FORM_CONTADO = 1;
                        facturaFormaPago.FORM_CREDITO = 1;
                        facturaFormaPago.FORM_VENCIMIENTO = Convert.ToDateTime(gridFormasPago.Rows[i].Cells[3].Value);
                        facturaFormaPago.FORM_BANCO = Convert.ToString(gridFormasPago.Rows[i].Cells[4].Value);
                        facturaFormaPago.FORM_TARJETA_CRE = Convert.ToString(gridFormasPago.Rows[i].Cells[5].Value);
                        facturaFormaPago.FORM_CHEQUE = Convert.ToString(gridFormasPago.Rows[i].Cells[7].Value);
                        facturaFormaPago.FORM_NOM_DUE = Convert.ToString(gridFormasPago.Rows[i].Cells[9].Value);
                        facturaFormaPago.FORM_AUTORIZACION = Convert.ToString(gridFormasPago.Rows[i].Cells[8].Value);
                        facturaFormaPago.FORM_ESTADO = 1;
                        listaFacturaPagos.Add(facturaFormaPago);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos Detalle Factura: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos Detalle Factura: \n" + e.InnerException);
            }
        }

        public void cargarPaciente(string historia)
        {
            try
            {
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static double SignificantTruncate(double num, int significantDigits)
        {
            double y = Math.Pow(10, significantDigits);
            return Math.Round(num * y) / y;
        }

        public bool ValidaAnticipos()
        {
            DataTable anticipos = new DataTable();
            for (int i = 1; i < gridFormasPago.Rows.Count; i++)
            {
                if (gridFormasPago.Rows[i - 1].Cells[0].Value.ToString() == "15")
                {
                    anticipos = NegFactura.ValidaAnticipos(Convert.ToInt64(gridFormasPago.Rows[i - 1].Cells[10].Value.ToString()));
                    break;
                }
            }
            for (int i = 0; i < anticipos.Rows.Count; i++)
            {
                if (gridFormasPago.Rows[i].Cells[0].Value.ToString() == "15")
                {
                    if (Convert.ToDecimal(anticipos.Rows[i][0].ToString()) < Convert.ToDecimal(gridFormasPago.Rows[i].Cells[2].Value.ToString()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void guardarFactura()
        {
            try
            {
                if (ValidaAnticipos())
                {
                    if (!validarFormulario()) // Valida el formulario para que no falta ningun dato principal/Giovanny Tapia / 22/11/2012
                    {
                        double sumatotal = 0.00;
                        for (Int16 i = 0; i < gridFormasPago.Rows.Count; i++)
                        {
                            if (gridFormasPago.Rows[i].Cells[0].Value != null)
                            {
                                sumatotal += Convert.ToDouble(gridFormasPago.Rows[i].Cells[2].Value);
                                His.Honorarios.Datos.Atencion obj_atencion = new His.Honorarios.Datos.Atencion();
                                DataTable banco = new DataTable();
                                banco = obj_atencion.Banco(Convert.ToInt64((gridFormasPago.Rows[i].Cells[0].Value)));
                                if (banco.Rows[0][0].ToString() == "True")
                                {
                                    if (gridFormasPago.Rows[i].Cells[4].Value == null)
                                    {
                                        MessageBox.Show("Por favor ingrese el Banco.");
                                        return;
                                    }
                                    if (gridFormasPago.Rows[i].Cells[5].Value.ToString() == "")
                                    {
                                        MessageBox.Show("Por favor ingrese el plazo de diferido.");
                                        return;
                                    }
                                    if (gridFormasPago.Rows[i].Cells[6].Value == null)
                                    {
                                        MessageBox.Show("Por favor ingrese los datos de la Cuenta/Lote.");
                                        return;
                                    }
                                    if (gridFormasPago.Rows[i].Cells[7].Value == null)
                                    {
                                        MessageBox.Show("Por favor ingrese los datos del cheque.");
                                        return;
                                    }
                                    if (gridFormasPago.Rows[i].Cells[8].Value == null)
                                    {
                                        MessageBox.Show("Por favor ingrese la autorización.");
                                        return;
                                    }
                                    if (gridFormasPago.Rows[i].Cells[9].Value == null)
                                    {
                                        MessageBox.Show("Por favor ingrese los datos del dueño del documento.");
                                        return;
                                    }
                                }
                            }
                        }
                        DialogResult resultado;
                        sumatotal = SignificantTruncate(sumatotal, 2);
                        double sumatotal2 = Convert.ToDouble(txt_Total.Text);
                        sumatotal2 = SignificantTruncate(sumatotal2, 2);
                        if (sumatotal == sumatotal2)
                        {
                            string formaPago = "";
                            for (int i = 1; i < gridFormasPago.Rows.Count; i++)
                            {
                                formaPago += i + ".- " + gridFormasPago.Rows[i - 1].Cells[1].Value.ToString() + ".\n\r";
                            }

                            resultado = MessageBox.Show("Desea Generar la factura con las siguientes FORMAS DE PAGO? \r\n" + formaPago, "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (resultado == DialogResult.Yes)
                            {
                                DataTable verificafactura = new DataTable();
                                DataTable facturaduplicada = new DataTable();
                                verificafactura = NegFactura.VerificaFactura2(Convert.ToInt64(ultimaAtencion.ATE_CODIGO));
                                facturaduplicada = NegFactura.FacturaDuplicada(txt_Factura_Cod1.Text + txt_Factura_Cod3.Text);
                                string factura = null;
                                string esc_codigo = verificafactura.Rows[0]["esc_codigo"].ToString();
                                if (facturaduplicada.Rows.Count == 0)
                                {
                                    if (factura == null || factura == "")
                                    {
                                        if (esc_codigo == "2")
                                        {
                                            if (accionPaciente == false)
                                            {
                                                //MODIFICO LOS VALORES EN CUENTA PACIENTE PARA DIVIDIR FACTURA PABLO ROCHA 03-12-2018
                                                if (chbCopago.Checked == true)
                                                {
                                                    for (int i = 0; i < dgvDivideFactura.Rows.Count; i++)
                                                    {
                                                        //REALIZO AUTOMATIZACION DE COPAGO PARA SEGUROS PABLO ROCHA 12-12-2018
                                                        if (chbCopago.Checked == true && dgvDivideFactura.Rows[i].Cells[0].Value.ToString() == "46")
                                                        {
                                                            if (dgvDivideFactura.Rows[i].Cells[3].Value.ToString() != "0.00" && dgvDivideFactura.Rows[i].Cells[4].Value.ToString() != "0.00")
                                                            {
                                                                NegCuentasPacientes.CopagoFactura1(ultimaAtencion.ATE_CODIGO, Convert.ToDouble(dgvDivideFactura.Rows[i].Cells[4].Value.ToString()), dgvDivideFactura.Rows[i].Cells[1].Value.ToString() + " SIN IVA", 0.00, Convert.ToInt16(dgvDivideFactura.Rows[i].Cells[0].Value.ToString()));
                                                                NegCuentasPacientes.CopagoFactura1(ultimaAtencion.ATE_CODIGO, Convert.ToDouble(dgvDivideFactura.Rows[i].Cells[3].Value.ToString()), dgvDivideFactura.Rows[i].Cells[1].Value.ToString() + " CON IVA", Convert.ToDouble(dgvDivideFactura.Rows[i].Cells[2].Value.ToString()), Convert.ToInt16(dgvDivideFactura.Rows[i].Cells[0].Value.ToString()));
                                                            }
                                                            else if (dgvDivideFactura.Rows[i].Cells[3].Value.ToString() != "0.00")
                                                            {
                                                                NegCuentasPacientes.CopagoFactura1(ultimaAtencion.ATE_CODIGO, Convert.ToDouble(dgvDivideFactura.Rows[i].Cells[3].Value.ToString()), dgvDivideFactura.Rows[i].Cells[1].Value.ToString(), Convert.ToDouble(dgvDivideFactura.Rows[i].Cells[2].Value.ToString()), Convert.ToInt16(dgvDivideFactura.Rows[i].Cells[0].Value.ToString()));
                                                            }
                                                            else
                                                                NegCuentasPacientes.CopagoFactura1(ultimaAtencion.ATE_CODIGO, Convert.ToDouble(dgvDivideFactura.Rows[i].Cells[4].Value.ToString()), dgvDivideFactura.Rows[i].Cells[1].Value.ToString(), 0.00, Convert.ToInt16(dgvDivideFactura.Rows[i].Cells[0].Value.ToString()));

                                                        }
                                                        if (dgvDivideFactura.Rows[i].Cells[5].Value.ToString() == "S")
                                                        {
                                                            List<PRODUCTOCOPAGO> lista = new List<PRODUCTOCOPAGO>();
                                                            List<PRODUCTOCOPAGO> LISTA2 = new List<PRODUCTOCOPAGO>();
                                                            lista = NegCuentasPacientes.DivideFactura1(ultimaAtencion.ATE_CODIGO, Convert.ToInt16(dgvDivideFactura.Rows[i].Cells[0].Value.ToString()));
                                                            foreach (var inspector in lista)
                                                            {
                                                                inspector.CUE_VALOR_UNITARIO -= (inspector.CUE_VALOR_UNITARIO * Convert.ToDecimal(dgvDescuento.Rows[i].Cells[5].Value)) / 100;
                                                                inspector.CUE_VALOR = inspector.CUE_VALOR_UNITARIO * inspector.CUE_CANTIDAD;
                                                                if (inspector.paga_iva)
                                                                    inspector.CUE_IVA = (inspector.CUE_VALOR * inspector.iva) / 100;
                                                                LISTA2.Add(inspector);
                                                            }
                                                            NegCuentasPacientes.ActualizaProductos(LISTA2, ultimaAtencion.ATE_CODIGO);
                                                        }
                                                    }
                                                    for (int i = 0; i < dgvDivideFactura.Rows.Count; i++)
                                                    {
                                                        if (dgvDivideFactura.Rows[i].Cells[5].Value.ToString() == "S")
                                                        {
                                                            NegCuentasPacientes.DivideFactura2(ultimaAtencion.ATE_CODIGO);
                                                            DataGridViewRow row = dgvDivideFactura.Rows[dgvDivideFactura.Rows.Count - 1];
                                                            DataGridViewCheckBoxCell cellSelection = row.Cells["noFactura"] as DataGridViewCheckBoxCell;


                                                            if (chbCopago.Checked == true && Convert.ToBoolean(cellSelection.Value))
                                                                gridDetalleFactura.Rows[gridDetalleFactura.Rows.Count - 1].Delete();
                                                            break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 0; i < dgvDivideFactura.Rows.Count; i++)
                                                    {
                                                        if (dgvDivideFactura.Rows[i].Cells[5].Value.ToString() == "S")
                                                        {
                                                            List<PRODUCTOCOPAGO> lista = new List<PRODUCTOCOPAGO>();
                                                            List<PRODUCTOCOPAGO> LISTA2 = new List<PRODUCTOCOPAGO>();
                                                            lista = NegCuentasPacientes.DivideFactura1(ultimaAtencion.ATE_CODIGO, Convert.ToInt16(dgvDivideFactura.Rows[i].Cells[0].Value.ToString()));
                                                            foreach (var inspector in lista)
                                                            {
                                                                //inspector.CUE_VALOR_UNITARIO -= (inspector.CUE_VALOR_UNITARIO * Convert.ToDecimal(dgvDescuento.Rows[i].Cells[5].Value)) / 100;
                                                                inspector.CUE_VALOR = inspector.CUE_VALOR_UNITARIO * inspector.CUE_CANTIDAD;
                                                                if (inspector.paga_iva)
                                                                    inspector.CUE_IVA = (inspector.CUE_VALOR * inspector.iva) / 100;
                                                                LISTA2.Add(inspector);
                                                            }
                                                            NegCuentasPacientes.ActualizaProductos(LISTA2, ultimaAtencion.ATE_CODIGO);
                                                        }
                                                    }
                                                    //for (int i = 0; i < dgvDivideFactura.Rows.Count; i++)
                                                    //{
                                                    //    if (dgvDivideFactura.Rows[i].Cells[5].Value.ToString() == "S")
                                                    //    {
                                                    //        NegCuentasPacientes.DivideFactura3(ultimaAtencion.ATE_CODIGO, Convert.ToInt16(dgvDivideFactura.Rows[i].Cells[0].Value.ToString()));
                                                    //    }
                                                    //}
                                                    bool divideFac = true;
                                                    for (int i = 0; i < dgvDivideFactura.Rows.Count; i++)
                                                    {
                                                        if (dgvDivideFactura.Rows[i].Cells[5].Value.ToString() == "S")
                                                        {
                                                            if (divideFac)
                                                            {
                                                                NegCuentasPacientes.DivideFactura4(Convert.ToInt32(txt_Atencion.Text)); //DIVIDE EN DOS ATENCIONE SEGUN CAMPO DIVIDE CTA'
                                                                divideFac = false;
                                                            }
                                                            DataGridViewRow row = dgvDivideFactura.Rows[dgvDivideFactura.Rows.Count - 1];
                                                            DataGridViewCheckBoxCell cellSelection = row.Cells["noFactura"] as DataGridViewCheckBoxCell;
                                                            if (chbCopago.Checked == true && Convert.ToBoolean(cellSelection.Value))
                                                            {
                                                                gridDetalleFactura.Rows[gridDetalleFactura.Rows.Count - 1].Delete();
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                //Ingreso Nueva Factura   
                                                codigoFactura = NegFactura.recuperaMaximoFactura(); // Genera un secuencial para la factura His3000/Giovanny Tapia / 22/11/2012
                                                codigoFactura++;
                                                nuevaFactura.ATENCIONESReference.EntityKey = pacienteFactura.EntityKey;
                                                agregarDatosFactura(codigoFactura);
                                                NegFactura.crearFactura(nuevaFactura); // Guarda Detalle fatura His3000 ActualizaCajas(txt_Caja.Text)/Giovanny Tapia / 22/11/2012;
                                                agregarDatosDetalleFactura(nuevaFactura);
                                                bool ParaCodigo = true;
                                                // Guarda la factura en la BD Sic3000/Giovanny Tapia / 22/11/2012
                                                if (facturaPrefactura == 1)
                                                {
                                                    bool TipoDocumento = true;
                                                    DataTable parametro = new DataTable();
                                                    parametro = NegFactura.RecuperaParametros();
                                                    GuardaFacturaSic3000(ref ParaCodigo, Convert.ToInt16(Caja()));
                                                    if (!facturado)
                                                    {
                                                        return;
                                                    }
                                                    if (parametro.Rows[2][2].ToString() == "True")
                                                        GeneraFacturaElectronica(ref TipoDocumento);
                                                    //NegFactura.IncrementaNumeroFactura(Convert.ToInt16(Caja()));
                                                }
                                                CuentaCancelada();
                                                int aux = Sesion.codUsuario;
                                                ActualizaEstadoCuenta(); // Actualiza la cuenta para que no aparezca en el listado de cuentas por facturar / Giovanny Tapia / 16/01/2013
                                                //GuardaDatosAdicionales(); // Guarda los datos del paciente al que se le hizo la factura /Giovanny Tapia / 22/11/2012
                                                //CAMBIA ESTADOS DE ANTICIPOS 01-08-2014
                                                // CAMBIO HR 10092019-----------------
                                                if (facturaPrefactura == 1)
                                                    ActualizaCajas(txt_Caja.Text); // TERMINA EL PROCESO Y ACTUALIZA FACTURA
                                                else if (facturaPrefactura == 2)
                                                    ActualizaCajasPrefacturas(txt_Caja.Text);
                                                for (int i = 1; i < gridFormasPago.Rows.Count; i++)
                                                {
                                                    if (gridFormasPago.Rows[i - 1].Cells[0].Value.ToString() == "15")
                                                    {
                                                        NegFactura.EstadoAnticipos(Convert.ToInt16(gridFormasPago.Rows[i - 1].Cells[10].Value.ToString()), txt_Factura_Cod1.Text + txt_Factura_Cod3.Text, Convert.ToDecimal(gridFormasPago.Rows[i - 1].Cells[2].Value.ToString()));
                                                    }
                                                }
                                                MessageBox.Show("Datos Almacenados Correctamente ........", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);



                                                //GRABA TIPO DE IDENTIFICADOR Y EMAIL PABLO ROCHA 10-05-2015
                                                //try
                                                //{
                                                //    GrabarIdentificador();
                                                //}
                                                //catch (Exception ex)
                                                //{
                                                //    MessageBox.Show("IDENTIFICADOR NO GUARDADO " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                //}
                                                //GRABA DESCUENTOS DE FORMA ADECUADA PABLO ROCHA 08/05/2018
                                                //GrabaDescuentos();
                                                //GENERACION FACTURA ELECTRONICA PABLO ROCHA 03/09/2014 
                                                //bool TipoDocumento = true;
                                                //if (facturaPrefactura == 1)
                                                //{
                                                //    DataTable parametro = new DataTable();
                                                //    parametro = NegFactura.RecuperaParametros();
                                                //    if (parametro.Rows[2][2].ToString() == "True")
                                                //        GeneraFacturaElectronica(ref TipoDocumento);
                                                //    NegFactura.IncrementaNumeroFactura(Convert.ToInt16(Caja()));
                                                //}
                                                habiltarBotones(true, false, true, true, true, true, true, false);
                                                //NegFactura.ActualizaEstadoHabitacion(Convert.ToInt64(ultimaAtencion.ATE_CODIGO));
                                                if (facturaPrefactura == 2)
                                                    ActualizaEstadoCuenta2(3, Sesion.nomUsuario);
                                                //ACTUALIZO TODOS LOS RUBROS EN LAS CUENTAS
                                                if (VectorCodigoOK != "0")
                                                {
                                                    DataTable recuperaAuxAgrupa = new DataTable();
                                                    recuperaAuxAgrupa = NegFactura.RecuperaAuxAgrupa();
                                                    for (int i = 0; i < recuperaAuxAgrupa.Rows.Count; i++)
                                                    {
                                                        NegFactura.AgrupaCuentas(Convert.ToInt64(ultimaAtencion.ATE_CODIGO), Convert.ToInt64(recuperaAuxAgrupa.Rows[i][0].ToString()));
                                                        //ActualizaEstadoCuenta2(6);
                                                    }
                                                }
                                                //SE AUMATIZA EL REGISTRO DE LIQUIDACION CON HONORARIOS CONSULTA EXTERNA
                                                int tIngreso = NegTipoIngreso.RecuperarporAtencion(ultimaAtencion.ATE_CODIGO);
                                                //if (tIngreso == 4) //es consulta externa// se coementa por que se va a trabajar por codigo de producto mas no por tipo de ingreso y honorario // Mario Valencia // 21-12-2023
                                                LiquidacionHonorarios();
                                                //facturaToolStripMenuItem.PerformClick();
                                                //facturaimprime.PerformClick();
                                                if (facturaPrefactura == 1)
                                                    facturaimprime_Click(facturaimprime, EventArgs.Empty);
                                                NegValidaciones.alzheimer();
                                                //facturaimprime.PerformClick();
                                                this.Dispose();
                                                id = 0;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Cuenta De Paciente Sin Dar El Alta Verifique En Habitaciones Y Vuelva A Intentarlo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            return;
                                        }
                                    }
                                    else
                                        MessageBox.Show("Cuenta, Ya Facturada Revise Doble Pantalla", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                                else
                                {
                                    MessageBox.Show("Factura Ya Existe Revise Numeros De Secuencia De Las Facturas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No puede facturar MONTOS DISTINTOS en FORMA DE PAGO y TOTAL", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ingrese Campos obligatorios", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("El Valor del Anticipo es Mayor al Valor de Anticipo Registrado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos: \n" + e.Message);

                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos: \n" + e.InnerException);
                this.Close();
            }
        }
        //GRABA TIPO IDENTIFICACION Y EMAIL DE ENVIO PABLO ROCHA 10-05-2018
        public void GrabarIdentificador()
        {
            var tipoIdentificacion = "";
            if (chk_Ruc.Checked == true)
                tipoIdentificacion = "04";
            else if (chk_Cedula.Checked == true)
                tipoIdentificacion = "05";
            else if (chk_pasaporte.Checked == true)
                tipoIdentificacion = "06";
            else
                tipoIdentificacion = "07";
            NegFactura.GuardaIdentificadorEmail(tipoIdentificacion, txt_Caja.Text + txt_Factura_Cod3.Text, txtEmailFactura.Text);
        }
        #region CODIGO PARA CREAR HONORARIOS DESDE CUENTA PACIENTE
        public void LiquidacionHonorarios()
        {
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(ultimaAtencion.ATE_CODIGO);
            List<CUENTAS_PACIENTES> lHonorarios = new List<CUENTAS_PACIENTES>();
            lHonorarios = NegFactura.Honorarios(ultimaAtencion.ATE_CODIGO);
            DataTable FormaPagoxDentro = new DataTable();
            FormaPagoxDentro = NegHonorariosMedicos.HMDentroPago(ultimaAtencion.ATE_FACTURA_PACIENTE);
            TIPO_REFERIDO tr = NegTipoReferido.RecuperarReferido(Convert.ToInt32(ultimaAtencion.TIPO_REFERIDOReference.EntityKey.EntityKeyValues[0].Value));
            //no se contempla que en consulta externa se tenga varios medicos asignados para la cancelacion de los $25.00
            if (lHonorarios.Count > 0)
            {
                if (FormaPagoxDentro.Rows.Count > 0) //Valida que tenga factura
                {
                    int index1 = Convert.ToInt32(FormaPagoxDentro.Rows[0]["forpag"].ToString()); //Codigo de la FORMA DE PAGO
                    int index2 = Convert.ToInt32(FormaPagoxDentro.Rows[0]["claspag"].ToString()); //Codigo de la clasificacion
                                                                                                  //el seguro no esta contemplado.
                    foreach (var item in lHonorarios)
                    {
                        FORMA_PAGO fp = NegFormaPago.recuperarFormaPago_forpag(index1.ToString());
                        if (index2 == 2) //es efectivo
                        {
                            if (!creacionHonorarioLiquidacion(item, fp, tr.TIR_CODIGO))
                            {
                                MessageBox.Show("Honorario de consulta externa no termino el proceso, intentelo de manera manual.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else if (index2 == 5) //otros documentos
                        {
                            if (index1 == 15) //Anticipos
                            {
                                DataTable AnticipoFormaP = NegFormaPago.FormaPagoAnticipo(FormaPagoxDentro.Rows[0]["cheque_caduca"].ToString());
                                if (AnticipoFormaP.Rows.Count > 0)
                                {
                                    if (AnticipoFormaP.Rows[0][1].ToString().Contains("EFECTIVO"))
                                    {
                                        fp = NegFormaPago.recuperarFormaPago_forpag("6");
                                        if (!creacionHonorarioLiquidacion(item, fp, tr.TIR_CODIGO))
                                        {
                                            MessageBox.Show("Honorario de consulta externa no termino el proceso, intentelo de manera manual.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    else
                                    {
                                        if (!creacionHonorarioLiquidacion(item, fp, tr.TIR_CODIGO))
                                        {
                                            MessageBox.Show("Honorario de consulta externa no termino el proceso, intentelo de manera manual.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                }
                            }
                        }
                        else if (index2 == 4)
                        {
                            if (!creacionHonorarioLiquidacion(item, fp, tr.TIR_CODIGO))
                            {
                                MessageBox.Show("Honorario de consulta externa no termino el proceso, intentelo de manera manual.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
        }
        public bool creacionHonorarioLiquidacion(CUENTAS_PACIENTES cp, FORMA_PAGO fp, int referido)
        {
            PARAMETROS_DETALLE obj = new PARAMETROS_DETALLE();
            obj = NegLiquidacion.parametrosHonorarios(18);
            if (Convert.ToBoolean(obj.PAD_ACTIVO))
            {

                bool valido = true;
                if (fp.FOR_CODIGO == 1) //es efectivo en el his
                {
                    //creacion del honorario
                    HONORARIOS_MEDICOS nuevoHonorario;
                    MEDICOS m = NegMedicos.recuperarMedico(Convert.ToInt32(cp.MED_CODIGO));
                    USUARIOS u = NegUsuarios.RecuperarUsuarioID(Convert.ToInt16(cp.ID_USUARIO));
                    nuevoHonorario = new HONORARIOS_MEDICOS();
                    nuevoHonorario.HOM_CODIGO = Convert.ToInt32(NegHonorariosMedicos.ultimoCodigoHonorarios() + 1);
                    nuevoHonorario.MEDICOSReference.EntityKey = m.EntityKey;
                    nuevoHonorario.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                    nuevoHonorario.FOR_CODIGO = Convert.ToInt16(fp.FOR_CODIGO);
                    nuevoHonorario.TMO_CODIGO = 201; //default
                    nuevoHonorario.USUARIOSReference.EntityKey = u.EntityKey;
                    nuevoHonorario.HOM_FECHA_INGRESO = DateTime.Now;
                    nuevoHonorario.NUM_PAGO = null;
                    nuevoHonorario.HOM_FACTURA_MEDICO = ""; //default
                    nuevoHonorario.HOM_FACTURA_FECHA = DateTime.Now;
                    nuevoHonorario.HOM_VALOR_NETO = (decimal)cp.CUE_VALOR_UNITARIO;
                    nuevoHonorario.HOM_COMISION_CLINICA = 0;
                    if (referido == 1) //hospitalario
                        nuevoHonorario.HOM_APORTE_LLAMADA = (decimal)Math.Round((Convert.ToDouble(cp.CUE_VALOR_UNITARIO) * (Convert.ToDouble(fp.FOR_REFERIDO)) / 100), 2);
                    else
                    {

                        if (m.MED_CODIGO_LIBRO == "1")
                        {
                            //?
                        }
                        if (Convert.ToInt32(m.ESPECIALIDADES_MEDICASReference.EntityKey.EntityKeyValues[0].Value) == 102 ||
                            Convert.ToInt32(m.ESPECIALIDADES_MEDICASReference.EntityKey.EntityKeyValues[0].Value) == 130 ||
                            Convert.ToInt32(m.ESPECIALIDADES_MEDICASReference.EntityKey.EntityKeyValues[0].Value) == 146 ||
                            Convert.ToInt32(m.ESPECIALIDADES_MEDICASReference.EntityKey.EntityKeyValues[0].Value) == 154)
                        {
                            nuevoHonorario.HOM_APORTE_LLAMADA = (decimal)Math.Round((Convert.ToDouble(cp.CUE_VALOR_UNITARIO) * (Convert.ToDouble(fp.FOR_REFERIDO)) / 100), 2);
                        }
                        else
                            nuevoHonorario.HOM_APORTE_LLAMADA = 0;

                    }
                    RETENCIONES_FUENTE rf = NegRetencionesFuente.recuperarPorId(Convert.ToInt32(m.RETENCIONES_FUENTEReference.EntityKey.EntityKeyValues[0].Value));
                    nuevoHonorario.HOM_RETENCION = (decimal)Math.Round((Convert.ToDouble(cp.CUE_VALOR_UNITARIO) * (Convert.ToDouble(rf.RET_PORCENTAJE)) / 100), 2);
                    nuevoHonorario.HOM_ESTADO = His.Parametros.HonorariosPAR.HonorarioCreado;
                    nuevoHonorario.HOM_VALOR_PAGADO = 0;
                    nuevoHonorario.HOM_VALOR_CANCELADO = 0;
                    nuevoHonorario.HOM_VALOR_TOTAL = nuevoHonorario.HOM_VALOR_NETO - nuevoHonorario.HOM_COMISION_CLINICA - nuevoHonorario.HOM_APORTE_LLAMADA - nuevoHonorario.HOM_RETENCION;
                    nuevoHonorario.HOM_POR_PAGAR = nuevoHonorario.HOM_VALOR_TOTAL;
                    nuevoHonorario.HOM_RECORTE = 0;
                    nuevoHonorario.HOM_NETO_PAGAR = nuevoHonorario.HOM_VALOR_TOTAL;
                    nuevoHonorario.HOM_OBSERVACION = "CONSULTA EXTERNA";
                    nuevoHonorario.HOM_PACIENTE = txt_ApellidoH1.Text.Trim();
                    nuevoHonorario.HOM_VALE = cp.NumVale;
                    nuevoHonorario.HOM_LOTE = "";

                    NegHonorariosMedicos.CrearHonorarioMedico(nuevoHonorario);
                    string FecCaducidad = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    NegHonorariosMedicos.saveHMDatosAdicionales(nuevoHonorario.HOM_CODIGO, FecCaducidad, 0, "0", Caja(), 0, 0);
                    NegHonorariosMedicos.CrearHonorarioAuditoria(nuevoHonorario, false, false, "NUEVO", Caja(), 0, Convert.ToInt64(cp.MED_CODIGO));
                }
                else
                {
                    //creacion del honorario
                    HONORARIOS_MEDICOS nuevoHonorario;
                    MEDICOS m = NegMedicos.recuperarMedico(Convert.ToInt32(cp.MED_CODIGO));
                    USUARIOS u = NegUsuarios.RecuperarUsuarioID(Convert.ToInt16(cp.ID_USUARIO));
                    nuevoHonorario = new HONORARIOS_MEDICOS();
                    nuevoHonorario.HOM_CODIGO = Convert.ToInt32(NegHonorariosMedicos.ultimoCodigoHonorarios() + 1);
                    nuevoHonorario.MEDICOSReference.EntityKey = m.EntityKey;
                    nuevoHonorario.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                    nuevoHonorario.FOR_CODIGO = Convert.ToInt16(fp.FOR_CODIGO);
                    nuevoHonorario.TMO_CODIGO = 201; //default
                    nuevoHonorario.USUARIOSReference.EntityKey = u.EntityKey;
                    nuevoHonorario.HOM_FECHA_INGRESO = DateTime.Now;
                    nuevoHonorario.NUM_PAGO = null;
                    nuevoHonorario.HOM_FACTURA_MEDICO = ""; //default
                    nuevoHonorario.HOM_FACTURA_FECHA = DateTime.Now;
                    nuevoHonorario.HOM_VALOR_NETO = (decimal)cp.CUE_VALOR_UNITARIO;
                    nuevoHonorario.HOM_COMISION_CLINICA = (decimal)Math.Round((Convert.ToDouble(cp.CUE_VALOR_UNITARIO) * (Convert.ToDouble(fp.FOR_COMISION)) / 100), 2);
                    if (referido == 1) //hospitalario
                        nuevoHonorario.HOM_APORTE_LLAMADA = (decimal)Math.Round((Convert.ToDouble(cp.CUE_VALOR_UNITARIO) * (Convert.ToDouble(fp.FOR_REFERIDO)) / 100), 2);
                    else
                    {

                        if (m.MED_CODIGO_LIBRO == "1")
                        {
                            //?
                        }
                        if (Convert.ToInt32(m.ESPECIALIDADES_MEDICASReference.EntityKey.EntityKeyValues[0].Value) == 102 ||
                            Convert.ToInt32(m.ESPECIALIDADES_MEDICASReference.EntityKey.EntityKeyValues[0].Value) == 130 ||
                            Convert.ToInt32(m.ESPECIALIDADES_MEDICASReference.EntityKey.EntityKeyValues[0].Value) == 146 ||
                            Convert.ToInt32(m.ESPECIALIDADES_MEDICASReference.EntityKey.EntityKeyValues[0].Value) == 154)
                        {
                            nuevoHonorario.HOM_APORTE_LLAMADA = (decimal)Math.Round((Convert.ToDouble(cp.CUE_VALOR_UNITARIO) * (Convert.ToDouble(fp.FOR_REFERIDO)) / 100), 2);
                        }
                        else
                            nuevoHonorario.HOM_APORTE_LLAMADA = 0;

                    }
                    RETENCIONES_FUENTE rf = NegRetencionesFuente.recuperarPorId(Convert.ToInt32(m.RETENCIONES_FUENTEReference.EntityKey.EntityKeyValues[0].Value));
                    nuevoHonorario.HOM_RETENCION = (decimal)Math.Round((Convert.ToDouble(cp.CUE_VALOR_UNITARIO) * (Convert.ToDouble(rf.RET_PORCENTAJE)) / 100), 2);
                    nuevoHonorario.HOM_ESTADO = His.Parametros.HonorariosPAR.HonorarioCreado;
                    nuevoHonorario.HOM_VALOR_PAGADO = 0;
                    nuevoHonorario.HOM_VALOR_CANCELADO = 0;
                    nuevoHonorario.HOM_VALOR_TOTAL = nuevoHonorario.HOM_VALOR_NETO - nuevoHonorario.HOM_COMISION_CLINICA - nuevoHonorario.HOM_APORTE_LLAMADA - nuevoHonorario.HOM_RETENCION;
                    nuevoHonorario.HOM_POR_PAGAR = nuevoHonorario.HOM_VALOR_TOTAL;
                    nuevoHonorario.HOM_RECORTE = 0;
                    nuevoHonorario.HOM_NETO_PAGAR = nuevoHonorario.HOM_VALOR_TOTAL;
                    nuevoHonorario.HOM_OBSERVACION = "CONSULTA EXTERNA";
                    nuevoHonorario.HOM_PACIENTE = txt_ApellidoH1.Text.Trim();
                    nuevoHonorario.HOM_VALE = cp.NumVale;
                    nuevoHonorario.HOM_LOTE = "";

                    NegHonorariosMedicos.CrearHonorarioMedico(nuevoHonorario);
                    string FecCaducidad = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    NegHonorariosMedicos.saveHMDatosAdicionales(nuevoHonorario.HOM_CODIGO, FecCaducidad, 0, "0", Caja(), 0, 0);
                    NegHonorariosMedicos.CrearHonorarioAuditoria(nuevoHonorario, false, false, "NUEVO", Caja(), 0, Convert.ToInt64(cp.MED_CODIGO));
                }

                return valido;
            }
            else
            {
                return false;
            }
        }
        #endregion

        //FACTURA ELECTRONICA PABLO ROCHA 03/09/2014
        public void GeneraFacturaElectronica(ref bool TipoDocumento)
        {
            txtfacturaelectronica.Text = "";
            His.Honorarios.Datos.Atencion obj_atencion = new His.Honorarios.Datos.Atencion();
            DataTable cajaEmite = new DataTable();
            int cajaaux = Convert.ToInt16(txt_Factura_Cod1.Text);

            cajaEmite = obj_atencion.FacturaElectronicaFisica(Convert.ToString(cajaaux));
            //cajaEmite = obj_atencion.FacturaElectronicaFisica(txt_Factura_Cod1.Text.Trim());
            if (cajaEmite.Rows[0][0].ToString() == "1")
            {
                DataTable Producto = new DataTable();
                DataTable tipoIdentificacion = new DataTable();
                DataTable Empresa = new DataTable();
                DataTable Descuentos = new DataTable();
                DataTable Iva = new DataTable();
                DataTable Tabla17 = new DataTable();
                DataTable Parametros = new DataTable();
                DataTable CodiSri = new DataTable();
                DataTable EmailyMedico = new DataTable();
                DataTable FechasPaciente = new DataTable();
                DataTable FacturaPago = new DataTable();
                double[,] descuentosrubros = new double[10, 2];
                Empresa = NegFactura.RecuperaEmpresa();
                Descuentos = NegFactura.RecuperaRubrosDescuentos();
                Tabla17 = NegFactura.Tabla17SRI();
                FechasPaciente = NegFactura.FechaPaciente(Convert.ToInt64(txt_Atencion.Text));
                string NumFacturaActual = (txt_Factura_Cod1.Text + txt_Factura_Cod3.Text);
                CodiSri = NegFormaPago.RecuperaCodiSri(NumFacturaActual);
                Iva = NegFactura.RecuperaRubrosIva(NumFacturaActual);
                tipoIdentificacion = NegFactura.RecuperatipoIdentificacionEmail(NumFacturaActual);
                EmailyMedico = NegFactura.RecuperaEmailMed(txt_Atencion.Text);
                Parametros = NegFactura.RecuperaParametros();
                //traer la informacion de la tabla TABLA17sri
                double porcentajeiva = double.Parse(Tabla17.Rows[0][1].ToString());
                int codigoiva = int.Parse(Tabla17.Rows[0][2].ToString());
                int tarifa = int.Parse(Tabla17.Rows[0][4].ToString());
                //if (NegFactura.VerificaFacturaElectronica(ultimaAtencion.ATE_CODIGO))
                //{
                if (Parametros.Rows[2][2].ToString() == "True")
                {
                    double restadescuento = Convert.ToDouble(txt_Descuento.Text);
                    string cadena = "";
                    txtfacturaelectronica.AppendText("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n");

                    if (TipoDocumento == true)
                        txtfacturaelectronica.AppendText("<factura id=\"comprobante\" version=\"1.1.0\">\r\n");
                    else
                        txtfacturaelectronica.AppendText("<notacredito id=\"comprobante\" version=\"1.1.0\">\r\n");

                    txtfacturaelectronica.AppendText("<infoTributaria>\r\n");

                    txtfacturaelectronica.AppendText("  <ambiente>" + Parametros.Rows[0][3].ToString() + "</ambiente>\r\n");

                    txtfacturaelectronica.AppendText("  <tipoEmision>1</tipoEmision>\r\n");

                    txtfacturaelectronica.AppendText("  <razonSocial>" + Empresa.Rows[0][2].ToString() + "</razonSocial>\r\n");

                    txtfacturaelectronica.AppendText("  <nombreComercial>" + Empresa.Rows[0][1].ToString() + "</nombreComercial>\r\n");

                    txtfacturaelectronica.AppendText("  <ruc>" + Empresa.Rows[0][3].ToString() + "</ruc>\r\n");

                    string numctrl = "";
                    if (TipoDocumento == true)
                        numctrl = "01";
                    else
                        numctrl = "04";
                    string numcontrol = txt_Factura_Cod3.Text;
                    string fecha = Convert.ToString(dtpFechaFacturacion.Value.Date);
                    //string fechaextr = FechasPaciente.Rows[0][4].ToString();
                    fecha = fecha.Substring(0, 10);
                    string caja = cajaEmite.Rows[0][2].ToString();
                    if (caja.Length != 3)
                    {
                        int contador = 3 - caja.Length;
                        caja = "";
                        for (int i = 0; i < contador; i++)
                        {
                            caja += "0";
                        }
                        caja += cajaEmite.Rows[0][2].ToString();
                    }
                    cadena = fecha.Substring(0, 2) + fecha.Substring(3, 2) + fecha.Substring(6, 4) +
                        numctrl + Empresa.Rows[0][3].ToString() + Parametros.Rows[0][3].ToString() + cajaEmite.Rows[0][1].ToString() + caja
                        + txt_Factura_Cod3.Text + numcontrol.Substring(1, 8) + "1";
                    GeneraCodigo(cadena);
                    if (TipoDocumento == true)
                        txtfacturaelectronica.AppendText("  <codDoc>01</codDoc>\r\n");
                    else
                        txtfacturaelectronica.AppendText("  <codDoc>04</codDoc>\r\n");

                    txtfacturaelectronica.AppendText("  <estab>" + cajaEmite.Rows[0][1].ToString() + "</estab>\r\n");

                    txtfacturaelectronica.AppendText("  <ptoEmi>" + caja + "</ptoEmi>\r\n");

                    txtfacturaelectronica.AppendText("  <secuencial>" + txt_Factura_Cod3.Text + "</secuencial>\r\n");

                    txtfacturaelectronica.AppendText("  <dirMatriz>" + Empresa.Rows[0][4].ToString() + "</dirMatriz>\r\n");

                    DataTable verificaResoluciones = new DataTable();
                    verificaResoluciones = NegFactura.RecuperaResolucion2020SRI();
                    if (verificaResoluciones.Rows.Count > 0)
                    {
                        if (verificaResoluciones.Rows[0][0].ToString() == "True")
                            txtfacturaelectronica.AppendText("  <agenteRetencion>" + verificaResoluciones.Rows[0][1].ToString() + "</agenteRetencion>\r\n");
                        if (verificaResoluciones.Rows[1][0].ToString() == "True")
                            txtfacturaelectronica.AppendText("  <contribuyenteRimpe>" + verificaResoluciones.Rows[1][1].ToString() + "</contribuyenteRimpe>\r\n");
                    }

                    txtfacturaelectronica.AppendText("</infoTributaria>\r\n");

                    if (TipoDocumento == true)
                        txtfacturaelectronica.AppendText("  <infoFactura>\r\n");
                    else
                        txtfacturaelectronica.AppendText("  <infoNotaCredito>\r\n");

                    txtfacturaelectronica.AppendText("      <fechaEmision>" + fecha.ToString() + "</fechaEmision>\r\n");

                    txtfacturaelectronica.AppendText("      <dirEstablecimiento>" + Empresa.Rows[0][4].ToString() + "</dirEstablecimiento>\r\n");

                    if (Convert.ToInt64(Empresa.Rows[0][23].ToString()) > 0)
                        txtfacturaelectronica.AppendText("      <contribuyenteEspecial>" + Empresa.Rows[0][23].ToString() + "</contribuyenteEspecial>\r\n");

                    txtfacturaelectronica.AppendText("      <obligadoContabilidad>" + Empresa.Rows[0][24].ToString() + "</obligadoContabilidad>\r\n");

                    txtfacturaelectronica.AppendText("      <tipoIdentificacionComprador>" + tipoIdentificacion.Rows[0][1].ToString() + "</tipoIdentificacionComprador>\r\n");

                    txtfacturaelectronica.AppendText("      <razonSocialComprador>" + txt_Cliente.Text.Trim() + "</razonSocialComprador>\r\n");

                    txtfacturaelectronica.AppendText("      <identificacionComprador>" + txt_Ruc.Text.Trim() + "</identificacionComprador>\r\n");

                    txtfacturaelectronica.AppendText("      <direccionComprador>" + txt_Direc_Cliente.Text.Trim().Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ") + "</direccionComprador>\r\n");
                    decimal valor = 0, subTotal = 0, totalConIva = 0, totalSinIva = 0, descuentoTotal = 0, ivaTotal = 0, totalFactura = 0;

                    if (dgvDivideFactura.Rows.Count > 0)
                    {
                        for (int i = 0; i < gridDetalleFactura.Rows.Count; i++)
                        {
                            if (dgvDivideFactura.Rows[i].Cells[6].Value == null)
                            {
                                subTotal += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["SUBTOTAL"].Value);
                                descuentoTotal += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);
                                totalSinIva += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value);
                                totalConIva += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value);
                                ivaTotal += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["IVA"].Value);

                            }
                            totalFactura = totalSinIva + totalConIva + ivaTotal;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gridDetalleFactura.Rows.Count; i++)
                        {
                            subTotal += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["SUBTOTAL"].Value);
                            descuentoTotal += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);
                            totalSinIva += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value);
                            totalConIva += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value);
                            ivaTotal += Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["IVA"].Value);
                        }
                        totalFactura = totalSinIva + totalConIva + ivaTotal;
                    }

                    decimal subtotal1 = totalSinIva + totalConIva;

                    decimal totalenfor = 0;
                    for (int i = 0; i < CodiSri.Rows.Count; i++)
                    {
                        totalenfor += Convert.ToDecimal(CodiSri.Rows[i][1].ToString());
                    }

                    txtfacturaelectronica.AppendText("      <totalSinImpuestos>" + subtotal1.ToString("N2").Replace(",", "") + "</totalSinImpuestos>\r\n");

                    txtfacturaelectronica.AppendText("      <totalDescuento>" + descuentoTotal.ToString("N2").Replace(",", "") + "</totalDescuento>\r\n");

                    txtfacturaelectronica.AppendText("      <totalConImpuestos>\r\n");

                    txtfacturaelectronica.AppendText("          <totalImpuesto>\r\n");

                    txtfacturaelectronica.AppendText("              <codigo>2</codigo>\r\n");

                    txtfacturaelectronica.AppendText("              <codigoPorcentaje>" + codigoiva + "</codigoPorcentaje>\r\n");

                    txtfacturaelectronica.AppendText("              <descuentoAdicional>0.00</descuentoAdicional>\r\n");

                    txtfacturaelectronica.AppendText("              <baseImponible>" + totalConIva.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                    txtfacturaelectronica.AppendText("              <valor>" + ivaTotal.ToString("N2").Replace(",", "") + "</valor>\r\n");

                    txtfacturaelectronica.AppendText("          </totalImpuesto>\r\n");

                    txtfacturaelectronica.AppendText("          <totalImpuesto>\r\n");

                    txtfacturaelectronica.AppendText("              <codigo>2</codigo>\r\n");

                    txtfacturaelectronica.AppendText("              <codigoPorcentaje>0</codigoPorcentaje>\r\n");

                    txtfacturaelectronica.AppendText("              <descuentoAdicional>0.00</descuentoAdicional>\r\n");

                    txtfacturaelectronica.AppendText("              <baseImponible>" + totalSinIva.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                    txtfacturaelectronica.AppendText("              <valor>0.00</valor>\r\n");

                    txtfacturaelectronica.AppendText("          </totalImpuesto>\r\n");

                    txtfacturaelectronica.AppendText("      </totalConImpuestos>\r\n");

                    txtfacturaelectronica.AppendText("      <propina>0.00</propina>\r\n");//NO SE ULTILIZA EN CASO DE CLINICAS...

                    txtfacturaelectronica.AppendText("      <importeTotal>" + totalenfor.ToString("N2").Replace(",", "") + "</importeTotal>\r\n");

                    txtfacturaelectronica.AppendText("      <moneda>DOLAR</moneda>\r\n");

                    txtfacturaelectronica.AppendText("      <pagos>\r\n");



                    for (int i = 0; i < CodiSri.Rows.Count; i++)
                    {
                        txtfacturaelectronica.AppendText("          <pago>\r\n");

                        txtfacturaelectronica.AppendText("              <formaPago>" + CodiSri.Rows[i][0].ToString().Trim() + "</formaPago>\r\n");
                        totalenfor = 0;
                        totalenfor = Convert.ToDecimal(CodiSri.Rows[i][1].ToString());
                        //txtfacturaelectronica.AppendText("              <total>" + totalFactura.ToString("N2").Replace(",", "") + "</total>\r\n");
                        txtfacturaelectronica.AppendText("              <total>" + totalenfor.ToString("N2").Replace(",", "") + "</total>\r\n");

                        txtfacturaelectronica.AppendText("              <plazo>0</plazo>\r\n");

                        txtfacturaelectronica.AppendText("              <unidadTiempo>meses</unidadTiempo>\r\n");

                        txtfacturaelectronica.AppendText("          </pago>\r\n");
                    }


                    txtfacturaelectronica.AppendText("      </pagos>\r\n");

                    if (TipoDocumento == true)
                        txtfacturaelectronica.AppendText("  </infoFactura>\r\n");
                    else
                        txtfacturaelectronica.AppendText("  </infoNotaCredito>\r\n");

                    txtfacturaelectronica.AppendText("  <detalles>\r\n");
                    double baseImponible;


                    if (dgvDivideFactura.Rows.Count > 0)
                    {
                        for (int i = 0; i < gridDetalleFactura.Rows.Count; i++)
                        {
                            if (dgvDivideFactura.Rows[i].Cells[6].Value == null)
                            {
                                if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0 && Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) > 0)
                                {
                                    txtfacturaelectronica.AppendText("      <detalle>\r\n");

                                    txtfacturaelectronica.AppendText("          <codigoPrincipal>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoPrincipal>\r\n");

                                    txtfacturaelectronica.AppendText("          <codigoAuxiliar>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoAuxiliar>\r\n");

                                    txtfacturaelectronica.AppendText("          <descripcion>" + gridDetalleFactura.Rows[i].Cells[1].Value.ToString() + " Con IVA" + "</descripcion>\r\n");

                                    txtfacturaelectronica.AppendText("          <cantidad>1</cantidad>\r\n");

                                    valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value);

                                    txtfacturaelectronica.AppendText("          <precioUnitario>" + valor.ToString("N2").Replace(",", "") + "</precioUnitario>\r\n");

                                    decimal descuento = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);
                                    txtfacturaelectronica.AppendText("          <descuento>" + descuento.ToString("N2").Replace(",", "") + "</descuento>\r\n");
                                    valor -= descuento;
                                    txtfacturaelectronica.AppendText("          <precioTotalSinImpuesto>" + valor.ToString("N2").Replace(",", "") + "</precioTotalSinImpuesto>\r\n");

                                    txtfacturaelectronica.AppendText("          <impuestos>\r\n");

                                    valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value) - Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);

                                    txtfacturaelectronica.AppendText("              <impuesto>\r\n");

                                    txtfacturaelectronica.AppendText("                  <codigo>2</codigo>\r\n");

                                    txtfacturaelectronica.AppendText("                  <codigoPorcentaje>" + codigoiva + "</codigoPorcentaje>\r\n");

                                    txtfacturaelectronica.AppendText("                  <tarifa>" + tarifa + "</tarifa>\r\n");

                                    txtfacturaelectronica.AppendText("                  <baseImponible>" + valor.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                                    baseImponible = Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["IVA"].Value);
                                    txtfacturaelectronica.AppendText("                  <valor>" + baseImponible.ToString("N2").Replace(",", "") + "</valor>\r\n");

                                    txtfacturaelectronica.AppendText("              </impuesto>\r\n");

                                    txtfacturaelectronica.AppendText("          </impuestos>\r\n");

                                    txtfacturaelectronica.AppendText("      </detalle>\r\n");

                                    txtfacturaelectronica.AppendText("      <detalle>\r\n");

                                    txtfacturaelectronica.AppendText("          <codigoPrincipal>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoPrincipal>\r\n");

                                    txtfacturaelectronica.AppendText("          <codigoAuxiliar>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoAuxiliar>\r\n");

                                    txtfacturaelectronica.AppendText("          <descripcion>" + gridDetalleFactura.Rows[i].Cells[1].Value.ToString() + " Sin IVA" + "</descripcion>\r\n");

                                    txtfacturaelectronica.AppendText("          <cantidad>1</cantidad>\r\n");

                                    valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value);

                                    txtfacturaelectronica.AppendText("          <precioUnitario>" + valor.ToString("N2").Replace(",", "") + "</precioUnitario>\r\n");

                                    descuento = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);
                                    txtfacturaelectronica.AppendText("          <descuento>" + descuento.ToString("N2").Replace(",", "") + "</descuento>\r\n");
                                    valor -= descuento;
                                    txtfacturaelectronica.AppendText("          <precioTotalSinImpuesto>" + valor.ToString("N2").Replace(",", "") + "</precioTotalSinImpuesto>\r\n");

                                    txtfacturaelectronica.AppendText("          <impuestos>\r\n");

                                    valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value) - Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);

                                    txtfacturaelectronica.AppendText("              <impuesto>\r\n");

                                    txtfacturaelectronica.AppendText("                  <codigo>2</codigo>\r\n");

                                    txtfacturaelectronica.AppendText("                  <codigoPorcentaje>0</codigoPorcentaje>\r\n");

                                    txtfacturaelectronica.AppendText("                  <tarifa>0</tarifa>\r\n");

                                    txtfacturaelectronica.AppendText("                  <baseImponible>" + valor.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                                    txtfacturaelectronica.AppendText("                  <valor>0.00</valor>\r\n");

                                    txtfacturaelectronica.AppendText("              </impuesto>\r\n");

                                    txtfacturaelectronica.AppendText("          </impuestos>\r\n");

                                    txtfacturaelectronica.AppendText("      </detalle>\r\n");
                                }
                                else
                                {
                                    if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0 || Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) > 0)
                                    {
                                        txtfacturaelectronica.AppendText("      <detalle>\r\n");

                                        txtfacturaelectronica.AppendText("          <codigoPrincipal>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoPrincipal>\r\n");

                                        txtfacturaelectronica.AppendText("          <codigoAuxiliar>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoAuxiliar>\r\n");

                                        txtfacturaelectronica.AppendText("          <descripcion>" + gridDetalleFactura.Rows[i].Cells[1].Value.ToString() + "</descripcion>\r\n");

                                        txtfacturaelectronica.AppendText("          <cantidad>1</cantidad>\r\n");

                                        if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0 && Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) > 0)
                                            valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value) + Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value);
                                        else if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0)
                                            valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value);
                                        else
                                            valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value);

                                        txtfacturaelectronica.AppendText("          <precioUnitario>" + valor.ToString("N2").Replace(",", "") + "</precioUnitario>\r\n");

                                        decimal descuento = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);
                                        txtfacturaelectronica.AppendText("          <descuento>" + descuento.ToString("N2").Replace(",", "") + "</descuento>\r\n");
                                        valor -= descuento;
                                        txtfacturaelectronica.AppendText("          <precioTotalSinImpuesto>" + valor.ToString("N2").Replace(",", "") + "</precioTotalSinImpuesto>\r\n");

                                        txtfacturaelectronica.AppendText("          <impuestos>\r\n");


                                        if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0)
                                        {
                                            valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value) - Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);

                                            txtfacturaelectronica.AppendText("              <impuesto>\r\n");

                                            txtfacturaelectronica.AppendText("                  <codigo>2</codigo>\r\n");

                                            txtfacturaelectronica.AppendText("                  <codigoPorcentaje>" + codigoiva + "</codigoPorcentaje>\r\n");

                                            txtfacturaelectronica.AppendText("                  <tarifa>" + tarifa + "</tarifa>\r\n");

                                            txtfacturaelectronica.AppendText("                  <baseImponible>" + valor.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                                            baseImponible = Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["IVA"].Value);
                                            txtfacturaelectronica.AppendText("                  <valor>" + baseImponible.ToString("N2").Replace(",", "") + "</valor>\r\n");

                                            txtfacturaelectronica.AppendText("              </impuesto>\r\n");
                                        }

                                        if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells[4].Value.ToString()) > 0)
                                        {
                                            valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value) - Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);

                                            txtfacturaelectronica.AppendText("              <impuesto>\r\n");

                                            txtfacturaelectronica.AppendText("                  <codigo>2</codigo>\r\n");

                                            txtfacturaelectronica.AppendText("                  <codigoPorcentaje>0</codigoPorcentaje>\r\n");

                                            txtfacturaelectronica.AppendText("                  <tarifa>0</tarifa>\r\n");

                                            txtfacturaelectronica.AppendText("                  <baseImponible>" + valor.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                                            txtfacturaelectronica.AppendText("                  <valor>0.00</valor>\r\n");

                                            txtfacturaelectronica.AppendText("              </impuesto>\r\n");
                                        }

                                        txtfacturaelectronica.AppendText("          </impuestos>\r\n");

                                        txtfacturaelectronica.AppendText("      </detalle>\r\n");
                                    }
                                }


                            }
                        }
                    }

                    else
                    {
                        for (int i = 0; i < gridDetalleFactura.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0 && Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) > 0)
                            {
                                txtfacturaelectronica.AppendText("      <detalle>\r\n");

                                txtfacturaelectronica.AppendText("          <codigoPrincipal>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "" + "</codigoPrincipal>\r\n");

                                txtfacturaelectronica.AppendText("          <codigoAuxiliar>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "" + "</codigoAuxiliar>\r\n");

                                txtfacturaelectronica.AppendText("          <descripcion>" + gridDetalleFactura.Rows[i].Cells[1].Value.ToString() + " Con IVA" + "</descripcion>\r\n");

                                txtfacturaelectronica.AppendText("          <cantidad>1</cantidad>\r\n");

                                int indice = Convert.ToInt16(gridDetalleFactura.Rows[i].Cells["INDICE"].Value);

                                DataTable totalizadosConIva = NegFactura.RecuperaDescuentoXrubroConIva(ultimaAtencion.ATE_CODIGO, indice);
                                decimal descuento = Convert.ToDecimal(totalizadosConIva.Rows[0][1].ToString());
                                valor = Convert.ToDecimal(totalizadosConIva.Rows[0][0].ToString());

                                txtfacturaelectronica.AppendText("          <precioUnitario>" + valor.ToString("N2").Replace(",", "") + "</precioUnitario>\r\n");
                                txtfacturaelectronica.AppendText("          <descuento>" + descuento.ToString("N2").Replace(",", "") + "</descuento>\r\n");

                                valor -= descuento;

                                txtfacturaelectronica.AppendText("          <precioTotalSinImpuesto>" + valor.ToString("N2").Replace(",", "") + "</precioTotalSinImpuesto>\r\n");

                                txtfacturaelectronica.AppendText("          <impuestos>\r\n");


                                txtfacturaelectronica.AppendText("              <impuesto>\r\n");

                                txtfacturaelectronica.AppendText("                  <codigo>2</codigo>\r\n");

                                txtfacturaelectronica.AppendText("                  <codigoPorcentaje>" + codigoiva + "</codigoPorcentaje>\r\n");

                                txtfacturaelectronica.AppendText("                  <tarifa>" + tarifa + "</tarifa>\r\n");

                                txtfacturaelectronica.AppendText("                  <baseImponible>" + valor.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                                baseImponible = Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["IVA"].Value);
                                txtfacturaelectronica.AppendText("                  <valor>" + baseImponible.ToString("N2").Replace(",", "") + "</valor>\r\n");

                                txtfacturaelectronica.AppendText("              </impuesto>\r\n");

                                txtfacturaelectronica.AppendText("          </impuestos>\r\n");

                                txtfacturaelectronica.AppendText("      </detalle>\r\n");

                                txtfacturaelectronica.AppendText("      <detalle>\r\n");

                                txtfacturaelectronica.AppendText("          <codigoPrincipal>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoPrincipal>\r\n");

                                txtfacturaelectronica.AppendText("          <codigoAuxiliar>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoAuxiliar>\r\n");

                                txtfacturaelectronica.AppendText("          <descripcion>" + gridDetalleFactura.Rows[i].Cells[1].Value.ToString() + " Sin IVA" + "</descripcion>\r\n");

                                txtfacturaelectronica.AppendText("          <cantidad>1</cantidad>\r\n");


                                //if (valor < descuento)
                                //{
                                //    txtfacturaelectronica.AppendText("          <precioUnitario>" + valor.ToString("N2").Replace(",", "") + "</precioUnitario>\r\n");
                                //    txtfacturaelectronica.AppendText("          <descuento>0.00</descuento>\r\n");

                                //}
                                //else
                                //{
                                //    valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value) + Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);
                                //}
                                DataTable totalizadosSinIva = NegFactura.RecuperaDescuentoXrubroSinIva(ultimaAtencion.ATE_CODIGO, indice);
                                descuento = Convert.ToDecimal(totalizadosSinIva.Rows[0][1].ToString());
                                valor = Convert.ToDecimal(totalizadosSinIva.Rows[0][0].ToString());
                                txtfacturaelectronica.AppendText("          <precioUnitario>" + valor.ToString("N2").Replace(",", "") + "</precioUnitario>\r\n");
                                txtfacturaelectronica.AppendText("          <descuento>" + descuento.ToString("N2").Replace(",", "") + "</descuento>\r\n");

                                valor -= descuento;

                                txtfacturaelectronica.AppendText("          <precioTotalSinImpuesto>" + valor.ToString("N2").Replace(",", "") + "</precioTotalSinImpuesto>\r\n");

                                txtfacturaelectronica.AppendText("          <impuestos>\r\n");

                                txtfacturaelectronica.AppendText("              <impuesto>\r\n");

                                txtfacturaelectronica.AppendText("                  <codigo>2</codigo>\r\n");

                                txtfacturaelectronica.AppendText("                  <codigoPorcentaje>0</codigoPorcentaje>\r\n");

                                txtfacturaelectronica.AppendText("                  <tarifa>0</tarifa>\r\n");

                                txtfacturaelectronica.AppendText("                  <baseImponible>" + valor.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                                txtfacturaelectronica.AppendText("                  <valor>0.00</valor>\r\n");

                                txtfacturaelectronica.AppendText("              </impuesto>\r\n");

                                txtfacturaelectronica.AppendText("          </impuestos>\r\n");

                                txtfacturaelectronica.AppendText("      </detalle>\r\n");
                            }
                            else
                            {
                                if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0 || Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) > 0)
                                {
                                    decimal descuento = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);

                                    txtfacturaelectronica.AppendText("      <detalle>\r\n");

                                    txtfacturaelectronica.AppendText("          <codigoPrincipal>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoPrincipal>\r\n");

                                    txtfacturaelectronica.AppendText("          <codigoAuxiliar>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoAuxiliar>\r\n");

                                    txtfacturaelectronica.AppendText("          <descripcion>" + gridDetalleFactura.Rows[i].Cells[1].Value.ToString() + "</descripcion>\r\n");

                                    txtfacturaelectronica.AppendText("          <cantidad>1</cantidad>\r\n");

                                    if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0 && Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) > 0)
                                        valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value) + Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value);
                                    else if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0)
                                        valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value) + descuento;
                                    else
                                        valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value) + descuento;

                                    txtfacturaelectronica.AppendText("          <precioUnitario>" + valor.ToString("N2").Replace(",", "") + "</precioUnitario>\r\n");

                                    txtfacturaelectronica.AppendText("          <descuento>" + descuento.ToString("N2").Replace(",", "") + "</descuento>\r\n");
                                    valor -= descuento;
                                    txtfacturaelectronica.AppendText("          <precioTotalSinImpuesto>" + valor.ToString("N2").Replace(",", "") + "</precioTotalSinImpuesto>\r\n");

                                    txtfacturaelectronica.AppendText("          <impuestos>\r\n");


                                    if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) > 0)
                                    {
                                        valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value);

                                        txtfacturaelectronica.AppendText("              <impuesto>\r\n");

                                        txtfacturaelectronica.AppendText("                  <codigo>2</codigo>\r\n");

                                        txtfacturaelectronica.AppendText("                  <codigoPorcentaje>" + codigoiva + "</codigoPorcentaje>\r\n");

                                        txtfacturaelectronica.AppendText("                  <tarifa>" + tarifa + "</tarifa>\r\n");

                                        txtfacturaelectronica.AppendText("                  <baseImponible>" + valor.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                                        baseImponible = Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["IVA"].Value);
                                        txtfacturaelectronica.AppendText("                  <valor>" + baseImponible.ToString("N2").Replace(",", "") + "</valor>\r\n");

                                        txtfacturaelectronica.AppendText("              </impuesto>\r\n");
                                    }

                                    if (Convert.ToDouble(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) > 0)
                                    {
                                        valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value);

                                        txtfacturaelectronica.AppendText("              <impuesto>\r\n");

                                        txtfacturaelectronica.AppendText("                  <codigo>2</codigo>\r\n");

                                        txtfacturaelectronica.AppendText("                  <codigoPorcentaje>0</codigoPorcentaje>\r\n");

                                        txtfacturaelectronica.AppendText("                  <tarifa>0</tarifa>\r\n");

                                        txtfacturaelectronica.AppendText("                  <baseImponible>" + valor.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                                        txtfacturaelectronica.AppendText("                  <valor>0.00</valor>\r\n");

                                        txtfacturaelectronica.AppendText("              </impuesto>\r\n");
                                    }

                                    txtfacturaelectronica.AppendText("          </impuestos>\r\n");

                                    txtfacturaelectronica.AppendText("      </detalle>\r\n");
                                }
                                else
                                {
                                    decimal descuento = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value);

                                    txtfacturaelectronica.AppendText("      <detalle>\r\n");

                                    txtfacturaelectronica.AppendText("          <codigoPrincipal>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoPrincipal>\r\n");

                                    txtfacturaelectronica.AppendText("          <codigoAuxiliar>" + gridDetalleFactura.Rows[i].Cells[0].Value.ToString() + "</codigoAuxiliar>\r\n");

                                    txtfacturaelectronica.AppendText("          <descripcion>" + gridDetalleFactura.Rows[i].Cells[1].Value.ToString() + "</descripcion>\r\n");

                                    txtfacturaelectronica.AppendText("          <cantidad>1</cantidad>\r\n");


                                    valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["SUBTOTAL"].Value);


                                    txtfacturaelectronica.AppendText("          <precioUnitario>" + valor.ToString("N2").Replace(",", "") + "</precioUnitario>\r\n");

                                    txtfacturaelectronica.AppendText("          <descuento>" + descuento.ToString("N2").Replace(",", "") + "</descuento>\r\n");
                                    valor -= descuento;
                                    txtfacturaelectronica.AppendText("          <precioTotalSinImpuesto>" + valor.ToString("N2").Replace(",", "") + "</precioTotalSinImpuesto>\r\n");

                                    txtfacturaelectronica.AppendText("          <impuestos>\r\n");

                                    valor = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value);

                                    txtfacturaelectronica.AppendText("              <impuesto>\r\n");

                                    txtfacturaelectronica.AppendText("                  <codigo>2</codigo>\r\n");

                                    txtfacturaelectronica.AppendText("                  <codigoPorcentaje>0</codigoPorcentaje>\r\n");

                                    txtfacturaelectronica.AppendText("                  <tarifa>0</tarifa>\r\n");

                                    txtfacturaelectronica.AppendText("                  <baseImponible>" + valor.ToString("N2").Replace(",", "") + "</baseImponible>\r\n");

                                    txtfacturaelectronica.AppendText("                  <valor>0.00</valor>\r\n");

                                    txtfacturaelectronica.AppendText("              </impuesto>\r\n");

                                    txtfacturaelectronica.AppendText("          </impuestos>\r\n");

                                    txtfacturaelectronica.AppendText("      </detalle>\r\n");
                                }
                            }
                        }
                    }

                    var correoEmpresa = "";
                    correoEmpresa = Parametros.Rows[2][4].ToString();

                    txtfacturaelectronica.AppendText("  </detalles>\r\n");

                    txtfacturaelectronica.AppendText("  <infoAdicional>\r\n");

                    if (Parametros.Rows[3][4].ToString() != "")
                    {
                        txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Acuerdo\">" + Parametros.Rows[3][4].ToString() + "</campoAdicional>\r\n");
                        if (caja == "999" || caja == "998" || caja == "997")
                            txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Factura\">Transferencia a título gratuito</campoAdicional>\r\n");

                    }

                    txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Teléfono Clínica\">" + Empresa.Rows[0]["telefono"].ToString() + "</campoAdicional>\r\n");

                    if (Parametros.Rows[1][4].ToString() == "2")
                        txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Email\">" + correoEmpresa.ToString() + ";" + tipoIdentificacion.Rows[0][3].ToString() + "</campoAdicional>\r\n");
                    else
                        txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Email\">" + correoEmpresa.ToString() + ";" + Parametros.Rows[2][4].ToString() + "</campoAdicional>\r\n");

                    txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Comentario\">" + txtObserva.Text.Trim() + "</campoAdicional>\r\n");


                    //txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"BaseCero\">" + Convert.ToDouble(txt_SinIVA.Text.Trim()) + "</campoAdicional>\r\n");

                    //txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"BaseIva\">" + Convert.ToDouble(txt_ConIVA.Text.Trim()) + "</campoAdicional>\r\n");

                    txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Paciente\">" + txt_ApellidoH1.Text.Trim() + "</campoAdicional>\r\n");

                    txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"HistoriaClinica\">" + txt_Historia_Pc.Text.Trim() + "</campoAdicional>\r\n");

                    txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Número De Atención\">" + FechasPaciente.Rows[0][0].ToString().Trim() + "</campoAdicional>\r\n");

                    if (EmailyMedico.Rows[0][1].ToString().Trim() != "")
                        txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Medico\">" + EmailyMedico.Rows[0][1].ToString().Trim() + "</campoAdicional>\r\n");
                    else
                        txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Medico\">MÉDICO RESIDENTE</campoAdicional>\r\n");

                    txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Fecha Ingreso\">" + FechasPaciente.Rows[0][1].ToString().Trim() + "</campoAdicional>\r\n");

                    if (FechasPaciente.Rows[0][2].ToString() == null || FechasPaciente.Rows[0][2].ToString() == "")
                        txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Fecha De Alta\">" + fecha.ToString().Trim() + "</campoAdicional>\r\n");
                    else
                        txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Fecha De Alta\">" + Convert.ToDateTime(FechasPaciente.Rows[0][2].ToString().Trim()) + "</campoAdicional>\r\n");

                    txtfacturaelectronica.AppendText("      <campoAdicional nombre=\"Cajero\">" + FechasPaciente.Rows[0][3].ToString().Trim() + "</campoAdicional>\r\n");

                    txtfacturaelectronica.AppendText("  </infoAdicional>\r\n");

                    if (TipoDocumento == true)
                        txtfacturaelectronica.AppendText("</factura>\r\n");
                    else
                        txtfacturaelectronica.AppendText("</notacredito>\r\n");
                    var facturaelectronica = txtfacturaelectronica.Text;
                    Encoding enc = new UTF8Encoding(true, true);
                    string value2 = "";
                    try
                    {
                        byte[] bytes = enc.GetBytes(facturaelectronica);
                        value2 = enc.GetString(bytes);
                    }
                    catch (Exception)
                    {
                        value2 = facturaelectronica;
                    }
                    var directorio = Parametros.Rows[1][6].ToString();
                    var ftpdirectorio = "";
                    ftpdirectorio = Parametros.Rows[2][6].ToString();
                    var tipDoc = "";
                    if (TipoDocumento == true)
                        tipDoc = "FACT-";
                    else
                        tipDoc = "NC-";
                    if (File.Exists(directorio + "\\" + tipDoc + Empresa.Rows[0][3].ToString() + "-" + Parametros.Rows[0][3].ToString() + "-" + txt_Factura_Cod1.Text + txt_Factura_Cod3.Text.Trim() +
                        "-" + fecha.Substring(3, 2) + fecha.Substring(6, 4) + ".xml"))
                    {
                        File.Delete(directorio + "\\" + tipDoc + Empresa.Rows[0][3].ToString() + "-" + Parametros.Rows[0][3].ToString() + "-" + txt_Factura_Cod1.Text + txt_Factura_Cod3.Text.Trim() +
                        "-" + fecha.Substring(3, 2) + fecha.Substring(6, 4) + ".xml");
                    }
                    try
                    {
                        var batchWriter = new StreamWriter(directorio + "\\" + tipDoc + Empresa.Rows[0][3].ToString() + "-" + Parametros.Rows[0][3].ToString() + "-" + txt_Factura_Cod1.Text + txt_Factura_Cod3.Text.Trim() +
                            "-" + fecha.Substring(3, 2) + fecha.Substring(6, 4) + ".xml");
                        batchWriter.Write(value2);
                        batchWriter.Close();
                        StreamWriter sw = new StreamWriter(File.Open(directorio + "\\" + tipDoc + Empresa.Rows[0][3].ToString() + "-" + Parametros.Rows[0][3].ToString() + "-" + txt_Factura_Cod1.Text + txt_Factura_Cod3.Text.Trim() +
                            "-" + fecha.Substring(3, 2) + fecha.Substring(6, 4) + ".xml", FileMode.Create), Encoding.UTF8);
                        sw.Write(value2);
                        sw.Close();
                        //enviar el xml al ftp de gap system Pablo Rocha 15-02-2018
                        string directorio1 = (directorio + "\\" + tipDoc + Empresa.Rows[0][3].ToString() + "-" + Parametros.Rows[0][3].ToString() + "-" + txt_Factura_Cod1.Text + txt_Factura_Cod3.Text.Trim() +
                            "-" + fecha.Substring(3, 2) + fecha.Substring(6, 4) + ".xml").ToString();
                        string directorio2 = (tipDoc + Empresa.Rows[0][3].ToString() + "-" + Parametros.Rows[0][3].ToString() + "-" + txt_Factura_Cod1.Text + txt_Factura_Cod3.Text.Trim() +
                            "-" + fecha.Substring(3, 2) + fecha.Substring(6, 4) + ".xml").ToString();

                        try
                        {
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(@ftpdirectorio + directorio2)) as FtpWebRequest;
                            request.Method = WebRequestMethods.Ftp.UploadFile;
                            // Asigna las credenciales para obtener el acceso al ftp
                            request.Credentials = new NetworkCredential("FtpBackGap", "Gapgr2021$");
                            request.UsePassive = false;
                            request.UseBinary = true;
                            request.KeepAlive = true;
                            // Envia la informacion al servidor
                            StreamReader sourceStream = new StreamReader(directorio1);
                            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                            sourceStream.Close();
                            request.ContentLength = fileContents.Length;
                            Stream requestStream = request.GetRequestStream();
                            requestStream.Write(fileContents, 0, fileContents.Length);
                            requestStream.Close();
                            FtpWebResponse response = (FtpWebResponse)request.GetResponse() as FtpWebResponse;
                            //MessageBox.Show("Documento Electronico Enviado Con Exito");
                            response.Close();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                        try
                        {
                            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(@"ftp://pichincha.gapsystem.net/" + directorio2)) as FtpWebRequest;
                            request.Method = WebRequestMethods.Ftp.UploadFile;
                            // Asigna las credenciales para obtener el acceso al ftp
                            request.Credentials = new NetworkCredential("UserFtpGap", "Gapgr2019$");
                            request.UsePassive = false;
                            request.UseBinary = true;
                            request.KeepAlive = true;
                            // Envia la informacion al servidor
                            StreamReader sourceStream = new StreamReader(directorio1);
                            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                            sourceStream.Close();
                            request.ContentLength = fileContents.Length;
                            Stream requestStream = request.GetRequestStream();
                            requestStream.Write(fileContents, 0, fileContents.Length);
                            requestStream.Close();
                            FtpWebResponse response = (FtpWebResponse)request.GetResponse() as FtpWebResponse;
                            MessageBox.Show("Documento Electronico Enviado Con Exito");
                            response.Close();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Documento Electronico No Se Envio, Revise Su Conexión a Internet \n" + e.Message);
                            if (e.InnerException != null)
                                MessageBox.Show("Documento Electronico No Se Envio, Revise Su Conexión a Internet \n" + e.InnerException);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Documento Electronico No Se Guardo En Servidor Central Vuelva a Cargar La Cuenta, Revise Su Conexión Al Servidor\n" + e.Message);
                        //this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Desde esta caja no se puede emitir factura electrónica", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void GeneraCodigo(string tbCadena)
        {
            string cadena = tbCadena;
            string cadenaInversa = "";
            string tbInversa;
            string tbCadenaMultiplos = "";
            string tbSumatoria;
            string tbMod11;
            string tbDigitoVerificador;
            string tbResta11MenosMod;
            string tbClaveAcceso;
            for (int i = cadena.Length - 1; i >= 0; i--)
            {
                cadenaInversa += cadena[i];
            }
            tbInversa = cadenaInversa.ToString();
            string[] cadenaParalela = { "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7", "2", "3", "4", "5", "6", "7" };
            foreach (string valorMultiplo in cadenaParalela)
            {
                tbCadenaMultiplos += "[" + valorMultiplo + "]";
            }

            char[] arregloInverso = cadenaInversa.ToCharArray();

            int sumatoria = 0;
            for (int i = 0; i < arregloInverso.Length; i++)
            {
                sumatoria += int.Parse(arregloInverso[i].ToString()) * int.Parse(cadenaParalela[i]);
            }

            tbSumatoria = sumatoria.ToString();
            int restoDevision = sumatoria % 11;

            tbMod11 = restoDevision.ToString();
            int resta11Mod = 11 - restoDevision;
            tbResta11MenosMod = resta11Mod.ToString();

            int digitoVericadorNum = resta11Mod;
            if (resta11Mod == 11)
                digitoVericadorNum = 0;
            if (resta11Mod == 10)
                digitoVericadorNum = 1;
            tbDigitoVerificador = digitoVericadorNum.ToString();
            tbClaveAcceso = tbCadena + digitoVericadorNum.ToString();
            txtfacturaelectronica.AppendText("  <claveAcceso>" + tbClaveAcceso + "</claveAcceso>\r\n");
        }
        private bool validarFormulario()
        {
            errorFactura.Clear();
            bool valido = false;
            if (gridDetalleFactura.Rows.Count == 0)
            {
                AgregarError(gridDetalleFactura);
                valido = true;
            }
            if (gridFormasPago.Rows.Count == 0)
            {
                AgregarError(gridFormasPago);
                valido = true;
            }
            //if (txt_telef_Cliente.TextLength > 10)
            //{
            //    AgregarError(txt_telef_Cliente);
            //    valido = true;
            //}
            if (txt_Cliente.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Cliente);
                valido = true;
            }
            if (txtObserva.Text.Trim() == string.Empty && facturaPrefactura != 2)
            {
                AgregarError(txtObserva);
                valido = true;
            }
            if (txtEmailFactura.Text.Trim() == string.Empty)
            {
                AgregarError(txtEmailFactura);
                valido = true;
            }
            if (!NegUtilitarios.validadorEmail(txtEmailFactura.Text))
            {
                AgregarError(txtEmailFactura);
                valido = true;
            }
            if (txt_Direc_Cliente.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Direc_Cliente);
                valido = true;

            }
            if (txt_Factura_Cod1.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Factura_Cod1);
                valido = true;
            }
            if (txt_Factura_Cod3.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Factura_Cod3);
                valido = true;
            }
            if (txt_Autorizacion.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Autorizacion);
                valido = true;
            }
            return valido;
        }
        private void AgregarError(Control control)
        {
            errorFactura.SetError(control, "Campo Requerido");
        }
        private void agregarDatosFactura(int codigoFactura)
        {
            try
            {
                nuevaFactura.FAC_CODIGO = codigoFactura;
                nuevaFactura.ATENCIONESReference.EntityKey = ultimaAtencion.EntityKey;
                nuevaFactura.FAC_NUMERO = txt_Factura_Cod1.Text.Trim() + "-" + txt_Factura_Cod3.Text.Trim();
                nuevaFactura.FAC_AUTORIZACION = Convert.ToInt32(txt_Autorizacion.Text.Trim());
                nuevaFactura.FAC_FECHA = dtpFechaFacturacion.Value;
                nuevaFactura.CLI_RUC = txt_Ruc.Text.Trim();
                nuevaFactura.CLI_NOMBRE = txt_Cliente.Text.Trim();
                nuevaFactura.CLI_TELEFONO = txt_telef_Cliente.Text.TrimEnd();
                nuevaFactura.FAC_TOTAL = Convert.ToDecimal(txt_Total.Text);
                nuevaFactura.FAC_SUBTOTAL = Convert.ToDecimal(txt_SubTotal.Text);
                nuevaFactura.FAC_IVAUNO = Convert.ToDecimal(txt_ConIVA.Text);
                nuevaFactura.FAC_IVADOS = Convert.ToDecimal(txt_SinIVA.Text);
                nuevaFactura.FAC_IVATRES = Convert.ToDecimal(txt_IVA.Text);
                nuevaFactura.FAC_ESTADO = "PAGADO";
                nuevaFactura.FAC_CAJA = Convert.ToString(caja.CAJ_CODIGO);
                nuevaFactura.FAC_VENDEDOR = "";
                nuevaFactura.FAC_ARQUEO = 0;
                nuevaFactura.FAC_DESCUENTO = Convert.ToDecimal(txt_Descuento.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos: \n" + e.InnerException);
            }
        }
        private void agregarDatosDetalleFactura(FACTURA nuevaFactura)
        {
            try
            {
                List<FACTURA_DETALLE> listaFacturaDetalle = new List<FACTURA_DETALLE>();
                for (int i = 0; i < gridDetalleFactura.Rows.Count; i++)
                {
                    facturaDetalle = new FACTURA_DETALLE();
                    facturaDetalle.COD_FDETALLE = NegFactura.recuperaMaximoDetalleFactura() + 1;
                    facturaDetalle.FACTURAReference.EntityKey = nuevaFactura.EntityKey;
                    facturaDetalle.DET_DESCIPCION = Convert.ToString(gridDetalleFactura.Rows[i].Cells[1].Value);
                    facturaDetalle.DET_VALOR = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells[2].Value);
                    facturaDetalle.DET_ESTADO = 1;
                    NegFactura.crearFacturaDetalle(facturaDetalle);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos Detalle Factura: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos Detalle Factura: \n" + e.InnerException);
            }
        }
        private void CuentaCancelada()
        {
            //List<CUENTAS_PACIENTES> listaCuentasPacientes = listaCuentasPacientes = NegCuentasPacientes.RecuperarCuenta(ultimaAtencion.ATE_CODIGO);
            //foreach (var dtoCuentas in listaCuentasPacientes)
            //{
            //    dtoCuentas.CUE_NUM_FAC = txt_Caja.Text + txt_Factura_Cod3.Text;
            NegCuentasPacientes.actualizarCuentaNumFac(ultimaAtencion.ATE_CODIGO, txt_Caja.Text + txt_Factura_Cod3.Text);
            //}
        }
        private Int16 validarCedula_Factura()
        {
            string Cedula = "";
            if (txt_Ruc.Text.ToString() != string.Empty)
            {
                if (txt_Ruc.Text.ToString() == "9999999999")
                {
                    return 1;
                }
                Cedula = txt_Ruc.Text.Trim();
                if (Cedula.Length == 10 || Cedula.Length == 13)
                {
                    //SE QUITA VALIDACION POR PROBLEMAS DE VALIDACION DE RUC DE PROACTIVE DE LA ALIANZA 20220506 - 0826 
                    //if (NegValidaciones.esCedulaValida(txt_Ruc.Text) != true)
                    //{
                    //    txt_Ruc.Text = string.Empty;
                    //    return 0;
                    //}
                    //else
                    //{
                    //    return 1;
                    //}
                    return 1;
                }
                else
                {
                    MessageBox.Show("La identificacion ingresada es incorrecta");
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        private bool validarFormaPagos()
        {
            Double valor = 0.00;
            for (int i = 0; i < gridFormasPago.Rows.Count; i++)
            {
                if (gridFormasPago.Rows[i].Cells[0].Value != null)
                    valor = Convert.ToDouble(gridFormasPago.Rows[i].Cells[2].Value);
            }
            if (valor < 0.00)
                return false;
            else
                return true;
        }
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }

        #endregion

        #region Eventos

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            incioFactura();
            //NumeroPrefactura = ConexionAccesPrefactura("Select numcon from Numero_Control where codcon=73");
        }
        public void incioFactura()
        {
            DirectorioEmpresa();
            if (ConexionAcces("Select * from Numero_Control where codcon=72") == true) // Verifica el secuencial y autorizacion de la factura / 15/11/2012/ Giovanny Tapia
            {
                habiltarBotones(false, true, false, true, false, true, true, true);
                ultraGroupBox1.Enabled = true;   // Habilita los controles para el ingreso de datos / 07/11/2012 / Giovanny Tapia
                ultraTabControl1.Enabled = true;
                ultraTabControl2.Enabled = true;
                accionPaciente = false;
                accionFactura = true;
                facturaPrefactura = 1;
                ultraTabPageControl6.Enabled = true;
                txtObserva.Enabled = true;
                txtObserva.Text = "";
                btnBuscarDatos.Enabled = true;
                limpiarCampos();
            }
            else
            {
                MessageBox.Show("Este computador no tiene los datos necesarios para facturar. Consulte con el administrador del sistema.", "His3000");
            }
        }
        private void txt_historiaclinica_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void CargarFormasPago()
        {
            if (txt_telef_Cliente.Enabled == false)
            {
                DataTable aux = NegFactura.FormasDePago(txt_Factura_Cod1.Text.Trim() + txt_Factura_Cod3.Text.Trim());
                gridFormasPago.Rows.Clear();
                foreach (DataRow dtRow in aux.Rows)
                {
                    gridFormasPago.Rows.Add(dtRow[0].ToString(), dtRow[1].ToString(), dtRow[2].ToString(), dtRow[3].ToString(), dtRow[4].ToString(), dtRow[5].ToString(), dtRow[6].ToString(), dtRow[7].ToString(), dtRow[8].ToString(), dtRow[9].ToString());
                }
            }
            else
            {
                DataTable aux = NegFactura.FormasDePago(txt_Factura_Cod1.Text.Trim() + txt_Factura_Cod3.Text.Trim());
                //gridFormasPago.Rows.Clear();
                foreach (DataRow dtRow in aux.Rows)
                {
                    gridFormasPago.Rows.Add(dtRow[0].ToString(), dtRow[1].ToString(), dtRow[2].ToString(), dtRow[3].ToString(), dtRow[4].ToString(), dtRow[5].ToString(), dtRow[6].ToString(), dtRow[7].ToString(), dtRow[8].ToString(), dtRow[9].ToString());
                }
            }
        }
        string FacturaDato_ = "";
        private void btnBuscarDatos_Click(object sender, EventArgs e)
        {
            if (NegParametros.ParametroFacturaFecha())
                dtpFechaFacturacion.Enabled = true;
            dtpFechaFacturacion.Value = DateTime.Now;
            if (facturaPrefactura != 2)
                incioFactura();
            ultraTabControl2.Tabs["descuento"].Visible = false;
            try
            {
                if (pacienteNuevo == false)
                {
                    frmAyudaPacientesFacturacion form = new frmAyudaPacientesFacturacion();
                    form.campoPadre = txt_Historia_Pc;
                    form.campoAtencion = txt_Atencion;
                    form.ShowDialog();
                    FacturaDato_ = form.FacturaDato;

                    if (txt_Atencion.Text != "")
                    {
                        txtNumeroAtencion.Text = NegFactura.RecuperaNumeroAtencion(Convert.ToInt64(txt_Atencion.Text));
                        //NegFactura.ActualizaDescuentoAtencion(Convert.ToInt16(txt_Atencion.Text));
                        NegValidaciones.alzheimer();// LIBERA MEMORIA
                    }
                    else
                        return;
                    if (txt_Historia_Pc.Text.Trim() != "")
                    {
                        pacienteFactura = NegPacientes.RecuperarPacienteID(form.campoPadre.Text.Trim());
                        cargarDatosPaciente(pacienteFactura);
                        habiltarBotones(false, true, false, true, true, true, true, true);
                        if (ultimaAtencion.ESC_CODIGO == 6 || ultimaAtencion.ESC_CODIGO == 7 || ultimaAtencion.ESC_CODIGO == 3)
                        {
                            facturaimprime.Enabled = true;
                            enviaHistoriaClinicaToolStripMenuItem.Enabled = true;
                            facturaElectronicaToolStripMenuItem.Enabled = true;
                            txtObserva.Text = ultimaAtencion.ATE_FACTURA_PACIENTE;
                            txtObserva.Enabled = false;
                            dtpFechaFacturacion.Enabled = false;
                        }
                        else
                        {
                            txtObserva.Enabled = true;
                            NegFactura.ArreglaIVABase(Convert.ToString(ultimaAtencion.ATE_CODIGO));
                        }
                        cargarDetalleFactura();
                        if (Convert.ToInt16(ultimaAtencion.ESC_CODIGO) == 6 || Convert.ToInt16(ultimaAtencion.ESC_CODIGO) == 7 || Convert.ToInt16(ultimaAtencion.ESC_CODIGO) == 3)
                        {
                            habiltarBotones(false, false, false, true, true, true, true, false);
                            btnSolicitar.Enabled = false;
                            btnAnticipos.Enabled = false;
                            btnGeneraValores.Enabled = false;
                            btnAltaPaciente.Enabled = false;
                            btnAgrupar.Enabled = false;
                            button1.Enabled = false;
                            cbx_FacturaNombre.Enabled = false;
                            txt_Ruc.Enabled = false;
                            txt_Cliente.Enabled = false;
                        }
                        else
                        {
                            habiltarBotones(false, true, false, true, true, true, true, true);
                            btnSolicitar.Enabled = true;
                            btnAnticipos.Enabled = true;
                            if (!mushuñan)
                                btnGeneraValores.Enabled = true;
                            btnAltaPaciente.Enabled = true;
                            btnAgrupar.Enabled = true;
                            button1.Enabled = true;
                            cbx_FacturaNombre.Enabled = true;
                            txt_Ruc.Enabled = true;
                            txt_Cliente.Enabled = true;
                        }
                        ///////alex/////recupera fecha de ingrso y alta///
                        Int32 atencion = Convert.ToInt32(this.txt_Atencion.Text.Trim());
                        if (atencion > 0)
                        {
                            DataTable auxDT = NegFactura.fechasINOUT(atencion);
                            foreach (DataRow row in auxDT.Rows)
                            {
                                cboTipoDescuento.Items.Add(row[0].ToString());
                                this.dtp_FechaIngreso.Text = row[0].ToString();
                                if (String.IsNullOrEmpty(row[1].ToString()))
                                {
                                    this.txt_fechaalta.Text = "";
                                }
                                else
                                {
                                    this.txt_fechaalta.Text = row[1].ToString();
                                }
                            }
                        }
                        CargarFormasPago();
                        //HabilitarBontones2();

                    }
                }
                //CAMBIOS EDGAR RAMOS PARA BLOQUEAR A USUARIO DE ATENCION AL CLIENTE
                string departamento;
                departamento = Convert.ToString(His.Entidades.Clases.Sesion.codDepartamento);
                if (departamento == "15")
                {
                    btnGuardar.Enabled = false;
                    btnImprimir.Enabled = false;
                    btnGeneraValores.Enabled = false;
                    btnSolicitar.Enabled = false;
                    btnAgrupar.Enabled = false;
                    button1.Enabled = false;
                    btnAnticipos.Enabled = false;
                    btnAltaPaciente.Enabled = false;
                }
                //else
                //{
                //    btnGuardar.Enabled = true;
                //    btnImprimir.Enabled = true;
                //    btnGeneraValores.Enabled = true;
                //    btnSolicitar.Enabled = true;
                //    btnAgrupar.Enabled = true;
                //    button1.Enabled = true;
                //    btnAnticipos.Enabled = true;
                //    btnAltaPaciente.Enabled = true;
                //}
                //if (Convert.ToInt16(ultimaAtencion.ESC_CODIGO) == 6)
                //{
                //    His.Honorarios.Datos.Atencion obj_atencion = new His.Honorarios.Datos.Atencion();
                //    DataTable totalesSic = new DataTable();
                //    totalesSic = obj_atencion.TotalesSIC(txt_Factura_Cod1.Text.Trim() + txt_Factura_Cod3.Text.Trim());
                //    decimal val = Math.Round(Convert.ToDecimal(totalesSic.Rows[0][0].ToString()), 2);
                //    txt_SubTotal.Text = val.ToString();
                //    val = Math.Round(Convert.ToDecimal(totalesSic.Rows[0][1].ToString()), 2);
                //    txt_Descuento.Text = val.ToString();
                //    val = Math.Round(Convert.ToDecimal(totalesSic.Rows[0][2].ToString()), 2);
                //    txt_SinIVA.Text = val.ToString();
                //    val = Math.Round(Convert.ToDecimal(totalesSic.Rows[0][3].ToString()), 2);
                //    txt_ConIVA.Text = val.ToString();
                //    val = Math.Round(Convert.ToDecimal(totalesSic.Rows[0][4].ToString()), 2);
                //    txt_IVA.Text = val.ToString();
                //    val = Math.Round(Convert.ToDecimal(totalesSic.Rows[0][5].ToString()), 2);
                //    txt_Total.Text = val.ToString();
                //}
                CargarTiposDesuento();
                if (CuentaAgrupada())
                {
                    detalleCuentasAgrupadasToolStripMenuItem.Visible = true;
                    toolStripMenuItem2.Visible = false;
                    toolStripMenuItem3.Visible = false;
                    detallePorAreaToolStripMenuItem.Visible = false;
                }
                else
                {
                    detalleCuentasAgrupadasToolStripMenuItem.Visible = false;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                //MessageBox.Show("Algo ocurrio al cargar datos de paciente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txt_historiaclinica_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            incioFactura();
            limpiarCampos();
            txt_Atencion.Text = "";
            txt_Historia_Pc.Text = "";
            txtNumeroAtencion.Text = "";
            txt_Historia_Pc.Text = "";
            txt_Telef_P.Text = "";
            txt_RucPaciente.Text = "";
            lblCategoria.Text = "";
            lblConvenio.Text = "";
            if (gridFormasPago.Rows.Count >= 2)
            {
                gridFormasPago.Rows.Clear();

            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            NegValidaciones.alzheimer();
            this.Dispose();
        }
        private void txt_Historia_Pc_TextChanged(object sender, EventArgs e)
        {

        }
        private void gridFormasPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == (Char)Keys.Enter)
                {
                    e.Handled = true;
                    gridFormasPago.CurrentCell = gridFormasPago[2, gridFormasPago.CurrentRow.Index];
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Algo ocurrio con la forma de pago", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        private void txt_ApellidoH1_TextChanged(object sender, EventArgs e)
        {

        }
        private void cbx_FacturaNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            FacturaNombre(Convert.ToString(cbx_FacturaNombre.SelectedItem));
        }

        private void FacturaNombre(string dato)
        {
            bool estado = false;

            if (dato == "PACIENTE")
            {
                DtoPacientes pac = new DtoPacientes();
                pac = NegPacientes.RecuperarDtoPacienteID(Convert.ToInt32(ultimaAtencion.PACIENTESReference.EntityKey.EntityKeyValues[0].Value));
                txt_Ruc.Text = pac.PAC_IDENTIFICACION;
                txt_Direc_Cliente.Text = pac.PAC_DIRECCION.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
                txt_Cliente.Text = pac.PAC_APELLIDO_PATERNO + " " + pac.PAC_APELLIDO_MATERNO + " " + pac.PAC_NOMBRE1 + " " + pac.PAC_NOMBRE2;
                txt_telef_Cliente.Text = pac.PAC_TELEFONO2;
                txtEmailFactura.Text = pac.PAC_EMAIL;
                estado = true;
                btnBuscaClienteSic.Visible = false;
            }
            else if (dato == "CONVENIO DE PAGO")
            {
                if (ultimaAtencion.ATE_ACOMPANANTE_CEDULA != "")
                {
                    txt_Ruc.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
                    txt_Direc_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
                    txt_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                    txt_telef_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO;
                    estado = true;
                    btnBuscaClienteSic.Visible = false;
                    if (txt_Ruc.Text == "")
                    {
                        MessageBox.Show("Datos De CONVENIO DE PAGO No Fueron Ingresados En La ADMISION", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (dato == "GARANTE")
            {
                if (ultimaAtencion.ATE_GARANTE_CEDULA != "")
                {
                    txt_Ruc.Text = ultimaAtencion.ATE_GARANTE_CEDULA;
                    txt_Direc_Cliente.Text = ultimaAtencion.ATE_GARANTE_DIRECCION.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
                    txt_Cliente.Text = ultimaAtencion.ATE_GARANTE_NOMBRE;
                    txt_telef_Cliente.Text = ultimaAtencion.ATE_GARANTE_TELEFONO;
                    estado = true;
                    btnBuscaClienteSic.Visible = false;
                    if (txt_Ruc.Text == "")
                    {
                        MessageBox.Show("Datos De GARANTE No Fueron Ingresados En La ADMISION", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (dato == "EMPRESA")
            {
                if (datosPacienteActual.EMP_CODIGO != null)
                {
                    ASEGURADORAS_EMPRESAS lista = new ASEGURADORAS_EMPRESAS();
                    lista = NegAseguradoras.RecuperaEmpresa(Convert.ToInt16(datosPacienteActual.EMP_CODIGO));
                    if (lista != null)
                    {
                        cargarEmpresa(lista.ASE_RUC);
                        estado = true;
                        btnBuscaClienteSic.Visible = false;
                    }
                    else
                        MessageBox.Show("Datos De EMPRESA No Fueron Ingresados En La ADMISION", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Datos De EMPRESA No Fueron Ingresados En La ADMISION", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (dato == "CLIENTE")
            {
                btnBuscaClienteSic.Visible = true;
            }
            else if (dato == "OTROS")
            {
                if (ultimaAtencion.ATE_ACOMPANANTE_CEDULA != "")
                {
                    txt_Ruc.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
                    txt_Direc_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
                    txt_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                    txt_telef_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO;
                    txtEmailFactura.Text = NegAtenciones.PacienteDatosAdicionales(ultimaAtencion.ATE_CODIGO);
                    estado = true;
                    btnBuscaClienteSic.Visible = false;
                    if (txt_Ruc.Text == "")
                    {
                        MessageBox.Show("Datos De OTROS No Fueron Ingresados En La ADMISION", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }
        private void gridDetalleFactura_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }
        private void btnDetalleCuenta_Click(object sender, EventArgs e)
        {
            frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + '-' + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "FACTURA", "", 0, "", "", 0, 0, "", "", "", lblConvenio.Text, "", "", "", "");
            DesgloseFactura.Show();

        }
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (facturaPrefactura == 2)
                if (MessageBox.Show("Esta Por Generar Una PRE-FACTURA, ¿Desa Continuar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.No)
                    return;
                else
                    NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(ultimaAtencion.ATE_CODIGO, 3, Sesion.codUsuario, "FACTURACION-GUARDAR");
            txt_telef_Cliente.Text = txt_telef_Cliente.Text.ToString().Trim();
            //if (txt_telef_Cliente.TextLength > 10)
            //{
            //    AgregarError(txt_telef_Cliente);
            //    MessageBox.Show("NRO TELEFONICO NO PUEDE SER MAYOR A 10", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            if (chbCopago.Checked)
            {
                if (!ultraTabControl2.Tabs["dividirFactura"].Visible)
                {
                    MessageBox.Show("TIENE UN COPAGO PENDIENTE POR FACTURA SE DEBE DIVIDIR LA CUENTA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    for (int i = 0; i < dgvDivideFactura.Rows.Count; i++)
                    {
                        if (dgvDivideFactura.Rows[i].Cells[0].Value.ToString() == "46")
                        {
                            if (dgvDivideFactura.Rows[i].Cells["noFactura"].Value != null || Convert.ToBoolean(dgvDivideFactura.Rows[i].Cells["noFactura"].Value))
                            {
                                MessageBox.Show("TIENE UN COPAGO PENDIENTE POR FACTURA SE DEBE DIVIDIR LA CUENTA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        }
                    }
                }
            }
            if (txtEmailFactura.Text == "")
            {
                AgregarError(txtEmailFactura);
                MessageBox.Show("INGRESE CORREO ELECTRONICO", "HIS3000");
            }
            if (chbConsumidorFinal.Checked == true)
            {
                if ((MessageBox.Show("ESTA SEGURO DE FACTURAR A CONSUMIDOR FINAL????", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) == DialogResult.Yes)
                    GrabaFactura();
            }
            else if (chk_Cedula.Checked == true || chk_Ruc.Checked == true)
            {
                if (txt_Ruc.Text.Length == 10 && chk_Cedula.Checked == true)
                    GrabaFactura();
                else if (txt_Ruc.Text.Length == 13 && chk_Ruc.Checked == true)
                    GrabaFactura();
                else
                {
                    AgregarError(txt_Ruc);
                    AgregarError(gbxIdentificadores);
                    MessageBox.Show("TIPO IDENTIFICADOR NO CORRESPONDE", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (chk_pasaporte.Checked == true)
            {
                if ((MessageBox.Show("TIPO DE IDENTIFICACIÓN ES PASAPORTE???", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                    GrabaFactura();
            }
            else
            {
                MessageBox.Show("MARQUE UN TIPO DE INDENTIFICACIÓN PARA CONTINUAR", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                AgregarError(gbxIdentificadores);
            }
        }
        private void GrabaFactura()
        {
            if (validarFormaPagos())
            {
                if (chk_pasaporte.Checked == true)
                {
                    guardarFactura();
                }
                else
                {
                    if (validarCedula_Factura() == 0)  // valida la cedula para que no deje guardar si es incorrecta
                    {
                        MessageBox.Show("El numero de cedula ingresado es incorrecto.");
                        return;
                    }
                    guardarFactura();
                }
            }
            else
            {
                ultraTabControl2.SelectedTab = ultraTabControl2.Tabs["formaPago"];
                AgregarError(gridFormasPago);
                MessageBox.Show("Ingrese Datos Correctos en Formas de Pago");
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmCuentaPaciente frm = new frmCuentaPaciente(pacienteFactura.PAC_CODIGO);
            frm.Show();
        }
        private void gridDetalleFactura_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3)
                {
                    if (VerificaAltaPaciente())
                    {
                        e.Handled = true;
                        ultraTabControl2.SelectedTab = ultraTabControl2.Tabs["formaPago"];
                        SendKeys.SendWait("{TAB}");
                    }
                    else
                    {
                        MessageBox.Show("El Paciente Debe Estar Dado El Alta Para Llenar Formas de Pago", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (e.KeyCode == Keys.F4)
                {
                    try
                    {
                        e.Handled = true;
                        dgvDescuento.Visible = true;
                        dgvDescuento.Focus();
                        ultraTabControl2.Tabs["descuento"].Visible = true;
                        CargarTiposDesuento();
                        DataTable OrdenaRubros = new DataTable();
                        OrdenaRubros.Columns.Add("RUBRO");
                        OrdenaRubros.Columns.Add("DETALLE");
                        OrdenaRubros.Columns.Add("IVA");
                        OrdenaRubros.Columns.Add("TOTAL CON IVA");
                        OrdenaRubros.Columns.Add("TOTAL SIN IVA");
                        OrdenaRubros.Columns.Add("% DESCUENTO");
                        OrdenaRubros.Columns.Add("DESCUENTO TOTAL");
                        for (Int16 f = 0; f < gridDetalleFactura.Rows.Count; f++)
                        {
                            DataRow row = OrdenaRubros.NewRow();
                            row["RUBRO"] = Convert.ToString(gridDetalleFactura.Rows[f].Cells[0].Value);
                            row["DETALLE"] = Convert.ToString(gridDetalleFactura.Rows[f].Cells[1].Value);
                            row["IVA"] = Convert.ToString(Convert.ToDouble(gridDetalleFactura.Rows[f].Cells["IVA"].Value));
                            row["TOTAL CON IVA"] = Convert.ToString(Convert.ToDouble(gridDetalleFactura.Rows[f].Cells["TOTAL CON IVA"].Value));
                            row["TOTAL SIN IVA"] = Convert.ToString(Convert.ToDouble(gridDetalleFactura.Rows[f].Cells["TOTAL SIN IVA"].Value));
                            row["% DESCUENTO"] = "0.00";
                            row["DESCUENTO TOTAL"] = "0.00";
                            OrdenaRubros.Rows.Add(row);
                        }
                        dgvDescuento.DataSource = OrdenaRubros;
                        dgvDescuento.Columns["RUBRO"].Width = 80;
                        dgvDescuento.Columns["DETALLE"].Width = 400;
                        dgvDescuento.Columns["IVA"].Width = 80;
                        dgvDescuento.Columns["TOTAL CON IVA"].Width = 80;
                        dgvDescuento.Columns["TOTAL SIN IVA"].Width = 80;
                        dgvDescuento.Columns["% DESCUENTO"].Width = 80;
                        dgvDescuento.Columns["DESCUENTO TOTAL"].Width = 80;
                        dgvDescuento.Columns["RUBRO"].ReadOnly = true;
                        dgvDescuento.Columns["DETALLE"].ReadOnly = true;
                        dgvDescuento.Columns["IVA"].ReadOnly = true;
                        dgvDescuento.Columns["TOTAL CON IVA"].ReadOnly = true;
                        dgvDescuento.Columns["TOTAL SIN IVA"].ReadOnly = true;
                        dgvDescuento.Columns["% DESCUENTO"].ReadOnly = true;
                        dgvDescuento.Columns["DESCUENTO TOTAL"].ReadOnly = true;
                        dgvDescuento.CurrentCell = dgvDescuento.CurrentRow.Cells[5];
                        dgvDescuento.AllowUserToAddRows = false;
                        ultraTabControl2.SelectedTab = ultraTabControl2.Tabs["descuento"];
                        SendKeys.SendWait("{TAB}");

                        if (chbCopago.Checked)
                        {
                            dgvDescuento.Columns["% DESCUENTO"].ReadOnly = false;
                            button2.Visible = false;
                            cboTipoDescuento.Visible = false;
                            label47.Visible = false;
                        }
                        else if (chbCopago.Checked == false)
                        {
                            dgvDescuento.Columns["% DESCUENTO"].ReadOnly = true;
                            button2.Visible = true;
                            cboTipoDescuento.Visible = true;
                            label47.Visible = true;
                        }
                    }
                    catch (Exception)
                    {
                        ultraTabControl2.Tabs["descuento"].Visible = false;
                        MessageBox.Show("NO SE PUEDE APLICAR DESCUENTO SIN RUBRO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (e.KeyCode == Keys.F11)
                {
                    try
                    {
                        if (txt_Descuento.Text == "0" || txt_Descuento.Text == "0.00")
                        {
                            dgvDivideFactura.Columns.Clear();
                            e.Handled = true;
                            ultraTabControl2.Tabs["dividirFactura"].Visible = true;
                            DataTable divideFactura = new DataTable();
                            DataGridViewCheckBoxColumn NoFactura = new DataGridViewCheckBoxColumn();
                            NoFactura.TrueValue = 1;
                            NoFactura.ThreeState = false;
                            NoFactura.Name = "noFactura";
                            NoFactura.HeaderText = "NO FACTURA";
                            divideFactura.Columns.Add("RUBRO");
                            divideFactura.Columns.Add("DETALLE");
                            divideFactura.Columns.Add("IVA");
                            divideFactura.Columns.Add("TOTAL CON IVA");
                            divideFactura.Columns.Add("TOTAL SIN IVA");
                            divideFactura.Columns.Add("DIVIDE FACTURA");


                            for (Int16 f = 0; f < gridDetalleFactura.Rows.Count; f++)
                            {
                                DataRow row = divideFactura.NewRow();
                                row["RUBRO"] = Convert.ToString(gridDetalleFactura.Rows[f].Cells[0].Value);
                                row["DETALLE"] = Convert.ToString(gridDetalleFactura.Rows[f].Cells[1].Value);
                                row["IVA"] = gridDetalleFactura.Rows[f].Cells["IVA"].Value.ToString();
                                row["TOTAL CON IVA"] = Convert.ToString(gridDetalleFactura.Rows[f].Cells["TOTAL CON IVA"].Value);
                                row["TOTAL SIN IVA"] = Convert.ToString(gridDetalleFactura.Rows[f].Cells["TOTAL SIN IVA"].Value);
                                row["DIVIDE FACTURA"] = "N";
                                divideFactura.Rows.Add(row);
                            }
                            dgvDivideFactura.DataSource = divideFactura;
                            dgvDivideFactura.Columns["RUBRO"].Width = 80;
                            dgvDivideFactura.Columns["DETALLE"].Width = 400;
                            dgvDivideFactura.Columns["IVA"].Width = 80;
                            dgvDivideFactura.Columns["TOTAL CON IVA"].Width = 80;
                            dgvDivideFactura.Columns["TOTAL SIN IVA"].Width = 80;
                            dgvDivideFactura.Columns["DIVIDE FACTURA"].Width = 80;
                            dgvDivideFactura.Columns["DIVIDE FACTURA"].Visible = false;
                            dgvDivideFactura.Columns["RUBRO"].ReadOnly = true;
                            dgvDivideFactura.Columns["DETALLE"].ReadOnly = true;
                            dgvDivideFactura.Columns["IVA"].ReadOnly = true;
                            dgvDivideFactura.Columns["TOTAL CON IVA"].ReadOnly = true;
                            dgvDivideFactura.Columns["TOTAL SIN IVA"].ReadOnly = true;
                            dgvDivideFactura.Columns["DIVIDE FACTURA"].ReadOnly = true;
                            dgvDivideFactura.Columns.Add(NoFactura);
                            dgvDivideFactura.CurrentCell = dgvDivideFactura.CurrentRow.Cells[2];
                            dgvDivideFactura.AllowUserToAddRows = false;
                            ultraTabControl2.SelectedTab = ultraTabControl2.Tabs["dividirFactura"];
                            SendKeys.SendWait("{TAB}");
                        }
                        else
                        {
                            MessageBox.Show("NO SE PUEDE DIVIDIR CUENTA CUANDO HAY DESCUENTOS", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("ACCION NO PERMITIDA ", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                if (e.KeyCode == Keys.F10)
                {
                    if (ultimaAtencion.ESC_CODIGO != 6)
                    {
                        e.Handled = true;
                        frmAuditoriaCuenta frm = new frmAuditoriaCuenta(txt_Atencion.Text, txt_Historia_Pc.Text, txt_ApellidoH1.Text, lblCategoria.Text);
                        frm.ShowDialog();
                        cargarDetalleFactura();
                    }
                    else
                    {
                        MessageBox.Show("No se puede dividir una cuenta ya facturada", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                //Cambios Edgar 20210315 comentado porque no se sabe que funcion tiene. Y porque lanza un error.
                //if (e.KeyCode == Keys.F5)
                //{
                //    frmRubrosFactura form = new frmRubrosFactura(Convert.ToDouble(txt_Total.Text.Trim()), nuevaFactura,
                //                                                 listaRubros);
                //    form.ShowDialog();
                //    producto = form.producto;
                //    if (producto != null)
                //    {
                //        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
                //        cuentas.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                //        cuentas.CUE_FECHA = DateTime.Now;
                //        cuentas.CUE_DETALLE = producto.PRO_DESCRIPCION;
                //        cuentas.CUE_VALOR = producto.PRO_PRECIO;
                //        cuentas.CUE_IVA = producto.PRO_IVA;
                //        cuentas.CUE_ESTADO = 1;
                //        cuentas.CUE_NUM_FAC = "0";
                //        cuentas.CUE_CANTIDAD = 1;
                //        cuentas.RUB_CODIGO = 26;
                //        cuentas.PED_CODIGO = 0;
                //        cuentas.ID_USUARIO = 1;
                //        NegCuentasPacientes.CrearCuenta(cuentas);
                //        cargarDetalleFactura();
                //    }
                //}
                if (e.KeyCode == Keys.F8)
                {
                    e.Handled = true;
                    txtObserva.Visible = true;
                    txtObserva.Text = "";
                    txtObserva.Text = "DIAGNOSTICO: ";
                    txtObserva.Focus();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gridFormasPago_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int columna = gridFormasPago.SelectedCells[0].ColumnIndex;
                int fila = gridFormasPago.CurrentRow.Index;
                verificavalor = Convert.ToString(gridFormasPago.Rows[fila].Cells[5].Value);
                if (e.KeyCode == Keys.F1)
                {
                    if (columna == 0)
                    {
                        if (VerificaAltaPaciente())
                        {
                            string codigo = "0";
                            if (gridFormasPago.Rows.Count > 0)
                                codigo = Convert.ToString(gridFormasPago.Rows.Count);
                            frmAyudaFormasPagos form = new frmAyudaFormasPagos();
                            form.ShowDialog();

                            if (form.codigoFormaPago != null)
                            {
                                var nuevoCodigo = form.codigoFormaPago.ToString();
                                if (codigo != nuevoCodigo)
                                {
                                    gridFormasPago.Rows[fila].Cells[5].Value = "";
                                }
                                DataGridViewRow dt = new DataGridViewRow();
                                dt.CreateCells(gridFormasPago);
                                dt.Cells[0].Value = "" /*formaPago.FOR_CODIGO*/;
                                dt.Cells[1].Value = ""/*formaPago.FOR_DESCRIPCION*/;
                                dt.Cells[2].Value = "";
                                dt.Cells[3].Value = "";
                                dt.Cells[5].Value = "";
                                dt.Cells[9].Value = "";
                                if (fila >= 1)
                                    gridFormasPago.Rows.Add(dt);
                                gridFormasPago.Rows[fila].Cells[0].Value = form.codigoFormaPago;
                                gridFormasPago.Rows[fila].Cells[1].Value = form.nombreFormaPago;
                                gridFormasPago.Rows[fila].Cells[3].Value = DateTime.Today.AddDays(1);
                                cargarTotales(fila);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El Paciente Debe Estar Dado El Alta Para Llenar Formas de Pago", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else if (columna == 4)
                    {
                        frmAyudaBancos form = new frmAyudaBancos();
                        form.ShowDialog();
                        //alexalex
                        if (form.bancoElegido != "CANCELA ACCION")
                        {
                            this.gridFormasPago.Rows[gridFormasPago.CurrentRow.Index].Cells[4].Value = form.bancoElegido;
                        }
                    }
                    else if (columna == 5)
                    {
                        if (gridFormasPago.Rows[fila].Cells[0].Value.ToString() != "6")
                        {
                            frmAyudaPlazoPago form = new frmAyudaPlazoPago(Convert.ToInt32(gridFormasPago.Rows[fila].Cells[0].Value.ToString()));
                            try
                            {
                                form.ShowDialog();
                            }
                            catch
                            {
                                gridFormasPago.Rows[fila].Cells[5].Value = "C";
                            }
                            try
                            {


                                if (form.codigoFormaPago != null)
                                {
                                    gridFormasPago.Rows[fila].Cells[5].Value = form.nombreFormaPago;
                                    if (gridFormasPago.Rows.Count != 2)
                                    {
                                        cargarTotales(fila);
                                    }

                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                        else
                            MessageBox.Show("Forma De Pago Efectivo No Requiere Plazo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                if (e.KeyCode == Keys.F4)
                {
                    int contadorpago = Convert.ToInt16(gridFormasPago.Rows.Count) - 1;
                    for (Int16 i = 0; i < contadorpago; i++)
                    {
                        int verificador = NegFactura.PlazoPagoSicVerifica(gridFormasPago.Rows[i].Cells[5].Value.ToString());
                        if (verificador == 0)
                        {
                            MessageBox.Show("Plan Pago A Diferir No Existe", "ERROR");
                            return;
                        }
                    }
                    btnGuardar.PerformClick();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Algo ocurrio al presionar dentro de la tabla formas de pago.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    e.Handled = true;
                    //dar un nombre a los controles
                    txtObserva.Visible = true;
                    txtObserva.Text = "";
                    txtObserva.Text = "DIAGNOSTICO: ";
                    txtObserva.Focus();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Algo ocurrio al querer agregar el Dignostico", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RecuperaClienteSIC()
        {
            DataTable OtroCliente = new DataTable();
            OtroCliente = NegDetalleCuenta.RecuperaOtroCliente(txt_Ruc.Text);
            foreach (DataRow Item in OtroCliente.AsEnumerable())
            {
                txt_Cliente.Text = Item["nomcli"].ToString();
                txt_telef_Cliente.Text = Item["telcli"].ToString().Trim();
                txt_Direc_Cliente.Text = Item["dircli"].ToString();
                txtEmailFactura.Text = Item["email"].ToString();
            }
        }
        private void txt_Ruc_Leave(object sender, EventArgs e)
        {
            if (!consuktado)
            {
                if (chk_pasaporte.Checked == true)
                    MessageBox.Show("Recuerde Que Esta Buscando Como Pasaporte Al Cliente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);




                if (chk_Cedula.Checked || chk_Ruc.Checked)
                {
                    if (txt_Ruc.Text != "")
                    {
                        ValidaNube ok = new ValidaNube();
                        ok = NegUtilitarios.ValidarDocumento(txt_Ruc.Text);
                        if (!ok.ok)
                        {
                            MessageBox.Show("Número de identifación incorrecto.", "HIS3000", MessageBoxButtons.OK);
                            txt_Ruc.Focus();
                        }
                        else
                        {
                            RecuperaClienteSIC();
                        }
                    }

                }
                else
                {
                    RecuperaClienteSIC();
                }
            }
            else
            {
                consuktado = false;
            }
        }
        private void txt_Ruc_TextChanged(object sender, EventArgs e)
        {

        }
        private void chb_NombreFactura_CheckedChanged(object sender, EventArgs e)
        {
            gridFormasPago.Rows.Clear();
            DataGridViewRow dt = new DataGridViewRow();
            dt.CreateCells(gridFormasPago);
            if (chb_NombreFactura.Checked == true)
            {
                cbx_FacturaNombre.Enabled = false;
                txt_Ruc.Text = string.Empty;
                txt_Direc_Cliente.Text = string.Empty;
                txt_Cliente.Text = string.Empty;
                txt_telef_Cliente.Text = string.Empty;
                cbx_FacturaNombre.SelectedIndex = 0;
                dt.Cells[0].Value = "6" /*formaPago.FOR_CODIGO*/;
                dt.Cells[1].Value = "EFECTIVO"/*formaPago.FOR_DESCRIPCION*/;
                dt.Cells[2].Value = this.txt_Total.Text;
                dt.Cells[3].Value = DateTime.Today.AddDays(1);
                dt.Cells[5].Value = "0";
                dt.Cells[9].Value = "";
                gridFormasPago.Rows.Add(dt);

            }
            else
            {
                cbx_FacturaNombre.Enabled = true;
                txt_Ruc.Text = txt_RucPaciente.Text;
                txt_Direc_Cliente.Text = txt_Direccion_P.Text;
                txt_telef_Cliente.Text = txt_Telef_P.Text;
                txt_Cliente.Text = txt_ApellidoH1.Text;
                dt.Cells[0].Value = "6" /*formaPago.FOR_CODIGO*/;
                dt.Cells[1].Value = "EFECTIVO"/*formaPago.FOR_DESCRIPCION*/;
                dt.Cells[2].Value = this.txt_Total.Text;
                dt.Cells[3].Value = DateTime.Today.AddDays(1);
                dt.Cells[5].Value = "0";
                dt.Cells[9].Value = txt_Cliente.Text;
                gridFormasPago.Rows.Add(dt);
            }
        }
        bool consuktado = false;
        private void txt_Ruc_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (chk_pasaporte.Checked == false)
                if ((Char.IsDigit(e.KeyChar)) || (Char.IsControl(e.KeyChar)) || (Char.IsSeparator(e.KeyChar)))
                    e.Handled = false;
                else
                    e.Handled = true;

            if (e.KeyChar == (char)Keys.Enter)
            {

                CONTROL_CONSULTA obj = new CONTROL_CONSULTA();
                obj.ip = Sesion.ip;
                obj.usuario = Sesion.codUsuario;
                obj.fechaConsulta = DateTime.Now;
                obj.identificacion = txt_Ruc.Text;
                RegistroCivil registroCivil = new RegistroCivil();
                PARAMETROS_DETALLE parametroWEB = new PARAMETROS_DETALLE();
                parametroWEB = NegParametros.RecuperaPorCodigo(48);
                if ((bool)parametroWEB.PAD_ACTIVO)
                {
                    //if (NegNumeroControl.CreaControlConsulta(obj))
                    //{
                    int aux = 0;
                    if (txt_Ruc.Text.Length == 13)
                    {
                        RUC sri = new RUC();
                        sri = NegUtilitarios.ObtenerRUC(txt_Ruc.Text);
                        if (sri != null)
                        {
                            if (sri.ok)
                            {
                                txt_Cliente.Text = sri.consulta[0].razonSocial;
                                txt_Direc_Cliente.Text = "";
                                txtEmailFactura.Text = "";
                                txt_telef_Cliente.Text = "";
                                obj.tipoConsulta = "Facturación: SRI";
                                obj.resultado = sri.consulta[0].razonSocial;
                                NegNumeroControl.CreaControlConsulta(obj);
                                consuktado = true;
                            }
                            else
                            {
                                MessageBox.Show("RUC Incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                        }
                        else
                        {
                            MessageBox.Show("No se puede validar datos en la nube, realice validacion de forma manual!!! o vuelva a intentar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else if (txt_Ruc.Text.Length == 10)
                    {
                        registroCivil = NegUtilitarios.ObtenerRegistroCivil(txt_Ruc.Text);
                        if (registroCivil == null)
                            return;
                        if (registroCivil.consulta.Nombre != null)
                        {
                            if (!registroCivil.ok)
                            {
                                MessageBox.Show("Cédula Incorrecta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //if (registroCivil.consulta.fechaDefuncion == "")
                            //{
                            //    MessageBox.Show("Cidadano ya fallecido en: " + registroCivil.consulta.fechaDefuncion, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            txt_Cliente.Text = registroCivil.consulta.Nombre;
                            txt_Direc_Cliente.Text = registroCivil.consulta.CalleDomicilio + " " + registroCivil.consulta.NumeracionDomicilio;
                            txtEmailFactura.Text = "";
                            txt_telef_Cliente.Text = "";
                            obj.tipoConsulta = "Facturación: Reg Civil";
                            obj.resultado = registroCivil.consulta.Nombre;
                            NegNumeroControl.CreaControlConsulta(obj);
                            consuktado = true;
                        }
                        else
                        {
                            MessageBox.Show("Cédula incorrecta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Identificación Incorrecta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se puede validar datos en la nube, no tiene contratado este plan!!! o vuelva a intentar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }

        }
        private void txt_Cliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }
        private void txt_telef_Cliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar) < 48 && e.KeyChar != 8) || e.KeyChar > 57)
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    if ((Char.IsDigit(e.KeyChar)) || (Char.IsControl(e.KeyChar)) || (Char.IsSeparator(e.KeyChar)))
                        e.Handled = false;
                    else
                        e.Handled = true;
                    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                    {
                        e.Handled = true;
                        SendKeys.SendWait("{TAB}");
                    }
                    if (txt_telef_Cliente.TextLength > 10)
                    {
                        AgregarError(txt_telef_Cliente);
                    }
                }
                else if (e.KeyChar != 46 && e.KeyChar != 13 && e.KeyChar != 9)
                {
                    MessageBox.Show("Sólo se permiten Números");
                    e.Handled = true;
                }
            }
        }
        private void txt_Direc_Cliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {

            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
        private void btnSolicitar_Click(object sender, EventArgs e)
        {

            if (NegFactura.AgrupacionCuentas(Convert.ToInt64(txt_Atencion.Text)))
            {
                if (Convert.ToDouble(txt_Descuento.Text) > 0)
                {
                    DialogResult result = MessageBox.Show("La Siguiente Acción Eliminara Los Descuentos Ingresados. ¿Desea continuar?", "His3000", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        //encera descuentos
                        NegFactura.ActualizaDescuentoAtencion(Convert.ToInt64(txt_Atencion.Text));
                        ultraTabControl2.Tabs["descuento"].Visible = false;
                        cargaSolicitud();
                    }
                }
                else
                {
                    ultraTabControl2.Tabs["descuento"].Visible = false;
                    cargaSolicitud();
                }
                //His.Parametros.FacturaPAR.BodegaPorDefecto = 1; // Se comenta por que se comienza a trabajar por Ip-Bodega // Mario 15/02/2023
            }
            else
            {
                MessageBox.Show("Esta es una cuenta de agrupacion, por lo cual no puede solicitar rubros", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void cargaSolicitud()
        {
            ///abre dialog con opciones
            var archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
            byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
            if (codigoEstacion <= 0)
            {
                MessageBox.Show("No se encuentra asignada una estación", "Inf");
                return;
            }
            if (this.cmb_Areas.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor seleccione el area a la que realiza el pedido..");
                return;
            }
            PEDIDOS_ESTACIONES pe = new PEDIDOS_ESTACIONES();
            pe = (PEDIDOS_ESTACIONES)this.cmbEstaciones.SelectedItem;
            var Estacion = (Byte)pe.PEE_CODIGO;
            PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
            RUBROS Rubro1 = new RUBROS();
            Rubro1 = (RUBROS)cmb_Rubros.SelectedItem;
            ar = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
            var Rubro = (Int16)Rubro1.RUB_CODIGO;
            if (Convert.ToInt32(cmb_Areas.SelectedValue) != 18)
            {
                CargaPaciente();
            }
            else
            {
                frmAyudaCategorias form = new frmAyudaCategorias(ultimaAtencion.ATE_CODIGO, CodigoTipoEmpresa);
                form.ShowDialog();
                cargarDetalleFactura();
            }
        }
        private void cmb_Areas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmb_Rubros.DataSource = null;
                if (cmb_Areas.SelectedItem != null)
                {
                    PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
                    List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
                    if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
                        cmb_Rubros.DataSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
                    else
                        cmb_Rubros.DataSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();

                    cmb_Rubros.DisplayMember = "RUB_NOMBRE".Trim();
                    cmb_Rubros.ValueMember = "RUB_CODIGO";
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Algo ocurrio al cambiar de area/subarea.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion       
        private Boolean ConexionAcces(string Cadena)
        {
            OleDbConnection conexion = new OleDbConnection();
            conexion.ConnectionString = "Data Source = C:\\Sic3000\\" + DirectorioBaseSic3000 + "Sic3000.mdb; user Id = admin; provider = microsoft.jet.oledb.4.0";
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = conexion;
            comando.CommandText = Cadena;
            DataTable tabla = new DataTable();
            OleDbDataAdapter adaptador = new OleDbDataAdapter();
            adaptador.SelectCommand = comando;
            try
            {
                adaptador.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    if (tabla.Rows[0]["codcon"].ToString() == "72")
                    {
                        DataTable numeroFacturaSQL = new DataTable();
                        numeroFacturaSQL = NegFactura.RecuperaNumeroFactura(Convert.ToInt16(Caja()));
                        NumeroFactura = "";
                        int totalCaracteres = numeroFacturaSQL.Rows[0][0].ToString().Length;
                        for (int i = totalCaracteres; i < 9; i++)
                        {
                            NumeroFactura += "0";
                        }
                        NumeroFactura += numeroFacturaSQL.Rows[0][0].ToString();
                    }
                    else
                    {
                        NumeroFactura = tabla.Rows[0]["numcon"].ToString();
                    }
                    //NumeroFactura = numeroFacturaSQL.Rows[0]["numcon"].ToString();
                    NumeroCaja = Caja();
                    num_caja = Convert.ToInt32(NumeroCaja);
                    if (VerificaNumeroFactura(NumeroFactura, NumeroCaja) == true)
                    {
                        txt_Factura_Cod3.Text = NumeroFactura;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio dentro de la conexion Access.");
                return false;
            }
        }
        private Int64 ConexionAccesPrefactura(string Cadena)
        {
            OleDbConnection conexion = new OleDbConnection();
            conexion.ConnectionString = "Data Source = C:\\Sic3000\\" + DirectorioBaseSic3000 + "\\Sic3000.mdb; user Id = admin; provider = microsoft.jet.oledb.4.0";
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = conexion;
            comando.CommandText = Cadena;
            DataTable tabla = new DataTable();
            OleDbDataAdapter adaptador = new OleDbDataAdapter();
            adaptador.SelectCommand = comando;
            try
            {
                adaptador.Fill(tabla);
                if (tabla.Rows.Count > 0)
                {
                    return Convert.ToInt64(tabla.Rows[0][0].ToString());
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio con la conexion Access prefactura. Consulte con el Administrador.");
                return 0;
            }
        }
        private string DirectorioEmpresa()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("C:\\Sic3000\\Datos\\Directorio.ini");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window                    
                    DirectorioBaseSic3000 = line.ToString();
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                return DirectorioBaseSic3000;
            }
            catch (Exception e)
            {
                MessageBox.Show("Algo ocurrio con el directorio.ini. Consulte con el Administrador");
                return "";
            }
        }
        public int num_caja = 0;
        private String Caja()
        {
            string Empresa = "";
            string Caja = "";
            String line;
            try
            {
                Empresa = DirectorioEmpresa();
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Sic3000\\" + Empresa + "\\Sic3000.ini");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window                    
                    Caja = line.ToString();
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                return Caja.Substring(Caja.Length - 3, 3);
            }
            catch (Exception e)
            {
                MessageBox.Show("Algo ocurrio con el numero de caja con el Sic3000.ini, consulte con el Administrador");
                return "";
            }
        }
        private Boolean VerificaNumeroFactura(string NumeroFactura, string NumeroCaja)
        {
            DataTable DatosFactura = new DataTable();
            DatosFactura = NegFactura.Datosfactura(NumeroFactura, NumeroCaja, 3);
            if (DatosFactura.Rows.Count > 0)
            {
                txt_Factura_Cod1.Text = NumeroCaja;
                txt_Caja.Text = NumeroCaja;
                txt_Autorizacion.Text = DatosFactura.Rows[0][12].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void chb_NombreFactura_Click(object sender, EventArgs e)
        {

        }
        private void ActualizaCajas(string Caja)
        {
            // Genero el numero de factura
            Int64 NumeroFactura = 0;
            int Digitos = 0;
            string Factura = "";
            NumeroFactura = Convert.ToInt64(txt_Factura_Cod3.Text);
            Digitos = 9 - Convert.ToString(NumeroFactura + 1).Length;
            Factura = (NumeroFactura + 1).ToString();
            for (int i = 1; i <= Digitos; i++)
            {
                Factura = "0" + Factura;
            }
            OleDbConnection conexion = new OleDbConnection();
            conexion.ConnectionString = "Data Source = C:\\Sic3000\\" + DirectorioBaseSic3000 + "\\Sic3000.mdb; user Id = admin; provider = microsoft.jet.oledb.4.0";
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update Numero_Control set numcon='" + Factura + "' where codcon=72 ";
            DataTable tabla = new DataTable();
            OleDbDataAdapter adaptador = new OleDbDataAdapter();
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch { }
        }/// ACTUALIZA LOS DATOS DE LOS SECUENCIALES DE LAS FACTURAS EN ACCES
        private void ActualizaCajasPrefacturas(string Caja)
        {
            // Genero el numero de factura
            Int64 NumeroFactura = 0;
            int Digitos = 0;
            string Factura = "";

            NumeroFactura = Convert.ToInt64(txt_Factura_Cod3.Text);
            Digitos = 9 - Convert.ToString(NumeroFactura + 1).Length;
            Factura = (NumeroFactura + 1).ToString();
            for (int i = 1; i <= Digitos; i++)
            {
                Factura = "0" + Factura;
            }
            OleDbConnection conexion = new OleDbConnection();
            conexion.ConnectionString = "Data Source = C:\\Sic3000\\" + DirectorioBaseSic3000 + "\\Sic3000.mdb; user Id = admin; provider = microsoft.jet.oledb.4.0";
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update Numero_Control set numcon='" + Factura + "' where codcon=73 ";
            DataTable tabla = new DataTable();
            OleDbDataAdapter adaptador = new OleDbDataAdapter();
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch { }
        } /// ACTUALIZA LOS DATOS DE LOS SECUENCIALES DE LAS PREFACTURAS EN ACCES



        private void CargaDetalleFacturaCopago(String Local, String CodigoNominaUsuario, int i)
        {
            id++;
            DataTable Producto = new DataTable();
            Producto = null;
            Producto = NegFactura.RecuperaCodigoGrupoSic3000((gridDetalleFactura.Rows[i].Cells[0].Value).ToString());
            TABLA17sri tiva = NegParametros.tablaIva();
            if (Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) != 0 && Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) != 0)
            {
                if (facturaPrefactura == 1)
                    DetalleFactura.numnot = txt_Caja.Text + txt_Factura_Cod3.Text;
                else
                    DetalleFactura.numnot = "Pre" + txt_Caja.Text + txt_Factura_Cod3.Text;

                //DESGLOSE DE DESCUENTO POR PRODUCTO PR2022/04/04
                DataTable descuentos = new DataTable();
                decimal descuentoConIva = 0;
                decimal descuentoSinIva = 0;
                descuentos = NegFactura.RecuperaDescuentoXrubro(ultimaAtencion.ATE_CODIGO, Convert.ToInt32(gridDetalleFactura.Rows[i].Cells[0].Value));
                if (descuentos.Rows.Count > 0)
                {
                    for (int a = 0; a < descuentos.Rows.Count; a++)
                    {
                        if (NegFactura.ProductoConSinIVA(Convert.ToInt32(descuentos.Rows[a][0].ToString())))
                        {
                            descuentoConIva += Convert.ToDecimal(descuentos.Rows[a][1].ToString());
                        }
                        else
                        {
                            descuentoSinIva += Convert.ToDecimal(descuentos.Rows[a][1].ToString());
                        }
                    }
                }

                DetalleFactura.tipdoc = 3;
                DetalleFactura.codpro = Producto.Rows[0][0].ToString();
                DetalleFactura.id = id;
                DetalleFactura.codloc = Local;
                DetalleFactura.cantid = "1";
                DetalleFactura.precio = (Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value) + descuentoConIva).ToString();
                if (gridDetalleFactura.Rows[i].Cells[0].Value.ToString() == "1")
                    DetalleFactura.costo = Convert.ToString(NegCuentasPacientes.RecuperaCosto(Convert.ToString(codigoAtencion), 1));
                else if (gridDetalleFactura.Rows[i].Cells[0].Value.ToString() == "27")
                    DetalleFactura.costo = Convert.ToString(NegCuentasPacientes.RecuperaCosto(Convert.ToString(codigoAtencion), 27));
                else
                    DetalleFactura.costo = "0";
                DetalleFactura.desind = "0";
                DetalleFactura.uniman = "0";
                DetalleFactura.regalia = "0";
                DetalleFactura.coddiv = Convert.ToDecimal(Producto.Rows[0][1].ToString());
                DetalleFactura.coddep = Convert.ToDecimal(Producto.Rows[0][2].ToString());
                DetalleFactura.codsec = Convert.ToDecimal(Producto.Rows[0][3].ToString());
                DetalleFactura.fechaven = Convert.ToString(dtpFechaFacturacion.Value.Year + dtpFechaFacturacion.Value.Month + dtpFechaFacturacion.Value.Day);
                DetalleFactura.caja = txt_Caja.Text;
                DetalleFactura.porcobrar = false;
                DetalleFactura.Plista = 0;
                DetalleFactura.Iva = "S";
                DetalleFactura.cajero = Convert.ToInt16(CodigoNominaUsuario);
                DetalleFactura.candev = 0;
                DetalleFactura.encero = "0";
                DetalleFactura.porcentajeIva = (decimal)tiva.PORCENTAJE;
                DetalleFactura.descuento = Math.Round(descuentoConIva, 2);
                ListaDetalleFactura.Add(DetalleFactura);
                DetalleFactura = null;
                DetalleFactura = new DtoFacturaDetalleSic3000();
                id++;
                DetalleFactura.numnot = txt_Caja.Text + txt_Factura_Cod3.Text;
                DetalleFactura.tipdoc = 3;
                DetalleFactura.codpro = Producto.Rows[0][0].ToString();
                DetalleFactura.id = id;
                DetalleFactura.codloc = Local;
                DetalleFactura.cantid = "1";
                DetalleFactura.precio = (Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value) + descuentoSinIva).ToString();
                DetalleFactura.costo = "0";
                DetalleFactura.desind = "0";
                DetalleFactura.uniman = "0";
                DetalleFactura.regalia = "0";
                DetalleFactura.coddiv = Convert.ToDecimal(Producto.Rows[0][1].ToString());
                DetalleFactura.coddep = Convert.ToDecimal(Producto.Rows[0][2].ToString());
                DetalleFactura.codsec = Convert.ToDecimal(Producto.Rows[0][3].ToString());
                DetalleFactura.fechaven = Convert.ToString(dtpFechaFacturacion.Value.Year + dtpFechaFacturacion.Value.Month + dtpFechaFacturacion.Value.Day);
                DetalleFactura.caja = txt_Caja.Text;
                DetalleFactura.porcobrar = false;
                DetalleFactura.Plista = 0;
                DetalleFactura.Iva = "N";
                DetalleFactura.cajero = Convert.ToInt16(CodigoNominaUsuario);
                DetalleFactura.candev = 0;
                DetalleFactura.encero = "0";
                DetalleFactura.porcentajeIva = 0;
                DetalleFactura.descuento = Math.Round(descuentoSinIva, 2);
                ListaDetalleFactura.Add(DetalleFactura);
                DetalleFactura = null;
                DetalleFactura = new DtoFacturaDetalleSic3000();

            }
            else if (Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) == 0 && Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) == 0 && Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value.ToString()) != 0)
            {
                if (facturaPrefactura == 1)
                    DetalleFactura.numnot = txt_Caja.Text + txt_Factura_Cod3.Text;
                else
                    DetalleFactura.numnot = "Pre" + txt_Caja.Text + txt_Factura_Cod3.Text;
                DetalleFactura.tipdoc = 3;
                DetalleFactura.codpro = Producto.Rows[0][0].ToString();
                DetalleFactura.id = id;
                DetalleFactura.codloc = Local;
                DetalleFactura.cantid = "1";
                DetalleFactura.precio = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value).ToString();
                if (gridDetalleFactura.Rows[i].Cells[0].Value.ToString() == "1")
                    DetalleFactura.costo = Convert.ToString(NegCuentasPacientes.RecuperaCosto(Convert.ToString(codigoAtencion), 1));
                else if (gridDetalleFactura.Rows[i].Cells[0].Value.ToString() == "27")
                    DetalleFactura.costo = Convert.ToString(NegCuentasPacientes.RecuperaCosto(Convert.ToString(codigoAtencion), 27));
                else
                    DetalleFactura.costo = "0";
                DetalleFactura.desind = "0";
                DetalleFactura.uniman = "0";
                DetalleFactura.regalia = "0";
                DetalleFactura.coddiv = Convert.ToDecimal(Producto.Rows[0][1].ToString());
                DetalleFactura.coddep = Convert.ToDecimal(Producto.Rows[0][2].ToString());
                DetalleFactura.codsec = Convert.ToDecimal(Producto.Rows[0][3].ToString());
                DetalleFactura.fechaven = Convert.ToString(dtpFechaFacturacion.Value.Year + dtpFechaFacturacion.Value.Month + dtpFechaFacturacion.Value.Day);
                DetalleFactura.caja = txt_Caja.Text;
                DetalleFactura.porcobrar = false;
                DetalleFactura.Plista = 0;
                DetalleFactura.Iva = "S";
                DetalleFactura.cajero = Convert.ToInt16(CodigoNominaUsuario);
                DetalleFactura.candev = 0;
                DetalleFactura.encero = "0";
                DetalleFactura.porcentajeIva = (decimal)tiva.PORCENTAJE;
                DetalleFactura.descuento = Math.Round(Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value.ToString()), 2);
                ListaDetalleFactura.Add(DetalleFactura);
                DetalleFactura = null;
                DetalleFactura = new DtoFacturaDetalleSic3000();
            }
            else if (Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) != 0)
            {
                if (facturaPrefactura == 1)
                    DetalleFactura.numnot = txt_Caja.Text + txt_Factura_Cod3.Text;
                else
                    DetalleFactura.numnot = "Pre" + txt_Caja.Text + txt_Factura_Cod3.Text;
                DetalleFactura.tipdoc = 3;
                DetalleFactura.codpro = Producto.Rows[0][0].ToString();
                DetalleFactura.id = id;
                DetalleFactura.codloc = Local;
                DetalleFactura.cantid = "1";
                DetalleFactura.precio = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value).ToString();
                if (gridDetalleFactura.Rows[i].Cells[0].Value.ToString() == "27")
                    DetalleFactura.costo = Convert.ToString(NegCuentasPacientes.RecuperaCosto(Convert.ToString(codigoAtencion), 27));
                else
                    DetalleFactura.costo = "0";
                DetalleFactura.desind = "0";
                DetalleFactura.uniman = "0";
                DetalleFactura.regalia = "0";
                DetalleFactura.coddiv = Convert.ToDecimal(Producto.Rows[0][1].ToString());
                DetalleFactura.coddep = Convert.ToDecimal(Producto.Rows[0][2].ToString());
                DetalleFactura.codsec = Convert.ToDecimal(Producto.Rows[0][3].ToString());
                DetalleFactura.fechaven = Convert.ToString(dtpFechaFacturacion.Value.Year + dtpFechaFacturacion.Value.Month + dtpFechaFacturacion.Value.Day);
                DetalleFactura.caja = txt_Caja.Text;
                DetalleFactura.porcobrar = false;
                DetalleFactura.Plista = 0;
                DetalleFactura.Iva = "S";
                DetalleFactura.cajero = Convert.ToInt16(CodigoNominaUsuario);
                DetalleFactura.candev = 0;
                DetalleFactura.encero = "0";
                DetalleFactura.porcentajeIva = (decimal)tiva.PORCENTAJE;
                DetalleFactura.descuento = Math.Round(Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value.ToString()), 2);
                ListaDetalleFactura.Add(DetalleFactura);
                DetalleFactura = null;
                DetalleFactura = new DtoFacturaDetalleSic3000();
            }
            else if (Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) != 0)
            {
                if (facturaPrefactura == 1)
                    DetalleFactura.numnot = txt_Caja.Text + txt_Factura_Cod3.Text;
                else
                    DetalleFactura.numnot = "Pre" + txt_Caja.Text + txt_Factura_Cod3.Text;
                DetalleFactura.tipdoc = 3;
                DetalleFactura.codpro = Producto.Rows[0][0].ToString();
                DetalleFactura.id = id;
                DetalleFactura.codloc = Local;
                DetalleFactura.cantid = "1";
                DetalleFactura.precio = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value).ToString();
                if (gridDetalleFactura.Rows[i].Cells[0].Value.ToString() == "1")
                    DetalleFactura.costo = Convert.ToString(NegCuentasPacientes.RecuperaCosto(Convert.ToString(codigoAtencion), 1));
                else
                    DetalleFactura.costo = "0";
                DetalleFactura.desind = "0";
                DetalleFactura.uniman = "0";
                DetalleFactura.regalia = "0";
                DetalleFactura.coddiv = Convert.ToDecimal(Producto.Rows[0][1].ToString());
                DetalleFactura.coddep = Convert.ToDecimal(Producto.Rows[0][2].ToString());
                DetalleFactura.codsec = Convert.ToDecimal(Producto.Rows[0][3].ToString());
                DetalleFactura.fechaven = (dtpFechaFacturacion.Value.Year + dtpFechaFacturacion.Value.Month + dtpFechaFacturacion.Value.Day).ToString();
                DetalleFactura.caja = txt_Caja.Text;
                DetalleFactura.porcobrar = false;
                DetalleFactura.Plista = 0;
                DetalleFactura.Iva = "N";
                DetalleFactura.cajero = Convert.ToInt16(CodigoNominaUsuario);
                DetalleFactura.candev = 0;
                DetalleFactura.encero = "0";
                DetalleFactura.porcentajeIva = 0;
                DetalleFactura.descuento = Math.Round(Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["DESCUENTO"].Value.ToString()), 2);
                ListaDetalleFactura.Add(DetalleFactura);
                DetalleFactura = null;
                DetalleFactura = new DtoFacturaDetalleSic3000();
            }
        }
        private void GuardaFacturaSic3000(ref bool ParaCodigo, Int16 caja)
        {
            //Genero los datos en el objeto / 20/11/2012 / Giovanny tapia 

            String Local = "";
            String CodigoNominaUsuario = "";
            DataTable Producto = new DataTable();
            DataTable FormaPago = new DataTable();
            DataTable DatosCliente = new DataTable();
            DataTable DatosCaja = new DataTable();
            DataTable Usuario = new DataTable();
            DatosCaja = NegFactura.RecuperaInformacionCaja(txt_Caja.Text);

            Usuario = NegFactura.RecuperaInformacionUsuario(His.Entidades.Clases.Sesion.codUsuario);
            if (DatosCaja == null)
            {
                MessageBox.Show("No existen datos de la caja. Verificar.", "His3000");
                return;
            }
            Local = DatosCaja.Rows[0][10].ToString();
            CodigoNominaUsuario = Usuario.Rows[0]["Codigo_Rol"].ToString();
            if (facturaPrefactura == 1)
                FacturaSic3000.numnot = txt_Caja.Text + txt_Factura_Cod3.Text;
            else
                FacturaSic3000.numnot = "Pre" + txt_Caja.Text + txt_Factura_Cod3.Text;
            FacturaSic3000.tipdoc = 3;
            FacturaSic3000.codloc = Local;
            FacturaSic3000.codven = Usuario.Rows[0][13].ToString(); //usuario
            if (facturaPrefactura == 1)
                FacturaSic3000.numfac = txt_Caja.Text + txt_Factura_Cod3.Text;
            else
                FacturaSic3000.numfac = "Pre" + txt_Caja.Text + txt_Factura_Cod3.Text;
            FacturaSic3000.codcli = "1";
            FacturaSic3000.tipcli = "";
            FacturaSic3000.fecha = dtpFechaFacturacion.Value;
            FacturaSic3000.hora = dtpFechaFacturacion.Value.TimeOfDay.ToString();
            FacturaSic3000.ruc = txt_Ruc.Text;
            FacturaSic3000.pordes = "0";
            FacturaSic3000.subtotal = Math.Round(Convert.ToDecimal(txt_SubTotal.Text), 2);
            FacturaSic3000.desctot = Math.Round(Convert.ToDecimal(txt_Descuento.Text), 2);
            FacturaSic3000.totsiva = Math.Round(Convert.ToDecimal(txt_SinIVA.Text), 2);
            FacturaSic3000.totciva = Math.Round(Convert.ToDecimal(txt_ConIVA.Text), 2);
            FacturaSic3000.iva = Math.Round(Convert.ToDecimal(txt_IVA.Text), 2);
            FacturaSic3000.total = Math.Round(Convert.ToDecimal(txt_Total.Text), 2);
            FacturaSic3000.regalia = 0;
            FacturaSic3000.fecha1 = "0";
            FacturaSic3000.cancelado = "0";
            FacturaSic3000.items = 0;
            FacturaSic3000.caja = txt_Caja.Text;
            FacturaSic3000.nomcli = txt_Cliente.Text;
            FacturaSic3000.dircli = txt_Direc_Cliente.Text;
            FacturaSic3000.telcli = txt_telef_Cliente.Text;
            FacturaSic3000.ruccli = txt_Ruc.Text;
            if (txtObserva.Text == "")
            {
                FacturaSic3000.obs = "Factura His3000";
            }
            else
            {
                if (facturaPrefactura == 1)
                    FacturaSic3000.obs = txtObserva.Text;
                else
                    FacturaSic3000.obs = txtObserva.Text;
            }
            FacturaSic3000.numguirem = "0";
            FacturaSic3000.motivo = "0";
            FacturaSic3000.ructra = "0";
            FacturaSic3000.nomtra = "0";
            FacturaSic3000.codcobcli = 0;
            FacturaSic3000.codvencli = 0;
            FacturaSic3000.fecven = dtpFechaFacturacion.Value;
            FacturaSic3000.numorden = "";
            if (cboTipoDescuento.SelectedItem != null)
            {
                decimal codigoDescuento = Convert.ToDecimal(cboTipoDescuento.SelectedIndex.ToString());
                FacturaSic3000.porven = codigoDescuento + 1;
            }
            else
                FacturaSic3000.porven = 0;
            FacturaSic3000.formpagPro = "N/D";
            FacturaSic3000.validez = "N/D";
            FacturaSic3000.tiempoentrega = "N/D";
            if (facturaPrefactura == 1)
                FacturaSic3000.numfac2 = txt_Caja.Text + txt_Factura_Cod3.Text;
            else
                FacturaSic3000.numfac2 = "Pre" + txt_Caja.Text + txt_Factura_Cod3.Text;
            FacturaSic3000.porcobrar = true;
            FacturaSic3000.Impresa = false;
            FacturaSic3000.cajero = Convert.ToDecimal(CodigoNominaUsuario);
            FacturaSic3000.subt_Dev = 0;
            FacturaSic3000.coniva_Dev = 0;
            FacturaSic3000.siniva_Dev = 0;
            FacturaSic3000.desct_Dev = 0;
            FacturaSic3000.iva_Dev = 0;
            FacturaSic3000.Tot_Dev = 0;
            FacturaSic3000.pormayor = false;
            FacturaSic3000.facturada = false;
            FacturaSic3000.coniva = false;
            FacturaSic3000.imprimedesct = false;
            FacturaSic3000.autorizacion = txt_Autorizacion.Text;
            FacturaSic3000.GrupoCliente = false;
            FacturaSic3000.EmpId = 0;
            FacturaSic3000.ConvId = 0;
            TABLA17sri tiva = NegParametros.tablaIva();
            FacturaSic3000.porcentajeIva = (decimal)tiva.TARIFA;


            if (dgvDivideFactura.Rows.Count > 0)
            {
                for (int i = 0; i < dgvDivideFactura.Rows.Count; i++)
                {
                    //id++;
                    if (!Convert.ToBoolean(dgvDivideFactura.Rows[i].Cells[6].Value))
                    {
                        //if (Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value.ToString()) != 0 && Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value.ToString()) != 0)
                        //{

                        //    ////CREO LINEA EN CUENTA PACIENTE DE COPAGO CON IVA
                        //    ////if (NegFactura.CreaCopagoIva(txt_Atencion.Text, gridDetalleFactura.Rows[i].Cells[3].Value.ToString(), gridDetalleFactura.Rows[i].Cells[2].Value.ToString()))
                        //    ////{
                        //    ////    CargaDetalleFacturaCopago(Local, CodigoNominaUsuario, i);
                        //    ////}else
                        //    //{
                        //    //    MessageBox.Show("NO SE PUEDE GENERAR EL COPAGO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    //}
                        //}
                        //else
                        //{
                        CargaDetalleFacturaCopago(Local, CodigoNominaUsuario, i);
                        //}
                    }


                }
            }

            else
            {
                for (int i = 0; i < gridDetalleFactura.Rows.Count; i++)
                {
                    CargaDetalleFacturaCopago(Local, CodigoNominaUsuario, i);
                    //if (dgvDivideFactura.Rows.Count > 0)
                    //    if (!Convert.ToBoolean(dgvDivideFactura.Rows[i].Cells[6].Value))
                    //        CargaDetalleFacturaCopago(Local, CodigoNominaUsuario, i);
                }
            }
            FacturaSic3000.DetalleFactura = ListaDetalleFactura;
            // Guardo los datos de las cuentas por cobrar. Giovanny Tapia
            DtoCXC CuentasxCobrar = new DtoCXC();
            DtoEstadoCuenta EstadoCuenta = new DtoEstadoCuenta();
            DtoFacturaPago FacturaPago = new DtoFacturaPago();
            List<DtoCXC> ListaCuentasxCobrar = new List<DtoCXC>();
            List<DtoEstadoCuenta> ListaEstadoCuenta = new List<DtoEstadoCuenta>();
            List<DtoFacturaPago> ListaFacturaPago = new List<DtoFacturaPago>();
            for (Int16 i = 0; i < gridFormasPago.Rows.Count; i++)
            {
                if (gridFormasPago.Rows[i].Cells[0].Value != null)
                {
                    FormaPago = NegFactura.FormaPagoSic(true, Convert.ToInt32(gridFormasPago.Rows[i].Cells[0].Value));
                    if (Convert.ToBoolean(FormaPago.Rows[0]["CXC"]) == false)
                    {
                        //Genera el pago de la factura

                        FacturaPago.numdoc = FacturaSic3000.numfac2;
                        FacturaPago.tipdoc = 3;
                        FacturaPago.forpag = gridFormasPago.Rows[i].Cells[0].Value.ToString();
                        FacturaPago.tipomov = "F";
                        FacturaPago.codcli = "0";
                        FacturaPago.parcial = gridFormasPago.Rows[i].Cells[2].Value.ToString();
                        FacturaPago.parcial1 = "0";
                        FacturaPago.claspag = FormaPago.Rows[0]["claspag"].ToString();
                        FacturaPago.tipoventa = "FD";
                        FacturaPago.fecha = dtpFechaFacturacion.Value;
                        FacturaPago.fecha1 = dtpFechaFacturacion.Value.ToString();
                        FacturaPago.banco = "";
                        FacturaPago.numcuenta_tarj = "";
                        FacturaPago.cheque_caduca = "";
                        FacturaPago.dueño = "";
                        FacturaPago.autoriza = "";
                        FacturaPago.obs = gridFormasPago.Rows[i].Cells[5].Value.ToString();
                        FacturaPago.fila = i + 1;
                        FacturaPago.caja = txt_Caja.Text;
                        FacturaPago.cajero = CodigoNominaUsuario;
                        FacturaPago.Vendedor = CodigoNominaUsuario;
                        FacturaPago.local = Local;
                        FacturaPago.Arqueada = false;
                        FacturaPago.imprime = false;
                        FacturaPago.detalle = false;
                        if (gridFormasPago.Rows[i].Cells[4].Value != null)
                        {
                            FacturaPago.banco = gridFormasPago.Rows[i].Cells[4].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.banco = "";
                        }

                        if (gridFormasPago.Rows[i].Cells[6].Value != null)
                        {
                            FacturaPago.numcuenta_tarj = gridFormasPago.Rows[i].Cells[6].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.numcuenta_tarj = "";
                        }

                        if (gridFormasPago.Rows[i].Cells[7].Value != null)
                        {
                            FacturaPago.cheque_caduca = gridFormasPago.Rows[i].Cells[7].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.cheque_caduca = "";
                        }

                        if (gridFormasPago.Rows[i].Cells[9].Value != null)
                        {
                            FacturaPago.dueño = gridFormasPago.Rows[i].Cells[9].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.dueño = "";
                        }

                        if (gridFormasPago.Rows[i].Cells[8].Value != null)
                        {
                            FacturaPago.autoriza = gridFormasPago.Rows[i].Cells[8].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.autoriza = "";
                        }
                        ListaFacturaPago.Add(FacturaPago);
                        FacturaPago = null;
                        FacturaPago = new DtoFacturaPago();
                    }
                    else
                    {
                        //Genera el pago de la factura
                        FacturaPago.numdoc = FacturaSic3000.numfac2;
                        FacturaPago.tipdoc = 3;
                        FacturaPago.forpag = gridFormasPago.Rows[i].Cells[0].Value.ToString();
                        FacturaPago.tipomov = "F";
                        FacturaPago.codcli = "0";
                        FacturaPago.parcial = gridFormasPago.Rows[i].Cells[2].Value.ToString();
                        FacturaPago.parcial1 = "0";
                        FacturaPago.claspag = FormaPago.Rows[0]["claspag"].ToString();
                        FacturaPago.tipoventa = "FD";
                        FacturaPago.fecha = dtpFechaFacturacion.Value;
                        FacturaPago.fecha1 = dtpFechaFacturacion.Value.ToString();
                        if (gridFormasPago.Rows[i].Cells[4].Value != null)
                        {
                            FacturaPago.banco = gridFormasPago.Rows[i].Cells[4].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.banco = "";
                        }
                        if (gridFormasPago.Rows[i].Cells[5].Value != null)
                        {
                            FacturaPago.obs = gridFormasPago.Rows[i].Cells[5].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.numcuenta_tarj = "";
                        }
                        if (gridFormasPago.Rows[i].Cells[6].Value != null)
                        {
                            FacturaPago.numcuenta_tarj = gridFormasPago.Rows[i].Cells[6].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.numcuenta_tarj = "";
                        }
                        if (gridFormasPago.Rows[i].Cells[7].Value != null)
                        {
                            FacturaPago.cheque_caduca = gridFormasPago.Rows[i].Cells[7].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.cheque_caduca = "";
                        }

                        if (gridFormasPago.Rows[i].Cells[9].Value != null)
                        {
                            FacturaPago.dueño = gridFormasPago.Rows[i].Cells[9].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.dueño = "";
                        }
                        if (gridFormasPago.Rows[i].Cells[8].Value != null)
                        {
                            FacturaPago.autoriza = gridFormasPago.Rows[i].Cells[8].Value.ToString();
                        }
                        else
                        {
                            FacturaPago.autoriza = ""; ;
                        }
                        FacturaPago.fila = i + 1;
                        FacturaPago.caja = txt_Caja.Text;
                        FacturaPago.cajero = CodigoNominaUsuario;
                        FacturaPago.Vendedor = CodigoNominaUsuario;
                        FacturaPago.local = Local;
                        FacturaPago.Arqueada = false;
                        FacturaPago.imprime = false;
                        FacturaPago.detalle = false;
                        ListaFacturaPago.Add(FacturaPago);
                        FacturaPago = null;
                        FacturaPago = new DtoFacturaPago();
                        // Genera el Estado de Cuenta
                        EstadoCuenta.id = i + 1;
                        EstadoCuenta.numfac = FacturaSic3000.numfac2;
                        if (facturaPrefactura == 1)
                            EstadoCuenta.tipodoc = "FACTURA";
                        else
                            EstadoCuenta.tipodoc = "Pre-FACTURA";
                        EstadoCuenta.iddoc = "F";
                        EstadoCuenta.numdoc = FacturaSic3000.numfac2;
                        FacturaPago.codcli = "0";
                        EstadoCuenta.fecha = dtpFechaFacturacion.Value.ToString();
                        EstadoCuenta.obs = "FACTURA" + "";
                        EstadoCuenta.debe = Convert.ToDecimal(gridFormasPago.Rows[i].Cells[2].Value.ToString());
                        EstadoCuenta.haber = 0;
                        EstadoCuenta.saldo = Convert.ToDecimal(gridFormasPago.Rows[i].Cells[2].Value.ToString());
                        EstadoCuenta.fecha1 = "";
                        EstadoCuenta.forpag = gridFormasPago.Rows[i].Cells[0].Value.ToString();
                        EstadoCuenta.claspag = FormaPago.Rows[0]["claspag"].ToString();
                        EstadoCuenta.caja = txt_Caja.Text;
                        ListaEstadoCuenta.Add(EstadoCuenta);
                        EstadoCuenta = null;
                        EstadoCuenta = new DtoEstadoCuenta();
                        // Genera la CXC//Cambio convert fecha 07/10/2013
                        FacturaPago.codcli = "0";
                        CuentasxCobrar.numdoc = FacturaSic3000.numfac2;
                        CuentasxCobrar.fecha = Convert.ToDateTime(string.Format(("{0:dd/MM/yyyy}"), dtpFechaFacturacion.Value.ToString()));
                        CuentasxCobrar.tipo = "F";
                        CuentasxCobrar.debe = Convert.ToDecimal(gridFormasPago.Rows[i].Cells[2].Value.ToString());
                        CuentasxCobrar.haber = 0;
                        CuentasxCobrar.saldo = Convert.ToDecimal(gridFormasPago.Rows[i].Cells[2].Value.ToString());
                        CuentasxCobrar.fecha1 = dtpFechaFacturacion.Value.ToString();
                        CuentasxCobrar.fechapago = Convert.ToDateTime(string.Format(("{0:dd/MM/yyyy}"), gridFormasPago.Rows[i].Cells[3].Value.ToString()));
                        CuentasxCobrar.tipest = "";
                        CuentasxCobrar.fecven = Convert.ToDateTime(string.Format(("{0:dd/MM/yyyy}"), gridFormasPago.Rows[i].Cells[3].Value.ToString()));
                        CuentasxCobrar.forpag = gridFormasPago.Rows[i].Cells[0].Value.ToString();
                        CuentasxCobrar.claspag = FormaPago.Rows[0]["claspag"].ToString();
                        CuentasxCobrar.fecven1 = gridFormasPago.Rows[i].Cells[3].Value.ToString();
                        CuentasxCobrar.fila = i + 1;
                        CuentasxCobrar.Marca = "";
                        ListaCuentasxCobrar.Add(CuentasxCobrar);
                        CuentasxCobrar = null;
                        CuentasxCobrar = new DtoCXC();
                    }
                }
            }

            string Factura = "";

            Factura = (txt_Caja.Text + txt_Factura_Cod3.Text);

            MEDICOS Med = new MEDICOS();
            Med = NegMedicos.medicoPorAtencion(ultimaAtencion.ATE_CODIGO);
            string Medico = (Med.MED_APELLIDO_PATERNO + ' ' + Med.MED_APELLIDO_MATERNO + ' ' + Med.MED_NOMBRE1 + ' ' + Med.MED_NOMBRE2);
            DateTime FechaAlta;
            if (Convert.ToString(ultimaAtencion.ATE_FECHA_ALTA) == "")
                FechaAlta = DateTime.Now;
            else
                FechaAlta = Convert.ToDateTime(ultimaAtencion.ATE_FECHA_ALTA);
            //NegFactura.GuardaDatosAdicionales(Factura, txt_RucPaciente.Text, txt_ApellidoH1.Text, txt_Direc_Cliente.Text, txt_Telef_P.Text, Convert.ToInt64(txt_Historia_Pc.Text.Trim()), Convert.ToDateTime(ultimaAtencion.ATE_FECHA_INGRESO), Convert.ToDateTime(FechaAlta), Medico, Convert.ToInt64(ultimaAtencion.ATE_CODIGO));

            //IDENTIFICADOR A GUARDARSE
            var tipoIdentificacion = "";
            if (chk_Ruc.Checked == true)
                tipoIdentificacion = "04";
            else if (chk_Cedula.Checked == true)
                tipoIdentificacion = "05";
            else if (chk_pasaporte.Checked == true)
                tipoIdentificacion = "06";
            else
                tipoIdentificacion = "07";

            if (NegFactura.CrearFacturaSic3000(FacturaSic3000, Convert.ToInt64(txt_Historia_Pc.Text.Trim()), Convert.ToInt64(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), ListaFacturaPago, ListaEstadoCuenta, ListaCuentasxCobrar, facturaPrefactura, ultimaAtencion.ATE_CODIGO, txt_RucPaciente.Text, txt_ApellidoH1.Text, txt_Direc_Cliente.Text, txt_Telef_P.Text, Convert.ToDateTime(ultimaAtencion.ATE_FECHA_INGRESO), Convert.ToDateTime(FechaAlta), Medico, caja, tipoIdentificacion, txtEmailFactura.Text) == 0)
            {
                facturado = false;
                MessageBox.Show("NO SE PUEDE ALMACENAR LA FACTURA POR ERROR EN CONECTIVIDAD DE RED. PIDA SOPORTE A SISTEMAS Y LUEGO VUELVA A INGRESAR!!!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ParaCodigo = false;
                this.Close();
            }
        }
        private void txt_SinIVA_Click(object sender, EventArgs e)
        {

        }
        private void ultraTabControl2_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }
        private void ActualizaEstadoCuenta()
        {
            string Factura = "";
            int escCodigo = 0;
            if (facturaPrefactura == 1)
            {
                escCodigo = 6;
                Factura = (txt_Caja.Text + txt_Factura_Cod3.Text);
            }
            else
            {
                escCodigo = 3;
                Factura = ("Pre" + txt_Caja.Text + txt_Factura_Cod3.Text);
            }
            NegFactura.ActualizaEstadoCuenta(ultimaAtencion.ATE_CODIGO, escCodigo, Factura, Sesion.codUsuario);
        }
        private void ActualizaEstadoCuenta2(int Esc_Codigo, string usuario)
        {
            string Factura = ("Pre" + txt_Caja.Text + "" + txt_Factura_Cod3.Text);
            NegFactura.ActualizaEstadoCuenta2(ultimaAtencion.ATE_CODIGO, txt_Ruc.Text, Factura, Esc_Codigo, usuario);
        }
        private void GuardaDatosAdicionales()
        {
            string Factura = "";
            if (facturaPrefactura == 1)
                Factura = (txt_Caja.Text + txt_Factura_Cod3.Text);
            else
            {
                Factura = ("Pre" + txt_Caja.Text + txt_Factura_Cod3.Text);
            }
            MEDICOS Med = new MEDICOS();
            Med = NegMedicos.medicoPorAtencion(ultimaAtencion.ATE_CODIGO);
            string Medico = (Med.MED_APELLIDO_PATERNO + ' ' + Med.MED_APELLIDO_MATERNO + ' ' + Med.MED_NOMBRE1 + ' ' + Med.MED_NOMBRE2);
            DateTime FechaAlta;
            if (Convert.ToString(ultimaAtencion.ATE_FECHA_ALTA) == "")
                FechaAlta = DateTime.Now;
            else
                FechaAlta = Convert.ToDateTime(ultimaAtencion.ATE_FECHA_ALTA);
            NegFactura.GuardaDatosAdicionales(Factura, txt_RucPaciente.Text, txt_ApellidoH1.Text, txt_Direc_Cliente.Text, txt_Telef_P.Text, Convert.ToInt64(txt_Historia_Pc.Text.Trim()), Convert.ToDateTime(ultimaAtencion.ATE_FECHA_INGRESO), Convert.ToDateTime(FechaAlta), Medico, Convert.ToInt64(ultimaAtencion.ATE_CODIGO));
        }
        private void label37_Click(object sender, EventArgs e)
        {

        }
        private void btnAnticipos_Click(object sender, EventArgs e)
        {
            frmAnticipos Form = new frmAnticipos(txt_Atencion.Text);
            Form.Show();
        }
        private void frmFactura_Load(object sender, EventArgs e)
        {
            dtpFechaFacturacion.Value = DateTime.Now;
            this.cmbDescuentos.DataSource = NegFactura.RecuperaDescuentos();
            this.cmbDescuentos.DisplayMember = "DescuentoDescripcion".Trim();
            this.cmbDescuentos.ValueMember = "DescuentoCodigo".Trim();

            DtoParametros importar = new DtoParametros();
            importar = NegParametros.RecuperaParametros(7).Where(cod => cod.PAD_CODIGO == 42).FirstOrDefault();
            if (importar != null)
                if (importar.PAD_ACTIVO)
                {
                    btnImportar.Visible = true;
                }

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
            if (NegParametros.ParametroFacturaFecha())
                dtpFechaFacturacion.Enabled = true;
        }
        private void chbConsumidorFinal_CheckedChanged(object sender, EventArgs e)
        {
            chk_Cedula.Checked = false;
            chk_pasaporte.Checked = false;
            chk_Ruc.Checked = false;
            gridFormasPago.Rows.Clear();
            DataGridViewRow dt = new DataGridViewRow();
            dt.CreateCells(gridFormasPago);
            if (chbConsumidorFinal.Checked == true)
            {
                txt_Ruc.Text = "9999999999999";
                txt_Cliente.Text = "CONSUMIDOR FINAL";
                txt_telef_Cliente.Text = "9999999";
                txt_Direc_Cliente.Text = "CONSUMIDOR FINAL";
                txt_Ruc.Enabled = false;
                txt_Cliente.Enabled = false;
                txt_telef_Cliente.Enabled = false;
                txt_Direc_Cliente.Enabled = false;
                dt.Cells[0].Value = "6" /*formaPago.FOR_CODIGO*/;
                dt.Cells[1].Value = "EFECTIVO"/*formaPago.FOR_DESCRIPCION*/;
                dt.Cells[2].Value = this.txt_Total.Text;
                dt.Cells[3].Value = DateTime.Today.AddDays(1);
                dt.Cells[5].Value = "0";
                dt.Cells[9].Value = "";
                gridFormasPago.Rows.Add(dt);
            }
            else
            {
                cbx_FacturaNombre.Enabled = true;
                txt_Ruc.Text = txt_RucPaciente.Text;
                txt_Direc_Cliente.Text = txt_Direccion_P.Text;
                txt_telef_Cliente.Text = txt_Telef_P.Text;
                txt_Cliente.Text = txt_ApellidoH1.Text;
                txt_Ruc.Enabled = true;
                txt_Cliente.Enabled = true;
                txt_telef_Cliente.Enabled = true;
                txt_Direc_Cliente.Enabled = true;
                dt.Cells[0].Value = "6" /*formaPago.FOR_CODIGO*/;
                dt.Cells[1].Value = "EFECTIVO"/*formaPago.FOR_DESCRIPCION*/;
                dt.Cells[2].Value = this.txt_Total.Text;
                dt.Cells[3].Value = DateTime.Today.AddDays(1);
                dt.Cells[5].Value = "0";
                dt.Cells[9].Value = txt_Cliente.Text;
                gridFormasPago.Rows.Add(dt);
            }
        }
        private void txtDescuentos_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void cmbDescuentos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cmb_Rubros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txtDescuentos_TextChanged(object sender, EventArgs e)
        {

        }
        private void facturaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ReporteFactura reporteFactura = new ReporteFactura();
            // IMPRESION DE EL DETALLE DE LA FACTURA
            frmReporteDesgloseFactura FACTURA = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, txt_Direc_Cliente.Text, Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "ESTADO_CUENTA", "", 0, "", "", 0, 0, txtObserva.Text, txt_IVA.Text, txt_ConIVA.Text, lblConvenio.Text, txt_SinIVA.Text, txt_SubTotal.Text, txt_Descuento.Text, txt_Total.Text);
            FACTURA.Show();
            if ((MessageBox.Show("Desea imprimir el desglose de articulos de la factura?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + '-' + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "FACTURA", "", 0, "", "", 0, 0, "", "", "", "", "", "", "", "");
                DesgloseFactura.Show();
            }

            //if (txtObserva.Text != "")
            //{
            //    ReporteFactura reporteFactura = new ReporteFactura();
            //    // IMPRESION DE EL DETALLE DE LA FACTURA
            //    frmReporteDesgloseFactura FACTURA = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, txt_Direc_Cliente.Text, Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "FACTURA_IMPRESION", "", 0, "", "", 0, 0, txtObserva.Text, txt_IVA.Text, txt_ConIVA.Text, lblConvenio.Text, txt_SinIVA.Text, txt_SubTotal.Text, txt_Descuento.Text, txt_Total.Text);
            //    FACTURA.Show();
            //    if ((MessageBox.Show("Desea imprimir el desglose de articulos de la factura?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.Yes))
            //    {
            //        frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + '-' + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "FACTURA", "", 0, "", "", 0, 0, "", "", "", "", "", "", "", "");
            //        DesgloseFactura.Show();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Debe ingresar el procedimiento que se aplico al Paciente", "HIS3000");
            //}
        }
        private void preFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NumeroPrefactura == 0)
            {
                MessageBox.Show("El computador no tiene los datos necesariios para generar prefacturas.");
                return;
            }
            DtoPreFactura Prefactura = new DtoPreFactura();
            List<DtoDetallePrefactura> ListaPreFacturaDetalle = new List<DtoDetallePrefactura>();
            String Local = "";
            DataTable DatosCaja = new DataTable();
            /*datos de la caja*/
            DatosCaja = NegFactura.RecuperaInformacionCaja(txt_Caja.Text);
            Local = DatosCaja.Rows[0]["codlocal"].ToString();
            /*Encabezado Prefactura*/
            Prefactura.PREFAC_CODIGO = 0;
            Prefactura.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
            Prefactura.PREFAC_NUMERO = NumeroPrefactura.ToString();
            Prefactura.PREFAC_AUTORIZACION = Convert.ToInt32(txt_Autorizacion.Text);
            Prefactura.PREFAC_FECHA = DateTime.Now;
            Prefactura.CLI_NOMBRE = txt_Cliente.Text;
            Prefactura.CLI_RUC = txt_Ruc.Text;
            Prefactura.CLI_TELEFONO = txt_telef_Cliente.Text;
            Prefactura.PREFAC_TOTAL = Convert.ToDecimal(txt_Total.Text);
            Prefactura.PREFAC_SUBTOTAL = Convert.ToDecimal(txt_SubTotal.Text);
            Prefactura.PREFAC_IVAUNO = Convert.ToDecimal(txt_IVA.Text);
            Prefactura.PREFAC_IVADOS = Convert.ToDecimal(0);
            Prefactura.PREFAC_IVATRES = Convert.ToDecimal(0);
            Prefactura.PREFAC_ESTADO = "";
            Prefactura.PREFAC_CAJA = txt_Caja.Text;
            Prefactura.PREFAC_VENDEDOR = His.Entidades.Clases.Sesion.codUsuario.ToString();
            Prefactura.PREFAC_LOCAL = Local;
            Prefactura.PREFAC_ARQUEO = 0;
            Prefactura.PREFAC_DESCUENTO = Convert.ToDecimal(txtDescuentos.Text);
            Prefactura.PREFAC_CONIVA = Convert.ToDecimal(txt_ConIVA.Text);
            Prefactura.PREFAC_SINIVA = Convert.ToDecimal(txt_SinIVA.Text);
            for (int i = 0; i < gridDetalleFactura.Rows.Count; i++)
            {
                DtoDetallePrefactura DetallePrefactura = new DtoDetallePrefactura();
                DetallePrefactura.COD_PFDETALLE = 0;
                DetallePrefactura.PREFAC_NUMERO = NumeroPrefactura.ToString();
                DetallePrefactura.DET_DESCIPCION = Convert.ToString(gridDetalleFactura.Rows[i].Cells[1].Value);
                DetallePrefactura.DET_VALOR = Convert.ToDecimal(gridDetalleFactura.Rows[i].Cells[2].Value);
                DetallePrefactura.DET_ESTADO = 1;
                ListaPreFacturaDetalle.Add(DetallePrefactura);
                DetallePrefactura = null;
            }
            Prefactura.DetallePreFactura = ListaPreFacturaDetalle;
            NegFactura.GeneraPrefactura(Prefactura);  // Guarda la prefactura
            ActualizaCajasPrefacturas(txt_Factura_Cod1.Text); // actualiza el secuencial de la prefactura
            MessageBox.Show("La prefactura N. " + NumeroPrefactura.ToString() + " se a ingresado con exito.");
            frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura(NumeroPrefactura.ToString(), txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "PREFACTURA", "", 0, "", "", 0, 0, "", "", "", "", "", "", "", "");
            DesgloseFactura.Show();
            NegValidaciones.alzheimer();
            this.Dispose();
        }
        private void frmFactura_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void btnGeneraValores_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Está Seguro De Generar Rubros Automáticos?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DataTable UCI = new DataTable();
                    int totalDiasUCI = 0;
                    UCI = NegCuentasPacientes.ValoresHabitacionUCI(ultimaAtencion.ATE_CODIGO);
                    if (UCI.Rows.Count > 0)
                    {
                        TimeSpan difFechas = DateTime.Now - DateTime.Now;
                        for (int i = 0; i < UCI.Rows.Count; i++)
                        {
                            if (UCI.Rows[i][1].ToString() != "")
                                difFechas += Convert.ToDateTime(UCI.Rows[i][1].ToString()) - Convert.ToDateTime(UCI.Rows[i][0].ToString());
                            else
                                difFechas += DateTime.Now - Convert.ToDateTime(UCI.Rows[i][0].ToString());
                        }
                        if (Convert.ToInt16(difFechas.Days) == 0)
                            totalDiasUCI = Convert.ToInt16(difFechas.Days) + 1;
                        else
                            totalDiasUCI = Convert.ToInt16(difFechas.Days);
                        NegCuentasPacientes.GuardaTotalUCI(ultimaAtencion.ATE_CODIGO, His.Entidades.Clases.Sesion.codUsuario, totalDiasUCI, NegCuentasPacientes.RecuperaCodigoHabitacion(ultimaAtencion.ATE_CODIGO), CodigoAseguradora);
                    }
                    DataTable habitacion = new DataTable();
                    habitacion = NegCuentasPacientes.ValoresHabitacion(ultimaAtencion.ATE_CODIGO);
                    string habit = "";
                    if (habitacion.Rows.Count > 0)
                    {
                        for (int i = 0; i < habitacion.Rows.Count; i++)
                        {
                            DataTable habita = new DataTable();
                            habita = NegCuentasPacientes.ValidaHabitacion(CodigoAseguradora, habitacion.Rows[i][2].ToString());
                            if (habita.Rows.Count > 0)
                            {
                                habit = habita.Rows[0][1].ToString();
                                break;
                            }
                        }
                    }
                    if (habit != "")
                        NegCuentasPacientes.GeneraValoresautomaticos(ultimaAtencion.ATE_CODIGO, His.Entidades.Clases.Sesion.codUsuario, Convert.ToInt32(txt_Dias_P.Text) - totalDiasUCI, habit, CodigoAseguradora, CodigoTipoEmpresa);
                    else
                        NegCuentasPacientes.AdministracionMedicamentos(ultimaAtencion.ATE_CODIGO, His.Entidades.Clases.Sesion.codUsuario, CodigoAseguradora, CodigoTipoEmpresa);

                    cargarDetalleFactura();
                    MessageBox.Show("Los Valores Se Han Generado Correctamente.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Exception." + Ex, "His3000");
            }

        }
        private void gridDetalleFactura_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                txtObserva.Visible = true;
                txtObserva.Text = "";
                lblObserva.Visible = true;
                txtObserva.Focus();
            }
            if (e.KeyCode == Keys.F4)
            {
                ultraTabControl2.SelectedTab = ultraTabControl2.Tabs["detalle"];
                SendKeys.SendWait("{TAB}");
                dgvDescuento.Visible = true;
            }
        }
        private void btnImprimir_ButtonClick_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txt_Descuento.Text) > 0)
            {
                DialogResult result = MessageBox.Show("La siguiente acciòn eliminara los descuentos ingresados. Desea continuar?", "His3000", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    NegFactura.ActualizaDescuentoAtencion(Convert.ToInt64(txt_Atencion.Text));
                    ultraTabControl2.Tabs["descuento"].Visible = false;

                    frmCorreccionCuenta Form = new frmCorreccionCuenta(ultimaAtencion.ATE_CODIGO);
                    Form.ShowDialog();

                    cargarDetalleFactura();
                }
            }
            else
            {
                ultraTabControl2.Tabs["descuento"].Visible = false;
                frmCorreccionCuenta Form = new frmCorreccionCuenta(ultimaAtencion.ATE_CODIGO);
                Form.ShowDialog();
                NegFactura.ArreglaIVABase(Convert.ToString(ultimaAtencion.ATE_CODIGO));
                cargarDetalleFactura();
            }
        }
        private void txtObserva_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (VerificaAltaPaciente())
                {
                    e.Handled = true;
                    //dar un nombre a los controles
                    ultraTabControl2.SelectedTab = ultraTabControl2.Tabs["formaPago"];
                    SendKeys.SendWait("{TAB}");
                }
                else
                {
                    MessageBox.Show("El Paciente Debe Estar Dado El Alta Para Llenar Formas de Pago", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        public bool mushuñan = false;
        private void CargaPaciente()
        {
            Int32 Pedido = 0;
            PEDIDOS_ESTACIONES pe = new PEDIDOS_ESTACIONES();
            pe = (PEDIDOS_ESTACIONES)this.cmbEstaciones.SelectedItem;
            var Estacion = (Byte)pe.PEE_CODIGO;
            PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
            RUBROS Rubro1 = new RUBROS();
            Rubro1 = (RUBROS)cmb_Rubros.SelectedItem;
            ar = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
            var Rubro = (Int16)Rubro1.RUB_CODIGO;
            frmAyudaProductos form;

            ////edgar 20201203
            //Boolean todos = false;
            //var codigoArea = (Int16)ar.PEA_CODIGO;

            //var solicitudMedicamentos = new His.HabitacionesUI.frmProductosAyuda(codigoArea, txt_ApellidoH1.Text, His.Entidades.Clases.Sesion.nomUsuario, txt_Historia_Pc.Text.ToString(), txt_Atencion.Text.ToString(), "0", lblConvenio.Text, Rubro, 0 /*CodigoAseguradora*/, 0 /*CodigoTipoEmpresa*/, todos);
            //solicitudMedicamentos.ShowDialog();


            if (cmb_Areas.Text == "TODAS LAS AREAS")
            {
                //todos = true;
                form = new frmAyudaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem), (RUBROS)(this.cmb_Rubros.SelectedItem), true, CodigoTipoEmpresa);
                form._codigoatencion = ultimaAtencion.ATE_CODIGO;
                form.numcaja = num_caja;

            }
            else
            {
                form = new frmAyudaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem), (RUBROS)(this.cmb_Rubros.SelectedItem), false, CodigoTipoEmpresa);
                form._codigoatencion = ultimaAtencion.ATE_CODIGO;
                form.numcaja = num_caja;

            }

            form.ShowDialog();
            listaProductosSolicitados = form.PedidosDetalle;
            if (listaProductosSolicitados != null && listaProductosSolicitados.Count > 0)
            {
                Double totales = 0.00;
                Double valores = 0.00;
                Double ivaTotal = 0.00;
                PEDIDOS pedido = new PEDIDOS();
                pedido.PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1;
                pedido.PED_FECHA = DateTime.Now; /*PARA GUARDAR EL PEDIDO SE NECESITA FECHA Y HORA/ GIOVANNY TAPIA / 12/04/2013*/
                pedido.PED_DESCRIPCION = form.descripcion;
                pedido.PED_ESTADO = 2;
                pedido.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                pedido.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                pedido.PEE_CODIGO = codigoEstacion; /* CODIGO DE LA ESTACION DESDE DONDE SE REALIZA EL PEDIDO / GIOVANNY TAPIA / 04/01/2012 */
                pedido.TIP_PEDIDO = His.Parametros.FarmaciaPAR.PedidoMedicamentos;
                pedido.PED_PRIORIDAD = 1;
                pedido.MED_CODIGO = 0;
                pedido.PEDIDOS_AREASReference.EntityKey = ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).EntityKey;
                pedido.IP_MAQUINA = Sesion.ip;
                HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == ultimaAtencion.HABITACIONESReference.EntityKey);
                if (hab != null)
                {
                    pedido.HAB_CODIGO = Sesion.codHabitacion;
                }
                if (form.codTransaccion != 0)
                {
                    pedido.PED_TRANSACCION = form.codTransaccion;
                }
                else
                {
                    pedido.PED_TRANSACCION = pedido.PED_CODIGO;
                }
                Pedido = pedido.PED_CODIGO;
                //NegPedidos.crearPedido(pedido);
                List<PEDIDOS_DETALLE> listapedidos = new List<PEDIDOS_DETALLE>();
                foreach (var solicitud in listaProductosSolicitados)
                {
                    if (!solicitud.PRO_DESCRIPCION.Contains("HONORARIOS"))
                        listapedidos.Add(solicitud);
                }

                if (NegPedidos.crearDetallePedido(listapedidos, pedido, form.NumVale))
                {
                    //foreach (var item in listapedidos) // Se comenta por el cambio a IP-Bodega // Mario 15/02/2023
                    //{
                    //    Quirofano.ProductoBodega(item.PRO_CODIGO_BARRAS, item.PDD_CANTIDAD.ToString(), FacturaPAR.BodegaPorDefecto);
                    //}
                    foreach (var solicitud in listaProductosSolicitados)
                    {
                        if (solicitud.PRO_DESCRIPCION.Contains("HONORARIOS"))//pruebas para agregar medicos
                        {
                            foreach (var item in form.DetalleHonorarios)
                            {
                                DtoDetalleHonorariosMedicos HonorariosMedico = new DtoDetalleHonorariosMedicos();
                                HonorariosMedico = item;
                                if (HonorariosMedico != null)
                                {
                                    pedido.MED_CODIGO = HonorariosMedico.MED_CODIGO;
                                }
                                solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
                                Int64 pdd_codigo = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                                //solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                                // --alexalexalex
                                if (cmb_Areas.Text == "TODAS LAS AREAS")
                                {
                                    Int32 codpro = Convert.ToInt32(solicitud.PRO_CODIGO_BARRAS.ToString());
                                    Int32 xcodDiv = 0;
                                    Int16 XRubro = 0;
                                    DataTable auxDT = NegFactura.recuperaCodRubro(codpro);
                                    foreach (DataRow row in auxDT.Rows)
                                    {
                                        XRubro = Convert.ToInt16(row[0].ToString());
                                        xcodDiv = Convert.ToInt32(row[1].ToString());
                                    }

                                    NegPedidos.GuardaPedDetalleHonorarios(pdd_codigo, Pedido, item.CODPRO,
                                        solicitud.PRO_DESCRIPCION, Convert.ToDecimal(solicitud.PDD_CANTIDAD), item.VALOR, Convert.ToDecimal(solicitud.PDD_IVA),
                                        item.VALOR, pedido, XRubro, xcodDiv, item.numdoc, Sesion.bodega);

                                    //NegPedidos.crearDetallePedido(solicitud, pedido, XRubro, xcodDiv, item.numdoc);
                                }
                                else
                                {
                                    NegPedidos.GuardaPedDetalleHonorarios(pdd_codigo, Pedido, item.CODPRO,
                                        pedido.PED_DESCRIPCION, Convert.ToDecimal(solicitud.PDD_CANTIDAD), item.VALOR, Convert.ToDecimal(solicitud.PDD_IVA),
                                        item.VALOR, pedido, Rubro, Convert.ToInt32(ar.DIV_CODIGO), item.numdoc, Sesion.bodega);

                                    //NegPedidos.crearDetallePedido(solicitud, pedido, Rubro, Convert.ToInt32(ar.DIV_CODIGO), item.numdoc);
                                }
                                DtoDetalleHonorariosMedicos HonorariosM = new DtoDetalleHonorariosMedicos();
                                HonorariosM = item;
                                if (HonorariosM != null)
                                {
                                    HonorariosM.PED_CODIGO = Pedido;
                                    HonorariosM.ID_LINEA = pdd_codigo;
                                    NegPedidos.GuardaDetalleHonorarios(HonorariosM);
                                }
                                totales = totales + Convert.ToDouble(HonorariosMedico.VALOR);
                                valores = valores + Convert.ToDouble(HonorariosMedico.VALOR);
                                ivaTotal = ivaTotal + Convert.ToDouble(solicitud.PDD_IVA);
                            }
                        }
                    }
                    listaCuenta = NegDetalleCuenta.recuperarCuentaPaciente(ultimaAtencion.ATE_CODIGO);
                    cargarDetalleFactura();
                    MessageBox.Show("Pedido No." + Pedido.ToString() + " fue ingresado correctamente.", "His3000");

                    if (mushuñan)
                    {
                        Int16 AreaUsuario = 1;
                        DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario));
                        bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
                        if (parse)
                        {
                            //ACTUALIZA DENTRO DEL KARDEX EL NUMERO DE LA BODEGA
                            switch (AreaUsuario)
                            {
                                case 2:
                                    //if (!NegQuirofano.ActualizarKardexSicMushuñan(pedido.PED_CODIGO.ToString())) // Se comenta por el cambio a IP-Bodega // Mario 15/02/2023
                                    //{
                                    //    MessageBox.Show("Kardex no ha sido actualizado con el pedido: " + pedido.PED_CODIGO, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning); //en el caso que no actulice el kardex con la bodega de mushuñan se debera hacer esto manual ya que si hay error se hara un rollback
                                    //}
                                    /*impresion*/
                                    frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Pedido, ar.PEA_CODIGO, 1, 5);
                                    frmPedidos.Show();

                                    //CREO EL DESPACHO AUTOMATICO PARA MUSHUÑAN
                                    List<DtoDespachos> despachos = new List<DtoDespachos>();
                                    DtoDespachos xdespacho = new DtoDespachos();

                                    xdespacho.observacion = "DESPACHADO DESDE MUSHUÑAN";

                                    xdespacho.ped_codigo = Convert.ToInt64(Pedido);

                                    despachos.Add(xdespacho);

                                    if (!NegPedidos.InsertarDespachos(despachos, 0, DateTime.Now))
                                    {
                                        MessageBox.Show("No se pudo realizar despacho automatico." +
                                            "\r\nIntentelo manual desde el modulo de pedidos - Control despachos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    break;
                                case 3:
                                    //if (!NegQuirofano.ActualizarKardexSicBrigada(pedido.PED_CODIGO.ToString())) // Se comenta por el cambio a IP-Bodega // Mario 15/02/2023
                                    //{
                                    //    MessageBox.Show("Kardex no ha sido actualizado con el pedido: " + pedido.PED_CODIGO, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning); //en el caso que no actulice el kardex con la bodega de mushuñan se debera hacer esto manual ya que si hay error se hara un rollback
                                    //}
                                    /*impresion*/
                                    frmImpresionPedidos frmPedidos1 = new frmImpresionPedidos(Pedido, ar.PEA_CODIGO, 1, 5);
                                    frmPedidos1.Show();

                                    //CREO EL DESPACHO AUTOMATICO PARA BRIGADA
                                    List<DtoDespachos> despachos2 = new List<DtoDespachos>();
                                    DtoDespachos xdespacho2 = new DtoDespachos();

                                    xdespacho2.observacion = "DESPACHADO DESDE BRIGADA MEDICA";

                                    xdespacho2.ped_codigo = Convert.ToInt64(Pedido);

                                    despachos2.Add(xdespacho2);

                                    if (!NegPedidos.InsertarDespachos(despachos2, 0, DateTime.Now))
                                    {
                                        MessageBox.Show("No se pudo realizar despacho automatico." +
                                            "\r\nIntentelo manual desde el modulo de pedidos - Control despachos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        /*impresion*/
                        PARAMETROS_DETALLE pd = NegParametros.RecuperaPorCodigo(68); // se aumenta por parametro para impresion con valores y sin valores // Mario Valencia // 04-01-2024
                        frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Pedido, ar.PEA_CODIGO, 1, Convert.ToInt32(pd.PAD_VALOR));
                        frmPedidos.Show();
                    }





                    //Cambios Edgar 20210519
                    //DataTable HonorarioReporte = NegDietetica.getDataTable("getHMCtasPctes", ultimaAtencion.ATE_CODIGO.ToString());
                    DataTable HonorarioReporte = NegDietetica.ReporteHonorario(ultimaAtencion.ATE_CODIGO, Pedido);
                    DataTable PacienteReferido = NegDetalleCuenta.ReferidoPaciente(ultimaAtencion.ATE_CODIGO);
                    His.Formulario.DSHonorarioReporte Honorario = new His.Formulario.DSHonorarioReporte();
                    if (HonorarioReporte.Rows.Count > 0)
                    {
                        MEDICOS med = new MEDICOS();
                        DataRow drHonorarios;
                        NegCertificadoMedico cer = new NegCertificadoMedico();
                        foreach (DataRow item in HonorarioReporte.Rows)
                        {
                            med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(item[0].ToString()));
                            drHonorarios = Honorario.Tables["Honorario"].NewRow();
                            drHonorarios["Empresa"] = Sesion.nomEmpresa;
                            drHonorarios["Logo"] = cer.path();
                            drHonorarios["Paciente"] = txt_ApellidoH1.Text.Trim();
                            drHonorarios["Medico"] = item[1].ToString();
                            drHonorarios["IdentificacionP"] = pacienteFactura.PAC_IDENTIFICACION;
                            drHonorarios["IdentificacionM"] = med.MED_RUC.Substring(0, 10);
                            drHonorarios["Hc"] = pacienteFactura.PAC_HISTORIA_CLINICA;
                            drHonorarios["Hab"] = txt_Habitacion_P.Text.Trim();
                            drHonorarios["Atencion"] = txt_Atencion.Text.Trim();
                            drHonorarios["Fecha"] = item[2].ToString();
                            drHonorarios["Valor"] = item[3].ToString();
                            drHonorarios["Vale"] = item[4].ToString();
                            drHonorarios["Descripcion"] = "HONORARIOS MEDICOS";
                            drHonorarios["Referido"] = PacienteReferido.Rows[0][0].ToString();
                            drHonorarios["Seguro"] = lblConvenio.Text;
                            List<ESPECIALIDADES_MEDICAS> especialidad = NegEspecialidades.ListaEspecialidades();
                            string esp = especialidad.FirstOrDefault(m => m.EntityKey == med.ESPECIALIDADES_MEDICASReference.EntityKey).ESP_NOMBRE;
                            drHonorarios["Especialidad"] = esp;

                            Honorario.Tables["Honorario"].Rows.Add(drHonorarios);

                        }
                        His.Formulario.frmReportes x = new His.Formulario.frmReportes(1, "HonorarioReporte", Honorario);
                        x.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Este pedido no se guardara porque la red esta inestable consulte con el administrador y vuelva a intentar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }
        private void garantiaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void label20_Click(object sender, EventArgs e)
        {

        }
        private void chk_Cedula_Click(object sender, EventArgs e)
        {
            chk_pasaporte.Checked = false;
            chk_Ruc.Checked = false;
            chbConsumidorFinal.Checked = false;
            chk_Cedula.Checked = true;
        }
        private void chk_Ruc_Click(object sender, EventArgs e)
        {
            chk_pasaporte.Checked = false;
            chk_Cedula.Checked = false;
            chbConsumidorFinal.Checked = false;
            chk_Ruc.Checked = true;
        }
        private void chk_pasaporte_Click(object sender, EventArgs e)
        {
            chk_Cedula.Checked = false;
            chk_Ruc.Checked = false;
            chbConsumidorFinal.Checked = false;
            chk_pasaporte.Checked = true;
        }
        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }
        private void btnNotaCredito_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("USTED VA REALIZAR UNA NOTA DE CREDITO QUE AFECTARA A LA FACTURA Nº: " +
                txt_Factura_Cod3.Text + " DEL CLIENTE: " + txt_Cliente.Text,
                "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (MessageBox.Show("UNA VEZ GENERADA LA NOTA DE CREDITO LA FACTURA Nº:" +
                    txt_Factura_Cod3.Text + "NO TENDRA VALOR TRIBUTARIO", "HIS3000",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    bool TipoDocumento = false;
                    GeneraFacturaElectronica(ref TipoDocumento);
                }
                else
                    MessageBox.Show("USTED DECIDIO CANCELAR LA GENERACIÓN DE UNA NOTA DE CREDITO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void facturaElectronicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ultimaAtencion.ESC_CODIGO == 6)
            {
                if (MessageBox.Show("EL XML DE ESTÁ FACTURA ELECTRONICA SE ENVIARA NUEVAMENTE AL SRI Y AL CLIENTE!!!!", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    txtfacturaelectronica.Text = "";
                    bool TipoDocumento = true;
                    GeneraFacturaElectronica(ref TipoDocumento);
                }

            }
            else
                MessageBox.Show("No se puede generar una factura electrónica de una cuenta sin facturar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void dgvDescuento_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvDescuento.CurrentCell.ColumnIndex == 3)
            {
                DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;
                dText.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
                dText.KeyPress += new KeyPressEventHandler(dText_KeyPress);
            }
        }
        void dText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '.')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void dgvDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void chbDescuento_Click(object sender, EventArgs e)
        {

        }
        //divide factura Pablo Rocha 01-12-2018
        private void dgvDivideFactura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDivideFactura.Columns[e.ColumnIndex].Name == "noFactura")
            {
                DataGridViewRow row = dgvDivideFactura.Rows[e.RowIndex];
                DataGridViewCheckBoxCell cellSelection = row.Cells["noFactura"] as DataGridViewCheckBoxCell;

                //Cambios Edgar 20210315 
                if (dgvDivideFactura.Rows[e.RowIndex].Cells["noFactura"].Value == null)
                {
                    dgvDivideFactura.Rows[e.RowIndex].Cells["noFactura"].Value = true;
                    decimal valorSubtotal = Convert.ToDecimal(txt_SubTotal.Text) - (Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value.ToString()) + Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[4].Value.ToString()));
                    txt_SubTotal.Text = valorSubtotal.ToString("N2");
                    decimal valorSinIva = Convert.ToDecimal(txt_SinIVA.Text) - Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[4].Value.ToString());
                    txt_SinIVA.Text = valorSinIva.ToString("N2");
                    decimal valorConIva = Convert.ToDecimal(txt_ConIVA.Text) - Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value.ToString());
                    txt_ConIVA.Text = valorConIva.ToString("N2");
                    decimal ivatotal = Convert.ToDecimal(txt_IVA.Text) - Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[2].Value.ToString());
                    txt_IVA.Text = ivatotal.ToString("N2");

                    txt_Total.Text = (Convert.ToDecimal(txt_SinIVA.Text) + Convert.ToDecimal(txt_ConIVA.Text) + Convert.ToDecimal(txt_IVA.Text)).ToString("N2");
                    dgvDivideFactura.Rows[e.RowIndex].Cells[5].Value = "S";
                }
                else
                {
                    if (Convert.ToBoolean(dgvDivideFactura.Rows[e.RowIndex].Cells["noFactura"].Value))
                    {
                        dgvDivideFactura.Rows[e.RowIndex].Cells["noFactura"].Value = false;

                        decimal valorSubtotal = Convert.ToDecimal(txt_SubTotal.Text) + (Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value.ToString()) + Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[4].Value.ToString()));
                        txt_SubTotal.Text = valorSubtotal.ToString("N2");
                        decimal valorSinIva = Convert.ToDecimal(txt_SinIVA.Text) + Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[4].Value.ToString());
                        txt_SinIVA.Text = valorSinIva.ToString("N2");
                        decimal valorConIva = Convert.ToDecimal(txt_ConIVA.Text) + Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value.ToString());
                        txt_ConIVA.Text = valorConIva.ToString("N2");
                        decimal ivatotal = Convert.ToDecimal(txt_IVA.Text) + Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[2].Value.ToString());
                        txt_IVA.Text = ivatotal.ToString("N2");

                        txt_Total.Text = (Convert.ToDecimal(txt_SinIVA.Text) + Convert.ToDecimal(txt_ConIVA.Text) + Convert.ToDecimal(txt_IVA.Text)).ToString("N2");
                        dgvDivideFactura.Rows[e.RowIndex].Cells[5].Value = "N";
                    }
                    else
                    {
                        dgvDivideFactura.Rows[e.RowIndex].Cells["noFactura"].Value = true;
                        decimal valorSubtotal = Convert.ToDecimal(txt_SubTotal.Text) - (Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value.ToString()) + Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[4].Value.ToString()));
                        txt_SubTotal.Text = valorSubtotal.ToString("N2");
                        decimal valorSinIva = Convert.ToDecimal(txt_SinIVA.Text) - Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[4].Value.ToString());
                        txt_SinIVA.Text = valorSinIva.ToString("N2");
                        decimal valorConIva = Convert.ToDecimal(txt_ConIVA.Text) - Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value.ToString());
                        txt_ConIVA.Text = valorConIva.ToString("N2");
                        decimal ivatotal = Convert.ToDecimal(txt_IVA.Text) - Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[2].Value.ToString());
                        txt_IVA.Text = ivatotal.ToString("N2");

                        txt_Total.Text = (Convert.ToDecimal(txt_SinIVA.Text) + Convert.ToDecimal(txt_ConIVA.Text) + Convert.ToDecimal(txt_IVA.Text)).ToString("N2");
                        dgvDivideFactura.Rows[e.RowIndex].Cells[5].Value = "S";
                    }
                }
                //---------------------------------------------------------------------------------



                //anterior 20210315
                //if (Convert.ToBoolean(cellSelection.Value) == true)
                //{
                //    double ValorRubro = Convert.ToDouble(dgvDivideFactura.Rows[e.RowIndex].Cells[2].Value.ToString());
                //    double ValorNeto = Convert.ToDouble(txt_SubTotal.Text);
                //    txt_SubTotal.Text = Convert.ToString(ValorNeto + ValorRubro);
                //    txt_TotalCSIva.Text = txt_SubTotal.Text;
                //    if (dgvDivideFactura.Rows[e.RowIndex].Cells[0].Value.ToString() == "27")
                //    {
                //        txt_ConIVA.Text = Convert.ToString(Convert.ToDouble(txt_ConIVA.Text) + ValorRubro);
                //        double iva = Convert.ToDouble(txt_ConIVA.Text) * 0.12;
                //        txt_IVA.Text = Convert.ToString(Math.Round(iva, 2));
                //    }
                //    else
                //        txt_SinIVA.Text = String.Format("{0:0.00}", Math.Round(((Convert.ToDouble(txt_SinIVA.Text) + Convert.ToDouble(txt_Descuento.Text) + ValorRubro))), 2);
                //    txt_Total.Text = String.Format("{0:0.00}", Math.Round(((Convert.ToDouble(txt_SinIVA.Text) + Convert.ToDouble(txt_ConIVA.Text) + Convert.ToDouble(txt_IVA.Text)))), 2);

                //    lblConIva.Text = Convert.ToString(Convert.ToDouble(txt_ConIVA.Text) + Convert.ToDouble(txt_IVA.Text));
                //    dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value = "N";
                //    dgvDivideFactura.Rows[e.RowIndex].Cells[4].Selected = false;
                //    dgvDivideFactura.CurrentCell = dgvDivideFactura.Rows[e.RowIndex].Cells[2];
                //    dgvDivideFactura.Rows[e.RowIndex].Cells[2].Selected = true;

                //}
                //else
                //{
                //    decimal valorSubtotal = Convert.ToDecimal(txt_SubTotal.Text) - (Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value.ToString()) + Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[4].Value.ToString()));
                //    txt_SubTotal.Text = valorSubtotal.ToString("N2");
                //    decimal valorSinIva = Convert.ToDecimal(txt_SinIVA.Text) - Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[4].Value.ToString());
                //    txt_SinIVA.Text = valorSinIva.ToString("N2");
                //    decimal valorConIva = Convert.ToDecimal(txt_ConIVA.Text) - Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value.ToString());
                //    txt_ConIVA.Text = valorConIva.ToString("N2");
                //    decimal ivatotal = Convert.ToDecimal(txt_IVA.Text) - Convert.ToDecimal(dgvDivideFactura.Rows[e.RowIndex].Cells[2].Value.ToString());
                //    txt_IVA.Text = ivatotal.ToString("N2");

                //    txt_Total.Text = (Convert.ToDecimal(txt_SinIVA.Text) + Convert.ToDecimal(txt_ConIVA.Text) + Convert.ToDecimal(txt_IVA.Text)).ToString("N2");
                //    dgvDivideFactura.Rows[e.RowIndex].Cells[5].Value = "S";
                //}
                //anterior----------



                //double ValorRubro = Convert.ToDouble(dgvDivideFactura.Rows[e.RowIndex].Cells[2].Value.ToString());
                //double ValorNeto = Convert.ToDouble(txt_SubTotal.Text);
                //txt_SubTotal.Text = Convert.ToString(ValorNeto - ValorRubro);
                //txt_TotalCSIva.Text = txt_SubTotal.Text;
                //if (dgvDivideFactura.Rows[e.RowIndex].Cells[0].Value.ToString() == "27")
                //{
                //    txt_ConIVA.Text = Convert.ToString(Convert.ToDouble(txt_ConIVA.Text) - ValorRubro);
                //    double iva = Convert.ToDouble(txt_ConIVA.Text) * 0.12;
                //    txt_IVA.Text = Convert.ToString(Math.Round(iva, 2));
                //}
                //else
                //    txt_SinIVA.Text = Convert.ToString(Convert.ToDouble(txt_SinIVA.Text) - Convert.ToDouble(txt_Descuento.Text) - ValorRubro);
                //txt_Total.Text = Convert.ToString(Convert.ToDouble(txt_SinIVA.Text) + Convert.ToDouble(txt_ConIVA.Text) + Convert.ToDouble(txt_IVA.Text));
                //lblConIva.Text = Convert.ToString(Convert.ToDouble(txt_ConIVA.Text) + Convert.ToDouble(txt_IVA.Text));
                //dgvDivideFactura.Rows[e.RowIndex].Cells[3].Value = "S";
                //dgvDivideFactura.Rows[e.RowIndex].Cells[4].Selected = false;
                //dgvDivideFactura.CurrentCell = dgvDivideFactura.Rows[e.RowIndex].Cells[2];
                //dgvDivideFactura.Rows[e.RowIndex].Cells[2].Selected = true;

            }
        }

        private void chbCopago_Click(object sender, EventArgs e)
        {
            PARAMETROS_DETALLE pd = NegParametros.RecuperaPorCodigo(70);
            if (pd.PAD_VALOR != "1")
            {
                chbDescuento.Checked = false;
                cargarDetalleFactura();
                if (chbCopago.Checked == true)
                    chbGuardaCopago.Visible = true;

                else
                    chbGuardaCopago.Visible = false;
            }
        }
        private void descuentoGeneral(Int32 codAte, Int32 CodRub)
        {
        }

        double valorFacturaIva = 0, valorFactutaConIva = 0;
        decimal[] ivaFactura = new decimal[100];
        decimal[] totalivaFactura = new decimal[100];
        decimal[] totalsinFactura = new decimal[100];
        decimal[] iva = new decimal[100];
        decimal[] totaliva = new decimal[100];
        decimal[] totalsin = new decimal[100];
        private void dgvDescuento_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvDescuento.AllowUserToAddRows = false;

            if (chbCopago.Checked)
            {
                if (dgvDescuento.Rows[e.RowIndex].Cells[0].Value.ToString() != "46")
                {
                    if (ivaFactura[e.RowIndex] == 0 && totalivaFactura[e.RowIndex] == 0 && totalsinFactura[e.RowIndex] == 0)
                    {
                        ivaFactura[e.RowIndex] = Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[2].Value);
                        totalivaFactura[e.RowIndex] = Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[3].Value);
                        totalsinFactura[e.RowIndex] = Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[4].Value);
                    }
                    decimal copagoiva = 0;
                    decimal copagocon = 0;
                    decimal copagosin = 0;
                    if (dgvDescuento.Rows[e.RowIndex].Cells[5].Value.ToString() != "0.00" && dgvDescuento.Rows[e.RowIndex].Cells[5].Value.ToString() != "0")
                    {
                        iva[e.RowIndex] = (Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[2].Value) * Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[5].Value)) / 100;
                        totaliva[e.RowIndex] = (Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[3].Value) * Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[5].Value)) / 100;
                        totalsin[e.RowIndex] = (Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[4].Value) * Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[5].Value)) / 100;
                        dgvDescuento.Rows[e.RowIndex].Cells[2].Value = (Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[2].Value) - iva[e.RowIndex]).ToString("N2");
                        dgvDescuento.Rows[e.RowIndex].Cells[3].Value = (Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[3].Value) - totaliva[e.RowIndex]).ToString("N2");
                        dgvDescuento.Rows[e.RowIndex].Cells[4].Value = (Convert.ToDecimal(dgvDescuento.Rows[e.RowIndex].Cells[4].Value) - totalsin[e.RowIndex]).ToString("N2");
                    }
                    else
                    {
                        dgvDescuento.Rows[e.RowIndex].Cells[2].Value = ivaFactura[e.RowIndex];
                        dgvDescuento.Rows[e.RowIndex].Cells[3].Value = totalivaFactura[e.RowIndex];
                        dgvDescuento.Rows[e.RowIndex].Cells[4].Value = totalsinFactura[e.RowIndex];
                    }
                    for (int i = 0; i < dgvDescuento.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(dgvDescuento.Rows[i].Cells[5].Value) != 0)
                        {
                            copagoiva += iva[i];
                            copagocon += totaliva[i];
                            copagosin += totalsin[i];
                        }
                    }
                    dgvDescuento.Rows[dgvDescuento.Rows.Count - 1].Cells[2].Value = copagoiva.ToString("N2");
                    dgvDescuento.Rows[dgvDescuento.Rows.Count - 1].Cells[3].Value = copagocon.ToString("N2");
                    dgvDescuento.Rows[dgvDescuento.Rows.Count - 1].Cells[4].Value = copagosin.ToString("N2");
                    copagoiva = 0;
                    copagocon = 0;
                    copagosin = 0;
                }
                else
                    dgvDescuento.Rows[e.RowIndex].Cells[5].Value = 0.00;
            }
        }
        private void chbGuardaCopago_Click(object sender, EventArgs e)
        {
            chbGuardaCopago.Enabled = false;
            dgvDescuento.Enabled = false;
            decimal subTotal = 0, sinIva = 0, conIva = 0, iva = 0;

            for (int i = 0; i < dgvDescuento.Rows.Count; i++)
            {
                iva += Convert.ToDecimal(dgvDescuento.Rows[i].Cells[2].Value);
                subTotal += Convert.ToDecimal(dgvDescuento.Rows[i].Cells[3].Value) + Convert.ToDecimal(dgvDescuento.Rows[i].Cells[4].Value);
                conIva += Convert.ToDecimal(dgvDescuento.Rows[i].Cells[3].Value);
                sinIva += Convert.ToDecimal(dgvDescuento.Rows[i].Cells[4].Value);

                gridDetalleFactura.Rows[i].Cells["SUBTOTAL"].Value = Convert.ToDecimal(dgvDescuento.Rows[i].Cells[3].Value) + Convert.ToDecimal(dgvDescuento.Rows[i].Cells[4].Value);
                gridDetalleFactura.Rows[i].Cells["IVA"].Value = dgvDescuento.Rows[i].Cells[2].Value;
                gridDetalleFactura.Rows[i].Cells["TOTAL CON IVA"].Value = dgvDescuento.Rows[i].Cells[3].Value;
                gridDetalleFactura.Rows[i].Cells["TOTAL SIN IVA"].Value = dgvDescuento.Rows[i].Cells[4].Value;
                gridDetalleFactura.Rows[i].Cells["TOTAL"].Value = Convert.ToDecimal(dgvDescuento.Rows[i].Cells[3].Value) + Convert.ToDecimal(dgvDescuento.Rows[i].Cells[4].Value);
            }

            txt_SubTotal.Text = subTotal.ToString("N2");
            txt_TotalCSIva.Text = subTotal.ToString("N");
            txt_SinIVA.Text = sinIva.ToString("N");
            txt_ConIVA.Text = conIva.ToString("N");
            txt_IVA.Text = iva.ToString("N");
            txt_Total.Text = (iva + sinIva + conIva).ToString("N2");
            ultraTabControl2.SelectedTab = ultraTabControl2.Tabs["detalle"];

        }

        private void chbCopago_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCopago.Checked)
            {
                PARAMETROS_DETALLE pd = NegParametros.RecuperaPorCodigo(70);
                if (pd.PAD_VALOR == "1")
                {
                    frmCopagos frm = new frmCopagos(txt_Atencion.Text, txt_Historia_Pc.Text);
                    frm.ShowDialog();
                    if (frm.ate_codigo1 != 0 && frm.pac_codigo != 0)
                        cargaFactura(frm.ate_codigo1, frm.pac_codigo);
                }
                else
                {
                    if (VerificaAltaPaciente())
                    {
                        ultraTabControl2.Tabs["descuento"].Visible = false; //invisible
                        NegFactura.ActualizaDescuentoAtencion(Convert.ToInt64(txt_Atencion.Text));//encerar descuentos
                                                                                                  //alex////////////////////////            
                        cargarDetalleFactura();
                        /////////////////////////////
                        if (ultraTabControl2.Tabs["descuento"].Visible == true)
                        {
                            if (chbCopago.Checked)
                            {
                                dgvDescuento.Columns[" % DESCUENTO"].ReadOnly = false;
                                button2.Visible = false;
                                cboTipoDescuento.Visible = false;
                                label47.Visible = false;
                            }
                            else if (chbCopago.Checked == false)
                            {
                                dgvDescuento.Columns[" % DESCUENTO"].ReadOnly = true;
                                button2.Visible = true;
                                cboTipoDescuento.Visible = true;
                                label47.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        chbCopago.Checked = false;
                        MessageBox.Show("El Paciente Debe Estar Dado El Alta Para Generar Copago", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (cboTipoDescuento.SelectedItem != null)
            {
                Int32 codRubro = Convert.ToInt32(GetValorCelda(this.dgvDescuento, 0));
                codRubro = Convert.ToInt32(this.dgvDescuento.Rows[dgvDescuento.CurrentRow.Index].Cells["RUBRO"].Value);
                double total = Convert.ToDouble(this.dgvDescuento.Rows[dgvDescuento.CurrentRow.Index].Cells["TOTAL SIN IVA"].Value) + Convert.ToDouble(this.dgvDescuento.Rows[dgvDescuento.CurrentRow.Index].Cells["TOTAL CON IVA"].Value);

                frmCorreccioDesc Form = new frmCorreccioDesc(Convert.ToInt32(txt_Atencion.Text), codRubro);
                Form.ShowDialog(); ///realiza el descuento en la otra ventana

                cargarDetalleFactura();

                double sumaDescuento = Form.descuentos[1] + Form.descuentos[2];
                double porDescuento = (sumaDescuento * 100) / total;
                this.dgvDescuento.Rows[dgvDescuento.CurrentRow.Index].Cells["% DESCUENTO"].Value = porDescuento.ToString("#####0.00");
                this.dgvDescuento.Rows[dgvDescuento.CurrentRow.Index].Cells["DESCUENTO TOTAL"].Value = sumaDescuento.ToString("#####0.00");
            }
            else
            {
                MessageBox.Show("Escoja un tipo de descuento para continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static string GetValorCelda(DataGridView dgv, int num)
        {
            string valor = "";
            valor = dgv.Rows[dgv.CurrentRow.Index].Cells[num].Value.ToString();
            return valor;
        }
        private void dgvDescuento_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDescuento_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void dgvDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    e.Handled = true;
                    //dar un nombre a los controles
                    txtObserva.Visible = true;
                    txtObserva.Text = "";
                    txtObserva.Text = "DIAGNOSTICO: ";
                    txtObserva.Focus();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvDivideFactura_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    e.Handled = true;
                    //dar un nombre a los controles
                    txtObserva.Visible = true;
                    txtObserva.Text = "";
                    txtObserva.Text = "DIAGNOSTICO: ";
                    txtObserva.Focus();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void CargarTiposDesuento()
        {
            cboTipoDescuento.Items.Clear();
            DataTable auxDT = NegFactura.TiposDescuento();
            //cboTipoDescuento.DataSource = auxDT;
            //cboTipoDescuento.ValueMember = "ctaContable";
            //cboTipoDescuento.DisplayMember = "tipos";
            foreach (DataRow row in auxDT.Rows)
            {
                cboTipoDescuento.Items.Add(row[0].ToString());
            }
            auxDT = NegFactura.TipoDescuentoAtencionVer(Convert.ToInt32(txt_Atencion.Text));
            foreach (DataRow row in auxDT.Rows)
            {
                cboTipoDescuento.Text = (row[0].ToString());
            }
        }
        private void cboTipoDescuento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Int32 CodigoAtencion = Convert.ToInt32(txt_Atencion.Text);
            Int32 CodigoDescento = Convert.ToInt32(cboTipoDescuento.Text.Substring(0, 2).Replace(" ", ""));
            NegFactura.TipoDescuentoAtencionActualizar(codigoAtencion, CodigoDescento);
        }
        private void cmbauxarea_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        private void cmbauxarea_TextUpdate(object sender, EventArgs e)
        {

        }
        private void cmbauxarea_TextChanged(object sender, EventArgs e)
        {
            if (cmb_Areas.Text != "TODAS LAS AREAS")
            {
                cmb_Rubros.Enabled = true;
            }
            else
            {
                cmb_Rubros.Enabled = false;
            }
        }
        private void cmbauxarea_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txt_telef_Cliente_Validated(object sender, EventArgs e)
        {
            txt_telef_Cliente.Text = txt_telef_Cliente.Text.ToString().Trim();
            if (txt_telef_Cliente.TextLength > 10)
            {
                AgregarError(txt_telef_Cliente);
            }
        }

        private void txt_telef_Cliente_Leave(object sender, EventArgs e)
        {
            //txt_telef_Cliente.Text = txt_telef_Cliente.Text.ToString().Trim();
            //if (txt_telef_Cliente.TextLength > 10)
            //{
            //    AgregarError(txt_telef_Cliente);
            //}
        }
        private void gridDetalleFactura_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string rubro = ""; //Cambios Edgar 20210312 variable que recibira el nombre del rubro de honorarios para mostrar los medicos, no afectara el funcionamiento anterior.
            if (gridDetalleFactura.Rows.Count > 0)
            {
                UltraGridRow fila = gridDetalleFactura.ActiveRow;
                try
                {
                    Int32 codRubro = Convert.ToInt32(gridDetalleFactura.Rows[gridDetalleFactura.ActiveRow.Index].Cells["INDICE"].Value.ToString());
                    rubro = fila.Cells["RUBRO"].Value.ToString();
                    frmCorreccioDesc.rubro = rubro;
                    frmCorreccioDesc Form = new frmCorreccioDesc(Convert.ToInt32(txt_Atencion.Text), codRubro, true);
                    Form.ShowDialog();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void btnAuditaCuenta_Click(object sender, EventArgs e)
        {
            COPAGO copor = NegCopago.recuperaCopago(Convert.ToInt64(txt_Atencion.Text));
            if (copor != null)
            {
                MessageBox.Show("No se puede revertir el estado de una atencion Copago", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDouble(txt_Descuento.Text) > 0)
            {
                DialogResult result = MessageBox.Show("La siguiente acciòn eliminara los descuentos ingresados y habilitara la cuenta en el módulo de habitaciones. Desea continuar?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    NegFactura.ActualizaDescuentoAtencion(Convert.ToInt64(txt_Atencion.Text));
                    ultraTabControl2.Tabs["descuento"].Visible = false;
                    cargarDetalleFactura();
                    RevertirAtencion();
                }
            }
            else
            {
                ultraTabControl2.Tabs["descuento"].Visible = false;
                RevertirAtencion();
                //if (facturaPrefactura != 2)
            }

            //Form frm = new frmAuditoriaCuenta(txt_Historia_Pc.Text.Trim(), txt_Atencion.Text.Trim(), txt_ApellidoH1.Text.Trim(), lblConvenio.Text.Trim());
            //frm.ShowDialog();
            //NegFactura.ActualizaDescuentoAtencion(Convert.ToInt16(txt_Atencion.Text));

            //NegValidaciones.alzheimer();// LIBERA MEMORIA


            //if (txt_Historia_Pc.Text.Trim() != "")
            //{                
            //    cargarDetalleFactura();                
            //    if (Convert.ToInt16(ultimaAtencion.ESC_CODIGO) == 6)
            //    {
            //        habiltarBotones(false, false, false, true, true, true, true);
            //        btnSolicitar.Enabled = false;
            //        btnAnticipos.Enabled = false;
            //        btnGeneraValores.Enabled = false;
            //        btnAltaPaciente.Enabled = false;
            //    }
            //    else
            //    {
            //        habiltarBotones(false, true, false, true, true, true, true);
            //        btnSolicitar.Enabled = true;
            //        btnAnticipos.Enabled = true;
            //        btnGeneraValores.Enabled = true;
            //        btnAltaPaciente.Enabled = true;
            //    }               
            //    Int32 atencion = Convert.ToInt32(this.txt_Atencion.Text.Trim());
            //    if (atencion > 0)
            //    {
            //        DataTable auxDT = NegFactura.fechasINOUT(atencion);
            //        foreach (DataRow row in auxDT.Rows)
            //        {
            //            cboTipoDescuento.Items.Add(row[0].ToString());
            //            this.dtp_FechaIngreso.Text = row[0].ToString();
            //            if (String.IsNullOrEmpty(row[1].ToString()))
            //            {
            //                this.txt_fechaalta.Text = "";
            //            }
            //            else
            //            {
            //                this.txt_fechaalta.Text = row[1].ToString();
            //            }
            //        }

            //    }
            //}
            //CargarFormasPago();
        }
        //private void RecertirEstadoHabitacion(HABITACIONES habitacion)
        //{
        //    Int16 codigo = 0;
        //    try
        //    {
        //        //if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionDisponible())
        //        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO != 1)
        //        {
        //            DataTable revertirHabitacion = new DataTable();
        //            revertirHabitacion = NegHabitaciones.RevertirMovimientoHabitacion(habitacion);
        //            try
        //            {
        //                if (revertirHabitacion.Rows.Count == 0)
        //                    MessageBox.Show("Paciente Ya Tiene Factura No Se Puede Revertir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                else
        //                    MessageBox.Show("Reversión De Habitación Exitosa", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //                //if (arbolHabitaciones.SelectedItem != null)
        //                //{
        //                //    TreeViewItem itemH = (TreeViewItem)arbolHabitaciones.SelectedItem;
        //                //    codigo = Convert.ToInt16(itemH.Tag);
        //                //}

        //                //if (control == 1)
        //                //{
        //                //    _habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
        //                //    CargarHabitacionesVistaGen(_habitacionesLista);
        //                //}
        //                //else
        //                //{
        //                //    _habitacionesLista = NegHabitaciones.listaHabitaciones(codigo).OrderBy(h => h.hab_Numero).ToList();
        //                //    CargarHabitacionesVistaGen(_habitacionesLista);
        //                //}
        //                ////cargo pacientes
        //                ////var otr0 = _pacientes.Where(p => p.codigoHabitacion == habitacion.hab_Codigo).OrderByDescending(p => p.fechaIngreso).Select(p => p.historiaClincia).FirstOrDefault();
        //                //DataTable InformacionPaciente = new DataTable();
        //                //InformacionPaciente = NegHabitaciones.InformacionPaciente(habitacion);
        //                //DataTable codAtencion = NegPacientes.registropaciente(InformacionPaciente.Rows[0][0].ToString().Trim(), 2);
        //                //string codAtencioP = codAtencion.Rows[0]["ATE_CODIGO"].ToString();
        //                //HABITACIONES_HISTORIAL habitacionHistorial = new HABITACIONES_HISTORIAL();
        //                //habitacionHistorial.HAH_CODIGO = NegHabitacionesHistorial.RecuperaMaximoHabitacionHistorial();
        //                //habitacionHistorial.ATE_CODIGO = Convert.ToInt32(codAtencioP.Trim());
        //                //habitacionHistorial.HAB_CODIGO = (habitacion.hab_Codigo);
        //                //habitacionHistorial.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
        //                //habitacionHistorial.HAH_FECHA_INGRESO = DateTime.Now;
        //                //habitacionHistorial.HAD_OBSERVACION = "Se revierte  el  estado ";
        //                //habitacionHistorial.HAH_ESTADO = 1;
        //                //NegHabitacionesHistorial.CrearHabitacionHistorial(habitacionHistorial);
        //                //_pacientes = NegPacientes.RecuperarPacientesAtencionUltimasReporte(0, true, null, null, 0);
        //                ////_pacientes = NegHabitaciones.sp_HabitacionesCenso(1);
        //                //xamDataPresenterPacientes.DataSource = _pacientes.ToList();
        //                //WindowLoaded(null, null);
        //            }
        //            catch
        //            {
        //                MessageBox.Show("aqui es el error al revertir");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Por favor seleccione una habitación disponible", "Advertencia", MessageBoxButtons.OK,
        //                            MessageBoxIcon.Exclamation);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message);
        //    }
        //}
        private void btnNuevo_ButtonClick(object sender, EventArgs e)
        {

        }

        private void txt_Cliente_Leave(object sender, EventArgs e)
        {

        }

        private void btnBuscaClienteSic_Click(object sender, EventArgs e)
        {
            try
            {
                frmRecuperaClienteSIC from = new frmRecuperaClienteSIC();
                from.IDENTIFICADOR = txt_Ruc;
                from.ShowDialog();
                DataTable OtroCliente = new DataTable();
                OtroCliente = NegDetalleCuenta.RecuperaOtroCliente(txt_Ruc.Text);
                foreach (DataRow Item in OtroCliente.AsEnumerable())
                {
                    txt_Cliente.Text = Item["nomcli"].ToString();
                    if (Item["telcli"].ToString().Any(x => !char.IsNumber(x)))
                    {
                        txt_telef_Cliente.Text = Regex.Replace(Item["telcli"].ToString(), "[^1-9]", "", RegexOptions.None);
                    }
                    else
                        txt_telef_Cliente.Text = Item["telcli"].ToString();
                    txt_Direc_Cliente.Text = Item["dircli"].ToString();
                    txtEmailFactura.Text = Item["email"].ToString();
                }
                if (txt_Ruc.Text.Length == 10)
                {
                    chk_Cedula.Checked = true;
                    chk_Ruc.Checked = false;
                    chk_pasaporte.Checked = false;
                    chbConsumidorFinal.Checked = false;
                }
                else if (txt_Ruc.Text.Length == 13)
                {
                    chk_Cedula.Checked = false;
                    chk_Ruc.Checked = true;
                    chk_pasaporte.Checked = false;
                    chbConsumidorFinal.Checked = false;
                }
                else
                {
                    chk_Cedula.Checked = false;
                    chk_Ruc.Checked = false;
                    chk_pasaporte.Checked = true;
                    chbConsumidorFinal.Checked = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnPrefactura_Click(object sender, EventArgs e)
        {
            DirectorioEmpresa();
            if (ConexionAcces("Select * from Numero_Control where codcon=73") == true) // Verifica el secuencial y autorizacion de la factura / 15/11/2012/ Giovanny Tapia
            {
                habiltarBotones(false, true, false, true, false, true, true, false);
                ultraGroupBox1.Enabled = true;   // Habilita los controles para el ingreso de datos / 07/11/2012 / Giovanny Tapia
                ultraTabControl1.Enabled = true;
                ultraTabControl2.Enabled = true;
                accionPaciente = false;
                accionFactura = true;
                facturaPrefactura = 2;
                label49.Visible = true;
                label50.Visible = true;
                ultraTabPageControl6.Enabled = false;
                txtObserva.Text = "Prefactura realizada por: " + Sesion.nomUsuario;
                txtObserva.Enabled = false;
                btnBuscarDatos.Enabled = true;
            }
            else
            {
                MessageBox.Show("Este computador no tiene los datos necesarios para Pre-Facturar. Consulte con el administrador del sistema.", "His3000");
            }
            //NumeroPrefactura = ConexionAccesPrefactura("Select numcon from Numero_Control where codcon=73");
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (CuentaAgrupada())
            {
                label50.Text = "AGRUPANDO CUENTAS";
                label50.BackColor = Color.Red;
                label50.Visible = true;
                AgrupacionCuentasPacientes frm = new AgrupacionCuentasPacientes(ultimaAtencion.ATE_CODIGO, Sesion.codUsuario);
                //frm.AteCodigoAgrupado = VectorCodigos;
                //VectorCodigoOK = VectorCodigos.Text;
                frm.ShowDialog();
                cargarDetalleFactura();
            }
            else
            {
                MessageBox.Show("Esta cuenta no puede servir de agrupador debe crear una atencion nueva, para agrupar cuentas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public bool CuentaAgrupada()
        {
            List<CUENTAS_PACIENTES> cp = new List<CUENTAS_PACIENTES>();
            cp = NegFactura.RecuepraCuenta(ultimaAtencion.ATE_CODIGO);
            if (cp.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void DesHabilitarBontones2()
        {
            btnSolicitar.Enabled = false;
            btnAnticipos.Enabled = false;
            btnAgrupar.Enabled = false;
            button1.Enabled = false;
            btnGeneraValores.Enabled = false;
            btnAltaPaciente.Enabled = false;
        }
        private void HabilitarBontones2()
        {
            //btnSolicitar.Enabled = true;
            //btnAnticipos.Enabled = true;
            //btnAgrupar.Enabled = true;
            //button1.Enabled = true;
            //btnGeneraValores.Enabled = true;
            //btnAltaPaciente.Enabled = true;
        }
        public bool VerificaAltaPaciente()
        {
            bool ok = false;

            ok = NegFactura.VerificaAltaPaciente(Convert.ToInt64(txt_Atencion.Text));

            return ok;

        }
        private void btnAltaPaciente_Click(object sender, EventArgs e)
        {
            //CAMBIO EL ESTADO DE LA HABITACION A ALTA PROGRAMADA Y ESC_CODIGO=2 POR MEDIO DEL USUARIO

            if (ultimaAtencion.ESC_CODIGO == 1)
            {
                if (MessageBox.Show("¿DESEA CERRAR (Bloquear) LA CUENTA?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    var val = ultimaAtencion.TIPO_INGRESOReference.EntityKey.EntityKeyValues[0].Value;
                    if (Convert.ToInt16(val) != 4 && Convert.ToInt16(val) != 10)
                    {
                        if (NegFactura.AltaProgramada(ultimaAtencion.ATE_CODIGO) == 1)
                        {
                            NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(ultimaAtencion.ATE_CODIGO, 2, Sesion.codUsuario, "FACTURACION-BLOQUEAR");
                            //MessageBox.Show("La cuenta del paciente se esta arreglando.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //NegFactura.actualizaEscCodigo(Convert.ToInt16(txt_Atencion.Text));
                            btnAltaPaciente.Enabled = false;
                            btnAuditaCuenta.Enabled = true;
                            ultimaAtencion.ESC_CODIGO = 2;
                            cargarDetalleFactura();
                        }
                        else
                            MessageBox.Show("La cuenta del cliente aun no se bloquea.\n no se puede facturar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        PARAMETROS_DETALLE obj = NegParametros.RecuperaPorCodigo(51);
                        if ((bool)obj.PAD_ACTIVO)
                        {
                            if (!NegConsultaExterna.PacienteCerradaCxE(ultimaAtencion.ATE_CODIGO))
                            {
                                MessageBox.Show("La atención es de CONSULTA EXTERNA y la misma no esta cerrada por el MÉDICO.\nNo se puede facturar!!!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                if (NegFactura.AltaProgramada(ultimaAtencion.ATE_CODIGO) == 1)
                                {
                                    NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(ultimaAtencion.ATE_CODIGO, 2, Sesion.codUsuario, "FACTURACION-BLOQUEAR");
                                    //MessageBox.Show("La cuenta del paciente se esta arreglando.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //NegFactura.actualizaEscCodigo(Convert.ToInt16(txt_Atencion.Text));
                                    btnAltaPaciente.Enabled = false;
                                    btnAuditaCuenta.Enabled = true;
                                    ultimaAtencion.ESC_CODIGO = 2;
                                    cargarDetalleFactura();
                                }
                                else
                                    MessageBox.Show("La cuenta del cliente aun no se bloquea.\n no se puede facturar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            if (NegFactura.AltaProgramada(ultimaAtencion.ATE_CODIGO) == 1)
                            {
                                NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(ultimaAtencion.ATE_CODIGO, 2, Sesion.codUsuario, "FACTURACION-BLOQUEAR");
                                //MessageBox.Show("La cuenta del paciente se esta arreglando.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //NegFactura.actualizaEscCodigo(Convert.ToInt16(txt_Atencion.Text));
                                btnAltaPaciente.Enabled = false;
                                btnAuditaCuenta.Enabled = true;
                                ultimaAtencion.ESC_CODIGO = 2;
                                cargarDetalleFactura();
                            }
                            else
                                MessageBox.Show("La cuenta del cliente aun no se bloquea.\n no se puede facturar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pacienta Ya Fue Dado De Alta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAltaPaciente.Enabled = false;
            }
            ultimaAtencion = NegAtenciones.AtencionID(Convert.ToInt32(txt_Atencion.Text.Trim()));
        }

        private void RevertirAtencion()
        {
            NegFactura Fac = new NegFactura();
            try
            {
                HABITACIONES objHabitacion = new HABITACIONES();
                short codigo_hab = Fac.RecuperarHabitacion(ultimaAtencion.ATE_CODIGO);
                short estado = Fac.RecuperarEstado(codigo_hab);
                objHabitacion.hab_Codigo = codigo_hab;
                if (ultimaAtencion.ESC_CODIGO == 2)
                {
                    if (codigo_hab != 0)
                    {
                        if (estado == 2 || estado == 5)
                        {
                            if (ultimaAtencion.ATE_FECHA_ALTA != null)
                            {
                                TimeSpan diasTrascurridos = DateTime.Now - (DateTime)ultimaAtencion.ATE_FECHA_ALTA;
                                Int64 dia = NegParametros.RecuperaValorParSvXcodigo(65);
                                if (diasTrascurridos.Days > dia)
                                {
                                    MessageBox.Show("No puede desbloquear la cuenta se ha superado \r\nel limite de dias para desbloquear la cuenta", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            DataTable Revertir = NegHabitaciones.RevertirMovimientoHabitacion(objHabitacion);
                            if (Revertir != null)
                            {
                                DataTable InformacionPaciente = new DataTable();
                                InformacionPaciente = NegHabitaciones.InformacionPaciente(objHabitacion);
                                //DataTable codAtencion = NegPacientes.registropaciente(InformacionPaciente.Rows[0][0].ToString().Trim(), 2);

                                //string codAtencioP = codAtencion.Rows[0]["ATE_CODIGO"].ToString();
                                HABITACIONES_HISTORIAL habitacionHistorial = new HABITACIONES_HISTORIAL();
                                habitacionHistorial.HAH_CODIGO = NegHabitacionesHistorial.RecuperaMaximoHabitacionHistorial();
                                habitacionHistorial.ATE_CODIGO = Convert.ToInt32(ultimaAtencion.ATE_CODIGO);
                                habitacionHistorial.HAB_CODIGO = (objHabitacion.hab_Codigo);
                                habitacionHistorial.ID_USUARIO = Sesion.codUsuario;
                                habitacionHistorial.HAH_FECHA_INGRESO = DateTime.Now;
                                habitacionHistorial.HAD_OBSERVACION = "Se revierte  el  estado ";
                                habitacionHistorial.HAH_ESTADO = 1;
                                NegHabitacionesHistorial.CrearHabitacionHistorial(habitacionHistorial);
                                if (Revertir.Rows.Count > 0)
                                {
                                    NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(ultimaAtencion.ATE_CODIGO, 1, Sesion.codUsuario, "FACTURACION-DESBLOQUEAR");
                                    MessageBox.Show("Reversión De Habitación Exitosa", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    NegFactura.EliminaMEDICOS_ALTA(Convert.ToInt64(txt_Atencion.Text));
                                    btnAuditaCuenta.Enabled = false;
                                    btnAltaPaciente.Enabled = true;
                                    incioFactura();
                                }
                                else
                                {
                                    MessageBox.Show("Paciente Ya Tiene Factura No Se Puede Revertir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Cuenta Habilitada para el área de enfermeria", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            MessageBox.Show("La Habitacion ya ha sido ocupada, No se puede revertir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {

                        NegFactura.RevertirEscCodigo(Convert.ToInt64(txt_Atencion.Text));
                        DataTable Revertir = NegHabitaciones.RevertirMovimientoHabitacion(objHabitacion);
                        if (Revertir != null)
                        {
                            DataTable InformacionPaciente = new DataTable();
                            InformacionPaciente = NegHabitaciones.InformacionPaciente(objHabitacion);
                            //DataTable codAtencion = NegPacientes.registropaciente(InformacionPaciente.Rows[0][0].ToString().Trim(), 2);

                            //string codAtencioP = codAtencion.Rows[0]["ATE_CODIGO"].ToString();
                            HABITACIONES_HISTORIAL habitacionHistorial = new HABITACIONES_HISTORIAL();
                            habitacionHistorial.HAH_CODIGO = NegHabitacionesHistorial.RecuperaMaximoHabitacionHistorial();
                            habitacionHistorial.ATE_CODIGO = Convert.ToInt32(ultimaAtencion.ATE_CODIGO);
                            habitacionHistorial.HAB_CODIGO = (objHabitacion.hab_Codigo);
                            habitacionHistorial.ID_USUARIO = Sesion.codUsuario;
                            habitacionHistorial.HAH_FECHA_INGRESO = DateTime.Now;
                            habitacionHistorial.HAD_OBSERVACION = "Se revierte  el  estado ";
                            habitacionHistorial.HAH_ESTADO = 1;
                            NegHabitacionesHistorial.CrearHabitacionHistorial(habitacionHistorial);
                            if (Revertir.Rows.Count > 0)
                            {
                                NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(ultimaAtencion.ATE_CODIGO, 1, Sesion.codUsuario, "FACTURACION-DESBLOQUEAR");
                                MessageBox.Show("Reversión De Habitación Exitosa", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnAuditaCuenta.Enabled = false;
                                btnAltaPaciente.Enabled = true;

                                incioFactura();
                            }
                            else
                            {
                                MessageBox.Show("Paciente Ya Tiene Factura No Se Puede Revertir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }

                    }
                }
                else
                {
                    if (ultimaAtencion.ESC_CODIGO == 1)
                        MessageBox.Show("Cuenta activa en piso", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnAuditaCuenta.Enabled = false;

                    //else 
                    //    MessageBox.Show("Paciente Ya Tiene Factura No Se Puede Revertir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                ultimaAtencion = NegAtenciones.AtencionID(Convert.ToInt32(txt_Atencion.Text.Trim()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void txtObserva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
            }

        }

        private void facturaimprime_Click(object sender, EventArgs e)
        {
            ReporteFactura reporteFactura = new ReporteFactura();

            if (CuentaAgrupada())
            {
                frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + '-' + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "AgrupacionCuentasDetalle", "", 0, "", "", 0, 0, txtObserva.Text, txt_IVA.Text, txt_ConIVA.Text, lblConvenio.Text, txt_SinIVA.Text, txt_SubTotal.Text, txt_Descuento.Text, txt_Total.Text);
                DesgloseFactura.Show();
                DesgloseFactura = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + '-' + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "FACTURA_IMPRESION", "", 0, "", "", 0, 0, txtObserva.Text, txt_IVA.Text, txt_ConIVA.Text, lblConvenio.Text, txt_SinIVA.Text, txt_SubTotal.Text, txt_Descuento.Text, txt_Total.Text);
                DesgloseFactura.Show();
            }
            else
            {
                // IMPRESION DE EL DETALLE DE LA FACTURA    
                if (facturaPrefactura == 2)
                {
                    frmReporteDesgloseFactura FACTURA = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, txt_Direc_Cliente.Text, Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "PREFACTURA", "", 0, "", "", 0, 0, txtObserva.Text, txt_IVA.Text, txt_ConIVA.Text, lblConvenio.Text, txt_SinIVA.Text, txt_SubTotal.Text, txt_Descuento.Text, txt_Total.Text);
                    FACTURA.pEmision = txt_Autorizacion.Text.Trim();
                    FACTURA.Show();
                }
                else
                {
                    frmReporteDesgloseFactura FACTURA = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, txt_Direc_Cliente.Text, Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "FACTURA_IMPRESION", "", 0, "", "", 0, 0, txtObserva.Text, txt_IVA.Text, txt_ConIVA.Text, lblConvenio.Text, txt_SinIVA.Text, txt_SubTotal.Text, txt_Descuento.Text, txt_Total.Text);
                    FACTURA.pEmision = txt_Autorizacion.Text.Trim();
                    FACTURA.Show();
                }
                if ((MessageBox.Show("Desea imprimir el desglose de articulos de la factura?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    //frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + '-' + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "CertificadoHA", "", 0, "", "", 0, 0, "", "", "", "", "", "", "", "");
                    //DesgloseFactura.Show();
                    ImprimeItems();
                }
            }
        }

        private void ultraTabControl2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void txtEmailFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (ComprobarFormatoEmail(txtEmailFactura.Text) == true)
                {
                    e.Handled = true;
                    txtEmailFactura.Focus();
                }
                else
                {
                    MessageBox.Show("DIRECCIÓN DE CORREO ELECTRONICO NO ES VALIDA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmailFactura.Text = string.Empty;
                }
            }
        }
        public static bool ComprobarFormatoEmail(string seMailAComprobar)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(seMailAComprobar, sFormato))
            {
                if (Regex.Replace(seMailAComprobar, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + '-' + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "FACTURA", "", 0, "", "", 0, 0, "", "", "", lblConvenio.Text, "", "", "", "");
            DesgloseFactura.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ImprimeItems();
        }

        public void ImprimeItems()
        {
            NegCertificadoMedico c = new NegCertificadoMedico();
            PACIENTES paciente = new PACIENTES();
            paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value));
            ATENCIONES ultima = new ATENCIONES();
            ultima = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value)));

            DSDetalleItem DSDetalle = new DSDetalleItem();
            DataRow dr;

            DataTable Detalle = new DataTable();
            Detalle = NegFactura.DetalleItem(Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value));
            double valorCxE = 0;
            foreach (DataRow item in Detalle.Rows)
            {
                if (Convert.ToInt32(ultimaAtencion.TIPO_INGRESOReference.EntityKey.EntityKeyValues[0].Value.ToString()) == 4)
                {
                    if (item[4].ToString() == "CONSULTA EXTERNA")
                    {
                        valorCxE += Convert.ToDouble(item[3].ToString());
                    }
                }
            }
            int cantidad = 0;
            foreach (DataRow item in Detalle.Rows)
            {
                dr = DSDetalle.Tables["DetalleItem"].NewRow();
                if (Convert.ToInt32(ultimaAtencion.TIPO_INGRESOReference.EntityKey.EntityKeyValues[0].Value.ToString()) == 4)
                {

                    if (item[4].ToString() == "CONSULTA EXTERNA")
                    {
                        cantidad++;
                        if (cantidad == 1)
                        {
                            dr["Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                            dr["Hc"] = paciente.PAC_HISTORIA_CLINICA;
                            dr["Atencion"] = ultima.ATE_NUMERO_ATENCION;
                            dr["FIngreso"] = ultima.ATE_FECHA_INGRESO;
                            dr["FAlta"] = ultima.ATE_FECHA_ALTA;
                            dr["Cliente"] = txt_Cliente.Text;
                            dr["Ruc"] = txt_Ruc.Text;
                            dr["Telefono"] = txt_telef_Cliente.Text;
                            dr["Cantidad"] = Convert.ToDouble(item[0].ToString());
                            if (cantidad == 1)
                                dr["Descripcion"] = "Consulta Externa";
                            dr["Rubro"] = item[4].ToString();
                            dr["Iva"] = Convert.ToDouble(item[2].ToString());
                            dr["Total"] = valorCxE;
                            dr["Subtotal"] = txt_SubTotal.Text;
                            dr["IVATotal"] = txt_IVA.Text;
                            dr["TotalTotal"] = txt_Total.Text;
                            dr["Logo"] = c.path();
                            dr["Descuento"] = Convert.ToDouble(item[5].ToString());
                            dr["totalDescuento"] = Convert.ToDouble(txt_Descuento.Text);
                        }
                    }
                    else
                    {
                        cantidad = 0;
                        dr["Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                        dr["Hc"] = paciente.PAC_HISTORIA_CLINICA;
                        dr["Atencion"] = ultima.ATE_NUMERO_ATENCION;
                        dr["FIngreso"] = ultima.ATE_FECHA_INGRESO;
                        dr["FAlta"] = ultima.ATE_FECHA_ALTA;
                        dr["Cliente"] = txt_Cliente.Text;
                        dr["Ruc"] = txt_Ruc.Text;
                        dr["Telefono"] = txt_telef_Cliente.Text;
                        dr["Cantidad"] = Convert.ToDouble(item[0].ToString());
                        dr["Descripcion"] = item[1].ToString();
                        dr["Rubro"] = item[4].ToString();
                        dr["Iva"] = Convert.ToDouble(item[2].ToString());
                        dr["Total"] = Convert.ToDouble(item[3].ToString());
                        dr["Subtotal"] = txt_SubTotal.Text;
                        dr["IVATotal"] = txt_IVA.Text;
                        dr["TotalTotal"] = txt_Total.Text;
                        dr["Logo"] = c.path();
                        dr["Descuento"] = Convert.ToDouble(item[5].ToString());
                        dr["totalDescuento"] = Convert.ToDouble(txt_Descuento.Text);
                    }
                }
                else
                {
                    dr["Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                    dr["Hc"] = paciente.PAC_HISTORIA_CLINICA;
                    dr["Atencion"] = ultima.ATE_NUMERO_ATENCION;
                    dr["FIngreso"] = ultima.ATE_FECHA_INGRESO;
                    dr["FAlta"] = ultima.ATE_FECHA_ALTA;
                    dr["Cliente"] = txt_Cliente.Text;
                    dr["Ruc"] = txt_Ruc.Text;
                    dr["Telefono"] = txt_telef_Cliente.Text;
                    dr["Cantidad"] = Convert.ToDouble(item[0].ToString());
                    dr["Descripcion"] = item[1].ToString();
                    dr["Rubro"] = item[4].ToString();
                    dr["Iva"] = Convert.ToDouble(item[2].ToString());
                    dr["Total"] = Convert.ToDouble(item[3].ToString());
                    dr["Subtotal"] = txt_SubTotal.Text;
                    dr["IVATotal"] = txt_IVA.Text;
                    dr["TotalTotal"] = txt_Total.Text;
                    dr["Logo"] = c.path();
                    dr["descuento"] = Convert.ToDouble(item[5].ToString());
                    dr["totalDescuento"] = Convert.ToDouble(txt_Descuento.Text);
                }
                if (cantidad <= 1)
                    DSDetalle.Tables["DetalleItem"].Rows.Add(dr);
            }
            His.Formulario.frmReportes x = new His.Formulario.frmReportes(1, "DetalleItem", DSDetalle);
            x.Show();
        }

        private void estadoDeCuentaXFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadoFechas frm = new EstadoFechas(txt_ApellidoH1.Text, Convert.ToInt32(ultimaAtencion.ATE_CODIGO), txt_Historia_Pc.Text, CodigoAseguradora);
            frm.ShowDialog();
        }

        private void detallePorAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimeArea();
        }
        public void ImprimeArea()
        {
            PACIENTES paciente = new PACIENTES();
            paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value));
            DSDetalleArea DSDetalle = new DSDetalleArea();
            DataRow dr;
            DataTable Detalle = new DataTable();
            Detalle = NegFactura.DetalleArea(Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value));
            foreach (DataRow item in Detalle.Rows)
            {
                dr = DSDetalle.Tables["Factura"].NewRow();
                dr["Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                dr["Hc"] = paciente.PAC_HISTORIA_CLINICA;
                dr["Atencion"] = ultimaAtencion.ATE_NUMERO_ATENCION;
                dr["FIngreso"] = ultimaAtencion.ATE_FECHA_INGRESO;
                dr["FAlta"] = ultimaAtencion.ATE_FECHA_ALTA;
                dr["Cliente"] = txt_Cliente.Text;
                dr["Ruc"] = txt_Ruc.Text;
                dr["Telefono"] = txt_telef_Cliente.Text;
                dr["Cantidad"] = Convert.ToDouble(item[0].ToString());
                dr["Descripcion"] = item[1].ToString();
                dr["Rubro"] = item[4].ToString();
                dr["Iva"] = Convert.ToDouble(item[2].ToString());
                dr["Total"] = Convert.ToDouble(item[3].ToString());
                dr["Subtotal"] = txt_SubTotal.Text;
                dr["IVATotal"] = txt_IVA.Text;
                dr["TotalTotal"] = txt_Total.Text;
                dr["Logo"] = NegUtilitarios.RutaLogo("General");
                DSDetalle.Tables["Factura"].Rows.Add(dr);
            }
            His.Formulario.frmReportes x = new His.Formulario.frmReportes(1, "DetalleArea", DSDetalle);
            x.Show();

        }

        int aux = 1;
        private void dtpFechaFacturacion_ValueChanged(object sender, EventArgs e)
        {
            if (aux == 1)
            {
                if (dtpFechaFacturacion.Value.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("No puede elegir una fecha mayor a la actual. " + DateTime.Now.Date, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    dtpFechaFacturacion.Value = DateTime.Now;
                    return;
                    aux = 0;
                }
                //else
                //{
                //    DateTime Fecha_Inicio = DateTime.Now;
                //    DateTime Fecha_Final = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
                //    aux = 0;
                //}
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (ultimaAtencion.ESC_CODIGO == 6)
            {
                if (NegFactura.ExportaCuentaPaciente(ultimaAtencion.ATE_CODIGO, ultimaAtencion.PACIENTES.PAC_CODIGO))
                {
                    MessageBox.Show("Exportacion realizada con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Exportacion no realizada, verifique datos en la otra empresa", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("La cuenta debe estar facturada para que pueda exportar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void detalleCuentasAgrupadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable df = NegFactura.FacturaDetalleAgrupado(FacturaDato_);
            if (df.Rows.Count > 0)
            {
                frmReporteDesgloseFactura DesgloseFactura = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + '-' + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "AgrupacionCuentasDetalle", "", 0, "", "", 0, 0, "", df.Rows[0][15].ToString(), df.Rows[0][14].ToString(), lblConvenio.Text, df.Rows[0][13].ToString(), "", df.Rows[0][12].ToString(), "");
                DesgloseFactura.Show();
                DesgloseFactura = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + '-' + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, "", Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "FACTURA_IMPRESION", "", 0, "", "", 0, 0, "", df.Rows[0][15].ToString(), df.Rows[0][14].ToString(), lblConvenio.Text, df.Rows[0][13].ToString(), "", df.Rows[0][12].ToString(), "");
                DesgloseFactura.Show();

            }
            else
            {
                MessageBox.Show("Primero debe facturar la cuenta agrupada para sacar el detalle", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void gridRecuperaFP_KeyPress(object sender, KeyPressEventArgs e)
        {
            int columna = gridFormasPago.SelectedCells[0].ColumnIndex;
            int fila = gridFormasPago.CurrentRow.Index;
            verificavalor = Convert.ToString(gridFormasPago.Rows[fila].Cells[5].Value);
            if (columna == 2)
            {
                Convert.ToString(gridFormasPago.Rows[fila].Cells[2].Value);

            }
        }

        private void gridRecuperaFP_KeyDown(object sender, KeyEventArgs e)
        {
            int columna = gridFormasPago.SelectedCells[0].ColumnIndex;
            int fila = gridFormasPago.CurrentRow.Index;
            verificavalor = Convert.ToString(gridFormasPago.Rows[fila].Cells[5].Value);
            if (columna == 2)
            {
                Convert.ToString(gridFormasPago.Rows[fila].Cells[2].Value);

            }
        }

        private void gridFormasPago_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int columna = gridFormasPago.SelectedCells[0].ColumnIndex;
            int fila = gridFormasPago.CurrentRow.Index;
            verificavalor = Convert.ToString(gridFormasPago.Rows[fila].Cells[5].Value);
            if (columna == 2)
            {
                double numericValue = 0;
                bool isNumber = double.TryParse(Convert.ToString(gridFormasPago.Rows[fila].Cells[2].Value), out numericValue);
                if (!isNumber)
                {
                    gridFormasPago.Rows[fila].Cells[2].Value = "";
                    MessageBox.Show("Ingrese solo números", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (numericValue < 0)
                    {
                        gridFormasPago.Rows[fila].Cells[2].Value = "";
                        MessageBox.Show("Ingrese solo cantidades positivas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        private void txt_Ruc_Enter(object sender, EventArgs e)
        {

            //}
            //    else
            //    {
            //        MessageBox.Show("LA IDENTIFICACIÓN INGRESADA ES INCORRECTA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        chk_Ruc.Checked = false;
            //        chk_Cedula.Checked = false;
            //        chk_pasaporte.Checked = false;
            //        chbConsumidorFinal.Checked = false;
            //    }
        }

        private void enviaHistoriaClinicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PARAMETROS_DETALLE obj = NegParametros.RecuperaPorCodigo(52);
            if ((bool)obj.PAD_ACTIVO)
            {
                DataTable x = NegDietetica.getDataTable("PathLocalHC");
                string numeroAtencion = ultimaAtencion.ATE_CODIGO.ToString();
                PACIENTES pac = new PACIENTES();
                pac = NegPacientes.recuperarPacientePorAtencion(ultimaAtencion.ATE_CODIGO);
                string numeroHistoria = pac.PAC_HISTORIA_CLINICA + "//" + numeroAtencion + "//";
                if (x.Rows.Count > 0)
                {
                    string strPathGeneral = x.Rows[0][0].ToString() + "//" + pac.PAC_HISTORIA_CLINICA + "//" + numeroAtencion;
                    bool existe = Directory.Exists(strPathGeneral);

                    if (existe)
                    {
                        frmReporteDesgloseFactura FACTURA = new frmReporteDesgloseFactura(txt_Factura_Cod1.Text + txt_Factura_Cod3.Text, txt_Cliente.Text, txt_Ruc.Text, txt_telef_Cliente.Text, txt_Direc_Cliente.Text, Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value), "FACTURA_IMPRESION", "", 0, "", "", 0, 0, txtObserva.Text, txt_IVA.Text, txt_ConIVA.Text, lblConvenio.Text, txt_SinIVA.Text, txt_SubTotal.Text, txt_Descuento.Text, txt_Total.Text, strPathGeneral, "factura");
                        FACTURA.pEmision = txt_Autorizacion.Text.Trim();
                        FACTURA.Show();
                        FACTURA.Close();
                    }
                    else
                    {
                        MessageBox.Show("No existe la historia clinica de esta atencion generada, pida que le creen y vuelva a intentar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    string rutaCarpeta = strPathGeneral;
                    string remitente = obj.PAD_NOMBRE;
                    string destinatario = txtEmailFactura.Text;
                    string asunto = "Paciente: " + txt_ApellidoH1.Text;
                    string cuerpoMensaje = "Adjunto se encuentra la Historia Clinica: " + pac.PAC_HISTORIA_CLINICA + ". De la Atención: " + numeroAtencion + ".";


                    // Crear instancia de MailMessage
                    MailMessage mensaje = new MailMessage(remitente, destinatario, asunto, cuerpoMensaje);

                    // Obtener lista de archivos en la carpeta
                    string[] archivos = Directory.GetFiles(rutaCarpeta);

                    // Adjuntar cada archivo al correo electrónico
                    foreach (string archivo in archivos)
                    {
                        mensaje.Attachments.Add(new Attachment(archivo));
                    }

                    // Configurar el cliente de correo electrónico
                    SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 25);
                    clienteSmtp.EnableSsl = true;
                    clienteSmtp.Credentials = new NetworkCredential(obj.PAD_NOMBRE, obj.PAD_VALOR);

                    try
                    {
                        // Enviar el correo electrónico
                        clienteSmtp.Send(mensaje);
                        Console.WriteLine("Correo electrónico enviado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
                    }
                    finally
                    {
                        // Liberar recursos
                        mensaje.Dispose();
                    }
                }
            }

        }

        private void chbGuardaCopago_CheckedChanged(object sender, EventArgs e)
        {

        }
        public void cargaFactura(Int64 ate_codigo, Int64 pac_codigo)
        {
            incioFactura();
            ultraTabControl2.Tabs["descuento"].Visible = false;
            try
            {
                chbCopago.Checked = false;
                if (txt_Atencion.Text != "")
                {
                    txtNumeroAtencion.Text = NegFactura.RecuperaNumeroAtencion(ate_codigo);
                    txt_Atencion.Text = Convert.ToString(ate_codigo);
                    NegValidaciones.alzheimer();// LIBERA MEMORIA
                }
                else
                    return;
                if (txt_Historia_Pc.Text.Trim() != "")
                {
                    pacienteFactura = NegPacientes.RecuperarPacienteID(pac_codigo);
                    cargarDatosPacienteCopago(pacienteFactura, ate_codigo);
                    habiltarBotones(false, true, false, true, true, true, true, true);
                    if (ultimaAtencion.ESC_CODIGO == 6 || ultimaAtencion.ESC_CODIGO == 7 || ultimaAtencion.ESC_CODIGO == 3)
                    {
                        facturaimprime.Enabled = true;
                        enviaHistoriaClinicaToolStripMenuItem.Enabled = true;
                        facturaElectronicaToolStripMenuItem.Enabled = true;
                        txtObserva.Text = ultimaAtencion.ATE_FACTURA_PACIENTE;
                        txtObserva.Enabled = false;
                        dtpFechaFacturacion.Enabled = false;
                    }
                    else
                    {
                        txtObserva.Enabled = true;
                        NegFactura.ArreglaIVABase(Convert.ToString(ultimaAtencion.ATE_CODIGO));
                    }
                    cargarDetalleFactura();
                    if (Convert.ToInt16(ultimaAtencion.ESC_CODIGO) == 6 || Convert.ToInt16(ultimaAtencion.ESC_CODIGO) == 7 || Convert.ToInt16(ultimaAtencion.ESC_CODIGO) == 3)
                    {
                        habiltarBotones(false, false, false, true, true, true, true, false);
                        btnSolicitar.Enabled = false;
                        btnAnticipos.Enabled = false;
                        btnGeneraValores.Enabled = false;
                        btnAltaPaciente.Enabled = false;
                        btnAgrupar.Enabled = false;
                        button1.Enabled = false;
                        cbx_FacturaNombre.Enabled = false;
                        txt_Ruc.Enabled = false;
                        txt_Cliente.Enabled = false;
                    }
                    else
                    {
                        habiltarBotones(false, true, false, true, true, true, true, true);
                        btnSolicitar.Enabled = true;
                        btnAnticipos.Enabled = true;
                        if (!mushuñan)
                            btnGeneraValores.Enabled = true;
                        btnAltaPaciente.Enabled = true;
                        btnAgrupar.Enabled = true;
                        button1.Enabled = true;
                        cbx_FacturaNombre.Enabled = true;
                        txt_Ruc.Enabled = true;
                        txt_Cliente.Enabled = true;
                    }
                    Int32 atencion = Convert.ToInt32(this.txt_Atencion.Text.Trim());
                    if (atencion > 0)
                    {
                        DataTable auxDT = NegFactura.fechasINOUT(atencion);
                        foreach (DataRow row in auxDT.Rows)
                        {
                            cboTipoDescuento.Items.Add(row[0].ToString());
                            this.dtp_FechaIngreso.Text = row[0].ToString();
                            if (String.IsNullOrEmpty(row[1].ToString()))
                            {
                                this.txt_fechaalta.Text = "";
                            }
                            else
                            {
                                this.txt_fechaalta.Text = row[1].ToString();
                            }
                        }
                    }
                    CargarFormasPago();
                }

                //CAMBIOS EDGAR RAMOS PARA BLOQUEAR A USUARIO DE ATENCION AL CLIENTE
                string departamento;
                departamento = Convert.ToString(His.Entidades.Clases.Sesion.codDepartamento);
                if (departamento == "15")
                {
                    btnGuardar.Enabled = false;
                    btnImprimir.Enabled = false;
                    btnGeneraValores.Enabled = false;
                    btnSolicitar.Enabled = false;
                    btnAgrupar.Enabled = false;
                    button1.Enabled = false;
                    btnAnticipos.Enabled = false;
                    btnAltaPaciente.Enabled = false;
                }
                CargarTiposDesuento();
                if (CuentaAgrupada())
                {
                    detalleCuentasAgrupadasToolStripMenuItem.Visible = true;
                    toolStripMenuItem2.Visible = false;
                    toolStripMenuItem3.Visible = false;
                    detallePorAreaToolStripMenuItem.Visible = false;
                }
                else
                {
                    detalleCuentasAgrupadasToolStripMenuItem.Visible = false;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                //MessageBox.Show("Algo ocurrio al cargar datos de paciente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void detalleCopagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PACIENTES paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value));
            ATENCIONES ultima = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value)));
            COPAGO copago = NegCopago.recuperaCopago(ultimaAtencion.ATE_CODIGO);

            List<CUENTAS_PACIENTES> cuenta_original = NegCuentasPacientes.RecuperarCuenta(copago.ATE_CODIGO);
            List<CUENTAS_PACIENTES> cuenta_Copago = NegCuentasPacientes.RecuperarCuenta(copago.ATE_CODIGO_COPAGO);
            List<DtoReporteCopago> original = NegCopago.valoresCuentas(copago.ATE_CODIGO);
            List<DtoReporteCopago> copgo = NegCopago.valoresCuentas(copago.ATE_CODIGO_COPAGO);
            List<DtoReporteCopago> cuentaCopago = NegCopago.cuentaAuditoria(copago.ATE_CODIGO);

            DSDetalleItem DSDetalle = new DSDetalleItem();
            DataRow dr;

            DataTable Detalle = NegFactura.DetalleItem(Convert.ToInt32(ultimaAtencion.EntityKey.EntityKeyValues[0].Value));
            foreach (var item in cuentaCopago)
            {
                dr = DSDetalle.Tables["DetalleCopago"].NewRow();
                dr["Logo"] = NegUtilitarios.RutaLogo("General");
                dr["Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                dr["Hc"] = paciente.PAC_HISTORIA_CLINICA;
                dr["Atencion"] = ultima.ATE_NUMERO_ATENCION;
                dr["FIngreso"] = ultima.ATE_FECHA_INGRESO;
                dr["FAlta"] = ultima.ATE_FECHA_ALTA;
                dr["Ruc"] = txt_Ruc.Text;
                dr["Telefono"] = txt_telef_Cliente.Text;
                dr["Descripcion"] = item.DETALLE;
                dr["Cantidad"] = item.CANTIDAD;
                dr["unitario"] = item.VALOR_UNITARIO;
                dr["iva"] = item.IVA;
                dr["valor"] = item.PRO_CODIGO;
                dr["total"] = item.TOTAL;

                DtoReporteCopago roriginal = original.FirstOrDefault(o => o.PRO_CODIGO == item.PRO_CODIGO);
                if (roriginal != null)
                    dr["t_original"] = roriginal.IVA + roriginal.TOTAL;
                else
                    dr["t_original"] = 0;
                DtoReporteCopago rcopago = copgo.FirstOrDefault(y => y.PRO_CODIGO == item.PRO_CODIGO);
                if (rcopago != null)
                    dr["t_copago"] = rcopago.IVA + rcopago.TOTAL;
                else
                    dr["t_copago"] = 0;
                dr["rubro"] = roriginal.RUBRO;
                DSDetalle.Tables["DetalleCopago"].Rows.Add(dr);
            }
            His.Formulario.frmReportes x = new His.Formulario.frmReportes(1, "Copago", DSDetalle);
            x.Show();
        }

        private void cargarDatosPacienteCopago(PACIENTES pacienteActual, Int64 ate_codigo)
        {
            ultimaAtencion = NegAtenciones.AtencionID(ate_codigo);
            DataTable Atenciones = NegAtenciones.atencionesID(ate_codigo);
            DataTable DatosConvenioAtencion = new DataTable();
            limpiarCampos();
            string NombreFactura = ultimaAtencion.ATE_FACTURA_NOMBRE;
            if (NombreFactura != "PACIENTE")
            {
                txt_ApellidoH1.Text = pacienteActual.PAC_APELLIDO_PATERNO + ' ' + pacienteActual.PAC_APELLIDO_MATERNO + ' ' + pacienteActual.PAC_NOMBRE1 + ' ' + pacienteActual.PAC_NOMBRE2;
                FacturaNombre(NombreFactura);
            }
            else
            {
                if (pacienteActual.PAC_IDENTIFICACION != null)
                    txt_RucPaciente.Text = pacienteActual.PAC_IDENTIFICACION;
                txt_ApellidoH1.Text = pacienteActual.PAC_APELLIDO_PATERNO + ' ' + pacienteActual.PAC_APELLIDO_MATERNO + ' ' + pacienteActual.PAC_NOMBRE1 + ' ' + pacienteActual.PAC_NOMBRE2;
                txt_Telef_P.Text = pacienteActual.PAC_REFERENTE_TELEFONO;
            }
            txt_tipoIngreso.Text = Atenciones.Rows[0][0].ToString();
            cbx_FacturaNombre.SelectedText = NombreFactura;
            codigoAtencion = ultimaAtencion.ATE_CODIGO;
            HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == ultimaAtencion.HABITACIONESReference.EntityKey);
            if (hab != null)
            {
                Sesion.codHabitacion = hab.hab_Codigo;
                txt_Habitacion_P.Text = hab.hab_Numero;
            }
            int codMedicos = Convert.ToInt32(ultimaAtencion.MEDICOSReference.EntityKey.EntityKeyValues[0].Value);
            medico = NegMedicos.recuperarMedico(codMedicos);
            DatosConvenioAtencion = NegAtenciones.CodigoConvenio(ultimaAtencion.ATE_CODIGO);
            CodigoConvenio = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["TE_CODIGO"].ToString());
            CodigoTipoEmpresa = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["CAT_CODIGO"].ToString()); /*CONVENIO*/
            CodigoAseguradora = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["ASE_CODIGO"].ToString()); /* EMPRESA */
            this.lblCategoria.Text = DatosConvenioAtencion.Rows[0]["TE_DESCRIPCION"].ToString();
            this.lblConvenio.Text = DatosConvenioAtencion.Rows[0]["CAT_NOMBRE"].ToString();
            //CALCULO SI EL PACIENTE ESTA HOSPITALIZADO O NO PARA EL DIA DE HOSPITALIZACION PABLO ROCHA 30/05/2014
            DateTime diashospitalizadoalta;
            DateTime diashospitalizadoinicio;
            TimeSpan diastotales;
            int diaHospiAlta = (DateTime.Now.Day);
            int mesHospiAlta = (DateTime.Now.Month);
            int anioHospiAlta = (DateTime.Now.Year);
            int diaHospiIngreso = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_INGRESO).Day);
            int mesHospiIngreso = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_INGRESO).Month);
            int anioHospiIngreso = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_INGRESO).Year);
            if (ultimaAtencion.ESC_CODIGO == 1)
            {
                diashospitalizadoalta = Convert.ToDateTime(diaHospiAlta + "/" + mesHospiAlta + "/" + anioHospiAlta);
                diashospitalizadoinicio = Convert.ToDateTime(diaHospiIngreso + "/" + mesHospiIngreso + "/" + anioHospiIngreso);
                diastotales = diashospitalizadoalta.Subtract(diashospitalizadoinicio);
                txt_Dias_P.Text = Convert.ToString(diastotales.Days);
            }
            else
            {
                diaHospiAlta = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_ALTA).Day);
                mesHospiAlta = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_ALTA).Month);
                anioHospiAlta = (Convert.ToDateTime(ultimaAtencion.ATE_FECHA_ALTA).Year);
                diashospitalizadoalta = Convert.ToDateTime(diaHospiAlta + "/" + mesHospiAlta + "/" + anioHospiAlta);
                diashospitalizadoinicio = Convert.ToDateTime(diaHospiIngreso + "/" + mesHospiIngreso + "/" + anioHospiIngreso);
                diastotales = diashospitalizadoalta.Subtract(diashospitalizadoinicio);
                txt_Dias_P.Text = Convert.ToString(diastotales.Days);
            }
            DataTable PacienteReferido = NegDetalleCuenta.ReferidoPaciente(ultimaAtencion.ATE_CODIGO);
            if (txt_Dias_P.Text == "0")
                txt_Dias_P.Text = "1";
            if (PacienteReferido.Rows.Count > 0)
                txt_Referido.Text = PacienteReferido.Rows[0][0].ToString();
            datosPacienteActual = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);
            txt_Direccion_P.Text = datosPacienteActual.DAP_DIRECCION_DOMICILIO;
            cargarDatosFacturaA(pacienteActual);
            FacturaNombre(NombreFactura);
            cargarDatosPagos(ultimaAtencion);
        }
    }
}