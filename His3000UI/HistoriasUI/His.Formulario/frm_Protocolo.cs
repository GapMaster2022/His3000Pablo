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
using Recursos;
using His.Parametros;
using His.DatosReportes;
using Infragistics;
using System.Data.SqlClient;
using His.Entidades.Clases;
using His.Formulario;


namespace His.Formulario
{
    public partial class frm_Protocolo : Form
    {
        #region Variables

        ATENCIONES atencion = null;
        ASEGURADORAS_EMPRESAS aseguradora = null;
        PACIENTES paciente = null;
        MEDICOS medico = null;
        Boolean nuevo = false;
        HC_PROTOCOLO_OPERATORIO nuevoProtocolo = new HC_PROTOCOLO_OPERATORIO();
        private int contlrP1 = 0;
        private int contlrP2;
        private string proced1 = "";
        private string proced2 = "";

        private int CodigoAtencion; // Almacena el Codigo de la Atencion / Giovanny Tapia
        private int CodigoProtocolo; // Almacena el Codigo del protocolo / Giovanny Tapia

        internal static string medicopatologo; //recibe el nombre del medico p.
        internal static string cie10; //recibe el procedimiento
        internal static string tarifario; //recibe el tarifario elegido
        internal static string personal; //recibe el nombre del personal
        public bool _valido = false;

        ToolTip toolTip1 = new ToolTip();
        //ATENCIONES atencion = new ATENCIONES();
        #endregion


        public frm_Protocolo()
        {
            InitializeComponent();
            CargarAnestesia();

        }

        public void CargarAnestesia()
        {
            NegQuirofano quir = new NegQuirofano();
            txtTipoAnestesia.DataSource = quir.MostrarAnestesia();
            txtTipoAnestesia.ValueMember = "CODIGO";
            txtTipoAnestesia.DisplayMember = "DESCRIPCION";
        }
        private void refrescarSolicitudes()
        {
            gridSol.DataSource = NegImagen.getProtocolos(CodigoAtencion);
            gridSol.Columns["ID"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (gridSol.RowCount > 1)
                habilitarBotones(true, false, true, true, true);
            else
                habilitarBotones(true, false, false, false, true);
        }

        public frm_Protocolo(Int32 codigoAtencion, Int32 CodigoFormulario)
        {
            InitializeComponent();
            CargarAnestesia();
            CargarEspCirugia();
            CargarHabitacionesQuirofano();
            btnNuevo.Image = Archivo.imgBtnAdd2;
            btnGuardar.Image = Archivo.imgBtnGoneSave48;
            btnImprimir.Image = Archivo.imgBtnGonePrint48;
            //btnExportarExcel.Image = Archivo.imgOfficeExcel;
            btnSalir.Image = Archivo.imgBtnGoneExit48;

            CodigoAtencion = codigoAtencion;
            CodigoProtocolo = CodigoFormulario;

            if (codigoAtencion != 0)
                cargarAtencion(codigoAtencion, CodigoFormulario);


            NegAtenciones atenciones = new NegAtenciones();
            string estado = atenciones.EstadoCuenta(Convert.ToString(codigoAtencion));
            refrescarSolicitudes();
            List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(His.Entidades.Clases.Sesion.codUsuario);
            //if (estado != "1")
            //{
            //    foreach (var item in perfilUsuario)
            //    {
            //        if (item.ID_PERFIL == 31) //validara con codigo
            //        {
            //            if (item.DESCRIPCION.Contains("HCS")) //valida contra la descripcion
            //            {
            //                _valido = true;
            //                break;
            //            }
            //        }
            //        else
            //        {
            //            if (item.DESCRIPCION.Contains("HCS")) //solo valida contra la descripcion
            //            {
            //                _valido = true;
            //                break;
            //            }
            //        }

            //    }
            //    if (!_valido)
            //        Bloquear();
            //}
            if (estado != "1")
            {
                foreach (var item in perfilUsuario)
                {
                    List<ACCESO_OPCIONES> accop = NegUtilitarios.ListaAccesoOpcionesPorPerfil(item.ID_PERFIL, 4);
                    foreach (var items in accop)
                    {
                        if (items.ID_ACCESO == 46000) // Mario Valencia 21/12/2023 // cambio en seguridades.
                        {
                            _valido = true;
                            break;
                        }
                        else
                            _valido = true;
                    }
                }
                if (!_valido)
                    Bloquear();
            }

            ValidarEnfermeria();
            BloquearTabulador(false);
            toolTip1.SetToolTip(btnOral, "Añadir tarifario");
            toolTip1.SetToolTip(btnPostOperatorio, "Añadir tarifario");
        }

        public void ValidarEnfermeria()
        {

            if (His.Entidades.Clases.Sesion.codDepartamento == 6 && !_valido)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnImprimir.Enabled = false;
            }

        }
        #region Métodos
        public void Bloquear()
        {
            btnNuevo.Enabled = false;
            //btnImprimir.Enabled = false;
            btnGuardar.Enabled = false;
            //ugbPaciente.Enabled = false;
            //ultraTabPageControl1.Enabled = false;
            //ultraTabPageControl2.Enabled = false;
            //ultraTabPageControl3.Enabled = false;
            //ultraTabPageControl4.Enabled = false;
            //ultraTabPageControl5.Enabled = false;
            //ultraTabPageControl6.Enabled = false;
        }
        private void habilitarBotones(bool nuevo, bool modificar, bool imprimir, bool exportar, bool salir)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = modificar;
            btnImprimir.Enabled = imprimir;
            btnExportarExcel.Enabled = exportar;
            btnSalir.Enabled = salir;
        }
        public void LimpiarCampos()
        {
            txtServicio.Text = "";
            txtSala.Text = "";
            txtPreOperatorio.Text = "";
            txtPostOperatorio.Text = "";
            txtProyectada.Text = "";

            rdbElectiva.Checked = false;
            rdbEmergencia.Checked = false;
            rdbPaleativa.Checked = false;
            txtRealizada.Text = "";
            txtCirujano.Text = "";
            txtPAyudante.Text = "";
            txtSAyudante.Text = "";
            txtTAyudante.Text = "";
            txtInstrumentista.Text = "";
            txtCircundante.Text = "";
            txtAnestesista.Text = "";
            txtAyuAnestesista.Text = "";
            dtpFecha.Value = DateTime.Now;
            txtHoraInicio.Text = "";
            txtHoraTerm.Text = "";
            txtTipoAnestesia.SelectedIndex = 1;
            //txtTipoAnestesia.Text = protocolo.PROT_TIPOANEST;
            rtbDieresis.Text = "";
            rtbExposicion.Text = "";
            rtbExploracion.Text = "";
            rtbProcedimientos.Text = "";
            rtbSintesis.Text = "";
            rtbComplicaciones.Text = "";
            txtHoraAnestecia.Text = "";
            rdbSi.Checked = false;
            rdbNo.Checked = true;
            rtbDiagHispa.Text = "";
            txtDictada.Text = "";
            dtpFechaDictada.Value = DateTime.Now;
            txtHoraDictada.Text = "";
            if (gridSol.Rows.Count > 1)
                txtEscrita.Text = His.Entidades.Clases.Sesion.nomUsuario;
            txtProfesional.Text = "";
            txt_tipos_anestesia.Text = "";
            dtgProyectada.Rows.Clear();
            dtgRealizada.Rows.Clear();
            txtCirujano1.Text = "";
            txtCirujano2.Text = "";
        }

