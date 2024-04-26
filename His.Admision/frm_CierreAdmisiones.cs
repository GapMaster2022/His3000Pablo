using Core.Datos;
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
using His.Formulario;

namespace His.Admision
{
    public partial class frm_CierreAdmisiones : Form
    {
        private object xAux;

        public frm_CierreAdmisiones()
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

            cmbUsuario.DataSource = Negocio.NegMaintenance.getDataTable("Usuarios");
            cmbUsuario.DisplayMember = "NOMBRE";
            cmbUsuario.ValueMember = "ID";
            cmbUsuario.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbUsuario.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cmbUsuario.SelectedValue = His.Entidades.Clases.Sesion.codUsuario; // Sesion.codUsuario.ToString();

            cmb_tipoatencion.DataSource = NegTipoTratamiento.RecuperaTipoTratamiento(); ;
            cmb_tipoatencion.DisplayMember = "TIA_DESCRIPCION";
            cmb_tipoatencion.ValueMember = "TIA_CODIGO";
            cmb_tipoatencion.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmb_tipoatencion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
        private void frm_ExploradorIngresos_Load(object sender, EventArgs e)
        {
            try
            {
                //dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
                dtpFiltroDesde.Value = DateTime.Now;
                dtpFiltroHasta.Value = DateTime.Now;
                //ultraGridPacientes.DataSource = Negocio.NegPacientes.getAtencionesIngresos(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), true, false, false, false, 0, false, 0, false, 0);
                refrescar();
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
        
        private void dtpFiltroDesde_ValueChanged(object sender, EventArgs e)
        {

        }
        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ultraPanel1_PaintClient(object sender, PaintEventArgs e)
        {

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
            refrescar();
        }

        private void refrescar()
        {
            try
            {
                  ultraGridPacientes.DataSource = Negocio.NegPacientes.getAtencionesCierre(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'01"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkUsuario.Checked, Convert.ToInt32(cmbUsuario.SelectedValue));

                //DataTable admis = Negocio.NegPacientes.getCierreAdmision(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkUsuario.Checked, Convert.ToInt32(cmbUsuario.SelectedValue));
                //DataTable fact = Negocio.NegPacientes.getCierreFacturacion(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkUsuario.Checked, Convert.ToInt32(cmbUsuario.SelectedValue));
                //DataTable alta = Negocio.NegPacientes.getCierreAlta(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkUsuario.Checked, Convert.ToInt32(cmbUsuario.SelectedValue));
                //admis.Merge(fact);
                //admis.Merge(alta);

                //ultraGridPacientes.DataSource = admis;

                        var gridBand = ultraGridPacientes.DisplayLayout.Bands[0];
                        ///all columns read only
                        for (int i = 0; i < gridBand.Columns.Count; i++)
                        {
                            gridBand.Columns[i].CellActivation = Activation.NoEdit;
                        }
                        //i activate the check column
                        //gridBand.Columns["OBSERVACION_"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.;
                        gridBand.Columns["OBSERVACION_"].CellActivation = Activation.AllowEdit;
                       // gridBand.Columns["OBS_CONVENIO"].Hidden = true;
                       // gridBand.Columns["ATE_NUMERO"].Hidden = true;
                        //gridBand.Columns["NOMBRE_USUARIO"].Hidden = true;
                       // gridBand.Columns["ID_USUARIO"].Hidden = true;
                


            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }



        private void toolStripButtonSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkUsuario_CheckedChanged(object sender, EventArgs e)
        {
            cmbUsuario.Enabled = chkUsuario.Checked;
        }

      

     

        private void ultraGridPacientes_BeforeExitEditMode(object sender, BeforeExitEditModeEventArgs e)
        {
            if (e.CancellingEditOperation)
                return;
            try
            {
                //if (this.ultraGridPacientes.ActiveCell.Column.Key == "PAD_VALOR")
                {

                    if (xAux.ToString() != this.ultraGridPacientes.ActiveCell.Text)
                    {
                        //DialogResult result = MessageBox.Show("Confirma que desea cambiar el valor del parametro?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        //if (result == DialogResult.Yes)
                        if (His.Entidades.Clases.Sesion.codUsuario == Convert.ToUInt32(ultraGridPacientes.ActiveRow.Cells["ID_USUARIO"].Value))
                        {
                            //if (this.grid.ActiveCell.Text == "")
                            //    this.grid.ActiveCell.Value = "";
                            //setDetalleParametros();
                            string[] x = new string[] {
                            ultraGridPacientes.ActiveRow.Cells["ATE_CODIGO"].Value.ToString().Trim(),
                           // ultraGridPacientes.ActiveRow.Cells["TIPO_"].Value.ToString().Trim(),
                           "ADMISION",
                            this.ultraGridPacientes.ActiveCell.Text,
                            //ultraGridPacientes.ActiveRow.Cells["OBSERVACION_"].Value.ToString().Trim(),
                            ultraGridPacientes.ActiveRow.Cells["ID_USUARIO"].Value.ToString().Trim()
                        };
                            NegDietetica.setROW("setBitacora", x);
                            return;
                        }
                        else
                        {
                            this.ultraGridPacientes.ActiveCell.CancelUpdate();
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {

                try
                {
                  ///  this.ultraGridPacientes.ActiveCell.CancelUpdate();
                }
                catch (Exception)
                {

                }
            }
          
        }

        private void ultraGridPacientes_BeforeEnterEditMode(object sender, CancelEventArgs e)
        {
            xAux = this.ultraGridPacientes.ActiveCell.Text;
        }

        private void chbTipoIngreso_CheckedChanged(object sender, EventArgs e)
        {
            cboTipoIngreso.Enabled = chbTipoIngreso.Checked;
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            NegCertificadoMedico C = new NegCertificadoMedico();
            CierreTurno CT = new CierreTurno();
            if (ultraGridPacientes.Rows.Count > 0)
            {
                DataRow dr;
                foreach (var item in ultraGridPacientes.Rows)
                {
                    if (item.Cells["OBSERVACION_"].Value.ToString() != "")
                    {
                        dr = CT.Tables["CIERRE_TURNO"].NewRow();
                        dr["Observacion"] = item.Cells["OBSERVACION_"].Value.ToString();
                        dr["Paciente"] = item.Cells["NOMBRE_PACIENTE"].Value.ToString();
                        dr["Habitacion"] = item.Cells["HABIT"].Value.ToString();
                        dr["Hc"] = item.Cells["HC"].Value.ToString();
                        dr["Atencion"] = item.Cells["ATE_NUMERO"].Value.ToString();
                        dr["Identificacion"] = item.Cells["IDENTIFICACION"].Value.ToString();
                        dr["Usuario"] = His.Entidades.Clases.Sesion.nomUsuario;
                        dr["FechaIngreso"] = item.Cells["FECHA_INGRESO"].Value.ToString();
                        dr["Logo"] = C.path();
                        CT.Tables["CIERRE_TURNO"].Rows.Add(dr);
                    }
                }
                His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "CierreAdmision", CT);
                myreport.Show();
                //CierredeTurno reporte = new CierredeTurno();//el nombre del reporte crystal
                //reporte.SetDataSource(CT);
                //CrystalDecisions.Windows.Forms.CrystalReportViewer vista = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                //vista.ReportSource = reporte;
                //vista.PrintReport();
            }
            else
            {
                MessageBox.Show("No hay registros para imprimir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

