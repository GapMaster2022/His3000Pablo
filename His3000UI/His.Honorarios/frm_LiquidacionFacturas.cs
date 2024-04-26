using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinMaskedEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using System.IO;
using System.Xml;

namespace His.Honorarios
{
    public partial class frm_LiquidacionFacturas : Form
    {
        Datos.Atencion obj_atencion = new His.Honorarios.Datos.Atencion();
        public frm_LiquidacionFacturas()
        {
            InitializeComponent();
        }
        public void CargarLiquidaciones()
        {
            dsLiquidacion1 = NegLiquidacion.liquidar(dtpDesde.Value, dtpHasta.Value);

            ultraGridLiquidacion.DataSource = dsLiquidacion1;

            foreach (var item in ultraGridLiquidacion.Rows)
            {
                foreach (UltraGridChildBand x in item.ChildBands)
                {
                    RowsCollection bandchild = x.Rows;
                    foreach (var y in bandchild)
                    {
                        y.Cells["Paciente"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Valor"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Comision"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Aporte"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Retencion"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Total"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Fecha"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Codigo"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Honorario"].Column.CellActivation = Activation.NoEdit;
                    }
                }
            }
        }
        private void frm_LiquidacionFacturas_Load(object sender, EventArgs e)
        {
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            dtpDesde.Value = oPrimerDiaDelMes;
            dtpHasta.Value = oUltimoDiaDelMes;
            CargarLiquidaciones();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarLiquidaciones();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarLiquidaciones();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ultraGridLiquidacion_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Liquidacion"].Hidden = true;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Medico"].Width = 400;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Factura"].Width = 200;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Factura"].MaxLength = 15;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Factura"].MaskInput = "###-###-#########";
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Factura"].MaskDataMode = MaskMode.Raw;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Factura"].MaskClipMode = MaskMode.IncludeBoth;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Factura"].MaskDisplayMode = MaskMode.IncludeBoth;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Autorizacion"].Width = 450;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Autorizacion"].MaxLength = 49;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Autorizacion"].MaskInput = "#################################################";
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Autorizacion"].MaskDataMode = MaskMode.Raw;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Autorizacion"].MaskClipMode = MaskMode.IncludeBoth;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Autorizacion"].MaskDisplayMode = MaskMode.IncludeBoth;

            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Medico"].CellActivation = Activation.NoEdit;
            ultraGridLiquidacion.DisplayLayout.Bands[0].Columns["Codigo"].CellActivation = Activation.NoEdit;
        }

        private void ultraGridLiquidacion_AfterCellUpdate(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Seleccionar") //valido cuando el true se elijan todos los childrows
            {
                if (e.Cell.Value.ToString() == "True")
                {
                    UltraGridRow fila = ultraGridLiquidacion.ActiveRow;
                    foreach (var item in ultraGridLiquidacion.Rows)
                    {
                        foreach (UltraGridChildBand x in item.ChildBands)
                        {
                            RowsCollection bandchild = x.Rows;
                            foreach (var y in bandchild)
                            {
                                y.Cells["Seleccion"].Value = true;
                            }
                        }
                    }
                }
                else
                {
                    UltraGridRow fila = ultraGridLiquidacion.ActiveRow;
                    foreach (var item in ultraGridLiquidacion.Rows)
                    {
                        foreach (UltraGridChildBand x in item.ChildBands)
                        {
                            RowsCollection bandchild = x.Rows;
                            foreach (var y in bandchild)
                            {
                                y.Cells["Seleccion"].Value = false;
                            }
                        }
                    }
                }
            }
            if (e.Cell.Column.Key == "Factura") //valido el numero de factura contra el medico
            {
                if (e.Cell.Value.ToString() != "")
                {
                    if (!libretin)
                    {
                        libretin = true;
                        return;
                    }
                    UltraGridRow fila = ultraGridLiquidacion.ActiveRow;
                    MEDICOS med = NegMedicos.recuperarMedico(Convert.ToInt32(fila.Cells["Codigo"].Value.ToString()));
                    string numfac = e.Cell.Value.ToString();
                    numfac = numfac.Substring(6, numfac.Length - 6);
                    DataTable SRI = NegLiquidacion.DatosSRI(Convert.ToInt64(med.MED_CODIGO_MEDICO.Trim()), Convert.ToInt64(numfac));
                    if (!validarNumeroFacturaMedico(e.Cell.Value.ToString(), fila.Cells["Codigo"].Value.ToString())) //valido el facturero del medico
                    {
                        fila.Cells["Factura"].Value = "";
                        return;
                    }
                    if (NegLiquidacion.validaFactura(e.Cell.Value.ToString(), Convert.ToDouble(med.MED_CODIGO_MEDICO.Trim())))//valido que la factura no este en la liquidacion y que no sea una anulada
                    {
                        MessageBox.Show("La factura ya ha sido utilizada.\r\nConsulte en el explorador de liquidaciones.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        fila.Cells["Factura"].Value = "";
                        return;
                    }
                    if (NegLiquidacion.validaFacturaCG(e.Cell.Value.ToString(), Convert.ToDouble(med.MED_CODIGO_MEDICO.Trim())))//valido que la factura no este en la liquidacion y que no sea una anulada
                    {
                        MessageBox.Show("La factura ya ha sido utilizada.\r\nConsulte en la contabilidad.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        fila.Cells["Factura"].Value = "";
                        return;
                    }
                    if (SRI.Rows.Count > 0)
                    {
                        fila.Cells["Autorizacion"].Value = SRI.Rows[0]["autorizacion"].ToString();
                        fila.Cells["FechaCaduca"].Value = SRI.Rows[0]["fechacaduc"].ToString();
                    }
                }
            }
            if (e.Cell.Column.Key == "FechaFactura")
            {
                UltraGridRow fila = ultraGridLiquidacion.ActiveRow;
                if (e.Cell.Value.ToString() != "")
                {
                    if (Convert.ToDateTime(e.Cell.Value.ToString()) > DateTime.Now)
                    {
                        MessageBox.Show("La fecha de la factura del medico no debe ser mayor a la fecha actual", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        fila.Cells["FechaFactura"].Value = "";
                    }
                }
            }
            if (e.Cell.Column.Key == "FacturaElectronica")
            {
                if (e.Cell.Value.ToString() == "True")
                {
                    UltraGridRow fila = ultraGridLiquidacion.ActiveRow;
                    if (Explorador(Convert.ToInt32(fila.Cells["Codigo"].Value.ToString())))
                    {
                        string _fecha = fecha.Substring(0, 2) + "/" + fecha.Substring(2, 2) + "/" + fecha.Substring(4, fecha.Length - 4);
                        fila.Cells["Autorizacion"].Value = autorizacion;
                        fila.Cells["FechaCaduca"].Value = Convert.ToDateTime(_fecha).Date;
                        fila.Cells["FechaFactura"].Value = Convert.ToDateTime(_fecha).Date;
                        libretin = false;
                        fila.Cells["Factura"].Value = factura;
                    }
                    else
                    {
                        MessageBox.Show("Factura electronica no corresponde al medico.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fila.Cells["Autorizacion"].Value = "";
                        fila.Cells["FechaCaduca"].Value = "";
                        fila.Cells["FechaFactura"].Value = "";
                        fila.Cells["Factura"].Value = "";
                        fila.Cells["FacturaElectronica"].Value = false;
                    }
                }
                else
                {
                    UltraGridRow fila = ultraGridLiquidacion.ActiveRow;
                    fila.Cells["Autorizacion"].Value = "";
                    fila.Cells["FechaCaduca"].Value = "";
                    fila.Cells["FechaFactura"].Value = "";
                    fila.Cells["Factura"].Value = "";
                }
            }
        }
        public bool validarNumeroFacturaMedico(string factura, string codmedico)
        {
            int codMedico = Convert.ToInt16(codmedico);
            bool facturaRepetida = false;
            facturaRepetida = NegHonorariosMedicos.DatosRecuperaFacturasMedicos(codMedico, factura);
            if (!facturaRepetida)
            {
                string serie = factura.Substring(0, 6);
                Int64 numFact = Convert.ToInt64(factura.Substring(6, 9));
                DataTable validaFactura = new DataTable();
                validaFactura = obj_atencion.ValidaFactura(Convert.ToInt64(codmedico));
                if (validaFactura.Rows.Count > 0)
                {
                    bool valido = false;
                    foreach (DataRow item in validaFactura.Rows)
                    {
                        if (DateTime.Now.Date <= Convert.ToDateTime(item[5].ToString()).Date)
                        {
                            if (serie == item[6].ToString())
                            {
                                if (numFact >= Convert.ToInt64(item[7].ToString()) && numFact <= Convert.ToInt64(item[8].ToString()))
                                {
                                    valido = true;
                                }
                            }
                        }
                    }
                    if (!valido)
                    {
                        MessageBox.Show("El numero de factura no se encontro dentro del libretin.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return valido;
                }
                else
                {
                    MessageBox.Show("Médico no cuenta con facturas registradas ó Libretin de facturas registradas se encuentra caducado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Factura del médico ya fue utilizada para liquidar otro Honorario", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void btnLiquidar_Click(object sender, EventArgs e)
        {
            GenerarAsiento();
        }
        public void GenerarAsiento()
        {
            List<Cgcabmae> lcab = new List<Cgcabmae>();
            List<Cgdetmae> ldet;
            List<LIQUIDACION> l;
            Cgcabmae cgcabmae = new Cgcabmae();
            Cgdetmae cgdetmae = new Cgdetmae();
            DateTime fechaIngreso = DateTime.Now;
            if (ultraGridLiquidacion.Rows.Count > 0)
            {
                int i = 0;
                foreach (var item in ultraGridLiquidacion.Rows)
                {
                    if (item.Cells["Seleccionar"].Value.ToString() == "True")
                    {
                        lcab = new List<Cgcabmae>();
                        ldet = new List<Cgdetmae>();
                        l = new List<LIQUIDACION>();
                        if (item.Cells["Factura"].Text != "___-___-_________")
                        {
                            if (item.Cells["FechaFactura"].Text != "")
                            {
                                if (Convert.ToDateTime(item.Cells["FechaFactura"].Text).Date > DateTime.Now.Date)
                                {
                                    MessageBox.Show("La fecha de la factura del medico no debe ser mayor a la fecha actual", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    item.Cells["FechaFactura"].Value = "";
                                    return;
                                }
                                if (item.Cells["FechaFactura"].Text != "01/01/0001")
                                {
                                    i++;
                                    MEDICOS medico = NegMedicos.recuperarMedico(Convert.ToInt32(item.Cells["Codigo"].Value.ToString()));
                                    string numfac = item.Cells["Factura"].Value.ToString();
                                    numfac = numfac.Substring(6, numfac.Length - 6);
                                    DataTable SRI = NegLiquidacion.DatosSRI(Convert.ToInt64(medico.MED_CODIGO_MEDICO.Trim()), Convert.ToInt64(numfac));

                                    RETENCIONES_FUENTE retencionFuente = NegRetencionesFuente.recuperarPorId(Convert.ToInt32(medico.RETENCIONES_FUENTEReference.EntityKey.EntityKeyValues[0].Value));
                                    short linea = 0;
                                    double valor = 0;
                                    double aporte = 0;
                                    double comision = 0;
                                    double retencion = 0;
                                    double total = 0;
                                    PARAMETROS_DETALLE parametros = new PARAMETROS_DETALLE();
                                    string[] parValor;
                                    ultraGridLiquidacion.Refresh();
                                    double numcontrol = NegHonorariosMedicos.OcupoNumeroControl(Convert.ToDateTime(item.Cells["FechaFactura"].Text));
                                    if (numcontrol > 0)
                                    {
                                        foreach (UltraGridChildBand x in item.ChildBands)
                                        {
                                            RowsCollection bandchild = x.Rows;

                                            foreach (var y in bandchild)
                                            {
                                                //valido los select dentro del padre
                                                if (y.Cells["Seleccion"].Text == "True")
                                                {
                                                    valor += Convert.ToDouble(y.Cells["Valor"].Value.ToString());
                                                    aporte += Convert.ToDouble(y.Cells["Aporte"].Value.ToString());
                                                    comision += Convert.ToDouble(y.Cells["Comision"].Value.ToString());
                                                    retencion += Convert.ToDouble(y.Cells["Retencion"].Value.ToString());
                                                    total += Convert.ToDouble(y.Cells["Total"].Value.ToString());
                                                    LIQUIDACION li = new LIQUIDACION();
                                                    li.LIQ_NUMDOC = Convert.ToInt64(item.Cells["Liquidacion"].Value.ToString());
                                                    li.MED_CODIGO = Convert.ToInt32(item.Cells["Codigo"].Value.ToString());
                                                    li.HOM_CODIGO = Convert.ToInt64(y.Cells["Honorario"].Value.ToString());

                                                    l.Add(li);
                                                }
                                            }

                                        }
                                        #region DETALLE CGDETMAE
                                        if (valor > 0) //valor neto del honorario 
                                        {
                                            linea++;
                                            //parametros = NegLiquidacion.parametrosHonorarios(18); //18 es el codido del parametro_detalle no cambiar
                                            parametros = NegLiquidacion.parametrosHonorarios(46); // 46 es el nuevo codigo parametro detalle Mario 23012023// se aumenta el parametro 46 para diferenciar el parametro de consulta externa
                                            cgdetmae = new Cgdetmae();
                                            cgdetmae.tipdoc = "AD";
                                            cgdetmae.numdoc = numcontrol;
                                            cgdetmae.numlinea = linea;
                                            cgdetmae.año = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString();
                                            cgdetmae.fechatran = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Date;
                                            cgdetmae.codzona = 1;
                                            cgdetmae.codloc = 1;
                                            parValor = parametros.PAD_VALOR.Split('-');
                                            cgdetmae.codcue_cp = parValor[0].Substring(0, 1);
                                            cgdetmae.cuenta_pc = parValor[0];
                                            cgdetmae.subcta_pc = parValor[1];
                                            cgdetmae.codpre_pc = parametros.PAD_VALOR;
                                            cgdetmae.codigo_c = Convert.ToDouble(medico.MED_CODIGO_MEDICO.Trim());
                                            cgdetmae.nocomp = item.Cells["Factura"].Value.ToString();
                                            cgdetmae.cheque = Convert.ToDouble(item.Cells["Liquidacion"].Value.ToString());
                                            cgdetmae.beneficiario = item.Cells["Medico"].Value.ToString();
                                            cgdetmae.debe = valor;
                                            cgdetmae.haber = 0;
                                            cgdetmae.comentario = "HONORARIOS MEDICOS - LIQUIDACION: " + item.Cells["Liquidacion"].Value.ToString();
                                            cgdetmae.movbanc = "FSS";
                                            cgdetmae.fechaing = fechaIngreso.Date;
                                            cgdetmae.fecha1 = Convert.ToInt64(Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Month.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.fecha2 = Convert.ToInt64(fechaIngreso.Year.ToString().PadLeft(4, '0') + fechaIngreso.Month.ToString().PadLeft(2, '0') + fechaIngreso.Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.printed = "N";
                                            cgdetmae.cierre = "N";
                                            cgdetmae.conciliado = "N";
                                            cgdetmae.autorizacion = item.Cells["Autorizacion"].Value.ToString();
                                            cgdetmae.sustentotrib = "02";
                                            cgdetmae.tipcomprob = "01";
                                            cgdetmae.feccaduca = item.Cells["FechaCaduca"].Text.ToString();
                                            cgdetmae.codretfuente = retencionFuente.RET_REFERENCIA;
                                            cgdetmae.estado = "N";

                                            ldet.Add(cgdetmae);
                                        }
                                        if (aporte > 0)
                                        {
                                            linea++;
                                            parametros = NegLiquidacion.parametrosHonorarios(15);
                                            cgdetmae = new Cgdetmae();
                                            cgdetmae.tipdoc = "AD";
                                            cgdetmae.numdoc = numcontrol;
                                            cgdetmae.numlinea = linea;
                                            cgdetmae.año = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0');
                                            cgdetmae.fechatran = Convert.ToDateTime(item.Cells["FechaFactura"].Text.ToString()).Date;
                                            cgdetmae.codzona = 1;
                                            cgdetmae.codloc = 1;
                                            parValor = parametros.PAD_VALOR.Split('-');
                                            cgdetmae.codcue_cp = parValor[0].Substring(0, 1);
                                            cgdetmae.cuenta_pc = parValor[0];
                                            cgdetmae.subcta_pc = parValor[1];
                                            cgdetmae.codpre_pc = parametros.PAD_VALOR;
                                            cgdetmae.codigo_c = Convert.ToDouble(medico.MED_CODIGO_MEDICO.Trim());
                                            cgdetmae.nocomp = item.Cells["Factura"].Value.ToString();
                                            cgdetmae.cheque = Convert.ToDouble(item.Cells["Liquidacion"].Value.ToString());
                                            cgdetmae.beneficiario = item.Cells["Medico"].Value.ToString();
                                            cgdetmae.debe = 0;
                                            cgdetmae.haber = aporte;
                                            cgdetmae.comentario = "APORTE - LIQUIDACION: " + item.Cells["Liquidacion"].Value.ToString();
                                            cgdetmae.movbanc = "0";
                                            cgdetmae.fechaing = fechaIngreso.Date;
                                            cgdetmae.fecha1 = Convert.ToInt64(Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Month.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.fecha2 = Convert.ToInt64(fechaIngreso.Year.ToString().PadLeft(4, '0') + fechaIngreso.Month.ToString().PadLeft(2, '0') + fechaIngreso.Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.printed = "N";
                                            cgdetmae.cierre = "N";
                                            cgdetmae.conciliado = "N";
                                            cgdetmae.autorizacion = item.Cells["Autorizacion"].Value.ToString();
                                            cgdetmae.sustentotrib = "02";
                                            cgdetmae.tipcomprob = "01";
                                            cgdetmae.feccaduca = item.Cells["FechaCaduca"].Text;
                                            cgdetmae.codretfuente = retencionFuente.RET_REFERENCIA;
                                            cgdetmae.estado = "N";

                                            ldet.Add(cgdetmae);
                                        }
                                        if (comision > 0)
                                        {
                                            linea++;
                                            parametros = NegLiquidacion.parametrosHonorarios(16);
                                            cgdetmae = new Cgdetmae();
                                            cgdetmae.tipdoc = "AD";
                                            cgdetmae.numdoc = numcontrol;
                                            cgdetmae.numlinea = linea;
                                            cgdetmae.año = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0');
                                            cgdetmae.fechatran = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Date;
                                            cgdetmae.codzona = 1;
                                            cgdetmae.codloc = 1;
                                            parValor = parametros.PAD_VALOR.Split('-');
                                            cgdetmae.codcue_cp = parValor[0].Substring(0, 1);
                                            cgdetmae.cuenta_pc = parValor[0];
                                            cgdetmae.subcta_pc = parValor[1];
                                            cgdetmae.codpre_pc = parametros.PAD_VALOR;
                                            cgdetmae.codigo_c = Convert.ToDouble(medico.MED_CODIGO_MEDICO.Trim());
                                            cgdetmae.nocomp = item.Cells["Factura"].Value.ToString();
                                            cgdetmae.cheque = Convert.ToDouble(item.Cells["Liquidacion"].Value.ToString());
                                            cgdetmae.beneficiario = item.Cells["Medico"].Value.ToString();
                                            cgdetmae.debe = 0;
                                            cgdetmae.haber = comision;
                                            cgdetmae.comentario = "COMISION - LIQUIDACION: " + item.Cells["Liquidacion"].Value.ToString();
                                            cgdetmae.movbanc = "0";
                                            cgdetmae.fechaing = fechaIngreso.Date;
                                            cgdetmae.fecha1 = Convert.ToInt64(Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Month.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.fecha2 = Convert.ToInt64(fechaIngreso.Year.ToString().PadLeft(4, '0') + fechaIngreso.Month.ToString().PadLeft(2, '0') + fechaIngreso.Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.printed = "N";
                                            cgdetmae.cierre = "N";
                                            cgdetmae.conciliado = "N";
                                            cgdetmae.autorizacion = item.Cells["Autorizacion"].Value.ToString();
                                            cgdetmae.sustentotrib = "02";
                                            cgdetmae.tipcomprob = "01";
                                            cgdetmae.feccaduca = item.Cells["FechaCaduca"].Text;
                                            cgdetmae.codretfuente = retencionFuente.RET_REFERENCIA;
                                            cgdetmae.estado = "N";

                                            ldet.Add(cgdetmae);
                                        }
                                        if (retencion > 0)
                                        {
                                            linea++;
                                            cgdetmae = new Cgdetmae();
                                            cgdetmae.tipdoc = "AD";
                                            cgdetmae.numdoc = numcontrol;
                                            cgdetmae.numlinea = linea;
                                            cgdetmae.año = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0');
                                            cgdetmae.fechatran = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Date;
                                            cgdetmae.codzona = 1;
                                            cgdetmae.codloc = 1;
                                            parValor = retencionFuente.COD_CUE.Split('-');
                                            cgdetmae.codcue_cp = parValor[0].Substring(0, 1);
                                            cgdetmae.cuenta_pc = parValor[0];
                                            cgdetmae.subcta_pc = parValor[1];
                                            cgdetmae.codpre_pc = retencionFuente.COD_CUE;
                                            cgdetmae.codigo_c = Convert.ToDouble(medico.MED_CODIGO_MEDICO.Trim());
                                            cgdetmae.nocomp = item.Cells["Factura"].Value.ToString();
                                            cgdetmae.cheque = Convert.ToDouble(item.Cells["Liquidacion"].Value.ToString());
                                            cgdetmae.beneficiario = item.Cells["Medico"].Value.ToString();
                                            cgdetmae.debe = 0;
                                            cgdetmae.haber = retencion;
                                            cgdetmae.comentario = "RETENCION - LIQUIDACION: " + item.Cells["Liquidacion"].Value.ToString();
                                            cgdetmae.movbanc = "RFS";
                                            cgdetmae.fechaing = fechaIngreso.Date;
                                            cgdetmae.fecha1 = Convert.ToInt64(Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Month.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.fecha2 = Convert.ToInt64(fechaIngreso.Year.ToString().PadLeft(4, '0') + fechaIngreso.Month.ToString().PadLeft(2, '0') + fechaIngreso.Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.printed = "N";
                                            cgdetmae.cierre = "N";
                                            cgdetmae.conciliado = "N";
                                            cgdetmae.autorizacion = item.Cells["Autorizacion"].Value.ToString();
                                            cgdetmae.sustentotrib = "02";
                                            cgdetmae.tipcomprob = "01";
                                            cgdetmae.feccaduca = item.Cells["FechaCaduca"].Text;
                                            cgdetmae.codretfuente = retencionFuente.RET_REFERENCIA;
                                            cgdetmae.estado = "N";

                                            ldet.Add(cgdetmae);
                                        }
                                        if (total > 0)
                                        {
                                            linea++;
                                            cgdetmae = new Cgdetmae();
                                            cgdetmae.tipdoc = "AD";
                                            cgdetmae.numdoc = numcontrol;
                                            cgdetmae.numlinea = linea;
                                            cgdetmae.año = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0');
                                            cgdetmae.fechatran = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Date;
                                            cgdetmae.codzona = 1;
                                            cgdetmae.codloc = 1;
                                            parValor = medico.MED_CUENTA_CONTABLE.Split('-');
                                            cgdetmae.codcue_cp = parValor[0].Substring(0, 1);
                                            cgdetmae.cuenta_pc = parValor[0];
                                            cgdetmae.subcta_pc = parValor[1];
                                            cgdetmae.codpre_pc = medico.MED_CUENTA_CONTABLE;
                                            cgdetmae.codigo_c = Convert.ToDouble(medico.MED_CODIGO_MEDICO.Trim());
                                            cgdetmae.nocomp = item.Cells["Factura"].Value.ToString();
                                            cgdetmae.cheque = Convert.ToDouble(item.Cells["Liquidacion"].Value.ToString());
                                            cgdetmae.beneficiario = item.Cells["Medico"].Value.ToString();
                                            cgdetmae.debe = 0;
                                            cgdetmae.haber = total;
                                            cgdetmae.comentario = "HON. MED. " + item.Cells["Medico"].Value.ToString() + " - LIQUIDACION: " + item.Cells["Liquidacion"].Value.ToString();
                                            cgdetmae.movbanc = "0";
                                            cgdetmae.fechaing = fechaIngreso.Date;
                                            cgdetmae.fecha1 = Convert.ToInt64(Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Month.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.fecha2 = Convert.ToInt64(fechaIngreso.Year.ToString().PadLeft(4, '0') + fechaIngreso.Month.ToString().PadLeft(2, '0') + fechaIngreso.Day.ToString().PadLeft(2, '0'));
                                            cgdetmae.printed = "N";
                                            cgdetmae.cierre = "N";
                                            cgdetmae.conciliado = "N";
                                            cgdetmae.autorizacion = item.Cells["Autorizacion"].Value.ToString();
                                            cgdetmae.sustentotrib = "02";
                                            cgdetmae.tipcomprob = "01";
                                            cgdetmae.feccaduca = item.Cells["FechaCaduca"].Text;
                                            cgdetmae.codretfuente = retencionFuente.RET_REFERENCIA;
                                            cgdetmae.estado = "N";

                                            ldet.Add(cgdetmae);
                                        }
                                        #endregion

                                        #region Cabecera CABMAE
                                        //creo la cabecera el cgcabmae
                                        cgcabmae = new Cgcabmae();
                                        cgcabmae.codrespon = His.Entidades.Clases.Sesion.codUsuario;
                                        cgcabmae.codzona = 1;
                                        cgcabmae.tipdoc = "AD";
                                        cgcabmae.numdoc = numcontrol;
                                        cgcabmae.año = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0');
                                        cgcabmae.fechatran = Convert.ToDateTime(item.Cells["FechaFactura"].Text).Date;
                                        cgcabmae.fechaing = fechaIngreso.Date;
                                        cgcabmae.observacion = "PCTES. CE. VARIOS LIQUIDACION: " + item.Cells["Liquidacion"].Value.ToString();
                                        cgcabmae.totdebe = valor;
                                        cgcabmae.tothaber = valor;
                                        cgcabmae.fecha1 = Convert.ToInt64(Convert.ToDateTime(item.Cells["FechaFactura"].Text).Year.ToString().PadLeft(4, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Month.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.Cells["FechaFactura"].Text).Day.ToString().PadLeft(2, '0'));
                                        cgcabmae.fecha2 = Convert.ToInt64(fechaIngreso.Year.ToString().PadLeft(4, '0') + fechaIngreso.Month.ToString().PadLeft(2, '0') + fechaIngreso.Day.ToString().PadLeft(2, '0'));
                                        cgcabmae.beneficiario = item.Cells["Medico"].Value.ToString();
                                        cgcabmae.vdolares = 0;
                                        cgcabmae.cierre = "N";
                                        cgcabmae.borrar = false;
                                        cgcabmae.solicitado = "NINGUNO";
                                        cgcabmae.depto = "NINGUNO";
                                        cgcabmae.autorizado = "NINGUNO";
                                        cgcabmae.HOM_CODIGO = 0;

                                        lcab.Add(cgcabmae);
                                        #endregion

                                        if (lcab.Count > 0)
                                        {
                                            if (ldet.Count > 0)
                                            {
                                                //bloqueo las liquidaciones hechas el asiento contable
                                                if (NegLiquidacion.bloquearLiquidacion(ldet, l))
                                                {
                                                    if (NegLiquidacion.LiquidacionGlobal(lcab, ldet))
                                                    {
                                                        MessageBox.Show("Asiento(s) contable(s) generado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        NegLiquidacion.LiberarControlADS(Convert.ToDateTime(item.Cells["FechaFactura"].Text));
                                                        if (!NegLiquidacion.guardoAudtoria(lcab, Convert.ToInt64(medico.MED_CODIGO_MEDICO.Trim()), "ASIENTO GENERADO POR HIS LIQUIDACION CE", fechaIngreso))
                                                            MessageBox.Show("No se ha guardado en la auditora.\r\nConsulte con el Administrador.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("No se pudo generar el/los asiento(s) contable(s)", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        if (!NegLiquidacion.guardoAudtoria(lcab, Convert.ToInt64(medico.MED_CODIGO_MEDICO.Trim()), "ALGO OCURRIO AL GENERAR ASIENTO: " + numcontrol, fechaIngreso))
                                                            MessageBox.Show("No se ha guardado en la auditora.\r\nConsulte con el Administrador.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                        if (!NegLiquidacion.desbloquearLiquidacion(Convert.ToInt64(item.Cells["Liquidacion"].Value.ToString()), medico.MED_CODIGO, numcontrol, ldet))
                                                        {
                                                            MessageBox.Show("No se pudo desbloquear la liquidacion.\r\nConsulte con el proveedor.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            return;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("No se pudo crear detalle de la liquidacion.\r\nConsulte con el proveedor.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se pudo obtener el numero de control intente mas tarde.", "HIS3000",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        NegLiquidacion.LiberarControlADS(Convert.ToDateTime(item.Cells["FechaFactura"].Text));

                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("La fecha de la factura: " + item.Cells["Factura"].Value.ToString() + " no es valida.",
                                        "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe ingresar la fecha de factura del medico.",
                                        "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe ingresar el numero de factura del medico: " +
                                item.Cells["Medico"].Value.ToString(), "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                if (i == 0)
                    MessageBox.Show("Debe elegir medico para generar el asiento contable", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarLiquidaciones();
                //mando a imprimir reporte de asiento contable
                if (lcab.Count > 0)
                    Imprimir(lcab);
            }
            else
                MessageBox.Show("No tiene honorarios pendientes ha generar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void Imprimir(List<Cgcabmae> cab)
        {
            Formulario.DSAsiento asiento = new Formulario.DSAsiento();
            DataRow cabecera;
            bool asientos = false;

            foreach (var item in cab)
            {
                USUARIOS users = new USUARIOS();
                try
                {
                    DataTable TCabecera = NegLiquidacion.reporteAsientoLiquidacion(item.numdoc, item.tipdoc, 0);

                    //DataTable TDetalle = NegHonorariosMedicos.ImpresionAsientos(Convert.ToInt64(item.Cells["COD"].Value.ToString()), 1);
                    if (TCabecera.Rows.Count > 0)
                    {
                        asientos = true;

                        if (TCabecera.Rows.Count > 0)
                        {
                            for (int i = 0; i < TCabecera.Rows.Count; i++)
                            {
                                cabecera = asiento.Tables["Cabecera"].NewRow();
                                cabecera["numdoc"] = TCabecera.Rows[i]["numdoc"].ToString();
                                users = NegUsuarios.RecuperaUsuario(Convert.ToInt16(TCabecera.Rows[i]["codrespon"].ToString()));
                                cabecera["usuario"] = users.USR;
                                cabecera["logo"] = NegUtilitarios.RutaLogo("General");
                                cabecera["fecha"] = TCabecera.Rows[i]["fechatran"].ToString();
                                cabecera["observacion"] = TCabecera.Rows[i]["observacion"].ToString();
                                cabecera["beneficiario"] = TCabecera.Rows[i]["beneficiario"].ToString();
                                cabecera["numCuenta"] = TCabecera.Rows[i]["CODIGO"].ToString();
                                cabecera["Cuenta"] = TCabecera.Rows[i]["nomcue_pc"].ToString();
                                cabecera["Auxiliar"] = TCabecera.Rows[i]["codigo_c"].ToString();
                                cabecera["numFac"] = TCabecera.Rows[i]["nocomp"].ToString();
                                cabecera["Debe"] = TCabecera.Rows[i]["debe"].ToString();
                                cabecera["Haber"] = TCabecera.Rows[i]["haber"].ToString();

                                asiento.Tables["Cabecera"].Rows.Add(cabecera);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (asientos)
            {
                Formulario.frmReportes x = new Formulario.frmReportes(1, "HonorariosAsiento", asiento);
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("No tiene asiento generados.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public bool Explorador(int med_codigo)
        {
            MEDICOS med = NegMedicos.recuperarMedico(med_codigo);
            var fileContent = string.Empty;
            var filePath = string.Empty;
            bool valido = true;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    if (!CargarFElectronica(filePath, med.MED_RUC))
                        valido = false;
                }
            }
            return valido;
        }
        public string fecha = "";
        public string factura = "";
        public string autorizacion = "";
        public bool libretin = true;
        public bool CargarFElectronica(string filePath, string rucMedico)
        {
            XmlReader reader = XmlReader.Create(filePath);
            string ruc = "";
            bool valido = true;
            while (reader.Read())
            {
                try
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "claveAcceso":
                                autorizacion = reader.ReadString();
                                break;
                            case "numeroAutorizacion":
                                autorizacion = reader.ReadString();
                                break;
                        }
                    }
                    fecha = autorizacion.Substring(0, 8);
                    ruc = autorizacion.Substring(10, 13);
                    factura = autorizacion.Substring(24, 15);
                    if (ruc != rucMedico)
                        valido = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El archivo XML contiene errores: " + ex.Message, "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    valido = false;
                    //throw;
                }
            }
            return valido;

        }
    }
}
