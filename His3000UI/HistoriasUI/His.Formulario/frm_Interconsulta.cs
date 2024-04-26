using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Recursos;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using His.Parametros;
using His.DatosReportes;
using His.Entidades.Reportes;
using His.Entidades.Clases;

using His.Entidades;
using GeneralApp.ControlesWinForms;

namespace His.Formulario
{
    public partial class frm_Interconsulta : Form
    {
        public frm_Interconsulta()
        {
            InitializeComponent();


        }


        //cmb_especialidadCirugia.SelectedIndexChanged += cmb_especialidadCirugia_SelectedIndexChanged;
        //cmb_especialidadCirugia2.SelectedIndexChanged += cmb_especialidadCirugia2_SelectedIndexChanged;
        MaskedTextBox codMedico;
        MEDICOS med = null;
        MEDICOS medicointercosultado = new MEDICOS();
        int codigoMedico = 0;
        public int pac_codigo = 0;
        public int ate_codigo = 0;
        public int hin_codigo = 0;
        NegQuirofano Quirofano = new NegQuirofano();
        //cmb_especialidadCirugia.SelectedIndexChanged += cmb_especialidadCirugia_SelectedIndexChanged;
        //cmb_especialidadCirugia2.SelectedIndexChanged += cmb_especialidadCirugia2_SelectedIndexChanged;
        //int index;
        //, index2;
        string diagnostico = string.Empty;
        string codigoCIE = string.Empty;
        string diagnostico2 = string.Empty;
        string codigoCIE2 = string.Empty;
        string modo = "SAVE";
        MEDICOS medico = null;
        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        ASEGURADORAS_EMPRESAS aseguradora = null;
        HC_INTERCONSULTA interconsulta = null;
        List<HC_INTERCONSULTA_DIAGNOSTICO> diagnosticos4 = null;
        List<HC_INTERCONSULTA_DIAGNOSTICO> diagnosticos8 = null;
        USUARIOS usu = null;
        public bool _valido = false;
        string HIN_MEDICO_CODIGO = "";
        Int64 ID_USUARIO = 0;
        private void inicializar()
        {
            btnNuevo.Image = Archivo.imgBtnAdd2;
            btnGuardar.Image = Archivo.imgBtnGoneSave48;
            btnImprimir.Image = Archivo.imgBtnGonePrint48;
            btnSalir.Image = Archivo.imgBtnGoneExit48;
            pictureBox1.Image = Archivo.F1;
            pictureBox2.Image = Archivo.F1;
        }

        public frm_Interconsulta(int codigoAtencion)
        {
            InitializeComponent();
            tabControl1.Enabled = true;
            ate_codigo = codigoAtencion;
            if (codigoAtencion != 0)
                cargarAtencion(codigoAtencion);
            inicializar();
            refrescarSolicitudes();
            if (gridSol.Rows.Count > 1)
                HabilitarBotones(true, false, false, false);
            NegAtenciones atenciones = new NegAtenciones();
            string estado = atenciones.EstadoCuenta(Convert.ToString(codigoAtencion));
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
                        if (items.ID_ACCESO == 45000) // Mario Valencia 21/12/2023 // cambio en seguridades.
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
            cmb_especialidadCirugia.DataSource = Quirofano.MostrarEspCirugia();
            cmb_especialidadCirugia.DisplayMember = "detalle";
            cmb_especialidadCirugia.ValueMember = "id";
            cmb_especialidadCirugia.SelectedIndex = -1;
            cmb_especialidadCirugia.Text = "";
            cmb_especialidadCirugia2.DataSource = Quirofano.MostrarEspCirugia();
            cmb_especialidadCirugia2.DisplayMember = "detalle";
            cmb_especialidadCirugia2.ValueMember = "id";
            cmb_especialidadCirugia2.SelectedIndex = -1;
            cmb_especialidadCirugia2.Text = "";
            txt_serv_consultado.Visible = false;
            txt_serv_solicita.Visible = false;
            tabControl1.Enabled = false;

        }

        public void ValidarEnfermeria()
        {
            //Cambios Edgar 20210303 Requerimientos de la pasteur por Alex
            if (His.Entidades.Clases.Sesion.codDepartamento == 6 && !_valido)
            {
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                btnImprimir.Enabled = false;
            }
        }
        public void Bloquear()
        {
            btnGuardar.Enabled = false;
            btnNuevo.Enabled = false;
            //btnImprimir.Enabled = false;
            //ultraTabPageControl1.Enabled = false;
            //ultraTabPageControl2.Enabled = false;
            //ultraTabPageControl3.Enabled = false;
            //ultraTabPageControl4.Enabled = false;
            //ultraTabPageControl5.Enabled = false;
            //txt_pacHCL.Enabled = false;
            //txt_pacNombre.Enabled = false;
            //txt_sexo.Enabled = false;
        }
        private void cargarAtencion(int codAtencion)
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
                int servicio = 0;
                servicio = Convert.ToInt32(atencion.TIPO_INGRESOReference.EntityKey.EntityKeyValues[0].Value);
                if (servicio == 1)
                {
                    radioEmergencia.Checked = true;
                }
                if (servicio == 2)
                {
                    radioHospitalizacion.Checked = true;
                }
                if (servicio != 1 || servicio != 3)
                {
                    radioHospitalizacion.Checked = true;
                }

                HABITACIONES hab = new HABITACIONES();
                hab.hab_Codigo = atencion.HABITACIONES.hab_Codigo;
                txt_cama.Text = Convert.ToString(NegHabitaciones.RecuperarHabitacionId(hab.hab_Codigo).hab_Numero);
                cargarPaciente(atencion.PACIENTES.PAC_CODIGO);
                pac_codigo = atencion.PACIENTES.PAC_CODIGO;

                aseguradora = NegAseguradoras.recuperaAseguradoraPorAtencion(codAtencion);
                //lbl_aseguradora.Text = aseguradora.ASE_NOMBRE;
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                codigoMedico = medicos.FirstOrDefault(m => m.EntityKey == atencion.MEDICOSReference.EntityKey).MED_CODIGO;
                //if (codigoMedico != 0) Causa que no se pueda validar una Interconsulta con medico residente Mario Valencia
                    cargarMedico(codigoMedico);
                cargarHora();
                cargarInterconsulta(atencion.ATE_CODIGO);
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
            txt_profesional.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                medico.MED_APELLIDO_MATERNO.Trim() + " " +
                medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
            lbl_medico.Text = txt_profesional.Text;
            textBox7.Text = txt_profesional.Text;
        }

