using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using System.IO;
using His.Entidades;
using Infragistics.Win.UltraWinGrid;

namespace His.Maintenance
{
    public partial class frmCatalogoCostos : Form
    {
        #region comportamiento formularo 

        public frmCatalogoCostos()
        {
            InitializeComponent();
            refreshObject();
        }

       

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void btnExcel_Click(object sender, EventArgs e)
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled=true;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
            cmbTipo.Focus();
            grid.Visible = false;
            grpDatos.Visible = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled = true;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
            cmbTipo.Focus();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (!NegMaintenance.delete("CatalogoCostos", Convert.ToInt32(txtCod.Text.Trim())))
            {
                MessageBox.Show("No ha sido posible eliminar, es probable que este asociado a otros registros.");
            }
            resetCampos();
        }



        private void resetCampos()
        {
            clsCampos();
            //refresco objetos con datos
            refreshObject();
            //habilitando controles
            grpDatos.Enabled = false;
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;


            grid.Visible = true;
            grpDatos.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            resetCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validar())   
            {
                if (Guardar())
                {
                    resetCampos();
                }
            }
        }

        #endregion

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }





        #region variables

        private void refreshObject() //actualizar grid, combos, etc 
        {
            grid.DataSource = NegMaintenance.getDataTable("CatalogoCostos");

            cmbTipo.DataSource = NegMaintenance.getDataTable("TipoCatalogoCostos");
            cmbTipo.ValueMember = "CODIGO";
            cmbTipo.DisplayMember = "TIPO";
        }

        private void clsCampos() //vaciar campos necesarios
        {
            txtCod.Text = string.Empty;
            txtDesc.Text = string.Empty;
        }



        private bool Guardar() //devolver si se guardo o no si hubo excepcion
        {

            try
            {
                int cod;
                if (txtCod.Text.Trim() == string.Empty)
                    cod = -1;
                else
                    cod = Convert.ToInt32(txtCod.Text.Trim());

                NegMaintenance.save_CatalogoCostos(cod, Convert.ToInt32(cmbTipo.SelectedValue.ToString()), txtDesc.Text.Trim());
                
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
                
            }
        }
        private bool Eliminar() //devolver si se elimino o no si hubo excepcion
        {

            try
            {
                //MessageBox.Show(cmbTipo.SelectedIndex.ToString()); //OK devuelve el ID del registro q esta en el combo
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;

            }
        }
        private bool Validar() //Validar SI, si los campos necesarios estan bien llenos, else NO
        {
            if (txtDesc.Text.Trim() == string.Empty)
                return false;
            return true;

        }

        //carga la linea del grid a los campos
        private void grid_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {

        }

        #endregion


        #region focus
        private void cmbTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter || (int)e.KeyChar == (int)Keys.Tab)
            {
                txtDesc.Focus();
            }
        }

        private void txtDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter || (int)e.KeyChar == (int)Keys.Tab)
            {
                cmbTipo.Focus();
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            frm_Ayuda ayuda = new frm_Ayuda();
            ayuda.ShowDialog();
            if (ayuda.codigo != string.Empty)
                txtDesc.Text = ayuda.codigo;
        }

        private void txtDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                txtDesc.Text = string.Empty;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F1)
            {
                frm_Ayuda ayuda = new frm_Ayuda();
                ayuda.ShowDialog();
                if (ayuda.codigo != string.Empty)
                    txtDesc.Text = ayuda.codigo;
            }
        }

        private void grid_DoubleClickRow_1(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (grid.ActiveRow.Index > -1)
                {
                    txtCod.Text = grid.ActiveRow.Cells["CODIGO"].Value.ToString();
                    txtDesc.Text = grid.ActiveRow.Cells["CATALOGO"].Value.ToString();
                    cmbTipo.Text = grid.ActiveRow.Cells["TIPO"].Value.ToString();

                    grpDatos.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnBorrar.Enabled = true;

                    grid.Visible = false;
                    grpDatos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];
            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;
            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            grid.DisplayLayout.UseFixedHeaders = true;
        }
    }
}
