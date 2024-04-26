using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Entidades.Reportes;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using Infragistics.Win.UltraWinGrid;
using System.Data.SqlClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using His.General;
using System.IO;
using His.DatosReportes;

namespace CuentaPaciente
{
    public partial class frmDetalleCuenta : Form
    {
        #region Variables
        public bool pacienteNuevo = false;
        PACIENTES pacienteCuenta = new PACIENTES();
        ATENCIONES atencion = new ATENCIONES();
        //List<PEDIDOS> listaPedido = new List<PEDIDOS>();
        List<DtoDetalleCuentaPaciente> listaPedido = new List<DtoDetalleCuentaPaciente>();
        List<PEDIDOS_AREAS> listaPedidoAreas = new List<PEDIDOS_AREAS>();
        Infragistics.Win.UltraWinTree.UltraTree raiz = new Infragistics.Win.UltraWinTree.UltraTree();
        public string codigoAtencionA;
        private List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = null;

        DataTable dtCuentasModificadas = new DataTable(); // aumento para seleccioonar todos los items modificados de una cuenta / Giovanny Tapia / 24/08/2012

        #endregion

        public frmDetalleCuenta()
        {
            InitializeComponent();
            
        }

        public frmDetalleCuenta(int codigoAtencion)
        {
            InitializeComponent();
            cargarPaciente(codigoAtencion);
            btn_Imprimir.Image = Archivo.imgBtnImprimir32;
            btn_Excel.Image = Archivo.imgOfficeExcel;
            btn_Salir.Image = Archivo.imgBtnSalir32;
            
        }

        #region Métodos

        private void cargarDatosPaciente(PACIENTES pacienteActual, ATENCIONES atencion)
        {
            lbl_HistoriaC.Text = pacienteActual.PAC_HISTORIA_CLINICA.Trim();
            lbl_Paciente.Text = pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2 + " " + pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO;
            lbl_Fecha.Text = Convert.ToString(atencion.ATE_FECHA_INGRESO);
            lbl_NroAtencion.Text = Convert.ToString(atencion.ATE_CODIGO);
            lblNumeroAtencion.Text = Convert.ToString(atencion.ATE_NUMERO_ATENCION); // Para que tome el numero de la atencion y no el codigo , el codigo de la atencion esta oculto abajo del numero /30/01/2012 / GIOVANNY TAPIA
            lbl_NroSeguro.Text = CargarConvenio();
            lbl_MedicoP.Text = Convert.ToString(atencion.MEDICOS.MED_NOMBRE1 + " " + atencion.MEDICOS.MED_NOMBRE2 + " " + atencion.MEDICOS.MED_APELLIDO_PATERNO + " " + atencion.MEDICOS.MED_APELLIDO_MATERNO);
            lbl_EdadAños.Text = Funciones.CalcularEdad(Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO)).ToString() + " años";
            this.lblFechaAlta.Text = Convert.ToString(atencion.ATE_FECHA_ALTA);

            dtCuentasModificadas = NegDetalleCuenta.CargaItemsModificados(atencion.ATE_CODIGO);
        }

