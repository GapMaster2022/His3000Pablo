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
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Core.Datos;
using His.Parametros;
using System.IO;
using System.Diagnostics;

namespace CuentaPaciente
{
    public partial class frmPaquetesCtas : Form
    {
        Infragistics.Win.UltraWinGrid.UltraGrid ugrdCuen = new UltraGrid();
        private string fechaAltaIni;
        private string fechaAltaFin;
        private List<ESTADOS_CUENTA> listaEstadosCuenta;
        private List<ESTADOS_CUENTA> listaEstadosCuentaMenu;
        private List<ASEGURADORAS_EMPRESAS> listaTiposSeguros;
        DataTable ultimaAtencion = null;

        int contador = 1;

        int esto = 0;

        public frmPaquetesCtas()
        {
            InitializeComponent();
            cargarDatos();
            dtpFiltroHasta.Value = DateTime.Now.Date;
            toolStripButton6.Enabled = false;

        }
        public void cargarDatos()
        {
            toolStripButton4.Image = Archivo.imgSptOrganizar;
            toolStripButton6.Image = Archivo.imgOfficeExcel;
            toolStripButton5.Image = Archivo.ButtonRefresh;
            tsbDespacho.Image = Archivo.imgBtnFloppy;
            dtpFiltroHasta.Value = DateTime.Now.Date;
            toolStripButton3.Image = Archivo.imgBtnDocuments_open;
            toolStripbtnNuevo.Image = Archivo.imgBtnRestart;
            toolStripButton2.Image = Archivo.imgOfficeExcel;

            listaEstadosCuenta = NegCuentasPacientes.RecuperarEstadosCuenta();
            cmb_EstadoCuenta.DataSource = listaEstadosCuenta;
            cmb_EstadoCuenta.DisplayMember = "ESC_NOMBRE";
            cmb_EstadoCuenta.ValueMember = "ESC_CODIGO";
            cmb_EstadoCuenta.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_EstadoCuenta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb_EstadoCuenta.SelectedIndex = 1;

            // carga los tipos de seguros de las cuentas / Giovanny tapia / 06/09/2012

            listaTiposSeguros = NegCuentasPacientes.RecuperarCategoriasConvenios();

            cmbTipoCuenta.DataSource = listaTiposSeguros;
            cmbTipoCuenta.DisplayMember = "ASE_NOMBRE";
            cmbTipoCuenta.ValueMember = "ASE_CODIGO";
            cmbTipoCuenta.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbTipoCuenta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbTipoCuenta.Text = "IESS";

            listaEstadosCuentaMenu = NegCuentasPacientes.RecuperarEstadosCuenta();
            cmbEstado.ComboBox.DataSource = listaEstadosCuentaMenu;
            cmbEstado.ComboBox.DisplayMember = "ESC_NOMBRE";
            cmbEstado.ComboBox.ValueMember = "ESC_CODIGO";
            cmbEstado.ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbEstado.ComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbEstado.ComboBox.SelectedIndex = 1;
        }

        private void frmEstadoCuenta_Load(object sender, EventArgs e)
        {


        }

