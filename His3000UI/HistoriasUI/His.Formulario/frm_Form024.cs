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
using Recursos;

namespace His.Formulario
{
    public partial class frm_Form024 : Form
    {
        HC_CONSENTIMIENTO_INFORMADO conInf = new HC_CONSENTIMIENTO_INFORMADO();
        HC_EXONERACION_RETIRO exRet = new HC_EXONERACION_RETIRO();
        ATENCIONES atencion = null;
        ASEGURADORAS_EMPRESAS aseguradora = null;
        PACIENTES paciente = null;
        MEDICOS medico = null;
        public Int64 CodigoAtencion;
        public string tespecialidad;
        public string ttelefono;
        public string cespecialidad = "";
        public string ctelefono;
        public string aespecialidad = "";
        public string atelefono;

        public string raespecialidad = "";
        public string ratelefono = "";
        public string racedula = "";
        public string meespecialidad = "";
        public string metelefono = "";
        public string mecedula = "";
        public string odespecialidad = "";
        public string odtelefono = "";
        public string odcedula = "";
        public string anespecialidad = "";
        public string antelefono = "";
        public string ancedula = "";

        public bool editar = false;

        Int64 ATE_CODIGO;
        Int64 CON_CODIGO;
        public frm_Form024()
        {
            InitializeComponent();
        }
        public frm_Form024(Int64 ate_codigo, string hc)
        {
            InitializeComponent();
            bloquearSeccion2();
            CargarEspCirugia();
            CargarHabitacionesQuirofano();
            HabilitarBotones(true, false, false, false, true);
            CodigoAtencion = ate_codigo;
            cargaPaciente(ate_codigo);
            cargarGrid();
            Bloquear();
        }
        private void cargarGrid()
        {
            gridSol.DataSource = NegConsentimiento.getConsentimiento(CodigoAtencion);
            gridSol.Columns["CON_CODIGO"].Visible = false;
            gridSol.Columns["ATE_CODIGO"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
        private void cargaPaciente(Int64 codAtencion)
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
                aseguradora = NegAseguradoras.recuperaAseguradoraPorAtencion(Convert.ToInt32(codAtencion));
                paciente = NegPacientes.RecuperarPacienteID(atencion.PACIENTES.PAC_CODIGO);
                lbl_aseguradora.Text = aseguradora.ASE_NOMBRE;
                if (paciente.PAC_GENERO == "F")
                    ugb_Aborto.Enabled = true;
                else
                    ugb_Aborto.Enabled = false;
                if (txt_sexo.Text == "F")
                    ugb_Aborto.Enabled = true;
                else
                    ugb_Aborto.Enabled = false;

                if (paciente != null)
                {
                    txt_pacNombre.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                                         paciente.PAC_APELLIDO_MATERNO + " " +
                                         paciente.PAC_NOMBRE1 + " " +
                                         paciente.PAC_NOMBRE2;
                    txt_pacHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                    txt_sexo.Text = paciente.PAC_GENERO;
                    txtrepresentante.Text = paciente.PAC_REFERENTE_NOMBRE.Trim();
                    txtparentesco.Text = paciente.PAC_REFERENTE_PARENTESCO.Trim();
                    txttelefono.Text = paciente.PAC_REFERENTE_TELEFONO.Trim();
                }
                else
                {
                    txt_pacHCL.Text = string.Empty;
                    txt_pacNombre.Text = string.Empty;
                    txt_sexo.Text = string.Empty;
                }
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                int codigoMedico = medicos.FirstOrDefault(m => m.EntityKey == atencion.MEDICOSReference.EntityKey).MED_CODIGO;
                if (codigoMedico != 0)
                    cargarMedico(codigoMedico);
                HABITACIONES hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == atencion.HABITACIONESReference.EntityKey);
                if (hab != null)
                    txtCama.Text = hab.hab_Numero;
                adicionales = new PACIENTES_DATOS_ADICIONALES();
                adicionales = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(paciente.PAC_CODIGO);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar los datos de la atencion, error: " + ex.Message, "error");
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
            txtSala.DataSource = NegQuirofano.listadoHAbitacionesConsentimiento();
            txtSala.DisplayMember = "HABITACION";
            txtSala.ValueMember = "CODIGO";
            txtSala.SelectedIndex = -1;
        }
        public PACIENTES_DATOS_ADICIONALES adicionales;
        private void cargarAtencion(Int64 conCodigo, Int64 ate_codigo)
        {
            if (atencion.ATE_FECHA_INGRESO != null)
            {
                dtpFechaIngreso.Value = (DateTime)atencion.ATE_FECHA_INGRESO;
            }
            else
            {
                dtpFechaIngreso.Value = Convert.ToDateTime("01-01-1999");
            }
            if (atencion.ATE_FECHA_ALTA != null)
            {
                dtpFechaAlta.Value = (DateTime)atencion.ATE_FECHA_ALTA;
            }
            else
            {
                dtpFechaAlta.Value = Convert.ToDateTime("01-01-1999");
            }
            DataTable Datos = new DataTable();
            Datos = NegConsentimiento.CargarDatos(ate_codigo, conCodigo);
            if (Datos != null)
            {
                txtServicio.Text = Datos.Rows[0][2].ToString();
                txtSala.Text = Datos.Rows[0][3].ToString();
                txtproposito1.Text = Datos.Rows[0][4].ToString();
                txtresultado1.Text = Datos.Rows[0][5].ToString();
                txtprocedimientos.Text = Datos.Rows[0][6].ToString();
                txtriegosc.Text = Datos.Rows[0][7].ToString();
                txtpropositos2.Text = Datos.Rows[0][8].ToString();
                txtresultados2.Text = Datos.Rows[0][9].ToString();
                txtquirurgica.Text = Datos.Rows[0][10].ToString();
                txtriesgoq.Text = Datos.Rows[0][11].ToString();
                txtpropositos3.Text = Datos.Rows[0][12].ToString();
                txtresultados3.Text = Datos.Rows[0][13].ToString();
                txtanestesia.Text = Datos.Rows[0][14].ToString();
                txtriegosa.Text = Datos.Rows[0][15].ToString();
                dtpFecha.Value = Convert.ToDateTime(Datos.Rows[0][16].ToString());
                dtpHora.Value = Convert.ToDateTime(Datos.Rows[0][17].ToString());
                txtProfesionalT.Text = Datos.Rows[0][18].ToString();
                tespecialidad = Datos.Rows[0][19].ToString();
                ttelefono = Datos.Rows[0][20].ToString();
                txtcodigo1.Text = Datos.Rows[0][21].ToString();
                txtcirujano.Text = Datos.Rows[0][22].ToString();
                cespecialidad = Datos.Rows[0][23].ToString();
                ctelefono = Datos.Rows[0][24].ToString();
                txtcodigo2.Text = Datos.Rows[0][25].ToString();
                txtanestesiologo.Text = Datos.Rows[0][26].ToString();
                aespecialidad = Datos.Rows[0][27].ToString();
                atelefono = Datos.Rows[0][28].ToString();
                txtcodigo3.Text = Datos.Rows[0][29].ToString();
                txtrepresentante.Text = Datos.Rows[0][30].ToString();
                txtparentesco.Text = Datos.Rows[0][31].ToString();
                txtidentificacion.Text = Datos.Rows[0][32].ToString();
                txttelefono.Text = Datos.Rows[0][33].ToString();

                HabilitarBotones(false, false, true, true, true);
                Bloquear();
            }
            else
            {
                txtrepresentante.Text = paciente.PAC_REFERENTE_NOMBRE;
                txtparentesco.Text = paciente.PAC_REFERENTE_PARENTESCO;
                txttelefono.Text = paciente.PAC_REFERENTE_TELEFONO;
                HabilitarBotones(true, false, false, false, true);
                Bloquear();
            }
            //Mario 2022.06.22
            exRet = NegConsentimiento.CargarDatosH2(CodigoAtencion);
            if (exRet != null)
            {
                txt_tpcTestigo.Text = exRet.ER_PDTELEFONO;
                txt_tpcParentesco.Text = exRet.ER_PDPARENTESCO;
                txt_tpcTelefono.Text = exRet.ER_PDTELEFONO;
                txt_tpcIdentificacion.Text = exRet.ER_PDCEDULA;
                txt_tabTestigo.Text = exRet.ER_ABTESTIGO;
                txt_tabParentesco.Text = exRet.ER_ABPARENTESCO;
                txt_tabTelefono.Text = exRet.ER_ABTELEFONO;
                txt_tabIdentificacion.Text = exRet.ER_ABCEDULA;
                txt_exoHospital.Text = exRet.ER_AHMEDICO;
                ratelefono = exRet.ER_AHTELEFONO;
                racedula = exRet.ER_AHCEDULA;
                txt_cod1.Text = exRet.ER_AHCEDULA;
                txt_tahTestigo.Text = exRet.ER_AHTTESTIGO;
                txt_tahParentesco.Text = exRet.ER_AHTPARENTESCO;
                txt_tahTelefono.Text = exRet.ER_AHTTELEFONO;
                txt_tahIdentificacion.Text = exRet.ER_AHTCEDULA;
                txt_retiroMenor.Text = exRet.ER_MEMEDICO;
                metelefono = exRet.ER_METELEFONO;
                mecedula = exRet.ER_MECEDULA;
                txt_cod2.Text = exRet.ER_MECEDULA;
                txt_tmeTestigo.Text = exRet.ER_METTESTIGO;
                txt_tmeParentesco.Text = exRet.ER_METPARENTESCO;
                txt_tmeTelefono.Text = exRet.ER_METTELEFONO;
                txt_tmeIdentificacion.Text = exRet.ER_MECEDULA;
                txt_exoDonacion.Text = exRet.ER_ODMEDICO;
                odtelefono = exRet.ER_ODTELEFONO;
                odcedula = exRet.ER_ODCEDULA;
                txt_cod3.Text = exRet.ER_ODCEDULA;
                txt_tdoTestigo.Text = exRet.ER_ODTTESTIGO;
                txt_tdoParentesco.Text = exRet.ER_ODTPARENTESCO;
                txt_tdoTelefono.Text = exRet.ER_ODTTELEFONO;
                txt_tdoIdentificacion.Text = exRet.ER_ODTCEDULA;
                txt_autorizacionNecro.Text = exRet.ER_ANMEDICO;
                antelefono = exRet.ER_ANTELEFONO;
                ancedula = exRet.ER_ANCEDULA;
                txt_cod4.Text = exRet.ER_ANCEDULA;
                txt_tanTestigo.Text = exRet.ER_ANTTESTIGO;
                txt_tanParentesco.Text = exRet.ER_ANTPARENTESCO;
                txt_tanTelefono.Text = exRet.ER_ANTTELEFONO;
                txt_tanIdentificacion.Text = exRet.ER_ANTCEDULA;
                txt_Organos.Text = exRet.ER_ORGANOS_DONADOS;
                txt_Receptor.Text = exRet.ER_NOMBRE_RECEPTOR;
                chbS1.Checked = (bool)exRet.ER_ESTA1;
                chbS2.Checked = (bool)exRet.ER_ESTA2;
                chbS3.Checked = (bool)exRet.ER_ESTA3;
                chbS4.Checked = (bool)exRet.ER_ESTA4;
                chbS5.Checked = (bool)exRet.ER_ESTA5;
                chbS6.Checked = (bool)exRet.ER_ESTA6;

            }
            else
            {
                txtrepresentante.Text = paciente.PAC_REFERENTE_NOMBRE;
                txtparentesco.Text = paciente.PAC_REFERENTE_PARENTESCO;
                txttelefono.Text = paciente.PAC_REFERENTE_TELEFONO;
            }
        }
        private void cargarMedico(int cod)
        {
            medico = NegMedicos.RecuperaMedicoId(cod);
            lbl_medico.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                medico.MED_APELLIDO_MATERNO.Trim() + " " +
                medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
        }
        private void CargarConsentimiento()
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Form024_Load(object sender, EventArgs e)
        {

        }
        public void Bloquear()
        {
            txtServicio.Enabled = false;
            txtSala.Enabled = false;
            txtrepresentante.Enabled = false;
            txtidentificacion.Enabled = false;
            txtparentesco.Enabled = false;
            txttelefono.Enabled = false;
            //tabulador.Enabled = false;
            //Mario2022.06.22
            txt_tanTestigo.Enabled = false;
            txt_tmeTestigo.Enabled = false;
            txt_tdoTestigo.Enabled = false;
            txt_tahTestigo.Enabled = false;
            dtpFecha.Enabled = false;
            dtpHora.Enabled = false;
            ugb_Hospital.Enabled = false;
            ugb_Incapacidad.Enabled = false;
            ugb_Necropsia.Enabled = false;
            ugb_Procedimiento.Enabled = false;
            ugb_Transplante.Enabled = false;
            ugb_Aborto.Enabled = false;

            txtproposito1.Enabled = false;
            txtpropositos2.Enabled = false;
            txtpropositos3.Enabled = false;
            txtresultado1.Enabled = false;
            txtresultados2.Enabled = false;
            txtresultados3.Enabled = false;
            txtprocedimientos.Enabled = false;
            txtquirurgica.Enabled = false;
            txtanestesia.Enabled = false;
            txtriegosc.Enabled = false;
            txtriegosa.Enabled = false;
            txtriesgoq.Enabled = false;
            txtProfesionalT.Enabled = false;
            txtcirujano.Enabled = false;
            txtanestesiologo.Enabled = false;
            btnAyuda1.Enabled = false;
            btnAyuda2.Enabled = false;
            btnAyuda3.Enabled = false;
        }
        public void Desbloquear()
        {
            txtServicio.Enabled = true;
            txtSala.Enabled = true;
            txtrepresentante.Enabled = true;
            txtidentificacion.Enabled = true;
            txtparentesco.Enabled = true;
            txttelefono.Enabled = true;
            //tabulador.Enabled = true;
            //Mario2022.06.22
            txt_tanTestigo.Enabled = true;
            txt_tmeTestigo.Enabled = true;
            txt_tdoTestigo.Enabled = true;
            txt_tahTestigo.Enabled = true;
            dtpFecha.Enabled = true;
            dtpHora.Enabled = true;
            ugb_Hospital.Enabled = true;
            ugb_Incapacidad.Enabled = true;
            ugb_Necropsia.Enabled = true;
            ugb_Procedimiento.Enabled = true;
            ugb_Transplante.Enabled = true;

            txtproposito1.Enabled = true;
            txtpropositos2.Enabled = true;
            txtpropositos3.Enabled = true;
            txtresultado1.Enabled = true;
            txtresultados2.Enabled = true;
            txtresultados3.Enabled = true;
            txtprocedimientos.Enabled = true;
            txtquirurgica.Enabled = true;
            txtanestesia.Enabled = true;
            txtriegosc.Enabled = true;
            txtriegosa.Enabled = true;
            txtriesgoq.Enabled = true;
            txtProfesionalT.Enabled = true;
            txtcirujano.Enabled = true;
            txtanestesiologo.Enabled = true;
            btnAyuda1.Enabled = true;
            btnAyuda2.Enabled = true;
            btnAyuda3.Enabled = true;
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool imprimir, bool actualizar, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnImprimir.Enabled = imprimir;
            btnActualizar.Enabled = actualizar;
            btnCancelar.Enabled = cancelar;
        }