        public void cargarPaciente(int codigoAtencion)
        {
            try
            {
                DataTable dtListaItemsEliminados = new DataTable();

                atencion = NegAtenciones.RecuperarAtencionID(codigoAtencion);
                pacienteCuenta = NegPacientes.recuperarPacientePorAtencion(codigoAtencion);
                cargarDatosPaciente(pacienteCuenta, atencion);
                listaPedidoAreas = NegDetalleCuenta.recuperarPedidosAreas();
                //List<DtoDetalleCuentaPaciente> listap = new List<DtoDetalleCuentaPaciente>();

                dtListaItemsEliminados = NegDetalleCuenta.ListaItemsEliminadosCuenta(codigoAtencion); //GT

                if (dtListaItemsEliminados.Rows.Count > 0)
                {
                    grvItemsEliminados.DataSource = dtListaItemsEliminados;
                    //btnItemsEliminados.Visible = true;

                }
                DataTable dtItemsNuevos = new DataTable();
                dtItemsNuevos = NegDetalleCuenta.MuestraItemsNuevos(codigoAtencion); //GT

                if (dtItemsNuevos.Rows.Count > 0)
                {
                    dgrItemsNuevos.DataSource = dtItemsNuevos;
                    //btnItemsEliminados.Visible = true;

                }

                DataTable dtItemsModificados = new DataTable();
                dtItemsModificados = NegDetalleCuenta.MuestraItemsModificados1(codigoAtencion);
                if (dtItemsModificados.Rows.Count > 0)
                {
                    dgrItemsModificados.DataSource = dtItemsModificados;
                }

                listaPedido = NegDetalleCuenta.recuperarCuentaPaciente(codigoAtencion);
                var query = (from a in listaPedido
                             group a by new
                             {
                                 RUBRO = a.RUBRO_NOMBRE
                             }
                                 into grupo
                                 select new
                                 {
                                     RUBRO = grupo.Key.RUBRO,
                                     DETALLE = grupo,
                                     //TOTAL = grupo.Sum(s => s.TOTAL),
                                     //IVA = grupo.Sum(s => s.IVA),
                                     TOTALES_CUENTA = grupo.Sum(s => s.TOTAL)
                                     //Nro_PRODUCTOS = grupo.Sum(s => s.CANTIDAD)
                                 }).ToList();


                ulgdbListadoCIE.DataSource = query;
                UltraGridBand bandUno = ulgdbListadoCIE.DisplayLayout.Bands[0];
                ulgdbListadoCIE.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ulgdbListadoCIE.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ulgdbListadoCIE.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ulgdbListadoCIE.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                ulgdbListadoCIE.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

                //Añado la columna de totales
                ulgdbListadoCIE.DisplayLayout.UseFixedHeaders = true;
                bandUno.Summaries.Clear();
                bandUno.SummaryFooterCaption = "Total: ";
                bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
                bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.Blue;

                //suma de valor total de la cuenta 
                SummarySettings sumValor = bandUno.Summaries.Add("TOTALES_CUENTA", SummaryType.Sum, bandUno.Columns["TOTALES_CUENTA"]);
                //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
                sumValor.DisplayFormat = "{0:#####.00}";
                sumValor.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;


                //var query = (from a in listaPedido
                //             group a by a.RUBRO_NOMBRE into grupo
                //             select new {grupo.Key, detalle = grupo }).ToList();
                //ulgdbListadoCIE.DataSource =query;    


                // for (int FilaG = 1; FilaG <= ulgdbListadoCIE.Rows.Count(); FilaG++)
                //{

                //    MessageBox.Show(FilaG.ToString());

                //}



            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarArbol(List<PEDIDOS_AREAS> listaPedidoAreas)
        {
        }

        /// <summary>
        /// Busca el directorio donde se guarda el archivo de excel
        /// </summary>
        /// <returns>retorna el directorio</returns>
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

        private string CargarConvenio() // Carga el nombre de la aseguradora a la que esta asignado el paciente / Giovanny Tapia / 31/08/2012
        {
            string resultado = "";
            //ATENCION_DETALLE_CATEGORIAS
            List<ASEGURADORAS_EMPRESAS> listaAseguradoras = new List<ASEGURADORAS_EMPRESAS>();
            detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(atencion.ATE_CODIGO);
            Int16 CategoriaId = 0; // Variable para cargar la Aseguradora (IESS/SOAT) / Giovanny Tapia /30/08/2012
            if (detalleCategorias != null)
            {
                foreach (ATENCION_DETALLE_CATEGORIAS detalle in detalleCategorias)
                {
                    if (detalle != null)
                    {
                        if ((Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 21) || (Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 125)) // Convenio 21=IESS /125 soat (Aumento 125 para que se cargue aseguradoras SOAT ) /Giovanny Tapia / 30/08/2012
                        {
                            CategoriaId = (Convert.ToInt16(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value));
                            resultado =
                                //((CATEGORIAS_CONVENIOS) (NegCategorias.RecuperaCategoriaID(21))).CAT_NOMBRE;                     
                                ((CATEGORIAS_CONVENIOS)(NegCategorias.RecuperaCategoriaID(CategoriaId))).CAT_NOMBRE; //Busco el nombre de la aseguradora apartir del codigo / Giovanny Tapia / 30/08/2012
                            return resultado;
                        }
                        else
                        {
                            resultado = String.Empty;
                            return resultado;
                        }
                    }
                }
            }
            else
            {
                resultado = String.Empty;
                return resultado;
            }

            return resultado;
        }


        #endregion




        #region Eventos

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ulgdbListadoCIE.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ulgdbListadoCIE, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            ReporteCuentaPaciente reporteCuenta = new ReporteCuentaPaciente();
            reporteCuenta.CUE_INSTITUCION = "Clinica Pasteur";
            reporteCuenta.CUE_NOMBRE_PACIENTE = lbl_Paciente.Text;
            reporteCuenta.CUE_NTRAMITE = " ";
            reporteCuenta.CUE_NEXPEDIENTE = " ";
            reporteCuenta.CUE_HC = lbl_HistoriaC.Text;
            //reporteCuenta.CUE_NATENCION = this.lbl_NroAtencion.Text;
            reporteCuenta.CUE_NATENCION = lblNumeroAtencion.Text;// A la impresion envio el numero de atencion no el codigo /30/10/2012 / GIOVANNY TAPIA
            reporteCuenta.CUE_HABITACION = "H";
            reporteCuenta.CUE_FECHA_INGRESO = Convert.ToDateTime(lbl_Fecha.Text);
            reporteCuenta.CUE_FECHA_ALTA = Convert.ToDateTime(atencion.ATE_FECHA_ALTA);
            ReportesCuentas reporte = new ReportesCuentas();
            reporte.ingresarCuentaPaciente(reporteCuenta);
            List<RUBROS> listaRubros = new List<RUBROS>();
            listaRubros = NegRubros.recuperarRubros();
            decimal total = 0;
            List<PEDIDOS_AREAS> listaAreas = NegPedidos.recuperarListaAreas();
            for (int i = 0; i < listaAreas.Count; i++)
            {
                PEDIDOS_AREAS areaP = listaAreas.ElementAt(i);
                if (areaP.DIV_CODIGO != His.Parametros.CuentasPacientes.CodigoFarmacia)
                {
                    ReporteDetalleCuenta reporteDetalle = new ReporteDetalleCuenta();
                    List<CUENTAS_PACIENTES> listaCuentas = new List<CUENTAS_PACIENTES>();
                    listaCuentas = NegCuentasPacientes.RecuperarCuentaArea(atencion.ATE_CODIGO, Convert.ToInt16(areaP.DIV_CODIGO));
                    Decimal totalRubros = 0;
                    for (int j = 0; j < listaCuentas.Count; j++)
                    {
                        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
                        cuentas = listaCuentas.ElementAt(j);
                        if (cuentas.MED_CODIGO != null && cuentas.MED_CODIGO != 0 && cuentas.RUB_CODIGO != 1 && cuentas.RUB_CODIGO != 27)
                        {
                            MEDICOS medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(cuentas.MED_CODIGO));
                            reporteDetalle.DC_DETALLE_RUBRO = cuentas.CUE_DETALLE + " Dr./a " + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                        }
                        else
                            reporteDetalle.DC_DETALLE_RUBRO = cuentas.CUE_DETALLE;
                        reporteDetalle.DC_RUBRO = " ";
                        reporteDetalle.DC_FECHA_RUBRO = cuentas.CUE_FECHA.Value.ToString().Substring(0, 10);
                        reporteDetalle.DC_CODIGO_RUBRO = cuentas.PRO_CODIGO_BARRAS;
                        reporteDetalle.DC_COSTO_U_RUBRO = Convert.ToString(cuentas.CUE_VALOR_UNITARIO);
                        reporteDetalle.DC_CANTIDAD_RUBRO = Convert.ToString(Math.Round(Convert.ToDouble(cuentas.CUE_CANTIDAD), 2));
                        reporteDetalle.DC_COSTO_T_RUBRO = Convert.ToString(cuentas.CUE_VALOR);
                        ReportesCuentas reporteD = new ReportesCuentas();
                        if (j == 0)
                        {
                            ReporteDetalleCuenta reporteDetalleRubro = new ReporteDetalleCuenta();
                            reporteDetalleRubro.DC_RUBRO = " ";
                            reporteDetalleRubro.DC_FECHA_RUBRO = " ";
                            reporteDetalleRubro.DC_CODIGO_RUBRO = " ";
                            reporteDetalleRubro.DC_DETALLE_RUBRO = areaP.PEA_NOMBRE.Trim();
                            reporteDetalleRubro.DC_COSTO_U_RUBRO = " ";
                            reporteDetalleRubro.DC_CANTIDAD_RUBRO = " ";
                            reporteDetalleRubro.DC_COSTO_T_RUBRO = " ";
                            reporteD.ingresarDetalleCuenta(reporteDetalleRubro);
                            reporteD = new ReportesCuentas();
                        }
                        totalRubros = Convert.ToDecimal(totalRubros + cuentas.CUE_VALOR);
                        reporteD.ingresarDetalleCuenta(reporteDetalle);
                    }
                    ReportesCuentas reporteDC = new ReportesCuentas();
                    if (totalRubros != 0)
                    {
                        ReporteDetalleCuenta reporteTotalRubro = new ReporteDetalleCuenta();
                        reporteTotalRubro.DC_DETALLE_RUBRO = "TOTAL";
                        reporteTotalRubro.DC_FECHA_RUBRO = " ";
                        reporteTotalRubro.DC_COSTO_T_RUBRO = Convert.ToString(totalRubros);
                        reporteDC.ingresarDetalleCuenta(reporteTotalRubro);
                        reporteDC = new ReportesCuentas();
                        total = total + totalRubros;
                    }
                }
                else
                {
                    ReporteDetalleCuenta reporteDetalle = new ReporteDetalleCuenta();
                    List<CUENTAS_PACIENTES> listaCuentas = new List<CUENTAS_PACIENTES>();
                    listaCuentas = NegCuentasPacientes.RecuperarCuentasRubros(atencion.ATE_CODIGO, 1);
                    Decimal totalRubros = 0;
                    for (int j = 0; j < listaCuentas.Count; j++)
                    {
                        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
                        cuentas = listaCuentas.ElementAt(j);
                        reporteDetalle.DC_RUBRO = " ";
                        reporteDetalle.DC_FECHA_RUBRO = cuentas.CUE_FECHA.Value.ToString().Substring(0, 10);
                        reporteDetalle.DC_CODIGO_RUBRO = cuentas.PRO_CODIGO_BARRAS;
                        reporteDetalle.DC_DETALLE_RUBRO = cuentas.CUE_DETALLE;
                        reporteDetalle.DC_COSTO_U_RUBRO = Convert.ToString(cuentas.CUE_VALOR_UNITARIO);
                        reporteDetalle.DC_CANTIDAD_RUBRO = Convert.ToString(Math.Round(Convert.ToDouble(cuentas.CUE_CANTIDAD), 2));
                        reporteDetalle.DC_COSTO_T_RUBRO = Convert.ToString(cuentas.CUE_VALOR);
                        ReportesCuentas reporteD = new ReportesCuentas();
                        if (j == 0)
                        {
                            ReporteDetalleCuenta reporteDetalleRubro = new ReporteDetalleCuenta();
                            reporteDetalleRubro.DC_RUBRO = " ";
                            reporteDetalleRubro.DC_FECHA_RUBRO = " ";
                            reporteDetalleRubro.DC_CODIGO_RUBRO = " ";
                            reporteDetalleRubro.DC_DETALLE_RUBRO = "MEDICINAS VALOR AL ORIGEN";
                            reporteDetalleRubro.DC_COSTO_U_RUBRO = " ";
                            reporteDetalleRubro.DC_CANTIDAD_RUBRO = " ";
                            reporteDetalleRubro.DC_COSTO_T_RUBRO = " ";
                            reporteD.ingresarDetalleCuenta(reporteDetalleRubro);
                            reporteD = new ReportesCuentas();
                        }
                        totalRubros = Convert.ToDecimal(totalRubros + cuentas.CUE_VALOR);
                        reporteD.ingresarDetalleCuenta(reporteDetalle);

                    }
                    ReportesCuentas reporteDC = new ReportesCuentas();
                    if (totalRubros != 0)
                    {
                        ReporteDetalleCuenta reporteTotalRubro = new ReporteDetalleCuenta();
                        reporteTotalRubro.DC_DETALLE_RUBRO = "TOTAL";
                        reporteTotalRubro.DC_FECHA_RUBRO = " ";
                        reporteTotalRubro.DC_COSTO_T_RUBRO = Convert.ToString(totalRubros);
                        reporteDC.ingresarDetalleCuenta(reporteTotalRubro);
                        reporteDC = new ReportesCuentas();
                        total = total + totalRubros;
                    }
                    reporteDetalle = new ReporteDetalleCuenta();
                    listaCuentas = new List<CUENTAS_PACIENTES>();
                    listaCuentas = NegCuentasPacientes.RecuperarCuentasRubros(atencion.ATE_CODIGO, 27);
                    totalRubros = 0;
                    for (int j = 0; j < listaCuentas.Count; j++)
                    {
                        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
                        cuentas = listaCuentas.ElementAt(j);
                        reporteDetalle.DC_RUBRO = " ";
                        reporteDetalle.DC_FECHA_RUBRO = cuentas.CUE_FECHA.Value.ToString().Substring(0, 10);
                        reporteDetalle.DC_CODIGO_RUBRO = cuentas.PRO_CODIGO_BARRAS;
                        reporteDetalle.DC_DETALLE_RUBRO = cuentas.CUE_DETALLE;
                        reporteDetalle.DC_COSTO_U_RUBRO = Convert.ToString(cuentas.CUE_VALOR_UNITARIO);
                        reporteDetalle.DC_CANTIDAD_RUBRO = Convert.ToString(Math.Round(Convert.ToDouble(cuentas.CUE_CANTIDAD), 2));
                        reporteDetalle.DC_COSTO_T_RUBRO = Convert.ToString(cuentas.CUE_VALOR);
                        ReportesCuentas reporteD = new ReportesCuentas();
                        if (j == 0)
                        {
                            ReporteDetalleCuenta reporteDetalleRubro = new ReporteDetalleCuenta();
                            reporteDetalleRubro.DC_RUBRO = " ";
                            reporteDetalleRubro.DC_FECHA_RUBRO = " ";
                            reporteDetalleRubro.DC_CODIGO_RUBRO = " ";
                            reporteDetalleRubro.DC_DETALLE_RUBRO = "INSUMOS - VALOR AL ORIGEN";
                            reporteDetalleRubro.DC_COSTO_U_RUBRO = " ";
                            reporteDetalleRubro.DC_CANTIDAD_RUBRO = " ";
                            reporteDetalleRubro.DC_COSTO_T_RUBRO = " ";
                            reporteD.ingresarDetalleCuenta(reporteDetalleRubro);
                            reporteD = new ReportesCuentas();
                        }
                        totalRubros = Convert.ToDecimal(totalRubros + cuentas.CUE_VALOR);
                        reporteD.ingresarDetalleCuenta(reporteDetalle);

                    }
                    reporteDC = new ReportesCuentas();
                    if (totalRubros != 0)
                    {
                        ReporteDetalleCuenta reporteTotalRubro = new ReporteDetalleCuenta();
                        reporteTotalRubro.DC_DETALLE_RUBRO = "TOTAL";
                        reporteTotalRubro.DC_FECHA_RUBRO = " ";
                        reporteTotalRubro.DC_COSTO_T_RUBRO = Convert.ToString(totalRubros);
                        reporteDC.ingresarDetalleCuenta(reporteTotalRubro);
                        reporteDC = new ReportesCuentas();
                        total = total + totalRubros;
                    }
                }
            }
            ReportesCuentas reporteTotal = new ReportesCuentas();
            ReporteDetalleCuenta reporteDetalleTotal = new ReporteDetalleCuenta();
            reporteDetalleTotal.DC_DETALLE_RUBRO = "TOTAL CUENTA";
            reporteDetalleTotal.DC_COSTO_T_RUBRO = Convert.ToString(total);
            reporteTotal.ingresarDetalleCuenta(reporteDetalleTotal);
            reporteTotal = new ReportesCuentas();
            frmReportes ventana = new frmReportes(1, "CuentaPaciente");
            ventana.Show();
        }