        private void toolStripbtnNuevo_Click(object sender, EventArgs e)
        {

            contador = 1;
            if (ultraTabControl1.SelectedTab != ultraTabControl1.Tabs["detalle"])
            {
                MessageBox.Show("Este proceso podria tardar un poco espere por favor....", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarGrid();
            }

            else
            {
                MessageBox.Show("Este proceso podria tardar un poco espere por favor....", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCuentas();
            }

        }
        public void cargarGrid()
        {
            fechaAltaIni = String.Format("{0:dd-MM-yyyy}", dtpFiltroDesde.Value) + " 00:00:01";
            fechaAltaFin = String.Format("{0:dd-MM-yyyy}", dtpFiltroHasta.Value) + " 23:59:59";

            if ((cmb_EstadoCuenta.Text) != "TODOS")
            {
                DataTable datos = NegCuentasPacientes.AtencionesCuentas
                (fechaAltaIni, fechaAltaFin, Convert.ToInt32(cmb_EstadoCuenta.SelectedValue.ToString()), this.txtPaciente.Text, this.txtHC.Text, txtAtencion.Text, Convert.ToInt16(this.cmbTipoCuenta.SelectedValue));

                ugrdCuenta.DataSource = datos;

                if (datos.Rows.Count != 0)

                    toolStripButton6.Enabled = true;
            }
            else
            {
                DataTable datos = NegCuentasPacientes.AtencionesCuentasTodos
                (fechaAltaIni, fechaAltaFin, Convert.ToInt32(cmb_EstadoCuenta.SelectedValue.ToString()), this.txtPaciente.Text, this.txtHC.Text, txtAtencion.Text, Convert.ToInt16(this.cmbTipoCuenta.SelectedValue));

                ugrdCuenta.DataSource = datos;

                if (datos.Rows.Count != 0)

                    toolStripButton6.Enabled = true;
            }




        }


        private void ultraCuentas_InitializeLayout_1(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void tsbDespacho_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Esta seguro que desea  cambiar  de  estado  " + cmbEstado.ComboBox.SelectedText.ToString(), "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int VerificaCambio;
                    Int16 contador = 0;
                    VerificaCambio = 0;

                    // VERIFICO SI EL ESTADO A CAMBIAR ES MENOR AL ESTADO DEL FILTRO / GIOVANNY TAPIA / 07/08/2012
                    //if (Convert.ToInt16(cmbEstado.ComboBox.SelectedValue) <= Convert.ToInt16(cmb_EstadoCuenta.SelectedValue))
                    //{
                    //    MessageBox.Show("No se puede cambiar el estado de una cuenta a un estado anterior...Por favor verificar..");
                    //    return;
                    //}

                    //INVOCO AL METODO DE NEGOCIO PARA SABER SI EL USUARIO ACTUAL TIENE LOS PERMISOS NECESARIOS PARA CAMBIAR DE ESTADO UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

                    ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];

                    foreach (var item in ugrdCuenta.Rows)
                    {
                        if ((bool)item.Cells["CHECKTRANS"].Value == true)
                        {

                            VerificaCambio = NegCuentasPacientes.PermisosActualizacionCuentas(His.Entidades.Clases.Sesion.codUsuario, Convert.ToInt16(item.Cells["ESC_CODIGO"].Value), Convert.ToInt16(cmbEstado.ComboBox.SelectedValue));
                            if (VerificaCambio == 0) // SI DEVUELVE 0 NO TIENE PERMISOS PARA CAMBIAR EL ESTADO / GIOVANNY TAPIA / 07/08/2012
                            {
                                MessageBox.Show("No tiene los permisos necesarios para realizar estos cambios.. Por favor verificar HC. " + item.Cells["HC"].Value.ToString() + " PACIENTE. " + item.Cells["PACIENTE"].Value.ToString());
                                return;
                            }

                        }
                    }
                    ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];

                    foreach (var item in ugrdCuenta.Rows)
                    {
                        if ((bool)item.Cells["CHECKTRANS"].Value == true)
                        {
                            NegCuentasPacientes.actualizarEstadoFactura(Convert.ToInt32(item.Cells["ATE_CODIGO"].Value), Convert.ToInt16(cmbEstado.ComboBox.SelectedValue.ToString()));
                            contador++;
                        }
                    }
                    if (contador > 0)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Seleccione una  cuenta para el  cambio de estado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            cargarGrid();
            CargarCuentas();



        }
        public void CargarCuentas()
        {

            fechaAltaIni = String.Format("{0:dd-MM-yyyy}", dtpFiltroDesde.Value) + " 00:00:01";
            fechaAltaFin = String.Format("{0:dd-MM-yyyy}", dtpFiltroHasta.Value) + " 23:59:59";

            DataTable datos = NegCuentasPacientes.Datos_reporte(fechaAltaIni, fechaAltaFin, Convert.ToInt32(cmb_EstadoCuenta.SelectedValue.ToString()));
            uGridCuentas.DataSource = datos;
            if (datos.Rows.Count != 0)

                toolStripButton6.Enabled = true;
        }

        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    CreateExcel(FindSavePath());
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message); }
            //finally { this.Cursor = Cursors.Default; }
        }
        private void CreateExcel(UltraGrid datosGrid, String myFilepath)
        {
            try
            {
                if (myFilepath != null)
                {


                    this.ultraGridExcelExporter1.Export(datosGrid, myFilepath);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + myFilepath);
                }

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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CargarCuentas();

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

            ////grid.Rows[currentRow + 1].Cells[grid.ActiveCell].Activate();
            //ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];
            //string cadenaSql = "";
            //try
            //{
            //    Int16 contador = 0;
            //    foreach (var item in ugrdCuenta.Rows)
            //    {
            //        if ((bool)item.Cells["CHECKTRANS"].Value == true)
            //        {
            //             cadenaSql = Convert.ToString(item.Cells["ATENCION"].Value).Trim()+","+cadenaSql;
            //            contador++;
            //        }
            //    }
            //    if (contador > 0)
            //    {
            //        frmExportarExel frm = new frmExportarExel(cadenaSql, contador);
            //        frm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Seleccione una  cuenta para exportar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }


            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //List<Int32> ListaAtencionesPaquete = new List<Int32>();
            //string Cadena = "(";
            //Int32 Contador = 1;
            //Int32 Seleccionados = 0;

            //ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];

            //foreach (var item in ugrdCuenta.Rows)
            //{
            //    if ((bool)item.Cells["CHECKTRANS"].Value == true)
            //    {
            //        Seleccionados++;
            //    }
            //}

            //foreach (var item1 in ugrdCuenta.Rows)
            //{
            //    if ((bool)item1.Cells["CHECKTRANS"].Value == true)
            //    {
            //        if (Contador == Seleccionados)
            //        {
            //            Cadena = Cadena + item1.Cells["ATE_CODIGO"].Value.ToString() + ")";
            //        }
            //        else
            //        {
            //            Cadena = Cadena + item1.Cells["ATE_CODIGO"].Value.ToString() + ",";
            //        }
            //    }
            //    Contador++;
            //}

            //if (Contador > 1)
            //{
            //    frmReporteDesgloseFactura Form = new frmReporteDesgloseFactura("", "", "", "", 0, "CUENTAS", Cadena, 0, "", "", 0);
            //    Form.ShowDialog();
            //}

            List<Int32> ListaAtenciones = new List<Int32>();

            foreach (var item1 in ugrdCuenta.Rows)
            {
                if ((bool)item1.Cells["CHECKTRANS"].Value == true)
                {

                    ListaAtenciones.Add(Convert.ToInt32(item1.Cells["ATE_CODIGO"].Value.ToString()));
                }
            }

            frmArchivoPlanoIess Forma = new frmArchivoPlanoIess(ListaAtenciones);
            Forma.Show();
            //toolStripButton4.Enabled = false;

        }

        private void uGridCuentas_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = uGridCuentas.DisplayLayout.Bands[0];
            uGridCuentas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            uGridCuentas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            uGridCuentas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            uGridCuentas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            uGridCuentas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            //Añado la columna de totales
            uGridCuentas.DisplayLayout.UseFixedHeaders = true;
            bandUno.Summaries.Clear();
            bandUno.SummaryFooterCaption = "Total Cuentas: ";
            bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
            bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.Blue;

            //suma de valor total de la cuenta 
            SummarySettings sumValor = bandUno.Summaries.Add("Valor Cuenta", SummaryType.Sum, bandUno.Columns["Valor Cuenta"]);
            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
            sumValor.DisplayFormat = "{0:#####.00}";
            sumValor.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;


        }

        private void ugrdCuenta_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {


            e.Layout.Bands[0].Columns["ESC_CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["PROCESOS"].Hidden = true;
            e.Layout.Bands[0].Columns["ATE_CODIGO"].Hidden = true;
            ////e.Layout.Bands[0].Columns["CUE_CODIGO"].Hidden = true;
            ////e.Layout.Bands[0].Columns["ADS_CODIGO"].Hidden = true;

            if (!ugrdCuenta.DisplayLayout.Bands[0].Columns.Exists("CHECKTRANS"))
            {
                ugrdCuenta.DisplayLayout.Bands[0].Columns.Add("CHECKTRANS", "");
                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DataType = typeof(bool);
                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                ugrdCuenta.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;

            }
            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Header.VisiblePosition = 0;
            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].CellActivation = Activation.AllowEdit;
            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DefaultCellValue = true;

            ugrdCuenta.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ugrdCuenta.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ugrdCuenta.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ugrdCuenta.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ugrdCuenta.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

        }

        private void chkSinFiltroFechas_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmb_EstadoCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void uGridCuentas_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            //e.Row.Appearance.BackColor = Color.Orange;


        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];
            string cadenaSql = "";
            try
            {
                Int16 contador = 0;
                foreach (var item in ugrdCuenta.Rows)
                {
                    if ((bool)item.Cells["CHECKTRANS"].Value == true)
                    {
                        cadenaSql = Convert.ToString(item.Cells["ATE_CODIGO"].Value).Trim();
                        contador++;
                    }
                }
                if (contador == 1)
                {
                    frmDetalleCuenta frm = new frmDetalleCuenta(Convert.ToInt32(cadenaSql));
                    frm.ShowDialog();
                    toolStripButton4.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Seleccione un solo  Item para ver  detalle ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ugrdCuenta_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            //if (e.Row.Cells.Exists("PROCESO"))
            //{
            //    e.Row.Cells["PROCESO"].Value = Archivo.imgCancelar;
            //}
            if (Convert.ToString(e.Row.Cells["PROCESOS"].Value) == "1")
            {


                e.Row.Appearance.BackColor = Color.Orange;

            }


            //string campo= e.Row.Cells[9].Value.ToString();
            //DataTable datos = NegAseguradoras.AtencionesValorTotal(campo.Trim());

            //e.Row.Cells["valor"].Value = datos.Rows[0]["valor"].ToString(); 


        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Esta seguro que desea procesar  " + cmbEstado.ComboBox.SelectedText.ToString(), "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Int16 contador = 0;
                    ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];
                    foreach (var item in ugrdCuenta.Rows)
                    {
                        if ((bool)item.Cells["CHECKTRANS"].Value == true)
                        {
                            NegCuentasPacientes.actualizarProseso(Convert.ToString(item.Cells["ATE_CODIGO"].Value), "1");
                            contador++;
                        }
                    }
                    if (contador > 0)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Seleccione una  cuenta ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            cargarGrid();



        }
        private DataTable GetAtenciones(string sqlCadena, int cont)
        {
            DataTable dtAtenciones = new DataTable("Atenciones");
            DataRow drRow = dtAtenciones.NewRow();

            dtAtenciones.Columns.Add(new DataColumn("codigo",
                Type.GetType("System.String")));

            string[] cadenaAtenciones = sqlCadena.Split(',');


            for (int i = 0; i < cont; i++)
            {
                drRow = dtAtenciones.NewRow();
                drRow["codigo"] = cadenaAtenciones[i];

                dtAtenciones.Rows.Add(drRow);


            }

            return dtAtenciones;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (ultraTabControl1.SelectedTab == ultraTabControl1.Tabs["detalle"])
            {
                try
                {
                    CreateExcel(uGridCuentas, FindSavePath());
                }
                catch (Exception esx)
                {

                }
            }
            else
            {
                //grid.Rows[currentRow + 1].Cells[grid.ActiveCell].Activate();
                ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];
                string cadenaSql = "";
                try
                {
                    Int16 contador = 0;
                    foreach (var item in ugrdCuenta.Rows)
                    {
                        if ((bool)item.Cells["CHECKTRANS"].Value == true)
                        {
                            cadenaSql = Convert.ToString(item.Cells["ATENCION"].Value).Trim() + "," + cadenaSql;
                            contador++;
                        }
                    }
                    if (contador > 0)
                    {
                        cadenaSql = cadenaSql + "1";

                        StringWriter swStringWriter = new StringWriter();
                        DataTable dtatenciones = GetAtenciones(cadenaSql, contador);
                        dtatenciones.WriteXml(swStringWriter);

                        string stratenciones = swStringWriter.ToString();
                        DataTable datos = NegCuentasPacientes.DatosExportarAseguradora(stratenciones, Convert.ToInt32(cmbTipoCuenta.SelectedValue.ToString()));

                        ultraGrid1.DataSource = datos;
                        CreateExcel(ultraGrid1, FindSavePath());
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una  cuenta para exportar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void grpFechas_Click(object sender, EventArgs e)
        {

        }

        private void ugrdCuenta_FilterRow(object sender, FilterRowEventArgs e)
        {



        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void chk_todos_CheckedChanged(object sender, EventArgs e)
        {
            int Fila = 0;
            ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];

            foreach (var item in ugrdCuenta.Rows)
            {
                //if ((bool)item.Cells["CHECKTRANS"].Value == true)
                //{
                if (chk_todos.Checked == true)
                {
                    ugrdCuenta.Rows[Fila].Cells["CHECKTRANS"].Value = true;
                }
                else
                {
                    ugrdCuenta.Rows[Fila].Cells["CHECKTRANS"].Value = false;
                }
                //}
                Fila++;
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            frmHonorariosMedicosRpt Form = new frmHonorariosMedicosRpt();
            Form.Show();

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }

        private void btnGenraIsspol_Click(object sender, EventArgs e)
        {
            List<Int32> ListaAtenciones = new List<Int32>();
            int contador = 0;
            foreach (var item1 in ugrdCuenta.Rows)
            {
                if ((bool)item1.Cells["CHECKTRANS"].Value == true)
                {
                    contador++;
                    ListaAtenciones.Add(Convert.ToInt32(item1.Cells["ATE_CODIGO"].Value.ToString()));
                }
            }
            string periodo = txtPeriodo.Text + "-" + txtMes.Text;
            frmArchivoPlanoIess Forma = new frmArchivoPlanoIess(ListaAtenciones, "ISSPOL", contador, periodo);
            Forma.Show();
        }

        private void txtPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void txtMes_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
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
//using His.Negocio;
//using Infragistics.Win.UltraWinGrid;
//using Recursos;
//using System.Data.Objects;
//using System.Data.Objects.DataClasses;
//using Core.Datos;
//using His.Parametros;
//using System.IO;
//using System.Diagnostics;

//namespace CuentaPaciente
//{
//    public partial class frmPaquetesCtas : Form
//    {
//        Infragistics.Win.UltraWinGrid.UltraGrid ugrdCuen = new UltraGrid();
//        private string fechaAltaIni;
//        private string fechaAltaFin;
//        private List<ESTADOS_CUENTA> listaEstadosCuenta;
//        private List<ESTADOS_CUENTA> listaEstadosCuentaMenu;
//        private List<ASEGURADORAS_EMPRESAS> listaTiposSeguros;

//        int contador = 1;
       
//        int esto=0;

//            public frmPaquetesCtas()
//        {
//            InitializeComponent();
//            cargarDatos();
//            dtpFiltroHasta.Value = DateTime.Now.Date;
//            toolStripButton6.Enabled = false;
            
//        }
//            public void cargarDatos()
//            {
//                toolStripButton4.Image = Archivo.imgSptOrganizar;
//                toolStripButton6.Image = Archivo.imgOfficeExcel;
//                toolStripButton5.Image = Archivo.ButtonRefresh;
//                tsbDespacho.Image = Archivo.imgBtnFloppy;
//                dtpFiltroHasta.Value = DateTime.Now.Date;
//                toolStripButton3.Image = Archivo.imgBtnDocuments_open;
//                toolStripbtnNuevo.Image = Archivo.imgBtnRestart;
//                toolStripButton2.Image = Archivo.imgOfficeExcel;

//                listaEstadosCuenta = NegCuentasPacientes.RecuperarEstadosCuenta();
//                cmb_EstadoCuenta.DataSource = listaEstadosCuenta;
//                cmb_EstadoCuenta.DisplayMember = "ESC_NOMBRE";
//                cmb_EstadoCuenta.ValueMember = "ESC_CODIGO";
//                cmb_EstadoCuenta.AutoCompleteSource = AutoCompleteSource.ListItems;
//                cmb_EstadoCuenta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
//                cmb_EstadoCuenta.SelectedIndex = 1;

//                // carga los tipos de seguros de las cuentas / Giovanny tapia / 06/09/2012

//                listaTiposSeguros = NegCuentasPacientes.RecuperarCategoriasConvenios();

//                cmbTipoCuenta.DataSource = listaTiposSeguros;
//                cmbTipoCuenta.DisplayMember = "ASE_NOMBRE";
//                cmbTipoCuenta.ValueMember = "ASE_CODIGO";
//                cmbTipoCuenta.AutoCompleteSource = AutoCompleteSource.ListItems;
//                cmbTipoCuenta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
//                cmbTipoCuenta.Text = "IESS";

//                listaEstadosCuentaMenu = NegCuentasPacientes.RecuperarEstadosCuenta();
//                cmbEstado.ComboBox.DataSource = listaEstadosCuentaMenu;
//                cmbEstado.ComboBox.DisplayMember = "ESC_NOMBRE";
//                cmbEstado.ComboBox.ValueMember = "ESC_CODIGO";
//                cmbEstado.ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
//                cmbEstado.ComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
//                cmbEstado.ComboBox.SelectedIndex = 1;
//            }

//        private void frmEstadoCuenta_Load(object sender, EventArgs e)
//            {
                

//        }

//        private void toolStripbtnNuevo_Click(object sender, EventArgs e)
//        {

//            contador = 1;
//            if (ultraTabControl1.SelectedTab != ultraTabControl1.Tabs["detalle"])
//            {
//                MessageBox.Show("Este proceso podria tardar un poco espere por favor....", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                cargarGrid();
//            }

//            else
//            {
//                MessageBox.Show("Este proceso podria tardar un poco espere por favor....", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                CargarCuentas();
//            }

//        }
//        public void cargarGrid()
//        {
//            fechaAltaIni = String.Format("{0:dd-MM-yyyy}", dtpFiltroDesde.Value) + " 00:00:01";
//            fechaAltaFin = String.Format("{0:dd-MM-yyyy}", dtpFiltroHasta.Value) + " 23:59:59";

//            if ((cmb_EstadoCuenta.Text) != "TODOS")
//            {
//                DataTable datos = NegCuentasPacientes.AtencionesCuentas
//                (fechaAltaIni, fechaAltaFin, Convert.ToInt32(cmb_EstadoCuenta.SelectedValue.ToString()), this.txtPaciente.Text, this.txtHC.Text, txtAtencion.Text, Convert.ToInt16(this.cmbTipoCuenta.SelectedValue));

//                ugrdCuenta.DataSource = datos;

//                if (datos.Rows.Count != 0)

//                    toolStripButton6.Enabled = true;
//            }
//            else
//            {
//                DataTable datos = NegCuentasPacientes.AtencionesCuentasTodos
//                (fechaAltaIni, fechaAltaFin, Convert.ToInt32(cmb_EstadoCuenta.SelectedValue.ToString()), this.txtPaciente.Text, this.txtHC.Text, txtAtencion.Text,Convert.ToInt16 (this.cmbTipoCuenta.SelectedValue));

//                ugrdCuenta.DataSource = datos;

//                if (datos.Rows.Count != 0)

//                    toolStripButton6.Enabled = true;
//            }

                

           
//        }
      

//        private void ultraCuentas_InitializeLayout_1(object sender, InitializeLayoutEventArgs e)
//        {

//        }

//        private void tsbDespacho_Click(object sender, EventArgs e)
//        {
           
//            if (MessageBox.Show("Esta seguro que desea  cambiar  de  estado  " + cmbEstado.ComboBox.SelectedText.ToString(), "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//            {
//                try
//                {
//                    int VerificaCambio;
//                    Int16 contador = 0;
//                    VerificaCambio = 0;

//                    // VERIFICO SI EL ESTADO A CAMBIAR ES MENOR AL ESTADO DEL FILTRO / GIOVANNY TAPIA / 07/08/2012
//                    //if (Convert.ToInt16(cmbEstado.ComboBox.SelectedValue) <= Convert.ToInt16(cmb_EstadoCuenta.SelectedValue))
//                    //{
//                    //    MessageBox.Show("No se puede cambiar el estado de una cuenta a un estado anterior...Por favor verificar..");
//                    //    return;
//                    //}

//                    //INVOCO AL METODO DE NEGOCIO PARA SABER SI EL USUARIO ACTUAL TIENE LOS PERMISOS NECESARIOS PARA CAMBIAR DE ESTADO UNA CUENTA / GIOVANNY TAPIA / 07/08/2012

//                    ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];

//                    foreach (var item in ugrdCuenta.Rows)
//                    {
//                        if ((bool)item.Cells["CHECKTRANS"].Value == true)
//                        {

//                            VerificaCambio = NegCuentasPacientes.PermisosActualizacionCuentas(His.Entidades.Clases.Sesion.codUsuario, Convert.ToInt16(item.Cells["ESC_CODIGO"].Value), Convert.ToInt16(cmbEstado.ComboBox.SelectedValue));
//                            if (VerificaCambio == 0) // SI DEVUELVE 0 NO TIENE PERMISOS PARA CAMBIAR EL ESTADO / GIOVANNY TAPIA / 07/08/2012
//                            {
//                                MessageBox.Show("No tiene los permisos necesarios para realizar estos cambios.. Por favor verificar HC. " + item.Cells["HC"].Value.ToString() + " PACIENTE. " + item.Cells["PACIENTE"].Value.ToString());
//                                return;
//                            }

//                        }
//                    }
//                    ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];

//                    foreach (var item in ugrdCuenta.Rows)
//                    {
//                        if ((bool)item.Cells["CHECKTRANS"].Value == true)
//                        {
//                            NegCuentasPacientes.actualizarEstadoFactura(Convert.ToInt32(item.Cells["ATE_CODIGO"].Value), Convert.ToInt16(cmbEstado.ComboBox.SelectedValue.ToString()));
//                            contador++;
//                        }
//                    }
//                    if (contador > 0)
//                    {

//                    }
//                    else
//                    {
//                        MessageBox.Show("Seleccione una  cuenta para el  cambio de estado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                }
//                catch (Exception err)
//                {
//                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }

//            cargarGrid();
//            CargarCuentas();


         
//        }
//        public void CargarCuentas()
//        {

//            fechaAltaIni = String.Format("{0:dd-MM-yyyy}", dtpFiltroDesde.Value) + " 00:00:01";
//            fechaAltaFin = String.Format("{0:dd-MM-yyyy}", dtpFiltroHasta.Value) + " 23:59:59";

//            DataTable datos = NegCuentasPacientes.Datos_reporte(fechaAltaIni, fechaAltaFin, Convert.ToInt32(cmb_EstadoCuenta.SelectedValue.ToString()));
//            uGridCuentas.DataSource = datos;
//            if (datos.Rows.Count != 0)

//                toolStripButton6.Enabled = true;
//        }

//        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
//        {

//        }

//        private void toolStripButton2_Click(object sender, EventArgs e)
//        {
//            //try
//            //{
//            //    CreateExcel(FindSavePath());
//            //}
//            //catch (Exception ex) { MessageBox.Show(ex.Message); }
//            //finally { this.Cursor = Cursors.Default; }
//        }
//        private void CreateExcel(UltraGrid datosGrid, String myFilepath)
//        {
//            try
//            {
//                if (myFilepath != null)
//                {


//                    this.ultraGridExcelExporter1.Export(datosGrid, myFilepath);
//                    MessageBox.Show("Se termino de exportar el grid en el archivo " + myFilepath);
//                }
               
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
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

//        private void toolStripButton3_Click(object sender, EventArgs e)
//        {
//            CargarCuentas();

//        }

//        private void toolStripButton4_Click(object sender, EventArgs e)
//        {

//            ////grid.Rows[currentRow + 1].Cells[grid.ActiveCell].Activate();
//            //ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];
//            //string cadenaSql = "";
//            //try
//            //{
//            //    Int16 contador = 0;
//            //    foreach (var item in ugrdCuenta.Rows)
//            //    {
//            //        if ((bool)item.Cells["CHECKTRANS"].Value == true)
//            //        {
//            //             cadenaSql = Convert.ToString(item.Cells["ATENCION"].Value).Trim()+","+cadenaSql;
//            //            contador++;
//            //        }
//            //    }
//            //    if (contador > 0)
//            //    {
//            //        frmExportarExel frm = new frmExportarExel(cadenaSql, contador);
//            //        frm.ShowDialog();
//            //    }
//            //    else
//            //    {
//            //        MessageBox.Show("Seleccione una  cuenta para exportar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            //    }


//            //}
//            //catch (Exception err)
//            //{
//            //    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            //}

//            //List<Int32> ListaAtencionesPaquete = new List<Int32>();
//            //string Cadena = "(";
//            //Int32 Contador = 1;
//            //Int32 Seleccionados = 0;

//            //ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];

//            //foreach (var item in ugrdCuenta.Rows)
//            //{
//            //    if ((bool)item.Cells["CHECKTRANS"].Value == true)
//            //    {
//            //        Seleccionados++;
//            //    }
//            //}

//            //foreach (var item1 in ugrdCuenta.Rows)
//            //{
//            //    if ((bool)item1.Cells["CHECKTRANS"].Value == true)
//            //    {
//            //        if (Contador == Seleccionados)
//            //        {
//            //            Cadena = Cadena + item1.Cells["ATE_CODIGO"].Value.ToString() + ")";
//            //        }
//            //        else
//            //        {
//            //            Cadena = Cadena + item1.Cells["ATE_CODIGO"].Value.ToString() + ",";
//            //        }
//            //    }
//            //    Contador++;
//            //}

//            //if (Contador > 1)
//            //{
//            //    frmReporteDesgloseFactura Form = new frmReporteDesgloseFactura("", "", "", "", 0, "CUENTAS", Cadena, 0, "", "", 0);
//            //    Form.ShowDialog();
//            //}

//            List<Int32> ListaAtenciones = new List<Int32>();

//            foreach (var item1 in ugrdCuenta.Rows)
//            {
//                if ((bool)item1.Cells["CHECKTRANS"].Value == true)
//                {

//                    ListaAtenciones.Add(Convert.ToInt32(item1.Cells["ATE_CODIGO"].Value.ToString()));
//                }
//            }

//            frmArchivoPlanoIess Forma = new frmArchivoPlanoIess(ListaAtenciones);
//            Forma.Show();

//        }

//        private void uGridCuentas_InitializeLayout(object sender, InitializeLayoutEventArgs e)
//        {
//            UltraGridBand bandUno = uGridCuentas.DisplayLayout.Bands[0];
//            uGridCuentas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
//            uGridCuentas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
//            uGridCuentas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
//            uGridCuentas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
//            uGridCuentas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

//            //Añado la columna de totales
//            uGridCuentas.DisplayLayout.UseFixedHeaders = true;
//            bandUno.Summaries.Clear();
//            bandUno.SummaryFooterCaption = "Total Cuentas: ";
//            bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
//            bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
//            bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.Blue;

//            //suma de valor total de la cuenta 
//            SummarySettings sumValor = bandUno.Summaries.Add("Valor Cuenta", SummaryType.Sum, bandUno.Columns["Valor Cuenta"]);
//            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
//            sumValor.DisplayFormat = "{0:#####.00}";
//            sumValor.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
         

//        }

//        private void ugrdCuenta_InitializeLayout(object sender, InitializeLayoutEventArgs e)
//        {
         

//            e.Layout.Bands[0].Columns["ESC_CODIGO"].Hidden = true;
//            e.Layout.Bands[0].Columns["PROCESOS"].Hidden = true;
//            e.Layout.Bands[0].Columns["ATE_CODIGO"].Hidden = true;
//            ////e.Layout.Bands[0].Columns["CUE_CODIGO"].Hidden = true;
//            ////e.Layout.Bands[0].Columns["ADS_CODIGO"].Hidden = true;

//            if (!ugrdCuenta.DisplayLayout.Bands[0].Columns.Exists("CHECKTRANS"))
//            {
//                ugrdCuenta.DisplayLayout.Bands[0].Columns.Add("CHECKTRANS", "");
//                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DataType = typeof(bool);
//                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
//                ugrdCuenta.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;

//            }
//            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Header.VisiblePosition = 0;
//            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].CellActivation = Activation.AllowEdit;
//            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DefaultCellValue = true;

//            ugrdCuenta.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
//            ugrdCuenta.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
//            ugrdCuenta.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
//            ugrdCuenta.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
//            ugrdCuenta.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
        
//        }

//        private void chkSinFiltroFechas_CheckedChanged(object sender, EventArgs e)
//        {

//        }

//        private void cmb_EstadoCuenta_SelectedIndexChanged(object sender, EventArgs e)
//        {
         
//        }

//        private void uGridCuentas_InitializeRow(object sender, InitializeRowEventArgs e)
//        {
//            //e.Row.Appearance.BackColor = Color.Orange;


//        }

//        private void toolStripButton3_Click_1(object sender, EventArgs e)
//        {
//            ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];
//            string cadenaSql = "";
//            try
//            {
//                Int16 contador = 0;
//                foreach (var item in ugrdCuenta.Rows)
//                {
//                    if ((bool)item.Cells["CHECKTRANS"].Value == true)
//                    {
//                        cadenaSql = Convert.ToString(item.Cells["ATE_CODIGO"].Value).Trim();
//                        contador++;
//                    }
//                }
//                if (contador == 1)
//                {
//                    frmDetalleCuenta frm = new frmDetalleCuenta(Convert.ToInt32(cadenaSql));
//                    frm.ShowDialog();
//                }
//                else
//                {
//                    MessageBox.Show("Seleccione un solo  Item para ver  detalle ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }


//            }
//            catch (Exception err)
//            {
//                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//        }

//        private void ugrdCuenta_InitializeRow(object sender, InitializeRowEventArgs e)
//        {
//            //if (e.Row.Cells.Exists("PROCESO"))
//            //{
//            //    e.Row.Cells["PROCESO"].Value = Archivo.imgCancelar;
//            //}
//            if (Convert.ToString(e.Row.Cells["PROCESOS"].Value) == "1")
//            {


//                e.Row.Appearance.BackColor = Color.Orange;

//            }
         

//            //string campo= e.Row.Cells[9].Value.ToString();
//            //DataTable datos = NegAseguradoras.AtencionesValorTotal(campo.Trim());

//            //e.Row.Cells["valor"].Value = datos.Rows[0]["valor"].ToString(); 
          
            
//        }

//        private void toolStripButton5_Click(object sender, EventArgs e)
//        {

//            if (MessageBox.Show("Esta seguro que desea procesar  " + cmbEstado.ComboBox.SelectedText.ToString(), "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
//            {
//                try
//                {
//                    Int16 contador = 0;
//                    ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];
//                    foreach (var item in ugrdCuenta.Rows)
//                    {
//                        if ((bool)item.Cells["CHECKTRANS"].Value == true )
//                        {
//                            NegCuentasPacientes.actualizarProseso(Convert.ToString(item.Cells["ATE_CODIGO"].Value), "1");
//                            contador++;
//                        }
//                    }
//                    if (contador > 0)
//                    {

//                    }
//                    else
//                    {
//                        MessageBox.Show("Seleccione una  cuenta ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                }
//                catch (Exception err)
//                {
//                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }

//            cargarGrid();
           


//        }
//        private DataTable GetAtenciones(string sqlCadena,int cont)
//        {
//            DataTable dtAtenciones = new DataTable("Atenciones");
//            DataRow drRow = dtAtenciones.NewRow();

//            dtAtenciones.Columns.Add(new DataColumn("codigo",
//                Type.GetType("System.String")));

//            string[] cadenaAtenciones = sqlCadena.Split(',');


//            for (int i = 0; i < cont; i++)
//            {
//                drRow = dtAtenciones.NewRow();
//                drRow["codigo"] = cadenaAtenciones[i];

//                dtAtenciones.Rows.Add(drRow);


//            }

//            return dtAtenciones;
//        }

//        private void toolStripButton6_Click(object sender, EventArgs e)
//        {
//            if (ultraTabControl1.SelectedTab == ultraTabControl1.Tabs["detalle"])
//            {
//                try
//                {
//                    CreateExcel(uGridCuentas, FindSavePath());
//                }
//                catch (Exception esx)
//                {

//                }
//            }
//            else
//            {
//                //grid.Rows[currentRow + 1].Cells[grid.ActiveCell].Activate();
//                ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];
//                string cadenaSql = "";
//                try
//                {
//                    Int16 contador = 0;
//                    foreach (var item in ugrdCuenta.Rows)
//                    {
//                        if ((bool)item.Cells["CHECKTRANS"].Value == true)
//                        {
//                            cadenaSql = Convert.ToString(item.Cells["ATENCION"].Value).Trim() + "," + cadenaSql;
//                            contador++;
//                        }
//                    }
//                    if (contador > 0)
//                    {
//                        cadenaSql = cadenaSql + "1";

//                        StringWriter swStringWriter = new StringWriter();
//                        DataTable dtatenciones = GetAtenciones(cadenaSql, contador);
//                        dtatenciones.WriteXml(swStringWriter);

//                        string stratenciones = swStringWriter.ToString();
//                        DataTable datos = NegCuentasPacientes.DatosExportarAseguradora(stratenciones, Convert.ToInt32(cmbTipoCuenta.SelectedValue.ToString()));

//                        ultraGrid1.DataSource = datos;
//                        CreateExcel(ultraGrid1, FindSavePath());
//                    }
//                    else
//                    {
//                        MessageBox.Show("Seleccione una  cuenta para exportar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }


//                }
//                catch (Exception err)
//                {
//                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void grpFechas_Click(object sender, EventArgs e)
//        {

//        }

//        private void ugrdCuenta_FilterRow(object sender, FilterRowEventArgs e)
//        {

                  
         
//        }

//        private void toolStripLabel1_Click(object sender, EventArgs e)
//        {

//        }

//        private void chk_todos_CheckedChanged(object sender, EventArgs e)
//        {
//            int Fila = 0; 
//            ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];

//            foreach (var item in ugrdCuenta.Rows)
//            {
//                //if ((bool)item.Cells["CHECKTRANS"].Value == true)
//                //{
//                if (chk_todos.Checked == true)
//                {
//                    ugrdCuenta.Rows[Fila].Cells["CHECKTRANS"].Value = true;
//                }
//                else 
//                {
//                    ugrdCuenta.Rows[Fila].Cells["CHECKTRANS"].Value = false;
//                }
//                //}
//                Fila++;
//            }
//        }

//        private void toolStripButton7_Click(object sender, EventArgs e)
//        {
//            frmHonorariosMedicosRpt Form = new frmHonorariosMedicosRpt();
//            Form.Show();

//        }
//    }
//}
