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
using His.General;


namespace His.Maintenance
{
    public partial class frm_CategoriasConvenios : Form
    {
        #region Variables
        public CATEGORIAS_CONVENIOS conveniosOriginal = new CATEGORIAS_CONVENIOS();
        public CATEGORIAS_CONVENIOS conveniosModificada = new CATEGORIAS_CONVENIOS();
        public List<CATEGORIAS_CONVENIOS> convenios = new List<CATEGORIAS_CONVENIOS>();
        private List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        public int columnabuscada;
        private bool nuevo;
        #endregion

        #region Constructores
        public frm_CategoriasConvenios()
        {

            InitializeComponent();
            inicializarControles();
        }

        #endregion

        #region Eventos
        private void inicializarControles()
        {
            aseguradoras = NegAseguradoras.ListaEmpresas();
            CmbAseguradoras.DataSource = aseguradoras;
            CmbAseguradoras.ValueMember = "ASE_CODIGO";
            CmbAseguradoras.DisplayMember = "ASE_NOMBRE";
            RecuperaConvenios();

        }
        private void frm_CategoriasConvenios_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaConvenios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();
                nuevo = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void gridConvenios_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                //ccostosModificada = gridCcostos.CurrentRow.DataBoundItem as CATALOGO_COSTOS;
                conveniosModificada.CAT_CODIGO = (Int16)gridConvenios.CurrentRow.Cells[0].Value;
                conveniosModificada.CAT_NOMBRE = gridConvenios.CurrentRow.Cells[1].Value.ToString();
                conveniosModificada.CAT_DESCRIPCION = gridConvenios.CurrentRow.Cells[2].Value.ToString();

                conveniosModificada.CAT_FECHA_INICIO = Convert.ToDateTime(gridConvenios.CurrentRow.Cells[5].Value);
                conveniosModificada.CAT_FECHA_FIN = Convert.ToDateTime(gridConvenios.CurrentRow.Cells[6].Value);

                //conveniosModificada.CAT_FECHA_FIN = gridConvenios.CurrentRow.Cells[5].Value.ToString();     

                int codAseguradora = (Int16)gridConvenios.CurrentRow.Cells[3].Value;
                CmbAseguradoras.SelectedItem = aseguradoras.FirstOrDefault(c => c.ASE_CODIGO == codAseguradora);
                //conveniosOriginal = conveniosModificada.ClonarEntidad();
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
                ASEGURADORAS_EMPRESAS aseguradoras = (ASEGURADORAS_EMPRESAS)CmbAseguradoras.SelectedItem;
                DialogResult resultado;

                resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    NegCategorias.EliminarCategorias(conveniosModificada.ClonarEntidad());
                    if (NegCategorias.ListaCategorias(aseguradoras.ASE_CODIGO).Count < 1)
                        NegAseguradoras.actualizarConvenio(aseguradoras, false);
                    RecuperaConvenios();
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
            RecuperaConvenios();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                nuevo = true;
                HalitarControles(true, true, false, true, false, false, true);
                conveniosOriginal = new CATEGORIAS_CONVENIOS();
                conveniosModificada = new CATEGORIAS_CONVENIOS();

                ResetearControles();
                conveniosModificada.CAT_CODIGO = Int16.Parse((NegCategorias.RecuperaMaximoCategorias() + 1).ToString());
                AgregarBindigControles();
                txt_nombre.Focus();
                txt_descripcion.Focus();
                dtp_Finicio.Focus();
                dtp_Ffin.Focus();

