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

namespace His.Honorarios
{
    public partial class frm_ExploradorFormularios : Form
    {
        public Int64 FiltroHC;
        public frm_ExploradorFormularios()
        {
            InitializeComponent();
            cargarTipoIngreso();           
        }
        ////public frm_ExploradorFormularios(Int64 HC = 0)
        ////{
        ////    InitializeComponent();
        ////    cargarTipoIngreso();
        ////    FiltroHC = HC;
        ////}
        //public frm_ExploradorFormularios()
        //{
        //    InitializeComponent();
        //    cargarTipoIngreso();

        //}

        private void ultraGridPacientes_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            //Caracteristicas de Filtro en la grilla
            ultraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            ultraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            //Dimension los registros
            //ultraGridPacientes.DisplayLayout.Bands[0].Columns[0].Width = 120;
            //ultraGridPacientes.DisplayLayout.Bands[0].Columns[1].Width = 60;
            //ultraGridPacientes.DisplayLayout.Bands[0].Columns[2].Width = 60;
            //ultraGridPacientes.DisplayLayout.Bands[0].Columns[3].Width = 260;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns[4].Width = 300;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns[15].Width = 300;
            //ultraGridPacientes.DisplayLayout.Bands[0].Columns[6].Width = 120;
            //ultraGridPacientes.DisplayLayout.Bands[0].Columns[7].Width = 220;