        private void frmDetalleCuenta_Load(object sender, EventArgs e)
        {

        }

        private void ulgdbListadoCIE_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            //ulgdbListadoCIE.Rows[1].Cells["DESCRIPCION"].ActiveAppearance.BackColor = System.Drawing.Color.Red
            try
            {

                Int64 CodigoAtencion = 0;
                string CodigoProducto = "";
                Int64 CodigoDetalle = 0;

                CodigoAtencion = Convert.ToInt64(lbl_NroAtencion.Text);
                CodigoProducto = Convert.ToString(e.Row.Cells["CODIGO"].Value);
                CodigoDetalle = Convert.ToInt64(e.Row.Cells["INDICE"].Value);


                var Verifica = from qs in dtCuentasModificadas.AsEnumerable()
                               where (qs.Field<string>("PRO_CODIGO_BARRAS") == CodigoProducto) && (qs.Field<Int64>("CUE_CODIGO") == CodigoDetalle)
                               select qs;

                foreach (DataRow dr in Verifica)
                {
                    e.Row.Appearance.BackColor = Color.Orange;
                }

            }

            catch (Exception ex)
            { return; }
        }

        private void ulgdbListadoCIE_AfterRowExpanded(object sender, RowEventArgs e)
        {
            //Añado la columna de totales
            UltraGridBand bandUno = ulgdbListadoCIE.DisplayLayout.Bands[1];
            ulgdbListadoCIE.DisplayLayout.UseFixedHeaders = true;
            bandUno.Summaries.Clear();
            bandUno.SummaryFooterCaption = "Total: ";
            bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
            bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.Blue;

            //suma de valor total de la cuenta 
            SummarySettings sumValor = bandUno.Summaries.Add("TOTAL", SummaryType.Sum, bandUno.Columns["TOTAL"]);
            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
            sumValor.DisplayFormat = "{0:#####.00}";
            sumValor.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
        }


        private void ulgdbListadoCIE_ClickCell(object sender, ClickCellEventArgs e)
        {
            try
            {
                //if (tabControl1.SelectedTab.Text != "Items Modificados")
                if (tabControl1.SelectedIndex != 1)
                {
                    tabControl1.SelectedIndex = 1;
                }

                Int64 CodigoAtencion = 0;
                string CodigoProducto = "";
                Int64 CodigoDetalle = 0;

                CodigoAtencion = Convert.ToInt64(lbl_NroAtencion.Text);
                CodigoProducto = Convert.ToString(ulgdbListadoCIE.ActiveRow.Cells["CODIGO"].Value);
                CodigoDetalle = Convert.ToInt64(ulgdbListadoCIE.ActiveRow.Cells["INDICE"].Value);

                DataTable dtItemsModificados = new DataTable();
                dtItemsModificados = NegDetalleCuenta.MuestraItemsModificados(CodigoAtencion, CodigoProducto, CodigoDetalle);

                dgrItemsModificados.DataSource = dtItemsModificados;
            }
            catch (Exception er)
            {
                return;
            }

        }

        #endregion

        private void ulgdbListadoCIE_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }
    }
}