        private void llenarCamposProtocolo(HC_PROTOCOLO_OPERATORIO protocolo)
        {
            dtgProyectada.Rows.Clear();
            dtgRealizada.Rows.Clear();
            txtServicio.Text = nuevoProtocolo.PROT_SERVICIO;
            txtSala.Text = nuevoProtocolo.PROT_SALA;
            txtPreOperatorio.Text = protocolo.PROT_PREOPERATORIO;
            txtPostOperatorio.Text = protocolo.PROT_POSTOPERATORIO;
            string[] cdnProyectada = protocolo.PROT_PROYECTADA.Split('|');
            string[] cdnRealizada = protocolo.PROT_REALIZADO.Split('|');
            for (int i = 0; i < cdnProyectada.Length; i++)
            {
                string[] cad1 = cdnProyectada[i].Split('*');
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _cie = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _porcentaje = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _observacion = new DataGridViewTextBoxCell();

                _codigo.Value = "";
                _cie.Value = cad1[0].Trim().Substring(0, cad1[0].Length - 2);
                _porcentaje.Value = cad1[1].Trim().Substring(0, cad1[1].Length - 1);
                _observacion.Value = cad1[2].Trim().Substring(0, cad1[2].Length - 1);

                fila.Cells.Add(_codigo);
                fila.Cells.Add(_cie);
                fila.Cells.Add(_porcentaje);
                fila.Cells.Add(_observacion);

                dtgProyectada.Rows.Add(fila);

            }
            for (int i = 0; i < cdnRealizada.Length; i++)
            {
                string[] cad1 = cdnRealizada[i].Split('*');
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _cie = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _porcentaje = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _observacion = new DataGridViewTextBoxCell();

                _codigo.Value = "";
                _cie.Value = cad1[0].Trim().Substring(0, cad1[0].Length - 2);
                _porcentaje.Value = cad1[1].Trim().Substring(0, cad1[1].Length - 1);
                _observacion.Value = cad1[2].Trim().Substring(0, cad1[2].Length - 1);

                fila.Cells.Add(_codigo);
                fila.Cells.Add(_cie);
                fila.Cells.Add(_porcentaje);
                fila.Cells.Add(_observacion);

                dtgRealizada.Rows.Add(fila);

            }
            //txtProyectada.Text = protocolo.PROT_PROYECTADA;
            //txtRealizada.Text = protocolo.PROT_REALIZADO;
            if (protocolo.PROT_ELECTIVA == 1)
                rdbElectiva.Checked = true;
            if (protocolo.PROT_EMERGENTE == 1)
                rdbEmergencia.Checked = true;
            if (protocolo.PROT_PALEATIVA == 1)
                rdbPaleativa.Checked = true;
            txtCirujano.Text = protocolo.PROT_CIRUJANO;
            txtPAyudante.Text = protocolo.PROT_PAYUDANTE;
            txtSAyudante.Text = protocolo.PROT_SAYUDANTE;
            txtTAyudante.Text = protocolo.PROT_TAYUDANTE;
            txtInstrumentista.Text = protocolo.PROT_INSTRUMENTISTA;
            txtCircundante.Text = protocolo.PROT_CIRCULANTE;
            txtAnestesista.Text = protocolo.PROT_ANESTESISTA;
            txtAyuAnestesista.Text = protocolo.PROT_AYUANESTESIA;
            dtpFecha.Value = protocolo.PROT_FECHA.Value;
            txtHoraInicio.Text = protocolo.PROT_HORAINICIO;
            txtHoraTerm.Text = protocolo.PROT_HORAFIN;
            txtHoraAnestecia.Text = protocolo.PROT_HORA_ANESTESIA;
            //int index = txtTipoAnestesia.FindString(protocolo.PROT_TIPOANEST);
            //txtTipoAnestesia.SelectedIndex = index;
            txt_tipos_anestesia.Text = protocolo.PROT_TIPOANEST;
            if (txtTipoAnestesia.SelectedIndex == -1)
            {
                txtTipoAnestesia.Text = protocolo.PROT_TIPOANEST;
            }
            //txtTipoAnestesia.Text = protocolo.PROT_TIPOANEST;
            rtbDieresis.Text = protocolo.PROT_DIERESIS;
            rtbExposicion.Text = protocolo.PROT_EXPOSICION;
            rtbExploracion.Text = protocolo.PROT_EXPLORACION;
            rtbProcedimientos.Text = protocolo.PROT_PROCEDIMIENTO;
            rtbSintesis.Text = protocolo.PROT_SINTESIS;
            string cadena = protocolo.PROT_COMPLICACIONES;
            char[] separadores = { '\n', '/' }; // Define el carácter delimitador, en este caso, el punto y coma

            string[] partes = cadena.Split(separadores, StringSplitOptions.RemoveEmptyEntries);
            if(partes.Length > 0)
            {
                if(partes.Length == 4)
                {
                    rtbComplicaciones.Text = partes[0];
                    txt_perdida_sanguinea.Text = partes[1];
                    txt_sangrado_aprox.Text = partes[2];
                    txt_uso_material.Text = partes[3];
                    rdb_material_si.Checked = true;
                }
                else
                {
                    rtbComplicaciones.Text = partes[0];
                    txt_perdida_sanguinea.Text = partes[1];
                    txt_sangrado_aprox.Text = partes[2];
                    rdb_material_no.Checked = true;
                }
            }
            if (protocolo.PROT_EXAMENHIS == true)
            {
                rdbSi.Checked = true;
                txt_detalle_exaHisto.Text = protocolo.DETALLE_HISTOPATOLOGICO;
                txt_detalle_exaHisto.Enabled = true;
            }
            else
                rdbNo.Checked = true;
            rtbDiagHispa.Text = protocolo.PROT_DIAGNOSTICOH;
            txtDictada.Text = protocolo.PROT_DICTADO;
            dtpFechaDictada.Value = protocolo.PROT_FECHADIC.Value;
            txtHoraDictada.Text = protocolo.PROT_HORADIC;
            txtEscrita.Text = protocolo.PROT_ESCRITA;
            txtProfesional.Text = protocolo.PROT_PROFESIONAL;
            if (protocolo.OtroAnestesia != "")
            {
                txtOtroAnestesia.Text = protocolo.OtroAnestesia;
                txtOtroAnestesia.Enabled = true;
            }
            txtCirujano1.Text = protocolo.COCIRUJANO_1;
            txtCirujano2.Text = protocolo.COCIRUJANO_2;

            TimeSpan Duracion;
            DateTime horainicio = Convert.ToDateTime(protocolo.PROT_HORAINICIO);
            DateTime horafin = Convert.ToDateTime(protocolo.PROT_HORAFIN);
            Duracion = horafin.Subtract(horainicio).Duration();
            txtHoraTotal.Text = Convert.ToString(Duracion);

            if (protocolo.CULTIVO == true)
            {
                rb_cultivo_si.Checked = true;
                txt_examen_cultivo.Text = protocolo.CULTIVO_DETALLE;
                txt_examen_cultivo.Enabled = true;
            }
            else
            {
                rb_cultivo_no.Checked = true;

            }

            if (protocolo.DREN == true)
            {
                rb_dren_si.Checked = true;
                txt_dren.Text = protocolo.DREN_DETALLE;
                txt_dren.Enabled = true;
            }
            else
            {
                rb_cultivo_no.Checked = true;

            }

        }

        private HC_PROTOCOLO_OPERATORIO llenarProtocolo()
        {
            nuevoProtocolo.PROT_SERVICIO = txtServicio.Text;
            nuevoProtocolo.PROT_SALA = txtSala.Text;

            nuevoProtocolo.PROT_PREOPERATORIO = txtPreOperatorio.Text.Trim();
            nuevoProtocolo.PROT_POSTOPERATORIO = txtPostOperatorio.Text.Trim();
            string cadenaProyectada = "";
            string cadenaRealizado = "";
            for (int i = 0; i < dtgProyectada.Rows.Count - 1; i++)
            {
                cadenaProyectada = cadenaProyectada + dtgProyectada.Rows[i].Cells[1].Value.ToString() + "-*" + dtgProyectada.Rows[i].Cells[2].Value.ToString() + "-*" + dtgProyectada.Rows[i].Cells[3].Value.ToString() + " | ";
            }
            for (int i = 0; i < dtgRealizada.Rows.Count - 1; i++)
            {
                cadenaRealizado = cadenaRealizado + dtgRealizada.Rows[i].Cells[1].Value.ToString() + "-*" + dtgRealizada.Rows[i].Cells[2].Value.ToString() + "-*" + dtgRealizada.Rows[i].Cells[3].Value.ToString() + " | ";
            }
            nuevoProtocolo.PROT_PROYECTADA = cadenaProyectada.Trim().Substring(0, cadenaProyectada.Length - 2);
            nuevoProtocolo.PROT_REALIZADO = cadenaRealizado.Trim().Substring(0, cadenaRealizado.Length - 2);
            nuevoProtocolo.ADF_CODIGO = CodigoProtocolo;
            if (rdbElectiva.Checked == true)
            {
                nuevoProtocolo.PROT_ELECTIVA = 1;
                nuevoProtocolo.PROT_EMERGENTE = 0;
                nuevoProtocolo.PROT_PALEATIVA = 0;
            }
            if (rdbEmergencia.Checked == true)
            {
                nuevoProtocolo.PROT_ELECTIVA = 0;
                nuevoProtocolo.PROT_EMERGENTE = 1;
                nuevoProtocolo.PROT_PALEATIVA = 0;
            }
            if (rdbPaleativa.Checked == true)
            {
                nuevoProtocolo.PROT_ELECTIVA = 0;
                nuevoProtocolo.PROT_EMERGENTE = 0;
                nuevoProtocolo.PROT_PALEATIVA = 1;
            }
            nuevoProtocolo.PROT_CIRUJANO = txtCirujano.Text;
            nuevoProtocolo.PROT_PAYUDANTE = txtPAyudante.Text;
            nuevoProtocolo.PROT_SAYUDANTE = txtSAyudante.Text;
            nuevoProtocolo.PROT_TAYUDANTE = txtTAyudante.Text;
            nuevoProtocolo.PROT_INSTRUMENTISTA = txtInstrumentista.Text;
            nuevoProtocolo.PROT_CIRCULANTE = txtCircundante.Text;
            nuevoProtocolo.PROT_ANESTESISTA = txtAnestesista.Text;
            nuevoProtocolo.PROT_AYUANESTESIA = txtAyuAnestesista.Text;
            nuevoProtocolo.PROT_FECHA = dtpFecha.Value;
            nuevoProtocolo.PROT_HORAINICIO = txtHoraInicio.Text;
            nuevoProtocolo.PROT_HORAFIN = txtHoraTerm.Text;
            nuevoProtocolo.PROT_TIPOANEST = txt_tipos_anestesia.Text;

            nuevoProtocolo.PROT_DIERESIS = rtbDieresis.Text;
            nuevoProtocolo.PROT_EXPOSICION = rtbExposicion.Text;
            nuevoProtocolo.PROT_EXPLORACION = rtbExploracion.Text;
            nuevoProtocolo.PROT_PROCEDIMIENTO = rtbProcedimientos.Text;
            nuevoProtocolo.PROT_SINTESIS = rtbSintesis.Text;
            nuevoProtocolo.PROT_COMPLICACIONES = rtbComplicaciones.Text + "\n//" + txt_perdida_sanguinea.Text + "\n//" + txt_sangrado_aprox.Text + "\n//" + txt_uso_material.Text;
            if (rdbSi.Checked == true)
                nuevoProtocolo.PROT_EXAMENHIS = true;
            else
                nuevoProtocolo.PROT_EXAMENHIS = false;
            nuevoProtocolo.PROT_DIAGNOSTICOH = rtbDiagHispa.Text;
            nuevoProtocolo.PROT_DICTADO = txtDictada.Text;
            nuevoProtocolo.PROT_FECHADIC = dtpFechaDictada.Value;
            nuevoProtocolo.PROT_HORADIC = txtHoraDictada.Text;
            nuevoProtocolo.PROT_ESCRITA = txtEscrita.Text;
            nuevoProtocolo.PROT_PROFESIONAL = txtProfesional.Text;
            nuevoProtocolo.OtroAnestesia = txtOtroAnestesia.Text;

            nuevoProtocolo.COCIRUJANO_1 = txtCirujano1.Text;
            nuevoProtocolo.COCIRUJANO_2 = txtCirujano2.Text;

            nuevoProtocolo.DETALLE_HISTOPATOLOGICO = txt_detalle_exaHisto.Text;
            if (rb_cultivo_si.Checked)
            {
                nuevoProtocolo.CULTIVO = true;
                nuevoProtocolo.CULTIVO_DETALLE = txt_examen_cultivo.Text;
            }
            else
            {
                nuevoProtocolo.CULTIVO = false;
                nuevoProtocolo.CULTIVO_DETALLE = "";
            }
            if (rb_dren_si.Checked)
            {
                nuevoProtocolo.DREN = true;
                nuevoProtocolo.DREN_DETALLE = txt_dren.Text;
            }
            else
            {
                nuevoProtocolo.DREN = false;
                nuevoProtocolo.DREN_DETALLE = "";
            }


            return nuevoProtocolo;
        }

