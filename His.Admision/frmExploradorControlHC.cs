using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using System.IO;
using His.DatosReportes;
using His.Entidades.Clases;
using His.Entidades;

namespace His.Admision
{
    public partial class frmExploradorControlHC : Form
    {
        NegControlHc Negocio = new NegControlHc();
        internal static string hcayuda; //numero de HC que envia al abrir formulario de paciente ayuda
        public frmExploradorControlHC()
        {
            InitializeComponent();
        }

        public void RedimencionarTabla()
        {
            try
            {
                UltraGridBand bandUno = ultraGridControl.DisplayLayout.Bands[0];

                ultraGridControl.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                ultraGridControl.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                ultraGridControl.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                ultraGridControl.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                ultraGridControl.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                ultraGridControl.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                ultraGridControl.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                ultraGridControl.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ultraGridControl.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ultraGridControl.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ultraGridControl.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                ultraGridControl.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                ultraGridControl.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                ultraGridControl.DisplayLayout.Bands[0].Columns[0].Width = 80;
                ultraGridControl.DisplayLayout.Bands[0].Columns[1].Width = 60;
                ultraGridControl.DisplayLayout.Bands[0].Columns[2].Width = 300;
                ultraGridControl.DisplayLayout.Bands[0].Columns[3].Width = 100;
                ultraGridControl.DisplayLayout.Bands[0].Columns[4].Width = 80;
                ultraGridControl.DisplayLayout.Bands[0].Columns[5].Width = 180;
                ultraGridControl.DisplayLayout.Bands[0].Columns[6].Width = 280;
                ultraGridControl.DisplayLayout.Bands[0].Columns[7].Width = 180;
                ultraGridControl.DisplayLayout.Bands[0].Columns[8].Width = 120;
                ultraGridControl.DisplayLayout.Bands[0].Columns[9].Width = 120;

                //Ocultar columnas, que son fundamentales al levantar informacion
                ultraGridControl.DisplayLayout.Bands[0].Columns[11].Hidden = true;
                ultraGridControl.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                ultraGridControl.DisplayLayout.Bands[0].Columns[13].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ultraGridControl_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Fecha_CheckedChanged(object sender, EventArgs e)
        {
            if(FechaIngreso.Checked == true)
            {
                dtpFiltroDesde.Enabled = true;
                dtpFiltroHasta.Enabled = true;
            }
            else
            {
                dtpFiltroDesde.Enabled = false;
                dtpFiltroHasta.Enabled = false;
            }
        }

        private void Nhc_CheckedChanged(object sender, EventArgs e)
        {
            if(Nhc.Checked == true)
            {
                txthc.Enabled = true;
                txtf1.Enabled = true;
            }
            else
            {
                txthc.Enabled = false;
                txtf1.Enabled = false;
            }
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            if (FechaIngreso.Checked == true &&  FechaAlta.Checked == false &&
                Nhc.Checked == false && chkEstado.Checked == false) //Valida solo la fecha
            {
                if (dtpFiltroDesde.Value.Date < dtpFiltroHasta.Value.Date)
                {
                    ultraGridControl.DataSource = Negocio.PorFecha(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59));
                    RedimencionarTabla();
                    if (ultraGridControl.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (Nhc.Checked == true  && FechaAlta.Checked == false && 
                FechaIngreso.Checked == false && chkEstado.Checked == false) //Valida solo por HC
            {
                ultraGridControl.DataSource = Negocio.PorHc(txthc.Text);
                RedimencionarTabla();
                if (ultraGridControl.Rows.Count <= 0)
                {
                    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (FechaIngreso.Checked == false && FechaAlta.Checked == false && 
                Nhc.Checked == false && chkEstado.Checked == true) //Valida solo por estado completo o incompleto
            {
                if (cboEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("Valor Vacio no Valido", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    ultraGridControl.DataSource = Negocio.PorEstado(cboEstado.SelectedText);
                    RedimencionarTabla();
                    if (ultraGridControl.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            if (FechaIngreso.Checked == true && FechaAlta.Checked == false && 
                Nhc.Checked == true && chkEstado.Checked == false) //valida por fecha y HC del paciente
            {
                if (dtpFiltroDesde.Value.Date < dtpFiltroHasta.Value.Date)
                {
                    ultraGridControl.DataSource = Negocio.PorFechayHc(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), txthc.Text);
                    RedimencionarTabla();
                    if(ultraGridControl.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (FechaIngreso.Checked == true && FechaAlta.Checked == false && 
                Nhc.Checked == false && chkEstado.Checked == true) //Valida por fecha y por estado 
            {
                if (dtpFiltroDesde.Value.Date < dtpFiltroHasta.Value.Date)
                {
                    if(cboEstado.SelectedIndex != -1) //controla que en el combo no se eliga vacio
                    {
                        ultraGridControl.DataSource = Negocio.PorFechayEstado(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), cboEstado.SelectedText);
                        RedimencionarTabla();
                        if (ultraGridControl.Rows.Count <= 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Valor Vacio no Valido", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (FechaIngreso.Checked == false && FechaAlta.Checked == false && 
                Nhc.Checked == true && chkEstado.Checked == true) //Valida por hc y estado
            {
                if (cboEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("Valor Vacio no Valido", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    ultraGridControl.DataSource = Negocio.PorHCyEstado(txthc.Text,cboEstado.SelectedText);
                    RedimencionarTabla();
                    if (ultraGridControl.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            if (FechaIngreso.Checked == true && FechaAlta.Checked == false && 
                Nhc.Checked == true && chkEstado.Checked == true) //valida por fecha, hc y estado
            {
                if (dtpFiltroDesde.Value.Date < dtpFiltroHasta.Value.Date)
                {
                    if (cboEstado.SelectedIndex != -1)
                    {
                        ultraGridControl.DataSource = Negocio.PorFechaHCEstado(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), txthc.Text, cboEstado.SelectedText);
                        RedimencionarTabla();
                        if (ultraGridControl.Rows.Count <= 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Valor Vacio no Valido", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (FechaIngreso.Checked == false && FechaAlta.Checked == true &&
                Nhc.Checked == false && chkEstado.Checked == false)
            {
                if (dtpFiltroDesde.Value.Date < dtpFiltroHasta.Value.Date)
                {
                    ultraGridControl.DataSource = Negocio.PorFechaAlta(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59));
                    RedimencionarTabla();
                    if (ultraGridControl.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (FechaIngreso.Checked == false && FechaAlta.Checked == true &&
                Nhc.Checked == true && chkEstado.Checked == false) //valida por fecha alta y HC del paciente
            {
                if (dtpFiltroDesde.Value.Date < dtpFiltroHasta.Value.Date)
                {
                    ultraGridControl.DataSource = Negocio.PorFechaAltayHc(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), txthc.Text);
                    RedimencionarTabla();
                    if (ultraGridControl.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (FechaIngreso.Checked == false && FechaAlta.Checked == true &&
                Nhc.Checked == false && chkEstado.Checked == true) //Valida por fecha y por estado 
            {
                if (dtpFiltroDesde.Value.Date < dtpFiltroHasta.Value.Date)
                {
                    if (cboEstado.SelectedIndex != -1) //controla que en el combo no se eliga vacio
                    {
                        ultraGridControl.DataSource = Negocio.PorFechaAltayEstado(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), cboEstado.SelectedText);
                        RedimencionarTabla();
                        if (ultraGridControl.Rows.Count <= 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Valor Vacio no Valido", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (FechaIngreso.Checked == false && FechaAlta.Checked == true &&
                Nhc.Checked == true && chkEstado.Checked == true) //valida por fecha alta, hc y estado
            {
                if (dtpFiltroDesde.Value.Date < dtpFiltroHasta.Value.Date)
                {
                    if (cboEstado.SelectedIndex != -1)
                    {
                        ultraGridControl.DataSource = Negocio.PorFechaAltaHCEstado(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), txthc.Text, cboEstado.SelectedText);
                        RedimencionarTabla();
                        if (ultraGridControl.Rows.Count <= 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Valor Vacio no Valido", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void ultraGridControl_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow Fila = ultraGridControl.ActiveRow; //eligue el numero de fila que esta seleccionada

            frmExploradorControlAyuda x = new frmExploradorControlAyuda();
            frmExploradorControlAyuda.paciente = Fila.Cells[2].Value.ToString();//nombre de paciente
            frmExploradorControlAyuda.atencion = Fila.Cells[0].Value.ToString();//numero de atencion ATE_NUM_ATENCION
            frmExploradorControlAyuda.codigoA = Fila.Cells[11].Value.ToString();//Codigo de atencion ATE_CODIGO
            frmExploradorControlAyuda.codigop = Fila.Cells[12].Value.ToString();//codigo de paciente PAC_CODIGO
            frmExploradorControlAyuda.numestado = Fila.Cells[13].Value.ToString(); //Numero de estado PCHC_ESTATUS 
            x.Show();

        }
        private void frmExploradorControlHC_Load(object sender, EventArgs e)
        {
            Bloquear();
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            dtpFiltroDesde.Value = oPrimerDiaDelMes;
            dtpFiltroHasta.Value = oUltimoDiaDelMes;
        }
        public void Bloquear()
        {
            txtf1.Enabled = false;
            txthc.Enabled = false;
            cboEstado.Enabled = false;
        }

        private void chkEstado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstado.Checked == true)
            {
                cboEstado.Enabled = true;
            }
            else
            {
                cboEstado.Enabled = false;
            }
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ultraGridControl.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ultraGridControl, PathExcel);
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

        private void txtf1_Click(object sender, EventArgs e)
        {
            frm_AyudaPacientes x = new frm_AyudaPacientes();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(hcayuda != null || hcayuda != "")
            {
                txthc.Text = hcayuda; //Envia el HC del paciente desde el formulario frm_AyudaPacientes
                hcayuda = null; 
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(FechaAlta.Checked == true)
            {
                dtpFiltroDesde.Enabled = true;
                dtpFiltroHasta.Enabled = true;
            }
            else
            {
                dtpFiltroDesde.Enabled = false;
                dtpFiltroHasta.Enabled = false;
            }
        }
    }
}
