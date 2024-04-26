using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Admision;
using His.Entidades.Clases;
using His.Entidades;
using His.Negocio;
using System.Text.RegularExpressions;
using Outlook=Microsoft.Office.Interop.Outlook;


namespace His.ConsultaExterna
{
    public partial class frm_AgendaCita : Form
    {
        public TextBox txtCodMedico = new TextBox();
        MEDICOS medico = null;
        public frm_AgendaCita()
        {
            InitializeComponent();
            lblFechaActual.Text = DateTime.Today.ToString("dd/MM/yyyy");
            CargaComboEspecialidades();
            CargaComboConsultorios();
        }

        #region OBJETOS Y CLASES
        private void CargaComboEspecialidades()
        {
            DataTable especialidades = new DataTable();
            especialidades = NegConsultaExterna.RecuperaEspecialidades();            
            this.cmbEspecialidades.DataSource = especialidades;
            this.cmbEspecialidades.DisplayMember = "ESP_NOMBRE";
            this.cmbEspecialidades.ValueMember = "ESP_CODIGO";
            cmbEspecialidades.SelectedIndex = -1;
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
                lblMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                lblMailMed.Text = medico.MED_EMAIL;
                lblCodMedico.Text = txtCodMedico.Text;
                lblMedico.Visible = true;
                lblMailMed.Visible = true;
                btnCambiaEmal.Visible = true;
            }
            else
                lblMedico.Text = string.Empty;
        }

        public static bool ValidaEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
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

        private void txtKeyPress(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public bool Valida()
        {
            if (txtNombres.Text == "")
            {
                ValidaError(txtNombres, "Ingrese Nombre Del Paciente");
                return false;
            }
            error.Clear();
            if (txtApellidos.Text == "")
            {
                ValidaError(txtApellidos, "Ingrese Apellido Del Paciente");
                return false;
            }
            error.Clear();            
            if (txtCelular.Text == "")
            {
                ValidaError(txtCelular, "Ingrese Celular Del Paciente");
                return false;
            }
            error.Clear();
            if (txtEmail.Text == "")
            {
                ValidaError(txtEmail, "Ingrese E-mail Del Paciente");
                return false;
            }
            error.Clear();
            if (txtIdentificacion.Text == "")
            {
                ValidaError(txtIdentificacion, "Ingrese Identificación Del Paciente");
                return false;
            }
            error.Clear();
            if (txtDireccion.Text == "")
            {
                ValidaError(txtDireccion, "Ingrese Dirección Del Paciente");
                return false;
            }
            error.Clear();
            if (cmbEspecialidades.Text == "")
            {
                ValidaError(cmbEspecialidades, "Escoja Una Especialidad");
                return false;
            }
            error.Clear();
            if (lblMedico.Text == "NOMBRE MEDICO")
            {
                ValidaError(gbDr, "Seleccione Médico");
                return false;
            }
            
            if (cmbConsultorios.Text == "")
            {
                ValidaError(cmbConsultorios, "Seleccione Un Consultorio");
                return false;
            }
            error.Clear();
            if (cmbHora.Text == "")
            {
                ValidaError(cmbHora, "Seleccione Una Hora");
                return false;
            }
            error.Clear();
            if (txtMotivo.Text == "")
            {
                ValidaError(txtMotivo, "Ingrese Motivo Para Consulta");
                return false;
            }
            error.Clear();
            if(txtNuevoEmailDr.Visible)
            {
                lblMailMed.Text = txtNuevoEmailDr.Text;
            }
            if(txtNuevoEmailDr.Visible && txtNuevoEmailDr.Text=="")
            {
                ValidaError(txtNuevoEmailDr, "Ingrese Nuevo E-mail Del Dr/Dra");
            }
            if (lblMailMed.Text == "")
            {
                ValidaError(gbDr, "Ingrese Correo de Médico");
                return false;
            }
            error.Clear();
            return true;
        }
        public void ValidaError(Control control, string campo)
        {
            error.SetError(control, campo);
        }

        public void LimpiaCampos()
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtTelefono.Text = "";
            txtCelular.Text = "";
            txtEmail.Text = "";
            txtIdentificacion.Text = "";
            txtDireccion.Text = "";
            txtMotivo.Text = "";
            txtNotas.Text = "";
            txtNuevoEmailDr.Text = "";
            txtNuevoEmailDr.Visible = false;
            lblMailMed.Visible = false;
            rdbCedula.Checked = true;
            rdbPasaporte.Checked = false;
            rdbManiana.Checked = false;
            rdbTarde.Checked = false;
            CargaComboEspecialidades();
            CargaComboConsultorios();
            lblMedico.Text = "NOMBRE MEDICO";
            lblMedico.Visible = false;
            cmbHora.SelectedIndex = -1;
            cmbHora.Enabled = false;
            btnCambiaEmal.Visible = false;
        }

