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

namespace His.Emergencia
{
    public partial class frm_Ingreso : Form
    {
        #region Variables

        List<MEDICOS> medicos = new List<MEDICOS>();
        List<PAIS> paises = new List<PAIS>();
        List<CIUDAD> ciudad = new List<CIUDAD>();
        List<ESTADO_CIVIL> estadoCivil = new List<ESTADO_CIVIL>();
        List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        List<TIPO_TRATAMIENTO> tipoTratamiento = new List<TIPO_TRATAMIENTO>();
        List<TIPO_CIUDADANO> tipoCiudadano = new List<TIPO_CIUDADANO>();
        List<ATENCION_FORMAS_LLEGADA> formasLlegada = new List<ATENCION_FORMAS_LLEGADA>();
        List<TIPO_REFERIDO> tipoReferido = new List<TIPO_REFERIDO>();
        List<CATEGORIAS_CONVENIOS> categorias = new List<CATEGORIAS_CONVENIOS>();
        List<TIPO_FORMA_PAGO> listaTipoFormaPago = new List<TIPO_FORMA_PAGO>();
        List<PACIENTES> pacientes = new List<PACIENTES>();
        List<CLASE_LOCALIDAD> clasesLocalidad = new List<CLASE_LOCALIDAD>();
        List<TIPO_FORMA_PAGO> tipoPago = new List<TIPO_FORMA_PAGO>();
        List<TIPO_GARANTIA> tipoGarantia = new List<TIPO_GARANTIA>();
        List<TIPO_EMPRESA> tipoEmpresa = new List<TIPO_EMPRESA>();
        List<GRUPO_SANGUINEO> tipoSangre = new List<GRUPO_SANGUINEO>();
        List<ETNIA> etnias = new List<ETNIA>();
        List<TIPO_INGRESO> tipoingreso = new List<TIPO_INGRESO>();
        
        PACIENTES pacienteActual = null;
        PACIENTES_DATOS_ADICIONALES datosPacienteActual = null;
        ATENCIONES ultimaAtencion = new ATENCIONES();
        ASEGURADORAS_EMPRESAS empresaPaciente = null;
        List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = null;
        List<ATENCION_DETALLE_GARANTIAS> detalleGarantias = null;

        USUARIOS usuario = new USUARIOS();

        public bool cargaciu;
        public bool direccionNueva = false;
        public bool pacienteNuevo = false;
        public bool atencionNueva = false;
        public bool bandCategorias = false;
        public bool bandGarantias = false;

        #endregion

        #region Constructor

        public frm_Ingreso()
        {
            tipoPago = NegFormaPago.RecuperaTipoFormaPagos();
            usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            aseguradoras = NegAseguradoras.ListaEmpresas();
            categorias = NegCategorias.ListaCategorias();
            tipoTratamiento = NegTipoTratamiento.RecuperaTipoTratamiento();
            formasLlegada = NegAtencionesFormasLlegada.listaAtencionesFormasLlegada();
            estadoCivil = NegEstadoCivil.RecuperaEstadoCivil();
            paises = NegPais.RecuperaPaises().OrderBy(cod => cod.NOMPAIS).ToList();
            ciudad = NegCiudad.ListaCiudades();
            tipoCiudadano = NegTipoCiudadano.listaTiposCiudadano();
            medicos = NegMedicos.listaMedicos();
            listaTipoFormaPago = NegFormaPago.RecuperaTipoFormaPagos();
            tipoReferido = NegTipoReferido.listaTipoReferido();
            tipoGarantia = NegTipoGarantia.listaTipoGarantia();
            tipoEmpresa = NegAseguradoras.ListaTiposEmpresa();
            tipoingreso = NegTipoIngreso.ListaTipoIngreso();
            tipoSangre = NegGrupoSanguineo.ListaGrupoSanguineo();
            etnias = NegEtnias.ListaEtnias();
            InitializeComponent();
            CargarDatos();
        }

        #endregion

        #region Cargar Informacion

