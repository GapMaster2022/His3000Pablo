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

namespace His.Dietetica
{
    public partial class frm_AyudaImagenPaciente : Form
    {
        NegImagen imagen = new NegImagen();
        public frm_AyudaImagenPaciente()
        {
            InitializeComponent();
        }

        private void frm_AyudaImagenPaciente_Load(object sender, EventArgs e)
        {
            CargarPacientesImagen();
        }
        public void CargarPacientesImagen()
        {
            try
            {
                grid.DataSource = imagen.PacientesImagen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
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

                //Dimension los registros
                grid.DisplayLayout.Bands[0].Columns[0].Width = 80;
                grid.DisplayLayout.Bands[0].Columns[1].Width = 60;
                grid.DisplayLayout.Bands[0].Columns[2].Width = 100;
                grid.DisplayLayout.Bands[0].Columns[3].Width = 80;
                grid.DisplayLayout.Bands[0].Columns[4].Width = 300;
                grid.DisplayLayout.Bands[0].Columns[5].Width = 100;
                grid.DisplayLayout.Bands[0].Columns[6].Width = 200;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void grid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (grid.Selected.Rows.Count>0)
            {
                UltraGridRow fila = grid.ActiveRow;
                ImagenAgendamiento.ate_codigo = fila.Cells[2].Value.ToString();
                ImagenAgendamiento.paciente = fila.Cells[4].Value.ToString();
                this.Close();
            }
            
        }
    }
}
