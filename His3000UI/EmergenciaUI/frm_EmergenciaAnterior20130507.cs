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
using His.Formulario;
using His.Negocio;
using Recursos;
using GeneralApp.ControlesWinForms;
using His.Parametros;
using His.General;
using His.Entidades.Clases;
using His.DatosReportes;
using System.Windows.Forms.VisualStyles;

namespace His.Emergencia
{
    public partial class frm_Emergencia : Form
    {
        #region VARIABLES GLOBALES
        private bool mostrarInfPaciente = true;
        private int codAtencion;
        ATENCIONES atencionActual = new ATENCIONES();
        PACIENTES pacienteActual = new PACIENTES();
        HC_EMERGENCIA_FORM emergenciaActual = new HC_EMERGENCIA_FORM();
        private int codigoEmergencia;
        private string accion;

        private List<HC_CATALOGOS> listaLugarE = new List<HC_CATALOGOS>();
        private List<HC_CATALOGOS> listaAccidentes = new List<HC_CATALOGOS>();
        private List<HC_CATALOGOS> listaDestino = new List<HC_CATALOGOS>();
        List<string> listaOcular = new List<string>();
        List<string> listaVerbal = new List<string>();
        List<string> listaMotora = new List<string>();
        private List<DtoCatalogos> ant1;
        private List<DtoCatalogos> ant2;
        private List<DtoCatalogos> ant3;
        private List<DtoCatalogos> ant4;

        public bool pacienteNuevo = false;
        PACIENTES_DATOS_ADICIONALES datosPacienteActual = null;

        DataTable dtMedicamentos = new DataTable();
        DataTable dtMedicoAtencion = new DataTable();

        #endregion


        public frm_Emergencia()
        {
            mostrarInfPaciente = true;
            InitializeComponent();
            //this.codAtencion = 199182;
            //cargarAtencion(codAtencion);

            recuperarUltimaEmergencia();
        }

        //public frm_Emergencia(int codAtencion)
        //{
        //    mostrarInfPaciente = true;
        //    InitializeComponent();
        //    this.codAtencion = 199182;
        //    cargarAtencion(codAtencion);
        //}


        #region METODOS
        /// <summary>
        /// METODO QUE PERMITE RECUPERAR LA ULTIMA EMERGENCIA INGRESADA AL SISTEMA
        /// </summary>
        private void recuperarUltimaEmergencia()
        {
            emergenciaActual = NegHcEmergenciaForm.RecuperarUltimaEmergencia();
            //emerNuevo = true;
            if (emergenciaActual != null)
            {                
                cargarAtencion(Convert.ToInt32(emergenciaActual.ATENCIONESReference.EntityKey.EntityKeyValues[0].Value));
                actualizarPaciente();
                txtRegistro.Text = emergenciaActual.EMER_CODIGO.ToString();
                txt_Atencion.Text = emergenciaActual.ATENCIONESReference.EntityKey.EntityKeyValues[0].Value.ToString();
            }
            
        }

        private void HabilitarBotones(Boolean nuevo, Boolean guardar, Boolean imprimir, Boolean actualizar, Boolean salir, Boolean cancelar, Boolean cerrar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnImprimir.Enabled = imprimir;
            btnModificar.Enabled = actualizar;
            btnSalir.Enabled = salir;
            btnCancelar.Enabled = cancelar;
            btnCerrar.Enabled = cerrar;
        }


        private void LimpiarCamposEmergencia()
        {
            chk_Trauma.Checked = false;
            dtp_FechaHoraEvento.Value = DateTime.Now;
            cmb_LugarEvento.SelectedItem = 1;
            txt_DireccionEvento.Text = "";
            chk_CustodiaPolicial.Checked = false;
            cmb_Accidentes.SelectedItem = 1;
            txt_ObservacionAccidente.Text = "";
            chb_AlientoEtilico.Checked = false;
            chb_ValorAlcocheck.Checked = false;
            chb_ValorAlcocheck.Checked = false;
            chk_CausaC.Checked = false;
            chk_CausaGOb.Checked = false;
            chk_CausaQuir.Checked = false;
            txt_OtroMotivo.Text = "";
            chk_CausaQuir.Checked = false;
            chk_ViaAL.Checked = false;
            chk_ViaAO.Checked = false;
            chk_CondE.Checked = false;
            chk_CondI.Checked = false;
            txt_EnfermedadActual.Text = "";
            txt_PresionA1.Text = "0";
            txt_PresionA2.Text = "0";
            txt_FCardiaca.Text = "0";
            txt_FResp.Text = "0";
            txt_TBucal.Text = "0";
            txt_TAxilar.Text = "0";
            txt_TAxilar.Enabled = true;
            txt_TBucal.Enabled = true;
            txt_PesoKG.Text = "0";
            txt_Talla.Text = "0";
            cmb_Ocular.SelectedIndex = 0;
            cmb_Verbal.SelectedIndex = 0;
            cmb_Motora.SelectedIndex = 0;
            txt_TotalG.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.SelectedValue) + Convert.ToInt32(cmb_Motora.SelectedValue) + Convert.ToInt32(cmb_Verbal.SelectedValue));
            //chk_DiamPD.Checked = false;
            //chk_ReacPD.Checked = false;
            txt_DiamPDV.Text = "0";
            cmb_ReacPDValor.SelectedIndex = 0;
            //chk_DiamPI.Checked = false;
            //chk_ReacPI.Checked = false;
            txt_DiamPIV.Text = "0";
            cmb_ReacPIValor.SelectedIndex = 0;
            txt_Glicemia.Text = "0";
            //cmb_Destino.SelectedIndex = 0;
            txt_servicioReferencia.Text = "";
            txt_Establecimiento.Text = "";
            chk_EgresaVivo.Checked = false;
            txt_DiasInc.Text = "0";
            chk_Estable.Checked = false;
            chk_MuertoE.Checked = false;
            txt_CausaMuerte.Text = "";


            //while (dtg_DiagnosticosI.RowCount > 1)
            //{

            //    dtg_DiagnosticosI.Rows.Remove(dtg_DiagnosticosI.CurrentRow);

            //}

