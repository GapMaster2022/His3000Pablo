using His.Formulario;
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

namespace CuentaPaciente
{
    public partial class frmExploradorAuditoria : Form
    {
        public frmExploradorAuditoria()
        {
            InitializeComponent();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        public void CargarDatos()
        {
            try
            {
                if (dtpdesde.Value < dtphasta.Value)
                    ultraGridAuditoria.DataSource = NegDetalleCuenta.PacientesAuditoria(dtpdesde.Value, dtphasta.Value, chkIngreso.Checked, chkAlta.Checked, txt_historiaclinica.Text.Trim());
                else
                    MessageBox.Show("Fecha desde no puede ser mayor", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido cargar explorador.\r\nMas detalle: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmExploradorAuditoria_Load(object sender, EventArgs e)
        {
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            dtpdesde.Value = oPrimerDiaDelMes;
            dtphasta.Value = oUltimoDiaDelMes;
            CargarDatos();
        }

        private void ultraGridAuditoria_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridAuditoria.DisplayLayout.Bands[0];

            ultraGridAuditoria.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGridAuditoria.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridAuditoria.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridAuditoria.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridAuditoria.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridAuditoria.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridAuditoria.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            ultraGridAuditoria.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

            //Caracteristicas de Filtro en la grilla
            ultraGridAuditoria.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridAuditoria.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridAuditoria.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridAuditoria.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridAuditoria.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGridAuditoria.DisplayLayout.UseFixedHeaders = true;
            ultraGridAuditoria.DisplayLayout.Bands[0].Columns["ATE_CODIGO"].Hidden = true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if(ultraGridAuditoria.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = ultraGridAuditoria.ActiveRow;

                ReporteAuditoria dataSet = new ReporteAuditoria();
                DataRow dataRow;
                dataRow = dataSet.Tables["Paciente"].NewRow();
                dataRow["Nombre"] = fila.Cells["PACIENTE"].Value.ToString();
                dataRow["FechaIngreso"] = fila.Cells["F. INGRESO"].Value.ToString();
                dataRow["FechaAlta"] = fila.Cells["F. ALTA"].Value.ToString();
                dataRow["Habitacion"] = fila.Cells["HAB."].Value.ToString();
                dataRow["Convenio"] = fila.Cells["ASEGURADORA"].Value.ToString();
                dataRow["Usuario"] = His.Entidades.Clases.Sesion.nomUsuario;
                dataSet.Tables["Paciente"].Rows.Add(dataRow);

                DataTable detalleNuevos = new DataTable();
                detalleNuevos = NegDetalleCuenta.ListaNuevos(Convert.ToInt64(fila.Cells["ATE_CODIGO"].Value.ToString()));
                foreach (DataRow item in detalleNuevos.Rows)
                {

                    dataRow = dataSet.Tables["Nuevos"].NewRow();
                    dataRow["Cue_codigo"] = item["CUE_CODIGO"];
                    dataRow["Cue_detalle"] = item["CUE_DETALLE"];
                    dataRow["Cue_cantidad"] = item["CUE_CANTIDAD"];
                    dataRow["Cue_valor_unitario"] = item["CUE_VALOR_UNITARIO"];
                    dataRow["Cue_iva"] = item["CUE_IVA"];
                    dataRow["Cue_valor"] = item["CUE_VALOR"];
                    dataRow["Usuario"] = item["USUARIO"];
                    dataSet.Tables["Nuevos"].Rows.Add(dataRow);
                }

                DataTable detalleHistoria = new DataTable();
                detalleHistoria = NegDetalleCuenta.MuestraItemsModificados(Convert.ToInt64(fila.Cells["ATE_CODIGO"].Value.ToString()));
                foreach (DataRow item in detalleHistoria.Rows)
                {
                    dataRow = dataSet.Tables["Historial"].NewRow();
                    dataRow["Detalle"] = item["Descripcion"];
                    dataRow["CantidadOriginal"] = item["CantidadAnterior"];
                    dataRow["NuevaCantidad"] = item["CantidadActual"];
                    dataRow["ValorOriginal"] = item["SubTotalanterior"];
                    dataRow["NuevoValor"] = item["SubTotalActual"];
                    dataRow["Observacion"] = item["Usuario"] + "--" + item["Observacion"];
                    dataSet.Tables["Historial"].Rows.Add(dataRow);

                }
                DataTable detalleItemEliminados = new DataTable();
                detalleItemEliminados = NegDetalleCuenta.ListaItemsEliminadosCuenta(Convert.ToInt64(fila.Cells["ATE_CODIGO"].Value.ToString()));

                foreach (DataRow item in detalleItemEliminados.Rows)
                {
                    dataRow = dataSet.Tables["Eliminados"].NewRow();

                    dataRow["detalle"] = item["Descripcion"];
                    dataRow["precio"] = item["Total"];
                    dataRow["Observacion"] = item["Observacion"];
                    dataSet.Tables["Eliminados"].Rows.Add(dataRow);
                }


                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "Auditoria", dataSet);
                reporte.ShowDialog();
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(ultraGridAuditoria, PathExcel);
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

        private void chkHC_CheckedChanged(object sender, EventArgs e)
        {
            txt_historiaclinica.Enabled = chkHC.Checked;
            ayudaPacientes.Visible = chkHC.Checked;
            txt_historiaclinica.Text = "0";
        }

        private void txt_historiaclinica_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txt_historiaclinica_Leave(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text == "")
                txt_historiaclinica.Text = "0";
        }

        private void txt_historiaclinica_Enter(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text == "0")
                txt_historiaclinica.Text = "";
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            His.Admision.frm_AyudaPacientes form = new His.Admision.frm_AyudaPacientes();
            form.campoPadre = txt_historiaclinica;
            form.ShowDialog();
            form.Dispose();
            txt_historiaclinica.Text = txt_historiaclinica.Text.Trim();
        }

        public void cambio()
        {

        }
    }
}
