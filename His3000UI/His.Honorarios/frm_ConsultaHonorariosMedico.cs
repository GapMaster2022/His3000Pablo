using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using Infragistics.Win.UltraWinGrid;
using System.IO;

namespace His.Honorarios
{
    public partial class frm_ConsultaHonorariosMedico : Form
    {
        #region Declaracion de variables

        List<TIPO_FORMA_PAGO> listaTipoPagos = new List<TIPO_FORMA_PAGO>();
        List<FORMA_PAGO> listaPagos = new List<FORMA_PAGO>();
        FORMA_PAGO pago = null;
        TIPO_FORMA_PAGO tipoPago = null;
        MEDICOS medico = null;
        private bool iniFacturasIngresadas = false;
        Datos.Atencion obj_atencion = new His.Honorarios.Datos.Atencion();

        #endregion

        public frm_ConsultaHonorariosMedico()
        {
            InitializeComponent();
            RecuperarDatos();
            CargarDatos();
            //if (medico == null)
            //{
            //    CargarHonorariosMedicos(null, null, null, null, null, null, null, null, null, null);
            //    txtCodMedico.Enabled = true;
            //}
            if (medico != null)
            {
                validarFiltros();
                txtCodMedico.Enabled = false;
            }
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            dtpFactMedDesde.Value = oPrimerDiaDelMes;
        }
        public void RecuperarDatos()
        {
            try
            {
                cbTipoPago.DataSource = NegHonorariosMedicos.HonoFormasPago();
                cbTipoPago.DisplayMember = "desclas";
                cbTipoPago.ValueMember = "codclas";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar las formas de pago. Mas detalles: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //cbTipoPago.DataSource = dts_combos;
            //cbTipoPago.DisplayMember = dts_combos.Tables[0].Columns["desclas"].ColumnName.ToString();
            //cbTipoPago.ValueMember = dts_combos.Tables[0].Columns["codclas"].ColumnName.ToString();

            //listaTipoPagos = NegFormaPago.RecuperaTipoFormaPagos();
            //listaPagos = NegFormaPago.listaFormasPago();
            //CargarHonorariosMedicos(null, null, null, null, null, null, null, null, null, null, null);
            //CargarHonorariosMedicos();
        }
        public void CargarDatos()
        {
            //TIPO_FORMA_PAGO nuevo = new TIPO_FORMA_PAGO();
            //nuevo.TIF_CODIGO=0;
            //nuevo.TIF_NOMBRE="TODAS";
            //listaTipoPagos.Insert(0, nuevo);
            //cbTipoPago.DataSource = listaTipoPagos;
            //cbTipoPago.ValueMember = "TIF_CODIGO";
            //cbTipoPago.DisplayMember = "TIF_NOMBRE";
        }

        public void CargarMedico(string codMedico)
        {
            if (codMedico != string.Empty)
            {
                DataTable med = NegMedicos.MedicoIDValida(Convert.ToInt16(codMedico));
                if (med.Rows[0][0].ToString() == "7")
                {
                    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    return;
                }
                medico = NegMedicos.MedicoID(Convert.ToInt32(codMedico));                
            }
            else
                medico = null;

            if (medico != null)
            {
                //dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Hidden = true;
                //dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["MED_NOMBRE_MEDICO"].Hidden = true;
                txtNomMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;     
            }
            else
            {
                dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Hidden = false;
                dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["MED_NOMBRE_MEDICO"].Hidden = false;
                txtNomMedico.Text = string.Empty;
            }

            validarFiltros();
        }

        private void cbTipoPago_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbTipoPago.Text != "" && cbTipoPago.Text != "System.Data.DataRowView")
            {
                cbFormaPago.DataSource = NegHonorariosMedicos.Difiere(cbTipoPago.SelectedValue.ToString());
                cbFormaPago.DisplayMember = "despag";
                cbFormaPago.ValueMember = "forpag";
            }
            //TIPO_FORMA_PAGO tfp = (TIPO_FORMA_PAGO)cbTipoPago.SelectedItem;

            //if (tfp.TIF_CODIGO != 0)
            //{
            //    tipoPago = (TIPO_FORMA_PAGO)cbTipoPago.SelectedItem;
            //    listaPagos = NegFormaPago.RecuperaFormaPago(tfp.TIF_CODIGO);

            //    FORMA_PAGO nuevo = new FORMA_PAGO();
            //    nuevo.FOR_CODIGO = 0;
            //    nuevo.FOR_DESCRIPCION = "TODAS";
            //    listaPagos.Insert(0, nuevo);

