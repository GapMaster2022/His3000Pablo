using His.Entidades.Clases;
using His.HabitacionesUI;
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

namespace His.Pedidos
{
    public partial class frmMonitorDevoluciones : Form
    {
        Timer Actualizar = new Timer();
        public frmMonitorDevoluciones()
        {
            InitializeComponent();
        }

        private void frmMonitorDevoluciones_Load(object sender, EventArgs e)
        {
            //INICIAMOS EL VALOR DE LAS FECHAS
            dtpDesde.Value = DateTime.Now.AddDays(-1).Date;
            dtpHasta.Value = DateTime.Now;

            Buscar();
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Buscar()
        {
            if (ckbFiltro.Checked)
            {
                UltraGridDevoluciones.DataSource = NegPedidos.Devoluciones(dtpDesde.Value, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), txt_historiaclinica.Text.Trim());
            }
            else
            {
                UltraGridDevoluciones.DataSource = NegPedidos.Devoluciones(DateTime.Now.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), txt_historiaclinica.Text.Trim());
            }
        }

        private void ckbAutomatico_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbAutomatico.Checked)
            {
                Actualizar.Interval = 30000;
                Actualizar.Tick += Refrescar;
                Actualizar.Enabled = true;
            }
        }
        private void Refrescar(object sender, EventArgs e)
        {
            lbltimer.Text = DateTime.Now.ToLongTimeString();
            Buscar();
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 14)
            {
                if (UltraGridDevoluciones.Selected.Rows.Count > 0 && UltraGridDevoluciones.Selected.Rows.Count == 1)
                {
                    UltraGridRow fila = this.UltraGridDevoluciones.ActiveRow;
                    int ped_codigo = Convert.ToInt32(fila.Cells["N° DEVOLUCIÓN"].Value.ToString());

                    if (ped_codigo > 0)
                    {
                        var codigoArea = 100;

                        frmImpresionPedidos frmPedidos = new frmImpresionPedidos(ped_codigo, codigoArea, 1, 0);
                        frmImpresionPedidos.reimpresion = true;
                        frmPedidos.Show();
                    }

                }
                else
                {
                    MessageBox.Show("Debe elegir un pedido o no pueden ser varias filas a la vez.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void UltraGridDevoluciones_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = UltraGridDevoluciones.DisplayLayout.Bands[0];

                UltraGridDevoluciones.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridDevoluciones.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridDevoluciones.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridDevoluciones.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridDevoluciones.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridDevoluciones.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridDevoluciones.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                UltraGridDevoluciones.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                UltraGridDevoluciones.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridDevoluciones.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridDevoluciones.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridDevoluciones.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridDevoluciones.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridDevoluciones.DisplayLayout.UseFixedHeaders = true;

                //UltraGridDevoluciones.DisplayLayout.Bands[0].Columns["ESTADO PEDIDO"].Hidden = true;


                ////Dimension los registros
                UltraGridDevoluciones.DisplayLayout.Bands[0].Columns[0].Width = 60;
                UltraGridDevoluciones.DisplayLayout.Bands[0].Columns[1].Width = 200;
                UltraGridDevoluciones.DisplayLayout.Bands[0].Columns[2].Width = 60;
                UltraGridDevoluciones.DisplayLayout.Bands[0].Columns[3].Width = 60;
                UltraGridDevoluciones.DisplayLayout.Bands[0].Columns[4].Width = 100;
                UltraGridDevoluciones.DisplayLayout.Bands[0].Columns[5].Width = 100;
                UltraGridDevoluciones.DisplayLayout.Bands[0].Columns[6].Width = 120;
                UltraGridDevoluciones.DisplayLayout.Bands[0].Columns[7].Width = 60;
                UltraGridDevoluciones.DisplayLayout.Bands[0].Columns[8].Width = 200;


                ////agrandamiento de filas 

                ////Ocultar columnas, que son fundamentales al levantar informacion
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Hidden = false;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = false;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[8].Hidden = false;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[10].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[11].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[13].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[14].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[15].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[16].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[14].Hidden = true;
                ////TablaProductos.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                ////TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message); //No se muestra el error al usuario solo sirve para saber cual es el error por interno.
            }
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            His.Admision.frm_AyudaPacientes x = new Admision.frm_AyudaPacientes();
            x.campoPadre = txt_historiaclinica;
            x.ShowDialog();
            x.Dispose();
            txt_historiaclinica.Text = txt_historiaclinica.Text.Trim();
        }

        private void chkHC_CheckedChanged(object sender, EventArgs e)
        {
            txt_historiaclinica.Enabled = chkHC.Checked;
            ayudaPacientes.Visible = chkHC.Checked;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (UltraGridDevoluciones.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(UltraGridDevoluciones, PathExcel);
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
