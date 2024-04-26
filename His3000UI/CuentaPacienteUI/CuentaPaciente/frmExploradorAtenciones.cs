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
using Infragistics.Win.UltraWinGrid;
using Recursos;

using System.Diagnostics;

namespace CuentaPaciente
{
    public partial class frmExploradorAtenciones : Form
    {
        #region variables
        
        PEDIDOS_AREAS areaPedido;
        bool iniGrid;
        string valorGridAnt; //valor del ultimo registro ingresado en una fila de la grilla
        Color[] pColor;
        byte ranColor;
        bool pintarCeldas;
        Int16 pedidosPendientes;
        Int16 pedidosAtendidos;
        enum sAccion { Inicial, Actualizar};
        sAccion accion;
        List<TIPO_EMPRESA> tipoEmpresa = new List<TIPO_EMPRESA>();
        //private string fechaCreacionIni;
        //private string fechaCreacionFin;
        //private string fechaIngresoIni;
        //private string fechaIngresoFin;
        private string fechaAltaIni;
        private string fechaAltaFin;
        private string atencionActiva;
        private string codigoMedico;
        private string codigoAseguradoraEmpresa;
        private int codigoEsatoCuenta;

        private List<PACIENTES_VISTA> pacientesVistaLista; 


        #endregion

        #region propiedades

        //public PEDIDOS_AREAS AreaPedido
        //{
        //    get { return areaPedido; }
        //    set { areaPedido = value; }
        //}

        #endregion

        #region constructor
        public frmExploradorAtenciones()
        {
            InitializeComponent();
        }

        #endregion

