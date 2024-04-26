//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using His.Entidades;
//using His.Entidades.Pedidos;
//using His.Negocio;
//using His.Entidades.Clases;
//using Recursos;
//using Infragistics.Win.UltraWinGrid;
//using His.Mantenimiento;
//using His.Parametros;
//using His.DatosReportes;
//using His.Entidades.Reportes;
//using His.Formulario;
//using Core.Utilitarios;
////using GeneralApp;
//using System.Runtime.InteropServices;
//using System.Data.OleDb;
//using frmImpresionPedidos = His.HabitacionesUI.frmImpresionPedidos;


//namespace CuentaPaciente
//{



//    public partial class frmCorreccioDesc : Form
//    {
//        DataTable dtValoresCuentas = new DataTable();
//        DataTable dtValoresCuentasEliminados = new DataTable();
//        List<DataRow> ElementosEliminados = new List<DataRow>();
//        List<DtoItemEliminadoCuentas> ListaItemsEliminados = new List<DtoItemEliminadoCuentas>();

//        Int32 CodAtencion = 0;

//        public double[] descuentos = new double[3];

//        private void frmCorreccioDesc_Load(object sender, EventArgs e)
//        {

//        }

//        public frmCorreccioDesc(Int32 CodigoAtencion, Int32 CodigoRubro)
//        {
//            InitializeComponent();
//            dtValoresCuentas = NegFactura.DatosFacDescuento(CodigoAtencion, CodigoRubro);

//             this.dgvDatosCuenta.DataSource = dtValoresCuentas;
//            CodAtencion = CodigoAtencion;

//            dgvDatosCuenta.Columns["PRO_CODIGO"].ReadOnly = true;
//            dgvDatosCuenta.Columns["Producto"].ReadOnly = true;
//            dgvDatosCuenta.Columns["Cantidad"].ReadOnly = true;
//            dgvDatosCuenta.Columns["Total"].ReadOnly = true;
//            dgvDatosCuenta.Columns["iva"].ReadOnly = true;
//            dgvDatosCuenta.Columns["RUB_CODIGO"].Visible = false;
//            dgvDatosCuenta.Columns["CUE_CODIGO"].Visible = false;

//            sumar();
//        }
//        public frmCorreccioDesc(Int32 CodigoAtencion, Int32 CodigoRubro, Boolean x)
//        {
//            InitializeComponent();
//            dtValoresCuentas = NegFactura.DatosFacDescuento(CodigoAtencion, CodigoRubro);

//            this.dgvDatosCuenta.DataSource = dtValoresCuentas;
//            CodAtencion = CodigoAtencion;

//            dgvDatosCuenta.Columns["PRO_CODIGO"].ReadOnly = true;
//            dgvDatosCuenta.Columns["Producto"].ReadOnly = true;
//            dgvDatosCuenta.Columns["Cantidad"].ReadOnly = true;
//            dgvDatosCuenta.Columns["Porcentage_desc"].ReadOnly = true;
//            dgvDatosCuenta.Columns["Valor_Descuento"].ReadOnly = true;
//            dgvDatosCuenta.Columns["Total"].ReadOnly = true;
//            dgvDatosCuenta.Columns["iva"].ReadOnly = true;
//            dgvDatosCuenta.Columns["RUB_CODIGO"].Visible = false;
//            dgvDatosCuenta.Columns["CUE_CODIGO"].Visible = false;
//            this.menu.Visible = false;
//            this.label2.Visible = false;
//            this.optPor.Visible = false;
//            this.optVal.Visible = false;
//            this.maskPor.Visible = false;
//            this.maskVal.Visible = false;
//            this.button2.Visible = false;
//            this.ultraGroupBox2.Text= "DETALLE DE PRODUCTOS";
//            this.button1.Visible = true;
//            this.button1.Select();

//            sumar();
//        }


//        private void dgvDatosCuenta_CellValueChanged(object sender, DataGridViewCellEventArgs e)
//        {
//        }

//        private void GuardaDatos()
//        {

//        }

//        private void dgvDatosCuenta_KeyDown(object sender, KeyEventArgs e)
//        {

