using His.Admision;
using His.Entidades;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace His.ConsultaExterna
{
    public partial class frm_AgendaCitaMedica : Form
    {
        public TextBox Identificacion;
        public TextBox txtCodMedico;
        PACIENTES pac;
        PACIENTES_DATOS_ADICIONALES padicional;
        MEDICOS medico;
        AGENDA_PACIENTE pacienteAgendado;
        public bool re = false;
        public Int64 reCodigo = 0;
        public frm_AgendaCitaMedica()
        {
            InitializeComponent();
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chkCedula.Checked)
            {
                NegUtilitarios.OnlyNumber(e, false);
            }
        }

        public void Reagendar(string codigo)
        {
            AGENDAMIENTO a = NegConsultaExterna.recuperaAgendamiento(Convert.ToInt64(codigo));
            txtNombres.Text = pacienteAgendado.Nombres;
            txtApellidos.Text = pacienteAgendado.Apellidos;
            txtDireccion.Text = pacienteAgendado.Direccion;
            txtTelefono.Text = pacienteAgendado.Telefono;
            txtCelular.Text = pacienteAgendado.Celular;
            txtEmail.Text = pacienteAgendado.Email;



            HABITACIONES hab = NegConsultaExterna.recuperarConsultorio(a.Consultorio);
            HORARIO_ATENCION horario = NegConsultaExterna.recuperarHorarioPorNombre(a.Hora);
            medico = NegConsultaExterna.recuperarMedico(a.Medico);
            dtpCita.Value = a.FechaAgenda;

            if (a.MED_CODIGO != null)
            {
                CargarMedico(Convert.ToInt32(a.MED_CODIGO));
                txtMedico.Text = a.Medico;
                txtEmailMedico.Text = a.EmailMedico;
            }
            else
            {
                cmbEspecialidad.Value = medico.ESPECIALIDADES_MEDICAS.ESP_CODIGO;
                cmbConsultorios.Value = hab.hab_Codigo;
                txtMedico.Text = a.Medico;
                txtEmailMedico.Text = a.EmailMedico;
            }

            txtMotivo.Text = a.MotivoConsulta;
            txtObservacion.Text = a.ObservacionesConsulta;
            re = true;
            reCodigo = Convert.ToInt64(codigo);
        }
        private void btnF1_Click(object sender, EventArgs e)
        {
            try
            {
                re = false;
                reCodigo = 0;
                textBox1.Text = "";
                frmAyudaPacientes form = new frmAyudaPacientes();
                form.campoPadre = textBox1;
                form.ShowDialog();
                form.Dispose();

                if (textBox1.Text != "")
                {
                    txtIdentificacion.Text = textBox1.Text;
                    CargarPaciente();
                    IngresoManual(false);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnCancelar.Enabled = cancelar;
        }
        private void frm_AgendaCitaMedica_Load(object sender, EventArgs e)
        {
            HabilitarBotones(true, false, false);
            CargarEspecialidades();
            ultraGroupBox1.Enabled = false;
            CargaComboConsultorios();
        }
        public void CargarEspecialidades()
        {
            cmbEspecialidad.DataSource = NegMedicos.EspecialidadesCita();
            cmbEspecialidad.DisplayMember = "ESP_NOMBRE";
            cmbEspecialidad.ValueMember = "ESP_CODIGO";

            cmbEspecialidad.Value = 999;
        }
        private void btnCambiar_Click(object sender, EventArgs e)
        {
            Errores.Clear();
            if (btnCambiar.Text == "Cambiar")
            {
                txtEmailMedico.Text = "";
                txtEmailMedico.Enabled = true;
                btnCancelar1.Enabled = true;
                btnCambiar.Text = "Guardar";
            }
            else if (btnCambiar.Text == "Guardar")
            {
                if (txtEmailMedico.Text != "")
                {
                    if (NegUtilitarios.validadorEmail(txtEmailMedico.Text))
                    {
                        txtEmailMedico.Enabled = false;
                        btnCancelar1.Enabled = false;
                        btnCambiar.Text = "Cambiar";
                        dtpCita.Focus();
                    }
                    else
                    {
                        Errores.SetError(txtEmailMedico, "Email no valido.");
                        txtEmailMedico.Text = "";
                        txtEmailMedico.Focus();
                    }
                }

            }
        }
        private void CargaComboConsultorios()
        {
            DataTable consultorios = new DataTable();
            consultorios = NegConsultaExterna.RecuperaConsultorios();
            this.cmbConsultorios.DataSource = consultorios;
            this.cmbConsultorios.DisplayMember = "hab_Numero";
            this.cmbConsultorios.ValueMember = "hab_Codigo";
            cmbConsultorios.SelectedIndex = -1;
        }
        private void btnCancelar1_Click(object sender, EventArgs e)
        {
            txtEmailMedico.Text = medico.MED_EMAIL;
            btnCancelar1.Enabled = false;
            btnCambiar.Text = "Cambiar";
            txtEmailMedico.Enabled = false;
        }
        int validacion = 0;
        private void btnF11_Click(object sender, EventArgs e)
        {
            Errores.Clear();
            if (cmbEspecialidad.SelectedItem == null)
            {
                Errores.SetError(cmbEspecialidad, "No se encontro: " + cmbEspecialidad.Text + " como especialidad.");
                return;
            }
            validacion = 1;
            frm_AyudaMedicos x = new frm_AyudaMedicos(cmbEspecialidad.SelectedItem.DataValue.ToString());
            x.ShowDialog();

            if (x.med_codigo != null)
            {
                if (x.med_codigo != "")
                {
                    CargarMedico(Convert.ToInt32(x.med_codigo));
                    cmbEspecialidad.Focus();
                    CargaComboConsultorios();
                    cmbHorario.Text = "";
                }
            }
        }
        private void CargarMedico(int codMedico)
        {
            medico = NegMedicos.MedicoID(codMedico);

            if (medico != null)
            {
                DataTable med = NegMedicos.MedicoIDValida(codMedico);
                if (med.Rows[0][0].ToString() == "7")
                {
                    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                txtMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                txtEmailMedico.Text = medico.MED_EMAIL;

                cmbEspecialidad.Value = Convert.ToInt32(medico.ESPECIALIDADES_MEDICASReference.EntityKey.EntityKeyValues[0].Value);
                btnCambiar.Enabled = true;

            }
            else
            {
                txtEmailMedico.Text = string.Empty;
                txtMedico.Text = string.Empty;
                btnCambiar.Enabled = false;
            }
        }

        private void chkCedula_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCedula.Checked)
            {
                txtIdentificacion.Text = "";
                chkPasaporte.Checked = false;
                txtIdentificacion.MaxLength = 10;
            }
            else
            {
                txtIdentificacion.Text = "";
                chkPasaporte.Checked = true;
                txtIdentificacion.MaxLength = 30;
            }
        }

        private void chkPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPasaporte.Checked)
            {
                txtIdentificacion.Text = "";
                chkCedula.Checked = false;
                txtIdentificacion.MaxLength = 30;
            }
            else
            {
                txtIdentificacion.Text = "";
                chkCedula.Checked = true;
                txtIdentificacion.MaxLength = 10;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ultraGroupBox1.Enabled = true;
            NumeroCita();
            HabilitarBotones(false, true, true);
        }
        public void NumeroCita()
        {
            DataTable numeroConsulta = new DataTable();
            numeroConsulta = NegConsultaExterna.RecuperaNumAgenda();
            lblNumero.Text = "No. " + numeroConsulta.Rows[0][0].ToString();
        }
        public void CargarPaciente()
        {
            if (chkCedula.Checked)
            {
                List<DtoAgendados> agendados = new List<DtoAgendados>();
                if (txtIdentificacion.Text.Trim() != "")
                {
                    agendados = NegConsultaExterna.reagendar(txtIdentificacion.Text.Trim());
                    DataTable paciente = new DataTable();
                    paciente = NegConsultaExterna.BuscaPaciente(txtIdentificacion.Text);
                    if (agendados.Count > 0)
                    {
                        if (MessageBox.Show("Paciente ya tiene citas agendadas, le gustaria reagendar alguna?",
                            "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //muestro las agendas del paciente 
                            pacienteAgendado = NegConsultaExterna.recuperarPacienteAgendado(txtIdentificacion.Text.Trim());
                            frm_Reagendamiento x = new frm_Reagendamiento(agendados, pacienteAgendado.Apellidos.Trim() + " " + pacienteAgendado.Nombres.Trim());
                            x.ShowDialog();

                            if (x.codigo != "")
                            {
                                Reagendar(x.codigo);
                            }
                        }
                        else
                        {
                            if (NegValidaciones.esCedulaValida(txtIdentificacion.Text))
                            {

                                if (paciente.Rows.Count > 0)
                                {
                                    IngresoManual(false);
                                    txtNombres.Text = paciente.Rows[0][0].ToString();
                                    txtApellidos.Text = paciente.Rows[0][1].ToString();
                                    txtEmail.Text = paciente.Rows[0][2].ToString();
                                    txtTelefono.Text = paciente.Rows[0][3].ToString();
                                    txtCelular.Text = paciente.Rows[0][4].ToString();
                                    txtDireccion.Text = paciente.Rows[0][5].ToString();
                                }
                                else
                                {
                                    pacienteAgendado = NegConsultaExterna.recuperarPacienteAgendado(txtIdentificacion.Text.Trim());
                                    txtNombres.Text = pacienteAgendado.Nombres;
                                    txtApellidos.Text = pacienteAgendado.Apellidos;
                                    txtDireccion.Text = pacienteAgendado.Direccion;
                                    txtTelefono.Text = pacienteAgendado.Telefono;
                                    txtCelular.Text = pacienteAgendado.Celular;
                                    txtEmail.Text = pacienteAgendado.Email;

                                }
                            }
                            else
                            {
                                MessageBox.Show("Cédula Incorrecta", "HIS3000", MessageBoxButtons.OK);
                                txtIdentificacion.Text = "";
                                txtIdentificacion.Focus();
                            }
                        }
                    }
                    else if (NegValidaciones.esCedulaValida(txtIdentificacion.Text))
                    {

                        if (paciente.Rows.Count > 0)
                        {
                            IngresoManual(false);
                            txtNombres.Text = paciente.Rows[0][0].ToString();
                            txtApellidos.Text = paciente.Rows[0][1].ToString();
                            txtEmail.Text = paciente.Rows[0][2].ToString();
                            txtTelefono.Text = paciente.Rows[0][3].ToString();
                            txtCelular.Text = paciente.Rows[0][4].ToString();
                            txtDireccion.Text = paciente.Rows[0][5].ToString();
                        }
                        else
                        {
                            IngresoManual(false);
                            DatosPersonales();
                            txtNombres.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cédula Incorrecta", "HIS3000", MessageBoxButtons.OK);
                        txtIdentificacion.Text = "";
                        txtIdentificacion.Focus();
                    }
                }
            }
            else
            {
                List<DtoAgendados> agendados = new List<DtoAgendados>();
                if (txtIdentificacion.Text.Trim() != "")
                {
                    agendados = NegConsultaExterna.reagendar(txtIdentificacion.Text.Trim());
                    DataTable paciente = new DataTable();
                    paciente = NegConsultaExterna.BuscaPaciente(txtIdentificacion.Text);
                    if (agendados.Count > 0)
                    {
                        if (MessageBox.Show("Paciente ya tiene citas agendadas, le gustaria reagendar alguna?",
                            "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //muestro las agendas del paciente 
                            pacienteAgendado = NegConsultaExterna.recuperarPacienteAgendado(txtIdentificacion.Text.Trim());
                            frm_Reagendamiento x = new frm_Reagendamiento(agendados, pacienteAgendado.Apellidos.Trim() + " " + pacienteAgendado.Nombres.Trim());
                            x.ShowDialog();

                            if (x.codigo != "")
                            {
                                Reagendar(x.codigo);
                            }
                        }
                        else
                        {
                            if (paciente.Rows.Count > 0)
                            {
                                IngresoManual(false);
                                txtNombres.Text = paciente.Rows[0][0].ToString();
                                txtApellidos.Text = paciente.Rows[0][1].ToString();
                                txtEmail.Text = paciente.Rows[0][2].ToString();
                                txtTelefono.Text = paciente.Rows[0][3].ToString();
                                txtCelular.Text = paciente.Rows[0][4].ToString();
                                txtDireccion.Text = paciente.Rows[0][5].ToString();
                            }
                            else
                            {
                                IngresoManual(false);
                                DatosPersonales();
                                txtNombres.Focus();
                            }
                        }
                    }
                    else
                    {
                        if (paciente.Rows.Count > 0)
                        {
                            IngresoManual(false);
                            txtNombres.Text = paciente.Rows[0][0].ToString();
                            txtApellidos.Text = paciente.Rows[0][1].ToString();
                            txtEmail.Text = paciente.Rows[0][2].ToString();
                            txtTelefono.Text = paciente.Rows[0][3].ToString();
                            txtCelular.Text = paciente.Rows[0][4].ToString();
                            txtDireccion.Text = paciente.Rows[0][5].ToString();
                        }
                        else
                        {
                            IngresoManual(false);
                            DatosPersonales();
                            txtNombres.Focus();
                        }
                    }
                }
            }
        }
        public void IngresoManual(bool only)
        {
            txtNombres.ReadOnly = only;
            txtApellidos.ReadOnly = only;
            txtDireccion.ReadOnly = only;
            txtTelefono.ReadOnly = only;
            txtCelular.ReadOnly = only;
            txtEmail.ReadOnly = only;
        }
        private void txtIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                CargarPaciente();
        }

        private void txtIdentificacion_Leave(object sender, EventArgs e)
        {
            //if(txtIdentificacion.Text.Trim() != "")
            //CargarPaciente();
        }

        public void ValidarHorario()
        {
            if (cmbConsultorios.SelectedItem != null)
            {
                cmbHorario.Text = "";
                DateTime Hoy = DateTime.Now;
                if (dtpCita.Value.Date == Hoy.Date)
                {
                    if (Hoy.Hour < 12)
                    {
                        cmbHorario.DataSource = NegConsultaExterna.HorariosDisponibles("A", cmbConsultorios.Text, dtpCita.Value.Date, txtMedico.Text.Trim(), txtIdentificacion.Text.Trim());
                        cmbHorario.DisplayMember = "Horarios";
                        cmbHorario.ValueMember = "Id_Horarios";
                    }
                    else
                    {
                        cmbHorario.DataSource = NegConsultaExterna.HorariosDisponibles("A", cmbConsultorios.Text, dtpCita.Value.Date, txtMedico.Text.Trim(), txtIdentificacion.Text.Trim());
                        cmbHorario.DisplayMember = "Horarios";
                        cmbHorario.ValueMember = "Id_Horarios";
                    }
                }
                else
                {
                    cmbHorario.DataSource = NegConsultaExterna.HorariosDisponibles("", cmbConsultorios.Text, dtpCita.Value.Date, txtMedico.Text.Trim(), txtIdentificacion.Text.Trim());
                    cmbHorario.DisplayMember = "Horarios";
                    cmbHorario.ValueMember = "Id_Horarios";
                }
            }
        }

        private void cmbConsultorios_ValueChanged(object sender, EventArgs e)
        {
            if (cmbConsultorios.SelectedIndex != -1)
            {
                if (dtpCita.Value.Date <= DateTime.Now.Date)
                {
                    if (dtpCita.Value.Hour <= 19)
                        ValidarHorario();
                    else
                    {
                        cmbConsultorios.Enabled = true;
                        cmbHorario.Enabled = true;
                        MessageBox.Show("Actualmente no hay horarios disponibles\r\nIntente con otro consultorio o con otra fecha.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                    ValidarHorario();
            }
        }

        private void dtpCita_ValueChanged(object sender, EventArgs e)
        {
            if (dtpCita.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("No puede agendar una cita con fecha menor a la actual.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpCita.Value = DateTime.Now;
                cmbConsultorios.SelectedItem = null;
                cmbHorario.SelectedItem = null;
                cmbConsultorios.Enabled = true;
                cmbHorario.Enabled = true;
            }
            else
            {
                CargaComboConsultorios();
                cmbHorario.Text = "";
            }
        }
        public void DatosPersonales()
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCelular.Text = "";
            txtEmail.Text = "";
        }
        public void LimpiarCampos()
        {
            txtIdentificacion.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCelular.Text = "";
            txtEmail.Text = "";
            lblNumero.Text = "No. ";
            txtMedico.Text = "";
            cmbEspecialidad.Value = 999;
            txtEmailMedico.Text = "";
            btnCambiar.Enabled = false;
            btnCancelar1.Enabled = false;
            txtMotivo.Text = "";
            txtObservacion.Text = "";
            textBox1.Text = "";
            cmbHorario.Text = "";
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de Cancelar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                LimpiarCampos();
                HabilitarBotones(true, false, false);
                ultraGroupBox1.Enabled = false;
                CargaComboConsultorios();
            }
        }

        public bool ValidarCampos()
        {
            Errores.Clear();
            bool valido = true;
            if (txtIdentificacion.Text.Trim() == "")
            {
                Errores.SetError(txtIdentificacion, "Debe elegir un paciente.");
                valido = false;
            }
            if (chkCedula.Checked)
            {
                if (txtIdentificacion.Text.Trim() != "")
                {
                    if (!NegValidaciones.esCedulaValida(txtIdentificacion.Text.Trim()))
                    {
                        Errores.SetError(txtIdentificacion, "Cedula no valida.");
                        valido = false;
                    }
                }
            }
            if (txtMedico.Text.Trim() == "")
            {
                Errores.SetError(txtMedico, "Debe elegir un medico");
                valido = false;
            }
            if (txtEmailMedico.Text.Trim() == "")
            {
                Errores.SetError(txtEmailMedico, "Debe ingresar el correo del medico.");
                valido = false;
            }
            if (dtpCita.Value.Date < DateTime.Now.Date)
            {
                Errores.SetError(dtpCita, "Fecha de cita no puede ser menor a la fecha actual");
                valido = false;
            }
            if (cmbConsultorios.SelectedItem == null)
            {
                Errores.SetError(dtpCita, "Debe elegir un consultorio.");
                valido = false;
            }
            if (cmbHorario.SelectedItem == null)
            {
                Errores.SetError(cmbHorario, "Debe eleguir el horario para la cita medica");
                valido = false;
            }
            if (txtEmailMedico.Text.Trim() != "")
            {
                if (!NegUtilitarios.validadorEmail(txtEmailMedico.Text.Trim()))
                {
                    Errores.SetError(txtEmailMedico, "Email no valido");
                    valido = false;
                }
            }
            return valido;
        }
        public bool GuardaCita()
        {
            try
            {
                if (NegConsultaExterna.GuardaAgendamientoPaciente(txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text, txtEmail.Text, txtTelefono.Text, txtCelular.Text, txtDireccion.Text, dtpCita.Value, cmbEspecialidad.Text, txtMedico.Text, txtEmailMedico.Text, cmbConsultorios.Text, cmbHorario.Text, txtMotivo.Text, txtObservacion.Text, medico.MED_TELEFONO_CELULAR, medico.MED_CODIGO))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }
        public static Boolean EnviaEmailOutlook(string mailPaciente, string mailDr, string mailSubject, string mailContent)
        {
            try
            {
                var oApp = new Outlook.Application();

                Outlook.NameSpace ns = oApp.GetNamespace("MAPI");
                var f = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);

                System.Threading.Thread.Sleep(1000);

                var mailItem = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                mailItem.Subject = mailSubject;
                mailItem.HTMLBody = mailContent;
                mailItem.To = mailPaciente;
                mailItem.CC = mailDr;
                mailItem.Send();

            }
            catch
            {
                return false;
            }

            return true;
        }
        public static Boolean creaCitaOutlook(Outlook._Application olApp, string adicionales, string consultorio, string paciente, DateTime dt)
        {
            try
            {
                // usar el objeto de outlook para crear el recordatorio
                Outlook._AppointmentItem apt = (Outlook._AppointmentItem)
                olApp.CreateItem(Outlook.OlItemType.olAppointmentItem);

                // algunas propiedades
                apt.Subject = paciente;
                apt.Body = adicionales;
                apt.Start = dt;
                apt.End = dt.AddHours(0.5);

                //apt.ReminderMinutesBeforeStart = 24 * 60 * 7 * 1; // una semana antes
                apt.ReminderMinutesBeforeStart = 30;

                // Hacer que aparezca con negrita en el calendario!
                apt.BusyStatus = Outlook.OlBusyStatus.olBusy;

                apt.AllDayEvent = false;
                apt.Location = consultorio;

                apt.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    if (MessageBox.Show("La Cita Se Va A Generar Y Enviar A Los Destinatarios\r\n ¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        if (GuardaCita())
                        {
                            MessageBox.Show("Agendamiento Guardado Con Exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            try
                            {
                                EMPRESA empresa = NegEmpresa.RecuperaEmpresa();
                                Outlook._Application olApp = new Outlook.Application();
                                Outlook.NameSpace mapiNS = olApp.GetNamespace("MAPI");
                                string profile = "";
                                mapiNS.Logon(profile, null, null, null);
                                int año = dtpCita.Value.Year;
                                int mes = dtpCita.Value.Month;
                                int dia = dtpCita.Value.Day;
                                string[] horas = cmbHorario.Text.Split('-');
                                string[] horafinal = horas[0].Split(':');
                                int hora = Convert.ToInt16(horafinal[0]);
                                int minutos = Convert.ToInt16(horafinal[1]);
                                string cuerpoCorreo;
                                cuerpoCorreo = "Paciente: " + txtNombres.Text + " " + txtApellidos.Text + "\r\n";
                                cuerpoCorreo += "<br>" + "Cita Médica Agendada: " + año + "/" + mes + "/" + dia + " a las " + horas[0] + "\r\n";
                                cuerpoCorreo += "<br>" + "Cita Médica con el/la Dr./Dra. " + txtMedico.Text + "\r\n";
                                cuerpoCorreo += "<br>" + "En La Especialidad De:" + cmbEspecialidad.Text + "\r\n";
                                cuerpoCorreo += "<br>" + "Motivo: " + txtMotivo.Text + "\r\n";
                                cuerpoCorreo += "<br>" + "Observación: " + txtObservacion.Text + "\r\n";
                                cuerpoCorreo += "<br>" + "Consultorio: " + cmbConsultorios.Text + "\r\n";
                                cuerpoCorreo += "<br>" + "Cita Generada por: " + His.Entidades.Clases.Sesion.nomUsuario;
                                if (!EnviaEmailOutlook(txtEmail.Text, txtEmailMedico.Text, empresa.EMP_NOMBRE + ", Cita Médica Agendada", "Paciente: " + txtNombres.Text + " " + txtApellidos.Text + "\r\nCita Médica con el/la Dr./Dra. " + txtMedico.Text + "\r\nEn La Especialidad De:" + cmbEspecialidad.Text + "\r\nMotivo: " + txtMotivo.Text + "\r\nConsultorio: " + cmbConsultorios.Text + " \r\nFecha: " + año + "/" + mes + "/" + dia + " a las " + horas[0] + "\r\nCita Generada por: " + His.Entidades.Clases.Sesion.nomUsuario))
                                    MessageBox.Show("Enviar Mensaje Manualmente Y Solicite Soporte Tecnico Outlook Fuera de Línea", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (!creaCitaOutlook(olApp, "Cita Médica Dr./Dra. " + txtMedico.Text + "\r\nEn La Especialidad De:" + cmbEspecialidad.Text + "\r\nMotivo: " + txtMotivo.Text + "\r\nCita Generada por: " + His.Entidades.Clases.Sesion.nomUsuario, "\r\nConsultorio: " + cmbConsultorios.Text, "Consultorio: " + cmbConsultorios.Text + ", Paciente: " + txtNombres.Text + " " + txtApellidos.Text, new DateTime(año, mes, dia, hora, minutos, 0)))
                                    MessageBox.Show("Verifique Su Outlook La Cita No Se Almaceno En Su Calendario", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //limpio los campos
                                LimpiarCampos();
                                HabilitarBotones(true, false, false);
                                ultraGroupBox1.Enabled = false;
                                CargaComboConsultorios();
                            }
                            catch
                            {
                                MessageBox.Show("Enviar Mensaje Manualmente Y Solicite Soporte Técnico, Outlook Fuera de Línea", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            if (reCodigo != 0)
                            {
                                if (!NegConsultaExterna.EliminarCita(reCodigo))
                                    MessageBox.Show("Algo ocurrio al liberar consultorio anterior", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                            MessageBox.Show("Agendamiento No Guardado, Consulte Con Sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Verifique la existencia de Outlook en esta maquina o que el mismo este parametrizado de forma correcta.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbEspecialidad_ValueChanged(object sender, EventArgs e)
        {
            if (validacion == 0)
            {
                txtEmailMedico.Text = "";
                txtMedico.Text = "";
                //medico = null;
            }
            else
                validacion = 0;
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            Errores.Clear();
            if (e.KeyCode == Keys.Enter)
            {
                if (txtEmail.Text.Trim() != "")
                {
                    if (!NegUtilitarios.validadorEmail(txtEmail.Text.Trim()))
                    {
                        Errores.SetError(txtEmail, "Email no valido");
                        txtEmail.Text = "";
                        txtEmail.Focus();
                    }
                    else
                        btnF1.Focus();
                }
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtNombres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtApellidos.Focus();
        }

        private void txtApellidos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDireccion.Focus();
        }

        private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTelefono.Focus();
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCelular.Focus();
        }

        private void txtCelular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtEmail.Focus();
        }

        private void cmbConsultorios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbHorario.Focus();
        }

        private void cmbHorario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMotivo.Focus();
        }

        private void txtMotivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtObservacion.Focus();
        }

        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            frm_AyudaMedicos x = new frm_AyudaMedicos("999");
            x.ShowDialog();
        }
    }
}
