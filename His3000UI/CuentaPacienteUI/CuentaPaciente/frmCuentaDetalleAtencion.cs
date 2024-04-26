using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Honorarios.Datos;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Formulario;
using His.Negocio;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using His.Admision;
using His.General;
using System.Data.OleDb;
using TarifariosUI;
using His.Entidades.Clases;

namespace CuentaPaciente
{
    public partial class frmCuentaDetalleAtencion : Form
    {
        #region Declaracion de Variables

        public Int32 CodigoEstadoCuenta;
        public PACIENTES paciente;
        public PACIENTES_DATOS_ADICIONALES datos;
        private ATENCIONES atencion = null;
        public ListView listaDatos = null;
        public int codigoAtencion;
        public List<PRODUCTO> listaProductosSolicitados;
        private byte codigoEstacion;
        public PRODUCTO producto;
        private string direccion;
        private List<DtoCuentaCargas> listaCuentaCargas = new List<DtoCuentaCargas>();
        private List<DtoDetalleCuentaPaciente> listaCuenta = new List<DtoDetalleCuentaPaciente>();
        private ATENCIONES_DETALLE_SEGUROS atdSeguros = new ATENCIONES_DETALLE_SEGUROS();
        private CUENTAS_PACIENTES cuentaPacientes = new CUENTAS_PACIENTES();
        private List<CUENTAS_PACIENTES> listaCuentaAtencion = new List<CUENTAS_PACIENTES>();
        private List<ANEXOS_IESS> listaAnexos;
        private List<DtoDetalleCuentaPaciente> listaPedido = new List<DtoDetalleCuentaPaciente>();
        private List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = null;
        private List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        private List<ESTADOS_CUENTA> listaEstadosCuenta;
        public ANEXOS_IESS anexoIess;
        private ATENCION_DETALLE_CATEGORIAS atencionDCategoria;
        private TARIFARIOS_DETALLE tarifarioHI;
        private TARIFARIOS_DETALLE tarifarioH;
        PEDIDOS_AREAS pedidosArea = new PEDIDOS_AREAS();
        CUENTAS_PACIENTES cuentaModificada = null;
        CUENTAS_PACIENTES cuentaHistorial = null;
        private List<PEDIDOS_AREAS> listaAreas = new List<PEDIDOS_AREAS>();
        decimal unidadesUvr;
        decimal precioUnitario;
        Boolean diagnostico = false;
        private MEDICOS medico;
        private string observacCuenta;
        private Boolean estado = false;
        Int32 codUsuario = 0;
        List<HC_CATALOGO_SUBNIVEL> listaTipoDiag = new List<HC_CATALOGO_SUBNIVEL>();
        public bool formatoFecha = false;
        #endregion

        #region Contructor

        public frmCuentaDetalleAtencion(Int32 cod_Usuario)
        {
            InitializeComponent();
            HabilitarBotones(false, false, false, false, true, false);
            CargarDatos();
            codUsuario = cod_Usuario;
        }

        #endregion

        private void LimpiarCampos()
        {
            txtPacienteNombre.Text = string.Empty;
            txtPacienteNombre2.Text = string.Empty;
            txtPacienteApellidoPaterno.Text = string.Empty;
            txtPacienteApellidoMaterno.Text = string.Empty;
            txtPacienteDireccion.Text = string.Empty;
            txtPacienteHCL.Text = string.Empty;
            txtPacienteTelf.Text = string.Empty;
            txtPacienteCedula.Text = string.Empty;
            datos = null;
            txt_nombreTitular.Text = string.Empty;
            txt_CedulaTitular.Text = string.Empty;
            txt_CiudadTitular.Text = string.Empty;
            txt_TelefonoTitular.Text = string.Empty;
            txt_DireccionTitular.Text = string.Empty;
            txt_nombreTitular.Text = string.Empty;
            txt_CedulaTitular.Text = string.Empty;
            cmb_Parentesco.SelectedIndex = 0;
            txt_CodigoDependencia.Text = string.Empty;
            txt_Dependencia.Text = string.Empty;
            txt_CSeguro.Text = string.Empty;
            txt_Seguro.Text = string.Empty;
            txt_CDerivacion.Text = string.Empty;
            txt_Derivacion.Text = string.Empty;
            txt_SDerivacion.Text = string.Empty;
            txt_CContingencia.Text = string.Empty;
            txt_Contigencia.Text = string.Empty;
            txt_CTexamen.Text = string.Empty;
            txt_TExamen.Text = string.Empty;
            cmb_Diagnostico.SelectedIndex = 0;
            dateTimePickerFechaIngreso.Value = DateTime.Now;
            dateTimePickerFechaAlta.Value = DateTime.Now;
            dateTimePickerFechaAlta.Enabled = false;
            //dtg_diagnosticos.DataSource = null;
            txtCantidad.Text = "1";
            rdb_SinIva.Checked = true;

        }

        //private void limpiarCamposProductos()
        //{
        //    cuentaModificada = null;
        //    txt_Codigo.Text = string.Empty;
        //    txtCodigoSic.Text = string.Empty;
        //    txt_Nombre.Text = string.Empty;
        //    txtValorNeto.Text = "0.00";
        //    txtCantidad.Text = "1";
        //    txtTotal.Text = "0.00";
        //    txtCControl.Text = string.Empty;
        //    //txtDirArchivo.Text = string.Empty;
        //}


        private void actualizarDatos()
        {

            LimpiarCampos();
            if (Microsoft.VisualBasic.Information.IsNumeric(txtAtencion.Text) == false)
                txtAtencion.Text = string.Empty;
            if (txtAtencion.Text != string.Empty)
            {
                CargarAtencion();
                //HabilitarBotones(false, false, true, false, true, true); Comento por que solo debe actualizar datos de la forma / Giovanny Tapia / 12062012
                HabilitarBotones(false, true, false, false, true, true);
            }
            btnActualizar.Enabled = true;
            if (Microsoft.VisualBasic.Information.IsNumeric(txtCodigoPaciente.Text) == false)
                txtCodigoPaciente.Text = string.Empty;
            if (txtCodigoPaciente.Text != string.Empty)
                CargarPaciente(Convert.ToInt32(txtCodigoPaciente.Text.ToString()));
            else
            {
                paciente = null;
                txtPacienteNombre.Text = string.Empty;
                txtPacienteNombre2.Text = string.Empty;
                txtPacienteApellidoPaterno.Text = string.Empty;
                txtPacienteApellidoMaterno.Text = string.Empty;
                txtPacienteDireccion.Text = string.Empty;
                txtPacienteHCL.Text = string.Empty;
                txtPacienteTelf.Text = string.Empty;
                txtPacienteCedula.Text = string.Empty;
            }
        }


        public void CargarPaciente(int codigo)
        {

            paciente = NegPacientes.RecuperarPacienteID(codigo);
            if (paciente != null)
            {
                txtPacienteNombre.Text = paciente.PAC_NOMBRE1;
                txtPacienteNombre2.Text = paciente.PAC_NOMBRE2;
                txtPacienteApellidoPaterno.Text = paciente.PAC_APELLIDO_PATERNO;
                txtPacienteApellidoMaterno.Text = paciente.PAC_APELLIDO_MATERNO;
                txtPacienteHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                txtPacienteCedula.Text = paciente.PAC_IDENTIFICACION;
                datos = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(paciente.PAC_CODIGO);
                txtPacienteDireccion.Text = datos.DAP_DIRECCION_DOMICILIO;
                txtPacienteTelf.Text = datos.DAP_TELEFONO1;
                //CargarDatos();
            }
            else
            {
                LimpiarCampos();
            }

        }

