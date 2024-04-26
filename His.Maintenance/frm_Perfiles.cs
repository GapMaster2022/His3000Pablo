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
using His.Entidades.Clases;

namespace His.Maintenance
{
    public partial class frm_Perfiles : Form
    {
        #region variables
        public PERFILES perfilOriginal = new PERFILES();
        public PERFILES perfilModificada = new PERFILES();
        List<PERFILES> perfil = new List<PERFILES>();
        List<DtoAccesosPorPerfil> dsDatos = new List<DtoAccesosPorPerfil>();
        private Boolean cargadatoscombo;
        public int columnabuscada;
        #endregion

        #region Constructores
        public frm_Perfiles()
        {
            InitializeComponent();
        }
        #endregion

        #region eventos
        private void frm_Perfiles_Load(object sender, EventArgs e)
        {
            try
            {
                
                HalitarControles(false, false, false, false, false, true,false);
                habilitarTab(false);
                RecuperaPerfiles();
                cargadatoscombo = false;
                var modulo = NegModulo.RecuperaModulos();
                cmb_modulo.DataSource = modulo;
                cmb_modulo.ValueMember = "id_modulo";
                cmb_modulo.DisplayMember = "descripcion";
                cargadatoscombo = true;
                cmb_modulo.Text = "";
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
        private void gridperfiles_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false,true);
                perfilModificada = gridperfiles.CurrentRow.DataBoundItem as PERFILES;
                perfilOriginal = perfilModificada.ClonarEntidad();
                AgregarBindigControles();
                habilitarTab(true);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false,true);
                perfilOriginal = new PERFILES();
                perfilModificada = new PERFILES();

                ResetearControles();
                perfilModificada.ID_PERFIL = Int16.Parse((NegPerfil.RecuperaMaximoPerfil() + 1).ToString());
                AgregarBindigControles();
                cargadatoscombo = true;
                txt_descripcion.Focus();
                grid_accesosn.DataSource = null;
                grid_accesosn.Columns.Clear();
                grid_accesosn.Rows.Clear();
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
                    NegPerfil.EliminarPerfil(perfilModificada.ClonarEntidad());
                    RecuperaPerfiles();
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
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperaPerfiles();
            HalitarControles(false, false, false, false, false, true, false);
            habilitarTab(false);
            tabControl1.SelectedTab = tabControl1.Tabs[0];
            SendKeys.SendWait("{TAB}");
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmb_modulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cargadatoscombo == true)
                {
                    

                    dsDatos = NegPerfilesAcceso.ListaConsultaTablasOpciones(Int16.Parse(cmb_modulo.SelectedValue.ToString()), int.Parse(txt_idperfil.Text));

                    grid_accesosn.DataSource = dsDatos;
                    grid_accesosn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
        }
        private void btn_accesosn_Click(object sender, EventArgs e)
        {
            try
            {
                cmb_modulo.Focus();
                DialogResult resultado;
                if (txt_descripcion.Text != string.Empty)
                {
                    resultado = MessageBox.Show("Desea guardar los Accesos para el Perfil?", "Opciones de Acceso Perfil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        List<PERFILES_ACCESOS> acOriginal = NegPerfilesAcceso.ListaPerfilesAccesos().Where(p => ((p.ID_PERFIL == Int16.Parse(txt_idperfil.Text))) && (p.ACCESO_OPCIONES.MODULO.ID_MODULO == Int32.Parse(cmb_modulo.SelectedValue.ToString()))).ToList().ClonarEntidad();
                        List<PERFILES_ACCESOS> acModificada = new List<PERFILES_ACCESOS>();
                        PERFILES_ACCESOS acNuevo = new PERFILES_ACCESOS();

                        NegPerfilesAcceso.EliminaListaPerfilesAccesos(acModificada, acOriginal);

                        for (int i = 0; i <= grid_accesosn.RowCount - 1; i++)
                        {
                            acNuevo = new PERFILES_ACCESOS();
                            if (grid_accesosn.Rows[i].Cells[2].Value.ToString() == true.ToString())
                            {
                                PERFILES nperfil = NegPerfil.RecuperaPerfiles().Where(p => p.ID_PERFIL == Int16.Parse(txt_idperfil.Text)).FirstOrDefault();
                                acNuevo.PERFILESReference.EntityKey = nperfil.EntityKey;
                                acNuevo.ID_PERFIL = Int16.Parse(txt_idperfil.Text);
                                ACCESO_OPCIONES nacceso = NegAccesoOpciones.ListaAccesoOpciones().Where(a => a.ID_ACCESO == Int32.Parse(grid_accesosn.Rows[i].Cells[0].Value.ToString())).FirstOrDefault();
                                acNuevo.ACCESO_OPCIONESReference.EntityKey = nacceso.EntityKey;
                                acNuevo.ID_ACCESO = Int32.Parse(grid_accesosn.Rows[i].Cells[0].Value.ToString());

                                NegPerfilesAcceso.CrearPerfilesAccesos(acNuevo);
                            }
                        }
                        MessageBox.Show("Datos Almacenados Correctamente");      

                    }

                }

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
                SendKeys.Send("{TAB}");
            }
        }
        private void gridperfiles_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridperfiles.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                perfil.Sort(new PerfilesComparer(strColumnName, strSortOrder));
                gridperfiles.DataSource = null;
                gridperfiles.DataSource = perfil;
                gridperfiles.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridperfiles.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridperfiles.Columns["ID_PERFIL"].HeaderText = "CODIGO";
                gridperfiles.Columns["DESCRIPCION"].HeaderText = "NOMBRE";