//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using His.Entidades;
//using His.Entidades.Pedidos;
//using His.Entidades.Reportes;
//using His.Negocio;
//using His.Entidades.Clases;
//using Recursos;
//using Infragistics.Win.UltraWinGrid;
//using System.Data.SqlClient;
//using System.Data.Objects;
//using System.Data.Objects.DataClasses;
//using His.General;
//using System.IO;
//using His.DatosReportes;

//namespace CuentaPaciente
//{
//    public partial class frmDetalleCuenta : Form
//    {
//        #region Variables
//        public bool pacienteNuevo = false;
//        PACIENTES pacienteCuenta = new PACIENTES();
//        ATENCIONES atencion = new ATENCIONES();
//        //List<PEDIDOS> listaPedido = new List<PEDIDOS>();
//        List<DtoDetalleCuentaPaciente> listaPedido = new List<DtoDetalleCuentaPaciente>();
//        List<PEDIDOS_AREAS> listaPedidoAreas = new List<PEDIDOS_AREAS>();
//        Infragistics.Win.UltraWinTree.UltraTree raiz = new Infragistics.Win.UltraWinTree.UltraTree();
//        public string codigoAtencionA;
//        private List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = null;

//        DataTable dtCuentasModificadas = new DataTable(); // aumento para seleccioonar todos los items modificados de una cuenta / Giovanny Tapia / 24/08/2012