        private void cargarHora()
        {
            //DateTime dt = new DateTime();
            txt_fecha.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
            txt_hora.Text = System.DateTime.Now.ToString("HH:mm:ss");
            textBox9.Text = txt_fecha.Text;
            textBox8.Text = txt_hora.Text;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = true;
            HabilitarBotones(false, true, false, false);
            nuevo = true;
            modo = "SAVE";
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            //txt_cama.Text = string.Empty;
            txt_ccInterconsulta.Text = string.Empty;
            txt_cuadroclinico.Text = string.Empty;
            txt_destino.Text = string.Empty;
            txt_med_interconsultado.Text = string.Empty;
            txt_motivo.Text = string.Empty;
            txt_plan_diagnostico.Text = string.Empty;
            txt_plan_tratamiento.Text = string.Empty;
            txt_planes.Text = string.Empty;
            txt_resultados.Text = string.Empty;
            txt_resumen.Text = string.Empty;
            txt_sala.Text = string.Empty;
            txt_serv_consultado.Text = string.Empty;
            txt_serv_solicita.Text = string.Empty;
            //txt_sexo.Text = string.Empty;
            dtg_4.Rows.Clear();
            dtg_8.Rows.Clear();
            txt_serv_consultado.Visible = false;
            txt_serv_solicita.Visible = false;
            txtCodMedInterconsultado.Text = "";
            txtCodMedInterconsultado.Enabled = false;
            txtRucInterconsultado.Text = "";
            HIN_MEDICO_CODIGO = "";
            ID_USUARIO = 0;
            //cmb_especialidadCirugia.
            //  cmb_especialidadCirugia2

        }
        public bool nuevo = false;
        private void CargarUltimaInterconsulta()
        {
            try
            {
                //ATENCIONES attecion = NegAtenciones.RecuepraAtencionNumeroAtencion(42429);
                interconsulta = NegInterconsulta.Ultima(ate_codigo, hin_codigo); 
                if (interconsulta != null)
                {
                    //txt_cama.Text = Convert.ToString(interconsulta.HIN_CAMA);
                    txt_ccInterconsulta.Text = interconsulta.HIN_CUADRO_INTERCONSULTA;
                    txt_cuadroclinico.Text = interconsulta.HIN_CUADRO_CLINICO;
                    txt_motivo.Text = interconsulta.HIN_DESCRIPCION_MOTIVO;
                    txt_destino.Text = interconsulta.HIN_ESTABLECIMIENTO_DESTINO;
                    txt_med_interconsultado.Text = interconsulta.HIN_MED_INTERCONSULTADO;
                    txtCodMedInterconsultado.Text = interconsulta.HIN_MEDICO_INTERCONSULTADO.ToString();
                    HIN_MEDICO_CODIGO = interconsulta.HIN_MEDICO_CODIGO;
                    ID_USUARIO = (long)interconsulta.ID_USUARIO;
                    txt_plan_diagnostico.Text = interconsulta.HIN_PLAN_DIAGNOSTICO;
                    txt_plan_tratamiento.Text = interconsulta.HIN_PLAN_TRATAMIENTO;
                    txt_resultados.Text = interconsulta.HIN_RESULTADO_EXAMENES;
                    txt_resumen.Text = interconsulta.HIN_RESUMEN_CRITERIO;
                    txt_sala.Text = interconsulta.HIN_SALA;

                    //txt_serv_consultado.Text = interconsulta.HIN_SERV_CONSULTADO;

                    //.Text = interconsul&a.HIN_MEDICO_INTERCONSULTADO.ToString();

                    int index = cmb_especialidadCirugia.FindString(interconsulta.HIN_SERV_CONSULTADO);

                    cmb_especialidadCirugia.SelectedIndex = index;




                    txt_serv_solicita.Text = interconsulta.HIN_SERV_SOLICITA;

                    index2 = cmb_especialidadCirugia2.FindString(interconsulta.HIN_SERV_SOLICITA);

                    cmb_especialidadCirugia2.SelectedIndex = index2;

                    txt_planes.Text = interconsulta.HIN_PLANES_TERAPEUTICOS;

                    if (interconsulta.HIN_TIPO == "N")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    cargarDiagnosticos(interconsulta.HIN_CODIGO);
                    if (txt_profesional.Text == "" && textBox7.Text == "")
                    {
                        try
                        {
                            DataTable Tabla = NegInterconsulta.RecuperarDatosInterconsulta(ate_codigo);
                            textBox9.Text = Convert.ToDateTime(Tabla.Rows[0]["FECHA"].ToString()).ToShortDateString();
                            textBox8.Text = Convert.ToDateTime(Tabla.Rows[0]["FECHA"].ToString()).ToShortTimeString();
                            foreach (DataRow item in Tabla.Rows)
                            {
                                if (item[1].ToString() != "")
                                {
                                    txt_profesional.Text = item[1].ToString();
                                    textBox7.Text = item[1].ToString();
                                    string[] arr = txt_profesional.Text.Split(' ');

                                    medico = new MEDICOS();
                                    medico.MED_CODIGO = Convert.ToInt32(item[2].ToString());
                                    medico.MED_NOMBRE1 = arr[2].ToString();
                                    medico.MED_APELLIDO_PATERNO = arr[0].ToString();
                                    textBox9.Text = item["FECHA"].ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    HabilitarBotones(false, true, true, true);
                    nuevo = false;
                    tabControl1.Enabled = true;
                    bool estado = false;
                    //if (interconsulta.HIN_ESTADO != null)
                    //{
                    //    if (Convert.ToBoolean(interconsulta.HIN_ESTADO))
                    //        btnInterB.Visible = true;
                    //}
                    //else
                    //    btnInterB.Visible = false;
                }
                else
                {
                    HabilitarBotones(true, false, false, false);
                    tabControl1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar ultima atencion. Más detalle: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }


        }
        private void cargarInterconsulta(int codAtencion)
        {
            interconsulta = NegInterconsulta.recuperarInterconsulta(codAtencion);
            HC_INTERCONSULTA objInter = new HC_INTERCONSULTA();
            objInter = NegInterconsulta.UltimoCodigoAtencion(ate_codigo);
            if (objInter != null)
            {
                hin_codigo = objInter.HIN_CODIGO;

                if (hin_codigo > interconsulta.HIN_CODIGO)
                {
                    CargarUltimaInterconsulta();
                    ValidarCerrado();
                }
                else
                {
                    if (interconsulta != null)
                    {
                        //txt_cama.Text = Convert.ToString(interconsulta.HIN_CAMA);

                        txt_ccInterconsulta.Text = interconsulta.HIN_CUADRO_INTERCONSULTA;
                        txt_cuadroclinico.Text = interconsulta.HIN_CUADRO_CLINICO;
                        txt_motivo.Text = interconsulta.HIN_DESCRIPCION_MOTIVO;
                        txt_destino.Text = interconsulta.HIN_ESTABLECIMIENTO_DESTINO;
                        txt_med_interconsultado.Text = interconsulta.HIN_MED_INTERCONSULTADO;
                        txt_plan_diagnostico.Text = interconsulta.HIN_PLAN_DIAGNOSTICO;
                        txt_plan_tratamiento.Text = interconsulta.HIN_PLAN_TRATAMIENTO;
                        txt_resultados.Text = interconsulta.HIN_RESULTADO_EXAMENES;
                        txt_resumen.Text = interconsulta.HIN_RESUMEN_CRITERIO;
                        txt_sala.Text = interconsulta.HIN_SALA;
                        //      txt_serv_consultado.Text = interconsulta.HIN_SERV_CONSULTADO;
                        int index = cmb_especialidadCirugia.FindString(interconsulta.HIN_SERV_CONSULTADO);

                        cmb_especialidadCirugia.SelectedIndex = index;

                        index2 = cmb_especialidadCirugia2.FindString(interconsulta.HIN_SERV_SOLICITA);

                        cmb_especialidadCirugia2.SelectedIndex = index2;

                        txtCodMedInterconsultado.Text = interconsulta.HIN_MEDICO_INTERCONSULTADO.ToString();

                        //txt_serv_solicita.Text = interconsulta.HIN_SERV_SOLICITA;
                        //txt_planes.Text = interconsulta.HIN_PLANES_TERAPEUTICOS;

                        if (interconsulta.HIN_TIPO == "N")
                            radioButton1.Checked = true;
                        else
                            radioButton2.Checked = true;
                        cargarDiagnosticos(interconsulta.HIN_CODIGO);
                        DataTable Tabla = NegInterconsulta.RecuperarDatosInterconsulta(codAtencion);
                        if (Tabla.Rows.Count > 0)
                        {
                            if (Tabla.Rows[0]["FECHA"].ToString() != "")
                            {
                                textBox9.Text = Convert.ToDateTime(Tabla.Rows[0]["FECHA"].ToString()).ToShortDateString();
                                textBox8.Text = Convert.ToDateTime(Tabla.Rows[0]["FECHA"].ToString()).ToShortTimeString();
                            }

                        }
                        if (txt_profesional.Text == "" && textBox7.Text == "")
                        {
                            try
                            {
                                //DataTable Tabla = NegInterconsulta.RecuperarDatosInterconsulta(codAtencion);
                                //textBox9.Text = Convert.ToDateTime(Tabla.Rows[0]["FECHA"].ToString()).ToShortDateString();
                                //textBox8.Text = Convert.ToDateTime(Tabla.Rows[0]["FECHA"].ToString()).ToShortTimeString();
                                foreach (DataRow item in Tabla.Rows)
                                {
                                    if (item[1].ToString() != "")
                                    {
                                        txt_profesional.Text = item[1].ToString();
                                        textBox7.Text = item[1].ToString();
                                        string[] arr = txt_profesional.Text.Split(' ');

                                        medico = new MEDICOS();
                                        medico.MED_CODIGO = Convert.ToInt32(item[2].ToString());
                                        medico.MED_NOMBRE1 = arr[2].ToString();
                                        medico.MED_APELLIDO_PATERNO = arr[0].ToString();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        HabilitarBotones(true, true, true, true);
                        tabControl1.Enabled = true;
                        hin_codigo = interconsulta.HIN_CODIGO;
                        DataTable Recover = NegInterconsulta.RecoverDataInterconsulta(hin_codigo);
                        interconsu_id = Recover.Rows[0]["HIN_INTERCONSU_ID"].ToString();
                        ValidarCerrado();
                    }
                    else
                    {
                        HabilitarBotones(true, false, false, false);
                        tabControl1.Enabled = false;
                    }
                    //interconsulta.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    //interconsulta.PACIENTESReference.EntityKey = paciente.EntityKey;
                    //interconsulta.ATENCIONESReference.EntityKey = atencion.EntityKey;
                }
            }
        }

        private void cargarDiagnosticos(int codInterconsulta)
        {
            List<HC_INTERCONSULTA_DIAGNOSTICO> diagnosticos4 = NegInterconsulta.recuperarDiagnosticosIntercIng(codInterconsulta);
            if (diagnosticos4 != null)
            {
                foreach (HC_INTERCONSULTA_DIAGNOSTICO diag in diagnosticos4)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell2 = new DataGridViewTextBoxCell();
                    txtcell.Value = diag.HID_DIAGNOSTICO;
                    txtcell2.Value = diag.CIE_CODIGO;
                    fila.Cells.Add(txtcell);
                    fila.Cells.Add(txtcell2);
                    DataGridViewCheckBoxCell c1 = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell c2 = new DataGridViewCheckBoxCell();
                    if (diag.HID_ESTADO.Value)
                    {
                        c1.Value = true;
                        c2.Value = false;
                    }
                    else
                    {
                        c1.Value = false;
                        c2.Value = true;
                    }
                    fila.Cells.Add(c1);
                    fila.Cells.Add(c2);
                    dtg_4.Rows.Add(fila);
                    index2++;
                }
            }

            List<HC_INTERCONSULTA_DIAGNOSTICO> diagnosticos8 = NegInterconsulta.recuperarDiagnosticosIntercEgre(codInterconsulta);
            if (diagnosticos8 != null)
            {
                foreach (HC_INTERCONSULTA_DIAGNOSTICO diag in diagnosticos8)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell txtcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell txtcell2 = new DataGridViewTextBoxCell();
                    txtcell.Value = diag.HID_DIAGNOSTICO;
                    txtcell2.Value = diag.CIE_CODIGO;
                    fila.Cells.Add(txtcell);
                    fila.Cells.Add(txtcell2);
                    DataGridViewCheckBoxCell c1 = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell c2 = new DataGridViewCheckBoxCell();
                    if (diag.HID_ESTADO.Value)
                    {
                        c1.Value = true;
                        c2.Value = false;
                    }
                    else
                    {
                        c1.Value = false;
                        c2.Value = true;
                    }
                    fila.Cells.Add(c1);
                    fila.Cells.Add(c2);
                    dtg_8.Rows.Add(fila);
                    index3++;
                }
            }
        }


        //private void actualizarDiagnosticos(int codEpicrisis)
        //{
        //    //INGRESOS
        //    List<HC_EPICRISIS_DIAGNOSTICO> diagnosticosIngreso = NegEpicrisis.recuperarDiagnosticosEpicrisisIngreso(codEpicrisis);
        //    if (diagnosticosIngreso != null)
        //    {
        //        int cont = 0;
        //        foreach (HC_EPICRISIS_DIAGNOSTICO diag in diagnosticosIngreso)
        //        {
        //            DataGridViewRow fila = dtg_DIngreso.Rows[cont];
        //            if (fila.Cells[1].Value != null)
        //                diag.CIE_CODIGO = fila.Cells[1].Value.ToString();
        //            else
        //                diag.CIE_CODIGO = "";
        //            if ((bool)fila.Cells[2].Value)
        //                diag.HED_ESTADO = true;
        //            else
        //                diag.HED_ESTADO = false;
        //            diag.HED_DESCRIPCION = fila.Cells[0].Value.ToString();
        //            diag.HC_EPICRISISReference.EntityKey = epicrisis.EntityKey;
        //            diag.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
        //            diag.HED_TIPO = "I";
        //            NegEpicrisis.ActualizarDiagnostico(diag);
        //            cont++;
        //        }
        //    }
        //}

        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");
        }
        private bool validarFormulario()
        {
            bool flag = false;
            error.Clear();
            if (txt_cama.Text.ToString() == string.Empty)
            {
                AgregarError(txt_cama);
                flag = true;
            }
            //if (txt_ccInterconsulta.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_ccInterconsulta);
            //    flag = true;
            //}
            if (txt_cuadroclinico.Text.ToString() == string.Empty)
            {
                AgregarError(txt_cuadroclinico);
                flag = true;
            }
            //if (txt_destino.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_destino);
            //    flag = true;
            //}

            if (txt_motivo.Text.ToString() == string.Empty)
            {
                AgregarError(txt_motivo);
                flag = true;
            }
            //if (txt_plan_diagnostico.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_plan_diagnostico);
            //    flag = true;
            //}
            //if (txt_plan_tratamiento.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_plan_tratamiento);
            //    flag = true;
            //}
            if (txt_planes.Text.ToString() == string.Empty)
            {
                AgregarError(txt_planes);
                flag = true;
            }
            if (txt_profesional.Text.ToString() == string.Empty)
            {
                AgregarError(txt_profesional);
                flag = true;
            }
            if (txt_resultados.Text.ToString() == string.Empty)
            {
                AgregarError(txt_resultados);
                flag = true;
            }
            //if (txt_resumen.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_resumen);
            //    flag = true;
            //}
            //if (txt_sala.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_sala);
            //    flag = true;
            //}
            if (cmb_especialidadCirugia.Text.ToString() == string.Empty)
            {
                AgregarError(cmb_especialidadCirugia);
                flag = true;
            }
            if (cmb_especialidadCirugia2.Text.ToString() == string.Empty)
            {
                AgregarError(cmb_especialidadCirugia2);
                flag = true;
            }
            if (dtg_4.Rows.Count == 1)
            {
                AgregarError(dtg_4);
                flag = true;
            }
            foreach (DataGridViewRow fila in dtg_4.Rows)
            {
                DataGridViewCheckBoxCell txtcell = (DataGridViewCheckBoxCell)this.dtg_4.Rows[fila.Index].Cells[2];
                DataGridViewCheckBoxCell txtcell2 = (DataGridViewCheckBoxCell)this.dtg_4.Rows[fila.Index].Cells[3];
                DataGridViewTextBoxCell caja = (DataGridViewTextBoxCell)this.dtg_4.Rows[fila.Index].Cells[0];
                if ((txtcell.Value == null) && (txtcell2.Value == null) && (caja.Value != null))
                {
                    AgregarError(dtg_4);
                    flag = true;
                }
            }


            //if (dtg_8.Rows.Count == 1)
            //{
            //    AgregarError(dtg_8);
            //    flag = true;
            //}
            foreach (DataGridViewRow fila in dtg_8.Rows)
            {
                DataGridViewCheckBoxCell txtcell = (DataGridViewCheckBoxCell)this.dtg_8.Rows[fila.Index].Cells[2];
                DataGridViewCheckBoxCell txtcell2 = (DataGridViewCheckBoxCell)this.dtg_8.Rows[fila.Index].Cells[3];
                DataGridViewTextBoxCell caja = (DataGridViewTextBoxCell)this.dtg_8.Rows[fila.Index].Cells[0];
                if ((txtcell.Value == null) && (txtcell2.Value == null) && (caja.Value != null))
                {
                    AgregarError(dtg_8);
                    flag = true;
                }
            }
            return flag;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!validarFormulario())
            {
                try
                {
                    if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 23 || Sesion.codDepartamento == 34)
                    {
                        int Cambio = 0;
                        His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                        usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        usuario.ShowDialog();
                        if (!usuario.aceptado)
                            return;
                        medico = NegMedicos.recuperarMedicoID_Usuario(usuario.usuarioActual);

                        if (interconsulta == null || nuevo == true)
                            interconsulta = new HC_INTERCONSULTA();
                        else
                        {
                            modo = "UPDATE";
                        }

                        if (medico != null)
                            HIN_MEDICO_CODIGO = Convert.ToString(medico.MED_CODIGO);
                        else
                            ID_USUARIO = usuario.usuarioActual;
                        //interconsulta.HIN_CODIGO = hin_codigo;
                        interconsulta.HIN_CAMA = 0;
                        interconsulta.HIN_CUADRO_CLINICO = txt_cuadroclinico.Text;
                        interconsulta.HIN_CUADRO_INTERCONSULTA = txt_ccInterconsulta.Text;
                        interconsulta.HIN_DESCRIPCION_MOTIVO = txt_motivo.Text;
                        interconsulta.HIN_ESTABLECIMIENTO_DESTINO = txt_destino.Text;
                        interconsulta.HIN_MED_INTERCONSULTADO = txt_med_interconsultado.Text;
                        interconsulta.HIN_PLAN_DIAGNOSTICO = txt_plan_diagnostico.Text;
                        interconsulta.HIN_PLAN_TRATAMIENTO = txt_plan_tratamiento.Text;
                        interconsulta.HIN_PLANES_TERAPEUTICOS = txt_planes.Text;
                        interconsulta.HIN_RESULTADO_EXAMENES = txt_resultados.Text;
                        interconsulta.HIN_RESUMEN_CRITERIO = txt_resumen.Text;
                        interconsulta.HIN_SALA = txt_sala.Text;
                        interconsulta.HIN_SERV_CONSULTADO = cmb_especialidadCirugia.Text;
                        interconsulta.HIN_SERV_SOLICITA = cmb_especialidadCirugia2.Text;
                        if (int.TryParse(txtCodMedInterconsultado.Text, out Cambio))
                        {
                            interconsulta.HIN_MEDICO_INTERCONSULTADO = Cambio;
                        }
                        else
                        {
                            interconsulta.HIN_MEDICO_INTERCONSULTADO = 1;
                        }
                        //interconsulta.HIN_SERV_CONSULTADO = txt_serv_consultado.Text;
                        //interconsulta.HIN_SERV_SOLICITA = txt_serv_solicita.Text;
                        if (radioButton1.Checked)
                            interconsulta.HIN_TIPO = "N";
                        else
                            interconsulta.HIN_TIPO = "U";
                        interconsulta.ID_USUARIO = (short)usuario.usuarioActual;
                        interconsulta.PACIENTESReference.EntityKey = paciente.EntityKey;
                        interconsulta.ATENCIONESReference.EntityKey = atencion.EntityKey;
                        interconsulta.HIN_ESTADO = null;
                        if (modo == "SAVE")
                        {
                            interconsulta.HIN_CODIGO = NegInterconsulta.ultimoCodigo() + 1;
                            int hin_codigo = NegInterconsulta.ultimoCodigo() + 1;
                            NegInterconsulta.crearInterconsulta(interconsulta);
                            if (medico == null)
                                NegInterconsulta.GuardarCamaInterconsulta(hin_codigo, txt_cama.Text, txt_profesional.Text, "", interconsu_id);
                            else
                                NegInterconsulta.GuardarCamaInterconsulta(hin_codigo, txt_cama.Text, txt_profesional.Text, medico.MED_CODIGO.ToString(), interconsu_id);
                            guardarDiagnosticos();
                            MessageBox.Show("Registro Guardado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            if (modo == "UPDATE")
                        {
                            NegInterconsulta.actualizarInterconsulta(interconsulta);
                            if (medico == null)
                                NegInterconsulta.GuardarCamaInterconsulta(hin_codigo, txt_cama.Text, txt_profesional.Text, "", interconsu_id);
                            else
                                NegInterconsulta.GuardarCamaInterconsulta(interconsulta.HIN_CODIGO, txt_cama.Text, txt_profesional.Text, medico.MED_CODIGO.ToString(), interconsu_id);
                            //actualizarDiagnosticos4(interconsulta.HIN_CODIGO);
                            //actualizarDiagnosticos8(interconsulta.HIN_CODIGO);
                            borrarDiagnosticos(interconsulta.HIN_CODIGO);
                            MessageBox.Show("Registro Actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //cargarMedicosTratantes();
                        }

                        refrescarSolicitudes();
                        //NegInterconsulta.crearInterconsulta(interconsulta);
                        //guardarDiagnosticos();
                        //MessageBox.Show("Registro Guardado", "INTERCONSULTA", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        //if (txt_med_interconsultado.Text.ToString() == string.Empty)
                        //{
                        //    if (MessageBox.Show("¡Medico no ingresado!. ¿El médico es de llamada?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                        //         == DialogResult.Yes)
                        //    {

                        //        imprimirReporte("reporte");

                        //    }

                        //}

                        HabilitarBotones(true, false, false, false);
                        limpiarCampos();

                        tabControl1.Enabled = false;

                        if (ate_codigo != 0)
                        {

                        }

                    }
                    else
                    {
                        MessageBox.Show("No tiene permiso para modificar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Datos Incompletos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void borrarDiagnosticos(int codInterc)
        {
            NegInterconsulta.BorraDiagnosticosIntercIng(codInterc);
            guardarDiagnosticos();
        }

        private void habilitaGrid(bool grp)
        {
            groupBox1.Enabled = grp;
            groupBox2.Enabled = grp;
            groupBox5.Enabled = grp;
            groupBox6.Enabled = grp;
            groupBox7.Enabled = grp;
        }

        private void actualizarDiagnosticos4(int codInterc)
        {
            //INGRESOS4
            List<HC_INTERCONSULTA_DIAGNOSTICO> diagnosticosUno = NegInterconsulta.recuperarDiagnosticosIntercIng(codInterc);
            if (diagnosticosUno != null)
            {
                int cont = 0;
                foreach (HC_INTERCONSULTA_DIAGNOSTICO diag in diagnosticosUno)
                {
                    DataGridViewRow fila = dtg_4.Rows[cont];
                    if (fila.Cells[1].Value != null)
                        diag.CIE_CODIGO = fila.Cells[1].Value.ToString();
                    else
                        diag.CIE_CODIGO = "";
                    if ((bool)fila.Cells[2].Value)
                        diag.HID_ESTADO = true;
                    else
                        diag.HID_ESTADO = false;
                    diag.HID_DIAGNOSTICO = fila.Cells[0].Value.ToString();
                    diag.HC_INTERCONSULTAReference.EntityKey = interconsulta.EntityKey;
                    diag.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    diag.HID_TIPO = "I";
                    NegInterconsulta.actualizarDiagnostico(diag);
                    cont++;
                }
            }
        }


        private void actualizarDiagnosticos8(int codInterc)
        {
            //ENGRESOS8
            List<HC_INTERCONSULTA_DIAGNOSTICO> diagnosticosUno = NegInterconsulta.recuperarDiagnosticosIntercEgre(codInterc);
            if (diagnosticosUno != null)
            {
                int cont = 0;
                foreach (HC_INTERCONSULTA_DIAGNOSTICO diag in diagnosticosUno)
                {
                    DataGridViewRow fila = dtg_4.Rows[cont];
                    if (fila.Cells[1].Value != null)
                        diag.CIE_CODIGO = fila.Cells[1].Value.ToString();
                    else
                        diag.CIE_CODIGO = "";
                    if ((bool)fila.Cells[2].Value)
                        diag.HID_ESTADO = true;
                    else
                        diag.HID_ESTADO = false;
                    diag.HID_DIAGNOSTICO = fila.Cells[0].Value.ToString();
                    diag.HC_INTERCONSULTAReference.EntityKey = interconsulta.EntityKey;
                    diag.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    diag.HID_TIPO = "E";
                    NegInterconsulta.actualizarDiagnostico(diag);
                    cont++;
                }
            }
        }

        private void guardarDiagnosticos()
        {
            foreach (DataGridViewRow fila in dtg_4.Rows)
            {
                if (fila.Cells[1].RowIndex < dtg_4.Rows.Count - 1)
                {
                    HC_INTERCONSULTA_DIAGNOSTICO detalle = new HC_INTERCONSULTA_DIAGNOSTICO();
                    if (fila.Cells[1].Value != null)
                        detalle.CIE_CODIGO = fila.Cells[1].Value.ToString();
                    else
                        detalle.CIE_CODIGO = "";

                    if (fila.Cells[2].Value != null)
                        if (Convert.ToBoolean(fila.Cells[2].Value))
                            detalle.HID_ESTADO = true;
                        else
                            detalle.HID_ESTADO = false;
                    else
                        detalle.HID_ESTADO = false;

                    detalle.HID_DIAGNOSTICO = fila.Cells[0].Value.ToString();
                    detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    detalle.HC_INTERCONSULTAReference.EntityKey = interconsulta.EntityKey;
                    detalle.HID_TIPO = "I";
                    detalle.HID_CODIGO = NegInterconsulta.ultimoCodigoDiagnostico() + 1;
                    NegInterconsulta.crearInterconsultaDiagnosticos(detalle);
                }
            }
            foreach (DataGridViewRow fila in dtg_8.Rows)
            {
                if (fila.Cells[1].RowIndex < dtg_8.Rows.Count - 1)
                {
                    HC_INTERCONSULTA_DIAGNOSTICO detalle = new HC_INTERCONSULTA_DIAGNOSTICO();
                    if (fila.Cells[1].Value != null)
                        detalle.CIE_CODIGO = fila.Cells[1].Value.ToString();
                    else
                        detalle.CIE_CODIGO = "";

                    if (fila.Cells[2].Value != null)
                        if ((bool)fila.Cells[2].Value)
                            detalle.HID_ESTADO = true;
                        else
                            detalle.HID_ESTADO = false;
                    else
                        detalle.HID_ESTADO = false;


                    detalle.HID_DIAGNOSTICO = fila.Cells[0].Value.ToString();
                    detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    detalle.HC_INTERCONSULTAReference.EntityKey = interconsulta.EntityKey;
                    detalle.HID_TIPO = "E";
                    detalle.HID_CODIGO = NegInterconsulta.ultimoCodigoDiagnostico() + 1;
                    NegInterconsulta.crearInterconsultaDiagnosticos(detalle);
                }
            }
        }
        int index2 = 0;
        int index3 = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if ((diagnostico != "") && (diagnostico != null))
            //{

            //    DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_4.Rows[index2].Cells[0];
            //    DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_4.Rows[index2].Cells[1];
            //    if (diagnostico != null)
            //    {
            //        txtcell.Value = diagnostico;
            //        txtcell2.Value = codigoCIE;
            //        diagnostico = "";
            //    }


            //    index2++;
            //}
            //if ((diagnostico2 != "") && (diagnostico2 != null))
            //{

            //    DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_8.Rows[index3].Cells[0];
            //    DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_8.Rows[index3].Cells[1];
            //    if (diagnostico2 != null)
            //    {
            //        txtcell.Value = diagnostico2;
            //        txtcell2.Value = codigoCIE2;
            //        diagnostico2 = "";
            //    }
            //    index3++;
            //}
        }

        private void dtg_4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                BuscaCIEDTG4();
            }
        }

        private void BuscaCIEDTG4()
        {
            //if (dtg_4.CurrentRow != null)
            //{
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            diagnostico = busqueda.resultado;
            codigoCIE = busqueda.codigo;

            if ((diagnostico != "") && (diagnostico != null))
            {
                if (dtg_4.Rows.Count < 7)
                {
                    if (dtg_4.Rows.Count > 1)
                    {
                        for (int i = 0; i < dtg_4.Rows.Count - 1; i++)
                        {
                            if (busqueda.codigo == dtg_4.Rows[i].Cells[1].Value.ToString())
                            {
                                MessageBox.Show("El procedimiento ya ha sido agregado.\r\nIntente con uno diferente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                    //DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_4.CurrentRow.Cells[0]; // se comenta por el proceso y genere una fila de manera automatica // Mario Valencia //11-03-2024
                    //DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_4.CurrentRow.Cells[1];
                    //DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_4.CurrentRow.Cells[2];


                    //if (diagnostico != null)
                    //{
                    //    txtcell.Value = diagnostico;
                    //    txtcell2.Value = codigoCIE;
                    //    //chkpres.Value = true;
                    //    diagnostico = "";
                    //    //dtg_4_CellContentClick(object, dtg_4);

                    //}
                    DataGridViewRow fila = new DataGridViewRow();

                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell CodigoPcell = new DataGridViewTextBoxCell();

                    DataGridViewCheckBoxCell Check1Cell = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell Check2Cell = new DataGridViewCheckBoxCell();

                    codigoCell.Value = null;
                    CodigoPcell.Value = busqueda.resultado;
                    textcell.Value = busqueda.codigo;

                    Check1Cell = null;
                    Check2Cell = null;

                    fila.Cells.Add(CodigoPcell);
                    fila.Cells.Add(textcell);

                    dtg_4.Rows.Add(fila);
                    index2++;
                }
                else
                    MessageBox.Show("No puede agregar mas de 6 procedimientos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //}
            //else
            //{
            //    MessageBox.Show("Seleccione una fila antes de ingresar el diagnóstico.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}

        }

        private void dtg_8_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                BuscaCIEDTG8();
            }
        }

        private void BuscaCIEDTG8()
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            diagnostico2 = busqueda.resultado;
            codigoCIE2 = busqueda.codigo;

            if ((diagnostico2 != "") && (diagnostico2 != null))
            {
                if (dtg_8.Rows.Count < 7)
                {
                    if (dtg_8.Rows.Count > 1)
                    {
                        for (int i = 0; i < dtg_8.Rows.Count - 1; i++)
                        {
                            if (busqueda.codigo == dtg_8.Rows[i].Cells[1].Value.ToString())
                            {
                                MessageBox.Show("El procedimiento ya ha sido agregado.\r\nIntente con uno diferente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                    DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_8.CurrentRow.Cells[0];
                    DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_8.CurrentRow.Cells[1];

                    if (diagnostico2 != null)
                    {
                        txtcell.Value = diagnostico2;
                        txtcell2.Value = codigoCIE2;
                        diagnostico2 = "";
                    }
                    index3++;
                }
                else
                    MessageBox.Show("No puede agregar mas de 6 procedimientos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimirReporte("reporte");
        }

        public int calcularEdad(DateTime fechaNac)
        {
            DateTime fechaActual = DateTime.Now;
            TimeSpan diferencia = fechaActual - fechaNac;

            // Calcula la edad en años
            int edad = (int)(diferencia.TotalDays / 365.25);

            return edad;
        }

        private void imprimirReporte(string accion)
        {
            try
            {

                NegCertificadoMedico med = new NegCertificadoMedico();
                ReporteInterconsulta reporte = new ReporteInterconsulta();
                MEDICOS medic = null;
                if (HIN_MEDICO_CODIGO != "")
                    medic = NegMedicos.recuperarMedico(int.Parse(HIN_MEDICO_CODIGO));

                usu = NegUsuarios.RecuperarUsuarioID(ID_USUARIO);
                if (pac_codigo != 0)
                    paciente = NegPacientes.RecuperarPacienteID(pac_codigo);

                atencion = NegAtenciones.RecuperarAtencionID(ate_codigo);
                paciente = NegPacientes.RecuperarPacienteID(atencion.PACIENTES.PAC_CODIGO);
                DataTable IO = NegDietetica.getDataTable("EMPRESA");
                DSInterconsulta ds = new DSInterconsulta();
                DataRow dr;
                dr = ds.Tables["InterconsultaA"].NewRow();

                dr["establecimiento"] = His.Entidades.Clases.Sesion.nomEmpresa;
                dr["path"] = IO.Rows[0]["EMP_PATHIMAGEN"].ToString();
                dr["nombre1"] = paciente.PAC_NOMBRE1;
                dr["nombre2"] = paciente.PAC_NOMBRE2;
                dr["apellido1"] = paciente.PAC_APELLIDO_PATERNO;
                dr["apellido2"] = paciente.PAC_APELLIDO_MATERNO;
                dr["sexo"] = txt_sexo.Text;
                dr["edad"] = calcularEdad(Convert.ToDateTime(paciente.PAC_FECHA_NACIMIENTO));
                dr["cuadroclinico"] = txt_cuadroclinico.Text;
                dr["resultado"] = txt_resultados.Text;
                dr["plan"] = txt_planes.Text;
                dr["cama"] = txt_sala.Text.Trim();
                dr["sala"] = txt_cama.Text.Trim();
                dr["urgente"] = radioButton2.Checked == true ? "X" : "";
                dr["normal"] = radioButton1.Checked == true ? "X" : "";
                if (!NegParametros.ParametroFormularios())
                    dr["hc"] = paciente.PAC_HISTORIA_CLINICA.Trim() + "-" + atencion.ATE_NUMERO_ATENCION.Trim();
                else
                    dr["hc"] = paciente.PAC_IDENTIFICACION;

                dr["sconsultado"] = cmb_especialidadCirugia.Text;
                dr["ssolicitado"] = cmb_especialidadCirugia2.Text + "- (Dr/a. " + txt_med_interconsultado.Text + ")";
                dr["semergencia"] = radioEmergencia.Checked == true ? "X" : "";
                dr["scexterna"] = radioCExterna.Checked == true ? "X" : "";
                dr["shospitalizacion"] = radioHospitalizacion.Checked == true ? "X" : "";
                dr["descripcion"] = txt_motivo.Text;
                dr["ci"] = paciente.PAC_IDENTIFICACION;
                string[] medicointer;
                if (txt_med_interconsultado.Text != "")
                {
                    medicointer = txt_med_interconsultado.Text.Split(' ');
                }
                else
                {
                    string medico = "NA NA NA NA";
                    medicointer = medico.Split(' ');
                }

                //reporte.for_med_int = "Dr/a. " + medicointer[0] + " " + medicointer[2];
                dr["fecha"] = textBox9.Text;
                dr["hora"] = textBox8.Text;
                if (medic != null)
                {
                    dr["mednombre1"] = "Dr/a. " + medic.MED_NOMBRE1;
                    dr["medapellido1"] = medic.MED_APELLIDO_PATERNO;
                    dr["medapellido2"] = medic.MED_APELLIDO_MATERNO;

                    if (medic.MED_RUC.Length > 10)
                        dr["medruc"] = medic.MED_RUC.Substring(0, 10);
                    else
                        dr["medruc"] = medic.MED_RUC;
                }
                else
                {
                    dr["mednombre1"] = usu.NOMBRES;
                    dr["medapellido1"] = usu.APELLIDOS;
                    dr["medapellido2"] = "";

                    if (usu.IDENTIFICACION.Length > 10)
                        dr["medruc"] = usu.IDENTIFICACION.Substring(0, 10);
                    else
                        dr["medruc"] = usu.IDENTIFICACION;
                }

                dtg_4.Refresh();
                for (int i = 0; i < dtg_4.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        dr["d1c"] = dtg_4.Rows[0].Cells[0].Value.ToString();
                        dr["d1"] = (dtg_4.Rows[0].Cells[1].Value.ToString()).Replace("'", "´");
                        if (Convert.ToBoolean(dtg_4.Rows[i].Cells[2].Value)) //Convierte explicitamente a booleano para que pueda validar la casilla seleccionada A.Cabrera 2024/03/06
                            dr["d1p"] = dtg_4.Rows[0].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d1d"] = dtg_4.Rows[0].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 1)
                    {
                        dr["d2c"] = dtg_4.Rows[1].Cells[0].Value.ToString();
                        dr["d2"] = (dtg_4.Rows[1].Cells[1].Value.ToString()).Replace("'", "´");
                        if (Convert.ToBoolean(dtg_4.Rows[i].Cells[2].Value))
                            dr["d2p"] = dtg_4.Rows[1].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d2d"] = dtg_4.Rows[1].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 2)
                    {
                        dr["d3c"] = dtg_4.Rows[2].Cells[0].Value.ToString();
                        dr["d3"] = (dtg_4.Rows[2].Cells[1].Value.ToString()).Replace("'", "´");
                        if (Convert.ToBoolean(dtg_4.Rows[i].Cells[2].Value))
                            dr["d3p"] = dtg_4.Rows[2].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d3d"] = dtg_4.Rows[2].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 3)
                    {
                        dr["d4c"] = dtg_4.Rows[3].Cells[0].Value.ToString();
                        dr["d4"] = (dtg_4.Rows[3].Cells[1].Value.ToString()).Replace("'", "´");
                        if (Convert.ToBoolean(dtg_4.Rows[i].Cells[2].Value))
                            dr["d4p"] = dtg_4.Rows[3].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d4d"] = dtg_4.Rows[3].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 4)
                    {
                        dr["d5c"] = dtg_4.Rows[4].Cells[0].Value.ToString();
                        dr["d5"] = (dtg_4.Rows[4].Cells[1].Value.ToString()).Replace("'", "´");
                        if (Convert.ToBoolean(dtg_4.Rows[i].Cells[2].Value))
                            dr["d5p"] = dtg_4.Rows[4].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d5d"] = dtg_4.Rows[4].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 5)
                    {
                        dr["d6c"] = dtg_4.Rows[5].Cells[0].Value.ToString();
                        dr["d6"] = (dtg_4.Rows[5].Cells[1].Value.ToString()).Replace("'", "´");
                        if (Convert.ToBoolean(dtg_4.Rows[i].Cells[2].Value))
                            dr["d6p"] = dtg_4.Rows[5].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d6d"] = dtg_4.Rows[5].Cells[3].Value != null ? "X" : " ";
                    }

                }
                ds.Tables["InterconsultaA"].Rows.Add(dr);

                //ReportesHistoriaClinica reporteInterconsulta = new ReportesHistoriaClinica();
                //reporteInterconsulta.ingresarInterConsulta(reporte);
                if (accion.Equals("reporte"))
                {
                    //frmReportes ventana = new frmReportes(1, "interconsulta2");
                    //ventana.Show();
                    medicointercosultado = NegMedicos.MedicoNombreApellido(medicointer);
                    frmReportes ventana = new frmReportes(1, "interconsulta", medicointercosultado, ds);

                    ventana.Show();

                }
                else
                {
                    frmReportes ventana = new frmReportes(1, "interconsulta2");
                    CrearCarpetas_Srvidor("interconsulta2");
                    ventana = new frmReportes(1, "interconsulta");
                    CrearCarpetas_Srvidor("interconsulta");
                }
                //frmReportes ventana = new frmReportes(1, "interconsulta2");
                //ventana.Show();
                //ventana = new frmReportes(1, "interconsulta");
                //ventana.Show();
            }
            catch (Exception err)
            { MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            ////frmReportes rep = new frmReportes(interconsulta.HIN_CODIGO, "interconsulta");
            ////rep.Show();
            //rep = new frmReportes(interconsulta.HIN_CODIGO, "interconsulta2");
            //rep.Show();

        }
        private void imprimirMail(string accion)
        {
            try
            {
                NegCertificadoMedico med = new NegCertificadoMedico();
                ReporteInterconsulta reporte = new ReporteInterconsulta();
                MEDICOS medic = NegMedicos.recuperarMedico(int.Parse(HIN_MEDICO_CODIGO));

                if (pac_codigo != 0)
                    paciente = NegPacientes.RecuperarPacienteID(pac_codigo);

                atencion = NegAtenciones.RecuperarAtencionID(ate_codigo);
                paciente = NegPacientes.RecuperarPacienteID(atencion.PACIENTES.PAC_CODIGO);
                DataTable IO = NegDietetica.getDataTable("EMPRESA");
                DSInterconsulta ds = new DSInterconsulta();
                DataRow dr;
                dr = ds.Tables["InterconsultaA"].NewRow();
                dr["establecimiento"] = His.Entidades.Clases.Sesion.nomEmpresa;
                dr["path"] = IO.Rows[0]["EMP_PATHIMAGEN"].ToString();
                dr["nombre1"] = paciente.PAC_NOMBRE1;
                dr["nombre2"] = paciente.PAC_NOMBRE2;
                dr["apellido1"] = paciente.PAC_APELLIDO_PATERNO;
                dr["apellido2"] = paciente.PAC_APELLIDO_MATERNO;
                dr["sexo"] = txt_sexo.Text;
                dr["edad"] = calcularEdad(Convert.ToDateTime(paciente.PAC_FECHA_NACIMIENTO));
                dr["cuadroclinico"] = txt_cuadroclinico.Text;
                dr["resultado"] = txt_resultados.Text;
                dr["plan"] = txt_planes.Text;
                dr["cama"] = txt_sala.Text.Trim();
                dr["sala"] = txt_cama.Text.Trim();
                dr["urgente"] = radioButton2.Checked == true ? "X" : "";
                dr["normal"] = radioButton1.Checked == true ? "X" : "";
                if (!NegParametros.ParametroFormularios())
                    dr["hc"] = paciente.PAC_HISTORIA_CLINICA.Trim() + "-" + atencion.ATE_NUMERO_ATENCION.Trim();
                else
                    dr["hc"] = paciente.PAC_IDENTIFICACION;

                dr["sconsultado"] = cmb_especialidadCirugia.Text;
                dr["ssolicitado"] = cmb_especialidadCirugia2.Text + "- (Dr/a. " + txt_med_interconsultado.Text + ")";
                dr["semergencia"] = radioEmergencia.Checked == true ? "X" : "";
                dr["scexterna"] = radioCExterna.Checked == true ? "X" : "";
                dr["shospitalizacion"] = radioHospitalizacion.Checked == true ? "X" : "";
                dr["descripcion"] = txt_motivo.Text;
                dr["ci"] = paciente.PAC_IDENTIFICACION;
                string[] medicointer = txt_med_interconsultado.Text.Split(' ');

                //reporte.for_med_int = "Dr/a. " + medicointer[0] + " " + medicointer[2];
                dr["fecha"] = textBox9.Text;
                dr["hora"] = textBox8.Text;
                if (medic != null)
                {
                    dr["mednombre1"] = "Dr/a. " + medic.MED_NOMBRE1;
                    dr["medapellido1"] = medic.MED_APELLIDO_PATERNO;
                    dr["medapellido2"] = medic.MED_APELLIDO_MATERNO;

                    if (medic.MED_RUC.Length > 10)
                        dr["medruc"] = medic.MED_RUC.Substring(0, 10);
                    else
                        dr["medruc"] = medic.MED_RUC;
                }
                else
                {
                    dr["mednombre1"] = usu.NOMBRES;
                    dr["medapellido1"] = usu.APELLIDOS;
                    dr["medapellido2"] = "";

                    if (usu.IDENTIFICACION.Length > 10)
                        dr["medruc"] = usu.IDENTIFICACION.Substring(0, 10);
                    else
                        dr["medruc"] = usu.IDENTIFICACION;
                }

                dtg_4.Refresh();
                for (int i = 0; i < dtg_4.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        dr["d1c"] = dtg_4.Rows[0].Cells[0].Value.ToString();
                        dr["d1"] = (dtg_4.Rows[0].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_4.Rows[i].Cells[2].Value)
                            dr["d1p"] = dtg_4.Rows[0].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d1d"] = dtg_4.Rows[0].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 1)
                    {
                        dr["d2c"] = dtg_4.Rows[1].Cells[0].Value.ToString();
                        dr["d2"] = (dtg_4.Rows[1].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_4.Rows[i].Cells[2].Value)
                            dr["d2p"] = dtg_4.Rows[1].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d2d"] = dtg_4.Rows[1].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 2)
                    {
                        dr["d3c"] = dtg_4.Rows[2].Cells[0].Value.ToString();
                        dr["d3"] = (dtg_4.Rows[2].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_4.Rows[i].Cells[2].Value)
                            dr["d3p"] = dtg_4.Rows[2].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d3d"] = dtg_4.Rows[2].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 3)
                    {
                        dr["d4c"] = dtg_4.Rows[3].Cells[0].Value.ToString();
                        dr["d4"] = (dtg_4.Rows[3].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_4.Rows[i].Cells[2].Value)
                            dr["d4p"] = dtg_4.Rows[3].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d4d"] = dtg_4.Rows[3].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 4)
                    {
                        dr["d5c"] = dtg_4.Rows[4].Cells[0].Value.ToString();
                        dr["d5"] = (dtg_4.Rows[4].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_4.Rows[i].Cells[2].Value)
                            dr["d5p"] = dtg_4.Rows[4].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d5d"] = dtg_4.Rows[4].Cells[3].Value != null ? "X" : " ";
                    }
                    if (i == 5)
                    {
                        dr["d6c"] = dtg_4.Rows[5].Cells[0].Value.ToString();
                        dr["d6"] = (dtg_4.Rows[5].Cells[1].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_4.Rows[i].Cells[2].Value)
                            dr["d6p"] = dtg_4.Rows[5].Cells[2].Value != null ? "X" : " ";
                        else
                            dr["d6d"] = dtg_4.Rows[5].Cells[3].Value != null ? "X" : " ";
                    }

                }
                ds.Tables["InterconsultaA"].Rows.Add(dr);

                //ReportesHistoriaClinica reporteInterconsulta = new ReportesHistoriaClinica();
                //reporteInterconsulta.ingresarInterConsulta(reporte);
                if (accion.Equals("reporte"))
                {
                    //frmReportes ventana = new frmReportes(1, "interconsulta2");
                    //ventana.Show();
                    medicointercosultado = NegMedicos.MedicoNombreApellido(medicointer);
                    frmReportes ventana = new frmReportes(1, "mail", medicointercosultado, ds);

                    ventana.Show();

                }
                else
                {
                    frmReportes ventana = new frmReportes(1, "interconsulta2");
                    CrearCarpetas_Srvidor("interconsulta2");
                    ventana = new frmReportes(1, "interconsulta");
                    CrearCarpetas_Srvidor("interconsulta");
                }
                //frmReportes ventana = new frmReportes(1, "interconsulta2");
                //ventana.Show();
                //ventana = new frmReportes(1, "interconsulta");
                //ventana.Show();
            }
            catch (Exception err)
            { MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            ////frmReportes rep = new frmReportes(interconsulta.HIN_CODIGO, "interconsulta");
            ////rep.Show();
            //rep = new frmReportes(interconsulta.HIN_CODIGO, "interconsulta2");
            //rep.Show();

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

        private void btnMedico_Click(object sender, EventArgs e)
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos frm = new frm_AyudaMedicos(medicos, "MEDICOS", "CODIGO");
            frm.ShowDialog();
            if (frm.campoPadre.Text != string.Empty)
            {
                med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(frm.campoPadre.Text.ToString()));
                txtCodMedInterconsultado.Text = med.MED_CODIGO.ToString();
                txtRucInterconsultado.Text = med.MED_RUC.ToString();
                agregarMedico(med);
                //medicointercosultado = med;
            }

        }
        public string interconsu_id = "";
        private void agregarMedico(MEDICOS medicoTratante)
        {

            if ((medicoTratante != null))
            {
                txt_med_interconsultado.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                interconsu_id = medicoTratante.MED_RUC.Substring(0, 10);


                //if (medicoTratante.MED_CODIGO_MEDICO != null)
                //    txt_CodMSPE.Text = medicoTratante.MED_CODIGO_MEDICO.ToString();
                //else
                //    txt_CodMSPE.Text = "0"; //no tiene codigo
            }

        }
        private void agregarProfesional(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                medico = new MEDICOS();
                medico.MED_NOMBRE1 = medicoTratante.MED_NOMBRE1.Trim();
                medico.MED_APELLIDO_PATERNO = medicoTratante.MED_APELLIDO_PATERNO.Trim();
                txt_profesional.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                textBox7.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_CODIGO_MEDICO != null)
                    medico.MED_CODIGO = Convert.ToInt32(medicoTratante.MED_CODIGO_MEDICO.ToString());
                else
                    medico.MED_CODIGO = 0; //no tiene codigo
            }

        }
        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                frm_Ayudas frm = new frm_Ayudas(medicos);
                frm.bandCampo = true;
                frm.ShowDialog();
                if (frm.campoPadre2.Text != string.Empty)
                {
                    codMedico = (frm.campoPadre2);
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                    agregarProfesional(med);
                }
            }
        }

        private void dtg_4_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //Cambios Edgar 20210308
            try
            {
                if (dtg_4.CurrentRow.Cells[1].Value != null)
                {
                    if (e.ColumnIndex == this.dtg_4.Columns[2].Index)
                    {
                        DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_4.Rows[e.RowIndex].Cells[2];
                        if (chkpres.Value == null)
                            chkpres.Value = false;
                        else
                            chkpres.Value = true;

                        DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_4.Rows[e.RowIndex].Cells[3];
                        chkdef.Value = false;
                    }
                    else
                    {
                        if (e.ColumnIndex == this.dtg_4.Columns[3].Index)
                        {
                            DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_4.Rows[e.RowIndex].Cells[3];
                            if (chkdef.Value == null)
                                chkdef.Value = false;
                            else
                                chkdef.Value = true;

                            DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_4.Rows[e.RowIndex].Cells[2];
                            chkpres.Value = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha agregado diagnostico en esta fila.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtg_4.Rows.RemoveAt(dtg_4.CurrentRow.Index);
                }
            }
            catch
            {
            }

        }

        private void dtg_8_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //Cambios Edgar 20210308
            try
            {
                if (dtg_8.CurrentRow.Cells[1].Value != null)
                {
                    if (e.ColumnIndex == this.dtg_8.Columns[2].Index)
                    {
                        DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_8.Rows[e.RowIndex].Cells[2];
                        if (chkpres.Value == null)
                            chkpres.Value = false;
                        else
                            chkpres.Value = true;

                        DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_8.Rows[e.RowIndex].Cells[3];
                        chkdef.Value = false;
                    }
                    else
                    {
                        if (e.ColumnIndex == this.dtg_8.Columns[3].Index)
                        {
                            DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_8.Rows[e.RowIndex].Cells[3];
                            if (chkdef.Value == null)
                                chkdef.Value = false;
                            else
                                chkdef.Value = true;

                            DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_8.Rows[e.RowIndex].Cells[2];
                            chkpres.Value = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se ha agregado diagnostico en esta fila.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtg_8.Rows.RemoveAt(dtg_8.CurrentRow.Index);
                }
            }
            catch
            {
            }


        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool imprimir, bool cerrar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnImprimir.Enabled = imprimir;
            btnCierre.Enabled = cerrar;
        }
        private void frm_Interconsulta_Load(object sender, EventArgs e)
        {
            //Añado el panel con la informaciòn del paciente
            InfPaciente infPaciente = new InfPaciente(ate_codigo);
            panelInfPaciente.Controls.Add(infPaciente);
            //cambio las dimenciones de los paneles
            panelInfPaciente.Size = new Size(panelInfPaciente.Width, 110);
            //pantab1.Top = 125;
            HabilitarBotones(true, false, false, false);
        }

        private void txt_destino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_sala.Focus();
            }
        }

        private void txt_sala_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_serv_consultado.Focus();
            }
        }

        private void txt_serv_consultado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_serv_solicita.Focus();
            }
        }

        private void txt_serv_solicita_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnMedico.Focus();
            }
        }

        private void btnMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_motivo.Focus();
            }
        }

        private void txt_motivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_cuadroclinico.Focus();
            }
        }

        private void txt_cuadroclinico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                tabControl1.SelectedTab = this.tabControl1.Tabs[1];
                txt_resultados.Focus();
            }
        }

        private void txt_resultados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                dtg_4.Focus();
            }
        }

        private void txt_planes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txt_ccInterconsulta.Focus();
            }
        }

        private void txt_ccInterconsulta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                tabControl1.SelectedTab = this.tabControl1.Tabs[3];
                txt_resumen.Focus();
            }
        }

        private void txt_resumen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                dtg_8.Focus();
            }
        }

        private void txt_plan_diagnostico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txt_plan_tratamiento.Focus();
            }
        }

