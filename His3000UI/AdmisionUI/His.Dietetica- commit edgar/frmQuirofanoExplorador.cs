using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;

namespace His.Dietetica
{
    public partial class frmQuirofanoExplorador : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static string cie_codigo; //Variable que contiene el codigo del registro que se seleccione desde el grid
        private static string pac_codigo; //Variable que contiene el codigo del paciente que se seleccione desde el grid
        private static string ate_codigo; //Variable que contiene el codigo de la atencion que se seleccione desde el grid
        private static UltraGridRow num_fila; //Variable que contendra el numero de fila seleccionada por el usuario
        internal static string cie_descripcion; //Variable que contiene la descripcion del procedimiento
        private static string paciente; //nombre de paciente
        private static bool modotablet; //modo table activado para este formulario 
        public frmQuirofanoExplorador()
        {
            InitializeComponent();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    this.ultraGridExcelExporter1.Export(UltraGridPacientes, PathExcel);
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

        private void frmQuirofanoExplorador_Load(object sender, EventArgs e)
        {
            CargarProcedimientos();
            CargarQuirofanoPacientes();
        }
        public void CargarProcedimientos()
        {
            cmbProcedimiento.DataSource = Quirofano.TodosProcedimiento();
            cmbProcedimiento.DisplayMember = "Procedimiento";
            cmbProcedimiento.ValueMember = "CIE_CODIGO";
        }
        public void CargarQuirofanoPacientes()
        {
            UltraGridPacientes.DataSource = Quirofano.QuirofanoPacientes();
            RedimencionarTabla();
        }
        public void RedimencionarTabla()
        {
            try
            {
                UltraGridBand bandUno = UltraGridPacientes.DisplayLayout.Bands[0];

                UltraGridPacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridPacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridPacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridPacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridPacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridPacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                UltraGridPacientes.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                UltraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridPacientes.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[0].Width = 60;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[1].Width = 60;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[2].Width = 300;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Width = 100;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[4].Width = 80;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Width = 140;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[6].Width = 400;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Hidden = false;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = false;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[8].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[10].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[11].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[13].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[14].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UltraGridPacientes_Click(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow Fila = UltraGridPacientes.ActiveRow;
                num_fila = Fila;
                ate_codigo = Fila.Cells[9].Value.ToString();
                pac_codigo = Fila.Cells[8].Value.ToString();
                cie_codigo = Fila.Cells[7].Value.ToString();
            }
            catch 
            {

                //throw;
            }
            
        }

        private void UltraGridPacientes_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow Fila = UltraGridPacientes.ActiveRow;
                if (Fila.Cells[6].Value.ToString() == "")
                {
                    if (MessageBox.Show("¿Desea Agregar Procedimiento al Paciente?", "Warning", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        cie_codigo = null;
                        frmayudaProcedimiento.pedidopaciente = true;
                        frmayudaProcedimiento x = new frmayudaProcedimiento();
                        x.Show();
                        x.FormClosed += X_FormClosed;
                    }
                }
                else
                {
                    paciente = Fila.Cells[2].Value.ToString();
                    cie_descripcion = Fila.Cells[6].Value.ToString();
                    ate_codigo = Fila.Cells[9].Value.ToString();
                    pac_codigo = Fila.Cells[8].Value.ToString();
                    cie_codigo = Fila.Cells[7].Value.ToString();
                    //Envio datos a otro formulario 
                    frmQuirofanoPedidoPaciente.medico = Fila.Cells[10].Value.ToString();
                    frmQuirofanoPedidoPaciente.seguro = Fila.Cells[11].Value.ToString();
                    frmQuirofanoPedidoPaciente.tipo = Fila.Cells[12].Value.ToString();
                    frmQuirofanoPedidoPaciente.genero = Fila.Cells[13].Value.ToString();
                    frmQuirofanoPedidoPaciente.referido = Fila.Cells[14].Value.ToString();
                    frmQuirofanoPedidoPaciente.nombrepaciente = paciente;
                    frmQuirofanoPedidoPaciente.cie_descripcion = cie_descripcion;
                    frmQuirofanoPedidoPaciente.ate_codigo = ate_codigo;
                    frmQuirofanoPedidoPaciente.pac_codigo = pac_codigo;
                    frmQuirofanoPedidoPaciente.cie_codigo = cie_codigo;
                    //Abrir el formulario
                    frmQuirofanoPedidoPaciente x = new frmQuirofanoPedidoPaciente();
                    x.Show();
                }
            }
            catch 
            {

                //throw;
            }
            
            
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            DateTime fecha = DateTime.Now;
            frmayudaProcedimiento.pedidopaciente = false;
            try
            {
                if(cie_codigo != null)
                {
                    Quirofano.PedidoPaciente(cie_codigo, pac_codigo, ate_codigo, Convert.ToString(fecha));
                    MessageBox.Show("Procedimiento Agregado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CargarQuirofanoPacientes();
        }

        private void UltraGridPacientes_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                cie_codigo = null;
                frmayudaProcedimiento.pedidopaciente = true;
                frmayudaProcedimiento x = new frmayudaProcedimiento();
                x.Show();
                x.FormClosed += X_FormClosed;
            }
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            if(modotablet == false)
            {
                UltraGridPacientes.DataSource = Quirofano.PorProcedimiento(cmbProcedimiento.SelectedValue.ToString());
                RedimencionarTabla();
            }
            else
            {
                UltraGridPacientes.DataSource = Quirofano.PorProcedimiento(cmbProcedimiento.SelectedValue.ToString());
                RedimencionarTablet();
            }
        }

        private void toolStripButtonTablet_Click(object sender, EventArgs e)
        {
            if(modotablet == false)
            {
                UltraGridPacientes.DataSource = Quirofano.QuirofanoPacientes();
                RedimencionarTablet();
                UltraGridPacientes.Focus();
                frmQuirofanoPedidoPaciente.modotablet = true;
                modotablet = true;
            }
            else
            {
                if(MessageBox.Show("¿Desea Salir de Modo Tablet?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.Yes)
                {
                    UltraGridPacientes.DataSource = Quirofano.QuirofanoPacientes();
                    RedimencionarTabla();
                    UltraGridPacientes.Focus();
                    frmQuirofanoPedidoPaciente.modotablet = false;
                    modotablet = false;
                }
            }
        }
        public void RedimencionarTablet()
        {
            try
            {
                UltraGridBand bandUno = UltraGridPacientes.DisplayLayout.Bands[0];

                UltraGridPacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridPacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridPacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridPacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridPacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridPacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
                
                //Agrandar las filas
                UltraGridPacientes.DisplayLayout.Override.DefaultRowHeight = 40; //Para el modo tablet

                //Caracteristicas de Filtro en la grilla
                UltraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridPacientes.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[0].Width = 70;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[1].Width = 60;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[2].Width = 300;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Width = 100;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[4].Width = 80;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Width = 140;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[6].Width = 400;

                //agrandamiento de filas 

                //Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[7].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[8].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[10].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[11].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[13].Hidden = true;
                UltraGridPacientes.DisplayLayout.Bands[0].Columns[14].Hidden = true;

                //TablaProductos.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                //TablaProductos.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
