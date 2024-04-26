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

namespace His.Maintenance
{
    public partial class frm_ExploradorUsrAcc : Form
    {
        public frm_ExploradorUsrAcc()
        {
            InitializeComponent();
        }

        private void btnHis_Click(object sender, EventArgs e)
        {
            ultraGridUsuariosAccesos.DataSource = null;
            if(chkUsuario.Checked == true)
                ultraGridUsuariosAccesos.DataSource = NegAccesoOpciones.ExploradorUsrInacHis();
            else
                ultraGridUsuariosAccesos.DataSource = NegAccesoOpciones.ExploradorUsrAccHis();
        }

        private void btnSic_Click(object sender, EventArgs e)
        {
            ultraGridUsuariosAccesos.DataSource = null;
            if (chkUsuario.Checked == true)
                ultraGridUsuariosAccesos.DataSource = NegAccesoOpciones.ExploradorUsrInacSic();
            else
                ultraGridUsuariosAccesos.DataSource = NegAccesoOpciones.ExploradorUsrAccSic();
        }

        private void btnCg_Click(object sender, EventArgs e)
        {
            ultraGridUsuariosAccesos.DataSource = null;
            if (chkUsuario.Checked == true)
                ultraGridUsuariosAccesos.DataSource = NegAccesoOpciones.ExploradorUsrInacCG();
            else
                ultraGridUsuariosAccesos.DataSource = NegAccesoOpciones.ExploradorUsrAccCG();
        }

        private void ultraGridUsuariosAccesos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridUsuariosAccesos.DisplayLayout.Bands[0];

            //ultraGridProcedimiento.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGridUsuariosAccesos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridUsuariosAccesos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridUsuariosAccesos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridUsuariosAccesos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridUsuariosAccesos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridUsuariosAccesos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            ultraGridUsuariosAccesos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridUsuariosAccesos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridUsuariosAccesos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridUsuariosAccesos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridUsuariosAccesos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGridUsuariosAccesos.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            ultraGridUsuariosAccesos.DisplayLayout.Bands[0].Columns[1].Width = 400;
            ultraGridUsuariosAccesos.DisplayLayout.Bands[0].Columns[4].Width = 250;
            ultraGridUsuariosAccesos.DisplayLayout.Bands[0].Columns[6].Width = 400;
            //ultraGridUsuariosAccesos.DisplayLayout.Bands[0].Columns["Perfil"].Width = 300;
            //e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ultraGridUsuariosAccesos.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        this.ultraGridExcelExporter1.Export(ultraGridUsuariosAccesos, PathExcel);
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
    }
}
