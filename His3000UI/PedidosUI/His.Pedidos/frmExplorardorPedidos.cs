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
using His.Entidades.Clases;
using Recursos;
using Infragistics.Win.UltraWinGrid;
using System.IO;

namespace His.Pedidos
{
    public partial class frmExplorardorPedidos : Form
    {
        #region variables
        List<DtoPedidos> listaPedidos;
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
        #endregion

        #region propiedades

        public PEDIDOS_AREAS AreaPedido
        {
            get { return areaPedido; }
            set { areaPedido = value; }
        }

        #endregion

        #region constructor
        public frmExplorardorPedidos()
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
            toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            toolStripButtonImprimir.Image = Archivo.imgBtnImprimir32;
            toolStripButtonSalir.Image = Archivo.imgBtnSalir32;
            toolStripButtonExportar.Image = Archivo.imgOfficeExcel;
            //cargo las imagenes del menu de la grilla
            tsbAdjuntar.Image = Archivo.imgBtnGoneMailAttachment48;
            tsbAnular.Image = Archivo.imgBtnDelete;
            tsbComentarios.Image = Archivo.imgBtnDocuments_open;
            tsbDespacho.Image = Archivo.imgBtnGoneSend48;
            //cargo los comboBox
            uCboDepartamento.DataSource = NegPedidos.recuperarListaEstaciones();
            uCboDepartamento.DisplayMember = "PEE_NOMBRE";
            uCboDepartamento.ValueMember = "PEE_CODIGO";
            uCboDepartamento.SelectedIndex = 0;
            //
            uCboEstado.DataSource = NegCatalogos.RecuperarCatalogoListaGen("PEDIDOS","PED_ESTADO") ;
            uCboEstado.DisplayMember = "nombre";
            uCboEstado.ValueMember = "codigo";
            uCboEstado.SelectedIndex = 0;
            //cargo los valores por defecto de las fechas
            dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", "01/" + DateTime.Now.Month + "/" + (DateTime.Now.Year).ToString()));
            dtpFiltroHasta.Value = DateTime.Now;
            //otros
            uLblArea.Text = areaPedido.PEA_NOMBRE;
            this.WindowState = FormWindowState.Maximized;
        }
        #endregion

        #region metodos de la grilla
        private void cargarGrilla()
        {
            pintarCeldas = true;
            if(accion==sAccion.Inicial)
                listaPedidos = NegPedidos.recuperarListaPedidosVistaPendientesPorArea(areaPedido.PEA_CODIGO);
            else if(accion==sAccion.Actualizar)
                listaPedidos = NegPedidos.recuperarListaPedidosVistaPorArea(areaPedido.PEA_CODIGO, Convert.ToByte(uCboDepartamento.Value), Convert.ToInt32(uCboEstado.Value), dtpFiltroDesde.Value, dtpFiltroHasta.Value, 0, 1000);
            switch (Convert.ToByte(optAgrupar.CheckedItem.DataValue))
            {
                case 1:
                    var lista = from p in listaPedidos
                                group p by p.PED_CODIGO into g
                                select new { grupo = g, detalle = g.Key };
                    uGridPedidos.DataSource = lista.ToList();
                    break;
                default:
                    uGridPedidos.DataSource = listaPedidos;
                    break;
            }
            pintarCeldas = false;
        }
#endregion

