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
using His.DatosReportes;
using His.Entidades.Clases;
using Infragistics.Win.UltraWinGrid;

namespace His.Admision
{
    public partial class frm_ExploradorFormularios : Form
    {
        public Int64 FiltroHC;
        public bool _ayudaSubsecuentes = false;
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
            ultraGridPacientes.DisplayLayout.Bands[0].Columns[0].Width = 120;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns[17].Width = 120;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns[18].Width = 120;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns[4].Width = 300;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns[15].Width = 300;

            //Ocultar columnas, que son fundamentales al levantar informacion
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["ID_Formulario"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["ATE_CODIGO"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["HAB_CODIGO"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_CODIGO"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["ADS_ASEGURADO_NOMBRE"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["ADS_ASEGURADO_CEDULA"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["PARENTESCO"].Hidden = true;
            ultraGridPacientes.DisplayLayout.Bands[0].Columns["HAB ESTADO"].Hidden = true;



            ultraGridPacientes.DisplayLayout.Bands[0].Columns[0].Format = "dd/MM/yyyy hh:mm:ss";
            ultraGridPacientes.DisplayLayout.Bands[0].Columns[17].Format = "dd/MM/yyyy hh:mm:ss";
            ultraGridPacientes.DisplayLayout.Bands[0].Columns[18].Format = "dd/MM/yyyy hh:mm:ss";
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
        bool mushugñan = false;
        private void ExploradorFormularios_Load(object sender, EventArgs e)
        {
            try
            {
                List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(Sesion.codUsuario);

                foreach (var item in perfilUsuario)
                {
                    List<ACCESO_OPCIONES> accop = NegUtilitarios.ListaAccesoOpcionesPorPerfil(item.ID_PERFIL, 7);
                    foreach (var items in accop)
                    {
                        if (items.ID_ACCESO == 71110)// se cambia del perfil  29 a opcion 71110// Mario Valencia 14/11/2023 // cambio en seguridades.
                        {
                            mushugñan = true;
                        }
                    }
                    //if (item.ID_PERFIL == 29)
                    //{
                    //    if (item.DESCRIPCION.Contains("SUCURSALES")) //se debe tomar en cuenta que si es 29 en otra empresa no actuara de la forma como en la pasteur.
                    //        mushugñan = true;
                    //}
                }
                //FiltroHC =Convert.ToInt64(txt_historiaclinica.Text);

                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                dtpFiltroDesde.Value = oPrimerDiaDelMes;

                if (FiltroHC == 0)
                {
                    //ultraGridPacientes.DataSource = Negocio.NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), true, false, false, false, 0, false, 0, false, 0, false, "");
                    ultraGridPacientes.DataSource = Negocio.NegPacientes.getExploradorFormulariosCext(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), true, false, false, false, 0, false, 0, false, 0, false, "", mushugñan, areaAsignada());

                }
                else
                {
                    //ultraGridPacientes.DataSource = Negocio.NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), false, false, false, false, 0, false, 0, true, Convert.ToInt32(FiltroHC), false, "");
                    ultraGridPacientes.DataSource = Negocio.NegPacientes.getExploradorFormulariosCext(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), false, false, false, false, 0, false, 0, true, Convert.ToInt32(FiltroHC), false, "", mushugñan, areaAsignada());

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
        public Int16 areaAsignada()
        {
            Int16 AreaUsuario = 1;
            DataTable codigoAreaAsignada = NegUsuarios.AreaAsignada(Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario));
            bool parse = Int16.TryParse(codigoAreaAsignada.Rows[0][0].ToString(), out AreaUsuario);
            if (parse)
            {
                return AreaUsuario;
            }
            else
                return AreaUsuario;
        }
        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                //ultraGridPacientes.DataSource = NegPacientes.getAtencionesFormularios(dtpFiltroDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), dtpFiltroHasta.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkHC.Checked, Convert.ToInt32(txt_historiaclinica.Text),  chkFormulario.Checked, cmbFormulario.Text.Trim());
                ultraGridPacientes.DataSource = NegPacientes.getExploradorFormulariosCext(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value.AddHours(23).AddMinutes(59).AddSeconds(59), chkIngreso.Checked, chkAlta.Checked, chkFacturacion.Checked, chbTipoIngreso.Checked, Convert.ToInt32(cboTipoIngreso.SelectedValue), chkTratamiento.Checked, Convert.ToInt32(cmb_tipoatencion.SelectedValue), chkHC.Checked, Convert.ToInt32(txt_historiaclinica.Text), chkFormulario.Checked, cmbFormulario.Text.Trim(), mushugñan, areaAsignada());

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
        public bool ImprimirAlta()
        {
            if (ultraGridPacientes.ActiveRow.Cells["TIPO"].Value.ToString() == "HOJA DE ALTA")
            {
                ATENCIONES ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ultraGridPacientes.ActiveRow.Cells["ATE_CODIGO"].Value.ToString()));
                if (ultimaAtencion.ESC_CODIGO != 1)
                {
                    //DataTable Medicos = NegDietetica.getDataTable("GetEgreso_MedicosEvolucion", ultimaAtencion.ATE_CODIGO.ToString());
                    DataTable Medicos = NegHabitaciones.CargarMedicos(ultimaAtencion.ATE_CODIGO);
                    DateTime fechaNacimiento = NegHabitaciones.RecuperaFechaNacimiento(ultraGridPacientes.ActiveRow.Cells["HC"].Value.ToString());
                    DataTable Garantias = NegDietetica.getDataTable("GetEgreso_Garantias", ultimaAtencion.ATE_CODIGO.ToString());
                    DataTable HistorialHabitacion = NegDietetica.getDataTable("GetEgreso_HistorialHabitacion", ultimaAtencion.ATE_CODIGO.ToString());
                    DataTable ConvenioSeguro = NegDietetica.getDataTable("GetEgreso_ConvenioSeguro", ultimaAtencion.ATE_CODIGO.ToString());
                    try
                    {
                        //limpiar tablas
                        DataTable DatosPcte = NegDietetica.getDataTable("GetEgreso_DatosPaciente", ultimaAtencion.ATE_CODIGO.ToString());
                        ReportesHistoriaClinica r2 = new ReportesHistoriaClinica(); r2.DeleteTable("rptEgreso_Medicos");
                        ReportesHistoriaClinica r3 = new ReportesHistoriaClinica();
                        r3.DeleteTable("rptEgreso_DatosPcte");
                        ReportesHistoriaClinica r4 = new ReportesHistoriaClinica(); r4.DeleteTable("rptEgreso_HabitacionHistorial");
                        ReportesHistoriaClinica r5 = new ReportesHistoriaClinica(); r5.DeleteTable("rptEgreso_ConvenioSeguro");

                        foreach (DataRow row in DatosPcte.Rows)
                        {
                            ///edad
                            var now = DateTime.Now;
                            var birthday = fechaNacimiento;
                            var yearsOld = now - birthday;

                            int years = (int)(yearsOld.TotalDays / 365.25);
                            string[] xy = new string[] {
                            row["PAC_HISTORIA_CLINICA"].ToString(),
                            row["PACIENTE"].ToString(),
                            row["PAC_IDENTIFICACION"].ToString(),
                            row["PAC_FECHA_NACIMIENTO"].ToString(),
                            row["hab_Numero"].ToString(),
                            row["ATE_FECHA_INGRESO"].ToString(),
                            row["ATE_CODIGO"].ToString(),
                            row["ATE_NUMERO_ATENCION"].ToString(),
                            row["MEDICO"].ToString(),
                            row["TIP_DESCRIPCION"].ToString(),
                            row["TIA_DESCRIPCION"].ToString(),
                            row["ATE_DIAGNOSTICO_INICIAL"].ToString(),
                            row["ATE_DIAGNOSTICO_FINAL"].ToString(),
                            row["USUARIO"].ToString(),
                            row["REFERIDO"].ToString(),
                            row["FECHA_ALTA"].ToString(),
                            years.ToString()
                    };
                            ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                            AUXma.InsertTable("rptEgreso_DatosPcte", xy);
                        }
                        USUARIOS USUA = new USUARIOS();
                        if (Medicos.Rows[0][3].ToString() != "")
                            USUA = NegUsuarios.RecuperarUsuarioID(Convert.ToInt32(Medicos.Rows[0][3].ToString()));

                        foreach (DataRow row in HistorialHabitacion.Rows)
                        {
                            string[] xy = new string[] {
                            row["OBSERVACION"].ToString(),
                            row["FECHA_MOVIMIENTO"].ToString(),
                            row["HORA"].ToString(),
                            row["HABITACION"].ToString(),
                            row["ANTERIOR"].ToString(),
                            row["ESTADO"].ToString(),
                            row["USUARIO"].ToString()
                    };
                            ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                            AUXma.InsertTable("rptEgreso_HabitacionHistorial", xy);
                        }

                        foreach (DataRow row in Garantias.Rows)
                        {
                            string[] xy = new string[] {
                            row["ADG_FECHA"].ToString(),
                            row["TG_NOMBRE"].ToString(),
                            row["ADG_VALOR"].ToString(),
                            row["TITULAR"].ToString(),
                            row["ADG_BANCO"].ToString(),
                            row["ADG_DOCUMENTO"].ToString(),
                            row["ADG_TIPOTARJETA"].ToString(),
                            row["ADG_ESTATUS"].ToString(),
                            row["ADG_OBSERVACION"].ToString()
                    };
                            ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                            AUXma.InsertTable("rptEgreso_Garantias", xy);
                        }
                        foreach (DataRow row in ConvenioSeguro.Rows)
                        {
                            string[] xy = new string[] {
                            row["CAT_NOMBRE"].ToString(),
                            row["ADA_FECHA_INICIO"].ToString(),
                            row["ADA_FECHA_FIN"].ToString(),
                            row["ADA_MONTO_COBERTURA"].ToString()
                    };
                            ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                            AUXma.InsertTable("rptEgreso_ConvenioSeguro", xy);
                        }
                        foreach (DataRow row in Medicos.Rows)
                        {

                            if (row[0].ToString() != "")
                            {
                                string[] xy = new string[] {
                                row[0].ToString(), "Generado desde Admsion"
                        };
                                ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                                AUXma.InsertTable("rptEgreso_Medicos", xy);
                            }
                        }
                        if (Medicos.Rows[0][3].ToString() == "")
                        {
                            string[] x2 = new string[] { Medicos.Rows[0][1].ToString(), Sesion.nomUsuario };
                            ReportesHistoriaClinica AUXmha = new ReportesHistoriaClinica();

                            AUXmha.InsertTable("rptupdate_dtos", x2);
                            System.Threading.Thread.Sleep(3000);
                        }

                        else
                        {
                            string[] x2 = new string[] { Medicos.Rows[0][1].ToString(), USUA.APELLIDOS + ' ' + USUA.NOMBRES };
                            ReportesHistoriaClinica AUXmha = new ReportesHistoriaClinica();

                            AUXmha.InsertTable("rptupdate_dtos", x2);
                            System.Threading.Thread.Sleep(3000);
                        }




                        //llamo al reporte
                        frmReportes form = new frmReportes();
                        form.reporte = "EGRESO_LX";
                        form.ShowDialog();
                        return true;

                    }
                    catch
                    {
                        MessageBox.Show("No se genero hoja de alta para esta atención!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Paciente en Hospitalización, no se puede generar Hoja de Alta.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
                return false;
        }
        private void ultraGridPacientes_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            if (ImprimirAlta())
                return;

            if (_ayudaSubsecuentes == true)
            {
                try
                {
                    string ateCodigo = ultraGridPacientes.ActiveRow.Cells["ATE_CODIGO"].Value.ToString();
                    int codigoAtencion = Convert.ToInt32(ateCodigo);


                    string c = ultraGridPacientes.ActiveRow.Cells["TIPO"].Value.ToString();
                    if (c.Equals("ANAMNESIS"))
                    {
                        His.Formulario.frm_Anemnesis evolucion = new His.Formulario.frm_Anemnesis(codigoAtencion, false);

                        evolucion.Show();
                    }
                    else if (c.Equals("EMERGENCIA"))
                    {
                        His.Emergencia.frm_EmergenciaNew evolucion = new His.Emergencia.frm_EmergenciaNew(codigoAtencion);

                        evolucion.Show();
                    }
                    else if (c.Equals("EPICRISIS"))
                    {
                        His.Formulario.frm_Epicrisis evolucion = new His.Formulario.frm_Epicrisis(codigoAtencion);

                        evolucion.Show();
                    }
                    else if (c.Equals("EVOLUCION"))
                    {
                        His.Formulario.frm_Evolucion evolucion = new His.Formulario.frm_Evolucion(codigoAtencion, false);
                        evolucion.subsecuente = true;
                        evolucion.Show();
                    }
                    else if (c.Equals("IMAGENOLOGIA"))
                    {
                        His.Formulario.frm_Imagen evolucion = new His.Formulario.frm_Imagen(codigoAtencion);

                        evolucion.Show();
                    }
                    else if (c.Equals("INTERCONSULTA"))
                    {
                        His.Formulario.frm_Interconsulta evolucion = new His.Formulario.frm_Interconsulta(codigoAtencion);

                        evolucion.Show();
                    }
                    else if (c.Equals("LABORATORIO"))
                    {
                        His.Formulario.frm_LaboratorioClinico evolucion = new His.Formulario.frm_LaboratorioClinico(codigoAtencion, false);

                        evolucion.Show();
                    }
                    else if (c.Equals("PROTOCOLO"))
                    {
                        His.Formulario.frm_Protocolo evolucion = new His.Formulario.frm_Protocolo(codigoAtencion, Convert.ToInt32(ultraGridPacientes.ActiveRow.Cells["ID_Formulario"].Value.ToString()));

                        evolucion.Show();
                    }
                    else if (c.Equals("CONSULTA EXTERNA"))
                    {
                        His.ConsultaExterna.Consulta consulta = new ConsultaExterna.Consulta(Convert.ToString(codigoAtencion));
                        //consulta.subsecuentes = true;
                        consulta.Show();
                        consulta.Close();
                    }
                    else if (c.Equals("NOTA EVOLUCION"))
                    {
                        Formulario.frmEvolucionEnfermeria enfermeria = new Formulario.frmEvolucionEnfermeria(codigoAtencion);

                        enfermeria.Show();
                    }
                    else if (c.Equals("KARDEX INSUMOS"))
                    {
                        Formulario.frm_admisnitracionInsumos insumos = new Formulario.frm_admisnitracionInsumos(codigoAtencion.ToString());

                        insumos.Show();
                    }
                    else if (c.Equals("KARDEX MEDICAMENTOS"))
                    {
                        Formulario.frmLogin frm = new Formulario.frmLogin(true);
                        frm.ShowDialog();
                        USUARIOS user = frm.user;
                        if (user.USR != "")
                        {
                            Formulario.frm_admisnitracionMedicamentos medicamentos = new Formulario.frm_admisnitracionMedicamentos(codigoAtencion.ToString(), user);

                            medicamentos.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                            medicamentos.Show();
                        }
                        else
                        {
                            MessageBox.Show("Debe Ingresar Login para abrir este formulario", "HIS3000");
                        }
                        
                    }
                    else if (c.Equals("EVOLUCION ENFERMERIA"))
                    {
                        His.Formulario.frmEvolucionEnfermeria consulta = new Formulario.frmEvolucionEnfermeria(codigoAtencion);
                        ////consulta.explorador = true;
                        //consulta.MdiParent = exploradorHC;
                        ////consulta.explorador = true;
                        consulta.Show();

                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
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
                        else if (c.Equals("EMERGENCIA"))
                        {
                            His.Formulario.frm_Emergencia evolucion = new His.Formulario.frm_Emergencia(codigoAtencion);
                            evolucion.MdiParent = exploradorHC;
                            evolucion.Show();
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
                            His.Formulario.frm_LaboratorioClinico evolucion = new His.Formulario.frm_LaboratorioClinico(codigoAtencion, false);
                            evolucion.MdiParent = exploradorHC;
                            evolucion.Show();
                        }
                        else if (c.Equals("PROTOCOLO"))
                        {
                            His.Formulario.frm_Protocolo evolucion = new His.Formulario.frm_Protocolo(codigoAtencion, Convert.ToInt32(ultraGridPacientes.ActiveRow.Cells["ID_Formulario"].Value.ToString()));
                            evolucion.MdiParent = exploradorHC;
                            evolucion.Show();
                        }
                        else if (c.Equals("CONSULTA EXTERNA"))
                        {
                            His.ConsultaExterna.Consulta consulta = new ConsultaExterna.Consulta(Convert.ToString(codigoAtencion));
                            //consulta.explorador = true;
                            consulta.MdiParent = exploradorHC;
                            //consulta.explorador = true;
                            consulta.Show();
                        }
                        else if (c.Equals("NOTA EVOLUCION"))
                        {
                            Formulario.frmEvolucionEnfermeria enfermeria = new Formulario.frmEvolucionEnfermeria(codigoAtencion);
                            enfermeria.MdiParent = exploradorHC;
                            enfermeria.Show();
                        }
                        else if (c.Equals("KARDEX INSUMOS"))
                        {
                            Formulario.frm_admisnitracionInsumos insumos = new Formulario.frm_admisnitracionInsumos(codigoAtencion.ToString());
                            insumos.MdiParent = exploradorHC;
                            insumos.Show();
                        }
                        else if (c.Equals("KARDEX MEDICAMENTOS"))
                        {
                            Formulario.frmLogin frm = new Formulario.frmLogin(true);
                            frm.ShowDialog();
                            USUARIOS user = frm.user;
                            if (user.USR != "")
                            {
                                Formulario.frm_admisnitracionMedicamentos medicamentos = new Formulario.frm_admisnitracionMedicamentos(codigoAtencion.ToString(), user);

                                medicamentos.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                                medicamentos.Show();
                            }
                            else
                            {
                                MessageBox.Show("Debe Ingresar Login para abrir este formulario", "HIS3000");
                            }
                            
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
                            MessageBox.Show("Imprima la hoja de alta desde el modulo de admision", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
}

