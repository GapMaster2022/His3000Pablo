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
using His.Parametros;

namespace His.Honorarios
{
    public partial class frm_ComisionAprtes : Form
    {
        #region Variables
        public DtoFormaPago formapagoOriginal = new DtoFormaPago();
        public DtoFormaPago formapagoModificada = new DtoFormaPago();
        public FORMA_PAGO fpOrigen = new FORMA_PAGO();
        public FORMA_PAGO fpModificada = new FORMA_PAGO();
        public List<TIPO_FORMA_PAGO> tipoformap = new List<TIPO_FORMA_PAGO>();
        public List<DtoFormaPago> formapagos = new List<DtoFormaPago>();
        public int columnabuscada;
        #endregion

        #region Constructores
        public frm_ComisionAprtes()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_ComisionAprtes_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(true, false, false, false, false, true, false);

                //carga los tipos forma de pago en el combobox
                //tipoformap =NegFormaPago.RecuperaTipoFormaPagos();
                DataTable Tabla = NegFormaPago.RecuperarClasificacion();
                cmb_tipoformapago.DataSource = Tabla;
                cmb_tipoformapago.ValueMember = Tabla.Columns[0].ToString();
                cmb_tipoformapago.DisplayMember = Tabla.Columns[1].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cmb_tipoformapago.Focus();
            ResetearControles();
            RecuperaFormaPagos();
            HalitarControles(true, false, false, false, false, true, false);


        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void grid_formapago_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, false, true, false, false, false, true);
                for_codigo = Convert.ToInt32(grid_formapago.CurrentRow.Cells[0].Value.ToString());
                txt_formapago.Text = grid_formapago.CurrentRow.Cells[1].Value.ToString();
                txt_comision.Text = grid_formapago.CurrentRow.Cells[2].Value.ToString();
                txt_aporte.Text = grid_formapago.CurrentRow.Cells[3].Value.ToString();
                formapagoModificada = grid_formapago.CurrentRow.DataBoundItem as DtoFormaPago;
                formapagoOriginal = formapagoModificada.ClonarEntidad();
                txt_formapago.ReadOnly = true;
                //AgregarBindigControles();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void cmb_tipoformapago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cmb_tipoformapago.Text != "System.Data.DataRowView" && cmb_tipoformapago.DataSource != null)
                {
                    Datos.Atencion obj = new Datos.Atencion();
                    DataSet dts_combos = new DataSet();
                    dts_combos = obj.combo_fomapago(cmb_tipoformapago.Text);

                    cmb_Pago.DataSource = dts_combos.Tables[0];
                    cmb_Pago.DisplayMember = dts_combos.Tables[0].Columns["TE_DESCRIPCION"].ColumnName.ToString();
                    cmb_Pago.ValueMember = dts_combos.Tables[0].Columns["TE_CODIGO"].ColumnName.ToString();

                }

            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
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
        private void txt_comision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    txt_aporte.Focus();
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void txt_aporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;

                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void grid_formapago_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    //get the current column details 
            //    string strColumnName = grid_formapago.Columns[e.ColumnIndex].Name;
            //    SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

            //    formapagos.Sort(new FormaPagoComparer(strColumnName, strSortOrder));
            //    grid_formapago.DataSource = null;
            //    grid_formapago.DataSource = formapagos;
            //    grid_formapago.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
            //    grid_formapago.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
            //    grid_formapago.Columns["FOR_CODIGO"].HeaderText = "CODIGO";
            //    grid_formapago.Columns["FOR_DESCRIPCION"].HeaderText = "NOMBRE";
            //    grid_formapago.Columns["FOR_COMISION"].HeaderText = "COMISION CLINICA";
            //    grid_formapago.Columns["FOR_REFERIDO"].HeaderText = "LLAMADAS REFERIDAS";