        private void CargarAtencion()
        {
            try
            {
                atencion = NegAtenciones.AtencionID(Convert.ToInt32(txtAtencion.Text.Trim()));
                if (atencion != null)
                {
                    dateTimePickerFechaIngreso.Value = atencion.ATE_FECHA_INGRESO.Value;
                    if (atencion.ATE_FECHA_ALTA != null)
                        dateTimePickerFechaAlta.Value = atencion.ATE_FECHA_ALTA.Value;
                    else
                    {
                        dateTimePickerFechaAlta.Value = DateTime.Now;
                        dateTimePickerFechaAlta.Enabled = false;
                    }

                    atdSeguros = NegAtencionDetalleSeguros.RecuAtencionesDetalleSeguros(atencion.ATE_CODIGO);
                    if (atdSeguros != null)
                    {
                        txt_nombreTitular.Text = atdSeguros.ADS_ASEGURADO_NOMBRE;
                        txt_CedulaTitular.Text = atdSeguros.ADS_ASEGURADO_CEDULA;
                        txt_CiudadTitular.Text = atdSeguros.ADS_ASEGURADO_CIUDAD;
                        txt_TelefonoTitular.Text = atdSeguros.ADS_ASEGURADO_TELEFONO;
                        txt_DireccionTitular.Text = atdSeguros.ADS_ASEGURADO_DIRECCION;
                        cmb_Parentesco.SelectedItem = listaAnexos.FirstOrDefault(a => a.ANI_CODIGO == Convert.ToInt32(atdSeguros.ADS_ASEGURADO_PARENTESCO));
                        txt_CContingencia.Text = Convert.ToString(atdSeguros.ADA_CONTINGENCIA);
                        txt_CTexamen.Text = atdSeguros.ADA_TIPO_EXAMEN;
                        //LimpiarDiagnosticos();
                        cargarDiagnosticos();
                        if (!diagnostico)
                        {
                            dtg_diagnosticos.Rows.Clear();
                        }
                        cmb_Diagnostico.SelectedItem =
                            listaTipoDiag.FirstOrDefault(
                                d => d.CA_CODIGO == Convert.ToInt32(atdSeguros.ADA_TIPO_DIAGNOSTICO));
                    }
                    else
                    {
                        txt_nombreTitular.Text = string.Empty;
                        txt_CedulaTitular.Text = string.Empty;
                        txt_CiudadTitular.Text = string.Empty;
                        txt_TelefonoTitular.Text = string.Empty;
                        txt_DireccionTitular.Text = string.Empty;
                        txt_nombreTitular.Text = string.Empty;
                        txt_CedulaTitular.Text = string.Empty;
                        cmb_Parentesco.SelectedIndex = 0;
                        txt_CodigoDependencia.Text = string.Empty;
                        txt_Dependencia.Text = string.Empty;
                        txt_CSeguro.Text = string.Empty;
                        txt_Seguro.Text = string.Empty;
                        txt_CDerivacion.Text = string.Empty;
                        txt_Derivacion.Text = string.Empty;
                        txt_SDerivacion.Text = string.Empty;
                        txt_CContingencia.Text = string.Empty;
                        txt_Contigencia.Text = string.Empty;
                        txt_CTexamen.Text = string.Empty;
                        txt_TExamen.Text = string.Empty;
                        cmb_Diagnostico.SelectedIndex = 0;
                        dtg_diagnosticos.DataSource = null;
                        //LimpiarDiagnosticos();
                        cargarDiagnosticosEpicrisis();
                        if (!diagnostico)
                        {
                            dtg_diagnosticos.Rows.Clear();
                        }
                    }
                    CargarConvenio();
                    HABITACIONES hab =
                        NegHabitaciones.listaHabitaciones().FirstOrDefault(
                            h => h.EntityKey == atencion.HABITACIONESReference.EntityKey);
                    if (hab != null)
                    {
                        //Sesion.codHabitacion = hab.hab_Codigo;
                        txtHabitacion.Text = hab.hab_Numero;
                    }
                    cargarCuenta(atencion.ATE_CODIGO);
                    cmb_EstadoCuenta.SelectedItem = listaEstadosCuenta.FirstOrDefault(e => e.ESC_CODIGO == atencion.ESC_CODIGO);
                    CodigoEstadoCuenta = Convert.ToInt32(atencion.ESC_CODIGO);
                }
                else
                {
                    dtg_diagnosticos.Rows.Clear();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void limpiarCampos()
        {
            cuentaModificada = null;
            txt_Codigo.Text = string.Empty;
            txtCodigoSic.Text = string.Empty;
            txt_Nombre.Text = string.Empty;
            txtValorNeto.Text = "0.00";
            txtCantidad.Text = "1";
            txtTotal.Text = "0.00";
            txtCControl.Text = string.Empty;
            //txtCodMedico.Text = string.Empty;
            //txtNombreMedico.Text = string.Empty;
            precioUnitario = 0;
            txtPorcentaje.Text = "0";
            rdb_SinIva.Checked = true;
            txtObserCuenta.Text = string.Empty;
            //txtDirArchivo.Text = string.Empty;
        }

        //private void LimpiarDiagnosticos()
        //{
        //    if(dtg_diagnosticos.Rows.Count >= 1)
        //    {
        //        dtg_diagnosticos.Rows.Clear();
        //    }else
        //    {
        //        if (dtg_diagnosticos.Rows.Count == 0)
        //        {
        //            dtg_diagnosticos.Rows.Clear();
        //        }
        //    }

        //}

        private void CargarConvenio()
        {
            //ATENCION_DETALLE_CATEGORIAS
            List<ASEGURADORAS_EMPRESAS> listaAseguradoras = new List<ASEGURADORAS_EMPRESAS>();
            detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(atencion.ATE_CODIGO);
            if (detalleCategorias != null)
            {
                foreach (ATENCION_DETALLE_CATEGORIAS detalle in detalleCategorias)
                {
                    CATEGORIAS_CONVENIOS cat =
                        NegCategorias.RecuperaCategoriaID(
                            Convert.ToInt16(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value));
                    aseguradoras = NegAseguradoras.ListaEmpresas();
                    ASEGURADORAS_EMPRESAS aseg = new ASEGURADORAS_EMPRESAS();
                    if (cat != null)
                    {
                        aseg = aseguradoras.FirstOrDefault(a => a.EntityKey == cat.ASEGURADORAS_EMPRESASReference.EntityKey);
                        if (aseg.ASE_CODIGO != null) listaAseguradoras.Add(aseg);
                    }
                    if (detalle != null)
                    {
                        atencionDCategoria = new ATENCION_DETALLE_CATEGORIAS();
                        atencionDCategoria = detalle;
                        txt_CodigoDependencia.Text = Convert.ToString(atencionDCategoria.HCC_CODIGO_DE);
                        txt_CSeguro.Text = Convert.ToString(atencionDCategoria.HCC_CODIGO_TS);
                        if (atencionDCategoria.ADA_AUTORIZACION != null)
                            camposDerivacion(atencionDCategoria.ADA_AUTORIZACION.ToString());
                    }
                }
            }
            if (listaAseguradoras.Count > 0)
            {
                cmb_Convenio.DataSource = listaAseguradoras;
                cmb_Convenio.DisplayMember = "ASE_NOMBRE";
                cmb_Convenio.ValueMember = "ASE_CODIGO";
                cmb_Convenio.SelectedIndex = 0;
            }
            else
            {
                listaAseguradoras = new List<ASEGURADORAS_EMPRESAS>();
                cmb_Convenio.DataSource = null;
                MessageBox.Show("No se ha definido Convenio o Aseguradora");
            }
        }

        private void camposDerivacion(string campo)
        {
            if (campo != null)
            {
                bool accion = false;
                string cadena = "";
                string cadena1 = "";
                for (int i = 0; i < campo.Length; i++)
                {
                    if (accion == false)
                    {
                        if (campo.Substring(i, 1) != "-")
                            cadena = cadena + campo.Substring(i, 1);
                        else
                        {
                            accion = true;
                            i++;
                        }
                    }
                    if (campo.Substring(i, 1) != "-")
                        cadena1 = cadena1 + campo.Substring(i, 1);
                    else
                        i = campo.Length;
                }

                txt_SDerivacion.Text = cadena1;
                List<ANEXOS_IESS> listaA = NegAnexos.RecuperarListaAnexos(Convert.ToString(AdmisionParametros.CodigoAnexoDerivacion));
                foreach (ANEXOS_IESS anexos in listaA)
                {
                    if (anexos.ANI_COD_PRO.ToString().Equals(cadena))
                    {
                        txt_CDerivacion.Text = anexos.ANI_COD_PRO;
                        txt_Derivacion.Text = anexos.ANI_DESCRIPCION;
                    }
                }
            }
        }


        //private void cargarProductos()
        //{
        //    //btnGuardar.Enabled = false;
        //    List<CUENTAS_PACIENTES> listaCuentasPacientes = new List<CUENTAS_PACIENTES>();
        //    ugrdCuenta.DataSource = null;
        //    try
        //    {
        //        if (txtAtencion.Text.Trim() != string.Empty || txtAtencion.Text.Trim() != "")
        //        {
        //            pedidosArea = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
        //            if (pedidosArea != null)
        //            {
        //                listaCuentasPacientes =
        //                    NegCuentasPacientes.RecuperarCuentaArea(Convert.ToInt32(txtAtencion.Text.Trim()),
        //                                                            (pedidosArea.PEA_CODIGO));
        //                if (listaCuentasPacientes.Count > 0)
        //                {
        //                    ugrdCuenta.DataSource = listaCuentasPacientes;
        //                    UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
        //                    SummarySettings sumTarifa = bandUno.Summaries.Add("CUE_VALOR", SummaryType.Sum,
        //                                                                      bandUno.Columns["CUE_VALOR"]);
        //                }
        //            }
        //        }

        //        //cargo los valores por defecto de la grilla
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message);
        //    }

        //}


        //private void CalcularIvaSuministros(int codigoAtencion)
        //{
        //    CUENTAS_PACIENTES cuentaIva = new CUENTAS_PACIENTES();
        //    cuentaIva = NegCuentasPacientes.RecuperarCuentasIvaS(codigoAtencion, His.Parametros.CuentasPacientes.CodigoSuministros);
        //    List<CUENTAS_PACIENTES> listaCuentaRubro = NegCuentasPacientes.RecuperarCuentasRubros(codigoAtencion,
        //                                                                  His.Parametros.CuentasPacientes.
        //                                                                      RubroSuministros);
        //    if (listaCuentaRubro.Count > 0)
        //    {
        //        Decimal iva = 0;
        //        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
        //        for (int i = 0; i < listaCuentaRubro.Count; i++)
        //        {
        //            cuentas = listaCuentaRubro.ElementAt(i);
        //            iva = iva + Convert.ToDecimal(cuentas.CUE_VALOR);
        //        }
        //        if (cuentaIva == null)
        //        {
        //            cuentaPacientes = new CUENTAS_PACIENTES();
        //            cuentaPacientes.ATE_CODIGO = codigoAtencion;
        //            if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
        //                cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_ALTA;
        //            else
        //                cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
        //            //cuentaPacientes.CUE_FECHA = DateTime.Now;
        //            cuentaPacientes.PRO_CODIGO_BARRAS = His.Parametros.CuentasPacientes.CodigoSuministros;
        //            cuentaPacientes.CUE_DETALLE = "IVA 12% SUMINISTROS";
        //            cuentaPacientes.CUE_VALOR_UNITARIO = (iva * Convert.ToDecimal(0.12));
        //            cuentaPacientes.CUE_CANTIDAD = 1;
        //            cuentaPacientes.CUE_VALOR = (iva * Convert.ToDecimal(0.12));
        //            cuentaPacientes.CUE_ESTADO = 1;
        //            cuentaPacientes.CUE_NUM_FAC = "0";
        //            cuentaPacientes.CUE_NUM_CONTROL = "0";
        //            cuentaPacientes.PRO_CODIGO = "0";
        //            cuentaPacientes.RUB_CODIGO = 26;
        //            cuentaPacientes.CUE_IVA = 0;
        //            cuentaPacientes.PED_CODIGO = Convert.ToInt32(His.Parametros.CuentasPacientes.CodigoServicosI);
        //            cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
        //            cuentaPacientes.CAT_CODIGO = 0;
        //            NegCuentasPacientes.CrearCuenta(cuentaPacientes);
        //        }
        //        else
        //        {
        //            if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
        //                cuentaIva.CUE_FECHA = atencion.ATE_FECHA_ALTA;
        //            else
        //                cuentaIva.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
        //            cuentaIva.CUE_VALOR = (iva * Convert.ToDecimal(0.12));
        //            cuentaIva.CUE_VALOR_UNITARIO = (iva * Convert.ToDecimal(0.12));
        //            NegCuentasPacientes.ModificarCuenta(cuentaIva);
        //        }
        //    }

        //}

        private void CalcularIvaSuministros(int codigoAtencion)
        {
            CUENTAS_PACIENTES cuentaIva = new CUENTAS_PACIENTES();
            cuentaIva = NegCuentasPacientes.RecuperarCuentasIvaS(codigoAtencion, His.Parametros.CuentasPacientes.CodigoSuministros);
            List<CUENTAS_PACIENTES> listaCuentaRubro = NegCuentasPacientes.RecuperarCuentasRubros(codigoAtencion,
                                                                          His.Parametros.CuentasPacientes.
                                                                              RubroSuministros);
            List<CUENTAS_PACIENTES> listaCuentaRubroO = NegCuentasPacientes.RecuperarCuentasRubros(codigoAtencion,
                                                                          His.Parametros.CuentasPacientes.RubroOtrosP);
            if (listaCuentaRubro.Count > 0)
            {
                Decimal iva = 0;
                CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
                for (int i = 0; i < listaCuentaRubro.Count; i++)
                {
                    cuentas = listaCuentaRubro.ElementAt(i);
                    if (cuentas.CUE_ESTADO == 1)
                    {
                        iva = iva + Convert.ToDecimal(cuentas.CUE_VALOR);
                    }
                }
                if (listaCuentaRubroO.Count > 0)
                {
                    for (int i = 0; i < listaCuentaRubroO.Count; i++)
                    {
                        cuentas = listaCuentaRubroO.ElementAt(i);
                        if (cuentas.CUE_IVA > 0)
                        {
                            if (cuentas.CUE_ESTADO == 1)
                                iva = iva + Convert.ToDecimal(cuentas.CUE_VALOR);
                        }
                    }
                }
                if (cuentaIva == null)
                {
                    //cuentaPacientes = new CUENTAS_PACIENTES();
                    //cuentaPacientes.ATE_CODIGO = codigoAtencion;
                    //if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
                    //    cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_ALTA;
                    //else
                    //    cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
                    //cuentaPacientes.PRO_CODIGO_BARRAS = His.Parametros.CuentasPacientes.CodigoSuministros;
                    //cuentaPacientes.CUE_DETALLE = "IVA 12% SUMINISTROS";
                    //string ivaTotal = Convert.ToString(Convert.ToDouble(iva) * 0.12);
                    //cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                    //cuentaPacientes.CUE_CANTIDAD = 1;
                    //cuentaPacientes.CUE_VALOR = cuentaPacientes.CUE_VALOR_UNITARIO;
                    //cuentaPacientes.CUE_ESTADO = 1;
                    //cuentaPacientes.CUE_NUM_FAC = "0";
                    //cuentaPacientes.CUE_NUM_CONTROL = "0";
                    //cuentaPacientes.PRO_CODIGO = "0";
                    //cuentaPacientes.RUB_CODIGO = 26;
                    //cuentaPacientes.CUE_IVA = 0;
                    //cuentaPacientes.PED_CODIGO = Convert.ToInt32(His.Parametros.CuentasPacientes.CodigoServicosI);
                    //cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    //cuentaPacientes.CAT_CODIGO = 0;
                    //NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                }
                else
                {
                    if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
                        cuentaIva.CUE_FECHA = atencion.ATE_FECHA_ALTA;
                    else
                        cuentaIva.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
                    string ivaTotal = Convert.ToString(Convert.ToDouble(iva) * 0.12);
                    cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                    cuentaIva.CUE_VALOR = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                    cuentaIva.CUE_VALOR_UNITARIO = cuentaIva.CUE_VALOR;
                    NegCuentasPacientes.ModificarCuenta(cuentaIva);
                }
            }
        }


        #region Cargar Datos  public void cargarFiltros()


        public void CargarDatos()
        {
            listaAreas = NegPedidos.recuperarListaAreas().OrderBy(a => a.PEA_NOMBRE).ToList();
            cmb_Areas.DataSource = listaAreas;
            cmb_Areas.ValueMember = "PEA_CODIGO";
            cmb_Areas.DisplayMember = "PEA_NOMBRE";
            cmb_Areas.SelectedIndex = 0;

            listaAnexos = NegAnexos.RecuperarListaAnexos(Convert.ToString(His.Parametros.AdmisionParametros.CodigoAnexoParentesco));
            cmb_Parentesco.DataSource = listaAnexos;
            cmb_Parentesco.DisplayMember = "ANI_DESCRIPCION";
            cmb_Parentesco.ValueMember = "ANI_CODIGO";
            cmb_Parentesco.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_Parentesco.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb_Parentesco.SelectedItem = 3;

            listaTipoDiag = NegHcCatalogoSubNivel.RecuperarSubNivel(His.Parametros.CuentasPacientes.CodigoDiagnostico);
            cmb_Diagnostico.DataSource = listaTipoDiag;
            cmb_Diagnostico.DisplayMember = "CA_DESCRIPCION";
            cmb_Diagnostico.ValueMember = "CA_CODIGO";
            cmb_Diagnostico.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_Diagnostico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            listaEstadosCuenta = NegCuentasPacientes.RecuperarEstadosCuenta();
            cmb_EstadoCuenta.DataSource = listaEstadosCuenta;
            cmb_EstadoCuenta.DisplayMember = "ESC_NOMBRE";
            cmb_EstadoCuenta.ValueMember = "ESC_CODIGO";
            cmb_EstadoCuenta.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_EstadoCuenta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb_EstadoCuenta.SelectedIndex = 1;
            cmb_Marca.SelectedIndex = 0;
            ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
            byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
        }

        public void HabilitarBotones(Boolean actualizar, Boolean editar, Boolean guardar, Boolean cancelar, Boolean salir, Boolean detalle)
        {
            btnActualizar.Enabled = actualizar;
            btnEditar.Enabled = editar;
            btnGuardar.Enabled = guardar;
            btnCancelar.Enabled = cancelar;
            btnSalir.Enabled = salir;
            btnDetalle.Enabled = detalle;
        }

        public bool ValidarCampos()
        {
            try
            {
                erroresAtencion.Clear();
                bool validoCampo = true;
                if (ugrdCuenta.Rows.Count <= 0)
                {
                    AgregarError(ugrdCuenta);
                    validoCampo = false;
                }
                //if (dtg_diagnosticos.Rows.Count == 1)
                //{
                //    AgregarError(dtg_diagnosticos);
                //    validoCampo = false;
                //}
                //else
                //{
                //    for (Int16 i = 0; i < dtg_diagnosticos.Rows.Count - 1; i++)
                //    {
                //        DataGridViewRow fila = dtg_diagnosticos.Rows[i];
                //        DataGridViewTextBoxCell caja = (DataGridViewTextBoxCell)this.dtg_diagnosticos.Rows[fila.Index].Cells[1];
                //        if (caja.Value == null)
                //        {
                //            AgregarError(dtg_diagnosticos);
                //            validoCampo = false;
                //        }
                //    }
                //}
                //if (txt_nombreTitular.Text.Trim()== string.Empty)
                //{
                //    AgregarError(txt_nombreTitular);
                //    validoCampo = false;
                //}
                //if (txt_CedulaTitular.Text.Trim() == string.Empty)
                //{
                //    AgregarError(txt_CedulaTitular);
                //    validoCampo = false;
                //}
                //if (txt_CodigoDependencia.Text.Trim() == string.Empty)
                //{
                //    AgregarError(txt_CodigoDependencia);
                //    validoCampo = false;
                //}
                //if (txt_CSeguro.Text.Trim() == string.Empty)
                //{
                //    AgregarError(txt_CSeguro);
                //    validoCampo = false;
                //}
                //if (txt_CSeguro.Text.Trim() == string.Empty)
                //{
                //    AgregarError(txt_CSeguro);
                //    validoCampo = false;
                //} 
                //if (txt_CDerivacion.Text.Trim() == string.Empty)
                //{
                //    AgregarError(txt_CDerivacion);
                //    validoCampo = false;
                //}
                //if (txt_SDerivacion.Text.Trim() == string.Empty)
                //{
                //    AgregarError(txt_SDerivacion);
                //    validoCampo = false;
                //}
                //if (txt_CContingencia.Text.Trim() == string.Empty)
                //{
                //    AgregarError(txt_CContingencia);
                //    validoCampo = false;
                //} 
                //if (txt_CTexamen.Text.Trim() == string.Empty)
                //{
                //    AgregarError(txt_CTexamen);
                //    validoCampo = false;
                //}

                return validoCampo;
            }
            catch (Exception err) { MessageBox.Show(err.Message); return false; }
        }


        public bool ValidarCamposCuentas()
        {
            try
            {
                erroresAtencion.Clear();
                bool validoCampo = true;
                if(dtpFechaPedido.Text == " ")
                {
                    AgregarError(dtpFechaPedido);
                    validoCampo = false;
                }
                if (txtCodigoSic.Text == string.Empty.Trim())
                {
                    AgregarError(txtCodigoSic);
                    validoCampo = false;
                }
                if (txt_Nombre.Text == string.Empty)
                {
                    AgregarError(txt_Nombre);
                    validoCampo = false;
                }
                if (txtObserCuenta.Text == string.Empty)
                {
                    AgregarError(txtObserCuenta);
                    validoCampo = false;
                }

                if (txtValorNeto.Text.ToString() == "0.00")
                {
                    AgregarError(txtValorNeto);
                    validoCampo = false;
                }
                if (txtValorNeto.Text.ToString() == string.Empty)
                {
                    AgregarError(txtValorNeto);
                    validoCampo = false;
                }
                if (txtCantidad.Text == string.Empty)
                {
                    AgregarError(txtCantidad);
                    validoCampo = false;
                }
                //if (txtCantidad.Text == "0")
                //{
                //    AgregarError(txtCantidad);
                //    validoCampo = false;
                //}
                if (txtTotal.Text.ToString() == "0.00")
                {
                    AgregarError(txtTotal);
                    validoCampo = false;
                }
                if (txtTotal.Text.ToString() == string.Empty)
                {
                    AgregarError(txtTotal);
                    validoCampo = false;
                }
                if (txtObserCuenta.Text.Trim() == string.Empty)
                {
                    AgregarError(txtObserCuenta);
                    validoCampo = false;
                }

                return validoCampo;
            }
            catch (Exception err) { MessageBox.Show(err.Message); return false; }
        }

        private void AgregarError(Control control)
        {
            erroresAtencion.SetError(control, "Campo Requerido");
        }

        public void cargarCuenta(int codigoAtencion)
        {
            try
            {
                ugrdCuenta.DataSource = null;
                listaCuentaAtencion = NegCuentasPacientes.RecuperarCuenta(atencion.ATE_CODIGO);
                if (listaCuentaAtencion.Count > 0)
                {
                    listaPedido = NegDetalleCuenta.recuperarCuentaPaciente(codigoAtencion);
                    var query = (from a in listaPedido
                                 group a by new
                                 {
                                     RUBRO = a.RUBRO_NOMBRE
                                 }
                                     into grupo
                                 select new
                                 {
                                     RUBRO = grupo.Key.RUBRO,
                                     DETALLE = grupo,
                                     //TOTAL = grupo.Sum(s => s.TOTAL),
                                     //IVA = grupo.Sum(s => s.IVA),
                                     TOTALES_CUENTA = grupo.Sum(s => s.TOTAL)
                                     //Nro_PRODUCTOS = grupo.Sum(s => s.CANTIDAD)
                                 }).ToList();
                    ugrdCuenta.DataSource = query;

                    //UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
                    //SummarySettings sumTarifa1 = bandUno.Summaries.Add("TOTAL", SummaryType.Sum,
                    //                                                  bandUno.Columns["TOTAL"]);
                    //bandUno = ugrdCuenta.DisplayLayout.Bands[0];
                    //SummarySettings sumTarifa2 = bandUno.Summaries.Add("IVA", SummaryType.Sum,
                    //                                                  bandUno.Columns["IVA"]);
                    //bandUno = ugrdCuenta.DisplayLayout.Bands[0];
                    //SummarySettings sumTarifa3 = bandUno.Summaries.Add("N.PRODUCTOS", SummaryType.Sum,
                    //                                                  bandUno.Columns["Nro_PRODUCTOS"]);
                    UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
                    SummarySettings sumTarTotal = bandUno.Summaries.Add("TOTALES_CUENTA", SummaryType.Sum,
                                                                      bandUno.Columns["TOTALES_CUENTA"]);

                    bandUno = ugrdCuenta.DisplayLayout.Bands[1];
                    SummarySettings sumTar = bandUno.Summaries.Add("TOTAL", SummaryType.Sum,
                                                                      bandUno.Columns["TOTAL"]);

                    bandUno = ugrdCuenta.DisplayLayout.Bands[1];
                    SummarySettings sumCant = bandUno.Summaries.Add("CANTIDAD", SummaryType.Sum,
                                                                      bandUno.Columns["CANTIDAD"]);
                    this.ugrdCuenta.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
                }
                else { ugrdCuenta.DataSource = null; }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void cargarDiagnosticos()
        {
            List<ATENCIONES_SEGUROS_DIAGNOSTICOS> diag = new List<ATENCIONES_SEGUROS_DIAGNOSTICOS>();
            diag = NegAtencionDetalleSeguros.recuperarDiagnosticosAtencion(atdSeguros.ADS_CODIGO);
            if (diag.Count() > 0)
            {
                dtg_diagnosticos.Rows.Clear();
                foreach (ATENCIONES_SEGUROS_DIAGNOSTICOS diagnosticos in diag)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell1 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell2 = new DataGridViewTextBoxCell();
                    DataGridViewCheckBoxCell chkcell = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell chkcell2 = new DataGridViewCheckBoxCell();
                    textcell.Value = diagnosticos.ASD_CODIGO;
                    textcell1.Value = diagnosticos.CIE_CODIGO;
                    textcell2.Value = diagnosticos.ASD_DESCRIPCION;
                    if (diagnosticos.ASD_ESTADO.Value)
                    {
                        chkcell.Value = true;
                        chkcell2.Value = false;
                    }
                    else
                    {
                        chkcell2.Value = true;
                        chkcell.Value = false;
                    }
                    fila.Cells.Add(textcell);
                    fila.Cells.Add(textcell1);
                    fila.Cells.Add(textcell2);
                    fila.Cells.Add(chkcell);
                    fila.Cells.Add(chkcell2);
                    dtg_diagnosticos.Rows.Add(fila);
                    diagnostico = true;
                }
            }
            else
            {
                diagnostico = false;
                HC_EPICRISIS epicrisis =
                    NegEpicrisis.recuperarEpicrisisPorAtencion(Convert.ToInt32(atdSeguros.ATENCIONESReference.EntityKey.EntityKeyValues[0].Value));
                if (epicrisis != null)
                {
                    List<HC_EPICRISIS_DIAGNOSTICO> diagEgreso =
                        NegEpicrisis.recuperarDiagnosticosEpicrisisEgreso(epicrisis.EPI_CODIGO);
                    if (diagEgreso != null)
                    {
                        dtg_diagnosticos.Rows.Clear();
                        foreach (HC_EPICRISIS_DIAGNOSTICO diagE in diagEgreso)
                        {
                            DataGridViewRow fila = new DataGridViewRow();
                            DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell textcell1 = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell textcell2 = new DataGridViewTextBoxCell();
                            DataGridViewCheckBoxCell chkcell = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell chkcell2 = new DataGridViewCheckBoxCell();
                            textcell.Value = null;
                            textcell1.Value = diagE.CIE_CODIGO;
                            textcell2.Value = diagE.HED_DESCRIPCION;
                            if (diagE.HED_ESTADO.Value)
                            {
                                chkcell.Value = true;
                                chkcell2.Value = false;
                            }
                            else
                            {
                                chkcell2.Value = true;
                                chkcell.Value = false;
                            }
                            fila.Cells.Add(textcell);
                            fila.Cells.Add(textcell1);
                            fila.Cells.Add(textcell2);
                            fila.Cells.Add(chkcell);
                            fila.Cells.Add(chkcell2);
                            dtg_diagnosticos.Rows.Add(fila);
                            diagnostico = true;
                        }
                    }
                }
            }
        }

        private void cargarDiagnosticosEpicrisis()
        {
            diagnostico = false;
            DataGridViewRow filas = new DataGridViewRow();
            HC_EPICRISIS epicrisis = NegEpicrisis.recuperarEpicrisisPorAtencion(atencion.ATE_CODIGO);
            if (epicrisis != null)
            {
                List<HC_EPICRISIS_DIAGNOSTICO> diagEgreso = NegEpicrisis.recuperarDiagnosticosEpicrisisEgreso(epicrisis.EPI_CODIGO);
                if (diagEgreso != null)
                {
                    dtg_diagnosticos.Rows.Clear();
                    foreach (HC_EPICRISIS_DIAGNOSTICO diagE in diagEgreso)
                    {
                        DataGridViewRow fila = new DataGridViewRow();
                        DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell textcell1 = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell textcell2 = new DataGridViewTextBoxCell();
                        DataGridViewCheckBoxCell chkcell = new DataGridViewCheckBoxCell();
                        DataGridViewCheckBoxCell chkcell2 = new DataGridViewCheckBoxCell();
                        textcell.Value = diagE.HED_CODIGO;
                        textcell1.Value = diagE.CIE_CODIGO;
                        textcell2.Value = diagE.HED_DESCRIPCION;
                        if (diagE.HED_ESTADO.Value)
                        {
                            chkcell.Value = true;
                            chkcell2.Value = false;
                        }
                        else
                        {
                            chkcell2.Value = true;
                            chkcell.Value = false;
                        }
                        fila.Cells.Add(textcell);
                        fila.Cells.Add(textcell1);
                        fila.Cells.Add(textcell2);
                        fila.Cells.Add(chkcell);
                        fila.Cells.Add(chkcell2);
                        dtg_diagnosticos.Rows.Add(fila);
                        diagnostico = true;
                    }
                }
            }
        }


        private void GuardarDatos()
        {
            atdSeguros = NegAtencionDetalleSeguros.RecuAtencionesDetalleSeguros(atencion.ATE_CODIGO);
            if (atdSeguros != null)
            {
                //atdSeguros.ADS_CODIGO
                //ATEE_CODIGO
                if (((ASEGURADORAS_EMPRESAS)(cmb_Convenio.SelectedItem)).ASE_CODIGO != null)
                {
                    atdSeguros.ADA_CODIGO = Convert.ToInt32(((ASEGURADORAS_EMPRESAS)(cmb_Convenio.SelectedItem)).ASE_CODIGO);
                }
                //atdSeguros.ADA_CODIGO = Convert.ToInt32(((ASEGURADORAS_EMPRESAS)(cmb_Convenio.SelectedItem)).ASE_CODIGO);
                atdSeguros.ADS_ASEGURADO_NOMBRE = txt_nombreTitular.Text;
                atdSeguros.ADS_ASEGURADO_CEDULA = txt_CedulaTitular.Text;
                atdSeguros.ADS_ASEGURADO_PARENTESCO = cmb_Parentesco.SelectedValue.ToString();
                atdSeguros.ADS_ASEGURADO_TELEFONO = txt_TelefonoTitular.Text.Replace("-", string.Empty).ToString();
                atdSeguros.ADS_ASEGURADO_CIUDAD = txt_CiudadTitular.Text;
                atdSeguros.ADS_ASEGURADO_DIRECCION = txt_DireccionTitular.Text;
                //NegAtencionDetalleSeguros.editarDetalleSeguro(adTitularSeguro);
                atdSeguros.ADA_NUM_PANTILLA = 1;
                atdSeguros.ADA_PAC_EDAD = Convert.ToInt32(DateTime.Now.Year - Convert.ToDateTime(paciente.PAC_FECHA_NACIMIENTO.Value).Year);
                atdSeguros.ADA_CONTINGENCIA = txt_CContingencia.Text != "" ? Convert.ToInt32(txt_CContingencia.Text) : 0;
                atdSeguros.ADA_TIPO_DIAGNOSTICO = cmb_Diagnostico.SelectedValue != null ? Convert.ToInt32(cmb_Diagnostico.SelectedValue.ToString()) : 2;
                atdSeguros.ADA_TIEMPO_ANESTESIA = "0";
                atdSeguros.ADA_MARCA = "F";
                atdSeguros.ADA_TIPO_EXAMEN = txt_CTexamen.Text;
                NegAtencionDetalleSeguros.editarDetalleSeguro(atdSeguros);
                guardarDiagnosticos();
                actualizarDetallecategoria();
                atencion.ESC_CODIGO = ((ESTADOS_CUENTA)cmb_EstadoCuenta.SelectedItem).ESC_CODIGO;
                atencion.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
                NegAtenciones.EditarAtencionAdmision(atencion, 2);
                NegCuentasPacientes.ActualizarFechaCuentas(atencion.ATE_CODIGO);
                CuentaCancelada();
                MessageBox.Show("Registro guardado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePickerFechaAlta.Enabled = false;


            }
            else
            {
                int codigoS = NegAtencionDetalleSeguros.ultimoCodigoDetalleCategorias();
                codigoS++;
                atdSeguros = new ATENCIONES_DETALLE_SEGUROS();
                atdSeguros.ADS_CODIGO = codigoS;
                atdSeguros.ATENCIONESReference.EntityKey = atencion.EntityKey;
                if (((ASEGURADORAS_EMPRESAS)(cmb_Convenio.SelectedItem)).ASE_CODIGO != null)
                {
                    atdSeguros.ADA_CODIGO = Convert.ToInt32(((ASEGURADORAS_EMPRESAS)(cmb_Convenio.SelectedItem)).ASE_CODIGO);
                }
                atdSeguros.ADS_ASEGURADO_NOMBRE = txt_nombreTitular.Text;
                atdSeguros.ADS_ASEGURADO_CEDULA = txt_CedulaTitular.Text;
                atdSeguros.ADS_ASEGURADO_PARENTESCO = cmb_Parentesco.SelectedValue.ToString();
                atdSeguros.ADS_ASEGURADO_TELEFONO = txt_TelefonoTitular.Text.Replace("-", string.Empty).ToString();
                atdSeguros.ADS_ASEGURADO_CIUDAD = txt_CiudadTitular.Text;
                atdSeguros.ADS_ASEGURADO_DIRECCION = txt_DireccionTitular.Text;
                atdSeguros.ADA_NUM_PANTILLA = 1;
                atdSeguros.ADA_PAC_EDAD = Convert.ToInt32(DateTime.Now.Year - Convert.ToDateTime(paciente.PAC_FECHA_NACIMIENTO.Value).Year);
                atdSeguros.ADA_CONTINGENCIA = txt_CContingencia.Text != "" ? Convert.ToInt32(txt_CContingencia.Text) : 0;
                atdSeguros.ADA_TIPO_DIAGNOSTICO = Convert.ToInt32(cmb_Diagnostico.SelectedValue.ToString());
                atdSeguros.ADA_TIEMPO_ANESTESIA = "0";
                atdSeguros.ADA_MARCA = "F";
                atdSeguros.ADA_TIPO_EXAMEN = txt_CTexamen.Text;
                NegAtencionDetalleSeguros.CrearDetalleCategorias(atdSeguros);
                guardarDiagnosticos();
                actualizarDetallecategoria();
                atencion.ESC_CODIGO = ((ESTADOS_CUENTA)cmb_EstadoCuenta.SelectedItem).ESC_CODIGO;
                NegAtenciones.EditarAtencionAdmision(atencion, 1);
                CuentaCancelada();
                MessageBox.Show("Registro guardado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CargarPaciente(Convert.ToInt32(txtCodigoPaciente.Text.Trim()));
            CargarAtencion();

        }


        public void GuardarProducto()
        {
            RUBROS rubroM = new RUBROS();
            RUBROS rubroS = new RUBROS();
            Boolean accion = false;
            if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
            {
                rubroM = (RUBROS)(cmb_Rubros.SelectedItem);
                cmb_Rubros.SelectedIndex = 1;
                rubroS = (RUBROS)(cmb_Rubros.SelectedItem);
                cmb_Rubros.SelectedIndex = 0;
            }
            if (cuentaModificada == null)
            {
                cuentaPacientes = new CUENTAS_PACIENTES();
                cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text);
                cuentaPacientes.CUE_FECHA = dtpFechaPedido.Value;
                cuentaPacientes.PRO_CODIGO = txtCodigoSic.Text;
                cuentaPacientes.CUE_DETALLE = (txt_Nombre.Text).ToString();
                cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(precioUnitario);
                cuentaPacientes.CUE_CANTIDAD = Convert.ToDecimal(txtCantidad.Text);
                cuentaPacientes.CUE_VALOR = Convert.ToDecimal(txtTotal.Text);
                cuentaPacientes.CUE_ESTADO = 1;
                cuentaPacientes.CUE_NUM_CONTROL = txtCControl.Text;
                cuentaPacientes.CUE_NUM_FAC = "0";
                cuentaPacientes.PRO_CODIGO_BARRAS = txtCodigoSic.Text.Trim();
                cuentaPacientes.CUE_IVA = Convert.ToDecimal(txtIva.Text);
                cuentaPacientes.DivideFactura = "N";
                cuentaPacientes.FECHA_FACTURA = dtpFechaPedido.Value;
                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                {
                    if ((txtCodigoSic.Text).ToString().Substring(0, 1).Equals("2")) /* Solo marcan iva los productos que empiezan con 2*/
                    {
                        cuentaPacientes.RUB_CODIGO = rubroS.RUB_CODIGO;
                        string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.12);
                        cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                        //Double iva = Math.Round(Convert.ToDouble(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * Convert.ToDouble(0.12)), 2);
                        //cuentaPacientes.CUE_IVA = Convert.ToDecimal(iva);
                    }
                    else
                    {
                        cuentaPacientes.RUB_CODIGO = rubroM.RUB_CODIGO;
                        cuentaPacientes.CUE_IVA = 0;
                    }
                }
                else
                {
                    cuentaPacientes.RUB_CODIGO = Convert.ToInt16(((RUBROS)(cmb_Rubros.SelectedItem)).RUB_CODIGO);
                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
                    {
                        accion = true;
                        if (rdb_ConIva.Checked == true)
                        {
                            string ivaTotal = Convert.ToString(Convert.ToDouble(txtTotal.Text) * 0.12);
                            cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                            //Double iva = Math.Round(Convert.ToDouble(Convert.ToDouble(txtTotal.Text) * Convert.ToDouble(0.12)), 2);
                            //cuentaPacientes.CUE_IVA =Convert.ToDecimal(iva);
                        }
                        else
                            cuentaPacientes.CUE_IVA = 0;
                    }
                    //else
                        //cuentaPacientes.CUE_IVA = 0;
                }
                cuentaPacientes.PED_CODIGO = Convert.ToInt32(((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).DIV_CODIGO);
                cuentaPacientes.ID_USUARIO = Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario);
                cuentaPacientes.CAT_CODIGO = 0;
                cuentaPacientes.CUE_OBSERVACION = txtObserCuenta.Text;
                if (txtCodMedico.Text.Trim() != string.Empty)
                {
                    cuentaPacientes.MED_CODIGO = Convert.ToInt32(txtCodMedico.Text.Trim());
                    cuentaPacientes.Id_Tipo_Medico = this.cmbTipoMedico.SelectedIndex;
                }
                else // Añado el else para que cuando no se ingrese el medico en la DB se inserte el valor 0(Ninguno) / Giovanny Tapia / 09/08/2012
                {
                    cuentaPacientes.MED_CODIGO = 0;
                    cuentaPacientes.Id_Tipo_Medico = 0;
                }

                NuevoAuditoria nuevo = new NuevoAuditoria();
                nuevo.cue_codigoAud = NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                nuevo.cue_detalleAud = cuentaPacientes.CUE_DETALLE;
                nuevo.cue_cantidadAdi = Convert.ToDecimal(cuentaPacientes.CUE_CANTIDAD);
                nuevo.cue_valorUnitarioAdi = Convert.ToDecimal(cuentaPacientes.CUE_VALOR_UNITARIO);
                nuevo.cue_ivaAdi = Convert.ToDecimal(cuentaPacientes.CUE_IVA);
                nuevo.cue_valorAdi = Convert.ToDecimal(cuentaPacientes.CUE_VALOR);
                nuevo.usuarioAdi = His.Entidades.Clases.Sesion.nomUsuario + " -- " + cuentaPacientes.CUE_OBSERVACION;
                nuevo.ate_codigoAud = Convert.ToInt64(cuentaPacientes.ATE_CODIGO);

                NegCuentasPacientes.CreaHistorialNuevoAuditoria(nuevo);

                //cargarCuenta(atencion.ATE_CODIGO);

                MessageBox.Show("Datos Guardados Correctamente.!");
            }
            else
            {
                cuentaModificada.CUE_FECHA = dtpFechaPedido.Value;
                cuentaModificada.PRO_CODIGO = txtCodigoSic.Text;
                cuentaModificada.CUE_DETALLE = (txt_Nombre.Text).ToString();
                cuentaModificada.CUE_VALOR_UNITARIO = Convert.ToDecimal(precioUnitario);
                cuentaModificada.CUE_CANTIDAD = Convert.ToDecimal(txtCantidad.Text);
                cuentaModificada.CUE_VALOR = Convert.ToDecimal(txtTotal.Text);
                cuentaModificada.CUE_NUM_CONTROL = txtCControl.Text;
                cuentaModificada.CUE_IVA = Convert.ToDecimal(txtIva.Text);
                cuentaModificada.PRO_CODIGO_BARRAS = txtCodigoSic.Text.Trim();
                if (cuentaModificada.PED_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                {
                    if ((txtCodigoSic.Text).ToString().Substring(0, 1).Equals("2"))
                    {
                        //cuentaModificada.RUB_CODIGO = rubroS.RUB_CODIGO;
                        string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaModificada.CUE_VALOR) * 0.12);
                        cuentaModificada.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                    }
                    else
                    {
                        //cuentaModificada.RUB_CODIGO = rubroM.RUB_CODIGO;
                        cuentaModificada.CUE_IVA = 0;
                    }
                }
                else
                {
                    //cuentaModificada.CUE_IVA = 0;
                    if (cuentaModificada.PED_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
                    {
                        accion = true;
                        if (rdb_ConIva.Checked == true)
                        {
                            string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaModificada.CUE_VALOR) * 0.12);
                            cuentaModificada.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                            //Double iva = Math.Round(Convert.ToDouble(Convert.ToDouble(txtTotal.Text) * Convert.ToDouble(0.12)), 2);
                            //cuentaModificada.CUE_IVA = Convert.ToDecimal(iva);
                        }
                        else
                            cuentaModificada.CUE_IVA = 0;
                    }
                    //else
                    //    cuentaModificada.CUE_IVA = 0;
                }
                cuentaModificada.RUB_CODIGO = cuentaModificada.RUB_CODIGO;
                cuentaModificada.CUE_OBSERVACION = txtObserCuenta.Text + "\r\nActualizado por el Usuario: " + His.Entidades.Clases.Sesion.codUsuario;

                if (txtCodMedico.Text.Trim() != string.Empty)
                {
                    cuentaModificada.MED_CODIGO = Convert.ToInt32(txtCodMedico.Text.Trim());
                    cuentaModificada.Id_Tipo_Medico = this.cmbTipoMedico.SelectedIndex;
                }
                else // Añado el else para que cuando no se ingrese el medico en la DB se inserte el valor 0(Ninguno) / Giovanny Tapia / 09/08/2012
                {
                    cuentaModificada.MED_CODIGO = 0;
                    cuentaModificada.Id_Tipo_Medico = 0;
                }

                cuentaHistorial = NegCuentasPacientes.RecuperarCuentaId(Convert.ToInt32(cuentaModificada.CUE_CODIGO));
                NegCuentasPacientes.ModificarCuenta(cuentaModificada);
                GuardarCuentaHistorial(cuentaHistorial);
                ActualizarFilaProducto(cuentaModificada);
                MessageBox.Show("Datos Actualizados Correctamente.!");
            }
            cuentaModificada = null;
            limpiarCampos();
            cargarCuenta(atencion.ATE_CODIGO);
            estado = false;

        }

        private void GuardarCuentaHistorial(CUENTAS_PACIENTES cuentaHistorial)
        {
            if (cuentaHistorial != null)
            {
                CUENTAS_PACIENTES_HISTORIAL cuentaPHistorial = new CUENTAS_PACIENTES_HISTORIAL();
                cuentaPHistorial.CPH_FECHA = DateTime.Now;
                cuentaPHistorial.CPH_ID_USUARIO = Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario);
                cuentaPHistorial.CUE_CODIGO = cuentaHistorial.CUE_CODIGO;
                cuentaPHistorial.ATE_CODIGO = cuentaHistorial.ATE_CODIGO;
                cuentaPHistorial.CUE_FECHA = cuentaHistorial.CUE_FECHA;
                cuentaPHistorial.PRO_CODIGO = cuentaHistorial.PRO_CODIGO;
                cuentaPHistorial.CUE_DETALLE = cuentaHistorial.CUE_DETALLE;
                cuentaPHistorial.CUE_VALOR_UNITARIO = cuentaHistorial.CUE_VALOR_UNITARIO;
                cuentaPHistorial.CUE_CANTIDAD = cuentaHistorial.CUE_CANTIDAD;
                cuentaPHistorial.CUE_VALOR = cuentaHistorial.CUE_VALOR;
                cuentaPHistorial.CUE_IVA = cuentaHistorial.CUE_IVA;
                cuentaPHistorial.CUE_ESTADO = cuentaHistorial.CUE_ESTADO;
                cuentaPHistorial.CUE_NUM_FAC = cuentaHistorial.CUE_NUM_FAC;
                cuentaPHistorial.RUB_CODIGO = cuentaHistorial.RUB_CODIGO;
                cuentaPHistorial.PED_CODIGO = cuentaHistorial.PED_CODIGO;
                cuentaPHistorial.ID_USUARIO = cuentaHistorial.ID_USUARIO;
                cuentaPHistorial.CAT_CODIGO = cuentaHistorial.CAT_CODIGO;
                cuentaPHistorial.PRO_CODIGO_BARRAS = cuentaHistorial.PRO_CODIGO_BARRAS;
                cuentaPHistorial.CUE_NUM_CONTROL = cuentaHistorial.CUE_NUM_CONTROL;
                cuentaPHistorial.CUE_OBSERVACION = cuentaHistorial.CUE_OBSERVACION;
                cuentaPHistorial.MED_CODIGO = cuentaHistorial.MED_CODIGO;
                cuentaPHistorial.CUE_ORDER_IMPRESION = cuentaHistorial.CUE_ORDER_IMPRESION;
                NegCuentasPacientes.CrearCuentaHistorial(cuentaPHistorial);
                cuentaHistorial = null;
            }
        }

        private void guardarDiagnosticos()
        {
            try
            {
                //DIAGNOSTICOS
                int indice = 1;
                foreach (DataGridViewRow fila in dtg_diagnosticos.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            ATENCIONES_SEGUROS_DIAGNOSTICOS detalleD = new ATENCIONES_SEGUROS_DIAGNOSTICOS();
                            detalleD.CIE_CODIGO = fila.Cells[1].Value.ToString();
                            if (Convert.ToBoolean(fila.Cells[3].Value))
                                detalleD.ASD_ESTADO = true;
                            else
                                detalleD.ASD_ESTADO = false;
                            detalleD.ATENCIONES_DETALLE_SEGUROSReference.EntityKey = atdSeguros.EntityKey;
                            detalleD.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            detalleD.ASD_DESCRIPCION = fila.Cells[2].Value.ToString();
                            detalleD.ASD_INDICE = indice;
                            if (fila.Cells[0].Value != null)
                            {
                                detalleD.ASD_CODIGO = Convert.ToInt32(fila.Cells[0].Value.ToString());
                                NegAtencionDetalleSeguros.actualizarASDiagnostico(detalleD);
                            }
                            else
                                NegAtencionDetalleSeguros.crearASDiagnostico(detalleD);
                            indice++;
                        }
                    }
                }
            }
            catch (Exception err) { MessageBox.Show("error", err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void actualizarDetallecategoria()
        {
            if (atencionDCategoria != null)
            {

                atencionDCategoria.HCC_CODIGO_DE = txt_CodigoDependencia.Text != "" ? Convert.ToInt32(txt_CodigoDependencia.Text) : 0;
                atencionDCategoria.HCC_CODIGO_TS = txt_CSeguro.Text != "" ? Convert.ToInt32(txt_CSeguro.Text) : 0;
                atencionDCategoria.ADA_AUTORIZACION = Convert.ToString(txt_CDerivacion.Text + "-" + txt_SDerivacion.Text);
                NegAtencionDetalleCategorias.editarDetalleCategoria(atencionDCategoria);
            }
        }

        private void CuentaCancelada()
        {
            if (((ESTADOS_CUENTA)cmb_EstadoCuenta.SelectedItem).ESC_NOMBRE == "FACTURADA")
            {
                List<CUENTAS_PACIENTES> listaCuentasPacientes = listaCuentasPacientes = NegCuentasPacientes.RecuperarCuenta(atencion.ATE_CODIGO);
                foreach (var dtoCuentas in listaCuentasPacientes)
                {
                    dtoCuentas.CUE_ESTADO = 1;
                    //dtoCuentas.CUE_NUM_FAC = txt_Factura_Cod3.Text;
                    NegCuentasPacientes.ModificarCuenta(dtoCuentas);
                }
            }
        }


        /// <summary>
        /// Leer Productos de la Bade de Datos de la tabla Productos
        /// </summary>
        public void CargarProducto()
        {
            DataTable producto = new DataTable();
                    producto = NegProducto.RecuperarProductoIDSIC(Convert.ToInt64(txt_Codigo.Text.Trim()));
            if (producto.Rows.Count > 0)
            {
                txt_Nombre.Text = producto.Rows[0][0].ToString();
                txtCodigoSic.Enabled = true;
                txtCodigoSic.Text = txt_Codigo.Text.Trim();
                txtValorNeto.Text = Math.Round(Convert.ToDouble(producto.Rows[0][1].ToString()), 2).ToString();
                txtCantidad.Text = "1";
                txtTotal.Text = Math.Round(Convert.ToDecimal(txtValorNeto.Text) * Convert.ToDecimal(txtCantidad.Text),2).ToString();
                txtCodigoSic.Enabled = false;
            }
            else
            {
                txtCodigoSic.Enabled = true;
                txt_Codigo.Text = string.Empty;
                txtCodigoSic.Text = string.Empty;
                txt_Nombre.Text = string.Empty;
                txtValorNeto.Text = "0.00";
                txtCantidad.Text = "1";
                txtTotal.Text = "0.00";
            }
        }

        public void CargarTarifarioIess()
        {
        //    dtpFechaPedido.Format = DateTimePickerFormat.Custom;
        //    dtpFechaPedido.CustomFormat = " ";
            TARIFARIO_IESS tarifarioI = NegTarifario.RecuperarTarifarioIess(Convert.ToInt32(txt_Codigo.Text.Trim()));
            if (tarifarioI != null)
            {
                txt_Codigo.Text = (tarifarioI.COD_IESS).ToString();
                txt_Nombre.Text = tarifarioI.NOM_EXA;
                //txtValorNeto.Text = Convert.ToString(tarifarioI.PR_LAB_NV3);
                txtValorNeto.Text = String.Format("{0:0.000}", tarifarioI.PR_LAB_NV3);
                txtCodigoSic.Text = (tarifarioI.COD_IESS).ToString();
                txtCodigoSic.Enabled = false;
            }
            else
            {
                txtCodigoSic.Enabled = true;
                txt_Codigo.Text = string.Empty;
                txtCodigoSic.Text = string.Empty;
                txt_Nombre.Text = string.Empty;
                txtValorNeto.Text = "0.00";
                txtCantidad.Text = "1";
                txtTotal.Text = "0.00";
            }
        }


        public void CargarTarifarioHonorarios()
        {
            //dtpFechaPedido.Format = DateTimePickerFormat.Custom;
            //dtpFechaPedido.CustomFormat = " ";
            List<TARIFARIOS_DETALLE> listaTatifario = new List<TARIFARIOS_DETALLE>();
            listaTatifario = NegTarifario.ListaRecuperarTarifarioHono(txt_Codigo.Text.Trim());
            TARIFARIOS_DETALLE tarifarioH = NegTarifario.RecuperarTarifarioHono(txt_Codigo.Text.Trim());
            if (tarifarioH != null)
            {
                txt_Codigo.Text = (tarifarioH.TAD_REFERENCIA).ToString();
                if (((RUBROS)cmb_Rubros.SelectedItem).RUB_CODIGO == His.Parametros.CuentasPacientes.RubroAnestesia)
                    txt_Nombre.Text = tarifarioH.TAD_DESCRIPCION + (((RUBROS)cmb_Rubros.SelectedItem)).RUB_NOMBRE;
                else
                    txt_Nombre.Text = tarifarioH.TAD_DESCRIPCION;
                txtCodigoSic.Text = (tarifarioH.TAD_REFERENCIA).ToString();
                calcularValor(tarifarioH.TAD_CODIGO);
                txtCodigoSic.Enabled = false;
                txt_Nombre.Focus();
            }
            else
            {
                txtCodigoSic.Enabled = true;
                txt_Codigo.Text = string.Empty;
                txtCodigoSic.Text = "0";
                txt_Nombre.Text = string.Empty;
                txtValorNeto.Text = "0.00";
                txtCantidad.Text = "1";
                txtTotal.Text = "0.00";
                txt_Codigo.Focus();
            }
        }

        private void calcularValor(Int64 codigoTarifarioH)
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            tarifarioH = contexto.TARIFARIOS_DETALLE.Include("ESPECIALIDADES_TARIFARIOS").FirstOrDefault(
                                                        t => t.TAD_CODIGO == codigoTarifarioH);
            Int64 codigo = tarifarioH.ESPECIALIDADES_TARIFARIOS.EST_CODIGO;
            addHonorarioDetalle(codigo, Convert.ToDouble(tarifarioH.TAD_UVR));
            tarifarioHI = contexto.TARIFARIOS_DETALLE.Include("ESPECIALIDADES_TARIFARIOS").FirstOrDefault(t => t.TAD_PADRE == codigoTarifarioH);
        }

        private void addHonorarioDetalle(Int64 codigoEspecialidad, double uvr)
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
                                                c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == 13
                                                    && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);
                double costoU = 0;
                if (convenios != null)
                    costoU = His.General.CalculosCuentas.CalcularRedondeo(String.Format("{0:0.000}", (Convert.ToDouble(convenios.CON_VALOR_UVR) * uvr)));
                else
                {
                    MessageBox.Show("No existe un Convenio", MessageBoxIcon.Information.ToString());
                    return;
                }
                unidadesUvr = Convert.ToDecimal(costoU) * Convert.ToInt32(txtCantidad.Text);
                txtValorNeto.Text = String.Format("{0:0.000}", (Decimal.Round(unidadesUvr, 3)));
                //txtPrecio.Text = txtValorNeto.Text;
                txt_Nombre.Focus();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //try
            //{
            //    HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            //    CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
            //                                    c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == 13
            //                                        && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);
            //    double costoU = 0;
            //    if (convenios != null)
            //        costoU = (Convert.ToDouble(convenios.CON_VALOR_UVR) * uvr);
            //    else
            //    {
            //        MessageBox.Show("No existe un Convenio", MessageBoxIcon.Information.ToString());
            //        return;
            //    }
            //    unidadesUvr = Convert.ToDecimal(costoU) * Convert.ToInt32(txtCantidad.Text);
            //    txtValorNeto.Text = Convert.ToString(Decimal.Round(unidadesUvr, 2));
            //    txtValorNeto.Focus();
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        double IvaActual = 0;
        public void calcularValoresFactura()
        {
            string Valor = "";
            try
            {
                if (txtValorNeto.Text != string.Empty && txtPorcentaje.Text != string.Empty)
                {
                    double porcentaje = Math.Round((Convert.ToDouble(txtPorcentaje.Text) / 100), 3);

                    //Valor=Convert.ToString(Math.Round((Convert.ToDecimal((Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtValorNeto.Text.ToString())) * porcentaje)), 3));

                    Valor = String.Format("{0:0.000}", Math.Round((Convert.ToDecimal((Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtValorNeto.Text.ToString())))),4));
                    IvaActual = 0;
                        if (porcentaje != 0)
                    {
                        IvaActual = Convert.ToDouble(Valor) * porcentaje;
                    }
                    txtTotal.Text = String.Format("{0:0.000}", Valor);
                    txtIva.Text = IvaActual.ToString();
                    double totales = Convert.ToDouble(txtTotal.Text) + IvaActual;
                    txtTotal2.Text= String.Format("{0:0.000}", totales);

                    //Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) *
                    //                 (Convert.ToDouble(txtValorNeto.Text.ToString()))) * porcentaje);
                    //Valor = String.Format("{0:0.000}", Valor);

                    

                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO != His.Parametros.CuentasPacientes.CodigoFarmacia)
                    {
                        if (txtCantidad.Text == "1" || txtCantidad.Text == "0")
                        {
                            precioUnitario = Convert.ToDecimal(txtTotal.Text);
                        }
                        else
                        {
                            precioUnitario = Math.Round(Convert.ToDecimal(txtTotal.Text) / Convert.ToDecimal(txtCantidad.Text.Trim()), 3);
                        }
                    }
                    else
                    {
                        if (txtCantidad.Text == "1" || txtCantidad.Text == "0")
                        {
                            precioUnitario = Convert.ToDecimal(txtTotal.Text);
                        }else
                        precioUnitario = Math.Round(Convert.ToDecimal(txtTotal.Text) / Convert.ToDecimal(txtCantidad.Text.Trim()), 3);
                        }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            //try
            //{
            //    if (txtValorNeto.Text != string.Empty)
            //        txtTotal.Text = Convert.ToString((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(txtValorNeto.Text.ToString())));

            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message);
            //}
        }

