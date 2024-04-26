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
using His.Negocio;

namespace His.Admision
{
    public partial class frmReporteDefunciones : Form
    {
        public string pac_codigo;
        public frmReporteDefunciones()
        {
            InitializeComponent();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refrescar()
        {
            string X = "'" + dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' AND '" + dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' ";
            ugrdHistorial.DataSource = NegDietetica.getDataTable("Defunciones",X);
            
        }


        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ugrdHistorial.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ugrdHistorial, PathExcel);
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

        private void ugrdHistorial_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ugrdHistorial.DisplayLayout.Bands[0];
            ugrdHistorial.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ugrdHistorial.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ugrdHistorial.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ugrdHistorial.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ugrdHistorial.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            ugrdHistorial.DisplayLayout.Bands[0].Columns[0].Hidden = true;

        }

        private void frmReporteDefunciones_Load(object sender, EventArgs e)
        {
            dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            dtpFiltroHasta.Value = DateTime.Now;
            refrescar();
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        private void btnRevertir_Click(object sender, EventArgs e)
        {
            if(ugrdHistorial.Selected.Rows.Count > 0)
            {
                UltraGridRow fila = ugrdHistorial.ActiveRow;
                frm_ClavePedido x = new frm_ClavePedido();
                x.ShowDialog();
                if (x.aceptado == true)
                {
                    try
                    {
                        NegPacienteDatosAdicionales.RevertirDefuncion(Convert.ToInt32(fila.Cells[0].Value.ToString()));
                        MessageBox.Show("Reversión Exitosa.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refrescar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