        public void LlenaHorario()
        {
            DataTable horario = new DataTable();
            string tiempo;
            if (rdbManiana.Checked)
                tiempo = "M";
            else
                tiempo = "T";
            horario = NegConsultaExterna.RecuperaHorario(tiempo, dtpFechaCita.Value, cmbConsultorios.Text);
            this.cmbHora.DataSource = horario;
            this.cmbHora.DisplayMember = "HORARIOS";
            this.cmbHora.ValueMember = "ID_HORARIOS";
            cmbHora.SelectedIndex = -1;
            cmbHora.Enabled = true;
        }

        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            //BUSCA EN LA COLECCION EL FORMULARIO
            formulario = panel2.Controls.OfType<MiForm>().FirstOrDefault();
            //VERIFICO SI EL FORMULARIO EXISTE O NO EXISTE PARA NO VOLVER ABRIRLO SI NO SOLO LLAMARLO
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panel2.Controls.Add(formulario);
                panel2.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                //formulario.FormClosed += new FormClosedEventHandler(CloseForm);
            }
            else
            {
                formulario.BringToFront();
            }
        }


        public static Boolean EnviaEmailOutlook(string mailPaciente,string mailDr, string mailSubject, string mailContent)
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

        public bool GuardaCita()
        {
            try
            {
                if (NegConsultaExterna.GuardaAgendamientoPaciente(txtIdentificacion.Text, txtNombres.Text, txtApellidos.Text, txtEmail.Text, txtTelefono.Text, txtCelular.Text, txtDireccion.Text, dtpFechaCita.Value, cmbEspecialidades.Text, lblMedico.Text, lblMailMed.Text, cmbConsultorios.Text, cmbHora.Text, txtMotivo.Text, txtNotas.Text, medico.MED_TELEFONO_CELULAR, medico.MED_CODIGO))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
            
        }
        #endregion

        private void btnBuscaMedico_Click(object sender, EventArgs e)
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

        private void txtIdentificacion_Leave(object sender, EventArgs e)
        {
            if (rdbCedula.Checked)
            {
                if (NegValidaciones.esCedulaValida(txtIdentificacion.Text))
                {
                    DataTable paciente = new DataTable();
                    paciente = NegConsultaExterna.BuscaPaciente(txtIdentificacion.Text);
                    if (paciente.Rows.Count > 0)
                    {
                        txtNombres.Text = paciente.Rows[0][0].ToString();
                        txtApellidos.Text = paciente.Rows[0][1].ToString();
                        txtEmail.Text = paciente.Rows[0][2].ToString();
                        txtTelefono.Text = paciente.Rows[0][3].ToString();
                        txtCelular.Text = paciente.Rows[0][4].ToString();
                        txtDireccion.Text = paciente.Rows[0][5].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Cédula Incorrecta", "HIS3000", MessageBoxButtons.OK);
                    txtIdentificacion.Text = "";
                }
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (ValidaEmail(txtEmail.Text) == false)
            {
                MessageBox.Show("Correo Electronico Invalido", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Text = "";
            }
        }

        private void rdbCedula_CheckedChanged(object sender, EventArgs e)
        {
            txtIdentificacion.Enabled = true;
            LimpiaCampos();
        }

        private void rdbPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            txtIdentificacion.Enabled = true;
            LimpiaCampos();
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtCelular.Focus();
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtDireccion.Focus();
            }
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(rdbCedula.Checked)
                txtKeyPress(e);            
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtNombres.Focus();
            }
        }
       
        public void NumeroCita()
        {
            DataTable numeroConsulta = new DataTable();
            numeroConsulta = NegConsultaExterna.RecuperaNumAgenda();
            txtNumAgenda.Text = numeroConsulta.Rows[0][0].ToString();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                if (MessageBox.Show("La Cita Se Va A Generar Y Enviar A Los Destinatarios\r\n ¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Outlook._Application olApp = new Microsoft.Office.Interop.Outlook.Application();
                    Outlook.NameSpace mapiNS = olApp.GetNamespace("MAPI");
                    string profile = "";
                    mapiNS.Logon(profile, null, null, null);
                    int año = dtpFechaCita.Value.Year;
                    int mes = dtpFechaCita.Value.Month;
                    int dia = dtpFechaCita.Value.Day;
                    string[] horas = cmbHora.Text.Split('-');
                    string[] horafinal = horas[0].Split(':');
                    int hora = Convert.ToInt16(horafinal[0]);
                    int minutos = Convert.ToInt16(horafinal[1]);
                    string cuerpoCorreo;
                    cuerpoCorreo = "Paciente: " + txtNombres.Text + " " + txtApellidos.Text;
                    cuerpoCorreo += "<br>" + "Cita Medica Agendada para el: " + año + "/" + mes + "/" + dia + " a las " + horas[0];
                    cuerpoCorreo += "<br>" + "Cita Medica con el/la Dr./Dra. " + lblMedico.Text;
                    cuerpoCorreo += "<br>" + "En La Especialidad De:" + cmbEspecialidades.Text;
                    cuerpoCorreo += "<br>" + "Motivo: " + txtMotivo.Text;
                    cuerpoCorreo += "<br>" + "Observación: " + txtNotas.Text;
                    cuerpoCorreo += "<br>" + "Consultorio: " + cmbConsultorios.Text;
                    cuerpoCorreo += "<br>" + "Cita Generada por: " + Sesion.nomUsuario;
                    if (GuardaCita())
                    {
                        MessageBox.Show("Agendamiento Guardado Con Exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {   if (!EnviaEmailOutlook(txtEmail.Text, lblMailMed.Text, "Cita Medica Agendada para el: " + año + "/" + mes + "/" + dia + " a las " + horas[0], "Paciente: " + txtNombres.Text + " " + txtApellidos.Text + "\r\nCita Medica con el/la Dr./Dra. " + lblMedico.Text + "\r\nEn La Especialidad De:" + cmbEspecialidades.Text + "\r\nMotivo: " + txtMotivo.Text + "\r\nConsultorio: " + cmbConsultorios.Text + "\r\nCita Generada por: " + Sesion.nomUsuario))
                            //if (!EnviaEmailOutlook(txtEmail.Text, "", "Cita Medica Agendada para el: " + año + "/" + mes + "/" + dia + " a las " + horas[0], cuerpoCorreo))
                                MessageBox.Show("Enviar Mensaje Manualmente Y Solicite Soporte Tecnico Outlook Fuera de Línea", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (!creaCitaOutlook(olApp, "Cita Medica Dr./Dra. " + lblMedico.Text + "\r\nEn La Especialidad De:" + cmbEspecialidades.Text + "\r\nMotivo: " + txtMotivo.Text + "\r\nCita Generada por: " + Sesion.nomUsuario, "\r\nConsultorio: " + cmbConsultorios.Text, "Consultorio: " + cmbConsultorios.Text + ", Paciente: " + txtNombres.Text + " " + txtApellidos.Text, new DateTime(año, mes, dia, hora, minutos, 0)))
                                MessageBox.Show("Verifique Su Outlook La Cita No Se Almaceno En Su Calendario", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            MessageBox.Show("Enviar Mensaje Manualmente Y Solicite Soporte Técnico, Outlook Fuera de Línea", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    
                        cmbHora.Text = "";
                        cmbConsultorios.Text = "";
                        dtpFechaCita.Focus();
                    }
                    else
                        MessageBox.Show("Agendamiento No Guardado, Consulte Con Sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Si Continua Va perder Todos Los Datos Ingresados!!!","HIS3000",MessageBoxButtons.OKCancel,MessageBoxIcon.Information)==DialogResult.OK)
            {
                LimpiaCampos();
                txtIdentificacion.Focus();
            }
        }

        private void rdbManiana_CheckedChanged(object sender, EventArgs e)
        {
            LlenaHorario();
        }

        private void rdbTarde_CheckedChanged(object sender, EventArgs e)
        {
            LlenaHorario();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Esta Seguro De Cerrar Agenda De Citas Medicas","HIS3000",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnHistorias_Click(object sender, EventArgs e)
        {            
            //AbrirFormulario<frm_ExploradorFormularios>();
            //frm_ExploradorFormularios Procedimientos = new frm_ExploradorFormularios();            
            //Procedimientos.Show();
        }
        
        private void txtNuevoEmailDr_Leave(object sender, EventArgs e)
        {
            if (!ValidaEmail(txtNuevoEmailDr.Text))
            {
                MessageBox.Show("Correo Electronico Invalido", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNuevoEmailDr.Text = "";
            }
        }

        private void btnCambiaEmal_Click(object sender, EventArgs e)
        {
            lblMailMed.Visible = false;
            txtNuevoEmailDr.Visible = true;
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {           
            LimpiaCampos();
            NumeroCita();
            txtIdentificacion.Focus();
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtApellidos.Focus();
            }
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtTelefono.Focus();
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtMotivo.Focus();
            }
        }

        private void frm_AgendaCita_Load(object sender, EventArgs e)
        {
            NumeroCita();
            
            txtIdentificacion.Focus();
        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscaPaciente_Click(object sender, EventArgs e)
        {
            try
            {                
                frmAyudaPacientes form = new frmAyudaPacientes();
                form.campoPadre = txtIdentificacion;
                form.ShowDialog();
                form.Dispose();                
            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbConsultorios_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dtpFechaCita_ValueChanged(object sender, EventArgs e)
        {
            cmbHora.Text = "";
            LlenaHorario();
        }
    }
}
