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
    public partial class frmAyudaFormasPagos : Form
    {
        List<FORMA_PAGO> listaFormaPago = new List<FORMA_PAGO>();
        //public FORMA_PAGO formaPago = new FORMA_PAGO();
        public string codigoFormaPago;
        public string nombreFormaPago;

        public frmAyudaFormasPagos()
        {
            InitializeComponent();
            cargarFormasPagos();
        }

        private void cargarFormasPagos() 
        {  
            DataTable TABLA = NegFactura.FormaPagoSic(false, 0);
            ultraGridFormasPago.DataSource = TABLA;
        }
        

        private void ultraGridFormasPago_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridFormasPago.DisplayLayout.Bands[0];
            //Caracteristicas de Filtro en la grilla
            //ultraGridFormasPago.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            //ultraGridFormasPago.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            //ultraGridFormasPago.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            //ultraGridFormasPago.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //ultraGridFormasPago.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            ultraGridFormasPago.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            ultraGridFormasPago.DisplayLayout.GroupByBox.Hidden = true;
            ultraGridFormasPago.DisplayLayout.Bands[0].SortedColumns.Add("TIPO DE PAGO", false, true);
            ultraGridFormasPago.DisplayLayout.Bands[0].Columns["FORMA PAGO"].Width = 300;
            
            bandUno.Columns["FORMA PAGO"].Hidden = false;
            bandUno.Columns["CODIGO"].Hidden = false;

            // Oculto los campos que no necesito / Giovanny tapia / 07/01/2012
            bandUno.Columns["claspag"].Hidden = true;
            bandUno.Columns["codcue"].Hidden = true;
            bandUno.Columns["ActivarFactura"].Hidden = true;
            bandUno.Columns["ActivarCuentas"].Hidden = true;
            bandUno.Columns["ActivarProveedor"].Hidden = true;
            bandUno.Columns["comision"].Hidden = true;
            bandUno.Columns["tPago"].Hidden = true;
            bandUno.Columns["CXC"].Hidden = true;
            bandUno.Columns["tarjeta"].Hidden = true;

        }

        private void ultraGridFormasPago_DoubleClick(object sender, System.EventArgs e)
        {
            UltraGridRow fila = ultraGridFormasPago.ActiveRow; //Cambios Edgar 20210203
            if(fila.Cells != null)
            {
                codigoFormaPago = fila.Cells["CODIGO"].Value.ToString();
                //codigoFormaPago = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells["CODIGO"].Value.ToString();
                //nombreFormaPago = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells["FORMA PAGO"].Value.ToString();
                nombreFormaPago = fila.Cells["FORMA PAGO"].Value.ToString();
                if (codigoFormaPago != null)
                    this.Close();
            }
           
            //codigoFormaPago = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells[0].Value.ToString();
            //nombreFormaPago = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells[1].Value.ToString();
            //if (codigoFormaPago != null)
            //    this.Close();
        }

        private void ultraGridFormasPago_KeyDown(object sender, KeyEventArgs e)
        {
            
            UltraGridRow fila = ultraGridFormasPago.ActiveRow; //Cambios Edgar 20210203
            if(fila.Cells != null)
            {
                string aux = fila.Cells["CODIGO"].Value.ToString();

                fila.Cells["CODIGO"].Value = aux;
                if (e.KeyCode == Keys.Enter)
                {
                    codigoFormaPago = fila.Cells["CODIGO"].Value.ToString();
                    //codigoFormaPago = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells["CODIGO"].Value.ToString();
                    //nombreFormaPago = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells["FORMA PAGO"].Value.ToString();
                    nombreFormaPago = fila.Cells["FORMA PAGO"].Value.ToString();
                    if (codigoFormaPago != null)
                        this.Close();
                }
            }
           if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void ultraGridFormasPago_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            UltraGridRow fila = ultraGridFormasPago.ActiveRow; //Cambios Edgar 20210203
            if(fila.Cells != null)
            {
                codigoFormaPago = fila.Cells["CODIGO"].Value.ToString();
                //codigoFormaPago = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells["CODIGO"].Value.ToString();
                //nombreFormaPago = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells["FORMA PAGO"].Value.ToString();
                nombreFormaPago = fila.Cells["FORMA PAGO"].Value.ToString();
                if (codigoFormaPago != null)
                    this.Close();
            }
        }
    }
}
