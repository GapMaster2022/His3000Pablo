using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;

namespace His.Dietetica
{
    public partial class frmExploradorRubrosProcedimiento : Form
    {
        public frmExploradorRubrosProcedimiento()
        {
            InitializeComponent();
            DateTime oPrimerDiaDelMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtpDesde.Value = oPrimerDiaDelMes;
            dtpHasta.Value = oUltimoDiaDelMes;
            CargarInformacion();
        }
        public void CargarInformacion()
        {
            DataTable informacion = NegQuirofano.TodoslosProcedimiento(dtpDesde.Value, dtpHasta.Value);
            UltraGridProductos.DataSource = informacion;
            label4.Text = Convert.ToString(informacion.Rows.Count);
        }
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
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
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (dtpDesde.Value < dtpHasta.Value)
            {
                try
                {
                    dtpHasta.Value = dtpHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                    CargarInformacion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Fecha \"Desde\" no puede mayor a fecha \"Hasta\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (UltraGridProductos.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        this.ultraGridExcelExporter1.Export(UltraGridProductos, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                else
                {
                    MessageBox.Show("No tiene Registros para Exportar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UltraGridProductos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridProductos.DisplayLayout.Bands[0];

            UltraGridProductos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            UltraGridProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            UltraGridProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            UltraGridProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            UltraGridProductos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            UltraGridProductos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            UltraGridProductos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            UltraGridProductos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            UltraGridProductos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            UltraGridProductos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            UltraGridProductos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            UltraGridProductos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            UltraGridProductos.DisplayLayout.UseFixedHeaders = true;

            UltraGridProductos.DisplayLayout.Bands[0].Columns["HC"].Width = 50;
            UltraGridProductos.DisplayLayout.Bands[0].Columns["ATENCION"].Width = 50;
            UltraGridProductos.DisplayLayout.Bands[0].Columns["PACIENTE"].Width = 250;
            UltraGridProductos.DisplayLayout.Bands[0].Columns["CIRUJANO"].Width = 250;
            UltraGridProductos.DisplayLayout.Bands[0].Columns["AYUDANTE"].Width = 250;
            UltraGridProductos.DisplayLayout.Bands[0].Columns["ANESTESIÓLOGO"].Width = 250;
            UltraGridProductos.DisplayLayout.Bands[0].Columns["PATOLOGO"].Width = 250;
            UltraGridProductos.DisplayLayout.Bands[0].Columns["PRODUCTO"].Width = 250;
        }
    }
}