            //Ocultar columnas, que son fundamentales al levantar informacion
            //ultraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_CODIGO"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["ATE_CODIGO"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["HAB_CODIGO"].Hidden = true;
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
        private void ExploradorFormularios_Load(object sender, EventArgs e)
        {
            try
            {
                FiltroHC = Convert.ToInt64(txt_historiaclinica.Text);
                //dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
                //dtpFiltroHasta.Value = DateTime.Now;
                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;
                date = date.AddDays(-5);
                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, date.Day);
                dtpFiltroDesde.Value = oPrimerDiaDelMes;

                if (FiltroHC == 0)
                {
                    //ultraGridPacientes.DataSource = Negocio.NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), true, false, false, false, 0, false, 0, false, 0, false, "");
                    ultraGridPacientes.DataSource = Negocio.NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), true, false, false, false, 0, false, 0, false, 0, false, "", false);

                }
                else
                {
                    //ultraGridPacientes.DataSource = Negocio.NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), false, false, false, false, 0, false, 0, true, Convert.ToInt32(FiltroHC), false, "");
                    ultraGridPacientes.DataSource = Negocio.NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), false, false, false, false, 0, false, 0, true, Convert.ToInt32(FiltroHC), false, "", false);

                    chkHC.Checked = true;
                    txt_historiaclinica.Text = FiltroHC.ToString();
                    chkIngreso.Checked = false;
                    ultraPanel1.Visible = false;
                }
                string numAnterior = "0";
                string tipo = "";
                foreach (UltraGridRow row in ultraGridPacientes.Rows)
                {
                    if (numAnterior == "0")
                    {
                        numAnterior = row.Cells["NUMERO ATENCION"].Value.ToString().Trim();
                        tipo = row.Cells["TIPO"].Value.ToString();
                    }
                    else
                    {
                        if (row.Cells["NUMERO ATENCION"].Value.ToString().Trim() == numAnterior && row.Cells["TIPO"].Value.ToString() == tipo)
                        {
                            row.Hidden = true;
                        }
                        numAnterior = row.Cells["NUMERO ATENCION"].Value.ToString().Trim();
                        tipo = row.Cells["TIPO"].Value.ToString();
                    }
                }

            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                //ultraGridPacientes.DataSource = NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkHC.Checked, Convert.ToInt32(txt_historiaclinica.Text),  chkFormulario.Checked, cmbFormulario.Text.Trim());
                ultraGridPacientes.DataSource = NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkHC.Checked, Convert.ToInt32(txt_historiaclinica.Text), chkFormulario.Checked, cmbFormulario.Text.Trim(), false) ;

                //ultraGridPacientes.DataSource = NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.ToString(), dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59).ToString(), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkHC.Checked, Convert.ToInt32(txt_historiaclinica.Text), chkFormulario.Checked, cmbFormulario.Text.Trim());

                string numAnterior = "0";
                string tipo = "";
                foreach (UltraGridRow row in ultraGridPacientes.Rows)
                {
                    if (numAnterior == "0")
                    {
                        numAnterior = row.Cells["NUMERO ATENCION"].Value.ToString().Trim();
                        tipo = row.Cells["TIPO"].Value.ToString();
                    }
                    else
                    {
                        if (row.Cells["NUMERO ATENCION"].Value.ToString().Trim() == numAnterior && row.Cells["TIPO"].Value.ToString() == tipo)
                        {
                            row.Hidden = true;
                        }
                        numAnterior = row.Cells["NUMERO ATENCION"].Value.ToString().Trim();
                    }
                }
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

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chbTipoIngreso_CheckedChanged(object sender, EventArgs e)
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

        private void cboTipoIngreso_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(cboTipoIngreso.SelectedValue.ToString()); //codigo en bdd de item
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            His.Formulario.frm_AyudaPacientes form = new Formulario.frm_AyudaPacientes();
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

        private void chkTratamiento_CheckedChanged(object sender, EventArgs e)
        {
                cmb_tipoatencion.Enabled = chkTratamiento.Checked;
        }

        private void chkHC_CheckedChanged(object sender, EventArgs e)
        {
                txt_historiaclinica.Enabled = chkHC.Checked;
                ayudaPacientes.Visible = chkHC.Checked; 
        }

        private void txt_historiaclinica_Leave(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text == "")
                txt_historiaclinica.Text = "0";
        }

        private void chkFormulario_CheckedChanged(object sender, EventArgs e)
        {
                cmbFormulario.Enabled = chkFormulario.Checked;
        }

        private void ultraGridPacientes_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{

            //        ultraGridPacientes.ContextMenuStrip = MnuCtxHC;

            //}
        }

        private void historiaClinicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ultraGridPacientes_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {

        }

        private void ultraGridPacientes_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Desea cargar la ventana del formulario?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string ateCodigo = ultraGridPacientes.ActiveRow.Cells["ATE_CODIGO"].Value.ToString();
                    int codigoAtencion = Convert.ToInt32(ateCodigo);
                    ATENCIONES atenncion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ateCodigo));
                    //AdminHistoriasClinicas.txtNombrePaciente exploradorHC = new AdminHistoriasClinicas.txtNombrePaciente(codigoAtencion);
                    //exploradorHC.Show();
                    frm_ExploradorHCNew exploradorHC = new frm_ExploradorHCNew(codigoAtencion);
                    exploradorHC.Show();

                    string c = ultraGridPacientes.ActiveRow.Cells["TIPO"].Value.ToString();
                    if (c.Equals("ANAMNESIS"))
                    {
                        FORMULARIOS_HCU parametroFecha = NegParametros.RecuperaFechaPorCodigo(3);
                        ATENCIONES ate = NegAtenciones.RecuperarAtencionID(codigoAtencion);
                        HC_ANAMNESIS anamnesis = NegAnamnesis.recuperarAnamnesisPorAtencion(ate.ATE_CODIGO);
                        if (anamnesis != null)
                        {
                            if (Convert.ToDateTime(parametroFecha.fecha) <= anamnesis.ANE_FECHA)
                            {
                                His.Formulario.frmAnamnesisNew evolucion1 = new His.Formulario.frmAnamnesisNew(codigoAtencion, false);

                                evolucion1.StartPosition = FormStartPosition.CenterParent;
                                evolucion1.MdiParent = exploradorHC;
                                evolucion1.Show();
                            }
                            else
                            {
                                His.Formulario.frm_Anemnesis evolucion = new His.Formulario.frm_Anemnesis(codigoAtencion);
                                //His.Formulario.frm_Anemnesis evolucion = new His.Formulario.frm_Anemnesis(codigoAtencion, false);
                                evolucion.StartPosition = FormStartPosition.CenterParent;
                                evolucion.MdiParent = exploradorHC;
                                evolucion.Show();
                            }

                        }
                        else
                        {
                            His.Formulario.frmAnamnesisNew evolucion1 = new His.Formulario.frmAnamnesisNew(codigoAtencion, false);
                            evolucion1.StartPosition = FormStartPosition.CenterParent;
                            evolucion1.MdiParent = exploradorHC;
                            evolucion1.Show();
                        }



                    }
                    else if (c.Equals("EMERGENCIA"))
                    {


                        //His.Formulario.frm_Emergencia evolucion = new His.Formulario.frm_Emergencia(codigoAtencion);
                        //evolucion.MdiParent = exploradorHC;
                        //evolucion.Show();

                        FORMULARIOS_HCU parametroFecha = NegParametros.RecuperaFechaPorCodigo(7);
                        ATENCIONES ate = NegAtenciones.RecuperarAtencionID(codigoAtencion);


                        if (Convert.ToDateTime(parametroFecha.fecha) <= ate.ATE_FECHA_INGRESO)
                        {
                            His.Emergencia.frm_EmergenciaNew emerge = new His.Emergencia.frm_EmergenciaNew(codigoAtencion);
                            emerge.MdiParent = exploradorHC;
                            emerge.Show();
                        }
                        else
                        {
                            His.Formulario.frm_Emergencia evolucion1 = new His.Formulario.frm_Emergencia(codigoAtencion);
                            evolucion1.MdiParent = exploradorHC;
                            evolucion1.Show();
                        }


                    }
                    else if (c.Equals("EPICRISIS"))
                    {
                        His.Formulario.frm_Epicrisis evolucion = new His.Formulario.frm_Epicrisis(codigoAtencion);
                        evolucion.MdiParent = exploradorHC;
                        evolucion.Show();
                    }
                    else if (c.Equals("EVOLUCION"))
                    {
                        His.Formulario.frm_Evolucion evolucion = new His.Formulario.frm_Evolucion(codigoAtencion, false);
                        evolucion.MdiParent = exploradorHC;
                        evolucion.Show();
                    }
                    else if (c.Equals("IMAGENOLOGIA"))
                    {
                        His.Formulario.frm_Imagen evolucion = new His.Formulario.frm_Imagen(codigoAtencion);
                        evolucion.MdiParent = exploradorHC;
                        evolucion.Show();
                    }
                    else if (c.Equals("INTERCONSULTA"))
                    {
                        His.Formulario.frm_Interconsulta evolucion = new His.Formulario.frm_Interconsulta(codigoAtencion);
                        evolucion.MdiParent = exploradorHC;
                        evolucion.Show();
                    }
                    else if (c.Equals("LABORATORIO"))
                    {
                        His.Formulario.frm_LaboratorioClinico evolucion = new His.Formulario.frm_LaboratorioClinico();
                        evolucion.MdiParent = exploradorHC;
                        evolucion.Show();
                    }
                    else if (c.Equals("PROTOCOLO"))
                    {
                        His.Formulario.frm_Protocolo evolucion = new His.Formulario.frm_Protocolo(codigoAtencion, Convert.ToInt32(ultraGridPacientes.ActiveRow.Cells["ID_Formulario"].Value.ToString()));
                        evolucion.MdiParent = exploradorHC;
                        evolucion.Show();
                    }
                    else if(c.Equals("CONSULTA EXTERNA"))
                    {
                        His.ConsultaExterna.Consulta consulta = new ConsultaExterna.Consulta(Convert.ToString(codigoAtencion));
                        //consulta.explorador = true;
                        consulta.MdiParent = exploradorHC;
                        //consulta.explorador = true;
                        consulta.Show();
                        
                    }
                    else if (c.Equals("EVOLUCION ENFERMERIA"))
                    {
                        His.Formulario.frmEvolucionEnfermeria consulta = new Formulario.frmEvolucionEnfermeria(codigoAtencion);
                        //consulta.explorador = true;
                        consulta.MdiParent = exploradorHC;
                        //consulta.explorador = true;
                        consulta.Show();

                    }
                    else if (c.Equals("HOJA DE ALTA"))
                    {
                        MessageBox.Show("Imprima la hoja de alta desde el modulo de admision","HIS3000",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        //  frm_Egreso_preview consulta = new frm_Egreso_preview(Convert.ToString(codigoAtencion));
                        ////consulta.explorador = true;
                        //consulta.MdiParent = exploradorHC;
                        ////consulta.explorador = true;
                        //consulta.Show();

                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }
           
    }
}

