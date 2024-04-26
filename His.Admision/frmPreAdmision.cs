using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using His.Formulario;

namespace His.Admision
{
    public partial class frmPreAdmision : Form
    {
        PREADMISION preadmision = new PREADMISION();
        List<PREADMISION_DETALLE> detalle = new List<PREADMISION_DETALLE>();
        ATENCIONES atencion = new ATENCIONES();
        bool edicion = false;
        public frmPreAdmision()
        {
            InitializeComponent();
            dtpFecha.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpFecha.Format = DateTimePickerFormat.Custom;
            List<TIPO_REFERIDO> referido = new List<TIPO_REFERIDO>();
            referido = NegTipoReferido.listaTipoReferido();
            TIPO_REFERIDO excluir = referido.FirstOrDefault(x => x.TIR_CODIGO == 3);
            referido.Remove(excluir);
            cmbReferido.DataSource = referido;
            cmbReferido.ValueMember = "TIR_CODIGO";
            cmbReferido.DisplayMember = "TIR_NOMBRE";

            List<TIPO_TRATAMIENTO> tratamiento = new List<TIPO_TRATAMIENTO>();
            tratamiento = NegTipoTratamiento.RecuperaTipoTratamiento();

            cmbTrtamiento.DataSource = tratamiento;
            cmbTrtamiento.DisplayMember = "TIA_DESCRIPCION";
            cmbTrtamiento.ValueMember = "TIA_CODIGO";

            ultraGroupBox1.Enabled = false;

            if (chkCedula.Checked)
                txtIdentificacion.MaxLength = 10;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chkCedula.Checked)
                NegUtilitarios.OnlyNumber(e, false);
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool imprimir, bool estado, bool editar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnimprimir.Enabled = imprimir;
            btnDeshabilitar.Enabled = estado;
            ultraGroupBox1.Enabled = guardar;
            btnEditar.Enabled = editar;
        }