        public void calcularValoresFacturaIva()
        {
            try
            {
                if (txtValorNeto.Text != string.Empty && txtPorcentaje.Text != string.Empty)
                {
                    double porcentaje = Math.Round((Convert.ToDouble(txtPorcentaje.Text) / 100), 2);

                    //MessageBox.Show ("Porcentaje");

                    double totalValor = Math.Round(((Convert.ToDouble(txtCantidad.Text) *
                                 (Convert.ToDouble(txtValorNeto.Text.ToString()))) * porcentaje), 2);

                    //MessageBox.Show("Total Valor");

                    //string TotalC = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) *
                    //(Convert.ToDouble(txtValorNeto.Text.ToString()))) * porcentaje);
                    txtTotal.Text = Convert.ToString(Math.Round(((Convert.ToDouble(txtCantidad.Text) *
                                         (Convert.ToDouble(txtValorNeto.Text.ToString()))) + Convert.ToDouble(totalValor)), 2));

                    //MessageBox.Show("Total");

                    //Convert.ToString(Convert.ToInt32(txtCantidad.Text) * (
                    //Convert.ToDouble(txtValorNeto.Text.ToString()) + Convert.ToDouble(totalValor)));
                    precioUnitario = Convert.ToDecimal(Math.Round(Convert.ToDouble(txtTotal.Text) / Convert.ToDouble(txtCantidad.Text.Trim()), 2));

                    //MessageBox.Show("Precio Unitario");

                }
            }
            catch (Exception err)

            {
                MessageBox.Show(err.Message);
            }
        }