//        }

//        private void dgvDatosCuenta_RowEnter(object sender, DataGridViewCellEventArgs e)
//        {
//            foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
//            {
//                if (Convert.ToInt16(fila.Cells["Porcentage_desc"].Value.ToString()) < 0)
//                {
//                    fila.Cells["Porcentage_desc"].Value = "0";
//                }
//                if (Convert.ToInt16(fila.Cells["Porcentage_desc"].Value.ToString()) > 100)
//                {
//                    fila.Cells["Porcentage_desc"].Value = "100";
//                }
//            }
//            /*
//            if (dgvDatosCuenta.CurrentRow != null)
//            {
//                FilaActual = dgvDatosCuenta.CurrentRow.Index;
//                NumeroPedido = dgvDatosCuenta.CurrentRow.Cells["PEDIDO"].Value.ToString();
//            }*/
//        }



//        public void sumar()
//        {
//            //sumar
//            decimal suma = 0;
//            decimal sumaCIva = 0;
//            decimal sumaSIva = 0;
//            decimal sumaValor = 0;
//            decimal cantidades = 0;
//            foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
//            {
//              suma += Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value);
//              sumaValor += Convert.ToDecimal(fila.Cells["Total"].Value);
//              cantidades += Convert.ToDecimal(fila.Cells["Cantidad"].Value);
//                if (Convert.ToDecimal(fila.Cells["iva"].Value) > 0)
//                {
//                    sumaCIva += Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value);
//                }
//                else
//                {
//                    sumaSIva += Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value);
//                }

//            }
//            this.txtDescConIVA.Text = sumaCIva.ToString("#####0.00");
//            this.txtDescSinIVA.Text = sumaSIva.ToString("#####0.00");

//            txtTTvalor.Text = sumaValor.ToString("#####0.00"); // total valor
//            txtTTcantidad.Text = cantidades.ToString("#####0.00"); // cantidades
//            this.lblTotal.Text = suma.ToString("#####0.00"); //descuento
//            txtTTporcentage.Text = ((suma * 100) / sumaValor).ToString("#####0.00"); // porcentage descueto global

//        }

//        public void GrabaDescuentoProductos()
//        {
//            // datos de descuento
//            for (int i = 0; i < dgvDatosCuenta.Rows.Count; i++)
//            {

//                if (dgvDatosCuenta.Rows[i].Cells["PRO_CODIGO"].Value != null && dgvDatosCuenta.Rows[i].Cells["Porcentage_desc"].Value != null && dgvDatosCuenta.Rows[i].Cells["Porcentage_desc"].Value.ToString() != "")
//                {
//                    //NegFactura.GuardaDescuentoProductos( Convert.ToInt16(dgvDescProd.Rows[i].Cells[5].Value), dgvDescProd.Rows[i].Cells[0].Value.ToString(), Convert.ToDouble(dgvDescProd.Rows[i].Cells[4].Value), Convert.ToDouble(dgvDescProd.Rows[i].Cells[3].Value.ToString()), Convert.ToInt64(dgvDescProd.Rows[i].Cells[6].Value.ToString()), Convert.ToInt16(txt_Atencion.Text) );
//                    NegFactura.GuardaDescuentoProductos(Convert.ToInt16(dgvDatosCuenta.Rows[i].Cells["RUB_CODIGO"].Value), dgvDatosCuenta.Rows[i].Cells["PRO_CODIGO"].Value.ToString(), Convert.ToDouble(dgvDatosCuenta.Rows[i].Cells["Valor_Descuento"].Value), Convert.ToDouble(dgvDatosCuenta.Rows[i].Cells["Porcentage_desc"].Value.ToString()), Convert.ToInt64(dgvDatosCuenta.Rows[i].Cells["CUE_CODIGO"].Value.ToString()), Convert.ToInt32(CodAtencion));
//                    //s(Int32 PcodRubro, string PCodpro, double Pdescuento, double Pporcentaje, Int64 PCueCodigo, Int32 Patencion)

//                }
//            }
//        }

