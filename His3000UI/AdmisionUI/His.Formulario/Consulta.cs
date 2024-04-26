using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades.Clases;
using His.Entidades;
using His.Formulario;
using System.Text.RegularExpressions;

namespace His.ConsultaExterna
{
    public partial class Consulta : Form
    {
        bool band = true;
        public bool nuevaConsulta = false;
        DtoForm002 datos = new DtoForm002();
        public Int64 id_form002 = 0;
        ATENCIONES_SUBSECUENTES subsecuente = new ATENCIONES_SUBSECUENTES();
        MEDICOS medico = new MEDICOS();
        bool emergecia = false;
        Form002MSP conExter = null;
        public Consulta()
        {
            InitializeComponent();
            inicializarGridPrescripciones();
            txt_horaAltaEmerencia.Text = DateTime.Now.ToString("hh:mm");
            txt_profesionalEmergencia.Text = Sesion.nomUsuario;
        }
        public Consulta(string ateCodigo)
        {
            InitializeComponent();
            inicializarGridPrescripciones();
            txt_horaAltaEmerencia.Text = DateTime.Now.ToString("hh:mm");
            txt_profesionalEmergencia.Text = Sesion.nomUsuario;
            if (ateCodigo != "")
            {
                txtatecodigo.Text = ateCodigo;
                CargaConsulta();
            }
        }
        public void CargaConsulta()
        {
            DataTable paciente = new DataTable();
            paciente = NegConsultaExterna.RecuperaPaciente(Convert.ToInt64(txtatecodigo.Text));
            SIGNOSVITALES_CONSULTAEXTERNA signos = NegConsultaExterna.signoscitalesCex(Convert.ToInt64(txtatecodigo.Text));

            consultaExterna = NegConsultaExterna.PacienteExisteCxE(txtatecodigo.Text.Trim());
            if (consultaExterna != null)
                CargarPacienteExiste();
            else
            {
                HabilitarBotones(true, false, false, false, false, false, false, false, false, false, false);
                nuevaConsulta = true;
                //P_Central.Enabled = true;
                grupos(true);
            }
            if (signos != null)
            {
                txtTemperatura.Text = Convert.ToString(signos.T_Bucal);
                txtTempAx.Text = Convert.ToString(signos.T_Axilar);
                txtPresionArteria1.Text = Convert.ToString(signos.Presion1);
                txtPresionArteria2.Text = Convert.ToString(signos.Presion2);
                txtPulso.Text = Convert.ToString(signos.F_Cardiaca);
                txtFrecuenciaRespiratoria.Text = Convert.ToString(signos.F_Respiratoria);
                cmb_Ocular.Text = Convert.ToString(signos.Ocular);
                cmb_Verbal.Text = Convert.ToString(signos.Verbal);
                cmb_Motora.Text = Convert.ToString(signos.Motora);
                txtGlasgow.Text = Convert.ToString(signos.Glasgow);
                txtGlucosa.Text = Convert.ToString(signos.Glucosa_Capilar);
                txtPeso.Text = Convert.ToString(signos.PesoKG);
                txtTalla.Text = Convert.ToString(signos.TallaM);
                txtIndice.Text = Convert.ToString(signos.Ind_Masa);
                txtPerimetro.Text = Convert.ToString(signos.PerimetroA);
                txtHemoglobina.Text = Convert.ToString(signos.Hemoglobina);
                txtPulsioximetria.Text = Convert.ToString(signos.S_Oxigeno);
            }
            #region codigo anterior
            //if (signos.Rows.Count > 0)
            //{
            //    string enteros = signos.Rows[0][3].ToString();
            //    string[] final = enteros.Split('.');
            //    txtPresionArteria1.Text = final[0];
            //    enteros = signos.Rows[0][4].ToString();
            //    final = enteros.Split('.');
            //    txtPresionArteria2.Text = final[0];
            //    //enteros = signos.Rows[0][5].ToString();
            //    //final = enteros.Split('.');
            //    txtPulso.Text = signos.Rows[0][5].ToString();
            //    //enteros = signos.Rows[0][6].ToString();
            //    //final = enteros.Split('.');
            //    txtFrecuenciaRespiratoria.Text = signos.Rows[0][6].ToString();
            //    //enteros = signos.Rows[0][7].ToString();
            //    //final = enteros.Split('.');
            //    txtTemperatura.Text = signos.Rows[0][7].ToString();
            //    final = enteros.Split('.');
            //    txtTemperatura.Text = signos.Rows[0][8].ToString();
            //    //enteros = signos.Rows[0][10].ToString();
            //    //final = enteros.Split('.');
            //    txtPeso.Text = signos.Rows[0][10].ToString();
            //    enteros = signos.Rows[0][11].ToString();
            //    final = enteros.Split('.');
            //    txtTalla.Text = signos.Rows[0][11].ToString();
            //}
            #endregion

            lblHistoria.Text = txthistoria.Text;
            lblAteCodigo.Text = txtatecodigo.Text;
            lblNombre.Text = paciente.Rows[0][0].ToString();
            lblApellido.Text = paciente.Rows[0][1].ToString();
            lblEdad.Text = paciente.Rows[0][2].ToString();

            if (paciente.Rows[0][3].ToString().Trim() == "M")
            {
                lblSexo.Text = "Masculino";
            }
            else
            {
                lblSexo.Text = "Femenino";
            }
            //CargarPaciente(); //aqui se puede ver las n consultas externas del paciente
            P_Central.Visible = true;
            P_Datos.Visible = true;
            //btnBuscarPaciente.Visible = false;
            //btnGuarda.Visible = true;
            //btnNuevo.Visible = true;
        }
        #region FUNCIONES Y OBJETOS


        private void inicializarGridPrescripciones()
        {
            gridPrescripciones.EditMode = DataGridViewEditMode.EditOnKeystroke;
            PRES_FARMACOTERAPIA_INDICACIONES.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_FARMACOS_INSUMOS.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_FECHA.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_ESTADO.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_CODIGO.Visible = false;
            ID_USUARIO.Visible = false;
            NOM_USUARIO.Visible = false;
            PRES_ESTADO.Width = 20;
            PRES_FECHA.Width = 130;
            PRES_FARMACOTERAPIA_INDICACIONES.Width = 350;
            PRES_FARMACOS_INSUMOS.Width = 200;
        }


