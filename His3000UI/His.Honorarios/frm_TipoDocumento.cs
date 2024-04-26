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
    public partial class frm_TipoDocumento : Form
    {
        #region Variables
        public TIPO_DOCUMENTO tipodocOriginal = new TIPO_DOCUMENTO();
        public TIPO_DOCUMENTO tipodocModificada = new TIPO_DOCUMENTO();
        public List<TIPO_DOCUMENTO> tipodocumentos = new List<TIPO_DOCUMENTO>();
        public int columnabuscada;
        #endregion

        #region Constructor
        public frm_TipoDocumento()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_TipoDocumento_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaTipoDocumentos();
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
        private void gridtipodocmento_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                tipodocModificada = gridtipodocmento.CurrentRow.DataBoundItem as TIPO_DOCUMENTO;
                tipodocOriginal = tipodocModificada.ClonarEntidad();
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
                //var ga = NegGeneracionesAutomaticas.ListaGeneracionesAutomaticas().Where(cod => cod.TIPO_DOCUMENTO.TID_CODIGO == tipodocModificada.TID_CODIGO).ToList();
                //if (ga == null)
                //{
                //    var ncd = NegNotaCreditoDebito.ListaNotasCreditoDebito().Where(cod => cod.TID_CODIGO == tipodocModificada.TID_CODIGO).ToList();
                //    if (ncd == null)
                //    {
                //        DialogResult resultado;

                //        resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //        if (resultado == DialogResult.Yes)
                //        {
                //            NegTipoDocumentos.EliminarTipoDocumento(tipodocModificada.ClonarEntidad());
                //            RecuperaTipoDocumentos();
                //            ResetearControles();
                //            HalitarControles(false, false, false, false, false, true, false);
                //            MessageBox.Show("Datos Eliminados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }
                //    }
                //    else
                //        MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //else
                //    MessageBox.Show("Operación Invalida", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult resultado;

                resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {

                    NegGeneracionesAutomaticas.EliminaTipoDocumento(Convert.ToInt32(tipodocModificada.TID_CODIGO));

                    RecuperaTipoDocumentos();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false);

                    MessageBox.Show("Registro eliminado correctamente.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                HalitarControles(true, true, false, true, false, false,true);
                tipodocOriginal = new TIPO_DOCUMENTO();
                tipodocModificada = new TIPO_DOCUMENTO();

                ResetearControles();
                tipodocModificada.TID_CODIGO = Int16.Parse((NegTipoDocumentos.RecuperaMaximoTipoDocumento() + 1).ToString());
                tipodocModificada.TID_ESTADO = true;
                AgregarBindigControles();
                txt_nombre.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
            RecuperaTipoDocumentos();
            HalitarControles(false, false, false, false, false, true, false);
        }
        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                chk_estado.Focus();
            }

        }
        private void gridtipodocmento_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //get the current column details 
                string strColumnName = gridtipodocmento.Columns[e.ColumnIndex].Name;
                SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

                tipodocumentos.Sort(new TipoDocumentoComparer(strColumnName, strSortOrder));
                gridtipodocmento.DataSource = null;
                gridtipodocmento.DataSource = tipodocumentos;
                gridtipodocmento.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridtipodocmento.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
                gridtipodocmento.Columns["TID_CODIGO"].HeaderText = "CODIGO";
                gridtipodocmento.Columns["TID_DESCRIPCION"].HeaderText = "NOMBRE";
                gridtipodocmento.Columns["TID_ESTADO"].HeaderText = "ESTADO";
                
            }
            else
            {
                columnabuscada = e.ColumnIndex;
                //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
                mnuContexsecundario.Show(gridtipodocmento, new Point(MousePosition.X - e.X - gridtipodocmento.Width, e.Y));

            }
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Comisión Aportes", gridtipodocmento.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridtipodocmento.RowCount - 1; i++)
                    {
                        if (gridtipodocmento.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridtipodocmento.CurrentCell = gridtipodocmento.Rows[i].Cells[columnabuscada];

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
            Binding TID_CODIGO = new Binding("Text", tipodocModificada, "TID_CODIGO", true);
            Binding TID_DESCRIPCION = new Binding("Text", tipodocModificada, "TID_DESCRIPCION", true);
            Binding TID_ESTADO = new Binding("Checked", tipodocModificada, "TID_ESTADO", true);

            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            chk_estado.DataBindings.Clear();

            txt_codigo.DataBindings.Add(TID_CODIGO);
            txt_nombre.DataBindings.Add(TID_DESCRIPCION);
            chk_estado.DataBindings.Add(TID_ESTADO);

        }
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            tipodocModificada = new TIPO_DOCUMENTO();
            tipodocOriginal = new TIPO_DOCUMENTO();

            txt_codigo.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            chk_estado.Checked = true;
        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (tipodocModificada.TID_CODIGO == 0)
            {
                AgregarError(txt_codigo);
                valido = false;
            }
            if (tipodocModificada.TID_DESCRIPCION == null || tipodocModificada.TID_DESCRIPCION == string.Empty)
            {
                AgregarError(txt_nombre);
                valido = false;
            }

            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridtipodocmento.Focus();

            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (tipodocModificada.EntityKey == null)
                    {
                        NegTipoDocumentos.CrearTipoDocumento(tipodocModificada);

                    }
                    else
                    {
                        NegTipoDocumentos.GrabarTipoDocumento(tipodocModificada, tipodocOriginal);

                    }

                    RecuperaTipoDocumentos();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true,false);
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
        private void RecuperaTipoDocumentos()
        {
            tipodocumentos =NegTipoDocumentos.RecuperaTipoDocumentos();
            gridtipodocmento.DataSource = tipodocumentos;
            gridtipodocmento.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridtipodocmento.Columns["TID_CODIGO"].HeaderText = "CODIGO";
            gridtipodocmento.Columns["TID_DESCRIPCION"].HeaderText = "NOMBRE";
            gridtipodocmento.Columns["TID_ESTADO"].HeaderText = "ESTADO";
            gridtipodocmento.Columns["GENERACIONES_AUTOMATICAS"].Visible = false;
            gridtipodocmento.Columns["NOTAS_CREDITO_DEBITO"].Visible = false;
                

        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridtipodocmento.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridtipodocmento.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridtipodocmento.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridtipodocmento.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridtipodocmento.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class TipoDocumentoComparer : IComparer<TIPO_DOCUMENTO>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public TipoDocumentoComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(TIPO_DOCUMENTO Student1, TIPO_DOCUMENTO Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "TID_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TID_CODIGO.CompareTo(Student2.TID_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.TID_CODIGO.CompareTo(Student1.TID_CODIGO);
                        }

                        break;
                    case "TID_DESCRIPCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TID_DESCRIPCION.CompareTo(Student2.TID_DESCRIPCION);
                        }
                        else
                        {
                            returnValue = Student2.TID_DESCRIPCION.CompareTo(Student1.TID_DESCRIPCION);
                        }
                        break;
                    case "TID_ESTADO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.TID_ESTADO.CompareTo(Student2.TID_ESTADO);
                        }
                        else
                        {
                            returnValue = Student2.TID_ESTADO.CompareTo(Student1.TID_ESTADO);
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
