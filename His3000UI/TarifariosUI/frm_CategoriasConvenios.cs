using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using Core.Entidades;
using His.General;
using Recursos;
using TarifariosUI.Properties;

namespace TarifariosUI
{
    public partial class FrmCategoriasConvenios : Form
    {
        #region Variables

            private CATEGORIAS_CONVENIOS _conveniosOriginal;
            private CATEGORIAS_CONVENIOS _conveniosModificada;
            private List<CATEGORIAS_CONVENIOS> _convenios;
            private List<ASEGURADORAS_EMPRESAS> _aseguradoras;
            public int columnabuscada;
            private bool _nuevo;

        #endregion

        #region Constructores

            public FrmCategoriasConvenios()
            {
                InitializeComponent();
                InicializarVariables();
                CargarRecursos();
            }
            /// <summary>
            /// Metodo que inicializa las variables
            /// </summary>
            private void InicializarVariables()
            {
                _conveniosOriginal = new CATEGORIAS_CONVENIOS();
                _conveniosModificada = new CATEGORIAS_CONVENIOS();
                _convenios = new List<CATEGORIAS_CONVENIOS>();
                _aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
            }

            private void CargarRecursos()
            {
                btnNuevo.Image = Archivo.imgBtnAdd;
                btnActualizar.Image = Archivo.imgBtnRefresh;
                btnEliminar.Image = Archivo.imgBtnDelete;
                btnGuardar.Image = Archivo.imgBtnFloppy;
                btnCancelar.Image = Archivo.imgBtnStop;
                btnCerrar.Image = Archivo.imgBtnSalir32;
            }


        #endregion

        #region Metodos Privados

            private void AgregarBindigControles()
            {
                var catCodigo = new Binding("Text", _conveniosModificada, "CAT_CODIGO", true);
                var catNombre = new Binding("Text", _conveniosModificada, "CAT_NOMBRE", true);
                var catDescripcion = new Binding("Text", _conveniosModificada, "CAT_DESCRIPCION", true);
                var catFechaInicio = new Binding("Text", _conveniosModificada, "CAT_FECHA_INICIO", true);
                var catFechaFin = new Binding("Text", _conveniosModificada, "CAT_FECHA_FIN", true);
                var catTipoPrecio = new Binding("Text", _conveniosModificada, "CAT_TIPO_PRECIO", true);
                var catPorDescuento = new Binding("Text", _conveniosModificada, "CAT_POR_DESCUENTO", true);

                txt_codigo.DataBindings.Clear();
                txt_nombre.DataBindings.Clear();
                txt_descripcion.DataBindings.Clear();
                dtp_Finicio.DataBindings.Clear();
                dtp_Ffin.DataBindings.Clear();
                nTxtPorcentaje.DataBindings.Clear();
                cboTipoPrecio.DataBindings.Clear();

                txt_codigo.DataBindings.Add(catCodigo);
                txt_nombre.DataBindings.Add(catNombre);
                txt_descripcion.DataBindings.Add(catDescripcion);
                dtp_Finicio.DataBindings.Add(catFechaInicio);
                dtp_Ffin.DataBindings.Add(catFechaFin);
                nTxtPorcentaje.DataBindings.Add(catPorDescuento);
                cboTipoPrecio.DataBindings.Add(catTipoPrecio);
            }

            private void ResetearControles()
            {
                _conveniosModificada = new CATEGORIAS_CONVENIOS();
                _conveniosOriginal = new CATEGORIAS_CONVENIOS();

                txt_codigo.Text = string.Empty;
                txt_nombre.Text = string.Empty;
                txt_descripcion.Text = string.Empty;
                dtp_Finicio.Text = string.Empty;
                cboTipoPrecio.SelectedIndex = 0;
                nTxtPorcentaje.Value = 0;
            }