        private void CargarMedico(int codMedico)
        {
            DataTable med = NegMedicos.MedicoIDValida(codMedico);
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
                txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            else
                txtNombreMedico.Text = string.Empty;
        }


        private void ActualizarFilaProducto(CUENTAS_PACIENTES cuentaP)
        {
            try
            {
                ugrdCuenta.ActiveRow.Cells["CODIGO"].Value = txt_Codigo.Text;
                ugrdCuenta.ActiveRow.Cells["CODIGO"].Value = txtCodigoSic.Text;
                ugrdCuenta.ActiveRow.Cells["DESCRIPCION"].Value = txt_Nombre.Text;
                ugrdCuenta.ActiveRow.Cells["VALOR"].Value = txtValorNeto.Text;
                ugrdCuenta.ActiveRow.Cells["TOTAL"].Value = txtTotal.Text;
                ugrdCuenta.ActiveRow.Cells["IVA"].Value = cuentaP.CUE_IVA.ToString();
                ugrdCuenta.ActiveRow.Cells["CANTIDAD"].Value = txtCantidad.Text;
                ugrdCuenta.ActiveRow.Cells["CANTIDAD_ORIGINAL"].Value = txtCantidad.Text;
                ugrdCuenta.ActiveRow.Cells["CANTIDAD_DEVUELTA"].Value = txtCantidad.Text;
                //ugrdCuenta.ActiveRow.Cells[9].Value = cuentaP.CUE_IVA.ToString();
                ugrdCuenta.ActiveRow.Cells["FECHA"].Value = dtpFechaPedido.Value;
                ugrdCuenta.ActiveRow.Cells["TipoMedico"].Value = this.cmbTipoMedico.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        #endregion

        #region Eventos

        private void txtAtencion_TextChanged(object sender, EventArgs e)
        {
            try { }
            catch (Exception err) { }
        }


        private void tarifarioList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnSolicitar_Click(object sender, EventArgs e)
        {


        }

        private void cmb_Areas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Areas_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void ImportarArchivoDelimitado(string archivo, char[] separador)
        {

        }


        private void ayudaPacientes_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmAyudaPaciente form = new frmAyudaPaciente();
                form.campoPadre = txtPacienteHCL;
                form.campoAtencion = txtAtencion;
                form.campoCodigo = txtCodigoPaciente;
                form.campoAtencionNumero = txtAtencionNumero; //Toma el numero de atencion de la ventana de ayuda /30/10/2012 / GIOVANNY TAPIA

                form.ShowDialog();
                if (txtCodigoPaciente.Text != string.Empty)
                {
                    CargarPaciente(Convert.ToInt32(txtCodigoPaciente.Text));
                    CargarAtencion();
                    HabilitarBotones(false, true, false, false, true, true);
                }
                cuentaModificada = null;

                //LimpiarCampos();
                //if (Microsoft.VisualBasic.Information.IsNumeric(txtAtencion.Text) == false)
                //    txtAtencion.Text = string.Empty;

                //if (txtAtencion.Text != string.Empty)
                //{

                //    CargarAtencion();
                //}
            }
            catch (Exception er) { MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }



        private void txtPacienteHCL_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoPaciente_TextChanged_1(object sender, EventArgs e)
        {
            btnActualizar.Enabled = true;
            try
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(txtCodigoPaciente.Text) == false)
                    txtCodigoPaciente.Text = string.Empty;
                if (txtCodigoPaciente.Text != string.Empty)
                    CargarPaciente(Convert.ToInt32(txtCodigoPaciente.Text.ToString()));
                else
                {
                    paciente = null;
                    txtPacienteNombre.Text = string.Empty;
                    txtPacienteNombre2.Text = string.Empty;
                    txtPacienteApellidoPaterno.Text = string.Empty;
                    txtPacienteApellidoMaterno.Text = string.Empty;
                    txtPacienteDireccion.Text = string.Empty;
                    txtPacienteHCL.Text = string.Empty;
                    txtPacienteTelf.Text = string.Empty;
                    txtPacienteCedula.Text = string.Empty;

                    //txtPacienteNombre.ReadOnly = false;
                    //txtPacienteNombre2.ReadOnly = false;
                    //txtPacienteApellidoPaterno.ReadOnly = false;
                    //txtPacienteApellidoMaterno.ReadOnly = false;
                    //txtPacienteDireccion.ReadOnly = false;
                    //txtPacienteHCL.ReadOnly = false;
                    //txtPacienteTelf.ReadOnly = false;
                    //txtPacienteCedula.ReadOnly = false;
                }
            }
            catch (Exception err) { }

        }

