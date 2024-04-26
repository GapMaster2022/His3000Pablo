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
    public partial class frm_ExploradorCertificadoAsistencia : Form
    {
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        public frm_ExploradorCertificadoAsistencia()
        {
            InitializeComponent();
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            dtpFiltroDesde.Value = oPrimerDiaDelMes;
            dtpFiltroHasta.Value = DateTime.Now;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCertificados();
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            if (UltraGridCertificados.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = this.UltraGridCertificados.ActiveRow;
                
                string path = "";
                int ingreso = NegTipoIngreso.RecuperarporAtencion(Convert.ToInt64(fila.Cells["NRO ATENCION"].Value.ToString()));
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
                CerificadoPrecentacionDatos CM = new CerificadoPrecentacionDatos();
                CERTIFICADO_PRESENTACION certPre = new CERTIFICADO_PRESENTACION();
                MEDICOS medico = NegCertificadoMedico.DatosMedicos(Convert.ToInt64(fila.Cells["MED_CODIGO"].Value.ToString()));
                DataTable espMedicas = NegMedicos.RecuperaEspecialidadMed(Convert.ToInt32(fila.Cells["MED_CODIGO"].Value.ToString()));
                string especialidadesMed = espMedicas.Rows[0][0].ToString();
                certPre = NegCertificadoMedico.RecuperarCertificadoPresentacion(Convert.ToInt64(fila.Cells["NRO ATENCION"].Value.ToString()),Convert.ToInt64(fila.Cells["MED_CODIGO"].Value.ToString()));
                DataRow drCertificado;
                drCertificado = CM.Tables["Certificado"].NewRow();
                drCertificado["nombre"] = certPre.apellido1 + " " + certPre.apellido2 + " " + certPre.nombre1 + " " + certPre.nombre2;
                drCertificado["cedula"] = certPre.cedula;
                drCertificado["fechaIngreso"] = certPre.fechaIngreso.ToString().Substring(0, 16);
                drCertificado["fechaAlta"] = certPre.fechaAlta.ToString().Substring(0, 16);
                drCertificado["consulta"] = certPre.tipo;
                drCertificado["medico"] = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                drCertificado["imagen"] = path;
                drCertificado["id"] = certPre.id;
                drCertificado["hc"] = certPre.hc;
                drCertificado["identificacion"] = medico.MED_RUC.Substring(0, 10);
                drCertificado["especialidad"] = especialidadesMed;
                drCertificado["email"] = medico.MED_EMAIL;
                drCertificado["telefono"] = medico.MED_TELEFONO_CONSULTORIO;
                drCertificado["fechaEmision"] = Fecha_Actual_En_Palabra(Convert.ToDateTime(certPre.fechaAlta.ToString()).ToShortDateString()); ;
                CM.Tables["Certificado"].Rows.Add(drCertificado);

                His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "CertificadoPresentacion", CM);
                myreport.Show();
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
        private void Fecha_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkAnulados_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Nhc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtf1_Click(object sender, EventArgs e)
        {

        }

        private void chkEstado_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbTipo_CheckedChanged(object sender, EventArgs e)
        {

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
                    UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadosPresentacion(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, estado);
                else
                {
                    USUARIOS datosUsuario = new USUARIOS();
                    datosUsuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
                    if (datosUsuario != null)
                    {
                        Int64 codigoMedico = NegCertificadoMedico.Med_CodigoCertificadoAsistencia(datosUsuario.IDENTIFICACION);
                        UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadoPresentacionXmedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, Convert.ToInt32(codigoMedico), estado);

                    }
                    else
                        UltraGridCertificados.DataSource = NegCertificadoMedico.CertificadoPresentacionXmedicos(dtpFiltroDesde.Value.Date, dtpFiltroHasta.Value, Sesion.codUsuario, estado);

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

        private void frm_ExploradorCertificadoAsistencia_Load(object sender, EventArgs e)
        {
            CargarCertificados();
        }

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

        private void UltraGridCertificados_InitializeLayout(object sender, InitializeLayoutEventArgs e)
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
                UltraGridCertificados.DisplayLayout.Bands[0].Columns[3].Width = 500;
                UltraGridCertificados.DisplayLayout.Bands[0].Columns[4].Width = 500;
                //UltraGridCertificados.DisplayLayout.Bands[0].Columns[6].Width = 500;

                //agrandamiento de filas 

                ////Ocultar columnas, que son fundamentales al levantar informacion
                UltraGridCertificados.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Hidden = true;
                UltraGridCertificados.DisplayLayout.Bands[0].Columns["TIPO CERTIFICADO"].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
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
    }
}