            //    grid_formapago.Columns["TIF_CODIGO"].Visible = false;
            //    grid_formapago.Columns["TIF_NOMBRE"].Visible = false;
            //    grid_formapago.Columns["FOR_ACTIVO"].Visible = false;
            //    grid_formapago.Columns["FOR_ESTADO"].Visible = false;
            //    grid_formapago.Columns["FOR_CUENTA_CONTABLE"].Visible = false;
            //    grid_formapago.Columns["ENTITYSETNAME"].Visible = false;
            //    grid_formapago.Columns["ENTITYID"].Visible = false;
            //}
            //else
            //{
            //    columnabuscada = e.ColumnIndex;
            //    //gridMedicos.Columns[e.ColumnIndex].ContextMenuStrip = new mnuContexsecundario.Show();
            //    mnuContexsecundario.Show(grid_formapago, new Point(MousePosition.X - e.X - grid_formapago.Width, e.Y));

            //}
        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Comisión Aportes", grid_formapago.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= grid_formapago.RowCount - 1; i++)
                    {
                        if (grid_formapago.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            grid_formapago.CurrentCell = grid_formapago.Rows[i].Cells[columnabuscada];

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
        protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        {

            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            grpDatosPrincipales.Enabled = datosPrincipales;
            grpDatosSecundarios.Enabled = datosSecundarios;
            btnNuevo.Enabled = Nuevo;
            btnModificar.Enabled = Modificar;

        }
        private void RecuperaFormaPagos()
        {
            DataTable Forma = new DataTable();



            Forma = NegFactura.PlazoPagoSic(Convert.ToInt32(cmb_Pago.SelectedValue.ToString()));
            if (Forma.Rows.Count > 0)
            {
                grid_formapago.DataSource = Forma;

            }
            else
                grid_formapago.DataSource = null;
            //formapagos = NegFormaPago.RecuperaFormaPagos().Where(p => p.TIF_CODIGO == Int16.Parse(cmb_tipoformapago.SelectedValue.ToString())).ToList();

            //grid_formapago.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //grid_formapago.Columns["FOR_CODIGO"].HeaderText = "CODIGO";
            //grid_formapago.Columns["FOR_DESCRIPCION"].HeaderText = "NOMBRE";
            //grid_formapago.Columns["FOR_COMISION"].HeaderText = "COMISION CLINICA";
            //grid_formapago.Columns["FOR_REFERIDO"].HeaderText = "LLAMADAS REFERIDAS";

            //grid_formapago.Columns["TIF_CODIGO"].Visible = false;
            //grid_formapago.Columns["TIF_NOMBRE"].Visible = false;
            //grid_formapago.Columns["FOR_ACTIVO"].Visible = false;
            //grid_formapago.Columns["FOR_ESTADO"].Visible = false;
            //grid_formapago.Columns["FOR_CUENTA_CONTABLE"].Visible = false;
            //grid_formapago.Columns["ENTITYSETNAME"].Visible = false;
            //grid_formapago.Columns["ENTITYID"].Visible = false;
        }
        private void AgregarBindigControles()
        {
            Binding FOR_COMISION = new Binding("Text", formapagoModificada, "FOR_COMISION", true);
            Binding FOR_REFERIDO = new Binding("Text", formapagoModificada, "FOR_REFERIDO", true);
            Binding FOR_DESCRIPCION = new Binding("Text", formapagoModificada, "FOR_DESCRIPCION", true);
            //Binding PWD1 = new Binding("Text", usuarioModificada, "PWD", true);

            txt_aporte.DataBindings.Clear();
            txt_comision.DataBindings.Clear();
            txt_formapago.DataBindings.Clear();
            //txt_conclave.DataBindings.Clear();

            txt_aporte.DataBindings.Add(FOR_REFERIDO);
            txt_comision.DataBindings.Add(FOR_COMISION);
            txt_formapago.DataBindings.Add(FOR_DESCRIPCION);
            txt_formapago.Enabled = false;

        }
        public int for_codigo;
        private void GrabarDatos()
        {
            DialogResult resultado;
            grid_formapago.Focus();

            if (ValidarFormulario())
            {
                if (Editar == true)
                {

                    resultado = MessageBox.Show("¿Desea actualizar los datos?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {

                        NegFormaPago.ActualizarFormaPago(cmb_Pago.SelectedValue.ToString(), for_codigo, txt_formapago.Text, Convert.ToDouble(txt_comision.Text), Convert.ToDouble(txt_aporte.Text));
                        //fpModificada.FOR_CODIGO = formapagoModificada.FOR_CODIGO;
                        //fpModificada.FOR_DESCRIPCION = formapagoModificada.FOR_DESCRIPCION;
                        //fpModificada.FOR_CUENTA_CONTABLE = formapagoModificada.FOR_CUENTA_CONTABLE;
                        //fpModificada.FOR_COMISION = formapagoModificada.FOR_COMISION;
                        //fpModificada.FOR_REFERIDO = formapagoModificada.FOR_REFERIDO;
                        //fpModificada.FOR_ACTIVO = formapagoModificada.FOR_ACTIVO;
                        //fpModificada.FOR_ESTADO = formapagoModificada.FOR_ESTADO;
                        //TIPO_FORMA_PAGO tipfpModificada = cmb_tipoformapago.SelectedItem as TIPO_FORMA_PAGO;
                        //fpModificada.TIPO_FORMA_PAGOReference.EntityKey = tipfpModificada.EntityKey;
                        //fpModificada.EntityKey = new EntityKey(formapagos.First().ENTITYSETNAME
                        //        , formapagos.First().ENTITYID, formapagoModificada.FOR_CODIGO);

                        //fpOrigen.FOR_CODIGO = formapagoOriginal.FOR_CODIGO;
                        //fpOrigen.FOR_DESCRIPCION = formapagoOriginal.FOR_DESCRIPCION;
                        //fpOrigen.FOR_CUENTA_CONTABLE = formapagoOriginal.FOR_CUENTA_CONTABLE;
                        //fpOrigen.FOR_COMISION = formapagoOriginal.FOR_COMISION;
                        //fpOrigen.FOR_REFERIDO = formapagoOriginal.FOR_REFERIDO;
                        //fpOrigen.FOR_ACTIVO = formapagoOriginal.FOR_ACTIVO;
                        //fpOrigen.FOR_ESTADO = formapagoOriginal.FOR_ESTADO;
                        //TIPO_FORMA_PAGO tipfpOriginal = tipoformap.Where(emp => emp.TIF_CODIGO == formapagoOriginal.TIF_CODIGO).FirstOrDefault();
                        //fpOrigen.TIPO_FORMA_PAGOReference.EntityKey = tipfpOriginal.EntityKey;
                        //fpOrigen.EntityKey = new EntityKey(formapagos.First().ENTITYSETNAME
                        //  , formapagos.First().ENTITYID, formapagoOriginal.FOR_CODIGO);

                        //NegFormaPago.GrabarFormaPago(fpModificada, fpOrigen);

                        RecuperaFormaPagos();
                        ResetearControles();
                        HalitarControles(true, false, false, false, false, true, false);
                        Editar = false;
                        MessageBox.Show("Datos actualizados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (!NegFormaPago.NoRepetir(txt_formapago.Text.Trim()))
                    {
                        if(NegFormaPago.UltimoCodigoFormaPago() != 0)
                        {
                            for_codigo = NegFormaPago.UltimoCodigoFormaPago();
                            NegFormaPago.CrearFormaPago(cmb_Pago.SelectedValue.ToString(), for_codigo, txt_formapago.Text.Trim(), Convert.ToDouble(txt_comision.Text), Convert.ToDouble(txt_aporte.Text));

                            RecuperaFormaPagos();
                            ResetearControles();
                            HalitarControles(true, false, false, false, false, true, false);
                            txt_formapago.ReadOnly = true;
                            MessageBox.Show("Datos almacenados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No pueden existir formas de pagos con el mismo nombre", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
            }
        }
        private void ResetearControles()
        {
            formapagoModificada = new DtoFormaPago();
            formapagoOriginal = new DtoFormaPago();
            fpOrigen = new FORMA_PAGO();
            fpModificada = new FORMA_PAGO();

            txt_aporte.Text = string.Empty;
            txt_comision.Text = string.Empty;
            txt_formapago.Text = string.Empty;

        }
        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }
        private bool ValidarFormulario()
        {
            bool valido = true;
            //if (formapagoModificada.FOR_COMISION == null)
            //{
            //    AgregarError(txt_comision);
            //    valido = false;
            //}
            //if (formapagoModificada.FOR_REFERIDO == null)
            //{
            //    AgregarError(txt_aporte);
            //    valido = false;
            //}

            if (txt_formapago.Text.Trim() == "")
            {
                controlErrores.SetError(txt_formapago, "Campo Obligatorio");
                valido = false;
            }

            if (txt_comision.Text.Trim() == "")
            {
                controlErrores.SetError(txt_comision, "Campo Obligatorio");
                valido = false;
            }

            if (txt_aporte.Text.Trim() == "")
            {
                controlErrores.SetError(txt_aporte, "Campo Obligatorio");
                valido = false;
            }


            return valido;

        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (grid_formapago.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                grid_formapago.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                grid_formapago.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                grid_formapago.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                grid_formapago.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class FormaPagoComparer : IComparer<DtoFormaPago>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public FormaPagoComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(DtoFormaPago Student1, DtoFormaPago Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "FOR_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.FOR_CODIGO.CompareTo(Student2.FOR_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.FOR_CODIGO.CompareTo(Student1.FOR_CODIGO);
                        }

                        break;
                    case "FOR_DESCRIPCION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.FOR_DESCRIPCION.CompareTo(Student2.FOR_DESCRIPCION);
                        }
                        else
                        {
                            returnValue = Student2.FOR_DESCRIPCION.CompareTo(Student1.FOR_DESCRIPCION);
                        }
                        break;
                    case "FOR_COMISION":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.FOR_COMISION.CompareTo(Student2.FOR_COMISION);
                        }
                        else
                        {
                            returnValue = Student2.FOR_COMISION.CompareTo(Student1.FOR_COMISION);
                        }
                        break;
                    case "FOR_REFERIDO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.FOR_REFERIDO.CompareTo(Student2.FOR_REFERIDO);
                        }
                        else
                        {
                            returnValue = Student2.FOR_REFERIDO.CompareTo(Student1.FOR_REFERIDO);
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

        private void txt_comision_Leave(object sender, EventArgs e)
        {
            if (txt_comision.Text == string.Empty)
                txt_comision.Text = formapagoModificada.FOR_COMISION.ToString();

        }

        private void txt_aporte_Leave(object sender, EventArgs e)
        {
            if (txt_aporte.Text == string.Empty)
                txt_aporte.Text = formapagoModificada.FOR_REFERIDO.ToString();
        }

        private void grpDatosSecundarios_Click(object sender, EventArgs e)
        {

        }

        private void cmb_Pago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Pago.Text != "System.Data.DataRowView" && cmb_Pago.DataSource != null)
            {
                RecuperaFormaPagos();
            }
        }

        private void grid_formapago_DoubleClick(object sender, EventArgs e)
        {
            if (grid_formapago.SelectedRows.Count > 0)
            {
                HalitarControles(true, false, true, false, false, false, true);
                for_codigo = Convert.ToInt32(grid_formapago.CurrentRow.Cells[0].Value.ToString());
                txt_formapago.Text = grid_formapago.CurrentRow.Cells[1].Value.ToString();
                txt_comision.Text = grid_formapago.CurrentRow.Cells[2].Value.ToString();
                txt_aporte.Text = grid_formapago.CurrentRow.Cells[3].Value.ToString();
                formapagoModificada = grid_formapago.CurrentRow.DataBoundItem as DtoFormaPago;
                formapagoOriginal = formapagoModificada.ClonarEntidad();
                txt_formapago.ReadOnly = true;
            }
        }
        public bool Editar = false;
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Editar = true;
            HalitarControles(true, true, false, true, false, false, true);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Editar = false;
            HalitarControles(true, true, false, true, false, false, true);
            txt_formapago.ReadOnly = false;
        }
    }
}