        private void txtAtencion_TextChanged_1(object sender, EventArgs e)
        {
            //try
            //{
            //    LimpiarCampos();
            //    if (Microsoft.VisualBasic.Information.IsNumeric(txtAtencion.Text) == false)
            //        txtAtencion.Text = string.Empty;

            //    if (txtAtencion.Text != string.Empty)
            //    {

            //        CargarAtencion();
            //        HabilitarBotones(false, true, false, false, true, true);
            //    }
            //}
            //catch (Exception err)
            //{

            //}
        }

        private void ultraGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void ultraGrid_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ultraGrid_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if ((e.KeyCode == Keys.Delete))
            //    {
            //        //int i = ugrdCuenta.ActiveRow.Index;
            //        if (ugrdCuenta.ActiveRow.Cells[0].Value != null)
            //        {
            //            int codCuentaP = Convert.ToInt32(ugrdCuenta.ActiveRow.Cells[0].Value);
            //            frmEliminarProductoCuenta frm = new frmEliminarProductoCuenta(codCuentaP);
            //            frm.ShowDialog();
            //            cargarCuenta(atencion.ATE_CODIGO);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.InnerException.Message);
            //}

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_CodigoDependencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_CodigoDependencia_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.F1))
                {
                    His.Admision.frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(His.Parametros.AdmisionParametros.CodigoAnexoDependencia);
                    frm.ShowDialog();
                    anexoIess = frm.anexos;
                    if (anexoIess.ANI_CODIGO != 0)
                    {
                        txt_CodigoDependencia.Text = (anexoIess.ANI_CODIGO).ToString();
                        txt_Dependencia.Text = anexoIess.ANI_DESCRIPCION;
                        e.Handled = true;
                        SendKeys.SendWait("{TAB}");
                    }
                }
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab))
                {
                    if (txt_CodigoDependencia.Text.Trim() != string.Empty)
                    {
                        anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(txt_CodigoDependencia.Text.Trim()));
                        if (anexoIess != null)
                        {
                            txt_CodigoDependencia.Text = (anexoIess.ANI_CODIGO).ToString();
                            txt_Dependencia.Text = anexoIess.ANI_DESCRIPCION;

                        }
                        else
                        {
                            txt_CodigoDependencia.Text = string.Empty;
                            txt_Dependencia.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txt_Dependencia.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txt_CSeguro_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.F1))
                {
                    His.Admision.frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(His.Parametros.AdmisionParametros.CodigoAnexoTipoSeguro);
                    frm.ShowDialog();
                    anexoIess = frm.anexos;
                    if (anexoIess.ANI_CODIGO != 0)
                    {
                        txt_CSeguro.Text = (anexoIess.ANI_CODIGO).ToString();
                        txt_Seguro.Text = anexoIess.ANI_DESCRIPCION;
                        e.Handled = true;
                        SendKeys.SendWait("{TAB}");
                    }
                }
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab))
                {
                    if (txt_CSeguro.Text.Trim() != string.Empty)
                    {
                        anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(txt_CSeguro.Text.Trim()));
                        if (anexoIess != null)
                        {
                            txt_CSeguro.Text = (anexoIess.ANI_CODIGO).ToString();
                            txt_Seguro.Text = anexoIess.ANI_DESCRIPCION;
                        }
                        else
                        {
                            txt_CSeguro.Text = string.Empty;
                            txt_Seguro.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txt_Seguro.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txt_CTexamen_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.F1))
                {
                    His.Admision.frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(His.Parametros.AdmisionParametros.CodigoAnexoTipoExamen);
                    frm.ShowDialog();
                    anexoIess = frm.anexos;
                    if (anexoIess.ANI_CODIGO != 0)
                    {
                        txt_CTexamen.Text = (anexoIess.ANI_CODIGO).ToString();
                        txt_TExamen.Text = anexoIess.ANI_DESCRIPCION;
                        e.Handled = true;
                        SendKeys.SendWait("{TAB}");
                    }
                }
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab))
                {
                    if (txt_CTexamen.Text.Trim() != string.Empty)
                    {
                        anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(txt_CTexamen.Text.Trim()));
                        if (anexoIess != null)
                        {
                            txt_CTexamen.Text = (anexoIess.ANI_CODIGO).ToString();
                            txt_TExamen.Text = anexoIess.ANI_DESCRIPCION;
                        }
                        else
                        {
                            txt_CTexamen.Text = string.Empty;
                            txt_TExamen.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txt_TExamen.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }

        private void txt_CDerivacion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.F1))
                {
                    His.Admision.frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(His.Parametros.AdmisionParametros.CodigoAnexoDerivacion);
                    frm.ShowDialog();
                    anexoIess = frm.anexos;
                    if (anexoIess.ANI_CODIGO != 0)
                    {
                        txt_CDerivacion.Text = (anexoIess.ANI_COD_PRO).ToString();
                        txt_Derivacion.Text = anexoIess.ANI_DESCRIPCION;
                        e.Handled = true;
                        SendKeys.SendWait("{TAB}");
                    }
                }
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab))
                {
                    if (txt_CodigoDependencia.Text.Trim() != string.Empty)
                    {
                        anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(txt_CDerivacion.Text.Trim()));
                        if (anexoIess != null)
                        {
                            txt_CDerivacion.Text = (anexoIess.ANI_COD_PRO).ToString();
                            txt_Derivacion.Text = anexoIess.ANI_DESCRIPCION;
                        }
                        else
                        {
                            txt_CDerivacion.Text = string.Empty;
                            txt_Derivacion.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txt_Derivacion.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }

        private void txt_CContingencia_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.F1))
                {
                    His.Admision.frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(His.Parametros.AdmisionParametros.CodigoAnexoContingencia);
                    frm.ShowDialog();
                    anexoIess = frm.anexos;
                    if (anexoIess.ANI_CODIGO != 0)
                    {
                        txt_CContingencia.Text = (anexoIess.ANI_CODIGO).ToString();
                        txt_Contigencia.Text = anexoIess.ANI_DESCRIPCION;
                        e.Handled = true;
                        SendKeys.SendWait("{TAB}");
                    }
                }
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab))
                {
                    if (txt_CContingencia.Text.Trim() != string.Empty)
                    {
                        anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(txt_CContingencia.Text.Trim()));
                        if (anexoIess != null)
                        {
                            txt_CContingencia.Text = (anexoIess.ANI_CODIGO).ToString();
                            txt_Contigencia.Text = anexoIess.ANI_DESCRIPCION;
                        }
                        else
                        {
                            txt_CContingencia.Text = string.Empty;
                            txt_Contigencia.Text = string.Empty;
                        }
                    }
                    else
                    {
                        txt_Contigencia.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void dtg_diagnosticos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {
                    DataGridViewRow fila = dtg_diagnosticos.CurrentRow;
                    fila.Cells[1].Value = busqueda.codigo;
                    fila.Cells[2].Value = busqueda.resultado;
                }
                dtg_diagnosticos.Focus();
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (dtg_diagnosticos.CurrentRow != null)
                {
                    if (dtg_diagnosticos.CurrentRow.Cells["codigoDia"].Value != null)
                    {
                        Int32 codigoDetDiag = Convert.ToInt32(dtg_diagnosticos.CurrentRow.Cells["codigoDia"].Value);
                        NegAtencionDetalleSeguros.eliminarASDiagnosticoDetalle(codigoDetDiag);
                        dtg_diagnosticos.Rows.Remove(dtg_diagnosticos.CurrentRow);
                        MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dtg_diagnosticos.Rows.Remove(dtg_diagnosticos.CurrentRow);
                        MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //actualizarDatos();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    GuardarDatos();
                    HabilitarBotones(true, true, false, false, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void cmb_Convenio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                //1
            }
        }

        private void txt_nombreTitular_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_CedulaTitular_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_TelefonoTitular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_Parentesco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_CiudadTitular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_DireccionTitular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_CSeguro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_CDerivacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_SDerivacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_CContingencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_CTexamen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_Diagnostico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_Marca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_CedulaTitular_Leave(object sender, EventArgs e)
        {
            if (txt_CedulaTitular.Text != string.Empty)
                if (NegValidaciones.esCedulaValida(txt_CedulaTitular.Text) != true)
                {
                    txt_CedulaTitular.Focus();
                    MessageBox.Show("Cédula Incorrecta");
                }
        }

        private void txt_CodigoDependencia_TextChanged(object sender, EventArgs e)
        {
            if (txt_CodigoDependencia.Text.Trim() != string.Empty)
            {
                anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(txt_CodigoDependencia.Text.Trim()));
                if (anexoIess != null)
                {
                    txt_CodigoDependencia.Text = (anexoIess.ANI_CODIGO).ToString();
                    txt_Dependencia.Text = anexoIess.ANI_DESCRIPCION;
                }
                else
                {
                    txt_CodigoDependencia.Text = string.Empty;
                    txt_Dependencia.Text = string.Empty;
                }
            }
        }

        private void txt_CSeguro_TextChanged(object sender, EventArgs e)
        {
            if (txt_CSeguro.Text.Trim() != string.Empty)
            {
                anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(txt_CSeguro.Text.Trim()));
                if (anexoIess != null)
                {
                    txt_CSeguro.Text = (anexoIess.ANI_CODIGO).ToString();
                    txt_Seguro.Text = anexoIess.ANI_DESCRIPCION;
                }
                else
                {
                    txt_CSeguro.Text = string.Empty;
                    txt_Seguro.Text = string.Empty;
                }
            }
        }

        private void txt_CDerivacion_TextChanged(object sender, EventArgs e)
        {
            //if (txt_CodigoDependencia.Text.Trim() != string.Empty)
            //{
            //    catalogoSnivel = NegHcCatalogoSubNivel.RecuperarSubNivelCatalogo(His.Parametros.CuentasPacientes.CodigoDerivacion, Convert.ToInt32(txt_CDerivacion.Text.Trim()));
            //    if (catalogoSnivel != null)
            //    {
            //        txt_CDerivacion.Text = (catalogoSnivel.CA_CODIGO).ToString();
            //        txt_Derivacion.Text = catalogoSnivel.CA_DESCRIPCION;
            //    }
            //    else
            //    {
            //        txt_CDerivacion.Text = string.Empty;
            //        txt_Derivacion.Text = string.Empty;
            //    }
            //}
        }

        private void txt_CContingencia_TextChanged(object sender, EventArgs e)
        {
            if (txt_CContingencia.Text.Trim() != string.Empty)
            {
                anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(txt_CContingencia.Text.Trim()));
                if (anexoIess != null)
                {
                    txt_CContingencia.Text = (anexoIess.ANI_CODIGO).ToString();
                    txt_Contigencia.Text = anexoIess.ANI_DESCRIPCION;
                }
                else
                {
                    txt_CContingencia.Text = string.Empty;
                    txt_Contigencia.Text = string.Empty;
                }
            }
        }

        private void txt_CTexamen_TextChanged(object sender, EventArgs e)
        {
            if (txt_CTexamen.Text.Trim() != string.Empty)
            {
                anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(txt_CTexamen.Text.Trim()));
                if (anexoIess != null)
                {
                    txt_CTexamen.Text = (anexoIess.ANI_CODIGO).ToString();
                    txt_TExamen.Text = anexoIess.ANI_DESCRIPCION;
                }
                else
                {
                    txt_CTexamen.Text = string.Empty;
                    txt_TExamen.Text = string.Empty;
                }
            }
        }

        private void bindingNavigatorAtenciones_RefreshItems(object sender, EventArgs e)
        {

        }

        private void cmb_EstadoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetalleCuenta(Convert.ToInt32(txtAtencion.Text.Trim()));
            frm.Show();
        }

        private void cmb_Areas_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            try
            {
                cmb_Rubros.DataSource = null;
                if (cmb_Areas.SelectedItem != null)
                {
                   // limpiarCampos();
                    PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
                    List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
                    if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
                        cmb_Rubros.DataSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
                    else
                        cmb_Rubros.DataSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();

                    cmb_Rubros.DisplayMember = "RUB_NOMBRE".Trim();
                    cmb_Rubros.ValueMember = "RUB_CODIGO";
                    //cargarProductos();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void txt_Codigo_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView listas = new DataGridView();
            try
            {
                if (cuentaModificada == null)
                {
                    if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
                    {
                        dtpFechaPedido.Format = DateTimePickerFormat.Custom;
                        dtpFechaPedido.CustomFormat = " ";
                        if (txt_Codigo.Text.Trim() != string.Empty)
                        {
                            txtCantidad.Text = "1";
                            int codigoArea = Convert.ToInt32(((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO);
                            CargarProducto();
                            //if (His.Parametros.CuentasPacientes.CodigoLaboratorio == codigoArea ||
                            //    His.Parametros.CuentasPacientes.CodigoLaboratorioP == codigoArea)
                            //{
                            //    CargarTarifarioIess();
                            //}
                            //else
                            //{
                            //    //if (codigoArea == His.Parametros.CuentasPacientes.CodigoHonorarios ||

                            //    //    codigoArea == His.Parametros.CuentasPacientes.CodigoServicosI)
                            //    //{
                            //    //    CargarTarifarioHonorarios();
                            //    //}
                            //    //else
                            //        CargarProducto();
                            //}
                            txt_Nombre.Focus();
                        }
                    }
                    else
                    {
                        if ((e.KeyCode == Keys.F1))
                        {
                            int codigoArea = Convert.ToInt32(((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO);
                            RUBROS rubro = (RUBROS)(cmb_Rubros.SelectedItem);
                            PEDIDOS_AREAS pedidosAreas = (PEDIDOS_AREAS)(cmb_Areas.SelectedItem);

                            frmAyudaTarifarios frm = new frmAyudaTarifarios(txtCodMedico.Text.Trim(), dateTimePickerFechaAlta);
                            frm.ShowDialog();
                            listas = frm.lista;
                            if (listas != null)
                            {
                                btnGuardar.Enabled = true;
                                for (Int16 i = 0; i <= listas.RowCount - 2; i++)
                                {
                                    DataGridViewRow fila = listas.Rows[i];


                                    string porcentaje = fila.Cells["PORCENTAJE"].Value.ToString();

                                    string cantidad = fila.Cells["VCANTIDAD"].Value.ToString();
                                    decimal valorAux = Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString());
                                    double aux = (double)valorAux;
                                    valorAux = (decimal)aux;
                                    string referencia = fila.Cells["CODIGO"].Value.ToString(); ;
                                    string detalle = fila.Cells["NOMBRE"].Value.ToString(); ;
                                    cuentaPacientes = new CUENTAS_PACIENTES();
                                    cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
                                    cuentaPacientes.CUE_FECHA = Convert.ToDateTime(fila.Cells["FECHAS"].Value.ToString());
                                    cuentaPacientes.PRO_CODIGO_BARRAS = referencia.Trim();
                                    cuentaPacientes.CUE_DETALLE = detalle.Trim();
                                    if (porcentaje.Trim() == "1.5")
                                        cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
                                    else
                                        cuentaPacientes.CUE_VALOR_UNITARIO = valorAux;
                                    cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cantidad);
                                    if (porcentaje.Trim() == "1.5")
                                        cuentaPacientes.CUE_VALOR = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
                                    else
                                    {
                                        valorAux = (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString())) * (Convert.ToInt16(fila.Cells["PORCENTAJE"].Value.ToString()) / 100));
                                        cuentaPacientes.CUE_VALOR =
                                            Decimal.Round(
                                                Convert.ToInt16(cantidad) *
                                                Decimal.Round(Convert.ToDecimal(Decimal.Round(valorAux, 2)), 2), 2);
                                    }
                                    cuentaPacientes.CUE_IVA = 0;
                                    cuentaPacientes.PED_CODIGO = ((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO;
                                    cuentaPacientes.RUB_CODIGO = rubro.RUB_CODIGO;
                                    cuentaPacientes.CUE_ESTADO = 1;
                                    cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                                    cuentaPacientes.CAT_CODIGO = 0;
                                    cuentaPacientes.PRO_CODIGO = "0";
                                    cuentaPacientes.CUE_NUM_CONTROL = "0";
                                    cuentaPacientes.CUE_NUM_FAC = "0";
                                    cuentaPacientes.MED_CODIGO = Convert.ToInt16(fila.Cells["MEDICOS"].Value.ToString());
                                    NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                                    ugrdCuenta.DataSource = null;

                                }
                            }
                            MessageBox.Show("Datos Guardados");
                            cargarCuenta(Convert.ToInt32(txtAtencion.Text.Trim()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txt_Codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCodigoSic_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (cuentaModificada == null)
                {
                    if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
                    {
                        //dtpFechaPedido.Format = DateTimePickerFormat.Custom;
                        //dtpFechaPedido.CustomFormat = " ";
                        if (txt_Codigo.Text.Trim() == string.Empty)
                        {
                            txt_Codigo.Text = string.Empty;
                            //txtCodigoSic.Text = string.Empty;
                            txt_Nombre.Text = string.Empty;
                            txtValorNeto.Text = "0.00";
                            txtCantidad.Text = "1";
                            txtTotal.Text = "0.00";
                            txtCControl.Text = "0.00";
                        }
                        else
                        {
                            if (txt_Codigo.Text.Trim() != txtCodigoSic.Text.Trim())
                            {
                                txt_Codigo.Text = string.Empty;
                                txt_Nombre.Text = string.Empty;
                                txtValorNeto.Text = "0.00";
                                txtCantidad.Text = "1";
                                txtTotal.Text = "0.00";
                                txtCControl.Text = string.Empty;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtCodigoSic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_Nombre_TextChanged(object sender, EventArgs e)
        {
            if (txtValorNeto.Text.ToString() != string.Empty || txtValorNeto.Text.ToString() != "0.00")
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(txtValorNeto.Text))
                {
                    calcularValoresFactura();
                }
                else
                {
                    txtValorNeto.Text = string.Empty;
                }
            }


            if (txtValorNeto.Text.ToString() == "0.00" || txtValorNeto.Text.ToString() == string.Empty)
            {
                //txt_Codigo.Text = string.Empty;
                //txt_Nombre.Text = string.Empty;
                //txtValorNeto.Text = string.Empty;
                //txtCantidad.Text = string.Empty;
            }
        }

        private void txtValorNeto_TextChanged(object sender, EventArgs e)
        {
            if (txtValorNeto.Text.ToString() != string.Empty || txtValorNeto.Text.ToString() != "0.00")
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(txtValorNeto.Text))
                {
                    calcularValoresFactura();
                }
                else
                {
                    txtValorNeto.Text = string.Empty;
                }
            }


            if (txtValorNeto.Text.ToString() == "0.00" || txtValorNeto.Text.ToString() == string.Empty)
            {
                //txt_Codigo.Text = string.Empty;
                //txt_Nombre.Text = string.Empty;
                //txtValorNeto.Text = string.Empty;
                //txtCantidad.Text = string.Empty;
            }
        }

        private void txtValorNeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                if (txtValorNeto.Text.Trim() != string.Empty)
                    //txtValorNeto.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtValorNeto.Text), 3));
                    txtValorNeto.Text = String.Format("{0:0.000}", Math.Round(Convert.ToDecimal(txtValorNeto.Text), 3));
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
                {
                    if (txtCantidad.Text.Trim() != string.Empty)
                    {
                        if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
                        {
                            if (rdb_ConIva.Checked == true)
                                calcularValoresFacturaIva();
                            else
                                calcularValoresFactura();
                        }
                        else
                        {
                            calcularValoresFactura();
                        }
                        //calcularValoresFactura();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dtpFechaPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void groupBoxMedico_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposCuentas())
                {
                    GuardarProducto();
                    //if (tarifarioHI != null) // Comento ya que el 1.5 segun requerimiento activa salud ya no va/ Giovanny Tapia / 14/12/2012
                    //{
                    //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
                    //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
                    //    txtValorNeto.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
                    //    txtCantidad.Text = "1";
                    //    txtTotal.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
                    //    txtCControl.Text = "0";
                    //    GuardarProducto();
                    //    tarifarioHI = null;
                    //    tarifarioH = null;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }




        private void btnAnadir_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (ValidarCamposCuentas())
                {
                    GuardarProducto();
                    //if (tarifarioHI != null) // Comento ya que el 1.5 segun requerimiento activa salud ya no va/ Giovanny Tapia / 14/12/2012
                    //{
                    //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
                    //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
                    //    txtValorNeto.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) *
                    //        (Convert.ToDouble(unidadesUvr))) * 0.015);
                    //    txtCantidad.Text = "1";
                    //    txtTotal.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) *
                    //        (Convert.ToDouble(unidadesUvr))) * 0.015);
                    //    txtCControl.Text = "0";
                    //    GuardarProducto();
                    //    tarifarioHI = null;
                    //    tarifarioH = null;
                    //}
                    txtCodigoSic.Text = "0";
                }
                else
                {
                    if (txt_Codigo.Text == string.Empty && txtCodigoSic.Text == string.Empty)
                    {
                        GuardarProducto();
                        //if (tarifarioHI != null) // Comento ya que el 1.5 segun requerimiento activa salud ya no va/ Giovanny Tapia / 14/12/2012
                        //{
                        //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
                        //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
                        //    txtValorNeto.Text =
                        //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) *
                        //                        0.015);
                        //    txtCantidad.Text = "1";
                        //    txtTotal.Text =
                        //       Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) *
                        //                        0.015);
                        //    txtCControl.Text = "0";
                        //    GuardarProducto();
                        //    tarifarioHI = null;
                        //    tarifarioH = null;
                        //}
                    }

                }
                txtCodigoSic.Enabled = false;

                txt_Codigo.Focus();
                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                    CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text.Trim()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCamposCuentas())
                {
                    GuardarProducto();
                    formatoFecha = false;
                    //if (tarifarioHI != null) // Comento ya que el 1.5 segun requerimiento activa salud ya no va/ Giovanny Tapia / 14/12/2012
                    //{
                    //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
                    //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
                    //    txtValorNeto.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) *
                    //        (Convert.ToDouble(unidadesUvr))) * 0.015);
                    //    txtCantidad.Text = "1";
                    //    txtTotal.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) *
                    //        (Convert.ToDouble(unidadesUvr))) * 0.015);
                    //    txtCControl.Text = "0";
                    //    GuardarProducto();
                    //    tarifarioHI = null;
                    //    tarifarioH = null;
                    //}
                    txtCodigoSic.Text = "";
                }
                else
                {
                    MessageBox.Show("Datos incompletos", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //else
                //{
                //    if (txt_Codigo.Text == string.Empty && txtCodigoSic.Text == string.Empty)
                //    {
                //        GuardarProducto();
                //        //if (tarifarioHI != null) // Comento ya que el 1.5 segun requerimiento activa salud ya no va/ Giovanny Tapia / 14/12/2012
                //        //{
                //        //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
                //        //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
                //        //    txtValorNeto.Text =
                //        //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) *
                //        //                         0.015);
                //        //    txtCantidad.Text = "1";
                //        //    txtTotal.Text =
                //        //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) *
                //        //                         0.015);
                //        //    txtCControl.Text = "0";
                //        //    GuardarProducto();
                //        //    tarifarioHI = null;
                //        //    tarifarioH = null;
                //        //}
                //    }

                //}
                txtCodigoSic.Enabled = false;
                txt_Codigo.Focus();
                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                    CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text.Trim()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datos incompletos", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                //CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text.Trim()));
                actualizarDatos();
                //LimpiarCampos();
                //if (Microsoft.VisualBasic.Information.IsNumeric(txtAtencion.Text) == false)
                //    txtAtencion.Text = string.Empty;
                //if (txtAtencion.Text != string.Empty)
                //{
                //    CargarAtencion();
                //    HabilitarBotones(false, false, true, false, true, true);
                //}
                //btnActualizar.Enabled = true;
                //if (Microsoft.VisualBasic.Information.IsNumeric(txtCodigoPaciente.Text) == false)
                //    txtCodigoPaciente.Text = string.Empty;
                //if (txtCodigoPaciente.Text != string.Empty)
                //    CargarPaciente(Convert.ToInt32(txtCodigoPaciente.Text.ToString()));
                //else
                //{
                //    paciente = null;
                //    txtPacienteNombre.Text = string.Empty;
                //    txtPacienteNombre2.Text = string.Empty;
                //    txtPacienteApellidoPaterno.Text = string.Empty;
                //    txtPacienteApellidoMaterno.Text = string.Empty;
                //    txtPacienteDireccion.Text = string.Empty;
                //    txtPacienteHCL.Text = string.Empty;
                //    txtPacienteTelf.Text = string.Empty;
                //    txtPacienteCedula.Text = string.Empty;
                //}
            }
            catch (Exception err) { }
        }

        private void ugrdCuenta_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                if (ugrdCuenta.ActiveRow != null)
                {
                    if (Convert.ToDecimal(ugrdCuenta.ActiveRow.Cells["IVA"].Value.ToString()) != 0)
                    {
                        rdb_ConIva.Checked = true;
                        txtPorcentaje.Text = "12";
                    }
                    else
                    {
                        rdb_SinIva.Checked = true;
                        txtPorcentaje.Text = "0";
                    }
                    estado = true;
                    txtCodigoSic.Enabled = false;
                    int codigo = Convert.ToInt32(ugrdCuenta.ActiveRow.Cells[0].Value);
                    txt_Codigo.Text = (ugrdCuenta.ActiveRow.Cells["CODIGO"].Value).ToString();
                    txtCodigoSic.Text = (ugrdCuenta.ActiveRow.Cells["CODIGO"].Value).ToString();
                    txt_Nombre.Text = (ugrdCuenta.ActiveRow.Cells["DESCRIPCION"].Value).ToString();
                    txtValorNeto.Text = (ugrdCuenta.ActiveRow.Cells["VALOR"].Value).ToString();
                    txtCantidad.Text = (ugrdCuenta.ActiveRow.Cells["CANTIDAD"].Value).ToString();
                    txtTotal.Text = (ugrdCuenta.ActiveRow.Cells["TOTAL"].Value).ToString();
                    //txtCControl.Text = (ugrdCuenta.ActiveRow.Cells[14].Value).ToString();                   

                    dtpFechaPedido.Value = Convert.ToDateTime(ugrdCuenta.ActiveRow.Cells["FECHA"].Value);
                    CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
                    cuentas = NegCuentasPacientes.RecuperarCuentaId(codigo);
                    if (cuentas.MED_CODIGO != null && cuentas.MED_CODIGO != 0)
                    {
                        txtCodMedico.Text = cuentas.MED_CODIGO.ToString();
                        CargarMedico(Convert.ToInt32(cuentas.MED_CODIGO));
                        this.cmbTipoMedico.SelectedIndex = Convert.ToInt32(ugrdCuenta.ActiveRow.Cells["TipoMedico"].Value);
                    }
                    else
                    {
                        txtCodMedico.Text = string.Empty;
                        txtNombreMedico.Text = string.Empty;
                        this.cmbTipoMedico.SelectedIndex = 0;
                    }

                    //cmb_Areas.SelectedItem = listaAreas.FirstOrDefault(a => a.DIV_CODIGO == cuentas.PED_CODIGO);
                    cuentaModificada = NegCuentasPacientes.RecuperarCuentaId(codigo);
                    txtObserCuenta.Text = cuentaModificada.CUE_OBSERVACION;
                    if (txt_Codigo.Text.Trim() == string.Empty)
                        txt_Codigo.Text = "0";
                    if (txtCodigoSic.Text.Trim() == string.Empty)
                        txtCodigoSic.Text = "0";
                    ultraTabControlCuenta.SelectedTab = ultraTabControlCuenta.Tabs["ingreso"];
                    txt_Nombre.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnMedico_Click(object sender, EventArgs e)
        {
            //His.Admision.
            List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

            His.Admision.frm_Ayudas ayuda = new His.Admision.frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
            ayuda.campoPadre = txtCodMedico;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
                CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
        }



        #endregion

        private void btnMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtCodMedico.Focus();
            }
        }

        private void txtCodMedico_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.F1))
                {
                    List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                    His.Admision.frm_Ayudas ayuda = new His.Admision.frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
                    ayuda.campoPadre = txtCodMedico;
                    ayuda.ShowDialog();
                    if (ayuda.campoPadre.Text != string.Empty)
                        CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                }
                else
                {
                    if ((e.KeyCode == Keys.Enter))
                    {
                        if (txtCodMedico.Text.Trim() != string.Empty)
                            CargarMedico(Convert.ToInt32(txtCodMedico.Text.Trim()));
                        else
                        {
                            txtNombreMedico.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtCodMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtObserCuenta.Focus();
            }
        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtCantidad.Focus();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            HabilitarBotones(true, false, true, true, true, true);
            dateTimePickerFechaAlta.Enabled = true; // La fecha de alta se puede modificar / giovanny tapia / 12062012
        }

        private void btn_GuardarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                NegFactura Fac = new NegFactura();
                int VerificaCambio;
                VerificaCambio = 0;

                //if (Convert.ToInt16(cmb_EstadoCuenta.SelectedValue) < CodigoEstadoCuenta)
                //{
                //    MessageBox.Show("No se puede cambiar el estado de una cuenta a un estado anterior...Por favor verificar..");
                //    return;
                //}

                //INVOCO AL METODO DE NEGOCIO PARA SABER SI EL USUARIO ACTUAL TIENE LOS PERMISOS NECESARIOS PARA CAMBIAR DE ESTADO UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

                if (Convert.ToInt16(cmb_EstadoCuenta.SelectedValue) != CodigoEstadoCuenta)
                {
                    VerificaCambio = NegCuentasPacientes.PermisosActualizacionCuentas(His.Entidades.Clases.Sesion.codUsuario, CodigoEstadoCuenta, Convert.ToInt16(cmb_EstadoCuenta.SelectedValue));
                    if (VerificaCambio == 0) // SI DEVUELVE 0 NO TIENE PERMISOS PARA CAMBIAR EL ESTADO / GIOVANNY TAPIA / 07/08/2012
                    {
                        MessageBox.Show("No tiene los permisos necesarios para realizar esta acccion.. Por favor verificar..");
                        return;
                    }
                }
                if (((ESTADOS_CUENTA)cmb_EstadoCuenta.SelectedItem).ESC_CODIGO == 1)
                {
                    short codigo_hab = Fac.RecuperarHabitacion(atencion.ATE_CODIGO);
                    short estado = Fac.RecuperarEstado(codigo_hab);
                    if (estado == 2 || estado == 5)
                    {
                        if (atencion.ATE_FECHA_ALTA != null)
                        {
                            TimeSpan diasTrascurridos = DateTime.Now - (DateTime)atencion.ATE_FECHA_ALTA;
                            Int64 dia = NegParametros.RecuperaValorParSvXcodigo(65);
                            if (diasTrascurridos.Days > dia)
                            {
                                MessageBox.Show("No puede desbloquear la cuenta se ha superado \r\nel limite de dias para desbloquear la cuenta", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("La Habitacion ya ha sido ocupada, No se puede revertir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                NegCertificadoMedico Certificado = new NegCertificadoMedico();
                atencion.ESC_CODIGO = ((ESTADOS_CUENTA)cmb_EstadoCuenta.SelectedItem).ESC_CODIGO;
                NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(atencion.ATE_CODIGO, Convert.ToInt32(cmb_EstadoCuenta.SelectedValue), Sesion.codUsuario,"AUDITORIA");
                NegAtenciones.EditarAtencionAdmision(atencion, 1);
                CodigoEstadoCuenta = Convert.ToInt32(atencion.ESC_CODIGO);
                MessageBox.Show("Estado Guardado Exitosamente");
                ReporteAuditoria dataSet = new ReporteAuditoria();
                DataRow dataRow;
                dataRow = dataSet.Tables["Paciente"].NewRow();
                dataRow["Nombre"] = txtPacienteApellidoPaterno.Text + " " + txtPacienteApellidoMaterno.Text + " " + txtPacienteNombre.Text + " " + txtPacienteNombre2.Text;
                dataRow["FechaIngreso"] = dateTimePickerFechaIngreso.Value.ToString();
                dataRow["FechaAlta"] = dateTimePickerFechaAlta.Value.ToString();
                dataRow["Habitacion"] = txtHabitacion.Text;
                //dataRow["Convenio"] = txt_Seguro.Text;
                dataRow["Convenio"] = Certificado.path();
                dataRow["Usuario"] = His.Entidades.Clases.Sesion.nomUsuario;
                dataSet.Tables["Paciente"].Rows.Add(dataRow);

                DataTable detalleNuevos = new DataTable();
                detalleNuevos = NegDetalleCuenta.ListaNuevos(Convert.ToInt64(txtAtencion.Text));
                foreach (DataRow item in detalleNuevos.Rows)
                {

                    dataRow = dataSet.Tables["Nuevos"].NewRow();
                    dataRow["Cue_codigo"] = item["CUE_CODIGO"];
                    dataRow["Cue_detalle"] = item["CUE_DETALLE"];
                    dataRow["Cue_cantidad"] = item["CUE_CANTIDAD"];
                    dataRow["Cue_valor_unitario"] = item["CUE_VALOR_UNITARIO"];
                    dataRow["Cue_iva"] = item["CUE_IVA"];
                    dataRow["Cue_valor"] = item["CUE_VALOR"];
                    dataRow["Usuario"] = item["USUARIO"];
                    dataSet.Tables["Nuevos"].Rows.Add(dataRow);
                }

                DataTable detalleHistoria = new DataTable();
                detalleHistoria = NegDetalleCuenta.MuestraItemsModificados(Convert.ToInt64(txtAtencion.Text));
                foreach (DataRow item in detalleHistoria.Rows)
                {
                    dataRow = dataSet.Tables["Historial"].NewRow();
                    dataRow["Detalle"] = item["Descripcion"];
                    dataRow["CantidadOriginal"] = item["CantidadAnterior"];
                    dataRow["NuevaCantidad"] = item["CantidadActual"];
                    dataRow["ValorOriginal"] = item["SubTotalanterior"];
                    dataRow["NuevoValor"] = item["SubTotalActual"];
                    dataRow["Observacion"] = item["Usuario"] + "--" + item["Observacion"];
                    dataSet.Tables["Historial"].Rows.Add(dataRow);

                }
                DataTable detalleItemEliminados = new DataTable();
                detalleItemEliminados = NegDetalleCuenta.ListaItemsEliminadosCuenta(Convert.ToInt64(txtAtencion.Text));

                foreach (DataRow item in detalleItemEliminados.Rows)
                {
                    dataRow = dataSet.Tables["Eliminados"].NewRow();

                    dataRow["detalle"] = item["Descripcion"];
                    dataRow["precio"] = item["Total"];
                    dataRow["Observacion"] = item["Observacion"];
                    dataSet.Tables["Eliminados"].Rows.Add(dataRow);
                }
                //NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(Convert.ToInt32(txtAtencion.Text), 9, Sesion.codUsuario);
                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "Auditoria", dataSet);
                reporte.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error............! ");
            }
        }

        private void rdb_ConIva_CheckedChanged(object sender, EventArgs e)
        {
            DataTable Tabla17 = new DataTable();
            Tabla17 = NegFactura.Tabla17SRI();
            double porcentajeiva = double.Parse(Tabla17.Rows[0][1].ToString());
            if (rdb_ConIva.Checked == true)
            {
                txtPorcentaje.Text = (porcentajeiva * 100).ToString();
                calcularValoresFactura();
            }
            else
                txtPorcentaje.Text = "";

        }

        private void rdb_SinIva_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_SinIva.Checked == true)
                txtPorcentaje.Text = "0";
            string Valor;
            Valor = String.Format("{0:0.000}", Math.Round((Convert.ToDecimal((Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtValorNeto.Text.ToString())))), 3));


            txtTotal.Text = String.Format("{0:0.000}", Valor);

        }

        private void txtObserCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                btnAnadir.Focus();
            }
        }

        private void c_1(object sender, EventArgs e)
        {

        }
        public bool validoCambio = false;
        private void btnSolicitar_Click_1(object sender, EventArgs e)
        {
            //DataGridView listas = new DataGridView();
            //RUBROS rubro = (RUBROS)(cmb_Rubros.SelectedItem);
            //PEDIDOS_AREAS pedidosAreas = (PEDIDOS_AREAS)(cmb_Areas.SelectedItem);
            //DataTable Productos = new DataTable();
            dtpFechaPedido.Value = DateTime.Now.AddDays(-1);
            dtpFechaPedido.Format = DateTimePickerFormat.Custom;
            dtpFechaPedido.CustomFormat = " ";
            formatoFecha = false;
            DataTable DatosConvenioAtencion = new DataTable();
            //DatosConvenioAtencion = NegAtenciones.CodigoConvenio(Convert.ToInt32(txtAtencionNumero.Text));
            //Int32 CodigoConvenio = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["ATE_CODIGO"].ToString());
            //Int32 CodigoTipoEmpresa = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["CAT_CODIGO"].ToString()); /*CONVENIO*/
            //Int32 CodigoAseguradora = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["ASE_CODIGO"].ToString()); /* EMPRESA */
            //Cambios Edgar 20210517
            PEDIDOS_ESTACIONES pe = new PEDIDOS_ESTACIONES();
            //pe = (PEDIDOS_ESTACIONES)this.cmb_Rubros.SelectedItem;
            var Estacion = (Byte)pe.PEE_CODIGO;
            PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
            RUBROS Rubro1 = new RUBROS();
            Rubro1 = (RUBROS)cmb_Rubros.SelectedItem;
            if(cmb_Rubros .Text == "")
            {
                MessageBox.Show("No puede Solicitar sin escoger una subarea", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ar = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
            var Rubro = (Int16)Rubro1.RUB_CODIGO;
            validoCambio = false;
            FrmAyudaAuditoriaProductos x;
            DatosConvenioAtencion = NegAtenciones.CodigoConvenio(Convert.ToInt64(txtAtencion.Text));
            Int32 CodigoConvenio = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["TE_CODIGO"].ToString());
            Int32 CodigoTipoEmpresa = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["CAT_CODIGO"].ToString()); /*CONVENIO*/
            Int32 CodigoAseguradora = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["ASE_CODIGO"].ToString());

            if (txtCodMedico.Text != "")
            {
                if (cmb_Areas.Text == "TODAS LAS AREAS")
                {
                    //todos = true;
                    x = new FrmAyudaAuditoriaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem), (RUBROS)(this.cmb_Rubros.SelectedItem), true, CodigoTipoEmpresa);

                }
                else
                {
                    x = new FrmAyudaAuditoriaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem), (RUBROS)(this.cmb_Rubros.SelectedItem), false, CodigoTipoEmpresa);

                }
                //x = new FrmAyudaAuditoriaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem), (RUBROS)(this.cmb_Rubros.SelectedItem), true, 0);
                x.ShowDialog();

                if (x.codpro != "")
                {
                    if(x.iva != "0")
                    {
                        rdb_SinIva.Checked = false;
                        rdb_ConIva.Checked = true;
                    }
                    else
                    {
                        rdb_ConIva.Checked = false;
                        rdb_SinIva.Checked = true;
                    }
                    validoCambio = true;
                    DataTable Tabla = new DataTable();
                    Tabla = NegProducto.DivisionProducto(x.codpro, Rubro);
                    if (Tabla.Rows.Count != 0)
                    {
                        int index = cmb_Areas.FindString(Tabla.Rows[0][0].ToString());
                        cmb_Areas.SelectedIndex = index;

                        if (cmb_Areas.SelectedItem != null)
                        {
                            //limpiarCampos();
                            PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
                            List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
                            if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
                                cmb_Rubros.DataSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
                            else
                                cmb_Rubros.DataSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();

                            cmb_Rubros.DisplayMember = "RUB_NOMBRE".Trim();
                            cmb_Rubros.ValueMember = "RUB_CODIGO";
                            /*cargarProductos();*/
                        }

                        int index2 = cmb_Rubros.FindString(Tabla.Rows[0][1].ToString());
                        cmb_Rubros.SelectedIndex = index2;
                        txt_Codigo.Text = x.codpro;
                        if (txt_Codigo.Text == "")
                        {
                            txt_Codigo.Text = x.codpro;
                            txt_Codigo_KeyDown(this, new KeyEventArgs((Keys.Enter)));
                        }
                    }
                    else
                        MessageBox.Show("Producto sin stock en Farmacia","HIS3000",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Debe elegir el Médico para poder solicitar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (txtCantidad.Text.Trim() == string.Empty)
            {
                txtCantidad.Text = "0";
            }
            if ((txtCantidad.Text.Trim() != string.Empty) && (Convert.ToDouble(txtCantidad.Text) > 0))
            {
                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
                {
                    if (rdb_ConIva.Checked == true)
                        calcularValoresFacturaIva();
                    else
                        calcularValoresFactura();
                }
                else
                {
                    calcularValoresFactura();
                }
                //calcularValoresFactura();
            }
            else
            {
                txtCantidad.Text = "0";
                txtTotal.Text = "0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmActualizaFechas frmActualizar = new frmActualizaFechas(atencion.ATE_CODIGO, Convert.ToDateTime(atencion.ATE_FECHA_INGRESO), Convert.ToDateTime(atencion.ATE_FECHA_ALTA));
            frmActualizar.ShowDialog();

            cargarCuenta(atencion.ATE_CODIGO);

        }

        private void frmCuentaDetalleAtencion_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = NegCuentasPacientes.CargaTipoMedico();
            dtpFechaPedido.Format = DateTimePickerFormat.Custom;
            dtpFechaPedido.CustomFormat = " ";
            cmbTipoMedico.DataSource = dt;
            cmbTipoMedico.ValueMember = "Id_Tipo_Medico";
            cmbTipoMedico.DisplayMember = "Descripcion_Tipo_Medico";
        }

        private void txt_Codigo_TextChanged(object sender, EventArgs e)
        {
            if (validoCambio == true)
            {
                DataGridView listas = new DataGridView();
                try
                {
                    if (cuentaModificada == null)
                    {
                        if (txt_Codigo.Text.Trim() != string.Empty)
                        {
                            formatoFecha = false;
                            txtCantidad.Text = "1";
                            int codigoArea = Convert.ToInt32(((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO);
                            if (His.Parametros.CuentasPacientes.CodigoLaboratorio == codigoArea ||
                                His.Parametros.CuentasPacientes.CodigoLaboratorioP == codigoArea)
                            {
                                CargarTarifarioIess();
                            }
                            else
                            {
                                if (codigoArea == His.Parametros.CuentasPacientes.CodigoHonorarios ||
                                    codigoArea == His.Parametros.CuentasPacientes.CodigoImagen ||
                                    codigoArea == His.Parametros.CuentasPacientes.CodigoServicosI)
                                {
                                    CargarTarifarioHonorarios();
                                }
                                else
                                    CargarProducto();
                            }
                            txt_Nombre.Focus();
                            validoCambio = false;
                        }
                        //if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
                        //{

                        //}
                        //else
                        //{
                        //    if ((e.KeyCode == Keys.F1))
                        //    {
                        //        int codigoArea = Convert.ToInt32(((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO);
                        //        RUBROS rubro = (RUBROS)(cmb_Rubros.SelectedItem);
                        //        PEDIDOS_AREAS pedidosAreas = (PEDIDOS_AREAS)(cmb_Areas.SelectedItem);

                        //        frmAyudaTarifarios frm = new frmAyudaTarifarios(txtCodMedico.Text.Trim(), dateTimePickerFechaAlta);
                        //        frm.ShowDialog();
                        //        listas = frm.lista;
                        //        if (listas != null)
                        //        {
                        //            btnGuardar.Enabled = true;
                        //            for (Int16 i = 0; i <= listas.RowCount - 2; i++)
                        //            {
                        //                DataGridViewRow fila = listas.Rows[i];


                        //                string porcentaje = fila.Cells["PORCENTAJE"].Value.ToString();

                        //                string cantidad = fila.Cells["VCANTIDAD"].Value.ToString();
                        //                decimal valorAux = Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString());
                        //                double aux = (double)valorAux;
                        //                valorAux = (decimal)aux;
                        //                string referencia = fila.Cells["CODIGO"].Value.ToString(); ;
                        //                string detalle = fila.Cells["NOMBRE"].Value.ToString(); ;
                        //                cuentaPacientes = new CUENTAS_PACIENTES();
                        //                cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
                        //                cuentaPacientes.CUE_FECHA = Convert.ToDateTime(fila.Cells["FECHAS"].Value.ToString());
                        //                cuentaPacientes.PRO_CODIGO_BARRAS = referencia.Trim();
                        //                cuentaPacientes.CUE_DETALLE = detalle.Trim();
                        //                if (porcentaje.Trim() == "1.5")
                        //                    cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
                        //                else
                        //                    cuentaPacientes.CUE_VALOR_UNITARIO = valorAux;
                        //                cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cantidad);
                        //                if (porcentaje.Trim() == "1.5")
                        //                    cuentaPacientes.CUE_VALOR = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
                        //                else
                        //                {
                        //                    valorAux = (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString())) * (Convert.ToInt16(fila.Cells["PORCENTAJE"].Value.ToString()) / 100));
                        //                    cuentaPacientes.CUE_VALOR =
                        //                        Decimal.Round(
                        //                            Convert.ToInt16(cantidad) *
                        //                            Decimal.Round(Convert.ToDecimal(Decimal.Round(valorAux, 2)), 2), 2);
                        //                }
                        //                cuentaPacientes.CUE_IVA = 0;
                        //                cuentaPacientes.PED_CODIGO = ((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO;
                        //                cuentaPacientes.RUB_CODIGO = rubro.RUB_CODIGO;
                        //                cuentaPacientes.CUE_ESTADO = 1;
                        //                cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                        //                cuentaPacientes.CAT_CODIGO = 0;
                        //                cuentaPacientes.PRO_CODIGO = "0";
                        //                cuentaPacientes.CUE_NUM_CONTROL = "0";
                        //                cuentaPacientes.CUE_NUM_FAC = "0";
                        //                cuentaPacientes.MED_CODIGO = Convert.ToInt16(fila.Cells["MEDICOS"].Value.ToString());
                        //                NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                        //                ugrdCuenta.DataSource = null;

                        //            }
                        //        }
                        //        MessageBox.Show("Datos Guardados");
                        //        cargarCuenta(Convert.ToInt32(txtAtencion.Text.Trim()));
                        //    }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void btnHonorarios_Click(object sender, EventArgs e)
        {
            if (txtCodMedico.Text != "")
            {
                frmCrearHonorarios honorario = new frmCrearHonorarios(Convert.ToInt32(txtCodigoPaciente.Text), Convert.ToInt32(txtCodMedico.Text), codUsuario, Convert.ToInt32(txtAtencion.Text));
                honorario.ShowDialog();
                actualizarDatos();
            }
            else
                MessageBox.Show("Debe ingresar un medico para solicitar honorarios", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtPorcentaje_Leave(object sender, EventArgs e)
        {
            calcularValoresFactura();
        }

        private void txtValorNeto_Leave(object sender, EventArgs e)
        {
            calcularValoresFactura();
        }

        private void txtPorcentaje_TextChanged(object sender, EventArgs e)
        {
            if (txtValorNeto.Text.ToString() != string.Empty || txtValorNeto.Text.ToString() != "0.00")
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(txtValorNeto.Text))
                {
                    calcularValoresFactura();
                }
                else
                {
                    txtValorNeto.Text = string.Empty;
                }
            }


            if (txtValorNeto.Text.ToString() == "0.00" || txtValorNeto.Text.ToString() == string.Empty)
            {
                //txt_Codigo.Text = string.Empty;
                //txt_Nombre.Text = string.Empty;
                //txtValorNeto.Text = string.Empty;
                //txtCantidad.Text = string.Empty;
            }
        }

        private void dtpFechaPedido_MouseDown(object sender, MouseEventArgs e)
        {

            //dtpFechaPedido.Format = DateTimePickerFormat.Short;
            //dtpFechaPedido.Value = DateTime.Now.Date;
        }

        private void dtpFechaPedido_ValueChanged(object sender, EventArgs e)
        {
            if(dtpFechaPedido.CustomFormat == " " && !formatoFecha)
            {
                if(dtpFechaPedido.Value.Date <= DateTime.Now.Date)
                {
                    DateTime fechaElegida = dtpFechaPedido.Value;
                    dtpFechaPedido.Format = DateTimePickerFormat.Short;
                    dtpFechaPedido.Value = fechaElegida;
                    formatoFecha = true;
                    txtObserCuenta.Focus();
                }
                else
                {
                    MessageBox.Show("No puede elegir fecha mayor a la fecha actual", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpFechaPedido.Value = DateTime.Now;
                    return;
                }
            }
            else if(dtpFechaPedido.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("No puede elegir fecha mayor a la fecha actual", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaPedido.Value = DateTime.Now;
                return;
            }
        }
    }
}