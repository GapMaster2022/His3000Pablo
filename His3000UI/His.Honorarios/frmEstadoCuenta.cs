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
using Recursos;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using System.IO;
using System.Windows.Forms.Integration;
using Infragistics.Win.UltraWinTabControl;
using His.Entidades.General;
using System.IO;
using System.Diagnostics;

namespace His.Honorarios
{
    public partial class frmEstadoCuenta : Form
    {
        public MEDICOS medico;
        private string fechaAltaIni;
        private string fechaAltaFin;
        public frmEstadoCuenta()
        {
            InitializeComponent();
            dtpFiltroHasta.Value = DateTime.Now.Date;
           
            toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            toolStripButtonExportar.Image = Archivo.imgOfficeExcel;          
            toolStripButtonSalir.Image = Archivo.imgBtnSalir32;
        
        }

        private void frmEstadoCuenta_Load(object sender, EventArgs e)
        {

        }

        public void cargarGrid()
        {
            fechaAltaIni = String.Format("{0:dd-MM-yyyy}", dtpFiltroDesde.Value) + " 00:00:01";
            fechaAltaFin = String.Format("{0:dd-MM-yyyy}", dtpFiltroHasta.Value) + " 23:59:59";

            DataTable datos = NegHonorariosMedicos.EstadoCuenta(fechaAltaIni,
                fechaAltaFin,Convert.ToInt16(txtCodMedico.Text.Trim()));
                            ugrdCuenta.DataSource = datos;
                            if (datos.Rows.Count == 0)
                            {
                                ultraTabControl1.Enabled = false;
                                toolStripButtonActualizar.Enabled = false;
                                toolStripButtonExportar.Enabled = false;
                            }
                            else
                            {
                                ultraTabControl1.Enabled = true;
                                toolStripButtonActualizar.Enabled = true;
                                toolStripButtonExportar.Enabled = true;

                            }
        }

        private void ayudaMedicos_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Ayudas lista = new frm_Ayudas(NegMedicos.listaMedicos());
                lista.tabla = "MEDICOS";
                lista.campoPadre = txtCodMedico;
                lista.ShowDialog();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtCodMedico_TextChanged(object sender, EventArgs e)
        {

            if (Microsoft.VisualBasic.Information.IsNumeric(txtCodMedico.Text) == false)
                txtCodMedico.Text = string.Empty;

            if (txtCodMedico.Text != string.Empty)
            {
                cargarMedico(Convert.ToInt32(txtCodMedico.Text.ToString()));
            }
            else
            {
               
            }
        }
        public void cargarMedico(int codigo)
        {
            medico = new MEDICOS();
            medico = NegMedicos.RecuperaMedicoId(codigo);

            if (medico != null)
            {
                txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                cargarGrid(); //txtTelfMedico.Text = medico.MED_TELEFONO_CASA;
                //txtAutSri.Text = medico.MED_AUTORIZACION_SRI;
                //txtFecCaducidad.Text = medico.MED_VALIDEZ_AUTORIZACION;

            }
            else
            {

                txtNombreMedico.Text = string.Empty;
                //txtTelfMedico.Text = string.Empty;
                //txtAutSri.Text = string.Empty;
                //txtFecCaducidad.Text = string.Empty;
                //lblPorcentajeRetencion.Text = string.Empty;
            }
          
        }

        private void ugrdCuenta_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];




            ugrdCuenta.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            ugrdCuenta.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ugrdCuenta.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ugrdCuenta.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            //Caracteristicas de Filtro en la grilla
            ugrdCuenta.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ugrdCuenta.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ugrdCuenta.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ugrdCuenta.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            ugrdCuenta.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ugrdCuenta.DisplayLayout.UseFixedHeaders = true;
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

           

