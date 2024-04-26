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

namespace His.Admision
{
    public partial class frm_AyudaReligiosos : Form
    {
        public frm_AyudaReligiosos()
        {
            InitializeComponent();
        }

        private void frm_AyudaReligiosos_Load(object sender, EventArgs e)
        {
            try
            {
                grid.DataSource = NegUtilitarios.Congregaciones(tipo);
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
        }
        public string nomcongregacion;
        public string idcongregacion;
        public string direccion;
        public string ciudad;
        public string telefono;
        public int tipo;
        public string email;
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void grid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (grid.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = grid.ActiveRow;
                nomcongregacion = fila.Cells["CONGREGACION"].Value.ToString();
                idcongregacion = fila.Cells["RUC"].Value.ToString();
                direccion = fila.Cells["DIRECCION"].Value.ToString();
                ciudad = fila.Cells["CIUDAD"].Value.ToString();
                telefono = fila.Cells["TELEFONO"].Value.ToString();
                email = fila.Cells["EMAIL"].Value.ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("No puede elegir mas de uno", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