        #region metodos
        private void cargarControles()
        {
            //
            pColor =mColor();
            //ranColor = new Random(DateTime.Now.Millisecond);
            //
            btnActualizar.Image = Archivo.imgBtnRestart;
            btnGenerar.Image = Archivo.imgOfficeExcel;
            btnCancelar.Image = Archivo.imgCancelar1;
            btnSalir.Image = Archivo.imgBtnSalir32;
           
            //cargo los valores por defecto de las fechas
            dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", "01/" + DateTime.Now.Month + "/" + (DateTime.Now.Year).ToString()));
            dtpFiltroHasta.Value = DateTime.Now;

            cmbConvenioPago.DataSource = tipoEmpresa;
            cmbConvenioPago.DisplayMember = "TE_DESCRIPCION";
            cmbConvenioPago.ValueMember = "TE_CODIGO";
            cmbConvenioPago.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbConvenioPago.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
        #endregion

        #region metodos de la grilla
        private void cargarGrilla()
        {
            
        }
        
        #endregion

        #region eventos
        private void frmExplorardorPedidos_Load(object sender, EventArgs e)
        {
            try
            {
                tipoEmpresa = NegAseguradoras.ListaTiposEmpresa();
                //inicializo variables
                accion = sAccion.Inicial;
                cargarControles();
                cargarGrilla();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uGridPedidos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["ESC_CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["CUE_CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["ADS_CODIGO"].Hidden = true;
            uGridCuentas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            uGridCuentas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            uGridCuentas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            uGridCuentas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            uGridCuentas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
           

        //    if (!uGridCuentas.DisplayLayout.Bands[0].Columns.Exists("CHECKTRANS"))
        //    {
        //        uGridCuentas.DisplayLayout.Bands[0].Columns.Add("CHECKTRANS", "");
        //        uGridCuentas.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DataType = typeof(bool);
        //        uGridCuentas.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
        //        uGridCuentas.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
        //        iniGrid = true;
        //    }
        //    uGridCuentas.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Header.VisiblePosition = 0;
        //    uGridCuentas.DisplayLayout.Bands[0].Columns["CHECKTRANS"].CellActivation = Activation.AllowEdit;
        //    uGridCuentas.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DefaultCellValue = true;
        }

        #endregion

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                accion = sAccion.Actualizar;
                cargarGrilla();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void lLblTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach(var item in uGridCuentas.Rows)
            {
                item.Cells["CHECKTRANS"].Value = true;
            }
        }

        private void lLblNinguno_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (var item in uGridCuentas.Rows)
            {
                item.Cells["CHECKTRANS"].Value = false;
            }
        }

        private void toolStripButtonExportar_Click(object sender, EventArgs e)
        {
            try
            {
                CreateExcel(FindSavePath());
            }
            catch (Exception ex){MessageBox.Show(ex.Message);}
            finally{this.Cursor = Cursors.Default;}
        }
        /// <summary>
        /// Crea el archivo de Excel usando el directorio q elige el usuario
        /// </summary>
        /// <param name="myFilepath"></param>
        private void CreateExcel(String myFilepath)
        {
            try
            {
                if (myFilepath != null)
                {
                    this.ultraGridExcelExporter1.Export(uGridCuentas, myFilepath);
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

        private void tsbDespacho_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 contador = 0;
                foreach (var item in uGridCuentas.Rows)
                {
                    if ((bool)item.Cells["CHECKTRANS"].Value == true)
                    {
                        NegPedidos.actualizarEstadoPedido(Convert.ToInt32(item.Cells["PED_CODIGO"].Value), 2);
                        contador++;
                    }
                }
                if (contador > 0)
                {
                    cargarGrilla();
                }
                else
                {
                    MessageBox.Show("Seleccione un Paciente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbAnular_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in uGridCuentas.Rows)
                {
                    if ((bool)item.Cells["CHECKTRANS"].Value == true)
                    {
                        NegPedidos.actualizarEstadoPedido((int)item.Cells["PED_CODIGO"].Value, 3);
                    }
                }
                cargarGrilla();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Color[] mColor (){
            Color[] color = new Color[]{
            Color.Aqua,
            Color.AliceBlue,
            Color.AntiqueWhite,
            Color.CadetBlue,
            Color.Aquamarine,
            Color.Azure,
            Color.BurlyWood,
            Color.DarkGray,
            Color.Beige,
            Color.DarkKhaki,
            Color.Bisque,
            Color.Gainsboro,
            Color.GhostWhite,
            Color.Khaki,
            Color.Lavender,
            Color.LavenderBlush,
            Color.LightCyan,
            Color.LightGoldenrodYellow,
            Color.LightGray,
            Color.LightGreen,
            Color.LightPink,
            Color.LightSalmon,
            Color.LightSeaGreen,
            Color.LightSkyBlue,
            Color.LightSlateGray,
            Color.LightSteelBlue,
            Color.LightYellow,
            Color.MintCream,
            Color.MistyRose,
            Color.Moccasin,
            Color.PaleGreen,
            Color.PaleTurquoise,
            Color.PaleVioletRed,
            Color.PapayaWhip,
            Color.PeachPuff,
            Color.Peru,
            Color.Pink,
            Color.Plum,
            Color.PowderBlue,
            Color.Salmon,
            Color.SandyBrown,
            Color.SeaGreen,
            Color.SeaShell,
            Color.Sienna,
            Color.Silver,
            Color.SkyBlue,
            Color.SlateBlue,
            Color.SlateGray,
            Color.Snow,
            Color.BlanchedAlmond,
            Color.Blue,
            Color.BlueViolet,
            Color.Brown,
            Color.Chartreuse,
            Color.Chocolate,
            Color.Coral,
            Color.CornflowerBlue,
            Color.Cornsilk,
            Color.Crimson,
            Color.Cyan,
            Color.DarkBlue,
            Color.DarkCyan,
            Color.DarkGoldenrod,
            Color.DarkGreen,
            Color.DarkMagenta,
            Color.DarkOliveGreen,
            Color.DarkOrange,
            Color.DarkOrchid,
            Color.DarkRed,
            Color.DarkSalmon,
            Color.DarkSeaGreen,
            Color.DarkSlateBlue,
            Color.DarkSlateGray,
            Color.DarkTurquoise,
            Color.DarkViolet,
            Color.DeepPink,
            Color.DeepSkyBlue,
            Color.DimGray,
            Color.DodgerBlue,
            Color.Firebrick,
            Color.FloralWhite,
            Color.ForestGreen,
            Color.Fuchsia,
            Color.Gold,
            Color.Goldenrod,
            Color.Gray,
            Color.Green,
            Color.GreenYellow,
            Color.Honeydew,
            Color.HotPink,
            Color.IndianRed,
            Color.Indigo,
            Color.Ivory,
            Color.LawnGreen,
            Color.LemonChiffon,
            Color.LightBlue,
            Color.LightCoral,
            Color.Lime,
            Color.LimeGreen,
            Color.Linen,
            Color.Magenta,
            Color.Maroon,
            Color.MediumAquamarine,
            Color.MediumBlue,
            Color.MediumOrchid,
            Color.MediumPurple,
            Color.MediumSeaGreen,
            Color.MediumSlateBlue,
            Color.MediumSpringGreen,
            Color.MediumTurquoise,
            Color.MediumVioletRed,
            Color.MidnightBlue,
            Color.NavajoWhite,
            Color.Navy,
            Color.OldLace,
            Color.Olive,
            Color.OliveDrab,
            Color.Orange,
            Color.OrangeRed,
            Color.Orchid,
            Color.PaleGoldenrod,
            Color.Purple,
            Color.Red,
            Color.RosyBrown,
            Color.RoyalBlue,
            Color.SaddleBrown,
            Color.SpringGreen,
            Color.SteelBlue,
            Color.Tan,
            Color.Teal,
            Color.Thistle,
            Color.Tomato,
            Color.Transparent,
            Color.Turquoise,
            Color.Violet,
            Color.Wheat,
            Color.White,
            Color.WhiteSmoke,
            Color.Yellow,
            Color.YellowGreen,
            Color.Black
            };
            return color;   
        }



        private void uGridPedidos_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                //if (pintarCeldas == false)
                //    return;
 
                //if (optOrdenar.Items[0].CheckState == CheckState.Checked)
                //{
                //    if (e.Row.Cells[0].Value.ToString() != valorGridAnt)
                //    {
                //        ranColor++;
                //        e.Row.Appearance.BackColor = pColor[ranColor];
                //        valorGridAnt = e.Row.Cells[0].Value.ToString();
                //    }
                //    else
                //        e.Row.Appearance.BackColor = pColor[ranColor];
                //}
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void tsbAdjuntar_Click(object sender, EventArgs e)
        {
            try
            {
                if (uGridCuentas.ActiveRow != null)
                {
                    var expArchivos = new GeneralApp.ControlesWinForms.ClienteFTPVista(0);
                    expArchivos.CarpetaServidor = uGridCuentas.ActiveRow.Cells["PED_CODIGO"].Text;
                    expArchivos.Modo = "Pedidos";
                    expArchivos.ShowDialog();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbComentarios_Click(object sender, EventArgs e)
        {
            try
            {
                if (uGridCuentas.ActiveRow != null)
                {
                    //frmComentarios ventana = new frmComentarios();
                    //ventana.Show();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonGuardar_Click(object sender, EventArgs e)
        {

        }

        private void uGridPedidos_Click(object sender, EventArgs e)
        {
            try
            {
                if (uGridCuentas.ActiveCell != null)
                {
                    if (uGridCuentas.ActiveCell.Column.Header.Caption == "")
                    {

                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            fechaAltaIni = String.Format("{0:dd-MM-yyyy}", dtpFiltroDesde.Value) + " 00:00:01";
            fechaAltaFin = String.Format("{0:dd-MM-yyyy}", dtpFiltroHasta.Value) + " 23:59:59";

            DataTable datos =  NegCuentasPacientes.Datos_reporte(fechaAltaIni, fechaAltaFin,3);
               uGridCuentas.DataSource = datos;

          
        }

        //private void activarFiltros()
        //{
        //    try
        //    {
        //        //Filtro por Fechas
        //        fechaCreacionIni = null;
        //        fechaCreacionFin = null;
        //        fechaIngresoIni = null;
        //        fechaIngresoFin = null;
        //        fechaAltaIni = null;
        //        fechaAltaFin = null;
        //        if (rbtCreacionPaciente.Checked == true)
        //        {
        //            fechaCreacionIni = String.Format("{0:yyyy-MM-dd}", dtpFiltroDesde.Value) + " 00:00:01";
        //            fechaCreacionFin = String.Format("{0:yyyy-MM-dd}", dtpFiltroHasta.Value) + " 23:59:59";
        //        }
        //        else if (rbtIngresoPaciente.Checked == true)
        //        {
        //            fechaIngresoIni = String.Format("{0:yyyy/MM/dd}", dtpFiltroDesde.Value);
        //            fechaIngresoFin = String.Format("{0:yyyy/MM/dd}", dtpFiltroHasta.Value);
        //        }
        //        else if (rbtAltaPaciente.Checked == true)
        //        {
        //            fechaAltaIni = String.Format("{0:yyyy/MM/dd}", dtpFiltroDesde.Value);
        //            fechaAltaFin = String.Format("{0:yyyy/MM/dd}", dtpFiltroHasta.Value);
        //        }

        //        //Estado Atencion
        //        atencionActiva = null;
        //        if (cboEstadoAtencion.SelectedIndex == 1)
        //        {
        //            atencionActiva = "true";
        //        }
        //        else if (cboEstadoAtencion.SelectedIndex == 2)
        //        {
        //            atencionActiva = "false";
        //        }

        //        //aseguradora
        //        codigoAseguradoraEmpresa = null;
        //        if (cboAseguradoras.SelectedIndex > 0)
        //        {
        //            ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)cboAseguradoras.SelectedItem;
        //            codigoAseguradoraEmpresa = aseguradora.ASE_CODIGO.ToString();
        //        }

        //        pacientesVistaLista = NegPacientes.RecuperarPacientesLista(fechaCreacionIni, fechaCreacionFin,
        //        fechaIngresoIni, fechaIngresoFin, fechaAltaIni, fechaAltaFin, atencionActiva, codigoMedico, codigoAseguradoraEmpresa);
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message);
        //    }
        //}

        private void cmbConvenioPago_SelectedValueChanged(object sender, EventArgs e)
        {
            TIPO_EMPRESA tfp = (TIPO_EMPRESA)cmbConvenioPago.SelectedItem;

            if (btnActualizar.Enabled == true)
            {
                cb_seguros.DataSource = NegCategorias.ListaCategorias(tfp);
                cb_seguros.ValueMember = "CAT_CODIGO";
                cb_seguros.DisplayMember = "CAT_NOMBRE";
                cb_seguros.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb_seguros.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                btnGenerar.Enabled = true;
                cb_seguros.Enabled = true;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                CreateExcel(FindSavePath());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }

    }
}
