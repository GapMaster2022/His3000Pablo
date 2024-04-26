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
using Infragistics.Win.UltraWinGrid;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace His.Formulario
{
    public partial class frm_Form020 : Form
    {
        USUARIOS usuario = new USUARIOS();
        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        MEDICOS medico = null;
        HC_SIGNOS_VITALES SigVit = new HC_SIGNOS_VITALES();
        public Int64 CodigoAtencion = 0;
        public Int16 UsuSec;
        DateTime horaAM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 06, 00, 00);
        DataTable SigniosVit = new DataTable();
        Int32 repeticiondias = 0;
        Int32 hoja = 1;
        Int32 SV_CODIGO = 0;
        Int32 SVD_CODIGO = 0;
        Int32 SVD_DIA = 0;
        bool editar = false;
        bool editarSV = false;
        bool valorSvActividad = false;
        bool conteoQuir = false;
        public frm_Form020(Int64 ate_codigo, string hc)
        {
            InitializeComponent();
            cargaDieta();
            dateTimePicker1.Value = horaAM;
            usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            UsuSec = usuario.ID_USUARIO;
            //USUARIOS usuario = NegUsuarios.RecuperaUsuario(His.Entidades.Clases.Sesion.codUsuario);
            //cargarCombo();
            CodigoAtencion = ate_codigo;
            CargarAtencion(ate_codigo);
            //CabiarFormatoHora();
            habilitarDia(false);
            refrescarSolicitudes();
            listarCombo();
            txt_responsable1.Text = usuario.NOMBRES + " " + usuario.APELLIDOS;
            SigVit = NegSignosVitales.CargarDatosSignosVitales(ate_codigo, 1);
            if (SigVit != null)
            {
                HabilitarBotones(true, false, true, false, false);
            }
            else
            {
                HabilitarBotones(true, false, false, false, false);
            }

        }

        private void frm_Form020_Load(object sender, EventArgs e)
        {

        }
        private void cargarPaciente(int codPac)
        {
            DateTime actual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime nacido = DateTime.Now.Date;
            paciente = NegPacientes.RecuperarPacienteID(codPac);
            if (paciente != null)
            {
                lblPaciente.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                                     paciente.PAC_APELLIDO_MATERNO + " " +
                                     paciente.PAC_NOMBRE1 + " " +
                                     paciente.PAC_NOMBRE2;
                lblHc.Text = paciente.PAC_HISTORIA_CLINICA;
                lblsexo.Text = paciente.PAC_GENERO;
            }
            else
            {
                lblHc.Text = string.Empty;
                lblPaciente.Text = string.Empty;
                lblsexo.Text = string.Empty;
            }
            nacido = (DateTime)paciente.PAC_FECHA_NACIMIENTO;
            int edadAnos = actual.Year - nacido.Year;
            if (actual.Month < nacido.Month || (actual.Month == nacido.Month && actual.Day < nacido.Day))
                edadAnos--;
            lbledad.Text = Convert.ToString(edadAnos);

        }
        public void cargarSignos(Int32 dia)
        {
            SigVit = new HC_SIGNOS_VITALES();

            SigVit = NegSignosVitales.CargarDatosSignosVitales(CodigoAtencion, dia);

            if (SigVit != null)
            {
                if (SigVit.SV_ACTIVIDAD_FISICA == "Ambulatorio")
                    valorSvActividad = true;
                txt_Interaccion1.Text = SigVit.SV_INTERACCION;
                txt_Postquirurgico1.Text = SigVit.SV_POSTQUIRURGICO;
                txtDiaSignos.Text = Convert.ToString(SigVit.SV_DIA);
                dtpFechaIngreso1.Value = (DateTime)SigVit.SV_FECHA;
                txt_Parental1.Text = SigVit.SV_ING_PARENTAL;
                txt_Oral1.Text = SigVit.SV_ING_ORAL;
                txt_iTotal1.Text = SigVit.SV_ING_TOTAL;
                txt_Orina1.Text = SigVit.SV_ELM_ORINA;
                txt_Drenaje1.Text = SigVit.SV_ELM_DRENAJE;
                txt_Otros1.Text = SigVit.SV_ELM_OTROS;
                txt_eTotal1.Text = SigVit.SV_ELM_TOTAL;
                if (SigVit.SV_BAÑO == "C")
                    rbCama.Checked = true;
                if (SigVit.SV_BAÑO == "D")
                    rbDucha.Checked = true;
                if (SigVit.SV_BAÑO == "N")
                    rbAplica.Checked = true;
                txt_Peso1.Text = SigVit.SV_PESO;
                cmbDieta.Text = SigVit.SV_DIETA_ADMINISTRADA;
                txt_Comidas1.Text = SigVit.SV_NUMERO_COMIDAS;
                txt_Medicciones1.Text = SigVit.SV_NUMERO_MEDICIONES;
                txt_Deposicion1.Text = SigVit.SV_NUMERO_DEPOSICIONES;
                if (SigVit.SV_ACTIVIDAD_FISICA == "Absoluto")
                    chkAbsolutoA.Checked = true;
                if (SigVit.SV_ACTIVIDAD_FISICA == "Relativo")
                    chkRelativoA.Checked = true;
                if (SigVit.SV_ACTIVIDAD_FISICA == "Ambulatorio")
                    chkAmbulatorio.Checked = true;
                if (DateTime.TryParse(SigVit.SV_CAMBIO_SONDA, out DateTime SV_CAMBIO_SONDA))
                    dtpSonda.Value = SV_CAMBIO_SONDA;
                else
                    chkSonda.Checked = true;
                if (DateTime.TryParse(SigVit.SV_RECANALIZACION, out DateTime SV_RECANALIZACION))
                    dtpVia.Value = SV_RECANALIZACION;
                else
                    chkAplica.Checked = true;
                usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(SigVit.SV_RESPONSABLE));
                txt_responsable1.Text = usuario.APELLIDOS + " " + usuario.NOMBRES;
            }
            else
            {
                DialogResult dilogoResult = MessageBox.Show("No tiene Registrado el dia" + cmb_dias.SelectedValue + " ¿Quiere registar el dia?", "HIS3000", MessageBoxButtons.YesNo);
                if (dilogoResult == DialogResult.Yes)
                {
                    int ctConsecutivo = 0;
                    DataTable consecutivo = new DataTable();
                    consecutivo = NegSignosVitales.diaConsecutivo(CodigoAtencion);
                    ctConsecutivo = Convert.ToInt32(consecutivo.Rows[0][0].ToString());
                    ctConsecutivo += 1;
                    if (ctConsecutivo == Convert.ToInt32(cmb_dias.SelectedValue))
                    {
                        limpiar();
                        errorProvider1.Clear();
                        dtpFechaIngreso1.Value = DateTime.Now;
                        usuario = NegUsuarios.RecuperaUsuario(UsuSec);
                        txt_responsable1.Text = usuario.NOMBRES + " " + usuario.APELLIDOS;
                        dateTimePicker1.Value = horaAM;
                    }
                    else
                    {
                        MessageBox.Show("Secuencia indorrecta Dia a continuar Dia" + ctConsecutivo, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        habilitarDia(false);
                        cargarCombo();
                        cmb_dias.Enabled = false;
                    }
                }
                else
                {

                    habilitarDia(false);
                    cargarCombo();
                    cmb_dias.Enabled = false;
                }

            }

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (validar())
                {
                    if (editar)
                    {
                        DialogResult dialogResult = MessageBox.Show("¿Desea editar los Signos?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (dialogResult == DialogResult.Yes)
                        {
                            His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                            usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                            usuario.ShowDialog();
                            if (!usuario.aceptado)
                                return;
                            HC_SIGNOS_VITALES sv = new HC_SIGNOS_VITALES();
                            sv.SV_INTERACCION = txt_Interaccion1.Text.Trim();
                            sv.SV_POSTQUIRURGICO = txt_Postquirurgico1.Text.Trim();
                            sv.SV_ING_PARENTAL = txt_Parental1.Text.Trim();
                            sv.SV_ING_ORAL = txt_Oral1.Text.Trim();
                            sv.SV_ING_TOTAL = txt_iTotal1.Text.Trim();
                            sv.SV_ELM_ORINA = txt_Orina1.Text.Trim();
                            sv.SV_ELM_DRENAJE = txt_Drenaje1.Text.Trim();
                            sv.SV_ELM_OTROS = txt_Otros1.Text.Trim();
                            sv.SV_ELM_TOTAL = txt_eTotal1.Text.Trim();
                            if (rbCama.Checked)
                                sv.SV_BAÑO = "C";
                            if (rbDucha.Checked)
                                sv.SV_BAÑO = "D";
                            if (rbAplica.Checked)
                                sv.SV_BAÑO = "N";
                            sv.SV_PESO = txt_Peso1.Text.Trim();
                            sv.SV_DIETA_ADMINISTRADA = cmbDieta.Text.Trim();
                            sv.SV_NUMERO_COMIDAS = txt_Comidas1.Text.Trim();
                            sv.SV_NUMERO_MEDICIONES = txt_Medicciones1.Text.Trim();
                            sv.SV_NUMERO_DEPOSICIONES = txt_Deposicion1.Text.Trim();
                            if (chkAbsolutoA.Checked)
                                sv.SV_ACTIVIDAD_FISICA = "Absoluto";
                            if (chkRelativoA.Checked)
                                sv.SV_ACTIVIDAD_FISICA = "Relativo";
                            if (chkAmbulatorio.Checked)
                                sv.SV_ACTIVIDAD_FISICA = "Ambulatorio";
                            if (!chkSonda.Checked)
                                sv.SV_CAMBIO_SONDA = dtpSonda.Value.ToString();
                            else
                                sv.SV_CAMBIO_SONDA = "No Aplica";
                            if (!chkAplica.Checked)
                                sv.SV_RECANALIZACION = dtpVia.Value.ToString();
                            else
                                sv.SV_RECANALIZACION = "No Aplica";
                            sv.SV_RESPONSABLE = Convert.ToString(usuario.usuarioActual);
                            sv.SV_PORCENTAJE = "";
                            sv.SV_HOJA = SV_HOJA;
                            sv.SV_RESPALDO_DIA = repeticiondias;
                            if (NegSignosVitales.EditarHojasignos(sv, SV_CODIGO))
                            {
                                editar = false;
                                limpiarSV();
                                ultraGridSignosVitales.DataSource = NegSignosVitales.CargarSignosAtencion(CodigoAtencion);
                                MessageBox.Show("Datos almacenados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                modificaDesdeIngestaEliminacio();
                            }
                            else
                                MessageBox.Show("Los datos no se pudieron editar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        if (validaDatosAdicionales())
                        {
                            His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                            usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                            usuario.ShowDialog();
                            if (!usuario.aceptado)
                                return;
                            HC_SIGNOS_VITALES sv = new HC_SIGNOS_VITALES();
                            sv.ATE_CODIGO = CodigoAtencion;
                            sv.SV_DIA = Convert.ToInt32(txtDiaSignos.Text.Trim());
                            sv.SV_FECHA = dtpFechaIngreso1.Value;
                            sv.SV_INTERACCION = txt_Interaccion1.Text.Trim();
                            sv.SV_POSTQUIRURGICO = txt_Postquirurgico1.Text.Trim();
                            sv.SV_ING_PARENTAL = txt_Parental1.Text.Trim();
                            sv.SV_ING_ORAL = txt_Oral1.Text.Trim();
                            sv.SV_ING_TOTAL = txt_iTotal1.Text.Trim();
                            sv.SV_ELM_ORINA = txt_Orina1.Text.Trim();
                            sv.SV_ELM_DRENAJE = txt_Drenaje1.Text.Trim();
                            sv.SV_ELM_OTROS = txt_Otros1.Text.Trim();
                            sv.SV_ELM_TOTAL = txt_eTotal1.Text.Trim();
                            if (rbCama.Checked)
                                sv.SV_BAÑO = "C";
                            if (rbDucha.Checked)
                                sv.SV_BAÑO = "D";
                            if (rbAplica.Checked)
                                sv.SV_BAÑO = "N";
                            sv.SV_PESO = txt_Peso1.Text.Trim();
                            sv.SV_DIETA_ADMINISTRADA = cmbDieta.Text.Trim();
                            sv.SV_NUMERO_COMIDAS = txt_Comidas1.Text.Trim();
                            sv.SV_NUMERO_MEDICIONES = txt_Medicciones1.Text.Trim();
                            sv.SV_NUMERO_DEPOSICIONES = txt_Deposicion1.Text.Trim();
                            if (chkAbsolutoA.Checked)
                                sv.SV_ACTIVIDAD_FISICA = "Absoluto";
                            if (chkRelativoA.Checked)
                                sv.SV_ACTIVIDAD_FISICA = "Relativo";
                            if (chkAmbulatorio.Checked)
                                sv.SV_ACTIVIDAD_FISICA = "Ambulatorio";
                            if (!chkSonda.Checked)
                                sv.SV_CAMBIO_SONDA = dtpSonda.Value.ToString();
                            else
                                sv.SV_CAMBIO_SONDA = "No Aplica";
                            if (!chkAplica.Checked)
                                sv.SV_RECANALIZACION = dtpVia.Value.ToString();
                            else
                                sv.SV_RECANALIZACION = "No Aplica";
                            sv.SV_RESPONSABLE = Convert.ToString(usuario.usuarioActual);
                            sv.SV_PORCENTAJE = "";
                            sv.SV_HOJA = SV_HOJA;
                            sv.SV_RESPALDO_DIA = repeticiondias;
                            if (NegSignosVitales.GrabarSignosVitales(sv))
                            {
                                Int32 registro = NegSignosVitales.ultimoRegistro();
                                HC_SIGNOS_DATOS_ADICIONALES svdat = new HC_SIGNOS_DATOS_ADICIONALES();
                                svdat.SV_CODIGO = registro;
                                //DateTime dt = Convert.ToDateTime(dateTimePicker1.Value.ToShortTimeString());//Se comenta por cambio a tomar las horas por frecuencia
                                DateTime dt = Convert.ToDateTime(cmb_frecuencia_hora.Text);
                                svdat.SVD_HORA = dt.TimeOfDay;
                                svdat.SVD_PULSO_AM = txt_Pulso1.Text.Trim();
                                svdat.SVD_TEMPERATURA_AM = txt_aTemp1.Text.Trim();
                                svdat.SVD_FRESPIRATORIA = txt_aRespiratoria1.Text.Trim();
                                svdat.SVD_SISTONICA = txt_aSistonica1.Text.Trim();
                                svdat.SVD_DIASTONICA = txt_aDiastonica1.Text.Trim();
                                if (chkOxigeno.Checked)
                                    svdat.SVD_SATURACION = mskSaturacion.Text.Trim() + " - CON OXIGENO";
                                else
                                    svdat.SVD_SATURACION = mskSaturacion.Text.Trim() + " - AIRE AMBIENTE";
                                svdat.ID_USUARIO = usuario.usuarioActual;
                                svdat.ID_FRECUENCIA = Convert.ToInt32(cmbFrecuencia.Value);
                                if (NegSignosVitales.GrabarSignosVitalesDat(svdat))
                                {
                                    limpiarSV();
                                    ultraGridSignosVitales.DataSource = NegSignosVitales.CargarSignosAtencion(CodigoAtencion);
                                    MessageBox.Show("Datos almacenados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    modificaDesdeIngestaEliminacio();
                                }
                                else
                                    MessageBox.Show("Los datos no se pudieron almacenar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                                MessageBox.Show("Los datos no se pudieron almacenar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        else
                            return;

                    }
                    //NegSignosVitales.GuardarSignosVitales(CodigoAtencion, Convert.ToInt32(txtDiaSignos.Text.Trim()), dtpFechaIngreso1.Value.ToShortDateString(), txt_Interaccion1.Text.Trim(),
                    //txt_Postquirurgico1.Text.Trim(),
                    //txt_Parental1.Text.Trim(), txt_Oral1.Text.Trim(), txt_iTotal1.Text.Trim(), txt_Orina1.Text.Trim(), txt_Drenaje1.Text.Trim(), txt_Otros1.Text.Trim(), txt_eTotal1.Text.Trim(),
                    //txt_Baño1.Text.Trim(), txt_Peso1.Text.Trim(), txt_Dieta1.Text.Trim(), txt_Comidas1.Text.Trim(), txt_Medicciones1.Text.Trim(), txt_Deposicion1.Text.Trim(), txt_Fisica1.Text.Trim(),
                    //txt_Sonda1.Text.Trim(), txt_Recalalizacion1.Text.Trim(), Convert.ToString(usuario.ID_USUARIO), mskPorcentaje.Text.Trim(), SV_HOJA, repeticiondias);

                    habilitarDia(false);
                    HabilitarBotones(false, false, true, true, true);
                    ImprimirFormulario();
                    errorProvider1.Clear();
                    refrescarSolicitudes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se logro abrir el reporte por: " + ex, "HIS-3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cargarCombo()
        {
            //DataTable signo = new DataTable();

            //DataColumn dcg = new DataColumn();
            //dcg.ColumnName = "Codigo";
            //dcg.DataType = typeof(double);

            //DataColumn dcg1 = new DataColumn();
            //dcg1.ColumnName = "Dia";
            //dcg1.DataType = typeof(string);

            //signo.Columns.AddRange(new DataColumn[] { dcg, dcg1 });

            //for (int i = 1; i < 200; i++)
            //{
            //    signo.Rows.Add(new object[] { i, "Dia" + i });
            //}
            //cmb_dias.DataSource = signo;
            //cmb_dias.ValueMember = "Codigo";
            //cmb_dias.DisplayMember = "Dia";
            //cmb_dias.SelectedIndex = 0;
        }
        public void cargaDieta()
        {
            cmbDieta.DataSource = NegSignosVitales.cargaDieta();
            cmbDieta.ValueMember = "TDI_CODIGO";
            cmbDieta.DisplayMember = "TDI_DESCRIPCION";
            cmbDieta.SelectedIndex = -1;
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFormulario();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, true, false, true);
            habilitarDia(true);
            cmb_dias.Enabled = true;
            editar = true;
        }

        public void HabilitarBotones(bool nuevo, bool guardar, bool imprimir, bool actualizar, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnImprimir.Enabled = imprimir;
            btnActualizar.Enabled = actualizar;
            btnCancelar.Enabled = cancelar;
        }
        private void CargarAtencion(Int64 codAtencion)
        {
            atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
            lblatencion.Text = atencion.ATE_NUMERO_ATENCION;
            lblHabitacion.Text = atencion.HABITACIONES.hab_Numero;
            dtpFechaIngreso.Value = (DateTime)atencion.ATE_FECHA_INGRESO;
            cargarPaciente(atencion.PACIENTES.PAC_CODIGO);

            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            int codigoMedico = medicos.FirstOrDefault(m => m.EntityKey == atencion.MEDICOSReference.EntityKey).MED_CODIGO;
            if (codigoMedico != 0)
                cargarMedico(codigoMedico);
            ultraGridSignosVitales.DataSource = NegSignosVitales.CargarSignosAtencion(CodigoAtencion);
        }

        private void cargarMedico(int cod)
        {
            medico = NegMedicos.RecuperaMedicoId(cod);
            lblmedico.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                medico.MED_APELLIDO_MATERNO.Trim() + " " +
                medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
        }
        private void CabiarFormatoHora()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Time;
            dateTimePicker1.ShowUpDown = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void habilitarDia(bool dia)
        {
            grb_Informacion1.Enabled = dia;
            grb_SignosVitales1.Enabled = dia;
            grb_BalanceHidrico1.Enabled = dia;
            grb_MedicionesActividades1.Enabled = dia;
        }
        public void limpiar()
        {
            errorProvider1.Clear();
            txt_Interaccion1.Text = "";
            txt_Postquirurgico1.Text = "";
            txt_Pulso1.Text = "";
            txt_aTemp1.Text = "";
            txt_aRespiratoria1.Text = "";
            txt_aSistonica1.Text = "";
            txt_aDiastonica1.Text = "";
            txt_Parental1.Text = "0";
            txt_Oral1.Text = "0";
            txt_iTotal1.Text = "0";
            txt_Orina1.Text = "0";
            txt_Drenaje1.Text = "0";
            txt_Otros1.Text = "0";
            txt_eTotal1.Text = "0";
            txt_Baño1.Text = "";
            txt_Peso1.Text = "";
            txt_Dieta1.Text = "";
            txt_Comidas1.Text = "";
            txt_Medicciones1.Text = "";
            txt_Deposicion1.Text = "";
            txt_Fisica1.Text = "";
            txt_Sonda1.Text = "";
            txt_Recalalizacion1.Text = "";
            txt_responsable1.Text = "";
            txtDiaSignos.Text = "";
            repeticiondias = 0;
            rbCama.Checked = false;
            rbDucha.Checked = false;
            rbAplica.Checked = false;
            chkRelativoA.Checked = false;
            chkAbsolutoA.Checked = false;
            chkAplica.Checked = false;
            chkSonda.Checked = false;
            valorSvActividad = false;
        }
        public void limpiarSV()
        {
            txt_Pulso1.Text = "";
            txt_aTemp1.Text = "";
            txt_aRespiratoria1.Text = "";
            txt_aSistonica1.Text = "";
            txt_aDiastonica1.Text = "";
            mskSaturacion.Text = "";
            cmb_frecuencia_hora.SelectedIndex = -1;
            cmbFrecuencia.SelectedIndex = -1;
            chkOxigeno.Checked = false;
            chkAmbiente.Checked = false;
            errorProvider1.Clear();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            HabilitarBotones(false, true, false, false, true);
            habilitarDia(true);
            btnGrabasignos.Visible = false;
            int ctConsecutivo = 0;
            DataTable consecutivo = new DataTable();
            DataTable consecutivoRep = new DataTable();
            TimeSpan diasTrascurridos = DateTime.Now - (DateTime)atencion.ATE_FECHA_INGRESO;
            txt_Interaccion1.Text = diasTrascurridos.Days.ToString();
            Int64 diasPost = NegSignosVitales.calculoDiasQuirurgico(CodigoAtencion);
            if (diasPost != 0)
            {
                txt_Postquirurgico1.Text = diasPost.ToString();
                txt_Postquirurgico1.Enabled = false;
            }
            else
                txt_Postquirurgico1.Enabled = true;
            consecutivo = NegSignosVitales.diaConsecutivo(CodigoAtencion);
            if (consecutivo != null)
            {
                ctConsecutivo = Convert.ToInt32(consecutivo.Rows[0][0].ToString());
                ctConsecutivo += 1;
                txtDiaSignos.Text = Convert.ToString(ctConsecutivo);
                if (diasImprecion(ctConsecutivo))
                {
                    consecutivo = NegSignosVitales.diaConsecutivoHoja(CodigoAtencion, SV_HOJA);
                    if (consecutivo != null)
                    {
                        repeticiondias = Convert.ToInt32(consecutivo.Rows[0][0].ToString()) + 1;
                    }
                }
                else
                {
                    repeticiondias = ctConsecutivo;
                }
                usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
                UsuSec = usuario.ID_USUARIO;
                txt_responsable1.Text = usuario.NOMBRES + " " + usuario.APELLIDOS;
            }
            else
            {
                repeticiondias = 1;
                txtDiaSignos.Text = "1";
                usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
                UsuSec = usuario.ID_USUARIO;
                txt_responsable1.Text = usuario.NOMBRES + " " + usuario.APELLIDOS;
            }
        }
        int contDias = 0; //Para repetir unica mente los dias auxiliares 
        public bool diasImprecion(Int32 contador)
        {
            DataTable conteo = new DataTable();
            if (contador > 9)
            {
                SV_HOJA = 2;

                return true;
            }
            if (contador > 18)
            {
                SV_HOJA = 3;

                return true;
            }
            if (contador > 27)
            {
                SV_HOJA = 4;

                return true;
            }
            if (contador > 36)
            {
                SV_HOJA = 5;

                return true;
            }
            if (contador > 45)
            {
                SV_HOJA = 6;

                return true;
            }
            if (contador > 54)
            {
                SV_HOJA = 7;

                return true;
            }
            if (contador > 63)
            {
                SV_HOJA = 8;

                return true;
            }
            if (contador > 72)
            {
                SV_HOJA = 9;

                return true;
            }
            if (contador > 81)
            {
                SV_HOJA = 10;

                return true;
            }
            if (contador > 90)
            {
                SV_HOJA = 11;

                return true;
            }
            return false;
        }

        bool valida;
        public bool validar()
        {
            errorProvider1.Clear();
            valida = true;
            DateTime horaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            DateTime hora = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 00);

            DataTable sign = NegSignosVitales.ConsultaSignosXfecha(CodigoAtencion, dtpFechaIngreso1.Value);
            if (!editar)
            {
                if (sign.Rows.Count != 0)
                {
                    MessageBox.Show("Fecha ya registrada", "His-3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valida = false;
                }
            }
            if (dtpFechaIngreso1.Value > DateTime.Now)
            {
                errorProvider1.SetError(dtpFechaIngreso1, "La fecha tiene que ser menor a la fecha actual ");
                valida = false;
                dtpFechaIngreso1.Value = DateTime.Now;
            }
            if (dtpFechaIngreso1.Value < atencion.ATE_FECHA_INGRESO)
            {
                errorProvider1.SetError(dtpFechaIngreso1, "La fecha tiene que ser mayor a la fecha de ingreso del paciente ");
                valida = false;
                dtpFechaIngreso1.Value = DateTime.Now;
            }
            if (txt_responsable1.Text == "")
            {
                errorProvider1.SetError(txt_responsable1, "Seleccione medico Tratante ");
                valida = false;
            }
            if (dateTimePicker1.Value > horaActual)
            {
                errorProvider1.SetError(dateTimePicker1, "Lahora no puede ser mayor a la hora actual ");
                valida = false;
            }
            //if (txt_Dieta1.Text == "")
            //{
            //    errorProvider1.SetError(txt_Dieta1, "No ha registrado informacion");
            //    valida = false;
            //}
            //if (txt_Peso1.Text == "")
            //{
            //    errorProvider1.SetError(txt_Peso1, "No ha registrado informacion");
            //    valida = false;
            //}
            if (!chkAbsolutoA.Checked)
                if (!chkRelativoA.Checked)
                    if (!chkAmbulatorio.Checked)
                    {
                        errorProvider1.SetError(chkAmbulatorio, "No ha seleccionado una opción");
                        valida = false;
                    }
            //if (!rbCama.Checked)
            //    if (!rbDucha.Checked)
            //        if (!rbAplica.Checked)
            //        {
            //            errorProvider1.SetError(rbAplica, "No ha seleccionado una opción");
            //            valida = false;
            //        }
            //if (txt_Interaccion1.Text == "")
            //{
            //    txt_Interaccion1.Text = "0";
            //}

            //if (dateTimePicker2.Value > horaActual)
            //{
            //    errorProvider1.SetError(dateTimePicker2, "Lahora no puede ser mayor a la hora actual ");
            //    valida = false;
            //}

            return valida;
        }
        public bool validaDatosAdicionales()
        {
            valida = true;
            if (chkAmbiente.Checked == false && chkOxigeno.Checked == false)
            {
                errorProvider1.SetError(chkAmbiente, "Seleccione una opcion");
                valida = false;
            }
            if (cmbFrecuencia.Text == "")
            {
                errorProvider1.SetError(cmbFrecuencia, "Seleccione una frecuencia");
                valida = false;
            }
            if (cmb_frecuencia_hora.Text == "")
            {
                errorProvider1.SetError(cmb_frecuencia_hora, "Seleccione una hora");
                valida = false;
            }
            return valida;
        }
        private void ImprimirFormulario()
        {
            WaitForm wf = new WaitForm();
            wf.Show();
            if (NegSignosVitales.editarReporteSv())
            {
                if (NegSignosVitales.cargaSignosVitales(CodigoAtencion, SV_HOJA))
                {
                    grabaDatosExcel(7);
                    grabarImagenExcel(7, 6);
                }
                else
                    MessageBox.Show("No se pudo crear el grafico \r\n de signos vitales", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("No se pudo crear el grafico \r\n de signos vitales", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            NegCertificadoMedico medico = new NegCertificadoMedico();
            DSForm020 frm = new DSForm020();
            DataRow dr;
            List<HC_SIGNOS_VITALES> sv = new List<HC_SIGNOS_VITALES>();
            sv = NegSignosVitales.CargarImpresion(CodigoAtencion, SV_HOJA);
            LOGOS_EMPRESA logEmp = NegParametros.logosEmpresa(6);
            dr = frm.Tables["Form020"].NewRow();
            foreach (var item in sv)
            {
                switch (item.SV_RESPALDO_DIA)
                {
                    case 1:
                        dr["Logo"] = medico.path();
                        dr["pathSV"] = logEmp.LEM_RUTA;
                        dr["Empresa"] = His.Entidades.Clases.Sesion.nomEmpresa;
                        dr["Apellido"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
                        dr["Nombre"] = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                        dr["HC"] = lblHc.Text; ;
                        dr["Sexo"] = paciente.PAC_GENERO;
                        dr["1fecha"] = item.SV_FECHA;
                        dr["1interaccion"] = item.SV_INTERACCION;
                        dr["1postquirurgico"] = item.SV_POSTQUIRURGICO;

                        dr["1ingParental"] = item.SV_ING_PARENTAL;
                        dr["1ingOral"] = item.SV_ING_ORAL;
                        dr["1ingTotal"] = item.SV_ING_TOTAL;
                        dr["1elmOrina"] = item.SV_ELM_ORINA;
                        dr["1elmDrenaje"] = item.SV_ELM_DRENAJE;
                        dr["1elmOtros"] = item.SV_ELM_OTROS;
                        dr["1elmTotal"] = item.SV_ELM_TOTAL;
                        if (item.SV_BAÑO == "C")
                            dr["1baño"] = "Cama";
                        if (item.SV_BAÑO == "D")
                            dr["1baño"] = "Ducha";
                        if (item.SV_BAÑO == "N")
                            dr["1baño"] = "No Aplica";
                        dr["1peso"] = item.SV_PESO;
                        dr["1dieta"] = item.SV_DIETA_ADMINISTRADA;
                        dr["1comidas"] = item.SV_NUMERO_COMIDAS;
                        dr["1mediciones"] = item.SV_NUMERO_MEDICIONES;
                        dr["1depposiciones"] = item.SV_NUMERO_DEPOSICIONES;
                        dr["1actividad"] = item.SV_ACTIVIDAD_FISICA;
                        dr["1sonda"] = item.SV_CAMBIO_SONDA;
                        dr["1recanalizacion"] = item.SV_RECANALIZACION;
                        usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(item.SV_RESPONSABLE));
                        dr["1Responsable"] = usuario.USR;
                        dr["1pRespiratoria"] = item.SV_PORCENTAJE;
                        break;
                    case 2:
                        dr["2fecha"] = item.SV_FECHA;
                        dr["2interaccion"] = item.SV_INTERACCION;
                        dr["2postquirurgico"] = item.SV_POSTQUIRURGICO;

                        dr["2ingParental"] = item.SV_ING_PARENTAL;
                        dr["2ingOral"] = item.SV_ING_ORAL;
                        dr["2ingTotal"] = item.SV_ING_TOTAL;
                        dr["2elmOrina"] = item.SV_ELM_ORINA;
                        dr["2elmDrenajes"] = item.SV_ELM_DRENAJE;
                        dr["2elmOtros"] = item.SV_ELM_OTROS;
                        dr["2elmTotal"] = item.SV_ELM_TOTAL;
                        if (item.SV_BAÑO == "C")
                            dr["2baño"] = "Cama";
                        if (item.SV_BAÑO == "D")
                            dr["2baño"] = "Ducha";
                        if (item.SV_BAÑO == "N")
                            dr["2baño"] = "No Aplica";
                        dr["2peso"] = item.SV_PESO;
                        dr["2dieta"] = item.SV_DIETA_ADMINISTRADA;
                        dr["2comidas"] = item.SV_NUMERO_COMIDAS;
                        dr["2mediciones"] = item.SV_NUMERO_MEDICIONES;
                        dr["2depodiciones"] = item.SV_NUMERO_DEPOSICIONES;
                        dr["2actividad"] = item.SV_ACTIVIDAD_FISICA;
                        dr["2sonda"] = item.SV_CAMBIO_SONDA;
                        dr["2recanalizacion"] = item.SV_RECANALIZACION;
                        usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(item.SV_RESPONSABLE));
                        dr["2Responsable"] = usuario.USR;
                        dr["1pSistonica"] = item.SV_PORCENTAJE;
                        break;
                    case 3:
                        dr["3fecha"] = item.SV_FECHA;
                        dr["3interaccion"] = item.SV_INTERACCION;
                        dr["3postquirurgico"] = item.SV_POSTQUIRURGICO;

                        dr["3ingParental"] = item.SV_ING_PARENTAL;
                        dr["3ingOral"] = item.SV_ING_ORAL;
                        dr["3ingTotal"] = item.SV_ING_TOTAL;
                        dr["3elmOrina"] = item.SV_ELM_ORINA;
                        dr["3elmDrenaje"] = item.SV_ELM_DRENAJE;
                        dr["3elmOtros"] = item.SV_ELM_OTROS;
                        dr["3elmTotal"] = item.SV_ELM_TOTAL;
                        if (item.SV_BAÑO == "C")
                            dr["3baño"] = "Cama";
                        if (item.SV_BAÑO == "D")
                            dr["3baño"] = "Ducha";
                        if (item.SV_BAÑO == "N")
                            dr["3baño"] = "No Aplica";
                        dr["3peso"] = item.SV_PESO;
                        dr["3dieta"] = item.SV_DIETA_ADMINISTRADA;
                        dr["3comidas"] = item.SV_NUMERO_COMIDAS;
                        dr["3mediciones"] = item.SV_NUMERO_MEDICIONES;
                        dr["3deposisciones"] = item.SV_NUMERO_DEPOSICIONES;
                        dr["3actividad"] = item.SV_ACTIVIDAD_FISICA;
                        dr["3sonda"] = item.SV_CAMBIO_SONDA;
                        dr["3recanalizacion"] = item.SV_RECANALIZACION;
                        usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(item.SV_RESPONSABLE));
                        dr["3Responsable"] = usuario.USR;
                        dr["1pDiastonica"] = item.SV_PORCENTAJE;
                        break;
                    case 4:
                        dr["4fecha"] = item.SV_FECHA;
                        dr["4interaccion"] = item.SV_INTERACCION;
                        dr["4postquirurgico"] = item.SV_POSTQUIRURGICO;

                        dr["4ingParental"] = item.SV_ING_PARENTAL;
                        dr["4ingOral"] = item.SV_ING_ORAL;
                        dr["4ingTotal"] = item.SV_ING_TOTAL;
                        dr["4elmOrina"] = item.SV_ELM_ORINA;
                        dr["4elmDrenaje"] = item.SV_ELM_DRENAJE;
                        dr["4elmOtros"] = item.SV_ELM_OTROS;
                        dr["4elmTotal"] = item.SV_ELM_TOTAL;
                        if (item.SV_BAÑO == "C")
                            dr["4baño"] = "Cama";
                        if (item.SV_BAÑO == "D")
                            dr["4baño"] = "Ducha";
                        if (item.SV_BAÑO == "N")
                            dr["4baño"] = "No Aplica";
                        dr["4peso"] = item.SV_PESO;
                        dr["4administrada"] = item.SV_DIETA_ADMINISTRADA;
                        dr["4comidas"] = item.SV_NUMERO_COMIDAS;
                        dr["4mediciones"] = item.SV_NUMERO_MEDICIONES;
                        dr["4deposiciones"] = item.SV_NUMERO_DEPOSICIONES;
                        dr["4fisica"] = item.SV_ACTIVIDAD_FISICA;
                        dr["4sonda"] = item.SV_CAMBIO_SONDA;
                        dr["4recanalizacion"] = item.SV_RECANALIZACION;
                        usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(item.SV_RESPONSABLE));
                        dr["4Responsable"] = usuario.USR;
                        dr["2pRespiratoria"] = item.SV_PORCENTAJE;
                        break;
                    case 5:
                        dr["5fecha"] = item.SV_FECHA;
                        dr["5interaccion"] = item.SV_INTERACCION;
                        dr["5postquirurgico"] = item.SV_POSTQUIRURGICO;

                        dr["5ingParental"] = item.SV_ING_PARENTAL;
                        dr["5ingOral"] = item.SV_ING_ORAL;
                        dr["5ingTotal"] = item.SV_ING_TOTAL;
                        dr["5elmOrina"] = item.SV_ELM_ORINA;
                        dr["5elmDrenaje"] = item.SV_ELM_DRENAJE;
                        dr["5elmOtros"] = item.SV_ELM_OTROS;
                        dr["5elmTotal"] = item.SV_ELM_TOTAL;
                        if (item.SV_BAÑO == "C")
                            dr["5baño"] = "Cama";
                        if (item.SV_BAÑO == "D")
                            dr["5baño"] = "Ducha";
                        if (item.SV_BAÑO == "N")
                            dr["5baño"] = "No Aplica";
                        dr["5peso"] = item.SV_PESO;
                        dr["5dieta"] = item.SV_DIETA_ADMINISTRADA;
                        dr["5comidas"] = item.SV_NUMERO_COMIDAS;
                        dr["5mediciones"] = item.SV_NUMERO_MEDICIONES;
                        dr["5deposiciones"] = item.SV_NUMERO_DEPOSICIONES;
                        dr["5fisica"] = item.SV_ACTIVIDAD_FISICA;
                        dr["5sonda"] = item.SV_CAMBIO_SONDA;
                        dr["5recanalizacion"] = item.SV_RECANALIZACION;
                        usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(item.SV_RESPONSABLE));
                        dr["5Responsable"] = usuario.USR;
                        dr["2pSistonica"] = item.SV_PORCENTAJE;
                        break;
                    case 6:
                        dr["6fecha"] = item.SV_FECHA;
                        dr["6interaccion"] = item.SV_INTERACCION;
                        dr["6postquirurgico"] = item.SV_POSTQUIRURGICO;

                        dr["6ingParental"] = item.SV_ING_PARENTAL;
                        dr["6ingOral"] = item.SV_ING_ORAL;
                        dr["6intTotal"] = item.SV_ING_TOTAL;
                        dr["6elmOrina"] = item.SV_ELM_ORINA;
                        dr["6elmDrenaje"] = item.SV_ELM_DRENAJE;
                        dr["6elmOtros"] = item.SV_ELM_OTROS;
                        dr["6elmTotal"] = item.SV_ELM_TOTAL;
                        if (item.SV_BAÑO == "C")
                            dr["6baño"] = "Cama";
                        if (item.SV_BAÑO == "D")
                            dr["6baño"] = "Ducha";
                        if (item.SV_BAÑO == "N")
                            dr["6baño"] = "No Aplica";
                        dr["6peso"] = item.SV_PESO;
                        dr["6administrada"] = item.SV_DIETA_ADMINISTRADA;
                        dr["6comidas"] = item.SV_NUMERO_COMIDAS;
                        dr["6mediciones"] = item.SV_NUMERO_MEDICIONES;
                        dr["6deposiciones"] = item.SV_NUMERO_DEPOSICIONES;
                        dr["6fisica"] = item.SV_ACTIVIDAD_FISICA;
                        dr["6sonda"] = item.SV_CAMBIO_SONDA;
                        dr["6recanalizacion"] = item.SV_RECANALIZACION;
                        usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(item.SV_RESPONSABLE));
                        dr["6Responsable"] = usuario.USR;
                        dr["2pDiastonica"] = item.SV_PORCENTAJE;
                        break;
                    case 7:
                        dr["7fecha"] = item.SV_FECHA;
                        dr["7interaccion"] = item.SV_INTERACCION;
                        dr["7postquirurgico"] = item.SV_POSTQUIRURGICO;

                        dr["7ingParental"] = item.SV_ING_PARENTAL;
                        dr["7ingOral"] = item.SV_ING_ORAL;
                        dr["7ingTotal"] = item.SV_ING_TOTAL;
                        dr["7elmOrina"] = item.SV_ELM_ORINA;
                        dr["7elmDrenaje"] = item.SV_ELM_DRENAJE;
                        dr["7elmOtros"] = item.SV_ELM_OTROS;
                        dr["7elmTotal"] = item.SV_ELM_TOTAL;
                        if (item.SV_BAÑO == "C")
                            dr["7baño"] = "Cama";
                        if (item.SV_BAÑO == "D")
                            dr["7baño"] = "Ducha";
                        if (item.SV_BAÑO == "N")
                            dr["7baño"] = "No Aplica";
                        dr["7peso"] = item.SV_PESO;
                        dr["7dieta"] = item.SV_DIETA_ADMINISTRADA;
                        dr["7comidas"] = item.SV_NUMERO_COMIDAS;
                        dr["7mediciones"] = item.SV_NUMERO_MEDICIONES;
                        dr["7deposiciones"] = item.SV_NUMERO_DEPOSICIONES;
                        dr["7fisica"] = item.SV_ACTIVIDAD_FISICA;
                        dr["7sonda"] = item.SV_CAMBIO_SONDA;
                        dr["7recanalizacion"] = item.SV_RECANALIZACION;
                        usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(item.SV_RESPONSABLE));
                        dr["7Responsable"] = usuario.USR;
                        dr["3pRespiratoria"] = item.SV_PORCENTAJE;
                        break;
                    case 8:
                        dr["8fecha"] = item.SV_FECHA;
                        dr["8interaccion"] = item.SV_INTERACCION;
                        dr["8postquirurgico"] = item.SV_POSTQUIRURGICO;

                        dr["8ingParental"] = item.SV_ING_PARENTAL;
                        dr["8ingOral"] = item.SV_ING_ORAL;
                        dr["8ingTotal"] = item.SV_ING_TOTAL;
                        dr["8elmOrina"] = item.SV_ELM_ORINA;
                        dr["8elmDrenaje"] = item.SV_ELM_DRENAJE;
                        dr["8elmOtros"] = item.SV_ELM_OTROS;
                        dr["8elmTotal"] = item.SV_ELM_TOTAL;
                        if (item.SV_BAÑO == "C")
                            dr["8baño"] = "Cama";
                        if (item.SV_BAÑO == "D")
                            dr["8baño"] = "Ducha";
                        if (item.SV_BAÑO == "N")
                            dr["8baño"] = "No Aplica";
                        dr["8peso"] = item.SV_PESO;
                        dr["8administrada"] = item.SV_DIETA_ADMINISTRADA;
                        dr["8comidas"] = item.SV_NUMERO_COMIDAS;
                        dr["8mediciones"] = item.SV_NUMERO_MEDICIONES;
                        dr["8depopsiscion"] = item.SV_NUMERO_DEPOSICIONES;
                        dr["8fisica"] = item.SV_ACTIVIDAD_FISICA;
                        dr["8sonda"] = item.SV_CAMBIO_SONDA;
                        dr["8recanalizacion"] = item.SV_RECANALIZACION;
                        usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(item.SV_RESPONSABLE));
                        dr["8Responsable"] = usuario.USR;
                        dr["3pSistonica"] = item.SV_PORCENTAJE;
                        break;
                    case 9:
                        dr["9fecha"] = item.SV_FECHA;
                        dr["9interaccion"] = item.SV_INTERACCION;
                        dr["9postquirurgico"] = item.SV_POSTQUIRURGICO;

                        dr["9ingParental"] = item.SV_ING_PARENTAL;
                        dr["9ingOral"] = item.SV_ING_ORAL;
                        dr["9ingTotal"] = item.SV_ING_TOTAL;
                        dr["9elmOrina"] = item.SV_ELM_ORINA;
                        dr["9elmDrenaje"] = item.SV_ELM_DRENAJE;
                        dr["9elmOtros"] = item.SV_ELM_OTROS;
                        dr["9elmTotal"] = item.SV_ELM_TOTAL;
                        if (item.SV_BAÑO == "C")
                            dr["9baño"] = "Cama";
                        if (item.SV_BAÑO == "D")
                            dr["9baño"] = "Ducha";
                        if (item.SV_BAÑO == "N")
                            dr["9baño"] = "No Aplica";
                        dr["9peso"] = item.SV_PESO;
                        dr["9administrada"] = item.SV_DIETA_ADMINISTRADA;
                        dr["9comidas"] = item.SV_NUMERO_COMIDAS;
                        dr["9mediciones"] = item.SV_NUMERO_MEDICIONES;
                        dr["9deposiciones"] = item.SV_NUMERO_DEPOSICIONES;
                        dr["9fisica"] = item.SV_ACTIVIDAD_FISICA;
                        dr["9sonda"] = item.SV_CAMBIO_SONDA;
                        dr["9recanalizacion"] = item.SV_RECANALIZACION;
                        usuario = NegUsuarios.RecuperaUsuario(Convert.ToInt16(item.SV_RESPONSABLE));
                        dr["9Responsable"] = usuario.USR;
                        dr["3pDiastonica"] = item.SV_PORCENTAJE;
                        break;
                    default:
                        break;

                }
            }

            frm.Tables["Form020"].Rows.Add(dr);
            //dr["HC"] = "";
            //List<DtoExploSignosVitales> dat = NegSignosVitales.CargarSignosAtencion(CodigoAtencion);
            List<DtoExploSignosVitales> dat = NegSignosVitales.cargarSignosXatencion(CodigoAtencion);

            foreach (var item in dat)
            {
                dr = frm.Tables["SignosVitales"].NewRow();
                dr["fecha"] = item.FECHA;
                dr["hora"] = item.HORA;
                dr["pulso"] = item.F_CARDIACA;
                dr["temperatura"] = item.TEMPERATURA;
                dr["respiratoria"] = item.F_RESPIRATORIA;
                dr["sistonica"] = item.P_SISTONICA;
                dr["diastonica"] = item.P_DIASTONICA;
                dr["medico"] = item.MEDICO;
                dr["oxigeno"] = item.S_OXIGENO;
                frm.Tables["SignosVitales"].Rows.Add(dr);
            }
            wf.Close();
            frmReportes x = new frmReportes(1, "SignosVitalesA", frm);
            x.Show();
        }

        #region OpcionesKeyDow
        private void txt_Postquirurgico1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_aSistonica1.Focus();
            }
        }

        private void txt_Pulso1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_aRespiratoria1.Focus();
            }
        }

        private void txt_aTemp1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                mskSaturacion.Focus();
            }
        }

        private void txt_aRespiratoria1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_aTemp1.Focus();
            }
        }
        private void txt_aSistonica1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_aDiastonica1.Focus();
            }
        }

        private void txt_aDiastonica1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Pulso1.Focus();
            }
        }

        private void txt_Parental1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Oral1.Focus();
            }
        }

        private void txt_Oral1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Orina1.Focus();
            }
        }

        private void txt_Orina1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Drenaje1.Focus();
            }
        }

        private void txt_Drenaje1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Otros1.Focus();
            }
        }

        private void txt_Otros1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                rbCama.Focus();
            }
        }

        private void txt_Baño1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Peso1.Focus();
            }
        }

        private void txt_Peso1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Dieta1.Focus();
            }
        }

        private void txt_Dieta1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Comidas1.Focus();
            }
        }

        private void txt_Comidas1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Medicciones1.Focus();
            }
        }
        private void txt_Medicciones1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Deposicion1.Focus();
            }
        }

        private void txt_Deposicion1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                chkAbsolutoA.Focus();
            }
        }

        private void txt_Fisica1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Sonda1.Focus();
            }
        }

        private void txt_Sonda1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                dtpVia.Focus();
            }
        }

        private void txt_Recalalizacion1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_responsable1.Focus();
            }
        }

        private void txt_bDiastonica1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Parental1.Focus();
            }
        }
        private void txt_Interaccion1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Postquirurgico1.Focus();
            }
        }
        private void mskPorcentaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Baño1.Focus();
            }
        }

        private void mskSaturacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Parental1.Focus();
            }
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

        private void txt_aSistonica1_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_aDiastonica1_TextChanged(object sender, EventArgs e)
        {
            if (txt_aDiastonica1.Text != "")
            {
                if (txt_aDiastonica1.Text == "" || !NegUtilitarios.ValidaPrecion2(Convert.ToDouble(txt_aDiastonica1.Text)))
                {
                    txt_aDiastonica1.Text = "0";
                }
            }
        }

        private void txt_aSistonica1_TextChanged(object sender, EventArgs e)
        {
            if (txt_aSistonica1.Text == "" || !NegUtilitarios.ValidaPrecion1(Convert.ToInt16(txt_aSistonica1.Text)))
            {
                //txt_aSistonica1.Text = "0";
            }
        }

        private void txt_aDiastonica1_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodoNumeros(sender, e);
        }

        private void txt_aDiastonica1_Leave(object sender, EventArgs e)
        {
            if (txt_aDiastonica1.Text != "")
            {
                if (Convert.ToInt32(txt_aSistonica1.Text) < Convert.ToDouble(txt_aDiastonica1.Text))
                {
                    txt_aDiastonica1.Text = "0";
                    txt_aDiastonica1.Focus();
                }
            }
        }
        private void txt_aTemp1_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodofuncionTeclitas(sender, e, txt_aTemp1);
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
        private void mskSaturacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
            MiMetodoNumeros(sender, e);
        }

        private void mskSaturacion_Leave(object sender, EventArgs e)
        {

        }

        #endregion

        #region Validacion txt
        private void txt_iTotal1_Leave(object sender, EventArgs e)
        {
            txt_iTotal1.Text = (Convert.ToUInt32(txt_Parental1.Text) + Convert.ToUInt32(txt_Oral1.Text)).ToString();
        }

        private void txt_eTotal1_Leave(object sender, EventArgs e)
        {
            if (!valorSvActividad)
                txt_eTotal1.Text = (Convert.ToUInt32(txt_Orina1.Text) + Convert.ToUInt32(txt_Drenaje1.Text) + Convert.ToUInt32(txt_Otros1.Text)).ToString();
            else
                txt_eTotal1.Text = (Convert.ToUInt32(txt_Drenaje1.Text) + Convert.ToUInt32(txt_Otros1.Text)).ToString();
        }

        private void txt_Parental1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_Oral1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_iTotal1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_Orina1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_Drenaje1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_Otros1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_eTotal1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        #endregion

        #region Suma txt
        private void txt_Oral1_TextChanged(object sender, EventArgs e)
        {
            if (txt_Oral1.Text != "")
            {
                txt_iTotal1.Text = (Convert.ToUInt32(txt_Parental1.Text) + Convert.ToUInt32(txt_Oral1.Text)).ToString();
            }
        }

        private void txt_Parental1_TextChanged(object sender, EventArgs e)
        {
            if (txt_Parental1.Text != "")
            {
                txt_iTotal1.Text = (Convert.ToUInt32(txt_Parental1.Text) + Convert.ToUInt32(txt_Oral1.Text)).ToString();
            }
        }

        private void txt_Orina1_TextChanged(object sender, EventArgs e)
        {
            if (txt_Orina1.Text != "")
            {
                if (!valorSvActividad)
                    txt_eTotal1.Text = (Convert.ToUInt32(txt_Orina1.Text) + Convert.ToUInt32(txt_Drenaje1.Text) + Convert.ToUInt32(txt_Otros1.Text)).ToString();
                else
                    txt_eTotal1.Text = (Convert.ToUInt32(txt_Drenaje1.Text) + Convert.ToUInt32(txt_Otros1.Text)).ToString();
            }
        }

        private void txt_Drenaje1_TextChanged(object sender, EventArgs e)
        {
            if (txt_Drenaje1.Text != "")
            {
                if (!valorSvActividad)
                    txt_eTotal1.Text = (Convert.ToUInt32(txt_Orina1.Text) + Convert.ToUInt32(txt_Drenaje1.Text) + Convert.ToUInt32(txt_Otros1.Text)).ToString();
                else
                    txt_eTotal1.Text = (Convert.ToUInt32(txt_Drenaje1.Text) + Convert.ToUInt32(txt_Otros1.Text)).ToString();
            }
        }

        private void txt_Otros1_TextChanged(object sender, EventArgs e)
        {
            if (txt_Otros1.Text != "")
            {
                if (!valorSvActividad)
                    txt_eTotal1.Text = (Convert.ToUInt32(txt_Orina1.Text) + Convert.ToUInt32(txt_Drenaje1.Text) + Convert.ToUInt32(txt_Otros1.Text)).ToString();
                else
                    txt_eTotal1.Text = (Convert.ToUInt32(txt_Drenaje1.Text) + Convert.ToUInt32(txt_Otros1.Text)).ToString();
            }
        }

        #endregion
        private void refrescarSolicitudes()
        {
            SigniosVit = NegSignosVitales.getSignos(CodigoAtencion);
            gridSol.DataSource = SigniosVit;
            gridSol.Columns["SV_CODIGO"].Visible = false;
            gridSol.Columns["SV_DIA"].Visible = false;
            gridSol.Columns["SV_HOJA"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (gridSol.RowCount > 1)
                HabilitarBotones(true, false, false, true, false);
            else
                HabilitarBotones(true, false, false, false, false);
        }
        private void cmb_dias_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dtpFechaIngreso1.Enabled = false;
            //if (Convert.ToString(cmb_dias.SelectedValue) != "System.Data.DataRowView")
            //{
            //    cargarSignos(Convert.ToInt32(cmb_dias.SelectedValue));
            //    if (Convert.ToInt32(cmb_dias.SelectedValue) == 1)
            //        habilitarContoles(false, true);
            //    else
            //        habilitarContoles(true, false);
            //}
        }

        private void btn_Fecha_Click(object sender, EventArgs e)
        {
            dtpFechaIngreso1.Enabled = true;
        }
        Int32 SV_HOJA = 1;
        private void gridSol_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            DialogResult dialogResult = MessageBox.Show("¿Desea cargar los Signos?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                limpiarSV();
                btnGrabasignos.Visible = true;
                SV_HOJA = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["SV_HOJA"].Value.ToString());
                SV_CODIGO = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["SV_CODIGO"].Value.ToString());
                SVD_DIA = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["SV_DIA"].Value.ToString());
                cargarSignos(Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["SV_DIA"].Value.ToString()));
                HabilitarBotones(false, false, true, true, true);
                habilitarDia(false);
                grb_SignosVitales1.Enabled = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            SigVit = new HC_SIGNOS_VITALES();
            SigVit = NegSignosVitales.CargarDatosSignosVitales(CodigoAtencion, 1);
            if (SigVit != null)
                HabilitarBotones(true, false, true, false, false);
            else
                HabilitarBotones(true, false, false, false, false);

            habilitarDia(false);
            editar = false;
        }

        private void btnGrabasignos_Click(object sender, EventArgs e)
        {
            if (validaDatosAdicionales())
            {
                DateTime dt = Convert.ToDateTime(cmb_frecuencia_hora.Text);
                List<HC_SIGNOS_DATOS_ADICIONALES> lh = NegSignosVitales.ValidaHora(CodigoAtencion, SVD_DIA);
                foreach (var item in lh)
                {
                    if (!editarSV)
                    {
                        if (item.SVD_HORA == dt.TimeOfDay)
                        {
                            MessageBox.Show("Esta hora ya se encuentra registrada para la fecha", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("Signos");
                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                usuario.ShowDialog();
                if (!usuario.aceptado)
                    return;
                HC_SIGNOS_DATOS_ADICIONALES svdat = new HC_SIGNOS_DATOS_ADICIONALES();
                svdat.SV_CODIGO = SV_CODIGO;
                svdat.SVD_HORA = dt.TimeOfDay;
                svdat.SVD_PULSO_AM = txt_Pulso1.Text.Trim();
                svdat.SVD_TEMPERATURA_AM = txt_aTemp1.Text.Trim();
                svdat.SVD_FRESPIRATORIA = txt_aRespiratoria1.Text.Trim();
                svdat.SVD_SISTONICA = txt_aSistonica1.Text.Trim();
                svdat.SVD_DIASTONICA = txt_aDiastonica1.Text.Trim();
                svdat.ID_FRECUENCIA = Convert.ToInt32(cmbFrecuencia.Value);
                if (chkOxigeno.Checked)
                    svdat.SVD_SATURACION = mskSaturacion.Text.Trim() + " - CON OXIGENO";
                else
                    svdat.SVD_SATURACION = mskSaturacion.Text.Trim() + " - AIRE AMBIENTE";
                svdat.ID_USUARIO = usuario.usuarioActual;
                if (editarSV)
                {
                    HC_SIGNOS_DATOS_ADICIONALES da = NegSignosVitales.CargarDatosSignosDatos(SVD_CODIGO);
                    if (da.ID_USUARIO == usuario.usuarioActual)
                    {
                        svdat.SVD_CODIGO = SVD_CODIGO;
                        if (NegSignosVitales.EditarSignosVit(svdat))
                        {
                            limpiarSV();
                            ultraGridSignosVitales.DataSource = NegSignosVitales.CargarSignosAtencion(CodigoAtencion);
                            grb_SignosVitales1.Enabled = false;
                            btnGrabasignos.Visible = false;
                            MessageBox.Show("Signos Vitales almacenados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Los datos no se pudieron almacenar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Solo puede editar el mismo ususario que Registro los Signos Vitales", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    if (NegSignosVitales.GrabarSignosVitalesDat(svdat))
                    {
                        limpiarSV();
                        ultraGridSignosVitales.DataSource = NegSignosVitales.CargarSignosAtencion(CodigoAtencion);
                        MessageBox.Show("Datos almacenados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                        MessageBox.Show("Los datos no se pudieron almacenar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void ultraGridSignosVitales_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            //Dimension los registros
            ultraGridSignosVitales.DisplayLayout.Bands[0].Columns[0].Width = 100;
            //Ocultar los registros
            ultraGridSignosVitales.DisplayLayout.Bands[0].Columns["CODIGO"].Hidden = true;
            ultraGridSignosVitales.DisplayLayout.Bands[0].Columns["P_SISTONICA"].Hidden = true;
            ultraGridSignosVitales.DisplayLayout.Bands[0].Columns["P_DIASTONICA"].Hidden = true;
        }

        private void ultraGridSignosVitales_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            foreach (UltraGridRow item in ultraGridSignosVitales.Rows)
            {
                if (item.Cells["CODIGO"].Value.ToString() == e.Cell.Row.Cells["CODIGO"].Value.ToString())
                {
                    try
                    {
                        DialogResult dialogResult = MessageBox.Show("¿Desea editar los Signos?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (dialogResult == DialogResult.Yes)
                        {
                            SVD_CODIGO = Convert.ToInt32(e.Cell.Row.Cells["CODIGO"].Value.ToString());
                            HC_SIGNOS_DATOS_ADICIONALES svd = NegSignosVitales.CargarDatosSignosDatos(SVD_CODIGO);
                            txt_Pulso1.Text = e.Cell.Row.Cells["F_CARDIACA"].Value.ToString();
                            txt_aTemp1.Text = e.Cell.Row.Cells["TEMPERATURA"].Value.ToString();
                            txt_aSistonica1.Text = e.Cell.Row.Cells["P_SISTONICA"].Value.ToString();
                            txt_aDiastonica1.Text = e.Cell.Row.Cells["P_DIASTONICA"].Value.ToString();
                            txt_aRespiratoria1.Text = e.Cell.Row.Cells["F_RESPIRATORIA"].Value.ToString();
                            string Saturacion = e.Cell.Row.Cells["S_OXIGENO"].Value.ToString();
                            string[] arr = Saturacion.Split('-');
                            mskSaturacion.Text = arr[0].Trim();
                            if (arr[1].Trim() == "AIRE AMBIENTE")
                            {
                                chkOxigeno.Checked = false;
                                chkAmbiente.Checked = true;
                            }
                            else
                            {
                                chkOxigeno.Checked = true;
                                chkAmbiente.Checked = false;
                            }
                            cmbFrecuencia.Value = svd.ID_FRECUENCIA;
                            cmb_frecuencia_hora.Text = Convert.ToString(svd.SVD_HORA);
                            editarSV = true;
                            tabulador.SelectedTab = tabulador.Tabs[0];
                            grb_SignosVitales1.Enabled = true;
                            btnGrabasignos.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //throw;
                    }
                }
            }
        }
        private void modificaDesdeIngestaEliminacio()
        {
            Int32 IE_CODIGO = NegIngestaEliminacion.ExistenciaParaSignosVitales(CodigoAtencion, dtpFechaIngreso1.Value);
            if (IE_CODIGO != null && IE_CODIGO != 0)
            {
                Int32 ING_TOTAL = 0;
                Int32 ELM_TOTAL = 0;
                HC_SIGNOS_VITALES sv = new HC_SIGNOS_VITALES();
                sv.SV_ING_ORAL = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "IO"));
                sv.SV_ING_PARENTAL = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "IP"));
                ING_TOTAL = Convert.ToInt32(sv.SV_ING_ORAL) + Convert.ToInt32(sv.SV_ING_PARENTAL);
                sv.SV_ING_TOTAL = Convert.ToString(ING_TOTAL);
                if (!valorSvActividad)
                    sv.SV_ELM_ORINA = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "EO"));
                sv.SV_ELM_DRENAJE = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "ED"));
                sv.SV_ELM_OTROS = Convert.ToString(NegIngestaEliminacion.SumaTotales(IE_CODIGO, "ER"));
                if (!valorSvActividad)
                    ELM_TOTAL = Convert.ToInt32(sv.SV_ELM_ORINA) + Convert.ToInt32(sv.SV_ELM_DRENAJE) + Convert.ToInt32(sv.SV_ELM_OTROS);
                else
                    ELM_TOTAL = Convert.ToInt32(sv.SV_ELM_DRENAJE) + Convert.ToInt32(sv.SV_ELM_OTROS);
                sv.SV_ELM_TOTAL = Convert.ToString(ELM_TOTAL);
                sv.SV_NUMERO_COMIDAS = Convert.ToString(NegIngestaEliminacion.sumaIngesta(IE_CODIGO, "IO"));
                sv.SV_NUMERO_MEDICIONES = Convert.ToString(NegIngestaEliminacion.sumaOrinaDeposiciones(IE_CODIGO, "EO"));
                sv.SV_NUMERO_DEPOSICIONES = Convert.ToString(NegIngestaEliminacion.sumaOrinaDeposiciones(IE_CODIGO, "EP"));

                if (!NegSignosVitales.EditarDesdeIngestaEliminacion(sv, CodigoAtencion, dtpFechaIngreso1.Value.Date))
                {
                    MessageBox.Show("No se pudierin editar los valores \n desde Ingesta y Eliminacion", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void chkAbsolutoA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAbsolutoA.Checked)
            {
                chkRelativoA.Checked = false;
                chkAmbulatorio.Checked = false;
            }
        }

        private void chkRelativoA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRelativoA.Checked)
            {
                chkAbsolutoA.Checked = false;
                chkAmbulatorio.Checked = false;
            }
        }

        private void chkAplica_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAplica.Checked)
            //    dtpVia.Enabled = false;
            //else
            //    dtpVia.Enabled = true;
        }

        private void chkOxigeno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOxigeno.Checked)
                chkAmbiente.Checked = false;
        }

        private void chkAmbiente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAmbiente.Checked)
                chkOxigeno.Checked = false;
        }

        public void listarCombo()
        {
            List<FRECUENCIA> frecuencia = new List<FRECUENCIA>();
            frecuencia = NegFormulariosHCU.RecuperarFrecuencias();
            cmbFrecuencia.DataSource = frecuencia;
            cmbFrecuencia.DisplayMember = "Detalle";
            cmbFrecuencia.ValueMember = "ID_FRECUENCIA";
            cmbFrecuencia.SelectedIndex = -1;
        }

        private void cmbFrecuencia_SelectionChanged(object sender, EventArgs e)
        {
            int combo = cmbFrecuencia.SelectedIndex + 1;
            List<FRECUENCIA_HORAS> obj = new List<FRECUENCIA_HORAS>();
            obj = NegFormulariosHCU.RecuperarFrecuenciasHoras(combo);
            cmb_frecuencia_hora.DataSource = obj;
            cmb_frecuencia_hora.DisplayMember = "detalle";
            cmb_frecuencia_hora.ValueMember = "id_hora_frecuencia";
            cmb_frecuencia_hora.SelectedIndex = -1;
        }

        private void tsbImprimirSV_Click(object sender, EventArgs e)
        {
            NegCertificadoMedico medico = new NegCertificadoMedico();
            DSForm020 frm = new DSForm020();
            DataRow dr;
            List<DtoExploSignosVitales> dat = NegSignosVitales.CargarSignosAtencion(CodigoAtencion);
            dr = frm.Tables["Form020"].NewRow();
            dr["Logo"] = medico.path();
            dr["Empresa"] = His.Entidades.Clases.Sesion.nomEmpresa;
            dr["Apellido"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
            dr["Nombre"] = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
            dr["HC"] = lblHc.Text; ;
            dr["Sexo"] = paciente.PAC_GENERO;
            frm.Tables["Form020"].Rows.Add(dr);

            foreach (var item in dat)
            {
                dr = frm.Tables["SignosVitales"].NewRow();
                dr["fecha"] = item.FECHA;
                dr["hora"] = item.HORA;
                dr["pulso"] = item.F_CARDIACA;
                dr["temperatura"] = item.TEMPERATURA;
                dr["respiratoria"] = item.F_RESPIRATORIA;
                dr["sistonica"] = item.P_SISTONICA;
                dr["diastonica"] = item.P_DIASTONICA;
                dr["medico"] = item.MEDICO;
                dr["oxigeno"] = item.S_OXIGENO;
                frm.Tables["SignosVitales"].Rows.Add(dr);
            }

            frmReportes x = new frmReportes(1, "SignosVitalesB", frm);
            x.Show();
        }

        private void txt_Peso1_KeyPress(object sender, KeyPressEventArgs e)
        {
            MiMetodofuncionTeclitas(sender, e, txt_Peso1);
        }

        private void txt_Pulso1_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (txt_Pulso1.Text == "" || !NegUtilitarios.ValidaFcardiaca(Convert.ToDouble(txt_Pulso1.Text.Trim())))
            {
                txt_Pulso1.Text = NegParametros.RecuperaValorParSvXcodigo(56).ToString();
                return;
            }
        }

        private void txt_aRespiratoria1_Validating(object sender, CancelEventArgs e)
        {
            if (txt_aRespiratoria1.Text == "" || !NegUtilitarios.ValidaFrespiratoria(Convert.ToDouble(txt_aRespiratoria1.Text.Trim())))
            {
                txt_aRespiratoria1.Text = NegParametros.RecuperaValorParSvXcodigo(58).ToString();
            }
        }

        private void txt_aTemp1_Validating(object sender, CancelEventArgs e)
        {
            if (txt_aTemp1.Text != "" || txt_aTemp1.Text == "0")
            {
                if (!NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txt_aTemp1.Text)))
                {
                    txt_aTemp1.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
                    return;
                }
            }
            else { txt_aTemp1.Enabled = true; }

            if (txt_aTemp1.Text == "")
            {
                txt_aTemp1.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
            }

        }

        private void chkAmbulatorio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAmbulatorio.Checked)
            {
                chkAbsolutoA.Checked = false;
                chkRelativoA.Checked = false;
            }
        }

        private void txt_aTemp1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (txt_aTemp1.Text != "" || txt_aTemp1.Text == "0")
            //{
            //    if (!NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txt_aTemp1.Text)))
            //    {
            //        txt_aTemp1.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
            //        return;
            //    }
            //}
            //else { txt_aTemp1.Enabled = true; }

            //if (txt_aTemp1.Text == "")
            //{
            //    txt_aTemp1.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
            //}
        }

        private void mskSaturacion_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int satura = 0;
                if (mskSaturacion.Text == "")
                {
                    satura = 0;
                }
                else
                    satura = Convert.ToInt16(mskSaturacion.Text.Substring(0, 3));
                if (!NegUtilitarios.ValidaSatOxigeno(Convert.ToDouble(satura)))
                {
                    mskSaturacion.Text = NegParametros.RecuperaValorParSvXcodigo(62).ToString();
                    return;
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        public void grabaDatosExcel(int lg)
        {
            // Crear una instancia de Excel
            Excel.Application excelApp = new Excel.Application();

            // Deshabilitar la visualización de la interfaz de Excel
            excelApp.Visible = false;

            // Abrir el archivo Excel
            LOGOS_EMPRESA logEmp = NegParametros.logosEmpresa(lg);
            Excel.Workbook workbook = excelApp.Workbooks.Open(@"" + logEmp.LEM_RUTA);

            workbook.RefreshAll();
            // Esperar un tiempo para asegurarse de que los datos se actualicen correctamente
            System.Threading.Thread.Sleep(3500); // Puedes ajustar el tiempo según tus necesidades
            // Guardar el archivo
            workbook.Save();
            // Cerrar el archivo y Excel
            workbook.Close();
            excelApp.Quit();

            // Liberar recursos
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
        public void grabarImagenExcel(int lg, int im)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;
            LOGOS_EMPRESA logEmp = NegParametros.logosEmpresa(lg);
            Excel.Workbook workbook = excelApp.Workbooks.Open(@"" + logEmp.LEM_RUTA);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            Excel.ChartObjects chartObjects = (Excel.ChartObjects)worksheet.ChartObjects();
            Excel.ChartObject chartObject = (Excel.ChartObject)chartObjects.Item(1); // Cambia el índice según el gráfico que desees

            logEmp = NegParametros.logosEmpresa(im);
            // Eliminar la imagen en la ruta antes de crear la nueva
            if (File.Exists(logEmp.LEM_RUTA))
            {
                File.Delete(logEmp.LEM_RUTA); // Elimina el archivo.
                Console.WriteLine("El archivo fue eliminado con éxito.");
            }
            // Guardar el gráfico como una imagen JPG
            chartObject.Chart.Export(@"" + logEmp.LEM_RUTA, "JPG");

            workbook.Close(false);
            excelApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            Console.WriteLine("Grafico exportado como imagen.");
        }
        public void cargaInformacion()
        {

        }
        private void tsbImprimirCT_Click(object sender, EventArgs e)
        {
            WaitForm wf = new WaitForm();
            wf.Show();
            DateTime horaCorte = new DateTime(2023, 8, 4, 10, 00, 0);
            List<HC_SIGNOS_DATOS_ADICIONALES> lsv = NegSignosVitales.listaSVdatos(CodigoAtencion);
            Int64 contador = 1;
            Int64 registros = 0;
            Int32 imagen = 1;
            DSForm020 frm = new DSForm020();
            DataRow dr;
            LOGOS_EMPRESA log = NegParametros.logosEmpresa(9);
            if (NegSignosVitales.editarReporteCT())
            {
                foreach (var item in lsv)
                {
                   
                    NegSignosVitales.cargaCurvaTermica(item, contador);
                    if (contador != 1)
                    {
                        contador++;
                        if (item.SVD_HORA == horaCorte.TimeOfDay)
                        {
                            try
                            {
                                contador = 1;
                                //NegSignosVitales.editarReporteCT();
                                grabaDatosExcel(8);
                                grabarImagenExcelCT(8, 9, imagen);
                                dr = frm.Tables["CurvaTermica"].NewRow();
                                dr["path"] = log.LEM_RUTA + imagen + ".png";
                                frm.Tables["CurvaTermica"].Rows.Add(dr);
                                imagen++;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                wf.Close();
                            }

                        }
                    }
                    else
                        contador++;
                    registros++;
                    if (lsv.Count == registros)
                    {
                        try
                        {
                           // NegSignosVitales.editarReporteCTF(1);
                            grabaDatosExcel(8);
                            grabarImagenExcelCT(8, 9, imagen);
                            dr = frm.Tables["CurvaTermica"].NewRow();
                            dr["path"] = log.LEM_RUTA + imagen + ".png";
                            frm.Tables["CurvaTermica"].Rows.Add(dr);
                            imagen++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            wf.Close();
                        }
                    }
                }
            }
            else
                MessageBox.Show("No se pudo crear el grafico \r\n de curva termica", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            //if (NegSignosVitales.editarReporteCT())
            //{
            //    if (NegSignosVitales.cargaCurvaTermica(CodigoAtencion))
            //    {
            //        try
            //        {
            //            grabaDatosExcel(8);
            //            grabarImagenExcel(8, 9);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message);
            //            wf.Close();
            //        }
            //    }
            //    else
            //        MessageBox.Show("No se pudo crear el grafico \r\n de signos vitales", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //    MessageBox.Show("No se pudo crear el grafico \r\n de signos vitales", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //LOGOS_EMPRESA logEmp = NegParametros.logosEmpresa(9);
            //DSForm020 frm = new DSForm020();
            //DataRow dr;
            //dr = frm.Tables["CurvaTermica"].NewRow();
            //dr["path"] = logEmp.LEM_RUTA;
            //frm.Tables["CurvaTermica"].Rows.Add(dr);
            wf.Close();
            frmReportes x = new frmReportes(1, "CurvaTermica", frm);
            x.Show();
        }
        public void grabarImagenExcelCT(int lg, int im, int imagen)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;
            LOGOS_EMPRESA logEmp = NegParametros.logosEmpresa(lg);
            Excel.Workbook workbook = excelApp.Workbooks.Open(@"" + logEmp.LEM_RUTA);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            Excel.ChartObjects chartObjects = (Excel.ChartObjects)worksheet.ChartObjects();
            Excel.ChartObject chartObject = (Excel.ChartObject)chartObjects.Item(1); // Cambia el índice según el gráfico que desees

            logEmp = NegParametros.logosEmpresa(im);
            // Eliminar la imagen en la ruta antes de crear la nueva
            if (File.Exists(logEmp.LEM_RUTA + imagen + ".png"))
            {
                File.Delete(logEmp.LEM_RUTA + imagen + ".png"); // Elimina el archivo.
                Console.WriteLine("El archivo fue eliminado con éxito.");
            }
            // Guardar el gráfico como una imagen PNG
            chartObject.Chart.Export(@"" + logEmp.LEM_RUTA + imagen + ".png", "PNG");

            workbook.Close(false);
            excelApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            Console.WriteLine("Grafico exportado como imagen.");
        }

        private void gridSol_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }
    }
}