                //nuevo = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txt_descripcion.Focus();
                //CmbAseguradoras.Focus();  
            }
        }
        private void gridConvenios_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (gridConvenios.Columns[e.ColumnIndex].Tag == null)
                    {
                        gridConvenios.Columns[e.ColumnIndex].Tag = SortOrder.Ascending;
                    }
                    else if (gridConvenios.Columns[e.ColumnIndex].Tag.ToString() == SortOrder.Ascending.ToString())
                    {
                        gridConvenios.Columns[e.ColumnIndex].Tag = SortOrder.Descending;
                    }
                    else if (gridConvenios.Columns[e.ColumnIndex].Tag.ToString() == SortOrder.Descending.ToString())
                    {
                        gridConvenios.Columns[e.ColumnIndex].Tag = SortOrder.Ascending;
                    }
                    string columna = gridConvenios.Columns[e.ColumnIndex].DataPropertyName + " " + gridConvenios.Columns[e.ColumnIndex].Tag;
                    var cat_conv = convenios.Select(c => new { c.CAT_CODIGO, c.CAT_NOMBRE, c.CAT_DESCRIPCION, c.ASEGURADORAS_EMPRESAS.ASE_CODIGO, c.ASEGURADORAS_EMPRESAS.ASE_NOMBRE, c.CAT_FECHA_INICIO, c.CAT_FECHA_FIN }).ToList();
                    cat_conv = cat_conv.OrdenarPorPropiedad(columna).ToList();
                    gridConvenios.DataSource = cat_conv;
                    //gridConvenios.DataSource = cat_conv.Select(c => new { c.CAT_CODIGO, c.CAT_NOMBRE, c.CAT_DESCRIPCION, c.ASEGURADORAS_EMPRESAS.ASE_CODIGO, c.ASEGURADORAS_EMPRESAS.ASE_NOMBRE, c.CAT_FECHA_INICIO, c.CAT_FECHA_FIN }).ToList();
                    
                }
                else if (e.Button == MouseButtons.Right)
                {
                    columnabuscada = e.ColumnIndex;
                    mnuContexsecundario.Show(gridConvenios, new Point(MousePosition.X - e.X - gridConvenios.Width, e.Y));
                }
            
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
                                                                                    
            
            
           
        }

        private void MmanuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Usuarios", gridConvenios.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridConvenios.RowCount - 1; i++)
                    {
                        if (gridConvenios.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridConvenios.CurrentCell = gridConvenios.Rows[i].Cells[columnabuscada];
                        //gridUsuarios.Rows[i].Cells[columnabuscada].Selected = true;
                    }
                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
            }
        }

        #endregion
        #region Metodos Privados
        private void AgregarBindigControles()
        {
            Binding CAT_CODIGO = new Binding("Text", conveniosModificada, "CAT_CODIGO", true);
            Binding CAT_NOMBRE = new Binding("Text", conveniosModificada, "CAT_NOMBRE", true);
            Binding CAT_DESCRIPCION = new Binding("Text", conveniosModificada, "CAT_DESCRIPCION", true);
            Binding CAT_FECHA_INICIO = new Binding("Text", conveniosModificada, "CAT_FECHA_INICIO", true);
            Binding CAT_FECHA_FIN = new Binding("Text", conveniosModificada, "CAT_FECHA_FIN", true);

            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();
            dtp_Finicio.DataBindings.Clear();
            dtp_Ffin.DataBindings.Clear();

            txt_codigo.DataBindings.Add(CAT_CODIGO);
            txt_nombre.DataBindings.Add(CAT_NOMBRE);
            txt_descripcion.DataBindings.Add(CAT_DESCRIPCION);
            dtp_Finicio.DataBindings.Add(CAT_FECHA_INICIO);
            dtp_Ffin.DataBindings.Add(CAT_FECHA_FIN);
            //  txt_nombre.DataBindings.Add(CCT_NOMBRE);


        }
        private void ResetearControles()
        {
            conveniosModificada = new CATEGORIAS_CONVENIOS();
            conveniosOriginal = new CATEGORIAS_CONVENIOS();

            txt_codigo.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            dtp_Finicio.Text = string.Empty;
            //txt_Ffin.Text = string.Empty;

        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (conveniosModificada.CAT_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (conveniosModificada.CAT_NOMBRE == null || conveniosModificada.CAT_NOMBRE == string.Empty
                || conveniosModificada.CAT_DESCRIPCION == string.Empty)
            {
                AgregarError(txt_nombre);
                valido = false;
            }

            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridConvenios.Focus();
            ASEGURADORAS_EMPRESAS aseguradoras = (ASEGURADORAS_EMPRESAS)CmbAseguradoras.SelectedItem;
            conveniosModificada.CAT_CODIGO = Convert.ToInt16(txt_codigo.Text.ToString());
            conveniosModificada.CAT_NOMBRE = txt_nombre.Text;
            conveniosModificada.CAT_DESCRIPCION = txt_descripcion.Text;
            conveniosModificada.ASEGURADORAS_EMPRESASReference.EntityKey = aseguradoras.EntityKey;
            conveniosModificada.CAT_FECHA_INICIO = dtp_Finicio.Value;
            conveniosModificada.CAT_FECHA_FIN = dtp_Ffin.Value;

            //if (ValidarFormulario())
            //{
            resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                if (nuevo == true)
                {
                    NegCategorias.CrearCategoria(conveniosModificada);
                    NegAseguradoras.actualizarConvenio(aseguradoras, true);
                }
                else
                {
                    NegCategorias.GrabarCategorias(conveniosModificada, conveniosOriginal);
                }

                RecuperaConvenios();
                ResetearControles();
                HalitarControles(false, false, false, false, false, true, false);
                MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}
        }
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;

        }
        private void RecuperaConvenios()
        {
            //convenios = NegCategorias.RecuperaCategorias();
            convenios = NegCategorias.RecuperaCategoriasVigente(chkVigente.Checked);
            //gridConvenios.DataSource = convenios;
            var listaConvenios = convenios.Select(c => new { c.CAT_CODIGO, c.CAT_NOMBRE, c.CAT_DESCRIPCION, c.ASEGURADORAS_EMPRESAS.ASE_CODIGO, c.ASEGURADORAS_EMPRESAS.ASE_NOMBRE, c.CAT_FECHA_INICIO, c.CAT_FECHA_FIN }).ToList();
            gridConvenios.DataSource = listaConvenios;
            gridConvenios.Columns["CAT_FECHA_INICIO"].Visible = true;
            gridConvenios.Columns["CAT_FECHA_FIN"].Visible = true;
            
            gridConvenios.Columns["CAT_CODIGO"].HeaderText = "CODIGO";
            gridConvenios.Columns["CAT_NOMBRE"].HeaderText = "NOMBRE CONVENIO";
            gridConvenios.Columns["CAT_DESCRIPCION"].HeaderText = "DESCRIPCION";   
            gridConvenios.Columns["ASE_NOMBRE"].HeaderText = "ASEGURADORA";
            gridConvenios.Columns["ASE_CODIGO"].Visible = false;
            gridConvenios.Columns["CAT_FECHA_INICIO"].HeaderText = "FECHA INICIO";
            gridConvenios.Columns["CAT_FECHA_FIN"].HeaderText = "FECHA FIN";
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridConvenios.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridConvenios.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridConvenios.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridConvenios.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridConvenios.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class ConveniosComparer : IComparer<CATEGORIAS_CONVENIOS>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public ConveniosComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(CATEGORIAS_CONVENIOS Student1, CATEGORIAS_CONVENIOS Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "CAT_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CAT_CODIGO.CompareTo(Student2.CAT_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.CAT_CODIGO.CompareTo(Student1.CAT_CODIGO);
                        }

                        break;
                    case "CAT_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CAT_NOMBRE.CompareTo(Student2.CAT_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.CAT_NOMBRE.CompareTo(Student1.CAT_NOMBRE);
                        }
                        break;
                    case "CAT_DESCRIPCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CAT_DESCRIPCION.CompareTo(Student2.CAT_DESCRIPCION);
                        }
                        else
                        {
                            returnValue = Student2.CAT_DESCRIPCION.CompareTo(Student1.CAT_DESCRIPCION);
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

        private void gridConvenios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grpDatosPrincipales_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RecuperaConvenios();
        }
    }
}