        Boolean permitir = true;
        public bool txtKeyPress(TextBox textbox, int code)
        {

            bool resultado;

            if (code == 46 && textbox.Text.Contains("."))//se evalua si es punto y si es punto se revisa si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }
        public void ValidaError(Control control, string campo)
        {
            error.SetError(control, campo);
        }

        public bool Valida()
        {

            if (txtTemperatura.Text == "")
            {
                ValidaError(txtTemperatura, "" +
                    "INGRESE");
                return false;
            }
            if (txtTempAx.Text == "")
            {
                ValidaError(txtTempAx, "" +
                    "INGRESE");
                return false;
            }
            if (txtPresionArteria1.Text == "")
            {
                ValidaError(txtPresionArteria1, "" +
                    "INGRESE");
                return false;
            }
            if (txtPresionArteria2.Text == "")
            {
                ValidaError(txtPresionArteria2, "" +
                    "INGRESE");
                return false;
            }
            if (txtPulso.Text == "")
            {
                ValidaError(txtPulso, "" +
                    "INGRESE");
                return false;
            }
            if (txtFrecuenciaRespiratoria.Text == "")
            {
                ValidaError(txtFrecuenciaRespiratoria, "" +
                    "INGRESE");
                return false;
            }
            if (cmb_Ocular.Text == "")
            {
                ValidaError(cmb_Ocular, "" +
                    "INGRESE");
                return false;
            }
            if (cmb_Verbal.Text == "")
            {
                ValidaError(cmb_Verbal, "" +
                    "INGRESE");
                return false;
            }
            if (cmb_Motora.Text == "")
            {
                ValidaError(cmb_Motora, "" +
                    "INGRESE");
                return false;
            }
            if (txtPeso.Text == "")
            {
                ValidaError(txtPeso, "" +
                    "INGRESE");
                return false;
            }
            if (txtTalla.Text == "")
            {
                ValidaError(txtTalla, "" +
                    "INGRESE");
                return false;
            }
            if (txtPerimetro.Text == "")
            {
                txtPerimetro.Text = "0";
            }
            if (txtHemoglobina.Text == "")
            {
                txtHemoglobina.Text = "0";
            }
            if (txtGlucosa.Text == "")
            {
                txtGlucosa.Text = "0";
            }
            if (txtPulsioximetria.Text == "")
            {
                ValidaError(txtPulsioximetria, "" +
                    "INGRESE");
                return false;
            }

            if (txt_profesionalEmergencia.Visible == true)
            {
                if (txt_profesionalEmergencia.Text == "")
                {
                    ValidaError(txt_profesionalEmergencia, "INGRESE UN PROFECIONAL");
                    return false;
                }
            }
            if (txtDiagnostico1.Text == "")
            {
                ValidaError(txtDiagnostico1, "INGRESE CIE-10 PARA GUARDAR");
                return false;
            }
            if (txtMotivo.Text == "")
            {
                ValidaError(txtMotivo, "INGRESE MOTIVO DE CONSULTA");
                return false;
            }
            error.Clear();
            if (!chbCardiopatiaP.Checked && !chbMetabolicoP.Checked && !chbVascularP.Checked && !chbHipertensionP.Checked && !chbCancerP.Checked && !chbTuberculosisP.Checked && !chbMentalP.Checked && !chbInfecciosa.Checked && !chbMalFormado.Checked && !chbOtroP.Checked)
            {
                ValidaError(ultraGroupBox5, "DEBE SELECCIONAR POR LO MENOS UNO");
                return false;
            }
            if (txtAntecedentesPersonales.Text == "")
            {
                ValidaError(txtAntecedentesPersonales, "INGRESE ANTECEDENTES PERSONALES");
                return false;
            }
            error.Clear();
            if (!chbCardiopatia.Checked && !chbEndocrino.Checked && !chbVascular.Checked && !chbHiperT.Checked && !chbCancer.Checked && !chbTuberculosis.Checked && !chbMental.Checked && !chbInfeccionsa.Checked && !chbMalFormado.Checked && !chbOtro.Checked)
            {
                ValidaError(ultraGroupBox6, "DEBE SELECCIONAR POR LO MENOS UNO");
                return false;
            }
            if (txtAntecedentesFamiliares.Text == "")
            {
                ValidaError(txtAntecedentesFamiliares, "INGRESE ANTECEDENTES FAMILIARES");
                return false;
            }
            error.Clear();
            if (txtEnfermedadProblema.Text == "")
            {
                ValidaError(txtEnfermedadProblema, "INGRESE ENFERMEDAD O PROBLEMA ACTUAL");
                return false;
            }
            error.Clear();
            if (txtRevisionActual.Text == "")
            {
                ValidaError(txtRevisionActual, "INGRESE REVISIÓN ACTUAL DE ÓRGANOS Y SISTEMAS");
                return false;
            }
            error.Clear();
            //if (txtTemperatura.Text == "")
            //{
            //    ValidaError(txtTemperatura, "INGRESE TEMPERATURA");
            //    return false;
            //}
            //error.Clear();
            if (txtPresionArteria1.Text == "")
            {
                ValidaError(txtPresionArteria1, "INGRESE PRESIÓN ARTERIAL");
                return false;
            }
            error.Clear();
            if (txtPresionArteria2.Text == "")
            {
                ValidaError(txtPresionArteria2, "INGRESE PRESIÓN ARTERIAL");
                return false;
            }
            error.Clear();
            //if (txtPeso.Text == "")
            //{
            //    ValidaError(txtPeso, "INGRESE PESO");
            //    return false;
            //}
            //error.Clear();
            if (txtFrecuenciaRespiratoria.Text == "")
            {
                ValidaError(txtFrecuenciaRespiratoria, "INGRESE FRECUENCIA RESPIRATORIA");
                return false;
            }
            error.Clear();
            //if (txtFrecuenciaCardiaca.Text == "")
            //{
            //    ValidaError(txtFrecuenciaCardiaca, "INGRESE FRECUENCIA CARDIACA");
            //    return false;
            //}
            //error.Clear();
            //if (txtSaturaOxigeno.Text == "")
            //{
            //    ValidaError(txtSaturaOxigeno, "INGRESE SATURTACION DE OXIGENO");
            //    return false;
            //}
            //error.Clear();
            //if (txtPulso.Text == "")
            //{
            //    ValidaError(txtPulso, "INGRESE PULSO");
            //    return false;
            //}
            //error.Clear();
            //if (txtTalla.Text == "")
            //{
            //    ValidaError(txtTalla, "INGRESE TALLA");
            //    return false;
            //}
            //error.Clear();
            if (txtExamenFisico.Text == "")
            {
                ValidaError(txtExamenFisico, "INGRESE EXAMEN FÍSICO REGIONAL");
                return false;
            }
            error.Clear();
            if (txtPlanesTratamiento.Text == "")
            {
                ValidaError(txtPlanesTratamiento, "INGRESE PLANES DE TRATAMIENTO");
                return false;
            }
            error.Clear();
            //if (txtEvolucion.Text == "")
            //{
            //    ValidaError(txtEvolucion, "INGRESE EVOLUCIÓN");
            //    return false;
            //}
            error.Clear();
            if (txtDiagnostico1.Text != "" && txtCieDiagnostico1.Text == "")
            {
                ValidaError(txtCieDiagnostico1, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            if (txtDiagnostico2.Text != "" && txtCieDiagnostico2.Text == "")
            {
                ValidaError(txtCieDiagnostico2, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            if (txtDiagnostico3.Text != "" && txtCieDiagnostico3.Text == "")
            {
                ValidaError(txtCieDiagnostico3, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            if (txtDiagnostico4.Text != "" && txtCieDiagnostico4.Text == "")
            {
                ValidaError(txtCieDiagnostico4, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            if (txtDiagnostico5.Text != "" && txtCieDiagnostico5.Text == "")
            {
                ValidaError(txtCieDiagnostico5, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            if (txtDiagnostico6.Text != "" && txtCieDiagnostico6.Text == "")
            {
                ValidaError(txtCieDiagnostico6, "INGRESE DIAGNOSTICO CIE10");
                return false;
            }
            error.Clear();
            return true;
        }

        //public void limpiaCampos()
        //{

        //    //lblNombre.Text = "";
        //    //lblApellido.Text = "";
        //    //lblSexo.Text = "";

        //    //lblEdad.Text = "";
        //    //lblHistoria.Text = "";
        //    //lblAteCodigo.Text = "";


        //    txtMotivo.Text = "";
        //    txtAntecedentesPersonales.Text = "";

        //    txtAntecedentesFamiliares.Text = "";
        //    txtEnfermedadProblema.Text = "";

        //    txtRevisionActual.Text = "";
        //    dtpMedicion.Text = "";
        //    txtTemperatura.Text = "";
        //    txtPresionArteria1.Text = "";
        //    txtPresionArteria2.Text = "";
        //    txtPulso.Text = "";
        //    txtFrecuenciaRespiratoria.Text = "";
        //    txtPeso.Text = "";
        //    txtTalla.Text = "";

        //    txtExamenFisico.Text = "";
        //    txtDiagnostico1.Text = "";
        //    txtCieDiagnostico1.Text = "";

        //    txtCieDiagnostico2.Text = "";
        //    txtCieDiagnostico2.Text = "";
        //    if (chb5cp1.Checked)
        //        chb5cp1.Checked = false;
        //    if (chb5cp10.Checked)
        //        chb5cp10.Checked = false;
        //    if (chb5cp2.Checked)
        //        chb5cp2.Checked = false;
        //    if (chb5cp3.Checked)
        //        chb5cp3.Enabled = false;
        //    if (chb5cp4.Checked)
        //        chb5cp4.Checked = false;
        //    if (chb5cp5.Checked)
        //        chb5cp5.Checked = false;
        //    if (chb5cp6.Checked)
        //        chb5cp6.Checked = false;
        //    if (chb5cp7.Checked)
        //        chb5cp7.Checked = false;
        //    if (chb5cp8.Checked)
        //        chb5cp8.Checked = false;
        //    if (chb5cp9.Checked)
        //        chb5cp9.Checked = false;
        //    if (chbCardiopatia.Checked)
        //        chbCardiopatia.Checked = false;
        //    if (chbDiabetes.Checked)
        //        chbDiabetes.Checked = false;
        //    if (chbVascular.Checked)
        //        chbVascular.Checked = false;
        //    if (chbHiperT.Checked)
        //        chbHiperT.Checked = false;
        //    if (chbCancer.Checked)
        //        chbCancer.Checked = false;
        //    if (chbTuberculosis.Checked)
        //        chbTuberculosis.Checked = false;
        //    if (chbMental.Checked)
        //        chbMental.Checked = false;
        //    if (chbInfeccionsa.Checked)
        //        chbInfeccionsa.Checked = false;
        //    if (chbMalFormado.Checked)
        //        chbMalFormado.Checked = false;
        //    if (chbOtro.Checked)
        //        chbOtro.Checked = false;
        //    txtDiagnostico3.Text = "";
        //    txtCieDiagnostico3.Text = "";

        //    txtDiagnostico4.Text = "";
        //    txtCieDiagnostico4.Text = "";

        //    txtPlanesTratamiento.Text = "";
        //    //txt_profesionalEmergencia.Text = "";
        //    txt_CodMSPE.Text = "";
        //    txtEvolucion.Text = "";
        //    txtindicaciones.Text = "";

        //}


        public bool GuardaFormulario()
        {
            SIGNOSVITALES_CONSULTAEXTERNA svcex = new SIGNOSVITALES_CONSULTAEXTERNA();
            Form002MSP externa = new Form002MSP();
            externa.MED_CODIGO = Convert.ToString(medico.MED_CODIGO);
            externa.Nombre = lblNombre.Text;
            externa.Apellido = lblApellido.Text;
            externa.Sexo = lblSexo.Text;
            externa.Edad = lblEdad.Text;
            externa.Historia = lblHistoria.Text;
            externa.AteCodigo = lblAteCodigo.Text;
            externa.Motivo = txtMotivo.Text;

            if (chbCardiopatiaP.Checked)
                externa.cardiopatiaP = "X";
            else
                externa.cardiopatiaP = "";
            if (chbMetabolicoP.Checked)
                externa.endocrinoP = "X";
            else
                externa.endocrinoP = "";
            if (chbVascularP.Checked)
                externa.vascularP = "X";
            else
                externa.vascularP = "";
            if (chbHipertensionP.Checked)
                externa.hipertencionP = "X";
            else
                externa.hipertencionP = "";
            if (chbCancerP.Checked)
                externa.cancerP = "X";
            else
                externa.cancerP = "";
            if (chbTuberculosisP.Checked)
                externa.tuberculosisP = "X";
            else
                externa.tuberculosisP = "";
            if (chbMentalP.Checked)
                externa.mentalP = "X";
            else
                externa.mentalP = "";
            if (chbInfecciosa.Checked)
                externa.infecciosaP = "X";
            else
                externa.infecciosaP = "";
            if (chbFormacionP.Checked)
                externa.malformacionP = "X";
            else
                externa.malformacionP = "";
            if (chbOtroP.Checked)
                externa.otroP = "X";
            else
                externa.otroP = "";

            externa.AntecedentesPersonales = txtAntecedentesPersonales.Text;

            if (chbCardiopatia.Checked)
                externa.Cardiopatia = "X";
            else
                externa.Cardiopatia = "";
            if (chbMetabolico.Checked)
                externa.endocrinoF = "X";
            else
                externa.endocrinoF = "";
            if (chbVascular.Checked)
                externa.Vascular = "X";
            else
                externa.Vascular = "";
            if (chbHiperT.Checked)
                externa.Hipertencion = "X";
            else
                externa.Hipertencion = "";
            if (chbCancer.Checked)
                externa.Cancer = "X";
            else
                externa.Cancer = "";
            if (chbTuberculosis.Checked)
                externa.tuberculosis = "X";
            else
                externa.tuberculosis = "";
            if (chbMental.Checked)
                externa.mental = "X";
            else
                externa.mental = "";
            if (chbInfeccionsa.Checked)
                externa.infecciosa = "X";
            else
                externa.infecciosa = "";
            if (chbMalFormado.Checked)
                externa.malformacion = "X";
            else
                externa.malformacion = "";
            if (chbOtro.Checked)
                externa.otro = "X";
            else
                externa.otro = "";
            externa.antecedentesFamiliares = txtAntecedentesFamiliares.Text;
            externa.enfermedadActual = txtEnfermedadProblema.Text;
            if (chb5cp1.Checked)
            {
                externa.sentidos = "X";
                externa.sentidossp = "";
            }
            else
            {
                externa.sentidos = "";
                externa.sentidossp = "X";
            }
            if (chb5cp2.Checked)
            {
                externa.respiratorio = "X";
                externa.respiratoriosp = "";
            }
            else
            {
                externa.respiratorio = "";
                externa.respiratoriosp = "X";
            }
            if (chb5cp3.Checked)
            {
                externa.cardioVascular = "X";
                externa.cardioVascularsp = "";
            }
            else
            {
                externa.cardioVascular = "";
                externa.cardioVascularsp = "X";
            }
            if (chb5cp4.Checked)
            {
                externa.digestivo = "X";
                externa.digestivosp = "";
            }
            else
            {
                externa.digestivo = "";
                externa.digestivosp = "X";
            }
            if (chb5cp5.Checked)
            {
                externa.genital = "X";
                externa.genitalsp = "";
            }
            else
            {
                externa.genital = "";
                externa.genitalsp = "X";
            }
            if (chb5cp6.Checked)
            {
                externa.urinario = "X";
                externa.urinariosp = "";
            }
            else
            {
                externa.urinario = "";
                externa.urinariosp = "X";
            }
            if (chb5cp7.Checked)
            {
                externa.esqueletico = "X";
                externa.esqueleticosp = "";
            }
            else
            {
                externa.esqueletico = "";
                externa.esqueleticosp = "X";
            }
            if (chb5cp8.Checked)
            {
                externa.endocrino = "X";
                externa.endocrinosp = "";
            }
            else
            {
                externa.endocrino = "";
                externa.endocrinosp = "X";
            }
            if (chb5cp9.Checked)
            {
                externa.linfatico = "X";
                externa.linfaticosp = "";
            }
            else
            {
                externa.linfatico = "";
                externa.linfaticosp = "X";
            }
            if (chb5cp10.Checked)
            {
                externa.nervioso = "X";
                externa.nerviososp = "";
            }
            else
            {
                externa.nervioso = "";
                externa.nerviososp = "X";
            }

            if (chb5cp11.Checked)
            {
                externa.piel = "X";
                externa.pielsp = "";
            }
            else
            {
                externa.piel = "";
                externa.pielsp = "X";
            }
            externa.revisionactual = txtRevisionActual.Text;

            externa.fechamedicion = Convert.ToString(dtpMedicion.Value);

            externa.temperatura = txtTemperatura.Text;
            externa.presion1 = txtPresionArteria1.Text;
            externa.presion2 = txtPresionArteria2.Text;
            externa.pulso = txtPulso.Text;
            externa.frecuenciaRespiratoria = txtFrecuenciaRespiratoria.Text;
            externa.peso = txtPeso.Text;
            externa.talla = txtTalla.Text;

            if (chb7cp1.Checked)
            {
                externa.cabeza = "X";
                externa.cabezasp = "";
            }
            else
            {
                externa.cabezasp = "X";
                externa.cabeza = "";
            }
            if (chb7cp2.Checked)
            {
                externa.cuello = "X";
                externa.cuellosp = "";
            }
            else
            {
                externa.cuellosp = "X";
                externa.cuello = "";
            }
            if (chb7cp3.Checked)
            {
                externa.torax = "X";
                externa.toraxsp = "";
            }
            else
            {
                externa.toraxsp = "X";
                externa.torax = "";
            }
            if (chb7cp4.Checked)
            {
                externa.abdomen = "X";
                externa.abdomensp = "";
            }
            else
            {
                externa.abdomensp = "X";
                externa.abdomen = "";
            }
            if (chb7cp5.Checked)
            {
                externa.pelvis = "X";
                externa.pelvissp = "";
            }
            else
            {
                externa.pelvissp = "X";
                externa.pelvis = "";
            }
            if (chb7cp6.Checked)
            {
                externa.extremidades = "X";
                externa.extremidadessp = "";
            }
            else
            {
                externa.extremidadessp = "X";
                externa.extremidades = "";
            }
            if (chbPiel.Checked)
                externa.rPiel = "X";
            else
                externa.rPiel = "";
            if (chbOjos.Checked)
                externa.rOjos = "X";
            else
                externa.rOjos = "";
            if (chbOidos.Checked)
                externa.rOidos = "X";
            else
                externa.rOidos = "";
            if (chbNariz.Checked)
                externa.rNariz = "X";
            else
                externa.rNariz = "";
            if (chbBoca.Checked)
                externa.rBoca = "X";
            else
                externa.rBoca = "";
            if (chbOrofaringe.Checked)
                externa.rOrofaringe = "X";
            else
                externa.rOrofaringe = "";
            if (chbAxilas.Checked)
                externa.rAxilas = "X";
            else
                externa.rAxilas = "";
            if (chbColumna.Checked)
                externa.rColumna = "X";
            else
                externa.rColumna = "";
            if (chbIngle.Checked)
                externa.rIngle = "X";
            else
                externa.rIngle = "";
            if (chbInferior.Checked)
                externa.rInferior = "X";
            else
                externa.rInferior = "";
            if (chbSentidos.Checked)
                externa.sSentidos = "X";
            else
                externa.sSentidos = "";
            if (chbRespiratorio.Checked)
                externa.sRespiratorio = "X";
            else
                externa.sRespiratorio = "";
            if (chbBascular.Checked)
                externa.sCardio = "X";
            else
                externa.sCardio = "";
            if (chbDigestivo.Checked)
                externa.sDigestivo = "X";
            else
                externa.sDigestivo = "";
            if (chbGenital.Checked)
                externa.sGenital = "X";
            else
                externa.sGenital = "";
            if (chbUrinario.Checked)
                externa.sUrinario = "X";
            else
                externa.sUrinario = "";
            if (chbMusculo.Checked)
                externa.sMusculo = "X";
            else
                externa.sMusculo = "";
            if (chbEndocrino.Checked)
                externa.sEndocrino = "X";
            else
                externa.sEndocrino = "";
            if (chbHemo.Checked)
                externa.sLimfatico = "X";
            else
                externa.sLimfatico = "";
            if (chbNeurologico.Checked)
                externa.sNeurologico = "X";
            else
                externa.sNeurologico = "";

            externa.examenFisico = txtExamenFisico.Text;

            externa.diagnostico1 = txtDiagnostico1.Text;
            externa.diagnostico1cie = txtCieDiagnostico1.Text;
            if (txtCieDiagnostico1.Text == "")
            {
                externa.diagnostico1pre = "";
                externa.diagnostico1def = "";
            }
            else
            {
                if (chbPre1.Checked)
                {
                    externa.diagnostico1pre = "X";
                    externa.diagnostico1def = "";
                }
                else
                {
                    externa.diagnostico1pre = "";
                    externa.diagnostico1def = "X";
                }
            }
            externa.diagnostico2 = txtDiagnostico2.Text;
            externa.diagnostico2cie = txtCieDiagnostico2.Text;
            if (txtCieDiagnostico2.Text == "")
            {
                externa.diagnostico2pre = "";
                externa.diagnostico2def = "";
            }
            else
            {
                if (chbPre2.Checked)
                {
                    externa.diagnostico2pre = "X";
                    externa.diagnostico2def = "";
                }
                else
                {
                    externa.diagnostico2pre = "";
                    externa.diagnostico2def = "X";
                }
            }
            externa.diagnostico3 = txtDiagnostico3.Text;
            externa.diagnostico3cie = txtCieDiagnostico3.Text;

            if (txtCieDiagnostico3.Text == "")
            {
                externa.diagnostico3pre = "";
                externa.diagnostico3def = "";
            }
            else
            {
                if (chbPre3.Checked)
                {
                    externa.diagnostico3pre = "X";
                    externa.diagnostico3def = "";
                }
                else
                {
                    externa.diagnostico3pre = "";
                    externa.diagnostico3def = "X";
                }
            }
            externa.diagnostico4 = txtDiagnostico4.Text;
            externa.diagnostico4cie = txtCieDiagnostico4.Text;
            if (txtCieDiagnostico4.Text == "")
            {
                externa.diagnostico4pre = "";
                externa.diagnostico4def = "";
            }
            else
            {
                if (chbPre4.Checked)
                {
                    externa.diagnostico4pre = "X";
                    externa.diagnostico4def = "";
                }
                else
                {
                    externa.diagnostico4pre = "";
                    externa.diagnostico4def = "X";
                }
            }

            externa.diagnostico5 = txtDiagnostico5.Text;
            externa.diagnostico5cie = txtCieDiagnostico5.Text;
            if (txtCieDiagnostico5.Text == "")
            {
                externa.diagnostico5pre = "";
                externa.diagnostico5def = "";
            }
            else
            {
                if (chbPre5.Checked)
                {
                    externa.diagnostico5pre = "X";
                    externa.diagnostico5def = "";
                }
                else
                {
                    externa.diagnostico5pre = "";
                    externa.diagnostico5def = "X";
                }
            }
            externa.diagnostico6 = txtDiagnostico6.Text;
            externa.diagnostico6cie = txtCieDiagnostico6.Text;
            if (txtCieDiagnostico6.Text == "")
            {
                externa.diagnostico6pre = "";
                externa.diagnostico6def = "";
            }
            else
            {
                if (chbPre6.Checked)
                {
                    externa.diagnostico6pre = "X";
                    externa.diagnostico6def = "";
                }
                else
                {
                    externa.diagnostico6pre = "";
                    externa.diagnostico6def = "X";
                }
            }


            externa.planesTratamiento = txtPlanesTratamiento.Text;
            externa.evolucion = txtEvolucion.Text;
            externa.prescripciones = txtindicaciones.Text.Trim();
            externa.dr = Sesion.nomUsuario;
            externa.codigo = "";
            if (button1.Visible == true)
            {
                externa.dr = txt_profesionalEmergencia.Text;
            }
            externa.fecha = Convert.ToString(DateTime.Now);
            externa.hora = "";
            externa.ID_USUARIO = Sesion.codUsuario;

            svcex.ID_AGENDA_PACIENTE = 1;
            svcex.Presion1 = Convert.ToDecimal(txtPresionArteria1.Text);
            svcex.Presion2 = Convert.ToDecimal(txtPresionArteria2.Text);
            svcex.ATE_CODIGO = Convert.ToInt64(externa.AteCodigo);
            svcex.F_Cardiaca = Convert.ToDecimal(txtPulso.Text);
            svcex.F_Respiratoria = Convert.ToDecimal(txtFrecuenciaRespiratoria.Text);
            svcex.S_Oxigeno = Convert.ToDecimal(txtPulsioximetria.Text);
            svcex.PerimetroA = Convert.ToDecimal(txtPerimetro.Text);
            svcex.Hemoglobina = Convert.ToDecimal(txtHemoglobina.Text);
            svcex.Glucosa_Capilar = Convert.ToDecimal(txtGlucosa.Text);
            svcex.TallaM = Convert.ToDecimal(txtTalla.Text);
            svcex.Ind_Masa = Convert.ToDecimal(txtIndice.Text);
            svcex.Ocular = Convert.ToInt16(cmb_Ocular.Text);
            svcex.Verbal = Convert.ToInt16(cmb_Verbal.Text);
            svcex.Motora = Convert.ToInt16(cmb_Motora.Text);
            svcex.Glasgow = Convert.ToDecimal(txtGlasgow.Text);
            svcex.PesoKG = Convert.ToDecimal(txtPeso.Text);
            svcex.T_Bucal = Convert.ToDecimal(txtTemperatura.Text);
            svcex.T_Axilar = Convert.ToDecimal(txtTempAx.Text);
            svcex.Reaccion_Iz = txtReaccionIz.Text;
            if (txtDiametroIz.Text.Trim() != "")
                svcex.Diametro_Iz = Convert.ToInt16(txtDiametroIz.Text);

            svcex.Reaccion_Der = txtReaccionDer.Text;
            if (txtDiametroDer.Text.Trim() != "")
                svcex.Diametro_Der = Convert.ToInt16(txtDiametroDer.Text);

            svcex.ID_USUARIO = Sesion.codUsuario;
            #region CodigoAntiguo // se comenta por que se trabaja ahora con el modelo Mario Valencia 20/10/2023
            //datos.historiaClinica = lblHistoria.Text;
            //datos.ateCodigo = lblAteCodigo.Text;
            //datos.nombrePaciente = lblNombre.Text;
            //datos.apellidoPaciemte = lblApellido.Text;
            //datos.edadPaciente = lblEdad.Text;

            //datos.sexoPaciente = lblSexo.Text;
            //datos.motivoConsulta = txtMotivo.Text;
            //datos.antecedentesPersonales = txtAntecedentesPersonales.Text;
            //if (chbCardiopatia.Checked)
            //    datos.cardiopatia = "X";
            //else
            //    datos.cardiopatia = "O";
            //if (chbDiabetes.Checked)
            //    datos.diabetes = "X";
            //else
            //    datos.diabetes = "O";
            //if (chbVascular.Checked)
            //    datos.vascular = "X";
            //else
            //    datos.vascular = "O";
            //if (chbHiperT.Checked)
            //    datos.hipertension = "X";
            //else
            //    datos.hipertension = "O";
            //if (chbCancer.Checked)
            //    datos.cancer = "X";
            //else
            //    datos.cancer = "O";
            //if (chbTuberculosis.Checked)
            //    datos.tuberculosis = "X";
            //else
            //    datos.tuberculosis = "O";
            //if (chbMental.Checked)
            //    datos.mental = "X";
            //else
            //    datos.mental = "O";
            //if (chbInfeccionsa.Checked)
            //    datos.infeccionsa = "X";
            //else
            //    datos.infeccionsa = "O";
            //if (chbMalFormado.Checked)
            //    datos.malFormado = "X";
            //else
            //    datos.malFormado = "O";
            //if (chbOtro.Checked)
            //    datos.otro = "X";
            //else
            //    datos.otro = "O";
            //datos.antecedentesFamiliares = txtAntecedentesFamiliares.Text;
            //datos.enfermedadProblemaActual = txtEnfermedadProblema.Text;
            //if (chb5cp1.Checked)
            //{
            //    datos.sentidos = "X";
            //    datos.sentidossp = "O";
            //}
            //else
            //{
            //    datos.sentidos = "O";
            //    datos.sentidossp = "X";
            //}
            //if (chb5cp2.Checked)
            //{
            //    datos.respiratorio = "X";
            //    datos.respiratoriosp = "O";
            //}
            //else
            //{
            //    datos.respiratorio = "O";
            //    datos.respiratoriosp = "X";
            //}
            //if (chb5cp3.Checked)
            //{
            //    datos.cardioVascular = "X";
            //    datos.cardioVascularsp = "O";
            //}
            //else
            //{
            //    datos.cardioVascular = "O";
            //    datos.cardioVascularsp = "X";
            //}
            //if (chb5cp4.Checked)
            //{
            //    datos.digestivo = "X";
            //    datos.digestivosp = "O";
            //}
            //else
            //{
            //    datos.digestivo = "O";
            //    datos.digestivosp = "X";
            //}
            //if (chb5cp5.Checked)
            //{
            //    datos.genital = "X";
            //    datos.genitalsp = "O";
            //}
            //else
            //{
            //    datos.genital = "O";
            //    datos.genitalsp = "X";
            //}
            //if (chb5cp6.Checked)
            //{
            //    datos.urinario = "X";
            //    datos.urinariosp = "O";
            //}
            //else
            //{
            //    datos.urinario = "O";
            //    datos.urinariosp = "X";
            //}
            //if (chb5cp7.Checked)
            //{
            //    datos.esqueletico = "X";
            //    datos.esqueleticosp = "O";
            //}
            //else
            //{
            //    datos.esqueletico = "O";
            //    datos.esqueleticosp = "X";
            //}
            //if (chb5cp8.Checked)
            //{
            //    datos.endocrino = "X";
            //    datos.endocrinosp = "O";
            //}
            //else
            //{
            //    datos.endocrino = "O";
            //    datos.endocrinosp = "X";
            //}
            //if (chb5cp9.Checked)
            //{
            //    datos.linfatico = "X";
            //    datos.linfaticosp = "O";
            //}
            //else
            //{
            //    datos.linfatico = "O";
            //    datos.linfaticosp = "X";
            //}
            //if (chb5cp10.Checked)
            //{
            //    datos.nervioso = "X";
            //    datos.nerviososp = "O";
            //}
            //else
            //{
            //    datos.nervioso = "O";
            //    datos.nerviososp = "X";
            //}
            //datos.detalleRevisionOrganos = txtRevisionActual.Text;
            //datos.fechaMedicion = Convert.ToString(dtpMedicion.Value);
            //datos.temperatura = txtTemperatura.Text;
            //datos.presionArterial1 = txtPresionArteria1.Text;
            //datos.presionArterial2 = txtPresionArteria2.Text;
            //datos.pulso = txtPulso.Text;
            //datos.frecuenciaRespiratoria = txtFrecuenciaRespiratoria.Text;
            //datos.peso = txtPeso.Text;
            //datos.talla = txtTalla.Text;
            //if (chb7cp1.Checked)
            //{
            //    datos.cabeza = "X";
            //    datos.cabezasp = "O";
            //}
            //else
            //{
            //    datos.cabezasp = "X";
            //    datos.cabeza = "O";
            //}
            //if (chb7cp2.Checked)
            //{
            //    datos.cuello = "X";
            //    datos.cuellosp = "O";
            //}
            //else
            //{
            //    datos.cuellosp = "X";
            //    datos.cuello = "O";
            //}
            //if (chb7cp3.Checked)
            //{
            //    datos.torax = "X";
            //    datos.toraxsp = "O";
            //}
            //else
            //{
            //    datos.toraxsp = "X";
            //    datos.torax = "O";
            //}
            //if (chb7cp4.Checked)
            //{
            //    datos.abdomen = "X";
            //    datos.abdomensp = "O";
            //}
            //else
            //{
            //    datos.abdomensp = "X";
            //    datos.abdomen = "O";
            //}
            //if (chb7cp5.Checked)
            //{
            //    datos.pelvis = "X";
            //    datos.pelvissp = "O";
            //}
            //else
            //{
            //    datos.pelvissp = "X";
            //    datos.pelvis = "O";
            //}
            //if (chb7cp6.Checked)
            //{
            //    datos.extremidades = "X";
            //    datos.extremidadessp = "O";
            //}
            //else
            //{
            //    datos.extremidadessp = "X";
            //    datos.extremidades = "O";
            //}
            //datos.examenFisico = txtExamenFisico.Text;
            //datos.diagnosticco1 = txtDiagnostico1.Text;
            //datos.diagnosticco1cie = txtCieDiagnostico1.Text;
            //if (txtCieDiagnostico1.Text == "")
            //{
            //    datos.diagnosticco1prepre = "O";
            //    datos.diagnosticco1predef = "O";
            //}
            //else
            //{
            //    if (chbPre1.Checked)
            //    {
            //        datos.diagnosticco1prepre = "X";
            //        datos.diagnosticco1predef = "O";
            //    }
            //    else
            //    {
            //        datos.diagnosticco1prepre = "O";
            //        datos.diagnosticco1predef = "X";
            //    }
            //}
            //datos.diagnosticco2 = txtDiagnostico2.Text;
            //datos.diagnosticco2cie = txtCieDiagnostico2.Text;
            //if (txtCieDiagnostico2.Text == "")
            //{
            //    datos.diagnosticco2prepre = "O";
            //    datos.diagnosticco2predef = "O";
            //}
            //else
            //{
            //    if (chbPre2.Checked)
            //    {
            //        datos.diagnosticco2prepre = "X";
            //        datos.diagnosticco2predef = "O";
            //    }
            //    else
            //    {
            //        datos.diagnosticco2prepre = "O";
            //        datos.diagnosticco2predef = "X";
            //    }
            //}
            //datos.diagnosticco3 = txtDiagnostico3.Text;
            //datos.diagnosticco3cie = txtCieDiagnostico3.Text;

            //if (txtCieDiagnostico3.Text == "")
            //{
            //    datos.diagnosticco3prepre = "O";
            //    datos.diagnosticco3predef = "O";
            //}
            //else
            //{
            //    if (chbPre3.Checked)
            //    {
            //        datos.diagnosticco3prepre = "X";
            //        datos.diagnosticco3predef = "O";
            //    }
            //    else
            //    {
            //        datos.diagnosticco3prepre = "O";
            //        datos.diagnosticco3predef = "X";
            //    }
            //}
            //datos.diagnosticco4 = txtDiagnostico4.Text;
            //datos.diagnosticco4cie = txtCieDiagnostico4.Text;
            //if (txtCieDiagnostico4.Text == "")
            //{
            //    datos.diagnosticco4prepre = "O";
            //    datos.diagnosticco4predef = "O";
            //}
            //else
            //{
            //    if (chbPre4.Checked)
            //    {
            //        datos.diagnosticco4prepre = "X";
            //        datos.diagnosticco4predef = "O";
            //    }
            //    else
            //    {
            //        datos.diagnosticco4prepre = "O";
            //        datos.diagnosticco4predef = "X";
            //    }
            //}
            //datos.planesTratamiento = txtPlanesTratamiento.Text;
            //datos.evolucion = txtEvolucion.Text;
            //datos.prescripciones = txtindicaciones.Text.Trim();
            //datos.drTratatnte = Sesion.nomUsuario;
            //if (button1.Visible == true)
            //{
            //    datos.drTratatnte = txt_profesionalEmergencia.Text;
            //}
            #endregion
            if (nuevaConsulta)
            {
                NegConsultaExterna.GuardaDatos002(externa, svcex);
                id_form002 = NegConsultaExterna.RecuperarId();
                return true;
            }
            else
            {
                if (NegConsultaExterna.EditarForm002(externa, id_form002, svcex))
                    return true;
                else
                    return false;
            }

        }



        #endregion

        private void txtMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txtAntecedentesPersonales.Focus();
            }
        }

        private void txtAntecedentesPersonales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txtAntecedentesFamiliares.Focus();
            }
        }

        private void txtAntecedentesFamiliares_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                pantab1.SelectedTab = pantab1.Tabs["CuatroCinco"];
                SendKeys.SendWait("{TAB}");
                txtEnfermedadProblema.Focus();
            }
        }

        private void txtEnfermedadProblema_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txtRevisionActual.Focus();
            }
        }


        private void txtExamenFisico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                pantab1.SelectedTab = pantab1.Tabs["OchoNueve"];
                SendKeys.SendWait("{TAB}");
                txtDiagnostico1.Focus();
            }
        }

        private void txtDiagnostico1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico1.Text != "")
                {
                    //txtCieDiagnostico1.Enabled = true;
                    //txtCieDiagnostico1.Focus();
                    txtDiagnostico2.Focus();
                }
            }

        }

        private void txtCieDiagnostico1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbPre1.Focus();
            }
        }

        private void chbPre1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbDef1.Focus();
            }
        }

        private void chbDef1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtDiagnostico2.Focus();
            }
        }

        private void txtDiagnostico2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico2.Text != "")
                {
                    //txtCieDiagnostico2.Enabled = true;
                    //txtCieDiagnostico2.Focus();
                    txtDiagnostico3.Focus();
                }
            }
        }

        private void txtCieDiagnostico2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbPre2.Focus();
            }
        }

        private void chbPre2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbDef2.Focus();
            }
        }

        private void chbDef2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtDiagnostico3.Focus();
            }
        }

        private void txtDiagnostico3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico3.Text != "")
                {
                    //txtCieDiagnostico3.Enabled = true;
                    //txtCieDiagnostico3.Focus();
                    txtDiagnostico4.Focus();
                }
            }
        }

        private void txtCieDiagnostico3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbPre3.Focus();
            }
        }

        private void chbPre3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbDef3.Focus();
            }
        }

        private void chbDef3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtDiagnostico4.Focus();
            }
        }

        private void txtDiagnostico4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico4.Text != "")
                {
                    //txtCieDiagnostico4.Enabled = true;
                    //txtCieDiagnostico4.Focus();
                    txtPlanesTratamiento.Focus();
                }
            }
        }

        private void txtCieDiagnostico4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbPre4.Focus();
            }
        }

        private void chbPre4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                chbDef4.Focus();
            }
        }

        private void chbDef4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                txtPlanesTratamiento.Focus();
            }
        }

        private void txtPlanesTratamiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                pantab1.SelectedTab = pantab1.Tabs["Evolucion"];
                SendKeys.SendWait("{TAB}");
                txtEvolucion.Focus();
            }
        }

        private void chb5cp1_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp1.Checked)
                chb5sp1.Checked = false;
            else
                chb5sp1.Checked = true;
        }

        private void chb5cp2_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp2.Checked)
                chb5sp2.Checked = false;
            else
                chb5sp2.Checked = true;
        }

        private void chb5cp3_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp3.Checked)
                chb5sp3.Checked = false;
            else
                chb5sp3.Checked = true;
        }

        private void chb5cp4_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp4.Checked)
                chb5sp4.Checked = false;
            else
                chb5sp4.Checked = true;
        }

        private void chb5cp5_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp5.Checked)
                chb5sp5.Checked = false;
            else
                chb5sp5.Checked = true;
        }

        private void chb5cp6_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp6.Checked)
                chb5sp6.Checked = false;
            else
                chb5sp6.Checked = true;
        }

        private void chb5cp7_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp7.Checked)
                chb5sp7.Checked = false;
            else
                chb5sp7.Checked = true;
        }

        private void chb5cp8_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp8.Checked)
                chb5sp8.Checked = false;
            else
                chb5sp8.Checked = true;
        }

        private void chb5cp9_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp9.Checked)
                chb5sp9.Checked = false;
            else
                chb5sp9.Checked = true;
        }

        private void chb5cp10_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5cp10.Checked)
                chb5sp10.Checked = false;
            else
                chb5sp10.Checked = true;
        }

        private void chb5sp1_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp1.Checked)
                chb5cp1.Checked = false;
            else
                chb5cp1.Checked = true;
        }

        private void chb5sp2_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp2.Checked)
                chb5cp2.Checked = false;
            else
                chb5cp2.Checked = true;
        }

        private void chb5sp3_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp3.Checked)
                chb5cp3.Checked = false;
            else
                chb5cp3.Checked = true;
        }

        private void chb5sp4_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp4.Checked)
                chb5cp4.Checked = false;
            else
                chb5cp4.Checked = true;
        }

        private void chb5sp5_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp5.Checked)
                chb5cp5.Checked = false;
            else
                chb5cp5.Checked = true;
        }

        private void chb5sp6_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp6.Checked)
                chb5cp6.Checked = false;
            else
                chb5cp6.Checked = true;
        }

        private void chb5sp7_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp7.Checked)
                chb5cp7.Checked = false;
            else
                chb5cp7.Checked = true;
        }

        private void chb5sp8_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp8.Checked)
                chb5cp8.Checked = false;
            else
                chb5cp8.Checked = true;
        }

        private void chb5sp9_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp9.Checked)
                chb5cp9.Checked = false;
            else
                chb5cp9.Checked = true;
        }

        private void chb5sp10_CheckedChanged(object sender, EventArgs e)
        {
            if (chb5sp10.Checked)
                chb5cp10.Checked = false;
            else
                chb5cp10.Checked = true;
        }

        private void chb7cp1_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp1.Checked)
                chb7sp1.Checked = false;
            else
                chb7sp1.Checked = true;
        }

        private void chb7cp2_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp2.Checked)
                chb7sp2.Checked = false;
            else
                chb7sp2.Checked = true;
        }

        private void chb7cp3_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp3.Checked)
                chb7sp3.Checked = false;
            else
                chb7sp3.Checked = true;
        }

        private void chb7cp4_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp4.Checked)
                chb7sp4.Checked = false;
            else
                chb7sp4.Checked = true;
        }

        private void chb7cp5_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp5.Checked)
                chb7sp5.Checked = false;
            else
                chb7sp5.Checked = true;
        }

        private void chb7cp6_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7cp6.Checked)
                chb7sp6.Checked = false;
            else
                chb7sp6.Checked = true;
        }

        private void chb7sp1_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp1.Checked)
                chb7cp1.Checked = false;
            else
                chb7cp1.Checked = true;
        }

        private void chb7sp2_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp2.Checked)
                chb7cp2.Checked = false;
            else
                chb7cp2.Checked = true;
        }

        private void chb7sp3_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp3.Checked)
                chb7cp3.Checked = false;
            else
                chb7cp3.Checked = true;
        }

        private void chb7sp4_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp4.Checked)
                chb7cp4.Checked = false;
            else
                chb7cp4.Checked = true;
        }

        private void chb7sp5_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp5.Checked)
                chb7cp5.Checked = false;
            else
                chb7cp5.Checked = true;
        }

        private void chb7sp6_CheckedChanged(object sender, EventArgs e)
        {
            if (chb7sp6.Checked)
                chb7cp6.Checked = false;
            else
                chb7cp6.Checked = true;
        }

        private void chbPre1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico1.Text != "")
                if (chbPre1.Checked)
                    chbDef1.Checked = false;
                else
                    chbDef1.Checked = true;
        }

        private void chbPre2_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico2.Text != "")
                if (chbPre2.Checked)
                    chbDef2.Checked = false;
                else
                    chbDef2.Checked = true;
        }

        private void chbPre3_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico3.Text != "")
                if (chbPre3.Checked)
                    chbDef3.Checked = false;
                else
                    chbDef3.Checked = true;
        }

        private void chbPre4_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico4.Text != "")
                if (chbPre4.Checked)
                    chbDef4.Checked = false;
                else
                    chbDef4.Checked = true;
        }

        private void chbDef1_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico1.Text != "")
                if (chbDef1.Checked)
                    chbPre1.Checked = false;
                else
                    chbPre1.Checked = true;
        }

        private void chbDef2_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico2.Text != "")
                if (chbDef2.Checked)
                    chbPre2.Checked = false;
                else
                    chbPre2.Checked = true;
        }

        private void chbDef3_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico3.Text != "")
                if (chbDef3.Checked)
                    chbPre3.Checked = false;
                else
                    chbPre3.Checked = true;
        }

        private void chbDef4_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico4.Text != "")
                if (chbDef4.Checked)
                    chbPre4.Checked = false;
                else
                    chbPre4.Checked = true;
        }

        public void HabilitarBotones(bool buscar, bool guardar, bool editar, bool imprimir, bool receta, bool certificado, bool imagen, bool laboratorio, bool abrir, bool cerrar, bool Subsecuente)
        {
            btnBuscar.Enabled = buscar;
            btnGuardar.Enabled = guardar;
            btnEditar.Enabled = editar;
            btnImprimir1.Enabled = imprimir;
            btnReceta1.Enabled = receta;
            btnCertificado1.Enabled = certificado;
            btnImagen.Enabled = imagen;
            btnLaboratorio.Enabled = laboratorio;
            btnAbrir.Enabled = abrir;
            btnCerrar.Enabled = cerrar;
            btnSubsecuente.Enabled = Subsecuente;
        }
        private void btnGuarda_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                if (GuardaFormulario())
                {
                    MessageBox.Show("Información Guardada Con Exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //P_Central.Enabled = false;
                    grupos(false);
                    HabilitarBotones(false, false, true, true, true, true, true, true, false, true, true);
                }
                else
                    MessageBox.Show("Información No Se Guardo Comuniquese Con Sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            btnGuarda.Enabled = false;
            if (datos.sexoPaciente == "Masculino")
            {
                datos.sexoPaciente = "M";
            }
            else
                datos.sexoPaciente = "F";

            NegCertificadoMedico neg = new NegCertificadoMedico();
            //HCU_form002MSP Ds = new HCU_form002MSP();
            His.Formulario.HCU_form002MSP Ds = new His.Formulario.HCU_form002MSP();
            //His.Formulario.FrmConsultaExterna002 Ds = new His.Formulario.FrmConsultaExterna002();
            Ds.Tables[0].Rows.Add
                (new object[]
                {
                    datos.nombrePaciente.ToString(),
                    datos.apellidoPaciemte.ToString(),
                    datos.sexoPaciente.ToString(),
                    datos.edadPaciente.ToString(),
                    datos.historiaClinica.ToString().Trim(),
                    datos.ateCodigo.ToString(),
                    datos.motivoConsulta.ToString(),
                    datos.antecedentesPersonales.ToString(),
                    datos.cardiopatia.ToString(),
                    datos.diabetes.ToString(),
                    datos.vascular.ToString(),
                    datos.hipertension.ToString(),
                    datos.cancer.ToString(),
                    datos.tuberculosis.ToString(),
                    datos.mental.ToString(),
                    datos.infeccionsa.ToString(),
                    datos.malFormado.ToString(),
                    datos.otro.ToString(),
                    datos.antecedentesFamiliares.ToString(),
                    datos.enfermedadProblemaActual.ToString(),
                    datos.sentidos.ToString(),
                    datos.sentidossp.ToString(),
                    datos.respiratorio.ToString(),
                    datos.respiratoriosp.ToString(),
                    datos.cardioVascular.ToString(),
                    datos.cardioVascularsp.ToString(),
                    datos.digestivo.ToString(),
                    datos.digestivosp.ToString(),
                    datos.genital.ToString(),
                    datos.genitalsp.ToString(),
                    datos.urinario.ToString(),
                    datos.urinariosp.ToString(),
                    datos.esqueletico.ToString(),
                    datos.esqueleticosp.ToString(),
                    datos.endocrino.ToString(),
                    datos.endocrinosp.ToString(),
                    datos.linfatico.ToString(),
                    datos.linfaticosp.ToString(),
                    datos.nervioso.ToString(),
                    datos.nerviososp.ToString(),
                    datos.detalleRevisionOrganos.ToString(),
                    datos.fechaMedicion.ToString(),
                    datos.temperatura.ToString(),
                    datos.presionArterial1.ToString(),
                    datos.presionArterial2.ToString(),
                    datos.pulso.ToString(),
                    datos.frecuenciaRespiratoria.ToString(),
                    datos.peso.ToString(),
                    datos.talla.ToString(),
                    datos.cabeza.ToString(),
                    datos.cabezasp.ToString(),
                    datos.cuello.ToString(),
                    datos.cuellosp.ToString(),
                    datos.torax.ToString(),
                    datos.toraxsp.ToString(),
                    datos.abdomen.ToString(),
                    datos.abdomensp.ToString(),
                    datos.pelvis.ToString(),
                    datos.pelvissp.ToString(),
                    datos.extremidades.ToString(),
                    datos.extremidadessp.ToString(),
                    datos.examenFisico.ToString(),
                    datos.diagnosticco1.ToString(),
                    datos.diagnosticco1cie.ToString(),
                    datos.diagnosticco1prepre.ToString(),
                    datos.diagnosticco1predef.ToString(),
                    datos.diagnosticco2.ToString(),
                    datos.diagnosticco2cie.ToString(),
                    datos.diagnosticco2prepre.ToString(),
                    datos.diagnosticco2predef.ToString(),
                    datos.diagnosticco3.ToString(),
                    datos.diagnosticco3cie.ToString(),
                    datos.diagnosticco3predef.ToString(),
                    datos.diagnosticco3prepre.ToString(),
                    datos.diagnosticco4.ToString(),
                    datos.diagnosticco4cie.ToString(),
                    datos.diagnosticco4predef.ToString(),
                    datos.diagnosticco4prepre.ToString(),
                    datos.planesTratamiento.ToString(),
                    datos.evolucion.ToString(),
                    datos.prescripciones.ToString(),
                    Convert.ToString(dtp_fechaAltaEmerencia.Value),
                    Convert.ToString(DateTime.Now.ToString("hh:mm")),
                    datos.drTratatnte.ToString(),
                    Sesion.codMedico.ToString(),
                    neg.path(),
                    Sesion.nomEmpresa
                });
            PACIENTES pacien = new PACIENTES();
            pacien = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(txtatecodigo.Text));
            if (NegParametros.ParametroFormularios())
                datos.historiaClinica = pacien.PAC_IDENTIFICACION;
            frmReportes x = new frmReportes(1, "ConsultaExterna", Ds);
            x.Show();
            //HCU_Form002MSPrpt report = new HCU_Form002MSPrpt();
            //report.SetDataSource(Ds);
            //CrystalDecisions.Windows.Forms.CrystalReportViewer vista = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            //vista.ReportSource = report;
            //vista.PrintReport();
        }
        public void CargarPacienteExiste(Int64 ATE_CODIGO = 0) //Cambios Edgar 20210203 antes no rescataba la informacion.
        {
            try
            {
                ATENCIONES atencion = new ATENCIONES();
                DataTable TPaciente = new DataTable(); //Trae el paciente con todas las consultas externas
                DataTable DatosPaciente = new DataTable(); //Contiene los datos del paciente
                //TPaciente = NegConsultaExterna.ExistePaciente(Convert.ToInt64(txtatecodigo.Text)); // Cambio para subsecuentes Mario 28/01/2023
                if (ATE_CODIGO == 0)
                {
                    TPaciente = NegConsultaExterna.ExistePaciente(Convert.ToInt64(txtatecodigo.Text));
                    atencion = NegAtenciones.RecuepraAtencionNumeroAtencion(Convert.ToInt64(txtatecodigo.Text));
                }
                else
                {
                    TPaciente = NegConsultaExterna.ExistePaciente(ATE_CODIGO);
                    atencion = NegAtenciones.RecuepraAtencionNumeroAtencion(ATE_CODIGO);
                }
                medico = NegMedicos.recuperarMedico(Convert.ToInt32(atencion.MEDICOSReference.EntityKey.EntityKeyValues[0].Value));
                if (TPaciente.Rows.Count > 0)
                {
                    txt_Medico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                    if (id_form002 == 0)
                        id_form002 = Convert.ToInt64(TPaciente.Rows[TPaciente.Rows.Count - 1][5].ToString());
                    conExter = NegConsultaExterna.DatosPaciente(Convert.ToInt32(id_form002));
                    SIGNOSVITALES_CONSULTAEXTERNA sign = NegConsultaExterna.signoscitalesCex(Convert.ToInt64(conExter.AteCodigo));
                    txtMotivo.Text = conExter.Motivo;
                    //cardiopatia
                    if (conExter.cardiopatiaP == "X")
                    {
                        chbCardiopatiaP.Checked = true;
                    }
                    else
                        chbCardiopatiaP.Checked = false;
                    //diabetes
                    if (conExter.endocrinoP == "X")
                        chbMetabolicoP.Checked = true;
                    else
                        chbMetabolicoP.Checked = false;
                    //Vascular
                    if (conExter.vascularP == "X")
                        chbVascularP.Checked = true;
                    else
                        chbVascularP.Checked = false;
                    //HiperTension
                    if (conExter.hipertencionP == "X")
                        chbHipertensionP.Checked = true;
                    else
                        chbHipertensionP.Checked = false;
                    //Cancer
                    if (conExter.cancerP == "X")
                        chbCancerP.Checked = true;
                    else
                        chbCancerP.Checked = false;
                    //tuberculosis
                    if (conExter.tuberculosisP == "X")
                        chbTuberculosisP.Checked = true;
                    else
                        chbTuberculosisP.Checked = false;
                    //mental
                    if (conExter.mentalP == "X")
                        chbMentalP.Checked = true;
                    else
                        chbMentalP.Checked = false;
                    //infecciosa
                    if (conExter.infecciosaP == "X")
                        chbInfecciosa.Checked = true;
                    else
                        chbInfecciosa.Checked = false;
                    //malformacion
                    if (conExter.malformacionP == "X")
                        chbFormacionP.Checked = true;
                    else
                        chbFormacionP.Checked = false;
                    //otro
                    if (conExter.otroP == "X")
                        chbOtroP.Checked = true;
                    else
                        chbOtroP.Checked = false;
                    txtAntecedentesPersonales.Text = conExter.AntecedentesPersonales;
                    //cardiopatia
                    if (conExter.Cardiopatia == "X")
                    {
                        chbCardiopatia.Checked = true;
                    }
                    else
                        chbCardiopatia.Checked = false;
                    //diabetes
                    if (conExter.endocrino == "X")
                        chbMetabolico.Checked = true;
                    else
                        chbMetabolico.Checked = false;
                    //Vascular
                    if (conExter.Vascular == "X")
                        chbVascular.Checked = true;
                    else
                        chbVascular.Checked = false;
                    //HiperTension
                    if (conExter.Hipertencion == "X")
                        chbHiperT.Checked = true;
                    else
                        chbHiperT.Checked = false;
                    //Cancer
                    if (conExter.Cancer == "X")
                        chbCancer.Checked = true;
                    else
                        chbCancer.Checked = false;
                    //tuberculosis
                    if (conExter.tuberculosis == "X")
                        chbTuberculosis.Checked = true;
                    else
                        chbTuberculosis.Checked = false;
                    //mental
                    if (conExter.mental == "X")
                        chbMental.Checked = true;
                    else
                        chbMental.Checked = false;
                    //infecciosa
                    if (conExter.infecciosa == "X")
                        chbInfeccionsa.Checked = true;
                    else
                        chbInfeccionsa.Checked = false;
                    //malformacion
                    if (conExter.malformacion == "X")
                        chbMalFormado.Checked = true;
                    else
                        chbMalFormado.Checked = false;
                    //otro
                    if (conExter.otro == "X")
                        chbOtro.Checked = true;
                    else
                        chbOtro.Checked = false;
                    //antecedentes familiares
                    txtAntecedentesFamiliares.Text = conExter.antecedentesFamiliares;
                    //enfermedad actual
                    txtEnfermedadProblema.Text = conExter.enfermedadActual;
                    //piel
                    if (conExter.piel == "X")
                        chb5cp11.Checked = true;
                    else
                        chb5cp11.Checked = false;
                    //sentidos
                    if (conExter.sentidos == "X")
                        chb5cp1.Checked = true;
                    else
                        chb5cp1.Checked = false;
                    //respiratorio
                    if (conExter.respiratorio == "X")
                        chb5cp2.Checked = true;
                    else
                        chb5cp2.Checked = false;
                    //CardioVascular
                    if (conExter.cardioVascular == "X")
                        chb5cp3.Checked = true;
                    else
                        chb5cp3.Checked = false;
                    //digestivo
                    //if (DatosPaciente.Rows[0][27].ToString() == "X")
                    //    chb5cp4.Checked = true;
                    //else
                    //    chb5cp4.Checked = false;
                    //genital
                    if (conExter.genital == "X")
                        chb5cp5.Checked = true;
                    else
                        chb5cp5.Checked = false;
                    //urinario
                    if (conExter.urinario == "X")
                        chb5cp6.Checked = true;
                    else
                        chb5cp6.Checked = false;
                    //esqueletico
                    if (conExter.esqueletico == "X")
                        chb5cp7.Checked = true;
                    else
                        chb5cp7.Checked = false;
                    //endocrino
                    if (conExter.endocrino == "X")
                        chb5cp8.Checked = true;
                    else
                        chb5cp8.Checked = false;
                    //linfatico
                    if (conExter.linfatico == "X")
                        chb5cp9.Checked = true;
                    else
                        chb5cp9.Checked = false;
                    //nervioso
                    if (conExter.nervioso == "X")
                        chb5cp10.Checked = true;
                    else
                        chb5cp10.Checked = false;
                    //revision actual
                    txtRevisionActual.Text = conExter.revisionactual;
                    dtpMedicion.Value = Convert.ToDateTime(conExter.fechamedicion);
                    //txtTemperatura.Text = conExter.temperatura;
                    //txtPresionArteria1.Text = conExter.presion1;
                    //txtPresionArteria2.Text = conExter.presion2;
                    //txtPulso.Text = conExter.pulso;
                    //txtFrecuenciaRespiratoria.Text = conExter.frecuenciaRespiratoria;
                    //txtPeso.Text = conExter.peso;
                    //txtTalla.Text = conExter.talla;
                    //cabeza
                    if (sign != null)
                    {
                        txtTemperatura.Text = Convert.ToString(sign.T_Bucal);
                        txtTempAx.Text = Convert.ToString(sign.T_Axilar);
                        txtPresionArteria1.Text = Convert.ToInt32(sign.Presion1).ToString();
                        txtPresionArteria2.Text = Convert.ToInt32(sign.Presion2).ToString();
                        txtPulso.Text = Convert.ToInt32(sign.F_Cardiaca).ToString();
                        txtFrecuenciaRespiratoria.Text = Convert.ToString(sign.F_Respiratoria);
                        cmb_Ocular.Text = sign.Ocular.ToString();
                        cmb_Verbal.Text = sign.Verbal.ToString();
                        cmb_Motora.Text = sign.Motora.ToString();
                        txtGlasgow.Text = Convert.ToString(sign.Glasgow);
                        txtTalla.Text = Convert.ToString(sign.TallaM);
                        txtPeso.Text = Convert.ToString(sign.PesoKG);
                        txtIndice.Text = Convert.ToString(sign.Ind_Masa);
                        txtPerimetro.Text = Convert.ToString(sign.PerimetroA);
                        txtHemoglobina.Text = Convert.ToString(sign.Hemoglobina);
                        txtGlucosa.Text = Convert.ToString(sign.Glucosa_Capilar);
                        txtPulsioximetria.Text = Convert.ToInt32(sign.S_Oxigeno).ToString();
                        if (sign.Diametro_Der.ToString() == "-1")
                            txtDiametroDer.Text = "";
                        else
                            txtDiametroDer.Text = sign.Diametro_Der.ToString();
                        txtReaccionDer.Text = sign.Reaccion_Der.ToString();
                        if (sign.Diametro_Iz.ToString() == "-1")
                            txtDiametroIz.Text = "";
                        else
                            txtDiametroIz.Text = sign.Diametro_Iz.ToString();
                        txtReaccionIz.Text = sign.Reaccion_Iz.ToString();
                    }
                    if (conExter.cabeza == "X")
                        chb7cp1.Checked = true;
                    else
                        chb7cp1.Checked = false;
                    //cuello
                    if (conExter.cuello == "X")
                        chb7cp2.Checked = true;
                    else
                        chb7cp2.Checked = false;
                    //torax
                    if (conExter.torax == "X")
                        chb7cp3.Checked = true;
                    else
                        chb7cp3.Checked = false;
                    //abdomen
                    if (conExter.abdomen == "X")
                        chb7cp4.Checked = true;
                    else
                        chb7cp4.Checked = false;
                    //pelvis
                    if (conExter.pelvis == "X")
                        chb7cp5.Checked = true;
                    else
                        chb7cp5.Checked = false;
                    //extremidades
                    if (conExter.extremidades == "X")
                        chb7cp6.Checked = true;
                    else
                        chb7cp5.Checked = false;
                    if (conExter.rPiel == "X")
                        chbPiel.Checked = true;
                    else
                        chbPiel.Checked = false;

                    if (conExter.rPiel == "X")
                        chbPiel.Checked = true;
                    else
                        chbPiel.Checked = false;
                    if (conExter.rOjos == "X")
                        chbOjos.Checked = true;
                    else
                        chbOjos.Checked = false;
                    if (conExter.rOidos == "X")
                        chbOidos.Checked = true;
                    else
                        chbOidos.Checked = false;
                    if (conExter.rNariz == "X")
                        chbNariz.Checked = true;
                    else
                        chbNariz.Checked = false;
                    if (conExter.rBoca == "X")
                        chbBoca.Checked = true;
                    else
                        chbBoca.Checked = false;
                    if (conExter.rOrofaringe == "X")
                        chbOrofaringe.Checked = true;
                    else
                        chbOrofaringe.Checked = false;
                    if (conExter.rAxilas == "X")
                        chbAxilas.Checked = true;
                    else
                        chbAxilas.Checked = false;
                    if (conExter.rColumna == "X")
                        chbColumna.Checked = true;
                    else
                        chbColumna.Checked = false;
                    if (conExter.rIngle == "X")
                        chbIngle.Checked = true;
                    else
                        chbIngle.Checked = false;
                    if (conExter.rInferior == "X")
                        chbInferior.Checked = true;
                    else
                        chbInferior.Checked = false;
                    if (conExter.sSentidos == "X")
                        chbSentidos.Checked = true;
                    else
                        chbSentidos.Checked = false;
                    if (conExter.sRespiratorio == "X")
                        chbRespiratorio.Checked = true;
                    else
                        chbRespiratorio.Checked = false;
                    if (conExter.sCardio == "X")
                        chbBascular.Checked = true;
                    else
                        chbBascular.Checked = false;
                    if (conExter.sDigestivo == "X")
                        chbDigestivo.Checked = true;
                    else
                        chbDigestivo.Checked = false;
                    if (conExter.sGenital == "X")
                        chbGenital.Checked = true;
                    else
                        chbGenital.Checked = false;
                    if (conExter.sUrinario == "X")
                        chbUrinario.Checked = true;
                    else
                        chbUrinario.Checked = false;
                    if (conExter.sMusculo == "X")
                        chbMusculo.Checked = true;
                    else
                        chbMusculo.Checked = false;
                    if (conExter.sEndocrino == "X")
                        chbEndocrino.Checked = true;
                    else
                        chbEndocrino.Checked = false;
                    if (conExter.sLimfatico == "X")
                        chbHemo.Checked = true;
                    else
                        chbHemo.Checked = false;
                    if (conExter.sNeurologico == "X")
                        chbNeurologico.Checked = true;
                    else
                        chbNeurologico.Checked = false;
                    //examen fisico
                    txtExamenFisico.Text = conExter.examenFisico;
                    //Cie10
                    txtDiagnostico1.Text = conExter.diagnostico1;
                    txtCieDiagnostico1.Text = conExter.diagnostico1cie;
                    if (conExter.diagnostico1pre == "X")
                    {
                        chbPre1.Checked = true;

                    }
                    else
                    {
                        chbPre1.Checked = false;
                        chbDef1.Checked = true;
                    }

                    txtDiagnostico2.Text = conExter.diagnostico2;
                    txtCieDiagnostico2.Text = conExter.diagnostico2cie;
                    if (conExter.diagnostico2pre == "X")
                    {

                        chbPre2.Checked = true;
                    }
                    else
                    {
                        chbPre2.Checked = false;
                        chbDef2.Checked = true;
                    }


                    txtDiagnostico3.Text = conExter.diagnostico3;
                    txtCieDiagnostico3.Text = conExter.diagnostico3cie;
                    if (conExter.diagnostico3pre == "X")
                    {

                        chbPre3.Checked = true;
                    }

                    else
                    {

                        chbPre3.Checked = false;
                        chbDef3.Checked = true;
                    }


                    txtDiagnostico4.Text = conExter.diagnostico4;
                    txtCieDiagnostico4.Text = conExter.diagnostico4cie;
                    if (conExter.diagnostico4pre == "X")
                    {

                        chbPre4.Checked = true;
                    }
                    else
                    {

                        chbPre4.Checked = false;
                        chbDef4.Checked = true;
                    }

                    txtDiagnostico5.Text = conExter.diagnostico5;
                    txtCieDiagnostico5.Text = conExter.diagnostico5cie;
                    if (conExter.diagnostico5pre == "X")
                    {

                        chbPre5.Checked = true;
                    }
                    else
                    {

                        chbPre5.Checked = false;
                        chbDef5.Checked = true;
                    }

                    txtDiagnostico6.Text = conExter.diagnostico6;
                    txtCieDiagnostico6.Text = conExter.diagnostico6cie;
                    if (conExter.diagnostico6pre == "X")
                    {

                        chbPre6.Checked = true;
                    }
                    else
                    {
                        chbPre6.Checked = false;
                        chbDef6.Checked = true;

                    }
                    //tratamiento
                    txtPlanesTratamiento.Text = conExter.planesTratamiento;
                    //txtEvolucion.Text = DatosPaciente.Rows[0][80].ToString();

                    //txtindicaciones.Text = DatosPaciente.Rows[0][81].ToString();
                    dtp_fechaAltaEmerencia.Value = Convert.ToDateTime(conExter.fecha);
                    txt_horaAltaEmerencia.Text = Convert.ToDateTime(conExter.fecha).ToShortTimeString();
                    txt_profesionalEmergencia.Text = conExter.dr;
                    txt_CodMSPE.Text = conExter.codigo;
                    ATENCIONES validaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(txtatecodigo.Text.Trim()));
                    //FORMULARIOS_MSP_CERRADOS cerrado = NegConsultaExterna.ValidaCerrado(Convert.ToInt64(txtatecodigo.Text.Trim()));
                    if (subsecuente != null)
                    {
                        if (!NegConsultaExterna.PacienteCerradaCxE(subsecuente.ate_codigo_subsecuente))
                        {
                            HabilitarBotones(false, false, true, true, true, true, true, true, false, true, true);
                            grupos(false);
                        }
                        else
                        {
                            HabilitarBotones(false, false, false, true, true, true, true, true, true, false, true);
                            grupos(false);
                        }
                    }
                    else
                    {
                        if (validaAtencion.ESC_CODIGO != 1)
                        {
                            HabilitarBotones(false, false, false, true, true, true, true, true, true, false, false);
                            //P_Central.Enabled = false;
                            grupos(false);
                        }
                        else
                        {
                            HabilitarBotones(false, false, false, true, true, true, true, true, false, true, true);
                            //P_Central.Enabled = true;
                            grupos(true);
                        }

                    }
                    nuevaConsulta = false;
                }
                else
                {
                    HabilitarBotones(false, true, false, false, false, false, false, false, false, false, false);
                    //P_Central.Enabled = true;
                    grupos(true);
                    nuevaConsulta = true;
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Usted Va Salir Del Formulario", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                this.Close();
        }


        public TextBox txthistoria = new TextBox();
        public TextBox txtatecodigo = new TextBox();
        public TextBox txtaseguradora = new TextBox();
        Form002MSP consultaExterna = new Form002MSP();
        SIGNOSVITALES_CONSULTAEXTERNA sign = new SIGNOSVITALES_CONSULTAEXTERNA();
        private void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            DataTable paciente = new DataTable();
            //frmAyudaPacientesFacturacion ayuda = new frmAyudaPacientesFacturacion();
            //ayuda.campoPadre = txthistoria;
            //ayuda.campoAtencion = txtatecodigo;
            //ayuda.campoAseguradora = txtaseguradora;
            //ayuda.ShowDialog();
            Emergencia.frm_AyudaPacientes ayuda = new Emergencia.frm_AyudaPacientes(emergecia);
            ayuda.campoPadre = txthistoria;
            ayuda.campoAtencion = txtatecodigo;
            //ayuda.campoFecha = fechaNacimimiento;
            //ayuda.campoFechaAtencion = fechaAtencion;
            //ayuda.campoId = id;
            ayuda.triaje = true;
            //ayuda.campoAseguradora = txtaseguradora;
            ayuda.ShowDialog();
            if (txthistoria.Text != "")
            {
                paciente = NegConsultaExterna.RecuperaPaciente(Convert.ToInt64(txtatecodigo.Text));
                DataTable signos = new DataTable();
                signos = NegConsultaExterna.RecuperaSignos(Convert.ToInt64(txtatecodigo.Text));
                SIGNOSVITALES_CONSULTAEXTERNA sign = NegConsultaExterna.signoscitalesCex(Convert.ToInt64(txtatecodigo.Text));
                if (sign != null)
                {
                    txtTemperatura.Text = Convert.ToString(sign.T_Bucal);
                    txtTempAx.Text = Convert.ToString(sign.T_Axilar);
                    txtPresionArteria1.Text = Convert.ToInt32(sign.Presion1).ToString();
                    txtPresionArteria2.Text = Convert.ToInt32(sign.Presion2).ToString();
                    txtPulso.Text = Convert.ToInt32(sign.F_Cardiaca).ToString();
                    txtFrecuenciaRespiratoria.Text = Convert.ToString(sign.F_Respiratoria);
                    cmb_Ocular.Text = sign.Ocular.ToString();
                    cmb_Verbal.Text = sign.Verbal.ToString();
                    cmb_Motora.Text = sign.Motora.ToString();
                    txtGlasgow.Text = Convert.ToString(sign.Glasgow);
                    txtPeso.Text = Convert.ToString(sign.PesoKG);
                    txtTalla.Text = Convert.ToString(sign.TallaM);
                    txtIndice.Text = Convert.ToString(sign.Ind_Masa);
                    txtPerimetro.Text = Convert.ToString(sign.PerimetroA);
                    txtHemoglobina.Text = Convert.ToString(sign.Hemoglobina);
                    txtGlucosa.Text = Convert.ToString(sign.Glucosa_Capilar);
                    txtPulsioximetria.Text = Convert.ToInt32(sign.S_Oxigeno).ToString();
                    if (sign.Diametro_Der.ToString() == "-1")
                        txtDiametroDer.Text = "";
                    else
                        txtDiametroDer.Text = sign.Diametro_Der.ToString();
                    txtReaccionDer.Text = sign.Reaccion_Der.ToString();
                    if (sign.Diametro_Iz.ToString() == "-1")
                        txtDiametroIz.Text = "";
                    else
                        txtDiametroIz.Text = sign.Diametro_Iz.ToString();
                    txtReaccionIz.Text = sign.Reaccion_Iz.ToString();
                }
                ////CONSULTO SI PACIENTE YA TIENE CONSULTA EXTERNA
                //consultaExterna = NegConsultaExterna.PacienteExisteCxE(txtatecodigo.Text.Trim());
                //if (consultaExterna != null)
                //    CargarPacienteExiste();

                //if (signos.Rows.Count > 0)
                //{
                //    string enteros = signos.Rows[0][3].ToString();
                //    string[] final = enteros.Split('.');
                //    txtPresionArteria1.Text = final[0];
                //    enteros = signos.Rows[0][4].ToString();
                //    final = enteros.Split('.');
                //    txtPresionArteria2.Text = final[0];
                //    enteros = signos.Rows[0][5].ToString();
                //    final = enteros.Split('.');
                //    txtPulso.Text = final[0];
                //    enteros = signos.Rows[0][6].ToString();
                //    final = enteros.Split('.');
                //    txtFrecuenciaRespiratoria.Text = final[0];
                //    enteros = signos.Rows[0][7].ToString();
                //    final = enteros.Split('.');
                //    txtTemperatura.Text = final[0];
                //    final = enteros.Split('.');
                //    txtTemperatura.Text = signos.Rows[0][8].ToString();
                //    enteros = signos.Rows[0][10].ToString();
                //    final = enteros.Split('.');
                //    txtPeso.Text = final[0];
                //    enteros = signos.Rows[0][11].ToString();
                //    final = enteros.Split('.');
                //    txtTalla.Text = signos.Rows[0][11].ToString();
                //}
                lblHistoria.Text = txthistoria.Text;
                lblAteCodigo.Text = txtatecodigo.Text;
                //lblAseguradora.Text = txtaseguradora.Text;
                lblNombre.Text = paciente.Rows[0][0].ToString();
                lblApellido.Text = paciente.Rows[0][1].ToString();
                lblEdad.Text = paciente.Rows[0][2].ToString();
                if (paciente.Rows[0][3].ToString().Trim() == "M")
                {
                    lblSexo.Text = "Masculino";
                }
                else
                {
                    lblSexo.Text = "Femenino";
                }
                CargarPaciente();
                P_Central.Visible = true;
                P_Datos.Visible = true;
                btnBuscarPaciente.Visible = false;
                btnGuarda.Visible = true;
                btnNuevo.Visible = true;
            }
        }
        public void CargarPaciente() //Cambios Edgar 20210203 antes no rescataba la informacion.
        {
            try
            {
                btnImprimir.Visible = true;
                btnGuarda.Visible = false;
                DataTable TPaciente = new DataTable(); //Trae el paciente con todas las consultas externas
                DataTable DatosPaciente = new DataTable(); //Contiene los datos del paciente
                TPaciente = NegConsultaExterna.ExistePaciente(Convert.ToInt64(txtatecodigo.Text));

                if (TPaciente.Rows.Count > 0)
                {
                    frm_PacientesConsultaExterna x = new frm_PacientesConsultaExterna();
                    x.Pacientes = TPaciente;
                    x.ShowDialog();
                    if (x.codigoConsultaExterna != "")
                    {
                        conExter = NegConsultaExterna.DatosPaciente(Convert.ToInt32(x.codigoConsultaExterna));
                        txtMotivo.Text = conExter.Motivo;
                        //cardiopatia
                        if (conExter.cardiopatiaP == "X")
                        {
                            chbCardiopatiaP.Checked = true;
                        }
                        else
                            chbCardiopatiaP.Checked = false;
                        //diabetes
                        if (conExter.endocrinoP == "X")
                            chbMetabolicoP.Checked = true;
                        else
                            chbMetabolicoP.Checked = false;
                        //Vascular
                        if (conExter.vascularP == "X")
                            chbVascularP.Checked = true;
                        else
                            chbVascularP.Checked = false;
                        //HiperTension
                        if (conExter.hipertencionP == "X")
                            chbHipertensionP.Checked = true;
                        else
                            chbHipertensionP.Checked = false;
                        //Cancer
                        if (conExter.cancerP == "X")
                            chbCancerP.Checked = true;
                        else
                            chbCancerP.Checked = false;
                        //tuberculosis
                        if (conExter.tuberculosisP == "X")
                            chbTuberculosisP.Checked = true;
                        else
                            chbTuberculosisP.Checked = false;
                        //mental
                        if (conExter.mentalP == "X")
                            chbMentalP.Checked = true;
                        else
                            chbMentalP.Checked = false;
                        //infecciosa
                        if (conExter.infecciosaP == "X")
                            chbInfecciosa.Checked = true;
                        else
                            chbInfecciosa.Checked = false;
                        //malformacion
                        if (conExter.malformacionP == "X")
                            chbFormacionP.Checked = true;
                        else
                            chbFormacionP.Checked = false;
                        //otro
                        if (conExter.otroP == "X")
                            chbOtroP.Checked = true;
                        else
                            chbOtroP.Checked = false;
                        txtAntecedentesPersonales.Text = conExter.AntecedentesPersonales;
                        //cardiopatia
                        if (conExter.Cardiopatia == "X")
                        {
                            chbCardiopatia.Checked = true;
                        }
                        else
                            chbCardiopatia.Checked = false;
                        //diabetes
                        if (conExter.endocrino == "X")
                            chbMetabolico.Checked = true;
                        else
                            chbMetabolico.Checked = false;
                        //Vascular
                        if (conExter.Vascular == "X")
                            chbVascular.Checked = true;
                        else
                            chbVascular.Checked = false;
                        //HiperTension
                        if (conExter.Hipertencion == "X")
                            chbHiperT.Checked = true;
                        else
                            chbHiperT.Checked = false;
                        //Cancer
                        if (conExter.Cancer == "X")
                            chbCancer.Checked = true;
                        else
                            chbCancer.Checked = false;
                        //tuberculosis
                        if (conExter.tuberculosis == "X")
                            chbTuberculosis.Checked = true;
                        else
                            chbTuberculosis.Checked = false;
                        //mental
                        if (conExter.mental == "X")
                            chbMental.Checked = true;
                        else
                            chbMental.Checked = false;
                        //infecciosa
                        if (conExter.infecciosa == "X")
                            chbInfeccionsa.Checked = true;
                        else
                            chbInfeccionsa.Checked = false;
                        //malformacion
                        if (conExter.malformacion == "X")
                            chbMalFormado.Checked = true;
                        else
                            chbMalFormado.Checked = false;
                        //otro
                        if (conExter.otro == "X")
                            chbOtro.Checked = true;
                        else
                            chbOtro.Checked = false;
                        //antecedentes familiares
                        txtAntecedentesFamiliares.Text = conExter.antecedentesFamiliares;
                        //enfermedad actual
                        txtEnfermedadProblema.Text = conExter.enfermedadActual;
                        //piel
                        if (conExter.piel == "X")
                            chb5cp11.Checked = true;
                        else
                            chb5cp11.Checked = false;
                        //sentidos
                        if (conExter.sentidos == "X")
                            chb5cp1.Checked = true;
                        else
                            chb5cp1.Checked = false;
                        //respiratorio
                        if (conExter.respiratorio == "X")
                            chb5cp2.Checked = true;
                        else
                            chb5cp2.Checked = false;
                        //CardioVascular
                        if (conExter.cardioVascular == "X")
                            chb5cp3.Checked = true;
                        else
                            chb5cp3.Checked = false;
                        //digestivo
                        //if (DatosPaciente.Rows[0][27].ToString() == "X")
                        //    chb5cp4.Checked = true;
                        //else
                        //    chb5cp4.Checked = false;
                        //genital
                        if (conExter.genital == "X")
                            chb5cp5.Checked = true;
                        else
                            chb5cp5.Checked = false;
                        //urinario
                        if (conExter.urinario == "X")
                            chb5cp6.Checked = true;
                        else
                            chb5cp6.Checked = false;
                        //esqueletico
                        if (conExter.esqueletico == "X")
                            chb5cp7.Checked = true;
                        else
                            chb5cp7.Checked = false;
                        //endocrino
                        if (conExter.endocrino == "X")
                            chb5cp8.Checked = true;
                        else
                            chb5cp8.Checked = false;
                        //linfatico
                        if (conExter.linfatico == "X")
                            chb5cp9.Checked = true;
                        else
                            chb5cp9.Checked = false;
                        //nervioso
                        if (conExter.nervioso == "X")
                            chb5cp10.Checked = true;
                        else
                            chb5cp10.Checked = false;
                        //revision actual
                        txtRevisionActual.Text = conExter.revisionactual;
                        dtpMedicion.Value = Convert.ToDateTime(conExter.fechamedicion);
                        //txtTemperatura.Text = conExter.temperatura;
                        //txtPresionArteria1.Text = conExter.presion1;
                        //txtPresionArteria2.Text = conExter.presion2;
                        //txtPulso.Text = conExter.pulso;
                        //txtFrecuenciaRespiratoria.Text = conExter.frecuenciaRespiratoria;
                        //txtPeso.Text = conExter.peso;
                        //txtTalla.Text = conExter.talla;
                        //cabeza
                        if (conExter.cabeza == "X")
                            chb7cp1.Checked = true;
                        else
                            chb7cp1.Checked = false;
                        //cuello
                        if (conExter.cuello == "X")
                            chb7cp2.Checked = true;
                        else
                            chb7cp2.Checked = false;
                        //torax
                        if (conExter.torax == "X")
                            chb7cp3.Checked = true;
                        else
                            chb7cp3.Checked = false;
                        //abdomen
                        if (conExter.abdomen == "X")
                            chb7cp4.Checked = true;
                        else
                            chb7cp4.Checked = false;
                        //pelvis
                        if (conExter.pelvis == "X")
                            chb7cp5.Checked = true;
                        else
                            chb7cp5.Checked = false;
                        //extremidades
                        if (conExter.extremidades == "X")
                            chb7cp6.Checked = true;
                        else
                            chb7cp5.Checked = false;
                        if (conExter.rPiel == "X")
                            chbPiel.Checked = true;
                        else
                            chbPiel.Checked = false;

                        if (conExter.rPiel == "X")
                            chbPiel.Checked = true;
                        else
                            chbPiel.Checked = false;
                        if (conExter.rOjos == "X")
                            chbOjos.Checked = true;
                        else
                            chbOjos.Checked = false;
                        if (conExter.rOidos == "X")
                            chbOidos.Checked = true;
                        else
                            chbOidos.Checked = false;
                        if (conExter.rNariz == "X")
                            chbNariz.Checked = true;
                        else
                            chbNariz.Checked = false;
                        if (conExter.rBoca == "X")
                            chbBoca.Checked = true;
                        else
                            chbBoca.Checked = false;
                        if (conExter.rOrofaringe == "X")
                            chbOrofaringe.Checked = true;
                        else
                            chbOrofaringe.Checked = false;
                        if (conExter.rAxilas == "X")
                            chbAxilas.Checked = true;
                        else
                            chbAxilas.Checked = false;
                        if (conExter.rColumna == "X")
                            chbColumna.Checked = true;
                        else
                            chbColumna.Checked = false;
                        if (conExter.rIngle == "X")
                            chbIngle.Checked = true;
                        else
                            chbIngle.Checked = false;
                        if (conExter.rInferior == "X")
                            chbInferior.Checked = true;
                        else
                            chbInferior.Checked = false;
                        if (conExter.sSentidos == "X")
                            chbSentidos.Checked = true;
                        else
                            chbSentidos.Checked = false;
                        if (conExter.sRespiratorio == "X")
                            chbRespiratorio.Checked = true;
                        else
                            chbRespiratorio.Checked = false;
                        if (conExter.sCardio == "X")
                            chbBascular.Checked = true;
                        else
                            chbBascular.Checked = false;
                        if (conExter.sDigestivo == "X")
                            chbDigestivo.Checked = true;
                        else
                            chbDigestivo.Checked = false;
                        if (conExter.sGenital == "X")
                            chbGenital.Checked = true;
                        else
                            chbGenital.Checked = false;
                        if (conExter.sUrinario == "X")
                            chbUrinario.Checked = true;
                        else
                            chbUrinario.Checked = false;
                        if (conExter.sMusculo == "X")
                            chbMusculo.Checked = true;
                        else
                            chbMusculo.Checked = false;
                        if (conExter.sEndocrino == "X")
                            chbEndocrino.Checked = true;
                        else
                            chbEndocrino.Checked = false;
                        if (conExter.sLimfatico == "X")
                            chbHemo.Checked = true;
                        else
                            chbHemo.Checked = false;
                        if (conExter.sNeurologico == "X")
                            chbNeurologico.Checked = true;
                        else
                            chbNeurologico.Checked = false;
                        //examen fisico
                        txtExamenFisico.Text = conExter.examenFisico;
                        //Cie10
                        txtDiagnostico1.Text = conExter.diagnostico1;
                        txtCieDiagnostico1.Text = conExter.diagnostico1cie;
                        if (conExter.diagnostico1pre == "X")
                            chbPre1.Checked = true;
                        else
                            chbPre1.Checked = false;

                        txtDiagnostico2.Text = conExter.diagnostico2;
                        txtCieDiagnostico2.Text = conExter.diagnostico2cie;
                        if (conExter.diagnostico2pre == "X")
                            chbPre2.Checked = true;
                        else
                            chbPre2.Checked = false;


                        txtDiagnostico3.Text = conExter.diagnostico3;
                        txtCieDiagnostico3.Text = conExter.diagnostico3cie;
                        if (conExter.diagnostico3pre == "X")
                            chbPre3.Checked = true;
                        else
                            chbPre3.Checked = false;


                        txtDiagnostico4.Text = conExter.diagnostico4;
                        txtCieDiagnostico4.Text = conExter.diagnostico4cie;
                        if (conExter.diagnostico4pre == "X")
                            chbPre4.Checked = true;
                        else
                            chbPre4.Checked = false;

                        txtDiagnostico5.Text = conExter.diagnostico5;
                        txtCieDiagnostico5.Text = conExter.diagnostico5cie;
                        if (conExter.diagnostico5pre == "X")
                            chbPre5.Checked = true;
                        else
                            chbPre5.Checked = false;

                        txtDiagnostico6.Text = conExter.diagnostico6;
                        txtCieDiagnostico6.Text = conExter.diagnostico6cie;
                        if (conExter.diagnostico6pre == "X")
                            chbPre6.Checked = true;
                        else
                            chbPre6.Checked = false;
                        //tratamiento
                        txtPlanesTratamiento.Text = conExter.planesTratamiento;
                        //txtEvolucion.Text = DatosPaciente.Rows[0][80].ToString();
                        //txtindicaciones.Text = DatosPaciente.Rows[0][81].ToString();
                        dtp_fechaAltaEmerencia.Value = Convert.ToDateTime(conExter.fecha);
                        txt_horaAltaEmerencia.Text = Convert.ToDateTime(conExter.fecha).ToShortTimeString();
                        txt_profesionalEmergencia.Text = conExter.dr;
                        txt_CodMSPE.Text = conExter.codigo;
                    }
                    nuevaConsulta = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtCieDiagnostico1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico1.Text = busqueda.codigo;
                    chbPre1.Checked = true;
                }

            }
        }
        private void txtCieDiagnostico2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico2.Text = busqueda.codigo;
                    chbPre2.Checked = true;
                }

            }
        }

        private void txtCieDiagnostico3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico3.Text = busqueda.codigo;
                    chbPre3.Checked = true;
                }

            }
        }


        private void txtCieDiagnostico4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {

                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {

                    txtCieDiagnostico4.Text = busqueda.codigo;
                    chbPre4.Checked = true;
                }

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Usted va generar una nueva consulta externa. ¿Desea Continuar?", "HIS3000", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                nuevaConsulta = true;
                limpiarCampos();
                P_Central.Visible = false;
                P_Datos.Visible = false;
                btnNuevo.Visible = false;
                btnBuscarPaciente.Visible = false;
            }
        }

        private void gridPrescripciones_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (band == false)
                {
                    if (e.ColumnIndex == gridPrescripciones.Columns["PRES_ESTADO"].Index)
                    {
                        DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)gridPrescripciones.Rows[e.RowIndex].Cells["PRES_ESTADO"];
                        if (chkCell.Value != null)
                        {
                            if ((bool)chkCell.Value == true)
                            {
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = false;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].Value = DateTime.Now;
                            }
                            else
                            {
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].Value = string.Empty;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = true;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].Value = null;
                            }
                        }
                        else
                        {
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].Value = string.Empty;
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = true;
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].Value = null;
                        }

                    }
                    else
                    {
                        gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                        gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = false;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnreceta_Click(object sender, EventArgs e)
        {
            if (NegParametros.ParametroReceta())
            {
                frmRecetaNew x = new frmRecetaNew();
                x.Show();
            }
            else
            {
                His.Formulario.frm_RecetaMedica x = new His.Formulario.frm_RecetaMedica();
                x.Show();
            }
            //frmRecetaNew x = new frmRecetaNew();
            //x.ShowDialog();
        }

        private void btnCertificado_Click(object sender, EventArgs e)
        {
            frm_Certificados x = new frm_Certificados();
            x.ShowDialog();
        }

        public bool ValidarCie10(string cie10)
        {
            if (txtCieDiagnostico1.Text.Trim() == cie10)
                return false;
            if (txtCieDiagnostico2.Text.Trim() == cie10)
                return false;
            if (txtCieDiagnostico3.Text.Trim() == cie10)
                return false;
            if (txtCieDiagnostico4.Text.Trim() == cie10)
                return false;
            else
                return true;
        }
        private void txtDiagnostico1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {
                    if (ValidarCie10(busqueda.codigo))
                    {
                        txtCieDiagnostico1.Text = busqueda.codigo;
                        txtDiagnostico1.Text = busqueda.resultado;
                        chbPre1.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("Cie10 ya ha sido agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtCieDiagnostico1.Text = "";
                    txtDiagnostico1.Text = "";
                    chbPre1.Checked = false;
                    chbDef1.Checked = false;
                }
            }
        }

        private void txtDiagnostico2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {
                    if (ValidarCie10(busqueda.codigo))
                    {
                        txtCieDiagnostico2.Text = busqueda.codigo;
                        txtDiagnostico2.Text = busqueda.resultado;
                        chbPre2.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("Cie10 ya ha sido agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtCieDiagnostico2.Text = "";
                    txtDiagnostico2.Text = "";
                    chbPre2.Checked = false;
                    chbDef2.Checked = false;
                }
            }
        }

        private void txtDiagnostico3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {
                    if (ValidarCie10(busqueda.codigo))
                    {
                        txtCieDiagnostico3.Text = busqueda.codigo;
                        txtDiagnostico3.Text = busqueda.resultado;
                        chbPre3.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("Cie10 ya ha sido agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtCieDiagnostico3.Text = "";
                    txtDiagnostico3.Text = "";
                    chbPre3.Checked = false;
                    chbDef3.Checked = false;
                }
            }
        }

        private void txtDiagnostico4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {
                    if (ValidarCie10(busqueda.codigo))
                    {
                        txtCieDiagnostico4.Text = busqueda.codigo;
                        txtDiagnostico4.Text = busqueda.resultado;
                        chbPre4.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("Cie10 ya ha sido agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtCieDiagnostico4.Text = "";
                    txtDiagnostico4.Text = "";
                    chbPre4.Checked = false;
                    chbDef4.Checked = false;
                }
            }
        }
        public void limpiarCampos()
        {
            medico = new MEDICOS();
            txt_Medico.Text = "";
            txtMotivo.Text = "";
            txtAntecedentesPersonales.Text = "";
            if (chb5cp1.Checked)
                chb5cp1.Checked = false;
            if (chb5cp10.Checked)
                chb5cp10.Checked = false;
            if (chb5cp2.Checked)
                chb5cp2.Checked = false;
            if (chb5cp3.Checked)
                chb5cp3.Enabled = false;
            if (chb5cp4.Checked)
                chb5cp4.Checked = false;
            if (chb5cp5.Checked)
                chb5cp5.Checked = false;
            if (chb5cp6.Checked)
                chb5cp6.Checked = false;
            if (chb5cp7.Checked)
                chb5cp7.Checked = false;
            if (chb5cp8.Checked)
                chb5cp8.Checked = false;
            if (chb5cp9.Checked)
                chb5cp9.Checked = false;
            if (chbCardiopatia.Checked)
                chbCardiopatia.Checked = false;
            if (chbDiabetes.Checked)
                chbDiabetes.Checked = false;
            if (chbVascular.Checked)
                chbVascular.Checked = false;
            if (chbHiperT.Checked)
                chbHiperT.Checked = false;
            if (chbCancer.Checked)
                chbCancer.Checked = false;
            if (chbTuberculosis.Checked)
                chbTuberculosis.Checked = false;
            if (chbMental.Checked)
                chbMental.Checked = false;
            if (chbInfeccionsa.Checked)
                chbInfeccionsa.Checked = false;
            if (chbMalFormado.Checked)
                chbMalFormado.Checked = false;
            if (chbOtro.Checked)
                chbOtro.Checked = false;
            txtAntecedentesFamiliares.Text = "";
            txtEnfermedadProblema.Text = "";
            txtRevisionActual.Text = "";
            txtTemperatura.Text = "";
            txtPresionArteria1.Text = "";
            txtPresionArteria2.Text = "";
            txtPulso.Text = "";
            txtFrecuenciaRespiratoria.Text = "";
            txtTalla.Text = "1";
            txtPeso.Text = "1";
            txtExamenFisico.Text = "";
            txtDiagnostico1.Text = "";
            txtDiagnostico2.Text = "";
            txtDiagnostico3.Text = "";
            txtDiagnostico4.Text = "";
            txtCieDiagnostico1.Text = "";
            txtCieDiagnostico2.Text = "";
            txtCieDiagnostico3.Text = "";
            txtCieDiagnostico4.Text = "";

            txtPlanesTratamiento.Text = "";
            txtEvolucion.Text = "";
            txtindicaciones.Text = "";
            txt_horaAltaEmerencia.Text = DateTime.Now.ToString("hh:mm");
        }
        private void Consulta_Load(object sender, EventArgs e)
        {
            HabilitarBotones(true, false, false, false, false, false, false, false, false, false, false);
            List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(His.Entidades.Clases.Sesion.codUsuario);
            foreach (var item in perfilUsuario)
            {
                List<ACCESO_OPCIONES> accop = NegUtilitarios.ListaAccesoOpcionesPorPerfil(item.ID_PERFIL, 7);
                foreach (var items in accop)
                {
                    if (items.ID_ACCESO == 71110)// se cambia del perfil  29 a opcion 71110// Mario Valencia 14/11/2023 // cambio en seguridades.
                    {
                        mushuñan = true;
                        Int16 AreaUsuario = 1;
                        DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario));
                        bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
                        if (parse)
                        {
                            switch (AreaUsuario)
                            {
                                case 2:
                                    //button1.Visible = true;
                                    break;
                                case 3:
                                    button1.Visible = true;
                                    txt_profesionalEmergencia.Text = "";
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable paciente = new DataTable();
            if (mushuñan)
                emergecia = true;
            Emergencia.frm_AyudaPacientes ayuda = new Emergencia.frm_AyudaPacientes(emergecia);
            ayuda.campoPadre = txthistoria;
            ayuda.campoAtencion = txtatecodigo;
            ayuda.triaje = false;
            ayuda.mushunia = mushuñan;
            if (Sesion.codDepartamento == 1)
                ayuda.sistemas = true;
            ayuda.ShowDialog();
            ATENCIONES atencion = NegAtenciones.RecuepraAtencionNumeroAtencion(Convert.ToInt64(txtatecodigo.Text));
            Int32 med_codigo = Convert.ToInt32(atencion.MEDICOSReference.EntityKey.EntityKeyValues[0].Value);
            medico = NegMedicos.recuperarMedico(med_codigo);
            txt_Medico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            if (txthistoria.Text != "")
            {
                subsecuente = NegAtenciones.RecuperaAtencionSub(Convert.ToInt64(txtatecodigo.Text));
                DataTable signos = new DataTable();
                if (subsecuente != null)
                {
                    paciente = NegConsultaExterna.RecuperaPaciente(subsecuente.ate_codigo_principal);
                    signos = NegConsultaExterna.RecuperaSignos(subsecuente.ate_codigo_principal);
                    //CONSULTO SI PACIENTE YA TIENE CONSULTA EXTERNA
                    consultaExterna = NegConsultaExterna.PacienteExisteCxE(Convert.ToString(subsecuente.ate_codigo_principal));
                    if (!NegConsultaExterna.PacienteCerradaCxE(subsecuente.ate_codigo_subsecuente))
                    {
                        HabilitarBotones(false, false, true, true, true, true, true, true, false, true, true);

                    }
                    else
                    {
                        HabilitarBotones(false, false, false, true, true, true, true, true, true, false, true);

                    }
                    if (consultaExterna != null)
                        CargarPacienteExiste(subsecuente.ate_codigo_principal);
                    else
                    {
                        HabilitarBotones(false, true, false, false, false, false, false, false, false, false, false);
                        nuevaConsulta = true;
                        //P_Central.Enabled = true;
                        grupos(true);
                    }
                }
                else
                {
                    paciente = NegConsultaExterna.RecuperaPaciente(Convert.ToInt64(txtatecodigo.Text));
                    signos = NegConsultaExterna.RecuperaSignos(Convert.ToInt64(txtatecodigo.Text));
                    //CONSULTO SI PACIENTE YA TIENE CONSULTA EXTERNA
                    consultaExterna = NegConsultaExterna.PacienteExisteCxE(txtatecodigo.Text);
                    if (consultaExterna != null)
                    {
                        CargarPacienteExiste();

                        if (!NegConsultaExterna.PacienteCerradaCxE(Convert.ToInt64(txtatecodigo.Text)))
                        {
                            HabilitarBotones(false, false, true, true, true, true, true, true, false, true, true);
                            grupos(false);
                        }
                        else
                        {
                            HabilitarBotones(false, false, false, true, true, true, true, true, true, false, true);
                            grupos(false);
                        }
                    }
                    else
                    {
                        HabilitarBotones(false, true, false, false, false, false, false, false, false, false, false);
                        nuevaConsulta = true;
                        //P_Central.Enabled = true;
                        grupos(true);
                    }
                }
                SIGNOSVITALES_CONSULTAEXTERNA sign = NegConsultaExterna.signoscitalesCex(Convert.ToInt64(txtatecodigo.Text));
                if (sign != null)
                {
                    txtTemperatura.Text = Convert.ToString(sign.T_Bucal);
                    txtTempAx.Text = Convert.ToString(sign.T_Axilar);
                    txtPresionArteria1.Text = Convert.ToInt32(sign.Presion1).ToString();
                    txtPresionArteria2.Text = Convert.ToInt32(sign.Presion2).ToString();
                    txtPulso.Text = Convert.ToInt32(sign.F_Cardiaca).ToString();
                    txtFrecuenciaRespiratoria.Text = Convert.ToString(sign.F_Respiratoria);
                    cmb_Ocular.Text = sign.Ocular.ToString();
                    cmb_Verbal.Text = sign.Verbal.ToString();
                    cmb_Motora.Text = sign.Motora.ToString();
                    txtGlasgow.Text = Convert.ToString(sign.Glasgow);
                    txtPeso.Text = Convert.ToString(sign.PesoKG);
                    txtTalla.Text = Convert.ToString(sign.TallaM);
                    txtIndice.Text = Convert.ToString(sign.Ind_Masa);
                    txtPerimetro.Text = Convert.ToString(sign.PerimetroA);
                    txtHemoglobina.Text = Convert.ToString(sign.Hemoglobina);
                    txtGlucosa.Text = Convert.ToString(sign.Glucosa_Capilar);
                    txtPulsioximetria.Text = Convert.ToInt32(sign.S_Oxigeno).ToString();
                    if (sign.Diametro_Der.ToString() == "-1")
                        txtDiametroDer.Text = "";
                    else
                        txtDiametroDer.Text = sign.Diametro_Der.ToString();
                    txtReaccionDer.Text = sign.Reaccion_Der.ToString();
                    if (sign.Diametro_Iz.ToString() == "-1")
                        txtDiametroIz.Text = "";
                    else
                        txtDiametroIz.Text = sign.Diametro_Iz.ToString();
                    txtReaccionIz.Text = sign.Reaccion_Iz.ToString();
                }
                //if (signos.Rows.Count > 0)
                //{
                //    string enteros = signos.Rows[0][3].ToString();
                //    string[] final = enteros.Split('.');
                //    txtPresionArteria1.Text = final[0];
                //    enteros = signos.Rows[0][4].ToString();
                //    final = enteros.Split('.');
                //    txtPresionArteria2.Text = final[0];
                //    //enteros = signos.Rows[0][5].ToString();
                //    //final = enteros.Split('.');
                //    txtPulso.Text = signos.Rows[0][5].ToString();
                //    //enteros = signos.Rows[0][6].ToString();
                //    //final = enteros.Split('.');
                //    txtFrecuenciaRespiratoria.Text = signos.Rows[0][6].ToString();
                //    txtFrecuenciaCardiaca.Text = signos.Rows[0][5].ToString();
                //    txtSaturaOxigeno.Text = signos.Rows[0][9].ToString();
                //    txtIndice.Text = signos.Rows[0][12].ToString();
                //    txtGlicemiaCapilar.Text = signos.Rows[0][14].ToString();
                //    txtGlasgow.Text = signos.Rows[0][15].ToString();
                //    cmb_Ocular.Text = signos.Rows[0][16].ToString();
                //    cmb_Ocular.Text = signos.Rows[0][16].ToString();
                //    cmb_Verbal.Text = signos.Rows[0][17].ToString();
                //    cmb_Motora.Text = signos.Rows[0][18].ToString();
                //    txtDiametroDer.Text = signos.Rows[0][19].ToString();
                //    txtReaccionDer.Text = signos.Rows[0][20].ToString();
                //    txtDiametroIz.Text = signos.Rows[0][21].ToString();
                //    txtReaccionIz.Text = signos.Rows[0][22].ToString();
                //    //txtPerimetro.Text = signos.Rows[0][23].ToString();
                //    //txtHemoglobina.Text = signos.Rows[0][24].ToString();
                //    //txtGlucosa.Text = signos.Rows[0][25].ToString();
                //    //txtPulsioximetria.Text = signos.Rows[0][26].ToString();

                //    //enteros = signos.Rows[0][7].ToString();
                //    //final = enteros.Split('.');
                //    txtTemperatura.Text = signos.Rows[0][7].ToString();
                //    final = enteros.Split('.');
                //    txtTemperatura.Text = signos.Rows[0][8].ToString();
                //    //enteros = signos.Rows[0][10].ToString();
                //    //final = enteros.Split('.');
                //    txtPeso.Text = signos.Rows[0][10].ToString();
                //    enteros = signos.Rows[0][11].ToString();
                //    final = enteros.Split('.');
                //    txtTalla.Text = signos.Rows[0][11].ToString();
                //    txtPerimetro.Text = signos.Rows[0]["PerimetroA"].ToString();
                //    txtHemoglobina.Text = signos.Rows[0]["Hemoglobina"].ToString();
                //    txtGlucosa.Text = signos.Rows[0]["Glucosa"].ToString();
                //    txtPulsioximetria.Text = signos.Rows[0]["Pulsioximetria"].ToString();
                //    txtPulso.Text = signos.Rows[0]["Pulsioximetria"].ToString();
                //}
                lblHistoria.Text = txthistoria.Text;
                lblAteCodigo.Text = txtatecodigo.Text;
                lblNombre.Text = paciente.Rows[0][0].ToString();
                lblApellido.Text = paciente.Rows[0][1].ToString();
                lblEdad.Text = paciente.Rows[0][2].ToString();

                if (paciente.Rows[0][3].ToString().Trim() == "M")
                {
                    lblSexo.Text = "Masculino";
                }
                else
                {
                    lblSexo.Text = "Femenino";
                }
                //CargarPaciente(); //aqui se puede ver las n consultas externas del paciente
                P_Central.Visible = true;
                P_Datos.Visible = true;
                //btnBuscarPaciente.Visible = false;
                //btnGuarda.Visible = true;
                //btnNuevo.Visible = true;

            }
        }
        public bool mushuñan = false;
        public void imprimir()
        {
            subsecuente = NegAtenciones.RecuperaAtencionSub(Convert.ToInt64(txtatecodigo.Text));
            string sub = "";
            string pri = "";
            if (subsecuente != null)
            {
                sub = "x";
                consultaExterna = NegConsultaExterna.PacienteExisteCxE(Convert.ToString(subsecuente.ate_codigo_principal));
                sign = NegConsultaExterna.signoscitalesCex(subsecuente.ate_codigo_principal);
                if (sign == null)
                {
                    MessageBox.Show("No se puede imprimir hasta haber  completado la hora de triaje.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                pri = "x";
                consultaExterna = NegConsultaExterna.PacienteExisteCxE(txtatecodigo.Text.Trim());
                sign = NegConsultaExterna.signoscitalesCex(Convert.ToInt64(txtatecodigo.Text.Trim()));
                if (sign == null)
                {
                    MessageBox.Show("No se puede imprimir hasta haber  completado la hora de triaje.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            MEDICOS mimprime = NegMedicos.recuperarMedico(Convert.ToInt32(consultaExterna.MED_CODIGO));
            MEDICOS musuario = NegMedicos.recuperarMedicoID_Usuario(Convert.ToInt32(consultaExterna.ID_USUARIO));
            double tot1 = 0;
            try
            {
                tot1 = sign.Ocular + sign.Verbal + sign.Motora;
            }
            catch (Exception ex)
            {
                //throw;
            }
            SUCURSALES sucursal = new SUCURSALES();
            string logo = "";
            string empresa = "";
            if (mushuñan)
            {
                Int32 ingreso = NegTipoIngreso.RecuperarporAtencion(Convert.ToInt64(txtatecodigo.Text));
                switch (ingreso)
                {
                    case 10:
                        logo = NegUtilitarios.RutaLogo("Mushuñan");
                        empresa = "SANTA CATALINA DE SENA";
                        break;
                    case 12:
                        logo = NegUtilitarios.RutaLogo("BrigadaMedica");
                        empresa = "BRIGADAS MEDICAS";
                        break;
                    default:
                        logo = NegUtilitarios.RutaLogo("General");
                        empresa = Sesion.nomEmpresa;
                        break;
                }
            }
            else
            {
                logo = NegUtilitarios.RutaLogo("General");
                empresa = Sesion.nomEmpresa;
            }
            if (consultaExterna != null)
            {
                if (consultaExterna.Sexo == "Masculino")
                {
                    datos.sexoPaciente = "M";
                }
                else
                    datos.sexoPaciente = "F";

                PACIENTES pacien = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(txtatecodigo.Text));
                ATENCIONES atencion = NegAtenciones.RecuepraAtencionNumeroAtencion(Convert.ToInt32(txtatecodigo.Text));
                if (NegParametros.ParametroFormularios())
                    datos.historiaClinica = pacien.PAC_IDENTIFICACION;
                else
                    datos.historiaClinica = consultaExterna.Historia;

                NegCertificadoMedico neg = new NegCertificadoMedico();
                //HCU_form002MSP Ds = new HCU_form002MSP();
                string patron = @"^\d+";
                His.Formulario.HCU_form002MSP Ds = new His.Formulario.HCU_form002MSP();
                //His.Formulario.FrmConsultaExterna002 Ds = new His.Formulario.FrmConsultaExterna002();        //se comenta para trabajar con el dataset 2021//mario valencia///31/01/2024
                DataRow tabla;
                tabla = Ds.Tables["Form002MSP"].NewRow();
                //Ds.Tables[0].Rows.Add
                //    (new object[]  /// se comenta para cambiar a un dato row //Cristian Ruiz/31/01/2024
                //    {

                //consultaExterna.Nombre.ToString(),
                //consultaExterna.Apellido.ToString(),

                tabla["Nombre"] = pacien.PAC_NOMBRE1;
                tabla["Apellido"] = pacien.PAC_APELLIDO_PATERNO;
                tabla["Sexo"] = datos.sexoPaciente.ToString();
                tabla["Edad"] = Regex.Match(consultaExterna.Edad.ToString(), patron);
                tabla["Historia"] = pacien.PAC_IDENTIFICACION;
                tabla["AteCodigo"] = pacien.PAC_HISTORIA_CLINICA + "-" + atencion.ATE_NUMERO_ATENCION;
                tabla["Motivo"] = consultaExterna.Motivo.ToString();
                tabla["AntecedentesPersonales"] = consultaExterna.AntecedentesPersonales.ToString();
                tabla["Cardiopatia"] = consultaExterna.Cardiopatia.ToString();
                tabla["Diabetes"] = (consultaExterna.Diabetes != null) ? consultaExterna.Diabetes.ToString() : "";
                tabla["Vascular"] = consultaExterna.Vascular.ToString();
                tabla["Hipertencion"] = consultaExterna.Hipertencion.ToString();
                tabla["Cancer"] = consultaExterna.Cancer.ToString();
                tabla["tuberculosis"] = consultaExterna.tuberculosis.ToString();
                tabla["mental"] = consultaExterna.mental.ToString();
                tabla["infecciosa"] = consultaExterna.infecciosa.ToString();
                tabla["malformacion"] = consultaExterna.malformacion.ToString();
                tabla["otro"] = consultaExterna.otro.ToString();
                tabla["antecedentesFamiliares"] = consultaExterna.antecedentesFamiliares.ToString();
                tabla["enfermedadActual"] = consultaExterna.enfermedadActual.ToString();
                tabla["sentidos"] = consultaExterna.sentidos.ToString();
                tabla["sentidossp"] = consultaExterna.sentidossp.ToString();
                tabla["respiratorio"] = consultaExterna.respiratorio.ToString();
                tabla["respiratoriosp"] = consultaExterna.respiratoriosp.ToString();
                tabla["cardioVascular"] = consultaExterna.cardioVascular.ToString();
                tabla["cardioVascularsp"] = consultaExterna.cardioVascularsp.ToString();
                tabla["digestivo"] = consultaExterna.digestivo.ToString();
                tabla["digestivosp"] = consultaExterna.digestivosp.ToString();
                tabla["genital"] = consultaExterna.genital.ToString();
                tabla["genitalsp"] = consultaExterna.genitalsp.ToString();
                tabla["urinario"] = consultaExterna.urinario.ToString();
                tabla["urinariosp"] = consultaExterna.urinariosp.ToString();
                tabla["esqueletico"] = consultaExterna.esqueletico.ToString();
                tabla["esqueleticosp"] = consultaExterna.esqueleticosp.ToString();
                tabla["endocrino"] = consultaExterna.endocrino.ToString();
                tabla["endocrinosp"] = consultaExterna.endocrinosp.ToString();
                tabla["linfatico"] = consultaExterna.linfatico.ToString();
                tabla["linfaticosp"] = consultaExterna.linfaticosp.ToString();
                tabla["nervioso"] = consultaExterna.nervioso.ToString();
                tabla["nerviososp"] = consultaExterna.nerviososp.ToString();
                tabla["revisionactual"] = consultaExterna.revisionactual.ToString();
                tabla["fechamedicion"] = consultaExterna.fechamedicion.ToString();
                if (Convert.ToInt32(Math.Round(sign.T_Bucal)) == 0)
                {
                    tabla["temperatura"] = sign.T_Axilar.ToString();
                }
                else
                {
                    tabla["temperatura"] = sign.T_Bucal.ToString();
                }

                tabla["presion1"] = consultaExterna.presion1.ToString();
                tabla["presion2"] = consultaExterna.presion2.ToString();
                tabla["pulso"] = consultaExterna.pulso.ToString();
                tabla["peso"] = consultaExterna.peso.ToString();
                tabla["talla"] = consultaExterna.talla.ToString();
                tabla["cabeza"] = consultaExterna.cabeza.ToString();
                tabla["cabezasp"] = consultaExterna.cabezasp.ToString();
                tabla["cuello"] = consultaExterna.cuello.ToString();
                tabla["cuellosp"] = consultaExterna.cuellosp.ToString();
                tabla["torax"] = consultaExterna.torax.ToString();
                tabla["toraxsp"] = consultaExterna.toraxsp.ToString();
                tabla["abdomen"] = consultaExterna.abdomen.ToString();
                tabla["abdomensp"] = consultaExterna.abdomensp.ToString();
                tabla["pelvis"] = consultaExterna.pelvis.ToString();
                tabla["pelvissp"] = consultaExterna.pelvissp.ToString();
                tabla["extremidades"] = consultaExterna.extremidades.ToString();
                tabla["extremidadessp"] = consultaExterna.extremidadessp.ToString();
                tabla["examenFisico"] = consultaExterna.examenFisico.ToString();
                tabla["diagnostico1"] = consultaExterna.diagnostico1.ToString();
                tabla["diagnostico1cie"] = consultaExterna.diagnostico1cie.ToString();
                tabla["diagnostico1pre"] = consultaExterna.diagnostico1pre.ToString();
                tabla["diagnostico1def"] = consultaExterna.diagnostico1def.ToString();
                tabla["diagnostico2"] = consultaExterna.diagnostico2.ToString();
                tabla["diagnostico2cie"] = consultaExterna.diagnostico2cie.ToString();
                tabla["diagnostico2pre"] = consultaExterna.diagnostico2pre.ToString();
                tabla["diagnostico2def"] = consultaExterna.diagnostico2def.ToString();
                tabla["diagnostico3"] = consultaExterna.diagnostico3.ToString();
                tabla["diagnostico3cie"] = consultaExterna.diagnostico3cie.ToString();
                tabla["diagnostico3def"] = consultaExterna.diagnostico3def.ToString();
                tabla["diagnostico3pre"] = consultaExterna.diagnostico3pre.ToString();
                tabla["diagnostico4"] = consultaExterna.diagnostico4.ToString();
                tabla["diagnostico4cie"] = consultaExterna.diagnostico4cie.ToString();
                tabla["diagnostico4def"] = consultaExterna.diagnostico4def.ToString();
                tabla["diagnostico4pre"] = consultaExterna.diagnostico4pre.ToString();
                tabla["diagnostico5"] = consultaExterna.diagnostico5.ToString();
                tabla["diagnostico5cie"] = consultaExterna.diagnostico5cie.ToString();
                tabla["diagnostico5def"] = consultaExterna.diagnostico5def.ToString();
                tabla["diagnostico5pre"] = consultaExterna.diagnostico5pre.ToString();
                tabla["diagnostico6"] = consultaExterna.diagnostico6.ToString();
                tabla["diagnostico6cie"] = consultaExterna.diagnostico6cie.ToString();
                tabla["diagnostico6def"] = consultaExterna.diagnostico6def.ToString();
                tabla["diagnostico6pre"] = consultaExterna.diagnostico6pre.ToString();
                tabla["planesTratamiento"] = consultaExterna.planesTratamiento.ToString();
                tabla["evolucion"] = consultaExterna.evolucion.ToString();
                tabla["prescripciones"] = consultaExterna.prescripciones.ToString();
                DateTime fecha1 = dtp_fechaAltaEmerencia.Value;
                tabla["fecha"] = fecha1.ToString("yyyy-MM-dd");
                //tabla["hora"] = Convert.ToString(DateTime.Now.ToString("hh:mm"));
                tabla["hora"] = Convert.ToDateTime(consultaExterna.fecha).ToShortTimeString();
                tabla["dr"] = consultaExterna.dr.ToString();
                tabla["codigo"] = Sesion.codMedico.ToString();
                tabla["Logo"] = logo;
                tabla["Empresa"] = empresa;
                tabla["ocular"] = sign.Ocular;
                tabla["verbal"] = sign.Verbal;
                tabla["motora"] = sign.Motora;
                tabla["reaIz"] = sign.Reaccion_Iz;
                tabla["reaDr"] = sign.Reaccion_Der;
                tabla["saturacion"] = sign.S_Oxigeno;
                tabla["capilar"] = sign.Glisemia_Capilar;
                tabla["Nombre2"] = pacien.PAC_NOMBRE2;
                tabla["Apellido2"] = pacien.PAC_APELLIDO_MATERNO;
                tabla["HipertencionP"] = consultaExterna.hipertencionP.ToString();
                tabla["CancerP"] = consultaExterna.cancerP.ToString();
                tabla["tuberculosisp"] = consultaExterna.tuberculosisP.ToString();
                tabla["mentalp"] = consultaExterna.mentalP.ToString();
                tabla["infecciosap"] = consultaExterna.infecciosaP.ToString();
                tabla["malformacionp"] = consultaExterna.malformacionP.ToString();
                tabla["otrop"] = consultaExterna.otroP.ToString();
                tabla["cardiopatiap"] = consultaExterna.cardiopatiaP.ToString();
                tabla["Vascularp"] = consultaExterna.vascularP.ToString();
                tabla["piel"] = consultaExterna.piel.ToString();
                tabla["horamedicion"] = DateTime.Parse(consultaExterna.fechamedicion).ToString("HH:mm:ss");
                tabla["endocrinoF"] = consultaExterna.endocrinoF.ToString();
                tabla["endocrinoP"] = consultaExterna.endocrinoP.ToString();
                tabla["ojos"] = consultaExterna.rOjos;
                tabla["oidos"] = consultaExterna.rOidos;
                tabla["nariz"] = consultaExterna.rNariz;
                tabla["boca"] = consultaExterna.rBoca;
                tabla["orofaringe"] = consultaExterna.rOrofaringe;
                tabla["axilas"] = consultaExterna.rAxilas;
                tabla["columna"] = consultaExterna.rColumna;
                tabla["ingle"] = consultaExterna.rIngle;
                tabla["minferior"] = consultaExterna.rInferior;
                tabla["osentidos"] = consultaExterna.sSentidos;
                tabla["vascular"] = consultaExterna.sCardio;
                tabla["mesqueletico"] = consultaExterna.sMusculo;
                tabla["hlimfatico"] = consultaExterna.sLimfatico;
                tabla["neurologico"] = consultaExterna.sNeurologico;
                tabla["fisico"] = consultaExterna.examenFisico;
                tabla["piel1"] = consultaExterna.rPiel;
                tabla["abdomen1"] = consultaExterna.abdomen;
                tabla["msuperior"] = consultaExterna.extremidades;
                tabla["respiratorio1"] = consultaExterna.respiratorio;
                tabla["cvascular"] = consultaExterna.Vascular;
                tabla["digestivo1"] = consultaExterna.sDigestivo;
                tabla["urinario1"] = consultaExterna.sUrinario;
                tabla["endocrino1"] = consultaExterna.sEndocrino;

                if (sign.PerimetroA.ToString() == "0.00" || sign.PerimetroA.ToString() == "0")
                    tabla["perimetroabdominal"] = "";
                else
                    tabla["perimetroabdominal"] = sign.PerimetroA;
                if (sign.Hemoglobina.ToString() == "0.00" || sign.Hemoglobina.ToString() == "0")
                    tabla["hemoglobina"] = "";
                else
                    tabla["hemoglobina"] = sign.Hemoglobina;
                if (sign.Glucosa_Capilar.ToString() == "0.00" || sign.Glucosa_Capilar.ToString() == "0")
                    tabla["glucosa"] = "";
                else
                    tabla["glucosa"] = sign.Glucosa_Capilar;

                tabla["pulsox"] = Math.Round(sign.S_Oxigeno);
                tabla["frecuenciaRespiratoria"] = Math.Round(sign.F_Respiratoria);
                tabla["indice"] = sign.Ind_Masa;
                tabla["nerviososp"] = pri;
                tabla["revisionactual"] = sub;

                tabla["genital"] = consultaExterna.sGenital;
                tabla["capilar"] = consultaExterna.revisionactual;
                if (musuario != null)
                {
                    tabla["pnombre"] = musuario.MED_NOMBRE1;
                    tabla["snombre"] = musuario.MED_APELLIDO_MATERNO;
                    tabla["papellido"] = musuario.MED_APELLIDO_PATERNO;
                    tabla["cedula"] = musuario.MED_RUC.Substring(0, 10);

                    tabla["total"] = mimprime.MED_APELLIDO_PATERNO + ' ' + mimprime.MED_APELLIDO_MATERNO + ' ' + mimprime.MED_NOMBRE1;
                }
                else
                {
                    tabla["pnombre"] = mimprime.MED_NOMBRE1;
                    tabla["snombre"] = mimprime.MED_APELLIDO_MATERNO;
                    tabla["papellido"] = mimprime.MED_APELLIDO_PATERNO;
                    tabla["cedula"] = mimprime.MED_RUC.Substring(0, 10);

                    tabla["total"] = consultaExterna.dr;
                }


                Ds.Tables["Form002MSP"].Rows.Add(tabla);
                //pacien.PAC_APELLIDO_PATERNO,
                //datos.sexoPaciente.ToString(),
                ////consultaExterna.Edad.ToString(),
                // Regex.Match(consultaExterna.Edad.ToString(), patron),
                //datos.historiaClinica.ToString().Trim(),
                //consultaExterna.AteCodigo.ToString(),
                //consultaExterna.Motivo.ToString(),
                //consultaExterna.AntecedentesPersonales.ToString(),
                //consultaExterna.Cardiopatia.ToString(),
                //consultaExterna.Diabetes.ToString(),
                //consultaExterna.Vascular.ToString(),
                //consultaExterna.Hipertencion.ToString(),
                //consultaExterna.Cancer.ToString(),
                //consultaExterna.tuberculosis.ToString(),
                //consultaExterna.mental.ToString(),
                //consultaExterna.infecciosa.ToString(),
                //consultaExterna.malformacion.ToString(),
                //consultaExterna.otro.ToString(),
                //consultaExterna.antecedentesFamiliares.ToString(),
                //consultaExterna.enfermedadActual.ToString(),
                //consultaExterna.sentidos.ToString(),
                //consultaExterna.sentidossp.ToString(),
                //consultaExterna.respiratorio.ToString(),
                //consultaExterna.respiratoriosp.ToString(),
                //consultaExterna.cardioVascular.ToString(),
                //consultaExterna.cardioVascularsp.ToString(),
                //consultaExterna.digestivo.ToString(),
                //consultaExterna.digestivosp.ToString(),
                //consultaExterna.genital.ToString(),
                //consultaExterna.genitalsp.ToString(),
                //consultaExterna.urinario.ToString(),
                //consultaExterna.urinariosp.ToString(),
                //consultaExterna.esqueletico.ToString(),
                //consultaExterna.esqueleticosp.ToString(),
                //consultaExterna.endocrino.ToString(),
                //consultaExterna.endocrinosp.ToString(),
                //consultaExterna.linfatico.ToString(),
                //consultaExterna.linfaticosp.ToString(),
                //consultaExterna.nervioso.ToString(),
                //consultaExterna.nerviososp.ToString(),
                //consultaExterna.revisionactual.ToString(),
                //consultaExterna.fechamedicion.ToString(),
                //consultaExterna.temperatura.ToString(),
                //consultaExterna.presion1.ToString(),
                //consultaExterna.presion2.ToString(),
                //consultaExterna.pulso.ToString(),
                //consultaExterna.frecuenciaRespiratoria.ToString(),
                //consultaExterna.peso.ToString(),
                //consultaExterna.talla.ToString(),
                //consultaExterna.cabeza.ToString(),
                //consultaExterna.cabezasp.ToString(),
                //consultaExterna.cuello.ToString(),
                //consultaExterna.cuellosp.ToString(),
                //consultaExterna.torax.ToString(),
                //consultaExterna.toraxsp.ToString(),
                //consultaExterna.abdomen.ToString(),
                //consultaExterna.abdomensp.ToString(),
                //consultaExterna.pelvis.ToString(),
                //consultaExterna.pelvissp.ToString(),
                //consultaExterna.extremidades.ToString(),
                //consultaExterna.extremidadessp.ToString(),
                //consultaExterna.examenFisico.ToString(),
                //consultaExterna.diagnostico1.ToString(),
                //consultaExterna.diagnostico1cie.ToString(),
                //consultaExterna.diagnostico1pre.ToString(),
                //consultaExterna.diagnostico1def.ToString(),
                //consultaExterna.diagnostico2.ToString(),
                //consultaExterna.diagnostico2cie.ToString(),
                //consultaExterna.diagnostico2pre.ToString(),
                //consultaExterna.diagnostico2def.ToString(),
                //consultaExterna.diagnostico3.ToString(),
                //consultaExterna.diagnostico3cie.ToString(),
                //consultaExterna.diagnostico3def.ToString(),
                //consultaExterna.diagnostico3pre.ToString(),
                //consultaExterna.diagnostico4.ToString(),
                //consultaExterna.diagnostico4cie.ToString(),
                //consultaExterna.diagnostico4def.ToString(),
                //consultaExterna.diagnostico4pre.ToString(),
                //consultaExterna.planesTratamiento.ToString(),
                //consultaExterna.evolucion.ToString(),
                //consultaExterna.prescripciones.ToString(),
                //Convert.ToString(dtp_fechaAltaEmerencia.Value),
                //Convert.ToString(DateTime.Now.ToString("hh:mm")),
                //consultaExterna.dr.ToString(),
                //Sesion.codMedico.ToString(),
                //logo,
                //empresa,
                //sign.Ocular,
                //sign.Verbal,
                //sign.Motora,
                //sign.Reaccion_Iz,
                //sign.Reaccion_Der,
                //sign.S_Oxigeno,
                //sign.Glisemia_Capilar,
                //tot1,
                //pacien.PAC_NOMBRE2,
                //pacien.PAC_APELLIDO_MATERNO
                //});

                frmReportes x = new frmReportes(1, "ConsultaExterna", Ds);
                x.Show();
            }
            else
                MessageBox.Show("Algo ocurrio al generar el reporte.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnImprimir1_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ATENCIONES validaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(txtatecodigo.Text.Trim()));
            if (validaAtencion.ESC_CODIGO == 1)
            {
                HabilitarBotones(false, true, false, false, false, false, false, false, false, false, false);
                //P_Central.Enabled = true;
                grupos(true);
                nuevaConsulta = false;
            }
            else
            {
                HabilitarBotones(false, false, false, true, true, true, true, false, false, false, false);
                MessageBox.Show("Paciente ha sido dado de alta.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                if (GuardaFormulario())
                {
                    MessageBox.Show("Información Guardada Con Exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //P_Central.Enabled = false;
                    grupos(false);
                    ATENCIONES validaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(txtatecodigo.Text.Trim()));
                    if (validaAtencion.ESC_CODIGO == 1)
                        HabilitarBotones(false, false, true, true, true, true, true, true, false, true, false);
                    else
                        HabilitarBotones(false, false, false, true, true, true, false, false, false, false, false);
                }
                else
                    MessageBox.Show("Información No Se Guardo Comuniquese Con Sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Datos incompletos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCertificado1_Click(object sender, EventArgs e)
        {
            try
            {
                //frm_CertificadoIESS x = new frm_CertificadoIESS(Convert.ToInt32(txtatecodigo.Text.Trim()), Convert.ToInt32(lblHistoria.Text.Trim()));
                //frm_Certificados x = new frm_Certificados();
                //if (x.abre)
                frm_Certificados x = new frm_Certificados(Convert.ToInt32(txtatecodigo.Text.Trim()), Convert.ToInt32(lblHistoria.Text.Trim()));
                x.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ud. No tiene acceso a generar certificados medicos ya que no esta registrado como un usuario medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnReceta1_Click(object sender, EventArgs e)
        {
            if (NegParametros.ParametroReceta())
            {
                MEDICOS medico = NegMedicos.recuperarMedicoID_Usuario(Sesion.codUsuario);
                var recuperada = NegAtenciones.RecuepraAtencionNumeroAtencion(Convert.ToInt64(txtatecodigo.Text.Trim()));
                if (NegCertificadoMedico.ExisteRecetaMedico(recuperada.ATE_CODIGO, medico.MED_CODIGO))
                {
                    MessageBox.Show("Este medico genero una receta medica para este paciente. SI DESEA MODIFICAR LA RECETA, PRIMERO DEBE ANULARLA EN EL EXPLORADOR DE RECETAS MEDICAS", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmRecetaNew x = new frmRecetaNew(txtatecodigo.Text.Trim(), true);
                if (x.noCargar)
                    x.Show();
                else
                    x.Close();
            }
            else
            {
                His.Formulario.frm_RecetaMedica x = new His.Formulario.frm_RecetaMedica();
                x.Show();
            }
        }

        private void btnLaboratorio_Click(object sender, EventArgs e)
        {
            His.Formulario.frm_LaboratorioClinico laboratorio = new His.Formulario.frm_LaboratorioClinico(Convert.ToInt64(txtatecodigo.Text.Trim()), true);
            laboratorio.Show();
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            frm_Imagen X = new frm_Imagen(Convert.ToInt32(txtatecodigo.Text.Trim()));
            X.mushuñan = mushuñan;
            X.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (subsecuente != null)
            {
                if (!NegConsultaExterna.CerrarCxe("Form002", subsecuente.ate_codigo_subsecuente))
                {
                    MessageBox.Show("No se ha podido cerrar formulario. Consulte con sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HabilitarBotones(false, false, true, true, true, true, true, false, false, true, false);
                }
                else
                {
                    MessageBox.Show("Formulario cerrado con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarBotones(false, false, false, true, true, true, true, false, true, false, false);
                }
            }
            else
            {
                if (!NegConsultaExterna.CerrarCxe("Form002", Convert.ToInt64(txtatecodigo.Text.Trim())))
                {
                    MessageBox.Show("No se ha podido cerrar formulario. Consulte con sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HabilitarBotones(false, false, true, true, true, true, true, false, false, true, false);
                }
                else
                {
                    MessageBox.Show("Formulario cerrado con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarBotones(false, false, false, true, true, true, true, false, true, false, false);
                }

            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 2 || Sesion.codDepartamento == 5)
            {
                if (!NegConsultaExterna.AbrirCxE(Convert.ToInt64(txtatecodigo.Text.Trim())))
                {
                    MessageBox.Show("No se ha podido abrir formulario. Consulte con sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                    MessageBox.Show("Formulario abierto con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HabilitarBotones(false, false, true, true, true, true, true, false, false, true, false);
            }
            else
            {
                MessageBox.Show("No se ha podido abrir formulario. No tiene los permisos necesarios", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitarBotones(true, false, false, false, false, false, false, false, false, false, false);
            P_Central.Visible = false;
            P_Datos.Visible = false;
            txtTemperatura.Enabled = true;
            txtTempAx.Enabled = true;
            limpiarCampos();
        }

        private void txtTemperatura_Leave(object sender, EventArgs e)
        {
            if (txtTemperatura.Text != "")
                if (Convert.ToDecimal(txtTemperatura.Text) > 50)
                {
                    MessageBox.Show("Rango de temperatura incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtTemperatura.Text = "0";
                }
        }

        private void txtPresionArteria1_Leave(object sender, EventArgs e)
        {
            if (txtPresionArteria1.Text != "")
                if (Convert.ToDecimal(txtPresionArteria1.Text) > 300)
                {
                    MessageBox.Show("Rango de presion incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPresionArteria1.Text = "0";
                }
        }

        private void txtPresionArteria2_Leave(object sender, EventArgs e)
        {
            if (txtPresionArteria2.Text != "")
                if (Convert.ToDecimal(txtPresionArteria2.Text) > 300)
                {
                    MessageBox.Show("Rango de presion incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPresionArteria2.Text = "0";
                }
        }

        private void txtPulso_Leave(object sender, EventArgs e)
        {
            if (txtPulso.Text != "")
                if (Convert.ToDecimal(txtPulso.Text) > 300)
                {
                    MessageBox.Show("Rango de pulso incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPulso.Text = "0";
                }
        }

        private void txtFrecuenciaRespiratoria_Leave(object sender, EventArgs e)
        {
            if (txtFrecuenciaRespiratoria.Text != "")
                if (Convert.ToDecimal(txtFrecuenciaRespiratoria.Text) > 300)
                {
                    MessageBox.Show("Rango de frecuencia respiratoria incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtFrecuenciaRespiratoria.Text = "0";
                }
        }

        private void txtPeso_Leave(object sender, EventArgs e)
        {
            if (txtPeso.Text != "")
                if (Convert.ToDecimal(txtPeso.Text) > 700)
                {
                    MessageBox.Show("Rango de peso incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPeso.Text = "0";
                }
        }

        private void txtTalla_Leave(object sender, EventArgs e)
        {
            if (txtTalla.Text != "")
                if (Convert.ToDecimal(txtTalla.Text) > 3)
                {
                    MessageBox.Show("Rango de talla incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtTalla.Text = "0";
                }
        }

        private void txtPresionArteria1_TextChanged(object sender, EventArgs e)
        {
            if (txtPresionArteria1.Text == "" || !NegUtilitarios.ValidaPrecion1(Convert.ToInt16(txtPresionArteria1.Text)))
            {
                txtPresionArteria1.Text = "0";
            }
        }

        private void txtPresionArteria2_TextChanged(object sender, EventArgs e)
        {
            if (txtPresionArteria2.Text == "" || !NegUtilitarios.ValidaPrecion2(Convert.ToDouble(txtPresionArteria2.Text)))
            {
                txtPresionArteria2.Text = "0";
            }
        }
        MEDICOS medicoTratante = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (mushuñan)
            {
                Int32 ingreso = NegTipoIngreso.RecuperarporAtencion(Convert.ToInt64(txtatecodigo.Text));
                switch (ingreso)
                {
                    case 12:
                        List<MEDICOS> medicos = NegMedicos.listaMedicos();
                        medicos = NegMedicos.listaMedicosIncTipoMedico();
                        His.Formulario.frm_AyudaMedicos ayuda = new His.Formulario.frm_AyudaMedicos(medicos, "MEDICOS", "CODIGO");
                        ayuda.ShowDialog();

                        if (ayuda.campoPadre.Text != string.Empty)
                        {
                            medicoTratante = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                            txt_profesionalEmergencia.Text = "";
                            txt_profesionalEmergencia.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim() + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                        }
                        break;
                    case 10:
                        List<MEDICOS> medicos1 = NegMedicos.listaMedicos();
                        medicos1 = NegMedicos.listaMedicosIncTipoMedico();
                        His.Formulario.frm_AyudaMedicos ayuda1 = new His.Formulario.frm_AyudaMedicos(medicos1, "MEDICOS", "CODIGO");
                        ayuda1.ShowDialog();

                        if (ayuda1.campoPadre.Text != string.Empty)
                        {
                            medicoTratante = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda1.campoPadre.Text.ToString()));
                            txt_profesionalEmergencia.Text = "";
                            txt_profesionalEmergencia.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim() + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnSubsecuente_Click(object sender, EventArgs e)
        {
            //Emergencia.frm_AyudaPacientes ayuda = new Emergencia.frm_AyudaPacientes(true, true); // Cambio para consulta subsecuente
            //ayuda.ShowDialog();
            //if (ayuda.ateCodigo != "")
            //{
            //    frm_Evolucion frm = new frm_Evolucion(true, Convert.ToInt64(ayuda.ateCodigo));
            //    frm.ShowDialog();
            //}
            if (subsecuente != null)
            {
                frm_Evolucion frm = new frm_Evolucion(true, subsecuente.ate_codigo_subsecuente);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se generado una ATENCIÓN SUBSECUENTE para este paciente en la admisión", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (txtatecodigo.Text != "")
            {
                PACIENTES paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(txtatecodigo.Text));
                His.Admision.frm_ExploradorFormularios frm = new Admision.frm_ExploradorFormularios();
                frm.FiltroHC = Convert.ToInt64(paciente.PAC_HISTORIA_CLINICA);
                frm._ayudaSubsecuentes = true;
                frm.ShowDialog();
            }
        }

        private void chbPre5_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico5.Text != "")
                if (chbPre5.Checked)
                    chbDef5.Checked = false;
                else
                    chbDef5.Checked = true;
        }

        private void chbDef5_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico5.Text != "")
                if (chbDef5.Checked)
                    chbPre5.Checked = false;
                else
                    chbPre5.Checked = true;
        }

        private void chbPre6_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico6.Text != "")
                if (chbPre6.Checked)
                    chbDef6.Checked = false;
                else
                    chbDef6.Checked = true;
        }

        private void chbDef6_CheckedChanged(object sender, EventArgs e)
        {
            if (txtDiagnostico6.Text != "")
                if (chbDef6.Checked)
                    chbPre6.Checked = false;
                else
                    chbPre6.Checked = true;
        }

        private void txtDiagnostico5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {
                    if (ValidarCie10(busqueda.codigo))
                    {
                        txtCieDiagnostico5.Text = busqueda.codigo;
                        txtDiagnostico5.Text = busqueda.resultado;
                        chbPre5.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("Cie10 ya ha sido agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtCieDiagnostico5.Text = "";
                    txtDiagnostico5.Text = "";
                    chbPre5.Checked = false;
                    chbDef5.Checked = false;
                }
            }
        }

        private void txtDiagnostico6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                if (busqueda.codigo != null)
                {
                    if (ValidarCie10(busqueda.codigo))
                    {
                        txtCieDiagnostico6.Text = busqueda.codigo;
                        txtDiagnostico6.Text = busqueda.resultado;
                        chbPre6.Checked = true;
                    }
                    else
                    {
                        MessageBox.Show("Cie10 ya ha sido agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    txtCieDiagnostico6.Text = "";
                    txtDiagnostico6.Text = "";
                    chbPre6.Checked = false;
                    chbDef6.Checked = false;
                }
            }
        }

        private void txtDiagnostico5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico5.Text != "")
                {
                    //txtCieDiagnostico4.Enabled = true;
                    //txtCieDiagnostico4.Focus();
                    txtPlanesTratamiento.Focus();
                }
            }
        }

        private void txtDiagnostico6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)09)
            {
                if (txtDiagnostico6.Text != "")
                {
                    //txtCieDiagnostico4.Enabled = true;
                    //txtCieDiagnostico4.Focus();
                    txtPlanesTratamiento.Focus();
                }
            }
        }

        private void txtPresionArteria1_TextChanged_1(object sender, EventArgs e)
        {
            if (txtPresionArteria1.Text == "" || !NegUtilitarios.ValidaPrecion1(Convert.ToDouble(txtPresionArteria1.Text)))
            {
                txtPresionArteria1.Text = "0";
            }
        }

        private void txtPresionArteria2_TextChanged_1(object sender, EventArgs e)
        {
            if (txtPresionArteria2.Text == "" || !NegUtilitarios.ValidaPrecion2(Convert.ToDouble(txtPresionArteria2.Text)))
            {
                txtPresionArteria2.Text = "0";
            }
        }

        private void txtPulsioximetria_Leave(object sender, EventArgs e)
        {
            decimal satura = 0;
            if (txtPulsioximetria.Text == "")
            {
                satura = 0;
            }
            else
            {
                satura = Convert.ToDecimal(txtPulsioximetria.Text);
            }
            if (satura < 30 || satura > 100)
            {
                txtPulsioximetria.Focus();
                txtPulsioximetria.Text = "0";
                MessageBox.Show("Saturación de oxigeno no puede ser menor a 30 ni mayor a 100", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void txtPulso_Leave_1(object sender, EventArgs e)
        {
            if (txtPulso.Text.Trim() == string.Empty)
            {
                txtPulso.Text = "0";
            }
        }

        private void txtPulso_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (txtPulso.Text == "" || !NegUtilitarios.ValidaFcardiaca(Convert.ToDouble(txtPulso.Text.Trim())))
            {
                txtPulso.Text = NegParametros.RecuperaValorParSvXcodigo(56).ToString();
                return;
            }
        }

        private void txtPresionArteria1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtPresionArteria2_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtPulso_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void txtFrecuenciaRespiratoria_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPulsioximetria.Focus();
            }
        }

        private void txtFrecuenciaRespiratoria_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPulso_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTemperatura_Validating(object sender, CancelEventArgs e)
        {
            if (txtTemperatura.Text != "" || txtTemperatura.Text == "0")
            {
                if (!NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txtTemperatura.Text)))
                {
                    txtTemperatura.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
                    return;
                }
            }
            else { txtTemperatura.Enabled = true; }

            if (txtTemperatura.Text == "")
            {
                txtTemperatura.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
            }
        }

        private void txtTempAx_Validating(object sender, CancelEventArgs e)
        {
            if (txtTempAx.Text != "" || txtTempAx.Text == "0")
            {
                if (!NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txtTempAx.Text)))
                {
                    txtTempAx.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
                    return;
                }
            }
            else { txtTempAx.Enabled = true; }

            if (txtTempAx.Text == "")
            {
                txtTempAx.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
            }
        }

        private void txtFrecuenciaRespiratoria_Validating(object sender, CancelEventArgs e)
        {
            if (txtFrecuenciaRespiratoria.Text == "" || !NegUtilitarios.ValidaFrespiratoria(Convert.ToDouble(txtFrecuenciaRespiratoria.Text.Trim())))
            {
                txtFrecuenciaRespiratoria.Text = NegParametros.RecuperaValorParSvXcodigo(58).ToString();
            }
        }

        private void cmb_Ocular_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Ocular.SelectedIndex != -1)
            {
                if (cmb_Verbal.SelectedIndex != -1)
                {
                    if (cmb_Motora.SelectedIndex != -1)
                    {
                        txtGlasgow.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.Text) + Convert.ToInt32(cmb_Verbal.Text) + Convert.ToInt32(cmb_Motora.Text));
                    }
                    else
                    {
                        txtGlasgow.Text = "0";
                    }
                }
            }
        }

        private void cmb_Verbal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Ocular.SelectedIndex != -1)
            {
                if (cmb_Verbal.SelectedIndex != -1)
                {
                    if (cmb_Motora.SelectedIndex != -1)
                    {
                        txtGlasgow.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.Text) + Convert.ToInt32(cmb_Verbal.Text) + Convert.ToInt32(cmb_Motora.Text));
                    }
                    else
                    {
                        txtGlasgow.Text = "0";
                    }
                }
            }
        }

        private void cmb_Motora_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Ocular.SelectedIndex != -1)
            {
                if (cmb_Verbal.SelectedIndex != -1)
                {
                    if (cmb_Motora.SelectedIndex != -1)
                    {

                        txtGlasgow.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.Text) + Convert.ToInt32(cmb_Verbal.Text) + Convert.ToInt32(cmb_Motora.Text));
                    }
                    else
                    {
                        txtGlasgow.Text = "0";
                    }
                }
            }
        }

        private void txtPulso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtFrecuenciaRespiratoria.Focus();
            }
        }

        private void txtFrecuenciaRespiratoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cmb_Ocular.Focus();
            }
        }

        private void txtPeso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtTalla.Focus();
            }
        }

        private void txtTalla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtIndice.Focus();
            }
        }

        private void txtIndice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPerimetro.Focus();
            }
        }

        private void txtPerimetro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtHemoglobina.Focus();
            }
        }

        private void txtHemoglobina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtGlucosa.Focus();
            }
        }

        private void txtGlucosa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPulsioximetria.Focus();
            }
        }

        private void txtPulsioximetria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cmb_Ocular.Focus();
            }
        }


        private void cmb_Ocular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cmb_Verbal.Focus();
            }
        }

        private void cmb_Verbal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cmb_Motora.Focus();
            }
        }

        private void cmb_Motora_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPeso.Focus();
            }
        }

        private void txtTemperatura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //txt_TAxilar.Focus();
                if (txtTemperatura.Text != "0")
                {
                    if (txtTemperatura.Text.Trim() != "")
                    {
                        if (txtTemperatura.Text.Trim().Substring(txtTemperatura.Text.Length - 1, 1) == ".")
                            txtTemperatura.Text = txtTemperatura.Text.Remove(txtTemperatura.Text.Length - 1);
                        txtTempAx.Enabled = false;
                        txtPresionArteria1.Focus();
                    }
                    else
                    {
                        txtTemperatura.Text = "0";
                        txtTempAx.Enabled = true;
                        txtTempAx.Focus();
                    }

                }
                else
                {
                    txtTemperatura.Text = "0";
                    txtTemperatura.Enabled = false;
                    txtTempAx.Enabled = true;
                    txtTempAx.Focus();
                }
            }
        }

        private void txtTempAx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtTempAx.Text != "0")
                {
                    if (txtTempAx.Text.Trim() != "")
                    {
                        if (txtTempAx.Text.Trim().Substring(txtTempAx.Text.Length - 1, 1) == ".")
                            txtTempAx.Text = txtTempAx.Text.Remove(txtTempAx.Text.Length - 1);
                        txtTemperatura.Enabled = false;
                        txtTemperatura.Text = "0";
                        txtPresionArteria1.Focus();
                    }

                    else
                    {
                        txtTempAx.Text = "0";
                        txtTemperatura.Enabled = true;
                        txtPresionArteria1.Focus();
                    }
                }
                else
                {
                    txtTempAx.Text = "0";
                    txtTemperatura.Enabled = true;
                    txtPresionArteria1.Focus();
                }
            }
        }

        private void txtPresionArteria1_Validating(object sender, CancelEventArgs e)
        {
            if (txtPresionArteria1.Text == "" || !NegUtilitarios.ValidaPrecion1(Convert.ToDouble(txtPresionArteria1.Text)))
            {
                txtPresionArteria1.Text = "0";
            }
        }

        private void txtPresionArteria1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPresionArteria2.Focus();
            }
        }

        private void txtPresionArteria2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtPulso.Focus();
            }
        }

        private void txtPeso_TextChanged(object sender, EventArgs e)
        {
            if (txtPeso.Text != "0" && txtPeso.Text.Trim() != "")
            {
                if (txtTalla.Text != "0" && txtTalla.Text.Trim() != "")
                {
                    double valor = (Convert.ToDouble(txtPeso.Text) / Math.Pow(Convert.ToDouble(txtTalla.Text), 2));
                    txtIndice.Text = (Math.Round(valor, 2)).ToString();
                }
                //else
                //    MessageBox.Show("El peso no puede ser 0", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("El peso no puede ser 0", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIndice.Text = "0";
            }
        }

        private void txtTalla_TextChanged(object sender, EventArgs e)
        {
            double talla1 = 0;
            if (txtTalla.Text.Length > 0)
                talla1 = Convert.ToDouble(txtTalla.Text);
            if (talla1 > 2.50)
            {
                MessageBox.Show("La talla no puede ser mayor a 2.50 m", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTalla.Text = "0";
                return;
            }
            if (txtPeso.Text != "0" && txtPeso.Text.Trim() != "")
            {
                if (txtTalla.Text != "0" && txtTalla.Text.Trim() != "")
                {
                    double valor = (Convert.ToDouble(txtPeso.Text) / Math.Pow(Convert.ToDouble(txtTalla.Text), 2));
                    txtIndice.Text = (Math.Round(valor, 2)).ToString();
                }
                else
                    MessageBox.Show("La talla no puede ser 0", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //MessageBox.Show("El peso no puede ser 0", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIndice.Text = "0";
            }
        }

        private void txtPulsioximetria_Validating(object sender, CancelEventArgs e)
        {
            decimal satura = 0;
            if (txtPulsioximetria.Text == "")
            {
                satura = 0;
            }
            else
            {
                satura = Convert.ToDecimal(txtPulsioximetria.Text);
            }
            if (satura < 30 || satura > 100)
            {
                txtPulsioximetria.Focus();
                txtPulsioximetria.Text = "30";
                MessageBox.Show("Saturación de oxigeno no puede ser menor a 30 ni mayor a 100", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void txtGlucosa_Validating(object sender, CancelEventArgs e)
        {
            if (txtGlucosa.Text == "")
            {
                txtGlucosa.Text = "0";
            }
        }

        private void txtHemoglobina_TextChanged(object sender, EventArgs e)
        {
            if (txtHemoglobina.Text == "")
            {
                txtHemoglobina.Text = "0";
            }
        }

        private void txtPeso_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtTalla_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtPerimetro_Validating(object sender, CancelEventArgs e)
        {
            if (txtPerimetro.Text == "")
            {
                txtPerimetro.Text = "0";
            }
        }

        private void txtHemoglobina_Validating(object sender, CancelEventArgs e)
        {
            if (txtHemoglobina.Text == "")
            {
                txtHemoglobina.Text = "0";
            }
        }

        private void txtTemperatura_Leave_1(object sender, EventArgs e)
        {
            if (txtTemperatura.Text != "0")
            {
                if (txtTemperatura.Text.Trim() != "")
                {
                    txtTempAx.Enabled = false;
                    txtTempAx.Text = "0";
                    txtPresionArteria1.Focus();
                }
                else
                {
                    txtTemperatura.Text = "0";
                    txtTempAx.Enabled = true;
                    txtTempAx.Focus();
                }

            }
            else
            {
                txtTemperatura.Text = "0";
                txtTempAx.Enabled = true;
                txtTempAx.Focus();
            }
        }

        private void txtTempAx_Leave(object sender, EventArgs e)
        {
            if (txtTempAx.Text != "0")
            {
                if (txtTempAx.Text.Trim() != "")
                {
                    txtTemperatura.Enabled = false;
                    txtPresionArteria1.Focus();
                }
                else
                {
                    txtTempAx.Text = "0";
                    txtTemperatura.Enabled = true;
                    txtPresionArteria1.Focus();
                }
            }
            else
            {
                txtTempAx.Text = "0";
                txtTemperatura.Enabled = true;
                txtPresionArteria1.Focus();
            }
        }
        private void grupos(bool estado)
        {
            ultraGroupBox1.Enabled = estado;
            ultraGroupBox2.Enabled = estado;
            ultraGroupBox3.Enabled = estado;
            ultraGroupBox4.Enabled = estado;
            ultraGroupBox5.Enabled = estado;
            ultraGroupBox6.Enabled = estado;
            ultraGroupBox7.Enabled = estado;
            ultraGroupBox8.Enabled = estado;
            ultraGroupBox9.Enabled = estado;
            ultraGroupBox10.Enabled = estado;
        }

        private void chbCardiopatiaP_Click(object sender, EventArgs e)
        {
            if (chbCardiopatiaP.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n1.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "1.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbHipertensionP_Click(object sender, EventArgs e)
        {
            if (chbHipertensionP.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n2.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "2.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbVascularP_Click(object sender, EventArgs e)
        {
            if (chbVascularP.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n3.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "3.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbMetabolicoP_Click(object sender, EventArgs e)
        {
            if (chbMetabolicoP.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n4.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "4.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbCancerP_Click(object sender, EventArgs e)
        {
            if (chbCancerP.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n5.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "5.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbTuberculosisP_Click(object sender, EventArgs e)
        {
            if (chbTuberculosisP.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n6.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "6.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbMentalP_Click(object sender, EventArgs e)
        {
            if (chbMentalP.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n7.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "7.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbInfecciosa_Click(object sender, EventArgs e)
        {
            if (chbInfecciosa.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n8.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "8.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbFormacionP_Click(object sender, EventArgs e)
        {
            if (chbFormacionP.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n9.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "9.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbOtroP_Click(object sender, EventArgs e)
        {
            if (chbOtroP.Checked == true)
            {
                if (txtAntecedentesPersonales.Text != "")
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "\r\n10.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesPersonales.Focus();
                    txtAntecedentesPersonales.Text += "10.-";
                    txtAntecedentesPersonales.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbCardiopatia_Click(object sender, EventArgs e)
        {
            if (chbCardiopatia.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n1.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "1.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbHiperT_Click(object sender, EventArgs e)
        {
            if (chbHiperT.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n2.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "2.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbVascular_Click(object sender, EventArgs e)
        {
            if (chbVascular.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n3.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "3.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbMetabolico_Click(object sender, EventArgs e)
        {
            if (chbMetabolico.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n4.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "4.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbCancer_Click(object sender, EventArgs e)
        {
            if (chbCancer.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n5.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "5.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbTuberculosis_Click(object sender, EventArgs e)
        {
            if (chbTuberculosis.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n6.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "6.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbMental_Click(object sender, EventArgs e)
        {
            if (chbMental.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n7.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "7.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbInfeccionsa_Click(object sender, EventArgs e)
        {
            if (chbInfeccionsa.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n8.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "8.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbMalFormado_Click(object sender, EventArgs e)
        {
            if (chbMalFormado.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n9.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "9.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbOtro_Click(object sender, EventArgs e)
        {
            if (chbOtro.Checked == true)
            {
                if (txtAntecedentesFamiliares.Text != "")
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "\r\n10.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
                else
                {
                    txtAntecedentesFamiliares.Focus();
                    txtAntecedentesFamiliares.Text += "10.-";
                    txtAntecedentesFamiliares.Select(txtAntecedentesPersonales.Text.Length, 0);
                }
            }
        }

        private void chbPiel_Click(object sender, EventArgs e)
        {

        }

        private void chb7cp1_Click(object sender, EventArgs e)
        {

        }

        private void chbOjos_Click(object sender, EventArgs e)
        {

        }

        private void chbOidos_Click(object sender, EventArgs e)
        {

        }

        private void chbNariz_Click(object sender, EventArgs e)
        {

        }

        private void chbBoca_Click(object sender, EventArgs e)
        {

        }

        private void chbOrofaringe_Click(object sender, EventArgs e)
        {

        }

        private void chb7cp2_Click(object sender, EventArgs e)
        {

        }

        private void chbAxilas_Click(object sender, EventArgs e)
        {

        }

        private void chb7cp3_Click(object sender, EventArgs e)
        {

        }

        private void chb7cp4_Click(object sender, EventArgs e)
        {

        }

        private void chbColumna_Click(object sender, EventArgs e)
        {

        }

        private void chbIngle_Click(object sender, EventArgs e)
        {

        }

        private void chb7cp6_Click(object sender, EventArgs e)
        {

        }

        private void chbInferior_Click(object sender, EventArgs e)
        {

        }

        private void txtTemperatura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtTempAx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPresionArteria1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPresionArteria2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPulso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFrecuenciaRespiratoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtTalla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPerimetro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtHemoglobina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGlucosa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPulsioximetria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDiametroDer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDiametroIz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
