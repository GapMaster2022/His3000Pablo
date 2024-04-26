using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using Core.Entidades;

namespace CuentaPaciente
{
    public partial class frmAyudaCategorias : Form
    {
        bool estadoGrid = false;
        List<DtoPedidoDetalleOtros> PedidosDetalle = new List<DtoPedidoDetalleOtros>();
        public DtoPedidoOtros PedidoEncabezado = new DtoPedidoOtros();
        int _CodigoAtencion = 0;
        int _Convenio = 0;

        public frmAyudaCategorias(int CodigoAtencion,int Convenio)
        {
            _CodigoAtencion = CodigoAtencion;
            _Convenio = Convenio;
            InitializeComponent();
        }

        private void frmAyudaCategorias_Load(object sender, EventArgs e)
        {
            try
            {
                gridPconvenios.DataSource = NegCatalogoCostos.RecuperarEstructuraCatalogos();
                //CATEGORIAS_CONVENIOS convenio = (CATEGORIAS_CONVENIOS )Cmb_Convenios.SelectedItem;
                List<PRECIOS_POR_CONVENIOS> preciosConveniosLista = NegPrecioConvenios.RecuperarPreciosPorConvenio(Convert.ToInt16(_Convenio));

                for (Int16 i = 0; i < gridPconvenios.Rows.Count; i++)
                {
                    for (Int16 j = 0; j < gridPconvenios.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        foreach (var item in preciosConveniosLista)
                        {
                            if (Convert.ToInt32(gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["CAC_CODIGO"].Value) == item.CATALOGO_COSTOS.CAC_CODIGO)
                            {
                                gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaCheck"].Value=true;
                                //gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                //gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                                gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Value=item.PRE_VALOR;
                                gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Value = item.PRE_PORCENTAJE;

                            }
                        }

                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);     
            }
        }

        private void gridPconvenios_InitializeGroupByRow(object sender, Infragistics.Win.UltraWinGrid.InitializeGroupByRowEventArgs e)
        {
            e.Row.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            e.Row.Expanded = true;
        }

        private void gridPconvenios_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            if (estadoGrid == false)
            {
                gridPconvenios.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                gridPconvenios.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
                gridPconvenios.DisplayLayout.Bands[2].Hidden = true;

                //Añado la columna check
                gridPconvenios.DisplayLayout.Bands[1].Columns.Add("columnaCheck", "");
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaCheck"].DataType = typeof(bool);
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaCheck"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

                gridPconvenios.DisplayLayout.Bands[1].Columns.Add("columnaPrecio", "");
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].DataType = typeof(Decimal);
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CurrencyNonNegative;
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].Header.Caption = "Precio";

                gridPconvenios.DisplayLayout.Bands[1].Columns.Add("columnaPorcentaje", "");
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].DataType = typeof(Decimal);
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CurrencyNonNegative;
                gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Header.Caption = "Porcentaje";

                gridPconvenios.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                gridPconvenios.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                gridPconvenios.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;

