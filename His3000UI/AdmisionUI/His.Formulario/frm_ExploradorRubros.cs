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
using His.Admision;

namespace His.ConsultaExterna
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
                grid.DataSource = Negocio.NegDietetica.getExploradorRubros(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cmb_areas.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_subareas.SelectedValue), chkHC.Checked, Convert.ToInt32(txt_historiaclinica.Text), chkFormulario.Checked, cmb_facturacion.Text.Trim(), false);
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
            cmb_subareas.Enabled = chbTipoIngreso.Checked;
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
        }
    }
}
