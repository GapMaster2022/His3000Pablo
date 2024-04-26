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

namespace His.Formulario
{
    public partial class frm_AyudaEspecialidad : Form
    {
        public frm_AyudaEspecialidad()
        {
            InitializeComponent();
        }

        private void frm_AyudaEspecialidad_Load(object sender, EventArgs e)
        {
            try
            {
               gridEspecialidad.DataSource = NegCatalogos.EspecilidadAdmision();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string esp_codigo = "";
        public string esp_nombre = "";
        private void gridEspecialidad_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if(gridEspecialidad.Selected.Rows.Count > 0)
            {
                UltraGridRow fila = gridEspecialidad.ActiveRow;
                esp_codigo = fila.Cells[0].Value.ToString();
                esp_nombre = fila.Cells[1].Value.ToString();
                this.Close();
            }
        }

        private void gridEspecialidad_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = gridEspecialidad.DisplayLayout.Bands[0];

            gridEspecialidad.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            gridEspecialidad.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            gridEspecialidad.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            gridEspecialidad.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            gridEspecialidad.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            gridEspecialidad.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            gridEspecialidad.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            gridEspecialidad.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            gridEspecialidad.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            gridEspecialidad.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            gridEspecialidad.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            gridEspecialidad.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            bandUno.Columns["DESCRIPCION"].Width = 200;
            bandUno.Columns["CODIGO"].Width = 50;

            gridEspecialidad.DisplayLayout.UseFixedHeaders = true;
        }
    }
}
