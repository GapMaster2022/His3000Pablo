using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;

namespace His.Admision
{
    public partial class frmExploradorPreAdmision : Form
    {
        public  frmExploradorPreAdmision()
        {
            InitializeComponent();
            DateTime oPrimerDiaDelMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtpFiltroDesde.Value = oPrimerDiaDelMes;
            dtpFiltroHasta.Value = oUltimoDiaDelMes;
            cargaGrid();
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            cargaGrid();
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (grid.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(grid, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_AyudaPacientes form = new frm_AyudaPacientes();
            form.campoPadre = txt_historiaclinica;
            form.ShowDialog();
            form.Dispose();
            txt_historiaclinica.Text = txt_historiaclinica.Text.Trim();
        }

        private void chkHC_CheckedChanged(object sender, EventArgs e)
        {
            txt_historiaclinica.Enabled = chkHC.Checked;
            ayudaPacientes.Visible = chkHC.Checked;
            txt_historiaclinica.Text = "0";
        }

        private void txt_historiaclinica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                //MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_historiaclinica_Leave(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text == "")
                txt_historiaclinica.Text = "0";
        }
        public void cargaGrid()
        {
            grid.DataSource = NegPreadmision.consultaPreatencion(dtpFiltroDesde.Value,dtpFiltroHasta.Value,chkHC.Checked,txt_historiaclinica.Text);
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

                grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                //grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                ////grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                //grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                //grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                //bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                //bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                //bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                //grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                //grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                //grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                grid.DisplayLayout.UseFixedHeaders = true;
                //Dimension los registros
                e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
                grid.DisplayLayout.Bands[0].Columns["PROCEDIMIENTO"].Width = 200;
                grid.DisplayLayout.Bands[0].Columns["DIRECCION"].Width = 250;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
           
        }
    }
}
