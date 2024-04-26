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
using His.Formulario;
using His.Entidades.Clases;
using System.Threading;


namespace His.Dietetica
{
    public partial class frmExploradorProcedimiento : Form
    {
        public frmExploradorProcedimiento()
        {
            InitializeComponent();
            DateTime oPrimerDiaDelMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            dtpDesde.Value = oPrimerDiaDelMes;
            dtpHasta.Value = oUltimoDiaDelMes;
            CargarInformacion(0,false);
            cargarCombo();
        }
        private void cargarCombo()
        {
            cmb_areas.DataSource = NegMaintenance.cargarBodegaExplorador();
            cmb_areas.ValueMember = "codlocal";
            cmb_areas.DisplayMember = "nomlocal";
            cmb_areas.SelectedIndex = 0;
        }
        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (dtpDesde.Value < dtpHasta.Value)
            {
                try
                {
                    dtpHasta.Value = dtpHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    if(chkHC.Checked)
                        CargarInformacion(Convert.ToInt32(cmb_areas.SelectedValue), true);
                    else
                        CargarInformacion(0, false);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Fecha \"Desde\" no puede mayor a fecha \"Hasta\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void CargarInformacion(Int64 bodega,bool filtro)
        {
           DataTable informacion = NegQuirofano.ExploradorProcedimientos(dtpDesde.Value, dtpHasta.Value,bodega,filtro);
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

        private void UltraGridProductos_InitializeLayout(object sender, InitializeLayoutEventArgs e)
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
            //Dimension los registros
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            UltraGridProductos.DisplayLayout.Bands[0].Columns["Procedimiento"].Width = 500;
            //agrandamiento de filas 

            //Ocultar columnas, que son fundamentales al levantar informacion
            //UltraGridProductos.DisplayLayout.Bands[0].Columns[6].Hidden = true;
            //UltraGridProductos.DisplayLayout.Bands[0].Columns[7].Hidden = true;
            //UltraGridProductos.DisplayLayout.Bands[0].Columns[15].Hidden = true;
            //UltraGridProductos.DisplayLayout.Bands[0].Columns[16].Hidden = true;
            //UltraGridProductos.DisplayLayout.Bands[0].Columns[11].Hidden = true;
            //UltraGridProductos.DisplayLayout.Bands[0].Columns[12].Hidden = true;
        }

        private void chkHC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHC.Checked)
                cmb_areas.Enabled = true;
            else
                cmb_areas.Enabled = false;
        }
    }
}
