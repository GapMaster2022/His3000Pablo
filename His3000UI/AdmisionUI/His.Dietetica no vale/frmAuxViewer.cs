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
    public partial class frmAuxViewer : Form
    {
        private string xcodigo;
        internal static string paciente; //Contiene el nombre del paciente que envia desde explorador Dieta
        internal static string hc; //contiene el hc del paciente
        internal static string ate_codigo; // codigo de la atencion del paciente
        internal static string medico; //contiene el nombre del medico
        public frmAuxViewer(string codigo)
        {
            InitializeComponent();
            grid.DataSource = NegDietetica.getDataTable("HistorialDietas", codigo);

            DataTable rs = NegDietetica.getDataTable("getObservacionAtencion", codigo);
            try { textBox1.Text = rs.Rows[0][0].ToString(); }
            catch { }
            xcodigo = codigo;
            this.Text = "Detalle de Alimentacion - Atencion Nro." + codigo;
            button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (xcodigo != textBox1.Text.ToString())
            {
                object[] x = new object[] { textBox1.Text.ToString() };
                NegDietetica.setROW("setObservacionAtencion", x, xcodigo);
            }
            this.Close();
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            ////grid.DisplayLayout.Override.Allow

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
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            //Dimension los registros
            grid.DisplayLayout.Bands[0].Columns[0].Width = 100;
            grid.DisplayLayout.Bands[0].Columns[1].Width = 150;
            grid.DisplayLayout.Bands[0].Columns[2].Width = 250;
            grid.DisplayLayout.Bands[0].Columns[3].Width = 100;
            grid.DisplayLayout.Bands[0].Columns[4].Width = 250;
            grid.DisplayLayout.Bands[0].Columns[5].Width = 250;
        }

        private void btndevolucion_Click(object sender, EventArgs e)
        {
            UltraGridRow Fila = grid.ActiveRow;
            if (grid.Selected.Rows.Count > 0)
            {
                if (Convert.ToDouble(Fila.Cells[3].Value.ToString()) != 0)
                {
                    if (MessageBox.Show("¿Está Seguro de hacer Devolucion de Nro. Pedido: " + Fila.Cells[0].Value.ToString()
                    , "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        His.HabitacionesUI.frmDevolucionPedido x = new HabitacionesUI.frmDevolucionPedido(100, paciente, medico, hc, ate_codigo, Fila.Cells[0].Value.ToString(), Convert.ToInt64(ate_codigo));
                        x.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("El pedido ya ha sido devuelto con anterioridad", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Debe elegir un Pedido", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}