        #region eventos
        private void frmExplorardorPedidos_Load(object sender, EventArgs e)
        {
            try
            {
                //inicializo variables
                accion = sAccion.Inicial;
                //cargo la vista
                //InicializarVariables();
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
            if (!uGridPedidos.DisplayLayout.Bands[0].Columns.Exists("CHECKTRANS"))
            {
                uGridPedidos.DisplayLayout.Bands[0].Columns.Add("CHECKTRANS", "");
                uGridPedidos.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DataType = typeof(bool);
                uGridPedidos.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                uGridPedidos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
                iniGrid = true;
            }
            uGridPedidos.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Header.VisiblePosition = 0;
            uGridPedidos.DisplayLayout.Bands[0].Columns["CHECKTRANS"].CellActivation = Activation.AllowEdit;
            uGridPedidos.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DefaultCellValue = true;
           
            e.Layout.Bands[0].Columns["CODIGO"].Hidden = true;
            //e.Layout.Bands[0].Columns["PED_CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["PEE_CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["PEA_CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["PED_ESTADO"].Hidden = true;
            e.Layout.Bands[0].Columns["ESTADO"].Hidden = true;
            e.Layout.Bands[0].Columns["PED_FECHA_ALTA"].Hidden = true;
            e.Layout.Bands[0].Columns["PED_FECHA_ALTA"].Hidden = true;
            e.Layout.Bands[0].Columns["ID_USUARIO"].Hidden = true;
            e.Layout.Bands[0].Columns["ATE_CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["TIP_PEDIDO"].Hidden = true;
            e.Layout.Bands[0].Columns["TIPO"].Hidden = true;
            e.Layout.Bands[0].Columns["PDD_IVA"].Hidden = true;
            e.Layout.Bands[0].Columns["PED_DESCRIPCION"].Hidden = true;

            UltraGridBand bandUno = uGridPedidos.DisplayLayout.Bands[0];
            
            bandUno.Columns["PED_FECHA"].Header.Caption = "FECHA";
            bandUno.Columns["HISTORIA_CLINICA"].Header.Caption = "HC";
            bandUno.Columns["PED_CODIGO"].Header.Caption = "PEDIDO";
            bandUno.Columns["PDD_CODIGO"].Header.Caption = "NUMERO PEDIDO DET.";
            bandUno.Columns["PRO_CODIGO"].Header.Caption = "CODIGO PRODUCTO";
            bandUno.Columns["PRO_DESCRIPCION"].Header.Caption = "DESCRIPCION";
            bandUno.Columns["PDD_CANTIDAD"].Header.Caption = "CANTIDAD";
            bandUno.Columns["PDD_VALOR"].Header.Caption = "VALOR";
            bandUno.Columns["PDD_TOTAL"].Header.Caption = "TOTAL";
            bandUno.Columns["PDD_ESTADO"].Header.Caption = "ESTADO";
        
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
            foreach(var item in uGridPedidos.Rows)
            {
                item.Cells["CHECKTRANS"].Value = true;
            }
        }

        private void lLblNinguno_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (var item in uGridPedidos.Rows)
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
                    this.ultraGridExcelExporter1.Export(uGridPedidos, myFilepath);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + myFilepath);
                }
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

                uGridPedidos.ActiveCell = uGridPedidos.Rows[0].Cells["PED_CODIGO"];

                foreach (var item in uGridPedidos.Rows)
                {
                    if ((bool)item.Cells["CHECKTRANS"].Value == true)
                    {
                        NegPedidos.actualizarEstadoPedidoDetalle(Convert.ToInt32(item.Cells["PDD_CODIGO"].Value), true);
                        contador++;
                    }
                }
                if (contador > 0)
                {
                    cargarGrilla();
                }
                else
                {
                    MessageBox.Show("Seleccione un pedido para su despacho", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                foreach (var item in uGridPedidos.Rows)
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
                if (pintarCeldas == false)
                    return;
 
                if (optOrdenar.Items[0].CheckState == CheckState.Checked)
                {
                    if (e.Row.Cells[0].Value.ToString() != valorGridAnt)
                    {
                        ranColor++;
                        e.Row.Appearance.BackColor = pColor[ranColor];
                        valorGridAnt = e.Row.Cells[0].Value.ToString();
                    }
                    else
                        e.Row.Appearance.BackColor = pColor[ranColor];
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void tsbAdjuntar_Click(object sender, EventArgs e)
        {
            try
            {
                if (uGridPedidos.ActiveRow != null)
                {
                    var expArchivos = new GeneralApp.ControlesWinForms.ClienteFTPVista(0);
                    expArchivos.CarpetaServidor = uGridPedidos.ActiveRow.Cells["PED_CODIGO"].Text;
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
                if (uGridPedidos.ActiveRow != null)
                {
                    frmComentarios ventana = new frmComentarios();
                    ventana.Show();
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

        }

    }
}