        public bool Validador()
        {
            errorProvider1.Clear();
            bool valido = true;
            //if (txtproposito1.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtproposito1, "Campo Obligatorio");
            //}
            //if (txtresultado1.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtresultado1, "Campo Obligatorio");
            //}
            //if (txtprocedimientos.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtprocedimientos, "Campo Obligatorio");
            //}
            //if (txtriegosc.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtriegosc, "Campo Obligatorio");
            //}

            //if (txtProfesionalT.Text.Trim() == "" && txtcodigo1.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtProfesionalT, "Campo Obligatorio");
            //    errorProvider1.SetError(txtcodigo1, "Campo Obligatorio");
            //}

            //if (txtpropositos2.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtpropositos2, "Campo Obligatorio");
            //}
            //if (txtresultados2.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtresultados2, "Campo Obligatorio");
            //}
            //if (txtquirurgica.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtquirurgica, "Campo Obligatorio");
            //}
            //if (txtriesgoq.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtriesgoq, "Campo Obligatorio");
            //}

            //if (txtcirujano.Text.Trim() == "" && txtcodigo2.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtcirujano, "Campo Obligatorio");
            //    errorProvider1.SetError(txtcodigo2, "Campo Obligatorio");
            //}
            //if (txtpropositos3.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtpropositos3, "Campo Obligatorio");
            //}
            //if (txtresultados3.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtresultados3, "Campo Obligatorio");
            //}
            //if (txtanestesia.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtanestesia, "Campo Obligatorio");
            //}
            //if (txtriegosa.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtriegosa, "Campo Obligatorio");
            //}
            //if (txtanestesiologo.Text.Trim() == "" && txtcodigo3.Text.Trim() == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txtanestesiologo, "Campo Obligatorio");
            //    errorProvider1.SetError(txtcodigo3, "Campo Obligatorio");
            //}
            if (txtrepresentante.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtrepresentante, "Campo Obligatorio");
            }
            if (txtparentesco.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtparentesco, "Campo Obligatorio");
            }

