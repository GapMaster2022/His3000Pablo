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
using His.Entidades.Clases;
using System.Globalization;

namespace His.Formulario
{
    public partial class frm_CertificadoIESS : Form
    {
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        NegQuirofano Quirofano = new NegQuirofano();
        MaskedTextBox codMedico;
        MEDICOS medico = null;
        internal static string hc; //Historial Clinico del paciente
        internal static string ate_codigo; //almacena el codigo del paciente elegido
        internal static string identificacion; //almacena el nro de cedula
        internal static string nom_medico; //Almacena el nombre del medico
        internal static string fechaalta;
        internal static string fechaingreso;
        internal static string med_email;
        internal static string med_identificacion;
        private static string tipo_ingreso; //contiene el tipo de ingreso del paciente
        private static string FechadeIngreso; //Contiene la fecha en palabra ejem: 1 (Uno) de Enero del 2020
        private static string FechadeAlta; //Contiene la fecha en palabras
        private string dias_reposo; //contiene los dias de reposo
        private static bool hosp_sin_alta;
        private static int cont = 0;
        public bool Pacientes = false;
        public bool mushugñan = false;
        public string telfmedico = "";
        public bool valida = false;
        public bool abre = true;
        public int ingreso;
        public bool reimprimir = false;
        public frm_CertificadoIESS()
        {
            InitializeComponent();
            txtdias.Enabled = false;
            MEDICOS med = new MEDICOS();
            med = NegMedicos.RecuperaMedicoIdUsuario(Sesion.codUsuario);
            var val = med.USUARIOSReference.EntityKey.EntityKeyValues[0].Value;
            if (Sesion.codDepartamento != 1)
                if (Convert.ToInt32(val) == 1)
                {
                    MessageBox.Show("Ud. No tiene acceso a generar certificados medicos ya que no esta registrado como un usuario medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    abre = false;
                    this.Close();
                    return;
                }
            agregarMedico(med);
        }
        public frm_CertificadoIESS(int AteCodigo, Int32 _hc)
        {
            InitializeComponent();
            CargarTipoContingencia();
            MEDICOS med = new MEDICOS();
            med = NegMedicos.RecuperaMedicoIdUsuario(Sesion.codUsuario);
            var val = med.USUARIOSReference.EntityKey.EntityKeyValues[0].Value;
            if (Convert.ToInt32(val) == 1)
            {
                MessageBox.Show("Ud. No tiene acceso a generar certificados medicos ya que no esta registrado como un usuario medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                abre = false;
                this.Close();
                return;
            }
            cmbTipos.SelectedIndex = -1;
            ate_codigo = Convert.ToString(AteCodigo);
            txt_historiaclinica.Focus();
            txt_historiaclinica.Text = _hc.ToString();
            btnañadir.Focus();
            mushugñan = true;
            agregarMedico(med);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Int32 CMI_CODIGO = 0;
            bool sinmedico = false;
            if (!validaCampos())
            {
                DataTable vigentes = new DataTable();
                vigentes = NegCertificadoMedico.VerificaEstadoIESS(Convert.ToInt64(ate_codigo));
                if (vigentes.Rows.Count > 0)
                {
                    for (int i = 0; i < vigentes.Rows.Count; i++)
                    {
                        if (vigentes.Rows[i][14].ToString() == "True" && medico.MED_CODIGO == Convert.ToInt32(vigentes.Rows[i][2].ToString()))
                        {

                            MessageBox.Show("No se puede guardar un Certificado nuevo, ya que el Dr. tiene realizado un Certificado para este paciente y el mismo esta ACTIVO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                    }
                }
                try
                {
                    if (rbEmergencia.Checked)
                        ingreso = 1;
                    else if (rbHospitalizacion.Checked)
                        ingreso = 2;
                    else if (rbConsultaExterna.Checked)
                    {
                        ingreso = NegTipoIngreso.RecuperarporAtencion(Convert.ToInt64(ate_codigo));
                    }
                    string path = "";
                    ingreso = NegTipoIngreso.RecuperarporAtencion(Convert.ToInt64(ate_codigo));
                    switch (ingreso)
                    {
                        case 10:
                            path = NegUtilitarios.RutaLogo("Mushuñan");
                            break;
                        case 12:
                            path = NegUtilitarios.RutaLogo("BrigadaMedica");
                            break;
                        default:
                            path = Certificado.path();
                            break;
                    }
                    if (medico == null)
                    {
                        Int32 CMG = Certificado.ContadorCertificado();
                        Int32 CME = Certificado.ContadorCertificadoEspecial();
                        if (CMG > CME)
                            CMI_CODIGO = CMG + 1;
                        else
                            CMI_CODIGO = CME + 1;
                        if (cbTratamiento.SelectedItem == null)
                        {
                            if (txtcontigencia.Visible)
                            {
                                Certificado.InsertarCertificadoIESS(CMI_CODIGO, ate_codigo, Convert.ToString(His.Entidades.Clases.Sesion.codMedico), txtobservacion.Text, DateTime.Now, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text, txtConfirmado.Text, dtpCirugia.Value,
                                txtSintomas.Text, txtNota.Text, ingreso, true, enfermedad, sintomas, reposo, aislamiento, teletrabajo, txtDireccionPac.Text, txtTelefonoPac.Text, "", Convert.ToDateTime(txtfechaalta.Text)
                                , txtprocedimiento.Text, "");
                            }
                            else
                            {
                                Certificado.InsertarCertificadoIESS(CMI_CODIGO, ate_codigo, Convert.ToString(Sesion.codMedico), txtobservacion.Text, DateTime.Now, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem), txtConfirmado.Text, dtpCirugia.Value,
                                txtSintomas.Text, txtNota.Text, ingreso, true, enfermedad, sintomas, reposo, aislamiento, teletrabajo, txtDireccionPac.Text, txtTelefonoPac.Text, "", Convert.ToDateTime(txtfechaalta.Text)
                                , txtprocedimiento.Text, "");
                            }
                        }
                        else
                        {
                            if (txtcontigencia.Visible)
                            {
                                Certificado.InsertarCertificadoIESS(CMI_CODIGO, ate_codigo, Convert.ToString(His.Entidades.Clases.Sesion.codMedico), txtobservacion.Text, DateTime.Now, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text, txtConfirmado.Text, dtpCirugia.Value,
                                txtSintomas.Text, txtNota.Text, ingreso, true, enfermedad, sintomas, reposo, aislamiento, teletrabajo, txtDireccionPac.Text, txtTelefonoPac.Text, "", Convert.ToDateTime(txtfechaalta.Text)
                                , txtprocedimiento.Text, cbTratamiento.SelectedItem.ToString());
                            }
                            else
                            {
                                Certificado.InsertarCertificadoIESS(CMI_CODIGO, ate_codigo, Convert.ToString(Sesion.codMedico), txtobservacion.Text, DateTime.Now, txtdias.Text,
                                txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem), txtConfirmado.Text, dtpCirugia.Value,
                                txtSintomas.Text, txtNota.Text, ingreso, true, enfermedad, sintomas, reposo, aislamiento, teletrabajo, txtDireccionPac.Text, txtTelefonoPac.Text, "", Convert.ToDateTime(txtfechaalta.Text)
                                , txtprocedimiento.Text, cbTratamiento.SelectedItem.ToString());
                            }

                        }
                    }
                    else
                    {
                        Int32 CMG = Certificado.ContadorCertificado();
                        Int32 CME = Certificado.ContadorCertificadoEspecial();
                        if (CMG > CME)
                            CMI_CODIGO = CMG + 1;
                        else
                            CMI_CODIGO = CME + 1;
                        if (txtcontigencia.Visible)
                        {
                            Certificado.InsertarCertificadoIESS(CMI_CODIGO, ate_codigo, medico.MED_CODIGO.ToString(), txtobservacion.Text, DateTime.Now, txtdias.Text,
                            txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text, txtConfirmado.Text, dtpCirugia.Value,
                            txtSintomas.Text, txtNota.Text, ingreso, true, enfermedad, sintomas, reposo, aislamiento, teletrabajo, txtDireccionPac.Text, txtTelefonoPac.Text, "", Convert.ToDateTime(txtfechaalta.Text)
                            , txtprocedimiento.Text, cbTratamiento.SelectedItem.ToString());
                        }
                        else
                        {
                            if (Tratamiento.Visible == true)
                            {
                                Certificado.InsertarCertificadoIESS(CMI_CODIGO, ate_codigo, medico.MED_CODIGO.ToString(), txtobservacion.Text, DateTime.Now, txtdias.Text,
                            txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem), txtConfirmado.Text, dtpCirugia.Value,
                            txtSintomas.Text, txtNota.Text, ingreso, true, enfermedad, sintomas, reposo, aislamiento, teletrabajo, txtDireccionPac.Text, txtTelefonoPac.Text, "", Convert.ToDateTime(txtfechaalta.Text)
                            , txtprocedimiento.Text, cbTratamiento.SelectedItem.ToString());
                            }
                            else
                            {
                                Certificado.InsertarCertificadoIESS(CMI_CODIGO, ate_codigo, medico.MED_CODIGO.ToString(), txtobservacion.Text, DateTime.Now, txtdias.Text,
                            txtactividad.Text, cmbTipos.GetItemText(cmbTipos.SelectedItem), txtConfirmado.Text, dtpCirugia.Value,
                            txtSintomas.Text, txtNota.Text, ingreso, true, enfermedad, sintomas, reposo, aislamiento, teletrabajo, txtDireccionPac.Text, txtTelefonoPac.Text, "", Convert.ToDateTime(txtfechaalta.Text)
                            , txtprocedimiento.Text, "");
                            }

                        }
                    }


                    for (int i = 0; i < TablaDiagnostico.Rows.Count; i++)
                    {
                        Certificado.InsertarCertificadoDetalleIESS(TablaDiagnostico.Rows[i].Cells["cod"].Value.ToString());
                    }

                    DataTable reporteDatos = new DataTable();
                    DataTable detalleReporteDatos = new DataTable();
                    reporteDatos = Certificado.CargarDatosCertificadoIESS(ate_codigo, Convert.ToString(CMI_CODIGO));
                    detalleReporteDatos = Certificado.CargarDatosCertificadoIESS_Detalle(Convert.ToInt64(reporteDatos.Rows[0][5].ToString()));

                    Certificado_Medico CM = new Certificado_Medico();

                    PACIENTES_DATOS_ADICIONALES pacien = new PACIENTES_DATOS_ADICIONALES();
                    PACIENTES pacienteActual = new PACIENTES();
                    pacienteActual = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));

                    pacien = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);

                    EMPRESA empresa = new EMPRESA();
                    empresa = NegEmpresa.RecuperaEmpresa();

                    SUCURSALES sucursal = NegEmpresa.RecuperaEmpresaID(1);

                    DataRow drCertificado;
                    foreach (DataRow item in detalleReporteDatos.Rows)
                    {
                        drCertificado = CM.Tables["IESS"].NewRow();
                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                        FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                        //if (FechadeIngreso.IndexOf(":") > 0)
                        //    FechadeIngreso = FechadeIngreso.Substring(0, FechadeIngreso.Length - 5);
                        drCertificado["FechaIngreso"] = FechadeIngreso;
                        dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][7].ToString());
                        drCertificado["Dias_Reposo"] = drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][7].ToString() + " (" + dias_reposo + ")";
                        if (reporteDatos.Rows[0][4].ToString() == "")
                        {
                            FechadeAlta = Fecha_En_Palabra(txtfechaalta.Text);
                            //if (FechadeAlta.IndexOf(":") > 0)
                            //    FechadeAlta = FechadeAlta.Substring(0, FechadeAlta.Length - 5);
                            drCertificado["FechaAlta"] = FechadeAlta;
                            DateTime Fecha = Convert.ToDateTime(txtfechaalta.Text);
                            string resultado = Fecha_En_Palabra_Hora(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][7].ToString()) - 1)));
                            string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(txtfechaalta.Text).ToShortDateString());
                            string fechaAyudahora = Fecha_En_Palabra_Hora(Convert.ToDateTime(txtfechaalta.Text).ToShortDateString());
                            if (rSi.Checked)
                            {
                                drCertificado["Dias_FinReposo"] = "Desde: " + fechaAyudahora + "  Hasta: " + resultado;
                            }
                            drCertificado["FechaAyuda"] = fechaAyuda;
                        }
                        else
                        {
                            FechadeAlta = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                            //if (FechadeAlta.IndexOf(":") > 0)
                            //    FechadeAlta = FechadeAlta.Substring(0, FechadeAlta.Length - 5);
                            drCertificado["FechaAlta"] = FechadeAlta;
                            //string fechaAyuda = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                            string fechaAyuda =  Fecha_Actual_En_Palabra(txtfechaalta.Text);
                            //string fechaAyuda = FechadeAlta;
                            string fechaAyudahora = Fecha_En_Palabra_Hora(reporteDatos.Rows[0][4].ToString());

                            drCertificado["FechaAyuda"] = fechaAyuda;
                            DateTime Fecha = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                            string resultado = Fecha_En_Palabra_Hora(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][7].ToString()) - 1)));
                            if (resultado.IndexOf(":") > 0)
                                resultado = resultado.Substring(0, resultado.Length - 5);
                            if (rSi.Checked)
                            {
                                drCertificado["Dias_FinReposo"] = "Desde: " + fechaAyudahora + "  Hasta: " + resultado;
                            }

                        }
                        if (sinmedico == true)
                        {
                            USUARIOS objUsuario = NegUsuarios.RecuperaUsuario(Entidades.Clases.Sesion.codUsuario);

                            drCertificado["NombreMedico"] = objUsuario.APELLIDOS + " " + objUsuario.NOMBRES;
                            drCertificado["Email_Medico"] = objUsuario.EMAIL;
                            drCertificado["Identificacion_Medico"] = objUsuario.IDENTIFICACION.Substring(0, 10);
                            drCertificado["telefonoMedico"] = pacien.DAP_TELEFONO2;
                        }
                        else
                        {
                            drCertificado["NombreMedico"] = txtmedico.Text;
                            drCertificado["Email_Medico"] = txtemail.Text;
                            drCertificado["Identificacion_Medico"] = txtmedcedula.Text.Substring(0, 10);
                            drCertificado["telefonoMedico"] = telfmedico;
                        }
                        drCertificado["Empresa"] = reporteDatos.Rows[0][22].ToString();
                        drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][23].ToString();
                        drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][24].ToString();
                        drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][5].ToString();
                        if (txtcontigencia.Visible)
                        {
                            drCertificado["institucionLabora"] = txtobservacion.Text;
                            drCertificado["actividadLaboral"] = txtactividad.Text;
                            drCertificado["tipoContingencia"] = cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text;
                        }
                        else
                        {
                            drCertificado["institucionLabora"] = txtobservacion.Text;
                            drCertificado["actividadLaboral"] = txtactividad.Text;
                            drCertificado["tipoContingencia"] = cmbTipos.GetItemText(cmbTipos.SelectedItem);
                        }
                        drCertificado["sintomas"] = txtSintomas.Text;
                        drCertificado["confirmado"] = txtConfirmado.Text;
                        drCertificado["nota"] = txtNota.Text;
                        if (enfermedad)
                        {
                            drCertificado["eSi"] = "X";
                        }
                        else
                        {
                            drCertificado["eNo"] = "X";
                        }
                        if (sintomas)
                        {
                            drCertificado["sSi"] = "X";
                        }
                        else
                        {
                            drCertificado["sNo"] = "X";
                        }
                        if (reposo)
                        {
                            drCertificado["rSi"] = "X";
                        }
                        else
                        {
                            drCertificado["rNo"] = "X";
                        }
                        if (aislamiento)
                        {
                            drCertificado["aSi"] = "X";
                        }
                        else
                        {
                            drCertificado["aNo"] = "X";
                        }
                        if (teletrabajo)
                        {
                            drCertificado["tSi"] = "X";
                        }
                        else
                        {
                            drCertificado["tNo"] = "X";
                        }

                        drCertificado["actividadLaboral"] = txtactividad.Text;
                        DataTable estado = NegCertificadoMedico.TIPO_INGRESO_IESS(Convert.ToInt32(ate_codigo));
                        drCertificado["tipo"] = estado.Rows[0][0].ToString();
                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                        drCertificado["PathImagen"] = path;

                        drCertificado["Especialidad_Medico"] = especialidadesMed;

                        drCertificado["FechaTratamiento"] = "FECHA: " + dtpCirugia.Value.ToShortDateString();
                        drCertificado["Procedimiento"] = reporteDatos.Rows[0][32].ToString();


                        drCertificado["Tratamiento"] = reporteDatos.Rows[0][31].ToString();
                        drCertificado["direccionPaciente"] = txtDireccionPac.Text;
                        drCertificado["telefonoPaciente"] = txtTelefonoPac.Text;

                        CM.Tables["IESS"].Rows.Add(drCertificado);
                    }
                    if (Convert.ToInt32(reporteDatos.Rows[0][30].ToString()) != 1)
                    {
                        if (cbTratamiento.SelectedIndex == 1)
                        {
                            frmReportes myreport = new frmReportes(1, "CertificadoProcesoIESS", CM);
                            myreport.Show();
                        }
                        else
                        {
                            frmReportes myreport = new frmReportes(1, "CertificadoProcesoClinicoIESS", CM);
                            myreport.Show();
                        }
                    }
                    else
                    {
                        frmReportes myreport = new frmReportes(1, "CertificadoIESS", CM);
                        myreport.Show();
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //throw;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {

            frm_Ayuda_Certificado.id_usuario = Convert.ToString(Sesion.codUsuario);
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            x.mushugñan = true;
            if (Pacientes)
                x.emergencia = true;
            x.ShowDialog();
            x.FormClosed += X_FormClosed;
            CargarTipoContingencia();
            cmbTipos.SelectedIndex = -1;
            if (hc != null)
            {
                txt_historiaclinica.Text = hc;
                ultraGroupBox1.Enabled = true;
                ultraGroupBox2.Enabled = true;
                Tratamiento.Enabled = true;
                ultraGroupBox3.Enabled = true;
                ayudaPacientes.Enabled = false;
            }
            else
            {
                limpiar();
            }
        }

        private void btnañadir_Click(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text != "")
            {
                errorProvider1.Clear();
                foreach (DataGridViewRow fila1 in TablaDiagnostico.Rows)
                {
                    if (fila1.Cells[1].Value != null)
                    {
                        cont++;
                    }

                }
                if (cont < 5)
                {
                    frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                    busqueda.ShowDialog();
                    if (busqueda.codigo != null)
                    {
                        cont = 0;
                        if (TablaDiagnostico.Rows.Count != 0)
                        {
                            for (int i = 0; i < TablaDiagnostico.Rows.Count; i++)
                            {
                                if (TablaDiagnostico.Rows[i].Cells[0].Value.ToString() == busqueda.codigo)
                                {
                                    MessageBox.Show("Codigo CIE-10 existente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    return;
                                }
                            }
                            TablaDiagnostico.Rows.Add(busqueda.codigo, busqueda.resultado);
                        }
                        else
                            TablaDiagnostico.Rows.Add(busqueda.codigo, busqueda.resultado);

                    }
                    TablaDiagnostico.Focus();
                }
                else
                {
                    MessageBox.Show("Solo puede ingresar un maximo de 5 DIAGNOSTICOS DE INGRESO");
                    cont = 0;
                }
            }
            else
            {
                errorProvider1.SetError(txt_historiaclinica, "Por favor elija el HC del Paciente");
            }
        }

        private void btnmedico_Click(object sender, EventArgs e)
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
        }

        private void cmbTipos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipos.SelectedIndex != -1)
                {
                    if (cmbTipos.SelectedValue.ToString() == "5")//cuando el tipo de contingencia es otros.
                    {
                        txtcontigencia.Visible = true;
                    }
                    else
                    {
                        txtcontigencia.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void cbTratamiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTratamiento.SelectedIndex == 0)
            {
                lblfecha.Visible = false;
                dtpCirugia.Visible = false;
                lblproce.Visible = false;
                txtprocedimiento.Visible = false;
            }
            else if (cbTratamiento.SelectedIndex == 1)
            {
                lblfecha.Visible = true;
                dtpCirugia.Visible = true;
                lblproce.Visible = true;
                txtprocedimiento.Visible = true;

                HC_PROTOCOLO_OPERATORIO protocolo = new HC_PROTOCOLO_OPERATORIO();
                protocolo = NegProtocoloOperatorio.recuperarProtocolo(Convert.ToInt32(ate_codigo));
                if (protocolo != null)
                {
                    txtprocedimiento.Text = protocolo.PROT_POSTOPERATORIO;
                }
            }
        }
        public void CargarTipoContingencia()
        {
            try
            {
                cmbTipos.DataSource = NegCertificadoMedico.TiposContingencia();
                cmbTipos.DisplayMember = "DESCRIPCION";
                cmbTipos.ValueMember = "CODIGO";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontraron Tipos de contingencia.\r\nMás detalle: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        public void limpiar()
        {
            txt_apellido1.Text = "";
            txt_apellido2.Text = "";
            txt_nombre1.Text = "";
            txt_nombre2.Text = "";
            txt_historiaclinica.Text = "";
            txtfechaalta.Text = "";
            txtfechaingreso.Text = "";
            txtSintomas.Text = "";
            txtConfirmado.Text = "";
            txtNota.Text = "";
            eSi.Checked = false;
            eNo.Checked = false;
            sSi.Checked = false;
            sNo.Checked = false;
            rSi.Checked = false;
            rNo.Checked = false;
            aSi.Checked = false;
            aNo.Checked = false;
            tSi.Checked = false;
            tNo.Checked = false;
            TablaDiagnostico.Rows.Clear();
            txtdias.Text = "1";
            txtobservacion.Text = "";
            cbTratamiento.DataBindings.Clear();
            txtprocedimiento.Text = "";
            txtactividad.Text = "";
            txtcontigencia.Text = "";
            txtTelefonoPac.Text = "";
            txtDireccionPac.Text = "";
            txt_historiaclinica.Text = hc;
            ultraGroupBox1.Enabled = false;
            ultraGroupBox2.Enabled = false;
            ultraGroupBox3.Enabled = false;
            Tratamiento.Visible = false;
            ayudaPacientes.Enabled = true;
            reimprimir = false;
            radioButton1.Checked = false;
            errorProvider1.Clear();
        }
        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hc != null)
            {
                txt_historiaclinica.Text = hc;
                ultraGroupBox1.Enabled = true;
                ultraGroupBox2.Enabled = true;
                //Tratamiento.Enabled = true;
                ayudaPacientes.Enabled = false;
            }
            else
            {
                limpiar();
            }
        }
        private bool validaCampos()
        {
            errorProvider1.Clear();
            valida = false;
            if (cmbTipos.Text == "")
            {
                errorProvider1.SetError(cmbTipos, "Por Favor Seleccione un Tipo de Contingencia");
                valida = true;
            }
            if (txt_historiaclinica.Text == "")
            {
                errorProvider1.SetError(txt_historiaclinica, "Por Favor eliga la HC del Paciente");
                valida = true;
            }
            if (TablaDiagnostico.Rows.Count <= 0)
            {
                errorProvider1.SetError(TablaDiagnostico, "El Paciente debe tener minimo un Diagnostico");
                valida = true;
            }
            if (Tratamiento.Visible == true)
            {
                if (cbTratamiento.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cbTratamiento, "Campo Obligatorio");
                    if (txtprocedimiento.Visible == true)
                    {
                        if (txtprocedimiento.Text == "")
                        {
                            errorProvider1.SetError(txtprocedimiento, "Campo Obligatorio");
                            valida = true;
                        }
                    }
                    valida = true;
                }
            }
            if (Tratamiento.Visible == true)
            {
                if (dtpCirugia.Value > DateTime.Now)
                {
                    errorProvider1.SetError(dtpCirugia, "La fecha no puede ser mayor a la fecha actual");
                    valida = true;
                }
            }
            if (txtmedico.Text == "")
            {
                errorProvider1.SetError(txtmedico, "Campo Obligatorio");
                valida = true;
            }
            if (txtobservacion.Text == "")
            {
                errorProvider1.SetError(txtobservacion, "Campo Obligatorio");
                valida = true;
            }
            if (txtactividad.Text == "")
            {
                errorProvider1.SetError(txtactividad, "Campo Obligatorio");
                valida = true;
            }
            if (txtdias.Text.Trim() == "")
            {
                txtdias.Text = "1";
                errorProvider1.SetError(txtdias, "Campo Obligatorio");
                valida = true;
            }
            if (!eSi.Checked && !eNo.Checked)
            {
                errorProvider1.SetError(eNo, "Campo Obligatorio");
                valida = true;
            }
            if (!sSi.Checked && !sNo.Checked)
            {
                errorProvider1.SetError(sNo, "Campo Obligatorio");
                valida = true;
            }
            if (!rSi.Checked && !rNo.Checked)
            {
                errorProvider1.SetError(rNo, "Campo Obligatorio");
                valida = true;
            }
            if (!aSi.Checked && !aNo.Checked)
            {
                errorProvider1.SetError(aNo, "Campo Obligatorio");
                valida = true;
            }
            if (!tSi.Checked && !tNo.Checked)
            {
                errorProvider1.SetError(tNo, "Campo Obligatorio");
                valida = true;
            }
            if (txtfechaalta.Text == "")
            {
                errorProvider1.SetError(txtfechaalta, "Campo Obligatorio");
                valida = true;
            }
            if (sSi.Checked)
            {
                if (txtSintomas.Text == "")
                {
                    errorProvider1.SetError(txtSintomas, "Campo Obligatorio");
                    valida = true;
                }
            }
            if (txtNota.Text == "")
            {
                errorProvider1.SetError(txtNota, "Campo Obligatorio");
                valida = true;
            }
            if (txtConfirmado.Text == "")
            {
                errorProvider1.SetError(txtConfirmado, "Campo Obligatorio");
                valida = true;
            }
            if (txtcontigencia.Visible)
            {
                if (txtcontigencia.Text == "")
                {
                    errorProvider1.SetError(txtcontigencia, "Campo Obligatorio");
                    valida = true;
                }
            }
            return valida;
        }
        string pathim = "";
        private void txt_historiaclinica_TextChanged(object sender, EventArgs e)
        {
            bool estado = false;
            if (txt_historiaclinica.Text != "")
            {
                CERTIFICADO_MEDICO_IESS cert = new CERTIFICADO_MEDICO_IESS();
                MEDICOS med = new MEDICOS();
                med = NegMedicos.RecuperaMedicoIdUsuario(His.Entidades.Clases.Sesion.codUsuario);
                cert = NegCertificadoMedico.RecuperaCertificadoIESSDuplicado(Convert.ToInt64(ate_codigo), med.MED_CODIGO);
                if (cert == null)
                {
                    DataTable vigentes = new DataTable();
                    vigentes = NegCertificadoMedico.VerificaEstadoIESS(Convert.ToInt64(ate_codigo));
                    if (vigentes.Rows.Count > 0)
                    {
                        if (vigentes.Rows[0][14].ToString() == "True")
                        {
                            estado = true;
                            if (MessageBox.Show("Paciente tiene un certificado para IESS ACTIVO, DESEA GENERAR UN NUEVO CERTIFICADO???", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                this.Close();
                                return;
                            }
                        }
                    }
                    DataTable ultima = NegAtenciones.Atencion(Convert.ToInt64(ate_codigo));
                    if (ultima.Rows[0][2].ToString() != "")
                    {
                        DateTime diferencia = Convert.ToDateTime(ultima.Rows[0][2].ToString());
                        DateTime fechaActual = DateTime.Now;
                        int resultado = (fechaActual - diferencia).Days;
                        DataTable dias = NegAtenciones.RecuperaParametroCertificado();
                        if (Convert.ToInt16(dias.Rows[0][0].ToString()) < resultado)
                        {
                            if (vigentes.Rows.Count <= 0)
                            {
                                MessageBox.Show("El paciente fue dado de alta hace: " + resultado + " días por lo que ya no se puede generar un Certificado Médico para esta atención", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this.Close();
                                return;
                            }
                        }
                    }
                    DataTable Tabla = new DataTable(); //Almacenara los nombres y apellidos del paciente por hc
                    Tabla = Certificado.BuscarPaciente(ate_codigo);
                    ATENCIONES atencionActual = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
                    int TipoConsulta = 0;
                    //validar los cie solo del formulario 008 y 002
                    List<CIE10> Cie10 = new List<CIE10>();
                    Cie10 = NegCIE10.RecuperarCieFormulario(atencionActual.ATE_CODIGO);
                    if (Cie10.Count > 0)
                    {
                        TablaDiagnostico.Rows.Clear();
                        foreach (var item in Cie10)
                        {
                            TablaDiagnostico.Rows.Add(item.CIE_CODIGO, item.CIE_DESCRIPCION);
                        }
                    }
                    foreach (DataRow item in Tabla.Rows)
                    {
                        txt_apellido1.Text = item[0].ToString();
                        txt_apellido2.Text = item[1].ToString();
                        txt_nombre1.Text = item[2].ToString();
                        txt_nombre2.Text = item[3].ToString();
                        tipo_ingreso = item[5].ToString();
                        identificacion = item[6].ToString();
                        TipoConsulta = Convert.ToInt16(item[12].ToString());
                        txtTelefonoPac.Text = item[13].ToString();
                        txtDireccionPac.Text = item[14].ToString();
                    }
                    txtfechaingreso.Text = atencionActual.ATE_FECHA_INGRESO.ToString();
                    ultraGroupBox1.Enabled = true;
                    ultraGroupBox2.Enabled = true;
                    Tratamiento.Enabled = true;
                    ultraGroupBox3.Enabled = true;
                    ayudaPacientes.Enabled = false;

                    if (atencionActual.ATE_FECHA_ALTA != null)
                        txtfechaalta.Text = atencionActual.ATE_FECHA_ALTA.ToString();
                    if (vigentes.Rows.Count > 0)
                    {
                        bool estado2 = false;
                        medico = NegMedicos.RecuperaMedicoIdUsuario(Sesion.codUsuario);
                        if (estado)
                        {
                            if (MessageBox.Show("El paciente tiene información en un certificado ACTIVO. \n ¿DESEA CARGAR ESTA INFORMACION?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                estado2 = true;
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("El paciente tiene información en un certificado anulado. \n ¿DESEA CARGAR ESTA INFORMACION?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                estado2 = true;
                            }
                        }
                        if (estado2)
                        {
                            CargarTipoContingencia();
                            ultraGroupBox1.Enabled = false;
                            ultraGroupBox2.Enabled = false;
                            ultraGroupBox3.Enabled = false;
                            TablaDiagnostico.Rows.Clear();
                            txtdias.Text = vigentes.Rows[0][5].ToString();
                            string[] valor = vigentes.Rows[0][3].ToString().Split('|');
                            txtobservacion.Text = valor[0].ToString();
                            txtactividad.Text = vigentes.Rows[0][6].ToString();
                            txtTelefonoPac.Text = vigentes.Rows[0][21].ToString();
                            txtDireccionPac.Text = vigentes.Rows[0][20].ToString();
                            if (Convert.ToBoolean(vigentes.Rows[0][15].ToString()))
                                eSi.Checked = true;
                            else
                                eNo.Checked = true;
                            if (Convert.ToBoolean(vigentes.Rows[0][16].ToString()))
                                sSi.Checked = true;
                            else
                                sNo.Checked = true;
                            if (Convert.ToBoolean(vigentes.Rows[0][17].ToString()))
                                rSi.Checked = true;
                            else
                            {
                                rNo.Checked = true;
                                txtdias.Text = "0";
                            }
                            if (Convert.ToBoolean(vigentes.Rows[0][18].ToString()))
                                aSi.Checked = true;
                            else
                                aNo.Checked = true;
                            if (Convert.ToBoolean(vigentes.Rows[0][19].ToString()))
                                tSi.Checked = true;
                            else
                                tNo.Checked = true;
                            txtdias.Text = vigentes.Rows[0][5].ToString();
                            txtSintomas.Text = vigentes.Rows[0][11].ToString();
                            txtConfirmado.Text = vigentes.Rows[0][8].ToString();
                            txtNota.Text = vigentes.Rows[0][12].ToString();
                            txtfechaalta.Text = vigentes.Rows[0][23].ToString();
                            cmbTipos.Text = vigentes.Rows[0][7].ToString();
                            pathim = vigentes.Rows[0][1].ToString();
                            DataTable reporteDatos = new DataTable();
                            DataTable detalleReporteDatos = new DataTable();
                            reporteDatos = Certificado.CargarDatosCertificadoIESS(ate_codigo.ToString(), vigentes.Rows[0][0].ToString());
                            detalleReporteDatos = Certificado.CargarDatosCertificadoIESS_Detalle(Convert.ToInt64(reporteDatos.Rows[0][5].ToString()));
                            //TablaDiagnostico.DataSource = detalleReporteDatos;
                            TablaDiagnostico.Rows.Add(detalleReporteDatos.Rows[0][0].ToString(), detalleReporteDatos.Rows[0][1].ToString());
                            medico = NegMedicos.RecuperaMedicoId(med.MED_CODIGO);
                            agregarMedico(medico);
                            //btnImprimir.Visible = false;
                            //btnReimprimir.Visible = true;
                            //txtdias.Text = vigentes.Rows[0][5].ToString();
                            if (Convert.ToInt32(vigentes.Rows[0][13].ToString()) != 3)
                            {
                                cbTratamiento.SelectedItem = vigentes.Rows[0][9].ToString();
                                txtprocedimiento.Text = vigentes.Rows[0][24].ToString();
                                dtpCirugia.Value = Convert.ToDateTime(vigentes.Rows[0][10].ToString());
                            }
                            errorProvider1.Clear();
                            reimprimir = true;
                        }
                    }
                    if (tipo_ingreso == "EMERGENCIA")
                    {
                        if (txtfechaalta.Text != "")
                        {
                            radioButton1.Enabled = true;
                            radioButton1.Checked = true;
                        }
                        else
                        {
                            radioButton1.Enabled = false;
                            radioButton1.Checked = false;
                        }
                        rbEmergencia.Checked = true;
                        rbHospitalizacion.Checked = false;
                        rbConsultaExterna.Checked = false;
                        Tratamiento.Visible = false;
                        CargarCie10Emerg();
                    }
                    else if (tipo_ingreso == "CONSULTA EXTERNA" || TipoConsulta == 4 || TipoConsulta == 10)
                    {
                        rbConsultaExterna.Checked = true;
                        rbEmergencia.Checked = false;
                        rbHospitalizacion.Checked = false;
                        radioButton1.Enabled = false;
                        Tratamiento.Visible = true;
                        CargarCie10ConsultaE();
                    }
                    else
                    {
                        rbHospitalizacion.Checked = true;
                        rbEmergencia.Checked = false;
                        rbConsultaExterna.Checked = false;
                        radioButton1.Enabled = true;
                        Tratamiento.Visible = true;
                        CargarCie10Hosp();
                    }
                    if (txtfechaalta.Text == "")
                    {
                        radioButton1.Enabled = true;
                    }
                    else
                    {
                        radioButton1.Enabled = false;
                        radioButton1.Checked = true;
                    }
                }
                else
                {
                    MessageBox.Show("Paciente Cuenta con Certificado medico elaborado por ud.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
        }


        public void CargarCie10Hosp() //Cargara el cie10 de acorde al tipo de atencion que tuvo el paciente
        {
            //cbCie10.DataSource = Certificado.CargarCie10Hosp(ate_codigo);
            //cbCie10.DisplayMember = "CIE_DESCRIPCION";
            //cbCie10.ValueMember = "CIE_CODIGO";
        }
        public void CargarCie10Emerg()
        {
            //cbCie10.DataSource = Certificado.CargarCie10Emerg(ate_codigo);
            //cbCie10.DisplayMember = "CIE_DESCRIPCION";
            //cbCie10.ValueMember = "CIE_CODIGO";
        }
        public void CargarCie10ConsultaE()
        {
            //cbCie10.DataSource = Certificado.CargarCie10Consulta(ate_codigo);
            //cbCie10.DisplayMember = "CIE_DESCRIPCION";
            //cbCie10.ValueMember = "CIE_CODIGO";
        }
        public string especialidadesMed = "";
        private void agregarMedico(MEDICOS medicoTratante)
        {
            if (medicoTratante != null && Convert.ToInt16(medicoTratante.USUARIOSReference.EntityKey.EntityKeyValues[0].Value) != 1)
            {
                txtmedico.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                txtmedcedula.Text = medicoTratante.MED_RUC;
                txtemail.Text = medicoTratante.MED_EMAIL;
                telfmedico = medicoTratante.MED_TELEFONO_CONSULTORIO;
                DataTable espMedicas = NegMedicos.RecuperaEspecialidadMed(Convert.ToInt32(medicoTratante.MED_CODIGO));
                especialidadesMed = espMedicas.Rows[0][0].ToString();
            }
            else
            {
                txtmedico.Text = "ADMINISTRADOR";
                txtmedcedula.Text = medicoTratante.MED_RUC;
                txtemail.Text = medicoTratante.MED_EMAIL;
                telfmedico = medicoTratante.MED_TELEFONO_CONSULTORIO;
                DataTable espMedicas = NegMedicos.RecuperaEspecialidadMed(Convert.ToInt32(medicoTratante.MED_CODIGO));
                especialidadesMed = espMedicas.Rows[0][0].ToString();
            }
        }
        public bool enfermedad = false;
        public bool sintomas = false;
        public bool reposo = false;
        public bool aislamiento = false;
        public bool teletrabajo = false;

        private void sSi_CheckedChanged(object sender, EventArgs e)
        {
            if (sSi.Checked)
            {
                sNo.Checked = false;
                sintomas = true;
                txtSintomas.Enabled = true;
            }
        }

        private void sNo_CheckedChanged(object sender, EventArgs e)
        {
            if (sNo.Checked)
            {
                sSi.Checked = false;
                sintomas = false;
                txtSintomas.Enabled = false;
                txtSintomas.Text = "";
            }
        }

        private void rSi_CheckedChanged(object sender, EventArgs e)
        {
            if (rSi.Checked)
            {
                rNo.Checked = false;
                reposo = true;
                txtdias.Text = "1";
                txtdias.Enabled = true;
            }
        }

        private void rNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rNo.Checked)
            {
                rSi.Checked = false;
                reposo = false;
                txtdias.Text = "0";
                txtdias.Enabled = false;
            }
        }

        private void aSi_CheckedChanged(object sender, EventArgs e)
        {
            if (aSi.Checked)
            {
                aNo.Checked = false;
                aislamiento = true;
            }
        }

        private void aNo_CheckedChanged(object sender, EventArgs e)
        {
            if (aNo.Checked)
            {
                aSi.Checked = false;
                aislamiento = false;
            }
        }

        private void tSi_CheckedChanged(object sender, EventArgs e)
        {
            if (tSi.Checked)
            {
                tNo.Checked = false;
                teletrabajo = true;
            }
        }

        private void tNo_CheckedChanged(object sender, EventArgs e)
        {
            if (tNo.Checked)
            {
                tSi.Checked = false;
                teletrabajo = false;
            }
        }

        #region Dias en Palabras
        public string Dia_En_Palabras(string dia)
        {
            string prueba;
            string num2Text = ""; int value = Convert.ToInt32(dia);
            if (value == 0) num2Text = "NO";
            else if (value == 1) num2Text = "UNO";
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

        #endregion

        #region Fecha en Palabras
        public string Fecha_En_Palabra(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha, cul);
            if (Convert.ToInt32(FI.ToString("dd")) == 1)
            {
                fechaprueba = fecha.Substring(0, 10) + " (UNO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToInt32(FI.ToString("dd")) == 2)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DOS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 3)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TRES de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 4)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CUATRO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 5)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CINCO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 6)
            {
                fechaprueba = fecha.Substring(0, 10) + " (SEIS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 7)
            {
                fechaprueba = fecha.Substring(0, 10) + " (SIETE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 8)
            {
                fechaprueba = fecha.Substring(0, 10) + " (OCHO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 9)
            {
                fechaprueba = fecha.Substring(0, 10) + " (NUEVE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 10)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIEZ de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 11)
            {
                fechaprueba = fecha.Substring(0, 10) + " (ONCE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 12)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DOCE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 13)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TRECE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 14)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CATORCE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 15)
            {
                fechaprueba = fecha.Substring(0, 10) + " (QUINCE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 16)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECISEIS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 17)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECISIETE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 18)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECIOCHO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 19)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECINUEVE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 20)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 21)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIUNO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 22)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIDOS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 23)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTITRES de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 24)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTICUATRO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 25)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTICINCO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 26)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTISEIS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 27)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTISIETE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 28)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIOCHO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 29)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTINUEVE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 30)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TREINTA de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 31)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TREINTA Y UNO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else
                return "";
        }
        #endregion

        #region Fecha en Palabras sin horas
        public string Fecha_En_Palabra_Hora(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha, cul);
            if (Convert.ToInt32(FI.ToString("dd")) == 1)
            {
                fechaprueba = fecha.Substring(0, 10) + " (UNO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToInt32(FI.ToString("dd")) == 2)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DOS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 3)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TRES DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 4)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CUATRO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 5)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CINCO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 6)
            {
                fechaprueba = fecha.Substring(0, 10) + " (SEIS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 7)
            {
                fechaprueba = fecha.Substring(0, 10) + " (SIETE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 8)
            {
                fechaprueba = fecha.Substring(0, 10) + " (OCHO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 9)
            {
                fechaprueba = fecha.Substring(0, 10) + " (NUEVE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 10)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIEZ DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 11)
            {
                fechaprueba = fecha.Substring(0, 10) + " (ONCE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 12)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DOCE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 13)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TRECE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 14)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CATORCE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 15)
            {
                fechaprueba = fecha.Substring(0, 10) + " (QUINCE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 16)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECISEIS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 17)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECISIETE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 18)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECIOCHO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 19)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECINUEVE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 20)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 21)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIUNO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 22)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIDOS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 23)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTITRES DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 24)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTICUATRO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 25)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTICINCO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 26)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTISEIS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 27)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTISIETE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 28)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIOCHO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 29)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTINUEVE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 30)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TREINTA DE " + FI.ToString("MMMM") + " DEL DOS MIL" + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 31)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TREINTA Y UNO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else
                return "";
        }
        #endregion

        #region Fecha Actual
        public string Fecha_Actual_En_Palabra(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha, cul);
            if (Convert.ToInt32(FI.ToString("dd")) == 1)
            {
                fechaprueba = FI.ToString("dddd") + " 1 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToInt32(FI.ToString("dd")) == 2)
            {
                fechaprueba = FI.ToString("dddd") + " 2  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 3)
            {
                fechaprueba = FI.ToString("dddd") + " 3  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 4)
            {
                fechaprueba = FI.ToString("dddd") + " 4  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 5)
            {
                fechaprueba = FI.ToString("dddd") + " 5  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 6)
            {
                fechaprueba = FI.ToString("dddd") + " 6  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 7)
            {
                fechaprueba = FI.ToString("dddd") + " 7  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 8)
            {
                fechaprueba = FI.ToString("dddd") + " 8  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 9)
            {
                fechaprueba = FI.ToString("dddd") + " 9  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 10)
            {
                fechaprueba = FI.ToString("dddd") + " 10 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 11)
            {
                fechaprueba = FI.ToString("dddd") + " 11 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 12)
            {
                fechaprueba = FI.ToString("dddd") + " 12 (DOCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 13)
            {
                fechaprueba = FI.ToString("dddd") + " 13 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 14)
            {
                fechaprueba = FI.ToString("dddd") + " 14 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 15)
            {
                fechaprueba = FI.ToString("dddd") + " 15 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 16)
            {
                fechaprueba = FI.ToString("dddd") + " 16 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 17)
            {
                fechaprueba = FI.ToString("dddd") + " 17 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 18)
            {
                fechaprueba = FI.ToString("dddd") + " 18 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 19)
            {
                fechaprueba = FI.ToString("dddd") + " 19 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 20)
            {
                fechaprueba = FI.ToString("dddd") + " 20 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 21)
            {
                fechaprueba = FI.ToString("dddd") + " 21 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 22)
            {
                fechaprueba = FI.ToString("dddd") + " 22 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 23)
            {
                fechaprueba = FI.ToString("dddd") + " 23 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 24)
            {
                fechaprueba = FI.ToString("dddd") + " 24 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 25)
            {
                fechaprueba = FI.ToString("dddd") + " 25 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 26)
            {
                fechaprueba = FI.ToString("dddd") + " 26 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 27)
            {
                fechaprueba = FI.ToString("dddd") + " 27 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 28)
            {
                fechaprueba = FI.ToString("dddd") + " 28 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 29)
            {
                fechaprueba = FI.ToString("dddd") + " 29 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 30)
            {
                fechaprueba = FI.ToString("dddd") + " 30 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 31)
            {
                fechaprueba = FI.ToString("dddd") + " 31 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else
            {
                return "";
            }
        }
        #endregion
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtdias_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ATENCIONES atencionActual = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            string fechaalta = txtfechaalta.Text;
            if (atencionActual.ATE_FECHA_ALTA.ToString() != "null")
                txtfechaalta.Text = atencionActual.ATE_FECHA_ALTA.ToString();
            if (txtfechaalta.Text == "")
            {
                if (radioButton1.Checked)
                {
                    DateTime Hoy = DateTime.Now;
                    txtfechaalta.Text = "";
                    txtfechaalta.Text = Hoy.ToString();
                    hosp_sin_alta = true;
                    //txtdias.Enabled = true;
                    atencionActual.ATE_FECHA_ALTA = Hoy;
                }
                else
                {
                    txtdias.Enabled = false;
                }
            }
        }

        private void frm_CertificadoIESS_Load(object sender, EventArgs e)
        {
            //CargarTipoContingencia();
            txtdias.Enabled = false;
            txtSintomas.Enabled = false;
        }

        private void txtobservacion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                txtactividad.Focus();
            }
        }

        private void txtactividad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                cmbTipos.Focus();
            }
        }

        private void txtSintomas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                txtConfirmado.Focus();
            }
        }

        private void txtConfirmado_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                txtNota.Focus();
            }
        }

        private void txtNota_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                eSi.Focus();
            }
        }

        private void eSi_CheckedChanged_1(object sender, EventArgs e)
        {
            if (eSi.Checked)
            {
                eNo.Checked = false;
                enfermedad = true;
            }
        }

        private void eNo_CheckedChanged_1(object sender, EventArgs e)
        {
            if (eNo.Checked)
            {
                eSi.Checked = false;
                enfermedad = false;
            }
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            string path = "";
            ingreso = NegTipoIngreso.RecuperarporAtencion(Convert.ToInt64(ate_codigo));
            switch (ingreso)
            {
                case 10:
                    path = NegUtilitarios.RutaLogo("Mushuñan");
                    break;
                case 12:
                    path = NegUtilitarios.RutaLogo("BrigadaMedica");
                    break;
                default:
                    path = Certificado.path();
                    break;
            }
            DataTable reporteDatos = new DataTable();
            DataTable detalleReporteDatos = new DataTable();
            reporteDatos = Certificado.CargarDatosCertificadoIESS(ate_codigo);
            detalleReporteDatos = Certificado.CargarDatosCertificadoIESS_Detalle(Convert.ToInt64(reporteDatos.Rows[0][5].ToString()));

            Certificado_Medico CM = new Certificado_Medico();

            PACIENTES_DATOS_ADICIONALES pacien = new PACIENTES_DATOS_ADICIONALES();
            PACIENTES pacienteActual = new PACIENTES();
            pacienteActual = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));

            pacien = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);

            EMPRESA empresa = new EMPRESA();
            empresa = NegEmpresa.RecuperaEmpresa();

            SUCURSALES sucursal = NegEmpresa.RecuperaEmpresaID(1);

            DataRow drCertificado;
            foreach (DataRow item in detalleReporteDatos.Rows)
            {
                drCertificado = CM.Tables["IESS"].NewRow();
                drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                //if (FechadeIngreso.IndexOf(":") > 0)
                //    FechadeIngreso = FechadeIngreso.Substring(0, FechadeIngreso.Length - 5);
                drCertificado["FechaIngreso"] = FechadeIngreso;
                dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][7].ToString());
                drCertificado["Dias_Reposo"] = drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][7].ToString() + " (" + dias_reposo + ")";
                if (reporteDatos.Rows[0][4].ToString() == "")
                {
                    FechadeAlta = Fecha_En_Palabra(txtfechaalta.Text);
                    //if (FechadeAlta.IndexOf(":") > 0)
                    //    FechadeAlta = FechadeAlta.Substring(0, FechadeAlta.Length - 5);
                    drCertificado["FechaAlta"] = FechadeAlta;
                    DateTime Fecha = Convert.ToDateTime(txtfechaalta.Text);
                    string resultado = Fecha_En_Palabra_Hora(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][7].ToString()) - 1)));
                    string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(txtfechaalta.Text).ToShortDateString());
                    string fechaAyudahora = Fecha_En_Palabra_Hora(Convert.ToDateTime(txtfechaalta.Text).ToShortDateString());
                    if (rSi.Checked)
                    {
                        drCertificado["Dias_FinReposo"] = "Desde: " + fechaAyudahora + "  Hasta: " + resultado;
                    }

                    //if (fechaAyuda.IndexOf(":") > 0)
                    //    fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                    drCertificado["FechaAyuda"] = fechaAyuda;
                }
                else
                {
                    FechadeAlta = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                    //if (FechadeAlta.IndexOf(":") > 0)
                    //    FechadeAlta = FechadeAlta.Substring(0, FechadeAlta.Length - 5);
                    drCertificado["FechaAlta"] = FechadeAlta;
                    //string fechaAyuda = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                    string fechaAyuda = Fecha_Actual_En_Palabra(DateTime.Now.ToShortDateString());
                    string fechaAyudahora = Fecha_En_Palabra_Hora(reporteDatos.Rows[0][4].ToString());

                    drCertificado["FechaAyuda"] = fechaAyuda;
                    DateTime Fecha = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                    string resultado = Fecha_En_Palabra_Hora(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][7].ToString()) - 1)));
                    if (resultado.IndexOf(":") > 0)
                        resultado = resultado.Substring(0, resultado.Length - 5);
                    if (rSi.Checked)
                    {
                        drCertificado["Dias_FinReposo"] = "Desde: " + fechaAyudahora + "  Hasta: " + resultado;
                    }

                }

                drCertificado["NombreMedico"] = txtmedico.Text;
                drCertificado["Email_Medico"] = txtemail.Text;
                drCertificado["Identificacion_Medico"] = txtmedcedula.Text.Substring(0, 10);
                drCertificado["telefonoMedico"] = telfmedico;

                drCertificado["Empresa"] = reporteDatos.Rows[0][22].ToString();
                drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][23].ToString();
                drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][24].ToString();
                drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                drCertificado["Num_Certificado"] = reporteDatos.Rows[0][5].ToString();
                if (txtcontigencia.Visible)
                {
                    drCertificado["institucionLabora"] = txtobservacion.Text;
                    drCertificado["actividadLaboral"] = txtactividad.Text;
                    drCertificado["tipoContingencia"] = cmbTipos.GetItemText(cmbTipos.SelectedItem) + " - " + txtcontigencia.Text;
                }
                else
                {
                    drCertificado["institucionLabora"] = txtobservacion.Text;
                    drCertificado["actividadLaboral"] = txtactividad.Text;
                    drCertificado["tipoContingencia"] = cmbTipos.GetItemText(cmbTipos.SelectedItem);
                }
                drCertificado["sintomas"] = txtSintomas.Text;
                drCertificado["confirmado"] = txtConfirmado.Text;
                drCertificado["nota"] = txtNota.Text;
                if (enfermedad)
                {
                    drCertificado["eSi"] = "X";
                }
                else
                {
                    drCertificado["eNo"] = "X";
                }
                if (sintomas)
                {
                    drCertificado["sSi"] = "X";
                }
                else
                {
                    drCertificado["sNo"] = "X";
                }
                if (reposo)
                {
                    drCertificado["rSi"] = "X";
                }
                else
                {
                    drCertificado["rNo"] = "X";
                }
                if (aislamiento)
                {
                    drCertificado["aSi"] = "X";
                }
                else
                {
                    drCertificado["aNo"] = "X";
                }
                if (teletrabajo)
                {
                    drCertificado["tSi"] = "X";
                }
                else
                {
                    drCertificado["tNo"] = "X";
                }

                drCertificado["actividadLaboral"] = txtactividad.Text;
                DataTable estado = NegCertificadoMedico.TIPO_INGRESO_IESS(Convert.ToInt32(ate_codigo));
                drCertificado["tipo"] = estado.Rows[0][0].ToString();
                drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                drCertificado["PathImagen"] = path;
                drCertificado["Tratamiento"] = reporteDatos.Rows[0][31].ToString();
                drCertificado["Especialidad_Medico"] = especialidadesMed;
                drCertificado["FechaTratamiento"] = "FECHA: " + dtpCirugia.Value.ToShortDateString();
                drCertificado["Procedimiento"] = reporteDatos.Rows[0][32].ToString();

                drCertificado["direccionPaciente"] = txtDireccionPac.Text;
                drCertificado["telefonoPaciente"] = txtTelefonoPac.Text;

                CM.Tables["IESS"].Rows.Add(drCertificado);
            }
            if (Convert.ToInt32(reporteDatos.Rows[0][31].ToString()) != 1)
            {
                frmReportes myreport = new frmReportes(1, "CertificadoProcesoIESS", CM);
                myreport.Show();
            }
            else
            {
                frmReportes myreport = new frmReportes(1, "CertificadoIESS", CM);
                myreport.Show();
            }
            Certificado.ActualizaCertificadoIESS(Convert.ToInt16(ate_codigo), txtDireccionPac.Text, txtTelefonoPac.Text);
            this.Close();

        }
    }
}