//        private void btnNuevo_Click(object sender, EventArgs e)
//        {
//            descuentos[0] = 1;
//            descuentos[1] = Convert.ToDouble(this.txtDescConIVA.Text);
//            descuentos[2] = Convert.ToDouble(this.txtDescSinIVA.Text);
//            GrabaDescuentoProductos();

//            //  GuardaDatos();
//            //  frmFactura.dgvDescuento.Rows[frmFactura.dgvDescuento.CurrentRow.Index].Cells["RUBRO"].Value)= lblTotal.Text;
//            //frmFactura.textBox1.Text = "100";
//            //xdescuento = 'asdsad';

//            //  valorDescuento = Convert.ToDecimal(this.lblTotal.Text);
//            this.Dispose();
//        }

//        private void toolStripButton2_Click(object sender, EventArgs e)
//        {
//            descuentos[0] = 0;
//            this.Dispose();
//        }

//        private void dgvDatosCuenta_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
//        {

//        }

//        private void dgvDatosCuenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {

//        }

//        private void dgvDatosCuenta_CellLeave(object sender, DataGridViewCellEventArgs e)
//        {

//        }

//        private void dgvDatosCuenta_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
//        {
//          //  DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;

//         //   dText.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
//         //   dText.KeyPress += new KeyPressEventHandler(dText_KeyPress);
//        }

//        void dText_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (Char.IsLetter(e.KeyChar))
//            {
//                e.Handled = false;
//            }
//            else
//            {
//                e.Handled = true;
//            }


//        }

//        private void button2_Click(object sender, EventArgs e)
//        {
//            if (optPor.Checked) // descuento global por porcentage
//            {
//                if ((((maskPor.Text).Replace("%", "").Replace(".", "")).Trim()) != "")
//                {
//                    double porcentage = Convert.ToDouble(((maskPor.Text).Replace("%", "")).Replace(" ","").Trim());
//                    double total = 0;
//                    double descuento = 0;

//                    foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
//                    {
//                        total = Convert.ToDouble(fila.Cells["Total"].Value);
//                        descuento = total * (porcentage / 100);

//                        fila.Cells["Valor_Descuento"].Value = descuento.ToString("#####0.00");
//                        fila.Cells["Porcentage_desc"].Value = porcentage.ToString("#####0.00");
//                    }

//                    sumar();
//                }
//            } 

//            if(optVal.Checked) // descuento global por valor
//            {
//                if ((((maskVal.Text).Replace("$", "").Replace(".","")).Trim()) != "")
//                {
//                    double valorDesc = (Convert.ToDouble(((maskVal.Text).Replace("$", "")).Replace(" ", "").Trim()));

//                    if (valorDesc< Convert.ToDouble(txtTTvalor.Text))
//                    {
//                        double porcentage = ((valorDesc * 100) / Convert.ToDouble(txtTTvalor.Text)); //el 2% 

//                        double total = 0;
//                        double descuento = 0;

//                        foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
//                        {
//                            total = Convert.ToDouble(fila.Cells["Total"].Value);
//                            descuento = total * (porcentage / 100);

//                            fila.Cells["Valor_Descuento"].Value = descuento.ToString("#####0.00");
//                            fila.Cells["Porcentage_desc"].Value = porcentage.ToString("#####0.00");
//                        }
//                        sumar();
//                    }
//                    else
//                    {
//                        MessageBox.Show("No puede dar un descuento mayor o igual al valor total de la cuenta.");
//                    }
//                }
//            }


//            // MessageBox.Show((mask1.Text).Replace("%", ""));

//        }

//        private void ultraGroupBox2_Click(object sender, EventArgs e)
//        {

//        }

//        private void label2_Click(object sender, EventArgs e)
//        {

//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            this.Dispose();
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using Infragistics.Win.UltraWinGrid;
using His.Maintenance;
using His.Parametros;
using His.DatosReportes;
using His.Entidades.Reportes;
using His.Formulario;
using Core.Utilitarios;
//using GeneralApp;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using frmImpresionPedidos = His.HabitacionesUI.frmImpresionPedidos;


namespace CuentaPaciente
{