                estadoGrid = true;
            }
            gridPconvenios.DisplayLayout.Bands[0].Columns["CCT_CODIGO"].Hidden = true;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CATALOGO_COSTOS_TIPO"].Hidden = true;
            //gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_CODIGO"].Hidden = true;
            //
            gridPconvenios.DisplayLayout.Bands[0].Columns["CCT_NOMBRE"].Header.Caption = "Tipo Costo";
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].Header.Caption = "Descripcion";
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_CODIGO"].Header.Caption = "Codigo";

            //
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaCheck"].Header.VisiblePosition = 2;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].Header.VisiblePosition = 1;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].Header.VisiblePosition = 3;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Header.VisiblePosition = 4;

            //
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaCheck"].Width = 30;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].Width = 70;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].Width = 70;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].Width = 350;

            //
            gridPconvenios.DisplayLayout.Bands[0].Columns["CCT_NOMBRE"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPrecio"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            gridPconvenios.DisplayLayout.Bands[1].Columns["columnaPorcentaje"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_NOMBRE"].CellActivation= Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            gridPconvenios.DisplayLayout.Bands[1].Columns["CAC_CODIGO"].CellActivation= Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            for (Int16 i = 0; i < gridPconvenios.Rows.Count; i++)
            {
                for (Int16 j = 0; j < gridPconvenios.Rows[i].ChildBands[0].Rows.Count; j++)
                {
                    gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPrecio"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    gridPconvenios.Rows[i].ChildBands[0].Rows[j].Cells["columnaPorcentaje"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }
            }
        }
        public void CargarProducto()
        {
            DtoPedidoDetalleOtros PedidosDetalleItem = null;

            PedidosDetalleItem = new DtoPedidoDetalleOtros();

            PedidosDetalleItem.PDD_CODIGO = 1;
            PedidosDetalleItem.PRO_CODIGO = Convert.ToInt32(gridPconvenios.ActiveRow.Cells["CAC_CODIGO"].Value);
            PedidosDetalleItem.PDD_CANTIDAD = Convert.ToInt32(this.txt_Cantidad.Text);
            PedidosDetalleItem.PRO_DESCRIPCION = Convert.ToString(gridPconvenios.ActiveRow.Cells["CAC_NOMBRE"].Value);
            PedidosDetalleItem.PDD_IVA = 0;
            PedidosDetalleItem.PDD_VALOR = /*Convert.ToDecimal(Item.Cells["VALOR"].Value)*/ Convert.ToDecimal(this.txtValorUnitario.Text);
            PedidosDetalleItem.PDD_TOTAL = (/*Convert.ToDecimal(Item.Cells["VALOR"].Value)*/Convert.ToDecimal(this.txtValorUnitario.Text) * Convert.ToInt32(this.txt_Cantidad.Text)) + PedidosDetalleItem.PDD_IVA;
            PedidosDetalleItem.PDD_ESTADO = true;
            PedidosDetalleItem.PDD_COSTO = 0;
            PedidosDetalleItem.PDD_FACTURA = "";
            PedidosDetalleItem.PDD_ESTADO_FACTURA = 0;
            PedidosDetalleItem.PDD_FECHA_FACTURA = "";
            PedidosDetalleItem.PDD_RESULTADO = "";
            PedidosDetalleItem.PRO_CODIGO_BARRAS = Convert.ToString(gridPconvenios.ActiveRow.Cells["CAC_CODIGO"].Value);


            PedidosDetalle.Insert(0, PedidosDetalleItem);


            gridProductosSeleccionados.DataSource = PedidosDetalle.Select(
                p => new
                {
                    CODIGO = p.PRO_CODIGO,
                    DESCRIPCION = p.PRO_DESCRIPCION,
                    CANTIDAD = p.PDD_CANTIDAD,
                    VALOR = p.PDD_VALOR,
                    IVA = p.PDD_IVA,
                    TOTAL = p.PDD_TOTAL
                }
                ).Distinct().ToList();
        }
        private void btn_Anadir_Click(object sender, EventArgs e)
        {
            NegFactura factura = new NegFactura();
            if (!NegCuentasPacientes.LlamaParametro())
            {
                CargarProducto();
            }
            else
            {
                if(NegCuentasPacientes.VerificaBien(Convert.ToInt64(gridPconvenios.ActiveRow.Cells["CAC_CODIGO"].Value)) == "S")
                {
                    CargarProducto();
                }
                else
                {
                    MessageBox.Show("No tiene Autorización", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void gridPconvenios_BeforeSelectChange(object sender, Infragistics.Win.UltraWinGrid.BeforeSelectChangeEventArgs e)
        {
            try
            {
                if (gridPconvenios.ActiveRow.Cells["columnaPrecio"].Value != null)
                {
                    this.txtValorUnitario.Text = gridPconvenios.ActiveRow.Cells["columnaPrecio"].Value.ToString();
                }
                else
                {
                    this.txtValorUnitario.Text = "0";
                }
            }
            catch{}
        }

        private void gridProductosSeleccionados_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["CODIGO"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 400;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["CANTIDAD"].Width = 100;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["VALOR"].Width = 120;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["IVA"].Width = 120;
            gridProductosSeleccionados.DisplayLayout.Bands[0].Columns["TOTAL"].Width = 120;
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Esta seguro que desea guardar el pedido.?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.No))
            {
                return;
            }
            GuardaPedido();
            this.Dispose();
        }

        private void GuardaPedido()
        { 
            
            PedidoEncabezado.PED_CODIGO=0;
            PedidoEncabezado.PEA_CODIGO=18;/*Pedido Area*/
            PedidoEncabezado.PEE_CODIGO=4; /*Pedido Estacion*/
            PedidoEncabezado.PED_DESCRIPCION="Otros Rubros";
            PedidoEncabezado.PED_ESTADO=1;
            PedidoEncabezado.PED_FECHA=Convert.ToDateTime(DateTime.Now.ToString());
            PedidoEncabezado.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
            PedidoEncabezado.ATE_CODIGO = _CodigoAtencion;
            PedidoEncabezado.TIP_PEDIDO=1;
            PedidoEncabezado.PED_PRIORIDAD=1;
            PedidoEncabezado.PED_TRANSACCION=0;
            PedidoEncabezado.MED_CODIGO = 0;

            PedidoEncabezado.DetallePedidoOtros=PedidosDetalle;

            try
            {
                NegPedidos.CreaPedido(PedidoEncabezado,Convert.ToInt64(txtVale.Text));
                MessageBox.Show("Los datos se han almacenado correctamente", "His3000");
            }
            catch
            {
                MessageBox.Show("Error");
            }
            

        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Esta seguro que desea cancelar el proceso.?", "HIS3000", MessageBoxButtons.YesNo) == DialogResult.No))
            {
                return;
            }

            this.Dispose();

        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {

        }
    }
}