        public void CargarPaciente()
        {
            PACIENTES paciente = new PACIENTES();
            DataTable jire = new DataTable();
            PACIENTES_DATOS_ADICIONALES adicionales = new PACIENTES_DATOS_ADICIONALES();

            PREADMISION admin = new PREADMISION();
            jire = NegPacientes.PacienteJire(txtIdentificacion.Text.Trim());

            paciente = NegPacientes.pacientePorIdentificacion(txtIdentificacion.Text.Trim());
            atencion = NegAtenciones.RecuperarUltimaAtencion(paciente.PAC_CODIGO);
            ATENCIONES_REINGRESO atRein = null;
            atRein = NegAtenciones.buscaReIngresoXate_codigo(atencion.ATE_CODIGO);
            admin = NegPreadmision.recuperaPreAdmisionAte(atencion.ATE_CODIGO);
            if (admin != null)
            {
                txtHC.Text = "";
                MessageBox.Show("Paciente ya cuenta con PREADMISION", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (jire.Rows.Count > 0)
            {
                txtNombre1.Text = jire.Rows[0][4].ToString();
                txtNombre2.Text = jire.Rows[0][5].ToString();
                txtApellido1.Text = jire.Rows[0][2].ToString();
                txtApellido2.Text = jire.Rows[0][3].ToString();
                txtDireccion.Text = jire.Rows[0][6].ToString();
                txtTelefono.Text = jire.Rows[0][8].ToString();
                txtEmail.Text = jire.Rows[0][9].ToString();
            }
            if (paciente != null)
            {
                adicionales = NegPacienteDatosAdicionales.RecuperarDatosPacientesID(paciente.PAC_CODIGO);
                HC_EMERGENCIA_FORM emer1 = new HC_EMERGENCIA_FORM();
                List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> emer2 = new List<HC_EMERGENCIA_FORM_DIAGNOSTICOS>();
                if (atRein != null)
                    emer1 = NegHcEmergencia.recuperaremergenciaFormPorAtencion(atRein.ATE_CODIGO_PRINCIPAL);
                else
                    emer1 = NegHcEmergencia.recuperaremergenciaFormPorAtencion(atencion.ATE_CODIGO);
                emer2 = NegHcEmergenciaFDetalle.recuperarDiagnosticosHcEmergencia(emer1.EMER_CODIGO, "A");
                foreach (var item in emer2)
                {
                    CIE10 cie = new CIE10();
                    cie = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);
                    this.dgvCie.Rows.Add(cie.CIE_CODIGO, cie.CIE_DESCRIPCION);
                }
                txtNombre1.Text = paciente.PAC_NOMBRE1;
                txtNombre2.Text = paciente.PAC_NOMBRE2;
                txtApellido1.Text = paciente.PAC_APELLIDO_PATERNO;
                txtApellido2.Text = paciente.PAC_APELLIDO_MATERNO;
                txtDireccion.Text = adicionales.DAP_DIRECCION_DOMICILIO.Trim();
                if (adicionales.DAP_DIRECCION_DOMICILIO2==null)
                {
                    txtCSecundaria.Text = "";
                }
                else
                {
                    txtCSecundaria.Text = adicionales.DAP_DIRECCION_DOMICILIO2.Trim();
                }

                if (adicionales.DAP_REFERENCIA == null)
                {
                    txtReferencia.Text = "";
                }
                else
                {
                    txtReferencia.Text = adicionales.DAP_REFERENCIA.Trim();
                }

                
                txtTelefono.Text = adicionales.DAP_TELEFONO1;
                txtCelular.Text = adicionales.DAP_TELEFONO2;
                txtEmail.Text = paciente.PAC_EMAIL;
                txtAteNumero.Text = atencion.ATE_CODIGO.ToString();
                if (paciente.PAC_TIPO_IDENTIFICACION == "C")
                    chkCedula.Checked = true;
                else
                    chkPasaporte.Checked = true;
            }
            else
                CargarPreAdmision();
        }
        private void btnF1_Click(object sender, EventArgs e)
        {
            string emergencia = "";
            frm_AyudaPacientes form = new frm_AyudaPacientes(emergencia);
            form.campoPadre = txtHC;
            form.campoId = txtIdentificacion;
            form.ShowDialog();
            form.Dispose();

            if (form.campoPadre.Text.Trim() != "")
            {
                //DataTable auxx = NegDietetica.getDataTable("EnHabitacion", form.campoPadre.Text.Trim());
                //if (auxx.Rows[0][0].ToString().Trim() != "0")
                //{
                //    MessageBox.Show("El paciente se encuentra hospitalizado actualmente, no es posible generar una nueva atención.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    HabilitarBotones(true, false, false, false);
                //    txtIdentificacion.Text = "";
                //    return;
                //}
            }


            if (form.campoId.Text != "")
            {
                txtHC.Text = form.campoPadre.Text.Trim();
                txtIdentificacion.Text = form.campoId.Text.Trim();
                CargarPaciente();
            }
            Errores.Clear();
            //if (NegValidaciones.esCedulaValida(form.campoId.Text.Trim()))
            //{
            //    PREADMISION existe = NegPreadmision.recuperarPreadmision(form.campoId.Text.Trim());
            //    if (existe != null)
            //    {
            //        preadmision = existe;
            //        CargarPreAdmision();
            //    }
            //    else
            //        CargarPaciente();
            //}

            //else
            //{
            //    txtIdentificacion.Focus();
            //    Errores.SetError(txtIdentificacion, "Cedula no valida");
            //}
            txtMedico.Text = "";
            cmbReferido.SelectedIndex = -1;
            cmbTrtamiento.SelectedIndex = -1;
        }
        public void CargarPreAdmision()
        {
            if (preadmision.PRE_CODIGO != 0)
            {
                detalle = NegPreadmision.recuperarPreAdmisionDetalle(preadmision.PRE_CODIGO);

                txtNombre1.Text = preadmision.PRE_NOMBRE1;
                txtNombre2.Text = preadmision.PRE_NOMBRE2;
                txtApellido1.Text = preadmision.PRE_APELLIDO1;
                txtApellido2.Text = preadmision.PRE_APELLIDO2;
                txtDireccion.Text = preadmision.PRE_DIRECCION;
                txtCSecundaria.Text = preadmision.PRE_DIRECCION2;
                txtReferencia.Text = preadmision.PRE_REFERENCIA;
                txtTelefono.Text = preadmision.PRE_TELEFONO;
                txtCelular.Text = preadmision.PRE_CELULAR;
                txtEmail.Text = preadmision.PRE_EMAIL;
                dtpFecha.Value = (DateTime)preadmision.PRE_FECHA;

                medico = NegMedicos.recuperarMedico((int)preadmision.MED_CODIGO);

                cmbReferido.Value = preadmision.TIR_CODIGO;
                cmbTrtamiento.Value = preadmision.TIA_CODIGO;

                txtMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;

                dgvCie.Rows.Clear();
                foreach (var item in detalle)
                {
                    CIE10 cie = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);
                    dgvCie.Rows.Add(cie.CIE_CODIGO, cie.CIE_DESCRIPCION);
                }
                HabilitarBotones(false, false, true, true, false);
            }
            else
                txtNombre1.Focus();
        }
        private void dgvCie_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvCie.CurrentCell.ColumnIndex == 0)
            {
                if (e.KeyCode == Keys.F1)
                {
                    frm_BusquedaCIE10 x = new frm_BusquedaCIE10();
                    x.ShowDialog();

                    if (x.codigo != string.Empty)
                    {
                        if (!BuscarItem(x.codigo, dgvCie))
                        {
                            this.dgvCie.Rows.Add(x.codigo, x.resultado);
                        }
                        else
                            MessageBox.Show("El item ya fue ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                if (dgvCie.Rows.Count > 1)
                {
                    for (int i = 0; i < dgvCie.Rows.Count - 1; i++)
                    {
                        if (dgvCie.Rows[i].Cells[0].Value == null && dgvCie.Rows[i].Cells[1].Value == null)
                            dgvCie.Rows.RemoveAt(dgvCie.Rows[i].Index);
                    }
                }
            }
        }
        private bool BuscarItem(string searchValue, DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        MessageBox.Show("El diagnostico ya ha sido ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }
            }
            return false;
        }
        public TextBox txtCodMedico;
        MEDICOS medico = new MEDICOS();
        private void btnMedicos_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
                CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));

            ayuda.Dispose();
        }
        private void CargarMedico(int codMedico)
        {
            medico = NegMedicos.MedicoID(codMedico);
            DataTable med = NegMedicos.MedicoIDValida(codMedico);
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodMedico.Text = "";
                txtMedico.Text = "";
                return;
            }
            if (medico != null)
                txtMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            else
                txtMedico.Text = string.Empty;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, false, false, false);
        }

        public bool guardar()
        {
            if (edicion)
            {
                ATENCIONES ate = new ATENCIONES();
                ate = NegAtenciones.RecuepraAtencionNumeroAtencion(Convert.ToInt64(txtAteNumero.Text));
                PREADMISION preadmisionEDIT = new PREADMISION();
                preadmisionEDIT = NegPreadmision.recuperaPreAdmisionAte(ate.ATE_CODIGO);
                NegPreadmision.cambioEstadoPreadmsion(preadmisionEDIT.PRE_CODIGO);
            }
            preadmision = new PREADMISION();
            preadmision.PRE_NOMBRE1 = txtNombre1.Text.Trim();
            preadmision.PRE_NOMBRE2 = txtNombre2.Text.Trim();
            preadmision.PRE_APELLIDO1 = txtApellido1.Text.Trim();
            preadmision.PRE_APELLIDO2 = txtApellido2.Text.Trim();
            preadmision.PRE_IDENTIFICACION = txtIdentificacion.Text.Trim();
            preadmision.PRE_DIRECCION = txtDireccion.Text.Trim();
            preadmision.PRE_DIRECCION2 = txtCSecundaria.Text.Trim();
            preadmision.PRE_REFERENCIA = txtReferencia.Text.Trim();
            preadmision.PRE_EMAIL = txtEmail.Text.Trim();
            preadmision.PRE_CODIGO = 0;
            preadmision.TIA_CODIGO = Convert.ToInt32(cmbTrtamiento.SelectedIndex);
            preadmision.TIR_CODIGO = Convert.ToInt32(cmbReferido.SelectedIndex);
            preadmision.PRE_CELULAR = txtCelular.Text.Trim();
            preadmision.PRE_TELEFONO = txtTelefono.Text;
            preadmision.PRE_ESTADO = true;
            preadmision.PRE_FECHA = DateTime.Now;
            preadmision.MED_CODIGO = medico.MED_CODIGO;
            preadmision.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
            preadmision.SEGURO_MEDICO = txtSeguro.Text;
            preadmision.PROCEDIMIENTO = txtProcedimiento.Text;
            preadmision.ATENCION = Convert.ToInt64(txtAteNumero.Text);

            if (chkAlta.Checked)
                preadmision.PRIORIDAD = 1;
            else if (chkMedia.Checked)
                preadmision.PRIORIDAD = 2;
            else
                preadmision.PRIORIDAD = 3;
            for (int i = 0; i < dgvCie.Rows.Count - 1; i++)
            {
                PREADMISION_DETALLE det = new PREADMISION_DETALLE();
                det.PRD_CODIGO = 0;
                det.PRE_CODIGO = 0;
                det.CIE_CODIGO = dgvCie.Rows[i].Cells[0].Value.ToString();

                detalle.Add(det);
            }
            if (NegPreadmision.crearPreadmision(preadmision, detalle))
            {
                MessageBox.Show("Datos guardados con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HabilitarBotones(false, false, true, true, false);
                return true;
            }
            else
            {
                MessageBox.Show("Algo ocurrio al guardar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                if (guardar())
                {
                    Imprimir();
                    HabilitarBotones(false, false, true, true, false);
                    this.Close();
                }
            }
        }

        public void Imprimir()
        {
            DsPreadmision preadmisionds = new DsPreadmision();
            DataRow dr;            
            USUARIOS user = NegUsuarios.RecuperarUsuarioID((Int16)His.Entidades.Clases.Sesion.codUsuario);
            ATENCIONES ate = new ATENCIONES();
            ate = NegAtenciones.RecuepraAtencionNumeroAtencion(Convert.ToInt64(txtAteNumero.Text));
            preadmision = new PREADMISION();
            preadmision = NegPreadmision.recuperaPreAdmisionAte(ate.ATE_CODIGO);
            for (int i = 0; i < dgvCie.Rows.Count - 1; i++)
            {
                dr = preadmisionds.Tables["PreAdmision"].NewRow();
                dr["Logo"] = NegUtilitarios.RutaLogo("General");
                dr["Empresa"] = His.Entidades.Clases.Sesion.nomEmpresa;
                dr["Paciente"] = preadmision.PRE_APELLIDO1 + " " + preadmision.PRE_APELLIDO2 + " " + preadmision.PRE_NOMBRE1 + " " + preadmision.PRE_NOMBRE2;
                dr["Identificacion"] = preadmision.PRE_IDENTIFICACION;
                dr["Medico"] = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                dr["Referido"] = cmbReferido.Text;
                dr["Tratamiento"] = cmbTrtamiento.Text;
                dr["Telefono"] = preadmision.PRE_TELEFONO;
                dr["Celular"] = preadmision.PRE_CELULAR;
                dr["Fecha"] = preadmision.PRE_FECHA;
                dr["Cie"] = dgvCie.Rows[i].Cells[0].Value.ToString();
                dr["Diagnostico"] = dgvCie.Rows[i].Cells[1].Value.ToString();
                dr["Codigo"] = preadmision.PRE_CODIGO;
                dr["Usuario"] = user.APELLIDOS + " " + user.NOMBRES;
                dr["Atencion"] = preadmision.ATENCION;
                dr["Seguro"] = preadmision.SEGURO_MEDICO;
                dr["fechaIngreso"] = ate.ATE_FECHA_INGRESO;
                dr["Procedimiento"] = preadmision.PROCEDIMIENTO;
                if (preadmision.PRIORIDAD == 1)
                    dr["Prioridad"] = "Alta";
                else if (preadmision.PRIORIDAD == 2)
                    dr["Prioridad"] = "Media";
                else
                    dr["Prioridad"] = "Baja";

                preadmisionds.Tables["PreAdmision"].Rows.Add(dr);
            }
            His.Formulario.frmReportes x = new Formulario.frmReportes(1, "PreAdmision", preadmisionds);
            x.Show();
        }

        public bool validarCampos()
        {
            bool valido = true;
            Errores.Clear();
            if (txtNombre1.Text.Trim() == "")
            {
                valido = false;
                Errores.SetError(txtNombre1, "Ingrese el nombre del paciente");
            }
            if (txtApellido1.Text.Trim() == "")
            {
                valido = false;
                Errores.SetError(txtApellido1, "Ingrese el apellido del paciente");
            }
            if (txtIdentificacion.Text.Trim() == "")
            {
                valido = false;
                Errores.SetError(txtIdentificacion, "Campo Obligatorio");
            }
            if (txtDireccion.Text.Trim() == "")
            {
                valido = false;
                Errores.SetError(txtDireccion, "Campo Obligatorio");
            }
            if (txtAteNumero.Text.Trim() == "")
            {
                valido = false;
                Errores.SetError(txtAteNumero, "Campo Obligatorio");
            }
            if (txtSeguro.Text.Trim() == "")
            {
                valido = false;
                Errores.SetError(txtSeguro, "Campo Obligatorio");
            }
            if (txtProcedimiento.Text.Trim() == "")
            {
                valido = false;
                Errores.SetError(txtProcedimiento, "Campo Obligatorio");
            }
            if (txtEmail.Text.Trim() != "")
            {
                if (!NegUtilitarios.validadorEmail(txtEmail.Text.Trim()))
                {
                    valido = false;
                    Errores.SetError(txtEmail, "Email no valido.");
                }
            }
            if (txtMedico.Text.Trim() == "")
            {
                valido = false;
                Errores.SetError(txtMedico, "Campo Obligatorio");
            }
            if (cmbReferido.SelectedItem == null)
            {
                valido = false;
                Errores.SetError(cmbReferido, "Campo Obligatorio");
            }
            if (cmbTrtamiento.SelectedItem == null)
            {
                valido = false;
                Errores.SetError(cmbTrtamiento, "Campo Obligatorio");
            }
            if (dgvCie.Rows.Count <= 1)
            {
                valido = false;
                Errores.SetError(dgvCie, "Debe agregar Cie");
            }
            if (!chkAlta.Checked && !chkMedia.Checked && !chkBaja.Checked)
            {
                valido = false;
                Errores.SetError(ultraGroupBox4, "Seleccione una prioridad");
            }
            return valido;
        }

        private void frmPreAdmision_Load(object sender, EventArgs e)
        {
            HabilitarBotones(true, false, false, false, false);
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            Imprimir();
        }

        private void txtIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                try
                {
                    PACIENTES paciente = NegPacientes.RecuperarPacienteCedula(txtIdentificacion.Text);
                    if (paciente != null)
                    {
                        //PACIENTES paciente = NegPacientes.RecuperarPacienteCedula(txtIdentificacion.Text);
                        DataTable auxx = NegDietetica.getDataTable("EnHabitacion", Convert.ToString(paciente.PAC_HISTORIA_CLINICA));

                        if (auxx.Rows[0][0].ToString().Trim() != "0")
                        {
                            MessageBox.Show("El paciente se encuentra hospitalizado actualmente, no es posible generar una nueva atención.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            HabilitarBotones(true, false, false, false, false);
                            txtIdentificacion.Text = "";
                            return;
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                Errores.Clear();
                if (chkCedula.Checked)
                {
                    if (NegValidaciones.esCedulaValida(txtIdentificacion.Text.Trim()))
                    {
                        PREADMISION existe = NegPreadmision.recuperarPreadmision(txtIdentificacion.Text.Trim());
                        if (existe != null)
                        {
                            preadmision = existe;
                            CargarPreAdmision();
                        }
                        else
                            CargarPaciente();
                    }

                    else
                    {
                        txtIdentificacion.Focus();
                        Errores.SetError(txtIdentificacion, "Cedula no valida");
                    }
                }
                else
                    txtNombre1.Focus();
            }
        }

        private void chkCedula_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCedula.Checked)
            {
                txtIdentificacion.MaxLength = 10;
                chkPasaporte.Checked = false;
            }
            else
            {
                txtIdentificacion.MaxLength = 30;
                chkPasaporte.Checked = true;
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

        private void txtIdentificacion_Leave(object sender, EventArgs e)
        {
            //if (chkCedula.Checked)
            //{
            //    if (NegValidaciones.esCedulaValida(txtIdentificacion.Text.Trim()))
            //    {
            //        PREADMISION existe = NegPreadmision.recuperarPreadmision(txtIdentificacion.Text.Trim());
            //        if (existe != null)
            //        {
            //            preadmision = existe;
            //            CargarPreAdmision();
            //        }
            //        else
            //            CargarPaciente();
            //    }

            //    else
            //    {
            //        txtIdentificacion.Focus();
            //        Errores.SetError(txtIdentificacion, "Cedula no valida");
            //    }
            //}
            //else
            //    txtNombre1.Focus();
        }
        private void txtNombre1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtNombre2.Focus();
            }
        }

        private void txtNombre2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtApellido1.Focus();
            }
        }

        private void txtApellido1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtApellido2.Focus();
            }
        }

        private void txtApellido2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtDireccion.Focus();
            }
        }

        private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCSecundaria.Focus();
            }
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCelular.Focus();
            }
        }

        private void txtCelular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnMedicos.Focus();
            }
        }

        private void chkPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPasaporte.Checked)
            {
                txtIdentificacion.MaxLength = 30;
                chkCedula.Checked = false;
            }
            else
            {
                txtIdentificacion.MaxLength = 10;
                chkCedula.Checked = true;
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de deshabilitar esta preadmision?", "HIS3000", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!NegPreadmision.cambioEstadoPreadmsion(preadmision.PRE_CODIGO))
                    MessageBox.Show("No se ha podido deshabilitar la preadmision", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show("Deshabilitado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void cmbTrtamiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbReferido_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            frmAyudaPreAdmision x = new frmAyudaPreAdmision();
            x.ShowDialog();
            if (x.iden != "")
            {

                cargarPacientePreAdmision(x.iden);

            }
        }
        public void cargarPacientePreAdmision(string preIngreso)
        {
            PREADMISION pre = new PREADMISION();
            List<PREADMISION_DETALLE> pde = new List<PREADMISION_DETALLE>();
            List<TIPO_REFERIDO> tipoReferido = new List<TIPO_REFERIDO>();
            tipoReferido = NegTipoReferido.listaTipoReferido();
            List<TIPO_TRATAMIENTO> tipoTratamiento = new List<TIPO_TRATAMIENTO>();
            tipoTratamiento = NegTipoTratamiento.RecuperaTipoTratamiento();
            pre = NegPreadmision.recuperarPreadmision(preIngreso);
            ATENCIONES ate = new ATENCIONES();
            if (pre != null)
            {
                ate = NegAtenciones.RecuepraAtencionNumeroAtencion(Convert.ToInt64(pre.ATENCION));
                pde = NegPreadmision.recuperarPreAdmisionDetalle(pre.PRE_CODIGO);
                medico = NegMedicos.MedicoID(Convert.ToInt32(pre.MED_CODIGO));
                PACIENTES pac = new PACIENTES();
                pac = NegPacientes.RecuperarPacienteCedula(pre.PRE_IDENTIFICACION);
                preadmision.PRE_CODIGO = pre.PRE_CODIGO;
                txtIdentificacion.Text = pre.PRE_IDENTIFICACION;
                txtApellido1.Text = pre.PRE_APELLIDO1;
                txtApellido2.Text = pre.PRE_APELLIDO2;
                txtNombre1.Text = pre.PRE_NOMBRE1;
                txtNombre2.Text = pre.PRE_NOMBRE2;
                txtHC.Text = pac.PAC_HISTORIA_CLINICA;
                txtAteNumero.Text = ate.ATE_CODIGO.ToString();
                dtpFecha.Value = pre.PRE_FECHA.Value;
                txtDireccion.Text = pre.PRE_DIRECCION;
                txtCSecundaria.Text = pre.PRE_DIRECCION2;
                txtReferencia.Text = pre.PRE_REFERENCIA;
                txtMedico.Text = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                txtTelefono.Text = pre.PRE_TELEFONO;
                txtCelular.Text = pre.PRE_CELULAR;
                txtEmail.Text = pre.PRE_EMAIL;
                txtSeguro.Text = pre.SEGURO_MEDICO;
                txtProcedimiento.Text = pre.PROCEDIMIENTO;
                if(pre.PRIORIDAD == 1)
                {
                    chkAlta.Checked = true;
                }else if(pre.PRIORIDAD == 2)
                {
                    chkMedia.Checked = true;
                }
                else
                {
                    chkBaja.Checked = true;
                }

                foreach (var item in pde)
                {
                    CIE10 cie = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);
                    dgvCie.Rows.Add(cie.CIE_CODIGO, cie.CIE_DESCRIPCION);
                }
                HabilitarBotones(false, false, true, true, true);
                cmbReferido.SelectedIndex = (Int16)pre.TIR_CODIGO;
                cmbTrtamiento.SelectedIndex = (Int16)pre.TIA_CODIGO;


                //DsPreadmision preadmisionds = new DsPreadmision();
                //DataRow dr;
                //USUARIOS user = NegUsuarios.RecuperarUsuarioID((Int16)pre.ID_USUARIO);
                //foreach (var item in pde)
                //{
                //    CIE10 c = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);
                //    dr = preadmisionds.Tables["PreAdmision"].NewRow();
                //    dr["Logo"] = NegUtilitarios.RutaLogo("General");
                //    dr["Empresa"] = His.Entidades.Clases.Sesion.nomEmpresa;
                //    dr["Paciente"] = pre.PRE_APELLIDO1 + " " + pre.PRE_APELLIDO2 + " " + pre.PRE_NOMBRE1 + " " + pre.PRE_NOMBRE2;
                //    dr["Identificacion"] = pre.PRE_IDENTIFICACION;
                //    dr["Medico"] = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                //    dr["Referido"] = tipoReferido.FirstOrDefault(t => t.TIR_CODIGO == pre.TIR_CODIGO).TIR_NOMBRE;
                //    dr["Tratamiento"] = tipoTratamiento.FirstOrDefault(t => t.TIA_CODIGO == pre.TIA_CODIGO).TIA_DESCRIPCION;
                //    dr["Telefono"] = pre.PRE_TELEFONO;
                //    dr["Celular"] = pre.PRE_CELULAR;
                //    dr["Fecha"] = pre.PRE_FECHA;
                //    dr["Cie"] = c.CIE_CODIGO;
                //    dr["Diagnostico"] = c.CIE_DESCRIPCION;
                //    dr["Codigo"] = pre.PRE_CODIGO;
                //    dr["Usuario"] = user.APELLIDOS + " " + user.NOMBRES;
                //    dr["Atencion"] = pre.ATENCION;
                //    dr["Seguro"] = pre.SEGURO_MEDICO;
                //    dr["fechaIngreso"] = ate.ATE_FECHA_INGRESO;
                //    dr["Procedimiento"] = pre.PROCEDIMIENTO;
                //    if (preadmision.PRIORIDAD == 1)
                //        dr["Prioridad"] = "Alta";
                //    else if (preadmision.PRIORIDAD == 2)
                //        dr["Prioridad"] = "Media";
                //    else
                //        dr["Prioridad"] = "Baja";


                //    preadmisionds.Tables["PreAdmision"].Rows.Add(dr);
                //}
                //His.Formulario.frmReportes x = new Formulario.frmReportes(1, "PreAdmision", preadmisionds);
                //x.Show();


            }
        }

        private void txtIdentificacion_Leave_1(object sender, EventArgs e)
        {
            try
            {
                PACIENTES paciente = NegPacientes.RecuperarPacienteCedula(txtIdentificacion.Text);
                if (paciente != null)
                {
                    //PACIENTES paciente = NegPacientes.RecuperarPacienteCedula(txtIdentificacion.Text);
                    DataTable auxx = NegDietetica.getDataTable("EnHabitacion", Convert.ToString(paciente.PAC_HISTORIA_CLINICA));

                    if (auxx.Rows[0][0].ToString().Trim() != "0")
                    {
                        MessageBox.Show("El paciente se encuentra hospitalizado actualmente, no es posible generar una nueva atención.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        HabilitarBotones(true, false, false, false, false);
                        txtIdentificacion.Text = "";
                        return;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            Errores.Clear();
            if (chkCedula.Checked)
            {
                if (NegValidaciones.esCedulaValida(txtIdentificacion.Text.Trim()))
                {
                    PREADMISION existe = NegPreadmision.recuperarPreadmision(txtIdentificacion.Text.Trim());
                    if (existe != null)
                    {
                        preadmision = existe;
                        CargarPreAdmision();
                    }
                    else
                        CargarPaciente();
                }

                else
                {
                    txtIdentificacion.Focus();
                    Errores.SetError(txtIdentificacion, "Cedula no valida");
                }
            }
            else
                txtNombre1.Focus();
        }

        private void chkAlta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAlta.Checked)
            {
                chkMedia.Checked = false;
                chkBaja.Checked = false;
            }
        }

        private void chkMedia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMedia.Checked)
            {
                chkAlta.Checked = false;
                chkBaja.Checked = false;
            }
        }

        private void chkBaja_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBaja.Checked)
            {
                chkAlta.Checked = false;
                chkMedia.Checked = false;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            ultraGroupBox1.Enabled = true;
            edicion = true;
        }
    }
}
