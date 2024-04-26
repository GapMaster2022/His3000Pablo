using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.Dietetica
{
    public partial class frmayudaProcedimiento : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static bool pedidopaciente = false;
        public frmayudaProcedimiento()
        {
            InitializeComponent();
        }
        public void RedimencionarTabla()
        {
            try
            {
                UltraGridBand bandUno = TablaProcedimiento.DisplayLayout.Bands[0];

                TablaProcedimiento.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                TablaProcedimiento.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                TablaProcedimiento.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                TablaProcedimiento.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                TablaProcedimiento.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                TablaProcedimiento.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                TablaProcedimiento.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                TablaProcedimiento.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                TablaProcedimiento.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                TablaProcedimiento.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                TablaProcedimiento.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                TablaProcedimiento.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                TablaProcedimiento.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                TablaProcedimiento.DisplayLayout.Bands[0].Columns[0].Width = 60;
                //TablaProcedimiento.DisplayLayout.Bands[0].Columns[1].Width = 100;


                ////Ocultar columnas, que son fundamentales al levantar informacion
                //TablaProcedimiento.DisplayLayout.Bands[0].Columns[2].Hidden = true;
                //TablaProcedimiento.DisplayLayout.Bands[0].Columns[3].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmayudaProcedimiento_Load(object sender, EventArgs e)
        {
            if(pedidopaciente == false)
            {
                CargarTablaProcedimientos();
            }
            else
            {
                CargarSoloProcedimientos();
            }
        }
        public void CargarTablaProcedimientos()
        {
            TablaProcedimiento.DataSource = Quirofano.MostrarProcedimientos();
            RedimencionarTabla();
        }
        public void CargarSoloProcedimientos()
        {
            TablaProcedimiento.DataSource = Quirofano.SoloProcedimientos();
            RedimencionarTabla();
        }
        private void TablaProcedimiento_DoubleClick(object sender, EventArgs e)
        {
            if(TablaProcedimiento.Selected.Rows.Count > 0)
            {
                UltraGridRow Fila = TablaProcedimiento.ActiveRow; //eligue el numero de fila que esta seleccionada
                if (pedidopaciente == false)
                {
                    frmQuirofanoAgregarProcedimiento.cie_codigo = Fila.Cells[0].Value.ToString();
                    frmQuirofanoAgregarProcedimiento.cie_descripcion = Fila.Cells[1].Value.ToString();
                    this.Close();
                }
                else
                {
                    if (MessageBox.Show("¿Desea Agregar este Procedimiento al Paciente?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                        == DialogResult.Yes)
                    {
                        frmQuirofanoExplorador.cie_codigo = Fila.Cells[0].Value.ToString();
                        frmQuirofanoExplorador.cie_descripcion = Fila.Cells[1].Value.ToString();
                        this.Close();
                    }
                    this.Close();
                }
            }
        }
    }
}
