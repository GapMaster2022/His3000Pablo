using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class frm_AgregarProductosNew : Form
    {
        public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano());  //por defecto es la 12 de quirofano
        public Int64 pci_codigo = 0; //maneja el codigo del procedimiento para creacion, modificacion y eliminacion
        public bool EditarProcedimiento = false;
        public bool editarProducto = false;
        NegQuirofano Quirofano = new NegQuirofano();

        public frm_AgregarProductosNew()
        {
            InitializeComponent();
        }
        public frm_AgregarProductosNew(int bodega)
        {
            InitializeComponent();
            this.bodega = bodega;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void HabilitarBotones(bool nuevo, bool editar, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnEditar.Enabled = editar;
            btnCancelar.Enabled = cancelar;
        }

        public void ListarProductos()
        {
            cmbProducto.DataSource = NegQuirofano.ListarProductos(bodega);
            cmbProducto.DisplayMember = "despro";
            cmbProducto.ValueMember = "codpro";
        }

        public void HabilitarNuevo(bool nuevoProce, bool eliminarProce, bool bloquear, bool añadir, bool eliminarProdu)
        {
            btnGuardarProc.Enabled = nuevoProce;
            btnEliminar.Enabled = eliminarProdu;
            btnEliminarProce.Enabled = eliminarProce;
            txtprocedimiento.Enabled = nuevoProce;
            txtcantidad.Enabled = bloquear;
            txtorden.Enabled = bloquear;
            cmbProducto.Enabled = bloquear;
            btnAñadir.Enabled = añadir;
        }
        private void frm_AgregarProductosNew_Load(object sender, EventArgs e)
        {
            HabilitarBotones(true, true, false);
            ListarProcedimientos();
            HabilitarNuevo(false, false, false, false, false);
        }
        public void ListarProcedimientos()
        {
            dsProcedimiento1 = NegQuirofano.listarProcedimiento(bodega);
            ultraGridProcedimiento.DataSource = dsProcedimiento1.Procedimiento;
        }

        private void ultraTextEditor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void ultraTextEditor3_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, false, true);
            HabilitarNuevo(true, false, false, false, false);
            ListarProductos();
            txtprocedimiento.Focus();
        }

        private void btnGuardarProc_Click(object sender, EventArgs e)
        {
            CrearEditarProcedimiento();
        }

        private void cmbProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                errores.Clear();
                string valor = "";
                if (cmbProducto.SelectedItem == null)
                {
                    valor = "";
                }
                else
                {
                    valor = cmbProducto.SelectedItem.DataValue.ToString();
                }
                if (valor != "")
                {
                    txtcantidad.Text = "1";
                    txtcantidad.Focus();
                }
                else
                {
                    errores.SetError(cmbProducto, "Debe elegir el producto.");
                    cmbProducto.Focus();
                }
            }
        }

        private void txtcantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                errores.Clear();
                if (txtcantidad.Text.Trim() == "")
                {
                    errores.SetError(txtcantidad, "Debe ingresar la cantidad.");
                    txtcantidad.Text = "1";
                    txtcantidad.Focus();
                }
                else
                {
                    if (!editarProducto)
                    {
                        int orden = NegQuirofano.UltimoOrdenProcedimiento(Convert.ToInt32(pci_codigo));
                        txtorden.Text = Convert.ToString(orden + 1);
                    }
                    txtorden.Focus();
                }
            }
        }
        public void CreaEditarProducto()
        {
            if (editarProducto)
            {
                if (NegQuirofano.ActualizaProce_Producto(pci_codigo, cmbProducto.SelectedItem.DataValue.ToString(),
                    Convert.ToInt32(txtorden.Text.Trim()), Convert.ToInt32(txtcantidad.Text.Trim())))
                {
                    MessageBox.Show("El producto ha sido actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarBotones(true, true, false);
                    HabilitarNuevo(false, false, false, false, false);
                    ListarProcedimientos();
                    LimpiarCampos();
                    editarProducto = false;
                }
            }
            //guardo el producto al procedimiento.
            else if (Quirofano.ProductoRepetido(cmbProducto.SelectedItem.DataValue.ToString(), pci_codigo.ToString()) != cmbProducto.SelectedItem.DataValue.ToString())
            {
                Quirofano.AgregarProcedimiento(txtorden.Text, cmbProducto.SelectedItem.DataValue.ToString(), pci_codigo.ToString(), txtcantidad.Text);
                ListarProcedimientos();
                LimpiarProducto();
                cmbProducto.Focus();
            }
            else
                MessageBox.Show("Producto ya esta agregado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void txtorden_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CreaEditarProducto();
            }
        }
        public void CrearEditarProcedimiento()
        {
            errores.Clear();
            if (txtprocedimiento.Text.Trim() != "")
            {
                if (EditarProcedimiento) //modo edicion de procedimiento
                {
                    if (!NegQuirofano.nombreProcedimiento(txtprocedimiento.Text.Trim(), bodega))
                    {
                        if (NegQuirofano.ActualizaProcedimiento(pci_codigo, txtprocedimiento.Text.Trim()))
                        {
                            HabilitarBotones(true, true, false);
                            LimpiarCampos();
                            ListarProcedimientos();
                            EditarProcedimiento = false;
                        }
                        else
                            MessageBox.Show("Algo ocurrio al actualizar procedimiento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("El procedimiento ya existe.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else //modo de creacion de procedimiento
                {
                    if (!NegQuirofano.nombreProcedimiento(txtprocedimiento.Text.Trim(), bodega))
                    {
                        pci_codigo = NegQuirofano.CrearProcedimiento(txtprocedimiento.Text.Trim(), bodega);
                        if (pci_codigo != 0)
                        {
                            HabilitarNuevo(false, false, true, true, false);
                            cmbProducto.Focus();
                        }
                        else
                            MessageBox.Show("Algo ocurrio al crear procedimiento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("El procedimiento ya existe.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                errores.SetError(txtprocedimiento, "Debe agregar el nombre del procedimiento");
                txtprocedimiento.Focus();
                return;
            }
        }
        private void txtprocedimiento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CrearEditarProcedimiento();
            }
        }
        public void LimpiarProducto()
        {
            cmbProducto.SelectedIndex = -1;
            txtcantidad.Text = "";
            txtorden.Text = "";
        }
        private void btnAñadir_Click(object sender, EventArgs e)
        {
            CreaEditarProducto();
        }
        public void LimpiarCampos()
        {
            txtprocedimiento.Text = "";
            LimpiarProducto();
            HabilitarBotones(true, true, true);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!btnNuevo.Enabled)
            {
                if (MessageBox.Show("¿Está seguro de cancelar?", "HIS3000", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    LimpiarCampos();
                    HabilitarBotones(true, true, false);
                    HabilitarNuevo(false, false, false, false, false);
                }
            }
            else
            {
                LimpiarCampos();
                HabilitarBotones(true, true, false);
                HabilitarNuevo(false, false, false, false, false);
            }
        }

        private void ultraGridProcedimiento_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridProcedimiento.DisplayLayout.Bands[0];

            //ultraGridProcedimiento.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGridProcedimiento.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridProcedimiento.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridProcedimiento.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridProcedimiento.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridProcedimiento.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridProcedimiento.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            ultraGridProcedimiento.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridProcedimiento.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridProcedimiento.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridProcedimiento.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridProcedimiento.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGridProcedimiento.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            ultraGridProcedimiento.DisplayLayout.Bands[0].Columns[0].Width = 60;
            ultraGridProcedimiento.DisplayLayout.Bands[0].Columns["Procedimiento"].Width = 300;

            //Ocultar columnas, que son fundamentales al levantar informacion
            //ultraGridProcedimiento.DisplayLayout.Bands[0].Columns[1].Hidden = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (ultraGridProcedimiento.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = ultraGridProcedimiento.ActiveRow;
                if (MessageBox.Show("¿Está seguro de editar ó eliminar: \n\r"
                    + fila.Cells["Procedimiento"].Value.ToString() + "?", "HIS3000",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    HabilitarNuevo(true, true, false, false, false);
                    EditarProcedimiento = true;
                    HabilitarBotones(false, false, true);
                    pci_codigo = Convert.ToInt32(fila.Cells["Codigo"].Value.ToString());
                    txtprocedimiento.Text = fila.Cells["Procedimiento"].Value.ToString();
                    txtprocedimiento.Focus();
                }
            }
            else
            {
                MessageBox.Show("Debe elegir un procedimiento.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnEliminarProce_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de eliminar: " + txtprocedimiento.Text, "HIS3000",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (NegQuirofano.EliminaProcedimiento(pci_codigo))
                {
                    MessageBox.Show("Procedimiento eliminado correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarBotones(true, true, false);
                    ListarProcedimientos();
                    LimpiarCampos();
                }
                else
                    MessageBox.Show("Algo ocurrio al eliminar procedimiento, revise si este procedimiento no ha sido utilizado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridProcedimiento_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            foreach (UltraGridRow item in ultraGridProcedimiento.Rows)
            {
                if (item.Cells["Codigo"].Value.ToString() == e.Cell.Row.Cells["Codigo"].Value.ToString())
                {
                    try
                    {
                        editarProducto = true;
                        ListarProductos();
                        cmbProducto.Value = e.Cell.Row.Cells["ID"].Value.ToString();
                        txtcantidad.Text = e.Cell.Row.Cells["Cantidad"].Value.ToString();
                        txtorden.Text = e.Cell.Row.Cells["Orden"].Value.ToString();
                        HabilitarBotones(false, false, true);
                        HabilitarNuevo(false, false, true, true, true);
                        cmbProducto.Enabled = false;
                        txtprocedimiento.Text = item.Cells["Procedimiento"].Value.ToString();
                        pci_codigo = Convert.ToInt64(item.Cells["Codigo"].Value.ToString());
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Seleccione un producto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //throw;
                    }
                }
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de eliminar: " + cmbProducto.Text, "HIS3000",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (NegQuirofano.EliminarProducto_Proce(pci_codigo, cmbProducto.SelectedItem.DataValue.ToString()))
                {
                    MessageBox.Show("Producto eliminado correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarBotones(true, true, false);
                    HabilitarNuevo(false, false, false, false, false);
                    LimpiarCampos();
                    ListarProcedimientos();
                }
                else
                    MessageBox.Show("Algo ocurrio al eliminar producto, intente nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGridProcedimiento_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point mousepoint = new Point(e.X, e.Y);
                contextMenuStrip1.Show(ultraGridProcedimiento, mousepoint);
            }
        }

        private void nuevoProcedimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UltraGridRow Fila = ultraGridProcedimiento.ActiveRow;
            if (ultraGridProcedimiento.Selected.Rows.Count == 1)
            {
                editarProducto = false;
                ListarProductos();
                HabilitarBotones(false, false, true);
                HabilitarNuevo(false, false, true, true, true);
                txtprocedimiento.Text = Fila.Cells["Procedimiento"].Value.ToString();
                pci_codigo = Convert.ToInt64(Fila.Cells["Codigo"].Value.ToString());
                LimpiarProducto();
            }
            else
            {
                MessageBox.Show("Debe elegir un Procedimienton para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