            SummarySettings sumValorPorRecuperar = bandUno.Summaries.Add("Valor Por Recuperar", SummaryType.Sum, bandUno.Columns["VALOR_POR_RECUPERAR"]);
            sumValorPorRecuperar.DisplayFormat = "{0:#####.00}";
            sumValorPorRecuperar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorCancelado = bandUno.Summaries.Add("Valor Cancelado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_CANCELADO"]);
            sumValorCancelado.DisplayFormat = "{0:#####.00}";
            sumValorCancelado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorSALDO = bandUno.Summaries.Add("SALDO", SummaryType.Sum, bandUno.Columns["VALOR_RECUPERADO"]);
            sumValorSALDO.DisplayFormat = "{0:#####.00}";
            sumValorSALDO.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorPORPAGAR = bandUno.Summaries.Add("SALDO POR PAGAR", SummaryType.Sum, bandUno.Columns["DIFERENCIA"]);
            sumValorPORPAGAR.DisplayFormat = "{0:#####.00}";
            sumValorPORPAGAR.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;


            SummarySettings sumSALDOS = bandUno.Summaries.Add("SALDOS", SummaryType.Sum, bandUno.Columns["SALDO"]);
            sumSALDOS.DisplayFormat = "{0:#####.00}";
            sumSALDOS.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumTOTAL = bandUno.Summaries.Add("TOTAL", SummaryType.Sum, bandUno.Columns["TOTAL"]);
            sumTOTAL.DisplayFormat = "{0:#####.00}";
            sumTOTAL.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumABONOSL = bandUno.Summaries.Add("ABONOS", SummaryType.Sum, bandUno.Columns["ABONOS"]);
            sumABONOSL.DisplayFormat = "{0:#####.00}";
            sumABONOSL.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;


            //Cambio el nombre de las cabeceras
            //bandUno.Columns["MED_CODIGO"].Header.Caption = "CODIGO";
            //bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Caption = "MEDICO";
            bandUno.Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA";
            bandUno.Columns["HOM_FACTURA_FECHA"].Header.Caption = "FECHA";
            bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.Caption = "PACIENTE";
            //bandUno.Columns["ATE_NUMERO_CONTROL"].Header.Caption = "NUMERO DE CONTROL";
            //bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.Caption = "FACTURA PACIENTE";
            //bandUno.Columns["ATE_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
            bandUno.Columns["FOR_DESCRIPCION"].Header.Caption = "FORMA DE PAGO";
            //bandUno.Columns["HOM_LOTE"].Header.Caption = "LOTE";
            //bandUno.Columns["RET_CODIGO1"].Header.Caption = "NUM. RETENCION";
            bandUno.Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
            bandUno.Columns["HOM_COMISION_CLINICA"].Header.Caption = "COMISION";
            bandUno.Columns["HOM_APORTE_LLAMADA"].Header.Caption = "REFERIDO";
            bandUno.Columns["HOM_RETENCION"].Header.Caption = "RETENCION";
            bandUno.Columns["HOM_VALOR_TOTAL"].Header.Caption = "VALOR A PAGAR";       
            bandUno.Columns["VALOR_POR_RECUPERAR"].Header.Caption = "SALDO";
            bandUno.Columns["HOM_VALOR_CANCELADO"].Header.Caption = "VALOR CANCELADO";
            //bandUno.Columns["HOM_OBSERVACION"].Header.Caption = "OBSERVACION";
            bandUno.Columns["VALOR_RECUPERADO"].Header.Caption = "SALDO";
            bandUno.Columns["DIFERENCIA"].Header.Caption = "DIFERENCIA";
            //bandUno.Columns["HOM_BONO"].Header.Caption = "BONO";

            //modifico el ancho por defecto de las columnas
          
         
            bandUno.Columns["HOM_FACTURA_MEDICO"].Width = 100;
            bandUno.Columns["HOM_FACTURA_FECHA"].Width = 70;
            bandUno.Columns["PAC_NOMBRE_PACIENTE"].Width = 200;     
            bandUno.Columns["FOR_DESCRIPCION"].Width = 120;      
            bandUno.Columns["HOM_VALOR_NETO"].Width = 80;
            bandUno.Columns["HOM_COMISION_CLINICA"].Width = 80;
            bandUno.Columns["HOM_APORTE_LLAMADA"].Width = 60;
            bandUno.Columns["HOM_RETENCION"].Width = 60;
            bandUno.Columns["HOM_VALOR_TOTAL"].Width = 60;
            bandUno.Columns["VALOR_POR_RECUPERAR"].Width = 60;
            bandUno.Columns["HOM_VALOR_CANCELADO"].Width = 60;
            bandUno.Columns["VALOR_RECUPERADO"].Width = 60;
            bandUno.Columns["DIFERENCIA"].Width = 60;

            //alineo las columnas
            bandUno.Columns["HOM_FACTURA_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Columns["PAC_NOMBRE_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Columns["FOR_DESCRIPCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
             bandUno.Columns["HOM_VALOR_NETO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_COMISION_CLINICA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_APORTE_LLAMADA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_RETENCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_VALOR_TOTAL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
           bandUno.Columns["VALOR_POR_RECUPERAR"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_VALOR_CANCELADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["VALOR_RECUPERADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["DIFERENCIA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            //ordeno las columnas
           
           
            bandUno.Columns["HOM_FACTURA_MEDICO"].Header.VisiblePosition = 2;
            bandUno.Columns["HOM_FACTURA_FECHA"].Header.VisiblePosition = 3;
            bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.VisiblePosition = 1;        
            bandUno.Columns["FOR_DESCRIPCION"].Header.VisiblePosition = 4;
            bandUno.Columns["HOM_VALOR_NETO"].Header.VisiblePosition = 5;
            bandUno.Columns["HOM_COMISION_CLINICA"].Header.VisiblePosition = 6;
            bandUno.Columns["HOM_APORTE_LLAMADA"].Header.VisiblePosition = 7;
            bandUno.Columns["HOM_RETENCION"].Header.VisiblePosition = 8;
            bandUno.Columns["HOM_VALOR_TOTAL"].Header.VisiblePosition = 9;
            bandUno.Columns["VALOR_RECUPERADO"].Header.VisiblePosition = 10;
            bandUno.Columns["VALOR_POR_RECUPERAR"].Header.VisiblePosition = 18;    
            bandUno.Columns["HOM_VALOR_CANCELADO"].Header.VisiblePosition = 11;  
            bandUno.Columns["DIFERENCIA"].Header.VisiblePosition = 12;

            //Cambio el color de las columnas
            string[] infMedico = new string[2] { "HOM_FACTURA_MEDICO", "HOM_FACTURA_FECHA" };
            string[] infPaciente = new string[2] { "PAC_NOMBRE_PACIENTE",  "FOR_DESCRIPCION",  };
            string[] infHonorarios = new string[9] {  "HOM_VALOR_NETO", "HOM_COMISION_CLINICA", "HOM_APORTE_LLAMADA", "HOM_RETENCION", "HOM_VALOR_TOTAL",  "VALOR_POR_RECUPERAR", "SALDO", "VALOR_RECUPERADO","TOTAL" };

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

            //
        
            //Oculto columnas 
            bandUno.Columns["VALOR_POR_RECUPERAR"].Hidden = true;
            bandUno.Columns["HOM_VALOR_CANCELADO"].Hidden = true;

           bandUno.Columns["DIFERENCIA"].Hidden = true;

           bandUno.Columns["HOM_VALOR_TOTAL"].Hidden = true;
           bandUno.Columns["generado"].Hidden = true;
         
          bandUno.Columns["VALOR_RECUPERADO"].Hidden = true;
           //bandUno.Columns["HOM_OBSERVACION"].Header.Caption = "OBSERVACION";
        
     

           
          

            
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            cargarGrid();
        }

        private void toolStripButtonExportar_Click(object sender, EventArgs e)
        {
            try
            {
                CreateExcel(FindSavePath());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void CreateExcel(String myFilepath)
        {
            try
            {
                if (myFilepath != null)
                {

                    this.ultraGridExcelExporter1.Export(ugrdCuenta, myFilepath);

                    MessageBox.Show("Se termino de exportar el grid en el archivo " + myFilepath);
                }
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "EXCEL.EXE";
                startInfo.Arguments = myFilepath;
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ugrdCuenta_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {

                if (Convert.ToString(e.Row.Cells[16].Value) =="0.00")
                {

                    string cadenaa = e.Row.Cells[16].Value.ToString();
                    e.Row.Cells[16].Hidden = true;
                    e.Row.Appearance.BackColor = Color.Red;
                    e.Row.Hidden = true;
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