//        #endregion

//        public frmDetalleCuenta()
//        {
//            InitializeComponent();
//        }

//        public frmDetalleCuenta(int codigoAtencion)
//        {
//            InitializeComponent();
//            cargarPaciente(codigoAtencion);
//            btn_Imprimir.Image = Archivo.imgBtnImprimir32;
//            btn_Excel.Image = Archivo.imgOfficeExcel;
//            btn_Salir.Image = Archivo.imgBtnSalir32;
//        }

//        #region Métodos

//        private void cargarDatosPaciente(PACIENTES pacienteActual, ATENCIONES  atencion)
//        {
//            lbl_HistoriaC.Text = pacienteActual.PAC_HISTORIA_CLINICA.Trim();
//            lbl_Paciente.Text = pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2 + " " + pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO;
//            lbl_Fecha.Text = Convert.ToString(atencion.ATE_FECHA_INGRESO);
//            lbl_NroAtencion.Text = Convert.ToString(atencion.ATE_CODIGO);
//            lblNumeroAtencion.Text = Convert.ToString(atencion.ATE_NUMERO_ATENCION); // Para que tome el numero de la atencion y no el codigo , el codigo de la atencion esta oculto abajo del numero /30/01/2012 / GIOVANNY TAPIA
//            lbl_NroSeguro.Text = CargarConvenio();           
//            lbl_MedicoP.Text = Convert.ToString(atencion.MEDICOS.MED_NOMBRE1 + " "+ atencion.MEDICOS.MED_NOMBRE2 + " " + atencion.MEDICOS.MED_APELLIDO_PATERNO + " " + atencion.MEDICOS.MED_APELLIDO_MATERNO);
//            lbl_EdadAños.Text = Funciones.CalcularEdad(Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO)).ToString() + " años";
//            this.lblFechaAlta.Text = Convert.ToString(atencion.ATE_FECHA_ALTA);

//            dtCuentasModificadas = NegDetalleCuenta.CargaItemsModificados(atencion.ATE_CODIGO);
//        }

//        public void cargarPaciente(int codigoAtencion)
//        {               
//            try
//            {
//                DataTable dtListaItemsEliminados = new DataTable();

//                atencion = NegAtenciones.RecuperarAtencionID(codigoAtencion);
//                pacienteCuenta = NegPacientes.recuperarPacientePorAtencion(codigoAtencion);
//                cargarDatosPaciente(pacienteCuenta, atencion);
//                listaPedidoAreas = NegDetalleCuenta.recuperarPedidosAreas();             
//                //List<DtoDetalleCuentaPaciente> listap = new List<DtoDetalleCuentaPaciente>();

//                dtListaItemsEliminados = NegDetalleCuenta.ListaItemsEliminadosCuenta(codigoAtencion); //GT

//                if (dtListaItemsEliminados.Rows.Count > 0)
//                {
//                    grvItemsEliminados.DataSource = dtListaItemsEliminados;
//                    //btnItemsEliminados.Visible = true;
                    
//                }
                                                                                                                                                                                                             
//                listaPedido = NegDetalleCuenta.recuperarCuentaPaciente(codigoAtencion);
//                var query = (from a in listaPedido
//                             group a by new
//                             {
//                                 RUBRO = a.RUBRO_NOMBRE
//                             }
//                                 into grupo
//                                 select new
//                                 {
//                                     RUBRO = grupo.Key.RUBRO,
//                                     DETALLE = grupo,
//                                     //TOTAL = grupo.Sum(s => s.TOTAL),
//                                     //IVA = grupo.Sum(s => s.IVA),
//                                     TOTALES_CUENTA = grupo.Sum(s => s.TOTAL)
                                     
//                                     //Nro_PRODUCTOS = grupo.Sum(s => s.CANTIDAD)
//                                 }).ToList();
                

//                ulgdbListadoCIE.DataSource = query;
//                UltraGridBand bandUno = ulgdbListadoCIE.DisplayLayout.Bands[0];
//                ulgdbListadoCIE.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
//                ulgdbListadoCIE.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
//                ulgdbListadoCIE.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
//                ulgdbListadoCIE.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
//                ulgdbListadoCIE.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

//                //Añado la columna de totales
//                ulgdbListadoCIE.DisplayLayout.UseFixedHeaders = true;
//                bandUno.Summaries.Clear();
//                bandUno.SummaryFooterCaption = "Total: ";
//                bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
//                bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
//                bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.Blue;

//                //suma de valor total de la cuenta 
//                SummarySettings sumValor = bandUno.Summaries.Add("TOTALES_CUENTA", SummaryType.Sum, bandUno.Columns["TOTALES_CUENTA"]);
//                //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
//                sumValor.DisplayFormat = "{0:#####.00}";
//                sumValor.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                
//                //var query = (from a in listaPedido
//                //             group a by a.RUBRO_NOMBRE into grupo
//                //             select new {grupo.Key, detalle = grupo }).ToList();
//                //ulgdbListadoCIE.DataSource =query;    
       

//                // for (int FilaG = 1; FilaG <= ulgdbListadoCIE.Rows.Count(); FilaG++)
//                //{

//                //    MessageBox.Show(FilaG.ToString());

//                //}



//            }
//            catch (Exception err)
//            {
//                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void cargarArbol(List<PEDIDOS_AREAS> listaPedidoAreas) 
//        {
//        }

