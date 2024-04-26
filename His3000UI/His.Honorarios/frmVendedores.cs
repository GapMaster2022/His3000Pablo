using His.Entidades;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Honorarios
{
    public partial class frm_Vendedores : Form
    {
        public frm_Vendedores()
        {
            InitializeComponent();
            ActualizarGrid();
            resetCampos();
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            grid.DisplayLayout.UseFixedHeaders = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
          
        }

        private void ActualizarGrid()
        {
            grid.DataSource = NegVendedores.getVendedores();
        }

        private void grid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                if (grid.ActiveRow.Index > -1)
                {
                    txtCod.Text = grid.ActiveRow.Cells["codigo"].Value.ToString();
                    txtNombre.Text = grid.ActiveRow.Cells["nombre"].Value.ToString();
                    txtRUC.Text = grid.ActiveRow.Cells["nro_identificacion"].Value.ToString();
                    dtpIngreso.Text = grid.ActiveRow.Cells["fec_ingreso"].Value.ToString();
                    txtComision.Text = grid.ActiveRow.Cells["comision"].Value.ToString();
                    dtpSalida.Text = grid.ActiveRow.Cells["fec_salida"].Value.ToString();
                    if (dtpSalida.Text == "01/01/1900")
                        dtpSalida.Checked = false;
                    else
                        dtpSalida.Checked = true;
                    habilitarCampos(false);
                    btnNuevo.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnBorrar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
           
        }

        private void habilitarCampos(bool x)
        {
            txtRUC.ReadOnly = !x;
            txtComision.ReadOnly = !x;
            txtNombre.ReadOnly = !x;
            dtpIngreso.Enabled = x;
            dtpSalida.Enabled = x;
        }
        private void limpiarCampos()
        {
            txtCod.Text = string.Empty;
            txtRUC.Text = string.Empty;
            txtComision.Text = string.Empty;
            txtNombre.Text = string.Empty;
            dtpIngreso.Text = DateTime.Now.ToString();
            dtpSalida.Text = "01/01/1900";
            dtpSalida.Checked = false;
        }


        private void resetCampos()
        {
            limpiarCampos();
            habilitarCampos(false);
            ActualizarGrid();
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            

        }

        

        private void btnGuardar_Click(object sender, EventArgs e)
        {
          
            
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            
        }

        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }

        private void txtComision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Console.WriteLine("Solo se permiten numeros");
                e.Handled = true;
                return;
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            
        }

        private void dtpSalida_Validating(object sender, CancelEventArgs e)
        {
            DateTime fecha;

            if (DateTime.TryParse(dtpSalida.Text, out fecha)){

                errorProvider1.SetError(dtpSalida, "la fecha es incorrecta");

                e.Cancel = true;

            }
        }

        private bool validar()
        {
            string x;
            if (txtCod.Text.Trim() == string.Empty)
            {
                x = "0";
            }
            else
            {
                x = txtCod.Text.Trim();
            }




            if (txtRUC.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor llene todos los campos.");
                txtRUC.Focus();
                return false;
            }
            else if (NegValidaciones.esCedulaValida(txtRUC.Text.Trim()) != true)
            {
                MessageBox.Show("Identificación invalida No Cumple con Formato.");
                txtRUC.Focus();
                return false;
            }
            else if (NegVendedores.existCedVendedore(txtRUC.Text.Trim(),x))
            {
                MessageBox.Show("Ya existe otro vendedor con el mismo numero de identificacion.");
                txtRUC.Focus();
                return false;
            }
            else if (txtNombre.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor llene todos los campos.");
                return false;
            }
            else if ((txtComision.Text.Replace(".", string.Empty)) == "")
            {
                MessageBox.Show("Por favor llene todos los campos.");
                return false;
            }
            return true;
        }


        private void dtpIngreso_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                dtpIngreso.Text = DateTime.Now.ToString();
            }
        }

        private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                txtComision.Focus();
            }
            if ((int)e.KeyChar == (int)Keys.Tab)
            {
                txtComision.Focus();
            }
        }

        private void txtComision_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                dtpIngreso.Focus();
            }
            if ((int)e.KeyChar == (int)Keys.Tab)
            {
                dtpIngreso.Focus();
            }
        }

        private void dtpIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                txtNombre.Focus();
            }
            if ((int)e.KeyChar == (int)Keys.Tab)
            {
                txtNombre.Focus();
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                dtpSalida.Focus();
            }
            if ((int)e.KeyChar == (int)Keys.Tab)
            {
                dtpSalida.Focus();
            }
        }

        private void dtpSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                txtRUC.Focus();
            }
            if ((int)e.KeyChar == (int)Keys.Tab)
            {
                txtRUC.Focus();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            habilitarCampos(true);
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
            txtRUC.Focus();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            habilitarCampos(true);
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
            txtRUC.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!NegVendedores.deleteVendedor(txtCod.Text.Trim()))
            {
                MessageBox.Show("No ha sido posible eliminar, es probable que este asociado a uno o varios medicos.");
            } 
            resetCampos();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                vendedor v = new vendedor();
                v.codigo = txtCod.Text.Trim();
                v.comision = Convert.ToDouble(txtComision.Text.Replace("/", string.Empty));
                v.nro_identificacion = txtRUC.Text.Trim();
                v.nombre = txtNombre.Text.Trim();
                v.fec_ingreso = Convert.ToDateTime(dtpIngreso.Text);
                if (dtpSalida.Checked)
                    v.fec_salida = dtpSalida.Text.Trim();
                else
                    v.fec_salida = "";
                try
                {
                    NegVendedores.saveVendedor(v);
                    MessageBox.Show("Se guardo exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error inesperado, favor comuniquese con sistemas.\n" + "  Error:" + ex);
                    throw;
                }

                resetCampos();
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(grid, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            resetCampos();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
