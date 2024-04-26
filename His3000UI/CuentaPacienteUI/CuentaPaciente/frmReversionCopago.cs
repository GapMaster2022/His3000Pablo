using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;


namespace CuentaPaciente
{
    public partial class frmReversionCopago : Form
    {
        ATENCIONES ateorg = null;
        ATENCIONES atecop = null;
        Int64 ate_codigo = 0;
        string ate_numero = "";
        public frmReversionCopago()
        {
            InitializeComponent();
            cargarGrid();
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            dtpFiltroDesde.Value = oPrimerDiaDelMes;
            dtpFiltroHasta.Value = oUltimoDiaDelMes;
        }

        private void gridReversion_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = gridReversion.DisplayLayout.Bands[0];

            gridReversion.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            gridReversion.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            gridReversion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            gridReversion.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            gridReversion.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            gridReversion.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
            // ultraGridModulo.DisplayLayout.Bands[0].Columns["TODO"].Hidden = true;
        }
        public void cargarGrid()
        {
            errorProvider1.Clear();
            if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                gridReversion.DataSource = NegCopago.cuentasCopago(dtpFiltroDesde.Value, dtpFiltroHasta.Value);
            else
                errorProvider1.SetError(grpFechas, "La fecha inicial no puede ser mayo a la fecha final");
        }

        private void gridReversion_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point mousepoint = new Point(e.X, e.Y);
                contextMenuStrip1.Show(gridReversion, mousepoint);
            }
        }

        private void nuevoAccesoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObtenerIDSeleccionado();
        }

        private void gridReversion_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            //ObtenerIDSeleccionado();
        }
        private void ObtenerIDSeleccionado()
        {
            if (gridReversion.ActiveRow != null)
            {
                var cellValue = gridReversion.ActiveRow.Cells["C_ATENCION"].Value;
                var cellValueAte = gridReversion.ActiveRow.Cells["ATENCION"].Value;

                // Verificar si el valor de la celda no es nulo y es convertible a un entero
                if (cellValue != null && Int64.TryParse(cellValue.ToString(), out ate_codigo))
                {
                    ate_numero = cellValueAte.ToString();
                    reversarCopago();
                }
                else
                {
                    return;
                }
            }
        }
        private void reversarCopago()
        {
            if (estadoAtenciones())
            {
                MessageBox.Show("No se puede eliminar la cuenta copago una de las atenciones \r\n(" + atecop.ATE_NUMERO_ATENCION + ") ya fue facturada ", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Quiere eliminar el Copago de la atencion" + ate_numero, "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!NegCopago.revertirCopago(ate_codigo))
                {
                    MessageBox.Show("No se logro eliminar la cuenta copago de la atencion " + ate_numero, "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    cargarGrid();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            cargarGrid();
        }
        public bool estadoAtenciones()
        {
            bool estado = false;
            COPAGO cop = NegCopago.recuperaCopago(ate_codigo);
            ateorg = NegAtenciones.RecuperarAtencionID(cop.ATE_CODIGO);
            atecop = NegAtenciones.RecuperarAtencionID(cop.ATE_CODIGO_COPAGO);
            if (ateorg.ESC_CODIGO == 6)
                estado = true;
            if (atecop.ESC_CODIGO == 6)
                estado = true;
            return estado;
        }
    }
}
