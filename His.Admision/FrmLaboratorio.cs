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

namespace His.Admision
{
    public partial class FrmLaboratorio : Form
    {
        #region
        private string fechaCreacionIni;
        private string fechaCreacionFin;
        private PACIENTES pacienteActual = null;
        private ATENCIONES ultimaAtencion = null;
        public int codigoAtencion = 0;
        private CUENTAS_PACIENTES cuentaPacientes = null;
        #endregion 

        public FrmLaboratorio()
        {
            InitializeComponent();
        }
        
        #region Métodos

        public void cargardatos()
        {
            try
            {
                //toolStripActualizar.Image = Archivo.imgBtnRestart;
                //toolStripexportar.Image = Archivo.imgOfficeExcel;
                //toolStripimprimir.Image = Archivo.imgBtnImprimir32;
                //toolStripsalir.Image = Archivo.imgBtnSalir32;
                toolStripCuenta.Image = Archivo.imgBtnAdd;
                //
                dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", "01/" + DateTime.Now.Month + "/" + (DateTime.Now.Year).ToString()));
                dtpFiltroHasta.Value = DateTime.Now;

                ultraGridPacientes.DataSource = NegLaboratorio.RecuperarPacientes(null, null);
            }
            catch (Exception ex)
            {

            }
        }