        private void cargarAtencion(int codAtencion, int CodigoProtocolo)
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
                aseguradora = NegAseguradoras.recuperaAseguradoraPorAtencion(codAtencion);
                lbl_aseguradora.Text = aseguradora.ASE_NOMBRE;
                txtEscrita.Text = His.Entidades.Clases.Sesion.nomUsuario;
                txtDictada.Text = His.Entidades.Clases.Sesion.nomUsuario;
                cargarPaciente(atencion.PACIENTES.PAC_CODIGO);
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                int codigoMedico = medicos.FirstOrDefault(m => m.EntityKey == atencion.MEDICOSReference.EntityKey).MED_CODIGO;
                // if (codigoMedico != 0)
                cargarMedico(codigoMedico);
                HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == atencion.HABITACIONESReference.EntityKey);
                if (hab != null)
                    txtCama.Text = hab.hab_Numero;
                cargarProtocolo(codAtencion, CodigoProtocolo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar los datos de la atencion, error: " + ex.Message, "error");
            }
        }

        private void cargarPaciente(int codPac)
        {
            paciente = NegPacientes.RecuperarPacienteID(codPac);

            if (paciente != null)
            {
                txt_pacNombre.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                                     paciente.PAC_APELLIDO_MATERNO + " " +
                                     paciente.PAC_NOMBRE1 + " " +
                                     paciente.PAC_NOMBRE2;
                txt_pacHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                txt_sexo.Text = paciente.PAC_GENERO;
            }
            else
            {
                txt_pacHCL.Text = string.Empty;
                txt_pacNombre.Text = string.Empty;
                txt_sexo.Text = string.Empty;
            }
        }

        private void cargarMedico(int cod)
        {
            medico = NegMedicos.RecuperaMedicoId(cod);
            lbl_medico.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                medico.MED_APELLIDO_MATERNO.Trim() + " " +
                medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
        }

        private void cargarProtocolo(int codAtencion, int CodigoProtocolo)
        {
            nuevoProtocolo = NegProtocoloOperatorio.recuperarProtocoloNew(codAtencion, CodigoProtocolo);
            try
            {
                txtHoraAnestecia.Text = NegProtocoloOperatorio.RecuperarHoraAnestesia(CodigoProtocolo, codAtencion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo recuperar la hora de anestesia. Más Detalles en: \r\n" + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            if (nuevoProtocolo == null)
            {
                habilitarBotones(true, false, false, false, true);
            }
            else
            {
                habilitarBotones(false, false, true, true, true);
                tabulador.Enabled = true;
                llenarCamposProtocolo(nuevoProtocolo);
                BloquearTabulador(false);
            }
        }

        private bool validarFormulario()
        {
            errorProtocolo.Clear();
            bool valido = false;
            if (txtServicio.Text.Trim() == string.Empty)
            {
                AgregarError(txtServicio);
                valido = true;
            }
            if (txtSala.Text.Trim() == string.Empty)
            {
                AgregarError(txtSala);
                valido = true;
            }
            if (txtPreOperatorio.Text.Trim() == string.Empty)
            {
                AgregarError(txtPreOperatorio);
                valido = true;
            }

            if (txtPostOperatorio.Text.Trim() == string.Empty)
            {
                AgregarError(txtPostOperatorio);
                valido = true;
            }

            //if (txtProyectada.Text.Trim() == string.Empty)
            //{
            //    AgregarError(txtProyectada);
            //    valido = true;
            //}
            //if (txtRealizada.Text.Trim() == string.Empty)
            //{
            //    AgregarError(txtRealizada);
            //    valido = true;
            //}

            if (rtbComplicaciones.Text.Trim() == string.Empty)
            {
                AgregarError(rtbComplicaciones);
                valido = true;
            }

            if (rtbExploracion.Text.Trim() == string.Empty)
            {
                AgregarError(rtbExploracion);
                valido = true;
            }

            if (rdbElectiva.Checked == false)
            {
                if (rdbEmergencia.Checked == false)
                {
                    if (rdbPaleativa.Checked == false)
                    {
                        AgregarError(rdbPaleativa);
                        valido = true;
                    }
                }
            }

            if (txtCirujano.Text.Trim() == string.Empty)
            {
                AgregarError(txtCirujano);
                valido = true;
            }

            if (txtAnestesista.Text.Trim() == string.Empty)
            {
                AgregarError(txtAnestesista);
                valido = true;
            }
            if (txtTipoAnestesia.Text.Trim() == string.Empty)
            {
                AgregarError(txtTipoAnestesia);
                valido = true;
            }

            if (txt_tipos_anestesia.Text == "")
            {
                AgregarError(txt_tipos_anestesia);
                valido = true;
            }

            if (rdbSi.Checked)
            {
                if (txtDrPatologo.Text.Trim() == "")
                {
                    AgregarError(txtDrPatologo);
                    valido = true;
                }
                if (txt_detalle_exaHisto.Text.Trim() == "")
                {
                    AgregarError(txt_detalle_exaHisto);
                    valido = true;
                }
            }
            if (rb_cultivo_si.Checked)
            {
                if (txt_examen_cultivo.Text.Trim() == "")
                {
                    AgregarError(txt_examen_cultivo);
                    valido = true;
                }
            }

            if (rb_dren_si.Checked)
            {
                if (txt_dren.Text.Trim() == "")
                {
                    AgregarError(txt_dren);
                    valido = true;
                }
            }

            if (txtEscrita.Text.Trim() == string.Empty)
            {
                AgregarError(txtEscrita);
                valido = true;
            }
            //if (txtPAyudante.Text.Trim() == string.Empty)
            //{
            //    AgregarError(txtPAyudante);
            //    valido = true;
            //}
            if (txtInstrumentista.Text.Trim() == string.Empty)
            {
                AgregarError(txtInstrumentista);
                valido = true;
            }
            if (txtCircundante.Text.Trim() == string.Empty)
            {
                AgregarError(txtCircundante);
                valido = true;
            }
            if (txtHoraInicio.Text == ":")
            {
                AgregarError(txtHoraInicio);
                valido = true;
            }
            if (txtHoraTerm.Text == ":")
            {
                AgregarError(txtHoraTerm);
                valido = true;
            }
            if (txtHoraAnestecia.Text == ":")
            {
                AgregarError(txtHoraAnestecia);
                valido = true;
            }
            if (txtHoraDictada.Text == ":")
            {
                AgregarError(groupBox4);
                valido = true;
            }
            //if (txtProfesional.Text.Trim() == string.Empty)
            //{
            //    AgregarError(groupBox4);
            //    valido = true;
            //}
            if (rtbDieresis.Text.Trim() == string.Empty)
            {
                AgregarError(rtbDieresis);
                valido = true;
            }
            if (rtbExposicion.Text.Trim() == string.Empty)
            {
                AgregarError(rtbExposicion);
                valido = true;
            }
            //if (rtbSintesis.Text.Trim() == string.Empty)
            //{
            //    AgregarError(rtbSintesis);
            //    valido = true;
            //}
            if (rtbProcedimientos.Text.Trim() == string.Empty)
            {
                AgregarError(rtbProcedimientos);
                valido = true;
            }
            if (rtbComplicaciones.Text.Trim() == string.Empty || rtbComplicaciones.Text == "SANGRADO:\r\n\r\nOTROS:")
            {
                AgregarError(rtbComplicaciones);
                valido = true;
            }
            if (rdb_material_si.Checked)
            {
                if(txt_uso_material.Text == "")
                {
                    AgregarError(txt_uso_material);
                    valido = true;
                }
            }
            if(txt_perdida_sanguinea.Text == "")
            {
                AgregarError(label22);
                valido = true;
            }
            if(txt_sangrado_aprox.Text == "")
            {
                AgregarError(label23);
                valido = true;
            }
            if (valido == true)
            {
                MessageBox.Show("Datos Incompletos, Revise todos los campos e intente guardar nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return valido;
        }

        private void AgregarError(Control control)
        {
            errorProtocolo.SetError(control, "Campo Requerido");
        }

        private void guardarProtocolo()
        {
            int prot_codigo;
            if (!validarFormulario())
            {
                if (!controlarCamposExtensos())
                {
                    try
                    {
                        if (nuevo == true)
                        {
                            if (gridSol.Rows.Count > 1)
                            {
                                if (NegImagen.getProtocoloExiste(Convert.ToInt64(CodigoProtocolo)))
                                {
                                    Int64 adf_codigo = NegImagen.getCodigoProtocolo(Convert.ToInt64(CodigoAtencion));
                                    CodigoProtocolo = Convert.ToInt32(adf_codigo);
                                }
                            }
                            nuevoProtocolo = new HC_PROTOCOLO_OPERATORIO();
                            nuevoProtocolo.PROT_CODIGO = NegProtocoloOperatorio.ultimoCodigo() + 1;
                            prot_codigo = NegProtocoloOperatorio.ultimoCodigo() + 1;
                            nuevoProtocolo.ATENCIONESReference.EntityKey = atencion.EntityKey;
                            nuevoProtocolo = llenarProtocolo();
                            nuevoProtocolo.OtroAnestesia = txtOtroAnestesia.Text;
                            try
                            {
                                NegProtocoloOperatorio.crearProtocolo(nuevoProtocolo);
                                
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("\r\n No se pudo guardar el Protocolo verifique la excepción \r\n \r\n " + ex, "PROTOCOLO OPERATORIO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }



                            //Guardamos la hora de anestesia
                            NegProtocoloOperatorio.GuardarHoraAnestesia(prot_codigo, txtHoraAnestecia.Text);

                            MessageBox.Show("Registro Guardado", "PROTOCOLO OPERATORIO", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            nuevo = false;
                            habilitarBotones(true, false, true, true, false);
                            BloquearTabulador(false);
                            refrescarSolicitudes();
                        }
                        else
                        {
                            nuevoProtocolo = llenarProtocolo();
                            NegProtocoloOperatorio.actualizarProtocolo(nuevoProtocolo);
                            
                            MessageBox.Show("Registro Guardado", "PROTOCOLO OPERATORIO", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            habilitarBotones(true, false, true, true, true);
                            BloquearTabulador(false);
                            refrescarSolicitudes();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese todos los campos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private bool controlarCamposExtensos()
        {
            bool flag = false;
            //if (rtbProcedimientos.TextLength <= 2000)
            //{
            //    int cont = 0;
            //    foreach (string s in rtbProcedimientos.Text.Split('\n'))
            //        cont++;
            //    if (cont > 32)
            //    {
            //        AgregarError(rtbProcedimientos);
            //        flag = true;
            //    }
            //}
            //else
            //{
            //    AgregarError(rtbProcedimientos);
            //    flag = true;
            //}
            return flag;
        }


        private void validarTextoProcedimiento()
        {
            proced1 = "";
            proced2 = "";
            int firstLine = rtbProcedimientos.GetLineFromCharIndex(0);
            int lastLine = rtbProcedimientos.GetLineFromCharIndex(rtbProcedimientos.Text.Length);
            List<string> lineas = new List<string>();

            for (int i = firstLine; i <= lastLine; i++)
            {
                int firstIndexFromLine = rtbProcedimientos.GetFirstCharIndexFromLine(i);
                int firstIndexFromNextLine = rtbProcedimientos.GetFirstCharIndexFromLine(i + 1);

                if (firstIndexFromNextLine == -1)
                {
                    // Get character index of last character in this line:
                    Point pt = new Point(rtbProcedimientos.ClientRectangle.Width, rtbProcedimientos.GetPositionFromCharIndex(firstIndexFromLine).Y);
                    firstIndexFromNextLine = rtbProcedimientos.GetCharIndexFromPosition(pt);
                    firstIndexFromNextLine += 1;
                }

                lineas.Add(rtbProcedimientos.Text.Substring(firstIndexFromLine, firstIndexFromNextLine - firstIndexFromLine));
                if (i < 9)
                {
                    proced1 = proced1 +
                              rtbProcedimientos.Text.Substring(firstIndexFromLine,
                                                               firstIndexFromNextLine - firstIndexFromLine);
                }
                else
                {
                    proced2 = proced2 +
                              rtbProcedimientos.Text.Substring(firstIndexFromLine,
                                                               firstIndexFromNextLine - firstIndexFromLine);
                }


            }
        }

        #endregion

        #region Eventos

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            nuevo = true;
            habilitarBotones(false, true, false, false, true);
            LimpiarCampos();
            BloquearTabulador(true);
            tabulador.Enabled = true;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            guardarProtocolo();

        }

        private void txtServicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtSala.Focus();
            }
        }

        private void txtSala_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtPreOperatorio.Focus();
            }
        }

        //private void txtCama_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        txtPreOperatorio.Focus();
        //    }
        //} 
        private void txtPreOperatorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtPostOperatorio.Focus();
            }

            else if (e.KeyChar == (char)(Keys.Delete))
            {
                txtPreOperatorio.Text = "";
            }
        }

        private void txtPostOperatorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtProyectada.Focus();
            }
            else if (e.KeyChar == (char)(Keys.Delete))
            {
                txtPostOperatorio.Text = "";
            }
        }

        private void txtProyectada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rdbElectiva.Focus();
            }
            else if (e.KeyChar == (char)(Keys.Delete))
            {
                txtProyectada.Text = "";
            }
        }
        private void rdbElectiva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtRealizada.Focus();
            }
        }

        //private void rdbEmergencia_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        rdbPaleativa.Focus();
        //    }
        //}

        //private void rdbPaleativa_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        txtRealizada.Focus();
        //    }
        //}
        private void txtRealizada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["equipo"];
                txtCirujano.Focus();
            }
            else if (e.KeyChar == (char)(Keys.Delete))
            {
                txtRealizada.Text = "";
            }
        }

        private void txtCirujano_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtPAyudante.Focus();
                txtProfesional.Text = txtCirujano.Text;
            }
        }

        private void txtPAyudante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtSAyudante.Focus();
            }
        }

        private void txtSAyudante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtTAyudante.Focus();
            }
        }

        private void txtTAyudante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtInstrumentista.Focus();
            }
        }

        private void txtInstrumentista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtCircundante.Focus();
            }
        }

        private void txtCircundante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtAnestesista.Focus();
            }
        }

        private void txtAnestesista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtAyuAnestesista.Focus();
            }
        }

        private void txtAyuAnestesista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                dtpFecha.Focus();
            }
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtHoraInicio.Focus();
            }
        }

        //private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        txtHoraTerm.Focus();
        //    }
        //}

        //private void txtHoraFin_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
        //    {
        //        e.Handled = true;
        //        SendKeys.SendWait("{TAB}");
        //        txtTipoAnestesia.Focus();
        //    }
        //}

        private void txtHoraInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtHoraTerm.Focus();
            }
        }

        private void txtHoraTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtHoraAnestecia.Focus();
            }
        }

        private void txtTipoAnestesia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["tiempos"];
                SendKeys.SendWait("{TAB}");
                rtbDieresis.Focus();
            }
        }

        private void rtbDieresis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rtbExposicion.Focus();
            }
        }

        private void rtbExposicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rtbExploracion.Focus();
            }
        }

        private void rtbExploracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rtbProcedimientos.Focus();
            }
        }

        private void rtbProcedimientos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["resumen"];
                rtbSintesis.Focus();

            }
            int linea = rtbProcedimientos.GetLineFromCharIndex(rtbProcedimientos.TextLength) + 1;
            if (linea > 35)
            {
                e.Handled = true;
                tabulador.SelectedTab = tabulador.Tabs["resumen"];
                rtbSintesis.Focus();
            }
        }

        private void rtbSintesis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rtbComplicaciones.Focus();
            }
        }

        private void rtbComplicaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rdbSi.Focus();
            }
        }

        private void rdbSi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                rtbDiagHispa.Focus();
            }
        }


        private void rtbDiagHispa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtDictada.Focus();
            }
        }

        private void txtDictada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                dtpFechaDictada.Focus();
            }
        }

        private void dtpFechaDictada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtHoraDictada.Focus();
            }
        }

        private void txtHoraDictad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtEscrita.Focus();
            }
        }

        private void txtEscrita_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtProfesional.Focus();
            }
        }

        private void txtProfesional_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtPreOperatorio.Focus();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimirReporte("reporte");
        }

        private void imprimirReporte(string accion)
        {
            try
            {
                string FullPath;
                FullPath = "C:\\Sic3000\\Iconos\\LogoEmpresa.png";
                ReporteProtocoloOperatorio reporte = new ReporteProtocoloOperatorio();
                NegCertificadoMedico m = new NegCertificadoMedico();
                if (!NegParametros.ParametroFormularios())
                    reporte.PROT_HISTORIA_CLINICA = paciente.PAC_HISTORIA_CLINICA + "-" + atencion.ATE_NUMERO_ATENCION;
                else
                    reporte.PROT_HISTORIA_CLINICA = paciente.PAC_IDENTIFICACION;
                reporte.PROT_EMPRESA = His.Entidades.Clases.Sesion.nomEmpresa;
                reporte.PROT_NUMHOJA = 1;
                reporte.PROT_LOGO = NegUtilitarios.RutaLogo("General");
                reporte.PROT_NOMBREPACIENTE = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                reporte.PROT_APELLIDOPACIENTE = paciente.PAC_APELLIDO_PATERNO + "  " + paciente.PAC_APELLIDO_MATERNO;
                reporte.PROT_GENERO = txt_sexo.Text;
                reporte.PROT_NUMHOJA = 1;
                reporte.PROT_MEDICO = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                reporte.PROT_SERVICIO = txtServicio.Text;
                reporte.PROT_SALA = txtSala.Text;
                reporte.PROT_HABITACION = txtCama.Text;
                reporte.PROT_PREOPERATORIO = txtPreOperatorio.Text;
                reporte.PROT_POSTOPERATORIO = txtPostOperatorio.Text;
                string cadenaProyectada = "";
                string cadenaRealizado = "";
                for (int i = 0; i < dtgProyectada.Rows.Count - 1; i++)
                {
                    cadenaProyectada = cadenaProyectada + dtgProyectada.Rows[i].Cells[1].Value.ToString() + "-" + dtgProyectada.Rows[i].Cells[2].Value.ToString() + "-" + dtgProyectada.Rows[i].Cells[3].Value.ToString() + " | ";
                }
                for (int i = 0; i < dtgRealizada.Rows.Count - 1; i++)
                {
                    cadenaRealizado = cadenaRealizado + dtgRealizada.Rows[i].Cells[1].Value.ToString() + "-" + dtgRealizada.Rows[i].Cells[2].Value.ToString() + "-" + dtgRealizada.Rows[i].Cells[3].Value.ToString() + " | ";
                }
                reporte.PROT_PROYECTADA = cadenaProyectada.Trim().Substring(0, cadenaProyectada.Length - 2);
                reporte.PROT_REALIZADO = cadenaRealizado.Trim().Substring(0, cadenaRealizado.Length - 2);
                if (rdbElectiva.Checked == true)
                {
                    reporte.PROT_ELECTIVA = "X";
                    reporte.PROT_EMERGENCIA = " ";
                    reporte.PROT_PALEATIVA = " ";
                }
                if (rdbEmergencia.Checked == true)
                {
                    reporte.PROT_ELECTIVA = " ";
                    reporte.PROT_EMERGENCIA = "X";
                    reporte.PROT_PALEATIVA = " ";
                }
                if (rdbPaleativa.Checked == true)
                {
                    reporte.PROT_ELECTIVA = " ";
                    reporte.PROT_EMERGENCIA = " ";
                    reporte.PROT_PALEATIVA = "X";
                }
                if (txtCirujano2.Text != "") // se comenta hasta realizar el nuevo diseno // Mario  // se verifico con reporte nuevo 2024/02/05 Cristian Ruiz
                {
                    reporte.PROT_CIRUJANO = txtCirujano.Text + "\r\n" + "COCIRUJANO 2: " + txtCirujano2.Text;
                }
                if (txtCirujano1.Text != "")
                {
                    reporte.PROT_CIRUJANO = txtCirujano.Text + "\r\n" + "COCIRUJANO 1: " + txtCirujano1.Text;
                    if (txtCirujano2.Text != "")
                    {
                        reporte.PROT_CIRUJANO = txtCirujano.Text + "\r\n" + "COCIRUJANO 1: " + txtCirujano1.Text + "\r\n" + "COCIRUJANO 2:" + txtCirujano2.Text;
                    }
                }
                else
                    reporte.PROT_CIRUJANO = txtCirujano.Text;

                reporte.PROT_PAYUDANTE = txtPAyudante.Text;
                reporte.PROT_SAYUDANTE = txtSAyudante.Text;
                reporte.PROT_TAYUDANTE = txtTAyudante.Text;
                reporte.PROT_INSTRUMENTISTA = txtInstrumentista.Text;
                reporte.PROT_CIRCULANTE = txtCircundante.Text;
                reporte.PROT_ANESTESISTA = txtAnestesista.Text;
                reporte.PROT_AYUANESTESISTA = txtAyuAnestesista.Text;
                reporte.PROT_FECHA = dtpFecha.Value;
                reporte.PROT_HORAI = txtHoraInicio.Text;
                reporte.PROT_HORAT = txtHoraTerm.Text;
                reporte.PROT_TIPOANESTESIA = txtTipoAnestesia.Text;
                reporte.PROT_DIERESIS = validarTexto(rtbDieresis.Text).Trim();
                reporte.PROT_EXPOSICION = rtbExposicion.Text;
                reporte.PROT_EXPLORACION = rtbExploracion.Text;
                validarTextoProcedimiento();



                reporte.PROT_PROCEDIMIENTO = proced1;
                reporte.PROT_PROCEDIMIENTO2 = proced2;
                reporte.PROT_SINTESIS = rtbSintesis.Text;
                reporte.PROT_COMPLICACIONES = rtbComplicaciones.Text;
                if (rdbSi.Checked == true)
                {
                    reporte.PROT_EXAMENHISSI = "X";
                    reporte.PROT_EXAMENHISNO = " ";
                }
                if (rdbNo.Checked == true)
                {
                    reporte.PROT_EXAMENHISSI = " ";
                    reporte.PROT_EXAMENHISNO = "X";
                }

                reporte.PROT_DIAGNOSTICOSHIS = rtbDiagHispa.Text + "\r\n \r\n 9.EXAMEN DE CULTIVO: " + txt_examen_cultivo.Text + "\r\n \r\n 10. DREN: " + txt_dren.Text;
                reporte.PROT_DICTADO = txtDictada.Text;
                reporte.PROT_FECHADIC = dtpFechaDictada.Value;
                reporte.PROT_HORADIC = txtHoraDictada.Text;
                reporte.PROT_ESCRITA = txtEscrita.Text;
                reporte.PROT_PROFESIONAL = txtProfesional.Text;
                reporte.PROT_FECHA_OD = Convert.ToString(dtpFecha.Value).Substring(0, 2);
                reporte.PROT_FECHA_OM = Convert.ToString(dtpFecha.Value).Substring(3, 2);
                reporte.PROT_FECHA_OA = Convert.ToString(dtpFecha.Value).Substring(6, 4);

                reporte.PROT_FECHA_DH = Convert.ToString(dtpFecha.Value).Substring(11, 5);
                reporte.PROT_FECHA_DM = Convert.ToString(dtpFecha.Value).Substring(0, 2);
                reporte.PROT_FECHA_DD = Convert.ToString(dtpFecha.Value).Substring(3, 2);
                reporte.PROT_FECHA_DA = Convert.ToString(dtpFecha.Value).Substring(6, 4);

                ReportesHistoriaClinica reporteOperatorio = new ReportesHistoriaClinica();
                reporteOperatorio.ingresarProtocolo(reporte);
                frmReportes ventana = new frmReportes(1, "protocolo");
                if (accion.Equals("reporte"))
                    ventana.Show();
                else
                {
                    CrearCarpetas_Srvidor("protocolo");
                }

                //ventana.Show();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Nuevo Reporte Protocolo Operatorio 2021 Pablo Rocha 29/04/2024
            try
            {
                string FullPath = "C:\\Sic3000\\Iconos\\LogoEmpresa.png";
                DSProtocoloOperatorio DSprotocolo = new DSProtocoloOperatorio();
                DataRow drDiagnostico;
                drDiagnostico = DSprotocolo.Tables["Diagnostico"].NewRow();
                string[] preOp = txtPreOperatorio.Text.Split('\n');
                if (preOp.Length == 1)
                {
                    string[] ciePos = preOp[0].Split('-');
                    drDiagnostico["preOp1"] = ciePos[1];
                    drDiagnostico["preOpCie1"] = ciePos[0];
                }
                else if (preOp.Length == 2)
                {
                    string[] ciePos1 = preOp[0].Split('-');
                    string[] ciePos2 = preOp[1].Split('-');
                    drDiagnostico["preOp1"] = ciePos1[1];
                    drDiagnostico["preOpCie1"] = ciePos1[0];
                    drDiagnostico["preOp2"] = ciePos2[1];
                    drDiagnostico["preOpCie2"] = ciePos2[0];
                }
                else
                {
                    string[] ciePos1 = preOp[0].Split('-');
                    string[] ciePos2 = preOp[1].Split('-');
                    string[] ciePos3 = preOp[2].Split('-');
                    drDiagnostico["preOp1"] = ciePos1[1];
                    drDiagnostico["preOpCie1"] = ciePos1[0];
                    drDiagnostico["preOp2"] = ciePos2[1];
                    drDiagnostico["preOpCie2"] = ciePos2[0];
                    drDiagnostico["preOp3"] = ciePos3[1];
                    drDiagnostico["preOpCie3"] = ciePos3[0];
                }


                string[] postOpe = txtPostOperatorio.Text.Split('\n');
                if(postOpe.Length == 1)
                {
                    string[] ciePos = postOpe[0].Split('-');
                    drDiagnostico["postOp1"] = ciePos[1];
                    drDiagnostico["postOpCie1"] = ciePos[0];
                }else if (postOpe.Length == 2)
                {
                    string[] ciePos1 = postOpe[0].Split('-');
                    string[] ciePos2 = postOpe[1].Split('-');
                    drDiagnostico["postOp1"] = ciePos1[1];
                    drDiagnostico["postOpCie1"] = ciePos1[0];
                    drDiagnostico["postOp2"] = ciePos2[1];
                    drDiagnostico["postOpCie2"] = ciePos2[0];
                }
                else
                {
                    string[] ciePos1 = postOpe[0].Split('-');
                    string[] ciePos2 = postOpe[1].Split('-');
                    string[] ciePos3 = postOpe[2].Split('-');
                    drDiagnostico["postOp1"] = ciePos1[1];
                    drDiagnostico["postOpCie1"] = ciePos1[0];
                    drDiagnostico["postOp2"] = ciePos2[1];
                    drDiagnostico["postOpCie2"] = ciePos2[0];
                    drDiagnostico["postOp3"] = ciePos3[1];
                    drDiagnostico["postOpCie3"] = ciePos3[0];
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string validarTexto(string campo, int indice)
        {
            int posicion = 0;
            string cadena = "";
            string texto = campo.Substring(indice, (campo.Length - indice));
            int cont = 0;
            for (int i = indice; i < campo.Length; i++)
            {
                if ((posicion < campo.Length - 2) && (cont <= 500))
                {
                    if (!(campo.Substring(i, 2) == "\r\n"))
                    {
                        cadena = cadena + campo.Substring(i, 1);
                        cont++;
                        posicion++;
                    }
                    else
                    {
                        posicion = i + 2;
                        return cadena;
                    }
                }
                else
                {
                    //int pos = CampoValidado(campo, cadena);
                    return cadena;
                }
                if ((posicion + 2 == campo.Length))
                {
                    cadena = cadena + "" + campo.Substring(posicion, 2);
                }
            }
            return cadena;
        }



        public void CrearCarpetas_Srvidor(string modo_formulario)
        {
            try
            {
                His.DatosReportes.Datos.GenerarPdf pdf = new His.DatosReportes.Datos.GenerarPdf();
                pdf.reporte = modo_formulario;
                pdf.campo1 = atencion.ATE_CODIGO.ToString();
                pdf.nuemro_atencion = atencion.ATE_NUMERO_ATENCION.ToString();
                pdf.clinica = paciente.PAC_HISTORIA_CLINICA.ToString();
                pdf.generar();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string validarTexto(string campo)
        {
            string cadena = "";
            int cont = 0;
            for (int i = 0; i < campo.Length; i++)
            {
                if (!(campo.Substring(i, 1) == "\n"))
                {
                    cadena = cadena + campo.Substring(i, 1);
                }
                else
                {
                    cadena = cadena + "  -  ";
                }
            }
            return cadena;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnF1Cirujano_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtCirujano.Text;

            if (ayuda.campoPadre.Text != string.Empty)
                txtCirujano.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
            txtProfesional.Text = txtCirujano.Text;
            btnF1Cirujano1.Focus();
        }

        private string MedicoNombre(int codMedico, string cadena)
        {
            DataTable med = NegMedicos.MedicoIDValida(codMedico);
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return "";
            }
            medico = NegMedicos.MedicoID(codMedico);

            if (medico != null)
                cadena = cadena + "" + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            else
                cadena = string.Empty;
            return cadena;
        }
        private string cargarMedico(int codMedico, string cadena)
        {
            DataTable med = NegMedicos.MedicoIDValida(Convert.ToInt16(codMedico));
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return "";
            }
            medico = NegMedicos.MedicoID(codMedico);

            if (medico != null)
            {
                //if (cadena != "")
                //{
                cadena = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                //}
                //else
                //{
                //    cadena = cadena + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                //}
            }
            else
                cadena = string.Empty;
            return cadena;
        }

        private void btnPAyudante_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtPAyudante.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtPAyudante.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);

            btnSAyudante.Focus();
        }

        private void btnSAyudante_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtSAyudante.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtSAyudante.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);

            btnTAyudante.Focus();
        }

        private void btnCirculante_Click(object sender, EventArgs e)
        {
            //List<MEDICOS> listaMedicos;

            //listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            //frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ////ayuda.campoPadre = txt;
            //ayuda.ShowDialog();
            //string cadena = txtCircundante.Text;
            //if (ayuda.campoPadre.Text != string.Empty)
            //    txtCircundante.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);

            frm_Ayuda_Certificado.Personal = true;
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            x.ShowDialog();

            frm_Ayuda_Certificado.Personal = false;

            if (txtCircundante.Text.Length <= 0)
            {
                txtCircundante.Text = personal;
            }
            else
            {
                string cadena = txtCircundante.Text;
                txtCircundante.Text = "";
                txtCircundante.Text = cadena + "/" + personal;
            }
        }

        private void txtAnestesista_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtAnestesista.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtAnestesista.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void btnAAnest_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtAyuAnestesista.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtAyuAnestesista.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void rtbProcedimientos_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode.Equals("\r"))
            //{
            //    contlrP1++;
            //}
            //if(contlrP1>8)
            //{
            //    e.Handled = true;
            //    rtbProcedimientos.Focus();
            //}else
            //{
            //    if(rtbProcedimientos.TextLength > 800)
            //    {
            //        MessageBox.Show("Campo demasiado Extenso");
            //    }
            //}

        }

        private void rtbProcedimientos2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    tabulador.SelectedTab = tabulador.Tabs["resumen"];
            //    rtbSintesis.Focus();
            //}
            //int linea = rtbProcedimientos2.GetLineFromCharIndex(rtbProcedimientos2.TextLength) + 1;
            //if (linea <= 25) return;
            //e.Handled = true;
            //tabulador.SelectedTab = tabulador.Tabs["resumen"];
            //rtbSintesis.Focus();
        }

        private void btnInstru_Click(object sender, EventArgs e)
        {
            //List<MEDICOS> listaMedicos;

            //listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            //frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ////ayuda.campoPadre = txt;
            //ayuda.ShowDialog();
            //string cadena = txtInstrumentista.Text;
            //if (ayuda.campoPadre.Text != string.Empty)
            //    txtInstrumentista.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);

            frm_Ayuda_Certificado.Personal = true;
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            x.ShowDialog();

            frm_Ayuda_Certificado.Personal = false;

            if (txtInstrumentista.Text.Length <= 0)
            {
                txtInstrumentista.Text = personal;
            }
            else
            {
                string cadena = txtInstrumentista.Text;
                txtInstrumentista.Text = "";
                txtInstrumentista.Text = cadena + "/" + personal;
            }
        }

        private void btnAnest_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;
            PARAMETROS_DETALLE pd = NegParametros.RecuperaPorCodigo(69); // se cambia por parametro para poder cunplir requerimiento Integral me muestre solo Anestesiologos // Mario Valencia // 04-01-2024
            if (pd.PAD_VALOR.Trim() == "1")
            {
                listaMedicos = NegMedicos.listaMedicosIncTipoMedicoXEsp(102);
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                //ayuda.campoPadre = txt;
                ayuda.ShowDialog();
                string cadena = txtAnestesista.Text;
                if (ayuda.campoPadre.Text != string.Empty)
                    txtAnestesista.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
            }
            else
            {
                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                //ayuda.campoPadre = txt;
                ayuda.ShowDialog();
                string cadena = txtAnestesista.Text;
                if (ayuda.campoPadre.Text != string.Empty)
                    txtAnestesista.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);

            }
        }

        private void btnTAyudante_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtTAyudante.Text;
            if (ayuda.campoPadre.Text != string.Empty)
                txtTAyudante.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void rdbNo_Click(object sender, EventArgs e)
        {
            txtDrPatologo.Enabled = false;
        }

        private void rdbSi_Click(object sender, EventArgs e)
        {
        }

        private void btnf1dr_Click(object sender, EventArgs e)
        {
            txtDrPatologo.Text = "";
            frm_Ayuda_Certificado.patologo = true;
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            x.ShowDialog();
            txtDrPatologo.Text = medicopatologo;
            frm_Ayuda_Certificado.patologo = false;




            //List<MEDICOS> listaMedicos;

            //listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            //frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ////ayuda.campoPadre = txt;
            //ayuda.ShowDialog();
            //string cadena = txtDrPatologo.Text;
            //if (ayuda.campoPadre.Text != string.Empty)
            //    txtDrPatologo.Text = MedicoNombre(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void rdbSi_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSi.Checked)
            {
                btnf1dr.Enabled = true;
                rdbNo.Checked = false;
                txt_detalle_exaHisto.Enabled = true;
                txtDrPatologo.Enabled = false;
            }
        }

        private void rdbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNo.Checked)
            {
                btnf1dr.Enabled = false;
                rdbSi.Checked = false;
                txtDrPatologo.Text = "";
                txtDrPatologo.Enabled = false;
                txt_detalle_exaHisto.Text = "";
                txt_detalle_exaHisto.Enabled = false;
            }
        }

        private void btnf1dic_Click(object sender, EventArgs e)
        {
            txtDictada.Text = "";
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtDictada.Text;

            if (ayuda.campoPadre.Text != string.Empty)
                txtDictada.Text = MedicoNombre(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);
        }

        private void btncie1_Click(object sender, EventArgs e)
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            if (busqueda.codigo != null)
            {
                if (txtPreOperatorio.Text.Length == 0)
                {
                    txtPreOperatorio.Text += busqueda.codigo + " - " + busqueda.resultado;
                }
                else
                {
                    txtPreOperatorio.Text += Environment.NewLine;
                    txtPreOperatorio.Text += busqueda.codigo + " - " + busqueda.resultado;
                }

                string[] lineas = txtPreOperatorio.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                if (lineas.Length == 3)
                {
                    btncie1.Enabled = false;
                }
            }
        }

        private void btncie2_Click(object sender, EventArgs e)
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            if (busqueda.codigo != null)
            {
                if (txtPostOperatorio.Text.Length <= 0)
                {
                    txtPostOperatorio.Text = busqueda.codigo + "-" + busqueda.resultado;
                }
                else
                {
                    string cadena = txtPostOperatorio.Text;
                    txtPostOperatorio.Text = "";
                    txtPostOperatorio.Text = cadena + " -- " + busqueda.codigo + "-" + busqueda.resultado;
                }
            }
        }

        private void btntarifario_Click(object sender, EventArgs e)
        {
            frm_AyudaGeneral x = new frm_AyudaGeneral();
            x.tarifario = true;
            x.ShowDialog();
            txtProyectada.Text = "";
            if (x.resultado.ToString() == "")
                return;
            txtProyectada.Text = x.codigo.Trim() + "-" + x.resultado;
            //if (x.resultado != "")
            //{
            //    if (txtProyectada.Text.Length <= 0)
            //    {
            //        txtProyectada.Text = x.codigo.Trim() + "-" + x.resultado;
            //    }
            //    else
            //    {
            //        string cadena = txtProyectada.Text;
            //        txtProyectada.Text = "";
            //        txtProyectada.Text = cadena + " -- " + x.codigo.Trim() + "-" + x.resultado;
            //    }
            //}
            x.tarifario = false;
            mskPorcentaje.Focus();
        }

        private void btntarifario2_Click(object sender, EventArgs e)
        {
            frm_AyudaGeneral x = new frm_AyudaGeneral();
            x.tarifario = true;
            x.ShowDialog();
            txtRealizada.Text = "";
            if (x.resultado.ToString() == "")
                return;
            txtRealizada.Text = x.codigo.Trim() + "-" + x.resultado;
            //if (x.resultado != "")
            //{
            //    if (txtRealizada.Text.Length <= 0)
            //    {
            //        txtRealizada.Text = x.codigo.Trim() + "-" + x.resultado;
            //    }
            //    else
            //    {
            //        string cadena = txtRealizada.Text;
            //        txtRealizada.Text = "";
            //        txtRealizada.Text = cadena + " -- " + x.codigo.Trim() + "-" + x.resultado;
            //    }
            //}
            x.tarifario = false;
            mskPorcentaje1.Focus();
        }
        #endregion

        private void gridSol_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea cargar el Protocolo?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                CodigoProtocolo = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["ID"].Value.ToString());
                cargarAtencion(CodigoAtencion, CodigoProtocolo);
                NegAtenciones atenciones = new NegAtenciones();
                string estado = atenciones.EstadoCuenta(Convert.ToString(CodigoAtencion));
                if (estado != "1")
                {
                    Bloquear();
                }
                else
                    habilitarBotones(true, false, true, true, true);
                ValidarEnfermeria();
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            nuevo = false;
            habilitarBotones(false, true, false, false, true);
            BloquearTabulador(true);
        }

        public void BloquearTabulador(bool estado)
        {
            txtSala.Enabled = estado;
            txtServicio.Enabled = estado;
            ultraTabPageControl1.Enabled = estado;
            ultraTabPageControl2.Enabled = estado;
            ultraTabPageControl3.Enabled = estado;
            ultraTabPageControl4.Enabled = estado;
            ultraTabPageControl5.Enabled = estado;
            ultraTabPageControl6.Enabled = estado;
        }

        private void ugbPaciente_Click(object sender, EventArgs e)
        {

        }

        private void frm_Protocolo_Load(object sender, EventArgs e)
        {

        }


        private void txtPreOperatorio_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    txtPreOperatorio.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void txtPostOperatorio_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    txtPostOperatorio.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void txtProyectada_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    txtProyectada.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void txtRealizada_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    txtRealizada.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void txtTipoAnestesia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTipoAnestesia.Text == "OTROS")
            {
                txtOtroAnestesia.Enabled = true;
            }
            else
            {
                txtOtroAnestesia.Enabled = false;
                txtOtroAnestesia.Text = "";
            }
        }
        NegQuirofano Quirofano = new NegQuirofano();
        public void CargarEspCirugia()
        {
            txtServicio.DataSource = Quirofano.MostrarEspCirugia();
            txtServicio.DisplayMember = "detalle";
            txtServicio.ValueMember = "id";
            txtServicio.SelectedIndex = -1;
        }
        public void CargarHabitacionesQuirofano()
        {
            //txtSala.DataSource = Quirofano.QuirofanoHabitacion();
            txtSala.DataSource = NegQuirofano.listadoHAbitaciones();
            txtSala.DisplayMember = "HABITACION";
            txtSala.ValueMember = "CODIGO";
            txtSala.SelectedIndex = -1;
        }

        private void btnF1Cirujano1_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtCirujano1.Text;

            if (ayuda.campoPadre.Text != string.Empty)
                txtCirujano1.Text = cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);

            btnPAyudante.Focus();
        }

        private void btnF1Cirujano2_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();
            string cadena = txtCirujano2.Text;

            if (ayuda.campoPadre.Text != string.Empty)
                txtCirujano2.Text += cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), cadena);

            btnPAyudante.Focus();
        }

        private void txtHoraTerm_Leave(object sender, EventArgs e)
        {
            if (txtHoraTerm.Text != "")
            {
                try
                {
                    TimeSpan Duracion;
                    DateTime horainicio = Convert.ToDateTime(txtHoraInicio.Text);
                    DateTime horafin = Convert.ToDateTime(txtHoraTerm.Text);
                    Duracion = horafin.Subtract(horainicio).Duration();
                    txtHoraTotal.Text = Convert.ToString(Duracion);
                }
                catch
                {
                    MessageBox.Show("Formato de hora incompleto o errado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHoraInicio.Focus();
                }

            }
        }

        private void txtHoraAnestecia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtTipoAnestesia.Focus();
            }
        }

        private void btn_mas_anestesias_Click(object sender, EventArgs e)
        {
            if (!txtOtroAnestesia.Enabled)
            {
                if (txtTipoAnestesia.Text != "")
                {
                    if (txt_tipos_anestesia.Text == "")
                    {
                        txt_tipos_anestesia.Text = txtTipoAnestesia.Text;
                    }
                    else
                    {
                        txt_tipos_anestesia.Text += " -- " + txtTipoAnestesia.Text;
                    }
                    txtTipoAnestesia.SelectedItem = -1;
                }
            }
            else
            {
                if (txt_tipos_anestesia.Text == "")
                {
                    txt_tipos_anestesia.Text = txtOtroAnestesia.Text;
                }
                else
                {
                    txt_tipos_anestesia.Text += " -- " + txtOtroAnestesia.Text;
                }
                txtOtroAnestesia.Text = "";
                txtOtroAnestesia.Enabled = false;
            }
        }

        private void rb_cultivo_no_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_cultivo_no.Checked)
            {
                txt_examen_cultivo.Text = "";
                txt_examen_cultivo.Enabled = false;
            }
        }

        private void rb_cultivo_si_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_cultivo_si.Checked)
            {
                txt_examen_cultivo.Text = "";
                txt_examen_cultivo.Enabled = true;
            }
        }

        private void rb_dren_no_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_dren_no.Checked)
            {
                txt_dren.Text = "";
                txt_dren.Enabled = false;
            }
        }

        private void rb_dren_si_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_dren_si.Checked)
            {
                txt_dren.Text = "";
                txt_dren.Enabled = true;
            }
        }

        private void btnAyudaPerfiles_MouseHover(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.Show("Perfiles de ayuda", btnAyudaPerfiles);
        }

        private void btnGrabaPerfil_MouseHover(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.Show("Grabar Perfil", btnGrabaPerfil);
        }

        private void btnGrabaPerfil_Click(object sender, EventArgs e)
        {
            errorProtocolo.Clear();
            lblPerfil.Visible = true;
            txtPerfil.Visible = true;
            if (txtPerfil.Text == "")
            {
                errorProtocolo.SetError(txtPerfil, "Escriba una descripcion para regsitrar el perfil");
                return;
            }
            His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("protocolo");
            usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            usuario.ShowDialog();
            if (!usuario.aceptado)
                return;
            MEDICOS med = NegMedicos.RecuperaMedicoIdUsuario(usuario.usuarioActual);
            if (med != null)
            {
                PERFILES_PROTOCOLO pp = new PERFILES_PROTOCOLO();
                pp.PP_NOMBRE_PERFIL = txtPerfil.Text.Trim();
                pp.MED_CODIGO = med.MED_CODIGO;
                pp.PP_DETALLE = rtbProcedimientos.Text.Trim();
                if (NegProtocoloOperatorio.registarPerfil(pp))
                {
                    MessageBox.Show("Perfil guardado correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblPerfil.Visible = false;
                    txtPerfil.Visible = false;
                    txtPerfil.Text = "";
                }
                else
                    MessageBox.Show("No se puedo guardar el perfil", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Este usuario no estas registrado como Medico", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAyudaPerfiles_Click(object sender, EventArgs e)
        {
            MEDICOS med = NegMedicos.RecuperaMedicoIdUsuario(Sesion.codUsuario);
            if (med != null)
            {
                frmAyudaIngElm frm = new frmAyudaIngElm(med.MED_CODIGO, "protocolo");
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
                if (frm.codigo != 0)
                {
                    PERFILES_PROTOCOLO ppro = NegProtocoloOperatorio.recuperaPerfilCodigo(frm.codigo);
                    rtbProcedimientos.Text = "";
                    rtbProcedimientos.Text = ppro.PP_DETALLE;
                }
            }
            else
                MessageBox.Show("Este usuario no estas registrado como Medico", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        class Elemento
        {
            public Int64 CODIGO { get; set; }
            public string PERFIL { get; set; }
            public string DESCRIPCION { get; set; }
        }
        private void txt_tipos_anestesia_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    txt_tipos_anestesia.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtPostOperatorio.Text = "";
            txtPostOperatorio.Text = txtPreOperatorio.Text;
            string[] lineas = txtPreOperatorio.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            if (lineas.Length == 3)
            {
                button1.Enabled = false;
                btncie2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //txtRealizada.Text = "";
            //txtRealizada.Text = txtProyectada.Text;
            dtgRealizada.Rows.Clear();
            try
            {
                for (int i = 0; i < dtgProyectada.Rows.Count - 1; i++)
                {

                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _cie = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _porcentaje = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell _observacion = new DataGridViewTextBoxCell();

                    _codigo.Value = "";
                    _cie.Value = dtgProyectada.Rows[i].Cells[1].Value.ToString().Trim();
                    _porcentaje.Value = dtgProyectada.Rows[i].Cells[2].Value.ToString().Trim();
                    _observacion.Value = dtgProyectada.Rows[i].Cells[3].Value.ToString().Trim();

                    fila.Cells.Add(_codigo);
                    fila.Cells.Add(_cie);
                    fila.Cells.Add(_porcentaje);
                    fila.Cells.Add(_observacion);

                    dtgRealizada.Rows.Add(fila);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void txtHoraTerm_Leave_1(object sender, EventArgs e)
        {
            if (txtHoraTerm.Text != "")
            {
                try
                {
                    TimeSpan Duracion;
                    DateTime horainicio = Convert.ToDateTime(txtHoraInicio.Text);
                    DateTime horafin = Convert.ToDateTime(txtHoraTerm.Text);
                    if (horainicio < horafin)
                    {
                        Duracion = horafin.Subtract(horainicio).Duration();
                        txtHoraTotal.Text = Convert.ToString(Duracion);
                    }
                    else
                    {
                        TimeSpan horainicio1 = TimeSpan.Parse(txtHoraInicio.Text);
                        TimeSpan horafin1 = TimeSpan.Parse(txtHoraTerm.Text);
                        TimeSpan diferenciaHastaMedianoche = new TimeSpan(24, 0, 0) - horainicio1;
                        TimeSpan diferenciaDesdeMedianoche = horafin1;
                        txtHoraTotal.Text = Convert.ToString(diferenciaHastaMedianoche + diferenciaDesdeMedianoche);

                    }
                }
                catch
                {
                    MessageBox.Show("Formato de hora incompleto o errado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHoraInicio.Focus();
                }

            }
        }
        public void limpiarPreOperatorio()
        {
            txtProyectada.Text = "";
            mskPorcentaje.Text = "";
            txtObservacion.Text = "";
        }
        public void limpiarPostOperatorio()
        {
            txtRealizada.Text = "";
            mskPorcentaje1.Text = "";
            txtObservacion1.Text = "";
        }
        private void btnOral_Click(object sender, EventArgs e)
        {
            errorProtocolo.Clear();
            if (txtProyectada.Text.Trim() == string.Empty)
            {
                AgregarError(txtProyectada);
                return;
            }
            try
            {
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _cie = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _porcentaje = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _observacion = new DataGridViewTextBoxCell();

                _codigo.Value = "";
                _cie.Value = txtProyectada.Text.Trim();
                _porcentaje.Value = mskPorcentaje.Text.Trim();
                _observacion.Value = txtObservacion.Text.Trim();

                fila.Cells.Add(_codigo);
                fila.Cells.Add(_cie);
                fila.Cells.Add(_porcentaje);
                fila.Cells.Add(_observacion);

                dtgProyectada.Rows.Add(fila);
                limpiarPreOperatorio();
                btntarifario.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnPostOperatorio_Click(object sender, EventArgs e)
        {
            errorProtocolo.Clear();
            if (txtRealizada.Text.Trim() == string.Empty)
            {
                AgregarError(txtRealizada);
                return;
            }
            try
            {
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell _codigo = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _cie = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _porcentaje = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell _observacion = new DataGridViewTextBoxCell();

                _codigo.Value = "";
                _cie.Value = txtRealizada.Text.Trim();
                _porcentaje.Value = mskPorcentaje1.Text.Trim();
                _observacion.Value = txtObservacion1.Text.Trim();

                fila.Cells.Add(_codigo);
                fila.Cells.Add(_cie);
                fila.Cells.Add(_porcentaje);
                fila.Cells.Add(_observacion);

                dtgRealizada.Rows.Add(fila);
                limpiarPostOperatorio();
                btntarifario2.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void mskPorcentaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtObservacion.Focus();
            }
        }

        private void txtObservacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                btnOral.Focus();
        }

        private void mskPorcentaje1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                txtObservacion1.Focus();
        }

        private void txtObservacion1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                btnPostOperatorio.Focus();
        }

        private void dtgProyectada_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dtgProyectada.CurrentRow != null)
                {
                    dtgProyectada.Rows.Remove(dtgProyectada.CurrentRow);
                }
            }
        }

        private void dtgRealizada_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dtgRealizada.CurrentRow != null)
                {
                    dtgRealizada.Rows.Remove(dtgRealizada.CurrentRow);
                }
            }
        }

        private void txtHoraInicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtHoraTerm.Focus();
            }
        }

        private void txtHoraTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtHoraAnestecia.Focus();
            }
        }

        private void txtInstrumentista_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                txtInstrumentista.Text = "";
        }

        private void txtCircundante_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                txtCircundante.Text = "";
        }

        private void tabulador_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            if (tabulador.Enabled == true)
            {
                if (tabulador.SelectedTab != tabulador.Tabs[0])
                {
                    if (dtgProyectada.Rows.Count <= 1)
                        MessageBox.Show("No ha registrado ningun tarifario proyectada", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (dtgRealizada.Rows.Count <= 1)
                        MessageBox.Show("No ha registrado ningun tarifario realizada", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnCargaDiagrama_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\"; // Directorio inicial
            //openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"; // Filtros de archivos
            //openFileDialog1.FilterIndex = 2;
            //openFileDialog1.RestoreDirectory = true;

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    // Obtener la ruta del archivo seleccionado
            //    lbl_name_archive.Text = openFileDialog1.FileName;
            //    lbl_name_archive.Visible = true;
            //    // Puedes hacer algo con el archivo seleccionado, como cargarlo en tu aplicación
            //    // Por ejemplo, mostrar el nombre del archivo en un TextBox
            //    //rtbDiagHispa.Text = lbl_name_archive.Text;
            //}
            Int64 entityId = Convert.ToInt64(atencion.EntityKey.EntityKeyValues[0].Value);
            ATENCIONES ate = NegAtenciones.RecuepraAtencionNumeroAtencion(entityId);
            PACIENTES pac = NegPacientes.recuperarPacientePorAtencion(entityId);
            string paciente = pac.PAC_NOMBRE1 + " " + pac.PAC_NOMBRE2;
            string apellido = pac.PAC_APELLIDO_PATERNO + " " + pac.PAC_APELLIDO_MATERNO;
            string numeroAtencion = ate.ATE_NUMERO_ATENCION;
            string numeroHistoria = pac.PAC_HISTORIA_CLINICA;
            int codPaciente = pac.PAC_CODIGO;
            int codAtencion = ate.ATE_CODIGO;
            DataTable x = NegDietetica.getDataTable("PathLocalHC");

            string strDirectorio = numeroHistoria + "//" + entityId + "//";
            if (x.Rows.Count > 0)
            {
                string strPathGeneral = x.Rows[0][0].ToString();

                var expArchivos = new GeneralApp.ControlesWinForms.ExploradorLocal(strDirectorio, strPathGeneral, paciente, codAtencion.ToString());
                expArchivos.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se encontro datos para cargar microfilm.", "HIS3000");
                return;
            }
        }

        private void rdb_material_si_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_material_si.Checked)
            {
                txt_uso_material.Enabled = true;
            }
            else
            {
                txt_uso_material.Enabled = false;
            }
        }

        private void rdb_material_no_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_material_no.Checked)
            {
                txt_uso_material.Enabled = false;
            }
            else
            {
                txt_uso_material.Enabled = true;
            }
        }

        private void txt_perdida_sanguinea_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumberDecimal(e, false);
        }

        private void txt_sangrado_aprox_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumberDecimal(e, false);
        }
    }
}
