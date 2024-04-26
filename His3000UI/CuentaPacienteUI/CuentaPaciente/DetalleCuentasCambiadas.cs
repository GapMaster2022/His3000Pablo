using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Formulario;
using His.Negocio;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using frm_Ayudas = His.Admision.frm_Ayudas;


namespace CuentaPaciente
{
    public partial class DetalleCuentasCambiadas : Form
    {
        #region declaracion de variables
        //Infragistics.Win.UltraWinGrid.UltraGrid ugdetcuecambiadas = new UltraGrid();
        private string fechaIni;
        private string fechaFin;

        List<HC_CATALOGO_SUBNIVEL> listaCatSub;
        #endregion

        public DetalleCuentasCambiadas()
        {
            InitializeComponent();
        }

        public void CargarDatos()
        {
            //ultraTabControlCuenta.Enabled = true;
            cmb_areadetalle.DataSource = NegPedidos.recuperarListaAreas().OrderBy(a => a.PEA_NOMBRE).ToList();
            cmb_areadetalle.ValueMember = "PEA_CODIGO";
            cmb_areadetalle.DisplayMember = "PEA_NOMBRE";
            cmb_areadetalle.SelectedIndex = 0;
            listaCatSub = NegHcCatalogoSubNivel.RecuperarSubNivel(40);
            //cmb_Parentesco.DataSource = listaCatSub;
            //cmb_Parentesco.DisplayMember = "CA_DESCRIPCION";
            //cmb_Parentesco.ValueMember = "CA_CODIGO";
            //cmb_Parentesco.AutoCompleteSource = AutoCompleteSource.ListItems;
            //cmb_Parentesco.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //txtCantidad.Text = "1";
            //ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
            //byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DetalleCuentasCambiadas_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cargarGrid();
        }
        public void cargarGrid() 
        {
            fechaIni = String.Format("{0:dd-MM-yyyy}", dtp_desde.Value) ;
            fechaFin = String.Format("{0:dd-MM-yyyy}", dtp_hasta.Value) ;

            DataTable datos;

            if (cmb_areadetalle.SelectedValue == null)
            {
                datos = NegCuentasPacientes.DetalleCuentasModificadas
                 (fechaIni.ToString(), fechaFin.ToString(), this.txt_historia.Text, this.txt_atencion.Text, "");
            }
            else
            {

                datos = NegCuentasPacientes.DetalleCuentasModificadas
                (fechaIni.ToString(), fechaFin.ToString(), this.txt_historia.Text, this.txt_atencion.Text, cmb_areadetalle.SelectedValue.ToString());     
            
            }
                ugdetcuecambiadas.DataSource = datos;
                if (datos.Rows.Count != 0)

                    toolStripButton6.Enabled = true;

        }

        private void ugdetcuecambiadas_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ugdetcuecambiadas.DisplayLayout.Bands[0];
            ugdetcuecambiadas.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            ugdetcuecambiadas.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ugdetcuecambiadas.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ugdetcuecambiadas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            //Caracteristicas de Filtro en la grilla
            ugdetcuecambiadas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ugdetcuecambiadas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ugdetcuecambiadas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ugdetcuecambiadas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            ugdetcuecambiadas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ugdetcuecambiadas.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ugdetcuecambiadas, PathExcel);
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
    
    }

}
