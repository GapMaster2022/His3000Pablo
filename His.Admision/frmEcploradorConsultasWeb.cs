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
    public partial class frmEcploradorConsultasWeb : Form
    {
        public frmEcploradorConsultasWeb()
        {
            InitializeComponent();
            DateTime oPrimerDiaDelMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtpFiltroDesde.Value = oPrimerDiaDelMes;
            dtpFiltroHasta.Value = oUltimoDiaDelMes;
            cargarGrid();
        }
        public void cargarGrid()
        {
            DataTable consulta = NegMaintenance.consultasWebSp(dtpFiltroDesde.Value, dtpFiltroHasta.Value);
            utrgCosultasWeb.DataSource = consulta;
            label2.Text = Convert.ToString(consulta.Rows.Count);
        }
        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (utrgCosultasWeb.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(utrgCosultasWeb, PathExcel);
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

        private void utrgCosultasWeb_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = utrgCosultasWeb.DisplayLayout.Bands[0];

            utrgCosultasWeb.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            utrgCosultasWeb.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            utrgCosultasWeb.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            utrgCosultasWeb.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            utrgCosultasWeb.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            utrgCosultasWeb.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            utrgCosultasWeb.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            utrgCosultasWeb.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            utrgCosultasWeb.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            utrgCosultasWeb.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            utrgCosultasWeb.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            utrgCosultasWeb.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            utrgCosultasWeb.DisplayLayout.UseFixedHeaders = true;
            //Dimension los registros
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            utrgCosultasWeb.DisplayLayout.Bands[0].Columns["FECHA"].Width = 300;
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            cargarGrid();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