            private void AgregarError(Control control)
            {
                controlErrores.SetError(control, "Campo Requerido");
            }
            private bool ValidarFormulario()
            {
                bool valido = true;
                if (txt_codigo.Text.Trim() == String.Empty)
                {
                    AgregarError(txt_codigo);
                    valido = false;
                }
                if (string.IsNullOrEmpty(txt_nombre.Text.Trim()))
                {
                    AgregarError(txt_nombre);
                    valido = false;
                }
                if (txt_descripcion.Text.Trim() == String.Empty)
                {
                    AgregarError(txt_descripcion);
                    valido = false;
                }
                if(dtp_Ffin.Value<dtp_Finicio.Value)
                {
                    AgregarError(dtp_Ffin);
                    valido = false;
                }
                return valido;

            }
            private void GrabarDatos()
            {
                if (ValidarFormulario() == false)
                {
                    MessageBox.Show(Archivo.Mensaje_General_Validacion_Campos,Resources.Mensaje_Tipo_Advertencia,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    return;
                }
                gridConvenios.Focus();
                var aseguradoras = (ASEGURADORAS_EMPRESAS)CmbAseguradoras.SelectedItem;
                _conveniosModificada.CAT_CODIGO = Convert.ToInt16(txt_codigo.Text);
                _conveniosModificada.CAT_NOMBRE = txt_nombre.Text;
                _conveniosModificada.CAT_DESCRIPCION = txt_descripcion.Text;
                _conveniosModificada.ASEGURADORAS_EMPRESASReference.EntityKey = aseguradoras.EntityKey;
                _conveniosModificada.CAT_FECHA_INICIO = dtp_Finicio.Value;
                _conveniosModificada.CAT_FECHA_FIN = dtp_Ffin.Value;
                _conveniosModificada.CAT_POR_DESCUENTO = Convert.ToInt16(nTxtPorcentaje.Text.Replace("_",""));
                _conveniosModificada.CAT_TIPO_PRECIO = cboTipoPrecio.SelectedValue.ToString();

                DialogResult resultado = MessageBox.Show(Resources.Mensaje_General_Grabar_Confirmacion, String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (_nuevo)
                    {
                        NegCategorias.CrearCategoria(_conveniosModificada);
                        NegAseguradoras.actualizarConvenio(aseguradoras, true);
                    }
                    else
                    {

                        NegCategorias.GrabarCategorias(_conveniosModificada, _conveniosOriginal);
                    }

                    RecuperaConvenios();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false);
                    MessageBox.Show(Archivo.Mensaje_General_Grabar_Exito, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            private void RecuperaConvenios()
            {
                _convenios = NegCategorias.RecuperaCategorias();
                var listaConvenios = _convenios.Select(c => new { c.CAT_CODIGO, c.CAT_NOMBRE, c.CAT_DESCRIPCION, c.ASEGURADORAS_EMPRESAS.ASE_CODIGO, c.ASEGURADORAS_EMPRESAS.ASE_NOMBRE, c.CAT_FECHA_INICIO, c.CAT_FECHA_FIN, c.CAT_TIPO_PRECIO, c.CAT_POR_DESCUENTO }).ToList();
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
                gridConvenios.Columns["CAT_TIPO_PRECIO"].HeaderText = "PRECIO APLI.";
                gridConvenios.Columns["CAT_POR_DESCUENTO"].HeaderText = "POR. DESCUENTO";
            }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            var form = new Form();
            var label = new Label();
            var textBox = new TextBox();
            var buttonOk = new Button();
            var buttonCancel = new Button();

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

            var dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        #endregion

        #region Eventos

        private void CargarControles()
        {
            _aseguradoras = NegAseguradoras.ListaEmpresas();
            CmbAseguradoras.DataSource = _aseguradoras;
            CmbAseguradoras.ValueMember = "ASE_CODIGO";
            CmbAseguradoras.DisplayMember = "ASE_NOMBRE";
            var lstTipoPrecio = new List<string> {"COSTO", "PVP", "PVE"};
            cboTipoPrecio.DataSource = lstTipoPrecio;
            cboTipoPrecio.SelectedIndex = 0;
            nTxtPorcentaje.Value = 0;
        }

        private void FrmCategoriasConveniosLoad(object sender, EventArgs e)
        {
            try
            {
                CargarControles();
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
                _nuevo = false;
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
                if (gridConvenios.CurrentRow != null)
                {
                    _conveniosModificada.CAT_CODIGO = (Int16)gridConvenios.CurrentRow.Cells[0].Value;
                    _conveniosModificada.CAT_NOMBRE = gridConvenios.CurrentRow.Cells[1].Value.ToString();
                    _conveniosModificada.CAT_DESCRIPCION = gridConvenios.CurrentRow.Cells[2].Value.ToString();
                    _conveniosModificada.CAT_FECHA_INICIO = Convert.ToDateTime(gridConvenios.CurrentRow.Cells[5].Value);
                    _conveniosModificada.CAT_FECHA_FIN = Convert.ToDateTime(gridConvenios.CurrentRow.Cells[6].Value);
                    _conveniosModificada.CAT_TIPO_PRECIO = gridConvenios.CurrentRow.Cells["CAT_TIPO_PRECIO"].Value!=null?gridConvenios.CurrentRow.Cells["CAT_TIPO_PRECIO"].Value.ToString():String.Empty;
                    _conveniosModificada.CAT_POR_DESCUENTO =  gridConvenios.CurrentRow.Cells["CAT_POR_DESCUENTO"].Value!=null?Convert.ToInt16(gridConvenios.CurrentRow.Cells["CAT_POR_DESCUENTO"].Value):(short?)0;
                    int codAseguradora = (Int16)gridConvenios.CurrentRow.Cells[3].Value;
                    CmbAseguradoras.SelectedItem = _aseguradoras.FirstOrDefault(c => c.ASE_CODIGO == codAseguradora);
                    AgregarBindigControles();
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
                var aseguradoras = (ASEGURADORAS_EMPRESAS)CmbAseguradoras.SelectedItem;
                DialogResult resultado = MessageBox.Show(Resources.Mensaje_General_Eliminar_Advertencia, " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    NegCategorias.EliminarCategorias(_conveniosModificada.ClonarEntidad());
                    if (NegCategorias.ListaCategorias(aseguradoras.ASE_CODIGO).Count < 1)
                        NegAseguradoras.actualizarConvenio(aseguradoras, false);
                    RecuperaConvenios();
                    ResetearControles();
                    HalitarControles(false, false, false, false, false, true, false);
                    MessageBox.Show(Archivo.Mensaje_General_Eliminar_Exito, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                _nuevo = true;
                HalitarControles(true, true, false, true, false, false, true);
                _conveniosOriginal = new CATEGORIAS_CONVENIOS();
                _conveniosModificada = new CATEGORIAS_CONVENIOS();

                ResetearControles();
                _conveniosModificada.CAT_CODIGO = Int16.Parse((NegCategorias.RecuperaMaximoCategorias() + 1).ToString());
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
                    var cat_conv = _convenios.Select(c => new { c.CAT_CODIGO, c.CAT_NOMBRE, c.CAT_DESCRIPCION, c.ASEGURADORAS_EMPRESAS.ASE_CODIGO, c.ASEGURADORAS_EMPRESAS.ASE_NOMBRE, c.CAT_FECHA_INICIO, c.CAT_FECHA_FIN, c.CAT_TIPO_PRECIO,c.CAT_POR_DESCUENTO }).ToList();
                    cat_conv = cat_conv.OrdenarPorPropiedad(columna).ToList();
                    gridConvenios.DataSource = cat_conv;

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
                string value = "";
                if (InputBox("Usuarios", gridConvenios.Columns[columnabuscada].HeaderText, ref value) == DialogResult.OK)
                {
                    string columna = value.ToUpper();
                    for (int i = 0; i <= gridConvenios.RowCount - 1; i++)
                    {
                        if (gridConvenios.Rows[i].Cells[columnabuscada].Value.ToString().Contains(columna))
                            gridConvenios.CurrentCell = gridConvenios.Rows[i].Cells[columnabuscada];
                    }
                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
            }
        }

        #endregion


    }
}
