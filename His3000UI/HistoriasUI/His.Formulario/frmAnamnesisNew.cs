using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeneralApp.ControlesWinForms;
using His.DatosReportes;
using His.Entidades;
using His.Entidades.Clases;
using His.Entidades.Reportes;
using His.Negocio;
using His.Parametros;

namespace His.Formulario
{
    public partial class frmAnamnesisNew : Form
    {
        #region Variables
        private int atencionId;             //codigo de la atencion del paciente
        private bool mostrarInfPaciente = false;    //si se mostrara el panel con la informacion del paciente
        public int codigoAtencion;
        string diagnosticoCIE = string.Empty;
        string codigoCIE = string.Empty;
        private bool isInitialized = false;
        int codigoMedico = 0;
        PACIENTES paciente = null;
        ATENCIONES atencion = null;
        MEDICOS medico = null;
        USUARIOS usu = null;
        private List<DtoCatalogos> ant2;
        private List<DtoCatalogos> antf;
        HC_ANAMNESIS anamnesis = new HC_ANAMNESIS();
        string modo = "SAVE";
        List<HC_ANAMNESIS_DETALLE> detalle = new List<HC_ANAMNESIS_DETALLE>();
        #endregion

        public frmAnamnesisNew(int codigoAtencion, bool parMostrarInfPaciente)
        {
            InitializeComponent();
            isInitialized = true;
            inicializar(codigoAtencion);
            atencionId = codigoAtencion;
            mostrarInfPaciente = parMostrarInfPaciente;

            NegAtenciones atenciones = new NegAtenciones(); //Edgar 20201126
            string estado = atenciones.EstadoCuenta(Convert.ToString(codigoAtencion));
            List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(Sesion.codUsuario);
            bool valido = false;
            if (estado != "1")
            {
                foreach (var item in perfilUsuario)
                {
                    if (item.ID_PERFIL == 31) //validara con codigo
                    {
                        if (item.DESCRIPCION.Contains("HCS")) //valida contra la descripcion
                        {
                            valido = true;
                            habilitaBotones(false, false, true, true, true);
                            break;
                        }
                    }
                    else
                    {
                        if (item.DESCRIPCION.Contains("HCS")) //solo valida contra la descripcion
                        {
                            valido = true;
                            habilitaBotones(false, false, true, true, true);
                            break;
                        }
                    }
                }
                if (!valido)
                    Bloquear();
            }

            //Cambios Edgar 20210303  Requerimientos de la pasteur por Alex.
            if (Sesion.codDepartamento == 6 && !valido)
            {
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = false;
                // btnImprimir.Enabled = false;
            }
        }

