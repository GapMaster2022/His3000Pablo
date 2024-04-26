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

namespace His.Honorarios
{
    public partial class frm_TipoHonorario : Form
    {
        #region Variables
        public TIPO_HONORARIO tipohonOriginal = new TIPO_HONORARIO();
        public TIPO_HONORARIO tipohonModificada = new TIPO_HONORARIO();
        public List<TIPO_HONORARIO> tipohonorario = new List<TIPO_HONORARIO>();
        public int columnabuscada;
        #endregion

        #region Constructores
        public frm_TipoHonorario()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_TipoHonorario_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaTipoHonorarios();
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
        private void gridtipohonorario_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                tipohonModificada = gridtipohonorario.CurrentRow.DataBoundItem as TIPO_HONORARIO;
                tipohonOriginal = tipohonModificada.ClonarEntidad();
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
                    NegTipoHonorario.EliminarTipoHonorario(tipohonModificada.ClonarEntidad());
                    RecuperaTipoHonorarios();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false);
                    MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Operación Invalida, no se puede eliminar el Registro hay datos relacionados.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false, true);
                tipohonOriginal = new TIPO_HONORARIO();
                tipohonModificada = new TIPO_HONORARIO();

                ResetearControles();
                tipohonModificada.TIH_CODIGO = Int16.Parse((NegTipoHonorario.RecuperaMaximoTipoHonorario() + 1).ToString());
                tipohonModificada.TIH_ESTADO = true;
                AgregarBindigControles();
                txt_nombre.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperaTipoHonorarios();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_descripcion.Focus();
            }
        }
        private void txt_descripcion_KeyPress(object sender, KeyPressEventArgs e)
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
                chk_estado.Focus();
            }
        }
        private void gridtipohonorario_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridtipohonorario.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                tipohonorario.Sort(new TipoHonorarioComparer(strColumnName, strSortOrder));
                gridtipohonorario.DataSource = null;
                gridtipohonorario.DataSource = tipohonorario;
                gridtipohonorario.Columns["TIH_CODIGO"].HeaderText = "CODIGO";
                gridtipohonorario.Columns["TIH_NOMBRE"].HeaderText = "NOMBRE";
                gridtipohonorario.Columns["TIH_DESCRIPCION"].HeaderText = "DESCRIPCION";
                gridtipohonorario.Columns["TIH_ESTADO"].HeaderText = "ESTADO";

                gridtipohonorario.Columns["TIH_CUENTA_CONTABLE"].Visible = false;
                gridtipohonorario.Columns["MEDICOS"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridtipohonorario, new Point(MousePosition.X - e.X - gridtipohonorario.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Comisión Aportes", gridtipohonorario.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridtipohonorario.RowCount - 1; i++)
                    {
                        if (gridtipohonorario.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridtipohonorario.CurrentCell = gridtipohonorario.Rows[i].Cells[columnabuscada];

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
            Binding TIH_CODIGO = new Binding("Text", tipohonModificada, "TIH_CODIGO", true);
            Binding TIH_NOMBRE = new Binding("Text", tipohonModificada, "TIH_NOMBRE", true);
            Binding TIH_DESCRIPCION = new Binding("Text", tipohonModificada, "TIH_DESCRIPCION", true);
            Binding TIH_CUENTA_CONTABLE = new Binding("Text", tipohonModificada, "TIH_CUENTA_CONTABLE", true);
            Binding TIH_ESTADO = new Binding("Checked", tipohonModificada, "TIH_ESTADO", true);

            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();
            txt_cuentacontable.DataBindings.Clear();
            chk_estado.DataBindings.Clear();

            txt_codigo.DataBindings.Add(TIH_CODIGO);
            txt_nombre.DataBindings.Add(TIH_NOMBRE);
            txt_descripcion.DataBindings.Add(TIH_DESCRIPCION);
            txt_cuentacontable.DataBindings.Add(TIH_CUENTA_CONTABLE);
            chk_estado.DataBindings.Add(TIH_ESTADO);

        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            tipohonModificada = new TIPO_HONORARIO();
            tipohonOriginal = new TIPO_HONORARIO();

            txt_codigo.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            txt_cuentacontable.Text = string.Empty;
            chk_estado.Checked = true;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (tipohonModificada.TIH_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (tipohonModificada.TIH_NOMBRE == null || tipohonModificada.TIH_NOMBRE == string.Empty)
            {
                AgregarError(txt_nombre);
                valido = false;
            }

            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridtipohonorario.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (tipohonModificada.EntityKey == null)
                    {
                        NegTipoHonorario.CrearTipoHonorario(tipohonModificada);

                    }
                    else
                    {
                        NegTipoHonorario.GrabarTipoHonorario(tipohonModificada, tipohonOriginal);

                    }

                    RecuperaTipoHonorarios();
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
        private void RecuperaTipoHonorarios()
        {
            tipohonorario =NegTipoHonorario.RecuperaTipoHonorarios();
            gridtipohonorario.DataSource = tipohonorario;
            gridtipohonorario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridtipohonorario.Columns["TIH_CODIGO"].HeaderText = "CODIGO";
            gridtipohonorario.Columns["TIH_NOMBRE"].HeaderText = "NOMBRE";
            gridtipohonorario.Columns["TIH_DESCRIPCION"].HeaderText = "DESCRIPCION";
            gridtipohonorario.Columns["TIH_ESTADO"].HeaderText = "ESTADO";

            gridtipohonorario.Columns["TIH_CUENTA_CONTABLE"].Visible = false;
            gridtipohonorario.Columns["MEDICOS"].Visible = false;
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridtipohonorario.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridtipohonorario.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridtipohonorario.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridtipohonorario.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridtipohonorario.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class TipoHonorarioComparer : IComparer<TIPO_HONORARIO>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public TipoHonorarioComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(TIPO_HONORARIO Student1, TIPO_HONORARIO Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "TIH_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TIH_CODIGO.CompareTo(Student2.TIH_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.TIH_CODIGO.CompareTo(Student1.TIH_CODIGO);
                        }

                        break;
                    case "TIH_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TIH_NOMBRE.CompareTo(Student2.TIH_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.TIH_NOMBRE.CompareTo(Student1.TIH_NOMBRE);
                        }
                        break;
                    case "TIH_DESCRIPCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TIH_DESCRIPCION.CompareTo(Student2.TIH_DESCRIPCION);
                        }
                        else
                        {
                            returnValue = Student2.TIH_DESCRIPCION.CompareTo(Student1.TIH_DESCRIPCION);
                        }
                        break;
                    case "TIH_ESTADO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TIH_ESTADO.CompareTo(Student2.TIH_ESTADO);
                        }
                        else
                        {
                            returnValue = Student2.TIH_ESTADO.CompareTo(Student1.TIH_ESTADO);
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
