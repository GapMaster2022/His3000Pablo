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
using His.Datos;

namespace His.Formulario
{
    public partial class frmAyudaIngElm : Form
    {
        public Int64 codigo = 0;
        public string descrip = "";
        public string tipo = "";
        public frmAyudaIngElm(Int64 ATE_CODIGO)
        {
            InitializeComponent();
            cargaInformacion(ATE_CODIGO);
        }
        public frmAyudaIngElm(Int64 MED_CODIGO,String _tipo)
        {
            InitializeComponent();
            tipo = _tipo;
            cargaPerfiles(MED_CODIGO);
        }
        private void cargaInformacion(Int64 ATE_CODIGO)
        {
            DSAyudaIngElm ds = NegIngestaEliminacion.cargaKardexCompuesto(ATE_CODIGO);
            ultraGridAyuda.DataSource = ds.Cabecera;
        }
        private void cargaPerfiles(Int64 MED_CODIGO)
        {
            ultraGridAyuda.DataSource = NegProtocoloOperatorio.listadoPerfiles(MED_CODIGO);
        }
        private void ultraGridAyuda_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            switch (tipo)
            {
                case "protocolo":
                    foreach (UltraGridRow item in ultraGridAyuda.Rows)
                    {
                        if (item.Cells["CODIGO"].Value.ToString() == e.Cell.Row.Cells["CODIGO"].Value.ToString())
                        {
                            try
                            {
                                codigo = Convert.ToInt32(e.Cell.Row.Cells["CODIGO"].Value.ToString());
                                descrip = e.Cell.Row.Cells["DESCRIPCION"].Value.ToString();
                                this.Close();
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                //throw;
                            }
                        }
                    }
                    break;
                default:
                    foreach (UltraGridRow item in ultraGridAyuda.Rows)
                    {
                        if (item.Cells["ID"].Value.ToString() == e.Cell.Row.Cells["ID"].Value.ToString())
                        {
                            try
                            {
                                codigo = Convert.ToInt32(e.Cell.Row.Cells["ID"].Value.ToString());
                                this.Close();
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                //throw;
                            }
                        }
                    }
                    break;
            }
            
        }

        private void ultraGridAyuda_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (ultraGridAyuda.Selected.Rows.Count > 0)
            {
                UltraGridRow fila = ultraGridAyuda.ActiveRow;
                codigo = Convert.ToInt64(fila.Cells[0].Value.ToString());
                this.Close();
            }
        }

        private void ultraGridAyuda_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridAyuda.DisplayLayout.Bands[0];

            ultraGridAyuda.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridAyuda.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridAyuda.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridAyuda.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridAyuda.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridAyuda.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }
    }
}
