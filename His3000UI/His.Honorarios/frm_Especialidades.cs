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
using His.Parametros;

namespace His.Honorarios
{
    public partial class frm_Especialidades : Form
    {
        #region Variables
        public ESPECIALIDADES_MEDICAS especialidadOriginal = new ESPECIALIDADES_MEDICAS();
        public ESPECIALIDADES_MEDICAS especialidadModificada = new ESPECIALIDADES_MEDICAS();
        public List<ESPECIALIDADES_MEDICAS> especialidad = new List<ESPECIALIDADES_MEDICAS>();
        public int columnabuscada;
        #endregion

        #region Constructor
        public frm_Especialidades()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_Especialidades_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaEspecialidades();
                especialidad = NegEspecialidades.ListaEspecialidades();
                cmb_esppadre.DataSource = especialidad;
                cmb_esppadre.ValueMember = "ESP_CODIGO";
                cmb_esppadre.DisplayMember = "ESP_NOMBRE";
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
        private void gridespercialidades_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false,true);
                especialidadModificada = gridespercialidades.CurrentRow.DataBoundItem as ESPECIALIDADES_MEDICAS;
                especialidadOriginal = especialidadModificada.ClonarEntidad();
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

                resultado = MessageBox.Show("Desea eliminar los Datos?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    NegEspecialidades.EliminarEspecialidad(especialidadModificada.ClonarEntidad());
                    RecuperaEspecialidades();
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
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperaEspecialidades();
            HalitarControles(false, false, false, false, false, true, false);

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
                especialidadOriginal = new ESPECIALIDADES_MEDICAS();
                especialidadModificada = new ESPECIALIDADES_MEDICAS();

                ResetearControles();
                especialidadModificada.ESP_CODIGO = Int16.Parse((NegEspecialidades.RecuperaMaximoEspecialidad() + 1).ToString());
                especialidadModificada.ESP_ESTADO = true;
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
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_descripcion.Focus();
                }
        }
        private void txt_descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    cmb_esppadre.Focus();
                }
            
            
        }
        private void txt_cuentacontable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                chk_eliminado.Focus();
            }
        }
        private void cmb_esppadre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
                txt_cuentacontable.Focus();
            }
        }
        private void gridespercialidades_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ////get the current column details 
                //string strColumnName = gridespercialidades.Columns[e.ColumnIndex].Name;
                //SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                //especialidad.Sort(new EspecialidadesMedComparer(strColumnName, strSortOrder));
                //gridespercialidades.DataSource = null;
                //gridespercialidades.DataSource = especialidad;
                //gridespercialidades.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                //gridespercialidades.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                //gridespercialidades.Columns["ESP_NUMERO"].HeaderText = "NUMERO";
                //gridespercialidades.Columns["ESP_CODIGO"].HeaderText = "CODIGO";
                //gridespercialidades.Columns["ESP_NOMBRE"].HeaderText = "NOMBRE";
                //gridespercialidades.Columns["ESP_DESCRIPCION"].HeaderText = "DESCRIPCION";
                //gridespercialidades.Columns["ESP_ESTADO"].HeaderText = "ACTIVO";

                //gridespercialidades.Columns["ESP_PADRE"].Visible = false;
                //gridespercialidades.Columns["MEDICOS"].Visible = false;
                //gridespercialidades.Columns["ESP_CUENTA_CONTABLE"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridespercialidades, new Point(MousePosition.X - e.X - gridespercialidades.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Comisión Aportes", gridespercialidades.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridespercialidades.RowCount - 1; i++)
                    {
                        if (gridespercialidades.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridespercialidades.CurrentCell = gridespercialidades.Rows[i].Cells[columnabuscada];

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion

        #region Medotos Privados
        private void AgregarBindigControles()
        {
            Binding ESP_CODIGO = new Binding("Text", especialidadModificada, "ESP_CODIGO", true);
            Binding ESP_NOMBRE = new Binding("Text", especialidadModificada, "ESP_NOMBRE", true);
            Binding ESP_CUENTA_CONTABLE = new Binding("Text", especialidadModificada, "ESP_CUENTA_CONTABLE", true);
            Binding ESP_DESCRIPCION = new Binding("Text", especialidadModificada, "ESP_DESCRIPCION", true);
            Binding ESP_PADRE = new Binding("SelectedValue", especialidadModificada, "ESP_PADRE", true);
            Binding ESP_ESTADO = new Binding("Checked", especialidadModificada, "ESP_ESTADO", true);

            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();
            txt_cuentacontable.DataBindings.Clear();
            cmb_esppadre.DataBindings.Clear();
            chk_eliminado.DataBindings.Clear();

            txt_codigo.DataBindings.Add(ESP_CODIGO);
            txt_nombre.DataBindings.Add(ESP_NOMBRE);
            txt_descripcion.DataBindings.Add(ESP_DESCRIPCION);
            txt_cuentacontable.DataBindings.Add(ESP_CUENTA_CONTABLE);
            cmb_esppadre.DataBindings.Add(ESP_PADRE);
            chk_eliminado.DataBindings.Add(ESP_ESTADO);
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            especialidadModificada = new ESPECIALIDADES_MEDICAS();
            especialidadOriginal = new ESPECIALIDADES_MEDICAS();

            txt_codigo.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            txt_cuentacontable.Text = string.Empty;
            cmb_esppadre.SelectedItem = -1;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (especialidadModificada.ESP_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (especialidadModificada.ESP_NOMBRE == null || especialidadModificada.ESP_NOMBRE == string.Empty)
            {
                AgregarError(txt_descripcion);
                valido = false;
            }

            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridespercialidades.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (especialidadModificada.EntityKey == null)
                    {
                        NegEspecialidades.CrearEspecialidad(especialidadModificada);

                    }
                    else
                    {
                        NegEspecialidades.GrabarEspecialidad(especialidadModificada, especialidadOriginal);

                    }

                    RecuperaEspecialidades();
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
        private void RecuperaEspecialidades()
        {
            especialidad =NegEspecialidades.ListaEspecialidades();
            gridespercialidades.DataSource = especialidad;
            gridespercialidades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridespercialidades.Columns["ESP_CODIGO"].Visible = false;
            gridespercialidades.Columns["ESP_NOMBRE"].HeaderText = "NOMBRE";
            gridespercialidades.Columns["ESP_DESCRIPCION"].HeaderText = "DESCRIPCION";
            gridespercialidades.Columns["ESP_ESTADO"].HeaderText = "ACTIVO";

            gridespercialidades.Columns["ESP_PADRE"].Visible = false;
            gridespercialidades.Columns["MEDICOS"].Visible = false;
            gridespercialidades.Columns["ESP_CUENTA_CONTABLE"].Visible = false;

        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridespercialidades.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridespercialidades.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridespercialidades.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridespercialidades.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridespercialidades.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class EspecialidadesMedComparer : IComparer<ESPECIALIDADES_MEDICAS>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public EspecialidadesMedComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(ESPECIALIDADES_MEDICAS Student1, ESPECIALIDADES_MEDICAS Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "ESP_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ESP_CODIGO.CompareTo(Student2.ESP_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.ESP_CODIGO.CompareTo(Student1.ESP_CODIGO);
                        }

                        break;
                    case "ESP_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ESP_NOMBRE.CompareTo(Student2.ESP_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.ESP_NOMBRE.CompareTo(Student1.ESP_NOMBRE);
                        }
                        break;
                    //case "ESP_DESCRIPCION":
                    //    if (sortOrder == SortOrder.Ascending)
                    //    {
                    //        returnValue = Student1.ESP_DESCRIPCION.CompareTo(Student2.ESP_DESCRIPCION);
                    //    }
                    //    else
                    //    {
                    //        returnValue = Student2.ESP_DESCRIPCION.CompareTo(Student1.ESP_DESCRIPCION);
                    //    }
                    //    break;
                    case "ESP_ESTADO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ESP_ESTADO.CompareTo(Student2.ESP_ESTADO);
                        }
                        else
                        {
                            returnValue = Student2.ESP_ESTADO.CompareTo(Student1.ESP_ESTADO);
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

        private void gridespercialidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