                gridperfiles.Columns["PERFILES_ACCESOS"].Visible = false;
                gridperfiles.Columns["USUARIOS_PERFILES"].Visible = false;
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridperfiles, new Point(MousePosition.X - e.X - gridperfiles.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Departamento", gridperfiles.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridperfiles.RowCount - 1; i++)
                    {
                        if (gridperfiles.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridperfiles.CurrentCell = gridperfiles.Rows[i].Cells[columnabuscada];

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
        private void AgregarBindigControles()
        {
            Binding ID_PERFIL = new Binding("Text", perfilModificada, "ID_PERFIL", true);
            Binding DESCRIPCION = new Binding("Text", perfilModificada, "DESCRIPCION", true);
            

            txt_idperfil.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();

            txt_idperfil.DataBindings.Add(ID_PERFIL);
            txt_descripcion.DataBindings.Add(DESCRIPCION);
            
        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            perfilModificada = new PERFILES();
            perfilOriginal = new PERFILES();

            txt_idperfil.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            cmb_modulo.SelectedItem = -1;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (perfilModificada.ID_PERFIL == 0)
            {
                AgregarError(txt_idperfil);
                valido = false;
            }
            if (perfilModificada.DESCRIPCION == null || perfilModificada.DESCRIPCION == string.Empty)
            {
                AgregarError(txt_descripcion);
                valido = false;
            }

            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridperfiles.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (perfilModificada.EntityKey == null)
                    {
                        NegPerfil.CrearPerfil(perfilModificada);

                    }
                    else
                    {
                        NegPerfil.GrabarPerfil(perfilModificada, perfilOriginal);

                    }

                    RecuperaPerfiles();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false);
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
            grpDatosPrincipales.Enabled = datosPrincipales;
            grpDatosSecundarios.Enabled = datosSecundarios;

        }
        public void habilitarTab(bool pg1)
        {
            ultraTabPageControl2.Enabled = pg1;
        }
        private void RecuperaPerfiles()
        {
            perfil = NegPerfil.RecuperaPerfiles();
            gridperfiles.DataSource = perfil;
            gridperfiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridperfiles.Columns["ID_PERFIL"].HeaderText = "CODIGO";
            gridperfiles.Columns["DESCRIPCION"].HeaderText = "NOMBRE";

            gridperfiles.Columns["PERFILES_ACCESOS"].Visible = false;
            gridperfiles.Columns["USUARIOS_PERFILES"].Visible = false;
            
        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridperfiles.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridperfiles.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridperfiles.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridperfiles.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridperfiles.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class PerfilesComparer : IComparer<PERFILES>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public PerfilesComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(PERFILES Student1, PERFILES Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "ID_PERFIL":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.ID_PERFIL.CompareTo(Student2.ID_PERFIL);
                        }
                        else
                        {
                            returnValue = Student2.ID_PERFIL.CompareTo(Student1.ID_PERFIL);
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
