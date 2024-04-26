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
using Core.Entidades;

namespace His.Maintenance
{
    public partial class frm_NumeroControl : Form
    {
        #region Variables
        public NUMERO_CONTROL numerocontrolOriginal = new NUMERO_CONTROL();
        public NUMERO_CONTROL numerocontrolModificada = new NUMERO_CONTROL();
        public List<NUMERO_CONTROL> numerocontrol = new List<NUMERO_CONTROL>();
        public int columnabuscada;
        #endregion

        #region Constructores
        public frm_NumeroControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_NumeroControl_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaNumeroControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void gridbancos_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                numerocontrolModificada = gridnumerocontrol.CurrentRow.DataBoundItem as NUMERO_CONTROL;
                numerocontrolOriginal = numerocontrolModificada.ClonarEntidad();
                AgregarBindigControles();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult resultado;

                resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    NegNumeroControl.EliminarNumeroControl(numerocontrolModificada.ClonarEntidad());
                    RecuperaNumeroControl();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false);
                    MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperaNumeroControl();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false, true);
                numerocontrolOriginal = new NUMERO_CONTROL();
                numerocontrolModificada = new NUMERO_CONTROL();

                ResetearControles();
                numerocontrolModificada.CODCON = int.Parse((NegNumeroControl.RecuperaMaximoNumeroControl() + 1).ToString());
                AgregarBindigControles();
                txt_nombre.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_numcontrol.Focus();
            }
        }
        private void txt_numcontrol_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    chk_automatico.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void gridbancos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridnumerocontrol.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                numerocontrol.Sort(new NumeroControlComparer(strColumnName, strSortOrder));
                gridnumerocontrol.DataSource = null;
                gridnumerocontrol.DataSource = numerocontrol;
                gridnumerocontrol.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridnumerocontrol.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridnumerocontrol.Columns["CODCON"].HeaderText = "CODIGO";
                gridnumerocontrol.Columns["MODCON"].HeaderText = "NOMBRE";
                gridnumerocontrol.Columns["TIPCON"].HeaderText = "AUTOMATICO";
                gridnumerocontrol.Columns["NUMCON"].HeaderText = "NUMERO";

                gridnumerocontrol.Columns["OCUPADO"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridnumerocontrol, new Point(MousePosition.X - e.X - gridnumerocontrol.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Departamento", gridnumerocontrol.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridnumerocontrol.RowCount - 1; i++)
                    {
                        if (gridnumerocontrol.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridnumerocontrol.CurrentCell = gridnumerocontrol.Rows[i].Cells[columnabuscada];
                        // gridMedicos.Rows[i].Cells[columnabuscada].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion

        #region Metodos privados
        private void AgregarBindigControles()
        {
            Binding CODCON = new Binding("Text", numerocontrolModificada, "CODCON", true);
            Binding MODCON = new Binding("Text", numerocontrolModificada, "MODCON", true);
            Binding NUMCON = new Binding("Text", numerocontrolModificada, "NUMCON", true);
            //Binding BAN_ESTADO = new Binding("Checked", numerocontrolModificada, "BAN_ESTADO", true);

            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_numcontrol.DataBindings.Clear();
            //chk_automatico.DataBindings.Clear();

            txt_codigo.DataBindings.Add(CODCON);
            txt_nombre.DataBindings.Add(MODCON);
            txt_numcontrol.DataBindings.Add(NUMCON);
            //chk_automatico.DataBindings.Add(BAN_ESTADO);
            if (numerocontrolModificada.TIPCON == "A")
                chk_automatico.Checked = true;
            else
                chk_automatico.Checked = false;

        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            numerocontrolModificada = new NUMERO_CONTROL();
            numerocontrolOriginal = new NUMERO_CONTROL();

            txt_codigo.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_numcontrol.Text = string.Empty;
            chk_automatico.Checked = false;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (numerocontrolModificada.CODCON == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (numerocontrolModificada.MODCON == null || numerocontrolModificada.MODCON == string.Empty)
            {
                AgregarError(txt_nombre);
                valido = false;
            }
            if (numerocontrolModificada.NUMCON == null || numerocontrolModificada.NUMCON == string.Empty)
            {
                AgregarError(txt_numcontrol);
                valido = false;
            }
            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridnumerocontrol.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (chk_automatico.Checked == true)
                        numerocontrolModificada.TIPCON = "A";
                    else
                        numerocontrolModificada.TIPCON = "M";

                    if (numerocontrolModificada.EntityKey == null)
                    {
                        NegNumeroControl.CrearNumeroControl(numerocontrolModificada);

                    }
                    else
                    {
                        NegNumeroControl.GrabarNumeroControl(numerocontrolModificada, numerocontrolOriginal);

                    }

                    RecuperaNumeroControl();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// fuencion para habilitar los botones
        /// </summary>
        /// <param name="control">boton que se acciona</param>
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;

        }
        private void RecuperaNumeroControl()
        {
            numerocontrol = NegNumeroControl.RecuperaNumeroControl();
            gridnumerocontrol.DataSource = numerocontrol;
            gridnumerocontrol.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridnumerocontrol.Columns["CODCON"].HeaderText = "CODIGO";
            gridnumerocontrol.Columns["MODCON"].HeaderText = "NOMBRE";
            gridnumerocontrol.Columns["TIPCON"].HeaderText = "AUTOMATICO";
            gridnumerocontrol.Columns["NUMCON"].HeaderText = "NUMERO";

            gridnumerocontrol.Columns["OCUPADO"].Visible = false;

        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridnumerocontrol.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridnumerocontrol.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridnumerocontrol.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridnumerocontrol.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridnumerocontrol.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class NumeroControlComparer : IComparer<NUMERO_CONTROL>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public NumeroControlComparer(string strMemberName, SortOrder sortingOrder)
            {
                memberName = strMemberName;
                sortOrder = sortingOrder;
            }

            /// <summary> 
            /// Compares two Students based on member name and sort order 
            /// and return the result. 
            /// </summary> 
            /// <param name="Student1"></param> 
            /// <param name="Student2"></param> 
            /// <returns></returns> 
            public int Compare(NUMERO_CONTROL Student1, NUMERO_CONTROL Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "CODCON":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CODCON.CompareTo(Student2.CODCON);
                        }
                        else
                        {
                            returnValue = Student2.CODCON.CompareTo(Student1.CODCON);
                        }

                        break;
                    case "MODCON":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.MODCON.CompareTo(Student2.MODCON);
                        }
                        else
                        {
                            returnValue = Student2.MODCON.CompareTo(Student1.MODCON);
                        }
                        break;
                    case "TIPCON":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TIPCON.CompareTo(Student2.TIPCON);
                        }
                        else
                        {
                            returnValue = Student2.TIPCON.CompareTo(Student1.TIPCON);
                        }
                        break;
                    case "NUMCON":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.NUMCON.CompareTo(Student2.NUMCON);
                        }
                        else
                        {
                            returnValue = Student2.NUMCON.CompareTo(Student1.NUMCON);
                        }
                        break;


                }
                return returnValue;
            }
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        #endregion

        

        
        

  
    }
}
