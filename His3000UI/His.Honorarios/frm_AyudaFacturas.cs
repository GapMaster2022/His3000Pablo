using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Honorarios
{
    public partial class frm_AyudaFacturas : Form
    {
        public string ate_codigo;
        public string facturaAnte;
        public string obs;
        public frm_AyudaFacturas()
        {
            InitializeComponent();
        }

        private void frm_AyudaFacturas_Load(object sender, EventArgs e)
        {
            try
            {
                grid.DataSource = NegHonorariosMedicos.PacientesFac_Anuladas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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

            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            grid.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            grid.DisplayLayout.Bands[0].Columns[0].Width = 100;
            grid.DisplayLayout.Bands[0].Columns[1].Width = 260;
            grid.DisplayLayout.Bands[0].Columns[2].Width = 80;
            grid.DisplayLayout.Bands[0].Columns[3].Width = 80;
            grid.DisplayLayout.Bands[0].Columns[4].Width = 120;
            //grid.DisplayLayout.Bands[0].Columns[5].Width = 120;
            grid.DisplayLayout.Bands[0].Columns[5].Hidden = true;
            grid.DisplayLayout.Bands[0].Columns[6].Hidden = true;

            //grid.DisplayLayout.Bands[0].Columns[7].Width = 180;
        }

        private void grid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if(grid.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = grid.ActiveRow;

                ate_codigo = fila.Cells["ATENCION"].Value.ToString();
                facturaAnte = fila.Cells["F. ANTERIOR"].Value.ToString();
                obs = fila.Cells["OBSERVACION"].Value.ToString();
                this.Close();
            }
        }

        private void txt_busqNom_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    if(txt_busqNom.Text.Trim() != "")
                        grid.DataSource = NegHonorariosMedicos.FiltroPacientes(txt_busqNom.Text.Trim());
                    else
                        grid.DataSource = NegHonorariosMedicos.PacientesFac_Anuladas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
