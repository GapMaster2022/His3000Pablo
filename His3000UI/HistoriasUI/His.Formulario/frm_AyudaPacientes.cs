using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Clases;
using Recursos;
using His.Negocio;
using His.Parametros;
using His.General;
using Infragistics.Win.UltraWinGrid;

namespace His.Formulario
{
    public partial class frm_AyudaPacientes : Form
    {
        #region Variables
        //List<PACIENTES> consultaPacientes = new List<PACIENTES>();
        List<DtoPacientesEmergencia> consultaPacientes = new List<DtoPacientesEmergencia>();
        public TextBox campoPadre = new TextBox();
        public TextBox campoAtencion = new TextBox();
        public string columnabuscada = "HCL";
        public string columnaAtencion = "ATENCION";
        public bool inicio = true;
        DataTable dtVerifica = new DataTable();

        //public List<PACIENTES> listaPacientes;

        #endregion
        public bool triaje = false;
        bool emergencia = true;
        public bool mushunia = false;
        public bool cxe = false;
        public bool sistemas = false;
        public bool consultaExterna = false;
        bool Microfilms = false;
        Form002MSP consultaEx = new Form002MSP();
        DtoForm002 datos = new DtoForm002();

        public Int32 HC = 0;
        #region Constructor  
        //public frm_AyudaPacientes(List<PACIENTES> pacientes)
        //{
        public frm_AyudaPacientes(bool _emergencia = true, bool _consultaExterna = false, bool _Microfilms = false)
        {
            InitializeComponent();

            emergencia = _emergencia;
            consultaExterna = _consultaExterna;
            Microfilms = _Microfilms;
        }