    public partial class frmCorreccioDesc : Form
    {
        DataTable dtValoresCuentas = new DataTable();
        DataTable dtValoresCuentasEliminados = new DataTable();
        List<DataRow> ElementosEliminados = new List<DataRow>();
        List<DtoItemEliminadoCuentas> ListaItemsEliminados = new List<DtoItemEliminadoCuentas>();
        internal static string rubro = "";
        Int32 CodAtencion = 0;

        public double[] descuentos = new double[3];

        private void frmCorreccioDesc_Load(object sender, EventArgs e)
        {

        }

        public frmCorreccioDesc(Int32 CodigoAtencion, Int32 CodigoRubro)
        {
            InitializeComponent();
            dtValoresCuentas = NegFactura.DatosFacDescuento(CodigoAtencion, CodigoRubro);

            this.dgvDatosCuenta.DataSource = dtValoresCuentas;
            CodAtencion = CodigoAtencion;

            dgvDatosCuenta.Columns["PRO_CODIGO"].ReadOnly = true;
            dgvDatosCuenta.Columns["Producto"].ReadOnly = true;
            dgvDatosCuenta.Columns["Cantidad"].ReadOnly = true;
            dgvDatosCuenta.Columns["Valor Unitario"].ReadOnly = true;
            dgvDatosCuenta.Columns["Total"].ReadOnly = true;
            dgvDatosCuenta.Columns["iva"].ReadOnly = true;
            dgvDatosCuenta.Columns["RUB_CODIGO"].Visible = false;
            dgvDatosCuenta.Columns["CUE_CODIGO"].Visible = false;

            //sumar();
        }
        public frmCorreccioDesc(Int32 CodigoAtencion, Int32 CodigoRubro, Boolean x)
        {
            InitializeComponent();
            if (rubro == "HONORARIOS MEDICOS")
            {
                dtValoresCuentas = NegFactura.DatosHonorarios(CodigoAtencion, CodigoRubro);

                dgvDatosCuenta.DataSource = dtValoresCuentas;
                CodAtencion = CodigoAtencion;

                dgvDatosCuenta.Columns["PRO_CODIGO"].ReadOnly = true;
                dgvDatosCuenta.Columns["Medico"].ReadOnly = true;
                dgvDatosCuenta.Columns["Producto"].ReadOnly = true;
                dgvDatosCuenta.Columns["Cantidad"].ReadOnly = true;
                dgvDatosCuenta.Columns["Porcentage_desc"].ReadOnly = true;
                dgvDatosCuenta.Columns["Valor_Descuento"].ReadOnly = true;
                dgvDatosCuenta.Columns["Valor Unitario"].ReadOnly = true;
                dgvDatosCuenta.Columns["Total"].ReadOnly = true;
                dgvDatosCuenta.Columns["iva"].ReadOnly = true;
                dgvDatosCuenta.Columns["RUB_CODIGO"].Visible = false;
                dgvDatosCuenta.Columns["CUE_CODIGO"].Visible = false;
                this.menu.Visible = false;
                this.label2.Visible = false;
                this.optPor.Visible = false;
                this.optVal.Visible = false;
                this.maskPor.Visible = false;
                this.maskVal.Visible = false;
                this.button2.Visible = false;
                this.ultraGroupBox2.Text = "DETALLE DE PRODUCTOS";
                this.button1.Visible = true;
                this.button1.Select();
            }
            else
            {
                dtValoresCuentas = NegFactura.DatosFacDescuento(CodigoAtencion, CodigoRubro);

                this.dgvDatosCuenta.DataSource = dtValoresCuentas;
                CodAtencion = CodigoAtencion;

                dgvDatosCuenta.Columns["PRO_CODIGO"].ReadOnly = true;
                dgvDatosCuenta.Columns["Producto"].ReadOnly = true;
                dgvDatosCuenta.Columns["Cantidad"].ReadOnly = true;
                dgvDatosCuenta.Columns["Porcentage_desc"].ReadOnly = true;
                dgvDatosCuenta.Columns["Valor_Descuento"].ReadOnly = true;
                dgvDatosCuenta.Columns["Valor Unitario"].ReadOnly = true;
                dgvDatosCuenta.Columns["Total"].ReadOnly = true;
                dgvDatosCuenta.Columns["iva"].ReadOnly = true;
                dgvDatosCuenta.Columns["RUB_CODIGO"].Visible = false;
                dgvDatosCuenta.Columns["CUE_CODIGO"].Visible = false;
                this.menu.Visible = false;
                this.label2.Visible = false;
                this.optPor.Visible = false;
                this.optVal.Visible = false;
                this.maskPor.Visible = false;
                this.maskVal.Visible = false;
                this.button2.Visible = false;
                this.label6.Visible = false;
                this.optInd.Visible = false;
                this.ultraGroupBox2.Text = "DETALLE DE PRODUCTOS";
                this.button1.Visible = true;
                this.button1.Select();

            }
            sumar();
        }


