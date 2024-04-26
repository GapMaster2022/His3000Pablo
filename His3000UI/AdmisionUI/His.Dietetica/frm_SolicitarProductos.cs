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

namespace His.Dietetica
{
    public partial class frm_SolicitarProductos : Form
    {
        public double codsub;
        public string grupo;
        public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()); //por defecto esta la bodega 12 que es quirofano, por el hecho que no se tenia previsto una nueva
        public frm_SolicitarProductos()
        {
            InitializeComponent();
        }
        public frm_SolicitarProductos(int bodega)
        {
            InitializeComponent();
            this.bodega = bodega;
        }

        private void UltraGridProductos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = UltraGridProductos.DisplayLayout.Bands[0];

                UltraGridProductos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridProductos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridProductos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridProductos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                UltraGridProductos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridProductos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridProductos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridProductos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridProductos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridProductos.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridProductos.DisplayLayout.Bands[0].Columns[0].Width = 60;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[1].Width = 500;
                //UltraGridProductos.DisplayLayout.Bands[0].Columns[2].Width = 100;

                //Ocultar columnas, que son fundamentales al levantar informacion
                //ultraGridProducto.DisplayLayout.Bands[0].Columns[3].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frm_SolicitarProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        public void CargarProductos()
        {
            try
            {
                UltraGridProductos.DataSource = NegQuirofano.MostrarProducto(codsub);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar los productos. Consulte con el Administrador.\r\nMás detalles: " + ex.Message,
                    "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Solicitados.Rows.Count; i++)
                {
                    NegQuirofano.AgregarProducto(Solicitados.Rows[i].Cells[0].Value.ToString(), grupo, bodega);
                }
                MessageBox.Show("El/Los producto(s) han sido agregados correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al guardar los productos. Consulte con el Administrador.\r\nMás detalles: " +ex.Message,
                    "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UltraGridProductos_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (UltraGridProductos.Selected.Rows.Count > 0)
            {
                UltraGridRow fila = UltraGridProductos.ActiveRow;

                string codpro = fila.Cells[0].Value.ToString();
                string existe = NegQuirofano.SoloProductoRepetido(codpro, bodega);
                string producto = fila.Cells[1].Value.ToString();

                if (codpro != existe)
                {
                    bool existe1 = false;
                    for (int i = 0; i < Solicitados.Rows.Count; i++)
                    {
                        if (codpro == Solicitados.Rows[i].Cells[0].Value.ToString())
                        {
                            existe1 = true;
                            break;
                        }
                    }
                    if (!existe1)
                    {
                        Solicitados.Rows.Add(codpro, producto);
                    }
                    else
                    {
                        MessageBox.Show("El producto ya ha sido agregado, intente con otro producto.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("El producto ya ha sido agregado, intente con otro producto.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto para continuar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void UltraGridProductos_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (UltraGridProductos.Selected.Rows.Count > 0)
                {
                    UltraGridRow fila = UltraGridProductos.ActiveRow;

                    string codpro = fila.Cells[0].Value.ToString();
                    string existe = NegQuirofano.SoloProductoRepetido(codpro, bodega);
                    string producto = fila.Cells[1].Value.ToString();
                    if (codpro != existe)
                    {
                        Solicitados.Rows.Add(codpro, producto);
                    }
                    else
                    {
                        MessageBox.Show("El producto ya ha sido agregado, intente con otro producto.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un producto para continuar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