//        /// <summary>
//        /// Busca el directorio donde se guarda el archivo de excel
//        /// </summary>
//        /// <returns>retorna el directorio</returns>
//        private String FindSavePath()
//        {
//            Stream myStream;
//            string myFilepath = null;
//            try
//            {
//                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
//                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
//                saveFileDialog1.FilterIndex = 2;
//                saveFileDialog1.RestoreDirectory = true;
//                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
//                {
//                    if ((myStream = saveFileDialog1.OpenFile()) != null)
//                    {
//                        myFilepath = saveFileDialog1.FileName;
//                        myStream.Close();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return myFilepath;
//        }

//        private string CargarConvenio() // Carga el nombre de la aseguradora a la que esta asignado el paciente / Giovanny Tapia / 31/08/2012
//        {
//            string resultado = "";
//            //ATENCION_DETALLE_CATEGORIAS
//            List<ASEGURADORAS_EMPRESAS> listaAseguradoras = new List<ASEGURADORAS_EMPRESAS>();
//            detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(atencion.ATE_CODIGO);
//            Int16 CategoriaId = 0; // Variable para cargar la Aseguradora (IESS/SOAT) / Giovanny Tapia /30/08/2012
//            if (detalleCategorias != null)
//            {
//                foreach (ATENCION_DETALLE_CATEGORIAS detalle in detalleCategorias)
//                {
//                    if (detalle != null)
//                    {
//                        if ((Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 21) || (Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 125)) // Convenio 21=IESS /125 soat (Aumento 125 para que se cargue aseguradoras SOAT ) /Giovanny Tapia / 30/08/2012
//                        {
//                            CategoriaId = (Convert.ToInt16(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value));
//                            resultado =
//                                //((CATEGORIAS_CONVENIOS) (NegCategorias.RecuperaCategoriaID(21))).CAT_NOMBRE;                     
//                                ((CATEGORIAS_CONVENIOS)(NegCategorias.RecuperaCategoriaID(CategoriaId))).CAT_NOMBRE; //Busco el nombre de la aseguradora apartir del codigo / Giovanny Tapia / 30/08/2012
//                            return resultado;
//                        }
//                        else
//                        {
//                            resultado = String.Empty;
//                            return resultado;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                resultado = String.Empty;
//                return resultado;
//            }

//            return resultado;
//        }


//        #endregion

       


//        #region Eventos

//        private void btn_Salir_Click(object sender, EventArgs e)
//        {
//            this.Dispose();
//        }

//        private void btn_Excel_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string PathExcel = FindSavePath();
//                if (PathExcel != null)
//                {
//                    if (ulgdbListadoCIE.CanFocus == true)
//                        this.ultraGridExcelExporter1.Export(ulgdbListadoCIE, PathExcel);
//                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
//                }
//            }
//            catch (Exception ex)
//            { MessageBox.Show(ex.Message); }
//            finally
//            { this.Cursor = Cursors.Default; }
//        }

