using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Recursos;
using System.IO;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;

namespace His.Admision
{
    public partial class frm_ExploradorIngresos : Form
    {
        public frm_ExploradorIngresos()
        {
            InitializeComponent();
            cargarTipoIngreso();
        }

        private void ultraGridPacientes_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            //Caracteristicas de Filtro en la grilla
            ultraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            ultraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
        }

        private void cargarTipoIngreso()
        {
            cboTipoIngreso.DataSource = Negocio.NegTipoIngreso.ListaTipoIngreso();
            cboTipoIngreso.DisplayMember = "TIP_DESCRIPCION";
            cboTipoIngreso.ValueMember = "TIP_CODIGO";
            cboTipoIngreso.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboTipoIngreso.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cmb_tipoatencion.DataSource = NegTipoTratamiento.RecuperaTipoTratamiento(); ;
            cmb_tipoatencion.DisplayMember = "TIA_DESCRIPCION";
            cmb_tipoatencion.ValueMember = "TIA_CODIGO";
            cmb_tipoatencion.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_tipoatencion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        public void OcultarAtencionesRepetidas()
        {

        }
        private void frm_ExploradorIngresos_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

                dtpFiltroDesde.Value = oPrimerDiaDelMes;
                dtpFiltroHasta.Value = oUltimoDiaDelMes;
                ultraGridPacientes.DataSource = NegPacientes.getAtencionesIngresos(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), true, false, false, false, 0, false, 0, false, 0, false, false);

            }
            catch (Exception err) { MessageBox.Show(err.Message); }
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

        private void ultraPanel1_PaintClient(object sender, PaintEventArgs e)
        {

        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_AyudaPacientes form = new frm_AyudaPacientes();
            form.campoPadre = txt_historiaclinica;
            form.ShowDialog();
            form.Dispose();
            txt_historiaclinica.Text = txt_historiaclinica.Text.Trim();
        }

        private void txt_historiaclinica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void chkTratamiento_CheckedChanged(object sender, EventArgs e)
        {
            cmb_tipoatencion.Enabled = chkTratamiento.Checked;
        }

        private void chbTipoIngreso_CheckedChanged_1(object sender, EventArgs e)
        {
            cboTipoIngreso.Enabled = chbTipoIngreso.Checked;
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
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

        private void toolStripButtonActualizar_Click_1(object sender, EventArgs e)
        {
            try
            {
                ultraGridPacientes.DataSource = Negocio.NegPacientes.getAtencionesIngresos(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkHC.Checked, Convert.ToInt32(txt_historiaclinica.Text), ckbestado.Checked, chbDivididas.Checked);

                if (chbDivididas.Checked == true)
                {

                    //Oculto Atenciones con el mismo numero de atencion
                    string numAnterior = "0";
                    foreach (UltraGridRow row in ultraGridPacientes.Rows)
                    {
                        if (numAnterior == "0")
                        {
                            numAnterior = row.Cells["Nº ATENCION"].Value.ToString().Trim();
                        }
                        else
                        {
                            if (row.Cells["Nº ATENCION"].Value.ToString().Trim() == numAnterior)
                            {
                                row.Hidden = true;
                            }
                            numAnterior = row.Cells["Nº ATENCION"].Value.ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void chkHC_CheckedChanged(object sender, EventArgs e)
        {
            txt_historiaclinica.Enabled = chkHC.Checked;
            ayudaPacientes.Visible = chkHC.Checked;
        }

        private void toolStripButtonSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ultraGridPacientes_InitializeLayout_1(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            //Caracteristicas de Filtro en la grilla
            ultraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            ultraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            //VARIABLE PARA PODER OCULTAR COLUMNAS
           // ultraGridPacientes.DisplayLayout.Bands[0].Columns[0].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["HAB_CODIGO"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_CODIGO"].Hidden = true;
            //var gridBand = ultraGridPacientes.DisplayLayout.Bands[0];

            //gridBand.Columns["ATE_CODIGO"].Hidden = false;
            //gridBand.Columns["HAB_CODIGO"].Hidden = false;
            //gridBand.Columns["PAC_CODIGO"].Hidden = false;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
