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

namespace His.Dietetica
{
    public partial class ImagenExamenesRealizados : Form
    {
        public ImagenExamenesRealizados()
        {
            InitializeComponent();
            dtpDesde.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"));
            dtpHasta.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"));
            refrescar();
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

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            refrescar();
        }


        private void refrescar()
        {
            DateTime fi = dtpDesde.Value;
            DateTime ff = dtpHasta.Value;
            ff = ff.AddDays(1);
            string frango = "'" + fi.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' and '" + ff.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "'";
            grid.DataSource = NegImagen.getAgendamientos(frango, 1);
            var g = grid.DisplayLayout.Bands[0];
            g.Columns["id_imagenologia_agendamientos"].Hidden = true;
            g.Columns["CUE_CODIGO"].Hidden = true;

        }
        private void grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (grid.Rows.Count > 0)
            {
                try
                {
                    object[] x = new string[] {
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["id_imagenologia_agendamientos"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["CUE_CODIGO"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["PACIENTE"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["HC"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["ATE_CODIGO"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["ATE_FECHA_INGRESO"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["FECHA_REALIZADO"].Value),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["30X40"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["8x10"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["14x14"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["14x17"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["18x24"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["ODONT"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["DANADAS"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["ENVIADAS"].Value.ToString()),
                            Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["MEDIO_CONTRASTE"].Value.ToString()),
                            ""
                    };



                    ImagenExamenes Form = new ImagenExamenes(
                           x
                            );
                    Form.ShowDialog();
                    refrescar();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
