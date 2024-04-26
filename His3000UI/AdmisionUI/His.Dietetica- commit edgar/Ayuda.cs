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

namespace His.Dietetica
{
    public partial class Ayuda : Form
    {

        public String producto = "";
        public String codigo = "";
        public String adicional1 = "";
        private string tabla;
        private int xcod;

        public Ayuda(string tipo, int codigo)
        {
            InitializeComponent();
            tabla = tipo;
            xcod = codigo;
            cargarTabla();
        }

        private void cargarTabla()
        {
            switch (tabla)
            {
                case "ATE_IMAGEN":
                    grid.DataSource = NegImagen.getAteImagen(xcod);
                    //grid.DisplayLayout.Bands[0].Columns[3].Hidden = true;
                    break;
                case "DIAGNOSTICOS":
                    grid.DataSource = NegImagen.getCIE10();
                    break;
                case "VENDEDORES":
                    grid.DataSource = NegVendedores.getVendedoresS();
                    break;
                case "MEDICOS":
                    grid.DataSource = NegVendedores.getVendedoresS();
                    break;
                case "TARJETAS":
                    grid.DataSource = NegVendedores.getTarjetas();
                    break;
            }


        }

        private void grid_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            if (grid.ActiveRow.Index > -1)
            {
                codigo = grid.ActiveRow.Cells[0].Value.ToString();
                producto = grid.ActiveRow.Cells[1].Value.ToString();
                try
                {
                    adicional1 = grid.ActiveRow.Cells[3].Value.ToString();
                }
                catch
                {
                }
            }
            this.Close();
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
    }
}
