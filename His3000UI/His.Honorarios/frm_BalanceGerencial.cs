using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using His.Negocio;
using His.Entidades;
using Infragistics.Win.UltraWinEditors;
using Recursos;

namespace His.Honorarios
{
    public partial class frm_BalanceGerencial : Form
    {
        public frm_BalanceGerencial()
        {
            InitializeComponent();
        }

        #region metodos privados
        public void cargarRecursos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            toolStripSplitButtonNuevo.Image = Archivo.imgBtnAdd2;
            toolStripButtonCancelar.Image = Archivo.imgBtnStop;
            toolStripButtonGuardar.Image = Archivo.imgBtnFloppy;
            toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            toolStripButtonExportar.Image = Archivo.imgOfficeExcel;
            toolStripButtonImprimir.Image = Archivo.imgBtnImprimir32;
            toolStripButtonSalir.Image = Archivo.imgBtnSalir32;
            //chart
           uChartFormaPagoRecuperados.Data.RowLabelsColumn = 10;
           uChartFormaPagoRecuperados.Data.UseRowLabelsColumn = true;
            //
           splitContainerPorRecuperar.Panel2Collapsed = true;
           splitContainerHonPorPagar.Panel2Collapsed = true;
           splitContainerIngresados.Panel2Collapsed = true; 
            
        }
        //filtro las facturas ingresadas de los medicos
        private void filtrarFacturasIngresadasMedicos(string parTipo)
        {
            string tipo = parTipo;
            string porRecuperar = null;
            string medCodigo = null;
            string espCodigo = null;
            string tihCodigo = null;
            string timCodigo = null;
            string medRecibeLlamada = null;
            string fechaIniFacturaMedico = null;
            string fechaFinFacturaMedico = null;
            string honorariosCancelados = null;
            string sinRetencion = null;
            string forCodigo = null;
            string tifCodigo = null; 
            string lote = null;
            string numeroControl = null;
            string facturaClinica = null;
            string fechaIniFacturaCliente = null;
            string fechaFinFacturaCliente = null;
            string pacienteReferido = null;
            string pacienteFechaAlta = null;
            string ateCodigo = null;
            string pacCodigo = null;
            try
            {
                //filtro Fechas
                if (optPorDefecto.Checked == true)
                {
                    fechaIniFacturaMedico = String.Format("{0:yyyy/MM/dd}", udteDesde.Value);
                    fechaFinFacturaMedico = String.Format("{0:yyyy/MM/dd}", udteHasta.Value);
                }
                else if (optFacturaClinica.Checked == true)
                {
                    fechaIniFacturaCliente = String.Format("{0:yyyy/MM/dd}", udteDesde.Value);
                    fechaFinFacturaCliente = String.Format("{0:yyyy/MM/dd}", udteHasta.Value);
                }
                //filtro tipo forma de pago
                if (cboFiltroTipoFormaPago.SelectedIndex> 0)
                {
                    TIPO_FORMA_PAGO tipoFormaPago = (TIPO_FORMA_PAGO)cboFiltroTipoFormaPago.SelectedItem.ListObject;
                    tifCodigo = tipoFormaPago.TIF_CODIGO.ToString();
                }
                else
                {
                    tifCodigo = null;
                }
                //filtro forma de pago
                if (cboFiltroFormaPago.Visible == true && cboFiltroFormaPago.SelectedIndex > 0)
                {
                    FORMA_PAGO formaPago = (FORMA_PAGO)cboFiltroFormaPago.SelectedItem.ListObject;
                    forCodigo = formaPago.FOR_CODIGO.ToString();
                }
                else
                {
                    forCodigo = null;
                }

                List<HONORARIOS_VISTA> honorariosMedicos = NegHonorariosMedicos.RecuperarHonorariosMedicos(tipo, porRecuperar, medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                    fechaFinFacturaMedico, honorariosCancelados, sinRetencion, forCodigo, tifCodigo, lote, numeroControl, facturaClinica, fechaIniFacturaCliente,
                    fechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta, ateCodigo, pacCodigo); 
                //ultraGridHonRecuperados.DataSource =honorariosMedicos;
                ultraGridHonIngresados.DataSource = honorariosMedicos;  

                var listaFormaPago = from h in honorariosMedicos 
                                    group h by h.FOR_DESCRIPCION into g
                                    select new {Forma_Pago=g.Key,Total = g.Sum(h=>h.HOM_VALOR_NETO) };
                //uChartFormaPagoRecuperados.DataSource = listaFormaPago.ToList();
                if (listaFormaPago.Count() > 0)
                {
                    ultraChartHonIngresados.DataSource = listaFormaPago.ToList();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error al cargar la información del medico");
            }
        }

        //cargo el filtro de las formas de pago
        public void cargarTipoFormaPago(UltraComboEditor  combo)
        {
            try
            {
                List<TIPO_FORMA_PAGO> listaTipoFormaPago = NegFormaPago.RecuperaTipoFormaPagos();
                TIPO_FORMA_PAGO tipo = new TIPO_FORMA_PAGO();
                tipo.TIF_CODIGO = 0;
                tipo.TIF_NOMBRE = " Todas";
                listaTipoFormaPago.Add(tipo);
                combo.DataSource = listaTipoFormaPago.OrderBy(t => t.TIF_NOMBRE).ToList();    
                combo.DisplayMember = "TIF_NOMBRE";
                combo.ValueMember = "TIF_CODIGO";
                combo.SelectedIndex = 0;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "err");
            }
        }

