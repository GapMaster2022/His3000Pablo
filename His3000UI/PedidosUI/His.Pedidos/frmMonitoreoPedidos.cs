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
using His.HabitacionesUI;
using System.IO;

namespace His.Pedidos
{
    public partial class frmMonitoreoPedidos : Form
    {
        DataTable dtPedidos = new DataTable();
        public frmMonitoreoPedidos()
        {
            InitializeComponent();
        }

        private void frmMonitoreoPedidos_Load(object sender, EventArgs e)
        {

            //ranColor = new Random(DateTime.Now.Millisecond);
            //
            toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            btnimprimir.Image = Archivo.imgBtnImprimir32;
            toolStripButtonSalir.Image = Archivo.imgBtnSalir32;
            btnExportar.Image = Archivo.imgOfficeExcel;

            //cargo los comboBox
            uCboDepartamento.DataSource = NegPedidos.recuperarListaEstaciones();
            uCboDepartamento.DisplayMember = "PEE_NOMBRE";
            uCboDepartamento.ValueMember = "PEE_CODIGO";
            uCboDepartamento.SelectedIndex = 0;
            //
            uCboArea.DataSource = NegPedidos.recuperarListaAreas();
            uCboArea.DisplayMember = "PEA_NOMBRE";
            uCboArea.ValueMember = "PEA_CODIGO";
            uCboArea.SelectedIndex = 0;

            uCboEstado.SelectedIndex = 0;

            dtpFiltroDesde.Value = DateTime.Now.AddDays(-1);
            dtpFiltroHasta.Value = DateTime.Now;

        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {

                int Estado = 0;
                bool FiltroFechas = false;

                if (chkSinFiltroFechas.Checked == true)
                {
                    FiltroFechas = false;
                }
                else
                {
                    FiltroFechas = true;
                }

            //His.Formulario.WaitForm x = new Formulario.WaitForm();
            //x.ShowDialog();
                Estado = Convert.ToInt32(uCboEstado.SelectedItem.DataValue);


            //dtPedidos = NegPedidos.ListaPedidosRealizados(Convert.ToInt32(uCboDepartamento.SelectedItem.DataValue), Estado, FiltroFechas, Convert.ToDateTime(this.dtpFiltroDesde.Text), Convert.ToDateTime(this.dtpFiltroHasta.Text));
            //uGridPedidos.DataSource = dtPedidos;

            //CAMBIOS EDGAR 20210903 16:28
            uGridPedidos.DataSource = NegPedidos.Pedidos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));

            //uGridPedidos.DisplayLayout.Bands[0].Columns["ESTADO PEDIDO"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            //uGridPedidos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chkActualizarAuto.Checked == true)
            {
                int Estado = 0;
                bool FiltroFechas = false;

                if (chkSinFiltroFechas.Checked == true)
                {
                    FiltroFechas = false;
                }
                else
                {
                    FiltroFechas = true;
                }

                Estado = Convert.ToInt32(uCboEstado.SelectedItem.DataValue);



                //dtPedidos = NegPedidos.ListaPedidosRealizados(Convert.ToInt32(uCboDepartamento.SelectedItem.DataValue), Estado, FiltroFechas, Convert.ToDateTime(this.dtpFiltroDesde.Text), Convert.ToDateTime(this.dtpFiltroHasta.Text));
                //uGridPedidos.DataSource = dtPedidos;

                //CAMBIOS EDGAR 20210903 16:28
                uGridPedidos.DataSource = NegPedidos.Pedidos(DateTime.Now.Date, DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
                
                //uGridPedidos.DisplayLayout.Bands[0].Columns["ESTADO PEDIDO"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                //uGridPedidos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;

                //UltraGridBand bandUno = uGridPedidos.DisplayLayout.Bands[0];
                //uGridPedidos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //uGridPedidos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                //bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                //bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            }
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            if(Sesion.codDepartamento == 1 || Sesion.codDepartamento == 14)
            {
                if (uGridPedidos.Selected.Rows.Count > 0 && uGridPedidos.Selected.Rows.Count == 1)
                {
                    UltraGridRow fila = this.uGridPedidos.ActiveRow;
                    int ped_codigo = Convert.ToInt32(fila.Cells["CODIGO"].Value.ToString());

                    if (ped_codigo > 0)
                    {
                        var codigoArea = 100;

                        frmImpresionPedidos frmPedidos = new frmImpresionPedidos(ped_codigo, codigoArea, 1, 1);
                        frmImpresionPedidos.reimpresion = true;
                        frmPedidos.Show();
                    }

                }
                else
                {
                    MessageBox.Show("Debe elegir un pedido o no pueden ser varias filas a la vez.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (Sesion.codDepartamento == 9 || Sesion.codDepartamento == 7)//SOLO PARA IMAGEN
            {
                UltraGridRow fila = this.uGridPedidos.ActiveRow;
                int ped_codigo = Convert.ToInt32(fila.Cells["CODIGO"].Value.ToString());

                if (ped_codigo > 0)
                {
                    var codigoArea = 100;
                    frmImpresionPedidos frmPedidos = new frmImpresionPedidos(ped_codigo, codigoArea, 1, 3);
                    frmImpresionPedidos.reimpresion = true;
                    frmPedidos.Show();
                }
            }
            else
            {
                MessageBox.Show("Reimpresion valida unicamente para Farmacia.\r\nComuniquese con sistemas",
                    "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void uGridPedidos_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = uGridPedidos.DisplayLayout.Bands[0];

                uGridPedidos.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                uGridPedidos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                uGridPedidos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                uGridPedidos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                uGridPedidos.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                uGridPedidos.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                uGridPedidos.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                uGridPedidos.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                uGridPedidos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                uGridPedidos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                uGridPedidos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                uGridPedidos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                uGridPedidos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                uGridPedidos.DisplayLayout.UseFixedHeaders = true;

                uGridPedidos.DisplayLayout.Bands[0].Columns["ESTADO PEDIDO"].Hidden = true;


                ////Dimension los registros
                uGridPedidos.DisplayLayout.Bands[0].Columns[0].Width = 60;
                uGridPedidos.DisplayLayout.Bands[0].Columns[1].Width = 80;
                uGridPedidos.DisplayLayout.Bands[0].Columns[2].Width = 100;
                uGridPedidos.DisplayLayout.Bands[0].Columns[3].Width = 300;
                uGridPedidos.DisplayLayout.Bands[0].Columns[4].Width = 120;
                uGridPedidos.DisplayLayout.Bands[0].Columns[5].Width = 300;
                uGridPedidos.DisplayLayout.Bands[0].Columns[6].Width = 60;
                uGridPedidos.DisplayLayout.Bands[0].Columns[6].Width = 200;

                ////agrandamiento de filas 

                ////Ocultar columnas, que son fundamentales al levantar informacion
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Hidden = false;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = false;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[6].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[8].Hidden = false;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[10].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[11].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[13].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[14].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[15].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[16].Hidden = true;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[14].Hidden = true;
                ////TablaProductos.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                ////TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message); //No se muestra el error al usuario solo sirve para saber cual es el error por interno.
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(uGridPedidos, PathExcel);
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
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
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

        private void chkActualizarAuto_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}


