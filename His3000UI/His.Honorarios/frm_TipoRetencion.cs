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
    public partial class frm_TipoRetencion : Form
    {
        #region Variable
        public RETENCIONES_FUENTE retencionOriginal = new RETENCIONES_FUENTE();
        public RETENCIONES_FUENTE retencionModificada = new RETENCIONES_FUENTE();
        public List<RETENCIONES_FUENTE> retencionesfuente = new List<RETENCIONES_FUENTE>();
        public int columnabuscada;
        #endregion

        #region Constructores
        public frm_TipoRetencion()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_TipoRetencion_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true,false);
                RecuperaRetencionFuente();
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
        private void gridretencionfuente_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                retencionModificada = gridretencionfuente.CurrentRow.DataBoundItem as RETENCIONES_FUENTE;
                retencionOriginal = retencionModificada.ClonarEntidad();
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
                    NegRetencionesFuente.EliminarRetencionFuente(retencionModificada.ClonarEntidad());
                    RecuperaRetencionFuente();
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
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperaRetencionFuente();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false, true);
                retencionOriginal = new RETENCIONES_FUENTE();
                retencionModificada = new RETENCIONES_FUENTE();

                ResetearControles();
                retencionModificada.RET_CODIGO = Int16.Parse((NegRetencionesFuente.RecuperaMaximoRetencionFuente() + 1).ToString());
                AgregarBindigControles();
                txt_descripcion.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_porcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    txt_referencia.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*Esto no permite escribir en el textbox / Giovanny Tapia / 14/03/2013 */

            //if (Char.IsDigit(e.KeyChar))
            //    e.Handled = false;
            //else if (e.KeyChar == 46)
            //    e.Handled = false;
            //else if (Char.IsControl(e.KeyChar))
            //{
            //    if (e.KeyChar == (char)(Keys.Enter))
            //    {
            //        e.Handled = true;
            //        txt_porcentaje.Focus();
            //    }
            //}
            //else if (Char.IsSeparator(e.KeyChar))
            //    e.Handled = false;
            //else
            //    e.Handled = true;

        }
        private void gridretencionfuente_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridretencionfuente.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                retencionesfuente.Sort(new RetencionFuenteComparer(strColumnName, strSortOrder));
                gridretencionfuente.DataSource = null;
                gridretencionfuente.DataSource = retencionesfuente;
                gridretencionfuente.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridretencionfuente.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridretencionfuente.Columns["RET_CODIGO"].HeaderText = "CODIGO";
                gridretencionfuente.Columns["RET_DESCRIPCION"].HeaderText = "DESCRIPCION";
                gridretencionfuente.Columns["RET_PORCENTAJE"].HeaderText = "PORCENTAJE";
                gridretencionfuente.Columns["RET_REFERENCIA"].HeaderText = "RETENCION";
                gridretencionfuente.Columns["MEDICOS"].Visible = false;
                gridretencionfuente.Columns["RETENCIONES"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridretencionfuente, new Point(MousePosition.X - e.X - gridretencionfuente.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Comisión Aportes", gridretencionfuente.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridretencionfuente.RowCount - 1; i++)
                    {
                        if (gridretencionfuente.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridretencionfuente.CurrentCell = gridretencionfuente.Rows[i].Cells[columnabuscada];

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
            Binding RET_CODIGO = new Binding("Text", retencionModificada, "RET_CODIGO", true);
            Binding RET_DESCRIPCION = new Binding("Text", retencionModificada, "RET_DESCRIPCION", true);
            Binding RET_PORCENTAJE = new Binding("Text", retencionModificada, "RET_PORCENTAJE", true);
            Binding RET_REFERENCIA = new Binding("Text", retencionModificada, "RET_REFERENCIA", true);

            txt_codigo.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();
            txt_porcentaje.DataBindings.Clear();
            txt_referencia.DataBindings.Clear();

            txt_codigo.DataBindings.Add(RET_CODIGO);
            txt_descripcion.DataBindings.Add(RET_DESCRIPCION);
            txt_porcentaje.DataBindings.Add(RET_PORCENTAJE);
            txt_referencia.DataBindings.Add(RET_REFERENCIA);
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            retencionModificada = new RETENCIONES_FUENTE();
            retencionOriginal = new RETENCIONES_FUENTE();

            txt_codigo.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            txt_porcentaje.Text = string.Empty;
            txt_referencia.Text = string.Empty;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (retencionModificada.RET_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (retencionModificada.RET_DESCRIPCION == null || retencionModificada.RET_DESCRIPCION == string.Empty)
            {
                AgregarError(txt_descripcion);
                valido = false;
            }
            if (retencionModificada.RET_PORCENTAJE == null)
            {
                AgregarError(txt_porcentaje);
                valido = false;
            }
            if (retencionModificada.RET_REFERENCIA == null || retencionModificada.RET_REFERENCIA == string.Empty)
            {
                AgregarError(txt_referencia);
                valido = false;
            }
            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridretencionfuente.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", "Tipo Documento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (retencionModificada.EntityKey == null)
                    {
                        NegRetencionesFuente.CrearRetencionFuente(retencionModificada);

                    }
                    else
                    {
                        NegRetencionesFuente.GrabarRetencionFuente(retencionModificada, retencionOriginal);

                    }

                    RecuperaRetencionFuente();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false);
                    MessageBox.Show("Datos Almacenados Correctamente");
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
        private void RecuperaRetencionFuente()
        {
            retencionesfuente =NegRetencionesFuente.RecuperaRetencionesFuente();
            gridretencionfuente.DataSource = retencionesfuente;
            gridretencionfuente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridretencionfuente.Columns["RET_CODIGO"].HeaderText = "CODIGO";
            gridretencionfuente.Columns["RET_DESCRIPCION"].HeaderText = "DESCRIPCION";
            gridretencionfuente.Columns["RET_PORCENTAJE"].HeaderText = "PORCENTAJE";
            gridretencionfuente.Columns["RET_REFERENCIA"].HeaderText = "RETENCION";
            gridretencionfuente.Columns["MEDICOS"].Visible = false;
            gridretencionfuente.Columns["RETENCIONES"].Visible = false;
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridretencionfuente.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridretencionfuente.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridretencionfuente.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridretencionfuente.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridretencionfuente.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class RetencionFuenteComparer : IComparer<RETENCIONES_FUENTE>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public RetencionFuenteComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(RETENCIONES_FUENTE Student1, RETENCIONES_FUENTE Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "RET_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.RET_CODIGO.CompareTo(Student2.RET_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.RET_CODIGO.CompareTo(Student1.RET_CODIGO);
                        }

                        break;
                    case "RET_DESCRIPCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.RET_DESCRIPCION.CompareTo(Student2.RET_DESCRIPCION);
                        }
                        else
                        {
                            returnValue = Student2.RET_DESCRIPCION.CompareTo(Student1.RET_DESCRIPCION);
                        }
                        break;
                    case "RET_PORCENTAJE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.RET_PORCENTAJE.CompareTo(Student2.RET_PORCENTAJE);
                        }
                        else
                        {
                            returnValue = Student2.RET_PORCENTAJE.CompareTo(Student1.RET_PORCENTAJE);
                        }
                        break;
                    case "RET_REFERENCIA":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.RET_REFERENCIA.CompareTo(Student2.RET_REFERENCIA);
                        }
                        else
                        {
                            returnValue = Student2.RET_REFERENCIA.CompareTo(Student1.RET_REFERENCIA);
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
