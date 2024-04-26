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

namespace His.Emergencia
{
    public partial class frm_AyudaPacientes : Form
    {
        #region Variables
        frm_Registro registro = new frm_Registro();
        //List<PACIENTES> consultaPacientes = new List<PACIENTES>();
        List<DtoPacientesEmergencia> consultaPacientes = new List<DtoPacientesEmergencia>();
        public TextBox campoPadre = new TextBox();
        public TextBox campoAtencion = new TextBox();
        public string columnabuscada = "HCL";
        public string columnaAtencion = "ATENCION";
        public bool inicio = true;
        public string ateCodigo = "";
        DataTable dtVerifica = new DataTable();

        //public List<PACIENTES> listaPacientes;

        #endregion
        public bool triaje = false;
        bool emergencia = true;
        public bool mushunia = false;
        public bool cxe = false;
        public bool sistemas = false;
        public bool subSecuente = false;
        #region Constructor  
        //public frm_AyudaPacientes(List<PACIENTES> pacientes)
        //{
        public frm_AyudaPacientes(bool _emergencia = true, bool _subSecuente = false)
        {
            InitializeComponent();

            emergencia = _emergencia;
            subSecuente = _subSecuente;
        }

        private void Buscar()
        {
            try
            {
                string id = txt_busqCi.Text.ToString();
                string historia = txt_busqHist.Text.ToString();
                string nombre = txt_busqNom.Text.ToString();
                int cantidad = 100;
                if (!subSecuente)
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
                        Int16 AreaUsuario = 1;
                        DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario));
                        bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
                        if (parse)
                        {
                            switch (AreaUsuario)
                            {
                                case 2:
                                    consultaPacientes = NegPacientes.recuperarPacientesMushunia(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                                    triaje = false;
                                    break;
                                case 3:
                                    consultaPacientes = NegPacientes.RecuperarPacientesListaBrigada(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
                                    triaje = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                    else
                        consultaPacientes = NegPacientes.RecuperarPacientesListaEmerg(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
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
                    consultaPacientes = NegPacientes.RecuperarPacientesListaSubSecuentes(id, historia, nombre, cantidad, EmergenciaPAR.CodigoEmergencia);
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
                campoPadre.Tag = true;
                campoAtencion.Text = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
                registro.codigoAtencionA = campoAtencion.Text;
                campoPadre.Text = grid.ActiveRow.Cells[columnabuscada].Value.ToString();
                campoAtencion.Text = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
                campoFecha = grid.ActiveRow.Cells["FECHA_NACIMIENTO"].Value.ToString();
                campoId = grid.ActiveRow.Cells["ID"].Value.ToString();
                campoFechaAtencion = grid.ActiveRow.Cells["FECHA_ATENCION"].Value.ToString();
                ateCodigo = grid.ActiveRow.Cells[columnaAtencion].Value.ToString();
                this.Close();
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

    }
}
