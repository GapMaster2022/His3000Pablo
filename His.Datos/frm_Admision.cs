using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Reportes;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using Infragistics;
using His.General;
using System.Reflection;
using His.Parametros;
using His.DatosReportes;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using His.Formulario;

namespace His.Admision
{
    public partial class frm_Admision : Form
    {
        #region Variables

        List<MEDICOS> medicos = new List<MEDICOS>();
        List<DIVISION_POLITICA> paises = new List<DIVISION_POLITICA>();
        List<DIVISION_POLITICA> provincias = new List<DIVISION_POLITICA>();
        List<ESTADO_CIVIL> estadoCivil = new List<ESTADO_CIVIL>();
        List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        List<TIPO_TRATAMIENTO> tipoTratamiento = new List<TIPO_TRATAMIENTO>();
        List<TIPO_CIUDADANO> tipoCiudadano = new List<TIPO_CIUDADANO>();
        List<ATENCION_FORMAS_LLEGADA> formasLlegada = new List<ATENCION_FORMAS_LLEGADA>();
        List<TIPO_REFERIDO> tipoReferido = new List<TIPO_REFERIDO>();
        List<CATEGORIAS_CONVENIOS> categorias = new List<CATEGORIAS_CONVENIOS>();
        List<PACIENTES> pacientes = new List<PACIENTES>();
        List<CLASE_LOCALIDAD> clasesLocalidad = new List<CLASE_LOCALIDAD>();
        List<TIPO_GARANTIA> tipoGarantia = new List<TIPO_GARANTIA>();
        List<TIPO_EMPRESA> tipoEmpresa = new List<TIPO_EMPRESA>();
        List<GRUPO_SANGUINEO> tipoSangre = new List<GRUPO_SANGUINEO>();
        List<ETNIA> etnias = new List<ETNIA>();
        List<TIPO_INGRESO> tipoIngreso = new List<TIPO_INGRESO>();
        List<ANEXOS_IESS> listaAnexos;

        ATENCIONES_DETALLE_SEGUROS adSeguros = new ATENCIONES_DETALLE_SEGUROS();

        MEDICOS medico = null;
        PACIENTES pacienteActual = null;
        PACIENTES_DATOS_ADICIONALES datosPacienteActual = null;
        DtoPacienteDatosAdicionales2 datosPaciente2 = new DtoPacienteDatosAdicionales2();
        DtoAtencionDatosAdicionales ateDA = new DtoAtencionDatosAdicionales();
        ATENCIONES ultimaAtencion = null;
        ASEGURADORAS_EMPRESAS empresaPaciente = null;
        List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = null;
        List<ATENCION_DETALLE_GARANTIAS> detalleGarantias = null;

        PREATENCIONES preAtencioActual = new PREATENCIONES();
        USUARIOS usuario = new USUARIOS();

        public bool convenioA = false;
        public bool cargaciu;
        public bool direccionNueva = true;
        public bool pacienteNuevo = false;
        public bool atencionNueva = false;
        public bool bandCategorias = false;
        public bool bandGarantias = false;
        public bool seleccionTipoIngreso = true;
        int codigoTipoIngreso = 0;

        public ANEXOS_IESS anexoIess;

        DateTime max = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

        #endregion

        #region Constructor
        public frm_Admision()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    //txt_fichaingreso.Text = DateTime.Now.ToString();
        //}

        #endregion

        #region Cargar Datos

        private void CargarDatos()
        {
            try
            {
                btnGenerar.Image = Archivo.imgBtnTools;
                toolStripNuevo.Image = Archivo.imgBtnAdd2;
                btnActualizar.Image = Archivo.imgBtnRestart;
                btnEliminar.Image = Archivo.imgDelete;
                btnGuardar.Image = Archivo.imgBtnFloppy;
                btnCancelar.Image = Archivo.imgBtnStop;
                btnCerrar.Image = Archivo.imgBtnSalir32;
                btnNuevo.Image = Archivo.nuevoPaciente;
                btnFormularios.Image = Archivo.iconoFormulario;
                btnNewAtencion.Image = Archivo.emergency;
                btnImprimir.Image = Archivo.imgBtnImprimir32;
                btnAddAseg.Image = Archivo.imgBtnAdd;
                btnOtroSeguro.Image = Archivo.imgBtnAdd;
                btnAddGar.Image = Archivo.imgBtnAdd;
                uBtnAddAcompaniante.Appearance.Image = Archivo.imgBtnAdd;
                btnHabitaciones.Image = Archivo.imgBtnAdd;


                cb_tipoGarantia.DataSource = tipoGarantia;
                cb_tipoGarantia.DisplayMember = "TG_NOMBRE";
                cb_tipoGarantia.ValueMember = "TG_CODIGO";
                cb_tipoGarantia.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_tipoGarantia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                cmb_tiporeferido.DataSource = tipoReferido;
                cmb_tiporeferido.ValueMember = "TIR_CODIGO";
                cmb_tiporeferido.DisplayMember = "TIR_NOMBRE";
                cmb_tiporeferido.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_tiporeferido.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                cmb_tipoatencion.DataSource = tipoTratamiento;
                cmb_tipoatencion.ValueMember = "TIA_CODIGO";
                cmb_tipoatencion.DisplayMember = "TIA_DESCRIPCION";
                cmb_tipoatencion.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_tipoatencion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                cmb_formaLlegada.DataSource = formasLlegada;
                cmb_formaLlegada.ValueMember = "AFL_CODIGO";
                cmb_formaLlegada.DisplayMember = "AFL_DESCRIPCION";
                cmb_formaLlegada.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_formaLlegada.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb_formaLlegada.SelectedIndex = 1;

                cmb_estadocivil.DataSource = estadoCivil;
                cmb_estadocivil.ValueMember = "ESC_CODIGO";
                cmb_estadocivil.DisplayMember = "ESC_NOMBRE";
                cmb_estadocivil.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_estadocivil.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                cargaciu = false;

                cmb_pais.DataSource = paises;
                cmb_pais.ValueMember = "DIPO_CODIINEC";
                cmb_pais.DisplayMember = "DIPO_NOMBRE";
                cmb_pais.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_pais.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cargaciu = true;

                cmb_ciudadano.DataSource = tipoCiudadano;
                cmb_ciudadano.ValueMember = "TC_CODIGO";
                cmb_ciudadano.DisplayMember = "TC_DESCRIPCION";
                cmb_ciudadano.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_ciudadano.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                cb_gruposanguineo.DataSource = tipoSangre;
                cb_gruposanguineo.ValueMember = "GS_CODIGO";
                cb_gruposanguineo.DisplayMember = "GS_NOMBRE";
                cb_gruposanguineo.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_gruposanguineo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                cb_etnia.DataSource = etnias;
                cb_etnia.ValueMember = "E_CODIGO";
                cb_etnia.DisplayMember = "E_NOMBRE";
                cb_etnia.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_etnia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                cmb_tipoingreso.DataSource = tipoIngreso;
                cmb_tipoingreso.ValueMember = "TIP_CODIGO";
                cmb_tipoingreso.DisplayMember = "TIP_DESCRIPCION";
                cmb_tipoingreso.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_tipoingreso.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                tm_fechaingreso.Start();

                cmbConvenioPago.DataSource = tipoEmpresa;
                cmbConvenioPago.DisplayMember = "TE_DESCRIPCION";
                cmbConvenioPago.ValueMember = "TE_CODIGO";
                cmbConvenioPago.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbConvenioPago.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                cmb_Parentesco.DataSource = listaAnexos;
                cmb_Parentesco.DisplayMember = "ANI_DESCRIPCION";
                cmb_Parentesco.ValueMember = "ANI_CODIGO";
                cmb_Parentesco.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_Parentesco.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                cb_personaFactura.SelectedItem = "PACIENTE";

                dtp_fecnac.MaxDate = max;
                dtp_feCreacion.MaxDate = max;
                dateTimeFecIngreso.MaxDate = max;

                gridAseguradoras.Rows.Clear();
                gridGarantias.Rows.Clear();

                btnActualizar.Enabled = false;
                btnGuardar.Enabled = false;
                ubtnDatosIncompletos.Enabled = false;
                ubtnDatosIncompletos.Tag = false;
                ubtnDatosIncompletos.Appearance.BackColor = Color.LightBlue;
                ubtnDatosIncompletos.Appearance.BackColor2 = Color.SteelBlue;
                btnCancelar.Enabled = false;
                btnImprimir.Enabled = false;
                btnFormularios.Enabled = false;

                btnHabitaciones.Enabled = false; // Bloquea el boton habitaciones del menu / Giovanny Tapia /23062012

                deshabilitarAyudas();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        public void CargarPaciente(string historia, int aux)
        {            
            try
            {


                if (aux == 0)
                {

                    erroresPaciente.Clear();
                    limpiarCamposPaciente();
                    limpiarCamposDireccion();
                    limpiarCamposAtencion(0);
                }
               
                
                if (historia.Trim() != string.Empty)
                {
                    pacienteActual = NegPacientes.RecuperarPacienteID(historia);

                }
                else
                    pacienteActual = null;

                if (pacienteActual != null)
                {
                    //
                    txt_historiaclinica.ReadOnly = false;
                    //
                    if (pacienteActual.PAC_DATOS_INCOMPLETOS == true)
                    {
                        ubtnDatosIncompletos.Tag = true;
                        ubtnDatosIncompletos.Appearance.BackColor = Color.Red;
                        ubtnDatosIncompletos.Appearance.BackColor2 = Color.GhostWhite;
                        ubtnDatosIncompletos.Enabled = true;
                    }
                    else
                    {
                        ubtnDatosIncompletos.Enabled = false;
                    }
                    
                    txt_nombre1.Text = pacienteActual.PAC_NOMBRE1;
                    txt_nombre2.Text = pacienteActual.PAC_NOMBRE2;
                    txt_apellido1.Text = pacienteActual.PAC_APELLIDO_PATERNO;
                    txt_apellido2.Text = pacienteActual.PAC_APELLIDO_MATERNO;

                    if (pacienteActual.PAC_TIPO_IDENTIFICACION == "R")
                        rbRuc.Checked = true;
                    else if (pacienteActual.PAC_TIPO_IDENTIFICACION == "P")
                        rbPasaporte.Checked = true;
                    else if (pacienteActual.PAC_TIPO_IDENTIFICACION == "C")
                        rbCedula.Checked = true;
                    else
                        rbSid.Checked = true;

                    DIVISION_POLITICA provincia = NegDivisionPolitica.DivisionPolitica(pacienteActual.DIPO_CODIINEC);

                    cmb_pais.SelectedItem = paises.FirstOrDefault(p => p.DIPO_CODIINEC == provincia.DIPO_DIPO_CODIINEC);
                    cmb_ciudad.SelectedItem = provincias.FirstOrDefault(c => c.DIPO_CODIINEC == provincia.DIPO_CODIINEC);
                    txt_cedula.Text = pacienteActual.PAC_IDENTIFICACION;
                    dtp_fecnac.Value = pacienteActual.PAC_FECHA_NACIMIENTO.Value.Date;
                    dtp_feCreacion.Value = pacienteActual.PAC_FECHA_CREACION.Date;
                    txt_nacionalidad.Text = pacienteActual.PAC_NACIONALIDAD;
                    cb_etnia.SelectedItem = etnias.FirstOrDefault(e => e.EntityKey == pacienteActual.ETNIAReference.EntityKey);
                    cb_gruposanguineo.SelectedItem = tipoSangre.FirstOrDefault(g => g.EntityKey == pacienteActual.GRUPO_SANGUINEOReference.EntityKey);
                    txt_email.Text = pacienteActual.PAC_EMAIL;

                    if (pacienteActual.PAC_GENERO == "M")
                        rbn_h.Checked = true;
                    else
                        rbn_m.Checked = true;

                    txt_nombreRef.Text = pacienteActual.PAC_REFERENTE_NOMBRE;
                    txt_parentRef.Text = pacienteActual.PAC_REFERENTE_PARENTESCO;
                    txt_telfRef.Text = pacienteActual.PAC_REFERENTE_TELEFONO;
                    txtRefDireccion.Text = pacienteActual.PAC_REFERENTE_DIRECCION;
                    btnGuardar.Enabled = true;
                    btnHabitaciones.Enabled = true; // Habilita el boton Habitaciones del menu / Giovanny Tapia /23062012

                    habilitarAyudas();
                    btnCancelar.Enabled = true;
                    btnImprimir.Enabled = true;
                    btnNewAtencion.Enabled = true;
                    //btnCita.Enabled = true;
                    btnNuevo.Enabled = false;
                    btnActualizar.Enabled = false;

                    panelBotonesDir.Enabled = true;

                    CargarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);
                    CargarUltimaAtencion(pacienteActual.PAC_CODIGO);
                    CargarAtencionesPaciente(pacienteActual.PAC_CODIGO);
                    CargarDatosAdicionales2(pacienteActual.PAC_CODIGO);//lx202005

                    
                }
                else
                {
                    //
                    txt_historiaclinica.ReadOnly = true;
                    //
                    btnGuardar.Enabled = false;
                    ubtnDatosIncompletos.Enabled = false;
                    ubtnDatosIncompletos.Tag = false;
                    ubtnDatosIncompletos.Appearance.BackColor = Color.LightBlue;
                    ubtnDatosIncompletos.Appearance.BackColor2 = Color.SteelBlue;
                    deshabilitarAyudas();
                    btnCancelar.Enabled = false;
                    btnImprimir.Enabled = false;

                    btnFormularios.Enabled = false; // Bloquea el boton formularios del menu / Giovanny Tapia /23062012
                    btnGenerar.Enabled = false;// Bloquea el boton Generar del menu / Giovanny Tapia /23062012
                    btnHabitaciones.Enabled = false; // Bloquea el boton Habitaciones del menu / Giovanny Tapia /23062012

                    btnNewAtencion.Enabled = false;
                    btnNuevo.Enabled = true;

                    panelBotonesDir.Enabled = false;

                    deshabilitarCamposPaciente();
                    deshabilitarCamposDireccion();
                    deshabilitarCamposAtencion();

                }

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void CargarAtencion(string numAtencion)
        {
            try
            {
                //lx201
                deshabilitarCamposAtencion();
                deshabilitarCamposDireccion();
                deshabilitarCamposPaciente();
                btnActualizar.Enabled = false;
                btnGuardar.Enabled = false;
                //lx201

                if (numAtencion != string.Empty)
                    ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(numAtencion);
                else
                    ultimaAtencion = null;

                if (ultimaAtencion != null)
                {

                    txt_numeroatencion.Text = ultimaAtencion.ATE_NUMERO_ATENCION;

                    cmb_tipoingreso.SelectedItem = tipoIngreso.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_INGRESOReference.EntityKey);
                    cmb_tipoatencion.SelectedItem = tipoTratamiento.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey);
                    cmb_tiporeferido.SelectedItem = tipoReferido.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_REFERIDOReference.EntityKey);
                    cmb_formaLlegada.SelectedItem = formasLlegada.FirstOrDefault(f => f.EntityKey == ultimaAtencion.ATENCION_FORMAS_LLEGADAReference.EntityKey);
                    //cmb_medicos.SelectedItem = medicos.FirstOrDefault(m => m.EntityKey == ultimaAtencion.MEDICOSReference.EntityKey);
                    cargar_cbotipoatencion(Convert.ToInt32(ultimaAtencion.TipoAtencion));
                    medico = NegMedicos.medicoPorAtencion(ultimaAtencion.ATE_CODIGO);

                    CargarMedico();

                    HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == ultimaAtencion.HABITACIONESReference.EntityKey);
                    if (hab != null)
                    {
                        Sesion.codHabitacion = hab.hab_Codigo;
                        txt_habitacion.Text = hab.hab_Numero;
                    }

                    //cmbConvenioPago.SelectedItem = tipoPago.FirstOrDefault(f => f.TIF_CODIGO == ultimaAtencion.TIF_CODIGO);

                    txt_diagnostico.Text = ultimaAtencion.ATE_DIAGNOSTICO_INICIAL;
                    txt_observaciones.Text = ultimaAtencion.ATE_OBSERVACIONES;
                    txt_nombreAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                    txt_cedulaAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
                    txt_parentescoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_PARENTESCO;
                    txt_telefonoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO;
                    txt_direccionAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION;
                    txt_ciudadAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CIUDAD;
                    txt_nombreGar.Text = ultimaAtencion.ATE_GARANTE_NOMBRE;
                    txt_cedulaGar.Text = ultimaAtencion.ATE_GARANTE_CEDULA;
                    txt_parentGar.Text = ultimaAtencion.ATE_GARANTE_PARENTESCO;
                    txt_montoGar.Text = ultimaAtencion.ATE_GARANTE_MONTO_GARANTIA.ToString();
                    txt_telfGar.Text = ultimaAtencion.ATE_GARANTE_TELEFONO;
                    txt_dirGar.Text = ultimaAtencion.ATE_GARANTE_DIRECCION;
                    txt_ciudadGar.Text = ultimaAtencion.ATE_GARANTE_CIUDAD;

                    if (ultimaAtencion.ATE_FECHA_INGRESO != null)
                        dateTimeFecIngreso.Value = ultimaAtencion.ATE_FECHA_INGRESO.Value;
                    else
                        dateTimeFecIngreso.Value = DateTime.Now;

                    txt_obPago.Text = ultimaAtencion.TIF_OBSERVACION;

                    cb_personaFactura.SelectedItem = ultimaAtencion.ATE_FACTURA_NOMBRE;

                    detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(ultimaAtencion.ATE_CODIGO);

                    if (detalleCategorias != null)
                    {
                        gridAseguradoras.Rows.Clear();
                        foreach (ATENCION_DETALLE_CATEGORIAS detalle in detalleCategorias)
                        {
                            DataGridViewRow dt = new DataGridViewRow();
                            dt.CreateCells(gridAseguradoras);
                            CATEGORIAS_CONVENIOS cat = categorias.FirstOrDefault(c => c.EntityKey == detalle.CATEGORIAS_CONVENIOSReference.EntityKey);

                            dt.Cells[0].Value = cat.CAT_CODIGO;
                            dt.Cells[1].Value = cat.CAT_NOMBRE;
                            dt.Cells[2].Value = detalle.ADA_AUTORIZACION;
                            dt.Cells[3].Value = detalle.ADA_CONTRATO;
                            dt.Cells[4].Value = detalle.ADA_MONTO_COBERTURA;
                            dt.Cells[6].Value = detalle.HCC_CODIGO_TS.ToString();
                            dt.Cells[8].Value = detalle.HCC_CODIGO_DE.ToString();
                            //Cambio en convenio
                            if (detalle.HCC_CODIGO_TS != null || Convert.ToString(detalle.HCC_CODIGO_TS) != "")
                            {
                                if (detalle.HCC_CODIGO_TS != 0)
                                {
                                    anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(detalle.HCC_CODIGO_TS));
                                    dt.Cells[7].Value = anexoIess.ANI_DESCRIPCION;
                                }
                            }
                            if (detalle.HCC_CODIGO_DE != null || Convert.ToString(detalle.HCC_CODIGO_DE) != "")
                            {
                                if (detalle.HCC_CODIGO_DE != 0)
                                {
                                    anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(detalle.HCC_CODIGO_DE));
                                    dt.Cells[9].Value = anexoIess.ANI_DESCRIPCION;
                                }
                            }
                            gridAseguradoras.Rows.Add(dt);
                        }
                    }
                    else
                    {
                        gridAseguradoras.Rows.Clear();
                    }

                    detalleGarantias = NegAtencionDetalleGarantias.RecuperarDetalleGarantiasAtencion(ultimaAtencion.ATE_CODIGO);
                    if (detalleGarantias != null)
                    {
                        gridGarantias.Rows.Clear();
                        foreach (ATENCION_DETALLE_GARANTIAS detalle in detalleGarantias)
                        {
                            DataGridViewRow dt = new DataGridViewRow();
                            dt.CreateCells(gridGarantias);
                            TIPO_GARANTIA cat = tipoGarantia.FirstOrDefault(c => c.EntityKey == detalle.TIPO_GARANTIAReference.EntityKey);

                            dt.Cells[0].Value = cat.TG_CODIGO;
                            dt.Cells[1].Value = cat.TG_NOMBRE;
                            dt.Cells[2].Value = detalle.ADG_DESCRIPCION;
                            dt.Cells[3].Value = detalle.ADG_DOCUMENTO;
                            dt.Cells[4].Value = detalle.ADG_VALOR;
                            dt.Cells[5].Value = detalle.ADG_FECHA;


                            //lxgrntsx20
                            DataTable rs = NegDietetica.getDataTable("getDetalleGaratias",Convert.ToString(detalle.ADG_CODIGO));
                            dt.Cells["ADG_BANCO"].Value = rs.Rows[0]["ADG_BANCO"].ToString();
                            dt.Cells["ADG_TIPOTARJETA"].Value = rs.Rows[0]["ADG_TIPOTARJETA"].ToString();
                            dt.Cells["ADG_CCV"].Value = rs.Rows[0]["ADG_CCV"].ToString();
                            dt.Cells["ADG_DIASVENCIMIENTO"].Value = rs.Rows[0]["ADG_DIASVENCIMIENTO"].ToString();
                            dt.Cells["ADG_CADUCIDAD"].Value = rs.Rows[0]["ADG_CADUCIDAD"].ToString();
                            dt.Cells["ADG_LOTE"].Value = rs.Rows[0]["ADG_LOTE"].ToString();
                            dt.Cells["ADG_AUTORIZACION"].Value = rs.Rows[0]["ADG_AUTORIZACION"].ToString();
                            dt.Cells["ADG_NUMERO_AUT"].Value = rs.Rows[0]["ADG_NUMERO_AUT"].ToString();
                            dt.Cells["ADG_PERSONA_AUT"].Value = rs.Rows[0]["ADG_PERSONA_AUT"].ToString();
                            if (rs.Rows[0]["ADG_FECHA_AUT"] != null)
                                dt.Cells["ADG_FECHA_AUT"].Value = rs.Rows[0]["ADG_FECHA_AUT"].ToString();
                            dt.Cells["ADG_ESTABLECIMIENTO"].Value = rs.Rows[0]["ADG_ESTABLECIMIENTO"].ToString();
                            dt.Cells["ADG_NROTARJETA"].Value = rs.Rows[0]["ADG_NROTARJETA"].ToString();
                            dt.Cells["ADG_USER"].Value = rs.Rows[0]["UserName"].ToString();
                            dt.Cells["ADG_ESTATUS"].Value = rs.Rows[0]["ADG_ESTATUS"].ToString();




                            gridGarantias.Rows.Add(dt);
                        }
                    }
                    else
                    {
                        gridGarantias.Rows.Clear();
                    }

                    CargarTitularSeguro(ultimaAtencion.ATE_CODIGO);
                    CargarAtencionDatosAdicionales(Convert.ToInt32(ultimaAtencion.ATE_NUMERO_ATENCION));//lx202005
                    btnFormularios.Enabled = true;


