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

namespace His.Admision
{
    public partial class INEN : Form
    {
        public INEN()
        {
            InitializeComponent();
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
                    if (ultraGridPacientes.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ultraGridPacientes, PathExcel);
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

        private void INEN_Load(object sender, EventArgs e)
        {
            try
            {
                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

                dtpFiltroDesde.Value = oPrimerDiaDelMes;
                dtpFiltroHasta.Value = oUltimoDiaDelMes;
                ultraGridPacientes.DataSource = Negocio.NegPacientes.getINEN(dtpFiltroDesde.Value.Date.ToString(), dtpFiltroHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString());
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            ultraGridPacientes.DataSource = Negocio.NegPacientes.getINEN(dtpFiltroDesde.Value.Date.ToString(), dtpFiltroHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59).ToString());

            if (chbDivididas.Checked == true)
            {

                //Oculto Atenciones con el mismo numero de atencion
                string numAnterior = "0";
                foreach (UltraGridRow row in ultraGridPacientes.Rows)
                {
                    if (numAnterior == "0")
                    {
                        numAnterior = row.Cells["Nº ATENCION"].Value.ToString().Trim();
                    }
                    else
                    {
                        if (row.Cells["Nº ATENCION"].Value.ToString().Trim() == numAnterior)
                        {
                            row.Hidden = true;
                        }
                        numAnterior = row.Cells["Nº ATENCION"].Value.ToString().Trim();
                    }
                }
            }
        }

        private void ultraGridPacientes_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            ultraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            ultraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
        }
    }
}