            dtg_DiagnosticosI.Rows.Clear();
            dtg_DiagnosticosAlta.Rows.Clear();
            dtg_antec_personales.Rows.Clear();
            dtg_ExamenFisico.Rows.Clear();
            dgv_LocalizacionL.Rows.Clear();
            dgv_ExamenesS.Rows.Clear();
            dgv_Indicaciones.Rows.Clear();
            dgv_Medicamentos.Rows.Clear();
            //dgv_Medicamentos.Rows.Add();
            dtp_fechaAltaEmerencia.Value = DateTime.Now;
            txt_horaAltaEmerencia.Text = "";
            txt_profesionalEmergencia.Text = "";
            txt_CodMSPE.Text = "";
            //txtRegistro.Text = "";
                //cargarDiagnosticos();
        }

        private void CargarDatos()
        {
            LimpiarCamposEmergencia();
            listaLugarE = NegCatalogos.RecuperarHcCatalogosPorTipo(His.Parametros.EmergenciaForm.CodigoLugarEvento).OrderBy(e=>e.HCC_NOMBRE).ToList();
            cmb_LugarEvento.DataSource = listaLugarE;
            cmb_LugarEvento.ValueMember = "HCC_CODIGO";
            cmb_LugarEvento.DisplayMember = "HCC_NOMBRE";
            cmb_LugarEvento.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_LugarEvento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            listaAccidentes = NegCatalogos.RecuperarHcCatalogosPorTipo(His.Parametros.EmergenciaForm.CodigoAccidentes).OrderBy(a => a.HCC_NOMBRE).ToList(); ;
            cmb_Accidentes.DataSource = listaAccidentes;
            cmb_Accidentes.ValueMember = "HCC_CODIGO";
            cmb_Accidentes.DisplayMember = "HCC_NOMBRE";
            cmb_Accidentes.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_Accidentes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            listaDestino = NegCatalogos.RecuperarHcCatalogosPorTipo(His.Parametros.EmergenciaForm.CodigoAlta);
            cmb_Destino.DataSource = listaDestino;
            cmb_Destino.ValueMember = "HCC_CODIGO";
            cmb_Destino.DisplayMember = "HCC_NOMBRE";
            cmb_Destino.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_Destino.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            listaOcular.Add("1");
            listaOcular.Add("2");
            listaOcular.Add("3");
            listaOcular.Add("4");
            cmb_Ocular.DataSource = listaOcular;
            //cmb_Ocular.ValueMember = "CODIDO";
            //cmb_Ocular.DisplayMember = "VALOR";
            //cmb_Ocular.AutoCompleteSource = AutoCompleteSource.ListItems;
            //cmb_Ocular.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            listaVerbal.Add("1");
            listaVerbal.Add("2");
            listaVerbal.Add("3");
            listaVerbal.Add("4");
            listaVerbal.Add("5");
            cmb_Verbal.DataSource = listaVerbal;
            //cmb_Verbal.ValueMember = "CODIDO";
            //cmb_Verbal.DisplayMember = "VALOR";
            //cmb_Verbal.AutoCompleteSource = AutoCompleteSource.ListItems;
            //cmb_Verbal.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            listaMotora.Add("1");
            listaMotora.Add("2");
            listaMotora.Add("3");
            listaMotora.Add("4");
            listaMotora.Add("5");
            listaMotora.Add("6");
            cmb_Motora.DataSource = listaMotora;
            //cmb_Motora.ValueMember = "CODIDO";
            //cmb_Motora.DisplayMember = "VALOR";
            //cmb_Motora.AutoCompleteSource = AutoCompleteSource.ListItems;
            //cmb_Motora.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cmb_ReacPDValor.SelectedIndex = 0;
            cmb_ReacPIValor.SelectedIndex = 0;

            cargarAntePersFamiliares();
            cargarExamenFisico();
            cargarLesiones();
            cargarSolicitusExamnes();
            CargarEmergenciaObstetrica();

            txt_HoraI.Text = atencionActual.ATE_FECHA_INGRESO.ToString().Substring(11, 8);

        }


        private void actualizarPaciente()
        {
            try
            {
                txt_NombreH1.Text = pacienteActual.PAC_NOMBRE1;
                txt_NombreH2.Text = pacienteActual.PAC_NOMBRE2;
                txt_ApellidoH1.Text = pacienteActual.PAC_APELLIDO_PATERNO;
                txt_ApellidoH2.Text = pacienteActual.PAC_APELLIDO_MATERNO;

                txt_historiaclinica.Text = pacienteActual.PAC_HISTORIA_CLINICA;
                //txt_nombre1.Text = pacienteActual.PAC_NOMBRE1;
                //txt_nombre2.Text = pacienteActual.PAC_NOMBRE2;
                //txt_apellido1.Text = pacienteActual.PAC_APELLIDO_PATERNO;
                //txt_apellido2.Text = pacienteActual.PAC_APELLIDO_MATERNO;
                txt_cedulaPacientes.Text = pacienteActual.PAC_IDENTIFICACION;
                if (pacienteActual.PAC_GENERO == "M")
                {
                    txtGenero.Text = "Masculino";
                    //dateTimeFum.Enabled = false;
                    //dateTimeFum.Value = Convert.ToDateTime("1900/01/01 00:00:00");
                }
                else
                {
                    txtGenero.Text = "Femenino";
                    //dateTimeFum.Enabled = true;
                }
                dtpFecNacimiento.Text = pacienteActual.PAC_FECHA_NACIMIENTO.Value.ToString();
                dtFecIngreso.Text = atencionActual.ATE_FECHA_INGRESO.ToString();
                txtEdad.Text = Funciones.CalcularEdad(Convert.ToDateTime(dtpFecNacimiento.Text)).ToString() + " años";
                CargarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void CargarDatosAdicionalesPaciente(int keyPaciente)
        {
            try
            {
                datosPacienteActual = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(keyPaciente);
                if (datosPacienteActual != null)
                {
                    //cb_aseguradoras.SelectedItem = aseguradoras.FirstOrDefault(aseg => aseg.EntityKey == datosPacienteActual.PACIENTES_ASEGURADORASReference.EntityKey);
                    ETNIA etniaPaciente = NegEtnias.RecuperarEtniaID(Convert.ToInt16(pacienteActual.ETNIAReference.EntityKey.EntityKeyValues[0].Value));
                    cb_Etnia.Text = etniaPaciente.E_NOMBRE;
                    txt_direccionP.Text = datosPacienteActual.DAP_DIRECCION_DOMICILIO;
                    txt_Telef1.Text = datosPacienteActual.DAP_TELEFONO1;
                    txt_Telef2.Text = datosPacienteActual.DAP_TELEFONO2;
                    ESTADO_CIVIL estadoCivilPaciente = NegEstadoCivil.RecuperarEstadoCivilID(Convert.ToInt16(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value));
                    cb_estadoCivilP.Text = estadoCivilPaciente.ESC_NOMBRE;
                    ATENCIONES atencionPaciente;
                    if (txt_Atencion.Text.Trim() == string.Empty)
                        atencionPaciente = NegAtenciones.RecuperarUltimaAtencionExt(keyPaciente);
                    else
                    {
                        atencionPaciente = NegAtenciones.RecuperarAtencionIDEmerg(Convert.ToInt32(txt_Atencion.Text.Trim()));
                        dtpFechaIngreso.Value = Convert.ToDateTime(atencionPaciente.ATE_FECHA_INGRESO.Value);
                        if (atencionPaciente.ATE_FECHA_ALTA != null)
                            dtpFechaAlta.Value = Convert.ToDateTime(atencionPaciente.ATE_FECHA_ALTA.Value);
                        else
                            dtpFechaAlta.Value = Convert.ToDateTime(atencionPaciente.ATE_FECHA_INGRESO.Value);
                    }

                    //ATENCION_FORMAS_LLEGADA formaLlegada = NegAtencionesFormasLlegada.atencionesFormasLlegadaPorAtencion(atencionPaciente.ATE_CODIGO);
                    txtFormaLlegada.Text = atencionPaciente.ATENCION_FORMAS_LLEGADA.AFL_DESCRIPCION;
                    txt_Medico.Text = atencionPaciente.MEDICOS.MED_APELLIDO_PATERNO + " " + atencionPaciente.MEDICOS.MED_APELLIDO_MATERNO + " " + atencionPaciente.MEDICOS.MED_NOMBRE1;
                    //ATENCIONES atencion = NegAtenciones.RecuperarUltimaAtencion((int)pacienteActual.EntityKey.EntityKeyValues[0].Value);
                    //ATENCION_DETALLE_CATEGORIAS atencionCategoria = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(atencion.ATE_CODIGO).OrderByDescending(a=>a.ADA_FECHA_INICIO).FirstOrDefault();
                    //CATEGORIAS_CONVENIOS categoria = NegCategorias.RecuperaCategoriaID((Int16)atencionCategoria.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value);
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }



        private void cargarAtencion(int codAtencion)
        {
            atencionActual = NegAtenciones.RecuperarAtencionID(codAtencion);
            if (atencionActual != null)
            {
                dtpFechaIngreso.Value = atencionActual.ATE_FECHA_INGRESO.Value;
                //if (atencionActual.ATE_FECHA_ALTA != null)
                //    dtpFechaAlta.Value = atencionActual.ATE_FECHA_ALTA.Value;
                //else
                //    dtpFechaAlta.Value = DateTime.Now;
                //if (dtpFechaAlta.Value != null)
                pacienteActual = NegPacientes.RecuperarPacienteID(atencionActual.PACIENTES.PAC_CODIGO);
                emergenciaActual = NegHcEmergenciaForm.RecuperarHcEmergenciasFAten(codAtencion);
                if(emergenciaActual != null)
                {
                    txtRegistro.Text = emergenciaActual.EMER_CODIGO.ToString();
                    accion = "UPDATE";
                    CargarDatos();
                    CargarCamposEmergencia(emergenciaActual);
                    cargarDetalles();
                    cargarDiagnosticos();
                    cargarTratamientosMedicamentos();
                    CargarEmergenciaObstetrica();
                    if (atencionActual.ATE_ESTADO == true)
                    {
                        //if (emergenciaActual.EMER_ESTADO != 1)
                        //    HabilitarBotones(false, false, true, true, true, false, true);
                        //else
                        HabilitarBotones(false, false, true, true, true, true, true);
                    }
                    else 
                    {
                        HabilitarBotones(false, false, true, false, true, false, false);
                        //pantab1.Enabled = false;

                    }
                    
                }else
                {
                    emergenciaActual = null;
                    CargarDatos();

                    dtMedicoAtencion= NegMedicos.RecuperaMedicoAtencion(pacienteActual.PAC_HISTORIA_CLINICA.Trim(), atencionActual.ATE_CODIGO);
                    if (dtMedicoAtencion.Rows.Count > 0)
                    {
                        txt_profesionalEmergencia.Text = dtMedicoAtencion.Rows[0]["DATOS"].ToString();
                        txt_CodMSPE.Text = dtMedicoAtencion.Rows[0]["MED_CODIGO_MEDICO"].ToString();
                    }

                    accion = "SAVE";
                    HabilitarBotones(true, false, false, false, true, false, false);
                }
            }
        }

        private void CargarCamposEmergencia(HC_EMERGENCIA_FORM emergenciaM)
        {
            if (emergenciaM.EMER_TRAUMA == true)
                chk_Trauma.Checked = true;
            else
                chk_Trauma.Checked = false;
            dtp_FechaHoraEvento.Value = emergenciaM.EMER_FECHA.Value;
            cmb_LugarEvento.SelectedItem = listaLugarE.FirstOrDefault(e => e.HCC_CODIGO == emergenciaM.HCC_CODIGO_LE);
            txt_DireccionEvento.Text = emergenciaM.EMER_DIRECCION_EVENTO;
            if (emergenciaM.EMER_CUSTODIA_POLICIAL == true)
                chk_CustodiaPolicial.Checked = true;
            else
                chk_CustodiaPolicial.Checked = false;
            cmb_Accidentes.SelectedItem =
                listaAccidentes.FirstOrDefault(a => a.HCC_CODIGO == emergenciaM.HCC_CODIGO_AVIEQ);
            txt_ObservacionAccidente.Text = emergenciaM.EMER_OBS_AVIEQ;
            if (emergenciaM.EMER_ALIENTO_ETIL == true)
                chb_AlientoEtilico.Checked = true;
            else
                chb_AlientoEtilico.Checked = false;
            if (emergenciaM.EMER_VALOR_ALCOH == true)
                chb_ValorAlcocheck.Checked = true;
            else
                chb_ValorAlcocheck.Checked = false;

            if (emergenciaM.EMER_VALOR_ALCOH == true)
                chb_ValorAlcocheck.Checked = true;
            else
                chb_ValorAlcocheck.Checked = false;
            if (emergenciaM.EMER_CAUSA_CLINICA == true)
                chk_CausaC.Checked = true;
            else
                chk_CausaC.Checked = false;
            if (emergenciaM.EMER_CUASA_GO == true)
                chk_CausaGOb.Checked = true;
            else
                chk_CausaGOb.Checked = false;
            if (emergenciaM.EMER_CUASA_Q == true)
                chk_CausaQuir.Checked = true;
            else
                chk_CausaQuir.Checked = false;
            txt_OtroMotivo.Text = emergenciaM.EMER_OTRO_MOTIVO;
            if (emergenciaM.EMER_CUASA_Q == true)
                chk_CausaQuir.Checked = true;
            else
                chk_CausaQuir.Checked = false;
            if (emergenciaM.EMER_VIA_AEREA == true)
                chk_ViaAL.Checked = true;
            else
                chk_ViaAL.Checked = false;
            if (emergenciaM.EMER_VIA_AEREA_OBT == true)
                chk_ViaAO.Checked = true;
            else
                chk_ViaAO.Checked = false;
            if (emergenciaM.EMER_COND_EST == true)
                chk_CondE.Checked = true;                
            else
                chk_CondE.Checked = false;

            if (emergenciaM.EMER_COND_INEST == true)
                chk_CondI.Checked = true;
            else
                chk_CondI.Checked = false;
            txt_EnfermedadActual.Text = emergenciaM.EMER_ENFACT_OBS;
            txt_PresionA1.Text = emergenciaM.EMER_PRES_A.ToString();
            txt_PresionA2.Text = emergenciaM.EMER_PRES_B.ToString();
            txt_FCardiaca.Text = emergenciaM.EMER_FREC_CARDIACA.ToString();
            txt_FResp.Text = emergenciaM.EMER_FREC_RESPIRATORIA.ToString();
            txt_TBucal.Text = emergenciaM.EMER_TEMP_BUCAL.ToString();
            txt_TAxilar.Text = emergenciaM.EMER_TEMP_AXILAR.ToString();
            txt_SaturaO.Text = emergenciaM.EMER_SATURA_OXI.ToString();
            txt_PesoKG.Text = emergenciaM.EMER_PESO.ToString();
            txt_Talla.Text = emergenciaM.EMER_TALLA.ToString();
            //txt_TotalG.Text = emergenciaM.EMER_GLASGOV.ToString();

            for (int i = 0; i < 4; i++ )
            {
                if (emergenciaM.EMER_OCULAR == i)
                    cmb_Ocular.SelectedIndex = i-1;
            }
            for (int i = 0; i < 5; i++)
            {
                if (emergenciaM.EMER_VERBAL == i)
                    cmb_Verbal.SelectedIndex = i-1;
            }
            for (int i = 0; i < 6; i++)
            {
                if (emergenciaM.EMER_MOTORA == i)
                    cmb_Motora.SelectedIndex = i-1;
            }



            //cmb_Ocular.SelectedItem = cmb_Ocular.Items.IndexOf(emergenciaM.EMER_OCULAR);
            //cmb_Verbal.SelectedText = cmb_Verbal.Items.IndexOf(emergenciaM.EMER_VERBAL);
            //cmb_Motora.SelectedItem = cmb_Motora.Items.Equals(emergenciaM.EMER_MOTORA);

            


            txt_DiamPDV.Text = emergenciaM.EMER_PUP_DIAD.ToString();

            cmb_ReacPDValor.SelectedItem = cmb_ReacPDValor.Items.Equals(emergenciaM.EMER_PUP_READ);
            txt_DiamPIV.Text = emergenciaM.EMER_PUP_DIAI.ToString();

            cmb_ReacPIValor.SelectedItem = cmb_ReacPIValor.Items.Equals(emergenciaM.EMER_PUP_REAI);

            txt_Glicemia.Text = emergenciaM.EMER_GLICEMIA_CAPILAR.ToString();

            if (emergenciaM.EMER_DOMICILIO == true)
                cmb_Destino.SelectedIndex = 0;
            if (emergenciaM.EMER_CONS_EXT == true)
                cmb_Destino.SelectedIndex = 1;
            if (emergenciaM.EMER_OBS_ALTA == true)
                cmb_Destino.SelectedIndex = 2;
            if (emergenciaM.EMER_INTER == true)
            {
                cmb_Destino.SelectedIndex = 3;
                txt_servicioReferencia.Text = emergenciaM.EMER_SER_REF;
            }
            if (emergenciaM.EMER_PREF == true)
            {
                cmb_Destino.SelectedIndex = 4;
                txt_Establecimiento.Text = emergenciaM.EMER_ESTAB;
            }

            if (emergenciaM.EMER_EGRESA_VIVO == true)
            {
                chk_EgresaVivo.Checked = true;
                txt_DiasInc.Text = emergenciaM.EMER_DIAS_INCAP.ToString();
                if (emergenciaM.EMER_CONDICION_EST == true)
                    chk_Estable.Checked = true;
                else
                    chk_Inestable.Checked = true;
            }
            if(emergenciaM.EMER_MUERTO_EMER == true)
            {
                chk_MuertoE.Checked = true;
                txt_CausaMuerte.Text = emergenciaActual.EMER_CAUSA_MUERTE.ToString();
            }
            else
                chk_MuertoE.Checked = false;
            if (emergenciaActual.EMER_FECHA_E != null)
                dtp_fechaAltaEmerencia.Value = Convert.ToDateTime(emergenciaActual.EMER_FECHA_E);
            else
                dtp_fechaAltaEmerencia.Value = DateTime.Now;
            txt_horaAltaEmerencia.Text = emergenciaM.EMER_HORA_E;
            txt_profesionalEmergencia.Text = emergenciaM.EMER_NOMBRE_PROF_E;
            txt_CodMSPE.Text = emergenciaM.EMER_CODIGO_PRO_E.ToString();
        }

        private void cargarAntePersFamiliares()
        {
            try
            {
                ant1 = NegCatalogos.RecuperarCatalogosPorTipo(His.Parametros.EmergenciaForm.CodigoAntecedentePF);
                Tipo.DataSource = ant1;
                Tipo.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void cargarExamenFisico()
        {
            try
            {
                ant2 = NegCatalogos.RecuperarCatalogosPorTipo(His.Parametros.EmergenciaForm.CodigoExaFD);
                tipo_Examen.DataSource = ant2;
                tipo_Examen.DisplayMember = "HCC_NOMBRE";
                //tipo_Examen.ValueMember = "HCC_CODIGO";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void cargarLesiones()
        {
            try
            {
                ant3 = NegCatalogos.RecuperarCatalogosPorTipo(His.Parametros.EmergenciaForm.CodigoLocalizacionL);
                Lesiones.DataSource = ant3;
                Lesiones.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void cargarSolicitusExamnes()
        {
            try
            {
                ant4 = NegCatalogos.RecuperarCatalogosPorTipo(His.Parametros.EmergenciaForm.CodigoSolicitudExa);
                Examenes.DataSource = ant4;
                Examenes.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }


        private void cargarDiagnosticos()
        {
            List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> diag = NegHcEmergenciaFDetalle.recuperarDiagnosticosHcEmergencia(emergenciaActual.EMER_CODIGO,"I");
            if (diag != null)
            {
                dtg_DiagnosticosI.Rows.Clear();
                foreach (HC_EMERGENCIA_FORM_DIAGNOSTICOS diagnosticos in diag)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell1 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell2 = new DataGridViewTextBoxCell();
                    DataGridViewCheckBoxCell chkcell = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell chkcell2 = new DataGridViewCheckBoxCell();
                    textcell.Value = diagnosticos.ED_CODIGO;
                    textcell1.Value = diagnosticos.ED_DESCRIPCION;
                    textcell2.Value = diagnosticos.CIE_CODIGO;
                    if (diagnosticos.ED_ESTADO.Value)
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
                    dtg_DiagnosticosI.Rows.Add(fila);
                }
            }
            //else
            //{
            //    dtg_DiagnosticosI.DataSource = null;
            //}

            diag = NegHcEmergenciaFDetalle.recuperarDiagnosticosHcEmergencia(emergenciaActual.EMER_CODIGO, "A");
            if (diag != null)
            {
                dtg_DiagnosticosAlta.Rows.Clear();
                foreach (HC_EMERGENCIA_FORM_DIAGNOSTICOS diagnosticos in diag)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell1 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell2 = new DataGridViewTextBoxCell();
                    DataGridViewCheckBoxCell chkcell = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell chkcell2 = new DataGridViewCheckBoxCell();
                    textcell.Value = diagnosticos.ED_CODIGO;
                    textcell1.Value = diagnosticos.ED_DESCRIPCION;
                    textcell2.Value = diagnosticos.CIE_CODIGO;
                    if (diagnosticos.ED_ESTADO.Value)
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
                    dtg_DiagnosticosAlta.Rows.Add(fila);
                }
            }
            //else
            //{
            //    dtg_DiagnosticosAlta.DataSource = null;
            //}
        }


        private void cargarDetalles()
        {


            //ANTECEDENTES PERSONALES
            List<HC_EMERGENCIA_FORM_EXAMENES> detalles = NegHcEmergenciaFDetalle.listaDetalleHcEmergencia(emergenciaActual.EMER_CODIGO, "4");
            if (detalles != null)
            {
                dtg_antec_personales.Rows.Clear();
                foreach (HC_EMERGENCIA_FORM_EXAMENES det in detalles)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewComboBoxCell cmbcell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    codigoCell.Value = det.EADE_CODIGO;
                    textcell.Value = det.EADE_DESCRIPCION;        
                    cmbcell.DataSource = ant1;                       
                    cmbcell.DisplayMember = "HCC_NOMBRE";
                    cmbcell.ValueMember = "HCC_NOMBRE";
                    cmbcell.Value = ant1.FirstOrDefault(i => i.HCC_CODIGO == det.HCC_CODIGO).HCC_NOMBRE;                      
                    fila.Cells.Add(codigoCell);
                    fila.Cells.Add(cmbcell);
                    fila.Cells.Add(textcell);
                    dtg_antec_personales.Rows.Add(fila);
                }
            }

            //LOCALIZACIÓN DE LESIONES
            cargarLesionesAtencion();
            //detalles = NegHcEmergenciaFDetalle.listaDetalleHcEmergencia(emergenciaActual.EMER_CODIGO, "8");
            //if (detalles != null)
            //{
            //    dgv_LocalizacionL.Rows.Clear();
            //    foreach (HC_EMERGENCIA_FORM_EXAMENES det in detalles)
            //    {
            //        DataGridViewRow fila = new DataGridViewRow();
            //        DataGridViewComboBoxCell cmbcell = new DataGridViewComboBoxCell();
            //        DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
            //        DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
            //        codigoCell.Value = det.EADE_CODIGO;
            //        if (det.HCC_CODIGO == 859)
            //            txt_OtrasL.Text = det.EADE_DESCRIPCION;
            //        cmbcell.DataSource = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.HCC_CODIGO == det.HCC_CODIGO).HCC_CODIGO);
            //        cmbcell.DisplayMember = "HCC_NOMBRE";
            //        cmbcell.ValueMember = "HCC_NOMBRE";
            //        cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.HCC_CODIGO == det.HCC_CODIGO).HCC_CODIGO).First().HCC_NOMBRE;
            //        fila.Cells.Add(codigoCell);
            //        fila.Cells.Add(cmbcell);
            //        //fila.Cells.Add(textcell);
            //        dgv_LocalizacionL.Rows.Add(fila);
            //    }
            //}

            //SOLICITUD DE EXAMENES
            detalles = NegHcEmergenciaFDetalle.listaDetalleHcEmergencia(emergenciaActual.EMER_CODIGO, "10");
            if (detalles != null)
            {
                dgv_ExamenesS.Rows.Clear();
                foreach (HC_EMERGENCIA_FORM_EXAMENES det in detalles)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewComboBoxCell cmbcell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    codigoCell.Value = det.EADE_CODIGO;
                    textcell.Value = det.EADE_DESCRIPCION;
                    cmbcell.DataSource = ant4;
                        //NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.HCC_CODIGO == det.HCC_CODIGO).HCC_CODIGO);
                    cmbcell.DisplayMember = "HCC_NOMBRE";
                    cmbcell.ValueMember = "HCC_NOMBRE";
                    cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.HCC_CODIGO == det.HCC_CODIGO).HCC_CODIGO).First().HCC_NOMBRE;
                    fila.Cells.Add(codigoCell);
                    fila.Cells.Add(cmbcell);
                    fila.Cells.Add(textcell);
                    dgv_ExamenesS.Rows.Add(fila);
                }
            }
            
            //EXAMENES FISICOS
            List<HC_EMERGENCIA_FORM_EXAMENFISICOD> examenes = NegHcEmergenciaFDetalle.listaDetalleHcEmergenciaEFD(emergenciaActual.EMER_CODIGO);
            if (examenes != null)
            {                
                dtg_ExamenFisico.Rows.Clear();
                foreach (HC_EMERGENCIA_FORM_EXAMENFISICOD exa in examenes)
                {
                    DataGridViewRow fila1 = new DataGridViewRow();
                    DataGridViewComboBoxCell cmbcell1 = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell codigoCell1 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell1 = new DataGridViewTextBoxCell();
                    DataGridViewCheckBoxCell chk1 = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell chk2 = new DataGridViewCheckBoxCell();
                    codigoCell1.Value = exa.EEFD_CODIGO;
                    cmbcell1.DataSource = ant2;
                        //NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.HCC_CODIGO == exa.HCC_CODIGO).HCC_CODIGO);
                    cmbcell1.DisplayMember = "HCC_NOMBRE";
                    cmbcell1.ValueMember = "HCC_NOMBRE";
                    cmbcell1.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.HCC_CODIGO == exa.HCC_CODIGO).HCC_CODIGO).First().HCC_NOMBRE;
                    textcell1.Value = exa.EEFD_DESCRIPCION;
                    if (exa.EEFD_TIPO.Trim().Equals("CP"))
                    {
                        chk1.Value = true;
                        chk2.Value = false;
                    }
                    else
                    {
                        chk2.Value = true;
                        chk1.Value = false;
                    }
                    fila1.Cells.Add(codigoCell1);
                    fila1.Cells.Add(cmbcell1);
                    fila1.Cells.Add(chk1);
                    fila1.Cells.Add(chk2);
                    fila1.Cells.Add(textcell1);
                    dtg_ExamenFisico.Rows.Add(fila1);
                }
            }
        }

        private void cargarLesionesAtencion()
        {
            //LOCALIZACIÓN DE LESIONES
            List<HC_EMERGENCIA_FORM_EXAMENES>  detalles = NegHcEmergenciaFDetalle.listaDetalleHcEmergencia(emergenciaActual.EMER_CODIGO, "8");
            if (detalles != null)
            {
                dgv_LocalizacionL.Rows.Clear();
                foreach (HC_EMERGENCIA_FORM_EXAMENES det in detalles)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewComboBoxCell cmbcell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    codigoCell.Value = det.EADE_CODIGO;
                    if (det.HCC_CODIGO == 859)
                        txt_OtrasL.Text = det.EADE_DESCRIPCION;
                    cmbcell.DataSource = ant3;
                        //NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.HCC_CODIGO == det.HCC_CODIGO).HCC_CODIGO);
                    cmbcell.DisplayMember = "HCC_NOMBRE";
                    cmbcell.ValueMember = "HCC_NOMBRE";
                    cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.HCC_CODIGO == det.HCC_CODIGO).HCC_CODIGO).First().HCC_NOMBRE;
                    fila.Cells.Add(codigoCell);
                    fila.Cells.Add(cmbcell);
                    //fila.Cells.Add(textcell);
                    dgv_LocalizacionL.Rows.Add(fila);
                }
            }
        }

        private void cargarTratamientosMedicamentos()
        {
            List<HC_EMERGENCIA_FORM_TRATAMIENTO> listaIndicaciones = NegHcEmergenciaFDetalle.recuperarTratameinto(emergenciaActual.EMER_CODIGO, "I");
            if (listaIndicaciones != null)
            {
                dgv_Indicaciones.Rows.Clear();
                foreach (HC_EMERGENCIA_FORM_TRATAMIENTO tratM in listaIndicaciones)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    codigoCell.Value = tratM.ETRA_CODIGO;
                    textcell.Value = tratM.ETRA_DESCRIPCION;
                    fila.Cells.Add(codigoCell);
                    fila.Cells.Add(textcell);
                    dgv_Indicaciones.Rows.Add(fila);
                }
            }

            List<HC_EMERGENCIA_FORM_TRATAMIENTO> listaMedicamentos = NegHcEmergenciaFDetalle.recuperarTratameinto(emergenciaActual.EMER_CODIGO, "M");
            if (listaMedicamentos != null)
            {
                dgv_Medicamentos.Rows.Clear();
                foreach (HC_EMERGENCIA_FORM_TRATAMIENTO med in listaMedicamentos)
                {
                    DataGridViewRow fila = new DataGridViewRow();

                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell CodigoPcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell PosologiaCell = new DataGridViewTextBoxCell();

                    codigoCell.Value = med.ETRA_CODIGO;
                    CodigoPcell.Value = med.PRO_CODIGO;
                    textcell.Value = med.ETRA_DESCRIPCION;
                    PosologiaCell.Value = med.EMER_POSOLOGIA;

                    fila.Cells.Add(codigoCell);
                    fila.Cells.Add(CodigoPcell);
                    fila.Cells.Add(textcell);
                    fila.Cells.Add(PosologiaCell);

                    dgv_Medicamentos.Rows.Add(fila);
                }
            }

            //dgv_Medicamentos

        }


        private void CargarEmergenciaObstetrica()
        {
            try
            {
                if (emergenciaActual != null)
                {
                    HC_EMERGENCIA_FORM_OBSTETRICA eObst =
                        NegHcEmergenciaFDetalle.recuperarHCEObstetrica(emergenciaActual.EMER_CODIGO);
                    if (eObst != null)
                    {
                        txt_Gesta.Text = eObst.EOBT_GESTA.ToString();
                        txt_Partos.Text = eObst.EOBT_PARTOS.ToString();
                        txt_Abortos.Text = eObst.EOBT_ABORTOS.ToString();
                        txt_Cesareas.Text = eObst.EOBT_CESAREAS.ToString();
                        dtp_ultimaMenst1.Value = eObst.EOBT_FUM.Value;
                        txt_SemanaG.Text = eObst.EOBT_SEM_GESTACION;
                        chk_MovimientoF.Checked = Convert.ToBoolean(eObst.EOBT_MOV_FETAL);
                        txt_FrecCF.Text = eObst.EOBT_FREC_CFETAL.ToString();
                        chk_MembranaS.Checked = Convert.ToBoolean(eObst.EOBT_MEM_SROTAS);
                        txt_Tiempo.Text = eObst.EOBT_TIEMPO.ToString();
                        txt_AltU.Text = eObst.EOBT_ALT_UTERINA.ToString();
                        txt_Presentacion.Text = eObst.EOBT_PRESENTACION.ToString();
                        txt_Dilatacion.Text = eObst.EOBT_DILATACION.ToString();
                        txt_Borramiento.Text = eObst.EOBT_BORRAMIENTO.ToString();
                        txt_Plano.Text = eObst.EOBT_PLANO.ToString();
                        chk_PelvisU.Checked = Convert.ToBoolean(eObst.EOBT_PELVIS_UTIL);
                        chk_SangradoV.Checked = Convert.ToBoolean(eObst.EOBT_SANGRADO_VAGINAL);
                        txt_Contracciones.Text = eObst.EOBT_CONTACCIONES.ToString();
                        txt_observacion.Text = eObst.EOBT_OBSERVACION.ToString();
                    }
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }



        public void GuardarEmergencia()
        {
            try
            {
                if (!ValidarCampos())
                {
                    if (emergenciaActual == null)
                        emergenciaActual = new HC_EMERGENCIA_FORM();
                    if (chk_Trauma.Checked == true)
                    {
                        emergenciaActual.EMER_TRAUMA = true;
                        emergenciaActual.EMER_FECHA_EVENTO = dtp_FechaHoraEvento.Value;
                        emergenciaActual.HCC_CODIGO_LE = ((HC_CATALOGOS) (cmb_LugarEvento.SelectedItem)).HCC_CODIGO;
                        emergenciaActual.EMER_DIRECCION_EVENTO = txt_DireccionEvento.Text;
                        if (chk_CustodiaPolicial.Checked == true)
                            emergenciaActual.EMER_CUSTODIA_POLICIAL = true;
                        else
                            emergenciaActual.EMER_CUSTODIA_POLICIAL = false;
                        emergenciaActual.HCC_CODIGO_AVIEQ = ((HC_CATALOGOS) (cmb_Accidentes.SelectedItem)).HCC_CODIGO;
                        emergenciaActual.EMER_OBS_AVIEQ = txt_ObservacionAccidente.Text;
                        if (chb_AlientoEtilico.Checked == true)
                            emergenciaActual.EMER_ALIENTO_ETIL = true;
                        else
                            emergenciaActual.EMER_ALIENTO_ETIL = false;
                        if (chb_ValorAlcocheck.Checked == true)
                            emergenciaActual.EMER_VALOR_ALCOH = true;
                        else
                            emergenciaActual.EMER_VALOR_ALCOH = false;
                    }
                    else
                        emergenciaActual.EMER_TRAUMA = false;
                    if (chk_CausaC.Checked == true)
                        emergenciaActual.EMER_CAUSA_CLINICA = true;
                    else
                        emergenciaActual.EMER_CAUSA_CLINICA = false;
                    if (chk_CausaGOb.Checked == true)
                    {
                        emergenciaActual.EMER_CUASA_GO = true;

                    }
                    else
                        emergenciaActual.EMER_CUASA_GO = false;
                    if (chk_CausaQuir.Checked == true)
                        emergenciaActual.EMER_CUASA_Q = true;
                    else
                        emergenciaActual.EMER_CUASA_Q = false;
                    emergenciaActual.EMER_OTRO_MOTIVO = txt_OtroMotivo.Text;
                    if (chk_ViaAL.Checked == true)
                        emergenciaActual.EMER_VIA_AEREA = true;
                    else
                        emergenciaActual.EMER_VIA_AEREA = false;
                    if (chk_ViaAO.Checked == true)
                        emergenciaActual.EMER_VIA_AEREA_OBT = true;
                    else
                        emergenciaActual.EMER_VIA_AEREA_OBT = false;

                    if (chk_CondE.Checked == true)
                        emergenciaActual.EMER_COND_EST = true;
                    else
                        emergenciaActual.EMER_COND_EST = false;
                    if (chk_CondI.Checked == true)
                        emergenciaActual.EMER_COND_INEST = true;
                    else
                        emergenciaActual.EMER_COND_INEST = false;
                    emergenciaActual.EMER_ENFACT_OBS = txt_EnfermedadActual.Text;
                    emergenciaActual.EMER_PRES_A = Convert.ToInt32(txt_PresionA1.Text);
                    emergenciaActual.EMER_PRES_B = Convert.ToInt32(txt_PresionA2.Text);
                    emergenciaActual.EMER_FREC_CARDIACA = txt_FCardiaca.Text;
                    emergenciaActual.EMER_FREC_RESPIRATORIA = txt_FResp.Text;
                    emergenciaActual.EMER_TEMP_BUCAL = txt_TBucal.Text;
                    emergenciaActual.EMER_TEMP_AXILAR = txt_TAxilar.Text;
                    emergenciaActual.EMER_SATURA_OXI = Convert.ToInt32(txt_SaturaO.Text);
                    emergenciaActual.EMER_PESO = txt_PesoKG.Text;
                    emergenciaActual.EMER_TALLA = txt_Talla.Text;
                    emergenciaActual.EMER_GLASGOV = Convert.ToInt32(txt_TotalG.Text);
                    emergenciaActual.EMER_OCULAR = Convert.ToInt32(cmb_Ocular.SelectedValue);
                    emergenciaActual.EMER_VERBAL = Convert.ToInt32(cmb_Verbal.SelectedItem);
                    emergenciaActual.EMER_MOTORA = Convert.ToInt32(cmb_Motora.SelectedItem);
                    if (txt_DiamPDV.Text.Trim() != "")
                        emergenciaActual.EMER_PUP_DIAD = Convert.ToInt32(txt_DiamPDV.Text);
                    emergenciaActual.EMER_PUP_READ = cmb_ReacPDValor.SelectedText;
                    if (txt_DiamPIV.Text.Trim() != "")
                        emergenciaActual.EMER_PUP_DIAI = Convert.ToInt32(txt_DiamPIV.Text);
                    emergenciaActual.EMER_PUP_REAI = cmb_ReacPIValor.SelectedText;
                    if (txt_Glicemia.Text.Trim() != "")
                        emergenciaActual.EMER_GLICEMIA_CAPILAR = Convert.ToInt32(txt_Glicemia.Text);

                    emergenciaActual.EMER_DOMICILIO = cmb_Destino.SelectedIndex == 0
                                                          ? emergenciaActual.EMER_DOMICILIO = true
                                                          : false;
                    emergenciaActual.EMER_CONS_EXT = cmb_Destino.SelectedIndex == 1
                                                         ? emergenciaActual.EMER_CONS_EXT = true
                                                         : false;
                    emergenciaActual.EMER_OBS_ALTA = cmb_Destino.SelectedIndex == 2
                                                         ? emergenciaActual.EMER_OBS_ALTA = true
                                                         : false;
                    if (cmb_Destino.SelectedIndex == 3)
                    {
                        emergenciaActual.EMER_INTER = true;
                        emergenciaActual.EMER_SER_REF = txt_servicioReferencia.Text;
                    }
                    else
                        emergenciaActual.EMER_SER_REF = "";
                    if (cmb_Destino.SelectedIndex == 4)
                    {
                        emergenciaActual.EMER_PREF = true;
                        emergenciaActual.EMER_ESTAB = txt_Establecimiento.Text;
                    }
                    else
                        emergenciaActual.EMER_ESTAB = "";
                    if (chk_EgresaVivo.Checked == true)
                    {
                        emergenciaActual.EMER_EGRESA_VIVO = true;
                        emergenciaActual.EMER_CONDICION_EST = chk_Estable.Checked == true? true:false;
                        emergenciaActual.EMER_CONDICION_INES = chk_Inestable.Checked == true ? true : false;
                        if (txt_DiasInc.Text != "" && txt_DiasInc.Text != null)
                            emergenciaActual.EMER_DIAS_INCAP = Convert.ToInt32(txt_DiasInc.Text);
                        else
                            emergenciaActual.EMER_DIAS_INCAP = 0;

                        emergenciaActual.EMER_MUERTO_EMER = false;
                    }
                    else
                    {
                        emergenciaActual.EMER_EGRESA_VIVO = false;
                    }
                    if (chk_MuertoE.Checked == true)
                    {
                        emergenciaActual.EMER_MUERTO_EMER = true;
                        emergenciaActual.EMER_CAUSA_MUERTE = txt_CausaMuerte.Text;
                        emergenciaActual.EMER_EGRESA_VIVO = false;
                    }
                    emergenciaActual.EMER_FECHA_E = dtp_fechaAltaEmerencia.Value;
                    emergenciaActual.EMER_HORA_E = txt_horaAltaEmerencia.Text;
                    emergenciaActual.EMER_NOMBRE_PROF_E = txt_profesionalEmergencia.Text;

                    if (txt_CodMSPE.Text != "")
                    {
                        emergenciaActual.EMER_CODIGO_PRO_E = txt_CodMSPE.Text.Trim() ;
                    }
                    else
                    {
                        emergenciaActual.EMER_CODIGO_PRO_E = "";
                    }

                    if (accion == "SAVE")
                    {
                        emergenciaActual.EMER_FECHA = Convert.ToDateTime(DateTime.Now);
                        emergenciaActual.ATENCIONESReference.EntityKey = atencionActual.EntityKey;
                        emergenciaActual.PACIENTESReference.EntityKey = pacienteActual.EntityKey;
                        emergenciaActual.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                        emergenciaActual.EMER_CODIGO = NegHcEmergenciaForm.ultimoCodigo() + 1;
                        NegHcEmergenciaForm.CrearHCEmergenciaF(emergenciaActual);
                        GuardarDetalles();
                        GuardarTratamiento();
                        if (dtg_DiagnosticosAlta.Rows[0].Cells[1].Value.ToString() != "")
                        {
                            NegHcEmergencia.ActualizaDiagnostico(dtg_DiagnosticosAlta.Rows[0].Cells[1].Value.ToString(), atencionActual.ATE_CODIGO);
                        }
                    }
                    else
                    {
                        if (accion == "UPDATE")
                        {
                            NegHcEmergenciaForm.ModificarHCEmergenciaF(emergenciaActual);
                            GuardarDetalles();
                            GuardarTratamiento();
                        }
                    }
                    MessageBox.Show("Registro Guardado", "FORM. 008", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarBotones(false, false, true, true, true, false, true);

                    ultraGroupBox7.Enabled = false;
                    grpDosTresCuatro.Enabled = false;
                    grpCincoSeis.Enabled = false;
                    grpSieteOchoNueve.Enabled = false;
                    groupBox31.Enabled = false;
                    grpTrceCatorce.Enabled = false;

                    accion = "UPDATE";
                    cargarDetalles();
                    cargarDiagnosticos();
                    cargarTratamientosMedicamentos();
                }else
                {
                    MessageBox.Show("Revise Campos Obligatorios", "FORM. 008", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
            
        }

        private void GuardarDetalles()
        {
            //ANTECEDENTES PERSONALES
            try
            {
                foreach (DataGridViewRow fila in dtg_antec_personales.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_EMERGENCIA_FORM_EXAMENES detalle = new HC_EMERGENCIA_FORM_EXAMENES();
                            if (fila.Cells[2].Value != null)
                                detalle.EADE_DESCRIPCION = fila.Cells[2].Value.ToString();
                            else
                                detalle.EADE_DESCRIPCION = "";
                            detalle.HC_EMERGENCIA_FORMReference.EntityKey = emergenciaActual.EntityKey;
                            //HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HCC_CODIGO = ant1.FirstOrDefault(n => n.HCC_NOMBRE == fila.Cells[1].Value.ToString().Trim()).HCC_CODIGO;
                                //hc.HCC_CODIGO;
                            detalle.EADE_PADRE = "4";
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            if (fila.Cells[0].Value != null)
                            {
                                detalle.EADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegHcEmergenciaFDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                detalle.EADE_CODIGO = NegHcEmergenciaFDetalle.ultimoCodigo() + 1;
                                NegHcEmergenciaFDetalle.crearHcEmergenciaDetalle(detalle);
                            }
                        }
                    }
                }

                //LOCALIZACIÓN DE LESIONES
                if (chk_Trauma.Checked == true)
                {
                    foreach (DataGridViewRow fila in dgv_LocalizacionL.Rows)
                    {
                        if (fila != null)
                        {
                            if (fila.Cells[1].Value != null)
                            {
                                HC_EMERGENCIA_FORM_EXAMENES detalle = new HC_EMERGENCIA_FORM_EXAMENES();
                                detalle.EADE_DESCRIPCION = txt_OtrasL.Text;
                                detalle.HC_EMERGENCIA_FORMReference.EntityKey = emergenciaActual.EntityKey;
                                //HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                                detalle.HCC_CODIGO = ant3.FirstOrDefault(n => n.HCC_NOMBRE == fila.Cells[1].Value.ToString().Trim()).HCC_CODIGO;
                                    //hc.HCC_CODIGO;
                                detalle.EADE_PADRE = "8";
                                detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                                //corregir q al  momento de seleccionar otras el tab vaya al txt_Otras
                                if (detalle.HCC_CODIGO == 859)
                                    detalle.EADE_DESCRIPCION = txt_OtrasL.Text;
                                if (fila.Cells[0].Value != null)
                                {
                                    detalle.EADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                    NegHcEmergenciaFDetalle.actualizarDetalle(detalle);
                                }
                                else
                                {
                                    detalle.EADE_CODIGO = NegHcEmergenciaFDetalle.ultimoCodigo() + 1;
                                    NegHcEmergenciaFDetalle.crearHcEmergenciaDetalle(detalle);
                                }
                            }
                        }
                    }
                }
                else
                {
                    List<HC_EMERGENCIA_FORM_EXAMENES> detalles = NegHcEmergenciaFDetalle.listaDetalleHcEmergencia(emergenciaActual.EMER_CODIGO, "8");
                    if (detalles != null)
                    {
                        dgv_LocalizacionL.Rows.Clear();
                        foreach (HC_EMERGENCIA_FORM_EXAMENES det in detalles)
                        {
                            NegHcEmergenciaFDetalle.eliminarEFD(det.EADE_CODIGO);
                        }
                    }
                }

                //SOLICITUD DE EXAMENES
                foreach (DataGridViewRow fila in dgv_ExamenesS.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_EMERGENCIA_FORM_EXAMENES detalle = new HC_EMERGENCIA_FORM_EXAMENES();
                            if (fila.Cells[2].Value != null)
                                detalle.EADE_DESCRIPCION = fila.Cells[2].Value.ToString();
                            else
                                detalle.EADE_DESCRIPCION = "";
                            detalle.HC_EMERGENCIA_FORMReference.EntityKey = emergenciaActual.EntityKey;
                            //HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HCC_CODIGO = ant4.FirstOrDefault(n => n.HCC_NOMBRE == fila.Cells[1].Value.ToString().Trim()).HCC_CODIGO;
                                //hc.HCC_CODIGO;
                            detalle.EADE_PADRE = "10";
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            if (fila.Cells[0].Value != null)
                            {
                                detalle.EADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegHcEmergenciaFDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                detalle.EADE_CODIGO = NegHcEmergenciaFDetalle.ultimoCodigo() + 1;
                                NegHcEmergenciaFDetalle.crearHcEmergenciaDetalle(detalle);
                            }
                        }
                    }
                }

                //EXAMEN FISICO
                foreach (DataGridViewRow fila in dtg_ExamenFisico.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[2].Value != null)
                        {
                            HC_EMERGENCIA_FORM_EXAMENFISICOD detalle = new HC_EMERGENCIA_FORM_EXAMENFISICOD();
                            if (fila.Cells[4].Value != null)
                                detalle.EEFD_DESCRIPCION = fila.Cells[4].Value.ToString();
                            else
                                detalle.EEFD_DESCRIPCION = "";
                            detalle.HC_EMERGENCIA_FORMReference.EntityKey = emergenciaActual.EntityKey;                            
                            detalle.HCC_CODIGO = (ant2.FirstOrDefault(n => n.HCC_NOMBRE == fila.Cells[1].Value.ToString().Trim())).HCC_CODIGO;                                
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            if (Convert.ToBoolean(fila.Cells[2].Value))
                                detalle.EEFD_TIPO = "CP";
                            else
                                detalle.EEFD_TIPO = "SP";
                            if (fila.Cells[0].Value != null)
                            {
                                detalle.EEFD_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegHcEmergenciaFDetalle.actualizarEFD(detalle);
                            }
                            else
                            {
                                detalle.EEFD_CODIGO = NegHcEmergenciaFDetalle.ultimoCodigoEFD() + 1;
                                NegHcEmergenciaFDetalle.crearHcEmergenciaEFD(detalle);
                            }
                        }

                    }
                }

                //DIAGNOSTICOS ALTA
                foreach (DataGridViewRow fila in dtg_DiagnosticosAlta.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_EMERGENCIA_FORM_DIAGNOSTICOS detalle = new HC_EMERGENCIA_FORM_DIAGNOSTICOS();
                            detalle.CIE_CODIGO = fila.Cells[2].Value.ToString();
                            if (Convert.ToBoolean(fila.Cells[3].Value))
                                detalle.ED_ESTADO = true;
                            else
                                detalle.ED_ESTADO = false;
                            detalle.HC_EMERGENCIA_FORMReference.EntityKey = emergenciaActual.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            detalle.ED_DESCRIPCION = fila.Cells[1].Value.ToString();
                            detalle.ED_TIPO = "A";
                            if (fila.Cells[0].Value != null)
                            {
                                detalle.ED_CODIGO = Convert.ToInt32(fila.Cells[0].Value.ToString());
                                NegHcEmergenciaFDetalle.actualizarHcEmergenciaDiagnostico(detalle);
                            }
                            else
                                NegHcEmergenciaFDetalle.crearHCEDiagnosticos(detalle);
                        }
                    }
                }

                //DIAGNOSTICOS INGRESO
                foreach (DataGridViewRow fila in dtg_DiagnosticosI.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_EMERGENCIA_FORM_DIAGNOSTICOS detalle = new HC_EMERGENCIA_FORM_DIAGNOSTICOS();
                            detalle.CIE_CODIGO = fila.Cells[2].Value.ToString();
                            if (Convert.ToBoolean(fila.Cells[3].Value))
                                detalle.ED_ESTADO = true;
                            else
                                detalle.ED_ESTADO = false;
                            detalle.HC_EMERGENCIA_FORMReference.EntityKey = emergenciaActual.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            detalle.ED_DESCRIPCION = fila.Cells[1].Value.ToString();
                            detalle.ED_TIPO = "I";
                            if (fila.Cells[0].Value != null)
                            {
                                detalle.ED_CODIGO = Convert.ToInt32(fila.Cells[0].Value.ToString());
                                NegHcEmergenciaFDetalle.actualizarHcEmergenciaDiagnostico(detalle);
                            }
                            else
                                NegHcEmergenciaFDetalle.crearHCEDiagnosticos(detalle);
                        }
                    }
                }
                if (gpb_Obstetrica.Enabled)
                    GuardarEmergenciaObstetrica();
            }
            catch (Exception err) { MessageBox.Show("error", err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void GuardarEmergenciaObstetrica()
        {
            try
            {
                HC_EMERGENCIA_FORM_OBSTETRICA eObst = NegHcEmergenciaFDetalle.recuperarHCEObstetrica(emergenciaActual.EMER_CODIGO);
                if (chk_CausaGOb.Checked == true)
                {
                    bool valor = false;                    
                    if (eObst == null)
                    {
                        valor = true;
                        eObst = new HC_EMERGENCIA_FORM_OBSTETRICA();
                        eObst.EOBT_CODIGO = NegHcEmergenciaFDetalle.ultimoCodigoHCEObstetrica() + 1;
                    }
                    eObst.EOBT_GESTA = Convert.ToInt32(txt_Gesta.Text);
                    eObst.EOBT_PARTOS = Convert.ToInt32(txt_Partos.Text);
                    eObst.EOBT_ABORTOS = Convert.ToInt32(txt_Abortos.Text);
                    eObst.EOBT_CESAREAS = Convert.ToInt32(txt_Cesareas.Text);
                    eObst.EOBT_FUM = dtp_ultimaMenst1.Checked == true ? dtp_ultimaMenst1.Value : Convert.ToDateTime("01/01/1900");
                    eObst.EOBT_SEM_GESTACION = dtp_ultimaMenst1.Checked == true ? txt_SemanaG.Text:"";
                    eObst.EOBT_MOV_FETAL = chk_MovimientoF.Checked == true ? eObst.EOBT_MOV_FETAL = true : false;
                    eObst.EOBT_FREC_CFETAL = Convert.ToInt32(txt_FrecCF.Text);
                    eObst.EOBT_MEM_SROTAS = chk_MembranaS.Checked == true ? eObst.EOBT_MEM_SROTAS = true : false;
                    eObst.EOBT_TIEMPO = txt_Tiempo.Text;
                    eObst.EOBT_ALT_UTERINA = Convert.ToInt32(txt_AltU.Text);
                    eObst.EOBT_PRESENTACION = txt_Presentacion.Text;
                    eObst.EOBT_DILATACION = Convert.ToInt32(txt_Dilatacion.Text);
                    eObst.EOBT_BORRAMIENTO = Convert.ToInt32(txt_Borramiento.Text);
                    eObst.EOBT_PLANO = txt_Plano.Text;
                    eObst.EOBT_PELVIS_UTIL = chk_PelvisU.Checked == true ? eObst.EOBT_PELVIS_UTIL = true : false;
                    eObst.EOBT_SANGRADO_VAGINAL = chk_SangradoV.Checked == true ? eObst.EOBT_SANGRADO_VAGINAL = true : false;
                    eObst.EOBT_CONTACCIONES = txt_Contracciones.Text;
                    eObst.EOBT_OBSERVACION = txt_observacion.Text;
                    if (valor == true)
                    {
                        eObst.HC_EMERGENCIA_FORMReference.EntityKey = emergenciaActual.EntityKey;
                        NegHcEmergenciaFDetalle.crearHCEObstetrica(eObst);
                    }
                    else
                    {
                        NegHcEmergenciaFDetalle.actualizarHCEObstetrica(eObst);
                    }
                }
                else 
                {
                    if (eObst != null)
                        NegHcEmergenciaFDetalle.eliminarHCEObstetrica(eObst.EOBT_CODIGO);
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void GuardarTratamiento()
        {
            try
            {
                //PLAN DE TRATAMIENTO INDICACIONES
                foreach (DataGridViewRow fila in dgv_Indicaciones.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_EMERGENCIA_FORM_TRATAMIENTO ind = new HC_EMERGENCIA_FORM_TRATAMIENTO();
                            ind.ETRA_DESCRIPCION = fila.Cells[1].Value.ToString();
                            ind.ETRA_TIPO = "I";
                            ind.HC_EMERGENCIA_FORMReference.EntityKey = emergenciaActual.EntityKey;
                            ind.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            ind.PRO_CODIGO = "";
                            ind.EMER_POSOLOGIA = "";
                            if (fila.Cells[0].Value != null)
                            {
                                ind.ETRA_CODIGO = Convert.ToInt32(fila.Cells[0].Value.ToString());
                                NegHcEmergenciaFDetalle.actualizarTratameinto(ind);
                            }
                            else
                                NegHcEmergenciaFDetalle.crearTratamiento(ind);
                        }
                    }
                }

                //PLAN DE TRATAMIENTO MEDICAMENTOS
                foreach (DataGridViewRow fila in dgv_Medicamentos.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_EMERGENCIA_FORM_TRATAMIENTO med = new HC_EMERGENCIA_FORM_TRATAMIENTO();
                            med.ETRA_DESCRIPCION = fila.Cells[2].Value.ToString();
                            med.ETRA_TIPO = "M";
                            med.HC_EMERGENCIA_FORMReference.EntityKey = emergenciaActual.EntityKey;
                            med.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            if (fila.Cells[3].Value != null)
                            {
                                med.EMER_POSOLOGIA = fila.Cells[3].Value.ToString();
                            }
                            else
                            {
                                med.EMER_POSOLOGIA = "";
                            }
                            med.PRO_CODIGO = fila.Cells[1].Value.ToString();

                            if (fila.Cells[0].Value != null)
                            {
                                med.ETRA_CODIGO = Convert.ToInt32(fila.Cells[0].Value.ToString());
                                NegHcEmergenciaFDetalle.actualizarTratameinto(med);
                            }
                            else
                            {
                                //gENERA UN CODIGO PARA LA TABLA HC_EMERGENCIA_FORM_TRATAMIENTO                            
                                NegHcEmergenciaFDetalle.crearTratamiento(med);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        //public void ValidarCampos()
        //{

        //}

        private void activarFormulario(bool estado)
        {
            grpDosTresCuatro.Enabled = estado;
            grpCincoSeis.Enabled = estado;
            grpSieteOchoNueve.Enabled = estado;
            grpDiezOnceDoce.Enabled = estado;
            grpTrceCatorce.Enabled = estado;
        }


        private void LimpiarCamposAccidentes()
        {
            gpb_Accidentes.Enabled = false;
            dtp_FechaHoraEvento.Value = DateTime.Now;
            cmb_LugarEvento.DataSource = listaLugarE;
            cmb_LugarEvento.SelectedIndex = 0;
            txt_DireccionEvento.Text = "";
            chk_CustodiaPolicial.Checked = false;
            cmb_Accidentes.DataSource = listaAccidentes;
            cmb_Accidentes.SelectedIndex = 0;
            txt_ObservacionAccidente.Text = "";
            chb_AlientoEtilico.Checked = false;
            chb_ValorAlcocheck.Checked = false;
            txt_OtroMotivo.Text = "";
            txt_OtroMotivo.Enabled = false;
            dgv_LocalizacionL.Rows.Clear();
            gpb_Localizacion.Enabled = false;
            txt_OtrasL.Text = "";            
        }

        private void LimpiarCamposObstetricos()
        {
            gpb_Obstetrica.Enabled = false;
            dgv_LocalizacionL.DataSource = null;
            txt_Gesta.Text = "";
            txt_Partos.Text = "";
            txt_Abortos.Text= "";
            txt_Cesareas.Text= "";
            dtp_ultimaMenst1.Value = DateTime.Now;
            txt_SemanaG.Text = "";
            chk_MovimientoF.Checked = false;
            txt_FrecCF.Text = "";
            chk_MembranaS.Checked = false;
            txt_Tiempo.Text= "";
            txt_AltU.Text= "";
            txt_Presentacion.Text= "";
            txt_Dilatacion.Text= "";
            txt_Borramiento.Text = "";
            txt_Plano.Text = "";
            chk_PelvisU.Checked = false;
            chk_SangradoV.Checked = false;
            txt_Contracciones.Text = "";
            txt_observacion.Text = "";
        }


        public void calcularREMS()
        {

            if (!(txt_FCardiaca.Text == String.Empty || txt_FResp.Text == String.Empty ||
                txt_SaturaO.Text == String.Empty || txt_PresionA1.Text == String.Empty ||
                txt_PresionA2.Text == String.Empty || txt_TotalG.Text == String.Empty))
            {
                int edad = Funciones.CalcularEdad(pacienteActual.PAC_FECHA_NACIMIENTO.Value);
                int fc = Convert.ToInt16(txt_FCardiaca.Text);
                int fr = Convert.ToInt16(txt_FResp.Text);
                int tas = Convert.ToInt16(txt_PresionA1.Text);
                int glasgow = Convert.ToInt16(txt_TotalG.Text);
                int sato2 = Convert.ToInt16(txt_SaturaO.Text);

                int rems = 0;

                //rems edad
                if (edad < 45)
                    rems = 0;
                else if (edad >= 45 && edad <= 54)
                    rems = 2;
                else if (edad >= 55 && edad <= 64)
                    rems = 3;
                else if (edad >= 65 && edad <= 74)
                    rems = 5;
                else if (edad > 74)
                    rems = 6;


                //rems fc
                if (fc >= 70 && fc <= 109)
                    rems += 0;
                else if (fc >= 55 && fc <= 69)
                    rems += 2;
                else if (fc >= 40 && fc <= 54)
                    rems += 3;
                else if (fc < 40)
                    rems += 4;
                else if (fc >= 110 && fc <= 139)
                    rems += 2;
                else if (fc >= 140 && fc <= 179)
                    rems += 3;
                else if (fc > 179)
                    rems += 4;

                //rems fr
                if (fr >= 12 && fr <= 24)
                    rems += 0;
                else if (fr >= 10 && fr <= 11)
                    rems += 1;
                else if (fr >= 25 && fr <= 34)
                    rems += 1;
                else if (fr >= 6 && fr <= 9)
                    rems += 2;
                else if (fr >= 35 && fr <= 49)
                    rems += 3;
                else if (fr < 6)
                    rems += 4;
                else if (fr > 49)
                    rems += 4;

                //rems tas
                if (tas >= 90 && tas <= 129)
                    rems += 0;
                else if (tas >= 70 && tas <= 89)
                    rems += 2;
                else if (tas >= 130 && tas <= 149)
                    rems += 2;
                else if (tas >= 150 && tas <= 179)
                    rems += 3;
                else if (tas < 69)
                    rems += 4;
                else if (tas > 179)
                    rems += 4;


                //rems glasgow
                if (glasgow < 5)
                    rems += 4;
                else if (glasgow >= 11 && glasgow <= 13)
                    rems += 1;
                else if (glasgow >= 8 && glasgow <= 10)
                    rems += 2;
                else if (glasgow >= 5 && glasgow <= 7)
                    rems += 3;
                else if (glasgow > 13)
                    rems += 0;

                //rems sato2
                if (sato2 < 75)
                    rems += 4;
                else if (sato2 >= 86 && sato2 <= 89)
                    rems += 1;
                else if (sato2 >= 75 && sato2 <= 85)
                    rems += 3;
                else if (sato2 > 89)
                    rems += 0;


                if (rems < 6)
                    lblRiesgo.Text = "   Bajo";
                else if (rems >= 6 && rems <= 13)
                    lblRiesgo.Text = "Moderado";
                else if (rems > 13)
                    lblRiesgo.Text = "   Alto";

                rShapeTriage.Text = rems.ToString();
            }

            else
            {
                rShapeTriage.BackColor = Color.Transparent;
            }
        }



        #endregion
        
        
        #region EVENTOS
        private void frm_Emergencia_Load(object sender, EventArgs e)
        {
            try
            {
                if (mostrarInfPaciente == true)
                {
                    //Añado el panel con la informaciòn del paciente
                    //InfPaciente infPaciente = new InfPaciente(atencionId);
                    //panelInfPaciente.Controls.Add(infPaciente);
                    //cambio las dimenciones de los paneles
                    //panelInfPaciente.Size = new Size(panelInfPaciente.Width, 110);
                    pantab1.Top = 60;
                    //cargar tamaño por defecto de la vista
                    this.Height = this.Height + 50;

                    ultraGroupBox7.Enabled=false;
                    grpDosTresCuatro.Enabled=false;
                    grpCincoSeis.Enabled=false;
                    grpSieteOchoNueve.Enabled=false;
                    groupBox31.Enabled=false;
                    grpTrceCatorce.Enabled = false;

                }
                //añade a los controles textbox el evento de keypress
                foreach (Control control in pantab1.Controls)
                {
                    if (control.Controls.Count > 0)
                        recorerControles(control);
                    else
                    {
                        if (control is TextBox)
                        {
                            control.KeyPress += new KeyPressEventHandler(keypressed);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            dtMedicamentos.Columns.Add("Codigo");
            dtMedicamentos.Columns.Add("Nombre");
            //dgv_Medicamentos.Rows.Add();

        }


        private void recorerControles(Control parControl)
        {
            try
            {
                foreach (Control control in parControl.Controls)
                {
                    if (control.Controls.Count > 0)
                        recorerControles(control);
                    else
                    {
                        if (control is TextBox)
                        {
                            control.KeyPress += new KeyPressEventHandler(keypressed);
                        }
                    }

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false,true, false, false,true,false,false);
            
            ultraGroupBox7.Enabled = true;
            grpDosTresCuatro.Enabled = true;
            grpCincoSeis.Enabled = true;
            grpSieteOchoNueve.Enabled = true;
            groupBox31.Enabled = true;
            grpTrceCatorce.Enabled = true;

            activarFormulario(true);
            emergenciaActual = null;
            if(txtRegistro.Text.Trim() != "" && txtRegistro.Text.Trim() != null)
                txtRegistro.Text = (Convert.ToInt32(txtRegistro.Text.Trim())+1).ToString();
        }

        private void chk_Trauma_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Trauma.Checked == true)
            {
                gpb_Accidentes.Enabled = true;
                gpb_Localizacion.Enabled = true;
                chk_CausaGOb.Checked = false;
                chk_CausaC.Checked = false;
                chk_CausaQuir.Checked = false;
                txt_OtroMotivo.Enabled = true;
                LimpiarCamposObstetricos();

                txt_OtroMotivo.Enabled = false;
                txt_OtroMotivo.Text = "";
                this.chk_OtroMotivo.Checked = false;

                if(emergenciaActual != null)
                    cargarLesionesAtencion();          
            }
            else
            {
                LimpiarCamposAccidentes();
            }
        }

        private void chk_CausaGOb_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_CausaGOb.Checked == true)
            {
                gpb_Obstetrica.Enabled = true;
                chk_Trauma.Checked = false;
                chk_CausaC.Checked = false;
                chk_CausaQuir.Checked = false;
                LimpiarCamposAccidentes();
                //if (emergenciaActual != null)
                    CargarEmergenciaObstetrica();

                txt_OtroMotivo.Enabled = false;
                txt_OtroMotivo.Text = "";
                this.chk_OtroMotivo.Checked = false;

            }else
            {
                LimpiarCamposObstetricos();
            }
        }

        private void chk_CausaC_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_CausaC.Checked == true)
            {
                chk_Trauma.Checked = false;
                chk_CausaGOb.Checked = false;
                chk_CausaQuir.Checked = false;
                LimpiarCamposAccidentes();
                LimpiarCamposObstetricos();

                txt_OtroMotivo.Enabled = false;
                txt_OtroMotivo.Text = "";
                this.chk_OtroMotivo.Checked = false;
            }
        }

        private void chk_CausaQuir_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_CausaQuir.Checked == true)
            {
                chk_Trauma.Checked = false;
                chk_CausaGOb.Checked = false;
                chk_CausaC.Checked = false;
                LimpiarCamposAccidentes();
                LimpiarCamposObstetricos();

                txt_OtroMotivo.Enabled = false;
                txt_OtroMotivo.Text = "";
                this.chk_OtroMotivo.Checked = false;

            }
        }

        private void chk_ViaAL_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ViaAL.Checked == true)
            {
                chk_ViaAO.Checked = false;
            }

        }

        private void chk_ViaAO_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ViaAO.Checked == true)
            {
                chk_ViaAL.Checked = false;
            }
        }

        private void chk_CondE_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_CondE.Checked == true)
            {
                chk_CondI.Checked = false;
            }
        }

        private void chk_CondI_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_CondI.Checked == true)
            {
                chk_CondE.Checked = false;
            }
        }

        private void cmb_Destino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Destino.Text == "DOMICILIO")
            {
                //txt_servicioReferencia.Enabled = true;
                //txt_servicioReferencia.ReadOnly = false;
                //txt_Establecimiento.ReadOnly = true;

                //txt_servicioReferencia.ReadOnly = false;
                //txt_Establecimiento.ReadOnly = true;

                txt_servicioReferencia.Enabled = false;
                txt_Establecimiento.Enabled = false;

                //txt_servicioReferencia.Focus();
                txt_Establecimiento.Text = "";
                txt_servicioReferencia.Text = "";
            }
            else 
            {
                txt_servicioReferencia.ReadOnly = false;
                txt_Establecimiento.ReadOnly = false;

                txt_servicioReferencia.Enabled = true;
                txt_Establecimiento.Enabled = true;

                txt_servicioReferencia.Focus();
                txt_Establecimiento.Text = "";
            }
            //}else
            //{
            //    if (cmb_Destino.SelectedIndex == 4)
            //    {
            //        //txt_Establecimiento.Enabled = true;
            //        txt_Establecimiento.ReadOnly = false;
            //        txt_servicioReferencia.ReadOnly = true;

            //        txt_Establecimiento.Enabled = false;
            //        txt_servicioReferencia.Enabled = true;

            //        txt_Establecimiento.Focus();
            //        txt_servicioReferencia.Text = "";
            //    }
            //    else 
            //    {
            //        txt_Establecimiento.ReadOnly = true;
            //        txt_servicioReferencia.ReadOnly = true;

            //        txt_Establecimiento.Enabled = true;
            //        txt_servicioReferencia.Enabled = true;

            //        txt_Establecimiento.Text = "";
            //        txt_servicioReferencia.Text = "";
            //    }
            //}
        }

        private void chk_EgresaVivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_EgresaVivo.Checked == true)
            {
                gpb_EgresaV.Enabled = true;
                gpb_MuertoE.Enabled = false;
                chk_MuertoE.Checked = false;
                txt_CausaMuerte.Text = "";
            }
        }

        private void chk_MuertoE_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_MuertoE.Checked == true)
            {
                gpb_MuertoE.Enabled = true;
                gpb_EgresaV.Enabled = false;
                chk_EgresaVivo.Checked = false;
                txt_CausaMuerte.Focus();
                chk_Estable.Checked = false;
                chk_Inestable.Checked = false;
                txt_DiasInc.Text = "";
                cmb_Destino.Enabled = false;

                txt_servicioReferencia.Text = "";
                txt_Establecimiento.Text = "";

                txt_servicioReferencia.Enabled = false;
                txt_Establecimiento.Enabled = false;

            }

            if (chk_MuertoE.Checked == false)
            {
                gpb_MuertoE.Enabled = false;
                gpb_EgresaV.Enabled = true;
                chk_EgresaVivo.Checked = true;
                //txt_CausaMuerte.Focus();
                chk_Estable.Checked = true;
                chk_Inestable.Checked = true;
                //txt_DiasInc.Text = "";
                cmb_Destino.Enabled = true;

                if (cmb_Destino.Text != "DOMICILIO")
                {
                    txt_servicioReferencia.Enabled = true;
                    txt_Establecimiento.Enabled = true;
                }

            }

        }

        private void chk_Estable_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Estable.Checked == true)
            {
                chk_Inestable.Checked = false;
                //txt_DiasInc.Text = "";
                txt_DiasInc.Focus();
            }

        }

        private void chk_Inestable_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Inestable.Checked == true)
            {
                chk_Estable.Checked = false;
                //txt_DiasInc.Text = "";
                txt_DiasInc.Focus();
            }
        }


        private void dtg_DiagnosticosAlta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {
                    DataGridViewRow fila = dtg_DiagnosticosAlta.CurrentRow;
                    fila.Cells[2].Value = busqueda.codigo;
                    fila.Cells[1].Value = busqueda.resultado;
                }
                dtg_DiagnosticosAlta.Focus();
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (dtg_DiagnosticosAlta.CurrentRow != null)
                {
                    if (dtg_DiagnosticosAlta.CurrentRow.Cells["CodigoDA"].Value != null)
                    {
                        Int32 codigoDetDiag = Convert.ToInt32(dtg_DiagnosticosAlta.CurrentRow.Cells["CodigoDA"].Value);
                        NegHcEmergenciaFDetalle.eliminarDiagnosticoDetalle(codigoDetDiag);
                        dtg_DiagnosticosAlta.Rows.Remove(dtg_DiagnosticosAlta.CurrentRow);
                        MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dtg_DiagnosticosAlta.Rows.Remove(dtg_DiagnosticosAlta.CurrentRow);
                        MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void dtg_ExamenFisico_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == this.dtg_ExamenFisico.Columns[2].Index)
            {
                DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_ExamenFisico.Rows[e.RowIndex].Cells[2];
                if (chkCell.Value == null)
                    chkCell.Value = false;
                else
                    chkCell.Value = true;

                DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_ExamenFisico.Rows[e.RowIndex].Cells[4];
                txtcell.ReadOnly = false;
                DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_ExamenFisico.Rows[e.RowIndex].Cells[3];
                chkCell2.Value = false;
            }
            else
            {
                if (e.ColumnIndex == this.dtg_ExamenFisico.Columns[3].Index)
                {
                    DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_ExamenFisico.Rows[e.RowIndex].Cells[3];
                    if (chkCell.Value == null)
                        chkCell.Value = false;
                    else
                        chkCell.Value = true;

                    DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_ExamenFisico.Rows[e.RowIndex].Cells[4];
                    txtcell.ReadOnly = true;
                    txtcell.Value = "";
                    DataGridViewCheckBoxCell chkCell2 = (DataGridViewCheckBoxCell)this.dtg_ExamenFisico.Rows[e.RowIndex].Cells[2];
                    chkCell2.Value = false;
                }
            }
        }

        private void dtg_ExamenFisico_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_ExamenFisico.CurrentRow != null)
                        {
                            if (dtg_ExamenFisico.CurrentRow.Cells["codigoEFD"].Value != null)
                            {
                                Int32 codigoExamen = Convert.ToInt32(dtg_ExamenFisico.CurrentRow.Cells["codigoEFD"].Value);
                                NegHcEmergenciaFDetalle.eliminarEFisico(codigoExamen);
                                dtg_ExamenFisico.Rows.Remove(dtg_ExamenFisico.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }else
                            {
                                dtg_ExamenFisico.Rows.Remove(dtg_ExamenFisico.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgv_ExamenesS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgv_ExamenesS.Rows[e.RowIndex].Cells[1].Value != null)
            //{
            //    if (dgv_ExamenesS.Rows[e.RowIndex].Cells[1].Value.ToString() == "BIOMETRIA" || dgv_ExamenesS.Rows[e.RowIndex].Cells[1].Value.ToString() == "GASOMETRÍA" ||
            //        dgv_ExamenesS.Rows[e.RowIndex].Cells[1].Value.ToString() == "ELECTRO CARDIOGRAMA" || dgv_ExamenesS.Rows[e.RowIndex].Cells[1].Value.ToString() == "ECOGRAFÍA PÉLVICA" || 
            //        dgv_ExamenesS.Rows[e.RowIndex].Cells[1].Value.ToString() == "ECOGRAFÍA ABDOMEN")
            //    {
            //        dgv_ExamenesS.Rows[e.RowIndex].Cells[2].Value = "";
            //        dgv_ExamenesS.Rows[e.RowIndex].Cells[2].ReadOnly = true;                    
            //    }
            //    else
            //    {
            //        dgv_ExamenesS.Rows[e.RowIndex].Cells[2].ReadOnly = false;             
            //    }
            //}
        }

        private void cmb_Ocular_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_TotalG.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.SelectedValue) + Convert.ToInt32(cmb_Motora.SelectedValue) + Convert.ToInt32(cmb_Verbal.SelectedValue));
            calcularREMS();
        }

        private void cmb_Verbal_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_TotalG.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.SelectedValue) + Convert.ToInt32(cmb_Motora.SelectedValue) + Convert.ToInt32(cmb_Verbal.SelectedValue));
            calcularREMS();
        }

        private void cmb_Motora_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_TotalG.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.SelectedValue) + Convert.ToInt32(cmb_Motora.SelectedValue) + Convert.ToInt32(cmb_Verbal.SelectedValue));
            calcularREMS();
        }

        private void txt_TotalG_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txt_PresionA1_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txt_FCardiaca_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txt_FResp_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txt_TBucal_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txt_TAxilar_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txt_SaturaO_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }


        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            try
            {
                frm_AyudaPacientes form = new frm_AyudaPacientes();
                form.campoAtencion = txt_Atencion;
                form.campoPadre = txt_historiaclinica;
                form.ShowDialog();
                if (txt_Atencion.Text.Trim() != null && txt_Atencion.Text.Trim() != "")
                {
                    //atencionActual = NegHcEmergenciaForm.RecuperarAtencionIDEmerg(Convert.ToInt32(txt_Atencion.Text.Trim()));
                    //cargarAtencion(atencionActual.ATE_CODIGO);
                    txtRegistro.Text = "";
                    cargarAtencion(Convert.ToInt32(txt_Atencion.Text.Trim()));
                    actualizarPaciente();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_TBucal_TextChanged(object sender, EventArgs e)
        {
            if (txt_TBucal.Text != "" || txt_TBucal.Text == "0")
            {
                bool estado;
                if (Convert.ToDecimal(txt_TBucal.Text) <= 0)
                    estado = true;
                else
                    estado = false;
                txt_TAxilar.Enabled = estado;
            }
            else { txt_TBucal.Enabled = true; }

            if (txt_TBucal.Text == "")
            {
                txt_TBucal.Text = "0";
            }

        }

        private void txt_TAxilar_TextChanged(object sender, EventArgs e)
        {
            if (txt_TAxilar.Text != "" || txt_TAxilar.Text == "0")
            {
                bool estado;
                if (Convert.ToDecimal(txt_TAxilar.Text) <= 0)
                    estado = true;
                else
                    estado = false;
                txt_TBucal.Enabled = estado;
            }
            else { txt_TAxilar.Enabled = true; }

            if (txt_TAxilar.Text == "")
            {
                txt_TAxilar.Text = "0";
            }

        }

        private void txt_SaturaO_TextChanged(object sender, EventArgs e)
        {
            if (txt_SaturaO.Text != "" || txt_SaturaO.Text == "0")
            {
                bool estado;
                if (Convert.ToInt32(txt_SaturaO.Text) > 100)
                    txt_SaturaO.Text = txt_SaturaO.Text.Substring(0, 2);
            }

            if (txt_SaturaO.Text == "")
            {
                txt_SaturaO.Text = "0";
            }

        }

        private void txt_DiamPDV_TextChanged(object sender, EventArgs e)
        {
            if (txt_DiamPDV.Text != "" || txt_DiamPDV.Text == "0")
            {
                bool estado;
                if (Convert.ToInt32(txt_DiamPDV.Text) > 8 || Convert.ToInt32(txt_DiamPDV.Text) < 1)
                    txt_DiamPDV.Text = "";
            }
        }

        private void txt_DiamPIV_TextChanged(object sender, EventArgs e)
        {
            if (txt_DiamPIV.Text != "" || txt_DiamPIV.Text == "0")
            {
                bool estado;
                if (Convert.ToInt32(txt_DiamPIV.Text) > 8 || Convert.ToInt32(txt_DiamPIV.Text) < 1)
                    txt_DiamPIV.Text = "";
            }
        }

        private void txt_DiamPDV_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
            //if (Char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (Char.IsSeparator(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void txt_DiamPIV_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
            //if (Char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else if (Char.IsSeparator(e.KeyChar))
            //{
            //    e.Handled = false;
            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarEmergencia();
            //MessageBox.Show("Registro Guardado", "FORM. 008", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
            //HabilitarBotones(false,true, true, false,true);
            //accion = "UPDATE";
            //cargarDetalles();
            //cargarDiagnosticos();
            //cargarTratamientosMedicamentos();
        }


        private bool ValidarCampos()
        {
            error.Clear();
            bool accion = false;

            if (chk_Trauma.Checked == false && chk_CausaC.Checked == false && chk_CausaGOb.Checked == false && chk_CausaQuir.Checked == false && chk_OtroMotivo.Checked == false)
            {
                AgregarError(chk_Trauma);
                AgregarError(chk_CausaC);
                AgregarError(chk_CausaGOb);
                AgregarError(chk_CausaQuir);
                AgregarError(chk_OtroMotivo);
                accion = true;
            }

            if(chk_Trauma.Checked == true)
            {
                /*LA FECHA DEL EVENTO PUEDE SER MENOR A LA FECHA DE INGRESO*/
                //if (dtp_FechaHoraEvento.Value < atencionActual.ATE_FECHA_INGRESO.Value)
                //{
                //    AgregarError(dtp_FechaHoraEvento);
                //    accion = true;
                //}
                if(cmb_LugarEvento.SelectedIndex <0 )
                {
                    AgregarError(cmb_LugarEvento);
                    accion = true;
                }
                if(txt_DireccionEvento.Text.Trim()== "")
                {
                    AgregarError(cmb_LugarEvento);
                    accion = true;
                }
                if (cmb_Accidentes.SelectedIndex < 0)
                {
                    AgregarError(cmb_Accidentes);
                    accion = true;
                }
                if (txt_ObservacionAccidente.Text.Trim() == "")
                {
                    AgregarError(txt_ObservacionAccidente);
                    accion = true;
                }
            }

            if (chk_ViaAL.Checked == false && chk_ViaAO.Checked == false)
            {
                AgregarError(chk_ViaAL);
                AgregarError(chk_ViaAO);
                accion = true;
            }
            else
            {
                if (chk_ViaAL.Checked == true)
                {
                    if (chk_CondE.Checked == false && chk_CondI.Checked == false)
                    {
                        AgregarError(chk_CondE);
                        AgregarError(chk_CondI);
                        accion = true;
                    }
                }
                //chk_ViaAO

                if (chk_ViaAO.Checked == true)
                {
                    if (chk_CondE.Checked == false && chk_CondI.Checked == false)
                    {
                        AgregarError(chk_CondE);
                        AgregarError(chk_CondI);
                        accion = true;
                    }
                }
            }
            if (txt_EnfermedadActual.Text.Trim()== "")
            {
                AgregarError(txt_EnfermedadActual);
                accion = true;
            }

            if (txt_PresionA1.Text.Trim() == "")
            {
                AgregarError(txt_PresionA1);
                accion = true;
            }
            if (txt_PresionA2.Text.Trim() == "")
            {
                AgregarError(txt_PresionA2);
                accion = true;
            }
            if (txt_FCardiaca.Text.Trim() == "")
            {
                AgregarError(txt_FCardiaca);
                accion = true;
            }
            if (txt_FResp.Text.Trim() == "")
            {
                AgregarError(txt_FResp);
                accion = true;
            }
            if (txt_TBucal.Text.Trim() == "" && txt_TAxilar.Text.Trim() == "")
            {       
                AgregarError(txt_TBucal);
                AgregarError(txt_TAxilar);
                accion = true;              
            }
            if (txt_SaturaO.Text.Trim() == "")
            {
                AgregarError(txt_SaturaO);                
                accion = true;
            }

            if (dtg_DiagnosticosI.RowCount > 0)
            {
                for (int i = 0; i < dtg_DiagnosticosI.RowCount-1; i++)
                {
                    if (dtg_DiagnosticosI.Rows[i].Cells[1].Value == null || dtg_DiagnosticosI.Rows[i].Cells[1].Value == "" || dtg_DiagnosticosI.Rows[i].Cells[2].Value == null || dtg_DiagnosticosI.Rows[i].Cells[2].Value == "")
                    {
                        /*CUANDO SE INGRESA UN DIAGNOSTICO DE INGRESO EL CODIGO NO ES NECESARIO / GIOVANNY TAPIA / 02/04/2013*/
                        //AgregarError(dtg_DiagnosticosI);
                        //accion = true;                    
                    }                
                }
            }

            if (dtg_DiagnosticosAlta.RowCount > 0)
            {
                for (int i = 0; i < dtg_DiagnosticosAlta.RowCount-1; i++)
                {
                    if (dtg_DiagnosticosAlta.Rows[i].Cells[1].Value == null || dtg_DiagnosticosAlta.Rows[i].Cells[1].Value == "" || dtg_DiagnosticosAlta.Rows[i].Cells[2].Value == null || dtg_DiagnosticosAlta.Rows[i].Cells[2].Value == "")
                    {
                        AgregarError(dtg_DiagnosticosAlta);
                        accion = true;
                    }
                }
            }

            /*GIOVANNY TAPIA / AGREGO VALIDACIONES A LA FORMA / 11/03/2013*/
            if (this.chk_OtroMotivo.Checked==true)
            {
                if (txt_OtroMotivo.Text == "")
                {
                    AgregarError(txt_OtroMotivo);
                    accion = true;
                }
            }

            if (txt_DiamPDV.Text == "")
            {
                AgregarError(txt_DiamPDV);
                accion = true;           
            }

            if (txt_DiamPIV.Text == "")
            {
                AgregarError(txt_DiamPIV);
                accion = true;
            }

            if (dtg_DiagnosticosAlta.Rows.Count > 1)
            {
                int i=0;
                for (i = 0; i < dtg_DiagnosticosAlta.Rows.Count-1; i++)
                {
                    if (dtg_DiagnosticosAlta.Rows[i].Cells[1].Value.ToString() == "" || dtg_DiagnosticosAlta.Rows[i].Cells[2].Value.ToString() == "" || (Convert.ToBoolean(dtg_DiagnosticosAlta.Rows[i].Cells[3].Value) == false && Convert.ToBoolean(dtg_DiagnosticosAlta.Rows[i].Cells[4].Value) == false))
                    {
                        AgregarError(dtg_DiagnosticosAlta);
                        accion = true;  
                    }
                }
            }
            else
            {
                AgregarError(dtg_DiagnosticosAlta);
                accion = true;            
            }


            //if (dgv_Indicaciones.Rows.Count > 1)/*  este campo no es obligatorio / Giovanny Tapia / 01/04/2013  */
            //{
            //    int i = 0;
            //    for (i = 0; i < dtg_DiagnosticosAlta.Rows.Count - 1; i++)
            //    {
            //        if (dgv_Indicaciones.Rows[i].Cells[1].Value==null)
            //        {
            //            AgregarError(dgv_Indicaciones);
            //            accion = true;
            //        }
            //    }
            //}
            //else
            //{
            //    AgregarError(dgv_Indicaciones);
            //    accion = true;
            //}

            if (chk_EgresaVivo.Checked == false && chk_MuertoE.Checked == false)
            {
                AgregarError(chk_EgresaVivo);
                AgregarError(chk_MuertoE);
                accion = true;                
            }

            if (chk_EgresaVivo.Checked==true) 
            {
                if (cmb_Destino.Text == "")
                { 
                    AgregarError(cmb_Destino);
                    accion = true;
                }
                if (txt_servicioReferencia.Enabled==true && txt_servicioReferencia.Text == "")
                { 
                    AgregarError(txt_servicioReferencia);
                    accion = true;
                }

                if (txt_Establecimiento.Enabled == true && txt_Establecimiento.Text == "")
                {
                    AgregarError(txt_Establecimiento);
                    accion = true;
                }

                
                if (txt_DiasInc.Text == "")
                {
                    AgregarError(txt_DiasInc);
                    accion = true;
                }

                if (chk_Estable.Checked == false && chk_Inestable.Checked == false)
                {
                    AgregarError(chk_Estable);
                    AgregarError(chk_Inestable);
                    accion = true;
                }

            }

            if (chk_MuertoE.Checked == true)
            {
                if (txt_CausaMuerte.Text == "")
                {
                    AgregarError(txt_CausaMuerte);
                    accion = true;
                }            
            }

            if (txt_horaAltaEmerencia.Text == "  :")
            {
                AgregarError(txt_horaAltaEmerencia);
                accion = true;
            }

            if (txt_profesionalEmergencia.Text == "")
            {
                AgregarError(txt_profesionalEmergencia);
                accion = true;
            }

            if (txt_CodMSPE.Text == "")
            {
                AgregarError(txt_CodMSPE);
                accion = true;
            }

            return accion;

        }


        private Boolean validarAlta()
        {
            Boolean estado = true;
            if (dtpFechaAlta.Value > dtpFechaIngreso.Value)
                estado = false;
            else
            {
                if (dtpFechaAlta.Value.Hour > dtpFechaIngreso.Value.Hour)
                    estado = false;
                else
                {
                    if (dtpFechaAlta.Value.Minute > dtpFechaIngreso.Value.Minute)

                        estado = false;
                    else
                        estado = true;
                }
            }
            return estado;
        }

        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");

        }

        private void txt_GrupoS_Enter(object sender, EventArgs e)
        {

        }

        private void txt_PresionA1_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_PresionA2_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_PresionA2_GotFocus(Object sender, EventArgs e)
        {

            txt_PresionA2.SelectAll();

        }

        private void txt_FCardiaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_FResp_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_TBucal_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodofuncionTeclitas(sender, e, txt_TBucal);
        }

        private void txt_TAxilar_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodofuncionTeclitas(sender, e, txt_TAxilar);
        }


        private void MiMetodoNumeros(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void MiMetodofuncionTeclitas(object sender, KeyPressEventArgs e, TextBox texto)
        {
            if (texto.Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                //REGISTRO DE ADMISION

                PACIENTES_DATOS_ADICIONALES datosAdic = new PACIENTES_DATOS_ADICIONALES();
                datosAdic = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);
                ReporteForm008E hoja008 = new ReporteForm008E();
                hoja008.FOR_IS = "HOSPITAL DE CLINICAS PICHINCHA ";
                hoja008.FOR_UO = "EMERGENCIAS";
                hoja008.FOR_PARR = Convert.ToString(170101);
                hoja008.FOR_CAT = Convert.ToString(1701);
                hoja008.FOR_PROV = Convert.ToString(17);
                hoja008.FOR_HISTO = txt_historiaclinica.Text + "-" + atencionActual.ATE_CODIGO;
                hoja008.FOR_APEUNO = txt_ApellidoH1.Text;
                hoja008.FOR_APEDOS= txt_ApellidoH2.Text;
                hoja008.FOR_NOMUNO = txt_NombreH1.Text;
                hoja008.FOR_NOMDOS = txt_NombreH2.Text;
                hoja008.FOR_CEDULA = pacienteActual.PAC_IDENTIFICACION;
                hoja008.FOR_DIRECCION = txt_direccionP.Text;
                hoja008.FOR_BARRIO = datosAdic.COD_SECTOR;
                hoja008.FOR_PARROQUIA = datosAdic.COD_PARROQUIA;
                hoja008.FOR_CANTON = datosAdic.COD_CANTON;
                hoja008.FOR_PROVINCIA = datosAdic.COD_PROVINCIA;
                hoja008.FOR_ZONAU = "X";
                hoja008.FOR_TELEFONO = txt_telefono1.Text;
                hoja008.FOR_FECHAN = (dtpFecNacimiento.Text.Trim() != string.Empty ? Convert.ToDateTime(dtpFecNacimiento.Text) : Convert.ToDateTime("1900/01/01")).ToString().Substring(0,10);
                hoja008.FOR_LUGNAC = datosAdic.DAP_EMP_CIUDAD;
                hoja008.FOR_NACIONAL = pacienteActual.PAC_NACIONALIDAD;
                hoja008.FOR_GRUPCULT = cb_estadoCivil.Text;
                hoja008.FOR_EDADA = Funciones.CalcularEdad(Convert.ToDateTime(dtpFecNacimiento.Text)).ToString();
                string genero = pacienteActual.PAC_GENERO;
                if (genero.Equals("F"))
                    hoja008.FOR_GF = "X";
                else
                    hoja008.FOR_GM = "X";
                string estadoC = cb_estadoCivil.Text;
                if (estadoC.Equals("SOLTERO"))
                    hoja008.FOR_ECSOL = "X";
                else
                    if (estadoC.Equals("CASADO"))
                        hoja008.FOR_ECCAS = "X";
                    else
                        if (estadoC.Equals("DIVORCIADO"))
                            hoja008.FOR_ECDIV = "X";
                        else
                            if (estadoC.Equals("VIUDO"))
                                hoja008.FOR_ECVIU = "X";
                            else
                                if (estadoC.Equals("UNION LIBRE"))
                                    hoja008.FOR_ECUL = "X";

                hoja008.FOR_INSTRUC = datosAdic.DAP_INSTRUCCION;
                hoja008.FOR_FECHADM = (atencionActual.ATE_FECHA_INGRESO.Value).ToString().Substring(0, 10); ;
                hoja008.FOR_OCUPACION = datosAdic.DAP_OCUPACION;
                hoja008.FOR_EMPRESAT = datosAdic.DAP_EMP_NOMBRE;
                hoja008.FOR_SEGURO = Convert.ToString(datosAdic.EMP_CODIGO);
                List<TIPO_REFERIDO> tipoReferido = new List<TIPO_REFERIDO>();
                hoja008.FOR_REFERIDO = atencionActual.ATE_REFERIDO == false ? "NSTITUCIONA" : "PRIVADO";
                hoja008.FOR_AVISARA = pacienteActual.PAC_REFERENTE_NOMBRE != null ? pacienteActual.PAC_REFERENTE_NOMBRE.Trim() : "";
                hoja008.FOR_PARENTESCO = pacienteActual.PAC_REFERENTE_PARENTESCO != null ? pacienteActual.PAC_REFERENTE_PARENTESCO.Trim() : "";
                hoja008.FOR_PARDIREC= pacienteActual.PAC_REFERENTE_DIRECCION != null ? pacienteActual.PAC_REFERENTE_DIRECCION.Trim() : "";
                hoja008.FOR_PARTELEFONO = Convert.ToString(pacienteActual.PAC_REFERENTE_TELEFONO);
                if (txtFormaLlegada.Text.Trim().Equals("AMBULATORIO"))
                    hoja008.FOR_AMBULATORIA = "X";
                else
                    if (txtFormaLlegada.Text.Trim().Equals("AMBULANCIA"))
                        hoja008.FOR_AMBULANCIA = "X";
                    else
                        if (txtFormaLlegada.Text.Trim().Equals("OTROS") || txtFormaLlegada.Text.Trim().Equals("TRANSFERENCIA"))
                            hoja008.FOR_OTROTRANS = "X";

                hoja008.FOR_FINF = "";
                hoja008.FOR_INSTPERSO = "";
                hoja008.FOR_INSTTELEFONO = "";
                //hoja008.FOR_IAM_HORA = Convert.ToDateTime(dtFecIngreso.Text);
                GRUPO_SANGUINEO grupoS = NegGrupoSanguineo.RecuperarGrupoSanguineoID(Convert.ToInt16(pacienteActual.GRUPO_SANGUINEOReference.EntityKey.EntityKeyValues[0].Value));
                if (grupoS.GS_NOMBRE.Equals("vacio"))
                    hoja008.FOR_IAM_GSF = "";
                else
                    hoja008.FOR_IAM_GSF = grupoS.GS_NOMBRE;
                
                CATEGORIAS_CONVENIOS categoria = new CATEGORIAS_CONVENIOS();
                List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(atencionActual.ATE_CODIGO);
                ATENCION_DETALLE_CATEGORIAS at = new ATENCION_DETALLE_CATEGORIAS();
                if (detalleCategorias != null)
                {
                    at = detalleCategorias[0];
                    categoria = NegCategorias.RecuperaCategoriaID(Convert.ToInt16(at.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value));
                    hoja008.FOR_SEGURO = categoria.CAT_NOMBRE;
                }


                //INICIO DE ATENCION Y MOTIVO

                hoja008.FOR_IAM_HORA = txt_HoraI.Text.Trim();
                hoja008.FOR_IAM_TRAUMA = chk_Trauma.Checked == true ? "X" : "";
                hoja008.FOR_IAM_CCLINICA = chk_CausaC.Checked == true ? "X" : ""; ;
                hoja008.FOR_IAM_COBSTET = chk_CausaGOb.Checked == true ? "X" : ""; ;
                hoja008.FOR_IAM_CQUIR = chk_CausaQuir.Checked == true ? "X" : ""; ;
                hoja008.FOR_IAM_NPOLICIA = chk_NotPolicia.Checked == true ? "X" : ""; ;
                hoja008.FOR_IAM_OMOTIVO = txt_OtroMotivo.Text.Trim();
                
                if (txt_OtroMotivo.Text != "")
                {
                    hoja008.FOR_IAM_OMOT = "X";
                }
                else
                {
                    hoja008.FOR_IAM_OMOT = "";
                }

                hoja008.FOR_IAM_GSF = txt_GSanguineo.Text;

                //ACCIDENTE, VIOLENCIA, INTOXICACION, ENVENENAMIENTO O QUEMADURA

                if (chk_Trauma.Checked == true)
                {
                    hoja008.FOR_AVIE_FHEVEN = dtp_FechaHoraEvento.Value.ToString().Substring(0, 17);
                    hoja008.FOR_AVIE_LUGEV = cmb_LugarEvento.Text;
                    hoja008.FOR_AVIE_DEVEN = txt_DireccionEvento.Text;
                    hoja008.FOR_AVIE_CPOLICIAL = chk_CustodiaPolicial.Checked == true ? "X" : "";
                    hoja008.FOR_AVIE_VALCOH = chb_ValorAlcocheck.Checked == true ? "X" : "";
                    hoja008.FOR_AVIE_AETILICO = chb_AlientoEtilico.Checked == true ? "X" : "";
                    hoja008.FOR_AVIE_AT = cmb_Accidentes.SelectedIndex == 3 ? "X" : "";
                    hoja008.FOR_AVIE_VAR = cmb_Accidentes.SelectedIndex == 23 ? "X" : "";
                    hoja008.FOR_AVIE_IAL = cmb_Accidentes.SelectedIndex == 11 ? "X" : "";
                    hoja008.FOR_AVIE_CAIDA = cmb_Accidentes.SelectedIndex == 7 ? "X" : "";
                    hoja008.FOR_AVIE_VAARM = cmb_Accidentes.SelectedIndex == 22 ? "X" : "";
                    hoja008.FOR_AVIE_IALI = cmb_Accidentes.SelectedIndex == 12 ? "X" : "";
                    hoja008.FOR_AVIE_QUEM = cmb_Accidentes.SelectedIndex == 19 ? "X" : "";
                    hoja008.FOR_AVIE_VRI = cmb_Accidentes.SelectedIndex == 20 ? "X" : "";
                    hoja008.FOR_AVIE_INXD = cmb_Accidentes.SelectedIndex == 13 ? "X" : "";
                    hoja008.FOR_AVIE_MORDE = cmb_Accidentes.SelectedIndex == 14 ? "X" : "";
                    hoja008.FOR_AVIE_VF = cmb_Accidentes.SelectedIndex == 21 ? "X" : "";
                    hoja008.FOR_AVIE_IGASES = cmb_Accidentes.SelectedIndex == 10 ? "X" : "";
                    hoja008.FOR_AVIE_AHOGA = cmb_Accidentes.SelectedIndex == 4 ? "X" : "";
                    hoja008.FOR_AVIE_AF = cmb_Accidentes.SelectedIndex == 0 ? "X" : "";
                    hoja008.FOR_AVIE_OINT = cmb_Accidentes.SelectedIndex == 15 ? "X" : "";
                    hoja008.FOR_AVIE_CEXT = cmb_Accidentes.SelectedIndex == 8 ? "X" : "";
                    hoja008.FOR_AVIE_APSIC = cmb_Accidentes.SelectedIndex == 1 ? "X" : "";
                    hoja008.FOR_AVIE_ENVE = cmb_Accidentes.SelectedIndex == 9 ? "X" : "";
                    hoja008.FOR_AVIE_APLAST = cmb_Accidentes.SelectedIndex == 6 ? "X" : "";
                    hoja008.FOR_AVIE_ASEXUAL = cmb_Accidentes.SelectedIndex == 2 ? "X" : "";
                    hoja008.FOR_AVIE_PICAD = cmb_Accidentes.SelectedIndex == 18 ? "X" : "";
                    hoja008.FOR_AVIE_OACC = cmb_Accidentes.SelectedIndex == 17 ? "X" : "";
                    hoja008.FOR_AVIE_OVIOL = cmb_Accidentes.SelectedIndex == 16 ? "X" : "";
                    hoja008.FOR_AVIE_ANAFLAX = cmb_Accidentes.SelectedIndex == 5 ? "X" : "";
                    hoja008.FOR_AVIE_OBSERVA = "\t \t \t \t " + txt_ObservacionAccidente.Text.Trim() + "\t \t \t \t ";
                }
                else 
                {
                    hoja008.FOR_AVIE_FHEVEN = " ";
                    hoja008.FOR_AVIE_LUGEV = " ";
                    hoja008.FOR_AVIE_DEVEN = " ";
                    hoja008.FOR_AVIE_CPOLICIAL = " ";
                    hoja008.FOR_AVIE_VALCOH = " ";
                    hoja008.FOR_AVIE_AETILICO = " ";
                    hoja008.FOR_AVIE_OBSERVA = " ";
                }                                    

                //ANTECEDENTES PERSONALES Y FAMILIARES
                
                hoja008.FOR_APF_ALERG = "NO";                
                hoja008.FOR_APF_CLIN = "NO";
                hoja008.FOR_APF_GINEC = "NO";
                hoja008.FOR_APF_TRAUM = "NO";
                hoja008.FOR_APF_QUIR = "NO";
                hoja008.FOR_APF_FARM = "NO";
                hoja008.FOR_APF_PSQUI = "NO";
                hoja008.FOR_APF_OTRO = "NO";
                
                foreach (DataGridViewRow item in dtg_antec_personales.Rows)
                {
                    if (item.Cells[0].Value != null)
                    {
                        for (int i = 0; i < ant1.Count; i++)
                        {
                            DtoCatalogos cat= ant1.ElementAt(i);
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (i == 0)
                                    hoja008.FOR_APF_ALERG = i == 0 ? "SI" : " ";                               
                                if (i == 1)
                                    hoja008.FOR_APF_CLIN = i == 1 ? "SI" : " ";
                                if (i == 2)
                                    hoja008.FOR_APF_GINEC = i == 2 ? "SI" : " ";
                                if (i == 3)
                                    hoja008.FOR_APF_TRAUM = i == 3 ? "SI" : " ";                                    
                                if (i == 4)
                                    hoja008.FOR_APF_QUIR = i == 4 ? "SI" : " ";
                                if (i == 5)
                                    hoja008.FOR_APF_FARM = i == 5 ? "SI" : " ";
                                if (i == 6)
                                    hoja008.FOR_APF_PSQUI = i == 6 ? "SI" : " ";
                                if (i == 7)
                                    hoja008.FOR_APF_OTRO = i == 7 ? "SI" : " ";                                
                            }
                        }
                    }
                }

                string anteDescripcion = " ";
                for (int i = 0; i < ant1.Count; i++)
                {
                    DtoCatalogos cat = ant1.ElementAt(i);
                    foreach (DataGridViewRow item in dtg_antec_personales.Rows)
                    {                        
                        if (item.Cells[0].Value != null)
                        {
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (item.Cells[0].Value != null && item.Cells[1].Value != null)
                                    anteDescripcion += i + 1 + "- " + item.Cells[2].Value.ToString() + "\t\t";
                                break;
                            }
                        }                
                    }                    
                }
                hoja008.FOR_APF_DESCIPCION = anteDescripcion.Replace("'", "´");

                // ENFERMEDAD ACTUAL Y REVISION DE SISTEMAS

                hoja008.FOR_EARS_VAL = chk_ViaAL.Checked == true ? "X":"";
                hoja008.FOR_EARS_VAO = chk_ViaAO.Checked == true ? "X" : "";
                hoja008.FOR_EARS_CE = chk_CondE.Checked == true ? "X" : "";
                hoja008.FOR_EARS_CI = chk_CondI.Checked == true ? "X" : "";
                hoja008.FOR_EARS_DESCRIPCION = txt_EnfermedadActual.Text;

                // SIGNOS VITALES, MEDICIONES Y VALORES

                hoja008.FOR_SVMV_PAUNO = txt_PresionA1.Text;
                hoja008.FOR_SVMV_PADOS = txt_PresionA2.Text;
                hoja008.FOR_SVMV_FC = txt_FCardiaca.Text;
                hoja008.FOR_SVMV_FR = txt_FResp.Text;
                if(txt_TBucal.Text == "0")
                    hoja008.FOR_SVMV_TB = "-";
                else
                    hoja008.FOR_SVMV_TB = txt_TBucal.Text;
                if(txt_TAxilar.Text == "0")
                    hoja008.FOR_SVMV_TA = "-";
                else
                    hoja008.FOR_SVMV_TA = txt_TAxilar.Text;
                if (txt_PesoKG.Text == "0.00")
                    hoja008.FOR_SVMV_PESO = "-";
                else
                    hoja008.FOR_SVMV_PESO = txt_PesoKG.Text;
                if (txt_Talla.Text == "0.00")
                    hoja008.FOR_SVMV_TALLA = "-";
                else
                    hoja008.FOR_SVMV_TALLA = txt_Talla.Text;
                hoja008.FOR_SVMV_OCULAR = cmb_Ocular.SelectedItem.ToString();
                hoja008.FOR_SVMV_VERBAL = cmb_Verbal.SelectedItem.ToString();
                hoja008.FOR_SVMV_MOT = cmb_Motora.SelectedItem.ToString();
                hoja008.FOR_SVMV_TOT = txt_TotalG.Text.ToString();
                if(cmb_ReacPDValor.SelectedIndex == 0)
                    hoja008.FOR_SVMV_RPULD = "Ok";
                if (cmb_ReacPDValor.SelectedIndex == 1)
                    hoja008.FOR_SVMV_RPULD = "▼";
                if (cmb_ReacPDValor.SelectedIndex == 2)
                    hoja008.FOR_SVMV_RPULD = "No";
                
                    //txt_DiamPDV.Text.ToString();
                //hoja008.FOR_SVMV_RPULI = cmb_ReacPIValor.SelectedText;
                if (cmb_ReacPIValor.SelectedIndex == 0)
                    hoja008.FOR_SVMV_RPULI = "Ok";
                if (cmb_ReacPIValor.SelectedIndex == 1)
                    hoja008.FOR_SVMV_RPULI = "▼";
                if (cmb_ReacPIValor.SelectedIndex == 2)
                    hoja008.FOR_SVMV_RPULI = "No";
                    //txt_DiamPIV.Text.ToString();
                hoja008.FOR_SVMV_TCAP = txt_Glicemia.Text.ToString();
                hoja008.FOR_SVMV_SOXI = txt_SaturaO.Text.ToString();

                //EXAMEN FISICO Y DIAGNOSTICO 
                hoja008.FOR_EFD_VAO = "SP";
                hoja008.FOR_EFD_CABEZA = "SP";
                hoja008.FOR_EFD_CUELLO = "SP";
                hoja008.FOR_EFD_TORAX = "SP";
                hoja008.FOR_EFD_ABD = "SP";
                hoja008.FOR_EFD_COLUM = "SP";
                hoja008.FOR_EFD_PELVIS = "SP";
                hoja008.FOR_EFD_EXTREM = "SP";
                foreach (DataGridViewRow item in dtg_ExamenFisico.Rows)
                {
                    if (item.Cells[0].Value != null)
                    {
                        for (int i = 0; i < ant2.Count; i++)
                        {
                            DtoCatalogos cat = ant2.ElementAt(i);
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (i == 0)
                                {
                                    if ((bool)item.Cells[2].Value)
                                        hoja008.FOR_EFD_VAO = "CP";

                                }
                                if (i == 1)
                                {
                                    if ((bool)item.Cells[2].Value)                                    
                                        hoja008.FOR_EFD_CABEZA = "CP"; 
                                }
                                if (i == 2)
                                {
                                    if ((bool)item.Cells[2].Value)                                    
                                        hoja008.FOR_EFD_CUELLO = "CP"; 
 
                                }
                                if (i == 3)
                                {
                                    if ((bool)item.Cells[2].Value)                                    
                                        hoja008.FOR_EFD_TORAX = "CP"; 
                                }
                                if (i == 4)
                                {
                                    if ((bool)item.Cells[2].Value)                                    
                                        hoja008.FOR_EFD_ABD = "CP"; 
                                }
                                if (i == 5)
                                {
                                    if ((bool)item.Cells[2].Value)                                   
                                        hoja008.FOR_EFD_COLUM = "CP";
                                }
                                if (i == 6)
                                {
                                    if ((bool)item.Cells[2].Value)                                    
                                        hoja008.FOR_EFD_PELVIS = "CP";                                    
                                }
                                if (i == 7)
                                {
                                    if ((bool)item.Cells[2].Value)                                    
                                        hoja008.FOR_EFD_EXTREM = "CP";                                    
                                }
                                i = ant2.Count;
                            }
                        }
                    }
                }
                string examenFisicoDescripcion = " ";
                for (int i = 0; i < ant2.Count; i++)
                {
                    DtoCatalogos cat = ant2.ElementAt(i);
                    foreach (DataGridViewRow item in dtg_ExamenFisico.Rows)
                    {                        
                        if (item.Cells[0].Value != null)
                        {
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (item.Cells[0].Value != null && item.Cells[1].Value != null && Convert.ToBoolean(item.Cells[2].Value) != false )
                                    examenFisicoDescripcion += (i + 1) + "- " + item.Cells[4].Value.ToString() + "\t\t";
                                break;
                            }
                        }
                    }
                }
                
                hoja008.FOR_EFD_DESCRIPCION = examenFisicoDescripcion.Replace("'", "´");

                // LOCALIZACIÓN DE LESIONES
                if (chk_Trauma.Checked == true)
                {
                    foreach (DataGridViewRow item in dgv_LocalizacionL.Rows)
                    {
                        if (item.Cells[0].Value != null)
                        {
                            for (int i = 0; i < ant3.Count; i++)
                            {
                                DtoCatalogos cat = ant3.ElementAt(i);
                                if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                                {
                                    if (i == 0)
                                        hoja008.FOR_LL_HP = "X";
                                    if (i == 1)
                                        hoja008.FOR_LL_HC = "X";
                                    if (i == 2)
                                        hoja008.FOR_LL_FX = "X";
                                    if (i == 3)
                                        hoja008.FOR_LL_FC = "X";
                                    if (i == 4)
                                        hoja008.FOR_LL_CE = "X";
                                    if (i == 5)
                                        hoja008.FOR_LL_H = "X";
                                    if (i == 6)
                                        hoja008.FOR_LL_M = "X";
                                    if (i == 7)
                                        hoja008.FOR_LL_HC = "X";
                                    if (i == 8)
                                        hoja008.FOR_LL_E = "X";
                                    if (i == 9)
                                        hoja008.FOR_LL_DM = "X";
                                    if (i == 10)
                                        hoja008.FOR_LL_HEM = "X";
                                    if (i == 11)
                                        hoja008.FOR_LL_EI = "X";
                                    if (i == 12)
                                        hoja008.FOR_LL_LE = "X";
                                    if (i == 13)
                                        hoja008.FOR_LL_QUEM = "X";
                                    if (i == 14)
                                    {
                                        hoja008.FOR_LL_OTROLQ = "X";
                                        hoja008.FOR_LL_OTROL = txt_OtrasL.Text;
                                    }
                                    i = ant3.Count;
                                }
                            }
                        }
                    }
                }
                else 
                {
                    hoja008.FOR_LL_HP = " ";
                    hoja008.FOR_LL_HC = " ";
                    hoja008.FOR_LL_FX = " ";
                    hoja008.FOR_LL_FC = " ";
                    hoja008.FOR_LL_CE = " ";
                    hoja008.FOR_LL_H = " ";
                    hoja008.FOR_LL_M = " ";
                    hoja008.FOR_LL_HC = " ";
                    hoja008.FOR_LL_E = " ";
                    hoja008.FOR_LL_DM = " ";
                    hoja008.FOR_LL_HEM = " ";
                    hoja008.FOR_LL_EI = " ";
                    hoja008.FOR_LL_LE = " ";
                    hoja008.FOR_LL_QUEM = " ";
                    hoja008.FOR_LL_OTROLQ = " ";
                    hoja008.FOR_LL_OTROL = " ";
                }
                
                // EMERGENCIA OBSTETRICA
                if (chk_CausaGOb.Checked == true)
                {
                    hoja008.FOR_EO_GESTA = txt_Gesta.Text != "" ? txt_Gesta.Text : " ";
                    hoja008.FOR_EO_PARTOS = txt_Partos.Text != "" ? txt_Partos.Text : " ";
                    hoja008.FOR_EO_ABORTOS = txt_Abortos.Text != "" ? txt_Abortos.Text : " ";
                    hoja008.FOR_EO_CESAREAS = txt_Cesareas.Text != "" ? txt_Cesareas.Text : " ";
                    hoja008.FOR_EO_FUMESTRU = (dtp_ultimaMenst1.Value).ToString().Substring(0, 12);
                    hoja008.FOR_EO_SEMGEST = txt_SemanaG.Text != "" ? txt_SemanaG.Text : " ";
                    hoja008.FOR_EO_MFETAL = chk_MovimientoF.Checked == true ? "X" : " ";
                    hoja008.FOR_EO_FCF = txt_FrecCF.Text != "" ? txt_FrecCF.Text : " ";
                    hoja008.FOR_EO_MSROTAS = chk_MembranaS.Checked == true ? "X" : " ";
                    hoja008.FOR_EO_TIEMPO = txt_Tiempo.Text;
                    hoja008.FOR_EO_AU = txt_AltU.Text != "" ? txt_AltU.Text : " ";
                    hoja008.FOR_EO_PRESEN = txt_Presentacion.Text;
                    hoja008.FOR_EO_DILAT = txt_Dilatacion.Text != "" ? txt_Dilatacion.Text : " ";
                    hoja008.FOR_EO_BORRAM = txt_Borramiento.Text != "" ? txt_Borramiento.Text : " ";
                    hoja008.FOR_EO_PLANO = txt_Plano.Text;
                    hoja008.FOR_EO_PELV = chk_PelvisU.Checked == true ? "X" : " ";
                    hoja008.FOR_EO_SV = chk_SangradoV.Checked == true ? "X" : " ";
                    hoja008.FOR_EO_CONTRAC = txt_Contracciones.Text;
                    hoja008.FOR_EO_DESCRIPCION = txt_EmergenciaObt.Text;
                }
                else 
                {
                    hoja008.FOR_EO_GESTA = " ";
                    hoja008.FOR_EO_PARTOS = " ";
                    hoja008.FOR_EO_ABORTOS = " ";
                    hoja008.FOR_EO_CESAREAS = " ";
                    hoja008.FOR_EO_FUMESTRU = " ";
                    hoja008.FOR_EO_SEMGEST = " ";
                    hoja008.FOR_EO_MFETAL = " ";
                    hoja008.FOR_EO_FCF = " ";
                    hoja008.FOR_EO_MSROTAS = " ";
                    hoja008.FOR_EO_TIEMPO = " ";
                    hoja008.FOR_EO_AU = " ";
                    hoja008.FOR_EO_PRESEN = " ";
                    hoja008.FOR_EO_DILAT = " ";
                    hoja008.FOR_EO_BORRAM = " ";
                    hoja008.FOR_EO_PLANO = " ";
                    hoja008.FOR_EO_PELV =  " ";
                    hoja008.FOR_EO_SV = " ";
                    hoja008.FOR_EO_CONTRAC = " ";
                    hoja008.FOR_EO_DESCRIPCION = " ";
                }


                //SOLICITUD DE EXAMENES
                
                hoja008.FOR_SE_BIOM = "-";
                hoja008.FOR_SE_UR = "-";
                hoja008.FOR_SE_QUIM = "-";
                hoja008.FOR_SE_ELEC = "-";
                hoja008.FOR_SE_GAS = "-";
                hoja008.FOR_SE_EC = "-";
                hoja008.FOR_SE_END = "-";
                hoja008.FOR_SE_RXT = "-";
                hoja008.FOR_SE_RXA = "-";
                hoja008.FOR_SE_RXO = "-";
                hoja008.FOR_SE_TOM = "-";
                hoja008.FOR_SE_RES = "-";
                hoja008.FOR_SE_EP = "-";
                hoja008.FOR_SE_EA = "-";
                hoja008.FOR_SE_INT = "-";
                hoja008.FOR_SE_OTROS = "-";
                foreach (DataGridViewRow item in dgv_ExamenesS.Rows)
                {
                    if (item.Cells[0].Value != null)
                    {
                        for (int i = 0; i < ant4.Count; i++)
                        {
                            DtoCatalogos cat = ant4.ElementAt(i);
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (i == 0)                                
                                    hoja008.FOR_SE_BIOM = "X";                                  
                                if (i == 1) 
                                    hoja008.FOR_SE_UR = "X";                              
                                if (i == 2)                               
                                    hoja008.FOR_SE_QUIM = "X";                             
                                if (i == 3)                             
                                    hoja008.FOR_SE_ELEC = "X";                    
                                if (i == 4)                              
                                    hoja008.FOR_SE_GAS = "X";
                                if (i == 5)
                                    hoja008.FOR_SE_EC = "X";
                                if (i == 6)
                                    hoja008.FOR_SE_END = "X";
                                if (i == 7)
                                    hoja008.FOR_SE_RXT = "X";
                                if (i == 8)
                                    hoja008.FOR_SE_RXA = "X";
                                if (i == 9)
                                    hoja008.FOR_SE_RXO = "X";
                                if (i == 10)
                                    hoja008.FOR_SE_TOM = "X";
                                if (i == 11)
                                    hoja008.FOR_SE_RES = "X";
                                if (i == 12)
                                    hoja008.FOR_SE_EP = "X";
                                if (i == 13)
                                    hoja008.FOR_SE_EA = "X";
                                if (i == 14)
                                    hoja008.FOR_SE_INT = "X";
                                if (i == 15)
                                    hoja008.FOR_SE_OTROS = "X";
                                i = ant3.Count;     
                            }
                        }
                    }
                }
                
                string examenesSolicitados = " ";
                int posicion = 1;
                foreach (DataGridViewRow item in dgv_ExamenesS.Rows)
                {
                    if (item.Cells[0].Value != null && item.Cells[1].Value != null)
                    {
                        examenesSolicitados += posicion + "- " + item.Cells[2].Value.ToString() + "\t\t";                        
                    }
                    posicion++;
                }
                hoja008.FOR_SE_DESC = examenesSolicitados.Replace("'", "´");

                // DIAGNÓSTICOS DE INGRESO
                for (int i = 0; i < dtg_DiagnosticosI.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        hoja008.FOR_DI_UNOCIE = dtg_DiagnosticosI.Rows[0].Cells[2].Value.ToString();
                        hoja008.FOR_DI_UNO = (dtg_DiagnosticosI.Rows[0].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_DiagnosticosI.Rows[i].Cells[3].Value)
                            hoja008.FOR_DI_UNOPRE = dtg_DiagnosticosI.Rows[0].Cells[3].Value != null ? "X" : " ";
                        else
                            hoja008.FOR_DI_UNODEF = dtg_DiagnosticosI.Rows[0].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 1)
                    {
                        hoja008.FOR_DI_DOSCIE = dtg_DiagnosticosI.Rows[1].Cells[2].Value.ToString();
                        hoja008.FOR_DI_DOS = (dtg_DiagnosticosI.Rows[1].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_DiagnosticosI.Rows[i].Cells[3].Value)
                            hoja008.FOR_DI_DOSPRE = dtg_DiagnosticosI.Rows[1].Cells[3].Value != null ? "X" : " ";
                        else
                            hoja008.FOR_DI_DOSDEF = dtg_DiagnosticosI.Rows[1].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 2)
                    {
                        hoja008.FOR_DI_TRESCIE = dtg_DiagnosticosI.Rows[2].Cells[2].Value.ToString();
                        hoja008.FOR_DI_TRES = (dtg_DiagnosticosI.Rows[2].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_DiagnosticosI.Rows[i].Cells[3].Value)
                            hoja008.FOR_DI_TRESPRE = dtg_DiagnosticosI.Rows[2].Cells[3].Value != null ? "X" : " ";
                        else
                            hoja008.FOR_DI_TRESDEF = dtg_DiagnosticosI.Rows[2].Cells[4].Value != null ? "X" : " ";
                    }                    
                }

                // DIAGNÓSTICOS DE ALTA
                for (int i = 0; i < dtg_DiagnosticosAlta.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        hoja008.FOR_DA_UNOCIE = dtg_DiagnosticosAlta.Rows[0].Cells[2].Value.ToString();
                        hoja008.FOR_DA_UNO = (dtg_DiagnosticosAlta.Rows[0].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_DiagnosticosAlta.Rows[i].Cells[3].Value)
                            hoja008.FOR_DA_UNOPRE = dtg_DiagnosticosAlta.Rows[0].Cells[3].Value != null ? "X" : " ";
                        else
                            hoja008.FOR_DA_UNODEF = dtg_DiagnosticosAlta.Rows[0].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 1)
                    {
                        hoja008.FOR_DA_DOSCIE = dtg_DiagnosticosAlta.Rows[1].Cells[2].Value.ToString();
                        hoja008.FOR_DA_DOS = (dtg_DiagnosticosAlta.Rows[1].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_DiagnosticosAlta.Rows[i].Cells[3].Value)
                            hoja008.FOR_DA_DOSPRE = dtg_DiagnosticosAlta.Rows[1].Cells[3].Value != null ? "X" : " ";
                        else
                            hoja008.FOR_DA_DOSDEF = dtg_DiagnosticosAlta.Rows[1].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 2)
                    {
                        hoja008.FOR_DA_TRESCIE = dtg_DiagnosticosAlta.Rows[2].Cells[2].Value.ToString();
                        hoja008.FOR_DA_TRES = (dtg_DiagnosticosAlta.Rows[2].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_DiagnosticosAlta.Rows[i].Cells[3].Value)
                            hoja008.FOR_DA_TRESPRE = dtg_DiagnosticosAlta.Rows[2].Cells[3].Value != null ? "X" : " ";
                        else
                            hoja008.FOR_DA_TRESDEF = dtg_DiagnosticosAlta.Rows[2].Cells[4].Value != null ? "X" : " ";
                    }
                }
                
                //PLAN DE TRATAMIENTO
                string indicaciones = "";
                for (int i = 0; i < dgv_Indicaciones.Rows.Count - 1; i++)
                {
                    if (i < 4)
                    {
                        if (dgv_Indicaciones.Rows[i].Cells[0].Value != null && dgv_Indicaciones.Rows[i].Cells[1].Value != null)
                            indicaciones = indicaciones + " " + dgv_Indicaciones.Rows[i].Cells[1].Value.ToString() + "\n";
                    }
                }

                hoja008.FOR_PT_INDICAD = indicaciones.Replace("'", "´");

                string medicamentos = "";
                for ( int i = 0; i < dgv_Medicamentos.Rows.Count - 1; i++)
                {
                    if (i < 4)
                    {
                        if (dgv_Medicamentos.Rows[i].Cells[0].Value != null && dgv_Medicamentos.Rows[i].Cells[1].Value != null)
                            medicamentos = medicamentos + " " + dgv_Medicamentos.Rows[i].Cells[2].Value.ToString() + "\n";
                    }
                }
                hoja008.FOR_PT_MEDICAM = medicamentos.Replace("'", "´");
       
                // ALTA 

                hoja008.FOR_ALTA_DOM = cmb_Destino.SelectedIndex == 0 ? "X":" ";
                hoja008.FOR_ALTA_CE = cmb_Destino.SelectedIndex == 1 ? "X" : " ";
                hoja008.FOR_ALTA_OBS = cmb_Destino.SelectedIndex == 2 ? "X" : " ";
                hoja008.FOR_ALTA_INT = cmb_Destino.SelectedIndex == 3 ? "X" : " ";
                hoja008.FOR_ALTA_REF = cmb_Destino.SelectedIndex == 4 ? "X" : " ";
                hoja008.FOR_ALTA_SR = txt_servicioReferencia.Text;
                hoja008.FOR_ALTA_ESTEB = txt_Establecimiento.Text;

                hoja008.FOR_ALTA_EGRE = chk_EgresaVivo.Checked == true ? "X" : " ";
                hoja008.FOR_ALTA_EE = chk_CondE.Checked == true ? "X" : "";
                hoja008.FOR_ALTA_EI = chk_CondI.Checked == true ? "X" : " ";
                hoja008.FOR_ALTA_DI = txt_DiasInc.Text;
                hoja008.FOR_ALTA_ME = chk_MuertoE.Checked == true ? "X" : " ";
                hoja008.FOR_ALTA_CAUSA = txt_CausaMuerte.Text;

                // USUARIOS

                hoja008.FOR_FECHA = dtp_fechaAltaEmerencia.Value.ToString().Substring(0,10);                
                hoja008.FOR_HORA = txt_horaAltaEmerencia.Text;
                hoja008.FOR_NPROF = txt_profesionalEmergencia.Text;
                hoja008.FOR_PROFCOD = txt_CodMSPE.Text;

                //hoja008.FOR_NPROF = ("Dr/a. " + NegUsuarios.RecuperaUsuario(Convert.ToInt16(emergenciaActual.ID_USUARIO)).NOMBRES + " " + NegUsuarios.RecuperaUsuario(Convert.ToInt16(emergenciaActual.ID_USUARIO)).APELLIDOS).Replace("'", "´");
                //hoja008.FOR_PROFCOD = Convert.ToInt32(emergenciaActual.ID_USUARIO);
                //USUARIOS usuariosI = NegUsuarios.RecuperaUsuario(Convert.ToInt16(atencionActual.USUARIOSReference.EntityKey.EntityKeyValues[0].Value));
                hoja008.USUARIO =
                    NegUsuarios.RecuperaUsuario(
                        Convert.ToInt16(atencionActual.USUARIOSReference.EntityKey.EntityKeyValues[0].Value)).APELLIDOS + " " +
                    NegUsuarios.RecuperaUsuario(
                        Convert.ToInt16(atencionActual.USUARIOSReference.EntityKey.EntityKeyValues[0].Value)).NOMBRES;

                //IMPRIMIR REPORTE
                ReportesHistoriaClinica reporte008 = new ReportesHistoriaClinica();
                reporte008.ingresarFormEmergencia(hoja008);
                His.Formulario.frmReportes ventana = new His.Formulario.frmReportes(1, "Hoja 008E");
                ventana.Show();
                //ventana.crystalReportViewer1.RefreshReport();                  
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_Gesta_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_Partos_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_Abortos_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_Cesareas_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_SemanaG_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_FrecCF_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_AltU_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_Dilatacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_Borramiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_SaturaO_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_DiasInc_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_CodMSPE_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void keypressed(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }

        }

        private void txt_PesoKG_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Decimal indiceMasaCorporal = 0;
                Decimal talla = Convert.ToDecimal(txt_Talla.Text);
                if (talla > 0)
                    indiceMasaCorporal = Convert.ToDecimal(txt_PesoKG.Text) / (talla * talla);
                txtIMCorporal.Text = Decimal.Round(indiceMasaCorporal, 2).ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            if (txt_PesoKG.Text=="")
            {
                txt_PesoKG.Text = "0";
            }

        }

        private void txt_Talla_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Decimal indiceMasaCorporal = 0;
                Decimal talla = Convert.ToDecimal(txt_Talla.Text);
                if (talla > 0)
                    indiceMasaCorporal = Convert.ToDecimal(txt_PesoKG.Text) / (talla * talla);
                txtIMCorporal.Text = Decimal.Round(indiceMasaCorporal, 2).ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            if (txt_Talla.Text == "")
            {
                txt_Talla.Text = "0";
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_LocalizacionL_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_LocalizacionL_SelectionChanged(object sender, EventArgs e)
        {
            int i = dgv_LocalizacionL.CurrentRow.Index;
            //foreach (DataGridViewRow item in dgv_LocalizacionL.Rows)
            //    {
            //        if (item.Cells[0].Value != null)
            //        {
            //            for (int i = 0; i < ant3.Count; i++)
            //            {
            //                DtoCatalogos cat = ant3.ElementAt(i);
            //                if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
            //                {
            //                    if (i == 0)
            //                    {
        }

        private void dtg_antec_personales_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_antec_personales.CurrentRow != null)
                        {
                            if (dtg_antec_personales.CurrentRow.Cells["codigoAntPer"].Value != null)
                            {
                                Int32 codigoA = Convert.ToInt32(dtg_antec_personales.CurrentRow.Cells["codigoAntPer"].Value);
                                NegHcEmergenciaFDetalle.eliminarEFD(codigoA);
                                dtg_antec_personales.Rows.Remove(dtg_antec_personales.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                            }
                            else
                            {
                                dtg_antec_personales.Rows.Remove(dtg_antec_personales.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void dgv_LocalizacionL_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dgv_LocalizacionL.CurrentRow != null)
                        {
                            if (dgv_LocalizacionL.CurrentRow.Cells["Codigo"].Value != null)
                            {
                                Int32 codigoL = Convert.ToInt32(dgv_LocalizacionL.CurrentRow.Cells["Codigo"].Value);
                                NegHcEmergenciaFDetalle.eliminarEFD(codigoL);
                                dgv_LocalizacionL.Rows.Remove(dgv_LocalizacionL.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dgv_LocalizacionL.Rows.Remove(dgv_LocalizacionL.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgv_ExamenesS_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dgv_ExamenesS.CurrentRow != null)
                        {
                            if (dgv_ExamenesS.CurrentRow.Cells["codigoE"].Value != null)
                            {
                                Int32 codigoL = Convert.ToInt32(dgv_ExamenesS.CurrentRow.Cells["codigoE"].Value);
                                NegHcEmergenciaFDetalle.eliminarEFD(codigoL);
                                dgv_ExamenesS.Rows.Remove(dgv_ExamenesS.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dgv_ExamenesS.Rows.Remove(dgv_ExamenesS.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtp_ultimaMenst1_ValueChanged(object sender, EventArgs e)
        {
            if (dtp_ultimaMenst1.Checked == true)
            {
                TimeSpan ts;
                ts = DateTime.Now - dtp_ultimaMenst1.Value;
                string valor = (ts.Days / 7).ToString();
                txt_SemanaG.Text = valor.ToString();
            }
            else
            {
                txt_SemanaG.Text = "";
            }
        }

        private void dgv_Indicaciones_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dgv_Indicaciones.CurrentRow != null)
                        {
                            if (dgv_Indicaciones.CurrentRow.Cells["codigoI"].Value != null)
                            {
                                Int32 codigoL = Convert.ToInt32(dgv_Indicaciones.CurrentRow.Cells["codigoI"].Value);
                                NegHcEmergenciaFDetalle.eliminarTratameinto(codigoL);
                                dgv_Indicaciones.Rows.Remove(dgv_Indicaciones.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dgv_Indicaciones.Rows.Remove(dgv_Indicaciones.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgv_Medicamentos_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        {
                            if (dgv_Medicamentos.CurrentRow != null)
                            {
                                if (dgv_Medicamentos.CurrentRow.Cells["CodigoMedicamento"].Value != null)
                                {
                                    Int32 codigoL = Convert.ToInt32(dgv_Medicamentos.CurrentRow.Cells["CodigoMedicamento"].Value);
                                    NegHcEmergenciaFDetalle.eliminarTratameinto(codigoL);
                                    dgv_Medicamentos.Rows.Remove(dgv_Medicamentos.CurrentRow);

                                    if (dgv_Medicamentos.Rows.Count == 1)
                                    {
                                        //dgv_Medicamentos.Rows.Add();
                                    }

                                    MessageBox.Show("Registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    dgv_Medicamentos.Rows.Remove(dgv_Medicamentos.CurrentRow);

                                    if (dgv_Medicamentos.Rows.Count == 1)
                                    {
                                        //dgv_Medicamentos.Rows.Add();
                                    }

                                    MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        }
                    case Keys.F1:

                        frmAyudaMedicamentos frmAyuda = new frmAyudaMedicamentos("MEDICAMENTOS");
                        frmAyuda.ShowDialog();

                        if (frmAyuda.Descripcion != "")
                        {
                            
                            //DataGridViewRow fila = dgv_Medicamentos.CurrentRow;
                            //fila.Cells[1].Value = frmAyuda.Codigo;
                            //fila.Cells[2].Value = frmAyuda.Descripcion;
                            //dgv_Medicamentos.Rows.Add();

                            DataGridViewRow fila = new DataGridViewRow();

                            DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell CodigoPcell = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell PosologiaCell = new DataGridViewTextBoxCell();

                            codigoCell.Value = null;
                            CodigoPcell.Value = frmAyuda.Codigo;
                            textcell.Value = frmAyuda.Descripcion;
                            PosologiaCell.Value = "";

                            fila.Cells.Add(codigoCell);
                            fila.Cells.Add(CodigoPcell);
                            fila.Cells.Add(textcell);
                            fila.Cells.Add(PosologiaCell);

                            dgv_Medicamentos.Rows.Add(fila);


                        }

                        //this.dgv_Medicamentos.Rows.Insert(dgv_Medicamentos.Rows.Count, "Inserto1", "Inserto2"); 

                        //frmAyuda.Dispose();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtg_DiagnosticosI_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_DiagnosticosI.CurrentRow != null)
                        {
                            if (dtg_DiagnosticosI.CurrentRow.Cells["codigoDia"].Value != null)
                            {
                                Int32 codigoDetDiag = Convert.ToInt32(dtg_DiagnosticosI.CurrentRow.Cells["codigoDia"].Value);
                                NegHcEmergenciaFDetalle.eliminarDiagnosticoDetalle(codigoDetDiag);
                                dtg_DiagnosticosI.Rows.Remove(dtg_DiagnosticosI.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dtg_DiagnosticosI.Rows.Remove(dtg_DiagnosticosI.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, false, false, true, true, false);

            ultraGroupBox7.Enabled = true;
            grpDosTresCuatro.Enabled = true;
            grpCincoSeis.Enabled = true;
            grpSieteOchoNueve.Enabled = true;
            groupBox31.Enabled = true;
            grpTrceCatorce.Enabled = true;

            activarFormulario(true);

            if (cmb_Destino.Text=="DOMICILIO")
            {
                txt_servicioReferencia.Enabled=false;
                txt_Establecimiento.Enabled = false;
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;

                dtpFechaAlta.Value = DateTime.Now;

                if (!validarAlta())
                {
                    resultado = MessageBox.Show("Desea cerrar la ficha de Registro de Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if ((emergenciaActual.EMER_ESTADO == null) || (emergenciaActual.EMER_ESTADO == 0))
                        {
                            MessageBox.Show("Numero:" + emergenciaActual.EMER_CODIGO);
                            //modificadaEmergencia.ATE_CODIGO = 1;
                            NegHcEmergenciaForm.cerrarHcEmergenciaForm(emergenciaActual.EMER_CODIGO);
                            atencionActual.ATE_FECHA_ALTA = dtpFechaAlta.Value;
                            atencionActual.ATE_ESTADO = false;
                            NegAtenciones.EditarAtencionAdmision(atencionActual);
                            HabilitarBotones(false, false, true, false, true, false, false);
                            MessageBox.Show("Emergencia Cerrada.....!?");
                        }
                    }
                }
                else
                {
                    resultado = MessageBox.Show("Ingrese Fecha y Hora de Alta de la Atención", " ",
                                                MessageBoxButtons.OK, MessageBoxIcon.Question);
                    AgregarError(dtpFechaAlta);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, false, true, false, true, false, false);
            cargarAtencion(Convert.ToInt32(txt_Atencion.Text.Trim()));
            actualizarPaciente();
        } 

        #endregion

       
        private void dgv_ExamenesS_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_ExamenesS.CurrentRow.Cells[1].Value != null)
            {
                if (dgv_ExamenesS.CurrentRow.Cells[1].Value.ToString() == "BIOMETRIA" || dgv_ExamenesS.CurrentRow.Cells[1].Value.ToString() == "GASOMETRÍA" ||
                    dgv_ExamenesS.CurrentRow.Cells[1].Value.ToString() == "ELECTRO CARDIOGRAMA" || dgv_ExamenesS.CurrentRow.Cells[1].Value.ToString() == "ECOGRAFÍA PÉLVICA" ||
                    dgv_ExamenesS.CurrentRow.Cells[1].Value.ToString() == "ECOGRAFÍA ABDOMEN")
                {
                    dgv_ExamenesS.CurrentRow.Cells[2].Value = "";
                    dgv_ExamenesS.CurrentRow.Cells[2].ReadOnly = true;
                }
                else
                {
                    dgv_ExamenesS.CurrentRow.Cells[2].ReadOnly = false;
                }
            }
        }

        private void dtg_DiagnosticosAlta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dtg_DiagnosticosAlta.Columns[4].Index)
            {
                DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_DiagnosticosAlta.Rows[e.RowIndex].Cells[3];
                if (chkCell.Value == null)
                {
                    chkCell.Value = false;
                }
                else
                {
                    chkCell.Value = false;
                }
            }
             if (e.ColumnIndex == this.dtg_DiagnosticosAlta.Columns[3].Index)
             {

                DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_DiagnosticosAlta.Rows[e.RowIndex].Cells[4];
                if (chkCell.Value == null)
                {
                    chkCell.Value = false;
                }
                else
                {
                    chkCell.Value = false;
                }
            
            }
        }

        private void dgv_Medicamentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_F1DA_Click(object sender, EventArgs e)
        {

        }

        private void txt_profesionalEmergencia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frmAyudaMedicamentos frmAyuda = new frmAyudaMedicamentos("MEDICOS");
                frmAyuda.ShowDialog();

                DataGridViewRow fila = dgv_Medicamentos.CurrentRow;
                //fila.Cells[1].Value = frmAyuda.Codigo;
                txt_profesionalEmergencia.Text = frmAyuda.Descripcion;
                txt_CodMSPE.Text = frmAyuda.CodigoMSP;
            }

        }

        private void dtg_ExamenFisico_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        if (e.RowIndex>=0)
        {

            DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)this.dtg_ExamenFisico.Rows[e.RowIndex].Cells[2];
            DataGridViewCell txtCell = this.dtg_ExamenFisico.Rows[e.RowIndex].Cells[4];
            if (chkCell.Value == null)
            {
                chkCell.Value = true;
                txtCell.ReadOnly = false;

            }
        }
        }

        private void txt_OtroMotivo_TextChanged(object sender, EventArgs e)
        {
            if (txt_OtroMotivo.Text.Length > 0)
            {
                chk_OtroMotivo.Checked = true;
            }
            else
            {
                chk_OtroMotivo.Checked = false;
            }


        }

        private void chk_OtroMotivo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_OtroMotivo.Checked == true) 
            {                
                chk_Trauma.Checked=false;
                chk_CausaC.Checked=false;
                chk_CausaGOb.Checked=false;
                chk_CausaQuir.Checked = false;
                this.txt_OtroMotivo.Enabled = true;
            }
            else
            {
                this.txt_OtroMotivo.Text = "";
                this.txt_OtroMotivo.Enabled = false;
            }

        }

        private void txt_PresionA1_TextChanged(object sender, EventArgs e)
        {
            if (txt_PresionA1.Text=="")
            {
                txt_PresionA1.Text = "0";
            }
        }

        private void txt_PresionA1_GotFocus(Object sender, EventArgs e)
        {

            txt_PresionA1.SelectAll();

        }

        private void txt_FCardiaca_TextChanged(object sender, EventArgs e)
        {
            if (txt_FCardiaca.Text == "")
            {
                txt_FCardiaca.Text = "0";
            }
        }

        private void txt_FResp_TextChanged(object sender, EventArgs e)
        {
            if (txt_FResp.Text == "")
            {
                txt_FResp.Text = "0";
            }
        }

        private void txt_Glicemia_TextChanged(object sender, EventArgs e)
        {
            if (txt_Glicemia.Text == "")
            {
                txt_Glicemia.Text = "0";
            }
        }

        private void txtIMCorporal_TextChanged(object sender, EventArgs e)
        {
            if (txtIMCorporal.Text == "")
            {
                txtIMCorporal.Text = "0";
            }
        }

        private void txt_PerimetroC_TextChanged(object sender, EventArgs e)
        {
            if (txt_PerimetroC.Text == "")
            {
                txt_PerimetroC.Text = "0";
            }
        }

        private void txt_DiamPDV_TextChanged_1(object sender, EventArgs e)
        {
            if (txt_DiamPDV.Text != "" || txt_DiamPDV.Text == "0")
            {
                bool estado;
                if (Convert.ToInt32(txt_DiamPDV.Text) > 8 || Convert.ToInt32(txt_DiamPDV.Text) < 1)
                    txt_DiamPDV.Text = "";
            }
        }

        private void btn_F1DA_Click_1(object sender, EventArgs e)
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            if (dtg_DiagnosticosAlta.CurrentRow != null)
            {
                if (busqueda.codigo != null)
                {
                    DataGridViewRow fila = dtg_DiagnosticosAlta.CurrentRow;
                    fila.Cells[2].Value = busqueda.codigo;
                    fila.Cells[1].Value = busqueda.resultado;
                }
            }
            dtg_DiagnosticosAlta.Focus();
        }

        private void txt_PresionA2_Click(object sender, EventArgs e)
        {
            txt_PresionA2.SelectAll();
        }

        private void txt_PresionA1_Click(object sender, EventArgs e)
        {
            txt_PresionA1.SelectAll();
        }

        private void txt_FCardiaca_Click(object sender, EventArgs e)
        {
            txt_FCardiaca.SelectAll();
        }

        private void txt_FResp_Click(object sender, EventArgs e)
        {
            txt_FResp.SelectAll();
        }

        private void txt_TBucal_Click(object sender, EventArgs e)
        {
            txt_TBucal.SelectAll();
        }

        private void txt_TAxilar_Click(object sender, EventArgs e)
        {
            txt_TAxilar.SelectAll();
        }

        private void txt_SaturaO_Click(object sender, EventArgs e)
        {
            txt_SaturaO.SelectAll();
        }

        private void txt_TotalG_Click(object sender, EventArgs e)
        {
            txt_TotalG.SelectAll();
        }

        private void txt_Glicemia_Click(object sender, EventArgs e)
        {
            txt_Glicemia.SelectAll();
        }

        private void txt_PesoKG_Click(object sender, EventArgs e)
        {
            txt_PesoKG.SelectAll();
        }

        private void txt_Talla_Click(object sender, EventArgs e)
        {
            txt_Talla.SelectAll();
        }

        private void txtIMCorporal_Click(object sender, EventArgs e)
        {
            txtIMCorporal.SelectAll();
        }

        private void txt_PerimetroC_Click(object sender, EventArgs e)
        {
            txt_PerimetroC.SelectAll();
        }

                        
        
    }
}