        private void inicializar(int codigoAtencion)
        {
            if (codigoAtencion != 0)
            {
                cargarAtencion(codigoAtencion);

            }

            cargarAntecedentesFamiliares();
            cargarAntecedentesPersonales();
            cargarOrganos();
            cargarExamen();
            if (anamnesis != null)
            {
                if (anamnesis.ANE_FECHA.ToString() == null)
                    cargarHora();
            }
            else
                cargarHora();

        }
        private void frmAnamnesisNew_Load(object sender, EventArgs e)
        {
            try
            {
                //Añado el panel con la informaciòn del paciente
                InfPaciente infPaciente = new InfPaciente(atencionId);
                panelInfPaciente.Controls.Add(infPaciente);
                //cambio las dimenciones de los paneles
                panelInfPaciente.Size = new Size(panelInfPaciente.Width, 110);
                //pantab1.Top = 125; editado
                //cargar tamaño por defecto de la vista
                this.Height = this.Height + 110;


                this.WindowState = FormWindowState.Maximized;
                //if (mostrarInfPaciente == true)
                //{

                //}
                //añade a los controles textbox el evento de keypress
                //foreach (Control control in pantab1.Controls)
                //{
                //    if (control.Controls.Count > 0)
                //        recorerControles(control);
                //    else
                //    {
                //        if (control is TextBox)
                //        {
                //            //control.KeyPress += new KeyPressEventHandler(keypressed);
                //        }
                //    }
                //}

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            //try
            //{                
            //    mostrarInfPaciente = false;
            //    foreach (Control control in ultraTabControl1.Controls)
            //    {
            //        if (control.Controls.Count > 0)
            //            recorerControles(control);
            //        else
            //        {
            //            if (control is TextBox)
            //            {
            //                control.KeyPress += new KeyPressEventHandler(keypressed);
            //            }
            //        }
            //    }

            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message);
            //}
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            habilitaBotones(false, true, false, false, false);
            activarFormulario(true);
            if (txt_gesta.Text == "0")
            {
                habilitarCamposGesta(false);
            }
            else
            {
                habilitarCamposGesta(true);
            }
            limpiarCampos();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!controlarCampos())
                {
                    if (!controlarCamposExtensos())
                    {
                        guardarAnamnesis();
                        limpiarCampos();
                        cargarAnamnesis();
                        btnGuardar.Enabled = false;
                        btnImprimir.Enabled = true;
                        btnEditar.Enabled = true;
                        activarFormulario(false);

                        //imprimirReporte("reporte");
                    }
                    else
                        MessageBox.Show("Campos Extensos");
                }
                else if (controlarCampos())
                    MessageBox.Show("Faltan campos por llenar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            btnImprimir.Enabled = false;
            btnEditar.Enabled = false;
            activarFormulario(true);
            modo = "UPDATE";
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimirReporte("reporte");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            activarFormulario(false);
            limpiarCampos();
            habilitaBotones(false, false, false, false, true);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void recorerControles(Control parControl)
        {
            try
            {
                foreach (Control control in parControl.Controls)
                {
                    if (control.Controls.Count > 0)
                        recorerControles(control);
                    else
                    {
                        if (control is TextBox)
                        {
                            control.KeyPress += new KeyPressEventHandler(keypressed);
                        }
                    }

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void keypressed(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }

        }

        public void habilitaBotones(bool nuevo, bool guardar, bool editar, bool imprimir, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnEditar.Enabled = editar;
            btnImprimir.Enabled = imprimir;
            btnCancelar.Enabled = cancelar;
            //panel1.Enabled = guardar;
        }
        private void cargarMedico(int cod)
        {
            medico = NegMedicos.RecuperaMedicoId(cod);
        }
        private void cargarAtencion(int codAtencion)
        {
            try
            {

                atencion = NegAtenciones.RecuperarAtencionID(codAtencion);
                paciente = NegPacientes.RecuperarPacienteID(atencion.PACIENTES.PAC_CODIGO);
                List<MEDICOS> medicos = NegMedicos.listaMedicos();
                codigoMedico = medicos.FirstOrDefault(m => m.EntityKey == atencion.MEDICOSReference.EntityKey).MED_CODIGO;
                //if (codigoMedico != 0) Causa que no se pueda validar una Interconsulta con medico residente Mario Valencia
                cargarMedico(codigoMedico);
                cargarAnamnesis();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar los datos de la atencion, error: " + ex.Message, "error");
            }
        }
        private void cargarAnamnesis()
        {
            try
            {
                anamnesis = NegAnamnesis.recuperarAnamnesisPorAtencion(atencion.ATE_CODIGO);
                //valido si el paciente es hombre o mujer
                //if (paciente.PAC_GENERO.Equals("M"))
                //    grp_mujer.Enabled = false;
                //else
                //    grp_mujer.Enabled = true;

                if (anamnesis == null)
                {
                    modo = "SAVE";
                    habilitaBotones(true, false, false, false, false);
                    activarFormulario(false);
                    txt_profesional.Text = Sesion.nomUsuario;
                }
                else
                {
                    modo = "UPDATE";

                    if (anamnesis.ID_USUARIO == His.Entidades.Clases.Sesion.codUsuario)
                        habilitaBotones(false, false, true, true, true);
                    else
                        habilitaBotones(false, false, false, true, true);
                    activarFormulario(false);
                    txt_problema.Text = anamnesis.ANE_PROBLEMA;
                    txt_presionA.Text = anamnesis.ANE_PRESION_A.ToString();
                    txt_presionB.Text = anamnesis.ANE_PRESION_B.ToString();
                    txt_frecCardiaca.Text = anamnesis.ANE_FREC_CARDIACA;
                    txt_frecRespiratoria.Text = anamnesis.ANE_FREC_RESPIRATORIA;
                    txt_tempBucal.Text = anamnesis.ANE_TEMP_BUCAL.ToString();
                    txt_tempAxilar.Text = anamnesis.ANE_TEMP_AXILAR.ToString();
                    txt_peso.Text = anamnesis.ANE_PESO.ToString();
                    txt_talla.Text = anamnesis.ANE_TALLA.ToString();
                    txtAnalisis.Text = anamnesis.ANE_ANALISIS;
                    txt_OtrosSignos.Text = anamnesis.ANE_OTROS_SIGNOS;
                    txt_perimetro.Text = anamnesis.ANE_PERIMETRO.ToString();
                    txt_tratamiento.Text = anamnesis.ANE_PLAN_TRATAMIENTO;
                    txt_fecha.Text = anamnesis.ANE_FECHA.Value.ToString("dd/MM/yyyy");
                    txt_hora.Text = anamnesis.ANE_FECHA.Value.ToString("HH:mm");
                    if (paciente.PAC_GENERO == "M")
                    {
                        if (Convert.ToString(anamnesis.ANE_FECHA_ULT_ECO_PROSTATICO.Value) != "01/01/1900 00:00:00")
                        {
                            dtp_EcoProstata.Checked = true;
                            dtp_EcoProstata.Value = anamnesis.ANE_FECHA_ULT_ECO_PROSTATICO.Value;
                        }
                        if (Convert.ToString(anamnesis.ANE_FECHA_ULT_ANTIGENO_PROSTATICO.Value) != "01/01/1900 00:00:00")
                        {
                            dtp_AntigenoProstata.Checked = true;
                            dtp_AntigenoProstata.Value = anamnesis.ANE_FECHA_ULT_ANTIGENO_PROSTATICO.Value;
                        }
                    }

                    mskSaturacion.Text = anamnesis.ANE_SATURACION;
                    cargarMotivosConsulta(anamnesis.ANE_CODIGO);
                    cargarDetalles(anamnesis.ANE_CODIGO);
                    cargarDiagnosticos();
                    cargarDA(anamnesis.ANE_CODIGO);
                    //if (grp_mujer.Enabled == true)
                    cargarDatosMujer();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void activarFormulario(bool estado)
        {
            ultraGroupBox2.Enabled = estado;
            ultraGroupBox3.Enabled = estado;
            ultraGroupBox4.Enabled = estado;
            ultraGroupBox5.Enabled = estado;
            ultraGroupBox6.Enabled = estado;
            ultraGroupBox7.Enabled = estado;
            ultraGroupBox8.Enabled = estado;
            ultraGroupBox9.Enabled = estado;
            ultraGroupBox10.Enabled = estado;
            if (paciente.PAC_GENERO.Equals("M"))
            {
                grp_mujer.Enabled = false;
                txt_ScoreMama.Enabled = false;
            }
            else
            {
                grp_mujer.Enabled = estado;
                txt_ScoreMama.Enabled = estado;
            }
            if (paciente.PAC_GENERO.Equals("F")) //Valida si es paciente femenino y habilita los campos de FUAP y FUEP 20240313 Andres Cabrera
                grp_Hombre.Enabled = false;
            else
                grp_Hombre.Enabled = estado;

            ultraGroupBox11.Enabled = estado;
            ultraGroupBox12.Enabled = false;
        }
        private void cargarMotivosConsulta(int codAnamnesis)
        {
            List<HC_ANAMNESIS_MOTIVOS_CONSULTA> amc = NegAnamnesisDetalle.motivosConsultaPorId(codAnamnesis);
            if (amc != null)
            {
                for (int i = 0; i < amc.Count; i++)
                {
                    HC_ANAMNESIS_MOTIVOS_CONSULTA motivo = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                    motivo = amc.ElementAt(i);
                    if (motivo.MOC_TIPO == "1")
                        txt_motivo1.Text = motivo.MOC_DESCRIPCION;
                    else
                    {
                        if (motivo.MOC_TIPO == "2")
                            txt_motivo2.Text = motivo.MOC_DESCRIPCION;
                        else
                        {
                            if (motivo.MOC_TIPO == "3")
                                txt_motivo3.Text = motivo.MOC_DESCRIPCION;
                            else
                            {
                                if (motivo.MOC_TIPO == "4")
                                    txt_motivo4.Text = motivo.MOC_DESCRIPCION;
                                else
                                {
                                    if (motivo.MOC_TIPO == "5")
                                        txt_Motivo5.Text = motivo.MOC_DESCRIPCION;
                                    else
                                    {
                                        if (motivo.MOC_TIPO == "6")
                                            txt_Motivo6.Text = motivo.MOC_DESCRIPCION;
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
        private void cargarDetalles(int codAnamnesis)
        {
            List<HC_ANAMNESIS_DETALLE> det = NegAnamnesisDetalle.listaDetallesAnamnesis(codAnamnesis);

            if (det != null)
            {
                foreach (HC_ANAMNESIS_DETALLE detalle in det)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewComboBoxCell cmbcell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    codigoCell.Value = detalle.ADE_CODIGO;
                    textcell.Value = detalle.ADE_DESCRIPCION;

                    switch (NegCatalogos.listaTipoCatalogos().FirstOrDefault(t => t.EntityKey == NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HC_CATALOGOS_TIPOReference.EntityKey).HCT_CODIGO)
                    {
                        case 2:
                            //ANTECEDENTES PERSONALES

                            cmbcell.DataSource = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            cmbcell.DisplayMember = "HCC_NOMBRE";
                            cmbcell.ValueMember = "HCC_NOMBRE";
                            cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HCC_NOMBRE;
                            fila.Cells.Add(codigoCell);
                            fila.Cells.Add(cmbcell);
                            fila.Cells.Add(textcell);
                            dtg_antec_personales.Rows.Add(fila);
                            break;

                        case 3:
                            //ANTECEDENTES FAMILIARES
                            cmbcell.DataSource = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            cmbcell.DisplayMember = "HCC_NOMBRE";
                            cmbcell.ValueMember = "HCC_NOMBRE";
                            cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HCC_NOMBRE;
                            fila.Cells.Add(codigoCell);
                            fila.Cells.Add(cmbcell);
                            fila.Cells.Add(textcell);
                            dtg_antec_familiares.Rows.Add(fila);
                            break;

                        case 4:
                            //ORGANOS Y SISTEMAS
                            DataGridViewCheckBoxCell chk1 = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell chk2 = new DataGridViewCheckBoxCell();
                            cmbcell.DataSource = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            cmbcell.DisplayMember = "HCC_NOMBRE";
                            cmbcell.ValueMember = "HCC_NOMBRE";
                            cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HCC_NOMBRE;
                            //detalle.ADE_TIPO = null; //Inicializa el ade_tipo en null para evitar que se inserte este campo5
                            fila.Cells.Add(codigoCell);
                            fila.Cells.Add(cmbcell);
                            //fila.Cells.Add(chk11); Se comentan estas lineas para no afectar a las filas Andres 20240315
                            //fila.Cells.Add(chk22);
                            fila.Cells.Add(textcell);
                            dtg_organos.Rows.Add(fila);
                            break;

                        case 5:
                            DataGridViewCheckBoxCell chk11 = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell chk22 = new DataGridViewCheckBoxCell();
                            //EXAMEN FISICO
                            //chk11 = new DataGridViewCheckBoxCell();
                            //chk22 = new DataGridViewCheckBoxCell();
                            cmbcell.DataSource = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            cmbcell.DisplayMember = "HCC_NOMBRE";
                            cmbcell.ValueMember = "HCC_NOMBRE";
                            cmbcell.Value = NegCatalogos.RecuperarCatalogoPorID(NegCatalogos.listaCatalogos().FirstOrDefault(i => i.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO).First().HCC_NOMBRE;
                            //detalle.ADE_TIPO = null; //Inicializa el ade_tipo en null para evitar que se inserte este campo5
                            fila.Cells.Add(codigoCell);
                            fila.Cells.Add(cmbcell);
                            //fila.Cells.Add(chk11); Se comentan estas lineas para no afectar a las filas Andres 20240315
                            //fila.Cells.Add(chk22);
                            fila.Cells.Add(textcell);
                            dtg_examenFisico.Rows.Add(fila);
                            break;
                    }
                }
            }
        }
        private void cargarDiagnosticos()
        {
            List<HC_ANAMNESIS_DIAGNOSTICOS> diag = NegAnamnesisDetalle.recuperarDiagnosticosAnamnesis(anamnesis.ANE_CODIGO);
            if (diag != null)
            {
                foreach (HC_ANAMNESIS_DIAGNOSTICOS diagnosticos in diag)
                {
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell1 = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell2 = new DataGridViewTextBoxCell();
                    DataGridViewCheckBoxCell chkcell = new DataGridViewCheckBoxCell();
                    DataGridViewCheckBoxCell chkcell2 = new DataGridViewCheckBoxCell();
                    textcell.Value = diagnosticos.CDA_CODIGO;
                    textcell1.Value = diagnosticos.CIE_CODIGO;
                    textcell2.Value = diagnosticos.CDA_DESCRIPCION;
                    if (diagnosticos.CDA_ESTADO.Value)
                    {
                        chkcell.Value = true;
                        chkcell2.Value = false;
                    }
                    else
                    {
                        chkcell2.Value = true;
                        chkcell.Value = false;
                    }
                    fila.Cells.Add(textcell);
                    fila.Cells.Add(textcell1);
                    fila.Cells.Add(textcell2);
                    fila.Cells.Add(chkcell);
                    fila.Cells.Add(chkcell2);
                    dtg_diagnosticos.Rows.Add(fila);
                }
            }
        }
        private void cargarDA(int ANE_codigo)
        {


            DtoAnamnesis_DA DA = null;
            DA = NegAnamnesis.getDA(ANE_codigo);

            if (DA != null)
            {
                txt_profesional.Text = DA.MEDICO;
            }
            else
            {
                txt_profesional.Text = Sesion.nomUsuario;
            }
        }

        private void habilitarCamposGesta(bool estado)
        {

            txt_ScoreMama.Enabled = estado;
            dtp_ultimoParto.Enabled = estado;
            grpGesta.Enabled = estado;
        }
        private void cargarDatosMujer()
        {
            try
            {
                HC_ANAMNESIS_ANTEC_MUJER datosMujer = NegAnamnesis.recuperarAnamnesisDatosMujer(anamnesis.ANE_CODIGO);
                if (datosMujer != null)
                {
                    chk_biopsia.Checked = (bool)datosMujer.AMU_BIOPSIA;
                    txt_ciclos.Text = datosMujer.AMU_CICLOS.ToString();
                    chk_colcoscopia.Checked = (bool)datosMujer.AMU_COLPOSCOPIA;
                    if (datosMujer.AMU_CICLOS_DETALLE == "R")
                        regularButton.Checked = true;
                    else
                        irregularButton.Checked = true;
                    if (Convert.ToString(datosMujer.AMU_FUM.Value) != "01/01/1900 00:00:00")
                    {
                        dtp_ultimaMenst.Checked = true;
                        dtp_ultimaMenst.Value = datosMujer.AMU_FUM.Value;
                    }
                    else
                        dtp_ultimaMenst.Checked = false;

                    //dtp_ultimaMenst.Value = (DateTime)datosMujer.AMU_FUM;
                    chk_mamografia.Checked = (bool)datosMujer.AMU_MAMOGRAFIA;
                    txt_menarquia.Text = datosMujer.AMU_MENARQUIA.ToString();
                    txt_menopausia.Text = datosMujer.AMU_MENOPAUSIA.ToString();
                    txt_prevencion.Text = datosMujer.AMU_MET_PREVENCION;
                    if (datosMujer.AMU_GESTA.ToString() == "0") //Habilita o deshabilita los campos referenciados a las gestas de la mujer
                    {
                        habilitarCamposGesta(false);
                        //txt_abortos.Enabled = false;
                        //txt_hijosvivos.Enabled = false;
                        //txt_ScoreMama.Enabled = false;
                        //txt_partos.Enabled = false;
                        //txt_cesareas.Enabled = false;
                        //dtp_ultimoParto.Enabled = false;
                    }
                    else
                    {
                        habilitarCamposGesta(true);
                        txt_gesta.Text = datosMujer.AMU_GESTA.ToString();
                        txt_abortos.Text = datosMujer.AMU_ABORTOS.ToString();
                        txt_hijosvivos.Text = datosMujer.AMU_HIJOSVIVOS.ToString();
                        txt_ScoreMama.Text = datosMujer.AMU_SCORE_MAMA.ToString();
                        txt_partos.Text = datosMujer.AMU_PARTOS.ToString();
                        txt_cesareas.Text = datosMujer.AMU_CESAREAS.ToString();
                        dtp_ultimoParto.Value = (DateTime)datosMujer.AMU_FUP;
                    }
                    //aaam.AMU_FUC = dtp_ultimaCito.Checked ==true?dtp_ultimaCito.Value: Convert.ToDateTime("01/01/1900");                    
                    if (Convert.ToString(datosMujer.AMU_FUC.Value) != "01/01/1900 00:00:00")
                    {
                        dtp_ultimaCito.Checked = true;
                        dtp_ultimaCito.Value = datosMujer.AMU_FUC.Value;
                    }
                    else
                        dtp_ultimaCito.Checked = false;

                    if (Convert.ToString(datosMujer.AMU_FECHA_COLPOSCOPIA.Value) != "01/01/1900 00:00:00")
                    {
                        dtp_FUColposcopia.Checked = true;
                        dtp_FUColposcopia.Value = datosMujer.AMU_FECHA_COLPOSCOPIA.Value;
                    }
                    else
                        dtp_FUColposcopia.Checked = false;

                    if (Convert.ToString(datosMujer.AMU_FECHA_MAMOGRAFIA.Value) != "01/01/1900 00:00:00")
                    {
                        dtp_FUMamografia.Checked = true;
                        dtp_FUMamografia.Value = datosMujer.AMU_FECHA_MAMOGRAFIA.Value;
                    }
                    else
                        dtp_FUMamografia.Checked = false;

                    txt_EdadVidaSexual.Text = datosMujer.AMU_EDAD_VIDA_SEXUAL.ToString();
                    chk_terapia.Checked = (bool)datosMujer.AMU_TERAPIAHORMONAL;
                    chk_vidasexual.Checked = (bool)datosMujer.AMU_VIDASEXUAL;
                }
                else
                {
                    txt_gesta.Text = "0";
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void cargarOrganos()
        {
            try
            {
                List<DtoCatalogos> org = NegCatalogos.RecuperarCatalogosPorTipo(4);
                List<DtoCatalogos> org2 = org.Where(item => item.HCC_NOMBRE != "GENITAL." && item.HCC_NOMBRE != "URINARIO.").ToList();
                tipoOS.DataSource = org2;
                tipoOS.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void cargarAntecedentesPersonales()
        {
            try
            {
                ant2 = NegCatalogos.RecuperarCatalogosPorTipo(2);


                List<DtoCatalogos> ant1 = ant2.Where(item => item.HCC_NOMBRE != "ENF. PERINATAL" && item.HCC_NOMBRE != "ENF. INFANCIA"
                && item.HCC_NOMBRE != "ENF. ADOLESCENTE" && item.HCC_NOMBRE != "ENF. ALERGICA" && item.HCC_NOMBRE != "ENF. CARDIACA"
                && item.HCC_NOMBRE != "ENF. RESPIRATORIA"
                && item.HCC_NOMBRE != "ENF. DIGESTIVA"
                && item.HCC_NOMBRE != "ENF. NEUROLOGICA"
                && item.HCC_NOMBRE != "ENF. METABOLICA"
                && item.HCC_NOMBRE != "ENF. HEMO LINF."
                && item.HCC_NOMBRE != "ENF. URINARIA"
                && item.HCC_NOMBRE != "ENF. TRAUMATOLOGICA"
                && item.HCC_NOMBRE != "ENF. MENTAL"
                && item.HCC_NOMBRE != "ENF. T. SEXUAL"
                && item.HCC_NOMBRE != "ENF. QUIRURGICA"
                && item.HCC_NOMBRE != "TENDENCIA SEXUAL"
                && item.HCC_NOMBRE != "RIESGO SOCIAL"
                && item.HCC_NOMBRE != "RIESGO LABORAL"
                && item.HCC_NOMBRE != "RIESGO FAMILIAR"
                && item.HCC_NOMBRE != "ACTIVIDAD FISICA"
                && item.HCC_NOMBRE != "OTROS"
                                ).ToList();


                tipoAP.DataSource = ant1;
                tipoAP.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void cargarAntecedentesFamiliares()
        {
            try
            {
                antf = NegCatalogos.RecuperarCatalogosPorTipo(3);
                List<DtoCatalogos> ant1 = antf.Where(item => item.HCC_NOMBRE != "DIABETES").ToList();

                tipoAF.DataSource = ant1;
                tipoAF.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }
        private void cargarExamen()
        {
            try
            {
                List<DtoCatalogos> exa = NegCatalogos.RecuperarCatalogosPorTipo(5);
                tipo_examen.DataSource = exa;
                tipo_examen.DisplayMember = "HCC_NOMBRE";
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }
        private void soloNumeros(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == true)
            { }
            else if (e.KeyChar == 46)
            { }
            else if (e.KeyChar == 44)
            { }
            else if (e.KeyChar == '\b')
            { }
            else
            { e.Handled = true; }
        }
        private void cargarHora()
        {
            DateTime dt = new DateTime();
            txt_fecha.Text = System.DateTime.Now.ToString("yyyy/MM/dd");
            txt_hora.Text = System.DateTime.Now.ToString("HH:mm:ss");
        }
        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");
        }

        private void limpiarCampos()
        {
            foreach (Control c in this.ultraTabControl1.Controls)
                foreach (Control control in c.Controls)
                    foreach (Control cajas in control.Controls)
                        if (cajas.Name.Substring(0, 3).Equals("txt"))
                            cajas.Text = "";
                        else
                            if (cajas.Name.Substring(0, 3).Equals("dtg"))
                        {
                            //DataGridView dtg = (DataGridView)cajas;
                            //for (int i = 0; i < dtg.Rows.Count; i++)
                            //    dtg.Rows[i].Cells.Remove(dtg.Rows[i].Cells[i]);
                        }
                        else
                                if (cajas.Name.Substring(0, 3).Equals("grp"))
                            foreach (Control txt in cajas.Controls)
                                if (txt.Name.Substring(0, 3).Equals("txt"))
                                    txt.Text = "";
            cargarHora();
            //añado 0 por defecto en los campos numericos
            txt_presionA.Text = "0";
            txt_presionB.Text = "0";
            //txt_frecCardiaca.Text = "0";
            //txt_frecRespiratoria.Text = "0";
            //txt_tempBucal.Text = "0";
            txt_peso.Text = "0";
            txt_talla.Text = "0";
            txtIndiceMasaCorporal.Text = "0";
            txt_perimetro.Text = "0";
            //txt_tempAxilar.Text = "0";
            txt_menarquia.Text = "0";
            txt_menopausia.Text = "0";
            txt_EdadVidaSexual.Text = "0";
            txt_OtrosSignos.Text = "";
            txt_ciclos.Text = "0";
            txt_gesta.Text = "0";
            txt_partos.Text = "0";
            txt_abortos.Text = "0";
            txt_cesareas.Text = "0";
            txt_ScoreMama.Text = "0";
            txt_hijosvivos.Text = "0";
            txt_profesional.Text = Sesion.nomUsuario;
            dtg_antec_familiares.Rows.Clear();
            dtg_antec_personales.Rows.Clear();
            dtg_diagnosticos.Rows.Clear();
            dtg_examenFisico.Rows.Clear();
            dtg_organos.Rows.Clear();
        }
        private bool controlarCampos()
        {
            error.Clear();
            bool flag = false;
            if (txt_motivo1.Text == string.Empty)
            {
                AgregarError(txt_motivo1);
                flag = true;
            }
            if (txt_problema.Text == string.Empty)
            {
                AgregarError(txt_problema);
                flag = true;
            }
            if (txtAnalisis.Text == string.Empty)
            {
                AgregarError(txtAnalisis);
                flag = true;
            }
            if (Convert.ToDecimal(txt_presionA.Text) <= 0)
            {
                AgregarError(txt_presionA);
                flag = true;
            }
            if (Convert.ToDecimal(txt_presionB.Text) <= 0)
            {
                AgregarError(txt_presionB);
                flag = true;
            }
            if (txt_frecCardiaca.Text == "")
            {
                AgregarError(txt_frecCardiaca);
                flag = true;
            }
            if (txt_frecRespiratoria.Text == "")
            {
                AgregarError(txt_frecRespiratoria);
                flag = true;
            }

            //if (txt_tempBucal.Text =="" && txt_tempAxilar.Text == "")
            //{
            //    if (txt_tempBucal.Text == "")
            //    {
            //        AgregarError(txt_tempBucal);
            //        flag = true;
            //    }
            //    if (txt_tempAxilar.Text == "")
            //    {
            //        AgregarError(txt_tempAxilar);
            //        flag = true;
            //    }
            //}
            if (txt_perimetro.Text == "")
            {
                AgregarError(txt_perimetro);
                flag = true;
            }
            if (txt_talla.Text == "")
            {
                AgregarError(txt_talla);
                flag = true;
            }
            if (txt_peso.Text == "")
            {
                AgregarError(txt_peso);
                flag = true;
            }
            if (dtg_diagnosticos.Rows.Count == 1)
            {
                AgregarError(dtg_diagnosticos);
                flag = true;
            }
            if (dtg_antec_familiares.Rows.Count == 1)
            {
                AgregarError(dtg_antec_familiares);
                flag = true;
            }
            if (dtg_antec_personales.Rows.Count == 1)
            {
                AgregarError(dtg_antec_personales);
                flag = true;
            }
            if (dtg_examenFisico.Rows.Count == 1)
            {
                AgregarError(dtg_examenFisico);
                flag = true;
            }
            if (dtg_organos.Rows.Count == 1)
            {
                AgregarError(dtg_organos);
                flag = true;
            }

            foreach (DataGridViewRow fila in dtg_diagnosticos.Rows)
            {
                DataGridViewCheckBoxCell txtcell = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[fila.Index].Cells[3];
                DataGridViewCheckBoxCell txtcell2 = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[fila.Index].Cells[4];
                DataGridViewTextBoxCell caja = (DataGridViewTextBoxCell)this.dtg_diagnosticos.Rows[fila.Index].Cells[0];
                if ((txtcell.Value == null) && (txtcell2.Value == null) && (caja.Value != null))
                {
                    AgregarError(dtg_diagnosticos);
                    flag = true;
                }
                //if (dtg_diagnosticos.Rows[i].Cells[3].Value == null && dtg_diagnosticos.Rows[i].Cells[4].Value == null)
                //{
                //    AgregarError(dtg_diagnosticos);
                //    flag = true;
                //}
                //else
                //{
                //    if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value == true)
                //    {
                //        dtg_diagnosticos.Rows[i].Cells[4].Value = false;
                //    }
                //    else
                //    {
                //        dtg_diagnosticos.Rows[i].Cells[3].Value = false;
                //    }
                //}
            }

            dtg_antec_personales.Refresh();

            if (dtg_antec_personales.Rows.Count > 1)
            {
                for (int i = 0; i < dtg_antec_personales.Rows.Count - 1; i++)
                {
                    if (dtg_antec_personales.Rows[i].Cells[2].Value == null)
                    {
                        error.SetError(dtg_antec_personales, "Debe agregar el antecedente personal");
                        flag = true;
                    }

                }
            }

            if (dtg_antec_familiares.Rows.Count > 1)
            {
                for (int i = 0; i < dtg_antec_familiares.Rows.Count - 1; i++)
                {
                    if (dtg_antec_familiares.Rows[i].Cells[2].Value == null)
                    {
                        error.SetError(dtg_antec_familiares, "Debe agregar el antecednete familiar.");
                        flag = true;
                    }

                }
            }
            if (dtg_organos.Rows.Count > 1)
            {
                for (int i = 0; i < dtg_organos.Rows.Count - 1; i++)
                {
                    if (dtg_organos.Rows[i].Cells[2].Value == null)
                    {
                        error.SetError(dtg_organos, "Debe agregar el detalle de la revisión de Órganos y Sistemas.");
                        flag = true;
                    }

                }
            }

            if (dtg_examenFisico.Rows.Count > 1)
            {
                for (int i = 0; i < dtg_examenFisico.Rows.Count - 1; i++)
                {
                    if (dtg_examenFisico.Rows[i].Cells[2].Value == null)
                    {
                        error.SetError(dtg_examenFisico, "Debe agregar el examen físico.");
                        flag = true;
                    }

                }
            }


            if (txt_tratamiento.Text == string.Empty)
            {
                AgregarError(txt_tratamiento);
                flag = true;
            }
            return flag;
        }
        private bool controlarCamposExtensos()
        {
            bool flag = false;
            if (txt_problema.TextLength <= 2000)
            {
                int cont = 0;
                foreach (string s in txt_problema.Text.Split('\n'))
                    cont++;
                if (cont > 14)
                {
                    AgregarError(txt_problema);
                    flag = true;
                }
            }
            else
            {
                AgregarError(txt_problema);
                flag = true;
            }
            return flag;
        }
        private void guardarAnamnesis()
        {
            try
            {
                if (anamnesis == null)
                    anamnesis = new HC_ANAMNESIS();

                anamnesis.PACIENTESReference.EntityKey = paciente.EntityKey;
                anamnesis.ATENCIONESReference.EntityKey = atencion.EntityKey;
                anamnesis.ANE_PROBLEMA = txt_problema.Text;
                anamnesis.ANE_FREC_CARDIACA = txt_frecCardiaca.Text;
                anamnesis.ANE_FREC_RESPIRATORIA = txt_frecRespiratoria.Text;
                anamnesis.ANE_TEMP_BUCAL = Convert.ToDecimal(txt_tempBucal.Value);
                anamnesis.ANE_TEMP_AXILAR = Convert.ToDecimal(txt_tempAxilar.Value);
                anamnesis.ANE_PESO = Convert.ToDecimal(txt_peso.Text);
                anamnesis.ANE_TALLA = Convert.ToDecimal(txt_talla.Text);
                anamnesis.ANE_ANALISIS = txtAnalisis.Text; //agrega el campo ANALISIS para que se almacene 20240306 Andres Cabrera
                anamnesis.ANE_OTROS_SIGNOS = txt_OtrosSignos.Text; //Se agrega el campo otros en signos vitales 20240313 Andres Cabrera
                if (grp_Hombre.Enabled == true)
                {
                    anamnesis.ANE_FECHA_ULT_ANTIGENO_PROSTATICO = dtp_AntigenoProstata.Checked == true ? dtp_AntigenoProstata.Value : Convert.ToDateTime("01/01/1900 00:00:00");

                    anamnesis.ANE_FECHA_ULT_ECO_PROSTATICO = dtp_EcoProstata.Checked == true ? dtp_EcoProstata.Value : Convert.ToDateTime("01/01/1900 00:00:00");
                }
                if (txt_perimetro.Text != "")
                    anamnesis.ANE_PERIMETRO = Convert.ToDecimal(txt_perimetro.Text);
                anamnesis.ANE_PLAN_TRATAMIENTO = txt_tratamiento.Text;
                anamnesis.ANE_FECHA = Convert.ToDateTime(DateTime.Now);
                anamnesis.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                anamnesis.ANE_PRESION_A = Convert.ToInt32(txt_presionA.Text);
                anamnesis.ANE_PRESION_B = Convert.ToInt32(txt_presionB.Text);
                anamnesis.ANE_SATURACION = mskSaturacion.Text;

                if (modo == "SAVE")
                {
                    anamnesis.ANE_CODIGO = NegAnamnesis.ultimoCodigo() + 1;
                    NegAnamnesis.crearAnamnesis(anamnesis);
                    guardarMotivosConsulta();
                    guardarDetalles();
                    MessageBox.Show("Registro Guardado", "ANAMNESIS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                    if (modo == "UPDATE")
                {

                    NegAnamnesis.actualizarAnamnesis(anamnesis);
                    //actualizarDetalles(anamnesis.ANE_CODIGO);
                    actualizarMotivosConsulta(anamnesis.ANE_CODIGO);
                    guardarDetalles();
                    MessageBox.Show("Registro Actualizado", "ANAMNESIS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                guardarDatosAdicionales(anamnesis.ANE_CODIGO, modo);
                btnGuardar.Enabled = false;
                btnImprimir.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }
        private void actualizarMotivosConsulta(int codAnamnesis) //El método solo actualiza si previamente tiene campos alamcenados
        {
            List<HC_ANAMNESIS_MOTIVOS_CONSULTA> mot = NegAnamnesisDetalle.listaMotivosConsulta(codAnamnesis);

            if (mot.Count > 0)
            {
                for (int i = 0; i < mot.Count; i++)
                {
                    HC_ANAMNESIS_MOTIVOS_CONSULTA motivo = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                    motivo = mot.ElementAt(i);
                    if (motivo.MOC_TIPO == "1")
                        motivo.MOC_DESCRIPCION = txt_motivo1.Text.Trim();
                    if (motivo.MOC_TIPO == "2")
                        motivo.MOC_DESCRIPCION = txt_motivo2.Text.Trim();
                    if (motivo.MOC_TIPO == "3")
                        motivo.MOC_DESCRIPCION = txt_motivo3.Text.Trim();
                    if (motivo.MOC_TIPO == "4")
                        motivo.MOC_DESCRIPCION = txt_motivo4.Text.Trim();
                    if (motivo.MOC_TIPO == "5")
                        motivo.MOC_DESCRIPCION = txt_Motivo5.Text.Trim();
                    if (motivo.MOC_TIPO == "6")
                        motivo.MOC_DESCRIPCION = txt_Motivo6.Text.Trim();
                    motivo.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                    NegAnamnesisDetalle.actualizarMotivosConsulta(motivo);
                }
            }
            else
            {
                guardarMotivosConsulta();
            }
        }
        private void guardarDetalles()
        {
            //ANTECEDENTES PERSONALES
            try
            {
                dtg_antec_personales.Refresh();
                foreach (DataGridViewRow fila in dtg_antec_personales.Rows)
                {

                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
                            if (fila.Cells[2].Value != null)
                                detalle.ADE_DESCRIPCION = fila.Cells[2].Value.ToString();
                            else
                                detalle.ADE_DESCRIPCION = "";
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            //select * from HC_CATALOGOS where HCT_TIPO = 2
                            HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;

                            if (fila.Cells[0].Value != null)
                            {
                                //se actualiza el detalle  
                                detalle.ADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegAnamnesisDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                //Se crea el detalle.
                                detalle.ADE_CODIGO = NegAnamnesisDetalle.ultimoCodigo() + 1;
                                NegAnamnesisDetalle.crearAnamnesisDetalle(detalle);
                            }
                        }
                    }
                }
                //ANTECEDENTES FAMILIARES
                dtg_antec_familiares.Refresh();
                foreach (DataGridViewRow fila in dtg_antec_familiares.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
                            if (fila.Cells[2].Value != null)
                                detalle.ADE_DESCRIPCION = fila.Cells[2].Value.ToString();
                            else
                                detalle.ADE_DESCRIPCION = "";
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            if (fila.Cells[0].Value != null)
                            {
                                //se actualiza el detalle para que valide siempre y no cree uno nuevo.
                                detalle.ADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegAnamnesisDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                //Se crea el detalle.
                                detalle.ADE_CODIGO = NegAnamnesisDetalle.ultimoCodigo() + 1;
                                NegAnamnesisDetalle.crearAnamnesisDetalle(detalle);
                            }
                        }
                    }
                }
                //ANTECEDENTES FAMILIARES COMENTADO TEMPORALMENTE HASTA PRUEBAS
                //foreach (DataGridViewRow fila in dtg_antec_familiares.Rows)
                //{
                //    if (fila != null)
                //    {
                //        if (fila.Cells[1].Value != null)
                //        {
                //            HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
                //            if (fila.Cells[2].Value != null)
                //                detalle.ADE_DESCRIPCION = fila.Cells[2].Value.ToString();
                //            else
                //                detalle.ADE_DESCRIPCION = "";
                //            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                //            HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                //            detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                //            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //            if (fila.Cells[0].Value != null)
                //            {
                //                detalle.ADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                //                NegAnamnesisDetalle.actualizarDetalle(detalle);
                //            }
                //            else
                //            {
                //                detalle.ADE_CODIGO = NegAnamnesisDetalle.ultimoCodigo() + 1;
                //                NegAnamnesisDetalle.crearAnamnesisDetalle(detalle);
                //            }
                //        }
                //    }
                //}
                //ORGANOS

                foreach (DataGridViewRow fila in dtg_organos.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
                            if (fila.Cells[2].Value != null)
                                detalle.ADE_DESCRIPCION = fila.Cells[2].Value.ToString();
                            else
                                detalle.ADE_DESCRIPCION = "";
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            //detalle.ADE_TIPO = null;
                            if (fila.Cells[0].Value != null)
                            {
                                //se actualiza el detalle para que valide siempre y no cree uno nuevo.
                                detalle.ADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegAnamnesisDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                //Se crea el detalle.
                                detalle.ADE_CODIGO = NegAnamnesisDetalle.ultimoCodigo() + 1;
                                NegAnamnesisDetalle.crearAnamnesisDetalle(detalle);
                            }
                        }
                    }
                }
                //EXAMEN FISICO
                foreach (DataGridViewRow fila in dtg_examenFisico.Rows)
                {

                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
                            if (fila.Cells[2].Value != null)
                                detalle.ADE_DESCRIPCION = fila.Cells[2].Value.ToString();
                            else
                                detalle.ADE_DESCRIPCION = "";
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            HC_CATALOGOS hc = NegCatalogos.RecuperarCatalogoPorNombre(fila.Cells[1].Value.ToString());
                            detalle.HC_CATALOGOSReference.EntityKey = hc.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            //detalle.ADE_TIPO = null;
                            if (fila.Cells[0].Value != null)
                            {
                                //se actualiza el detalle para que valide siempre y no cree uno nuevo.
                                detalle.ADE_CODIGO = Convert.ToInt32(fila.Cells[0].Value);
                                NegAnamnesisDetalle.actualizarDetalle(detalle);
                            }
                            else
                            {
                                //Se crea el detalle.
                                detalle.ADE_CODIGO = NegAnamnesisDetalle.ultimoCodigo() + 1;
                                NegAnamnesisDetalle.crearAnamnesisDetalle(detalle);
                            }
                        }
                    }
                }
                //DIAGNOSTICOss
                foreach (DataGridViewRow fila in dtg_diagnosticos.Rows)
                {
                    if (fila != null)
                    {
                        if (fila.Cells[1].Value != null)
                        {
                            HC_ANAMNESIS_DIAGNOSTICOS detalle = new HC_ANAMNESIS_DIAGNOSTICOS();
                            detalle.CIE_CODIGO = fila.Cells[1].Value.ToString();
                            if (Convert.ToBoolean(fila.Cells[3].Value))
                                detalle.CDA_ESTADO = true;
                            else
                                detalle.CDA_ESTADO = false;
                            detalle.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            detalle.CDA_DESCRIPCION = fila.Cells[2].Value.ToString();
                            if (fila.Cells[0].Value != null)
                            {
                                HC_ANAMNESIS_DIAGNOSTICOS objcont = NegAnamnesisDetalle.recuperarDiagnosticosAnamnesisCodigo();
                                detalle.CDA_CODIGO = Convert.ToInt32(fila.Cells[0].Value.ToString());
                                NegAnamnesisDetalle.actualizarAnamnesisDiagnosticos(detalle);
                                //NegAnamnesisDetalle.crearAnamnesisDiagnosticos(detalle);
                            }
                            else
                                NegAnamnesisDetalle.crearAnamnesisDiagnosticos(detalle);
                        }
                    }
                }
                //guardarMotivosConsulta();
                if (grp_mujer.Enabled)
                    guardarDatosMujer();
            }
            catch (Exception err) { MessageBox.Show("error", err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void guardarMotivosConsulta()
        {
            HC_ANAMNESIS_MOTIVOS_CONSULTA amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
            if (txt_motivo1.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_motivo1.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "1";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
            if (txt_motivo2.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_motivo2.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "2";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
            if (txt_motivo3.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_motivo3.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "3";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
            if (txt_motivo4.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_motivo4.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "4";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
            if (txt_Motivo5.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_Motivo5.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "5";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
            if (txt_Motivo6.Text != "")
            {
                amc = new HC_ANAMNESIS_MOTIVOS_CONSULTA();
                amc.MOC_CODIGO = NegAnamnesis.ultimoCodigoMotivoConsuta() + 1;
                amc.MOC_DESCRIPCION = txt_Motivo6.Text.Trim();
                amc.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                amc.MOC_TIPO = "6";
                NegAnamnesisDetalle.crearAnamnesisConsultas(amc);
            }
        }


        private void guardarDatosMujer()
        {
            try
            {
                bool accion = false;
                HC_ANAMNESIS_ANTEC_MUJER aaam = NegAnamnesis.recuperarAnamnesisDatosMujer(anamnesis.ANE_CODIGO);
                if (aaam == null)
                {
                    accion = true;
                    aaam = new HC_ANAMNESIS_ANTEC_MUJER();
                    aaam.AMU_CODIGO = NegAnamnesis.ultimoCodigoMujer() + 1;
                }
                aaam.AMU_BIOPSIA = chk_biopsia.Checked;
                aaam.AMU_CICLOS = Convert.ToInt32(txt_ciclos.Text); //Cambiar el tipo de dato a booleano para que se pueda escoger entre regular o irregular o crear nueva lógica
                aaam.AMU_COLPOSCOPIA = chk_colcoscopia.Checked;
                aaam.AMU_FUM = dtp_ultimaMenst.Checked == true ? dtp_ultimaMenst.Value : Convert.ToDateTime("01/01/1900  00:00:00");
                aaam.AMU_MAMOGRAFIA = chk_mamografia.Checked;
                aaam.AMU_MENARQUIA = Convert.ToInt32(txt_menarquia.Text);
                aaam.AMU_MENOPAUSIA = Convert.ToInt32(txt_menopausia.Text);
                aaam.AMU_MET_PREVENCION = txt_prevencion.Text;
                aaam.AMU_TERAPIAHORMONAL = chk_terapia.Checked;
                aaam.AMU_VIDASEXUAL = chk_vidasexual.Checked;
                aaam.AMU_EDAD_VIDA_SEXUAL = Convert.ToInt32(txt_EdadVidaSexual.Text); //Campo añadido Andres 18032024 
                aaam.HC_ANAMNESISReference.EntityKey = anamnesis.EntityKey;
                aaam.AMU_ABORTOS = Convert.ToInt32(txt_abortos.Text);
                aaam.AMU_CESAREAS = Convert.ToInt32(txt_cesareas.Text);
                aaam.AMU_GESTA = Convert.ToInt32(txt_gesta.Text);
                aaam.AMU_HIJOSVIVOS = Convert.ToInt32(txt_hijosvivos.Text);
                aaam.AMU_PARTOS = Convert.ToInt32(txt_partos.Text);
                aaam.AMU_SCORE_MAMA = Convert.ToInt32(txt_ScoreMama.Text);
                if (dtp_ultimaMenst.Checked == true)
                {

                    if (regularButton.Checked)
                        aaam.AMU_CICLOS_DETALLE = "R";
                    else
                        aaam.AMU_CICLOS_DETALLE = "I";
                }
                aaam.AMU_FUP = dtp_ultimoParto.Value;

                aaam.AMU_FECHA_COLPOSCOPIA = dtp_FUColposcopia.Checked == true ? dtp_FUColposcopia.Value : Convert.ToDateTime("01/01/1900 00:00:00");//Campo añadido Andres 18032024 
                aaam.AMU_FECHA_MAMOGRAFIA = dtp_FUMamografia.Checked == true ? dtp_FUMamografia.Value : Convert.ToDateTime("01/01/1900 00:00:00"); //Campo añadido Andres 18032024 
                aaam.AMU_FUC = dtp_ultimaCito.Checked == true ? dtp_ultimaCito.Value : Convert.ToDateTime("01/01/1900 00:00:00");
                if (accion == true)
                {
                    NegAnamnesis.crearAntecedentesMujer(aaam);
                }
                else
                {
                    NegAnamnesis.actualizarAnamnesisDatosMujer(aaam);
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }
        private void guardarDatosAdicionales(int ane_codigo, string modo)
        {
            DtoAnamnesis_DA x = new DtoAnamnesis_DA();
            x.ANE_CODIGO = ane_codigo;
            x.MEDICO = txt_profesional.Text.Trim();
            NegAnamnesis.saveDA(x, modo);
        }
        public void Bloquear()
        {
            btnEditar.Enabled = false;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = false;
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

                #region Imprimir 
                DS_Anamnesis ana = new DS_Anamnesis();
                DataRow dr;

                if (codigoMedico != 0)
                    medico = NegMedicos.RecuperaMedicoId(codigoMedico);
                dr = ana.Tables["Anamnesis"].NewRow();
                dr["path"] = NegUtilitarios.RutaLogo("General");
                dr["establecimiento"] = Sesion.nomEmpresa;
                dr["nombre1"] = (paciente.PAC_NOMBRE1).Replace("'", "´");
                dr["nombre2"] = (paciente.PAC_NOMBRE2).Replace("'", "´");
                dr["apellido1"] = (paciente.PAC_APELLIDO_PATERNO).Replace("'", "´");
                dr["apellido2"] = (paciente.PAC_APELLIDO_MATERNO).Replace("'", "´");
                dr["sexo"] = paciente.PAC_GENERO;
                dr["edad"] = calcularEdad(Convert.ToDateTime(paciente.PAC_FECHA_NACIMIENTO));
                dr["ci"] = paciente.PAC_IDENTIFICACION;
                if (!NegParametros.ParametroFormularios())
                    dr["hc"] = paciente.PAC_HISTORIA_CLINICA.Trim() + "-" + atencion.ATE_NUMERO_ATENCION.Trim();
                else
                    dr["hc"] = paciente.PAC_IDENTIFICACION;
                dr["mcA"] = (this.txt_motivo1.Text).Replace("'", "´");
                dr["mcB"] = (txt_motivo2.Text).Replace("'", "´");
                dr["mcC"] = (this.txt_motivo3.Text).Replace("'", "´");
                dr["mcD"] = (txt_motivo4.Text).Replace("'", "´");
                dr["mcE"] = (this.txt_Motivo5.Text).Replace("'", "´");
                dr["mcf"] = (txt_Motivo6.Text).Replace("'", "´");
                if (txt_menarquia.Text == "0")
                {
                    dr["menarquia"] = "";
                }
                else
                {
                    dr["menarquia"] = txt_menarquia.Text;
                }
                if (txt_menopausia.Text == "0")
                {
                    dr["menopausia"] = "";
                }
                else
                {
                    dr["menopausia"] = this.txt_menopausia.Text;
                }

                if (txt_ciclos.Text == "0")
                {
                    dr["ciclos"] = "";
                }
                else
                {
                    dr["ciclos"] = txt_ciclos.Text;
                }
                dr["vSexual"] = chk_vidasexual.Checked == true ? "X" : "";
                dr["gesta"] = txt_gesta.Text == "0" ? "" : txt_gesta.Text;
                dr["partos"] = txt_partos.Text == "0" ? "" : txt_partos.Text;
                dr["abortos"] = txt_abortos.Text == "0" ? "" : txt_abortos.Text;
                dr["cesareas"] = txt_cesareas.Text == "0" ? "" : txt_cesareas.Text;
                dr["hVivos"] = txt_hijosvivos.Text == "0" ? "" : txt_hijosvivos.Text;
                dr["scoremama"] = txt_ScoreMama.Text == "0" ? "" : txt_ScoreMama.Text;
                dr["edadInicioVidaSexual"] = txt_EdadVidaSexual.Text == "0" ? "" : txt_EdadVidaSexual.Text;
                if (paciente.PAC_GENERO == "F")
                {
                    dr["fuep"] = ""; dr["fuap"] = "";
                }
                else
                {
                    dr["fuep"] = dtp_EcoProstata.Checked == true ? Convert.ToString(dtp_EcoProstata.Value.ToString("dd/MM/yyyy")) : string.Empty;
                    dr["fuap"] = dtp_AntigenoProstata.Checked == true ? Convert.ToString(dtp_AntigenoProstata.Value.ToString("dd/MM/yyyy")) : string.Empty;
                }
                if (paciente.PAC_GENERO == "M")
                {
                    dr["fum"] = ""; dr["fup"] = ""; dr["fuc"] = ""; dr["fucolp"] = ""; dr["fumamografia"] = "";
                }
                else
                {
                    dr["fum"] = dtp_ultimaMenst.Checked == true ? Convert.ToString(dtp_ultimaMenst.Value.ToString("dd/MM/yyyy")) : "";
                    if (txt_gesta.Text != "0")
                    {
                        dr["fup"] = dtp_ultimoParto.Checked == true ? Convert.ToString(dtp_ultimoParto.Value.ToString("dd/MM/yyyy")) : "";
                    }
                    dr["fuc"] = dtp_ultimaCito.Checked == true ? Convert.ToString(dtp_ultimaCito.Value.ToString("dd/MM/yyyy")) : "";
                    dr["fucolp"] = dtp_FUColposcopia.Checked == true ? Convert.ToString(dtp_FUColposcopia.Value.ToString("dd/MM/yyyy")) : "";
                    dr["fumamografia"] = dtp_FUMamografia.Checked == true ? Convert.ToString(dtp_FUMamografia.Value.ToString("dd/MM/yyyy")) : "";
                }

                dr["biopsia"] = chk_biopsia.Checked == true ? "X" : "-";
                dr["pFamiliar"] = txt_prevencion.Text;
                dr["tHormonal"] = chk_terapia.Checked == true ? "X" : "";
                dr["cCopia"] = chk_colcoscopia.Checked == true ? "X" : "";
                dr["mamografia"] = chk_mamografia.Checked == true ? "X" : "-";
                if (paciente.PAC_GENERO == "F" && dtp_ultimaMenst.Checked == true)
                {
                    if (regularButton.Checked == true)
                    {
                        dr["ciclodetalle"] = "REGULAR";
                    }
                    else
                    {
                        dr["ciclodetalle"] = "IRREGULAR";
                    }
                }
                else
                {
                    dr["ciclodetalle"] = "";
                }
                dr["vacunas"] = "";
                dr["alergiasMed"] = "";
                dr["otraAlergia"] = "";
                dr["patologiaCli"] = "";
                dr["medicacionHab"] = "";
                dr["quirurgicos"] = "";
                dr["habitos"] = "";
                dr["condicionsocio"] = "";
                dr["discapacidad"] = "";
                dr["religion"] = "";
                dr["tipificacionSanguinea"] = "";
                dr["otroPP"] = "";

                //ANTECEDENTES PERSONALES
                string antecedentesPersonales = " "; //Almacena los antecedentes Personales para el reporte

                HC_CATALOGOS cat = new HC_CATALOGOS();
                List<HC_CATALOGOS> listaCatalogo = new List<HC_CATALOGOS>();
                listaCatalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(Parametros.ReporteFormularios.codigoCatalogo_APersonales);
                foreach (DataGridViewRow item in dtg_antec_personales.Rows)
                {
                    if (item.Cells[1].Value != null)
                    {
                        for (int i = 0; i < listaCatalogo.Count; i++)
                        {
                            cat = listaCatalogo.ElementAt(i);
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (i == 0)
                                    dr["vacunas"] = i == 0 ? "X" : "";
                                if (i == 21)
                                    dr["habitos"] = i == 21 ? "X" : "";
                                if (i == 22)
                                    dr["religion"] = i == 22 ? "X" : "";
                                if (i == 24)
                                    dr["alergiasMed"] = i == 24 ? "X" : "";
                                if (i == 25)
                                    dr["otraAlergia"] = i == 25 ? "X" : "";
                                if (i == 26)
                                    dr["patologiaCli"] = i == 26 ? "X" : "";
                                if (i == 27)
                                    dr["medicacionHab"] = i == 27 ? "X" : "";
                                if (i == 28)
                                    dr["quirurgicos"] = i == 28 ? "X" : "";
                                if (i == 29)
                                    dr["condicionsocio"] = i == 29 ? "X" : "";
                                if (i == 30)
                                    dr["discapacidad"] = i == 30 ? "X" : "";
                                if (i == 31)
                                    dr["tipificacionSanguinea"] = i == 31 ? "X" : "";
                                i = listaCatalogo.Count;
                            }
                        }
                    }
                }
                //foreach (DataGridViewRow item in dtg_antec_personales.Rows)
                //{

                //    try
                //    {
                //        if (item.Cells[0].Value != null)
                //        {
                //            HC_ANAMNESIS_DETALLE detalle = NegAnamnesisDetalle.rescataDetalleXcodigo(Convert.ToInt64(item.Cells[0].Value)); //Convierte el ADE_CODIGO recibido en un objeto HC_ANAMNESIS_DETALLE
                //            int num = 1;
                //            Int64 hcc_codigo = Convert.ToInt64(detalle.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value); //Convierte en INT64 el HCC_CODIGO recibido que se rescata del objeto anterior

                //            if (hcc_codigo == 1)
                //            {
                //                dr["vacunas"] = "X";
                //                //num = 1;
                //            }
                //            else if (hcc_codigo == 906)
                //            {
                //                dr["alergiasMed"] = "X";
                //                //num = 2;
                //            }

                //            else if (hcc_codigo == 907)
                //            {
                //                dr["otraAlergia"] = "X";
                //                //num = 3;
                //            }

                //            else if (hcc_codigo == 908)
                //            {
                //                dr["patologiaCli"] = "X";
                //                //num = 4;
                //            }
                //            else if (hcc_codigo == 909)
                //            {
                //                dr["medicacionHab"] = "X";
                //                //num = 5;
                //            }

                //            else if (hcc_codigo == 910)
                //            {
                //                dr["quirurgicos"] = "X";
                //                //num = 6;
                //            }

                //            else if (hcc_codigo == 22)
                //            {
                //                dr["habitos"] = "X";
                //                //num = 7;
                //            }
                //            else if (hcc_codigo == 911)
                //            {
                //                dr["condicionsocio"] = "X";
                //                //num = 8;
                //            }
                //            else if (hcc_codigo == 912)
                //            {
                //                dr["discapacidad"] = "X";
                //                //num = 9;
                //            }
                //            else if (hcc_codigo == 23)
                //            {
                //                dr["religion"] = "X";
                //                //num = 10;
                //            }
                //            else if (hcc_codigo == 913)
                //            {
                //                dr["tipificacionSanguinea"] = "X";
                //                //num = 11;
                //            }
                //        }                                               
                //    }
                //    catch (Exception)
                //    {

                //        throw;
                //    }

                //}                
                List<object> list2 = new List<object> { 906, 907, 1, 908, 909, 910, 22, 911, 912, 23, 913 };
                HC_ANAMNESIS anam2 = NegAnamnesis.recuperarAnamnesisPorAtencion(atencion.ATE_CODIGO);
                List<HC_ANAMNESIS_DETALLE> det = NegAnamnesisDetalle.listaDetallesAnamnesisAntPerso(anam2.ANE_CODIGO);
                var detOrdenados = det.OrderBy(y =>
                {
                    var valorDetalle = y.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value;
                    return list2.IndexOf(valorDetalle);
                }).ToList();
                //det.OrderBy(y => list.IndexOf(y.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value)).ToList();
                foreach (var item in detOrdenados)
                {

                    antecedentesPersonales += tipoidAntPersonales(Convert.ToInt64(item.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value.ToString())) + " - " + item.ADE_DESCRIPCION + "\t";
                }
                //for (int i = 0; i < dtg_antec_personales.Rows.Count - 1; i++)
                //{
                //    if (dtg_antec_personales.Rows[i].Cells[1].Value != null)
                //    {
                //        antecedentesPersonales += dtg_antec_personales.Rows[i].Cells[1].Value.ToString() + " - " + dtg_antec_personales.Rows[i].Cells[2].Value.ToString() + "\t\t";
                //    }
                //}

                dr["descripcion1"] = (antecedentesPersonales).Replace("'", "´");
                dr["cardiopatia"] = "-";
                dr["diabetes"] = "-";
                dr["efVascular"] = "-";
                dr["hipertension"] = "-";
                dr["endocrinometa"] = "-";
                dr["cancer"] = "-";
                dr["tuberculosis"] = "-";
                dr["enfermedad"] = "-";
                dr["efMental"] = "-";
                dr["efInfecciosa"] = "-";
                dr["malformacion"] = "-";
                dr["otros"] = "-";

                string antecedentesFamiliares = " ";
                listaCatalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(Parametros.ReporteFormularios.codigoCatalogo_AFamiliares);
                foreach (DataGridViewRow item in dtg_antec_familiares.Rows)
                {
                    if (item.Cells[1].Value != null)
                    {
                        for (int i = 0; i < listaCatalogo.Count; i++)
                        {
                            cat = listaCatalogo.ElementAt(i);
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (i == 0)
                                    dr["cardiopatia"] = i == 0 ? "X" : "";
                                if (i == 1)
                                    dr["diabetes"] = i == 1 ? "X" : "";
                                if (i == 2)
                                    dr["efVascular"] = i == 2 ? "X" : "";
                                if (i == 3)
                                    dr["hipertension"] = i == 3 ? "X" : "";
                                if (i == 4)
                                    dr["cancer"] = i == 4 ? "X" : "";
                                if (i == 5)
                                    dr["tuberculosis"] = i == 5 ? "X" : "";
                                if (i == 6)
                                    dr["efMental"] = i == 6 ? "X" : "";
                                if (i == 7)
                                    dr["efInfecciosa"] = i == 7 ? "X" : "";
                                if (i == 8)
                                    dr["malformacion"] = i == 8 ? "X" : "";
                                if (i == 9)
                                    dr["otros"] = i == 9 ? "X" : "";
                                if (i == 10)
                                    dr["endocrinometa"] = i == 10 ? "X" : "";
                                i = listaCatalogo.Count;
                            }
                        }
                    }
                }
                List<object> listAF = new List<object> { 25, 28, 27, 914, 29, 30, 31, 32, 33, 34 };
                HC_ANAMNESIS anamAF = NegAnamnesis.recuperarAnamnesisPorAtencion(atencion.ATE_CODIGO);
                List<HC_ANAMNESIS_DETALLE> detAF = NegAnamnesisDetalle.listaDetallesAnamnesisAntFam(anamAF.ANE_CODIGO);
                var detOrdenadosAF = detAF.OrderBy(y =>
                {
                    var valorDetalle2 = y.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value;
                    return listAF.IndexOf(valorDetalle2);
                }).ToList();
                //det.OrderBy(y => list.IndexOf(y.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value)).ToList();
                foreach (var item in detOrdenadosAF)
                {

                    antecedentesFamiliares += tipoIdAntFamiliares(Convert.ToInt64(item.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value.ToString())) + " - " + item.ADE_DESCRIPCION + "\t";
                }
                //foreach (DataGridViewRow item in dtg_antec_familiares.Rows)
                //{
                //    if (item.Cells[1].Value != null)
                //        dr["descripcion2"] += item.Cells[1].Value.ToString() + " - " + item.Cells[2].Value.ToString() + "\t\t";
                //}
                dr["descripcion2"] = (antecedentesFamiliares).Replace("'", "´");
                dr["efProActual"] = (txt_problema.Text).Replace("'", "´");
                dr["oSentidos2"] = "X";
                dr["res2"] = "X";
                dr["cv2"] = "X";
                dr["dg2"] = "X";
                dr["gt2"] = "X";
                dr["ur2"] = "X";
                dr["me2"] = "X";
                dr["en2"] = "X";
                dr["hl2"] = "X";
                dr["nv2"] = "X";

                //Organos y Sistemas
                try
                {
                    listaCatalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(Parametros.ReporteFormularios.codigoCatalogo_OSistemas);

                    foreach (DataGridViewRow item in dtg_organos.Rows)
                    {
                        if (item.Cells[0].Value != null)
                        {
                            for (int i = 0; i < listaCatalogo.Count; i++)
                            {
                                cat = listaCatalogo.ElementAt(i);
                                if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                                {
                                    if (i == 0)
                                    {
                                        dr["oSentidos1"] = i == 0 ? "X" : " ";
                                    }
                                    if (i == 1)
                                    {
                                        dr["res1"] = i == 1 ? "X" : " ";
                                    }
                                    if (i == 2)
                                    {
                                        dr["cv1"] = i == 2 ? "X" : " ";
                                    }
                                    if (i == 3)
                                    {
                                        dr["dg1"] = i == 3 ? "X" : " ";
                                    }
                                    if (i == 6)
                                    {
                                        dr["me1"] = i == 6 ? "X" : " ";
                                    }
                                    if (i == 7)
                                    {
                                        dr["en1"] = i == 7 ? "X" : " ";
                                    }
                                    if (i == 8)
                                    {
                                        dr["hl1"] = i == 8 ? "X" : " ";
                                    }
                                    if (i == 9)
                                    {
                                        dr["nv1"] = i == 9 ? "X" : " ";
                                    }
                                    if (i == 10)
                                    {
                                        dr["pielAnexos"] = i == 10 ? "X" : " ";
                                    }
                                    if (i == 11)
                                    {
                                        dr["gt1"] = i == 11 ? "X" : " ";
                                    }

                                    i = listaCatalogo.Count;

                                }
                            }
                        }
                    }
                    string revisionActualOrganos = " ";
                    //List<string> listaCampos = new List<string> { "PIEL-ANEXOS", "ÓRGANOS DE LOS SENTIDOS", "RESPIRATORIO", "CARDIO VASCULAR", "DIGESTIVO", "GENITAL", "URINARIO", "MÚSCULO ESQUELÉTICO", "ENDOCRINO", "HEMO LINFÁTICO", "NERVIOSO" };
                    //listaCatalogo.OrderBy(x => listaCampos.IndexOf(x.HCC_NOMBRE)).ToList();

                    List<object> listOrg = new List<object> { 915, 35, 36, 37, 38, 926, 41, 42, 43, 44 };
                    HC_ANAMNESIS anamOrg = NegAnamnesis.recuperarAnamnesisPorAtencion(atencion.ATE_CODIGO);
                    List<HC_ANAMNESIS_DETALLE> detOrganos1 = NegAnamnesisDetalle.listaDetallesAnamnesisOrganos1(anamOrg.ANE_CODIGO);
                    var detOrdenadosOrg = detOrganos1.OrderBy(y =>
                    {
                        var valorDetalle = y.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value;
                        return listOrg.IndexOf(valorDetalle);
                    }).ToList();
                    //det.OrderBy(y => list.IndexOf(y.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value)).ToList();
                    foreach (var item in detOrdenadosOrg)
                    {

                        revisionActualOrganos += tipoid(Convert.ToInt64(item.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value.ToString())) + " - " + item.ADE_DESCRIPCION + "\t";
                    }
                    //foreach (DataGridViewRow item in dtg_organos.Rows)
                    //{
                    //    //if (item.Cells[2].Value == null)
                    //    //    item.Cells[2].Value = "";
                    //    if (item.Cells[1].Value != null && item.Cells[2].Value != null)
                    //        revisionActualOrganos += item.Cells[1].Value.ToString() + " - " + item.Cells[2].Value.ToString() + "\t\t";
                    //}
                    dr["descripcion3"] = (revisionActualOrganos).Replace("'", "´");
                    dr["pa1"] = txt_presionA.Text != string.Empty ? Convert.ToInt32(txt_presionA.Text) : 0;
                    dr["pa2"] = txt_presionB.Text != string.Empty ? Convert.ToInt32(txt_presionB.Text) : 0;
                    dr["fCardiaca"] = txt_frecCardiaca.Text != string.Empty ? Convert.ToInt32(txt_frecCardiaca.Text) : 0;
                    dr["fRespiratoria"] = txt_frecRespiratoria.Text != string.Empty ? Convert.ToInt32(txt_frecRespiratoria.Text) : 0;
                    dr["tBucal"] = txt_tempBucal.Text != string.Empty ? Convert.ToDouble(txt_tempBucal.Text) : 0;
                    dr["tAxilar"] = txt_tempAxilar.Text != string.Empty ? Convert.ToDouble(txt_tempAxilar.Text) : 0;
                    if (Convert.ToDecimal(txt_tempBucal.Text) > 0)//Valida la temperatura para poder imprimirla dependiendo lo que se ingrese
                    {
                        dr["temperatura"] = txt_tempBucal.Text != string.Empty ? Convert.ToDouble(txt_tempBucal.Text) : 0;
                    }
                    else
                    {
                        dr["temperatura"] = txt_tempAxilar.Text != string.Empty ? Convert.ToDouble(txt_tempAxilar.Text) : 0;
                    }
                    dr["peso"] = txt_peso.Text != string.Empty ? Convert.ToDouble(txt_peso.Text) : 0;
                    dr["talla"] = txt_talla.Text != string.Empty ? Convert.ToDouble(txt_talla.Text) : 0;
                    dr["pCefalio"] = txt_perimetro.Text != string.Empty ? Convert.ToDouble(txt_perimetro.Text) : 0;
                    dr["sOxigeno"] = mskSaturacion.Text;
                    dr["imc"] = txtIndiceMasaCorporal.Text;
                }
                catch (Exception)
                {

                    throw;
                }
                dr["piel2"] = "X";
                dr["cabeza2"] = "X";
                dr["ojos2"] = "X";
                dr["oidos2"] = "X";
                dr["nariz2"] = "X";
                dr["boca2"] = "X";
                dr["faringe2"] = "X";
                dr["cuello2"] = "X";
                dr["axilas2"] = "X";
                dr["torax2"] = "X";
                dr["abdomen2"] = "X";
                dr["columna2"] = "X";
                dr["ingle2"] = "X";
                dr["mSup2"] = "X";
                dr["mInf2"] = "X";
                dr["oSen2"] = "X";
                dr["res2"] = "X";
                dr["cVas2"] = "X";
                dr["diges2"] = "X";
                dr["gtal2"] = "X";
                dr["urn2"] = "X";
                dr["mEsq2"] = "X";
                dr["end2"] = "X";
                dr["hLim2"] = "X";
                dr["neu2"] = "X";
                listaCatalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(Parametros.ReporteFormularios.codigoCatalogo_ExamenFisico);
                foreach (DataGridViewRow item in dtg_examenFisico.Rows)
                {
                    if (item.Cells[1].Value != null)
                    {
                        for (int i = 0; i < listaCatalogo.Count; i++)
                        {
                            cat = listaCatalogo.ElementAt(i);
                            if (cat.HCC_NOMBRE == item.Cells[1].Value.ToString())
                            {
                                if (i == 0)
                                {

                                    dr["piel1"] = i == 0 ? "X" : " ";
                                    //if ((bool)item.Cells[2].Value)
                                    //{
                                    //    dr["piel2"] = " ";
                                    //}
                                    //else
                                    //    dr["piel2"] = i == 0 ? "X" : " ";
                                }
                                if (i == 1)
                                {
                                    dr["cabeza1"] = i == 1 ? "X" : " ";
                                }
                                if (i == 2)
                                {
                                    dr["ojos1"] = i == 2 ? "X" : " ";
                                }
                                if (i == 3)
                                {
                                    dr["oidos1"] = i == 3 ? "X" : " ";
                                }
                                if (i == 4)
                                {
                                    dr["naiz1"] = i == 4 ? "X" : " ";
                                }
                                if (i == 5)
                                {
                                    dr["boca1"] = i == 5 ? "X" : " ";
                                }
                                if (i == 6)
                                {
                                    dr["faringe1"] = i == 6 ? "X" : " ";
                                }
                                if (i == 7)
                                {
                                    dr["cuello1"] = i == 7 ? "X" : " ";
                                }
                                if (i == 8)
                                {
                                    dr["axilas1"] = i == 8 ? "X" : " ";
                                }
                                if (i == 9)
                                {
                                    dr["torax1"] = i == 9 ? "X" : " ";
                                }

                                if (i == 10)
                                {
                                    dr["abdomen1"] = i == 10 ? "X" : " ";
                                }
                                if (i == 11)
                                {
                                    dr["columna1"] = i == 11 ? "X" : " ";
                                }
                                if (i == 12)
                                {
                                    dr["ingle1"] = i == 12 ? "X" : " ";
                                }
                                if (i == 13)
                                {
                                    dr["mSup1"] = i == 13 ? "X" : " ";
                                }
                                if (i == 14)
                                {
                                    dr["mInf1"] = i == 14 ? "X" : " ";
                                }
                                if (i == 15)
                                {
                                    dr["oSen1"] = i == 15 ? "X" : " ";
                                }
                                if (i == 16)
                                {
                                    dr["resp1"] = i == 16 ? "X" : " ";
                                }
                                if (i == 17)
                                {
                                    dr["cVas1"] = i == 17 ? "X" : " ";
                                }
                                if (i == 18)
                                {
                                    dr["diges1"] = i == 18 ? "X" : " ";
                                }
                                if (i == 19)
                                {
                                    dr["gtal1"] = i == 19 ? "X" : " ";
                                }

                                if (i == 20)
                                {
                                    dr["urn1"] = i == 20 ? "X" : " ";
                                }
                                if (i == 21)
                                {
                                    dr["mEsq1"] = i == 21 ? "X" : " ";
                                }
                                if (i == 22)
                                {
                                    dr["end1"] = i == 22 ? "X" : " ";
                                }
                                if (i == 23)
                                {
                                    dr["hLim1"] = i == 23 ? "X" : " ";
                                }
                                if (i == 24)
                                {
                                    dr["neu1"] = i == 24 ? "X" : " ";
                                }

                                i = listaCatalogo.Count;
                            }
                        }
                    }
                }
                string examenFisicoRegional = " ";
                string examenFisicoSistemico = " ";

                //det.OrderBy(y => list.IndexOf(y.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value)).ToList();                
                //foreach (var item in detOrdenados1)
                //{
                //    if (exa)
                //    {
                //        examenFisicoRegional += tipoEFR(Convert.ToInt64(item.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value.ToString())) + "R - " + item.ADE_DESCRIPCION + "\n";
                //    }           

                //}
                HC_ANAMNESIS anamF = NegAnamnesis.recuperarAnamnesisPorAtencion(atencion.ATE_CODIGO);
                List<HC_ANAMNESIS_DETALLE> detFR = NegAnamnesisDetalle.listaDetallesAnamExamenFisico(anamF.ANE_CODIGO);
                List<object> exaFR = new List<object> { 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59 };
                var detOrdenados1 = detFR.OrderBy(y =>
                {
                    var valorDetalle = y.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value;
                    return exaFR.IndexOf(valorDetalle);
                }).ToList();

                List<object> exaFS = new List<object> { 916, 917, 918, 919, 920, 921, 922, 923, 924, 925 };
                var detordenFS = detFR.OrderBy(y =>
                {
                    var valorDetalle = y.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value;
                    return exaFS.IndexOf(valorDetalle);
                }).ToList();

                foreach (var item in detOrdenados1)
                {
                    var valorDetalle = Convert.ToInt64(item.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value);
                    var tipo = tipoEFR(valorDetalle); //Obtiene el tipo de examen físico
                                                      //Verificar si el tipo es válido para exámenes físicos regionales
                    if (tipo >= 1 && tipo <= 15)
                    {
                        examenFisicoRegional += tipo + "R - " + item.ADE_DESCRIPCION + ", ";
                    }
                }
                examenFisicoRegional += "\n";
                foreach (var item in detordenFS)
                {
                    var valorDetalle = Convert.ToInt64(item.HC_CATALOGOSReference.EntityKey.EntityKeyValues[0].Value);
                    var tipo = tipoEFS(valorDetalle); //Obtiene el tipo de examen físico
                                                      //Verificar si el tipo es válido para exámenes físicos sistemáticos
                    if (tipo >= 1 && tipo <= 10)
                    {
                        examenFisicoRegional += tipo + "S - " + item.ADE_DESCRIPCION;
                        if (item != detordenFS.Last()) //Verifica si es ultimo en el detalle ordenado, o no
                        {
                            examenFisicoRegional += ", ";
                        }
                        else
                        {
                            examenFisicoRegional += ".";
                        }
                    }
                }

                dr["descripcion4"] = (examenFisicoRegional);
                dr["descExamenFisicoSistemico"] = (examenFisicoSistemico);

                //foreach (DataGridViewRow item in dtg_examenFisico.Rows)
                //{
                //    if (item.Cells[1].Value != null)
                //        examenFisicoDescripcion += item.Cells[1].Value.ToString() + " - " + item.Cells[2].Value.ToString() + "\t";
                //}

                dr["analisis"] = (txtAnalisis.Text).Replace("'", "´");
                dr["otrosSignos"] = (txt_OtrosSignos.Text).Replace("'", "´");
                //LLena los campos de Diagnóstico CIE10
                dtg_diagnosticos.Refresh();
                for (int i = 0; i < dtg_diagnosticos.Rows.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        dr["d1c"] = dtg_diagnosticos.Rows[0].Cells[1].Value.ToString();
                        dr["d1"] = (dtg_diagnosticos.Rows[0].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            dr["d1p"] = dtg_diagnosticos.Rows[0].Cells[3].Value != null ? "X" : " ";
                        else
                            dr["d1d"] = dtg_diagnosticos.Rows[0].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 1)
                    {
                        dr["d2c"] = dtg_diagnosticos.Rows[1].Cells[1].Value.ToString();
                        dr["d2"] = (dtg_diagnosticos.Rows[1].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            dr["d2p"] = dtg_diagnosticos.Rows[1].Cells[3].Value != null ? "X" : " ";
                        else
                            dr["d2d"] = dtg_diagnosticos.Rows[1].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 2)
                    {
                        dr["d3c"] = dtg_diagnosticos.Rows[2].Cells[1].Value.ToString();
                        dr["d3"] = (dtg_diagnosticos.Rows[2].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            dr["d3p"] = dtg_diagnosticos.Rows[2].Cells[3].Value != null ? "X" : " ";
                        else
                            dr["d3d"] = dtg_diagnosticos.Rows[2].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 3)
                    {
                        dr["d4c"] = dtg_diagnosticos.Rows[3].Cells[1].Value.ToString();
                        dr["d4"] = (dtg_diagnosticos.Rows[3].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            dr["d4p"] = dtg_diagnosticos.Rows[3].Cells[3].Value != null ? "X" : " ";
                        else
                            dr["d4d"] = dtg_diagnosticos.Rows[3].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 4)
                    {
                        dr["d5c"] = dtg_diagnosticos.Rows[4].Cells[1].Value.ToString();
                        dr["d5"] = (dtg_diagnosticos.Rows[4].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            dr["d5p"] = dtg_diagnosticos.Rows[4].Cells[3].Value != null ? "X" : " ";
                        else
                            dr["d5d"] = dtg_diagnosticos.Rows[4].Cells[4].Value != null ? "X" : " ";
                    }
                    if (i == 5)
                    {
                        dr["d6c"] = dtg_diagnosticos.Rows[5].Cells[1].Value.ToString();
                        dr["d6"] = (dtg_diagnosticos.Rows[5].Cells[2].Value.ToString()).Replace("'", "´");
                        if ((bool)dtg_diagnosticos.Rows[i].Cells[3].Value)
                            dr["d6p"] = dtg_diagnosticos.Rows[5].Cells[3].Value != null ? "X" : " ";
                        else
                            dr["d6d"] = dtg_diagnosticos.Rows[5].Cells[4].Value != null ? "X" : " ";
                    }
                }
                dr["descripcion5"] = (txt_tratamiento.Text).Replace("'", "´");
                dr["fecha"] = txt_fecha.Value.ToString("yyyy/MM/dd");
                dr["hora"] = txt_hora.Value.ToString("HH:mm");
                if (medico != null)
                {
                    dr["profesionalNombres"] = (NegMedicos.recuperarMedicoID_Usuario(anamnesis.ID_USUARIO).MED_NOMBRE1).Replace("'", "´");
                    dr["profesionalApellidos"] = (NegMedicos.recuperarMedicoID_Usuario(anamnesis.ID_USUARIO).MED_APELLIDO_PATERNO).Replace("'", "´");
                    dr["profesionalApellidos2"] = (NegMedicos.recuperarMedicoID_Usuario(anamnesis.ID_USUARIO).MED_APELLIDO_MATERNO).Replace("'", "´"); ;
                }
                else
                {
                    dr["profesionalNombres"] = (NegUsuarios.RecuperaUsuario(anamnesis.ID_USUARIO).NOMBRES).Replace("'", "´");
                    dr["profesionalApellidos"] = (NegUsuarios.RecuperaUsuario(anamnesis.ID_USUARIO).APELLIDOS).Replace("'", "´");
                    dr["profesionalApellidos2"] = "";
                }

                USUARIOS objUsuario = NegUsuarios.RecuperaUsuario(anamnesis.ID_USUARIO);
                if (objUsuario.IDENTIFICACION.Length <= 10)
                    dr["proCedula"] = objUsuario.IDENTIFICACION;
                else
                    dr["proCedula"] = objUsuario.IDENTIFICACION.Substring(0, 10);
                //if (usu.IDENTIFICACION.Length > 10)
                //    dr["medruc"] = usu.IDENTIFICACION.Substring(0, 10);
                //else
                //    dr["medruc"] = usu.IDENTIFICACION;

                //dr["profesionalNombres"] = usu.NOMBRES; //Cambio del nombre completo a los nombres y apellidos respectivamente
                //dr["profesionalApellidos"] = usu.APELLIDOS;                            
                ana.Tables["Anamnesis"].Rows.Add(dr);

                frmReportes x = new frmReportes(1, "AnamnesisNew", ana);
                x.Show();
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
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
        Boolean permitir = true;
        public bool txtKeyPress(TextBox textbox, int code)
        {
            bool resultado;

            if (code == 46 && textbox.Text.Contains("."))//se evalua si es punto y si es punto se rebiza si ya existe en el textbox
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
        private void txt_presionA_TextChanged(object sender, EventArgs e)
        {
            if (txt_presionA.Text == "" || !NegUtilitarios.ValidaPrecion1(Convert.ToDouble(txt_presionA.Text)))
            {
                txt_presionA.Text = "0";
            }
        }

        private void txt_presionA_Leave(object sender, EventArgs e)
        {
            if (txt_presionA.Text.Trim() == string.Empty)
            {
                txt_presionA.Text = "0";
            }
        }

        private void txt_presionA_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
            //soloNumeros(e);
            //NegUtilitarios.OnlyNumber(e, false);
            //txtKeyPress(e);
            //if (e.KeyChar == (char)09)
            //{
            //    txt_presionB.Focus();
            //}
        }

        private void txt_presionA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_presionB.Focus();
            }
        }

        private void txt_presionB_TextChanged(object sender, EventArgs e)
        {
            if (txt_presionB.Text == "" || !NegUtilitarios.ValidaPrecion2(Convert.ToDouble(txt_presionB.Text)))
            {
                txt_presionB.Text = "0";
            }
        }

        private void txt_presionB_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txt_presionA.Text) < Convert.ToInt32(txt_presionB.Text))
            {
                txt_presionB.Text = "0";
                txt_presionB.Focus();
            }
        }

        private void txt_presionB_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(e);
            NegUtilitarios.OnlyNumber(e, false);
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_frecCardiaca.Focus();
            }
        }

        private void txt_presionB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Convert.ToInt32(txt_presionA.Text) < Convert.ToInt32(txt_presionB.Text))
                    txt_presionB.Text = "0";
                txt_frecCardiaca.Focus();
            }
        }

        private void txt_frecCardiaca_Leave(object sender, EventArgs e)
        {
            if (txt_frecCardiaca.Text.Trim() == string.Empty)
            {
                txt_frecCardiaca.Text = "0";
            }
        }

        private void txt_frecCardiaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(e);
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_frecRespiratoria.Focus();
            }
        }

        private void txt_frecCardiaca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_frecRespiratoria.Focus();
            }
        }

        private void txt_frecCardiaca_Enter(object sender, EventArgs e)
        {
            if (txt_frecCardiaca.Text == "0")
            {
                txt_frecCardiaca.Text = string.Empty;
            }
        }

        private void txt_presionA_Enter(object sender, EventArgs e)
        {
            if (txt_presionA.Text == "0")
            {
                txt_presionA.Text = string.Empty;
            }
        }

        private void txt_frecRespiratoria_Leave(object sender, EventArgs e)
        {
            if (txt_frecRespiratoria.Text.Trim() == string.Empty)
            {
                txt_frecRespiratoria.Text = "0";
            }
        }

        private void txt_frecRespiratoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            soloNumeros(e);
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_tempBucal.Focus();
            }
        }

        private void txt_frecRespiratoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_tempBucal.Focus();
            }
        }

        private void txt_frecRespiratoria_Enter(object sender, EventArgs e)
        {
            if (txt_frecRespiratoria.Text == "0")
            {
                txt_frecRespiratoria.Text = string.Empty;
            }
        }

        private void txt_talla_TextChanged(object sender, EventArgs e)
        {
            double talla1 = 0;
            if (txt_talla.Text.Length > 0)
                talla1 = Convert.ToDouble(txt_talla.Text);
            if (talla1 > 2.50)
            {
                MessageBox.Show("La talla no puede ser mayor a 2.50 m", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_talla.Text = "0";
                return;
            }
            if (txt_peso.Text != "0" && txt_peso.Text.Trim() != "")
            {
                if (txt_talla.Text != "0" && txt_talla.Text.Trim() != "")
                {
                    double valor = (Convert.ToDouble(txt_peso.Text) / Math.Pow(Convert.ToDouble(txt_talla.Text), 2));
                    txtIndiceMasaCorporal.Text = (Math.Round(valor, 2)).ToString();
                }
                //else
                //  MessageBox.Show("La talla no puede ser 0", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                txtIndiceMasaCorporal.Text = "0";
            }
        }


        private void dtg_antec_personales_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_antec_personales.CurrentRow != null)
                        {
                            Int32 codigoDetAnam = Convert.ToInt32(dtg_antec_personales.CurrentRow.Cells["codigoAntPer"].Value);
                            if (codigoDetAnam != 0)
                            {
                                NegAnamnesisDetalle.eliminarAnamnesisDetalle(codigoDetAnam);
                                dtg_antec_personales.Rows.Remove(dtg_antec_personales.CurrentRow);
                                MessageBox.Show("Registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dtg_antec_personales.Rows.Remove(dtg_antec_personales.CurrentRow);
                                MessageBox.Show("Registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtg_antec_familiares_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_antec_familiares.CurrentRow != null)
                        {
                            Int32 codigoDetAnam = Convert.ToInt32(dtg_antec_familiares.CurrentRow.Cells["codigoAF"].Value);
                            if (codigoDetAnam != 0)
                            {
                                NegAnamnesisDetalle.eliminarAnamnesisDetalle(codigoDetAnam);
                                dtg_antec_familiares.Rows.Remove(dtg_antec_familiares.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dtg_antec_familiares.Rows.Remove(dtg_antec_familiares.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtg_organos_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        private void dtg_organos_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_organos.CurrentRow != null)
                        {
                            Int32 codigoDetAnam = Convert.ToInt32(dtg_organos.CurrentRow.Cells["codigoOrganos"].Value);
                            if (codigoDetAnam != 0)
                            {
                                NegAnamnesisDetalle.eliminarAnamnesisDetalle(codigoDetAnam);
                                dtg_organos.Rows.Remove(dtg_organos.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dtg_organos.Rows.Remove(dtg_organos.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { /*MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error);*/ }
        }

        private void dtg_examenFisico_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        if (dtg_examenFisico.CurrentRow != null)
                        {
                            Int32 codigoDetAnam = Convert.ToInt32(dtg_examenFisico.CurrentRow.Cells["codigoExamen"].Value);
                            if (codigoDetAnam != 0)
                            {
                                NegAnamnesisDetalle.eliminarAnamnesisDetalle(codigoDetAnam);
                                dtg_examenFisico.Rows.Remove(dtg_examenFisico.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dtg_examenFisico.Rows.Remove(dtg_examenFisico.CurrentRow);
                                MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message, "err", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtg_diagnosticos_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (dtg_diagnosticos.CurrentRow.Cells[1].Value != null)
            {
                if (e.ColumnIndex == this.dtg_diagnosticos.Columns[3].Index)
                {
                    DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[3];
                    if (chkpres.Value == null)
                        chkpres.Value = false;
                    else
                        chkpres.Value = true;

                    DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[4];
                    chkdef.Value = false;
                }
                else
                {
                    if (e.ColumnIndex == this.dtg_diagnosticos.Columns[4].Index)
                    {
                        DataGridViewCheckBoxCell chkdef = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[4];
                        if (chkdef.Value == null)
                            chkdef.Value = false;
                        else
                            chkdef.Value = true;

                        DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_diagnosticos.Rows[e.RowIndex].Cells[3];
                        chkpres.Value = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("No se ha agregado diagnostico en esta fila.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtg_diagnosticos.Rows.RemoveAt(dtg_diagnosticos.CurrentRow.Index);
            }
        }


        private void dtg_diagnosticos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
                busqueda.ShowDialog();
                //if (busqueda.codigo != null)
                //{
                //    DataGridViewRow fila = dtg_diagnosticos.CurrentRow;
                //    fila.Cells[1].Value = busqueda.codigo;
                //    fila.Cells[2].Value = busqueda.resultado;
                //}
                if (busqueda.codigo != null)
                {
                    for (int i = 0; i < dtg_diagnosticos.Rows.Count - 1; i++)
                    {
                        if (dtg_diagnosticos.Rows[i].Cells[1].Value.ToString() == busqueda.codigo.ToString())
                        {
                            MessageBox.Show("Detalle CIE-10 ya existente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    dtg_diagnosticos.Rows.Add(null, busqueda.codigo, busqueda.resultado, false, false);
                    //DataGridViewRow fila = dtg_diagnosticos.CurrentRow;
                    //fila.Cells[1].Value = busqueda.codigo;
                    //fila.Cells[2].Value = busqueda.resultado;
                    //fila.Cells[3].Value = false;
                    //fila.Cells[4].Value = false;
                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (dtg_diagnosticos.CurrentRow != null)
                    {
                        if (anamnesis != null)
                        {
                            int aneCod = anamnesis.ANE_CODIGO;
                            if (aneCod != 0)
                            {
                                Int32 codigoDetAnam = Convert.ToInt32(dtg_diagnosticos.CurrentRow.Cells["codigoD"].Value);
                                NegAnamnesisDetalle.eliminarDiagnosticoDetalle(dtg_diagnosticos.CurrentRow.Cells[1].Value.ToString(), aneCod);
                                dtg_diagnosticos.Rows.Remove(dtg_diagnosticos.CurrentRow);
                                MessageBox.Show("Registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dtg_diagnosticos.Rows.Remove(dtg_diagnosticos.CurrentRow);
                                MessageBox.Show("Registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            dtg_diagnosticos.Rows.Remove(dtg_diagnosticos.CurrentRow);
                            MessageBox.Show("registro eliminado exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch
                {

                }

            }
        }

        private void ultraGroupBox3_Click(object sender, EventArgs e)
        {

        }
        //int index2 = 0;
        //private void BuscaCIEDTG4()
        //{
        //    //if (dtg_4.CurrentRow != null)
        //    //{
        //    frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
        //    busqueda.ShowDialog();
        //    diagnosticoCIE = busqueda.resultado;
        //    codigoCIE = busqueda.codigo;

        //    if ((diagnosticoCIE != "") && (diagnosticoCIE != null))
        //    {
        //        if (dtg_diagnosticos.Rows.Count < 7)
        //        {
        //            if (dtg_diagnosticos.Rows.Count > 1)
        //            {
        //                for (int i = 0; i < dtg_diagnosticos.Rows.Count - 1; i++)
        //                {
        //                    if (busqueda.codigo == dtg_diagnosticos.Rows[i].Cells[1].Value.ToString())
        //                    {
        //                        MessageBox.Show("El procedimiento ya ha sido agregado.\r\nIntente con uno diferente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                        return;
        //                    }
        //                }
        //            }
        //            //DataGridViewTextBoxCell txtcell = (DataGridViewTextBoxCell)this.dtg_4.CurrentRow.Cells[0]; // se comenta por el proceso y genere una fila de manera automatica // Mario Valencia //11-03-2024
        //            //DataGridViewTextBoxCell txtcell2 = (DataGridViewTextBoxCell)this.dtg_4.CurrentRow.Cells[1];
        //            //DataGridViewCheckBoxCell chkpres = (DataGridViewCheckBoxCell)this.dtg_4.CurrentRow.Cells[2];


        //            //if (diagnostico != null)
        //            //{
        //            //    txtcell.Value = diagnostico;
        //            //    txtcell2.Value = codigoCIE;
        //            //    //chkpres.Value = true;
        //            //    diagnostico = "";
        //            //    //dtg_4_CellContentClick(object, dtg_4);

        //            //}
        //            DataGridViewRow fila = new DataGridViewRow();

        //            DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
        //            DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
        //            DataGridViewTextBoxCell CodigoPcell = new DataGridViewTextBoxCell();

        //            DataGridViewCheckBoxCell Check1Cell = new DataGridViewCheckBoxCell();
        //            DataGridViewCheckBoxCell Check2Cell = new DataGridViewCheckBoxCell();

        //            codigoCell.Value = null;
        //            CodigoPcell.Value = busqueda.resultado;
        //            textcell.Value = busqueda.codigo;

        //            Check1Cell = null;
        //            Check2Cell = null;

        //            fila.Cells.Add(CodigoPcell);
        //            fila.Cells.Add(textcell);

        //            dtg_diagnosticos.Rows.Add(fila);
        //            index2++;
        //        }
        //        else
        //            MessageBox.Show("No puede agregar mas de 6 procedimientos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("Seleccione una fila antes de ingresar el diagnóstico.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    //}

        //}

        private void button1_Click(object sender, EventArgs e) //Botón F! para recuperar Diagnosticos //Andrés Cabrera//20240301
        {
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            if (busqueda.codigo != null)
            {
                if (dtg_diagnosticos.Rows.Count < 7)
                {
                    for (int i = 0; i < dtg_diagnosticos.Rows.Count - 1; i++)
                    {
                        if (dtg_diagnosticos.Rows[i].Cells[1].Value.ToString() == busqueda.codigo.ToString())
                        {
                            MessageBox.Show("Detalle CIE-10 ya existente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    dtg_diagnosticos.Rows.Add(null, busqueda.codigo, busqueda.resultado, false, false);
                    //DataGridViewRow fila = dtg_diagnosticos.CurrentRow;
                    //fila.Cells[1].Value = busqueda.codigo;
                    //fila.Cells[2].Value = busqueda.resultado;
                    //fila.Cells[3].Value = false;
                    //fila.Cells[4].Value = false;
                }
                else
                    MessageBox.Show("No puede agregar mas de 6 procedimientos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            dtg_diagnosticos.Focus();

            //BuscaCIEDTG4();
        }

        private void txt_tempBucal_Validating(object sender, CancelEventArgs e)
        {
            if (txt_tempBucal.Text != "" || txt_tempBucal.Text == "0")
            {
                if (!NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txt_tempBucal.Text)))
                {
                    txt_tempBucal.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
                    mskSaturacion.Focus();
                    return;
                }
            }
            mskSaturacion.Focus();
        }

        private void mskSaturacion_Leave(object sender, EventArgs e)
        {
            decimal satura = 0;
            if (mskSaturacion.Text == "")
            {
                satura = 0;
            }
            else
            {
                satura = Convert.ToDecimal(mskSaturacion.Text);
            }
            if (satura < 30 || satura > 100)
            {
                mskSaturacion.Focus();
                mskSaturacion.Text = "0";
                MessageBox.Show("Saturación de oxigeno no puede ser menor a 30 ni mayor a 100", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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

        private void txt_peso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_talla.Focus();
            }
        }


        private void txt_talla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_perimetro.Focus();
            }
        }

        private void txt_talla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            Infragistics.Win.UltraWinEditors.UltraTextEditor ute = sender as Infragistics.Win.UltraWinEditors.UltraTextEditor;

            if ((e.KeyChar == '.') && (ute.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_tempAxilar_Validating(object sender, CancelEventArgs e)
        {
            if (txt_tempAxilar.Text != "" || txt_tempAxilar.Text == "0")
            {
                if (!NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txt_tempAxilar.Text)))
                {
                    txt_tempAxilar.Text = "0";
                    return;
                }
            }
            else { txt_tempAxilar.Enabled = true; }
        }

        private void txt_peso_TextChanged(object sender, EventArgs e)
        {
            if (txt_peso.Text != "0" && txt_peso.Text.Trim() != "")
            {
                if (txt_talla.Text != "0" && txt_talla.Text.Trim() != "")
                {
                    double valor = (Convert.ToDouble(txt_peso.Text) / Math.Pow(Convert.ToDouble(txt_talla.Text), 2));
                    txtIndiceMasaCorporal.Text = (Math.Round(valor, 2)).ToString();
                }
            }
            else
            {
                txtIndiceMasaCorporal.Text = "0";
            }
        }

        private void txtIndiceMasaCorporal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_perimetro.Focus();
            }
        }
        private void dtg_organos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void mskSaturacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_peso.Focus();
            }
        }

        private void mskSaturacion_Validating(object sender, CancelEventArgs e)
        {
            decimal satura = 0;
            if (mskSaturacion.Text == "")
            {
                satura = 0;
            }
            else
            {
                satura = Convert.ToDecimal(mskSaturacion.Text);
            }
            if (satura < 30 || satura > 100)
            {
                mskSaturacion.Focus();
                mskSaturacion.Text = "30";
                MessageBox.Show("Saturación de oxigeno no puede ser menor a 30 ni mayor a 100", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void txt_tempBucal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            Infragistics.Win.UltraWinEditors.UltraTextEditor ute = sender as Infragistics.Win.UltraWinEditors.UltraTextEditor;

            if ((e.KeyChar == '.') && (ute.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_tempAxilar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            Infragistics.Win.UltraWinEditors.UltraTextEditor ute = sender as Infragistics.Win.UltraWinEditors.UltraTextEditor;

            if ((e.KeyChar == '.') && (ute.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void mskSaturacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_peso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            Infragistics.Win.UltraWinEditors.UltraTextEditor ute = sender as Infragistics.Win.UltraWinEditors.UltraTextEditor;

            if ((e.KeyChar == '.') && (ute.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_perimetro_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_menarquia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_menopausia.Focus();
            }
        }

        private void txt_menarquia_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_menopausia_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_menopausia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_ciclos.Focus();
            }
        }

        private void txt_ciclos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_gesta.Focus();
            }
        }

        private void txt_ciclos_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_gesta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_ScoreMama.Focus();
            }
        }

        private void txt_gesta_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_ScoreMama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_ScoreMama.Focus();
            }
        }

        private void txt_ScoreMama_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_partos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_abortos.Focus();
            }
        }

        private void txt_partos_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_abortos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_cesareas.Focus();
            }
        }

        private void txt_abortos_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_cesareas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_hijosvivos.Focus();
            }
        }

        private void txt_cesareas_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_hijosvivos_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void chk_biopsia_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_EdadVidaSexual_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_gesta_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(txt_gesta.Value) > 0)
            {
                habilitarCamposGesta(true);
            }
            else
            {
                habilitarCamposGesta(false);
                txt_partos.Text = "0";
                txt_abortos.Text = "0";
                txt_cesareas.Text = "0";
                txt_hijosvivos.Text = "0";

            }
        }

        private void txt_gesta_ValueChanged(object sender, EventArgs e)
        {

        }

        public Int64 tipoid(Int64 id)
        {
            Int64 retorno = 0;
            if (id == 915) //PIEL-ANEXOS
                retorno = 1;
            if (id == 35) //ÓRGANOS DE LOS SENTIDOS
                retorno = 2;
            if (id == 36) //RESPIRATORIO
                retorno = 3;
            if (id == 37) //CARDIO VASCULAR
                retorno = 4;
            if (id == 38) //DIGESTIVO
                retorno = 5;
            if (id == 926) //GENITAL URINARIO
                retorno = 6;
            if (id == 41) //MÚSCULO ESQUELÉTICO
                retorno = 7;
            if (id == 42) //ENDOCRINO
                retorno = 8;
            if (id == 43) //HEMO LINFÁTICO
                retorno = 9;
            if (id == 44) //NERVIOSO
                retorno = 10;

            return retorno;
        }
        public Int64 tipoidAntPersonales(Int64 id)
        {
            Int64 retorno = 0;
            if (id == 906) //Alergias a med
                retorno = 1;
            if (id == 907) //Otras Alergias
                retorno = 2;
            if (id == 1) //Vacunas
                retorno = 3;
            if (id == 908)//Patología Clínica
                retorno = 4;
            if (id == 909) //Medicación Habitual 
                retorno = 5;
            if (id == 910) //Quierurgicos
                retorno = 6;
            if (id == 22) //Hábitos
                retorno = 7;
            if (id == 911) //Condicion socio
                retorno = 8;
            if (id == 912)//Discapacidad
                retorno = 9;
            if (id == 23) //Religion
                retorno = 10;
            if (id == 913) //Tipifciación Sanguínea
                retorno = 11;

            return retorno;
        }
        public Int64 tipoIdAntFamiliares(Int64 id)
        {
            Int64 retorno = 0;
            if (id == 25) //Cardiopatia
                retorno = 1;
            if (id == 28) //Hipertensión
                retorno = 2;
            if (id == 27) //Enf. Cardiovascular 
                retorno = 3;
            if (id == 914)//Endocrino
                retorno = 4;
            if (id == 29) //Cancer
                retorno = 5;
            if (id == 30) //Tuberculosis
                retorno = 6;
            if (id == 31) //Enf Mental
                retorno = 7;
            if (id == 32) //Enf. Infecciosa
                retorno = 8;
            if (id == 33)//MAL FORMACIÓN
                retorno = 9;
            if (id == 34) //OTRO
                retorno = 10;
            return retorno;
        }
        public Int64 tipoEFR(Int64 id)
        {
            Int64 retorno = 0;
            if (id == 45) //Cardiopatia
                retorno = 1;
            if (id == 46) //Hipertensión
                retorno = 2;
            if (id == 47) //Enf. Cardiovascular 
                retorno = 3;
            if (id == 48)//Endocrino
                retorno = 4;
            if (id == 49) //Cancer
                retorno = 5;
            if (id == 50) //Tuberculosis
                retorno = 6;
            if (id == 51) //Enf Mental
                retorno = 7;
            if (id == 52) //Enf. Infecciosa
                retorno = 8;
            if (id == 53)//MAL FORMACIÓN
                retorno = 9;
            if (id == 54) //OTRO
                retorno = 10;
            if (id == 55) //OTRO
                retorno = 11;
            if (id == 56) //OTRO
                retorno = 12;
            if (id == 57) //OTRO
                retorno = 13;
            if (id == 58) //OTRO
                retorno = 14;
            if (id == 59) //OTRO
                retorno = 15;
            return retorno;
        }
        public Int64 tipoEFS(Int64 id)
        {
            Int64 retorno = 0;
            if (id == 916) //Cardiopatia
                retorno = 1;
            if (id == 917) //Hipertensión
                retorno = 2;
            if (id == 918) //Enf. Cardiovascular 
                retorno = 3;
            if (id == 919)//Endocrino
                retorno = 4;
            if (id == 920) //Cancer
                retorno = 5;
            if (id == 921) //Tuberculosis
                retorno = 6;
            if (id == 922) //Enf Mental
                retorno = 7;
            if (id == 923) //Enf. Infecciosa
                retorno = 8;
            if (id == 924)//MAL FORMACIÓN
                retorno = 9;
            if (id == 925) //OTRO
                retorno = 10;
            return retorno;
        }
        private void dtg_antec_personales_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dtg_antec_familiares_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dtg_examenFisico_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dtg_diagnosticos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void txt_frecRespiratoria_Validated(object sender, EventArgs e)
        {
            if (txt_frecRespiratoria.Text == "" || !NegUtilitarios.ValidaFrespiratoria(Convert.ToDouble(txt_frecRespiratoria.Text.Trim())))
            {
                txt_frecRespiratoria.Text = NegParametros.RecuperaValorParSvXcodigo(58).ToString();
            }
        }

        private void txt_frecCardiaca_Validating(object sender, CancelEventArgs e)
        {
            if (txt_frecCardiaca.Text == "" || !NegUtilitarios.ValidaFcardiaca(Convert.ToDouble(txt_frecCardiaca.Text.Trim())))
            {
                txt_frecCardiaca.Text = NegParametros.RecuperaValorParSvXcodigo(56).ToString();
                return;
            }
        }

        private void txt_talla_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txt_peso_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_peso.Text))
            {
                decimal valorIngresado;
                if (decimal.TryParse(txt_peso.Text, out valorIngresado))
                {
                    if (valorIngresado < 0 || valorIngresado > 635)
                    {
                        MessageBox.Show("El valor debe estar entre 0 y 635.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_peso.Text = "0";
                        txt_peso.Focus();
                    }

                }
                else
                {
                    txt_peso.Text = "0";
                    txt_peso.Focus();
                    MessageBox.Show("Ingresar un valor válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txt_tempAxilar_Validated(object sender, EventArgs e)
        {
            if (txt_tempAxilar.Text != "" || txt_tempAxilar.Text == "0")
            {
                if (!NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txt_tempAxilar.Text)))
                {
                    txt_tempAxilar.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
                    return;
                }
            }
            else { txt_tempAxilar.Enabled = true; }

            if (txt_tempAxilar.Text == "")
            {
                txt_tempAxilar.Text = NegParametros.RecuperaValorParSvXcodigo(60).ToString();
            }
        }

        private void dtg_antec_personales_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dtg_antec_personales[e.ColumnIndex, e.RowIndex].Value = dtg_antec_personales[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
            }
        }

        private void dtg_antec_familiares_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dtg_antec_familiares[e.ColumnIndex, e.RowIndex].Value = dtg_antec_familiares[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
            }
        }

        private void dtg_organos_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dtg_organos[e.ColumnIndex, e.RowIndex].Value = dtg_organos[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
            }
        }

        private void dtg_examenFisico_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (dtg_examenFisico[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    dtg_examenFisico[e.ColumnIndex, e.RowIndex].Value = dtg_examenFisico[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
                }
            }
        }
        //ANTECEDENTES PERSONALES
        private void chkAlergiasMed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAlergiasMed.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ALERGIA A MEDICAMENTOS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ALERGIA A MEDICAMENTOS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkOtrasAlergias_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtrasAlergias.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "OTRAS ALERGIAS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "OTRAS ALERGIAS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkVacunas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVacunas.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "VACUNAS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "VACUNAS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkPatologia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPatologia.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "PATOLOGIA CLÍNICA")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "PATOLOGIA CLÍNICA";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkMedHab_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMedHab.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "MEDICACION HABITUAL")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "MEDICACION HABITUAL";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);

            }
            else
            {
                for (int i = dtg_antec_personales.Rows.Count - 1; i >= 0; i--)
                {
                    if (dtg_antec_personales.Rows[i].Cells[1].Value != null)
                    {
                        if (dtg_antec_personales.Rows[i].Cells[1].Value.ToString() == "MEDICACION HABITUAL")
                        {
                            dtg_antec_personales.Rows.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void chkQuirurgicos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQuirurgicos.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "QUIRÚRGICOS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "QUIRÚRGICOS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkHabitos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHabitos.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "HÁBITOS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "HÁBITOS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkCondSocio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCondSocio.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "CONDICIÓN SOCIOECONÓMICA")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "CONDICIÓN SOCIOECONÓMICA";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkDiscapacidad_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiscapacidad.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "DISCAPACIDAD")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "DISCAPACIDAD";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkReligion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReligion.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "RELIGIÓN")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "RELIGIÓN";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkTipifiacionSangre_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTipifiacionSangre.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_personales.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "TIPIFICACIÓN SANGUÍNEA")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "TIPIFICACIÓN SANGUÍNEA";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_personales.Rows.Add(fila);
            }
        }

        private void chkCardiopatia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCardiopatia.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "CARDIOPATÍA")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "CARDIOPATÍA";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void chkHiper_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHiper.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "HIPERTENSIÓN")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "HIPERTENSIÓN";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void chkEnfCV_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnfCV.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ENF. C. VASCULAR")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ENF. C. VASCULAR";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void chkEndocrino_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEndocrino.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ENDOCRINO METABÓLICO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ENDOCRINO METABÓLICO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void chkCancer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCancer.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "CÁNCER")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "CÁNCER";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void chkTuber_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTuber.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "TUBERCULOSIS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "TUBERCULOSIS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void chkEnfermedadMental_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnfermedadMental.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ENFERMEDAD MENTAL")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ENFERMEDAD MENTAL";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void chkEnfInfecciosa_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnfInfecciosa.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ENF. INFECCIOSA")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ENF. INFECCIOSA";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void chkMalFormación_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMalFormación.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "MALFORMACIÓN")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "MALFORMACIÓN";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void chkOtro_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtro.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_antec_familiares.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "OTRO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "OTRO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_antec_familiares.Rows.Add(fila);
            }
        }

        private void checkPielA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPielA.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_organos.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "PIEL-ANEXOS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "PIEL-ANEXOS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_organos.Rows.Add(fila);
            }
        }

        private void checkOrganos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkOrganos.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_organos.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ÓRGANOS DE LOS SENTIDOS.")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ÓRGANOS DE LOS SENTIDOS.";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_organos.Rows.Add(fila);
            }
        }

        private void checkRespiratorio_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRespiratorio.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_organos.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "RESPIRATORIO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "RESPIRATORIO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_organos.Rows.Add(fila);
            }
        }

        private void checkCV_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCV.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_organos.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "CARDIO VASCULAR")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "CARDIO VASCULAR";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_organos.Rows.Add(fila);
            }
        }

        private void checkDigestivo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCV.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_organos.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "DIGESTIVO.")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "DIGESTIVO.";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_organos.Rows.Add(fila);
            }
        }

        private void checkGenitoUri_CheckedChanged(object sender, EventArgs e)
        {
            if (checkGenitoUri.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow filas in dtg_organos.Rows)
                    {
                        if (filas.Cells[1].Value != null)
                        {
                            if (filas.Cells[1].Value.ToString() == "GÉNITO-URINARIO")
                                return;
                        }
                    }
                    DataGridViewRow fila = new DataGridViewRow();
                    DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                    codigoCell.Value = null;
                    textcell.Value = "GÉNITO-URINARIO";
                    fila.Cells.Add(codigoCell);
                    fila.Cells.Add(textcell);
                    dtg_organos.Rows.Add(fila);
                }
                catch (Exception error)
                {
                    throw error;
                }
            }
        }

        private void checkMusculoEsqueleto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkMusculoEsqueleto.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_organos.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "MÚSCULO ESQUELÉTICO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "MÚSCULO ESQUELÉTICO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_organos.Rows.Add(fila);
            }
        }

        private void checkEndocrino_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEndocrino.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_organos.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ENDOCRINO.")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ENDOCRINO.";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_organos.Rows.Add(fila);
            }
        }

        private void checkHemoLinfa_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHemoLinfa.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_organos.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "HEMO LINFÁTICO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "HEMO LINFÁTICO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_organos.Rows.Add(fila);
            }
        }

        private void checkNervio_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNervio.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_organos.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "NERVIOSO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "NERVIOSO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_organos.Rows.Add(fila);
            }
        }

        private void chkPFaneras_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPFaneras.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "PIEL - FANERAS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "PIEL - FANERAS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkCabeza_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCabeza.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "CABEZA")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "CABEZA";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkOjos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOjos.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "OJOS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "OJOS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkOidos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOidos.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "OIDOS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "OIDOS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkNariz_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNariz.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "NARIZ")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "NARIZ";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkBoca_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoca.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "BOCA")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "BOCA";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkOroFaringe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOroFaringe.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "OROFARINGE")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "OROFARINGE";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkCuello_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCuello.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "CUELLO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "CUELLO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkAxilas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAxilas.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "AXILAS - MAMAS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "AXILAS - MAMAS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkTorax_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTorax.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "TORAX")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "TORAX";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkAbdomen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAbdomen.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ABDOMEN ")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ABDOMEN ";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkColumna_CheckedChanged(object sender, EventArgs e)
        {
            if (chkColumna.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "COLUMNA VERTEBRAL")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "COLUMNA VERTEBRAL";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkIngle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIngle.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "INGLE-PERINÉ")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "INGLE-PERINÉ";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkMSuperiores_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMSuperiores.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "MIEMBROS SUPERIORES")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "MIEMBROS SUPERIORES";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkMInferiores_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMInferiores.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "MIEMBROS INFERIORES")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "MIEMBROS INFERIORES";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkOrganosSentidos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOrganosSentidos.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ÓRGANOS DE LOS SENTIDOS")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ÓRGANOS DE LOS SENTIDOS";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkResp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResp.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "RESPIRATORIO.")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "RESPIRATORIO.";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkCardioVascular_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCardioVascular.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "CARDIO - VASCULAR")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "CARDIO - VASCULAR";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkDigestivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDigestivo.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "DIGESTIVO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "DIGESTIVO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkGenital_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGenital.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "GENITAL")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "GENITAL";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkUrinario_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUrinario.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "URINARIO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "URINARIO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkMusculoEsq_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMusculoEsq.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "MÚSCULO - ESQUELÉTICO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "MÚSCULO - ESQUELÉTICO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkEndocrinoMeta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEndocrinoMeta.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "ENDÓCRINO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "ENDÓCRINO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkHemoLinfa2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHemoLinfa2.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "HEMO - LINFÁTICO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "HEMO - LINFÁTICO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void chkNeurologico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNeurologico.Checked == true)
            {
                foreach (DataGridViewRow filas in dtg_examenFisico.Rows)
                {
                    if (filas.Cells[1].Value != null)
                    {
                        if (filas.Cells[1].Value.ToString() == "NEUROLÓGICO")
                            return;
                    }
                }
                DataGridViewRow fila = new DataGridViewRow();
                DataGridViewTextBoxCell codigoCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                codigoCell.Value = null;
                textcell.Value = "NEUROLÓGICO";
                fila.Cells.Add(codigoCell);
                fila.Cells.Add(textcell);
                dtg_examenFisico.Rows.Add(fila);
            }
        }

        private void txt_tempBucal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txt_tempBucal.Text != "0")
                {
                    if (txt_tempBucal.Text.Trim() != "")
                    {
                        if (txt_tempBucal.Text.Trim().Substring(txt_tempBucal.Text.Length - 1, 1) == ".")
                            txt_tempBucal.Text = txt_tempAxilar.Text.Remove(txt_tempBucal.Text.Length - 1);
                        txt_tempAxilar.Text = string.Empty;
                        txt_ScoreMama.Focus();
                    }

                    else
                    {
                        txt_tempBucal.Text = string.Empty;
                        txt_ScoreMama.Focus();
                    }
                }
                else
                {
                    txt_tempBucal.Text = string.Empty;
                    txt_ScoreMama.Focus();
                }
            }
        }

        private void txt_tempAxilar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txt_tempAxilar.Text != "0")
                {
                    if (txt_tempAxilar.Text.Trim() != "")
                    {
                        if (txt_tempAxilar.Text.Trim().Substring(txt_tempAxilar.Text.Length - 1, 1) == ".")
                            txt_tempAxilar.Text = txt_tempAxilar.Text.Remove(txt_tempAxilar.Text.Length - 1);
                        txt_tempBucal.Text = string.Empty;
                        txt_ScoreMama.Focus();
                    }

                    else
                    {
                        txt_tempAxilar.Text = string.Empty;
                        txt_ScoreMama.Focus();
                    }
                }
                else
                {
                    txt_tempAxilar.Text = string.Empty;
                    txt_ScoreMama.Focus();
                }
            }
        }
    }
}