            //    cbFormaPago.DataSource = listaPagos;
            //    cbFormaPago.ValueMember = "FOR_CODIGO";
            //    cbFormaPago.DisplayMember = "FOR_DESCRIPCION";
            //}
            //else
            //{
            //    tipoPago=null;
            //    cbFormaPago.DataSource = null;
            //}
            //validarFiltros();
        }

        public void CargarHonorariosMedicos(
            string medCodigo, 
            string fecFacMedDesde, 
            string fecFacMedHasta, 
            string forCodigo, 
            string tifCodigo,
            string lote,
            string numControl)
        {
            dbugrFacturasIngresadas.DataSource = NegHonorariosMedicos.RecuperarHonorariosMedicos(
                null,
                null,
                medCodigo,
                null,
                null,
                null,
                null,
                fecFacMedDesde,
                fecFacMedHasta,
                null,
                null,
                forCodigo,
                tifCodigo,
                lote,
                numControl,
                null,
                null,
                null,
                null,
                null,
                null,
                null);
               
        }

        public void NuevaVistaHonorarios(int med_codigo, DateTime fechaInicio, DateTime fechaFin, int for_codigo, 
            int tif_codigo, string lote, string numControl)
        {
            try
            {

                dbugrFacturasIngresadas.DataSource = NegMedicos.VistaHonorarioMedico(med_codigo, fechaInicio, fechaFin, for_codigo,
                    tif_codigo, lote, numControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void validarFiltros()
        {
            string medCodigo = null;
            string fecFacMedDesde = null; 
            string fecFacMedHasta = null;
            string forCodigo = null;
            string tifCodigo = null;
            string lote = null;
            string numControl = null;

            if (medico != null)
                medCodigo = medico.MED_CODIGO.ToString();
            else
                medCodigo = "0";

            fecFacMedDesde = String.Format("{0:yyyy/MM/dd}", dtpFactMedDesde.Value);
            fecFacMedHasta = String.Format("{0:yyyy/MM/dd}", dtpFactMedHasta.Value) + " 23:59:59";


            if(cbFormaPago.Text != "" && cbFormaPago.Text != "System.Data.DataRowView")
                forCodigo = cbFormaPago.SelectedValue.ToString();
            else
                forCodigo = "0";

            if (tipoPago != null)
                tifCodigo = tipoPago.TIF_CODIGO.ToString();

            //CargarHonorariosMedicos
            //    (medCodigo,
            //    fecFacMedDesde,
            //    fecFacMedHasta,
            //    forCodigo,
            //    tifCodigo,
            //    lote,
            //    numControl);

            NuevaVistaHonorarios(Convert.ToInt32(medCodigo), Convert.ToDateTime(fecFacMedDesde), Convert.ToDateTime(fecFacMedHasta), 
                Convert.ToInt32(forCodigo), Convert.ToInt32(tifCodigo), lote, numControl);


        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                if (iniFacturasIngresadas == false)
                {
                    UltraGridBand bandUno = dbugrFacturasIngresadas.DisplayLayout.Bands[0];

                    //Opciones por defecto de la grilla

                    //dbugrFacturasIngresadas.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                    dbugrFacturasIngresadas.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                    dbugrFacturasIngresadas.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                    dbugrFacturasIngresadas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                    bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    //Caracteristicas de Filtro en la grilla
                    dbugrFacturasIngresadas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                    dbugrFacturasIngresadas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                    dbugrFacturasIngresadas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                    dbugrFacturasIngresadas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                    //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro"; 
                    dbugrFacturasIngresadas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                    //
                    dbugrFacturasIngresadas.DisplayLayout.UseFixedHeaders = true;
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
                    SummarySettings sumReferido = bandUno.Summaries.Add("Aporte", SummaryType.Sum, bandUno.Columns["HOM_APORTE_LLAMADA"]);
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

                    //Añado la columna check
                    bandUno.Columns.Add("columnaCheck", "");
                    bandUno.Columns["columnaCheck"].DataType = typeof(bool);
                    bandUno.Columns["columnaCheck"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                    bandUno.Columns["columnaCheck"].Hidden = true;

                    //Cambio el nombre de las cabeceras
                    bandUno.Columns["MED_CODIGO"].Header.Caption = "CODIGO";
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Caption = "MEDICO";
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA DEL MEDICO";
                    bandUno.Columns["HOM_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA MEDICO";
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.Caption = "PACIENTE";
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Header.Caption = "NUMERO DE CONTROL";
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.Caption = "FACTURA PACIENTE";
                    bandUno.Columns["ATE_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA PACIENTE";
                    bandUno.Columns["despag"].Header.Caption = "FORMA DE PAGO";
                    bandUno.Columns["FOR_DESCRIPCION"].Header.Caption = "CORRIENTE / DIFERIDO";
                    bandUno.Columns["HOM_LOTE"].Header.Caption = "LOTE";
                    bandUno.Columns["RET_CODIGO1"].Header.Caption = "NUM. RETENCION";
                    bandUno.Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
                    bandUno.Columns["HOM_COMISION_CLINICA"].Header.Caption = "COMISION CLINICA";
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Header.Caption = "APORTE";
                    bandUno.Columns["HOM_RETENCION"].Header.Caption = "VALOR RETENCION";
                    bandUno.Columns["HOM_VALOR_TOTAL"].Header.Caption = "VALOR A PAGAR";
                    bandUno.Columns["HOM_VALOR_PAGADO"].Header.Caption = "VALOR RECUPERADO";
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Header.Caption = "VALOR POR RECUPERAR";
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Header.Caption = "VALOR CANCELADO";
                    bandUno.Columns["TMO_NOMBRE"].Header.Caption = "MOVIMIENTO";
                    bandUno.Columns["HON_FUERA"].Header.Caption = "HON. POR FUERA";
                    bandUno.Columns["TMO_CODIGO"].Hidden = true;
                    bandUno.Columns["HOM_RECORTE"].Hidden = true;
                    bandUno.Columns["despag"].Hidden = false;
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].Header.Caption = "OBSERVACION";

                    //Cambio el a no editables a las columnas
                    bandUno.Columns["MED_CODIGO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_FACTURA_FECHA"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["ATE_FACTURA_FECHA"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["FOR_DESCRIPCION"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_LOTE"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["RET_CODIGO1"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_VALOR_NETO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_COMISION_CLINICA"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_RETENCION"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_VALOR_TOTAL"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_VALOR_PAGADO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].CellActivation = Activation.NoEdit;
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].CellActivation = Activation.NoEdit;

                    //modifico el ancho por defecto de las columnas
                    bandUno.Columns["columnaCheck"].Width = 40;
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
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Width = 100;
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].Width = 250;

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
                    bandUno.Columns["HOM_VALOR_CANCELADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    //ordeno las columnas
                    bandUno.Columns["columnaCheck"].Header.VisiblePosition = 0;
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
                    bandUno.Columns["HOM_VALOR_PAGADO"].Header.VisiblePosition = 18;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Header.VisiblePosition = 19;
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].Header.VisiblePosition = 20;

                    //
                    bandUno.Columns["columnaCheck"].Header.Fixed = true;
                    bandUno.Columns["MED_CODIGO"].Header.Fixed = true;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Fixed = true;
                    //Oculto columnas 
                    bandUno.Columns["HOM_CODIGO"].Hidden = true;
                    bandUno.Columns["ATE_CODIGO"].Hidden = true;
                    bandUno.Columns["FOR_CODIGO"].Hidden = true;
                    bandUno.Columns["ID_USUARIO"].Hidden = true;
                    bandUno.Columns["despag"].Hidden = false;
                    ////bandUno.Columns["HOM_VALOR_CANCELADO"].Hidden = true;
                    bandUno.Columns["HOM_FECHA_INGRESO"].Hidden = true;
                    //bandUno.Columns["VALOR_POR_RECUPERAR"].Hidden = true;
                    bandUno.Columns["MED_RUC"].Hidden = true;

                    //Cambio el color de las columnas
                    string[] infMedico = new string[4] { "MED_CODIGO", "MED_NOMBRE_MEDICO", "HOM_FACTURA_MEDICO", "HOM_FACTURA_FECHA" };
                    string[] infPaciente = new string[7] { "PAC_NOMBRE_PACIENTE", "ATE_NUMERO_CONTROL", "ATE_FACTURA_PACIENTE", "ATE_FACTURA_FECHA", "despag", "FOR_DESCRIPCION", "HOM_LOTE" };
                    string[] infHonorarios = new string[10] { "RET_CODIGO1", "HOM_VALOR_NETO", "HOM_COMISION_CLINICA", "HOM_APORTE_LLAMADA", "HOM_RETENCION", "HOM_VALOR_TOTAL", "HOM_VALOR_PAGADO", "VALOR_POR_RECUPERAR", "HOM_VALOR_CANCELADO", "HOM_OBSERVACION" };

                    foreach (string item in infMedico)
                    {
                        //bandUno.Columns["MED_NOMBRE_MEDICO"].CellAppearance.AlphaLevel = 125;
                        bandUno.Columns[item].CellAppearance.BackColor2 = Color.White;
                        bandUno.Columns[item].CellAppearance.BackColor = Color.Silver;
                        //bandUno.Columns[item].CellAppearance.BackColor = Color.DarkGray;
                        bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
                    }
                    foreach (string item in infPaciente)
                    {
                        //bandUno.Columns[item].CellAppearance.BackColor2 = Color.LightCyan;
                        bandUno.Columns[item].CellAppearance.BackColor = Color.LightCyan;
                        //bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
                    }
                    foreach (string item in infHonorarios)
                    {
                        bandUno.Columns[item].CellAppearance.BackColor = Color.LightSteelBlue;
                        //bandUno.Columns[item].CellAppearance.BackColor = Color.SlateGray;
                        //bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
                    }
                    //excluyó columnas no visibles de la seleccion
                    bandUno.Columns["HOM_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["ATE_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["FOR_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["ID_USUARIO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    ////bandUno.Columns["HOM_VALOR_CANCELADO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["HOM_FECHA_INGRESO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    //bandUno.Columns["VALOR_POR_RECUPERAR"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["MED_RUC"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;

                    //bandUno.Columns["MED_CODIGO"].Hidden = true;
                    //bandUno.Columns["MED_NOMBRE_MEDICO"].Hidden = true;
                    iniFacturasIngresadas = true;

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
              CargarMedico(txtCodMedico.Text.Trim().ToString());
        }

        private void txtCodMedico_KeyDown(object sender, KeyEventArgs e)
        {
                try
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        frm_Ayudas lista = new frm_Ayudas(NegMedicos.listaMedicos());
                        lista.tabla = "MEDICOS";
                        //lista.bandCampo = true;
                        lista.campoEspecial = txtCodMedico;
                        lista.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        MessageBox.Show(ex.InnerException.Message);
                }
        }

        private void cbFormaPago_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpFactPacDesde_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpFactPacHasta_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpFactMedDesde_ValueChanged(object sender, EventArgs e)
        {
            //validarFiltros();
        }

        private void dtpFactMedHasta_ValueChanged(object sender, EventArgs e)
        {
            //validarFiltros();
        }

        private void ayudaMedicos_Click(object sender, EventArgs e)
        {
            try
            {
                    frm_Ayudas lista = new frm_Ayudas(NegMedicos.listaMedicos());
                    lista.tabla = "MEDICOS";
                    //lista.bandCampo = true;
                    lista.campoEspecial = txtCodMedico;
                    lista.ShowDialog();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void dbugrFacturasIngresadas_Click(object sender, EventArgs e)
        {
            
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
        private void btnExcel_Click(object sender, EventArgs e)
        {
            
        }

        private void dbugrFacturasIngresadas_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            string facturaMedico = ""; //Cambios Edgar 20210312 variable que recibira el nombre del rubro de honorarios para mostrar los medicos, no afectara el funcionamiento anterior.
            if (dbugrFacturasIngresadas.Rows.Count > 0)
            {
                UltraGridRow fila = dbugrFacturasIngresadas.ActiveRow;
                try
                {
                    Int32 codMedico = Convert.ToInt32(dbugrFacturasIngresadas.Rows[dbugrFacturasIngresadas.ActiveRow.Index].Cells["MED_CODIGO"].Value.ToString());
                    facturaMedico = fila.Cells["HOM_FACTURA_MEDICO"].Value.ToString();
                    frmAsientoContable Form = new frmAsientoContable(facturaMedico, codMedico);
                    Form.ShowDialog();
                }
                catch
                {

                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dtpFactMedDesde.Value.Date <= dtpFactMedHasta.Value)
                validarFiltros();
            else
                MessageBox.Show("Fecha \"Desde\" no puede ser mayor a fecha \"Hasta\".", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtCodMedico.Text = "";
            txtNomMedico.Text = "";
            dbugrFacturasIngresadas.DataSource = null;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dbugrFacturasIngresadas.Rows.Count > 0)
            {
                try
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        if (dbugrFacturasIngresadas.CanFocus == true)
                            this.ultraGridExcelExporter1.Export(dbugrFacturasIngresadas, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                finally
                { this.Cursor = Cursors.Default; }
            }
            else
                MessageBox.Show("No tiene datos ha exportar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void frm_ConsultaHonorariosMedico_Load(object sender, EventArgs e)
        {

        }
    }
}
