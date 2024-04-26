using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Formulario
{
    public partial class frm_PacientesConsultaExterna : Form
    {
        public string codigoConsultaExterna = "";
        public DataTable Pacientes = new DataTable();
        public frm_PacientesConsultaExterna()
        {
            InitializeComponent();
        }

        private void UltraGridPacientes_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = UltraGridPacientes.DisplayLayout.Bands[0];

                UltraGridPacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridPacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridPacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridPacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridPacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridPacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                UltraGridPacientes.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                UltraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridPacientes.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[0].Width = 80;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[1].Width = 60;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[2].Width = 300;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Width = 60;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[4].Width = 100;

                UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UltraGridPacientes_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (UltraGridPacientes.Selected.Rows.Count > 0)
            {
                UltraGridRow fila = UltraGridPacientes.ActiveRow;
                codigoConsultaExterna = fila.Cells[5].Value.ToString();
                this.Close();
            }
            else
            {
                codigoConsultaExterna = "";
            }
        }

        private void frm_PacientesConsultaExterna_Load(object sender, EventArgs e)
        {
            UltraGridPacientes.DataSource = Pacientes;
        }
    }
}
