using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Entidades;
using His.Entidades;
using His.Negocio;

namespace TarifariosUI
{
    public partial class frmCatalogosCostosTipo : Form
    {
        #region Variables
        public CATALOGO_COSTOS_TIPO cctipoOriginal = new CATALOGO_COSTOS_TIPO();
        public CATALOGO_COSTOS_TIPO cctipoModificada = new CATALOGO_COSTOS_TIPO();
        public List<CATALOGO_COSTOS_TIPO> cctipo = new List<CATALOGO_COSTOS_TIPO>();
        public int columnabuscada;
        #endregion
        #region Constructores
        public frmCatalogosCostosTipo()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frmCatalogosCostosTipo_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaCctipo();
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
                MessageBox.Show(ex.Message);
            }
        }
        private void gridCctipo_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                cctipoModificada = gridCctipo.CurrentRow.DataBoundItem as CATALOGO_COSTOS_TIPO;
                cctipoOriginal = cctipoModificada.ClonarEntidad();
                AgregarBindigControles();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    NegCatalogoCostosTipo.EliminarCctipo(cctipoModificada.ClonarEntidad());
                    RecuperaCctipo();
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
            RecuperaCctipo();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false, true);
                cctipoOriginal = new CATALOGO_COSTOS_TIPO();
                cctipoModificada = new CATALOGO_COSTOS_TIPO();

                ResetearControles();
                cctipoModificada.CCT_CODIGO = Int16.Parse((NegCatalogoCostosTipo.RecuperaMaximoCctipo() + 1).ToString());
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
                //txt_cuentacontable.Focus();
            }
        }
        private void gridCctipo_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridCctipo.Columns[e.ColumnIndex].Name;

                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);
                cctipo.Sort(new CctipoComparer(strColumnName, strSortOrder));

                gridCctipo.DataSource = null;
                gridCctipo.DataSource = cctipo;
                gridCctipo.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridCctipo.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridCctipo.Columns["CCT_CODIGO"].HeaderText = "CODIGO";
                gridCctipo.Columns["CCT_NOMBRE"].HeaderText = "NOMBRE";
                //gridbancos.Columns["BAN_ESTADO"].HeaderText = "ESTADO";
                //gridbancos.Columns["MEDICOS"].Visible = false;
                //gridbancos.Columns["BAN_CUENTA_CONTABLE"].Visible = false;

            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                // mnuContexsecunadario.Show(gridCctipo, new Point(MousePosition.X - this.Left, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region Metodos Privados
        private void AgregarBindigControles()
        {
            Binding CCT_CODIGO = new Binding("Text", cctipoModificada, "CCT_CODIGO", true);
            Binding CCT_NOMBRE = new Binding("Text", cctipoModificada, "CCT_NOMBRE", true);
            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_codigo.DataBindings.Add(CCT_CODIGO);
            txt_nombre.DataBindings.Add(CCT_NOMBRE);

            //  txt_nombre.DataBindings.Add(CCT_NOMBRE);


        }
        private void ResetearControles()
        {
            cctipoModificada = new CATALOGO_COSTOS_TIPO();
            cctipoOriginal = new CATALOGO_COSTOS_TIPO();

            txt_codigo.Text = string.Empty;
            txt_nombre.Text = string.Empty;

        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (cctipoModificada.CCT_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (cctipoModificada.CCT_NOMBRE == null || cctipoModificada.CCT_NOMBRE == string.Empty)
            {
                AgregarError(txt_nombre);
                valido = false;
            }

            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridCctipo.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (cctipoModificada.EntityKey == null)
                    {
                        NegCatalogoCostosTipo.CrearCctipo(cctipoModificada);
                    }
                    else
                    {
                        NegCatalogoCostosTipo.GrabarCctipo(cctipoModificada, cctipoOriginal);

                    }

                    RecuperaCctipo();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            //grpDatosPrincipales.Enabled = datosPrincipales;

        }
        private void RecuperaCctipo()
        {
            cctipo = NegCatalogoCostosTipo.RecuperaCctipo();
            gridCctipo.DataSource = cctipo;
            gridCctipo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridCctipo.Columns["CCT_CODIGO"].HeaderText = "CODIGO";
            gridCctipo.Columns["CCT_NOMBRE"].HeaderText = "NOMBRE";
            gridCctipo.Columns["CATALOGO_COSTOS"].Visible = false;
            //gridbancos.Columns["BAN_CUENTA_CONTABLE"].Visible = false;

        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridCctipo.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridCctipo.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridCctipo.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridCctipo.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridCctipo.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class CctipoComparer : IComparer<CATALOGO_COSTOS_TIPO>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public CctipoComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(CATALOGO_COSTOS_TIPO Student1, CATALOGO_COSTOS_TIPO Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "CCT_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CCT_CODIGO.CompareTo(Student2.CCT_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.CCT_CODIGO.CompareTo(Student1.CCT_CODIGO);
                        }

                        break;
                    case "CCT_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CCT_NOMBRE.CompareTo(Student2.CCT_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.CCT_NOMBRE.CompareTo(Student1.CCT_NOMBRE);
                        }
                        break;
                    //case "BAN_ESTADO":
                    //    if (sortOrder == SortOrder.Ascending)
                    //    {
                    //        returnValue = Student1.BAN_ESTADO.CompareTo(Student2.BAN_ESTADO);
                    //    }
                    //    else
                    //    {
                    //        returnValue = Student2.BAN_ESTADO.CompareTo(Student1.BAN_ESTADO);
                    //    }
                    //    break;


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
        private void grpDatosPrincipales_Enter(object sender, EventArgs e)
        {

        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }
    }
}