                    if (ultimaAtencion.ATE_FECHA_ALTA == null)
                        btnActualizar.Enabled = true;
             
 
                }
                else
                {
                    limpiarCamposAtencion(0);
                    //txt_numeroatencion.Enabled = true;
                    detalleCategorias = null;
                    detalleGarantias = null;
                }
                lockcells();//lxgrntsx

            }
            catch (System.Exception err)
            { MessageBox.Show(err.Message); }
        }

        public void setCampoHistoriaClinica(string historia)
        {
            txt_historiaclinica.Text = historia;
        }


        private void CargarDatosAdicionales2(int codigoPaciente)//lx202005
        {
            datosPaciente2 = null;
            datosPaciente2 = NegPacienteDatosAdicionales.PDA2_find(Convert.ToInt16(codigoPaciente));

            if (datosPaciente2 != null)
            {
                txt_telfRef2.Text= datosPaciente2.REF_TELEFONO_2;
                this.dtpFallecido.Text = datosPaciente2.FEC_FALLECIDO;
                if (datosPaciente2.FALLECIDO)
                    chkFallecido.Checked = true;
                else
                    chkFallecido.Checked = false;
            }
      

        }

        private void lockcells()
        {

            //lxgrntsx2
            foreach (DataGridViewRow dt in gridGarantias.Rows)
            {
                dt.Cells["ADG_USER"].ReadOnly = true;
                dt.Cells["ADG_USER"].Style.BackColor = Color.FromArgb(200, 199, 195);

                if (Convert.ToString(dt.Cells["garantia"].Value).Contains("CHEQUE"))
                {
                    dt.Cells["ADG_TIPOTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_TIPOTARJETA"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_CCV"].ReadOnly = true;
                    dt.Cells["ADG_CCV"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_DIASVENCIMIENTO"].ReadOnly = true;
                    dt.Cells["ADG_DIASVENCIMIENTO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_CADUCIDAD"].ReadOnly = true;
                    dt.Cells["ADG_CADUCIDAD"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_LOTE"].ReadOnly = true;
                    dt.Cells["ADG_LOTE"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_AUTORIZACION"].ReadOnly = true;
                    dt.Cells["ADG_AUTORIZACION"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_NUMERO_AUT"].ReadOnly = true;
                    dt.Cells["ADG_NUMERO_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_PERSONA_AUT"].ReadOnly = true;
                    dt.Cells["ADG_PERSONA_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_FECHA_AUT"].ReadOnly = true;
                    dt.Cells["ADG_FECHA_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_ESTABLECIMIENTO"].ReadOnly = true;
                    dt.Cells["ADG_ESTABLECIMIENTO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_NROTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_NROTARJETA"].Style.BackColor = Color.FromArgb(200, 199, 195);
                }
                else if (Convert.ToString(dt.Cells["garantia"].Value).Contains("EFECTIVO"))
                {
                    dt.Cells["ADG_TIPOTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_TIPOTARJETA"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_CCV"].ReadOnly = true;
                    dt.Cells["ADG_CCV"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_DIASVENCIMIENTO"].ReadOnly = true;
                    dt.Cells["ADG_DIASVENCIMIENTO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_CADUCIDAD"].ReadOnly = true;
                    dt.Cells["ADG_CADUCIDAD"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_LOTE"].ReadOnly = true;
                    dt.Cells["ADG_LOTE"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_AUTORIZACION"].ReadOnly = true;
                    dt.Cells["ADG_AUTORIZACION"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_NUMERO_AUT"].ReadOnly = true;
                    dt.Cells["ADG_NUMERO_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_PERSONA_AUT"].ReadOnly = true;
                    dt.Cells["ADG_PERSONA_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_FECHA_AUT"].ReadOnly = true;
                    dt.Cells["ADG_FECHA_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_ESTABLECIMIENTO"].ReadOnly = true;
                    dt.Cells["ADG_ESTABLECIMIENTO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_BANCO"].ReadOnly = true;
                    dt.Cells["ADG_BANCO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["numdocumento"].ReadOnly = true;
                    dt.Cells["numdocumento"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_NROTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_NROTARJETA"].Style.BackColor = Color.FromArgb(200, 199, 195);
                }
                else if (Convert.ToString(dt.Cells["garantia"].Value).Contains("GARANTIA BANCARIA"))
                {
                    //dt.Cells["Descripcion"].ReadOnly = true;
                    //dt.Cells["numdocumento"].ReadOnly = true;
                    //dt.Cells["valorGar"].ReadOnly = true;
                    //dt.Cells["fecha"].ReadOnly = true;
                    //dt.Cells["ADG_BANCO"].ReadOnly = true;
                    dt.Cells["ADG_TIPOTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_TIPOTARJETA"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_CCV"].ReadOnly = true;
                    dt.Cells["ADG_CCV"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_DIASVENCIMIENTO"].ReadOnly = true;
                    dt.Cells["ADG_DIASVENCIMIENTO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_CADUCIDAD"].ReadOnly = true;
                    dt.Cells["ADG_CADUCIDAD"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_LOTE"].ReadOnly = true;
                    dt.Cells["ADG_LOTE"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_AUTORIZACION"].ReadOnly = true;
                    dt.Cells["ADG_AUTORIZACION"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_NUMERO_AUT"].ReadOnly = true;
                    dt.Cells["ADG_NUMERO_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_PERSONA_AUT"].ReadOnly = true;
                    dt.Cells["ADG_PERSONA_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_FECHA_AUT"].ReadOnly = true;
                    dt.Cells["ADG_FECHA_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_ESTABLECIMIENTO"].ReadOnly = true;
                    dt.Cells["ADG_ESTABLECIMIENTO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_NROTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_NROTARJETA"].Style.BackColor = Color.FromArgb(200, 199, 195);
                }
                else if (Convert.ToString(dt.Cells["garantia"].Value).Contains("VOUCHER"))
                {
                    dt.Cells["ADG_TIPOTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_BANCO"].ReadOnly = true;
                    dt.Cells["ADG_BANCO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                }
                else
                {
                    dt.Cells["ADG_TIPOTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_TIPOTARJETA"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_CCV"].ReadOnly = true;
                    dt.Cells["ADG_CCV"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_DIASVENCIMIENTO"].ReadOnly = true;
                    dt.Cells["ADG_DIASVENCIMIENTO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_CADUCIDAD"].ReadOnly = true;
                    dt.Cells["ADG_CADUCIDAD"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_LOTE"].ReadOnly = true;
                    dt.Cells["ADG_LOTE"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_AUTORIZACION"].ReadOnly = true;
                    dt.Cells["ADG_AUTORIZACION"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_NUMERO_AUT"].ReadOnly = true;
                    dt.Cells["ADG_NUMERO_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_PERSONA_AUT"].ReadOnly = true;
                    dt.Cells["ADG_PERSONA_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_FECHA_AUT"].ReadOnly = true;
                    dt.Cells["ADG_FECHA_AUT"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_ESTABLECIMIENTO"].ReadOnly = true;
                    dt.Cells["ADG_ESTABLECIMIENTO"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["ADG_NROTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_NROTARJETA"].Style.BackColor = Color.FromArgb(200, 199, 195);
                }

                   
            }
        }


        private void CargarAtencionDatosAdicionales(int codigoAtencion) //lx202005
        {
            ateDA = null;
            ateDA = NegAtenciones.atencionDA_find(codigoAtencion);

            if (ateDA != null)
            {
                txtObservacionDA.Text = ateDA.observaciones;
                txtEmpresaDA.Text = ateDA.empresa;
                txtPorcentageDA.Text = Convert.ToString(ateDA.porcentage_discapacidad);
                cmbTiposDiscapacidadesDA.Items.Clear();
                //if (ateDA.tipo_discapacidad != string.Empty)
                //    cmbTiposDiscapacidadesDA.Items.Add(ateDA.tipo_discapacidad);
                //int codTipo = Convert.ToInt16(ateDA.tipo_discapacidad);
                //cargar_cbotipoatencion(Convert.ToInt16(ateDA.cod_atencion));
                cargar_cbotipodiscapacidad(Convert.ToInt16(ateDA.tipo_discapacidad));
                //if (ateDA.paquete.Trim()!="0")
                    cargar_cbotipopaquete(Convert.ToInt32(ateDA.paquete));
                //else
                //    cargar_cbotipopaquete(Convert.ToInt32(ateDA.paquete));
                cargar_cbotiposatenciones_adicionarYhabiliar();
            }
            else {
                DataTable dt2 = NegAtenciones.TiposDiscapacidades();
                cmbTiposDiscapacidadesDA.Items.Clear();
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    cmbTiposDiscapacidadesDA.Items.Add(dt2.Rows[i]["id"].ToString() + "  - " + dt2.Rows[i]["name"].ToString());
                }
                cmbTiposDiscapacidadesDA.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbTiposDiscapacidadesDA.SelectedIndex = 0;
                

            }





        }



        private void CargarDatosAdicionalesPaciente(int keyPaciente)
        {

            datosPacienteActual = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(keyPaciente);
            if (datosPacienteActual != null)
            {
                txt_direccion.Text = datosPacienteActual.DAP_DIRECCION_DOMICILIO;
                txt_telefono1.Text = datosPacienteActual.DAP_TELEFONO1;
                txt_telefono2.Text = datosPacienteActual.DAP_TELEFONO2;
                txt_codPais.Text = datosPacienteActual.COD_PAIS;
                txt_codProvincia.Text = datosPacienteActual.COD_PROVINCIA;
                txt_codCanton.Text = datosPacienteActual.COD_CANTON;
                txt_codParroquia.Text = datosPacienteActual.COD_PARROQUIA;
                txt_codBarrio.Text = datosPacienteActual.COD_SECTOR;
                txt_instuccion.Text = datosPacienteActual.DAP_INSTRUCCION;
                txt_ocupacion.Text = datosPacienteActual.DAP_OCUPACION;
                cmb_estadocivil.SelectedItem = estadoCivil.FirstOrDefault(e => e.EntityKey == datosPacienteActual.ESTADO_CIVILReference.EntityKey);
                cmb_ciudadano.SelectedItem = tipoCiudadano.FirstOrDefault(c => c.EntityKey == datosPacienteActual.TIPO_CIUDADANOReference.EntityKey);

                if (datosPacienteActual.EMP_CODIGO != null)
                {
                    int codEmp = (Int16)datosPacienteActual.EMP_CODIGO;
                    empresaPaciente = aseguradoras.FirstOrDefault(e => e.ASE_CODIGO == codEmp);
                    CargarEmpresa(empresaPaciente.ASE_RUC);
                }
                else
                {
                    empresaPaciente = null;
                    txt_empresa.Text = datosPacienteActual.DAP_EMP_NOMBRE;
                    txt_direcEmp.Text = datosPacienteActual.DAP_EMP_DIRECCION;
                    txt_telfEmp.Text = datosPacienteActual.DAP_EMP_TELEFONO;
                    txt_ciudadEmp.Text = datosPacienteActual.DAP_EMP_CIUDAD;
                }
                CargarRegistroCambiosPaciente(keyPaciente);

            }
        }

        private void CargarRegistroCambiosPaciente(int keyPaciente)
        {
            registroCambios.DataSource = NegPacienteDatosAdicionales.listaDatosAdicionalesDto(keyPaciente);
        }

        private void CargarAtencionesPaciente(int keyPaciente)
        {
            List<DtoAtenciones> dataAtenciones = NegAtenciones.RecuperarAtencionesPaciente(keyPaciente);

            if (dataAtenciones != null)
            {
                gridAtenciones.DataSource = dataAtenciones.Select(n => new
                {
                    CODIGO = n.ATE_CODIGO,
                    NUM_ATENCION = n.ATE_NUMERO_ATENCION,
                    FECHA_INGRESO = n.ATE_FECHA_INGRESO,
                    FECHA_ALTA = n.ATE_FECHA_ALTA,
                    NUM_CONTROL = n.ATE_NUMERO_CONTROL,
                    FACTURA = n.ATE_FACTURA_PACIENTE,
                    FECHA_FACTURA = n.ATE_FACTURA_FECHA,
                    REFERIDO = n.ATE_REFERIDO,
                    ESTADO = n.ATE_ESTADO,
                    HABITACION = n.HAB_NUMERO
                }).ToList();
            }
            else
            {
                gridAtenciones.DataSource = null;
            }

        }

        private void CargarUltimaAtencion(int keyPaciente)
        {
            ultimaAtencion = NegAtenciones.RecuperarUltimaAtencion(keyPaciente);
            CargarAtencion();
        }

        public void CargarAtencion()
        {
            try
            {
                if (ultimaAtencion != null)
                {


                    txt_numeroatencion.Text = ultimaAtencion.ATE_NUMERO_ATENCION;

                    cmb_tipoingreso.SelectedItem = tipoIngreso.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_INGRESOReference.EntityKey);
                    

                    cmb_tipoatencion.SelectedItem = tipoTratamiento.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey);
                    cmb_tiporeferido.SelectedItem = tipoReferido.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_REFERIDOReference.EntityKey);
                    cmb_formaLlegada.SelectedItem = formasLlegada.FirstOrDefault(f => f.EntityKey == ultimaAtencion.ATENCION_FORMAS_LLEGADAReference.EntityKey);
                    medico = NegMedicos.medicoPorAtencion(ultimaAtencion.ATE_CODIGO);

                    CargarMedico();

                    HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == ultimaAtencion.HABITACIONESReference.EntityKey);

                    if (hab != null)
                    {
                        Sesion.codHabitacion = hab.hab_Codigo;
                        txt_habitacion.Text = hab.hab_Numero;
                    }
                    else
                    {
                        hab = NegHabitaciones.RecuperarHabitacionId(Convert.ToInt16(ultimaAtencion.HABITACIONESReference.EntityKey.EntityKeyValues[0].Value));
                        if (hab != null)
                        {
                            Sesion.codHabitacion = hab.hab_Codigo;
                            txt_habitacion.Text = hab.hab_Numero;
                        }
                    }

                    txt_diagnostico.Text = ultimaAtencion.ATE_DIAGNOSTICO_INICIAL;
                    txt_observaciones.Text = ultimaAtencion.ATE_OBSERVACIONES;
                    txt_nombreAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                    txt_cedulaAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
                    txt_parentescoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_PARENTESCO;
                    txt_telefonoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO;
                    txt_direccionAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION;
                    txt_ciudadAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CIUDAD;
                    txt_nombreGar.Text = ultimaAtencion.ATE_GARANTE_NOMBRE;
                    txt_cedulaGar.Text = ultimaAtencion.ATE_GARANTE_CEDULA;
                    txt_parentGar.Text = ultimaAtencion.ATE_GARANTE_PARENTESCO;
                    txt_montoGar.Text = ultimaAtencion.ATE_GARANTE_MONTO_GARANTIA.ToString();
                    txt_telfGar.Text = ultimaAtencion.ATE_GARANTE_TELEFONO;
                    txt_dirGar.Text = ultimaAtencion.ATE_GARANTE_DIRECCION;
                    txt_ciudadGar.Text = ultimaAtencion.ATE_GARANTE_CIUDAD;

                    
                    int codTipoAtencion1 = Convert.ToInt16(ultimaAtencion.TipoAtencion);//lx202005
                    cargar_cbotipoatencion(codTipoAtencion1);//lx202005

                    if (ultimaAtencion.ATE_FECHA_INGRESO != null)
                        dateTimeFecIngreso.Value = ultimaAtencion.ATE_FECHA_INGRESO.Value;
                    else
                        dateTimeFecIngreso.Value = DateTime.Now;
                    /*discapacidad*/

                    if (ultimaAtencion.ate_discapacidad == true) //lx202005
                    {
                        this.chkDiscapacidad.Checked = true;
                    }
                    else
                    {
                        this.chkDiscapacidad.Checked = false;
                    }

                    if (this.chkDiscapacidad.Checked == true)
                    {
                        if (ultimaAtencion.ate_carnet_conadis != null && ultimaAtencion.ate_carnet_conadis != "")
                        {
                            txtIdDiscapacidad.Text = ultimaAtencion.ate_carnet_conadis;
                        }
                        else
                        {
                            txtIdDiscapacidad.Text = "";
                        }
                    }
                    else
                    {
                        txtIdDiscapacidad.Text = "";
                    }

                    /**/

                    this.cmbAccidente.SelectedIndex = Convert.ToInt32(ultimaAtencion.ATE_ID_ACCIDENTE);

                    txt_obPago.Text = ultimaAtencion.TIF_OBSERVACION;
                    //                        txtPersonaEntregaPac.Text = ultimaAtencion.ATE_QUIEN_ENTREGA_PAC == null ? " " : ultimaAtencion.ATE_QUIEN_ENTREGA_PAC;
                    txtReferidoDe.Text = ultimaAtencion.ATE_REFERIDO_DE;
                    //cmbFormaPago.SelectedItem = ultimaAtencion.FOR_PAGO;  

                    cb_personaFactura.SelectedItem = ultimaAtencion.ATE_FACTURA_NOMBRE;

                    detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(ultimaAtencion.ATE_CODIGO);

                    if (detalleCategorias != null)
                    {
                        gridAseguradoras.Rows.Clear();
                        foreach (ATENCION_DETALLE_CATEGORIAS detalle in detalleCategorias)
                        {
                            DataGridViewRow dt = new DataGridViewRow();
                            dt.CreateCells(gridAseguradoras);
                            //CATEGORIAS_CONVENIOS cat = categorias.FirstOrDefault(c => c.EntityKey == detalle.CATEGORIAS_CONVENIOSReference.EntityKey);
                            CATEGORIAS_CONVENIOS cat = NegCategorias.RecuperaCategoriaID(Convert.ToInt16(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value));
                            dt.Cells[0].Value = cat.CAT_CODIGO.ToString();
                            dt.Cells[1].Value = cat.CAT_NOMBRE.ToString();
                            dt.Cells[2].Value = detalle.ADA_AUTORIZACION;
                            dt.Cells[3].Value = detalle.ADA_CONTRATO;
                            dt.Cells[4].Value = detalle.ADA_MONTO_COBERTURA;
                            dt.Cells[5].Value = detalle.ADA_CODIGO.ToString();
                            dt.Cells[6].Value = detalle.HCC_CODIGO_TS.ToString();
                            dt.Cells[8].Value = detalle.HCC_CODIGO_DE.ToString();

                            dt.Cells[10].Value = detalle.HCC_CODIGO_ES.ToString();
                            //dt.Cells[11].Value= detalle.h

                            //Cambio en convenio
                            if (detalle.HCC_CODIGO_TS != null || Convert.ToString(detalle.HCC_CODIGO_TS) != "")
                            {
                                if (detalle.HCC_CODIGO_TS != 0)
                                {
                                    anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(detalle.HCC_CODIGO_TS));
                                    dt.Cells[7].Value = anexoIess.ANI_DESCRIPCION;
                                }
                            }
                            if (detalle.HCC_CODIGO_DE != null || Convert.ToString(detalle.HCC_CODIGO_DE) != "")
                            {
                                if (detalle.HCC_CODIGO_DE != 0)
                                {
                                    anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(detalle.HCC_CODIGO_DE));
                                    dt.Cells[9].Value = anexoIess.ANI_DESCRIPCION;
                                }
                            }

                            if (detalle.HCC_CODIGO_ES != null || Convert.ToString(detalle.HCC_CODIGO_ES) != "")
                            {
                                if (detalle.HCC_CODIGO_ES != 0)
                                {
                                    //dt.Cells[11].Value = NegAnexos.RecuperarAnexos1(Convert.ToInt32(detalle.HCC_CODIGO_ES));
                                    //dt.Cells[11].Value = anexoIess1;
                                }
                            }

                            gridAseguradoras.Rows.Add(dt);
                        }
                    }
                    else
                    {
                        gridAseguradoras.Rows.Clear();
                    }

                    detalleGarantias = NegAtencionDetalleGarantias.RecuperarDetalleGarantiasAtencion(ultimaAtencion.ATE_CODIGO);
                    if (detalleGarantias != null)
                    {
                        gridGarantias.Rows.Clear();
                        foreach (ATENCION_DETALLE_GARANTIAS detalle in detalleGarantias)
                        {
                            DataGridViewRow dt = new DataGridViewRow();
                            dt.CreateCells(gridGarantias);
                            TIPO_GARANTIA cat = tipoGarantia.FirstOrDefault(c => c.EntityKey == detalle.TIPO_GARANTIAReference.EntityKey);

                            dt.Cells[0].Value = cat.TG_CODIGO.ToString();
                            dt.Cells[1].Value = cat.TG_NOMBRE.ToString();
                            dt.Cells[2].Value = detalle.ADG_DESCRIPCION;
                            dt.Cells[3].Value = detalle.ADG_DOCUMENTO;
                            dt.Cells[4].Value = detalle.ADG_VALOR.ToString();
                            dt.Cells[5].Value = detalle.ADG_FECHA.ToString();
                            dt.Cells[6].Value = detalle.ADG_CODIGO.ToString();

                            //lxgrntsx2
                            DataTable rs = NegDietetica.getDataTable("getDetalleGaratias", Convert.ToString(detalle.ADG_CODIGO));
                            dt.Cells[7].Value = rs.Rows[0]["ADG_BANCO"].ToString();
                            dt.Cells[8].Value = rs.Rows[0]["ADG_TIPOTARJETA"].ToString();
                            dt.Cells[9].Value = rs.Rows[0]["ADG_NROTARJETA"].ToString();
                            dt.Cells[10].Value = rs.Rows[0]["ADG_CCV"].ToString();
                            dt.Cells[11].Value = rs.Rows[0]["ADG_DIASVENCIMIENTO"].ToString();
                            dt.Cells[12].Value = rs.Rows[0]["ADG_CADUCIDAD"].ToString();
                            dt.Cells[13].Value = rs.Rows[0]["ADG_LOTE"].ToString();
                            dt.Cells[14].Value = rs.Rows[0]["ADG_AUTORIZACION"].ToString();
                            dt.Cells[15].Value = rs.Rows[0]["ADG_NUMERO_AUT"].ToString();
                            dt.Cells[16].Value = rs.Rows[0]["ADG_PERSONA_AUT"].ToString();
                            if (rs.Rows[0]["ADG_FECHA_AUT"] != null)
                                dt.Cells[17].Value = rs.Rows[0]["ADG_FECHA_AUT"].ToString();
                            dt.Cells[18].Value = rs.Rows[0]["ADG_ESTABLECIMIENTO"].ToString();
                            dt.Cells[19].Value = rs.Rows[0]["UserName"].ToString();
                            dt.Cells[20].Value = rs.Rows[0]["ADG_ESTATUS"].ToString();
                            



                            //dt.ReadOnly = true;
                            gridGarantias.Rows.Add(dt);
                        }
                    }
                    else
                    {
                        gridGarantias.Rows.Clear();
                    }


                    /* OTROS SEGUROS */

                    DataTable dtOtrosSeguros = new DataTable();
                    Int32 Fila = 0;
                    dtOtrosSeguros = NegAtenciones.RecuperaOtrosSeguros(ultimaAtencion.ATE_CODIGO);

                    if (dtOtrosSeguros != null)
                    {
                        dgvOtroSeguro.Rows.Clear();

                        foreach (DataRow Item in dtOtrosSeguros.AsEnumerable())
                        {

                            /* inserto una fila */

                            DataGridViewRow dt = new DataGridViewRow();
                            dt.CreateCells(dgvOtroSeguro);
                            TIPO_GARANTIA aseg = (TIPO_GARANTIA)cb_tipoGarantia.SelectedItem;

                            dt.Cells[0].Value = "";
                            dt.Cells[1].Value = "";
                            dt.Cells[2].Value = "";
                            dt.Cells[3].Value = "";

                            dgvOtroSeguro.Rows.Add(dt);

                            /* agrego informacion */

                            dgvOtroSeguro.Rows[Fila].Cells[0].Value = Item["ani_codigo"].ToString();
                            dgvOtroSeguro.Rows[Fila].Cells[1].Value = Item["ANI_COD_PRO"].ToString();

                            if (Item["ANI_COD_PRO"].ToString() != "OTROS")
                            {
                                dgvOtroSeguro.Rows[Fila].Cells[2].ReadOnly = true;
                            }

                            dgvOtroSeguro.Rows[Fila].Cells[2].Value = Item["DESCRIPCION"].ToString();
                            dgvOtroSeguro.Rows[Fila].Cells[3].Value = Item["CODIGO_VALIDACION"].ToString();

                            Fila++;
                        }
                    }

                    else
                    {
                        dgvOtroSeguro.Rows.Clear();
                    }

                    /*  */

                    /* DERIVADOS */

                    DataTable dtDerivado = new DataTable();
                    Int32 Fila1 = 0;
                    dtDerivado = NegAtenciones.RecuperaDerivado(ultimaAtencion.ATE_CODIGO);

                    if (dtOtrosSeguros != null)
                    {
                        dgvDerivado.Rows.Clear();

                        foreach (DataRow Item in dtDerivado.AsEnumerable())
                        {

                            /* inserto una fila */

                            DataGridViewRow dt = new DataGridViewRow();
                            dt.CreateCells(dgvDerivado);
                            TIPO_GARANTIA aseg = (TIPO_GARANTIA)cb_tipoGarantia.SelectedItem;

                            dt.Cells[0].Value = "";
                            dt.Cells[1].Value = "";
                            dt.Cells[2].Value = "";
                            dt.Cells[3].Value = "";
                            dt.Cells[4].Value = "";
                            dt.Cells[5].Value = "";

                            dgvDerivado.Rows.Add(dt);

                            /* agrego informacion */

                            dgvDerivado.Rows[Fila1].Cells[0].Value = Item["ANI_CODIGO_DERIVACION"].ToString();
                            dgvDerivado.Rows[Fila1].Cells[1].Value = Item["DesDerivacion"].ToString();
                            dgvDerivado.Rows[Fila1].Cells[2].Value = Item["ANI_CODIGO_RED"].ToString();
                            dgvDerivado.Rows[Fila1].Cells[3].Value = Item["DesRed"].ToString();

                            if (Item["DesRed"].ToString() != "OTROS")
                            {
                                dgvDerivado.Rows[Fila1].Cells[5].ReadOnly = true;
                            }

                            dgvDerivado.Rows[Fila1].Cells[4].Value = Item["RUC_RED"].ToString();
                            dgvDerivado.Rows[Fila1].Cells[5].Value = Item["DESCRIPCION_OTROS"].ToString();

                            Fila1++;
                        }
                    }
                    else
                    {
                        dgvDerivado.Rows.Clear();
                    }

                    
                    if (ultimaAtencion.ESC_CODIGO != 7)
                        habilitarCamposAtencion();
                    else
                        deshabilitarCamposAtencion();


                    btnFormularios.Enabled = true;
                    CargarTitularSeguro(ultimaAtencion.ATE_CODIGO);
                    cargarPreAtencion(ultimaAtencion.ATE_CODIGO);
                    CargarAtencionDatosAdicionales(Convert.ToInt32(ultimaAtencion.ATE_NUMERO_ATENCION)); //lx202005
                }
                else
                {
                    limpiarCamposAtencion(0);
                    txt_numeroatencion.Enabled = true;
                    deshabilitarCamposAtencion();
                    detalleCategorias = null;
                    detalleGarantias = null;
                    btnFormularios.Enabled = false;
                }

                DataTable Ginecologia = new DataTable();
                Ginecologia = NegCuentasPacientes.RecuperaGinecologia(txt_historiaclinica.Text);
                foreach (DataRow Item in Ginecologia.AsEnumerable())
                {
                    txtMenarquia.Text = Item["MenarquiaGHC"].ToString();
                    txtCiclos.Text = Item["CiclosGHC"].ToString();
                    dateFUM.Text = Item["FechaUltimaMenstruacion"].ToString();
                    txtG.Text = Item["G"].ToString();
                    txtP.Text = Item["P"].ToString();
                    txtA.Text = Item["A"].ToString();
                    txtC.Text = Item["C"].ToString();
                    txtHV.Text = Item["HV"].ToString();
                    txtGO.Text = Item["G_O"].ToString();
                    txtDIU.Text = Item["DIU"].ToString();
                    txtOM.Text = Item["OM"].ToString();
                    txtCV.Text = Item["CV"].ToString();
                    txtAPP.Text = Item["APP_Alergia"].ToString();
                    txtAPF.Text = Item["APF"].ToString();
                    txtGS.Text = Item["GS"].ToString(); ;
                    txtOperaciones.Text = Item["Operaciones"].ToString();
                    txtRecomendado.Text = Item["Recomendado"].ToString();
                }
                lockcells();//lxgrntsx
            }
            catch (System.Exception x)
            {
                MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void CargarEmpresa(string numRuc)
        {
            empresaPaciente = NegAseguradoras.RecuperarEmpresa(numRuc);
            if (empresaPaciente != null)
            {
                txt_rucEmp.Text = empresaPaciente.ASE_RUC;
                txt_empresa.Text = empresaPaciente.ASE_NOMBRE;
                txt_direcEmp.Text = empresaPaciente.ASE_DIRECCION;
                txt_ciudadEmp.Text = empresaPaciente.ASE_CIUDAD;
                txt_telfEmp.Text = empresaPaciente.ASE_TELEFONO;
            }
        }

        private void CargarMedico(int codMedico)
        {
            medico = NegMedicos.MedicoID(codMedico);

            if (medico != null)
                txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            else
                txtNombreMedico.Text = string.Empty;
        }

        private void CargarMedicoCert(int codMedico)
        {
            medico = NegMedicos.MedicoID(codMedico);

            if (medico != null)
                txt_MedicoCertificado.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            else
                txt_MedicoCertificado.Text = string.Empty;
        }

        private void CargarMedico()
        {
            txtCodMedico.Text = medico.MED_CODIGO.ToString();
            txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
        }


        private void CargarTitularSeguro(int codAtencion)
        {
            adSeguros = NegAtencionDetalleSeguros.RecuAtencionesDetalleSeguros(ultimaAtencion.ATE_CODIGO);
            if (adSeguros != null)
            {
                txt_nombreTitular.Text = adSeguros.ADS_ASEGURADO_NOMBRE;
                txt_DireccionTitular.Text = adSeguros.ADS_ASEGURADO_DIRECCION;
                txt_CedulaTitular.Text = adSeguros.ADS_ASEGURADO_CEDULA;
                txt_TelefonoTitular.Text = adSeguros.ADS_ASEGURADO_TELEFONO.Replace("-", string.Empty);
                txt_CiudadTitular.Text = adSeguros.ADS_ASEGURADO_CIUDAD;
                cmb_Parentesco.SelectedItem = listaAnexos.FirstOrDefault(a => a.ANI_CODIGO == Convert.ToInt32(adSeguros.ADS_ASEGURADO_PARENTESCO));

            }
            else
            {
                txt_nombreTitular.Text = string.Empty;
                txt_DireccionTitular.Text = string.Empty;
                txt_CedulaTitular.Text = string.Empty;                
                txt_TelefonoTitular.Text = string.Empty;
                txt_CiudadTitular.Text = string.Empty;
            }
        }

        # endregion

        #region Eventos sobre la BDD

        private void guardarDatos()
        {
            try
            {

                if (pacienteNuevo == true)
                {
                    #region pacientenuevo
                    //INGRESO PACIENTE

                    pacienteActual = new PACIENTES();
                    datosPacienteActual = new PACIENTES_DATOS_ADICIONALES();
                    ultimaAtencion = new ATENCIONES();


                    pacienteActual.PAC_CODIGO = NegPacientes.ultimoCodigoPacientes() + 1;

                    NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();

                    if (NegNumeroControl.NumerodeControlAutomatico(6))
                    {
                        numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 6).FirstOrDefault();
                        pacienteActual.PAC_HISTORIA_CLINICA = numerocontrol.NUMCON.Trim();
                        NegNumeroControl.LiberaNumeroControl(6);
                    }
                    else
                    {
                        pacienteActual.PAC_HISTORIA_CLINICA = txt_historiaclinica.Text.Trim();
                    }

                    pacienteActual.PAC_FECHA_CREACION = DateTime.Now;
                    if (Convert.ToBoolean(ubtnDatosIncompletos.Tag) == true)
                        pacienteActual.PAC_DATOS_INCOMPLETOS = true;

                    agregarDatosPaciente();

                    NegPacientes.crearPaciente(pacienteActual);

                    //INGRESO DATOS ADICIONALES

                    datosPacienteActual.DAP_CODIGO = NegPacienteDatosAdicionales.ultimoCodigoDatos() + 1;
                    datosPacienteActual.DAP_NUMERO_REGISTRO = (Int16)(NegPacienteDatosAdicionales.ultimoNumeroRegistro(pacienteActual.PAC_CODIGO) + 1);
                    datosPacienteActual.DAP_ESTADO = true;
                    datosPacienteActual.DAP_FECHA_INGRESO = DateTime.Now;

                    agregarDatosAdicionalesPaciente();

                    NegPacienteDatosAdicionales.CrearPacienteDatosAdicionales(datosPacienteActual, pacienteActual.PAC_CODIGO);

                    //DATOS ADICIONALES 2  - empaqueta y guarda lx202005
                    DtoPacienteDatosAdicionales2 datosPaciente123 = new DtoPacienteDatosAdicionales2();
                    datosPaciente123.COD_PACIENTE = pacienteActual.PAC_CODIGO;
                    datosPaciente123.FALLECIDO = false;
                    if (chkFallecido.Checked)
                        datosPaciente123.FALLECIDO = true;
                    datosPaciente123.FEC_FALLECIDO = (dtpFallecido.Value).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    datosPaciente123.REF_TELEFONO_2 = txt_telfRef2.Text.Replace("-", string.Empty).ToString();
                    NegPacienteDatosAdicionales.PDA2_save(datosPaciente123);

                    //INGRESO ATENCION

                    int codigo_atencion_asignado = NegAtenciones.UltimoCodigoAtenciones() + 1;
                    ultimaAtencion.ATE_CODIGO = codigo_atencion_asignado;

                    ultimaAtencion.ATE_NUMERO_ADMISION = NegAtenciones.ultimoNumeroAdmision(pacienteActual.PAC_CODIGO) + 1;
                    ultimaAtencion.ATE_ESTADO = true;
                    ultimaAtencion.ATE_FECHA = DateTime.Now;
                     
                    ultimaAtencion.ESC_CODIGO = 1; // por defecto ingreso

                    if (NegNumeroControl.NumerodeControlAutomatico(8))
                        numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
                    ultimaAtencion.ATE_NUMERO_ATENCION = numerocontrol.NUMCON;
                    agregarDatosAtencion();

                    ocuparHabitacion();

                    NegAtenciones.CrearAtencion(ultimaAtencion);
                    if (cmb_tipoingreso.SelectedIndex == 5)
                    {
                        ultimaAtencion.TIPO_INGRESOReference.EntityKey = ((TIPO_INGRESO)cmb_tipoingreso.SelectedItem).EntityKey;
                        crearPreAtencion(pacienteActual.PAC_CODIGO, ultimaAtencion.ATE_CODIGO);
                    }

                    NegNumeroControl.LiberaNumeroControl(8);


                    agregarDetalleCategorias();

                    AgregarOtroSeguro();
                    AgregarDerivados();

                    agregarDetalleGarantias();
                    crearFormularioAdmision();
                    guardarDatosTitularSeguro();
                    NegCuentasPacientes.GuardaGinecologia(txt_historiaclinica.Text, Convert.ToInt16(txtMenarquia.Text), txtCiclos.Text, Convert.ToDateTime(dateFUM.Text), Convert.ToInt16(txtG.Text),
                       Convert.ToInt16(txtP.Text), Convert.ToInt16(txtA.Text), Convert.ToInt16(txtC.Text), Convert.ToInt16(txtHV.Text), Convert.ToInt16(txtGO.Text), Convert.ToInt16(txtDIU.Text), txtOM.Text,
                       txtCV.Text, txtAPP.Text, txtAPF.Text, txtGS.Text, txt_diagnostico.Text, txtOperaciones.Text, txtRecomendado.Text);

                    //ATENCION_DATOSADICIONALES  - empaqueta y guarda lx202005
                    DtoAtencionDatosAdicionales atenDA = new DtoAtencionDatosAdicionales();
                    atenDA.empresa = txtEmpresaDA.Text;
                    if (txtPorcentageDA.Text.ToString().Trim() != "")
                        atenDA.porcentage_discapacidad = Convert.ToInt32(txtPorcentageDA.Text.ToString().Trim());
                    else
                        atenDA.porcentage_discapacidad = 0;
                    atenDA.observaciones = txtObservacionDA.Text;
                    string[] wordss = cmbTiposDiscapacidadesDA.Text.Split('-');
                    atenDA.tipo_discapacidad = wordss[0].Trim();

                    if (cboPaquete.Text!="Ninguno")
                    {
                        string[] words = cboPaquete.Text.Split('-');
                        atenDA.paquete = words[0].Trim();
                    }
                    else
                        atenDA.paquete = "";

                    
                    atenDA.cod_atencion = Convert.ToInt32(ultimaAtencion.ATE_NUMERO_ATENCION);

                    NegAtenciones.atencionDA_save(atenDA);

                    #endregion
                }
                else
                {
                    #region Editar paciente
                    //EDITAR PACIENTE
                    pacienteActual.PAC_HISTORIA_CLINICA = txt_historiaclinica.Text.Trim();
                    agregarDatosPaciente();
    
                    NegPacientes.EditarPaciente(pacienteActual);

                    if (direccionNueva == true)
                    {
                        //INGRESO DATOS
                        datosPacienteActual = new PACIENTES_DATOS_ADICIONALES();
                        datosPacienteActual.DAP_CODIGO = NegPacienteDatosAdicionales.ultimoCodigoDatos() + 1;
                        datosPacienteActual.DAP_NUMERO_REGISTRO = (Int16)(NegPacienteDatosAdicionales.ultimoNumeroRegistro(pacienteActual.PAC_CODIGO) + 1);
                        datosPacienteActual.DAP_ESTADO = true;
                        datosPacienteActual.DAP_FECHA_INGRESO = DateTime.Now;
                        agregarDatosAdicionalesPaciente();
                        NegPacienteDatosAdicionales.CrearPacienteDatosAdicionales(datosPacienteActual, pacienteActual.PAC_CODIGO);
                    }
                    else
                    {
                        //EDITAR DATOS
                        agregarDatosAdicionalesPaciente();
                        NegPacienteDatosAdicionales.EditarPacienteDatosAdicionales(datosPacienteActual);
                    }

                    //DATOS ADICIONALES 2  - empaqueta y guarda lx202005
                    DtoPacienteDatosAdicionales2 datosPaciente23 = new DtoPacienteDatosAdicionales2();
                    datosPaciente23.COD_PACIENTE = pacienteActual.PAC_CODIGO;
                    datosPaciente23.FALLECIDO = false;
                    if (chkFallecido.Checked)
                        datosPaciente23.FALLECIDO = true;
                    datosPaciente23.FEC_FALLECIDO = (dtpFallecido.Value).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    datosPaciente23.REF_TELEFONO_2 = txt_telfRef2.Text.Replace("-", string.Empty).ToString();
                    NegPacienteDatosAdicionales.PDA2_save(datosPaciente23);
                    ////////////////////////////////////////////////lx


                    if (txt_numeroatencion.Text.Trim() != string.Empty)
                    {
                       


                        if (ultimaAtencion == null || atencionNueva == true)
                        {
                            ultimaAtencion = new ATENCIONES();
                            ultimaAtencion.ATE_CODIGO = NegAtenciones.UltimoCodigoAtenciones() + 1;
                            ultimaAtencion.ATE_NUMERO_ADMISION = NegAtenciones.ultimoNumeroAdmision(pacienteActual.PAC_CODIGO) + 1;
                            ultimaAtencion.ATE_ESTADO = true;
                            ultimaAtencion.ATE_FECHA = DateTime.Now;
                            ultimaAtencion.ESC_CODIGO = 1;

                            NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                            if (NegNumeroControl.NumerodeControlAutomatico(8))
                                numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
                            ultimaAtencion.ATE_NUMERO_ATENCION = numerocontrol.NUMCON;

                            agregarDatosAtencion();
                            if (cmb_tipoingreso.SelectedIndex == 5)
                            {
                                ultimaAtencion.TIPO_INGRESOReference.EntityKey = ((TIPO_INGRESO)cmb_tipoingreso.SelectedItem).EntityKey;
                                crearPreAtencion(pacienteActual.PAC_CODIGO, ultimaAtencion.ATE_CODIGO);
                            }

                            ocuparHabitacion();
                            NegAtenciones.CrearAtencion(ultimaAtencion);
                            NegNumeroControl.LiberaNumeroControl(8);
                            if (chb_PreAdmision.Checked != true)
                            {
                                CUENTAS_PACIENTES_TOTALES cuentaP = new CUENTAS_PACIENTES_TOTALES();
                                cuentaP = NegCuentasPacientes.RecuperarCuentasTotal(ultimaAtencion.ATE_CODIGO);
                                if (cuentaP == null)
                                    His.Admision.Datos.CuentaPaciente.AgregarCuentaPacientesT(ultimaAtencion);
                            }
                            crearFormularioAdmision();
                  
                        }

                        else
                        {
                            agregarDatosAtencion();

                            if (chb_PreAdmision.Checked == true)
                            {
                                if (preAtencioActual != null)
                                {
                                    if (cmb_tipoingreso.SelectedIndex == 5)
                                    {
                                        preAtencioActual.PREA_FECHA_PREADMISON = dtpPreAdmision.Value;
                                        preAtencioActual.PREA_FEC_INGRESO = DateTime.Now;
                                        if (validarFechasPreadmison())
                                            preAtencioActual.PREA_ESTADO = true;
                                        NegPreAtencion.editarPreAtencion(preAtencioActual);
                                    }
                                }
                                else
                                {
                                    cmb_tipoingreso.SelectedItem = 5;
                                    ultimaAtencion.TIPO_INGRESOReference.EntityKey = ((TIPO_INGRESO)cmb_tipoingreso.SelectedItem).EntityKey;
                                    crearPreAtencion(pacienteActual.PAC_CODIGO, ultimaAtencion.ATE_CODIGO);
                                }
                            }
                            else
                            {
                                if (preAtencioActual != null && chb_PreAdmision.Checked == false && txt_habitacion.Text != "PREA")
                                {
                                    ultimaAtencion.TIPO_INGRESOReference.EntityKey = ((TIPO_INGRESO)cmb_tipoingreso.SelectedItem).EntityKey;
                                    ultimaAtencion.ATE_FECHA_INGRESO = DateTime.Now;
                                    preAtencioActual.PREA_FEC_INGRESO = dtpPreAdmision.Value;
                                    preAtencioActual.PREA_ESTADO = false;
                                    NegPreAtencion.editarPreAtencion(preAtencioActual);
                                    preAtencioActual = new PREATENCIONES();
                                    ocuparHabitacion();
                                }
                            }

                            NegAtenciones.EditarAtencionAdmision(ultimaAtencion, 0);
                            if (chb_PreAdmision.Checked != true)
                            {
                                CUENTAS_PACIENTES_TOTALES cuentaP = new CUENTAS_PACIENTES_TOTALES();
                                cuentaP = NegCuentasPacientes.RecuperarCuentasTotal(ultimaAtencion.ATE_CODIGO);
                                if (cuentaP == null)
                                    His.Admision.Datos.CuentaPaciente.AgregarCuentaPacientesT(ultimaAtencion);
                            }
                        }

                        if (gridAseguradoras.RowCount > 0)
                            agregarDetalleCategorias();

                        if (gridGarantias.RowCount > 0)
                            agregarDetalleGarantias();
                        guardarDatosTitularSeguro();

                        /********************OTROS SEGUROS *********************************/

                        if (dgvOtroSeguro.RowCount > 0)
                        {
                            NegAtenciones.EliminaOtrosSeguros(ultimaAtencion.ATE_CODIGO);
                            AgregarOtroSeguro();
                        }
                        else
                        {
                            NegAtenciones.EliminaOtrosSeguros(ultimaAtencion.ATE_CODIGO);
                        }

                        /******************************************************************/

                        /********************  DERIVADO  *********************************/

                        if (dgvDerivado.RowCount > 0)
                        {
                            NegAtenciones.EliminaDerivado(ultimaAtencion.ATE_CODIGO);
                            AgregarDerivados();
                        }
                        else
                        {
                            NegAtenciones.EliminaDerivado(ultimaAtencion.ATE_CODIGO);
                        }

                        /******************************************************************/


                        //ATENCION_DATOSADICIONALES  - empaqueta y guarda lx202005
 


                        DtoAtencionDatosAdicionales ateD = new DtoAtencionDatosAdicionales();
                        ateD.empresa = txtEmpresaDA.Text;
                        if (txtPorcentageDA.Text.ToString().Trim() != "")
                            ateD.porcentage_discapacidad = Convert.ToInt32(txtPorcentageDA.Text.ToString().Trim());
                        else
                            ateD.porcentage_discapacidad = 0;
                        ateD.observaciones = txtObservacionDA.Text;
                        string[] wordss = cmbTiposDiscapacidadesDA.Text.Split('-');
                        ateD.tipo_discapacidad = wordss[0].Trim();

                        if (cboPaquete.Text != "Ninguno")
                        {
                            string[] words = cboPaquete.Text.Split('-');
                            ateD.paquete = words[0].Trim();
                        }
                        else
                            ateD.paquete = "";


                        ateD.cod_atencion = Convert.ToInt32(ultimaAtencion.ATE_NUMERO_ATENCION);

                        NegAtenciones.atencionDA_save(ateD);



                        ////////////
                    }       
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos: \n" + e.InnerException);
            }
        }


        private void agregarDatosPaciente()
        {
            pacienteActual.PAC_NOMBRE1 = txt_nombre1.Text.ToString();
            pacienteActual.PAC_NOMBRE2 = txt_nombre2.Text.ToString();
            pacienteActual.PAC_APELLIDO_MATERNO = txt_apellido2.Text.ToString();
            pacienteActual.PAC_APELLIDO_PATERNO = txt_apellido1.Text.ToString();
            pacienteActual.ETNIAReference.EntityKey = ((ETNIA)cb_etnia.SelectedItem).EntityKey;

            pacienteActual.PAC_FECHA_NACIMIENTO = dtp_fecnac.Value;

            //int codCiudad = Convert.ToInt16(cmb_ciudad.SelectedValue);
            if (cmb_ciudad.SelectedItem != null)
                pacienteActual.DIPO_CODIINEC = ((DIVISION_POLITICA)cmb_ciudad.SelectedItem).DIPO_CODIINEC;
            else
                pacienteActual.DIPO_CODIINEC = "0";

            if (rbn_h.Checked == true)
                pacienteActual.PAC_GENERO = "M";
            else
                pacienteActual.PAC_GENERO = "F";

            pacienteActual.GRUPO_SANGUINEOReference.EntityKey = ((GRUPO_SANGUINEO)cb_gruposanguineo.SelectedItem).EntityKey;
            pacienteActual.PAC_IDENTIFICACION = txt_cedula.Text.ToString();
            pacienteActual.PAC_NACIONALIDAD = txt_nacionalidad.Text.ToString();

            if (rbCedula.Checked == true)
                pacienteActual.PAC_TIPO_IDENTIFICACION = "C";
            else if (rbPasaporte.Checked == true)
                pacienteActual.PAC_TIPO_IDENTIFICACION = "P";
            else if (rbRuc.Checked == true)
                pacienteActual.PAC_TIPO_IDENTIFICACION = "R";
            else
                pacienteActual.PAC_TIPO_IDENTIFICACION = "S";

            pacienteActual.PAC_EMAIL = txt_email.Text.ToString();
            pacienteActual.PAC_ESTADO = true;
            pacienteActual.USUARIOSReference.EntityKey = usuario.EntityKey;
            pacienteActual.PAC_REFERENTE_NOMBRE = txt_nombreRef.Text.ToString();
            pacienteActual.PAC_REFERENTE_PARENTESCO = txt_parentRef.Text.ToString();
            pacienteActual.PAC_REFERENTE_TELEFONO = txt_telfRef.Text.Replace("-", string.Empty).ToString();
            pacienteActual.PAC_REFERENTE_DIRECCION = txtRefDireccion.Text.ToString();

        }

        private void agregarDatosAdicionalesPaciente()
        {
            datosPacienteActual.DAP_DIRECCION_DOMICILIO = txt_direccion.Text.ToString();
            datosPacienteActual.DAP_TELEFONO1 = txt_telefono1.Text.Replace("-", string.Empty).ToString();
            datosPacienteActual.DAP_TELEFONO2 = txt_telefono2.Text.Replace("-", string.Empty).ToString();
            datosPacienteActual.DAP_OCUPACION = txt_ocupacion.Text.ToString();

            if (txt_rucEmp.Text.ToString() != string.Empty)
            {
                if (empresaPaciente == null)
                {
                    empresaPaciente = new ASEGURADORAS_EMPRESAS();
                    empresaPaciente.ASE_CODIGO = (Int16)(NegAseguradoras.UltimoCodigoAseguradoras() + 1);
                    empresaPaciente.ASE_RUC = txt_rucEmp.Text.ToString();
                    empresaPaciente.ASE_NOMBRE = txt_empresa.Text.ToString();
                    empresaPaciente.ASE_DIRECCION = txt_direcEmp.Text.ToString();
                    empresaPaciente.ASE_CIUDAD = txt_ciudadEmp.Text.ToString();
                    empresaPaciente.ASE_TELEFONO = txt_telfEmp.Text.Replace("-", string.Empty).ToString();
                    empresaPaciente.ASE_CONVENIO = false;
                    empresaPaciente.ASE_ESTADO = true;
                    empresaPaciente.TIPO_EMPRESAReference.EntityKey = NegAseguradoras.RecuperaTipoEmpresa().FirstOrDefault(a => a.TE_CODIGO == 2).EntityKey;
                    NegAseguradoras.Crear(empresaPaciente);
                    MessageBox.Show("Se creo una nueva empresa");
                }
                else
                {
                    empresaPaciente.ASE_NOMBRE = txt_empresa.Text.ToString();
                    empresaPaciente.ASE_DIRECCION = txt_direcEmp.Text.ToString();
                    empresaPaciente.ASE_CIUDAD = txt_ciudadEmp.Text.ToString();
                    empresaPaciente.ASE_TELEFONO = txt_telfEmp.Text.Replace("-", string.Empty).ToString();
                    NegAseguradoras.ModificarAseguradora(empresaPaciente);
                }
                datosPacienteActual.EMP_CODIGO = empresaPaciente.ASE_CODIGO;
                aseguradoras = NegAseguradoras.ListaEmpresas();
            }

            datosPacienteActual.DAP_EMP_NOMBRE = txt_empresa.Text.ToString();
            datosPacienteActual.DAP_EMP_DIRECCION = txt_direcEmp.Text.ToString();
            datosPacienteActual.DAP_EMP_CIUDAD = txt_ciudadEmp.Text.ToString();
            datosPacienteActual.DAP_EMP_TELEFONO = txt_telfEmp.Text.Replace("-", string.Empty).ToString();
            datosPacienteActual.DAP_INSTRUCCION = txt_instuccion.Text.ToString();

            if (txt_codPais.Text.ToString() != string.Empty)
                datosPacienteActual.COD_PAIS = txt_codPais.Text.ToString();
            else
                datosPacienteActual.COD_PAIS = null;

            if (txt_codProvincia.Text.ToString() != string.Empty)
                datosPacienteActual.COD_PROVINCIA = txt_codProvincia.Text.ToString();
            else
                datosPacienteActual.COD_PROVINCIA = null;

            if (txt_codCanton.Text.ToString() != string.Empty)
                datosPacienteActual.COD_CANTON = txt_codCanton.Text.ToString();
            else
                datosPacienteActual.COD_CANTON = null;

            if (txt_codParroquia.Text.ToString() != string.Empty)
                datosPacienteActual.COD_PARROQUIA = txt_codParroquia.Text.ToString();
            else
                datosPacienteActual.COD_PARROQUIA = null;

            if (txt_codBarrio.Text.ToString() != string.Empty)
                datosPacienteActual.COD_SECTOR = txt_codBarrio.Text.ToString();
            else
                datosPacienteActual.COD_SECTOR = null;

            //int codigoEstadoCivil = Convert.ToInt32(cmb_estadocivil.SelectedValue);
            datosPacienteActual.ESTADO_CIVILReference.EntityKey = ((ESTADO_CIVIL)cmb_estadocivil.SelectedItem).EntityKey;

            //nt codigoTipoCiudadano = Convert.ToInt32(cmb_ciudadano.SelectedValue);
            datosPacienteActual.TIPO_CIUDADANOReference.EntityKey = ((TIPO_CIUDADANO)cmb_ciudadano.SelectedItem).EntityKey;

            datosPacienteActual.PACIENTESReference.EntityKey = pacienteActual.EntityKey;
            datosPacienteActual.USUARIOSReference.EntityKey = usuario.EntityKey;
        }

        private void agregarDatosAtencion()
        {

            ultimaAtencion.ATE_FECHA_INGRESO = dateTimeFecIngreso.Value;

            /*discapacidad*/

            if (this.chkDiscapacidad.Checked == true) //lx202005
            {
                ultimaAtencion.ate_discapacidad = true;
            }
            else
            {
                ultimaAtencion.ate_discapacidad = false;
            }

            if (this.chkDiscapacidad.Checked == true)
            {
                if (txtIdDiscapacidad.Text != "")
                    ultimaAtencion.ate_carnet_conadis = txtIdDiscapacidad.Text;
            }

            /**/

            ultimaAtencion.ATE_ID_ACCIDENTE = Convert.ToInt32(this.cmbAccidente.SelectedIndex);

            ultimaAtencion.ATE_EDAD_PACIENTE = Convert.ToInt16(DateTime.Now.Year - pacienteActual.PAC_FECHA_CREACION.Year);
            ultimaAtencion.ATE_ACOMPANANTE_CEDULA = txt_cedulaAcomp.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_CIUDAD = txt_ciudadAcomp.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_DIRECCION = txt_direccionAcomp.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_NOMBRE = txt_nombreAcomp.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_PARENTESCO = txt_parentescoAcomp.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_TELEFONO = txt_telefonoAcomp.Text.Replace("-", string.Empty).ToString();
            ultimaAtencion.ATE_DIAGNOSTICO_INICIAL = txt_diagnostico.Text.ToString();
            ultimaAtencion.ATE_GARANTE_CEDULA = txt_cedulaGar.Text.ToString();
            ultimaAtencion.ATE_GARANTE_CIUDAD = txt_ciudadGar.Text.ToString();
            ultimaAtencion.ATE_GARANTE_DIRECCION = txt_dirGar.Text.ToString();
            ultimaAtencion.TipoAtencion = (cmbTipoAtencion.Text).Substring(0,3).Trim(); //lx202005


            if (txt_montoGar.Text != string.Empty)
                ultimaAtencion.ATE_GARANTE_MONTO_GARANTIA = Convert.ToDecimal(txt_montoGar.Text.ToString());

            ultimaAtencion.ATE_GARANTE_NOMBRE = txt_nombreGar.Text.ToString();
            ultimaAtencion.ATE_GARANTE_PARENTESCO = txt_parentGar.Text.ToString();
            ultimaAtencion.ATE_GARANTE_TELEFONO = txt_telfGar.Text.Replace("-", string.Empty).ToString();
            ultimaAtencion.ATE_OBSERVACIONES = txt_observaciones.Text.ToString();

            int codTipoReferido = Convert.ToInt16(cmb_tiporeferido.SelectedValue);
            ultimaAtencion.TIPO_REFERIDOReference.EntityKey = tipoReferido.FirstOrDefault(t => t.TIR_CODIGO == codTipoReferido).EntityKey;

            if (codTipoReferido == 1)
                ultimaAtencion.ATE_REFERIDO = true;
            else
                ultimaAtencion.ATE_REFERIDO = false;

            int codFormaLlegada = Convert.ToInt16(cmb_formaLlegada.SelectedValue);
            ultimaAtencion.ATENCION_FORMAS_LLEGADAReference.EntityKey = formasLlegada.FirstOrDefault(f => f.AFL_CODIGO == codFormaLlegada).EntityKey;
            ultimaAtencion.MEDICOSReference.EntityKey = medico.EntityKey;
            ultimaAtencion.CAJASReference.EntityKey = NegCajas.ListaCajas().FirstOrDefault(c => c.CAJ_CODIGO == 1).EntityKey;
            ultimaAtencion.TIPO_INGRESOReference.EntityKey = ((TIPO_INGRESO)cmb_tipoingreso.SelectedItem).EntityKey;
            ultimaAtencion.PACIENTESReference.EntityKey = pacienteActual.EntityKey;
            ultimaAtencion.PACIENTES_DATOS_ADICIONALESReference.EntityKey = datosPacienteActual.EntityKey;

            int codTipoAtencion = Convert.ToInt16(cmb_tipoatencion.SelectedValue);
            ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey = tipoTratamiento.FirstOrDefault(t => t.TIA_CODIGO == codTipoAtencion).EntityKey;
            ultimaAtencion.USUARIOSReference.EntityKey = usuario.EntityKey;
            ultimaAtencion.ATE_FACTURA_NOMBRE = cb_personaFactura.SelectedItem.ToString();
            ultimaAtencion.TIF_OBSERVACION = txt_obPago.Text.ToString();
        }



        private void crearPreAtencion(int codPaciente, int codAtencion)
        {
            try
            {
                int codigoPreA = NegPreAtencion.recuperaMaximoPreAtencion();
                preAtencioActual = new PREATENCIONES();
                preAtencioActual.PREA_CODIGO = codigoPreA + 1;
                preAtencioActual.PREA_COD_PACIENTE = codPaciente;
                preAtencioActual.PREA_COD_ATENCION = codAtencion;
                preAtencioActual.PREA_FECHA_ADMISON = ultimaAtencion.ATE_FECHA;
                preAtencioActual.PREA_FECHA_PREADMISON = dtpPreAdmision.Value;
                preAtencioActual.PREA_FEC_INGRESO = ultimaAtencion.ATE_FECHA;
                preAtencioActual.PREA_ESTADO = true;
                NegPreAtencion.crearPreAtencion(preAtencioActual);
            }
            catch (System.Exception err) { MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cargarPreAtencion(int codAtencion)
        {
            try
            {
                preAtencioActual = new PREATENCIONES();
                preAtencioActual = NegPreAtencion.recuperarPreAtencion(ultimaAtencion.ATE_CODIGO);
                if (preAtencioActual != null)
                {
                    if (preAtencioActual.PREA_ESTADO == true)
                    {
                        cmb_tipoingreso.SelectedIndex = 5;
                        dtpPreAdmision.Value = Convert.ToDateTime(preAtencioActual.PREA_FECHA_PREADMISON);
                    }
                }
            }
            catch (System.Exception err) { MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ocuparHabitacion()
        {
            if (cmb_tipoingreso.SelectedIndex == 0)
            {
                HABITACIONES hab = NegHabitaciones.RecuperarHabitacionId((Int16)Sesion.codHabitacion);
                hab.HABITACIONES_ESTADOReference.EntityKey = NegHabitaciones.RecuperarEstadoHabitacion(AdmisionParametros.getEstadoHabitacionOcupado()).EntityKey;
                NegHabitaciones.CambiarEstadoHabitacion(hab);
                ultimaAtencion.HABITACIONESReference.EntityKey = hab.EntityKey;
                HABITACIONES_DETALLE habDetalle = new HABITACIONES_DETALLE();
                habDetalle.HAD_CODIGO = NegHabitaciones.RecuperaMaximoDetalleHabitacion() + 1;
                habDetalle.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                habDetalle.HABITACIONESReference.EntityKey = hab.EntityKey;
                habDetalle.ID_USUARIO = Sesion.codUsuario;
                habDetalle.HAD_FECHA_INGRESO = DateTime.Now;
                NegHabitaciones.CrearHabitacionDetalle(habDetalle);

            }
            else
            {
                if (cmb_tipoingreso.SelectedIndex == 5)
                {
                    HABITACIONES hab = NegHabitaciones.RecuperarHabitacionId(AdmisionParametros.CodigoPreHabitacion);
                    ultimaAtencion.HABITACIONESReference.EntityKey = hab.EntityKey;
                }
                else
                {
                    HABITACIONES hab = NegHabitaciones.RecuperarHabitacionId((Int16)Sesion.codHabitacion);
                    hab.HABITACIONES_ESTADOReference.EntityKey = NegHabitaciones.RecuperarEstadoHabitacion(AdmisionParametros.getEstadoHabitacionOcupado()).EntityKey;
                    NegHabitaciones.CambiarEstadoHabitacion(hab);
                    ultimaAtencion.HABITACIONESReference.EntityKey = hab.EntityKey;
                    HABITACIONES_DETALLE habDetalle = new HABITACIONES_DETALLE();
                    habDetalle.HAD_CODIGO = NegHabitaciones.RecuperaMaximoDetalleHabitacion() + 1;
                    habDetalle.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                    habDetalle.HABITACIONESReference.EntityKey = hab.EntityKey;
                    habDetalle.ID_USUARIO = Sesion.codUsuario;
                    habDetalle.HAD_FECHA_INGRESO = DateTime.Now;
                    NegHabitaciones.CrearHabitacionDetalle(habDetalle);
                }                          
            }
        }

        private void AgregarOtroSeguro()
        {
            for (Int16 i = 0; i < dgvOtroSeguro.RowCount; i++)
            {
                DataGridViewRow fila = dgvOtroSeguro.Rows[i];
                if (fila.Cells[0].Value != null)
                {
                    NegAtenciones.OtrosSeguros(Convert.ToInt64(ultimaAtencion.ATE_CODIGO), Convert.ToInt32(fila.Cells[0].Value), fila.Cells[2].Value.ToString(), fila.Cells[3].Value.ToString());
                }

            }
        }

        private void AgregarDerivados()
        {
            for (Int16 i = 0; i < dgvDerivado.RowCount; i++)
            {
                DataGridViewRow fila = dgvDerivado.Rows[i];
                if (fila.Cells[0].Value != null)
                {
                    NegAtenciones.AgregarDerivado(Convert.ToInt64(ultimaAtencion.ATE_CODIGO), Convert.ToInt32(fila.Cells[0].Value), Convert.ToInt32(fila.Cells[2].Value.ToString()), fila.Cells[4].Value.ToString(), fila.Cells[5].Value.ToString());
                }

            }
        }

        private void agregarDetalleCategorias()
        {
            try
            {
                bool fechainicio = false;
                //int orden = gridAseguradoras.RowCount;
                NegAtencionDetalleCategorias.EliminaAtencion_Detalle_Categorias(txt_numeroatencion.Text);
                for (Int16 i = 0; i < gridAseguradoras.RowCount; i++)
                {
                    DataGridViewRow fila = gridAseguradoras.Rows[i];

                    ATENCION_DETALLE_CATEGORIAS nuevoDetalle = new ATENCION_DETALLE_CATEGORIAS();
                    //if (fila.Cells["ADA_CODIGO"].Value == null)
                    //{
                    nuevoDetalle.ADA_CODIGO = NegAtencionDetalleCategorias.ultimoCodigoDetalleCategorias_sp() + 1;
                    //nuevoDetalle.ADA_CODIGO = NegAtencionDetalleCategorias.ultimoCodigoDetalleCategorias_sp() + 1;    

                    nuevoDetalle.ATENCIONESReference.EntityKey = ultimaAtencion.EntityKey;
                    Int16 codCategoria = Convert.ToInt16(fila.Cells["codCategoria"].Value.ToString());
                    CATEGORIAS_CONVENIOS cat = NegCategorias.RecuperaCategoriaID(codCategoria);
                    nuevoDetalle.CATEGORIAS_CONVENIOSReference.EntityKey = cat.EntityKey;
                    //nuevoDetalle.CATEGORIAS_CONVENIOSReference.EntityKey = NegCategorias.RecuperaCategoriaID(codCategoria).EntityKey;
                    if (fila.Cells["autorizacion"].Value != null)
                        nuevoDetalle.ADA_AUTORIZACION = fila.Cells["autorizacion"].Value.ToString();
                    if (fila.Cells["Contrato"].Value != null)
                        nuevoDetalle.ADA_CONTRATO = fila.Cells["Contrato"].Value.ToString();
                    if (fila.Cells["Monto"].Value != null)
                        if (fila.Cells["Monto"].Value.ToString() != string.Empty)
                            nuevoDetalle.ADA_MONTO_COBERTURA = Convert.ToDecimal(fila.Cells["Monto"].Value.ToString());
                    if (fila.Cells["tipoSeguroCodigo"].Value != null)
                        if (fila.Cells["tipoSeguroCodigo"].Value.ToString() != string.Empty)
                            nuevoDetalle.HCC_CODIGO_TS = Convert.ToInt32(fila.Cells["tipoSeguroCodigo"].Value.ToString());
                    if (fila.Cells["dependenciaCodigo"].Value != null)
                        if (fila.Cells["dependenciaCodigo"].Value.ToString() != string.Empty)
                            nuevoDetalle.HCC_CODIGO_DE = Convert.ToInt32(fila.Cells["dependenciaCodigo"].Value.ToString());

                    if (fila.Cells["CodigoEspecialidad"].Value != null)
                        if (fila.Cells["CodigoEspecialidad"].Value.ToString() != string.Empty)
                            nuevoDetalle.HCC_CODIGO_ES = Convert.ToInt32(fila.Cells["CodigoEspecialidad"].Value.ToString());

                    nuevoDetalle.ADA_ORDEN = i + 1;
                    //orden += 1;
                    nuevoDetalle.ADA_ESTADO = false;
                    if (fechainicio == false)
                    {
                        nuevoDetalle.ADA_ESTADO = true;
                        nuevoDetalle.ADA_FECHA_INICIO = DateTime.Today;
                        fechainicio = true;
                    }
                    NegAtencionDetalleCategorias.CrearDetalleCategorias(nuevoDetalle);
                    //}
                    //else
                    //{

                    //    int adaCodigo = Convert.ToInt32(fila.Cells["ADA_CODIGO"].Value);
                    //    nuevoDetalle = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasID(adaCodigo);
                    //    if (fila.Cells["autorizacion"].Value != null)
                    //        nuevoDetalle.ADA_AUTORIZACION = fila.Cells["autorizacion"].Value.ToString();
                    //    else
                    //        nuevoDetalle.ADA_AUTORIZACION = null;
                    //    if (fila.Cells["Contrato"].Value != null)
                    //        nuevoDetalle.ADA_CONTRATO = fila.Cells["Contrato"].Value.ToString();
                    //    else
                    //        nuevoDetalle.ADA_CONTRATO = null;
                    //    if (fila.Cells["Monto"].Value != null)
                    //        if (fila.Cells["Monto"].Value.ToString() != string.Empty)
                    //            nuevoDetalle.ADA_MONTO_COBERTURA = Convert.ToDecimal(fila.Cells["Monto"].Value.ToString());
                    //        else
                    //            nuevoDetalle.ADA_MONTO_COBERTURA = null;
                    //    else
                    //        nuevoDetalle.ADA_MONTO_COBERTURA = null;
                    //    if (fila.Cells["tipoSeguroCodigo"].Value != null)
                    //    {
                    //        if (fila.Cells["tipoSeguroCodigo"].Value.ToString() != string.Empty)
                    //            nuevoDetalle.HCC_CODIGO_TS = Convert.ToInt32(fila.Cells["tipoSeguroCodigo"].Value.ToString());
                    //        else
                    //            nuevoDetalle.HCC_CODIGO_TS = null;
                    //    }
                    //    if (fila.Cells["dependenciaCodigo"].Value != null)
                    //    {
                    //        if (fila.Cells["dependenciaCodigo"].Value.ToString() != string.Empty)

                    //            nuevoDetalle.HCC_CODIGO_DE = Convert.ToInt32(fila.Cells["dependenciaCodigo"].Value.ToString());
                    //        else
                    //            nuevoDetalle.HCC_CODIGO_DE = null;
                    //    }

                    //    if (fila.Cells["CodigoEspecialidad"].Value != null)
                    //    {
                    //        if (fila.Cells["CodigoEspecialidad"].Value.ToString() != string.Empty)
                    //        {
                    //            nuevoDetalle.HCC_CODIGO_ES = Convert.ToInt32(fila.Cells["CodigoEspecialidad"].Value.ToString());
                    //        }
                    //    }

                    //    NegAtencionDetalleCategorias.editarDetalleCategoria(nuevoDetalle);
                    //}
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Error en el ingreso de convenios: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de convenios: \n" + e.InnerException.Message);
            }
        }

        private void agregarDetalleGarantias()
        {
            try
            { ///agregar garatias para guardar
                for (int i = 0; i < gridGarantias.Rows.Count; i++)
                {
                    DataGridViewRow fila = gridGarantias.Rows[i];
                    ATENCION_DETALLE_GARANTIAS nuevoDetalle = new ATENCION_DETALLE_GARANTIAS();
                    int xcodigo;//lxgrntsx
                    bool nuevo;//lxgrntsx2

                    if (fila.Cells["ADG_CODIGO"].Value == null)
                    {
                        nuevoDetalle.ADG_CODIGO = NegAtencionDetalleGarantias.ultimoCodigoDetalleGarantias() + 1;
                        xcodigo = (nuevoDetalle.ADG_CODIGO);  //lxgrntsx
                        nuevo = true;//lxgrntsx2
                        nuevoDetalle.ATENCIONESReference.EntityKey = ultimaAtencion.EntityKey;
                        int codGarantia = Convert.ToInt16(fila.Cells["codGarantia"].Value.ToString());
                        nuevoDetalle.TIPO_GARANTIAReference.EntityKey = tipoGarantia.FirstOrDefault(c => c.TG_CODIGO == codGarantia).EntityKey;

                        if (fila.Cells["descripcion"].Value != null)
                            nuevoDetalle.ADG_DESCRIPCION = fila.Cells["descripcion"].Value.ToString();

                        if (fila.Cells["numdocumento"].Value != null)
                            nuevoDetalle.ADG_DOCUMENTO = fila.Cells["numdocumento"].Value.ToString();

                        if (fila.Cells["valorGar"].Value != null)
                            if (fila.Cells["valorGar"].Value.ToString() != string.Empty)
                                nuevoDetalle.ADG_VALOR = Convert.ToDecimal(fila.Cells["valorGar"].Value.ToString());
                            else
                                nuevoDetalle.ADG_VALOR = 0;
                        else
                            nuevoDetalle.ADG_VALOR = 0;

                        nuevoDetalle.ADG_ESTADO = true;
                        if (fila.Cells["fecha"].Value != null)
                            nuevoDetalle.ADG_FECHA = Convert.ToDateTime(fila.Cells["fecha"].Value.ToString());
                        else
                            nuevoDetalle.ADG_FECHA = System.DateTime.Now;

                        NegAtencionDetalleGarantias.CrearDetalleGarantias(nuevoDetalle);
                    }
                    else
                    {
                       
                        int codDetalle = Convert.ToInt32(fila.Cells["ADG_CODIGO"].Value);
                        xcodigo = codDetalle;//lxgrntsx
                        nuevo = false;//lxgrntsx2
                        nuevoDetalle = NegAtencionDetalleGarantias.RecuperarDetalleGarantiasID(codDetalle);
                        if (fila.Cells["descripcion"].Value != null)
                            nuevoDetalle.ADG_DESCRIPCION = fila.Cells["descripcion"].Value.ToString();
                        else
                            nuevoDetalle.ADG_DESCRIPCION = null;

                        if (fila.Cells["numdocumento"].Value != null)
                            nuevoDetalle.ADG_DOCUMENTO = fila.Cells["numdocumento"].Value.ToString();
                        else
                            nuevoDetalle.ADG_DOCUMENTO = null;

                        if (fila.Cells["valorGar"].Value != null)
                            if (fila.Cells["valorGar"].Value.ToString() != string.Empty)
                                nuevoDetalle.ADG_VALOR = Convert.ToDecimal(fila.Cells["valorGar"].Value.ToString());
                            else
                                nuevoDetalle.ADG_VALOR = 0;
                        else
                            nuevoDetalle.ADG_VALOR = 0;

                        NegAtencionDetalleGarantias.editarDetalleGarantia(nuevoDetalle);

                    }

                    //lxgrntsx2 
                    string xfecha ="";
                    if (gridGarantias.Rows[i].Cells["ADG_FECHA_AUT"].Value != null)
                    {
                        xfecha = Convert.ToDateTime(fila.Cells["ADG_FECHA_AUT"].Value).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    }
                    object[] garantiaDA= new object[]{ 
                    fila.Cells["ADG_BANCO"].Value, fila.Cells["ADG_TIPOTARJETA"].Value,
                    fila.Cells["ADG_CCV"].Value, fila.Cells["ADG_DIASVENCIMIENTO"].Value,
                    fila.Cells["ADG_CADUCIDAD"].Value, fila.Cells["ADG_LOTE"].Value,
                    fila.Cells["ADG_AUTORIZACION"].Value, fila.Cells["ADG_NUMERO_AUT"].Value,
                    fila.Cells["ADG_PERSONA_AUT"].Value, xfecha, fila.Cells["ADG_ESTABLECIMIENTO"].Value,
                    fila.Cells["ADG_NROTARJETA"].Value, Sesion.codUsuario.ToString(), nuevo};

                    NegDietetica.setROW("setDetalleGarantia", garantiaDA, Convert.ToString(xcodigo));


                }
                lockcells();//lxgrntsx
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message);
            }
        }
        /// <summary>
        /// Método para Guardar los datos de el Titular del Seguro
        /// </summary>
        /// <param name="accion"></param>

        private void guardarDatosTitularSeguro()
        {
            if (txt_nombreTitular.Text.Trim() != "" && txt_CedulaTitular.Text.Trim() != "")
            {
                ATENCIONES_DETALLE_SEGUROS adTitularSeguro = new ATENCIONES_DETALLE_SEGUROS();
                adTitularSeguro = NegAtencionDetalleSeguros.RecuAtencionesDetalleSeguros(ultimaAtencion.ATE_CODIGO);
                if (adTitularSeguro != null)
                {
                    adTitularSeguro.ADS_ASEGURADO_NOMBRE = txt_nombreTitular.Text;
                    adTitularSeguro.ADS_ASEGURADO_CEDULA = txt_CedulaTitular.Text;
                    if (cmb_Parentesco.SelectedIndex == -1)
                        cmb_Parentesco.SelectedIndex = 3;
                    adTitularSeguro.ADS_ASEGURADO_PARENTESCO = cmb_Parentesco.SelectedValue.ToString();
                    adTitularSeguro.ADS_ASEGURADO_TELEFONO = txt_TelefonoTitular.Text.Replace("-", string.Empty).ToString();
                    adTitularSeguro.ADS_ASEGURADO_CIUDAD = txt_CiudadTitular.Text;
                    adTitularSeguro.ADS_ASEGURADO_DIRECCION = txt_DireccionTitular.Text;
                    NegAtencionDetalleSeguros.editarDetalleSeguro(adTitularSeguro);
                    chkDatosPac.Checked = false;
                    chkDatosAcSeg.Checked = false;
                }
                else
                {
                    int codigoS = NegAtencionDetalleSeguros.ultimoCodigoDetalleCategorias();
                    codigoS++;
                    adTitularSeguro = new ATENCIONES_DETALLE_SEGUROS();
                    adTitularSeguro.ADS_CODIGO = codigoS;
                    adTitularSeguro.ATENCIONESReference.EntityKey = ultimaAtencion.EntityKey;
                    adTitularSeguro.ADS_ASEGURADO_NOMBRE = txt_nombreTitular.Text;
                    adTitularSeguro.ADS_ASEGURADO_CEDULA = txt_CedulaTitular.Text;
                    adTitularSeguro.ADS_ASEGURADO_PARENTESCO = cmb_Parentesco.SelectedValue.ToString();
                    adTitularSeguro.ADS_ASEGURADO_TELEFONO = txt_TelefonoTitular.Text.Replace("-", string.Empty).ToString();
                    adTitularSeguro.ADS_ASEGURADO_CIUDAD = txt_CiudadTitular.Text;
                    adTitularSeguro.ADS_ASEGURADO_DIRECCION = txt_DireccionTitular.Text;
                    NegAtencionDetalleSeguros.CrearDetalleCategorias(adTitularSeguro);
                    chkDatosPac.Checked = false;
                    chkDatosAcSeg.Checked = false;
                }
            }
            else
            {
                ATENCIONES_DETALLE_SEGUROS adTitularSeguro = new ATENCIONES_DETALLE_SEGUROS();
                adTitularSeguro = NegAtencionDetalleSeguros.RecuAtencionesDetalleSeguros(ultimaAtencion.ATE_CODIGO);
                if (adTitularSeguro != null)
                {
                    NegAtencionDetalleSeguros.eliminarAtencionDetalleSeguro(ultimaAtencion.ATE_CODIGO);
                    txt_nombreTitular.Text = string.Empty;
                    txt_CedulaTitular.Text = string.Empty; ;
                    txt_TelefonoTitular.Text = string.Empty; ;
                    txt_CiudadTitular.Text = string.Empty;
                    txt_DireccionTitular.Text = string.Empty;
                }
            }
        }

        private void crearFormularioAdmision()
        {
            ATENCION_DETALLE_FORMULARIOS_HCU nuevoFormulario = new ATENCION_DETALLE_FORMULARIOS_HCU();
            nuevoFormulario.ADF_CODIGO = NegAtencionDetalleFormulariosHCU.MaxCodigo() + 1;
            nuevoFormulario.ADF_FECHA_INGRESO = DateTime.Now;
            nuevoFormulario.ADF_ESTADO = true;
            nuevoFormulario.ATENCIONESReference.EntityKey = ultimaAtencion.EntityKey;
            nuevoFormulario.FORMULARIOS_HCUReference.EntityKey = NegFormulariosHCU.RecuperarFormularioID((Int32)AdmisionParametros.getHcAdmisionAltaEgreso()).EntityKey;
            nuevoFormulario.ID_USUARIO = Sesion.codUsuario;
            NegAtencionDetalleFormulariosHCU.Crear(nuevoFormulario);
        }

        #endregion

        # region Eventos sobre los botones

        //Botones de Paciente

        private void pacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pacienteNuevo = true;

            pacienteActual = null;
            datosPacienteActual = null;
            ultimaAtencion = null;

            btnGuardar.Enabled = true;
            ubtnDatosIncompletos.Enabled = true;
            habilitarAyudas();
            btnCancelar.Enabled = true;
            btnImprimir.Enabled = false;
            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;
            btnNewAtencion.Enabled = false;

            panelBotonesDir.Enabled = false;

            limpiarCamposPaciente();
            limpiarCamposDireccion();
            limpiarCamposAtencion(0);
            limpiarCamposPreAtencion();
            fijarDatos();

            NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
            numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 6).FirstOrDefault();
           
            if (numerocontrol.TIPCON == "A")
            {
                txt_historiaclinica.Text = numerocontrol.NUMCON.ToString();
                txt_historiaclinica.ReadOnly = true;
            }
            else
            {
                //txt_historiaclinica.Text = numerocontrol.NUMCON.ToString();
                txt_historiaclinica.ReadOnly = false;
            }

            txt_numeroatencion.Enabled = false;
            DataTable ATE_NUMERO_CONTROL = new DataTable();
            ATE_NUMERO_CONTROL = NegNumeroControl.RecuperaNumeroControlPablo();
            //numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
            //int ATE_NUMERO_CONTROL = Convert.ToInt16(numerocontrol.NUMCON.ToString()) + 1;
            txt_numeroatencion.Text = ATE_NUMERO_CONTROL.Rows[0][0].ToString();
            cmb_pais.SelectedItem = paises.FirstOrDefault(p => p.DIPO_CODIINEC == AdmisionParametros.DefPais);
            cb_etnia.SelectedItem = etnias.FirstOrDefault(et => et.E_CODIGO == AdmisionParametros.CodEtniaDefault);
            cmb_estadocivil.SelectedItem = estadoCivil.FirstOrDefault(es => es.ESC_CODIGO == AdmisionParametros.CodEstadoCivilDefault);
            //cmbConvenioPago.SelectedItem = tipoPago.FirstOrDefault(tp => tp.TIF_CODIGO == AdmisionParametros.CodTipoPagoDefault);
            dtp_feCreacion.Text = DateTime.Now.ToString();
            dateTimeFecIngreso.Value = DateTime.Now;

            habilitarCamposPaciente();
            habilitarCamposDireccion();
            habilitarCamposAtencion();

            habilitarCamposPreAtencion();

        }

        private void btnNewAtencion_Click(object sender, EventArgs e)
        {
            atencionNueva = true;
            btnImprimir.Visible = true;
            preAtencioActual = new PREATENCIONES();
            convenioA = true;
            btnGuardar.Enabled = true;
            ubtnDatosIncompletos.Enabled = true;
            habilitarAyudas();
            btnCancelar.Enabled = true;
            btnImprimir.Enabled = false;
            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;
            btnNewAtencion.Enabled = false;

            panelBotonesDir.Enabled = false;

            limpiarCamposAtencion(0);
            fijarDatos();

            txt_numeroatencion.Enabled = false;

            NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
            numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
            int ATE_NUMERO_ATENCION = Convert.ToInt16(numerocontrol.NUMCON.ToString())+1;
            txt_numeroatencion.Text = Convert.ToString(ATE_NUMERO_ATENCION);            
            dateTimeFecIngreso.Value = DateTime.Now;
             habilitarCamposAtencion();

            tabulador.SelectedTab = tabulador.Tabs["atencion"];
            limpiarCamposPreAtencion();
            habilitarCamposPreAtencion();




        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            erroresPaciente.Clear();
            pacienteNuevo = false;
            direccionNueva = false;
            atencionNueva = false;
            txt_numeroatencion.Text = string.Empty;

            if (txt_historiaclinica.Text.Trim() != string.Empty)
                txt_historiaclinica.Text = string.Empty;
            else
                CargarPaciente(txt_historiaclinica.Text,0);
            txt_historiaclinica.ReadOnly = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            pacienteNuevo = false;

            btnGuardar.Enabled = true;
            habilitarAyudas();
            btnCancelar.Enabled = true;
            btnImprimir.Enabled = true;
            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;

            panelBotonesDir.Enabled = true;
            
            habilitarCamposPaciente();
            habilitarCamposDireccion();

            if (txt_numeroatencion.Text != string.Empty)
                habilitarCamposAtencion();

            if (txt_codPais.Text != string.Empty)
                txt_codPais.ReadOnly = false;
            if (txt_codProvincia.Text != string.Empty)
                txt_codProvincia.ReadOnly = false;
            if (txt_codCanton.Text != string.Empty)
                txt_codCanton.ReadOnly = false;
            if (txt_codParroquia.Text != string.Empty)
                txt_codParroquia.ReadOnly = false;
            if (txt_codBarrio.Text != string.Empty)
                txt_codBarrio.ReadOnly = false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            txt_historiaclinica.Focus();
            if (validarFormulario() == true)
            {
                guardarDatos();

                btnNuevo.Enabled = true;
                btnImprimir.Enabled = true;
                btnGuardar.Enabled = false;
                ubtnDatosIncompletos.Enabled = false;
                ubtnDatosIncompletos.Tag = false;
                ubtnDatosIncompletos.Appearance.BackColor = Color.LightBlue;
                ubtnDatosIncompletos.Appearance.BackColor2 = Color.SteelBlue;
                deshabilitarAyudas();
                btnCancelar.Enabled = false;
                btnActualizar.Enabled = true;
                panelBotonesDir.Enabled = true;
                direccionNueva = false;
                pacienteNuevo = false;
                atencionNueva = false;

                CargarDatosAdicionales2(pacienteActual.PAC_CODIGO); //lx202005
                CargarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);
                CargarAtencionesPaciente(pacienteActual.PAC_CODIGO);
                CargarAtencion();
                


                deshabilitarCamposPaciente();
                deshabilitarCamposDireccion();
                deshabilitarCamposAtencion();
                txt_historiaclinica.ReadOnly = true;
                convenioA = false;
                MessageBox.Show("Datos Almacenados Correctamente");
                NegValidaciones.alzheimer();
            }
            else
            {
                MessageBox.Show("Informacion Incompleta");
            }
        }
        public void CrearCarpetas_Srvidor(string modo_formulario)
        {

            try
            {
                if (ultimaAtencion != null)
                {
                    string nombreContrato;
                    string numPoliza = string.Empty;
                    string nomAseg = string.Empty;
                    string montoAseg = string.Empty;
                    string telfAseg = string.Empty;
                    string numContrato = "-";
                    string nomEmp = string.Empty;
                    string montoEmp = string.Empty;
                    string telfEmp = string.Empty;

                    if (ultimaAtencion.ATE_FACTURA_NOMBRE == "ACOMPAÑANTE")
                        nombreContrato = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                    else if (ultimaAtencion.ATE_FACTURA_NOMBRE == "GARANTE")
                        nombreContrato = ultimaAtencion.ATE_GARANTE_NOMBRE;
                    else if (ultimaAtencion.ATE_FACTURA_NOMBRE == "EMPRESA")
                    {
                        if (empresaPaciente != null)
                        {
                            nombreContrato = empresaPaciente.ASE_NOMBRE;
                        }
                        else
                        {
                            nombreContrato = datosPacienteActual.DAP_EMP_NOMBRE;
                        }
                    }
                    else
                        nombreContrato = pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO + " " + pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2;

                    if (detalleCategorias != null)
                    {
                        int codDetalle = Convert.ToInt32(gridAseguradoras.Rows[0].Cells["ADA_CODIGO"].Value);
                        ATENCION_DETALLE_CATEGORIAS dcat = detalleCategorias.FirstOrDefault(d => d.ADA_CODIGO == codDetalle);
                        CATEGORIAS_CONVENIOS cat = categorias.FirstOrDefault(c => c.EntityKey == dcat.CATEGORIAS_CONVENIOSReference.EntityKey);
                        ASEGURADORAS_EMPRESAS aseg = new ASEGURADORAS_EMPRESAS();
                        if (cat != null)
                            aseg = aseguradoras.FirstOrDefault(a => a.EntityKey == cat.ASEGURADORAS_EMPRESASReference.EntityKey);
                        //else

                        TIPO_EMPRESA tipo = new TIPO_EMPRESA();
                        if (aseg.ASE_CODIGO > 0)
                            tipoEmpresa.FirstOrDefault(t => t.EntityKey == aseg.TIPO_EMPRESAReference.EntityKey);

                        if (tipo.TE_CODIGO == 1)
                        {
                            if (dcat.ADA_CONTRATO != null)
                                numPoliza = dcat.ADA_CONTRATO;
                            nomAseg = aseg.ASE_NOMBRE;
                            montoAseg = dcat.ADA_MONTO_COBERTURA.ToString();
                            if (aseg.ASE_TELEFONO != null)
                                telfAseg = aseg.ASE_TELEFONO;
                        }
                        else if (tipo.TE_CODIGO == 2)
                        {
                            numContrato = dcat.ADA_CONTRATO;
                            nomEmp = aseg.ASE_NOMBRE;
                            montoEmp = dcat.ADA_MONTO_COBERTURA.ToString();
                            telfEmp = aseg.ASE_TELEFONO;
                        }
                    }



                    His.DatosReportes.Datos.GenerarPdf pdf = new His.DatosReportes.Datos.GenerarPdf();
                    pdf.reporte = modo_formulario;
                    pdf.campo1 = ultimaAtencion.ATE_CODIGO.ToString();
                    pdf.nuemro_atencion = ultimaAtencion.ATE_NUMERO_ATENCION.ToString();
                    pdf.clinica = txt_historiaclinica.Text;
                    pdf.campo2 = nombreContrato;
                    pdf.campo3 = numPoliza;
                    pdf.campo4 = nomAseg;
                    pdf.campo5 = montoAseg;
                    pdf.campo6 = telfAseg;
                    pdf.campo7 = numContrato;
                    pdf.campo8 = nomEmp;
                    pdf.campo9 = telfEmp;
                    pdf.campo10 = montoEmp;
                    pdf.generar();

                }
                else
                {
                    MessageBox.Show("No existe una atención", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        # endregion

        # region Otros

        private void limpiarCamposPaciente()
        {
            txt_apellido1.Text = string.Empty;
            txt_apellido2.Text = string.Empty;
            txt_nombre1.Text = string.Empty;
            txt_nombre2.Text = string.Empty;
            txt_cedula.Text = string.Empty;
            //dtp_fecnac.Value = DateTime.Now;
            txt_nacionalidad.Text = string.Empty;
            dtp_feCreacion.Value = DateTime.Now;
            dtp_fecnac.Value = DateTime.Now;
            txt_nombreRef.Text = string.Empty;
            txt_parentRef.Text = string.Empty;
            txt_telfRef.Text = string.Empty;
            txtRefDireccion.Text = string.Empty;
            //cmb_pais.SelectedIndex = 0;
            //cb_etnia.SelectedIndex = 0;
            //cb_gruposanguineo.SelectedIndex = 0;
            registroCambios.DataSource = null;
            gridAtenciones.DataSource = null;
            rbCedula.Checked = true;

            txt_telfRef2.Text = string.Empty;  //lx202005
            dtpFallecido.Value = DateTime.Now;//lx202005
            chkFallecido.Checked = false;//lx202005
        }

        private void limpiarCamposDireccion()
        {
            txt_direccion.Text = string.Empty;
            txt_codBarrio.Text = string.Empty;
            txt_codCanton.Text = string.Empty;
            txt_codParroquia.Text = string.Empty;
            txt_codProvincia.Text = string.Empty;
            txt_codPais.Text = string.Empty;
            txt_telefono1.Text = string.Empty;
            txt_telefono2.Text = string.Empty;
            txt_ocupacion.Text = string.Empty;
            txt_rucEmp.Text = string.Empty;
            txt_empresa.Text = string.Empty;
            txt_direcEmp.Text = string.Empty;
            txt_telfEmp.Text = string.Empty;
            txt_ciudadEmp.Text = string.Empty;
            txt_instuccion.Text = string.Empty;
            txt_email.Text = string.Empty;
            //cmb_estadocivil.SelectedIndex = 0;
            //cmb_ciudadano.SelectedIndex = 0;
        }

        private void limpiarCamposAtencion(int aux)
        {
            txt_nombreAcomp.Text = string.Empty;
            txt_telefonoAcomp.Text = string.Empty;
            txt_direccionAcomp.Text = string.Empty;
            txt_ciudadAcomp.Text = string.Empty;
            txt_cedulaAcomp.Text = string.Empty;
            txt_parentescoAcomp.Text = string.Empty;
            txt_habitacion.Text = string.Empty;
            txtReferidoDe.Text = string.Empty;
            txt_diagnostico.Text = string.Empty;
            txt_nombreGar.Text = string.Empty;
            txt_telfGar.Text = string.Empty;
            txt_dirGar.Text = string.Empty;
            txt_ciudadGar.Text = string.Empty;
            txt_cedulaGar.Text = string.Empty;
            txt_parentGar.Text = string.Empty;
            txt_montoGar.Text = string.Empty;
            txt_observaciones.Text = string.Empty;
            txt_numeroatencion.Text = string.Empty;
            txtCodMedico.Text = string.Empty;
            dateTimeFecIngreso.Value = DateTime.Now;
            txt_obPago.Text = string.Empty;

           if(cmb_tipoingreso.SelectedIndex >= 0)
                cmb_tipoingreso.SelectedIndex = codigoTipoIngreso;
            if (cmb_formaLlegada.SelectedIndex >= 0)
                cmb_formaLlegada.SelectedIndex = 0;
            if (cmb_tiporeferido.SelectedIndex >= 0)
                cmb_tiporeferido.SelectedIndex = 0;
            if (cmb_tipoatencion.SelectedIndex > 0)
                cmb_tipoatencion.SelectedIndex = 0;
            if (cb_tipoGarantia.SelectedIndex >= 0)
                cb_tipoGarantia.SelectedIndex = 0;
            if (cb_personaFactura.SelectedIndex >= 0)
                cb_personaFactura.SelectedIndex = 0;
            gridAseguradoras.Rows.Clear();
            gridGarantias.Rows.Clear();

            txt_nombreTitular.Text = string.Empty;
            txt_DireccionTitular.Text = string.Empty;
            txt_CedulaTitular.Text = string.Empty;
            if (aux == 0)
            {
                this.cmbAccidente.SelectedIndex = 0;
                if(cmb_Parentesco.SelectedIndex >= 0)
                    cmb_Parentesco.SelectedIndex = 0;
            }
            txt_TelefonoTitular.Text = string.Empty;
            txt_CedulaTitular.Text = string.Empty;
            cargar_cbotiposatenciones(); //lx202005
            this.txtIdDiscapacidad.Text = string.Empty;//lx202005
            txtPorcentageDA.Text = "0";//lx202005
            txtObservacionDA.Text = string.Empty;//lx202005
            txtEmpresaDA.Text = string.Empty;//lx202005
            this.chkDiscapacidad.Checked = false;//lx202005
        }

        private void cargar_cbotiposatenciones() //lx202005
        {
            DataTable dt = NegAtenciones.TiposAtenciones(Convert.ToString(cmb_tipoingreso.SelectedValue));//lx2323
           

            cmbTipoAtencion.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbTipoAtencion.Items.Add(dt.Rows[i]["id"].ToString() + "  - " + dt.Rows[i]["name"].ToString());
            }
            cmbTipoAtencion.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cmbTipoAtencion.Items.Count>0)
              cmbTipoAtencion.SelectedIndex = 0;


            DataTable dt2 = NegAtenciones.TiposDiscapacidades();
            cmbTiposDiscapacidadesDA.Items.Clear();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                cmbTiposDiscapacidadesDA.Items.Add(dt2.Rows[i]["id"].ToString() + "  - " + dt2.Rows[i]["name"].ToString());
            }
            cmbTiposDiscapacidadesDA.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTiposDiscapacidadesDA.SelectedIndex = 0;

            DataTable dt3 = NegAtenciones.TiposPaquetes();
            cboPaquete.Items.Clear();
            cboPaquete.Items.Add("Ninguno");
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                cboPaquete.Items.Add(dt3.Rows[i]["id"].ToString() + "  - " + dt3.Rows[i]["name"].ToString());
            }
            cboPaquete.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPaquete.SelectedIndex = 0;

        }
        private void cargar_cbotipoatencion(int codigo) //lx202005
        {
            string dt = NegAtenciones.TipoAtencion(codigo);
            cmbTipoAtencion.Items.Clear();
            cmbTipoAtencion.Items.Add(dt);
            cmbTipoAtencion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoAtencion.SelectedIndex = 0;
          
            
        }
        private void cargar_cbotipodiscapacidad(int codigo) //lx202005
        {
            string dt2 = NegAtenciones.TipoDiscapacidad(codigo);
            cmbTiposDiscapacidadesDA.Items.Clear();
            cmbTiposDiscapacidadesDA.Items.Add(dt2);
            cmbTiposDiscapacidadesDA.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTiposDiscapacidadesDA.SelectedIndex = 0;

        }
        private void cargar_cbotipopaquete(int codigo) //lx202005
        {
            
            cboPaquete.Items.Clear();
            if (ateDA.paquete.Trim() != "0")
            {
                string dt2 = NegAtenciones.TipoPaquete(codigo);
                cboPaquete.Items.Add(dt2);
            }
            

            DataTable dt3 = NegAtenciones.TiposPaquetes();
            cboPaquete.Items.Add("Ninguno");
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                cboPaquete.Items.Add(dt3.Rows[i]["id"].ToString() + "  - " + dt3.Rows[i]["name"].ToString());
            }
            cboPaquete.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPaquete.SelectedIndex = 0;

        }

        private void cargar_cbotiposatenciones_adicionarYhabiliar() //lx202005
        {
            DataTable dt = NegAtenciones.TiposAtenciones(Convert.ToString(cmb_tipoingreso.SelectedValue));//lx2323
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbTipoAtencion.Items.Add(dt.Rows[i]["id"].ToString() + "  - " + dt.Rows[i]["name"].ToString());
            }
            cmbTipoAtencion.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cmbTipoAtencion.Items.Count > 0)
                cmbTipoAtencion.SelectedIndex = 0;


            DataTable dt2 = NegAtenciones.TiposDiscapacidades();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                cmbTiposDiscapacidadesDA.Items.Add(dt2.Rows[i]["id"].ToString() + "  - " + dt2.Rows[i]["name"].ToString());
            }
            cmbTiposDiscapacidadesDA.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTiposDiscapacidadesDA.SelectedIndex = 0;

            

        }





        private void habilitarCamposPaciente()
        {
            txt_apellido1.ReadOnly = false;
            txt_apellido2.ReadOnly = false;
            txt_nombre1.ReadOnly = false;
            txt_nombre2.ReadOnly = false;
            txt_cedula.ReadOnly = false;
            dtp_fecnac.Enabled = true;
            txt_nacionalidad.ReadOnly = false;
            cb_etnia.Enabled = true;
            cb_gruposanguineo.Enabled = true;
            rbPasaporte.Enabled = true;
            rbRuc.Enabled = true;
            rbCedula.Enabled = true;
            rbSid.Enabled = true;
            txt_cedula.ReadOnly = false;
            dtp_fecnac.Enabled = true;
            txt_nacionalidad.ReadOnly = false;
            cb_etnia.Enabled = true;
            cb_gruposanguineo.Enabled = true;
            cmb_pais.Enabled = true;
            cmb_ciudad.Enabled = true;
            rbn_h.Enabled = true;
            rbn_m.Enabled = true;
            txt_nombreRef.ReadOnly = false;
            txt_parentRef.ReadOnly = false;
            txt_telfRef.ReadOnly = false;
            txtRefDireccion.ReadOnly = false;

            txt_telfRef2.ReadOnly = false;//lx202005
            dtpFallecido.Enabled = true;//lx202005
            chkFallecido.Enabled = true;//lx202005

        }

        private void deshabilitarCamposPaciente()
        {
            txt_apellido1.ReadOnly = true;
            txt_apellido2.ReadOnly = true;
            txt_nombre1.ReadOnly = true;
            txt_nombre2.ReadOnly = true;
            txt_cedula.ReadOnly = true;
            dtp_fecnac.Enabled = false;
            txt_nacionalidad.ReadOnly = true;
            cb_etnia.Enabled = false;
            cb_gruposanguineo.Enabled = false;
            rbPasaporte.Enabled = false;
            rbRuc.Enabled = false;
            rbCedula.Enabled = false;
            rbSid.Enabled = false;
            txt_cedula.ReadOnly = true;
            dtp_fecnac.Enabled = false;
            txt_nacionalidad.ReadOnly = true;
            cb_etnia.Enabled = false;
            cb_gruposanguineo.Enabled = false;
            cmb_pais.Enabled = false;
            cmb_ciudad.Enabled = false;
            rbn_h.Enabled = false;
            rbn_m.Enabled = false;
            txt_nombreRef.ReadOnly = true;
            txt_parentRef.ReadOnly = true;
            txt_telfRef.ReadOnly = true;
            txtRefDireccion.ReadOnly = true;

            txt_telfRef2.ReadOnly = true;//lx202005
            dtpFallecido.Enabled = false;//lx202005
            chkFallecido.Enabled = false;//lx202005

        }


        private void habilitarCamposDireccion()
        {
            txt_codPais.ReadOnly = false;
            txt_direccion.ReadOnly = false;
            txt_telefono1.ReadOnly = false;
            txt_telefono2.ReadOnly = false;
            txt_ocupacion.ReadOnly = false;
            txt_rucEmp.ReadOnly = false;
            txt_empresa.ReadOnly = false;
            txt_direcEmp.ReadOnly = false;
            txt_telfEmp.ReadOnly = false;
            txt_ciudadEmp.ReadOnly = false;
            cmb_estadocivil.Enabled = true;
            cmb_ciudadano.Enabled = true;
            txt_instuccion.ReadOnly = false;
            txt_email.ReadOnly = false;
        }

        private void deshabilitarCamposDireccion()
        {
            txt_codPais.ReadOnly = true;
            txt_codProvincia.ReadOnly = true;
            txt_codCanton.ReadOnly = true;
            txt_codParroquia.ReadOnly = true;
            txt_codBarrio.ReadOnly = true;
            txt_direccion.ReadOnly = true;
            txt_telefono1.ReadOnly = true;
            txt_telefono2.ReadOnly = true;
            txt_ocupacion.ReadOnly = true;
            txt_rucEmp.ReadOnly = true;
            txt_empresa.ReadOnly = true;
            txt_direcEmp.ReadOnly = true;
            txt_telfEmp.ReadOnly = true;
            txt_ciudadEmp.ReadOnly = true;
            cmb_estadocivil.Enabled = false;
            cmb_ciudadano.Enabled = false;
            txt_instuccion.ReadOnly = true;
            txt_email.ReadOnly = true;
        }


        private void habilitarCamposAtencion()
        {
            txt_nombreAcomp.ReadOnly = false;
            txt_telefonoAcomp.ReadOnly = false;
            txt_direccionAcomp.ReadOnly = false;
            txt_ciudadAcomp.ReadOnly = false;
            txt_cedulaAcomp.ReadOnly = false;
            txt_parentescoAcomp.ReadOnly = false;
            cmb_tipoatencion.Enabled = true;
            cmb_tiporeferido.Enabled = true;
            cmb_formaLlegada.Enabled = true;
            if (codigoTipoIngreso == 0)
                cmb_tipoingreso.Enabled = true;
            cboPaquete.Enabled = true;
            cmbConvenioPago.Enabled = true;
            cb_seguros.Enabled = true;
            btnAddAseg.Enabled = true;
            txtReferidoDe.ReadOnly = false;
            txt_diagnostico.ReadOnly = false;
            txt_nombreGar.ReadOnly = false;
            txt_telfGar.ReadOnly = false;
            txt_dirGar.ReadOnly = false;
            txt_ciudadGar.ReadOnly = false;
            txt_cedulaGar.ReadOnly = false;
            txt_parentGar.ReadOnly = false;
            txt_montoGar.ReadOnly = false;
            txt_observaciones.ReadOnly = false;
            txt_obPago.ReadOnly = false;
            txtCodMedico.ReadOnly = false;
            cb_personaFactura.Enabled = true;
            cb_tipoGarantia.Enabled = true;
            btnAddGar.Enabled = true;
            uBtnAddAcompaniante.Enabled = true;
            txtIdDiscapacidad.Enabled = true;//lx2020
            chkDiscapacidad.Enabled = true;//lx2020
            txtPorcentageDA.Enabled = true;//lx2020
            txtObservacionDA.Enabled = true;//lx2020
            txtEmpresaDA.Enabled = true;//lx2020
            cmbTiposDiscapacidadesDA.Enabled = true;//lx2020
            cmb_Parentesco.Enabled = true;//lx201
            cmbTipoAtencion.Enabled = true;//lx201
            habilitarAyudas();
        }

        private void deshabilitarCamposAtencion()
        {
            deshabilitarAyudas();
        
            cmb_Parentesco.Enabled = false;//lx201
            cmbTipoAtencion.Enabled = false;//lx201
            cmbTiposDiscapacidadesDA.Enabled = false;//lx202005
            txt_nombreAcomp.ReadOnly = true;
            txt_telefonoAcomp.ReadOnly = true;
            txt_direccionAcomp.ReadOnly = true;
            txt_ciudadAcomp.ReadOnly = true;
            txt_cedulaAcomp.ReadOnly = true;
            txt_parentescoAcomp.ReadOnly = true;
            cmb_tipoatencion.Enabled = false;
            cmb_tiporeferido.Enabled = false;
            cmb_formaLlegada.Enabled = false;
            if (codigoTipoIngreso == 0)
                cmb_tipoingreso.Enabled = false;
            txt_habitacion.ReadOnly = true;
            txtReferidoDe.ReadOnly = true;
            cboPaquete.Enabled = false;
            cmbConvenioPago.Enabled = false;
            cb_seguros.Enabled = false;
            txt_diagnostico.ReadOnly = true;
            txt_nombreGar.ReadOnly = true;
            txt_telfGar.ReadOnly = true;
            txt_dirGar.ReadOnly = true;
            txt_ciudadGar.ReadOnly = true;
            txt_cedulaGar.ReadOnly = true;
            txt_parentGar.ReadOnly = true;
            txt_montoGar.ReadOnly = true;
            txt_observaciones.ReadOnly = true;
            txt_obPago.ReadOnly = true;
            cb_personaFactura.Enabled = false;
            dateTimeFecIngreso.Enabled = false;
            cb_seguros.Enabled = false;
            txtPorcentageDA.Enabled = false;//lx202005
            txtObservacionDA.Enabled = false;//lx202005
            txtEmpresaDA.Enabled = false;//lx202005
            chkDiscapacidad.Enabled = false;//lx2020
            txtIdDiscapacidad.Enabled = false;//lx2020
            cb_tipoGarantia.Enabled = false;
            cb_seguros.Enabled = false;

            btnAddAseg.Enabled = false;
            btnAddGar.Enabled = false;
            uBtnAddAcompaniante.Enabled = false;
            
        }


        private void habilitarAyudas()
        {
            ayudaNacionalidad.Visible = true;
            ayudaPais.Visible = true;
            ayudaProvincia.Visible = true;
            ayudaCanton.Visible = true;
            ayudaParroquia.Visible = true;
            ayudaBarrio.Visible = true;
            ayudaEmpresa.Visible = true;
            ayudaMedicos.Visible = true;
            ayudaHabitaciones.Visible = true;
        }

        private void deshabilitarAyudas()
        {
            ayudaNacionalidad.Visible = false;
            ayudaPais.Visible = false;
            ayudaProvincia.Visible = false;
            ayudaCanton.Visible = false;
            ayudaParroquia.Visible = false;
            ayudaBarrio.Visible = false;
            ayudaEmpresa.Visible = false;
            ayudaMedicos.Visible = false;
            ayudaHabitaciones.Visible = false;
        }

        private void fijarDatos()
        {
            cmb_tipoingreso.SelectedIndex = 4;
            cmb_formaLlegada.SelectedIndex = 1;
            cmb_tiporeferido.SelectedIndex = 0;
            cmb_tipoatencion.SelectedIndex = 0;
            cmbConvenioPago.SelectedIndex = 0;
            cmb_Parentesco.SelectedIndex = -1;
            cb_personaFactura.SelectedIndex = 0;

        }

        private void limpiarCamposPreAtencion()
        {
            chb_PreAdmision.Checked = false;
            dtpPreAdmision.Value = DateTime.Now;
        }
        private void habilitarCamposPreAtencion()
        {
            chb_PreAdmision.Enabled = true;
            dtpPreAdmision.Enabled = true;
            dtpPreAdmision.Value = DateTime.Now;
            ayudaHabitaciones.Enabled = true;
            preAtencioActual = null;
        }
        private void deshabilitarCamposPreAtencion()
        {
            chb_PreAdmision.Enabled = false;
            dtpPreAdmision.Enabled = false;
            ayudaHabitaciones.Enabled = false;
        }
        private bool validarFormulario()
        {
            try
            {
                erroresPaciente.Clear();

                bool valido = true;
                //VALIDA CONVENIOS 
                if (gridAseguradoras.RowCount <= 0)
                {
                    AgregarError(gridAseguradoras);
                    valido = false;
                }
                else
                {
                    bool accion = false;
                    for (Int16 i = 0; i < gridAseguradoras.RowCount; i++)
                    {
                        DataGridViewRow fila = gridAseguradoras.Rows[i];
                        if (fila.Cells["Monto"].Value == null)
                            accion = true;
                        //if (gridAseguradoras.Rows[i].Cells[0].Value.Equals(AdmisionParametros.CodigoConvenioIess))
                        if (NegAtenciones.RPIS(Convert.ToInt32(gridAseguradoras.Rows[i].Cells[0].Value))) //lx202005
                        {
                            if (!validarAutorizacion(i))
                            {
                                MessageBox.Show("Autorización Convenio IESS ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                valido = false;
                            }
                        }
                    }
                    if (accion)
                        MessageBox.Show("No se ingreso Monto del Convenio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }

                //CAMPOS PACIENTE

                if (txt_historiaclinica.Text.Trim() == string.Empty)
                {
                    AgregarError(txt_historiaclinica);
                    valido = false;
                }
                if (txt_CedulaTitular.Text.Trim() == string.Empty && cmbConvenioPago.Text == "ASEGURADORA")
                {
                    tabulador2.SelectedTab = tabulador2.Tabs["titular"];
                    AgregarError(txt_CedulaTitular);
                    valido = false;
                }

                if (txt_apellido1.Text.Trim() == string.Empty)
                {
                    AgregarError(txt_apellido1);
                    valido = false;
                }

                if (txt_nombre1.Text.Trim() == string.Empty)
                {
                    AgregarError(txt_nombre1);
                    valido = false;
                }
                if (txt_cedula.Text.Trim() == string.Empty)
                {
                    AgregarError(txt_cedula);
                    valido = false;
                }
                if (dtp_fecnac.Value > DateTime.Now)
                {
                    AgregarError(dtp_fecnac);
                    valido = false;
                }
                if (txt_nacionalidad.Text.Trim() == string.Empty)
                {
                    AgregarError(txt_nacionalidad);
                    valido = false;
                }
                if (cb_etnia.SelectedItem == null)
                {
                    AgregarError(cb_etnia);
                    valido = false;
                }
                if (cb_gruposanguineo.SelectedItem == null)
                {
                    AgregarError(cb_gruposanguineo);
                    valido = false;
                }
     
                // campos SEXO
                if (rbn_h.Checked == false && rbn_m.Checked == false)
                {
                    AgregarError(rbn_h);
                    valido = false;
                }

                //CAMPOS DIRECCION

                if (txt_pais.Text == string.Empty)
                {
                    AgregarError(txt_pais);
                    valido = false;
                }
                else if (txt_codPais.Text == "57")
                {
                    if (txt_provincia.Text == string.Empty)
                    {
                        AgregarError(txt_provincia);
                        valido = false;
                    }
                    if (txt_direccion.Text == string.Empty)
                    {
                        AgregarError(txt_direccion);
                        valido = false;
                    }
                }
                if (txt_telefono1.Text == "  -   -")
                {
                    AgregarError(txt_telefono1);
                    valido = false;
                }

                if (txt_rucEmp.Text != string.Empty)
                {
                    if (txt_empresa.Text == string.Empty)
                    {
                        AgregarError(txt_empresa);
                        valido = false;
                    }
                    if (txt_direcEmp.Text == string.Empty)
                    {
                        AgregarError(txt_direcEmp);
                        valido = false;
                    }
                    if (txt_telfEmp.Text == "   -   -")
                    {
                        AgregarError(txt_telfEmp);
                        valido = false;
                    }
                    if (txt_ciudadEmp.Text == string.Empty)
                    {
                        AgregarError(txt_ciudadEmp);
                        valido = false;
                    }
                }

                //CAMPOS ATENCION
                if (txt_numeroatencion.Text != string.Empty)
                {

                    if (dateTimeFecIngreso.Value == null)
                    {
                        AgregarError(dateTimeFecIngreso);
                        valido = false;
                    }
                    if (cmb_tipoingreso.SelectedIndex != 2)
                    {
                        if (txt_habitacion.Text == string.Empty)
                        {
                            AgregarError(txt_habitacion);
                            valido = false;
                        }
                    }

                    if (cb_personaFactura.SelectedItem.ToString() == "ACOMPAÑANTE")
                    {
                        if (txt_nombreAcomp.Text == string.Empty)
                        {
                            AgregarError(txt_nombreAcomp);
                            valido = false;
                        }
                        if (txt_cedulaAcomp.Text == string.Empty)
                        {
                            AgregarError(txt_cedulaAcomp);
                            valido = false;
                        }
                        if (txt_parentescoAcomp.Text == string.Empty)
                        {
                            AgregarError(txt_parentescoAcomp);
                            valido = false;
                        }
                        if (txt_telefonoAcomp.Text == "   -   -")
                        {
                            AgregarError(txt_telefonoAcomp);
                            valido = false;
                        }
                        if (txt_direccionAcomp.Text == string.Empty)
                        {
                            AgregarError(txt_direccionAcomp);
                            valido = false;
                        }
                        if (txt_ciudadAcomp.Text == string.Empty)
                        {
                            AgregarError(txt_ciudadAcomp);
                            valido = false;
                        }
                    }


                    if (cb_personaFactura.SelectedItem.ToString() == "GARANTE")
                    {
                        if (txt_nombreGar.Text != string.Empty)
                        {
                            AgregarError(txt_nombreGar);
                            valido = false;
                        }

                        if (txt_cedulaGar.Text == string.Empty)
                        {
                            AgregarError(txt_cedulaGar);
                            valido = false;
                        }
                        if (txt_parentGar.Text == string.Empty)
                        {
                            AgregarError(txt_parentGar);
                            valido = false;
                        }
                        if (txt_montoGar.Text == string.Empty)
                        {
                            AgregarError(txt_montoGar);
                            valido = false;
                        }
                        if (txt_telfGar.Text == "   -   -")
                        {
                            AgregarError(txt_telfGar);
                            valido = false;
                        }
                        if (txt_dirGar.Text == string.Empty)
                        {
                            AgregarError(txt_dirGar);
                            valido = false;
                        }
                        if (txt_ciudadGar.Text == string.Empty)
                        {
                            AgregarError(txt_ciudadGar);
                            valido = false;
                        }
                    }

                    if (cb_personaFactura.SelectedItem.ToString() == "EMPRESA")
                    {
                        if (txt_rucEmp.Text == string.Empty)
                        {
                            AgregarError(txt_rucEmp);
                            valido = false;
                        }
                    }


                    if (cmb_tipoatencion.SelectedItem == null)
                    {
                        AgregarError(cmb_tipoatencion);
                        valido = false;
                    }
                    if (cmb_tiporeferido.SelectedItem == null)
                    {
                        AgregarError(cmb_tiporeferido);
                        valido = false;
                    }
                    if (medico == null)
                    {
                        AgregarError(txtNombreMedico);
                        valido = false;
                    }
                    if (cmb_formaLlegada.SelectedItem == null)
                    {
                        AgregarError(cmb_formaLlegada);
                        valido = false;
                    }
                }
                if (cmb_tipoingreso.SelectedIndex == 5)
                {
                    if (!validarFechasPreadmison())
                    {
                        AgregarError(chb_PreAdmision);
                        valido = false;
                    }
                    if (txt_habitacion.Text != "PREA")
                    {
                        AgregarError(txt_habitacion);
                        valido = false;
                    }
                }
                else
                {
                    if (preAtencioActual != null)
                    {
                        if (preAtencioActual.PREA_FECHA_PREADMISON < DateTime.Now)
                        {
                            if (txt_observaciones.Text.Trim() == "")
                            {
                                AgregarError(txt_observaciones);
                                valido = false;
                            }
                        }
                        if (cmb_tipoingreso.SelectedIndex == 5)
                        {
                            AgregarError(cmb_tipoingreso);
                            valido = false;
                        }
                    }
                }
                if (cmbTipoAtencion.Text == "")
                {
                    AgregarError(cmbTipoAtencion);
                    valido = false;
                }
                if (txt_email.Text == string.Empty)
                {
                    AgregarError(txt_email);
                    valido = false;
                }
                return valido;
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        private void AgregarError(Control control)
        {
            erroresPaciente.SetError(control, "Campo Requerido");
        }


        private bool validarAutorizacion(int fila)
        {
            bool valido = false;
            string campo = (Convert.ToString(gridAseguradoras.Rows[fila].Cells[2].Value));
            if (campo != "")
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
                }
                if (accion)
                {
                    List<ANEXOS_IESS> listaA = NegAnexos.RecuperarListaAnexos(Convert.ToString(AdmisionParametros.CodigoAnexoDerivacion));
                    foreach (ANEXOS_IESS anexos in listaA)
                    {
                        if (anexos.ANI_COD_PRO.Equals(cadena))
                            valido = true;
                    }
                }
            }
            else
            {
                valido = true;
            }
            return valido;
        }

        # endregion

        # region Eventos para la tabulacion


        private void rbSid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void btnAddAseg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void btnAddGar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dateTimePicker1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dateTimePicker1_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void chk_fecing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_rucEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_obPago_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cb_personaFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                chkDiscapacidad.Focus();
            }
        }


        private void txt_apellido1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_historiaclinica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_apellido2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_nombre1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_nombre2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                tabulador.SelectedTab = tabulador.Tabs["paciente"];
                e.Handled = true;

                if (rbCedula.Enabled == true)
                {
                    if (rbCedula.Checked == true)
                        rbCedula.Focus();
                    else if (rbPasaporte.Checked == true)
                        rbPasaporte.Focus();
                    else if (rbRuc.Checked == true)
                        rbRuc.Focus();
                    else if (rbSid.Checked == true)
                        rbSid.Focus();
                    else
                        rbCedula.Focus();
                }
                else
                {
                    txt_cedula.Focus();
                }

            }
        }

        private void rbCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void rbPasaporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void rbRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_cedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || Convert.ToInt32(e.KeyChar).ToString() == "8")
            {

            }
            else
            {
                e.Handled = true; // no deja que ingrese simbolos / Giovanny Tapia /24/08/2012
            }

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_nacionalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void rbn_h_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void rbn_m_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_direccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_barrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_parroquia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_canton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_provincia_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void rbn_zu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void rbn_zr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_telefono1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_telefono2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_ocupacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_empresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_direcEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_telfEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_ciudadEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_instuccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_seguro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_habitacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    if (cb_seguros.Enabled == true)
            //        SendKeys.SendWait("{TAB}");
            //    else
            //        cb_tipoGarantia.Focus();
            //}
        }

        private void cboFiltroFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_diagnostico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_observaciones.Focus();
            }
        }

        private void txt_observaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_nombreAcomp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_cedulaAcomp.Focus();
            }
        }

        private void txt_cedulaAcomp_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_parentescoAcomp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_telefonoAcomp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_direccionAcomp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_ciudadAcomp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                tabulador2.SelectedTab = tabulador2.Tabs["titular"];
                e.Handled = true;
                txt_nombreTitular.Focus();
                //SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_nombreGar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_cedulaGar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_parentGar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_montoGar_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_telfGar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_dirGar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }
        private void txt_feCreacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                txt_email.Text = txt_email.Text.Trim();
                if (ComprobarFormatoEmail(txt_email.Text) == true)
                {
                    e.Handled = true;
                    txt_nombreRef.Focus();
                }
                else
                {
                    MessageBox.Show("DIRECCIÓN DE CORREO ELECTRONICO NO ES VALIDA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_email.Text = string.Empty;
                }
            }
        }

        //PABLO ROCHA 08-10-2014
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

        private void txt_codProvincia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_codCanton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_codParroquia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_codBarrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_numeroatencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }

        }

        private void txt_ciudadGar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }
        private void txt_codPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        #endregion

        # region Filtros Busqueda Pacientes

        private void txt_busqCi_KeyDown(object sender, KeyEventArgs e)
        {
            Buscar(e);
        }

        private void txt_busqHist_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_busqApe_KeyDown(object sender, KeyEventArgs e)
        {
            Buscar(e);
        }

        private void txt_busqNom_KeyDown(object sender, KeyEventArgs e)
        {
            Buscar(e);
        }

        private void Buscar(KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
                {

                    //string id = txt_busqCi.Text.ToString();
                    //string historia = txt_busqHist.Text.ToString();
                    //string apellido = txt_busqApe.Text.ToString();
                    //string nombre = txt_busqNom.Text.ToString();

                    ////frm_Ayudas ayuda = new frm_Ayudas(NegPacientes.listaPacientesFiltros(id, historia, apellido, nombre),"PACIENTES","HISTORIA_CLINICA");
                    //frm_AyudaPacientes ayuda = new frm_AyudaPacientes(NegPacientes.listaPacientesFiltros(id, historia, apellido, nombre));
                    //ayuda.campoPadre = txt_historiaclinica;
                    //ayuda.ShowDialog();

                    //if (ayuda.campoPadre.Text != string.Empty)
                    //    CargarPaciente(ayuda.campoPadre.Text.Trim().ToString());
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        #endregion

        #region campos Division Politica y eventos

        private void BuscarDivision(string tipo)
        {
            try
            {
                List<DIVISION_POLITICA> divi = new List<DIVISION_POLITICA>();
                TextBox control = txt_codPais;
                TextBox control2 = txt_pais;
                string pais = txt_codPais.Text.ToString();
                string provincia = txt_codProvincia.Text.ToString();
                string canton = txt_codCanton.Text.ToString();
                string parroquia = txt_codParroquia.Text.ToString();
                string sector = txt_codBarrio.Text.ToString();



                if (tipo == "pais")
                {
                    divi = NegDivisionPolitica.listaDivisionPolitica("2").ToList();
                    control = txt_codPais;
                    control2 = txt_pais;
                }
                if (tipo == "provincia")
                {
                    divi = divi = NegDivisionPolitica.RecuperarDivisionPolitica(pais).ToList();
                    control = txt_codProvincia;
                    control2 = txt_provincia;
                }
                if (tipo == "canton")
                {
                    divi = divi = NegDivisionPolitica.RecuperarDivisionPolitica(provincia).ToList();
                    control = txt_codCanton;
                    control2 = txt_canton;
                }
                if (tipo == "parroquia")
                {
                    divi = divi = NegDivisionPolitica.RecuperarDivisionPolitica(canton).ToList();
                    control = txt_codParroquia;
                    control2 = txt_parroquia;
                }
                if (tipo == "sector")
                {
                    divi = divi = NegDivisionPolitica.RecuperarDivisionPolitica(parroquia).ToList();
                    control = txt_codBarrio;
                    control2 = txt_barrio;
                }

                frm_Ayudas ayuda = new frm_Ayudas(divi, "DIVISION_POLITICA", "CODIGO");
                ayuda.campoPadre = control;
                ayuda.campoPadre2 = control2;
                ayuda.colRetorno = "CODIGO";
                ayuda.colRetorno2 = "NOMBRE";
                ayuda.ShowDialog();
                ayuda.Dispose();

            }
            catch (System.Exception r)
            {
                MessageBox.Show(r.Message);
                if (r.InnerException != null)
                    MessageBox.Show(r.InnerException.Message);
            }
        }

        private void cargarDivision(string texto, TextBox control, string tipo)
        {
            string pais = txt_codPais.Text.ToString();
            string provincia = txt_codProvincia.Text.ToString();
            string canton = txt_codCanton.Text.ToString();
            string parroquia = txt_codParroquia.Text.ToString();
            string sector = txt_codBarrio.Text.ToString();
            List<DIVISION_POLITICA> divi = new List<DIVISION_POLITICA>();
            TextBox control2 = new TextBox();

            if (tipo == "pais")
            {
                divi = NegDivisionPolitica.listaDivisionPolitica("2").ToList();
                control2 = txt_codPais;
            }
            if (tipo == "provincia")
            {
                divi = NegDivisionPolitica.RecuperarDivisionPolitica(pais).ToList();
                control2 = txt_codProvincia;
            }
            if (tipo == "canton")
            {
                divi = NegDivisionPolitica.RecuperarDivisionPolitica(provincia).ToList();
                control2 = txt_codCanton;
            }
            if (tipo == "parroquia")
            {
                divi = NegDivisionPolitica.RecuperarDivisionPolitica(canton).ToList();
                control2 = txt_codParroquia;
            }
            if (tipo == "sector")
            {
                divi = NegDivisionPolitica.RecuperarDivisionPolitica(parroquia).ToList();
                control2 = txt_codBarrio;
            }

            DIVISION_POLITICA dp = divi.FirstOrDefault(p => p.DIPO_CODIINEC == texto);
            if (dp != null)
                control.Text = dp.DIPO_NOMBRE;
            else
            {
                control.Text = string.Empty;
            }
        }


        private void txt_codPais_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnGuardar.Enabled == true)
            {
                if (e.KeyCode == Keys.F1)
                {
                    BuscarDivision("pais");
                }
            }
        }

        private void txt_codProvincia_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnGuardar.Enabled == true)
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (txt_pais.Text.ToString() == string.Empty)
                        MessageBox.Show("Seleccione primero un pais");
                    else
                        BuscarDivision("provincia");
                }
            }
        }

        private void txt_codCanton_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnGuardar.Enabled == true)
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (txt_provincia.Text.ToString() == string.Empty)
                        MessageBox.Show("Seleccion primero una provincia");
                    else
                        BuscarDivision("canton");
                }
            }
        }

        private void txt_codParroquia_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnGuardar.Enabled == true)
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (txt_canton.Text.ToString() == string.Empty)
                        MessageBox.Show("Seleccion primero un canton");
                    else
                        BuscarDivision("parroquia");
                }
            }

        }

        private void txt_codBarrio_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnGuardar.Enabled == true)
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (txt_parroquia.Text.ToString() == string.Empty)
                        MessageBox.Show("Seleccion primero una parroquia");
                    else
                        BuscarDivision("sector");
                }
            }
        }


        private void txt_pais_TextChanged(object sender, EventArgs e)
        {
            if (txt_pais.Text != string.Empty && btnGuardar.Enabled == true)
                txt_codProvincia.ReadOnly = false;
            else
                txt_codProvincia.ReadOnly = true;

            txt_codProvincia.Text = string.Empty;
        }

        private void txt_provincia_TextChanged(object sender, EventArgs e)
        {
            if (txt_provincia.Text != string.Empty && btnGuardar.Enabled == true)
                txt_codCanton.ReadOnly = false;
            else
                txt_codCanton.ReadOnly = true;

            txt_codCanton.Text = string.Empty;
        }

        private void txt_canton_TextChanged(object sender, EventArgs e)
        {
            if (txt_canton.Text != string.Empty && btnGuardar.Enabled == true)
                txt_codParroquia.ReadOnly = false;
            else
                txt_codParroquia.ReadOnly = true;

            txt_codParroquia.Text = string.Empty;
        }

        private void txt_parroquia_TextChanged(object sender, EventArgs e)
        {
            if (txt_parroquia.Text != string.Empty && btnGuardar.Enabled == true)
                txt_codBarrio.ReadOnly = false;
            else
                txt_codBarrio.ReadOnly = true;

            txt_codBarrio.Text = string.Empty;
        }


        private void txt_codPais_TextChanged(object sender, EventArgs e)
        {
            cargarDivision(txt_codPais.Text.ToString(), txt_pais, "pais");
        }

        private void txt_codProvincia_TextChanged(object sender, EventArgs e)
        {
            cargarDivision(txt_codProvincia.Text.ToString(), txt_provincia, "provincia");
        }

        private void txt_codCanton_TextChanged(object sender, EventArgs e)
        {
            cargarDivision(txt_codCanton.Text.ToString(), txt_canton, "canton");
        }

        private void txt_codParroquia_TextChanged(object sender, EventArgs e)
        {
            cargarDivision(txt_codParroquia.Text.ToString(), txt_parroquia, "parroquia");
        }

        private void txt_codBarrio_TextChanged(object sender, EventArgs e)
        {
            cargarDivision(txt_codBarrio.Text.ToString(), txt_barrio, "sector");
        }

        private void txt_codPais_Leave(object sender, EventArgs e)
        {
            if (txt_pais.Text == string.Empty)
                txt_codPais.Text = string.Empty;
        }

        private void txt_codProvincia_Leave(object sender, EventArgs e)
        {
            if (txt_provincia.Text == string.Empty)
                txt_codProvincia.Text = string.Empty;
        }

        private void txt_codCanton_Leave(object sender, EventArgs e)
        {
            if (txt_canton.Text == string.Empty)
                txt_codCanton.Text = string.Empty;
        }

        private void txt_codParroquia_Leave(object sender, EventArgs e)
        {
            if (txt_parroquia.Text == string.Empty)
                txt_codParroquia.Text = string.Empty;
        }

        private void txt_codBarrio_Leave(object sender, EventArgs e)
        {
            if (txt_barrio.Text == string.Empty)
                txt_codBarrio.Text = string.Empty;
        }

        #endregion

        #region Validacion de telefonos y cedulas

        private void txt_telefono1_Leave(object sender, EventArgs e)
        {
            if (txt_telefono1.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txt_telefono1.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Numero de teléfono incorrecto");
                    txt_telefono1.Focus();
                }
            }
        }

        private void txt_telefono2_Leave(object sender, EventArgs e)
        {
            if (txt_telefono2.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValidoCelular(txt_telefono2.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Numero de teléfono incorrecto");
                    txt_telefono2.Focus();
                }
            }
        }

        private void txt_telfEmp_Leave(object sender, EventArgs e)
        {
            if (txt_telfEmp.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txt_telfEmp.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Numero de teléfono incorrecto");
                    txt_telfEmp.Focus();
                }
            }
        }

        private void txt_telefonoAcomp_Leave(object sender, EventArgs e)
        {
            if (txt_telefonoAcomp.Text.ToString() != "   -   -")
            {
                if (NegValidaciones.esTelefonoValido(txt_telefonoAcomp.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Numero de teléfono incorrecto");
                    txt_telefonoAcomp.Focus();
                }
            }
        }

        private void txt_telfGar_Leave(object sender, EventArgs e)
        {
            if (txt_telfGar.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txt_telfGar.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Numero de teléfono incorrecto");
                    txt_telfGar.Focus();
                }
            }
        }

        private void txt_cedulaAcomp_Leave(object sender, EventArgs e)
        {
            if (txt_cedulaAcomp.Text != string.Empty)
                if (NegValidaciones.esCedulaValida(txt_cedulaAcomp.Text) != true)
                {
                    txt_cedulaAcomp.Focus();
                    MessageBox.Show("Cédula Incorrecta");
                }
        }

        private void txt_cedulaGar_Leave(object sender, EventArgs e)
        {
            if (txt_cedulaGar.Text != string.Empty)
                if (NegValidaciones.esCedulaValida(txt_cedulaGar.Text) != true)
                {
                    MessageBox.Show("Cédula Incorrecta");
                    txt_cedulaGar.Focus();
                }


        }

        private void txt_cedula_Leave_1(object sender, EventArgs e)
        {
            if (rbCedula.Checked == true || rbRuc.Checked == true)
            {
                if (txt_cedula.Text.ToString() != string.Empty)
                {
                    try
                    {
                        if (NegValidaciones.esCedulaValida(txt_cedula.Text) != true)
                        {
                            //MessageBox.Show("Cédula Incorrecta");
                            MessageBox.Show("Identificación No Cumple con Formato", "HIS3000", MessageBoxButtons.OK);
                            txt_cedula.Focus();
                        }
                        //string cedula = txt_cedula.Text;
                        //if (rbCedula.Checked == true && cedula.Count() == 10)
                        //{
                        //    if (NegValidaciones.esCedulaValida2020(txt_cedula.Text) != true)
                        //    {
                        //        MessageBox.Show("Cédula Incorrecta");
                        //        txt_cedula.Focus();
                        //    }
                        //}
                        //else if (rbRuc.Checked == true && cedula.Count() == 13)
                        //{
                        //    if (NegValidaciones.esCedulaValida2020Ruc(txt_cedula.Text) != true)
                        //    {
                        //        MessageBox.Show("Ruc Incorrecto");
                        //        txt_cedula.Focus();
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Identificación No Cumple con Formato", "HIS3000", MessageBoxButtons.OK);
                        //    txt_cedula.Focus();
                        //}
                        
                        else{ 
                            if (pacienteActual == null)
                            {
                                PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());
                                DialogResult resp = new DialogResult();
                                if (consulta != null)
                                {
                                    resp = MessageBox.Show("Esta identificación ya existe.\nDesea cargar el paciente con este ID?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                                    if (resp == DialogResult.No)
                                    {
                                        txt_cedula.Focus();
                                    }
                                    else
                                    {
                                        //CargarPaciente(consulta.PAC_HISTORIA_CLINICA.ToString());
                                        pacienteNuevo = false;
                                        txt_historiaclinica.Tag = true;
                                        txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString();
                                    }
                                }
                            }
                            else if (pacienteActual.PAC_IDENTIFICACION != txt_cedula.Text.ToString())
                            {
                                PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());

                                DialogResult resp = new DialogResult();
                                if (consulta != null)
                                {
                                    resp = MessageBox.Show("Esta identificación ya existe.\nDesea cargar el paciente con este ID?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                                    if (resp == DialogResult.No)
                                    {
                                        txt_cedula.Focus();
                                    }
                                    else
                                    {
                                        //CargarPaciente(consulta.PAC_HISTORIA_CLINICA.ToString());
                                        pacienteNuevo = false;
                                        txt_historiaclinica.Tag = true;
                                        txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString();
                                    }
                                }
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }



            }
        }



        private void txt_rucEmp_Leave(object sender, EventArgs e)
        {
            if (txt_rucEmp.Text.ToString() != string.Empty)
            {
                CargarEmpresa(txt_rucEmp.Text);
                if (NegValidaciones.esCedulaValida(txt_rucEmp.Text) != true)
                {
                    MessageBox.Show("RUC Incorrecto");
                    txt_rucEmp.Focus();
                }
            }
        }

        #endregion

        #region Ayudas

        private void txt_habitacion_KeyDown_1(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.F1 && btnGuardar.Enabled == true)
            //    {
            //        Sesion.codHabitacion = 0;
            //        FRMCONTROLESWPF ayuda = new FRMCONTROLESWPF();
            //        ayuda.ShowDialog();
            //        ayuda.Dispose();
            //        if (Sesion.codHabitacion != 0)
            //        {
            //            txt_habitacion.Text = Sesion.numHabitacion;
            //            if (cb_seguros.Enabled == true)
            //                cb_seguros.Focus();
            //            else
            //                cb_tipoGarantia.Focus();
            //        }

            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(ex.InnerException.Message);
            //}
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                ayudaHabitaciones.PerformClick();
            }
        }

        private void txt_rucEmp_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && btnGuardar.Enabled == true)
            {
                frm_Ayudas ayuda = new frm_Ayudas(NegAseguradoras.ListaEmpresas(), "EMPRESAS", "RUC");
                ayuda.campoEspecial = txt_rucEmp;
                ayuda.colRetorno = "RUC";
                ayuda.ShowDialog();
                if (ayuda.campoEspecial.Text != string.Empty)
                    txt_ocupacion.Focus();
            }
        }

        private void txt_nacionalidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && btnGuardar.Enabled == true)
            {
                frm_Ayudas ayuda = new frm_Ayudas(NegPais.ListaNacionalidades(), "NACIONALIDADES", "NACIONALIDAD");
                ayuda.campoPadre = txt_nacionalidad;
                //ayuda.colRetorno = "RUC";
                ayuda.ShowDialog();
                ayuda.Dispose();
            }
        }

        #endregion

        #region Convenios y Garantias
        private void button2_Click(object sender, EventArgs e)
        {
            if (cb_seguros.SelectedItem != null)
            {
                DataGridViewRow dt = new DataGridViewRow();
                dt.CreateCells(gridAseguradoras);
                //ASEGURADORAS_EMPRESAS aseg = (ASEGURADORAS_EMPRESAS)cb_seguros.SelectedItem;
                //List<CATEGORIAS_CONVENIOS> cat = NegCategorias.ListaCategorias(aseg.ASE_CODIGO);
                CATEGORIAS_CONVENIOS cat = (CATEGORIAS_CONVENIOS)cb_seguros.SelectedItem;

                //if (cat.Count <= 1)
                //{
                dt.Cells[0].Value = cat.CAT_CODIGO.ToString();
                dt.Cells[1].Value = cat.CAT_NOMBRE;
                //}
                //else
                //{
                //    int value = 0;
                //    if (InputBox("Categorias", "Categorias", ref value, cat) == DialogResult.OK)
                //    {
                //        dt.Cells[0].Value = categorias.FirstOrDefault(c => c.CAT_CODIGO == value).CAT_CODIGO.ToString();
                //        dt.Cells[1].Value = categorias.FirstOrDefault(c => c.CAT_CODIGO == value).CAT_NOMBRE.ToString();
                //    }
                //}
                string cod = dt.Cells[0].Value.ToString();
                bool band = false;
                for (int i = 0; i < gridAseguradoras.Rows.Count; i++)
                    if (gridAseguradoras.Rows[i].Cells[0].Value.ToString() == cod)
                        band = true;

                if (band == false)
                    gridAseguradoras.Rows.Add(dt);
                else
                    MessageBox.Show("Convenio / Categoria no puede repetirse");
            }


            //if (cb_seguros.SelectedItem != null)
            //{
            //    DataGridViewRow dt = new DataGridViewRow();
            //    dt.CreateCells(gridAseguradoras);

            //    CATEGORIAS_CONVENIOS cat = (CATEGORIAS_CONVENIOS)cb_seguros.SelectedItem;

            //        dt.Cells[0].Value = cat.CAT_CODIGO.ToString();
            //        dt.Cells[1].Value = cat.CAT_NOMBRE;

            //    string cod = dt.Cells[0].Value.ToString();
            //    bool band = false;

            //    if(ultimaAtencion.ATE_CODIGO != null){
            //    List<CUENTAS_PACIENTES> cuentasPacientes = new List<CUENTAS_PACIENTES>();
            //    cuentasPacientes = NegCuentasPacientes.RecuperarCuenta(ultimaAtencion.ATE_CODIGO);

            //    Decimal totalCuenta = 0;
            //    Decimal totalConvenio = 0;
            //    if (cuentasPacientes.Count != 0)
            //    {
            //        foreach (var cuenta in cuentasPacientes)
            //        totalCuenta = totalCuenta + Convert.ToDecimal(cuenta.CUE_VALOR);
            //        for (int i = 0; i < gridAseguradoras.Rows.Count; i++)
            //        {
            //            if (gridAseguradoras.Rows[i].Cells[0].Value.ToString() == cod)
            //                band = true;
            //            totalConvenio = totalConvenio + Convert.ToDecimal(gridAseguradoras.Rows[i].Cells[4].Value.ToString());
            //        }
            //        if (band == false)
            //        {
            //            if (totalConvenio <= totalCuenta)
            //                gridAseguradoras.Rows.Add(dt);
            //            else
            //                MessageBox.Show("Ya cuenta con un Convenio");
            //        }
            //        else
            //            MessageBox.Show("Convenio / Categoria no puede repetirse");
            //    }
            //    else
            //    {
            //        if (gridAseguradoras.Rows.Count == 0)
            //            gridAseguradoras.Rows.Add(dt);
            //    }
            //}
        }

        public static DialogResult InputBox(string title, string promptText, ref int value, List<CATEGORIAS_CONVENIOS> lCategorias)
        {
            Form form = new Form();
            Label label = new Label();
            ComboBox combo = new ComboBox();
            Button buttonOk = new Button();
            //Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            combo.DataSource = lCategorias;
            combo.DisplayMember = "CAT_NOMBRE";
            combo.ValueMember = "CAT_CODIGO";

            buttonOk.Text = "OK";
            //buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            //buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            combo.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            //buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            combo.Anchor = combo.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            //buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, combo, buttonOk });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            form.AcceptButton = buttonOk;
            //form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = Convert.ToInt32(combo.SelectedValue);
            return dialogResult;
        }

        private void btnAddGar_Click(object sender, EventArgs e)
        {
            if (cb_tipoGarantia.SelectedItem != null)
            {
                DataGridViewRow dt = new DataGridViewRow();
                dt.CreateCells(gridGarantias);
                TIPO_GARANTIA aseg = (TIPO_GARANTIA)cb_tipoGarantia.SelectedItem;

                dt.Cells[0].Value = aseg.TG_CODIGO;
                dt.Cells[1].Value = aseg.TG_NOMBRE;
                gridGarantias.Rows.Add(dt);
            }
            lockcells();//lxgrntsx
        }

        
        #endregion

        #region Eventos

        private void txt_historiaclinica_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void cboFiltroTipoFormaPago_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            direccionNueva = chkCambioDatos.Checked;
        }


        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(((ComboBox)sender).SelectedValue.ToString());
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkDatosAc.Checked == true)
            {
                txt_nombreGar.Text = txt_nombreAcomp.Text;
                txt_dirGar.Text = txt_direccionAcomp.Text;
                txt_cedulaGar.Text = txt_cedulaAcomp.Text;
                txt_parentGar.Text = txt_parentescoAcomp.Text;
                txt_telfGar.Text = txt_telefonoAcomp.Text.Replace("-", string.Empty);
                txt_ciudadGar.Text = txt_ciudadAcomp.Text;
            }
            else
            {
                txt_nombreGar.Text = string.Empty;
                txt_dirGar.Text = string.Empty;
                txt_cedulaGar.Text = string.Empty;
                txt_parentGar.Text = string.Empty;
                txt_telfGar.Text = string.Empty;
                txt_ciudadGar.Text = string.Empty;
            }
        }

        private void txt_numeroatencion_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void txt_numeroatencion_TextChanged(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbSid_CheckedChanged(object sender, EventArgs e)
        {
            if (btnGuardar.Enabled == true)
            {
                if (rbSid.Checked == true)
                {
                    txt_cedula.Text = txt_historiaclinica.Text.ToString();
                    txt_cedula.ReadOnly = true;
                }
                else
                {
                    txt_cedula.Text = string.Empty;
                    txt_cedula.ReadOnly = false;
                }
            }
        }

        private void tabulador_DrawItem(object sender, DrawItemEventArgs e)
        {
            //try
            //{
            //    //This line of code will help you to change the apperance like size,name,style.
            //    Font f;
            //    //For background color
            //    Brush backBrush;
            //    //For forground color
            //    Brush foreBrush;

            //    //This construct will hell you to deside which tab page have current focus
            //    //to change the style.
            //    if (e.Index == this.tabulador.SelectedIndex)
            //    {
            //        //This line of code will help you to change the apperance like size,name,style.
            //        f = new Font(e.Font, FontStyle.Bold | FontStyle.Bold);
            //        f = new Font(e.Font, FontStyle.Bold);

            //        backBrush = new System.Drawing.SolidBrush(Color.LightGray);
            //        foreBrush = Brushes.Black;
            //        tabulador.SelectedTab.BackColor = Color.LightGray;
            //        tabulador.SelectedTab.ForeColor = Color.Black;
            //        registroCambios.ForeColor = Color.Black;
            //        gridAseguradoras.ForeColor = Color.Black;
            //        gridGarantias.ForeColor = Color.Black;
            //        gridAtenciones.ForeColor = Color.Black;
            //    }
            //    else
            //    {
            //        f = e.Font;
            //        backBrush = new SolidBrush(Color.Transparent);
            //        foreBrush = new SolidBrush(e.ForeColor);
            //        tabulador.SelectedTab.BackColor = Color.Transparent;
            //        tabulador.SelectedTab.ForeColor = Color.Black;
            //    }

            //    //To set the alignment of the caption.
            //    string tabName = this.tabulador.TabPages[e.Index].Text;
            //    StringFormat sf = new StringFormat();
            //    sf.Alignment = StringAlignment.Center;

            //    //Thsi will help you to fill the interior portion of
            //    //selected tabpage.
            //    e.Graphics.FillRectangle(backBrush, e.Bounds);
            //    Rectangle r = e.Bounds;
            //    r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height - 3);
            //    e.Graphics.DrawString(tabName, f, foreBrush, r, sf);

            //    sf.Dispose();
            //    if (e.Index == this.tabulador.SelectedIndex)
            //    {
            //        f.Dispose();
            //        backBrush.Dispose();
            //    }
            //    else
            //    {
            //        backBrush.Dispose();
            //        foreBrush.Dispose();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show(Ex.Message.ToString(), "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frm_ListaFormularios form = new frm_ListaFormularios(ultimaAtencion, pacienteActual, datosPacienteActual);
            form.ShowDialog();
            form.Dispose();
        }

        private void contratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ultimaAtencion != null)
                {
                    string nombreContrato;
                    string numPoliza = string.Empty;
                    string nomAseg = string.Empty;
                    string montoAseg = string.Empty;
                    string telfAseg = string.Empty;
                    string numContrato = "-";
                    string nomEmp = string.Empty;
                    string montoEmp = string.Empty;
                    string telfEmp = string.Empty;

                    if (ultimaAtencion.ATE_FACTURA_NOMBRE == "ACOMPAÑANTE")
                        nombreContrato = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                    else if (ultimaAtencion.ATE_FACTURA_NOMBRE == "GARANTE")
                        nombreContrato = ultimaAtencion.ATE_GARANTE_NOMBRE;
                    else if (ultimaAtencion.ATE_FACTURA_NOMBRE == "EMPRESA")
                    {
                        if (empresaPaciente != null)
                        {
                            nombreContrato = empresaPaciente.ASE_NOMBRE;
                        }
                        else
                        {
                            nombreContrato = datosPacienteActual.DAP_EMP_NOMBRE;
                        }
                    }
                    else
                        nombreContrato = pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO + " " + pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2;

                    if (detalleCategorias != null)
                    {
                        int codDetalle = Convert.ToInt32(gridAseguradoras.Rows[0].Cells["ADA_CODIGO"].Value);
                        ATENCION_DETALLE_CATEGORIAS dcat = detalleCategorias.FirstOrDefault(d => d.ADA_CODIGO == codDetalle);
                        CATEGORIAS_CONVENIOS cat = categorias.FirstOrDefault(c => c.EntityKey == dcat.CATEGORIAS_CONVENIOSReference.EntityKey);
                        ASEGURADORAS_EMPRESAS aseg = new ASEGURADORAS_EMPRESAS();
                        if (cat != null)
                            aseg = aseguradoras.FirstOrDefault(a => a.EntityKey == cat.ASEGURADORAS_EMPRESASReference.EntityKey);
                        //else

                        TIPO_EMPRESA tipo = new TIPO_EMPRESA();
                        if (aseg.ASE_CODIGO > 0)
                            tipoEmpresa.FirstOrDefault(t => t.EntityKey == aseg.TIPO_EMPRESAReference.EntityKey);

                        if (tipo.TE_CODIGO == 1)
                        {
                            if (dcat.ADA_CONTRATO != null)
                                numPoliza = dcat.ADA_CONTRATO;
                            nomAseg = aseg.ASE_NOMBRE;
                            montoAseg = dcat.ADA_MONTO_COBERTURA.ToString();
                            if (aseg.ASE_TELEFONO != null)
                                telfAseg = aseg.ASE_TELEFONO;
                        }
                        else if (tipo.TE_CODIGO == 2)
                        {
                            numContrato = dcat.ADA_CONTRATO;
                            nomEmp = aseg.ASE_NOMBRE;
                            montoEmp = dcat.ADA_MONTO_COBERTURA.ToString();
                            telfEmp = aseg.ASE_TELEFONO;
                        }
                    }

                    frmReportes form = new frmReportes();
                    form.reporte = "Contrato";
                    form.campo1 = ultimaAtencion.ATE_CODIGO.ToString();
                    form.campo2 = nombreContrato;
                    form.campo3 = numPoliza;
                    form.campo4 = nomAseg;
                    form.campo5 = montoAseg;
                    form.campo6 = telfAseg;
                    form.campo7 = numContrato;
                    form.campo8 = nomEmp;
                    form.campo9 = telfEmp;
                    form.campo10 = montoEmp;
                    form.Show();
                }
                else
                {
                    MessageBox.Show("No existe una atención", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_pais_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb_pais.SelectedIndex >= 0 && cargaciu == true)
                {
                    DIVISION_POLITICA pais = paises.FirstOrDefault(p => p.DIPO_CODIINEC == cmb_pais.SelectedValue.ToString());

                    cmb_ciudad.DataSource = null;
                    provincias = NegDivisionPolitica.RecuperarDivisionPolitica(pais.DIPO_CODIINEC).OrderBy(c => c.DIPO_NOMBRE).ToList();
                    cmb_ciudad.DataSource = provincias;
                    cmb_ciudad.ValueMember = "DIPO_CODIINEC";
                    cmb_ciudad.DisplayMember = "DIPO_NOMBRE";
                    cmb_ciudad.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmb_ciudad.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                    if (pacienteNuevo == true)
                    {
                        //if (pais.NACIONALIDAD != null && pais.NACIONALIDAD != string.Empty)
                        //    txt_nacionalidad.Text = pais.NACIONALIDAD;
                        //else
                        //    txt_nacionalidad.Text = string.Empty;

                        if (pais.DIPO_CODIINEC == AdmisionParametros.DefPais)
                        {
                            cmb_ciudad.SelectedValue = AdmisionParametros.DefProvincia;
                            txt_codPais.Text = AdmisionParametros.DefPais;
                            txt_codProvincia.Text = AdmisionParametros.DefProvincia;
                            txt_codCanton.Text = AdmisionParametros.DefCanton;
                            txt_nacionalidad.Text = "ECUATORIANO";
                        }
                        else
                        {
                            txt_codPais.Text = string.Empty;
                            txt_codProvincia.Text = string.Empty;
                            txt_codCanton.Text = string.Empty;
                        }
                    }

                }
                else
                {
                    cmb_ciudad.DataSource = null;
                    //formaPago = null;
                    //cboFiltroFormaPago.SelectedIndex = 0;
                    //cboFiltroFormaPago.Enabled = false;
                }
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message, "error");
            }
        }

        #endregion

        private void txt_nombreRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_telfRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_parentRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void rbCedula_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCedula.Checked == true)
                txt_cedula.MaxLength = 10;
        }

        private void rbPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPasaporte.Checked == true)
                txt_cedula.MaxLength = 13;
        }

        private void rbRuc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRuc.Checked == true)
                txt_cedula.MaxLength = 13;
        }

        private void txt_cedulaAcomp_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void chkDatosAc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void gridGarantias_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewRow fila = gridGarantias.Rows[e.RowIndex];

            if (fila.Cells["ADG_CODIGO"].Value == null)
            {
                fila.Cells["fecha"].Value = DateTime.Now.ToString();
                fila.Cells["fecha"].ReadOnly = true;
            }
        }

        private void txtCodMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnGuardar.Enabled == true)
            {
                if (e.KeyCode == Keys.F1 || e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
                {
                    frm_Ayudas ayuda = new frm_Ayudas(NegMedicos.medicosLista(), "MEDICOS", "CODIGO", "");
                    ayuda.campoPadre = txtCodMedico;
                    ayuda.ShowDialog();
                    if (ayuda.campoPadre.Text != string.Empty)
                        CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    cmbConvenioPago.Focus();
                }
            }
        }

        private void txtCodMedico_Leave(object sender, EventArgs e)
        {

        }

        private void txtCodMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Back && btnGuardar.Enabled == true)
            {
                txtCodMedico.Text = string.Empty;
            }
            if (e.KeyChar == (Char)Keys.Enter)
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCodMedico_TextChanged(object sender, EventArgs e)
        {
            if (txtCodMedico.Text.ToString() == string.Empty)
            {
                txtNombreMedico.Text = string.Empty;
                medico = null;
            }
        }

        private void form001ADMISIONYALTAEGRESOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportes frm = new frmReportes();                
                frm.campo1 = ultimaAtencion.ATE_CODIGO.ToString();
                frm.reporte = "rAdmision";
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void form001EGRESOHOSPITALARIOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ReportesHistoriaClinica reporteAdmisionE = new ReportesHistoriaClinica();
                reporteAdmisionE.EliminarDatosAdmisionEgreso();
                List<ReporteAdmisionEgreso> listaReportes = new List<ReporteAdmisionEgreso>();
                ReporteAdmisionEgreso reporte = new ReporteAdmisionEgreso();
                HC_EPICRISIS epicrisis = new HC_EPICRISIS();
                TIPO_EGRESO tipoE = new TIPO_EGRESO();
                List<HC_EPICRISIS_DIAGNOSTICO> listaDiag = new List<HC_EPICRISIS_DIAGNOSTICO>();
                List<DtoAtenciones> dataAtenciones = NegAtenciones.RecuperarAtencionesPaciente(pacienteActual.PAC_CODIGO);
                if (dataAtenciones.Count > 0)
                {
                    DtoAtenciones atenc = new DtoAtenciones();
                    for (int i = 0; i < dataAtenciones.Count; i++)
                    {
                        atenc = dataAtenciones.ElementAt(i);
                        reporte = new ReporteAdmisionEgreso();
                        epicrisis = NegEpicrisis.recuperarEpicrisisPorAtencion(atenc.ATE_CODIGO);
                        if (epicrisis != null)
                        {
                            listaDiag = NegEpicrisis.recuperarDiagnosticosEpicrisisEgreso(epicrisis.EPI_CODIGO);
                            reporte.FORM_FECHA_INGRESO = Convert.ToString(atenc.ATE_FECHA_INGRESO);
                            reporte.FORM_FECHA_EGRESO = Convert.ToString(epicrisis.EPI_FECHA_EGRESO);
                            reporte.FORM_DIAS = Convert.ToString((epicrisis.EPI_FECHA_EGRESO.Day - atenc.ATE_FECHA_INGRESO.Day) + 1);
                            //reporte.FORM_SERVICIO = NegTipoTratamiento.recuperaTipoTratamiento(NegAtenciones.RecuperarAtencionPorNumero(atenc.ATE_NUMERO_ATENCION).).ToString();
                            tipoE.EntityKey = (epicrisis.TIPO_EGRESOReference.EntityKey);
                            if (tipoE.TIE_CODIGO == 1)
                                reporte.FORM_ALTA = "X";
                            if (tipoE.TIE_CODIGO == 9)
                                reporte.FORM_MIERTE_MENOS = "X";
                            if (tipoE.TIE_CODIGO == 10)
                                reporte.FORM_MUERTE_MAS = "X";
                            for (int j = 0; j < listaDiag.Count; j++)
                            {
                                HC_EPICRISIS_DIAGNOSTICO diagnostico = new HC_EPICRISIS_DIAGNOSTICO();
                                diagnostico = listaDiag.ElementAt(j);
                                if (diagnostico.HED_TIPO == "I")
                                {
                                    reporte.FORM_DIAG_ING = diagnostico.HED_DESCRIPCION;
                                    reporte.FORM_DIAG_ING_CIE = diagnostico.CIE_CODIGO;
                                    if (diagnostico.HED_ESTADO == false)
                                        reporte.FORM_DIAG_ING_PRES = "X";
                                    else
                                        reporte.FORM_DIAG_ING_DEF = "X";
                                    j = listaDiag.Count;
                                }
                            }
                            for (int j = 0; j < listaDiag.Count; j++)
                            {
                                HC_EPICRISIS_DIAGNOSTICO diagnostico = new HC_EPICRISIS_DIAGNOSTICO();
                                diagnostico = listaDiag.ElementAt(j);
                                if (diagnostico.HED_TIPO == "E")
                                {
                                    reporte.FORM_DIAG_EGRE = diagnostico.HED_DESCRIPCION;
                                    reporte.FORM_DIAG_EGRE_CIE = diagnostico.CIE_CODIGO;
                                    if (diagnostico.HED_ESTADO == false)
                                        reporte.FORM_DIAG_EGRE_PRES = "X";
                                    else
                                        reporte.FORM_DIAG_EGRE_DEF = "X";
                                    j = listaDiag.Count;
                                }
                            }
                            reporte.FORM_TRAT_PROCED = epicrisis.EPI_TRATAMIENTO;
                            reporte.FORM_COD_RESP = Convert.ToString(epicrisis.ID_USUARIO);
                            reporteAdmisionE.ingresarAdmisionEgreso(reporte);
                        }
                    }


                    His.Formulario.frmReportes ventana = new His.Formulario.frmReportes(1, "admisionE");
                    ventana.Show();
                    ventana.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label63_Click(object sender, EventArgs e)
        {

        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            BuscarDivision("pais");
        }

        private void ultraButton2_Click(object sender, EventArgs e)
        {
            if (txt_pais.Text.ToString() == string.Empty)
                MessageBox.Show("Seleccione primero un pais");
            else
                BuscarDivision("provincia");
        }

        private void ultraButton3_Click(object sender, EventArgs e)
        {
            if (txt_provincia.Text.ToString() == string.Empty)
                MessageBox.Show("Seleccion primero una provincia");
            else
                BuscarDivision("canton");
        }

        private void ultraButton4_Click(object sender, EventArgs e)
        {
            if (txt_canton.Text.ToString() == string.Empty)
                MessageBox.Show("Seleccion primero un canton");
            else
                BuscarDivision("parroquia");
        }

        private void ultraButton5_Click(object sender, EventArgs e)
        {
            if (txt_parroquia.Text.ToString() == string.Empty)
                MessageBox.Show("Seleccion primero una parroquia");
            else
                BuscarDivision("sector");
        }

        private void ultraButton6_Click(object sender, EventArgs e)
        {
            frm_Ayudas ayuda = new frm_Ayudas(NegAseguradoras.ListaEmpresas(), "EMPRESAS", "RUC");
            ayuda.campoEspecial = txt_rucEmp;
            ayuda.colRetorno = "RUC";
            ayuda.ShowDialog();
            if (ayuda.campoEspecial.Text != string.Empty)
                CargarEmpresa(ayuda.campoEspecial.Text);
        }

        private void btnAyudaMedicos_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

            frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
            ayuda.campoPadre = txtCodMedico;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
                CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));

            ayuda.Dispose();
        }


        private void ayudaHabitaciones_Click(object sender, EventArgs e)
        {
            try
            {
                Sesion.codHabitacion = 0;
                if (cmb_tipoingreso.Text == "AMBULATORIO")
                {
                    Sesion.TipoBusquedaHabitacion = 1;
                }
                else
                {
                    Sesion.TipoBusquedaHabitacion = 0;
                }
                frmControlesWPF ayuda = new frmControlesWPF();
                ayuda.ShowDialog();
                ayuda.Dispose();

                if (Sesion.codHabitacion != 0)
                {
                    txt_habitacion.Text = Sesion.numHabitacion;
                    cmbTipoAtencion.Focus();
                }
            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ayudaNacionalidad_Click(object sender, EventArgs e)
        {
            frm_Ayudas ayuda = new frm_Ayudas(NegPais.ListaNacionalidades(), "NACIONALIDADES", "NACIONALIDAD");
            ayuda.campoPadre = txt_nacionalidad;
            //ayuda.colRetorno = "RUC";
            ayuda.ShowDialog();
        }

        private void groupBoxTipoDoc_Click(object sender, EventArgs e)
        {

        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void dtp_feCreacion_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dtp_fecnac_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cmb_ciudad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void txt_nacionalidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cb_etnia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void cb_gruposanguineo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxGenero_Click(object sender, EventArgs e)
        {

        }

        private void ultraGrid1_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {

        }

        private void txt_historiaclinica_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (pacienteNuevo == false)
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        frm_AyudaPacientes form = new frm_AyudaPacientes();
                        form.campoPadre = txt_historiaclinica;
                        form.ShowDialog();

                    }
                }
            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            try
            {
                if (pacienteNuevo == false)
                {
                    frm_AyudaPacientes form = new frm_AyudaPacientes();
                    form.campoPadre = txt_historiaclinica;
                    form.ShowDialog();
                    form.Dispose();

                    //btn_generar.Enabled = true;
                    btnGenerar.Enabled = true;
                }
            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cb_etnia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cb_gruposanguineo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }


        private void cmb_pais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_ciudad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_estadocivil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_ciudadano_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_tipoingreso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_formaLlegada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_tiporeferido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_tipoatencion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_tipopago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                txt_obPago.Focus();
            }
        }

        private void cb_seguros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cb_tipoGarantia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (atencionNueva == false && pacienteActual != null)
                {
                    string numatencion = gridAtenciones.ActiveRow.Cells["NUM_ATENCION"].Value.ToString();
                    ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(numatencion);
                    if (ultimaAtencion != null)
                    {
                        CargarAtencion();
                        txt_LugarFecha.Text= "QUITO," + " " + DateTime.Today;
                        dtIngreso.Text= gridAtenciones.ActiveRow.Cells["FECHA_INGRESO"].Value.ToString();
                        mktCedula.Text = txt_cedula.Text;
                        tabulador.Tabs["certificado"].Visible = true;
                        label84.Text = txt_numeroatencion.Text;
                        txt_MedicoCertificado.Text = txtNombreMedico.Text;
                        txt_MedicoCertificado.ReadOnly = false;
                        tabulador.SelectedTab = tabulador.Tabs[1];
                        //lx201
                        deshabilitarCamposAtencion();
                        deshabilitarCamposDireccion();
                        deshabilitarCamposPaciente();
                        btnActualizar.Enabled = false;
                        btnGuardar.Enabled = false;
                        //lx201

                        if (gridAtenciones.ActiveRow.Cells["FACTURA"].Value == null)
                            btnActualizar.Enabled = true;
                        /////////////////////////////////////
                    }
                    else
                    {
                        MessageBox.Show("Atencion con información incompleta");
                    }
                }
            }
            catch (System.Exception r)
            {
                MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_historiaclinica_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string historia = txt_historiaclinica.Text.ToString();               
                if (pacienteNuevo == false)
                {
                    if (Convert.ToBoolean(txt_historiaclinica.Tag) == true)
                    {
                        CargarPaciente(txt_historiaclinica.Text.ToString(),0);
                        txt_historiaclinica.Tag = false;
                    }
                }
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void frm_Admision_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
                aseguradoras = NegAseguradoras.ListaEmpresas();
                tipoTratamiento = NegTipoTratamiento.RecuperaTipoTratamiento();
                formasLlegada = NegAtencionesFormasLlegada.listaAtencionesFormasLlegada();
                estadoCivil = NegEstadoCivil.RecuperaEstadoCivil();
                paises = NegDivisionPolitica.listaDivisionPolitica("2");
                tipoCiudadano = NegTipoCiudadano.listaTiposCiudadano();
                tipoReferido = NegTipoReferido.listaTipoReferido();
                tipoGarantia = NegTipoGarantia.listaTipoGarantia();
                tipoEmpresa = NegAseguradoras.ListaTiposEmpresa();
                tipoSangre = NegGrupoSanguineo.ListaGrupoSanguineo();
                etnias = NegEtnias.ListaEtnias();
                tipoIngreso = NegTipoIngreso.ListaTipoIngreso();
                listaAnexos = NegAnexos.RecuperarListaAnexos(Convert.ToString(His.Parametros.AdmisionParametros.CodigoAnexoParentesco));

                CargarDatos();

                His.Parametros.ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                codigoTipoIngreso = Convert.ToInt16(archivo.IniReadValue("admision", "default"));
                if (codigoTipoIngreso > 0)
                {
                    cmb_tipoingreso.SelectedIndex = codigoTipoIngreso;
                    seleccionTipoIngreso = false;
                }
            }
            catch (System.Exception err) { MessageBox.Show(err.Message); }
        }

        private void txt_telfRef_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtRefDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["atencion"];
                SendKeys.SendWait("{TAB}");
            }
        }

        private void ubtnDatosIncompletos_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ubtnDatosIncompletos.Tag) == false)
            {
                ubtnDatosIncompletos.Tag = true;
                ubtnDatosIncompletos.Appearance.BackColor = Color.Red;
                ubtnDatosIncompletos.Appearance.BackColor2 = Color.GhostWhite;
            }
            else
            {
                ubtnDatosIncompletos.Tag = false;
                ubtnDatosIncompletos.Appearance.BackColor = Color.LightBlue;
                ubtnDatosIncompletos.Appearance.BackColor2 = Color.SteelBlue;
            }
        }

        private void uBtnAddAcompaniante_Click(object sender, EventArgs e)
        {
            txt_nombreAcomp.Text = txt_nombreRef.Text;
            txt_telefonoAcomp.Text = txt_telfRef.Text;
            txt_direccionAcomp.Text = txtRefDireccion.Text;
            txt_parentescoAcomp.Text = txt_parentRef.Text;
            txt_ciudadAcomp.Text = txt_canton.Text;
        }

        private void uBtnAddAcompaniante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_historiaclinica_Leave(object sender, EventArgs e)
        {
            try
            {
                PACIENTES pacienteExiste = NegPacientes.RecuperarPacienteID(txt_historiaclinica.Text);
                if (pacienteExiste != null)
                {
                    if (pacienteExiste.PAC_CODIGO != pacienteActual.PAC_CODIGO)
                    {
                        MessageBox.Show("El número de historia clinica ya existe, por favor ingrese otro número ", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txt_historiaclinica.Text = "";
                    }
                }
                else
                {
                    if (pacienteActual != null)
                    {
                        DialogResult resultado;
                        resultado = MessageBox.Show("Se ha modificado el número de historia clinica, desea proseguir con los cambios", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (resultado == DialogResult.Cancel)
                        {
                            txt_historiaclinica.Text = pacienteActual.PAC_HISTORIA_CLINICA;
                        }
                    }
                }
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void cmb_pais_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtPersonaEntregaPac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                if (cb_seguros.Enabled == true)
                    SendKeys.SendWait("{TAB}");
                else
                    cb_tipoGarantia.Focus();
            }
        }


        private void txtReferidoDe_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void uTabFormaPago_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void cmb_tipopago_SelectedValueChanged(object sender, EventArgs e)
        {
            TIPO_EMPRESA tfp = (TIPO_EMPRESA)cmbConvenioPago.SelectedItem;

            if (btnGuardar.Enabled == true)
            {
                cb_seguros.DataSource = NegCategorias.ListaCategorias(tfp);
                cb_seguros.ValueMember = "CAT_CODIGO";
                cb_seguros.DisplayMember = "CAT_NOMBRE";
                cb_seguros.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_seguros.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                btnAddAseg.Enabled = true;
                cb_seguros.Enabled = true;
                //if (tfp.TIF_CODIGO == 3 || tfp.TIF_CODIGO == 4)
                //{
                //    btnAddAseg.Enabled = true;
                //    cb_seguros.Enabled = true;
                //}
                //else
                //{
                //    btnAddAseg.Enabled = false;
                //    cb_seguros.Enabled = false;
                //}
            }
        }

        private void cmb_tiporeferido_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_cedula_TextChanged(object sender, EventArgs e)
        {

        }

        private void chb_PreAdmision_CheckedChanged(object sender, EventArgs e)
        {
        }

        private Boolean validarFechasPreadmison()
        {
            int horas = AdmisionParametros.NumeroHoras;
            Boolean val = false;
            TimeSpan ts;
            if (preAtencioActual == null)
            {
                ts = dtpPreAdmision.Value - DateTime.Now;
                if (ts.Hours >= (AdmisionParametros.NumeroHoras - 1))
                {
                    if (ts.Minutes >= 30)
                        val = true;
                }
            }
            else
            {
                ts = dtpPreAdmision.Value - Convert.ToDateTime(preAtencioActual.PREA_FECHA_ADMISON);
                if (ts.Hours >= (AdmisionParametros.NumeroHoras - 2))
                {
                    if (ts.Minutes >= 01)
                        val = true;
                }
            }
            return val;
        }

        private void cmb_tipoingreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_tipoingreso.Enabled = true;
            if (cmb_tipoingreso.SelectedIndex == 5)
            {
                ayudaHabitaciones.Enabled = false;
                txt_habitacion.Text = "PREA";
                txt_habitacion.Enabled = true;
                dtpPreAdmision.Enabled = true;
                chb_PreAdmision.Checked = true;
                chb_PreAdmision.Enabled = false;
                if (atencionNueva == true)
                {
                    dtpPreAdmision.Value = DateTime.Now;
                }
            }
            else
            {
                if (cmb_tipoingreso.SelectedIndex == 0)
                {
                    //ayudaHabitaciones.Enabled = false;
                    //txt_habitacion.Text = "AMB";
                    //txt_habitacion.Enabled = false;                    
                }
                else
                {
                    if (cmb_tipoingreso.SelectedIndex == 2)
                    {
                        ayudaHabitaciones.Enabled = false;
                        cmb_tipoingreso.Enabled = false;
                    }
                    else
                    {
                        ayudaHabitaciones.Enabled = true;
                        txt_habitacion.Enabled = true;
                        txt_habitacion.Text = "";
                        dtpPreAdmision.Enabled = false;
                        dtpPreAdmision.Value = DateTime.Now;
                        chb_PreAdmision.Checked = false;
                    }

                }
            }
        }

        private void cmbConvenioPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gridAseguradoras_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                if (convenioA != true)
                {
                    if (e.Row.Cells[5].Value.ToString() != "")
                    {
                        if (!NegAtencionDetalleCategorias.eliminarAtencionDetalleCategorias(Convert.ToInt64(e.Row.Cells[5].Value)))
                            MessageBox.Show("No se puede eliminar el convenio", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show("Registro eliminado exitosamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information); // Cambio el mensage y el tipo de mensage ya que el mensaje no se genera por un error / Giovanny Tapia /27/082012
                    }
                }
                else
                {
                    gridAseguradoras.CurrentCell.Dispose();
                }

            }
            catch (System.Exception err) { MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {


        }

        private void contratoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text != "" && txt_numeroatencion.Text != "")
            {
                CrearCarpetas_Srvidor("Contrato");
            }
        }

        private void fOO1ADMISIÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text != "" && txt_numeroatencion.Text != "")
            {
                CrearCarpetas_Srvidor("rAdmision");
            }
        }

        private void gridAseguradoras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (gridAseguradoras.CurrentRow != null)
                {
                    int index = gridAseguradoras.CurrentRow.Index;
                    int columna = gridAseguradoras.SelectedCells[0].ColumnIndex;
                    //if (gridAseguradoras.Rows[index].Cells[0].Value.Equals(AdmisionParametros.CodigoConvenioIess))
                    if (NegAtenciones.RPIS(Convert.ToInt32(gridAseguradoras.Rows[index].Cells[0].Value))) //lx202005
                    {
                        if (columna == 7)
                        {
                            frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(His.Parametros.AdmisionParametros.CodigoAnexoTipoSeguro);
                            frm.ShowDialog();
                            frm.Dispose();
                            anexoIess = frm.anexos;
                            if (anexoIess != null)
                            {
                                gridAseguradoras.Rows[index].Cells[columna - 1].Value = anexoIess.ANI_CODIGO;
                                gridAseguradoras.Rows[index].Cells[columna].Value = anexoIess.ANI_DESCRIPCION;
                                gridAseguradoras.Rows[index].Cells[2].ReadOnly = true;
                            }
                        }
                        else
                        {
                            if (columna == 9)
                            {
                                frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(His.Parametros.AdmisionParametros.CodigoAnexoDependencia);
                                frm.ShowDialog();
                                frm.Dispose();
                                anexoIess = frm.anexos;
                                if (anexoIess != null)
                                {
                                    gridAseguradoras.Rows[index].Cells[columna - 1].Value = anexoIess.ANI_CODIGO;
                                    gridAseguradoras.Rows[index].Cells[columna].Value = anexoIess.ANI_DESCRIPCION;
                                    gridAseguradoras.Rows[index].Cells[2].ReadOnly = true;
                                }
                            }
                            else
                            {
                                if (columna == 2)
                                {
                                    frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(His.Parametros.AdmisionParametros.CodigoAnexoDerivacion);
                                    frm.ShowDialog();
                                    anexoIess = frm.anexos;

                                    if (anexoIess != null)
                                    {
                                        gridAseguradoras.Rows[index].Cells[columna].Value = anexoIess.ANI_COD_PRO + "-";
                                    }
                                }
                            }


                        }
                        if (columna == 11)
                        {
                            frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(277);/*ANEXOS DE ESPECIALIDADES MEDICAS*/
                            frm.ShowDialog();
                            anexoIess = frm.anexos;

                            if (anexoIess != null)
                            {
                                gridAseguradoras.Rows[index].Cells[10].Value = anexoIess.ANI_CODIGO;
                                gridAseguradoras.Rows[index].Cells[columna].Value = anexoIess.ANI_DESCRIPCION;
                            }
                        }
                    }
                }
            }

        }



        private void txt_CedulaTitular_Leave(object sender, EventArgs e)
        {
            if (txt_CedulaTitular.Text.ToString() != string.Empty)
                if (NegValidaciones.esCedulaValida(txt_CedulaTitular.Text) != true)
                {
                    //MessageBox.Show("Cédula Incorrecta");
                    MessageBox.Show("Identificación No Cumple con Formato", "HIS3000", MessageBoxButtons.OK);
                    txt_CedulaTitular.Focus();
                }
        }

        private void txt_CedulaTitular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_TelefonoTitular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_TelefonoTitular_Leave(object sender, EventArgs e)
        {
            if (txt_TelefonoTitular.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txt_TelefonoTitular.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    MessageBox.Show("Numero de teléfono incorrecto");
                    txt_TelefonoTitular.Focus();
                }
            }
        }

        private void chkDatosAcSeg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void chkDatosAcSeg_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDatosAcSeg.Checked == true)
            {
                chkDatosPac.Checked = false;
                txt_nombreTitular.Text = txt_nombreAcomp.Text;
                txt_DireccionTitular.Text = txt_direccionAcomp.Text;
                txt_CedulaTitular.Text = txt_cedulaAcomp.Text;
                cmb_Parentesco.SelectedIndex = 3;
                txt_TelefonoTitular.Text = txt_telefonoAcomp.Text.Replace("-", string.Empty);
                txt_CiudadTitular.Text = txt_ciudadAcomp.Text;

            }
            else
            {
                txt_nombreTitular.Text = string.Empty;
                txt_DireccionTitular.Text = string.Empty;
                txt_CedulaTitular.Text = string.Empty;
                cmb_Parentesco.SelectedIndex = 0;
                txt_TelefonoTitular.Text = string.Empty;
                txt_CedulaTitular.Text = string.Empty;
            }
        }

        private void chkDatosPac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void chkDatosPac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDatosPac.Checked == true)
            {
                chkDatosAcSeg.Checked = false;
                txt_nombreTitular.Text = txt_apellido1.Text.Trim() + " " + txt_apellido2.Text.Trim() + " " +
                                         txt_nombre1.Text.Trim() + " " + txt_nombre2.Text.Trim();
                txt_DireccionTitular.Text = txt_direccion.Text;
                txt_CedulaTitular.Text = txt_cedula.Text;
                cmb_Parentesco.SelectedIndex = 3;
                txt_TelefonoTitular.Text = txt_telefono1.Text.Replace("-", string.Empty);
                txt_CiudadTitular.Text = txt_canton.Text;
            }
            else
            {
                txt_nombreTitular.Text = string.Empty;
                txt_DireccionTitular.Text = string.Empty;
                txt_CedulaTitular.Text = string.Empty;
                cmb_Parentesco.SelectedIndex = 0;
                txt_TelefonoTitular.Text = string.Empty;
            }
            
        }


        /// <summary>
        /// Método que me permite imprimir el Reporte del Certificado Médico ingresado en Pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        
        private void btnImprimirCertificado_Click(object sender, EventArgs e)
        {
            Conversion c = new Conversion();
            string ingreso;
            string egreso;
            string desde;
            string hasta;
            int resultado = DateTime.Compare(dtIngreso.Value, dtEgreso.Value);
            int resultado2 = DateTime.Compare(dtDesde.Value, dtHasta.Value);
            if (txt_MedicoCertificado.Text != "")
                if (resultado2 <= 0)
                    if (txt_diagnosticoMedico.Text != "")
                        if (resultado <= 0)
                        {
                            ingreso = dtIngreso.Value.ToString();
                            egreso = dtEgreso.Value.ToString();
                            desde = dtDesde.Value.ToString();
                            hasta = dtHasta.Value.ToString();
                            txt_LugarFecha.Text = "";
                            ImpresionCertificado impresion = new ImpresionCertificado();
                            CertificadoMedico certificadoMedico = new CertificadoMedico();
                            certificadoMedico.paciente = txt_apellido1.Text + " " + txt_apellido2.Text + " " + txt_nombre1.Text + " " + txt_nombre2.Text;
                            certificadoMedico.fecha = "D.M. QUITO. " + dtFechayLugar.Text;
                            certificadoMedico.cedula = mktCedula.Text;
                            certificadoMedico.edad = mkt_Años.Text;
                            certificadoMedico.diagnostico = txt_diagnosticoMedico.Text;
                            certificadoMedico.ingreso = ingreso.Substring(0, 2) + " (" + c.enletras(ingreso.Substring(0, 2)) + ") de " + ingreso.Substring(3, 2) + " (" + c.ElMes(ingreso.Substring(3, 2)) + ") del " + ingreso.Substring(6, 4);
                            certificadoMedico.egreso = egreso.Substring(0, 2) + " (" + c.enletras(egreso.Substring(0, 2)) + ") de " + egreso.Substring(3, 2) + " (" + c.ElMes(egreso.Substring(3, 2)) + ") del " + egreso.Substring(6, 4);
                            certificadoMedico.desde = desde.Substring(0, 2) + " (" + c.enletras(desde.Substring(0, 2)) + ") de " + desde.Substring(3, 2) + " (" + c.ElMes(desde.Substring(3, 2)) + ") del " + desde.Substring(6, 4);
                            certificadoMedico.hasta = hasta.Substring(0, 2) + " (" + c.enletras(hasta.Substring(0, 2)) + ") de " + hasta.Substring(3, 2) + " (" + c.ElMes(hasta.Substring(3, 2)) + ") del " + hasta.Substring(6, 4);
                            certificadoMedico.medico = txt_MedicoCertificado.Text;
                            certificadoMedico.especialidad = txt_MedicoEspCertificado.Text;
                            certificadoMedico.atencion = label84.Text;
                            certificadoMedico.hcu = txt_historiaclinica.Text;
                            impresion.certificadoMedico.Add(certificadoMedico);
                            impresion.Show();
                        }
                        else
                            MessageBox.Show("NO SE PUEDE EMITIR UN CERTIFICADO CON LAS FECHAS DE INGRESO Y DESDE DE ESTA FROMA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("SE NECESITA UN DIAGNOSTICO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("NO SE PUEDE EMITIR UN CERTIFICADO CON LAS FECHAS DESDE Y HASTA DE ESTA FROMA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("SE NECESITA MEDICO RESPONSABLE", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txt_LugarFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void mktCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void mkt_Años_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_diagnosticoMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_ingresoMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_egresoMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_ReposoMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_MedicoCertificado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_MedicoEspCertificado_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnHabitaciones_Click(object sender, EventArgs e)
        {
            //HABITACIONES hab = new HABITACIONES();
            //hab = NegHabitaciones.RecuperaHabitacionPorNumero(txt_habitacion.Text);
            ////FrmRevertirSalida form = new FrmRevertirSalida(hab,ultimaAtencion);
            ////form.Show();
            try
            {
                //string numatencion = gridAtenciones.ActiveRow.Cells["ATE_CODIGO"].Value.ToString();
                //ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(numatencion);

                var ventana = new FrmRevertirSalida { Atencion = ultimaAtencion };
                ventana.Show();
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmReIngresoHabitacion_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (gridAtenciones.ActiveRow != null)
            //    {
            //        string numatencion = gridAtenciones.ActiveRow.Cells["ATE_CODIGO"].Value.ToString();
            //        ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(numatencion);
            //        var ventana = new FrmRevertirSalida {Atencion = ultimaAtencion};
            //        ventana.Show();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Por favor seleccione una habitación");
            //    }
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //}


        }

        private void tabulador_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void gridAseguradoras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridAseguradoras.CurrentRow != null)
                {
                    int index = gridAseguradoras.CurrentRow.Index;
                    int columna = gridAseguradoras.SelectedCells[0].ColumnIndex;
                    //if (gridAseguradoras.Rows[index].Cells[0].Value.Equals(AdmisionParametros.CodigoConvenioIess)) //lx202005
                    if (NegAtenciones.RPIS(Convert.ToInt32(gridAseguradoras.Rows[index].Cells[0].Value))) //lx202005
                    {
                        if (columna == 2)
                        {
                            gridAseguradoras.Rows[index].Cells[columna].ReadOnly = false;
                        }
                        else
                        {
                            gridAseguradoras.Rows[index].Cells[2].ReadOnly = true;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void gridAtenciones_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void frm_Admision_FormClosing(object sender, FormClosingEventArgs e)
        {

            NegValidaciones.alzheimer();

        }

        private void txtNombreMedico_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkDiscapacidad_CheckedChanged(object sender, EventArgs e)
        {            
            if (chkDiscapacidad.Checked == true)
            {
                txtIdDiscapacidad.Enabled = true;
                txtIdDiscapacidad.Focus();
            }
            else
            {
                txtIdDiscapacidad.Enabled = false;                
            }
        }



        private void btnOtroSeguro_Click(object sender, EventArgs e)
        {
            DataGridViewRow dt = new DataGridViewRow();
            dt.CreateCells(dgvOtroSeguro);
            TIPO_GARANTIA aseg = (TIPO_GARANTIA)cb_tipoGarantia.SelectedItem;

            dt.Cells[0].Value = "";
            dt.Cells[1].Value = "";
            dt.Cells[2].Value = "";
            dt.Cells[3].Value = "";

            dgvOtroSeguro.Rows.Add(dt);
        }

        private void dgvOtroSeguro_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgvOtroSeguro_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                int index = dgvOtroSeguro.CurrentRow.Index;
                int columna = dgvOtroSeguro.SelectedCells[0].ColumnIndex;

                if (columna == 1)
                {

                    frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(278);/*ANEXOS DE ESPECIALIDADES MEDICAS*/
                    frm.ShowDialog();
                    anexoIess = frm.anexos;

                    if (anexoIess != null)
                    {
                        dgvOtroSeguro.Rows[index].Cells[0].Value = anexoIess.ANI_CODIGO;
                        dgvOtroSeguro.Rows[index].Cells[1].Value = anexoIess.ANI_DESCRIPCION;
                        dgvOtroSeguro.Rows[index].Cells[2].Value = anexoIess.ANI_DESCRIPCION;

                        if (anexoIess.ANI_DESCRIPCION != "OTROS")
                        {
                            dgvOtroSeguro.Rows[index].Cells[2].ReadOnly = true;
                        }
                    }

                }

            }
        }

        private void btnAgrgarDerivado_Click(object sender, EventArgs e)
        {
            DataGridViewRow dt = new DataGridViewRow();
            dt.CreateCells(dgvDerivado);
            TIPO_GARANTIA aseg = (TIPO_GARANTIA)cb_tipoGarantia.SelectedItem;

            dt.Cells[0].Value = "";
            dt.Cells[1].Value = "";
            dt.Cells[2].Value = "";
            dt.Cells[3].Value = "";
            dt.Cells[4].Value = "";
            dt.Cells[5].Value = "";

            dgvDerivado.Rows.Add(dt);
        }

        private void dgvDerivado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                int index = dgvDerivado.CurrentRow.Index;
                int columna = dgvDerivado.SelectedCells[0].ColumnIndex;

                if (columna == 1)
                {

                    frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(279);/*ANEXOS DE ESPECIALIDADES MEDICAS*/
                    frm.ShowDialog();
                    anexoIess = frm.anexos;

                    if (anexoIess != null)
                    {
                        dgvDerivado.Rows[index].Cells[0].Value = anexoIess.ANI_CODIGO;
                        dgvDerivado.Rows[index].Cells[1].Value = anexoIess.ANI_DESCRIPCION;

                        if (anexoIess.ANI_DESCRIPCION != "Otros")
                        {
                            dgvDerivado.Rows[index].Cells[5].ReadOnly = true;
                        }
                        else
                        {
                            dgvDerivado.Rows[index].Cells[5].ReadOnly = false;
                        }
                    }

                }

                if (columna == 3)
                {
                    frm_AyudaCatalogoSubNivel frm = new frm_AyudaCatalogoSubNivel(278);/*ANEXOS DE ESPECIALIDADES MEDICAS*/
                    frm.ShowDialog();
                    anexoIess = frm.anexos;

                    if (anexoIess != null)
                    {
                        dgvDerivado.Rows[index].Cells[2].Value = anexoIess.ANI_CODIGO;
                        dgvDerivado.Rows[index].Cells[3].Value = anexoIess.ANI_DESCRIPCION;
                    }
                }
            }
        }

        private void gridAseguradoras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan 
                    e.Handled = true;
                }
        }

        private void txtP_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(sender, e);
            SaltoEnter(sender, e);
        }

        private void txtA_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(sender, e);
            SaltoEnter(sender, e);
        }

        private void txtC_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(sender, e);
            SaltoEnter(sender, e);
        }

        private void txtHV_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(sender, e);
            SaltoEnter(sender, e);
        }

        private void txtMenarquia_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(sender, e);
            SaltoEnter(sender, e);
        }

        private void LimpiarGinecologia()
        {
            txtG.Text = "";
            txtP.Text = "";
            txtA.Text = "";
            txtC.Text = "";
            txtHV.Text = "";
            txtMenarquia.Text = "";
            txtCiclos.Text = "";
            txtGO.Text = "";
            txtDIU.Text = "";
            txtOM.Text = "";
            txtCV.Text = "";
            txtAPP.Text = "";
            txtAPF.Text = "";
            txtGS.Text = "";
            txtOperaciones.Text = "";
            txtRecomendado.Text = "";
        }
        private void SaltoEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCiclos_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void dateFUM_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtGO_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtDIU_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtCV_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtAPP_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtAPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtGS_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtOperaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtRecomendado_KeyPress(object sender, KeyPressEventArgs e)
        {
            SaltoEnter(sender, e);
        }

        private void txtG_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(sender, e);
            SaltoEnter(sender, e);
        }

        private void cb_seguros_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void form001GARANTIASPACIENTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                DataTable ds1 = new DataTable();
                dtsAdmision ds2 = new dtsAdmision();
                DataRow dr2;
                ds1 = NegAtencionDetalleGarantias.SPRecuperaGarantia(ultimaAtencion.ATE_CODIGO);
                if (ds1 != null && ds1.Rows.Count > 0)
                {
                    DataTable ds3 = new DataTable();

                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        dr2 = ds2.Tables["Garantias"].NewRow();

                        dr2["NombrePaciente"] = txt_nombre1.Text + "" + txt_nombre2.Text + " " +
                                                txt_apellido1.Text + " " + txt_apellido2.Text;
                        dr2["FechaGarantia"] = dr1["FECHA"];
                        dr2["Cajero"] = His.Entidades.Clases.Sesion.nomUsuario;
                        dr2["TipoGarantia"] = dr1["TG_NOMBRE"];
                        dr2["ValorGarantia"] = dr1["ADG_VALOR"];
                        dr2["DescripcionGarantia"] = dr1["ADG_DESCRIPCION"];
                        dr2["NumDocumentoGarantia"] = dr1["ADG_DOCUMENTO"];
                        dr2["CedulaPaciente"] = txt_cedula.Text;
                        dr2["CedulaGarante"] = txt_cedulaGar.Text;
                        dr2["NombreGarante"] = txt_nombreGar.Text;
                        ds2.Tables["Garantias"].Rows.Add(dr2);
                    }
                    MessageBox.Show("SE IMPRIMIRA DOCUMENTO DE GARANTIA(S).", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    Garantias crgarantia = new Garantias();
                    crgarantia.SetDataSource(ds2);
                    CrystalDecisions.Windows.Forms.CrystalReportViewer vista = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                    vista.ReportSource = crgarantia;
                    vista.PrintReport();
                }
                else
                    MessageBox.Show("PACIENTE NO CUENTA CON GRANTIA(S)", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbn_h_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void rbn_m_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbTipoAtencion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                txtReferidoDe.Focus();
            }
        }

        private void txtReferidoDe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                txtCodMedico.Focus();
            }
        }

        private void txt_obPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                cmbAccidente.Focus();
            }
        }

        private void cmbAccidente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                cb_seguros.Focus();
            }

        }

        private void btnAddAseg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                btnAddAseg.PerformClick();
            }
        }

        private void txt_CiudadTitular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                tabulador2.SelectedTab = tabulador2.Tabs["garante"];
                e.Handled = true;
                txt_nombreGar.Focus();
                //SendKeys.SendWait("{TAB}");
            }
        }

        private void txtIdDiscapacidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                if (MessageBox.Show("DESEA GUARDAR LA ADMISIÓN", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnGuardar.PerformClick();
                }
            }

        } 

        private void txt_DireccionTitular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_Parentesco.Focus();
            }
        }
                                       
        private void cmb_Parentesco_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txt_nombreTitular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_CedulaTitular.Focus();
            }
        }

        private void txt_CedulaTitular_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_TelefonoTitular.Focus();
            }
        }

        private void cmb_Parentesco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                txt_CiudadTitular.Focus();
            }
        }

        private void chkDiscapacidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                if (chkDiscapacidad.Checked == false)
                {
                    if (MessageBox.Show("DESEA GUARDAR LA ADMISIÓN", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        btnGuardar.PerformClick();
                    }
                }
            }
        }

        private void btn_AyudaCM_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

            frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
            ayuda.campoPadre = txtCodMedico;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
                CargarMedicoCert(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));

            ayuda.Dispose();
        }

        private void mktCedula_Leave(object sender, EventArgs e)
        {
            if (NegValidaciones.esCedulaValida(mktCedula.Text) != true)
            {
                MessageBox.Show("Cédula Incorrecta");
                mktCedula.Focus();
            }
        }

        private void tabulador2_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtPorcentageDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtPorcentageDA_Leave(object sender, EventArgs e)
        {
            if (txtPorcentageDA.Text.Trim() == "")
                txtPorcentageDA.Text = "0";
        }

        private void btnGuardar_EnabledChanged(object sender, EventArgs e)
        {
            if (btnGuardar.Enabled)
            {
                habilitarAyudas();
                habilitarCamposAtencion();
                habilitarCamposDireccion();
                habilitarCamposPaciente();
                habilitarCamposPreAtencion();
            }else
            {
                deshabilitarAyudas();
                deshabilitarCamposAtencion();
                deshabilitarCamposDireccion();
                deshabilitarCamposPaciente();
                deshabilitarCamposPreAtencion();
            }
        }

        private void txt_diagnostico_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cmb_tipoingreso_SelectedValueChanged(object sender, EventArgs e)
        {
            cargar_cbotiposatenciones();
        }

        private void mnuEncuesta_Click(object sender, EventArgs e)
        {
            try
            {
                if (ultimaAtencion != null)
                {
                    
                    frmReportes form = new frmReportes();
                    form.reporte = "rEncuesta";
                    form.campo1 = ultimaAtencion.ATE_CODIGO.ToString();
                    form.campo2 = "";
                    form.campo3 = "";
                    form.campo4 = "";
                    form.campo5 = "";
                    form.campo6 = "";
                    form.campo7 = "";
                    form.campo8 = "";
                    form.campo9 = "";
                    form.campo10 = "";
                    form.Show();
                }
                else
                {
                    MessageBox.Show("No existe una atención", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtReferidoDe_TextChanged(object sender, EventArgs e)
        {

        }

        private void get_formasPagoTC()
        {
           
        }

        private void gridGarantias_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void gridGarantias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                
                if (gridGarantias.CurrentRow != null)
                {
                    int index = gridGarantias.CurrentRow.Index;
                    int columna = gridGarantias.SelectedCells[0].ColumnIndex;
                    if (Convert.ToString(gridGarantias.Rows[index].Cells[0].Value).Contains("VOUCHER"))
                    {
                        if (columna == 8)
                        {
                            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("TARJETAS");
                            ayuda.ShowDialog();
                            if (ayuda.codigo != string.Empty)
                            {
                                gridGarantias.Rows[index].Cells[columna].Value = ayuda.producto;
                            }
                        }
                    }
                }
            }
        }

        private void gridGarantias_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (gridGarantias.CurrentRow != null)
            {
                int index = gridGarantias.CurrentRow.Index;
                int columna = gridGarantias.SelectedCells[0].ColumnIndex;
                if (Convert.ToString(gridGarantias.Rows[index].Cells[1].Value).Contains("VOUCHER"))
                {
                    if (columna == 8)
                    {
                        frm_ImagenAyuda ayuda = new frm_ImagenAyuda("TARJETAS");
                        ayuda.ShowDialog();
                        if (ayuda.codigo != string.Empty)
                        {
                            gridGarantias.Rows[index].Cells[columna].Value = ayuda.producto;
                        }
                    }
                }
            }
        }
    }
    #endregion
}