        private void CargarDatos()
        {
            try
            {
                btnNew.Image = Archivo.btnNuevo16;
                btnActualizar.Image = Archivo.btnEditar16;
                btnGuardar.Image = Archivo.btnGuardar16;
                btnCancelar.Image = Archivo.btnCancel16;
                btnSalir.Image = Archivo.imgBtnSalir32;

                cb_seguros.DataSource = new BindingSource(categorias, null);
                cb_seguros.DisplayMember = "CAT_NOMBRE";
                cb_seguros.ValueMember = "CAT_CODIGO";

                cmb_formaLlegada.DataSource = new BindingSource(formasLlegada, null);
                cmb_formaLlegada.ValueMember = "AFL_CODIGO";
                cmb_formaLlegada.DisplayMember = "AFL_DESCRIPCION";

                cmb_estadocivil.DataSource = new BindingSource(estadoCivil, null);
                cmb_estadocivil.ValueMember = "ESC_CODIGO";
                cmb_estadocivil.DisplayMember = "ESC_NOMBRE";

                cmb_admision.DataSource = new BindingSource(tipoingreso, null);
                cmb_admision.ValueMember = "TIP_CODIGO";
                cmb_admision.DisplayMember = "TIP_DESCRIPCION";


                cargaciu = false;

                cmb_pais.DataSource = new BindingSource(paises, null);
                cmb_pais.ValueMember = "CODPAIS";
                cmb_pais.DisplayMember = "NOMPAIS";
                cargaciu = true;

                cmb_ciudadano.DataSource = new BindingSource(tipoCiudadano, null);
                cmb_ciudadano.ValueMember = "TC_CODIGO";
                cmb_ciudadano.DisplayMember = "TC_DESCRIPCION";

                cb_gruposanguineo.DataSource = new BindingSource(tipoSangre, null);
                cb_gruposanguineo.ValueMember = "GS_CODIGO";
                cb_gruposanguineo.DisplayMember = "GS_NOMBRE";

                cb_etnia.DataSource = new BindingSource(etnias, null);
                cb_etnia.ValueMember = "E_CODIGO";
                cb_etnia.DisplayMember = "E_NOMBRE";

                cmb_medicos.DataSource = new BindingSource(medicos.Select(m => new { m.MED_CODIGO, MED_NOMBRE = m.MED_APELLIDO_PATERNO + " " + m.MED_NOMBRE1 }).ToList(), null);
                cmb_medicos.ValueMember = "MED_CODIGO";
                cmb_medicos.DisplayMember = "MED_NOMBRE";

                txt_busqCi.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        public void CargarPaciente(string historia)
        {
            erroresPaciente.Clear();
            if (historia != string.Empty)
                pacienteActual = NegPacientes.RecuperarPacienteID(historia);
            else
                pacienteActual = null;

            if (pacienteActual != null)
            {
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnNewAtencion.Enabled = true;

                btnNuevo.Enabled = false;
                btnActualizar.Enabled = false;

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

                //CIUDAD city = ciudad.FirstOrDefault(c => c.EntityKey == pacienteActual.CIUDADReference.EntityKey);
                //cmb_pais.SelectedItem = paises.FirstOrDefault(p => p.CODPAIS == city.PAIS.CODPAIS);
                //cmb_ciudad.SelectedItem = ciudad.FirstOrDefault(c => c.CODCIUDAD == city.CODCIUDAD);

                txt_cedula.Text = pacienteActual.PAC_IDENTIFICACION;
                txt_fecnac.Value = pacienteActual.PAC_FECHA_NACIMIENTO.Value.Date;
                txt_feCreacion.Value = Convert.ToDateTime(pacienteActual.PAC_FECHA_CREACION).Date;
                txt_nacionalidad.Text = pacienteActual.PAC_NACIONALIDAD;
                cb_etnia.SelectedItem = etnias.FirstOrDefault(e => e.EntityKey == pacienteActual.ETNIAReference.EntityKey);
                cb_gruposanguineo.SelectedItem = tipoSangre.FirstOrDefault(g => g.EntityKey == pacienteActual.GRUPO_SANGUINEOReference.EntityKey);
                txt_email.Text = pacienteActual.PAC_EMAIL;

                if (pacienteActual.PAC_GENERO == "M")
                    rbn_h.Checked = true;
                else
                    rbn_m.Checked = true;

                CargarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);
                CargarUltimaAtencion(pacienteActual.PAC_CODIGO);

                habilitarCamposPaciente();
                habilitarCamposDireccion();

            }
            else
            {
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnNewAtencion.Enabled = false;
                btnNuevo.Enabled = true;

                panelBotonesDir.Visible = false;

                limpiarCamposPaciente();
                limpiarCamposDireccion();
                limpiarCamposAtencion();
                deshabilitarCamposPaciente();
                deshabilitarCamposDireccion();
                deshabilitarCamposAtencion();

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

        private void CargarUltimaAtencion(int keyPaciente)
        {
            ultimaAtencion = NegAtenciones.RecuperarUltimaAtencionEmergencia(keyPaciente);

            if (ultimaAtencion != null)
            {
                //txt_numeroatencion.Text = ultimaAtencion.ATE_NUMERO_ATENCION;
                cmb_formaLlegada.SelectedItem = formasLlegada.FirstOrDefault(f => f.EntityKey == ultimaAtencion.ATENCION_FORMAS_LLEGADAReference.EntityKey);
                cmb_medicos.SelectedItem = medicos.FirstOrDefault(m => m.EntityKey == ultimaAtencion.MEDICOSReference.EntityKey);
                
                cmb_admision.SelectedItem = tipoingreso.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_INGRESOReference.EntityKey);
                //txt_habitacion.Text = hab.hab_Numero;

                //cboFiltroTipoFormaPago.SelectedItem = tipoPago.FirstOrDefault(f => f.TIF_CODIGO == ultimaAtencion.TIF_CODIGO);

                txt_diagnostico.Text = ultimaAtencion.ATE_DIAGNOSTICO_INICIAL;
                txt_observaciones.Text = ultimaAtencion.ATE_OBSERVACIONES;
                txt_nombreAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                txt_cedulaAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
                txt_parentescoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_PARENTESCO;
                txt_telefonoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO;
                txt_direccionAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION;
                txt_ciudadAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CIUDAD;
                //txt_nombreGar.Text = ultimaAtencion.ATE_GARANTE_NOMBRE;
                //txt_cedulaGar.Text = ultimaAtencion.ATE_GARANTE_CEDULA;
                //txt_parentGar.Text = ultimaAtencion.ATE_GARANTE_PARENTESCO;
                //txt_montoGar.Text = ultimaAtencion.ATE_GARANTE_MONTO_GARANTIA.ToString();
                //txt_telfGar.Text = ultimaAtencion.ATE_GARANTE_TELEFONO;
                //txt_dirGar.Text = ultimaAtencion.ATE_GARANTE_DIRECCION;
                //txt_ciudadGar.Text = ultimaAtencion.ATE_GARANTE_CIUDAD;
                txt_refDe.Text = ultimaAtencion.ATE_REFERIDO_DE;

                if (ultimaAtencion.ATE_FECHA_INGRESO != null)
                    dateTimeFecIngreso.Value = ultimaAtencion.ATE_FECHA_INGRESO.Value;
                else
                    dateTimeFecIngreso.Value = DateTime.Now;
                txt_numeroatencion.Text = Convert.ToString(ultimaAtencion.ATE_CODIGO);
            }
            else
            {
                txt_numeroatencion.Text = string.Empty;
            }
        }

        public void CargarAtencion(string numAtencion)
        {
            if (numAtencion != string.Empty)
                ultimaAtencion = NegAtenciones.RecuperarAtencionPorNumero(numAtencion);
            else
                ultimaAtencion = null;

            if (ultimaAtencion != null)
            {
                //MessageBox.Show("se cargo");
                txt_numeroatencion.Text = ultimaAtencion.ATE_NUMERO_ATENCION;


                //cmb_tipoatencion.SelectedItem = tipoTratamiento.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey);
                //cmb_tiporeferido.SelectedItem = tipoReferido.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_REFERIDOReference.EntityKey);
                cmb_formaLlegada.SelectedItem = formasLlegada.FirstOrDefault(f => f.EntityKey == ultimaAtencion.ATENCION_FORMAS_LLEGADAReference.EntityKey);
                cmb_medicos.SelectedItem = medicos.FirstOrDefault(m => m.EntityKey == ultimaAtencion.MEDICOSReference.EntityKey);
                cmb_admision.SelectedItem = tipoingreso.FirstOrDefault(t => t.EntityKey == ultimaAtencion.TIPO_INGRESOReference.EntityKey);
                //txt_habitacion.Text = hab.hab_Numero;

                //cboFiltroTipoFormaPago.SelectedItem = tipoPago.FirstOrDefault(f => f.TIF_CODIGO == ultimaAtencion.TIF_CODIGO);

                txt_diagnostico.Text = ultimaAtencion.ATE_DIAGNOSTICO_INICIAL;
                txt_observaciones.Text = ultimaAtencion.ATE_OBSERVACIONES;
                txt_nombreAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                txt_cedulaAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
                txt_parentescoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_PARENTESCO;
                txt_telefonoAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO;
                txt_direccionAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION;
                txt_ciudadAcomp.Text = ultimaAtencion.ATE_ACOMPANANTE_CIUDAD;
                //txt_nombreGar.Text = ultimaAtencion.ATE_GARANTE_NOMBRE;
                //txt_cedulaGar.Text = ultimaAtencion.ATE_GARANTE_CEDULA;
                //txt_parentGar.Text = ultimaAtencion.ATE_GARANTE_PARENTESCO;
                //txt_montoGar.Text = ultimaAtencion.ATE_GARANTE_MONTO_GARANTIA.ToString();
                //txt_telfGar.Text = ultimaAtencion.ATE_GARANTE_TELEFONO;
                //txt_dirGar.Text = ultimaAtencion.ATE_GARANTE_DIRECCION;
                //txt_ciudadGar.Text = ultimaAtencion.ATE_GARANTE_CIUDAD;
                txt_refDe.Text = ultimaAtencion.ATE_REFERIDO_DE;

                if (ultimaAtencion.ATE_FECHA_INGRESO != null)
                    dateTimeFecIngreso.Value = ultimaAtencion.ATE_FECHA_INGRESO.Value;
                else
                    dateTimeFecIngreso.Value = DateTime.Now;

                //txt_obPago.Text = ultimaAtencion.TIF_OBSERVACION;

                //cb_personaFactura.SelectedItem = ultimaAtencion.ATE_FACTURA_NOMBRE;

                //detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(ultimaAtencion.ATE_CODIGO);
                //if (detalleCategorias != null)
                //{
                //    gridAseguradoras.Rows.Clear();
                //    foreach (ATENCION_DETALLE_CATEGORIAS detalle in detalleCategorias)
                //    {
                //        DataGridViewRow dt = new DataGridViewRow();
                //        dt.CreateCells(gridAseguradoras);
                //        CATEGORIAS_CONVENIOS cat = categorias.FirstOrDefault(c => c.EntityKey == detalle.CATEGORIAS_CONVENIOSReference.EntityKey);

                //        dt.Cells[0].Value = cat.CAT_CODIGO.ToString();
                //        dt.Cells[1].Value = cat.CAT_NOMBRE.ToString();
                //        dt.Cells[2].Value = detalle.ADA_AUTORIZACION.ToString();
                //        dt.Cells[3].Value = detalle.ADA_CONTRATO.ToString();
                //        dt.Cells[4].Value = detalle.ADA_MONTO_COBERTURA.ToString();

                //        gridAseguradoras.Rows.Add(dt);
                //    }
                //}
                //else
                //{
                //    gridAseguradoras.Rows.Clear();
                //}

                //detalleGarantias = NegAtencionDetalleGarantias.RecuperarDetalleGarantiasAtencion(ultimaAtencion.ATE_CODIGO);
                //if (detalleGarantias != null)
                //{
                //    gridGarantias.Rows.Clear();
                //    foreach (ATENCION_DETALLE_GARANTIAS detalle in detalleGarantias)
                //    {
                //        DataGridViewRow dt = new DataGridViewRow();
                //        dt.CreateCells(gridGarantias);
                //        TIPO_GARANTIA cat = tipoGarantia.FirstOrDefault(c => c.EntityKey == detalle.TIPO_GARANTIAReference.EntityKey);

                //        dt.Cells[0].Value = cat.TG_CODIGO.ToString();
                //        dt.Cells[1].Value = cat.TG_NOMBRE.ToString();
                //        dt.Cells[2].Value = detalle.ADG_DESCRIPCION;
                //        dt.Cells[3].Value = detalle.ADG_DOCUMENTO;
                //        dt.Cells[4].Value = detalle.ADG_VALOR.ToString();
                //        dt.Cells[5].Value = detalle.ADG_FECHA.ToString();

                //        gridGarantias.Rows.Add(dt);
                //    }
                //}
                //else
                //{
                //    gridGarantias.Rows.Clear();
                //}

                if (ultimaAtencion.ATE_FECHA_ALTA == null)
                    habilitarCamposAtencion();
                else
                    deshabilitarCamposAtencion();


                //btnFormularios.Enabled = true;
            }
            else
            {
                limpiarCamposAtencion();
                txt_numeroatencion.Enabled = true;
                deshabilitarCamposAtencion();
                detalleCategorias = null;
                detalleGarantias = null;
                //btnFormularios.Enabled = false;
            }
        }

        #endregion

        #region Eventos sobre la BDD

        private void guardarDatos()
        {
            try
            {
                FtpClient ftp = new FtpClient();
                ftp.Login();
                //ftp.ChangeDir(AdmisionParametros.getDirectorioPacientesFormularios());
                string dirPac = txt_historiaclinica.Text.ToString().Trim();
                string dirAte = txt_numeroatencion.Text.ToString().Trim();

                if (pacienteNuevo == true)
                {
                    //INGRESO PACIENTE

                    pacienteActual = new PACIENTES();
                    datosPacienteActual = new PACIENTES_DATOS_ADICIONALES();
                    ultimaAtencion = new ATENCIONES();


                    pacienteActual.PAC_CODIGO = NegPacientes.ultimoCodigoPacientes() + 1;

                    NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();

                    if (NegNumeroControl.NumerodeControlAutomatico(6))
                        numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 6).FirstOrDefault();

                    pacienteActual.PAC_HISTORIA_CLINICA = numerocontrol.NUMCON;
                    pacienteActual.PAC_FECHA_CREACION = DateTime.Now;

                    agregarDatosPaciente();

                    ftp.MakeDir(dirPac);
                    pacienteActual.PAC_DIRECTORIO = ftp.RemotePath + dirPac;

                    NegPacientes.crearPaciente(pacienteActual);
                    NegNumeroControl.LiberaNumeroControl(6);

                    //INGRESO DATOS ADICIONALES

                    datosPacienteActual.DAP_CODIGO = NegPacienteDatosAdicionales.ultimoCodigoDatos() + 1;
                    datosPacienteActual.DAP_ESTADO = true;
                    datosPacienteActual.DAP_FECHA_INGRESO = DateTime.Now;

                    agregarDatosAdicionalesPaciente();

                    NegPacienteDatosAdicionales.CrearPacienteDatosAdicionales(datosPacienteActual, pacienteActual.PAC_CODIGO);

                    //INGRESO ATENCION

                    ultimaAtencion.ATE_CODIGO = NegAtenciones.UltimoCodigoAtenciones() + 1;
                    ultimaAtencion.ATE_ESTADO = true;
                    ultimaAtencion.ATE_FECHA = DateTime.Now;

                

                    if (NegNumeroControl.NumerodeControlAutomatico(8))
                        numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();

                    ultimaAtencion.ATE_NUMERO_ATENCION = numerocontrol.NUMCON;
                    

                    
                    agregarDatosAtencion();

                    ftp.ChangeDir(dirPac);

                    ftp.MakeDir(dirAte);
                    ultimaAtencion.ATE_DIRECTORIO = ftp.RemotePath + dirAte;

                    NegAtenciones.CrearAtencion(ultimaAtencion);
                    NegNumeroControl.LiberaNumeroControl(8);
                    //agregarDetalleCategorias();
                    //agregarDetalleGarantias();

                }
                else
                {
                    //EDITAR PACIENTE

                    agregarDatosPaciente();

                    if (pacienteActual.PAC_DIRECTORIO == null || pacienteActual.PAC_DIRECTORIO == string.Empty)
                    {
                        ftp.MakeDir(dirPac);
                        pacienteActual.PAC_DIRECTORIO = ftp.RemotePath + dirPac;
                    }
                    NegPacientes.EditarPaciente(pacienteActual);

                    if (direccionNueva == true)
                    {
                        //INGRESO DATOS
                        datosPacienteActual = new PACIENTES_DATOS_ADICIONALES();
                        datosPacienteActual.DAP_CODIGO = NegPacienteDatosAdicionales.ultimoCodigoDatos() + 1;
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

                    if (txt_numeroatencion.Text != string.Empty)
                    {
                        if (ultimaAtencion == null || atencionNueva == true)
                        {
                            ultimaAtencion = new ATENCIONES();
                            ultimaAtencion.ATE_CODIGO = NegAtenciones.UltimoCodigoAtenciones() + 1;
                            ultimaAtencion.ATE_ESTADO = true;
                            
                            ultimaAtencion.ATE_FECHA = DateTime.Now;

                            NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
                            if (NegNumeroControl.NumerodeControlAutomatico(8))
                                numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
                            ultimaAtencion.ATE_NUMERO_ATENCION = numerocontrol.NUMCON;

                            agregarDatosAtencion();

                            ftp.ChangeDir(dirPac);
                            ftp.MakeDir(dirAte);
                            ultimaAtencion.ATE_DIRECTORIO = ftp.RemotePath + dirAte;

                            NegAtenciones.CrearAtencion(ultimaAtencion);
                            NegNumeroControl.LiberaNumeroControl(8);

                        }
                        else
                        {
                            agregarDatosAtencion();

                            if (ultimaAtencion.ATE_DIRECTORIO == null || ultimaAtencion.ATE_DIRECTORIO == string.Empty)
                            {
                                ftp.ChangeDir(dirPac);
                                ftp.MakeDir(dirAte);
                                ultimaAtencion.ATE_DIRECTORIO = ftp.RemotePath + dirAte;
                            }

                            NegAtenciones.EditarAtencionAdmision(ultimaAtencion,0);

                            if (detalleCategorias != null)
                                NegAtencionDetalleCategorias.eliminarDetalleCategorias(ultimaAtencion.ATE_CODIGO);

                            if (detalleGarantias != null)
                                NegAtencionDetalleGarantias.eliminarDetalleGarantias(ultimaAtencion.ATE_CODIGO);
                        }

                        //if (gridAseguradoras.RowCount > 0)
                        //    agregarDetalleCategorias();

                        //if (gridGarantias.RowCount > 0)
                        //    agregarDetalleGarantias();
                    }

                }
                ftp.Close();

            }
            catch (Exception e)
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

            pacienteActual.PAC_FECHA_NACIMIENTO = txt_fecnac.Value;

            int codCiudad = Convert.ToInt16(cmb_ciudad.SelectedValue);
            //pacienteActual.CIUDADReference.EntityKey = ciudad.FirstOrDefault(c => c.CODCIUDAD == codCiudad).EntityKey;

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
            

            ultimaAtencion.ATE_EDAD_PACIENTE = Convert.ToInt16(DateTime.Now.Year - Convert.ToDateTime(pacienteActual.PAC_FECHA_CREACION).Year);
            ultimaAtencion.ATE_ACOMPANANTE_CEDULA = txt_cedulaAcomp.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_CIUDAD = txt_ciudadAcomp.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_DIRECCION = txt_direccionRef.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_NOMBRE = txt_nombreAcomp.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_PARENTESCO = txt_parentescoRef.Text.ToString();
            ultimaAtencion.ATE_ACOMPANANTE_TELEFONO = txt_telefonoRef.Text.Replace("-", string.Empty).ToString();
            ultimaAtencion.ATE_DIAGNOSTICO_INICIAL = txt_diagnostico.Text.ToString();
            //ultimaAtencion.ATE_GARANTE_CEDULA = txt_cedulaGar.Text.ToString();
            //ultimaAtencion.ATE_GARANTE_CIUDAD = txt_ciudadGar.Text.ToString();
            //ultimaAtencion.ATE_GARANTE_DIRECCION = txt_dirGar.Text.ToString();
            ultimaAtencion.ATE_REFERIDO_DE = txt_refDe.Text.ToString();

            //if (txt_montoGar.Text != string.Empty)
            //    ultimaAtencion.ATE_GARANTE_MONTO_GARANTIA = Convert.ToDecimal(txt_montoGar.Text.ToString());

            //ultimaAtencion.ATE_GARANTE_NOMBRE = txt_nombreGar.Text.ToString();
            //ultimaAtencion.ATE_GARANTE_PARENTESCO = txt_parentGar.Text.ToString();
            //ultimaAtencion.ATE_GARANTE_TELEFONO = txt_telfGar.Text.Replace("-", string.Empty).ToString();
            ultimaAtencion.ATE_OBSERVACIONES = txt_observaciones.Text.ToString();

            //int codTipoReferido = Convert.ToInt16(cmb_tiporeferido.SelectedValue);
            //ultimaAtencion.TIPO_REFERIDOReference.EntityKey = tipoReferido.FirstOrDefault(t => t.TIR_CODIGO == codTipoReferido).EntityKey;

            //if (codTipoReferido == 1)
            //    ultimaAtencion.ATE_REFERIDO = true;
            //else
            //    ultimaAtencion.ATE_REFERIDO = false;

            
            

            int codFormaLlegada = Convert.ToInt16(cmb_formaLlegada.SelectedValue);
            ultimaAtencion.ATENCION_FORMAS_LLEGADAReference.EntityKey = formasLlegada.FirstOrDefault(f => f.AFL_CODIGO == codFormaLlegada).EntityKey;

            int codMedico = Convert.ToInt16(cmb_medicos.SelectedValue);
            ultimaAtencion.MEDICOSReference.EntityKey = medicos.FirstOrDefault(m => m.MED_CODIGO == codMedico).EntityKey;

            //DtoCajas dtoCaja = NegCajas.RecuperaCajas().FirstOrDefault(c => c.LOC_CODIGO == His.Entidades.Clases.Sesion.codLocal);
            //CAJAS caja = NegCajas.ListaCajas().FirstOrDefault(c => c.CAJ_CODIGO == dtoCaja.CAJ_CODIGO);   
            //nuevaAtencion.CAJASReference.EntityKey = caja.EntityKey;

            //ultimaAtencion.TIF_CODIGO = (Int16)cboFiltroTipoFormaPago.SelectedValue;
            ultimaAtencion.PACIENTESReference.EntityKey = pacienteActual.EntityKey;
            ultimaAtencion.PACIENTES_DATOS_ADICIONALESReference.EntityKey = datosPacienteActual.EntityKey;

            //int codTipoAtencion = Convert.ToInt16(cmb_tipoatencion.SelectedValue);
            //ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey = tipoTratamiento.FirstOrDefault(t => t.TIA_CODIGO == codTipoAtencion).EntityKey;
            ultimaAtencion.USUARIOSReference.EntityKey = usuario.EntityKey;
            //ultimaAtencion.ATE_FACTURA_NOMBRE = cb_personaFactura.SelectedItem.ToString();
            //ultimaAtencion.TIF_OBSERVACION = txt_obPago.Text.ToString();

            ultimaAtencion.TIPO_INGRESOReference.EntityKey = NegTipoIngreso.FiltrarPorId(Convert.ToInt16(cmb_admision.SelectedValue)).EntityKey;
            //ultimaAtencion.TIP_CODIGO = Convert.ToInt16(cmb_admision.SelectedValue);

            //ultimaAtencion.HABITACIONESReference.EntityKey = NegHabitaciones.RecuperarHabitacionId(999).EntityKey;
        }

        //private void agregarDetalleCategorias()
        //{
        //    try
        //    {
        //        bool fechainicio = false;
        //        for (int i = 0; i < gridAseguradoras.Rows.Count; i++)
        //        {
        //            ATENCION_DETALLE_CATEGORIAS nuevoDetalle = new ATENCION_DETALLE_CATEGORIAS();
        //            nuevoDetalle.ADA_CODIGO = NegAtencionDetalleCategorias.ultimoCodigoDetalleCategorias() + 1;
        //            nuevoDetalle.ATENCIONESReference.EntityKey = ultimaAtencion.EntityKey;
        //            int codCategoria = Convert.ToInt16(gridAseguradoras.Rows[i].Cells["codCategoria"].Value.ToString());
        //            nuevoDetalle.CATEGORIAS_CONVENIOSReference.EntityKey = categorias.FirstOrDefault(c => c.CAT_CODIGO == codCategoria).EntityKey;
        //            nuevoDetalle.ADA_AUTORIZACION = gridAseguradoras.Rows[i].Cells["autorizacion"].Value.ToString();
        //            nuevoDetalle.ADA_CONTRATO = gridAseguradoras.Rows[i].Cells["Contrato"].Value.ToString();
        //            nuevoDetalle.ADA_MONTO_COBERTURA = Convert.ToDecimal(gridAseguradoras.Rows[i].Cells["Monto"].Value.ToString());
        //            nuevoDetalle.ADA_ORDEN = i + 1;
        //            nuevoDetalle.ADA_ESTADO = false;

        //            if (fechainicio == false)
        //            {
        //                nuevoDetalle.ADA_ESTADO = true;
        //                nuevoDetalle.ADA_FECHA_INICIO = DateTime.Today;
        //                fechainicio = true;
        //            }

        //            NegAtencionDetalleCategorias.CrearDetalleCategorias(nuevoDetalle);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Error en el ingreso de convenios: \n" + e.Message);
        //        if (e.InnerException != null)
        //            MessageBox.Show("Error en el ingreso de convenios: \n" + e.InnerException.Message);
        //    }
        //}

        //private void agregarDetalleGarantias()
        //{
        //    try
        //    {
        //        for (int i = 0; i < gridGarantias.Rows.Count; i++)
        //        {
        //            ATENCION_DETALLE_GARANTIAS nuevoDetalle = new ATENCION_DETALLE_GARANTIAS();
        //            nuevoDetalle.ADG_CODIGO = NegAtencionDetalleGarantias.ultimoCodigoDetalleGarantias() + 1;
        //            nuevoDetalle.ATENCIONESReference.EntityKey = ultimaAtencion.EntityKey;
        //            int codGarantia = Convert.ToInt16(gridGarantias.Rows[i].Cells["codGarantia"].Value.ToString());
        //            nuevoDetalle.TIPO_GARANTIAReference.EntityKey = tipoGarantia.FirstOrDefault(c => c.TG_CODIGO == codGarantia).EntityKey;
        //            nuevoDetalle.ADG_DESCRIPCION = gridGarantias.Rows[i].Cells["descripcion"].Value.ToString();

        //            if (gridGarantias.Rows[i].Cells["numdocumento"].Value != null)
        //                nuevoDetalle.ADG_DOCUMENTO = gridGarantias.Rows[i].Cells["numdocumento"].Value.ToString();

        //            nuevoDetalle.ADG_VALOR = Convert.ToDecimal(gridGarantias.Rows[i].Cells["valorGar"].Value.ToString());
        //            nuevoDetalle.ADG_ESTADO = true;
        //            nuevoDetalle.ADG_FECHA = Convert.ToDateTime(gridGarantias.Rows[i].Cells["fecha"].Value.ToString());

        //            NegAtencionDetalleGarantias.CrearDetalleGarantias(nuevoDetalle);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //        if (e.InnerException != null)
        //            MessageBox.Show(e.InnerException.Message);
        //    }
        //}

        #endregion

        # region Filtros Busqueda Pacientes

        private void txt_busqCi_KeyDown(object sender, KeyEventArgs e)
        {
            Buscar(e);
        }

        private void txt_busqHist_KeyDown(object sender, KeyEventArgs e)
        {
            Buscar(e);
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

                    string id = txt_busqCi.Text.ToString();
                    string historia = txt_busqHist.Text.ToString();
                    string apellido = txt_busqApe.Text.ToString();
                    string nombre = txt_busqNom.Text.ToString();

                    //frm_Ayudas ayuda = new frm_Ayudas(NegPacientes.listaPacientesFiltros(id, historia, apellido, nombre),"PACIENTES","HISTORIA_CLINICA");
                    //frm_AyudaPacientes ayuda = new frm_AyudaPacientes(NegPacientes.listaPacientesFiltros(id, historia, apellido, nombre));
                    //ayuda.campoPadre = txt_historiaclinica;
                    //ayuda.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        #endregion

        # region Otros

        private void limpiarCamposPaciente()
        {
            txt_historiaclinica.Text = string.Empty;
            txt_apellido1.Text = string.Empty;
            txt_apellido2.Text = string.Empty;
            txt_nombre1.Text = string.Empty;
            txt_nombre2.Text = string.Empty;
            txt_cedula.Text = string.Empty;
            txt_fecnac.Value = DateTime.Now;
            txt_nacionalidad.Text = string.Empty;
            txt_feCreacion.Value = DateTime.Now;
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
        }

        private void limpiarCamposAtencion()
        {
            //txt_nombreAcomp.Text = string.Empty;
            txt_telefonoRef.Text = string.Empty;
            txt_direccionRef.Text = string.Empty;
            //txt_ciudadAcomp.Text = string.Empty;
            //txt_cedulaAcomp.Text = string.Empty;
            txt_parentescoRef.Text = string.Empty;
            //txt_habitacion.Text = string.Empty;
            //txt_diagnostico.Text = string.Empty;
            //txt_nombreGar.Text = string.Empty;
            //txt_telfGar.Text = string.Empty;
            //txt_dirGar.Text = string.Empty;
            //txt_ciudadGar.Text = string.Empty;
            //txt_cedulaGar.Text = string.Empty;
            //txt_parentGar.Text = string.Empty;
            //txt_montoGar.Text = string.Empty;
            //txt_observaciones.Text = string.Empty;
            txt_numeroatencion.Text = string.Empty;
            //txt_refDe.Text = string.Empty;
            //dateTimeFecIngreso.Value = DateTime.Now;
            //txt_obPago.Text = string.Empty;
            //cboFiltroTipoFormaPago.SelectedIndex = 0;
            //gridAseguradoras.Rows.Clear();
            //gridGarantias.Rows.Clear();

        }


        private void habilitarCamposPaciente()
        {
            txt_apellido1.ReadOnly = false;
            txt_apellido2.ReadOnly = false;
            txt_nombre1.ReadOnly = false;
            txt_nombre2.ReadOnly = false;
            txt_cedula.ReadOnly = false;
            txt_fecnac.Enabled = true;
            txt_nacionalidad.ReadOnly = false;
            cb_etnia.Enabled = true;
            cb_gruposanguineo.Enabled = true;
            rbPasaporte.Enabled = true;
            rbRuc.Enabled = true;
            rbCedula.Enabled = true;
            rbSid.Enabled = true;
            txt_cedula.ReadOnly = false;
            txt_fecnac.Enabled = true;
            txt_nacionalidad.ReadOnly = false;
            cb_etnia.Enabled = true;
            cb_gruposanguineo.Enabled = true;
            cmb_pais.Enabled = true;
            cmb_ciudad.Enabled = true;
            rbn_h.Enabled = true;
            rbn_m.Enabled = true;
        }

        private void deshabilitarCamposPaciente()
        {
            txt_apellido1.ReadOnly = true;
            txt_apellido2.ReadOnly = true;
            txt_nombre1.ReadOnly = true;
            txt_nombre2.ReadOnly = true;
            txt_cedula.ReadOnly = true;
            txt_fecnac.Enabled = false;
            txt_nacionalidad.ReadOnly = true;
            cb_etnia.Enabled = false;
            cb_gruposanguineo.Enabled = false;
            rbPasaporte.Enabled = false;
            rbRuc.Enabled = false;
            rbCedula.Enabled = false;
            rbSid.Enabled = false;
            txt_cedula.ReadOnly = true;
            txt_fecnac.Enabled = false;
            txt_nacionalidad.ReadOnly = true;
            cb_etnia.Enabled = false;
            cb_gruposanguineo.Enabled = false;
            cmb_pais.Enabled = false;
            cmb_ciudad.Enabled = false;
            rbn_h.Enabled = false;
            rbn_m.Enabled = false;
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
            //txt_nombreAcomp.ReadOnly = false;
            txt_telefonoRef.ReadOnly = false;
            txt_direccionRef.ReadOnly = false;
            //txt_ciudadAcomp.ReadOnly = false;
            //txt_cedulaAcomp.ReadOnly = false;
            txt_parentescoRef.ReadOnly = false;
            //cmb_tipoatencion.Enabled = true;
            //cmb_tiporeferido.Enabled = true;
            cmb_medicos.Enabled = true;
            cmb_formaLlegada.Enabled = true;
            cb_seguros.Enabled = true;
            //cboFiltroTipoFormaPago.Enabled = true;
            //cboFiltroTipoFormaPago.Enabled = true;
            //txt_diagnostico.ReadOnly = false;
            //txt_nombreGar.ReadOnly = false;
            //txt_telfGar.ReadOnly = false;
            //txt_dirGar.ReadOnly = false;
            //txt_ciudadGar.ReadOnly = false;
            //txt_cedulaGar.ReadOnly = false;
            //txt_parentGar.ReadOnly = false;
            //txt_montoGar.ReadOnly = false;
            //txt_refDe.ReadOnly = false;
            //txt_observaciones.ReadOnly = false;
            //txt_obPago.ReadOnly = false;
            //cb_personaFactura.Enabled = true;
            dateTimeFecIngreso.Enabled = true;
            cmb_admision.Enabled = true;

            //gridAseguradoras.Columns["autorizacion"].ReadOnly = false;
            //gridAseguradoras.Columns["Contrato"].ReadOnly = false;
            //gridAseguradoras.Columns["Monto"].ReadOnly = false;

            //gridGarantias.Columns["valorGar"].ReadOnly = false;
            //gridGarantias.Columns["descripcion"].ReadOnly = false;
            //gridGarantias.Columns["numdocumento"].ReadOnly = false;
            //gridGarantias.Columns["fecha"].ReadOnly = false;

            //cb_tipoGarantia.Enabled = true;
            //btnAddGar.Enabled = true;
        }

        private void deshabilitarCamposAtencion()
        {
            //txt_nombreAcomp.ReadOnly = true;
            txt_telefonoRef.ReadOnly = true;
            txt_direccionRef.ReadOnly = true;
            //txt_ciudadAcomp.ReadOnly = true;
            //txt_cedulaAcomp.ReadOnly = true;
            txt_parentescoRef.ReadOnly = true;
            //cmb_tipoatencion.Enabled = false;
            //cmb_tiporeferido.Enabled = false;
            cmb_medicos.Enabled = false;
            cmb_formaLlegada.Enabled = false;
            cb_seguros.Enabled = false;
            //txt_habitacion.ReadOnly = true;
            //cboFiltroTipoFormaPago.SelectedIndex = 0;
            //cboFiltroTipoFormaPago.Enabled = false;
            //txt_diagnostico.ReadOnly = true;
            //txt_nombreGar.ReadOnly = true;
            //txt_telfGar.ReadOnly = true;
            //txt_dirGar.ReadOnly = true;
            //txt_ciudadGar.ReadOnly = true;
            //txt_cedulaGar.ReadOnly = true;
            //txt_parentGar.ReadOnly = true;
            //txt_montoGar.ReadOnly = true;
            //txt_observaciones.ReadOnly = true;
            //txt_obPago.ReadOnly = true;
            //txt_refDe.ReadOnly = true;
            //cb_personaFactura.Enabled = false;
            dateTimeFecIngreso.Enabled = false;
            cmb_admision.Enabled = false;

            //gridAseguradoras.Columns["autorizacion"].ReadOnly = true;
            //gridAseguradoras.Columns["Contrato"].ReadOnly = true;
            //gridAseguradoras.Columns["Monto"].ReadOnly = true;

            //gridGarantias.Columns["valorGar"].ReadOnly = true;
            //gridGarantias.Columns["descripcion"].ReadOnly = true;
            //gridGarantias.Columns["numdocumento"].ReadOnly = true;
            //gridGarantias.Columns["fecha"].ReadOnly = true;

            //cb_tipoGarantia.Enabled = false;
            //btnAddGar.Enabled = false;
        }


        private bool ValidarFormulario()
        {
            erroresPaciente.Clear();

            bool valido = true;

            //CAMPOS PACIENTE

            if (txt_apellido1.Text == string.Empty)
            {
                AgregarError(txt_apellido1);
                valido = false;
            }
            if (txt_apellido2.Text == string.Empty)
            {
                AgregarError(txt_apellido2);
                valido = false;
            }
            if (txt_nombre1.Text == string.Empty)
            {
                AgregarError(txt_nombre1);
                valido = false;
            }
            if (txt_nombre2.Text == string.Empty)
            {
                AgregarError(txt_nombre2);
                valido = false;
            }
            if (txt_cedula.Text == string.Empty)
            {
                AgregarError(txt_cedula);
                valido = false;
            }
            if (txt_fecnac.Value > DateTime.Now)
            {
                AgregarError(txt_fecnac);
                valido = false;
            }
            if (txt_nacionalidad.Text == string.Empty)
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
            //if (cmb_pais.SelectedItem == null)
            //{
            //    AgregarError(cmb_pais);
            //    valido = false;
            //}else if(cmb_pais.SelectedItem==(PAIS)paises.FirstOrDefault(p=>p.CODPAIS==234))
            if (cmb_ciudad.SelectedItem == null)
            {
                AgregarError(cmb_ciudad);
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
            if (txt_telefono1.Text == "   -   -")
            {
                AgregarError(txt_telefono1);
                valido = false;
            }
            if (txt_ocupacion.Text == string.Empty)
            {
                AgregarError(txt_ocupacion);
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
            if (txt_instuccion.Text == string.Empty)
            {
                AgregarError(txt_instuccion);
                valido = false;
            }


            //CAMPOS ATENCION
            if (txt_numeroatencion.Text != string.Empty)
            {

                //if (dateTimeFecIngreso.Value == null)
                //{
                //    AgregarError(dateTimeFecIngreso);
                //    valido = false;
                //}

                //if (txt_habitacion.Text == string.Empty)
                //{
                //    AgregarError(txt_habitacion);
                //    valido = false;
                //}
                //if (txt_diagnostico.Text == string.Empty)
                //{
                //    AgregarError(txt_diagnostico);
                //    valido = false;
                //}
                //if (txt_observaciones.Text == string.Empty)
                //{
                //    AgregarError(txt_observaciones);
                //    valido = false;
                //}

                //if (cb_personaFactura.SelectedItem.ToString() == "ACOMPAÑANTE")
                //{
                //    if (txt_nombreAcomp.Text == string.Empty)
                //    {
                //        AgregarError(txt_nombreAcomp);
                //        valido = false;
                //    }
                //    if (txt_cedulaAcomp.Text == string.Empty)
                //    {
                //        AgregarError(txt_cedulaAcomp);
                //        valido = false;
                //    }
                //    if (txt_parentescoAcomp.Text == string.Empty)
                //    {
                //        AgregarError(txt_parentescoAcomp);
                //        valido = false;
                //    }
                //    if (txt_telefonoAcomp.Text == "   -   -")
                //    {
                //        AgregarError(txt_telefonoAcomp);
                //        valido = false;
                //    }
                //    if (txt_direccionAcomp.Text == string.Empty)
                //    {
                //        AgregarError(txt_direccionAcomp);
                //        valido = false;
                //    }
                //    if (txt_ciudadAcomp.Text == string.Empty)
                //    {
                //        AgregarError(txt_ciudadAcomp);
                //        valido = false;
                //    }
                //}


                //if (cb_personaFactura.SelectedItem.ToString() == "GARANTE")
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

                //if (cb_personaFactura.SelectedItem.ToString() == "EMPRESA")
                //{
                //    if (txt_rucEmp.Text == string.Empty)
                //    {
                //        AgregarError(txt_rucEmp);
                //        valido = false;
                //    }
                //}


                //if (cmb_tipoatencion.SelectedItem == null)
                //{
                //    AgregarError(cmb_tipoatencion);
                //    valido = false;
                //}
                //if (cmb_tiporeferido.SelectedItem == null)
                //{
                //    AgregarError(cmb_tiporeferido);
                //    valido = false;
                //}
                //if (cmb_medicos.SelectedItem == null)
                //{
                //    AgregarError(cmb_medicos);
                //    valido = false;
                //}
                //if (cmb_formaLlegada.SelectedItem == null)
                //{
                //    AgregarError(cmb_formaLlegada);
                //    valido = false;
                //}
            }
            return valido;
        }

        private void AgregarError(Control control)
        {
            erroresPaciente.SetError(control, "Campo Requerido");
        }

        # endregion

        #region Eventos botones del menu

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            pacienteNuevo = true;
            
            pacienteActual = null;
            datosPacienteActual = null;
            ultimaAtencion = null;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnImprimir.Enabled = false;
            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;
            btnNewAtencion.Enabled = false;

            txt_busqCi.Enabled = false;
            txt_busqHist.Enabled = false;
            txt_busqApe.Enabled = false;
            txt_busqNom.Enabled = false;

            panelBotonesDir.Visible = false;

            limpiarCamposPaciente();
            limpiarCamposDireccion();
            limpiarCamposAtencion();


            NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
            numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 6).FirstOrDefault();

            txt_historiaclinica.Text = numerocontrol.NUMCON.ToString();

            txt_numeroatencion.Enabled = false;
            numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();
            txt_numeroatencion.Text = numerocontrol.NUMCON.ToString();

            txt_feCreacion.Text = DateTime.Now.ToString();
            dateTimeFecIngreso.Value = DateTime.Now;
            habilitarCamposPaciente();
            habilitarCamposDireccion();
            habilitarCamposAtencion();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            pacienteNuevo = false;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnImprimir.Enabled = true;
            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;

            //panelBotonesDir.Visible = false;

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
            if (ValidarFormulario() == true)
            {
                guardarDatos();

                btnNuevo.Enabled = true;
                btnNewAtencion.Enabled = true;
                btnImprimir.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnActualizar.Enabled = true;
                panelBotonesDir.Visible = true;
                direccionNueva = false;
                pacienteNuevo = false;
                atencionNueva = false;

                txt_busqCi.Enabled = true;
                txt_busqHist.Enabled = true;
                txt_busqApe.Enabled = true;
                txt_busqNom.Enabled = true;

                CargarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);
                CargarAtencion(txt_numeroatencion.Text);
                deshabilitarCamposPaciente();
                deshabilitarCamposDireccion();
                deshabilitarCamposAtencion();

                MessageBox.Show("Datos Almacenados Correctamente");
            }
            else
            {
                MessageBox.Show("Informacion Incompleta");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            erroresPaciente.Clear();
            pacienteNuevo = false;
            direccionNueva = false;
            atencionNueva = false;
            txt_numeroatencion.Text = string.Empty;
            txt_historiaclinica.Text = string.Empty;

            txt_busqCi.Enabled = true;
            txt_busqHist.Enabled = true;
            txt_busqApe.Enabled = true;
            txt_busqNom.Enabled = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnNewAtencion_Click(object sender, EventArgs e)
        {
            atencionNueva = true;
            //ultimaAtencion = null;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnImprimir.Enabled = false;
            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;
            btnNewAtencion.Enabled = false;

            txt_busqCi.Enabled = false;
            txt_busqHist.Enabled = false;
            txt_busqApe.Enabled = false;
            txt_busqNom.Enabled = false;

            panelBotonesDir.Visible = false;

            limpiarCamposAtencion();

            txt_numeroatencion.Enabled = false;

            NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();
            numerocontrol = NegNumeroControl.RecuperaNumeroControl().Where(cod => cod.CODCON == 8).FirstOrDefault();

            txt_numeroatencion.Text = numerocontrol.NUMCON.ToString();
            dateTimeFecIngreso.Value = DateTime.Now;
            habilitarCamposAtencion();
        }

        #endregion

        #region campos Division Politica y eventos

        private void BuscarDivision(KeyEventArgs e, string tipo)
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

            }
            catch (Exception r)
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
                    BuscarDivision(e, "pais");
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
                        BuscarDivision(e, "provincia");
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
                        BuscarDivision(e, "canton");
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
                        BuscarDivision(e, "parroquia");
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
                        BuscarDivision(e, "sector");
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
                if (NegValidaciones.esTelefonoValido(txt_telefono2.Text.Replace("-", string.Empty).ToString()) == false)
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

        }

        private void txt_cedulaAcomp_Leave(object sender, EventArgs e)
        {

        }

        private void txt_cedula_Leave_1(object sender, EventArgs e)
        {
            if (rbCedula.Checked == true || rbRuc.Checked == true)
            {
                if (txt_cedula.Text.ToString() != string.Empty)
                    if (NegValidaciones.esCedulaValida(txt_cedula.Text) != true)
                    {
                        MessageBox.Show("Cédula Incorrecta");
                        txt_cedula.Focus();
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
                    MessageBox.Show("Cédula Incorrecta");
                    txt_rucEmp.Focus();
                }
            }
        }

        #endregion

        #region Eventos

        private void txt_historiaclinica_TextChanged(object sender, EventArgs e)
        {
            string historia = txt_historiaclinica.Text.ToString();
            if (pacienteNuevo == false)
                CargarPaciente(historia);
        }

        private void cmb_pais_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmb_pais.SelectedIndex >= 0 && cargaciu == true)
                {
                    PAIS pais = paises.FirstOrDefault(p => p.CODPAIS == Int16.Parse(cmb_pais.SelectedValue.ToString()));

                    cmb_ciudad.DataSource = null;
                    cmb_ciudad.DataSource = ciudad.Where(cod => cod.PAIS.CODPAIS == pais.CODPAIS).OrderBy(c => c.NOMCIU).ToList();
                    cmb_ciudad.ValueMember = "CODCIUDAD";
                    cmb_ciudad.DisplayMember = "NOMCIU";

                    if (pais.NACIONALIDAD != null && pais.NACIONALIDAD != string.Empty)
                        txt_nacionalidad.Text = pais.NACIONALIDAD;
                    else
                        txt_nacionalidad.Text = string.Empty;

                }
                else
                {
                    cmb_ciudad.DataSource = null;
                    //formaPago = null;
                    //cboFiltroFormaPago.SelectedIndex = 0;
                    //cboFiltroFormaPago.Enabled = false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error");
            }
        }

