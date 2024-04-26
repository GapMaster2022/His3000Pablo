using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using Infragistics;
using His.General;
using System.Reflection;
using His.Parametros;
using His.Formulario;
using Infragistics.Win.UltraWinGrid;
using His.Entidades.Reportes;
using His.DatosReportes;

namespace His.Emergencia
{
    public partial class frm_Registro : Form
    {
        #region Variables

        //List<MEDICOS> medicos = new List<MEDICOS>();
        List<PAIS> paises = new List<PAIS>();
        List<CIUDAD> ciudad = new List<CIUDAD>();
        //List<ESTADO_CIVIL> estadoCivil = new List<ESTADO_CIVIL>();
        //List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        List<ESPECIALIDADES_MEDICAS> especialidades = new List<ESPECIALIDADES_MEDICAS>();
        //List<TIPO_TRATAMIENTO> tipoTratamiento = new List<TIPO_TRATAMIENTO>();
        //List<TIPO_CIUDADANO> tipoCiudadano = new List<TIPO_CIUDADANO>();
        //List<ATENCION_FORMAS_LLEGADA> formasLlegada = new List<ATENCION_FORMAS_LLEGADA>();
        //List<TIPO_REFERIDO> tipoReferido = new List<TIPO_REFERIDO>();
        //List<CATEGORIAS_CONVENIOS> categorias = new List<CATEGORIAS_CONVENIOS>();
        //List<TIPO_FORMA_PAGO> listaTipoFormaPago = new List<TIPO_FORMA_PAGO>();
        List<PACIENTES> pacientes = new List<PACIENTES>();
        //List<CLASE_LOCALIDAD> clasesLocalidad = new List<CLASE_LOCALIDAD>();
        //List<TIPO_FORMA_PAGO> tipoPago = new List<TIPO_FORMA_PAGO>();
        //List<TIPO_GARANTIA> tipoGarantia = new List<TIPO_GARANTIA>();
        //List<TIPO_EMPRESA> tipoEmpresa = new List<TIPO_EMPRESA>();
        List<HC_CATALOGOS> catalogoTratamiento = new List<HC_CATALOGOS>();
        //List<HC_CATALOGOS> catTratamientoActual = new List<HC_CATALOGOS>();
        //List<HC_CATALOGOS> catProcedimientos = new List<HC_CATALOGOS>();
        List<HC_CATALOGOS> catalogoCirugiasPrevias = new List<HC_CATALOGOS>();
        List<HC_CATALOGOS> catalogoSintomas = new List<HC_CATALOGOS>();
        List<HC_CATALOGOS> catalogoComplicaciones = new List<HC_CATALOGOS>();
        List<HC_CATALOGOS> catalogoTransferido = new List<HC_CATALOGOS>();
        //List<HC_CATALOGOS> catalogoAtenciones = new List<HC_CATALOGOS>();
        List<HC_CATALOGOS> catalogoTriaje = new List<HC_CATALOGOS>();
        List<HC_CATALOGOS> catalogoEspeClinicas = new List<HC_CATALOGOS>();
        List<HC_CATALOGOS> catalogoEspeQuirur = new List<HC_CATALOGOS>();
        List<HC_CATALOGOS> catalogoEspeOtras = new List<HC_CATALOGOS>();

        HC_EMERGENCIA nuevaEmergencia = new HC_EMERGENCIA();
        HC_EMERGENCIA modificadaEmergencia = new HC_EMERGENCIA();

        DataTable dt = new DataTable();
        DataTable dtCatalogoExamen = new DataTable();
        DataTable dtCatalogoImagen = new DataTable();
        DataTable dtCatalogoInter= new DataTable();
        DataTable dtCatalogoReferido = new DataTable();

        //List<ETNIA> etnias = new List<ETNIA>();
        List<CIE10> listaCIE = new List<CIE10>();
        List<CIE10> listaBusqICD = new List<CIE10>();

        PACIENTES pacienteActual = null;
        ATENCIONES atencionActual = null;
        PACIENTES_DATOS_ADICIONALES datosPacienteActual = null;
        //ATENCIONES ultimaAtencion = null;
        ASEGURADORAS_EMPRESAS empresaPaciente = null;
        //List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = null;
        //List<ATENCION_DETALLE_GARANTIAS> detalleGarantias = null;
        MEDICOS medico = null;
        List<HC_EMERGENCIA> listaEmergencias = new List<HC_EMERGENCIA>();
        List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        int codigoPaciente;
        int codigoEmergencia;
        string resultadoSub;
        string descripcionSub;
        HC_CATALOGOS examen = new HC_CATALOGOS();
        HC_CATALOGOS imagen = new HC_CATALOGOS();
        HC_CATALOGOS inter = new HC_CATALOGOS();
        HC_CATALOGOS referido = new HC_CATALOGOS();
        string comunicado;
        string resultado;
        string especialidad;
        DateTime comunicadoFecha;
        DateTime realizado;
        string nombreI;
        string observaciones;
        Boolean estadoFecha = false;
        Boolean estadoFechaRea = false;
        Boolean estadoCarga = false;
        public string codigoAtencionA;        

        USUARIOS usuario = new USUARIOS();

        //int num = 0;

        public bool cargaciu;
        public bool direccionNueva = false;
        public bool pacienteNuevo = false;
        public bool emerNuevo = false;
        public bool atencionNueva = false;
        public bool bandCategorias = false;
        public bool bandGarantias = false;
        Boolean regisEmer = false;
        //variable que controla el refresh de 

        #endregion

        public frm_Registro()
        {
            //tipoPago = NegFormaPago.RecuperaTipoFormaPagos();
            usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);            
            //especialidades = NegEspecialidades.ListaEspecialidades();            
            //aseguradoras = NegAseguradoras.ListaEmpresas();
            //categorias = NegCategorias.ListaCategorias();
            //tipoTratamiento = NegTipoTratamiento.RecuperaTipoTratamiento();
            //formasLlegada = NegAtencionesFormasLlegada.listaAtencionesFormasLlegada();
            //estadoCivil = NegEstadoCivil.RecuperaEstadoCivil();
            //etnias = NegEtnias.ListaEtnias();
            //paises = NegPais.RecuperaPaises().OrderBy(cod => cod.NOMPAIS).ToList();
            //ciudad = NegCiudad.ListaCiudades();
            //tipoCiudadano = NegTipoCiudadano.listaTiposCiudadano();
            //medicos = NegMedicos.listaMedicos();
            //listaTipoFormaPago = NegFormaPago.RecuperaTipoFormaPagos();
            //tipoReferido = NegTipoReferido.listaTipoReferido();
            //tipoGarantia = NegTipoGarantia.listaTipoGarantia();
            //tipoEmpresa = NegAseguradoras.ListaTiposEmpresa();
            catalogoTratamiento = NegCatalogos.RecuperarHcCatalogosPorTipo(13);
            catalogoSintomas = NegCatalogos.RecuperarHcCatalogosPorTipo(23);           
            InitializeComponent();
            toolStripNuevo.Image = Archivo.imgBtnAdd2;
            btnCerrarHistoria.Image = Archivo.imgBtnRestart;
            btnEliminar.Image = Archivo.imgDelete;
            btnGuardar.Image = Archivo.imgBtnFloppy;
            btnCancelar.Image = Archivo.imgBtnStop;
            btnCerrar.Image = Archivo.imgBtnSalir32;
            toolStripNuevo.Image = Archivo.nuevoPaciente;
            btnEmergencia.Image = Archivo.emergency;
            btnImprimir.Image = Archivo.imgBtnImprimir32;           
            //habilitarBotones(false, false, false, false, false, false, true); 
            btnAgSint.Image = Archivo.imgBtnAdd;
                                    
        }
        
        #region cargarDatos

        private void leerUltimaEmergencia() 
        {
            modificadaEmergencia = NegHcEmergencia.RecuperarUltimaEmergencia();
            emerNuevo = true;
            if (modificadaEmergencia != null)
            {
                if (modificadaEmergencia != null)
                {
                    //pacienteNuevo = false;
                    buscarHistorias.Enabled = true;
                    txt_historiaclinica.Tag = true;
                    string historiaPaciente = modificadaEmergencia.PACIENTES.PAC_HISTORIA_CLINICA;
                    txtRegistro.Text = Convert.ToString(modificadaEmergencia.COD_EMERGENCIA);
                    cargarPaciente(historiaPaciente);
                    //cargarEmergencia(modificadaEmergencia.COD_EMERGENCIA);
                    if (modificadaEmergencia.EME_ESTADO == true) 
                        habilitarBotones(false, false, false, true, true, true, true);
                    else
                        habilitarBotones(true, true, true, true, true, true, true);
                    emerNuevo = false;
                    cargarDatosImpresion(modificadaEmergencia.COD_EMERGENCIA, modificadaEmergencia);
                    txtRegistro.Text = Convert.ToString(modificadaEmergencia.COD_EMERGENCIA);
                }
            }
            else
            {
                habilitarBotones(false, false, false, false, false, true, true);
                buscarHistorias.Enabled = false;
                emerNuevo = true;
            }   
        }

        private void habilitarBotones(bool nuevo, bool cerrarEmergencia, bool guardar, bool eliminar, bool cancelar, bool imprimir, bool salir)
        {
            
            toolStripNuevo.Enabled = nuevo;
            btnCerrarHistoria.Enabled = cerrarEmergencia;
            btnGuardar.Enabled = guardar;
            btnEliminar.Enabled = eliminar;
            btnCancelar.Enabled = cancelar;
            btnImprimir.Enabled = imprimir;
            btnCerrar.Enabled = salir;
        }
        
