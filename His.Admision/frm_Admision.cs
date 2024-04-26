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
using System.IO;
using Infragistics.Win.UltraWinTabControl;
using System.Net;
using System.Threading;

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
        List<NACIONALIDAD_ETNIA> netnias = new List<NACIONALIDAD_ETNIA>();
        List<TIPO_INGRESO> tipoIngreso = new List<TIPO_INGRESO>();
        List<ANEXOS_IESS> listaAnexos;
        PREADMISION preadmision = new PREADMISION();
        int atencionReal = 0;
        int consultaExterna = 0;
        int cancelar = 0;
        bool primera = true;
        public bool ConsultaExterna = false;
        Int64 AtencionSubsecuente = 0;

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
        public bool pacienteNuevo2 = false;
        public bool atencionNueva = false;
        public bool bandCategorias = false;
        public bool bandGarantias = false;
        public bool seleccionTipoIngreso = true;
        public bool fallecido = false;
        public bool modificar = false;
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

        public bool consultas = false;
        #region Cargar Datos

        private void CargarDatos()
        {
            try
            {
                btnGenerar.Image = Archivo.imgBtnTools;
                //toolStripNuevo.Image = Archivo.imgBtnAdd2;
                //btnActualizar.Image = Archivo.imgBtnRestart;
                //btnEliminar.Image = Archivo.imgDelete;
                //btnGuardar.Image = Archivo.imgBtnFloppy;
                //btnCancelar.Image = Archivo.imgBtnStop;
                //btnCerrar.Image = Archivo.imgBtnSalir32;
                btnNuevo.Image = Archivo.nuevoPaciente;
                btnFormularios.Image = Archivo.iconoFormulario;
                btnNewAtencion.Image = Archivo.emergency;
                //btnImprimir.Image = Archivo.imgBtnImprimir32;
                btnAddAseg.Image = Archivo.imgBtnAdd;
                btnOtroSeguro.Image = Archivo.imgBtnAdd;
                btnAddGar.Image = Archivo.imgBtnAdd;
                uBtnAddAcompaniante.Appearance.Image = Archivo.imgBtnAdd;
                btnHabitaciones.Image = Archivo.imgBtnAdd;
                btnfacturaDatos.Appearance.Image = Archivo.imgBtnAdd;


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
                cmb_tiporeferido.SelectedValue = 3;

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
                cmb_estadocivil.SelectedIndex = 1;

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

                cb_netnica.DataSource = netnias;
                cb_netnica.ValueMember = "NE_CODIGO";
                cb_netnica.DisplayMember = "NE_DESCRIPCION";
                cb_netnica.SelectedIndex = -1;


                cb_pueblo.DataSource = netnias;
                cb_pueblo.ValueMember = "NE_CODIGO";
                cb_pueblo.DisplayMember = "NE_DESCRIPCION";
                cb_pueblo.SelectedIndex = -1;


                cmb_tipoingreso.DataSource = tipoIngreso;
                cmb_tipoingreso.ValueMember = "TIP_CODIGO";
                cmb_tipoingreso.DisplayMember = "TIP_DESCRIPCION";
                cmb_tipoingreso.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb_tipoingreso.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                //tm_fechaingreso.Start();

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

                //SE COMENTA PARA QUE PUEDAN INGRESAR MANUALMENTE POR EL USUARIO.
                //cb_personaFactura.SelectedItem = "PACIENTE";

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
                //Aqui

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
                    else if (pacienteActual.PAC_TIPO_IDENTIFICACION == "N")
                        rbRn.Checked = true;
                    else if (pacienteActual.PAC_TIPO_IDENTIFICACION == "R")
                        rbtCarnet.Checked = true;
                    else if (pacienteActual.PAC_TIPO_IDENTIFICACION == "E")
                        rbtCedulaExt.Checked = true;
                    else
                        rbSid.Checked = true;

                    DIVISION_POLITICA provincia = NegDivisionPolitica.DivisionPolitica(pacienteActual.DIPO_CODIINEC);

                    if (provincia.DIPO_DIPO_CODIINEC == "57")
                    {
                        cmb_pais.SelectedItem = paises.FirstOrDefault(p => p.DIPO_CODIINEC == provincia.DIPO_DIPO_CODIINEC);
                        cmb_ciudad.SelectedItem = provincias.FirstOrDefault(c => c.DIPO_CODIINEC == provincia.DIPO_CODIINEC);
                    }

                    //if (provincia.DIPO_CODIINEC == "17")
                    //{
                    //    cmb_pais.SelectedItem = paises.FirstOrDefault(p => p.DIPO_CODIINEC == provincia.DIPO_DIPO_CODIINEC);
                    //    cmb_ciudad.SelectedItem = provincias.FirstOrDefault(c => c.DIPO_CODIINEC == provincia.DIPO_CODIINEC);
                    //}
                    else
                    {
                        cmb_pais.SelectedItem = paises.FirstOrDefault(p => p.DIPO_CODIINEC == provincia.DIPO_CODIINEC);
                        cmb_ciudad.Text = cmb_pais.Text;
                    }

                    txt_cedula.Text = pacienteActual.PAC_IDENTIFICACION;
                    dtp_fecnac.Value = pacienteActual.PAC_FECHA_NACIMIENTO.Value.Date;
                    dtp_feCreacion.Value = Convert.ToDateTime(pacienteActual.PAC_FECHA_CREACION).Date;
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
                    //CargarUltimaAtencion(pacienteActual.PAC_CODIGO);
                    CargarAtencionesPaciente(pacienteActual.PAC_CODIGO);
                    CargarDatosAdicionales2(pacienteActual.PAC_CODIGO);//lx202005
                    if (!pacienteNuevo2)
                    {
                        tabulador.SelectedTab = tabulador.Tabs["gridatenciones"];
                        SendKeys.SendWait("{TAB}");
                    }

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

                if (ultimaAtencion != null)
                {
                    if (ultimaAtencion.ESC_CODIGO == 13)
                    {
                        rbDesactivada.Checked = true;
                    }
                }
                //else
                //rbActiva.Checked = true;


            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //public void CargarAtencion(string numAtencion)
        //{
        //    try
        //    {
        //        //lx201
        //        deshabilitarCamposAtencion();
        //        deshabilitarCamposDireccion();
        //        deshabilitarCamposPaciente();
        //        btnActualizar.Enabled = false;
        //        btnGuardar.Enabled = false;
        //        //lx201

        //        if (numAtencion != string.Empty)
        //            ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(numAtencion);
        //        else
        //            ultimaAtencion = null;

        //        if (ultimaAtencion != null)
        //        {

        //            txt_numeroatencion.Text = ultimaAtencion.ATE_NUMERO_ATENCION;

        //            cmb_tipoingreso.SelectedItem = tipoIngreso.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_INGRESOReference.EntityKey);
        //            cmb_tipoatencion.SelectedItem = tipoTratamiento.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey);
        //            cmb_tiporeferido.SelectedItem = tipoReferido.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_REFERIDOReference.EntityKey);
        //            cmb_formaLlegada.SelectedItem = formasLlegada.FirstOrDefault(f => f.EntityKey == ultimaAtencion.ATENCION_FORMAS_LLEGADAReference.EntityKey);
        //            //cmb_medicos.SelectedItem = medicos.FirstOrDefault(m => m.EntityKey == ultimaAtencion.MEDICOSReference.EntityKey);
        //            cargar_cbotipoatencion(Convert.ToInt32(ultimaAtencion.TipoAtencion));
        //            medico = NegMedicos.medicoPorAtencion(ultimaAtencion.ATE_CODIGO);

        //            DataTable AreaActualHabitacion = new DataTable();

        //            AreaActualHabitacion = NegHabitaciones.AreaActualHab(ultimaAtencion.ATE_CODIGO);
        //            if (AreaActualHabitacion.Rows.Count > 0)
        //            {
        //                txtAreaActual.Text = AreaActualHabitacion.Rows[0][3].ToString();
        //            }

        //            CargarMedico();

        //            HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == ultimaAtencion.HABITACIONESReference.EntityKey);
        //            if (hab != null)
        //            {
        //                Sesion.codHabitacion = hab.hab_Codigo;
        //                txt_habitacion.Text = hab.hab_Numero;
        //            }

        //            //cmbConvenioPago.SelectedItem = tipoPago.FirstOrDefault(f => f.TIF_CODIGO == ultimaAtencion.TIF_CODIGO);

        //            txt_diagnostico.Text = ultimaAtencion.ATE_DIAGNOSTICO_INICIAL;
        //            txt_observaciones.Text = ultimaAtencion.ATE_OBSERVACIONES;
        //            txt_nombreAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
        //            txt_cedulaAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
        //            txt_parentescoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_PARENTESCO;
        //            txt_telefonoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO;
        //            txt_direccionAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION;
        //            txt_ciudadAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CIUDAD;
        //            txt_nombreGar.Text = ultimaAtencion.ATE_GARANTE_NOMBRE;
        //            txt_cedulaGar.Text = ultimaAtencion.ATE_GARANTE_CEDULA;
        //            txt_parentGar.Text = ultimaAtencion.ATE_GARANTE_PARENTESCO;
        //            txt_montoGar.Text = ultimaAtencion.ATE_GARANTE_MONTO_GARANTIA.ToString();
        //            txt_telfGar.Text = ultimaAtencion.ATE_GARANTE_TELEFONO;
        //            txt_dirGar.Text = ultimaAtencion.ATE_GARANTE_DIRECCION;
        //            txt_ciudadGar.Text = ultimaAtencion.ATE_GARANTE_CIUDAD;

        //            if (ultimaAtencion.ATE_FECHA_INGRESO != null)
        //                dateTimeFecIngreso.Value = ultimaAtencion.ATE_FECHA_INGRESO.Value;
        //            else
        //                dateTimeFecIngreso.Value = DateTime.Now;

        //            txt_obPago.Text = ultimaAtencion.TIF_OBSERVACION;

        //            cb_personaFactura.SelectedItem = ultimaAtencion.ATE_FACTURA_NOMBRE;

        //            detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(ultimaAtencion.ATE_CODIGO);

        //            if (detalleCategorias != null)
        //            {
        //                gridAseguradoras.Rows.Clear();
        //                foreach (ATENCION_DETALLE_CATEGORIAS detalle in detalleCategorias)
        //                {
        //                    DataGridViewRow dt = new DataGridViewRow();
        //                    dt.CreateCells(gridAseguradoras);
        //                    CATEGORIAS_CONVENIOS cat = categorias.FirstOrDefault(c => c.EntityKey == detalle.CATEGORIAS_CONVENIOSReference.EntityKey);

        //                    dt.Cells[0].Value = cat.CAT_CODIGO;
        //                    dt.Cells[1].Value = cat.CAT_NOMBRE;
        //                    dt.Cells[2].Value = detalle.ADA_AUTORIZACION;
        //                    dt.Cells[3].Value = detalle.ADA_CONTRATO;
        //                    dt.Cells[4].Value = detalle.ADA_MONTO_COBERTURA;
        //                    dt.Cells[6].Value = detalle.HCC_CODIGO_TS.ToString();
        //                    dt.Cells[8].Value = detalle.HCC_CODIGO_DE.ToString();
        //                    dt.Cells[10].Value = detalle.HCC_CODIGO_ES.ToString();
        //                    dt.Cells[11].Value = NegCatalogos.Especialidad(Convert.ToInt32(detalle.HCC_CODIGO_ES.ToString()));

        //                    //Cambio en convenio
        //                    if (detalle.HCC_CODIGO_TS != null || Convert.ToString(detalle.HCC_CODIGO_TS) != "")
        //                    {
        //                        if (detalle.HCC_CODIGO_TS != 0)
        //                        {
        //                            anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(detalle.HCC_CODIGO_TS));
        //                            dt.Cells[7].Value = anexoIess.ANI_DESCRIPCION;
        //                        }
        //                    }
        //                    if (detalle.HCC_CODIGO_DE != null || Convert.ToString(detalle.HCC_CODIGO_DE) != "")
        //                    {
        //                        if (detalle.HCC_CODIGO_DE != 0)
        //                        {
        //                            anexoIess = NegAnexos.RecuperarAnexos(Convert.ToInt32(detalle.HCC_CODIGO_DE));
        //                            dt.Cells[9].Value = anexoIess.ANI_DESCRIPCION;
        //                        }
        //                    }
        //                    gridAseguradoras.Rows.Add(dt);
        //                }
        //            }
        //            else
        //            {
        //                gridAseguradoras.Rows.Clear();
        //            }

        //            detalleGarantias = NegAtencionDetalleGarantias.RecuperarDetalleGarantiasAtencion(ultimaAtencion.ATE_CODIGO);
        //            if (detalleGarantias != null)
        //            {
        //                gridGarantias.Rows.Clear();
        //                foreach (ATENCION_DETALLE_GARANTIAS detalle in detalleGarantias)
        //                {
        //                    DataGridViewRow dt = new DataGridViewRow();
        //                    dt.CreateCells(gridGarantias);
        //                    TIPO_GARANTIA cat = tipoGarantia.FirstOrDefault(c => c.EntityKey == detalle.TIPO_GARANTIAReference.EntityKey);

        //                    dt.Cells[0].Value = cat.TG_CODIGO;
        //                    dt.Cells[1].Value = cat.TG_NOMBRE;
        //                    dt.Cells[2].Value = detalle.ADG_DESCRIPCION;
        //                    dt.Cells[3].Value = detalle.ADG_DOCUMENTO;
        //                    dt.Cells[4].Value = detalle.ADG_VALOR;
        //                    dt.Cells[5].Value = detalle.ADG_FECHA;


        //                    //lxgrntsx20
        //                    DataTable rs = NegDietetica.getDataTable("getDetalleGaratias", Convert.ToString(detalle.ADG_CODIGO));
        //                    dt.Cells["ADG_BANCO"].Value = rs.Rows[0]["ADG_BANCO"].ToString();
        //                    dt.Cells["ADG_TIPOTARJETA"].Value = rs.Rows[0]["ADG_TIPOTARJETA"].ToString();
        //                    dt.Cells["ADG_CCV"].Value = rs.Rows[0]["ADG_CCV"].ToString();
        //                    dt.Cells["ADG_DIASVENCIMIENTO"].Value = rs.Rows[0]["ADG_DIASVENCIMIENTO"].ToString();
        //                    dt.Cells["ADG_CADUCIDAD"].Value = rs.Rows[0]["ADG_CADUCIDAD"].ToString();
        //                    dt.Cells["ADG_LOTE"].Value = rs.Rows[0]["ADG_LOTE"].ToString();
        //                    dt.Cells["ADG_AUTORIZACION"].Value = rs.Rows[0]["ADG_AUTORIZACION"].ToString();
        //                    dt.Cells["ADG_NUMERO_AUT"].Value = rs.Rows[0]["ADG_NUMERO_AUT"].ToString();
        //                    dt.Cells["ADG_PERSONA_AUT"].Value = rs.Rows[0]["ADG_PERSONA_AUT"].ToString();
        //                    if (rs.Rows[0]["ADG_FECHA_AUT"] != null)
        //                        dt.Cells["ADG_FECHA_AUT"].Value = rs.Rows[0]["ADG_FECHA_AUT"].ToString();
        //                    dt.Cells["ADG_ESTABLECIMIENTO"].Value = rs.Rows[0]["ADG_ESTABLECIMIENTO"].ToString();
        //                    dt.Cells["ADG_NROTARJETA"].Value = rs.Rows[0]["ADG_NROTARJETA"].ToString();
        //                    dt.Cells["ADG_USER"].Value = rs.Rows[0]["UserName"].ToString();
        //                    dt.Cells["Estado"].Value = rs.Rows[0]["ADG_ESTATUS"].ToString();



        //                    gridGarantias.Rows.Add(dt);
        //                }
        //            }
        //            else
        //            {
        //                gridGarantias.Rows.Clear();
        //            }

        //            CargarTitularSeguro(ultimaAtencion.ATE_CODIGO);
        //            CargarAtencionDatosAdicionales(Convert.ToInt32(ultimaAtencion.ATE_NUMERO_ATENCION));//lx202005
        //            btnFormularios.Enabled = true;


        //            if (ultimaAtencion.ATE_FECHA_ALTA == null)
        //                btnActualizar.Enabled = true;


        //        }
        //        else
        //        {
        //            limpiarCamposAtencion(0);
        //            //txt_numeroatencion.Enabled = true;
        //            detalleCategorias = null;
        //            detalleGarantias = null;
        //        }
        //        lockcells();//lxgrntsx

        //    }
        //    catch (System.Exception err)
        //    { MessageBox.Show(err.Message); }
        //}

        public void setCampoHistoriaClinica(string historia)
        {
            txt_historiaclinica.Text = historia.Trim();
        }


        private void CargarDatosAdicionales2(int codigoPaciente)//lx202005
        {
            datosPaciente2 = null;
            datosPaciente2 = NegPacienteDatosAdicionales.PDA2_find(Convert.ToInt64(codigoPaciente));

            if (datosPaciente2 != null)
            {
                txt_telfRef2.Text = datosPaciente2.REF_TELEFONO_2;
                this.dtpFallecido.Text = datosPaciente2.FEC_FALLECIDO;
                if (datosPaciente2.FALLECIDO)
                {
                    fallecido = true;
                    chkFallecido.Checked = true;
                    BloquearControles();
                    btnEliminar.Visible = true;
                    btnActualizar.Enabled = false;
                }
                else
                {
                    chkFallecido.Checked = false;
                    tabulador.Tabs["paciente"].Enabled = true;
                    tabulador.Tabs["atencion"].Enabled = true;
                    tabulador.Tabs["gridatenciones"].Enabled = true;
                    //tabulador.Tabs["certificado"].Enabled = true;
                    btnGuardar.Enabled = true;
                    toolStripNuevo.Enabled = true;
                    btnHabitaciones.Enabled = true;
                    btnFormularios.Enabled = true;
                    btnEliminar.Visible = false;
                    fallecido = false;
                    if (datosPaciente2.nac_etnica != "" && datosPaciente2.nac_etnica != null)
                        cb_netnica.SelectedItem = netnias.FirstOrDefault(e => e.NE_CODIGO == Convert.ToInt32(datosPaciente2.nac_etnica));
                    else
                        cb_netnica.SelectedItem = -1;
                    if (datosPaciente2.pueblo != "" && datosPaciente2.pueblo != null)
                        cb_pueblo.SelectedItem = netnias.FirstOrDefault(e => e.NE_CODIGO == Convert.ToInt32(datosPaciente2.pueblo));
                    else
                        cb_pueblo.SelectedItem = -1;
                    txt_neducacion.Text = datosPaciente2.neducacion;
                    txt_tempresa.Text = datosPaciente2.tempresa;
                    txt_seguro.Text = datosPaciente2.ssalud;
                    txt_bono.Text = datosPaciente2.tbono;
                }
                txtFolio.Text = datosPaciente2.FOLIO;
                //cambios Edgar 20210210 Aqui va recuperar el email si el paciente ha fallecido
                txt_emailAcomp.Text = datosPaciente2.email;
            }
        }

        //Cambios Edgar 20210210
        public void BloquearControles()
        {
            if (tabulador.Tabs["paciente"].Active == true)
            {
                ultraTabPageControl1.Enabled = false;
                tabulador.Tabs["atencion"].Enabled = true;
                tabulador.Tabs["gridatenciones"].Enabled = true;
                //tabulador.Tabs["certificado"].Enabled = true;
            }
            if (tabulador.Tabs["atencion"].Active == true)
            {
                ultraTabPageControl2.Enabled = false;
                tabulador.Tabs["paciente"].Enabled = true;
                tabulador.Tabs["gridatenciones"].Enabled = true;
                tabulador.Tabs["certificado"].Enabled = true;
            }
            if (tabulador.Tabs["gridatenciones"].Active == true)
            {
                ultraTabPageControl3.Enabled = true;
                tabulador.Tabs["paciente"].Enabled = true;
                tabulador.Tabs["atencion"].Enabled = true;
                tabulador.Tabs["certificado"].Enabled = true;
            }
            //if (tabulador.Tabs["certificado"].Active == true)
            //{
            //    ultraTabPageControl11.Enabled = false;
            //    tabulador.Tabs["paciente"].Enabled = true;
            //    tabulador.Tabs["atencion"].Enabled = true;
            //    tabulador.Tabs["gridatenciones"].Enabled = true;
            //}
            btnGuardar.Enabled = false;
            toolStripNuevo.Enabled = false;
            btnHabitaciones.Enabled = false;
            btnFormularios.Enabled = false;
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
                    dt.Cells["ADG_BANCO"].ReadOnly = true;
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
                    dt.Cells["Identificacion"].ReadOnly = true;
                    dt.Cells["Identificacion"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["Telefono"].ReadOnly = true;
                    dt.Cells["Telefono"].Style.BackColor = Color.FromArgb(200, 199, 195);
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
                    dt.Cells["Identifacion"].ReadOnly = true;
                    dt.Cells["Identificacion"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["Telefono"].ReadOnly = true;
                    dt.Cells["Telefono"].Style.BackColor = Color.FromArgb(200, 199, 195);
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
                    dt.Cells["Identifacion"].ReadOnly = true;
                    dt.Cells["Identificacion"].Style.BackColor = Color.FromArgb(200, 199, 195);
                    dt.Cells["Telefono"].ReadOnly = true;
                    dt.Cells["Telefono"].Style.BackColor = Color.FromArgb(200, 199, 195);
                }
                else if (Convert.ToString(dt.Cells["garantia"].Value).Contains("VOUCHER"))
                {
                    dt.Cells["ADG_TIPOTARJETA"].ReadOnly = true;
                    dt.Cells["ADG_BANCO"].ReadOnly = true;
                    //dt.Cells["ADG_BANCO"].Style.BackColor = Color.FromArgb(200, 199, 195);
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
                //cmbTiposDiscapacidadesDA.DataSource = null;
                //if (ateDA.tipo_discapacidad != string.Empty)
                //    cmbTiposDiscapacidadesDA.Items.Add(ateDA.tipo_discapacidad);
                //int codTipo = Convert.ToInt16(ateDA.tipo_discapacidad);
                //cargar_cbotipoatencion(Convert.ToInt16(ateDA.cod_atencion));

                ///Cambios Edgar 20210326 Comente  donde limpian al combo
                cargar_cbotipodiscapacidad(Convert.ToInt16(ateDA.tipo_discapacidad));
                //if (ateDA.paquete.Trim()!="0")
                cargar_cbotipopaquete(Convert.ToInt32(ateDA.paquete));
                //else
                //    cargar_cbotipopaquete(Convert.ToInt32(ateDA.paquete));
                cargar_cbotiposatenciones_adicionarYhabiliar();
            }
            else
            {

                //Cambios edgar 20210326 se corrige que el combo no repita los datos que va añadiendo
                DataTable dt2 = NegAtenciones.TiposDiscapacidades();
                cmbTiposDiscapacidadesDA.DataSource = dt2;
                cmbTiposDiscapacidadesDA.ValueMember = "id";
                cmbTiposDiscapacidadesDA.DisplayMember = "name";
                //DataTable dt2 = NegAtenciones.TiposDiscapacidades();
                //cmbTiposDiscapacidadesDA.Items.Clear();
                //for (int i = 0; i < dt2.Rows.Count; i++)
                //{
                //    cmbTiposDiscapacidadesDA.Items.Add(dt2.Rows[i]["id"].ToString() + "  - " + dt2.Rows[i]["name"].ToString());
                //}
                cmbTiposDiscapacidadesDA.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbTiposDiscapacidadesDA.SelectedIndex = 0;


            }
        }


        public void ValidaDiscapacidad()
        {
            if (chkDiscapacidad.Checked)
            {
                txtPorcentageDA.Enabled = true;
                txtIdDiscapacidad.Enabled = true;
                cmbTiposDiscapacidadesDA.Enabled = true;
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
                txt_Secundaria.Text = datosPacienteActual.DAP_DIRECCION_DOMICILIO2;
                txt_Referencia.Text = datosPacienteActual.DAP_REFERENCIA;

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
                    ,
                    DIAGNOSTICO = n.ATE_DIAGNOSTICOINICIAL
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
            if (ultimaAtencion != null)
            //    MessageBox.Show("Paciente con atenciones facturadas, se debe seleccionar de forma manual la atención requerida.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //else
            {
                ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(Convert.ToString(ultimaAtencion.ATE_CODIGO));
                txt_numeroatencion.Text = ultimaAtencion.ATE_NUMERO_ATENCION;
            }
            CargarAtencion();



            try
            {
                string factura = "";
                if (ultimaAtencion != null)
                {
                    factura = NegAtenciones.PacienteFacturado(ultimaAtencion.ATE_CODIGO);
                }
                if (factura != "")
                {
                    modificar = true;
                }
                else
                    modificar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EdadCalculada()
        {
            if (dtp_fecnac.Value.Date == DateTime.Now.Date)//valida fecha de naciemiento si tiene fecha de hoy no validará el resto de informacion
                return;
            DateTime actual = DateTime.Now.Date;
            if (ultimaAtencion != null)
                actual = Convert.ToDateTime(ultimaAtencion.ATE_FECHA);
            DateTime nacido = dtp_fecnac.Value;

            int edadAnos = actual.Year - nacido.Year;
            if (actual.Month < nacido.Month || (actual.Month == nacido.Month && actual.Day < nacido.Day))
                edadAnos--;

            int edadMeses = actual.Month - nacido.Month;
            if (actual.Day < nacido.Day)
                edadMeses--;
            if (edadMeses < 0)
                edadMeses += 12;

            int diaActual = actual.Day;
            int diaCumple = nacido.Day;
            int diasDiferencia = diaActual - diaCumple;
            if (diasDiferencia < 0)
            {
                //edadMeses -= 1;
                diasDiferencia += DateTime.DaysInMonth(actual.Year, actual.Month);
            }

            lblAnios.Text = edadAnos.ToString();
            gbAnios.Visible = true;

            lblMes.Text = edadMeses.ToString();
            gbMeses.Visible = true;

            lblDias.Text = diasDiferencia.ToString();
            gbDias.Visible = true;
        }

        public void CargarAtencion()

        {
            try
            {
                if (ultimaAtencion != null)
                {

                    //txt_numeroatencion.Text = Convert.ToString(ultimaAtencion.ATE_CODIGO);


                    //TimeSpan diferencia = actual - nacido;
                    //lblDias.Text = diferencia.TotalDays.ToString();
                    //lblMes.Text = (diferencia.TotalDays / 365.25).ToString();
                    //lblAnios.Text = ((diferencia.TotalDays / 365.25) * 12).ToString();
                    //gbMeses.Visible = true;
                    //gbMeses.Visible = true;
                    //gbDias.Visible = true;

                    EdadCalculada();

                    cmb_tipoingreso.SelectedItem = tipoIngreso.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_INGRESOReference.EntityKey);


                    cmb_tipoatencion.SelectedItem = tipoTratamiento.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey);
                    cmb_tiporeferido.SelectedItem = tipoReferido.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_REFERIDOReference.EntityKey);
                    cmb_formaLlegada.SelectedItem = formasLlegada.FirstOrDefault(f => f.EntityKey == ultimaAtencion.ATENCION_FORMAS_LLEGADAReference.EntityKey);
                    medico = NegMedicos.medicoPorAtencion(ultimaAtencion.ATE_CODIGO);

                    CargarMedico();

                    DataTable AreaActualHabitacion = new DataTable();

                    AreaActualHabitacion = NegHabitaciones.AreaActualHab(ultimaAtencion.ATE_CODIGO);
                    if (AreaActualHabitacion.Rows.Count > 0)
                    {
                        txtAreaActual.Text = AreaActualHabitacion.Rows[0][3].ToString();
                    }

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

                    cb_personaFactura.SelectedItem = ultimaAtencion.ATE_FACTURA_NOMBRE;
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
                    txt_emailAcomp.Text = NegAtenciones.PacienteDatosAdicionales(Convert.ToInt64(ultimaAtencion.ATE_CODIGO));

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


                    detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(ultimaAtencion.ATE_CODIGO);

                    DataTable validador = new DataTable();
                    if (txt_numeroatencion.Text != "")
                        validador = NegAtenciones.ValidaEstatusAtencion(Convert.ToInt64(txt_numeroatencion.Text));

                    //if(validador.Rows.Count > 0)
                    //{
                    //if (Convert.ToDecimal(validador.Rows[0][1].ToString()) == 0)
                    //{
                    //    groupBox3.Enabled = true;
                    //}
                    //else
                    //    groupBox3.Enabled = false;

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
                            if (detalle.HCC_CODIGO_ES != null)
                                dt.Cells[11].Value = NegCatalogos.Especialidad(Convert.ToInt32(detalle.HCC_CODIGO_ES.ToString()));
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

                    //}
                    //else
                    //    groupBox3.Enabled = false;

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
                Ginecologia = NegCuentasPacientes.RecuperaGinecologia(txt_historiaclinica.Text.Trim());
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
                //Cambios edgar 20210415
                EstadoAtencion();
                lockcells();//lxgrntsx
            }
            catch (System.Exception x)
            {
                MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void EstadoAtencion()
        {
            DataTable validador = new DataTable();
            //Cambios Edgar Valida Estado Atencion
            if (txt_numeroatencion.Text != "")
                validador = NegAtenciones.ValidaEstatusAtencion(Convert.ToInt64(txt_numeroatencion.Text));

            if (validador.Rows.Count > 0)
            {
                if (Convert.ToDecimal(validador.Rows[0][0].ToString()) == 0)
                {
                    groupBox3.Enabled = true;
                }
                else
                    groupBox3.Enabled = false;
            }
            else
                groupBox3.Enabled = true;
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
            DataTable med = NegMedicos.MedicoIDValida(codMedico);
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodMedico.Text = "";
                txtNombreMedico.Text = "";
                return;
            }
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
                listaAnexos = NegAnexos.RecuperarListaAnexos(Convert.ToString(His.Parametros.AdmisionParametros.CodigoAnexoParentesco));
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

        public bool ClienteExiste = false;
        private void guardarDatos()
        {
            try
            {

                if (pacienteNuevo == true)
                {
                    #region pacientenuevo
                    //INGRESO PACIENTE
                    try
                    {

                        pacienteActual = new PACIENTES();
                        datosPacienteActual = new PACIENTES_DATOS_ADICIONALES();
                        ultimaAtencion = new ATENCIONES();


                        pacienteActual.PAC_CODIGO = NegPacientes.ultimoCodigoPacientes() + 1;

                        NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                        DataTable numHC = new DataTable();
                        numHC = NegPacientes.RecuperaMaximoPacienteHistoriaClinica();
                        string historia = numHC.Rows[0][0].ToString();
                        if (NegNumeroControl.NumerodeControlAutomatico(6))
                        {
                            //numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 6).FirstOrDefault();

                            DataTable pacienteJire = new DataTable();
                            pacienteJire = NegPacientes.PacienteJire(txt_cedula.Text.Trim());
                            if (pacienteJire.Rows.Count > 0)
                            {
                                pacienteActual.PAC_HISTORIA_CLINICA = pacienteJire.Rows[0][1].ToString();
                            }
                            else
                                pacienteActual.PAC_HISTORIA_CLINICA = historia;
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
                        datosPaciente123.FOLIO = txtFolio.Text.ToString().Trim();
                        if (chkFallecido.Checked)
                            datosPaciente123.FALLECIDO = true;
                        datosPaciente123.FEC_FALLECIDO = (dtpFallecido.Value).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                        datosPaciente123.REF_TELEFONO_2 = txt_telfRef2.Text.Replace("-", string.Empty).ToString();
                        datosPaciente123.email = txt_emailAcomp.Text;
                        datosPaciente123.id_usuario = Sesion.codUsuario;
                        if (cb_netnica.SelectedValue != null)
                            datosPaciente123.nac_etnica = cb_netnica.SelectedValue.ToString();
                        else
                            datosPaciente123.nac_etnica = "";
                        if (cb_pueblo.SelectedValue != null)
                            datosPaciente123.pueblo = cb_pueblo.SelectedValue.ToString();
                        else
                            datosPaciente123.pueblo = "";
                        datosPaciente123.neducacion = txt_neducacion.Text;
                        datosPaciente123.tempresa = txt_tempresa.Text;
                        datosPaciente123.ssalud = txt_seguro.Text;
                        datosPaciente123.tbono = txt_bono.Text;

                        //se almacena el email del acompañante

                        NegPacienteDatosAdicionales.PDA2_save(datosPaciente123);

                        //INGRESO ATENCION

                        int codigo_atencion_asignado = NegAtenciones.UltimoCodigoAtenciones() + 1;
                        ultimaAtencion.ATE_CODIGO = codigo_atencion_asignado;
                        DataTable numAtencion = new DataTable();
                        numAtencion = NegAtenciones.NumeroAtencion(pacienteActual.PAC_CODIGO);
                        ultimaAtencion.ATE_NUMERO_ADMISION = Convert.ToInt16(numAtencion.Rows[0][0].ToString().Trim());
                        ultimaAtencion.ATE_ESTADO = true;
                        ultimaAtencion.ATE_FECHA = DateTime.Now;

                        ultimaAtencion.ESC_CODIGO = 1; // por defecto ingreso

                        //if (NegNumeroControl.NumerodeControlAutomatico(8))
                        //    numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
                        ultimaAtencion.ATE_NUMERO_ATENCION = Convert.ToString(NegAtenciones.ultimoNumeroAdmision(pacienteActual.PAC_CODIGO));
                        agregarDatosAtencion();

                        ocuparHabitacion();

                        NegAtenciones.CrearAtencion(ultimaAtencion);
                        if (cmb_tipoingreso.SelectedIndex == 5)
                        {
                            ultimaAtencion.TIPO_INGRESOReference.EntityKey = ((TIPO_INGRESO)cmb_tipoingreso.SelectedItem).EntityKey;
                            crearPreAtencion(pacienteActual.PAC_CODIGO, ultimaAtencion.ATE_CODIGO);
                        }
                        NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(ultimaAtencion.ATE_CODIGO, 1, Sesion.codUsuario, "ADMISION");
                        NegNumeroControl.LiberaNumeroControl(8);


                        agregarDetalleCategorias();

                        AgregarOtroSeguro();
                        AgregarDerivados();

                        agregarDetalleGarantias();
                        crearFormularioAdmision();
                        guardarDatosTitularSeguro();
                        NegCuentasPacientes.GuardaGinecologia(txt_historiaclinica.Text.Trim(), Convert.ToInt16(txtMenarquia.Text), txtCiclos.Text, Convert.ToDateTime(dateFUM.Text), Convert.ToInt16(txtG.Text),
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
                        //string[] wordss = cmbTiposDiscapacidadesDA.Text.Split('-');
                        atenDA.tipo_discapacidad = cmbTiposDiscapacidadesDA.SelectedValue.ToString();

                        if (cboPaquete.SelectedValue != null)
                        {
                            //string[] words = cboPaquete.Text.Split('-');
                            //atenDA.paquete = words[0].Trim();
                            atenDA.paquete = cboPaquete.SelectedValue.ToString();
                        }
                        else
                            atenDA.paquete = "";


                        atenDA.cod_atencion = Convert.ToInt32(ultimaAtencion.ATE_NUMERO_ATENCION);

                        NegAtenciones.atencionDA_save(atenDA);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Perdida de conexión con el servidor, la aplicación va tratar de recuperarse!!! \nEsperar 5 segundos, gracias.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Thread.Sleep(5000);
                        NegPacientes.EliminarPaciente(pacienteActual.PAC_CODIGO);
                        MessageBox.Show("Error vuelva a iniciar la aplicación", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }

                    #endregion
                }
                else
                {
                    #region Editar paciente
                    //EDITAR PACIENTE
                    pacienteActual.PAC_HISTORIA_CLINICA = txt_historiaclinica.Text.Trim();
                    agregarDatosPaciente();

                    NegPacientes.EditarPaciente(pacienteActual);

                    //if (direccionNueva == true)
                    //{
                    //    //INGRESO DATOS
                    //    datosPacienteActual = new PACIENTES_DATOS_ADICIONALES();
                    //    datosPacienteActual.DAP_CODIGO = NegPacienteDatosAdicionales.ultimoCodigoDatos() + 1;
                    //    datosPacienteActual.DAP_NUMERO_REGISTRO = (Int16)(NegPacienteDatosAdicionales.ultimoNumeroRegistro(pacienteActual.PAC_CODIGO) + 1);
                    //datosPacienteActual.DAP_NUMERO_REGISTRO += 1;
                    //    datosPacienteActual.DAP_ESTADO = true;
                    //    datosPacienteActual.DAP_FECHA_INGRESO = DateTime.Now;
                    //    agregarDatosAdicionalesPaciente();
                    //    NegPacienteDatosAdicionales.CrearPacienteDatosAdicionales(datosPacienteActual, pacienteActual.PAC_CODIGO);
                    //}
                    if (direccionActual != txt_direccion.Text || secundaria != txt_Secundaria.Text || referencia != txt_Referencia.Text)
                    {
                        datosPacienteActual = new PACIENTES_DATOS_ADICIONALES();
                        datosPacienteActual.DAP_CODIGO = NegPacienteDatosAdicionales.ultimoCodigoDatos() + 1;
                        datosPacienteActual.DAP_NUMERO_REGISTRO = (Int16)(NegPacienteDatosAdicionales.ultimoNumeroRegistro(pacienteActual.PAC_CODIGO) + 1);
                        datosPacienteActual.DAP_NUMERO_REGISTRO += 1;
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
                    DtoPacienteDatosAdicionales2 datosPaciente25 = NegPacienteDatosAdicionales.PDA2_find(Convert.ToInt32(pacienteActual.PAC_CODIGO));
                    DtoPacienteDatosAdicionales2 datosPaciente23 = new DtoPacienteDatosAdicionales2();
                    datosPaciente23.COD_PACIENTE = pacienteActual.PAC_CODIGO;
                    datosPaciente23.FALLECIDO = false;
                    if (chkFallecido.Checked)
                        datosPaciente23.FALLECIDO = true;
                    if (datosPaciente25 != null)
                    {
                        datosPaciente23.FEC_FALLECIDO = datosPaciente25.FEC_FALLECIDO;
                        datosPaciente23.motivo = datosPaciente25.motivo;
                        datosPaciente23.diagnostico = datosPaciente25.diagnostico;
                    }
                    else
                    {
                        datosPaciente23.FEC_FALLECIDO = (dtpFallecido.Value).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    }
                    datosPaciente23.FOLIO = txtFolio.Text.ToString().Trim();
                    datosPaciente23.REF_TELEFONO_2 = txt_telfRef2.Text.Replace("-", string.Empty).ToString();
                    datosPaciente23.email = txt_emailAcomp.Text;
                    if (cb_netnica.SelectedValue != null)
                        datosPaciente23.nac_etnica = cb_netnica.SelectedValue.ToString();
                    else
                        datosPaciente23.nac_etnica = "";
                    if (cb_pueblo.SelectedValue != null)
                        datosPaciente23.pueblo = cb_pueblo.SelectedValue.ToString();
                    else
                        datosPaciente23.pueblo = "";
                    datosPaciente23.neducacion = txt_neducacion.Text;
                    datosPaciente23.tempresa = txt_tempresa.Text;
                    datosPaciente23.ssalud = txt_seguro.Text;
                    datosPaciente23.tbono = txt_bono.Text;
                    NegPacienteDatosAdicionales.PDA2_save(datosPaciente23);
                    ////////////////////////////////////////////////lx


                    if (txt_numeroatencion.Text.Trim() != string.Empty)
                    {
                        if (ultimaAtencion == null || atencionNueva)
                        {
                            ultimaAtencion = new ATENCIONES();
                            ultimaAtencion.ATE_CODIGO = NegAtenciones.UltimoCodigoAtenciones() + 1;
                            ultimaAtencion.ATE_NUMERO_ADMISION = NegAtenciones.ultimoNumeroAdmision(pacienteActual.PAC_CODIGO);
                            ultimaAtencion.ATE_ESTADO = true;
                            ultimaAtencion.ATE_FECHA = DateTime.Now;
                            ultimaAtencion.ESC_CODIGO = 1;
                            ultimaAtencion.ATE_FACTURA_NOMBRE = cb_personaFactura.Text;
                            NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                            if (NegNumeroControl.NumerodeControlAutomatico(8))
                                numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
                            ultimaAtencion.ATE_NUMERO_ATENCION = txt_numeroatencion.Text.Trim();

                            agregarDatosAtencion();
                            if (cmb_tipoingreso.SelectedIndex == 5)
                            {
                                ultimaAtencion.TIPO_INGRESOReference.EntityKey = ((TIPO_INGRESO)cmb_tipoingreso.SelectedItem).EntityKey;
                                crearPreAtencion(pacienteActual.PAC_CODIGO, ultimaAtencion.ATE_CODIGO);
                            }

                            ocuparHabitacion();
                            NegAtenciones.CrearAtencion(ultimaAtencion);
                            NegAtenciones.CrearCAMBIO_ESTADO_ATENCIONES(ultimaAtencion.ATE_CODIGO, 1, Sesion.codUsuario, "ADMISION");
                            NegNumeroControl.LiberaNumeroControl(8);
                            if (!chb_PreAdmision.Checked)
                            {
                                CUENTAS_PACIENTES_TOTALES cuentaP = new CUENTAS_PACIENTES_TOTALES();
                                cuentaP = NegCuentasPacientes.RecuperarCuentasTotal(ultimaAtencion.ATE_CODIGO);
                                if (cuentaP == null)
                                    His.Admision.Datos.CuentaPaciente.AgregarCuentaPacientesT(ultimaAtencion);
                            }
                            crearFormularioAdmision();
                            atencionNueva = false;
                        }

                        else
                        {
                            agregarDatosAtencion();

                            if (chb_PreAdmision.Checked)
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
                                if (preAtencioActual != null && !chb_PreAdmision.Checked && txt_habitacion.Text != "PREA")
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
                            if (!chb_PreAdmision.Checked)
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
                        //string[] wordss = cmbTiposDiscapacidadesDA.Text.Split('-');
                        ateD.tipo_discapacidad = cmbTiposDiscapacidadesDA.SelectedValue.ToString();

                        if (cboPaquete.SelectedValue != null)
                        {
                            ateD.paquete = cboPaquete.SelectedValue.ToString();
                        }
                        else
                            ateD.paquete = "";


                        ateD.cod_atencion = Convert.ToInt32(ultimaAtencion.ATE_NUMERO_ATENCION.Trim());
                        if (ultimaAtencion.ESC_CODIGO == 1)
                        {
                            ultimaAtencion.ATE_FACTURA_FECHA = null;
                            ultimaAtencion.ATE_ESTADO = true;
                        }
                        //cambios Mario 2022.08.26 -- cambion por que no se actualizaba la atencion con el objeto anterior
                        NegAtenciones.ActualizarUAtencion(ultimaAtencion);
                        NegAtenciones.atencionDA_save(ateD);



                        ////////////
                    }
                }

                //Cambios Edgar 20210528 --Facturar a Nombre de...
                if (ClienteExiste)
                    NegAtenciones.FacturarA(txtFNombre.Text, txtFDireccion.Text, txtFCedula.Text, txtFCelular.Text, txtFEmail.Text);


                //CONSULTAS SUBSECUENTES PABLOROCHA 20230128

                if (AtencionSubsecuente != 0)
                {
                    ATENCIONES_SUBSECUENTES obj = new ATENCIONES_SUBSECUENTES();
                    obj.ate_codigo_principal = AtencionSubsecuente;
                    obj.ate_codigo_subsecuente = ultimaAtencion.ATE_CODIGO;
                    obj.id_usuario = Sesion.codUsuario;
                    obj.estado = true;
                    obj.fecha_creacion = DateTime.Now;

                    NegAtenciones.GuardaAtencionSubsecuente(obj);
                    AtencionSubsecuente = 0;
                }


            }
            catch (System.Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos: \n" + e.InnerException);
                return;
            }
        }


        private void agregarDatosPaciente()
        {
            pacienteActual.PAC_NOMBRE1 = txt_nombre1.Text.ToString().Trim();
            pacienteActual.PAC_NOMBRE2 = txt_nombre2.Text.ToString().Trim();
            pacienteActual.PAC_APELLIDO_MATERNO = txt_apellido2.Text.ToString().Trim();
            pacienteActual.PAC_APELLIDO_PATERNO = txt_apellido1.Text.ToString().Trim();
            pacienteActual.ETNIAReference.EntityKey = ((ETNIA)cb_etnia.SelectedItem).EntityKey;

            pacienteActual.PAC_FECHA_NACIMIENTO = dtp_fecnac.Value;

            //int codCiudad = Convert.ToInt16(cmb_ciudad.SelectedValue);
            if (cmb_ciudad.SelectedItem != null)
            {
                if (((DIVISION_POLITICA)cmb_ciudad.SelectedItem).DIPO_CODIINEC != "0")
                    pacienteActual.DIPO_CODIINEC = ((DIVISION_POLITICA)cmb_ciudad.SelectedItem).DIPO_CODIINEC;
                else
                {
                    pacienteActual.DIPO_CODIINEC = cmb_pais.SelectedValue.ToString();
                }
            }
            else
                pacienteActual.DIPO_CODIINEC = cmb_pais.SelectedValue.ToString();

            if (rbn_h.Checked == true)
                pacienteActual.PAC_GENERO = "M";
            else
                pacienteActual.PAC_GENERO = "F";

            pacienteActual.GRUPO_SANGUINEOReference.EntityKey = ((GRUPO_SANGUINEO)cb_gruposanguineo.SelectedItem).EntityKey;
            pacienteActual.PAC_IDENTIFICACION = txt_cedula.Text.ToString().Trim();
            pacienteActual.PAC_NACIONALIDAD = txt_nacionalidad.Text.ToString().Trim();

            if (rbCedula.Checked == true)
                pacienteActual.PAC_TIPO_IDENTIFICACION = "C";
            else if (rbPasaporte.Checked == true)
                pacienteActual.PAC_TIPO_IDENTIFICACION = "P";
            else if (rbtCarnet.Checked == true)
                pacienteActual.PAC_TIPO_IDENTIFICACION = "R";
            else if (rbtCedulaExt.Checked == true)
                pacienteActual.PAC_TIPO_IDENTIFICACION = "E";
            else if (rbRuc.Checked == true)
                pacienteActual.PAC_TIPO_IDENTIFICACION = "R";
            else if (rbRn.Checked == true)
                pacienteActual.PAC_TIPO_IDENTIFICACION = "N";
            else
                pacienteActual.PAC_TIPO_IDENTIFICACION = "S";

            pacienteActual.PAC_EMAIL = txt_email.Text.ToString().Trim();
            pacienteActual.PAC_ESTADO = true;
            pacienteActual.USUARIOSReference.EntityKey = usuario.EntityKey;
            pacienteActual.PAC_REFERENTE_NOMBRE = txt_nombreRef.Text.ToString().Trim();
            pacienteActual.PAC_REFERENTE_PARENTESCO = txt_parentRef.Text.ToString().Trim();
            pacienteActual.PAC_REFERENTE_TELEFONO = txt_telfRef.Text.Replace("-", string.Empty).ToString();
            pacienteActual.PAC_REFERENTE_DIRECCION = txtRefDireccion.Text.ToString().Trim();

        }

        private void agregarDatosAdicionalesPaciente()
        {
            datosPacienteActual.DAP_DIRECCION_DOMICILIO = txt_direccion.Text.ToString().Trim();
            datosPacienteActual.DAP_TELEFONO1 = txt_telefono1.Text.Replace("-", string.Empty).ToString();
            datosPacienteActual.DAP_TELEFONO2 = txt_telefono2.Text.Replace("-", string.Empty).ToString();
            datosPacienteActual.DAP_OCUPACION = txt_ocupacion.Text.ToString().Trim();
            datosPacienteActual.DAP_DIRECCION_DOMICILIO2 = txt_Secundaria.Text.Trim();
            datosPacienteActual.DAP_REFERENCIA = txt_Referencia.Text.Trim();

            if (txt_rucEmp.Text.ToString() != string.Empty)
            {
                if (empresaPaciente == null)
                {
                    empresaPaciente = new ASEGURADORAS_EMPRESAS();
                    empresaPaciente.ASE_CODIGO = (Int16)(NegAseguradoras.UltimoCodigoAseguradoras() + 1);
                    empresaPaciente.ASE_RUC = txt_rucEmp.Text.ToString().Trim();
                    empresaPaciente.ASE_NOMBRE = txt_empresa.Text.ToString().Trim();
                    empresaPaciente.ASE_DIRECCION = txt_direcEmp.Text.ToString().Trim();
                    empresaPaciente.ASE_CIUDAD = txt_ciudadEmp.Text.ToString().Trim();
                    empresaPaciente.ASE_TELEFONO = txt_telfEmp.Text.Replace("-", string.Empty).ToString();
                    empresaPaciente.ASE_CONVENIO = false;
                    empresaPaciente.ASE_ESTADO = true;
                    empresaPaciente.TIPO_EMPRESAReference.EntityKey = NegAseguradoras.RecuperaTipoEmpresa().FirstOrDefault(a => a.TE_CODIGO == 2).EntityKey;
                    NegAseguradoras.Crear(empresaPaciente);
                    MessageBox.Show("Se creo una nueva empresa");
                }
                else
                {
                    empresaPaciente.ASE_NOMBRE = txt_empresa.Text.ToString().Trim();
                    empresaPaciente.ASE_DIRECCION = txt_direcEmp.Text.ToString().Trim();
                    empresaPaciente.ASE_CIUDAD = txt_ciudadEmp.Text.ToString().Trim();
                    empresaPaciente.ASE_TELEFONO = txt_telfEmp.Text.Replace("-", string.Empty).ToString();
                    NegAseguradoras.ModificarAseguradora(empresaPaciente);
                }
                datosPacienteActual.EMP_CODIGO = empresaPaciente.ASE_CODIGO;
                aseguradoras = NegAseguradoras.ListaEmpresas();
            }

            datosPacienteActual.DAP_EMP_NOMBRE = txt_empresa.Text.ToString().Trim();
            datosPacienteActual.DAP_EMP_DIRECCION = txt_direcEmp.Text.ToString().Trim();
            datosPacienteActual.DAP_EMP_CIUDAD = txt_ciudadEmp.Text.ToString().Trim();
            datosPacienteActual.DAP_EMP_TELEFONO = txt_telfEmp.Text.Replace("-", string.Empty).ToString();
            datosPacienteActual.DAP_INSTRUCCION = txt_instuccion.Text.ToString().Trim();

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
            ultimaAtencion.ATE_FACTURA_NOMBRE = cb_personaFactura.Text;
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

            ultimaAtencion.ATE_EDAD_PACIENTE = Convert.ToInt16(DateTime.Now.Year - Convert.ToDateTime(pacienteActual.PAC_FECHA_CREACION).Year);
            ultimaAtencion.ATE_ACOMPANANTE_CEDULA = txt_cedulaAcomp.Text.ToString().Trim();
            ultimaAtencion.ATE_ACOMPANANTE_CIUDAD = txt_ciudadAcomp.Text.ToString().Trim();
            ultimaAtencion.ATE_ACOMPANANTE_DIRECCION = txt_direccionAcomp.Text.ToString().Trim();
            ultimaAtencion.ATE_ACOMPANANTE_NOMBRE = txt_nombreAcomp.Text.ToString().Trim();
            ultimaAtencion.ATE_ACOMPANANTE_PARENTESCO = txt_parentescoAcomp.Text.ToString().Trim();
            ultimaAtencion.ATE_ACOMPANANTE_TELEFONO = txt_telefonoAcomp.Text.Replace("-", string.Empty).ToString();
            ultimaAtencion.ATE_DIAGNOSTICO_INICIAL = txt_diagnostico.Text.ToString().Trim();
            ultimaAtencion.ATE_GARANTE_CEDULA = txt_cedulaGar.Text.ToString().Trim();
            ultimaAtencion.ATE_GARANTE_CIUDAD = txt_ciudadGar.Text.ToString().Trim();
            ultimaAtencion.ATE_GARANTE_DIRECCION = txt_dirGar.Text.ToString().Trim();
            string[] codigoTAtencion = cmbTipoAtencion.Text.Split(' ');

            //ultimaAtencion.TipoAtencion = (cmbTipoAtencion.Text).Substring(0, 3).Trim(); //lx202005
            ultimaAtencion.TipoAtencion = codigoTAtencion[0].Trim();


            if (txt_montoGar.Text != string.Empty)
                ultimaAtencion.ATE_GARANTE_MONTO_GARANTIA = Convert.ToDecimal(txt_montoGar.Text.ToString());

            ultimaAtencion.ATE_GARANTE_NOMBRE = txt_nombreGar.Text.ToString().Trim();
            ultimaAtencion.ATE_GARANTE_PARENTESCO = txt_parentGar.Text.ToString().Trim();
            ultimaAtencion.ATE_GARANTE_TELEFONO = txt_telfGar.Text.Replace("-", string.Empty).ToString();
            ultimaAtencion.ATE_OBSERVACIONES = txt_observaciones.Text.ToString().Trim();

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
                habDetalle.HAD_REGISTRO_ANTERIOR = Convert.ToInt16(cmb_tipoingreso.SelectedValue);
                NegHabitaciones.CrearHabitacionDetalle(habDetalle);

            }
            else
            {
                if (Convert.ToInt32(cmb_tipoingreso.SelectedValue) == 5)
                {
                    HABITACIONES hab = NegHabitaciones.RecuperarHabitacionId(0);
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
                    habDetalle.HAD_REGISTRO_ANTERIOR = Convert.ToInt16(cmb_tipoingreso.SelectedValue);
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
                NegAtencionDetalleCategorias.EliminaAtencion_Detalle_Categorias(ultimaAtencion.ATE_CODIGO);
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
                    string xfecha = "";
                    if (gridGarantias.Rows[i].Cells["ADG_FECHA_AUT"].Value != null)
                    {
                        xfecha = Convert.ToDateTime(fila.Cells["ADG_FECHA_AUT"].Value).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                    }
                    object[] garantiaDA = new object[]{
                    fila.Cells["ADG_BANCO"].Value, fila.Cells["ADG_TIPOTARJETA"].Value,
                    fila.Cells["ADG_CCV"].Value, fila.Cells["ADG_DIASVENCIMIENTO"].Value,
                    fila.Cells["ADG_CADUCIDAD"].Value, fila.Cells["ADG_LOTE"].Value,
                    fila.Cells["ADG_AUTORIZACION"].Value, fila.Cells["ADG_NUMERO_AUT"].Value,
                    fila.Cells["ADG_PERSONA_AUT"].Value, xfecha, fila.Cells["ADG_ESTABLECIMIENTO"].Value,
                    fila.Cells["ADG_NROTARJETA"].Value, Sesion.codUsuario.ToString(), nuevo,
                    fila.Cells["Identificacion"].Value, fila.Cells["Telefono"].Value};

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
            txt_historiaclinica.ReadOnly = false;
            pacienteNuevo = true;
            pacienteNuevo2 = true;

            pacienteActual = null;
            datosPacienteActual = null;
            ultimaAtencion = null;

            atencionNueva = true;

            btnGuardar.Enabled = true;
            ubtnDatosIncompletos.Enabled = true;
            habilitarAyudas();
            btnCancelar.Enabled = true;
            btnImprimir.Enabled = false;
            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;
            btnNewAtencion.Enabled = false;
            btnPr.Enabled = true;


            panelBotonesDir.Enabled = false;

            limpiarCamposPaciente();
            limpiarCamposDireccion();
            limpiarCamposAtencion(0);
            limpiarCamposPreAtencion();
            fijarDatos();
            desactivada = false;

            NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
            numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 6).FirstOrDefault();
            DataTable numHC = new DataTable();
            numHC = NegPacientes.RecuperaMaximoPacienteHistoriaClinica();
            string historia = numHC.Rows[0][0].ToString();


            if (numerocontrol.TIPCON == "A")
            {
                txt_historiaclinica.Text = historia.ToString();
                txt_historiaclinica.ReadOnly = true;
            }
            else
            {
                //txt_historiaclinica.Text = numerocontrol.NUMCON.ToString();
                txt_historiaclinica.ReadOnly = false;
            }

            txt_numeroatencion.Enabled = false;
            //DataTable ATE_NUMERO_CONTROL = new DataTable();
            //ATE_NUMERO_CONTROL = NegNumeroControl.RecuperaNumeroControlPablo();
            //numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
            //int ATE_NUMERO_CONTROL = Convert.ToInt16(numerocontrol.NUMCON.ToString()) + 1;
            DataTable ATE_NUMERO_CONTROL = NegPacientes.RecuperaMaximoPacienteNumeroAtencion();
            txt_numeroatencion.Text = (Convert.ToInt64(ATE_NUMERO_CONTROL.Rows[0][0]) + 1).ToString();
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

            CargarLocalizacion();

            txt_historiaclinica.ReadOnly = false;
            txt_historiaclinica.Enabled = true;
            cmb_tiporeferido.SelectedValue = 3;
            TIPO_INGRESO _INGRESO = new TIPO_INGRESO();
            _INGRESO = NegTipoIngreso.FiltrarPorId(2);
            cmb_tipoingreso.SelectedValue = _INGRESO.TIP_CODIGO; //DEFAULT HOSPITALIZACION

            if (ConsultaExterna)
            {
                if (mushuñan)
                {
                    switch (AreaUsuario())
                    {
                        case 2:
                            consultaExterna = 1;
                            _INGRESO = NegTipoIngreso.FiltrarPorId(10);
                            cmb_tipoingreso.SelectedValue = _INGRESO.TIP_CODIGO;
                            break;
                        case 3:
                            consultaExterna = 1;
                            _INGRESO = NegTipoIngreso.FiltrarPorId(12);
                            cmb_tipoingreso.SelectedValue = _INGRESO.TIP_CODIGO;
                            txtCodMedico.Text = "0";
                            CargarMedico(0);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    consultaExterna = 1;
                    _INGRESO = NegTipoIngreso.FiltrarPorId(4);
                    cmb_tipoingreso.SelectedValue = _INGRESO.TIP_CODIGO;
                }
            }
        }
        public Int16 AreaUsuario()
        {
            Int16 AreaUsuario = 1;
            DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario));
            bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
            if (parse)
            {
                return AreaUsuario;
            }
            else
                return AreaUsuario;
        }
        private void btnNewAtencion_Click(object sender, EventArgs e)
        {

            ///////////////
            DataTable auxx = NegDietetica.getDataTable("EnHabitacion", txt_historiaclinica.Text.Trim());
            if (auxx.Rows[0][0].ToString().Trim() == "0")
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

                //cambios Edgar 20210302
                consultas = false;
                desactivada = false;
                habilitarCamposPaciente();
                habilitarCamposDireccion();
                btnHabitaciones.Enabled = false;
                btnFormularios.Enabled = false;
                btnNuevo.Enabled = false;
                btnActualizar.Enabled = false;
                btnImprimir.Enabled = false;
                btnPr.Enabled = true;
                //-------------------------

                limpiarCamposAtencion(0);
                fijarDatos();

                txt_numeroatencion.Enabled = false;

                NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
                DataTable ATE_NUMERO_CONTROL = NegPacientes.RecuperaMaximoPacienteNumeroAtencion();
                txt_numeroatencion.Text = (Convert.ToInt64(ATE_NUMERO_CONTROL.Rows[0][0]) + 1).ToString();
                dateTimeFecIngreso.Value = DateTime.Now;
                habilitarCamposAtencion();

                tabulador.SelectedTab = tabulador.Tabs["atencion"];
                limpiarCamposPreAtencion();
                habilitarCamposPreAtencion();
                atencionReal = Convert.ToInt32(txt_numeroatencion.Text) - 1;
                cmb_tiporeferido.SelectedValue = 3;
                consultaExterna = 1;

                TIPO_INGRESO _INGRESO = new TIPO_INGRESO();
                _INGRESO = NegTipoIngreso.FiltrarPorId(2);
                cmb_tipoingreso.SelectedValue = _INGRESO.TIP_CODIGO; //DEFAULT HOSPITALIZACION

                if (ConsultaExterna)
                {
                    if (mushuñan)
                    {
                        switch (AreaUsuario())
                        {
                            case 2:
                                consultaExterna = 1;
                                _INGRESO = NegTipoIngreso.FiltrarPorId(10);
                                cmb_tipoingreso.SelectedValue = _INGRESO.TIP_CODIGO;
                                break;
                            case 3:
                                consultaExterna = 1;
                                _INGRESO = NegTipoIngreso.FiltrarPorId(12);
                                cmb_tipoingreso.SelectedValue = _INGRESO.TIP_CODIGO;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        _INGRESO = NegTipoIngreso.FiltrarPorId(4);
                        cmb_tipoingreso.SelectedValue = _INGRESO.TIP_CODIGO;
                    }
                    btn_Subsecuente.Visible = true;
                    btn_Subsecuente.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("El paciente se encuentra hospitalizado actualmente, no es posible generar una nueva atención.", "HIS3000");
                btn_Subsecuente.Visible = false;
                btn_Subsecuente.Enabled = false;
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {

        }
        ATENCIONES validacionEstado = new ATENCIONES();
        string direccionActual = "";
        string secundaria = "";
        string referencia = "";
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (ultimaAtencion != null)
            {
                validacionEstado = NegAtenciones.RecuperarAtencionID(ultimaAtencion.ATE_CODIGO);
                if (validacionEstado != null)
                {
                    if (validacionEstado.ESC_CODIGO == 6)
                    {
                        MessageBox.Show("Esta Atencion ya fue facturada  no se puede modificar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnActualizar.Enabled = false;
                        return;
                    }
                    if (validacionEstado.ESC_CODIGO == 2)
                    {
                        MessageBox.Show("Esta Atencion ya fue dada el alta no se puede modificar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnActualizar.Enabled = false;
                        return;
                    }
                }
            }

            if (txt_numeroatencion.Text == "")
            {
                MessageBox.Show("Debe seleccionar una ATENCIÓN para poder modificarla.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabulador.SelectedTab = tabulador.Tabs["gridatenciones"];
                SendKeys.SendWait("{TAB}");
                return;
            }
            pacienteNuevo = false;
            atencionNueva = false;
            //cmb_tiporeferido.SelectedIndex = -1;
            //txtCodMedico.Text = "";
            //txtNombreMedico.Text = "";
            btnGuardar.Enabled = true;
            habilitarAyudas();
            btnCancelar.Enabled = true;
            btnImprimir.Enabled = true;
            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;
            btnHabitaciones.Enabled = true;
            btnFormularios.Enabled = true;
            panelBotonesDir.Enabled = true;
            btn_imprimir_datos_facturar_a.Visible = false;
            consultas = false;
            habilitarCamposPaciente();
            habilitarCamposDireccion();
            ValidaDiscapacidad();
            atencionNueva = false;

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
            direccionActual = txt_direccion.Text;
            secundaria = txt_Secundaria.Text;
            referencia = txt_Referencia.Text;

        }
        public Int64 preCodigo = 0;
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ultimaAtencion != null)
            {
                validacionEstado = NegAtenciones.RecuperarAtencionID(ultimaAtencion.ATE_CODIGO);
                if (validacionEstado != null)
                {
                    if (validacionEstado.ESC_CODIGO == 6)
                    {
                        MessageBox.Show("Paciente ya fue facturado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            try
            {
                PACIENTES pacienteExiste = NegPacientes.RecuperarPacienteID(txt_historiaclinica.Text.Trim());
                if (pacienteExiste != null)
                {
                    if (pacienteActual != null)
                    {
                        if (pacienteActual.PAC_CODIGO != pacienteActual.PAC_CODIGO)
                        {
                            MessageBox.Show("El número de historia clinica ya existe, por favor ingrese otro número ", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txt_historiaclinica.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("El número de historia clinica ya existe, por favor ingrese otro número ", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txt_historiaclinica.Text = "";
                        NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                        numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 6).FirstOrDefault();

                        if (numerocontrol.TIPCON == "A")
                        {
                            txt_historiaclinica.Text = numerocontrol.NUMCON.ToString().Trim();
                            txt_historiaclinica.ReadOnly = true;
                        }
                        else
                        {
                            txt_historiaclinica.ReadOnly = false;
                        }
                        txt_numeroatencion.Enabled = false;
                        DataTable ATE_NUMERO_CONTROL = new DataTable();
                        ATE_NUMERO_CONTROL = NegNumeroControl.RecuperaNumeroControlPablo();
                        //numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
                        //int ATE_NUMERO_CONTROL = Convert.ToInt16(numerocontrol.NUMCON.ToString()) + 1;
                        txt_numeroatencion.Text = ATE_NUMERO_CONTROL.Rows[0][0].ToString();
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
                            txt_historiaclinica.Text = pacienteActual.PAC_HISTORIA_CLINICA.Trim();
                        }
                    }
                }
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message);
            }
            ////////////////////////////////////////////////////////////////////////////////////

            txt_historiaclinica.Focus();
            if (validarFormulario() == true)
            {

                if (cmb_tipoingreso.Text == "PREADMISION")
                {
                    NegAtenciones.actualizaEscCodigoPreadmision(Convert.ToInt32(txt_numeroatencion.Text), 14);
                }
                else
                {
                    NegAtenciones.actualizaEscCodigoPreadmision(Convert.ToInt32(txt_numeroatencion.Text), 1);
                }

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
                btnPr.Enabled = false;
                //atencionNueva = false;
                try
                {
                    guardarDatos();
                    CargarDatosAdicionales2(pacienteActual.PAC_CODIGO); //lx202005
                    CargarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);
                    CargarAtencionesPaciente(pacienteActual.PAC_CODIGO);
                    CargarAtencion();
                }
                catch (Exception ex)
                {
                    NegPacientes.EliminarPaciente(pacienteActual.PAC_CODIGO);
                    MessageBox.Show("Error de concetividad con servidor vuelva a iniciar la aplicación", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }



                deshabilitarCamposPaciente();
                deshabilitarCamposDireccion();
                deshabilitarCamposAtencion();
                btnGuardar.Enabled = false;
                txt_historiaclinica.ReadOnly = true;
                convenioA = false;
                gb_consultaRegistro.Visible = false;
                MessageBox.Show("Datos Almacenados Correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ImpresionFacturaANombreDe();
                NegValidaciones.alzheimer();
                if (preAdmision)
                {
                    //QUITO EL ESTADO DE LA PREADMISION
                    if (!NegPreadmision.cambioEstadoPreadmsion(preCodigo))
                        MessageBox.Show("Algo ocurrio al cambiar estado en la preadmision", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Información Incompleta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    pdf.clinica = txt_historiaclinica.Text.Trim();
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
            AtencionSubsecuente = 0;
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
            txt_parentRef.Text = string.Empty.Trim();
            txt_telfRef.Text = string.Empty;
            txtRefDireccion.Text = string.Empty;
            //cmb_pais.SelectedIndex = 0;
            //cb_etnia.SelectedIndex = 0;
            //cb_gruposanguineo.SelectedIndex = 0;
            registroCambios.DataSource = null;
            gridAtenciones.DataSource = null;
            rbCedula.Checked = true;

            cb_gruposanguineo.SelectedIndex = -1;
            txt_telfRef2.Text = string.Empty;  //lx202005
            dtpFallecido.Value = DateTime.Now;//lx202005
            chkFallecido.Checked = false;//lx202005
            txtFolio.Text = string.Empty;
            txt_emailAcomp.Text = "";
        }

        private void limpiarCamposDireccion()
        {
            txt_direccion.Text = string.Empty;
            txt_Secundaria.Text = string.Empty;
            txt_Referencia.Text = string.Empty;
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
            txtFCedula.Text = string.Empty;
            txtFCelular.Text = string.Empty;
            txtFDireccion.Text = string.Empty;
            txtFEmail.Text = string.Empty;
            txtFNombre.Text = string.Empty;
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

            if (cmb_tipoingreso.SelectedIndex >= 0)
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
            //gridAseguradoras.Enabled = false;

            txt_nombreTitular.Text = string.Empty;
            txt_DireccionTitular.Text = string.Empty;
            txt_CedulaTitular.Text = string.Empty;
            if (aux == 0)
            {
                this.cmbAccidente.SelectedIndex = 0;
                if (cmb_Parentesco.SelectedIndex >= 0)
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
            if (cmbTipoAtencion.Items.Count > 0)
                cmbTipoAtencion.SelectedIndex = 0;

            //Cambios edgar 20210326 se corrige que el combo no repita los datos que va añadiendo
            DataTable dt2 = NegAtenciones.TiposDiscapacidades();
            cmbTiposDiscapacidadesDA.DataSource = dt2;
            cmbTiposDiscapacidadesDA.ValueMember = "id";
            cmbTiposDiscapacidadesDA.DisplayMember = "name";
            cmbTiposDiscapacidadesDA.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTiposDiscapacidadesDA.SelectedIndex = 0;

            cboPaquete.DataSource = NegAtenciones.TiposPaquetes();
            cboPaquete.DisplayMember = "CAC_NOMBRE";
            cboPaquete.ValueMember = "CAC_CODIGO";
            //cboPaquete.Items.Clear();
            //cboPaquete.Items.Add("Ninguno");
            //for (int i = 0; i < dt3.Rows.Count; i++)
            //{
            //    cboPaquete.Items.Add(dt3.Rows[i]["id"].ToString() + "  - " + dt3.Rows[i]["name"].ToString());
            //}
            cboPaquete.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPaquete.SelectedIndex = -1;

            if (cmb_tipoingreso.SelectedValue.ToString() == "4" || cmb_tipoingreso.SelectedValue.ToString() == "10" || cmb_tipoingreso.SelectedValue.ToString() == "12")
            {
                try
                {
                    if (txt_numeroatencion.Text != "")
                    {
                        cmbConvenioPago.SelectedIndex = 1;
                        if (cmb_tipoingreso.SelectedValue.ToString() != "4")
                            txt_obPago.Text = "EFECTIVO";
                        else
                            txt_obPago.Text = "";
                        DataGridViewRow dt1 = new DataGridViewRow();
                        dt1.CreateCells(gridAseguradoras);
                        CATEGORIAS_CONVENIOS cat = (CATEGORIAS_CONVENIOS)cb_seguros.SelectedItem;
                        if (cat == null)
                        {
                            MessageBox.Show("Convenio seleccionado está caducado o no está vigente por favor consulte con el personal de sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        dt1.Cells[0].Value = cat.CAT_CODIGO.ToString();
                        dt1.Cells[1].Value = cat.CAT_NOMBRE;
                        string cod = dt1.Cells[0].Value.ToString();
                        bool band = false;
                        for (int i = 0; i < gridAseguradoras.Rows.Count; i++)
                            if (gridAseguradoras.Rows[i].Cells[0].Value.ToString() == cod)
                                band = true;
                        if (band == false)
                            gridAseguradoras.Rows.Add(dt1);
                        cb_personaFactura.SelectedIndex = 0;
                        cmb_tiporeferido.SelectedIndex = 0;
                    }
                }
                catch { }
            }
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
            //CAmbios Edgar 20210326
            cmbTiposDiscapacidadesDA.SelectedValue = codigo;
            //string dt2 = NegAtenciones.TipoDiscapacidad(codigo);
            //cmbTiposDiscapacidadesDA.Items.Clear();
            //cmbTiposDiscapacidadesDA.Items.Add(dt2);
            //cmbTiposDiscapacidadesDA.DropDownStyle = ComboBoxStyle.DropDownList;
            //cmbTiposDiscapacidadesDA.SelectedIndex = 0;

        }
        private void cargar_cbotipopaquete(int codigo) //lx202005
        {
            cboPaquete.DataSource = NegAtenciones.TiposPaquetes();
            cboPaquete.DisplayMember = "CAC_NOMBRE";
            cboPaquete.ValueMember = "CAC_CODIGO";
            cboPaquete.SelectedIndex = -1;
            //cboPaquete.Items.Clear();
            if (ateDA.paquete.Trim() != "")
            {
                string CAC_NOMBRE = NegAtenciones.TipoPaquete(codigo);
                Int32 index = cboPaquete.FindString(CAC_NOMBRE);
                cboPaquete.SelectedIndex = index;
            }
            //cboPaquete.Items.Add("Ninguno");// se comenta para funcionar con el nuevo codigo y de una mejor manera paquetes// MarioValencia // 16-04-2024
            //DataTable dt3 = NegAtenciones.TiposPaquetes();
            //cboPaquete.Items.Add("Ninguno");
            //for (int i = 0; i < dt3.Rows.Count; i++)
            //{
            //    cboPaquete.Items.Add(dt3.Rows[i]["id"].ToString() + "  - " + dt3.Rows[i]["name"].ToString());
            //}
            //cboPaquete.DropDownStyle = ComboBoxStyle.DropDownList;

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


            //DataTable dt2 = NegAtenciones.TiposDiscapacidades();
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{
            //    cmbTiposDiscapacidadesDA.Items.Add(dt2.Rows[i]["id"].ToString() + "  - " + dt2.Rows[i]["name"].ToString());
            //}
            //cmbTiposDiscapacidadesDA.DropDownStyle = ComboBoxStyle.DropDownList;
            //cmbTiposDiscapacidadesDA.SelectedIndex = 0;




            ////Cambios edgar 20210326 se corrige que el combo no repita los datos que va añadiendo
            //DataTable dt2 = NegAtenciones.TiposDiscapacidades();
            //cmbTiposDiscapacidadesDA.DataSource = dt2;
            //cmbTiposDiscapacidadesDA.ValueMember = "id";
            //cmbTiposDiscapacidadesDA.DisplayMember = "name";



        }





        private void habilitarCamposPaciente()
        {
            txt_apellido1.ReadOnly = false;
            txt_apellido2.ReadOnly = false;
            txt_nombre1.ReadOnly = false;
            txt_nombre2.ReadOnly = false;
            txt_cedula.ReadOnly = false;
            dtp_fecnac.Enabled = true;
            txt_nacionalidad.ReadOnly = true;
            cb_etnia.Enabled = true;
            cb_gruposanguineo.Enabled = true;
            rbPasaporte.Enabled = true;
            rbRuc.Enabled = true;
            rbCedula.Enabled = true;
            rbSid.Enabled = true;
            txt_cedula.ReadOnly = false;
            dtp_fecnac.Enabled = true;
            txt_nacionalidad.ReadOnly = true;
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
            txtFolio.ReadOnly = false;
            txt_emailAcomp.ReadOnly = false;

            gridAseguradoras.Enabled = true;

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
            txtFolio.ReadOnly = true;
            txt_emailAcomp.ReadOnly = true;
        }


        private void habilitarCamposDireccion()
        {
            txt_codPais.ReadOnly = false;
            txt_direccion.ReadOnly = false;
            txt_Secundaria.ReadOnly = false;
            txt_Referencia.ReadOnly = false;
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
            txtFCedula.ReadOnly = false;
            txtFCelular.ReadOnly = true;
            txtFDireccion.ReadOnly = true;
            txtFEmail.ReadOnly = true;
            txtFNombre.ReadOnly = true;
        }

        private void deshabilitarCamposDireccion()
        {
            txt_codPais.ReadOnly = true;
            txt_codProvincia.ReadOnly = true;
            txt_codCanton.ReadOnly = true;
            txt_codParroquia.ReadOnly = true;
            txt_codBarrio.ReadOnly = true;
            txt_direccion.ReadOnly = true;
            txt_Secundaria.ReadOnly = true;
            txt_Referencia.ReadOnly = true;
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
            txtFCedula.ReadOnly = true;
            txtFCelular.ReadOnly = true;
            txtFDireccion.ReadOnly = true;
            txtFEmail.ReadOnly = true;
            txtFNombre.ReadOnly = true;
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
            btn_Subsecuente.Enabled = true;

            chkDiscapacidad.Enabled = true;//lx2020
            if (!chkDiscapacidad.Checked)
            {
                txtIdDiscapacidad.Enabled = false;//lx2020
                cmbTiposDiscapacidadesDA.Enabled = false;//lx2020
                txtPorcentageDA.Enabled = false;
            }
            txtObservacionDA.Enabled = true;//lx2020
            txtEmpresaDA.Enabled = true;//lx2020            
            cmb_Parentesco.Enabled = true;//lx201
            cmbTipoAtencion.Enabled = true;//lx201ç
            btnfacturaDatos.Enabled = true;
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
            dateTimeFecIngreso.Enabled = true;
            cb_seguros.Enabled = false;
            txtPorcentageDA.Enabled = false;//lx202005
            txtObservacionDA.Enabled = false;//lx202005
            txtEmpresaDA.Enabled = false;//lx202005
            chkDiscapacidad.Enabled = false;//lx2020
            txtIdDiscapacidad.Enabled = false;//lx2020
            cb_tipoGarantia.Enabled = false;
            cb_seguros.Enabled = false;
            btn_imprimir_datos_facturar_a.Visible = true;
            btnAddAseg.Enabled = false;
            btnAddGar.Enabled = false;
            uBtnAddAcompaniante.Enabled = false;
            btnfacturaDatos.Enabled = false;


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
            //cmb_tipoingreso.SelectedIndex = 4;
            cmb_formaLlegada.SelectedIndex = 1;
            cmb_tiporeferido.SelectedIndex = 0;
            cmb_tipoatencion.SelectedIndex = 0;
            cmbConvenioPago.SelectedIndex = 0;
            cmb_Parentesco.SelectedIndex = -1;
            cb_personaFactura.SelectedIndex = -1;

        }

        private void limpiarCamposPreAtencion()
        {
            chb_PreAdmision.Checked = false;
            dtpPreAdmision.Value = DateTime.Now;
        }
        private void habilitarCamposPreAtencion()
        {
            //chb_PreAdmision.Enabled = true;
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
                //Cambios Edgar 20210210
                NegFactura Fac = new NegFactura();
                short codigo_hab = 0;
                if (ultimaAtencion != null)
                {
                    codigo_hab = Fac.RecuperarHabitacion(ultimaAtencion.ATE_CODIGO);
                }
                short estado = Fac.RecuperarEstado(Convert.ToInt16(Sesion.codHabitacion));
                ////habitaciones = NegHabitaciones.
                if (chkDiscapacidad.Checked)
                {
                    if (Convert.ToInt32(txtPorcentageDA.Text) <= 0 || Convert.ToInt32(txtPorcentageDA.Text) > 100)
                    {
                        erroresPaciente.SetError(txtPorcentageDA, "Debe agregar un valor mayor a 0 de % de discapacidad.");
                        valido = false;
                    }
                    if (cmbTiposDiscapacidadesDA.SelectedValue.ToString() == "1")
                    {
                        erroresPaciente.SetError(cmbTiposDiscapacidadesDA, "Debe elegir el tipo de discapacidad, no puede ser NINGUNA");
                        valido = false;
                    }
                }

                //Valida cuando el combo de pais es vacio
                if (cmb_pais.SelectedIndex == -1)
                {
                    erroresPaciente.SetError(cmb_pais, "Debe asignar el país de nacimiento");
                    valido = false;
                }
                if (Convert.ToInt32(cmb_tiporeferido.SelectedValue) == 3)
                {
                    erroresPaciente.SetError(cmb_tiporeferido, "Debe elegir el tipo de referido, no puede ser vacio.");
                    valido = false;
                }
                if (cb_personaFactura.SelectedIndex == -1)
                {
                    erroresPaciente.SetError(cb_personaFactura, "Debe elegir a nombre de quien se facturará.");
                    valido = false;
                }
                if (cmb_estadocivil.SelectedIndex == -1)
                {
                    erroresPaciente.SetError(cmb_estadocivil, "Seleccione Estado Civil.");
                    valido = false;
                }
                if (cb_gruposanguineo.SelectedIndex == -1 || cb_gruposanguineo.Text == "vacio")
                {
                    erroresPaciente.SetError(cb_gruposanguineo, "Seleccione Grupo Sanguineo.");
                    valido = false;
                }
                if (txt_ocupacion.Text == "")
                {
                    erroresPaciente.SetError(txt_ocupacion, "Ingrese Ocupación.");
                    valido = false;
                }
                if (txt_instuccion.Text == "")
                {
                    erroresPaciente.SetError(txt_instuccion, "Ingrese Instrucción.");
                    valido = false;
                }


                if (chkFallecido.Checked)
                {
                    //if(txtFolio.Text.Trim() == "")
                    //{
                    //    erroresPaciente.SetError(txtFolio, "Debe agregar nro de folio para el paciente fallecido");
                    //    valido = false;
                    //}
                    if (ComprobarFormatoEmail(txt_emailAcomp.Text))
                    {
                        txt_emailAcomp.Focus();
                    }
                    else
                    {
                        erroresPaciente.SetError(txt_emailAcomp, "Email de acompañante no es válido.");
                        valido = false;
                    }
                    if (txt_emailAcomp.Text.Trim() == "")
                    {
                        erroresPaciente.SetError(txt_emailAcomp, "Debe agregar email de acompañante.");
                        valido = false;
                    }
                    //if (Convert.ToInt32(txtPorcentageDA.Text) <= 0)
                    //{
                    //    erroresPaciente.SetError(txtPorcentageDA, "Debe agregar un valor mayor a 0 de % de discapacidad.");
                    //    valido = false;
                    //}
                }

                //Valida que sea hospitalizacion
                if (txt_nombreRef.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txt_nombreRef, "Debe agregar nombre de Referencia en caso de Emergencia.");
                    valido = false;
                }
                if (txt_parentRef.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txt_parentRef, "Debe agregar parentesco de referencia en caso de Emergencia.");
                    valido = false;
                }
                if (txt_telfRef.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txt_telfRef, "Debe agregar telefono de referencia en caso de Emergencia");
                    valido = false;
                }
                if (txtRefDireccion.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txtRefDireccion, "Debe agregar direccion de referencia en caso de Emergencia");
                    valido = false;
                }
                if (cmb_tipoingreso.Text == "HOSPITALIZACION")
                {
                }
                if (txt_cedulaAcomp.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txt_cedulaAcomp, "Camporequerido Para facturar");
                    valido = false;
                }

                if (txt_nombreAcomp.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txt_nombreAcomp, "Camporequerido Para facturar");
                    valido = false;
                }
                if (txt_direccionAcomp.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txt_direccionAcomp, "Camporequerido Para facturar");
                    valido = false;
                }

                if (txt_telefonoAcomp.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txt_telefonoAcomp, "Camporequerido Para facturar");
                    valido = false;
                }
                if (txt_ciudadAcomp.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txt_ciudadAcomp, "Camporequerido Para facturar");
                    valido = false;
                }
                //if (txt_parentescoAcomp.Text.Trim() == "")
                //{
                //    erroresPaciente.SetError(txt_parentescoAcomp, "Camporequerido Para facturar");
                //    valido = false;
                //    //if (cb_personaFactura.Text == "Persona" || cb_personaFactura.Text == "Otros")
                //    //{

                //    //}
                //}
                if (txt_emailAcomp.Text.Trim() == "")
                {
                    erroresPaciente.SetError(txt_emailAcomp, "Paciente menor de edad, agregue datos del acompañante.");
                    valido = false;
                }
                if (ComprobarFormatoEmail(txt_emailAcomp.Text) == false)
                {
                    erroresPaciente.SetError(txt_emailAcomp, "Email de acompañante no es válido.");
                    valido = false;
                    txt_emailAcomp.Text = string.Empty;
                    txt_emailAcomp.Focus();
                }
                //if ((DateTime.Now.Year - dtp_fecnac.Value.Year) >= 0 && (DateTime.Now.Year - dtp_fecnac.Value.Year) <= 17) //Menor de Edad
                //{
                //}
                //if ((DateTime.Now.Year - dtp_fecnac.Value.Year) >= 70)
                //{
                //    if (txt_nombreAcomp.Text.Trim() == "")
                //    {
                //        erroresPaciente.SetError(txt_nombreAcomp, "Paciente mayor a 70 de edad, agregue datos del acompañante.");
                //        valido = false;
                //    }
                //    if (txt_direccionAcomp.Text.Trim() == "")
                //    {
                //        erroresPaciente.SetError(txt_direccionAcomp, "Paciente mayor a 70 de edad, agregue datos del acompañante.");
                //        valido = false;
                //    }
                //    if (txt_cedulaAcomp.Text.Trim() == "")
                //    {
                //        erroresPaciente.SetError(txt_cedulaAcomp, "Paciente mayor a 70 de edad, agregue datos del acompañante.");
                //        valido = false;
                //    }
                //    else if (txt_telefonoAcomp.Text.Trim() == "")
                //    {
                //        erroresPaciente.SetError(txt_telefonoAcomp, "Paciente mayor a 70 de edad, agregue datos del acompañante.");
                //        valido = false;
                //    }
                //    if (txt_ciudadAcomp.Text.Trim() == "")
                //    {
                //        erroresPaciente.SetError(txt_ciudadAcomp, "Paciente mayor a 70 de edad, agregue datos del acompañante.");
                //        valido = false;
                //    }
                //    if (txt_parentescoAcomp.Text.Trim() == "")
                //    {
                //        if (cb_personaFactura.Text == "Persona" || cb_personaFactura.Text == "Otros")
                //        {
                //            erroresPaciente.SetError(txt_parentescoAcomp, "Paciente mayor a 70 de edad, agregue datos del acompañante.");
                //            valido = false;
                //        }
                //    }
                //    if (txt_emailAcomp.Text.Trim() == "")
                //    {
                //        erroresPaciente.SetError(txt_emailAcomp, "Paciente mayor a 70 de edad, agregue datos del acompañante.");
                //        valido = false;
                //    }
                //    if (ComprobarFormatoEmail(txt_emailAcomp.Text) == true)
                //    {
                //        txt_emailAcomp.Focus();
                //    }
                //    else
                //    {
                //        erroresPaciente.SetError(txt_emailAcomp, "Email de acompañante no es válido.");
                //        valido = false;
                //        txt_emailAcomp.Text = string.Empty;
                //    }
                //}
                if (cmb_ciudadano.Text.Trim() != "RELIGIOSO")
                {
                    if (txt_obPago.Text.Trim() == "")
                    {
                        erroresPaciente.SetError(txt_obPago, "Debe agregar observacion de pago.");
                        AgregarError(txt_obPago);
                        valido = false;
                    }
                }
                //-----------------------------------------------

                //if (cmb_ciudadano.SelectedText == "RELIGIOSO")

                if (cmb_tiporeferido.Text == "PRIVADO")
                {
                    if (txtCodMedico.Text.Trim() == "0")
                    {
                        erroresPaciente.SetError(txtNombreMedico, "Seleccione otro médico, no puede ser el genérico.");
                        valido = false;
                    }
                }
                if (txtCodMedico.Text.Trim() == string.Empty)
                {
                    erroresPaciente.SetError(txtNombreMedico, "Debe agregar médico tratante para el paciente.");
                    valido = false;
                }
                //VALIDA CONVENIOS 
                if (gridAseguradoras.RowCount <= 0)
                {
                    erroresPaciente.SetError(gridAseguradoras, "Debe agregar un convenio como minimo.");
                    valido = false;
                }
                else
                {
                    bool accion = false;
                    for (Int16 i = 0; i < gridAseguradoras.RowCount; i++)
                    {
                        DataGridViewRow fila = gridAseguradoras.Rows[i];

                        Int16 cat_codigo = Convert.ToInt16(fila.Cells[0].Value.ToString());
                        string seguro = fila.Cells[1].Value.ToString();
                        if (NegAtenciones.ValidarAsegurador(cat_codigo))
                        {
                            if (txt_CedulaTitular.Text.Trim() == "")
                            {
                                erroresPaciente.SetError(txt_CedulaTitular, "Obligatorio campos del titular del seguro: " + seguro);
                                valido = false;
                                return valido;
                            }

                        }
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
                    //if (accion)
                    //MessageBox.Show("No se ingreso Monto del Convenio", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }

                //CAMPOS PACIENTE

                if (txt_historiaclinica.Text.Trim() == string.Empty)
                {
                    erroresPaciente.SetError(txt_historiaclinica, "Debe asignar Nro de historia clínica.");
                    valido = false;
                }
                //if (txt_CedulaTitular.Text.Trim() == string.Empty && cmbConvenioPago.Text == "ASEGURADORA")
                //{
                //    tabulador2.SelectedTab = tabulador2.Tabs["titular"];
                //    erroresPaciente.SetError(txt_CedulaTitular, "Debe agregar nro de identificacion del titular de la aseguradora.");
                //    valido = false;
                //}

                if (txt_apellido1.Text.Trim() == string.Empty)
                {
                    erroresPaciente.SetError(txt_apellido1, "Debe agregar el apellido del paciente.");
                    valido = false;
                }

                if (txt_nombre1.Text.Trim() == string.Empty)
                {
                    erroresPaciente.SetError(txt_nombre1, "Debe agregar el nombre del paciente.");
                    valido = false;
                }
                if (txt_cedula.Text.Trim() == string.Empty)
                {
                    erroresPaciente.SetError(txt_cedula, "Debe agregar nro de identificación del paciente.");
                    valido = false;
                }
                if (dtp_fecnac.Value > DateTime.Now)
                {
                    erroresPaciente.SetError(dtp_fecnac, "Fecha de nacimiento no puede ser mayor a fecha actual.");
                    valido = false;
                }
                //if (txt_nacionalidad.Text.Trim() == string.Empty)
                //{
                //    AgregarError(txt_nacionalidad);
                //    valido = false;
                //}
                if (cb_etnia.SelectedItem == null)
                {
                    erroresPaciente.SetError(cb_etnia, "Debe elegir la etnia del paciente.");
                    valido = false;
                }
                if (cb_gruposanguineo.SelectedItem == null)
                {
                    erroresPaciente.SetError(cb_gruposanguineo, "Debe elegir el tipo de sangre del paciente");
                    valido = false;
                }

                // campos SEXO
                if (rbn_h.Checked == false && rbn_m.Checked == false)
                {
                    erroresPaciente.SetError(rbn_h, "Debe elegir el sexo del paciente.");
                    valido = false;
                }

                //CAMPOS DIRECCION

                if (txt_pais.Text == string.Empty)
                {
                    erroresPaciente.SetError(txt_pais, "Debe asignar el país de localización del paciente.");
                    valido = false;
                }
                else if (txt_codPais.Text == "57")
                {
                    if (txt_provincia.Text == string.Empty)
                    {
                        erroresPaciente.SetError(txt_provincia, "Debe asignar la provincia de localización del paciente.");
                        valido = false;
                    }
                    if (txt_direccion.Text == string.Empty)
                    {
                        erroresPaciente.SetError(txt_direccion, "Debe asignar la direccion de localización del paciente.");
                        valido = false;
                    }
                }
                if (txt_direccion.Text == string.Empty)
                {
                    erroresPaciente.SetError(txt_direccion, "Debe asignar la direccion de localización del paciente.");
                    valido = false;
                }
                if (txt_Secundaria.Text == string.Empty)
                {
                    erroresPaciente.SetError(txt_Secundaria, "Debe asignar la calle secundaria de localización del paciente.");
                    valido = false;
                }
                if (txt_Referencia.Text == string.Empty)
                {
                    erroresPaciente.SetError(txt_Referencia, "Debe asignar la referencia de localización del paciente.");
                    valido = false;
                }
                if (txt_telefono2.Text == "  -   -")
                {
                    erroresPaciente.SetError(txt_telefono2, "Debe asignar numero de telefono/celular del paciente.");
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
                //if(txt.Text.Trim() != "")
                //{
                //    txtFEmail.Text = txtFEmail.Text.Trim();
                //    if (ComprobarFormatoEmail(txtFEmail.Text) != true)
                //    {
                //        MessageBox.Show("DIRECCIÓN DE CORREO ELECTRONICO NO ES VALIDA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        txtFEmail.Text = string.Empty;
                //        AgregarError(txtFEmail);
                //        valido = false;
                //    }
                //}
                //CAMPOS ATENCION
                if (txt_numeroatencion.Text != string.Empty)
                {

                    if (dateTimeFecIngreso.Value == null)
                    {
                        erroresPaciente.SetError(dateTimeFecIngreso, "Debe agregar fecha de ingreso del paciente");
                        valido = false;
                    }
                    if (cmb_tipoingreso.SelectedIndex != 2)
                    {
                        if (txt_habitacion.Text == string.Empty)
                        {
                            erroresPaciente.SetError(txt_habitacion, "Debe elegir la habitación para el paciente.");
                            valido = false;
                        }
                        else if (estado != 5 && codigo_hab == 0)
                        {
                            if (txt_habitacion.Text != "PREA" && txt_habitacion.Text != "PROCE")
                            {
                                erroresPaciente.SetError(txt_habitacion, "La habitacion ya ha sido asignada a otro paciente, Eligir otra.");
                                valido = false;
                            }
                        }
                    }
                    if (cb_personaFactura.SelectedIndex != -1)
                    {
                        if (cb_personaFactura.SelectedItem.ToString() == "OTROS")
                        {
                            if (txt_nombreAcomp.Text == string.Empty)
                            {
                                AgregarError(txt_nombreAcomp);
                                valido = false;
                            }
                            bool validoCedula = true;
                            if (radioButton1.Checked)
                                validoCedula = NegValidaciones.esCedulaValida(txt_cedulaAcomp.Text);
                            if (txt_cedulaAcomp.Text == string.Empty || !validoCedula)
                            {
                                AgregarError(txt_cedulaAcomp);
                                valido = false;
                            }
                            //if (txt_parentescoAcomp.Text == string.Empty)
                            //{
                            //    AgregarError(txt_parentescoAcomp);
                            //    valido = false;
                            //}
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
                            if (txt_emailAcomp.Text.Trim() == "")
                            {
                                AgregarError(txt_emailAcomp);
                                valido = false;
                            }
                            if (ComprobarFormatoEmail(txt_emailAcomp.Text) == true)
                            {
                                txt_emailAcomp.Focus();
                            }
                            else
                            {
                                erroresPaciente.SetError(txt_emailAcomp, "Email de persona ha facturar no es válido.");
                                txt_emailAcomp.Text = string.Empty;
                                valido = false;
                            }
                        }
                        if (cb_personaFactura.SelectedItem.ToString() == "RELIGIOSO")
                        {
                            if (txt_rucEmp.Text == string.Empty)
                            {
                                AgregarError(txt_rucEmp);
                                valido = false;
                            }
                        }

                    }


                    //if (cb_personaFactura.SelectedItem.ToString() == "GARANTE" || cb_personaFactura.SelectedItem.ToString() == "CONVENIO DE PAGO")
                    //{
                    //    if (txt_nombreGar.Text != string.Empty)
                    //    {
                    //        AgregarError(txt_nombreGar);
                    //        valido = false;
                    //    }

                    //    if (txt_cedulaGar.Text == string.Empty)
                    //    {
                    //        AgregarError(txt_cedulaGar);
                    //        valido = false;
                    //    }
                    //    if (txt_parentGar.Text == string.Empty)
                    //    {
                    //        AgregarError(txt_parentGar);
                    //        valido = false;
                    //    }
                    //    if (txt_montoGar.Text == string.Empty)
                    //    {
                    //        AgregarError(txt_montoGar);
                    //        valido = false;
                    //    }
                    //    if (txt_telfGar.Text == "   -   -")
                    //    {
                    //        AgregarError(txt_telfGar);
                    //        valido = false;
                    //    }
                    //    if (txt_dirGar.Text == string.Empty)
                    //    {
                    //        AgregarError(txt_dirGar);
                    //        valido = false;
                    //    }
                    //    if (txt_ciudadGar.Text == string.Empty)
                    //    {
                    //        AgregarError(txt_ciudadGar);
                    //        valido = false;
                    //    }
                    //}



                    if (cmb_tipoatencion.SelectedItem == null)
                    {
                        erroresPaciente.SetError(cmb_tipoatencion, "Debe elegir tipo de atención.");
                        AgregarError(cmb_tipoatencion);
                        valido = false;
                    }
                    if (cmb_tiporeferido.SelectedItem == null)
                    {
                        erroresPaciente.SetError(cmb_tiporeferido, "Debe elegir tipo de referido.");
                        AgregarError(cmb_tiporeferido);
                        valido = false;
                    }
                    if (medico == null)
                    {
                        erroresPaciente.SetError(txtNombreMedico, "Debe elegir nombre del médico.");
                        valido = false;
                    }
                    if (cmb_formaLlegada.SelectedItem == null)
                    {
                        erroresPaciente.SetError(cmb_formaLlegada, "Debe elegir forma de llegada.");
                        AgregarError(cmb_formaLlegada);
                        valido = false;
                    }
                }
                if (Convert.ToInt32(cmb_tipoingreso.SelectedValue) == 5)
                {
                    if (!validarFechasPreadmison())
                    {
                        erroresPaciente.SetError(cmb_formaLlegada, "Debe elegir forma de llegada.");
                        AgregarError(chb_PreAdmision);
                        valido = false;
                    }
                    if (txt_habitacion.Text != "PREA" && txt_habitacion.Text != "PROCE")
                    {
                        erroresPaciente.SetError(cmb_formaLlegada, "Debe elegir forma de llegada.");
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
                if (chkDiscapacidad.Checked)
                {
                    if (txtIdDiscapacidad.Text == "")
                    {
                        erroresPaciente.SetError(txtIdDiscapacidad, "Se necesita La identificación del carnet");
                        AgregarError(txtIdDiscapacidad);
                        valido = false;
                    }
                    //if (Convert.ToInt16(txtPorcentageDA.Text) > 0)
                    //{
                    //    erroresPaciente.SetError(txtIdDiscapacidad, "Se necesita cantidad de Discapacidad");
                    //    AgregarError(txtIdDiscapacidad);
                    //    valido = false;
                    //}
                    //if (cmbTiposDiscapacidadesDA.SelectedIndex == 1)
                    //{
                    //    erroresPaciente.SetError(cmbTiposDiscapacidadesDA, "Se necesita tipo de Discapacidad");
                    //    AgregarError(cmbTiposDiscapacidadesDA);
                    //    valido = false;
                    //}
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
            e.Handled = true;
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
            tabulador.Tabs["paciente"].Enabled = true;
            tabulador.Tabs["atencion"].Enabled = true;
            tabulador.Tabs["gridatenciones"].Enabled = true;
            if (char.IsNumber(e.KeyChar) || char.IsLetter(e.KeyChar) || Convert.ToInt32(e.KeyChar).ToString() == "8")
            {

            }
            else
            {
                e.Handled = true; // no deja que ingrese simbolos / Giovanny Tapia /24/08/2012
            }
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (rbCedula.Checked == true || rbtCedulaExt.Checked == true)
                {
                    if (txt_cedula.Text.ToString() != string.Empty)
                    {
                        try
                        {
                            if (rbCedula.Checked && txt_cedula.Text.Length == 10)
                            {
                                //int valida = 0;
                                ValidaNube ok = new ValidaNube();
                                ok = NegUtilitarios.ValidarDocumento(txt_cedula.Text);
                                if (ok != null)
                                {
                                    if (!ok.ok)
                                    {
                                        MessageBox.Show("Número de identifación no válida.", "HIS3000", MessageBoxButtons.OK);
                                        return;
                                    }
                                }
                                //validacion de si existe pre admision Mario 2022-06-07
                                //PREADMISION existe = NegPreadmision.recuperarPreadmision(txt_cedula.Text.Trim());
                                //if (existe != null)
                                //{
                                //    preadmision = existe;
                                //    MessageBox.Show("Este paciente ya tiene una pre admision", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}
                                //if (valida == 1)
                                //{
                                if (pacienteActual == null)
                                {
                                    PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());
                                    DialogResult resp = new DialogResult();
                                    if (consulta != null)
                                    {
                                        resp = MessageBox.Show("Esta identificación ya existe.\nDesea cargar el paciente con este ID?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (resp == DialogResult.No)
                                        {
                                            txt_cedula.Focus();
                                        }
                                        else
                                        {
                                            //CargarPaciente(consulta.PAC_HISTORIA_CLINICA.ToString());
                                            btnPr.Enabled = false;
                                            pacienteNuevo = false;
                                            txt_historiaclinica.Tag = true;
                                            txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString().Trim();

                                            CargarPaciente(txt_historiaclinica.Text.Trim(), 0);
                                            deshabilitarCamposAtencion();
                                            deshabilitarCamposDireccion();
                                            deshabilitarCamposPaciente();
                                            if (txt_historiaclinica.Text.Trim() == "")
                                            {
                                                consultas = false;
                                                btnGuardar.Enabled = false;
                                                btnHabitaciones.Enabled = false;
                                                btnFormularios.Enabled = false;
                                                btnActualizar.Enabled = false;
                                            }
                                            else
                                            {
                                                btnGuardar.Enabled = false;
                                                btnHabitaciones.Enabled = false;
                                                btnFormularios.Enabled = false;

                                                if (fallecido == true)
                                                    btnActualizar.Enabled = false;
                                                else
                                                {
                                                    btnActualizar.Enabled = true;
                                                    toolStripNuevo.Enabled = true;
                                                    btnEliminar.Visible = false;
                                                    tabulador.Tabs["paciente"].Enabled = true;
                                                    tabulador.Tabs["atencion"].Enabled = true;
                                                    tabulador.Tabs["gridatenciones"].Enabled = true;
                                                }

                                                consultas = true;
                                            }
                                        }
                                        return;
                                    }
                                    else
                                    {
                                        DataTable pacienteJire = new DataTable();
                                        pacienteJire = NegPacientes.PacienteJire(txt_cedula.Text.Trim());
                                        if (pacienteJire.Rows.Count > 0)
                                        {
                                            txt_historiaclinica.Text = pacienteJire.Rows[0][1].ToString();
                                            txt_apellido1.Text = pacienteJire.Rows[0][2].ToString();
                                            txt_apellido2.Text = pacienteJire.Rows[0][3].ToString();
                                            txt_nombre1.Text = pacienteJire.Rows[0][4].ToString();
                                            txt_nombre2.Text = pacienteJire.Rows[0][5].ToString();
                                            txt_direccion.Text = pacienteJire.Rows[0][6].ToString();
                                            dtp_fecnac.Value = Convert.ToDateTime(pacienteJire.Rows[0][7].ToString());
                                            txt_email.Text = pacienteJire.Rows[0][9].ToString();
                                            return;
                                        }
                                        else
                                        {
                                            cargarPacientePreAdmision();

                                            if (!preAdmision)
                                            {
                                                PARAMETROS_DETALLE parametroWEB = new PARAMETROS_DETALLE();
                                                parametroWEB = NegParametros.RecuperaPorCodigo(48);
                                                if ((bool)parametroWEB.PAD_ACTIVO)
                                                {
                                                    if (txt_apellido1.Text == "")
                                                    {
                                                        if (ok != null)
                                                            BuscaRegistroCivil(ok.ok);
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                                else if (pacienteActual.PAC_IDENTIFICACION != txt_cedula.Text.ToString())
                                {
                                    PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());

                                    DialogResult resp = new DialogResult();
                                    if (consulta != null)
                                    {
                                        resp = MessageBox.Show("Esta identificación ya existe.\nDesea cargar el paciente con este ID?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (resp == DialogResult.No)
                                        {
                                            txt_cedula.Focus();
                                        }
                                        else
                                        {
                                            //CargarPaciente(consulta.PAC_HISTORIA_CLINICA.ToString());
                                            pacienteNuevo = false;
                                            txt_historiaclinica.Tag = true;
                                            txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString().Trim();
                                        }
                                    }
                                }
                                //}

                            }
                            else
                            {
                                MessageBox.Show("Número de cédula no cumple con el formato de 10 digitos", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if (rbPasaporte.Checked)
                {
                    //cargarPacientePreAdmision();
                    PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());
                    DialogResult resp = new DialogResult();
                    if (consulta != null)
                    {
                        resp = MessageBox.Show("Esta identificación ya existe.\nDesea cargar el paciente con este ID?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resp == DialogResult.No)
                        {
                            txt_cedula.Focus();
                        }
                        else
                        {
                            //CargarPaciente(consulta.PAC_HISTORIA_CLINICA.ToString());
                            btnPr.Enabled = false;
                            pacienteNuevo = false;
                            txt_historiaclinica.Tag = true;
                            txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString().Trim();

                            CargarPaciente(txt_historiaclinica.Text.Trim(), 0);
                            deshabilitarCamposAtencion();
                            deshabilitarCamposDireccion();
                            deshabilitarCamposPaciente();
                            if (txt_historiaclinica.Text.Trim() == "")
                            {
                                consultas = false;
                                btnGuardar.Enabled = false;
                                btnHabitaciones.Enabled = false;
                                btnFormularios.Enabled = false;
                                btnActualizar.Enabled = false;
                            }
                            else
                            {
                                btnGuardar.Enabled = false;
                                btnHabitaciones.Enabled = false;
                                btnFormularios.Enabled = false;

                                if (fallecido == true)
                                    btnActualizar.Enabled = false;
                                else
                                {
                                    btnActualizar.Enabled = true;
                                    toolStripNuevo.Enabled = true;
                                    btnEliminar.Visible = false;
                                    tabulador.Tabs["paciente"].Enabled = true;
                                    tabulador.Tabs["atencion"].Enabled = true;
                                    tabulador.Tabs["gridatenciones"].Enabled = true;
                                }

                                consultas = true;
                            }
                        }
                    }
                    else
                    {
                        DataTable pacienteJire = new DataTable();
                        pacienteJire = NegPacientes.PacienteJire(txt_cedula.Text.Trim());
                        if (pacienteJire.Rows.Count > 0)
                        {
                            txt_historiaclinica.Text = pacienteJire.Rows[0][1].ToString();
                            txt_apellido1.Text = pacienteJire.Rows[0][2].ToString();
                            txt_apellido2.Text = pacienteJire.Rows[0][3].ToString();
                            txt_nombre1.Text = pacienteJire.Rows[0][4].ToString();
                            txt_nombre2.Text = pacienteJire.Rows[0][5].ToString();
                            txt_direccion.Text = pacienteJire.Rows[0][6].ToString();
                            dtp_fecnac.Value = Convert.ToDateTime(pacienteJire.Rows[0][7].ToString());
                            txt_email.Text = pacienteJire.Rows[0][9].ToString();
                        }
                        else
                            cargarPacientePreAdmision();
                    }
                }
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
                txt_codPais.Focus();
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                txt_codPais.Focus();
            }
        }

        private void rbn_m_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_codPais.Focus();
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                txt_codPais.Focus();
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
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab) || Convert.ToInt32(e.KeyChar).ToString() == "8")
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
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
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
                    //txt_telefonoAcomp.Focus();
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

        }

        public void RecuperaClienteSIC()
        {
            DataTable cliente = new DataTable();
            cliente = NegDetalleCuenta.RecuperaOtroCliente(txt_cedulaAcomp.Text);
            foreach (DataRow Item in cliente.AsEnumerable())
            {
                txt_nombreAcomp.Text = Item["nomcli"].ToString().Trim();
                txt_telefonoAcomp.Text = Item["telcli"].ToString().Trim();
                txt_direccionAcomp.Text = Item["dircli"].ToString().Trim();
                txt_emailAcomp.Text = Item["email"].ToString().Trim();
                txt_ciudadAcomp.Text = Item["ciudad"].ToString().Trim();
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
        public bool preAdmision = false;
        public void cargarPacientePreAdmision()
        {
            PREADMISION pre = new PREADMISION();
            pre = NegPreadmision.recuperarPreadmision(txt_cedula.Text.Trim());
            List<PREADMISION_DETALLE> pde = new List<PREADMISION_DETALLE>();
            if (pre != null)
            {
                pde = NegPreadmision.recuperarPreAdmisionDetalle(pre.PRE_CODIGO);

                txt_apellido1.Text = pre.PRE_APELLIDO1;
                txt_apellido2.Text = pre.PRE_APELLIDO2;
                txt_nombre1.Text = pre.PRE_NOMBRE1;
                txt_nombre2.Text = pre.PRE_NOMBRE2;
                txt_direccion.Text = pre.PRE_DIRECCION;
                txt_telefono1.Text = pre.PRE_TELEFONO;
                txt_telefono2.Text = pre.PRE_CELULAR;
                txt_email.Text = pre.PRE_EMAIL;
                preCodigo = pre.PRE_CODIGO;
                txtCodMedico.Text = pre.MED_CODIGO.ToString();
                medico = NegMedicos.recuperarMedico((int)pre.MED_CODIGO);
                txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                cmb_tiporeferido.SelectedItem = tipoReferido.FirstOrDefault(t => t.TIR_CODIGO == pre.TIR_CODIGO);
                cmb_tipoatencion.SelectedItem = tipoTratamiento.FirstOrDefault(t => t.TIA_CODIGO == pre.TIA_CODIGO);

                foreach (var item in pde)
                {
                    CIE10 c = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);
                    txt_diagnostico.Text = txt_diagnostico.Text + c.CIE_DESCRIPCION + " ";
                }
                preAdmision = true;
            }
        }


        private void txt_cedula_Leave_1(object sender, EventArgs e)
        {
            tabulador.Tabs["paciente"].Enabled = true;
            tabulador.Tabs["atencion"].Enabled = true;
            tabulador.Tabs["gridatenciones"].Enabled = true;
            if (rbCedula.Checked == true || rbtCedulaExt.Checked == true)
            {
                if (txt_cedula.Text.ToString() != string.Empty)
                {
                    try
                    {
                        if (rbCedula.Checked && txt_cedula.Text.Length == 10)
                        {
                            //int valida = 0;
                            //ValidaNube ok = new ValidaNube();
                            //ok = NegUtilitarios.ValidarDocumento(txt_cedula.Text);
                            //if (ok == null)
                            //{
                            //    valida = 1;
                            //    MessageBox.Show("En este momento no se puede recuperar información del Registro Civil", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //else
                            //{
                            //    if (!ok.ok)
                            //    {
                            //        MessageBox.Show("Número de identifación no válida.", "HIS3000", MessageBoxButtons.OK);
                            //        return;
                            //    }

                            //    valida = 1;

                            //}
                            //validacion de si existe pre admision Mario 2022-06-07
                            //PREADMISION existe = NegPreadmision.recuperarPreadmision(txt_cedula.Text.Trim());
                            //if (existe != null)
                            //{
                            //    preadmision = existe;
                            //    MessageBox.Show("Este paciente ya tiene una pre admision", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //if (valida == 1)
                            //{
                            if (pacienteActual == null)
                            {
                                PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());
                                DialogResult resp = new DialogResult();
                                if (consulta != null)
                                {
                                    resp = MessageBox.Show("Esta identificación ya existe.\nDesea cargar el paciente con este ID?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (resp == DialogResult.No)
                                    {
                                        txt_cedula.Focus();
                                    }
                                    else
                                    {
                                        atencionNueva = false;
                                        //CargarPaciente(consulta.PAC_HISTORIA_CLINICA.ToString());
                                        btnPr.Enabled = false;
                                        pacienteNuevo = false;
                                        txt_historiaclinica.Tag = true;
                                        txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString().Trim();

                                        CargarPaciente(txt_historiaclinica.Text.Trim(), 0);
                                        deshabilitarCamposAtencion();
                                        deshabilitarCamposDireccion();
                                        deshabilitarCamposPaciente();
                                        if (txt_historiaclinica.Text.Trim() == "")
                                        {
                                            consultas = false;
                                            btnGuardar.Enabled = false;
                                            btnHabitaciones.Enabled = false;
                                            btnFormularios.Enabled = false;
                                            btnActualizar.Enabled = false;
                                        }
                                        else
                                        {
                                            btnGuardar.Enabled = false;
                                            btnHabitaciones.Enabled = false;
                                            btnFormularios.Enabled = false;

                                            if (fallecido == true)
                                                btnActualizar.Enabled = false;
                                            else
                                            {
                                                btnActualizar.Enabled = true;
                                                toolStripNuevo.Enabled = true;
                                                btnEliminar.Visible = false;
                                                tabulador.Tabs["paciente"].Enabled = true;
                                                tabulador.Tabs["atencion"].Enabled = true;
                                                tabulador.Tabs["gridatenciones"].Enabled = true;
                                            }

                                            consultas = true;
                                        }
                                    }
                                }
                                else
                                {
                                    DataTable pacienteJire = new DataTable();
                                    pacienteJire = NegPacientes.PacienteJire(txt_cedula.Text.Trim());
                                    if (pacienteJire.Rows.Count > 0)
                                    {
                                        txt_historiaclinica.Text = pacienteJire.Rows[0][1].ToString();
                                        txt_apellido1.Text = pacienteJire.Rows[0][2].ToString();
                                        txt_apellido2.Text = pacienteJire.Rows[0][3].ToString();
                                        txt_nombre1.Text = pacienteJire.Rows[0][4].ToString();
                                        txt_nombre2.Text = pacienteJire.Rows[0][5].ToString();
                                        txt_direccion.Text = pacienteJire.Rows[0][6].ToString();
                                        dtp_fecnac.Value = Convert.ToDateTime(pacienteJire.Rows[0][7].ToString());
                                        txt_email.Text = pacienteJire.Rows[0][9].ToString();
                                    }
                                    else
                                        cargarPacientePreAdmision();
                                }

                            }
                            else if (pacienteActual.PAC_IDENTIFICACION != txt_cedula.Text.ToString())
                            {
                                PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());

                                DialogResult resp = new DialogResult();
                                if (consulta != null)
                                {
                                    resp = MessageBox.Show("Esta identificación ya existe.\nDesea cargar el paciente con este ID?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (resp == DialogResult.No)
                                    {
                                        txt_cedula.Focus();
                                    }
                                    else
                                    {
                                        //CargarPaciente(consulta.PAC_HISTORIA_CLINICA.ToString());
                                        pacienteNuevo = false;
                                        txt_historiaclinica.Tag = true;
                                        txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString().Trim();
                                    }
                                }
                            }
                            //}

                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (rbPasaporte.Checked)
            {
                //cargarPacientePreAdmision();
                PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());
                DialogResult resp = new DialogResult();
                if (consulta != null)
                {
                    resp = MessageBox.Show("Esta identificación ya existe.\nDesea cargar el paciente con este ID?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resp == DialogResult.No)
                    {
                        txt_cedula.Focus();
                    }
                    else
                    {
                        //CargarPaciente(consulta.PAC_HISTORIA_CLINICA.ToString());
                        atencionNueva = false;
                        btnPr.Enabled = false;
                        pacienteNuevo = false;
                        txt_historiaclinica.Tag = true;
                        txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString().Trim();

                        CargarPaciente(txt_historiaclinica.Text.Trim(), 0);
                        deshabilitarCamposAtencion();
                        deshabilitarCamposDireccion();
                        deshabilitarCamposPaciente();
                        if (txt_historiaclinica.Text.Trim() == "")
                        {
                            consultas = false;
                            btnGuardar.Enabled = false;
                            btnHabitaciones.Enabled = false;
                            btnFormularios.Enabled = false;
                            btnActualizar.Enabled = false;
                        }
                        else
                        {
                            btnGuardar.Enabled = false;
                            btnHabitaciones.Enabled = false;
                            btnFormularios.Enabled = false;

                            if (fallecido == true)
                                btnActualizar.Enabled = false;
                            else
                            {
                                btnActualizar.Enabled = true;
                                toolStripNuevo.Enabled = true;
                                btnEliminar.Visible = false;
                                tabulador.Tabs["paciente"].Enabled = true;
                                tabulador.Tabs["atencion"].Enabled = true;
                                tabulador.Tabs["gridatenciones"].Enabled = true;
                            }

                            consultas = true;
                        }
                    }
                }
                else
                {
                    DataTable pacienteJire = new DataTable();
                    pacienteJire = NegPacientes.PacienteJire(txt_cedula.Text.Trim());
                    if (pacienteJire.Rows.Count > 0)
                    {
                        txt_historiaclinica.Text = pacienteJire.Rows[0][1].ToString();
                        txt_apellido1.Text = pacienteJire.Rows[0][2].ToString();
                        txt_apellido2.Text = pacienteJire.Rows[0][3].ToString();
                        txt_nombre1.Text = pacienteJire.Rows[0][4].ToString();
                        txt_nombre2.Text = pacienteJire.Rows[0][5].ToString();
                        txt_direccion.Text = pacienteJire.Rows[0][6].ToString();
                        dtp_fecnac.Value = Convert.ToDateTime(pacienteJire.Rows[0][7].ToString());
                        txt_email.Text = pacienteJire.Rows[0][9].ToString();
                    }
                    else
                        cargarPacientePreAdmision();
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
                    txt_cedula.Text = txt_historiaclinica.Text.ToString().Trim();
                    txt_cedula.ReadOnly = true;
                    btnRegistroCivil.Enabled = false;
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

                    for (Int16 i = 0; i < gridAseguradoras.RowCount; i++)
                    {
                        DataGridViewRow fila = gridAseguradoras.Rows[i];

                        Int16 cat_codigo = Convert.ToInt16(fila.Cells[0].Value.ToString());
                        string seguro = fila.Cells[1].Value.ToString();
                        if (NegAtenciones.ValidarAsegurador(cat_codigo))
                        {
                            //Aqui valido que es seguro
                            if (fila.Cells[2].Value != null)
                                numPoliza = fila.Cells[2].Value.ToString();
                            else
                                numPoliza = "";
                            if (fila.Cells[1].Value != null)
                                nomAseg = fila.Cells[1].Value.ToString();
                            else
                                nomAseg = "";
                            if (fila.Cells[4].Value != null)
                                montoAseg = fila.Cells[4].Value.ToString();
                            else
                                montoAseg = "";
                            telfAseg = NegAtenciones.AseguradoraTelefono(cat_codigo);
                            //if (aseg.ASE_TELEFONO != null)
                            //    telfAseg = aseg.ASE_TELEFONO;
                        }
                        else
                        {
                            if (fila.Cells[2].Value != null)
                                numContrato = fila.Cells[2].Value.ToString();
                            else
                                numContrato = "";
                            if (fila.Cells[1].Value != null)
                                nomEmp = fila.Cells[1].Value.ToString();
                            else
                                nomEmp = "";
                            if (fila.Cells[4].Value != null)
                                montoEmp = fila.Cells[4].Value.ToString();
                            else
                                montoEmp = "";
                            telfEmp = NegAtenciones.AseguradoraTelefono(cat_codigo);
                        }
                    }



                    frmReportes form = new frmReportes();
                    form.reporte = "Contrato";
                    form.campo1 = ultimaAtencion.ATE_CODIGO.ToString();
                    form.campo2 = nombreContrato;
                    form.campo3 = numPoliza.Trim();
                    form.campo4 = nomAseg;
                    form.campo5 = montoAseg;
                    form.campo6 = telfAseg;
                    form.campo7 = numContrato;
                    form.campo8 = nomEmp;
                    form.campo9 = telfEmp;
                    form.campo10 = montoEmp;
                    form.campo13 = txt_email.Text;

                    //Cambios Edgar 20210414 fecha para el vencimiento de pagare
                    if (ultimaAtencion.ATE_FECHA_INGRESO.Value.Day > 15)
                    {
                        DateTime fechapagare = ultimaAtencion.ATE_FECHA_INGRESO.Value.AddDays(15);
                        if (fechapagare < ultimaAtencion.ATE_FECHA_INGRESO.Value)
                        {
                            fechapagare = fechapagare.AddMonths(1);
                            form.campo14 = fechapagare;
                        }
                        else
                            form.campo14 = fechapagare;
                    }
                    else
                        form.campo14 = ultimaAtencion.ATE_FECHA_INGRESO.Value.AddDays(15);
                    form.campo15 = numContrato;
                    form.campo16 = txt_telefono2.Text.Trim();
                    form.campo17 = txt_obPago.Text.Trim();
                    if (txt_direccion.Text.Trim() == txtRefDireccion.Text.Trim())
                    {
                        form.campo18 = txt_canton.Text.Trim();
                    }
                    else
                    {
                        form.campo18 = txt_canton.Text.Trim();
                    }
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
        public int _pais = 0;
        private void cmb_pais_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb_pais.SelectedIndex >= 0 && cargaciu == true)
                {
                    string codiinec = cmb_pais.SelectedValue.ToString();

                    DIVISION_POLITICA pais = paises.FirstOrDefault(p => p.DIPO_CODIINEC == codiinec);

                    cmb_ciudad.DataSource = null;
                    provincias = NegDivisionPolitica.RecuperarDivisionPolitica(pais.DIPO_CODIINEC);
                    cmb_ciudad.DataSource = provincias;
                    cmb_ciudad.ValueMember = "DIPO_CODIINEC";
                    cmb_ciudad.DisplayMember = "DIPO_NOMBRE";
                    //cmb_ciudad.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //cmb_ciudad.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                    if (pacienteNuevo == true && cmb_pais.SelectedValue.ToString() == "57")
                    {
                        //if (pais.NACIONALIDAD != null && pais.NACIONALIDAD != string.Empty)
                        //    txt_nacionalidad.Text = pais.NACIONALIDAD;
                        //else
                        //    txt_nacionalidad.Text = string.Empty;

                        if (pais.DIPO_CODIINEC == AdmisionParametros.DefPais)
                        {
                            cmb_ciudad.SelectedValue = Convert.ToString(NegParametros.RecuperaValorParSvXcodigo(73)); ;// SE CAMBIA A PARAMETRO POR CODIGO QUEMADO // Mario Valencia // 12-02-2023
                            txt_codPais.Text = AdmisionParametros.DefPais;
                            txt_codProvincia.Text = Convert.ToString(NegParametros.RecuperaValorParSvXcodigo(73));
                            txt_codCanton.Text = Convert.ToString(NegParametros.RecuperaValorParSvXcodigo(74));
                            txt_nacionalidad.Text = "ECUATORIANO";
                        }
                        else
                        {
                            txt_codPais.Text = string.Empty;
                            txt_codProvincia.Text = string.Empty;
                            txt_codCanton.Text = string.Empty;
                        }

                    }
                    else
                    {
                        if (cmb_pais.SelectedValue.ToString() == "57")
                            txt_nacionalidad.Text = "ECUATORIANO";
                        else
                        {
                            txt_nacionalidad.Text = "";
                            cmb_ciudad.Text = cmb_pais.Text;
                            txt_nacionalidad.Text = cmb_pais.Text;
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
                _pais++;
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
            {
                txt_cedula.MaxLength = 10;
                btnRegistroCivil.Enabled = true;
            }
        }

        private void rbPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPasaporte.Checked == true)
            {
                txt_cedula.MaxLength = 13;
                btnRegistroCivil.Enabled = false;
                if (txt_cedula.Text != "")
                    validaPasaporte();
            }
        }

        private void rbRuc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRuc.Checked == true)
                txt_cedula.MaxLength = 13;
        }



        private void txt_cedulaAcomp_KeyPress_1(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (radioButton1.Checked)
                {
                    if (txt_cedulaAcomp.Text != string.Empty)
                    {

                        PARAMETROS_DETALLE parametro = NegParametros.RecuperaPorCodigo(48);
                        if ((bool)parametro.PAD_ACTIVO)
                        {
                            if (MessageBox.Show("Desea consultar los datos del cliente en la nube?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                CONTROL_CONSULTA obj = new CONTROL_CONSULTA();
                                obj.ip = Sesion.ip;
                                obj.usuario = Sesion.codUsuario;

                                obj.fechaConsulta = DateTime.Now;
                                obj.identificacion = txt_cedulaAcomp.Text;
                                //if (NegNumeroControl.CreaControlConsulta(obj))
                                //{
                                if (txt_cedulaAcomp.Text.Length == 13)
                                {
                                    RUC sri = new RUC();
                                    sri = NegUtilitarios.ObtenerRUC(txt_cedulaAcomp.Text);
                                    if (sri == null)
                                    {
                                        int min = 1;
                                        int max = 10;

                                        Random rnd = new Random();
                                        int aux = rnd.Next(min, max + 1);
                                        MessageBox.Show("SRI, ocupado en " + aux + " segundos", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                        Thread.Sleep(aux * 1000);
                                        sri = NegUtilitarios.ObtenerRUC(txt_cedulaAcomp.Text);
                                    }
                                    if (sri.ok)
                                    {
                                        txt_nombreAcomp.Text = sri.consulta[0].razonSocial;
                                        txt_direccionAcomp.Text = "";
                                        txt_parentescoAcomp.Text = "";
                                        txt_telefonoAcomp.Text = "";
                                        txt_emailAcomp.Text = "";
                                        txt_ciudadAcomp.Text = "";
                                        obj.tipoConsulta = "Facturar a Nombre de: SRI";
                                        obj.resultado = sri.consulta[0].razonSocial;
                                        NegNumeroControl.CreaControlConsulta(obj);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ruc Incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    }
                                }
                                else if (txt_cedulaAcomp.Text.Length == 10)
                                {
                                    RegistroCivil registroCivil = new RegistroCivil();
                                    registroCivil = NegUtilitarios.ObtenerRegistroCivil(txt_cedulaAcomp.Text);
                                    if (registroCivil == null)
                                        return;
                                    if (registroCivil.consulta.Nombre != null)
                                    {

                                        if (registroCivil == null)
                                        {
                                            int min = 1;
                                            int max = 10;

                                            Random rnd = new Random();
                                            int aux = rnd.Next(min, max + 1);
                                            MessageBox.Show("Registo Civil, ocupado en " + aux + " segundos", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            Thread.Sleep(aux * 1000);
                                            registroCivil = NegUtilitarios.ObtenerRegistroCivil(txt_cedulaAcomp.Text);
                                        }
                                        if (registroCivil != null)
                                        {
                                            cb_personaFactura.SelectedIndex = 3;
                                            txt_nombreAcomp.Text = registroCivil.consulta.Nombre;
                                            txt_direccionAcomp.Text = registroCivil.consulta.CalleDomicilio + " " + registroCivil.consulta.NumeracionDomicilio;
                                            txt_parentescoAcomp.Text = "";
                                            txt_telefonoAcomp.Text = "";
                                            txt_emailAcomp.Text = "";
                                            txt_cedulaAcomp.Text = registroCivil.consulta.Cedula;
                                            if (registroCivil.consulta.LugarDomicilio != "")
                                            {
                                                string[] ciudad = registroCivil.consulta.LugarDomicilio.Split('/');

                                                txt_ciudadAcomp.Text = ciudad[1];
                                            }
                                            else
                                            {
                                                txt_ciudadAcomp.Text = "";

                                            }
                                            obj.tipoConsulta = "Facturar a Nombre de: Reg Civil";
                                            obj.resultado = registroCivil.consulta.Nombre;
                                            NegNumeroControl.CreaControlConsulta(obj);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se puede recuperar información del ciudadano en este momento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cedula Incorrecta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Identificación Incorrecta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

    //}
    //else
    //{
    //    MessageBox.Show("Web Services inaccesibles, vuelva a intentar consultar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //}
    ;
                            }
                            else
                            {
                                RecuperaClienteSIC();
                            }
                        }
                        else
                        {
                            RecuperaClienteSIC();
                        }

                    }
                }
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
            if (e.KeyChar == (char)(Keys.Tab))
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

        public void imprimeAdmision()
        {
            DSAdmision Admision = new DSAdmision();

            DataTable Registro1 = NegAtenciones.RegistroAdmision1(ultimaAtencion.ATE_CODIGO);
            DataTable Registro2 = NegAtenciones.RegistroAdmision2(ultimaAtencion.ATE_CODIGO);
            DataTable Cambios1 = NegAtenciones.CambiosAdmision(pacienteActual.PAC_CODIGO);

            TIPO_REFERIDO referido = NegTipoReferido.RecuperarReferido(Convert.ToInt32(ultimaAtencion.TIPO_REFERIDOReference.EntityKey.EntityKeyValues[0].Value));
            CATEGORIAS_CONVENIOS cc = NegAtenciones.tipoSeguro(ultimaAtencion.ATE_CODIGO);
            TIPO_TRATAMIENTO tratamiento = NegTipoTratamiento.recuperaTipoTratamiento(Convert.ToInt16(ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey.EntityKeyValues[0].Value));

            DataRow drAdmision;
            DataRow drCambios;
            DataRow drRegistro;

            drAdmision = Admision.Tables["Admision"].NewRow();
            drAdmision["Empresa"] = Sesion.nomEmpresa;
            drAdmision["Logo"] = NegUtilitarios.RutaLogo("General");
            drAdmision["Unidad"] = tratamiento.TIA_DESCRIPCION;
            drAdmision["Cod"] = datosPacienteActual.COD_PROVINCIA;
            drAdmision["c_Parroquia"] = datosPacienteActual.COD_PARROQUIA;
            drAdmision["c_Canton"] = datosPacienteActual.COD_CANTON;
            drAdmision["c_Provincia"] = datosPacienteActual.COD_PROVINCIA;
            if (!NegParametros.ParametroFormularios())
                drAdmision["Hc"] = pacienteActual.PAC_HISTORIA_CLINICA;
            else
                drAdmision["Hc"] = pacienteActual.PAC_IDENTIFICACION;
            drAdmision["Apellido1"] = pacienteActual.PAC_APELLIDO_PATERNO.Trim();
            drAdmision["Apellido2"] = pacienteActual.PAC_APELLIDO_MATERNO.Trim();
            drAdmision["Nombre1"] = pacienteActual.PAC_NOMBRE1.Trim();
            drAdmision["Nombre2"] = pacienteActual.PAC_NOMBRE2.Trim();
            drAdmision["Identificacion"] = pacienteActual.PAC_IDENTIFICACION.Trim();
            drAdmision["Direccion"] = datosPacienteActual.DAP_DIRECCION_DOMICILIO.Trim();
            drAdmision["Barrio"] = txt_barrio.Text.Trim();
            drAdmision["Parroquia"] = txt_parroquia.Text.Trim();
            drAdmision["Canton"] = txt_canton.Text.Trim();
            drAdmision["Provincia"] = txt_provincia.Text.Trim();
            drAdmision["Zona"] = "N/A";
            if (datosPacienteActual.DAP_TELEFONO1 == null || datosPacienteActual.DAP_TELEFONO1 == "")
                drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO2;
            else if (datosPacienteActual.DAP_TELEFONO2 == null || datosPacienteActual.DAP_TELEFONO2 == "")
                drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO1;
            else
                drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO1 + " / " + datosPacienteActual.DAP_TELEFONO2;
            drAdmision["F_Nacimiento"] = Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO).ToShortDateString();
            drAdmision["L_Nacimiento"] = cmb_ciudad.Text;
            drAdmision["Nacionalidad"] = pacienteActual.PAC_NACIONALIDAD;
            drAdmision["Etnia"] = cb_etnia.Text;
            drAdmision["Edad"] = DateTime.Now.Year - Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO).Year;

            if (pacienteActual.PAC_GENERO == "M")
                drAdmision["Masculino"] = "X";
            else
                drAdmision["Femenino"] = "X";

            if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 1)
                drAdmision["Soltero"] = "X";
            else if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 2)
                drAdmision["Casado"] = "X";
            else if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 3)
                drAdmision["Divorsiado"] = "X";
            else if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 4)
                drAdmision["Union"] = "X";
            else if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 5)
                drAdmision["Viudo"] = "X";

            drAdmision["Instruccion"] = datosPacienteActual.DAP_INSTRUCCION;
            drAdmision["c_Emergencia"] = pacienteActual.PAC_REFERENTE_NOMBRE.Trim();
            drAdmision["Parentesco"] = pacienteActual.PAC_REFERENTE_PARENTESCO.Trim();
            drAdmision["c_Direccion"] = pacienteActual.PAC_REFERENTE_DIRECCION.Trim();
            drAdmision["c_Telefono"] = pacienteActual.PAC_REFERENTE_TELEFONO.Trim();
            drAdmision["Admisionista"] = ultimaAtencion.USUARIOSReference.EntityKey.EntityKeyValues[0].Value;
            drAdmision["F_Admision"] = ultimaAtencion.ATE_FECHA_INGRESO;
            drAdmision["Ocupacion"] = datosPacienteActual.DAP_OCUPACION;
            drAdmision["p_Empresa"] = datosPacienteActual.DAP_EMP_NOMBRE;
            drAdmision["T_Seguro"] = cc.CAT_NOMBRE;
            drAdmision["Referido"] = referido.TIR_NOMBRE;
            drAdmision["ATE_CODIGO"] = ultimaAtencion.ATE_CODIGO;

            Admision.Tables["Admision"].Rows.Add(drAdmision);

            int contador = Registro1.Rows.Count - 10;
            int aux = 0;
            for (int i = Registro1.Rows.Count - 1; i >= 0; i--)
            {
                drRegistro = Admision.Tables["Registro"].NewRow();
                if (i >= aux)
                {
                    drRegistro["Numero"] = Registro1.Rows[i]["N_ORDEN"].ToString();
                    drRegistro["Fecha"] = Registro1.Rows[i]["FECHA_INGRESO"].ToString();
                    drRegistro["Edad"] = Registro1.Rows[i]["EDAD"].ToString();
                    drRegistro["Referido"] = Registro1.Rows[i]["REFERIDO_DE"].ToString();
                    if (Registro1.Rows[i]["N_ORDEN"].ToString() == "1")
                        drRegistro["Primera"] = "X";
                    else
                        drRegistro["Sub1"] = Registro1.Rows[i]["SUBSEC"].ToString();
                    drRegistro["Admisionista"] = Registro1.Rows[i]["COD_ADMISIONISTA"].ToString();
                }
                if (aux < contador - 1)
                {
                    drRegistro["Numero1"] = Registro1.Rows[aux]["N_ORDEN"].ToString();
                    drRegistro["Fecha1"] = Registro1.Rows[aux]["FECHA_INGRESO"].ToString();
                    drRegistro["Edad1"] = Registro1.Rows[aux]["EDAD"].ToString();
                    drRegistro["Referido1"] = Registro1.Rows[aux]["REFERIDO_DE"].ToString();
                    drRegistro["Sub1"] = Registro1.Rows[aux]["SUBSEC"].ToString();
                    drRegistro["Admisionista1"] = Registro1.Rows[aux]["COD_ADMISIONISTA"].ToString();
                    aux++;
                }
                drRegistro["ATE_CODIGO"] = ultimaAtencion.ATE_CODIGO;

                Admision.Tables["Registro"].Rows.Add(drRegistro);
            }

            foreach (DataRow item in Cambios1.Rows)
            {
                drCambios = Admision.Tables["Cambios"].NewRow();
                drCambios["Numero"] = item[0].ToString();
                drCambios["Fecha"] = item[2].ToString();
                drCambios["Estado_Civil"] = item[3].ToString();
                drCambios["Instruccion"] = item[4].ToString();
                drCambios["Ocupacion"] = item[5].ToString();
                drCambios["Empresa"] = item[6].ToString();
                drCambios["T_Seguro"] = item[7].ToString();
                drCambios["Direccion"] = item[8].ToString();
                drCambios["Barrio"] = item[9].ToString();
                drCambios["Zona"] = item[10].ToString();
                drCambios["Parroquia"] = item[11].ToString();
                drCambios["Canton"] = item[12].ToString();
                drCambios["Provincia"] = item[13].ToString();
                drCambios["Telefono"] = item[14].ToString();
                drCambios["ATE_CODIGO"] = ultimaAtencion.ATE_CODIGO;

                Admision.Tables["Cambios"].Rows.Add(drCambios);
            }
            Formulario.frmReportes x = new Formulario.frmReportes(1, "Admision", Admision);
            x.ShowDialog();
        }
        private void form001ADMISIONYALTAEGRESOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ultimaAtencion == null)
                    MessageBox.Show("Paciente con atenciones facturadas, debe seleccionar de forma manual la atención requerida", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    HojaAdmision(Convert.ToInt64(ultimaAtencion.ATE_CODIGO));
                    //frmReportes frm = new frmReportes();
                    //frm.campo1 = ultimaAtencion.ATE_CODIGO.ToString();
                    //frm.reporte = "rAdmision";
                    //frm.ShowDialog();
                    //frm.Dispose();

                    ////    Nuevo formulario de Admsion Edgar 20210511

                    //#region Nuevo reporte FORM001
                    //imprimeAdmision();
                    //#endregion
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void HojaAdmision(Int64 ateCodigo)
        {
            DSAdmision Admision = new DSAdmision();
            ATENCIONES ultimaAtencion = NegAtenciones.AtencionID((int)ateCodigo);
            DataTable Registro1 = NegAtenciones.RegistroAdmision1(Convert.ToInt32(ultimaAtencion.ATE_CODIGO));
            DataTable Registro2 = NegAtenciones.RegistroAdmision2(ateCodigo);
            DataTable Cambios1 = NegAtenciones.CambiosAdmision(Convert.ToInt32(ultimaAtencion.PACIENTESReference.EntityKey.EntityKeyValues[0].Value));

            TIPO_REFERIDO referido = NegTipoReferido.RecuperarReferido(Convert.ToInt32(ultimaAtencion.TIPO_REFERIDOReference.EntityKey.EntityKeyValues[0].Value));
            CATEGORIAS_CONVENIOS cc = NegAtenciones.tipoSeguro(ultimaAtencion.ATE_CODIGO);
            TIPO_TRATAMIENTO tratamiento = NegTipoTratamiento.recuperaTipoTratamiento(Convert.ToInt16(ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey.EntityKeyValues[0].Value));
            PACIENTES_DATOS_ADICIONALES datosPacienteActual = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPacienteID(ultimaAtencion.ATE_CODIGO);
            PACIENTES pacienteActual = NegPacientes.recuperarPacientePorAtencion(ultimaAtencion.ATE_CODIGO);
            DtoPacienteDatosAdicionales2 dadicionales2 = NegPacienteDatosAdicionales.PDA2_find(pacienteActual.PAC_CODIGO);
            DIVISION_POLITICA dp = new DIVISION_POLITICA();
            //GRUPO_SANGUINEO gs = NegGrupoSanguineo.RecuperarGrupoSanguineoID(Convert.ToInt32(pacienteActual.GRUPO_SANGUINEOReference.EntityKey.EntityKeyValues[0].Value));
            ETNIA etnia = new ETNIA();
            etnia = NegEtnias.RecuperarEtniaID(Convert.ToInt32(pacienteActual.ETNIAReference.EntityKey.EntityKeyValues[0].Value));
            DataRow drCambios;
            DataRow drRegistro;
            DataRow drAdmision;

            drAdmision = Admision.Tables["Admision"].NewRow();
            drAdmision["Empresa"] = Sesion.nomEmpresa;
            drAdmision["Logo"] = NegUtilitarios.RutaLogo("General");
            drAdmision["Unidad"] = tratamiento.TIA_DESCRIPCION;
            // drAdmision["Cod"] = datosPacienteActual.COD_PROVINCIA;
            drAdmision["Cod"] = His.Entidades.Clases.Sesion.unicodigo;
            drAdmision["c_Parroquia"] = !string.IsNullOrEmpty(datosPacienteActual.COD_PARROQUIA) ? datosPacienteActual.COD_PARROQUIA : "N/A";
            drAdmision["c_Canton"] = !string.IsNullOrEmpty(datosPacienteActual.COD_CANTON) ? datosPacienteActual.COD_CANTON : "N/A";
            drAdmision["c_Provincia"] = !string.IsNullOrEmpty(datosPacienteActual.COD_PROVINCIA) ? datosPacienteActual.COD_PROVINCIA : "N/A";
            //if (!NegParametros.ParametroFormularios())
            //    drAdmision["Hc"] = pacienteActual.PAC_HISTORIA_CLINICA;
            //else
            //    drAdmision["Hc"] = pacienteActual.PAC_IDENTIFICACION;
            drAdmision["Hc"] = pacienteActual.PAC_HISTORIA_CLINICA;
            drAdmision["Apellido1"] = !string.IsNullOrEmpty(pacienteActual.PAC_APELLIDO_PATERNO.Trim()) ? pacienteActual.PAC_APELLIDO_PATERNO.Trim() : "N/A";
            drAdmision["Apellido2"] = !string.IsNullOrEmpty(pacienteActual.PAC_APELLIDO_MATERNO) ? pacienteActual.PAC_APELLIDO_MATERNO : "N/A";
            drAdmision["Nombre1"] = !string.IsNullOrEmpty(pacienteActual.PAC_NOMBRE1) ? pacienteActual.PAC_NOMBRE1 : "N/A";
            drAdmision["Nombre2"] = !string.IsNullOrEmpty(pacienteActual.PAC_NOMBRE2) ? pacienteActual.PAC_NOMBRE2 : "N/A";
            drAdmision["Identificacion"] = pacienteActual.PAC_IDENTIFICACION.Trim();
            drAdmision["Direccion"] = datosPacienteActual.DAP_DIRECCION_DOMICILIO.Trim();
            //drAdmision["CalleSecundaria"] = datosPacienteActual.DAP_DIRECCION_DOMICILIO2.Trim();
            drAdmision["CalleSecundaria"] = !string.IsNullOrEmpty(datosPacienteActual.DAP_DIRECCION_DOMICILIO2) ? datosPacienteActual.DAP_DIRECCION_DOMICILIO2 : "N/A";
            //drAdmision["Referencia"] = datosPacienteActual.DAP_REFERENCIA.Trim();
            drAdmision["Referencia"] = (datosPacienteActual.DAP_REFERENCIA ?? "").Trim();
            drAdmision["Email"] = !string.IsNullOrEmpty(pacienteActual.PAC_EMAIL) ? pacienteActual.PAC_EMAIL : "N/A";

            if (datosPacienteActual.COD_SECTOR != null)
            {
                dp = NegDivisionPolitica.DivisionPolitica(datosPacienteActual.COD_SECTOR);
                drAdmision["Barrio"] = !string.IsNullOrEmpty(dp.DIPO_NOMBRE) ? dp.DIPO_NOMBRE : "N/A";
            }
            else
                drAdmision["Barrio"] = "N/A";
            if (datosPacienteActual.COD_PARROQUIA != null)
            {
                dp = NegDivisionPolitica.DivisionPolitica(datosPacienteActual.COD_PARROQUIA);
                drAdmision["Parroquia"] = !string.IsNullOrEmpty(dp.DIPO_NOMBRE) ? dp.DIPO_NOMBRE : "N/A";
            }
            else
                drAdmision["Parroquia"] = "N/A";

            if (datosPacienteActual.COD_CANTON != null)
            {
                dp = NegDivisionPolitica.DivisionPolitica(datosPacienteActual.COD_CANTON);
                drAdmision["Canton"] = !string.IsNullOrEmpty(dp.DIPO_NOMBRE) ? dp.DIPO_NOMBRE : "N/A";
            }
            else
                drAdmision["Canton"] = "N/A";
            if (datosPacienteActual.COD_PROVINCIA != null)
            {
                dp = NegDivisionPolitica.DivisionPolitica(datosPacienteActual.COD_PROVINCIA);
                drAdmision["Provincia"] = !string.IsNullOrEmpty(dp.DIPO_NOMBRE) ? dp.DIPO_NOMBRE : "N/A";
            }
            else
                drAdmision["Provincia"] = "N/A";
            drAdmision["Zona"] = "N/A";
            //if (datosPacienteActual.DAP_TELEFONO1 == null || datosPacienteActual.DAP_TELEFONO1 == "")
            //    drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO2;
            //else if (datosPacienteActual.DAP_TELEFONO2 == null || datosPacienteActual.DAP_TELEFONO2 == "")
            //    drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO1;
            //else
            //    drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO1 + " / " + datosPacienteActual.DAP_TELEFONO2;
            //drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO1;
            //drAdmision["Telefono2"] = datosPacienteActual.DAP_TELEFONO2;
            drAdmision["Telefono"] = !string.IsNullOrEmpty(datosPacienteActual.DAP_TELEFONO1.Trim()) ? datosPacienteActual.DAP_TELEFONO1.Trim() : "N/A";
            drAdmision["Telefono2"] = !string.IsNullOrEmpty(datosPacienteActual.DAP_TELEFONO2) ? datosPacienteActual.DAP_TELEFONO2 : "N/A";
            drAdmision["F_Nacimiento"] = Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO).ToShortDateString();
            dp = NegDivisionPolitica.DivisionPolitica(pacienteActual.DIPO_CODIINEC);
            drAdmision["L_Nacimiento"] = dp.DIPO_NOMBRE;
            drAdmision["Etnia"] = etnia.E_NOMBRE;
            drAdmision["Nacionalidad"] = pacienteActual.PAC_NACIONALIDAD;
            if (int.TryParse(cb_etnia.SelectedValue.ToString(), out int codigoEtnia))
            {
                if (codigoEtnia == 15)
                {
                    drAdmision["Netnica"] = cb_netnica.Text.Trim();
                    drAdmision["Pueblo"] = cb_pueblo.Text.Trim();
                }
                else
                {
                    drAdmision["Netnica"] = "N/A";
                    drAdmision["Pueblo"] = "N/A";
                }
            }
            drAdmision["Edad"] = NegUtilitarios.EdadCalculada(Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO), dateTimeFecIngreso.Value);
            //drAdmision["Edad"] = DateTime.Now.Year - Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO).Year;
            #region antiguo formulario
            //if (pacienteActual.PAC_GENERO == "M")
            //    drAdmision["Masculino"] = "X";
            //else
            //    drAdmision["Femenino"] = "X";

            //if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 1)
            //    drAdmision["Soltero"] = "X";
            //else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 2)
            //    drAdmision["Casado"] = "X";
            //else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 3)
            //    drAdmision["Divorsiado"] = "X";
            //else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 4)
            //    drAdmision["Union"] = "X";
            //else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 5)
            //    drAdmision["Viudo"] = "X";
            #endregion
            if (pacienteActual.PAC_GENERO == "M")
                drAdmision["Masculino"] = "HOMBRE";
            else
                drAdmision["Masculino"] = "MUJER";

            if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 1)
                drAdmision["Soltero"] = "SOLTERO";
            else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 2)
                drAdmision["Soltero"] = "CASADO";
            else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 3)
                drAdmision["Soltero"] = "DIVORCIADO";
            else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 4)
                drAdmision["Soltero"] = "VIUDO";
            else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 5)
                drAdmision["Soltero"] = "X=UNION DE HECHO";

            //drAdmision["Instruccion"] = datosPacienteActual.DAP_INSTRUCCION;
            //drAdmision["c_Emergencia"] = pacienteActual.PAC_REFERENTE_NOMBRE.Trim();
            //drAdmision["Parentesco"] = pacienteActual.PAC_REFERENTE_PARENTESCO.Trim();
            //drAdmision["c_Direccion"] = pacienteActual.PAC_REFERENTE_DIRECCION.Trim();
            //drAdmision["c_Telefono"] = pacienteActual.PAC_REFERENTE_TELEFONO.Trim();
            drAdmision["Admisionista"] = ultimaAtencion.USUARIOSReference.EntityKey.EntityKeyValues[0].Value;
            drAdmision["F_Admision"] = ultimaAtencion.ATE_FECHA_INGRESO;
            //drAdmision["Ocupacion"] = datosPacienteActual.DAP_OCUPACION;
            //drAdmision["p_Empresa"] = datosPacienteActual.DAP_EMP_NOMBRE;
            drAdmision["T_Seguro"] = cc.CAT_NOMBRE;
            drAdmision["Referido"] = referido.TIR_NOMBRE;
            drAdmision["ATE_CODIGO"] = ultimaAtencion.ATE_CODIGO;
            //drAdmision["NEducacion"] = dadicionales2.neducacion;
            //drAdmision["TEmpresa"] = dadicionales2.tempresa;
            //drAdmision["SSalud"] = dadicionales2.ssalud;
            //drAdmision["TBono"] = dadicionales2.tbono;

            drAdmision["Instruccion"] = !string.IsNullOrEmpty(datosPacienteActual.DAP_INSTRUCCION) ? datosPacienteActual.DAP_INSTRUCCION : "N/A";
            drAdmision["c_Emergencia"] = !string.IsNullOrEmpty(pacienteActual.PAC_REFERENTE_NOMBRE.Trim()) ? pacienteActual.PAC_REFERENTE_NOMBRE.Trim() : "N/A";
            drAdmision["Parentesco"] = !string.IsNullOrEmpty(pacienteActual.PAC_REFERENTE_PARENTESCO) ? pacienteActual.PAC_REFERENTE_PARENTESCO : "N/A";
            drAdmision["c_Direccion"] = !string.IsNullOrEmpty(pacienteActual.PAC_REFERENTE_DIRECCION.Trim()) ? pacienteActual.PAC_REFERENTE_DIRECCION.Trim() : "N/A";
            drAdmision["c_Telefono"] = !string.IsNullOrEmpty(pacienteActual.PAC_REFERENTE_TELEFONO) ? pacienteActual.PAC_REFERENTE_TELEFONO : "N/A";
            drAdmision["Ocupacion"] = !string.IsNullOrEmpty(datosPacienteActual.DAP_OCUPACION) ? datosPacienteActual.DAP_OCUPACION : "N/A";
            drAdmision["p_Empresa"] = !string.IsNullOrEmpty(datosPacienteActual.DAP_EMP_NOMBRE) ? datosPacienteActual.DAP_EMP_NOMBRE : "N/A";
            drAdmision["NEducacion"] = !string.IsNullOrEmpty(dadicionales2.neducacion) ? dadicionales2.neducacion : "N/A";
            drAdmision["TEmpresa"] = !string.IsNullOrEmpty(dadicionales2.tempresa) ? dadicionales2.tempresa : "N/A";
            drAdmision["SSalud"] = !string.IsNullOrEmpty(dadicionales2.ssalud) ? dadicionales2.ssalud : "N/A";
            drAdmision["TBono"] = !string.IsNullOrEmpty(dadicionales2.tbono) ? dadicionales2.tbono : "N/A";

            Admision.Tables["Admision"].Rows.Add(drAdmision);
            int aux = 0;
            int contador = Registro1.Rows.Count - 10;
            int llenado = 10 - Registro1.Rows.Count;
            int llenarcampo = Registro1.Rows.Count + 1;
            int llenarcampo2 = 11;
            for (int i = Registro1.Rows.Count - 1; i >= 0; i--)
            {
                if (aux < 10)
                {
                    drRegistro = Admision.Tables["Registro"].NewRow();

                    drRegistro["Numero"] = Registro1.Rows[i]["N_ORDEN"].ToString();
                    drRegistro["Fecha"] = Registro1.Rows[i]["FECHA_INGRESO"].ToString().Trim();
                    drRegistro["Edad"] = Registro1.Rows[i]["EDAD"].ToString();
                    drRegistro["Referido"] = Registro1.Rows[i]["REFERIDO_DE"].ToString();
                    if (Registro1.Rows[i]["N_ORDEN"].ToString() == "1")
                        drRegistro["Primera"] = "X";
                    else
                        drRegistro["Sub"] = "X";
                    drRegistro["Admisionista"] = Registro1.Rows[i]["COD_ADMISIONISTA"].ToString().Trim();

                    if (contador >= 1)
                    {
                        contador--;
                        drRegistro["Numero1"] = Registro1.Rows[contador]["N_ORDEN"].ToString();
                        drRegistro["Fecha1"] = Registro1.Rows[contador]["FECHA_INGRESO"].ToString();
                        drRegistro["Edad1"] = Registro1.Rows[contador]["EDAD"].ToString();
                        drRegistro["Referido1"] = Registro1.Rows[contador]["REFERIDO_DE"].ToString();
                        drRegistro["Sub1"] = Registro1.Rows[contador]["SUBSEC"].ToString();
                        drRegistro["Admisionista1"] = Registro1.Rows[contador]["COD_ADMISIONISTA"].ToString();
                    }
                    else
                    {
                        drRegistro["Numero1"] = llenarcampo2;
                        llenarcampo2++;
                    }

                    aux++;
                    drRegistro["ATE_CODIGO"] = ultimaAtencion.ATE_CODIGO;

                    Admision.Tables["Registro"].Rows.Add(drRegistro);
                }
            }
            //for (int i = 0; i < llenado; i++)
            //{
            //    drRegistro = Admision.Tables["Registro"].NewRow();
            //    drRegistro["Numero"] = llenarcampo;
            //    llenarcampo++;
            //    drRegistro["Numero1"] = llenarcampo2;
            //    llenarcampo2++;
            //    Admision.Tables["Registro"].Rows.Add(drRegistro);
            //}
            foreach (DataRow item in Cambios1.Rows)
            {
                drCambios = Admision.Tables["Cambios"].NewRow();
                drCambios["Numero"] = item[0].ToString().Trim();
                drCambios["Fecha"] = item[2].ToString();
                drCambios["Estado_Civil"] = item[7].ToString();
                //drCambios["Instruccion"] = item[3].ToString();
                //drCambios["Ocupacion"] = item[4].ToString();
                //drCambios["Empresa"] = item[5].ToString();
                //drCambios["T_Seguro"] = item["PAC_SEG_SALUD"].ToString();
                //drCambios["Direccion"] = item[6].ToString();
                //drCambios["Barrio"] = item[9].ToString();
                //drCambios["Zona"] = item[10].ToString();
                //drCambios["Parroquia"] = item[11].ToString();
                //drCambios["Canton"] = item[12].ToString();
                //drCambios["Provincia"] = item[13].ToString();
                //drCambios["Telefono"] = item[14].ToString();
                //drCambios["CalleSecundaria"] = item[15].ToString();
                //drCambios["Referencia"] = item[16].ToString();
                drCambios["ATE_CODIGO"] = ultimaAtencion.ATE_CODIGO;

                drCambios["Instruccion"] = !string.IsNullOrEmpty(item[3].ToString().Trim()) ? item[3].ToString().Trim() : "N/A";
                drCambios["Ocupacion"] = !string.IsNullOrEmpty(item[4].ToString().Trim()) ? item[4].ToString().Trim() : "N/A";
                drCambios["Empresa"] = !string.IsNullOrEmpty(item[5].ToString().Trim()) ? item[5].ToString().Trim() : "N/A";
                drCambios["T_Seguro"] = !string.IsNullOrEmpty(item["PAC_SEG_SALUD"].ToString().Trim()) ? item["PAC_SEG_SALUD"].ToString().Trim() : "N/A";
                drCambios["Direccion"] = !string.IsNullOrEmpty(item[9].ToString().Trim()) ? item[6].ToString().Trim() : "N/A";
                drCambios["Barrio"] = !string.IsNullOrEmpty(item[10].ToString().Trim()) ? item[9].ToString().Trim() : "N/A";
                drCambios["Zona"] = !string.IsNullOrEmpty(item[11].ToString().Trim()) ? item[10].ToString().Trim() : "N/A";
                drCambios["Parroquia"] = !string.IsNullOrEmpty(item[12].ToString().Trim()) ? item[11].ToString().Trim() : "N/A";
                drCambios["Canton"] = !string.IsNullOrEmpty(item[13].ToString().Trim()) ? item[0].ToString().Trim() : "N/A";
                drCambios["Provincia"] = !string.IsNullOrEmpty(item[0].ToString().Trim()) ? item[13].ToString().Trim() : "N/A";
                drCambios["Telefono"] = !string.IsNullOrEmpty(item[14].ToString().Trim()) ? item[14].ToString().Trim() : "N/A";
                drCambios["CalleSecundaria"] = !string.IsNullOrEmpty(item[15].ToString().Trim()) ? item[15].ToString().Trim() : "N/A";
                drCambios["Referencia"] = !string.IsNullOrEmpty(item[16].ToString().Trim()) ? item[16].ToString().Trim() : "N/A";
                Admision.Tables["Cambios"].Rows.Add(drCambios);
            }
            Formulario.frmReportes x = new Formulario.frmReportes(1, "Admision", Admision);
            x.ShowDialog();
        }

        public void Egreso2020(string codigo)
        {
            DataTable DatosPcte = NegDietetica.getDataTable("GetEgreso_DatosPaciente", codigo);
            DataTable Medicos = NegDietetica.getDataTable("GetEgreso_MedicosEvolucion", codigo);
            DataTable Garantias = NegDietetica.getDataTable("GetEgreso_Garantias", codigo);
            DataTable HistorialHabitacion = NegDietetica.getDataTable("GetEgreso_HistorialHabitacion", codigo);
            DataTable ConvenioSeguro = NegDietetica.getDataTable("GetEgreso_ConvenioSeguro", codigo);

            try
            {
                #region//limpiar tablas
                ReportesHistoriaClinica r1 = new ReportesHistoriaClinica(); r1.DeleteTable("rptEgreso_DatosPcte");
                ReportesHistoriaClinica r2 = new ReportesHistoriaClinica(); r2.DeleteTable("rptEgreso_Medicos");
                ReportesHistoriaClinica r3 = new ReportesHistoriaClinica(); r3.DeleteTable("rptEgreso_Garantias");
                ReportesHistoriaClinica r4 = new ReportesHistoriaClinica(); r4.DeleteTable("rptEgreso_HabitacionHistorial");
                ReportesHistoriaClinica r5 = new ReportesHistoriaClinica(); r5.DeleteTable("rptEgreso_ConvenioSeguro");
                #endregion
                #region //empaquetar y guardar en tablas access
                foreach (DataRow row in DatosPcte.Rows)
                {
                    ///edad
                    var now = DateTime.Now;
                    var birthday = dtp_fecnac.Value;
                    var yearsOld = now - birthday;

                    int years = (int)(yearsOld.TotalDays / 365.25);
                    string[] x = new string[] {
                            row["PAC_HISTORIA_CLINICA"].ToString(),
                            row["PACIENTE"].ToString(),
                            row["PAC_IDENTIFICACION"].ToString(),
                            row["PAC_FECHA_NACIMIENTO"].ToString(),
                            row["hab_Numero"].ToString(),
                            row["ATE_FECHA_INGRESO"].ToString(),
                            row["ATE_CODIGO"].ToString(),
                            row["ATE_NUMERO_ATENCION"].ToString(),
                            row["MEDICO"].ToString(),
                            row["TIP_DESCRIPCION"].ToString(),
                            row["TIA_DESCRIPCION"].ToString(),
                            row["ATE_DIAGNOSTICO_INICIAL"].ToString(),
                            row["ATE_DIAGNOSTICO_FINAL"].ToString(),
                            row["USUARIO"].ToString(),
                            years.ToString()
                    };
                    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                    AUXma.InsertTable("rptEgreso_DatosPcte", x);
                }

                foreach (DataRow row in Medicos.Rows)
                {
                    string[] x = new string[] {
                            row["NOM_USUARIO"].ToString()
                    };
                    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                    AUXma.InsertTable("rptEgreso_Medicos", x);
                }


                foreach (DataRow row in Garantias.Rows)
                {
                    string[] x = new string[] {
                            row["ADG_FECHA"].ToString(),
                            row["TG_NOMBRE"].ToString(),
                            row["ADG_VALOR"].ToString(),
                            row["TITULAR"].ToString(),
                            row["ADG_BANCO"].ToString(),
                            row["ADG_DOCUMENTO"].ToString(),
                            row["ADG_TIPOTARJETA"].ToString(),
                            row["ADG_ESTATUS"].ToString(),
                            row["ADG_OBSERVACION"].ToString()
                    };
                    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                    AUXma.InsertTable("rptEgreso_Garantias", x);
                }


                foreach (DataRow row in HistorialHabitacion.Rows)
                {
                    string[] x = new string[] {
                            row["OBSERVACION"].ToString(),
                            row["FECHA_MOVIMIENTO"].ToString(),
                            row["HORA"].ToString(),
                            row["HABITACION"].ToString(),
                            row["ANTERIOR"].ToString(),
                            row["ESTADO"].ToString(),
                            row["USUARIO"].ToString()
                    };
                    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                    AUXma.InsertTable("rptEgreso_HabitacionHistorial", x);
                }

                foreach (DataRow row in ConvenioSeguro.Rows)
                {
                    string[] x = new string[] {
                            row["CAT_NOMBRE"].ToString(),
                            row["ADA_FECHA_INICIO"].ToString(),
                            row["ADA_FECHA_FIN"].ToString(),
                            row["ADA_MONTO_COBERTURA"].ToString()
                    };
                    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                    AUXma.InsertTable("rptEgreso_ConvenioSeguro", x);
                }
                #endregion
                //llamo al reporte
                //frmReportes form = new frmReportes();
                //form.reporte = "EGRESO_LX";
                //form.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void form001EGRESOHOSPITALARIOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Egreso2020(ultimaAtencion.ATE_CODIGO.ToString());
            //frm_Egreso_preview form = new frm_Egreso_preview(ultimaAtencion.ATE_CODIGO);
            //form.ShowDialog();
            if (ultimaAtencion.ESC_CODIGO != 1)
            {
                //DataTable Medicos = NegDietetica.getDataTable("GetEgreso_MedicosEvolucion", ultimaAtencion.ATE_CODIGO.ToString());
                DataTable Medicos = NegHabitaciones.CargarMedicos(ultimaAtencion.ATE_CODIGO);
                DateTime fechaNacimiento = NegHabitaciones.RecuperaFechaNacimiento(txt_historiaclinica.Text);
                DataTable Garantias = NegDietetica.getDataTable("GetEgreso_Garantias", ultimaAtencion.ATE_CODIGO.ToString());
                DataTable HistorialHabitacion = NegDietetica.getDataTable("GetEgreso_HistorialHabitacion", ultimaAtencion.ATE_CODIGO.ToString());
                DataTable ConvenioSeguro = NegDietetica.getDataTable("GetEgreso_ConvenioSeguro", ultimaAtencion.ATE_CODIGO.ToString());
                try
                {
                    //limpiar tablas
                    DataTable DatosPcte = NegDietetica.getDataTable("GetEgreso_DatosPaciente", ultimaAtencion.ATE_CODIGO.ToString());
                    ReportesHistoriaClinica r2 = new ReportesHistoriaClinica(); r2.DeleteTable("rptEgreso_Medicos");
                    ReportesHistoriaClinica r3 = new ReportesHistoriaClinica();
                    r3.DeleteTable("rptEgreso_DatosPcte");
                    ReportesHistoriaClinica r4 = new ReportesHistoriaClinica(); r4.DeleteTable("rptEgreso_HabitacionHistorial");
                    ReportesHistoriaClinica r5 = new ReportesHistoriaClinica(); r5.DeleteTable("rptEgreso_ConvenioSeguro");

                    foreach (DataRow row in DatosPcte.Rows)
                    {
                        ///edad
                        var now = DateTime.Now;
                        var birthday = fechaNacimiento;
                        var yearsOld = now - birthday;

                        int years = (int)(yearsOld.TotalDays / 365.25);
                        string[] x = new string[] {
                            row["PAC_HISTORIA_CLINICA"].ToString(),
                            row["PACIENTE"].ToString(),
                            row["PAC_IDENTIFICACION"].ToString(),
                            row["PAC_FECHA_NACIMIENTO"].ToString(),
                            row["hab_Numero"].ToString(),
                            row["ATE_FECHA_INGRESO"].ToString(),
                            row["ATE_CODIGO"].ToString(),
                            row["ATE_NUMERO_ATENCION"].ToString(),
                            row["MEDICO"].ToString(),
                            row["TIP_DESCRIPCION"].ToString(),
                            row["TIA_DESCRIPCION"].ToString(),
                            row["ATE_DIAGNOSTICO_INICIAL"].ToString(),
                            row["ATE_DIAGNOSTICO_FINAL"].ToString(),
                            row["USUARIO"].ToString(),
                            row["REFERIDO"].ToString(),
                            row["FECHA_ALTA"].ToString(),
                            years.ToString()
                    };
                        ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                        AUXma.InsertTable("rptEgreso_DatosPcte", x);
                    }
                    USUARIOS USUA = new USUARIOS();
                    if (Medicos.Rows[0][3].ToString() != "")
                        USUA = NegUsuarios.RecuperarUsuarioID(Convert.ToInt32(Medicos.Rows[0][3].ToString()));

                    foreach (DataRow row in HistorialHabitacion.Rows)
                    {
                        string[] x = new string[] {
                            row["OBSERVACION"].ToString(),
                            row["FECHA_MOVIMIENTO"].ToString(),
                            row["HORA"].ToString(),
                            row["HABITACION"].ToString(),
                            row["ANTERIOR"].ToString(),
                            row["ESTADO"].ToString(),
                            row["USUARIO"].ToString()
                    };
                        ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                        AUXma.InsertTable("rptEgreso_HabitacionHistorial", x);
                    }

                    foreach (DataRow row in Garantias.Rows)
                    {
                        string[] x = new string[] {
                            row["ADG_FECHA"].ToString(),
                            row["TG_NOMBRE"].ToString(),
                            row["ADG_VALOR"].ToString(),
                            row["TITULAR"].ToString(),
                            row["ADG_BANCO"].ToString(),
                            row["ADG_DOCUMENTO"].ToString(),
                            row["ADG_TIPOTARJETA"].ToString(),
                            row["ADG_ESTATUS"].ToString(),
                            row["ADG_OBSERVACION"].ToString()
                    };
                        ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                        AUXma.InsertTable("rptEgreso_Garantias", x);
                    }
                    foreach (DataRow row in ConvenioSeguro.Rows)
                    {
                        string[] x = new string[] {
                            row["CAT_NOMBRE"].ToString(),
                            row["ADA_FECHA_INICIO"].ToString(),
                            row["ADA_FECHA_FIN"].ToString(),
                            row["ADA_MONTO_COBERTURA"].ToString()
                    };
                        ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                        AUXma.InsertTable("rptEgreso_ConvenioSeguro", x);
                    }
                    foreach (DataRow row in Medicos.Rows)
                    {

                        if (row[0].ToString() != "")
                        {
                            string[] x = new string[] {
                                row[0].ToString(), "Generado desde Admsion"
                        };
                            ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                            AUXma.InsertTable("rptEgreso_Medicos", x);
                        }
                    }
                    if (Medicos.Rows[0][3].ToString() == "")
                    {
                        string[] x2 = new string[] { Medicos.Rows[0][1].ToString(), Sesion.nomUsuario };
                        ReportesHistoriaClinica AUXmha = new ReportesHistoriaClinica();

                        AUXmha.InsertTable("rptupdate_dtos", x2);
                        System.Threading.Thread.Sleep(3000);
                    }

                    else
                    {
                        string[] x2 = new string[] { Medicos.Rows[0][1].ToString(), USUA.APELLIDOS + ' ' + USUA.NOMBRES };
                        ReportesHistoriaClinica AUXmha = new ReportesHistoriaClinica();

                        AUXmha.InsertTable("rptupdate_dtos", x2);
                        System.Threading.Thread.Sleep(3000);
                    }




                    //llamo al reporte
                    frmReportes form = new frmReportes();
                    form.reporte = "EGRESO_LX";
                    form.ShowDialog();

                }
                catch
                {
                    MessageBox.Show("No se genero hoja de alta para esta atención!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Paciente en Hospitalización, no se puede generar Hoja de Alta.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                //if (!NegAtenciones.RecuperarPacientes(txt_cedula.Text))
                if (NegAtenciones.existeAtencionAdmision(txt_numeroatencion.Text.Trim()) || atencionNueva)
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
                    if (ConsultaExterna)
                    {
                        if (mushuñan)
                        {
                            Int16 AreaUsuario = 1;
                            DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario));
                            bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
                            if (parse)
                            {
                                switch (AreaUsuario)
                                {
                                    case 2:
                                        frmControlesWPF ayuda = new frmControlesWPF(2);
                                        ayuda.tip_codigo = cmb_tipoingreso.SelectedValue.ToString();
                                        ayuda.ShowDialog();
                                        ayuda.Dispose();
                                        break;
                                    case 3:
                                        frmControlesWPF ayuda1 = new frmControlesWPF(4);
                                        ayuda1.tip_codigo = cmb_tipoingreso.SelectedValue.ToString();
                                        ayuda1.ShowDialog();
                                        ayuda1.Dispose();
                                        break;
                                    default:
                                        frmControlesWPF ayuda2 = new frmControlesWPF();
                                        ayuda2.tip_codigo = cmb_tipoingreso.SelectedValue.ToString();
                                        ayuda2.ShowDialog();
                                        ayuda2.Dispose();
                                        break;
                                }
                            }
                        }
                        else
                        {
                            frmControlesWPF ayuda = new frmControlesWPF(3);
                            ayuda.tip_codigo = cmb_tipoingreso.SelectedValue.ToString();
                            ayuda.ShowDialog();
                            ayuda.Dispose();
                        }
                    }
                    else
                    {
                        frmControlesWPF ayuda = new frmControlesWPF();
                        ayuda.tip_codigo = cmb_tipoingreso.SelectedValue.ToString();
                        ayuda.ShowDialog();
                        ayuda.Dispose();
                    }

                    if (Sesion.codHabitacion != 0)
                    {
                        txt_habitacion.Text = Sesion.numHabitacion;
                        cmbTipoAtencion.Focus();
                    }
                }
                else
                    MessageBox.Show("Paciente Ya fue Asigando Habitación", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            EdadCalculada();
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
            if (int.TryParse(cb_etnia.SelectedValue.ToString(), out int codigoEtnia))
            {
                if (codigoEtnia == 15)
                {
                    cb_netnica.Enabled = true;
                    //cb_pueblo.Enabled = true;
                }
                else
                {
                    cb_netnica.Enabled = false;
                    cb_pueblo.Enabled = false;
                    cb_pueblo.SelectedIndex = -1;
                    cb_netnica.SelectedIndex = -1;
                }
            }
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
                fallecido = false;
                if (pacienteNuevo == false)
                {

                    frm_AyudaPacientes form = new frm_AyudaPacientes();
                    form.campoPadre = txt_historiaclinica;
                    form.ShowDialog();
                    form.Dispose();

                    //Cambios Edgar 20210223
                    CargarPaciente(txt_historiaclinica.Text.Trim(), 0);
                    deshabilitarCamposAtencion();
                    deshabilitarCamposDireccion();
                    deshabilitarCamposPaciente();
                    if (txt_historiaclinica.Text.Trim() == "")
                    {
                        consultas = false;
                        btnGuardar.Enabled = false;
                        btnHabitaciones.Enabled = false;
                        btnFormularios.Enabled = false;
                        btnActualizar.Enabled = false;
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                        btnHabitaciones.Enabled = false;
                        btnFormularios.Enabled = false;

                        if (fallecido == true || modificar == true)
                            btnActualizar.Enabled = false;
                        else
                        {
                            btnActualizar.Enabled = true;
                            toolStripNuevo.Enabled = true;
                            btnEliminar.Visible = false;
                            tabulador.Tabs["paciente"].Enabled = true;
                            tabulador.Tabs["atencion"].Enabled = true;
                            tabulador.Tabs["gridatenciones"].Enabled = true;
                        }
                        consultas = true;
                        preAdmision = false;
                        preCodigo = 0;
                    }
                    //________________________________

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
            if (e.KeyCode == Keys.Enter)
            {
                cb_gruposanguineo.Focus();
                //SendKeys.SendWait("{TAB}");
            }
            //if (e.KeyCode == Keys.Enter)
            //    cb_gruposanguineo.Focus();
            //else if(e.KeyCode == Keys.Tab)
            //    cb_gruposanguineo.Focus();
            //if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
        }

        private void cb_gruposanguineo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
            if (e.KeyCode == Keys.Enter)
            {
                //SendKeys.SendWait("{TAB}");
                rbn_h.Focus();
            }
            //if (e.KeyCode == Keys.Tab)
            //{
            //    rbn_h.Focus();
            //}
            //else if (e.KeyCode == Keys.Enter)
            //    rbn_h.Focus();
            //if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
        }


        private void cmb_pais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
            if (e.KeyCode == Keys.Enter)
            {
                cmb_ciudad.Focus();
            }
            //if(e.KeyCode == Keys.Tab)
            //{
            //    cmb_ciudad.Focus();
            //}
            //else if(e.KeyCode == Keys.Enter)
            //{
            //    cmb_ciudad.Focus();
            //}
            //if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
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
            //if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
            //if(e.KeyCode == Keys.Enter)
            //{
            //    cmb_ciudadano.Focus();
            //}
            //else if(e.KeyCode == Keys.Tab)
            //{
            //    cmb_ciudadano.Focus();
            //}
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_ciudadano_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.Tab)
            //{
            //    txt_email.Focus();
            //}
            //else if(e.KeyCode == Keys.Enter)
            //{
            //    txt_email.Focus();
            //}
            if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.SendWait("{TAB}");
            }
            //if (e.KeyCode == (GeneralPAR.TeclaTabular) || e.KeyCode == (Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
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
                PREADMISION pre = new PREADMISION();
                if (atencionNueva == false && pacienteActual != null)
                {
                    string numatencion = gridAtenciones.ActiveRow.Cells["CODIGO"].Value.ToString();
                    ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(numatencion);
                    NegAtenciones.BarridoNumeroAdmision(Convert.ToInt64(ultimaAtencion.PACIENTESReference.EntityKey.EntityKeyValues[0].Value));
                    txt_numeroatencion.Text = ultimaAtencion.ATE_NUMERO_ATENCION;
                    consultas = true;
                    if (ultimaAtencion != null)
                    {
                        CargarAtencion();
                        txt_LugarFecha.Text = "QUITO," + " " + DateTime.Today;
                        dtIngreso.Text = gridAtenciones.ActiveRow.Cells["FECHA_INGRESO"].Value.ToString();
                        mktCedula.Text = txt_cedula.Text;
                        //tabulador.Tabs["certificado"].Visible = true;
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
                        btnActualizar.Enabled = true;
                        btn_Subsecuente.Enabled = false;
                        //lx201

                        if (gridAtenciones.ActiveRow.Cells["FACTURA"].Value == null || gridAtenciones.ActiveRow.Cells["FACTURA"].Value.ToString() == "")
                        {
                            btnActualizar.Enabled = true;
                            modificar = false;
                        }

                        else
                        {
                            modificar = true;
                            btnActualizar.Enabled = false;
                        }

                        /////////////////////////////////////

                        if (ultimaAtencion != null)
                        {
                            if (ultimaAtencion.ESC_CODIGO == 13)
                            {
                                rbDesactivada.Checked = true;
                            }
                        }

                        if (ultimaAtencion.ESC_CODIGO == (int)6 || ultimaAtencion.ESC_CODIGO == (int)2 || ultimaAtencion.ESC_CODIGO == (int)13)
                        {
                            groupBox3.Enabled = false;
                        }
                        else
                        {
                            groupBox3.Enabled = true;
                        }
                        List<PREADMISION_DETALLE> detalle = new List<PREADMISION_DETALLE>();
                        pre = NegPreadmision.recuperaPreAdmisionAte(Convert.ToInt64(ultimaAtencion.ATE_NUMERO_ATENCION));
                        if (pre != null)
                        {
                            detalle = NegPreadmision.recuperarPreAdmisionDetalle(pre.PRE_CODIGO);
                            txt_observaciones.Text = pre.PROCEDIMIENTO;
                            CargarMedico(Convert.ToInt32(pre.MED_CODIGO));
                            txtCodMedico.Text = pre.MED_CODIGO.ToString();
                            cmb_tipoatencion.SelectedItem = tipoTratamiento.FirstOrDefault(t => t.TIA_CODIGO == pre.TIA_CODIGO);
                            cmb_tiporeferido.SelectedItem = tipoReferido.FirstOrDefault(t => t.TIR_CODIGO == pre.TIR_CODIGO);

                            foreach (var item in detalle)
                            {
                                CIE10 cie = new CIE10();
                                cie = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);

                                txt_diagnostico.Text += cie.CIE_DESCRIPCION + "---";
                            }
                        }
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
                        CargarPaciente(txt_historiaclinica.Text.Trim().ToString(), 0);
                        txt_historiaclinica.Tag = false;
                    }
                }
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public bool mushuñan = false;
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
                netnias = NegEtnias.ListaNacionalidadEtnica();
                tipoIngreso = NegTipoIngreso.ListaTipoIngreso();
                listaAnexos = NegAnexos.RecuperarListaAnexos(Convert.ToString(His.Parametros.AdmisionParametros.CodigoAnexoParentesco));
                txt_nombreRef.Text.Trim();
                txtRefDireccion.Text.Trim();
                CargarDatos();

                His.Parametros.ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                codigoTipoIngreso = Convert.ToInt16(archivo.IniReadValue("admision", "default"));
                if (codigoTipoIngreso > 0)
                {
                    cmb_tipoingreso.SelectedIndex = codigoTipoIngreso;
                    seleccionTipoIngreso = false;
                    cmb_tipoingreso.SelectedValue = 2; //CAMBIOS EDGAR 20220116  POR DEFECTO ES HOSPITALIZACION
                }
                if (NegRubros.ParametroGarantia()) //Cambios Edgar 20210211
                {
                    uTabFormaPago.Tabs["garantia"].Visible = false;
                    uTabFormaPago.Tabs["seguros"].Visible = false;
                    uTabFormaPago.Tabs["derivados"].Visible = false;
                }
                else
                {
                    uTabFormaPago.Tabs["garantia"].Visible = true;
                    uTabFormaPago.Tabs["seguros"].Visible = true;
                    uTabFormaPago.Tabs["derivados"].Visible = true;
                }

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
            if (txt_nombreRef.Text != "" && txt_telfRef.Text != ""
                && txtRefDireccion.Text != "" && txtRefDireccion.Text != "")
            {
                txt_nombreAcomp.Text = txt_nombreRef.Text;
                txt_telefonoAcomp.Text = txt_telfRef.Text;
                txt_direccionAcomp.Text = txtRefDireccion.Text;
                txt_parentescoAcomp.Text = txt_parentRef.Text.Trim();
                txt_ciudadAcomp.Text = txt_canton.Text;
            }
            else
            {
                MessageBox.Show("No tiene registrado a persona \r\n\"En caso de Emergencia llamar a..\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                PACIENTES pacienteExiste = NegPacientes.RecuperarPacienteID(txt_historiaclinica.Text.Trim());
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
                            txt_historiaclinica.Text = pacienteActual.PAC_HISTORIA_CLINICA.Trim();
                        }
                    }
                }
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Message);
            }
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
                if (ts.TotalHours >= 0 && ts.TotalMinutes >= 0 && ts.TotalSeconds >= 0)
                {
                    //if (ts.Minutes >= 30)
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

            if (cmb_tipoingreso.Text == "PREADMISION")
            {
                ayudaHabitaciones.Enabled = false;
                txt_habitacion.Text = "PREA";
                cmb_tiporeferido.SelectedIndex = -1;
                txtCodMedico.Text = "";
                txtNombreMedico.Text = "";
                txt_habitacion.Enabled = true;
                dtpPreAdmision.Enabled = true;
                chb_PreAdmision.Checked = true;
                chb_PreAdmision.Enabled = false;
                if (atencionNueva == true)
                {
                    dtpPreAdmision.Value = DateTime.Now;
                }
                btn_Subsecuente.Visible = false;
                btn_Subsecuente.Enabled = false;
            }
            else if (cmb_tipoingreso.Text == "CONSULTA EXTERNA" || cmb_tipoingreso.Text == "MUSHUÑAN" || cmb_tipoingreso.Text == "BRIGADAS MEDICAS")
            {
                btn_Subsecuente.Enabled = true;
                btn_Subsecuente.Visible = true;
            }
            else
            {
                ayudaHabitaciones.Enabled = true;
                txt_habitacion.Text = "";
                txt_habitacion.Enabled = true;
                chb_PreAdmision.Checked = false;
                chb_PreAdmision.Enabled = false;
                cmb_tiporeferido.SelectedIndex = -1;
                txtCodMedico.Text = "";
                txtNombreMedico.Text = "";
                if (consultaExterna == 1 && cmb_tipoingreso.SelectedValue.ToString() == "4" || cmb_tipoingreso.SelectedValue.ToString() == "10" || cmb_tipoingreso.SelectedValue.ToString() == "12")
                {
                    cmb_tiporeferido.SelectedIndex = 0;
                }
                else
                {
                    cmb_tiporeferido.SelectedIndex = -1;
                }
                btn_Subsecuente.Visible = false;
                btn_Subsecuente.Enabled = false;

                //if (cmb_tipoingreso.SelectedIndex == 0)
                //{
                //    //ayudaHabitaciones.Enabled = false;
                //    //txt_habitacion.Text = "AMB";
                //    //txt_habitacion.Enabled = false;                    
                //}
                //else
                //{
                //    if (cmb_tipoingreso.SelectedIndex == 2)
                //    {
                //        ayudaHabitaciones.Enabled = false;
                //        //cmb_tipoingreso.Enabled = false;
                //    }
                //    else
                //    {
                //        ayudaHabitaciones.Enabled = true;
                //        txt_habitacion.Enabled = true;
                //        txt_habitacion.Text = "";
                //        dtpPreAdmision.Enabled = false;
                //        dtpPreAdmision.Value = DateTime.Now;
                //        chb_PreAdmision.Checked = false;
                //    }

                //}
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
            if (txt_historiaclinica.Text.Trim() != "" && txt_numeroatencion.Text != "")
            {
                CrearCarpetas_Srvidor("Contrato");
            }
        }

        private void fOO1ADMISIÓNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text.Trim() != "" && txt_numeroatencion.Text != "")
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
            if (gridAseguradoras.CurrentCell.ColumnIndex == 10)
            {
                if (e.KeyCode == Keys.F1)
                {
                    His.Formulario.frm_AyudaEspecialidad x = new frm_AyudaEspecialidad();
                    x.ShowDialog();
                    if (x.esp_codigo != "")
                    {
                        gridAseguradoras.CurrentRow.Cells[10].Value = x.esp_codigo;
                        gridAseguradoras.CurrentRow.Cells[11].Value = x.esp_nombre;
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
                            certificadoMedico.hcu = txt_historiaclinica.Text.Trim();
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
            if (ultimaAtencion != null)
            {
                validacionEstado = NegAtenciones.RecuperarAtencionID(ultimaAtencion.ATE_CODIGO);
                if (validacionEstado != null)
                {
                    if (validacionEstado.ESC_CODIGO == 6)
                    {
                        MessageBox.Show("Paciente ya fue facturado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            try
            {
                //string numatencion = gridAtenciones.ActiveRow.Cells["ATE_CODIGO"].Value.ToString();
                //ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(numatencion);

                string habitacionNueva = "";
                var ventana = new FrmRevertirSalida { Atencion = ultimaAtencion };
                ventana.ShowDialog();
                habitacionNueva = ventana.habitacion;
                if (ventana.estado)
                {
                    txt_habitacion.Text = habitacionNueva;
                }
                CargarAtencionesPaciente(pacienteActual.PAC_CODIGO);

                DataTable AreaActualHabitacion = new DataTable();

                AreaActualHabitacion = NegHabitaciones.AreaActualHab(ultimaAtencion.ATE_CODIGO);
                if (AreaActualHabitacion.Rows.Count > 0)
                {
                    txtAreaActual.Text = AreaActualHabitacion.Rows[0][3].ToString();
                }

                ////Cambios Edgar 20210325  Alerta para cambiar de habitacion a paciente de emergencia 
                //if (NegAtenciones.ValidaEmergencia(ultimaAtencion.ATE_CODIGO))
                //{
                //    if(MessageBox.Show("¿Está seguro de cambiar a paciente de EMERGENCIA a HOSPITALIZACIÓN?",
                //        "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){


                //        string habitacionNueva = "";
                //        var ventana = new FrmRevertirSalida { Atencion = ultimaAtencion };
                //        ventana.ShowDialog();
                //        habitacionNueva = ventana.habitacion;
                //        if (ventana.estado)
                //        {
                //            txt_habitacion.Text = habitacionNueva;
                //        }
                //        CargarAtencionesPaciente(pacienteActual.PAC_CODIGO);
                //    }
                //}
                //else
                //{
                //    string habitacionNueva = "";
                //    var ventana = new FrmRevertirSalida { Atencion = ultimaAtencion };
                //    ventana.ShowDialog();
                //    habitacionNueva = ventana.habitacion;
                //    if (ventana.estado)
                //    {
                //        txt_habitacion.Text = habitacionNueva;
                //    }
                //    CargarAtencionesPaciente(pacienteActual.PAC_CODIGO);
                //}
                //----fin


                //string habitacionNueva = "";
                //var ventana = new FrmRevertirSalida { Atencion = ultimaAtencion };
                //ventana.ShowDialog();
                //habitacionNueva = ventana.habitacion;
                //if (ventana.estado)
                //{
                //    txt_habitacion.Text = habitacionNueva;
                //}
                //CargarAtencionesPaciente(pacienteActual.PAC_CODIGO);
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
                        dr2["Path"] = NegUtilitarios.RutaLogo("General");
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
                    MessageBox.Show("PACIENTE NO CUENTA CON GARANTIA(S)", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbn_h_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab)
            //{
            //    txt_codPais.Focus();
            //}
            //else if (e.KeyCode == Keys.Enter)
            //{
            //    txt_codPais.Focus();
            //}
        }

        private void rbn_m_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab)
            //{
            //    txt_pais.Focus();
            //}
            //else if (e.KeyCode == Keys.Enter)
            //{
            //    rbn_m.Checked = true;
            //    rbn_h.Checked = false;
            //    txt_pais.Focus();
            //}
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
            }
            else
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
                    string prueba = gridGarantias.Rows[index].Cells[0].Value.ToString();
                    if (gridGarantias.Rows[index].Cells[0].Value.ToString() == "2") //para voucher
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
                        else if (columna == 7)
                        {
                            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("BANCOS");
                            ayuda.ShowDialog();
                            if (ayuda.codigo != string.Empty)
                            {
                                gridGarantias.Rows[index].Cells[columna].Value = ayuda.producto;
                            }
                        }
                    }
                    else if (gridGarantias.Rows[index].Cells[0].Value.ToString() == "1") //para cheque
                    {
                        if (columna == 7)
                        {
                            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("BANCOS");
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
                    else if (columna == 7)
                    {
                        frm_ImagenAyuda ayuda = new frm_ImagenAyuda("BANCOS");
                        ayuda.ShowDialog();
                        if (ayuda.codigo != string.Empty)
                        {
                            gridGarantias.Rows[index].Cells[columna].Value = ayuda.producto;
                        }
                    }
                }
                //cambios Edgar 20210108
                else if (Convert.ToString(gridGarantias.Rows[index].Cells[1].Value).Contains("CHEQUE"))
                {
                    if (columna == 7)
                    {
                        frm_ImagenAyuda ayuda = new frm_ImagenAyuda("BANCOS");
                        ayuda.ShowDialog();
                        if (ayuda.codigo != string.Empty)
                        {
                            gridGarantias.Rows[index].Cells[columna].Value = ayuda.producto;
                        }
                    }
                }
            }
        }

        private void letreroParaHabitacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //limpiar tablas
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.DeleteTable("AuxAdmision");
                imagenL = new ReportesHistoriaClinica();
                imagenL.DeleteTable("AuxAdmision");
                //empaquetar y guardar en tablas access
                string[] x = new string[] { txt_apellido1.Text, txt_apellido2.Text, txt_nombre1.Text, txt_nombre2.Text, txt_cedula.Text };
                ReportesHistoriaClinica imagen = new ReportesHistoriaClinica();
                imagen.InsertLetrero(x);
                //llamo al reporte
                frmReportes form = new frmReportes();
                form.reporte = "LETRERO";
                form.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aCTACOMPROMISOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //limpiar tablas
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.DeleteTable("AuxAdmision");
                //empaquetar y guardar en tablas access
                //string[] x = new string[] { txt_apellido1.Text, txt_apellido2.Text, txt_nombre1.Text, txt_nombre2.Text, txt_cedula.Text };
                string[] x = new string[] { txt_nombreAcomp.Text, txt_apellido1.Text + " " + txt_apellido2.Text + " " + txt_nombre1.Text + " " + txt_nombre2.Text, " ", " ", txt_cedulaAcomp.Text };
                ReportesHistoriaClinica imagen = new ReportesHistoriaClinica();
                imagen.InsertLetrero(x);

                //llamo al reporte
                frmReportes form = new frmReportes();
                form.reporte = "ACTA";
                form.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void brazaleteYEtquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void datosEnFormulariosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void recuadroFormulariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //limpiar tablas
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.DeleteTable("AuxAdmision");
                //empaquetar y guardar en tablas access
                string convenio = "";
                foreach (DataGridViewRow row in gridAseguradoras.Rows)
                {
                    convenio += (row.Cells["nomCategoria"].Value.ToString()) + ". ";

                }
                ///edad
                var now = DateTime.Now;
                var birthday = dtp_fecnac.Value;
                var yearsOld = now - birthday;

                int years = (int)(yearsOld.TotalDays / 365.25);
                int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

                TimeSpan age = now - birthday;
                DateTime totalTime = new DateTime(age.Ticks);

                string[] x = new string[] { txt_nombre1.Text, txt_nombre2.Text, txt_apellido1.Text, txt_apellido2.Text, txt_cedula.Text,
                                            txtNombreMedico.Text, txt_telefono1.Text + '/'+txt_telefono2.Text, txt_historiaclinica.Text.Trim(),
                                            txt_numeroatencion.Text, dateTimeFecIngreso.Value.ToString(), txt_habitacion.Text,
                                            convenio, years + " Años"};
                //convenio, years + " AÑOS, " + months + " MESES, " + totalTime.Day + " DIAS"};
                ReportesHistoriaClinica imagen = new ReportesHistoriaClinica();
                imagen.InsertAuxAdmisones(x);
                //llamo al reporte
                frmReportes form = new frmReportes();
                form.reporte = "EncabezadosForms";
                form.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void adminMedicamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //limpiar tablas
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.DeleteTable("AuxAdmision");
                //empaquetar y guardar en tablas access
                string convenio = "";
                foreach (DataGridViewRow row in gridAseguradoras.Rows)
                {
                    convenio += (row.Cells["nomCategoria"].Value.ToString()) + ". ";

                }
                ///edad
                var now = DateTime.Now;
                var birthday = dtp_fecnac.Value;
                var yearsOld = now - birthday;

                int years = (int)(yearsOld.TotalDays / 365.25);
                int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

                TimeSpan age = now - birthday;
                DateTime totalTime = new DateTime(age.Ticks);

                string[] x = new string[] { txt_nombre1.Text, txt_nombre2.Text, txt_apellido1.Text, txt_apellido2.Text, txt_cedula.Text,
                                            txtNombreMedico.Text, txt_telefono1.Text + '/'+txt_telefono2.Text, txt_historiaclinica.Text.Trim(),
                                            txt_numeroatencion.Text, dateTimeFecIngreso.Value.ToString(), txt_habitacion.Text,
                                            convenio, years + " Años"};
                //convenio, years + " AÑOS, " + months + " MESES, " + totalTime.Day + " DIAS"};
                ReportesHistoriaClinica imagen = new ReportesHistoriaClinica();
                imagen.InsertAuxAdmisones(x);
                //llamo al reporte
                frmReportes form = new frmReportes();
                form.reporte = "EncabezadosForms2";
                form.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listaDeVerificacionDeSeguridadServicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //limpiar tablas
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.DeleteTable("AuxAdmision");
                //empaquetar y guardar en tablas access
                string convenio = "";
                foreach (DataGridViewRow row in gridAseguradoras.Rows)
                {
                    convenio += (row.Cells["nomCategoria"].Value.ToString()) + ". ";

                }
                ///edad
                var now = DateTime.Now;
                var birthday = dtp_fecnac.Value;
                var yearsOld = now - birthday;

                int years = (int)(yearsOld.TotalDays / 365.25);
                int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

                TimeSpan age = now - birthday;
                DateTime totalTime = new DateTime(age.Ticks);

                string[] x = new string[] { txt_nombre1.Text, txt_nombre2.Text, txt_apellido1.Text, txt_apellido2.Text, txt_cedula.Text,
                                            txtNombreMedico.Text, txt_telefono1.Text + '/'+txt_telefono2.Text, txt_historiaclinica.Text.Trim(),
                                            txt_numeroatencion.Text, dateTimeFecIngreso.Value.ToString(), txt_habitacion.Text,
                                            convenio, years + " Años"};
                //convenio, years + " AÑOS, " + months + " MESES, " + totalTime.Day + " DIAS"};
                ReportesHistoriaClinica imagen = new ReportesHistoriaClinica();
                imagen.InsertAuxAdmisones(x);
                //llamo al reporte
                frmReportes form = new frmReportes();
                form.reporte = "EncabezadosForms3";
                form.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraButton1_Click_1(object sender, EventArgs e)
        {
            Formulario.frm_BusquedaCIE10 cieDiez = new His.Formulario.frm_BusquedaCIE10();
            cieDiez.ShowDialog();
            txt_diagnostico.Text = cieDiez.resultado;
        }

        private void txtGS_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_pais_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;

            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
        }

        private void cmb_ciudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cb_etnia_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Tab)
            //    cb_gruposanguineo.Focus();
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
        }

        private void cb_gruposanguineo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
        }

        private void ayudaNacionalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void groupBoxGenero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void ayudaPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void ayudaProvincia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void ayudaCanton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void ayudaParroquia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void ayudaBarrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void ayudaEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cmb_estadocivil_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
        }

        private void cmb_ciudadano_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    SendKeys.SendWait("{TAB}");
            //}
        }

        private void chkFallecido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dtpFallecido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_telfRef2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["atencion"];
                SendKeys.SendWait("{TAB}");
            }
        }
        public bool desactivada = false; //cuando la atencion ha sido desactivada
        private void rbDesactivada_CheckedChanged(object sender, EventArgs e)
        {
            DataTable validador = new DataTable();
            if (NegParametros.ParametroAdmisionDesactivacion())
            {
                if (rbDesactivada.Checked)
                {
                    int ver;
                    if (txt_numeroatencion.Text.Trim() != "")
                        validador = NegAtenciones.ValidaEstatusAtencion(Convert.ToInt64(txt_numeroatencion.Text.Trim()));
                    else
                    {
                        MessageBox.Show("No se encontro nro de atencion del paciente. Consulte con el Administrador", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        erroresPaciente.SetError(txt_numeroatencion, "No hay Nro de Atención.");
                        return;
                    }
                    if (validador.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(validador.Rows[0][0].ToString()) == 0)
                        {
                            if (ultimaAtencion.ESC_CODIGO == 13)
                            {
                                groupBox3.Enabled = false;
                                btnActualizar.Enabled = false;
                            }
                            else
                            {
                                frm_CuentaDesactivada cuenta = new frm_CuentaDesactivada(txt_numeroatencion.Text.Trim());
                                cuenta.ShowDialog();
                                ver = cuenta.verificador;
                                if (ver == 0)
                                {
                                    groupBox3.Enabled = false;
                                    btnActualizar.Enabled = false;
                                    desactivada = true;
                                }
                                else
                                {
                                    rbActiva.Checked = true;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Atención no se puede dasactivar, cuenta con productos y/o servicios, por favor, Dar de ALta y Facturar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            rbActiva.Enabled = true;
                        }
                    }
                    else
                    {
                        //frm_CuentaDesactivada cuenta = new frm_CuentaDesactivada(txt_numeroatencion.Text.Trim());
                        //cuenta.ShowDialog();
                        //ver = cuenta.verificador;
                        //if (ver == 0)
                        //{
                        //    groupBox3.Enabled = false;
                        //    btnActualizar.Enabled = false;
                        //    desactivada = true;
                        //}
                        //else
                        //{
                        //    rbActiva.Checked = true;
                        //}
                        //////MessageBox.Show("No se encontraron datos de esa atención.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //////return;
                    }
                }
                else
                {
                    validador = NegAtenciones.ValidaEstatusAtencion(Convert.ToInt64(txt_numeroatencion.Text.Trim()));
                    if (validador.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(validador.Rows[0][0].ToString()) == 0)
                        {
                            rbActiva.Checked = true;
                            rbDesactivada.Checked = false;
                        }
                        else
                        {
                            rbDesactivada.Checked = true;
                            rbActiva.Checked = false;
                        }
                    }
                }
            }
            else
            {
                rbActiva.Checked = true;
                MessageBox.Show("No tiene permiso para poder desactivar cuenta.\r\nComuniquese con el Administrador.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tabulador_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            if (chkFallecido.Checked == true && fallecido == true)
            {
                btnActualizar.Enabled = false;
                btnEliminar.Visible = true;
                if (tabulador.Tabs["paciente"].Active == true)
                {
                    ultraTabPageControl1.Enabled = false;
                    tabulador.Tabs["atencion"].Enabled = true;
                    tabulador.Tabs["gridatenciones"].Enabled = true;
                    tabulador.Tabs["certificado"].Enabled = true;
                }
                if (tabulador.Tabs["atencion"].Active == true)
                {
                    ultraTabPageControl2.Enabled = false;
                    tabulador.Tabs["paciente"].Enabled = true;
                    tabulador.Tabs["gridatenciones"].Enabled = true;
                    tabulador.Tabs["certificado"].Enabled = true;
                }
                if (tabulador.Tabs["gridatenciones"].Active == true)
                {
                    ultraTabPageControl3.Enabled = true;
                    tabulador.Tabs["paciente"].Enabled = true;
                    tabulador.Tabs["atencion"].Enabled = true;
                    tabulador.Tabs["certificado"].Enabled = true;
                }
                if (tabulador.Tabs["certificado"].Active == true)
                {
                    ultraTabPageControl11.Enabled = false;
                    tabulador.Tabs["paciente"].Enabled = true;
                    tabulador.Tabs["atencion"].Enabled = true;
                    tabulador.Tabs["gridatenciones"].Enabled = true;
                }
            }
            else
            {
                if (consultas == true && txt_historiaclinica.Text.Trim() != "")
                {
                    tabulador.Tabs["paciente"].Enabled = true;
                    tabulador.Tabs["atencion"].Enabled = true;
                    tabulador.Tabs["gridatenciones"].Enabled = true;
                    tabulador.Tabs["certificado"].Enabled = true;
                    btnGuardar.Enabled = false;
                    toolStripNuevo.Enabled = true;
                    btnHabitaciones.Enabled = true;
                    btnFormularios.Enabled = true;
                    deshabilitarCamposAtencion();
                    deshabilitarCamposDireccion();
                    deshabilitarCamposPaciente();
                    if (desactivada == true || modificar == true)
                        btnActualizar.Enabled = false;
                    else
                        btnActualizar.Enabled = true;
                    btnEliminar.Visible = false;
                    btnHabitaciones.Enabled = false;
                }
            }
        }

        private void txt_emailAcomp_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_emailAcomp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (ComprobarFormatoEmail(txt_emailAcomp.Text) == true)
                {
                    e.Handled = true;
                    txt_emailAcomp.Focus();
                }
                else
                {
                    MessageBox.Show("DIRECCIÓN DE CORREO ELECTRONICO NO ES VALIDA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_emailAcomp.Text = string.Empty;
                }
            }
        }

        //Cambios Edgar 20210216
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelar = 1;
            if (btnGuardar.Enabled == true)
            {
                if (MessageBox.Show("¿Está seguro que desea \"Cancelar\"?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.Yes)
                {
                    erroresPaciente.Clear();
                    pacienteNuevo = false;
                    direccionNueva = false;
                    atencionNueva = false;
                    consultas = false;
                    modificar = false;
                    fallecido = false;
                    desactivada = false;
                    txt_numeroatencion.Text = string.Empty;
                    txt_historiaclinica.Text = string.Empty;
                    CargarPaciente(txt_historiaclinica.Text, 0);
                    txt_historiaclinica.ReadOnly = true;
                    toolStripNuevo.Enabled = true;
                    btnActualizar.Enabled = false;
                    btnEliminar.Visible = false;
                    btnPr.Enabled = false;
                    txt_apellidoActualizar1.Text = "";
                    txt_apellidoActualizar2.Text = "";
                    txt_nombreActualizar1.Text = "";
                    txt_nombreActualizar2.Text = "";
                    gb_consultaRegistro.Visible = false;
                    cb_gruposanguineo.SelectedIndex = -1;
                    cmb_estadocivil.SelectedIndex = -1;
                }
            }
            else
            {
                erroresPaciente.Clear();
                pacienteNuevo = false;
                direccionNueva = false;
                atencionNueva = false;
                consultas = false;
                modificar = false;
                fallecido = false;
                txt_numeroatencion.Text = string.Empty;
                txt_historiaclinica.Text = string.Empty;
                CargarPaciente(txt_historiaclinica.Text, 0);
                txt_historiaclinica.ReadOnly = true;
                toolStripNuevo.Enabled = true;
                btnActualizar.Enabled = false;
                btnEliminar.Visible = false;
                txt_apellidoActualizar1.Text = "";
                txt_apellidoActualizar2.Text = "";
                txt_nombreActualizar1.Text = "";
                txt_nombreActualizar2.Text = "";
                gb_consultaRegistro.Visible = false;
            }
            consultaExterna = 0;
            btn_Subsecuente.Visible = false;
            btn_Subsecuente.Enabled = false;
            AtencionSubsecuente = 0;
            rbn_h.Checked = false;
            rbn_m.Checked = false;
            cb_gruposanguineo.SelectedIndex = -1;
            cmb_estadocivil.SelectedIndex = -1;
            cmb_ciudadano.SelectedIndex = -1;
            cb_netnica.SelectedIndex = -1;
            cb_pueblo.SelectedIndex = -1;
            txt_neducacion.Text = "";
            txt_seguro.Text = "";
            txt_tempresa.Text = "";
            txt_bono.Text = "";
        }

        public void CargarLocalizacion()
        {
            try
            {
                if (cmb_pais.SelectedIndex >= 0 && cargaciu == true)
                {
                    string codiinec = cmb_pais.SelectedValue.ToString();

                    DIVISION_POLITICA pais = paises.FirstOrDefault(p => p.DIPO_CODIINEC == codiinec);

                    cmb_ciudad.DataSource = null;
                    provincias = NegDivisionPolitica.RecuperarDivisionPolitica(pais.DIPO_CODIINEC);
                    cmb_ciudad.DataSource = provincias;
                    cmb_ciudad.ValueMember = "DIPO_CODIINEC";
                    cmb_ciudad.DisplayMember = "DIPO_NOMBRE";
                    //cmb_ciudad.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //cmb_ciudad.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                    if (pacienteNuevo == true && cmb_pais.SelectedValue.ToString() == "57")
                    {
                        //if (pais.NACIONALIDAD != null && pais.NACIONALIDAD != string.Empty)
                        //    txt_nacionalidad.Text = pais.NACIONALIDAD;
                        //else
                        //    txt_nacionalidad.Text = string.Empty;

                        if (pais.DIPO_CODIINEC == AdmisionParametros.DefPais)
                        {
                            //cmb_ciudad.SelectedValue = AdmisionParametros.DefProvincia; // SECOMENTA POR QUE LAS VARIABLES SE ENCONTRABAN QUEMADAS EN EL SISTEMA // Mario Valencia // 12-02-2024
                            //txt_codPais.Text = AdmisionParametros.DefPais;
                            //txt_codProvincia.Text = AdmisionParametros.DefProvincia;
                            //txt_codCanton.Text = AdmisionParametros.DefCanton;
                            //txt_nacionalidad.Text = "ECUATORIANO";
                            cmb_ciudad.SelectedValue = Convert.ToString(NegParametros.RecuperaValorParSvXcodigo(73));
                            txt_codPais.Text = AdmisionParametros.DefPais;
                            txt_codProvincia.Text = Convert.ToString(NegParametros.RecuperaValorParSvXcodigo(73));
                            txt_codCanton.Text = Convert.ToString(NegParametros.RecuperaValorParSvXcodigo(74));
                            txt_nacionalidad.Text = "ECUATORIANO";
                        }
                        else
                        {
                            txt_codPais.Text = string.Empty;
                            txt_codProvincia.Text = string.Empty;
                            txt_codCanton.Text = string.Empty;
                        }

                    }
                    else
                    {
                        if (cmb_pais.SelectedValue.ToString() == "57")
                            txt_nacionalidad.Text = "ECUATORIANO";
                        else
                        {
                            txt_nacionalidad.Text = "";
                            cmb_ciudad.Text = cmb_pais.Text;
                            txt_nacionalidad.Text = cmb_pais.Text;
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
                _pais++;
            }
            catch (System.Exception err)
            {
                MessageBox.Show("Algo Ocurrio al cargar la localización.\r\nMás detelle: " + err.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void chkDiscapacidad_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkDiscapacidad.Checked)
            {
                txtPorcentageDA.Enabled = true;
                txtIdDiscapacidad.Enabled = true;
                cmbTiposDiscapacidadesDA.Enabled = true;
            }
            else
            {
                txtPorcentageDA.Enabled = false;
                txtIdDiscapacidad.Enabled = false;
                cmbTiposDiscapacidadesDA.Enabled = false;
                txtPorcentageDA.Text = "0";
                txtIdDiscapacidad.Text = "";
                cmbTiposDiscapacidadesDA.SelectedIndex = 0;
            }
        }

        private void cboPaquete_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_tipoingreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_tipoatencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_formaLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_tiporeferido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbTipoAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbAccidente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbConvenioPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbTiposDiscapacidadesDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cb_seguros_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            frm_Fallecido x = new frm_Fallecido();
            x.pac_codigo = pacienteActual.PAC_CODIGO;
            x.ShowDialog();
        }

        private void chkFallecido_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFallecido.Checked)
            {
                if (pacienteNuevo != true)
                {
                    frm_Fallecido x = new frm_Fallecido();
                    x.pac_codigo = pacienteActual.PAC_CODIGO;

                    if (fallecido != true)
                    {
                        x.ShowDialog();
                        if (x.Validador != true)
                        {
                            chkFallecido.Checked = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El paciente aun no ha sido creado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    chkFallecido.Checked = false;
                }
            }
        }

        private void btnfacturaDatos_Click(object sender, EventArgs e)
        {
            if (txt_nombreRef.Text != "" && txt_telfRef.Text != ""
               && txtRefDireccion.Text != "" && txtRefDireccion.Text != "")
            {
                txtFNombre.Text = txt_nombreRef.Text;
                txtFCelular.Text = txt_telfRef.Text;
                txtFDireccion.Text = txtRefDireccion.Text;
            }
            else
            {
                MessageBox.Show("No tiene registrado a persona \r\n\"En caso de Emergencia llamar a..\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtFEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (ComprobarFormatoEmail(txtFEmail.Text) == true)
                {
                    e.Handled = true;
                    txtFEmail.Focus();
                }
                else
                {
                    MessageBox.Show("DIRECCIÓN DE CORREO ELECTRONICO NO ES VALIDA", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFEmail.Text = string.Empty;
                }
            }
        }

        private void txtFCedula_Leave(object sender, EventArgs e)
        {
            if (txtFCedula.TextLength == 13)
            {
                if (!NegValidaciones.ValidarRuc(txtFCedula.Text.Trim()))
                {
                    txtFCedula.Text = "";
                    ClienteExiste = false; //Si ruc no es valido
                }

                else
                {
                    DataTable Cliente = new DataTable();//Recibe los datos del cliente del sic en caso de existir
                    Cliente = NegAtenciones.CargarClienteSic(txtFCedula.Text);
                    if (Cliente.Rows.Count > 0)
                    {
                        txtFNombre.Text = Cliente.Rows[0][0].ToString();//Nombre del cliente
                        txtFDireccion.Text = Cliente.Rows[0][1].ToString(); //Direccion del cliente
                        txtFEmail.Text = Cliente.Rows[0][2].ToString(); //Email del cliente
                        if (Cliente.Rows[0][3].ToString().Length > 10)
                        {
                            string[] num = Cliente.Rows[0][3].ToString().Split('P', 'T', 'A');
                            if (num[2] == "09")
                                txtFCelular.Text = num[2] + num[3];
                            else
                                txtFCelular.Text = num[3];
                        }
                        else
                        {
                            if (Cliente.Rows[0][3].ToString() != "PAT")
                                txtFCelular.Text = Cliente.Rows[0][3].ToString();
                        }
                        ClienteExiste = true;
                    }
                    else
                    {
                        txtFNombre.ReadOnly = false;
                        txtFDireccion.ReadOnly = false;
                        txtFCelular.ReadOnly = false;
                        txtFEmail.ReadOnly = false;
                        ClienteExiste = false;
                    }
                }
            }
            else if (txtFCedula.Text.Length == 10)
            {
                if (NegValidaciones.esCedulaValida(txtFCedula.Text))
                {
                    DataTable Cliente = new DataTable();//Recibe los datos del cliente del sic en caso de existir
                    Cliente = NegAtenciones.CargarClienteSic(txtFCedula.Text);
                    if (Cliente.Rows.Count > 0)
                    {
                        txtFNombre.Text = Cliente.Rows[0][0].ToString();//Nombre del cliente
                        txtFDireccion.Text = Cliente.Rows[0][1].ToString(); //Direccion del cliente
                        txtFEmail.Text = Cliente.Rows[0][2].ToString(); //Email del cliente
                        if (Cliente.Rows[0][3].ToString().Length > 10)
                        {
                            string[] num = Cliente.Rows[0][3].ToString().Split('P', 'T');
                        }
                        else
                        {
                            txtFCelular.Text = Cliente.Rows[0][3].ToString();
                        }
                        ClienteExiste = true;
                    }
                    else
                    {
                        txtFNombre.ReadOnly = false;
                        txtFDireccion.ReadOnly = false;
                        txtFCelular.ReadOnly = false;
                        txtFEmail.ReadOnly = false;
                        ClienteExiste = false;
                    }
                }
                else
                {
                    MessageBox.Show("Cedula no valida", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtFNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                txtFDireccion.Focus();
        }

        private void txtFDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                txtFCedula.Focus();
        }

        private void txtFCedula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtFCedula.Text == "")
                    txtFCelular.Focus();
                else
                {
                    if (txtFCedula.TextLength == 13)
                    {
                        if (!NegValidaciones.ValidarRuc(txtFCedula.Text.Trim()))
                        {
                            txtFCedula.Text = "";
                            ClienteExiste = false; //Si ruc no es valido
                        }

                        else
                        {
                            DataTable Cliente = new DataTable();//Recibe los datos del cliente del sic en caso de existir
                            Cliente = NegAtenciones.CargarClienteSic(txtFCedula.Text);
                            if (Cliente.Rows.Count > 0)
                            {
                                txtFNombre.Text = Cliente.Rows[0][0].ToString();//Nombre del cliente
                                txtFDireccion.Text = Cliente.Rows[0][1].ToString(); //Direccion del cliente
                                txtFEmail.Text = Cliente.Rows[0][2].ToString(); //Email del cliente
                                if (Cliente.Rows[0][3].ToString().Length > 10)
                                {
                                    string[] num = Cliente.Rows[0][3].ToString().Split('P', 'T', 'A');
                                    if (num[2] == "09")
                                        txtFCelular.Text = num[2] + num[3];
                                    else
                                        txtFCelular.Text = num[3];
                                }
                                else
                                {
                                    if (Cliente.Rows[0][3].ToString() != "PAT")
                                        txtFCelular.Text = Cliente.Rows[0][3].ToString();
                                }
                                ClienteExiste = true;
                            }
                            else
                            {
                                txtFNombre.ReadOnly = false;
                                txtFDireccion.ReadOnly = false;
                                txtFCelular.ReadOnly = false;
                                txtFEmail.ReadOnly = false;
                                ClienteExiste = false;
                            }
                        }
                    }
                    else if (txtFCedula.Text.Length == 10)
                    {
                        if (NegValidaciones.esCedulaValida(txtFCedula.Text))
                        {
                            DataTable Cliente = new DataTable();//Recibe los datos del cliente del sic en caso de existir
                            Cliente = NegAtenciones.CargarClienteSic(txtFCedula.Text);
                            if (Cliente.Rows.Count > 0)
                            {
                                txtFNombre.Text = Cliente.Rows[0][0].ToString();//Nombre del cliente
                                txtFDireccion.Text = Cliente.Rows[0][1].ToString(); //Direccion del cliente
                                txtFEmail.Text = Cliente.Rows[0][2].ToString(); //Email del cliente
                                if (Cliente.Rows[0][3].ToString().Length > 10)
                                {
                                    string[] num = Cliente.Rows[0][3].ToString().Split('P', 'T');
                                }
                                else
                                {
                                    txtFCelular.Text = Cliente.Rows[0][3].ToString();
                                }
                                ClienteExiste = true;
                            }
                            else
                            {
                                txtFNombre.ReadOnly = false;
                                txtFDireccion.ReadOnly = false;
                                txtFCelular.ReadOnly = false;
                                txtFEmail.ReadOnly = false;
                                ClienteExiste = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cedula no valida", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }

        private void txtFCelular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                txtFEmail.Focus();
        }

        private void etiquetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region ETIQUETA
            string HC = txt_historiaclinica.Text.Trim().PadLeft(7, '0');
            string ATE_NUM = txt_numeroatencion.Text.Trim().PadLeft(7, '0');
            string xgenero = "MASCULINO";
            if (!rbn_h.Checked)
                xgenero = "FEMENINO";

            var now = DateTime.Now;
            var birthday = dtp_fecnac.Value;
            var yearsOld = now - birthday;

            int years = (int)(yearsOld.TotalDays / 365.25);
            int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

            TimeSpan age = now - birthday;
            DateTime totalTime = new DateTime(age.Ticks);

            DSBarCode ET = new DSBarCode();
            DataRow Etiqueta;

            Etiqueta = ET.Tables["BarCode"].NewRow();
            //Etiqueta["Barras"] = txt_cedula.Text.Trim();
            Etiqueta["Logo"] = NegUtilitarios.RutaLogo("Etiqueta");
            Etiqueta["Empresa"] = Sesion.nomEmpresa;
            Etiqueta["Paciente"] = txt_apellido1.Text + ' ' + txt_apellido2.Text + ' ' + txt_nombre1.Text + ' ' + txt_nombre2.Text;
            Etiqueta["Edad"] = "EDAD: " + years + " AÑOS";
            Etiqueta["Sexo"] = "SEXO: " + xgenero;
            Etiqueta["HC"] = "HCL: " + txt_historiaclinica.Text.Trim();
            Etiqueta["Identificacion"] = "IDENTIFICACIÓN:" + txt_cedula.Text.Trim();
            Etiqueta["F_Ingreso"] = "F. INGRESO: " + dateTimeFecIngreso.Value.ToString();
            Etiqueta["Triaje"] = "Hab.: " + txt_habitacion.Text.ToString().Trim();
            Etiqueta["Atencion"] = "HAB.: " + txt_habitacion.Text.ToString().Trim();
            Etiqueta["Medico"] = "M. TRATANTE: " + txtNombreMedico.Text.Trim();
            //Etiqueta["Referido"] = "REFERIDO: " + cmb_tiporeferido.Text.ToString();


            ET.Tables["BarCode"].Rows.Add(Etiqueta);
            frmReportes etiqueta = new frmReportes(1, "Etiqueta", ET); //SE CAMBIO DE REPORTE DE ETIQUETA POR ETIQUETA_ADMISION POR QUE SALIA CON HOJAS EN BLANCO
            etiqueta.Show();
            #endregion

        }

        #endregion

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        public void limpiarFacturar()
        {
            txt_nombreAcomp.Text = "";
            txt_cedulaAcomp.Text = "";
            txt_direccionAcomp.Text = "";
            txt_parentescoAcomp.Text = "";
            txt_emailAcomp.Text = "";
            txt_telefonoAcomp.Text = String.Empty;
            uBtnAddAcompaniante.Visible = false;
            btnReligioso.Visible = false;
            txt_ciudadAcomp.Text = "";
        }

        private void cb_personaFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_personaFactura.SelectedIndex != -1)
            {
                if (cb_personaFactura.SelectedIndex == 0)//Paciente
                {
                    limpiarFacturar();
                    txt_nombreAcomp.Text = txt_apellido1.Text.Trim() + " " + txt_apellido2.Text.Trim() + " " + txt_nombre1.Text.Trim() + " " + txt_nombre2.Text.Trim();
                    txt_cedulaAcomp.Text = txt_cedula.Text.Trim();
                    txt_direccionAcomp.Text = txt_direccion.Text.Trim();
                    txt_parentescoAcomp.Text = txt_parentRef.Text.Trim();
                    txt_emailAcomp.Text = txt_email.Text;
                    txt_telefonoAcomp.Text = txt_telefono2.Text;
                    txt_ciudadAcomp.Text = txt_canton.Text;
                    uBtnAddAcompaniante.Visible = false;
                    btnReligioso.Visible = false;
                }
                else if (cb_personaFactura.SelectedIndex == 1)
                {
                    limpiarFacturar();
                    uBtnAddAcompaniante.Visible = false;
                    btnReligioso.Visible = true;
                }
                else if (cb_personaFactura.SelectedIndex == 2)//Religiosos
                {
                    limpiarFacturar();
                    uBtnAddAcompaniante.Visible = false;
                    btnReligioso.Visible = true;
                }
                else if (cb_personaFactura.SelectedIndex == 3) //Otros
                {
                    limpiarFacturar();
                    //uBtnAddAcompaniante.Visible = true;
                    btnReligioso.Visible = false;
                }
            }
        }

        private void btnReligioso_Click(object sender, EventArgs e)
        {
            frm_AyudaReligiosos x = new frm_AyudaReligiosos();
            if (cb_personaFactura.SelectedIndex == 2)
                x.tipo = 0;
            else if (cb_personaFactura.SelectedIndex == 1)
                x.tipo = 1;
            x.ShowDialog();

            if (x.idcongregacion != null)
            {
                txt_nombreAcomp.Text = x.nomcongregacion;
                txt_cedulaAcomp.Text = x.idcongregacion;
                txt_direccionAcomp.Text = x.direccion;
                txt_telefonoAcomp.Text = x.telefono;
                txt_ciudadAcomp.Text = x.ciudad;
                txt_emailAcomp.Text = x.email;
            }
            if (x.email == "")
            {
                DataTable cliente = new DataTable();
                cliente = NegDetalleCuenta.RecuperaOtroCliente(txt_cedulaAcomp.Text);
                foreach (DataRow Item in cliente.AsEnumerable())
                {
                    txt_nombreAcomp.Text = Item["nomcli"].ToString();
                    txt_telefonoAcomp.Text = Item["telcli"].ToString().Trim();
                    txt_direccionAcomp.Text = Item["dircli"].ToString();
                    txt_emailAcomp.Text = Item["email"].ToString();
                }
            }
        }

        private void txtRecomendado_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbRn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRn.Checked == true)
                txt_cedula.MaxLength = 30;
        }

        private void ultraButton3_Click_1(object sender, EventArgs e)
        {
            frmAyudaPreAdmision x = new frmAyudaPreAdmision();
            x.ShowDialog();
            if (x.iden != "")
            {
                txt_cedula.Text = x.iden;
                preAdmision = true;
                preCodigo = x.codigo;
                PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());

                if (consulta != null)
                {
                    txt_historiaclinica.Tag = true;
                    txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString().Trim();
                    dtp_fecnac.Value = consulta.PAC_FECHA_NACIMIENTO.Value.Date;
                    pacienteNuevo = false;
                    PREADMISION pre = new PREADMISION();
                    pre = NegPreadmision.recuperarPreadmision(txt_cedula.Text.Trim());
                    List<PREADMISION_DETALLE> pde = new List<PREADMISION_DETALLE>();
                    pde = NegPreadmision.recuperarPreAdmisionDetalle(pre.PRE_CODIGO);
                    preCodigo = pre.PRE_CODIGO;
                    txtCodMedico.Text = pre.MED_CODIGO.ToString();
                    medico = NegMedicos.recuperarMedico((int)pre.MED_CODIGO);
                    txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                    cmb_tiporeferido.SelectedItem = tipoReferido.FirstOrDefault(t => t.TIR_CODIGO == pre.TIR_CODIGO);
                    cmb_tipoatencion.SelectedItem = tipoTratamiento.FirstOrDefault(t => t.TIA_CODIGO == pre.TIA_CODIGO);
                    foreach (var item in pde)
                    {
                        CIE10 c = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);
                        txt_diagnostico.Text = txt_diagnostico.Text + c.CIE_DESCRIPCION + " ";
                    }
                    CargarPaciente(consulta.PAC_HISTORIA_CLINICA, 1);
                    //tabulador.SelectedTab = paciente;
                    tabulador.Tabs["paciente"].Enabled = true;
                    tabulador.SelectedTab = tabulador.Tabs["paciente"];
                    SendKeys.SendWait("{TAB}");
                }
                else
                    cargarPacientePreAdmision();
                //DataTable jire = new DataTable();
                //jire = NegPacientes.PacienteJire(txt_cedula.Text.Trim());
                //if (jire.Rows.Count > 0)
                //{
                //    txt_historiaclinica.Tag = true;
                //    txt_historiaclinica.Text = jire.Rows[0][1].ToString();
                //}

                MessageBox.Show("Valide informacion antes de guardar", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void btn_Subsecuente_Click(object sender, EventArgs e)
        {
            List<PACIENTES_VISTA> pac = new List<PACIENTES_VISTA>();
            pac = NegPacientes.recuperarPacientePorHistoria(txt_historiaclinica.Text);
            frm_AyudaPacientesSubsecuentes frm = new frm_AyudaPacientesSubsecuentes(pac[0].PAC_CODIGO);
            frm.ShowDialog();
            AtencionSubsecuente = frm.ateCodigo;
        }

        private void btnRegistroCivil_Click(object sender, EventArgs e)
        {
            if (txt_cedula.Text == "")
            {
                MessageBox.Show("Ingrese un numero de cedula valido", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ValidaNube ok = new ValidaNube();
            ok = NegUtilitarios.ValidarDocumento(txt_cedula.Text);
            if (ok != null)
            {
                if (!ok.ok)
                {
                    MessageBox.Show("Número de identifación no válida.", "HIS3000", MessageBoxButtons.OK);
                    return;
                }
                BuscaRegistroCivil(ok.ok);
            }
            else
            {
                MessageBox.Show("Registro Civil Congestionado.", "HIS3000", MessageBoxButtons.OK);
            }
        }
        public void BuscaRegistroCivil(bool ok)
        {
            txt_apellidoActualizar1.Text = "";
            txt_apellidoActualizar2.Text = "";
            txt_nombreActualizar1.Text = "";
            txt_nombreActualizar2.Text = "";

            if (ok)
            {
                PARAMETROS_DETALLE parametroWEB = new PARAMETROS_DETALLE();
                parametroWEB = NegParametros.RecuperaPorCodigo(48);
                if ((bool)parametroWEB.PAD_ACTIVO)
                {
                    btn_actualizarInfo.Visible = true;
                    txt_nombreActualizar1.Text = "";
                    if (txt_cedula.Text.Length == 10 && rbCedula.Checked)
                    {
                        CONTROL_CONSULTA obj = new CONTROL_CONSULTA();
                        obj.ip = Sesion.ip;
                        obj.usuario = Sesion.codUsuario;
                        obj.tipoConsulta = "Admision.";
                        obj.fechaConsulta = DateTime.Now;
                        obj.identificacion = txt_cedula.Text;
                        //if (NegNumeroControl.CreaControlConsulta(obj))
                        //{
                        RegistroCivil registroCivil = new RegistroCivil();
                        PARAMETROS_DETALLE parametro = new PARAMETROS_DETALLE();
                        parametro = NegParametros.RecuperaPorCodigo(48);
                        if ((bool)parametro.PAD_ACTIVO)
                        {
                            registroCivil = NegUtilitarios.ObtenerRegistroCivil(txt_cedula.Text);
                            if (registroCivil != null)
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

                                txt_fecha_nacimiento_actualiza.Text = registroCivil.consulta.FechaNacimiento;
                                txt_genero_actualiza.Text = registroCivil.consulta.Genero;
                                txt_ocupacion_actualiza.Text = registroCivil.consulta.Profesion;
                                txt_instruccion_actualiza.Text = registroCivil.consulta.Instruccion;
                                txt_estado_civil_actualiza.Text = registroCivil.consulta.EstadoCivil;
                                txt_direccion_actualiza.Text = registroCivil.consulta.CalleDomicilio + " " + registroCivil.consulta.NumeracionDomicilio;


                                string[] words = registroCivil.consulta.Nombre.Split(' ');
                                int count = 0;
                                foreach (var item in words)
                                {
                                    count++;
                                    if (count > 3)
                                    {
                                        if (count == 5)
                                        {
                                            MessageBox.Show("El paciente no tiene el formato estandar de los nombres por lo que debe actualizar de forma manual", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            btn_actualizarInfo.Visible = false;
                                        }
                                        if (count >= 4)
                                        {
                                            txt_nombreActualizar2.Text += " ";
                                        }
                                        txt_nombreActualizar2.Text += item;

                                    }
                                }
                                txt_apellidoActualizar1.Text = words[0];
                                txt_apellidoActualizar2.Text = words[1];
                                txt_nombreActualizar1.Text = words[2];
                                gb_consultaRegistro.Visible = true;
                                obj.resultado = registroCivil.consulta.Nombre;
                                NegNumeroControl.CreaControlConsulta(obj);
                            }
                            else
                            {
                                MessageBox.Show("No se puede recuperar información del ciudadano en este momento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Está Institucuión no tiene un plan activo para consultar en el Registro Civil la información del paciente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Formato de cedula incorrecto.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Servicio sin activarse.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_actualizarInfo_Click(object sender, EventArgs e)
        {
            if (txt_apellido1.Text == "")
            {
                txt_apellido1.Text = txt_apellidoActualizar1.Text;
                txt_apellido2.Text = txt_apellidoActualizar2.Text;
                txt_nombre1.Text = txt_nombreActualizar1.Text;
                txt_nombre2.Text = txt_nombreActualizar2.Text.Trim();

                txt_apellidoActualizar1.Text = "";
                txt_apellidoActualizar2.Text = "";
                txt_nombreActualizar1.Text = "";
                txt_nombreActualizar2.Text = "";
                gb_consultaRegistro.Visible = false;
                dtp_fecnac.Value = Convert.ToDateTime(txt_fecha_nacimiento_actualiza.Text);
                if (txt_fecha_nacimiento_actualiza.Text != "")
                {
                    dtp_fecnac.Enabled = false;
                }
                if (txt_genero_actualiza.Text == "HOMBRE")
                {
                    rbn_h.Checked = true;
                    rbn_m.Checked = false;
                    groupBoxGenero.Enabled = false;
                }
                else if (txt_genero_actualiza.Text == "MUJER")
                {
                    rbn_h.Checked = false;
                    rbn_m.Checked = true;
                    groupBoxGenero.Enabled = false;
                }
                txt_ocupacion.Text = txt_ocupacion_actualiza.Text;
                if (txt_ocupacion.Text != "")
                    txt_ocupacion.Enabled = false;
                txt_instuccion.Text = txt_instruccion_actualiza.Text;
                if (txt_instuccion.Text != "")
                    txt_instuccion.Enabled = false;
                if (txt_estado_civil_actualiza.Text != "")
                {
                    var estCiv = 2;
                    if (txt_estado_civil_actualiza.Text == "SOLTERO")
                        estCiv = 2;
                    else if (txt_estado_civil_actualiza.Text == "CASADO")
                        estCiv = 0;
                    else if (txt_estado_civil_actualiza.Text == "DIVORCIADO")
                        estCiv = 1;
                    else if (txt_estado_civil_actualiza.Text == "UNION LIBRE")
                        estCiv = 3;
                    else if (txt_estado_civil_actualiza.Text == "VIUDO")
                        estCiv = 4;
                    cmb_estadocivil.SelectedIndex = estCiv;
                }
                txt_direccion.Text = txt_direccion_actualiza.Text;

                return;
            }
            if (MessageBox.Show("La información actual del paciente va ser remplazada. ¿Desea continuar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                txt_apellido1.Text = txt_apellidoActualizar1.Text;
                txt_apellido2.Text = txt_apellidoActualizar2.Text;
                txt_nombre1.Text = txt_nombreActualizar1.Text;
                txt_nombre2.Text = txt_nombreActualizar2.Text.Trim();

                txt_apellidoActualizar1.Text = "";
                txt_apellidoActualizar2.Text = "";
                txt_nombreActualizar1.Text = "";
                txt_nombreActualizar2.Text = "";
                gb_consultaRegistro.Visible = false;
                dtp_fecnac.Value = Convert.ToDateTime(txt_fecha_nacimiento_actualiza.Text);
                if (txt_fecha_nacimiento_actualiza.Text != "")
                {
                    dtp_fecnac.Enabled = false;
                }
                if (txt_genero_actualiza.Text == "HOMBRE")
                {
                    rbn_h.Checked = true;
                    rbn_m.Checked = false;
                    groupBoxGenero.Enabled = false;
                }
                else if (txt_genero_actualiza.Text == "MUJER")
                {
                    rbn_h.Checked = false;
                    rbn_m.Checked = true;
                    groupBoxGenero.Enabled = false;
                }
                txt_ocupacion.Text = txt_ocupacion_actualiza.Text;
                if (txt_ocupacion.Text != "")
                    txt_ocupacion.Enabled = false;
                txt_instuccion.Text = txt_instruccion_actualiza.Text;
                if (txt_instuccion.Text != "")
                    txt_instuccion.Enabled = false;
                if (txt_estado_civil_actualiza.Text != "")
                {
                    var estCiv = 2;
                    if (txt_estado_civil_actualiza.Text == "SOLTERO")
                        estCiv = 2;
                    else if (txt_estado_civil_actualiza.Text == "CASADO")
                        estCiv = 0;
                    else if (txt_estado_civil_actualiza.Text == "DIVORCIADO")
                        estCiv = 1;
                    else if (txt_estado_civil_actualiza.Text == "UNION LIBRE")
                        estCiv = 3;
                    else if (txt_estado_civil_actualiza.Text == "VIUDO")
                        estCiv = 4;
                    cmb_estadocivil.SelectedIndex = estCiv;
                }
                txt_direccion.Text = txt_direccion_actualiza.Text;
            }
        }

        private void btn_imprimir_datos_facturar_a_Click(object sender, EventArgs e)
        {
            ImpresionFacturaANombreDe();
        }

        public void ImpresionFacturaANombreDe()
        {
            ImprimeFacturaNombre dtx = new ImprimeFacturaNombre();
            DataRow drAdmision;

            drAdmision = dtx.Tables["facturaNombre"].NewRow();
            drAdmision["Empresa"] = txt_habitacion.Text;
            drAdmision["ruta"] = NegUtilitarios.RutaLogo("General");
            drAdmision["paciente"] = txt_apellido1.Text + " " + txt_apellido2.Text + " " + txt_nombre1.Text + " " + txt_nombre2.Text;
            drAdmision["atencion"] = txt_numeroatencion.Text;
            drAdmision["hc"] = txt_historiaclinica.Text;
            drAdmision["fechaAdmision"] = dateTimeFecIngreso.Text;
            drAdmision["convenioPago"] = txt_obPago.Text;
            drAdmision["medicoTratante"] = txtNombreMedico.Text;
            drAdmision["nombre"] = txt_nombreAcomp.Text;
            drAdmision["ciudad"] = txt_ciudadAcomp.Text;
            drAdmision["direccion"] = txt_direccionAcomp.Text;
            drAdmision["identificacion"] = txt_cedulaAcomp.Text;
            drAdmision["telefono"] = txt_telefonoAcomp.Text;
            drAdmision["email"] = txt_emailAcomp.Text;

            USUARIOS user = new USUARIOS();
            user = NegUsuarios.RecuperarUsuarioID(Sesion.codUsuario);

            drAdmision["usuario"] = user.NOMBRES + " " + user.APELLIDOS;

            dtx.Tables["facturaNombre"].Rows.Add(drAdmision);

            Formulario.frmReportes x = new Formulario.frmReportes(1, "facturaNombre", dtx);
            x.ShowDialog();
        }

        private void txt_cedula_Enter(object sender, EventArgs e)
        {

        }

        private void txt_cedulaAcomp_Enter(object sender, EventArgs e)
        {

        }
        public void validaPasaporte()
        {
            PACIENTES consulta = NegPacientes.pacientePorIdentificacion(txt_cedula.Text.Trim().ToString());
            DialogResult resp = new DialogResult();
            if (consulta != null)
            {
                resp = MessageBox.Show("Esta identificación ya existe.\nDesea cargar el paciente con este ID?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.No)
                {
                    txt_cedula.Focus();
                }
                else
                {
                    //CargarPaciente(consulta.PAC_HISTORIA_CLINICA.ToString());
                    btnPr.Enabled = false;
                    pacienteNuevo = false;
                    txt_historiaclinica.Tag = true;
                    txt_historiaclinica.Text = consulta.PAC_HISTORIA_CLINICA.ToString().Trim();

                    CargarPaciente(txt_historiaclinica.Text.Trim(), 0);
                    deshabilitarCamposAtencion();
                    deshabilitarCamposDireccion();
                    deshabilitarCamposPaciente();
                    if (txt_historiaclinica.Text.Trim() == "")
                    {
                        consultas = false;
                        btnGuardar.Enabled = false;
                        btnHabitaciones.Enabled = false;
                        btnFormularios.Enabled = false;
                        btnActualizar.Enabled = false;
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                        btnHabitaciones.Enabled = false;
                        btnFormularios.Enabled = false;

                        if (fallecido == true)
                            btnActualizar.Enabled = false;
                        else
                        {
                            btnActualizar.Enabled = true;
                            toolStripNuevo.Enabled = true;
                            btnEliminar.Visible = false;
                            tabulador.Tabs["paciente"].Enabled = true;
                            tabulador.Tabs["atencion"].Enabled = true;
                            tabulador.Tabs["gridatenciones"].Enabled = true;
                        }

                        consultas = true;
                    }
                }
            }
        }

        private void form001EGRESOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PACIENTES pac = NegPacientes.RecuperarPacienteID(txt_historiaclinica.Text);
            PACIENTES_DATOS_ADICIONALES2 dat = NegPacienteDatosAdicionales.pacientesdatos2(pac.PAC_CODIGO);
            List<ATENCIONES> adm = NegAtenciones.listaAtencionesPaciente(pac.PAC_CODIGO);
            string formato = "yyyy-MM-dd";
            DSAdmision ds = new DSAdmision();
            DataRow dr;
            int i = 1;
            foreach (var item in adm)
            {
                MEDICOS med = NegMedicos.RecuperaMedicoId(Convert.ToInt16(item.MEDICOSReference.EntityKey.EntityKeyValues[0].Value));
                USUARIOS_FIRMA usrf = NegUsuarios.recuperaFirma(Convert.ToInt64(med.USUARIOSReference.EntityKey.EntityKeyValues[0].Value));
                DateTime fingreso = (DateTime)item.ATE_FECHA_INGRESO;
                DateTime falta = (DateTime)item.ATE_FECHA_ALTA;
                dr = ds.Tables["Egreso"].NewRow();
                dr["path"] = NegUtilitarios.RutaLogo("General");
                dr["admision"] = fingreso.ToString(formato);
                //dr["ate_codigo"] = item.ATE_CODIGO;
                if (usrf != null)
                    dr["firma"] = usrf.URL;
                else
                    dr["firma"] = "";

                dr["egreso"] = falta.ToString(formato);
                DateTime fini = (DateTime)item.ATE_FECHA_INGRESO;
                DateTime ffin = DateTime.Now;
                if (item.ATE_FECHA_ALTA != null)
                    ffin = (DateTime)item.ATE_FECHA_ALTA;
                TimeSpan tsp = ffin - fini;
                dr["nd"] = tsp.Days;

                if (item.ATE_FECHA_ALTA != null)
                    dr["alta"] = "X";
                else
                    dr["alta"] = "";
                if (i == adm.Count)
                {
                    DateTime def = (DateTime)dat.FECHA_FALLECIDO;
                    TimeSpan fechd = def - fini;
                    if (Convert.ToBoolean(dat.FALLECIDO))
                    {
                        if (fechd.TotalHours > 48)
                        {
                            dr["m2"] = "X";
                            dr["m1"] = " ";
                        }
                        else
                        {
                            dr["m1"] = "X";
                            dr["m2"] = " ";
                        }
                    }
                    else
                    {
                        dr["m1"] = " ";
                        dr["m2"] = " ";
                    }
                }
                if (Convert.ToInt32(item.TIPO_TRATAMIENTOReference.EntityKey.EntityKeyValues[0].Value) == 1)
                    dr["cl"] = "X";

                if (Convert.ToInt32(item.TIPO_TRATAMIENTOReference.EntityKey.EntityKeyValues[0].Value) == 2)
                    dr["qr"] = "X";
                dr["proced"] = NegQuirofano.registroQuirofano(item.ATE_CODIGO);
                List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> emerI = NegHcEmergencia.cieDiagnostico(item.ATE_CODIGO, "I");
                List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> emerA = NegHcEmergencia.cieDiagnostico(item.ATE_CODIGO, "A");
                int cnt = 1;
                foreach (var emdg in emerI)
                {
                    if (cnt == 1)
                    {
                        dr["id1"] = emdg.ED_DESCRIPCION;
                        dr["ic1"] = emdg.CIE_CODIGO;
                        if (emdg.ED_ESTADO.Value)
                            dr["if1"] = "X";
                        else
                            dr["if1"] = " ";
                    }
                    if (cnt == 2)
                    {
                        dr["id2"] = emdg.ED_DESCRIPCION;
                        dr["ic2"] = emdg.CIE_CODIGO;
                        if (emdg.ED_ESTADO.Value)
                            dr["if2"] = "X";
                        else
                            dr["if2"] = " ";
                    }
                    if (cnt == 3)
                    {
                        dr["id3"] = emdg.ED_DESCRIPCION;
                        dr["ic3"] = emdg.CIE_CODIGO;
                        if (emdg.ED_ESTADO.Value)
                            dr["if3"] = "X";
                        else
                            dr["if3"] = " ";
                    }
                    cnt++;
                }
                cnt = 1;
                foreach (var emdg in emerA)
                {
                    if (cnt == 1)
                    {
                        dr["ad1"] = emdg.ED_DESCRIPCION;
                        dr["ac1"] = emdg.CIE_CODIGO;
                        if (emdg.ED_ESTADO.Value)
                            dr["af1"] = "X";
                        else
                            dr["af1"] = " ";
                    }
                    if (cnt == 2)
                    {
                        dr["ad2"] = emdg.ED_DESCRIPCION;
                        dr["ac2"] = emdg.CIE_CODIGO;
                        if (emdg.ED_ESTADO.Value)
                            dr["af2"] = "X";
                        else
                            dr["af2"] = " ";
                    }
                    if (cnt == 3)
                    {
                        dr["ad3"] = emdg.ED_DESCRIPCION;
                        dr["ac3"] = emdg.CIE_CODIGO;
                        if (emdg.ED_ESTADO.Value)
                            dr["af3"] = "X";
                        else
                            dr["af3"] = " ";
                    }
                    cnt++;
                }


                //foreach (var emdg in emerI)
                //{
                //    dr1 = ds.Tables["DiagnosticoI"].NewRow();
                //    dr1["ate_codigo"] = item.ATE_CODIGO;
                //    dr1["da"] = emdg.ED_DESCRIPCION;
                //    dr1["ca"] = emdg.CIE_CODIGO;
                //    if (emdg.ED_ESTADO.Value)
                //        dr1["ad"] = "X";
                //    else
                //        dr1["ad"] = " ";
                //    ds.Tables["DiagnosticoI"].Rows.Add(dr1);
                //}

                ds.Tables["Egreso"].Rows.Add(dr);
            }

            Formulario.frmReportes x = new Formulario.frmReportes(1, "Egreso", ds);
            x.ShowDialog();
        }

        private void frm002ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ATENCIONES_SUBSECUENTES subsecuente = new ATENCIONES_SUBSECUENTES();
            Form002MSP consultaExterna = new Form002MSP();
            DtoForm002 datos = new DtoForm002();
            subsecuente = NegAtenciones.RecuperaAtencionSub(Convert.ToInt64(ultimaAtencion.ATE_CODIGO));
            if (subsecuente != null)
            {
                consultaExterna = NegConsultaExterna.PacienteExisteCxE(Convert.ToString(subsecuente.ate_codigo_principal));
            }
            else
            {
                consultaExterna = NegConsultaExterna.PacienteExisteCxE(Convert.ToString(ultimaAtencion.ATE_CODIGO));
            }
            SUCURSALES sucursal = new SUCURSALES();
            string logo = "";
            string empresa = "";
            if (mushuñan)
            {
                Int32 ingreso = NegTipoIngreso.RecuperarporAtencion(ultimaAtencion.ATE_CODIGO);
                switch (ingreso)
                {
                    case 10:
                        logo = NegUtilitarios.RutaLogo("Mushuñan");
                        empresa = "SANTA CATALINA DE SENA";
                        break;
                    case 12:
                        logo = NegUtilitarios.RutaLogo("BrigadaMedica");
                        empresa = Sesion.nomEmpresa;
                        break;
                    default:
                        logo = NegUtilitarios.RutaLogo("General");
                        empresa = Sesion.nomEmpresa;
                        break;
                }
            }
            else
            {
                logo = NegUtilitarios.RutaLogo("General");
                empresa = Sesion.nomEmpresa;
            }

            if (rbn_h.Checked == true)
            {
                datos.sexoPaciente = "X";
                datos.cancer = " ";
            }
            else
            {
                datos.cancer = "X";
                datos.sexoPaciente = " ";
            }
            if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 1)
                datos.diagnosticco1 = "X";
            else if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 2)
                datos.diagnosticco2 = "X";
            else if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 3)
                datos.diagnosticco3 = "X";
            else if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 4)
                datos.diagnosticco4 = "X";
            else if (Convert.ToInt32(cmb_estadocivil.SelectedValue) == 5)
                datos.diagnosticco4cie = "X";
            PACIENTES pacien = new PACIENTES();
            pacien = NegPacientes.recuperarPacientePorAtencion(ultimaAtencion.ATE_CODIGO);
            if (NegParametros.ParametroFormularios())
                datos.historiaClinica = pacien.PAC_IDENTIFICACION;
            else
                datos.historiaClinica = txt_historiaclinica.Text;

            NegCertificadoMedico neg = new NegCertificadoMedico();
            Int32 edad = DateTime.Now.Year - Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO).Year;
            His.Formulario.HCU_form002MSP Ds = new His.Formulario.HCU_form002MSP();
            Ds.Tables[0].Rows.Add
                (new object[]
                {
                    pacienteActual.PAC_NOMBRE1.Trim(),
                    pacienteActual.PAC_APELLIDO_PATERNO.Trim(),
                    datos.sexoPaciente.ToString(),
                    edad,
                    datos.historiaClinica.ToString().Trim(),
                    pacienteActual.PAC_NOMBRE2.Trim(),
                    pacienteActual.PAC_APELLIDO_MATERNO.Trim(),
                    txt_direccion.Text.Trim(),
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    txt_cedula.Text.Trim(),
                    datos.cancer.ToString().Trim(),
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    pacienteActual.PAC_FECHA_NACIMIENTO.ToString(),
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    txt_telefono2.Text.Trim(),
                    datos.diagnosticco1,
                    datos.diagnosticco2,
                    datos.diagnosticco3,
                    datos.diagnosticco4,
                    datos.diagnosticco4cie,
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    Sesion.codMedico.ToString(),
                    logo,
                    empresa,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0
                });

            His.Formulario.frmReportes x = new His.Formulario.frmReportes(1, "EncConsultaExterna", Ds);
            x.Show();
        }

        private void txt_parentRef_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void generealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // para crear el archivo

            string HC = txt_historiaclinica.Text.Trim().PadLeft(7, '0');
            string ATE_NUM = txt_numeroatencion.Text.Trim().PadLeft(7, '0');
            string xgenero = "MASCULINO";
            if (!rbn_h.Checked)
                xgenero = "FEMENINO";

            //var now = DateTime.Now; // Se comenta calcula la edad de forma incorrecta // Mario Valencia // 2024-01-11
            //var birthday = dtp_fecnac.Value;
            //var yearsOld = now - birthday;

            //int years = (int)(yearsOld.TotalDays / 365.25);
            //int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

            //TimeSpan age = now - birthday;
            //DateTime totalTime = new DateTime(age.Ticks);

            if (dtp_fecnac.Value.Date == DateTime.Now.Date)// calculo de edad // Mario Valencia // 2024-01-11
                return;
            DateTime actual = DateTime.Now.Date;
            if (ultimaAtencion != null)
                actual = Convert.ToDateTime(ultimaAtencion.ATE_FECHA);
            DateTime nacido = dtp_fecnac.Value;

            int edadAnos = actual.Year - nacido.Year;
            if (actual.Month < nacido.Month || (actual.Month == nacido.Month && actual.Day < nacido.Day))
                edadAnos--;

            int edadMeses = actual.Month - nacido.Month;
            if (actual.Day < nacido.Day)
                edadMeses--;
            if (edadMeses < 0)
                edadMeses += 12;

            int diaActual = actual.Day;
            int diaCumple = nacido.Day;
            int diasDiferencia = diaActual - diaCumple;
            if (diasDiferencia < 0)
            {
                //edadMeses -= 1;
                diasDiferencia += DateTime.DaysInMonth(actual.Year, actual.Month);
            }

            #region BRAZZALETES
            DSBarCode BC = new DSBarCode();
            DataRow Brazzalete;

            Brazzalete = BC.Tables["BarCode"].NewRow();
            Brazzalete["Logo"] = NegUtilitarios.RutaLogo("Brazzalete"); //aqui va el logo parametrizado desde la base
            Brazzalete["Barras"] = txt_cedula.Text.Trim();
            Brazzalete["Empresa"] = Sesion.nomEmpresa;
            Brazzalete["Paciente"] = "PAC.: " + txt_apellido1.Text + ' ' + txt_apellido2.Text + ' ' + txt_nombre1.Text + ' ' + txt_nombre2.Text;
            Brazzalete["Edad"] = "EDAD: " + edadAnos + " AÑOS, " + edadMeses + " MESES, " + diasDiferencia + " DIAS";
            Brazzalete["Sexo"] = "SEXO: " + xgenero + " - HAB.: " + txt_habitacion.Text.ToString().Trim();
            Brazzalete["HC"] = " - HC : " + txt_historiaclinica.Text.Trim();
            Brazzalete["Identificacion"] = "CEDULA:" + txt_cedula.Text.Trim();
            Brazzalete["F_Ingreso"] = "INGRESO: " + dateTimeFecIngreso.Value.ToString();
            if (cmb_tipoingreso.SelectedValue.ToString() == "2")
                Brazzalete["Triaje"] = "HO";
            else if (cmb_tipoingreso.SelectedValue.ToString() == "3")
                Brazzalete["Triaje"] = "HD";
            Brazzalete["Atencion"] = "INGRESO: " + dateTimeFecIngreso.Value.ToString();
            Brazzalete["Medico"] = "MÉD.: " + txtNombreMedico.Text.Trim();
            Brazzalete["Referido"] = "REFERIDO: " + cmb_tiporeferido.Text.ToString();
            string convenios = "";
            foreach (DataGridViewRow row in gridAseguradoras.Rows)
            {
                convenios += (row.Cells["nomCategoria"].Value.ToString()) + ". ";
            }
            Brazzalete["Convenio"] = " - CONVENIO: " + convenios;


            BC.Tables["BarCode"].Rows.Add(Brazzalete);
            frmReportes reporte = new frmReportes(1, "Brazzalete", BC);
            reporte.Show();

            #endregion

            string rutaCompleta = Application.StartupPath + "\\Reportes\\Admision\\Brazaletes\\" + HC + ATE_NUM + ' ' + txt_apellido1.Text + ' ' + txt_apellido2.Text + ' ' + txt_nombre1.Text + ' ' + txt_nombre2.Text + ".txt";
            if (File.Exists(rutaCompleta))
                File.Delete(rutaCompleta);

            using (StreamWriter sw = File.AppendText(rutaCompleta))         //se crea el archivo
            {
                sw.WriteLine(txt_apellido1.Text + ' ' + txt_apellido2.Text + ' ' + txt_nombre1.Text + ' ' + txt_nombre2.Text);
                sw.WriteLine("EDAD:" + edadAnos + " AÑOS, " + edadMeses + " MESES, " + diasDiferencia + " DIAS");
                sw.WriteLine("GENERO: " + xgenero + "  HAB.:" + txt_habitacion.Text.ToString().Trim());
                sw.WriteLine("HC : " + txt_historiaclinica.Text.Trim() + " CEDULA:" + txt_cedula.Text.Trim());
                sw.WriteLine("INGRESO : " + dateTimeFecIngreso.Value.ToString());
                sw.WriteLine("MED.TRATANTE: Dr." + txtNombreMedico.Text.ToString());
                sw.WriteLine("REFERIDO : " + cmb_tiporeferido.Text.ToString());
                sw.WriteLine("ATENCION NUMERO: " + txt_numeroatencion.Text.Trim());
                string convenio = "";
                foreach (DataGridViewRow row in gridAseguradoras.Rows)
                {
                    convenio += (row.Cells["nomCategoria"].Value.ToString()) + ". ";
                }
                sw.WriteLine("CONVENIO: " + convenio);
                if (chkDiscapacidad.Checked)
                {
                    sw.WriteLine(txtPorcentageDA.Text.Trim() + "% [Discapacidad:" + cmbTiposDiscapacidadesDA.Text.Substring(5) + "]");
                }

                sw.Close();
            }
        }

        private void especialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // para crear el archivo

            string HC = txt_historiaclinica.Text.Trim().PadLeft(7, '0');
            string ATE_NUM = txt_numeroatencion.Text.Trim().PadLeft(7, '0');
            string xgenero = "MASCULINO";
            if (!rbn_h.Checked)
                xgenero = "FEMENINO";

            //var now = DateTime.Now; // Se comenta calcula la edad de forma incorrecta // Mario Valencia // 2024-01-11
            //var birthday = dtp_fecnac.Value;
            //var yearsOld = now - birthday;

            //int years = (int)(yearsOld.TotalDays / 365.25);
            //int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

            //TimeSpan age = now - birthday;
            //DateTime totalTime = new DateTime(age.Ticks);

            if (dtp_fecnac.Value.Date == DateTime.Now.Date)// calculo de edad // Mario Valencia // 2024-01-11
                return;
            DateTime actual = DateTime.Now.Date;
            if (ultimaAtencion != null)
                actual = Convert.ToDateTime(ultimaAtencion.ATE_FECHA);
            DateTime nacido = dtp_fecnac.Value;

            int edadAnos = actual.Year - nacido.Year;
            if (actual.Month < nacido.Month || (actual.Month == nacido.Month && actual.Day < nacido.Day))
                edadAnos--;

            int edadMeses = actual.Month - nacido.Month;
            if (actual.Day < nacido.Day)
                edadMeses--;
            if (edadMeses < 0)
                edadMeses += 12;

            int diaActual = actual.Day;
            int diaCumple = nacido.Day;
            int diasDiferencia = diaActual - diaCumple;
            if (diasDiferencia < 0)
            {
                //edadMeses -= 1;
                diasDiferencia += DateTime.DaysInMonth(actual.Year, actual.Month);
            }

            #region BRAZZALETES
            DSBarCode BC = new DSBarCode();
            DataRow Brazzalete;

            Brazzalete = BC.Tables["BarCode"].NewRow();
            Brazzalete["Logo"] = NegUtilitarios.RutaLogo("Brazzalete"); //aqui va el logo parametrizado desde la base
            Brazzalete["Barras"] = txt_cedula.Text.Trim();
            Brazzalete["Empresa"] = Sesion.nomEmpresa;
            Brazzalete["Paciente"] = "PAC.: " + txt_apellido1.Text + ' ' + txt_apellido2.Text + ' ' + txt_nombre1.Text + ' ' + txt_nombre2.Text;
            Brazzalete["Edad"] = "EDAD: " + edadAnos + " AÑOS, " + edadMeses + " MESES, " + diasDiferencia + " DIAS";
            Brazzalete["Sexo"] = "SEXO: " + xgenero + " - HAB.: " + txt_habitacion.Text.ToString().Trim();
            Brazzalete["HC"] = " - HC : " + txt_historiaclinica.Text.Trim();
            Brazzalete["Identificacion"] = "CEDULA:" + txt_cedula.Text.Trim();
            Brazzalete["F_Ingreso"] = "INGRESO: " + dateTimeFecIngreso.Value.ToString();
            if (cmb_tipoingreso.SelectedValue.ToString() == "2")
                Brazzalete["Triaje"] = "HO";
            else if (cmb_tipoingreso.SelectedValue.ToString() == "3")
                Brazzalete["Triaje"] = "HD";
            Brazzalete["Atencion"] = "INGRESO: " + dateTimeFecIngreso.Value.ToString();
            Brazzalete["Medico"] = "MÉD.: " + txtNombreMedico.Text.Trim();
            Brazzalete["Referido"] = "REFERIDO: " + cmb_tiporeferido.Text.ToString();
            string convenios = "";
            foreach (DataGridViewRow row in gridAseguradoras.Rows)
            {
                convenios += (row.Cells["nomCategoria"].Value.ToString()) + ". ";
            }
            Brazzalete["Convenio"] = " - CONVENIO: " + convenios;


            BC.Tables["BarCode"].Rows.Add(Brazzalete);
            His.Formulario.frmReportes x = new His.Formulario.frmReportes(1, "BrazzaleteE", BC);
            x.Show();

            #endregion

            string rutaCompleta = Application.StartupPath + "\\Reportes\\Admision\\Brazaletes\\" + HC + ATE_NUM + ' ' + txt_apellido1.Text + ' ' + txt_apellido2.Text + ' ' + txt_nombre1.Text + ' ' + txt_nombre2.Text + ".txt";
            if (File.Exists(rutaCompleta))
                File.Delete(rutaCompleta);

            using (StreamWriter sw = File.AppendText(rutaCompleta))         //se crea el archivo
            {
                sw.WriteLine(txt_apellido1.Text + ' ' + txt_apellido2.Text + ' ' + txt_nombre1.Text + ' ' + txt_nombre2.Text);
                sw.WriteLine("EDAD:" + edadAnos + " AÑOS, " + edadMeses + " MESES, " + diasDiferencia + " DIAS");
                sw.WriteLine("GENERO: " + xgenero + "  HAB.:" + txt_habitacion.Text.ToString().Trim());
                sw.WriteLine("HC : " + txt_historiaclinica.Text.Trim() + " CEDULA:" + txt_cedula.Text.Trim());
                sw.WriteLine("INGRESO : " + dateTimeFecIngreso.Value.ToString());
                sw.WriteLine("MED.TRATANTE: Dr." + txtNombreMedico.Text.ToString());
                sw.WriteLine("REFERIDO : " + cmb_tiporeferido.Text.ToString());
                sw.WriteLine("ATENCION NUMERO: " + txt_numeroatencion.Text.Trim());
                string convenio = "";
                foreach (DataGridViewRow row in gridAseguradoras.Rows)
                {
                    convenio += (row.Cells["nomCategoria"].Value.ToString()) + ". ";
                }
                sw.WriteLine("CONVENIO: " + convenio);
                if (chkDiscapacidad.Checked)
                {
                    sw.WriteLine(txtPorcentageDA.Text.Trim() + "% [Discapacidad:" + cmbTiposDiscapacidadesDA.Text.Substring(5) + "]");
                }

                sw.Close();
            }
        }
    }
}
