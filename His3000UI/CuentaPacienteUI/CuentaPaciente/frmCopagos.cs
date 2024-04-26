using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades.Clases;

namespace CuentaPaciente
{
    public partial class frmCopagos : Form
    {
        public Int64 ate_codigo1;
        Int64 ate_codigo;
        public Int64 pac_codigo;
        string hc;
        DataTable producto = new DataTable();
        PARAMETROS_DETALLE pd = NegParametros.RecuperaPorCodigo(71);
        public frmCopagos(string ateNumero, string _hc)
        {
            InitializeComponent();

            gridN.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            gridN.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            gridN.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            gridN.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            gridN.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            ate_codigo = Convert.ToInt64(ateNumero);
            hc = _hc;

            try
            {
                cargartabla();
                totales();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void cargartabla()
        {
            //CargarGrid();
            if (NegFactura.RecuperaAtencion(ate_codigo)) //verifica si la atencion esta ya facturada // Mario Valencia // 28-02-2023 
            {
                gridN.DataSource = NegFactura.cargaCuentaPacienteXAtencion(ate_codigo);

                //gridN.Columns[].Frozen = true;
                UltraGridColumn c = gridN.DisplayLayout.Bands[0].Columns["PEDIDO"];
                c.Hidden = false;
                c = gridN.DisplayLayout.Bands[0].Columns["ID"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["RUBRO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["PRO_CODIGO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["DETALLE"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["CANTIDAD"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["VALOR_UNITARIO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["VALOR_IVA"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["VALOR_TOTAL"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["UNITARIO_COPAGO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["IVA_COPAGO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["TOTAL_COPAGO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["UNITARIO"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["IVA"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
                c = gridN.DisplayLayout.Bands[0].Columns["TOTAL"];
                c.CellActivation = Activation.NoEdit;
                c.CellClickAction = CellClickAction.CellSelect;
            }
            else
            {
                MessageBox.Show("No se puede dividir una cuenta ya facturada", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        private void totales()
        {
            gridN.Refresh();
            double xMarcados = 0;
            double xTotal = 0;
            foreach (UltraGridRow fila in gridN.Rows)
            {

                xTotal += Convert.ToDouble(fila.Cells["VALOR_UNITARIO"].Value);

            }
            this.txtTotal.Text = xTotal.ToString();
            this.txtCantidad.Text = Math.Round(Convert.ToDecimal(xMarcados), 2).ToString();
        }

        private void btnDescuento_Click(object sender, EventArgs e)
        {
            if (optInd.Checked)
            {
                bool valida = false;
                foreach (UltraGridRow fila in gridN.Rows)
                {
                    if (fila.Cells["PORCENTAJE_COPAGO"].Value.ToString() != "")
                    {
                        if (Convert.ToDouble(fila.Cells["PORCENTAJE_COPAGO"].Value.ToString()) <= 100)
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

                double v_iva = Convert.ToDouble(pd.PAD_VALOR);
                if ((((maskPor.Text).Replace("%", "").Replace(".", "")).Trim()) != "")
                {
                    double porcentage = Convert.ToDouble(((maskPor.Text).Replace("%", "")).Replace(" ", "").Trim());
                    double porcentrestante = 100 - porcentage;
                    double valcopago = 0;
                    double totcopago = 0;
                    double totunitario = 0;
                    double total = 0;
                    double descuento = 0;
                    double iva = 0;
                    double ivacopago = 0;
                    if (porcentage <= 100)
                    {
                        foreach (UltraGridRow fila in gridN.Rows)
                        {
                            total = Convert.ToDouble(fila.Cells["VALOR_UNITARIO"].Value);
                            descuento = total * (porcentage / 100);

                            fila.Cells["VALOR_COPAGO"].Value = descuento.ToString("#####0.000");
                            fila.Cells["PORCENTAJE_COPAGO"].Value = porcentage.ToString("#####0.000");

                            valcopago = total * (porcentrestante / 100);
                            totcopago = Convert.ToDouble(fila.Cells["CANTIDAD"].Value) * valcopago;
                            totunitario = Convert.ToDouble(fila.Cells["CANTIDAD"].Value) * descuento;
                            producto = NegProducto.recuperaProductoSicXcodpro(fila.Cells["PRO_CODIGO"].Value.ToString());
                            if (producto.Rows[0]["iva"].ToString() != "0")
                            {
                                iva = totcopago * v_iva;
                                ivacopago = totunitario * v_iva;
                            }
                            else
                            {
                                iva = 0;
                                ivacopago = 0;
                            }

                            fila.Cells["UNITARIO"].Value = valcopago.ToString("#####0.000");
                            fila.Cells["IVA"].Value = iva.ToString("#####0.0000");
                            fila.Cells["TOTAL"].Value = totcopago.ToString("#####0.000");


                            fila.Cells["UNITARIO_COPAGO"].Value = descuento.ToString("#####0.000");
                            fila.Cells["IVA_COPAGO"].Value = ivacopago.ToString("#####0.0000");
                            fila.Cells["TOTAL_COPAGO"].Value = totunitario.ToString("#####0.000");
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

                double v_iva = Convert.ToDouble(pd.PAD_VALOR);
                if ((((maskVal.Text).Replace("$", "").Replace(".", "")).Trim()) != "")
                {
                    double valorDesc = (Convert.ToDouble(((maskVal.Text).Replace("$", "")).Replace(" ", "").Trim()));
                    sumar();
                    if (valorDesc <= Convert.ToDouble(txtTotal.Text))
                    {
                        double porcentage = ((valorDesc * 100) / Convert.ToDouble(txtTotal.Text)); //el 2% 
                        double porcentrestante = 100 - porcentage;
                        double total = 0;
                        double descuento = 0;

                        double valcopago = 0;
                        double totcopago = 0;
                        double totunitario = 0;
                        double iva = 0;
                        double ivacopago = 0;

                        foreach (UltraGridRow fila in gridN.Rows)
                        {
                            total = Convert.ToDouble(fila.Cells["VALOR_UNITARIO"].Value);
                            descuento = total * (porcentage / 100);

                            fila.Cells["VALOR_COPAGO"].Value = descuento.ToString("#####0.000");
                            fila.Cells["PORCENTAJE_COPAGO"].Value = porcentage.ToString("#####0.000");

                            valcopago = total * (porcentrestante / 100);
                            totcopago = Convert.ToDouble(fila.Cells["CANTIDAD"].Value) * valcopago;
                            totunitario = Convert.ToDouble(fila.Cells["CANTIDAD"].Value) * descuento;
                            producto = NegProducto.recuperaProductoSicXcodpro(fila.Cells["PRO_CODIGO"].Value.ToString());
                            if (producto.Rows[0]["iva"].ToString() != "0")
                            {
                                iva = totcopago * v_iva;
                                ivacopago = totunitario * v_iva;
                            }
                            else
                            {
                                iva = 0;
                                ivacopago = 0;
                            }

                            fila.Cells["UNITARIO"].Value = valcopago.ToString("#####0.000");
                            fila.Cells["IVA"].Value = iva.ToString("#####0.0000");
                            fila.Cells["TOTAL"].Value = totcopago.ToString("#####0.000");


                            fila.Cells["UNITARIO_COPAGO"].Value = descuento.ToString("#####0.000");
                            fila.Cells["IVA_COPAGO"].Value = ivacopago.ToString("#####0.0000");
                            fila.Cells["TOTAL_COPAGO"].Value = totunitario.ToString("#####0.000");
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
        public void sumar()
        {
            decimal utotal = 0;
            decimal iva = 0;
            decimal total = 0;
            decimal civa = 0;
            decimal ctotal = 0;
            decimal siva = 0;
            decimal scopago = 0;
            try
            {
                foreach (UltraGridRow fila in gridN.Rows)
                {
                    utotal += Convert.ToDecimal(fila.Cells["VALOR_TOTAL"].Value);
                    ctotal += Convert.ToDecimal(fila.Cells["TOTAL_COPAGO"].Value);
                    scopago += Convert.ToDecimal(fila.Cells["TOTAL"].Value);
                    if (Convert.ToDecimal(fila.Cells["VALOR_IVA"].Value) > 0)
                    {
                        civa += Convert.ToDecimal(fila.Cells["IVA_COPAGO"].Value);
                        siva += Convert.ToDecimal(fila.Cells["IVA"].Value);
                        iva += Convert.ToDecimal(fila.Cells["VALOR_IVA"].Value);
                    }
                }
                total = utotal + iva;
                txtTotal.Text = total.ToString("#####0.000"); // total valor
                //txtCantidad.Text = cantidades.ToString("#####0.000"); // cantidades
                this.txtSiva.Text = siva.ToString("#####0.000");
                this.txtStotal.Text = scopago.ToString("#####0.000");
                this.txtCiva.Text = civa.ToString("#####0.000");
                this.txtCtotal.Text = ctotal.ToString("#####0.000");
                //if (sumaValor != 0)
                //    txtCiva.Text = ((suma * 100) / sumaValor).ToString("#####0.000"); // porcentage descueto global
                //else
                //    txtCiva.Text = "0.00";
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private void gridN_CellChange(object sender, CellEventArgs e)
        {
            UltraGridRow row = e.Cell.Row;
            double valorcopago = 0;
            double v_iva = Convert.ToDouble(pd.PAD_VALOR);
            if (double.TryParse(e.Cell.Text, out valorcopago))
            {
                try
                {
                    valorcopago = Convert.ToDouble(e.Cell.Text);
                }
                catch (Exception)
                {
                    row.Cells["VALOR_COPAGO"].Value = 0;
                    row.Cells["PORCENTAJE_COPAGO"].Value = 0;
                }
                double porcentajeDesc = Convert.ToDouble(row.Cells["PORCENTAJE_COPAGO"].Value);
                double valorDescuento = Convert.ToDouble(row.Cells["VALOR_COPAGO"].Value);
                double v_Unitario = Convert.ToDouble(row.Cells["VALOR_UNITARIO"].Value);

                try
                {
                    double tdbValor = 0;
                    double tdbPorDesc = 0;
                    double tdbDescTot = 0;

                    double valcopago = 0;
                    double totcopago = 0;
                    double totunitario = 0;
                    double iva = 0;
                    double ivacopago = 0;
                    double porcentrestante = 0;

                    if (gridN.ActiveCell != null)
                    {
                        string headerText = gridN.ActiveCell.Column.Header.Caption;

                        if (headerText == "VALOR_COPAGO")
                        {
                            if (valorcopago < 0 || valorcopago > v_Unitario)
                            {
                                row.Cells["PORCENTAJE_COPAGO"].Value = 0;
                                row.Cells["VALOR_COPAGO"].Value = 0;
                                return;
                            }
                            tdbDescTot = (100 * valorcopago) / v_Unitario;
                            porcentrestante = 100 - tdbDescTot;
                            row.Cells["PORCENTAJE_COPAGO"].Value = tdbDescTot.ToString("#####0.00");
                            valcopago = v_Unitario * (porcentrestante / 100);

                            producto = NegProducto.recuperaProductoSicXcodpro(row.Cells["PRO_CODIGO"].Value.ToString());
                            if (producto.Rows[0]["iva"].ToString() != "0")
                            {
                                iva = valcopago * v_iva;
                                ivacopago = valorcopago * v_iva;
                            }
                            else
                            {
                                iva = 0;
                                ivacopago = 0;
                            }
                            totcopago = Convert.ToDouble(row.Cells["CANTIDAD"].Value) * valcopago;

                            row.Cells["UNITARIO"].Value = valcopago.ToString("#####0.000");
                            row.Cells["IVA"].Value = iva.ToString("#####0.0000");
                            row.Cells["TOTAL"].Value = totcopago.ToString("#####0.000");

                            totunitario = Convert.ToDouble(row.Cells["CANTIDAD"].Value) * valorcopago;

                            row.Cells["UNITARIO_COPAGO"].Value = valorcopago.ToString("#####0.000");
                            row.Cells["IVA_COPAGO"].Value = ivacopago.ToString("#####0.0000");
                            row.Cells["TOTAL_COPAGO"].Value = totunitario.ToString("#####0.000");

                        }
                        else if (headerText == "PORCENTAJE_COPAGO")
                        {
                            if (valorcopago < 0 || valorcopago > 100)
                            {
                                row.Cells["PORCENTAJE_COPAGO"].Value = 0;
                                row.Cells["VALOR_COPAGO"].Value = 0;
                                return;
                            }
                            tdbValor = v_Unitario;
                            tdbDescTot = (tdbValor * valorcopago) / 100;
                            porcentrestante = 100 - valorcopago;
                            row.Cells["VALOR_COPAGO"].Value = tdbDescTot.ToString("#####0.000");
                            valcopago = v_Unitario * (porcentrestante / 100);

                            producto = NegProducto.recuperaProductoSicXcodpro(row.Cells["PRO_CODIGO"].Value.ToString());
                            if (producto.Rows[0]["iva"].ToString() != "0")
                            {
                                iva = valcopago * v_iva;
                                ivacopago = tdbDescTot * v_iva;
                            }
                            else
                            {
                                iva = 0;
                                ivacopago = 0;
                            }
                            totcopago = Convert.ToDouble(row.Cells["CANTIDAD"].Value) * valcopago;

                            row.Cells["UNITARIO"].Value = valcopago.ToString("#####0.000");
                            row.Cells["IVA"].Value = iva.ToString("#####0.0000");
                            row.Cells["TOTAL"].Value = totcopago.ToString("#####0.000");

                            totunitario = Convert.ToDouble(row.Cells["CANTIDAD"].Value) * tdbDescTot;

                            row.Cells["UNITARIO_COPAGO"].Value = tdbDescTot.ToString("#####0.000");
                            row.Cells["IVA_COPAGO"].Value = ivacopago.ToString("#####0.0000");
                            row.Cells["TOTAL_COPAGO"].Value = totunitario.ToString("#####0.000");


                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ingrese números");
                    row.Cells["VALOR_COPAGO"].Value = 0;
                    row.Cells["PORCENTAJE_COPAGO"].Value = 0;
                    // Puedes agregar lógica adicional aquí según tus necesidades
                }

            }

        }

        private void optPor_CheckedChanged(object sender, EventArgs e)
        {
            UltraGridColumn c;
            c = gridN.DisplayLayout.Bands[0].Columns["VALOR_COPAGO"];
            c.CellActivation = Activation.NoEdit;
            c.CellClickAction = CellClickAction.CellSelect;
            c = gridN.DisplayLayout.Bands[0].Columns["PORCENTAJE_COPAGO"];
            c.CellActivation = Activation.NoEdit;
            c.CellClickAction = CellClickAction.CellSelect;
            //double valor = 0;
            //foreach (UltraGridRow fila in gridN.Rows)
            //{

            //    fila.Cells["VALOR_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["PORCENTAJE_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["UNITARIO_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["IVA_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["TOTAL_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["UNITARIO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["IVA"].Value = valor.ToString("#####0.000");
            //    fila.Cells["TOTAL"].Value = valor.ToString("#####0.000");
            //}
        }

        private void optVal_CheckedChanged(object sender, EventArgs e)
        {
            UltraGridColumn c;
            c = gridN.DisplayLayout.Bands[0].Columns["VALOR_COPAGO"];
            c.CellActivation = Activation.NoEdit;
            c.CellClickAction = CellClickAction.CellSelect;
            c = gridN.DisplayLayout.Bands[0].Columns["PORCENTAJE_COPAGO"];
            c.CellActivation = Activation.NoEdit;
            c.CellClickAction = CellClickAction.CellSelect;
            //double valor = 0;
            //foreach (UltraGridRow fila in gridN.Rows)
            //{

            //    fila.Cells["VALOR_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["PORCENTAJE_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["UNITARIO_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["IVA_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["TOTAL_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["UNITARIO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["IVA"].Value = valor.ToString("#####0.000");
            //    fila.Cells["TOTAL"].Value = valor.ToString("#####0.000");
            //}
        }

        private void optInd_CheckedChanged(object sender, EventArgs e)
        {
            UltraGridColumn c;
            c = gridN.DisplayLayout.Bands[0].Columns["VALOR_COPAGO"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;
            c = gridN.DisplayLayout.Bands[0].Columns["PORCENTAJE_COPAGO"];
            c.CellActivation = Activation.AllowEdit;
            c.CellClickAction = CellClickAction.Edit;
            //double valor = 0;
            //foreach (UltraGridRow fila in gridN.Rows)
            //{

            //    fila.Cells["VALOR_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["PORCENTAJE_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["UNITARIO_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["IVA_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["TOTAL_COPAGO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["UNITARIO"].Value = valor.ToString("#####0.000");
            //    fila.Cells["IVA"].Value = valor.ToString("#####0.000");
            //    fila.Cells["TOTAL"].Value = valor.ToString("#####0.000");
            //}
        }

        private void gridN_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            if (e.Layout.Bands.Count > 0)
            {
                UltraGridBand band = e.Layout.Bands[0];
                UltraGridColumn clm1 = band.Columns["UNITARIO_COPAGO"];
                clm1.CellAppearance.BackColor = ObtenerVerdePastel();
                UltraGridColumn cmlm2 = band.Columns["TOTAL_COPAGO"];
                cmlm2.CellAppearance.BackColor = ObtenerVerdePastel();
                UltraGridColumn cmlm3 = band.Columns["UNITARIO"];
                cmlm3.CellAppearance.BackColor = ObtenerAzulPastel();
                UltraGridColumn cmlm4 = band.Columns["TOTAL"];
                cmlm4.CellAppearance.BackColor = ObtenerAzulPastel();
                UltraGridColumn clm5 = band.Columns["IVA_COPAGO"];
                clm5.CellAppearance.BackColor = ObtenerVerdePastel();
                UltraGridColumn clm6 = band.Columns["IVA"];
                clm6.CellAppearance.BackColor = ObtenerAzulPastel();
            }
        }
        private Color ObtenerVerdePastel()
        {
            // Puedes ajustar estos valores para obtener el tono deseado
            int rojo = 144;
            int verde = 238;
            int azul = 144;

            return Color.FromArgb(rojo, verde, azul);
        }
        private Color ObtenerAzulPastel()
        {
            // Puedes ajustar estos valores para obtener el tono deseado
            int rojo = 173;
            int verde = 216;
            int azul = 230;

            return Color.FromArgb(rojo, verde, azul);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (validaCopago())
            {
                if (MessageBox.Show("Quiere generar la division de cuentas para copago", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    List<DtopCuenta> cuentaOriginal = new List<DtopCuenta>();
                    List<DtoCopago> cuentaCopago = new List<DtoCopago>();
                    PACIENTES paciente = NegPacientes.RecuperarPacienteID(hc);
                    pac_codigo = paciente.PAC_CODIGO;
                    foreach (UltraGridRow fila in gridN.Rows)
                    {
                        DtopCuenta co = new DtopCuenta();
                        DtoCopago cp = new DtoCopago();
                        if (Convert.ToDouble(fila.Cells["UNITARIO_COPAGO"].Value) != 0.00 || Convert.ToDouble(fila.Cells["UNITARIO_COPAGO"].Value) != 0)
                        {
                            cp.id = Convert.ToInt64(fila.Cells["ID"].Value);
                            cp.CPv_unitario = Convert.ToDecimal(fila.Cells["UNITARIO_COPAGO"].Value);
                            cp.CPiva = Convert.ToDecimal(fila.Cells["IVA_COPAGO"].Value);
                            cp.CPtotal = Convert.ToDecimal(fila.Cells["TOTAL_COPAGO"].Value);
                            cp.CPpro_codigo = Convert.ToInt64(fila.Cells["PRO_CODIGO"].Value);
                            cuentaCopago.Add(cp);
                            co.id = Convert.ToInt64(fila.Cells["ID"].Value);
                            co.COv_unitario = Convert.ToDecimal(fila.Cells["UNITARIO"].Value);
                            co.COiva = Convert.ToDecimal(fila.Cells["IVA"].Value);
                            co.COtotal = Convert.ToDecimal(fila.Cells["TOTAL"].Value);
                            co.COpro_codigo = Convert.ToInt64(fila.Cells["PRO_CODIGO"].Value);
                            cuentaOriginal.Add(co);
                        }
                    }
                    ate_codigo1 = NegCuentasPacientes.generaCuentaAuditoria(ate_codigo, cuentaCopago, cuentaOriginal, Sesion.codUsuario);
                    if (ate_codigo1 != 0)
                    {
                        MessageBox.Show("Copago Generado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                        MessageBox.Show("El copago no se genero comuniquese con sistemas", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
                MessageBox.Show("No se puede generar un copago sin valores \r\n ingresse valores para continuar", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (gridN.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(gridN, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }
        public bool validaCopago()
        {
            Int64 contador = 0;
            foreach (UltraGridRow fila in gridN.Rows)
            {
                double valor = Convert.ToDouble(fila.Cells["VALOR_COPAGO"].Value);
                double porcent = Convert.ToDouble(fila.Cells["PORCENTAJE_COPAGO"].Value);
                if (valor != 0 || valor != 0.00)
                {
                    if (porcent != 0 || porcent != 0.00)
                    {
                        contador++;
                    }
                }
            }
            if (contador == 0)
                return false;
            else
                return true;
        }

        private void gridN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UltraGridRow currentRow = gridN.ActiveRow;
                UltraGridCell currentCell = gridN.ActiveCell;

                int rowIndex = currentRow.Index;
                int columnIndex = currentCell.Column.Index;

                int nextRowIndex = rowIndex + 1;

                if (nextRowIndex < gridN.Rows.Count)
                {
                    gridN.Rows[nextRowIndex].Cells[columnIndex].Activate();
                    gridN.PerformAction(UltraGridAction.EnterEditMode);
                    sumar();
                }
            }
        }
    }
}