        private void txt_numeroatencion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    txt_numeroatencion.Text = string.Empty;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                if (x.InnerException != null)
                    MessageBox.Show(x.InnerException.Message);
            }

        }

        private void txt_numeroatencion_TextChanged(object sender, EventArgs e)
        {
            string numeroatencion = txt_numeroatencion.Text.ToString();
            if (atencionNueva == false && pacienteNuevo == false)
                CargarAtencion(numeroatencion);
        }

        #endregion

        #region Ayudas

        private void txt_rucEmp_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && btnGuardar.Enabled == true)
            {
                frm_Ayudas ayuda = new frm_Ayudas(NegAseguradoras.ListaEmpresas(), "EMPRESAS", "RUC");
                ayuda.campoEspecial = txt_rucEmp;
                ayuda.colRetorno = "RUC";
                ayuda.ShowDialog();
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
            }
        }

        #endregion

        private void txt_numeroatencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_busqCi.Focus();
            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_busqHist.Focus();
            }
        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_busqApe.Focus();
            }
        }

        private void txt_busqApe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_busqNom.Focus();
            }
        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_historiaclinica.Focus();
            }
        }

        private void txt_historiaclinica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_nombre1.Focus();
            }
        }

        private void txt_nombre1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_nombre2.Focus();
            }
        }

        private void txt_nombre2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_apellido1.Focus();
            }
        }

        private void txt_apellido1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_apellido2.Focus();
            }
        }

        private void txt_apellido2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_cedula.Focus();
            }
        }

        private void txt_cedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_feCreacion.Focus();
            }
        }

        private void txt_feCreacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_fecnac.Focus();
            }
        }

        private void txt_fecnac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_pais.Focus();
            }
        }

        private void cmb_pais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_ciudad.Focus();
            }
        }

        private void cmb_ciudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_nacionalidad.Focus();
            }
        }

        private void txt_nacionalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cb_etnia.Focus();
            }
        }

        private void cb_etnia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cb_gruposanguineo.Focus();
            }
        }

        private void cb_gruposanguineo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rbn_h.Focus();
            }
        }

        private void rbn_h_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_codPais.Focus();
            }
        }

        private void txt_codPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_codProvincia.Focus();
            }
        }

        private void txt_codProvincia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_codCanton.Focus();
            }
        }

        private void txt_codCanton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_codParroquia.Focus();
            }
        }

        private void txt_codParroquia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_codBarrio.Focus();
            }
        }

        private void txt_codBarrio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_direccion.Focus();
            }
        }

        private void txt_direccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_telefono1.Focus();
            }
        }

        private void txt_telefono1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_telefono2.Focus();
            }
        }

        private void txt_telefono2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                dateTimeFecIngreso.Focus();
            }
        }

        private void dateTimeFecIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_formaLlegada.Focus();
            }
        }

        private void cmb_formaLlegada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_medicos.Focus();
            }
        }

        private void cmb_medicos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cb_seguros.Focus();
            }
        }

        private void cb_seguros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_refDe.Focus();
            }
        }

        private void txt_refDe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_nombreRef.Focus();
            }
        }

        private void txt_nombreRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_parentescoRef.Focus();
            }
        }

        private void txt_parentescoRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_direccionRef.Focus();
            }
        }

        private void txt_direccionRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txt_telefonoRef.Focus();
            }
        }

        private void txt_telefonoRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tabulador.SelectedTab=tabulador.Tabs["adicionales"];
                txt_nombreAcomp.Focus();
            }
        }

        private void frm_Ingreso_Load(object sender, EventArgs e)
        {

        }

        private void tabulador_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

       
    }
}