//        private void btn_Imprimir_Click(object sender, EventArgs e)
//        {
//            ReporteCuentaPaciente reporteCuenta = new ReporteCuentaPaciente();
//            reporteCuenta.CUE_INSTITUCION = "CLINICA INTEGRAL GRUPO HEALTH";
//            reporteCuenta.CUE_NOMBRE_PACIENTE = lbl_Paciente.Text;
//            reporteCuenta.CUE_NTRAMITE = " ";
//            reporteCuenta.CUE_NEXPEDIENTE = " ";
//            reporteCuenta.CUE_HC = lbl_HistoriaC.Text;
//            //reporteCuenta.CUE_NATENCION = this.lbl_NroAtencion.Text;
//            reporteCuenta.CUE_NATENCION = lblNumeroAtencion.Text;// A la impresion envio el numero de atencion no el codigo /30/10/2012 / GIOVANNY TAPIA
//            reporteCuenta.CUE_HABITACION = "H";
//            reporteCuenta.CUE_FECHA_INGRESO = Convert.ToDateTime(lbl_Fecha.Text);
//            reporteCuenta.CUE_FECHA_ALTA = Convert.ToDateTime(atencion.ATE_FECHA_ALTA);
//            ReportesCuentas reporte = new ReportesCuentas();
//            reporte.ingresarCuentaPaciente(reporteCuenta);
//            List<RUBROS> listaRubros = new List<RUBROS>();
//            listaRubros = NegRubros.recuperarRubros();
//            decimal total = 0;
//            List<PEDIDOS_AREAS> listaAreas = NegPedidos.recuperarListaAreas();
//            for (int i = 0; i < listaAreas.Count; i++)
//            {
//                PEDIDOS_AREAS areaP = listaAreas.ElementAt(i);
//                if (areaP.DIV_CODIGO != His.Parametros.CuentasPacientes.CodigoFarmacia)
//                {
//                    ReporteDetalleCuenta reporteDetalle = new ReporteDetalleCuenta();
//                    List<CUENTAS_PACIENTES> listaCuentas = new List<CUENTAS_PACIENTES>();
//                    listaCuentas = NegCuentasPacientes.RecuperarCuentaArea(atencion.ATE_CODIGO, Convert.ToInt16(areaP.DIV_CODIGO));
//                    Decimal totalRubros = 0;
//                    for (int j = 0; j < listaCuentas.Count; j++)
//                    {
//                        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
//                        cuentas = listaCuentas.ElementAt(j);
//                        if (cuentas.MED_CODIGO != null && cuentas.MED_CODIGO != 0)
//                        {
//                            MEDICOS medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(cuentas.MED_CODIGO));
//                            reporteDetalle.DC_DETALLE_RUBRO = cuentas.CUE_DETALLE + " Dr./a " + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
//                        }
//                        else
//                            reporteDetalle.DC_DETALLE_RUBRO = cuentas.CUE_DETALLE;
//                        reporteDetalle.DC_RUBRO = " ";
//                        reporteDetalle.DC_FECHA_RUBRO = cuentas.CUE_FECHA.Value.ToString().Substring(0,10);
//                        reporteDetalle.DC_CODIGO_RUBRO = cuentas.PRO_CODIGO_BARRAS;
//                        reporteDetalle.DC_COSTO_U_RUBRO = Convert.ToString(cuentas.CUE_VALOR_UNITARIO);
//                        reporteDetalle.DC_CANTIDAD_RUBRO = Convert.ToString(Math.Round(Convert.ToDouble(cuentas.CUE_CANTIDAD), 2));
//                        reporteDetalle.DC_COSTO_T_RUBRO = Convert.ToString(cuentas.CUE_VALOR);
//                        ReportesCuentas reporteD = new ReportesCuentas();
//                        if (j == 0)
//                        {
//                            ReporteDetalleCuenta reporteDetalleRubro = new ReporteDetalleCuenta();
//                            reporteDetalleRubro.DC_RUBRO = " ";
//                            reporteDetalleRubro.DC_FECHA_RUBRO = " ";
//                            reporteDetalleRubro.DC_CODIGO_RUBRO = " ";
//                            reporteDetalleRubro.DC_DETALLE_RUBRO = areaP.PEA_NOMBRE.Trim();
//                            reporteDetalleRubro.DC_COSTO_U_RUBRO = " ";
//                            reporteDetalleRubro.DC_CANTIDAD_RUBRO = " ";
//                            reporteDetalleRubro.DC_COSTO_T_RUBRO = " ";
//                            reporteD.ingresarDetalleCuenta(reporteDetalleRubro);
//                            reporteD = new ReportesCuentas();
//                        }
//                        totalRubros = Convert.ToDecimal(totalRubros + cuentas.CUE_VALOR);
//                        reporteD.ingresarDetalleCuenta(reporteDetalle);
//                    }
//                    ReportesCuentas reporteDC = new ReportesCuentas();
//                    if (totalRubros != 0)
//                    {
//                        ReporteDetalleCuenta reporteTotalRubro = new ReporteDetalleCuenta();
//                        reporteTotalRubro.DC_DETALLE_RUBRO = "TOTAL";
//                        reporteTotalRubro.DC_FECHA_RUBRO = " ";
//                        reporteTotalRubro.DC_COSTO_T_RUBRO = Convert.ToString(totalRubros);
//                        reporteDC.ingresarDetalleCuenta(reporteTotalRubro);
//                        reporteDC = new ReportesCuentas();
//                        total = total + totalRubros;
//                    }
//                }else
//                {
//                    ReporteDetalleCuenta reporteDetalle = new ReporteDetalleCuenta();
//                    List<CUENTAS_PACIENTES> listaCuentas = new List<CUENTAS_PACIENTES>();
//                    listaCuentas = NegCuentasPacientes.RecuperarCuentasRubros(atencion.ATE_CODIGO, 1);
//                    Decimal totalRubros = 0;
//                    for (int j = 0; j < listaCuentas.Count; j++)
//                    {
//                        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
//                        cuentas = listaCuentas.ElementAt(j);
//                        reporteDetalle.DC_RUBRO = " ";
//                        reporteDetalle.DC_FECHA_RUBRO = cuentas.CUE_FECHA.Value.ToString().Substring(0, 10);
//                        reporteDetalle.DC_CODIGO_RUBRO = cuentas.PRO_CODIGO_BARRAS;
//                        reporteDetalle.DC_DETALLE_RUBRO = cuentas.CUE_DETALLE;
//                        reporteDetalle.DC_COSTO_U_RUBRO = Convert.ToString(cuentas.CUE_VALOR_UNITARIO);
//                        reporteDetalle.DC_CANTIDAD_RUBRO = Convert.ToString(Math.Round(Convert.ToDouble(cuentas.CUE_CANTIDAD), 2));
//                        reporteDetalle.DC_COSTO_T_RUBRO = Convert.ToString(cuentas.CUE_VALOR);
//                        ReportesCuentas reporteD = new ReportesCuentas();
//                        if (j == 0)
//                        {
//                            ReporteDetalleCuenta reporteDetalleRubro = new ReporteDetalleCuenta();
//                            reporteDetalleRubro.DC_RUBRO = " ";
//                            reporteDetalleRubro.DC_FECHA_RUBRO = " ";
//                            reporteDetalleRubro.DC_CODIGO_RUBRO = " ";
//                            reporteDetalleRubro.DC_DETALLE_RUBRO = "MEDICINAS VALOR AL ORIGEN";
//                            reporteDetalleRubro.DC_COSTO_U_RUBRO = " ";
//                            reporteDetalleRubro.DC_CANTIDAD_RUBRO = " ";
//                            reporteDetalleRubro.DC_COSTO_T_RUBRO = " ";
//                            reporteD.ingresarDetalleCuenta(reporteDetalleRubro);
//                            reporteD = new ReportesCuentas();
//                        }
//                        totalRubros = Convert.ToDecimal(totalRubros + cuentas.CUE_VALOR);
//                        reporteD.ingresarDetalleCuenta(reporteDetalle);

//                    }
//                    ReportesCuentas reporteDC = new ReportesCuentas();
//                    if (totalRubros != 0)
//                    {
//                        ReporteDetalleCuenta reporteTotalRubro = new ReporteDetalleCuenta();
//                        reporteTotalRubro.DC_DETALLE_RUBRO = "TOTAL";
//                        reporteTotalRubro.DC_FECHA_RUBRO = " ";
//                        reporteTotalRubro.DC_COSTO_T_RUBRO = Convert.ToString(totalRubros);
//                        reporteDC.ingresarDetalleCuenta(reporteTotalRubro);
//                        reporteDC = new ReportesCuentas();
//                        total = total + totalRubros;
//                    }
//                    reporteDetalle = new ReporteDetalleCuenta();
//                    listaCuentas = new List<CUENTAS_PACIENTES>();
//                    listaCuentas = NegCuentasPacientes.RecuperarCuentasRubros(atencion.ATE_CODIGO, 27);
//                    totalRubros = 0;
//                    for (int j = 0; j < listaCuentas.Count; j++)
//                    {
//                        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
//                        cuentas = listaCuentas.ElementAt(j);
//                        reporteDetalle.DC_RUBRO = " ";
//                        reporteDetalle.DC_FECHA_RUBRO = cuentas.CUE_FECHA.Value.ToString().Substring(0, 10);
//                        reporteDetalle.DC_CODIGO_RUBRO = cuentas.PRO_CODIGO_BARRAS;
//                        reporteDetalle.DC_DETALLE_RUBRO = cuentas.CUE_DETALLE;
//                        reporteDetalle.DC_COSTO_U_RUBRO = Convert.ToString(cuentas.CUE_VALOR_UNITARIO);
//                        reporteDetalle.DC_CANTIDAD_RUBRO = Convert.ToString(Math.Round(Convert.ToDouble(cuentas.CUE_CANTIDAD), 2));
//                        reporteDetalle.DC_COSTO_T_RUBRO = Convert.ToString(cuentas.CUE_VALOR);
//                        ReportesCuentas reporteD = new ReportesCuentas();
//                        if (j == 0)
//                        {
//                            ReporteDetalleCuenta reporteDetalleRubro = new ReporteDetalleCuenta();
//                            reporteDetalleRubro.DC_RUBRO = " ";
//                            reporteDetalleRubro.DC_FECHA_RUBRO = " ";
//                            reporteDetalleRubro.DC_CODIGO_RUBRO = " ";
//                            reporteDetalleRubro.DC_DETALLE_RUBRO = "INSUMOS - VALOR AL ORIGEN";
//                            reporteDetalleRubro.DC_COSTO_U_RUBRO = " ";
//                            reporteDetalleRubro.DC_CANTIDAD_RUBRO = " ";
//                            reporteDetalleRubro.DC_COSTO_T_RUBRO = " ";
//                            reporteD.ingresarDetalleCuenta(reporteDetalleRubro);
//                            reporteD = new ReportesCuentas();
//                        }
//                        totalRubros = Convert.ToDecimal(totalRubros + cuentas.CUE_VALOR);
//                        reporteD.ingresarDetalleCuenta(reporteDetalle);

