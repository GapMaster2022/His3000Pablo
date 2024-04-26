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
using His.General;
using His.Negocio;

namespace TarifariosUI
{
    public partial class frm_CatlogoCostos : Form
    {
        #region Variables
        public CATALOGO_COSTOS ccostosOriginal = new CATALOGO_COSTOS();
        public CATALOGO_COSTOS ccostosModificada = new CATALOGO_COSTOS();
        public List<CATALOGO_COSTOS> ccostos = new List<CATALOGO_COSTOS>();
        private List<CATALOGO_COSTOS_TIPO> cctipo = new List<CATALOGO_COSTOS_TIPO>();
        public int columnabuscada;
        private bool nuevo;
        #endregion

        #region Constructores
        public frm_CatlogoCostos()
        {
            InitializeComponent();
            inicializarControles();
        }
        #endregion

        #region Eventos
        private void inicializarControles()
        {
            cctipo = NegCatalogoCostosTipo.RecuperaCctipo();
            Cmb_cctipo.DataSource = cctipo;
            Cmb_cctipo.ValueMember = "CCT_CODIGO";
            Cmb_cctipo.DisplayMember = "CCT_NOMBRE";
            RecuperaCcostos();

        }

        private void frm_CatlogosCostos_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(false, false, false, false, false, true, false);
                RecuperaCcostos();
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
                nuevo = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gridCcostos_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                //ccostosModificada = gridCcostos.CurrentRow.DataBoundItem as CATALOGO_COSTOS;
                ccostosModificada.CAC_CODIGO = (Int16)gridCcostos.CurrentRow.Cells[0].Value;
                ccostosModificada.CAC_NOMBRE = gridCcostos.CurrentRow.Cells[1].Value.ToString();
                int codTipoCatalogo = (Int16)gridCcostos.CurrentRow.Cells[2].Value;
                Cmb_cctipo.SelectedItem = cctipo.FirstOrDefault(c => c.CCT_CODIGO == codTipoCatalogo);
                ccostosOriginal = ccostosModificada.ClonarEntidad();
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
                    NegCatalogoCostos.EliminarCcostos(ccostosModificada);

                    RecuperaCcostos();
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
            RecuperaCcostos();
            HalitarControles(false, false, false, false, false, true, false);
            nuevo = false;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                nuevo = true;
                HalitarControles(true, true, false, true, false, false, true);
                ccostosOriginal = new CATALOGO_COSTOS();
                ccostosModificada = new CATALOGO_COSTOS();