        private void Buscar()
        {
            try
            {
                string id = txt_busqCi.Text.ToString();
                string historia = txt_busqHist.Text.ToString();
                string nombre = txt_busqNom.Text.ToString();
                int cantidad = 100;

                
                if (!consultaExterna)
                {

                    if (cb_numFilas.SelectedItem != null)
                    {
                        KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cb_numFilas.SelectedItem;
                        cantidad = sitem.Key;
                    }
                    List<DtoPacientesEmergencia> pacientes = new List<DtoPacientesEmergencia>();
                    if (triaje == true && emergencia == true)
                    {
                        consultaPacientes = NegPacientes.RecuperarPacientesListaEmergtriaje(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                        triaje = false;
                    }
                    else if (emergencia == false)
                    {
                        consultaPacientes = NegPacientes.RecuperarPacientesListaConsultaExterna(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                        triaje = false;
                    }
                    else if (mushunia)
                    {
                        consultaPacientes = NegPacientes.recuperarPacientesMushunia(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                        triaje = false;
                    }
                    else if(triaje == false && emergencia == true)
                    {
                        consultaPacientes = NegPacientes.RecuperarPacientesListaEmerg(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                    }
                    else
                        consultaPacientes = NegPacientes.recuperarPacientesTodos(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                    //consultaPacientes = NegPacientes.RecuperarPacientesListaEmerg(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                    if (sistemas)
                    {
                        consultaPacientes = NegPacientes.recuperarPacientesTodos(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                        triaje = false;
                    }
                    //consultaPacientes = pacientes;
                    //foreach (var item in pacientes)
                    //{
                    //    string valido = NegPacientes.EmergenciaEstado(Convert.ToInt64(item.ATE_CODIGO));
                    //    if (valido == "1")
                    //    {
                    //        consultaPacientes.Remove(item);
                    //    }
                    //}
                }
                else
                {
                    consultaPacientes = NegPacientes.RecuperaPacienteCunsultaExterna(HC, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                }
                grid.DataSource = consultaPacientes.Select(
                        p => new
                        {
                            HCL = p.PAC_HISTORIA_CLINICA,
                            NOMBRE = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
                            ID = p.PAC_IDENTIFICACION,
                            ATENCION = p.ATE_CODIGO,
                            FECHA_ATENCION = p.PAC_FECHA_ATENCION,
                            FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO
                        }
                        ).OrderByDescending(a => a.FECHA_ATENCION).ToList();
                grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
                grid.DisplayLayout.Bands[0].Columns["FECHA_NACIMIENTO"].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion

        #region Metodos Privados

        private void cb_numFilas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (inicio == false)
                Buscar();
        }

        private void txt_busqNom_AfterExitEditMode(object sender, EventArgs e)
        {

        }

        private void txt_busqHist_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void txt_busqNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void txt_busqCi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                Buscar();
            }
        }

        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {

        }
        public string campoFecha;
        public string campoId;
        public string campoFechaAtencion;
        private void enviarCodigo()
        {
            try
            {
                if (Microfilms)
                {
                    try
                    {
                        ATENCIONES ate = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(grid.ActiveRow.Cells["ATENCION"].Value.ToString()));
                        PACIENTES pac = NegPacientes.RecuperarPacienteID(Convert.ToInt32(ate.PACIENTES.PAC_CODIGO));
                        string paciente = pac.PAC_NOMBRE1 + " " + pac.PAC_NOMBRE2;
                        string apellido = pac.PAC_APELLIDO_PATERNO + " " + pac.PAC_APELLIDO_MATERNO;
                        string numeroAtencion = ate.ATE_NUMERO_ATENCION;
                        string numeroHistoria = pac.PAC_HISTORIA_CLINICA;
                        int codPaciente = pac.PAC_CODIGO;
                        int codAtencion = ate.ATE_CODIGO;

                        DataTable x = NegDietetica.getDataTable("PathLocalHC");

                        string strDirectorio = numeroHistoria + "//" + codAtencion + "//";
                        if (x.Rows.Count > 0)
                        {
                            string strPathGeneral = x.Rows[0][0].ToString();

                            var expArchivos = new GeneralApp.ControlesWinForms.ExploradorLocal(strDirectorio, strPathGeneral, paciente, codAtencion.ToString());
                            expArchivos.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No se encontro datos para cargar microfilm.", "HIS3000");
                            return;
                        }

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!consultaExterna)
                {
                    campoPadre.Tag = true;
                    campoAtencion.Text = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
                    campoPadre.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
                    campoAtencion.Text = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
                    campoFecha = grid.ActiveRow.Cells["FECHA_NACIMIENTO"].Value.ToString();
                    campoId = grid.ActiveRow.Cells["ID"].Value.ToString();
                    campoFechaAtencion = grid.ActiveRow.Cells["FECHA_ATENCION"].Value.ToString();
                    this.Close();
                }
                else
                {
                    this.Close();
                    imprimir();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void frm_AyudaPacientes_Load(object sender, EventArgs e)
        {

        }

        private void ultraGrid1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    enviarCodigo();
                }
                else if (e.KeyCode == Keys.End)
                {
                    grid.ActiveCell = grid.Rows[grid.Rows.Count - 1].Cells[grid.ActiveCell.Column.Index];
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Home)
                {
                    grid.ActiveCell = grid.Rows[0].Cells[grid.ActiveCell.Column.Index];
                    e.Handled = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            enviarCodigo();
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

        }

        #endregion

        private void frm_AyudaPacientes_Load_1(object sender, EventArgs e)
        {
            btnActualizar.Appearance.Image = Archivo.ButtonRefresh;
            //cb_numFilas.Items.Add(new KeyValuePair<int, string>(10, "10"));
            cb_numFilas.Items.Add(new KeyValuePair<int, string>(100, "100"));
            cb_numFilas.Items.Add(new KeyValuePair<int, string>(1000, "1000"));
            cb_numFilas.Items.Add(new KeyValuePair<int, string>(10000, "10000"));
            cb_numFilas.DisplayMember = "Value";
            cb_numFilas.ValueMember = "Key";
            cb_numFilas.SelectedIndex = 0;
            inicio = false;
            dtVerifica = NegPacientes.EstadosHoja();
            try
            {
                //cargo los ultimos 25 pacientes ingresados
                //consultaPacientes = NegPacientes.RecuperarPacientesListaEmerg("", "", "", Convert.ToInt32(cb_numFilas.Text), EmergenciaPAR.CodigoEmergencia);
                //grid.DataSource = consultaPacientes.Select(
                //    p => new
                //    {
                //        HCL = p.PAC_HISTORIA_CLINICA,
                //        NOMBRE = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
                //        ID = p.PAC_IDENTIFICACION,
                //        ATENCION = p.ATE_CODIGO,
                //        FECHA_ATENCION = p.PAC_FECHA_ATENCION,
                //        FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO
                //    }
                //    ).OrderByDescending(a=> a.FECHA_ATENCION).ToList();

                //grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
                //grid.DisplayLayout.Bands[0].Columns["FECHA_NACIMIENTO"].Hidden=true;   
                //pongo el foco en el campo

                Buscar();

                txt_busqNom.Focus();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void grid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            Int64 CodigoAtencion = 0;
            CodigoAtencion = Convert.ToInt64(e.Row.Cells["ATENCION"].Value);

            var Verifica = from qs in dtVerifica.AsEnumerable()
                           where (qs.Field<int>("ATE_CODIGO") == CodigoAtencion)
                           select qs;

            foreach (DataRow dr in Verifica)
            {

                e.Row.Appearance.BackColor = Color.GreenYellow;

            }

            if (Convert.ToInt32((Convert.ToDateTime(e.Row.Cells["FECHA_ATENCION"].Value) - Convert.ToDateTime(e.Row.Cells["FECHA_NACIMIENTO"].Value)).Days / 365.25) < 14)
            {
                e.Row.Appearance.BackColor = Color.Yellow;
            }

        }

        private void grid_InitializeRow_1(object sender, InitializeRowEventArgs e)
        {

        }



        //private void enviarCodigo()
        //{
        //    this.Close();
        //}

        //public static DialogResult InputBox(string title, string promptText, ref string value)
        //{
        //    Form form = new Form();
        //    Label label = new Label();
        //    TextBox textBox = new TextBox();
        //    Button buttonOk = new Button();
        //    Button buttonCancel = new Button();

        //    form.Text = title;
        //    label.Text = promptText;
        //    textBox.Text = value;

        //    buttonOk.Text = "OK";
        //    buttonCancel.Text = "Cancel";
        //    buttonOk.DialogResult = DialogResult.OK;
        //    buttonCancel.DialogResult = DialogResult.Cancel;

        //    label.SetBounds(9, 20, 372, 13);
        //    textBox.SetBounds(12, 36, 372, 20);
        //    buttonOk.SetBounds(228, 72, 75, 23);
        //    buttonCancel.SetBounds(309, 72, 75, 23);

        //    label.AutoSize = true;
        //    textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
        //    buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        //    buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        //    form.ClientSize = new Size(396, 107);
        //    form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
        //    form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
        //    form.FormBorderStyle = FormBorderStyle.FixedDialog;
        //    form.StartPosition = FormStartPosition.CenterScreen;
        //    form.MinimizeBox = false;
        //    form.MaximizeBox = false;
        //    form.AcceptButton = buttonOk;
        //    form.CancelButton = buttonCancel;

        //    DialogResult dialogResult = form.ShowDialog();
        //    value = textBox.Text;
        //    return dialogResult;
        //}

        //#endregion

        //private void timerCapturaTeclas_Tick(object sender, EventArgs e)
        //{
        //    //desactivo el control, al terminarse el intervalo
        //    timerCapturaTeclas.Enabled = false;
        //}

        //private void grid_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (grid.CurrentRow != null)
        //        {
        //            campoPadre.Text = grid.CurrentRow.Cells["HISTORIA_CLINICA"].Value.ToString();
        //            this.Close();
        //        }    
        //    }
        //}

        //private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    campoPadre.Text = grid.Rows[e.RowIndex].Cells["HISTORIA_CLINICA"].Value.ToString();
        //    this.Close();
        //}
        public void imprimir()
        {
            consultaEx = NegConsultaExterna.PacienteExisteCxE(grid.ActiveRow.Cells["ATENCION"].Value.ToString());
            SUCURSALES sucursal = new SUCURSALES();
            string logo = "";
            string empresa = "";
            //if (mushuñan)
            //{
            //    Int16 AreaUsuario = 1;
            //    DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario));
            //    bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
            //    if (parse)
            //    {
            //        switch (AreaUsuario)
            //        {
            //            case 2:
            //                logo = NegUtilitarios.RutaLogo("Mushuñan");
            //                empresa = "SANTA CATALINA DE SENA";
            //                break;
            //            case 3:
            //                logo = NegUtilitarios.RutaLogo("BrigadaMedica");
            //                empresa = "BRIGADA MEDICA";
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}
            //else
            //{
            logo = NegUtilitarios.RutaLogo("General");
            empresa = Sesion.nomEmpresa;
            //}
            if (consultaEx != null)
            {
                if (consultaEx.Sexo == "Masculino")
                {
                    datos.sexoPaciente = "M";
                }
                else
                    datos.sexoPaciente = "F";

                PACIENTES pacien = new PACIENTES();
                pacien = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(grid.ActiveRow.Cells["ATENCION"].Value.ToString()));
                if (NegParametros.ParametroFormularios())
                    datos.historiaClinica = pacien.PAC_IDENTIFICACION;
                else
                    datos.historiaClinica = consultaEx.Historia;

                NegCertificadoMedico neg = new NegCertificadoMedico();
                //HCU_form002MSP Ds = new HCU_form002MSP();
                His.Formulario.HCU_form002MSP Ds = new His.Formulario.HCU_form002MSP();
                Ds.Tables[0].Rows.Add
                    (new object[]
                    {
                    consultaEx.Nombre.ToString(),
                    consultaEx.Apellido.ToString(),
                    datos.sexoPaciente.ToString(),
                    consultaEx.Edad.ToString(),
                    datos.historiaClinica.ToString().Trim(),
                    consultaEx.AteCodigo.ToString(),
                    consultaEx.Motivo.ToString(),
                    consultaEx.AntecedentesPersonales.ToString(),
                    consultaEx.Cardiopatia.ToString(),
                    consultaEx.Diabetes.ToString(),
                    consultaEx.Vascular.ToString(),
                    consultaEx.Hipertencion.ToString(),
                    consultaEx.Cancer.ToString(),
                    consultaEx.tuberculosis.ToString(),
                    consultaEx.mental.ToString(),
                    consultaEx.infecciosa.ToString(),
                    consultaEx.malformacion.ToString(),
                    consultaEx.otro.ToString(),
                    consultaEx.antecedentesFamiliares.ToString(),
                    consultaEx.enfermedadActual.ToString(),
                    consultaEx.sentidos.ToString(),
                    consultaEx.sentidossp.ToString(),
                    consultaEx.respiratorio.ToString(),
                    consultaEx.respiratoriosp.ToString(),
                    consultaEx.cardioVascular.ToString(),
                    consultaEx.cardioVascularsp.ToString(),
                    consultaEx.digestivo.ToString(),
                    consultaEx.digestivosp.ToString(),
                    consultaEx.genital.ToString(),
                    consultaEx.genitalsp.ToString(),
                    consultaEx.urinario.ToString(),
                    consultaEx.urinariosp.ToString(),
                    consultaEx.esqueletico.ToString(),
                    consultaEx.esqueleticosp.ToString(),
                    consultaEx.endocrino.ToString(),
                    consultaEx.endocrinosp.ToString(),
                    consultaEx.linfatico.ToString(),
                    consultaEx.linfaticosp.ToString(),
                    consultaEx.nervioso.ToString(),
                    consultaEx.nerviososp.ToString(),
                    consultaEx.revisionactual.ToString(),
                    consultaEx.fechamedicion.ToString(),
                    consultaEx.temperatura.ToString(),
                    consultaEx.presion1.ToString(),
                    consultaEx.presion2.ToString(),
                    consultaEx.pulso.ToString(),
                    consultaEx.frecuenciaRespiratoria.ToString(),
                    consultaEx.peso.ToString(),
                    consultaEx.talla.ToString(),
                    consultaEx.cabeza.ToString(),
                    consultaEx.cabezasp.ToString(),
                    consultaEx.cuello.ToString(),
                    consultaEx.cuellosp.ToString(),
                    consultaEx.torax.ToString(),
                    consultaEx.toraxsp.ToString(),
                    consultaEx.abdomen.ToString(),
                    consultaEx.abdomensp.ToString(),
                    consultaEx.pelvis.ToString(),
                    consultaEx.pelvissp.ToString(),
                    consultaEx.extremidades.ToString(),
                    consultaEx.extremidadessp.ToString(),
                    consultaEx.examenFisico.ToString(),
                    consultaEx.diagnostico1.ToString(),
                    consultaEx.diagnostico1cie.ToString(),
                    consultaEx.diagnostico1pre.ToString(),
                    consultaEx.diagnostico1def.ToString(),
                    consultaEx.diagnostico2.ToString(),
                    consultaEx.diagnostico2cie.ToString(),
                    consultaEx.diagnostico2pre.ToString(),
                    consultaEx.diagnostico2def.ToString(),
                    consultaEx.diagnostico3.ToString(),
                    consultaEx.diagnostico3cie.ToString(),
                    consultaEx.diagnostico3def.ToString(),
                    consultaEx.diagnostico3pre.ToString(),
                    consultaEx.diagnostico4.ToString(),
                    consultaEx.diagnostico4cie.ToString(),
                    consultaEx.diagnostico4def.ToString(),
                    consultaEx.diagnostico4pre.ToString(),
                    consultaEx.planesTratamiento.ToString(),
                    consultaEx.evolucion.ToString(),
                    consultaEx.prescripciones.ToString(),
                    Convert.ToString(DateTime.Now),
                    Convert.ToString(DateTime.Now.ToString("hh:mm")),
                    consultaEx.dr.ToString(),
                    Sesion.codMedico.ToString(),
                    logo,
                    empresa
                    });

                frmReportes x = new frmReportes(1, "ConsultaExterna", Ds);
                x.Show();
            }
            else
                MessageBox.Show("Atencion sin Formulario FRM002", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
