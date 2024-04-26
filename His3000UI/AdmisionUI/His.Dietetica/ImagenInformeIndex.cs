using His.DatosReportes;
using His.Entidades;
using His.Entidades.Clases;
using His.Formulario;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class ImagenInformeIndex : Form
    {
        public bool Editar = false;
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        public ImagenInformeIndex()
        {
            InitializeComponent();
            //dtpDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            ///dtpDesde.Value = DateTime.Now.AddDays(-1);
            dtpDesde.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"));
            dtpHasta.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"));
            //dtpHasta.Value = (DateTime.Now).AddDays(1);
            actualizarGrid();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbCedula_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbCedula.Checked)
                btnEmail.Visible = true;
            else
            {
                btnEmail.Visible = false;
            }
            btnNuevo.Enabled = rbCedula.Checked;
            btnModificar.Enabled = !rbCedula.Checked;
            btnImprimir.Enabled = !rbCedula.Checked;
            btnModificar.Visible = false;
            actualizarGrid();
        }

        private void rbGenerados_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGenerados.Checked)
                btnEmail.Visible = true;
            else
            {
                btnEmail.Visible = false;
            }
            btnNuevo.Enabled = !rbGenerados.Checked;
            btnModificar.Enabled = rbGenerados.Checked;
            btnImprimir.Enabled = rbGenerados.Checked;
            btnModificar.Visible = true;
            actualizarGrid();
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            actualizarGrid();
        }

        private void actualizarGrid()
        {
            DateTime fi = dtpDesde.Value;
            DateTime ff = dtpHasta.Value;
            ff = ff.AddDays(1);
            string frango = "'" + fi.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' and '" + ff.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "'";

            if (rbCedula.Checked)
            {
                grid.DataSource = NegImagen.getAgendamientos(frango, 2);
            }
            else if (rbGenerados.Checked)
            {
                grid.DataSource = NegImagen.getAgendamientosInformes(frango);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grid.Rows.Count > 0)
            {
                //ImprimirReportex(Convert.ToInt32(grid.ActiveRow.Cells["id"].Value));//Mario se cambia para que funcione con base SQL y como reporte por dentro
                ImprimirReporteNew(Convert.ToInt32(grid.ActiveRow.Cells["id"].Value));
            }
        }
        private void ImprimirReporteNew(Int64 imagen)
        {
            try
            {
                DsFromImagen ds = new DsFromImagen();

                DataTable est = NegImagen.getForm012Estudios(imagen);
                string estudios = "";
                int aux = 0;
                foreach (DataRow row in est.Rows)
                {
                    if (aux > 0)
                        estudios += "   ,    ";
                    estudios += row["PRO_DESCRIPCION"].ToString();
                    aux++;
                }
                DataTable e= NegImagen.getForm012(imagen);
                DataRow ima;

                ima = ds.Tables["form012"].NewRow();
                ima["path"] = Certificado.path();
                ima["clinica"] = Convert.ToString(Sesion.nomEmpresa);
                ima["parroquia"] = Convert.ToString(e.Rows[0]["PARROQUIA"]);
                ima["canton"] = Convert.ToString(e.Rows[0]["CANTON"]);
                ima["provincia"] = Convert.ToString(e.Rows[0]["PROVINCIA"]);
                ima["HC"] = Convert.ToString(e.Rows[0]["PAC_HISTORIA_CLINICA"]);
                ima["apellidoPaterno"] = Convert.ToString(e.Rows[0]["PAC_APELLIDO_PATERNO"]);
                ima["apellidoMaterno"] = Convert.ToString(e.Rows[0]["PAC_APELLIDO_MATERNO"]);
                ima["primerNombre"] = Convert.ToString(e.Rows[0]["PAC_NOMBRE1"]);
                ima["segundoNombre"] = Convert.ToString(e.Rows[0]["PAC_NOMBRE2"]);
                ima["identificacion"] = Convert.ToString(e.Rows[0]["PAC_IDENTIFICACION"]);
                ima["fechaInforme"] = (Convert.ToDateTime(e.Rows[0]["fecha_informe"])).ToString("yyyy-MM-dd");
                ima["hora"] = (Convert.ToDateTime(e.Rows[0]["fecha_informe"])).ToString("HH:mm:ss", CultureInfo.InvariantCulture);//hora
                ima["servicio"] = Convert.ToString(e.Rows[0]["TIP_DESCRIPCION"]);
                ima["sala"] = " 1 "; //sala
                ima["cama"] = Convert.ToString(e.Rows[0]["hab_Numero"]);
                ima["medSolicitante"] = Convert.ToString(e.Rows[0]["Medico_solicitante"]);
                ima["control"] = Convert.ToString(e.Rows[0]["P_Control"]);
                ima["normal"] = Convert.ToString(e.Rows[0]["P_Normal"]);
                ima["urgente"] = Convert.ToString(e.Rows[0]["P_Urgente"]);
                ima["fechaEntrega"] = (Convert.ToDateTime(e.Rows[0]["fecha_entrega"])).ToString("yyyy-MM-dd");
                ima["estudios"] = estudios;
                ima["informe"] = Convert.ToString(e.Rows[0]["informe"]);
                ima["dbV"] = ima["cedula"] = Convert.ToString(e.Rows[0]["DB_V"]);
                ima["lfV"] = Convert.ToString(e.Rows[0]["LF_V"]);
                ima["paV"] = Convert.ToString(e.Rows[0]["PA_V"]);
                ima["dbEG"] = Convert.ToString(e.Rows[0]["DB_EG"]);
                ima["lfEG"] = Convert.ToString(e.Rows[0]["LF_EG"]);
                ima["paEG"] = Convert.ToString(e.Rows[0]["PA_EG"]);
                ima["dbP"] = Convert.ToString(e.Rows[0]["DB_P"]);
                ima["lfP"] = Convert.ToString(e.Rows[0]["LF_P"]);
                ima["paP"] = Convert.ToString(e.Rows[0]["PA_P"]);
                ima["placentaF"] = (Convert.ToString(e.Rows[0]["PLACENTA_F"]) == "0" ? " " : "X");
                ima["placentaM"] = (Convert.ToString(e.Rows[0]["PLACENTA_M"]) == "0" ? " " : "X");
                ima["placentaP"] = (Convert.ToString(e.Rows[0]["PLACENTA_P"]) == "0" ? " " : "X");
                ima["masc"] = (Convert.ToString(e.Rows[0]["MASCULINO"]) == "0" ? " " : "X");
                ima["femen"] = (Convert.ToString(e.Rows[0]["FEMENINO"]) == "0" ? " " : "X");
                ima["multiple"] = (Convert.ToString(e.Rows[0]["MULTIPLE"]) == "0" ? " " : "X");
                ima["gradoMadurez"] = Convert.ToString(e.Rows[0]["GRADO_MADUREZ"]);
                ima["anteVersion"] = (Convert.ToString(e.Rows[0]["ANTEVERSION"]) == "0" ? " " : "X");
                ima["registroVersion"] = (Convert.ToString(e.Rows[0]["RETROVERSION"]) == "0" ? " " : "X");
                ima["diu"] = (Convert.ToString(e.Rows[0]["DIU"]) == "0" ? " " : "X");
                ima["fibroma"] = (Convert.ToString(e.Rows[0]["FIBROMA"]) == "0" ? " " : "X");
                ima["mioma"] = (Convert.ToString(e.Rows[0]["MIOMA"]) == "0" ? " " : "X");
                ima["ausente"] = (Convert.ToString(e.Rows[0]["AUSENTE"]) == "0" ? " " : "X");
                ima["hidrosalpix"] = (Convert.ToString(e.Rows[0]["HIDROSALPIX"]) == "0" ? " " : "X");
                ima["quiste"] = (Convert.ToString(e.Rows[0]["QUISTE"]) == "0" ? " " : "X");
                ima["vacia"] = (Convert.ToString(e.Rows[0]["VACIA"]) == "0" ? " " : "X");
                ima["ocupada"] = (Convert.ToString(e.Rows[0]["OCUPADA"]) == "0" ? " " : "X");
                ima["sacoDouglas"] = Convert.ToString(e.Rows[0]["SACO_DOUGLAS"]);
                ima["recomendaciones"] = Convert.ToString(e.Rows[0]["recomendaciones"]);
                ima["placas"] = Convert.ToString(e.Rows[0]["PLACAS_ENVIADAS"]);
                ima["pl30x40"] = Convert.ToString(e.Rows[0]["30X40"]);
                ima["pl8x10"] = Convert.ToString(e.Rows[0]["8X10"]);
                ima["pl14x14"] = Convert.ToString(e.Rows[0]["14X14"]);
                ima["pl14x17"] = Convert.ToString(e.Rows[0]["14X17"]);
                ima["pl18x24"] = Convert.ToString(e.Rows[0]["18X24"]);
                ima["odont"] = Convert.ToString(e.Rows[0]["ODONT"]);
                ima["pldanadas"] = Convert.ToString(e.Rows[0]["DANADAS"]);
                ima["medContraste"] = (Convert.ToString(e.Rows[0]["MEDIO_CONTRASTE"]) == "0" ? " " : "X");
                ima["tecnico"] = Convert.ToString(e.Rows[0]["Tecnologo"]);
                ima["radiologo"] = Convert.ToString(e.Rows[0]["Radiologo"]);
                ima["conclusiones"] = Convert.ToString(e.Rows[0]["conclusiones"]);

                ds.Tables["form012"].Rows.Add(ima);


                DataTable diagnosticos = NegImagen.getForm012Dx(imagen);
                DataRow cie;
                foreach (DataRow row in diagnosticos.Rows)
                {
                    cie = ds.Tables["cie10"].NewRow();
                    cie["diagnostico"] = row["CIE_DESCRIPCION"].ToString();
                    cie["cie10"] = row["CIE_CODIGO"].ToString();
                    cie["presuntivo"] = "1";
                    ds.Tables["cie10"].Rows.Add(cie);
                }
                frmReportes myreport = new frmReportes(1, "Frm0012Imagen", ds);
                myreport.Show();
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        private void ImprimirReportex(int idImagenologia)
        {
            try
            {
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.limpiarForm012();

                ReportesHistoriaClinica imagenL1 = new ReportesHistoriaClinica();
                imagenL1.limpiarForm012();

                NegCertificadoMedico c = new NegCertificadoMedico();


                DataTable est = NegImagen.getForm012Estudios(idImagenologia);
                string estudios = "";
                int aux = 0;
                foreach (DataRow row in est.Rows)
                {
                    if (aux > 0)
                        estudios += "   ,    ";
                    estudios += row["PRO_DESCRIPCION"].ToString();
                    aux++;
                }

                DataTable e = NegImagen.getForm012(idImagenologia);

                string[] x = new string[] {
                    Convert.ToString(Sesion.nomEmpresa),
                    Convert.ToString(e.Rows[0]["PARROQUIA"]),
                    Convert.ToString(e.Rows[0]["CANTON"]),
                    Convert.ToString(e.Rows[0]["PROVINCIA"]),
                    Convert.ToString(e.Rows[0]["PAC_HISTORIA_CLINICA"]),
                    Convert.ToString(e.Rows[0]["PAC_APELLIDO_PATERNO"]),
                    Convert.ToString(e.Rows[0]["PAC_APELLIDO_MATERNO"]),
                    Convert.ToString(e.Rows[0]["PAC_NOMBRE1"]),
                    Convert.ToString(e.Rows[0]["PAC_NOMBRE2"]),
                    Convert.ToString(e.Rows[0]["PAC_IDENTIFICACION"]),
                    (Convert.ToDateTime(e.Rows[0]["fecha_informe"])).ToString("yyyy-MM-dd"),
                    (Convert.ToDateTime(e.Rows[0]["fecha_informe"])).ToString("HH:mm:ss",CultureInfo.InvariantCulture) , //hora
                    Convert.ToString(e.Rows[0]["TIP_DESCRIPCION"]),
                    " 1 ", //sala
                    Convert.ToString(e.Rows[0]["hab_Numero"]),
                    Convert.ToString(e.Rows[0]["Medico_solicitante"]),
                    Convert.ToString(e.Rows[0]["P_Control"]),
                    Convert.ToString(e.Rows[0]["P_Normal"]),
                    Convert.ToString(e.Rows[0]["P_Urgente"]),
                    (Convert.ToDateTime(e.Rows[0]["fecha_entrega"])).ToString("yyyy-MM-dd"),
                    " ", //rubros
                    estudios,
                    Convert.ToString(e.Rows[0]["informe"]),
                    Convert.ToString(e.Rows[0]["DB_V"]),
                    Convert.ToString(e.Rows[0]["LF_V"]),
                    Convert.ToString(e.Rows[0]["PA_V"]),
                    Convert.ToString(e.Rows[0]["DB_EG"]),
                    Convert.ToString(e.Rows[0]["LF_EG"]),
                    Convert.ToString(e.Rows[0]["PA_EG"]),
                    Convert.ToString(e.Rows[0]["DB_P"]),
                    Convert.ToString(e.Rows[0]["LF_P"]),
                    Convert.ToString(e.Rows[0]["PA_P"]),
                    (Convert.ToString(e.Rows[0]["PLACENTA_F"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["PLACENTA_M"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["PLACENTA_P"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["MASCULINO"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["FEMENINO"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["MULTIPLE"]) == "0" ? " " : "X"),
                    Convert.ToString(e.Rows[0]["GRADO_MADUREZ"]),
                    (Convert.ToString(e.Rows[0]["ANTEVERSION"])=="0"?" ":"X"),
                    (Convert.ToString(e.Rows[0]["RETROVERSION"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["DIU"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["FIBROMA"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["MIOMA"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["AUSENTE"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["HIDROSALPIX"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["QUISTE"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["VACIA"]) == "0" ? " " : "X"),
                    (Convert.ToString(e.Rows[0]["OCUPADA"]) == "0" ? " " : "X"),
                    Convert.ToString(e.Rows[0]["SACO_DOUGLAS"]),
                    Convert.ToString(e.Rows[0]["recomendaciones"]),
                    Convert.ToString(e.Rows[0]["PLACAS_ENVIADAS"]),
                    Convert.ToString(e.Rows[0]["30X40"]),
                    Convert.ToString(e.Rows[0]["8X10"]),
                    Convert.ToString(e.Rows[0]["14X14"]),
                    Convert.ToString(e.Rows[0]["14X17"]),
                    Convert.ToString(e.Rows[0]["18X24"]),
                    Convert.ToString(e.Rows[0]["ODONT"]),
                    Convert.ToString(e.Rows[0]["DANADAS"]),
                    (Convert.ToString(e.Rows[0]["MEDIO_CONTRASTE"])=="0"?" ":"X"),
                    Convert.ToString(e.Rows[0]["Tecnologo"]),
                    Convert.ToString(e.Rows[0]["Radiologo"]),
                    Convert.ToString(c.path())
                };


                ReportesHistoriaClinica imagen = new ReportesHistoriaClinica();
                imagen.addImagenologiaInforme(x);

                //List<PedidoImagen_reporteDiagnosticos> ListDx = empaquetarReporteDx(idImagenologia);

                ReportesHistoriaClinica imagend = new ReportesHistoriaClinica();
                imagend.limpiarImagenologiaDiagnostico();

                DataTable diagnosticos = NegImagen.getForm012Dx(idImagenologia);
                foreach (DataRow row in diagnosticos.Rows)
                {
                    PedidoImagen_reporteDiagnosticos dx = new PedidoImagen_reporteDiagnosticos();
                    dx.diagnostico = row["CIE_DESCRIPCION"].ToString();
                    dx.CIE = row["CIE_CODIGO"].ToString();
                    dx.presuntivo = "1";
                    ReportesHistoriaClinica AUXX = new ReportesHistoriaClinica();
                    AUXX.ingresarImagenologiaDiagnostico(dx);
                }

                frmReportes ventana = new frmReportes(1, "form012");
                ventana.ShowDialog();
                ventana.Close();
                ventana.Dispose();
                NegValidaciones.alzheimer();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            dtpHasta.MinDate = dtpDesde.Value;

        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            dtpDesde.MaxDate = dtpHasta.Value;
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];
            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;
            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            grid.DisplayLayout.UseFixedHeaders = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (grid.Rows.Count > 0)
            {
                ImagenInforme x = new ImagenInforme(Convert.ToInt32(grid.ActiveRow.Cells["id"].Value), 0);
                if (x.estado)
                    x.ShowDialog();
                //x.Show();
                actualizarGrid();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (grid.Rows.Count > 0)
            {
                #region Validacion anterior con respecto a la cuenta Cambio 20220428_0813
                //DataTable ag = NegImagen.getAgendamiento(grid.ActiveRow.Cells["id"].Value.ToString());

                //if (ag.Rows.Count == 0)
                //{
                //    MessageBox.Show("El informe ya no es posible editarlo cuenta facturada hace mas de 48 horas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                #endregion

                //nueva validacion contra la fecha de informe
                HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME informe = new HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME();
                informe = NegImagen.validarInformeHoras(Convert.ToInt32(grid.ActiveRow.Cells["id"].Value.ToString()));
                DateTime Hoy = DateTime.Now;

                if (informe.fecha_informe != null)
                {
                    DateTime fInforme = informe.fecha_informe;
                    DateTime fecha_actual = new DateTime(day: Hoy.Day, month: Hoy.Month, year: Hoy.Year, hour: Hoy.Hour, minute: Hoy.Minute, second: Hoy.Second);
                    DateTime fecha_informe = new DateTime(day: fInforme.Day, month: fInforme.Month, year: fInforme.Year, hour: fInforme.Hour, minute: fInforme.Minute, second: fInforme.Second);
                    TimeSpan ts = fecha_actual - fecha_informe;

                    var differenceInDias = ts.TotalDays;
                    var differenceInHours = ts.TotalHours;
                    var differenceInMinuntos = ts.TotalMinutes;
                    if (differenceInHours > 72)
                    {
                        MessageBox.Show("No es posible editar informe ya que supera las 72 horas.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                ImagenInforme x = new ImagenInforme(Convert.ToInt32(grid.ActiveRow.Cells["id"].Value), 1);
                x.ShowDialog();
                //x.Show();
                actualizarGrid();
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.ActiveRow.Cells["id"].Value.ToString() != "")
                    if (MessageBox.Show("Usted, va enviar los resultados por correo electronico", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        EnviaEmail frm = new EnviaEmail(Convert.ToInt32(grid.ActiveRow.Cells["id"].Value), Convert.ToInt32(grid.ActiveRow.Cells["Atencion"].Value));
                        frm.ShowDialog();
                        DateTime fi = dtpDesde.Value;
                        DateTime ff = dtpHasta.Value;
                        ff = ff.AddDays(1);
                        string frango = "'" + fi.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "' and '" + ff.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") + "'";

                        grid.DataSource = NegImagen.getAgendamientosInformes(frango);
                    }
            }
            catch
            {
                MessageBox.Show("Debe elegir un paciente para hacer el envío del examen", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