                ResetearControles();
                ccostosModificada.CAC_CODIGO = Int16.Parse((NegCatalogoCostos.RecuperaMaximoCcostos() + 1).ToString());
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

        }
        private void gridCcostos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (gridCcostos.Columns[e.ColumnIndex].Tag == null)
                    {
                        gridCcostos.Columns[e.ColumnIndex].Tag = SortOrder.Ascending;
                    }
                    else if (gridCcostos.Columns[e.ColumnIndex].Tag.ToString() == SortOrder.Ascending.ToString())
                    {
                        gridCcostos.Columns[e.ColumnIndex].Tag = SortOrder.Descending;
                    }
                    else if (gridCcostos.Columns[e.ColumnIndex].Tag.ToString() == SortOrder.Descending.ToString())
                    {
                        gridCcostos.Columns[e.ColumnIndex].Tag = SortOrder.Ascending;
                    }
                    string columna = gridCcostos.Columns[e.ColumnIndex].DataPropertyName + " " + gridCcostos.Columns[e.ColumnIndex].Tag;
                    var listCostos = ccostos.Select(c => new { c.CAC_CODIGO, c.CAC_NOMBRE, c.CATALOGO_COSTOS_TIPO.CCT_CODIGO, c.CATALOGO_COSTOS_TIPO.CCT_NOMBRE }).ToList();
                    listCostos = listCostos.OrdenarPorPropiedad(columna).ToList();
                    //gridCcostos.DataSource = listCostos.Select(c => new { c.CAC_CODIGO, c.CAC_NOMBRE, c.CATALOGO_COSTOS_TIPO.CCT_CODIGO, c.CATALOGO_COSTOS_TIPO.CCT_NOMBRE }).ToList(); 
                    gridCcostos.DataSource = listCostos;

                }

                else if (e.Button == MouseButtons.Right)
                {

                    columnabuscada = e.ColumnIndex;
                    mnuContexsecundario.Show(gridCcostos, new Point(MousePosition.X - e.X - gridCcostos.Width, e.Y));

                }


            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }


        }
        private void mnuBuscar_Click(object sender, EventArgs e)
        {

        }
        private void MmanuBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string columna;
                string value = "";
                if (InputBox("Usuarios", gridCcostos.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    columna = value.ToUpper();
                    for (int i = 0; i <= gridCcostos.RowCount - 1; i++)
                    {
                        if (gridCcostos.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridCcostos.CurrentCell = gridCcostos.Rows[i].Cells[columnabuscada];
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
            Binding CAC_CODIGO = new Binding("Text", ccostosModificada, "CAC_CODIGO", true);
            Binding CAC_NOMBRE = new Binding("Text", ccostosModificada, "CAC_NOMBRE", true);
            txt_codigo.DataBindings.Clear();
            txt_nombre.DataBindings.Clear();
            txt_codigo.DataBindings.Add(CAC_CODIGO);
            txt_nombre.DataBindings.Add(CAC_NOMBRE);

            //  txt_nombre.DataBindings.Add(CCT_NOMBRE);


        }
        private void ResetearControles()
        {
            ccostosModificada = new CATALOGO_COSTOS();
            ccostosOriginal = new CATALOGO_COSTOS();

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
            //if (ccostosModificada.CAC_CODIGO == 0)
            //{
            //    AgregarError(txt_codigo);
            //    valido = false;
            //}
            if (Cmb_cctipo.SelectedIndex == 0)
            {
                AgregarError(Cmb_cctipo);
                valido = false;
            }
            if (txt_nombre.Text == string.Empty)
            {
                AgregarError(txt_nombre);
                valido = false;
            }

            //if (ccostosModificada.CAC_NOMBRE == null || ccostosModificada.CAC_NOMBRE == string.Empty)
            //{
            //    AgregarError(txt_nombre);
            //    valido = false;
            //}

            return valido;

        }

        private void GrabarDatos()
        {
            DialogResult resultado;
            gridCcostos.Focus();

            if (ValidarFormulario())
            {
                CATALOGO_COSTOS_TIPO cctipo = (CATALOGO_COSTOS_TIPO)Cmb_cctipo.SelectedItem;
                ccostosModificada.CAC_CODIGO = Convert.ToInt16(txt_codigo.Text.ToString());
                ccostosModificada.CAC_NOMBRE = txt_nombre.Text;
                ccostosModificada.CATALOGO_COSTOS_TIPOReference.EntityKey = cctipo.EntityKey;

                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (nuevo == true)
                    {
                        NegCatalogoCostos.CrearCcostos(ccostosModificada);
                    }
                    else
                    {
                        NegCatalogoCostos.GrabarCcostos(ccostosModificada, ccostosOriginal);

                    }

                    RecuperaCcostos();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false);
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Datos Incompletos");
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

        }
        private void RecuperaCcostos()
        {
            ccostos = NegCatalogoCostos.RecuperaCcostos();
            var listaCostos = ccostos.Select(c => new { c.CAC_CODIGO, c.CAC_NOMBRE, c.CATALOGO_COSTOS_TIPO.CCT_CODIGO, c.CATALOGO_COSTOS_TIPO.CCT_NOMBRE }).ToList();
            gridCcostos.DataSource = listaCostos;
            //gridCcostos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridCcostos.Columns["CAC_CODIGO"].HeaderText = "CODIGO";
            gridCcostos.Columns["CCT_NOMBRE"].HeaderText = "TIPO CATALOGO";
            gridCcostos.Columns["CAC_NOMBRE"].HeaderText = "CATALOGO";
            gridCcostos.Columns["CCT_CODIGO"].Visible = false;
            //gridCcostos.Columns["CATALOGO_COSTOS"].Visible = false;
            //gridbancos.Columns["BAN_CUENTA_CONTABLE"].Visible = false;

        }
        private SortOrder getSortOrder(int columnIndex)
        {
            if (gridCcostos.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gridCcostos.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gridCcostos.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                gridCcostos.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gridCcostos.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }
        class CcostosComparer : IComparer<CATALOGO_COSTOS>
        {
            string memberName = string.Empty; // specifies the member name to be sorted 
            SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder. 

            /// <summary> 
            /// constructor to set the sort column and sort order. 
            /// </summary> 
            /// <param name="strMemberName"></param> 
            /// <param name="sortingOrder"></param> 
            public CcostosComparer(string strMemberName, SortOrder sortingOrder)
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
            public int Compare(CATALOGO_COSTOS Student1, CATALOGO_COSTOS Student2)
            {
                int returnValue = 1;
                switch (memberName)
                {
                    case "CAC_CODIGO":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CAC_CODIGO.CompareTo(Student2.CAC_CODIGO);
                        }
                        else
                        {
                            returnValue = Student2.CAC_CODIGO.CompareTo(Student1.CAC_CODIGO);
                        }

                        break;
                    case "CAC_NOMBRE":
                        if (sortOrder == SortOrder.Ascending)
                        {
                            returnValue = Student1.CAC_NOMBRE.CompareTo(Student2.CAC_NOMBRE);
                        }
                        else
                        {
                            returnValue = Student2.CAC_NOMBRE.CompareTo(Student1.CAC_NOMBRE);
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

        private void frm_CatlogoCostos_Load(object sender, EventArgs e)
        {

        }

        private void Cmb_cctipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CATALOGO_COSTOS_TIPO cctipo = (CATALOGO_COSTOS_TIPO)Cmb_cctipo.SelectedItem;
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void btnCerrar_Click_1(object sender, EventArgs e)
        {

        }

        private void ultraPanel1_PaintClient(object sender, PaintEventArgs e)
        {

        }

        
    }
}
