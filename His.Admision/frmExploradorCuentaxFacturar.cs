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
using Infragistics.Win.UltraWinGrid;

namespace His.Admision
{
    public partial class frmExploradorCuentaxFacturar : Form
    {
        public frmExploradorCuentaxFacturar()
        {
            InitializeComponent();
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            if(dtpFiltroDesde.Value.Date < dtpFiltroHasta.Value)
            {
                grid.DataSource = NegRubros.getCuentas(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), chkIngreso.Checked, chkAlta.Checked, txt_historiaclinica.Text, chkCero.Checked);
            }
            else
            {
                MessageBox.Show("Fecha \"Desde\" no puede ser mayor a fecha \"Hasta\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void frmExploradorCuentaxFacturar_Load(object sender, EventArgs e)
        {
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;
            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            dtpFiltroDesde.Value = oPrimerDiaDelMes;
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;



            //Cambio la apariencia de las sumas
            bandUno.Summaries.Clear();
            bandUno.SummaryFooterCaption = "Totales: ";
            bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.FromArgb(189, 191, 191);
            bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.Black;
            
            //totalizo las columnas
            SummarySettings sumReferido = bandUno.Summaries.Add("Valor", SummaryType.Sum, bandUno.Columns["VALOR"]);
            sumReferido.DisplayFormat = "{0:#####.00}";
            sumReferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            //dimesiono las columnas
            bandUno.Columns["PACIENTE"].Width = 300;
            bandUno.Columns["HCL"].Width = 80;
            bandUno.Columns["CODIGO_UNICO_ATENCION"].Hidden = true;
            bandUno.Columns["NRO ATENCION"].Width = 100;
            bandUno.Columns["F. INGRESO"].Width = 100;
            bandUno.Columns["F. ALTA"].Width = 100;
            bandUno.Columns["VALOR"].Width = 80;
            bandUno.Columns["ASEGURADORA"].Width = 250;

        }
    }
}
