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
        public string resultado = "";
        public string codigo = "";

        public bool anestesia = false;

        NegQuirofano Quirofano = new NegQuirofano();
        internal static bool producto; // Bool que permite cargar en el grid lista de productos
        //internal static bool procedimiento; //Bool que permite cargar lista de procedimientos
        internal static bool productosQuirofano;
        internal static bool productosPedidos; //Variable para agregar productos al formulario de pedido paciente

        public int bodega = Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()); //por defecfto
        public frmayudaQuirofano()
        {
            InitializeComponent();
            TablaProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            TablaProductos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            //Inicializo las opciones del timer por defecto
            timerBusqueda.Interval = 1500;

            txtBuscar.Focus();
        }

        public frmayudaQuirofano(int bodega)
        {
            InitializeComponent();
            TablaProductos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            TablaProductos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            //Inicializo las opciones del timer por defecto
            timerBusqueda.Interval = 1500;

            txtBuscar.Focus();

            this.bodega = bodega;
        }
        private void frmayudaQuirofano_Load(object sender, EventArgs e)
        {
            //cargarBusqueda();
        }

        public void cargarBusqueda()
        {
            if (anestesia == true)
            {
                TablaProductos.DataSource = Quirofano.CargarAnestesias(txtBuscar.Text, rdbPorCodigo.Checked, rdbPorDescripcion.Checked);
            }
            else if (productosQuirofano == true)
            {
                TablaProductos.DataSource = Quirofano.CargarTablaProductos(txtBuscar.Text, rdbPorCodigo.Checked, rdbPorDescripcion.Checked, bodega);
            }
            else if (productosPedidos == true)
            {
                TablaProductos.DataSource = Quirofano.CargarTablaProductos(txtBuscar.Text, rdbPorCodigo.Checked, rdbPorDescripcion.Checked, bodega);
            }
        }

    private void TablaProductos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow Fila = TablaProductos.ActiveRow; //eligue el numero de fila que esta seleccionada
                if (producto == true)
                {

                    frmQuirofanoAgregarProducto.codpro = Fila.Cells[0].Value.ToString(); //Recoge el codigo de la tabla productos de la BD SIC CODPRO
                    frmQuirofanoAgregarProducto.despro = Fila.Cells[1].Value.ToString(); //Recoge la descripcion de la tabla productos de la BD SIC DESPRO
                    producto = false;
                    this.Close();
                }
                if (productosQuirofano == true)
                {
                    frmQuirofanoAgregarProcedimiento.codpro = Fila.Cells[0].Value.ToString(); //Recoge el codigo de la tabla productos de la BD SIC CODPRO
                    frmQuirofanoAgregarProcedimiento.despro = Fila.Cells[1].Value.ToString(); //Recoge la descripcion de la tabla productos de la BD SIC DESPRO
                    productosQuirofano = false;
                    this.Close();
                }
                if (productosPedidos == true)
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
                if(anestesia == true)
                {
                    //Cambios 20210607
                    frm_QuirofanoNuevo.codigo_producto = Fila.Cells["CODIGO"].Value.ToString();
                    anestesia = false;
                    this.Close();
                }
            }
            catch (Exception)
            {
                
            }
            
        }

        private void TablaProductos_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {

                //UltraGridDatos.DisplayLayout.Bands[0].ColHeadersVisible = false;

                TablaProductos.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 80;
                TablaProductos.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 450;
                //ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_IDPADRE"].Hidden = true;


                TablaProductos.DisplayLayout.Bands[0].Columns["CODIGO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                TablaProductos.DisplayLayout.Bands[0].Columns["DESCRIPCION"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                //ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_CODIGO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                //ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_DESCRIPCION"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                //ulgdbListadoCIE.DisplayLayout.Bands[1].Columns["CIE_DESCRIPCION"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;

                TablaProductos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                TablaProductos.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                TablaProductos.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;

                //e.Layout.Rows.ExpandAllCards();
                //e.Row.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
                //e.Row.Expanded = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void TablaProductos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //resultado = TablaProductos.ActiveRow.Cells[1].Text;
                //codigo = TablaProductos.ActiveRow.Cells[0].Text;
                //this.Close();

                if (productosQuirofano == true)
                {
                    frmQuirofanoAgregarProcedimiento.codpro = TablaProductos.ActiveRow.Cells[0].Text; //Recoge el codigo de la tabla productos de la BD SIC CODPRO
                    frmQuirofanoAgregarProcedimiento.despro = TablaProductos.ActiveRow.Cells[1].Text; //Recoge la descripcion de la tabla productos de la BD SIC DESPRO
                    productosQuirofano = false;
                    this.Close();
                }
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chkAutoBusqueda.Checked == true)
            {
                if (!timerBusqueda.Enabled)
                {
                    timerBusqueda.Start();
                }
            }
        }

        private void chkAutoBusqueda_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoBusqueda.Checked)
            {
                timerBusqueda.Enabled = true;
            }
            else
            {
                timerBusqueda.Stop();
                timerBusqueda.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarBusqueda();
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (timerBusqueda.Enabled)
            {
                timerBusqueda.Stop();
            }
        }

        private void TablaProductos_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (TablaProductos.ActiveRow.Index > 0)
            {
                //resultado = TablaProductos.ActiveRow.Cells[1].Value.ToString();
                //codigo = TablaProductos.ActiveRow.Cells[0].Value.ToString();

                if (productosQuirofano == true)
                {
                    frmQuirofanoAgregarProcedimiento.codpro = TablaProductos.ActiveRow.Cells[0].Value.ToString(); //Recoge el codigo de la tabla productos de la BD SIC CODPRO
                    frmQuirofanoAgregarProcedimiento.despro = TablaProductos.ActiveRow.Cells[1].Value.ToString(); //Recoge la descripcion de la tabla productos de la BD SIC DESPRO
                    productosQuirofano = false;
                    this.Close();
                }
            }
            //this.Close();
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            try
            {
                btnBuscar_Click(null, null);
                timerBusqueda.Stop();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
