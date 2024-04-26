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
    public partial class frm_Departamentos : Form
    {
        #region variable
        public DtoDepartamentos departamentoOriginal = new DtoDepartamentos();
        public DtoDepartamentos departamentoModificada = new DtoDepartamentos();
        public DEPARTAMENTOS depOrigen = new DEPARTAMENTOS();
        public DEPARTAMENTOS depModificada = new DEPARTAMENTOS();
        private int temp;
        private int fila;
        private DataSet mDatos;
        private string pathlogo;
        private bool modificaDatos;
        List<EMPRESA> empresa = new List<EMPRESA>();
        List<DtoDepartamentos> departamentopadre = new List<DtoDepartamentos>();
        List<DtoDepartamentos> departamentos = new List<DtoDepartamentos>();
        public int columnabuscada;
        #endregion

        #region constructores
        public frm_Departamentos()
        {
            InitializeComponent();
            
        }
        #endregion

        #region eventos
        private void frm_Departamentos_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, true, true,false, true);
                RecuperarDepartamentos();
                // carga combo EMPRESA
                empresa = NegEmpresa.RecuperaEmpresas();
                cmb_empresa.DataSource = empresa;
                cmb_empresa.ValueMember = "EMP_CODIGO";
                cmb_empresa.DisplayMember = "EMP_NOMBRE";
                //carga combo  DEPARTEMENTO PADRE
                departamentopadre = NegDepartamentos.RecuperaDepartamentos();
                cmb_deppadre.DataSource = departamentopadre;
                cmb_deppadre.ValueMember = "DEP_CODIGO";
                cmb_deppadre.DisplayMember = "DEP_NOMBRE";

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
        private void gridDepartamentos_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false,true, true);
                departamentoModificada = gridDepartamentos.CurrentRow.DataBoundItem as DtoDepartamentos;
                departamentoOriginal = departamentoModificada.ClonarEntidad();
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
                    depModificada.DEP_CODIGO = departamentoModificada.DEP_CODIGO;
                    depModificada.DEP_NOMBRE = departamentoModificada.DEP_NOMBRE;
                    depModificada.DEP_PADRE = departamentoModificada.DEP_PADRE;
                    EMPRESA empresaModificada = cmb_empresa.SelectedItem as EMPRESA;
                    depModificada.EMPRESAReference.EntityKey = empresaModificada.EntityKey;
                    depModificada.DEP_ESTADO = departamentoModificada.DEP_ESTADO;
                    depModificada.EntityKey = new EntityKey(departamentopadre.First().ENTITYSETNAME
                        , departamentopadre.First().ENTITYID, departamentoModificada.DEP_CODIGO);

                    NegDepartamentos.EliminarDepartamento(depModificada.ClonarEntidad());
                    RecuperarDepartamentos();
                    ResetearControles();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false, true);
                    MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false,true, true);
                departamentoOriginal = new DtoDepartamentos();
                departamentoModificada = new DtoDepartamentos();

                ResetearControles();
                departamentoModificada.DEP_CODIGO = Int16.Parse((NegDepartamentos.RecuperaMaximoDepartamento() + 1).ToString());
                departamentoModificada.DEP_ESTADO = true;
                AgregarBindigControles();
                txt_depnombre.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperarDepartamentos();
            HalitarControles(false, false, false, false, false, true, false, true);
        }
        private void txt_depnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cmb_empresa.Focus();
            }
        }
        private void cmb_empresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cmb_deppadre.Focus();
            }
        }
        private void cmb_deppadre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void chk_depeliminado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void gridDepartamentos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridDepartamentos.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                departamentos.Sort(new DepartamentoComparer(strColumnName, strSortOrder));
                gridDepartamentos.DataSource = null;
                gridDepartamentos.DataSource = departamentos;
                gridDepartamentos.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridDepartamentos.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridDepartamentos.Columns["DEP_CODIGO"].HeaderText = "CODIGO";
                gridDepartamentos.Columns["DEP_NOMBRE"].HeaderText = "NOMBRE";
                gridDepartamentos.Columns["EMP_NOMBRE"].HeaderText = "EMPRESA";
                //Desahbilitamos las demas columnas
                gridDepartamentos.Columns["EMP_CODIGO"].Visible = false;
                gridDepartamentos.Columns["DEP_PADRE"].Visible = false;
                gridDepartamentos.Columns["ENTITYSETNAME"].Visible = false;
                gridDepartamentos.Columns["ENTITYID"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridDepartamentos, new Point(MousePosition.X - e.X  - gridDepartamentos.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Departamento", gridDepartamentos.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridDepartamentos.RowCount - 1; i++)
                    {
                        if (gridDepartamentos.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridDepartamentos.CurrentCell = gridDepartamentos.Rows[i].Cells[columnabuscada];
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
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (departamentoModificada.DEP_CODIGO == 0)
            {
                AgregarError(txt_depcodigo);
                valido = false;
            }
            if (departamentoModificada.DEP_NOMBRE == null || departamentoModificada.DEP_NOMBRE == string.Empty)
            {
                AgregarError(txt_depnombre);
                valido = false;
            }
            if (departamentoModificada.EMP_CODIGO <= 0)
            {
                AgregarError(cmb_empresa);
                valido = false;
            }            
            return valido;

        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        /// <summary>
        /// funcion para habilitar los botones
        /// </summary>
        /// <param name="control">boton que se acciona</param>
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar, bool Cerrar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            btnCerrar.Enabled = Cerrar;
            grpDatosPrincipales.Enabled = datosPrincipales;
            
        }
        private void RecuperarDepartamentos()
        {
            departamentos = NegDepartamentos.RecuperaDepartamentos();
            gridDepartamentos.DataSource = departamentos;
            gridDepartamentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridDepartamentos.Columns["DEP_CODIGO"].HeaderText = "CODIGO";
            gridDepartamentos.Columns["DEP_NOMBRE"].HeaderText = "NOMBRE";
            gridDepartamentos.Columns["EMP_NOMBRE"].HeaderText = "EMPRESA";
            //Desahbilitamos las demas columnas
            gridDepartamentos.Columns["EMP_CODIGO"].Visible = false;
            gridDepartamentos.Columns["DEP_PADRE"].Visible = false;
            gridDepartamentos.Columns["ENTITYSETNAME"].Visible = false;
            gridDepartamentos.Columns["ENTITYID"].Visible = false;
        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridDepartamentos.Focus();
            depModificada = new DEPARTAMENTOS();
            depOrigen = new DEPARTAMENTOS();
            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {                   
                    //Caso1
                    //string entitySetName = Constantes.NOMBREMODELO + ".EMPRESA";
                    //depModificada.EMPRESAReference.EntityKey = new EntityKey(entitySetName, "EMP_CODIGO", departamentoModificada.EMP_CODIGO);
                    //Caso2
                    
                    depModificada.DEP_CODIGO = departamentoModificada.DEP_CODIGO;
                    depModificada.DEP_NOMBRE = departamentoModificada.DEP_NOMBRE;
                    depModificada.DEP_PADRE = departamentoModificada.DEP_PADRE;
                    depModificada.DEP_ESTADO = departamentoModificada.DEP_ESTADO;
                    EMPRESA empresaModificada = cmb_empresa.SelectedItem as EMPRESA;
                    depModificada.EMPRESAReference.EntityKey = empresaModificada.EntityKey;

                    if (departamentoOriginal.DEP_CODIGO == 0)
                    {
                        
                        NegDepartamentos.CrearDepartamento(depModificada);
                    }
                    else
                    {
                        
                        depModificada.EntityKey = new EntityKey(departamentopadre.First().ENTITYSETNAME
                            , departamentopadre.First().ENTITYID, departamentoModificada.DEP_CODIGO);
                        depOrigen.DEP_CODIGO = departamentoOriginal.DEP_CODIGO;
                        depOrigen.DEP_NOMBRE = departamentoOriginal.DEP_NOMBRE;
                        depOrigen.DEP_PADRE = departamentoOriginal.DEP_PADRE;
                        depOrigen.EntityKey = new EntityKey(departamentopadre.First().ENTITYSETNAME
                          , departamentopadre.First().ENTITYID, departamentoOriginal.DEP_CODIGO);
                        EMPRESA empresaOriginal = empresa.Where(emp => emp.EMP_CODIGO == departamentoOriginal.EMP_CODIGO).FirstOrDefault();
                        depOrigen.EMPRESAReference.EntityKey = empresaOriginal.EntityKey;
                        depOrigen.DEP_ESTADO = departamentoOriginal.DEP_ESTADO;

                        NegDepartamentos.GrabarDepartamento(depModificada, depOrigen);
                    }

                    RecuperarDepartamentos();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false, true);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            departamentoModificada = new DtoDepartamentos();
            departamentoOriginal = new DtoDepartamentos();
            txt_depcodigo.Text = string.Empty;
            txt_depnombre.Text = string.Empty;
            cmb_deppadre.SelectedItem = -1;
            chk_estado.DataBindings.Clear();
        }
        private void AgregarBindigControles()
        {
            Binding DEP_CODIGO = new Binding("Text", departamentoModificada, "DEP_CODIGO", true);
            Binding DEP_NOMBRE = new Binding("Text", departamentoModificada, "DEP_NOMBRE", true) ;
            Binding EMP_CODIGO = new Binding("SelectedValue", departamentoModificada, "EMP_CODIGO", true);
            //Binding EMP_NOMBRE = new Binding("Text", departamentoModificada, "EMP_NOMBRE", true);
            Binding DEP_PADRE = new Binding("SelectedValue", departamentoModificada, "DEP_PADRE", true);
            Binding DEP_ESTADO = new Binding("Checked", departamentoModificada, "DEP_ESTADO", true);


            txt_depcodigo.DataBindings.Clear();
            txt_depnombre.DataBindings.Clear();
            cmb_empresa.DataBindings.Clear();
            cmb_deppadre.DataBindings.Clear();
            chk_estado.DataBindings.Clear();


            txt_depcodigo.DataBindings.Add(DEP_CODIGO);
            txt_depnombre.DataBindings.Add(DEP_NOMBRE);
            cmb_empresa.DataBindings.Add(EMP_CODIGO);
            cmb_deppadre.DataBindings.Add(DEP_PADRE);
            chk_estado.DataBindings.Add(DEP_ESTADO);
            
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridDepartamentos.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridDepartamentos.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridDepartamentos.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridDepartamentos.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridDepartamentos.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class DepartamentoComparer : IComparer<DtoDepartamentos>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public DepartamentoComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoDepartamentos Student1, DtoDepartamentos Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "DEP_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.DEP_CODIGO.CompareTo(Student2.DEP_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.DEP_CODIGO.CompareTo(Student1.DEP_CODIGO);
                        }

                        break;
                    case "DEP_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.DEP_NOMBRE.CompareTo(Student2.DEP_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.DEP_NOMBRE.CompareTo(Student1.DEP_NOMBRE);
                        }
                        break;
                    case "EMP_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.EMP_NOMBRE.CompareTo(Student2.EMP_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.EMP_NOMBRE.CompareTo(Student1.EMP_NOMBRE);
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

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        








    }
}
