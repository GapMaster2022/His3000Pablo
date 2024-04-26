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
    public partial class frm_TipoNegocio : Form
    {
        #region variables
        public TIPO_NEGOCIO tiponegOriginal = new TIPO_NEGOCIO();
        public TIPO_NEGOCIO tiponegModificada = new TIPO_NEGOCIO();
        public List<TIPO_NEGOCIO> tiponegocio = new List<TIPO_NEGOCIO>();
        public int columnabuscada;
        #endregion

        #region constructor
        public frm_TipoNegocio()
        {
            InitializeComponent();
        }
        #endregion

        #region eventos
        private void frm_TipoNegocio_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaTipoNegocios();
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
        private void gridTiponegocio_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                tiponegModificada = gridTiponegocio.CurrentRow.DataBoundItem as TIPO_NEGOCIO;
                tiponegOriginal = tiponegModificada.ClonarEntidad();
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
                    NegTipoNegocio.EliminarTipoNegocio(tiponegModificada.ClonarEntidad());
                    RecuperaTipoNegocios();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false);
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
            RecuperaTipoNegocios();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false,true);
                tiponegOriginal = new TIPO_NEGOCIO();
                tiponegModificada = new TIPO_NEGOCIO();

                ResetearControles();
                tiponegModificada.CODTIPNEG = Int16.Parse((NegTipoNegocio.RecuperaMaximoTipoNegocio() + 1).ToString());
                AgregarBindigControles();
                txt_descripcion.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void gridTiponegocio_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridTiponegocio.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                tiponegocio.Sort(new TipoNegocioComparer(strColumnName, strSortOrder));
                gridTiponegocio.DataSource = null;
                gridTiponegocio.DataSource = tiponegocio;
                gridTiponegocio.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridTiponegocio.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridTiponegocio.Columns["CODTIPNEG"].HeaderText = "CODIGO";
                gridTiponegocio.Columns["TIPNEG"].HeaderText = "DESCRIPCION";
                gridTiponegocio.Columns["LOCALES"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridTiponegocio, new Point(MousePosition.X - e.X - gridTiponegocio.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Departamento", gridTiponegocio.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridTiponegocio.RowCount - 1; i++)
                    {
                        if (gridTiponegocio.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridTiponegocio.CurrentCell = gridTiponegocio.Rows[i].Cells[columnabuscada];

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion

        #region metodos privados
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
        private void RecuperaTipoNegocios()
        {
            tiponegocio = NegTipoNegocio.RecuperaTipoNegocios();
            gridTiponegocio.DataSource = tiponegocio;
            gridTiponegocio.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridTiponegocio.Columns["CODTIPNEG"].HeaderText = "CODIGO";
            gridTiponegocio.Columns["TIPNEG"].HeaderText = "DESCRIPCION";
            gridTiponegocio.Columns["LOCALES"].Visible = false;
       
        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridTiponegocio.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (tiponegModificada.EntityKey == null)
                    {
                        NegTipoNegocio.CrearTipoNegocio(tiponegModificada);

                    }
                    else
                    {
                        NegTipoNegocio.GrabarTipoNegocio(tiponegModificada, tiponegOriginal);

                    }

                    RecuperaTipoNegocios();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            tiponegModificada = new TIPO_NEGOCIO();
            tiponegOriginal = new TIPO_NEGOCIO();

            txt_codigo.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (tiponegModificada.CODTIPNEG == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (tiponegModificada.TIPNEG == null || tiponegModificada.TIPNEG == string.Empty)
            {
                AgregarError(txt_descripcion);
                valido = false;
            }

            return valido;

        }
        private void AgregarBindigControles()
        {
            Binding CODTIPNEG = new Binding("Text", tiponegModificada, "CODTIPNEG", true);
            Binding TIPNEG = new Binding("Text", tiponegModificada, "TIPNEG", true);
            

            txt_codigo.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();

            txt_codigo.DataBindings.Add(CODTIPNEG);
            txt_descripcion.DataBindings.Add(TIPNEG);
            
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridTiponegocio.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridTiponegocio.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridTiponegocio.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridTiponegocio.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridTiponegocio.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class TipoNegocioComparer : IComparer<TIPO_NEGOCIO>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public TipoNegocioComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(TIPO_NEGOCIO Student1, TIPO_NEGOCIO Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "CODTIPNEG":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CODTIPNEG.CompareTo(Student2.CODTIPNEG);
                        }
                        else
                        {
                            returnValue = Student2.CODTIPNEG.CompareTo(Student1.CODTIPNEG);
                        }

                        break;
                    case "TIPNEG":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TIPNEG.CompareTo(Student2.TIPNEG);
                        }
                        else
                        {
                            returnValue = Student2.TIPNEG.CompareTo(Student1.TIPNEG);
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