        private void limpiarDatos()
        {
            try
            {
                txtAlergias.Text = string.Empty;
                chbAlcoholE.Checked = false;
                chbTabacoE.Checked = false;
                chbDrogasE.Checked = false;
                dateTimeFum.Value = DateTime.Now;
                //dtpFechaInicio.Value = DateTime.Now;
                dtpEstanciaHosp.Value = DateTime.Now;
                txtTA1.Text = string.Empty;
                txtTA2.Text = string.Empty;
                txtT.Text = string.Empty;
                txtSat.Text = string.Empty;
                txtFc.Text = string.Empty;
                txtFr.Text = string.Empty;
                rdb_NoUrgente.Checked = false;
                rdb_Emergencia.Checked = false;
                rdb_Critico.Checked = false;
                rdb_Otras.Checked = false;
                chbAlcoholC.Checked = false;
                chbOtrasC.Checked = false;
                txtOtrasActual.Text = string.Empty;
                txtOtrasActual.Enabled = false;
                chbDrogasC.Checked = false;
                txtOrl.Text = "OK";
                txtCabeza.Text = "OK";
                txtCuello.Text = "OK";
                txtTorax.Text = "OK";
                txtCardiaco.Text = "OK";
                txtPulmonar.Text = "OK";
                txtAbdomen.Text = "OK";
                txtGenitales.Text = "OK";
                txtPerine.Text = "OK";
                txtExtremidades.Text = "OK";
                txtFosasL.Text = "OK";
                txtLinfa.Text = "OK";
                txtNeuro.Text = "OK";
                txtPiel.Text = "OK";
                txtObserEnfer.Text = string.Empty;
                txtDiagEmerg.Text = string.Empty;
                txtDiagDef.Text = string.Empty;
                txtDiagDef.Enabled = false;
                //rdb_Directo.Checked = false;
                //rdb_Indirecto.Checked = false;
                //rdb_Trauma.Checked = false;
                //rdb_Infecciones.Checked = false;
                //rdb_Cardiov.Checked = false;
                //rdb_Quirur.Checked = false;
                //rdb_OtrasDis.Checked = false;

                cb_Clinicas.SelectedIndex = -1;
                cb_Clinicas.Enabled = false;
                cb_Quirurgicas.SelectedIndex = -1;
                cb_Quirurgicas.Enabled = false;
                cb_OtrasEspec.SelectedIndex = -1;
                cb_OtrasEspec.Enabled = false;
                rdb_Clinicas.Checked = false;
                rdb_Quirurgicas.Checked = false;
                rdb_EspeOtras.Checked = false;

                //rdb_Referido.Checked = false;
                //rdb_Privado.Checked = false;
                rdb_Alta.Checked = false;
                rdb_IngresoPiso.Checked = false;
                //rdb_IngresoQuirofano.Checked = false;
                rdb_IngresoUCI.Checked = false;
                //rdb_IngresoCoron.Checked = false;
                rdb_Muerto.Checked = false;
                rdb_Transferido.Checked = false;
                txtTransferido.Text = string.Empty;
                txtTransferido.Enabled = false;
                chb_ComplSelec.Checked = false;
                rdb_Vivo.Checked = false;
                rdb_MuertoP.Checked = false;
                rdb_TransferidoP.Checked = false;
                txtObserFinal.Text = string.Empty;
                txtVideosEnc.Text = string.Empty;
                txtResumenDiag.Text = string.Empty;
                txtProcedimientos.Text = string.Empty;
                txtExamenesReal.Text = string.Empty;
                txtTratamientos.Text = string.Empty;
                txtResumenDiag.Text = string.Empty;
                txtProcedimientos.Text = string.Empty;
                txtExamenesReal.Text = string.Empty;
                txtTratamientos.Text = string.Empty;

                codigoEmergencia = 0;
                resultadoSub = "";
                descripcionSub = "";
                examen = new HC_CATALOGOS();
                imagen = new HC_CATALOGOS();
                inter = new HC_CATALOGOS();
                referido = new HC_CATALOGOS();
                comunicado = "";
                resultado = "";
                especialidad = "";
                //comunicadoFecha = DateTime.Now;
                comunicadoFecha = new DateTime();
                //realizado = DateTime.Now;
                realizado = new DateTime();
                nombreI = "";
                observaciones = "";
                listaCIE = new List<CIE10>();
                listaBusqICD = new List<CIE10>();
                gridEnfPrevias.DataSource = null;
                //gridBusquedaICD.DataSource = null;
                catalogoTratamiento = new List<HC_CATALOGOS>();
                //catTratamientoActual = new List<HC_CATALOGOS>();
                //catProcedimientos = new List<HC_CATALOGOS>();
                catalogoCirugiasPrevias = new List<HC_CATALOGOS>();
                catalogoSintomas = new List<HC_CATALOGOS>();
                catalogoComplicaciones = new List<HC_CATALOGOS>();
                catalogoTransferido = new List<HC_CATALOGOS>();
                //catalogoAtenciones = new List<HC_CATALOGOS>();
                catalogoTriaje = new List<HC_CATALOGOS>();                
                dataGridViewSubSintomas.DataSource = null;
                dataGridViewExamenes.DataSource = null;
                dataGridViewImagenes.DataSource = null;
                dataGridViewReferido.DataSource = null;
                dataGridViewInterconsultas.DataSource = null;
                //gridBusquedaICD.DataSource = null;
                dt = new DataTable();
                dtCatalogoExamen = new DataTable();
                dtCatalogoImagen = new DataTable();
                dtCatalogoInter= new DataTable();
                dtCatalogoReferido = new DataTable();                

                //cb_medico.DataSource = new BindingSource(medicos.Select(m => new { m.MED_CODIGO, MED_NOMBRE = m.MED_APELLIDO_PATERNO + " " + m.MED_NOMBRE1 }).ToList(), null);
                //cb_medico.ValueMember = "MED_CODIGO";
                //cb_medico.DisplayMember = "MED_NOMBRE";

                //cb_medico.DataSource = new BindingSource(medicos.Select(m => new { m.MED_CODIGO, MED_NOMBRE = m.MED_APELLIDO_PATERNO + " " + m.MED_NOMBRE1 }).ToList(), null);
                //cb_medico.ValueMember = "MED_CODIGO";
                //cb_medico.DisplayMember = "MED_NOMBRE";
                //cb_medico.AutoCompleteSource = AutoCompleteSource.ListItems;
                //cb_medico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

              
                //catTratamientoActual = NegCatalogos.RecuperarHcCatalogosPorTipo(15);                
                //cb_TratamientoActual.DataSource = catTratamientoActual;
                //cb_TratamientoActual.ValueMember = "HCC_CODIGO";
                //cb_TratamientoActual.DisplayMember = "HCC_NOMBRE";
                //cb_TratamientoActual.AutoCompleteSource = AutoCompleteSource.ListItems;
                //cb_TratamientoActual.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //for (int i = 0; i < catTratamientoActual.Count; i++)
                //{
                //    HC_CATALOGOS cat = new HC_CATALOGOS();
                //    cat = catTratamientoActual.ElementAt(i);
                //    if (cat.HCC_NOMBRE.Equals("NINGUNO"))
                //    {
                //        cb_TratamientoActual.SelectedIndex = i;
                //    }
                //}

                //catProcedimientos = NegCatalogos.RecuperarHcCatalogosPorTipo(13);                
                //cb_Procedimiento.DataSource = catProcedimientos;
                //cb_Procedimiento.ValueMember = "HCC_CODIGO";
                //cb_Procedimiento.DisplayMember = "HCC_NOMBRE";
                //cb_Procedimiento.AutoCompleteSource = AutoCompleteSource.ListItems;
                //cb_Procedimiento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //for (int i = 0; i < catProcedimientos.Count; i++)
                //{
                //    HC_CATALOGOS cat = new HC_CATALOGOS();
                //    cat = catProcedimientos.ElementAt(i);
                //    if (cat.HCC_NOMBRE.Equals("NINGUNO"))
                //    {
                //        cb_Procedimiento.SelectedIndex = i;
                //    }
                //}

                catalogoEspeClinicas = NegCatalogos.RecuperarHcCatalogosPorTipo(32);
                cb_Clinicas.DataSource = catalogoEspeClinicas;
                cb_Clinicas.ValueMember = "HCC_CODIGO";
                cb_Clinicas.DisplayMember = "HCC_NOMBRE";
                cb_Clinicas.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_Clinicas.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb_Clinicas.SelectedIndex = -1;

                catalogoEspeQuirur = NegCatalogos.RecuperarHcCatalogosPorTipo(33);
                cb_Quirurgicas.DataSource = catalogoEspeQuirur;
                cb_Quirurgicas.ValueMember = "HCC_CODIGO";
                cb_Quirurgicas.DisplayMember = "HCC_NOMBRE";
                cb_Quirurgicas.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_Quirurgicas.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb_Quirurgicas.SelectedIndex = -1;

                catalogoEspeOtras = NegCatalogos.RecuperarHcCatalogosPorTipo(34);
                cb_OtrasEspec.DataSource = catalogoEspeOtras;
                cb_OtrasEspec.ValueMember = "HCC_CODIGO";
                cb_OtrasEspec.DisplayMember = "HCC_NOMBRE";
                cb_OtrasEspec.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_OtrasEspec.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb_OtrasEspec.SelectedIndex = -1;

                //catalogoTransferido = NegCatalogos.RecuperarHcCatalogosPorTipo(29);
                //cb_Transferido.DataSource = catalogoTransferido;
                //cb_Transferido.ValueMember = "HCC_CODIGO";
                //cb_Transferido.DisplayMember = "HCC_NOMBRE";
                //cb_Transferido.AutoCompleteSource = AutoCompleteSource.ListItems;
                //cb_Transferido.AutoCompleteMode = AutoCompleteMode.SuggestAppend;                
                ////cb_Transferido.SelectedIndex = 0;
                //for (int i = 0; i < catalogoTransferido.Count; i++)
                //{
                //    HC_CATALOGOS cat = new HC_CATALOGOS();
                //    cat = catalogoTransferido.ElementAt(i);
                //    if (cat.HCC_NOMBRE.Equals("NINGUNO"))
                //    {
                //        cb_Transferido.SelectedIndex = i;
                //    }
                //}

                //catalogoAtenciones = NegCatalogos.RecuperarHcCatalogosPorTipo(29);
                //cb_AtencionMedica.DataSource = catalogoAtenciones;
                //cb_AtencionMedica.ValueMember = "HCC_CODIGO";
                //cb_AtencionMedica.DisplayMember = "HCC_NOMBRE";
                //cb_AtencionMedica.AutoCompleteSource = AutoCompleteSource.ListItems;
                //cb_AtencionMedica.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ////cb_AtencionMedica.SelectedIndex = 0;
                //for (int i = 0; i < catalogoAtenciones.Count; i++)
                //{
                //    HC_CATALOGOS cat = new HC_CATALOGOS();
                //    cat = catalogoAtenciones.ElementAt(i);
                //    if (cat.HCC_NOMBRE.Equals("NINGUNO"))
                //    {
                //        cb_AtencionMedica.SelectedIndex = i;
                //    }
                //}

                //catalogoTriaje = NegCatalogos.RecuperarHcCatalogosPorTipo(30);
                //cb_TriageOtras.DataSource = catalogoTriaje;
                //cb_TriageOtras.ValueMember = "HCC_CODIGO";
                //cb_TriageOtras.DisplayMember = "HCC_NOMBRE";
                //cb_TriageOtras.AutoCompleteSource = AutoCompleteSource.ListItems;
                //cb_TriageOtras.AutoCompleteMode = AutoCompleteMode.SuggestAppend;                
                //cb_TriageOtras.Enabled = false;                
                for (int i = 3; i < 16; i++ )
                {
                    cb_Glasgow.Items.Add(i);
                }
                cb_Glasgow.SelectedIndex = 12;
                //cb_TriageOtras.SelectedIndex = 0;
             
                tratamientos();
                //cargarMedico();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }            
        }

        public void cargarPaciente(string historia)
        {     
            try
            {
                regisEmer = false;
                erroresPaciente.Clear();
                limpiarCamposPaciente();
                if (historia.Trim() != string.Empty)
                {
                    pacienteActual = NegPacientes.RecuperarPacienteID(historia);
                    if (codigoAtencionA == null)
                        atencionActual = NegAtenciones.RecuperarUltimaAtencionExt(pacienteActual.PAC_CODIGO);
                    else
                        atencionActual = NegAtenciones.RecuperarAtencionIDEmerg(Convert.ToInt32(codigoAtencionA));                                            
                    //atencionActual = NegAtenciones.AtencionID(Convert.ToInt32(codigoAtencionA));                                            
                    //atencionActual = NegAtenciones.RecuperarUltimaAtencionExt(pacienteActual.PAC_CODIGO);                    
                    HC_EMERGENCIA emergenciaTemp = NegHcEmergencia.RecuperarUltimaEmergenciaPorPaciente(pacienteActual.PAC_CODIGO);
                    if (emergenciaTemp != null)
                    {
                        modificadaEmergencia = emergenciaTemp;
                        codigoEmergencia = modificadaEmergencia.COD_EMERGENCIA;
                        cargarEmergencia(modificadaEmergencia.COD_EMERGENCIA);
                    } 
                }
                else
                    pacienteActual = null;

                if (pacienteActual != null)              
                    actualizarPaciente();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                txt_cedula.Text = pacienteActual.PAC_IDENTIFICACION;
                if (pacienteActual.PAC_GENERO == "M")
                {
                    txtGenero.Text = "Masculino";
                    dateTimeFum.Enabled = false;
                    dateTimeFum.Value = Convert.ToDateTime("1900/01/01 00:00:00");
                }
                else
                {
                    txtGenero.Text = "Femenino";
                    dateTimeFum.Enabled = true;
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
                    txt_direccion.Text = datosPacienteActual.DAP_DIRECCION_DOMICILIO;
                    txt_telefono1.Text = datosPacienteActual.DAP_TELEFONO1;
                    txt_telefono2.Text = datosPacienteActual.DAP_TELEFONO2;
                    ESTADO_CIVIL estadoCivilPaciente = NegEstadoCivil.RecuperarEstadoCivilID(Convert.ToInt16(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value));
                    cb_estadoCivil.Text = estadoCivilPaciente.ESC_NOMBRE;
                     ATENCIONES atencionPaciente;
                     if (txt_Atencion.Text.Trim() == string.Empty)
                         atencionPaciente = NegAtenciones.RecuperarUltimaAtencionExt(keyPaciente);
                     else
                     {
                         atencionPaciente =NegAtenciones.RecuperarAtencionIDEmerg(Convert.ToInt32(txt_Atencion.Text.Trim()));
                         dtpFechaIngreso.Value = Convert.ToDateTime(atencionPaciente.ATE_FECHA_INGRESO.Value);
                         if (atencionPaciente.ATE_FECHA_ALTA!=null)
                             dtpFechaAlta.Value = Convert.ToDateTime(atencionPaciente.ATE_FECHA_ALTA.Value);
                         else
                             dtpFechaAlta.Value = Convert.ToDateTime(atencionPaciente.ATE_FECHA_INGRESO.Value);
                     }

                    //ATENCION_FORMAS_LLEGADA formaLlegada = NegAtencionesFormasLlegada.atencionesFormasLlegadaPorAtencion(atencionPaciente.ATE_CODIGO);
                    txtFormaLlegada.Text = atencionPaciente.ATENCION_FORMAS_LLEGADA.AFL_DESCRIPCION;
                    cb_medico.Text = atencionPaciente.MEDICOS.MED_APELLIDO_PATERNO + " " + atencionPaciente.MEDICOS.MED_APELLIDO_MATERNO + " " + atencionPaciente.MEDICOS.MED_NOMBRE1;      
                    //ATENCIONES atencion = NegAtenciones.RecuperarUltimaAtencion((int)pacienteActual.EntityKey.EntityKeyValues[0].Value);
                    //ATENCION_DETALLE_CATEGORIAS atencionCategoria = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(atencion.ATE_CODIGO).OrderByDescending(a=>a.ADA_FECHA_INICIO).FirstOrDefault();
                    //CATEGORIAS_CONVENIOS categoria = NegCategorias.RecuperaCategoriaID((Int16)atencionCategoria.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value);
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); } 
        }

        private void CargarEmpresa(string numRuc)
        {
            empresaPaciente = NegAseguradoras.RecuperarEmpresa(numRuc);
            if (empresaPaciente != null)
            {
                //txt_rucEmp.Text = empresaPaciente.ASE_RUC;
                //txt_empresa.Text = empresaPaciente.ASE_NOMBRE;
                //txt_direcEmp.Text = empresaPaciente.ASE_DIRECCION;
                //txt_ciudadEmp.Text = empresaPaciente.ASE_CIUDAD;
                //txt_telfEmp.Text = empresaPaciente.ASE_TELEFONO;
            }
        }

        
        #endregion


        # region Otros

        private void limpiarCamposPaciente()
        {
            //Datos Paciente
            //txtRegistro.Text = string.Empty;
            //txt_historiaclinica.Text = string.Empty;
            txt_NombreH1.Text = string.Empty;
            txt_NombreH2.Text = string.Empty;
            txt_ApellidoH1.Text = string.Empty;
            txt_ApellidoH2.Text = string.Empty;
            txt_cedula.Text = string.Empty;
            //cb_Etnia.SelectedIndex = 0;    
            cb_Etnia.Text = string.Empty;  
            //cb_estadoCivil.SelectedIndex = 0; 
            cb_estadoCivil.Text = string.Empty; 
            dtpFecNacimiento.Text = "";
            txtGenero.Text = ""; 
            dtFecIngreso.Text = "";
            txt_direccion.Text = string.Empty;
            txt_telefono1.Text = string.Empty;
            txt_telefono2.Text = string.Empty;
            txt_observacion.Text = string.Empty;            

            //Enfermedades Previas

            limpiarDatos();            
        }

        //private void cargarMedico()
        //{
        //    cb_medico.DataSource = medico;
        //    cb_medico.ValueMember = "MED_CODIGO";
        //    cb_medico.DisplayMember = "MED_APELLIDO_PATERNO" + "MED_APELLIDO_MATERNO" + "MED_NOMBRE1" +"MED_NOMBRE2";
        //    cb_medico.AutoCompleteSource = AutoCompleteSource.ListItems;
        //    cb_medico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    //cb_medico.DataSource = medico.MED_CODIGO.ToString();
        //    //txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;

        //}

        private void tratamientos()
        {
            try
            {
                chbL_SelecTratam.DataSource = null;
                catalogoTratamiento = NegCatalogos.RecuperarHcCatalogosPorTipo(15);                
                chbL_SelecTratam.DataSource = catalogoTratamiento;
                chbL_SelecTratam.ValueMember = "HCC_CODIGO";
                chbL_SelecTratam.DisplayMember = "HCC_NOMBRE";
                chbL_CirugiasPrev.DataSource = null;
                catalogoCirugiasPrevias = NegCatalogos.RecuperarHcCatalogosPorTipo(25);
                chbL_CirugiasPrev.DataSource = catalogoCirugiasPrevias;
                chbL_CirugiasPrev.ValueMember = "HCC_CODIGO";
                chbL_CirugiasPrev.DisplayMember = "HCC_NOMBRE";
                chdL_Complicaciones.DataSource = null;
                catalogoComplicaciones = NegCatalogos.RecuperarHcCatalogosPorTipo(16);
                chdL_Complicaciones.DataSource = catalogoComplicaciones;
                chdL_Complicaciones.ValueMember = "HCC_CODIGO";
                chdL_Complicaciones.DisplayMember = "HCC_NOMBRE";                            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        

        private void cargarEnferPreven(CIE10 cie10) 
        {            
            CIE10 cie = new CIE10();
            if (listaCIE.Count == 0)            
                listaCIE.Add(cie10);         
            else 
            {
                string cod = cie10.CIE_CODIGO;
                bool band = false;
                for (int i = 0; i < listaCIE.Count; i++)
                {
                    cie = new CIE10();
                    cie = listaCIE[i];
                    if (cie.CIE_CODIGO.Equals(cod))
                    {
                        band = true;
                        i = listaCIE.Count;
                    }
                }
                if (band == false)                
                    listaCIE.Add(cie10);         
                else
                    MessageBox.Show("Convenio1 / Categoria no puede repetirse");    
            }
            gridEnfPrevias.DataSource= null;
            gridEnfPrevias.DataSource = listaCIE;            
        }

        private void panelFiltros2_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            gridEnfPrevias.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            gridEnfPrevias.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

            gridEnfPrevias.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            gridEnfPrevias.DisplayLayout.Bands[0].Columns["CIE_CODIGO"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
            gridEnfPrevias.DisplayLayout.Bands[0].Columns["CIE_DESCRIPCION"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
            gridEnfPrevias.DisplayLayout.Bands[0].Columns["CIE_IDPADRE"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;

            gridEnfPrevias.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            gridEnfPrevias.DisplayLayout.Override.DefaultRowHeight = 20;
            gridEnfPrevias.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
            
            gridEnfPrevias.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;

            gridEnfPrevias.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
            gridEnfPrevias.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
            gridEnfPrevias.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;


            gridEnfPrevias.DisplayLayout.Bands[0].Columns["CIE_CODIGO"].Hidden = false;
            gridEnfPrevias.DisplayLayout.Bands[0].Columns["CIE_DESCRIPCION"].Hidden = false;
            gridEnfPrevias.DisplayLayout.Bands[0].Columns["CIE_IDPADRE"].Hidden = true;           
        }

        //private void cargarBusquedaICD(CIE10 cie10)
        //{
        //    listaBusqICD = new List<CIE10>();
        //    CIE10 cie = new CIE10();
        //    listaBusqICD.Add(cie10);            
        //    gridBusquedaICD.DataSource = null;
        //    gridBusquedaICD.DataSource = listaBusqICD;
        //}

        //private void panelFiltros3_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        //{
        //    gridBusquedaICD.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
        //    gridBusquedaICD.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

        //    gridBusquedaICD.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
        //    gridBusquedaICD.DisplayLayout.Bands[0].Columns["CIE_CODIGO"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
        //    gridBusquedaICD.DisplayLayout.Bands[0].Columns["CIE_DESCRIPCION"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
        //    gridBusquedaICD.DisplayLayout.Bands[0].Columns["CIE_IDPADRE"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;

        //    gridBusquedaICD.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
        //    gridBusquedaICD.DisplayLayout.Override.DefaultRowHeight = 20;
        //    gridBusquedaICD.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

        //    gridBusquedaICD.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;

        //    gridBusquedaICD.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
        //    gridBusquedaICD.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
        //    gridBusquedaICD.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;


        //    gridBusquedaICD.DisplayLayout.Bands[0].Columns["CIE_CODIGO"].Hidden = true;
        //    gridBusquedaICD.DisplayLayout.Bands[0].Columns["CIE_DESCRIPCION"].Hidden = false;
        //    gridBusquedaICD.DisplayLayout.Bands[0].Columns["CIE_IDPADRE"].Hidden = true;
        //}

        private bool validarFormulario()
        {
            erroresPaciente.Clear();
            bool valido = false;           
            if (dataGridViewSubSintomas.Rows.Count == 0)
            {
                AgregarError(dataGridViewSubSintomas);
                valido = true;
            }
            if (txtTA1.Text.Trim() == string.Empty)
            {
                AgregarError(txtTA1);
                valido = true;
            }
            if (txtTA2.Text.Trim() == string.Empty)
            {
                AgregarError(txtTA2);
                valido = true;
            }
            if (txtT.Text.Trim() == string.Empty)
            {
                AgregarError(txtT);
                valido = true;
            }
            if (txtSat.Text.Trim() == string.Empty)
            {
                AgregarError(txtSat);
                valido = true;
            }
            if (txtFc.Text.Trim() == string.Empty)
            {
                AgregarError(txtFc);
                valido = true;
            }
            if (txtFr.Text.Trim() == string.Empty)
            {
                AgregarError(txtFr);
                valido = true;
            }
            if (cb_Glasgow.SelectedIndex == -1)
            {
                AgregarError(cb_Glasgow);
                valido = true;
            }
            if (rdb_NoUrgente.Checked == false & rdb_Critico.Checked == false & rdb_Emergencia.Checked == false & rdb_Otras.Checked == false)
            {
                AgregarError(uGroupBoxTriage);
                valido = true;
            }
            if (txtOrl.Text.Trim() == string.Empty)
            {
                AgregarError(txtOrl);
                valido = true;
            }
            if (txtCabeza.Text.Trim() == string.Empty)
            {
                AgregarError(txtCabeza);
                valido = true;
            }
            if (txtCuello.Text.Trim() == string.Empty)
            {
                AgregarError(txtCuello);
                valido = true;
            }
            if (txtTorax.Text.Trim() == string.Empty)
            {
                AgregarError(txtTorax);
                valido = true;
            }
            if (txtCardiaco.Text.Trim() == string.Empty)
            {
                AgregarError(txtCardiaco);
                valido = true;
            }
            if (txtPulmonar.Text.Trim() == string.Empty)
            {
                AgregarError(txtPulmonar);
                valido = true;
            }
            if (txtAbdomen.Text.Trim() == string.Empty)
            {
                AgregarError(txtAbdomen);
                valido = true;
            }
            if (txtGenitales.Text.Trim() == string.Empty)
            {
                AgregarError(txtGenitales);
                valido = true;
            }
            if (txtPerine.Text.Trim() == string.Empty)
            {
                AgregarError(txtPerine);
                valido = true;
            }
            if (txtExtremidades.Text.Trim() == string.Empty)
            {
                AgregarError(txtExtremidades);
                valido = true;
            }
            if (txtFosasL.Text.Trim() == string.Empty)
            {
                AgregarError(txtFosasL);
                valido = true;
            }
            if (txtLinfa.Text.Trim() == string.Empty)
            {
                AgregarError(txtLinfa);
                valido = true;
            }
            if (txtNeuro.Text.Trim() == string.Empty)
            {
                AgregarError(txtNeuro);
                valido = true;
            }
            if (txtPiel.Text.Trim() == string.Empty)
            {
                AgregarError(txtPiel);
                valido = true;
            }
            if (txtDiagDef.Text.Trim() == string.Empty)
            {
                AgregarError(txtDiagDef);
                valido = true;
            }
            if (txtDiagEmerg.Text.Trim() == string.Empty)
            {
                AgregarError(txtDiagEmerg);
                valido = true;
            }
            if (rdb_Clinicas.Checked == false & rdb_Quirurgicas.Checked == false & rdb_EspeOtras.Checked == false)
            {
                AgregarError(groupBox20);
                valido = true;
            }            
            if (cb_Clinicas.SelectedIndex == -1 & cb_Quirurgicas.SelectedIndex == -1 & cb_OtrasEspec.SelectedIndex == -1) 
            {
                AgregarError(groupBox20);
                valido = true;                
            }  
            //Todos
            if (rdb_Alta.Checked == false & rdb_Muerto.Checked == false & rdb_IngresoPiso.Checked == false & rdb_IngresoUCI.Checked == false & rdb_Transferido.Checked == false)
            {
                AgregarError(groupBox19);
                valido = true;
            }
            if (rdb_Transferido.Checked == true)
            {
                if (txtTransferido.Text.Trim() == string.Empty)
                {
                    AgregarError(txtTransferido);
                    valido = true;
                }
            }
            if (txtExamenesReal.Text == string.Empty)
            {
                AgregarError(txtExamenesReal);
                valido = true;
            }
            if (txtTratamientos.Text == string.Empty)
            {
                AgregarError(txtTratamientos);
                valido = true;
            }
            if (chbOtrasC.Checked == true) 
            {
                if (txtOtrasActual.Text.Trim() == string.Empty) 
                {
                    AgregarError(txtOtrasActual);
                    valido = true;
                }
            }
            return valido;
        }
        private void AgregarError(Control control)
        {
            erroresPaciente.SetError(control, "Campo Requerido");
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

        # endregion
        

        //private void txt_codmedicamento_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //Buscar(e);
        //}
       
        private void txt_codmedico_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F1)
                {

                    frm_AyudaMedico help = new frm_AyudaMedico();

                    help.ShowDialog();

                    //txt_codmedico.Text = help.cod;
                    //txt_nommedico.Text = help.nombre;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void frm_Registro_Load(object sender, EventArgs e)
        {
            try
            {
                tabulador.Tabs[0].Selected=true ; 
                estadoCarga = false;
                leerUltimaEmergencia();
                estadoCarga = true;
                calcularREMS();
            }
            catch (Exception err) { MessageBox.Show(err.Message); } 
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        //private void ultraTabPageControl1_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void btnModi_Click(object sender, EventArgs e)
        //{

        //}
        private void toolStripSalir_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txt_historiaclinica_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (pacienteNuevo == false)
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        string cod = txt_Atencion.Text.Trim();
                        frm_AyudaPacientes form = new frm_AyudaPacientes();
                        form.campoAtencion = txt_Atencion;
                        form.campoPadre = txt_historiaclinica;                        
                        if(txt_Atencion.Text.Trim()!= cod)
                        {
                            //RecuperarAtencionIDEmerg
                            //atencionActual = NegAtenciones.AtencionID(Convert.ToInt32(txt_Atencion.Text.Trim()));
                            atencionActual = NegAtenciones.RecuperarAtencionIDEmerg(Convert.ToInt32(txt_Atencion.Text.Trim()));
                            actualizarPaciente();
                        }        
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void frm_Admision_Load(object sender, EventArgs e)
        {
            //CargarDatos();
        }
       

        //private void text_Apellido1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Buscar(e);
        //}

        //private void txt_historiaclinica_keyPress(object sender, KeyPressEventArgs e)
        //{            
        //}
      
        private void txt_historiaclinica_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (pacienteNuevo == false && estadoCarga == true)
                {
                    codigoAtencionA = txt_Atencion.Text.Trim();
                    if ((bool)txt_historiaclinica.Tag == true)
                    {
                        cargarPaciente(txt_historiaclinica.Text.ToString());
                        txt_historiaclinica.Tag = false;
                        if (!regisEmer)
                        {
                            txtRegistro.Text = string.Empty;
                            if(atencionActual.ATE_ESTADO != true)
                                habilitarBotones(false, false, false, false, false, true, true);
                            else
                                habilitarBotones(true, false, false, false, false, true, true);
                        }
                        else
                        {
                            if (atencionActual.ATE_ESTADO != true)
                                habilitarBotones(false, false, false, true, true, true, true);
                            else
                            {
                                habilitarBotones(true, true, true, true, true, true, true);
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

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {            
            try
            {
                if (pacienteNuevo == false)
                {
                    string cod = txt_Atencion.Text.Trim();
                    frm_AyudaPacientes form = new frm_AyudaPacientes();
                    form.campoAtencion = txt_Atencion;
                    form.campoPadre = txt_historiaclinica;                    
                    form.ShowDialog();
                    if (txt_Atencion.Text.Trim() != cod)
                    {
                        //RecuperarAtencionIDEmerg
                        //atencionActual = NegAtenciones.AtencionID(Convert.ToInt32(txt_Atencion.Text.Trim()));
                        atencionActual = NegAtenciones.RecuperarAtencionIDEmerg(Convert.ToInt32(txt_Atencion.Text.Trim()));
                        actualizarPaciente();
                    }   
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTratamiento_Click(object sender, EventArgs e)
        {
            try
            {
                HC_CATALOGOS_TIPO tipoCodigo = NegCatalogos.RecuperarHcCatalogoTipo(11);                      
                frm_TipoCatalogo form = new frm_TipoCatalogo(tipoCodigo);
                form.ShowDialog();
            }
             catch (Exception er)
             {
                 MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }

        private void btnCirugiasPrevias_Click(object sender, EventArgs e)
        {
            try
            {
                HC_CATALOGOS_TIPO tipoCodigo = NegCatalogos.RecuperarHcCatalogoTipo(25);
                frm_TipoCatalogo form = new frm_TipoCatalogo(tipoCodigo);
                form.ShowDialog();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ayudaEnfPrevias_Click(object sender, EventArgs e)
        {
            try
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                CIE10 cie10 = new CIE10();
                cie10.CIE_CODIGO = busqueda.codigo;
                cie10.CIE_DESCRIPCION = busqueda.resultado;
                cargarEnferPreven(cie10);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgSint_Click(object sender, EventArgs e)
        {
            try
            {
                cargarSubsintomas(true);               
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cargarSubsintomas(Boolean accion)
        {
            try
            {
                Boolean valor = true;
                if (accion == true)
                {
                    frm_AyudaCatalogoSubNivel subSintoma = new frm_AyudaCatalogoSubNivel();
                    subSintoma.ShowDialog();
                    if (subSintoma.resultado != null)
                    {
                        resultadoSub = subSintoma.resultado;
                        descripcionSub = subSintoma.descripcion;                        
                    }
                    else                    
                        valor = false;                                                        
                }
                if (valor) 
                {
                    DataRow dr;                    
                    dataGridViewSubSintomas.DataSource = dt;
                    dataGridViewSubSintomas.DataSource = dt;                    
                    if (dataGridViewSubSintomas.Rows.Count == 0)
                    {
                        dt.Columns.Add("SUBSINTOMA", Type.GetType("System.String"));
                        dt.Columns.Add("DESCRIPCION", Type.GetType("System.String"));

                    }
                    if (dataGridViewSubSintomas.Rows.Count >= 1)
                    {
                        dr = dt.NewRow();
                        dr["SUBSINTOMA"] = resultadoSub;
                        dr["DESCRIPCION"] = descripcionSub;
                        dt.Rows.Add(dr);                        
                    }
                }       
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridEnfPrevias_DoubleClick(object sender, EventArgs e)
        {
            if (gridEnfPrevias.ActiveRow != null)
            {
                if (emerNuevo == false)
                {
                    CIE10 cie = new CIE10();
                    cie = (CIE10)gridEnfPrevias.ActiveRow.ListObject;
                    List<HC_EMERGENCIA_EPREVIAS> listaEfer = new List<HC_EMERGENCIA_EPREVIAS>();
                    listaEfer = NegHcEmergenciaEnfPrevias.RecuperarHcEmergenciaEnfPrevias(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaEfer.Count; i++)
                    {
                        HC_EMERGENCIA_EPREVIAS previa = new HC_EMERGENCIA_EPREVIAS();
                        previa = listaEfer.ElementAt(i);
                        if (previa.EFP_CIE10 == cie.CIE_CODIGO)
                        {
                            NegHcEmergenciaEnfPrevias.EliminarHcEmergenciaEnfPrevias(previa);
                            gridEnfPrevias.DeleteSelectedRows();
                        }
                    }
                }
                else
                {
                    List<CIE10> nuevaLista = new List<CIE10>();
                    CIE10 cie = new CIE10();
                    CIE10 cie10 = new CIE10();
                    cie10 = (CIE10)gridEnfPrevias.ActiveRow.ListObject;
                    if (listaCIE.Count == 1)
                    {
                        listaCIE = new List<CIE10>();
                        gridEnfPrevias.DataSource = null;
                    }
                    else
                    {
                        string cod = cie10.CIE_CODIGO;
                        for (int i = 0; i < listaCIE.Count; i++)
                        {
                            cie = new CIE10();
                            cie = listaCIE[i];
                            if (cie.CIE_CODIGO.Equals(cod))
                            {
                            }
                            else
                                nuevaLista.Add(cie);
                        }
                    }
                    listaCIE = nuevaLista;
                    gridEnfPrevias.DataSource = null;
                    gridEnfPrevias.DataSource = listaCIE;
                }
            }
        }
  
        private void dataGridViewSubSintomasSelec_DoubleClick(object sender, EventArgs e)
        {            
        }

        private void btn_TramientoActual_Click(object sender, EventArgs e)
        {
            HC_CATALOGOS_TIPO tipoCodigo = NegCatalogos.RecuperarHcCatalogoTipo(15);
            frm_TipoCatalogo form = new frm_TipoCatalogo(tipoCodigo);
            form.ShowDialog();
        }

        private void btn_Procedimiento_Click(object sender, EventArgs e)
        {
            HC_CATALOGOS_TIPO tipoCodigo = NegCatalogos.RecuperarHcCatalogoTipo(13);
            frm_TipoCatalogo form = new frm_TipoCatalogo(tipoCodigo);
            form.ShowDialog();
        }

        private void rdb_Otras_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Otras.Checked) 
                rShapeTriage.Appearance.BackColor = Color.Black;    
        }

        private void rdb_Critico_CheckedChanged(object sender, EventArgs e)
        {   
            if(rdb_Critico.Checked)
                rShapeTriage.Appearance.BackColor = Color.Red;
        }


        private void rdb_NoUrgente_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_NoUrgente.Checked)
                rShapeTriage.Appearance.BackColor = Color.Green;    
        }

        private void rdb_Emergencia_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Emergencia.Checked)
                rShapeTriage.Appearance.BackColor = Color.Orange;                    
        }

        private void btn_BusquedaICD_Click(object sender, EventArgs e)
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            txtDiagDef.Text = busqueda.resultado;
            txtDiagDef.Tag = busqueda.codigo;      
        }

        //private void gridBusquedaICD_DoubleClick(object sender, EventArgs e)
        //{
        //    if (gridBusquedaICD.ActiveRow != null)
        //    {
        //        listaBusqICD = new List<CIE10>();
        //        gridBusquedaICD.DataSource = null;
        //        gridBusquedaICD.DataSource = listaBusqICD;
        //    }
        //}

        private void rdb_Alta_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Alta.Checked == true)
            {               
                txtTransferido.Text = string.Empty; 
                txtTransferido.Enabled = false; 
                rdb_Transferido.Checked = false;
            }
        }

        private void rdb_IngresoPiso_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_IngresoPiso.Checked == true)
            {
                txtTransferido.Text = string.Empty;
                txtTransferido.Enabled = false; 
                rdb_Transferido.Checked = false;
            }
        }

        //private void rdb_IngresoQuirofano_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdb_IngresoQuirofano.Checked == true)
        //    {
        //        //cb_Transferido.SelectedIndex = -1;
        //        //cb_Transferido.Enabled = false;
        //        txtTransferido.Enabled = false; 
        //        rdb_Transferido.Checked = false;
        //    }

        //}

        //private void rdb_IngresoCoron_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdb_IngresoCoron.Checked == true)
        //    {
        //        //cb_Transferido.SelectedIndex = -1;
        //        //cb_Transferido.Enabled = false;
        //        txtTransferido.Enabled = false; 
        //        rdb_Transferido.Checked = false;
        //    }

        //}

        private void chbOtrasC_CheckedChanged(object sender, EventArgs e)
        {
            txtOtrasActual.Text = string.Empty;
            if (chbOtrasC.Checked == true)
                txtOtrasActual.Enabled = true;
            else
                txtOtrasActual.Enabled = false;
        }

        private void rdb_Clinicas_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Clinicas.Checked == true)
            {
                cb_Clinicas.SelectedIndex = -1;
                cb_Clinicas.Enabled = true;
                cb_Quirurgicas.SelectedIndex = -1;
                cb_Quirurgicas.Enabled = false;
                cb_OtrasEspec.SelectedIndex = -1;
                cb_OtrasEspec.Enabled = false;
                rdb_EspeOtras.Checked = false;
                rdb_Quirurgicas.Checked = false;
            }
        }

        private void rdb_Quirurgicas_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Quirurgicas.Checked == true)
            {
                cb_Clinicas.SelectedIndex = -1;
                cb_Clinicas.Enabled = false;
                cb_Quirurgicas.SelectedIndex = -1;
                cb_Quirurgicas.Enabled = true;
                cb_OtrasEspec.SelectedIndex = -1;
                cb_OtrasEspec.Enabled = false;
                rdb_EspeOtras.Checked = false;
                rdb_Clinicas.Checked = false;
            }
        }

        private void rdb_EspeOtras_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_EspeOtras.Checked == true)
            {
                cb_Clinicas.SelectedIndex = -1;
                cb_Clinicas.Enabled = false;
                cb_Quirurgicas.SelectedIndex = -1;
                cb_Quirurgicas.Enabled = false;
                cb_OtrasEspec.SelectedIndex = -1;
                cb_OtrasEspec.Enabled = true;
                rdb_Clinicas.Checked = false;
                rdb_Quirurgicas.Checked = false;
            }

        }

        private void rdb_IngresoUCI_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_IngresoUCI.Checked == true)
            {
                //cb_Transferido.SelectedIndex = -1;
                //cb_Transferido.Enabled = false;
                txtTransferido.Text = string.Empty;
                txtTransferido.Enabled = false; 
                rdb_Transferido.Checked = false;
            }

        }

        private void rdb_Muerto_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Muerto.Checked == true)
            {
                //cb_Transferido.SelectedIndex = -1;
                //cb_Transferido.Enabled = false;
                txtTransferido.Text = string.Empty;
                txtTransferido.Enabled = false; 
                rdb_Transferido.Checked = false;
            }

        }

        private void rdb_Transferido_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Transferido.Checked == true)
            {
                //cb_Transferido.SelectedIndex = 0;
                //cb_Transferido.Enabled = true;
                txtTransferido.Text = string.Empty;
                txtTransferido.Enabled = true;
            }
        }

        private void chb_SelecTratami_CheckedChanged(object sender, EventArgs e)
        {
            List<HC_CATALOGOS> lista = new List<HC_CATALOGOS>();
            HC_CATALOGOS catal = new HC_CATALOGOS();
            HC_CATALOGOS cat = new HC_CATALOGOS();
            for (int i = 0; i < chbL_SelecTratam.Items.Count; i++)
            {
                if (chbL_SelecTratam.GetItemCheckState(i) == CheckState.Checked)
                {
                    catal = catalogoTratamiento.ElementAt(i);
                    lista.Add(catal); 
                }         
            }
            if (chb_SelecTratami.Checked == true)
            {
                cat = new HC_CATALOGOS();
                List<HC_CATALOGOS> listaCat = new List<HC_CATALOGOS>();
                for (int i = 0; i < chbL_SelecTratam.Items.Count; i++)
                {
                    if (chbL_SelecTratam.GetItemCheckState(i) == CheckState.Checked)
                    {
                        cat = catalogoTratamiento.ElementAt(i);
                        listaCat.Add(cat);                    
                    }            
                }
                if (listaCat.Count > (0))
                {
                    chbL_SelecTratam.DataSource = listaCat;
                    chbL_SelecTratam.ValueMember = "HCC_CODIGO";
                    chbL_SelecTratam.DisplayMember = "HCC_NOMBRE";
                    for (int i = 0; i < chbL_SelecTratam.Items.Count; i++)
                        chbL_SelecTratam.SetItemChecked(i, true);
                }
                else 
                {
                    chbL_SelecTratam.DataSource = catalogoTratamiento;
                    chbL_SelecTratam.ValueMember = "HCC_CODIGO";
                    chbL_SelecTratam.DisplayMember = "HCC_NOMBRE";
                }                               
            }
            else
            {
                chbL_SelecTratam.DataSource = catalogoTratamiento;
                chbL_SelecTratam.ValueMember = "HCC_CODIGO";
                chbL_SelecTratam.DisplayMember = "HCC_NOMBRE";
                if (lista.Count > (0))
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        catal = new HC_CATALOGOS();
                        catal = lista.ElementAt(i);
                        for (int j = 0; j < catalogoTratamiento.Count; j++) 
                        {
                            cat = new HC_CATALOGOS();
                            cat = catalogoTratamiento.ElementAt(j);
                            if(cat.HCC_CODIGO == catal.HCC_CODIGO)
                            {
                                chbL_SelecTratam.SetItemChecked(j, true);
                            }
                        }
                    }                   
                }                                             
            }
        }

        private void chb_SelecCirugias_CheckedChanged(object sender, EventArgs e)
        {
            List<HC_CATALOGOS> lista = new List<HC_CATALOGOS>();
            HC_CATALOGOS catal = new HC_CATALOGOS();
            HC_CATALOGOS cat = new HC_CATALOGOS();
            for (int i = 0; i < chbL_CirugiasPrev.Items.Count; i++)
            {
                if (chbL_CirugiasPrev.GetItemCheckState(i) == CheckState.Checked)
                {
                    catal = catalogoCirugiasPrevias.ElementAt(i);
                    lista.Add(catal);
                }
            }
            if (chb_SelecCirugias.Checked == true)
            {
                cat = new HC_CATALOGOS();
                List<HC_CATALOGOS> listaCat = new List<HC_CATALOGOS>();
                for (int i = 0; i < chbL_CirugiasPrev.Items.Count; i++)
                {
                    if (chbL_CirugiasPrev.GetItemCheckState(i) == CheckState.Checked)
                    {
                        cat = catalogoCirugiasPrevias.ElementAt(i);
                        listaCat.Add(cat);
                    }
                }
                if (listaCat.Count > (0))
                {
                    chbL_CirugiasPrev.DataSource = listaCat;
                    chbL_CirugiasPrev.ValueMember = "HCC_CODIGO";
                    chbL_CirugiasPrev.DisplayMember = "HCC_NOMBRE";
                    for (int i = 0; i < chbL_CirugiasPrev.Items.Count; i++)
                        chbL_CirugiasPrev.SetItemChecked(i, true);
                }
                else
                {
                    chbL_CirugiasPrev.DataSource = catalogoCirugiasPrevias;
                    chbL_CirugiasPrev.ValueMember = "HCC_CODIGO";
                    chbL_CirugiasPrev.DisplayMember = "HCC_NOMBRE";
                }
            }
            else
            {
                chbL_CirugiasPrev.DataSource = catalogoCirugiasPrevias;
                chbL_CirugiasPrev.ValueMember = "HCC_CODIGO";
                chbL_CirugiasPrev.DisplayMember = "HCC_NOMBRE";
                if (lista.Count > (0))
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        catal = new HC_CATALOGOS();
                        catal = lista.ElementAt(i);
                        for (int j = 0; j < catalogoCirugiasPrevias.Count; j++)
                        {
                            cat = new HC_CATALOGOS();
                            cat = catalogoCirugiasPrevias.ElementAt(j);
                            if (cat.HCC_CODIGO == catal.HCC_CODIGO)
                            {
                                chbL_CirugiasPrev.SetItemChecked(j, true);
                            }
                        }
                    }
                }
            }
        }

        //private void chbL_SelecTratam_SelectedIndexChanged(object sender, EventArgs e)
        //{
            

        //}

        //private void chbL_CirugiasPrev_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        private void chb_ComplSelec_CheckedChanged(object sender, EventArgs e)
        {
            List<HC_CATALOGOS> lista = new List<HC_CATALOGOS>();
            HC_CATALOGOS catal = new HC_CATALOGOS();
            HC_CATALOGOS cat = new HC_CATALOGOS();
            for (int i = 0; i < chdL_Complicaciones.Items.Count; i++)
            {
                if (chdL_Complicaciones.GetItemCheckState(i) == CheckState.Checked)
                {
                    catal = catalogoComplicaciones.ElementAt(i);
                    lista.Add(catal);
                }
            }
            if (chb_ComplSelec.Checked == true)
            {
                cat = new HC_CATALOGOS();
                List<HC_CATALOGOS> listaCat = new List<HC_CATALOGOS>();
                for (int i = 0; i < chdL_Complicaciones.Items.Count; i++)
                {
                    if (chdL_Complicaciones.GetItemCheckState(i) == CheckState.Checked)
                    {
                        cat = catalogoComplicaciones.ElementAt(i);
                        listaCat.Add(cat);
                    }
                }
                if (listaCat.Count > (0))
                {
                    chdL_Complicaciones.DataSource = listaCat;
                    chdL_Complicaciones.ValueMember = "HCC_CODIGO";
                    chdL_Complicaciones.DisplayMember = "HCC_NOMBRE";
                    for (int i = 0; i < chdL_Complicaciones.Items.Count; i++)
                        chdL_Complicaciones.SetItemChecked(i, true);
                }
                else
                {
                    chdL_Complicaciones.DataSource = catalogoComplicaciones;
                    chdL_Complicaciones.ValueMember = "HCC_CODIGO";
                    chdL_Complicaciones.DisplayMember = "HCC_NOMBRE";
                }
            }
            else
            {
                chdL_Complicaciones.DataSource = catalogoComplicaciones;
                chdL_Complicaciones.ValueMember = "HCC_CODIGO";
                chdL_Complicaciones.DisplayMember = "HCC_NOMBRE";
                if (lista.Count > (0))
                {
                    for (int i = 0; i < lista.Count; i++)
                    {
                        catal = new HC_CATALOGOS();
                        catal = lista.ElementAt(i);
                        for (int j = 0; j < catalogoComplicaciones.Count; j++)
                        {
                            cat = new HC_CATALOGOS();
                            cat = catalogoComplicaciones.ElementAt(j);
                            if (cat.HCC_CODIGO == catal.HCC_CODIGO)
                            {
                                chdL_Complicaciones.SetItemChecked(j, true);
                            }
                        }
                    }
                }
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string numHistoria = txt_historiaclinica.Text.ToString();
            pacienteNuevo = false;            
            emerNuevo = true;           
            cargarPaciente(numHistoria);
            txt_historiaclinica.Tag = false;
            habilitarBotones(false, false, true, false, true, true, true);
            codigoEmergencia = NegHcEmergencia.RecuperaMaximoHcEmergencia()+1;
            txtRegistro.Text = codigoEmergencia.ToString();
            limpiarDatos();           
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {            
            guardarDatos();
        }       


        //#region Eventos sobre la BDD

        private void guardarDatos()
        {
            try
            {
                if (!validarFormulario())
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if (emerNuevo == true)
                        {
                            //INGRESO NUEVA EMERGENCIA   
                            codigoEmergencia = NegHcEmergencia.RecuperaMaximoHcEmergencia();
                            codigoEmergencia++;
                            nuevaEmergencia.COD_EMERGENCIA = codigoEmergencia;
                            nuevaEmergencia.PACIENTESReference.EntityKey = pacienteActual.EntityKey;
                            nuevaEmergencia.EME_FECHA = DateTime.Now;
                            nuevaEmergencia.ATE_CODIGO = atencionActual.ATE_CODIGO; 
                            agregarDatosHcEmergencia();
                            cargarDatosTablas();
                            NegHcEmergencia.crearHcEmergencia(nuevaEmergencia);                            
                            MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            habilitarBotones(true, true, true, true, true, true, true);                            
                            buscarHistorias.Enabled = true;
                            emerNuevo = false;
                            cargarDatosImpresion(nuevaEmergencia.COD_EMERGENCIA, nuevaEmergencia);
                            nuevaEmergencia = new HC_EMERGENCIA();                                                   
                        }
                        else
                        {
                            //MODIFICAR EMERGENCIA
                            agregarDatosHcEmergencia();
                            nuevaEmergencia.COD_EMERGENCIA = modificadaEmergencia.COD_EMERGENCIA;
                            modificadaEmergencia = nuevaEmergencia;
                            codigoEmergencia = modificadaEmergencia.COD_EMERGENCIA;
                            NegHcEmergencia.actualizarHcEmergencia(modificadaEmergencia);
                            cargarDatosTablasModificadas(modificadaEmergencia.COD_EMERGENCIA);
                            emerNuevo = false;
                            buscarHistorias.Enabled = true;
                            MessageBox.Show("Datos Corriguidos Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            habilitarBotones(true, true, true, true, true, true, true);
                            cargarDatosImpresion(codigoEmergencia, modificadaEmergencia);
                        }
                        //ultraTabPageControl1.Focus();
                       
                        tabulador.SelectedTab = tabulador.Tabs[0];
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese Campos obligatorios");
                }                
            }               
            catch (Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos: \n" + e.InnerException);
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void agregarDatosHcEmergencia() 
        {
            try
            {                               
                nuevaEmergencia.EME_ALERGIAS = txtAlergias.Text.Trim();
                nuevaEmergencia.EME_FUM = dateTimeFum.Value;
                nuevaEmergencia.EME_FECHA_INICIO = DateTime.Now;
                nuevaEmergencia.EME_TA = txtTA1.Text.Trim() + "/" + txtTA2.Text.Trim();
                nuevaEmergencia.EME_T = txtT.Text.Trim();
                nuevaEmergencia.EME_SAT = txtSat.Text.Trim();
                nuevaEmergencia.EME_FC = txtFc.Text.Trim();
                nuevaEmergencia.EME_FR = txtFr.Text.Trim();

                nuevaEmergencia.EME_ORL = txtOrl.Text.Trim();
                nuevaEmergencia.EME_CABEZA = txtCabeza.Text.Trim();
                nuevaEmergencia.EME_CUELLO = txtCuello.Text.Trim();
                nuevaEmergencia.EME_TORAX = txtTorax.Text.Trim();
                nuevaEmergencia.EME_CARDIACO = txtCardiaco.Text.Trim();
                nuevaEmergencia.EME_PULMONAR = txtPulmonar.Text.Trim();
                nuevaEmergencia.EME_ABDOMEN = txtAbdomen.Text.Trim();
                nuevaEmergencia.EME_GENETALES = txtGenitales.Text.Trim();
                nuevaEmergencia.EME_PERINE = txtPerine.Text.Trim();
                nuevaEmergencia.EME_EXTREMIDADES = txtExtremidades.Text;
                nuevaEmergencia.EME_FOSAS_LUM = txtFosasL.Text.Trim();
                nuevaEmergencia.EME_LINFATICO = txtLinfa.Text.Trim();
                nuevaEmergencia.EME_NEUROLOGICO = txtNeuro.Text.Trim();
                nuevaEmergencia.EME_PIEL = txtPiel.Text.Trim();
                nuevaEmergencia.EME_OBSERVACIONES = txtObserEnfer.Text.Trim();
                nuevaEmergencia.EME_DIAG_EMER = txtDiagEmerg.Text.Trim();
                nuevaEmergencia.EME_DIAG_DEFENITICO = txtDiagDef.Text.Trim();
                nuevaEmergencia.EME_OBSER_FINAL = txtObserFinal.Text.Trim();
                nuevaEmergencia.EME_RESUMEN = txtResumenDiag.Text.Trim();
                nuevaEmergencia.EME_PROCEDIMIENTOS = txtProcedimientos.Text.Trim();
                nuevaEmergencia.EME_EVO_COMP = txtExamenesReal.Text.Trim();
                nuevaEmergencia.EME_TRATAMIENTOS = txtTratamientos.Text.Trim();
                HC_CATALOGOS cat = new HC_CATALOGOS();
                ////cb_TratamientoActual
                ////cat = new HC_CATALOGOS();
                //HC_CATALOGOS catalogoCombo1 = (HC_CATALOGOS)cb_TratamientoActual.SelectedItem;
                //nuevaEmergencia.HCC_CAT_TRATAMIENTO = catalogoCombo1.HCC_CODIGO;    
                
                ////cb_AtencionMedica
                ////cat = new HC_CATALOGOS();
                //HC_CATALOGOS catalogoCombo2 = (HC_CATALOGOS)cb_AtencionMedica.SelectedItem;
                //nuevaEmergencia.HCC_CAT_ATENCION = catalogoCombo2.HCC_CODIGO;     
                                
                ////cb_Procedimiento
                ////cat = new HC_CATALOGOS();
                //HC_CATALOGOS catalogoCombo3 = (HC_CATALOGOS)cb_Procedimiento.SelectedItem;
                //nuevaEmergencia.HCC_CAT_PROCEDIMIENTO = catalogoCombo3.HCC_CODIGO;

                //cat = new HC_CATALOGOS();
                if(cb_Clinicas.SelectedIndex != -1)
                {
                    HC_CATALOGOS catalogoCombo4 = (HC_CATALOGOS)cb_Clinicas.SelectedItem;
                    nuevaEmergencia.ESP_CLIN = catalogoCombo4.HCC_CODIGO;
                    nuevaEmergencia.ESP_QUIR = 0;
                    nuevaEmergencia.ESP_OTRAS = 0;
                }else
                {
                    if (cb_Quirurgicas.SelectedIndex != -1)
                    {
                        HC_CATALOGOS catalogoCombo5 = (HC_CATALOGOS)cb_Quirurgicas.SelectedItem;
                        nuevaEmergencia.ESP_QUIR = catalogoCombo5.HCC_CODIGO;
                        nuevaEmergencia.ESP_CLIN= 0;
                        nuevaEmergencia.ESP_OTRAS = 0;
                    }
                    else
                    {
                        HC_CATALOGOS catalogoCombo6 = (HC_CATALOGOS)cb_OtrasEspec.SelectedItem;
                        nuevaEmergencia.ESP_OTRAS = catalogoCombo6.HCC_CODIGO;
                        nuevaEmergencia.ESP_CLIN = 0;
                        nuevaEmergencia.ESP_QUIR = 0;
                    }
                }
                if (chbAlcoholE.Checked == true)                
                    nuevaEmergencia.EME_ALCOHOL = true;                
                else                 
                    nuevaEmergencia.EME_ALCOHOL = false;                
                if (chbTabacoE.Checked == true)                
                    nuevaEmergencia.EME_TABACO = true;                
                else                
                    nuevaEmergencia.EME_TABACO = false;                
                if (chbDrogasE.Checked == true)                
                    nuevaEmergencia.EME_DROGAS = true;                
                else                
                    nuevaEmergencia.EME_DROGAS = false;
                //if (rdb_Directo.Checked == true)
                //    nuevaEmergencia.EME_DIRECTO = true;
                //else
                //    nuevaEmergencia.EME_DIRECTO = false;
                //if (rdb_Indirecto.Checked == true)
                //    nuevaEmergencia.EME_INDIRECTO = true;
                //else
                //    nuevaEmergencia.EME_INDIRECTO = false;
                if (cb_Glasgow.SelectedIndex > -1)      
                    nuevaEmergencia.EME_GLASGOW = Convert.ToInt32(cb_Glasgow.SelectedItem);                                
                else
                    nuevaEmergencia.EME_GLASGOW = 0;
                if (rdb_NoUrgente.Checked == true)
                    nuevaEmergencia.EME_NOURGE = true;
                else
                    nuevaEmergencia.EME_NOURGE = false;
                if (rdb_Emergencia.Checked == true)
                    nuevaEmergencia.EME_EMERGENCIA = true;
                else
                    nuevaEmergencia.EME_EMERGENCIA = false;
                if (rdb_Critico.Checked == true)
                    nuevaEmergencia.EME_CRITICO = true;
                else
                    nuevaEmergencia.EME_CRITICO = false;                             
                
                if (chbAlcoholC.Checked == true)
                    nuevaEmergencia.EME_ALCOHOL_ACTUAL = true;
                else
                    nuevaEmergencia.EME_ALCOHOL_ACTUAL = false;
                if (chbOtrasC.Checked == true)
                {
                    nuevaEmergencia.EME_TABACO_ACTUAL = true;
                    if (txtOtrasActual.Text != string.Empty) 
                    {
                        nuevaEmergencia.EME_OTRAS_ACTUAL = txtOtrasActual.Text.Trim();
                    }                    
                }
                else
                    nuevaEmergencia.EME_TABACO_ACTUAL = false;
                if (chbDrogasC.Checked == true)
                    nuevaEmergencia.EME_DROGAR_ACTUAL = true;
                else
                    nuevaEmergencia.EME_DROGAR_ACTUAL = false;
                //if (rdb_Trauma.Checked == true)
                //    nuevaEmergencia.EME_TRAUMA = true;
                //else
                    nuevaEmergencia.EME_TRAUMA = false;
                //if (rdb_Infecciones.Checked == true)
                //    nuevaEmergencia.EME_INFECCIONES = true;
                //else
                    nuevaEmergencia.EME_INFECCIONES = false;
                //if (rdb_Cardiov.Checked == true)
                //    nuevaEmergencia.EME_CARDIOVASCULAR = true;
                //else
                    nuevaEmergencia.EME_CARDIOVASCULAR = false;
                //if (rdb_Quirur.Checked == true)
                //    nuevaEmergencia.EME_QUIRURGUCAS = true;
                //else
                    nuevaEmergencia.EME_QUIRURGUCAS = false;
                //if (rdb_OtrasDis.Checked == true)
                //    nuevaEmergencia.EME_OTRAS_DIS = true;
                //else
                    nuevaEmergencia.EME_OTRAS_DIS = false;
                //if (rdb_Referido.Checked == true)
                //    nuevaEmergencia.EME_REFERIDO = true;
                //else
                    nuevaEmergencia.EME_REFERIDO = false;
                //if (rdb_Privado.Checked == true)
                //    nuevaEmergencia.EME_PRIVADO = true;
                //else
                    nuevaEmergencia.EME_PRIVADO = false;
                    if (rdb_Alta.Checked == true)
                        nuevaEmergencia.EME_ALTA = true;              
                    else
                        nuevaEmergencia.EME_ALTA = false;                    
                    if (rdb_IngresoPiso.Checked == true)                   
                        nuevaEmergencia.EME_PISO = true;             
                    else
                        nuevaEmergencia.EME_PISO = false;
                //if (rdb_IngresoQuirofano.Checked == true)
                //    nuevaEmergencia.EME_QUIR = true;
                //else
                    nuevaEmergencia.EME_QUIR = false;
                //if (rdb_IngresoCoron.Checked == true)
                //    nuevaEmergencia.EME_CORON = true;
                //else
                    nuevaEmergencia.EME_CORON = false;
                if (rdb_IngresoUCI.Checked == true)
                    nuevaEmergencia.EME_UCI = true;
                else
                    nuevaEmergencia.EME_UCI = false;
                if (rdb_Muerto.Checked == true)
                    nuevaEmergencia.EME_MUERTO = true;
                else
                    nuevaEmergencia.EME_MUERTO = false;
               
                if (rdb_Vivo.Checked == true)
                    nuevaEmergencia.EME_VIVO = true;
                else
                    nuevaEmergencia.EME_VIVO = false;
                if (rdb_MuertoP.Checked == true)
                    nuevaEmergencia.EME_MUERTO_ESTADO = true;
                else
                    nuevaEmergencia.EME_MUERTO_ESTADO = false;
                if (rdb_TransferidoP.Checked == true)
                    nuevaEmergencia.EME_TRANFERIDO = true;
                else
                    nuevaEmergencia.EME_TRANFERIDO = false;

                CIE10 cie = new CIE10();
                if (txtDiagDef.Text != string.Empty)
                {
                    nuevaEmergencia.EME_DIAG_DEFENITICO = txtDiagDef.Text;
                    nuevaEmergencia.EME_BUSQUEDA_ICD = Convert.ToString(txtDiagDef.Tag);
                }
                //if (listaBusqICD.Count > 0)
                //{
                //    cie = new CIE10();
                //    cie = listaBusqICD.ElementAt(0);
                //    nuevaEmergencia.EME_BUSQUEDA_ICD = cie.CIE_DESCRIPCION;
                //}
                if (rdb_Otras.Checked == true)
                {
                    nuevaEmergencia.EME_OTRAS = "1";                    
                    
                    //if (cb_TriageOtras.SelectedIndex > -1)
                    //{
                    //    HC_CATALOGOS catalogoCombo4 = (HC_CATALOGOS)cb_TriageOtras.SelectedItem;
                    //    nuevaEmergencia.EME_OTRAS = catalogoCombo4.HCC_NOMBRE;
                    //}
                    //else
                    //    nuevaEmergencia.EME_OTRAS = null;

                }                
                if (rdb_Transferido.Checked == true)
                {
                    if (txtTransferido.Text != string.Empty)
                    {
                        nuevaEmergencia.COD_TRANSFERIDO = 1;
                        nuevaEmergencia.EME_TRANSFERIDO = txtTransferido.Text;
                    }
                    //nuevaEmergencia.COD_TRANSFERIDO = Convert.ToString(txtTransferido.Text);

                    //cat = new HC_CATALOGOS();
                    //if (cb_Transferido.SelectedIndex > -1)
                    //{
                    //    HC_CATALOGOS  catalogoCombo5 = (HC_CATALOGOS)cb_Transferido.SelectedItem;
                    //    nuevaEmergencia.COD_TRANSFERIDO = catalogoCombo5.HCC_CODIGO;
                    //} 

                }
                else { 
                    nuevaEmergencia.COD_TRANSFERIDO = 0;
                }
            }
            catch (Exception e)
             {
                 MessageBox.Show("Error en el ingreso de datos: \n" + e.Message);
                 if (e.InnerException != null)
                     MessageBox.Show("Error en el ingreso de datos: \n" + e.InnerException);
             }
        }

        private void cargarEmergencia(int codigoEmergencia)
        {
            try
            {
                if (modificadaEmergencia != null)
                {
                    regisEmer = true;
                    txtRegistro.Text = Convert.ToString(modificadaEmergencia.COD_EMERGENCIA);
                    txt_Atencion.Text = Convert.ToString(modificadaEmergencia.ATE_CODIGO);
                    txtAlergias.Text = modificadaEmergencia.EME_ALERGIAS;
                    //nuevaEmergencia.EME_FUM = DateTime.Now;
                    //dateTimeFum.Value = modificadaEmergencia.EME_FUM.Date;
                    dateTimeFum.Value = DateTime.Now;
                    //Eliminar dtpFechaInicio
                    //dtpFechaInicio.Value = modificadaEmergencia.EME_FECHA_INICIO.Date;                   
                    string[] ta = modificadaEmergencia.EME_TA.Trim().Split(new char[] { '/' });                    
                    txtTA1.Text = ta[0];                    
                    txtTA2.Text = ta[1];
                    txtT.Text = modificadaEmergencia.EME_T.Trim();
                    txtSat.Text = modificadaEmergencia.EME_SAT.Trim();
                    txtFc.Text = modificadaEmergencia.EME_FC.Trim();
                    txtFr.Text = modificadaEmergencia.EME_FR.Trim();
                    txtOrl.Text = modificadaEmergencia.EME_ORL;
                    txtCabeza.Text = modificadaEmergencia.EME_CABEZA;
                    txtCuello.Text = modificadaEmergencia.EME_CUELLO;
                    txtTorax.Text = modificadaEmergencia.EME_TORAX;
                    txtCardiaco.Text = modificadaEmergencia.EME_CARDIACO;
                    txtPulmonar.Text = modificadaEmergencia.EME_PULMONAR;
                    txtAbdomen.Text = modificadaEmergencia.EME_ABDOMEN;
                    txtGenitales.Text = modificadaEmergencia.EME_GENETALES;
                    txtPerine.Text = modificadaEmergencia.EME_PERINE;
                    txtExtremidades.Text = modificadaEmergencia.EME_EXTREMIDADES;
                    txtFosasL.Text = modificadaEmergencia.EME_FOSAS_LUM;
                    txtLinfa.Text = modificadaEmergencia.EME_LINFATICO;
                    txtNeuro.Text = modificadaEmergencia.EME_NEUROLOGICO;
                    txtPiel.Text = modificadaEmergencia.EME_PIEL;
                    txtObserEnfer.Text = modificadaEmergencia.EME_OBSERVACIONES;
                    //modificadaEmergencia.EME_FECHA_INICIO.Date;                   
                    //dtpEstanciaHosp.Value = modificadaEmergencia.EME_FECHA_ESTANCIA.Date;
                    dtpEstanciaHosp.Value = DateTime.Now;
                        //.Date;
                    txtDiagEmerg.Text = modificadaEmergencia.EME_DIAG_EMER;
                    txtDiagDef.Text = modificadaEmergencia.EME_DIAG_DEFENITICO;
                    if (modificadaEmergencia.ESP_CLIN != 0)
                    {
                        rdb_Clinicas.Checked = true;
                        cb_Clinicas.SelectedItem = catalogoEspeClinicas.FirstOrDefault(c => c.HCC_CODIGO == modificadaEmergencia.ESP_CLIN);                                                                      
                    }
                    else
                    {
                        if (modificadaEmergencia.ESP_QUIR != 0)
                        {
                            rdb_Quirurgicas.Checked = true;
                            cb_Quirurgicas.SelectedItem = catalogoEspeQuirur.FirstOrDefault(q => q.HCC_CODIGO == modificadaEmergencia.ESP_QUIR);                            
                        }
                        else
                        {
                            rdb_EspeOtras.Checked = true;
                            cb_OtrasEspec.SelectedItem = catalogoEspeOtras.FirstOrDefault(o => o.HCC_CODIGO == modificadaEmergencia.ESP_OTRAS);                          
                        }
                    }
                    txtObserFinal.Text = modificadaEmergencia.EME_OBSER_FINAL;
                    txtResumenDiag.Text = modificadaEmergencia.EME_RESUMEN;
                    txtProcedimientos.Text = modificadaEmergencia.EME_PROCEDIMIENTOS;
                    txtExamenesReal.Text = modificadaEmergencia.EME_EVO_COMP;
                    txtTratamientos.Text = modificadaEmergencia.EME_TRATAMIENTOS;
                    gridEnfPrevias.DataSource = null;
                    List<HC_EMERGENCIA_EPREVIAS> listaEnferPrevias = new List<HC_EMERGENCIA_EPREVIAS>();
                    listaEnferPrevias = NegHcEmergenciaEnfPrevias.RecuperarHcEmergenciaEnfPrevias(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaEnferPrevias.Count; i++)
                    {
                        HC_EMERGENCIA_EPREVIAS enferPrevias = new HC_EMERGENCIA_EPREVIAS();
                        enferPrevias = listaEnferPrevias.ElementAt(i);
                        CIE10 cie = new CIE10();
                        cie = NegCIE10.RecuperarCIE10(enferPrevias.EFP_CIE10);
                        listaCIE = new List<CIE10>();
                        cargarEnferPreven(cie);
                    }

                    List<HC_EMERGENCIA_TRATAMIENTO> listaTratamientos = new List<HC_EMERGENCIA_TRATAMIENTO>();
                    listaTratamientos = NegHcEmergenciaTratamiento.RecuperarHcEmergenciaTratamientos(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaTratamientos.Count; i++)
                    {
                        HC_EMERGENCIA_TRATAMIENTO trat = new HC_EMERGENCIA_TRATAMIENTO();
                        trat = listaTratamientos.ElementAt(i);
                        for (int j = 0; j < chbL_SelecTratam.Items.Count; j++)
                        {
                            HC_CATALOGOS cat = new HC_CATALOGOS();
                            cat = (HC_CATALOGOS)chbL_SelecTratam.Items[j];
                            if (trat.TRA_HC_CATALOGOS == cat.HCC_CODIGO)
                            {
                                chbL_SelecTratam.SetItemChecked(j, true);
                                break;
                            }
                        }
                    }
                    List<HC_EMERGENCIA_CIRUGIAS> listaCirugias = new List<HC_EMERGENCIA_CIRUGIAS>();
                    listaCirugias = NegHcEmergenciaCirugias.RecuperarHcEmergenciaCirugias(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaCirugias.Count; i++)
                    {
                        HC_EMERGENCIA_CIRUGIAS cirug = new HC_EMERGENCIA_CIRUGIAS();
                        cirug = listaCirugias.ElementAt(i);
                        for (int j = 0; j < chbL_CirugiasPrev.Items.Count; j++)
                        {
                            HC_CATALOGOS cat = new HC_CATALOGOS();
                            cat = (HC_CATALOGOS)chbL_CirugiasPrev.Items[j];
                            if (cirug.CIR_HC_CATALOGOS == cat.HCC_CODIGO)
                            {
                                chbL_CirugiasPrev.SetItemChecked(j, true);
                                break;
                            }
                        }
                    }

                    ////cb_AtencionMedica
                    //HC_CATALOGOS catal = new HC_CATALOGOS();
                    //catal = (HC_CATALOGOS)cb_AtencionMedica.SelectedItem;
                    //if (modificadaEmergencia.HCC_CAT_ATENCION != catal.HCC_CODIGO)
                    //{
                    //    for (int i = 0; i < cb_AtencionMedica.Items.Count; i++)
                    //    {
                    //        HC_CATALOGOS cat = new HC_CATALOGOS();
                    //        cat = (HC_CATALOGOS)cb_AtencionMedica.Items[i];
                    //        if (cat.HCC_CODIGO == modificadaEmergencia.HCC_CAT_ATENCION)
                    //        {
                    //            cb_AtencionMedica.SelectedIndex = i;
                    //            break;
                    //        }
                    //    }
                    //}

                    ////cb_TratamientoActual
                    //catal = new HC_CATALOGOS();
                    //catal = (HC_CATALOGOS)cb_TratamientoActual.SelectedItem;
                    //if (modificadaEmergencia.HCC_CAT_TRATAMIENTO != catal.HCC_CODIGO)
                    //{
                    //    for (int i = 0; i < cb_TratamientoActual.Items.Count; i++)
                    //    {
                    //        HC_CATALOGOS cat = new HC_CATALOGOS();
                    //        cat = (HC_CATALOGOS)cb_TratamientoActual.Items[i];
                    //        if (cat.HCC_CODIGO == modificadaEmergencia.HCC_CAT_TRATAMIENTO)
                    //        {
                    //            cb_TratamientoActual.SelectedIndex = i;
                    //            break;
                    //        }
                    //    }
                    //}
                    //cb_Procedimiento
                    //catal = new HC_CATALOGOS();
                    //catal = (HC_CATALOGOS)cb_Procedimiento.SelectedItem;
                    //if (modificadaEmergencia.HCC_CAT_PROCEDIMIENTO != catal.HCC_CODIGO)
                    //{
                    //    for (int i = 0; i < cb_Procedimiento.Items.Count; i++)
                    //    {
                    //        HC_CATALOGOS cat = new HC_CATALOGOS();
                    //        cat = (HC_CATALOGOS)cb_Procedimiento.Items[i];
                    //        if (cat.HCC_CODIGO == modificadaEmergencia.HCC_CAT_PROCEDIMIENTO)
                    //        {
                    //            cb_Procedimiento.SelectedIndex = i;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (modificadaEmergencia.EME_BUSQUEDA_ICD != null)
                    //{
                    //    CIE10 cie10 = new CIE10();
                    //    cie10 = NegCIE10.RecuperarCIE10(modificadaEmergencia.EME_BUSQUEDA_ICD);
                    //    txtDiagDef.Text = cie10.CIE_DESCRIPCION;
                    //    txtDiagDef.Tag = cie10.CIE_CODIGO;
                    //    //cie10.CIE_CODIGO = "A00";
                    //    //cie10.CIE_DESCRIPCION = modificadaEmergencia.EME_BUSQUEDA_ICD;
                    //    //cie10.CIE_IDPADRE = "A00.1";
                    //    //cargarBusquedaICD(cie10);
                    //}
                    if (modificadaEmergencia.EME_ALCOHOL == true)
                    {
                        chbAlcoholE.Checked = true;
                    }
                    if (modificadaEmergencia.EME_TABACO == true)
                    {
                        chbTabacoE.Checked = true;
                    }
                    if (modificadaEmergencia.EME_DROGAS == true)
                    {
                        chbDrogasE.Checked = true;
                    }
                    //if (modificadaEmergencia.EME_DIRECTO == true)
                    //{
                    //    rdb_Directo.Checked = true;
                    //}
                    //if (modificadaEmergencia.EME_INDIRECTO == true)
                    //{
                    //    rdb_Indirecto.Checked = true;
                    //}

                    if (modificadaEmergencia.EME_GLASGOW != 0)
                    {
                        for (int i = 0; i < cb_Glasgow.Items.Count; i++)
                        {
                            int val = Convert.ToInt32(cb_Glasgow.Items[i]);
                            if (val == modificadaEmergencia.EME_GLASGOW)
                            {
                                cb_Glasgow.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    if (modificadaEmergencia.EME_NOURGE == true)
                    {
                        rdb_NoUrgente.Checked = true;
                    }
                    if (modificadaEmergencia.EME_EMERGENCIA == true)
                    {
                        rdb_Emergencia.Checked = true;
                    }
                    if (modificadaEmergencia.EME_CRITICO == true)
                    {
                        rdb_Critico.Checked = true;
                    }
                    if (modificadaEmergencia.EME_OTRAS != null)
                    {
                        rdb_Otras.Checked = true;                        
                        //for (int i = 0; i < cb_TriageOtras.Items.Count; i++)
                        //{
                        //    HC_CATALOGOS val = (HC_CATALOGOS)cb_TriageOtras.SelectedItem;
                        //    if (val.HCC_NOMBRE == modificadaEmergencia.EME_OTRAS)
                        //    {
                        //        cb_TriageOtras.SelectedIndex = i;
                        //        break;
                        //    }
                        //}
                        //txtTriageOtras.Text = modificadaEmergencia.EME_OTRAS;

                    }
                    if (modificadaEmergencia.EME_ALCOHOL_ACTUAL == true)
                    {
                        chbAlcoholC.Checked = true;
                    }
                    if (modificadaEmergencia.EME_TABACO_ACTUAL == true)
                    {
                        chbOtrasC.Checked = true;
                        txtOtrasActual.Text = modificadaEmergencia.EME_OTRAS_ACTUAL;
                    }
                    if (modificadaEmergencia.EME_DROGAR_ACTUAL == true)
                    {
                        chbDrogasC.Checked = true;
                    }
                    //if (modificadaEmergencia.EME_TRAUMA == true)
                    //{
                    //    rdb_Trauma.Checked = true;
                    //}
                    //if (modificadaEmergencia.EME_INFECCIONES == true)
                    //{
                    //    rdb_Infecciones.Checked = true;
                    //}
                    //if (modificadaEmergencia.EME_CARDIOVASCULAR == true)
                    //{
                    //    rdb_Cardiov.Checked = true;
                    //}
                    //if (modificadaEmergencia.EME_QUIRURGUCAS == true)
                    //{
                    //    rdb_Quirur.Checked = true;
                    //}
                    //if (modificadaEmergencia.EME_OTRAS_DIS == true)
                    //{
                    //    rdb_OtrasDis.Checked = true;
                    //}
                    //if (modificadaEmergencia.EME_REFERIDO == true)
                    //{
                    //    rdb_Referido.Checked = true;
                    //}
                    //if (modificadaEmergencia.EME_PRIVADO == true)
                    //{
                    //    rdb_Privado.Checked = true;
                    //}
                    if (modificadaEmergencia.EME_ALTA == true)
                    {
                        rdb_Alta.Checked = true;
                    }
                    if (modificadaEmergencia.EME_PISO == true)
                    {
                        rdb_IngresoPiso.Checked = true;
                    }
                    //if (modificadaEmergencia.EME_QUIR == true)
                    //{
                    //    rdb_IngresoQuirofano.Checked = true;
                    //}
                    //if (modificadaEmergencia.EME_CORON == true)
                    //{
                    //    rdb_IngresoCoron.Checked = true;
                    //}
                    if (modificadaEmergencia.EME_UCI == true)
                    {
                        rdb_IngresoUCI.Checked = true;
                    }
                    if (modificadaEmergencia.EME_MUERTO == true)
                    {
                        rdb_Muerto.Checked = true;
                    }

                    if (modificadaEmergencia.COD_TRANSFERIDO != 0)
                    {
                        rdb_Transferido.Checked = true;
                        txtTransferido.Text = modificadaEmergencia.EME_TRANSFERIDO;
                        //txtTransferido.Text = ;

                        //for (int i = 0; i < cb_Transferido.Items.Count; i++)
                        //{
                        //    HC_CATALOGOS val = (HC_CATALOGOS)cb_Transferido.Items[i];
                        //    if (val.HCC_CODIGO == modificadaEmergencia.COD_TRANSFERIDO)
                        //    {
                        //        cb_Transferido.SelectedIndex = i;
                        //        break;
                        //    }
                        //}
                    }

                    ////Complicaciones
                    List<HC_EMERGENCIA_COMPLICACIONES> listaComplic = new List<HC_EMERGENCIA_COMPLICACIONES>();
                    listaComplic = NegHcEmergenciaComplicaciones.RecuperarHcEmergenciaComplicaciones(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaComplic.Count; i++)
                    {
                        HC_EMERGENCIA_COMPLICACIONES comp = new HC_EMERGENCIA_COMPLICACIONES();
                        comp = listaComplic.ElementAt(i);
                        for (int j = 0; j < chbL_SelecTratam.Items.Count; j++)
                        {
                            HC_CATALOGOS cat = new HC_CATALOGOS();
                            cat = (HC_CATALOGOS)chdL_Complicaciones.Items[j];
                            if (comp.COM_HC_CATALOGOS == cat.HCC_CODIGO)
                            {
                                chdL_Complicaciones.SetItemChecked(j, true);
                                break;
                            }
                        }
                    }

                    if (modificadaEmergencia.EME_VIVO == true)
                    {
                        rdb_Vivo.Checked = true;
                    }
                    if (modificadaEmergencia.EME_MUERTO_ESTADO == true)
                    {
                        rdb_MuertoP.Checked = true;
                    }
                    if (modificadaEmergencia.EME_TRANFERIDO == true)
                    {
                        rdb_TransferidoP.Checked = true;                        
                    }


                    ///SubSintomas                    
                    dataGridViewSubSintomas.DataSource = null;
                    List<HC_EMERGENCIA_SS> listaSS = new List<HC_EMERGENCIA_SS>();
                    listaSS = NegHcEmergenciaSubsintomas.RecuperarHcEmergenciaSubSintomas(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaSS.Count; i++)
                    {
                        HC_EMERGENCIA_SS comp = new HC_EMERGENCIA_SS();
                        comp = listaSS.ElementAt(i);
                        resultadoSub = comp.SS_CATALOGO;
                        descripcionSub = comp.SS_DESCRIPCION;
                        cargarSubsintomas(false);
                    }

                    ////EXÁMENES
                    dataGridViewExamenes.DataSource = null;
                    List<HC_EMERGENCIA_EXAMENES> listaExamenes = new List<HC_EMERGENCIA_EXAMENES>();
                    listaExamenes = NegHcEmergenciaExamenes.RecuperarHcEmergenciaExamenes(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaExamenes.Count; i++)
                    {
                        HC_EMERGENCIA_EXAMENES exam = new HC_EMERGENCIA_EXAMENES();
                        exam = listaExamenes.ElementAt(i);
                        examen.HCC_NOMBRE = exam.EXA_HC_CATALOGOS;
                        comunicado = exam.EXA_COMUNICADO;
                        resultado = exam.EXA_RESULTADO;
                        cargarExamen(false);
                    }

                    ////IMAGENES
                    dataGridViewImagenes.DataSource = null;
                    List<HC_EMERGENCIA_IMAGENES> listaImagenes = new List<HC_EMERGENCIA_IMAGENES>();
                    listaImagenes = NegHcEmergenciaImagenes.RecuperarHcEmergenciaImagenes(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaImagenes.Count; i++)
                    {
                        HC_EMERGENCIA_IMAGENES imag = new HC_EMERGENCIA_IMAGENES();
                        imag = listaImagenes.ElementAt(i);
                        imagen.HCC_NOMBRE = imag.IMA_HC_CATALOGOS;
                        comunicado = imag.IMA_COMUNICADO;
                        resultado = imag.IMA_RESULTADO;
                        cargarImagen(false);
                    }

                    ////INTERCONSULTAS
                    List<HC_EMERGENCIA_EVALUACION> listaEval = new List<HC_EMERGENCIA_EVALUACION>();
                    listaEval = NegHcEmergenciaEvaluacion.RecuperarHcEmergenciaEvaluacion(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaEval.Count; i++)
                    {
                        HC_EMERGENCIA_EVALUACION eval = new HC_EMERGENCIA_EVALUACION();
                        eval = listaEval.ElementAt(i);
                        inter.HCC_NOMBRE = eval.INT_HC_CATALOGOS;
                        if (eval.INT_COMUNICADO != null)
                        {
                            comunicadoFecha = DateTime.Parse(eval.INT_COMUNICADO.ToString());
                            estadoFecha = true;
                        }
                        else
                            estadoFecha = false;
                        if (eval.INT_REALIZADO != null)
                        {
                            realizado = DateTime.Parse(eval.INT_REALIZADO.ToString());
                            estadoFechaRea = true;
                        }
                        else
                            estadoFechaRea = false;
                        observaciones = eval.INT_OBSERVACIONES;
                        cargarInterconsulta(false);
                    }

                    ////REFERIDOS
                    List<HC_EMERGENCIA_REFERIDOS> listaRef = new List<HC_EMERGENCIA_REFERIDOS>();
                    listaRef = NegHcEmergenciaReferido.RecuperarHcEmergenciaReferencias(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaRef.Count; i++)
                    {
                        HC_EMERGENCIA_REFERIDOS refer = new HC_EMERGENCIA_REFERIDOS();
                        refer = listaRef.ElementAt(i);
                        referido.HCC_NOMBRE = refer.REF_HC_CATALOGO;
                        nombreI = refer.REF_COMUNICADO;
                        if (refer.REF_REALIZADO != null)
                        {
                            comunicadoFecha = DateTime.Parse(refer.REF_REALIZADO.ToString());
                            estadoFecha = true;
                        }
                        else
                            estadoFecha = false;
                        //realizado = DateTime.Parse(refer.REF_REALIZADO.ToString());
                        //realizado = new DateTime();
                        observaciones = refer.REF_OBSERVACION;
                        cargarReferido(false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void cargarDatosTablas()
        {
            cargarEnferPrevias();
            cargarTratamientos();
            cargarCirugias();
            cargarComplicaciones();
            cargarSubSintomas();
            cargarExamenes();
            cargarImagenes();
            cargarInterconsultas();
            cargarReferido();
        }

        private void cargarEnferPrevias()
        {
            CIE10 cie = new CIE10();
            if (listaCIE.Count > 0)
            {
                for (int i = 0; i < listaCIE.Count; i++)
                {
                    HC_EMERGENCIA_EPREVIAS enferPrevias = new HC_EMERGENCIA_EPREVIAS();
                    cie = listaCIE.ElementAt(i);
                    int codigoNuevo = NegHcEmergenciaEnfPrevias.RecuperaMaximoHcEmergenciaEnfPrevias();
                    codigoNuevo++;
                    enferPrevias.EFP_CODIGO = codigoNuevo;
                    enferPrevias.EFP_EMERGENCIA = codigoEmergencia;
                    enferPrevias.EFP_CIE10 = cie.CIE_CODIGO;
                    NegHcEmergenciaEnfPrevias.crearHcEmergenciaEnfPrevias(enferPrevias);
                }
            }
        }

        private void cargarTratamientos() 
        {
            HC_CATALOGOS cat = new HC_CATALOGOS();
            for (int i = 0; i < chbL_SelecTratam.Items.Count; i++)
            {
                if (chbL_SelecTratam.GetItemCheckState(i) == CheckState.Checked)
                {
                    cat = (HC_CATALOGOS)chbL_SelecTratam.Items[i];
                    HC_EMERGENCIA_TRATAMIENTO tratamiento = new HC_EMERGENCIA_TRATAMIENTO();
                    tratamiento.TRA_CODIGO = NegHcEmergenciaTratamiento.RecuperaMaximoHcEmergenciaTratamiento() + 1;
                    tratamiento.TRA_EMERGENCIA = codigoEmergencia;
                    tratamiento.TRA_HC_CATALOGOS = cat.HCC_CODIGO;
                    NegHcEmergenciaTratamiento.crearHcEmergenciaTratamiento(tratamiento);
                }
            }

        }

        private void cargarCirugias() 
        {
            HC_CATALOGOS cat = new HC_CATALOGOS();
            for (int i = 0; i < chbL_CirugiasPrev.Items.Count; i++)
            {
                if (chbL_CirugiasPrev.GetItemCheckState(i) == CheckState.Checked)
                {
                    cat = (HC_CATALOGOS)chbL_CirugiasPrev.Items[i];
                    HC_EMERGENCIA_CIRUGIAS cirugias = new HC_EMERGENCIA_CIRUGIAS();
                    cirugias.CIR_CODIGO = NegHcEmergenciaCirugias.RecuperaMaximoHcEmergenciaCirugias() + 1;
                    cirugias.CIR_EMERGENCIA = codigoEmergencia;
                    cirugias.CIR_HC_CATALOGOS = cat.HCC_CODIGO;
                    NegHcEmergenciaCirugias.crearHcEmergenciaCirugias(cirugias);
                }
            }
        }

        private void cargarComplicaciones() 
        {
            //Complicaciones
            HC_CATALOGOS cat = new HC_CATALOGOS();
            for (int i = 0; i < chdL_Complicaciones.Items.Count; i++)
            {
                if (chdL_Complicaciones.GetItemCheckState(i) == CheckState.Checked)
                {
                    cat = (HC_CATALOGOS)chdL_Complicaciones.Items[i];
                    HC_EMERGENCIA_COMPLICACIONES complicaciones = new HC_EMERGENCIA_COMPLICACIONES();
                    complicaciones.COM_CODIGO = NegHcEmergenciaComplicaciones.RecuperaMaximoHcEmergenciaComplicaciones() + 1;
                    complicaciones.COM_EMERGENCIA = codigoEmergencia;
                    complicaciones.COM_HC_CATALOGOS = cat.HCC_CODIGO;
                    NegHcEmergenciaComplicaciones.crearHcEmergenciaComplicaciones(complicaciones);
                }
            }
        }

        private void cargarSubSintomas() 
        {
            //SubSintomas
            if (dataGridViewSubSintomas.Rows.Count > 0)
            {
                int col = 0;
                for (int i = 0; i < dataGridViewSubSintomas.Rows.Count - 1; i++)
                {
                    HC_EMERGENCIA_SS subSintomas = new HC_EMERGENCIA_SS();
                    string nombre = dataGridViewSubSintomas.Rows[i].Cells[col].Value.ToString();
                    string descripcion = dataGridViewSubSintomas.Rows[i].Cells[col + 1].Value.ToString();
                    subSintomas.SS_CODIGO = NegHcEmergenciaSubsintomas.RecuperaMaximoHcEmergenciaSubsintomas() + 1;
                    subSintomas.SS_EMERGENCIA = codigoEmergencia;
                    subSintomas.SS_CATALOGO = nombre;
                    subSintomas.SS_DESCRIPCION = descripcion;
                    NegHcEmergenciaSubsintomas.crearHcEmergenciaSubsintomas(subSintomas);
                }
            }
        }

        private void cargarExamenes() 
        {
            //EXÁMENES
            if (dataGridViewExamenes.Rows.Count > 0)
            {
                int col = 0;
                for (int i = 0; i < dataGridViewExamenes.Rows.Count - 1; i++)
                {
                    HC_EMERGENCIA_EXAMENES emerExamen = new HC_EMERGENCIA_EXAMENES();
                    string nombre = dataGridViewExamenes.Rows[i].Cells[col].Value.ToString();
                    string comunicado = dataGridViewExamenes.Rows[i].Cells[col + 1].Value.ToString();
                    string resultado = dataGridViewExamenes.Rows[i].Cells[col + 2].Value.ToString();
                    emerExamen.EXA_CODIGO = NegHcEmergenciaExamenes.RecuperaMaximoHcEmergenciaExamenes() + 1;
                    emerExamen.EXA_EMERGENCIA = codigoEmergencia;
                    emerExamen.EXA_HC_CATALOGOS = nombre;
                    emerExamen.EXA_COMUNICADO = comunicado;
                    emerExamen.EXA_RESULTADO = resultado;
                    NegHcEmergenciaExamenes.crearHcEmergenciaExamenes(emerExamen);
                }
            }
        }

        private void cargarImagenes() 
        {
            //IMAGENES
            if (dataGridViewImagenes.Rows.Count > 0)
            {
                int col = 0;
                for (int i = 0; i < dataGridViewImagenes.Rows.Count - 1; i++)
                {
                    HC_EMERGENCIA_IMAGENES emerImagen = new HC_EMERGENCIA_IMAGENES();
                    string nombre = dataGridViewImagenes.Rows[i].Cells[col].Value.ToString();
                    string comunicado = dataGridViewImagenes.Rows[i].Cells[col + 1].Value.ToString();
                    string resultado = dataGridViewImagenes.Rows[i].Cells[col + 2].Value.ToString();
                    emerImagen.IMA_CODIGO = NegHcEmergenciaImagenes.RecuperaMaximoHcEmergenciaImagenes() + 1;
                    emerImagen.IMA_EMERGENCIA = codigoEmergencia;
                    emerImagen.IMA_HC_CATALOGOS = nombre;
                    emerImagen.IMA_COMUNICADO = comunicado;
                    emerImagen.IMA_RESULTADO = resultado;
                    NegHcEmergenciaImagenes.crearHcEmergenciaImagenes(emerImagen);
                }
            }
        }

        private void cargarInterconsultas() 
        {
            //INTERCONSULTAS
            if (dataGridViewInterconsultas.Rows.Count > 0)
            {
                int col = 0;
                for (int i = 0; i < dataGridViewInterconsultas.Rows.Count - 1; i++)
                {
                    HC_EMERGENCIA_EVALUACION evol = new HC_EMERGENCIA_EVALUACION();
                    string espac = dataGridViewInterconsultas.Rows[i].Cells[col].Value.ToString();
                    string comunicado = dataGridViewInterconsultas.Rows[i].Cells[col + 1].Value.ToString();
                    string resultado = dataGridViewInterconsultas.Rows[i].Cells[col + 2].Value.ToString();
                    string observ = dataGridViewInterconsultas.Rows[i].Cells[col + 3].Value.ToString();
                    evol.INT_CODIGO = NegHcEmergenciaEvaluacion.RecuperaMaximoHcEmergenciaEvaluacion() + 1;
                    evol.INT_EMERGENCIA = codigoEmergencia;
                    evol.INT_HC_CATALOGOS = espac;
                    estadoFecha = false;
                    validarFecha(comunicado, estadoFecha, "Valida");
                    if (estadoFecha == true)
                        evol.INT_COMUNICADO = DateTime.Parse(dataGridViewInterconsultas.Rows[i].Cells[1].Value.ToString());
                    else
                        evol.INT_COMUNICADO = null;  
                    estadoFecha = false;
                    validarFecha(resultado, estadoFecha, "Valida");
                    if (estadoFecha == true)
                        evol.INT_REALIZADO = DateTime.Parse(dataGridViewInterconsultas.Rows[i].Cells[2].Value.ToString());                    
                    else
                        evol.INT_REALIZADO = null;             
                    evol.INT_OBSERVACIONES = observ;
                    NegHcEmergenciaEvaluacion.crearHcEmergenciaEvaluacion(evol);
                }
            }            
        }

        private void cargarReferido() 
        {
            //REFERIDO A
            if (dataGridViewReferido.Rows.Count > 0)
            {
                int col = 0;
                for (int i = 0; i < dataGridViewReferido.Rows.Count - 1; i++)
                {
                    HC_EMERGENCIA_REFERIDOS refer = new HC_EMERGENCIA_REFERIDOS();
                    string espac = dataGridViewReferido.Rows[i].Cells[col].Value.ToString();
                    string nombre = dataGridViewReferido.Rows[i].Cells[1].Value.ToString();
                    string fecha = dataGridViewReferido.Rows[i].Cells[2].Value.ToString();
                    string observ = dataGridViewReferido.Rows[i].Cells[3].Value.ToString();
                    refer.REF_CODIGO = NegHcEmergenciaReferido.RecuperaMaximoHcEmergenciaReferido() + 1;
                    refer.REF_EMERGENCIA = codigoEmergencia;
                    refer.REF_HC_CATALOGO = espac;
                    refer.REF_COMUNICADO = nombre;
                    estadoFecha = false;
                    validarFecha(fecha, estadoFecha, "Valida");
                    if (estadoFecha == true)
                        refer.REF_REALIZADO = DateTime.Parse(dataGridViewReferido.Rows[i].Cells[2].Value.ToString());
                    else
                        refer.REF_REALIZADO = null; 
                    refer.REF_OBSERVACION = observ;
                    NegHcEmergenciaReferido.crearHcEmergenciaReferido(refer);
                }
            }
        }    


        private void cargarDatosTablasModificadas(int codigoEmergencia)
        {
            try
            {
                Boolean accion = false;
                CIE10 cie = new CIE10();
                HC_EMERGENCIA_EPREVIAS enferPrevias = new HC_EMERGENCIA_EPREVIAS();
                List<HC_EMERGENCIA_EPREVIAS> listEmerPrevias = new List<HC_EMERGENCIA_EPREVIAS>();
                listEmerPrevias = NegHcEmergenciaEnfPrevias.RecuperarHcEmergenciaEnfPrevias(modificadaEmergencia.COD_EMERGENCIA);
                if (listaCIE.Count > 0) 
                {
                    if (listEmerPrevias.Count > 0)
                    {
                        for (int i = 0; i < listaCIE.Count; i++)
                        {
                            cie = listaCIE.ElementAt(i);
                            for (int j = 0; j < listEmerPrevias.Count;j++ )
                            {
                                enferPrevias = new HC_EMERGENCIA_EPREVIAS();
                                enferPrevias = listEmerPrevias.ElementAt(j);
                                if (enferPrevias.EFP_CIE10 != cie.CIE_CODIGO)
                                    accion = false;
                                else
                                {
                                    accion = true;
                                    break;
                                }
                            }
                            if(accion==false)
                            {
                                enferPrevias = new HC_EMERGENCIA_EPREVIAS();
                                int codigoNuevo = NegHcEmergenciaEnfPrevias.RecuperaMaximoHcEmergenciaEnfPrevias();
                                codigoNuevo++;
                                enferPrevias.EFP_CODIGO = codigoNuevo;
                                enferPrevias.EFP_EMERGENCIA = codigoEmergencia;
                                enferPrevias.EFP_CIE10 = cie.CIE_CODIGO;
                                NegHcEmergenciaEnfPrevias.crearHcEmergenciaEnfPrevias(enferPrevias);
                            }    
                        }
                    }
                    else 
                    {
                        cargarEnferPrevias();
                    }
                }

                //SubSintomas               
                HC_EMERGENCIA_SS subSintomas = new HC_EMERGENCIA_SS();
                List<HC_EMERGENCIA_SS> listSub = new List<HC_EMERGENCIA_SS>();
                listSub = NegHcEmergenciaSubsintomas.RecuperarHcEmergenciaSubSintomas(modificadaEmergencia.COD_EMERGENCIA);              
                int fila = 0;
                if (dataGridViewSubSintomas.Rows.Count > 0)
                {
                    if (listSub.Count > 0)
                    {
                        for (int i = 0; i < listSub.Count; i++ ) 
                        {
                            subSintomas = listSub.ElementAt(i);
                            subSintomas.SS_CATALOGO = dataGridViewSubSintomas.Rows[i].Cells[0].Value.ToString();
                            subSintomas.SS_DESCRIPCION = dataGridViewSubSintomas.Rows[i].Cells[1].Value.ToString();                      
                            NegHcEmergenciaSubsintomas.actualizarHcSunsintoma(subSintomas);
                            fila = i;
                        }
                        if (((dataGridViewSubSintomas.Rows.Count-1) - listSub.Count) > 0)
                        {                            
                            for (int i = fila+1; i < dataGridViewSubSintomas.Rows.Count-1; i++ )
                            {
                                subSintomas = new HC_EMERGENCIA_SS();
                                subSintomas.SS_CODIGO = NegHcEmergenciaSubsintomas.RecuperaMaximoHcEmergenciaSubsintomas() + 1;
                                subSintomas.SS_EMERGENCIA = codigoEmergencia;
                                subSintomas.SS_CATALOGO = dataGridViewSubSintomas.Rows[i].Cells[0].Value.ToString();
                                subSintomas.SS_DESCRIPCION = dataGridViewSubSintomas.Rows[i].Cells[1].Value.ToString();
                                NegHcEmergenciaSubsintomas.crearHcEmergenciaSubsintomas(subSintomas);                                
                            }
                        }                     
                    }
                    else 
                    {
                        cargarSubSintomas();
                    }
                }


                //EXÁMENES
                HC_EMERGENCIA_EXAMENES exam = new HC_EMERGENCIA_EXAMENES();
                List<HC_EMERGENCIA_EXAMENES> listExa = new List<HC_EMERGENCIA_EXAMENES>();
                listExa = NegHcEmergenciaExamenes.RecuperarHcEmergenciaExamenes(modificadaEmergencia.COD_EMERGENCIA);
                fila = 0;
                if (dataGridViewExamenes.Rows.Count > 0)
                {
                    if (listExa.Count > 0)
                    {
                        for (int i = 0; i < listExa.Count; i++)
                        {
                            exam = listExa.ElementAt(i);
                            exam.EXA_HC_CATALOGOS = dataGridViewExamenes.Rows[i].Cells[0].Value.ToString();
                            exam.EXA_COMUNICADO = dataGridViewExamenes.Rows[i].Cells[1].Value.ToString();
                            exam.EXA_RESULTADO = dataGridViewExamenes.Rows[i].Cells[2].Value.ToString();
                            NegHcEmergenciaExamenes.actualizarHcExamen(exam);                            
                            fila = i;
                        }
                        if (((dataGridViewExamenes.Rows.Count - 1) - listExa.Count) > 0)
                        {
                            for (int i = fila + 1; i < dataGridViewExamenes.Rows.Count - 1; i++)
                            {
                                exam = new HC_EMERGENCIA_EXAMENES();
                                exam.EXA_CODIGO = NegHcEmergenciaExamenes.RecuperaMaximoHcEmergenciaExamenes() + 1;
                                exam.EXA_EMERGENCIA = codigoEmergencia;
                                exam.EXA_HC_CATALOGOS = dataGridViewExamenes.Rows[i].Cells[0].Value.ToString();
                                exam.EXA_COMUNICADO = dataGridViewExamenes.Rows[i].Cells[1].Value.ToString();
                                exam.EXA_RESULTADO = dataGridViewExamenes.Rows[i].Cells[2].Value.ToString();
                                NegHcEmergenciaExamenes.crearHcEmergenciaExamenes(exam);
                            }
                        }
                    }
                    else
                    {
                        cargarExamenes();                        
                    }
                }

                ////IMAGENES
                HC_EMERGENCIA_IMAGENES imag = new HC_EMERGENCIA_IMAGENES();
                List<HC_EMERGENCIA_IMAGENES> listIma = new List<HC_EMERGENCIA_IMAGENES>();
                listIma = NegHcEmergenciaImagenes.RecuperarHcEmergenciaImagenes(modificadaEmergencia.COD_EMERGENCIA);
                fila = 0;
                if (dataGridViewImagenes.Rows.Count > 0)
                {
                    if (listIma.Count > 0)
                    {
                        for (int i = 0; i < listIma.Count; i++)
                        {
                            imag = listIma.ElementAt(i);
                            imag.IMA_HC_CATALOGOS = dataGridViewImagenes.Rows[i].Cells[0].Value.ToString();
                            imag.IMA_COMUNICADO = dataGridViewImagenes.Rows[i].Cells[1].Value.ToString();
                            imag.IMA_RESULTADO = dataGridViewImagenes.Rows[i].Cells[2].Value.ToString();
                            NegHcEmergenciaImagenes.actualizarHcImagen(imag);
                            fila = i;
                        }
                        if (((dataGridViewImagenes.Rows.Count - 1) - listIma.Count) > 0)
                        {
                            for (int i = fila + 1; i < dataGridViewImagenes.Rows.Count - 1; i++)
                            {
                                imag = new HC_EMERGENCIA_IMAGENES();
                                imag.IMA_CODIGO = NegHcEmergenciaImagenes.RecuperaMaximoHcEmergenciaImagenes() + 1;
                                imag.IMA_EMERGENCIA = codigoEmergencia;
                                imag.IMA_HC_CATALOGOS = dataGridViewImagenes.Rows[i].Cells[0].Value.ToString();
                                imag.IMA_COMUNICADO = dataGridViewImagenes.Rows[i].Cells[1].Value.ToString();
                                imag.IMA_RESULTADO = dataGridViewImagenes.Rows[i].Cells[2].Value.ToString();
                                NegHcEmergenciaImagenes.crearHcEmergenciaImagenes(imag);
                            }
                        }
                    }
                    else
                    {
                        cargarImagenes();
                    }
                }

                ////INTERCONSULTAS
                HC_EMERGENCIA_EVALUACION eval = new HC_EMERGENCIA_EVALUACION();
                List<HC_EMERGENCIA_EVALUACION> listEval = new List<HC_EMERGENCIA_EVALUACION>();
                listEval = NegHcEmergenciaEvaluacion.RecuperarHcEmergenciaEvaluacion(modificadaEmergencia.COD_EMERGENCIA);
                fila = 0;
                if (dataGridViewInterconsultas.Rows.Count > 0)
                {
                    if (listEval.Count > 0)
                    {
                        for (int i = 0; i < listEval.Count; i++)
                        {
                            eval = listEval.ElementAt(i);
                            eval.INT_HC_CATALOGOS = dataGridViewInterconsultas.Rows[i].Cells[0].Value.ToString();                            
                            estadoFecha = false;
                            validarFecha(dataGridViewInterconsultas.Rows[i].Cells[1].Value.ToString(), estadoFecha, "Valida");
                            if (estadoFecha == true)
                                eval.INT_COMUNICADO = DateTime.Parse(dataGridViewInterconsultas.Rows[i].Cells[1].Value.ToString());
                            else
                                eval.INT_COMUNICADO = null;  
                            estadoFecha = false;
                            validarFecha(dataGridViewInterconsultas.Rows[i].Cells[2].Value.ToString(), estadoFecha, "Valida");
                            if (estadoFecha == true)
                                eval.INT_REALIZADO = DateTime.Parse(dataGridViewInterconsultas.Rows[i].Cells[2].Value.ToString());                    
                            else
                                eval.INT_REALIZADO = null;                            
                            eval.INT_OBSERVACIONES = dataGridViewInterconsultas.Rows[i].Cells[3].Value.ToString();
                            NegHcEmergenciaEvaluacion.actualizarHcEvaluacion(eval);
                            fila = i;
                        }
                        if (((dataGridViewInterconsultas.Rows.Count - 1) - listEval.Count) > 0)
                        {
                            for (int i = fila + 1; i < dataGridViewInterconsultas.Rows.Count - 1; i++)
                            {
                                eval = new HC_EMERGENCIA_EVALUACION();                           
                                eval.INT_CODIGO = NegHcEmergenciaEvaluacion.RecuperaMaximoHcEmergenciaEvaluacion() + 1;
                                eval.INT_EMERGENCIA = codigoEmergencia;
                                eval.INT_HC_CATALOGOS = dataGridViewInterconsultas.Rows[i].Cells[0].Value.ToString();
                                estadoFecha = false;
                                validarFecha(dataGridViewInterconsultas.Rows[i].Cells[1].Value.ToString(), estadoFecha, "Valida");
                                if (estadoFecha == true)
                                    eval.INT_COMUNICADO = DateTime.Parse(dataGridViewInterconsultas.Rows[i].Cells[1].Value.ToString());
                                else
                                    eval.INT_COMUNICADO = null;
                                estadoFecha = false;
                                validarFecha(dataGridViewInterconsultas.Rows[i].Cells[2].Value.ToString(), estadoFecha, "Valida");
                                if (estadoFecha == true)
                                    eval.INT_REALIZADO = DateTime.Parse(dataGridViewInterconsultas.Rows[i].Cells[2].Value.ToString());
                                else
                                    eval.INT_REALIZADO = null;                                 
                                eval.INT_OBSERVACIONES = dataGridViewInterconsultas.Rows[i].Cells[3].Value.ToString();
                                NegHcEmergenciaEvaluacion.crearHcEmergenciaEvaluacion(eval);
                            }
                        }
                    }
                    else
                    {
                        cargarInterconsultas();
                    }
                }                               

                ////REFERIDO A
                HC_EMERGENCIA_REFERIDOS refer = new HC_EMERGENCIA_REFERIDOS();
                List<HC_EMERGENCIA_REFERIDOS> listRefer = new List<HC_EMERGENCIA_REFERIDOS>();
                listRefer = NegHcEmergenciaReferido.RecuperarHcEmergenciaReferencias(modificadaEmergencia.COD_EMERGENCIA);
                fila = 0;
                if (dataGridViewReferido.Rows.Count > 0)
                {
                    if (listRefer.Count > 0)
                    {
                        for (int i = 0; i < listRefer.Count; i++)
                        {
                            refer = listRefer.ElementAt(i);
                            refer.REF_HC_CATALOGO = dataGridViewReferido.Rows[i].Cells[0].Value.ToString();
                            refer.REF_COMUNICADO = dataGridViewReferido.Rows[i].Cells[1].Value.ToString();
                            estadoFecha = false;
                            validarFecha(dataGridViewReferido.Rows[i].Cells[2].Value.ToString(), estadoFecha, "Valida");
                            if (estadoFecha == true)
                                refer.REF_REALIZADO = DateTime.Parse(dataGridViewReferido.Rows[i].Cells[2].Value.ToString());
                            else
                                refer.REF_REALIZADO = null;   
                            refer.REF_OBSERVACION = dataGridViewReferido.Rows[i].Cells[3].Value.ToString();                     
                            NegHcEmergenciaReferido.actualizarHcReferido(refer);
                            fila = i;
                        }
                        if (((dataGridViewReferido.Rows.Count - 1) - listRefer.Count) > 0)
                        {
                            for (int i = fila + 1; i < dataGridViewReferido.Rows.Count - 1; i++)
                            {
                                refer = new HC_EMERGENCIA_REFERIDOS();
                                refer.REF_CODIGO = NegHcEmergenciaReferido.RecuperaMaximoHcEmergenciaReferido() + 1;
                                refer.REF_EMERGENCIA = codigoEmergencia;
                                refer.REF_HC_CATALOGO = dataGridViewReferido.Rows[i].Cells[0].Value.ToString();
                                refer.REF_COMUNICADO = dataGridViewReferido.Rows[i].Cells[1].Value.ToString();
                                estadoFecha = false;
                                validarFecha(dataGridViewReferido.Rows[i].Cells[2].Value.ToString(), estadoFecha, "Valida");
                                if (estadoFecha == true)
                                    refer.REF_REALIZADO = DateTime.Parse(dataGridViewReferido.Rows[i].Cells[2].Value.ToString());
                                else
                                    refer.REF_REALIZADO = null;                                  
                                refer.REF_OBSERVACION = dataGridViewReferido.Rows[i].Cells[3].Value.ToString();
                                NegHcEmergenciaReferido.crearHcEmergenciaReferido(refer);                                
                            }
                        }
                    }
                    else
                    {
                        cargarReferido();
                    }
                }
                Boolean estado = false;
                HC_CATALOGOS cat = new HC_CATALOGOS();
                List<HC_EMERGENCIA_TRATAMIENTO> listaTrat = new List<HC_EMERGENCIA_TRATAMIENTO>();
                listaTrat = NegHcEmergenciaTratamiento.RecuperarHcEmergenciaTratamientos(modificadaEmergencia.COD_EMERGENCIA);
                if (listaTrat.Count > 0)
                {
                    for (int i = 0; i < chbL_SelecTratam.Items.Count; i++)
                    {
                        if (chbL_SelecTratam.GetItemCheckState(i) == CheckState.Checked)
                        {
                            estado = false;
                            cat = (HC_CATALOGOS)chbL_SelecTratam.Items[i];
                            HC_EMERGENCIA_TRATAMIENTO tratamiento = new HC_EMERGENCIA_TRATAMIENTO();
                            for (int j = 0; j < listaTrat.Count; j++)
                            {
                                tratamiento = new HC_EMERGENCIA_TRATAMIENTO();
                                tratamiento = listaTrat.ElementAt(j);
                                if (tratamiento.TRA_HC_CATALOGOS == cat.HCC_CODIGO)
                                {
                                    estado = true;
                                    break;
                                }
                            }
                            if (estado == false)
                            {
                                tratamiento = new HC_EMERGENCIA_TRATAMIENTO();
                                tratamiento.TRA_CODIGO = NegHcEmergenciaTratamiento.RecuperaMaximoHcEmergenciaTratamiento() + 1;
                                tratamiento.TRA_EMERGENCIA = codigoEmergencia;
                                tratamiento.TRA_HC_CATALOGOS = cat.HCC_CODIGO;
                                NegHcEmergenciaTratamiento.crearHcEmergenciaTratamiento(tratamiento);
                            }
                        }
                        else 
                        {
                            cat = (HC_CATALOGOS)chbL_SelecTratam.Items[i];
                            for (int j = 0; j < listaTrat.Count; j++)
                            {
                                HC_EMERGENCIA_TRATAMIENTO tratamiento = new HC_EMERGENCIA_TRATAMIENTO();
                                tratamiento = listaTrat.ElementAt(j);
                                if (tratamiento.TRA_HC_CATALOGOS == cat.HCC_CODIGO)
                                {
                                    NegHcEmergenciaTratamiento.EliminarHcEmergenciaTratamiento(tratamiento);
                                }
                            }
                        }                         
                    }
                }
                else
                {
                    for (int i = 0; i < chbL_SelecTratam.Items.Count; i++)
                    {
                        if (chbL_SelecTratam.GetItemCheckState(i) == CheckState.Checked)
                        {
                            cat = (HC_CATALOGOS)chbL_SelecTratam.Items[i];
                            HC_EMERGENCIA_TRATAMIENTO tratamiento = new HC_EMERGENCIA_TRATAMIENTO();
                            tratamiento.TRA_CODIGO = NegHcEmergenciaTratamiento.RecuperaMaximoHcEmergenciaTratamiento() + 1;
                            tratamiento.TRA_EMERGENCIA = codigoEmergencia;
                            tratamiento.TRA_HC_CATALOGOS = cat.HCC_CODIGO;
                            NegHcEmergenciaTratamiento.crearHcEmergenciaTratamiento(tratamiento);
                        }
                    }
                }

                //chbL_CirugiasPrev
                estado = false;
                cat = new HC_CATALOGOS();
                List<HC_EMERGENCIA_CIRUGIAS> listaCirug = new List<HC_EMERGENCIA_CIRUGIAS>();
                HC_EMERGENCIA_CIRUGIAS cirugia = new HC_EMERGENCIA_CIRUGIAS();
                listaCirug = NegHcEmergenciaCirugias.RecuperarHcEmergenciaCirugias(modificadaEmergencia.COD_EMERGENCIA);
                if (listaCirug.Count > 0)
                {
                    for (int i = 0; i < chbL_CirugiasPrev.Items.Count; i++)
                    {
                        if (chbL_CirugiasPrev.GetItemCheckState(i) == CheckState.Checked)
                        {
                            estado = false;
                            cat = (HC_CATALOGOS)chbL_CirugiasPrev.Items[i];
                            for (int j = 0; j < listaCirug.Count; j++)
                            {
                                cirugia = new HC_EMERGENCIA_CIRUGIAS();
                                cirugia = listaCirug.ElementAt(j);
                                if (cirugia.CIR_HC_CATALOGOS == cat.HCC_CODIGO)
                                {
                                    estado = true;
                                    break;
                                }
                            }
                            if (estado == false)
                            {
                                cirugia = new HC_EMERGENCIA_CIRUGIAS();
                                cirugia.CIR_CODIGO = NegHcEmergenciaCirugias.RecuperaMaximoHcEmergenciaCirugias() + 1;
                                cirugia.CIR_EMERGENCIA = codigoEmergencia;
                                cirugia.CIR_HC_CATALOGOS = cat.HCC_CODIGO;
                                NegHcEmergenciaCirugias.crearHcEmergenciaCirugias(cirugia);
                            }
                        }
                        else
                        {
                            cat = (HC_CATALOGOS)chbL_CirugiasPrev.Items[i];
                            for (int j = 0; j < listaCirug.Count; j++)
                            {
                                cirugia = new HC_EMERGENCIA_CIRUGIAS();
                                cirugia = listaCirug.ElementAt(j);
                                if (cirugia.CIR_HC_CATALOGOS == cat.HCC_CODIGO)
                                {
                                    NegHcEmergenciaCirugias.EliminarHcEmergenciaCirugia(cirugia);
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chbL_CirugiasPrev.Items.Count; i++)
                    {
                        if (chbL_CirugiasPrev.GetItemCheckState(i) == CheckState.Checked)
                        {
                            cat = (HC_CATALOGOS)chbL_CirugiasPrev.Items[i];
                            cirugia = new HC_EMERGENCIA_CIRUGIAS();
                            cirugia.CIR_CODIGO = NegHcEmergenciaCirugias.RecuperaMaximoHcEmergenciaCirugias() + 1;
                            cirugia.CIR_EMERGENCIA = codigoEmergencia;
                            cirugia.CIR_HC_CATALOGOS = cat.HCC_CODIGO;
                            NegHcEmergenciaCirugias.crearHcEmergenciaCirugias(cirugia);
                        }
                    }
                }                
                estado = false;
                cat = new HC_CATALOGOS();
                List<HC_EMERGENCIA_COMPLICACIONES> listaCompl = new List<HC_EMERGENCIA_COMPLICACIONES>();
                HC_EMERGENCIA_COMPLICACIONES complicacion = new HC_EMERGENCIA_COMPLICACIONES();
                listaCompl = NegHcEmergenciaComplicaciones.RecuperarHcEmergenciaComplicaciones(modificadaEmergencia.COD_EMERGENCIA);
                if (listaCompl.Count > 0)
                {
                    for (int i = 0; i < chdL_Complicaciones.Items.Count; i++)
                    {
                        if (chdL_Complicaciones.GetItemCheckState(i) == CheckState.Checked)
                        {
                            estado = false;
                            cat = (HC_CATALOGOS)chdL_Complicaciones.Items[i];                            
                            for (int j = 0; j < listaCompl.Count; j++)
                            {
                                complicacion = new HC_EMERGENCIA_COMPLICACIONES();
                                complicacion = listaCompl.ElementAt(j);
                                if (complicacion.COM_HC_CATALOGOS == cat.HCC_CODIGO)
                                {
                                    estado = true;
                                    break;
                                }
                            }
                            if (estado == false)
                            {
                                complicacion = new HC_EMERGENCIA_COMPLICACIONES();
                                complicacion.COM_CODIGO = NegHcEmergenciaComplicaciones.RecuperaMaximoHcEmergenciaComplicaciones() + 1;
                                complicacion.COM_EMERGENCIA = codigoEmergencia;
                                complicacion.COM_HC_CATALOGOS = cat.HCC_CODIGO;
                                NegHcEmergenciaComplicaciones.crearHcEmergenciaComplicaciones(complicacion);
                            }
                        }
                        else
                        {
                            cat = (HC_CATALOGOS)chdL_Complicaciones.Items[i];
                            for (int j = 0; j < listaCompl.Count; j++)
                            {
                                complicacion = new HC_EMERGENCIA_COMPLICACIONES();
                                complicacion = listaCompl.ElementAt(j);
                                if (complicacion.COM_HC_CATALOGOS == cat.HCC_CODIGO)
                                {
                                    NegHcEmergenciaComplicaciones.EliminarHcEmergenciaComplicaciones(complicacion);
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chdL_Complicaciones.Items.Count; i++)
                    {
                        if (chdL_Complicaciones.GetItemCheckState(i) == CheckState.Checked)
                        {
                            cat = (HC_CATALOGOS)chdL_Complicaciones.Items[i];
                            complicacion = new HC_EMERGENCIA_COMPLICACIONES();
                            complicacion.COM_CODIGO = NegHcEmergenciaComplicaciones.RecuperaMaximoHcEmergenciaComplicaciones() + 1;
                            complicacion.COM_EMERGENCIA = codigoEmergencia;
                            complicacion.COM_HC_CATALOGOS = cat.HCC_CODIGO;
                            NegHcEmergenciaComplicaciones.crearHcEmergenciaComplicaciones(complicacion);
                        }
                    }
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }       
        }

        private void rdbIndirecto_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCamposPaciente();
            txt_historiaclinica.Text = string.Empty;
            txtRegistro.Text = string.Empty;
            habilitarBotones(false, false, false, false, false,false,true);            
        }
        
        public void cargarExamen(Boolean accion)
        {
            try
            {                
                if (accion == true)
                {
                    examen = new HC_CATALOGOS();
                    frm_Catalogo form = new frm_Catalogo(1);
                    form.ShowDialog();
                    examen = form.catalogoFinal;
                    comunicado = "";
                    resultado = "";
                }
                if (examen != null)
                {
                    DataRow drCatalogo;
                    //dataGridViewExamenes.DataSource = dtCatalogoExamen;
                    dataGridViewExamenes.DataSource = dtCatalogoExamen;
                    if (dataGridViewExamenes.Rows.Count == 0)
                    {
                        dtCatalogoExamen.Columns.Add("NOMBRE", Type.GetType("System.String"));
                        dtCatalogoExamen.Columns.Add("COMUNICADO", Type.GetType("System.String"));
                        dtCatalogoExamen.Columns.Add("RESULTADO", Type.GetType("System.String"));

                    }
                    if (dataGridViewExamenes.Rows.Count >= 1)
                    {
                        drCatalogo = dtCatalogoExamen.NewRow();
                        drCatalogo["NOMBRE"] = examen.HCC_NOMBRE;
                        drCatalogo["COMUNICADO"] = comunicado;
                        drCatalogo["RESULTADO"] = resultado;
                        dtCatalogoExamen.Rows.Add(drCatalogo);
                    }
                }                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       

        public void cargarImagen(Boolean accion)
        {
            try
            {                
                if (accion == true)
                {
                    imagen = new HC_CATALOGOS();
                    frm_Catalogo form = new frm_Catalogo(2);
                    form.ShowDialog();
                    imagen = form.catalogoFinal;
                    comunicado = "";
                    resultado = "";
                }
                if (imagen != null) 
                {
                    DataRow drCatalogo;
                    //dataGridViewImagenes.DataSource = dtCatalogoImagen;
                    dataGridViewImagenes.DataSource = dtCatalogoImagen;
                    if (dataGridViewImagenes.Rows.Count == 0)
                    {
                        dtCatalogoImagen.Columns.Add("NOMBRE", Type.GetType("System.String"));
                        dtCatalogoImagen.Columns.Add("COMUNICADO", Type.GetType("System.String"));
                        dtCatalogoImagen.Columns.Add("RESULTADO", Type.GetType("System.String"));

                    }
                    if (dataGridViewImagenes.Rows.Count >= 1)
                    {
                        drCatalogo = dtCatalogoImagen.NewRow();
                        drCatalogo["NOMBRE"] = imagen.HCC_NOMBRE;
                        drCatalogo["COMUNICADO"] = comunicado;
                        drCatalogo["RESULTADO"] = resultado;
                        dtCatalogoImagen.Rows.Add(drCatalogo);
                    }
                }                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Interconsultas_Click(object sender, EventArgs e)
        {
            try
            {
                //cargarInterconsulta(true);     

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cargarInterconsulta(Boolean accion)
        {
            try
            {
                if (accion == true)
                {
                    inter = new HC_CATALOGOS();
                    frm_Catalogo form = new frm_Catalogo(3);
                    form.ShowDialog();
                    inter = form.catalogoFinal;
                    comunicado = "";                    
                    observaciones = "";
                }    
                if(inter != null)
                {
                    DataRow drCatalogoInter;
                    dataGridViewInterconsultas.DataSource = dtCatalogoInter;
                    dataGridViewInterconsultas.DataSource = dtCatalogoInter;
                    if (dataGridViewInterconsultas.Rows.Count == 0)
                    {
                        dtCatalogoInter.Columns.Add("ESPECIALIDAD", Type.GetType("System.String"));
                        dtCatalogoInter.Columns.Add("COMUNICADO", Type.GetType("System.String"));
                        dtCatalogoInter.Columns.Add("REALIZADO", Type.GetType("System.String"));
                        dtCatalogoInter.Columns.Add("OBSERVACIONES", Type.GetType("System.String"));

                    }
                    if (dataGridViewInterconsultas.Rows.Count >= 1)
                    {
                        drCatalogoInter = dtCatalogoInter.NewRow();
                        drCatalogoInter["ESPECIALIDAD"] = inter.HCC_NOMBRE;                        
                        drCatalogoInter["OBSERVACIONES"] = observaciones;
                        if (accion == false)
                        {
                            if (estadoFecha ==true ) 
                                drCatalogoInter["COMUNICADO"] = Convert.ToString(comunicadoFecha);
                            else
                                drCatalogoInter["COMUNICADO"] = comunicado;
                            if(estadoFechaRea == true)
                                drCatalogoInter["REALIZADO"] = Convert.ToString(realizado);
                            else
                                drCatalogoInter["REALIZADO"] = comunicado;                  
                        }
                        else 
                        {
                            drCatalogoInter["COMUNICADO"] = comunicado;
                            drCatalogoInter["REALIZADO"] = comunicado;
                        }
                        dtCatalogoInter.Rows.Add(drCatalogoInter);
                    } 
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Referido_Click(object sender, EventArgs e)
        {
            try
            {
                cargarReferido(true);

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarReferido(Boolean accion) 
        {
            try
            {
                if (accion == true)
                {
                    referido = new HC_CATALOGOS();
                    frm_Catalogo form = new frm_Catalogo(4);
                    form.ShowDialog();
                    referido = form.catalogoFinal;
                    //nombreI = "";
                    comunicado = "";
                    observaciones = "";                    
                }   
                if(referido != null)
                {
                    DataRow drCatalogoReferido;
                    dataGridViewReferido.DataSource = dtCatalogoReferido;
                    //dataGridViewReferido.DataSource = dtCatalogoReferido;
                    if (dataGridViewReferido.Rows.Count == 0)
                    {
                        dtCatalogoReferido.Columns.Add("ESPECIALIDAD", Type.GetType("System.String"));
                        dtCatalogoReferido.Columns.Add("NOMBRE", Type.GetType("System.String"));
                        dtCatalogoReferido.Columns.Add("FECHA DE CITA", Type.GetType("System.String"));
                        dtCatalogoReferido.Columns.Add("OBSERVACIONES", Type.GetType("System.String"));

                    }
                    if (dataGridViewReferido.Rows.Count >= 1)
                    {
                        drCatalogoReferido = dtCatalogoReferido.NewRow();
                        drCatalogoReferido["ESPECIALIDAD"] = referido.HCC_NOMBRE;
                        drCatalogoReferido["NOMBRE"] = nombreI;
                        drCatalogoReferido["FECHA DE CITA"] = comunicado;
                        if (accion == false)
                            drCatalogoReferido["FECHA DE CITA"] = Convert.ToString(comunicadoFecha);                
                        else                      
                            drCatalogoReferido["FECHA DE CITA"] = comunicado;             
                        drCatalogoReferido["OBSERVACIONES"] = observaciones;
                        dtCatalogoReferido.Rows.Add(drCatalogoReferido);
                    }
                }               
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                

        private void dataGridViewExamenes_KeyPress(object sender, KeyPressEventArgs e)
        {  
            
        }

        private void dataGridViewInterconsultas_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {            
                int columna = dataGridViewInterconsultas.SelectedCells[0].ColumnIndex;
                int fila = dataGridViewInterconsultas.SelectedCells[0].RowIndex;

                if (dataGridViewInterconsultas.SelectedCells[0].ColumnIndex == 1)
                {
                    dataGridViewInterconsultas.Rows[fila-1].Cells[columna].Value = DateTime.Now;
                }
                if (dataGridViewInterconsultas.SelectedCells[0].ColumnIndex == 2)
                {
                    dataGridViewInterconsultas.Rows[fila-1].Cells[columna].Value = DateTime.Now;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridViewReferido_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int columna = dataGridViewReferido.SelectedCells[0].ColumnIndex;
                int fila = dataGridViewReferido.SelectedCells[0].RowIndex;

                if (dataGridViewReferido.SelectedCells[0].ColumnIndex == 2)
                {
                    dataGridViewReferido.Rows[fila-1].Cells[columna].Value = DateTime.Now;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void gridEnfPrevias_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Delete)               
                {
                    CIE10 cie = new CIE10();

                    cie = (CIE10)gridEnfPrevias.ActiveRow.ListObject;
                    List<HC_EMERGENCIA_EPREVIAS> listaEfer = new List<HC_EMERGENCIA_EPREVIAS>();
                    listaEfer = NegHcEmergenciaEnfPrevias.RecuperarHcEmergenciaEnfPrevias(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < listaEfer.Count; i++ ) 
                    {
                        HC_EMERGENCIA_EPREVIAS previa = new HC_EMERGENCIA_EPREVIAS();
                        previa = listaEfer.ElementAt(i);
                        if(previa.EFP_CIE10 == cie.CIE_CODIGO)                        
                        {
                            MessageBox.Show(previa.EFP_CIE10);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtAlergias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                //dateTimeFum.Focus();
            }
        }

        private void dateTimeFum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                dataGridViewSubSintomas.Focus();
            }
            //dataGridViewSubSintomas
        }
        //private void dataGridViewSubSintomas_KeyPress(object sender, KeyPressEventArgs e)
        //{            
        //}

        //private void dateTimePicker2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(Keys.Enter))
        //    {
        //        e.Handled = true;
        //        cb_AtencionMedica.Focus();
        //    }
        //}

        //private void cb_AtencionMedica_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(Keys.Enter))
        //    {
        //        e.Handled = true;
        //        cb_TratamientoActual.Focus();
        //    }
        //}

        //private void cb_TratamientoActual_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(Keys.Enter))
        //    {
        //        e.Handled = true;
        //        cb_Procedimiento.Focus();
        //    }
        //}

        //private void cb_Procedimiento_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(Keys.Enter))
        //    {
        //        e.Handled = true;
        //        rdb_Directo.Focus();
        //    }
        //}

        //private void rdb_Directo_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(Keys.Enter))
        //    {
        //        e.Handled = true;
        //        rdb_Indirecto.Focus();
        //    }
        //}

        private void rdb_Indirecto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtTA1.Focus();
            }
        }

        private void txtTA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == (char)(Keys.Back) && this.txtTA1.Text != String.Empty))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    txtTA2.Focus();
                }
                else if (!Char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
            else
                this.txtTA1.Text = "0";
        }
        private void txtTA2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))            
                e.Handled = true;            
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtT.Focus();
            }

        }

        private void txtT_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtSat.Focus();
            }
        }

        private void txtSat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
                e.Handled = true; 
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtFc.Focus();
            }
        }
        private void txtFc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
                e.Handled = true; 
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtFr.Focus();
            }
        }

        private void txtFr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
                e.Handled = true; 
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cb_Glasgow.Focus();
            }
        }                

        private void cb_Glasgow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                rdb_NoUrgente.Focus();
            }
        }

        private void rdb_NoUrgente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                rdb_Emergencia.Focus();
            }
        }

        private void rdb_Emergencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                rdb_Critico.Focus();
            }
        }

        private void rdb_Critico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                rdb_Otras.Focus();
            }
        }

        private void rdb_Otras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
            }
        }

        private void cb_TriageOtras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                chbAlcoholC.Focus();
            }
        }

        private void chbAlcoholC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                chbOtrasC.Focus();
                txtOtrasActual.Text = string.Empty;
                txtOtrasActual.Enabled = false;
            }
        }

        private void chbTabacoC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                chbDrogasC.Focus();
                txtOtrasActual.Text = string.Empty;
                txtOtrasActual.Enabled = true;
            }
        }

        private void chbDrogasC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtOrl.Focus();
            }
        }

        private void txtOrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtCabeza.Focus();
            }
        }

        private void txtCabeza_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtCuello.Focus();
            }
        }

        private void txtCuello_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtTorax.Focus();
            }
        }

        private void txtTorax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtCardiaco.Focus();
            }
        }

        private void txtCardiaco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtPulmonar.Focus();
            }
        }

        private void txtPulmonar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtAbdomen.Focus();
            }
        }

        private void txtAbdomen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtGenitales.Focus();
            }
        }

        private void txtGenitales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtPerine.Focus();
            }
        }

        private void txtPerine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtExtremidades.Focus();
            }
        }

        private void txtExtremidades_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtFosasL.Focus();
            }
        }

        private void txtFosasL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtLinfa.Focus();
            }
        }

        private void txtLinfa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtNeuro.Focus();
            }
        }

        private void txtNeuro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtPiel.Focus();
            }
        }

        private void txtPiel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtObserEnfer.Focus();
            }
        }

        private void txtObserEnfer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtDiagEmerg.Focus();
            }
        }

        private void txtDiagEmerg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtDiagDef.Focus();
            }
        }

        private void txtDiagDef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cb_Glasgow.Focus();
            }
        }

        private void buscarHistorias_Click(object sender, EventArgs e)
        {    
            try
            {              
                codigoPaciente = pacienteActual.PAC_CODIGO;
                if (codigoPaciente != 0) 
                {
                    string nomPac = pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2 + " " + pacienteActual.PAC_APELLIDO_PATERNO + " "+ pacienteActual.PAC_APELLIDO_MATERNO; 
                    frm_AyudaEmergencias form = new frm_AyudaEmergencias(codigoPaciente,nomPac);
                    form.ShowDialog();
                    modificadaEmergencia = form.nuevaEmergenciaBuscada;
                    if (modificadaEmergencia != null)
                    {
                        string historiaPaciente = modificadaEmergencia.PACIENTES.PAC_HISTORIA_CLINICA;
                        txtRegistro.Text = Convert.ToString(modificadaEmergencia.COD_EMERGENCIA);
                        if (historiaPaciente != null)
                        {
                            //cargarTablas();
                            cargarPaciente(historiaPaciente);
                            cargarEmergencia(modificadaEmergencia.COD_EMERGENCIA);
                            if (modificadaEmergencia.EME_ESTADO == true)
                                habilitarBotones(false, true, true, true, true, true, true);
                            else
                                habilitarBotones(false, true, true, true, true, true, true);

                        }
                    } 
                }                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea cerrar la ficha de Registro de Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    //MessageBox.Show(" codigo :"+modificadaEmergencia.COD_EMERGENCIA);
                    NegHcEmergencia.eliminarHcEmergencia(modificadaEmergencia);
                    List<HC_EMERGENCIA_CIRUGIAS> lisCirugias = new List<HC_EMERGENCIA_CIRUGIAS>();
                    lisCirugias = NegHcEmergenciaCirugias.RecuperarHcEmergenciaCirugias(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < lisCirugias.Count; i++)
                    {
                        HC_EMERGENCIA_CIRUGIAS cirugia = new HC_EMERGENCIA_CIRUGIAS();
                        cirugia = lisCirugias.ElementAt(i);
                        NegHcEmergenciaCirugias.EliminarHcEmergenciaCirugia(cirugia);
                    }
                    List<HC_EMERGENCIA_COMPLICACIONES> lisCompli = new List<HC_EMERGENCIA_COMPLICACIONES>();
                    lisCompli = NegHcEmergenciaComplicaciones.RecuperarHcEmergenciaComplicaciones(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < lisCompli.Count; i++)
                    {
                        HC_EMERGENCIA_COMPLICACIONES compl = new HC_EMERGENCIA_COMPLICACIONES();
                        compl = lisCompli.ElementAt(i);
                        NegHcEmergenciaComplicaciones.EliminarHcEmergenciaComplicaciones(compl);
                    }
                    List<HC_EMERGENCIA_EPREVIAS> lisEnfPrevias = new List<HC_EMERGENCIA_EPREVIAS>();
                    lisEnfPrevias = NegHcEmergenciaEnfPrevias.RecuperarHcEmergenciaEnfPrevias(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < lisEnfPrevias.Count; i++)
                    {
                        HC_EMERGENCIA_EPREVIAS enfPrev = new HC_EMERGENCIA_EPREVIAS();
                        enfPrev = lisEnfPrevias.ElementAt(i);
                        NegHcEmergenciaEnfPrevias.EliminarHcEmergenciaEnfPrevias(enfPrev);
                    }
                    List<HC_EMERGENCIA_EVALUACION> lisEval = new List<HC_EMERGENCIA_EVALUACION>();
                    lisEval = NegHcEmergenciaEvaluacion.RecuperarHcEmergenciaEvaluacion(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < lisEval.Count; i++)
                    {
                        HC_EMERGENCIA_EVALUACION eval = new HC_EMERGENCIA_EVALUACION();
                        eval = lisEval.ElementAt(i);
                        NegHcEmergenciaEvaluacion.EliminarHcEmergenciaEvaluacion(eval);
                    }
                    List<HC_EMERGENCIA_EXAMENES> lisExam = new List<HC_EMERGENCIA_EXAMENES>();
                    lisExam = NegHcEmergenciaExamenes.RecuperarHcEmergenciaExamenes(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < lisExam.Count; i++)
                    {
                        HC_EMERGENCIA_EXAMENES exam = new HC_EMERGENCIA_EXAMENES();
                        exam = lisExam.ElementAt(i);
                        NegHcEmergenciaExamenes.EliminarHcEmergenciaExamen(exam);
                    }

                    List<HC_EMERGENCIA_IMAGENES> lisImag = new List<HC_EMERGENCIA_IMAGENES>();
                    lisImag = NegHcEmergenciaImagenes.RecuperarHcEmergenciaImagenes(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < lisImag.Count; i++)
                    {
                        HC_EMERGENCIA_IMAGENES imag = new HC_EMERGENCIA_IMAGENES();
                        imag = lisImag.ElementAt(i);
                        NegHcEmergenciaImagenes.EliminarHcEmergenciaImagen(imag);
                    }
                    List<HC_EMERGENCIA_REFERIDOS> lisRef = new List<HC_EMERGENCIA_REFERIDOS>();
                    lisRef = NegHcEmergenciaReferido.RecuperarHcEmergenciaReferencias(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < lisRef.Count; i++)
                    {
                        HC_EMERGENCIA_REFERIDOS refer = new HC_EMERGENCIA_REFERIDOS();
                        refer = lisRef.ElementAt(i);
                        NegHcEmergenciaReferido.EliminarHcEmergenciaReferido(refer);
                    }
                    List<HC_EMERGENCIA_SS> lisSubS = new List<HC_EMERGENCIA_SS>();
                    lisSubS = NegHcEmergenciaSubsintomas.RecuperarHcEmergenciaSubSintomas(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < lisSubS.Count; i++)
                    {
                        HC_EMERGENCIA_SS refer = new HC_EMERGENCIA_SS();
                        refer = lisSubS.ElementAt(i);
                        NegHcEmergenciaSubsintomas.EliminarHcEmergenciaSubSintoma(refer);
                    }
                    List<HC_EMERGENCIA_TRATAMIENTO> lisTrat = new List<HC_EMERGENCIA_TRATAMIENTO>();
                    lisTrat = NegHcEmergenciaTratamiento.RecuperarHcEmergenciaTratamientos(modificadaEmergencia.COD_EMERGENCIA);
                    for (int i = 0; i < lisTrat.Count; i++)
                    {
                        HC_EMERGENCIA_TRATAMIENTO trat = new HC_EMERGENCIA_TRATAMIENTO();
                        trat = lisTrat.ElementAt(i);
                        NegHcEmergenciaTratamiento.EliminarHcEmergenciaTratamiento(trat);
                    }
                    limpiarCamposPaciente();
                    leerUltimaEmergencia();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void dataGridViewSubSintomas_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    e.Handled = true;
                    int index = dataGridViewSubSintomas.CurrentRow.Index;
                    if (index < dataGridViewSubSintomas.Rows.Count - 1)
                    {
                        dataGridViewSubSintomas.Rows.RemoveAt(index);
                        List<HC_EMERGENCIA_SS> listSub = new List<HC_EMERGENCIA_SS>();
                        listSub = NegHcEmergenciaSubsintomas.RecuperarHcEmergenciaSubSintomas(modificadaEmergencia.COD_EMERGENCIA);
                        if (listSub.Count > 0)
                        {
                            HC_EMERGENCIA_SS subSintomas = new HC_EMERGENCIA_SS();
                            subSintomas = listSub.ElementAt(index);
                            NegHcEmergenciaSubsintomas.EliminarHcEmergenciaSubSintoma(subSintomas);
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void dataGridViewExamenes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    e.Handled = true;
                    int index = dataGridViewExamenes.CurrentRow.Index;
                    if (index < dataGridViewExamenes.Rows.Count - 1)
                    {
                        dataGridViewExamenes.Rows.RemoveAt(index);
                        List<HC_EMERGENCIA_EXAMENES> listExa = new List<HC_EMERGENCIA_EXAMENES>();
                        listExa = NegHcEmergenciaExamenes.RecuperarHcEmergenciaExamenes(modificadaEmergencia.COD_EMERGENCIA);
                        if (listExa.Count > 0)
                        {
                            HC_EMERGENCIA_EXAMENES examen = new HC_EMERGENCIA_EXAMENES();
                            examen = listExa.ElementAt(index);
                            NegHcEmergenciaExamenes.EliminarHcEmergenciaExamen(examen);
                        }
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        if (dataGridViewExamenes.Rows.Count == 0)
                        {
                            cargarExamen(true);
                        }
                        else
                        {
                            int columna = dataGridViewExamenes.SelectedCells[0].ColumnIndex;
                            int fila = dataGridViewExamenes.SelectedCells[0].RowIndex;
                            string nombreCat = dataGridViewExamenes.Rows[fila].Cells[0].Value.ToString();
                            if (columna == 0)
                            {
                                if (nombreCat == "")
                                {
                                    cargarExamen(true);
                                    dataGridViewExamenes.Rows.RemoveAt(dataGridViewExamenes.Rows.Count - 2);
                                }
                                else
                                {
                                    examen = new HC_CATALOGOS();
                                    frm_Catalogo form = new frm_Catalogo(2);
                                    form.ShowDialog();
                                    examen = form.catalogoFinal;
                                    if (examen != null)
                                    {
                                        dataGridViewExamenes.Rows[fila].Cells[0].Value = examen.HCC_NOMBRE;
                                        dataGridViewExamenes.Rows[fila].Cells[1].Value = "";
                                        dataGridViewExamenes.Rows[fila].Cells[2].Value = "";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridViewImagenes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    e.Handled = true;
                    int index = dataGridViewImagenes.CurrentRow.Index;
                    if (index < dataGridViewImagenes.Rows.Count - 1)
                    {
                        dataGridViewImagenes.Rows.RemoveAt(index);
                        List<HC_EMERGENCIA_IMAGENES> listImag = new List<HC_EMERGENCIA_IMAGENES>();
                        listImag = NegHcEmergenciaImagenes.RecuperarHcEmergenciaImagenes(modificadaEmergencia.COD_EMERGENCIA);
                        if (listImag.Count > 0)
                        {
                            HC_EMERGENCIA_IMAGENES imagen = new HC_EMERGENCIA_IMAGENES();
                            imagen = listImag.ElementAt(index);
                            NegHcEmergenciaImagenes.EliminarHcEmergenciaImagen(imagen);
                        }
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        if (dataGridViewImagenes.Rows.Count == 0)
                        {
                            cargarImagen(true);
                        }
                        else
                        {
                            int columna = dataGridViewImagenes.SelectedCells[0].ColumnIndex;
                            int fila = dataGridViewImagenes.SelectedCells[0].RowIndex;
                            string nombreCat = dataGridViewImagenes.Rows[fila].Cells[0].Value.ToString();
                            if (columna == 0)
                            {
                                if (nombreCat == "")
                                {
                                    cargarImagen(true);
                                    dataGridViewImagenes.Rows.RemoveAt(dataGridViewImagenes.Rows.Count - 2);
                                }
                                else
                                {
                                    imagen = new HC_CATALOGOS();
                                    frm_Catalogo form = new frm_Catalogo(2);
                                    form.ShowDialog();
                                    imagen = form.catalogoFinal;
                                    if (imagen != null)
                                    {
                                        dataGridViewImagenes.Rows[fila].Cells[0].Value = imagen.HCC_NOMBRE;
                                        dataGridViewImagenes.Rows[fila].Cells[1].Value = "";
                                        dataGridViewImagenes.Rows[fila].Cells[2].Value = "";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewInterconsultas_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    e.Handled = true;
                    int index = dataGridViewInterconsultas.CurrentRow.Index;
                    if (index < dataGridViewInterconsultas.Rows.Count - 1)
                    {
                        dataGridViewInterconsultas.Rows.RemoveAt(index);
                        List<HC_EMERGENCIA_EVALUACION> listEvol = new List<HC_EMERGENCIA_EVALUACION>();
                        listEvol = NegHcEmergenciaEvaluacion.RecuperarHcEmergenciaEvaluacion(modificadaEmergencia.COD_EMERGENCIA);
                        if (listEvol.Count > 0)
                        {
                            HC_EMERGENCIA_EVALUACION evoluc = new HC_EMERGENCIA_EVALUACION();
                            evoluc = listEvol.ElementAt(index);
                            NegHcEmergenciaEvaluacion.EliminarHcEmergenciaEvaluacion(evoluc);
                        }
                    }
                }
                else 
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        if (dataGridViewInterconsultas.Rows.Count == 0)
                        {
                            cargarInterconsulta(true);
                        }
                        else 
                        {
                            int columna = dataGridViewInterconsultas.SelectedCells[0].ColumnIndex;
                            int fila = dataGridViewInterconsultas.SelectedCells[0].RowIndex;
                            string nombreCat = dataGridViewInterconsultas.Rows[fila].Cells[0].Value.ToString();
                            if (columna == 0)
                            {
                                if (nombreCat == "")
                                {
                                    cargarInterconsulta(true);
                                    dataGridViewInterconsultas.Rows.RemoveAt(dataGridViewInterconsultas.Rows.Count - 2);
                                }
                                else
                                {
                                    inter = new HC_CATALOGOS();
                                    frm_Catalogo form = new frm_Catalogo(3);
                                    form.ShowDialog();
                                    inter = form.catalogoFinal;
                                    if(inter != null)
                                    {
                                        dataGridViewInterconsultas.Rows[fila].Cells[0].Value = inter.HCC_NOMBRE;
                                        dataGridViewInterconsultas.Rows[fila].Cells[1].Value = "";
                                        dataGridViewInterconsultas.Rows[fila].Cells[2].Value = "";
                                        dataGridViewInterconsultas.Rows[fila].Cells[3].Value = "";
                                    }                                    
                                }
                            }
                            else 
                            {
                                if(columna == 1)
                                {
                                    nombreCat = dataGridViewInterconsultas.Rows[fila].Cells[0].Value.ToString();
                                    if (nombreCat != "")
                                    {
                                        dataGridViewInterconsultas.Rows[fila].Cells[1].Value = DateTime.Now;                                        
                                    }

                                }
                                if (columna == 2)
                                {
                                    nombreCat = dataGridViewInterconsultas.Rows[fila].Cells[0].Value.ToString();
                                    if (nombreCat != "")
                                    {
                                        dataGridViewInterconsultas.Rows[fila].Cells[2].Value = DateTime.Now;
                                    }

                                }
                            }                            
                        }               
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewReferido_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    e.Handled = true;
                    int index = dataGridViewReferido.CurrentRow.Index;
                    if (index < dataGridViewReferido.Rows.Count - 1)
                    {
                        dataGridViewReferido.Rows.RemoveAt(index);
                        List<HC_EMERGENCIA_REFERIDOS> listRef = new List<HC_EMERGENCIA_REFERIDOS>();
                        listRef = NegHcEmergenciaReferido.RecuperarHcEmergenciaReferencias(modificadaEmergencia.COD_EMERGENCIA);
                        if (listRef.Count > 0)
                        {
                            HC_EMERGENCIA_REFERIDOS refer = new HC_EMERGENCIA_REFERIDOS();
                            refer = listRef.ElementAt(index);
                            NegHcEmergenciaReferido.EliminarHcEmergenciaReferido(refer);
                        }
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        if (dataGridViewReferido.Rows.Count == 0)
                        {
                            cargarReferido(true);
                        }
                        else
                        {
                            int columna = dataGridViewReferido.SelectedCells[0].ColumnIndex;
                            int fila = dataGridViewReferido.SelectedCells[0].RowIndex;
                            string nombreCat = dataGridViewReferido.Rows[fila].Cells[0].Value.ToString();
                            if (columna == 0)
                            {                                
                                if (nombreCat == "")
                                {
                                    cargarReferido(true);
                                    dataGridViewReferido.Rows.RemoveAt(dataGridViewReferido.Rows.Count - 2);
                                }
                                else
                                {
                                    referido = new HC_CATALOGOS();
                                    frm_Catalogo form = new frm_Catalogo(3);
                                    form.ShowDialog();
                                    referido = form.catalogoFinal;
                                    if (referido != null)
                                    {
                                        dataGridViewReferido.Rows[fila].Cells[0].Value = referido.HCC_NOMBRE;
                                        dataGridViewReferido.Rows[fila].Cells[1].Value = "";
                                        dataGridViewReferido.Rows[fila].Cells[2].Value = "";
                                        dataGridViewReferido.Rows[fila].Cells[3].Value = "";
                                    }                                       
                                }
                            }
                            else 
                            {
                                if(columna == 1)
                                {
                                    nombreCat = dataGridViewReferido.Rows[fila].Cells[0].Value.ToString();
                                    if (nombreCat != "")
                                    {
                                        HC_CATALOGOS medico = new HC_CATALOGOS();
                                        frm_Catalogo form = new frm_Catalogo(5);
                                        form.ShowDialog();
                                        medico = form.catalogoFinal;
                                        if(medico != null)
                                        {
                                            dataGridViewReferido.Rows[fila].Cells[1].Value = medico.HCC_NOMBRE;
                                        }                                        
                                    }
                                }                         
                            }                            
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrarHistoria_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                if (!validarAlta())
                {
                    resultado = MessageBox.Show("Desea cerrar la ficha de Registro de Datos?", " ",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if ((modificadaEmergencia.EME_ESTADO == null) || (modificadaEmergencia.EME_ESTADO == false))
                        {
                            MessageBox.Show("Numero:" + modificadaEmergencia.COD_EMERGENCIA);
                            modificadaEmergencia.ATE_CODIGO =1;
                            NegHcEmergencia.cerrarHcEmergencia(modificadaEmergencia.COD_EMERGENCIA);
                            atencionActual.ATE_FECHA_ALTA = dtpFechaAlta.Value;
                            atencionActual.ATE_ESTADO = false;
                            NegAtenciones.EditarAtencionAdmision(atencionActual,0);
                            habilitarBotones(false, false, false, true, true, true, true);
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
            
        }

        private void btn_Complicaciones_Click(object sender, EventArgs e)
        {
            try
            {
                HC_CATALOGOS_TIPO tipoCodigo = NegCatalogos.RecuperarHcCatalogoTipo(16);
                frm_TipoCatalogo form = new frm_TipoCatalogo(tipoCodigo);
                form.ShowDialog();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void epicrisisToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            frm_Epicrisis form = new frm_Epicrisis();
            form.Show();

        }

        private void hoja008ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ReporteForm008 hoja008 = new ReporteForm008();
                hoja008.forIs = "HOSPITAL DE CLINICAS PICHINCHA ";
                hoja008.forUo = "EMERGENCIAS";
                hoja008.forCoduo = Convert.ToString(170101);
                hoja008.forParr = Convert.ToString(170101);
                hoja008.forCat = Convert.ToString(1701);
                hoja008.forProv = Convert.ToString(17);
                hoja008.forHisto = txt_historiaclinica.Text;  
                hoja008.forApeuno = txt_ApellidoH1.Text;   
                hoja008.forApedos = txt_ApellidoH2.Text;  
                hoja008.forNomuno = txt_NombreH1.Text;  
                hoja008.forNomdos = txt_NombreH2.Text;  
                hoja008.forCedula = pacienteActual.PAC_IDENTIFICACION;
                hoja008.forDireccion = txt_direccion.Text;
                //int codigo = pacienteActual.PAC_CODIGO;
                PACIENTES_DATOS_ADICIONALES datosAdic = new PACIENTES_DATOS_ADICIONALES();
                datosAdic = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);                
                hoja008.forBarrio = datosAdic.COD_SECTOR;
                hoja008.forParroquia = datosAdic.COD_PARROQUIA;
                hoja008.forCanton = datosAdic.COD_CANTON;
                hoja008.forProvincia = datosAdic.COD_PROVINCIA;
                hoja008.forZonau = "X";
                hoja008.forTelefono = txt_telefono1.Text;
                hoja008.forFechan = dtpFecNacimiento.Text.Trim() != string.Empty ? Convert.ToDateTime(dtpFecNacimiento.Text) : Convert.ToDateTime("1900/01/01");                
                hoja008.forLugnac = datosAdic.DAP_EMP_CIUDAD;
                hoja008.forNacional = pacienteActual.PAC_NACIONALIDAD ;
                hoja008.forGrupcult = cb_estadoCivil.Text;
                hoja008.forEdada = Funciones.CalcularEdad(Convert.ToDateTime(dtpFecNacimiento.Text));
                string genero = pacienteActual.PAC_GENERO ;
                if (genero.Equals("F"))
                    hoja008.forGf = "X";
                else
                    hoja008.forGm = "X";
                string estadoC = cb_estadoCivil.Text;
                if (estadoC.Equals("SOLTERO"))
                    hoja008.forEcsol = "X";
                else
                    if (estadoC.Equals("CASADO"))
                        hoja008.forEccas = "X";
                    else
                        if (estadoC.Equals("DIVORCIADO"))
                            hoja008.forEcdiv = "X";
                        else
                            if (estadoC.Equals("VIUDO"))
                                hoja008.forEcviu = "X";
                            else
                                if (estadoC.Equals("UNION LIBRE"))
                                    hoja008.forEcul = "X";

                hoja008.forInstruc = datosAdic.DAP_INSTRUCCION;
                hoja008.forFechadm = atencionActual.ATE_FECHA_INGRESO.Value;
                //hoja008.forFechadm = Convert.ToDateTime(dtFecIngreso.Text);
                hoja008.forOcupacion = datosAdic.DAP_OCUPACION;
                hoja008.forEmpresat = datosAdic.DAP_EMP_NOMBRE;
                hoja008.forSeguro = Convert.ToString(datosAdic.EMP_CODIGO);
                List<TIPO_REFERIDO> tipoReferido = new List<TIPO_REFERIDO>();
                hoja008.forReferido = "     ";                   
                hoja008.forAvisara = pacienteActual.PAC_REFERENTE_NOMBRE != null ? pacienteActual.PAC_REFERENTE_NOMBRE.Trim() : "";
                hoja008.forParentesco = pacienteActual.PAC_REFERENTE_PARENTESCO != null ? pacienteActual.PAC_REFERENTE_PARENTESCO.Trim() : "";
                hoja008.forPardirec = pacienteActual.PAC_REFERENTE_DIRECCION!=null?pacienteActual.PAC_REFERENTE_DIRECCION.Trim():"";
                hoja008.forPartelefono = Convert.ToString(pacienteActual.PAC_REFERENTE_TELEFONO);
                if (txtFormaLlegada.Text.Trim().Equals("AMBULATORIO"))
                    hoja008.forAmbulatoria = "X";
                else
                    if (txtFormaLlegada.Text.Trim().Equals("AMBULANCIA"))
                        hoja008.forAmbulancia = "X";
                    else
                        if (txtFormaLlegada.Text.Trim().Equals("OTROS") || txtFormaLlegada.Text.Trim().Equals("TRANSFERENCIA"))
                            hoja008.forOtrotrans = "X";                        
                hoja008.forFinf = "0";
                hoja008.forInstperso = "0";
                hoja008.forInsttelefono = ("000000");
                hoja008.forIamHora = Convert.ToDateTime(dtFecIngreso.Text);                  
                GRUPO_SANGUINEO grupoS = NegGrupoSanguineo.RecuperarGrupoSanguineoID(Convert.ToInt16(pacienteActual.GRUPO_SANGUINEOReference.EntityKey.EntityKeyValues[0].Value));                    
                if(grupoS.GS_NOMBRE.Equals("vacio"))
                    hoja008.forIamGsf = "";
                else
                    hoja008.forIamGsf = grupoS.GS_NOMBRE;                    
                hoja008.usuario = atencionActual.USUARIOS.APELLIDOS + " " + atencionActual.USUARIOS.NOMBRES ;                
                CATEGORIAS_CONVENIOS categoria = new CATEGORIAS_CONVENIOS();
                List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(atencionActual.ATE_CODIGO);
                ATENCION_DETALLE_CATEGORIAS at = new ATENCION_DETALLE_CATEGORIAS();
                if (detalleCategorias != null) 
                {
                    at = detalleCategorias[0];
                    categoria = NegCategorias.RecuperaCategoriaID(Convert.ToInt16(at.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value));
                    hoja008.forSeguro = categoria.CAT_NOMBRE;                    
                }                                
                ReportesHistoriaClinica reporte008 = new ReportesHistoriaClinica();                
                reporte008.ingresarEmergencia(hoja008);
                frmReportes ventana = new frmReportes(1, "Hoja 008");                
                ventana.Show();
                //ventana.crystalReportViewer1.RefreshReport();                  
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);  
            }
        }
        
        private void validarFecha(string Fecha, Boolean estado, string Comparacion)
        {
            int diferencia;
            DateTime fechaHoy = DateTime.Now;
            DateTime fechaParametro;
            if (Fecha == "")
            {
                estadoFecha = false;
            }
            else
            {
                try
                {
                    fechaParametro = DateTime.Parse(Fecha);
                    TimeSpan ts = fechaParametro - fechaHoy;
                    diferencia = ts.Days;
                    //Response.Write(diferencia);
                    if (Comparacion == "Valida")
                    {
                        if (diferencia <= 0)
                        {
                            estadoFecha = true;
                        }
                        else { estadoFecha = false; }
                    }

                    if (Comparacion == "Mayor")
                    {
                        if (diferencia > 0)
                        {
                            estadoFecha = true;
                        }
                        else { estadoFecha = false; }
                    }
                    if (Comparacion == "MayorIgual")
                    {
                        if (diferencia >= 0)
                        {
                            estadoFecha = true;
                        }
                        else { estadoFecha = false; }
                    }
                    if (Comparacion == "Menor")
                    {
                        if (diferencia < 0)
                        {
                            estadoFecha = true;
                        }
                        else { estadoFecha = false; }
                    }
                    if (Comparacion == "Igual")
                    {
                        if (diferencia == 0)
                        {
                            estadoFecha = true;
                        }
                        else { estadoFecha = false; }
                    }
                }
                catch
                {
                    estadoFecha = false;
                }
            }
        }

        private Boolean validarAlta()
        {
            Boolean estado = true;
            if(dtpFechaAlta.Value > dtpFechaIngreso.Value)
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

        private void ultraButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "*.*|Todos los archivos";
            archivo.ShowDialog();   
        }

        private void ultraButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "*.*|Todos los archivos";
            archivo.ShowDialog();
        }

        private void btn_CargarVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.Filter = "*.*|Todos los archivos";
            archivo.ShowDialog();
        }        

        private void cargarDatosImpresion(int codEmergencia, HC_EMERGENCIA emerg) 
        {
            string textoCie10 = "";
            for(int i = 0; i < listaCIE.Count; i++ )
            {
                CIE10 e = new CIE10();
                e = (CIE10)listaCIE.ElementAt(i);
                textoCie10 += (e.CIE_DESCRIPCION)+ "\r\n";                 
            }
            List<HC_EMERGENCIA_SS> listaSs = new List<HC_EMERGENCIA_SS>();
            listaSs = NegHcEmergenciaSubsintomas.RecuperarHcEmergenciaSubSintomas(codEmergencia);
            for (int i = 0; i < listaSs.Count; i++)
            {
                HC_EMERGENCIA_SS e = new HC_EMERGENCIA_SS();
                e = (HC_EMERGENCIA_SS)listaSs.ElementAt(i);
                textoCie10 += (e.SS_CATALOGO) + " / "+ (e.SS_DESCRIPCION) +"\r\n";
            }
            txtResumenDiag.Text = textoCie10;
            string dat ="ORL: " +emerg.EME_ORL + ", CABEZA: " +emerg.EME_CABEZA + ", CUELLO: " + emerg.EME_CUELLO + ", TORAX " + emerg.EME_TORAX +  ", CARDIACO: " +emerg.EME_CARDIACO +  ", PULMONAR: " + emerg.EME_PULMONAR +  ", ABDOMEN: " +emerg.EME_ABDOMEN +
                 ", GENITALES: " + emerg.EME_GENETALES + ", PERINE: " + emerg.EME_PERINE + ", EXTREMIDADES: " + emerg.EME_EXTREMIDADES + ", FOSAS LUMBARES: " + emerg.EME_FOSAS_LUM + ", LINFATICO: " + emerg.EME_LINFATICO + ", NEUROLOGICO: " + emerg.EME_NEUROLOGICO + ", PIEL: " + emerg.EME_PIEL + "\r\n" +
             ", TA " + emerg.EME_TA + ", TEMPERATURA: " + emerg.EME_T + ", SAT02: " + emerg.EME_SAT + ", FC: " + emerg.EME_FC + ", FR: " +  emerg.EME_FR + ", GLASGOW: " + emerg.EME_GLASGOW;
            txtProcedimientos.Text = dat;
        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void txtTA1_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txtTA2_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txtT_Leave(object sender, EventArgs e)
        {
            //calcularREMS();
        }

        private void txtSat_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txtFc_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void txtFr_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        private void cb_Glasgow_Leave(object sender, EventArgs e)
        {
            calcularREMS();
        }

        public void calcularREMS()
        {
            
            if (!(txtFc.Text == String.Empty || txtFr.Text == String.Empty ||
                txtSat.Text == String.Empty || txtTA2.Text == String.Empty ||
                txtTA1.Text == String.Empty || cb_Glasgow.SelectedIndex == 0))
            {
                int edad = Funciones.CalcularEdad(pacienteActual.PAC_FECHA_NACIMIENTO.Value);
                int fc = Convert.ToInt16(txtFc.Text);
                int fr = Convert.ToInt16(txtFr.Text);
                int tas = Convert.ToInt16(txtTA1.Text);
                int glasgow = Convert.ToInt16(cb_Glasgow.SelectedItem.ToString());
                int sato2 = Convert.ToInt16(txtSat.Text);

                int rems = 0;

                //rems edad
                if (edad < 45)
                        rems = 0;
                else if (edad>=45 && edad <= 54)
                        rems = 2;
                else if (edad>=55 && edad <= 64) 
                        rems = 3;
                else if (edad>=65 && edad <= 74) 
                        rems = 5;
                else if (edad >74)
                        rems = 6;


                //rems fc
                if (fc >= 70 && fc <= 109)
                    rems += 0;
                else if (fc >= 55 && fc <= 69)
                    rems += 2;
                else if (fc >= 40 && fc <= 54)
                    rems += 3;
                else if (fc <40)
                    rems += 4;
                else if (fc >= 110 && fc <= 139)
                    rems += 2;
                else if (fc >= 140 && fc <= 179)
                    rems += 3;
                else if (fc >179)
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
                else if (fr > 49 )
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
                if(glasgow < 5)
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
                else if (rems >= 6 && rems <=13)
                    lblRiesgo.Text = "Moderado";
                else if(rems > 13)
                    lblRiesgo.Text = "   Alto";

                uLblRems.Text = rems.ToString();
            }

            else
            {
                rShapeTriage.BackColor = Color.Transparent; 
            }
        }

        private void txtSat_TextChanged(object sender, EventArgs e)
        {

        }

        private void uLblRems_Click(object sender, EventArgs e)
        {

        }

        private void txtTA1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRegistro_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtFecIngreso_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void dtpFecNacimiento_TextChanged(object sender, EventArgs e)
        {
            if((dtpFecNacimiento.Text).Trim()!=string.Empty)
                txtEdad.Text = Funciones.CalcularEdad(Convert.ToDateTime(dtpFecNacimiento.Text)).ToString() + " años";  
        }

        private void btnImprimir_ButtonClick(object sender, EventArgs e)
        {

        }

        private void rShapeTriage_Click(object sender, EventArgs e)
        {

        }

        private void txt_Atencion_TextChanged(object sender, EventArgs e)
        {
            //actualizarPaciente();
        }
                
    }       
}