        private void dgvDatosCuenta_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void GuardaDatos()
        {

        }

        private void dgvDatosCuenta_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dgvDatosCuenta_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
            {
                if (Convert.ToDecimal(fila.Cells["Porcentage_desc"].Value.ToString()) < 0)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Porcentage_desc"].Value.ToString()) > 100)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value.ToString()) < 0)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                //else if (Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value.ToString()) > Convert.ToDecimal(fila.Cells["Total"].Value.ToString()))
                //{
                //    fila.Cells["Porcentage_desc"].Value = "100";
                //    fila.Cells["Valor_Descuento"].Value = "0";
                //}
                else
                {
                    //NumeroPedido = dgvDatosCuenta.CurrentRow.Cells["PEDIDO"].Value.ToString();
                    Decimal tdbValor = 0;
                    Decimal tdbPorDesc = 0;
                    Decimal tdbDescTot = 0;
                    if (dgvDatosCuenta.Columns[e.ColumnIndex].HeaderText == "Porcentage_desc")
                    //if (dgvDatosCuenta.Columns[e.ColumnIndex].Name == "Descuento")
                    {

                        try
                        {
                            tdbValor = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Total"].Value.ToString());
                            tdbPorDesc = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value.ToString());

                            tdbDescTot = (100 * tdbPorDesc) / tdbValor;
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value = tdbDescTot.ToString("#####0.0000");


                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ingrese numeros");
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value = 0;
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value = 0;
                            //dgvDescProd.Focus();
                        }
                    }
                    if (dgvDatosCuenta.Columns[e.ColumnIndex].HeaderText == "Valor_Descuento")
                    //if (dgvDatosCuenta.Columns[e.ColumnIndex].Name == "Descuento")
                    {

                        try
                        {
                            tdbValor = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Total"].Value.ToString());
                            tdbPorDesc = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value.ToString());


                            tdbDescTot = (tdbValor * tdbPorDesc) / 100;
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value = tdbDescTot.ToString("#####0.00");


                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ingrese numeros");
                            dgvDatosCuenta.Rows[e.RowIndex].Cells[3].Value = "";
                            dgvDatosCuenta.Rows[e.RowIndex].Cells[4].Value = "";
                            //dgvDescProd.Focus();
                        }
                    }

                }
            }
            //sumar();
            /*
            if (dgvDatosCuenta.CurrentRow != null)
            {
                FilaActual = dgvDatosCuenta.CurrentRow.Index;
                NumeroPedido = dgvDatosCuenta.CurrentRow.Cells["PEDIDO"].Value.ToString();
            }*/
        }


        private void dgvDatosCuenta_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
            {
                if (Convert.ToDecimal(fila.Cells["Porcentage_desc"].Value.ToString()) < 0)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Porcentage_desc"].Value.ToString()) > 100)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value.ToString()) < 0)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value.ToString()) > Convert.ToDecimal(fila.Cells["Total"].Value.ToString()))
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else
                {
                    // NumeroPedido = dgvDatosCuenta.CurrentRow.Cells["PEDIDO"].Value.ToString();
                    Decimal tdbValor = 0;
                    Decimal tdbPorDesc = 0;
                    Decimal tdbDescTot = 0;
                    if (dgvDatosCuenta.Columns[e.ColumnIndex].HeaderText == "Valor_Descuento")
                    //if (dgvDatosCuenta.Columns[e.ColumnIndex].Name == "Descuento")
                    {

                        try
                        {
                            tdbValor = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Total"].Value.ToString());
                            tdbPorDesc = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value.ToString());

                            tdbDescTot = (100 * tdbPorDesc) / tdbValor;
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value = tdbDescTot.ToString("#####0.0000");


                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ingrese numeros");
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value = "";
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value = "";
                            //dgvDescProd.Focus();
                        }
                    }
                    if (dgvDatosCuenta.Columns[e.ColumnIndex].HeaderText == "Porcentage_desc")
                    //if (dgvDatosCuenta.Columns[e.ColumnIndex].Name == "Descuento")
                    {

                        try
                        {
                            tdbValor = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Total"].Value.ToString());
                            tdbPorDesc = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value.ToString());


                            tdbDescTot = (tdbValor * tdbPorDesc) / 100;
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value = tdbDescTot.ToString("#####0.00");


                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ingrese numeros");
                            dgvDatosCuenta.Rows[e.RowIndex].Cells[3].Value = "";
                            dgvDatosCuenta.Rows[e.RowIndex].Cells[4].Value = "";
                            //dgvDescProd.Focus();
                        }
                    }

                }
            }
            //sumar();
        }


        public void sumar()
        {
            //sumar
            decimal suma = 0;
            decimal sumaCIva = 0;
            decimal sumaSIva = 0;
            decimal sumaValor = 0;
            decimal cantidades = 0;
            try
            {
                foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
                {
                    suma += Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value);
                    sumaValor += Convert.ToDecimal(fila.Cells["Total"].Value);
                    cantidades += Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                    if (Convert.ToDecimal(fila.Cells["iva"].Value) > 0)
                    {
                        sumaCIva += Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value);
                    }
                    else
                    {
                        sumaSIva += Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value);
                    }

                }
                this.txtDescConIVA.Text = sumaCIva.ToString("#####0.0000");
                this.txtDescSinIVA.Text = sumaSIva.ToString("#####0.0000");

                txtTTvalor.Text = sumaValor.ToString("#####0.0000"); // total valor
                txtTTcantidad.Text = cantidades.ToString("#####0.0000"); // cantidades
                this.lblTotal.Text = suma.ToString("#####0.0000"); //descuento
                if (sumaValor != 0)
                    txtTTporcentage.Text = ((suma * 100) / sumaValor).ToString("#####0.0000"); // porcentage descueto global
                else
                    txtTTporcentage.Text = "0.0000";
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void GrabaDescuentoProductos()
        {
            // datos de descuento
            for (int i = 0; i < dgvDatosCuenta.Rows.Count; i++)
            {

                if (dgvDatosCuenta.Rows[i].Cells["PRO_CODIGO"].Value != null && dgvDatosCuenta.Rows[i].Cells["Porcentage_desc"].Value != null && dgvDatosCuenta.Rows[i].Cells["Porcentage_desc"].Value.ToString() != "")
                {
                    //NegFactura.GuardaDescuentoProductos( Convert.ToInt16(dgvDescProd.Rows[i].Cells[5].Value), dgvDescProd.Rows[i].Cells[0].Value.ToString(), Convert.ToDouble(dgvDescProd.Rows[i].Cells[4].Value), Convert.ToDouble(dgvDescProd.Rows[i].Cells[3].Value.ToString()), Convert.ToInt64(dgvDescProd.Rows[i].Cells[6].Value.ToString()), Convert.ToInt16(txt_Atencion.Text) );
                    NegFactura.GuardaDescuentoProductos(Convert.ToInt16(dgvDatosCuenta.Rows[i].Cells["RUB_CODIGO"].Value), dgvDatosCuenta.Rows[i].Cells["PRO_CODIGO"].Value.ToString(), Convert.ToDouble(dgvDatosCuenta.Rows[i].Cells["Valor_Descuento"].Value), Convert.ToDouble(dgvDatosCuenta.Rows[i].Cells["Porcentage_desc"].Value.ToString()), Convert.ToInt64(dgvDatosCuenta.Rows[i].Cells["CUE_CODIGO"].Value.ToString()), Convert.ToInt32(CodAtencion));
                    //s(Int32 PcodRubro, string PCodpro, double Pdescuento, double Pporcentaje, Int64 PCueCodigo, Int32 Patencion)

                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //sumar();
            if (lblTotal.Text != "")
            {
                descuentos[0] = 1;
                descuentos[1] = Convert.ToDouble(this.txtDescConIVA.Text);
                descuentos[2] = Convert.ToDouble(this.txtDescSinIVA.Text);
                GrabaDescuentoProductos();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Primero aplique descuento para luego guardar el mismo","HIS3000",MessageBoxButtons.OK,MessageBoxIcon.Information);
                button2.Focus();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            descuentos[0] = 0;
            this.Dispose();
        }

        private void dgvDatosCuenta_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dgvDatosCuenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
            {
                if (Convert.ToDecimal(fila.Cells["Porcentage_desc"].Value.ToString()) < 0)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Porcentage_desc"].Value.ToString()) > 100)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value.ToString()) < 0)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value.ToString()) > Convert.ToDecimal(fila.Cells["Total"].Value.ToString()))
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else
                {
                    // NumeroPedido = dgvDatosCuenta.CurrentRow.Cells["PEDIDO"].Value.ToString();
                    Decimal tdbValor = 0;
                    Decimal tdbPorDesc = 0;
                    Decimal tdbDescTot = 0;
                    if (dgvDatosCuenta.Columns[e.ColumnIndex].HeaderText == "Valor_Descuento")
                    //if (dgvDatosCuenta.Columns[e.ColumnIndex].Name == "Descuento")
                    {

                        try
                        {
                            tdbValor = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Total"].Value.ToString());
                            tdbPorDesc = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value.ToString());

                            tdbDescTot = (100 * tdbPorDesc) / tdbValor;
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value = tdbDescTot.ToString("#####0.00");


                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ingrese numeros");
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value = "";
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value = "";
                            //dgvDescProd.Focus();
                        }
                    }
                    if (dgvDatosCuenta.Columns[e.ColumnIndex].HeaderText == "Porcentage_desc")
                    //if (dgvDatosCuenta.Columns[e.ColumnIndex].Name == "Descuento")
                    {

                        try
                        {
                            tdbValor = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Total"].Value.ToString());
                            tdbPorDesc = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value.ToString());


                            tdbDescTot = (tdbValor * tdbPorDesc) / 100;
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value = tdbDescTot.ToString("#####0.00");


                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ingrese numeros");
                            dgvDatosCuenta.Rows[e.RowIndex].Cells[3].Value = "";
                            dgvDatosCuenta.Rows[e.RowIndex].Cells[4].Value = "";
                            //dgvDescProd.Focus();
                        }
                    }

                }
            }
            //sumar();
        }

        private void dgvDatosCuenta_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
            {
                if (Convert.ToDecimal(fila.Cells["Porcentage_desc"].Value.ToString()) < 0)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Porcentage_desc"].Value.ToString()) > 100)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value.ToString()) < 0)
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else if (Convert.ToDecimal(fila.Cells["Valor_Descuento"].Value.ToString()) > Convert.ToDecimal(fila.Cells["Total"].Value.ToString()))
                {
                    fila.Cells["Porcentage_desc"].Value = "0";
                    fila.Cells["Valor_Descuento"].Value = "0";
                }
                else
                {
                    // NumeroPedido = dgvDatosCuenta.CurrentRow.Cells["PEDIDO"].Value.ToString();
                    Decimal tdbValor = 0;
                    Decimal tdbPorDesc = 0;
                    Decimal tdbDescTot = 0;
                    if (dgvDatosCuenta.Columns[e.ColumnIndex].HeaderText == "Valor_Descuento")
                    //if (dgvDatosCuenta.Columns[e.ColumnIndex].Name == "Descuento")
                    {

                        try
                        {
                            tdbValor = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Total"].Value.ToString());
                            tdbPorDesc = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value.ToString());

                            tdbDescTot = (100 * tdbPorDesc) / tdbValor;
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value = tdbDescTot.ToString("#####0.00");


                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ingrese numeros");
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value = "";
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value = "";
                            //dgvDescProd.Focus();
                        }
                    }
                    if (dgvDatosCuenta.Columns[e.ColumnIndex].HeaderText == "Porcentage_desc")
                    //if (dgvDatosCuenta.Columns[e.ColumnIndex].Name == "Descuento")
                    {

                        try
                        {
                            tdbValor = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Total"].Value.ToString());
                            tdbPorDesc = Decimal.Parse(dgvDatosCuenta.Rows[e.RowIndex].Cells["Porcentage_desc"].Value.ToString());


                            tdbDescTot = (tdbValor * tdbPorDesc) / 100;
                            dgvDatosCuenta.Rows[e.RowIndex].Cells["Valor_Descuento"].Value = tdbDescTot.ToString("#####0.00");


                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ingrese numeros");
                            dgvDatosCuenta.Rows[e.RowIndex].Cells[3].Value = "";
                            dgvDatosCuenta.Rows[e.RowIndex].Cells[4].Value = "";
                            //dgvDescProd.Focus();
                        }
                    }


                }
            }
            //sumar();
        }

        private void dgvDatosCuenta_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //  DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;

            //   dText.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
            //   dText.KeyPress += new KeyPressEventHandler(dText_KeyPress);
        }

        void dText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {           
            if (optInd.Checked)
            {
                bool valida = false;
                foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
                {
                    if (fila.Cells["Porcentage_desc"].Value.ToString() != "")
                    {
                        if(Convert.ToDouble(fila.Cells["Porcentage_desc"].Value.ToString()) <= 100)
                        valida = true;
                        else
                        {
                            MessageBox.Show("No se puede dar un descuento mayor al 100% corriga para que pueda generar el descuento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                if (valida)
                {
                    sumar();
                    btnNuevo.Enabled = true;
                    return;
                }
                else
                {
                    MessageBox.Show("Debe incluir un valor para poder aplicar un descuento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (optPor.Checked) // descuento global por porcentage
            {
                if ((((maskPor.Text).Replace("%", "").Replace(".", "")).Trim()) != "")
                {
                    double porcentage = Convert.ToDouble(((maskPor.Text).Replace("%", "")).Replace(" ", "").Trim());
                    double total = 0;
                    double descuento = 0;
                    if (porcentage <= 100)
                    {
                        foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
                        {
                            total = Convert.ToDouble(fila.Cells["Total"].Value);
                            descuento = total * (porcentage / 100);

                            fila.Cells["Valor_Descuento"].Value = descuento.ToString("#####0.0000");
                            fila.Cells["Porcentage_desc"].Value = porcentage.ToString("#####0.0000");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El descuento no puede ser mayor al 100%", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        maskPor.Text = "100%";
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese Una cantidad para descuento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (optVal.Checked) // descuento global por valor
            {
                if ((((maskVal.Text).Replace("$", "").Replace(".", "")).Trim()) != "")
                {
                    double valorDesc = (Convert.ToDouble(((maskVal.Text).Replace("$", "")).Replace(" ", "").Trim()));
                    sumar();
                    if (valorDesc <= Convert.ToDouble(txtTTvalor.Text))
                    {
                        double porcentage = ((valorDesc * 100) / Convert.ToDouble(txtTTvalor.Text)); //el 2% 

                        double total = 0;
                        double descuento = 0;

                        foreach (DataGridViewRow fila in dgvDatosCuenta.Rows)
                        {
                            total = Convert.ToDouble(fila.Cells["Total"].Value);
                            descuento = total * (porcentage / 100);

                            fila.Cells["Valor_Descuento"].Value = descuento.ToString("#####0.00");
                            fila.Cells["Porcentage_desc"].Value = porcentage.ToString("#####0.00");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No puede dar un descuento mayor al valor total de la cuenta.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese Una cantidad para descuento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }


            sumar();

            btnNuevo.Enabled = true;

        }

        private void ultraGroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
