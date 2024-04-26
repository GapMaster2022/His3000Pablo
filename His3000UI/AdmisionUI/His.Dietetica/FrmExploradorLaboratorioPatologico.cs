using His.Admision;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class FrmExploradorLaboratorioPatologico : Form
    {
        public Int64 FiltroHC;
        public FrmExploradorLaboratorioPatologico()
        {
            InitializeComponent();
            cargarTipoIngreso();
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ultraGridPacientes.DataSource = Negocio.NegPacientes.getPedidosLaboratorioPatologico(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'01"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkHC.Checked, Convert.ToInt32(txt_historiaclinica.Text), chkFormulario.Checked, cmbFormulario.Text.Trim(), false);
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
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

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_AyudaPacientes form = new frm_AyudaPacientes();
            form.campoPadre = txt_historiaclinica;
            form.ShowDialog();
            form.Dispose();
            txt_historiaclinica.Text = txt_historiaclinica.Text.Trim();
        }

        private void chkHC_CheckedChanged(object sender, EventArgs e)
        {
            txt_historiaclinica.Enabled = chkHC.Checked;
            ayudaPacientes.Visible = chkHC.Checked;
        }

        private void chkTratamiento_CheckedChanged(object sender, EventArgs e)
        {
            cmb_tipoatencion.Enabled = chkTratamiento.Checked;
        }

        private void chbTipoIngreso_CheckedChanged(object sender, EventArgs e)
        {
            cboTipoIngreso.Enabled = chbTipoIngreso.Checked;
        }

        private void ultraGridPacientes_DoubleClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Desea cargar la ventana del formulario?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string ateCodigo = ultraGridPacientes.ActiveRow.Cells["ATE_CODIGO"].Value.ToString();
                    int codigoAtencion = Convert.ToInt32(ateCodigo);
                    His.AdminHistoriasClinicas.txtNombrePaciente exploradorHC = new His.AdminHistoriasClinicas.txtNombrePaciente(codigoAtencion);
                    exploradorHC.Show();

                    string c = ultraGridPacientes.ActiveRow.Cells["TIPO"].Value.ToString();
                    if (c.Equals("ANAMNESIS"))
                    {
                        His.Formulario.frm_Anemnesis evolucion = new His.Formulario.frm_Anemnesis(codigoAtencion, false);
                        evolucion.MdiParent = exploradorHC;
                        evolucion.Show();
                    }                    
                    else if (c.Equals("HISTOPATOLOGICO"))
                    {
                        His.Formulario.frm_Histopatologico013A evolucion = new His.Formulario.frm_Histopatologico013A(codigoAtencion);
                        evolucion.MdiParent = exploradorHC;
                        evolucion.Show();
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
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

        private void FrmExploradorLaboratorioPatologico_Load(object sender, EventArgs e)
        {
            try
            {
                FiltroHC = Convert.ToInt64(txt_historiaclinica.Text);
                dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
                dtpFiltroHasta.Value = DateTime.Now;

                if (FiltroHC == 0)
                {
                    ultraGridPacientes.DataSource = Negocio.NegPacientes.getPedidosLaboratorioPatologico(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'01"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), true, false, false, false, 0, false, 0, false, 0, false, "", false);
                }
                else
                {
                    ultraGridPacientes.DataSource = Negocio.NegPacientes.getPedidosLaboratorioPatologico(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'01"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), false, false, false, false, 0, false, 0, true, Convert.ToInt32(FiltroHC), false, "", false);
                    chkHC.Checked = true;
                    txt_historiaclinica.Text = FiltroHC.ToString();
                    chkIngreso.Checked = false;
                    ultraPanel1.Visible = false;
                }

            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }
    }
}
