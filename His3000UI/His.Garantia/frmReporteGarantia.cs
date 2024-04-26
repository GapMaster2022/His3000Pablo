using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using His.Negocio;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using System.IO;

namespace His.Garantia
{
    public partial class frmReporteGarantia : Form
    {
        NegPacienteGarantia Garantia = new NegPacienteGarantia();
        internal static string hc;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        public frmReporteGarantia()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(TablaReporte.Rows.Count == 0)
            {
                MessageBox.Show("No hay Registros para Mostrar.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ExportarAExcel();
            }
            //try
            //{
            //    string PathExcel = FindSavePath();
            //    if (PathExcel != null)
            //    {
            //        if (TablaGarantia.CanFocus == true)
            //            this.ultraGridExcelExporter1.Export(TablaGarantia, PathExcel);
            //        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
            //    }
            //}
            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }
            //finally
            //{ this.Cursor = Cursors.Default; }
        }
        private void ExportarAExcel()
        {
            this.CopiarGrilla();

            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object valor = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(valor);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            MessageBox.Show("Exportación Finalizada", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void CopiarGrilla()
        {
            TablaReporte.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            TablaReporte.MultiSelect = true;
            TablaReporte.SelectAll();
            DataObject dataObj = TablaReporte.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);

            TablaReporte.MultiSelect = false;
            TablaReporte.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

        }

        private void btnmostrar_Click(object sender, EventArgs e)
        {
            //if (radiotodos.Checked == true)
            //{
            //    TodasGarantias();
            //}
            //if (radiofechas.Checked == true)
            //{
            //    if (fechainicio.Value <= fechafin.Value)
            //    {
            //        PorFechaGarantias();
            //    }
            //    else
            //    {
            //        MessageBox.Show("La Fecha \"Desde\" no puede ser Mayor a la fecha \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        radiotodos.Checked = true;
            //    }
            //}
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {

        }
        private void CerrarAyuda(object sender, FormClosedEventArgs e)
        {
            frmAyuda.reporte = false;
            if(hc != null)
            {
                txthc.Text = hc.Trim();
            }
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            hc = null;
            frmAyuda x = new frmAyuda();
            frmAyuda.reporte = true;
            x.Show();
            x.FormClosed += CerrarAyuda;
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            if(Nhc.Checked == false && chkTratamiento.Checked == false && chbTipoIngreso.Checked == false)
            {
                //TablaGarantia.DataSource = Garantia.CargarPorFechas(dtpFiltroDesde.Text, dtpFiltroHasta.Text);
                //RedimencionarGrid();
                //if (TablaGarantia.Rows.Count == 0)
                //{
                //    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
                if(dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    TablaReporte.DataSource = Garantia.CargarPorFechas(dtpFiltroDesde.Value, dtpFiltroHasta.Value);
                    if (TablaReporte.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if(Fecha.Checked == true && Nhc.Checked == true
                && chkTratamiento.Checked == false && chbTipoIngreso.Checked == false)
            {
                if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    TablaReporte.DataSource = Garantia.CargarPorFechasHc(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                    if (TablaReporte.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            if(Fecha.Checked == true && Nhc.Checked == true && chkTratamiento.Checked == true && chbTipoIngreso.Checked == false)
            {
                if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Todas")
                    {
                        TablaReporte.DataSource = Garantia.CargarPorFechasHc(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                        if (TablaReporte.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Caducadas")
                    {
                        TablaReporte.DataSource = Garantia.CargarPorFechasHcCaducada(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                        if (TablaReporte.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Canceladas")
                    {
                        TablaReporte.DataSource = Garantia.CargarPorFechasHcCancelada(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                        if (TablaReporte.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Vigentes")
                    {
                        TablaReporte.DataSource = Garantia.CargarPorFechasHcVigente(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                        if (TablaReporte.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (Fecha.Checked == true && Nhc.Checked == true && chkTratamiento.Checked == true && chbTipoIngreso.Checked == true)
            {
                if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Todas")
                    {
                        TablaReporte.DataSource = Garantia.CargarGarantiaFechas(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                        if (TablaReporte.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Caducadas")
                    {
                        TablaReporte.DataSource = Garantia.CargarGarantiaFechasCaduca(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                        if (TablaReporte.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Canceladas")
                    {
                        TablaReporte.DataSource = Garantia.CargarGarantiaFechasCancelado(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                        if (TablaReporte.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Vigentes")
                    {
                        TablaReporte.DataSource = Garantia.CargarGarantiaFechasVigente(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                        if (TablaReporte.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if(Fecha.Checked == true && Nhc.Checked == true && chbTipoIngreso.Checked == true && chkTratamiento.Checked == false)
            {
                if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    TablaReporte.DataSource = Garantia.CargarPorFechasHcTipo(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                    if (TablaReporte.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if(Fecha.Checked == false && Nhc.Checked == true && chbTipoIngreso.Checked == false && chkTratamiento.Checked == false)
            {
                //TablaReporte.DataSource
            }
        }
        private void frmReporteGarantia_Load(object sender, EventArgs e)
        {
            CargarTipo();
            Bloquear();
        }
        public void Bloquear()
        {
            txthc.Enabled = false;
            txtf1.Enabled = false;
            combotipo.Enabled = false;
            cboEstadoGarantia.Enabled = false;
        }
        public void CargarTipo()
        {
            combotipo.DataSource = Garantia.TipoGarantia();
            combotipo.ValueMember = "TG_NOMBRE";
            combotipo.ValueMember = "TG_CODIGO";
        }
        public void RedimencionarGrid()
        {
            try
            {
                UltraGridBand bandUno = TablaGarantia.DisplayLayout.Bands[0];

                TablaGarantia.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                TablaGarantia.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                TablaGarantia.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                TablaGarantia.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                TablaGarantia.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                TablaGarantia.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                TablaGarantia.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                TablaGarantia.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                TablaGarantia.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                TablaGarantia.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                TablaGarantia.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                TablaGarantia.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                TablaGarantia.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                TablaGarantia.DisplayLayout.Bands[0].Columns[0].Width = 120;
                TablaGarantia.DisplayLayout.Bands[0].Columns[1].Width = 60;
                TablaGarantia.DisplayLayout.Bands[0].Columns[2].Width = 60;
                TablaGarantia.DisplayLayout.Bands[0].Columns[3].Width = 260;
                TablaGarantia.DisplayLayout.Bands[0].Columns[4].Width = 140;
                TablaGarantia.DisplayLayout.Bands[0].Columns[5].Width = 80;
                TablaGarantia.DisplayLayout.Bands[0].Columns[6].Width = 120;
                TablaGarantia.DisplayLayout.Bands[0].Columns[7].Width = 220;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            //if(txthc.Text != "")
            //{
            //    if (radiotodo.Checked == true)
            //    {
            //        if (cboEstadoGarantia.SelectedItem.ToString() == "Todas")
            //        {
            //            TablaReportes.DataSource = Garantia.CargarPacienteGarantiaTodo(hc, combotipo.SelectedValue.ToString());
            //            if (TablaReportes.Rows.Count == 0)
            //            {
            //                MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            }
            //        }
            //        if (cboEstadoGarantia.SelectedItem.ToString() == "Caducadas")
            //        {
            //            TablaReportes.DataSource = Garantia.CargarPacienteGarantiaCaducada(txthc.Text, combotipo.SelectedValue.ToString());
            //            if (TablaReportes.Rows.Count == 0)
            //            {
            //                MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            }
            //        }
            //        if (cboEstadoGarantia.SelectedItem.ToString() == "Canceladas")
            //        {
            //            TablaReportes.DataSource = Garantia.CargarPacienteGarantiaCancelada(txthc.Text, combotipo.SelectedValue.ToString());
            //            if (TablaReportes.Rows.Count == 0)
            //            {
            //                MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            }
            //        }
            //        if(cboEstadoGarantia.SelectedItem.ToString() == "Vigentes")
            //        {
            //            TablaReportes.DataSource = Garantia.CargarPacienteGarantiaVigente(txthc.Text, combotipo.SelectedValue.ToString());
            //            if (TablaReportes.Rows.Count == 0)
            //            {
            //                MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            }
            //        }
            //        if (cboEstadoGarantia.SelectedItem.ToString() == "")
            //        {
            //            combotipo.Focus();
            //        }
            //    }
            //    if(radiofechas.Checked == true)
            //    {
            //        if(cboEstadoGarantia.SelectedItem.ToString() == "Todas")
            //        {
            //            if(dtpFiltroDesde.Value < dtpFiltroHasta.Value)
            //            {
            //                TablaReportes.DataSource = Garantia.CargarGarantiaFechas(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
            //                if (TablaReportes.Rows.Count == 0)
            //                {
            //                    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("\"Desde\" No Puede Ser Mayor que \"Hasta\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //        if(cboEstadoGarantia.SelectedItem.ToString() == "Caducadas")
            //        {
            //            if(dtpFiltroDesde.Value < dtpFiltroHasta.Value)
            //            {
            //                TablaReportes.DataSource = Garantia.CargarGarantiaFechasCaduca(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
            //                if (TablaReportes.Rows.Count == 0)
            //                {
            //                    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("\"Desde\" No Puede Ser Mayor que \"Hasta\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //        if (cboEstadoGarantia.SelectedItem.ToString() == "Canceladas")
            //        {
            //            if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
            //            {
            //                TablaReportes.DataSource = Garantia.CargarGarantiaFechasCancelado(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
            //                if (TablaReportes.Rows.Count == 0)
            //                {
            //                    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("\"Desde\" No Puede Ser Mayor que \"Hasta\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //        if (cboEstadoGarantia.SelectedItem.ToString() == "Vigentes")
            //        {
            //            if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
            //            {
            //                TablaReportes.DataSource = Garantia.CargarGarantiaFechasVigente(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
            //                if (TablaReportes.Rows.Count == 0)
            //                {
            //                    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("\"Desde\" No Puede Ser Mayor que \"Hasta\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }
            //        if(cboEstadoGarantia.SelectedItem.ToString() == "")
            //        {
            //            combotipo.Focus();
            //        }
            //    }
            //}
        }

        private void txthc_TextChanged(object sender, EventArgs e)
        {
            //((DataTable)TablaReportes.DataSource).DefaultView.RowFilter = $"HC LIKE '{txthc.Text}%'";
        }
        private void radiohc_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chkTratamiento_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTratamiento.Checked == true)
            {
                cboEstadoGarantia.Enabled = true;
            }
            else
            {
                cboEstadoGarantia.Enabled = false;
            }
        }

        private void chbTipoIngreso_CheckedChanged(object sender, EventArgs e)
        {
            if(chbTipoIngreso.Checked == true)
            {
                combotipo.Enabled = true;
            }
            else
            {
                combotipo.Enabled = false;
            }
        }

        private void Fecha_CheckedChanged(object sender, EventArgs e)
        {
            if(Fecha.Checked == true)
            {
                dtpFiltroDesde.Enabled = true;
                dtpFiltroHasta.Enabled = true;
            }else
            {
                dtpFiltroDesde.Enabled = false;
                dtpFiltroHasta.Enabled = false;
            }
        }

        private void Nhc_CheckedChanged(object sender, EventArgs e)
        {
            if (Nhc.Checked == true)
            {
                txthc.Enabled = true;
                txtf1.Enabled = true;
            }
            else
            {
                txthc.Enabled = false;
                txtf1.Enabled = false;
                txthc.Text = "";
            }
        }
    }
}