            if (txttelefono.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txttelefono, "Campo Obligatorio");
            }
            if (txtidentificacion.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtidentificacion, "Campo Obligatorio");
            }
            if (txtServicio.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtServicio, "Campo Obligatorio");
            }
            if (txtSala.Text.Trim() == "")
            {
                valido = false;
                errorProvider1.SetError(txtSala, "Campo Obligatorio");
            }
            if (dtpFecha.Value.Date > DateTime.Now.Date)
            {
                valido = false;
                errorProvider1.SetError(dtpFecha, "La fecha no puede ser mayor a la fecha actual.");
            }
            #region Mario
            //Mario 2022.06.22
            //if (txt_exoDonacion.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_exoDonacion, "Campo Obligatorio");
            //}
            //if (txt_exoHospital.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_exoHospital, "Campo Obligatorio");
            //}
            //if (txt_retiroMenor.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_retiroMenor, "Campo Obligatorio");
            //}
            //if (txt_autorizacionNecro.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_autorizacionNecro, "Campo Obligatorio");
            //}
            //if (txt_tanTestigo.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tanTestigo, "Campo Obligatorio");
            //}
            //if (txt_tanParentesco.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tanParentesco, "Campo Obligatorio");
            //}
            //if (txt_tanIdentificacion.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tanIdentificacion, "Campo Obligatorio");
            //}
            //if (txt_tanTelefono.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tanTelefono, "Campo Obligatorio");
            //}
            //if (txt_tdoTestigo.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tdoTestigo, "Campo Obligatorio");
            //}
            //if (txt_tdoParentesco.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tdoParentesco, "Campo Obligatorio");
            //}
            //if (txt_tdoIdentificacion.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tdoIdentificacion, "Campo Obligatorio");
            //}
            //if (txt_tdoTelefono.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tdoTelefono, "Campo Obligatorio");
            //}
            //if (txt_tmeTestigo.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tmeTestigo, "Campo Obligatorio");
            //}
            //if (txt_tmeParentesco.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tmeParentesco, "Campo Obligatorio");
            //}
            //if (txt_tmeIdentificacion.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tmeIdentificacion, "Campo Obligatorio");
            //}
            //if (txt_tmeTelefono.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_tmeTelefono, "Campo Obligatorio");
            //}
            //if (txt_Receptor.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_Receptor, "Campo Obligatorio");
            //}
            //if (txt_Organos.Text == "")
            //{
            //    valido = false;
            //    errorProvider1.SetError(txt_Organos, "Campo Obligatorio");
            //}
            #endregion
            if (dtpFecha.Value < (DateTime)atencion.ATE_FECHA_INGRESO)
            {
                valido = false;
                errorProvider1.SetError(dtpHora, "La fecha no puede ser menor a la Fecha de Ingreso");
                dtpFecha.Value = DateTime.Now;
            }
            return valido;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Desbloquear();
            limpiar();
            HabilitarBotones(false, true, false, false, true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validador())
                {
                    conInf = new HC_CONSENTIMIENTO_INFORMADO();
                    conInf.ATE_CODIGO = CodigoAtencion;
                    conInf.CON_SERVICIO = txtServicio.Text.Trim();
                    conInf.CON_SALA = txtSala.Text.Trim();
                    conInf.CON_PROPOSITO1 = txtproposito1.Text.Trim();
                    conInf.CON_RESULTADO1 = txtresultado1.Text.Trim();
                    conInf.CON_PROCEDIMIENTO = txtprocedimientos.Text.Trim();
                    conInf.CON_RIESGO1 = txtriegosc.Text.Trim();
                    conInf.CON_PROPOSITO2 = txtpropositos2.Text.Trim();
                    conInf.CON_RESULTADO2 = txtresultados2.Text.Trim();
                    conInf.CON_QUIRURGICO = txtquirurgica.Text.Trim();
                    conInf.CON_RIESGO2 = txtriesgoq.Text.Trim();
                    conInf.CON_PROPOSITO3 = txtpropositos3.Text.Trim();
                    conInf.CON_RESULTADO3 = txtresultados3.Text.Trim();
                    conInf.CON_ANESTESIA = txtanestesia.Text;
                    conInf.CON_RIESGO3 = txtriegosa.Text;
                    conInf.CON_FECHA = dtpFecha.Value;
                    DateTime hora = Convert.ToDateTime(dtpHora.Text);
                    conInf.CON_HORA = hora.TimeOfDay;
                    conInf.CON_TRATANTE = txtProfesionalT.Text.Trim();
                    conInf.CON_TESPECIALIDAD = tespecialidad;
                    conInf.CON_TTELEFONO = ttelefono;
                    conInf.CON_TCODIGO = txtcodigo1.Text.Trim();
                    conInf.CON_CIRUJANO = txtcirujano.Text.Trim();
                    conInf.CON_CESPECIALIDAD = cespecialidad;
                    conInf.CON_CTELEFONO = ctelefono;
                    conInf.CON_CCODIGO = txtcodigo2.Text.Trim();
                    conInf.CON_ANESTESISTA = txtanestesiologo.Text.Trim();
                    conInf.CON_AESPECIALIDAD = aespecialidad;
                    conInf.CON_ATELEFONO = atelefono;
                    conInf.CON_ACODIGO = txtcodigo3.Text.Trim();
                    conInf.CON_REPRESENTANTE = txtrepresentante.Text.Trim();
                    conInf.CON_PARENTESCO = txtparentesco.Text.Trim();
                    conInf.CON_IDENTIFICACION = txtidentificacion.Text.Trim();
                    conInf.CON_TELEFONO = txttelefono.Text.Trim();

                    exRet = new HC_EXONERACION_RETIRO();
                    exRet.ATE_CODIGO = CodigoAtencion;
                    exRet.ER_PDTESTIGO = txt_tpcTestigo.Text.Trim();
                    exRet.ER_PDPARENTESCO = txt_tpcParentesco.Text.Trim();
                    exRet.ER_PDTELEFONO = txt_tpcTelefono.Text.Trim();
                    exRet.ER_PDCEDULA = txt_tpcIdentificacion.Text.Trim();
                    exRet.ER_ABTESTIGO = txt_tabTestigo.Text.Trim();
                    exRet.ER_ABPARENTESCO = txt_tabParentesco.Text.Trim();
                    exRet.ER_ABTELEFONO = txt_tabTelefono.Text.Trim();
                    exRet.ER_ABCEDULA = txt_tabIdentificacion.Text.Trim();
                    exRet.ER_AHMEDICO = txt_exoHospital.Text.Trim();
                    exRet.ER_AHTELEFONO = ratelefono;
                    exRet.ER_AHCEDULA = racedula;
                    exRet.ER_AHTTESTIGO = txt_tahTestigo.Text.Trim();
                    exRet.ER_AHTPARENTESCO = txt_tahParentesco.Text.Trim();
                    exRet.ER_AHTTELEFONO = txt_tahTelefono.Text.Trim();
                    exRet.ER_AHTCEDULA = txt_tahIdentificacion.Text.Trim();
                    exRet.ER_MEMEDICO = txt_retiroMenor.Text.Trim();
                    exRet.ER_METELEFONO = metelefono;
                    exRet.ER_MECEDULA = mecedula;
                    exRet.ER_METTESTIGO = txt_tmeTestigo.Text.Trim();
                    exRet.ER_METPARENTESCO = txt_tmeParentesco.Text.Trim();
                    exRet.ER_METTELEFONO = txt_tmeTelefono.Text.Trim();
                    exRet.ER_METCEDULA = txt_tmeIdentificacion.Text.Trim();
                    exRet.ER_ODMEDICO = txt_exoDonacion.Text.Trim();
                    exRet.ER_ODTELEFONO = odtelefono;
                    exRet.ER_ODCEDULA = odcedula;
                    exRet.ER_ODTTESTIGO = txt_tdoTestigo.Text.Trim();
                    exRet.ER_ODTPARENTESCO = txt_tdoParentesco.Text.Trim();
                    exRet.ER_ODTTELEFONO = txt_tdoTelefono.Text.Trim();
                    exRet.ER_ODTCEDULA = txt_tdoIdentificacion.Text.Trim();
                    exRet.ER_ANMEDICO = txt_autorizacionNecro.Text.Trim();
                    exRet.ER_ANTELEFONO = antelefono;
                    exRet.ER_ANCEDULA = ancedula;
                    exRet.ER_ANTTESTIGO = txt_tanTestigo.Text.Trim();
                    exRet.ER_ANTPARENTESCO = txt_tanParentesco.Text.Trim();
                    exRet.ER_ANTTELEFONO = txt_tanTelefono.Text.Trim();
                    exRet.ER_ANTCEDULA = txt_tanIdentificacion.Text.Trim();
                    exRet.ER_REPRESENTANTE = txtrepresentante.Text.Trim();
                    exRet.ER_PARENTESCO = txtparentesco.Text.Trim();
                    exRet.ER_IDENTIFICACION = txtidentificacion.Text.Trim();
                    exRet.ER_TELEFONO = txttelefono.Text.Trim();
                    exRet.ER_ORGANOS_DONADOS = txt_Organos.Text.Trim();
                    exRet.ER_NOMBRE_RECEPTOR = txt_Receptor.Text.Trim();
                    exRet.ER_ESTA1 = chbS1.Checked;
                    exRet.ER_ESTA2 = chbS2.Checked;
                    exRet.ER_ESTA3 = chbS3.Checked;
                    exRet.ER_ESTA4 = chbS4.Checked;
                    exRet.ER_ESTA5 = chbS5.Checked;
                    exRet.ER_ESTA6 = chbS6.Checked;

                    if (editar)
                    {
                        if (NegConsentimiento.actualizarConsentimiento(CodigoAtencion, conInf))
                        {
                            editar = false;
                            MessageBox.Show("Datos almacenados Hoja 1 correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Los datos no se pudieron almacenar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (NegConsentimiento.actualizarExoneracion(CodigoAtencion, exRet))
                        {
                            editar = false;
                            MessageBox.Show("Datos almacenados Hoja 2 correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Los datos no se pudieron almacenar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (NegConsentimiento.guardaConsentimiento(conInf))
                        {
                            MessageBox.Show("Datos almacenados Hoja 1 correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Los datos no se pudieron almacenar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        exRet.CON_CODIGO = NegConsentimiento.ultimoRegistro();
                        if (NegConsentimiento.guardaExoneracion(exRet))
                        {
                            MessageBox.Show("Datos almacenados Hoja 2 correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Los datos no se pudieron almacenar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    //NegConsentimiento.GuardarConsentimiento(CodigoAtencion, txtServicio.Text.Trim(),
                    //    txtSala.Text.Trim(), txtproposito1.Text.Trim(), txtresultado1.Text.Trim(),
                    //    txtprocedimientos.Text.Trim(), txtriegosc.Text.Trim(), txtpropositos2.Text.Trim(),
                    //    txtresultados2.Text.Trim(), txtquirurgica.Text.Trim(), txtriesgoq.Text.Trim(),
                    //    txtpropositos3.Text.Trim(), txtresultados3.Text.Trim(), txtanestesia.Text, txtriegosa.Text,
                    //    dtpFecha.Value.ToShortDateString(), dtpHora.Value.ToShortTimeString(), txtProfesionalT.Text.Trim(), tespecialidad, ttelefono,
                    //    txtcodigo1.Text.Trim(), txtcirujano.Text.Trim(), cespecialidad, ctelefono, txtcodigo2.Text.Trim(),
                    //    txtanestesiologo.Text.Trim(), aespecialidad, atelefono, txtcodigo3.Text.Trim(),
                    //    txtrepresentante.Text.Trim(), txtparentesco.Text.Trim(), txtidentificacion.Text.Trim(), txttelefono.Text.Trim());
                    //Mario 2022.06.22


                    //NegConsentimiento.GuardarConsentimientoH2(CodigoAtencion, txt_tpcTestigo.Text.Trim(), txt_tpcParentesco.Text.Trim(), txt_tpcTelefono.Text.Trim(),
                    //    txt_tpcIdentificacion.Text.Trim(), txt_tabTestigo.Text.Trim(), txt_tabParentesco.Text.Trim(), txt_tabTelefono.Text.Trim(), txt_tabIdentificacion.Text.Trim(),
                    //    txt_exoHospital.Text.Trim(), ratelefono, racedula, txt_tahTestigo.Text.Trim(), txt_tahParentesco.Text.Trim(), txt_tahTelefono.Text.Trim()
                    //    , txt_tahIdentificacion.Text.Trim(), txt_retiroMenor.Text.Trim(), metelefono, mecedula, txt_tmeTestigo.Text.Trim(),
                    //    txt_tmeParentesco.Text.Trim(), txt_tmeTelefono.Text.Trim(), txt_tmeIdentificacion.Text.Trim(), txt_exoDonacion.Text.Trim(), odtelefono, odcedula,
                    //    txt_tdoTestigo.Text.Trim(), txt_tdoParentesco.Text.Trim(), txt_tdoTelefono.Text.Trim(), txt_tdoIdentificacion.Text.Trim(), txt_autorizacionNecro.Text.Trim(),
                    //    antelefono, ancedula, txt_tanTestigo.Text.Trim(), txt_tanParentesco.Text.Trim(), txt_tanTelefono.Text.Trim(),
                    //    txt_tanIdentificacion.Text.Trim(), txtrepresentante.Text.Trim(), txtparentesco.Text.Trim(), txttelefono.Text.Trim(),
                    //    txtidentificacion.Text.Trim(),txt_Organos.Text.Trim(),txt_Receptor.Text.Trim());

                    //MessageBox.Show("Datos almacenados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarGrid();
                    HabilitarBotones(false, false, true, true, true);
                    ImprimirFormulario();
                }
            }
            catch (Exception EX)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Algo ocurrio al guardar los datos. Consulte con el Administrador.\r\nMas detalles: " + EX.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirFormulario();
        }

        public void ImprimirFormulario()
        {
            TIPO_TRATAMIENTO tratamiento = NegTipoTratamiento.recuperaTipoTratamiento(Convert.ToInt16(atencion.TIPO_TRATAMIENTOReference.EntityKey.EntityKeyValues[0].Value));

            NegCertificadoMedico medico = new NegCertificadoMedico();
            DS_Form024 form = new DS_Form024();
            DataRow dr;
            dr = form.Tables["Form024"].NewRow();
            dr["Empresa"] = His.Entidades.Clases.Sesion.nomEmpresa;
            dr["Logo"] = medico.path();
            dr["Unidad"] = tratamiento.TIA_DESCRIPCION;
            dr["Cod_UO"] = "";
            dr["Parroquia"] = adicionales.COD_PARROQUIA;
            dr["Canton"] = adicionales.COD_CANTON;
            dr["Provincia"] = adicionales.COD_PROVINCIA;
            if (!NegParametros.ParametroFormularios())
                dr["HC"] = txt_pacHCL.Text.Trim();
            else
                dr["HC"] = txtidentificacion.Text.Trim();
            dr["ApellidoP"] = paciente.PAC_APELLIDO_PATERNO;

            dr["ApellidoM"] = paciente.PAC_APELLIDO_MATERNO;
            dr["Nombres"] = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
            dr["Servicio"] = txtServicio.Text.Trim();
            dr["Sala"] = txtSala.Text.Trim();
            dr["Cama"] = txtCama.Text.Trim();
            dr["Fecha"] = dtpFecha.Text;
            dr["Hora"] = dtpHora.Text;
            dr["Propositos1"] = txtproposito1.Text.Trim();
            dr["ResultadoE1"] = txtresultado1.Text.Trim();
            dr["Terapia_Proce"] = txtprocedimientos.Text.Trim();
            dr["Riesgos1"] = txtriegosc.Text.Trim();
            dr["ProfesionalT1"] = txtProfesionalT.Text.Trim();
            dr["Especialidad1"] = tespecialidad;
            dr["Telefono1"] = ttelefono;
            dr["Codigo1"] = txtcodigo1.Text.Trim();
            dr["Propositos2"] = txtpropositos2.Text.Trim();
            dr["Resultado2"] = txtresultados2.Text.Trim();
            dr["Intervenciones"] = txtquirurgica.Text.Trim();
            dr["Riesgos2"] = txtriesgoq.Text.Trim();

            dr["ProfesionalT2"] = txtcirujano.Text.Trim();
            dr["Especialidad2"] = cespecialidad;
            dr["Codigo2"] = txtcodigo2.Text.Trim();
            dr["Propositos3"] = txtpropositos3.Text.Trim();
            dr["Resultado3"] = txtresultados3.Text.Trim();
            dr["Anestesia"] = txtanestesia.Text.Trim();
            dr["Riesgo3"] = txtriegosa.Text.Trim();
            dr["ProfesionalT3"] = txtanestesiologo.Text.Trim();
            dr["Telefono3"] = atelefono;
            dr["Telefono2"] = ctelefono;
            dr["Codigo3"] = txtcodigo3.Text.Trim();
            dr["Representante"] = txtrepresentante.Text.Trim();

            dr["Parentesco"] = txtparentesco.Text.Trim();
            dr["TelefonoR"] = txttelefono.Text.Trim();
            dr["IdentificacionR"] = txtidentificacion.Text.Trim();
            dr["Especialidad3"] = aespecialidad;
            form.Tables["Form024"].Rows.Add(dr);
            //Mario2022.06.22
            DataRow dr2;
            dr2 = form.Tables["Form024H2"].NewRow();
            if (chbS1.Checked)
            {
                dr2["pdTestigo"] = txt_tpcTestigo.Text.Trim();
                dr2["pdParentesco"] = txt_tpcParentesco.Text.Trim();
                dr2["pdTelefono"] = txt_tpcTelefono.Text.Trim();
                dr2["pdCedula"] = txt_tpcIdentificacion.Text.Trim();
                dr2["Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                dr2["Cedula"] = paciente.PAC_IDENTIFICACION;
                dr2["Telefono"] = adicionales.DAP_TELEFONO2;
            }
            if (chbS2.Checked)
            {
                dr2["abTestigo"] = txt_tabTestigo.Text.Trim();
                dr2["adParentesco"] = txt_tabParentesco.Text.Trim();
                dr2["abTelefono"] = txt_tabTelefono.Text.Trim();
                dr2["adCedula"] = txt_tabIdentificacion.Text.Trim();
                dr2["p2Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                dr2["p2Cedula"] = paciente.PAC_IDENTIFICACION;
                dr2["p2Telefono"] = adicionales.DAP_TELEFONO2;
            }
            if (chbS3.Checked)
            {
                dr2["ahMedico"] = txt_exoHospital.Text.Trim();
                dr2["ahTelefono"] = ratelefono;
                dr2["ahCedula"] = racedula;
                dr2["ahtTestigo"] = txt_tahTestigo.Text.Trim();
                dr2["ahtParentesco"] = txt_tahParentesco.Text.Trim();
                dr2["ahtTelefono"] = txt_tahTelefono.Text.Trim();
                dr2["ahtCedula"] = txt_tahIdentificacion.Text.Trim();
                dr2["p3Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                dr2["p3Cedula"] = paciente.PAC_IDENTIFICACION;
                dr2["p3Telefono"] = adicionales.DAP_TELEFONO2;
            }
            if (chbS4.Checked)
            {
                dr2["meMedico"] = txt_retiroMenor.Text.Trim();
                dr2["meTelefono"] = metelefono;
                dr2["meCedula"] = mecedula;
                dr2["metTestigo"] = txt_tmeTestigo.Text.Trim();
                dr2["metParentesco"] = txt_tmeParentesco.Text.Trim();
                dr2["metTelefono"] = txt_tmeTelefono.Text.Trim();
                dr2["metCedula"] = txt_tmeIdentificacion.Text.Trim();
                dr2["Representante3"] = txtrepresentante.Text.Trim();
                dr2["Parentesco13"] = txtparentesco.Text.Trim();
                dr2["TelefonoR3"] = txttelefono.Text.Trim();
                dr2["IdentificacionR3"] = txtidentificacion.Text.Trim();
            }
            if (chbS5.Checked)
            {
                dr2["Organos"] = txt_Organos.Text.Trim();
                dr2["Receptor"] = txt_Receptor.Text.Trim();
                dr2["odMedico"] = txt_exoDonacion.Text.Trim();
                dr2["odTelefono"] = odtelefono;
                dr2["odCedula"] = odcedula;
                dr2["odtTestigo"] = txt_tdoTestigo.Text.Trim();
                dr2["odtParentesco"] = txt_tdoParentesco.Text.Trim();
                dr2["odtTelefono"] = txt_tdoTelefono.Text.Trim();
                dr2["odtCedula"] = txt_tdoIdentificacion.Text.Trim();
                dr2["Representante1"] = txtrepresentante.Text.Trim();
                dr2["Parentesco1"] = txtparentesco.Text.Trim();
                dr2["TelefonoR1"] = txttelefono.Text.Trim();
                dr2["IdentificacionR1"] = txtidentificacion.Text.Trim();
            }
            if (chbS6.Checked)
            {
                dr2["anMedico"] = txt_autorizacionNecro.Text.Trim();
                dr2["anTelefono"] = antelefono;
                dr2["anCedula"] = ancedula;
                dr2["antTestigo"] = txt_tanTestigo.Text.Trim();
                dr2["antParentesco"] = txt_tanParentesco.Text.Trim();
                dr2["antTelefono"] = txt_tanTelefono.Text.Trim();
                dr2["antCedula"] = txt_tanIdentificacion.Text.Trim();
                dr2["Representante2"] = txtrepresentante.Text.Trim();
                dr2["Parentesco2"] = txtparentesco.Text.Trim();
                dr2["TelefonoR2"] = txttelefono.Text.Trim();
                dr2["IdentificacionR2"] = txtidentificacion.Text.Trim();
            }
            if (!NegParametros.ParametroFormularios())
                dr2["HC"] = txt_pacHCL.Text.Trim();
            else
                dr2["HC"] = txtidentificacion.Text.Trim();
            form.Tables["Form024H2"].Rows.Add(dr2);


            frmReportes x = new frmReportes(1, "Consentimiento", form);
            x.Show();
        }
        MaskedTextBox codMedico;
        MEDICOS med = null;
        private void txtanestesiologo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //List<MEDICOS> medicos = NegMedicos.listaMedicos();
                //frm_Ayudas frm = new frm_Ayudas(medicos);
                //frm.bandCampo = true;
                //frm.ShowDialog();
                //if (frm.campoPadre2.Text != string.Empty)
                //{
                //    codMedico = (frm.campoPadre2);
                //    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                //    agregarAnestesista(med);
                //}
                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listarMedicosAnesteciologos();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarAnestesista(med);
                }
            }
        }
        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtProfesionalT.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_CODIGO_MEDICO != null)
                    txtcodigo1.Text = medicoTratante.MED_RUC.Substring(0, 10);
                else
                    txtcodigo1.Text = "0"; //no tiene codigo
                tespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                ttelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
            }
        }

        private void agregarCirujano(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtcirujano.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_CODIGO_MEDICO != null)
                    txtcodigo2.Text = medicoTratante.MED_RUC.Substring(0, 10);
                else
                    txtcodigo2.Text = "0"; //no tiene codigo
                cespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                ctelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
            }
        }

        private void agregarAnestesista(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtanestesiologo.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_CODIGO_MEDICO != null)
                    txtcodigo3.Text = medicoTratante.MED_RUC.Substring(0, 10);
                else
                    txtcodigo3.Text = "0"; //no tiene codigo
                aespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                atelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
            }
        }

        private void txtProfesionalT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //List<MEDICOS> medicos = NegMedicos.listaMedicos();
                //frm_Ayudas frm = new frm_Ayudas(medicos);
                //frm.bandCampo = true;
                //frm.ShowDialog();
                //if (frm.campoPadre2.Text != string.Empty)
                //{
                //    codMedico = (frm.campoPadre2);
                //    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                //    agregarMedico(med);
                //}

                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarMedico(med);
                }
            }
        }
        private void txtcirujano_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //List<MEDICOS> medicos = NegMedicos.listaMedicos();
                //frm_Ayudas frm = new frm_Ayudas(medicos);
                //frm.bandCampo = true;
                //frm.ShowDialog();
                //if (frm.campoPadre2.Text != string.Empty)
                //{
                //    codMedico = (frm.campoPadre2);
                //    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(codMedico.Text));
                //    agregarCirujano(med);
                //}
                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarCirujano(med);
                }
            }
        }
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

        private void txttelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void ultraTabControl2_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }
        //Mario 2022.06.22
        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarMedicoRespossabilidad(med);
                }
            }
        }
        private void agregarMedicoRespossabilidad(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txt_exoHospital.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_RUC != null)
                    txt_cod1.Text = medicoTratante.MED_RUC.Substring(0, 10);
                else
                    txt_cod1.Text = "0"; //no tiene codigo
                raespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                ratelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
                racedula = medicoTratante.MED_RUC.Substring(0, 10);
            }
        }
        private void txt_autorizacionNecro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarMedicoNecro(med);
                }
            }
        }
        private void agregarMedicoNecro(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txt_autorizacionNecro.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_RUC != null)
                    txt_cod4.Text = medicoTratante.MED_RUC.Substring(0, 10);
                else
                    txt_cod4.Text = "0"; //no tiene codigo
                anespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                antelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
                ancedula = medicoTratante.MED_RUC.Substring(0, 10);
            }
        }
        private void txt_retiroMenor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarMedicoMenor(med);
                }
            }
        }
        private void agregarMedicoMenor(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txt_retiroMenor.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_RUC != null)
                    txt_cod2.Text = medicoTratante.MED_RUC.Substring(0, 10);
                else
                    txt_cod2.Text = "0"; //no tiene codigo
                meespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                metelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
                mecedula = medicoTratante.MED_RUC.Substring(0, 10);
            }
        }
        private void txt_exoDonacion_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void agregarMedicoDonacion(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txt_exoDonacion.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                if (medicoTratante.MED_RUC != null)
                    txt_cod3.Text = medicoTratante.MED_RUC.Substring(0, 10);
                else
                    txt_cod3.Text = "0"; //no tiene codigo
                odespecialidad = NegEspecialidades.Especialidad(medicoTratante.MED_CODIGO);
                odtelefono = medicoTratante.MED_TELEFONO_CONSULTORIO;
                odcedula = medicoTratante.MED_RUC.Substring(0, 10);
            }
        }

        private void txt_exoDonacion_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                List<MEDICOS> listaMedicos;

                listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                ayuda.ShowDialog();
                if (ayuda.campoPadre.Text != string.Empty)
                {
                    med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                    agregarMedicoDonacion(med);
                }
            }
        }

        private void txt_tanTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void txt_tdoTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void txt_tahTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void txt_tmeTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, true, false, true);
            Desbloquear();
            if (paciente.PAC_GENERO == "F")
                ugb_Aborto.Enabled = true;
            else
                ugb_Aborto.Enabled = false;
            if (txt_sexo.Text == "F")
                ugb_Aborto.Enabled = true;
            else
                ugb_Aborto.Enabled = false;
            editar = true;
        }

        private void txt_tahTestigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tahParentesco.Focus();
            }
        }

        private void txt_tahParentesco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tahIdentificacion.Focus();
            }
        }

        private void txt_tahIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tahTelefono.Focus();
            }
        }

        private void txt_tahTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_exoHospital.Focus();
            }
        }

        private void txt_tmeTestigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tmeParentesco.Focus();
            }
        }

        private void txt_tmeParentesco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tmeIdentificacion.Focus();
            }
        }

        private void txt_tmeIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tmeTelefono.Focus();
            }
        }

        private void txt_tmeTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_retiroMenor.Focus();
            }
        }

        private void txt_Organos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_Receptor.Focus();
            }
        }

        private void txt_Receptor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tdoTestigo.Focus();
            }
        }

        private void txt_tdoTestigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tdoParentesco.Focus();
            }
        }

        private void txt_tdoParentesco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tdoIdentificacion.Focus();
            }
        }

        private void txt_tdoIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tdoTelefono.Focus();
            }
        }

        private void txt_tdoTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_exoDonacion.Focus();
            }
        }

        private void txt_tanTestigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tanParentesco.Focus();
            }
        }

        private void txt_tanParentesco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tanIdentificacion.Focus();
            }
        }

        private void txt_tanIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tanTelefono.Focus();
            }
        }

        private void txt_tanTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_autorizacionNecro.Focus();
            }
        }

        private void dtpFecha_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dtpFecha.Value < atencion.ATE_FECHA_INGRESO)
            {
                MessageBox.Show("La fecha no puede ser menor a la Fecha de ingreso", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpFecha.Value = DateTime.Now;
                return;
            }
        }

        private void txtServicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtSala.Focus();
            }
        }

        private void txtSala_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtrepresentante.Focus();
            }

        }

        private void txtCama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtproposito1.Focus();
            }
        }

        private void txtproposito1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtresultado1.Focus();
            }
        }

        private void txtresultado1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtprocedimientos.Focus();
            }
        }

        private void txtprocedimientos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtriegosc.Focus();
            }
        }

        private void txtriegosc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtProfesionalT.Focus();
            }
        }

        private void txtpropositos2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtresultados2.Focus();
            }
        }

        private void txtresultados2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtquirurgica.Focus();
            }
        }

        private void txtquirurgica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtriesgoq.Focus();
            }
        }

        private void txtriesgoq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtcirujano.Focus();
            }
        }

        private void txtpropositos3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtresultados3.Focus();
            }
        }

        private void txtresultados3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtanestesia.Focus();
            }
        }

        private void txtanestesia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtriegosa.Focus();
            }
        }

        private void txtriegosa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtanestesiologo.Focus();
            }
        }

        private void btnAyuda1_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
            {
                med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                agregarMedico(med);
            }
        }

        private void btnAyuda2_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
            {
                med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                agregarCirujano(med);
            }
        }

        private void btnAyuda3_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listarMedicosAnesteciologos();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
            {
                med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                agregarAnestesista(med);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
            {
                med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                agregarMedicoDonacion(med);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
            {
                med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                agregarMedicoNecro(med);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
            {
                med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                agregarMedicoRespossabilidad(med);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
            {
                med = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                agregarMedicoMenor(med);
            }
        }

        private void dtpFecha_Leave(object sender, EventArgs e)
        {
            if (dtpFecha.Value < atencion.ATE_FECHA_INGRESO)
            {
                MessageBox.Show("La fecha no puede ser menor a la Fecha de ingreso", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpFecha.Value = DateTime.Now;
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txt_tpcTestigo.Text = txtrepresentante.Text;
                txt_tpcParentesco.Text = txtparentesco.Text;
                txt_tpcTelefono.Text = txttelefono.Text;
                txt_tpcIdentificacion.Text = txtidentificacion.Text;
            }
            else
            {
                txt_tpcTestigo.Text = "";
                txt_tpcParentesco.Text = "";
                txt_tpcTelefono.Text = "";
                txt_tpcIdentificacion.Text = "";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                txt_tabTestigo.Text = txtrepresentante.Text;
                txt_tabParentesco.Text = txtparentesco.Text;
                txt_tabTelefono.Text = txttelefono.Text;
                txt_tabIdentificacion.Text = txtidentificacion.Text;
            }
            else
            {
                txt_tabTestigo.Text = "";
                txt_tabParentesco.Text = "";
                txt_tabTelefono.Text = "";
                txt_tabIdentificacion.Text = "";
            }
        }

        private void chbS1_CheckedChanged(object sender, EventArgs e)
        {
            if (chbS1.Checked)
                grbA1.Enabled = true;
            else
                grbA1.Enabled = false;
        }

        private void chbS2_CheckedChanged(object sender, EventArgs e)
        {
            if (chbS2.Checked)
                grbA2.Enabled = true;
            else
                grbA2.Enabled = false;
        }

        private void chbS3_CheckedChanged(object sender, EventArgs e)
        {
            if (chbS3.Checked)
                grbA3.Enabled = true;
            else
                grbA3.Enabled = false;
        }

        private void chbS4_CheckedChanged(object sender, EventArgs e)
        {
            if (chbS4.Checked)
                grbA4.Enabled = true;
            else
                grbA4.Enabled = false;
        }

        private void chbS5_CheckedChanged(object sender, EventArgs e)
        {
            if (chbS5.Checked)
                grbA5.Enabled = true;
            else
                grbA5.Enabled = false;
        }

        private void chbS6_CheckedChanged(object sender, EventArgs e)
        {
            if (chbS6.Checked)
                grbA6.Enabled = true;
            else
                grbA6.Enabled = false;
        }
        public void bloquearSeccion2()
        {
            grbA1.Enabled = false;
            grbA2.Enabled = false;
            grbA3.Enabled = false;
            grbA4.Enabled = false;
            grbA5.Enabled = false;
            grbA6.Enabled = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                txt_tdoTestigo.Text = txtrepresentante.Text;
                txt_tdoParentesco.Text = txtparentesco.Text;
                txt_tdoTelefono.Text = txttelefono.Text;
                txt_tdoIdentificacion.Text = txtidentificacion.Text;
            }
            else
            {
                txt_tdoTestigo.Text = "";
                txt_tdoParentesco.Text = "";
                txt_tdoTelefono.Text = "";
                txt_tdoIdentificacion.Text = "";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                txt_tanTestigo.Text = txtrepresentante.Text;
                txt_tanParentesco.Text = txtparentesco.Text;
                txt_tanTelefono.Text = txttelefono.Text;
                txt_tanIdentificacion.Text = txtidentificacion.Text;
            }
            else
            {
                txt_tanTestigo.Text = "";
                txt_tanParentesco.Text = "";
                txt_tanTelefono.Text = "";
                txt_tanIdentificacion.Text = "";
            }
        }

        private void gridSol_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            DialogResult dialogResult = MessageBox.Show("¿Desea cargar el consentimiento?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                CON_CODIGO = Convert.ToInt64(gridSol.Rows[gridSol.CurrentRow.Index].Cells["CON_CODIGO"].Value.ToString());
                ATE_CODIGO = Convert.ToInt64(gridSol.Rows[gridSol.CurrentRow.Index].Cells["ATE_CODIGO"].Value.ToString());
                limpiar();
                cargarAtencion(CON_CODIGO, ATE_CODIGO);
            }
        }
        public void limpiar()
        {
            txtServicio.Text = "";
            txtSala.Text = "";
            txtproposito1.Text = "";
            txtresultado1.Text = "";
            txtprocedimientos.Text = "";
            txtriegosc.Text = "";
            txtpropositos2.Text = "";
            txtresultados2.Text = "";
            txtquirurgica.Text = "";
            txtriesgoq.Text = "";
            txtpropositos3.Text = "";
            txtresultados3.Text = "";
            txtanestesia.Text = "";
            txtriegosa.Text = "";
            dtpFecha.Value = DateTime.Now;
            dtpHora.Value = DateTime.Now;
            txtProfesionalT.Text = "";
            tespecialidad = "";
            ttelefono = "";
            txtcodigo1.Text = "";
            txtcirujano.Text = "";
            cespecialidad = "";
            ctelefono = "";
            txtcodigo2.Text = "";
            txtanestesiologo.Text = "";
            aespecialidad = "";
            atelefono = "";
            txtcodigo3.Text = "";
            //txtrepresentante.Text = "";
            //txtparentesco.Text = "";
            //txtidentificacion.Text = "";
            //txttelefono.Text = "";

            txt_tpcTestigo.Text = "";
            txt_tpcParentesco.Text = "";
            txt_tpcTelefono.Text = "";
            txt_tpcIdentificacion.Text = "";
            txt_tabTestigo.Text = "";
            txt_tabParentesco.Text = "";
            txt_tabTelefono.Text = "";
            txt_tabIdentificacion.Text = "";
            txt_exoHospital.Text = "";
            ratelefono = "";
            racedula = "";
            txt_tahTestigo.Text = "";
            txt_tahParentesco.Text = "";
            txt_tahTelefono.Text = "";
            txt_tahIdentificacion.Text = "";
            txt_retiroMenor.Text = "";
            metelefono = "";
            mecedula = "";
            txt_tmeTestigo.Text = "";
            txt_tmeParentesco.Text = "";
            txt_tmeTelefono.Text = "";
            txt_tmeIdentificacion.Text = "";
            txt_exoDonacion.Text = "";
            odtelefono = "";
            odcedula = "";
            txt_tdoTestigo.Text = "";
            txt_tdoParentesco.Text = "";
            txt_tdoTelefono.Text = "";
            txt_tdoIdentificacion.Text = "";
            txt_autorizacionNecro.Text = "";
            antelefono = "";
            ancedula = "";
            txt_tanTestigo.Text = "";
            txt_tanParentesco.Text = "";
            txt_tanTelefono.Text = "";
            txt_tanIdentificacion.Text = "";
            txt_Organos.Text = "";
            txt_Receptor.Text = "";
            chbS1.Checked = false;
            chbS2.Checked = false;
            chbS3.Checked = false;
            chbS4.Checked = false;
            chbS5.Checked = false;
            chbS6.Checked = false;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            limpiar();
            Bloquear();
            bloquearSeccion2();
            HabilitarBotones(true, false, false, false, true);
        }

        private void txtproposito1_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500; // Define el límite de caracteres permitidos.

            // Verifica si la longitud del texto en el TextBox supera el límite.
            if (txtproposito1.Text.Length > limiteCaracteres)
            {
                // Muestra una alerta o un mensaje.
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");

                // Puedes tomar otras acciones, como truncar o limitar el texto.
                txtproposito1.Text = txtproposito1.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtresultado1_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500; 
            if (txtresultado1.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtresultado1.Text = txtresultado1.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtprocedimientos_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtprocedimientos.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtprocedimientos.Text = txtprocedimientos.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtriegosc_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtriegosc.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtriegosc.Text = txtriegosc.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtpropositos2_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtpropositos2.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtpropositos2.Text = txtpropositos2.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtresultados2_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtresultados2.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtresultados2.Text = txtresultados2.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtquirurgica_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtquirurgica.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtquirurgica.Text = txtquirurgica.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtriesgoq_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtriesgoq.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtriesgoq.Text = txtriesgoq.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtpropositos3_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtpropositos3.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtpropositos3.Text = txtpropositos3.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtresultados3_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtresultados3.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtresultados3.Text = txtresultados3.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtanestesia_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtanestesia.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtanestesia.Text = txtanestesia.Text.Substring(0, limiteCaracteres);
            }
        }

        private void txtriegosa_TextChanged(object sender, EventArgs e)
        {
            int limiteCaracteres = 500;
            if (txtriegosa.Text.Length > limiteCaracteres)
            {
                MessageBox.Show("Has alcanzado el límite de caracteres (500).");
                txtriegosa.Text = txtriegosa.Text.Substring(0, limiteCaracteres);
            }
        }
    }

}