//                    }
//                    reporteDC = new ReportesCuentas();
//                    if (totalRubros != 0)
//                    {
//                        ReporteDetalleCuenta reporteTotalRubro = new ReporteDetalleCuenta();
//                        reporteTotalRubro.DC_DETALLE_RUBRO = "TOTAL";
//                        reporteTotalRubro.DC_FECHA_RUBRO = " ";
//                        reporteTotalRubro.DC_COSTO_T_RUBRO = Convert.ToString(totalRubros);
//                        reporteDC.ingresarDetalleCuenta(reporteTotalRubro);
//                        reporteDC = new ReportesCuentas();
//                        total = total + totalRubros;
//                    }
//                }
//            }
//            ReportesCuentas reporteTotal = new ReportesCuentas();
//            ReporteDetalleCuenta reporteDetalleTotal = new ReporteDetalleCuenta();
//            reporteDetalleTotal.DC_DETALLE_RUBRO = "TOTAL CUENTA";
//            reporteDetalleTotal.DC_COSTO_T_RUBRO = Convert.ToString(total);
//            reporteTotal.ingresarDetalleCuenta(reporteDetalleTotal);
//            reporteTotal = new ReportesCuentas();
//            frmReportes ventana = new frmReportes(1, "CuentaPaciente");
//            ventana.Show();
//        }

//        private void frmDetalleCuenta_Load(object sender, EventArgs e)
//        {

//        }

//        private void ulgdbListadoCIE_InitializeRow(object sender, InitializeRowEventArgs e)
//        {
//            //ulgdbListadoCIE.Rows[1].Cells["DESCRIPCION"].ActiveAppearance.BackColor = System.Drawing.Color.Red
//            try
//            {

//                Int64 CodigoAtencion = 0;
//                string CodigoProducto = "";
//                Int64 CodigoDetalle = 0;

//                CodigoAtencion = Convert.ToInt64(lbl_NroAtencion.Text);
//                CodigoProducto = Convert.ToString(e.Row.Cells["CODIGO"].Value);
//                CodigoDetalle = Convert.ToInt64(e.Row.Cells["INDICE"].Value);


//                var Verifica = from qs in dtCuentasModificadas.AsEnumerable()
//                               where (qs.Field<string>("PRO_CODIGO_BARRAS") == CodigoProducto) && (qs.Field<Int64>("CUE_CODIGO") == CodigoDetalle)
//                               select qs;

//                foreach (DataRow dr in Verifica)
//                {
//                    e.Row.Appearance.BackColor = Color.Orange;
//                }

//            }

//            catch (Exception ex)
//            { return; }
//        }

//        private void ulgdbListadoCIE_AfterRowExpanded(object sender, RowEventArgs e)
//        {
//            //Añado la columna de totales
//            UltraGridBand bandUno = ulgdbListadoCIE.DisplayLayout.Bands[1];
//            ulgdbListadoCIE.DisplayLayout.UseFixedHeaders = true;
//            bandUno.Summaries.Clear();
//            bandUno.SummaryFooterCaption = "Total: ";
//            bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
//            bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
//            bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.Blue;

//            //suma de valor total de la cuenta 
//            SummarySettings sumValor = bandUno.Summaries.Add("TOTAL", SummaryType.Sum, bandUno.Columns["TOTAL"]);
//            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
//            sumValor.DisplayFormat = "{0:#####.00}";
//            sumValor.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
//        }

       
//        private void ulgdbListadoCIE_ClickCell(object sender, ClickCellEventArgs e)
//        {
//            try
//            {
//                //if (tabControl1.SelectedTab.Text != "Items Modificados")
//                if (tabControl1.SelectedIndex != 1)
//                {
//                    tabControl1.SelectedIndex = 1;
//                }

//                Int64 CodigoAtencion = 0;
//                string CodigoProducto = "";
//                Int64 CodigoDetalle = 0;

//                CodigoAtencion = Convert.ToInt64(lbl_NroAtencion.Text);
//                CodigoProducto = Convert.ToString(ulgdbListadoCIE.ActiveRow.Cells["CODIGO"].Value);
//                CodigoDetalle = Convert.ToInt64(ulgdbListadoCIE.ActiveRow.Cells["INDICE"].Value);

//                DataTable dtItemsModificados = new DataTable();
//                dtItemsModificados = NegDetalleCuenta.MuestraItemsModificados(CodigoAtencion, CodigoProducto, CodigoDetalle);

//                dgrItemsModificados.DataSource = dtItemsModificados;
//            }
//            catch (Exception er)
            
//            {
//                return;
//            }
            
//        }

//       #endregion
//    }
//}
