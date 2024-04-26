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
    public partial class frm_TipoMedico : Form
    {
        #region Variables
        public TIPO_MEDICO tipomedicoOriginal = new TIPO_MEDICO();
        public TIPO_MEDICO tipomedicoModificada = new TIPO_MEDICO();
        public List<TIPO_MEDICO> tipomedico = new List<TIPO_MEDICO>();
        public int columnabuscada;
        #endregion

        #region Constructores
        public frm_TipoMedico()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_TipoMedico_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaTipoMedicos();
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
        private void gridtipomedico_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                tipomedicoModificada = gridtipomedico.CurrentRow.DataBoundItem as TIPO_MEDICO;
                tipomedicoOriginal = tipomedicoModificada.ClonarEntidad();
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
                    NegTipoMedico.EliminarTipoMedico(tipomedicoModificada.ClonarEntidad());
                    RecuperaTipoMedicos();
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
            RecuperaTipoMedicos();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false, true);
                tipomedicoOriginal = new TIPO_MEDICO();
                tipomedicoModificada = new TIPO_MEDICO();

                ResetearControles();
                tipomedicoModificada.TIM_CODIGO = Int16.Parse((NegTipoMedico.RecuperaMaximoTipoMedico() + 1).ToString());
                tipomedicoModificada.TIM_ESTADO = true;
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
                txt_cuentacontable.Focus();
            }
        }
        private void txt_cuentacontable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                chk_activo.Focus();
            }
        }
        private void gridtipomedico_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridtipomedico.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                tipomedico.Sort(new TipoMedicoComparer(strColumnName, strSortOrder));
                gridtipomedico.DataSource = null;
                gridtipomedico.DataSource = tipomedico;
                gridtipomedico.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridtipomedico.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridtipomedico.Columns["TIM_CODIGO"].HeaderText = "CODIGO";
                gridtipomedico.Columns["TIM_NOMBRE"].HeaderText = "NOMBRE";
                gridtipomedico.Columns["TIM_ESTADO"].HeaderText = "ESTADO";
                gridtipomedico.Columns["MEDICOS"].Visible = false;
                gridtipomedico.Columns["TIM_CUENTA_CONTABLE"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridtipomedico, new Point(MousePosition.X - e.X - gridtipomedico.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Departamento", gridtipomedico.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridtipomedico.RowCount - 1; i++)
                    {
                        if (gridtipomedico.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridtipomedico.CurrentCell = gridtipomedico.Rows[i].Cells[columnabuscada];
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion

        #region Metodos Privados
        private void AgregarBindigControles()
        {
            Binding TIM_CODIGO = new Binding("Text", tipomedicoModificada, "TIM_CODIGO", true);
            Binding TIM_NOMBRE = new Binding("Text", tipomedicoModificada, "TIM_NOMBRE", true);
            Binding TIM_CUENTA_CONTABLE = new Binding("Text", tipomedicoModificada, "TIM_CUENTA_CONTABLE", true);
            Binding TIM_ESTADO = new Binding("Checked", tipomedicoModificada, "TIM_ESTADO", true);

            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_cuentacontable.DataBindings.Clear();
            chk_activo.DataBindings.Clear();
            txt_codigo.DataBindings.Add(TIM_CODIGO);
            txt_nombre.DataBindings.Add(TIM_NOMBRE);
            txt_cuentacontable.DataBindings.Add(TIM_CUENTA_CONTABLE);
            chk_activo.DataBindings.Add(TIM_ESTADO);

        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            tipomedicoModificada = new TIPO_MEDICO();
            tipomedicoOriginal = new TIPO_MEDICO();

            txt_codigo.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_cuentacontable.Text = string.Empty;
            chk_activo.Checked = false;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (tipomedicoModificada.TIM_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (tipomedicoModificada.TIM_NOMBRE == null || tipomedicoModificada.TIM_NOMBRE == string.Empty)
            {
                AgregarError(txt_nombre);
                valido = false;
            }

            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridtipomedico.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (tipomedicoModificada.EntityKey == null)
                    {
                        NegTipoMedico.CrearTipoMedico(tipomedicoModificada);

                    }
                    else
                    {
                        NegTipoMedico.GrabarTipoMedico(tipomedicoModificada, tipomedicoOriginal);

                    }

                    RecuperaTipoMedicos();
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
        private void RecuperaTipoMedicos()
        {
            tipomedico =NegTipoMedico.RecuperaTipoMedicos ();
            gridtipomedico.DataSource = tipomedico;
            gridtipomedico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridtipomedico.Columns["TIM_CODIGO"].HeaderText = "CODIGO";
            gridtipomedico.Columns["TIM_NOMBRE"].HeaderText = "NOMBRE";
            gridtipomedico.Columns["TIM_ESTADO"].HeaderText = "ESTADO";
            gridtipomedico.Columns["MEDICOS"].Visible = false;
            gridtipomedico.Columns["TIM_CUENTA_CONTABLE"].Visible = false;
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridtipomedico.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridtipomedico.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridtipomedico.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridtipomedico.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridtipomedico.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class TipoMedicoComparer : IComparer<TIPO_MEDICO>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public TipoMedicoComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(TIPO_MEDICO Student1, TIPO_MEDICO Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "TIM_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TIM_CODIGO.CompareTo(Student2.TIM_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.TIM_CODIGO.CompareTo(Student1.TIM_CODIGO);
                        }

                        break;
                    case "TIM_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TIM_NOMBRE.CompareTo(Student2.TIM_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.TIM_NOMBRE.CompareTo(Student1.TIM_NOMBRE);
                        }
                        break;
                    case "TIM_ESTADO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TIM_ESTADO.CompareTo(Student2.TIM_ESTADO);
                        }
                        else
                        {
                            returnValue = Student2.TIM_ESTADO.CompareTo(Student1.TIM_ESTADO);
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
