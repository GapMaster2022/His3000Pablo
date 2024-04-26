using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using Core.Datos;
using Core.Entidades;


namespace His.Maintenance
{
    public partial class frm_ZonaEdicion : Form
    {
        #region Variables
        public ZONAS zonaOriginal = new ZONAS();
        public ZONAS zonaModificada = new ZONAS();
        public List<DtoZonas> zonas = new List<DtoZonas>();
        public DtoZonas zonafrmOriginal = new DtoZonas();
        public DtoZonas zonafrmModificada = new DtoZonas();
        public List<EMPRESA> empresa = new List<EMPRESA>();
        public List<CIUDAD> ciudad = new List<CIUDAD>();
        public int columnabuscada;
        #endregion

        #region Constructor
        public frm_ZonaEdicion()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_ZonaEdicion_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true,false);
                RecuperaZonas();
                empresa = NegEmpresa.RecuperaEmpresas();
                cmb_empresa.DataSource = empresa;
                cmb_empresa.ValueMember = "EMP_CODIGO";
                cmb_empresa.DisplayMember = "EMP_NOMBRE";
                ciudad = NegCiudad.ListaCiudades();
                cmb_ciudad.DataSource = ciudad;
                cmb_ciudad.ValueMember = "CODCIUDAD";
                cmb_ciudad.DisplayMember = "NOMCIU";
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
        private void gridzonas_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false,true);
                zonafrmModificada = gridzonas.CurrentRow.DataBoundItem as DtoZonas;
                zonafrmOriginal = zonafrmModificada.ClonarEntidad();
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
                    zonafrmModificada.CODIGO = "1";
                    zonaModificada.CODZONA = zonafrmModificada.CODZONA;
                    zonaModificada.NOMZONA = zonafrmModificada.NOMZONA;
                    zonaModificada.CODIGO = zonafrmModificada.CODIGO;
                    CIUDAD ciudadModidificada = cmb_ciudad.SelectedItem as CIUDAD;
                    zonaModificada.CIUDADReference.EntityKey = ciudadModidificada.EntityKey;
                    EMPRESA empresaModificada = cmb_empresa.SelectedItem as EMPRESA;
                    zonaModificada.EMPRESAReference.EntityKey = empresaModificada.EntityKey;
                    zonaModificada.EntityKey = new EntityKey(zonas.First().ENTITYSETNAME
                            , zonas.First().ENTITYID, zonafrmModificada.CODZONA);

                    NegZonas.EliminarZona(zonaModificada.ClonarEntidad());
                    RecuperaZonas();
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
            RecuperaZonas();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {

            try
            {
                HalitarControles(true, true, false, true, false, false,true);
                zonaOriginal = new ZONAS();
                zonaModificada = new ZONAS();

                ResetearControles();
                zonafrmModificada.CODZONA = Int16.Parse((NegZonas.RecuperaMaximoZona() + 1).ToString());
                AgregarBindigControles();
                txt_descripcion.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_descripcion_KeyPress(object sender, KeyPressEventArgs e)
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
                cmb_ciudad.Focus();
            }
        }
        private void gridzonas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridzonas.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                zonas.Sort(new ZonasComparer(strColumnName, strSortOrder));
                gridzonas.DataSource = null;
                gridzonas.DataSource = zonas;
                gridzonas.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridzonas.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridzonas.Columns["CODZONA"].HeaderText = "CODIGO";
                gridzonas.Columns["NOMZONA"].HeaderText = "NOMBRE";

                //Desahbilitamos las demas columnas
                gridzonas.Columns["CODIGO"].Visible = false;
                gridzonas.Columns["EMP_CODIGO"].Visible = false;
                gridzonas.Columns["CODCIUDAD"].Visible = false;
                gridzonas.Columns["ENTITYSETNAME"].Visible = false;
                gridzonas.Columns["ENTITYID"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridzonas, new Point(MousePosition.X - e.X - gridzonas.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Departamento", gridzonas.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridzonas.RowCount - 1; i++)
                    {
                        if (gridzonas.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridzonas.CurrentCell = gridzonas.Rows[i].Cells[columnabuscada];

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
            Binding CODZONA = new Binding("Text", zonafrmModificada, "CODZONA", true);
            Binding NOMZONA = new Binding("Text", zonafrmModificada, "NOMZONA", true);
            Binding EMP_CODIGO = new Binding("SelectedValue", zonafrmModificada, "EMP_CODIGO", true);
            Binding CODCIUDAD = new Binding("SelectedValue", zonafrmModificada, "CODCIUDAD", true);


            txt_codigo.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();
            cmb_ciudad.DataBindings.Clear();
            cmb_empresa.DataBindings.Clear();

            txt_codigo.DataBindings.Add(CODZONA);
            txt_descripcion.DataBindings.Add(NOMZONA);
            cmb_ciudad.DataBindings.Add(CODCIUDAD);
            cmb_empresa.DataBindings.Add(EMP_CODIGO);
            
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
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridzonas.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    zonafrmModificada.CODIGO = "1";
                    zonaModificada.CODZONA = zonafrmModificada.CODZONA;
                    zonaModificada.NOMZONA = zonafrmModificada.NOMZONA;
                    zonaModificada.CODIGO = zonafrmModificada.CODIGO;
                    CIUDAD ciudadModidificada = cmb_ciudad.SelectedItem as CIUDAD;
                    zonaModificada.CIUDADReference.EntityKey = ciudadModidificada.EntityKey;
                    EMPRESA empresaModificada = cmb_empresa.SelectedItem as EMPRESA;
                    zonaModificada.EMPRESAReference.EntityKey = empresaModificada.EntityKey;
                    if (zonafrmOriginal.CODZONA == 0)
                    {

                        NegZonas.CrearZona(zonaModificada);

                    }
                    else
                    {
                        zonaModificada.EntityKey = new EntityKey(zonas.First().ENTITYSETNAME
                            , zonas.First().ENTITYID, zonafrmModificada.CODZONA);

                        zonaOriginal.CODZONA = zonafrmOriginal.CODZONA;
                        zonaOriginal.NOMZONA = zonafrmOriginal.NOMZONA;
                        zonaOriginal.CODIGO = zonafrmOriginal.CODIGO;
                        CIUDAD ciudadOriginal = ciudad.Where(cod => cod.CODCIUDAD == zonafrmOriginal.CODCIUDAD).FirstOrDefault();
                        zonaOriginal.CIUDADReference.EntityKey = ciudadOriginal.EntityKey;
                        EMPRESA empresaOriginal = empresa.Where(emp => emp.EMP_CODIGO == zonafrmOriginal.EMP_CODIGO).FirstOrDefault();
                        zonaOriginal.EMPRESAReference.EntityKey = empresaOriginal.EntityKey;
                        zonaOriginal.EntityKey = new EntityKey(zonas.First().ENTITYSETNAME
                          , zonas.First().ENTITYID, zonafrmOriginal.CODZONA);
                        
                        

                        NegZonas.GrabarZona(zonaModificada, zonaOriginal);

                    }

                    RecuperaZonas();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void RecuperaZonas()
        {
            zonas =  NegZonas.RecuperaZonasFormulario(); 
            gridzonas.DataSource = zonas;
            gridzonas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridzonas.Columns["CODZONA"].HeaderText = "CODIGO";
            gridzonas.Columns["NOMZONA"].HeaderText = "NOMBRE";
            
            //Desahbilitamos las demas columnas
            gridzonas.Columns["CODIGO"].Visible = false;
            gridzonas.Columns["EMP_CODIGO"].Visible = false;
            gridzonas.Columns["CODCIUDAD"].Visible = false;
            gridzonas.Columns["ENTITYSETNAME"].Visible = false;
            gridzonas.Columns["ENTITYID"].Visible = false;
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (zonafrmModificada.CODZONA == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (zonafrmModificada.NOMZONA == null || zonafrmModificada.NOMZONA == string.Empty)
            {
                AgregarError(txt_descripcion);
                valido = false;
            }
            if (zonafrmModificada.EMP_CODIGO == 0)
            {
                AgregarError(cmb_empresa);
                valido = false;
            }
            if (zonafrmModificada.CODCIUDAD == 0)
            {
                AgregarError(cmb_ciudad);
                valido = false;
            }
            
            return valido;

        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            zonaModificada = new ZONAS();
            zonaOriginal = new ZONAS();
            zonafrmOriginal = new DtoZonas();
            zonafrmModificada = new DtoZonas();

            txt_codigo.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            cmb_empresa.SelectedItem = -1;
            cmb_ciudad.SelectedItem = -1;
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridzonas.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridzonas.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridzonas.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridzonas.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridzonas.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class ZonasComparer : IComparer<DtoZonas>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public ZonasComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoZonas Student1, DtoZonas Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "CODZONA":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CODZONA.CompareTo(Student2.CODZONA);
                        }
                        else
                        {
                            returnValue = Student2.CODZONA.CompareTo(Student1.CODZONA);
                        }

                        break;
                    case "NOMZONA":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.NOMZONA.CompareTo(Student2.NOMZONA);
                        }
                        else
                        {
                            returnValue = Student2.NOMZONA.CompareTo(Student1.NOMZONA);
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
