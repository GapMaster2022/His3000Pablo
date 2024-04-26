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
    public partial class frmBancos : Form
    {

        #region Variables
        public BANCOS bancoOriginal = new BANCOS();
        public BANCOS bancoModificada = new BANCOS();
        public List<BANCOS> bancos = new List<BANCOS>();
        public int columnabuscada;
        #endregion

        public frmBancos()
        {
            InitializeComponent();
        }






        #region Metodos Privados
        private void AgregarBindigControles()
        {
            Binding BAN_CODIGO = new Binding("Text", bancoModificada, "BAN_CODIGO", true);
            Binding BAN_NOMBRE = new Binding("Text", bancoModificada, "BAN_NOMBRE", true);
            Binding BAN_CUENTA_CONTABLE = new Binding("Text", bancoModificada, "BAN_CUENTA_CONTABLE", true);
            Binding BAN_ESTADO = new Binding("Checked", bancoModificada, "BAN_ESTADO", true);

            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_cuentacontable.DataBindings.Clear();
            chk_activo.DataBindings.Clear();
            txt_codigo.DataBindings.Add(BAN_CODIGO);
            txt_nombre.DataBindings.Add(BAN_NOMBRE);
            txt_cuentacontable.DataBindings.Add(BAN_CUENTA_CONTABLE);
            chk_activo.DataBindings.Add(BAN_ESTADO);

        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            bancoModificada = new BANCOS();
            bancoOriginal = new BANCOS();

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
            if (bancoModificada.BAN_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (bancoModificada.BAN_NOMBRE == null || bancoModificada.BAN_NOMBRE == string.Empty)
            {
                AgregarError(txt_nombre);
                valido = false;
            }

            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridbancos.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (bancoModificada.EntityKey == null)
                    {
                        NegBancos.CrearBanco(bancoModificada);

                    }
                    else
                    {
                        NegBancos.GrabarBanco(bancoModificada, bancoOriginal);

                    }

                    RecuperaBancos();
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
        private void RecuperaBancos()
        {
            bancos = NegBancos.RecuperaBancos();
            gridbancos.DataSource = bancos;
            gridbancos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridbancos.Columns["BAN_CODIGO"].HeaderText = "CODIGO";
            gridbancos.Columns["BAN_NOMBRE"].HeaderText = "NOMBRE";
            gridbancos.Columns["BAN_ESTADO"].HeaderText = "ESTADO";
            gridbancos.Columns["MEDICOS"].Visible = false;
            gridbancos.Columns["BAN_CUENTA_CONTABLE"].Visible = false;

        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridbancos.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridbancos.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridbancos.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridbancos.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridbancos.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class BancosComparer : IComparer<BANCOS>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public BancosComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(BANCOS Student1, BANCOS Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "BAN_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.BAN_CODIGO.CompareTo(Student2.BAN_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.BAN_CODIGO.CompareTo(Student1.BAN_CODIGO);
                        }

                        break;
                    case "BAN_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.BAN_NOMBRE.CompareTo(Student2.BAN_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.BAN_NOMBRE.CompareTo(Student1.BAN_NOMBRE);
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

        private void frmBancos_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaBancos();
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
                bancoModificada = gridbancos.CurrentRow.DataBoundItem as BANCOS;
                bancoOriginal = bancoModificada.ClonarEntidad();
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
                    NegBancos.EliminarBanco(bancoModificada.ClonarEntidad());
                    RecuperaBancos();
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
            RecuperaBancos();
            HalitarControles(false, false, false, false, false, true, false);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false, true);
                bancoOriginal = new BANCOS();
                bancoModificada = new BANCOS();

                ResetearControles();
                bancoModificada.BAN_CODIGO = Int16.Parse((NegBancos.RecuperaMaximoBanco() + 1).ToString());
                bancoModificada.BAN_ESTADO = true;
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

        private void gridbancos_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridbancos.Columns[e.ColumnIndex].Name;


                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                bancos.Sort(new BancosComparer(strColumnName, strSortOrder));
                gridbancos.DataSource = null;
                gridbancos.DataSource = bancos;
                gridbancos.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridbancos.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridbancos.Columns["BAN_CODIGO"].HeaderText = "CODIGO";
                gridbancos.Columns["BAN_NOMBRE"].HeaderText = "NOMBRE";
                gridbancos.Columns["BAN_ESTADO"].HeaderText = "ESTADO";
                gridbancos.Columns["MEDICOS"].Visible = false;
                gridbancos.Columns["BAN_CUENTA_CONTABLE"].Visible = false;

            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridbancos, new Point(MousePosition.X - this.Left, e.Y));

            }
        }

        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Usuarios", gridbancos.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridbancos.RowCount - 1; i++)
                    {
                        if (gridbancos.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridbancos.CurrentCell = gridbancos.Rows[i].Cells[columnabuscada];
                        //gridUsuarios.Rows[i].Cells[columnabuscada].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperaBancos();
            HalitarControles(false, false, false, false, false, true, false);
        }
    }
}
