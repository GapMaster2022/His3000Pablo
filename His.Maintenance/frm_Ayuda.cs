using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;

namespace His.Maintenance
{
    public partial class frm_Ayuda : Form
    {
        public String codigo = "";
        public frm_Ayuda()
        {
            InitializeComponent();
            grid.DataSource = NegMaintenance.getDataTable("DescripcionCatalogoCostos");
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];
            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
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
            grid.DisplayLayout.UseFixedHeaders = true;
        }

        private void grid_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            if (grid.ActiveRow.Index > -1)
            {
                codigo = grid.ActiveRow.Cells[0].Value.ToString();
            }
            this.Close();
        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (grid.ActiveRow.Index > -1)
                {
                    codigo = grid.ActiveRow.Cells[0].Value.ToString();
                }
                this.Close();
                e.Handled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //   MessageBox.Show("1. El producto que busca ya fue agregado anteriormente.  \n" + "2. No ha sido creado en His3000 o Sic3000", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("¿Por qué no encuentro un producto?  \n" + "    Por que el producto no existe en el modulo de inventarios SIC3000.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
