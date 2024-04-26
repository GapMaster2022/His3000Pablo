using His.Entidades;
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

namespace His.ConsultaExterna
{
    public partial class frm_AyudaMedicos : Form
    {
        public string med_codigo;
        public int esp_codigo;
        public frm_AyudaMedicos(string esp_codigo)
        {
            InitializeComponent();
            this.esp_codigo = Convert.ToInt32(esp_codigo);
        }

        private void frm_AyudaMedicos_Load(object sender, EventArgs e)
        {
            List<MEDICOS> lista = NegMedicos.MedicosCitaMedica(esp_codigo);
            grid.DataSource = lista.Select(n => new
            {
                CODIGO = n.MED_CODIGO,
                NOMBRE = n.MED_APELLIDO_PATERNO + " " + n.MED_APELLIDO_MATERNO + " " + n.MED_NOMBRE1 + " " + n.MED_NOMBRE2,
                ESPECIALIDAD = n.ESPECIALIDADES_MEDICAS.ESP_NOMBRE,
                RUC = n.MED_RUC,
                TELF_CONSULT = n.MED_TELEFONO_CONSULTORIO,
                TELF_CASA = n.MED_TELEFONO_CASA,
                CELULAR = n.MED_TELEFONO_CELULAR
            }).ToList();
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

            grid.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 80;
            grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
            grid.DisplayLayout.Bands[0].Columns["ESPECIALIDAD"].Width = 180;
            grid.DisplayLayout.Bands[0].Columns["RUC"].Width = 100;
            grid.DisplayLayout.Bands[0].Columns["TELF_CONSULT"].Width = 100;
        }

        private void grid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if(grid.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = grid.ActiveRow;
                med_codigo = fila.Cells["CODIGO"].Value.ToString();
                this.Close();
            }
        }
    }
}
