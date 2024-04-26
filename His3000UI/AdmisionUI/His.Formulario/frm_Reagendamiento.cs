using His.Entidades;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.ConsultaExterna
{
    public partial class frm_Reagendamiento : Form
    {
        List<DtoAgendados> DtoAgendados = new List<DtoAgendados>();
        string Paciente = "";
        public string codigo = "";
        public frm_Reagendamiento(List<DtoAgendados> agendados, string paciente)
        {
            InitializeComponent();
            this.DtoAgendados = agendados;
            this.Paciente = paciente;
            cargarAgendas();
            lblPaciente.Text = paciente;
        }
        public void cargarAgendas()
        {
            grid.DataSource = DtoAgendados;
        }

        private void grid_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if(grid.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = grid.ActiveRow;
                codigo = fila.Cells["Codigo"].Value.ToString();
                this.Close();
            }
        }

        private void grid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
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

            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            grid.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            grid.DisplayLayout.UseFixedHeaders = true;

            grid.DisplayLayout.Bands[0].Columns["Medico"].Width = 400;
            grid.DisplayLayout.Bands[0].Columns["Especialidad"].Width = 150;
        }
    }
}
