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
using His.Entidades;
using Infragistics.Win.UltraWinGrid;

namespace His.Admision
{
    public partial class frm_ExploradorRubros : Form
    {
        public frm_ExploradorRubros()
        {
            InitializeComponent();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refresh()
        {
         
            //grid.DataSource = Negocio.NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), true, false, false, false, 0, false, 0, false, 0, false, "");
            try
            {
                if (chkPiso.Checked || chkHabitaciones.Checked)
                {
                    grid.DataSource = NegDietetica.getExploradorRubrosNew2(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.Date,
                    txt_historiaclinica.Text, chkIngreso.Checked,chkPiso.Checked,chkHabitaciones.Checked,Convert.ToInt64(cmb_habitaciones.SelectedValue.ToString()),
                    Convert.ToInt64(cmb_piso.SelectedValue.ToString()));
                }
                else
                {
                grid.DataSource = NegDietetica.getExploradorRubrosNew(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.Date,
                    txt_historiaclinica.Text, chkIngreso.Checked, chkAlta.Checked, chkTratamiento.Checked,
                    Convert.ToInt32(cmb_areas.SelectedValue.ToString()), chbTipoIngreso.Checked, Convert.ToInt32(cmb_subareas.SelectedValue.ToString()),
                    chkFormulario.Checked, ckbDepartamento.Checked, cmbDepartamento.SelectedValue.ToString());
                }
                //grid.DataSource = NegDietetica.getExploradorRubros(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cmb_areas.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_subareas.SelectedValue), chkHC.Checked, Convert.ToInt32(txt_historiaclinica.Text), chkFormulario.Checked, cmb_facturacion.Text.Trim(), ckbDepartamento.Checked);
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (grid.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(grid, PathExcel);
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

        private void frm_ExploradorRubros_Load(object sender, EventArgs e)
        {

            // grupo_facturacion
            cmb_facturacion.DataSource = NegDietetica.getDataTable("getGrupoRubros");
            cmb_facturacion.DisplayMember = "RUB_GRUPO";
            //areas
            cmb_areas.DataSource = NegPedidos.recuperarListaAreasTodas().OrderBy(a => a.PEA_NOMBRE).ToList();
            cmb_areas.ValueMember = "PEA_CODIGO";
            cmb_areas.DisplayMember = "PEA_NOMBRE";
            cmb_areas.SelectedIndex = 0;
            //subareas
            cmb_subareas.DataSource = NegRubros.recuperarRubros().OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();
            cmb_subareas.DisplayMember = "RUB_NOMBRE".Trim();
            cmb_subareas.ValueMember = "RUB_CODIGO";
            cmb_areas.SelectedIndex = 0;
            //grid
            //dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            dtpFiltroDesde.Value = DateTime.Now.AddDays(-7);
            dtpFiltroHasta.Value = DateTime.Now;

            //departamento por el producto
            cmbDepartamento.DataSource = NegRubros.getDepartamento();
            cmbDepartamento.DisplayMember = "desdep";
            cmbDepartamento.ValueMember = "coddep";
            cmbDepartamento.SelectedIndex = 0;

            //carga de los pisos
            cmb_piso.DataSource = NegMaintenance.cargarComboNivelPiso();
            cmb_piso.DisplayMember = "NIV_NOMBRE";
            cmb_piso.ValueMember = "NIV_CODIGO";

            cmb_habitaciones.DataSource = NegHabitaciones.listaHabitaciones();
            cmb_habitaciones.DisplayMember = "hab_Numero";
            cmb_habitaciones.ValueMember = "hab_Codigo";

            refresh();
        }

        private void cmb_subareas_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    try
        //    {
        //        cmb_subareas.DataSource = null;
        //        if (cmb_subareas.SelectedItem != null)
        //        {
        //            PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_subareas.SelectedItem;
        //            List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
        //            if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
        //                cmb_subareas.DataSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
        //            else
        //                cmb_subareas.DataSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();

        //            cmb_subareas.DisplayMember = "RUB_NOMBRE".Trim();
        //            cmb_subareas.ValueMember = "RUB_CODIGO";
        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message);
        //    }
        }

        private void chkTratamiento_CheckedChanged(object sender, EventArgs e)
        {
            cmb_areas.Enabled = chkTratamiento.Checked;
            //if (chkTratamiento.Checked)
            //{
            //    try
            //    {
            //        cmb_subareas.DataSource = null;
            //        if (cmb_subareas.SelectedItem != null)
            //        {
            //            PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_subareas.SelectedItem;
            //            List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
            //            if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
            //                cmb_subareas.DataSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
            //            else
            //                cmb_subareas.DataSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();

            //            cmb_subareas.DisplayMember = "RUB_NOMBRE".Trim();
            //            cmb_subareas.ValueMember = "RUB_CODIGO";
            //        }
            //    }
            //    catch (Exception err)
            //    {
            //        MessageBox.Show(err.Message);
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        cmb_subareas.DataSource = null;
            //        if (cmb_subareas.SelectedItem != null)
            //        {
            //            PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_subareas.SelectedItem;
            //            List<RUBROS> listaRubros = NegRubros.recuperarRubros();
            //            if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
            //                cmb_subareas.DataSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
            //            else
            //                cmb_subareas.DataSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();

            //            cmb_subareas.DisplayMember = "RUB_NOMBRE".Trim();
            //            cmb_subareas.ValueMember = "RUB_CODIGO";
            //        }
            //    }
            //    catch (Exception err)
            //    {
            //        MessageBox.Show(err.Message);
            //    }
            //}
        }

        private void chbTipoIngreso_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTratamiento.Checked)
            {
                cmb_subareas.Enabled = chbTipoIngreso.Checked;
                try
                {
                    cmb_subareas.DataSource = NegPedidos.Area_SubArea(Convert.ToInt32(cmb_areas.SelectedValue.ToString()));
                    cmb_subareas.DisplayMember = "RUB_NOMBRE";
                    cmb_subareas.ValueMember = "RUB_CODIGO";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo ocurrio al cargar el subarea con respecto al area. Mas detalles: " + ex.Message,
                        "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                cmb_subareas.Enabled = chbTipoIngreso.Checked;
                cmb_subareas.DataSource = NegRubros.recuperarRubros().OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();
                cmb_subareas.DisplayMember = "RUB_NOMBRE".Trim();
                cmb_subareas.ValueMember = "RUB_CODIGO";
                cmb_areas.SelectedIndex = 0;
            }

        }

        private void chkFormulario_CheckedChanged(object sender, EventArgs e)
        {
            cmb_facturacion.Enabled = chkFormulario.Checked;
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
                //MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_historiaclinica_Leave(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text == "")
                txt_historiaclinica.Text = "0";
        }

        private void chkHC_CheckedChanged(object sender, EventArgs e)
        {
            txt_historiaclinica.Enabled = chkHC.Checked;
            ayudaPacientes.Visible = chkHC.Checked;
            txt_historiaclinica.Text = "0";
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;



            //Cambio la apariencia de las sumas
            bandUno.Summaries.Clear();
            bandUno.SummaryFooterCaption = "Totales: ";
            bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.FromArgb(189, 191, 191);
            bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.Black;
            //totalizo las columnas
            SummarySettings sumHonorarios = bandUno.Summaries.Add("Unitario", SummaryType.Sum, bandUno.Columns["V. UNITARIO"]);
            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
            sumHonorarios.DisplayFormat = "{0:#####.00}";
            sumHonorarios.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
            SummarySettings sumComision = bandUno.Summaries.Add("Cantidad", SummaryType.Sum, bandUno.Columns["CANTIDAD"]);
            //sumComision.DisplayFormat = "Tot = {0:#####.00}";
            sumComision.DisplayFormat = "{0:#####.00}";
            sumComision.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
            SummarySettings sumReferido = bandUno.Summaries.Add("Valor", SummaryType.Sum, bandUno.Columns["VALOR"]);
            //sumReferido.DisplayFormat = "Tot = {0:#####.00}";
            sumReferido.DisplayFormat = "{0:#####.00}";
            sumReferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumRetenido = bandUno.Summaries.Add("Iva", SummaryType.Sum, bandUno.Columns["IVA"]);
            sumRetenido.DisplayFormat = "{0:#####.00}";
            sumRetenido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorPagar = bandUno.Summaries.Add("Total", SummaryType.Sum, bandUno.Columns["TOTAL"]);
            sumValorPagar.DisplayFormat = "{0:#####.00}";
            sumValorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorRecuperado = bandUno.Summaries.Add("Descuento", SummaryType.Sum, bandUno.Columns["DESCUENTO"]);
            sumValorRecuperado.DisplayFormat = "{0:#####.00}";
            sumValorRecuperado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            //SummarySettings sumValorPorRecuperar = bandUno.Summaries.Add("Valor Por Recuperar", SummaryType.Sum, bandUno.Columns["VALOR_POR_RECUPERAR"]);
            //sumValorPorRecuperar.DisplayFormat = "{0:#####.00}";
            //sumValorPorRecuperar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            //SummarySettings sumValorCancelado = bandUno.Summaries.Add("Valor Cancelado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_CANCELADO"]);
            //sumValorCancelado.DisplayFormat = "{0:#####.00}";
            //sumValorCancelado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
        }

        private void ckbDepartamento_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDepartamento.Checked)
                cmbDepartamento.Enabled = true;
            else
                cmbDepartamento.Enabled = false;
        }

