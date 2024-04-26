using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Formulario
{
    public partial class frmRecetaNew : Form
    {

        public bool sinAlta = false;
        ATENCIONES ultimaAtencion = new ATENCIONES();
        PACIENTES paciente = new PACIENTES();
        List<TIPO_INGRESO> tiposIngreso = new List<TIPO_INGRESO>();
        MEDICOS medico = null;
        TIPO_INGRESO ingreso = new TIPO_INGRESO();
        RECETA_MEDICA mReceta = new RECETA_MEDICA();

        public string Ate_Codigo = "";
        public bool noCargar = true; //esto valida para que no aparesca de nuevo en consulta externa
        bool consulta = false;
        public frmRecetaNew()
        {
            InitializeComponent();
            CargarIngresos();
            medico = NegMedicos.recuperarMedicoID_Usuario(Sesion.codUsuario);
            agregarMedico(medico);
        }
        public frmRecetaNew(string AteCodigo, bool _consulta = false)
        {
            InitializeComponent();
            consulta = _consulta;
            CargarIngresos();

            if(medico == null)
            {
                medico = NegMedicos.recuperarMedicoID_Usuario(Sesion.codUsuario);
            }
            agregarMedico(medico);
            
            CargarPaciente(AteCodigo);
            ayudaPacientes.Enabled = false;
            this.Ate_Codigo = AteCodigo;
            cargarInformacion();
            sinAlta = true;
            ayudaPacientes.Visible = false;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CargarDatos(Int64 ate_codigo)
        {
            mReceta = NegCertificadoMedico.RecuperaReceta(ate_codigo);
            List<RECETA_DIAGNOSTICO> diagnostico = NegCertificadoMedico.RecuperaDiagnostico(mReceta.RM_CODIGO);
            List<RECETA_MEDICAMENTOS> medica = NegCertificadoMedico.RecuperarMedicamentos(mReceta.RM_CODIGO);
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(ate_codigo);
            DataTable PacienteReferido = NegDetalleCuenta.ReferidoPaciente(ultimaAtencion.ATE_CODIGO);

            if (ultimaAtencion.ESC_CODIGO != 1)
            {
                MessageBox.Show("No se puede editar paciente dado de alta.\r\nComuniquese con Caja.",
                    "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
            medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(mReceta.MED_CODIGO));
            agregarMedico(medico);

            txt_apellido1.Text = paciente.PAC_APELLIDO_PATERNO;
            txt_apellido2.Text = paciente.PAC_APELLIDO_MATERNO;
            txt_nombre1.Text = paciente.PAC_NOMBRE1;
            txt_nombre2.Text = paciente.PAC_NOMBRE2;
            txt_historiaclinica.Text = paciente.PAC_HISTORIA_CLINICA.Trim();

            txtalergias.Text = mReceta.RM_ALERGIAS;
            txtsigno.Text = mReceta.RM_SIGNO;
            txtFarmacos.Text = mReceta.RM_FARMACOS;
            dtpCita.Value = (DateTime)mReceta.RM_CITA;
            ingreso = NegTipoIngreso.FiltrarPorId(Convert.ToInt16(mReceta.TIP_CODIGO));
            cmbServicio.Text = ingreso.TIP_DESCRIPCION;
            cmbConsulta.Value = mReceta.TC_CONSULTA;

            if (cmbConsulta.Text.Trim() == "0")
                chkCita.Checked = false;
            else
                chkCita.Checked = true;
            dgvDiagnostico.Rows.Clear();
            dgvMedicamentos.Rows.Clear();
            dgvIndicacion.Rows.Clear();
            foreach (var item in diagnostico)
            {
                CIE10 cie = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);
                dgvDiagnostico.Rows.Add(item.CIE_CODIGO, cie.CIE_DESCRIPCION);
            }
            foreach (var item in medica)
            {
                DataTable TablaP = NegProducto.RecuperarProductoSic(item.CODPRO);
                if (TablaP.Rows.Count > 0)
                {
                    dgvMedicamentos.Rows.Add(item.CODPRO, item.RMD_DESCRIPCION, item.RMD_COMERCIAL, item.RMD_ADMINISTRACION, item.RMD_PRESENTACION, item.RMD_CONCENTRACION, item.RMD_CANTIDAD);
                    dgvIndicacion.Rows.Add(item.RMD_DESCRIPCION, item.RMD_INDICACIONES, item.CODPRO);
                }
                else
                {
                    DataTable TablaPB = NegCertificadoMedico.RecuperarPBasico(item.CODPRO);
                    dgvMedicamentos.Rows.Add(item.CODPRO, item.RMD_DESCRIPCION, item.RMD_COMERCIAL, item.RMD_ADMINISTRACION, item.RMD_PRESENTACION, item.RMD_CONCENTRACION, item.RMD_CANTIDAD);
                    dgvIndicacion.Rows.Add(item.RMD_DESCRIPCION, item.RMD_INDICACIONES, item.CODPRO);
                }
            }
            if (PacienteReferido.Rows[0][0].ToString() == "PRIVADO")
                chkReferido.Checked = true;
            else
                chkReferido.Checked = false;
            chkReferido.Text = PacienteReferido.Rows[0][0].ToString();
        }
        public void Imprimir(Int64 ate_codgo, Int64 RM_CODIGO)
        {
            DSRecetaPasteur newreceta = new DSRecetaPasteur();

            DataRow row;
            EMPRESA emp = NegEmpresa.RecuperaEmpresa();

            RECETA_MEDICA receta = NegCertificadoMedico.RecuperaReceta(ate_codgo,RM_CODIGO);
            List<RECETA_DIAGNOSTICO> diagnostico = NegCertificadoMedico.RecuperaDiagnostico(receta.RM_CODIGO);
            List<RECETA_MEDICAMENTOS> medica = NegCertificadoMedico.RecuperarMedicamentos(receta.RM_CODIGO);
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(ate_codgo);
            paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codgo));
            //medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(receta.MED_CODIGO));
            //agregarMedico(medico);

            txt_apellido1.Text = paciente.PAC_APELLIDO_PATERNO;
            txt_apellido2.Text = paciente.PAC_APELLIDO_MATERNO;
            txt_nombre1.Text = paciente.PAC_NOMBRE1;
            txt_nombre2.Text = paciente.PAC_NOMBRE2;
            txt_historiaclinica.Text = paciente.PAC_HISTORIA_CLINICA.Trim();
            ingreso = NegTipoIngreso.FiltrarPorId(Convert.ToInt16(receta.TIP_CODIGO));
            int index = cmbServicio.FindString(ingreso.TIP_DESCRIPCION);
            cmbServicio.SelectedIndex = index;
            cmbConsulta.Value = receta.TC_CONSULTA;

            var now = DateTime.Now;
            var birthday = paciente.PAC_FECHA_NACIMIENTO.Value;
            var yearsOld = now - birthday;

            int years = (int)(yearsOld.TotalDays / 365.25);
            int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

            TimeSpan age = now - birthday;
            DateTime totalTime = new DateTime(age.Ticks);

            row = newreceta.Tables["RecetaPasteur"].NewRow();
            SUCURSALES sucursal = NegEmpresa.RecuperaEmpresaID(1);
            SUCURSALES sucursal2 = NegEmpresa.RecuperaEmpresaID(2);
            if (ingreso.TIP_CODIGO == 10)
            {
                row["Logo"] = NegUtilitarios.RutaLogo("Mushuñan");
                row["Empresa"] = sucursal.SUC_NOMBRE;
                row["Direccion"] = sucursal.SUC_DIRECCION;
                row["Telefonos"] = sucursal.SUC_TELEFONO;
                row["Fecha"] = "Sangolqui, " + Convert.ToDateTime(receta.RM_FECHA).ToString("dddd") + ", " + Convert.ToDateTime(receta.RM_FECHA).Day.ToString() + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("MMMM") + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("yyyy");
            }
            else if (ingreso.TIP_CODIGO == 12)
            {
                row["Logo"] = NegUtilitarios.RutaLogo("BrigadaMedica");
                row["Empresa"] = sucursal2.SUC_NOMBRE;
                row["Direccion"] = sucursal2.SUC_DIRECCION;
                row["Telefonos"] = sucursal2.SUC_TELEFONO;
                row["Fecha"] = "Quito, DM, " + Convert.ToDateTime(receta.RM_FECHA).ToString("dddd") + ", " + Convert.ToDateTime(receta.RM_FECHA).Day.ToString() + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("MMMM") + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("yyyy");
            }
            else
            {
                row["Logo"] = NegUtilitarios.RutaLogo("GENERAL");
                row["Empresa"] = His.Entidades.Clases.Sesion.nomEmpresa;
                row["Direccion"] = emp.EMP_DIRECCION;
                row["Telefonos"] = emp.EMP_TELEFONO;
                row["Fecha"] = "Quito, DM, " + Convert.ToDateTime(receta.RM_FECHA).ToString("dddd") + ", " + Convert.ToDateTime(receta.RM_FECHA).Day.ToString() + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("MMMM") + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("yyyy");
            }

            row["Paciente"] = txt_apellido1.Text + " " + txt_apellido2.Text + " " + txt_nombre1.Text + " " + txt_nombre2.Text;
            row["Identificacion"] = paciente.PAC_IDENTIFICACION;
            row["Edad"] = years + " AÑOS, " + months + " MESES, " + totalTime.Day + " DIAS";
            row["Procedencia"] = cmbServicio.GetItemText(cmbServicio.SelectedItem);
            row["Alergias"] = txtalergias.Text;
            row["Medico"] = txtmedico.Text;
            if (txtmedcedula.Text.Length <= 10)
                row["Medico_Ruc"] = txtmedcedula.Text;
            else
            {
                string te = txtmedcedula.Text;
                te = te.Substring(0, 10);
                row["Medico_Ruc"] = te;
            }
            row["Medico_Telefono"] = receta.MED_TELEFONO;
            row["Alarma"] = receta.RM_SIGNO;
            row["Cita"] = receta.RM_CITA;
            row["Farmacos"] = receta.RM_FARMACOS;
            row["Numero"] = receta.RM_CODIGO;
            newreceta.Tables["RecetaPasteur"].Rows.Add(row);

            foreach (var item in diagnostico)
            {
                CIE10 cie = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);
                row = newreceta.Tables["Diagnostico"].NewRow();
                row["Diagnosticos"] = cie.CIE_DESCRIPCION;
                row["Cie10"] = item.CIE_CODIGO;
                row["Alergias"] = receta.RM_ALERGIAS;
                newreceta.Tables["Diagnostico"].Rows.Add(row);
            }

            foreach (var item in medica)
            {
                DataTable TablaP = NegProducto.RecuperarProductoSic(item.CODPRO);
                row = newreceta.Tables["Medicamentos"].NewRow();
                if (TablaP.Rows.Count > 0)
                    row["Medicamento"] = TablaP.Rows[0]["despro"].ToString();
                else
                {
                    row["Medicamento"] = item.RMD_DESCRIPCION;
                }
                row["Via"] = item.RMD_ADMINISTRACION;
                row["Presentacion"] = item.RMD_PRESENTACION;
                row["Concentracion"] = item.RMD_CONCENTRACION;
                row["Cantidad"] = item.RMD_CANTIDAD + " (" + Dia_En_Palabras(item.RMD_CANTIDAD.ToString()) + ")";
                row["Indicacion"] = item.RMD_INDICACIONES;
                if (chkCita.Checked)
                {
                    if (receta.TC_CONSULTA == 0)
                    {
                        row["Consulta"] = "";
                        row["Cita"] = "";
                    }
                    else
                    {
                        row["Consulta"] = cmbConsulta.Text;
                        row["Cita"] = Convert.ToDateTime(receta.RM_CITA).ToString("dd/MM/yyyy HH:mm");
                    }

                }
                else
                {
                    if (receta.TC_CONSULTA == 0)
                    {
                        row["Consulta"] = "";
                        row["Cita"] = "";
                    }
                }
                row["Alarma"] = receta.RM_SIGNO;
                row["Numero"] = receta.RM_CODIGO;
                row["Farmacos"] = receta.RM_FARMACOS;
                row["Paciente"] = txt_apellido1.Text + " " + txt_apellido2.Text + " " + txt_nombre1.Text + " " + txt_nombre2.Text;
                row["Identificacion"] = paciente.PAC_IDENTIFICACION;
                row["Edad"] = years + " AÑOS, " + months + " MESES, " + totalTime.Day + " DIAS";
                if (ingreso.TIP_CODIGO == 10)
                    row["Logo"] = NegUtilitarios.RutaLogo("Mushuñan");
                else
                    row["Logo"] = NegUtilitarios.RutaLogo("GENERAL");
                row["Comercial"] = item.RMD_COMERCIAL;

                newreceta.Tables["Medicamentos"].Rows.Add(row);
            }
            frm_Advertencia xy = new frm_Advertencia();
            xy.ShowDialog();
            His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "RecetaPasteur", newreceta);
            reporte.Show();
        }
        public bool Editar = false;


        private bool BuscarItem(string searchValue, DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        MessageBox.Show("El medicamento ya fue agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }
            }
            return false;
        }


        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null) && Convert.ToInt16(medicoTratante.USUARIOSReference.EntityKey.EntityKeyValues[0].Value) != 1)
            {
                txtmedico.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                txtmedcedula.Text = medicoTratante.MED_RUC;
                txtemail.Text = medicoTratante.MED_EMAIL;
                telfmedico.Text = medicoTratante.MED_TELEFONO_CONSULTORIO;
            }
            else if (Sesion.codUsuario == 1)
            {
                txtmedico.Text = "ADMINISTRADOR";
                txtmedcedula.Text = "9999999999999";
                txtemail.Text = "administrador@gapsystem.net";
                telfmedico.Text = "022222222";
            }
            else
            {
                MessageBox.Show("El usuario logeado no es un medico, por lo que no podra generar la receta medica", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            LimpiarMedicamentos();
        }
        public void limpiar()
        {
            txt_historiaclinica.Text = "";
            txt_apellido1.Text = "";
            txt_apellido2.Text = "";
            txt_nombre1.Text = "";
            txt_nombre2.Text = "";
            txtmedico.Text = "";
            dgvDiagnostico.Rows.Clear();
            dgvIndicacion.Rows.Clear();
            dgvMedicamentos.Rows.Clear();
            txtalergias.Text = "";
            txtsigno.Text = "";
            txtFarmacos.Text = "";
            cmbServicio.SelectedIndex = -1;
            cmbConsulta.SelectedIndex = -1;
            mReceta = new RECETA_MEDICA();
            chkReferido.Text = "";
            chkReferido.Checked = false;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(ultimaAtencion.ATE_CODIGO);

            //if (ultimaAtencion.ESC_CODIGO != 1)
            //{
            //    MessageBox.Show("Paciente ha sido dado de alta.\r\nComiquese con caja.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            List<RECETA_DIAGNOSTICO> diagnostico = new List<RECETA_DIAGNOSTICO>();
            List<RECETA_MEDICAMENTOS> rmedicamentos = new List<RECETA_MEDICAMENTOS>();
            RECETA_MEDICA receta = new RECETA_MEDICA();
            if (Sesion.codUsuario != 1)
            {
                if (txt_historiaclinica.Text.Trim() != "")
                {
                    if (dgvDiagnostico.Rows.Count > 1)
                    {
                        if (dgvMedicamentos.Rows.Count > 0)
                        {
                            if (txtmedico.Text.Trim() != "")
                            {
                                Int64 idReceta = NegCertificadoMedico.IdReceta();
                                Int64 idDiagnostico = NegCertificadoMedico.IdRecetaDiagnostico();
                                Int64 idMedicamento = NegCertificadoMedico.IdRecetaMedicamento();

                                receta.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                                receta.MED_CODIGO = medico.MED_CODIGO;
                                receta.RM_ALERGIAS = txtalergias.Text.Trim();
                                receta.RM_CITA = dtpCita.Value;
                                receta.RM_FECHA = DateTime.Now;
                                if (mReceta.RM_CODIGO == 0)
                                    receta.RM_CODIGO = idReceta + 1;
                                else
                                    receta.RM_CODIGO = mReceta.RM_CODIGO;
                                receta.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                                receta.RM_ESTADO = true;
                                receta.RM_SIGNO = txtsigno.Text.Trim();
                                receta.RM_FARMACOS = txtFarmacos.Text.Trim();
                                receta.TIP_CODIGO = Convert.ToInt32(cmbServicio.SelectedValue);
                                receta.TC_CONSULTA = Convert.ToInt32(cmbConsulta.Value);
                                receta.MED_TELEFONO = telfmedico.Text.Trim();
                                for (int i = 0; i < dgvDiagnostico.Rows.Count - 1; i++)
                                {
                                    idDiagnostico++;
                                    RECETA_DIAGNOSTICO diag = new RECETA_DIAGNOSTICO();
                                    diag.CIE_CODIGO = dgvDiagnostico.Rows[i].Cells[0].Value.ToString();
                                    if (mReceta.RM_CODIGO == 0)
                                        diag.RM_CODIGO = idReceta + 1;
                                    else
                                        diag.RM_CODIGO = mReceta.RM_CODIGO;
                                    diag.RD_CODIGO = idDiagnostico;
                                    diagnostico.Add(diag);
                                }

                                for (int i = 0; i < dgvMedicamentos.Rows.Count; i++)
                                {
                                    idMedicamento++;
                                    RECETA_MEDICAMENTOS med = new RECETA_MEDICAMENTOS();
                                    med.CODPRO = dgvMedicamentos.Rows[i].Cells[0].Value.ToString();
                                    if (mReceta.RM_CODIGO == 0)
                                        med.RM_CODIGO = idReceta + 1;
                                    else
                                        med.RM_CODIGO = mReceta.RM_CODIGO;
                                    med.RMD_INDICACIONES = dgvIndicacion.Rows[i].Cells[1].Value.ToString();
                                    med.RMD_ADMINISTRACION = dgvMedicamentos.Rows[i].Cells[3].Value.ToString();
                                    med.RMD_PRESENTACION = dgvMedicamentos.Rows[i].Cells[4].Value.ToString();
                                    med.RMD_CONCENTRACION = dgvMedicamentos.Rows[i].Cells[5].Value.ToString();
                                    med.RMD_COMERCIAL = dgvMedicamentos.Rows[i].Cells[2].Value.ToString();
                                    med.RMD_DESCRIPCION = dgvMedicamentos.Rows[i].Cells[1].Value.ToString();
                                    if (dgvMedicamentos.Rows[i].Cells[6].Value != null)
                                        med.RMD_CANTIDAD = Convert.ToInt32(dgvMedicamentos.Rows[i].Cells[6].Value.ToString());
                                    else
                                        med.RMD_CANTIDAD = 1;
                                    med.RMD_CODIGO = idMedicamento;
                                    rmedicamentos.Add(med);
                                }
                                for (int i = 0; i < dgvIndicacion.Rows.Count; i++)
                                {
                                    if (dgvIndicacion.Rows[i].Cells[1].Value.ToString() == "")
                                    {
                                        MessageBox.Show("Debe agregar las indicaciones a los medicamentos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                                if (!Editar)
                                {
                                    if (NegCertificadoMedico.InsertReceta(receta, diagnostico, rmedicamentos))
                                    {
                                        Imprimir(Convert.ToInt64(receta.ATE_CODIGO),receta.RM_CODIGO);
                                        this.Close();
                                    }
                                    else
                                        MessageBox.Show("Algo ocurrio con receta medica.\r\nIntente nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    if (NegCertificadoMedico.UpdateReceta(receta, diagnostico, rmedicamentos))
                                    {
                                        Imprimir(Convert.ToInt64(mReceta.ATE_CODIGO),receta.RM_CODIGO);
                                        this.Close();
                                    }
                                    else
                                        MessageBox.Show("Algo ocurrio con receta medica.\r\nIntente nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                                errorProvider1.SetError(txtmedico, "Debe eligir un medico.");
                        }
                        else
                            MessageBox.Show("Debe agregar medicamentos a la receta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Debe agregar minimo un diagnostico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    errorProvider1.SetError(txt_historiaclinica, "Debe elegir un paciente");
            }
            else
            {
                MessageBox.Show("Recetana no puede generar un usuario administrador ni un usuario sin vincular con el medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }
        private void frmRecetaNew_Load(object sender, EventArgs e)
        {
            CuadroBasico();
            //CargarIngresos();
            CargarConsulta();
            CargarVia();
            CargarPresentacion();
        }
        public void CuadroBasico()
        {
            cmbMedicamento.DataSource = NegCertificadoMedico.CuadroBasico();
            cmbMedicamento.DisplayMember = "PRODUCTO";
            cmbMedicamento.ValueMember = "CODIGO";
        }
        public void CargarIngresos()
        {
            tiposIngreso = NegTipoIngreso.ListaTipoIngreso();
            cmbServicio.DataSource = tiposIngreso;
            cmbServicio.ValueMember = "TIP_CODIGO";
            cmbServicio.DisplayMember = "TIP_DESCRIPCION";
        }
        public void CargarConsulta()
        {
            cmbConsulta.DataSource = NegCertificadoMedico.Consulta();
            cmbConsulta.ValueMember = "TC_CODIGO";
            cmbConsulta.DisplayMember = "TC_DESCRIPCION";
        }
        public void CargarConsultaHospitalaria()
        {
            cmbConsulta.DataSource = NegCertificadoMedico.ConsultaHospitalaria();
            cmbConsulta.ValueMember = "TC_CODIGO";
            cmbConsulta.DisplayMember = "TC_DESCRIPCION";
        }
        public double maxPT = 0;

        private void CargarVia()
        {
            List<VIA_ADMINISTRACION_MEDICAMENTO> vias = new List<VIA_ADMINISTRACION_MEDICAMENTO>();
            vias = NegCertificadoMedico.ListarViaAdministrativa();
            cmbAdministracion.DataSource = vias;
            cmbAdministracion.DisplayMember = "Detalle";
        }
        private void CargarPresentacion()
        {
            List<TIPO_PRESENTACION> LPresentacion = new List<TIPO_PRESENTACION>();
            LPresentacion = NegCertificadoMedico.Presentacion();
            cmbPresentacion.DataSource = LPresentacion;
            cmbPresentacion.DisplayMember = "TP_DESCRIPCION";
        }

        private void dgvMedicamentos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvMedicamentos.CurrentCell.ColumnIndex == 6)
                NegUtilitarios.OnlyNumber(e, false);
        }
        public string Dia_En_Palabras(string dia)
        {
            string num2Text = ""; int value = Convert.ToInt32(dia);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UN";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + Dia_En_Palabras(Convert.ToString(value - 10));
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + Dia_En_Palabras(Convert.ToString(value - 20));
            else if (value == 30) num2Text = "TREINTA";
            else if (value < 40) num2Text = "TREINTA Y " + Dia_En_Palabras(Convert.ToString(value - 30));
            else if (value == 40) num2Text = "CUARENTA";
            else if (value < 50) num2Text = "CUARENTA Y " + Dia_En_Palabras(Convert.ToString(value - 40));
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            return num2Text;

        }
        int validaTiempo = 1;
        public void cargarInformacion()
        {
            //if (NegCertificadoMedico.ExisteReceta(Convert.ToInt64(Ate_Codigo)))
            //{
            //    if (MessageBox.Show("Paciente ya tiene receta medica.\r\n¿Desea Reimprimir?", "HIS3000",
            //        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        Imprimir(Convert.ToInt64(Ate_Codigo));
            //        Editar = false;
            //        limpiar();
            //        noCargar = false;
            //        return;
            //    }
            //    else
            //    {
            //        CargarPaciente(Ate_Codigo);
            //        ////Aqui se habilita la modificacion
            //        //CargarDatos(Convert.ToInt64(Ate_Codigo));
            //        //Editar = true;
            //    }
            //}
            //else
            //{
            //    CargarPaciente(Ate_Codigo);
            //}
            CargarPaciente(Ate_Codigo);

            ultraGroupBox1.Enabled = true;
            ultraGroupBox2.Enabled = true;
            ultraGroupBox3.Enabled = true;
            ultraGroupBox4.Enabled = true;
            ultraGroupBox5.Enabled = true;
        }
        private void ayudaPacientes_Click_1(object sender, EventArgs e)
        {
            frm_Ayuda_Certificado.id_usuario = Convert.ToString(Sesion.codUsuario);
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            x.mushugñan = true;

            x.ShowDialog();


            //frm_AyudaPacientes2 x = new frm_AyudaPacientes2();
            //x.ShowDialog();

            if (x.ate_codigo != "")
            {
                Ate_Codigo = x.ate_codigo;
                var recuperada = NegAtenciones.RecuepraAtencionNumeroAtencion2(Ate_Codigo);
                if (NegCertificadoMedico.ExisteRecetaMedico(recuperada.ATE_CODIGO, medico.MED_CODIGO))
                {
                    MessageBox.Show("Este medico genero una receta medica para este paciente. SI DESEA MODIFICAR LA RECETA, PRIMERO DEBE ANULARLA EN EL EXPLORADOR DE RECETAS MEDICAS", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                cargarInformacion();
            }
        }
        public void CargarPaciente(string ateCodigo)
        {
            if (!consulta)
            {
                var recuperada = NegAtenciones.RecuepraAtencionNumeroAtencion2(ateCodigo);
                ultimaAtencion = NegAtenciones.RecuperarAtencionID(recuperada.ATE_CODIGO);
                paciente = NegPacientes.recuperarPacientePorAtencion(recuperada.ATE_CODIGO);
            }
            else
            {
                ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ateCodigo));
                paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ateCodigo));
            }
            int tip_codigo = NegTipoIngreso.RecuperarporAtencion(ultimaAtencion.ATE_CODIGO);
            ingreso = NegTipoIngreso.FiltrarPorId(Convert.ToInt16(tip_codigo));
            DataTable PacienteReferido = NegDetalleCuenta.ReferidoPaciente(ultimaAtencion.ATE_CODIGO);

            txt_apellido1.Text = paciente.PAC_APELLIDO_PATERNO;
            txt_apellido2.Text = paciente.PAC_APELLIDO_MATERNO;
            txt_nombre1.Text = paciente.PAC_NOMBRE1;
            txt_nombre2.Text = paciente.PAC_NOMBRE2;
            txt_historiaclinica.Text = paciente.PAC_HISTORIA_CLINICA.Trim();
            int index = cmbServicio.FindString(ingreso.TIP_DESCRIPCION);
            cmbServicio.SelectedIndex = index;
            //cmbServicio.Text = ingreso.TIP_DESCRIPCION.ToString();

            if (PacienteReferido.Rows[0][0].ToString() == "PRIVADO")
                chkReferido.Checked = true;
            else
                chkReferido.Checked = false;
            chkReferido.Text = PacienteReferido.Rows[0][0].ToString();
            List<CIE10> Cies = NegCIE10.RecuperarCieFormulario(ultimaAtencion.ATE_CODIGO);
            dgvDiagnostico.Rows.Clear();
            foreach (var item in Cies)
            {
                dgvDiagnostico.Rows.Add(item.CIE_CODIGO, item.CIE_DESCRIPCION);
            }
        }
        private void dgvDiagnostico_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (dgvDiagnostico.CurrentCell.ColumnIndex == 0)
            {
                if (e.KeyCode == Keys.F1)
                {
                    frm_BusquedaCIE10 x = new frm_BusquedaCIE10();
                    x.ShowDialog();

                    if (x.codigo != string.Empty)
                    {
                        if (!BuscarItem(x.codigo, dgvDiagnostico))
                        {
                            this.dgvDiagnostico.Rows.Add(x.codigo, x.resultado);
                        }
                        else
                            MessageBox.Show("El item ya fue ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                if (dgvDiagnostico.Rows.Count > 1)
                {
                    for (int i = 0; i < dgvDiagnostico.Rows.Count - 1; i++)
                    {
                        if (dgvDiagnostico.Rows[i].Cells[0].Value == null && dgvDiagnostico.Rows[i].Cells[1].Value == null)
                            dgvDiagnostico.Rows.RemoveAt(dgvDiagnostico.Rows[i].Index);
                    }
                }
            }
        }

        private void txtmedico_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                medicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(medicos, "MEDICOS", "CODIGO");
                //ayuda.campoPadre = txt;
                ayuda.ShowDialog();

                if (ayuda.campoPadre.Text != string.Empty)
                {
                    medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarMedico(medico);
                }
                //List<MEDICOS> medicos = NegMedicos.listaMedicos();
                //frm_Ayudas x = new frm_Ayudas(medicos);
                //x.ShowDialog();
                //if (x.campoPadre.Text != string.Empty)
                //{
                //    codMedico = (x.campoPadre2);
                //    string cod = x.campoPadre.Text;
                //    medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(cod));
                //    agregarMedico(medico);
                //}
            }
        }

        private void dgvMedicamentos_CellValueChanged_1(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (dgvMedicamentos.Rows.Count > 1)
            {
                if (dgvMedicamentos.CurrentCell.ColumnIndex == 1)
                {
                    if (dgvMedicamentos.CurrentCell.Value != null)
                    {
                        int row = dgvMedicamentos.CurrentRow.Index;
                        frmAyudaReceta x = new frmAyudaReceta();
                        x.producto = dgvMedicamentos.CurrentCell.Value.ToString();
                        x.codigoArea = 1;
                        x._Rubro = 1;
                        x._CodigoConvenio = 145;
                        x._CodigoEmpresa = 146;
                        x.ShowDialog();
                        string nombre = dgvMedicamentos.CurrentRow.Cells[1].Value.ToString();
                        if (x.medicamento != "")
                        {
                            for (int i = 0; i < dgvMedicamentos.Rows.Count - 1; i++)
                            {
                                if (dgvMedicamentos.Rows[i].Cells[0].Value == null && dgvMedicamentos.Rows[i].Cells[1].Value != null)
                                {
                                    dgvMedicamentos.Rows.RemoveAt(dgvMedicamentos.Rows[i].Index);
                                }
                            }
                            int index = 0;
                            if (!BuscarItem(x.codpro, dgvMedicamentos))
                            {
                                if (x.presentacion == "")
                                    dgvMedicamentos.Rows.Add(x.codpro, x.medicamento, "", "", "", "", "");
                                else
                                    dgvMedicamentos.Rows.Add(x.codpro, x.medicamento.ToUpper(), "", "", "", x.presentacion, "");
                                index = dgvMedicamentos.CurrentRow.Index;
                                dgvIndicacion.Rows.Add(x.medicamento.ToUpper(), "", x.codpro);
                            }
                            //dgvMedicamentos.CurrentCell = dgvMedicamentos.Rows[index].Cells[2];
                        }
                        else
                        {
                            if (MessageBox.Show("¿Desea agregar este producto manual?", "HIS3000",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                for (int i = 0; i < dgvMedicamentos.Rows.Count - 1; i++)
                                {
                                    if (dgvMedicamentos.Rows[i].Cells[0].Value == null && dgvMedicamentos.Rows[i].Cells[1].Value != null)
                                    {
                                        dgvMedicamentos.Rows.RemoveAt(dgvMedicamentos.Rows[i].Index);
                                    }
                                }

                                if (maxPT == 0)
                                    maxPT = NegCertificadoMedico.UltimoPT() + 1;
                                else
                                    maxPT++;
                                dgvMedicamentos.Rows.Add(maxPT, nombre, "", "", "", "", "");
                                dgvIndicacion.Rows.Add(nombre, "", maxPT);
                            }
                        }
                    }
                }
            }
        }

        private void dgvMedicamentos_UserDeletingRow_1(object sender, DataGridViewRowCancelEventArgs e)
        {
            string codpro = dgvMedicamentos.CurrentRow.Cells[0].Value.ToString();
            DialogResult usersChoice = MessageBox.Show("¿Está seguro de eliminar el medicamento?", "HIS3000", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            // Cancela la eliminacion
            if (usersChoice == DialogResult.Cancel)
                e.Cancel = true;
            else
            {
                for (int i = 0; i < dgvIndicacion.Rows.Count; i++)
                {
                    if (dgvIndicacion.Rows[i].Cells[2].Value.ToString() == codpro)
                        dgvIndicacion.Rows.RemoveAt(dgvIndicacion.Rows[i].Index);
                }
            }
        }

        private void dgvMedicamentos_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvMedicamentos.CurrentCell.ColumnIndex == 6)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dgvMedicamentos_KeyPress);
                }
            }
        }
        private void dtpCita_ValueChanged_1(object sender, EventArgs e)
        {
            if (dtpCita.Value < DateTime.Now)
            {
                if (validaTiempo == 1)
                {
                    validaTiempo++;
                    dtpCita.Value = DateTime.Now.AddMinutes(5);
                    MessageBox.Show("La fecha de la cita no puede ser menor a la fecha actual.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    dtpCita.Value = DateTime.Now.AddMinutes(5);
                    validaTiempo = 1;
                }
            }
        }

        private void chkCita_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCita.Checked)
            {
                if (chkReferido.Checked)
                {
                    CargarConsulta();
                    cmbConsulta.SelectedIndex = 2;
                }
                else
                {
                    CargarConsultaHospitalaria();
                }
                cmbConsulta.Enabled = true;
                dtpCita.Enabled = true;
            }
            else
            {
                cmbConsulta.SelectedIndex = -1;
                cmbConsulta.Enabled = false;
                dtpCita.Enabled = false;
            }
        }

        private void cmbMedicamento_SelectionChanged(object sender, EventArgs e)
        {
            if (cmbMedicamento.Text != "")
            {
                string value = "";
                string nombre = "";
                value = cmbMedicamento.Text;
                int index = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    if (Char.IsDigit(value[i]))
                    {
                        index = i;
                        break;
                    }
                    else
                    {
                        if (nombre == "")
                            nombre = value[i].ToString();
                        else
                            nombre = nombre + value[i].ToString();
                    }
                }
                txtmedicamento.Text = nombre;
                txtConcentracion.Text = value.Substring(index, value.Length - index);
            }
        }

        private void chkVacumed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVacumed.Checked)
            {
                MessageBox.Show("Información aun no disponible.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkBasico.Checked = true;
                chkOtros.Checked = false;
                chkVacumed.Checked = false;
            }
        }

        private void chkBasico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBasico.Checked)
            {
                CuadroBasico();
                chkVacumed.Checked = false;
                chkOtros.Checked = false;
                txtmedicamento.Visible = false;
                cmbMedicamento.Visible = true;
            }
        }

        private void chkOtros_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtros.Checked)
            {
                cmbMedicamento.Visible = false;
                txtmedicamento.Visible = true;
                chkVacumed.Checked = false;
                chkBasico.Checked = false;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }
        public bool Validacion()
        {
            bool valida = true;
            if (cmbAdministracion.Text.Trim() == "")
            {
                errorProvider1.SetError(cmbAdministracion, "Debe elegir la via de administracion");
                valida = false;

            }
            if (cmbPresentacion.Text.Trim() == "")
            {
                errorProvider1.SetError(cmbPresentacion, "Debe elegir la presentacion del medicamento.");
                valida = false;
            }
            if (txtConcentracion.Text.Trim() == "")
            {
                errorProvider1.SetError(txtConcentracion, "Debe ingresar la concentracion del medicamento.");
                valida = false;
            }
            if (txtCantidad.Text.Trim() == "")
            {
                errorProvider1.SetError(txtCantidad, "Debe ingresar la cantidad ó no puede ser cero.");
                valida = false;
            }
            if (txtCantidad.Text.Trim() != "")
            {
                if (Convert.ToDouble(txtCantidad.Text) <= 0)
                {
                    errorProvider1.SetError(txtCantidad, "Cantidad no puede ser cero o menor a cero.");
                    valida = false;
                }
            }
            if (chkBasico.Checked)
            {
                if (cmbMedicamento.SelectedItem == null)
                {
                    errorProvider1.SetError(cmbMedicamento, "No puede agregar medicamento que no existe en el cuadro básico.");
                    valida = false;
                }
            }
            if (chkOtros.Checked)
            {
                if (txtmedicamento.Text.Trim() == "")
                {
                    errorProvider1.SetError(txtmedicamento, "Debe agregar el nombre del medicamento que va a añadir");
                    valida = false;
                }
            }
            return valida;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (Validacion())
            {
                if (chkOtros.Checked)
                {
                    //aqui guarda cuando el producto no existe
                    if (maxPT == 0)
                        maxPT = NegCertificadoMedico.UltimoPT() + 1;
                    else
                        maxPT++;
                    if (!BuscarItem(maxPT.ToString(), dgvMedicamentos))
                    {
                        dgvMedicamentos.Rows.Add(maxPT, txtmedicamento.Text, txtComercial.Text, cmbAdministracion.Text, cmbPresentacion.Text, txtConcentracion.Text, txtCantidad.Text);
                        dgvIndicacion.Rows.Add(txtmedicamento.Text, "", maxPT);
                    }

                }
                else
                {
                    if (!BuscarItem(cmbMedicamento.SelectedItem.DataValue.ToString(), dgvMedicamentos))
                    {
                        dgvMedicamentos.Rows.Add(cmbMedicamento.SelectedItem.DataValue.ToString(), txtmedicamento.Text, txtComercial.Text, cmbAdministracion.Text, cmbPresentacion.Text, txtConcentracion.Text, txtCantidad.Text);
                        dgvIndicacion.Rows.Add(txtmedicamento.Text, "", cmbMedicamento.SelectedItem.DataValue.ToString());
                    }
                }
                LimpiarMedicamentos();
                dgvIndicacion.Columns[0].ReadOnly = true;
            }
        }
        public void LimpiarMedicamentos()
        {
            cmbMedicamento.SelectedIndex = -1;
            txtComercial.Text = "";
            cmbAdministracion.SelectedIndex = -1;
            cmbPresentacion.SelectedIndex = -1;
            txtConcentracion.Text = "";
            txtCantidad.Text = "";
            txtCodigo.Text = "";
            txtmedicamento.Text = "";
        }

        private void ultraGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtComercial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cmbAdministracion.Focus();
            }
        }

        private void cmbAdministracion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cmbPresentacion.Focus();
            }
        }

        private void cmbPresentacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtConcentracion.Focus();
            }
        }

        private void txtConcentracion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                txtCantidad.Focus();
        }

        private void txtmedicamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                txtComercial.Focus();
        }

        private void cmbMedicamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                txtComercial.Focus();
        }

        private void dgvMedicamentos_DoubleClick(object sender, EventArgs e)
        {
            if (dgvMedicamentos.SelectedRows.Count == 1)
            {
                //Recupero los datos para que pueda modificar si se equivocan
                double index = NegCertificadoMedico.UltimoPT();

                string codigo = dgvMedicamentos.CurrentRow.Cells[0].Value.ToString();
                txtCodigo.Text = dgvMedicamentos.CurrentRow.Cells[0].Value.ToString();
                cmbMedicamento.Value = txtCodigo.Text;
                txtComercial.Text = dgvMedicamentos.CurrentRow.Cells[2].Value.ToString();
                cmbAdministracion.Text = dgvMedicamentos.CurrentRow.Cells[3].Value.ToString();
                cmbPresentacion.Text = dgvMedicamentos.CurrentRow.Cells[4].Value.ToString();
                txtConcentracion.Text = dgvMedicamentos.CurrentRow.Cells[5].Value.ToString();
                txtCantidad.Text = dgvMedicamentos.CurrentRow.Cells[6].Value.ToString();
                if (index <= Convert.ToInt32(codigo))
                {
                    if (cmbMedicamento.SelectedItem == null)
                    {
                        chkOtros.Checked = true;
                        txtmedicamento.Text = dgvMedicamentos.CurrentRow.Cells[1].Value.ToString();
                        cmbMedicamento.Text = "";
                    }
                }
                //elimino para que puedan ingresar de nuevo
                for (int i = 0; i < dgvMedicamentos.Rows.Count; i++)
                {
                    if (dgvMedicamentos.Rows[i].Cells[0].Value.ToString() == codigo)
                    {
                        dgvMedicamentos.Rows.RemoveAt(dgvMedicamentos.Rows[i].Index);
                    }
                    if (dgvIndicacion.Rows[i].Cells[2].Value.ToString() == codigo)
                    {
                        dgvIndicacion.Rows.RemoveAt(dgvIndicacion.Rows[i].Index);
                    }
                }
            }
        }

        private void btnMedicos_Click(object sender, EventArgs e)
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            medicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(medicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
            {
                medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                if (NegCertificadoMedico.ExisteRecetaMedico(Convert.ToInt64(Ate_Codigo), medico.MED_CODIGO))
                {
                    MessageBox.Show("Este medico genero una receta medica para este paciente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                agregarMedico(medico);
            }
        }

        private void telfmedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }
    }
}
