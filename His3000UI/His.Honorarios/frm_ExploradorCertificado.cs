using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using His.Entidades;
using His.Formulario;
using His.Entidades.Clases;
using System.Globalization;

namespace His.Honorarios
{
    public partial class frm_ExploradorCertificado : Form
    {
        public frm_ExploradorCertificado()
        {
            InitializeComponent();
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            dtpFiltroDesde.Value = oPrimerDiaDelMes;
            dtpFiltroHasta.Value = DateTime.Now;
        }

        public void CargarCertificados()
        {
            try
            {
                if (dtpFiltroDesde.Value > dtpFiltroHasta.Value)
                {
                    errorProvider1.SetError(grpFechas, "La fecha desde no puede ser mayor a fecha hasta.");
                }
                else
                {
                    UltraGridCertificados.DataSource = null;
                    if (chkAnulados.Checked)
                    {
                        Reportes(false);
                        btnimprimir.Enabled = false;
                        btnanular.Enabled = false;
                    }
                    else
                    {
                        Reportes(true);
                        btnimprimir.Enabled = true;
                        btnanular.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar certificados del mes actual.\r\nMas detalles: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reportes(bool estado)
        {
            if (dtpFiltroDesde.Value.Date < DateTime.Now.Date)
            {
                dtpFiltroHasta.Value = dtpFiltroHasta.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                //UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadosMedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value);
                if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 5 || Sesion.codDepartamento == 7 || Sesion.codDepartamento == 10)
                    UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadosMedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, estado);
                else
                {
                    USUARIOS datosUsuario = new USUARIOS();
                    datosUsuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
                    if (datosUsuario != null)
                    {
                        Int64 codigoMedico = NegCertificadoMedico.MedicoCodigo(datosUsuario.IDENTIFICACION);
                        UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadoXmedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, Convert.ToInt32(codigoMedico), estado);

                    }
                    else
                        UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadoXmedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, Sesion.codUsuario, estado);

                }
            }
            else
            {
                //DataTable usu = new DataTable();
                //usu = NegUsuarios.ConsultaUsuarioDep(Sesion.codDepartamento);
                if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 5 || Sesion.codDepartamento == 7 || Sesion.codDepartamento == 10)
                    UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadosMedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, estado);
                else
                    UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadoXmedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, Sesion.codMedico, estado);
            }
        }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (UltraGridCertificados.Rows.Count > 0)
                {
                    string PathExcel = FindSavePath();
                    if (PathExcel != null)
                    {
                        if (UltraGridCertificados.CanFocus == true)
                            this.ultraGridExcelExporter1.Export(UltraGridCertificados, PathExcel);
                        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                    }
                }
                else
                    MessageBox.Show("No hay registros para exportar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCertificados();
        }

        private void frm_ExploradorCertificado_Load(object sender, EventArgs e)
        {
            CargarCertificados();
        }

        private void UltraGridCertificados_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = UltraGridCertificados.DisplayLayout.Bands[0];

                UltraGridCertificados.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                UltraGridCertificados.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                UltraGridCertificados.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                UltraGridCertificados.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                UltraGridCertificados.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                UltraGridCertificados.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                UltraGridCertificados.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;


                //Caracteristicas de Filtro en la grilla
                UltraGridCertificados.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                UltraGridCertificados.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                UltraGridCertificados.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                UltraGridCertificados.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                UltraGridCertificados.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                UltraGridCertificados.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                UltraGridCertificados.DisplayLayout.Bands[0].Columns[3].Width = 300;
                UltraGridCertificados.DisplayLayout.Bands[0].Columns[4].Width = 300;
                UltraGridCertificados.DisplayLayout.Bands[0].Columns[6].Width = 500;

                //agrandamiento de filas 

                ////Ocultar columnas, que son fundamentales al levantar informacion
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[3].Hidden = false;
                //UltraGridPacientes.DisplayLayout.Bands[0].Columns[5].Hidden = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        private void btnimprimir_Click(object sender, EventArgs e)
        {

            if (UltraGridCertificados.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = this.UltraGridCertificados.ActiveRow;
                DataTable Tabla = new DataTable();
                DataTable TablaIESS = new DataTable();
                ATENCIONES atencion = new ATENCIONES();
                if (fila.Cells["TIPO CERTIFICADO"].Value.ToString() == "CM")
                {
                    Tabla = NegCertificadoMedico.ReimpresionCertificado(Convert.ToInt32(fila.Cells["NRO CERTIFICADO"].Value.ToString()));


                }
                if (fila.Cells["TIPO CERTIFICADO"].Value.ToString() == "CME")
                {
                    TablaIESS = NegCertificadoMedico.ReimpresionCertificadoIESS(Convert.ToInt32(fila.Cells["NRO CERTIFICADO"].Value.ToString()));
                }
                if (Tabla.Rows.Count != 0)
                {
                    if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "8")
                    {
                        MessageBox.Show("No se puede reimprimir un certificado de una atencion de Servicios Externos \r\n consulte con el administrador", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                }
                else
                {
                    if (TablaIESS.Rows[0]["CMI_TIPO_INGRESO"].ToString() == "8")
                    {
                        MessageBox.Show("No se puede reimprimir un certificado de una atencion de Servicios Externos \r\n consulte con el administrador", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                //atencion = NegAtenciones.RecuperarAtencionPorNumero(fila.Cells["NRO ATENCION"].Value.ToString().Trim());
                if (Tabla.Rows.Count != 0)
                    atencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(Tabla.Rows[0][1].ToString()));
                else
                    atencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(TablaIESS.Rows[0][1].ToString()));
                DataTable reporteDatos = new DataTable();
                DataTable detalleReporteDatos = new DataTable();
                if (Tabla.Rows.Count != 0)
                    //reporteDatos = Certificado.CargarDatosCertificado(atencion.ATE_CODIGO.ToString());
                    reporteDatos = Certificado.CargarDatosCertificadoN(atencion.ATE_CODIGO.ToString(), Tabla.Rows[0][0].ToString());

                else
                    reporteDatos = Certificado.CargarDatosCertificadoIESS(atencion.ATE_CODIGO.ToString(), TablaIESS.Rows[0][0].ToString());
                if (Tabla.Rows.Count != 0)
                    detalleReporteDatos = Certificado.CargarDatosCertificado_Detalle(Convert.ToInt64(reporteDatos.Rows[0][9].ToString()));
                else
                    detalleReporteDatos = Certificado.CargarDatosCertificadoIESS_Detalle(Convert.ToInt64(reporteDatos.Rows[0][5].ToString()));


                Certificado_Medico CM = new Certificado_Medico();

                PACIENTES_DATOS_ADICIONALES pacien = new PACIENTES_DATOS_ADICIONALES();
                PACIENTES pacienteActual = new PACIENTES();
                pacienteActual = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(atencion.ATE_CODIGO));

                pacien = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(pacienteActual.PAC_CODIGO);

                EMPRESA empresa = new EMPRESA();
                empresa = NegEmpresa.RecuperaEmpresa();

                string FechadeIngreso, FechadeAlta, dias_reposo;

                MEDICOS medico = new MEDICOS();
                if (Tabla.Rows.Count != 0)
                    medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(Tabla.Rows[0]["MED_CODIGO"].ToString()));
                else
                    medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(TablaIESS.Rows[0]["MED_CODIGO"].ToString()));

                if (Tabla.Rows.Count != 0)
                {
                    if (Convert.ToBoolean(Tabla.Rows[0]["CER_ESTADO"].ToString()) == true)
                    {
                        if (atencion.ATE_FECHA_ALTA != null)
                        {
                            if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "1") //ES EMERGENCIA SIN FECHA DE ALTA
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {
                                    drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    //FechadeIngreso = Fecha_En_Palabra(atencion.ATE_FECHA_INGRESO.ToString());
                                    FechadeIngreso = Fecha_Larga_En_Palabra(atencion.ATE_FECHA_INGRESO.ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];

                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                    drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                    drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();

                                    dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                    drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                    DateTime FechaHoy = atencion.ATE_FECHA_ALTA.Value;
                                    DateTime FechaAlta2 = atencion.ATE_FECHA_ALTA.Value;
                                    //FechadeAlta = Fecha_En_Palabra(FechaHoy.ToString());
                                    FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                    FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                    drCertificado["FechaAlta"] = FechadeAlta;
                                    FechaAlta2 = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) + 1);
                                    if (dias_reposo == "CERO")
                                    {
                                        drCertificado["Dias_FinReposo"] = FechaAlta2.ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(FechaAlta2.ToShortDateString());//FechaAlta2.ToString("dd") + " (" + Dia_En_Palabras(FechaAlta2.ToString("dd")) + ")" + " de " + FechaAlta2.ToString("MMMM") + " del " + FechaAlta2.ToString("yyyy");
                                    }
                                    else
                                    {
                                        drCertificado["Dias_FinReposo"] = FechaHoy.ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(FechaHoy.ToShortDateString());//FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    }
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                    // else
                                    // {
                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    if (medico.MED_RUC.Length > 10)
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                    }
                                    else
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    }

                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                    drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                    //}
                                    string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                    if (fechaAyuda.IndexOf(":") > 0)
                                        fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                    drCertificado["FechaAyuda"] = fechaAyuda;
                                    //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                    ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                    //drCertificado["FechaAyuda"] = fechaAyuda;
                                    drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                    drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                    drCertificado["Tipo"] = "emergencia";
                                    drCertificado["PathImagen"] = Certificado.path();

                                    CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                }
                                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                                reporte.Show();
                            }
                            else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "2")//ES HOSPITALIZACION SIN FECHA DE ALTA
                            {
                                if (Tabla.Rows[0]["CER_TRATAMIENTO"].ToString() == "CLINICO")
                                {
                                    DataRow drCertificado;
                                    foreach (DataRow item in detalleReporteDatos.Rows)
                                    {

                                        drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                        //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        //if (FechadeIngreso.IndexOf(":") > 0)
                                        //    FechadeIngreso = FechadeIngreso.Substring(0, FechadeIngreso.Length - 5);
                                        drCertificado["FechaIngreso"] = FechadeIngreso.Substring(0, FechadeIngreso.Length - 11);

                                        dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                        if (dias_reposo.IndexOf(":") > 0)
                                            dias_reposo = dias_reposo.Substring(0, dias_reposo.Length - 5);
                                        drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                        if (reporteDatos.Rows[0][4].ToString() == "")
                                        {
                                            //FechadeAlta = Fecha_En_Palabra(atencion.ATE_FECHA_ALTA.ToString());
                                            FechadeAlta = Fecha_Larga_En_Palabra(atencion.ATE_FECHA_ALTA.ToString());
                                            //if (FechadeAlta.IndexOf(":") > 0)
                                            //    FechadeAlta = FechadeAlta.Substring(0, FechadeAlta.Length - 5);
                                            drCertificado["FechaAlta"] = FechadeAlta.Substring(0, FechadeAlta.Length - 11);
                                            DateTime Fecha = Convert.ToDateTime(atencion.ATE_FECHA_ALTA.ToString());
                                            string resultado = "";
                                            if (Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) == 0)
                                                resultado = Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString())).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));//Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));
                                            else
                                                resultado = Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));//Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                            if (resultado.IndexOf(":") > 0)
                                                resultado = resultado.Substring(0, resultado.Length - 5);
                                            drCertificado["Dias_FinReposo"] = resultado;
                                        }
                                        else
                                        {
                                            //FechadeAlta = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                                            FechadeAlta = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][4].ToString());
                                            //if (FechadeAlta.IndexOf(":") > 0)
                                            //    FechadeAlta = FechadeAlta.Substring(0, FechadeAlta.Length - 5);
                                            drCertificado["FechaAlta"] = FechadeAlta.Substring(0, FechadeAlta.Length - 11);
                                            DateTime Fecha = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                                            string resultado = "";
                                            if (Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) == 0)
                                                resultado = Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString())).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));
                                            else
                                                resultado = Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));

                                            if (resultado.IndexOf(":") > 0)
                                                resultado = resultado.Substring(0, resultado.Length - 5);
                                            drCertificado["Dias_FinReposo"] = resultado;
                                        }

                                        drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                        drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                        if (medico.MED_RUC.Length > 10)
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                        }
                                        else
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                        }

                                        drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                        drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);

                                        drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                        drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                        drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                        drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();

                                        drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                        drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                        drCertificado["tipoContongencia"] = Tabla.Rows[0][7].ToString();

                                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                        drCertificado["PathImagen"] = Certificado.path();
                                        drCertificado["Tratamiento"] = Tabla.Rows[0]["CER_TRATAMIENTO"].ToString();
                                        drCertificado["FechaTratamiento"] = "";
                                        drCertificado["Procedimiento"] = "NO APLICA";
                                        string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        if (fechaAyuda.IndexOf(":") > 0)
                                            fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        drCertificado["FechaAyuda"] = fechaAyuda;
                                        //string fechaAyuda = Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        //if (fechaAyuda.IndexOf(":") > 0)
                                        //    fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        //drCertificado["FechaAyuda"] = fechaAyuda;
                                        drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                        drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                        CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                                    }
                                    His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "CertificadoHA", CM);
                                    myreport.Show();
                                }
                                else
                                {
                                    DataRow drCertificado;
                                    foreach (DataRow item in detalleReporteDatos.Rows)
                                    {
                                        drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                        //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        //if (FechadeIngreso.IndexOf(":") > 0)
                                        //    FechadeIngreso = FechadeIngreso.Substring(0, FechadeIngreso.Length - 5);
                                        drCertificado["FechaIngreso"] = FechadeIngreso;
                                        dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                        drCertificado["Dias_Reposo"] = drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                        if (reporteDatos.Rows[0][4].ToString() == "")
                                        {
                                            //FechadeAlta = Fecha_En_Palabra(atencion.ATE_FECHA_ALTA.ToString());
                                            FechadeAlta = Fecha_Larga_En_Palabra(atencion.ATE_FECHA_ALTA.ToString());
                                            //if (FechadeAlta.IndexOf(":") > 0)
                                            //    FechadeAlta = FechadeAlta.Substring(0, FechadeAlta.Length - 5);
                                            drCertificado["FechaAlta"] = FechadeAlta;
                                            DateTime Fecha = Convert.ToDateTime(atencion.ATE_FECHA_ALTA.ToString());
                                            string resultado = "";
                                            if (Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) == 0)
                                                resultado = Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString())).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));
                                            else
                                                resultado = Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));
                                            drCertificado["Dias_FinReposo"] = resultado;
                                        }
                                        else
                                        {
                                            //FechadeAlta = Fecha_En_Palabra(reporteDatos.Rows[0][4].ToString());
                                            FechadeAlta = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][4].ToString());
                                            //if (FechadeAlta.IndexOf(":") > 0)
                                            //    FechadeAlta = FechadeAlta.Substring(0, FechadeAlta.Length - 5);
                                            drCertificado["FechaAlta"] = FechadeAlta;
                                            DateTime Fecha = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                                            string resultado = "";
                                            if (Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) == 0)
                                                resultado = Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString())).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));
                                            else
                                                resultado = Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(Fecha.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));
                                            if (resultado.IndexOf(":") > 0)
                                                resultado = resultado.Substring(0, resultado.Length - 5);
                                            drCertificado["Dias_FinReposo"] = resultado;
                                        }

                                        drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                        drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                        if (medico.MED_RUC.Length > 10)
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                        }
                                        else
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                        }

                                        drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                        drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);

                                        drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                        drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                        drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                        drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();

                                        drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                        drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                        drCertificado["tipoContongencia"] = Tabla.Rows[0][7].ToString();
                                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                        drCertificado["PathImagen"] = Certificado.path();
                                        drCertificado["Tratamiento"] = Tabla.Rows[0]["CER_TRATAMIENTO"].ToString();
                                        drCertificado["FechaTratamiento"] = "FECHA: " + Convert.ToDateTime(Tabla.Rows[0]["CER_FECHA_CIRUGIA"].ToString()).ToShortDateString();
                                        drCertificado["Procedimiento"] = Tabla.Rows[0]["CER_PROCEDIMIENTO"].ToString();
                                        //string fechaAyuda = Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        if (fechaAyuda.IndexOf(":") > 0)
                                            fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        drCertificado["FechaAyuda"] = fechaAyuda;
                                        //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        //if (fechaAyuda.IndexOf(":") > 0)
                                        //    fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        //drCertificado["FechaAyuda"] = fechaAyuda;
                                        drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                        drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();

                                        CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);

                                    }
                                    His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "CertificadoHA", CM);
                                    myreport.Show();

                                }
                            }
                            else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "4") //ES CONSULTA EXTERNA CON FECHA DE ALTA
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {
                                    drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                    DateTime FechaHoy = atencion.ATE_FECHA_ALTA.Value;
                                    FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    drCertificado["FechaAlta"] = FechadeAlta;
                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                    drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                    drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();
                                    dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                    drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                    //FechadeAlta = Fecha_En_Palabra(FechaHoy.ToString());
                                    FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                    drCertificado["FechaAlta"] = FechadeAlta;
                                    string resultado = "";
                                    if (Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) == 0)
                                        resultado = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString())).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()))));
                                    else
                                        resultado = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToString(FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1)));
                                    if (resultado.IndexOf(":") > 0)
                                        resultado = resultado.Substring(0, resultado.Length - 5);
                                    drCertificado["Dias_FinReposo"] = resultado;
                                    //FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                    //drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    if (medico.MED_RUC.Length > 10)
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                    }
                                    else
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    }

                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                    drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                    //string fechaAyuda = Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                    //fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                    string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                    if (fechaAyuda.IndexOf(":") > 0)
                                        fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                    drCertificado["FechaAyuda"] = fechaAyuda;
                                    //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                    //drCertificado["FechaAyuda"] = fechaAyuda;
                                    drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                    drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                    drCertificado["Tipo"] = "consulta externa";
                                    drCertificado["PathImagen"] = Certificado.path();
                                    drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                    drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                    CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                }
                                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                                reporte.Show();
                            }
                            else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "3") //ES CONSULTA EXTERNA CON FECHA DE ALTA
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {
                                    drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                    DateTime FechaHoy = atencion.ATE_FECHA_ALTA.Value;
                                    FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                    drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                    drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();
                                    dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                    drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                    //FechadeAlta = Fecha_En_Palabra(FechaHoy.ToString());
                                    FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                    drCertificado["FechaAlta"] = FechadeAlta;
                                    FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                    drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    if (medico.MED_RUC.Length > 10)
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                    }
                                    else
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    }
                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                    drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                    //string fechaAyuda = Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                    //fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                    string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                    if (fechaAyuda.IndexOf(":") > 0)
                                        fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                    drCertificado["FechaAyuda"] = fechaAyuda;
                                    //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                    //drCertificado["FechaAyuda"] = fechaAyuda;
                                    drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                    drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                    drCertificado["Tipo"] = "hospital del dia";
                                    drCertificado["PathImagen"] = Certificado.path();
                                    drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                    drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                    CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                }
                                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                                reporte.Show();
                            }
                            else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "10") //ES MUSHUÑAN SIN FECHA DE ALTA
                            {
                                DataRow drCertificado;
                                SUCURSALES sucursal = NegEmpresa.RecuperaEmpresaID(1);
                                if (reporteDatos.Rows[0][5].ToString() != "0")
                                {
                                    foreach (DataRow item in detalleReporteDatos.Rows)
                                    {
                                        drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                        //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        drCertificado["FechaIngreso"] = FechadeIngreso;
                                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                        DateTime FechaHoy = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                                        FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                           Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                        dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                        if (dias_reposo.IndexOf(":") > 0)
                                            dias_reposo = dias_reposo.Substring(0, dias_reposo.Length - 5);
                                        drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                        //FechadeAlta = Fecha_En_Palabra(FechaHoy.ToString());
                                        FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                        drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                        drCertificado["Empresa"] = sucursal.SUC_NOMBRE;
                                        drCertificado["Direccion_Empresa"] = sucursal.SUC_DIRECCION;
                                        drCertificado["Telefono_Empresa"] = sucursal.SUC_TELEFONO;
                                        drCertificado["emailEmpresa"] = sucursal.SUC_EMAIL;

                                        drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                        drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                        if (medico.MED_RUC.Length > 10)
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                        }
                                        else
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                        }
                                        drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                        drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                        if (atencion.ATE_FECHA_ALTA != null)
                                        {
                                            //string fechaAyuda = Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                            //fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                            ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            //drCertificado["FechaAyuda"] = fechaAyuda;
                                            string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                            if (fechaAyuda.IndexOf(":") > 0)
                                                fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            drCertificado["FechaAyuda"] = fechaAyuda;
                                        }
                                        else
                                        {
                                            //string fechaAyuda = Fecha_En_Palabra(DateTime.Now.ToString());
                                            //fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            //string fechaAyuda = Fecha_Actual_En_Palabra(DateTime.Now.ToString());
                                            //drCertificado["FechaAyuda"] = fechaAyuda;
                                            string fechaAyuda = DateTime.Now.ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(DateTime.Now.ToShortDateString());
                                            if (fechaAyuda.IndexOf(":") > 0)
                                                fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            drCertificado["FechaAyuda"] = fechaAyuda;
                                        }

                                        drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                        drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                        drCertificado["Tipo"] = "consulta externa";
                                        drCertificado["PathImagen"] = NegUtilitarios.RutaLogo("Mushuñan");
                                        drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                        drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();
                                        CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                    }
                                    His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                                    reporte.Show();
                                }
                                else
                                {
                                    foreach (DataRow item in detalleReporteDatos.Rows)
                                    {
                                        drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                        //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        drCertificado["FechaIngreso"] = FechadeIngreso;
                                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                        DateTime FechaHoy = Convert.ToDateTime(reporteDatos.Rows[0][4].ToString());
                                        FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                           Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                        dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                        if (dias_reposo.IndexOf(":") > 0)
                                            dias_reposo = dias_reposo.Substring(0, dias_reposo.Length - 5);
                                        drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                        //FechadeAlta = Fecha_En_Palabra(FechaHoy.ToString());
                                        FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                        drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                        drCertificado["Empresa"] = sucursal.SUC_NOMBRE;
                                        drCertificado["Direccion_Empresa"] = sucursal.SUC_DIRECCION;
                                        drCertificado["Telefono_Empresa"] = sucursal.SUC_TELEFONO;
                                        drCertificado["emailEmpresa"] = sucursal.SUC_EMAIL;

                                        drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                        drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                        if (medico.MED_RUC.Length > 10)
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                        }
                                        else
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                        }
                                        drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                        drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                        if (atencion.ATE_FECHA_ALTA != null)
                                        {
                                            //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                            ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            //drCertificado["FechaAyuda"] = fechaAyuda;
                                            string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                            if (fechaAyuda.IndexOf(":") > 0)
                                                fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            drCertificado["FechaAyuda"] = fechaAyuda;
                                        }
                                        else
                                        {
                                            //string fechaAyuda = Fecha_Actual_En_Palabra(DateTime.Now.ToString());
                                            ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            //drCertificado["FechaAyuda"] = fechaAyuda;
                                            string fechaAyuda = DateTime.Now.ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(DateTime.Now.ToShortDateString());
                                            if (fechaAyuda.IndexOf(":") > 0)
                                                fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            drCertificado["FechaAyuda"] = fechaAyuda;
                                        }

                                        drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                        drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                        drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                        drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();

                                        drCertificado["Tipo"] = "consulta externa";
                                        drCertificado["PathImagen"] = NegUtilitarios.RutaLogo("Mushuñan");
                                        CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                    }
                                    His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEAM", CM);
                                    reporte.Show();
                                }
                            }
                        }
                        else
                        {
                            if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "1") //ES EMERGENCIA SIN FECHA DE ALTA
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {
                                    drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    //FechadeIngreso = Fecha_En_Palabra(atencion.ATE_FECHA_INGRESO.ToString());
                                    FechadeIngreso = Fecha_Larga_En_Palabra(atencion.ATE_FECHA_INGRESO.ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                    drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                    drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();
                                    dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                    drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                    DateTime FechaHoy = DateTime.Now;
                                    //FechadeAlta = Fecha_En_Palabra(FechaHoy.ToShortDateString());
                                    FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                    drCertificado["FechaAlta"] = FechadeAlta;
                                    string fechaAyuda = FechaHoy.ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(FechaHoy.ToShortDateString());
                                    if (fechaAyuda.IndexOf(":") > 0)
                                        fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                    drCertificado["FechaAyuda"] = fechaAyuda;
                                    //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(FechaHoy).ToShortDateString());
                                    ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                    //drCertificado["FechaAyuda"] = fechaAyuda;

                                    FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                    drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                    // else
                                    // {
                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    if (medico.MED_RUC.Length > 10)
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                    }
                                    else
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    }
                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                    drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                    //}
                                    drCertificado["Tipo"] = "emergencia";
                                    drCertificado["PathImagen"] = Certificado.path();
                                    drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                    drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                    CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                }
                                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                                reporte.Show();
                            }
                            else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "2")//ES HOSPITALIZACION SIN FECHA DE ALTA
                            {
                                if (Tabla.Rows[0]["CER_TRATAMIENTO"].ToString() == "CLINICO")
                                {
                                    DataRow drCertificado;
                                    foreach (DataRow item in detalleReporteDatos.Rows)
                                    {
                                        drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                        //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        //if (FechadeIngreso.IndexOf(":") > 0)
                                        //    FechadeIngreso = FechadeIngreso.Substring(0, FechadeIngreso.Length - 5);
                                        drCertificado["FechaIngreso"] = FechadeIngreso.Substring(0, FechadeIngreso.Length - 11);
                                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];

                                        drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                        drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                        if (medico.MED_RUC.Length > 10)
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                        }
                                        else
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                        }
                                        drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                        drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);

                                        drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                        drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                        drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                        drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();

                                        drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                        drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                        drCertificado["tipoContongencia"] = Tabla.Rows[0][7].ToString();

                                        drCertificado["PathImagen"] = Certificado.path();
                                        drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + Tabla.Rows[0]["CER_TRATAMIENTO"].ToString();
                                        drCertificado["FechaTratamiento"] = "";
                                        drCertificado["Procedimiento"] = "NO APLICA";
                                        string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        if (fechaAyuda.IndexOf(":") > 0)
                                            fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        drCertificado["FechaAyuda"] = fechaAyuda;
                                        //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        //drCertificado["FechaAyuda"] = fechaAyuda;
                                        drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                        drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                        CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                                    }
                                    His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoHSA", CM);
                                    reporte.Show();
                                }
                                else
                                {
                                    DataRow drCertificado;
                                    foreach (DataRow item in detalleReporteDatos.Rows)
                                    {
                                        drCertificado = CM.Tables["HOSPITALARIO"].NewRow();
                                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                        //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        //if (FechadeIngreso.IndexOf(":") > 0)
                                        //    FechadeIngreso = FechadeIngreso.Substring(0, FechadeIngreso.Length - 5);
                                        drCertificado["FechaIngreso"] = FechadeIngreso;
                                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];

                                        drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                        drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                        if (medico.MED_RUC.Length > 10)
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                        }
                                        else
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                        }
                                        drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                        drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);

                                        drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                        drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                        drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                        drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();

                                        drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                        drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                        drCertificado["tipoContongencia"] = Tabla.Rows[0][7].ToString();

                                        drCertificado["PathImagen"] = Certificado.path();
                                        drCertificado["Tratamiento"] = "TIPO DE TRATAMIENTO: " + Tabla.Rows[0]["CER_TRATAMIENTO"].ToString();
                                        drCertificado["FechaTratamiento"] = "FECHA: " + Convert.ToDateTime(Tabla.Rows[0]["CER_FECHA_CIRUGIA"].ToString()).ToShortDateString();
                                        drCertificado["Procedimiento"] = Tabla.Rows[0]["CER_PROCEDIMIENTO"].ToString();
                                        string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        if (fechaAyuda.IndexOf(":") > 0)
                                            fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        drCertificado["FechaAyuda"] = fechaAyuda;
                                        //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        //drCertificado["FechaAyuda"] = fechaAyuda;
                                        drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                        drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                        CM.Tables["HOSPITALARIO"].Rows.Add(drCertificado);
                                    }
                                    His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoHSA", CM);
                                    reporte.Show();
                                }
                            }
                            else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "4") //ES CONSULTA EXTERNA SIN FECHA DE ALTA
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {
                                    drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                    DateTime FechaHoy = DateTime.Now;
                                    FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    drCertificado["FechaAlta"] = FechadeAlta;

                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                    drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                    drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();

                                    dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                    if (dias_reposo.IndexOf(":") > 0)
                                        dias_reposo = dias_reposo.Substring(0, dias_reposo.Length - 5);
                                    drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                    //FechadeAlta = Fecha_En_Palabra(FechaHoy.ToShortDateString());
                                    FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                    drCertificado["FechaAlta"] = FechadeAlta;
                                    FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                    drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    if (medico.MED_RUC.Length > 10)
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                    }
                                    else
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    }


                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                    drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                    if (atencion.ATE_FECHA_ALTA != null)
                                    {
                                        string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        if (fechaAyuda.IndexOf(":") > 0)
                                            fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        drCertificado["FechaAyuda"] = fechaAyuda;
                                        //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        //drCertificado["FechaAyuda"] = fechaAyuda;
                                    }
                                    else
                                    {
                                        string fechaAyuda = Fecha_Actual_En_Palabra(DateTime.Now.ToString());
                                        //fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        drCertificado["FechaAyuda"] = fechaAyuda;
                                    }
                                    drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                    drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                    drCertificado["Tipo"] = "consulta externa";
                                    drCertificado["PathImagen"] = Certificado.path();
                                    CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                }
                                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                                reporte.Show();
                            }
                            else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "3") //ES EMERGENCIA SIN FECHA DE ALTA
                            {
                                DataRow drCertificado;
                                foreach (DataRow item in detalleReporteDatos.Rows)
                                {
                                    drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                    drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                    drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                    drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                    //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                    drCertificado["FechaIngreso"] = FechadeIngreso;
                                    drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                    drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                    DateTime FechaHoy = DateTime.Now;
                                    //FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                    drCertificado["FechaAlta"] = FechadeAlta;

                                    drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString();
                                    drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                    drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();

                                    dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                    if (dias_reposo.IndexOf(":") > 0)
                                        dias_reposo = dias_reposo.Substring(0, dias_reposo.Length - 5);
                                    drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                    FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                    drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                    drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                    drCertificado["Empresa"] = reporteDatos.Rows[0][11].ToString();
                                    drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][12].ToString();
                                    drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][13].ToString();
                                    drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;

                                    drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                    drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                    if (medico.MED_RUC.Length > 10)
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                    }
                                    else
                                    {
                                        drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                    }
                                    drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                    drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                    if (atencion.ATE_FECHA_ALTA != null)
                                    {
                                        string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        if (fechaAyuda.IndexOf(":") > 0)
                                            fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        drCertificado["FechaAyuda"] = fechaAyuda;
                                        //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                        ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        //drCertificado["FechaAyuda"] = fechaAyuda;
                                    }
                                    else
                                    {
                                        string fechaAyuda = DateTime.Now.ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(DateTime.Now.ToShortDateString());
                                        if (fechaAyuda.IndexOf(":") > 0)
                                            fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        drCertificado["FechaAyuda"] = fechaAyuda;
                                        //string fechaAyuda = Fecha_Actual_En_Palabra(DateTime.Now.ToString());
                                        ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                        //drCertificado["FechaAyuda"] = fechaAyuda;
                                    }

                                    drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                    drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                    drCertificado["Tipo"] = "hospital del día";
                                    drCertificado["PathImagen"] = Certificado.path();
                                    CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                }
                                His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                                reporte.Show();
                            }
                            else if (Tabla.Rows[0]["CER_TIPO_INGRESO"].ToString() == "10") //ES CONSULTA EXTERNA SIN FECHA DE ALTA
                            {
                                DataRow drCertificado;
                                SUCURSALES sucursal = NegEmpresa.RecuperaEmpresaID(1);
                                if (reporteDatos.Rows[0][5].ToString() != "0")
                                {
                                    foreach (DataRow item in detalleReporteDatos.Rows)
                                    {
                                        drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                        //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        drCertificado["FechaIngreso"] = FechadeIngreso;
                                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                        DateTime FechaHoy = DateTime.Now;
                                        FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                           Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                        dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                        if (dias_reposo.IndexOf(":") > 0)
                                            dias_reposo = dias_reposo.Substring(0, dias_reposo.Length - 5);
                                        drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                        //FechadeAlta = Fecha_En_Palabra(FechaHoy.ToString());
                                        FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                        drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                        drCertificado["Empresa"] = sucursal.SUC_NOMBRE;
                                        drCertificado["Direccion_Empresa"] = sucursal.SUC_DIRECCION;
                                        drCertificado["Telefono_Empresa"] = sucursal.SUC_TELEFONO;
                                        drCertificado["emailEmpresa"] = sucursal.SUC_EMAIL;

                                        drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                        drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                        if (medico.MED_RUC.Length > 10)
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                        }
                                        else
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                        }


                                        drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                        drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                        if (atencion.ATE_FECHA_ALTA != null)
                                        {
                                            string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                            if (fechaAyuda.IndexOf(":") > 0)
                                                fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            drCertificado["FechaAyuda"] = fechaAyuda;
                                            //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                            ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            //drCertificado["FechaAyuda"] = fechaAyuda;
                                        }
                                        else
                                        {
                                            string fechaAyuda = DateTime.Now.ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(DateTime.Now.ToShortDateString());
                                            if (fechaAyuda.IndexOf(":") > 0)
                                                fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            drCertificado["FechaAyuda"] = fechaAyuda;
                                            //string fechaAyuda = Fecha_Actual_En_Palabra(DateTime.Now.ToString());
                                            ////fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            //drCertificado["FechaAyuda"] = fechaAyuda;
                                        }

                                        drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                        drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                        drCertificado["Tipo"] = "consulta externa";
                                        drCertificado["PathImagen"] = NegUtilitarios.RutaLogo("Mushuñan");
                                        drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                        drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();
                                        CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                    }
                                    His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEA", CM);
                                    reporte.Show();
                                }
                                else
                                {
                                    foreach (DataRow item in detalleReporteDatos.Rows)
                                    {
                                        drCertificado = CM.Tables["EMERGENCIA"].NewRow();
                                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                                        //FechadeIngreso = Fecha_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                                        drCertificado["FechaIngreso"] = FechadeIngreso;
                                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                                        DateTime FechaHoy = DateTime.Now;
                                        FechadeAlta = FechaHoy.ToString("dd") + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        drCertificado["Observacion"] = reporteDatos.Rows[0][10].ToString() + "\r\n\r\nACTIVIDAD LABORAL: " +
                                           Tabla.Rows[0]["CER_ACTIVIDAD_LABORAL"].ToString() + "\r\nTIPO CONTINGENCIA: " + Tabla.Rows[0]["CER_CONTINGENCIA"].ToString();

                                        dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][5].ToString());
                                        if (dias_reposo.IndexOf(":") > 0)
                                            dias_reposo = dias_reposo.Substring(0, dias_reposo.Length - 5);
                                        drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][5].ToString() + " (" + dias_reposo + ")";
                                        //FechadeAlta = Fecha_En_Palabra(FechaHoy.ToString());
                                        FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                                        drCertificado["FechaAlta"] = FechadeAlta;
                                        FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][5].ToString()) - 1);
                                        drCertificado["Dias_FinReposo"] = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][9].ToString();
                                        drCertificado["Empresa"] = sucursal.SUC_NOMBRE;
                                        drCertificado["Direccion_Empresa"] = sucursal.SUC_DIRECCION;
                                        drCertificado["Telefono_Empresa"] = sucursal.SUC_TELEFONO;
                                        drCertificado["emailEmpresa"] = sucursal.SUC_EMAIL;

                                        drCertificado["Nombre_Medico"] = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                                        drCertificado["Email_Medico"] = medico.MED_EMAIL;
                                        if (medico.MED_RUC.Length > 10)
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                                        }
                                        else
                                        {
                                            drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                                        }
                                        drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO;
                                        drCertificado["espMedica"] = NegEspecialidades.Especialidad(medico.MED_CODIGO);
                                        if (atencion.ATE_FECHA_ALTA != null)
                                        {
                                            string fechaAyuda = Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                            if (fechaAyuda.IndexOf(":") > 0)
                                                fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            drCertificado["FechaAyuda"] = fechaAyuda;
                                            //string fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                                            //drCertificado["FechaAyuda"] = fechaAyuda;
                                        }
                                        else
                                        {
                                            string fechaAyuda = DateTime.Now.ToString().Substring(0, 11) + ' ' + Fecha_En_Palabra(DateTime.Now.ToShortDateString());
                                            if (fechaAyuda.IndexOf(":") > 0)
                                                fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            drCertificado["FechaAyuda"] = fechaAyuda;
                                            //string fechaAyuda = Fecha_Actual_En_Palabra(DateTime.Now.ToString());
                                            //fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                                            //drCertificado["FechaAyuda"] = fechaAyuda;
                                        }

                                        drCertificado["direccionPaciente"] = reporteDatos.Rows[0][14].ToString();
                                        drCertificado["telefonoContactoP"] = reporteDatos.Rows[0][15].ToString();
                                        drCertificado["actividadLaboral"] = Tabla.Rows[0][6].ToString();
                                        drCertificado["tipoContingencia"] = Tabla.Rows[0][7].ToString();

                                        drCertificado["Tipo"] = "consulta externa";
                                        drCertificado["PathImagen"] = NegUtilitarios.RutaLogo("Mushuñan");
                                        CM.Tables["EMERGENCIA"].Rows.Add(drCertificado);
                                    }
                                    His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoEAM", CM);
                                    reporte.Show();
                                }
                            }
                        }
                    }
                    else
                        MessageBox.Show("Los certificados anulados no pueden imprimirse.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Int32 ingreso;
                    string path = "";
                    ingreso = NegTipoIngreso.RecuperarporAtencion(Convert.ToInt64(atencion.ATE_CODIGO));
                    switch (ingreso)
                    {
                        case 10:
                            path = NegUtilitarios.RutaLogo("Mushuñan");
                            break;
                        case 12:
                            path = NegUtilitarios.RutaLogo("BrigadaMedica");
                            break;
                        default:
                            path = Certificado.path();
                            break;
                    }
                    DataRow drCertificado;
                    foreach (DataRow item in detalleReporteDatos.Rows)
                    {
                        drCertificado = CM.Tables["IESS"].NewRow();
                        drCertificado["Paciente"] = reporteDatos.Rows[0][0].ToString();
                        drCertificado["Identificacion"] = reporteDatos.Rows[0][1].ToString();
                        drCertificado["HC"] = reporteDatos.Rows[0][2].ToString();
                        FechadeIngreso = Fecha_Larga_En_Palabra(reporteDatos.Rows[0][3].ToString());
                        drCertificado["FechaIngreso"] = FechadeIngreso;
                        dias_reposo = Dia_En_Palabras(reporteDatos.Rows[0][7].ToString());
                        drCertificado["Dias_Reposo"] = drCertificado["Dias_Reposo"] = reporteDatos.Rows[0][7].ToString() + " (" + dias_reposo + ")";
                        DateTime FechaHoy = Convert.ToDateTime(reporteDatos.Rows[0][28].ToString());
                        if (atencion.ATE_FECHA_ALTA.ToString() != "")
                            FechaHoy = atencion.ATE_FECHA_ALTA.Value;
                        FechadeAlta = Fecha_Larga_En_Palabra(FechaHoy.ToString());
                        string FechadeAltahoras = Fecha_En_Palabra_Hora(FechaHoy.ToString());
                        drCertificado["FechaAlta"] = FechadeAlta;

                        FechaHoy = FechaHoy.AddDays(Convert.ToInt32(reporteDatos.Rows[0][7].ToString()) - 1);
                        //string finreposo = FechaHoy.ToString("dd") + " (" + Dia_En_Palabras(FechaHoy.ToString("dd")) + ")" + " de " + FechaHoy.ToString("MMMM") + " del " + FechaHoy.ToString("yyyy");
                        string finreposo = Fecha_En_Palabra_Hora(Convert.ToString(FechaHoy));

                        string fechaAyuda = "";
                        string fechaAyudaHora = "";
                        if (reporteDatos.Rows[0]["ATE_FECHA_ALTA"].ToString() != "")
                        {
                            ///fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(atencion.ATE_FECHA_ALTA).ToShortDateString());
                            fechaAyudaHora = Fecha_En_Palabra_Hora(Convert.ToDateTime(reporteDatos.Rows[0][28].ToString()).ToShortDateString());
                            fechaAyuda = Fecha_Actual_En_Palabra(Convert.ToDateTime(reporteDatos.Rows[0][28].ToString()).ToShortDateString());
                        }
                        else
                        {
                            fechaAyudaHora = FechadeAltahoras;
                            fechaAyuda = Fecha_Actual_En_Palabra(DateTime.Now.ToShortDateString());
                        }

                        //fechaAyuda = fechaAyuda.Substring(0, fechaAyuda.Length - 5);
                        drCertificado["FechaAyuda"] = fechaAyuda;
                        if (Convert.ToBoolean(reporteDatos.Rows[0][15].ToString()))
                        {
                            drCertificado["Dias_FinReposo"] = "Desde: " + fechaAyudaHora + "  Hasta: " + finreposo;

                        }
                        drCertificado["NombreMedico"] = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                        drCertificado["Email_Medico"] = medico.MED_EMAIL.ToString();
                        if (medico.MED_RUC.Length > 10)
                        {
                            drCertificado["Identificacion_Medico"] = medico.MED_RUC.Substring(0, 10);
                        }
                        else
                        {
                            drCertificado["Identificacion_Medico"] = medico.MED_RUC;
                        }
                        drCertificado["telefonoMedico"] = medico.MED_TELEFONO_CONSULTORIO.ToString();

                        drCertificado["Empresa"] = reporteDatos.Rows[0][22].ToString();
                        drCertificado["Direccion_Empresa"] = reporteDatos.Rows[0][23].ToString();
                        drCertificado["Telefono_Empresa"] = reporteDatos.Rows[0][24].ToString();
                        drCertificado["emailEmpresa"] = empresa.EMP_EMAIL;
                        drCertificado["Num_Certificado"] = reporteDatos.Rows[0][5].ToString();
                        DataTable estado = NegCertificadoMedico.TIPO_INGRESO_IESS(atencion.ATE_CODIGO);
                        drCertificado["tipo"] = estado.Rows[0][0].ToString();

                        drCertificado["institucionLabora"] = reporteDatos.Rows[0][9].ToString();
                        drCertificado["actividadLaboral"] = reporteDatos.Rows[0][10].ToString();
                        drCertificado["tipoContingencia"] = reporteDatos.Rows[0][27].ToString();

                        drCertificado["sintomas"] = reporteDatos.Rows[0][6].ToString();
                        drCertificado["confirmado"] = reporteDatos.Rows[0][8].ToString();
                        drCertificado["nota"] = reporteDatos.Rows[0][18].ToString();

                        if (Convert.ToBoolean(reporteDatos.Rows[0][13]))
                        {
                            drCertificado["eSi"] = "X";
                        }
                        else
                        {
                            drCertificado["eNo"] = "X";
                        }
                        if (Convert.ToBoolean(reporteDatos.Rows[0][14]))
                        {
                            drCertificado["sSi"] = "X";
                        }
                        else
                        {
                            drCertificado["sNo"] = "X";
                        }
                        if (Convert.ToBoolean(reporteDatos.Rows[0][15]))
                        {
                            drCertificado["rSi"] = "X";
                        }
                        else
                        {
                            drCertificado["rNo"] = "X";
                        }
                        if (Convert.ToBoolean(reporteDatos.Rows[0][16]))
                        {
                            drCertificado["aSi"] = "X";
                        }
                        else
                        {
                            drCertificado["aNo"] = "X";
                        }
                        if (Convert.ToBoolean(reporteDatos.Rows[0][17]))
                        {
                            drCertificado["tSi"] = "X";
                        }
                        else
                        {
                            drCertificado["tNo"] = "X";
                        }

                        drCertificado["actividadLaboral"] = reporteDatos.Rows[0][10].ToString();

                        drCertificado["Cie_Codigo"] = item["CIE_CODIGO"];
                        drCertificado["Cie_Descripcion"] = item["CIE_DESCRIPCION"];
                        drCertificado["PathImagen"] = path;
                        DataTable espMedicas = NegMedicos.RecuperaEspecialidadMed(Convert.ToInt32(medico.MED_CODIGO));
                        drCertificado["Especialidad_Medico"] = espMedicas.Rows[0][0].ToString();


                        drCertificado["FechaTratamiento"] = "FECHA: " + Convert.ToDateTime(TablaIESS.Rows[0]["CMI_FECHA_CIRUGIA"].ToString()).ToShortDateString();
                        drCertificado["Procedimiento"] = reporteDatos.Rows[0][32].ToString();

                        drCertificado["direccionPaciente"] = reporteDatos.Rows[0][11].ToString();
                        drCertificado["telefonoPaciente"] = reporteDatos.Rows[0][12].ToString();
                        drCertificado["Tratamiento"] = reporteDatos.Rows[0][31].ToString();
                        //string proce = String.Format("<strong>{0} </strong>", "PROCEDIMIENTO: ");


                        CM.Tables["IESS"].Rows.Add(drCertificado);
                    }
                    if (Convert.ToInt32(reporteDatos.Rows[0][30].ToString()) != 1)
                    {
                        if (reporteDatos.Rows[0][31].ToString() == "QUIRURGICO")
                        {
                            His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoProcesoIESS", CM);
                            reporte.Show();
                        }
                        else
                        {
                            His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoProcesoClinicoIESS", CM);
                            reporte.Show();
                        }
                    }
                    else
                    {
                        His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "CertificadoIESS", CM);
                        reporte.Show();
                    }

                }

            }
            else
            {
                MessageBox.Show("Debe elegir un certificado para continuar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #region fecha en palabras anterior
        //public string Fecha_En_Palabra(string fecha)
        //{
        //    string fechaprueba;
        //    CultureInfo cul = new CultureInfo("es");
        //    DateTime FI = Convert.ToDateTime(fecha, cul);
        //    if (Convert.ToInt32(FI.ToString("dd")) == 1)
        //    {
        //        fechaprueba = "1 (UNO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm", cul);
        //        return fechaprueba;
        //    }
        //    if (Convert.ToInt32(FI.ToString("dd")) == 2)
        //    {
        //        fechaprueba = "2 (DOS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 3)
        //    {
        //        fechaprueba = "3 (TRES) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 4)
        //    {
        //        fechaprueba = "4 (CUATRO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 5)
        //    {
        //        fechaprueba = "5 (CINCO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 6)
        //    {
        //        fechaprueba = "6 (SEIS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 7)
        //    {
        //        fechaprueba = "7 (SIETE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 8)
        //    {
        //        fechaprueba = "8 (OCHO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 9)
        //    {
        //        fechaprueba = "9 (NUEVE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 10)
        //    {
        //        fechaprueba = "10 (DIEZ) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 11)
        //    {
        //        fechaprueba = "11 (ONCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 12)
        //    {
        //        fechaprueba = "12 (DOCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 13)
        //    {
        //        fechaprueba = "13 (TRECE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 14)
        //    {
        //        fechaprueba = "14 (CATORCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 15)
        //    {
        //        fechaprueba = "15 (QUINCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 16)
        //    {
        //        fechaprueba = "16 (DIECISEIS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 17)
        //    {
        //        fechaprueba = "17 (DIECISIETE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 18)
        //    {
        //        fechaprueba = "18 (DIECIOCHO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 19)
        //    {
        //        fechaprueba = "19 (DIECINUEVE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 20)
        //    {
        //        fechaprueba = "20 (VEINTE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 21)
        //    {
        //        fechaprueba = "21 (VEINTIUNO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 22)
        //    {
        //        fechaprueba = "22 (VEINTIDOS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 23)
        //    {
        //        fechaprueba = "23 (VEINTITRES) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 24)
        //    {
        //        fechaprueba = "24 (VEINTICUATRO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 25)
        //    {
        //        fechaprueba = "25 (VEINTICINCO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 26)
        //    {
        //        fechaprueba = "26 (VEINTISEIS) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 27)
        //    {
        //        fechaprueba = "27 (VEINTISIETE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 28)
        //    {
        //        fechaprueba = "28 (VEINTIOCHO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 29)
        //    {
        //        fechaprueba = "29 (VEINTINUEVE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 30)
        //    {
        //        fechaprueba = "30 (TREINTA) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    if (Convert.ToUInt32(FI.ToString("dd")) == 31)
        //    {
        //        fechaprueba = "31 (TREINTA Y UNO) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy") + " " + FI.ToString("HH:mm");
        //        return fechaprueba;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        #endregion
        public string Dia_En_Palabras(string dia)
        {
            string prueba;
            string num2Text = ""; int value = Convert.ToInt32(dia);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UNO";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + Dia_En_Palabras(Convert.ToString(value - 10));
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + Dia_En_Palabras(Convert.ToString(value - 20));
            else if (value == 30) num2Text = "TREINTA";
            else if (value < 40) num2Text = "TREINTA Y " + Dia_En_Palabras(Convert.ToString(value - 30));
            else if (value == 40) num2Text = "CUARENTA";
            else if (value < 50) num2Text = "CUARENTA Y " + Dia_En_Palabras(Convert.ToString(value - 40));
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            return num2Text;

        }
        #region Fecha en Palabras sin horas
        public string Fecha_En_Palabra_Hora(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha, cul);
            if (Convert.ToInt32(FI.ToString("dd")) == 1)
            {
                fechaprueba = fecha.Substring(0, 10) + " (UNO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToInt32(FI.ToString("dd")) == 2)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DOS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 3)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TRES DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 4)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CUATRO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 5)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CINCO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 6)
            {
                fechaprueba = fecha.Substring(0, 10) + " (SEIS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 7)
            {
                fechaprueba = fecha.Substring(0, 10) + " (SIETE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 8)
            {
                fechaprueba = fecha.Substring(0, 10) + " (OCHO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 9)
            {
                fechaprueba = fecha.Substring(0, 10) + " (NUEVE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 10)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIEZ DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 11)
            {
                fechaprueba = fecha.Substring(0, 10) + " (ONCE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 12)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DOCE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 13)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TRECE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 14)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CATORCE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 15)
            {
                fechaprueba = fecha.Substring(0, 10) + " (QUINCE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 16)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECISEIS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 17)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECISIETE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 18)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECIOCHO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 19)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECINUEVE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 20)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 21)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIUNO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 22)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIDOS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 23)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTITRES DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 24)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTICUATRO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 25)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTICINCO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 26)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTISEIS DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 27)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTISIETE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 28)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIOCHO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 29)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTINUEVE DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 30)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TREINTA DE " + FI.ToString("MMMM") + " DEL DOS MIL" + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 31)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TREINTA Y UNO DE " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ")";
                return fechaprueba.ToUpper();
            }
            else
                return "";
        }
        #endregion
        #region Fecha Larga en Palabras
        public string Fecha_Larga_En_Palabra(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha, cul);
            if (Convert.ToInt32(FI.ToString("dd")) == 1)
            {
                fechaprueba = fecha.Substring(0, 10) + " (UNO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToInt32(FI.ToString("dd")) == 2)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DOS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 3)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TRES de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 4)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CUATRO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 5)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CINCO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 6)
            {
                fechaprueba = fecha.Substring(0, 10) + " (SEIS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 7)
            {
                fechaprueba = fecha.Substring(0, 10) + " (SIETE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 8)
            {
                fechaprueba = fecha.Substring(0, 10) + " (OCHO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 9)
            {
                fechaprueba = fecha.Substring(0, 10) + " (NUEVE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 10)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIEZ de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 11)
            {
                fechaprueba = fecha.Substring(0, 10) + " (ONCE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 12)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DOCE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 13)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TRECE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 14)
            {
                fechaprueba = fecha.Substring(0, 10) + " (CATORCE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 15)
            {
                fechaprueba = fecha.Substring(0, 10) + " (QUINCE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 16)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECISEIS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 17)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECISIETE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 18)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECIOCHO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 19)
            {
                fechaprueba = fecha.Substring(0, 10) + " (DIECINUEVE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 20)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 21)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIUNO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 22)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIDOS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 23)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTITRES de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 24)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTICUATRO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 25)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTICINCO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 26)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTISEIS de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 27)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTISIETE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 28)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTIOCHO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 29)
            {
                fechaprueba = fecha.Substring(0, 10) + " (VEINTINUEVE de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 30)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TREINTA de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else if (Convert.ToUInt32(FI.ToString("dd")) == 31)
            {
                fechaprueba = fecha.Substring(0, 10) + " (TREINTA Y UNO de " + FI.ToString("MMMM") + " DEL DOS MIL " + Dia_En_Palabras(FI.ToString("yy")) + ") a las " + FI.ToString("HH:mm", cul);
                return fechaprueba.ToUpper();
            }
            else
                return "";
        }
        #endregion
        #region Fecha Actual
        public string Fecha_Actual_En_Palabra(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha, cul);
            if (Convert.ToInt32(FI.ToString("dd")) == 1)
            {
                fechaprueba = FI.ToString("dddd") + " 1 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToInt32(FI.ToString("dd")) == 2)
            {
                fechaprueba = FI.ToString("dddd") + " 2  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 3)
            {
                fechaprueba = FI.ToString("dddd") + " 3  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 4)
            {
                fechaprueba = FI.ToString("dddd") + " 4  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 5)
            {
                fechaprueba = FI.ToString("dddd") + " 5  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 6)
            {
                fechaprueba = FI.ToString("dddd") + " 6  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 7)
            {
                fechaprueba = FI.ToString("dddd") + " 7  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 8)
            {
                fechaprueba = FI.ToString("dddd") + " 8  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 9)
            {
                fechaprueba = FI.ToString("dddd") + " 9  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 10)
            {
                fechaprueba = FI.ToString("dddd") + " 10 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 11)
            {
                fechaprueba = FI.ToString("dddd") + " 11 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 12)
            {
                fechaprueba = FI.ToString("dddd") + " 12 (DOCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 13)
            {
                fechaprueba = FI.ToString("dddd") + " 13 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 14)
            {
                fechaprueba = FI.ToString("dddd") + " 14 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 15)
            {
                fechaprueba = FI.ToString("dddd") + " 15 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 16)
            {
                fechaprueba = FI.ToString("dddd") + " 16 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 17)
            {
                fechaprueba = FI.ToString("dddd") + " 17 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 18)
            {
                fechaprueba = FI.ToString("dddd") + " 18 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 19)
            {
                fechaprueba = FI.ToString("dddd") + " 19 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 20)
            {
                fechaprueba = FI.ToString("dddd") + " 20 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 21)
            {
                fechaprueba = FI.ToString("dddd") + " 21 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 22)
            {
                fechaprueba = FI.ToString("dddd") + " 22 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 23)
            {
                fechaprueba = FI.ToString("dddd") + " 23 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 24)
            {
                fechaprueba = FI.ToString("dddd") + " 24 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 25)
            {
                fechaprueba = FI.ToString("dddd") + " 25 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 26)
            {
                fechaprueba = FI.ToString("dddd") + " 26 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 27)
            {
                fechaprueba = FI.ToString("dddd") + " 27 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 28)
            {
                fechaprueba = FI.ToString("dddd") + " 28 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 29)
            {
                fechaprueba = FI.ToString("dddd") + " 29 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 30)
            {
                fechaprueba = FI.ToString("dddd") + " 30 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 31)
            {
                fechaprueba = FI.ToString("dddd") + " 31 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else
            {
                return "";
            }
        }
        #endregion
        private void btnanular_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Para inhabilitar un certificado necesita la autorización de Dirección Médica", "HIS3000", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                UltraGridRow fila = this.UltraGridCertificados.ActiveRow;
                frmAnulaCertificado frm = new frmAnulaCertificado(Convert.ToInt32(fila.Cells["NRO CERTIFICADO"].Value.ToString()), Convert.ToString(fila.Cells["MEDICO"].Value.ToString()), Convert.ToString(fila.Cells["TIPO CERTIFICADO"].Value.ToString()));
                frm.ShowDialog();
                CargarCertificados();
            }
        }

        private void chkAnulados_CheckedChanged(object sender, EventArgs e)
        {
            CargarCertificados();
        }
        #region Fecha en Palabras
        public string Fecha_En_Palabra(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha, cul);
            string diaEnPalabras = ConvertirDiaEnPalabras(FI.Day);

            string anioEnPalabras = ConvertirAnioEnPalabras(FI.Year);

            fechaprueba = "(" + diaEnPalabras + " DE " + FI.ToString("MMMM").ToUpper() + " DEL " + anioEnPalabras + ")";
            return fechaprueba;

        }

        private string ConvertirDiaEnPalabras(int dia)
        {
            string[] numeros = {"",  "UNO", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE",
                        "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISEIS", "DIECISIETE",
                        "DIECIOCHO", "DIECINUEVE", "VEINTE", "VEINTIUNO", "VEINTIDOS", "VEINTITRES",
                        "VEINTICUATRO", "VEINTICINCO", "VEINTISEIS", "VEINTISIETE", "VEINTIOCHO", "VEINTINUEVE", "TREINTA", "TREINTA Y UNO"};
            return numeros[dia];
        }

        // Función para convertir el año a palabras (solo para los años hasta el 2999)
        private string ConvertirAnioEnPalabras(int anio)
        {
            string[] unidades = { "", "UN", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE" };
            string[] decenas = { "", "DIEZ", "VEINTI", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA" };
            string[] especiales = { "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISEIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE" };

            if (anio >= 1000 && anio <= 2999)
            {
                int unidad = anio % 10;
                int decena = (anio / 10) % 10;
                int centena = (anio / 100) % 10;
                int millar = (anio / 1000) % 10;

                string anioEnPalabras = "";

                if (millar > 0)
                    anioEnPalabras += unidades[millar] + " MIL ";

                if (centena > 0)
                    anioEnPalabras += unidades[centena] + " CIENTOS ";

                if (decena > 0)
                {
                    if (decena == 1)
                        anioEnPalabras += especiales[unidad];
                    else
                    {
                        if (unidad > 0)
                            anioEnPalabras += decenas[decena] + unidades[unidad];
                        else
                            anioEnPalabras += decenas[decena];
                    }
                }
                else
                {
                    if (unidad > 0)
                        anioEnPalabras += unidades[unidad];
                }

                return anioEnPalabras;
            }
            else
            {
                return "Año no soportado";
            }
        }
        #endregion
    }
}
