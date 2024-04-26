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
    public partial class frm_Categoria : Form
    {
        #region Variables
        public CATEGORIAS_CONVENIOS categoriasOriginal = new CATEGORIAS_CONVENIOS();
        public CATEGORIAS_CONVENIOS categoriasModificada = new CATEGORIAS_CONVENIOS();
        public List<CATEGORIAS_CONVENIOS> categorias = new List<CATEGORIAS_CONVENIOS>();
        public int columnabuscada;
        private List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        private  List<TIPO_EMPRESA> tempresa = new List<TIPO_EMPRESA>();
        #endregion

        public frm_Categoria()
        {
            InitializeComponent();
            inicializarControles(); 
        }
        private void inicializarControles()
        {
              
            tempresa=NegAseguradoras.RecuperaTipoEmpresa();
            Cmb_tempresas.DataSource = tempresa;
            Cmb_tempresas.ValueMember = "TE_CODIGO";
            Cmb_tempresas.DisplayMember = "TE_DESCRIPCION";
            Cmb_aseguradoras.SelectedIndex = 0; 

            TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)Cmb_tempresas.SelectedItem;
            aseguradoras = NegAseguradoras.RecuperarListaEmpresas(tipoEmpresa.TE_CODIGO );
            Cmb_aseguradoras.DataSource = aseguradoras;
            Cmb_aseguradoras.ValueMember = "ASE_CODIGO";
            Cmb_aseguradoras.DisplayMember = "ASE_NOMBRE";
        }


        private void frm_Convenios_Load(object sender, EventArgs e)
        {
            try
            {

                HalitarControles(true , true, true, true, true, true, false);
                RecuperaCategorias();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) 
                    MessageBox.Show(ex.InnerException.Message);
                else
                    MessageBox.Show(ex.Message);
 
            }

        }

        private void grpDatosPrincipales_Enter(object sender, EventArgs e)
        {

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
                if(ex.InnerException!=null)
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void gridcateoria_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                HalitarControles(true, true, true, true, true, false, true);
                categoriasModificada = gridcategorias.CurrentRow.DataBoundItem as CATEGORIAS_CONVENIOS;
                categoriasOriginal = categoriasModificada.ClonarEntidad();
                AgregarBindigControles();
                //
                for (int i = 0; i < Cmb_tempresas.Items.Count; i++)
                {
                    TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)Cmb_tempresas.Items[i];
                    if (tipoEmpresa.TE_CODIGO == categoriasModificada.ASEGURADORAS_EMPRESAS.TIPO_EMPRESA.TE_CODIGO)
                    {
                        Cmb_tempresas.SelectedItem = Cmb_tempresas.Items[i];
                        for (int j = 0; j < Cmb_aseguradoras.Items.Count; j++)
                        {
                            ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)Cmb_aseguradoras.Items[j];
                            if (aseguradora.ASE_CODIGO == categoriasModificada.ASEGURADORAS_EMPRESAS.ASE_CODIGO)
                            {
                                Cmb_aseguradoras.SelectedItem = Cmb_aseguradoras.Items[j];
                                break;
                            }
                        }
                        return;
                    }
                }  
                 
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
                    NegCategorias.EliminarCategorias(categoriasModificada.ClonarEntidad());
                    RecuperaCategorias();
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
            RecuperaCategorias();
            HalitarControles(false, false, false, false, false, true, false);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
                try
                {
                    HalitarControles(true, true, true, true, true, true, false);
                    categoriasOriginal = new CATEGORIAS_CONVENIOS();
                    categoriasModificada = new CATEGORIAS_CONVENIOS();

                    ResetearControles();
                    categoriasModificada.CAT_CODIGO = Int16.Parse((NegCategorias.RecuperaMaximoCategorias() + 1).ToString());
                    categoriasModificada.CAT_ESTADO = true;
                    
                    
                    AgregarBindigControles();
                    txt_nombre.Focus();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }
        private void Cmb_tempresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)Cmb_tempresas.SelectedItem;
            Cmb_aseguradoras.DataSource = NegAseguradoras.RecuperarListaEmpresas(tipoEmpresa.TE_CODIGO);
            Cmb_aseguradoras.ValueMember = "ASE_CODIGO";
            Cmb_aseguradoras.DisplayMember = "ASE_NOMBRE";

        }
 
        #region Metodos Privados
        private void AgregarBindigControles()
        {
            Binding CAT_CODIGO = new Binding("Text", categoriasModificada, "CAT_CODIGO", true);
            Binding CAT_NOMBRE = new Binding("Text", categoriasModificada, "CAT_NOMBRE", true);
            Binding CAT_DESCRIPCION = new Binding("Text", categoriasModificada, "CAT_DESCRIPCION", true);            
            Binding CAT_ESTADO = new Binding("Checked", categoriasModificada, "CAT_ESTADO", true);
            Binding TE_CODIGO = new Binding("SelectedValue", categoriasModificada, "TE_CODIGO", true);            
            Binding ASE_CODIGO = new Binding("SelectedValue", categoriasModificada, "ASE_CODIGO", true);
            Binding ASE_NOMBRE = new Binding("SelectedValue", categoriasModificada, "ASE_NOMBRE", true); 
            txt_nombre.DataBindings.Clear();
            txt_descripcion.DataBindings.Clear();
            //Cmb_aseguradoras.DataBindings.Clear();
            
            //Cmb_aseguradoras.DataBindings.Add(ASE_CODIGO);
            txt_nombre.DataBindings.Add(CAT_NOMBRE);
            txt_descripcion.DataBindings.Add(CAT_DESCRIPCION);  
                     
        
        }
        private void ResetearControles()
        {
            categoriasModificada = new CATEGORIAS_CONVENIOS();
            categoriasOriginal = new CATEGORIAS_CONVENIOS();
            Cmb_tempresas.Text = string.Empty; 
            Cmb_aseguradoras.Text = string.Empty;
            txt_nombre.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            
        }
        //private void AgregarError(Control control)
        //{
        //    controlErrores.SetError(control, "");
        //    //controlErrores.SetError(control, "Campo Requerido");
        //}
        private bool ValidarFormulario()
        {
            bool valido = true;
            if (categoriasModificada.CAT_CODIGO == 0)
            {
                valido = false;
            }
            if (categoriasModificada.CAT_NOMBRE == null || categoriasModificada.CAT_NOMBRE == string.Empty)
            {
                //AgregarError(txt_nombre);
                valido = false;
            }

            return valido;

        }
        private void GrabarDatos()
        {
            DialogResult resultado;
            gridcategorias.Focus();
            ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)Cmb_aseguradoras.SelectedItem;
            categoriasModificada.ASEGURADORAS_EMPRESASReference.EntityKey  = aseguradora.EntityKey ;   
            if (ValidarFormulario())
            {
                resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (categoriasModificada.EntityKey == null)
                    {
                        NegCategorias.CrearCategoria(categoriasModificada);
                    }
                    else
                    {
                        NegCategorias.GrabarCategorias(categoriasModificada, categoriasOriginal);
                    }

                    RecuperaCategorias();
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
            grpDatosPrincipales.Enabled = datosPrincipales;

        }
        private void RecuperaCategorias()
        {
            try
            {
                //categorias = NegCategorias.RecuperaCategorias().Select(c=>new{c.CAT_CODIGO,c.CAT_NOMBRE,c.CAT_DESCRIPCION,c.ASEGURADORAS_EMPRESAS.ASE_NOMBRE}).ToList()    ; 
                gridcategorias.DataSource = NegCategorias.RecuperaCategorias().Select(c => new { c.CAT_CODIGO, c.CAT_NOMBRE, c.CAT_DESCRIPCION, c.ASEGURADORAS_EMPRESAS.ASE_NOMBRE }).ToList();
                //gridcategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //gridcategorias.Columns["CAT_CODIGO"].HeaderText = "CODIGO";
                //gridcategorias.Columns["CAT_NOMBRE"].HeaderText = "NOMBRE";
                //gridcategorias.Columns["CAT_DESCRIPCION"].HeaderText = "DESCRIPCION";
                //gridcategorias.Columns["CAT_TIPO"].Visible = false;
                //gridcategorias.Columns["CAT_ESTADO"].HeaderText = "ESTADO";
                //gridcategorias.Columns["PRECIOS_POR_CATEGORIA"].Visible = false;
                //gridcategorias.Columns["PORCENTAJES_POR_SERVICVIOS"].Visible = false;
                //gridcategorias.Columns["CAT_ESTADO"].Visible = false;
                //gridcategorias.Columns["ASE_NOMBRE"].Visible = true;   
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        //private SortOrder getSortOrder(int columnIndex)
        //{
        //    if (gridcategorias.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
        //        gridcategorias.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
        //    {
        //        gridcategorias.Columns[columnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
        //        gridcategorias.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
        //        return SortOrder.Ascending;
        //    }
        //    else
        //    {
        //        gridcategorias.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
        //        return SortOrder.Descending;
        //    }
        //}
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

        

        private void gridcategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_nombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void gridcategorias_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Cmb_aseguradoras_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASEGURADORAS_EMPRESAS aseguradora =(ASEGURADORAS_EMPRESAS)Cmb_aseguradoras.SelectedItem;
            //gridcategorias.DataSource = NegCategorias.RecuperaCategorias(aseguradora.ASE_CODIGO);            
        }

    }
}