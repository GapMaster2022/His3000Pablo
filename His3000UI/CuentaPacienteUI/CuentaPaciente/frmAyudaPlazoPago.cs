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
using Infragistics.Win.UltraWinGrid;

namespace CuentaPaciente
{
    public partial class frmAyudaPlazoPago : Form
    {

        public string codigoFormaPago;
        public string nombreFormaPago;
        public frmAyudaPlazoPago(int forpag)
        {
            InitializeComponent();
            cargarFormasPagos(forpag);
        }

        private void cargarFormasPagos(int forpag)
        {

            DataTable plazoPago = new DataTable();
            plazoPago = NegFactura.PlazoPagoSic(forpag);
            if (plazoPago.Rows.Count > 0)
            {
                dataGridView1.DataSource = plazoPago;
                //dataGridView1.Rows[2].Visible = false;
                //dataGridView1.Rows[3].Visible = false;
                dataGridView1.Columns[1].Width = 350;
            }
            else
            {
                MessageBox.Show("No Hay Plazo Para Esta Forma De Pago", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ultraGridPlazoPago_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    codigoFormaPago = ultraGridPlazoPago.Rows[ultraGridPlazoPago.ActiveRow.Index].Cells[0].Value.ToString();
            //    nombreFormaPago = ultraGridPlazoPago.Rows[ultraGridPlazoPago.ActiveRow.Index].Cells[1].Value.ToString();
            //    if (codigoFormaPago != null)
            //        this.Close();
            //}
        }

        private void ultraGridPlazoPago_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            //UltraGridBand bandUno = ultraGridPlazoPago.DisplayLayout.Bands[0];
            ////Caracteristicas de Filtro en la grilla
            //ultraGridPlazoPago.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            //ultraGridPlazoPago.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            //ultraGridPlazoPago.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            //ultraGridPlazoPago.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //ultraGridPlazoPago.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            //ultraGridPlazoPago.DisplayLayout.Bands[0].Columns["DESCRIPCION PAGO"].Width = 300;            
        }

        private void ultraGridPlazoPago_DoubleClick(object sender, EventArgs e)
        {
            //codigoFormaPago = ultraGridPlazoPago.Rows[ultraGridPlazoPago.ActiveRow.Index].Cells[0].Value.ToString();
            //nombreFormaPago = ultraGridPlazoPago.Rows[ultraGridPlazoPago.ActiveRow.Index].Cells[1].Value.ToString();
            //if (codigoFormaPago != null)
            //    this.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataGridViewRow fila = dataGridView1.CurrentRow;
                codigoFormaPago = fila.Cells[0].Value.ToString();
                nombreFormaPago = fila.Cells[1].Value.ToString();
                this.Close();
            }
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //codigoFormaPago = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            //nombreFormaPago = (string)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
            //this.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow fila = dataGridView1.CurrentRow;
                codigoFormaPago = fila.Cells[0].Value.ToString();
                nombreFormaPago = fila.Cells[1].Value.ToString();
                this.Close();
            }
            
        }
    }
}