        public void CargarPaciente(int codigoAtencion)
        {
            try
            {
                limpiarCamposPaciente();
                if (codigoAtencion > 0)
                    pacienteActual = NegPacientes.recuperarPacientePorAtencion(codigoAtencion);
                else
                    pacienteActual = null;

                if (pacienteActual != null)
                {
                    //
                    txtPaciente.ReadOnly = false;
                    txtPacienteHCL.Text = pacienteActual.PAC_HISTORIA_CLINICA;
                    txtNombrePPaciente.Text = pacienteActual.PAC_NOMBRE1;
                    txtNombreSPaciente.Text = pacienteActual.PAC_NOMBRE2;
                    txtApellidoPPaciente.Text = pacienteActual.PAC_APELLIDO_PATERNO;
                    txtApellidoSPaciente.Text = pacienteActual.PAC_APELLIDO_MATERNO;
                    ultimaAtencion = NegAtenciones.RecuperarAtencionID(codigoAtencion);
                    dtpFechaIngreso.Value = ultimaAtencion.ATE_FECHA_INGRESO.Value;
                }
                else
                {
                    txtNombrePPaciente.Text = pacienteActual.PAC_NOMBRE1;
                    txtNombreSPaciente.Text = pacienteActual.PAC_NOMBRE2;
                    txtApellidoPPaciente.Text = pacienteActual.PAC_APELLIDO_PATERNO;
                    txtApellidoSPaciente.Text = pacienteActual.PAC_APELLIDO_MATERNO;
                    dtpFechaIngreso.Value = DateTime.Now;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void limpiarCamposPaciente()
        {
           
            //txtAtencion.Text = string.Empty;
            txtNombrePPaciente.Text = string.Empty;
            txtNombreSPaciente.Text = string.Empty;
            txtApellidoPPaciente.Text = string.Empty;
            txtApellidoSPaciente.Text = string.Empty;
            dtpFechaIngreso.Value = DateTime.Now;
            //txtAtencion.Text = string.Empty;
            txtPacienteHCL.Text = string.Empty;
            //txtPaciente.Text = string.Empty;
        }
        
        #endregion


        private void toolStripexportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ultraGridPacientes.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ultraGridPacientes, PathExcel);
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

        private void toolStripsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripActualizar_Click(object sender, EventArgs e)
        {
            //ultraGridPacientes.DataSource = NegLaboratorio.RecuperarPacientes(dtpFiltroDesde.Value, dtpFiltroHasta.Value);
            ultraGridPacientes.DataSource = NegLaboratorio.RecuperarPacientes(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString());

        }

        private void ultraGridPacientes_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridPacientes.DisplayLayout.Bands[0];

            ultraGridPacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            ultraGridPacientes.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridPacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            //Caracteristicas de Filtro en la grilla
            ultraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            ultraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGridPacientes.DisplayLayout.UseFixedHeaders = true;
            bandUno.Summaries.Clear();
            bandUno.SummaryFooterCaption = "Totales: ";
            bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
            bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.LightYellow;
            //añado las sumatorias a las columnas
            SummarySettings sumTarifa = bandUno.Summaries.Add("TARIFA", SummaryType.Sum, bandUno.Columns["TARIFA"]);
            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
            sumTarifa.DisplayFormat = "{0:#####.00}";
            sumTarifa.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
            //
            SummarySettings sumTarifaIess = bandUno.Summaries.Add("TAR_IESS", SummaryType.Sum, bandUno.Columns["TAR_IESS"]);
            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
            sumTarifaIess.DisplayFormat = "{0:#####.00}";
            sumTarifaIess.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
            //
            SummarySettings sumTotal = bandUno.Summaries.Add("TOTAL", SummaryType.Sum, bandUno.Columns["TOTAL"]);
            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
            sumTotal.DisplayFormat = "{0:#####.00}";
            sumTotal.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
            SummarySettings diftarifa = bandUno.Summaries.Add("TAR_DIFERENCIA", SummaryType.Sum, bandUno.Columns["TAR_DIFERENCIA"]);
            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
            diftarifa.DisplayFormat = "{0:#####.00}";
            diftarifa.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;


            if (utcklaboratorio.Checked == true)
            {
                bandUno.Columns["HISTORIA_CLINICA"].Hidden = true;
                bandUno.Columns["APELLIDO"].Hidden = true;
                bandUno.Columns["NOMBRE"].Hidden = true;
                bandUno.Columns["NO_ORDEN"].Hidden = true;
                bandUno.Columns["AÑO_ORDEN"].Hidden = true;
                bandUno.Columns["COD_EXAMEN"].Hidden = true;
                bandUno.Columns["SOAT"].Hidden = true;
                bandUno.Columns["COD_TARIFA"].Hidden = true;
                bandUno.Columns["COD_IESS"].Hidden = true;
                bandUno.Columns["TARIFA"].Hidden = true;
                bandUno.Columns["TAR_DIFERENCIA"].Hidden = true;
                bandUno.Columns["NOM_TARIFA"].Hidden = true;
                ugbAtencion.Enabled = true;
            }
            else
            {
                bandUno.Columns["HISTORIA_CLINICA"].Hidden = false ;
                bandUno.Columns["APELLIDO"].Hidden = false;
                bandUno.Columns["NOMBRE"].Hidden = false;
                bandUno.Columns["NO_ORDEN"].Hidden = false;
                bandUno.Columns["AÑO_ORDEN"].Hidden = false;
                bandUno.Columns["COD_EXAMEN"].Hidden = false;
                bandUno.Columns["SOAT"].Hidden = false;
                bandUno.Columns["COD_TARIFA"].Hidden = false;
                bandUno.Columns["COD_IESS"].Hidden = false;
                bandUno.Columns["TAR_IESS"].Hidden = false;
                bandUno.Columns["TAR_DIFERENCIA"].Hidden = false;
                bandUno.Columns["CANTIDAD"].Hidden = false;
                bandUno.Columns["TARIFA"].Hidden = false;
                bandUno.Columns["TOTAL"].Hidden = false;
                bandUno.Columns["NOM_TARIFA"].Hidden = false;
                ugbAtencion.Enabled = false;
            }
            //ordeno las columnas
            bandUno.Columns["HISTORIA_CLINICA"].Header.VisiblePosition = 11;
            bandUno.Columns["APELLIDO"].Header.VisiblePosition = 9;
            bandUno.Columns["NOMBRE"].Header.VisiblePosition = 10;
            bandUno.Columns["NO_ORDEN"].Header.VisiblePosition = 17;
            bandUno.Columns["AÑO_ORDEN"].Header.VisiblePosition = 12;
            bandUno.Columns["FECHA"].Header.VisiblePosition = 0;
            bandUno.Columns["COD_EXAMEN"].Header.VisiblePosition = 16;
            bandUno.Columns["SOAT"].Header.VisiblePosition = 13;
            bandUno.Columns["IESS"].Header.VisiblePosition = 1;
            bandUno.Columns["NOM_EXA"].Header.VisiblePosition = 2;
            bandUno.Columns["COD_TARIFA"].Header.VisiblePosition = 14;
            bandUno.Columns["TARIFA"].Header.VisiblePosition = 3;
            bandUno.Columns["NOM_TARIFA"].Header.VisiblePosition = 4;
            bandUno.Columns["COD_IESS"].Header.VisiblePosition = 15;
            bandUno.Columns["TAR_IESS"].Header.VisiblePosition = 5;
            bandUno.Columns["TAR_DIFERENCIA"].Header.VisiblePosition = 6;
            bandUno.Columns["CANTIDAD"].Header.VisiblePosition = 7;
            bandUno.Columns["TOTAL"].Header.VisiblePosition = 8;
         
        }

        private void FrmLaboratorio_Load_1(object sender, EventArgs e)
        {
            fechaCreacionIni = String.Format("{0:yyyy-MM-dd }", dtpFiltroDesde.Value) + " 00:00:01";
            fechaCreacionFin = String.Format("{0:yyyy-MM-dd }", dtpFiltroHasta.Value) + " 23:59:59";
            cargardatos();
            ugbAtencion.Enabled = false;
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            try
            {
                frm_AyudaPacientesCuentas frm = new frm_AyudaPacientesCuentas();

                frm.campoPadre = txtPaciente;
                frm.campoAtencion = txtAtencion;
                frm.fechaingreso = dtpFechaIngreso;
                //HabilitarBotones(false, true, true, true);
                //if (txtAtencion.Text != "")
                //    CargarPaciente(Convert.ToInt32(txtAtencion.Text));
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void ayudaPacientes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    frm_AyudaPacientes form = new frm_AyudaPacientes();
                    
                    
                    form.campoPadre = txtPaciente;
                    form.ShowDialog();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
               
      
        private void txtPaciente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(txtPaciente.Tag) == true)
                {
                    
                    txtPaciente.Tag = false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void txtAtencion_TextChanged(object sender, EventArgs e)
        {
            if (txtAtencion.Text != "")
            {
                toolStripCuenta.Enabled = true;
                CargarPaciente(Convert.ToInt32(txtAtencion.Text.Trim()));
            }
        }

        private void utcklaboratorio_CheckedChanged(object sender, EventArgs e)
        {
            ultraGridPacientes.DataSource = NegLaboratorio.RecuperarPacientes(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.ToString());
        }

        private void toolStripCuenta_Click(object sender, EventArgs e)
        {
            if (utcklaboratorio.Checked == true)
            {
                try
                {
                    ultraGridPacientes.Selected.Rows.AddRange(this.ultraGridPacientes.Rows.GetFilteredInNonGroupByRows());
                    foreach (var cuenta in ultraGridPacientes.Selected.Rows)
                    {
                        //if(cuenta.IsActiveRow == true)
                        //{
                            cuentaPacientes = new CUENTAS_PACIENTES();
                            cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
                            cuentaPacientes.CUE_FECHA = Convert.ToDateTime(cuenta.Cells[1].Value);
                            cuentaPacientes.PRO_CODIGO_BARRAS = (cuenta.Cells[13].Value).ToString();
                            cuentaPacientes.CUE_DETALLE = (cuenta.Cells[9].Value).ToString();
                            cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(cuenta.Cells[14].Value);
                            cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cuenta.Cells[16].Value);
                            cuentaPacientes.CUE_VALOR = Convert.ToDecimal(cuenta.Cells[17].Value);
                            cuentaPacientes.CUE_ESTADO = 1;
                            cuentaPacientes.CUE_NUM_FAC = "0";
                            cuentaPacientes.PRO_CODIGO = "0";
                            cuentaPacientes.RUB_CODIGO = 6;
                            cuentaPacientes.CUE_IVA = 0;
                            cuentaPacientes.PED_CODIGO = His.Parametros.CuentasPacientes.CodigoLaboratorio;
                            cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            cuentaPacientes.CAT_CODIGO = 0;
                            cuentaPacientes.MED_CODIGO = 0;
                            cuentaPacientes.CUE_NUM_CONTROL = Convert.ToString(cuenta.Cells["NO_ORDEN"].Value);
                            NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                        //}
                    }
                    
                    MessageBox.Show("Datos Almacenados Correctamente");
                    txtAtencion.Text = string.Empty;
                    limpiarCamposPaciente();
                    toolStripCuenta.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo  cargar  archivo ");
                    toolStripCuenta.Enabled = false;
                }
            }
        }

        
        }
    }
