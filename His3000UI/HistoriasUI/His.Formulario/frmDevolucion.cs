using His.Admision;
using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace His.Formulario
{
    public partial class frmDevolucion : Form
    {
        public frmDevolucion()
        {
            InitializeComponent();
        }
        public string ped_codigo;
        public string ate_codigo;
        public string medico;
        PACIENTES paciente = new PACIENTES();
        ATENCIONES ultimaAtencion = new ATENCIONES();
        public List<DtoPedidoDevolucionDetalle> PedidosDetalle = new List<DtoPedidoDevolucionDetalle>();
        public DtoPedidoDevolucionDetalle PedidosDetalleItem = null;
        private void frmDevolucion_Load(object sender, EventArgs e)
        {
            gb_Pedido.Text = "Pedido Nº " + ped_codigo;
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));

            lblPaciente.Text += paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
            lblHc.Text += paciente.PAC_HISTORIA_CLINICA;
            lblMedico.Text += medico;
            lblAtencion.Text += ultimaAtencion.ATE_NUMERO_ATENCION;

            CargarPedido();
            ultraGridPedido.Focus();

        }
        public void CargarPedido()
        {
            try
            {
                ultraGridPedido.DataSource = NegProducto.RecuperarPedido(Convert.ToInt32(ped_codigo));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ultraGridPedido_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridPedido.DisplayLayout.Bands[0];

            ultraGridPedido.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGridPedido.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridPedido.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridPedido.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;


            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridPedido.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridPedido.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridPedido.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            ultraGridPedido.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridPedido.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridPedido.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridPedido.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridPedido.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGridPedido.DisplayLayout.UseFixedHeaders = true;


            bandUno.Columns["PRO_CODIGO"].Header.Caption = "CODIGO";
            bandUno.Columns["PRO_DESCRIPCION"].Header.Caption = "PRODUCTO";
            bandUno.Columns["PDD_CANTIDAD"].Header.Caption = "CANTIDAD";
            bandUno.Columns["CantidadDevuelta"].Header.Caption = "CANT. DEVUELTA";
            bandUno.Columns["PDD_VALOR"].Header.Caption = "V. UNITARIO";
            bandUno.Columns["PDD_IVA"].Header.Caption = "V. IVA";
            bandUno.Columns["PDD_TOTAL"].Header.Caption = "V. TOTAL";

            bandUno.Columns[0].Hidden = true;
            bandUno.Columns[1].Hidden = true;
            bandUno.Columns[9].Hidden = true;
            bandUno.Columns[10].Hidden = true;
            bandUno.Columns[11].Hidden = true;
            bandUno.Columns[12].Hidden = true;
            bandUno.Columns[13].Hidden = true;
            bandUno.Columns[14].Hidden = true;
            bandUno.Columns[15].Hidden = true;

            bandUno.Columns[3].Width = 280;
        }

        private void AgregarProducto()
        {
            Errores.Clear();
            if (ultraGridPedido.Selected.Rows.Count == 1)
            {
                UltraGridRow Fila = ultraGridPedido.ActiveRow;


                if (!NegParametros.ParametroDevolucionBienes())
                {
                    if (ultraGridDevolucion.Rows.Count > 0) //Verifico si tengo producto agregados
                    {
                        foreach (UltraGridRow item in ultraGridDevolucion.Rows)
                        {
                            if (Fila.Cells[2].Value.ToString() == item.Cells["PRO_CODIGO"].Value.ToString()) //Verifico si ya fue ingresado
                            {
                                MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ultraGridDevolucion.Focus();
                                return;
                            }
                        }
                    }


                    if ((Convert.ToDecimal(Fila.Cells[4].Value) - Convert.ToDecimal(Fila.Cells[5].Value)) - (Convert.ToDecimal(txtCantidad.Text)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                    {
                        PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                        PedidosDetalleItem.DevCodigo = 1;
                        PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(Fila.Cells[2].Value);
                        PedidosDetalleItem.PRO_DESCRIPCION = Fila.Cells[3].Value.ToString();
                        PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(txtCantidad.Text);
                        PedidosDetalleItem.DevDetValor = Convert.ToDecimal(Fila.Cells[6].Value);
                        PedidosDetalleItem.DevDetIva = ((((Convert.ToDecimal(Fila.Cells[6].Value) * Convert.ToDecimal(Convert.ToDouble(txtCantidad.Text)))) * Convert.ToDecimal(Fila.Cells[7].Value)) / 100);
                        PedidosDetalleItem.DevDetIvaTotal = Convert.ToDecimal(Fila.Cells[6].Value) * Convert.ToDecimal(Convert.ToDouble(txtCantidad.Text));
                        PedidosDetalleItem.PDD_CODIGO = Convert.ToInt64(Fila.Cells[2].Value);

                        PedidosDetalle.Add(PedidosDetalleItem);

                        ultraGridDevolucion.DataSource = PedidosDetalle.ToList();
                        RediseñarGrid();

                    }
                    else
                    {
                        MessageBox.Show("La cantidad devuelta no puede ser mayor a la cantidad ingresada en el pedido..", "His3000");

                    }
                }
                else
                {
                    if (Entidades.Clases.Sesion.codDepartamento == 1 || Entidades.Clases.Sesion.codDepartamento == 14) //Pueden hacer devoluciones para todo
                    {
                        if (ultraGridDevolucion.Rows.Count > 0) //Verifico si tengo producto agregados
                        {
                            foreach (UltraGridRow item in ultraGridDevolucion.Rows)
                            {
                                if (Fila.Cells[2].Value.ToString() == item.Cells["PRO_CODIGO"].Value.ToString()) //Verifico si ya fue ingresado
                                {
                                    MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    ultraGridDevolucion.Focus();
                                    return;
                                }
                            }
                        }

                        if ((Convert.ToDecimal(Fila.Cells[4].Value) - Convert.ToDecimal(Fila.Cells[5].Value)) - (Convert.ToDecimal(txtCantidad.Text)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                        {
                            PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                            PedidosDetalleItem.DevCodigo = 1;
                            PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(Fila.Cells[2].Value);
                            PedidosDetalleItem.PRO_DESCRIPCION = Fila.Cells[3].Value.ToString();
                            PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(txtCantidad.Text);
                            PedidosDetalleItem.DevDetValor = Convert.ToDecimal(Fila.Cells[6].Value);
                            PedidosDetalleItem.DevDetIva = ((((Convert.ToDecimal(Fila.Cells[6].Value) * Convert.ToDecimal(Convert.ToDouble(txtCantidad.Text)))) * Convert.ToDecimal(Fila.Cells[7].Value)) / 100);
                            PedidosDetalleItem.DevDetIvaTotal = Convert.ToDecimal(Fila.Cells[6].Value) * Convert.ToDecimal(Convert.ToDouble(txtCantidad.Text));
                            PedidosDetalleItem.PDD_CODIGO = Convert.ToInt64(Fila.Cells[2].Value);

                            PedidosDetalle.Add(PedidosDetalleItem);

                            ultraGridDevolucion.DataSource = PedidosDetalle.ToList();
                            RediseñarGrid();

                        }
                        else
                        {
                            MessageBox.Show("La cantidad devuelta no puede ser mayor a la cantidad ingresada en el pedido..", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        DataTable ProductoB = new DataTable();
                        ProductoB = NegProducto.RecuperarProductoSic(Fila.Cells[2].Value.ToString());

                        if (ProductoB.Rows[0]["clasprod"].ToString().Trim() == "B")
                        {
                            MessageBox.Show("No tiene permisos para hacer devolución de bienes.\r\nConsulte con el Sistemas.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (ultraGridDevolucion.Rows.Count > 0) //Verifico si tengo producto agregados
                            {
                                foreach (UltraGridRow item in ultraGridDevolucion.Rows)
                                {
                                    if (Fila.Cells[2].Value.ToString() == item.Cells["PRO_CODIGO"].Value.ToString()) //Verifico si ya fue ingresado
                                    {
                                        MessageBox.Show("El item seleccionado ya esta ingresado. Verifique por favor.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        ultraGridDevolucion.Focus();
                                        return;
                                    }
                                }
                            }

                            if ((Convert.ToDecimal(Fila.Cells[4].Value) - Convert.ToDecimal(Fila.Cells[5].Value)) - (Convert.ToDecimal(txtCantidad.Text)) >= 0) // Verifica si la existe la cantidad seleccionada / Giovanny Tapia / 04/10/2012
                            {
                                PedidosDetalleItem = new DtoPedidoDevolucionDetalle();
                                PedidosDetalleItem.DevCodigo = 1;
                                PedidosDetalleItem.PRO_CODIGO = Convert.ToInt64(Fila.Cells[2].Value);
                                PedidosDetalleItem.PRO_DESCRIPCION = Fila.Cells[3].Value.ToString();
                                PedidosDetalleItem.DevDetCantidad = Convert.ToDouble(txtCantidad.Text);
                                PedidosDetalleItem.DevDetValor = Convert.ToDecimal(Fila.Cells[6].Value);
                                PedidosDetalleItem.DevDetIva = ((((Convert.ToDecimal(Fila.Cells[6].Value) * Convert.ToDecimal(txtCantidad.Text))) * Convert.ToDecimal(Fila.Cells[7].Value)) / 100);
                                PedidosDetalleItem.DevDetIvaTotal = Convert.ToDecimal(Fila.Cells[6].Value) * Convert.ToDecimal(txtCantidad.Text);
                                PedidosDetalleItem.PDD_CODIGO = Convert.ToInt64(Fila.Cells[2].Value);

                                PedidosDetalle.Add(PedidosDetalleItem);

                                ultraGridDevolucion.DataSource = PedidosDetalle.ToList();
                                RediseñarGrid();
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("Debe seleccionar producto ha devolver.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void RediseñarGrid()
        {
            UltraGridBand bandUno = ultraGridDevolucion.DisplayLayout.Bands[0];

            ultraGridDevolucion.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGridDevolucion.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridDevolucion.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridDevolucion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;


            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridDevolucion.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridDevolucion.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridDevolucion.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            ultraGridDevolucion.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridDevolucion.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridDevolucion.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridDevolucion.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridDevolucion.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGridDevolucion.DisplayLayout.UseFixedHeaders = true;


            bandUno.Columns["PRO_CODIGO"].Header.Caption = "CODIGO";
            bandUno.Columns["PRO_DESCRIPCION"].Header.Caption = "PRODUCTO";
            bandUno.Columns["DevDetCantidad"].Header.Caption = "CANTIDAD";
            bandUno.Columns["DevDetValor"].Header.Caption = "V. UNITARIO";
            bandUno.Columns["DevDetIva"].Header.Caption = "V. IVA";
            bandUno.Columns["DevDetIvaTotal"].Header.Caption = "V. TOTAL";

            bandUno.Columns[0].Hidden = true;
            bandUno.Columns[7].Hidden = true;

            bandUno.Columns[2].Width = 280;
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCantidad.Text.Trim() != "")
                {
                    if (txtCantidad.Text != "0")
                    {
                        UltraGridRow Fila = ultraGridPedido.ActiveRow;
                        string isDecimal = txtCantidad.Text.Trim();
                        bool valido = false;
                        for (int i = 0; i < isDecimal.Length; i++)
                        {
                            if (isDecimal.Substring(i, 1) == ".")
                                valido = true;
                        }
                        if (valido)
                        {
                            if (!NegQuirofano.validaDecimales(Fila.Cells["PRO_CODIGO"].Value.ToString()))
                            {
                                MessageBox.Show("El producto no permite decimales.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtCantidad.Text = "1";
                                return;
                            }
                        }
                        AgregarProducto();
                        ultraGridPedido.Focus();
                        txtCantidad.Text = "";
                    }
                    else
                        txtCantidad.Text = "1";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumberDecimal(e, false);
        }

        private void ultraGridPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ultraGridPedido.Selected.Rows.Count == 1)
            {
                txtCantidad.Text = "1";
                txtCantidad.Focus();
            }
        }

        private void ultraGridPedido_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if (ultraGridPedido.Selected.Rows.Count == 1)
            {
                txtCantidad.Text = "1";
                txtCantidad.Focus();
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCantidad.Text.Trim() != "")
                {
                    if (txtCantidad.Text != "0")
                    {
                        UltraGridRow Fila = ultraGridPedido.ActiveRow;
                        string isDecimal = txtCantidad.Text.Trim();
                        bool valido = false;
                        for (int i = 0; i < isDecimal.Length; i++)
                        {
                            if (isDecimal.Substring(i, 1) == ".")
                                valido = true;
                        }
                        if (valido)
                        {
                            if (!NegQuirofano.validaDecimales(Fila.Cells["PRO_CODIGO"].Value.ToString()))
                            {
                                MessageBox.Show("El producto no permite decimales.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtCantidad.Text = "1";
                                return;
                            }
                        }
                        AgregarProducto();
                        ultraGridPedido.Focus();
                        txtCantidad.Text = "";
                    }
                    else
                        txtCantidad.Text = "1";
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Errores.Clear();
            if (ultraGridDevolucion.Rows.Count > 0)
            {
                if (txtObservacion.Text.Trim() != "")
                {
                    foreach (var item in PedidosDetalle)// validaciones no genere devoluciones en negativo // MarioValencia // 10-10-2023
                    {
                        decimal cantidadReal = NegPedidos.validaCantidad(Convert.ToInt64(ate_codigo), Convert.ToString(item.PRO_CODIGO), Convert.ToInt64(ped_codigo));
                        if (cantidadReal < 1)
                        {
                            MessageBox.Show("El Siguiente Producto: " + item.PRO_DESCRIPCION.ToString() + ", No Cuenta Con Stock Suficiente\r\nCantidad a devolver: " + item.DevDetCantidad + ".\r\nDebe Ser Removido Para Continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    DtoPedidoDevolucion Devolucion = new DtoPedidoDevolucion();

                    Devolucion.DevCodigo = 1;
                    Devolucion.Ped_Codigo = Convert.ToInt64(ped_codigo);
                    Devolucion.DevFecha = Convert.ToDateTime(DateTime.Now.ToString());
                    Devolucion.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                    Devolucion.DevObservacion = txtObservacion.Text.ToUpper(); //AQUI SE VA A LA TABLA DE PEDIDO_DEVOLUCION LA RAZON DE DEVOLUCION AQUI ANTES ESTABA  = ""
                    Devolucion.IP_MAQUINA = Sesion.ip;

                    Devolucion.DetalleDevolucion = PedidosDetalle;
                    Int64 DevolucionNumero = NegPedidos.CrearDevolucionPedido(Devolucion, Convert.ToInt64(ate_codigo), 0);

                    if (DevolucionNumero != 0)
                    {
                        //MessageBox.Show("La devolución No." + " " + DevolucionNumero.ToString() + " a sido ingresada correctamente.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //if (MessageBox.Show("¿Desea Imprimir Directamente a impresora por defecto?",
                        //    "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                        //{
                            frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Convert.ToInt32(DevolucionNumero), 100, 1, 0);
                            frmPedidos.ShowDialog();
                        //}
                        //else
                        //{
                        //Imprimir(100, Convert.ToInt32(DevolucionNumero), 0);
                        //}
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("La devolución no se a guardado. Consulte con el administrador del sistema.", "His3000");
                    }
                }
                else
                    Errores.SetError(txtObservacion, "Debe agregar una razón de la devolución.");
            }
            else
                Errores.SetError(btnAceptar, "No tiene productos para realizar devolución.");
        }

        private void ultraGridDevolucion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (ultraGridDevolucion.Selected.Rows.Count == 1)
                {
                    UltraGridRow Fila = ultraGridDevolucion.ActiveRow;
                    foreach (var item in PedidosDetalle)
                    {
                        if (item.PRO_CODIGO.ToString() == Fila.Cells["PRO_CODIGO"].Value.ToString())
                        {
                            PedidosDetalle.Remove(item);
                            ultraGridDevolucion.DataSource = PedidosDetalle.ToList();
                            RediseñarGrid();
                            break;
                        }
                    }
                }
                else
                    MessageBox.Show("Debe elegir un producto que desee eliminar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
