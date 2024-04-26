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
using System.Text.RegularExpressions;

namespace His.Dietetica
{
    public partial class frmayudaQuirofano : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static bool producto; // Bool que permite cargar en el grid lista de productos
        //internal static bool procedimiento; //Bool que permite cargar lista de procedimientos
        internal static bool productosQuirofano;
        internal static bool productosPedidos; //Variable para agregar productos al formulario de pedido paciente
        public frmayudaQuirofano()
        {
            InitializeComponent();
        }

        private void frmayudaQuirofano_Load(object sender, EventArgs e)
        {
            if (producto == true)
            {
                TablaProductos.DataSource = Quirofano.MostrarProducto();
                RedimencionarTabla1();
            }
            if (productosQuirofano == true)
            {
                TablaProductos.DataSource = Quirofano.CargarTablaProductos();
                RedimencionarTabla();
            }
            if(productosPedidos == true)
            {
                TablaProductos.DataSource = Quirofano.CargarTablaProductos();
                RedimencionarTabla();
            }
        }
        public void RedimencionarTabla()
        {
            try
            {
                UltraGridBand bandUno = TablaProductos.DisplayLayout.Bands[0];

                TablaProductos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                TablaProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                TablaProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                TablaProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                TablaProductos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                TablaProductos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                TablaProductos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                TablaProductos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                TablaProductos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                TablaProductos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                TablaProductos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                TablaProductos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                TablaProductos.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                TablaProductos.DisplayLayout.Bands[0].Columns[0].Width = 60;
                TablaProductos.DisplayLayout.Bands[0].Columns[1].Width = 100;


                ////Ocultar columnas, que son fundamentales al levantar informacion
                TablaProductos.DisplayLayout.Bands[0].Columns[2].Hidden = true;
                TablaProductos.DisplayLayout.Bands[0].Columns[3].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void RedimencionarTabla1()
        {
            try
            {
                UltraGridBand bandUno = TablaProductos.DisplayLayout.Bands[0];

                TablaProductos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                TablaProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";

            TablaProductos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            TablaProductos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            TablaProductos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            TablaProductos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            TablaProductos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            TablaProductos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            TablaProductos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            TablaProductos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            TablaProductos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            TablaProductos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            TablaProductos.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            TablaProductos.DisplayLayout.Bands[0].Columns[0].Width = 60;
            TablaProductos.DisplayLayout.Bands[0].Columns[1].Width = 100;


            ////Ocultar columnas, que son fundamentales al levantar informacion
            //TablaProductos.DisplayLayout.Bands[0].Columns[2].Hidden = true;
            //TablaProductos.DisplayLayout.Bands[0].Columns[3].Hidden = true;
            //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
        }
        catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    private void TablaProductos_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow Fila = TablaProductos.ActiveRow; //eligue el numero de fila que esta seleccionada
            if (producto == true)
            {

                frmQuirofanoAgregarProducto.codpro = Fila.Cells[0].Value.ToString(); //Recoge el codigo de la tabla productos de la BD SIC CODPRO
                frmQuirofanoAgregarProducto.despro = Fila.Cells[1].Value.ToString(); //Recoge la descripcion de la tabla productos de la BD SIC DESPRO
                producto = false;
                this.Close();
            }
            if(productosQuirofano == true)
            {
                frmQuirofanoAgregarProcedimiento.codpro = Fila.Cells[0].Value.ToString(); //Recoge el codigo de la tabla productos de la BD SIC CODPRO
                frmQuirofanoAgregarProcedimiento.despro = Fila.Cells[1].Value.ToString(); //Recoge la descripcion de la tabla productos de la BD SIC DESPRO
                productosQuirofano = false;
                this.Close();
            }
            if(productosPedidos == true)
            {
                Regex reg = new Regex("[0-9]"); //Expresión que solo acepta números.
                bool numeros; //Devuelve true si el string tiene solo numeros
                frmQuirofanoPedidoPaciente.codigo_producto = Fila.Cells[0].Value.ToString();
                //controlamos que el usuarui ingrese numero y no ingrese vacios
                do
                {
                    frmQuirofanoPedidoPaciente.cant = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Cantidad del Producto ", "His3000", "", 200, 100);
                    numeros = reg.IsMatch(frmQuirofanoPedidoPaciente.cant);
                } while (numeros == false && frmQuirofanoPedidoPaciente.cant != "");
                frmQuirofanoPedidoPaciente.orden = "1";
                //controlamos que el usuario ingrese numeros y que no ingrese vacios
                //do
                //{
                //    frmQuirofanoPedidoPaciente.orden = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Orden de Salida de Producto ", "His3000", "", 200, 100);
                //    numeros = reg.IsMatch(frmQuirofanoPedidoPaciente.orden);
                //} while (numeros == false && frmQuirofanoPedidoPaciente.orden != "");
                productosPedidos = false;
                this.Close();
            }
        }
    }
}
