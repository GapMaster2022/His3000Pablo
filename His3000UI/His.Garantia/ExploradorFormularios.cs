using Recursos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using His.Entidades;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;

namespace His.Garantia
{
    public partial class frm_ExploradorFormularios : Form
    {
        NegPacienteGarantia Garantia = new NegPacienteGarantia();
        internal static string hc;
        public frm_ExploradorFormularios()
        {
            InitializeComponent();
        }

        private void ultraGridPacientes_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            //Caracteristicas de Filtro en la grilla
            ultraGridReportes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridReportes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridReportes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridReportes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            ultraGridReportes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
        }

        private void ExploradorFormularios_Load(object sender, EventArgs e)
        {
            try
            {
                dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
                dtpFiltroHasta.Value = DateTime.Now;
                //ultraGridReportes.DataSource = Negocio.NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), true, false, false, false, 0, false, 0, false, 0,false,"");
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
            CargarTipo();
            Bloquear();
        }
        public void Bloquear()
        {
            txthc.Enabled = false;
            txtf1.Enabled = false;
            combotipo.Enabled = false;
            cboEstadoGarantia.Enabled = false;
        }
        public void CargarTipo()
        {
            combotipo.DataSource = Garantia.TipoGarantia();
            combotipo.ValueMember = "TG_NOMBRE";
            combotipo.ValueMember = "TG_CODIGO";
        }
        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            if (Fecha.Checked == true && Nhc.Checked == false 
                && chkEstado.Checked == false && chbTipo.Checked == false)
            {
                if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorFechas(dtpFiltroDesde.Value, dtpFiltroHasta.Value);
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //fecha y HC
            if (Fecha.Checked == true && Nhc.Checked == true
                && chkEstado.Checked == false && chbTipo.Checked == false)
            {
                if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    if(txthc.Text != "" || txthc.Text != " ")
                    {
                        ultraGridReportes.DataSource = Garantia.CargarPorFechasHc(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                        RedimencionarGrid();
                        if (ultraGridReportes.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //Fecha, HC, Estado
            if (Fecha.Checked == true && Nhc.Checked == true && chkEstado.Checked == true && chbTipo.Checked == false)
            {
                if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Todas")
                    {
                        ultraGridReportes.DataSource = Garantia.CargarPorFechasHc(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                        RedimencionarGrid(); if (ultraGridReportes.Rows.Count == 0)

                            if (ultraGridReportes.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Caducadas")
                    {
                        ultraGridReportes.DataSource = Garantia.CargarPorFechasHcCaducada(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                        RedimencionarGrid();
                        if (ultraGridReportes.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Canceladas")
                    {
                        ultraGridReportes.DataSource = Garantia.CargarPorFechasHcCancelada(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                        RedimencionarGrid();
                        if (ultraGridReportes.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Vigentes")
                    {
                        ultraGridReportes.DataSource = Garantia.CargarPorFechasHcVigente(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text);
                        RedimencionarGrid();
                        if (ultraGridReportes.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //Fecha, HC, Estado y Tipo
            if (Fecha.Checked == true && Nhc.Checked == true && chkEstado.Checked == true && chbTipo.Checked == true)
            {
                if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Todas")
                    {
                        ultraGridReportes.DataSource = Garantia.CargarGarantiaFechas(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                        RedimencionarGrid();
                        if (ultraGridReportes.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Caducadas")
                    {
                        ultraGridReportes.DataSource = Garantia.CargarGarantiaFechasCaduca(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                        RedimencionarGrid();
                        if (ultraGridReportes.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Canceladas")
                    {
                        ultraGridReportes.DataSource = Garantia.CargarGarantiaFechasCancelado(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                        RedimencionarGrid();
                        if (ultraGridReportes.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (cboEstadoGarantia.SelectedItem.ToString() == "Vigentes")
                    {
                        ultraGridReportes.DataSource = Garantia.CargarGarantiaFechasVigente(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                        RedimencionarGrid();
                        if (ultraGridReportes.Rows.Count == 0)
                        {
                            MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //fecha, hc, tipo
            if (Fecha.Checked == true && Nhc.Checked == true && chbTipo.Checked == true && chkEstado.Checked == false)
            {
                if (dtpFiltroDesde.Value < dtpFiltroHasta.Value)
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorFechasHcTipo(dtpFiltroDesde.Text, dtpFiltroHasta.Text, txthc.Text, combotipo.SelectedValue.ToString());
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("\"Desde\" No Puede Ser Mayor Que \"Hasta\"", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //Solo HC
            if (Fecha.Checked == false && Nhc.Checked == true && chbTipo.Checked == false && chkEstado.Checked == false)
            {
                ultraGridReportes.DataSource = Garantia.CargarPorHc(txthc.Text);
                RedimencionarGrid();
                if (ultraGridReportes.Rows.Count == 0)
                {
                    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //Solo HC, y Estado
            if (Fecha.Checked == false && Nhc.Checked == true && chbTipo.Checked == false && chkEstado.Checked == true)
            {
                if(cboEstadoGarantia.SelectedItem.ToString() == "Todas")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorHc(txthc.Text);
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (cboEstadoGarantia.SelectedItem.ToString() == "Caducadas")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorHcCaducada(txthc.Text);
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (cboEstadoGarantia.SelectedItem.ToString() == "Canceladas")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorHcCancelada(txthc.Text);
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (cboEstadoGarantia.SelectedItem.ToString() == "Vigentes")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorHcVigente(txthc.Text);
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            //Solo HC y Tipo
            if (Fecha.Checked == false && Nhc.Checked == true && chbTipo.Checked == true && chkEstado.Checked == false)
            {
                ultraGridReportes.DataSource = Garantia.CargarPacienteGarantiaTodo(txthc.Text, combotipo.SelectedValue.ToString());
                RedimencionarGrid();
                if (ultraGridReportes.Rows.Count == 0)
                {
                    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //Solo Estado
            if (Fecha.Checked == false && Nhc.Checked == false && chbTipo.Checked == false && chkEstado.Checked == true)
            {
                if(cboEstadoGarantia.SelectedItem.ToString() == "Todas")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorTodo();
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (cboEstadoGarantia.SelectedItem.ToString() == "Caducadas")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorCaducada();
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (cboEstadoGarantia.SelectedItem.ToString() == "Canceladas")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorCancelada();
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (cboEstadoGarantia.SelectedItem.ToString() == "Vigentes")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorVigente();
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            //Solo Tipo
            if (Fecha.Checked == false && Nhc.Checked == false && chbTipo.Checked == true && chkEstado.Checked == false)
            {
                ultraGridReportes.DataSource = Garantia.CargarPorTipo(combotipo.SelectedValue.ToString());
                RedimencionarGrid();
                if (ultraGridReportes.Rows.Count == 0)
                {
                    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //Fecha y Tipo
            if (Fecha.Checked == true && Nhc.Checked == false && chbTipo.Checked == true && chkEstado.Checked == false)
            {
                ultraGridReportes.DataSource = Garantia.CargarPorFechayTipo(dtpFiltroDesde.Text, dtpFiltroHasta.Text, combotipo.SelectedValue.ToString());
                RedimencionarGrid();
                if (ultraGridReportes.Rows.Count == 0)
                {
                    MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //Fecha Y Estado
            if (Fecha.Checked == true && Nhc.Checked == false && chbTipo.Checked == false && chkEstado.Checked == true)
            {
                if (cboEstadoGarantia.SelectedItem.ToString() == "Todas")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorFechas(dtpFiltroDesde.Value, dtpFiltroHasta.Value);
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (cboEstadoGarantia.SelectedItem.ToString() == "Caducadas")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorFechayCaducada(dtpFiltroDesde.Text, dtpFiltroHasta.Text);
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (cboEstadoGarantia.SelectedItem.ToString() == "Canceladas")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorFechayCancelada(dtpFiltroDesde.Text, dtpFiltroHasta.Text);
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (cboEstadoGarantia.SelectedItem.ToString() == "Vigentes")
                {
                    ultraGridReportes.DataSource = Garantia.CargarPorFechayVigente(dtpFiltroDesde.Text, dtpFiltroHasta.Text);
                    RedimencionarGrid();
                    if (ultraGridReportes.Rows.Count == 0)
                    {
                        MessageBox.Show("No Hay Registros Para Mostrar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private static void Numbers(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
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

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ultraGridReportes.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ultraGridReportes, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }


        private void ultraGridPacientes_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void historiaClinicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ultraGridPacientes_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {

        }

        private void txtf1_Click(object sender, EventArgs e)
        {
            hc = null;
            frmAyuda x = new frmAyuda();
            frmAyuda.reporte = true;
            x.Show();
            x.FormClosed += CerrarAyuda;
        }
        private void CerrarAyuda(object sender, FormClosedEventArgs e)
        {
            frmAyuda.reporte = false;
            if (hc != null)
            {
                txthc.Text = hc.Trim();
            }
        }
        private void Fecha_CheckedChanged(object sender, EventArgs e)
        {
            if (Fecha.Checked == true)
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
            if (Nhc.Checked == true)
            {
                txthc.Enabled = true;
                txtf1.Enabled = true;
            }
            else
            {
                txthc.Enabled = false;
                txtf1.Enabled = false;
                txthc.Text = "";
            }
        }

        private void chkTratamiento_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstado.Checked == true)
            {
                cboEstadoGarantia.Enabled = true;
            }
            else
            {
                cboEstadoGarantia.Enabled = false;
            }
        }

        private void chbTipoIngreso_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTipo.Checked == true)
            {
                combotipo.Enabled = true;
            }
            else
            {
                combotipo.Enabled = false;
            }
        }
        public void RedimencionarGrid()
        {
            try
            {
                UltraGridBand bandUno = ultraGridReportes.DisplayLayout.Bands[0];

                ultraGridReportes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                ultraGridReportes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                ultraGridReportes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                ultraGridReportes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                ultraGridReportes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                ultraGridReportes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                ultraGridReportes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                ultraGridReportes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ultraGridReportes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ultraGridReportes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ultraGridReportes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                ultraGridReportes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                ultraGridReportes.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                ultraGridReportes.DisplayLayout.Bands[0].Columns[0].Width = 120;
                ultraGridReportes.DisplayLayout.Bands[0].Columns[3].Width = 260;
                ultraGridReportes.DisplayLayout.Bands[0].Columns[6].Width = 180;
                ultraGridReportes.DisplayLayout.Bands[0].Columns[7].Width = 120;
                ultraGridReportes.DisplayLayout.Bands[0].Columns[8].Width = 120;
                ultraGridReportes.DisplayLayout.Bands[0].Columns[11].Width = 120;
                ultraGridReportes.DisplayLayout.Bands[0].Columns[13].Width = 180;
                ultraGridReportes.DisplayLayout.Bands[0].Columns[14].Width = 180;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txthc_TextChanged(object sender, EventArgs e)
        {
            if(txthc.Text == "" || txthc.Text == " ")
            {
                txthc.Text = "0";
            }
            else
            {

            }
        }
        private void txthc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numbers(e, false);
        }
    }
}

