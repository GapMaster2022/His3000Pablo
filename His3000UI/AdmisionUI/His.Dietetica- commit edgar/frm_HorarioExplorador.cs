using Infragistics.Win.UltraWinGrid;
using System;
using His.Negocio;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class frm_HorarioExplorador : Form
    {
        public frm_HorarioExplorador()
        {
            InitializeComponent();
            dtpDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            dtpHasta.Value = DateTime.Now;
            Refrescar();
        }

        public void Refrescar()
        {
            grid.DataSource = NegDietetica.getDataTable("GetHorarioMedicos", dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"), dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"));
        }



        private void gridRegistro_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            grid.DisplayLayout.UseFixedHeaders = true;
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            Refrescar();
        }
    }
}
