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
    public partial class frm_OpcionesAcceso : Form
    {
        #region variables
        public DtoAccesoOpciones accOpcinesOriginal = new DtoAccesoOpciones();
        public DtoAccesoOpciones accOpcinesModificada = new DtoAccesoOpciones();
        public ACCESO_OPCIONES aoOrigen = new ACCESO_OPCIONES();
        public ACCESO_OPCIONES aoModificada = new ACCESO_OPCIONES();
        public List<MODULO> modulo = new List<MODULO>();
        public List<DtoAccesoOpciones> accesoOp = new List<DtoAccesoOpciones>();
        private Boolean cargadatoscombo;
        public int columnabuscada;
        #endregion

        #region Constructor
        public frm_OpcionesAcceso()
        {
            InitializeComponent();
        }
        #endregion

        #region eventos
        private void frm_OpcionesAcceso_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true,false);
                RecuperaAccesoOpciones();
                // carga combo MODULO
                cargadatoscombo = false;
                modulo = NegModulo.RecuperaModulos();
                cmb_modulo.DataSource = modulo;
                cmb_modulo.ValueMember = "ID_MODULO";
                cmb_modulo.DisplayMember = "DESCRIPCION";
                

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
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void gridOpacceso_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false,true);
                accOpcinesModificada = gridOpacceso.CurrentRow.DataBoundItem as DtoAccesoOpciones;
                accOpcinesOriginal = accOpcinesModificada.ClonarEntidad();
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
                    aoModificada.ID_ACCESO = accOpcinesModificada.ID_ACCESO;
                    aoModificada.DESCRIPCION = accOpcinesModificada.DESCRIPCION;
                    MODULO mod = cmb_modulo.SelectedItem as MODULO;
                    aoModificada.MODULOReference.EntityKey = mod.EntityKey;
                    aoModificada.EntityKey = new EntityKey(accesoOp.First().ENTITYSETNAME
                            , accesoOp.First().ENTITYID, accOpcinesModificada.ID_ACCESO);

                    NegAccesoOpciones.EliminarAccesoOpciones(aoModificada.ClonarEntidad());
                    RecuperaAccesoOpciones();
                    ResetearControles();
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
        private void txt_codigomodulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void txt_descripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");
            }
        }
        private void cmb_modulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false,true);
                accOpcinesOriginal = new DtoAccesoOpciones();
                accOpcinesModificada = new DtoAccesoOpciones();
                ResetearControles();
                
                AgregarBindigControles();
                cargadatoscombo = true;
                cmb_modulo.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperaAccesoOpciones();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void cmb_modulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int signumero;

            try
            {
                if (cargadatoscombo == true)
                {
                    string valmaximo = NegAccesoOpciones.RecuperaMaximoAccesoOpciones(Int16.Parse(cmb_modulo.SelectedValue.ToString())).ToString();
                    if (valmaximo != "0")
                        if(valmaximo.Length==1)
                            signumero=Int16.Parse( valmaximo)+1;
                        else
                        signumero = Int16.Parse(valmaximo.Substring(valmaximo.Length - 5, 5)) + 1;
                    else
                        signumero = 1;
                    if (valmaximo.Length == 1)
                        accOpcinesModificada.ID_ACCESO = signumero;
                    else
                        accOpcinesModificada.ID_ACCESO = int.Parse(string.Format("{0}{1:00000}", cmb_modulo.SelectedValue.ToString(), signumero));
                    //txt_codigomodulo.Text = string.Format("{0}{1:00000}", cmb_modulo.SelectedValue.ToString(), signumero);
                    txt_descripcion.Focus();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void gridOpacceso_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridOpacceso.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                accesoOp.Sort(new AccesoOpcionesComparer(strColumnName, strSortOrder));
                gridOpacceso.DataSource = null;
                gridOpacceso.DataSource = accesoOp;
                gridOpacceso.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridOpacceso.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridOpacceso.Columns["ID_ACCESO"].HeaderText = "CODIGO";
                gridOpacceso.Columns["DESCRIPCION"].HeaderText = "NOMBRE";
                gridOpacceso.Columns["DESCRIPCIONMod"].HeaderText = "MODULO";

                gridOpacceso.Columns["ID_MODULO"].Visible = false;
                gridOpacceso.Columns["ENTITYSETNAME"].Visible = false;
                gridOpacceso.Columns["ENTITYID"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridOpacceso, new Point(MousePosition.X - e.X - gridOpacceso.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Departamento", gridOpacceso.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridOpacceso.RowCount - 1; i++)
                    {
                        if (gridOpacceso.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridOpacceso.CurrentCell = gridOpacceso.Rows[i].Cells[columnabuscada];
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

        #region metodos privados
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        {
            btnNuevo.Enabled = Nuevo;
            btnActualizar.Enabled = Modificar;
            btnEliminar.Enabled = Eliminar;
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;

        }
        private void RecuperaAccesoOpciones()
        {
            accesoOp =NegAccesoOpciones.RecuperaAccesoOpciones();
            gridOpacceso.DataSource = accesoOp;
            gridOpacceso.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridOpacceso.Columns["ID_ACCESO"].HeaderText = "CODIGO";
            gridOpacceso.Columns["DESCRIPCION"].HeaderText = "NOMBRE";
            gridOpacceso.Columns["DESCRIPCIONMod"].HeaderText = "MODULO";

            gridOpacceso.Columns["ID_MODULO"].Visible = false;
            gridOpacceso.Columns["ENTITYSETNAME"].Visible = false;
            gridOpacceso.Columns["ENTITYID"].Visible = false;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridOpacceso.Focus();
            aoModificada = new ACCESO_OPCIONES();
            aoOrigen = new ACCESO_OPCIONES();
            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    aoModificada.ID_ACCESO = accOpcinesModificada.ID_ACCESO;
                    aoModificada.DESCRIPCION = accOpcinesModificada.DESCRIPCION;
                    MODULO mod = cmb_modulo.SelectedItem as MODULO;
                    aoModificada.MODULOReference.EntityKey = mod.EntityKey;
                    //Caso1
                    //string entitySetName = Constantes.NOMBREMODELO + ".ACCESO_OPCIONES";
                    //depModificada.EMPRESAReference.EntityKey = new EntityKey(entitySetName, "EMP_CODIGO", departamentoModificada.EMP_CODIGO);
                    //Caso2
                    
                    if (accOpcinesOriginal.ID_ACCESO == 0)
                    {
                        NegAccesoOpciones.CrearAccesoOpciones(aoModificada);
                    }
                    else
                    {
                        aoModificada.EntityKey = new EntityKey(accesoOp.First().ENTITYSETNAME
                            , accesoOp.First().ENTITYID, accOpcinesModificada.ID_ACCESO);

                        aoOrigen.ID_ACCESO = accOpcinesOriginal.ID_ACCESO;
                        aoOrigen.DESCRIPCION = accOpcinesOriginal.DESCRIPCION;
                        MODULO modOriginal = modulo.Where(emp => emp.ID_MODULO == accOpcinesOriginal.ID_MODULO).FirstOrDefault();
                        aoOrigen.MODULOReference.EntityKey = modOriginal.EntityKey;
                        aoOrigen.EntityKey = new EntityKey(accesoOp.First().ENTITYSETNAME
                            , accesoOp.First().ENTITYID, accOpcinesOriginal.ID_ACCESO);

                        NegAccesoOpciones.GrabarAccesoOpciones(aoModificada, aoOrigen);
                    }

                    RecuperaAccesoOpciones();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (accOpcinesModificada.ID_ACCESO == 0)
            {
                AgregarError(txt_codigomodulo);
                valido = false;
            }
            if (accOpcinesModificada.DESCRIPCION == null || accOpcinesModificada.DESCRIPCION == string.Empty)
            {
                AgregarError(txt_descripcion);
                valido = false;
            }
            return valido;

        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private void ResetearControles()
        {
            accOpcinesModificada = new DtoAccesoOpciones();
            accOpcinesOriginal = new DtoAccesoOpciones();
            aoOrigen = new ACCESO_OPCIONES();
            aoModificada = new ACCESO_OPCIONES();

            aoOrigen = new ACCESO_OPCIONES();
            aoModificada= new ACCESO_OPCIONES();
            txt_codigomodulo.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            cargadatoscombo = false;
            
        }
        private void AgregarBindigControles()
        {
            Binding ID_ACCESO = new Binding("Text", accOpcinesModificada, "ID_ACCESO", true);
            Binding DESCRIPCION = new Binding("Text", accOpcinesModificada, "DESCRIPCION", true);
            Binding ID_MODULO = new Binding("SelectedValue", accOpcinesModificada, "ID_MODULO", true);
            

            txt_codigomodulo.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();
            cmb_modulo.DataBindings.Clear();


            txt_codigomodulo.DataBindings.Add(ID_ACCESO);
            txt_descripcion.DataBindings.Add(DESCRIPCION);
            cmb_modulo.DataBindings.Add(ID_MODULO);
            

        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridOpacceso.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridOpacceso.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridOpacceso.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridOpacceso.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridOpacceso.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class AccesoOpcionesComparer : IComparer<DtoAccesoOpciones>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public AccesoOpcionesComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoAccesoOpciones Student1, DtoAccesoOpciones Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "ID_ACCESO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ID_ACCESO.CompareTo(Student2.ID_ACCESO);
                        }
                        else
                        {
                            returnValue = Student2.ID_ACCESO.CompareTo(Student1.ID_ACCESO);
                        }

                        break;
                    case "DESCRIPCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.DESCRIPCION.CompareTo(Student2.DESCRIPCION);
                        }
                        else
                        {
                            returnValue = Student2.DESCRIPCION.CompareTo(Student1.DESCRIPCION);
                        }
                        break;
                    case "DESCRIPCIONMod":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.DESCRIPCIONMod.CompareTo(Student2.DESCRIPCIONMod);
                        }
                        else
                        {
                            returnValue = Student2.DESCRIPCIONMod.CompareTo(Student1.DESCRIPCIONMod);
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