        private void chkPiso_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPiso.Checked)
            {
                cmb_piso.Enabled = true;
                ultraGroupBox2.Enabled = false;
            }
            else
            {
                cmb_piso.Enabled = false;
                ultraGroupBox2.Enabled = true;
            }

        }

        private void chkHabitaciones_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cmb_habitaciones.Enabled = chkHabitaciones.Checked;
                if (chkHabitaciones.Checked)
                    ultraGroupBox2.Enabled = false;
                else
                    ultraGroupBox2.Enabled = true;

                if (chkPiso.Checked)
                {
                    cmb_habitaciones.DataSource = NegHabitaciones.listaHabitacionesXpiso(Convert.ToInt64(cmb_piso.SelectedValue.ToString()));
                    cmb_habitaciones.DisplayMember = "hab_Numero";
                    cmb_habitaciones.ValueMember = "hab_Codigo";
                }
                else
                {
                    cmb_habitaciones.DataSource = NegHabitaciones.listaHabitaciones();
                    cmb_habitaciones.DisplayMember = "hab_Numero";
                    cmb_habitaciones.ValueMember = "hab_Codigo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar la habitacion con respecto al piso. Mas detalles: " + ex.Message,
                        "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmb_piso_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_piso.SelectedIndex >0)
            {
                cmb_habitaciones.DataSource = NegHabitaciones.listaHabitacionesXpiso(Convert.ToInt64(cmb_piso.SelectedValue.ToString()));
                cmb_habitaciones.DisplayMember = "hab_Numero";
                cmb_habitaciones.ValueMember = "hab_Codigo";
            }
        }
    }
}