        #region Cambios Edgar 20210405
        private void refrescarSolicitudes()
        {
            gridSol.DataSource = NegImagen.getInterconsultas(ate_codigo);
            gridSol.Columns["ID"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (gridSol.RowCount > 0) //Habilita o deshabilita el boton de impresión en caso de tener campos cargados
                btnImprimir.Enabled = false; //intercambiar true por false y false por true abajo
            else
                btnImprimir.Enabled = true;
        }

        private void gridSol_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea cargar la Interconsulta?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                limpiarCampos();
                hin_codigo = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["ID"].Value.ToString());
                CargarUltimaInterconsulta();
                ValidarCerrado();
                //controles_prepararedicion();
                //cargarPedido(Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["id"].Value));
                //controles_visualizaPedido();
                NegAtenciones atenciones = new NegAtenciones();
                string estado = atenciones.EstadoCuenta(Convert.ToString(ate_codigo));
                if (estado != "1")
                {
                    Bloquear();
                }
                ValidarEnfermeria();
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        public void ValidarCerrado()
        {
            try
            {
                string valido = NegInterconsulta.EstadoInterconsulta(hin_codigo);
                if (valido != "")
                {

                    HabilitarBotones(false, false, true, false);
                    tabControl1.Enabled = false;
                    //btnabrir.Enabled = true;
                    //btnabrir.Visible = true;
                    btnNuevo.Enabled = true;
                }
                else
                {
                    HabilitarBotones(true, true, true, true);
                    //btnabrir.Enabled = true;
                    //btnabrir.Visible = true;
                    // btnNuevo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al consultar estado de interconsulta. Más detalles: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnCierre_Click(object sender, EventArgs e)
        {
            if (!validarTodoFormulario())
            {
                if (MessageBox.Show("¿Está seguro de cerrar esta interconsulta?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    try
                    {
                        if (txt_med_interconsultado.Text.ToString() == "")
                        {
                            //MessageBox.Show("No se puede cerrar la interconsulta \n\r sin un médico ingresado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            AgregarError(txt_med_interconsultado);
                            return;
                        }
                        if (hin_codigo == 0)
                        {
                            HC_INTERCONSULTA objInter = NegInterconsulta.UltimoCodigoAtencion(ate_codigo);
                            hin_codigo = objInter.HIN_CODIGO;
                        }
                        if (NegInterconsulta.EditarEstado(hin_codigo, true))
                        {
                            error.Clear();
                            MessageBox.Show("La interconsulta ha sido cerrada con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ValidarCerrado();
                            btnabrir.Visible = false;
                            tabControl1.Enabled = false;
                            imprimirMail("reporte");
                        }
                        else
                            MessageBox.Show("Algo ocurrio al querer cerrar la interconsulta.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Algo ocurrio al querer cerrar la interconsulta. Más detalles: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("Datos incompletos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private bool validarTodoFormulario()
        {
            bool flag = false;
            error.Clear();
            if (txt_cama.Text.ToString() == string.Empty)
            {
                AgregarError(txt_cama);
                flag = true;
            }
            //if (cmb_especialidadCirugia.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_ccInterconsulta);
            //    flag = true;
            //}
            if (txt_cuadroclinico.Text.ToString() == string.Empty)
            {
                AgregarError(txt_cuadroclinico);
                flag = true;
            }
            //if (txt_destino.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_destino);
            //    flag = true;
            //}
            if (cmb_especialidadCirugia2.Text.ToString() == string.Empty)
            {
                AgregarError(txt_med_interconsultado);
                flag = true;
            }
            if (txt_motivo.Text.ToString() == string.Empty)
            {
                AgregarError(txt_motivo);
                flag = true;
            }
            //if (txt_plan_diagnostico.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_plan_diagnostico);
            //    flag = true;
            //}
            //if (txt_plan_tratamiento.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_plan_tratamiento);
            //    flag = true;
            //}
            if (txt_planes.Text.ToString() == string.Empty)
            {
                AgregarError(txt_planes);
                flag = true;
            }
            if (txt_profesional.Text.ToString() == string.Empty)
            {
                AgregarError(txt_profesional);
                flag = true;
            }
            if (txt_resultados.Text.ToString() == string.Empty)
            {
                AgregarError(txt_resultados);
                flag = true;
            }
            //if (txt_resumen.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_resumen);
            //    flag = true;
            //}
            if (txt_sala.Text.ToString() == string.Empty)
            {
                AgregarError(txt_sala);
                flag = true;
            }
            //if (txt_serv_consultado.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_serv_consultado);
            //    flag = true;
            //}
            //if (txt_serv_solicita.Text.ToString() == string.Empty)
            //{
            //    AgregarError(txt_serv_solicita);
            //    flag = true;
            //}
            if (dtg_4.Rows.Count == 1)
            {
                AgregarError(dtg_4);
                flag = true;
            }
            foreach (DataGridViewRow fila in dtg_4.Rows)
            {
                DataGridViewCheckBoxCell txtcell = (DataGridViewCheckBoxCell)this.dtg_4.Rows[fila.Index].Cells[2];
                DataGridViewCheckBoxCell txtcell2 = (DataGridViewCheckBoxCell)this.dtg_4.Rows[fila.Index].Cells[3];
                DataGridViewTextBoxCell caja = (DataGridViewTextBoxCell)this.dtg_4.Rows[fila.Index].Cells[0];
                if ((txtcell.Value == null) && (txtcell2.Value == null) && (caja.Value != null))
                {
                    AgregarError(dtg_4);
                    flag = true;
                }
            }


            //if (dtg_8.Rows.Count == 1)
            //{
            //    AgregarError(dtg_8);
            //    flag = true;
            //}
            //foreach (DataGridViewRow fila in dtg_8.Rows)
            //{
            //    DataGridViewCheckBoxCell txtcell = (DataGridViewCheckBoxCell)this.dtg_8.Rows[fila.Index].Cells[2];
            //    DataGridViewCheckBoxCell txtcell2 = (DataGridViewCheckBoxCell)this.dtg_8.Rows[fila.Index].Cells[3];
            //    DataGridViewTextBoxCell caja = (DataGridViewTextBoxCell)this.dtg_8.Rows[fila.Index].Cells[0];
            //    if ((txtcell.Value == null) && (txtcell2.Value == null) && (caja.Value != null))
            //    {
            //        AgregarError(dtg_8);
            //        flag = true;
            //    }
            //}
            return flag;
        }

        private void btnabrir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de volver abrir Interconsulta?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    NegInterconsulta.AbrirEstado(hin_codigo, null);
                    MessageBox.Show("La interconsulta ha sido abierta correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    ValidarCerrado();
                    btnabrir.Visible = false;
                    tabControl1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo ocurrio al querer abrir la interconsulta. Más detalles: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        #endregion

        private void btnimagen_Click(object sender, EventArgs e)
        {
            frm_Imagen x = new frm_Imagen(ate_codigo);
            x.ShowDialog();
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyHora(e, false);
        }

        private void btnF1_Click(object sender, EventArgs e)
        {
            BuscaCIEDTG4();
        }

        private void btnF1_8_Click(object sender, EventArgs e)
        {
            BuscaCIEDTG8();
        }

        private void cmb_especialidadCirugia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txt_serv_consultado.Text = cmb_especialidadCirugia.Text;

        }

        private void cmb_especialidadCirugia2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //txt_serv_solicita.Text = cmb_especialidadCirugia2.Text;


        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void gridSol_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        private void btnInterB_Click(object sender, EventArgs e)
        {
            frm_InterconsultaB frm = new frm_InterconsultaB(ate_codigo);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();

        }

        private void txt_med_interconsultado_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    txt_med_interconsultado.Text = "";
                    txtCodMedInterconsultado.Text = "";
                    break;
            }
        }
    }
}
