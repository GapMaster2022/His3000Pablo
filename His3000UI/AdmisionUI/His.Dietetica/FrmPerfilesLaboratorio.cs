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
using His.Entidades;
using His.Formulario;

namespace His.Dietetica
{
    public partial class FrmPerfilesLaboratorio : Form
    {
        public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano());  //por defecto es la 12 de quirofano
        public Int32 PL_CODIGO = 0; //maneja el codigo del procedimiento para creacion, modificacion y eliminacion
        public bool EditarProcedimiento = false;
        public bool editarProducto = false;
        public Int32 codpro = 0; //PARA EDITAR LOS PRODUCTOS
        public Int32 coddep = 0; //PARA EDITAR LOS SECCION
        Int32 ultCodigo;
        PERFILES_LABORATORIO perLab = null;
        PERFILES_PRODUCTOS perPro = null;
        public FrmPerfilesLaboratorio()
        {
            InitializeComponent();
            ListarProcedimientos();
            ListarProductos();
            HabilitarBotones(true, true, false);
            HabilitarNuevo(false, false, false, false, false);
        }

        private void btnGuardarProc_Click_1(object sender, EventArgs e)
        {
            CrearEditarPerfil();
        }
        public void HabilitarBotones(bool nuevo, bool editar, bool cancelar)
        {
            btnNuevo.Enabled = nuevo;
            btnEditar.Enabled = editar;
            btnCancelar.Enabled = cancelar;
        }
        public void ListarProductos()
        {
            cmbProducto.DataSource = NegLaboratorio.CargaDepartamento();
            cmbProducto.DisplayMember = "desdep";
            cmbProducto.ValueMember = "coddep";
        }
        public void HabilitarNuevo(bool nuevoProce, bool eliminarProce, bool bloquear, bool añadir, bool eliminarProdu)
        {
            btnGuardarProc.Enabled = nuevoProce;
            btnEliminar.Enabled = eliminarProdu;
            btnEliminarProce.Enabled = eliminarProce;
            txtprocedimiento.Enabled = nuevoProce;
            cmbProducto.Enabled = bloquear;
            cmbPedido.Enabled = bloquear;
            btnAñadir.Enabled = añadir;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, false, true);
            HabilitarNuevo(true, false, false, false, false);
            ListarProductos();
            txtprocedimiento.Focus();
            PL_CODIGO = 0;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (ultraGridProcedimiento.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = ultraGridProcedimiento.ActiveRow;
                His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("PerLab");
                usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                usuario.ShowDialog();
                if (!usuario.aceptado)
                    return;
                if (Convert.ToInt32(fila.Cells["ID_USUARIO"].Value) == usuario.usuarioActual)
                {
                    if (MessageBox.Show("¿Está seguro de editar ó eliminar: \n\r"
                    + fila.Cells["PERFIL"].Value.ToString() + "?", "HIS3000",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        HabilitarNuevo(true, true, false, false, false);
                        EditarProcedimiento = true;
                        HabilitarBotones(false, false, true);
                        PL_CODIGO = Convert.ToInt32(fila.Cells["CODIGO"].Value.ToString());
                        txtprocedimiento.Text = fila.Cells["PERFIL"].Value.ToString();
                        txtprocedimiento.Focus();
                    }
                }
                else
                    MessageBox.Show("El perfil solo puede editar el usuario que lo creo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                MessageBox.Show("Debe elegir un perfil.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminarProce_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de eliminar: " + txtprocedimiento.Text, "HIS3000",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (NegLaboratorio.EliminarPerfil(PL_CODIGO))
                {
                    MessageBox.Show("Perfil eliminado correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HabilitarBotones(true, true, false);
                    ListarProcedimientos();
                    txtprocedimiento.Enabled = false;
                    txtprocedimiento.Text = "";
                }
                else
                    MessageBox.Show("Algo ocurrio al eliminar perfil, revise si este perfil no ha sido utilizado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            CreaEditarProducto();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de eliminar: " + cmbPedido.Text, "HIS3000",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (NegLaboratorio.EliminarPerfil_producto(PL_CODIGO, codpro))
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
        public void CrearEditarPerfil()
        {
            perLab = new PERFILES_LABORATORIO();
            errores.Clear();
            if (txtprocedimiento.Text.Trim() != "")
            {
                if (EditarProcedimiento) //modo edicion de procedimiento
                {
                    if (!NegLaboratorio.RepProcedimiento(txtprocedimiento.Text.Trim()))
                    {
                        if (NegLaboratorio.ActualizarPerfil(PL_CODIGO, txtprocedimiento.Text.Trim()))
                        {
                            LimpiarCampos();
                            HabilitarBotones(true, true, false);
                            HabilitarNuevo(false, false, false, false, false);
                            ListarProcedimientos();
                            EditarProcedimiento = false;
                            MessageBox.Show("Perfil editado con exito ", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Algo ocurrio al actualizar perfil", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("El perfil ya existe.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else //modo de creacion de procedimiento
                {
                    His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("PerLab");
                    usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    usuario.ShowDialog();
                    if (!usuario.aceptado)
                        return;
                    if (!NegLaboratorio.RepProcedimiento(txtprocedimiento.Text.Trim()))
                    {
                        perLab.ID_USUARIO = usuario.usuarioActual;
                        perLab.PL_PERFIL = txtprocedimiento.Text.Trim();
                        if (NegLaboratorio.CreaPerfil(perLab))
                        {
                            ultCodigo = NegLaboratorio.UltimoCodigo();
                            HabilitarNuevo(false, false, true, true, false);
                            cmbProducto.Focus();
                        }
                        else
                            MessageBox.Show("Algo ocurrio al crear perfil", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("El perfil ya existe.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                errores.SetError(txtprocedimiento, "Debe agregar el nombre del perfil");
                txtprocedimiento.Focus();
                return;
            }
        }
        public void ListarProcedimientos()
        {
            //dsProcedimiento1 = NegLaboratorio.lProcedimientos();
            //ultraGridProcedimiento.DataSource = dsProcedimiento1.Perfil;
            ultraGridProcedimiento.DataSource = NegLaboratorio.listarPerfiles();
        }
        public void LimpiarCampos()
        {
            txtprocedimiento.Text = "";
            LimpiarProducto();
            HabilitarBotones(true, true, true);
        }
        public void LimpiarProducto()
        {
            cmbPedido.SelectedIndex = -1;
        }

        private void cmbProducto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProducto.SelectedText != "")
                {
                    if (cmbProducto.SelectedText == "HEMATOLOGÍA")
                    {
                        cmbPedido.DataSource = NegLaboratorio.listarProductos(6);
                        cmbPedido.DisplayMember = "EXAMEN";
                        cmbPedido.ValueMember = "COD_PRODUCTO";
                    }
                    if (cmbProducto.SelectedText == "UROANALISIS")
                    {
                        cmbPedido.DataSource = NegLaboratorio.listarProductos(7);
                        cmbPedido.DisplayMember = "EXAMEN";
                        cmbPedido.ValueMember = "COD_PRODUCTO";
                    }
                    if (cmbProducto.SelectedText == "COPROLOGICO")
                    {
                        cmbPedido.DataSource = NegLaboratorio.listarProductos(8);
                        cmbPedido.DisplayMember = "EXAMEN";
                        cmbPedido.ValueMember = "COD_PRODUCTO";
                    }
                    if (cmbProducto.SelectedText == "QUIMICA SANGUINEA")
                    {
                        cmbPedido.DataSource = NegLaboratorio.listarProductos(9);
                        cmbPedido.DisplayMember = "EXAMEN";
                        cmbPedido.ValueMember = "COD_PRODUCTO";
                    }
                    if (cmbProducto.SelectedText == "SEROLOGÍA")
                    {
                        cmbPedido.DataSource = NegLaboratorio.listarProductos(10);
                        cmbPedido.DisplayMember = "EXAMEN";
                        cmbPedido.ValueMember = "COD_PRODUCTO";
                    }
                    if (cmbProducto.SelectedText == "BACTERIOLOGÍA")
                    {
                        cmbPedido.DataSource = NegLaboratorio.listarProductos(11);
                        cmbPedido.DisplayMember = "EXAMEN";
                        cmbPedido.ValueMember = "COD_PRODUCTO";
                    }
                    if (cmbProducto.SelectedText == "OTROS")
                    {
                        cmbPedido.DataSource = NegLaboratorio.listarProductos(12);
                        cmbPedido.DisplayMember = "EXAMEN";
                        cmbPedido.ValueMember = "COD_PRODUCTO";
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void CreaEditarProducto()
        {
            if (editarProducto)
            {
                if ((MessageBox.Show("Esta seguro de editar el producto", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    if (NegLaboratorio.ActualizarPerfil_Producto(PL_CODIGO, codpro, Convert.ToInt32(cmbProducto.SelectedItem.DataValue.ToString()), Convert.ToInt32(cmbPedido.SelectedItem.DataValue.ToString())))
                    {
                        MessageBox.Show("El producto ha sido actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HabilitarBotones(true, true, false);
                        HabilitarNuevo(false, false, false, false, false);
                        ListarProcedimientos();
                        LimpiarCampos();
                        editarProducto = false;
                    }
                }
            }
            //guardo el producto al procedimiento.
            else if (cmbProducto.Text != "")
            {
                if (cmbPedido.Text != "")
                {
                    if (PL_CODIGO != 0)
                    {
                        if (NegLaboratorio.RepProducto(PL_CODIGO, Convert.ToInt64(cmbPedido.SelectedItem.DataValue.ToString())) != cmbPedido.SelectedItem.DataValue.ToString())
                        {
                            perPro = new PERFILES_PRODUCTOS();
                            perPro.PL_CODIGO = PL_CODIGO;
                            perPro.coddep = Convert.ToInt32(cmbProducto.SelectedItem.DataValue.ToString());
                            perPro.codrpo = Convert.ToInt32(cmbPedido.SelectedItem.DataValue.ToString());
                            if (NegLaboratorio.AgregarProducto(perPro))
                            {
                                ListarProcedimientos();
                                LimpiarProducto();
                                cmbPedido.Focus();
                            }
                            else
                                MessageBox.Show("Algo ocurrio al agregar el producto ", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                            MessageBox.Show("Producto ya esta agregado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (NegLaboratorio.RepProducto(ultCodigo, Convert.ToInt64(cmbPedido.SelectedItem.DataValue.ToString())) != cmbPedido.SelectedItem.DataValue.ToString())
                        {
                            perPro = new PERFILES_PRODUCTOS();
                            perPro.PL_CODIGO = ultCodigo;
                            perPro.coddep = Convert.ToInt32(cmbProducto.SelectedItem.DataValue.ToString());
                            perPro.codrpo = Convert.ToInt32(cmbPedido.SelectedItem.DataValue.ToString());
                            if (NegLaboratorio.AgregarProducto(perPro))
                            {
                                ListarProcedimientos();
                                LimpiarProducto();
                                cmbPedido.Focus();
                            }
                            else
                                MessageBox.Show("Algo ocurrio al agregar el producto ", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                            MessageBox.Show("Producto ya esta agregado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                    MessageBox.Show("Agrege una Producto ", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Agrege una seccion ", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void ultraGridProcedimiento_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            foreach (UltraGridRow item in ultraGridProcedimiento.Rows)
            {
                if (item.Cells["CODIGO"].Value.ToString() == e.Cell.Row.Cells["CODIGO"].Value.ToString())
                {
                    try
                    {
                        His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios("PerLab");
                        usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        usuario.ShowDialog();
                        if (!usuario.aceptado)
                            return;
                        if (Convert.ToInt32(e.Cell.Row.Cells["ID_USUARIO"].Value) == usuario.usuarioActual)
                        {
                            editarProducto = true;
                            ListarProductos();
                            txtprocedimiento.Text = e.Cell.Row.Cells["PERFIL"].Value.ToString();
                            cmbPedido.Value = e.Cell.Row.Cells["DESCRIPCION"].Value.ToString();
                            cmbProducto.Value = e.Cell.Row.Cells["DIVISION"].Value.ToString();
                            codpro = Convert.ToInt32(e.Cell.Row.Cells["codpro"].Value.ToString());
                            coddep = Convert.ToInt32(e.Cell.Row.Cells["coddep"].Value.ToString());
                            PL_CODIGO = Convert.ToInt32(e.Cell.Row.Cells["CODIGO"].Value.ToString());
                            HabilitarBotones(false, false, true);
                            HabilitarNuevo(false, false, true, true, true);
                            cmbProducto.Enabled = false;
                            cmbPedido.Enabled = true;
                            btnAñadir.Enabled = false;
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Solo puede editar el usuario que lo creo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Seleccione un producto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //throw;
                    }
                }
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
                txtprocedimiento.Text = Fila.Cells["PERFIL"].Value.ToString();
                PL_CODIGO = Convert.ToInt32(Fila.Cells["CODIGO"].Value.ToString());
                cmbProducto.Enabled = true;
                cmbProducto.SelectedIndex = -1;
                cmbPedido.Text = "";
                btnEliminar.Enabled = false;
                LimpiarProducto();
            }
            else
            {
                MessageBox.Show("Debe elegir un Perfil para continuar...", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void ultraGridProcedimiento_InitializeLayout(object sender, InitializeLayoutEventArgs e)
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
            //Dimenciono automaticamenet al contenido del texto
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            //Dimension los registros
            //ultraGridProcedimiento.DisplayLayout.Bands[0].Columns[0].Width = 60;
            //ultraGridProcedimiento.DisplayLayout.Bands[0].Columns["PERFIL"].Width = 250;
            //ultraGridProcedimiento.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 340;
            //Ocultar Columnas
            ultraGridProcedimiento.DisplayLayout.Bands[0].Columns["ID_USUARIO"].Hidden = true;
        }
    }
}
