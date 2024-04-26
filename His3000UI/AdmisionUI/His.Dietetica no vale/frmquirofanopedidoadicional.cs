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
    public partial class frmquirofanopedidoadicional : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        NegHabitaciones pedido = new NegHabitaciones();
        internal static string ate_codigo; //Codigo de atencion del paciente
        internal static string pac_codigo; //Codigo del paciente
        internal static string cie_codigo; //Codigo del procedimiento
        internal static string id_usuario; //codigo de usuario
        internal static string hab_codigo;//codigo de habitacion
        public frmquirofanopedidoadicional()
        {
            InitializeComponent();
        }

        private void frmquirofanopedidoadicional_Load(object sender, EventArgs e)
        {
            toolStripImprimir.Enabled = false;
            CargarTickets();
            CargarProductos();
        }
        public void CargarTickets()
        {
            TablaTickets.DataSource = Quirofano.VerTicketsPaciente(ate_codigo);
        }
        public void CargarProductos()
        {
            UltraGridProductos.DataSource = Quirofano.PacienteProcedimiento(cie_codigo, ate_codigo);
            Redimencionar();
        }

        public void GuardarCambios()
        {
            string codpro;
            string cantidad;
            if(TablaPedidos.Rows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow item in TablaPedidos.Rows)
                    {
                        codpro = item.Cells[0].Value.ToString();
                        cantidad = item.Cells[2].Value.ToString();
                        Quirofano.ActualizarPedidoAdicional(ate_codigo, cie_codigo, codpro, cantidad);
                    }
                    MessageBox.Show("Pedido Adicional Guardado Correctamente.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Algo Ocurrio al Guardar Pedido Adicional!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Tabla de \"Productos Solicitados\" está vacia \n Agregue Productos para Continuar...",
                    "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Desea guardar los cambios?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            {
                GuardarCambios();
            }
            else
            {
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(TablaPedidos.Rows.Count > 0)
            {
                if(MessageBox.Show("¿Está Seguro de Generar Pedido?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                    == DialogResult.Yes)
                {
                    GuardarCambios();
                    GuardarPedido();
                    //this.Close();
                }
            }
            else
            {
                MessageBox.Show("Tabla de \"Productos Solicitados\" Vacio", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void GuardarPedido()
        {
            int cantidad;
            double valor, total;
            string codproducto, prodesc, numpedido;
            DateTime fecha = DateTime.Now;
            try
            {
                Quirofano.AgregarPedidoPaciente(Convert.ToString(fecha), id_usuario, ate_codigo, hab_codigo); //Primero almacena el pedido
                numpedido = Quirofano.RecuperarPedidoNum(ate_codigo); //Recupera el ultimo numero de pedido de acuerdo con la atencion
                foreach (DataGridViewRow item in TablaPedidos.Rows)
                {
                    //Aqui alamcena los productos de acuerdo a el numero de pedido generado
                    codproducto = item.Cells[0].Value.ToString();
                    prodesc = item.Cells[1].Value.ToString();
                    cantidad = Convert.ToInt32(item.Cells[2].Value.ToString());
                    valor = Convert.ToDouble(item.Cells[3].Value.ToString());
                    total = Math.Round((cantidad * valor), 2);
                    Quirofano.PedidoDetalle(codproducto, prodesc, cantidad.ToString(), valor.ToString(), total.ToString(), numpedido);
                }
                //Aqui Funcion de impresion de pedido
                GenerarTicket();
            }
            catch (Exception)
            {
                MessageBox.Show("¡Algo ocurrio al guardar pedido!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void GenerarTicket()
        {
            string numped;
            DatosQuirofanoPedido p = new DatosQuirofanoPedido();
            frmQuirofanoPedidoImprimir x = new frmQuirofanoPedidoImprimir();
            numped = Quirofano.RecuperarPedidoNum(ate_codigo); //Recupera el ultimo numero de pedido de acuerdo con la atencion
            DataTable TablaInfo = new DataTable();
            TablaInfo = Quirofano.RecuperarInfoPaciente(ate_codigo);
            foreach (DataRow item in TablaInfo.Rows)
            {
                x.ped.Clear();
                p.atencion = item[0].ToString();
                p.hc = item[1].ToString(); ;
                p.num_pedido = numped;
                p.paciente = item[2].ToString(); ;
                p.fecha = item[3].ToString(); ;
                p.medico = item[4].ToString(); ;
                p.usuario = item[5].ToString(); ;
                x.ped.Add(p);
            }
            DataTable Tabla = new DataTable(); //Almacena los productos del pedido generado recientemente
            Tabla = Quirofano.ProductosPaciente(ate_codigo, numped);
            foreach (DataRow item in Tabla.Rows)
            {
                DatosQuirofanoPedido pedidoimp = new DatosQuirofanoPedido
                {
                    cant = item[0].ToString(),
                    codigo = item[1].ToString(),
                    descripcion = item[2].ToString()
                };
                x.ped.Add(pedidoimp);
            }
            x.Show();

        }
        public void EnviarPedido()
        {
            //Aqui se enviara el pedido adicional actualmente registrado a farmacia
        }

        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UltraGridRow Fila = UltraGridProductos.ActiveRow;
            string codpro;
            string descripcion;
            string valor;
            if(UltraGridProductos.Selected.Rows.Count > 0 && txtcantidad.Text != "")
            {
                if (Convert.ToInt32(Fila.Cells[3].Value.ToString()) > 0)
                {
                    codpro = Fila.Cells[0].Value.ToString();
                    descripcion = Fila.Cells[1].Value.ToString();
                    valor = Fila.Cells[10].Value.ToString();
                    TablaPedidos.Rows.Add(codpro, descripcion, txtcantidad.Text, valor);
                    txtcantidad.Text = "";
                }
                else
                {
                    MessageBox.Show("¡No hay suficiente Stock!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("¡No se ha Elegido Producto ha Agregar!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
                
        }
        public void Redimencionar()
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

                UltraGridProductos.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

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
                UltraGridProductos.DisplayLayout.Bands[0].Columns[1].Width = 300;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[2].Width = 100;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[3].Width = 60;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridProductos.DisplayLayout.Bands[0].Columns[4].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[5].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[8].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                UltraGridProductos.DisplayLayout.Bands[0].Columns[10].Hidden = true;
            }
            catch (Exception)
            {

            }
        }

        private void UltraGridProductos_KeyDown(object sender, KeyEventArgs e)
        {
            UltraGridRow Fila = UltraGridProductos.ActiveRow;
            if (UltraGridProductos.Selected.Rows.Count > 0)
            {
                if(e.KeyCode == Keys.Enter)
                {
                    txtcantidad.Text = "1";
                    txtcantidad.Focus();
                }
            }
        }

        private void txtcantidad_KeyDown(object sender, KeyEventArgs e)
        {
            string codpro;
            string descripcion;
            string valor;
            if (e.KeyCode == Keys.Enter)
            {
                UltraGridRow Fila = UltraGridProductos.ActiveRow;
                if (UltraGridProductos.Selected.Rows.Count <= 0 && txtcantidad.Text == "")
                {
                    MessageBox.Show("¡No se ha Elegido Producto ha Agregar!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (Convert.ToInt32(Fila.Cells[3].Value.ToString()) > 0)
                    {
                        codpro = Fila.Cells[0].Value.ToString();
                        descripcion = Fila.Cells[1].Value.ToString();
                        valor = Fila.Cells[10].Value.ToString();
                        TablaPedidos.Rows.Add(codpro, descripcion, txtcantidad.Text, valor);
                        txtcantidad.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("¡No hay suficiente Stock!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void toolStripImprimir_Click(object sender, EventArgs e)
        {
            string numpedido;
            DataTable TablaPedidoProducto = new DataTable();
            DatosQuirofanoPedido p = new DatosQuirofanoPedido();
            frmQuirofanoPedidoImprimir x = new frmQuirofanoPedidoImprimir();
            x.ped.Clear();
            p.atencion = TablaTickets.CurrentRow.Cells[0].Value.ToString();
            p.hc = TablaTickets.CurrentRow.Cells[1].Value.ToString();
            p.num_pedido = TablaTickets.CurrentRow.Cells[2].Value.ToString();
            p.paciente = TablaTickets.CurrentRow.Cells[3].Value.ToString();
            p.fecha = TablaTickets.CurrentRow.Cells[4].Value.ToString();
            p.medico = TablaTickets.CurrentRow.Cells[5].Value.ToString();
            p.usuario = TablaTickets.CurrentRow.Cells[6].Value.ToString();
            x.ped.Add(p);
            if (TablaTickets.SelectedRows.Count > 0)
            {
                //Mostrara el ticket a farmacia
                numpedido = TablaTickets.CurrentRow.Cells[2].Value.ToString();
                TablaPedidoProducto = Quirofano.ProductosPaciente(ate_codigo, numpedido);
                foreach (DataRow item in TablaPedidoProducto.Rows)
                {
                    DatosQuirofanoPedido pedidoimp = new DatosQuirofanoPedido
                    {
                        cant = item[0].ToString(),
                        codigo = item[1].ToString(),
                        descripcion = item[2].ToString()
                    };
                    x.ped.Add(pedidoimp);
                }
                x.Show();
            }
            else
            {
                MessageBox.Show("¡Debe Elegir un Pedido!", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tools_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                toolStripImprimir.Enabled = false;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                toolStripImprimir.Enabled = true;
                CargarTickets();
            }
        }
    }
}