        private void cargarHonorariosRecuperados()
        {
            try
            {
                //variables de filtro
                string codigoMedico = null;
                string tipoFormaPagoId = null;
                string formaPagoId = null;
                string fechaFacturaIni = null;
                string fechaFacturaFin = null;
                //Cargo Filtros

                //filtro los honorarios por forma de pago
                if (cboFiltroTipoFormaPago.SelectedIndex > 0)
                {
                    TIPO_FORMA_PAGO tipoFormaPago = (TIPO_FORMA_PAGO)cboFiltroTipoFormaPago.SelectedItem.ListObject;
                    tipoFormaPagoId = tipoFormaPago.TIF_CODIGO.ToString();
                    if (cboFiltroFormaPago.SelectedIndex > 0)
                    {
                        FORMA_PAGO formaPago = (FORMA_PAGO)cboFiltroFormaPago.SelectedItem.ListObject;
                        formaPagoId = formaPago.FOR_CODIGO.ToString();
                    }
                }
                //fecha fatura medico
                if (optPorDefecto.Checked == true)
                {
                    fechaFacturaIni = String.Format("{0:yyyy/MM/dd}", udteDesde.Value);
                    fechaFacturaFin = String.Format("{0:yyyy/MM/dd}", udteHasta.Value);
                }
                List<HONORARIOS_MEDICOS_TRANSFERENCIAS> listaHonrariosMedicos = NegHonorariosMedicos.RecuperarHonorariosMedicosPorTransferir(codigoMedico, tipoFormaPagoId, formaPagoId, fechaFacturaIni, fechaFacturaFin);
                var listaAgrupada = (from h in listaHonrariosMedicos
                                     group h by h.NOMBRE_MEDICO into grupo
                                     select new { MEDICO = grupo.Key, Total = grupo.Sum(h => h.VALOR_A_CANCELAR), DETALLE = grupo }).ToList();
                ultraGridHonPorPagar.DataSource = listaAgrupada;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        #endregion

        #region eventos
        private void ultraGridPorPagar_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {

                if (ultraGridHonRecuperados.DataSource != null)
                {
                    UltraGridBand bandUno = ultraGridHonRecuperados.DisplayLayout.Bands[0];

                    ultraGridHonRecuperados.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                    ultraGridHonRecuperados.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                    ultraGridHonRecuperados.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                    ultraGridHonRecuperados.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                    bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    //Caracteristicas de Filtro en la grilla
                    ultraGridHonRecuperados.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                    ultraGridHonRecuperados.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                    ultraGridHonRecuperados.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                    ultraGridHonRecuperados.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                    ultraGridHonRecuperados.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                    //
                    ultraGridHonRecuperados.DisplayLayout.UseFixedHeaders = true;
                    //Cambio la apariencia de las sumas
                    bandUno.Summaries.Clear();
                    bandUno.SummaryFooterCaption = "Totales: ";
                    bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                    bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
                    bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.LightYellow;
                    //totalizo las columnas
                    SummarySettings sumHonorarios = bandUno.Summaries.Add("Honorarios", SummaryType.Sum, bandUno.Columns["HOM_VALOR_NETO"]);
                    //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
                    sumHonorarios.DisplayFormat = "{0:#####.00}";
                    sumHonorarios.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    SummarySettings sumComision = bandUno.Summaries.Add("Comision", SummaryType.Sum, bandUno.Columns["HOM_COMISION_CLINICA"]);
                    //sumComision.DisplayFormat = "Tot = {0:#####.00}";
                    sumComision.DisplayFormat = "{0:#####.00}";
                    sumComision.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    SummarySettings sumReferido = bandUno.Summaries.Add("Referido", SummaryType.Sum, bandUno.Columns["HOM_APORTE_LLAMADA"]);
                    //sumReferido.DisplayFormat = "Tot = {0:#####.00}";
                    sumReferido.DisplayFormat = "{0:#####.00}";
                    sumReferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumRetenido = bandUno.Summaries.Add("Retenido", SummaryType.Sum, bandUno.Columns["HOM_RETENCION"]);
                    sumRetenido.DisplayFormat = "{0:#####.00}";
                    sumRetenido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorPagar = bandUno.Summaries.Add("Valor a pagar", SummaryType.Sum, bandUno.Columns["HOM_VALOR_TOTAL"]);
                    sumValorPagar.DisplayFormat = "{0:#####.00}";
                    sumValorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorRecuperado = bandUno.Summaries.Add("Valor Recuperado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_PAGADO"]);
                    sumValorRecuperado.DisplayFormat = "{0:#####.00}";
                    sumValorRecuperado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorPorRecuperar = bandUno.Summaries.Add("Valor Por Recuperar", SummaryType.Sum, bandUno.Columns["VALOR_POR_RECUPERAR"]);
                    sumValorPorRecuperar.DisplayFormat = "{0:#####.00}";
                    sumValorPorRecuperar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorCancelado = bandUno.Summaries.Add("Valor Cancelado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_CANCELADO"]);
                    sumValorCancelado.DisplayFormat = "{0:#####.00}";
                    sumValorCancelado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;


                    //Cambio el nombre de las cabeceras
                    bandUno.Columns["MED_CODIGO"].Header.Caption = "CODIGO";
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Caption = "MEDICO";
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA DEL MEDICO";
                    bandUno.Columns["HOM_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.Caption = "PACIENTE";
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Header.Caption = "NUMERO DE CONTROL";
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.Caption = "FACTURA PACIENTE";
                    bandUno.Columns["ATE_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
                    bandUno.Columns["FOR_DESCRIPCION"].Header.Caption = "FORMA DE PAGO";
                    bandUno.Columns["HOM_LOTE"].Header.Caption = "LOTE";
                    bandUno.Columns["RET_CODIGO1"].Header.Caption = "NUM. RETENCION";
                    bandUno.Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
                    bandUno.Columns["HOM_COMISION_CLINICA"].Header.Caption = "COMISION CLINICA";
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Header.Caption = "REFERIDO";
                    bandUno.Columns["HOM_RETENCION"].Header.Caption = "VALOR RETENCION";
                    bandUno.Columns["HOM_VALOR_TOTAL"].Header.Caption = "VALOR A PAGAR";
                    bandUno.Columns["HOM_VALOR_PAGADO"].Header.Caption = "VALOR RECUPERADO";
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Header.Caption = "VALOR POR RECUPERAR";
                    bandUno.Columns["HOM_RECORTE"].Header.Caption = "RECORTE";
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Header.Caption = "VALOR CANCELADO";
                    bandUno.Columns["HOM_OBSERVACION"].Header.Caption = "OBSERVACION";

                    //modifico el ancho por defecto de las columnas
                    bandUno.Columns["MED_CODIGO"].Width = 50;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Width = 200;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Width = 100;
                    bandUno.Columns["HOM_FACTURA_FECHA"].Width = 70;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Width = 200;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Width = 80;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Width = 100;
                    bandUno.Columns["ATE_FACTURA_FECHA"].Width = 70;
                    bandUno.Columns["FOR_DESCRIPCION"].Width = 120;
                    bandUno.Columns["HOM_LOTE"].Width = 80;
                    bandUno.Columns["RET_CODIGO1"].Width = 100;
                    bandUno.Columns["HOM_VALOR_NETO"].Width = 100;
                    bandUno.Columns["HOM_COMISION_CLINICA"].Width = 100;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Width = 100;
                    bandUno.Columns["HOM_RETENCION"].Width = 100;
                    bandUno.Columns["HOM_VALOR_TOTAL"].Width = 100;
                    bandUno.Columns["HOM_VALOR_PAGADO"].Width = 100;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Width = 100;
                    bandUno.Columns["HOM_RECORTE"].Width = 100;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Width = 100;
                    bandUno.Columns["HOM_OBSERVACION"].Width = 250;

                    //alineo las columnas
                    bandUno.Columns["MED_CODIGO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["ATE_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["FOR_DESCRIPCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["HOM_LOTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["RET_CODIGO1"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["HOM_VALOR_NETO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_COMISION_CLINICA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_RETENCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_VALOR_TOTAL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_VALOR_PAGADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_RECORTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_OBSERVACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    //ordeno las columnas
                    bandUno.Columns["MED_CODIGO"].Header.VisiblePosition = 1;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.VisiblePosition = 2;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Header.VisiblePosition = 3;
                    bandUno.Columns["HOM_FACTURA_FECHA"].Header.VisiblePosition = 4;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.VisiblePosition = 5;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Header.VisiblePosition = 6;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.VisiblePosition = 7;
                    bandUno.Columns["ATE_FACTURA_FECHA"].Header.VisiblePosition = 8;
                    bandUno.Columns["FOR_DESCRIPCION"].Header.VisiblePosition = 9;
                    bandUno.Columns["HOM_LOTE"].Header.VisiblePosition = 10;
                    bandUno.Columns["RET_CODIGO1"].Header.VisiblePosition = 11;
                    bandUno.Columns["HOM_VALOR_NETO"].Header.VisiblePosition = 12;
                    bandUno.Columns["HOM_COMISION_CLINICA"].Header.VisiblePosition = 13;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Header.VisiblePosition = 14;
                    bandUno.Columns["HOM_RETENCION"].Header.VisiblePosition = 15;
                    bandUno.Columns["HOM_VALOR_TOTAL"].Header.VisiblePosition = 16;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Header.VisiblePosition = 17;
                    bandUno.Columns["HOM_RECORTE"].Header.VisiblePosition = 18;
                    bandUno.Columns["HOM_VALOR_PAGADO"].Header.VisiblePosition = 19;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Header.VisiblePosition = 20;
                    bandUno.Columns["HOM_OBSERVACION"].Header.VisiblePosition = 21;

                    //
                    bandUno.Columns["MED_CODIGO"].Header.Fixed = true;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Fixed = true;
                    //Oculto columnas 
                    bandUno.Columns["HOM_CODIGO"].Hidden = true;
                    bandUno.Columns["ATE_CODIGO"].Hidden = true;
                    bandUno.Columns["FOR_CODIGO"].Hidden = true;
                    bandUno.Columns["ID_USUARIO"].Hidden = true;
                    bandUno.Columns["HOM_FECHA_INGRESO"].Hidden = true;
                    bandUno.Columns["TMO_NOMBRE"].Hidden = true;
                    bandUno.Columns["TMO_CODIGO"].Hidden = true;
                    bandUno.Columns["MED_RUC"].Hidden = true;
                    //Oculto columnas que podran añadirse desde la aplicacion
                    bandUno.Columns["MED_CODIGO"].Hidden = true;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Hidden = true;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Hidden = true;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Hidden = true;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Hidden = true;
                    bandUno.Columns["ATE_FACTURA_FECHA"].Hidden = true;
                    bandUno.Columns["HOM_LOTE"].Hidden = true;
                    bandUno.Columns["RET_CODIGO1"].Hidden = true;
                    bandUno.Columns["HOM_COMISION_CLINICA"].Hidden = true;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Hidden = true;
                    bandUno.Columns["HOM_RETENCION"].Hidden = true;
                    bandUno.Columns["HOM_VALOR_TOTAL"].Hidden = true;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Hidden = true;
                    bandUno.Columns["HOM_RECORTE"].Hidden = true;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Hidden = true;
                    bandUno.Columns["HOM_OBSERVACION"].Hidden = true;

                    //excluyó columnas no visibles de la seleccion
                    bandUno.Columns["HOM_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["ATE_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["FOR_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["ID_USUARIO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["HOM_FECHA_INGRESO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["TMO_NOMBRE"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["TMO_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["MED_RUC"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                }
            }
            catch (Exception err)
            { 
                MessageBox.Show("error",err.Message,MessageBoxButtons.OK,MessageBoxIcon.Error);     
            }
        }

        private void frm_BalanceGerencial_Load(object sender, EventArgs e)
        {
            try
            {
                cargarRecursos();
                cargarTipoFormaPago(cboFiltroTipoFormaPago);
            }
            catch (Exception err)
            {
                if (err.InnerException != null)
                    MessageBox.Show(err.InnerException.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
        }

        private void optNinguno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!optNinguno.Checked)
                {
                    udteDesde.Enabled = true;
                    udteHasta.Enabled = true;
                }
                else
                {
                    udteDesde.Enabled = false;
                    udteHasta.Enabled = false;
                }
            }
            catch (Exception err)
            {
                if (err.InnerException != null)
                    MessageBox.Show(err.InnerException.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

        private void cboFiltroTipoFormaPago_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboFiltroTipoFormaPago.SelectedIndex   > 0)
                {
                    TIPO_FORMA_PAGO item = (TIPO_FORMA_PAGO)cboFiltroTipoFormaPago.SelectedItem.ListObject; 
                    Int16 codigo = (Int16)(item.TIF_CODIGO);
                    cboFiltroFormaPago.Enabled = true;
                    lbFiltroFormaPago.Enabled = true;

                    List<FORMA_PAGO> listaFormaPago = NegFormaPago.RecuperaFormaPago(codigo);
                    FORMA_PAGO formaPago = new FORMA_PAGO();
                    formaPago.FOR_CODIGO = 0;
                    formaPago.FOR_DESCRIPCION = " Todas";
                    listaFormaPago.Add(formaPago);   
                    cboFiltroFormaPago.DataSource = listaFormaPago.OrderBy(f=>f.FOR_DESCRIPCION).ToList();
                    cboFiltroFormaPago.DisplayMember = "FOR_DESCRIPCION";
                    cboFiltroFormaPago.ValueMember = "FOR_CODIGO";
                    cboFiltroFormaPago.SelectedIndex = 0;

                }
                else
                {
                    cboFiltroFormaPago.Enabled = false;
                    lbFiltroFormaPago.Enabled =false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error");
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ultraTabControlBalace.Tabs[0].Active)
                {
                    splitContainerIngresados.Panel2Collapsed = false;
                    splitContainerIngresados.SplitterDistance = splitContainerIngresados.Width / 2;
                    filtrarFacturasIngresadasMedicos(Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosMedicos.ToString() );
                }
                else if (ultraTabControlBalace.Tabs[1].Active)
                {
                    splitContainerPorRecuperar.Panel2Collapsed = false;
                    splitContainerPorRecuperar.SplitterDistance = splitContainerPorRecuperar.Width / 2;
                    filtrarFacturasIngresadasMedicos("1");
                }
                else if (ultraTabControlBalace.Tabs[2].Active)
                {
                    cargarHonorariosRecuperados();
                }
            }
            catch (Exception err)
            {
                if (err.InnerException != null)
                    MessageBox.Show(err.InnerException.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ultraGridRecuperados_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                //Opciones por defecto de la grilla   
                ultraGridHonPorPagar.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
                ultraGridHonPorPagar.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                //Caracteristicas de Filtro en la grilla
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                //Totalizo columnas
                ultraGridHonPorPagar.DisplayLayout.Bands[0].Summaries.Clear();
                ultraGridHonPorPagar.DisplayLayout.Bands[1].SummaryFooterCaption = "Totales: ";
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Override.SummaryFooterCaptionAppearance.ForeColor = Color.LightYellow;
                //edito las columnas
                ultraGridHonPorPagar.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.Always;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].MergedCellStyle = MergedCellStyle.Always;

                //añado las sumatorias a las columnas
                SummarySettings sumValorHonorario = ultraGridHonPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor Honorario", SummaryType.Sum, ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_NETO"]);
                sumValorHonorario.DisplayFormat = "{0:#####.00}";
                sumValorHonorario.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorarPorPagar = ultraGridHonPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor a Pagar", SummaryType.Sum, ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_TOTAL"]);
                sumValorarPorPagar.DisplayFormat = "{0:#####.00}";
                sumValorarPorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorTransferido = ultraGridHonPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor Transferido", SummaryType.Sum, ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_CANCELADO"]);
                sumValorTransferido.DisplayFormat = "{0:#####.00}";
                sumValorTransferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorarPorTransferir = ultraGridHonPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor por transferir", SummaryType.Sum, ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["VALOR_A_CANCELAR"]);
                sumValorarPorTransferir.DisplayFormat = "{0:#####.00}";
                sumValorarPorTransferir.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].Header.Caption = "MEDICO";
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA";
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_TOTAL"].Header.Caption = "VALOR CON DESCUENTOS";
                ////ultraGridRecuperados.DisplayLayout.Bands[0].Columns["HOM_VALOR_PAGADO"].Header.Caption ="VALOR PAGADO"; 
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_CANCELADO"].Header.Caption = "VALOR TRANSFERIDO";
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["VALOR_A_CANCELAR"].Header.Caption = "VALOR A TRANSFERIR";

                //cambio a no editables los campos de consulta
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].CellActivation = Activation.NoEdit;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_MEDICO"].CellActivation = Activation.NoEdit;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_FECHA"].CellActivation = Activation.NoEdit;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_NETO"].CellActivation = Activation.NoEdit;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_TOTAL"].CellActivation = Activation.NoEdit;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_CANCELADO"].CellActivation = Activation.NoEdit;


                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].Hidden = true;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_PAGADO"].Hidden = true;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["HOM_CODIGO"].Hidden = true;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["MED_CODIGO"].Hidden = true;
                ultraGridHonPorPagar.DisplayLayout.Bands[1].Columns["FOR_CODIGO"].Hidden = true;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ultraGridHonIngresados_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {

                if (ultraGridHonIngresados.DataSource != null)
                {
                    UltraGridBand bandUno = ultraGridHonIngresados.DisplayLayout.Bands[0];

                    ultraGridHonIngresados.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                    ultraGridHonIngresados.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                    ultraGridHonIngresados.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                    ultraGridHonIngresados.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                    bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    //Caracteristicas de Filtro en la grilla
                    ultraGridHonIngresados.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                    ultraGridHonIngresados.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                    ultraGridHonIngresados.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                    ultraGridHonIngresados.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                    ultraGridHonIngresados.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                    //
                    ultraGridHonIngresados.DisplayLayout.UseFixedHeaders = true;
                    //Cambio la apariencia de las sumas
                    bandUno.Summaries.Clear();
                    bandUno.SummaryFooterCaption = "Totales: ";
                    bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                    bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
                    bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.LightYellow;
                    //totalizo las columnas
                    SummarySettings sumHonorarios = bandUno.Summaries.Add("Honorarios", SummaryType.Sum, bandUno.Columns["HOM_VALOR_NETO"]);
                    //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
                    sumHonorarios.DisplayFormat = "{0:#####.00}";
                    sumHonorarios.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    SummarySettings sumComision = bandUno.Summaries.Add("Comision", SummaryType.Sum, bandUno.Columns["HOM_COMISION_CLINICA"]);
                    //sumComision.DisplayFormat = "Tot = {0:#####.00}";
                    sumComision.DisplayFormat = "{0:#####.00}";
                    sumComision.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    SummarySettings sumReferido = bandUno.Summaries.Add("Referido", SummaryType.Sum, bandUno.Columns["HOM_APORTE_LLAMADA"]);
                    //sumReferido.DisplayFormat = "Tot = {0:#####.00}";
                    sumReferido.DisplayFormat = "{0:#####.00}";
                    sumReferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumRetenido = bandUno.Summaries.Add("Retenido", SummaryType.Sum, bandUno.Columns["HOM_RETENCION"]);
                    sumRetenido.DisplayFormat = "{0:#####.00}";
                    sumRetenido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorPagar = bandUno.Summaries.Add("Valor a pagar", SummaryType.Sum, bandUno.Columns["HOM_VALOR_TOTAL"]);
                    sumValorPagar.DisplayFormat = "{0:#####.00}";
                    sumValorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorRecuperado = bandUno.Summaries.Add("Valor Recuperado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_PAGADO"]);
                    sumValorRecuperado.DisplayFormat = "{0:#####.00}";
                    sumValorRecuperado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorPorRecuperar = bandUno.Summaries.Add("Valor Por Recuperar", SummaryType.Sum, bandUno.Columns["VALOR_POR_RECUPERAR"]);
                    sumValorPorRecuperar.DisplayFormat = "{0:#####.00}";
                    sumValorPorRecuperar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorCancelado = bandUno.Summaries.Add("Valor Cancelado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_CANCELADO"]);
                    sumValorCancelado.DisplayFormat = "{0:#####.00}";
                    sumValorCancelado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;


                    //Cambio el nombre de las cabeceras
                    bandUno.Columns["MED_CODIGO"].Header.Caption = "CODIGO";
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Caption = "MEDICO";
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA DEL MEDICO";
                    bandUno.Columns["HOM_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.Caption = "PACIENTE";
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Header.Caption = "NUMERO DE CONTROL";
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.Caption = "FACTURA PACIENTE";
                    bandUno.Columns["ATE_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
                    bandUno.Columns["FOR_DESCRIPCION"].Header.Caption = "FORMA DE PAGO";
                    bandUno.Columns["HOM_LOTE"].Header.Caption = "LOTE";
                    bandUno.Columns["RET_CODIGO1"].Header.Caption = "NUM. RETENCION";
                    bandUno.Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
                    bandUno.Columns["HOM_COMISION_CLINICA"].Header.Caption = "COMISION CLINICA";
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Header.Caption = "REFERIDO";
                    bandUno.Columns["HOM_RETENCION"].Header.Caption = "VALOR RETENCION";
                    bandUno.Columns["HOM_VALOR_TOTAL"].Header.Caption = "VALOR A PAGAR";
                    bandUno.Columns["HOM_VALOR_PAGADO"].Header.Caption = "VALOR RECUPERADO";
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Header.Caption = "VALOR POR RECUPERAR";
                    bandUno.Columns["HOM_RECORTE"].Header.Caption = "RECORTE";
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Header.Caption = "VALOR CANCELADO";
                    bandUno.Columns["HOM_OBSERVACION"].Header.Caption = "OBSERVACION";

                    //modifico el ancho por defecto de las columnas
                    bandUno.Columns["MED_CODIGO"].Width = 50;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Width = 200;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Width = 100;
                    bandUno.Columns["HOM_FACTURA_FECHA"].Width = 70;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Width = 200;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Width = 80;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Width = 100;
                    bandUno.Columns["ATE_FACTURA_FECHA"].Width = 70;
                    bandUno.Columns["FOR_DESCRIPCION"].Width = 120;
                    bandUno.Columns["HOM_LOTE"].Width = 80;
                    bandUno.Columns["RET_CODIGO1"].Width = 100;
                    bandUno.Columns["HOM_VALOR_NETO"].Width = 100;
                    bandUno.Columns["HOM_COMISION_CLINICA"].Width = 100;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Width = 100;
                    bandUno.Columns["HOM_RETENCION"].Width = 100;
                    bandUno.Columns["HOM_VALOR_TOTAL"].Width = 100;
                    bandUno.Columns["HOM_VALOR_PAGADO"].Width = 100;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Width = 100;
                    bandUno.Columns["HOM_RECORTE"].Width = 100;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Width = 100;
                    bandUno.Columns["HOM_OBSERVACION"].Width = 250;

                    //alineo las columnas
                    bandUno.Columns["MED_CODIGO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["ATE_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["FOR_DESCRIPCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["HOM_LOTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["RET_CODIGO1"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["HOM_VALOR_NETO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_COMISION_CLINICA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_RETENCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_VALOR_TOTAL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_VALOR_PAGADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_RECORTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_OBSERVACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    //ordeno las columnas
                    bandUno.Columns["MED_CODIGO"].Header.VisiblePosition = 1;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.VisiblePosition = 2;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Header.VisiblePosition = 3;
                    bandUno.Columns["HOM_FACTURA_FECHA"].Header.VisiblePosition = 4;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.VisiblePosition = 5;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Header.VisiblePosition = 6;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.VisiblePosition = 7;
                    bandUno.Columns["ATE_FACTURA_FECHA"].Header.VisiblePosition = 8;
                    bandUno.Columns["FOR_DESCRIPCION"].Header.VisiblePosition = 9;
                    bandUno.Columns["HOM_LOTE"].Header.VisiblePosition = 10;
                    bandUno.Columns["RET_CODIGO1"].Header.VisiblePosition = 11;
                    bandUno.Columns["HOM_VALOR_NETO"].Header.VisiblePosition = 12;
                    bandUno.Columns["HOM_COMISION_CLINICA"].Header.VisiblePosition = 13;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Header.VisiblePosition = 14;
                    bandUno.Columns["HOM_RETENCION"].Header.VisiblePosition = 15;
                    bandUno.Columns["HOM_VALOR_TOTAL"].Header.VisiblePosition = 16;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Header.VisiblePosition = 17;
                    bandUno.Columns["HOM_RECORTE"].Header.VisiblePosition = 18;
                    bandUno.Columns["HOM_VALOR_PAGADO"].Header.VisiblePosition = 19;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Header.VisiblePosition = 20;
                    bandUno.Columns["HOM_OBSERVACION"].Header.VisiblePosition = 21;

                    //
                    bandUno.Columns["MED_CODIGO"].Header.Fixed = true;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Fixed = true;
                    //Oculto columnas 
                    bandUno.Columns["HOM_CODIGO"].Hidden = true;
                    bandUno.Columns["ATE_CODIGO"].Hidden = true;
                    bandUno.Columns["FOR_CODIGO"].Hidden = true;
                    bandUno.Columns["ID_USUARIO"].Hidden = true;
                    bandUno.Columns["HOM_FECHA_INGRESO"].Hidden = true;
                    bandUno.Columns["TMO_NOMBRE"].Hidden = true;
                    bandUno.Columns["TMO_CODIGO"].Hidden = true;
                    bandUno.Columns["MED_RUC"].Hidden = true;
                    //Oculto columnas que podran añadirse desde la aplicacion
                    bandUno.Columns["MED_CODIGO"].Hidden = true;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Hidden = true;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Hidden = true;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Hidden = true;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Hidden = true;
                    bandUno.Columns["ATE_FACTURA_FECHA"].Hidden = true;
                    bandUno.Columns["HOM_LOTE"].Hidden = true;
                    bandUno.Columns["RET_CODIGO1"].Hidden = true;
                    bandUno.Columns["HOM_COMISION_CLINICA"].Hidden = true;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Hidden = true;
                    //bandUno.Columns["HOM_RETENCION"].Hidden = true;
                    //bandUno.Columns["HOM_VALOR_TOTAL"].Hidden = true;
                    //bandUno.Columns["VALOR_POR_RECUPERAR"].Hidden = true;
                    //bandUno.Columns["HOM_RECORTE"].Hidden = true;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Hidden = true;
                    bandUno.Columns["HOM_OBSERVACION"].Hidden = true;

                    //excluyó columnas no visibles de la seleccion
                    bandUno.Columns["HOM_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["ATE_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["FOR_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["ID_USUARIO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["HOM_FECHA_INGRESO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["TMO_NOMBRE"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["TMO_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["MED_RUC"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("error", err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


    }
}
